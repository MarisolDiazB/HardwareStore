﻿using Microsoft.AspNetCore.Identity; // Importa el espacio de nombres para la funcionalidad de Identity.
using System.ComponentModel.DataAnnotations; // Importa el espacio de nombres para atributos de validación.

namespace HardwareStore.Data.Entities // Define el espacio de nombres y declara la clase User.
{
    public class User : IdentityUser // Declara la clase User, que hereda de IdentityUser proporcionada por ASP.NET Core Identity.
    {
        [Display(Name = "Documento")]
        [MaxLength(32, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Document { get; set; } = null!;

        [Display(Name = "Nombres")]
        [MaxLength(64, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Apellidos")]
        [MaxLength(64, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string LastName { get; set; } = null!;

        [Display(Name = "Foto")]
        public string? Photo { get; set; }

        [Display(Name = "Usuario")]
        public string FullName => $"{FirstName} {LastName}";

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int RoleId { get; set; }

        public Role Role { get; set; } = null!;
    }
}
