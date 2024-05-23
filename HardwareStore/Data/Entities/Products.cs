using System.ComponentModel.DataAnnotations; // Importa el espacio de nombres para atributos de validación.

namespace HardwareStore.Data.Entities // Define el espacio de nombres y declara la clase Products.
{
    public class Products // Declara la clase Products.
    {
        [Key] // Especifica que la propiedad Id es la clave primaria.
        public int Id { get; set; } // Propiedad para el identificador del producto.

        [MaxLength(32, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")] // Establece la longitud máxima y el mensaje de error para Name.
        [Required(ErrorMessage = "El campo {0} es requerido.")] // Indica que Name es obligatorio y establece el mensaje de error.
        [Display(Name = "Nombre")] // Especifica el nombre de visualización para Name.
        public string Name { get; set; } // Propiedad para el nombre del producto.

        [Required(ErrorMessage = "El campo {0} es requerido.")] // Indica que Price es obligatorio y establece el mensaje de error.
        [Display(Name = "Precio")] // Especifica el nombre de visualización para Price.
        public double Price { get; set; } // Propiedad para el precio del producto.

        public int Stock { get; set; } // Propiedad para el stock del producto.
    }
}
