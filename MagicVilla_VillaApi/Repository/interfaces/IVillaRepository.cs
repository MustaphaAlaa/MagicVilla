using MagicVilla_VillaApi.Model;
using MagicVilla_VillaApi.Model.DTO;

namespace MagicVilla_VillaApi.Repository.interfaces
{
    public interface IVillaRepository : IRepository<Villa>
    {
        Task<Villa?> UpdateAsync(Villa villaDTO);
    }
}
