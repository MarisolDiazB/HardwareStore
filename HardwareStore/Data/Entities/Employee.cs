using System.ComponentModel.DataAnnotations; // Importa el espacio de nombres para atributos de validación.

namespace HardwareStore.Data.Entities // Define el espacio de nombres y declara la clase Employee.
{
    public class Employee // Declara la clase Employee.
    {
        [Key] // Especifica que la propiedad Id es la clave primaria.
        public int Id { get; set; } // Propiedad para el identificador del empleado.

        [MaxLength(32, ErrorMessage = "El campo  {0} debe tener máximo {1} caracteres")] // Establece la longitud máxima y el mensaje de error para FirstName.
        [Required(ErrorMessage = "El campo {0} es requerido")] // Indica que FirstName es obligatorio y establece el mensaje de error.
        [Display(Name = "Nombres")] // Especifica el nombre de visualización para FirstName.
        public string FirstName { get; set; } // Propiedad para el nombre del empleado.

        [MaxLength(32, ErrorMessage = "El campo  {0} debe tener máximo {1} caracteres")] // Establece la longitud máxima y el mensaje de error para LastName.
        [Required(ErrorMessage = "El campo {0} es requerido")] // Indica que LastName es obligatorio y establece el mensaje de error.
        [Display(Name = "Apellidos")] // Especifica el nombre de visualización para LastName.
        public string LastName { get; set; } // Propiedad para los apellidos del empleado.

        public int Age { get; set; } // Propiedad para la edad del empleado.
        public bool IsActive { get; set; } = true; // Propiedad para indicar si el empleado está activo, con valor predeterminado verdadero.
    }
}
