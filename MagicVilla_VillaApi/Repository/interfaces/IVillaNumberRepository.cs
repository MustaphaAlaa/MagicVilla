using MagicVilla_VillaApi.Model;
using MagicVilla_VillaApi.Model.DTO;

namespace MagicVilla_VillaApi.Repository.interfaces
{
    public interface IVillaNumberRepository : IRepository<VillaNumber>
    {
        Task<VillaNumber?> UpdateAsync(VillaNumber villaNo);
    }
}
