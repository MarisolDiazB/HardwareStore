using System.Reflection.PortableExecutable; // Importa el espacio de nombres para trabajar con archivos ejecutables portátiles.
using Microsoft.EntityFrameworkCore; // Importa el espacio de nombres para Entity Framework Core.

namespace HardwareStore.Data.Seeders // Define el espacio de nombres y declara la clase SeedDb.
{
    public class SeedDb // Declara la clase SeedDb.
    {
        private readonly DataContext _context; // Declara una variable privada para el contexto de datos.

        // Constructor de la clase SeedDb que recibe el contexto de datos como parámetro.
        public SeedDb(DataContext context)
        {
            _context = context; // Asigna el contexto de datos recibido al campo privado.
        }

        // Método para sembrar datos en la base de datos de forma asíncrona.
        public async Task SeedAsync()
        {
            await new ProductsSeeder(_context).SeedAsync(); // Llama al seeder de productos para sembrar datos de productos.

            await new EmployeeSeeder(_context).SeedAsync(); // Llama al seeder de empleados para sembrar datos de empleados.

            await new CustomerSeeder(_context).SeedAsync(); // Llama al seeder de clientes para sembrar datos de clientes.

            await new PermissionSeeder(_context).SeedAsync(); // Llama al seeder de permisos para sembrar datos de permisos.
        }
    }
}
