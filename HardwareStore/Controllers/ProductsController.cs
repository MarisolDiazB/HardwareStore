﻿using AspNetCoreHero.ToastNotification.Abstractions; 
using HardwareStore.Core;
using HardwareStore.Core.Pagination; 
using HardwareStore.Data.Entities;
using HardwareStore.Core.Attributes;
using HardwareStore.Services; 
using Microsoft.AspNetCore.Mvc; 


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
        [CustomAuthorize(permission: "showProducts", module: "Products")]
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
                Response<PaginationResponse<Products>> response = await _services.GetListAsync(paginationRequest);

                if (response != null && response.IsSuccess && response.Result != null)
                {
                    return View(response.Result);
                }
                else
                {
                    _notify.Error("Ocurrió un error al obtener la lista de Productos.");
                    return View(new PaginationResponse<Products>());
                }
            }
            catch (Exception ex)
            {
                _notify.Error("Ocurrió un error al obtener la lista de Productos: " + ex.Message);
                return View(new PaginationResponse<Products>());
            }
        }

        //  para mostrar el formulario de creación de producto.
        [HttpGet]
        [CustomAuthorize(permission: "createProducts", module: "Products")]
        public IActionResult Create()
        {
            return View(); 
        }

        //  para procesar el formulario de creación de producto.
        [HttpPost]
        [CustomAuthorize(permission: "createProducts", module: "Products")]
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
        [HttpGet]
        [CustomAuthorize(permission: "updateProducts", module: "Products")]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _services.GetOneAsync(id);

            if (!response.IsSuccess || response.Result == null)
            {
                _notify.Error("Producto no encontrado.");
                return RedirectToAction(nameof(Index));
            }

            return View(response.Result);
        }

        //  para procesar el formulario de edición de producto.
        [HttpPost]
        [CustomAuthorize(permission: "updateProducts", module: "Products")]
        public async Task<IActionResult> Edit(int id, Products model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notify.Error("Debe ajustar los errores de validación.");
                    return View(model);
                }
                // edita el empleado
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

        //  para eliminar un producto.
        [HttpPost]
        [CustomAuthorize(permission: "deleteProducts", module: "Products")]
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
