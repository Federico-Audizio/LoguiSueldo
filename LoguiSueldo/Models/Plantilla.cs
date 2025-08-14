using System.ComponentModel.DataAnnotations;

namespace LoguiSueldo.Models
{
    public class Plantilla
    {
        [Key]
        public int PlantillaID { get; set; }
        public float Descripcion { get; set; }
        public int ConvenioID { get; set; }
    }
}
