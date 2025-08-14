namespace LoguiSueldo.Models
{
    public class Empleado_Adicional
    {
        public int PersonaID { get; set; }
        public int CodigoSiniestro1 { get; set; }
        public int DiaSiniestro1 { get; set; }
        public int CodigoSiniestro2 { get; set; }
        public int DiaSiniestro2 { get; set; }
        public int CodigoSiniestro3 { get; set; }
        public int DiaSiniestro3 { get; set; }
        public float AporteAdicional_ss { get; set; }
        public float ContribucionAdicional { get; set; }
        public float AporteAdicionalObraSocial { get; set; }
        public float ContribucionAdicionalObraSocial { get; set; }
        public float BaseDiferencialAporte_os_fsr { get; set; }
        public float BaseDiferencialContribucion_os_fsr { get; set; }
        public float BaseDiferencial_lrt {  get; set; }
        public float MaternidadAnses {  get; set; }
        public bool MediaJornada { get; set; }
        public int TipoEmpleador { get; set; }
    }
}
