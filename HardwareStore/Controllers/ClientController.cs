using AspNetCoreHero.ToastNotification.Abstractions;
using HardwareStore.Core;
using HardwareStore.Data;
using HardwareStore.Data.Entities;
using HardwareStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace HardwareStore.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientService _services;
        private readonly INotyfService _notify;

        public ClientController(IClientService productServices, INotyfService notify)
        {
            _services = productServices;
            _notify = notify;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Response<List<Clients>> list = await _services.GetListAsync();

            if (!list.IsSuccess)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(list.Result);
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
                Response<Clients> response = await _services.CreateAsync(model);

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

