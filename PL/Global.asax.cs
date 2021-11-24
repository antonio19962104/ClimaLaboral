using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Hangfire;
using Hangfire.SqlServer;
using System.Diagnostics;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Security.Claims;
using BL;
using Microsoft.Office.Interop.PowerPoint;
using Microsoft.Office.Core;
using Aspose.Pdf;
using System.Net.Mail;
using System.IO;

namespace PL
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            HangfireAspNet.Use(GetHangfireServers);

            PruebasIniciales();

            /*
             * Aqui se colocan las tareas programadas a ejecutar segun la recurrencia que se requiere
             * Agregar Jobs de notificaciones para planes de Acción (Notificaciones programadas, Cron de reenvio)
            */
            RecurringJob.AddOrUpdate("BorradoReportesPDFByFecha", () => BL.LogReporteoClima.DeleteReportes(), "0 0 * * *", TimeZoneInfo.Local);
            RecurringJob.AddOrUpdate("GeneraPromediosCategoriasForPlanes", () => BL.PlanesDeAccion.EjecutaJob(), "0 0 * * *", TimeZoneInfo.Local);
            RecurringJob.AddOrUpdate("PDANotificacionPrevia", () => BL.PlanesDeAccion.NotificacionPrevia(), "0 9 * * *", TimeZoneInfo.Local);
            RecurringJob.AddOrUpdate("PDANotificacionSinAvanceInicial", () => BL.PlanesDeAccion.TriggerNotificacionSinAvanceInicial(), "0 10 * * *", TimeZoneInfo.Local);
            RecurringJob.AddOrUpdate("PDANotificacionAgradecimiento", () => BL.PlanesDeAccion.NotificacionAgradecimiento(), "0 11 * * *", TimeZoneInfo.Local);
            RecurringJob.AddOrUpdate("JobReenvioEmails", () => BL.Email.CronReenvioNotificaciones(), "0 12 * * *", TimeZoneInfo.Local);
        }
        public void ResetRecurringJobs()
        {
            using (var connection = JobStorage.Current.GetConnection())
            {
                var setKey = "Queue-jobs";
                var savedJobIds = connection.GetAllItemsFromSet(setKey);
                var missingJobsIds = savedJobIds.ToList();
                var hangfire = GetHangfireServers();
                
                foreach (var jobId in missingJobsIds)
                {
                    RecurringJob.RemoveIfExists(jobId);
                }
            }
        }
        private IEnumerable<IDisposable> GetHangfireServers()
        {
            GlobalConfiguration.Configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(ConfigurationManager.ConnectionStrings["myConnection"].ToString(), new SqlServerStorageOptions
                    {
                        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                        QueuePollInterval = TimeSpan.Zero,
                        UseRecommendedIsolationLevel = true,
                        DisableGlobalLocks = true
                    });
            GlobalConfiguration.Configuration
            .UseNLogLogProvider();
            
            yield return new BackgroundJobServer();
        }
        /// <summary>
        /// Metodo donde se depositan las pruebas de inicio rapido
        /// </summary>
        private void PruebasIniciales()
        {
            /*PL.Controllers.BackGroundJobController backGroundJobController = new Controllers.BackGroundJobController();
            ML.Historico historico = new ML.Historico()
            {
                Anio = 2020,
                enfoqueSeleccionado = 0,
                currentURL = "localhost:11124",
                CurrentUsr = "jamurillo@grupoautofin.com",
                EntidadNombre = "GAFM",
                IdBaseDeDatos = 2114,
                idEncuesta = 1,
                IdTipoEntidad = 0,
                nivelDetalle = "012345",
                tipoEntidad = 0,
                ps = "52314672366B707931454E796D44553536504E4268773D3D"
            };*/
            //BL.PlanesDeAccion.EjecutaJob();
            //backGroundJobController.BackgroundJobCreateReportNivelGAFM(historico);
            //BL.ReporteD4U.getComparativoPorAntiguedadEE("", "GAFM", 2021, 2114);
        }
        public static void PruebaEnvioMain()
        {
            using (SmtpClient client = new SmtpClient())
            {
                var message = new MailMessage();
                message.To.Add("jamurillo@grupoautofin.com");
                message.Subject = "Prueba";
                message.IsBodyHtml = true;
                message.Body = @"
<!doctype html>
<html>
   <head>
      <meta charset='UTF-8'>
      <title>Nivel de Colaboración</title>
      <style>
      	td{
        	padding: 5px 5px 5px 10px;
        }
      </style>
   </head>
   <body style='font-family: Gotham, Helvetica Neue, Helvetica, Arial, sans-serif;'>
      <table style='border: solid 1px #EEEEEE;width:100%;'>
         <tbody style=''>
            <tr style='background-color: #002856; color: #FFFFFF; padding: 10px;'>
               <td colspan='2' style='font-weight:bold;padding: 5px 5px 5px 10px;'>NIVEL DE COLABORACIÓN</td>
               <td style='padding: 5px 5px 5px 10px;'>0%</td>
               <td style='padding: 5px 5px 5px 10px;'><img src='http://www.diagnostic4u.com//img/ReporteoClima/Iconos/sol-icono.png' width='30' height='' /></td>
            </tr>
            <tr style='padding: 10px; border: solid 1px #EEEEEE;'>
               <td colspan='3' style='padding: 5px 5px 5px 10px;'>ACCIÓN 1</td>
            </tr>
         </tbody>
      </table>
      <p style='display:none'>Perioricidad</p>
      <table style='border: solid 1px #EEEEEE;width:100%'>
         <tbody style=''>
            <tr>
                <td colspan='2' style='width: 50%;padding: 5px 5px 5px 10px;'>Perioricidad</td>
            </tr>
            <tr>
               <td colspan='2' style='width: 50%;padding: 5px 5px 5px 10px;'>0</td>
               <td style='padding: 5px 5px 5px 10px;'><img src='http://www.diagnostic4u.com/img/icono-calendario.png' width='20' height='20' style='max-width: 15px; border: solid 1px #EEEEEE;'> Inicia: 01/11/2021</td>
               <td style='padding: 5px 5px 5px 10px;'><img src='http://www.diagnostic4u.com/img/icono-calendario.png' width='20' height='20' style='max-width: 15px; border: solid 1px #EEEEEE;'> Inicia: 01/02/2022</td>
            </tr>
         </tbody>
      </table>
      <table style='border: solid 1px #EEEEEE;width:100%'>
         <tbody style=''>
            <tr>
               <td style='width: 50%;padding: 5px 5px 5px 10px;'><p>Objetivo</p></td>
               <td style='width: 50%;padding: 5px 5px 5px 10px;'><p>Meta</p></td>
            </tr>
            <tr>
               <td style='width: 50%; border: solid 1px #EEEEEE;padding: 5px 5px 5px 10px;'>Objetivo 1</td>
               <td style='width: 50%; border: solid 1px #EEEEEE;padding: 5px 5px 5px 10px;'>Meta 1</td>
            </tr>
         </tbody>
      </table>
   </body>
</html>
";
                try
                {
                    client.Send(message);
                }
                catch (SmtpException ae)
                {
                    Console.WriteLine(ae.Message);
                }
                finally
                {
                    client.Dispose();
                }
            }
        }
    }
}
