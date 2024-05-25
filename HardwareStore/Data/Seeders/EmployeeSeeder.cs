using HardwareStore.Data.Entities; 
using Microsoft.EntityFrameworkCore; 

namespace HardwareStore.Data.Seeders 
{
    public class EmployeeSeeder 
    {
        private readonly DataContext _context; 

        public EmployeeSeeder(DataContext context)
        {
            _context = context; 
        }

        public async Task SeedAsync()
        {
            List<Employee> employees = new List<Employee> 
            {
               
                new Employee {FirstName="Juan",LastName="Aguirre",Age=37,IsActive=true },
                new Employee {FirstName="Victor",LastName=" Loaiza",Age=44,IsActive=true},
                new Employee {FirstName="Carlos",LastName=" Gomez",Age=36,IsActive=false},
                new Employee {FirstName="Jose",LastName=" Castaño",Age=27,IsActive=true },
                new Employee {FirstName="Alfredo",LastName=" Leon",Age=20,IsActive=true }
            };

            foreach (Employee employee in employees) 
            {
                bool exists = await _context.Employees.AnyAsync(E => E.FirstName == employee.FirstName);

                if (!exists) 
                {
                    await _context.Employees.AddAsync(employee); 
                }
            }

            await _context.SaveChangesAsync(); 
        }
    }
}
