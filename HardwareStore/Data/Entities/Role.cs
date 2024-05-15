using System.ComponentModel.DataAnnotations;

namespace HardwareStore.Data.Entities
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(32, ErrorMessage = "El campo  {0} debe tener máximo {1} carácteres")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }
    }
}
