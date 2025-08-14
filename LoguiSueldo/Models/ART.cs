using System.ComponentModel.DataAnnotations;

namespace LoguiSueldo.Models
{
    public class ART
    {
        [Key, Required]
        public int ArtID {  get; set; }
        public int ConvenioID { get; set; }
        public string Nombre { get; set; }
    }
}
