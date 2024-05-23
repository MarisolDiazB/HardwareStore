using HardwareStore.Data.Entities; // Importa el espacio de nombres para las entidades de datos.
using Microsoft.EntityFrameworkCore; // Importa el espacio de nombres para Entity Framework Core.

namespace HardwareStore.Data.Seeders // Define el espacio de nombres y declara la clase EmployeeSeeder.
{
    public class EmployeeSeeder // Declara la clase EmployeeSeeder.
    {
        private readonly DataContext _context; // Declara una variable privada para el contexto de datos.

        // Constructor de la clase EmployeeSeeder que recibe el contexto de datos como parámetro.
        public EmployeeSeeder(DataContext context)
        {
            _context = context; // Asigna el contexto de datos recibido al campo privado.
        }

        // Método para sembrar datos de empleados de forma asíncrona.
        public async Task SeedAsync()
        {
            List<Employee> employees = new List<Employee> // Crea una lista de empleados.
            {
                // Agrega objetos Employee a la lista.
                new Employee {FirstName="Juan",LastName="Aguirre",Age=37,IsActive=true },
                new Employee {FirstName="Victor",LastName=" Loaiza",Age=44,IsActive=true},
                new Employee {FirstName="Carlos",LastName=" Gomez",Age=36,IsActive=false},
                new Employee {FirstName="Jose",LastName=" Castaño",Age=27,IsActive=true },
                new Employee {FirstName="Alfredo",LastName=" Leon",Age=20,IsActive=true }
            };

            foreach (Employee employee in employees) // Itera sobre cada empleado en la lista.
            {
                // Verifica si el empleado ya existe en la base de datos.
                bool exists = await _context.Employees.AnyAsync(E => E.FirstName == employee.FirstName);

                if (!exists) // Si el empleado no existe en la base de datos.
                {
                    await _context.Employees.AddAsync(employee); // Agrega el empleado al contexto.
                }
            }

            await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos.
        }
    }
}
