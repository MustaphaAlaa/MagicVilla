using MagicVilla_Web.Models;
using MagicVilla_Web.Models.DTO;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MagicVilla_Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IVillaService _villa;


    public HomeController(ILogger<HomeController> logger, IVillaService villa)
    {
        _villa = villa;
        _logger = logger;
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
}
