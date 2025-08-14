using System.ComponentModel.DataAnnotations;

namespace LoguiSueldo.Models
{
    public class Plantilla
    {
        [Key]
        public int Id_plantilla { get; set; }
        public float Descripcion { get; set; }
        public int Id_convenio { get; set; }
    }
}
