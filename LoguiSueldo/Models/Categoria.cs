using System.ComponentModel.DataAnnotations;

namespace LoguiSueldo.Models
{
    public class Categoria
    {
        [Key]
        public int CategoriaID { get; set; }
        public string Nombre { get; set; }
        public float Sueldo_basico { get; set; }
        public int TipoUnidad {  get; set; }
        public float CantUnidades { get; set; }
    }
}
