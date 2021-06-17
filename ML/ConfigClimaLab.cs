using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class ConfigClimaLab
    {
        public int PeriodoAplicacion { get; set; }
        public string TipoMensaje { get; set; }
        public string Mensaje { get; set; }
        public string nameBD { get; set; }
        public int IdDatabase { get; set; }
        public int IdConfigurtacion { get; set; }
        public ML.Encuesta Encuesta { get; set; }
        public ML.BasesDeDatos BaseDeDatos { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime FechaHoraCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string ProgramaCreacion { get; set; }
        public DateTime FechaHoraModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string ProgramaModificacion { get; set; }
        public DateTime FechaHoraEliminacion { get; set; }
        public string UsuarioEliminacion { get; set; }
        public string ProgramaEliminacion { get; set; }
        public string InicioEncuesta { get; set; }
        public string FinEncuesta { get; set; }
    }
}
