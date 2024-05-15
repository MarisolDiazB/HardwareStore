using HardwareStore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HardwareStore.Data.Seeders
{
    public class ClientSeeder//Customer
    {
        private readonly DataContext _context;
        public ClientSeeder(DataContext context)
        {
            _context = context;
        }
        public async Task SeedAsync()
        {
            List<Clients> clients = new List<Clients>
                {
                new Clients {FirstName="Juan",LastName="Aguirre",Customeraddress="Cll22a#70a-23",Phone="3123121123"},
                new Clients {FirstName="Fernando",LastName="Salazar",Customeraddress="Cll2a#70a-40",Phone="3123121123"},
                new Clients {FirstName="Carlos",LastName="Estrada",Customeraddress="Cll32a#40-46",Phone="3123121123"},
                new Clients {FirstName="Jaime",LastName="Aguilar",Customeraddress="Cll10a#10-40",Phone="3123121123"},
                new Clients {FirstName="Andres",LastName="Higuita",Customeraddress="Cll42a#10-50",Phone=""}
                };

            foreach (Clients clients1 in clients)
            {
                bool exists = await _context.Clients.AnyAsync(E => E.FirstName == clients1.FirstName);

                if (!exists)
                {
                    await _context.Clients.AddAsync(clients1);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
