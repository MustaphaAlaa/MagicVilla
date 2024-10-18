using AutoMapper;
using MagicVilla_VillaApi.Model.DTO;
using MagicVilla_VillaApi.Model;
using MagicVilla_VillaApi.Repository.interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla_VillaApi.Controllers
{

    [ApiController]
    [Route("api/VillaNumber")]
    public class VillaNumberController : ControllerBase
    {
        private readonly ILogger<VillaNumberController> _log;
        private readonly IVillaNumberRepository _dbVillaNo;
        private readonly IVillaRepository _dbVilla;
        private readonly IMapper _mappper;
        private readonly APIResponse _apiResponse;

        public VillaNumberController(IVillaRepository dbVilla, IMapper mapper, ILogger<VillaNumberController> log, IVillaNumberRepository db)
        {
            _dbVilla = dbVilla;
            _mappper = mapper;
            _log = log;
            _dbVillaNo = db;
            _apiResponse = new APIResponse();
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetAllVillasNo()
        {
            try
            {
                _log.LogInformation($"Hit GetAll() In {this.GetType().Name}");

                var villas = await _dbVillaNo.GetAllAsync();

                _apiResponse.StatusCode = HttpStatusCode.OK;
                _apiResponse.IsSuccess = true;
                _apiResponse.Result = _mappper.Map<IEnumerable<VillaNumberDTO>>(villas);

                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _apiResponse;

        }

        [HttpGet("{id:int}", Name = "GETVillaNo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse?>> GetVillaNo(int id)
        {

            try
            {
                _log.LogInformation($"Hit GetVillaNo() In {this.GetType().Name}");

                if (id <= 0)
                {

                    _log.LogError($"id: {id}, Is Not Valid");
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiResponse.ErrorMessages = new List<string>() { $"id: {id}, Is Not Valid" };
                    return BadRequest(_apiResponse);
                }

                VillaNumber? villaNo = await _dbVillaNo.Get(v => v.VillaNo == id);

                if (villaNo == null)
                {
                    _log.LogWarning($"id: {id}, Is Not Found");
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    _apiResponse.ErrorMessages = new List<string>() { $"id: {id}, Is Not Found" };
                    return NotFound(_apiResponse);
                }

                _apiResponse.StatusCode = HttpStatusCode.OK;
                _apiResponse.IsSuccess = true;
                _apiResponse.Result = _mappper.Map<VillaNumberDTO>(villaNo);
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
        public async Task<ActionResult<APIResponse?>> CreateVillaNo(CreateVillaNumberRequest villaNoReq)
        {
            try
            {
                VillaNumber? villaNo = await _dbVillaNo.Get(v => v.VillaNo == villaNoReq.VillaNo);

                if (villaNo != null)
                {
                    ModelState.AddModelError("", "Dublicated Villa Number");

                    _apiResponse.ErrorMessages = new List<string>() { "Dublicated Villa Number" };
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;

                    return BadRequest(_apiResponse);
                }

                Villa? villa = await _dbVilla.Get(v => v.Id == villaNoReq.VillaId);

                if (villa == null)
                {
                    ModelState.AddModelError("", "Villa With Id Dosen't");

                    _apiResponse.ErrorMessages = new List<string>() { "  Villa  Dosen't Exist" };
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;

                    return NotFound(_apiResponse);
                }




                var NewVillaNo = _mappper.Map<VillaNumber>(villaNoReq);


                NewVillaNo.CreatedAt = DateTime.Now;
                NewVillaNo.UpdatedAt = DateTime.Now;

                _apiResponse.Result = await _dbVillaNo.CreateAsync(NewVillaNo);
                _apiResponse.StatusCode = HttpStatusCode.OK;
                _apiResponse.IsSuccess = true;

                return CreatedAtRoute("GETVillaNo", new { Id = NewVillaNo.VillaNo }, _apiResponse);
            }
            catch (Exception ex)
            {

                _apiResponse.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _apiResponse;

        }

        [HttpDelete("{id:int}", Name = "DeleteVillaNo")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIResponse>> DeleteVillaNo(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _apiResponse.ErrorMessages = new List<string>() { "Not Valid Id" };
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest();
                }

                bool Deleted = await _dbVillaNo.Delete(v => v.VillaNo == id);

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

        [HttpPut("{id:int}", Name = "UpdateVillaNo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> UpdateVillaNo(UpdateVillaNumber villaReq, int id)
        {
            try
            {
                if (villaReq == null || villaReq.VillaNo <= 0)
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiResponse.ErrorMessages = new List<string> { "invalid request" };
                    return BadRequest(_apiResponse);
                }

                VillaNumber? VillaNo = await _dbVillaNo.Get(v => v.VillaNo == id);

                if (VillaNo == null)
                {
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    _apiResponse.ErrorMessages = new List<string> { "Not Found " };
                    return NotFound(_apiResponse);
                }


                Villa? villa = await _dbVilla.Get(v => v.Id == villaReq.VillaId);

                if (villa == null)
                {
                    ModelState.AddModelError("", "Villa With Id Dosen't");

                    _apiResponse.ErrorMessages = new List<string>() { "  Villa  Dosen't Exist" };
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    _apiResponse.IsSuccess = true;
                    return NotFound(_apiResponse);
                }


                var villaUpdated = _mappper.Map<VillaNumber>(villaReq);

                VillaNumber? villaToUpdated = await _dbVillaNo.UpdateAsync(villaUpdated, id);
                _apiResponse.IsSuccess = true;
                _apiResponse.Result = villaReq;
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _apiResponse;
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVillaNo")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIResponse>> UpdatePartialVillaNo(int id, JsonPatchDocument<VillaNumberDTO> patchDocument)
        {
            try
            {
                if (patchDocument == null)
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiResponse.ErrorMessages = new List<string>() { "Can't Accept null obj" };
                    return BadRequest(_apiResponse);
                }


                VillaNumber? VillaNo = await _dbVillaNo.Get(v => v.VillaNo == id);

                if (VillaNo == null)
                {

                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    _apiResponse.ErrorMessages = new List<string>() { "Vill Not Found" };
                    return NotFound(_apiResponse);
                }




                var villaPartialUpdated = _mappper.Map<VillaNumberDTO>(VillaNo);

                patchDocument.ApplyTo(villaPartialUpdated, ModelState);


                Villa? villa = await _dbVilla.Get(v => v.Id == villaPartialUpdated.VillaId);

                if (villa == null)
                {
                    ModelState.AddModelError("", "Villa With Id Dosen't");

                    _apiResponse.ErrorMessages = new List<string>() { "Villa With Id Dosen't" };
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;

                    return NotFound(_apiResponse);
                }


                var villaUpdated = _mappper.Map<VillaNumber>(villaPartialUpdated);

                await _dbVillaNo.UpdateAsync(villaUpdated, id);


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
