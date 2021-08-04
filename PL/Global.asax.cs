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


            /*var oPowerPoint = new Microsoft.Office.Interop.PowerPoint.Application();
            oPowerPoint.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
            var activeSlide = (Microsoft.Office.Interop.PowerPoint.Slide)oPowerPoint.ActiveWindow.View.Slide;
            HtmlFragment htmlFragment = new HtmlFragment("");
            htmlFragment.cop


            string pictureFileName = "C:\\example.jpg";

            Application pptApplication = new Application();

            Microsoft.Office.Interop.PowerPoint.Slides slides;
            Microsoft.Office.Interop.PowerPoint._Slide slide;
            Microsoft.Office.Interop.PowerPoint.TextRange objText;
            Microsoft.Office.Interop.PowerPoint.PpHTMLVersion ppHTMLVersion;

            


            // Create the Presentation File
            Presentation pptPresentation = pptApplication.Presentations.Add(MsoTriState.msoTrue);

            Microsoft.Office.Interop.PowerPoint.CustomLayout customLayout = pptPresentation.SlideMaster.CustomLayouts[Microsoft.Office.Interop.PowerPoint.PpSlideLayout.ppLayoutText];

            // Create new Slide
            slides = pptPresentation.Slides;
            slide = slides.AddSlide(1, customLayout);

            // Add title
            objText = slide.Shapes[1].TextFrame.TextRange;

            objText.Text = "FPPT.com";
            objText.Font.Name = "Arial";
            objText.Font.Size = 32;

            objText = slide.Shapes[2].TextFrame.TextRange;
            objText.Text = "<div class='form-box'>                <img src='/img/logo.png' class='img-fluid logo-form' alt='Logo'>                <h1 class='title-form-box'>Iniciar sesión</h1>                <div class='form-group mb-4'>                                        <input class='form-control form-control-lg UserName' data-val='true' data-val-email='El campo UserName no es una dirección de correo electrónico válida.' data-val-required='El Username es requerido' id='userLogin' name='UserName' placeholder='Usuario:' type='text' value=''>                </div>                <div class='form-group mb-4'>                                        <input class='form-control form-control-lg Pass' data-val='true' data-val-length='El password debe contener un minimo de 8 caracteres' data-val-length-max='12' data-val-length-min='8' data-val-required='El Password es requerido' id='passwordLogin' name='Password' placeholder='Contraseña:' type='password' value=''>                </div>                <a href='/Account/ForgotPassword' class='text-forgot'>¿Olvidaste tu contraseña?</a>            </div>";

            Microsoft.Office.Interop.PowerPoint.Shape shape = slide.Shapes[2];
            slide.Shapes.AddPicture(pictureFileName, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, shape.Left, shape.Top, shape.Width, shape.Height);

            slide.NotesPage.Shapes[2].TextFrame.TextRange.Text = "This demo is created by FPPT using C# - Download free templates from http://FPPT.com";

            


            pptPresentation.SaveAs(@"c:\temp\fppt.pptx", Microsoft.Office.Interop.PowerPoint.PpSaveAsFileType.ppSaveAsDefault, MsoTriState.msoTrue);*/
            //pptPresentation.Close();
            //pptApplication.Quit();


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
