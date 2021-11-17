using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Diagnostics;
using Newtonsoft.Json;
using System.IO;

namespace BL
{
    /// <summary>
    /// Capa de negocios de envíos de Email
    /// </summary>
    public class Email
    {
        /// <summary>
        /// Guarda el estatus de envío de un email de notificación
        /// </summary>
        /// <param name="message"></param>
        /// <param name="status"></param>
        /// <param name="MsgEnvio"></param>
        /// <param name="TipoNotificacion"></param>
        /// <returns></returns>
        public static bool GuardarEstatusEmail(MailMessage message, bool status, string MsgEnvio, int TipoNotificacion)
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
                        noIntentos = 1,
                        CC = ObtenerCopiaEnCorreo(message),
                        CCO = ObtenerCopiaOcultaEnCorreo(message),
                        TipoNotificacion = TipoNotificacion,
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
        /// Obtiene la cadena de destinatarios copiados en un email
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string ObtenerCopiaEnCorreo(MailMessage message)
        {
            string copia = string.Empty;
            try
            {
                int index = 0;
                foreach (var item in message.CC)
                {
                    index++;
                    if (index > 0)
                        copia += ";" + item.Address;
                    else
                        copia += item.Address;
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
            }
            return copia;
        }
        /// <summary>
        /// Obtiene la cadena de destinatarios copiados ocultos en un email
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string ObtenerCopiaOcultaEnCorreo(MailMessage message)
        {
            string copia = string.Empty;
            try
            {
                int index = 0;
                foreach (var item in message.Bcc)
                {
                    index++;
                    if (index > 0)
                        copia += ";" + item.Address;
                    else
                        copia += item.Address;
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
            }
            return copia;
        }
        /// <summary>
        /// Obtiene el objeto MailMessage de destinatarios copiados en un email
        /// </summary>
        /// <param name="cc"></param>
        /// <param name="cco"></param>
        /// <returns></returns>
        public static MailMessage ObtenerObjetoCopiaEnCorreo(string cc, string cco)
        {
            var data = new MailMessage();
            try
            {
                var arregloCC = cc.Split(';');
                foreach (var item in arregloCC)
                {
                    data.CC.Add(new MailAddress(item));
                }

                var arregloCCO = cc.Split(';');
                foreach (var item in arregloCCO)
                {
                    data.Bcc.Add(new MailAddress(item));
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
            }
            return data;
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
                        var message = ObtenerObjetoCopiaEnCorreo(email.CC, email.CCO);
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

        /// <summary>
        /// Disparador de envio de notificaciones
        /// </summary>
        public static void TriggerEnvioNotificaciones()
        {

        }

        /// <summary>
        /// Metodo de envio de notificacion
        /// </summary>
        /// <param name="TipoNotificacion"></param>
        /// <param name="destinatario"></param>
        /// <param name="acciones"></param>
        /// <param name="IdPlanDeAccion"></param>
        /// <param name="responsable"></param>
        /// <returns></returns>
        public static bool EnvioNotificaciones(int TipoNotificacion, string destinatario, List<ML.AccionDeMejora> acciones, DL.Responsable responsable, int IdPlanDeAccion)
        {
            try
            {
                if (acciones.Count > 0)
                {
                    var message = new MailMessage();
                    message.To.Add(new MailAddress(destinatario));
                    message.Subject = ObtenerAsunto(TipoNotificacion);
                    message.IsBodyHtml = true;
                    string contentMessage = ObtenerPlantilla(TipoNotificacion);
                    //Armar las iteraciones de acciones de ser el caso
                    contentMessage += ObtenerContenidoHTMLAccionesEmail(TipoNotificacion, acciones, IdPlanDeAccion);
                    contentMessage += "</div></body></html> ";
                    contentMessage = contentMessage.Replace("#responsable#", (string.Concat(responsable.Nombre, " ", responsable.ApellidoPaterno, " ", responsable.ApellidoMaterno)));
                    //Armar las iteraciones de acciones de ser el caso
                    contentMessage = PlanesDeAccion.CrearVistaWebEmail(contentMessage);
                    message.Body = contentMessage;
                    message.Priority = MailPriority.High;
                    message.BodyEncoding = System.Text.Encoding.UTF8;
                    message.Bcc.Add(new MailAddress("jamurillo@grupoautofin.com"));

                    using (var smtp = new SmtpClient())
                    {
                        try
                        {
                            smtp.Send(message);
                            BL.NLogGeneratorFile.nlogPlanesDeAccion.Info("Email enviado a: " + destinatario);
                            BL.Email.GuardarEstatusEmail(message, true, "Email enviado exitosamente", TipoNotificacion);
                        }
                        catch (SmtpException aE)
                        {
                            BL.NLogGeneratorFile.nlogPlanesDeAccion.Error("Falló envío de email a :" + destinatario);
                            BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                            BL.Email.GuardarEstatusEmail(message, false, (string.Concat(aE.Message, (aE.InnerException == null ? " inner exception is null " : aE.InnerException.ToString()))), TipoNotificacion);
                        }
                        finally
                        {
                            smtp.Dispose();
                        }
                    }
                }
                else
                {
                    BL.NLogGeneratorFile.nlogPlanesDeAccion.Info("El envío no procede ya que no se contienen acciones en la Lista");
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                return false;
            }
            return true;
        }
        /// <summary>
        /// Lee el fichero de la plantilla html
        /// </summary>
        /// <param name="tipoNotificacion"></param>
        /// <returns></returns>
        public static string ObtenerPlantilla(int tipoNotificacion)
        {
            string contentMessage = string.Empty;
            switch (tipoNotificacion)
            {
                case 1:
                    // Notificación al guardar nuevo plan
                    contentMessage = File.ReadAllText(Path.GetFullPath(@"\\10.5.2.101\RHDiagnostics\templates\template-email-1.html"));
                    break;
                case 2:
                    // Notificación una semana antes
                    contentMessage = File.ReadAllText(Path.GetFullPath(@"\\10.5.2.101\RHDiagnostics\templates\template-email-2.html"));
                    break;
                case 3:
                    // Notificacion no reporta avance en el primer día
                    contentMessage = File.ReadAllText(Path.GetFullPath(@"\\10.5.2.101\RHDiagnostics\templates\template-email-3.html"));
                    break;
                case 4:
                    // Notificacion avance esperado no logrado
                    contentMessage = File.ReadAllText(Path.GetFullPath(@"\\10.5.2.101\RHDiagnostics\templates\template-email-4.html"));
                    break;
                case 5:
                    // Notificacion agradecimiento
                    contentMessage = File.ReadAllText(Path.GetFullPath(@"\\10.5.2.101\RHDiagnostics\templates\template-email-5.html"));
                    break;
                default:
                    BL.NLogGeneratorFile.nlogPlanesDeAccion.Error("No se encontro la plantilla a obtener en el metodo ObtenerPlantilla(" + tipoNotificacion + ")");
                    break;
            }
            return contentMessage;
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
                    return ML.Email.AsuntoNotificacionInicial;
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
        /// <param name="TipoNotificacion"></param>
        /// <param name="IdPlanDeAccion"></param>
        /// <returns></returns>
        public static string ObtenerContenidoHTMLAccionesEmail(int TipoNotificacion, List<ML.AccionDeMejora> acciones, int IdPlanDeAccion)
        {
            string contenidoFinal = string.Empty;
            try
            {
                foreach (var accion in acciones)
                {
                    string contenido = Email.ObtenerPlantillaAcciones(TipoNotificacion);
                    string nombreCategoria = BL.Categoria.getNombreCatByIdCat(accion.Categoria.IdCategoria);
                    var planDeAccion = BL.PlanesDeAccion.GetPlanById(IdPlanDeAccion);
                    var accionPlan = BL.PlanesDeAccion.ObtenerAccionesPlan(accion.IdAccionDeMejora, IdPlanDeAccion);
                    ML.PromediosCategorias promediosCategorias = new ML.PromediosCategorias()
                    {
                        Area = planDeAccion.Area,
                        BasesDeDatos = new ML.BasesDeDatos() { IdBaseDeDatos = planDeAccion.IdBaseDeDatos },
                        Categoria = new ML.Categoria() { IdCategoria = accion.Categoria.IdCategoria },
                        Encuesta = new ML.Encuesta() { IdEncuesta = planDeAccion.IdEncuesta },
                    };
                    decimal promedioCategoria = BL.PlanesDeAccion.ObtenerPromedioCategoria(promediosCategorias);

                    contenido = contenido.Replace("#Categoria#", nombreCategoria);
                    contenido = contenido.Replace("#icono#", ObtenerIconoEmail(promedioCategoria));
                    contenido = contenido.Replace("#100%#", promedioCategoria.ToString() + "%");
                    contenido = contenido.Replace("#accion#", accion.Descripcion);
                    contenido = contenido.Replace("#periodicidad#", accionPlan.Periodicidad.ToString());
                    contenido = contenido.Replace("#inicio#", accionPlan.FechaInicio.ToString());
                    contenido = contenido.Replace("#fin#", accionPlan.FechaFin.ToString());
                    contenido = contenido.Replace("#objetivo#", accionPlan.Objetivo);
                    contenido = contenido.Replace("#meta#", accionPlan.Meta);

                    contenidoFinal += contenido;
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                return @"<div style='text-align:center;'>
                            <p>Ocurrió un error al generar este contenido</p>
                        </div>";
            }
            return contenidoFinal;
        }
        /// <summary>
        /// Obtiene la plantilla html del listado de acciones
        /// </summary>
        /// <param name="TipoNotificacion"></param>
        /// <returns></returns>
        public static string ObtenerPlantillaAcciones(int TipoNotificacion)
        {
            string contenido = string.Empty;
            try
            {
                switch (TipoNotificacion)
                {
                    case 1:
                        contenido = ML.Email.PlantillaContenidoAccionesNot1;
                        break;
                    case 2:
                        contenido = ML.Email.PlantillaContenidoAccionesNot1;
                        break;
                    case 3:
                        contenido = ML.Email.PlantillaContenidoAccionesNot1;
                        break;
                    case 4:
                        contenido = ML.Email.PlantillaContenidoAccionesNot1;
                        break;
                    case 5:
                        contenido = ML.Email.PlantillaContenidoAccionesNot1;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {

            }
            return contenido;
        }
        /// <summary>
        /// Obtiene el icono del header del email por categoria
        /// </summary>
        /// <param name="Porcentaje"></param>
        /// <returns></returns>
        public static string ObtenerIconoEmail(decimal Porcentaje)
        {
            string Imagen = string.Empty;
            try
            {
                if (Porcentaje < 70)
                {
                    Imagen = ML.Email.lluviaIcono;
                }
                if (Porcentaje >= 70 && Porcentaje < 80)
                {
                    Imagen = ML.Email.nubeIcono;
                }
                if (Porcentaje >= 80 && Porcentaje < 90)
                {
                    Imagen = ML.Email.solNubeIcono;
                }
                if (Porcentaje >= 90 && Porcentaje <= 100)
                {
                    Imagen = ML.Email.solIcono;
                }
            }
            catch (Exception aE)
            {
                return "";
            }
            return Imagen;
        }
    }
}
