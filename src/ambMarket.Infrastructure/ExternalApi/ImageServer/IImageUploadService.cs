using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RestSharp;

namespace ambMarket.Infrastructure.ExternalApi.ImageServer
{
    public interface IImageUploadService
    {
        Task<List<string>> Upload(List<IFormFile> files);
    }
    public class ImageUploadService : IImageUploadService
    {
        public async Task<List<string>> Upload(List<IFormFile> files)
        {
            var restClientOption = new RestClientOptions("https://localhost:7123/api/Images?apikey=mysecretkey")
            {
                Timeout = -1,
                ThrowOnDeserializationError = true,
                ThrowOnAnyError = true,
                
            };
            var client = new RestClient(restClientOption);
           
            var request = new RestRequest();
            foreach (var item in files)
            {
                byte[] bytes;
                using (var ms = new MemoryStream())
                {
                 await   item.CopyToAsync(ms);
                    bytes = ms.ToArray();
                }
                request.AddFile(item.FileName, bytes, item.FileName, item.ContentType);
            }


            var response = await client.PostAsync(request); 
            UploadDto  upload = JsonConvert.DeserializeObject<UploadDto>(response.Content);
            return upload.FileNameAddress;

        }
    }


    public class UploadDto
    {
        public bool Status { get; set; }
        public List<string> FileNameAddress { get; set; }
    }
}
