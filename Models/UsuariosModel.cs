using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Metodología_de_Software.Models
{
    public class UsuariosModel
    {
        [Display(Name = "Id Usuario")]
        public int IdUsuario { get; set; }

        [Display(Name = "Nombre Completo")]
        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(length: 45, ErrorMessage = "El nombre no debe tener más de 45 caracteres")]
        public string NombreCompleto { get; set; }

        [Display(Name = "Nombre Usuario")]
        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(length: 45, ErrorMessage = "El nombre no debe tener más de 45 caracteres")]
        public string NombreUsuario { get; set; }

        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "La contraseña es requerida")]
        [MaxLength(length: 64, ErrorMessage = "La contraseña no debe tener más de 64 caracteres")]
        public string Contraseña { get; set; }

        [Display(Name = "Correo")]
        [EmailAddress]
        [Required(ErrorMessage = "El correo es requerido")]
        [MaxLength(length: 45, ErrorMessage = "El correo no debe tener más de 45 caracteres")]
        public string Correo { get; set; }

        public string Mensaje { get; set; }
    }
}