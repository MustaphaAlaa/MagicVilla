using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MagicVilla_Web.Controllers
{
    public class VillaNumberController : Controller
    {

        private readonly IVillaService _villa;

        public VillaNumberController(IVillaService villa)
        {
            _villa = villa;
        }


        public async Task<IActionResult> Index()
        {
            var Villas = await _villa.GetAllAsync<ApiResponse>();

            List<VillaDTO?> VillasList = new();
            if (Villas != null && Villas.IsSuccess)
            {
                VillasList = JsonConvert.DeserializeObject<List<VillaDTO?>>(Convert.ToString(Villas.Result));
                return View(VillasList);
            }

            return View(VillasList);
        }


        public async Task<IActionResult> GetById(int id)
        {
            var villa = await _villa.GetAsync<ApiResponse>(id);

            if (villa != null && villa.IsSuccess)
            {
                VillaDTO villaDTO = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(villa.Result));
                return View("villa", villaDTO);
            }

            return RedirectToAction("Index");
        }




        public async Task<IActionResult> CreateVilla()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Create(CreateVillaRequest createVillaRequest)
        {
            if (ModelState.IsValid)
            {
                var response = await _villa.CreateAsync<ApiResponse>(createVillaRequest);

                if (response != null && response.IsSuccess)
                    return RedirectToAction("Index");
            }

            //TempData["Error"] = "error encounterd";
            return View("CreateVilla", createVillaRequest);
        }


        public async Task<IActionResult> UpdateVilla(int id)
        {

            var villaRes = await _villa.GetAsync<ApiResponse>(id);

            if (villaRes != null && villaRes.IsSuccess)
            {

                var villa = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(villaRes.Result.ToString()));

                return View("UpdateVilla", villa);
            }

            return RedirectToAction("Index");
        }



        [HttpPost]
        public async Task<IActionResult> Update(VillaDTO updateVillaRequest)
        {
            if (ModelState.IsValid)
            {
                var response = await _villa.UpdateAsync<ApiResponse>(updateVillaRequest);

                if (response != null && response.IsSuccess)
                    return RedirectToAction("Index");
            }

            //TempData["Error"] = "error encounterd";
            return View("UpdateVilla", updateVillaRequest);
        }

        public async Task<IActionResult> DeleteVilla(int id)
        {

            var villaRes = await _villa.GetAsync<ApiResponse>(id);

            if (villaRes != null && villaRes.IsSuccess)
            {
                var villa = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(villaRes.Result.ToString()));

                return View("DeleteVilla", villa);
            }

            return RedirectToAction("Index");
        }



        [HttpPost]
        public async Task<IActionResult> Delete(VillaDTO villa)
        {

            var response = await _villa.DeleteAsync<ApiResponse>(villa.Id);

            if (response != null && response.IsSuccess)
                return RedirectToAction("Index");

            return View("DeleteVilla", villa);
        }
    }
}
