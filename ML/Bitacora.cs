using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Bitacora
    {
        public DateTime FechaHoraCreacion { get; set; } = DateTime.Now;
        public int IdUsuarioCreacion { get; set; } = 0;
        public string IpCreacion { get; set; } = string.Empty;

        public DateTime FechaHoraModificacion { get; set; } = DateTime.Now;
        public int IdUsuarioModificacion { get; set; } = 0;
        public string IpModificacion { get; set; } = string.Empty;

        public DateTime FechaHoraEliminacion { get; set; } = DateTime.Now;
        public int IdUsuarioEliminacion { get; set; } = 0;
        public string IpEliminacion { get; set; } = string.Empty;
    }
}
