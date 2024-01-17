using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SESWeb
{
    public enum FileDownloadPathType
    {
        PhysicalPath = 1,
        WebURL = 2,
        SharePointPath = 3,
        AzureBlob = 4

    }
    public enum RequestedFileNotFoundAction
    {
        DownloadAlternateFile =1,
        Raise404Error = 2
    }
    public class SecuredFileDownload
    {
        public string FilePath;
        public string NewFileName;
        public bool IsDynamicFileName;
        public FileDownloadPathType FilePathType;
        public RequestedFileNotFoundAction FileNotFoundAction;
        public string AlternateFilePath;
        public bool IsDynamicFilePath;
        private Dictionary<string,FileDownloadHeader> _headers;

        public SecuredFileDownload(string filePath, FileDownloadPathType filePathType = FileDownloadPathType.PhysicalPath, bool isDynamicFilePath = false, string newFileName = "", bool isDynamicFileName = false, RequestedFileNotFoundAction fileNotFoundAction = RequestedFileNotFoundAction.Raise404Error,string alternateFilePath = "")
        {
            FilePath = filePath;
            NewFileName = newFileName;
            FilePathType = filePathType;
            IsDynamicFilePath = isDynamicFilePath;
            IsDynamicFileName = isDynamicFileName;
            FileNotFoundAction = fileNotFoundAction;
            AlternateFilePath = alternateFilePath;
            _headers = new Dictionary<string, FileDownloadHeader>();
        }
        public void SetHeader(string HeaderName, string HeaderValue,bool IsDynamicValue = false)
        {
            if (_headers.ContainsKey(HeaderName) == true)
            {
                _headers.Remove(HeaderName);
            }
            _headers.Add(HeaderName,new FileDownloadHeader(HeaderName, HeaderValue, IsDynamicValue));
        }
        public void RemoveHeader(string HeaderName)
        {
            _headers.Remove(HeaderName);
        }
        public string GenerateUrl(string HandlerPath = "", bool IsStaticFile = false)
        {
            Guid g = Guid.NewGuid();
            string id = g.ToString();
            if (IsStaticFile)
            {
                id = string.Format("{0}_{1}", this.FilePath.Replace("/", ""), DateTime.Now.ToString("yyyyMMdd"));
            }
            SecuredFile sf = new SecuredFile();
            sf.FilePath = this.FilePath;
            sf.FilePathType = this.FilePathType;
            sf.Headers = this._headers.Values.ToList<FileDownloadHeader>();
            sf.NewFileName = this.NewFileName;
            sf.IsDynamicFilePath = this.IsDynamicFilePath;
            sf.IsDynamicFileName = this.IsDynamicFileName;
            sf.FileNotFoundAction = this.FileNotFoundAction;
            sf.AlternateFilePath = this.AlternateFilePath;

            BISecurity bis = new BISecurity();
            bis.QueryStringsToEncrypt.Clear();
            bis.QueryStringsToEncrypt.Add("id", id);
            string url = "download.ashx?q=";
            if (HandlerPath != "")
            {
                url = HandlerPath;
            }
            url += bis.QueryStringsToEncrypt.getEncryptedString();

            System.Web.HttpContext.Current.Session.Add(id, sf);
            return url;
        }


    }
    public class FileDownloadHeader
    {
        public FileDownloadHeader(string headerName, string headerValue, bool isDynamicValue = false)
        {
            HeaderName = headerName;
            HeaderValue = headerValue;
            IsDynamicValue = isDynamicValue;
        }
        public string HeaderName;
        public string HeaderValue;
        public bool IsDynamicValue;
    }
    public class SecuredFile
    {
        public string FilePath;
        public string NewFileName;
        public FileDownloadPathType FilePathType;
        public bool IsDynamicFilePath;
        public bool IsDynamicFileName;
        public string AlternateFilePath;
        public RequestedFileNotFoundAction FileNotFoundAction;
        public List<FileDownloadHeader> Headers;
    }
}