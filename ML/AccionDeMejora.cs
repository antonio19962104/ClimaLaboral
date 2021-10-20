using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class AccionDeMejora : Bitacora
    {
        public int IdAccionDeMejora { get; set; } = 0;
        public string Descripcion { get; set; } = string.Empty;
        public ML.PlanDeAccion PlanDeAccion { get; set; } = new PlanDeAccion();
        public ML.Rango Rango { get; set; } = new Rango();
        public ML.TipoEstatus Estatus { get; set; } = new TipoEstatus();
        public ML.Categoria Categoria { get; set; } = new Categoria();
        public ML.Encuesta Encuesta { get; set; } = new Encuesta();
        public ML.BasesDeDatos BasesDeDatos { get; set; } = new BasesDeDatos();
        public int AnioAplicacion { get; set; } = 0;
    }
}
