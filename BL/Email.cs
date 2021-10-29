using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Diagnostics;
using Newtonsoft.Json;

namespace BL
{
    /// <summary>
    /// Capa de negocios de enviós de Email
    /// </summary>
    public class Email
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="accion"></param>
        /// <param name="TipoNotificacion"></param>
        public static void NotificacionesPlanes(ML.Email email, DL.Acciones accion, int TipoNotificacion)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(email.To));
            message.Subject = "Notificación inicial";
            message.Body = string.Format(GetPlantilla(TipoNotificacion), email.NombreDestinatario, email.NombrePlanAccion, accion.Descripcion);
            message.IsBodyHtml = true;
            using (var smtp = new SmtpClient())
            {
                try
                {
                    smtp.Send(message);
                    BL.NLogGeneratorFile.nlogPlanesDeAccion.Info("Email enviado a: " + email.To);
                }
                catch (SmtpException aE)
                {
                    BL.NLogGeneratorFile.nlogPlanesDeAccion.Error("Falló envío de email a :" + email.To);
                    BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                }
                finally
                {
                    smtp.Dispose();
                }
            }
        }
        public static ML.Email ObtenerObjetoEmail(DL.Responsable responsable, DL.PlanDeAccion planDeAccion)
        {
            ML.Email email = new ML.Email();
            try
            {
                email = new ML.Email()
                {
                    To = responsable.Email,
                    MailPriority = MailPriority.Normal,
                    NombreDestinatario = string.Concat(responsable.Nombre, " ", responsable.ApellidoPaterno, " ", responsable.ApellidoMaterno),
                    NombrePlanAccion = planDeAccion.Nombre,
                };
                BL.NLogGeneratorFile.logObjectsModuloPlanesDeAccion(email);
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
            }
            return email;
        }
        public static string GetPlantilla(int tipo)
        {
            switch (tipo)
            {
                case 1:
                    return ML.Email.PlantillaNotificacionInicial;
                case 2:
                    return "Plantilla";
                case 3:
                    return "Plantilla";
                default:
                    return string.Empty;
            }
        }
    }
}
