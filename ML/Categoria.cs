using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Categoria
    {
        public string NombrePadreCategoria { get; set; }
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdPadre { get; set; }
        public decimal Valoracion { get; set; }
        public List<Categoria> Subcategorias { get; set; }
        public int Estatus { get; set; }
        public DateTime FechaHoraCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string ProgramaCreacion { get; set; }
        public DateTime FechaHoraModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string ProgramaModificacion { get; set; }
        public DateTime FechaHoraEliminacion { get; set; }
        public string UsuarioEliminacion { get; set; }
        public string IdPadreObjeto { get; set; }
        public string ProgramaEliminacion { get; set; }
        public List<Categoria> Preguntas { get; set; }
        public object Promedio { get; set; }
    }
}
