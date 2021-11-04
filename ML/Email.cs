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
        public string To { get; set; } = string.Empty;
        public string From { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string BCC { get; set; } = string.Empty;
        public string CC { get; set; } = string.Empty;
        public int Priority { get; set; }
        public MailPriority MailPriority { get; set; } = new MailPriority {  };
        public string NombreDestinatario { get; set; } = string.Empty;
        public string NombrePlanAccion { get; set; } = string.Empty;
        public enum TipoNotificacion {
            inicial = 1,
            WeekAfter = 2
        }
        public static string AsuntoNotificacionInicial = "";
        public static string AsuntoNotificacionPrevia = "";
        public static string AsuntoNotificacionAvanceInicial = "";
        public static string AsuntoNotificacionAvanceNoLogrado = "";
        public static string AsuntoNotificacionAgradecimiento = "";
        public static string PlantillaNotificacionInicial { get; set; } =
            @"
            <p>Que tal {responsable}.</p>
            <p>Has sido elegido para colaborar en el plan de acción {nombrePlan} para dar seguimiento a las acciones de mejora en tu área.</p>
            <p>La acción que tendrás a cargo es:</p>
            <p>{acciones}</p>
            <p>Saludos<br></p>
            ";

        public static string PlantillaNotificacionPrevia { get; set; } =
            @"
            <p>Que tal {responsable}.</p>
            <p>Has sido elegido para colaborar en el plan de acción {nombrePlan} para dar seguimiento a las acciones de mejora en tu área.</p>
            <p>La acción que tendrás a cargo es:</p>
            <p>{acciones}</p>
            <p>Saludos<br></p>
            ";

        public static string PlantillaSinAvanceInicial { get; set; } =
            @"
            <p>Que tal {responsable}.</p>
            <p>Has sido elegido para colaborar en el plan de acción {nombrePlan} para dar seguimiento a las acciones de mejora en tu área.</p>
            <p>La acción que tendrás a cargo es:</p>
            <p>{acciones}</p>
            <p>Saludos<br></p>
            ";

        public static string PlantillaAvanceNoLogrado { get; set; } =
            @"
            <p>Que tal {responsable}.</p>
            <p>Has sido elegido para colaborar en el plan de acción {nombrePlan} para dar seguimiento a las acciones de mejora en tu área.</p>
            <p>La acción que tendrás a cargo es:</p>
            <p>{acciones}</p>
            <p>Saludos<br></p>
            ";

        public static string PlantillaNotificacionAgradecimiento { get; set; } =
            @"
            <p>Que tal {responsable}.</p>
            <p>Has sido elegido para colaborar en el plan de acción {nombrePlan} para dar seguimiento a las acciones de mejora en tu área.</p>
            <p>La acción que tendrás a cargo es:</p>
            <p>{acciones}</p>
            <p>Saludos<br></p>
            ";
    }
}
