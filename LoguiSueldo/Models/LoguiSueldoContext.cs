using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace LoguiSueldo.Models
{
    public class LoguiSueldoContext : DbContext
    {
        public LoguiSueldoContext() : base("name=LoguiSueldoContext")
        {
        }

        //ADMINISTRACION
        //public System.Data.Entity.DbSet<LoguiSueldo.Models.Administracion.ClientesLogui> ClientesLoguiSoft { get; set; }

        public System.Data.Entity.DbSet<LoguiSueldo.Models.Persona> Personas { get; set; }

        public System.Data.Entity.DbSet<LoguiSueldo.Models.Permiso> Permisos { get; set; }

        public System.Data.Entity.DbSet<LoguiSueldo.Models.PermisoUsuario> PermisoUsuarios { get; set; }

        public System.Data.Entity.DbSet<LoguiSueldo.Models.Empleado> Empleados { get; set; }

        public System.Data.Entity.DbSet<LoguiSueldo.Models.Empleado_Adicional> Empleados_Adicional { get; set; }

        public System.Data.Entity.DbSet<LoguiSueldo.Models.Empresa> Empresas { get; set; }

        public System.Data.Entity.DbSet<LoguiSueldo.Models.Pais> Pais { get; set; }

        public System.Data.Entity.DbSet<LoguiSueldo.Models.Provincia> Provincias { get; set; }

        public System.Data.Entity.DbSet<LoguiSueldo.Models.Localidad> Localidades { get; set; }

        public System.Data.Entity.DbSet<LoguiSueldo.Models.Categoria> Categorias { get; set; }

        //Informacion necesaria

        public System.Data.Entity.DbSet<LoguiSueldo.Models.ART> ARTs { get; set; }

        public System.Data.Entity.DbSet<LoguiSueldo.Models.Concepto_Afip> ConceptosAfip { get; set; }

        public System.Data.Entity.DbSet<LoguiSueldo.Models.Conceptos> Conceptos { get; set; }

        public System.Data.Entity.DbSet<LoguiSueldo.Models.Convenio> Convenios{ get; set; }

        public System.Data.Entity.DbSet<LoguiSueldo.Models.Antiguedad_Convenio> AntiguedadConvenios { get; set; }

        public System.Data.Entity.DbSet<LoguiSueldo.Models.Horas_empleados> Horas_Empleados{ get; set; }

        public System.Data.Entity.DbSet<LoguiSueldo.Models.Leyes> Leyes{ get; set; }

        public System.Data.Entity.DbSet<LoguiSueldo.Models.Sist_jubil> SistJubilatorios { get; set; }

        public System.Data.Entity.DbSet<LoguiSueldo.Models.Modalidad> Modalidades{ get; set; }

        public System.Data.Entity.DbSet<LoguiSueldo.Models.Obra_social> ObrasSociales{ get; set; }

        //Liquidacion
        public System.Data.Entity.DbSet<LoguiSueldo.Models.Libro_sueldo> LibroSueldos { get; set; }

        public System.Data.Entity.DbSet<LoguiSueldo.Models.Libro_sueldo_adicional> LibroSueldoAdicionales { get; set; }

        public System.Data.Entity.DbSet<LoguiSueldo.Models.Plantilla> Plantillas{ get; set; }

        public System.Data.Entity.DbSet<LoguiSueldo.Models.Plantilla_Detalle> PlantillaDetalles{ get; set; }

        public System.Data.Entity.DbSet<LoguiSueldo.Models.Plantilla_empleado> PlantillaEmpleados{ get; set; }

        public System.Data.Entity.DbSet<LoguiSueldo.Models.Recibo> Recibos{ get; set; }

        public System.Data.Entity.DbSet<LoguiSueldo.Models.Recibo_Sueldo_DatosAdicionales> Recibo_Sueldos{ get; set; }

    }
}
