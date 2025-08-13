using System.ComponentModel.DataAnnotations;

namespace LoguiSueldo.Models
{
    public class Libro_sueldo
    {
        [Key]
        public int Id_libro_sueldo { get; set; }
        public float Cuit_empleador { get; set; }
        public string Identificador { get; set; }
        public float Periodo { get; set; }
        public string Tipo_liquidador { get; set; }
        public int Numero_liquidacion { get; set; }
        public int Dias_base { get; set; }
        public int Cantidad_reg_4 { get; set; }
        public int Id_empresa { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }
        public string Ley {  get; set; }
    }
}
