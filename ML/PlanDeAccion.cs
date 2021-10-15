using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class PlanDeAccion : Bitacora
    {
        public int IdPlanDeAccion { get; set; } = 0;
        public string Nombre { get; set; } = string.Empty;
        public int IdAdminCreate { get; set; } = 0;
        public int IdEncuesta { get; set; } = 0;
        public int IdBaseDeDatos { get; set; } = 0;
        public int AnioAplicacion { get; set; }
        public string key { get; set; }
    }
}
