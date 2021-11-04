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
        /// Vista del listado de encuestas
        /// </summary>
        /// <returns></returns>
        public ActionResult GetEncuestas()
        {
            return View();
        }
        /// <summary>
        /// Api con el listado de encuestas de clima laboral
        /// </summary>
        /// <returns></returns>
        public JsonResult GetEncuestasClima()
        {
            var result = BL.PlanesDeAccion.GetEncuestasClima();
            return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        /// <summary>
        /// Vista de configuración del machote de acciones de mejora
        /// </summary>
        /// <param name="key">UID</param>
        /// <param name="IdEncuesta"></param>
        /// <param name="IdBaseDeDatos"></param>
        /// <param name="AnioAplicacion"></param>
        /// <returns></returns>
        public ActionResult Index(string key = "0", string IdEncuesta = "0", string IdBaseDeDatos = "0", string AnioAplicacion = "0")
        {
            ML.PlanDeAccion planDeAccion = new ML.PlanDeAccion();
            planDeAccion.IdEncuesta = Convert.ToInt32(IdEncuesta);
            planDeAccion.IdBaseDeDatos = Convert.ToInt32(IdBaseDeDatos);
            planDeAccion.AnioAplicacion = Convert.ToInt32(AnioAplicacion);
            planDeAccion.key = key;
            return View(planDeAccion);
        }
        /// <summary>
        /// Muestra el configurador de notificaciones
        /// </summary>
        /// <returns></returns>
        public ActionResult Notificaciones(string IdPlanDeAccion = "0")
        {
            return View(new ML.PlanDeAccion() { IdPlanDeAccion = Convert.ToInt32(IdPlanDeAccion) });
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
        public JsonResult GetSubCategoriasByIdEncuesta(ML.PromedioSubCategorias promedioSubCategorias)
        {
            var result = BL.PlanesDeAccion.GetSubCategoriasByIdEncuesta(promedioSubCategorias);
            return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }
        /// <summary>
        /// Consulta los promedios generales de cada subcategoria de un area especifica
        /// </summary>
        /// <param name="promedioSubCategorias"></param>
        /// <returns></returns>
        public JsonResult GetPromediosSubCategoriasByAreaAgencia(ML.PromedioSubCategorias promedioSubCategorias)
        {
            var result = BL.PlanesDeAccion.GetPromediosSubCategoriasByAreaAgencia(promedioSubCategorias);
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
        /// Obtiene las acciones de ayuda
        /// </summary>
        /// <param name="accionDeMejora"></param>
        /// <returns></returns>
        public JsonResult GetAccionesAyuda(ML.AccionDeMejora accionDeMejora)
        {
            var result = BL.PlanesDeAccion.GetAccionesAyuda(accionDeMejora);
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
        /// <summary>
        /// Agrega un conjunto de acciones
        /// </summary>
        /// <param name="ListAcciones"></param>
        /// <returns></returns>
        public JsonResult AddAcciones(List<ML.AccionDeMejora> ListAcciones)
        {
            string UsuarioActual = Session["AdminLog"] == null ? "Invitado" : Session["AdminLog"].ToString();
            var result = BL.PlanesDeAccion.AddAcciones(ListAcciones, UsuarioActual);
            return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        /// <summary>
        /// Agrega el perfil de administrador de planes de accion
        /// </summary>
        /// <param name="IdAdmin"></param>
        /// <returns></returns>
        public JsonResult AgregarPerfilPlanesDeAccion(int IdAdmin)
        {
            var result = BL.PlanesDeAccion.AgregarPerfilPlanesDeAccion(IdAdmin);
            return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
		/// <summary>
        /// Se crea un Nuevo Listado de encuestas de tipo Clima laboral para la creación de los Planes de Acción 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create() {           
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonResult GetEncuestaPM() {
            var result = BL.Encuesta.getEncuestasPM();
            return new JsonResult() { Data = result.ListadoDeEncuestas, JsonRequestBehavior = JsonRequestBehavior.AllowGet };        
        }
    }
}