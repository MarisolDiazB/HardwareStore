using AspNetCoreHero.ToastNotification.Abstractions;
using HardwareStore.Core.Attributes;
using HardwareStore.Core.Pagination;
using HardwareStore.Core;
using HardwareStore.Data.Entities;
using HardwareStore.Helpers;
using HardwareStore.Services;
using HardwareStore.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HardwareStore.Controllers
{
    public class UsersController : Controller
    {
        private readonly ICombosHelper _combosHelper;
        private readonly INotyfService _noty;
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService, ICombosHelper combosHelper, INotyfService noty)
        {
            _usersService = usersService;
            _combosHelper = combosHelper;
            _noty = noty;
        }

        [HttpGet]
        [CustomAuthorize(permission: "showUsers", module: "Usuarios")]
        public async Task<IActionResult> Index([FromQuery] int? Page,
                                               [FromQuery] int? RecordsPerPage,
                                               [FromQuery] string? Filter)
        {
            PaginationRequest request = new PaginationRequest
            {
                RecordsPerPage = RecordsPerPage ?? 15,
                Page = Page ?? 1,
                Filter = Filter
            };

            return View(await _usersService.GetUsersPaginatedAsync(request));
        }

        [HttpGet]
        [CustomAuthorize(permission: "createUsers", module: "Usuarios")]
        public async Task<IActionResult> Create()
        {
            return View(new UserDTO
            {
                IsNew = true,
                Roles = await _combosHelper.GetComboRolesAsync()
            });
        }

        [HttpPost]
        [CustomAuthorize(permission: "createUsers", module: "Usuarios")]
        public async Task<IActionResult> Create(UserDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _noty.Error("Debe ajustar los errores de validación.");
                dto.Roles = await _combosHelper.GetComboRolesAsync();
                return View(dto);
            }

            Response<User> response = await _usersService.CreateAsync(dto);

            if (response.IsSuccess)
            {
                _noty.Success(response.Message);
                return RedirectToAction(nameof(Index));
            }

            _noty.Error(response.Message);
            dto.Roles = await _combosHelper.GetComboRolesAsync();
            return View(dto);
        }

    }
}

