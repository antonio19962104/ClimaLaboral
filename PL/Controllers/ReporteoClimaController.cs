using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class ReporteoClimaController : Controller
    {
        // GET: ReporteoClima
        public ActionResult configurar()
        {
            return View("ConfigirarReporte");
        }
        public ActionResult Portada()
        {
            return View("~/Views/ReporteoClima/EstructuraReporte/no1_Portada.cshtml");
        }
        public ActionResult Index()
        {
            return View("~/Views/ReporteoClima/IndexReporte.cshtml");
        }
        public ActionResult CargarHistorico()
        {
            return View("~/Views/ReporteoClima/CargarHistorico/CargarHistorico.cshtml");
        }
        public JsonResult cronCrearReporte()
        {
            return Json(true);
        }
    }
}