using System.ComponentModel.DataAnnotations;

namespace LoguiSueldo.Models
{
    public class Categoria
    {
        [Key]
        public int Id_categoria { get; set; }
        public string Nombre { get; set; }
        public float Sueldo_basico { get; set; }
        public int Tipo_unidad {  get; set; }
        public float Cant_unidades { get; set; }
    }
}
