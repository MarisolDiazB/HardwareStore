using HardwareStore.Data.Entities; // Importa el espacio de nombres para las entidades de datos.

namespace HardwareStore.Core.Pagination // Define el espacio de nombres y declara las clases PaginacionViewModel y Customers.
{
    public class PaginacionViewModel // Declara la clase PaginacionViewModel.
    {
        public int PaginaActual { get; set; } // Propiedad para almacenar el número de página actual.
        public int TotalPaginas { get; set; } // Propiedad para almacenar el número total de páginas.
        public IEnumerable<Customer> Customers { get; set; } // Colección de clientes para mostrar en la vista.
    }

    public class Customers // Declara la clase Customers.
    {
        public int Id { get; set; } // Propiedad para el identificador del cliente.
        public string FirstName { get; set; } // Propiedad para el nombre del cliente.
        public string LastName { get; set; } // Propiedad para el apellido del cliente.
        public string Customeraddress { get; set; } // Propiedad para la dirección del cliente.
        public string Phone { get; set; } // Propiedad para el número de teléfono del cliente.
    }
}
