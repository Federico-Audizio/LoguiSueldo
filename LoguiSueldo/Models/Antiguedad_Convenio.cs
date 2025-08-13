using System.ComponentModel.DataAnnotations.Schema;

namespace LoguiSueldo.Models
{
    public class Antiguedad_Convenio
    {
        public int Id_convenio { get; set; }
        public float Porcentaje { get; set; }
        public float Desde { get; set; }
        public float Hasta { get; set; }
        public bool Calcula_por_anio { get; set; }
    }
}
