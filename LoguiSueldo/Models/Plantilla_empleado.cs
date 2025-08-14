using System.ComponentModel.DataAnnotations;

namespace LoguiSueldo.Models
{
    public class Plantilla_empleado
    {
        [Key]
        public int Id_plantilla_empleado { get; set; }
        public int Id_plantilla { get; set; }
        public int Id_empleado { get; set; }
        public int Id_empresa { get; set; }
        public string Nombre_plan {  get; set; }
        public string Nombre_empleado { get; set; }

    }
}
