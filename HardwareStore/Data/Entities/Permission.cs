using System.ComponentModel.DataAnnotations; // Importa el espacio de nombres para atributos de validación.

namespace HardwareStore.Data.Entities // Define el espacio de nombres y declara la clase Permission.
{
    public class Permission // Declara la clase Permission.
    {
        [Key] // Especifica que la propiedad Id es la clave primaria.
        public int Id { get; set; } // Propiedad para el identificador del permiso.

        [Display(Name = "Permiso")] // Especifica el nombre de visualización para Name.
        [MaxLength(64, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")] // Establece la longitud máxima y el mensaje de error para Name.
        [Required(ErrorMessage = "El campo {0} es obligatorio.")] // Indica que Name es obligatorio y establece el mensaje de error.
        public string Name { get; set; } = null!; // Propiedad para el nombre del permiso.

        [Display(Name = "Descripción")] // Especifica el nombre de visualización para Description.
        [MaxLength(512, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")] // Establece la longitud máxima y el mensaje de error para Description.
        public string? Description { get; set; } // Propiedad opcional para la descripción del permiso.

        [Display(Name = "Módulo")] // Especifica el nombre de visualización para Module.
        [MaxLength(64, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")] // Establece la longitud máxima y el mensaje de error para Module.
        [Required(ErrorMessage = "El campo {0} es obligatorio.")] // Indica que Module es obligatorio y establece el mensaje de error.
        public string Module { get; set; } = null!; // Propiedad para el módulo al que pertenece el permiso.

        public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>(); // Propiedad de navegación para las asignaciones de roles y permisos.
    }
}




/*  public class Permission
  {
      [Key]
      public int Id { get; set; }

      [Display(Name = "Permiso")]
      [MaxLength(64, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
      [Required(ErrorMessage = "El campo {0} es obligatorio.")]
      public string Name { get; set; } = null!;

      [Display(Name = "Descripción")]
      [MaxLength(512, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
      public string? Description { get; set; }

      [Display(Name = "Módulo")]
      [MaxLength(64, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
      [Required(ErrorMessage = "El campo {0} es obligatorio.")]
      public string Module { get; set; } = null!;

      public required ICollection<RolePermission> RolePermissions { get; set; }
  }
}*/
