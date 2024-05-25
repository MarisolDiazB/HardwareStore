using HardwareStore.Data.Entities; 
using Microsoft.EntityFrameworkCore; 


namespace HardwareStore.Data.Seeders 
{
    public class ProductsSeeder 
    {
        private readonly DataContext _context; 

        public ProductsSeeder(DataContext context)
        {
            _context = context; 
        }

        
        public async Task SeedAsync()
        {
            List<Products> products = new List<Products>(); 

           
            products.Add(new Products { Name = "Ryzen 5 3600g", Price = 200, Stock = 20 });
            products.Add(new Products { Name = "Ram 16gb 2X8gb 3200mhz", Price = 100, Stock = 30 });
            products.Add(new Products { Name = "Ryzen 5 5600x", Price = 400, Stock = 50 });
            products.Add(new Products { Name = "Asus B450M-A", Price = 100, Stock = 100 });
            products.Add(new Products { Name = "MSI RTX 3060", Price = 600, Stock = 10 });

            foreach (Products product in products) 
            {
                
                bool exists = await _context.Products.AnyAsync(p => p.Name == product.Name);

                if (!exists) 
                {
                    await _context.Products.AddAsync(product); 
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
