using AspNetCoreHero.ToastNotification.Abstractions; 
using HardwareStore.Core; 
using HardwareStore.Core.Pagination; 
using HardwareStore.Data.Entities; 
using HardwareStore.Services; 
using Microsoft.AspNetCore.Mvc; 
using System; 
using System.Linq;
using System.Threading.Tasks; 

namespace HardwareStore.Controllers 
{
    public class ProductsController : Controller 
    {
        private readonly IProductServices _services; 
        private readonly INotyfService _notify; 

        // Constructor de la clase ProductsController.
        public ProductsController(IProductServices productServices, INotyfService notify)
        {
            _services = productServices; 
            _notify = notify; 
        }

        // para mostrar la lista de productos.
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var paginationRequest = new PaginationRequest(); // una solicitud de paginación vacía.
            var response = await _services.GetListAsync(paginationRequest); // la lista de productos de forma asincrónica.
            var product = response.Result.List; 
            return View(product); 
        }

        //  para mostrar el formulario de creación de producto.
        [HttpGet]
        public IActionResult Create()
        {
            return View(); 
        }

        //  para procesar el formulario de creación de producto.
        [HttpPost]
        public async Task<IActionResult> Create(Products model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notify.Error("Debe ajustar los errores de validación.");
                    return View(model);
                }

                Response<Products> response = await _services.CreateAsync(model);

                if (response.IsSuccess)
                {
                    _notify.Success(response.Message);
                    return RedirectToAction(nameof(Index));
                }

                _notify.Error(response.Message);
                return View(model);
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return View(model);
            }
        }

        // para mostrar el formulario de edición de producto.
        [HttpGet("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            Response<Products> response = await _services.GetOneAsync(id);

            if (response.IsSuccess)
            {
                return View(response.Result);
            }

            _notify.Error(response.Errors.First());
            return RedirectToAction(nameof(Index));
        }

        //  para procesar el formulario de edición de producto.
        [HttpPost]
        public async Task<IActionResult> Update(Products model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notify.Error("Debe ajustar los errores de validación.");
                    return View(model);
                }

                Response<Products> response = await _services.EditAsync(model);

                if (response.IsSuccess)
                {
                    _notify.Success(response.Message);
                    return RedirectToAction(nameof(Index));
                }

                _notify.Error(response.Errors.First());
                return View(model);
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return View(model);
            }
        }

        //  para eliminar un producto.
        [HttpPost("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            Response<Products> response = await _services.DeleteAsync(id);

            if (response.IsSuccess)
            {
                _notify.Success(response.Message);
                return RedirectToAction(nameof(Index));
            }

            _notify.Error(response.Errors.First());
            return RedirectToAction(nameof(Index));
        }
    }
}
