using HardwareStore.Models; 
using Microsoft.AspNetCore.Mvc; 
using System.Diagnostics; 

namespace HardwareStore.Controllers 
{
    public class HomeController : Controller 
    {
        private readonly ILogger<HomeController> _logger; 

        // Constructor de la clase HomeController.
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger; 
        }

        // para mostrar la vista de inicio.
        public IActionResult Index()
        {
            return View(); // Devuelve la vista de inicio.
        }

        // para mostrar la vista de privacidad.
        public IActionResult Privacy()
        {
            return View();
        }

        // para manejar errores.
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // para mostrar información de error.
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
