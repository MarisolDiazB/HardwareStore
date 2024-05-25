using HardwareStore.Services;
using Microsoft.Extensions.Logging;

namespace HardwareStore.Data.Seeders
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUsersService _usersService;
        private readonly ILogger<SeedDb> _logger;

        public SeedDb(DataContext context, IUsersService usersService, ILogger<SeedDb> logger)
        {
            _context = context;
            _usersService = usersService;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            try
            {
                _logger.LogInformation("Starting database seeding...");

                _logger.LogInformation("Seeding products...");
                await new ProductsSeeder(_context).SeedAsync();
                _logger.LogInformation("Products seeded successfully.");

                _logger.LogInformation("Seeding employees...");
                await new EmployeeSeeder(_context).SeedAsync();
                _logger.LogInformation("Employees seeded successfully.");

                _logger.LogInformation("Seeding customers...");
                await new CustomerSeeder(_context).SeedAsync();
                _logger.LogInformation("Customers seeded successfully.");

                _logger.LogInformation("Seeding permissions...");
                await new PermissionSeeder(_context).SeedAsync();
                _logger.LogInformation("Permissions seeded successfully.");

                _logger.LogInformation("Seeding user roles...");
                await new UserRoleSeeder(_usersService, _context).SeedAsync();
                _logger.LogInformation("User roles seeded successfully.");

                _logger.LogInformation("Database seeding completed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while seeding the database: {Error}", ex.Message);
                throw;
            }
        }
    }
}