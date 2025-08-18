using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LoguiSueldo.Models
{
    public class Persona
    {
        [Key]
        [Display(Name = "Persona")]
        public int PersonaID { get; set; }

        [Display(Name = "Nombre y Apellido / Razón Social")]
        [Required(ErrorMessage = "Debe ingresar un {0}")]
        [StringLength(150, ErrorMessage = "El {0} debe tener entre como máximo {1} caracteres.")]
        public string NombreCompleto { get; set; }

        [Display(Name = "Nombre de Fantasía")]
        [StringLength(150, ErrorMessage = "El {0} debe tener entre como máximo {1} caracteres.")]
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
        public string NroTipoDocumento { get; set; }

        [Display(Name = "Teléfono 1")]
        public string Telefono1 { get; set; }

        [Display(Name = "Correo Electrónico")]
        public string CorreoElectronico { get; set; }

        public bool PersonaConsumidorFinal { get; set; }

        public bool Eliminado { get; set; }

        public int EmpresaID { get; set; }

        public virtual Localidad Localidades { get; set; }
        public virtual TipoContribuyente TipoContribuyentes { get; set; }
        public virtual TipoDocumento TipoDocumentos { get; set; }

        [NotMapped]
        public int CabeceraPersonaId { get; set; }

        [NotMapped]
        public string NombreFinal { get { if (NombreFantasia != "" && NombreFantasia != null) { return NombreFantasia; } else { return NombreCompleto; } } }

    }

    public class ListadoPersonas
    {
        public int Orden { get; set; }
        public int PersonaID { get; set; }
        public string NombreCompleto { get; set; }
        public string NombreFantasia { get; set; }
        public string DireccionReal { get; set; }
        public int LocalidadID { get; set; }
        public string LocalidadNombre { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public int TipoContribuyenteID { get; set; }
        public string TipoContribuyenteNombre { get; set; }
        public int TipoDocumentoID { get; set; }
        public string TipoDocumentoNombre { get; set; }
        public string NroTipoDocumento { get; set; }

        public DateTime FechaUltimoMovimiento { get; set; }
        public string FechaUltMov { get; set; }

        //Datos del usuario relacionado a la persona
        public string UsuarioID { get; set; }
        public string Email { get; set; }
        public int PermisoID { get; set; }
        public string PermisoNombre { get; set; }
        public string Contraseña { get; set; }
        public int Resultado { get; set; }
        public bool Editar { get; set; }
        public bool Eliminar { get; set; }
        public bool Desvinculado { get; set; }
    }

    public class ListadoPersonasExcel
    {
        public string NombreCompleto { get; set; }
        public string NombreFantasia { get; set; }
        public string TipoContribuyenteNombre { get; set; }
        public string NroTipoDocumento { get; set; }
        public string Direccion { get; set; }
        public string Localidad { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
    }
}