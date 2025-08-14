using System.ComponentModel.DataAnnotations;

namespace LoguiSueldo.Models
{
    public class Libro_sueldo_adicional
    {
        public int PersonaID { get; set; }
        public int LibroSueldoID { get; set; }
        public int ReciboID { get; set; }
        public int Cuil { get; set; }
        public int Conyugue { get; set; }
        public int Hijos { get; set; }
        public int Cct { get; set; }
        public int Svco { get; set; }
        public int Reduccion { get; set; }
        public int TipoEmpresa { get; set; }
        public int CodSituacion { get; set; }
        public int CodCondicion { get; set; }
        public int CodActividad { get; set;}
        public int CodSiniestrado { get; set; }
        public int CodContratacion { get; set; }
        public int CodLocalidad { get; set; }
        public int DiasTrabajados { get; set; }
        public float HorasTrabajadas { get; set; }
        public int CantidadAdherentes { get; set; }
        public int CodigoObraSocial { get; set; }
        public int CodigoSiniestro1 { get; set;}
        public int DiaSiniestro1 { get; set; }
        public int CodigoSiniestro2 { get; set; }
        public int DiaSiniestro2 { get; set; }
        public int CodigoSiniestro3 { get; set; }
        public int DiaSiniestro3 { get; set; }
        public float AporteAdicional_ss {  get; set; }
        public float ContribucionAdicional { get; set; }
        public float BaseDifAporte_os_fsr { get; set; }
        public float BaseDifCont_os_fsr { get; set; }
        public float BaseDif_lrt { get; set; }
        public float MaternidadAnses { get; set; }
        public float RemuneracionBruto { get; set; }
        public float BaseImponible1 { get; set; }
        public float BaseImponible2 { get; set; }
        public float BaseImponible3 { get; set; }
        public float BaseImponible4 { get; set; }
        public float BaseImponible5 { get; set; }
        public float BaseImponible6 { get; set; }
        public float BaseImponible7 { get; set; }
        public float BaseImponible8 { get; set; }
        public float BaseImponible9 { get; set; }
        public float BaseImponible10 { get; set; }
        public float BaseDifAportes_ss { get; set; }
        public float BaseDifContribucion_ss { get; set; }
        public float ImporteDetraer { get; set; }
    }
}
