using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class UsuarioEstatusEncuesta
    {
        public int IdUsuarioEstatusEncuesta { get; set; }
        public ML.Usuario Usuario { get; set; }
        public ML.Encuesta Encuesta { get; set; }
        public ML.EstatusEncuestaD4U EstatusEncuestaD4U { get; set; }
        public DateTime FechaHoraCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string ProgramaCreacion { get; set; }
        public DateTime FechaHoraModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string ProgramaModificacion { get; set; }
        public DateTime FechaHoraEliminacion { get; set; }
        public string UsuarioEliminacion { get; set; }
        public string ProgramaEliminacion { get; set; }

        public int NoIniciadas { get; set; }
        public int Iniciadas { get; set; }
        public int Terminadas { get; set; }
        public int Esperadas { get; set; }

    }
}
