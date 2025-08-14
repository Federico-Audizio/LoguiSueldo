using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;

namespace LoguiSueldo.Models
{
    public class Empleado
    {
        public int PersonaID { get; set; }
        public int ConvenioID { get; set; }
        public int LocalidadID { get; set; }
        public int ObraSocialID { get; set; }
        public int CategoriaID { get; set; }
        public int SistJubilID { get; set; }
        public int ArtID { get; set; }
        public int ModalidadID { get; set; }
        public int EmpresaID { get; set; }
        public string Legajo {  get; set; }
        public string Nombre { get; set; }
        public string Domicilio { get; set; }
        public int Cuil {  get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaLiquidacion { get; set; }
        public float Basico { get; set; }
        public int Tipo { get; set; }
        public int Estado { get; set; }
        public DateTime FechaBaja { get; set; }
        public float MontoUltimoRecibo { get; set; }
        public string UltimoPeriodoLiquidado { get; set; }
        public float CantidadHoras { get; set; }
        public float PrecioHoras { get; set; }
        public int AnioAntiguedad { get; set; }
        public float PorcentajeAntiguedad { get; set; }
        public float PorcentajeJubilacion { get; set; }
        public float PorcentajeObraSocial { get; set; }
        public float MontoHoraExtra { get; set; }
        public int ModalidadLiquidacion { get; set; }
        public float MontoHorasFeriado { get; set; }
        public float MontoHorasFeriado_tjdo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Nacionalidad { get; set; }
        public DateTime FechaAltaRecibo { get; set; }
        public float Extra1 { get; set; }
        public float Extra2 { get; set; }
        public float Extra3 { get; set; }
        public float Extra4 { get; set; }
        public float Extra5 { get; set; }
        public float Extra6 { get; set; }
        public float HorasEnfAcc {  get; set; }
        public int Conyugue {  get; set; }
        public int Hijos {  get; set; }
        public int Cct {  get; set; }
        public int Reduccion { get; set; }
        public int CodigoSituacion { get; set; }
        public int CodigoCondicion { get; set; }
        public int CodigoActividad { get; set; }
        public int CodigoSiniestrado { get; set; }
        public int CodigoContratacion { get; set; }
        public int CodigoLocalidad { get; set; }
        public int DiasTrabajados { get; set; }

    }
}
