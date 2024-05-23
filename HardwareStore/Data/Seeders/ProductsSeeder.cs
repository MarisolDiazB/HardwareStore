using HardwareStore.Data.Entities; // Importa el espacio de nombres para las entidades de datos.
using Microsoft.EntityFrameworkCore; // Importa el espacio de nombres para Entity Framework Core.
using System.ComponentModel.DataAnnotations; // Importa el espacio de nombres para las anotaciones de validación.
using static System.Collections.Specialized.BitVector32; // Importa la clase BitVector32 del espacio de nombres System.Collections.Specialized.

namespace HardwareStore.Data.Seeders // Define el espacio de nombres y declara la clase ProductsSeeder.
{
    public class ProductsSeeder // Declara la clase ProductsSeeder.
    {
        private readonly DataContext _context; // Declara una variable privada para el contexto de datos.

        // Constructor de la clase ProductsSeeder que recibe el contexto de datos como parámetro.
        public ProductsSeeder(DataContext context)
        {
            _context = context; // Asigna el contexto de datos recibido al campo privado.
        }

        // Método para sembrar datos de productos de forma asíncrona.
        public async Task SeedAsync()
        {
            List<Products> products = new List<Products>(); // Crea una lista de productos vacía.

            // Agrega productos a la lista.
            products.Add(new Products { Name = "Ryzen 5 3600g", Price = 200, Stock = 20 });
            products.Add(new Products { Name = "Ram 16gb 2X8gb 3200mhz", Price = 100, Stock = 30 });
            products.Add(new Products { Name = "Ryzen 5 5600x", Price = 400, Stock = 50 });
            products.Add(new Products { Name = "Asus B450M-A", Price = 100, Stock = 100 });
            products.Add(new Products { Name = "MSI RTX 3060", Price = 600, Stock = 10 });

            foreach (Products product in products) // Itera sobre cada producto en la lista.
            {
                // Verifica si el producto ya existe en la base de datos.
                bool exists = await _context.Products.AnyAsync(p => p.Name == product.Name);

                if (!exists) // Si el producto no existe en la base de datos.
                {
                    await _context.Products.AddAsync(product); // Agrega el producto al contexto.
                }
            }

            await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos.
        }
    }
}
