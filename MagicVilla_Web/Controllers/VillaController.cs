using MagicVilla_Web.Models;
using MagicVilla_Web.Models.DTO;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MagicVilla_Web.Controllers
{

    [Route("villa")]
    public class VillaController : Controller
    {

        private readonly IVillaService _villa;

        public VillaController(IVillaService villa)
        {
            _villa = villa;
        }

        [HttpGet]
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




        [HttpGet("Create")]
        public async Task<IActionResult> Create()
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
            return View(createVillaRequest);
        }
    }
}
