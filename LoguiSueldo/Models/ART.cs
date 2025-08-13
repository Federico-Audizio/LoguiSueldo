using System.ComponentModel.DataAnnotations;

namespace LoguiSueldo.Models
{
    public class ART
    {
        [Key]
        public int Id_ART {  get; set; }
        public int Id_convenio { get; set; }
        public string Nombre { get; set; }
    }
}
