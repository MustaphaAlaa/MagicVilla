using MagicVilla_Web.Models.DTO;

namespace MagicVilla_Web.Services.IServices
{
    public interface IVillaService : IGetAsync, IDeleteAsync
    {
        Task<T> CreateAsync<T>(CreateVillaRequest dto);
        Task<T> UpdateAsync<T>(VillaDTO dto);


    }
}
