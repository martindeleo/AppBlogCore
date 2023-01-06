using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Models
{
    public class ApplicationUser :IdentityUser
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El Direccion es obligatorio")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El Ciudad es obligatorio")]
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "El Pais es obligatorio")]
        public string Pais { get; set; }

        //[Required(ErrorMessage = "El telefono es obligatorio")]
        //public string PhoneNumber { get; set; }
    }

}
