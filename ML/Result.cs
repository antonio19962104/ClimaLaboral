using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Result
    {
        public double AvanceDouble { get; set; }
        public int IdusuarioForAnonima { get; set; }
        public int IsSuperAdmin { get; set; }
        public int IdentificadorArea { get; set; }
        public string CompanyDelAdmin { get; set; }
        public DateTime InicioCL { get; set; }
        public DateTime FinCL { get; set; }
        public System.Data.DataSet DataSet { get; set; }
        public bool IsContestada { get; set; }
        public Object DataColors { get; set; }
        public int IdEmpleadoFromSP { get; set; }
        public List<ML.Preguntas> ListadoPreguntas { get; set; }
        public int CURRENT_IDEMPLEADOLOG { get; set; }
        public List<object> ListCompanyDelete { get; set; }
        public List<object> ListAreaDelete { get; set; }
        public List<object> ListDepartamentoDelete { get; set; }
        public int UltimoAdminInsertado { get; set; }
        public bool IsMaster { get; set; }
        public bool Correct { get; set; }
        public bool Exist { get; set; }
        public bool EncuestaActiva { get; set; }
        public Object Object { get; set; }
        public Object ObjectAux { get; set; }
        public List<Object> Objects { get; set; }
        public List<object> ObjectsAux { get; set; }
        public List<Object> ObjectsPermisos { get; set; }
        public Exception ex { get; set; }
        public string DefUsername { get; set; }
        public string ErrorMessage { get; set; }
        public List<EmpleadoRespuesta> answer { get; set; }
        public ML.Plantillas EditaPlantillas { get; set; }
        public Encuesta EditaEncuesta { get; set;}
        public List<Aling> Aling { get; set; }
        public int Avance { get; set; }
        public string DefPass { get; set; }
        public string ParteGuardada { get; set; }
        public List<EmpleadoRespuesta> EmpleadoRes { get; set; }
        public List<TipoEstatus> ListadoTipoEstatus { get; set; }
        public List<HeaderPlantilla> ListadoHeadersPlantilla { get; set; }
        public List<FooterPlantilla> ListadoFooterPlantilla { get; set; }
        public List<DetallePlantilla> ListadoDetallePlantilla { get; set;}
        public string HtmlPlantilla { get; set;}
        public ML.CompanyCategoria CompanyCategoria { get; set; }
        public List<TipoEncuesta> ListadoTipoEncuesta { get; set; }
        //Modelado para la vista de reporte
        public ML.Empleado Empleado { get; set; }       
        public List<string> PerfilesList { get; set; }
        public string CURRENT_USER { get; set; }
        public List<object> ListadoDeBaseDeDatos { get; set; }
        public List<object> CompanyCategoriaList { get; set; }
        public int ExisteArea { get; set; }
        public int ExisteEmpresa { get; set; }
        public List<Plantillas> ListadoDePlantillasPredefinidas { get; set; }  
        public List<Plantillas> ListadoDePlantillasPorUsuario { get; set; }
        public List<Plantillas> ListadoDePlantillasUltimas { get; set; }     
        public List<object> ListadoEnfoquesPregunta { get; set; }
        public List<object> ListadoCompetenciasPregunta { get; set; }
        public List<Competencia> ListCompetenciasEdit { get; set; }
        public List<Preguntas> ListadoPreguntaCompetencias { get; set; }
        public List<object> ListadoTipoControl { get; set; }
        public List<Encuesta> ListadoDeEncuestas { get; set; }
        public List<Plantillas> ListadoDePlantillas { get; set; }
        public ML.Preguntas Pregunta { get; set; }

        public int IdCompanyCategoria { get; set; }
        public List<CompanyCategoria> ListCompanyCategoria { get; set; }
        public List<TipoPlantilla> ListTipoPlantilla { get; set; }
        public int CURRENTIDADMINLOG { get; set; }
        public string tableBody { get; set; }
        public int idTipoEncuesta { get; set; }
        public int idEncuestaAlta { get; set; }
        public List<TipoOrden> ListTipoOrden { get; set; }
    }
}
