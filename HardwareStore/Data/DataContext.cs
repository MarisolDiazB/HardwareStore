using HardwareStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Collections.Specialized.BitVector32;

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

        public DbSet<Role> Roles { get; set; }

    }

}
