using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class ResponsablesAccionesPlan : Bitacora
    {
        public int IdResponsablesAccionesPlan { get; set; }
        public ML.AccionesPlan AccionesPlan { get; set; } = new AccionesPlan();
        public ML.Responsable Responsable { get; set; } = new Responsable();
    }
}
