using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Evidencia : Bitacora
    {
        public int IdEvidencia { get; set; } = 0;
        public string Ruta { get; set; } = String.Empty;
        public ML.TipoEstatus Estatus { get; set; }
    }
}
