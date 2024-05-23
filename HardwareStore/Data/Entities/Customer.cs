using System.ComponentModel.DataAnnotations; // Importa el espacio de nombres para atributos de validación.

namespace HardwareStore.Data.Entities // Define el espacio de nombres y declara la clase Customer.
{
    public class Customer // Declara la clase Customer.
    {
        [Key] // Especifica que la propiedad Id es la clave primaria.
        public int Id { get; set; } // Propiedad para el identificador del cliente.

        [MaxLength(32, ErrorMessage = "El campo  {0} debe tener máximo {1} caracteres")] // Establece la longitud máxima y el mensaje de error para FirstName.
        [Required(ErrorMessage = "El campo {0} es requerido")] // Indica que FirstName es obligatorio y establece el mensaje de error.
        [Display(Name = "Nombres")] // Especifica el nombre de visualización para FirstName.
        public string FirstName { get; set; } // Propiedad para el nombre del cliente.

        [MaxLength(32, ErrorMessage = "El campo  {0} debe tener máximo {1} caracteres")] // Establece la longitud máxima y el mensaje de error para LastName.
        [Required(ErrorMessage = "El campo {0} es requerido")] // Indica que LastName es obligatorio y establece el mensaje de error.
        [Display(Name = "Apellidos")] // Especifica el nombre de visualización para LastName.
        public string LastName { get; set; } // Propiedad para los apellidos del cliente.

        [MaxLength(32, ErrorMessage = "El campo  {0} debe tener máximo {1} caracteres")] // Establece la longitud máxima y el mensaje de error para Customeraddress.
        [Required(ErrorMessage = "El campo {0} es requerido")] // Indica que Customeraddress es obligatorio y establece el mensaje de error.
        [Display(Name = "DireccionCliente")] // Especifica el nombre de visualización para Customeraddress.
        public string Customeraddress { get; set; } // Propiedad para la dirección del cliente.

        public string? Phone { get; set; } // Propiedad para el número de teléfono del cliente, que puede ser nula.
    }
}
