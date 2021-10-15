using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class ReporteD4U
    {
        [Key]
        public int id { get; set; }
        public int IdBD { get; set; }
        public string criterioBusquedaSeleccionado { get; set; }
        public string filtro { get; set; }
        public string filtroValor { get; set; }
        public string filtroEntidadAFM { get; set; }
        public string valorEntidadAFM { get; set; }
        public int Anio { get; set; }
        public enum EnfoqueSeleccionado
        {
            EnfoqueEmpresa = 1,
            EnfoqueArea = 2
        }
       

        public int noColumnas { get; set; }
        public string tableHTML { get; set; }
        public int IdPregunta { get; set; }
        public string UnidadNegocioFilter { get; set; } = "";
        public string Filtrosss { get; set; }
        public int IdReporte { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string location { get; set; }
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
        public int TotalPreguntas { get; set; }
        public int TotalRespuestasFavorables { get; set; }
        public decimal Porcentaje { get; set; }

        //Aux
        public List<object> Companies { get; set; }
        public List<object> Areas { get; set; }
        public List<object> Departamentos { get; set; }
        public List<object> Sundepartamentos { get; set; }
        //Final Aux
        public List<ML.Company> CompaniesList { get; set; }
        public List<Area> AreasList { get; set; }
        public List<Departamento> Departamento { get; set; }
        public List<Subdepartamento> Subdepartamento { get; set; }
        public bool Correct { get; set; }
        public string ErrorMessage { get; set; }
        public List<string> ListFiltros { get; set; }
        public List<double> ResultadosForMerge { get; set; }
        public string Enfoque { get; set; }
    }
}
