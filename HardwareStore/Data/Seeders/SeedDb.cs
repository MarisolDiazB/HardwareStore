using HardwareStore.Services;

namespace HardwareStore.Data.Seeders
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUsersService _usersService;


        public SeedDb(DataContext context, IUsersService usersService)
        {
            _context = context;
            _usersService = usersService;
        }


        public async Task SeedAsync()
        {
            await new ProductsSeeder(_context).SeedAsync();
            await new EmployeeSeeder(_context).SeedAsync();
            await new CustomerSeeder(_context).SeedAsync();
            await new PermissionSeeder(_context).SeedAsync();
            await new UserRoleSeeder(_usersService, _context).SeedAsync();
        }
    }
}