using LoguiSueldo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LoguiSueldo.Models
{
    public class Provincia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Provincia")]
        public int ProvinciaID { get; set; }

        [Display(Name = "Provincia")]
        [Required(ErrorMessage = "Debe ingresar una {0}")]
        public string ProvinciaNombre { get; set; }

        public int PaisID { get; set; }

        public int EmpresaID { get; set; }//06/08/2018 //0 o EmpresaID actual

        public virtual Pais Paises { get; set; }

        public virtual ICollection<Localidad> Localidad { get; set; }
    }

    public class ListadoProvinciasLocalidades
    {
        public int ProvinciaID { get; set; }

        public string ProvinciaNombre { get; set; }

        public int EmpresaID { get; set; }

        public List<ListadoLocalidades> ListadoLocalidades { get; set; }
    }
}