﻿using AspNetCoreHero.ToastNotification.Abstractions;
using HardwareStore.Core;
using HardwareStore.Core.Pagination;
using HardwareStore.Data.Entities;
using HardwareStore.Services;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Index([FromQuery] int? RecordsPerPage,
                                               [FromQuery] int? Page,
                                               [FromQuery] string? Filter)
        {
            PaginationRequest paginationRequest = new PaginationRequest
            {
                RecordsPerPage = RecordsPerPage ?? 15,
                Page = Page ?? 1,
                Filter = Filter,
            };

            Response<PaginationResponse<Products>> response = await _services.GetListAsync(paginationRequest);

            return View(response.Result);
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