using System.ComponentModel.DataAnnotations;

namespace LoguiSueldo.Models
{
    public class Obra_social
    {
        [Key]
        public int ObraSocialID { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
    }
}
