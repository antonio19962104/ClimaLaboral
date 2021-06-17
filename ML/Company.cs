using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Company
    {
        public string CURRENT_USER { get; set; }
        public int? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public ML.CompanyCategoria CompanyCategoria { get; set; }
        public ML.TipoEstatus TipoEstatus { get; set; }
        public DateTime FechaHoraCreacion { get; set; }
        public int? IdentificadorEstatus { get; set; }
        public string UsuarioCreacion { get; set; }
        public string ProgramaCreacion { get; set; }
        public DateTime FechaHoraModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string ProgramaModificacion { get; set; }
        public DateTime FechaHoraEliminacion { get; set; }
        public string UsuarioEliminacion { get; set; }
        public string ProgramaEliminacion { get; set; }
        public string LogoEmpresa { get; set; }
        public string Color { get; set; }
        public string TipoEmpresa { get; set; }
        public Area Area { get; set; }
    }
}
