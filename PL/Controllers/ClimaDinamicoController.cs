using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class ClimaDinamicoController : Controller
    {
        /*
         * SaveRespuesta
         * AutoSave
         * changeEstatusEncuestaClima
        */
        // GET: ClimaDinamico
        public ActionResult Login()
        {
            Session.Abandon();
            return View();
        }
        public ActionResult Introduccion()
        {
            string _idEncuesta = Convert.ToString(Session["EncuestaRealizar"]);
            return View(BL.ClimaDinamico.getHtmlIntro(_idEncuesta));
        }
        public ActionResult Encuesta(string aIdusuario, string aIdEncuesta)
        {
            /*
             * Tomar IdEmpleado y IdEncuesta
             * Para determinar en que seccion se colocara al usuario
            */
            return View(BL.ClimaDinamico.getHtmlInstrucciones(aIdEncuesta));
        }
        public ActionResult Thanks(string idEncuesta, string idUsuario, string idBaseDeDatos)
        {
            // change status
            BL.ClimaDinamico.changeEstatusEncuestaClima(idEncuesta, idUsuario, idBaseDeDatos, 3);
            return View(BL.ClimaDinamico.getHtmlAgradecimiento(idEncuesta));
        }
        public JsonResult getPreguntasByIdEncuesta(string aIdEncuesta)
        {
            return Json(BL.ClimaDinamico.getPreguntasByIdEncuesta(aIdEncuesta), JsonRequestBehavior.AllowGet);
        }
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
        public ActionResult getPreguntasFromEncuesta(string aIdEmpleado, string aIdEncuesta)
        {
            var result = BL.ClimaDinamico.getPreguntasFromEncuesta(aIdEmpleado, aIdEncuesta);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveAvance(List<ML.ClimaDinamico> aListRespuestas, string aIdBaseDeDatos, string aIdEncuesta)
        {
            // Ajusar para que reciba cualquier pregunta de x seccion
            aListRespuestas[0].IdBaseDeDatos = Convert.ToInt32(aIdBaseDeDatos);
            aListRespuestas[0].IdEncuesta = Convert.ToInt32(aIdEncuesta);
            var result = BL.ClimaDinamico.SaveRespuesta(aListRespuestas);
            return Json(result);
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
        public JsonResult EnvioEmailNotificacionAFM(string pPlantillaCliente, string pPlantillaAsesor, string pCorreoCliente, string pNombreEmisor, string pCorreoAsesor, string pAsuntoCorreo, string pNomPar, string pValPar)
        {
            var message = new MailMessage();
            //message.From
            message.To.Add(new MailAddress(pCorreoCliente));
            message.Subject = pAsuntoCorreo;
            message.Body = pPlantillaCliente + pPlantillaAsesor;
            message.IsBodyHtml = true;
            message.Bcc.Add(pCorreoAsesor); message.Bcc.Add("jamurillo@grupoautofin.com");
            return Json("success");
        }
    }
}