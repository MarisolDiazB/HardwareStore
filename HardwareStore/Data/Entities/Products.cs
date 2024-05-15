using System.ComponentModel.DataAnnotations;

namespace HardwareStore.Data.Entities
{
        public class Products
        {
            [Key]
            public int Id { get; set; }

            [MaxLength(32, ErrorMessage = "El campo {0} debe tener máximo {1} carácteres")]
            [Required(ErrorMessage = "El campo {0} es requerido")]
            [Display(Name = "Nombre")]
            public required string Name { get; set; }

            [Required(ErrorMessage = "El campo {0} es requerido")]
            [Display(Name = "Precio")]
            public double Price { get; set; }

            public int Stock { get; set; }
        }

}

