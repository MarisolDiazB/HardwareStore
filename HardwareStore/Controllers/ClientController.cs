using AspNetCoreHero.ToastNotification.Abstractions;
using HardwareStore.Core.Pagination;
using HardwareStore.Data.Entities;
using HardwareStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace HardwareStore.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientService _services;
        private readonly INotyfService _notify;

        public ClientController(IClientService clientService, INotyfService notify)
        {
            _services = clientService;
            _notify = notify;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var paginationRequest = new PaginationRequest();
            var response = await _services.GetListAsync(paginationRequest);
            var clients = response.Result.List; // Extraer la lista de empleados del objeto PaginationResponse
            return View(clients);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Clients model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notify.Error("Debe ajustar los errores de validación.");
                    return View(model);
                }

                var response = await _services.CreateAsync(model);

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
            var response = await _services.GetOneAsync(id);

            if (response.IsSuccess)
            {
                var client = response.Result;
                return View(client);
            }
            else
            {
                _notify.Error(response.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Clients model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notify.Error("Debe ajustar los errores de validación.");
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

