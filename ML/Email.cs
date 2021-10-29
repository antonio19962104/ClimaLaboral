using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Email
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string BCC { get; set; }
        public string CC { get; set; }
        public int Priority { get; set; }
        public MailPriority MailPriority { get; set; }
        public string NombreDestinatario { get; set; }
        public string NombrePlanAccion { get; set; }
        public enum TipoNotificacion {
            inicial = 1,
            WeekAfter = 2
        }

        public static string PlantillaNotificacionInicial { get; set; } = 
            @"Que tal {0}.
            Has sido elegido para colaborar en el plan de accion: {1} para dar seguimiento a las acciones de mejora en tu área.
            La acción de la cual estaras a cargo es:
            {2}
            ";
    }
}
