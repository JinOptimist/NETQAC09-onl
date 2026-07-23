namespace WebAppSmile.Services.Interfaces
{
    public interface IApiHelper
    {
        Task<T> GetDataFromApiAsync<T>(string url);
        Task<string> SaveImageAndGetLinkToIt(string url);
    }
}