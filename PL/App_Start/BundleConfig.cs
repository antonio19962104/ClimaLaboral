using System.Web;
using System.Web.Optimization;

namespace PL
{
    public class BundleConfig
    {
        // Para obtener más información sobre Bundles, visite http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryAdmin").Include(                        
                "~/scripts/jquery-3.4.1.min.js",
                "~/scripts/popper.min.js",               
                "~/scripts/bootstrap.min.js",
                "~/scripts/summernote/summernote.min.js",
                "~/scripts/custom.js",                    
                "~/scripts/templates-functions.js"
                ));
            bundles.Add(new StyleBundle("~/bundles/cssAdmin").Include(
                "~/css/all.min.css",
                "~/css/animate.min.css",
                "~/css/bootstrap.min.css",
                "~/css/custom-administrator.css",
                "~/css/swiper.min.css",
                "~/css/TextRich/font-awesome.min.css"
                ));
            bundles.Add(new StyleBundle("~/bundles/cssEncuesta").Include(                
                "~/css/all.min.css",
                "~/css/animate.min.css",
                "~/css/bootstrap.min.css",
                "~/css/custom-user.css",
                //"~/css/swiper.min.css",
                "~/css/TextRich/font-awesome.min.css"
                ));
            bundles.Add(new ScriptBundle("~/bundles/jqueryPreview").Include(               
                "~/scripts/jquery-3.4.1.min.js",
                "~/scripts/popper.min.js",
                "~/scripts/bootstrap.min.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/jqueryEncuesta").Include(
               // "~/scripts/jquery-1.12.4.min.js",
                "~/scripts/popper.min.js",
                "~/scripts/bootstrap.min.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/bundles/cssPreView").Include(
                      "~/css/preview/bootstrap.min.css",
                      "~/css/preview/custom-user.css",
                      "~/css/preview/animate.min.css",
                      "~/css/preview/all.min.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryReporteoClima").Include(
                "~/scripts/AngularJS.js",
                "~/scripts/kendo-all.js",
                "~/scripts/sweetalert_2.min.js",
                "~/scripts/wordcloud2.js",
                "~/scripts/ReporteoClima/html2pdf.bundle.min.js",
                "~/scripts/ReporteoClima/LinQ.js",
                "~/scripts/ReporteoClima/kendo.all.min.js"
                ));
            bundles.Add(new StyleBundle("~/bundles/cssReporteoClima").Include(
                "~/css/ReporteoClima/all.min.css",
                "~/css/ReporteoClima/animate.min.css",
                "~/css/ReporteoClima/bootstrap.min.css",
                "~/css/ReporteoClima/custom-administrator.css",
                "~/css/ReporteoClima/lightslider.min.css",
                "~/css/ReporteoClima/summernote.min.css",
                "~/css/ReporteoClima/swiper.min.css",
                "~/css/ReporteoClima/kendo.default-v2.min.css",
                "~/css/kendo-default.css",
                "~/css/sweetalert.min.css",
                "~/css/jQCloud.css",
                "~/css/reporteoClima.css"
                ));

        }
    }
}
