using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Periodicidad
    {
        public int IdPeriodicidad { get; set; }
        public string Descripcion { get; set; } = String.Empty;
        public DateTime FechaHoraCreacion { get; set; } = DateTime.MinValue;
        public string UsuarioCreacion { get; set; } = String.Empty;
        public string ProgramaCreacion { get; set; } = String.Empty;
        public DateTime FechaHoraModificacion { get; set; } = DateTime.MinValue;
        public string UsuarioModificacion { get; set; } = String.Empty;
        public string ProgramaModificacion { get; set; } = String.Empty;
        public DateTime FechaHoraEliminacion { get; set; } = DateTime.MinValue;
        public string UsuarioEliminacion { get; set; } = String.Empty;
        public string ProgramaEliminacion { get; set; } = String.Empty;
    }
}
