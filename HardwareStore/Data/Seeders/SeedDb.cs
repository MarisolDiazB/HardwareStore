using System.Reflection.PortableExecutable;
using Microsoft.EntityFrameworkCore;

namespace HardwareStore.Data.Seeders
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await new ProductsSeeder(_context).SeedAsync();

            await new EmployeeSeeder(_context).SeedAsync();

            await new ClientSeeder(_context).SeedAsync();
            
            await new RoleSeeder (_context).SeedAsync();
        }
    }
}
