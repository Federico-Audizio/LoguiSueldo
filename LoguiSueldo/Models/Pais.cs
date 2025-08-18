using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LoguiSueldo.Models
{
    public class Pais
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PaisID { get; set; }

        [Required]
        public string PaisNombre { get; set; }

        public string NoDenominacion { get; set; }

        public bool Visible { get; set; }

        public virtual ICollection<Provincia> Provincia { get; set; }
    }
}