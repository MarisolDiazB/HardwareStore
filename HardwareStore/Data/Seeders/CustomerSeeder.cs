using HardwareStore.Data.Entities; 
using Microsoft.EntityFrameworkCore; 

namespace HardwareStore.Data.Seeders 
{
    public class CustomerSeeder 
    {
        private readonly DataContext _context; 

        public CustomerSeeder(DataContext context)
        {
            _context = context; 
        }

        public async Task SeedAsync()
        {
            List<Customer> customers = new List<Customer> 
            {
                
                new Customer {FirstName="Juan",LastName="Aguirre",Customeraddress="Cll22a#70a-23",Phone="3123121123"},
                new Customer {FirstName="Fernando",LastName="Salazar",Customeraddress="Cll2a#70a-40",Phone="3123121123"},
                new Customer {FirstName="Carlos",LastName="Estrada",Customeraddress="Cll32a#40-46",Phone="3123121123"},
                new Customer {FirstName="Jaime",LastName="Aguilar",Customeraddress="Cll10a#10-40",Phone="3123121123"},
                new Customer {FirstName="Andres",LastName="Higuita",Customeraddress="Cll42a#10-50",Phone=""}
            };

            foreach (Customer customer1 in customers) 
            {
                bool exists = await _context.Customer.AnyAsync(E => E.FirstName == customer1.FirstName);

                if (!exists) 
                {
                    await _context.Customer.AddAsync(customer1); 
                }
            }

            await _context.SaveChangesAsync(); 
        }
    }
}
