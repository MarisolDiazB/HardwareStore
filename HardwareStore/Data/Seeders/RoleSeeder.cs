using HardwareStore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HardwareStore.Data.Seeders
{
    public class RoleSeeder
    {
        private readonly DataContext _context;
        public RoleSeeder(DataContext context)
        {
            _context = context;
        }
        public async Task SeedAsync()
        {
            List<Role> roles = new List<Role>
            {
                new Role { Name = "Administrador"},
                new Role { Name = "Vendedor"},
                new Role { Name = "Cliente"}
         };

            foreach (Role role in roles)
            {
                bool exists = await _context.Roles.AnyAsync(r => r.Name == role.Name);

                if (!exists)
                {
                    await _context.Roles.AddAsync(role);
                }
            }

            await _context.SaveChangesAsync();
        }

    }
}

