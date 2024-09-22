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

        [HttpGet]
        public List<VillaDTO> GetAll()
        {
            return VillaDATA.SomeData();

        }

        [HttpGet("{id:int}", Name = "GETVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO?> GetVillaById(int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            VillaDTO? villa = VillaDATA.SomeData().FirstOrDefault(v => v.Id == id);

            if (villa == null)
            {
                return NotFound();
            }


            return Ok(villa);

        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO?> CreateVilla(VillaDTO villa)
        {

            //if (!ModelState.IsValid)
            //    return BadRequest();

            if (VillaDATA.SomeData().FirstOrDefault(v => v.Name == villa.Name) != null)
            {
                ModelState.AddModelError("", "Dublicated Name");
                return BadRequest(ModelState);
            }


            if (villa.Id != 0)
            {
                return BadRequest();
            }


            var v = VillaDATA.CreateVillaDTO(villa);

            return CreatedAtRoute("GETVilla", new { Id = v.Id }, v);


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

            VillaDTO? villa = VillaDATA.Villas.FirstOrDefault(v => v.Id == id);

            if (villa == null)
            {
                return NotFound();
            }

            VillaDATA.Villas.Remove(villa);

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


            VillaDTO villa = VillaDATA.Villas.FirstOrDefault(v => v.Id == villaReq.Id);

            if (villa == null)
            {
                return NotFound();
            }


            villa.Name = villaReq.Name;


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


            VillaDTO? villa = VillaDATA.Villas.FirstOrDefault(v => v.Id == id);

            if (villa == null)
            {
                return NotFound();
            }


            patchDocument.ApplyTo(villa, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return NoContent();

        }




    }
}
