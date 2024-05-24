using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HardwareStore.Data.Entities; // Importa el espacio de nombres de Entity Framework Core.

namespace HardwareStore.Data // Define el espacio de nombres y declara la clase DataContext.
{
    public class DataContext : IdentityDbContext<User> // Declara la clase DataContext que hereda de DbContext.
    {
        // Constructor de la clase DataContext que recibe las opciones de DbContext como parámetro.
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }


        // Define un conjunto de entidades para la entidad Products.
        public DbSet<Products> Products { get; set; }

        // Define un conjunto de entidades para la entidad Employee.
        public DbSet<Employee> Employees { get; set; }

        // Define un conjunto de entidades para la entidad Customer.
        public DbSet<Customer> Customer { get; set; }

        // Define un conjunto de entidades para la entidad Permission.
        public DbSet<Permission> Permissions { get; set; }

        // Define un conjunto de entidades para la entidad Role.
        public DbSet<Role> Roles { get; set; }

        // Define un conjunto de entidades para la entidad RolePermission.
        public DbSet<RolePermission> RolePermissions { get; set; }


        // Método para configurar las relaciones de muchos a muchos entre Role y Permission.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Llama al método base de OnModelCreating.

            // Configura la clave principal compuesta de RolePermission.
            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            // Configura la relación de RolePermission con Role.
            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);

            // Configura la relación de RolePermission con Permission.
            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId);
        }
    }
}
