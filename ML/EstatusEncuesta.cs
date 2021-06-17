using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class EstatusEncuesta
    {
        public int IdEstatusEncuesta { get; set; }
        public string Estatus { get; set; }
        public ML.Encuesta Encuesta { get; set; }
        public ML.Empleado Empleado { get; set; }

        public DateTime FechaHoraCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string ProgramaCreacion { get; set; }
        public DateTime FechaHoraModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string ProgramaModificacion { get; set; }
    }
}
