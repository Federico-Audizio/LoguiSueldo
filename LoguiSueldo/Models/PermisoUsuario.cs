using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoguiSueldo.Models
{
    public class PermisoUsuario
    {
        [Key]
        public int PermisoUsuarioID { get; set; }

        public string UsuarioID { get; set; }

        public int PersonaID { get; set; }

        public int PermisoID { get; set; }

        public int EmpresaID { get; set; }

        public string UsuarioCarga { get; set; }

        public int Digitos { get; set; }

        public string CodigoSeguridad { get; set; }

        public int SolicitarCodigo { get; set; }

        public bool Desvinculado { get; set; }

        public virtual Permiso Permiso { get; set; }

        public virtual Empresa Empresa { get; set; }
    }

    public class ListadoEmpresaUsuario
    {
        public int EmpresaID { get; set; }

        public string RazonSocial { get; set; }

        public string NombreFantasia { get; set; }

        public int TipoContribuyenteID { get; set; }

        public string TipoContribuyenteNombre { get; set; }

        public string TipoDocumentoNombre { get; set; }

        public string NroTipoDocumento { get; set; }

        public string EstadoEmpresa { get; set; }

        public int PermisoID { get; set; }

        public string PermisoNombre { get; set; }

        public string Email { get; set; }

        public int CantUsuVendedores { get; set; }
        public int CantUsuAdministracion { get; set; }
        public int CantUsuSuperUsuarios { get; set; }

        public List<ListadoPersonas> PersonasEmpresa { get; set; }
    }

    public class ListadoEmpresasUsuarioTitular
    {
        public string UsuarioTitular { get; set; }
        public string PersonaNombre { get; set; }
        public List<ListadoEmpresaUsuario> ListadoEmpresaUsuario { get; set; }
    }
}