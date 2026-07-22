namespace WebAppSmile.Services;

public class IceApiDataService 
{
    public async Task<T> GetDataFromApiAsync<T>(string url) // универсальный метод для получения данных с API
    {
        var http = new HttpClient();
        var response = await http.GetAsync(url);
        var dto = await response.Content.ReadFromJsonAsync<T>();
        return dto;
    }
}
