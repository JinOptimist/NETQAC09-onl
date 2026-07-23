using WebAppSmile.Services.Interfaces;

namespace WebAppSmile.Services
{
    public class ApiHelper : IApiHelper
    {
        private IMyJsonSerializer _myJsonSerializer;

        public ApiHelper(IMyJsonSerializer myJsonSerializer)
        {
            _myJsonSerializer = myJsonSerializer;
        }

        public async Task<T> GetDataFromApiAsync<T>(string url)
        {
            var http = new HttpClient();
            var jokeTask = http.GetAsync(url);
            var result = await jokeTask;
            var dto = await _myJsonSerializer.Serialize<T>(result.Content);
            return dto;
        }
    }
}
