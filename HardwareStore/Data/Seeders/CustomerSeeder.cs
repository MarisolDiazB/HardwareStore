using HardwareStore.Data.Entities; // Importa el espacio de nombres para las entidades de datos.
using Microsoft.EntityFrameworkCore; // Importa el espacio de nombres para Entity Framework Core.

namespace HardwareStore.Data.Seeders // Define el espacio de nombres y declara la clase CustomerSeeder.
{
    public class CustomerSeeder // Declara la clase CustomerSeeder.
    {
        private readonly DataContext _context; // Declara una variable privada para el contexto de datos.

        // Constructor de la clase CustomerSeeder que recibe el contexto de datos como parámetro.
        public CustomerSeeder(DataContext context)
        {
            _context = context; // Asigna el contexto de datos recibido al campo privado.
        }

        // Método para sembrar datos de clientes de forma asíncrona.
        public async Task SeedAsync()
        {
            List<Customer> customers = new List<Customer> // Crea una lista de clientes.
            {
                // Agrega objetos Customer a la lista.
                new Customer {FirstName="Juan",LastName="Aguirre",Customeraddress="Cll22a#70a-23",Phone="3123121123"},
                new Customer {FirstName="Fernando",LastName="Salazar",Customeraddress="Cll2a#70a-40",Phone="3123121123"},
                new Customer {FirstName="Carlos",LastName="Estrada",Customeraddress="Cll32a#40-46",Phone="3123121123"},
                new Customer {FirstName="Jaime",LastName="Aguilar",Customeraddress="Cll10a#10-40",Phone="3123121123"},
                new Customer {FirstName="Andres",LastName="Higuita",Customeraddress="Cll42a#10-50",Phone=""}
            };

            foreach (Customer customer1 in customers) // Itera sobre cada cliente en la lista.
            {
                // Verifica si el cliente ya existe en la base de datos.
                bool exists = await _context.Customer.AnyAsync(E => E.FirstName == customer1.FirstName);

                if (!exists) // Si el cliente no existe en la base de datos.
                {
                    await _context.Customer.AddAsync(customer1); // Agrega el cliente al contexto.
                }
            }

            await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos.
        }
    }
}
