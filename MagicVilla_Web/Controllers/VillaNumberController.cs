using AutoMapper;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.DTO;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MagicVilla_Web.Controllers
{
    public class VillaNumberController : Controller
    {

        private readonly IVillaNumberService _villaNum;
        private readonly IVillaService _villa;
        private readonly IMapper _mapper;

        public VillaNumberController(IVillaNumberService villa, IVillaService Ivilla, IMapper mapper)
        {
            _villa = Ivilla;
            _villaNum = villa;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index()
        {
            var VillaNums = await _villaNum.GetAllAsync<ApiResponse>();

            List<VillaNumberDTO?> VillasList = new();
            if (VillaNums != null && VillaNums.IsSuccess)
            {
                VillasList = JsonConvert.DeserializeObject<List<VillaNumberDTO?>>(Convert.ToString(VillaNums.Result));
                return View(VillasList);
            }

            return View(VillasList);
        }


        public async Task<IActionResult> GetById(int id)
        {
            var villa = await _villaNum.GetAsync<ApiResponse>(id);

            if (villa != null && villa.IsSuccess)
            {
                VillaNumberDTO villaNumDTO = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(villa.Result));

                return View("villaNumber", villaNumDTO);
            }

            return RedirectToAction("Index");
        }




        public async Task<IActionResult> CreateVillaNumber()
        {

            var Villas = await _villa.GetAllAsync<ApiResponse>();

            List<VillaDTO?> VillasList = new();

            if (Villas != null && Villas.IsSuccess)
            {
                VillasList = JsonConvert.DeserializeObject<List<VillaDTO?>>(Convert.ToString(Villas.Result));
                ViewBag.Villas = VillasList;

                return View();
            }


            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Create(CreateVillaNumberRequest createVillaNumberRequest)
        {
            if (ModelState.IsValid)
            {

                var response = await _villaNum.CreateAsync<ApiResponse>(createVillaNumberRequest);

                if (response != null && response.IsSuccess)
                    return RedirectToAction("Index");
            }

            //TempData["Error"] = "error encounterd";
            return View("CreateVillaNumber", createVillaNumberRequest);
        }


        public async Task<IActionResult> UpdateVillaNumber(int id)
        {

            var villaRes = await _villaNum.GetAsync<ApiResponse>(id);

            if (villaRes != null && villaRes.IsSuccess)
            {


                //Get All Villa

                var allVilla = await _villa.GetAllAsync<ApiResponse>();

                ViewBag.Villas = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(allVilla.Result.ToString()));


                var villa = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(villaRes.Result.ToString()));

                var updateReq = _mapper.Map<UpdateVillaNumber>(villa);
                return View("UpdateVillaNumber", updateReq);
            }

            return RedirectToAction("Index");
        }



        [HttpPost]
        public async Task<IActionResult> Update(UpdateVillaNumber updateVillaRequest, int OriginalVillaNO)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaNum.UpdateAsync<ApiResponse>(updateVillaRequest, OriginalVillaNO);

                if (response != null && response.IsSuccess)
                    return RedirectToAction("Index");
            }

            //TempData["Error"] = "error encounterd";
            return RedirectToAction("UpdateVillaNumber");
        }

        public async Task<IActionResult> DeleteVillaNumber(int id)
        {

            var villaRes = await _villaNum.GetAsync<ApiResponse>(id);

            if (villaRes != null && villaRes.IsSuccess)
            {
                var villa = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(villaRes.Result));

                return View("DeleteVillaNumber", villa);
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(VillaNumberDTO villa)
        {

            var response = await _villaNum.DeleteAsync<ApiResponse>(villa.VillaNo);

            if (response != null && response.IsSuccess)
                return RedirectToAction("Index");

            return View("DeleteVillaNumber", villa);
        }
    }
}
