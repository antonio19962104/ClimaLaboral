using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class EstructuraAFMReporteController : Controller
    {
        // GET: EstructuraAFMReporte
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetPerfilesTipoFuncion(string IdBaseDeDatos)
        {
            return Json(BL.EstructuraAFMReporte.GetPerfilesTipoFuncion(IdBaseDeDatos), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCondicionTrabajo(string IdBaseDeDatos)
        {
            return Json(BL.EstructuraAFMReporte.GetCondicionTrabajo(IdBaseDeDatos), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetGradoAcademico(string IdBaseDeDatos)
        {
            return Json(BL.EstructuraAFMReporte.GetGradoAcademico(IdBaseDeDatos), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAntiguedad(string IdBaseDeDatos)
        {
            return Json(BL.EstructuraAFMReporte.GetAntiguedad(IdBaseDeDatos), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetRangoEdad(string IdBaseDeDatos)
        {
            return Json(BL.EstructuraAFMReporte.GetRangoEdad(IdBaseDeDatos), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetEstructuraDemografica(string IdBaseDeDatos)
        {
            return Json(BL.EstructuraAFMReporte.GetEstructuraDemografica(IdBaseDeDatos), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCompanyCategoria(string IdBaseDeDatos)
        {
            return Json(BL.EstructuraAFMReporte.GetCompanyCategoria(IdBaseDeDatos), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetEstructuraGAFM(string IdBaseDeDatos, [FromBody] List<string> data, [FromBody] List<string> level)
        {
            if (data == null)
                return Json(new List<string>(), JsonRequestBehavior.AllowGet);
            else
                return Json(BL.EstructuraAFMReporte.GetEstructuraGAFM(IdBaseDeDatos, data, level), JsonRequestBehavior.AllowGet);
        }
    }
}