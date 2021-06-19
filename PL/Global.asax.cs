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

            BL.ReporteD4U.GetPromedioCapacitacionDesarrolloByUNEGocioEE("turismo", 2020, 1);

            // metodos demo
            var aModel = new ML.modelReporte()
            {
                idTipoEntidad = 1,
                anioActual = 2019, // lo asigno asi porque asi cae de la vista del reporte
                entidadNombre = "turismo",
                idEntidad = 9,
                idPregunta = 1,
                idCompetencia = 1,
                idEncuesta = 1,
                idEnfoque = 1,
                idSubCategoria = 1
            };
            var model = new ML.Historico()
            {
                Anio = aModel.anioActual,
                IdTipoEntidad = aModel.idTipoEntidad,
                EntidadNombre = aModel.entidadNombre,
                EntidadId = aModel.idEntidad,
                idEncuesta = aModel.idEncuesta, CurrentUsr = "jamurillo"
            };

            //PL.Controllers.BackGroundJobController backGroundJobController = new Controllers.BackGroundJobController();
            //backGroundJobController.BackgroundJobCreateReport(model);
            //BL.ReporteClimaDinamico.getDataPermanenciaAbandono(aModel);
            //probar
            //PL.Controllers.ReporteClimaDinamicoController reporteClimaDinamicoController = new Controllers.ReporteClimaDinamicoController();
            //reporteClimaDinamicoController.creaReporte(model);
            //BL.ReporteD4U.GetEsperadasByUnegocio("turismo", 2020);






















            /*
             * Aqui se colocan las tareas programadas a ejecutar segun la recurrencia que se requiere
            */
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
