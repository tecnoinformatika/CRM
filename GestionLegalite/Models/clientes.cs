using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestionLegalite.Models
{
    public class clientes
    {
        [Required] //Dato requerido
        [StringLength(20)] //longitud maxima del campo
        [Display(Name = "Nombre de usuario ")] //Mensaje indicar obligatorio
        public string Username { get; set; }

        [Required] //Dato requerido
        [DataType(DataType.Password)] //Indicar dato  tipo password
        [StringLength(20, MinimumLength = 6)] //Longitud minima y maxima
        [Display(Name = "Password ")] //Mensaje indicar obligatorio
        public string Password { get; set; }

    }
}