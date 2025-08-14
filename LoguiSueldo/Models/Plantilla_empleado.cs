using System.ComponentModel.DataAnnotations;

namespace LoguiSueldo.Models
{
    public class Plantilla_empleado
    {
        [Key]
        public int PlantillaEmpleadoID { get; set; }
        public int PlantillaID { get; set; }
        public int EmpleadoID { get; set; }
        public int EmpresaID { get; set; }
        public string NombrePlan {  get; set; }
        //public string NombreEmpleado { get; set; }

    }
}
