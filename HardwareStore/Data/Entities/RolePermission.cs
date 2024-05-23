using HardwareStore.Data.Entities; // Importa el espacio de nombres que contiene las entidades de datos.
using System.ComponentModel.DataAnnotations; // Importa el espacio de nombres para atributos de validación.

namespace HardwareStore.Data.Entities // Define el espacio de nombres y declara la clase RolePermission.
{
    public class RolePermission // Declara la clase RolePermission.
    {
        public int RoleId { get; set; } // Propiedad para el ID del rol.

        public Role? Role { get; set; } // Propiedad de navegación para el rol asociado.

        public int PermissionId { get; set; } // Propiedad para el ID del permiso.

        public Permission? Permission { get; set; } // Propiedad de navegación para el permiso asociado.
    }
}
