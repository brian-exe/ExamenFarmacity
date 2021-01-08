using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Api.ViewModels
{
    public class PostArticuloViewModel
    {
        public string Descripcion { get; set; }
        [Required]
        public double Precio { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public bool Activo { get; set; }
    }
}