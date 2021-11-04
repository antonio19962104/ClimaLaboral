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
        /// Metodo de envío de notificaciones para los planes de acción (Un email por cada acción de mejora)
        /// </summary>
        /// <param name="email"></param>
        /// <param name="accion"></param>
        /// <param name="TipoNotificacion"></param>
        public static void NotificacionesPlanes(ML.Email email, DL.Acciones accion, int TipoNotificacion)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(email.To));
            message.Subject = ObtenerAsunto(TipoNotificacion);
            message.Body = string.Format(GetPlantilla(TipoNotificacion), email.NombreDestinatario, email.NombrePlanAccion, accion.Descripcion);
            message.IsBodyHtml = true;
            using (var smtp = new SmtpClient())
            {
                try
                {
                    smtp.Send(message);
                    BL.NLogGeneratorFile.nlogPlanesDeAccion.Info("Email enviado a: " + email.To);
                    BL.Email.GuardarEstatusEmail(message, true, "Email enviado exitosamente");
                }
                catch (SmtpException aE)
                {
                    BL.NLogGeneratorFile.nlogPlanesDeAccion.Error("Falló envío de email a :" + email.To);
                    BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                    BL.Email.GuardarEstatusEmail(message, false, aE.Message);
                }
                finally
                {
                    smtp.Dispose();
                }
            }
        }
        /// <summary>
        /// Metodo de envío de notificaciones para los planes de acción (Un email responsable)
        /// </summary>
        /// <param name="email"></param>
        /// <param name="acciones"></param>
        /// <param name="TipoNotificacion"></param>
        public static void NotificacionesPlanes(ML.Email email, List<DL.Acciones> acciones, int TipoNotificacion)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(email.To));
            message.Subject = ObtenerAsunto(TipoNotificacion);
            string DescripcionAcciones = ObtenerContenidoListadoAccionesEmail(acciones);
            message.Body = string.Format(GetPlantilla(TipoNotificacion), email.NombreDestinatario, email.NombrePlanAccion, DescripcionAcciones);
            message.Priority = (MailPriority)email.Priority;
            message.IsBodyHtml = true;
            using (var smtp = new SmtpClient())
            {
                try
                {
                    smtp.Send(message);
                    BL.NLogGeneratorFile.nlogPlanesDeAccion.Info("Email enviado a: " + email.To);
                    BL.Email.GuardarEstatusEmail(message, true, "Email enviado exitosamente");
                }
                catch (SmtpException aE)
                {
                    BL.NLogGeneratorFile.nlogPlanesDeAccion.Error("Falló envío de email a :" + email.To);
                    BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                    BL.Email.GuardarEstatusEmail(message, false, aE.Message);
                }
                finally
                {
                    smtp.Dispose();
                }
            }
        }
        /// <summary>
        /// Obtiene un objeto del modelo email
        /// </summary>
        /// <param name="responsable"></param>
        /// <param name="planDeAccion"></param>
        /// <returns></returns>
        public static ML.Email ObtenerObjetoEmail(DL.Responsable responsable, DL.PlanDeAccion planDeAccion)
        {
            ML.Email email = new ML.Email();
            try
            {
                email = new ML.Email()
                {
                    To = responsable.Email,
                    MailPriority = MailPriority.High,
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
        /// <summary>
        /// Obtiene la plantilla de email para enviar la notificación
        /// </summary>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public static string GetPlantilla(int tipo)
        {
            switch (tipo)
            {
                case 1:
                    return ML.Email.PlantillaNotificacionInicial;
                case 2:
                    return ML.Email.PlantillaNotificacionPrevia;
                case 3:
                    return ML.Email.PlantillaSinAvanceInicial;
                case 4:
                    return ML.Email.PlantillaAvanceNoLogrado;
                case 5:
                    return ML.Email.PlantillaNotificacionAgradecimiento;
                default:
                    return string.Empty;
            }
        }
        /// <summary>
        /// Obtiene el asunto del email de notificación
        /// </summary>
        /// <param name="TipoNotificacion"></param>
        /// <returns></returns>
        public static string ObtenerAsunto(int TipoNotificacion)
        {
            string asunto = string.Empty;
            switch (TipoNotificacion)
            {
                case 1:
                    return ML.Email.AsuntoNotificacionAvanceInicial;
                case 2:
                    return ML.Email.AsuntoNotificacionPrevia;
                case 3:
                    return ML.Email.AsuntoNotificacionAvanceInicial;
                case 4:
                    return ML.Email.AsuntoNotificacionAvanceNoLogrado;
                case 5:
                    return ML.Email.AsuntoNotificacionAgradecimiento;
                default:
                    return string.Empty;
            }
        }
        /// <summary>
        /// Convierte el listado de acciones a una lista html para el envío de la notificación
        /// </summary>
        /// <param name="acciones"></param>
        /// <returns></returns>
        public static string ObtenerContenidoListadoAccionesEmail(List<DL.Acciones> acciones)
        {
            string contenido = "<ul>";
            try
            {
                foreach (var accion in acciones)
                {
                    contenido += "<li>" + accion.Descripcion + "</li>";
                }
                contenido += "</ul>";
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                return string.Empty;
            }
            return contenido;
        }
        /// <summary>
        /// Guarda el estatus de envío de un email de notificación
        /// </summary>
        /// <param name="message"></param>
        /// <param name="status"></param>
        /// <param name="MsgEnvio"></param>
        /// <returns></returns>
        public static bool GuardarEstatusEmail(MailMessage message, bool status, string MsgEnvio)
        {
            bool correct = true;
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    DL.EstatusEmail estatusEmail = new DL.EstatusEmail()
                    {
                        Mensaje = message.Body,
                        Destinatario = message.To.ToString(),
                        IdEstatusMail = status == true ? 2 : 1,
                        FechaHoraCreacion = DateTime.Now,
                        UsuarioCreacion = "Envio Notificaciones",
                        ProgramaCreacion = "Envio Notificaciones Planes",
                        MsgEnvio = MsgEnvio,
                        noIntentos = 1
                    };
                    context.EstatusEmail.Add(estatusEmail);
                    context.SaveChanges();
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                correct = false;
            }
            return correct;
        }
        /// <summary>
        /// Actualiza el estatus de los estatus de email según lo que realiza el cron de reenvio
        /// </summary>
        /// <param name="email"></param>
        /// <param name="status"></param>
        /// <param name="msgEnvio"></param>
        /// <returns></returns>
        public static bool UpdateEstatusEmail(DL.EstatusEmail email, bool status, string msgEnvio)
        {
            bool correct = true;
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var EstatusEmail = context.EstatusEmail.Where(o => o.IdEstatusEmail == email.IdEstatusEmail).FirstOrDefault();
                    EstatusEmail.IdEstatusMail = status == true ? 2 : 1;
                    EstatusEmail.MsgEnvio = msgEnvio;
                    EstatusEmail.noIntentos = (EstatusEmail.noIntentos + 1);
                    EstatusEmail.FechaHoraModificacion = DateTime.Now;
                    EstatusEmail.UsuarioModificacion = "Envio Notificaciones";
                    EstatusEmail.ProgramaModificacion = "Envio Notificaciones Planes";

                    context.SaveChanges();
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                correct = false;
            }
            return correct;
        }
        /// <summary>
        /// Cron de reenvio de notificaciones fallidas
        /// </summary>
        public static void CronReenvioNotificaciones()
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var emailsFallidos = context.EstatusEmail.Where(o => o.IdEstatusMail == 1 && o.ProgramaCreacion == "Envio Notificaciones Planes" && o.noIntentos <= 2);
                    foreach (var email in emailsFallidos)
                    {
                        var message = new MailMessage();
                        message.To.Add(new MailAddress(email.Destinatario));
                        message.Subject = "Notificaciones planes de acción";
                        message.Body = email.Mensaje;
                        message.Priority = MailPriority.High;
                        message.IsBodyHtml = true;
                        using (var smtp = new SmtpClient())
                        {
                            try
                            {
                                smtp.Send(message);
                                BL.NLogGeneratorFile.nlogPlanesDeAccion.Info("Email enviado a: " + email.Destinatario);
                                BL.Email.UpdateEstatusEmail(email, true, "Email enviado exitosamente");
                            }
                            catch (SmtpException aE)
                            {
                                BL.NLogGeneratorFile.nlogPlanesDeAccion.Error("Falló envío de email a :" + email.Destinatario);
                                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                                BL.Email.UpdateEstatusEmail(email, false, aE.Message);
                            }
                            finally
                            {
                                smtp.Dispose();
                            }
                        }
                    }
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.nlogPlanesDeAccion.Error("Falló el cron de reenvio de notificacioness");
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
            }
        }
    }
}
