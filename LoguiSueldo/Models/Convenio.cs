using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System;

namespace LoguiSueldo.Models
{
	public class Convenio
	{
        [Required, Key]
        public int ConvenioID { get; set; }

        public required string Descripcion { get; set; }
        [StringLength(150, ErrorMessage = "El {0} debe tener entre como máximo {1} caracteres.")]

        public string Codigo { get; set; }
    }
}
