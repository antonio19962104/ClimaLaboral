using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class SeguimientoEvidencia : Bitacora
    {
        public int IdSeguimientoEvidencia { get; set; } = 0;
        public ML.Seguimiento Seguimiento { get; set; } = new Seguimiento();
        public ML.Evidencia Evidencia { get; set; } = new Evidencia();
    }
}
