using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class AccionesPlan : Bitacora
    {
        public int IdAccionesPlan { get; set; }
        public ML.PlanDeAccion PlanDeAccion { get; set; } = new PlanDeAccion();
        public ML.AccionDeMejora AccionesDeMejora { get; set; } = new AccionDeMejora();
        public int Periodicidad { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Objetivo { get; set; }
        public string Meta { get; set; }
        public string Comentarios { get; set; }
    }
}
