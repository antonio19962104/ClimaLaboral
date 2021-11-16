using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        #region Generales Modulo
        /// <summary>
        /// Agrega los promedios de cada categoria en una encuesta, base de datos y area especifica
        /// </summary>
        /// <param name="promediosCategorias"></param>
        /// <returns></returns>
        public JsonResult GuardarPromediosPorCategoria(List<ML.PromediosCategorias> promediosCategorias)
        {
            string UsuarioActual = Session["AdminLog"] == null ? "Invitado" : Session["AdminLog"].ToString();
            var result = BL.PlanesDeAccion.GuardarPromediosPorCategoria(promediosCategorias, UsuarioActual);
            return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PlanDeAccion"></param>
        /// <returns></returns>
        public JsonResult AddPlanDeAccion(ML.PlanDeAccion PlanDeAccion) {
            string UsuarioActual = Session["AdminLog"] == null ? "Invitado" : Session["AdminLog"].ToString();
            int IdUsuarioActual = (int)Session["IdAdministradorLogeado"];
            var result = BL.PlanesDeAccion.AddPlanDeAccion(PlanDeAccion, UsuarioActual, IdUsuarioActual);
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
        #endregion Generales Modulo

        #region Seguimiento
        /// <summary>
        /// Muestra la vista del seguimiento de planes de accion
        /// </summary>
        /// <returns></returns>
        public ActionResult Seguimiento()
        {
            Session["IdResponsable"] = 0;
            return View();
        }
        /// <summary>
        /// Obtiene los planes de acción a los que tiene acceso un usuario
        /// </summary>
        /// <returns></returns>
        public JsonResult GetPlanes(string IdResponsable)
        {
            bool IsResponsable = true;
            if (Session["IdAdministradorLogeado"] == null)
                return new JsonResult() { Data = "SessionTimeOut", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            if (IdResponsable == "0")
                IsResponsable = false;
            int UserId = Convert.ToInt32(Session["IdAdministradorLogeado"].ToString());
            var result = BL.PlanesDeAccion.GetPlanes(UserId, IsResponsable, IdResponsable);
            return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        /// <summary>
        /// Obtiene las acciones de un plan de accion en las que el responsable es participante
        /// </summary>
        /// <param name="IdPlan"></param>
        /// <param name="IdResponsable"></param>
        /// <returns></returns>
        public JsonResult GetAccionesByIdResponsable(string IdPlan, string IdResponsable)
        {
            if (string.IsNullOrEmpty(IdPlan) || string.IsNullOrEmpty(IdResponsable))
                return new JsonResult() { Data = "Los parametros no pueden ser nulos o vacios", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            var result = BL.PlanesDeAccion.GetAccionesByIdResponsable(Convert.ToInt32(IdPlan), Convert.ToInt32(IdResponsable));
            return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        /// <summary>
        /// Agrega un conjunto de archivos al seguimiento de una acción
        /// </summary>
        /// <param name="formCollection"></param>
        /// <param name="IdPlan"></param>
        /// <param name="IdAccion"></param>
        /// <returns></returns>
        public JsonResult AgregaArchivosSeguimieto(FormCollection formCollection, string IdPlan = "_IdPlan_1", string IdAccion = "_IdAccion_1")
        {
            ML.Result result = new ML.Result();
            try
            {
                /*
                 * Trabajar directorios por id
                 * Nombre Plan
                 *  Accion
                 *      Responsable 1
                 *          Archivo 1
                 *      Responsable 2
                 *          Archivo 1
                 *  Accion
                 *      Responsable 1
                 *          Archivo 1
                 */
                IdPlan = "IdPlan_" + IdPlan;
                IdAccion = "IdAccion_" + IdAccion;
                string cadenaResponsable;
                if (Session["AdminLog"] != null)
                    cadenaResponsable = "IdResponsable_" + Session["IdResponsable"].ToString();
                else
                    return new JsonResult() { Data = new ML.Result() { Correct = false, ErrorMessage = "El nombre del responsable no puede estar vacio" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                int IdResponsable = Convert.ToInt32(Session["IdAdministradorLogeado"].ToString());
                var ruta = @"\\\\10.5.2.101\\RHDiagnostics\\PlanesDeAccion\\" + IdPlan + @"\\" + IdAccion + @"\\" + cadenaResponsable + @"\\";
                int CantidadArchivos = Request.Files.Count;
                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase itemFile = Request.Files[file];
                    try
                    {
                        if (!Directory.Exists(ruta))
                            Directory.CreateDirectory(ruta);
                        itemFile.SaveAs(Path.Combine(ruta, itemFile.FileName));
                        BL.PlanesDeAccion.GuardarRutaArchivo(ruta + itemFile.FileName, IdPlan, IdAccion, IdResponsable);
                        BL.NLogGeneratorFile.nlogPlanesDeAccion.Info("El archivo " + ruta + @"\\" + itemFile.FileName + " fue agregado correctamente");
                    }
                    catch (Exception aE)
                    {
                        if (aE.Message.Contains("está siendo utilizado en otro proceso"))
                        {
                            if (!Directory.Exists(ruta))
                                Directory.CreateDirectory(ruta);
                            itemFile.SaveAs(Path.Combine(ruta, itemFile.FileName));
                            BL.PlanesDeAccion.GuardarRutaArchivo(ruta + itemFile.FileName, IdPlan, IdAccion, IdResponsable);
                            BL.NLogGeneratorFile.nlogPlanesDeAccion.Info("El archivo " + ruta + @"\\" + itemFile.FileName + " fue agregado correctamente");
                            return Json(true);
                        }
                        BL.NLogGeneratorFile.nlogPlanesDeAccion.Error("El archivo " + ruta + @"\\" + itemFile.FileName + " no pudo agregarse correctamente");
                        BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                        return Json(aE.Message);
                    }
                }
                result.Atachment = BL.PlanesDeAccion.ObtenerAtachments(ruta);
                result.Correct = true;
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
            }
            return Json(result);
        }
        /// <summary>
        /// Eliminacion logica en la tabla dbo.Evidencia y eliminacion del archivo fisico en el servidor
        /// </summary>
        /// <param name="ruta"></param>
        /// <returns></returns>
        public JsonResult EliminarEvidencia(string ruta)
        {
            var result = BL.PlanesDeAccion.EliminarEvidencia(ruta);
            return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        #endregion Seguimiento
    }
}