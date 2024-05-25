using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HardwareStore.Models;
using System.Diagnostics;
namespace HardwareStore.Controllers 
{
    public class HomeController : Controller 
    {
        private readonly ILogger<HomeController> _logger; 

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger; 
        }

        // para mostrar la vista de inicio.
        public IActionResult Index()
        {
            return View(); 
        }

        // para mostrar la vista de privacidad.
        [AllowAnonymous]
        public IActionResult Privacy()
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
