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
		public int IdAccion { get; set; }
        public int Periodicidad { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string sFechaInicio { get; set; }
        public string sFechaFin { get; set; }
        public string Objetivo { get; set; }
        public string Meta { get; set; }
        public string Comentarios { get; set; }
        public decimal PorcentajeAvance { get; set; }
		public List<ResponsablesAccionesPlan> ListadoResponsables { get; set; } = new List<ResponsablesAccionesPlan>();
        public List<Responsable> ListResponsable { get; set; } = new List<Responsable>();
        public List<string> Atachments { get; set; } = new List<string>();

    }
}
