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
        public int IdConvenio { get; set; }

        public string Descripcion { get; set; }

        public string Codigo { get; set; }
    }
}
