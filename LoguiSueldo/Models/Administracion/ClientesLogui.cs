using LoguiSueldo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoguiSueldo.Models.Administracion
{
    public class ClientesLogui
    {
        [Key]
        public int ClienteID { get; set; }

        [Display(Name = "Nombre y Apellido / Razón Social")]
        [Required(ErrorMessage = "Debe ingresar un {0}")]
        [StringLength(150, ErrorMessage = "El {0} debe tener entre como máximo {1} caracteres.")]
        public string RazonSocial { get; set; }

        [Display(Name = "Teléfono")]
        [Required(ErrorMessage = "Debe ingresar un {0}")]
        [StringLength(20, ErrorMessage = "El {0} deben tener como máximo {1} caracteres.")]
        public string Telefono { get; set; }

        [Display(Name = "Domicilio y Localidad")]
        [StringLength(100, ErrorMessage = "El {0} deben tener como máximo {1} caracteres.")]
        public string Domicilio { get; set; }

        public int TipoContribuyenteID { get; set; }

        public int TipoDocumentoID { get; set; }

        [Display(Name = "Nro. de Tipo Documento")]
        [Required(ErrorMessage = "Debe ingresar un {0}")]
        public string NroTipoDocumento { get; set; }

        public string UsuarioID { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaAlta { get; set; }

        public DateTime FechaBaja { get; set; }

        //public virtual TipoContribuyente TipoContribuyentes { get; set; }
        //public virtual TipoDocumento TipoDocumentos { get; set; }
    }


    public class ListadoClientesLogui
    {
        public int ClienteID { get; set; }
        public string RazonSocial { get; set; }
        public string UsuarioID { get; set; }
        public string Email { get; set; }
        public bool Activo { get; set; }

        public DateTime FechaAlta { get; set; }//FECHA REGISTRO DEL CLIENTE
        public string FechaAltaString { get; set; }//FECHA REGISTRO DEL CLIENTE
        public DateTime FechaBaja { get; set; }//FECHA DESACTIVACION DEL CLIENTE

        public int CantidadEmpresasCreadas { get; set; }

        public int TipoDocumentoID { get; set; }
        public string TipoDocumentoNombre { get; set; }
        public string NroDocumento { get; set; }
        public string Domicilio { get; set; }
        public string Telefono { get; set; }
        public int TipoContribuyenteID { get; set; }
        public string TipoContribuyenteNombre { get; set; }
    }
}