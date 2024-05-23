// Este archivo define la entidad Role en el espacio de nombres HardwareStore.Data.Entities.

using System.Collections.Generic; // Importa el espacio de nombres para ICollection e IEnumerable.
using System.ComponentModel.DataAnnotations; // Importa el espacio de nombres para las anotaciones de validación.

namespace HardwareStore.Data.Entities
{
    // Se define la clase Role para representar los roles de usuario en la base de datos.
    public class Role
    {
        // Identificador único del rol.
        public int Id { get; set; }

        // Nombre del rol.
        [Display(Name = "Rol")]
        [MaxLength(64, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;

        // Colección de asociaciones entre roles y permisos.
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

        // Colección de usuarios asociados a este rol.
        public IEnumerable<User> Users { get; set; } = new List<User>(); // Se inicializa con una lista vacía por defecto.
    }
}
