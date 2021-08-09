using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Encuesta
    {
        public int Anio { get; set; }
        public int IdTipoEncuesta { get; set; }      
        public int Enviada { get; set; }
        public string TipoMensaje { get; set; }
        public string Mensaje { get; set; }
        public List<Object> Objetos { get; set; }
        public bool SeccionarEncuesta { get; set; }
        public int IdEncuesta { get; set; }
        public string UID { get; set;}
        [Required]
        public string Nombre { get; set; }
        [Required]
        public DateTime? FechaInicio { get; set; }
        [Required]
        public DateTime? FechaFin { get; set; }
        public bool? Estatus { get; set; }
        public bool DosColumnas { get; set; }
        public bool PreguntasCondicion { get; set;}
        public int DosColumnasN { get; set; }
        public int PreguntasCondicionN { get; set; }
        public TipoEstatus TipoEstatus { get; set; }
        public TipoOrden TipoOrden { get; set; }
        public List<TipoOrden> ListTipoOrden { get; set; }
        public Plantillas Plantillas { get; set;}
        public BasesDeDatos BasesDeDatos { get; set;}
        public Preguntas Preguntas { get; set;}
        public List<Object> ConfiguraRespuesta { get; set; }
        public string Descripcion { get; set;}
        public string CodeHTML { get; set;}
        public string Instruccion { get; set;}
        public string ImagenInstruccion { get; set; }
        public string TipoEncuesta { get; set;}
        public string Agradecimiento { get; set; }
        public string ImagenAgradecimiento { get; set;}
        public DateTime FechaHoraCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string ProgramaCreacion { get; set; }
        public DateTime FechaHoraModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string ProgramaModificacion { get; set; }
        public DateTime FechaHoraEliminacion { get; set; }
        public string UsuarioEliminacion { get; set; }
        public string ProgramaEliminacion { get; set; }
        public List<Object> ListCompetencias { get; set; }
        public List<Competencia> ListaEditCompetencia { get; set; }
        //public enum Seccion
        //{
        //    Seccion1,
        //    Seccion2,
        //    Seccion3,
        //    Seccion4,
        //    Seccion5,
        //    Seccion6,
        //    Seccion7,
        //    Seccion8
        //}
        public ML.Administrador Administrador { get; set; }
        public List<TipoEstatus> EstatusList { get; set; }
        public List<object> ListEmpresas { get; set; }
        public int? IdEmpresa { get; set; }
        public List<TipoEncuesta> ListTipoEncuesta { get; set; }
        public List<Plantillas> ListPlantillas { get; set; }
        public List<object> ListDataBase { get; set;}
        public List<object> ListDataBaseG { get; set; }
        public List<object> ListDataBaseC { get; set; }
        public List<object> ListEnfoquePregunta { get; set; }
        public List<object> ListTipoControl { get; set; }
        public List<object> ListPreguntas { get; set;}
        public IList<Preguntas> NewCuestion { get; set;}
        public IList<Preguntas> NewCuestionEdit { get; set; }
        public IList<ValoracionPreguntaPorSubcategoria> NewVPPSC { get; set;}
        public IList<ValoracionSubcategoriaPorCategoria> NewVSCPC { get; set; }
        public List<Preguntas> ListarPreguntas { get; set; }
        public List<Categoria> ListCategorias { get; set; }
        public List<Categoria> ListCategoriasLoad { get; set; }
        public ML.UsuarioRespuestas UsuarioRespuestas { get; set; }
        //cargar listado de Categorias de la configuracion existaente
        public List<Categoria> ListCatCol2 { get; set; }
        //cargar listado de SubCategorias de la configuracion existaente
        public List<Categoria> ListSubCatCol3 { get; set; }
        /// <summary>
        /// para editar requerimos el numero maximo de id padre, para hacer la continuidad si agregan una nueva pregunta
        /// </summary>
        public int idMaxPregunta {get; set;}

        //JAMG
        public ML.Company Company { get; set; }
        public ML.TipoEncuesta MLTipoEncuesta { get; set; }
        public ML.Usuario usuario { get; set; }

        //Clima
        public string Instrucciones1 { get; set; }
        public string Instrucciones2 { get; set; }
        public string cadenaInicio { get; set; }
        public string cadenaFin { get; set; }
        ///Listado nuevo de Encuestas CAMOS 20/07/2021
        public int periodo { get; set;}
        public ML.DashBoardEncuesta resumen { get; set; }
        public List<object> ListReportes { get; set; }
    }
}
