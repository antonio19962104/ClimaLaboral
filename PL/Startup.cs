using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;

[assembly: OwinStartupAttribute(typeof(PL.Startup))]
namespace PL
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            app.UseHangfireAspNet(GetHangfireServers);
            app.UseHangfireDashboard();
            // Let's also create a sample background job
            BackgroundJob.Enqueue(() => Debug.WriteLine("Hello world from Hangfire!"));
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

            yield return new BackgroundJobServer();
        }
    }
}
