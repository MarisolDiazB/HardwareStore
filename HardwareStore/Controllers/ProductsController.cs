using AspNetCoreHero.ToastNotification.Abstractions;
using HardwareStore.Core;
using HardwareStore.Core.Pagination;
using HardwareStore.Data.Entities;
using HardwareStore.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;


namespace HardwareStore.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductServices _services;
        private readonly INotyfService _notify;
        
        public ProductsController(IProductServices productServices, INotyfService notify)
        {
            _services = productServices;
            _notify = notify;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var paginationRequest = new PaginationRequest();
            var response = await _services.GetListAsync(paginationRequest);
            var product = response.Result.List; // Extraer la lista de empleados del objeto PaginationResponse
            return View(product);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Response<Products> response = await _services.GetOneAsync(id);

            if (response.IsSuccess)
            {
                return View(response.Result);
            }

            _notify.Error(response.Errors.First());
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Products model)
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

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
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



