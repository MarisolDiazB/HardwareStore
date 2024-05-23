using System.ComponentModel.DataAnnotations; // Importa el espacio de nombres de DataAnnotations.

namespace HardwareStore.DTOs // Define el espacio de nombres y declara la clase RoleDTO.
{
    public class RoleDTO
    {
        public int Id { get; set; } // Propiedad que representa el identificador del rol.

        [Display(Name = "Rol")] // Atributo para personalizar el nombre que se mostrará en la interfaz de usuario.
        [MaxLength(64, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")] // Define la longitud máxima permitida para el nombre del rol.
        [Required(ErrorMessage = "El campo {0} es obligatorio.")] // Especifica que el nombre del rol es obligatorio.
        public string Name { get; set; } // Propiedad que representa el nombre del rol.

        public List<PermissionForDTO>? Permissions { get; set; } // Lista de permisos asociados al rol. Puede ser nula.

        public string? PermissionIds { get; set; } // Cadena que representa los identificadores de los permisos asociados al rol. Puede ser nula.
    }
}
