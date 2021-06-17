using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class EncuestaArea
    {
        public int IdEncuestaArea { get; set; }
        public ML.Area Area { get; set; }
        public ML.Encuesta Encuesta { get; set; }
        public DateTime FechaHoraCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string ProgramaCreacion { get; set; }
        public DateTime FechaHoraModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string ProgramaModificacion { get; set; }
        public DateTime FechaHoraEliminacion { get; set; }
        public string UsuarioEliminacion { get; set; }
        public string ProgramaEliminacion { get; set; }
        public List<Object> ConfiguraRespuesta { get; set; }
        public IList<Preguntas> NewCuestionEdit { get; set; }
    }
}
