namespace MagicVilla_Web.Services.IServices
{
    public interface IGetAsync
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
    }
}
