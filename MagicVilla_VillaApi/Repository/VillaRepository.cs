using AutoMapper;
using MagicVilla_VillaApi.Model;
using MagicVilla_VillaApi.Model.DTO;
using MagicVilla_VillaApi.Repository.interfaces;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaApi.Repository
{
    public class VillaRepository : Repository<Villa>, IVillaRepository
    {

        VillaDbContext _db;
        private readonly IMapper _mapper;
        public VillaRepository(IMapper mapper, VillaDbContext db) : base(db)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<Villa?> UpdateAsync(Villa VillaUpdateRequest)
        {
            Villa? villa = await _db.Villas.FirstOrDefaultAsync(v => v.Id == VillaUpdateRequest.Id);

            if (villa == null)
                return null;

            villa.Occupancy = VillaUpdateRequest.Occupancy;
            villa.Rate = VillaUpdateRequest.Rate;
            villa.Sqft = VillaUpdateRequest.Sqft;
            villa.Amenity = VillaUpdateRequest.Amenity;
            villa.Details = VillaUpdateRequest.Details;
            villa.ImageUrl = VillaUpdateRequest.ImageUrl;
            villa.UpdatedAt = DateTime.Now;
            villa.Name = VillaUpdateRequest.Name;


            _db.Villas.Update(villa);

            int updated = await _db.SaveChangesAsync();

            return updated > 0 ? VillaUpdateRequest : null;

        }
    }
}
