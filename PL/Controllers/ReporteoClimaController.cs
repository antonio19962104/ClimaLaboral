using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class ReporteoClimaController : Controller
    {
        BackGroundJobController BackGroundJobController = new BackGroundJobController();
        ApisController apis = new ApisController();
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
        public ActionResult ReporteComentarios3d()
        {
            return View(new ML.Encuesta());
        }
        public ActionResult ReporteDinamico()
        {
            return View(new ML.Encuesta());
        }

        public JsonResult ReporteDinamicoEE(BL.ReporteD4U.modelRep modelRep)
        {
            var data = new List<object>();
            foreach (var item in modelRep.listUnudadesNeg)
            {
                var aFiltrosHijosEstructura = BackGroundJobController.getEstructuraFromExcelRDinamico(1, modelRep.idBD, item, modelRep.niveles);
                var objComparativoResultadoGeneralPorNivelesEE = apis.getComparativoResultadoGeneralPorNivelesEE("1", item, item, aFiltrosHijosEstructura, modelRep.anio, modelRep.idBD);//ok
                data.Add(objComparativoResultadoGeneralPorNivelesEE.Data);
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ReporteDinamicoEA(BL.ReporteD4U.modelRep modelRep)
        {
            var aFiltrosHijosEstructura = BackGroundJobController.getEstructuraFromExcel(1, modelRep.idBD, modelRep.listUnudadesNeg[0]);
            var objComparativoResultadoGeneralPorNivelesEA = apis.getComparativoResultadoGeneralPorNivelesEA("1", modelRep.listUnudadesNeg[0], modelRep.listUnudadesNeg[0], aFiltrosHijosEstructura, modelRep.anio, modelRep.idBD);//ok
            return Json(objComparativoResultadoGeneralPorNivelesEA, JsonRequestBehavior.AllowGet);
        }
        public static string getFiltro(string f)
        {
            switch (f)
            {
                case "company":
                    return "Comp=>";
                case "area":
                    return "area=>";
                case "departamento":
                    return "Dpto=>";
                case "subdepartamento":
                    return "SubD=>";
                default:
                    return "";
            }
        }
    }
}