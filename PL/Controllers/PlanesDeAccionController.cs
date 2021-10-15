using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class PlanesDeAccionController : Controller
    {
        // GET: PlanesDeAccion
        public ActionResult Index(string key, string IdEncuesta, string IdBaseDeDatos, string AnioAplicacion)
        {
            ML.PlanDeAccion planDeAccion = new ML.PlanDeAccion();
            planDeAccion.IdEncuesta = Convert.ToInt32(IdEncuesta);
            planDeAccion.IdBaseDeDatos = Convert.ToInt32(IdBaseDeDatos);
            planDeAccion.AnioAplicacion = Convert.ToInt32(AnioAplicacion);
            planDeAccion.key = key;
            return View(planDeAccion);
        }
        /// <summary>
        /// Obtiene las areas existentes en una bd para crear los planes de accion
        /// </summary>
        /// <param name="IdBaseDeDatos"></param>
        /// <returns></returns>
        public JsonResult GetAreasForPlanAccion(int IdBaseDeDatos)
        {
            var result = BL.EstructuraAFMReporte.GetEstructuraGAFMForPlanesAccion(IdBaseDeDatos);
            return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        /// <summary>
        /// Obtiene los rangos de promedio establecidos
        /// </summary>
        /// <returns></returns>
        public JsonResult GetRangos()
        {
            var result = BL.PlanesDeAccion.GetRangos();
            return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        /// <summary>
        /// Job que genera los promedios generales de cada subcategoria (En desuso porque ya no se invoca manualmente)
        /// </summary>
        /// <param name="promedioSubCategorias"></param>
        /// <returns></returns>
        public JsonResult JobGenerarPromedioSubCategorias(ML.PromedioSubCategorias promedioSubCategorias)
        {
            var result = BL.PlanesDeAccion.JobGenerarPromedioSubCategorias(promedioSubCategorias);
            return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }
        /// <summary>
        /// Consulta los promedios generales de cada subcategoria que generó previamente el job
        /// </summary>
        /// <param name="promedioSubCategorias"></param>
        /// <returns></returns>
        public JsonResult GetPromediosSubCategorias(ML.PromedioSubCategorias promedioSubCategorias)
        {
            var result = BL.PlanesDeAccion.GetPromediosSubCategorias(promedioSubCategorias);
            return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }

    }
}