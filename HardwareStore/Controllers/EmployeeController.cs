using AspNetCoreHero.ToastNotification.Abstractions; // importa la interfaz para el servicio de notificaciones.
using HardwareStore.Data.Entities;
using HardwareStore.Services;
using Microsoft.AspNetCore.Mvc;
using HardwareStore.Core.Pagination;
using HardwareStore.Helpers;
using HardwareStore.Core;

namespace HardwareStore.Controllers
{
    public class EmployeeController : Controller 
    {
        private readonly IEmployeeServices _services;
        private readonly ICombosHelper _combosHelper;
        private readonly INotyfService _notify;

        // Constructor de la clase EmployeeController.
        public EmployeeController(IEmployeeServices employeeServices, INotyfService notify, ICombosHelper combosHelper)
        {
            _services = employeeServices; 
            _notify = notify; 
            _combosHelper = combosHelper;
        }

        //para mostrar la lista de empleados.
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
                Response<PaginationResponse<Employee>> response = await _services.GetListAsync(paginationRequest);

                if (response != null && response.IsSuccess && response.Result != null)
                {
                    return View(response.Result);
                }
                else
                {
                    _notify.Error("Ocurrió un error al obtener la lista de Empleados.");
                    return View(new PaginationResponse<Employee>());
                }
            }
            catch (Exception ex)
            {
                _notify.Error("Ocurrió un error al obtener la lista de Empleados: " + ex.Message);
                return View(new PaginationResponse<Employee>());
            }
        }

        //para mostrar el formulario de creación de empleado.
        [HttpGet]
        public IActionResult Create()
        {
            return View(); // Devuelve la vista para crear un nuevo empleado.
        }

        // para procesar el formulario de creación de empleado.
        [HttpPost]
        public async Task<IActionResult> Create(Employee model)
        {
            try
            {
                
                if (!ModelState.IsValid)
                {
                    _notify.Error("Debe ajustar los errores de validación.");
                    return View(model);
                }

              
                var response = await _services.CreateEmployeeAsync(model);

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

        //para mostrar el formulario de edición de empleado.
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _services.GetOneEmployeeAsync(id);

            if (!response.IsSuccess || response.Result == null)
            {
                _notify.Error("Empleado no encontrado.");
                return RedirectToAction(nameof(Index));
            }

            return View(response.Result);
        }

        //para procesar el formulario de edición de empleado.
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Employee model)
        {
            try
            {
               
                if (!ModelState.IsValid)
                {
                    _notify.Error("Debe ajustar los errores de validación.");
                    return View(model);
                }

                // edita el empleado
                var response = await _services.EditEmployeeAsync(model);

       
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

        // para eliminar un empleado.
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _services.DeleteEmployeeAsync(id);

                if (response.IsSuccess)
                {
                    _notify.Success(response.Message);
                }
                else
                {
                    _notify.Error(response.Message);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notify.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
