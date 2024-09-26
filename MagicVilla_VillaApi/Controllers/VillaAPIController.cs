using MagicVilla_VillaApi.Model;
using MagicVilla_VillaApi.Model.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaApiController : ControllerBase
    {

        ILogger<VillaApiController> _log;
        VillaDbContext _db;



        public VillaApiController(ILogger<VillaApiController> log, VillaDbContext db)
        {
            _log = log;
            _db = db;
        }

        [HttpGet]
        public List<VillaDTO> GetAll()
        {

            _log.LogInformation($"Hit GetAll() In {this.GetType().Name}");
            return _db.Villas.Select(villa =>
                        new VillaDTO
                        {
                            Id = villa.Id,
                            Name = villa.Name,
                            Amenity = villa.Amenity,
                            Details = villa.Details,
                            ImageUrl = villa.ImageUrl,
                            Occupancy = villa.Occupancy,
                            Rate = villa.Rate,
                            Sqft = villa.Sqft
                        })
                .ToList();

        }

        [HttpGet("{id:int}", Name = "GETVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO?> GetVillaById(int id)
        {
            _log.LogInformation($"Hit GetVillaById() In {this.GetType().Name}");

            if (id <= 0)
            {
                _log.LogError($"id: {id}, Is Not Valid");
                return BadRequest();
            }

            Villa? villa = _db.Villas.FirstOrDefault(v => v.Id == id);

            if (villa == null)
            {
                _log.LogWarning($"id: {id}, Is Not Found");
                return NotFound();
            }

            VillaDTO villaDTO = new VillaDTO
            {
                Id = villa.Id,
                Name = villa.Name,
                Amenity = villa.Amenity,
                Details = villa.Details,
                ImageUrl = villa.ImageUrl,
                Occupancy = villa.Occupancy,
                Rate = villa.Rate,
                Sqft = villa.Sqft
            };

            return Ok(villaDTO);

        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO?> CreateVilla(VillaDTO villa)
        {

            //if (!ModelState.IsValid)
            //    return BadRequest();

            if (_db.Villas.FirstOrDefault(v => v.Name == villa.Name) != null)
            {
                ModelState.AddModelError("", "Dublicated Name");
                return BadRequest(ModelState);
            }


            if (villa.Id != 0)
            {
                return BadRequest();
            }


            var newVilla = new Villa()
            {

                Name = villa.Name,
                Amenity = villa.Amenity,
                Details = villa.Details,
                ImageUrl = villa.ImageUrl,
                Occupancy = villa.Occupancy,
                Rate = villa.Rate,
                Sqft = villa.Sqft,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,

            };

            _db.Villas.Add(newVilla);
            _db.SaveChanges();


            return CreatedAtRoute("GETVilla", new { Id = newVilla.Id }, newVilla);


        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            Villa? villa = _db.Villas.FirstOrDefault(v => v.Id == id);

            if (villa == null)
            {
                return NotFound();
            }

            _db.Villas.Remove(villa);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult UpdateVilla(VillaDTO villaReq)
        {
            if (villaReq == null || villaReq.Id <= 0)
            {
                return BadRequest(villaReq);
            }


            Villa? villa = _db.Villas.FirstOrDefault(v => v.Id == villaReq.Id);

            if (villa == null)
            {
                return NotFound();
            }


            villa.Name = villaReq.Name;
            villa.Rate = villaReq.Rate;
            villa.Sqft = villaReq.Sqft;
            villa.Occupancy = villaReq.Occupancy;
            villa.Amenity = villaReq.Amenity;
            villa.Details = villaReq.Details;
            villa.ImageUrl = villaReq.ImageUrl;

            _db.Update(villa);
            _db.SaveChanges();

            return NoContent();

        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest(patchDocument);
            }


            Villa? villa = _db.Villas.FirstOrDefault(v => v.Id == id);

            if (villa == null)
            {
                return NotFound();
            }

            VillaDTO villaDTO = new VillaDTO
            {
                Id = villa.Id,
                Name = villa.Name,
                Amenity = villa.Amenity,
                Details = villa.Details,
                ImageUrl = villa.ImageUrl,
                Occupancy = villa.Occupancy,
                Rate = villa.Rate,
                Sqft = villa.Sqft
            };

            patchDocument.ApplyTo(villaDTO, ModelState);




            villa.Name = villaDTO.Name;
            villa.Amenity = villaDTO.Amenity;
            villa.Details = villaDTO.Details;
            villa.ImageUrl = villaDTO.ImageUrl;
            villa.Occupancy = villaDTO.Occupancy;
            villa.Rate = villaDTO.Rate;
            villa.Sqft = villaDTO.Sqft;


            _db.Update(villa);
            _db.SaveChanges();


            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            return NoContent();
        }




    }
}
