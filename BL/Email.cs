﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Diagnostics;
using Newtonsoft.Json;
using System.IO;
using System.Configuration;

namespace BL
{
    /// <summary>
    /// Capa de negocios de envíos de Email
    /// </summary>
    public class Email
    {
        /// <summary>
        /// Guarda el estatus de envío de un email de notificación
        /// El tipo de notificacion 0 (Cero) es cuando se trata de un envio de email customizado para encuestas
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
                        ProgramaCreacion = TipoNotificacion == 0 ? "Envio Notificaciones Encuesta" : "Envio Notificaciones Planes",
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
                    BL.NLogGeneratorFile.nlogPlanesDeAccion.Info("Job de reenvio de notificaciones de planes de accion");
                    var emailsFallidos = context.EstatusEmail.Where(o => o.IdEstatusMail == 1 && o.ProgramaCreacion == "Envio Notificaciones Planes" && o.noIntentos <= 2);
                    BL.NLogGeneratorFile.nlogPlanesDeAccion.Info($"Se encontraron {emailsFallidos.Count()} emails para reenviar");
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
                                BL.NLogGeneratorFile.nlogPlanesDeAccion.Info("Job Reenvio Notificaciones PDA. Email enviado a: " + email.Destinatario);
                                BL.Email.UpdateEstatusEmail(email, true, "Email enviado exitosamente");
                            }
                            catch (SmtpException aE)
                            {
                                BL.NLogGeneratorFile.nlogPlanesDeAccion.Error("Job Renvio de Notificaciones PDA. Falló envío de email a :" + email.Destinatario);
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
        /// <param name="administrador"></param>
        /// <returns></returns>
        public static bool EnvioNotificaciones(int TipoNotificacion, string destinatario, List<ML.AccionDeMejora> acciones, DL.Responsable responsable, DL.Administrador administrador, int IdPlanDeAccion)
        {
            try
            {
                if (acciones.Count > 0)
                {
                    if (administrador == null)
                        administrador = new DL.Administrador() { UserName = "Sin usuario", Password = "Sin contraseña" };
                    var message = new MailMessage();
                    message.To.Add(new MailAddress(destinatario));
                    message.Subject = ObtenerAsunto(TipoNotificacion);
                    message.IsBodyHtml = true;
                    string contentMessage = ObtenerPlantilla(TipoNotificacion);
                    //Armar las iteraciones de acciones de ser el caso
                    contentMessage += ObtenerContenidoHTMLAccionesEmail(TipoNotificacion, acciones, IdPlanDeAccion);
                    contentMessage += "<div><p>Tus credenciales para poder acceder a subir tus avances son las siguientes</p><p>Username: " + administrador.UserName + "</p><p>Contraseña: " + administrador.Password + "</p>             <p>Accede entrando a: </p><a href='" + ConfigurationManager.AppSettings["urlTemplateLocation"].ToString() + "/LoginAdmin/Login'><img src='http://www.diagnostic4u.com/img/Logo_emails.png' style='border-radius: 5px;' /></a></div>";
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
        /// 
        /// </summary>
        /// <param name="TipoNotificacion"></param>
        /// <param name="destinatario"></param>
        /// <param name="acciones"></param>
        /// <param name="responsable"></param>
        /// <param name="administrador"></param>
        /// <param name="IdPlanDeAccion"></param>
        /// <param name="Prioridad"></param>
        /// <param name="Asunto"></param>
        /// <param name="Plantilla"></param>
        /// <returns></returns>
        public static bool EnvioNotificacionesCustom(int TipoNotificacion, string destinatario, List<ML.AccionDeMejora> acciones, DL.Responsable responsable, DL.Administrador administrador, int IdPlanDeAccion, int Prioridad, string Asunto, string Plantilla)
        {
            try
            {
                if (acciones.Count > 0)
                {
                    if (administrador == null)
                        administrador = new DL.Administrador() { UserName = "Sin usuario", Password = "Sin contraseña" };
                    var message = new MailMessage();
                    message.To.Add(new MailAddress(destinatario));
                    message.Subject = Asunto;
                    message.IsBodyHtml = true;
                    string contentMessage = "<html><body><div>" + Plantilla;
                    string contenidoAcciones = ObtenerContenidoHTMLAccionesEmail(TipoNotificacion, acciones, IdPlanDeAccion);
                    contentMessage = contentMessage.Replace("#Lista Acciones#", contenidoAcciones);
                    contentMessage += "<div><p>Tus credenciales para poder acceder a subir tus avances son las siguientes</p><p>Username: " + administrador.UserName + "</p><p>Contraseña: " + administrador.Password + "</p>             <p>Accede entrando a: </p><a href='" + ConfigurationManager.AppSettings["urlTemplateLocation"].ToString() + "/LoginAdmin/Login'><img src='http://www.diagnostic4u.com/img/Logo_emails.png' style='border-radius: 5px;' /></a></div>";
                    contentMessage += "</div></body></html> ";
                    contentMessage = contentMessage.Replace("#Nombre#", (string.Concat(responsable.Nombre, " ", responsable.ApellidoPaterno, " ", responsable.ApellidoMaterno)));
                    contentMessage = PlanesDeAccion.CrearVistaWebEmail(contentMessage);
                    message.Body = contentMessage;
                    message.Priority = (MailPriority)Prioridad;
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
        public bool nullEmp(string s) => s == null || s == "";
        /// <summary>
        /// Envio de notificaciones customizadas para encuestas de clima laboral
        /// </summary>
        /// <returns></returns>
        public static bool EnvioNotificacionesCustom(DL.Encuesta encuesta, DL.ConfigClimaLab configClimaLab, DL.Empleado empleado, ML.Email email)
        {
            bool result = false;
            try
            {

                if (!string.IsNullOrEmpty(empleado.Correo))
                {
                    empleado.Correo = empleado.Correo.Trim();
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.Subject = email.Subject;
                    mailMessage.Priority = (MailPriority)email.Priority;
                    mailMessage.IsBodyHtml = true;
                    mailMessage.To.Add(new MailAddress(empleado.Correo));
                    string contentMessage = email.Plantilla;
                    contentMessage = contentMessage.Replace("*NombreUsuario*", string.Concat(empleado.Nombre, empleado.ApellidoPaterno, empleado.ApellidoMaterno));
                    contentMessage = contentMessage.Replace("*NombreEncuesta*", encuesta.Nombre);
                    contentMessage = contentMessage.Replace("*FechaInicio*", configClimaLab.FechaInicio.ToString());
                    contentMessage = contentMessage.Replace("*FechaFin*", configClimaLab.FechaFin.ToString());
                    contentMessage = contentMessage.Replace("#Contraseña#", empleado.ClaveAcceso);
                    contentMessage = contentMessage.Replace("#LinkEncuesta#", string.Concat(ConfigurationManager.AppSettings["urlTemplateLocation"].ToString(), "/Encuesta/Login/?e=", encuesta.IdEncuesta));

                    mailMessage.Body = contentMessage;
                    using (var smtp = new SmtpClient())
                    {
                        try
                        {
                            smtp.Send(mailMessage);
                            BL.NLogGeneratorFile.nlogPlanesDeAccion.Info("Email enviado a: " + empleado.Correo);
                            BL.Email.GuardarEstatusEmail(mailMessage, true, "Email enviado exitosamente ", 0);
                        }
                        catch (SmtpException aE)
                        {
                            BL.NLogGeneratorFile.nlogPlanesDeAccion.Error("Falló envío de email a :" + empleado.Correo);
                            BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                            BL.Email.GuardarEstatusEmail(mailMessage, false, (string.Concat(aE.Message, (aE.InnerException == null ? " inner exception is null " : aE.InnerException.ToString()))), 0);
                        }
                        finally
                        {
                            smtp.Dispose();
                        }
                    }
                }
                else
                {
                    BL.NLogGeneratorFile.nlogModuloEncuestas.Info($"El empleado {empleado.IdEmpleado} no cuenta con un email");
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloEncuestas(aE, new StackTrace());
            }
            return result;
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
                    contenido = contenido.Replace("#periodicidad#", BL.PlanesDeAccion.ObtenerPeriodicidadById(accionPlan.Periodicidad).Descripcion);
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
                contenido = ML.Email.PlantillaAcciones;
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
