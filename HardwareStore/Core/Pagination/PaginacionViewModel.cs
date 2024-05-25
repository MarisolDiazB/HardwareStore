using HardwareStore.Data.Entities; 

namespace HardwareStore.Core.Pagination 
{
    public class PaginacionViewModel 
    {
        public int PaginaActual { get; set; } 
        public int TotalPaginas { get; set; } 
        public IEnumerable<Customer> Customers { get; set; } 
    }

    public class Customers 
    {
        public int Id { get; set; } 
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string Customeraddress { get; set; } 
        public string Phone { get; set; } 
    }
}
