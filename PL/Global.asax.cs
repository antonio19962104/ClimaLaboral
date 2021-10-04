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

            RecurringJob.AddOrUpdate("BorradoReportesPDFByFecha", () => BL.LogReporteoClima.DeleteReportes(), "0 0 * * *");
            //RecurringJob.AddOrUpdate("RecurringJob", () => BL.LogReporteoClima.sendMail("demoEntidad", 2021, "jamurillo@grupoautofin.com", "demo.clima.com", "Pass@word01"), Cron.Minutely);
            /*
             * Aqui se colocan las tareas programadas a ejecutar segun la recurrencia que se requiere
            */
            // BL.ClimaDinamico.UpdateGeneracionEmpleado();
            //RecurringJob.AddOrUpdate("MyJobGeneraciones", () => BL.ClimaDinamico.UpdateGeneracionEmpleado(), Cron.Daily);
            // RecurringJob.AddOrUpdate("MyJob", () => BL.Encuesta.CronReenvioEmail(), Cron.Daily);
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
