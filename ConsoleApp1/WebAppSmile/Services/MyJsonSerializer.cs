using WebAppSmile.Services.Interfaces;

namespace WebAppSmile.Services
{
    public class MyJsonSerializer : IMyJsonSerializer
    {
        public async Task<T> Serialize<T>(HttpContent content)
        {
            return await content.ReadFromJsonAsync<T>();
        }
    }
}
