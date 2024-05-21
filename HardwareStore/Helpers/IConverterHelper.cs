using HardwareStore.Data.Entities;
using HardwareStore.Data;
using HardwareStore.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HardwareStore.Helpers
{
    public interface IConverterHelper
    {
        public Role ToRole(RoleDTO dto);
        public Task<RoleDTO> ToRoleDTOAsync(Role role);
    }

    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _context;

        public ConverterHelper(DataContext context)
        {
            _context = context;
        }

        public Role ToRole(RoleDTO dto)
        {
            return new Role
            {
                Id = dto.Id,
                Name = dto.Name,
            };
        }

        public async Task<RoleDTO> ToRoleDTOAsync(Role role)
        {
            List<PermissionForDTO> permissions = await _context.Permissions.Select(p => new PermissionForDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Module = p.Module,
                Selected = _context.RolePermissions.Any(rp => rp.PermissionId == p.Id && rp.RoleId == role.Id)

            }).ToListAsync();

            return new RoleDTO
            {
                Id = role.Id,
                Name = role.Name,
                Permissions = permissions,
            };
        }
    }
}