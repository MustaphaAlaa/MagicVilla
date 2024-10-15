using MagicVilla_Web.Models.DTO;

namespace MagicVilla_Web.Services.IServices
{
    public interface IVillaNumberService : IGetAsync, IDeleteAsync
    {
        Task<T> CreateAsync<T>(CreateVillaNumberRequest dto);
        Task<T> UpdateAsync<T>(VillaNumberDTO dto);
    }
}
