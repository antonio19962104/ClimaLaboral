using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Respuestas 
    {
        public ML.PreguntasLikert PreguntasLikert { get; set; }
        public int IdPreguntaLikert { get; set; }
        public List<ML.PreguntasLikert> listPreguntasLikert { get; set; }
        public int IdRespuesta { get; set; }
        //[Required(ErrorMessage ="Este campo es requerido")]
        public string Respuesta { get; set; }
        //[Required(ErrorMessage = "Este campo es requerido")]
        public bool Selected { get; set;}
        public int Verdadero { get; set;}
        public int Falso { get; set;}
        public Selecccionado SelectedRadio { get; set;}
        public Preguntas Pregunta { get; set; }
        public TipoEstatus TipoEstatus { get; set; }
        public DateTime FechaHoraCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string ProgramaCreacion { get; set; }
        public DateTime FechaHoraModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string ProgramaModificacion { get; set; }
        public DateTime FechaHoraEliminacion { get; set; }
        public string UsuarioEliminacion { get; set; }
        public string ProgramaEliminacion { get; set; }
        public Guid UniqueId { get; set; }
        public IList<Respuestas> NewAnswers { get; set; }        
        public string IdPadreObjeto { get; set; }
        public ML.EmpleadoRespuesta EmpleadoRespuesta { get;set;}
        public ML.UsuarioRespuestas UsuarioRespuestas { get; set;}
        public enum  Selecccionado
        {
            
            Verdadero = 1,
             Falso =0
        }
        public int conteoByPregunta { get; set; }

    }
}
