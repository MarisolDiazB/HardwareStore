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

    // Implementa la interfaz IConverterHelper para realizar conversiones entre objetos Role y RoleDTO.
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _context;

        // Constructor que recibe el contexto de datos.
        public ConverterHelper(DataContext context)
        {
            _context = context;
        }

        // Implementación del método ToRole para convertir un objeto RoleDTO en un objeto Role.
        public Role ToRole(RoleDTO dto)
        {
            return new Role
            {
                Id = dto.Id,
                Name = dto.Name,
            };
        }

        // Implementación del método ToRoleDTOAsync para convertir un objeto Role en un objeto RoleDTO de forma asincrónica.
        public async Task<RoleDTO> ToRoleDTOAsync(Role role)
        {
            // Obtiene los permisos asociados al rol de la base de datos.
            List<PermissionForDTO> permissions = await _context.Permissions.Select(p => new PermissionForDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Module = p.Module,
                Selected = _context.RolePermissions.Any(rp => rp.PermissionId == p.Id && rp.RoleId == role.Id)

            }).ToListAsync();

            // Crea un objeto RoleDTO con los datos del rol y sus permisos.
            return new RoleDTO
            {
                Id = role.Id,
                Name = role.Name,
                Permissions = permissions,
            };
        }
    }
}
