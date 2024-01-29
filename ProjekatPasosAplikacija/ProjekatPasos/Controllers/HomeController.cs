using Microsoft.AspNetCore.Mvc;
using ProjekatPasos.Models;
using System.Diagnostics;

namespace ProjekatPasos.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Pocetna()
        {
            return View();
        }

        public IActionResult OServisu() 
        {
            return View();
        }

        public IActionResult PrijavaIliRegistracija()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}