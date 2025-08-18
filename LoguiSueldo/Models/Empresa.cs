using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LoguiSueldo.Models
{
    public class Empresa
    {
        [Key]
        [Display(Name = "Persona")]
        public int EmpresaID { get; set; }

        public int EmpresaIDLoguiGestion { get; set; }

        [Display(Name = "Nombre y Apellido / Razón Social")]
        [Required(ErrorMessage = "Debe ingresar un {0}")]
        [StringLength(150, ErrorMessage = "El {0} debe tener entre como máximo {1} caracteres.")]
        public string RazonSocial { get; set; }

        [Display(Name = "Nombre de Fantasía")]
        [StringLength(50, ErrorMessage = "El {0} debe tener entre como máximo {1} caracteres.")]
        public string NombreFantasia { get; set; }

        [Display(Name = "Dirección Real")]
        [StringLength(80, ErrorMessage = "La {0} debe tener entre como máximo {1} caracteres.")]
        public string DireccionReal { get; set; }

        [Display(Name = "Localidad")]
        public int LocalidadID { get; set; }

        [Display(Name = "Tipo de Contribuyente")]
        public int TipoContribuyenteID { get; set; }

        [Display(Name = "Tipo de Documento")]
        public int TipoDocumentoID { get; set; }

        [Display(Name = "Nº Documento / CUIT")]
        [Required(ErrorMessage = "Debe ingresar un {0}")]
        public string NroTipoDocumento { get; set; }

        [Display(Name = "Teléfono")]
        public string Telefono1 { get; set; }

        [Display(Name = "Correo Electrónico")]
        public string CorreoElectronico { get; set; }

        public DateTime Cargado { get; set; }

        public string UsuarioTitular { get; set; }

        public bool AgenteRetencionGanancias { get; set; }

        //RELACIONES VIRTUALES
        public virtual Localidad Localidades { get; set; }
        //public virtual TipoContribuyente TipoContribuyentes { get; set; }
        //public virtual TipoDocumento TipoDocumentos { get; set; }

        public virtual ICollection<PermisoUsuario> PermisoUsuarios { get; set; }

        [NotMapped]
        public string NombreFinal { get { if (NombreFantasia != "" && NombreFantasia != null) { return NombreFantasia; } else { return RazonSocial; } } }
    }

    public class ComboGeneral
    {
        public int ID { get; set; }

        public required string Descripcion { get; set; }
    }

    public class ZonaHorariaServidor
    {
        public int HorasRestar { get { return 0; } }//0 SERVIDOR LOCAL O COOP, -3 SERVIDOR AZURE.
    }
}