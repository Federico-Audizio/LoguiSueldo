using System.ComponentModel.DataAnnotations;

namespace LoguiSueldo.Models
{
    public class Concepto_Afip
    {
        [Key, Required]
        public string ConceptoAfip { get; set; }
        public int ConceptoID { get; set; }
        public string Descripcion { get; set; }
        public bool Repeticion { get; set; }
        public bool AportesSipa { get; set; }
        public bool ContribucionSipa { get; set; }
        public bool AportesInssjyp { get; set; }
        public bool ContribucionInssjyp { get; set; }
        public bool AporteObraSocial { get; set; }
        public bool ContribucionObraSocial{ get; set; }
        public bool AporteFondo { get; set; }
        public bool ContribucionFondo { get; set; }
        public bool AporteRenatae { get; set; }
        public bool ContribucionRenatae { get; set; }
        public bool ContribucionAsigFamiliar { get; set; }
        public bool ContribucionFondoEmpleo { get; set; }
        public bool ContribucionRiesgoTrabajo { get; set; }
        public bool AporteRegDiferencial { get; set; }
        public bool AporteRegEspeciales{ get; set; }
        public string Observaciones { get; set; }
        public bool Seleccion { get; set; }
}
}
