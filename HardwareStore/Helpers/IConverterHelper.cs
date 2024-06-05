using HardwareStore.Data.Entities; 
using HardwareStore.Data;
using HardwareStore.DTOs;
using Microsoft.EntityFrameworkCore;


namespace HardwareStore.Helpers
{
    public interface IConverterHelper
    {
        public Role ToRole(RoleDTO dto);
        public AccountUserDTO ToAccountDTO(User user);

        public Task<RoleDTO> ToRoleDTOAsync(Role role);
        public User ToUser(UserDTO dto);
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

        public AccountUserDTO ToAccountDTO(User user)
        {
            return new AccountUserDTO
            {
                Id = Guid.Parse(user.Id),
                Document = user.Document,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
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

            // Crea un objeto RoleDTO con los datos del rol y sus permisos.
            return new RoleDTO
            {
                Id = role.Id,
                Name = role.Name,
                Permissions = permissions,
            };
        }

        public User ToUser(UserDTO dto)
        {
            return new User
            {
                Id = dto.Id.ToString(),
                Document = dto.Document,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                UserName = dto.Email,
                RoleId = dto.RoleId,
                PhoneNumber = dto.PhoneNumber,
               
            };
        }
    }
}
