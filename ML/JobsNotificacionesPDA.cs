using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class JobsNotificacionesPDA
    {
        public int IdJobsNotificacionesPDA { get; set; } = 0;
        public string JobId { get; set; } = string.Empty;
        public int IdEstatus { get; set; } = 0;
        public int IdPlanDeAccion { get; set; } = 0;
        public string CronExpression { get; set; } = string.Empty;
        public string Periodicidad { get; set; } = string.Empty;
    }
}
