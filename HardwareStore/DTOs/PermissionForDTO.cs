using HardwareStore.Data.Entities; // Importa el espacio de nombres de las entidades de datos.

namespace HardwareStore.DTOs // Define el espacio de nombres y declara la clase PermissionForDTO.
{
    public class PermissionForDTO : Permission // Declara la clase PermissionForDTO que hereda de la clase Permission.
    {
        public bool Selected { get; set; } = false; // Propiedad booleana para indicar si el permiso está seleccionado, con valor predeterminado false.
    }
}
