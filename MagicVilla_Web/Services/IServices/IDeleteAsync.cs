namespace MagicVilla_Web.Services.IServices
{
    public interface IDeleteAsync
    {
        Task<T> DeleteAsync<T>(int id);
    }
}
