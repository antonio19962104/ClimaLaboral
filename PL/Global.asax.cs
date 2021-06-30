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
            // caemos en el cntrolador y antes de todo lama a esta funcon
            // MenuToken token = MenuToken.ValidarToken(aToken, User.GetUserId());
            // atoken es loque va en url y User.GetIserId es una cokkie
            // public static MenuToken ValidarToken(string aCadenaEncriptada, int aIdUsuarioEnCookie)
            // {
            //    return ValidarToken(aCadenaEncriptada, aIdUsuarioEnCookie, 0, 0);
            // }
            // sobrecarga para recibir tambien idAgencia y id marca

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
