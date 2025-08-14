using System.ComponentModel.DataAnnotations;

namespace LoguiSueldo.Models
{
    public class Sist_jubil
    {
        [Key]
        public int Id_sist_jubil { get; set; }
        public string Nombre { get; set; }
    }
}
