/*using AspNetCoreHero.ToastNotification.Abstractions;
using HardwareStore.Core;
using HardwareStore.Data;
using HardwareStore.Data.Entities;
using HardwareStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace HardwareStore.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeServices _services;
        private readonly INotyfService _notify;

        public EmployeeController(IEmployeeServices employeeServices, INotyfService notify)
        {
            _services = employeeServices;
            _notify = notify;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Response<List<Employee>> response = await _services.GetListEmployeeAsync();
            return View(response.Result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

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
                Response<Employee> response = await _services.CreateEmployeeAsync(model);

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
        
    }
}
*/
using AspNetCoreHero.ToastNotification.Abstractions;
using HardwareStore.Core;
using HardwareStore.Data.Entities;
using HardwareStore.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HardwareStore.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeServices _services;
        private readonly INotyfService _notify;

        public EmployeeController(IEmployeeServices employeeServices, INotyfService notify)
        {
            _services = employeeServices;
            _notify = notify;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _services.GetListEmployeeAsync();
            return View(response.Result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

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
