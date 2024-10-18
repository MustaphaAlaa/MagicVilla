using MagicVilla_VillaApi.Model;
using MagicVilla_VillaApi.Model.DTO;
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

        public async Task<VillaNumber?> UpdateAsync(VillaNumber villaNo, int id)
        {
            VillaNumber? villaNumber = await _db.villaNumbers.FirstOrDefaultAsync(v => v.VillaNo == id);

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


        public override async Task<IEnumerable<VillaNumber>> GetAllAsync()
        {
            return await _db.villaNumbers.Include(vm => vm.Villa).ToListAsync();

        }


        public override async Task<VillaNumber?> Get(Expression<Func<VillaNumber, bool>> exp)
        {
            return await _db.villaNumbers.AsNoTracking().Include(v => v.Villa).FirstOrDefaultAsync(exp);
        }

    }
}
