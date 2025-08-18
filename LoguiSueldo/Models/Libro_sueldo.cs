using System.ComponentModel.DataAnnotations;

namespace LoguiSueldo.Models
{
    public class Libro_sueldo
    {
        [Key]
        public int LibroSueldoID { get; set; }
        public float CuitEmpleador { get; set; }
        public string Identificador { get; set; }
        public float Periodo { get; set; }
        public string TipoLiquidador { get; set; }
        public int NumeroLiquidacion { get; set; }
        public int DiasBase { get; set; }
        public int Cantidad_reg_4 { get; set; }
        public int EmpresaID { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }
        public string Ley {  get; set; }
    }
}
