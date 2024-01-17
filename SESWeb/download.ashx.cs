using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.SessionState;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace SESWeb
{
    /// <summary>
    /// Summary description for download
    /// </summary>
    public class download : IHttpHandler, IRequiresSessionState
    {
        public string FileLength = "";
        public void ProcessRequest(HttpContext context)
        {
            byte[] bts = null;
            byte[] altbts = null;
            HttpResponse Response = HttpContext.Current.Response;
            HttpRequest Request = HttpContext.Current.Request;
            bool FileNotFound = false;
            string Msg = "";

            bool DownloadAlternateFile = false;
            string AlternateFilePath = "";
            if (Request.QueryString["q"] != null)
            {
                try
                {
                    BISecurity bis = new BISecurity();
                    bis.setQueryStringToDecrypt(Request.QueryString["q"].ToString());
                    if (bis.getDecryptedQueryString("id") != null)
                    {
                        string id = bis.getDecryptedQueryString("id").ToString();

                        if (HttpContext.Current.Session[id] != null)
                        {
                            Response.Clear();
                            Response.ClearHeaders();


                             SecuredFile sf  = (SecuredFile)HttpContext.Current.Session[id];
                             if (sf.FileNotFoundAction == RequestedFileNotFoundAction.DownloadAlternateFile)
                             {
                                 DownloadAlternateFile = true;
                                 AlternateFilePath = sf.AlternateFilePath;
                             }
                             if (sf.IsDynamicFileName == true)
                             {
                                 sf.NewFileName = getDynamicValue(sf.NewFileName);
                             }
                             if (sf.IsDynamicFilePath == true)
                             {
                                 sf.FilePath = getDynamicValue(sf.FilePath);
                             }

                             if (sf.FilePathType == FileDownloadPathType.PhysicalPath)
                             {
                                bts = System.IO.File.ReadAllBytes(sf.FilePath);
                             }
                             if (sf.FilePathType == FileDownloadPathType.WebURL)
                             {
                                 try
                                 {
                                     var webClient = new WebClient();
                                     bts = webClient.DownloadData(sf.FilePath);
                                     webClient.Dispose();
                                     webClient = null;
                                 }
                                 catch (Exception we)
                                 {
                                     if (sf.FileNotFoundAction == RequestedFileNotFoundAction.Raise404Error)
                                     {
                                         Response.ClearHeaders();
                                         FileNotFound = true;
                                         Msg = we.Message;
                                         Response.Write("Error while downloading file!<br>" + Msg);
                                         Response.StatusCode = 404;
                                         Response.End();
                                     }
                                     else if (sf.FileNotFoundAction == RequestedFileNotFoundAction.DownloadAlternateFile)
                                     {
                                         Response.WriteFile(sf.AlternateFilePath);
                                     }
                                     else
                                     {

                                     }
                                     return;
                                 }
                             }

                            if (sf.FilePathType == FileDownloadPathType.SharePointPath)
                            {
                                try
                                {
                                    //var webClient = new WebClient();
                                    //bts = webClient.DownloadData(sf.FilePath);
                                    //webClient.Dispose();
                                    //webClient = null;

                                    Uri targetWeb = new Uri(HttpContext.Current.Session["hostWeb"].ToString());

                                    ClientContext clientContext = null;
                                    string targetRealm = TokenHelper.GetRealmFromTargetUrl(targetWeb);
                                    var responseToken = TokenHelper.GetAppOnlyAccessToken(TokenHelper.SharePointPrincipal, targetWeb.Authority, targetRealm);
                                    clientContext = TokenHelper.GetClientContextWithAccessToken(HttpContext.Current.Session["hostWeb"].ToString(), responseToken.AccessToken);

                                    var web = clientContext.Web;
                                    clientContext.Load(web);
                                    clientContext.ExecuteQuery();

                                    List list = web.Lists.GetByTitle(ConfigurationManager.AppSettings["DocumentListName"].ToString());
                                    clientContext.Load(list);

                                    Microsoft.SharePoint.Client.File file = web.GetFileByServerRelativeUrl(sf.FilePath);
                                    clientContext.Load(file);
                                    ClientResult<System.IO.Stream> stream = file.OpenBinaryStream();
                                    clientContext.ExecuteQuery();

                                    using (System.IO.MemoryStream mStream = new System.IO.MemoryStream())
                                    {
                                        if (stream != null)
                                        {
                                            stream.Value.CopyTo(mStream);
                                            bts = mStream.ToArray();
                                            //string b64String = Convert.ToBase64String(imageArray);
                                        }
                                    }
                                }
                                catch (Exception we)
                                {
                                    if (sf.FileNotFoundAction == RequestedFileNotFoundAction.Raise404Error)
                                    {
                                        Response.ClearHeaders();
                                        FileNotFound = true;
                                        Msg = we.Message;
                                        Response.Write("Error while downloading file!<br>" + Msg);
                                        Response.StatusCode = 404;
                                        Response.End();
                                    }
                                    else if (sf.FileNotFoundAction == RequestedFileNotFoundAction.DownloadAlternateFile)
                                    {
                                        Response.WriteFile(sf.AlternateFilePath);
                                    }
                                    else
                                    {

                                    }
                                    return;
                                }
                            }

                            if (sf.FilePathType == FileDownloadPathType.AzureBlob)
                            {
                                try
                                {
                                    var containerName = ConfigurationManager.AppSettings.Get("AzureBlobContainer");

                                    string storageConnection = ConfigurationManager.AppSettings.Get("AzureBlobConnectionString");
                                    CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(storageConnection);
                                    CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();
                                    CloudBlobContainer cloudBlobContainer = blobClient.GetContainerReference(containerName);
                                    CloudBlockBlob blockBlob = cloudBlobContainer.GetBlockBlobReference(sf.FilePath);

                                    if (blockBlob.Exists())
                                    {
                                        SharedAccessBlobPolicy sasConstraints = new SharedAccessBlobPolicy();
                                        sasConstraints.SharedAccessStartTime = DateTimeOffset.UtcNow.AddMinutes(-5);
                                        sasConstraints.SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddMinutes(5);
                                        sasConstraints.Permissions = SharedAccessBlobPermissions.Read;

                                        var sasBlobToken = blockBlob.GetSharedAccessSignature(sasConstraints);
                                        System.IO.MemoryStream memStream = new System.IO.MemoryStream();

                                        var BlobURL = blockBlob.Uri + sasBlobToken;
                                        //blockBlob.DownloadToStream(memStream);

                                        CloudBlockBlob blob = new CloudBlockBlob(new Uri(BlobURL));
                                        blob.DownloadToStream(memStream);

                                        if (memStream != null)
                                        {
                                            //memStream.Value.CopyTo(mStream);
                                            bts = memStream.ToArray();
                                            //string b64String = Convert.ToBase64String(imageArray);
                                        }
                                    }
                                    else
                                    {
                                        /** added by Yuvaraj Arasu on 28-Jul-2020 -- **/
                                        if (sf.FileNotFoundAction == RequestedFileNotFoundAction.Raise404Error)
                                        {
                                            Response.ClearHeaders();
                                            FileNotFound = true;
                                            Msg = "Blob file not exists";
                                            Response.Write("Error while downloading file!<br>" + Msg);
                                        }
                                        else if (sf.FileNotFoundAction == RequestedFileNotFoundAction.DownloadAlternateFile)
                                        {
                                            Response.WriteFile(sf.AlternateFilePath);
                                        }
                                        else
                                        {
                                            Response.Write("File not available!<br>");
                                            //Response.StatusCode = 404;
                                            //Response.End();
                                        }
                                        return;
                                    }
                                }
                                catch (Exception we)
                                {
                                    if (sf.FileNotFoundAction == RequestedFileNotFoundAction.Raise404Error)
                                    {
                                        Response.ClearHeaders();
                                        FileNotFound = true;
                                        Msg = we.Message;
                                        Response.Write("Error while downloading file!<br>" + Msg);
                                        //Response.StatusCode = 404;
                                        //Response.End();
                                    }
                                    else if (sf.FileNotFoundAction == RequestedFileNotFoundAction.DownloadAlternateFile)
                                    {
                                        Response.WriteFile(sf.AlternateFilePath);
                                    }
                                    else
                                    {

                                    }
                                    return;
                                }
                            }

                            /*The below FileLength must be set before using getDynamicValue() for Headers. Because in headers this FileLength may be used for content-length header.*/
                            FileLength = bts.Length.ToString();
                             //Response.AddHeader("content-length", bts.Length.ToString());
                             foreach (FileDownloadHeader fdh in sf.Headers)
                             {
                                 if (fdh.IsDynamicValue == true)
                                 {
                                     fdh.HeaderValue = getDynamicValue(fdh.HeaderValue);
                                 }
                                 Response.AddHeader(fdh.HeaderName, fdh.HeaderValue);
                             }
                             Response.BinaryWrite(bts);
                             //FileLength = bts.Length.ToString();
                        }
                        Response.Flush();
                        return;
                        //Response.End();
                    }

                }
                catch (Exception e)
                {
                    if (DownloadAlternateFile == false) {
                    Response.ClearHeaders();
                    FileNotFound = true;
                    Msg = e.Message;
                    Response.Write("Error while downloading file !<br>");
                    //Response.StatusCode = 500;
                    //Response.End();
                    }
                    else
                    {
                        if (AlternateFilePath != "")
                        {
                            Response.WriteFile(AlternateFilePath);
                        }
                    }
                    return;
                }
            }

        }
        private string getDynamicValue(string value)
        {

            value = value.Replace("%UserID%", System.Web.HttpContext.Current.Session["UserID"].ToString());
            value = value.Replace("%UserName%", System.Web.HttpContext.Current.Session["UserName"].ToString());
            value = value.Replace("%yyyy%", DateTime.Now.ToString("yyyy"));
            value = value.Replace("%MMM%", DateTime.Now.ToString("MMM"));
            value = value.Replace("%MM%", DateTime.Now.ToString("MM"));
            value = value.Replace("%DD%", DateTime.Now.ToString("DD"));
            value = value.Replace("%hh%", DateTime.Now.ToString("hh"));
            value = value.Replace("%mm%", DateTime.Now.ToString("mm"));
            value = value.Replace("%ss%", DateTime.Now.ToString("ss"));
            value = value.Replace("%FileLengthInBytes%", FileLength);

            return value;

        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}