using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class ReporteD4UController : Controller
    {
        [HttpGet]
        public ActionResult Add()
        {
            List<object> permisosEstructura = new List<object>();
            ViewBag.Permisos = Session["CompaniesPermisos"];
            permisosEstructura = ViewBag.Permisos;
            string companyDelAdminLog = Convert.ToString(Session["CompanyDelAdminLog"]);
            return View(BL.Encuesta.GetEncuestasForAddReporte(permisosEstructura, companyDelAdminLog));//Encuestas a las que tengo permisos
        }
        [HttpPost]
        public ActionResult AddReport(ML.AuxReporte reporte)
        {
            var result = BL.Reporte.CreateReporte(reporte);

            if (result.Correct == true)
            {
                return Json("success");
            }
            else
            {
                return Json("error");
            }
        }
        // GET: ReporteD4U
        public ActionResult Reporte(string IdEncuesta)
        {
            var result = BL.Encuesta.GetDataFromEncuesta(IdEncuesta);
            return View(result);
        }
        public ActionResult ReporteD4U(string IdEncuesta)
        {
            var result = BL.Encuesta.GetDataFromEncuesta(IdEncuesta);
            return View(result);
        }
        [HttpGet]
        public ActionResult GetParticipacion(ML.Encuesta encuesta)
        {
            var result = BL.Reporte.GetParticipacion(encuesta.IdEncuesta);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetPreguntasByIdEncuesta(ML.Encuesta encuesta)
        {
            var result = BL.Reporte.GetPreguntasByIdEncuesta(encuesta.IdEncuesta);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetRespuestasByIdPregunta(ML.Preguntas pregunta)
        {
            var result = BL.Reporte.GetRespuestasByIdPregunta(pregunta);
            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }
        //[HttpGet]
        public JsonResult GetPreguntasLikertExceptDobleByEncuesta(ML.Encuesta encuesta)
        {
            var result = BL.Reporte.GetPreguntasLikertExceptDobleByEncuesta(encuesta.IdEncuesta);
            
            var json = Json(result.Objects);
            json.MaxJsonLength = int.MaxValue;
            //return Json(result, JsonRequestBehavior.AllowGet);
            //return new JsonResult { Data = json, MaxJsonLength = Int32.MaxValue };
            return new JsonResult { Data = json, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }
        public ActionResult GetRespuestasFromPregLikert(ML.Preguntas pregunta)
        {
            var result = BL.Reporte.GetRespuestasFromPregLikert(pregunta.IdPregunta);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public ActionResult GetBase64Table(string base64image)
        {
            Console.WriteLine(base64image);
            ML.Result result = new ML.Result();
            result.Object = base64image;
            return Json("success");
        }
        [HttpGet]
        [ValidateInput(false)]
        public ActionResult GetBase64Table(string data, int st)
        {
            return View(data);
        }
        public ActionResult GetTipoControl(int idPregunta)
        {
            var result = BL.ReporteD4U.GetTipoControl(idPregunta);
            if (result.Correct)
            {
                return Json(result.Object, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("error");
            }
        }
    }
}