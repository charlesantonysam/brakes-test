using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace SESWeb
{
    public class BlobAndFileUtils
    {
        public class BlobFile
        {
            public string FileSize { get; set; }
            public string FileType { get; set; }
            public MemoryStream FileContent { get; set; }
        }
        #region Upload Or Download Documents To Azure Blob
        public bool UploadDocumentToAzureBlob(Stream DocumnetStream, string _DocumentPath)
        {
            try
            {
                string _AzureBlobContainer = ConfigurationManager.AppSettings.Get("AzureBlobContainer"); // Blob Container Name
                string _AzureBlobConnectionString = ConfigurationManager.AppSettings.Get("AzureBlobConnectionString"); // Blob Connection String

                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(_AzureBlobConnectionString); // Establishing Connection with Azure Blob Storage
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient(); // Creating Azure Blob Client
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(_AzureBlobContainer); // Accessing Specific Container to upload file

                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(_DocumentPath); // Create Blob Reference for the file to upload

                // Start : Setting Shared access key generation parameter
                SharedAccessBlobPolicy sasConstraints = new SharedAccessBlobPolicy();
                sasConstraints.SharedAccessStartTime = DateTimeOffset.UtcNow.AddMinutes(-5); // Access key validity start time
                sasConstraints.SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddMinutes(5); // Access key validity end time
                sasConstraints.Permissions = SharedAccessBlobPermissions.Write | SharedAccessBlobPermissions.Read; // Creating read and write permission for Access Key to blob
                                                                                                                   // End : Setting Shared access key generation parameter

                var sasBlobToken = cloudBlockBlob.GetSharedAccessSignature(sasConstraints); // Creating Shared Access for blob
                var BlobURL = cloudBlockBlob.Uri + sasBlobToken; // Concatinating URL of Blob and Access Key
                CloudBlockBlob blob = new CloudBlockBlob(new Uri(BlobURL)); // Create blob reference with Access Key for the blob

                blob.UploadFromStream(DocumnetStream); // Uploading file as blob to azure blob

                DocumnetStream.Close();
                DocumnetStream.Dispose();

                if (blob.Exists()) // To check upload is successful or not
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool IsBlobExists(string _DocumentPath)
        {
            try
            {
                var containerName = ConfigurationManager.AppSettings.Get("AzureBlobContainer");
                string storageConnection = ConfigurationManager.AppSettings.Get("AzureBlobConnectionString");
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(storageConnection);
                CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = blobClient.GetContainerReference(containerName);
                CloudBlockBlob blockBlob = cloudBlobContainer.GetBlockBlobReference(_DocumentPath);

                return blockBlob.Exists();

            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteBlob(string _DocumentPath)
        {
            try
            {
                var containerName = ConfigurationManager.AppSettings.Get("AzureBlobContainer");
                string storageConnection = ConfigurationManager.AppSettings.Get("AzureBlobConnectionString");
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(storageConnection);
                CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = blobClient.GetContainerReference(containerName);
                CloudBlockBlob blockBlob = cloudBlobContainer.GetBlockBlobReference(_DocumentPath);

                if (blockBlob.Exists() && (!blockBlob.IsDeleted))
                {
                    blockBlob.Delete();
                    return blockBlob.IsDeleted;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string CopyBlob(string _SourceDocumentPath, string _DestinationDocumentPath, bool DeleteSourceDocumentAfterCopy = false)
        {
            var containerName = ConfigurationManager.AppSettings.Get("AzureBlobContainer");
            string storageConnection = ConfigurationManager.AppSettings.Get("AzureBlobConnectionString");

            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(storageConnection);
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);

            CloudBlockBlob sourceBlockBlob = cloudBlobContainer.GetBlockBlobReference(_SourceDocumentPath);
            CloudBlockBlob targetBlockBlob = cloudBlobContainer.GetBlockBlobReference(_DestinationDocumentPath);

            SharedAccessBlobPolicy sasConstraints = new SharedAccessBlobPolicy();
            sasConstraints.SharedAccessStartTime = DateTimeOffset.UtcNow.AddMinutes(-5);
            sasConstraints.SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddMinutes(5);
            sasConstraints.Permissions = SharedAccessBlobPermissions.Write | SharedAccessBlobPermissions.Read | SharedAccessBlobPermissions.Delete;

            var sasSourceBlobToken = sourceBlockBlob.GetSharedAccessSignature(sasConstraints);
            var SourceBlobURL = sourceBlockBlob.Uri + sasSourceBlobToken;
            CloudBlockBlob sourceBlob = new CloudBlockBlob(new Uri(SourceBlobURL));

            var sasTargetBlobToken = targetBlockBlob.GetSharedAccessSignature(sasConstraints);
            var TargetBlobURL = targetBlockBlob.Uri + sasTargetBlobToken;
            CloudBlockBlob TargetBlob = new CloudBlockBlob(new Uri(TargetBlobURL));

            string result = TargetBlob.StartCopy(sourceBlob);

            if (DeleteSourceDocumentAfterCopy == true)
            {
                sourceBlob.Delete();
            }

            return result;

        }

        public BlobFile DownloadFileFromBlob(string _DocumentPath)
        {
            try
            {
                //   byte[] bts = null;
                var containerName = ConfigurationManager.AppSettings.Get("AzureBlobContainer");

                string storageConnection = ConfigurationManager.AppSettings.Get("AzureBlobConnectionString");
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(storageConnection);
                CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = blobClient.GetContainerReference(containerName);
                CloudBlockBlob blockBlob = cloudBlobContainer.GetBlockBlobReference(_DocumentPath);

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

                    BlobFile _BlobFile = new BlobFile();


                    _BlobFile.FileSize = blob.Properties.Length.ToString();
                    _BlobFile.FileType = blob.Properties.ContentType.ToString();
                    _BlobFile.FileContent = memStream;

                    return _BlobFile;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        public class BlobInfo
        {
            private BlobAndFileUtils cm;
            public string BlobName { get; set; }
            public BlobFile File { get; set; }
            public BlobInfo(string blobName, bool makeDownloadReady = false)
            {
                cm = new BlobAndFileUtils();
                this.BlobName = blobName;
                if (makeDownloadReady == true)
                {
                    this.File = cm.DownloadFileFromBlob(this.BlobName);
                }
                else
                {
                    this.File = null;
                }
            }
            public bool Exists()
            {
                return cm.IsBlobExists(this.BlobName);
            }
            public bool SaveAs(Stream DocumnetStream)
            {
                return cm.UploadDocumentToAzureBlob(DocumnetStream, this.BlobName);
            }
            public bool Delete()
            {
                return cm.DeleteBlob(this.BlobName);
            }
            public string CopyTo(string DestinationBlobName)
            {
                return cm.CopyBlob(this.BlobName, DestinationBlobName);
            }
            public string CopyFrom(string SourceBlobName)
            {
                return cm.CopyBlob(SourceBlobName, this.BlobName);
            }
            public string MoveTo(string DestinationBlobName)
            {
                return cm.CopyBlob(this.BlobName, DestinationBlobName, true);
            }
        }


    }
}