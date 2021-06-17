using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class BasesDeDatos
    {
        public int IdTipoBD { get; set; }
        public int? IdBaseDeDatos { get; set; }
        public string Nombre { get; set; }
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
        public int IdentificadorEstatus { get; set; }
        public ML.TipoEncuesta TipoEncuesta { get; set; }
        public List<ML.TipoEncuesta> listTipoEncuesta { get; set; }
        public ML.Encuesta Encuesta { get; set; }
        public ML.TipoBD TipoBD { get; set; }
    }
}
