using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class CompanyCategoria
    {
        public int IdCompanyCategoria { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaHoraCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string ProgramaCreacion { get; set; }
        public DateTime FechaHoraModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string ProgramaModificacion { get; set; }
        public DateTime FechaHoraEliminacion { get; set; }
        public string UsuarioEliminacion { get; set; }
        public string ProgramaEliminacion { get; set; }
        public List<Object> ListPerfiles { get; set; }
        public List<Object> ListCompanyCategoria { get; set; }
    }
}
