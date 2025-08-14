using System.ComponentModel.DataAnnotations;

namespace LoguiSueldo.Models
{
    public class Modalidad
    {
        [Key]
        public int Id_modalidad { get; set; }
        public string Nombre { get; set; }
        public float Codigo_afip {  get; set; }
    }
}
