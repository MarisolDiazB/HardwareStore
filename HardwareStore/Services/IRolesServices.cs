using HardwareStore.Core.Pagination;
using HardwareStore.Core;
using HardwareStore.Data.Entities;
using HardwareStore.Data;
using HardwareStore.Helpers;
using HardwareStore.DTOs;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;


namespace HardwareStore.Services
{
    public interface IRolesService
    {
        public Task<Response<Role>> CreateAsync(RoleDTO dto);

        public Task<Response<object>> DeleteAsync(int id);

        public Task<Response<Role>> EditAsync(RoleDTO dto);

        public Task<Response<PaginationResponse<Role>>> GetListAsync(PaginationRequest request);

        public Task<Response<RoleDTO>> GetOneAsync(int id);

        public Task<Response<IEnumerable<Permission>>> GetPermissionsAsync();

        public Task<Response<IEnumerable<PermissionForDTO>>> GetPermissionsByRoleAsync(int id);
    }

    public class RolesService : IRolesService
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;

        public RolesService(DataContext context, IConverterHelper converterHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
        }

        public async Task<Response<Role>> CreateAsync(RoleDTO dto)
        {
            using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Creación de Rol
                    Role model = _converterHelper.ToRole(dto);
                    EntityEntry<Role> modelStored = await _context.Roles.AddAsync(model);

                    await _context.SaveChangesAsync();

                    // Inserción de permisos
                    int roleId = modelStored.Entity.Id;

                    List<int> permissionIds = new List<int>();

                    if (!string.IsNullOrWhiteSpace(dto.PermissionIds))
                    {
                        permissionIds = JsonConvert.DeserializeObject<List<int>>(dto.PermissionIds);
                    }

                    foreach (int permissionId in permissionIds)
                    {
                        RolePermission rolePermission = new RolePermission
                        {
                            RoleId = roleId,
                            PermissionId = permissionId
                        };

                        _context.RolePermissions.Add(rolePermission);
                    }

                    await _context.SaveChangesAsync();
                    transaction.Commit();

                    return ResponseHelper<Role>.MakeResponseSuccess("Rol creado con éxito");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return ResponseHelper<Role>.MakeResponseFail(ex);
                }
            }
        }

        public async Task<Response<object>> DeleteAsync(int id)
        {
            try
            {
                Response<Role> roleResponse = await GetOneModelAsync(id);

                if (!roleResponse.IsSuccess)
                {
                    return ResponseHelper<object>.MakeResponseFail(roleResponse.Message);
                }

                if (roleResponse.Result.Name == Constants.SUPER_ADMIN_ROLE_NAME)
                {
                    return ResponseHelper<object>.MakeResponseFail($"El rol {Constants.SUPER_ADMIN_ROLE_NAME} no puede ser eliminado");
                }

                if (roleResponse.Result.Users.Count() > 0)
                {
                    return ResponseHelper<object>.MakeResponseFail($"El rol no puede ser eliminado debido a que tiene usuarios relacionados");
                }

                _context.Roles.Remove(roleResponse.Result);
                await _context.SaveChangesAsync();

                return ResponseHelper<object>.MakeResponseSuccess("Rol eliminado con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<object>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<Role>> EditAsync(RoleDTO dto)
        {
            try
            {
                if (dto.Name == Constants.SUPER_ADMIN_ROLE_NAME)
                {
                    return ResponseHelper<Role>.MakeResponseFail($"El Rol '{Constants.SUPER_ADMIN_ROLE_NAME}' no puede ser editado");
                }

                List<int> permissionIds = new List<int>();

                if (!string.IsNullOrEmpty(dto.PermissionIds))
                {
                    permissionIds = JsonConvert.DeserializeObject<List<int>>(dto.PermissionIds);
                }

                // Eliminación de antiguos permisos
                List<RolePermission> oldRolePermissions = await _context.RolePermissions.Where(rs => rs.RoleId == dto.Id).ToListAsync();
                _context.RolePermissions.RemoveRange(oldRolePermissions);

                // Inserción de nuevos permisos
                foreach (int permissionId in permissionIds)
                {
                    RolePermission rolePermission = new RolePermission
                    {
                        RoleId = dto.Id,
                        PermissionId = permissionId
                    };

                    _context.RolePermissions.Add(rolePermission);
                }

                // Actualización de rol
                Role model = _converterHelper.ToRole(dto);
                _context.Roles.Update(model);

                await _context.SaveChangesAsync();

                return ResponseHelper<Role>.MakeResponseSuccess("Rol editado con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<Role>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<PaginationResponse<Role>>> GetListAsync(PaginationRequest request)
        {
            try
            {
                IQueryable<Role> queryable = _context.Roles.AsQueryable();

                if (!string.IsNullOrWhiteSpace(request.Filter))
                {
                    queryable = queryable.Where(s => s.Name.ToLower().Contains(request.Filter.ToLower()));
                }

                PagedList<Role> list = await PagedList<Role>.ToPagedListAsync(queryable, request);

                PaginationResponse<Role> result = new PaginationResponse<Role>
                {
                    List = list,
                    TotalCount = list.TotalCount,
                    RecordsPerPage = list.RecordsPerPage,
                    CurrentPage = list.CurrentPage,
                    TotalPages = list.TotalPages,
                    Filter = request.Filter,
                };

                return ResponseHelper<PaginationResponse<Role>>.MakeResponseSuccess(result, "Roles obtenidas con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<PaginationResponse<Role>>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<RoleDTO>> GetOneAsync(int id)
        {
            try
            {
                Role? privateBlogRole = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);

                if (privateBlogRole is null)
                {
                    return ResponseHelper<RoleDTO>.MakeResponseFail($"El Rol con id '{id}' no existe.");
                }

                return ResponseHelper<RoleDTO>.MakeResponseSuccess(await _converterHelper.ToRoleDTOAsync(privateBlogRole));
            }
            catch (Exception ex)
            {
                return ResponseHelper<RoleDTO>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<IEnumerable<Permission>>> GetPermissionsAsync()
        {
            try
            {
                IEnumerable<Permission> permissions = await _context.Permissions.ToListAsync();

                return ResponseHelper<IEnumerable<Permission>>.MakeResponseSuccess(permissions);
            }
            catch (Exception ex)
            {
                return ResponseHelper<IEnumerable<Permission>>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<IEnumerable<PermissionForDTO>>> GetPermissionsByRoleAsync(int id)
        {
            try
            {
                Response<RoleDTO> response = await GetOneAsync(id);

                if (!response.IsSuccess)
                {
                    return ResponseHelper<IEnumerable<PermissionForDTO>>.MakeResponseSuccess(response.Message);
                }

                List<PermissionForDTO> permissions = response.Result.Permissions;

                return ResponseHelper<IEnumerable<PermissionForDTO>>.MakeResponseSuccess(permissions);
            }
            catch (Exception ex)
            {
                return ResponseHelper<IEnumerable<PermissionForDTO>>.MakeResponseFail(ex);
            }
        }

        private async Task<Response<Role>> GetOneModelAsync(int id)
        {
            try
            {
                Role? role = await _context.Roles.Include(r => r.Users)
                                                                       .FirstOrDefaultAsync(r => r.Id == id);

                if (role is null)
                {
                    return ResponseHelper<Role>.MakeResponseFail($"El Rol con id '{id}' no existe");
                }

                return ResponseHelper<Role>.MakeResponseSuccess(role);

            }
            catch (Exception ex)
            {
                return ResponseHelper<Role>.MakeResponseFail(ex);
            }
        }
    }
}