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
        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdConvenio { get; set; }


    }
}
