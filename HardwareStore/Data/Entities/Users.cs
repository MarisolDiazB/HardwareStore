using Microsoft.AspNetCore.Identity; // Importa el espacio de nombres para la funcionalidad de Identity.
using System.ComponentModel.DataAnnotations; // Importa el espacio de nombres para atributos de validación.

namespace HardwareStore.Data.Entities // Define el espacio de nombres y declara la clase User.
{
    public class User : IdentityUser // Declara la clase User, que hereda de IdentityUser proporcionada por ASP.NET Core Identity.
    {
        [Display(Name = "Documento")] // Atributo de visualización para personalizar el nombre en las vistas.
        [MaxLength(32, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")] // Especifica la longitud máxima del campo y el mensaje de error si se excede.
        [Required(ErrorMessage = "El campo {0} es obligatorio.")] // Especifica que el campo es obligatorio y el mensaje de error si está vacío.
        public string Document { get; set; } = null!; // Propiedad para el documento del usuario.

        [Display(Name = "Nombres")] // Atributo de visualización para personalizar el nombre en las vistas.
        [MaxLength(64, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")] // Especifica la longitud máxima del campo y el mensaje de error si se excede.
        [Required(ErrorMessage = "El campo {0} es obligatorio.")] // Especifica que el campo es obligatorio y el mensaje de error si está vacío.
        public string FirstName { get; set; } = null!; // Propiedad para el primer nombre del usuario.

        [Display(Name = "Apellidos")] // Atributo de visualización para personalizar el nombre en las vistas.
        [MaxLength(64, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")] // Especifica la longitud máxima del campo y el mensaje de error si se excede.
        [Required(ErrorMessage = "El campo {0} es obligatorio.")] // Especifica que el campo es obligatorio y el mensaje de error si está vacío.
        public string LastName { get; set; } = null!; // Propiedad para el apellido del usuario.

        [Display(Name = "Usuario")] // Atributo de visualización para personalizar el nombre en las vistas.
        public string FullName => $"{FirstName} {LastName}"; // Propiedad de solo lectura que retorna el nombre completo del usuario.

        [Required(ErrorMessage = "El campo {0} es obligatorio.")] // Especifica que el campo es obligatorio y el mensaje de error si está vacío.
        public int RoleId { get; set; } // Propiedad para el ID del rol del usuario.

        public Role Role { get; set; } = null!; // Propiedad de navegación para el rol del usuario.
    }
}
