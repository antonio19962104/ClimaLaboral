using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class PreguntaCategorias
    {
        public int IdPreguntaCategorias { get; set; }
        public int IdPregunta { get; set; }
        public int IdEncuesta { get; set; }
        public int IdCategoria { get; set; }
        public decimal Valoracion { get; set; }
        public int IdEstatus { get; set; }
        public DateTime FechaHoraCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string ProgramaCreacion { get; set; }
        public DateTime FechaHoraModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string ProgramaModificacion { get; set; }
        public DateTime FechaHoraEliminacion { get; set; }
        public string UsuarioEliminacion { get; set; }
        public string ProgramaEliminacion { get; set; }
    }
}
