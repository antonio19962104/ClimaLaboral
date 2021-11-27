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
		public static Encuesta Encuesta { get; set; }
        public int IdAdminCreate { get; set; } = 0;
        public int IdEncuesta { get; set; } = 0;
        public int IdBaseDeDatos { get; set; } = 0;
        public int AnioAplicacion { get; set; }
		public string Area { get; set; } = string.Empty;
        public string key { get; set; }
		 public decimal PorcentajeAvance { get; set; }
        public List<AccionesPlan> ListAcciones { get; set; } = new List<AccionesPlan>();
        public List<Responsable> ListResponsables { get; set; }
        public Categoria Categoria { get; set; }
        public List<JobsNotificacionesPDA> ListJobsNotificaciones { get; set; } = new List<JobsNotificacionesPDA>();
    }
}
