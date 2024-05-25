
using System.ComponentModel.DataAnnotations; 

namespace HardwareStore.Data.Entities
{
    public class Role
    {
       
        public int Id { get; set; }

       
        [Display(Name = "Rol")]
        [MaxLength(64, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;

        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

        
        public IEnumerable<User> Users { get; set; } = new List<User>(); 
    }
}
