using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Seguimiento : Bitacora
    {
        public int IdSeguimiento { get; set; } = 0;
        public ML.ResponsablesAccionesPlan ResponsableAccionesPlan { get; set; } = new ResponsablesAccionesPlan();
    }
}
