using AutoMapper;
using MagicVilla_VillaApi.Model;
using MagicVilla_VillaApi.Model.DTO;
using MagicVilla_VillaApi.Repository.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MagicVilla_VillaApi.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaApiController : ControllerBase
    {

        private readonly ILogger<VillaApiController> _log;
        private readonly IVillaRepository _dbvilla;
        private readonly IMapper _mappper;
        private readonly APIResponse _apiResponse;

        public VillaApiController(IMapper mapper, ILogger<VillaApiController> log, IVillaRepository db)
        {
            _mappper = mapper;
            _log = log;
            _dbvilla = db;
            _apiResponse = new APIResponse();
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetAll()
        {
            try
            {
                _log.LogInformation($"Hit GetAll() In {this.GetType().Name}");

                var villas = await _dbvilla.GetAllAsync();

                _apiResponse.StatusCode = HttpStatusCode.OK;
                _apiResponse.IsSuccess = true;
                _apiResponse.Result = _mappper.Map<IEnumerable<VillaDTO>>(villas);

                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _apiResponse;

        }

        [HttpGet("{id:int}", Name = "GETVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse?>> GetVillaById(int id)
        {

            try
            {
                _log.LogInformation($"Hit GetVillaById() In {this.GetType().Name}");

                if (id <= 0)
                {

                    _log.LogError($"id: {id}, Is Not Valid");
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiResponse.ErrorMessages = new List<string>() { $"id: {id}, Is Not Valid" };
                    return BadRequest(_apiResponse);
                }

                Villa? villa = await _dbvilla.Get(v => v.Id == id);

                if (villa == null)
                {
                    _log.LogWarning($"id: {id}, Is Not Found");
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    _apiResponse.ErrorMessages = new List<string>() { $"id: {id}, Is Not Found" };
                    return NotFound(_apiResponse);
                }

                _apiResponse.StatusCode = HttpStatusCode.OK;

                _apiResponse.Result = _mappper.Map<VillaDTO>(villa);
                _apiResponse.IsSuccess = true;
                return Ok(_apiResponse);

            }
            catch (Exception ex)
            {
                _apiResponse.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _apiResponse;
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse?>> CreateVilla(CreateVillaRequest villaReq)
        {

            //if (!ModelState.IsValid)
            //    return BadRequest();
            try
            {
                Villa? villa = await _dbvilla.Get(v => v.Name == villaReq.Name);

                if (villa != null)
                {
                    ModelState.AddModelError("", "Dublicated Name");

                    _apiResponse.ErrorMessages = new List<string>() { "Dublicated Name" };
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;

                    return BadRequest(_apiResponse);
                }



                var newVilla = _mappper.Map<Villa>(villaReq);


                newVilla.CreatedAt = DateTime.Now;
                newVilla.UpdatedAt = DateTime.Now;

                _apiResponse.Result = await _dbvilla.CreateAsync(newVilla);
                _apiResponse.StatusCode = HttpStatusCode.OK;
                _apiResponse.IsSuccess = true;

                return CreatedAtRoute("GETVilla", new { Id = newVilla.Id }, _apiResponse);
            }
            catch (Exception ex)
            {

                _apiResponse.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _apiResponse;

        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIResponse>> Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _apiResponse.ErrorMessages = new List<string>() { "Not Valid Id" };
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest();
                }

                bool Deleted = await _dbvilla.Delete(v => v.Id == id);

                if (Deleted)
                {
                    _apiResponse.IsSuccess = true;
                    _apiResponse.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    _apiResponse.IsSuccess = false;
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    _apiResponse.ErrorMessages = new List<string>() { "ID Not Found" };
                }

                return Deleted ? Ok(_apiResponse) : NotFound(_apiResponse);
            }
            catch (Exception ex)
            {

                _apiResponse.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _apiResponse;

        }

        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> UpdateVilla(VillaDTO villaReq)
        {
            try
            {
                if (villaReq == null || villaReq.Id <= 0)
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiResponse.ErrorMessages = new List<string> { "invalid request" };
                    return BadRequest(_apiResponse);
                }

                Villa? villa = await _dbvilla.Get(v => v.Id == villaReq.Id);

                if (villa == null)
                {
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    _apiResponse.ErrorMessages = new List<string> { "Not Found " };
                    return NotFound(_apiResponse);
                }

                var villaUpdated = _mappper.Map<Villa>(villaReq);

                Villa? villaToUpdated = await _dbvilla.UpdateAsync(villaUpdated);
                _apiResponse.IsSuccess = true;
                _apiResponse.Result = villaReq;
                _apiResponse.StatusCode = HttpStatusCode.NoContent;
                //return villaToUpdated != null ? Ok(_apiResponse) : NotFound(_apiResponse);
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _apiResponse;
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIResponse>> UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDocument)
        {
            try
            {
                if (patchDocument == null)
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiResponse.ErrorMessages = new List<string>() { "Can't Accept null obj" };
                    return BadRequest(_apiResponse);
                }


                Villa? villa = await _dbvilla.Get(v => v.Id == id);

                if (villa == null)
                {

                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    _apiResponse.ErrorMessages = new List<string>() { "Vill Not Found" };
                    return NotFound(_apiResponse);
                }


                var villaPartialUpdated = _mappper.Map<VillaDTO>(villa);

                patchDocument.ApplyTo(villaPartialUpdated, ModelState);

                var villaUpdated = _mappper.Map<Villa>(villaPartialUpdated);

                await _dbvilla.UpdateAsync(villaUpdated);


                if (!ModelState.IsValid)
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(ModelState);
                }
                _apiResponse.Result = villaUpdated;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                _apiResponse.IsSuccess = true;
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _apiResponse;

        }




    }
}
