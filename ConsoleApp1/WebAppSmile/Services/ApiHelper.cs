using Microsoft.JSInterop;
using System.Reflection;
using System.Runtime.InteropServices.Marshalling;
using WebAppSmile.Models;
using WebAppSmile.Services.Interfaces;

namespace WebAppSmile.Services
{
    public class ApiHelper : IApiHelper
    {
        private IMyJsonSerializer _myJsonSerializer;
        private IWebHostEnvironment _webHostEnvironment;

        public ApiHelper(IMyJsonSerializer myJsonSerializer, IWebHostEnvironment webHostEnvironment)
        {
            _myJsonSerializer = myJsonSerializer;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<SomeTypeParam> GetDataFromApiAsync<SomeTypeParam>(string url)
        {
            var http = new HttpClient();
            var jokeTask = http.GetAsync(url);
            var result = await jokeTask;
            var dto = await _myJsonSerializer.Serialize<SomeTypeParam>(result.Content);
            return dto;
        }

        public async Task<string> SaveImageAndGetLinkToIt(string url)
        {
            var http = new HttpClient();
            var jokeTask = http.GetAsync(url);// https://cataas.com/cat
            var result = await jokeTask;

            using var streamFromClient = result.Content.ReadAsStream();

            var pathToWwwRoot = _webHostEnvironment.WebRootPath;
            var imageFolderPath = "images";
            
            var guid = Guid.NewGuid();
            var fileName = $"{guid}.jpg";

            var path = Path.Combine(pathToWwwRoot, imageFolderPath, fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            using var fileStream = new FileStream(path, FileMode.CreateNew);
            streamFromClient.CopyTo(fileStream);

            return $"/{imageFolderPath}/{fileName}";
        }
    }
}
