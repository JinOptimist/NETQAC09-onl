using System.Net.Http.Json;
using WebAppSmile.Models;

namespace WebAppSmile.Services
{
    public class FlowerApiService
    {
        private readonly HttpClient _http;


        public FlowerApiService(HttpClient http)
        {
            _http = http;
        }


        public async Task<FlowerDto?> GetFlower()
        {
            var data = await _http.GetFromJsonAsync<FlowerResponseDto>(
                "https://api.gbif.org/v1/species/search?q=flower&limit=10"
            );


            if (data == null || data.results.Count == 0)
            {
                return null;
            }
            var random = new Random();

            return data.results[random.Next(data.results.Count)];  


        return data.results[0];
        }
    }
}