using System.ComponentModel.DataAnnotations;

namespace LoguiSueldo.Models
{
    public class Sist_jubil
    {
        [Key]
        public int SistJubilID { get; set; }
        public string Nombre { get; set; }
    }
}
