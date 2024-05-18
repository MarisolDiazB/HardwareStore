﻿using Microsoft.AspNetCore.Mvc;
using HardwareStore.Core.Attributes;
using HardwareStore.DTOs;
using HardwareStore.Core.Pagination;
using HardwareStore.Core;
using HardwareStore.Services;
using HardwareStore.Data.Entities;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace HardwareStore.Controllers
{
    public class RolesController : Controller
    {
        private IRolesService _rolesService;
        private readonly INotyfService _noty;

        public RolesController(IRolesService rolesService, INotyfService noty)
        {
            _rolesService = rolesService;
            _noty = noty;
        }

        [HttpGet]
        //[CustomAuthorize(permission: "showRoles", module: "Roles")]
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

            Response<PaginationResponse<Role>> response = await _rolesService.GetListAsync(paginationRequest);

            return View(response.Result);
        }

        [HttpGet]
        //[CustomAuthorize(permission: "createRoles", module: "Roles")]
        public async Task<IActionResult> Create()
        {
            Response<IEnumerable<Permission>> response = await _rolesService.GetPermissionsAsync();

            if (!response.IsSuccess)
            {
                _noty.Error(response.Message);
                return RedirectToAction(nameof(Index));
            }

            RoleDTO dto = new RoleDTO
            {
                Permissions = response.Result.Select(p => new PermissionForDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Module = p.Module,
                }).ToList()
            };
            return View(dto);
        }

        [HttpPost]
        //[CustomAuthorize(permission: "createRoles", module: "Roles")]
        public async Task<IActionResult> Create(RoleDTO dto)
        {
            Response<IEnumerable<Permission>> permissionsResponse = await _rolesService.GetPermissionsAsync();

            if (!ModelState.IsValid)
            {
                _noty.Error("Debe ajustar los errores de validación.");

                dto.Permissions = permissionsResponse.Result.Select(p => new PermissionForDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Module = p.Module,
                }).ToList();

                return View(dto);
            }

            Response<Role> createResponse = await _rolesService.CreateAsync(dto);

            if (createResponse.IsSuccess)
            {
                _noty.Success(createResponse.Message);
                return RedirectToAction(nameof(Index));
            }

            _noty.Error(createResponse.Message);
            dto.Permissions = permissionsResponse.Result.Select(p => new PermissionForDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Module = p.Module,
            }).ToList();

            return View(dto);
        }

        [HttpGet]
        //[CustomAuthorize(permission: "updateRoles", module: "Roles")]
        public async Task<IActionResult> Edit(int id)
        {
            Response<RoleDTO> response = await _rolesService.GetOneAsync(id);

            if (!response.IsSuccess)
            {
                _noty.Error(response.Message);
                return RedirectToAction(nameof(Index));
            }

            return View(response.Result);
        }

        [HttpPost]
        //[CustomAuthorize(permission: "updateRoles", module: "Roles")]
        public async Task<IActionResult> Edit(RoleDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _noty.Error("Debe ajustar los errores de validación.");

                Response<IEnumerable<PermissionForDTO>> res = await _rolesService.GetPermissionsByRoleAsync(dto.Id);
                dto.Permissions = res.Result.ToList();

                return View(dto);
            }

            Response<Role> response = await _rolesService.EditAsync(dto);

            if (response.IsSuccess)
            {
                _noty.Success(response.Message);
                return RedirectToAction(nameof(Index));
            }

            _noty.Error(response.Errors.First());
            Response<IEnumerable<PermissionForDTO>> res2 = await _rolesService.GetPermissionsByRoleAsync(dto.Id);
            dto.Permissions = res2.Result.ToList();
            return View(dto);
        }

        [HttpPost]
        //[CustomAuthorize("deleteRoles", "Roles")]
        public async Task<IActionResult> Delete(int id)
        {
            Response<object> response = await _rolesService.DeleteAsync(id);

            if (!response.IsSuccess)
            {
                _noty.Error(response.Message);
                return RedirectToAction(nameof(Index));
            }

            _noty.Success(response.Message);
            return RedirectToAction(nameof(Index));
        }
    }
}
