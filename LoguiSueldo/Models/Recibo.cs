using System.ComponentModel.DataAnnotations;

namespace LoguiSueldo.Models
{
    public class Recibo
    {
        [Key, Required]
        public int ReciboID { get; set; }
        public int EmpleadoID { get; set; }
        public int PlantillaID { get; set; }
        public int EmpresaID { get; set; }
        public int PeriodoMes {  get; set; }
        public int PeriodoAño { get; set; }
        public int Tipo {  get; set; }
        public float TotalRemunerativo { get; set; }
        public float TotalRemunerativoSinExtra { get; set; }
        public float TotalDescuento { get; set; }
        public float TotalExtras { get; set; }
        public float TotalBolsillo { get; set; }
        public string LugarPago { get; set; }
        public DateTime FechaPago { get; set; }
        public string Mes {  get; set; }
        public string Banco {  get; set; }
        public string Sucursal { get; set; }
        public DateTime FechaDeposito { get; set; }
        public string NombrePeriodo { get; set; }
        public string MontoLetras { get; set; }
        //public string EmpleadoNombre {  get; set; }
        public string TipoReciboDetalle { get; set; }
        //public string EmpleadoLegajo { get; set; }
        //public string EmpleadoDomicilio { get; set; }
        //public string EmpleadoLocalidad { get; set; }
        public int EmpleadoCuit {  get; set; }
        public DateTime EmpleadoFechaAlta { get; set; }
        public string EmpleadoCategoria { get; set; }
        //public string EmpleadoNacimiento { get; set; }
        //public string EmpleadoCCT {  get; set; }
        public int FormaPago { get; set;}
        public float DiasLiquidado { get; set; }
        public string Cbu {  get; set; }
    }
}
