using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;


namespace LoguiSueldo.Models
{
    public class Conceptos
    {
        [Key, Required]
        public int ConceptoID { get; set; }
        public int ConvenioID { get; set; }
        public string Descripcion { get; set; }
        public string NombreVariable { get; set; }
        public string Formula { get; set; }
        public int Nivel { get; set; }
        public int Asiento { get; set; }
        public int Bloque { get; set; }
        public Boolean Inamobible { get; set; }
        public Boolean Remunerable { get; set; }
        public Boolean sel { get; set; }
        public float Monto { get; set; }
    }
}
