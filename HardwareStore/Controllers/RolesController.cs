using Microsoft.AspNetCore.Mvc; 
using HardwareStore.DTOs; 
using HardwareStore.Core; 
using HardwareStore.Data.Entities; 
using AspNetCoreHero.ToastNotification.Abstractions;
using HardwareStore.Services;
using HardwareStore.Core.Pagination;

namespace HardwareStore.Controllers
{
    public class RolesController : Controller
    {
        private IRolesService _rolesService;

        private readonly INotyfService _noty;

        private readonly INotyfService _notify; 


        public RolesController(IRolesService rolesService, INotyfService noty)
        {
            _rolesService = rolesService;
            _notify = noty;
        }

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
                Response<PaginationResponse<Role>> response = await _rolesService.GetListAsync(paginationRequest);



                return View(response.Result);

                if (response != null && response.IsSuccess && response.Result != null)
                {
                    return View(response.Result);
                }
                else
                {
                    _notify.Error("Ocurrió un error al obtener la lista de roles.");
                    return View(new PaginationResponse<Role>());
                }
            }
            catch (Exception ex)
            {
                _notify.Error("Ocurrió un error al obtener la lista de roles: " + ex.Message);
                return View(new PaginationResponse<Role>());
            }

            }

        //  para mostrar el formulario de creación de rol.
        [HttpGet]
            //[CustomAuthorize(permission: "createRoles", module: "Roles")]
            public async Task<IActionResult> Create()
            {

                Response<IEnumerable<Permission>> response = await _rolesService.GetPermissionsAsync();

                if (!response.IsSuccess)
                {
                    _notify.Error(response.Message);
                    return RedirectToAction(nameof(Index));
                }

                // Crea un DTO para el formulario de creación de rol.
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
                    _notify.Error("Debe ajustar los errores de validación.");

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
                    _notify.Success(createResponse.Message);
                    return RedirectToAction(nameof(Index));
                }

                _notify.Error(createResponse.Message);
                dto.Permissions = permissionsResponse.Result.Select(p => new PermissionForDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Module = p.Module,
                }).ToList();

                return View(dto);
            }

            //  para mostrar el formulario de edición de rol.
            [HttpGet]
            //[CustomAuthorize(permission: "updateRoles", module: "Roles")]
            public async Task<IActionResult> Edit(int id)
            {

                Response<RoleDTO> response = await _rolesService.GetOneAsync(id);

                if (!response.IsSuccess)
                {
                    _notify.Error(response.Message);
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
                    _notify.Error("Debe ajustar los errores de validación.");

                    // Obtiene los permisos del rol para mostrarlos en el formulario.
                    Response<IEnumerable<PermissionForDTO>> res = await _rolesService.GetPermissionsByRoleAsync(dto.Id);
                    dto.Permissions = res.Result.ToList();

                    return View(dto);
                }

                Response<Role> response = await _rolesService.EditAsync(dto);

                if (response.IsSuccess)
                {
                    _notify.Success(response.Message);
                    return RedirectToAction(nameof(Index));
                }

                _noty.Error(response.Errors.First());

            _notify.Error(response.Errors.First());
            // obtiene los permisos del rol

                Response<IEnumerable<PermissionForDTO>> res2 = await _rolesService.GetPermissionsByRoleAsync(dto.Id);
                dto.Permissions = res2.Result.ToList();
                return View(dto);
            }

            [HttpPost]
            //[CustomAuthorize("deleteRoles", "Roles")]
            public async Task<IActionResult> Delete(int id)
            {
                // Elimina el rol 
                Response<object> response = await _rolesService.DeleteAsync(id);

                if (!response.IsSuccess)
                {
                    _notify.Error(response.Message);
                    return RedirectToAction(nameof(Index));
                }

                _notify.Success(response.Message);
                return RedirectToAction(nameof(Index));
            }
        }
    }

