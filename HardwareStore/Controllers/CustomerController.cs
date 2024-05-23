using AspNetCoreHero.ToastNotification.Abstractions; // importa la interfaz para el servicio de notificaciones.
<<<<<<< HEAD
using HardwareStore.Core.Pagination;
using HardwareStore.Data.Entities;
using HardwareStore.Services;
=======
using HardwareStore.Core.Pagination; 
using HardwareStore.Data.Entities; 
using HardwareStore.Services; 
>>>>>>> 50c25fa775e1463912e04e61b211578f7f4efd8f
using Microsoft.AspNetCore.Mvc; // importa el espacio de nombres de ASP.NET Core para MVC.

namespace HardwareStore.Controllers 
{
    public class CustomerController : Controller 
    {
<<<<<<< HEAD
        private readonly ICustomerService _services;
        private readonly INotyfService _notify;
        private const int PageSize = 10;
=======
        private readonly ICustomerService _services; 
        private readonly INotyfService _notify; 
        private const int PageSize = 10; 
>>>>>>> 50c25fa775e1463912e04e61b211578f7f4efd8f

        // constructor de la clase CustomerController.
        public CustomerController(ICustomerService customerService, INotyfService notify)
        {
            _services = customerService; // Inicializa
<<<<<<< HEAD
            _notify = notify;
=======
            _notify = notify; 
>>>>>>> 50c25fa775e1463912e04e61b211578f7f4efd8f
        }

        // mostrar la lista de clientes.
        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            //  solicitud de paginación.
            var paginationRequest = new PaginationRequest
            {
                PageNumber = pageNumber,
                RecordsPerPage = PageSize
            };

            // obtiene la lista de clientes de forma asincrónica.
            var response = await _services.GetListAsync(paginationRequest);
            var customers = response.Result;
            var totalItems = response.Result.Count;

            // el ViewModel para la paginación.
            var viewModel = new PaginacionViewModel
            {
                Customers = customers,
                PaginaActual = pageNumber,
                TotalPaginas = (int)Math.Ceiling(totalItems / (double)PageSize)
            };

<<<<<<< HEAD

=======
  
>>>>>>> 50c25fa775e1463912e04e61b211578f7f4efd8f
            return View(viewModel);
        }

        // para mostrar el formulario de creación de cliente.
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // para procesar 
        [HttpPost]
        public async Task<IActionResult> Create(Customer model)
        {
            try
            {
                // Valida el modelo.
                if (!ModelState.IsValid)
                {
                    _notify.Error("Debe ajustar los errores de validación.");
                    return View(model);
                }

                // Crea el cliente de forma asincrónica.
                var response = await _services.CreateAsync(model);

                // Maneja la respuesta del servicio.
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

        //  para mostrar el formulario de edición de cliente.
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _services.GetOneAsync(id);

            if (response.IsSuccess)
            {
                var customer = response.Result;
                return View(customer);
            }
            else
            {
                _notify.Error(response.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        // para procesar 
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Customer model)
        {
            try
            {
                // valida el modelo.
                if (!ModelState.IsValid)
                {
                    _notify.Error("Debe ajustar los errores de validación.");
                    return View(model);
                }

                model.Id = id;

                // edita el cliente de forma asincrónica.
                var response = await _services.EditAsync(model);

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

        // para eliminar un cliente.
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // elimina el cliente de forma asincrónica.
                var response = await _services.DeleteAsync(id);

<<<<<<< HEAD

=======
                
>>>>>>> 50c25fa775e1463912e04e61b211578f7f4efd8f
                if (response.IsSuccess)
                {
                    _notify.Success(response.Message);
                }
                else
                {
                    _notify.Error(response.Message);
                }
            }
            catch (Exception ex)
            {
                _notify.Error("Se produjo un error al intentar eliminar el cliente: " + ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> 50c25fa775e1463912e04e61b211578f7f4efd8f
