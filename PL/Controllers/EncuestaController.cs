using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using OfficeOpenXml;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Web.Hosting;
using System.Diagnostics;
using System.Threading;
using System.Web.SessionState;
using System.Text;
using System.Data;
using Newtonsoft.Json;
namespace PL.Controllers
{
    public class EncuestaController : Controller
    {
        Thread emailPadre = new Thread(delegate () { });
        HttpContext contextoPadre = System.Web.HttpContext.Current;
        string mensajeEmailCustom = "";
        ML.Encuesta enc = new ML.Encuesta();
        // GET: Encuesta
        public ActionResult GetAll()
        {
            var result = BL.Encuesta.getEncuestas();
            return View(result);
        }
        public ActionResult Create()
        {
            string idsessionAdmin = Convert.ToString(Session["IdAdministradorLogeado"]);
            string qsIdPlantilla = Request.QueryString["IdPlantilla"];
            ML.Encuesta modelo = new ML.Encuesta();
            //var resulListEmpresa = BL.Company.GetAllCompany();Todas
            List<object> permisosEstructura = new List<object>();
            ViewBag.Permisos = Session["CompaniesPermisos"];
            permisosEstructura = ViewBag.Permisos;

            var miEmpresaOrigen = Convert.ToInt32(Session["CompanyDelAdminLog"]);

            var resulListEmpresa = BL.Empresa.GetAll(permisosEstructura);//GetFiltrado
            var resulListTipoDeEmpresa = BL.TipoEncuesta.getAllTipoEncuesta();
            var listadoPlantillas = BL.Plantillas.getPlantillas(1);
            //var listadoBaseDeDatosAnonima = BL.BasesDeDatos.getBaseDeDatosAnonima();
            //var listadoBaseDeDatosGenerica = BL.BasesDeDatos.getBaseDeDatosGenerica();
            //var listadoBaseDeDatosConfidencial = BL.BasesDeDatos.getBaseDeDatosConfidencial();
            var listadoBaseDeDatosAnonima = BL.BasesDeDatos.getBaseDeDatosAnonimaByPermisos(permisosEstructura, miEmpresaOrigen);
            var listadoBaseDeDatosGenerica = BL.BasesDeDatos.getBaseDeDatosGenericaByPermisos(permisosEstructura, miEmpresaOrigen);
            var listadoBaseDeDatosConfidencial = BL.BasesDeDatos.getBaseDeDatosConfidencialByPermisos(permisosEstructura, miEmpresaOrigen);

            modelo.ListDataBase = listadoBaseDeDatosAnonima.ListadoDeBaseDeDatos;
            modelo.ListDataBaseC = listadoBaseDeDatosConfidencial.ListadoDeBaseDeDatos;
            modelo.ListDataBaseG = listadoBaseDeDatosGenerica.ListadoDeBaseDeDatos;

            modelo.ListDataBase.AddRange(modelo.ListDataBaseC);
            modelo.ListDataBase.AddRange(modelo.ListDataBaseG);

            var listadoEnfoquePregunta = BL.Preguntas.getEnfoquePregunta();
            var listadoCompetenciaPreguntas = BL.Competencia.getCompetencias(idsessionAdmin);
            var listadoTipoControl = BL.TipoControl.getTipoControl();
            modelo.ListTipoEncuesta = resulListTipoDeEmpresa.ListadoTipoEncuesta;
            modelo.ListEmpresas = resulListEmpresa.Objects;
            modelo.ListPlantillas = listadoPlantillas.ListadoDePlantillasPredefinidas;
            modelo.ListEnfoquePregunta = listadoEnfoquePregunta.ListadoEnfoquesPregunta;
            modelo.ListCompetencias = listadoCompetenciaPreguntas.ListadoCompetenciasPregunta;
            modelo.ListTipoControl = listadoTipoControl.ListadoTipoControl;
            modelo.Preguntas = new ML.Preguntas();
            if (qsIdPlantilla != "")
            {
                modelo.Plantillas = new ML.Plantillas();
                modelo.Plantillas.IdPlantilla = Convert.ToInt32(qsIdPlantilla);
            }
            ML.Encuesta enc = new ML.Encuesta();
            return View(modelo);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ML.Encuesta encuesta)
        {
            int configura = 0;
            if (encuesta.PreguntasCondicion == true)
            {
                configura = 1;
                Session["ConfCondicionesEnc"] = 1;
            }
            else
            {
                configura = 0;
                Session["ConfCondicionesEnc"] = 0;
            }

            Console.WriteLine(encuesta.SeccionarEncuesta);
            int ConfigurarSecciones = 0;
            if (encuesta.SeccionarEncuesta == true)
            {
                ConfigurarSecciones = 1;
            }
            else
            {
                ConfigurarSecciones = 0;
            }
            Session["configSections"] = ConfigurarSecciones;

            Session["SeccionarEncuesta"] = encuesta.SeccionarEncuesta;

            var fullUrl = this.Url.Action("Create", "Encuesta", new { encuesta = encuesta }, this.Request.Url.Scheme);
            int usuarioCreacion = Convert.ToInt32(Session["IdAdministradorLogeado"]);
            ML.Result altaEncuesta = BL.Encuesta.Add(encuesta, fullUrl, usuarioCreacion);

            if (altaEncuesta.Correct)
            {
                ViewBag.Message = "La Encuesta se creo correctamente";
                if (encuesta.SeccionarEncuesta == true)
                {
                    Session["IdEncuestaAlta"] = altaEncuesta.idEncuestaAlta;
                    return RedirectToAction("ConfiguraSecciones");
                }
                if (configura == 1 && encuesta.SeccionarEncuesta == false)
                {
                    Session["IdEncuestaAlta"] = altaEncuesta.idEncuestaAlta;
                    return RedirectToAction("ConfigurarCondiciones");
                }
                else if (configura != 1 && encuesta.SeccionarEncuesta == false)
                {
                    return RedirectToAction("GetAll");
                }
            }
            else
            {
                ViewBag.Message = "La Encuesta no se ha podido crear";
                return RedirectToAction("Create");
            }
            return RedirectToAction("GetAll");
        }
        public ActionResult Load()
        {
            string qsIdPlantilla = Request.QueryString["IdPlantilla"];
            ML.Encuesta modelo = new ML.Encuesta();
            //var resulListEmpresa = BL.Company.GetAllCompany();
            List<object> permisosEstructura = new List<object>();
            ViewBag.Permisos = Session["CompaniesPermisos"];
            permisosEstructura = ViewBag.Permisos;
            var resulListEmpresa = BL.Empresa.GetAll(permisosEstructura);//GetFiltrado

            var resulListTipoDeEncuesta = BL.TipoEncuesta.getAllTipoEncuesta();
            var listadoPlantillas = BL.Plantillas.getPlantillas(1);

            //var listadoBaseDeDatosConfidencial = BL.BasesDeDatos.getBaseDeDatosConfidencialByPermisos(permisosEstructura);
            //var listadoBaseDeDatosAnonima = BL.BasesDeDatos.getBaseDeDatosAnonimaByPermisos(permisosEstructura);
            //var listadoBaseDeDatosGenerica = BL.BasesDeDatos.getBaseDeDatosGenericaByPermisos(permisosEstructura);
            //modelo.ListDataBase.AddRange(listadoBaseDeDatosConfidencial.ListadoDeBaseDeDatos);//listadoBaseDeDatos.ListadoDeBaseDeDatos;
            //modelo.ListDataBase.AddRange(listadoBaseDeDatosAnonima.ListadoDeBaseDeDatos);
            //modelo.ListDataBase.AddRange(listadoBaseDeDatosGenerica.ListadoDeBaseDeDatos);
            var miEmpresaOrigen = Convert.ToInt32(Session["CompanyDelAdminLog"]);

            var listadoBaseDeDatosAnonima = BL.BasesDeDatos.getBaseDeDatosAnonimaByPermisos(permisosEstructura, miEmpresaOrigen);
            var listadoBaseDeDatosGenerica = BL.BasesDeDatos.getBaseDeDatosGenericaByPermisos(permisosEstructura, miEmpresaOrigen);
            var listadoBaseDeDatosConfidencial = BL.BasesDeDatos.getBaseDeDatosConfidencialByPermisos(permisosEstructura, miEmpresaOrigen);
            modelo.ListDataBase = listadoBaseDeDatosAnonima.ListadoDeBaseDeDatos;
            modelo.ListDataBaseC = listadoBaseDeDatosConfidencial.ListadoDeBaseDeDatos;
            modelo.ListDataBaseG = listadoBaseDeDatosGenerica.ListadoDeBaseDeDatos;

            modelo.ListDataBase.AddRange(modelo.ListDataBaseC);
            modelo.ListDataBase.AddRange(modelo.ListDataBaseG);

            var listadoEnfoquePregunta = BL.Preguntas.getEnfoquePregunta();
            // var listadoCompetenciaPreguntas = BL.Competencia.getCompetencias();
            var listadoTipoControl = BL.TipoControl.getTipoControl();
            modelo.ListTipoEncuesta = resulListTipoDeEncuesta.ListadoTipoEncuesta;
            modelo.ListEmpresas = resulListEmpresa.Objects;
            modelo.ListPlantillas = listadoPlantillas.ListadoDePlantillasPredefinidas;
            modelo.ListEnfoquePregunta = listadoEnfoquePregunta.ListadoEnfoquesPregunta;
            // modelo.ListCompetencias = listadoCompetenciaPreguntas.ListadoCompetenciasPregunta;
            modelo.ListTipoControl = listadoTipoControl.ListadoTipoControl;
            //modelo.Preguntas = new ML.Preguntas();
            //Session["IdEncuestaAlta"] = 0;
            Session["configSections"] = 0;
            return View(modelo);
        }
        [HttpPost]
        public ActionResult Load(ML.Encuesta encuesta)
        {
            int configura = 0;
            if (encuesta.PreguntasCondicionN > 0)
            {
                configura = 1;
            }

            Console.WriteLine(encuesta.SeccionarEncuesta);
            int ConfigurarSecciones = 0;
            if (encuesta.SeccionarEncuesta == true)
            {
                ConfigurarSecciones = 1;
            }
            else
            {
                ConfigurarSecciones = 0;
            }
            Session["configSections"] = ConfigurarSecciones;

            Session["SeccionarEncuesta"] = encuesta.SeccionarEncuesta;
            var fullUrl = this.Url.Action("Create", "Encuesta", new { encuesta = encuesta }, this.Request.Url.Scheme);
            encuesta.DosColumnas = encuesta.DosColumnasN == 0 ? false : true;
            string decodeHtmlDescripcion = encuesta.Descripcion != null ? System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(encuesta.Descripcion)) : "";
            string decodeHtmlInstruccion = encuesta.Instruccion != null ? System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(encuesta.Instruccion)) : "";
            string decodeHtmlImagenInstruccion = encuesta.ImagenInstruccion != null ? System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(encuesta.ImagenInstruccion)) : "";
            string decodeHtmlAgradecimiento = encuesta.Agradecimiento != null ? System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(encuesta.Agradecimiento)) : "";
            string decodeHtmlImagenAgradecimiento = encuesta.ImagenAgradecimiento != null ? System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(encuesta.ImagenAgradecimiento)) : "";
            encuesta.Descripcion = decodeHtmlDescripcion;
            encuesta.Instruccion = decodeHtmlInstruccion;
            encuesta.ImagenInstruccion = decodeHtmlImagenInstruccion;
            encuesta.Agradecimiento = decodeHtmlAgradecimiento;
            encuesta.ImagenAgradecimiento = decodeHtmlImagenAgradecimiento;
            int currentUsercreacion = Convert.ToInt32(Session["IdAdministradorLogeado"]);
            var result = BL.Encuesta.AddBasico(encuesta, currentUsercreacion);
            if (result.Correct == true)
            {
                if (configura == 1)
                {
                    Session["IdEncuestaAlta"] = result.idEncuestaAlta;
                    //return RedirectToAction("ConfigurarCondiciones");
                    return Json("success");
                }
                else
                {
                    Session["IdEncuestaAlta"] = result.idEncuestaAlta;
                    return Json("success"); }

            }
            else
            {
                return Json("error");
            }

        }
        public ActionResult FillCompany()
        {
            var companies = BL.Company.GetAllCompany();
            return Json(companies, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateNewCuestion()
        {
            string idsessionAdmin = Convert.ToString(Session["IdAdministradorLogeado"]);
            var cuestionViewModel = new ML.Preguntas();
            var listadoTipoControl = BL.TipoControl.getTipoControl();
            var listadoCompetenciaPreguntas = BL.Competencia.getCompetencias(idsessionAdmin);
            var listadoEnfoquePregunta = BL.Preguntas.getEnfoquePregunta();
            cuestionViewModel.ListTipoControl = listadoTipoControl.ListadoTipoControl;
            cuestionViewModel.ListCompetencia = listadoCompetenciaPreguntas.ListadoCompetenciasPregunta;
            cuestionViewModel.ListEnfoque = listadoEnfoquePregunta.ListadoEnfoquesPregunta;
            cuestionViewModel.UniqueId = Guid.NewGuid();
            ViewData["Preguntas"] = cuestionViewModel;
            return PartialView("~/Views/Preguntas/PreguntasAdd.cshtml", cuestionViewModel);
        }
        [HttpPost]
        public ActionResult EditNewCuestion(ML.Preguntas cuestionViewModel)
        {
            string idsessionAdmin = Convert.ToString(Session["IdAdministradorLogeado"]);
            var listadoTipoControl = BL.TipoControl.getTipoControl();
            var listadoCompetenciaPreguntas = BL.Competencia.getCompetencias(idsessionAdmin);
            var listadoEnfoquePregunta = BL.Preguntas.getEnfoquePregunta();
            cuestionViewModel.ListTipoControl = listadoTipoControl.ListadoTipoControl;
            cuestionViewModel.ListCompetencia = listadoCompetenciaPreguntas.ListadoCompetenciasPregunta;
            cuestionViewModel.ListEnfoque = listadoEnfoquePregunta.ListadoEnfoquesPregunta;
            cuestionViewModel.UniqueId = Guid.NewGuid();
            return PartialView("~/Views/Preguntas/PreguntasAdd.cshtml", cuestionViewModel);
        }
        public ActionResult CreateNewAnswereRespuestaCorta()
        {
            var answereViewModel = new ML.Respuestas();
            answereViewModel.Pregunta = new ML.Preguntas();
            answereViewModel.Pregunta.TipoControl = new ML.TipoControl();
            answereViewModel.Pregunta.TipoControl.IdTipoControl = 1;//IdTipodeControl;
            answereViewModel.UniqueId = Guid.NewGuid();
            return PartialView("~/Views/Respuestas/RespuestasAdd.cshtml", answereViewModel);
        }
        public ActionResult CreateNewAnswereRespuestaLarga()
        {
            var answereViewModel = new ML.Respuestas();
            answereViewModel.Pregunta = new ML.Preguntas();
            answereViewModel.Pregunta.TipoControl = new ML.TipoControl();
            answereViewModel.Pregunta.TipoControl.IdTipoControl = 2;//IdTipodeControl;
            answereViewModel.UniqueId = Guid.NewGuid();
            return PartialView("~/Views/Respuestas/RespuestasAdd.cshtml", answereViewModel);
        }
        public ActionResult CreateNewAnswereOpcionMultiple(string idpadreItem)
        {
            var objeto = ViewData["Preguntas"];
            var answereViewModel = new ML.Respuestas();
            answereViewModel.Pregunta = new ML.Preguntas();
            answereViewModel.Pregunta.TipoControl = new ML.TipoControl();
            answereViewModel.Pregunta.TipoControl.IdTipoControl = 3;//IdTipodeControl;
            answereViewModel.UniqueId = Guid.NewGuid();
            answereViewModel.IdPadreObjeto = idpadreItem;
            return PartialView("~/Views/Respuestas/RespuestasAdd.cshtml", answereViewModel);
        }
        public ActionResult CreateNewAnswereCL(string idpadreItem, string respuesta, string idtipocontrol)
        {
            
            var answereViewModel = new ML.Respuestas();
            answereViewModel.Pregunta = new ML.Preguntas();
            answereViewModel.Pregunta.TipoControl = new ML.TipoControl();
            answereViewModel.Pregunta.TipoControl.IdTipoControl = Convert.ToInt32(idtipocontrol);//IdTipodeControl;
            answereViewModel.Respuesta = idtipocontrol == "2"? "Respuesta Larga" : respuesta;
            answereViewModel.UniqueId = Guid.NewGuid();
            answereViewModel.IdPadreObjeto = idpadreItem;
            return PartialView("~/Views/Respuestas/RespuestasAdd.cshtml", answereViewModel);
        }
        public ActionResult CreateNewAnswereChkBox(string idpadreItem)
        {
            var answereViewModel = new ML.Respuestas();
            answereViewModel.Pregunta = new ML.Preguntas();
            answereViewModel.Pregunta.TipoControl = new ML.TipoControl();
            answereViewModel.Pregunta.TipoControl.IdTipoControl = 4;//IdTipodeControl;
            answereViewModel.UniqueId = Guid.NewGuid();
            answereViewModel.IdPadreObjeto = idpadreItem;
            return PartialView("~/Views/Respuestas/RespuestasAdd.cshtml", answereViewModel);
        }
        public ActionResult CreateNewAnswereListaDesplegable(string idpadreItem)
        {
            var answereViewModel = new ML.Respuestas();
            answereViewModel.Pregunta = new ML.Preguntas();
            answereViewModel.Pregunta.TipoControl = new ML.TipoControl();
            answereViewModel.Pregunta.TipoControl.IdTipoControl = 5;//IdTipodeControl;
            answereViewModel.UniqueId = Guid.NewGuid();
            answereViewModel.IdPadreObjeto = idpadreItem;
            return PartialView("~/Views/Respuestas/RespuestasAdd.cshtml", answereViewModel);
        }
        public ActionResult CreateNewAnswereSentimiento()
        {
            var answereViewModel = new ML.Respuestas();
            answereViewModel.Pregunta = new ML.Preguntas();
            answereViewModel.Pregunta.TipoControl = new ML.TipoControl();
            answereViewModel.Pregunta.TipoControl.IdTipoControl = 6;//IdTipodeControl;
            answereViewModel.UniqueId = Guid.NewGuid();
            return PartialView("~/Views/Respuestas/RespuestasAdd.cshtml", answereViewModel);
        }
        public ActionResult CreateNewAnswereRango()
        {
            var answereViewModel = new ML.Respuestas();
            answereViewModel.Pregunta = new ML.Preguntas();
            answereViewModel.Pregunta.TipoControl = new ML.TipoControl();
            answereViewModel.Pregunta.TipoControl.IdTipoControl = 7;//IdTipodeControl;
            answereViewModel.UniqueId = Guid.NewGuid();
            return PartialView("~/Views/Respuestas/RespuestasAdd.cshtml", answereViewModel);
        }

        public ActionResult CreateNewAnswereLikertAcuerdo()
        {
            var answereViewModel = new ML.Respuestas();
            answereViewModel.Pregunta = new ML.Preguntas();
            answereViewModel.Pregunta.TipoControl = new ML.TipoControl();
            answereViewModel.Pregunta.TipoControl.IdTipoControl = 8;//IdTipodeControl;
            answereViewModel.UniqueId = Guid.NewGuid();
            return PartialView("~/Views/Respuestas/RespuestasAdd.cshtml", answereViewModel);
        }
        public ActionResult CreateNewAnswereLikertFrecuencia()
        {
            var answereViewModel = new ML.Respuestas();
            answereViewModel.Pregunta = new ML.Preguntas();
            answereViewModel.Pregunta.TipoControl = new ML.TipoControl();
            answereViewModel.Pregunta.TipoControl.IdTipoControl = 9;//IdTipodeControl;
            answereViewModel.UniqueId = Guid.NewGuid();
            return PartialView("~/Views/Respuestas/RespuestasAdd.cshtml", answereViewModel);
        }
        public ActionResult CreateNewAnswereLikertImportacia()
        {
            var answereViewModel = new ML.Respuestas();
            answereViewModel.Pregunta = new ML.Preguntas();
            answereViewModel.Pregunta.TipoControl = new ML.TipoControl();
            answereViewModel.Pregunta.TipoControl.IdTipoControl = 10;//IdTipodeControl;
            answereViewModel.UniqueId = Guid.NewGuid();
            return PartialView("~/Views/Respuestas/RespuestasAdd.cshtml", answereViewModel);
        }
        public ActionResult CreateNewAnswereLikertProbabilidad()
        {
            var answereViewModel = new ML.Respuestas();
            answereViewModel.Pregunta = new ML.Preguntas();
            answereViewModel.Pregunta.TipoControl = new ML.TipoControl();
            answereViewModel.Pregunta.TipoControl.IdTipoControl = 11;//IdTipodeControl;
            answereViewModel.UniqueId = Guid.NewGuid();
            return PartialView("~/Views/Respuestas/RespuestasAdd.cshtml", answereViewModel);
        }
        public ActionResult CreateNewAnswereLikertDoble(string idpadreItem, int columna)
        {
            var answereViewModel = new ML.Respuestas();
            answereViewModel.Verdadero = columna;
            answereViewModel.Pregunta = new ML.Preguntas();
            answereViewModel.Pregunta.TipoControl = new ML.TipoControl();
            answereViewModel.Pregunta.TipoControl.IdTipoControl = 12;//IdTipodeControl;
            answereViewModel.UniqueId = Guid.NewGuid();
            answereViewModel.IdPadreObjeto = idpadreItem;
            return PartialView("~/Views/Respuestas/RespuestasAdd.cshtml", answereViewModel);
        }
        public ActionResult Delete(ML.Encuesta encuesta)
        {
            var result = BL.Encuesta.Delete(encuesta.IdEncuesta);
            if (result.Correct)
            {
                ViewBag.Message = "La Plantilla se ha eliminado";
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                ViewBag.Message = "No se ha podido eliminar la Plantilla";
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Preview(int idEncuesta)
        {
            ML.Result respuesta = BL.Encuesta.getPreviewEncuesta(idEncuesta);
            if (respuesta.EditaEncuesta == null)
            {
                return RedirectToAction("GetAll");
            }
            else {
                return View(respuesta.EditaEncuesta);
            }

        }
        public ActionResult e(string u)
        {
            var browser = this.Request.Browser;
            ML.Result respuesta = BL.Encuesta.e(u);
            //valida fecha fin de la encuesta
            if (respuesta.Correct)
            {
                return View(respuesta.EditaEncuesta);
            }
            else
            {
                return RedirectToAction("Login", "Encuesta", new { e = 0 });
            }
        }
        [HttpPost]
        [ValidateInput(true)]
        public ActionResult e(ML.Encuesta respuestas)
        {
            //20859
            respuestas.UsuarioCreacion = Convert.ToString(Session["IdUsuarioLog"]);
            if ((respuestas.UsuarioCreacion == "" && respuestas.MLTipoEncuesta.IdTipoEncuesta == 2) || (respuestas.UsuarioCreacion == "" && respuestas.MLTipoEncuesta.IdTipoEncuesta == 3))
            {
                return RedirectToAction("Login", "Encuesta", new { e = 999998888 });//LAS ANONIMAS PASAN SIN IDUSUARIO
            }

            ML.Result altaRespuestas = BL.Encuesta.AddRespuestas(respuestas);


            if (altaRespuestas.Correct == false && altaRespuestas.ErrorMessage == "reload")
            {
                return Redirect(Request.UrlReferrer.ToString());
            }

            int IdUsuario = Convert.ToInt32(respuestas.UsuarioCreacion);
            var browser = this.Request.Browser;
            var ip = this.Request.UserHostAddress;
            BL.Usuario.saveDeviceFinal(respuestas.IdEncuesta, IdUsuario, browser.IsMobileDevice, browser.Version, browser.Browser, ip);
            
            if (altaRespuestas.Correct)
            {
                var mensajeAgradecimiento = BL.Encuesta.GetAgradecimientoEncuesta(respuestas.IdEncuesta);
                //Set Estatus 3 Terminada
                int IdEncuesta = respuestas.IdEncuesta;
                int Idusuario = Convert.ToInt32(Session["IdUsuarioLog"]);
                if (respuestas.MLTipoEncuesta.IdTipoEncuesta == 1)
                {
                    var updateStatusAnonima = BL.Usuario.insertEstatusEncuesta(IdEncuesta, altaRespuestas.IdusuarioForAnonima, 3);
                }
                else
                {
                    var updateEstatus = BL.Usuario.updateEstatusEncuesta(IdEncuesta, Idusuario, 3);
                }
                ViewBag.Message = "La encuesta se envio correctamente";
                TempData["Agradecimiento"] = mensajeAgradecimiento.EditaEncuesta;
                return RedirectToAction("ThankyouPage", "Encuesta");
            }
            else
            {
                if (altaRespuestas.ErrorMessage == "Respondio")
                {
                    ViewBag.Message = "Ya respondio la encuesta";
                }
                else
                {
                    ViewBag.Message = "Problemas con la encuesta, intente más tarde";
                }
                return RedirectToAction("Login", "Encuesta", new { e = 0 });
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult DemoAutoSave(ML.Preguntas respuestas)
        {
            var result = new ML.Result();
            int Idusuario = 0;
            try
            {
                Idusuario = Convert.ToInt32(Session["IdUsuarioLog"]);
                if (Idusuario == 0)
                {
                    return Json("SessionTimeOut");
                }
            }
            catch (Exception)
            {
                return Json("SessionTimeOut");
            }
            var existe = BL.Encuesta.existeRespuesta(respuestas.IdEncuesta, Idusuario);
            if (existe.Correct == false)
            {
                return Json("Error server");
            }
            if (existe.Exist && respuestas.TipoControl != null && respuestas.Acceso == 1)
            {
                switch (respuestas.TipoControl.IdTipoControl)
                {
                    case 1://textarea
                        if (respuestas.IdPregunta == 0 || String.IsNullOrEmpty(respuestas.MLRespuestas.Respuesta))
                        {
                            if(respuestas.MLRespuestas == null) { respuestas.MLRespuestas = new ML.Respuestas(); respuestas.MLRespuestas.Respuesta = ""; }
                            if (respuestas.MLRespuestas.Respuesta == null) { respuestas.MLRespuestas.Respuesta = ""; }
                            BL.Encuesta.writeLogIdResCero(respuestas.IdEncuesta, Idusuario, respuestas, "Respuesta del objeto: " + respuestas.MLRespuestas.Respuesta);
                            return Json("reload");
                        }
                        result = BL.Encuesta.UpdateRespuestaT1(respuestas, respuestas.IdEncuesta, Idusuario);
                        break;
                    case 2://radio Likert
                        if (respuestas.MLRespuestas.IdRespuesta == 0 || respuestas.IdPregunta == 0) {
                            BL.Encuesta.writeLogIdResCero(respuestas.IdEncuesta, Idusuario, respuestas);
                            return Json("reload");
                        }
                        result = BL.Encuesta.UpdateRespuestaT2(respuestas, respuestas.IdEncuesta, Idusuario);
                        break;
                    case 3://Likert Doble
                        if (respuestas.IdPregunta == 0 || String.IsNullOrEmpty(respuestas.MLRespuestas.Respuesta))
                        {
                            BL.Encuesta.writeLogIdResCero(respuestas.IdEncuesta, Idusuario, respuestas);
                            return Json("reload");
                        }
                        result = BL.Encuesta.UpdateRespuestaTLikertD(respuestas, respuestas.IdEncuesta, Idusuario);
                        break;
                    case 4://checkbox
                        if (respuestas.MLRespuestas.IdRespuesta == 0 || respuestas.IdPregunta == 0) {
                            BL.Encuesta.writeLogIdResCero(respuestas.IdEncuesta, Idusuario, respuestas);
                            return Json("reload");
                        }
                        result = BL.Encuesta.UpdateRespuestaTCheck(respuestas, respuestas.IdEncuesta, Idusuario);
                        break;
                    default:
                        BL.Encuesta.writeLogIdResCero(respuestas.IdEncuesta, Idusuario, respuestas, "No se encontró un caso para autoguardar");
                        return Json("reload");
                        break;
                }
            }
            else if(existe.Exist == false && respuestas.TipoControl == null)
            {
                result = BL.Encuesta.AddRespuestasVacio(respuestas.IdEncuesta, Idusuario);
                if (result.Correct == false)
                {
                    return Json("Error server");
                }
            }
            //
            if (result.Correct && existe.Correct)
            {
                return Json("success");
            }
            else
            {
                if(result.ErrorMessage == null) { result.ErrorMessage = "Ha ocurrido un error"; }
                return Json(result.ErrorMessage);
            }
        }
        [HttpGet]
        public ActionResult ThankyouPage()
        {
            //FormsAuthentication.SignOut();

            var model = TempData["Agradecimiento"];
            if (model == null)
            {
                return RedirectToAction("Login", "Encuesta", new { e = 999999888 });
            }
            Session.Abandon();
            return View(model);
        }
        [HttpPost]
        public ActionResult PreviewThankyouPage(ML.Encuesta preview)
        {
            var mensajeAgradecimiento = BL.Encuesta.GetAgradecimientoEncuesta(preview.IdEncuesta);
            TempData["Agradecimiento"] = mensajeAgradecimiento.EditaEncuesta;
            return RedirectToAction("ThankyouPage", "Encuesta");
        }
        public ActionResult Edit(int idEncuesta)
        {
            string idsessionAdmin = Convert.ToString(Session["IdAdministradorLogeado"]);
            var consultaEncuestaTipo = BL.Encuesta.getEncuestaTipoById(idEncuesta); 
            if (idEncuesta == 1)
            {
                return RedirectToAction("EditClimaLab");
            }
            if (consultaEncuestaTipo == 4)
            {
                ML.Encuesta encuestaCL = new ML.Encuesta();
                encuestaCL.IdEncuesta = idEncuesta;
                //ML.Result encuestaCL = BL.Encuesta.getEncuestaByIdEdit(idEncuesta);
                //return View(encuestaCL.EditaEncuesta);
                return RedirectToAction("EditCL","Encuesta",new { idEncuestaCL = idEncuesta.ToString()});
            }

            ML.Result encuesta = BL.Encuesta.getEncuestaByIdEditCL(idEncuesta,idsessionAdmin);
            return View(encuesta.EditaEncuesta);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ML.Encuesta encuesta)
        {
            encuesta.UsuarioModificacion = Convert.ToString(Session["IdUsuarioLog"]);
            string usrLog = Convert.ToString(Session["AdminLog"]);
            ML.Result altaEncuesta = BL.Encuesta.Edit(encuesta, usrLog);
            Session["IdEncuestaUpdateSecciones"] = encuesta.IdEncuesta;
            Session["IdEncuestaUpdateBifurcacion"] = encuesta.IdEncuesta;

            //Primero mandar a las bifuraccaiones
            if (encuesta.PreguntasCondicion == true)
            {
                Session["EditConfigCond"] = 1;
            }
            else
            {
                Session["EditConfigCond"] = 0;
            }

            //EditCondiciones
            if (altaEncuesta.Correct)
            {
                if (encuesta.SeccionarEncuesta == true)
                {
                    //Mandaa editBifurcacion
                    return RedirectToAction("EditSecciones"); 
                }
                else if (encuesta.PreguntasCondicion == true)
                {
                    return RedirectToAction("EditCondiciones");
                }
                else
                {
                    ViewBag.Message = "La Encuesta se creo correctamente";
                    return RedirectToAction("GetAll");
                }
            }
            else
            {
                ViewBag.Message = "La Encuesta no se ha podido crear";
                return RedirectToAction("Create");
            }

        }
        public ActionResult DownloadLayout()
        {
            string file = "LayaoutEncuesta.xlsx";
            string fullPath = Path.Combine(Server.MapPath("~/resources/"), file);
            return File(fullPath, "application/vnd.ms-excel", file);
        }
        //JAMG
        public ActionResult AddEncuestaLayout(FormCollection formCollection)
        {
            int IdPreguntaSubSeccion = 0;
            var idempleado = Session["IdEmpleadoLog"];
            int lastEncuestaBD = 0;
            using (DL.RH_DesEntities contextLast = new DL.RH_DesEntities())
            {
                lastEncuestaBD = contextLast.Encuesta.Max(p => p.IdEncuesta);
            }
            var preguntasList = new List<DL.Preguntas>();
            var respuestasList = new List<DL.Respuestas>();
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["encuestaFile"];
                if ((file != null) && (file.ContentLength > 0) && (!string.IsNullOrEmpty(file.FileName)))
                {
                    string fileName = file.FileName;
                    string fileExtension = Path.GetExtension(file.FileName);
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    if (fileExtension == ".xlsx")
                    {
                        using (var package = new ExcelPackage(file.InputStream))
                        {
                            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                            var currentSheet = package.Workbook.Worksheets;
                            var workSheet = currentSheet.First();
                            var noOfCol = workSheet.Dimension.End.Column;
                            var noOfRow = workSheet.Dimension.End.Row;
                            if (noOfRow > 1)
                            {
                                for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                                {
                                    //Version antes de validacion de celdas fantasma
                                    bool obligatoria;
                                    //obligatoria = true;

                                    try
                                    {
                                        var obligaXls = (workSheet.Cells[rowIterator, 4].Value).ToString();
                                        if (obligaXls == "Si" || obligaXls == "SI") { obligatoria = true; } else { obligatoria = false; }
                                    }
                                    catch (Exception ex)
                                    {
                                        break;
                                    }

                                    var ObtenIdTipoDeControl = (workSheet.Cells[rowIterator, 5].Value).ToString();//String

                                    string TextoTipoControl = ObtenIdTipoDeControl.ToUpper();


                                    int IdTipoDeControl = 0;
                                    switch (TextoTipoControl)
                                    {
                                        case "RESPUESTA CORTA": IdTipoDeControl = 1; break;
                                        case "RESPUESTA LARGA": IdTipoDeControl = 2; break;
                                        case "OPCIÓN MULTIPLE": IdTipoDeControl = 3; break;
                                        case "CASILLA DE VERIFICACIÓN": IdTipoDeControl = 4; break;
                                        case "LISTA DESPLEGABLE": IdTipoDeControl = 5; break;
                                        case "SENTIMIENTO": IdTipoDeControl = 6; break;
                                        case "RANGO": IdTipoDeControl = 7; break;
                                        case "LIKERT ACUERDO": IdTipoDeControl = 8; break;
                                        case "LIKERT FRECUENCIA": IdTipoDeControl = 9; break;
                                        case "LIKERT IMPORTANCIA": IdTipoDeControl = 10; break;
                                        case "LIKERT PROBABILIDAD": IdTipoDeControl = 11; break;
                                        case "LIKERT DOBLE": IdTipoDeControl = 12; break;
                                        case "SUB SECCION": IdTipoDeControl = 13; break;

                                    }
                                    using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                                    {
                                        using (DbContextTransaction transaction = context.Database.BeginTransaction())
                                        {
                                            try
                                            {

                                                if (IdTipoDeControl == 13)
                                                    {
                                                        goto casoSubSeccion;
                                                    }
                                                var preguntas = new DL.Preguntas();
                                                preguntas.idEncuesta = lastEncuestaBD;
                                                preguntas.Seccion = Convert.ToInt32((workSheet.Cells[rowIterator, 1].Value));
                                                if (workSheet.Cells[rowIterator, 2].Value != null)
                                                {
                                                    preguntas.EncabezadoSeccion = (workSheet.Cells[rowIterator, 2].Value).ToString();
                                                }
                                                else
                                                {
                                                    preguntas.EncabezadoSeccion = "";
                                                }                                                
                                                preguntas.Pregunta = (workSheet.Cells[rowIterator, 3].Value).ToString();                                                
                                                preguntas.IdTipoControl = IdTipoDeControl;
                                                preguntas.Obligatoria = obligatoria;
                                                preguntas.IdEstatus = 1;
                                                preguntas.Valoracion = 0;
                                                preguntas.FechaHoraCreacion = DateTime.Now;
                                                preguntas.UsuarioCreacion = idempleado.ToString();
                                                preguntas.ProgramaCreacion = "Importa Encuesta";
                                                preguntas.Enfoque = "";
                                                preguntas.RespuestaCondicion = "";
                                                preguntas.PreguntasCondicion = "";
                                                context.Preguntas.Add(preguntas);
                                                context.SaveChanges();
                                                int idPregunta = context.Preguntas.Max(q => q.IdPregunta);
                                                if (preguntas.IdTipoControl == 13)
                                                {
                                                    IdPreguntaSubSeccion = idPregunta;
                                                }
                                                var update = context.Database.ExecuteSqlCommand("UPDATE PREGUNTAS SET SUBSECCION = {0} WHERE IDPREGUNTA = {1}", IdPreguntaSubSeccion, idPregunta);
                                                /***Inserte respuesta***/
                                                if (IdTipoDeControl != 1 && IdTipoDeControl != 2 && IdTipoDeControl != 6 && IdTipoDeControl != 7 && IdTipoDeControl != 8 && IdTipoDeControl != 9 && IdTipoDeControl != 10 && IdTipoDeControl != 11)
                                                {
                                                    if (workSheet.Cells[rowIterator, 6].Value != null)
                                                    {
                                                        if (IdTipoDeControl !=12)
                                                        {
                                                            var respuesta = new DL.Respuestas();
                                                            respuesta.IdPregunta = idPregunta;
                                                            respuesta.Respuesta = (workSheet.Cells[rowIterator, 6].Value).ToString();
                                                            respuesta.IdEstatus = 1;
                                                            respuesta.FechaHoraCreacion = DateTime.Now;
                                                            respuesta.UsuarioCreacion = idempleado.ToString();
                                                            respuesta.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(respuesta);
                                                            context.SaveChanges();
                                                        }
                                                       
                                                    }
                                                    if (workSheet.Cells[rowIterator, 7].Value != null)
                                                    {
                                                        if (IdTipoDeControl != 12)
                                                        {
                                                            var respuesta = new DL.Respuestas();
                                                            respuesta.IdPregunta = idPregunta;
                                                            respuesta.Respuesta = (workSheet.Cells[rowIterator, 7].Value).ToString();
                                                            respuesta.IdEstatus = 1;
                                                            respuesta.FechaHoraCreacion = DateTime.Now;
                                                            respuesta.UsuarioCreacion = idempleado.ToString();
                                                            respuesta.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(respuesta);
                                                            context.SaveChanges();
                                                        }
                                                        
                                                    }
                                                    if (workSheet.Cells[rowIterator, 8].Value != null)
                                                    {
                                                        if (IdTipoDeControl != 12)
                                                        {
                                                            var respuesta = new DL.Respuestas();
                                                            respuesta.IdPregunta = idPregunta;
                                                            respuesta.Respuesta = (workSheet.Cells[rowIterator, 8].Value).ToString();
                                                            respuesta.IdEstatus = 1;
                                                            respuesta.FechaHoraCreacion = DateTime.Now;
                                                            respuesta.UsuarioCreacion = idempleado.ToString();
                                                            respuesta.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(respuesta);
                                                            context.SaveChanges();
                                                        }
                                                        else
                                                        {
                                                            //Inserta pregunta likert doble
                                                           var preguntaLikert = new DL.PreguntasLikert();
                                                            preguntaLikert.idPregunta = idPregunta;
                                                            preguntaLikert.idEncuesta = lastEncuestaBD;
                                                            preguntaLikert.Pregunta = (workSheet.Cells[rowIterator, 8].Value).ToString();
                                                            preguntaLikert.IdEstatus = 1;
                                                            preguntaLikert.FechaHoraCreacion = DateTime.Now;
                                                            preguntaLikert.UsuarioCreacion = idempleado.ToString();
                                                            preguntaLikert.ProgramaCreacion = "Importa Encuesta";
                                                            context.PreguntasLikert.Add(preguntaLikert);
                                                            context.SaveChanges();
                                                            //Max Preguntas Likert Doble
                                                            int idPreguntasLikert = context.PreguntasLikert.Max(q => q.idPreguntasLikert);
                                                            //Inserta Respuesta por index 0 e index 1
                                                            var queryRespuestaColA = new DL.Respuestas();//Respuesta, IdPregunta,IdPreguntaLikertD
                                                            queryRespuestaColA.Respuesta = (workSheet.Cells[rowIterator, 6].Value).ToString();
                                                            queryRespuestaColA.IdPregunta = idPregunta;
                                                            queryRespuestaColA.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColA.IdEstatus = 1;
                                                            queryRespuestaColA.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColA.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColA.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColA);
                                                            context.SaveChanges();

                                                            var queryRespuestaColB = new DL.Respuestas();
                                                            queryRespuestaColB.Respuesta = (workSheet.Cells[rowIterator, 7].Value).ToString();
                                                            queryRespuestaColB.IdPregunta = idPregunta;
                                                            queryRespuestaColB.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColB.IdEstatus = 1;
                                                            queryRespuestaColB.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColB.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColB.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColB);
                                                            context.SaveChanges();

                                                        }

                                                    }
                                                    if (workSheet.Cells[rowIterator, 9].Value != null)
                                                    {
                                                        if (IdTipoDeControl != 12)
                                                        {
                                                            var respuesta = new DL.Respuestas();
                                                            respuesta.IdPregunta = idPregunta;
                                                            respuesta.Respuesta = (workSheet.Cells[rowIterator, 9].Value).ToString();
                                                            respuesta.IdEstatus = 1;
                                                            respuesta.FechaHoraCreacion = DateTime.Now;
                                                            respuesta.UsuarioCreacion = idempleado.ToString();
                                                            respuesta.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(respuesta);
                                                            context.SaveChanges();
                                                        }
                                                        else
                                                        {
                                                            //Inserta pregunta likert doble
                                                            var preguntaLikert = new DL.PreguntasLikert();
                                                            preguntaLikert.idPregunta = idPregunta;
                                                            preguntaLikert.idEncuesta = lastEncuestaBD;
                                                            preguntaLikert.Pregunta = (workSheet.Cells[rowIterator, 9].Value).ToString();
                                                            preguntaLikert.IdEstatus = 1;
                                                            preguntaLikert.FechaHoraCreacion = DateTime.Now;
                                                            preguntaLikert.UsuarioCreacion = idempleado.ToString();
                                                            preguntaLikert.ProgramaCreacion = "Importa Encuesta";
                                                            context.PreguntasLikert.Add(preguntaLikert);
                                                            context.SaveChanges();
                                                            //Max Preguntas Likert Doble
                                                            int idPreguntasLikert = context.PreguntasLikert.Max(q => q.idPreguntasLikert);
                                                            //Inserta Respuesta por index 0 e index 1
                                                            var queryRespuestaColA = new DL.Respuestas();//Respuesta, IdPregunta,IdPreguntaLikertD
                                                            queryRespuestaColA.Respuesta = (workSheet.Cells[rowIterator, 6].Value).ToString();
                                                            queryRespuestaColA.IdPregunta = idPregunta;
                                                            queryRespuestaColA.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColA.IdEstatus = 1;
                                                            queryRespuestaColA.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColA.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColA.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColA);
                                                            context.SaveChanges();

                                                            var queryRespuestaColB = new DL.Respuestas();
                                                            queryRespuestaColB.Respuesta = (workSheet.Cells[rowIterator, 7].Value).ToString();
                                                            queryRespuestaColB.IdPregunta = idPregunta;
                                                            queryRespuestaColB.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColB.IdEstatus = 1;
                                                            queryRespuestaColB.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColB.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColB.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColB);
                                                            context.SaveChanges();
                                                        }
                                                            
                                                    }
                                                    if (workSheet.Cells[rowIterator, 10].Value != null)
                                                    {
                                                        if (IdTipoDeControl != 12)
                                                        {
                                                            var respuesta = new DL.Respuestas();
                                                            respuesta.IdPregunta = idPregunta;
                                                            respuesta.Respuesta = (workSheet.Cells[rowIterator, 10].Value).ToString();
                                                            respuesta.IdEstatus = 1;
                                                            respuesta.FechaHoraCreacion = DateTime.Now;
                                                            respuesta.UsuarioCreacion = idempleado.ToString();
                                                            respuesta.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(respuesta);
                                                            context.SaveChanges();
                                                        }
                                                        else
                                                        {
                                                            //Inserta pregunta likert doble
                                                            var preguntaLikert = new DL.PreguntasLikert();
                                                            preguntaLikert.idPregunta = idPregunta;
                                                            preguntaLikert.idEncuesta = lastEncuestaBD;
                                                            preguntaLikert.Pregunta = (workSheet.Cells[rowIterator, 10].Value).ToString();
                                                            preguntaLikert.IdEstatus = 1;
                                                            preguntaLikert.FechaHoraCreacion = DateTime.Now;
                                                            preguntaLikert.UsuarioCreacion = idempleado.ToString();
                                                            preguntaLikert.ProgramaCreacion = "Importa Encuesta";
                                                            context.PreguntasLikert.Add(preguntaLikert);
                                                            context.SaveChanges();
                                                            //Max Preguntas Likert Doble
                                                            int idPreguntasLikert = context.PreguntasLikert.Max(q => q.idPreguntasLikert);
                                                            //Inserta Respuesta por index 0 e index 1
                                                            var queryRespuestaColA = new DL.Respuestas();//Respuesta, IdPregunta,IdPreguntaLikertD
                                                            queryRespuestaColA.Respuesta = (workSheet.Cells[rowIterator, 6].Value).ToString();
                                                            queryRespuestaColA.IdPregunta = idPregunta;
                                                            queryRespuestaColA.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColA.IdEstatus = 1;
                                                            queryRespuestaColA.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColA.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColA.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColA);
                                                            context.SaveChanges();

                                                            var queryRespuestaColB = new DL.Respuestas();
                                                            queryRespuestaColB.Respuesta = (workSheet.Cells[rowIterator, 7].Value).ToString();
                                                            queryRespuestaColB.IdPregunta = idPregunta;
                                                            queryRespuestaColB.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColB.IdEstatus = 1;
                                                            queryRespuestaColB.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColB.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColB.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColB);
                                                            context.SaveChanges();
                                                        } 
                                                        
                                                    }
                                                    if (workSheet.Cells[rowIterator, 11].Value != null)
                                                    {
                                                        if (IdTipoDeControl != 12)
                                                        {
                                                            var respuesta = new DL.Respuestas();
                                                            respuesta.IdPregunta = idPregunta;
                                                            respuesta.Respuesta = (workSheet.Cells[rowIterator, 11].Value).ToString();
                                                            respuesta.IdEstatus = 1;
                                                            respuesta.FechaHoraCreacion = DateTime.Now;
                                                            respuesta.UsuarioCreacion = idempleado.ToString();
                                                            respuesta.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(respuesta);
                                                            context.SaveChanges();
                                                        }
                                                        else
                                                        {
                                                            //Inserta pregunta likert doble
                                                            var preguntaLikert = new DL.PreguntasLikert();
                                                            preguntaLikert.idPregunta = idPregunta;
                                                            preguntaLikert.idEncuesta = lastEncuestaBD;
                                                            preguntaLikert.Pregunta = (workSheet.Cells[rowIterator, 11].Value).ToString();
                                                            preguntaLikert.IdEstatus = 1;
                                                            preguntaLikert.FechaHoraCreacion = DateTime.Now;
                                                            preguntaLikert.UsuarioCreacion = idempleado.ToString();
                                                            preguntaLikert.ProgramaCreacion = "Importa Encuesta";
                                                            context.PreguntasLikert.Add(preguntaLikert);
                                                            context.SaveChanges();
                                                            //Max Preguntas Likert Doble
                                                            int idPreguntasLikert = context.PreguntasLikert.Max(q => q.idPreguntasLikert);
                                                            //Inserta Respuesta por index 0 e index 1
                                                            var queryRespuestaColA = new DL.Respuestas();//Respuesta, IdPregunta,IdPreguntaLikertD
                                                            queryRespuestaColA.Respuesta = (workSheet.Cells[rowIterator, 6].Value).ToString();
                                                            queryRespuestaColA.IdPregunta = idPregunta;
                                                            queryRespuestaColA.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColA.IdEstatus = 1;
                                                            queryRespuestaColA.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColA.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColA.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColA);
                                                            context.SaveChanges();

                                                            var queryRespuestaColB = new DL.Respuestas();
                                                            queryRespuestaColB.Respuesta = (workSheet.Cells[rowIterator, 7].Value).ToString();
                                                            queryRespuestaColB.IdPregunta = idPregunta;
                                                            queryRespuestaColB.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColB.IdEstatus = 1;
                                                            queryRespuestaColB.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColB.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColB.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColB);
                                                            context.SaveChanges();
                                                        }
                                                        
                                                    }
                                                    if (workSheet.Cells[rowIterator, 12].Value != null)
                                                    {
                                                        if (IdTipoDeControl != 12)
                                                        {
                                                            var respuesta = new DL.Respuestas();
                                                            respuesta.IdPregunta = idPregunta;
                                                            respuesta.Respuesta = (workSheet.Cells[rowIterator, 12].Value).ToString();
                                                            respuesta.IdEstatus = 1;
                                                            respuesta.FechaHoraCreacion = DateTime.Now;
                                                            respuesta.UsuarioCreacion = idempleado.ToString();
                                                            respuesta.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(respuesta);
                                                            context.SaveChanges();
                                                        }
                                                        else
                                                        {
                                                            //Inserta pregunta likert doble
                                                            var preguntaLikert = new DL.PreguntasLikert();
                                                            preguntaLikert.idPregunta = idPregunta;
                                                            preguntaLikert.idEncuesta = lastEncuestaBD;
                                                            preguntaLikert.Pregunta = (workSheet.Cells[rowIterator, 12].Value).ToString();
                                                            preguntaLikert.IdEstatus = 1;
                                                            preguntaLikert.FechaHoraCreacion = DateTime.Now;
                                                            preguntaLikert.UsuarioCreacion = idempleado.ToString();
                                                            preguntaLikert.ProgramaCreacion = "Importa Encuesta";
                                                            context.PreguntasLikert.Add(preguntaLikert);
                                                            context.SaveChanges();
                                                            //Max Preguntas Likert Doble
                                                            int idPreguntasLikert = context.PreguntasLikert.Max(q => q.idPreguntasLikert);
                                                            //Inserta Respuesta por index 0 e index 1
                                                            var queryRespuestaColA = new DL.Respuestas();//Respuesta, IdPregunta,IdPreguntaLikertD
                                                            queryRespuestaColA.Respuesta = (workSheet.Cells[rowIterator, 6].Value).ToString();
                                                            queryRespuestaColA.IdPregunta = idPregunta;
                                                            queryRespuestaColA.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColA.IdEstatus = 1;
                                                            queryRespuestaColA.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColA.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColA.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColA);
                                                            context.SaveChanges();

                                                            var queryRespuestaColB = new DL.Respuestas();
                                                            queryRespuestaColB.Respuesta = (workSheet.Cells[rowIterator, 7].Value).ToString();
                                                            queryRespuestaColB.IdPregunta = idPregunta;
                                                            queryRespuestaColB.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColB.IdEstatus = 1;
                                                            queryRespuestaColB.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColB.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColB.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColB);
                                                            context.SaveChanges();
                                                        }
                                                            
                                                    }
                                                    if (workSheet.Cells[rowIterator, 13].Value != null)
                                                    {
                                                        if (IdTipoDeControl != 12)
                                                        {
                                                            var respuesta = new DL.Respuestas();
                                                            respuesta.IdPregunta = idPregunta;
                                                            respuesta.Respuesta = (workSheet.Cells[rowIterator, 13].Value).ToString();
                                                            respuesta.IdEstatus = 1;
                                                            respuesta.FechaHoraCreacion = DateTime.Now;
                                                            respuesta.UsuarioCreacion = idempleado.ToString();
                                                            respuesta.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(respuesta);
                                                            context.SaveChanges();
                                                        }
                                                        else
                                                        {
                                                            //Inserta pregunta likert doble
                                                            var preguntaLikert = new DL.PreguntasLikert();
                                                            preguntaLikert.idPregunta = idPregunta;
                                                            preguntaLikert.idEncuesta = lastEncuestaBD;
                                                            preguntaLikert.Pregunta = (workSheet.Cells[rowIterator, 13].Value).ToString();
                                                            preguntaLikert.IdEstatus = 1;
                                                            preguntaLikert.FechaHoraCreacion = DateTime.Now;
                                                            preguntaLikert.UsuarioCreacion = idempleado.ToString();
                                                            preguntaLikert.ProgramaCreacion = "Importa Encuesta";
                                                            context.PreguntasLikert.Add(preguntaLikert);
                                                            context.SaveChanges();
                                                            //Max Preguntas Likert Doble
                                                            int idPreguntasLikert = context.PreguntasLikert.Max(q => q.idPreguntasLikert);
                                                            //Inserta Respuesta por index 0 e index 1
                                                            var queryRespuestaColA = new DL.Respuestas();//Respuesta, IdPregunta,IdPreguntaLikertD
                                                            queryRespuestaColA.Respuesta = (workSheet.Cells[rowIterator, 6].Value).ToString();
                                                            queryRespuestaColA.IdPregunta = idPregunta;
                                                            queryRespuestaColA.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColA.IdEstatus = 1;
                                                            queryRespuestaColA.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColA.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColA.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColA);
                                                            context.SaveChanges();

                                                            var queryRespuestaColB = new DL.Respuestas();
                                                            queryRespuestaColB.Respuesta = (workSheet.Cells[rowIterator, 7].Value).ToString();
                                                            queryRespuestaColB.IdPregunta = idPregunta;
                                                            queryRespuestaColB.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColB.IdEstatus = 1;
                                                            queryRespuestaColB.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColB.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColB.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColB);
                                                            context.SaveChanges();
                                                        }
                                                           
                                                    }
                                                    if (workSheet.Cells[rowIterator, 14].Value != null)
                                                    {

                                                        if (IdTipoDeControl != 12)
                                                        {
                                                            var respuesta = new DL.Respuestas();
                                                            respuesta.IdPregunta = idPregunta;
                                                            respuesta.Respuesta = (workSheet.Cells[rowIterator, 14].Value).ToString();
                                                            respuesta.IdEstatus = 1;
                                                            respuesta.FechaHoraCreacion = DateTime.Now;
                                                            respuesta.UsuarioCreacion = idempleado.ToString();
                                                            respuesta.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(respuesta);
                                                            context.SaveChanges();
                                                        }
                                                        else
                                                        {
                                                            //Inserta pregunta likert doble
                                                            var preguntaLikert = new DL.PreguntasLikert();
                                                            preguntaLikert.idPregunta = idPregunta;
                                                            preguntaLikert.idEncuesta = lastEncuestaBD;
                                                            preguntaLikert.Pregunta = (workSheet.Cells[rowIterator, 14].Value).ToString();
                                                            preguntaLikert.IdEstatus = 1;
                                                            preguntaLikert.FechaHoraCreacion = DateTime.Now;
                                                            preguntaLikert.UsuarioCreacion = idempleado.ToString();
                                                            preguntaLikert.ProgramaCreacion = "Importa Encuesta";
                                                            context.PreguntasLikert.Add(preguntaLikert);
                                                            context.SaveChanges();
                                                            //Max Preguntas Likert Doble
                                                            int idPreguntasLikert = context.PreguntasLikert.Max(q => q.idPreguntasLikert);
                                                            //Inserta Respuesta por index 0 e index 1
                                                            var queryRespuestaColA = new DL.Respuestas();//Respuesta, IdPregunta,IdPreguntaLikertD
                                                            queryRespuestaColA.Respuesta = (workSheet.Cells[rowIterator, 6].Value).ToString();
                                                            queryRespuestaColA.IdPregunta = idPregunta;
                                                            queryRespuestaColA.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColA.IdEstatus = 1;
                                                            queryRespuestaColA.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColA.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColA.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColA);
                                                            context.SaveChanges();

                                                            var queryRespuestaColB = new DL.Respuestas();
                                                            queryRespuestaColB.Respuesta = (workSheet.Cells[rowIterator, 7].Value).ToString();
                                                            queryRespuestaColB.IdPregunta = idPregunta;
                                                            queryRespuestaColB.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColB.IdEstatus = 1;
                                                            queryRespuestaColB.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColB.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColB.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColB);
                                                            context.SaveChanges();
                                                        }
                                                           
                                                    }
                                                    if (workSheet.Cells[rowIterator, 15].Value != null)
                                                    {
                                                        if (IdTipoDeControl != 12)
                                                        {
                                                            var respuesta = new DL.Respuestas();
                                                            respuesta.IdPregunta = idPregunta;
                                                            respuesta.Respuesta = (workSheet.Cells[rowIterator, 15].Value).ToString();
                                                            respuesta.IdEstatus = 1;
                                                            respuesta.FechaHoraCreacion = DateTime.Now;
                                                            respuesta.UsuarioCreacion = idempleado.ToString();
                                                            respuesta.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(respuesta);
                                                            context.SaveChanges();
                                                        }
                                                        else
                                                        {
                                                            //Inserta pregunta likert doble
                                                            var preguntaLikert = new DL.PreguntasLikert();
                                                            preguntaLikert.idPregunta = idPregunta;
                                                            preguntaLikert.idEncuesta = lastEncuestaBD;
                                                            preguntaLikert.Pregunta = (workSheet.Cells[rowIterator, 15].Value).ToString();
                                                            preguntaLikert.IdEstatus = 1;
                                                            preguntaLikert.FechaHoraCreacion = DateTime.Now;
                                                            preguntaLikert.UsuarioCreacion = idempleado.ToString();
                                                            preguntaLikert.ProgramaCreacion = "Importa Encuesta";
                                                            context.PreguntasLikert.Add(preguntaLikert);
                                                            context.SaveChanges();
                                                            //Max Preguntas Likert Doble
                                                            int idPreguntasLikert = context.PreguntasLikert.Max(q => q.idPreguntasLikert);
                                                            //Inserta Respuesta por index 0 e index 1
                                                            var queryRespuestaColA = new DL.Respuestas();//Respuesta, IdPregunta,IdPreguntaLikertD
                                                            queryRespuestaColA.Respuesta = (workSheet.Cells[rowIterator, 6].Value).ToString();
                                                            queryRespuestaColA.IdPregunta = idPregunta;
                                                            queryRespuestaColA.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColA.IdEstatus = 1;
                                                            queryRespuestaColA.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColA.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColA.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColA);
                                                            context.SaveChanges();

                                                            var queryRespuestaColB = new DL.Respuestas();
                                                            queryRespuestaColB.Respuesta = (workSheet.Cells[rowIterator, 7].Value).ToString();
                                                            queryRespuestaColB.IdPregunta = idPregunta;
                                                            queryRespuestaColB.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColB.IdEstatus = 1;
                                                            queryRespuestaColB.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColB.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColB.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColB);
                                                            context.SaveChanges();
                                                        }
                                                            
                                                    }
                                                    if (workSheet.Cells[rowIterator, 16].Value != null)
                                                    {
                                                        if (IdTipoDeControl != 12)
                                                        {
                                                            var respuesta = new DL.Respuestas();
                                                            respuesta.IdPregunta = idPregunta;
                                                            respuesta.Respuesta = (workSheet.Cells[rowIterator, 16].Value).ToString();
                                                            respuesta.IdEstatus = 1;
                                                            respuesta.FechaHoraCreacion = DateTime.Now;
                                                            respuesta.UsuarioCreacion = idempleado.ToString();
                                                            respuesta.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(respuesta);
                                                            context.SaveChanges();
                                                        }
                                                        else
                                                        {
                                                            //Inserta pregunta likert doble
                                                            var preguntaLikert = new DL.PreguntasLikert();
                                                            preguntaLikert.idPregunta = idPregunta;
                                                            preguntaLikert.idEncuesta = lastEncuestaBD;
                                                            preguntaLikert.Pregunta = (workSheet.Cells[rowIterator, 16].Value).ToString();
                                                            preguntaLikert.IdEstatus = 1;
                                                            preguntaLikert.FechaHoraCreacion = DateTime.Now;
                                                            preguntaLikert.UsuarioCreacion = idempleado.ToString();
                                                            preguntaLikert.ProgramaCreacion = "Importa Encuesta";
                                                            context.PreguntasLikert.Add(preguntaLikert);
                                                            context.SaveChanges();
                                                            //Max Preguntas Likert Doble
                                                            int idPreguntasLikert = context.PreguntasLikert.Max(q => q.idPreguntasLikert);
                                                            //Inserta Respuesta por index 0 e index 1
                                                            var queryRespuestaColA = new DL.Respuestas();//Respuesta, IdPregunta,IdPreguntaLikertD
                                                            queryRespuestaColA.Respuesta = (workSheet.Cells[rowIterator, 6].Value).ToString();
                                                            queryRespuestaColA.IdPregunta = idPregunta;
                                                            queryRespuestaColA.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColA.IdEstatus = 1;
                                                            queryRespuestaColA.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColA.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColA.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColA);
                                                            context.SaveChanges();

                                                            var queryRespuestaColB = new DL.Respuestas();
                                                            queryRespuestaColB.Respuesta = (workSheet.Cells[rowIterator, 7].Value).ToString();
                                                            queryRespuestaColB.IdPregunta = idPregunta;
                                                            queryRespuestaColB.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColB.IdEstatus = 1;
                                                            queryRespuestaColB.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColB.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColB.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColB);
                                                            context.SaveChanges();
                                                        }
                                                            
                                                    }
                                                    if (workSheet.Cells[rowIterator, 17].Value != null)
                                                    {
                                                        if (IdTipoDeControl != 12)
                                                        {
                                                            var respuesta = new DL.Respuestas();
                                                            respuesta.IdPregunta = idPregunta;
                                                            respuesta.Respuesta = (workSheet.Cells[rowIterator, 17].Value).ToString();
                                                            respuesta.IdEstatus = 1;
                                                            respuesta.FechaHoraCreacion = DateTime.Now;
                                                            respuesta.UsuarioCreacion = idempleado.ToString();
                                                            respuesta.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(respuesta);
                                                            context.SaveChanges();
                                                        }
                                                        else
                                                        {
                                                            //Inserta pregunta likert doble
                                                            var preguntaLikert = new DL.PreguntasLikert();
                                                            preguntaLikert.idPregunta = idPregunta;
                                                            preguntaLikert.idEncuesta = lastEncuestaBD;
                                                            preguntaLikert.Pregunta = (workSheet.Cells[rowIterator, 17].Value).ToString();
                                                            preguntaLikert.IdEstatus = 1;
                                                            preguntaLikert.FechaHoraCreacion = DateTime.Now;
                                                            preguntaLikert.UsuarioCreacion = idempleado.ToString();
                                                            preguntaLikert.ProgramaCreacion = "Importa Encuesta";
                                                            context.PreguntasLikert.Add(preguntaLikert);
                                                            context.SaveChanges();
                                                            //Max Preguntas Likert Doble
                                                            int idPreguntasLikert = context.PreguntasLikert.Max(q => q.idPreguntasLikert);
                                                            //Inserta Respuesta por index 0 e index 1
                                                            var queryRespuestaColA = new DL.Respuestas();//Respuesta, IdPregunta,IdPreguntaLikertD
                                                            queryRespuestaColA.Respuesta = (workSheet.Cells[rowIterator, 6].Value).ToString();
                                                            queryRespuestaColA.IdPregunta = idPregunta;
                                                            queryRespuestaColA.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColA.IdEstatus = 1;
                                                            queryRespuestaColA.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColA.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColA.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColA);
                                                            context.SaveChanges();

                                                            var queryRespuestaColB = new DL.Respuestas();
                                                            queryRespuestaColB.Respuesta = (workSheet.Cells[rowIterator, 7].Value).ToString();
                                                            queryRespuestaColB.IdPregunta = idPregunta;
                                                            queryRespuestaColB.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColB.IdEstatus = 1;
                                                            queryRespuestaColB.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColB.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColB.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColB);
                                                            context.SaveChanges();
                                                        }
                                                            
                                                    }
                                                    if (workSheet.Cells[rowIterator, 18].Value != null)
                                                    {
                                                        if (IdTipoDeControl != 12)
                                                        {
                                                            var respuesta = new DL.Respuestas();
                                                            respuesta.IdPregunta = idPregunta;
                                                            respuesta.Respuesta = (workSheet.Cells[rowIterator, 18].Value).ToString();
                                                            respuesta.IdEstatus = 1;
                                                            respuesta.FechaHoraCreacion = DateTime.Now;
                                                            respuesta.UsuarioCreacion = idempleado.ToString();
                                                            respuesta.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(respuesta);
                                                            context.SaveChanges();
                                                        }
                                                        else
                                                        {
                                                            //Inserta pregunta likert doble
                                                            var preguntaLikert = new DL.PreguntasLikert();
                                                            preguntaLikert.idPregunta = idPregunta;
                                                            preguntaLikert.idEncuesta = lastEncuestaBD;
                                                            preguntaLikert.Pregunta = (workSheet.Cells[rowIterator, 18].Value).ToString();
                                                            preguntaLikert.IdEstatus = 1;
                                                            preguntaLikert.FechaHoraCreacion = DateTime.Now;
                                                            preguntaLikert.UsuarioCreacion = idempleado.ToString();
                                                            preguntaLikert.ProgramaCreacion = "Importa Encuesta";
                                                            context.PreguntasLikert.Add(preguntaLikert);
                                                            context.SaveChanges();
                                                            //Max Preguntas Likert Doble
                                                            int idPreguntasLikert = context.PreguntasLikert.Max(q => q.idPreguntasLikert);
                                                            //Inserta Respuesta por index 0 e index 1
                                                            var queryRespuestaColA = new DL.Respuestas();//Respuesta, IdPregunta,IdPreguntaLikertD
                                                            queryRespuestaColA.Respuesta = (workSheet.Cells[rowIterator, 6].Value).ToString();
                                                            queryRespuestaColA.IdPregunta = idPregunta;
                                                            queryRespuestaColA.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColA.IdEstatus = 1;
                                                            queryRespuestaColA.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColA.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColA.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColA);
                                                            context.SaveChanges();

                                                            var queryRespuestaColB = new DL.Respuestas();
                                                            queryRespuestaColB.Respuesta = (workSheet.Cells[rowIterator, 7].Value).ToString();
                                                            queryRespuestaColB.IdPregunta = idPregunta;
                                                            queryRespuestaColB.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColB.IdEstatus = 1;
                                                            queryRespuestaColB.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColB.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColB.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColB);
                                                            context.SaveChanges();
                                                        }
                                                            
                                                    }
                                                    if (workSheet.Cells[rowIterator, 19].Value != null)
                                                    {
                                                        if (IdTipoDeControl != 12)
                                                        {
                                                            var respuesta = new DL.Respuestas();
                                                            respuesta.IdPregunta = idPregunta;
                                                            respuesta.Respuesta = (workSheet.Cells[rowIterator, 19].Value).ToString();
                                                            respuesta.IdEstatus = 1;
                                                            respuesta.FechaHoraCreacion = DateTime.Now;
                                                            respuesta.UsuarioCreacion = idempleado.ToString();
                                                            respuesta.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(respuesta);
                                                            context.SaveChanges();
                                                        }
                                                        else
                                                        {
                                                            //Inserta pregunta likert doble
                                                            var preguntaLikert = new DL.PreguntasLikert();
                                                            preguntaLikert.idPregunta = idPregunta;
                                                            preguntaLikert.idEncuesta = lastEncuestaBD;
                                                            preguntaLikert.Pregunta = (workSheet.Cells[rowIterator, 19].Value).ToString();
                                                            preguntaLikert.IdEstatus = 1;
                                                            preguntaLikert.FechaHoraCreacion = DateTime.Now;
                                                            preguntaLikert.UsuarioCreacion = idempleado.ToString();
                                                            preguntaLikert.ProgramaCreacion = "Importa Encuesta";
                                                            context.PreguntasLikert.Add(preguntaLikert);
                                                            context.SaveChanges();
                                                            //Max Preguntas Likert Doble
                                                            int idPreguntasLikert = context.PreguntasLikert.Max(q => q.idPreguntasLikert);
                                                            //Inserta Respuesta por index 0 e index 1
                                                            var queryRespuestaColA = new DL.Respuestas();//Respuesta, IdPregunta,IdPreguntaLikertD
                                                            queryRespuestaColA.Respuesta = (workSheet.Cells[rowIterator, 6].Value).ToString();
                                                            queryRespuestaColA.IdPregunta = idPregunta;
                                                            queryRespuestaColA.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColA.IdEstatus = 1;
                                                            queryRespuestaColA.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColA.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColA.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColA);
                                                            context.SaveChanges();

                                                            var queryRespuestaColB = new DL.Respuestas();
                                                            queryRespuestaColB.Respuesta = (workSheet.Cells[rowIterator, 7].Value).ToString();
                                                            queryRespuestaColB.IdPregunta = idPregunta;
                                                            queryRespuestaColB.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColB.IdEstatus = 1;
                                                            queryRespuestaColB.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColB.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColB.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColB);
                                                            context.SaveChanges();
                                                        }
                                                            
                                                    }
                                                    if (workSheet.Cells[rowIterator, 20].Value != null)
                                                    {
                                                        if (IdTipoDeControl != 12)
                                                        {
                                                            var respuesta = new DL.Respuestas();
                                                            respuesta.IdPregunta = idPregunta;
                                                            respuesta.Respuesta = (workSheet.Cells[rowIterator, 20].Value).ToString();
                                                            respuesta.IdEstatus = 1;
                                                            respuesta.FechaHoraCreacion = DateTime.Now;
                                                            respuesta.UsuarioCreacion = idempleado.ToString();
                                                            respuesta.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(respuesta);
                                                            context.SaveChanges();
                                                        }
                                                        else
                                                        {
                                                            //Inserta pregunta likert doble
                                                            var preguntaLikert = new DL.PreguntasLikert();
                                                            preguntaLikert.idPregunta = idPregunta;
                                                            preguntaLikert.idEncuesta = lastEncuestaBD;
                                                            preguntaLikert.Pregunta = (workSheet.Cells[rowIterator, 20].Value).ToString();
                                                            preguntaLikert.IdEstatus = 1;
                                                            preguntaLikert.FechaHoraCreacion = DateTime.Now;
                                                            preguntaLikert.UsuarioCreacion = idempleado.ToString();
                                                            preguntaLikert.ProgramaCreacion = "Importa Encuesta";
                                                            context.PreguntasLikert.Add(preguntaLikert);
                                                            context.SaveChanges();
                                                            //Max Preguntas Likert Doble
                                                            int idPreguntasLikert = context.PreguntasLikert.Max(q => q.idPreguntasLikert);
                                                            //Inserta Respuesta por index 0 e index 1
                                                            var queryRespuestaColA = new DL.Respuestas();//Respuesta, IdPregunta,IdPreguntaLikertD
                                                            queryRespuestaColA.Respuesta = (workSheet.Cells[rowIterator, 6].Value).ToString();
                                                            queryRespuestaColA.IdPregunta = idPregunta;
                                                            queryRespuestaColA.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColA.IdEstatus = 1;
                                                            queryRespuestaColA.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColA.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColA.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColA);
                                                            context.SaveChanges();

                                                            var queryRespuestaColB = new DL.Respuestas();
                                                            queryRespuestaColB.Respuesta = (workSheet.Cells[rowIterator, 7].Value).ToString();
                                                            queryRespuestaColB.IdPregunta = idPregunta;
                                                            queryRespuestaColB.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColB.IdEstatus = 1;
                                                            queryRespuestaColB.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColB.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColB.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColB);
                                                            context.SaveChanges();
                                                        }
                                                        
                                                    }
                                                    if (workSheet.Cells[rowIterator, 21].Value != null)
                                                    {
                                                        if (IdTipoDeControl != 12)
                                                        {
                                                            var respuesta = new DL.Respuestas();
                                                            respuesta.IdPregunta = idPregunta;
                                                            respuesta.Respuesta = (workSheet.Cells[rowIterator, 21].Value).ToString();
                                                            respuesta.IdEstatus = 1;
                                                            respuesta.FechaHoraCreacion = DateTime.Now;
                                                            respuesta.UsuarioCreacion = idempleado.ToString();
                                                            respuesta.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(respuesta);
                                                            context.SaveChanges();
                                                        }
                                                        else
                                                        {
                                                            //Inserta pregunta likert doble
                                                            var preguntaLikert = new DL.PreguntasLikert();
                                                            preguntaLikert.idPregunta = idPregunta;
                                                            preguntaLikert.idEncuesta = lastEncuestaBD;
                                                            preguntaLikert.Pregunta = (workSheet.Cells[rowIterator, 21].Value).ToString();
                                                            preguntaLikert.IdEstatus = 1;
                                                            preguntaLikert.FechaHoraCreacion = DateTime.Now;
                                                            preguntaLikert.UsuarioCreacion = idempleado.ToString();
                                                            preguntaLikert.ProgramaCreacion = "Importa Encuesta";
                                                            context.PreguntasLikert.Add(preguntaLikert);
                                                            context.SaveChanges();
                                                            //Max Preguntas Likert Doble
                                                            int idPreguntasLikert = context.PreguntasLikert.Max(q => q.idPreguntasLikert);
                                                            //Inserta Respuesta por index 0 e index 1
                                                            var queryRespuestaColA = new DL.Respuestas();//Respuesta, IdPregunta,IdPreguntaLikertD
                                                            queryRespuestaColA.Respuesta = (workSheet.Cells[rowIterator, 6].Value).ToString();
                                                            queryRespuestaColA.IdPregunta = idPregunta;
                                                            queryRespuestaColA.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColA.IdEstatus = 1;
                                                            queryRespuestaColA.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColA.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColA.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColA);
                                                            context.SaveChanges();

                                                            var queryRespuestaColB = new DL.Respuestas();
                                                            queryRespuestaColB.Respuesta = (workSheet.Cells[rowIterator, 7].Value).ToString();
                                                            queryRespuestaColB.IdPregunta = idPregunta;
                                                            queryRespuestaColB.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColB.IdEstatus = 1;
                                                            queryRespuestaColB.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColB.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColB.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColB);
                                                            context.SaveChanges();
                                                        }
                                                        
                                                    }
                                                    if (workSheet.Cells[rowIterator, 22].Value != null)
                                                    {
                                                        if (IdTipoDeControl != 12)
                                                        {
                                                            var respuesta = new DL.Respuestas();
                                                            respuesta.IdPregunta = idPregunta;
                                                            respuesta.Respuesta = (workSheet.Cells[rowIterator, 22].Value).ToString();
                                                            respuesta.IdEstatus = 1;
                                                            respuesta.FechaHoraCreacion = DateTime.Now;
                                                            respuesta.UsuarioCreacion = idempleado.ToString();
                                                            respuesta.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(respuesta);
                                                            context.SaveChanges();
                                                        }
                                                        else
                                                        {
                                                            //Inserta pregunta likert doble
                                                            var preguntaLikert = new DL.PreguntasLikert();
                                                            preguntaLikert.idPregunta = idPregunta;
                                                            preguntaLikert.idEncuesta = lastEncuestaBD;
                                                            preguntaLikert.Pregunta = (workSheet.Cells[rowIterator, 22].Value).ToString();
                                                            preguntaLikert.IdEstatus = 1;
                                                            preguntaLikert.FechaHoraCreacion = DateTime.Now;
                                                            preguntaLikert.UsuarioCreacion = idempleado.ToString();
                                                            preguntaLikert.ProgramaCreacion = "Importa Encuesta";
                                                            context.PreguntasLikert.Add(preguntaLikert);
                                                            context.SaveChanges();
                                                            //Max Preguntas Likert Doble
                                                            int idPreguntasLikert = context.PreguntasLikert.Max(q => q.idPreguntasLikert);
                                                            //Inserta Respuesta por index 0 e index 1
                                                            var queryRespuestaColA = new DL.Respuestas();//Respuesta, IdPregunta,IdPreguntaLikertD
                                                            queryRespuestaColA.Respuesta = (workSheet.Cells[rowIterator, 6].Value).ToString();
                                                            queryRespuestaColA.IdPregunta = idPregunta;
                                                           queryRespuestaColA.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColA.IdEstatus = 1;
                                                            queryRespuestaColA.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColA.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColA.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColA);
                                                            context.SaveChanges();

                                                            var queryRespuestaColB = new DL.Respuestas();
                                                            queryRespuestaColB.Respuesta = (workSheet.Cells[rowIterator, 7].Value).ToString();
                                                            queryRespuestaColB.IdPregunta = idPregunta;
                                                            queryRespuestaColB.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColB.IdEstatus = 1;
                                                            queryRespuestaColB.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColB.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColB.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColB);
                                                            context.SaveChanges();
                                                        }
                                                        
                                                    }
                                                    if (workSheet.Cells[rowIterator, 23].Value != null)
                                                    {
                                                        if (IdTipoDeControl != 12)
                                                        {
                                                            var respuesta = new DL.Respuestas();
                                                            respuesta.IdPregunta = idPregunta;
                                                            respuesta.Respuesta = (workSheet.Cells[rowIterator, 23].Value).ToString();
                                                            respuesta.IdEstatus = 1;
                                                            respuesta.FechaHoraCreacion = DateTime.Now;
                                                            respuesta.UsuarioCreacion = idempleado.ToString();
                                                            respuesta.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(respuesta);
                                                            context.SaveChanges();
                                                        }
                                                        else
                                                        {
                                                            //Inserta pregunta likert doble
                                                            var preguntaLikert = new DL.PreguntasLikert();
                                                            preguntaLikert.idPregunta = idPregunta;
                                                            preguntaLikert.idEncuesta = lastEncuestaBD;
                                                            preguntaLikert.Pregunta = (workSheet.Cells[rowIterator, 23].Value).ToString();
                                                            preguntaLikert.IdEstatus = 1;
                                                            preguntaLikert.FechaHoraCreacion = DateTime.Now;
                                                            preguntaLikert.UsuarioCreacion = idempleado.ToString();
                                                            preguntaLikert.ProgramaCreacion = "Importa Encuesta";
                                                            context.PreguntasLikert.Add(preguntaLikert);
                                                            context.SaveChanges();
                                                            //Max Preguntas Likert Doble
                                                            int idPreguntasLikert = context.PreguntasLikert.Max(q => q.idPreguntasLikert);
                                                            //Inserta Respuesta por index 0 e index 1
                                                            var queryRespuestaColA = new DL.Respuestas();//Respuesta, IdPregunta,IdPreguntaLikertD
                                                            queryRespuestaColA.Respuesta = (workSheet.Cells[rowIterator, 6].Value).ToString();
                                                            queryRespuestaColA.IdPregunta = idPregunta;
                                                            queryRespuestaColA.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColA.IdEstatus = 1;
                                                            queryRespuestaColA.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColA.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColA.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColA);
                                                            context.SaveChanges();

                                                            var queryRespuestaColB = new DL.Respuestas();
                                                            queryRespuestaColB.Respuesta = (workSheet.Cells[rowIterator, 7].Value).ToString();
                                                            queryRespuestaColB.IdPregunta = idPregunta;
                                                            queryRespuestaColB.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColB.IdEstatus = 1;
                                                            queryRespuestaColB.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColB.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColB.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColB);
                                                            context.SaveChanges();
                                                        }
                                                        
                                                    }
                                                    if (workSheet.Cells[rowIterator, 24].Value != null)
                                                    {
                                                        if (IdTipoDeControl != 12)
                                                        {
                                                            var respuesta = new DL.Respuestas();
                                                            respuesta.IdPregunta = idPregunta;
                                                            respuesta.Respuesta = (workSheet.Cells[rowIterator, 24].Value).ToString();
                                                            respuesta.IdEstatus = 1;
                                                            respuesta.FechaHoraCreacion = DateTime.Now;
                                                            respuesta.UsuarioCreacion = idempleado.ToString();
                                                            respuesta.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(respuesta);
                                                            context.SaveChanges();
                                                        }
                                                        else
                                                        {
                                                            //Inserta pregunta likert doble
                                                            var preguntaLikert = new DL.PreguntasLikert();
                                                            preguntaLikert.idPregunta = idPregunta;
                                                            preguntaLikert.idEncuesta = lastEncuestaBD;
                                                            preguntaLikert.Pregunta = (workSheet.Cells[rowIterator, 24].Value).ToString();
                                                            preguntaLikert.IdEstatus = 1;
                                                            preguntaLikert.FechaHoraCreacion = DateTime.Now;
                                                            preguntaLikert.UsuarioCreacion = idempleado.ToString();
                                                            preguntaLikert.ProgramaCreacion = "Importa Encuesta";
                                                            context.PreguntasLikert.Add(preguntaLikert);
                                                            context.SaveChanges();
                                                            //Max Preguntas Likert Doble
                                                            int idPreguntasLikert = context.PreguntasLikert.Max(q => q.idPreguntasLikert);
                                                            //Inserta Respuesta por index 0 e index 1
                                                            var queryRespuestaColA = new DL.Respuestas();//Respuesta, IdPregunta,IdPreguntaLikertD
                                                            queryRespuestaColA.Respuesta = (workSheet.Cells[rowIterator, 6].Value).ToString();
                                                            queryRespuestaColA.IdPregunta = idPregunta;
                                                            queryRespuestaColA.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColA.IdEstatus = 1;
                                                            queryRespuestaColA.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColA.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColA.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColA);
                                                            context.SaveChanges();

                                                            var queryRespuestaColB = new DL.Respuestas();
                                                            queryRespuestaColB.Respuesta = (workSheet.Cells[rowIterator, 7].Value).ToString();
                                                            queryRespuestaColB.IdPregunta = idPregunta;
                                                            queryRespuestaColB.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColB.IdEstatus = 1;
                                                            queryRespuestaColB.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColB.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColB.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColB);
                                                            context.SaveChanges();
                                                        }
                                                        
                                                    }
                                                    if (workSheet.Cells[rowIterator, 25].Value != null)
                                                    {
                                                        if (IdTipoDeControl != 12)
                                                        {
                                                            var respuesta = new DL.Respuestas();
                                                            respuesta.IdPregunta = idPregunta;
                                                            respuesta.Respuesta = (workSheet.Cells[rowIterator, 25].Value).ToString();
                                                            respuesta.IdEstatus = 1;
                                                            respuesta.FechaHoraCreacion = DateTime.Now;
                                                            respuesta.UsuarioCreacion = idempleado.ToString();
                                                            respuesta.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(respuesta);
                                                            context.SaveChanges();
                                                        }
                                                        else
                                                        {
                                                            //Inserta pregunta likert doble
                                                            var preguntaLikert = new DL.PreguntasLikert();
                                                            preguntaLikert.idPregunta = idPregunta;
                                                            preguntaLikert.idEncuesta = lastEncuestaBD;
                                                            preguntaLikert.Pregunta = (workSheet.Cells[rowIterator, 25].Value).ToString();
                                                            preguntaLikert.IdEstatus = 1;
                                                            preguntaLikert.FechaHoraCreacion = DateTime.Now;
                                                            preguntaLikert.UsuarioCreacion = idempleado.ToString();
                                                            preguntaLikert.ProgramaCreacion = "Importa Encuesta";
                                                            context.PreguntasLikert.Add(preguntaLikert);
                                                            context.SaveChanges();
                                                            //Max Preguntas Likert Doble
                                                            int idPreguntasLikert = context.PreguntasLikert.Max(q => q.idPreguntasLikert);
                                                            //Inserta Respuesta por index 0 e index 1
                                                            var queryRespuestaColA = new DL.Respuestas();//Respuesta, IdPregunta,IdPreguntaLikertD
                                                            queryRespuestaColA.Respuesta = (workSheet.Cells[rowIterator, 6].Value).ToString();
                                                            queryRespuestaColA.IdPregunta = idPregunta;
                                                            queryRespuestaColA.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColA.IdEstatus = 1;
                                                            queryRespuestaColA.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColA.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColA.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColA);
                                                            context.SaveChanges();

                                                            var queryRespuestaColB = new DL.Respuestas();
                                                            queryRespuestaColB.Respuesta = (workSheet.Cells[rowIterator, 7].Value).ToString();
                                                            queryRespuestaColB.IdPregunta = idPregunta;
                                                            queryRespuestaColB.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColB.IdEstatus = 1;
                                                            queryRespuestaColB.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColB.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColB.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColB);
                                                            context.SaveChanges();
                                                        }
                                                        
                                                    }
                                                    if (workSheet.Cells[rowIterator, 26].Value != null)
                                                    {
                                                        if (IdTipoDeControl != 12)
                                                        {
                                                            var respuesta = new DL.Respuestas();
                                                            respuesta.IdPregunta = idPregunta;
                                                            respuesta.Respuesta = (workSheet.Cells[rowIterator, 26].Value).ToString();
                                                            respuesta.IdEstatus = 1;
                                                            respuesta.FechaHoraCreacion = DateTime.Now;
                                                            respuesta.UsuarioCreacion = idempleado.ToString();
                                                            respuesta.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(respuesta);
                                                            context.SaveChanges();
                                                        }
                                                        else
                                                        {
                                                            //Inserta pregunta likert doble
                                                            var preguntaLikert = new DL.PreguntasLikert();
                                                            preguntaLikert.idPregunta = idPregunta;
                                                            preguntaLikert.idEncuesta = lastEncuestaBD;
                                                            preguntaLikert.Pregunta = (workSheet.Cells[rowIterator, 26].Value).ToString();
                                                            preguntaLikert.IdEstatus = 1;
                                                            preguntaLikert.FechaHoraCreacion = DateTime.Now;
                                                            preguntaLikert.UsuarioCreacion = idempleado.ToString();
                                                            preguntaLikert.ProgramaCreacion = "Importa Encuesta";
                                                            context.PreguntasLikert.Add(preguntaLikert);
                                                            context.SaveChanges();
                                                            //Max Preguntas Likert Doble
                                                            int idPreguntasLikert = context.PreguntasLikert.Max(q => q.idPreguntasLikert);
                                                            //Inserta Respuesta por index 0 e index 1
                                                            var queryRespuestaColA = new DL.Respuestas();//Respuesta, IdPregunta,IdPreguntaLikertD
                                                            queryRespuestaColA.Respuesta = (workSheet.Cells[rowIterator, 6].Value).ToString();
                                                            queryRespuestaColA.IdPregunta = idPregunta;
                                                            queryRespuestaColA.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColA.IdEstatus = 1;
                                                            queryRespuestaColA.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColA.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColA.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColA);
                                                            context.SaveChanges();

                                                            var queryRespuestaColB = new DL.Respuestas();
                                                            queryRespuestaColB.Respuesta = (workSheet.Cells[rowIterator, 7].Value).ToString();
                                                            queryRespuestaColB.IdPregunta = idPregunta;
                                                            queryRespuestaColB.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColB.IdEstatus = 1;
                                                            queryRespuestaColB.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColB.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColB.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColB);
                                                            context.SaveChanges();
                                                        }
                                                        
                                                    }
                                                    if (workSheet.Cells[rowIterator, 27].Value != null)
                                                    {
                                                        if (IdTipoDeControl != 12)
                                                        {
                                                            var respuesta = new DL.Respuestas();
                                                            respuesta.IdPregunta = idPregunta;
                                                            respuesta.Respuesta = (workSheet.Cells[rowIterator, 27].Value).ToString();
                                                            respuesta.IdEstatus = 1;
                                                            respuesta.FechaHoraCreacion = DateTime.Now;
                                                            respuesta.UsuarioCreacion = idempleado.ToString();
                                                            respuesta.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(respuesta);
                                                            context.SaveChanges();
                                                        }
                                                        else
                                                        {
                                                            //Inserta pregunta likert doble
                                                            var preguntaLikert = new DL.PreguntasLikert();
                                                            preguntaLikert.idPregunta = idPregunta;
                                                            preguntaLikert.idEncuesta = lastEncuestaBD;
                                                            preguntaLikert.Pregunta = (workSheet.Cells[rowIterator, 27].Value).ToString();
                                                            preguntaLikert.IdEstatus = 1;
                                                            preguntaLikert.FechaHoraCreacion = DateTime.Now;
                                                            preguntaLikert.UsuarioCreacion = idempleado.ToString();
                                                            preguntaLikert.ProgramaCreacion = "Importa Encuesta";
                                                            context.PreguntasLikert.Add(preguntaLikert);
                                                            context.SaveChanges();
                                                            //Max Preguntas Likert Doble
                                                            int idPreguntasLikert = context.PreguntasLikert.Max(q => q.idPreguntasLikert);
                                                            //Inserta Respuesta por index 0 e index 1
                                                            var queryRespuestaColA = new DL.Respuestas();//Respuesta, IdPregunta,IdPreguntaLikertD
                                                            queryRespuestaColA.Respuesta = (workSheet.Cells[rowIterator, 6].Value).ToString();
                                                            queryRespuestaColA.IdPregunta = idPregunta;
                                                            queryRespuestaColA.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColA.IdEstatus = 1;
                                                            queryRespuestaColA.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColA.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColA.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColA);
                                                            context.SaveChanges();

                                                            var queryRespuestaColB = new DL.Respuestas();
                                                            queryRespuestaColB.Respuesta = (workSheet.Cells[rowIterator, 7].Value).ToString();
                                                            queryRespuestaColB.IdPregunta = idPregunta;
                                                            queryRespuestaColB.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColB.IdEstatus = 1;
                                                            queryRespuestaColB.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColB.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColB.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColB);
                                                            context.SaveChanges();
                                                        }
                                                        
                                                    }
                                                    if (workSheet.Cells[rowIterator, 28].Value != null)
                                                    {
                                                        if (IdTipoDeControl != 12)
                                                        {
                                                            var respuesta = new DL.Respuestas();
                                                            respuesta.IdPregunta = idPregunta;
                                                            respuesta.Respuesta = (workSheet.Cells[rowIterator, 28].Value).ToString();
                                                            respuesta.IdEstatus = 1;
                                                            respuesta.FechaHoraCreacion = DateTime.Now;
                                                            respuesta.UsuarioCreacion = idempleado.ToString();
                                                            respuesta.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(respuesta);
                                                            context.SaveChanges();
                                                        }
                                                        else
                                                        {
                                                            //Inserta pregunta likert doble
                                                            var preguntaLikert = new DL.PreguntasLikert();
                                                            preguntaLikert.idPregunta = idPregunta;
                                                            preguntaLikert.idEncuesta = lastEncuestaBD;
                                                            preguntaLikert.Pregunta = (workSheet.Cells[rowIterator, 28].Value).ToString();
                                                            preguntaLikert.IdEstatus = 1;
                                                            preguntaLikert.FechaHoraCreacion = DateTime.Now;
                                                            preguntaLikert.UsuarioCreacion = idempleado.ToString();
                                                            preguntaLikert.ProgramaCreacion = "Importa Encuesta";
                                                            context.PreguntasLikert.Add(preguntaLikert);
                                                            context.SaveChanges();
                                                            //Max Preguntas Likert Doble
                                                            int idPreguntasLikert = context.PreguntasLikert.Max(q => q.idPreguntasLikert);
                                                            //Inserta Respuesta por index 0 e index 1
                                                            var queryRespuestaColA = new DL.Respuestas();//Respuesta, IdPregunta,IdPreguntaLikertD
                                                            queryRespuestaColA.Respuesta = (workSheet.Cells[rowIterator, 6].Value).ToString();
                                                            queryRespuestaColA.IdPregunta = idPregunta;
                                                            queryRespuestaColA.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColA.IdEstatus = 1;
                                                            queryRespuestaColA.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColA.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColA.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColA);
                                                            context.SaveChanges();

                                                            var queryRespuestaColB = new DL.Respuestas();
                                                            queryRespuestaColB.Respuesta = (workSheet.Cells[rowIterator, 7].Value).ToString();
                                                            queryRespuestaColB.IdPregunta = idPregunta;
                                                            queryRespuestaColB.IdPreguntaLikertD = idPreguntasLikert;
                                                            queryRespuestaColB.IdEstatus = 1;
                                                            queryRespuestaColB.FechaHoraCreacion = DateTime.Now;
                                                            queryRespuestaColB.UsuarioCreacion = idempleado.ToString();
                                                            queryRespuestaColB.ProgramaCreacion = "Importa Encuesta";
                                                            context.Respuestas.Add(queryRespuestaColB);
                                                            context.SaveChanges();
                                                        }
                                                        
                                                    }
                                                }
                                                else
                                                {
                                                    var respuesta = new DL.Respuestas();
                                                    respuesta.IdPregunta = idPregunta;
                                                    respuesta.Respuesta = "Unica Respuesta";
                                                    respuesta.IdEstatus = 1;
                                                    respuesta.FechaHoraCreacion = DateTime.Now;
                                                    respuesta.UsuarioCreacion = idempleado.ToString();
                                                    respuesta.ProgramaCreacion = "Importa Encuesta";
                                                    context.Respuestas.Add(respuesta);
                                                    context.SaveChanges();
                                                    //Unica respuesta

                                                }
                                                casoSubSeccion:
                                                if (IdTipoDeControl == 13)
                                                {
                                                   
                                                    var preguntasSS = new DL.Preguntas();                                                    
                                                    preguntasSS.idEncuesta = lastEncuestaBD;
                                                    preguntasSS.Seccion = Convert.ToInt32((workSheet.Cells[rowIterator, 1].Value));
                                                    preguntasSS.EncabezadoSeccion = (workSheet.Cells[rowIterator, 2].Value).ToString() == null ? "" : (workSheet.Cells[rowIterator, 2].Value).ToString();
                                                    preguntasSS.Pregunta = (workSheet.Cells[rowIterator, 3].Value).ToString();
                                                    preguntasSS.IdTipoControl = IdTipoDeControl;
                                                    preguntasSS.Obligatoria = obligatoria;
                                                    preguntasSS.IdEstatus = 1;
                                                    preguntasSS.Valoracion = 0;
                                                    preguntasSS.FechaHoraCreacion = DateTime.Now;
                                                    preguntasSS.UsuarioCreacion = idempleado.ToString();
                                                    preguntasSS.ProgramaCreacion = "Importa Encuesta";
                                                    preguntasSS.Enfoque = "";
                                                    preguntasSS.RespuestaCondicion = "";
                                                    preguntasSS.PreguntasCondicion = "";
                                                    context.Preguntas.Add(preguntasSS);
                                                    context.SaveChanges();
                                                    int IdPregunta = context.Preguntas.Max(m => m.IdPregunta);
                                                    if (preguntasSS.IdTipoControl == 13)
                                                    {
                                                        IdPreguntaSubSeccion = IdPregunta;
                                                    }
                                                    var update_ = context.Database.ExecuteSqlCommand("UPDATE PREGUNTAS SET SUBSECCION = {0} WHERE IDPREGUNTA = {1}", IdPreguntaSubSeccion, IdPregunta);
                                                }

                                            }

                                            catch (Exception aE)
                                            {
                                                transaction.Rollback();
                                                Session["LogError"] = 1;
                                                return Redirect(Request.UrlReferrer.ToString());
                                                //result.Correct = false;
                                                //result.ErrorMessage = aE.Message;
                                            }
                                            int lastPregunta = context.Preguntas.Max(m=>m.IdPregunta);
                                            var actualiza = context.Database.ExecuteSqlCommand("UPDATE PREGUNTAS SET SUBSECCION = {0} WHERE IDPREGUNTA ={1}", IdPreguntaSubSeccion, lastPregunta);
                                            transaction.Commit();
                                            //return Json("success");
                                        }
                                    }
                                }
                                return Json("success");
                            }
                            else
                            {
                                Session["LogError"] = 6;
                                return Redirect(Request.UrlReferrer.ToString());
                            }
                        }
                    }
                    else
                    {
                        //Formato no valido
                        Session["LogError"] = 1;
                        return Redirect(Request.UrlReferrer.ToString());
                    }
                }
                else
                {
                    //No eligio archivo
                    Session["LogError"] = 5;
                    return Redirect(Request.UrlReferrer.ToString());
                }
            }
            /**Inserta Encuesta**/
            return null;

        }
        public ActionResult SendEmail(ML.Usuario user, string NombreEncuesta, DateTime FechaInicio, DateTime FechaFin, string Redirect)
        {
            //var result = BL.Email.SendEmail(nombreNewAdmin, perfil, UserName, Password);
            ML.Result result = new ML.Result();
            string FullName = user.Nombre + " " + user.ApellidoPaterno + " " + user.ApellidoMaterno;

            var body =
            "<p style='font-weight:bold;'>Que tal " + FullName + "</p>" +
            "<p>Has sido dado de alta dentro del portal de encuestas <b>Diagnostic4U</b></p>" +
            "<p>Has sido elegido para contestar la encuesta: " + NombreEncuesta + " la cual estará activa desde el " + FechaInicio + " hasta el " + FechaFin + "</p>" +

            "<p>Tu clave de acceso es la siguiente: <b>" + user.ClaveAcceso + "</b></p>" +
            "<p>Accede entrando a: <a href='" + Redirect + "'><b>Diagnostic4U</b></a></p>" +
            "<p><img src=http://www.diagnostic4u.com/img/logo.png'></p></ br>";
            //"<small>Si ya cuenta con un perfil anterior a este tenga en cuenta que las claves de acceso son las mismas.</small>";
            var message = new MailMessage();
            message.To.Add(new MailAddress(user.Email));
            //message.From = new MailAddress("jamurillo@grupoautofin.com");
            message.Subject = "Notificación Diagnostic4U";
            message.Body = string.Format(body, "DIAGNOSTIC4U", "jamurillo@grupoautofin.com", "Aqui se envian  las claves de acceso al portal");
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {

                //smtp.Credentials = CredentialCache.DefaultNetworkCredentials;

                try
                {
                    smtp.Send(message);
                    result.Correct = true;
                }
                catch (Exception ex)
                {
                    result.ErrorMessage = ex.Message;
                    result.Correct = false;
                    Session["errorEmail"] = 1;
                }
                finally
                {
                    smtp.Dispose();
                }

                //Session["error"] = result.ErrorMessage + " " + result.DefPass;

                return Json("success");

            }
        }
        [HttpGet]
        [HandleError(View = "Login/?e=999999888")]
        public ActionResult Login(string e)
        {


            var browser = this.Request.Browser;
            var navegador = browser.Browser;
            if (navegador == "InternetExplorer")
            {
                if (e != Convert.ToString(999988888))
                {
                    return RedirectToAction("Login", "Encuesta", new { e = 999988888 });
                }
            }
            
            try
            {
                if (String.IsNullOrEmpty(e.ToString()))
                {
                    return RedirectToAction("Login", "Encuesta", new { e = 999999888 });
                }
                ML.Usuario user = new ML.Usuario();
                user.IdEncuesta = Convert.ToInt32(e);
                return View(user);
            }
            catch (Exception aE)
            {
                return RedirectToAction("Login", "Encuesta", new { e = 999999888 });
            }
        }
        [HttpPost]
        public ActionResult Login(ML.Usuario user)
        {
            if (string.IsNullOrEmpty(user.ClaveAcceso))
            {
                user.ClaveAcceso = String.Empty;
            }
            user.ClaveAcceso = user.ClaveAcceso.Trim();
            //Validamos fecha Fin de la encuesta 
            var validaFechaTerminoEncuesta = BL.Encuesta.ValidaFechaEncuesta(user.IdEncuesta);
            if (!validaFechaTerminoEncuesta.Exist)
            {
                return RedirectToAction("Login", "Encuesta", new { e = 999999991 });
            }


            var result = BL.Encuesta.Autenticar(user);
            int idEncuestaSol = 0;
            int idEncuestaContestada = 999999999;
            if (user.IdEncuesta != 0) { idEncuestaSol = user.IdEncuesta; } else { idEncuestaSol = 0; }
            if (result.IsContestada == true)
            {
                return RedirectToAction("Login", "Encuesta", new { e = idEncuestaContestada });
            }

            result.tableBody = "";
            try
            {
                var IdUsuario = ((ML.Usuario)result.Object).IdUsuario;

                Session["IdUsuarioLog"] = IdUsuario;
                var url = ((ML.Encuesta)result.ObjectAux).UID;
                //Update Estatus => 2 Iniciada
                int IdEncuesta = ((ML.Encuesta)result.ObjectAux).IdEncuesta;

                //Dispositivo
                var browser = this.Request.Browser;
                var ip = this.Request.UserHostAddress;
                BL.Usuario.saveDevice(IdEncuesta, IdUsuario, browser.IsMobileDevice, browser.Version, browser.Browser, ip);
                var updatestatusEncuesta = BL.Usuario.updateEstatusEncuesta(IdEncuesta, IdUsuario, 2);

                //TempData["Agradecimiento"] 

                return RedirectToAction("e", new { u = url });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "Encuesta", new { e = 999999888 });
            }


        }
        public ActionResult ViewReporte(int IdEncuesta)
        {//Nuevo cambio
            //var fullUrl = this.Url.Action("ViewReporte", "Encuesta", new { id = IdEncuesta }, this.Request.Url.Scheme);

            //var getTipoEncuesta = BL.Encuesta.GetTipoEncuesta(IdEncuesta);
            //int TipoEncuesta = Convert.ToInt32(getTipoEncuesta.Object);

            //var ds = BL.Encuesta.GetRespuestasDinamycReport(IdEncuesta, fullUrl, TipoEncuesta);
            //var gettipoEncuesta = BL.Encuesta.GetTipoEncuesta(IdEncuesta);
            //ML.Result result = new ML.Result();
            //result.idTipoEncuesta = Convert.ToInt32(gettipoEncuesta.Object);
            //result.DataSet = ds;

            //return View(result);
            return View();
        }
        public ActionResult GetData(int IdEncuesta)
        {//Nuevo cambio
            var fullUrl = this.Url.Action("ViewReporte", "Encuesta", new { id = IdEncuesta }, this.Request.Url.Scheme);

            var getTipoEncuesta = BL.Encuesta.GetTipoEncuesta(IdEncuesta);
            int TipoEncuesta = Convert.ToInt32(getTipoEncuesta.Object);

            var ds = BL.Encuesta.GetRespuestasDinamycReport(IdEncuesta, fullUrl, TipoEncuesta);
            var gettipoEncuesta = BL.Encuesta.GetTipoEncuesta(IdEncuesta);
            ML.Result result = new ML.Result();
            result.idTipoEncuesta = Convert.ToInt32(gettipoEncuesta.Object);
            result.DataSet = ds;
            //var dataJSON = DataTableToJSONWithStringBuilder(result.DataSet.Tables[0]);
            var resultt = new ML.Result();
            resultt.Objects = new List<object>();
            //result.Objects.Add(JsonConvert.SerializeObject(result.DataSet.Tables[0].AsEnumerable().Select(r => r.ItemArray)));
            
            foreach (DataRow item in result.DataSet.Tables[0].Rows)
            {
                resultt.Objects.Add(item.ItemArray);
            }
            var json = Json(resultt.Objects);
            json.MaxJsonLength = int.MaxValue;
            //return Json(result, JsonRequestBehavior.AllowGet);
            //return new JsonResult { Data = json, MaxJsonLength = Int32.MaxValue };
            return new JsonResult { Data = json, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };

           // return Json(json, JsonRequestBehavior.AllowGet);
        }
        public string DataTableToJSONWithStringBuilder(DataTable table)
        {
            var JSONString = new StringBuilder();
            if (table.Rows.Count > 0)
            {
                JSONString.Append("[");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    JSONString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == table.Rows.Count - 1)
                    {
                        JSONString.Append("}");
                    }
                    else
                    {
                        JSONString.Append("},");
                    }
                }
                JSONString.Append("]");
            }
            char old = '\\';
            JSONString = JSONString.Replace('\\', ' ');
            return JSONString.ToString();
        }
        [HttpGet]
        public ActionResult ConfigurarCondiciones()
        {
            int IdEncuesta = Convert.ToInt32(Session["IdEncuestaAlta"]);
            string idsessionAdmin = Convert.ToString(Session["IdAdministradorLogeado"]);
            //IdEncuesta = 68;

            var result = BL.Encuesta.getEncuestaByIdConfigura(IdEncuesta,idsessionAdmin);

            return View(result.EditaEncuesta);
        }
        [HttpPost]
        public ActionResult ConfigurarCondiciones(ML.ConfiguraRespuesta configura)
        {
            string CURRENT_USER = Convert.ToString(Session["AdminLog"]);
            var result = BL.ConfiguraRespuesta.Add(configura, CURRENT_USER);
            //Session["SeccionarEncuesta"]
            bool configuraSecciones = Convert.ToBoolean(Session["SeccionarEncuesta"]);
            if (result.Correct == true)
            {
                return Json("success");
            }
            else
            {
                return Json("error");
            }
        }
        public ActionResult ObtenPreguntasDesacadenado(ML.Preguntas param)
        {


            var result = BL.Preguntas.getPreguntasByIdEncuestaConfigura(param.IdEncuesta, param.IdPregunta);

            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ObtenPreguntasDesacadenadoForEdit(ML.Preguntas param)
        {


            var result = BL.Preguntas.getPreguntasByIdEncuestaConfiguraForEdit(param.IdEncuesta, param.IdPregunta);

            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getConfByRespuesta(ML.ConfiguraRespuesta param)
        {
            var result = BL.Preguntas.getConfByRespuesta(param.IdEncuesta, param.IdRespuesta);
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getConfByEncuesta(ML.ConfiguraRespuesta param)
        {
            var result = BL.Preguntas.getConfByIdEncuesta(param.IdEncuesta);
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getConfiguraByIdEncuesta(int idEncuesta)
        {
            var result = BL.ConfiguraRespuesta.getAllByIdEncuesta(idEncuesta);
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPreguntas(ML.Encuesta encuesta)
        {
            var result = BL.Preguntas.GetPreguntasBydIdEncuesta(encuesta.IdEncuesta);

            var json = Json(result.Objects, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return Json(json, JsonRequestBehavior.AllowGet);
            //return new JsonResult { Data = json, MaxJsonLength = Int32.MaxValue };
        }
        public ActionResult GetRespuestas(ML.Encuesta encuesta)
        {
            var url = this.Url.Action("Edit", "Posts", new { id = 5 }, this.Request.Url.Scheme);
            var getTipoEncuesta = BL.Encuesta.GetTipoEncuesta(encuesta.IdEncuesta);
            int tipoEncuesta = Convert.ToInt32(getTipoEncuesta.Object);
            var ds = BL.Encuesta.GetRespuestasDinamycReport(encuesta.IdEncuesta, url, tipoEncuesta);
            ML.Result result = new ML.Result();
            result.DataSet = ds;

            return Json(ds, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult EncuestaIngreso()
        {
            //var result = BL.Encuesta.GetReporteEncuestaIngreso();
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            return View(result);
        }
        [HttpPost]
        public ActionResult EncuestaIngreso(ML.EncuestaIngreso enc)
        {
            var result = new ML.Result();
            if (enc.Filtro == 0)//All
            {
                result = BL.Encuesta.GetReporteEncuestaIngresoByFilter(enc);
            }
            else
            {
                result = BL.Encuesta.GetReporteEncuestaIngresoByFilter(enc);
            }

            var json = Json(result.Objects, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return new JsonResult { Data = json, MaxJsonLength = Int32.MaxValue };
        }
        [HttpGet]
        public ActionResult GetEmpresas(string num)
        {
            var result = BL.Empresa.GetEmpresasIngreso();

            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
        //Secciones
        [HttpGet]
        public ActionResult ConfiguraSecciones()
        {
            int IdEncuesta = Convert.ToInt32(Session["IdEncuestaAlta"]);
            string idsessionAdmin = Convert.ToString(Session["IdAdministradorLogeado"]);
            var result = BL.Encuesta.getEncuestaByIdConfigura(IdEncuesta,idsessionAdmin);

            return View(result.EditaEncuesta);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ConfiguraSecciones(List<int> listaIdPregunta, List<int> listaSecciones, List<string> listaEncabezados)
        {
            //listaIdPregunta
            //listaSecciones
            var result = BL.Encuesta.ConfiguraSecciones(listaIdPregunta, listaSecciones, listaEncabezados);

            if (result.Correct == true)
            {
                return Json("success");
            }
            else
            {
                return Json("error");
            }
        }
        [HttpGet]
        public ActionResult EditSecciones()
        {
            int IdEncuesta = Convert.ToInt32(Session["IdEncuestaUpdateSecciones"]);
            string idsessionAdmin = Convert.ToString(Session["IdAdministradorLogeado"]);
            //Merge en la vista la configuracion de secciones
            var result = BL.Encuesta.getEncuestaByIdConfigura(IdEncuesta,idsessionAdmin);

            return View(result.EditaEncuesta);
        }
        [HttpGet]
        public ActionResult GetSeccionTitle()
        {
            int IdEncuesta = Convert.ToInt32(Session["IdEncuestaUpdateSecciones"]);
            var result = BL.Encuesta.GetSeccionTitle(IdEncuesta);
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
        //Edit Condiciones
        [HttpGet]
        public ActionResult EditCondiciones()
        {
            int IdEncuesta = Convert.ToInt32(Session["IdEncuestaUpdateBifurcacion"]);
            string idsessionAdmin = Convert.ToString(Session["IdAdministradorLogeado"]);
            var result = BL.Encuesta.getEncuestaByIdConfiguraForEdit(IdEncuesta,idsessionAdmin);

            return View(result.EditaEncuesta);
        }
        [HttpPost]
        public ActionResult EditCondiciones(ML.ConfiguraRespuesta configura)
        {
            string CURRENT_USER = Convert.ToString(Session["AdminLog"]);
            var result = BL.ConfiguraRespuesta.Update(configura, CURRENT_USER);
            //Session["SeccionarEncuesta"]
            if (result.Correct == true)
            {
                return Json("success");
            }
            else
            {
                return Json("error");
            }
        }

        //ENviar CLima
        public ActionResult EnviarEncuestaCL()
        {
            //Si ya hay una configuracion cargarla
            Session["errorEmail"] = 0;
            return View();
        }
        public ActionResult GetBDCL()
        {
            var result = BL.BasesDeDatos.GetBDClima();
            return Json(result.Objetos, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveConfifClimaLab(ML.ConfigClimaLab conf)
        {
            string CURRENT_USER = Convert.ToString(Session["AdminLog"]);
            var result = BL.Encuesta.SaveConfigClima(conf, CURRENT_USER);

            Session["IdBaseDDForEnviar"] = conf.BaseDeDatos.IdBaseDeDatos;
            Session["FechaInicioCL"] = conf.FechaInicio;
            Session["FechaFinCL"] = conf.FechaFin;

            if (result.Correct == true)
            {
                return Json("success");
            }
            else
            {
                return Json("error");
            }
        }


        //EditClimaLaboral
        public ActionResult EditClimaLab()
        {
            if (ViewBag.Message == null || ViewBag.Message == "")
            {
                ViewBag.Message = "";
            }
            ML.Result encuesta = BL.Encuesta.getClimaLaboralByIdEdit(1);
            return View(encuesta.EditaEncuesta);
        }
        public ActionResult GetAllPeriodosConfig()
        {
            var result = BL.Encuesta.GetAllPeriodosConfig();
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdatePeriodosClimaLab(ML.ConfigClimaLab conf)
        {
            var result = BL.Encuesta.UpdatePeriodosClimaLab(conf);

            if (result.Correct == true)
            {
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetFechasByBD(ML.ConfigClimaLab conf)
        {
            var result = BL.Encuesta.GetFechasByBD(conf);

            Session["IdBaseDDForEnviar"] = conf.IdDatabase;
            //DateTime FechaInicio = Convert.ToDateTime(Session["FechaInicioCL"]);
            //DateTime FechaFin = Convert.ToDateTime(Session["FechaFinCL"]);
            foreach (ML.ConfigClimaLab item in result.Objects)
            {
                Session["FechaInicioCL"] = Convert.ToDateTime(item.InicioEncuesta);
                Session["FechaFinCL"] = Convert.ToDateTime(item.FinEncuesta);
            }
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetEmpleadoByIdDatabase(ML.ConfigClimaLab conf)
        {
            var result = BL.Empleado.GetEmpleadoByIdDatabase(conf);

            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateCL(ML.Encuesta encuesta)
        {
            string currentUser = Convert.ToString(Session["AdminLog"]);
            var result = BL.Encuesta.UpdateEncuestaCL(encuesta, currentUser);

            if (result.Correct == true)
            {
                return RedirectToAction("GetAll");
            }
            else
            {
                ViewBag.Message = "Error";
                return Redirect(Request.UrlReferrer.ToString());
            }
        }


        //Envíos asincronos Clima Laboral
        [HttpPost]
        public async Task<ActionResult> EnviarEncuesta(ML.Encuesta encuesta)
        {
            HttpRuntime.Cache.Remove("emailMessage");
            HttpRuntime.Cache.Add("emailMessage", encuesta.Mensaje, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(8),
                            System.Web.Caching.CacheItemPriority.High, null);
            encuesta.Mensaje = "";
            if (encuesta.IdEncuesta == 1)
            {
                return RedirectToAction("EnviarEncuestaCL");
            }
            else
            {
                return RedirectToAction("SendEncuestaGral", "Encuesta", encuesta);
            }
        }

        public async Task<ActionResult> EnviarEncuestaClima(int IdEncuesta)
        {
            if (IdEncuesta == 1)
            {
                return RedirectToAction("EnviarEncuestaCL");
            }
            return RedirectToAction("EnviarEncuestaCL");
        }

        public ActionResult Send(ML.ConfigClimaLab conf)
        {
            HttpRuntime.Cache.Remove("emailMessageCL");
            HttpRuntime.Cache.Add("emailMessageCL", conf.Mensaje, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(8),
                            System.Web.Caching.CacheItemPriority.High, null);
            conf.Mensaje = "";

            SynchronizationContext originalContext = SynchronizationContext.Current;
            //var context = contextoPadre;
            Session["NombreBDSendEmail"] = conf.nameBD;
            Session["UsuarioEnvia"] = Session["AdminLog"];
            Task<ActionResult> tarea1 = new Task<ActionResult>(n => SendEmailToClimaLaboral(conf), 10000);
            tarea1.Start();
            Task continuacion = tarea1.ContinueWith(next => check(tarea1));
            return Json("success");
        }
        public int check(Task<ActionResult> tarea)
        {
            var status = "";
            int num = 0;
            var ErrorMessage = HttpRuntime.Cache.Get("logError");
            Console.WriteLine("Ejecutando");
            status = tarea.Status.ToString();
            if (status == "Running")
            {
                Console.WriteLine("Ejecutando");
            }
            else if (status == "RanToCompletion" && ErrorMessage != null)
            {
                num = 1;
                //Set Session = 2 (Error)
                HttpRuntime.Cache.Add("status", 2, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(8),
                System.Web.Caching.CacheItemPriority.High, null);
            }
            else if (status == "RanToCompletion")
            {
                num = 1;
                //Set Session = 1 (Success)
                HttpRuntime.Cache.Add("status", 1, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(8),
                System.Web.Caching.CacheItemPriority.High, null);
                return num;
            }
            return num;
        }
        public ActionResult SendEmailToClimaLaboral(ML.ConfigClimaLab conf)
        {
            ML.Result result = new ML.Result();
            try
            {
                int IdBD = conf.IdDatabase;
                DateTime FechaInicio = conf.FechaInicio;
                DateTime FechaFin = conf.FechaFin;
                result.Objects = new List<object>();
                List<Task> lista = new List<Task>();
                string FullName = "";
                int IdBaseDeDatosForSend = IdBD;
                try
                {
                    using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                    {
                        var query = context.Empleado.SqlQuery("SELECT * FROM EMPLEADO WHERE IDBASEDEDATOS = {0} AND EstatusEmpleado = 'ACTIVO'", IdBaseDeDatosForSend).ToList();
                        if (query != null)
                        {
                            foreach (var item in query)
                            {
                                ML.Empleado empleado = new ML.Empleado();
                                empleado.Nombre = item.Nombre;
                                empleado.ApellidoPaterno = item.ApellidoPaterno;
                                empleado.ApellidoMaterno = item.ApellidoMaterno;
                                empleado.Correo = item.Correo;
                                empleado.ClavesAcceso = new ML.ClavesAcceso();
                                empleado.ClavesAcceso.ClaveAcceso = item.ClaveAcceso;

                                result.Objects.Add(empleado);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.Correct = false;
                    result.ErrorMessage = ex.Message;
                    Debug.WriteLine("Sending email #");
                    HttpRuntime.Cache.Add("logError", ex.Message, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(8),
                    System.Web.Caching.CacheItemPriority.High, null);
                }
                int enviadosCorrectos = 0;
                int TotalEmails = result.Objects.Count;
                foreach (ML.Empleado item in result.Objects)
                {
                    if (conf.TipoMensaje == "default" && item.Correo != null)
                    {
                        FullName = item.Nombre + " " + item.ApellidoPaterno + " " + item.ApellidoMaterno;
                        var body =
                        "<p style='font-weight:bold;'>Que tal " + FullName + "</p>" +
                        "<p>Has sido dado de alta dentro del portal de encuestas <b>Diagnostic4U</b></p>" +
                        "<p>Has sido elegido para contestar la encuesta: " + "Clima Laboral" + " la cual estará activa desde el " + FechaInicio + " hasta el " + FechaFin + "</p>" +
                        "<p>Tu clave de acceso es la siguiente: <b>" + item.ClavesAcceso.ClaveAcceso + "</b></p>" +
                        "<p>Accede entrando a: <a href='http://www.diagnostic4u.com/'><b>Diagnostic4U</b></a></p>" +
                        "<p><img src=http://www.diagnostic4u.com/img/logo.png'></p></ br>";
                        var message = new MailMessage();
                        message.To.Add(new MailAddress(item.Correo));
                        message.Subject = "Notificación Diagnostic4U";
                        message.Body = string.Format(body, "DIAGNOSTIC4U", "jamurillo@grupoautofin.com", "Aqui se envian  las claves de acceso al portal");
                        message.IsBodyHtml = true;

                        using (var smtp = new SmtpClient())
                        {
                            try
                            {

                                smtp.Send(message);
                                result.Correct = true;
                                enviadosCorrectos = enviadosCorrectos + 1;
                            }
                            catch (Exception ex)
                            {
                                result.ErrorMessage = ex.Message;
                                result.Correct = false;
                                Debug.WriteLine("Sending email #");
                                string mensajeError = ex.Message + "\n";
                                mensajeError += " " + item.Correo;
                                HttpRuntime.Cache.Add("logError", ex.Message, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(8),
                                System.Web.Caching.CacheItemPriority.High, null);
                            }
                            finally
                            {
                                smtp.Dispose();
                            }
                        }
                    }
                    else if(conf.TipoMensaje != "default" && item.Correo != null)
                    {
                        FullName = item.Nombre + " " + item.ApellidoPaterno + " " + item.ApellidoMaterno;

                        string mensajeBase64 = Convert.ToString(HttpRuntime.Cache.Get("emailMessageCL"));

                        string mensaje = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(mensajeBase64));

                        Console.WriteLine(mensaje);


                        mensaje = ReplaceDatosMensaje(mensaje, "*NombreUsuario*", FullName);
                        mensaje = ReplaceDatosMensaje(mensaje, "*FechaInicio*", Convert.ToString(FechaInicio));
                        mensaje = ReplaceDatosMensaje(mensaje, "*FechaFin*", Convert.ToString(FechaFin));
                        mensaje = ReplaceDatosMensaje(mensaje, "*NombreEncuesta*", "Clima Laboral" + DateTime.Today.Year);
                        mensaje = ReplaceDatosMensaje(mensaje, "*LinkEncuesta*", "<a style='font-weight:bold' href='http://www.diagnostic4u.com/'>Diagnostic4U</a>");
                        mensaje = ReplaceDatosMensaje(mensaje, "*ClaveAcceso*", item.ClavesAcceso.ClaveAcceso);

                        Console.WriteLine(mensaje);


                        var body = mensaje;


                        var message = new MailMessage();
                        message.To.Add(new MailAddress(item.Correo));
                        message.Subject = "Notificación Diagnostic4U";
                        message.Body = string.Format(body, "DIAGNOSTIC4U", "jamurillo@grupoautofin.com", "Aqui se envian  las claves de acceso al portal");
                        message.IsBodyHtml = true;

                        using (var smtp = new SmtpClient())
                        {
                            try
                            {

                                smtp.Send(message);
                                result.Correct = true;
                                enviadosCorrectos = enviadosCorrectos + 1;
                            }
                            catch (Exception ex)
                            {
                                result.ErrorMessage = ex.Message;
                                result.Correct = false;
                                Debug.WriteLine("Sending email #");
                                string mensajeError = ex.Message + "\n";
                                mensajeError += " " + item.Correo;
                                HttpRuntime.Cache.Add("logError", ex.Message, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(8),
                                System.Web.Caching.CacheItemPriority.High, null);
                            }
                            finally
                            {
                                smtp.Dispose();
                            }
                        }
                    }

                }
                HttpRuntime.Cache.Add("EnviadosCorrectos", enviadosCorrectos, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(8),
                System.Web.Caching.CacheItemPriority.High, null);

                HttpRuntime.Cache.Add("TotalAEnviar", TotalEmails, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(8),
                System.Web.Caching.CacheItemPriority.High, null);
            }
            catch (Exception ex)
            {
                //ViewBag.ErrorMessage = result.ErrorMessage;
                HttpRuntime.Cache.Add("logError", ex.Message, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(8),
                System.Web.Caching.CacheItemPriority.High, null);
                return RedirectToAction("GetAll");
            }
            return RedirectToAction("GetAll");
        }
        public ActionResult successEmail()
        {
            Session["UsuarioEnvia"] = "";
            HttpRuntime.Cache.Remove("logError");
            Session["logError"] = "";
            HttpRuntime.Cache.Remove("status");


            return View();
        }
        public ActionResult ErrorEmail()
        {
            Session["UsuarioEnvia"] = "";
            HttpRuntime.Cache.Remove("logError");
            Session["logError"] = "";
            HttpRuntime.Cache.Remove("status");

            return View();
        }



        public ActionResult SendEncuestaGral(ML.Encuesta encuesta)
        {
            SynchronizationContext originalContext = SynchronizationContext.Current;
            //var context = contextoPadre;
            //getNameBD for this Idencuesta
            Session["UsuarioEnvia"] = Session["AdminLog"];
            Task<ActionResult> tarea1 = new Task<ActionResult>(n => EnviarEncuestaGeneral(encuesta), 10000);
            tarea1.Start();
            Task continuacion = tarea1.ContinueWith(next => check(tarea1));
            return RedirectToAction("GetAll");
        }
        public ActionResult EnviarEncuestaGeneral(ML.Encuesta encuesta)
        {
            ML.Result result = new ML.Result();
            int IdEncuestInt = Convert.ToInt32(encuesta.IdEncuesta);
            var Data = BL.Encuesta.GetData(IdEncuestInt);
            string Redirect = "";

            ML.Encuesta dataEncuesta = new ML.Encuesta();
            dataEncuesta.MLTipoEncuesta = new ML.TipoEncuesta();
            dataEncuesta.BasesDeDatos = new ML.BasesDeDatos();

            try
            {
                dataEncuesta.Nombre = ((ML.Encuesta)Data.Object).Nombre;
                dataEncuesta.FechaInicio = ((ML.Encuesta)Data.Object).FechaInicio;
                dataEncuesta.FechaFin = ((ML.Encuesta)Data.Object).FechaFin;
                dataEncuesta.UID = ((ML.Encuesta)Data.Object).UID;
                dataEncuesta.BasesDeDatos.IdBaseDeDatos = ((ML.Encuesta)Data.Object).BasesDeDatos.IdBaseDeDatos;
                dataEncuesta.BasesDeDatos.Nombre = ((ML.Encuesta)Data.Object).BasesDeDatos.Nombre;
                dataEncuesta.MLTipoEncuesta.IdTipoEncuesta = ((ML.Encuesta)Data.Object).MLTipoEncuesta.IdTipoEncuesta;
            }
            catch (Exception ex)
            {
                HttpRuntime.Cache.Add("logError", ex.Message, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(8),
                System.Web.Caching.CacheItemPriority.High, null);
            }



            HttpRuntime.Cache.Add("NombreBDSendEmail", dataEncuesta.BasesDeDatos.Nombre, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(8),
            System.Web.Caching.CacheItemPriority.High, null);

            //Agregar reporte de respuestas
            //var AddReporte = BL.Reporte.Add(IdEncuestInt, dataEncuesta.Nombre);

            if (dataEncuesta.MLTipoEncuesta.IdTipoEncuesta == 1)
            {
                //EncuestaDirecta  Encuesta/e?=
                ///Encuesta/e/?u=
                Redirect = "http://www.diagnostic4u.com/Encuesta/e/?u=" + dataEncuesta.UID;
            }
            if (dataEncuesta.MLTipoEncuesta.IdTipoEncuesta == 2)
            {
                //Login
                Redirect = "http://www.diagnostic4u.com/Encuesta/Login/?e=" + IdEncuestInt;
            }
            if (dataEncuesta.MLTipoEncuesta.IdTipoEncuesta == 3)
            {
                //Login
                Redirect = "http://www.diagnostic4u.com/Encuesta/Login/?e=" + IdEncuestInt;
            }
            string FullName = "";
            int enviadosCorrectos = 0;
            int TotalEmails = Data.Objects.Count;
            List<string> emailFallido = new List<string>();
            List<int> IdEstatusEmailList = new List<int>();
            //Insert into EstatusEmail para guardar todos en un inicio dejando el espacio de nombre sustituyendo en SendEmail from CRON
            foreach (ML.Usuario item in Data.Objects)
            {
                if (item.Email == null)
                {
                    TotalEmails = TotalEmails - 1;
                }
                if (encuesta.TipoMensaje == "default" &&  item.Email != null)
                {
                    FullName = item.Nombre + " " + item.ApellidoPaterno + " " + item.ApellidoMaterno;
                    string bodySinNombreusuario =
                        "<p style='font-weight:bold;'>Que tal " + FullName + "</p>" +
                    "<p>Has sido dado de alta dentro del portal de encuestas <b>Diagnostic4U</b></p>" +
                    "<p>Has sido elegido para contestar la encuesta: " + dataEncuesta.Nombre + " la cual estará activa desde el " + dataEncuesta.FechaInicio + " hasta el " + dataEncuesta.FechaFin + "</p>" +
                    "<p>Tu clave de acceso es la siguiente: <b>" + item.ClaveAcceso + "</b></p>" +
                    "<p>Accede entrando a: <a href='" + Redirect + "'><b>Diagnostic4U</b></a></p>" +
                    "<p><img src=http://www.diagnostic4u.com/img/logo.png'></p></ br>";
                    ML.EstatusEmail estatusEmail = new ML.EstatusEmail();
                    estatusEmail.Mensaje = bodySinNombreusuario;
                    estatusEmail.Destinatario = item.Email;
                    estatusEmail.BaseDeDatos = new ML.BasesDeDatos();
                    estatusEmail.BaseDeDatos.IdBaseDeDatos = dataEncuesta.BasesDeDatos.IdBaseDeDatos;
                    estatusEmail.Encuesta = new ML.Encuesta();
                    estatusEmail.Encuesta.IdEncuesta = encuesta.IdEncuesta;

                    var IdInsert = BL.Encuesta.AddToEstatusEmail(estatusEmail);
                    IdEstatusEmailList.Add(IdInsert);

                }
                else if(encuesta.TipoMensaje != "default" && item.Email != null)
                {
                    FullName = item.Nombre + " " + item.ApellidoPaterno + " " + item.ApellidoMaterno;
                    string mensajeBase64 = Convert.ToString(HttpRuntime.Cache.Get("emailMessage"));

                    string mensaje = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(mensajeBase64));
                    mensaje = ReplaceDatosMensaje(mensaje, "*NombreUsuario*", FullName);
                    mensaje = ReplaceDatosMensaje(mensaje, "*NombreEncuesta*", dataEncuesta.Nombre);
                    mensaje = ReplaceDatosMensaje(mensaje, "*FechaInicio*", Convert.ToString(dataEncuesta.FechaInicio));
                    mensaje = ReplaceDatosMensaje(mensaje, "*FechaFin*", Convert.ToString(dataEncuesta.FechaFin));
                    mensaje = ReplaceDatosMensaje(mensaje, "*LinkEncuesta*", "<a style='font-weight:bold' href='" + Redirect + "'>Diagnotic4U</a>");
                    mensaje = ReplaceDatosMensaje(mensaje, "*ClaveAcceso*", item.ClaveAcceso);

                    ML.EstatusEmail estatusEmail = new ML.EstatusEmail();
                    estatusEmail.Mensaje = mensaje;
                    estatusEmail.Destinatario = item.Email;
                    estatusEmail.BaseDeDatos = new ML.BasesDeDatos();
                    estatusEmail.BaseDeDatos.IdBaseDeDatos = dataEncuesta.BasesDeDatos.IdBaseDeDatos;
                    estatusEmail.Encuesta = new ML.Encuesta();
                    estatusEmail.Encuesta.IdEncuesta = encuesta.IdEncuesta;

                    var IdInsert = BL.Encuesta.AddToEstatusEmail(estatusEmail);
                    IdEstatusEmailList.Add(IdInsert);
                }
            }

            //Enter SMTP
            var index = 0;
            int iteracion = 0;
            foreach (ML.Usuario user in Data.Objects)
            {
                iteracion++;
                if (user.Email == null)
                {
                    index = index - 1;
                    var msg = "";
                    msg += "El usuario: " + user.Nombre + " " + user.ApellidoPaterno + " " + user.ApellidoMaterno + " no cuenta con un email registrado" + Environment.NewLine;
                    if (iteracion == Data.Objects.Count)
                    {
                        HttpRuntime.Cache.Add("logError", msg, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(8),
                            System.Web.Caching.CacheItemPriority.High, null);
                    }
                }
                if (encuesta.TipoMensaje == "default" && user.Email != null)
                {
                    FullName = user.Nombre + " " + user.ApellidoPaterno + " " + user.ApellidoMaterno;
                    var body =
                    "<p style='font-weight:bold;'>Que tal " + FullName + "</p>" +
                    "<p>Has sido dado de alta dentro del portal de encuestas <b>Diagnostic4U</b></p>" +
                    "<p>Has sido elegido para contestar la encuesta: " + dataEncuesta.Nombre + " la cual estará activa desde el " + dataEncuesta.FechaInicio + " hasta el " + dataEncuesta.FechaFin + "</p>" +
                    "<p>Tu clave de acceso es la siguiente: <b>" + user.ClaveAcceso + "</b></p>" +
                    "<p>Accede entrando a: <a href='" + Redirect + "'><b>Diagnostic4U</b></a></p>" +
                    "<p><img src=http://www.diagnostic4u.com/img/logo.png'></p></ br>";
                    var message = new MailMessage();
                    message.To.Add(new MailAddress(user.Email));
                    message.Subject = "Notificación Diagnostic4U";
                    message.Body = string.Format(body, "DIAGNOSTIC4U", "jamurillo@grupoautofin.com", "Aqui se envian  las claves de acceso al portal");
                    message.IsBodyHtml = true;

                    ML.EstatusEmail estatusEmail = new ML.EstatusEmail();
                    estatusEmail.Mensaje = body;
                    estatusEmail.Destinatario = user.Email;
                    estatusEmail.BaseDeDatos = new ML.BasesDeDatos();
                    estatusEmail.BaseDeDatos.IdBaseDeDatos = dataEncuesta.BasesDeDatos.IdBaseDeDatos;
                    estatusEmail.Encuesta = new ML.Encuesta();
                    estatusEmail.Encuesta.IdEncuesta = encuesta.IdEncuesta;

                    using (var smtp = new SmtpClient())
                    {
                        try
                        {
                            smtp.Send(message);
                            result.Correct = true;
                            enviadosCorrectos = enviadosCorrectos + 1;
                            //Alta de EstatusEmail exitoso
                            BL.Encuesta.UpdateFlagEmailToSuccess(estatusEmail, IdEstatusEmailList[index]);
                        }
                        catch (SmtpException ex)
                        {
                            if (ex is Exception || ex is SmtpException)
                            {
                                result.Object = ex.StatusCode;
                                Console.Write(ex.Message);
                                Console.Write(ex.InnerException);
                                Console.Write(ex.StackTrace);
                                //Alta de EstatusEmailFallido
                                BL.Encuesta.UpdateFlagEmailToError(estatusEmail, ex, IdEstatusEmailList[index]);
                            }
                            result.ErrorMessage = ex.Message;
                            result.Correct = false;
                            Debug.WriteLine("Sending email #");
                            string mensajeError = ex.Message;
                            mensajeError += user.Email;
                            HttpRuntime.Cache.Add("logError", ex.Message, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(8),
                            System.Web.Caching.CacheItemPriority.High, null);
                            emailFallido.Add(user.Email);
                        }
                        finally
                        {
                            smtp.Dispose();
                        }
                    }
                }
                else if(encuesta.TipoMensaje != "default" && user.Email != null)
                {
                    FullName = user.Nombre + " " + user.ApellidoPaterno + " " + user.ApellidoMaterno;

                    string mensajeBase64 = Convert.ToString(HttpRuntime.Cache.Get("emailMessage"));

                    string mensaje = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(mensajeBase64));

                    Console.WriteLine(mensaje);


                    mensaje = ReplaceDatosMensaje(mensaje, "*NombreUsuario*", FullName);
                    mensaje = ReplaceDatosMensaje(mensaje, "*NombreEncuesta*", dataEncuesta.Nombre);
                    mensaje = ReplaceDatosMensaje(mensaje, "*FechaInicio*", Convert.ToString(dataEncuesta.FechaInicio));
                    mensaje = ReplaceDatosMensaje(mensaje, "*FechaFin*", Convert.ToString(dataEncuesta.FechaFin));
                    mensaje = ReplaceDatosMensaje(mensaje, "*LinkEncuesta*", "<a style='font-weight:bold' href='" + Redirect + "'>Diagnostic4U</a>");
                    mensaje = ReplaceDatosMensaje(mensaje, "*ClaveAcceso*", user.ClaveAcceso);

                    Console.WriteLine(mensaje);


                    var body = mensaje;

                    var message = new MailMessage();
                    message.To.Add(new MailAddress(user.Email));
                    message.Subject = "Notificación Diagnostic4U";
                    message.Body = string.Format(body, "DIAGNOSTIC4U", "jamurillo@grupoautofin.com", "Aqui se envian las claves de acceso al portal");
                    message.IsBodyHtml = true;

                    ML.EstatusEmail estatusEmail = new ML.EstatusEmail();
                    estatusEmail.Mensaje = body;
                    estatusEmail.Destinatario = user.Email;
                    estatusEmail.BaseDeDatos = new ML.BasesDeDatos();
                    estatusEmail.BaseDeDatos.IdBaseDeDatos = dataEncuesta.BasesDeDatos.IdBaseDeDatos;
                    estatusEmail.Encuesta = new ML.Encuesta();
                    estatusEmail.Encuesta.IdEncuesta = encuesta.IdEncuesta;

                    using (var smtp = new SmtpClient())
                    {
                        try
                        {
                            smtp.Send(message);

                            result.Correct = true;
                            enviadosCorrectos = enviadosCorrectos + 1;
                            BL.Encuesta.AddFlagEmailToSuccess(estatusEmail);
                        }
                        catch (SmtpException ex)
                        {
                            result.ErrorMessage = ex.Message;
                            result.Correct = false;
                            Debug.WriteLine("Sending email #");
                            string mensajeError = ex.Message;
                            mensajeError += user.Email;
                            HttpRuntime.Cache.Add("logError", ex.Message, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(8),
                            System.Web.Caching.CacheItemPriority.High, null);
                            BL.Encuesta.AddFlagEmailToError(estatusEmail, ex);
                        }
                        finally
                        {
                            smtp.Dispose();
                        }
                    }
                }
                index = index + 1;
            }
            HttpRuntime.Cache.Add("EnviadosCorrectos", enviadosCorrectos, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(8),
                System.Web.Caching.CacheItemPriority.High, null);

            HttpRuntime.Cache.Add("TotalAEnviar", TotalEmails, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(8),
            System.Web.Caching.CacheItemPriority.High, null);
            return RedirectToAction("GetAll");
        }
        private static string ReplaceDatosMensaje(string mensajeOriginal, string Find, string NewValue)
        {
            int Place = mensajeOriginal.IndexOf(Find);
            string result = mensajeOriginal.Remove(Place, Find.Length).Insert(Place, NewValue);
            return result;
        }

        public ActionResult PreguntaTerminaE(ML.ConfiguraRespuesta conf)
        {
            var result = BL.Encuesta.AddPreguntaTermina(conf);
            if (result.Correct == true)
            {
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult PreguntaHideNextSection(ML.ConfiguraRespuesta conf)
        {
            var result = BL.Encuesta.PreguntaHideNextSection(conf);
            if (result.Correct == true)
            {
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult PreguntaHideNextSubSeccion(ML.ConfiguraRespuesta conf)
        {
            var result = BL.Encuesta.PreguntaHideNextSubSeccion(conf);
            if (result.Correct == true)
            {
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteTermina(ML.ConfiguraRespuesta conf)
        {
            var result = BL.Encuesta.DeleteTermina(conf);
            if (result.Correct == true)
            {
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetRespuestaTermina(ML.Encuesta encuesta)
        {
            var result = BL.Encuesta.GetRespuestaTermina(encuesta);

            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetRespuestaHideNextSection(ML.Encuesta encuesta)
        {
            var result = BL.Encuesta.GetRespuestaHideNextSection(encuesta);

            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRespuestaHideNextSubSection(ML.Encuesta encuesta)
        {
            var result = BL.Encuesta.GetRespuestaHideNextSubSection(encuesta);

            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPeriodoEncuesta(ML.Encuesta encuesta)
        {
            var result = BL.Encuesta.getPeriodoByEncuesta(encuesta);
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetEstatusEnvio(ML.Encuesta encuesta)
        {
            string IdEncuesta = Convert.ToString(encuesta.IdEncuesta);
            var rEnc = BL.Encuesta.GetDataFromEncuesta(IdEncuesta);
            return View(rEnc);
        }
        [HttpGet]
        public ActionResult GetEstatusEnviojson(ML.Encuesta encuesta)
        {
            var result = BL.Encuesta.GetEstatusEnvioByIdBaseDeDatos(encuesta);
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }

        public ActionResult deletePreguntaOpen(ML.ConfiguraRespuesta conf)
        {
            var result = BL.Encuesta.DeletePreguntaOpen(conf);
            if (result.Correct == true)
            {
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetTitleBySeccion(string idpregunta)
        {
            int IDPreg = Convert.ToInt32(idpregunta);
            var result = BL.Encuesta.GetTitleBySeccion(IDPreg);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPregH(string IdEncuesta)
        {
            var result = BL.Encuesta.getPregH(IdEncuesta);
            return Json(result.Object, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRespuestasGuardadas(ML.Encuesta encuesta)
        {
            int Idusuario = (int)Session["IdUsuarioLog"];
            var result = BL.Encuesta.GetRespuestas(encuesta.IdEncuesta, Idusuario);

            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RedirectToLogin(string UID)
        {
            var result = BL.Encuesta.getEncuestaByUID(UID);
            return Json(result.Object, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateCL()
        {
            string qsIdPlantilla = Request.QueryString["IdPlantilla"];
            string idsessionAdmin = Convert.ToString(Session["IdAdministradorLogeado"]);
            ML.Encuesta modelo = new ML.Encuesta();
            //var resulListEmpresa = BL.Company.GetAllCompany();Todas
            List<object> permisosEstructura = new List<object>();
            ViewBag.Permisos = Session["CompaniesPermisos"];
            permisosEstructura = ViewBag.Permisos;

            var miEmpresaOrigen = Convert.ToInt32(Session["CompanyDelAdminLog"]);
            var resulListEmpresa = BL.Empresa.GetAll(permisosEstructura);//GetFiltrado          
            var listadoTipoOrden = BL.TipoOrden.getAllTipoOrden();                    
            modelo.ListDataBase = BL.BasesDeDatos.GetBDClima().Objetos;//listadoBaseDeDatosAnonima.ListadoDeBaseDeDatos;

            //var listadoEnfoquePregunta = BL.Preguntas.getEnfoquePregunta();
            var listadoCompetenciaPreguntas = BL.Competencia.getCompetencias(idsessionAdmin);
            var listadoTipoControl = BL.TipoControl.getTipoControlCL();            
            modelo.ListEmpresas = resulListEmpresa.Objects;
            modelo.ListTipoControl = listadoTipoControl.ListadoTipoControl;
            modelo.ListCompetencias = listadoCompetenciaPreguntas.ListadoCompetenciasPregunta;
            // modelo.Instruccion ="<div class='row main-content' style='padding-top:100px;' ng-init='vm.seccionesEncuesta.Id = 2'><div class='col-xs-12 col-sm-12 col-md-12 col-lg-5 col-xl-4 imagen-intro' id='img-intro'></div><div class='col-xs-12 col-sm-12 col-md-12 col-lg-7 col-xl-8' style='padding-top:30px; padding-left:30px; padding-right:30px;'><p class='welcome'>{{ vm.bienvenida }}</p><p class='text-justify'>{{ vm.introduccion }}</p><div class='row center-vertically'><div class='col-xs-3 col-sm-4 col-md-3 col-lg-4 col-xl-2 des-likert' style='background-color:rgb(241, 90, 36);'><p class='val'>{{ vm.val1 }}</p></div><div class='col-xs-3 col-sm-4 col-md-3 col-lg-4 col-xl-2 des-likert' style='background-color:rgb(247, 147, 30);'><p class='val'>{{ vm.val2 }}</p></div><div class='col-xs-3 col-sm-4 col-md-3 col-lg-4 col-xl-2 des-likert' id='val3' style='background-color: #cccc01;'><p class='val'>{{ vm.val3 }}</p></div><div class='col-xs-3 col-sm-4 col-md-3 col-lg-4 col-xl-2 des-likert' style='background-color:rgb(140, 198, 63);'><p class='val'>{{ vm.val4 }}</p></div><div class='col-xs-3 col-sm-4 col-md-3 col-lg-4 col-xl-2 des-likert' style='background-color:rgb(57, 181, 74);'><p class='val'>{{ vm.val5 }}</p></div></div><p class='text-justify' style='padding-top:35px;'>{{ vm.descripcionIntro1 }}</p><p class='text-justify'>{{ vm.descripcionIntro2 }}</p><p class='text-justify'>{{ vm.descripcionIntro3 }}</p><div class='row center-vertically' style='text-align:center;padding-top:15px;'><div class='col-xs-12 col-sm-12 col-md-6 col-lg-8 col-xl-4'><p class='welcome'>¡Tu participación es muy importante!</p><p class='welcome'>Para comenzar con tu aplicación, presiona el boton continuar.</p></div></div><div class='row center-vertically'><div class='col-xs-12 col-sm-12 col-md-7 col-lg-5 col-xl-4 btn-continuar text-center'><p class='btn-continuar' style='padding: 0.8rem;'>Continuar</p></div></div></div></div>";
            modelo.Instruccion = ML.ClimaDinamico.defaultHtmlIntro;//"<div class='row main-content' style='padding-top:100px;' ng-init='vm.seccionesEncuesta.Id = 2'>   <div class='col-xs-12 col-sm-12 col-md-12 col-lg-5 col-xl-4 imagen-intro' id='img-intro'></div>   <div class='col-xs-12 col-sm-12 col-md-12 col-lg-7 col-xl-8' style='padding-top:30px; padding-left:30px; padding-right:30px;'>      <p class='welcome'>Bienvenido(a) a la encuesta de Clima laboral</p>      <p class='text-justify'>A continuación, se te presentaran una serie de reactivos que te pedimos respondas con honestidad según el grado que mejor refleje tu punto de vista, de acuerdo con la siguiente escala:</p>      <div class='row center-vertically'>         <div class='col-xs-3 col-sm-4 col-md-3 col-lg-4 col-xl-2 des-likert' style='background-color:rgb(241, 90, 36);'>            <p class='val'>Casi siempre es verdad</p>         </div>         <div class='col-xs-3 col-sm-4 col-md-3 col-lg-4 col-xl-2 des-likert' style='background-color:rgb(247, 147, 30);'>            <p class='val'>Frecuentemente es verdad</p>         </div>         <div class='col-xs-3 col-sm-4 col-md-3 col-lg-4 col-xl-2 des-likert' id='val3' style='background-color: #cccc01;'>            <p class='val'>A veces es falso / A veces es verdad</p>         </div>         <div class='col-xs-3 col-sm-4 col-md-3 col-lg-4 col-xl-2 des-likert' style='background-color:rgb(140, 198, 63);'>            <p class='val'>Frecuentemente es falso</p>         </div>         <div class='col-xs-3 col-sm-4 col-md-3 col-lg-4 col-xl-2 des-likert' style='background-color:rgb(57, 181, 74);'>            <p class='val'>Casi siempre es falso</p>         </div>      </div>      <p class='text-justify' style='padding-top:35px;'>Para realizar la encuesta deberás responder a cada reactivo desde dos enfoques, pensando en la situacion actual de 'la empresa y todos los jefes' y pensando en la situación actual de 'tu área de trabajo y jefe directo'. Algunos reactivos refieren a cuestiones personales, por lo que deberás responderlos de la misma forma en ambos enfoques.</p>      <p class='text-justify'>Toma en cuenta que esta encuesta es confidencial y que la información que proporciones será procesada por un equipo ético y especializado que presentará resultados de manera general y nunca de manera particular.</p>      <p class='text-justify'>Recuerda que la calidad de la información y las acciones de mejora que se deriven de ella dependerán en la honestidad con que respondas.</p>      <div class='row center-vertically' style='text-align:center;padding-top:15px;'>         <div class='col-xs-12 col-sm-12 col-md-6 col-lg-8 col-xl-4'>            <p class='welcome'>¡Tu participación es muy importante!</p>            <p class='welcome'>Para comenzar con tu aplicación, presiona el boton continuar.</p>         </div>      </div>      <div class='row center-vertically'>         <div class='col-xs-12 col-sm-12 col-md-7 col-lg-5 col-xl-4 btn-continuar text-center'>            <p class='btn-continuar' style='padding: 0.8rem;'>Continuar</p>         </div>      </div>   </div></div>";
            //modelo.Agradecimiento ="<div class='col-xs-12 col-sm-12 col-md-12 col-lg-8 col-xl-8'><p class='instrucciones-likert text-justify'>{{ vm.likertInstruccion1 }}</p><p class='instrucciones-likert text-justify'>{{ vm.likertInstruccion2 }}</p></div>";
            modelo.Agradecimiento = ML.ClimaDinamico.defaultHtmlInstrucciones;//"<div class='col-xs-12 col-sm-12 col-md-12 col-lg-8 col-xl-8'>   <p class='instrucciones-likert text-justify'>Responde a cada reactivo desde los dos enfoques, pensando en la situación actual de 'la empresa y todos los jefes' y pensando en la situacion actual de 'tu área de trabajo y jefe directo'.</p>   <p class='instrucciones-likert text-justify'>Los reactivos que refieren a cuestiones personales, deberás responderlos de la misma forma en ambos enfoques.</p></div>";
            modelo.ListTipoOrden = listadoTipoOrden.ListTipoOrden;
            modelo.ListCategorias = BL.Categoria.getAllCategorias(idsessionAdmin).EditaEncuesta.ListCategorias;
            modelo.Preguntas = new ML.Preguntas();
            if (qsIdPlantilla != "")
            {
                modelo.Plantillas = new ML.Plantillas();
                modelo.Plantillas.IdPlantilla = Convert.ToInt32(qsIdPlantilla);
            }
            modelo.ListCatCol2 = BL.Categoria.getAllConfiguration(1);
            modelo.ListSubCatCol3 = BL.Categoria.getAllConfigurationPreSubCat(1);
            ML.Encuesta enc = new ML.Encuesta();
            return View(modelo);
        }
        public ActionResult CreateListCL(int aIdCompetencia)
        {
            //recupera las preguntas de la Competencia
            var listadoPreguntasbyIdCompetencia = BL.Competencia.getPreguntasByCompetencia(aIdCompetencia);
            return Json(listadoPreguntasbyIdCompetencia.ListadoPreguntaCompetencias, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateListCLEdit(int aidEncuesta,int aidCompetencia)
        {
            var listadoPreguntasEditCL = BL.Preguntas.getPreguntasEditEncuestaCL(aidEncuesta,aidCompetencia);
            return Json(listadoPreguntasEditCL.ListadoPreguntaCompetencias,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult CreateNewCuestionCL(ML.Preguntas cuestionModel)
        {
            string idsessionAdmin = Convert.ToString(Session["IdAdministradorLogeado"]);
            var cuestionViewModel = new ML.Preguntas();
            var listadoTipoControl = BL.TipoControl.getTipoControl();
            var listadoCompetenciaPreguntas = BL.Competencia.getCompetencias(idsessionAdmin);
            var listadoEnfoquePregunta = BL.Preguntas.getEnfoquePregunta();
            cuestionViewModel.ListTipoControl = listadoTipoControl.ListadoTipoControl;
            cuestionViewModel.ListCompetencia = listadoCompetenciaPreguntas.ListadoCompetenciasPregunta;
            cuestionViewModel.ListEnfoque = listadoEnfoquePregunta.ListadoEnfoquesPregunta;
            cuestionViewModel.Pregunta = cuestionModel.Pregunta;
            cuestionViewModel.IdEncuesta = cuestionModel.IdEncuesta;
            cuestionViewModel.IdPregunta = cuestionModel.IdPregunta;
            cuestionViewModel.IdPreguntaPadre = cuestionModel.IdPreguntaPadre;
            cuestionViewModel.IdEnfoque = cuestionModel.IdEnfoque;
            cuestionViewModel.Valoracion = cuestionModel.Valoracion;
            cuestionViewModel.Competencia = new ML.Competencia();
            //cuestionViewModel.IdentificadorTipoControl = (Int32)cuestionModel.TipoControl.IdTipoControl;
            cuestionViewModel.Competencia.IdCompetencia = cuestionModel.Competencia.IdCompetencia;
            cuestionViewModel.Competencia.Nombre = cuestionModel.Competencia.Nombre;
            cuestionViewModel.TipoControl = new ML.TipoControl();
            cuestionViewModel.TipoControl.IdTipoControl = cuestionModel.TipoControl.IdTipoControl;
            cuestionViewModel.Obligatoria = cuestionModel.Obligatoria;       
            cuestionViewModel.UniqueId = Guid.NewGuid();            
            return PartialView("~/Views/Preguntas/PreguntasAddCL.cshtml", cuestionViewModel);
        }
        /// <summary>
        /// Metodo que inserta por medio del BeginCollectionItem las preguntas a la pagina de edicion (IList)
        /// </summary>
        /// <param name="cuestionModel">Se necesita el objeto con los datos para que se puedan editar y tomar de referencia al guardar la edición</param>
        /// <returns>En el front inserta un objeto html con los datos a editar</returns>
        [HttpPost]
        public ActionResult EditNewCuestionCL(ML.Preguntas cuestionModel)
        {
            string idsessionAdmin = Convert.ToString(Session["IdAdministradorLogeado"]);
            var cuestionViewModel = new ML.Preguntas();
            //var listadoTipoControl = BL.TipoControl.getTipoControl();
            var listadoCompetenciaPreguntas = BL.Competencia.getCompetencias(idsessionAdmin);
            //var listadoEnfoquePregunta = BL.Preguntas.getEnfoquePregunta();
            //cuestionViewModel.ListTipoControl = listadoTipoControl.ListadoTipoControl;
            cuestionViewModel.ListCompetencia = listadoCompetenciaPreguntas.ListadoCompetenciasPregunta;
            //cuestionViewModel.ListEnfoque = listadoEnfoquePregunta.ListadoEnfoquesPregunta;
            cuestionViewModel.IdPregunta = cuestionModel.IdPregunta;
            cuestionViewModel.IdEncuesta = cuestionModel.IdEncuesta;
            cuestionViewModel.IdCompetencia = cuestionModel.IdCompetencia;
            cuestionViewModel.Pregunta = cuestionModel.Pregunta;
            cuestionViewModel.IdPreguntaPadre = cuestionModel.IdPreguntaPadre;
            cuestionViewModel.IdEnfoque = cuestionModel.IdEnfoque;
            cuestionViewModel.Valoracion = cuestionModel.Valoracion;
            cuestionViewModel.Competencia = new ML.Competencia();
            cuestionViewModel.Competencia.IdCompetencia = cuestionModel.Competencia.IdCompetencia;
            cuestionViewModel.Competencia.Nombre = cuestionModel.Competencia.Nombre;
            cuestionViewModel.UniqueId = Guid.NewGuid();
            return PartialView("~/Views/Preguntas/PreguntasAddCL.cshtml", cuestionViewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult PreviewData(ML.Encuesta encuesta)
        {
            return Json(encuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateCL(ML.Encuesta encuesta) {
            int configura = 0;          
            int usuarioCreacion = Convert.ToInt32(Session["IdAdministradorLogeado"]);
            ML.Result altaEncuesta = BL.Encuesta.AddCL(encuesta, usuarioCreacion);
            
            if (altaEncuesta.Correct)
            {
                ViewBag.Message = "La Encuesta se creo correctamente";
                if (encuesta.TipoOrden.IdTipoOrden == 3)
                {
                    Session["IdEncuestaAlta"] = altaEncuesta.idEncuestaAlta;
                    return RedirectToAction("ConfiguraOrden");
                }
                if (configura == 1 && encuesta.SeccionarEncuesta == false)
                {
                    Session["IdEncuestaAlta"] = altaEncuesta.idEncuestaAlta;
                    return RedirectToAction("ConfigurarCondiciones");
                }
                else if (configura != 1 && encuesta.SeccionarEncuesta == false)
                {
                    return RedirectToAction("GetAll");
                }
            }
            else
            {
                ViewBag.Message = "La Encuesta no se ha podido crear";
                return RedirectToAction("CreateCL");
            }
            return RedirectToAction("GetAll");

        }
        ///Orden Personalizado
        [HttpGet]
        public ActionResult ConfiguraOrden()
        {
            int IdEncuesta = Convert.ToInt32(Session["IdEncuestaAlta"]);
            var result = BL.Encuesta.getEncuestaByIdOrden(IdEncuesta);
            return View(result);
            //return View(result.EditaEncuesta);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ConfiguraOrden(ML.Result listdoPreguntas)
        {

            var result = BL.Encuesta.ConfiguraOrden(listdoPreguntas.ListadoPreguntas);

            if (result.Correct == true)
            {
                return Json("success");
            }
            else {return Json("error"); }

        }       
        /// <summary>
        /// Metodo para la carga de una encuesta a editar del tipo 4 Clima Laboral
        /// </summary>
        /// <param name="idEncuestaCL">necesita del id Encuesta para traer la consulta</param>
        /// <returns>Objeto de modelo Encuesta con lsitado de configuraciones, preguntas e informacion de la encuestas</returns>
        public ActionResult EditCL(string idEncuestaCL) {
            string idsessionAdmin = Convert.ToString(Session["IdAdministradorLogeado"]);
            ML.Result encuestaCL = BL.Encuesta.getEncuestaByIdEditClimaL(Convert.ToInt32(idEncuestaCL),idsessionAdmin);
            encuestaCL.EditaEncuesta.ListCatCol2 = BL.Categoria.getAllConfiguration(Convert.ToInt32(idEncuestaCL));
            encuestaCL.EditaEncuesta.ListSubCatCol3 = BL.Categoria.getAllConfigurationPreSubCat(Convert.ToInt32(idEncuestaCL));
            ///se agrega el MAX de Id pregunta por si quieren agregar nueva pregunta y tener continuidad de el id padre
            encuestaCL.EditaEncuesta.idMaxPregunta = BL.Preguntas.getMaxIdPadre(Convert.ToInt32(idEncuestaCL));
            List<object> permisosEstructura = new List<object>();
            ViewBag.Permisos = Session["CompaniesPermisos"];
            permisosEstructura = ViewBag.Permisos;
            var resulListEmpresa = BL.Empresa.GetAll(permisosEstructura);//GetFiltrado  
            encuestaCL.EditaEncuesta.ListEmpresas = resulListEmpresa.Objects;

            return View(encuestaCL.EditaEncuesta);
        }
        /// <summary>
        /// Metodo para editar la encuesta de clima dinamico, solo necesitamos el id de Encuesta y la sesion activa del usuario Administardor
        /// </summary>
        /// <param name="encuestaCl">Id de la encuesta a editar de tipo 4 (Encuesta Clima Laboral)</param>
        /// <returns>La modificacion completa de la encuesta y alta de preguntas con sus respectivas respuestas y/o alta de confoguraciones de categorias o subcategorias</returns>      
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditCLE(ML.Encuesta encuestaCl)
        {            
            string CURRENT_USER = Convert.ToString(Session["IdAdministradorLogeado"]);
            encuestaCl.UsuarioModificacion = CURRENT_USER;
            ML.Result editaEncuestaCl = BL.Encuesta.EditCL(encuestaCl, CURRENT_USER);
            if (editaEncuestaCl.Correct)
            {
                ViewBag.Message = "La Encuesta se edito correctamente";
                if (encuestaCl.TipoOrden.IdTipoOrden == 3)
                {
                    Session["IdEncuestaAlta"] = encuestaCl.IdEncuesta;
                    return RedirectToAction("ConfiguraOrden");
                }               
                else 
                {
                    return RedirectToAction("GetAll");
                }
            }
            else
            {
                ViewBag.Message = "La Encuesta no se ha podido editar";
                return RedirectToAction("GetAll");
            }
            //return Json("");
        }
        public ActionResult CreateNewAnswereCategorias(ML.Categoria mCategoria)
        {

            var answereViewModel = new ML.Categoria();
            //strNombre = strNombre.Split('-',);
            //answereViewModel.UniqueId = Guid.NewGuid();
            answereViewModel.IdCategoria = Convert.ToInt32(mCategoria.IdCategoria);
            answereViewModel.Nombre = mCategoria.Nombre;
            answereViewModel.IdPadreObjeto = mCategoria.IdPadreObjeto;
            return PartialView("~/Views/Categorias/CategoriasAdd.cshtml", answereViewModel);
        }
        [HttpPost]//Se cambia nombre de la tabla de ValoracionCategoria a ValoracionPreguntaPorSubcategoria // Partial View tercera Columna
        public ActionResult CreateNewCatVal(ML.ValoracionPreguntaPorSubcategoria ctaValModel)
        {
            var ctaValViewModel = new ML.ValoracionPreguntaPorSubcategoria();
            ctaValViewModel.NombrePregunta = ctaValModel.NombrePregunta;
            ctaValViewModel.Valor = ctaValModel.Valor;
            ctaValViewModel.IdSubcategoria = ctaValModel.IdSubcategoria;
            ctaValViewModel.IdPregunta = ctaValModel.IdPregunta;
            ctaValViewModel.UniqueId = Guid.NewGuid();
            ctaValViewModel.IdValoracionPreguntaPorSubcategoria = ctaValModel.IdValoracionPreguntaPorSubcategoria;
            return PartialView("~/Views/Categorias/ValPregSubCatAdd.cshtml", ctaValViewModel);
        }
        [HttpPost]//Partial View segunda Columna
        public ActionResult CreateNewSubCatCat(ML.ValoracionSubcategoriaPorCategoria ctaValModel)
        {
            var ctaValViewModel = new ML.ValoracionSubcategoriaPorCategoria();
            ctaValViewModel.NombreCat = ctaValModel.NombreCat;
            ctaValViewModel.Valor = ctaValModel.Valor;
            ctaValViewModel.IdSubcategoria = ctaValModel.IdSubcategoria;
            ctaValViewModel.IdCategoria = ctaValModel.IdCategoria;
            ctaValViewModel.UniqueId = Guid.NewGuid();
            ctaValViewModel.IdValoracionSubcategoriaPorCategoria = ctaValModel.IdValoracionSubcategoriaPorCategoria;
            return PartialView("~/Views/Categorias/ValSubCatCatAdd.cshtml", ctaValViewModel);

        }
        public ActionResult ListadoCompetenciasInicio()
        {
            ML.Encuesta modelo = new ML.Encuesta();
            string idSessionAdmin = Convert.ToString(Session["IdAdministradorLogeado"]);
            var listadoCompetenciaPreguntas = BL.Competencia.getCompetenciasConPreguntas(idSessionAdmin);
            modelo.ListCompetencias =listadoCompetenciaPreguntas.ListadoCompetenciasPregunta;
            return Json(modelo.ListCompetencias,JsonRequestBehavior.AllowGet);

        }
        public ActionResult ListadoCompetenciasInicioCLEdit(int aIdEncuesta)
        {
            ML.Encuesta modelo = new ML.Encuesta();
            string idSessionAdmin = Convert.ToString(Session["IdAdministradorLogeado"]);
            var listadoCompetenciaPreguntas = BL.Competencia.getcompetenciasConPreguntaCLEdit(idSessionAdmin,aIdEncuesta);
            modelo.ListCompetencias = listadoCompetenciaPreguntas.ListadoCompetenciasPregunta;
            return Json(modelo.ListCompetencias, JsonRequestBehavior.AllowGet);

        }
        public ActionResult consultaVSCpC(int idEncuesta)
        {
            var result = BL.Categoria.getAllConfiguration(idEncuesta);

            return Json(result,JsonRequestBehavior.AllowGet);
        }
        public ActionResult consultaOpcion2VSCpC(int idEncuesta, int idCat)
        {
            var result = BL.Categoria.getAllConfiguration(idEncuesta,idCat);

            return Json(result,JsonRequestBehavior.AllowGet);
        }
        public ActionResult consultaVPregpC(int idEncuesta)
        {
            var result = BL.Categoria.getAllConfigurationPreSubCat(idEncuesta);

            return Json(result,JsonRequestBehavior.AllowGet);
        }
        public JsonResult getCategorias()
        {
            var data = BL.Categoria.getCategorias();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult consultaOpcion2VPregpC(int idEncuesta,int idCat)
        {
            var result = BL.Categoria.getAllConfigurationPreSubCat(idEncuesta,idCat);

            return Json(result,JsonRequestBehavior.AllowGet);
        }
        public ActionResult consultaSubCatPorIdCat(int idEncuesta, int idCat)
        {
            var result = BL.Categoria.getSubCatByIdCat(idEncuesta,idCat);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getPreguntasDefault()
        {
            var result = BL.Encuesta.getPreguntasDefault();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConfigurarEmails()
        {
            return View();
        }
        public JsonResult SendEmailsCustom(ML.EmailsEncuesta emailsEncuesta)
        {
            return Json(BL.Encuesta.SendEmailsCustom(emailsEncuesta), JsonRequestBehavior.AllowGet);
        }

    }
}