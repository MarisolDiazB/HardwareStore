using HardwareStore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HardwareStore.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Products> Products { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Clients> Clients { get; set; }

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

    }

}
