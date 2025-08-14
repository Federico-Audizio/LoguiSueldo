using System.ComponentModel.DataAnnotations;

namespace LoguiSueldo.Models
{
    public class Plantilla_Detalle
    {
        [Key, Required]
        public int PlantillaDetalleID { get; set; }
        public int PlantillaID { get; set; }
        public int ConceptoID { get; set; }
        public int Inputacion {  get; set; }
        public string Unidad { get; set; }
        public int TipoUnidad { get; set; }
        public string UnidadSubfijo { get; set; }
        public bool CalcularUnidad { get; set; }
    }
}
