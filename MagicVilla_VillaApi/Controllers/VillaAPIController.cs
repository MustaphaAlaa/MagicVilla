using AutoMapper;
using MagicVilla_VillaApi.Model;
using MagicVilla_VillaApi.Model.DTO;
using MagicVilla_VillaApi.Repository.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaApiController : ControllerBase
    {

        private readonly ILogger<VillaApiController> _log;
        private readonly IVillaRepository _dbvilla;
        private readonly IMapper _mappper;


        public VillaApiController(IMapper mapper, ILogger<VillaApiController> log, IVillaRepository db)
        {
            _mappper = mapper;
            _log = log;
            _dbvilla = db;
        }

        [HttpGet]
        public async Task<ActionResult<VillaDTO>> GetAll()
        {

            _log.LogInformation($"Hit GetAll() In {this.GetType().Name}");

            var villas = await _dbvilla.GetAllAsync();

            return Ok(_mappper.Map<IEnumerable<VillaDTO>>(villas));

        }

        [HttpGet("{id:int}", Name = "GETVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CreateVillaRequest?>> GetVillaById(int id)
        {
            _log.LogInformation($"Hit GetVillaById() In {this.GetType().Name}");

            if (id <= 0)
            {
                _log.LogError($"id: {id}, Is Not Valid");
                return BadRequest();
            }

            Villa? villa = await _dbvilla.Get(v => v.Id == id);

            if (villa == null)
            {
                _log.LogWarning($"id: {id}, Is Not Found");
                return NotFound();
            }

            return Ok(_mappper.Map<VillaDTO>(villa));
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CreateVillaRequest?>> CreateVilla(CreateVillaRequest villaReq)
        {

            //if (!ModelState.IsValid)
            //    return BadRequest();

            Villa? villa = await _dbvilla.Get(v => v.Name == villaReq.Name);

            if (villa != null)
            {
                ModelState.AddModelError("", "Dublicated Name");
                return BadRequest(ModelState);
            }



            var newVilla = _mappper.Map<Villa>(villaReq);


            newVilla.CreatedAt = DateTime.Now;
            newVilla.UpdatedAt = DateTime.Now;
            await _dbvilla.CreateAsync(newVilla);



            return CreatedAtRoute("GETVilla", new { Id = newVilla.Id }, newVilla);


        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest();


            bool Deleted = await _dbvilla.Delete(v => v.Id == id);


            return Deleted ? NoContent() : NotFound();
        }

        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateVilla(VillaDTO villaReq)
        {
            if (villaReq == null || villaReq.Id <= 0)
                return BadRequest(villaReq);

            Villa? villa = await _dbvilla.Get(v => v.Id == villaReq.Id);

            if (villa == null)
                return NotFound();


            var villaUpdated = _mappper.Map<Villa>(villaReq);

            Villa? villaToUpdated = await _dbvilla.UpdateAsync(villaUpdated);

            return villaToUpdated != null ? NoContent() : NotFound();

        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDocument)
        {
            if (patchDocument == null)
                return BadRequest(patchDocument);


            Villa? villa = await _dbvilla.Get(v => v.Id == id);

            if (villa == null)
                return NotFound();


            var villaPartialUpdated = _mappper.Map<VillaDTO>(villa);

            patchDocument.ApplyTo(villaPartialUpdated, ModelState);

            var villaUpdated = _mappper.Map<Villa>(villaPartialUpdated);

            await _dbvilla.UpdateAsync(villaUpdated);


            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            return NoContent();
        }




    }
}
