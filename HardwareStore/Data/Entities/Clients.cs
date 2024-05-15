using System.ComponentModel.DataAnnotations;

namespace HardwareStore.Data.Entities
{
    public class Clients
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(32, ErrorMessage = "El campo  {0} debe tener máximo {1} carácteres")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Nombres")]
        public required string FirstName { get; set; }
        [MaxLength(32, ErrorMessage = "El campo  {0} debe tener máximo {1} carácteres")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Apellidos")]
        public string? LastName { get; set; }
        [MaxLength(32, ErrorMessage = "El campo  {0} debe tener máximo {1} carácteres")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "DireccionCliente")]
        public required string Customeraddress { get; set; }

        public string? Phone { get; set; }

    }
}
