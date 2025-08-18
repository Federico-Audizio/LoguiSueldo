using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoguiSueldo.Models
{
    public class Permiso
    {
        [Key]
        public int PermisoID { get; set; }

        public string PermisoNombre { get; set; }

        public virtual ICollection<PermisoUsuario> PermisoUsuarios { get; set; }
    }
}