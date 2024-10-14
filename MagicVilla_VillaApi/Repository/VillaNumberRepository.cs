using MagicVilla_VillaApi.Model;
using MagicVilla_VillaApi.Repository.interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net.Sockets;

namespace MagicVilla_VillaApi.Repository
{
    public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository
    {
        VillaDbContext _db;


        public VillaNumberRepository(VillaDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<VillaNumber?> UpdateAsync(VillaNumber villaNo)
        {
            VillaNumber? villaNumber = await _db.villaNumbers.FirstOrDefaultAsync(v => v.VillaNo == villaNo.VillaNo);

            if (villaNumber == null)
                return null;

            villaNumber.SpecialDetails = villaNo.SpecialDetails;
            villaNumber.VillaNo = villaNo.VillaNo;
            villaNumber.VillaId = villaNo.VillaId;
            villaNumber.UpdatedAt = DateTime.Now;

            _db.Update(villaNumber);

            int updated = await _db.SaveChangesAsync();

            return updated > 0 ? villaNo : null;

        }
    }
}
