using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Preguntas
    {
        public int IdPreguntaPadre { get; set; }
        public int noReactivosEE { get; set; }
        public int IdCompetencia { get; set; }
        public int IdTipoOrden { get; set; } = 0;
        public int IdEnfoque { get; set; }
        public int IdOrden { get; set; }
        public int CSF { get; set; }
        public int FF { get; set; }
        public int AV { get; set; }
        public int FV { get; set; }
        public int CSV { get; set; }
        public int Acceso { get; set; }
        public int SubSeccion { get; set; }
        public ML.Respuestas MLRespuestas { get; set; }
        public int IdentificadorTipoControl { get; set; }
        public List<UsuarioRespuestas> listUsuarioResp { get; set; }
        public PreguntasLikert PreguntasLikert { get; set; }
        public List<ML.PreguntasLikert> ListPreguntasLikert { get; set; }
        public int Seccion { get; set; }
        public string Encabezado{ get; set; }
        public int IdPregunta { get; set; }
        public int IdEncuesta { get; set; }        
        public string Pregunta { get; set; }
        public int NumeroPregunta { get; set;}
        public decimal? Valoracion { get; set; }
        //public string TipoControl { get; set; }
        public TipoControl TipoControl { get; set; }
        public TipoEstatus TipoEstatus { get; set;}
        public string Enfoque { get; set;}       
        public string RespuestaCondicion { get; set;}
        public string PreguntasCondicion { get; set;}
        public bool Obligatoria { get; set; }
        public Competencia Competencia { get; set; }        
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
        public List<object> ListadoRespuestas { get; set; }
        public List<object> ListTipoControl { get; set; }
        public List<object> ListCompetencia { get; set; }
        public List<object> ListEnfoque { get; set; }
        public IList<Respuestas> NewAnswer { get; set; }
        public IList<Categoria> NewCat { get; set; }
        public IList<Respuestas> NewAnswerEdit { get; set; }
        public List<Respuestas> Respuestas { get; set; }
        public bool isDisplay { get; set; }
        public List<Categoria> Subcategorias { get; set; }

        //Data for report
        public double TotalPreguntas { get; set; }
        public double TotalRespuestasFavorables { get; set; }
        public double Porcentaje { get; set; }

    }
    
}
