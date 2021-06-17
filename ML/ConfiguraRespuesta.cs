using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class ConfiguraRespuesta
    {
        public int TerminaEncuesta { get; set; }
        public int IdRespuestaTermina { get; set; }
        public string itemPreguntaOpen { get; set; }
        public int CondicionTermina { get; set; }
        public int IdConfiguraRespuesta { get; set; }       
        public int IdEncuesta { get; set; }
        public int IdPregunta { get; set; }
        public int IdRespuesta { get; set; }
        public int IdPreguntaOpen { get; set; }
        public DateTime FechaHoraCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string ProgramaCreacion { get; set; }
        public DateTime FechaHoraModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string ProgramaModificacion { get; set; }
        public string FechaHoraEliminacion { get; set; }
        public string UsuarioEliminacion { get; set; }
        public string ProgramaEliminacion { get; set; }

        public Encuesta Encuesta { get; set; }
        public Preguntas Preguntas { get; set; }
        public Respuestas Respuestas { get; set; }
        public List<int> ListPreguntas { get; set; }
    }
}
