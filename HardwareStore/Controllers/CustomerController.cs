using AspNetCoreHero.ToastNotification.Abstractions;
using HardwareStore.Data.Entities;
using HardwareStore.Services;
using Microsoft.AspNetCore.Mvc;
using HardwareStore.Core.Pagination;
using HardwareStore.Helpers;
using HardwareStore.Core;
using HardwareStore.Core.Attributes;
namespace HardwareStore.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _services;
        private readonly ICombosHelper _combosHelper;
        private readonly INotyfService _notify;

        // Constructor de la clase CustomerController.
        public CustomerController(ICustomerService customerService, INotyfService notify, ICombosHelper combosHelper)
        {
            _services = customerService;
            _notify = notify;
            _combosHelper = combosHelper;
        }

        [HttpGet]
        [CustomAuthorize(permission: "showCustomer", module: "Customer")]
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

        // Para mostrar el formulario de creación de cliente.
        [HttpGet]
        [CustomAuthorize(permission: "createCustomer", module: "Customer")]
        public async Task<IActionResult> Create()
        {
            ViewBag.EmployeeList = await _combosHelper.GetComboEmployees();
            return View();
        }

        // Para procesar la creación de un cliente.
        [HttpPost]
        [CustomAuthorize(permission: "showCustomer", module: "Customer")]
        public async Task<IActionResult> Create(Customer model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notify.Error("Debe ajustar los errores de validación.");
                    ViewBag.EmployeeList = await _combosHelper.GetComboEmployees(); // Recargar lista en caso de error
                    return View(model);
                }

                var response = await _services.CreateAsync(model);

                if (response.IsSuccess)
                {
                    _notify.Success(response.Message);
                    return RedirectToAction(nameof(Index));
                }

                _notify.Error(response.Message);
                ViewBag.EmployeeList = await _combosHelper.GetComboEmployees();
                return View(model);
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                ViewBag.EmployeeList = await _combosHelper.GetComboEmployees(); 
                return View(model);
            }
        }

        // Para mostrar el formulario de edición de cliente.
        [HttpGet]
        [CustomAuthorize(permission: "updateCustomer", module: "Customer")]
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
        [CustomAuthorize(permission: "updateCustomer", module: "Customer")]
        public async Task<IActionResult> Edit(int id, Customer model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notify.Error("Debe ajustar los errores de validación.");
                    ViewBag.EmployeeList = await _combosHelper.GetComboEmployees(); 
                    return View(model);
                }

                model.Id = id;

                var response = await _services.EditAsync(model);

                if (response.IsSuccess)
                {
                    _notify.Success(response.Message);
                    return RedirectToAction(nameof(Index));
                }

                _notify.Error(response.Message);
                ViewBag.EmployeeList = await _combosHelper.GetComboEmployees(); 
                return View(model);
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                ViewBag.EmployeeList = await _combosHelper.GetComboEmployees();
                return View(model);
            }
        }

        // Para eliminar un cliente.
        [HttpPost]
        [CustomAuthorize(permission: "deleteCustomer", module: "Customer")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
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