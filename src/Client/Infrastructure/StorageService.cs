using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Infrastructure
{
    public class StorageService
    {
        private readonly HttpClient httpClient;
        public const long maxFileSize = 1024 * 1024 * 10; // 10MB
        public StorageService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task UploadImageAsync(Uri sas, IBrowserFile file)
        {
            var content = new StreamContent(file.OpenReadStream(maxFileSize));
            content.Headers.Add("x-ms-blob-type", "BlockBlob");
            var response = await httpClient.PutAsync(sas, content);
            response.EnsureSuccessStatusCode();
        }
    }
}
