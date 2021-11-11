﻿using Quartz;
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


            //BL.PlanesDeAccion.NotificacionInicialResponsables(3);

            //var message = new MailMessage();
            //message.To.Add(new MailAddress("jamurillo@grupoautofin.com"));
            //message.Subject = "Prueba notificacion";
            //message.IsBodyHtml = true;
            //string contentMessage = File.ReadAllText(Path.GetFullPath(@"\\10.5.2.101\RHDiagnostics\templates\template-email.html"));
            //contentMessage = PlanesDeAccion.CrearVistaWebEmail(contentMessage);
            //message.Body = contentMessage;//ML.Email.PlantillaNotificacionInicial
            //message.Priority = MailPriority.Normal;
            //message.BodyEncoding = System.Text.Encoding.UTF8;
            //using (var smtp = new SmtpClient())
            //{
            //    try
            //    {
            //        smtp.Send(message);
            //    }
            //    catch (SmtpException aE)
            //    {
            //        Console.Write(aE.Message);
            //    }
            //    finally
            //    {
            //        smtp.Dispose();
            //    }
            //}

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

            /*
             * Aqui se colocan las tareas programadas a ejecutar segun la recurrencia que se requiere
             * Agregar Jobs de notificaciones para planes de Acción (Notificaciones programadas, Cron de reenvio)
            */
            RecurringJob.AddOrUpdate("BorradoReportesPDFByFecha", () => BL.LogReporteoClima.DeleteReportes(), "0 0 * * *", TimeZoneInfo.Local);
            RecurringJob.AddOrUpdate("GeneraCategoriasForPlanes", () => BL.PlanesDeAccion.EjecutaJob(), "0 0 * * *", TimeZoneInfo.Local);
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
    }
}
