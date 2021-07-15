using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    /// <summary>
    /// Controlador ClimaDinamico
    /// </summary>
    public class ClimaDinamicoController : Controller
    {
        int IdEmpleado = 0;
        int IdEncuesta = 0;
        /*
         * SaveRespuesta
         * AutoSave
         * changeEstatusEncuestaClima
        */
        /// <summary>
        /// Muestra la vista Login
        /// </summary>
        /// <returns>Vista Login</returns>
        public ActionResult Login()
        {
            Session.Abandon();
            return View();
        }
        /// <summary>
        /// Muestra el contenido dinamico de la introduccion de la encuesta
        /// </summary>
        /// <returns>Contenido de la introduccion</returns>
        public ActionResult Introduccion()
        {
            string _idEncuesta = Convert.ToString(Session["EncuestaRealizar"]);
            return View(BL.ClimaDinamico.getHtmlIntro(_idEncuesta));
        }
        //// <summary>/
        /// Se manda el IdEncuesta para la carga de la Intorduccion. CAMOS 08/07/2021
        /// </summary>
        /// <param name="aIdEncuesta">El id de Encuesta para previsualizarla</param>
        /// <returns>La introduccion en HTML</returns>
        public ActionResult IntroduccionPV(string aIdEncuesta)
        {
            return View(BL.ClimaDinamico.getHtmlIntro(aIdEncuesta));
        }
        public ActionResult Encuesta(string aIdusuario, string aIdEncuesta)
        {
            /*
             * Tomar IdEmpleado y IdEncuesta
             * Para determinar en que seccion se colocara al usuario
            */
            return View(BL.ClimaDinamico.getHtmlInstrucciones(aIdEncuesta));
        }
        /// <summary>
        /// Consulta Vista previa de encuesta de clima laboral 09072021 CAMOS
        /// </summary>
        /// <param name="idEncuesta">Es necesario el id de encuesta para consultar la vista previa</param>        
        /// <returns>La encuesta con las preguntas que el Administrador dio de alta</returns>
        public ActionResult EncuestaPV(string aIdEncuesta)
        {         
            return View(BL.ClimaDinamico.getHtmlInstrucciones(aIdEncuesta));
        }
    public ActionResult Thanks(string idEncuesta, string idUsuario, string idBaseDeDatos)
        {
            // change status
            BL.ClimaDinamico.changeEstatusEncuestaClima(idEncuesta, idUsuario, idBaseDeDatos, 3);
            return View(BL.ClimaDinamico.getHtmlAgradecimiento(idEncuesta));
        }
        /// <summary>
        /// Se clona el metodo Thanks para el preView de las encuestas de CLima laboral  --- CAMOS  09072021
        /// </summary>
        /// <param name="aIdEncuesta">Mandamos solo el id Encuesta</param>
        /// <returns>El View de Thanks</returns>
        public ActionResult ThanksVP(string idEncuesta)
        {
            // change status
            //BL.ClimaDinamico.changeEstatusEncuestaClima(idEncuesta, idUsuario, idBaseDeDatos, 3);
            return View(BL.ClimaDinamico.getHtmlAgradecimiento(idEncuesta));
        }
        public JsonResult getPreguntasByIdEncuesta(string aIdEncuesta)
        {
            return Json(BL.ClimaDinamico.getPreguntasByIdEncuesta(aIdEncuesta), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Autenticacion de usuarios de la encuesta
        /// </summary>
        /// <param name="aModel"></param>
        /// <param name="uid"></param>
        /// <returns>Enumerable de statusLogin</returns>
        public ActionResult Autenticar(ML.Empleado aModel, string uid)
        {
            // Session["EmpleadoEncuestado"], Session["EncuestaRealizar"]
            var result = BL.ClimaDinamico.Autenticacion(aModel, uid);
            string statusLogin = Convert.ToString(result.Split('_')[0]);
            // statusLogin = "success";
            if(statusLogin != "success")
                return Json(statusLogin);
            // result = "success_135127_489_158";
            Session["EmpleadoEncuestado"] = Convert.ToInt32(result.Split('_')[1]);
            Session["EncuestaRealizar"] = Convert.ToInt32(result.Split('_')[2]);
            Session["IdBaseDeDatos"] = Convert.ToInt32(result.Split('_')[3]);
            return Json(ML.ClimaDinamico.statusLogin.success);
        }
        /// <summary>
        /// Obtiene las respuestas del usuario especificado
        /// </summary>
        /// <param name="aIdEmpleado"></param>
        /// <param name="aIdEncuesta"></param>
        /// <returns>Listado de respuestas pre guardadas</returns>
        public ActionResult getPreguntasFromEncuesta(string aIdEmpleado, string aIdEncuesta)
        {
            var result = BL.ClimaDinamico.getPreguntasFromEncuesta(aIdEmpleado, aIdEncuesta);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveAvance(List<ML.ClimaDinamico> aListRespuestas, string aIdBaseDeDatos, string aIdEncuesta)
        {
            if (aListRespuestas != null)
            {
                // Ajusar para que reciba cualquier pregunta de x seccion
                aListRespuestas[0].IdBaseDeDatos = Convert.ToInt32(aIdBaseDeDatos);
                aListRespuestas[0].IdEncuesta = Convert.ToInt32(aIdEncuesta);
                var result = BL.ClimaDinamico.SaveRespuesta(aListRespuestas);
                return Json(result);
            }
            else
            {   // cuando cae vacio porque el enfoque area no tiene respuestas, ya esta validado tambien en el front
                return Json(ML.ClimaDinamico.statusGuardado.success);
            }
        }
        [HttpPost]
        public ActionResult AutoSave(ML.ClimaDinamico aClimaDinamico, string aIdBaseDeDatos, string aIdEncuesta)
        {
            // Guardado de un solo reactivo
            aClimaDinamico.IdBaseDeDatos = Convert.ToInt32(aIdBaseDeDatos);
            aClimaDinamico.IdEncuesta = Convert.ToInt32(aIdEncuesta);
            var result = BL.ClimaDinamico.AutoSave(aClimaDinamico);
            return Json(result);
        }
        public ActionResult getHtmlIntro(string aIdEncuesta)
        {
            return Json(BL.ClimaDinamico.getHtmlIntro(aIdEncuesta), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getHtmlInstruccion(string aIdEncuesta)
        {
            return Json(BL.ClimaDinamico.getHtmlInstrucciones(aIdEncuesta), JsonRequestBehavior.AllowGet);
        }
        public JsonResult LogFronEnd (ML.ClimaDinamico aClimaDinamico)
        {
            return Json(BL.NLogGeneratorFile.logErrorFronEnd(aClimaDinamico));
        }
        public JsonResult envioMasivoEmail(ML.ClimaDinamico aClimaDinamico)
        {
            BackgroundJob.Enqueue(() => BL.ClimaDinamico.envioMasivoEmail(aClimaDinamico));
            return Json(true);
        }
        /// <summary>
        /// Obtención de un listado de respuestas pertenecientes a una pregunta
        /// </summary>
        /// <param name="IdPregunta"></param>
        /// <param name="IdEncuesta"></param>
        /// <returns>Lista de objetos del model de Respuestas</returns>
        public JsonResult GetRespuestasByIdPregunta(int IdPregunta, int IdEncuesta)
        {
            if (Session["EmpleadoEncuestado"] != null)
                IdEmpleado = Convert.ToInt32(Session["EmpleadoEncuestado"]);
            return Json(BL.ClimaDinamico.GetRespuestasByIdPregunta(IdPregunta, IdEncuesta, IdEmpleado), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUnidadesNegocio()
        {
            if (Session["EmpleadoEncuestado"] != null)
                IdEmpleado = Convert.ToInt32(Session["EmpleadoEncuestado"]);
            if (Session["EncuestaRealizar"] != null)
                IdEncuesta = Convert.ToInt32(Session["EncuestaRealizar"]);
            return Json(BL.ClimaDinamico.GetUnidadesNegocio(IdEmpleado, IdEncuesta), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCompanies(int IdCompanyCategoria = 0)
        {
            if (Session["EmpleadoEncuestado"] != null)
                IdEmpleado = Convert.ToInt32(Session["EmpleadoEncuestado"]);
            if (Session["EncuestaRealizar"] != null)
                IdEncuesta = Convert.ToInt32(Session["EncuestaRealizar"]);
            return Json(BL.ClimaDinamico.GetCompanies(IdCompanyCategoria, IdEmpleado, IdEncuesta), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAreas(int CompanyId = 0)
        {
            if (Session["EmpleadoEncuestado"] != null)
                IdEmpleado = Convert.ToInt32(Session["EmpleadoEncuestado"]);
            if (Session["EncuestaRealizar"] != null)
                IdEncuesta = Convert.ToInt32(Session["EncuestaRealizar"]);
            return Json(BL.ClimaDinamico.GetArea(CompanyId, IdEmpleado, IdEncuesta), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDepartamentos(int IdArea = 0)
        {
            if (Session["EmpleadoEncuestado"] != null)
                IdEmpleado = Convert.ToInt32(Session["EmpleadoEncuestado"]);
            if (Session["EncuestaRealizar"] != null)
                IdEncuesta = Convert.ToInt32(Session["EncuestaRealizar"]);
            return Json(BL.ClimaDinamico.GetDepartamentos(IdArea, IdEmpleado, IdEncuesta), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSubDepartamentos(int IdDepartamento = 0)
        {
            return Json(BL.ClimaDinamico.GetSubDepartamentos(IdDepartamento), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetRespuestasRBByIdPregunta(int IdPregunta)
        {
            if (Session["EmpleadoEncuestado"] != null)
                IdEmpleado = Convert.ToInt32(Session["EmpleadoEncuestado"]);
            if (Session["EncuestaRealizar"] != null)
                IdEncuesta = Convert.ToInt32(Session["EncuestaRealizar"]);
            return Json(BL.ClimaDinamico.GetRespuestasByIdPreguntaRB(IdPregunta, IdEncuesta, IdEmpleado), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetRespuestas(int IdPregunta)
        {
            if (Session["EmpleadoEncuestado"] != null)
                IdEmpleado = Convert.ToInt32(Session["EmpleadoEncuestado"]);
            return Json(BL.ClimaDinamico.GetRespuesta(IdPregunta, IdEmpleado), JsonRequestBehavior.AllowGet);
        }
    }
}