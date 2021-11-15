using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.Extensions.Configuration;
using System;

namespace Services.Common
{
    public class BlobStorageService : IStorageService
    {
        private readonly string connectionString;
        private const string containerName = "images";

        public string StorageBaseUri => "https://hogentdemostorage.blob.core.windows.net/images/";

        public BlobStorageService(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("Storage");
        }

        public Uri CreateUploadUri(string filename)
        {
            var blobServiceClient = new BlobServiceClient(connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = containerClient.GetBlobClient(filename);
            var blobSasBuilder = new BlobSasBuilder
            {
                ExpiresOn = DateTime.UtcNow.AddMinutes(5),
                BlobContainerName = containerName,
                BlobName = filename,
            };
            blobSasBuilder.SetPermissions(BlobSasPermissions.Read | BlobSasPermissions.Write | BlobSasPermissions.Create);
            var sas = blobClient.GenerateSasUri(blobSasBuilder);
            return sas;
        }
    }
}
