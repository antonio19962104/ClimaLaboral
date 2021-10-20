using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    /// <summary>
    /// Controlador del Módulo de Planes de Acción
    /// </summary>
    public class PlanesDeAccionController : Controller
    {
        /// <summary>
        /// Vista de configuración del machote de acciones de mejora
        /// </summary>
        /// <param name="key">UID</param>
        /// <param name="IdEncuesta"></param>
        /// <param name="IdBaseDeDatos"></param>
        /// <param name="AnioAplicacion"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Obtiene las acciones preguardadas
        /// </summary>
        /// <param name="accionDeMejora"></param>
        /// <returns></returns>
        public JsonResult GetAcciones(ML.AccionDeMejora accionDeMejora)
        {
            var result = BL.PlanesDeAccion.GetAcciones(accionDeMejora);
            return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        /// <summary>
        /// Agrega las acciones de mejora
        /// </summary>
        /// <param name="accionDeMejora"></param>
        /// <returns></returns>
        public JsonResult AddAccion(ML.AccionDeMejora accionDeMejora)
        {
            string UsuarioActual = Session["AdminLog"] == null ? "Invitado" : Session["AdminLog"].ToString();
            var result = BL.PlanesDeAccion.AddAccion(accionDeMejora, UsuarioActual);
            return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }
        /// <summary>
        /// Eliminación logica de una acción de mejora
        /// </summary>
        /// <param name="IdAccion"></param>
        /// <returns></returns>
        public JsonResult DeleteAccion(string IdAccion)
        {
            string UsuarioActual = Session["AdminLog"] == null ? "Invitado" : Session["AdminLog"].ToString();
            int Id = string.IsNullOrEmpty(IdAccion) ? 0 : Convert.ToInt32(IdAccion);
            var result = BL.PlanesDeAccion.DeleteAccion(Id, UsuarioActual);
            return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }
        /// <summary>
        /// Re asigna una acción a una nueva categoria, con una nuevo Rango
        /// </summary>
        /// <param name="IdAccion"></param>
        /// <param name="IdCategoria"></param>
        /// <param name="IdRango"></param>
        /// <returns></returns>
        public JsonResult ReAsignar(string IdAccion, string IdCategoria, string IdRango)
        {
            var result = BL.PlanesDeAccion.ReAsignar(IdAccion, IdCategoria, IdRango);
            return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }

    }
}