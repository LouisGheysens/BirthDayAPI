global using Azure.Storage.Blobs;
global using System.Net;
global using System.Text;
global using Microsoft.Extensions.Configuration;


namespace Business.Azure
{
    public sealed class AzureSingleton
    {
        private static AzureSingleton instance = null;
        private static readonly object padlock = new object();
        private static readonly BlobServiceClient _blobClient;

        static AzureSingleton() => _blobClient = new BlobServiceClient(AppRetriever.GetAzureKey());

        public static AzureSingleton Instance
        {
            get
            {
                if(instance == null)
                {
                    lock (padlock)
                    {
                        if(instance == null)
                        {
                            instance = new AzureSingleton();
                        }
                    }
                }
                return instance;
            }
        }

        public string UploadToAzure(string file)
        {
            string firstSentence = file.Split('.').First();
            string extension = file.Split(".").Last();
            file = $"{firstSentence}_{DateTime.Now.ToString("yyyyMMddHHmmssffff")}.{extension}";
            var blobContainer = _blobClient?.GetBlobContainerClient("upload-file");
            var blobClient = blobContainer.GetBlobClient(file);
            byte[] collection = Encoding.ASCII.GetBytes(file);
            using (var ms = new MemoryStream(collection, false))
            {
                blobClient.Upload(ms);
            }
            return file;
        }


        public string GetFileFromAzure(string file)
        {
            var blobContainer = _blobClient?.GetBlobContainerClient("upload-file");
            var blobClient = blobContainer?.GetBlobClient(file);
            var base64 = string.Empty;
            using (var client = new WebClient())
            {
                byte[] dataBytes;
                dataBytes = client.DownloadData(blobClient.Uri);
                base64 = Convert.ToBase64String(dataBytes);
            }
            return base64;
        }


        public void DeleteFromAzure(string file)
        {
            var blobContainer = _blobClient?.GetBlobContainerClient("upload-file");
            var blobClient = blobContainer?.GetBlobClient(file);
            blobClient.DeleteIfExists();
        }






    }
}
