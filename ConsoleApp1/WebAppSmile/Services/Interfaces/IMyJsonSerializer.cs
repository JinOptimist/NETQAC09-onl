namespace WebAppSmile.Services.Interfaces
{
    public interface IMyJsonSerializer
    {
        Task<T> Serialize<T>(HttpContent content);
    }
}