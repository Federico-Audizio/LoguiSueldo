using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using LoguiSueldo.Models;

namespace LoguiSueldo.Models
{
    public class Localidad
    {
        [Key]
        [Display(Name = "Localidad")]
        public int LocalidadID { get; set; }

        [Display(Name = "Localidad")]
        [Required(ErrorMessage = "Debe ingresar una {0}")]
        public string LocalidadNombre { get; set; }

        [Display(Name = "Código Postal")]
        public string CodigoPostal { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una Provincia")]
        public int ProvinciaID { get; set; }

        public int EmpresaID { get; set; }//EmpresaID = 0 (CORDOBA CAPITAL) o EmpresaID = actual

        [NotMapped]
        public string NombreVista { get { return LocalidadNombre + " / " + Provincias.ProvinciaNombre.ToUpper() + " / " + Provincias.Paises.PaisNombre; } }

        public virtual Provincia Provincias { get; set; }

        public virtual ICollection<Persona> Persona { get; set; }

        public virtual ICollection<Empresa> Empresa { get; set; }
    }

    public class ListadoLocalidades
    {
        public int LocalidadID { get; set; }

        public string NombreVista { get; set; }
    }

    public class LocalidadDetalles
    {
        public int LocalidadID { get; set; }

        public string LocalidadNombre { get; set; }
        
        public string CodigoPostal { get; set; }

        public int ProvinciaID { get; set; }

        public string ProvinciaNombre { get; set; }

        public int PaisID { get; set; }
    }
}