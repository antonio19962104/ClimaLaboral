using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class UsuarioRespuestas
    {
        public int IdUsuarioRespuestas { get; set; }
        public Preguntas Preguntas { get; set; }
        public string RespuestaUsuario { get; set; }
        public ML.Encuesta Encuesta { get; set;}
        public ML.Respuestas Respuestas { get; set; }
        public ML.Usuario Usuario { get; set; }
        public DateTime? FechaHoraCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public ML.TipoEstatus TipoEstatus { get; set; }
        public string ProgramaCreacion { get; set; }
        public DateTime? FechaHoraModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string ProgramaModificacion { get; set; }
        public DateTime? FechaHoraEliminacion { get; set; }
        public string UsuarioEliminacion { get; set; }
        public string ProgramaEliminacion { get; set; }
    }
}
