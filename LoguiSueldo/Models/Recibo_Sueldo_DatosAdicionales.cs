using System.ComponentModel.DataAnnotations;

namespace LoguiSueldo.Models
{
    public class Recibo_Sueldo_DatosAdicionales
    {
        public int ReciboID { get; set; }
        public int EmpleadoID { get; set; }
        public float RemuneracionBruta { get; set; }
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
        public float BaseDifAportes_ss {  get; set; }
        public float BaseDifContribucion_ss { get; set; }
        public float ImporteDetraer {  get; set; }
        public int PeriodoMes {  get; set; }
        public int PeriodoAño { get; set; }

    }
}
