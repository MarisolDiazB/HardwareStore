using AspNetCoreHero.ToastNotification.Abstractions; // importa la interfaz para el servicio de notificaciones.
<<<<<<< HEAD
using HardwareStore.Core.Pagination; 
using HardwareStore.Data.Entities; 
using HardwareStore.Services; 
using Microsoft.AspNetCore.Mvc;
using PrivateBlog.Web.DTOs; // importa el espacio de nombres de ASP.NET Core para MVC.

namespace HardwareStore.Controllers 
=======
using HardwareStore.Data.Entities;
using HardwareStore.Services;
using Microsoft.AspNetCore.Mvc;
using HardwareStore.Core.Pagination;
using HardwareStore.Helpers;
using HardwareStore.Core;
namespace HardwareStore.Controllers
>>>>>>> 7bd0cc833889ba4d31d2be7feec5cb64a03a81f9
{
    public class CustomerController : Controller
    {
<<<<<<< HEAD
        private readonly ICustomerService _services; 
        private readonly INotyfService _notify; 
        private const int PageSize = 5; 
=======
        private readonly ICustomerService _services;
        private readonly ICombosHelper _combosHelper;
        private readonly INotyfService _notify;
>>>>>>> 7bd0cc833889ba4d31d2be7feec5cb64a03a81f9

        // Constructor de la clase CustomerController.
        public CustomerController(ICustomerService customerService, INotyfService notify, ICombosHelper combosHelper)
        {
            _services = customerService;
            _notify = notify;
            _combosHelper = combosHelper;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int? RecordsPerPage,
                                       [FromQuery] int? Page,
                                       [FromQuery] string? Filter)
        {
            try
            {
                PaginationRequest paginationRequest = new PaginationRequest
                {
                    RecordsPerPage = RecordsPerPage ?? 15,
                    Page = Page ?? 1,
                    Filter = Filter,
                };
                Response<PaginationResponse<Customer>> response = await _services.GetListAsync(paginationRequest);

                if (response != null && response.IsSuccess && response.Result != null)
                {
                    return View(response.Result);
                }
                else
                {
                    _notify.Error("Ocurrió un error al obtener la lista de Clientes.");
                    return View(new PaginationResponse<Customer>());
                }
            }
            catch (Exception ex)
            {
                _notify.Error("Ocurrió un error al obtener la lista de Clientes: " + ex.Message);
                return View(new PaginationResponse<Customer>());
            }
        }

<<<<<<< HEAD
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _services.LoginAsync(dto);
                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }

                    return RedirectToAction("Dashboard", "Home");
                }

                ModelState.AddModelError(string.Empty, "Email o contraseña incorrectos");
                _notify.Error("Email o contraseña incorrectos");
            }

            return View(dto);
        }


        // para mostrar el formulario de creación de cliente.
=======
        // Para mostrar el formulario de creación de cliente.
>>>>>>> 7bd0cc833889ba4d31d2be7feec5cb64a03a81f9
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.EmployeeList = await _combosHelper.GetComboEmployees();
            return View();
        }

        // Para procesar la creación de un cliente.
        [HttpPost]
        public async Task<IActionResult> Create(Customer model)
        {
            try
            {
                // Valida el modelo.
                if (!ModelState.IsValid)
                {
                    _notify.Error("Debe ajustar los errores de validación.");
                    ViewBag.EmployeeList = await _combosHelper.GetComboEmployees(); // Recargar lista en caso de error
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
                ViewBag.EmployeeList = await _combosHelper.GetComboEmployees(); // Recargar lista en caso de error
                return View(model);
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                ViewBag.EmployeeList = await _combosHelper.GetComboEmployees(); // Recargar lista en caso de error
                return View(model);
            }
        }

        // Para mostrar el formulario de edición de cliente.
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _services.GetOneAsync(id);

            if (response.IsSuccess)
            {
                var customer = response.Result;
                ViewBag.EmployeeList = await _combosHelper.GetComboEmployees();
                return View(customer);
            }
            else
            {
                _notify.Error(response.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        // Para procesar la edición de un cliente.
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Customer model)
        {
            try
            {
                // Valida el modelo.
                if (!ModelState.IsValid)
                {
                    _notify.Error("Debe ajustar los errores de validación.");
                    ViewBag.EmployeeList = await _combosHelper.GetComboEmployees(); // Recargar lista en caso de error
                    return View(model);
                }

                model.Id = id;

                // Edita el cliente de forma asincrónica.
                var response = await _services.EditAsync(model);

                if (response.IsSuccess)
                {
                    _notify.Success(response.Message);
                    return RedirectToAction(nameof(Index));
                }

                _notify.Error(response.Message);
                ViewBag.EmployeeList = await _combosHelper.GetComboEmployees(); // Recargar lista en caso de error
                return View(model);
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                ViewBag.EmployeeList = await _combosHelper.GetComboEmployees(); // Recargar lista en caso de error
                return View(model);
            }
        }

        // Para eliminar un cliente.
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // Elimina el cliente de forma asincrónica.
                var response = await _services.DeleteAsync(id);

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
}
