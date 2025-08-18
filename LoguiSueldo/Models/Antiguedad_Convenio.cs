using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoguiSueldo.Models
{
    public class Antiguedad_Convenio
    {
        public int ConvenioID { get; set; }
        [Required]
        public float Porcentaje { get; set; }
        public float Desde { get; set; }
        public float Hasta { get; set; }
        public bool CalculaPorAnio { get; set; }
    }
}
