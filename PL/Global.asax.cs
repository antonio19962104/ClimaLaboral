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

            
            // https://github.com/antonio19962104/ClimaLaboral.git
            // https://www.doodleish.com/2020/06/hosting-for-angular-app-react-app.html
            // https://stackoverflow.com/questions/10168240/encrypting-decrypting-a-string-in-c-sharp
            // mandar a encryptar el idusuario, nombre usuario, perfil, empresa de procedencia
            string usuario = "jamurillo@grupoautofin.com|1|1,2,3,4,5";
            string pass = "Pass@word01";
            var encryptData = BL.EstructuraAFMReporte.Encrypt(usuario, pass);// esto va a la url
            var descryptData = BL.EstructuraAFMReporte.Decrypt(encryptData, pass);
            var usr = descryptData.Split('|')[0];
            var perfil = descryptData.Split('|')[1];
            var comp = descryptData.Split('|')[2];
            var lst = comp.Split(',').Select(n => Convert.ToInt32(n)).ToList();
            var list = new List<string>();


            // PL.Controllers.BackGroundJobController.getUnidadNegocio("AUT - ELEGANTES", 2, 2114);

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
                    // .UseSqlServerStorage("Data Source=10.5.2.115;Initial Catalog=RH_Des;User ID=RHDiagnostics;Password=RH2020;", new SqlServerStorageOptions
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
