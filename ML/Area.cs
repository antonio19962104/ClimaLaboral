using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Area
    {
        public int CompanyId { get; set; }
        public int IdArea { get; set; }
        public string Nombre { get; set; }
        public int IdentitificadorEstatus { get; set; }
        public ML.Company Company { get; set; }
        public DateTime FechaHoraCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string ProgramaCreacion { get; set; }
        public DateTime FechaHoraModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string ProgramaModificacion { get; set; }
        public DateTime FechaHoraEliminacion { get; set; }
        public string UsuarioEliminacion { get; set; }
        public string ProgramaEliminacion { get; set; }
        public ML.TipoEstatus TipoEstatus { get; set; }
        public string CURRENT_USER { get; set; }
        public Departamento Departamento { get; set; }

    }
}
