using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class EmailsEncuesta
    {
        public int IdEncuesta { get; set; } = 0;
        public int IdBaseDeDatos { get; set; } = 0;
        public int TipoEnvio { get; set; } = 0;
        public string Template { get; set; } = string.Empty;
        public int Prioridad { get; set; } = 0;
        public string Asunto { get; set; } = "Notificaciones portal de encuestas";
        public string CC { get; set; } = string.Empty;
        public string CurrentAdmin { get; set; } = string.Empty;
    }
}
