using System.ComponentModel.DataAnnotations; 

namespace HardwareStore.Data.Entities 
{
    public class Permission 
    {
        [Key] 
        public int Id { get; set; } 

        [Display(Name = "Permiso")] 
        [MaxLength(64, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")] 
        [Required(ErrorMessage = "El campo {0} es obligatorio.")] 
        public string Name { get; set; } = null!; 

        [Display(Name = "Descripción")] 
        [MaxLength(512, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")] 
        public string? Description { get; set; } 

        [Display(Name = "Módulo")] 
        [MaxLength(64, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")] 
        [Required(ErrorMessage = "El campo {0} es obligatorio.")] 
        public string Module { get; set; } = null!; 

        public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>(); 
    }
}


