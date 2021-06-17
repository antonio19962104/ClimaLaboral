using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using RazorEngine;
using RazorEngine.Templating;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Microsoft;
namespace PL.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult ViewPrueba()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            return View(result);
        }
        public ActionResult Prueba()
        {
            var result = BL.Respuestas.Prueba();
            return Content(result.ErrorMessage);
        }
        public ActionResult Test()
        {
            var result = BL.Estatus.Test();
            return Content("alert(result);");
        }
        public ActionResult validarSession()
        {
            if (Session["EmpleadoEncuestado"] != null)
            {
                int Seccion = Convert.ToInt32(Session["AvanceSeccion"]);
                                
                return Json("success");
            }
            else
            {
                return Json("expired");
            }
        }

        public ActionResult Index_CL()
        {
            Session["EmpleadoEncuestado"] = null;
            return View("Index_CL");
        }
        public ActionResult Index_Aux()
        {
            Session["EmpleadoEncuestado"] = null;
            return View("Index_Aux");
        }
        public ActionResult ConsultarAvance()
        {
            ML.EmpleadoRespuesta EmpleadoRespuestas = new ML.EmpleadoRespuesta();
            EmpleadoRespuestas.Empleado = new ML.Empleado();
            EmpleadoRespuestas.Empleado.IdEmpleado = Convert.ToInt32(Session["EmpleadoEncuestado"]);

            var getRespuestas = BL.Respuestas.GetRespuestasCLByEmpleado(EmpleadoRespuestas);

            
            

            var result = BL.Encuesta.ConsultarAvance(EmpleadoRespuestas);

            

            if (result.Avance == 1)
            {
                //Si me envia aqui no hay nada que rellenar
                Session["AvanceSeccion"] = 1;
                Session["progreso"] = 0;
                return View("EncuestaP1");
            }
            else

            if (result.Avance == 2)
            {
                Session["progreso"] = 8;
                Session["AvanceSeccion"] = 1;
                // 1-8 => 87-94
                List<string> Respuesta1 = new List<string>();
                List<string> RespuestaFinal = new List<string>();
                Respuesta1.Add("ValorDefault");
                RespuestaFinal.Add("ValorDefault");
                foreach (ML.ResEmpleado obj in getRespuestas.Objects)
                {
                    Respuesta1.Add(obj.RespuestaEmpleado);
                }

                for (int i = 1; i < 17; i++)
                {
                    //1-16
                    RespuestaFinal.Add(Respuesta1[i]);
                }

                if (RespuestaFinal.Count < 16)
                {
                    ViewBag.Answers1 = Respuesta1;
                }
                else
                {
                    ViewBag.Answers1 = RespuestaFinal;
                }

                


                return View("EncuestaP1");
            }
            else
            if (result.Avance == 3)
            {
                Session["progreso"] = 16;
                Session["AvanceSeccion"] = 2;
                // 1-16 => 87-102
                List<string> Respuesta1 = new List<string>();
                List<string> Respuesta1Final = new List<string>();
                Respuesta1Final.Add("ValorDefault");
                Respuesta1.Add("ValorDefault");
                foreach (ML.ResEmpleado obj in getRespuestas.Objects)
                {
                    //Trae todas las que consulta
                    Respuesta1.Add(obj.RespuestaEmpleado);
                }
                
                for (int i = 9; i < 17; i++)
                {
                    //9-16
                    Respuesta1Final.Add(Respuesta1[i]);
                }
                for (int i = 25; i < 33; i++)
                {
                    //25-32
                    Respuesta1Final.Add(Respuesta1[i]);
                }
                ViewBag.Answers2 = Respuesta1Final;
                return View("EncuestaP2");
            }
            else
            if (result.Avance == 4)
            {
                Session["progreso"] = 24;
                Session["AvanceSeccion"] = 3;
                // 1-24 => 87-110
                List<string> Respuesta1 = new List<string>();
                List<string> Respuesta1Final = new List<string>();
                Respuesta1Final.Add("ValorDefault");
                Respuesta1.Add("ValorDefault");
                foreach (ML.ResEmpleado obj in getRespuestas.Objects)
                {
                    //Trae todas las que consulta
                    Respuesta1.Add(obj.RespuestaEmpleado);
                }

                for (int i = 17; i < 25; i++)
                {
                    //17-24
                    Respuesta1Final.Add(Respuesta1[i]);
                }
                for (int i = 41; i < 49; i++)
                {
                    //14-48
                    Respuesta1Final.Add(Respuesta1[i]);
                }
                ViewBag.Answers3 = Respuesta1Final;
                return View("EncuestaP3");
            }
            else
            if (result.Avance == 5)
            {
                Session["progreso"] = 32;
                Session["AvanceSeccion"] = 4;
                List<string> Respuesta1 = new List<string>();
                List<string> Respuesta1Final = new List<string>();
                Respuesta1Final.Add("ValorDefault");
                Respuesta1.Add("ValorDefault");
                foreach (ML.ResEmpleado obj in getRespuestas.Objects)
                {
                    //Trae todas las que consulta
                    Respuesta1.Add(obj.RespuestaEmpleado);
                }

                for (int i = 25; i < 33; i++)
                {
                    //17-24
                    Respuesta1Final.Add(Respuesta1[i]);
                }
                for (int i = 57; i < 65; i++)
                {
                    //14-48
                    Respuesta1Final.Add(Respuesta1[i]);
                }
                ViewBag.Answers4 = Respuesta1Final;
                return View("EncuestaP4");
            }
            else
            if (result.Avance == 6)
            {
                Session["progreso"] = 40;
                Session["AvanceSeccion"] = 5;
                List<string> Respuesta1 = new List<string>();
                List<string> Respuesta1Final = new List<string>();
                Respuesta1Final.Add("ValorDefault");
                Respuesta1.Add("ValorDefault");
                foreach (ML.ResEmpleado obj in getRespuestas.Objects)
                {
                    //Trae todas las que consulta
                    Respuesta1.Add(obj.RespuestaEmpleado);
                }

                for (int i = 33; i < 41; i++)
                {
                    //17-24
                    Respuesta1Final.Add(Respuesta1[i]);
                }
                for (int i = 73; i < 81; i++)
                {
                    //14-48
                    Respuesta1Final.Add(Respuesta1[i]);
                }
                ViewBag.Answers5 = Respuesta1Final;
                return View("EncuestaP5");
            }
            else
            if (result.Avance == 7)
            {
                Session["progreso"] = 48;
                Session["AvanceSeccion"] = 6;
                List<string> Respuesta1 = new List<string>();
                List<string> Respuesta1Final = new List<string>();
                Respuesta1Final.Add("ValorDefault");
                Respuesta1.Add("ValorDefault");
                foreach (ML.ResEmpleado obj in getRespuestas.Objects)
                {
                    //Trae todas las que consulta
                    Respuesta1.Add(obj.RespuestaEmpleado);
                }

                for (int i = 41; i < 49; i++)
                {
                    //17-24
                    Respuesta1Final.Add(Respuesta1[i]);
                }
                for (int i = 89; i < 97; i++)
                {
                    //14-48
                    Respuesta1Final.Add(Respuesta1[i]);
                }
                ViewBag.Answers6 = Respuesta1Final;
                return View("EncuestaP6");
            }
            if (result.Avance == 8)
            {
                Session["progreso"] = 56;
                Session["AvanceSeccion"] = 7;

                List<string> Respuesta1 = new List<string>();
                List<string> Respuesta1Final = new List<string>();
                Respuesta1Final.Add("ValorDefault");
                Respuesta1.Add("ValorDefault");
                foreach (ML.ResEmpleado obj in getRespuestas.Objects)
                {
                    //Trae todas las que consulta
                    Respuesta1.Add(obj.RespuestaEmpleado);
                }

                for (int i = 49; i < 57; i++)
                {
                    //17-24
                    Respuesta1Final.Add(Respuesta1[i]);
                }
                for (int i = 105; i < 113; i++)
                {
                    //14-48
                    Respuesta1Final.Add(Respuesta1[i]);
                }
                ViewBag.Answers7 = Respuesta1Final;
                return View("EncuestaP7");
            }
            else

            if (result.Avance == 9)
            {
                Session["progreso"] = 64;
                Session["AvanceSeccion"] = 8;
                List<string> Respuesta1 = new List<string>();
                List<string> Respuesta1Final = new List<string>();
                Respuesta1Final.Add("ValorDefault");
                Respuesta1.Add("ValorDefault");
                foreach (ML.ResEmpleado obj in getRespuestas.Objects)
                {
                    //Trae todas las que consulta
                    Respuesta1.Add(obj.RespuestaEmpleado);
                }

                for (int i = 57; i < 65; i++)
                {
                    //17-24
                    Respuesta1Final.Add(Respuesta1[i]);
                }
                for (int i = 121; i < 129; i++)
                {
                    //14-48
                    Respuesta1Final.Add(Respuesta1[i]);
                }
                ViewBag.Answers8 = Respuesta1Final;
                return View("EncuestaP8");
            }
            else
            if (result.Avance == 10)
            {
                Session["progreso"] = 72;
                Session["AvanceSeccion"] = 9;
                List<string> Respuesta1 = new List<string>();
                List<string> Respuesta1Final = new List<string>();
                Respuesta1Final.Add("ValorDefault");
                Respuesta1.Add("ValorDefault");
                foreach (ML.ResEmpleado obj in getRespuestas.Objects)
                {
                    //Trae todas las que consulta
                    Respuesta1.Add(obj.RespuestaEmpleado);
                }

                for (int i = 65; i < 73; i++)
                {
                    //17-24
                    Respuesta1Final.Add(Respuesta1[i]);
                }
                for (int i = 137; i < 145; i++)
                {
                    //14-48
                    Respuesta1Final.Add(Respuesta1[i]);
                }
                ViewBag.Answers9 = Respuesta1Final;
                return View("EncuestaP9");
            }
            else
            if (result.Avance == 11)
            {
                Session["progreso"] = 80;
                Session["AvanceSeccion"] = 10;
                List<string> Respuesta1 = new List<string>();
                List<string> Respuesta1Final = new List<string>();
                Respuesta1Final.Add("ValorDefault");
                Respuesta1.Add("ValorDefault");
                foreach (ML.ResEmpleado obj in getRespuestas.Objects)
                {
                    //Trae todas las que consulta
                    Respuesta1.Add(obj.RespuestaEmpleado);
                }

                for (int i = 73; i < 81; i++)
                {
                    //17-24
                    Respuesta1Final.Add(Respuesta1[i]);
                }
                for (int i = 153; i < 161; i++)
                {
                    //14-48
                    Respuesta1Final.Add(Respuesta1[i]);
                }
                ViewBag.Answers9A = Respuesta1Final;
                return View("EncuestaP9A");
            }
            else
            if (result.Avance == 12)
            {
                Session["progreso"] = 86;
                Session["AvanceSeccion"] = 11;
                List<string> Respuesta1 = new List<string>();
                List<string> Respuesta1Final = new List<string>();
                Respuesta1Final.Add("ValorDefault");
                Respuesta1.Add("ValorDefault");
                foreach (ML.ResEmpleado obj in getRespuestas.Objects)
                {
                    //Trae todas las que consulta
                    Respuesta1.Add(obj.RespuestaEmpleado);
                }
                //IdPregunta 144 CSF


                Respuesta1Final.Add(Respuesta1[144]);
                Respuesta1Final.Add(Respuesta1[58]);

                for (int i = 168; i < 173; i++)
                {
                    Respuesta1Final.Add(Respuesta1[i]);
                }

                for (int i = 82; i < 87; i++)
                {
                    Respuesta1Final.Add(Respuesta1[i]);
                }
                
                ViewBag.Answers9B = Respuesta1Final;
                return View("EncuestaP9B");
            }
            else
            if (result.Avance == 13)
            {
                Session["progreso"] = 93;
                Session["AvanceSeccion"] = 12;
                List<string> Respuesta1 = new List<string>();
                List<string> Respuesta1Final = new List<string>();
                Respuesta1Final.Add("ValorDefault");
                Respuesta1.Add("ValorDefault");
                foreach (ML.ResEmpleado obj in getRespuestas.Objects)
                {
                    //Trae todas las que consulta
                    Respuesta1.Add(obj.RespuestaEmpleado);
                }
                //IdPregunta 144 CSF


               

                for (int i = 173; i < 180; i++)
                {
                    Respuesta1Final.Add(Respuesta1[i]);
                }
                

                ViewBag.Answers10 = Respuesta1Final;
                return View("EncuestaP10");
            }
            if (result.Avance == 14)
            {
                Session["AvanceSeccion"] = 13;
                ML.CompanyCategoria companyCategoria = new ML.CompanyCategoria();

                var list = BL.CompanyCategoria.GetAll();
                var list2 = BL.Perfil.GetAll();

                companyCategoria.ListCompanyCategoria = new List<Object>();
                companyCategoria.ListPerfiles = new List<Object>();
                companyCategoria.ListCompanyCategoria = list.Objects;
                companyCategoria.ListPerfiles = list2.Objects;

                return View("EncuestaP11", companyCategoria);
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al saber en que parte iba\n" + result.ErrorMessage;
                Session["AvanceSeccion"] = 1;
                goto Inicio;
                // 1-8 => 87-94
                List<string> Respuesta1 = new List<string>();
                List<string> RespuestaFinal = new List<string>();
                Respuesta1.Add("ValorDefault");
                RespuestaFinal.Add("ValorDefault");
                foreach (ML.ResEmpleado obj in getRespuestas.Objects)
                {
                    Respuesta1.Add(obj.RespuestaEmpleado);
                }

                for (int i = 1; i < 17; i++)
                {
                    //1-16
                    RespuestaFinal.Add(Respuesta1[i]);
                }

                if (RespuestaFinal.Count < 16)
                {
                    ViewBag.Answers1 = Respuesta1;
                }
                else
                {
                    ViewBag.Answers1 = RespuestaFinal;
                }

                Inicio:
                return View("EncuestaP1");
            }
        }
        [HttpPost]
        public EmptyResult SaveAvance(List<ML.EmpleadoRespuesta> lista)
        {
            //Crear estatus
            ML.EstatusEncuesta estatusEncuesta = new ML.EstatusEncuesta();
            estatusEncuesta.Encuesta = new ML.Encuesta();
            estatusEncuesta.Encuesta.IdEncuesta = Convert.ToInt32(Session["EncuestaRealizar"]);

            estatusEncuesta.Empleado = new ML.Empleado();
            estatusEncuesta.Empleado.IdEmpleado = Convert.ToInt32(Session["EmpleadoEncuestado"]);

            var result = BL.Empleado.EstatusEncuestaAdd(estatusEncuesta);


            ML.Result resultado = new ML.Result();
            ViewBag.RespuestasP1 = Session["RespuestasP1"];
            ViewBag.RespuestasP2 = Session["RespuestasP2"];
            ViewBag.RespuestasP3 = Session["RespuestasP3"];
            ViewBag.RespuestasP4 = Session["RespuestasP4"];
            ViewBag.RespuestasP5 = Session["RespuestasP5"];
            ViewBag.RespuestasP6 = Session["RespuestasP6"];
            ViewBag.RespuestasP7 = Session["RespuestasP7"];
            ViewBag.RespuestasP8 = Session["RespuestasP8"];
            ViewBag.RespuestasP9 = Session["RespuestasP9"];
            ViewBag.RespuestasP9A = Session["RespuestasP9A"];
            ViewBag.RespuestasP9B = Session["RespuestasP9B"];
            ViewBag.RespuestasP10 = Session["RespuestasP10"];
            ViewBag.RespuestasP11 = Session["RespuestasP11"];

            if (ViewBag.RespuestasP1 != null) { lista.AddRange(ViewBag.RespuestasP1); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean1 = BL.Respuestas.CleanP1(IdUsuario); }
            if (ViewBag.RespuestasP2 != null) { lista.AddRange(ViewBag.RespuestasP2); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean2 = BL.Respuestas.CleanP2(IdUsuario); }
            if (ViewBag.RespuestasP3 != null) { lista.AddRange(ViewBag.RespuestasP3); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean3 = BL.Respuestas.CleanP3(IdUsuario); }
            if (ViewBag.RespuestasP4 != null) { lista.AddRange(ViewBag.RespuestasP4); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean4 = BL.Respuestas.CleanP4(IdUsuario); }
            if (ViewBag.RespuestasP5 != null) { lista.AddRange(ViewBag.RespuestasP5); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean5 = BL.Respuestas.CleanP5(IdUsuario); }
            if (ViewBag.RespuestasP6 != null) { lista.AddRange(ViewBag.RespuestasP6); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean6 = BL.Respuestas.CleanP6(IdUsuario); }
            if (ViewBag.RespuestasP7 != null) { lista.AddRange(ViewBag.RespuestasP7); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean7 = BL.Respuestas.CleanP7(IdUsuario); }
            if (ViewBag.RespuestasP8 != null) { lista.AddRange(ViewBag.RespuestasP8); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean8 = BL.Respuestas.CleanP8(IdUsuario); }
            if (ViewBag.RespuestasP9 != null) { lista.AddRange(ViewBag.RespuestasP9); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9 = BL.Respuestas.CleanP9(IdUsuario); }
            if (ViewBag.RespuestasP9A != null) { lista.AddRange(ViewBag.RespuestasP9A); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9A = BL.Respuestas.CleanP9A(IdUsuario); }
            if (ViewBag.RespuestasP9B != null) { lista.AddRange(ViewBag.RespuestasP9B); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9B = BL.Respuestas.CleanP9B(IdUsuario); }
            if (ViewBag.RespuestasP10 != null) { lista.AddRange(ViewBag.RespuestasP10); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean10 = BL.Respuestas.CleanP10(IdUsuario); }
            if (lista != null) { int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean2 = BL.Respuestas.CleanP11(IdUsuario); }

            resultado.answer = new List<ML.EmpleadoRespuesta>();
            resultado.answer = lista;

            foreach (ML.EmpleadoRespuesta usuarioRespuesta in resultado.answer)//Proceso de guardado
            {
                var results = BL.Respuestas.Add(usuarioRespuesta);
            }
            resultado.Correct = true;
            //Dejar que no retorne nada y asi solo muestro swal desde JS
            return new EmptyResult();
        }
        [HttpPost]
        public ActionResult Save(List<ML.EmpleadoRespuesta> lista)//Guarda todo
        {
            //lista son las respuestas del ultimo form "demograficos"
            ViewBag.RespuestasP1 = Session["RespuestasP1"];
            ViewBag.RespuestasP2 = Session["RespuestasP2"];
            ViewBag.RespuestasP3 = Session["RespuestasP3"];
            ViewBag.RespuestasP4 = Session["RespuestasP4"];
            ViewBag.RespuestasP5 = Session["RespuestasP5"];
            ViewBag.RespuestasP6 = Session["RespuestasP6"];
            ViewBag.RespuestasP7 = Session["RespuestasP7"];
            ViewBag.RespuestasP8 = Session["RespuestasP8"];
            ViewBag.RespuestasP9 = Session["RespuestasP9"];
            ViewBag.RespuestasP9A = Session["RespuestasP9A"];
            ViewBag.RespuestasP9B = Session["RespuestasP9B"];
            ViewBag.RespuestasP10 = Session["RespuestasP10"];

            if (ViewBag.RespuestasP1 != null) { lista.AddRange(ViewBag.RespuestasP1); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean1 = BL.Respuestas.CleanP1(IdUsuario); }
            if (ViewBag.RespuestasP2 != null) { lista.AddRange(ViewBag.RespuestasP2); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean2 = BL.Respuestas.CleanP2(IdUsuario); }
            if (ViewBag.RespuestasP3 != null) { lista.AddRange(ViewBag.RespuestasP3); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean3 = BL.Respuestas.CleanP3(IdUsuario); }
            if (ViewBag.RespuestasP4 != null) { lista.AddRange(ViewBag.RespuestasP4); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean4 = BL.Respuestas.CleanP4(IdUsuario); }
            if (ViewBag.RespuestasP5 != null) { lista.AddRange(ViewBag.RespuestasP5); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean5 = BL.Respuestas.CleanP5(IdUsuario); }
            if (ViewBag.RespuestasP6 != null) { lista.AddRange(ViewBag.RespuestasP6); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean6 = BL.Respuestas.CleanP6(IdUsuario); }
            if (ViewBag.RespuestasP7 != null) { lista.AddRange(ViewBag.RespuestasP7); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean7 = BL.Respuestas.CleanP7(IdUsuario); }
            if (ViewBag.RespuestasP8 != null) { lista.AddRange(ViewBag.RespuestasP8); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean8 = BL.Respuestas.CleanP8(IdUsuario); }
            if (ViewBag.RespuestasP9 != null) { lista.AddRange(ViewBag.RespuestasP9); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9 = BL.Respuestas.CleanP9(IdUsuario); }
            if (ViewBag.RespuestasP9A != null) { lista.AddRange(ViewBag.RespuestasP9A); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9A = BL.Respuestas.CleanP9A(IdUsuario); }
            if (ViewBag.RespuestasP9B != null) { lista.AddRange(ViewBag.RespuestasP9B); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9B = BL.Respuestas.CleanP9B(IdUsuario); }
            if (ViewBag.RespuestasP10 != null) { lista.AddRange(ViewBag.RespuestasP10); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean10 = BL.Respuestas.CleanP10(IdUsuario); }
            if (lista != null) { int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean10 = BL.Respuestas.CleanP11(IdUsuario); }
            //[DataSourceRequest] DataSourceRequest request
            ML.Result resultado = new ML.Result();
            resultado.answer = new List<ML.EmpleadoRespuesta>();
            resultado.answer = lista;

            foreach (ML.EmpleadoRespuesta usuarioRespuesta in resultado.answer)
            {
                var result = BL.Respuestas.Add(usuarioRespuesta);
            }
            resultado.Correct = true;
            return Json("success");
        }
        [HttpPost]
        public ActionResult SaveP1(List<ML.EmpleadoRespuesta> lista)
        {
            
            //Crear estatus
            ML.EstatusEncuesta estatusEncuesta = new ML.EstatusEncuesta();
            estatusEncuesta.Encuesta = new ML.Encuesta();
            estatusEncuesta.Encuesta.IdEncuesta = Convert.ToInt32(Session["EncuestaRealizar"]);

            estatusEncuesta.Empleado = new ML.Empleado();
            estatusEncuesta.Empleado.IdEmpleado = Convert.ToInt32(Session["EmpleadoEncuestado"]);

            var result = BL.Empleado.EstatusEncuestaAdd(estatusEncuesta);

            if (lista != null) { int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean1 = BL.Respuestas.CleanP1(IdUsuario); }

            //lista son las respuestas del ultimo form "demograficos"
            Session["RespuestasP1"] = lista;
            ML.Result resultado = new ML.Result();
            resultado.answer = new List<ML.EmpleadoRespuesta>();
            resultado.answer = lista;

            var Clean = BL.Respuestas.CleanP1(estatusEncuesta.Empleado.IdEmpleado);

            foreach (ML.EmpleadoRespuesta usuarioRespuesta in resultado.answer)
            {
                var results = BL.Respuestas.Add(usuarioRespuesta);
            }
            resultado.Correct = true;

            if (result.Correct == false)
            {
                return Json("error al guardar estatus");
            }
            else
            {
                return Json("success");
            }
        }
        [HttpPost]
        public ActionResult SaveP2(List<ML.EmpleadoRespuesta> lista)
        {
            //lista son las respuestas del ultimo form "demograficos"
            ViewBag.RespuestasP1 = Session["RespuestasP1"];
            //lista.AddRange(ViewBag.RespuestasP1);

            if (ViewBag.RespuestasP1 != null) { lista.AddRange(ViewBag.RespuestasP1); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean1 = BL.Respuestas.CleanP1(IdUsuario); }
            if (ViewBag.RespuestasP2 != null) { lista.AddRange(ViewBag.RespuestasP2); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean2 = BL.Respuestas.CleanP2(IdUsuario); }
            if (ViewBag.RespuestasP3 != null) { lista.AddRange(ViewBag.RespuestasP3); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean3 = BL.Respuestas.CleanP3(IdUsuario); }
            if (ViewBag.RespuestasP4 != null) { lista.AddRange(ViewBag.RespuestasP4); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean4 = BL.Respuestas.CleanP4(IdUsuario); }
            if (ViewBag.RespuestasP5 != null) { lista.AddRange(ViewBag.RespuestasP5); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean5 = BL.Respuestas.CleanP5(IdUsuario); }
            if (ViewBag.RespuestasP6 != null) { lista.AddRange(ViewBag.RespuestasP6); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean6 = BL.Respuestas.CleanP6(IdUsuario); }
            if (ViewBag.RespuestasP7 != null) { lista.AddRange(ViewBag.RespuestasP7); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean7 = BL.Respuestas.CleanP7(IdUsuario); }
            if (ViewBag.RespuestasP8 != null) { lista.AddRange(ViewBag.RespuestasP8); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean8 = BL.Respuestas.CleanP8(IdUsuario); }
            if (ViewBag.RespuestasP9 != null) { lista.AddRange(ViewBag.RespuestasP9); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9 = BL.Respuestas.CleanP9(IdUsuario); }
            if (ViewBag.RespuestasP9A != null) { lista.AddRange(ViewBag.RespuestasP9A); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9A = BL.Respuestas.CleanP9A(IdUsuario); }
            if (ViewBag.RespuestasP9B != null) { lista.AddRange(ViewBag.RespuestasP9B); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9B = BL.Respuestas.CleanP9B(IdUsuario); }
            if (ViewBag.RespuestasP10 != null) { lista.AddRange(ViewBag.RespuestasP10); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean10 = BL.Respuestas.CleanP10(IdUsuario); }
            if (lista != null) { int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean2 = BL.Respuestas.CleanP2(IdUsuario); }

            Session["RespuestasP2"] = lista;
            ML.Result resultado = new ML.Result();
            resultado.answer = new List<ML.EmpleadoRespuesta>();
            resultado.answer = lista;

            foreach (ML.EmpleadoRespuesta usuarioRespuesta in resultado.answer)
            {
                var result = BL.Respuestas.Add(usuarioRespuesta);
            }
            
            resultado.Correct = true;
            return Json("success");
        }
        [HttpPost]
        public ActionResult SaveP3(List<ML.EmpleadoRespuesta> lista)
        {
            //lista son las respuestas del ultimo form "demograficos"
            ViewBag.RespuestasP1 = Session["RespuestasP1"];
            ViewBag.RespuestasP2 = Session["RespuestasP2"];

            //lista.AddRange(ViewBag.RespuestasP1);
            //lista.AddRange(ViewBag.RespuestasP2);
            if (ViewBag.RespuestasP1 != null) { lista.AddRange(ViewBag.RespuestasP1); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean1 = BL.Respuestas.CleanP1(IdUsuario); }
            if (ViewBag.RespuestasP2 != null) { lista.AddRange(ViewBag.RespuestasP2); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean2 = BL.Respuestas.CleanP2(IdUsuario); }
            if (ViewBag.RespuestasP3 != null) { lista.AddRange(ViewBag.RespuestasP3); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean3 = BL.Respuestas.CleanP3(IdUsuario); }
            if (ViewBag.RespuestasP4 != null) { lista.AddRange(ViewBag.RespuestasP4); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean4 = BL.Respuestas.CleanP4(IdUsuario); }
            if (ViewBag.RespuestasP5 != null) { lista.AddRange(ViewBag.RespuestasP5); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean5 = BL.Respuestas.CleanP5(IdUsuario); }
            if (ViewBag.RespuestasP6 != null) { lista.AddRange(ViewBag.RespuestasP6); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean6 = BL.Respuestas.CleanP6(IdUsuario); }
            if (ViewBag.RespuestasP7 != null) { lista.AddRange(ViewBag.RespuestasP7); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean7 = BL.Respuestas.CleanP7(IdUsuario); }
            if (ViewBag.RespuestasP8 != null) { lista.AddRange(ViewBag.RespuestasP8); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean8 = BL.Respuestas.CleanP8(IdUsuario); }
            if (ViewBag.RespuestasP9 != null) { lista.AddRange(ViewBag.RespuestasP9); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9 = BL.Respuestas.CleanP9(IdUsuario); }
            if (ViewBag.RespuestasP9A != null) { lista.AddRange(ViewBag.RespuestasP9A); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9A = BL.Respuestas.CleanP9A(IdUsuario); }
            if (ViewBag.RespuestasP9B != null) { lista.AddRange(ViewBag.RespuestasP9B); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9B = BL.Respuestas.CleanP9B(IdUsuario); }
            if (ViewBag.RespuestasP10 != null) { lista.AddRange(ViewBag.RespuestasP10); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean10 = BL.Respuestas.CleanP10(IdUsuario); }
            if (lista != null) { int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean3 = BL.Respuestas.CleanP3(IdUsuario); }

            Session["RespuestasP3"] = lista;
            ML.Result resultado = new ML.Result();
            resultado.answer = new List<ML.EmpleadoRespuesta>();
            resultado.answer = lista;

            foreach (ML.EmpleadoRespuesta usuarioRespuesta in resultado.answer)
            {
                var result = BL.Respuestas.Add(usuarioRespuesta);
            }
            resultado.Correct = true;
            return Json("success");
        }
        [HttpPost]
        public ActionResult SaveP4(List<ML.EmpleadoRespuesta> lista)
        {
            //lista son las respuestas del ultimo form "demograficos"
            ViewBag.RespuestasP1 = Session["RespuestasP1"];
            ViewBag.RespuestasP2 = Session["RespuestasP2"];
            ViewBag.RespuestasP3 = Session["RespuestasP3"];

            //lista.AddRange(ViewBag.RespuestasP1);
            //lista.AddRange(ViewBag.RespuestasP2);
            //lista.AddRange(ViewBag.RespuestasP3);
            if (ViewBag.RespuestasP1 != null) { lista.AddRange(ViewBag.RespuestasP1); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean1 = BL.Respuestas.CleanP1(IdUsuario); }
            if (ViewBag.RespuestasP2 != null) { lista.AddRange(ViewBag.RespuestasP2); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean2 = BL.Respuestas.CleanP2(IdUsuario); }
            if (ViewBag.RespuestasP3 != null) { lista.AddRange(ViewBag.RespuestasP3); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean3 = BL.Respuestas.CleanP3(IdUsuario); }
            if (ViewBag.RespuestasP4 != null) { lista.AddRange(ViewBag.RespuestasP4); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean4 = BL.Respuestas.CleanP4(IdUsuario); }
            if (ViewBag.RespuestasP5 != null) { lista.AddRange(ViewBag.RespuestasP5); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean5 = BL.Respuestas.CleanP5(IdUsuario); }
            if (ViewBag.RespuestasP6 != null) { lista.AddRange(ViewBag.RespuestasP6); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean6 = BL.Respuestas.CleanP6(IdUsuario); }
            if (ViewBag.RespuestasP7 != null) { lista.AddRange(ViewBag.RespuestasP7); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean7 = BL.Respuestas.CleanP7(IdUsuario); }
            if (ViewBag.RespuestasP8 != null) { lista.AddRange(ViewBag.RespuestasP8); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean8 = BL.Respuestas.CleanP8(IdUsuario); }
            if (ViewBag.RespuestasP9 != null) { lista.AddRange(ViewBag.RespuestasP9); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9 = BL.Respuestas.CleanP9(IdUsuario); }
            if (ViewBag.RespuestasP9A != null) { lista.AddRange(ViewBag.RespuestasP9A); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9A = BL.Respuestas.CleanP9A(IdUsuario); }
            if (ViewBag.RespuestasP9B != null) { lista.AddRange(ViewBag.RespuestasP9B); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9B = BL.Respuestas.CleanP9B(IdUsuario); }
            if (ViewBag.RespuestasP10 != null) { lista.AddRange(ViewBag.RespuestasP10); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean10 = BL.Respuestas.CleanP10(IdUsuario); }
            if (lista != null) { int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean4 = BL.Respuestas.CleanP4(IdUsuario); }

            Session["RespuestasP4"] = lista;
            ML.Result resultado = new ML.Result();
            resultado.answer = new List<ML.EmpleadoRespuesta>();
            resultado.answer = lista;

            foreach (ML.EmpleadoRespuesta usuarioRespuesta in resultado.answer)
            {
                var result = BL.Respuestas.Add(usuarioRespuesta);
            }
            resultado.Correct = true;
            return Json("success");
        }
        [HttpPost]
        public ActionResult SaveP5(List<ML.EmpleadoRespuesta> lista)
        {
            //lista son las respuestas del ultimo form "demograficos"
            ViewBag.RespuestasP1 = Session["RespuestasP1"];
            ViewBag.RespuestasP2 = Session["RespuestasP2"];
            ViewBag.RespuestasP3 = Session["RespuestasP3"];
            ViewBag.RespuestasP4 = Session["RespuestasP4"];

            //lista.AddRange(ViewBag.RespuestasP1);
            //lista.AddRange(ViewBag.RespuestasP2);
            //lista.AddRange(ViewBag.RespuestasP3);
            //lista.AddRange(ViewBag.RespuestasP4);
            if (ViewBag.RespuestasP1 != null) { lista.AddRange(ViewBag.RespuestasP1); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean1 = BL.Respuestas.CleanP1(IdUsuario); }
            if (ViewBag.RespuestasP2 != null) { lista.AddRange(ViewBag.RespuestasP2); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean2 = BL.Respuestas.CleanP2(IdUsuario); }
            if (ViewBag.RespuestasP3 != null) { lista.AddRange(ViewBag.RespuestasP3); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean3 = BL.Respuestas.CleanP3(IdUsuario); }
            if (ViewBag.RespuestasP4 != null) { lista.AddRange(ViewBag.RespuestasP4); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean4 = BL.Respuestas.CleanP4(IdUsuario); }
            if (ViewBag.RespuestasP5 != null) { lista.AddRange(ViewBag.RespuestasP5); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean5 = BL.Respuestas.CleanP5(IdUsuario); }
            if (ViewBag.RespuestasP6 != null) { lista.AddRange(ViewBag.RespuestasP6); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean6 = BL.Respuestas.CleanP6(IdUsuario); }
            if (ViewBag.RespuestasP7 != null) { lista.AddRange(ViewBag.RespuestasP7); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean7 = BL.Respuestas.CleanP7(IdUsuario); }
            if (ViewBag.RespuestasP8 != null) { lista.AddRange(ViewBag.RespuestasP8); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean8 = BL.Respuestas.CleanP8(IdUsuario); }
            if (ViewBag.RespuestasP9 != null) { lista.AddRange(ViewBag.RespuestasP9); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9 = BL.Respuestas.CleanP9(IdUsuario); }
            if (ViewBag.RespuestasP9A != null) { lista.AddRange(ViewBag.RespuestasP9A); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9A = BL.Respuestas.CleanP9A(IdUsuario); }
            if (ViewBag.RespuestasP9B != null) { lista.AddRange(ViewBag.RespuestasP9B); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9B = BL.Respuestas.CleanP9B(IdUsuario); }
            if (ViewBag.RespuestasP10 != null) { lista.AddRange(ViewBag.RespuestasP10); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean10 = BL.Respuestas.CleanP10(IdUsuario); }
            if (lista != null) { int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean5 = BL.Respuestas.CleanP5(IdUsuario); }

            Session["RespuestasP5"] = lista;
            ML.Result resultado = new ML.Result();
            resultado.answer = new List<ML.EmpleadoRespuesta>();
            resultado.answer = lista;

            foreach (ML.EmpleadoRespuesta usuarioRespuesta in resultado.answer)
            {
                var result = BL.Respuestas.Add(usuarioRespuesta);
            }
            resultado.Correct = true;
            return Json("success");
        }
        [HttpPost]
        public ActionResult SaveP6(List<ML.EmpleadoRespuesta> lista)
        {
            //lista son las respuestas del ultimo form "demograficos"
            ViewBag.RespuestasP1 = Session["RespuestasP1"];
            ViewBag.RespuestasP2 = Session["RespuestasP2"];
            ViewBag.RespuestasP3 = Session["RespuestasP3"];
            ViewBag.RespuestasP4 = Session["RespuestasP4"];
            ViewBag.RespuestasP5 = Session["RespuestasP5"];

            //lista.AddRange(ViewBag.RespuestasP1);
            //lista.AddRange(ViewBag.RespuestasP2);
            //lista.AddRange(ViewBag.RespuestasP3);
            //lista.AddRange(ViewBag.RespuestasP4);
            //lista.AddRange(ViewBag.RespuestasP5);
            if (ViewBag.RespuestasP1 != null) { lista.AddRange(ViewBag.RespuestasP1); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean1 = BL.Respuestas.CleanP1(IdUsuario); }
            if (ViewBag.RespuestasP2 != null) { lista.AddRange(ViewBag.RespuestasP2); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean2 = BL.Respuestas.CleanP2(IdUsuario); }
            if (ViewBag.RespuestasP3 != null) { lista.AddRange(ViewBag.RespuestasP3); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean3 = BL.Respuestas.CleanP3(IdUsuario); }
            if (ViewBag.RespuestasP4 != null) { lista.AddRange(ViewBag.RespuestasP4); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean4 = BL.Respuestas.CleanP4(IdUsuario); }
            if (ViewBag.RespuestasP5 != null) { lista.AddRange(ViewBag.RespuestasP5); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean5 = BL.Respuestas.CleanP5(IdUsuario); }
            if (ViewBag.RespuestasP6 != null) { lista.AddRange(ViewBag.RespuestasP6); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean6 = BL.Respuestas.CleanP6(IdUsuario); }
            if (ViewBag.RespuestasP7 != null) { lista.AddRange(ViewBag.RespuestasP7); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean7 = BL.Respuestas.CleanP7(IdUsuario); }
            if (ViewBag.RespuestasP8 != null) { lista.AddRange(ViewBag.RespuestasP8); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean8 = BL.Respuestas.CleanP8(IdUsuario); }
            if (ViewBag.RespuestasP9 != null) { lista.AddRange(ViewBag.RespuestasP9); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9 = BL.Respuestas.CleanP9(IdUsuario); }
            if (ViewBag.RespuestasP9A != null) { lista.AddRange(ViewBag.RespuestasP9A); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9A = BL.Respuestas.CleanP9A(IdUsuario); }
            if (ViewBag.RespuestasP9B != null) { lista.AddRange(ViewBag.RespuestasP9B); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9B = BL.Respuestas.CleanP9B(IdUsuario); }
            if (ViewBag.RespuestasP10 != null) { lista.AddRange(ViewBag.RespuestasP10); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean10 = BL.Respuestas.CleanP10(IdUsuario); }
            if (lista != null) { int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean6 = BL.Respuestas.CleanP6(IdUsuario); }

            Session["RespuestasP6"] = lista;
            ML.Result resultado = new ML.Result();
            resultado.answer = new List<ML.EmpleadoRespuesta>();
            resultado.answer = lista;

            foreach (ML.EmpleadoRespuesta usuarioRespuesta in resultado.answer)
            {
                var result = BL.Respuestas.Add(usuarioRespuesta);
            }
            resultado.Correct = true;
            return Json("success");
        }
        [HttpPost]
        public ActionResult SaveP7(List<ML.EmpleadoRespuesta> lista)
        {
            //lista son las respuestas del ultimo form "demograficos"
            ViewBag.RespuestasP1 = Session["RespuestasP1"];
            ViewBag.RespuestasP2 = Session["RespuestasP2"];
            ViewBag.RespuestasP3 = Session["RespuestasP3"];
            ViewBag.RespuestasP4 = Session["RespuestasP4"];
            ViewBag.RespuestasP5 = Session["RespuestasP5"];
            ViewBag.RespuestasP6 = Session["RespuestasP6"];

            //lista.AddRange(ViewBag.RespuestasP1);
            //lista.AddRange(ViewBag.RespuestasP2);
            //lista.AddRange(ViewBag.RespuestasP3);
            //lista.AddRange(ViewBag.RespuestasP4);
            //lista.AddRange(ViewBag.RespuestasP5);
            //lista.AddRange(ViewBag.RespuestasP6);
            if (ViewBag.RespuestasP1 != null) { lista.AddRange(ViewBag.RespuestasP1); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean1 = BL.Respuestas.CleanP1(IdUsuario); }
            if (ViewBag.RespuestasP2 != null) { lista.AddRange(ViewBag.RespuestasP2); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean2 = BL.Respuestas.CleanP2(IdUsuario); }
            if (ViewBag.RespuestasP3 != null) { lista.AddRange(ViewBag.RespuestasP3); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean3 = BL.Respuestas.CleanP3(IdUsuario); }
            if (ViewBag.RespuestasP4 != null) { lista.AddRange(ViewBag.RespuestasP4); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean4 = BL.Respuestas.CleanP4(IdUsuario); }
            if (ViewBag.RespuestasP5 != null) { lista.AddRange(ViewBag.RespuestasP5); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean5 = BL.Respuestas.CleanP5(IdUsuario); }
            if (ViewBag.RespuestasP6 != null) { lista.AddRange(ViewBag.RespuestasP6); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean6 = BL.Respuestas.CleanP6(IdUsuario); }
            if (ViewBag.RespuestasP7 != null) { lista.AddRange(ViewBag.RespuestasP7); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean7 = BL.Respuestas.CleanP7(IdUsuario); }
            if (ViewBag.RespuestasP8 != null) { lista.AddRange(ViewBag.RespuestasP8); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean8 = BL.Respuestas.CleanP8(IdUsuario); }
            if (ViewBag.RespuestasP9 != null) { lista.AddRange(ViewBag.RespuestasP9); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9 = BL.Respuestas.CleanP9(IdUsuario); }
            if (ViewBag.RespuestasP9A != null) { lista.AddRange(ViewBag.RespuestasP9A); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9A = BL.Respuestas.CleanP9A(IdUsuario); }
            if (ViewBag.RespuestasP9B != null) { lista.AddRange(ViewBag.RespuestasP9B); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9B = BL.Respuestas.CleanP9B(IdUsuario); }
            if (ViewBag.RespuestasP10 != null) { lista.AddRange(ViewBag.RespuestasP10); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean10 = BL.Respuestas.CleanP10(IdUsuario); }
            if (lista != null) { int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean7 = BL.Respuestas.CleanP7(IdUsuario); }

            Session["RespuestasP7"] = lista;
            ML.Result resultado = new ML.Result();
            resultado.answer = new List<ML.EmpleadoRespuesta>();
            resultado.answer = lista;

            foreach (ML.EmpleadoRespuesta usuarioRespuesta in resultado.answer)
            {
                var result = BL.Respuestas.Add(usuarioRespuesta);
            }
            resultado.Correct = true;
            return Json("success");
        }
        [HttpPost]
        public ActionResult SaveP8(List<ML.EmpleadoRespuesta> lista)
        {
            //lista son las respuestas del ultimo form "demograficos"
            ViewBag.RespuestasP1 = Session["RespuestasP1"];
            ViewBag.RespuestasP2 = Session["RespuestasP2"];
            ViewBag.RespuestasP3 = Session["RespuestasP3"];
            ViewBag.RespuestasP4 = Session["RespuestasP4"];
            ViewBag.RespuestasP5 = Session["RespuestasP5"];
            ViewBag.RespuestasP6 = Session["RespuestasP6"];
            ViewBag.RespuestasP7 = Session["RespuestasP7"];

            //lista.AddRange(ViewBag.RespuestasP1);
            //lista.AddRange(ViewBag.RespuestasP2);
            //lista.AddRange(ViewBag.RespuestasP3);
            //lista.AddRange(ViewBag.RespuestasP4);
            //lista.AddRange(ViewBag.RespuestasP5);
            //lista.AddRange(ViewBag.RespuestasP6);
            //lista.AddRange(ViewBag.RespuestasP7);
            if (ViewBag.RespuestasP1 != null) { lista.AddRange(ViewBag.RespuestasP1); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean1 = BL.Respuestas.CleanP1(IdUsuario); }
            if (ViewBag.RespuestasP2 != null) { lista.AddRange(ViewBag.RespuestasP2); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean2 = BL.Respuestas.CleanP2(IdUsuario); }
            if (ViewBag.RespuestasP3 != null) { lista.AddRange(ViewBag.RespuestasP3); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean3 = BL.Respuestas.CleanP3(IdUsuario); }
            if (ViewBag.RespuestasP4 != null) { lista.AddRange(ViewBag.RespuestasP4); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean4 = BL.Respuestas.CleanP4(IdUsuario); }
            if (ViewBag.RespuestasP5 != null) { lista.AddRange(ViewBag.RespuestasP5); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean5 = BL.Respuestas.CleanP5(IdUsuario); }
            if (ViewBag.RespuestasP6 != null) { lista.AddRange(ViewBag.RespuestasP6); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean6 = BL.Respuestas.CleanP6(IdUsuario); }
            if (ViewBag.RespuestasP7 != null) { lista.AddRange(ViewBag.RespuestasP7); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean7 = BL.Respuestas.CleanP7(IdUsuario); }
            if (ViewBag.RespuestasP8 != null) { lista.AddRange(ViewBag.RespuestasP8); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean8 = BL.Respuestas.CleanP8(IdUsuario); }
            if (ViewBag.RespuestasP9 != null) { lista.AddRange(ViewBag.RespuestasP9); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9 = BL.Respuestas.CleanP9(IdUsuario); }
            if (ViewBag.RespuestasP9A != null) { lista.AddRange(ViewBag.RespuestasP9A); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9A = BL.Respuestas.CleanP9A(IdUsuario); }
            if (ViewBag.RespuestasP9B != null) { lista.AddRange(ViewBag.RespuestasP9B); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9B = BL.Respuestas.CleanP9B(IdUsuario); }
            if (ViewBag.RespuestasP10 != null) { lista.AddRange(ViewBag.RespuestasP10); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean10 = BL.Respuestas.CleanP10(IdUsuario); }
            if (lista != null) { int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean8 = BL.Respuestas.CleanP8(IdUsuario); }

            Session["RespuestasP8"] = lista;
            ML.Result resultado = new ML.Result();
            resultado.answer = new List<ML.EmpleadoRespuesta>();
            resultado.answer = lista;

            foreach (ML.EmpleadoRespuesta usuarioRespuesta in resultado.answer)
            {
                var result = BL.Respuestas.Add(usuarioRespuesta);
            }
            resultado.Correct = true;
            return Json("success");
        }
        [HttpPost]
        public ActionResult SaveP9(List<ML.EmpleadoRespuesta> lista)
        {
            //lista son las respuestas del ultimo form "demograficos"
            ViewBag.RespuestasP1 = Session["RespuestasP1"];
            ViewBag.RespuestasP2 = Session["RespuestasP2"];
            ViewBag.RespuestasP3 = Session["RespuestasP3"];
            ViewBag.RespuestasP4 = Session["RespuestasP4"];
            ViewBag.RespuestasP5 = Session["RespuestasP5"];
            ViewBag.RespuestasP6 = Session["RespuestasP6"];
            ViewBag.RespuestasP7 = Session["RespuestasP7"];
            ViewBag.RespuestasP8 = Session["RespuestasP8"];

            //lista.AddRange(ViewBag.RespuestasP1);
            //lista.AddRange(ViewBag.RespuestasP2);
            //lista.AddRange(ViewBag.RespuestasP3);
            //lista.AddRange(ViewBag.RespuestasP4);
            //lista.AddRange(ViewBag.RespuestasP5);
            //lista.AddRange(ViewBag.RespuestasP6);
            //lista.AddRange(ViewBag.RespuestasP7);
            //lista.AddRange(ViewBag.RespuestasP8);
            if (ViewBag.RespuestasP1 != null) { lista.AddRange(ViewBag.RespuestasP1); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean1 = BL.Respuestas.CleanP1(IdUsuario); }
            if (ViewBag.RespuestasP2 != null) { lista.AddRange(ViewBag.RespuestasP2); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean2 = BL.Respuestas.CleanP2(IdUsuario); }
            if (ViewBag.RespuestasP3 != null) { lista.AddRange(ViewBag.RespuestasP3); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean3 = BL.Respuestas.CleanP3(IdUsuario); }
            if (ViewBag.RespuestasP4 != null) { lista.AddRange(ViewBag.RespuestasP4); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean4 = BL.Respuestas.CleanP4(IdUsuario); }
            if (ViewBag.RespuestasP5 != null) { lista.AddRange(ViewBag.RespuestasP5); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean5 = BL.Respuestas.CleanP5(IdUsuario); }
            if (ViewBag.RespuestasP6 != null) { lista.AddRange(ViewBag.RespuestasP6); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean6 = BL.Respuestas.CleanP6(IdUsuario); }
            if (ViewBag.RespuestasP7 != null) { lista.AddRange(ViewBag.RespuestasP7); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean7 = BL.Respuestas.CleanP7(IdUsuario); }
            if (ViewBag.RespuestasP8 != null) { lista.AddRange(ViewBag.RespuestasP8); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean8 = BL.Respuestas.CleanP8(IdUsuario); }
            if (ViewBag.RespuestasP9 != null) { lista.AddRange(ViewBag.RespuestasP9); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9 = BL.Respuestas.CleanP9(IdUsuario); }
            if (ViewBag.RespuestasP9A != null) { lista.AddRange(ViewBag.RespuestasP9A); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9A = BL.Respuestas.CleanP9A(IdUsuario); }
            if (ViewBag.RespuestasP9B != null) { lista.AddRange(ViewBag.RespuestasP9B); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9B = BL.Respuestas.CleanP9B(IdUsuario); }
            if (ViewBag.RespuestasP10 != null) { lista.AddRange(ViewBag.RespuestasP10); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean10 = BL.Respuestas.CleanP10(IdUsuario); }
            if (lista != null) { int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9 = BL.Respuestas.CleanP9(IdUsuario); }

            Session["RespuestasP9"] = lista;
            ML.Result resultado = new ML.Result();
            resultado.answer = new List<ML.EmpleadoRespuesta>();
            resultado.answer = lista;

            foreach (ML.EmpleadoRespuesta usuarioRespuesta in resultado.answer)
            {
                var result = BL.Respuestas.Add(usuarioRespuesta);
            }
            resultado.Correct = true;
            return Json("success");
        }
        [HttpPost]
        public ActionResult SaveP9A(List<ML.EmpleadoRespuesta> lista)
        {
            //lista son las respuestas del ultimo form "demograficos"
            ViewBag.RespuestasP1 = Session["RespuestasP1"];
            ViewBag.RespuestasP2 = Session["RespuestasP2"];
            ViewBag.RespuestasP3 = Session["RespuestasP3"];
            ViewBag.RespuestasP4 = Session["RespuestasP4"];
            ViewBag.RespuestasP5 = Session["RespuestasP5"];
            ViewBag.RespuestasP6 = Session["RespuestasP6"];
            ViewBag.RespuestasP7 = Session["RespuestasP7"];
            ViewBag.RespuestasP8 = Session["RespuestasP8"];
            ViewBag.RespuestasP9 = Session["RespuestasP9"];

            //lista.AddRange(ViewBag.RespuestasP1);
            //lista.AddRange(ViewBag.RespuestasP2);
            //lista.AddRange(ViewBag.RespuestasP3);
            //lista.AddRange(ViewBag.RespuestasP4);
            //lista.AddRange(ViewBag.RespuestasP5);
            //lista.AddRange(ViewBag.RespuestasP6);
            //lista.AddRange(ViewBag.RespuestasP7);
            //lista.AddRange(ViewBag.RespuestasP8);
            //lista.AddRange(ViewBag.RespuestasP9);
            if (ViewBag.RespuestasP1 != null) { lista.AddRange(ViewBag.RespuestasP1); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean1 = BL.Respuestas.CleanP1(IdUsuario); }
            if (ViewBag.RespuestasP2 != null) { lista.AddRange(ViewBag.RespuestasP2); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean2 = BL.Respuestas.CleanP2(IdUsuario); }
            if (ViewBag.RespuestasP3 != null) { lista.AddRange(ViewBag.RespuestasP3); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean3 = BL.Respuestas.CleanP3(IdUsuario); }
            if (ViewBag.RespuestasP4 != null) { lista.AddRange(ViewBag.RespuestasP4); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean4 = BL.Respuestas.CleanP4(IdUsuario); }
            if (ViewBag.RespuestasP5 != null) { lista.AddRange(ViewBag.RespuestasP5); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean5 = BL.Respuestas.CleanP5(IdUsuario); }
            if (ViewBag.RespuestasP6 != null) { lista.AddRange(ViewBag.RespuestasP6); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean6 = BL.Respuestas.CleanP6(IdUsuario); }
            if (ViewBag.RespuestasP7 != null) { lista.AddRange(ViewBag.RespuestasP7); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean7 = BL.Respuestas.CleanP7(IdUsuario); }
            if (ViewBag.RespuestasP8 != null) { lista.AddRange(ViewBag.RespuestasP8); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean8 = BL.Respuestas.CleanP8(IdUsuario); }
            if (ViewBag.RespuestasP9 != null) { lista.AddRange(ViewBag.RespuestasP9); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9 = BL.Respuestas.CleanP9(IdUsuario); }
            if (ViewBag.RespuestasP9A != null) { lista.AddRange(ViewBag.RespuestasP9A); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9A = BL.Respuestas.CleanP9A(IdUsuario); }
            if (ViewBag.RespuestasP9B != null) { lista.AddRange(ViewBag.RespuestasP9B); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9B = BL.Respuestas.CleanP9B(IdUsuario); }
            if (ViewBag.RespuestasP10 != null) { lista.AddRange(ViewBag.RespuestasP10); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean10 = BL.Respuestas.CleanP10(IdUsuario); }
            if (lista != null) { int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9A = BL.Respuestas.CleanP9A(IdUsuario); }

            Session["RespuestasP9A"] = lista;
            ML.Result resultado = new ML.Result();
            resultado.answer = new List<ML.EmpleadoRespuesta>();
            resultado.answer = lista;

            foreach (ML.EmpleadoRespuesta usuarioRespuesta in resultado.answer)
            {
                var result = BL.Respuestas.Add(usuarioRespuesta);
            }
            resultado.Correct = true;
            return Json("success");
        }
        [HttpPost]
        public ActionResult SaveP9B(List<ML.EmpleadoRespuesta> lista)
        {
            //lista son las respuestas del ultimo form "demograficos"
            ViewBag.RespuestasP1 = Session["RespuestasP1"];
            ViewBag.RespuestasP2 = Session["RespuestasP2"];
            ViewBag.RespuestasP3 = Session["RespuestasP3"];
            ViewBag.RespuestasP4 = Session["RespuestasP4"];
            ViewBag.RespuestasP5 = Session["RespuestasP5"];
            ViewBag.RespuestasP6 = Session["RespuestasP6"];
            ViewBag.RespuestasP7 = Session["RespuestasP7"];
            ViewBag.RespuestasP8 = Session["RespuestasP8"];
            ViewBag.RespuestasP9 = Session["RespuestasP9"];
            ViewBag.RespuestasP9A = Session["RespuestasP9A"];


            //lista.AddRange(ViewBag.RespuestasP1);
            //lista.AddRange(ViewBag.RespuestasP2);
            //lista.AddRange(ViewBag.RespuestasP3);
            //lista.AddRange(ViewBag.RespuestasP4);
            //lista.AddRange(ViewBag.RespuestasP5);
            //lista.AddRange(ViewBag.RespuestasP6);
            //lista.AddRange(ViewBag.RespuestasP7);
            //lista.AddRange(ViewBag.RespuestasP8);
            //lista.AddRange(ViewBag.RespuestasP9);
            //lista.AddRange(ViewBag.RespuestasP9A);
            if (ViewBag.RespuestasP1 != null) { lista.AddRange(ViewBag.RespuestasP1); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean1 = BL.Respuestas.CleanP1(IdUsuario); }
            if (ViewBag.RespuestasP2 != null) { lista.AddRange(ViewBag.RespuestasP2); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean2 = BL.Respuestas.CleanP2(IdUsuario); }
            if (ViewBag.RespuestasP3 != null) { lista.AddRange(ViewBag.RespuestasP3); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean3 = BL.Respuestas.CleanP3(IdUsuario); }
            if (ViewBag.RespuestasP4 != null) { lista.AddRange(ViewBag.RespuestasP4); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean4 = BL.Respuestas.CleanP4(IdUsuario); }
            if (ViewBag.RespuestasP5 != null) { lista.AddRange(ViewBag.RespuestasP5); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean5 = BL.Respuestas.CleanP5(IdUsuario); }
            if (ViewBag.RespuestasP6 != null) { lista.AddRange(ViewBag.RespuestasP6); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean6 = BL.Respuestas.CleanP6(IdUsuario); }
            if (ViewBag.RespuestasP7 != null) { lista.AddRange(ViewBag.RespuestasP7); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean7 = BL.Respuestas.CleanP7(IdUsuario); }
            if (ViewBag.RespuestasP8 != null) { lista.AddRange(ViewBag.RespuestasP8); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean8 = BL.Respuestas.CleanP8(IdUsuario); }
            if (ViewBag.RespuestasP9 != null) { lista.AddRange(ViewBag.RespuestasP9); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9 = BL.Respuestas.CleanP9(IdUsuario); }
            if (ViewBag.RespuestasP9A != null) { lista.AddRange(ViewBag.RespuestasP9A); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9A = BL.Respuestas.CleanP9A(IdUsuario); }
            if (ViewBag.RespuestasP9B != null) { lista.AddRange(ViewBag.RespuestasP9B); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9B = BL.Respuestas.CleanP9B(IdUsuario); }
            if (ViewBag.RespuestasP10 != null) { lista.AddRange(ViewBag.RespuestasP10); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean10 = BL.Respuestas.CleanP10(IdUsuario); }
            if (lista != null) { int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean1 = BL.Respuestas.CleanP9B(IdUsuario); }

            Session["RespuestasP9B"] = lista;
            ML.Result resultado = new ML.Result();
            resultado.answer = new List<ML.EmpleadoRespuesta>();
            resultado.answer = lista;

            foreach (ML.EmpleadoRespuesta usuarioRespuesta in resultado.answer)
            {
                var result = BL.Respuestas.Add(usuarioRespuesta);
            }
            resultado.Correct = true;
            return Json("success");
        }
        [HttpPost]
        public ActionResult SaveP10(List<ML.EmpleadoRespuesta> lista)
        {
            //lista son las respuestas del ultimo form "demograficos"
            ViewBag.RespuestasP1 = Session["RespuestasP1"];
            ViewBag.RespuestasP2 = Session["RespuestasP2"];
            ViewBag.RespuestasP3 = Session["RespuestasP3"];
            ViewBag.RespuestasP4 = Session["RespuestasP4"];
            ViewBag.RespuestasP5 = Session["RespuestasP5"];
            ViewBag.RespuestasP6 = Session["RespuestasP6"];
            ViewBag.RespuestasP7 = Session["RespuestasP7"];
            ViewBag.RespuestasP8 = Session["RespuestasP8"];
            ViewBag.RespuestasP9 = Session["RespuestasP9"];
            ViewBag.RespuestasP9A = Session["RespuestasP9A"];
            ViewBag.RespuestasP9B = Session["RespuestasP9B"];

            //lista.AddRange(ViewBag.RespuestasP1);
            //lista.AddRange(ViewBag.RespuestasP2);
            //lista.AddRange(ViewBag.RespuestasP3);
            //lista.AddRange(ViewBag.RespuestasP4);
            //lista.AddRange(ViewBag.RespuestasP5);
            //lista.AddRange(ViewBag.RespuestasP6);
            //lista.AddRange(ViewBag.RespuestasP7);
            //lista.AddRange(ViewBag.RespuestasP8);
            //lista.AddRange(ViewBag.RespuestasP9);
            //lista.AddRange(ViewBag.RespuestasP9A);
            //lista.AddRange(ViewBag.RespuestasP9B);
            if (ViewBag.RespuestasP1 != null) { lista.AddRange(ViewBag.RespuestasP1); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean1 = BL.Respuestas.CleanP1(IdUsuario); }
            if (ViewBag.RespuestasP2 != null) { lista.AddRange(ViewBag.RespuestasP2); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean2 = BL.Respuestas.CleanP2(IdUsuario); }
            if (ViewBag.RespuestasP3 != null) { lista.AddRange(ViewBag.RespuestasP3); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean3 = BL.Respuestas.CleanP3(IdUsuario); }
            if (ViewBag.RespuestasP4 != null) { lista.AddRange(ViewBag.RespuestasP4); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean4 = BL.Respuestas.CleanP4(IdUsuario); }
            if (ViewBag.RespuestasP5 != null) { lista.AddRange(ViewBag.RespuestasP5); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean5 = BL.Respuestas.CleanP5(IdUsuario); }
            if (ViewBag.RespuestasP6 != null) { lista.AddRange(ViewBag.RespuestasP6); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean6 = BL.Respuestas.CleanP6(IdUsuario); }
            if (ViewBag.RespuestasP7 != null) { lista.AddRange(ViewBag.RespuestasP7); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean7 = BL.Respuestas.CleanP7(IdUsuario); }
            if (ViewBag.RespuestasP8 != null) { lista.AddRange(ViewBag.RespuestasP8); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean8 = BL.Respuestas.CleanP8(IdUsuario); }
            if (ViewBag.RespuestasP9 != null) { lista.AddRange(ViewBag.RespuestasP9); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9 = BL.Respuestas.CleanP9(IdUsuario); }
            if (ViewBag.RespuestasP9A != null) { lista.AddRange(ViewBag.RespuestasP9A); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9A = BL.Respuestas.CleanP9A(IdUsuario); }
            if (ViewBag.RespuestasP9B != null) { lista.AddRange(ViewBag.RespuestasP9B); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9B = BL.Respuestas.CleanP9B(IdUsuario); }
            if (ViewBag.RespuestasP10 != null) { lista.AddRange(ViewBag.RespuestasP10); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean10 = BL.Respuestas.CleanP10(IdUsuario); }
            if (lista != null) { int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean10 = BL.Respuestas.CleanP10(IdUsuario); }

            Session["RespuestasP10"] = lista;
            ML.Result resultado = new ML.Result();
            resultado.answer = new List<ML.EmpleadoRespuesta>();
            resultado.answer = lista;

            foreach (ML.EmpleadoRespuesta usuarioRespuesta in resultado.answer)
            {
                var result = BL.Respuestas.Add(usuarioRespuesta);
            }
            resultado.Correct = true;
            return Json("success");
        }

        /************************************************/
        [HttpPost]
        public ActionResult AddRespuestas(List<ML.EmpleadoRespuesta> lista)
        {
            ML.Result resultado = new ML.Result();
            resultado.answer = new List<ML.EmpleadoRespuesta>();
            resultado.answer = lista;

            foreach (ML.EmpleadoRespuesta usuarioRespuesta in resultado.answer)
            {
                var result = BL.Respuestas.Add(usuarioRespuesta);
            }
            resultado.Correct = true;
            return View("Final");
        }

        

        [HttpGet]
        public ActionResult EncuestaP1()
        {
            ML.EmpleadoRespuesta empRes = new ML.EmpleadoRespuesta();
            empRes.Empleado = new ML.Empleado();
            empRes.Empleado.IdEmpleado = Convert.ToInt32(Session["EmpleadoEncuestado"]);

            //Llamar a todas las respuestas y estando en session poner la comparartiva en razor
            GetRespuestasByIdEmpleado(empRes.Empleado.IdEmpleado);
            int TotalRes = Convert.ToInt32(Session["progreso"]);



            var respBD = BL.Respuestas.GetResP1BD(empRes);

            if (respBD.Objects.Count == 0 || respBD.Objects.Count == 1)//Si en BD no tengo nada
            {

                //si no se ha contestado nada hara lo siguiente; si ya se contesto algo se va a rellenar el form
                ViewBag.RespuestasP1 = Session["RespuestasP1"];
                var results = Session["RespuestasP1"];
                List<string> Respuesta1 = new List<string>();
                Respuesta1.Add("ValorDefault1");
                if (ViewBag.RespuestasP1 == null)
                {

                    //Hago la accion de guardar en la tabla EstatusEncuesta(Estatus, IdEncuesta, IdEmpleado)
                    ML.EstatusEncuesta estatusEncuesta = new ML.EstatusEncuesta();
                    estatusEncuesta.Encuesta = new ML.Encuesta();
                    estatusEncuesta.Encuesta.IdEncuesta = Convert.ToInt32(Session["EncuestaRealizar"]);

                    estatusEncuesta.Empleado = new ML.Empleado();
                    estatusEncuesta.Empleado.IdEmpleado = Convert.ToInt32(Session["EmpleadoEncuestado"]);

                    var result = BL.Empleado.EstatusEncuestaAdd(estatusEncuesta);

                    if (result.Correct == true)
                    {
                        return View("EncuestaP1");
                    }
                    else
                    {
                        ViewBag.Message = "Fallo guardar la relacion Estatus-Encuesta";
                        return View("Modal");
                    }
                }
                else
                {
                    foreach (ML.EmpleadoRespuesta obj in ViewBag.RespuestasP1)
                    {
                        Respuesta1.Add(obj.RespuestaEmpleado);
                    }
                    Session["Resp"] = Respuesta1;
                    ViewBag.Answers1 = Respuesta1;
                    return View("EncuestaP1");
                }
            }
            else
            {
                List<string> Respuesta1 = new List<string>();
                Respuesta1.Add("ValorDefault1");
                //Si hay algo en BD
                ViewBag.RespuestasP1 = respBD.Objects;
                foreach (ML.EmpleadoRespuesta obj in ViewBag.RespuestasP1)
                {
                    Respuesta1.Add(obj.RespuestaEmpleado);
                }
                Session["Resp"] = Respuesta1;
                ViewBag.Answers1 = Respuesta1;
                return View("EncuestaP1");
            }


        }
        [HttpPost]
        public ActionResult EncuestaP1(List<ML.EmpleadoRespuesta> lista)
        {
            

            ML.EstatusEncuesta estatusEncuesta = new ML.EstatusEncuesta();
            estatusEncuesta.Encuesta = new ML.Encuesta();
            estatusEncuesta.Encuesta.IdEncuesta = Convert.ToInt32(Session["EncuestaRealizar"]);

            estatusEncuesta.Empleado = new ML.Empleado();
            estatusEncuesta.Empleado.IdEmpleado = Convert.ToInt32(Session["EmpleadoEncuestado"]);

            var result = BL.Empleado.EstatusEncuestaAdd(estatusEncuesta);

            Session["RespuestasP1"] = lista;

            ViewBag.RespuestasP1 = Session["RespuestasP1"];
            List<string> Respuesta1 = new List<string>();
            Respuesta1.Add("ValorDefault");

            if (result.Correct == false)
            {
                return Json("Ocurrio un error al crear el estatus de la encuesta!\n" + result.ErrorMessage);
            }
            else
            if (result.Correct == true && ViewBag.RespuestasP1 == null)
            {
                Session["AvanceSeccion"] = 2;
                return Json("success");
            }
            else
            {
                foreach (ML.EmpleadoRespuesta obj in ViewBag.RespuestasP1)
                {
                    Respuesta1.Add(obj.RespuestaEmpleado);
                }
                Session["AvanceSeccion"] = 2;
                ViewBag.Answers1 = Respuesta1;
                return Json("success");
            }
        }
        [HttpGet]
        public ActionResult EncuestaP2Get()
          {
            
            ML.EmpleadoRespuesta empRes = new ML.EmpleadoRespuesta();
            empRes.Empleado = new ML.Empleado();
            empRes.Empleado.IdEmpleado = Convert.ToInt32(Session["EmpleadoEncuestado"]);
            var respBD = BL.Respuestas.GetResP2BD(empRes);

            GetRespuestasByIdEmpleado(empRes.Empleado.IdEmpleado);
            int TotalRes = Convert.ToInt32(Session["progreso"]);



            if (respBD.Objects.Count == 0 || respBD.Objects.Count == 1)
            {
                ViewBag.RespuestasP2 = Session["RespuestasP2"];
                List<string> Respuesta2 = new List<string>();
                Respuesta2.Add("ValorDefault");
                if (ViewBag.RespuestasP2 == null)
                {
                    return View("EncuestaP2");
                }
                else
                {
                    foreach (ML.EmpleadoRespuesta obj in ViewBag.RespuestasP2)
                    {
                        Respuesta2.Add(obj.RespuestaEmpleado);
                    }
                    ViewBag.Answers2 = Respuesta2;
                    return View("EncuestaP2");
                }
            }
            else
            {
                List<string> Respuesta1 = new List<string>();
                Respuesta1.Add("ValorDefault1");
                //Si hay algo en BD
                ViewBag.RespuestasP1 = respBD.Objects;
                foreach (ML.EmpleadoRespuesta obj in ViewBag.RespuestasP1)
                {
                    Respuesta1.Add(obj.RespuestaEmpleado);
                }
                Session["Resp"] = Respuesta1;
                ViewBag.Answers2 = Respuesta1;
                return View("EncuestaP2");
            }

            

        }
        [HttpPost]
        public ActionResult EncuestaP2(List<ML.EmpleadoRespuesta> lista)
        {
            
            Session["RespuestasP2"] = lista;

            ViewBag.RespuestasP2 = Session["RespuestasP2"];
            List<string> Respuesta2 = new List<string>();
            Respuesta2.Add("ValorDefault");

            if (ViewBag.RespuestasP2 == null)
            {
                return Json("success");
            }
            else
            {
                foreach (ML.EmpleadoRespuesta obj in ViewBag.RespuestasP2)
                {
                    Respuesta2.Add(obj.RespuestaEmpleado);
                }
                Session["AvanceSeccion"] = 3;
                ViewBag.Answers2 = Respuesta2;
                return Json("success");
            }
        }
        [HttpGet]
        public ActionResult EncuestaP3Get()
        {
            ML.EmpleadoRespuesta empRes = new ML.EmpleadoRespuesta();
            empRes.Empleado = new ML.Empleado();
            empRes.Empleado.IdEmpleado = Convert.ToInt32(Session["EmpleadoEncuestado"]);
            var respBD = BL.Respuestas.GetResP3BD(empRes);

            GetRespuestasByIdEmpleado(empRes.Empleado.IdEmpleado);
            int TotalRes = Convert.ToInt32(Session["progreso"]);


            if (respBD.Objects.Count == 0 || respBD.Objects.Count == 1)
            {
                ViewBag.RespuestasP3 = Session["RespuestasP3"];
                List<string> Respuesta3 = new List<string>();
                Respuesta3.Add("ValorDefault");

                if (ViewBag.RespuestasP3 == null)
                {
                    return View("EncuestaP3");
                }
                else
                {
                    foreach (ML.EmpleadoRespuesta obj in ViewBag.RespuestasP3)
                    {
                        Respuesta3.Add(obj.RespuestaEmpleado);
                    }
                    ViewBag.Answers3 = Respuesta3;
                    return View("EncuestaP3");
                }
            }
            else
            {
                List<string> Respuesta1 = new List<string>();
                Respuesta1.Add("ValorDefault1");
                //Si hay algo en BD
                ViewBag.RespuestasP3 = respBD.Objects;
                foreach (ML.EmpleadoRespuesta obj in ViewBag.RespuestasP3)
                {
                    Respuesta1.Add(obj.RespuestaEmpleado);
                }
                Session["Resp"] = Respuesta1;
                ViewBag.Answers3 = Respuesta1;
                return View("EncuestaP3");
            }

            
        }
        [HttpPost]
        public ActionResult EncuestaP3(List<ML.EmpleadoRespuesta> lista)
        {
            Session["RespuestasP3"] = lista;
            ViewBag.RespuestasP3 = Session["RespuestasP3"];
            List<string> Respuesta3 = new List<string>();
            Respuesta3.Add("ValorDefault");

            if (ViewBag.RespuestasP3 == null)
            {
                return Json("success");
            }
            else
            {
                foreach (ML.EmpleadoRespuesta obj in ViewBag.RespuestasP3)
                {
                    Respuesta3.Add(obj.RespuestaEmpleado);
                }
                ViewBag.Answers3 = Respuesta3;
                Session["AvanceSeccion"] = 4;
                return Json("success");
            }
        }
        [HttpGet]
        public ActionResult EncuestaP4Get()
        {
            ML.EmpleadoRespuesta empRes = new ML.EmpleadoRespuesta();
            empRes.Empleado = new ML.Empleado();
            empRes.Empleado.IdEmpleado = Convert.ToInt32(Session["EmpleadoEncuestado"]);
            var respBD = BL.Respuestas.GetResP4BD(empRes);

            GetRespuestasByIdEmpleado(empRes.Empleado.IdEmpleado);
            int TotalRes = Convert.ToInt32(Session["progreso"]);



            if (respBD.Objects.Count == 0 || respBD.Objects.Count == 1)
            {
                ViewBag.RespuestasP4 = Session["RespuestasP4"];//Session es un modelo completo
                List<string> Respuesta4 = new List<string>();
                Respuesta4.Add("ValorDefault");

                if (ViewBag.RespuestasP4 == null)
                {
                    return View("EncuestaP4");
                }
                else
                {
                    foreach (ML.EmpleadoRespuesta obj in ViewBag.RespuestasP4)
                    {
                        Respuesta4.Add(obj.RespuestaEmpleado);
                    }
                    ViewBag.Answers4 = Respuesta4;
                    return View("EncuestaP4");
                }
            }
            else
            {
                List<string> Respuesta1 = new List<string>();
                Respuesta1.Add("ValorDefault1");
                //Si hay algo en BD
                ViewBag.RespuestasP3 = respBD.Objects;
                foreach (ML.EmpleadoRespuesta obj in ViewBag.RespuestasP3)
                {
                    Respuesta1.Add(obj.RespuestaEmpleado);
                }
                Session["Resp"] = Respuesta1;
                ViewBag.Answers4 = Respuesta1;
                return View("EncuestaP4");
            }

            
        }
        [HttpPost]
        public ActionResult EncuestaP4(List<ML.EmpleadoRespuesta> lista)
        {
            Session["RespuestasP4"] = lista;
            ViewBag.RespuestasP4 = Session["RespuestasP4"];
            List<string> Respuesta4 = new List<string>();
            Respuesta4.Add("ValorDefault");

            if (ViewBag.RespuestasP4 == null)
            {
                return Json("success");
            }
            else
            {
                foreach (ML.EmpleadoRespuesta obj in ViewBag.RespuestasP4)
                {
                    Respuesta4.Add(obj.RespuestaEmpleado);
                }
                ViewBag.Answers4 = Respuesta4;
                Session["AvanceSeccion"] = 5;
                return Json("success");
            }
        }
        [HttpGet]
        public ActionResult EncuestaP5Get()
        {
            ML.EmpleadoRespuesta empRes = new ML.EmpleadoRespuesta();
            empRes.Empleado = new ML.Empleado();
            empRes.Empleado.IdEmpleado = Convert.ToInt32(Session["EmpleadoEncuestado"]);
            var respBD = BL.Respuestas.GetResP5BD(empRes);

            GetRespuestasByIdEmpleado(empRes.Empleado.IdEmpleado);
            int TotalRes = Convert.ToInt32(Session["progreso"]);


            if (respBD.Objects.Count == 0 || respBD.Objects.Count == 1)
            {
                ViewBag.RespuestasP5 = Session["RespuestasP5"];
                List<string> Respuesta5 = new List<string>();
                Respuesta5.Add("ValorDefault");

                if (ViewBag.RespuestasP5 == null)
                {
                    return View("EncuestaP5");
                }
                else
                {
                    foreach (ML.EmpleadoRespuesta obj in ViewBag.RespuestasP5)
                    {
                        Respuesta5.Add(obj.RespuestaEmpleado);
                    }
                    ViewBag.Answers5 = Respuesta5;
                    return View("EncuestaP5");
                }
            }
            else
            {
                List<string> Respuesta1 = new List<string>();
                Respuesta1.Add("ValorDefault1");
                //Si hay algo en BD
                ViewBag.RespuestasP3 = respBD.Objects;
                foreach (ML.EmpleadoRespuesta obj in ViewBag.RespuestasP3)
                {
                    Respuesta1.Add(obj.RespuestaEmpleado);
                }
                Session["Resp"] = Respuesta1;
                ViewBag.Answers5 = Respuesta1;
                return View("EncuestaP5");
            }

            
        }
        [HttpPost]
        public ActionResult EncuestaP5(List<ML.EmpleadoRespuesta> lista)
        {
            Session["RespuestasP5"] = lista;
            ViewBag.RespuestasP5 = Session["RespuestasP5"];
            List<string> Respuesta5 = new List<string>();
            Respuesta5.Add("ValorDefault");

            if (ViewBag.RespuestasP5 == null)
            {
                return Json("success");
            }
            else
            {
                foreach (ML.EmpleadoRespuesta obj in ViewBag.RespuestasP5)
                {
                    Respuesta5.Add(obj.RespuestaEmpleado);
                }
                Session["AvanceSeccion"] = 6;
                ViewBag.Answers5 = Respuesta5;
                return Json("success");
            }
        }
        [HttpGet]
        public ActionResult EncuestaP6Get()
        {
            ML.EmpleadoRespuesta empRes = new ML.EmpleadoRespuesta();
            empRes.Empleado = new ML.Empleado();
            empRes.Empleado.IdEmpleado = Convert.ToInt32(Session["EmpleadoEncuestado"]);
            var respBD = BL.Respuestas.GetResP6BD(empRes);

            GetRespuestasByIdEmpleado(empRes.Empleado.IdEmpleado);
            int TotalRes = Convert.ToInt32(Session["progreso"]);


            if (respBD.Objects.Count == 0 || respBD.Objects.Count == 1)
            {
                ViewBag.RespuestasP6 = Session["RespuestasP6"];
                List<string> Respuesta6 = new List<string>();
                Respuesta6.Add("ValorDefault");

                if (ViewBag.RespuestasP6 == null)
                {
                    return View("EncuestaP6");
                }
                else
                {
                    foreach (ML.EmpleadoRespuesta obj in ViewBag.RespuestasP6)
                    {
                        Respuesta6.Add(obj.RespuestaEmpleado);
                    }
                    ViewBag.Answers6 = Respuesta6;
                    return View("EncuestaP6");
                }
            }
            else
            {
                List<string> Respuesta1 = new List<string>();
                Respuesta1.Add("ValorDefault1");
                //Si hay algo en BD
                ViewBag.RespuestasP3 = respBD.Objects;
                foreach (ML.EmpleadoRespuesta obj in ViewBag.RespuestasP3)
                {
                    Respuesta1.Add(obj.RespuestaEmpleado);
                }
                Session["Resp"] = Respuesta1;
                ViewBag.Answers6 = Respuesta1;
                return View("EncuestaP6");
            }

            
        }
        [HttpPost]
        public ActionResult EncuestaP6(List<ML.EmpleadoRespuesta> lista)
        {
            Session["RespuestasP6"] = lista;
            ViewBag.RespuestasP6 = Session["RespuestasP6"];
            List<string> Respuesta6 = new List<string>();
            Respuesta6.Add("ValorDefault");
            if (ViewBag.RespuestasP6 == null)
            {
                return Json("success");
            }
            else
            {
                foreach (ML.EmpleadoRespuesta obj in ViewBag.RespuestasP6)
                {
                    Respuesta6.Add(obj.RespuestaEmpleado);
                }
                ViewBag.Answers6 = Respuesta6;
                Session["AvanceSeccion"] = 7;
                return Json("success");
            }
        }
        [HttpGet]
        public ActionResult EncuestaP7Get()
        {
            ML.EmpleadoRespuesta empRes = new ML.EmpleadoRespuesta();
            empRes.Empleado = new ML.Empleado();
            empRes.Empleado.IdEmpleado = Convert.ToInt32(Session["EmpleadoEncuestado"]);
            var respBD = BL.Respuestas.GetResP7BD(empRes);

            GetRespuestasByIdEmpleado(empRes.Empleado.IdEmpleado);
            int TotalRes = Convert.ToInt32(Session["progreso"]);


            if (respBD.Objects.Count == 0 || respBD.Objects.Count == 1)
            {
                ViewBag.RespuestasP7 = Session["RespuestasP7"];
                List<string> Respuesta7 = new List<string>();
                Respuesta7.Add("ValorDefault");

                if (ViewBag.RespuestasP7 == null)
                {
                    return View("EncuestaP7");
                }
                else
                {
                    foreach (ML.EmpleadoRespuesta obj in ViewBag.RespuestasP7)
                    {
                        Respuesta7.Add(obj.RespuestaEmpleado);
                    }
                    ViewBag.Answers7 = Respuesta7;
                    return View("EncuestaP7");
                }
            }
            else
            {
                List<string> Respuesta1 = new List<string>();
                Respuesta1.Add("ValorDefault1");
                //Si hay algo en BD
                ViewBag.RespuestasP3 = respBD.Objects;
                foreach (ML.EmpleadoRespuesta obj in ViewBag.RespuestasP3)
                {
                    Respuesta1.Add(obj.RespuestaEmpleado);
                }
                Session["Resp"] = Respuesta1;
                ViewBag.Answers7 = Respuesta1;
                return View("EncuestaP7");
            }

            


        }
        [HttpPost]
        public ActionResult EncuestaP7(List<ML.EmpleadoRespuesta> lista)
        {
            Session["RespuestasP7"] = lista;
            ViewBag.RespuestasP7 = Session["RespuestasP7"];
            List<string> Respuesta7 = new List<string>();
            Respuesta7.Add("ValorDefault");

            if (ViewBag.RespuestasP7 == null)
            {
                return Json("success");
            }
            else
            {
                foreach (ML.EmpleadoRespuesta obj in ViewBag.RespuestasP7)
                {
                    Respuesta7.Add(obj.RespuestaEmpleado);
                }
                Session["AvanceSeccion"] = 8;
                ViewBag.Answers7 = Respuesta7;
                return Json("success");
            }
        }
        [HttpGet]
        public ActionResult EncuestaP8Get()
        {
            ML.EmpleadoRespuesta empRes = new ML.EmpleadoRespuesta();
            empRes.Empleado = new ML.Empleado();
            empRes.Empleado.IdEmpleado = Convert.ToInt32(Session["EmpleadoEncuestado"]);
            var respBD = BL.Respuestas.GetResP8BD(empRes);

            GetRespuestasByIdEmpleado(empRes.Empleado.IdEmpleado);
            int TotalRes = Convert.ToInt32(Session["progreso"]);


            if (respBD.Objects.Count == 0 || respBD.Objects.Count == 1)
            {
                ViewBag.RespuestasP8 = Session["RespuestasP8"];
                List<string> Respuesta8 = new List<string>();
                Respuesta8.Add("ValorDefault");

                if (ViewBag.RespuestasP8 == null)
                {
                    return View("EncuestaP8");
                }
                else
                {
                    foreach (ML.EmpleadoRespuesta obj in ViewBag.RespuestasP8)
                    {
                        Respuesta8.Add(obj.RespuestaEmpleado);
                    }
                    ViewBag.Answers8 = Respuesta8;
                    return View("EncuestaP8");
                }
            }
            else
            {
                List<string> Respuesta1 = new List<string>();
                Respuesta1.Add("ValorDefault1");
                //Si hay algo en BD
                ViewBag.RespuestasP3 = respBD.Objects;
                foreach (ML.EmpleadoRespuesta obj in ViewBag.RespuestasP3)
                {
                    Respuesta1.Add(obj.RespuestaEmpleado);
                }
                Session["Resp"] = Respuesta1;
                ViewBag.Answers8 = Respuesta1;
                return View("EncuestaP8");
            }

            
        }
        [HttpPost]
        public ActionResult EncuestaP8(List<ML.EmpleadoRespuesta> lista)
        {
            Session["RespuestasP8"] = lista;
            ViewBag.RespuestasP8 = Session["RespuestasP8"];
            List<string> Respuesta8 = new List<string>();
            Respuesta8.Add("ValorDefault");

            if (ViewBag.RespuestasP8 == null)
            {
                return Json("success");
            }
            else
            {
                foreach (ML.EmpleadoRespuesta obj in ViewBag.RespuestasP8)
                {
                    Respuesta8.Add(obj.RespuestaEmpleado);
                }
                ViewBag.Answers8 = Respuesta8;
                Session["AvanceSeccion"] = 9;
                return Json("success");
            }
        }
        [HttpGet]
        public ActionResult EncuestaP9Get()
        {
            ML.EmpleadoRespuesta empRes = new ML.EmpleadoRespuesta();
            empRes.Empleado = new ML.Empleado();
            empRes.Empleado.IdEmpleado = Convert.ToInt32(Session["EmpleadoEncuestado"]);
            var respBD = BL.Respuestas.GetResP9BD(empRes);

            GetRespuestasByIdEmpleado(empRes.Empleado.IdEmpleado);
            int TotalRes = Convert.ToInt32(Session["progreso"]);


            if (respBD.Objects.Count == 0 || respBD.Objects.Count == 1)
            {
                ViewBag.RespuestasP9 = Session["RespuestasP9"];
                List<string> Respuesta9 = new List<string>();
                Respuesta9.Add("ValorDefault");

                if (ViewBag.RespuestasP9 == null)
                {
                    return View("EncuestaP9");
                }
                else
                {
                    foreach (ML.EmpleadoRespuesta obj in ViewBag.RespuestasP9)
                    {
                        Respuesta9.Add(obj.RespuestaEmpleado);
                    }
                    ViewBag.Answers9 = Respuesta9;
                    return View("EncuestaP9");
                }
            }
            else
            {
                List<string> Respuesta1 = new List<string>();
                Respuesta1.Add("ValorDefault1");
                //Si hay algo en BD
                ViewBag.RespuestasP3 = respBD.Objects;
                foreach (ML.EmpleadoRespuesta obj in ViewBag.RespuestasP3)
                {
                    Respuesta1.Add(obj.RespuestaEmpleado);
                }
                Session["Resp"] = Respuesta1;
                ViewBag.Answers9 = Respuesta1;
                return View("EncuestaP9");
            }

            
        }
        [HttpPost]
        public ActionResult EncuestaP9(List<ML.EmpleadoRespuesta> lista)
        {
            Session["RespuestasP9"] = lista;
            ViewBag.RespuestasP9 = Session["RespuestasP9"];
            List<string> Respuesta9 = new List<string>();
            Respuesta9.Add("ValorDefault");

            if (ViewBag.RespuestasP9 == null)
            {
                return Json("success");
            }
            else
            {
                foreach (ML.EmpleadoRespuesta obj in ViewBag.RespuestasP9)
                {
                    Respuesta9.Add(obj.RespuestaEmpleado);
                }
                Session["AvanceSeccion"] = 10;
                ViewBag.Answers9 = Respuesta9;
                return Json("success");
            }
        }
        [HttpGet]
        public ActionResult EncuestaP9AGet()
        {
            ML.EmpleadoRespuesta empRes = new ML.EmpleadoRespuesta();
            empRes.Empleado = new ML.Empleado();
            empRes.Empleado.IdEmpleado = Convert.ToInt32(Session["EmpleadoEncuestado"]);
            var respBD = BL.Respuestas.GetResP9ABD(empRes);

            GetRespuestasByIdEmpleado(empRes.Empleado.IdEmpleado);
            int TotalRes = Convert.ToInt32(Session["progreso"]);


            if (respBD.Objects.Count == 0 || respBD.Objects.Count == 1)
            {
                ViewBag.Respuestas9A = Session["RespuestasP9A"];
                List<string> Respuesta9A = new List<string>();
                Respuesta9A.Add("ValorDefault");

                if (ViewBag.Respuestas9A == null)
                {
                    return View("EncuestaP9A");
                }
                else
                {
                    foreach (ML.EmpleadoRespuesta obj in ViewBag.Respuestas9A)
                    {
                        Respuesta9A.Add(obj.RespuestaEmpleado);
                    }
                    ViewBag.Answers9A = Respuesta9A;
                    return View("EncuestaP9A");
                }
            }
            else
            {
                List<string> Respuesta1 = new List<string>();
                Respuesta1.Add("ValorDefault1");
                //Si hay algo en BD
                ViewBag.RespuestasP3 = respBD.Objects;
                foreach (ML.EmpleadoRespuesta obj in ViewBag.RespuestasP3)
                {
                    Respuesta1.Add(obj.RespuestaEmpleado);
                }
                Session["Resp"] = Respuesta1;
                ViewBag.Answers9A = Respuesta1;
                return View("EncuestaP9A");
            }

            

        }
        [HttpPost]
        public ActionResult EncuestaP9A(List<ML.EmpleadoRespuesta> lista)
        {
            Session["RespuestasP9A"] = lista;
            ViewBag.Respuestas9A = Session["RespuestasP9A"];
            List<string> Respuesta9A = new List<string>();
            Respuesta9A.Add("ValorDefault");

            if (ViewBag.Respuestas9A == null)
            {
                return Json("success");
            }
            else
            {
                foreach (ML.EmpleadoRespuesta obj in ViewBag.Respuestas9A)
                {
                    Respuesta9A.Add(obj.RespuestaEmpleado);
                }
                Session["AvanceSeccion"] = 11;
                ViewBag.Answers9A = Respuesta9A;
                return Json("success");
            }
        }
        [HttpGet]
        public ActionResult EncuestaP9BGet()
        {
            ML.EmpleadoRespuesta empRes = new ML.EmpleadoRespuesta();
            empRes.Empleado = new ML.Empleado();
            empRes.Empleado.IdEmpleado = Convert.ToInt32(Session["EmpleadoEncuestado"]);
            var respBD = BL.Respuestas.GetResP9BBD(empRes);

            GetRespuestasByIdEmpleado(empRes.Empleado.IdEmpleado);
            int TotalRes = Convert.ToInt32(Session["progreso"]);


            if (respBD.Objects.Count == 0 || respBD.Objects.Count == 1)
            {
                ViewBag.Respuestas9B = Session["RespuestasP9B"];
                List<string> Respuesta9B = new List<string>();
                Respuesta9B.Add("ValorDefault");

                if (ViewBag.Respuestas9B == null)
                {
                    return View("EncuestaP9B");
                }
                else
                {
                    foreach (ML.EmpleadoRespuesta obj in ViewBag.Respuestas9B)
                    {
                        Respuesta9B.Add(obj.RespuestaEmpleado);
                    }
                    ViewBag.Answers9B = Respuesta9B;
                    return View("EncuestaP9B");
                }
            }
            else
            {
                List<string> Respuesta1 = new List<string>();
                Respuesta1.Add("ValorDefault1");
                //Si hay algo en BD
                ViewBag.RespuestasP3 = respBD.Objects;
                foreach (ML.EmpleadoRespuesta obj in ViewBag.RespuestasP3)
                {
                    Respuesta1.Add(obj.RespuestaEmpleado);
                }
                Session["Resp"] = Respuesta1;
                ViewBag.Answers9B = Respuesta1;
                return View("EncuestaP9B");
            }

           

        }
        [HttpPost]
        public ActionResult EncuestaP9B(List<ML.EmpleadoRespuesta> lista)
        {
            Session["RespuestasP9B"] = lista;
            ViewBag.Respuestas9B = Session["RespuestasP9B"];
            List<string> Respuesta9B = new List<string>();
            Respuesta9B.Add("ValorDefault");

            if (ViewBag.Respuestas9B == null)
            {
                return Json("success");
            }
            else
            {
                foreach (ML.EmpleadoRespuesta obj in ViewBag.Respuestas9B)
                {
                    Respuesta9B.Add(obj.RespuestaEmpleado);
                }
                Session["AvanceSeccion"] = 12;
                ViewBag.Answers9B = Respuesta9B;
                return Json("success");
            }
        }
        /****Seccion de preguntas Abiertas****/
        [HttpGet]
        public ActionResult EncuestaP10Get()
        {
            ML.EmpleadoRespuesta empRes = new ML.EmpleadoRespuesta();
            empRes.Empleado = new ML.Empleado();
            empRes.Empleado.IdEmpleado = Convert.ToInt32(Session["EmpleadoEncuestado"]);
            var respBD = BL.Respuestas.GetResP10BD(empRes);

            GetRespuestasByIdEmpleado(empRes.Empleado.IdEmpleado);
            int TotalRes = Convert.ToInt32(Session["progreso"]);


            if (respBD.Objects.Count == 0 || respBD.Objects.Count == 1)
            {
                ViewBag.RespuestasP10 = Session["RespuestasP10"];
                List<string> Respuesta10 = new List<string>();
                Respuesta10.Add("ValorDefault");

                if (ViewBag.RespuestasP10 == null)
                {
                    return View("EncuestaP10");
                }
                else
                {
                    foreach (ML.EmpleadoRespuesta obj in ViewBag.RespuestasP10)
                    {
                        Respuesta10.Add(obj.RespuestaEmpleado);
                    }
                    ViewBag.Answers10 = Respuesta10;
                    return View("EncuestaP10");
                }
            }
            else
            {
                List<string> Respuesta1 = new List<string>();
                Respuesta1.Add("ValorDefault1");
                //Si hay algo en BD
                ViewBag.RespuestasP3 = respBD.Objects;
                foreach (ML.EmpleadoRespuesta obj in ViewBag.RespuestasP3)
                {
                    Respuesta1.Add(obj.RespuestaEmpleado);
                }
                Session["Resp"] = Respuesta1;
                ViewBag.Answers10 = Respuesta1;
                return View("EncuestaP10");
            }

            


        }
        [HttpPost]
        public ActionResult EncuestaP10(List<ML.EmpleadoRespuesta> lista)
        {
            Session["RespuestasP10"] = lista;
            ViewBag.RespuestasP10 = Session["RespuestasP10"];
            List<string> Respuesta10 = new List<string>();
            Respuesta10.Add("ValorDefault");

            if (ViewBag.RespuestasP10 == null)
            {
                return Json("success");
            }
            else
            {
                foreach (ML.EmpleadoRespuesta obj in ViewBag.RespuestasP10)
                {
                    Respuesta10.Add(obj.RespuestaEmpleado);
                }
                Session["AvanceSeccion"] = 13;
                ViewBag.Answers10 = Respuesta10;
                return Json("success");
            }
        }
        [HttpGet]
        public ActionResult EncuestaP11Get()
        {
            ViewBag.RespuestasP11 = Session["RespuestasP11"];
            //Envuar info para DDL
            ML.CompanyCategoria companyCategoria = new ML.CompanyCategoria();

            var list = BL.CompanyCategoria.GetAll();
            var list2 = BL.Perfil.GetAll();

            companyCategoria.ListCompanyCategoria = new List<Object>();
            companyCategoria.ListPerfiles = new List<Object>();
            companyCategoria.ListCompanyCategoria = list.Objects;
            companyCategoria.ListPerfiles = list2.Objects;

            return View("EncuestaP11", companyCategoria);
        }
        [HttpPost]
        public ActionResult EncuestaP11()
        {
            ML.CompanyCategoria companyCategoria = new ML.CompanyCategoria();

            var list = BL.CompanyCategoria.GetAll();
            var list2 = BL.Perfil.GetAll();

            companyCategoria.ListCompanyCategoria = new List<Object>();
            companyCategoria.ListPerfiles = new List<Object>();
            companyCategoria.ListCompanyCategoria = list.Objects;
            companyCategoria.ListPerfiles = list2.Objects;
            Session["AvanceSeccion"] = 14;
            return View("EncuestaP11", companyCategoria);
        }
        //[HttpGet]
        public ActionResult Final()
        {
            //PAsar sessiones a null
            Session["RespuestasP1"] = null;
            Session["RespuestasP2"] = null;
            Session["RespuestasP3"] = null;
            Session["RespuestasP4"] = null;
            Session["RespuestasP5"] = null;
            Session["RespuestasP6"] = null;
            Session["RespuestasP7"] = null;
            Session["RespuestasP8"] = null;
            Session["RespuestasP9"] = null;
            Session["RespuestasP9A"] = null;
            Session["RespuestasP9B"] = null;
            Session["RespuestasP10"] = null;

            int EmpleadoEncuestado = Convert.ToInt32(Session["EmpleadoEncuestado"]);
            int IdEncuesta = 1;
            ML.EstatusEncuesta estatusEncuesta = new ML.EstatusEncuesta();
            estatusEncuesta.Empleado = new ML.Empleado();
            estatusEncuesta.Encuesta = new ML.Encuesta();

            estatusEncuesta.Empleado.IdEmpleado = EmpleadoEncuestado;
            estatusEncuesta.Encuesta.IdEncuesta = IdEncuesta;
            var result = BL.Empleado.EstatusEncuestaFinal(estatusEncuesta);

            var resultado = BL.Encuesta.getClimaLaboralByIdEdit(1);

            return View("Final", resultado.EditaEncuesta);
        }
        public ActionResult FinalParcial()
        {
            //PAsar sessiones a null
            Session["TotalRespuestas"] = null;
            Session["RespuestasP1"] = null;
            Session["RespuestasP2"] = null;
            Session["RespuestasP3"] = null;
            Session["RespuestasP4"] = null;
            Session["RespuestasP5"] = null;
            Session["RespuestasP6"] = null;
            Session["RespuestasP7"] = null;
            Session["RespuestasP8"] = null;
            Session["RespuestasP9"] = null;
            Session["RespuestasP9A"] = null;
            Session["RespuestasP9B"] = null;
            Session["RespuestasP10"] = null;
            Session["EmpleadoEncuestado"] = null;
            Session["EncuestaRealizar"] = null;

            var resultado = BL.Encuesta.getClimaLaboralByIdEdit(1);

            return View("Final", resultado.EditaEncuesta);
        }

        public ActionResult Email()
        {
            // ...
            return Redirect("mailto:soporte@diagnostic4u.com");
        }


        //[HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }
        public ActionResult Index()
        {
            Session["EmpleadoEncuestado"] = null;
            return View("Index_CL");
        }
        [HttpGet]
        public ActionResult Inicio()
        {
            var result = BL.Encuesta.getClimaLaboralByIdEdit(1);
            return View("Inicio", result.EditaEncuesta);
        }

        [HttpPost]
        public ActionResult Login(ML.Empleado empleado)
        {

            var result = BL.Login.Autenticacion(empleado);

            if (result.Correct == true)
            {
                return RedirectToAction("Inicio", "Home");
            }
            else
            {
                ViewBag.Message = result.ErrorMessage;
                return PartialView("Modal");
            }
        }
        //DDL: Company
        public JsonResult GetCompanyAjax(int id)
        {
            var result = BL.Company.GetByCompanyCategoria(id);

            List<SelectListItem> Companies = new List<SelectListItem>();

            Companies.Add(new SelectListItem { Text = "Selecciona una opcion", Value = "0" });

            if (result.Objects != null)
            {
                foreach (ML.Company company in result.Objects)
                {
                    Companies.Add(new SelectListItem { Text = company.CompanyName.ToString(), Value = company.CompanyId.ToString() });



                }

            }

            return Json(new SelectList(Companies, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        //DDl: Area
        public JsonResult GetAreaAjax(int id)
        {
            var result = BL.Area.AreaGetByCompanyId(id);

            List<SelectListItem> Areas = new List<SelectListItem>();

            Areas.Add(new SelectListItem { Text = "Selecciona una opcion", Value = "0" });

            if (result.Objects != null)
            {
                foreach (ML.Area area in result.Objects)
                {
                    Areas.Add(new SelectListItem { Text = area.Nombre.ToString(), Value = area.IdArea.ToString() });
                }
            }
            return Json(new SelectList(Areas, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        //DDl: Departamento
        public JsonResult GetDepartamentoAjax(int id)
        {
            var result = BL.Departamento.GetByArea(id);

            List<SelectListItem> Departamentos = new List<SelectListItem>();

            Departamentos.Add(new SelectListItem { Text = "Selecciona una opcion", Value = "0" });

            if (result.Objects != null)
            {
                foreach (ML.Departamento departamento in result.Objects)
                {
                    Departamentos.Add(new SelectListItem { Text = departamento.Nombre.ToString(), Value = departamento.IdDepartamento.ToString() });
                }
            }
            return Json(new SelectList(Departamentos, "Value", "Text", JsonRequestBehavior.AllowGet));
        }

        //Example JSON
        public JsonResult GetJSON(Object jsonData)
        {
            var result = BL.Company.GetByCompanyCategoria(4);

            List<SelectListItem> Companies = new List<SelectListItem>();

            Companies.Add(new SelectListItem { Text = "Selecciona una opcion", Value = "0" });

            if (result.Objects != null)
            {
                foreach (ML.Company company in result.Objects)
                {
                    Companies.Add(new SelectListItem { Text = company.CompanyName.ToString(), Value = company.CompanyId.ToString() });
                }
            }
            return Json(new SelectList(Companies, "Value", "Text", JsonRequestBehavior.AllowGet));
        }






        /*Guardar mi avannce en la encuesta*/

        [HttpPost]
        public ActionResult SaveAvanceP1(List<ML.EmpleadoRespuesta> lista)
        {
            /*Consulta a BD en el rango de IdPregunta entre 1-16
             Si la consulta trae resultados llamar a UPDATE
             Si no hay resultados llamar a INSERT
             */
            int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]);
            //var query = BL.Respuestas.GetRespuestasUsuarioP1(IdUsuario);
            var Clean = BL.Respuestas.CleanP1(IdUsuario);

            //Crear estatus
            ML.EstatusEncuesta estatusEncuesta = new ML.EstatusEncuesta();
            estatusEncuesta.Encuesta = new ML.Encuesta();
            estatusEncuesta.Encuesta.IdEncuesta = Convert.ToInt32(Session["EncuestaRealizar"]);

            estatusEncuesta.Empleado = new ML.Empleado();
            estatusEncuesta.Empleado.IdEmpleado = Convert.ToInt32(Session["EmpleadoEncuestado"]);

            var result = BL.Empleado.EstatusEncuestaAdd(estatusEncuesta);

            //lista son las respuestas del ultimo form "demograficos" 
            //Session["RespuestasP1"] = null;
            ML.Result resultado = new ML.Result();
            resultado.answer = new List<ML.EmpleadoRespuesta>();
            resultado.answer = lista;

            foreach (ML.EmpleadoRespuesta usuarioRespuesta in resultado.answer)
            {
                var results = BL.Respuestas.Add(usuarioRespuesta);
                
            }
            resultado.Correct = true;
            Session["AvanceSeccion"] = 2;
            if (result.Correct == true)
            {
                return Json("success");
            }
            else
            {
                return Json("error");
            }

        }
        [HttpPost]
        public ActionResult SaveAvanceP2(List<ML.EmpleadoRespuesta> lista)
        {
            //Antes del guardado elimina haya o no respuestas para evitar sobreescritura
            

            ViewBag.RespuestasP1 = Session["RespuestasP1"];

            if (ViewBag.RespuestasP1 != null) { lista.AddRange(ViewBag.RespuestasP1); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean1 = BL.Respuestas.CleanP1(IdUsuario); }
            if (ViewBag.RespuestasP2 != null) { lista.AddRange(ViewBag.RespuestasP2); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean2 = BL.Respuestas.CleanP2(IdUsuario); }
            if (ViewBag.RespuestasP3 != null) { lista.AddRange(ViewBag.RespuestasP3); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean3 = BL.Respuestas.CleanP3(IdUsuario); }
            if (ViewBag.RespuestasP4 != null) { lista.AddRange(ViewBag.RespuestasP4); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean4 = BL.Respuestas.CleanP4(IdUsuario); }
            if (ViewBag.RespuestasP5 != null) { lista.AddRange(ViewBag.RespuestasP5); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean5 = BL.Respuestas.CleanP5(IdUsuario); }
            if (ViewBag.RespuestasP6 != null) { lista.AddRange(ViewBag.RespuestasP6); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean6 = BL.Respuestas.CleanP6(IdUsuario); }
            if (ViewBag.RespuestasP7 != null) { lista.AddRange(ViewBag.RespuestasP7); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean7 = BL.Respuestas.CleanP7(IdUsuario); }
            if (ViewBag.RespuestasP8 != null) { lista.AddRange(ViewBag.RespuestasP8); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean8 = BL.Respuestas.CleanP8(IdUsuario); }
            if (ViewBag.RespuestasP9 != null) { lista.AddRange(ViewBag.RespuestasP9); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9 = BL.Respuestas.CleanP9(IdUsuario); }
            if (ViewBag.RespuestasP9A != null) { lista.AddRange(ViewBag.RespuestasP9A); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9A = BL.Respuestas.CleanP9A(IdUsuario); }
            if (ViewBag.RespuestasP9B != null) { lista.AddRange(ViewBag.RespuestasP9B); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9B = BL.Respuestas.CleanP9B(IdUsuario); }
            if (ViewBag.RespuestasP10 != null) { lista.AddRange(ViewBag.RespuestasP10); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean10 = BL.Respuestas.CleanP10(IdUsuario); }
            if (lista != null) { int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean2 = BL.Respuestas.CleanP2(IdUsuario); }

            //Session["RespuestasP1"] = null;
            //Session["RespuestasP2"] = null;
            ML.Result resultado = new ML.Result();
            resultado.answer = new List<ML.EmpleadoRespuesta>();
            resultado.answer = lista;
            Session["AvanceSeccion"] = 3;
            foreach (ML.EmpleadoRespuesta usuarioRespuesta in resultado.answer)
            {
                var result = BL.Respuestas.Add(usuarioRespuesta);
            }
            resultado.Correct = true;
            return Json("success");
        }
        [HttpPost]
        public ActionResult SaveAvanceP3(List<ML.EmpleadoRespuesta> lista)
        {
            //Eliminar para no sobreescribir
            //int Idusuario = Convert.ToInt32(Session["EmpleadoEncuestado"]);

            //lista son las respuestas del ultimo form "demograficos"
            ViewBag.RespuestasP1 = Session["RespuestasP1"];
            ViewBag.RespuestasP2 = Session["RespuestasP2"];

            //lista.AddRange(ViewBag.RespuestasP1);
            //lista.AddRange(ViewBag.RespuestasP2);
            if (ViewBag.RespuestasP1 != null) { lista.AddRange(ViewBag.RespuestasP1); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean1 = BL.Respuestas.CleanP1(IdUsuario); }
            if (ViewBag.RespuestasP2 != null) { lista.AddRange(ViewBag.RespuestasP2); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean2 = BL.Respuestas.CleanP2(IdUsuario); }
            if (ViewBag.RespuestasP3 != null) { lista.AddRange(ViewBag.RespuestasP3); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean3 = BL.Respuestas.CleanP3(IdUsuario); }
            if (ViewBag.RespuestasP4 != null) { lista.AddRange(ViewBag.RespuestasP4); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean4 = BL.Respuestas.CleanP4(IdUsuario); }
            if (ViewBag.RespuestasP5 != null) { lista.AddRange(ViewBag.RespuestasP5); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean5 = BL.Respuestas.CleanP5(IdUsuario); }
            if (ViewBag.RespuestasP6 != null) { lista.AddRange(ViewBag.RespuestasP6); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean6 = BL.Respuestas.CleanP6(IdUsuario); }
            if (ViewBag.RespuestasP7 != null) { lista.AddRange(ViewBag.RespuestasP7); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean7 = BL.Respuestas.CleanP7(IdUsuario); }
            if (ViewBag.RespuestasP8 != null) { lista.AddRange(ViewBag.RespuestasP8); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean8 = BL.Respuestas.CleanP8(IdUsuario); }
            if (ViewBag.RespuestasP9 != null) { lista.AddRange(ViewBag.RespuestasP9); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9 = BL.Respuestas.CleanP9(IdUsuario); }
            if (ViewBag.RespuestasP9A != null) { lista.AddRange(ViewBag.RespuestasP9A); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9A = BL.Respuestas.CleanP9A(IdUsuario); }
            if (ViewBag.RespuestasP9B != null) { lista.AddRange(ViewBag.RespuestasP9B); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9B = BL.Respuestas.CleanP9B(IdUsuario); }
            if (ViewBag.RespuestasP10 != null) { lista.AddRange(ViewBag.RespuestasP10); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean10 = BL.Respuestas.CleanP10(IdUsuario); }
            if (lista != null) { int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean3 = BL.Respuestas.CleanP3(IdUsuario); }

            //Session["RespuestasP1"] = null;
            //Session["RespuestasP2"] = null;
            //Session["RespuestasP3"] = null;
            ML.Result resultado = new ML.Result();
            resultado.answer = new List<ML.EmpleadoRespuesta>();
            resultado.answer = lista;
            Session["AvanceSeccion"] = 4;
            foreach (ML.EmpleadoRespuesta usuarioRespuesta in resultado.answer)
            {
                var result = BL.Respuestas.Add(usuarioRespuesta);
            }
            resultado.Correct = true;
            return Json("success");
        }
        [HttpPost]
        public ActionResult SaveAvanceP4(List<ML.EmpleadoRespuesta> lista)
        {
            //Borrado para evitar sobreescritura

            //lista son las respuestas del ultimo form "demograficos"
            ViewBag.RespuestasP1 = Session["RespuestasP1"];
            ViewBag.RespuestasP2 = Session["RespuestasP2"];
            ViewBag.RespuestasP3 = Session["RespuestasP3"];

            //lista.AddRange(ViewBag.RespuestasP1);
            //lista.AddRange(ViewBag.RespuestasP2);
            //lista.AddRange(ViewBag.RespuestasP3);
            if (ViewBag.RespuestasP1 != null) { lista.AddRange(ViewBag.RespuestasP1); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean1 = BL.Respuestas.CleanP1(IdUsuario); }
            if (ViewBag.RespuestasP2 != null) { lista.AddRange(ViewBag.RespuestasP2); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean2 = BL.Respuestas.CleanP2(IdUsuario); }
            if (ViewBag.RespuestasP3 != null) { lista.AddRange(ViewBag.RespuestasP3); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean3 = BL.Respuestas.CleanP3(IdUsuario); }
            if (ViewBag.RespuestasP4 != null) { lista.AddRange(ViewBag.RespuestasP4); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean4 = BL.Respuestas.CleanP4(IdUsuario); }
            if (ViewBag.RespuestasP5 != null) { lista.AddRange(ViewBag.RespuestasP5); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean5 = BL.Respuestas.CleanP5(IdUsuario); }
            if (ViewBag.RespuestasP6 != null) { lista.AddRange(ViewBag.RespuestasP6); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean6 = BL.Respuestas.CleanP6(IdUsuario); }
            if (ViewBag.RespuestasP7 != null) { lista.AddRange(ViewBag.RespuestasP7); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean7 = BL.Respuestas.CleanP7(IdUsuario); }
            if (ViewBag.RespuestasP8 != null) { lista.AddRange(ViewBag.RespuestasP8); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean8 = BL.Respuestas.CleanP8(IdUsuario); }
            if (ViewBag.RespuestasP9 != null) { lista.AddRange(ViewBag.RespuestasP9); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9 = BL.Respuestas.CleanP9(IdUsuario); }
            if (ViewBag.RespuestasP9A != null) { lista.AddRange(ViewBag.RespuestasP9A); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9A = BL.Respuestas.CleanP9A(IdUsuario); }
            if (ViewBag.RespuestasP9B != null) { lista.AddRange(ViewBag.RespuestasP9B); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9B = BL.Respuestas.CleanP9B(IdUsuario); }
            if (ViewBag.RespuestasP10 != null) { lista.AddRange(ViewBag.RespuestasP10); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean10 = BL.Respuestas.CleanP10(IdUsuario); }
            if (lista != null) { int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean4 = BL.Respuestas.CleanP4(IdUsuario); }
            //Session["RespuestasP1"] = null;
            //Session["RespuestasP2"] = null;
            //Session["RespuestasP3"] = null;
            //Session["RespuestasP4"] = null;
            ML.Result resultado = new ML.Result();
            resultado.answer = new List<ML.EmpleadoRespuesta>();
            resultado.answer = lista;
            Session["AvanceSeccion"] = 5;
            foreach (ML.EmpleadoRespuesta usuarioRespuesta in resultado.answer)
            {
                var result = BL.Respuestas.Add(usuarioRespuesta);
            }
            resultado.Correct = true;
            return Json("success");
        }
        [HttpPost]
        public ActionResult SaveAvanceP5(List<ML.EmpleadoRespuesta> lista)
        {
            //Borrado para evitar sobreescritura


            //lista son las respuestas del ultimo form "demograficos"
            ViewBag.RespuestasP1 = Session["RespuestasP1"];
            ViewBag.RespuestasP2 = Session["RespuestasP2"];
            ViewBag.RespuestasP3 = Session["RespuestasP3"];
            ViewBag.RespuestasP4 = Session["RespuestasP4"];

            //lista.AddRange(ViewBag.RespuestasP1);
            //lista.AddRange(ViewBag.RespuestasP2);
            //lista.AddRange(ViewBag.RespuestasP3);
            //lista.AddRange(ViewBag.RespuestasP4);F
            if (ViewBag.RespuestasP1 != null) { lista.AddRange(ViewBag.RespuestasP1); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean1 = BL.Respuestas.CleanP1(IdUsuario); }
            if (ViewBag.RespuestasP2 != null) { lista.AddRange(ViewBag.RespuestasP2); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean2 = BL.Respuestas.CleanP2(IdUsuario); }
            if (ViewBag.RespuestasP3 != null) { lista.AddRange(ViewBag.RespuestasP3); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean3 = BL.Respuestas.CleanP3(IdUsuario); }
            if (ViewBag.RespuestasP4 != null) { lista.AddRange(ViewBag.RespuestasP4); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean4 = BL.Respuestas.CleanP4(IdUsuario); }
            if (ViewBag.RespuestasP5 != null) { lista.AddRange(ViewBag.RespuestasP5); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean5 = BL.Respuestas.CleanP5(IdUsuario); }
            if (ViewBag.RespuestasP6 != null) { lista.AddRange(ViewBag.RespuestasP6); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean6 = BL.Respuestas.CleanP6(IdUsuario); }
            if (ViewBag.RespuestasP7 != null) { lista.AddRange(ViewBag.RespuestasP7); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean7 = BL.Respuestas.CleanP7(IdUsuario); }
            if (ViewBag.RespuestasP8 != null) { lista.AddRange(ViewBag.RespuestasP8); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean8 = BL.Respuestas.CleanP8(IdUsuario); }
            if (ViewBag.RespuestasP9 != null) { lista.AddRange(ViewBag.RespuestasP9); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9 = BL.Respuestas.CleanP9(IdUsuario); }
            if (ViewBag.RespuestasP9A != null) { lista.AddRange(ViewBag.RespuestasP9A); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9A = BL.Respuestas.CleanP9A(IdUsuario); }
            if (ViewBag.RespuestasP9B != null) { lista.AddRange(ViewBag.RespuestasP9B); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9B = BL.Respuestas.CleanP9B(IdUsuario); }
            if (ViewBag.RespuestasP10 != null) { lista.AddRange(ViewBag.RespuestasP10); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean10 = BL.Respuestas.CleanP10(IdUsuario); }
            if (lista != null) { int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean5 = BL.Respuestas.CleanP5(IdUsuario); }
            //Session["RespuestasP1"] = null;
            //Session["RespuestasP2"] = null;
            //Session["RespuestasP3"] = null;
            //Session["RespuestasP4"] = null;
            //Session["RespuestasP5"] = null;
            ML.Result resultado = new ML.Result();
            resultado.answer = new List<ML.EmpleadoRespuesta>();
            resultado.answer = lista;
            Session["AvanceSeccion"] = 6;
            foreach (ML.EmpleadoRespuesta usuarioRespuesta in resultado.answer)
            {
                var result = BL.Respuestas.Add(usuarioRespuesta);
            }
            resultado.Correct = true;
            return Json("success");
        }
        [HttpPost]
        public ActionResult SaveAvanceP6(List<ML.EmpleadoRespuesta> lista)
        {
            //Borrado para evitar sobreescritura


            //lista son las respuestas del ultimo form "demograficos"
            ViewBag.RespuestasP1 = Session["RespuestasP1"];
            ViewBag.RespuestasP2 = Session["RespuestasP2"];
            ViewBag.RespuestasP3 = Session["RespuestasP3"];
            ViewBag.RespuestasP4 = Session["RespuestasP4"];
            ViewBag.RespuestasP5 = Session["RespuestasP5"];

            //lista.AddRange(ViewBag.RespuestasP1);
            //lista.AddRange(ViewBag.RespuestasP2);
            //lista.AddRange(ViewBag.RespuestasP3);
            //lista.AddRange(ViewBag.RespuestasP4);
            //lista.AddRange(ViewBag.RespuestasP5);
            if (ViewBag.RespuestasP1 != null) { lista.AddRange(ViewBag.RespuestasP1); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean1 = BL.Respuestas.CleanP1(IdUsuario); }
            if (ViewBag.RespuestasP2 != null) { lista.AddRange(ViewBag.RespuestasP2); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean2 = BL.Respuestas.CleanP2(IdUsuario); }
            if (ViewBag.RespuestasP3 != null) { lista.AddRange(ViewBag.RespuestasP3); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean3 = BL.Respuestas.CleanP3(IdUsuario); }
            if (ViewBag.RespuestasP4 != null) { lista.AddRange(ViewBag.RespuestasP4); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean4 = BL.Respuestas.CleanP4(IdUsuario); }
            if (ViewBag.RespuestasP5 != null) { lista.AddRange(ViewBag.RespuestasP5); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean5 = BL.Respuestas.CleanP5(IdUsuario); }
            if (ViewBag.RespuestasP6 != null) { lista.AddRange(ViewBag.RespuestasP6); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean6 = BL.Respuestas.CleanP6(IdUsuario); }
            if (ViewBag.RespuestasP7 != null) { lista.AddRange(ViewBag.RespuestasP7); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean7 = BL.Respuestas.CleanP7(IdUsuario); }
            if (ViewBag.RespuestasP8 != null) { lista.AddRange(ViewBag.RespuestasP8); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean8 = BL.Respuestas.CleanP8(IdUsuario); }
            if (ViewBag.RespuestasP9 != null) { lista.AddRange(ViewBag.RespuestasP9); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9 = BL.Respuestas.CleanP9(IdUsuario); }
            if (ViewBag.RespuestasP9A != null) { lista.AddRange(ViewBag.RespuestasP9A); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9A = BL.Respuestas.CleanP9A(IdUsuario); }
            if (ViewBag.RespuestasP9B != null) { lista.AddRange(ViewBag.RespuestasP9B); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9B = BL.Respuestas.CleanP9B(IdUsuario); }
            if (ViewBag.RespuestasP10 != null) { lista.AddRange(ViewBag.RespuestasP10); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean10 = BL.Respuestas.CleanP10(IdUsuario); }
            if (lista != null) { int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean6 = BL.Respuestas.CleanP6(IdUsuario); }
            //Session["RespuestasP1"] = null;
            //Session["RespuestasP2"] = null;
            //Session["RespuestasP3"] = null;
            //Session["RespuestasP4"] = null;
            //Session["RespuestasP5"] = null;
            //Session["RespuestasP6"] = null;
            ML.Result resultado = new ML.Result();
            resultado.answer = new List<ML.EmpleadoRespuesta>();
            resultado.answer = lista;
            Session["AvanceSeccion"] = 7;
            foreach (ML.EmpleadoRespuesta usuarioRespuesta in resultado.answer)
            {
                var result = BL.Respuestas.Add(usuarioRespuesta);
            }
            resultado.Correct = true;
            return Json("success");
        }
        [HttpPost]
        public ActionResult SaveAvanceP7(List<ML.EmpleadoRespuesta> lista)
        {
            //lista son las respuestas del ultimo form "demograficos"
            ViewBag.RespuestasP1 = Session["RespuestasP1"];
            ViewBag.RespuestasP2 = Session["RespuestasP2"];
            ViewBag.RespuestasP3 = Session["RespuestasP3"];
            ViewBag.RespuestasP4 = Session["RespuestasP4"];
            ViewBag.RespuestasP5 = Session["RespuestasP5"];
            ViewBag.RespuestasP6 = Session["RespuestasP6"];

            //lista.AddRange(ViewBag.RespuestasP1);
            //lista.AddRange(ViewBag.RespuestasP2);
            //lista.AddRange(ViewBag.RespuestasP3);
            //lista.AddRange(ViewBag.RespuestasP4);
            //lista.AddRange(ViewBag.RespuestasP5);
            //lista.AddRange(ViewBag.RespuestasP6);
            if (ViewBag.RespuestasP1 != null) { lista.AddRange(ViewBag.RespuestasP1); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean1 = BL.Respuestas.CleanP1(IdUsuario); }
            if (ViewBag.RespuestasP2 != null) { lista.AddRange(ViewBag.RespuestasP2); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean2 = BL.Respuestas.CleanP2(IdUsuario); }
            if (ViewBag.RespuestasP3 != null) { lista.AddRange(ViewBag.RespuestasP3); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean3 = BL.Respuestas.CleanP3(IdUsuario); }
            if (ViewBag.RespuestasP4 != null) { lista.AddRange(ViewBag.RespuestasP4); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean4 = BL.Respuestas.CleanP4(IdUsuario); }
            if (ViewBag.RespuestasP5 != null) { lista.AddRange(ViewBag.RespuestasP5); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean5 = BL.Respuestas.CleanP5(IdUsuario); }
            if (ViewBag.RespuestasP6 != null) { lista.AddRange(ViewBag.RespuestasP6); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean6 = BL.Respuestas.CleanP6(IdUsuario); }
            if (ViewBag.RespuestasP7 != null) { lista.AddRange(ViewBag.RespuestasP7); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean7 = BL.Respuestas.CleanP7(IdUsuario); }
            if (ViewBag.RespuestasP8 != null) { lista.AddRange(ViewBag.RespuestasP8); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean8 = BL.Respuestas.CleanP8(IdUsuario); }
            if (ViewBag.RespuestasP9 != null) { lista.AddRange(ViewBag.RespuestasP9); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9 = BL.Respuestas.CleanP9(IdUsuario); }
            if (ViewBag.RespuestasP9A != null) { lista.AddRange(ViewBag.RespuestasP9A); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9A = BL.Respuestas.CleanP9A(IdUsuario); }
            if (ViewBag.RespuestasP9B != null) { lista.AddRange(ViewBag.RespuestasP9B); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9B = BL.Respuestas.CleanP9B(IdUsuario); }
            if (ViewBag.RespuestasP10 != null) { lista.AddRange(ViewBag.RespuestasP10); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean10 = BL.Respuestas.CleanP10(IdUsuario); }
            if (lista != null) { int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean7 = BL.Respuestas.CleanP7(IdUsuario); }
            //Session["RespuestasP1"] = null;
            //Session["RespuestasP2"] = null;
            //Session["RespuestasP3"] = null;
            //Session["RespuestasP4"] = null;
            //Session["RespuestasP5"] = null;
            //Session["RespuestasP6"] = null;
            //Session["RespuestasP7"] = null;
            ML.Result resultado = new ML.Result();
            resultado.answer = new List<ML.EmpleadoRespuesta>();
            resultado.answer = lista;
            Session["AvanceSeccion"] = 8;
            foreach (ML.EmpleadoRespuesta usuarioRespuesta in resultado.answer)
            {
                var result = BL.Respuestas.Add(usuarioRespuesta);
            }
            resultado.Correct = true;
            return Json("success");
        }
        [HttpPost]
        public ActionResult SaveAvanceP8(List<ML.EmpleadoRespuesta> lista)
        {
            //lista son las respuestas del ultimo form "demograficos"
            ViewBag.RespuestasP1 = Session["RespuestasP1"];
            ViewBag.RespuestasP2 = Session["RespuestasP2"];
            ViewBag.RespuestasP3 = Session["RespuestasP3"];
            ViewBag.RespuestasP4 = Session["RespuestasP4"];
            ViewBag.RespuestasP5 = Session["RespuestasP5"];
            ViewBag.RespuestasP6 = Session["RespuestasP6"];
            ViewBag.RespuestasP7 = Session["RespuestasP7"];


            if (ViewBag.RespuestasP1 != null) { lista.AddRange(ViewBag.RespuestasP1); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean1 = BL.Respuestas.CleanP1(IdUsuario); }
            if (ViewBag.RespuestasP2 != null) { lista.AddRange(ViewBag.RespuestasP2); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean2 = BL.Respuestas.CleanP2(IdUsuario); }
            if (ViewBag.RespuestasP3 != null) { lista.AddRange(ViewBag.RespuestasP3); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean3 = BL.Respuestas.CleanP3(IdUsuario); }
            if (ViewBag.RespuestasP4 != null) { lista.AddRange(ViewBag.RespuestasP4); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean4 = BL.Respuestas.CleanP4(IdUsuario); }
            if (ViewBag.RespuestasP5 != null) { lista.AddRange(ViewBag.RespuestasP5); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean5 = BL.Respuestas.CleanP5(IdUsuario); }
            if (ViewBag.RespuestasP6 != null) { lista.AddRange(ViewBag.RespuestasP6); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean6 = BL.Respuestas.CleanP6(IdUsuario); }
            if (ViewBag.RespuestasP7 != null) { lista.AddRange(ViewBag.RespuestasP7); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean7 = BL.Respuestas.CleanP7(IdUsuario); }
            if (ViewBag.RespuestasP8 != null) { lista.AddRange(ViewBag.RespuestasP8); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean8 = BL.Respuestas.CleanP8(IdUsuario); }
            if (ViewBag.RespuestasP9 != null) { lista.AddRange(ViewBag.RespuestasP9); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9 = BL.Respuestas.CleanP9(IdUsuario); }
            if (ViewBag.RespuestasP9A != null) { lista.AddRange(ViewBag.RespuestasP9A); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9A = BL.Respuestas.CleanP9A(IdUsuario); }
            if (ViewBag.RespuestasP9B != null) { lista.AddRange(ViewBag.RespuestasP9B); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9B = BL.Respuestas.CleanP9B(IdUsuario); }
            if (ViewBag.RespuestasP10 != null) { lista.AddRange(ViewBag.RespuestasP10); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean10 = BL.Respuestas.CleanP10(IdUsuario); }
            if (lista != null) { int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean8 = BL.Respuestas.CleanP8(IdUsuario); }
            //Session["RespuestasP1"] = null;
            //Session["RespuestasP2"] = null;
            //Session["RespuestasP3"] = null;
            //Session["RespuestasP4"] = null;
            //Session["RespuestasP5"] = null;
            //Session["RespuestasP6"] = null;
            //Session["RespuestasP7"] = null;
            //Session["RespuestasP8"] = null;
            ML.Result resultado = new ML.Result();
            resultado.answer = new List<ML.EmpleadoRespuesta>();
            resultado.answer = lista;
            Session["AvanceSeccion"] = 9;
            foreach (ML.EmpleadoRespuesta usuarioRespuesta in resultado.answer)
            {
                var result = BL.Respuestas.Add(usuarioRespuesta);
            }
            resultado.Correct = true;
            return Json("success");
        }
        [HttpPost]
        public ActionResult SaveAvanceP9(List<ML.EmpleadoRespuesta> lista)
        {
            //lista son las respuestas del ultimo form "demograficos"
            ViewBag.RespuestasP1 = Session["RespuestasP1"];
            ViewBag.RespuestasP2 = Session["RespuestasP2"];
            ViewBag.RespuestasP3 = Session["RespuestasP3"];
            ViewBag.RespuestasP4 = Session["RespuestasP4"];
            ViewBag.RespuestasP5 = Session["RespuestasP5"];
            ViewBag.RespuestasP6 = Session["RespuestasP6"];
            ViewBag.RespuestasP7 = Session["RespuestasP7"];
            ViewBag.RespuestasP8 = Session["RespuestasP8"];

            //lista.AddRange(ViewBag.RespuestasP1);
            //lista.AddRange(ViewBag.RespuestasP2);
            //lista.AddRange(ViewBag.RespuestasP3);
            //lista.AddRange(ViewBag.RespuestasP4);
            //lista.AddRange(ViewBag.RespuestasP5);
            //lista.AddRange(ViewBag.RespuestasP6);
            //lista.AddRange(ViewBag.RespuestasP7);
            //lista.AddRange(ViewBag.RespuestasP8);
            if (ViewBag.RespuestasP1 != null) { lista.AddRange(ViewBag.RespuestasP1); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean1 = BL.Respuestas.CleanP1(IdUsuario); }
            if (ViewBag.RespuestasP2 != null) { lista.AddRange(ViewBag.RespuestasP2); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean2 = BL.Respuestas.CleanP2(IdUsuario); }
            if (ViewBag.RespuestasP3 != null) { lista.AddRange(ViewBag.RespuestasP3); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean3 = BL.Respuestas.CleanP3(IdUsuario); }
            if (ViewBag.RespuestasP4 != null) { lista.AddRange(ViewBag.RespuestasP4); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean4 = BL.Respuestas.CleanP4(IdUsuario); }
            if (ViewBag.RespuestasP5 != null) { lista.AddRange(ViewBag.RespuestasP5); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean5 = BL.Respuestas.CleanP5(IdUsuario); }
            if (ViewBag.RespuestasP6 != null) { lista.AddRange(ViewBag.RespuestasP6); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean6 = BL.Respuestas.CleanP6(IdUsuario); }
            if (ViewBag.RespuestasP7 != null) { lista.AddRange(ViewBag.RespuestasP7); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean7 = BL.Respuestas.CleanP7(IdUsuario); }
            if (ViewBag.RespuestasP8 != null) { lista.AddRange(ViewBag.RespuestasP8); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean8 = BL.Respuestas.CleanP8(IdUsuario); }
            if (ViewBag.RespuestasP9 != null) { lista.AddRange(ViewBag.RespuestasP9); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9 = BL.Respuestas.CleanP9(IdUsuario); }
            if (ViewBag.RespuestasP9A != null) { lista.AddRange(ViewBag.RespuestasP9A); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9A = BL.Respuestas.CleanP9A(IdUsuario); }
            if (ViewBag.RespuestasP9B != null) { lista.AddRange(ViewBag.RespuestasP9B); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9B = BL.Respuestas.CleanP9B(IdUsuario); }
            if (ViewBag.RespuestasP10 != null) { lista.AddRange(ViewBag.RespuestasP10); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean10 = BL.Respuestas.CleanP10(IdUsuario); }
            if (lista != null) { int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9 = BL.Respuestas.CleanP9(IdUsuario); }
            //Session["RespuestasP1"] = null;
            //Session["RespuestasP2"] = null;
            //Session["RespuestasP3"] = null;
            //Session["RespuestasP4"] = null;
            //Session["RespuestasP5"] = null;
            //Session["RespuestasP6"] = null;
            //Session["RespuestasP7"] = null;
            //Session["RespuestasP8"] = null;
            //Session["RespuestasP9"] = null;
            ML.Result resultado = new ML.Result();
            resultado.answer = new List<ML.EmpleadoRespuesta>();
            resultado.answer = lista;
            Session["AvanceSeccion"] = 10;
            foreach (ML.EmpleadoRespuesta usuarioRespuesta in resultado.answer)
            {
                var result = BL.Respuestas.Add(usuarioRespuesta);
            }
            resultado.Correct = true;
            return Json("success");
        }
        [HttpPost]
        public ActionResult SaveAvanceP9A(List<ML.EmpleadoRespuesta> lista)
        {
            //lista son las respuestas del ultimo form "demograficos"
            ViewBag.RespuestasP1 = Session["RespuestasP1"];
            ViewBag.RespuestasP2 = Session["RespuestasP2"];
            ViewBag.RespuestasP3 = Session["RespuestasP3"];
            ViewBag.RespuestasP4 = Session["RespuestasP4"];
            ViewBag.RespuestasP5 = Session["RespuestasP5"];
            ViewBag.RespuestasP6 = Session["RespuestasP6"];
            ViewBag.RespuestasP7 = Session["RespuestasP7"];
             ViewBag.RespuestasP8 = Session["RespuestasP8"];
            ViewBag.RespuestasP9 = Session["RespuestasP9"];

            if (ViewBag.RespuestasP1 != null) { lista.AddRange(ViewBag.RespuestasP1); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean1 = BL.Respuestas.CleanP1(IdUsuario); }
            if (ViewBag.RespuestasP2 != null) { lista.AddRange(ViewBag.RespuestasP2); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean2 = BL.Respuestas.CleanP2(IdUsuario); }
            if (ViewBag.RespuestasP3 != null) { lista.AddRange(ViewBag.RespuestasP3); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean3 = BL.Respuestas.CleanP3(IdUsuario); }
            if (ViewBag.RespuestasP4 != null) { lista.AddRange(ViewBag.RespuestasP4); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean4 = BL.Respuestas.CleanP4(IdUsuario); }
            if (ViewBag.RespuestasP5 != null) { lista.AddRange(ViewBag.RespuestasP5); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean5 = BL.Respuestas.CleanP5(IdUsuario); }
            if (ViewBag.RespuestasP6 != null) { lista.AddRange(ViewBag.RespuestasP6); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean6 = BL.Respuestas.CleanP6(IdUsuario); }
            if (ViewBag.RespuestasP7 != null) { lista.AddRange(ViewBag.RespuestasP7); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean7 = BL.Respuestas.CleanP7(IdUsuario); }
            if (ViewBag.RespuestasP8 != null) { lista.AddRange(ViewBag.RespuestasP8); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean8 = BL.Respuestas.CleanP8(IdUsuario); }
            if (ViewBag.RespuestasP9 != null) { lista.AddRange(ViewBag.RespuestasP9); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9 = BL.Respuestas.CleanP9(IdUsuario); }
            if (ViewBag.RespuestasP9A != null) { lista.AddRange(ViewBag.RespuestasP9A); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9A = BL.Respuestas.CleanP9A(IdUsuario); }
            if (ViewBag.RespuestasP9B != null) { lista.AddRange(ViewBag.RespuestasP9B); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9B = BL.Respuestas.CleanP9B(IdUsuario); }
            if (ViewBag.RespuestasP10 != null) { lista.AddRange(ViewBag.RespuestasP10); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean10 = BL.Respuestas.CleanP10(IdUsuario); }
            if (lista != null) { int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean2 = BL.Respuestas.CleanP9A(IdUsuario); }

            //Session["RespuestasP1"] = null;
            //Session["RespuestasP2"] = null;
            //Session["RespuestasP3"] = null;
            //Session["RespuestasP4"] = null;
            //Session["RespuestasP5"] = null;
            //Session["RespuestasP6"] = null;
            //Session["RespuestasP7"] = null;
            //Session["RespuestasP8"] = null;
            //Session["RespuestasP9"] = null;
            //Session["RespuestasP9A"] = null;
            ML.Result resultado = new ML.Result();
            resultado.answer = new List<ML.EmpleadoRespuesta>();
            resultado.answer = lista;
            Session["AvanceSeccion"] = 11;
            foreach (ML.EmpleadoRespuesta usuarioRespuesta in resultado.answer)
            {
                var result = BL.Respuestas.Add(usuarioRespuesta);
            }
            resultado.Correct = true;
            return Json("success");
        }
        [HttpPost]
        public ActionResult SaveAvanceP9B(List<ML.EmpleadoRespuesta> lista)
        {
            //lista son las respuestas del ultimo form "demograficos"
            ViewBag.RespuestasP1 = Session["RespuestasP1"];
            ViewBag.RespuestasP2 = Session["RespuestasP2"];
            ViewBag.RespuestasP3 = Session["RespuestasP3"];
            ViewBag.RespuestasP4 = Session["RespuestasP4"];
            ViewBag.RespuestasP5 = Session["RespuestasP5"];
            ViewBag.RespuestasP6 = Session["RespuestasP6"];
            ViewBag.RespuestasP7 = Session["RespuestasP7"];
            ViewBag.RespuestasP8 = Session["RespuestasP8"];
            ViewBag.RespuestasP9 = Session["RespuestasP9"];
            ViewBag.RespuestasP9A = Session["RespuestasP9A"];


            if (ViewBag.RespuestasP1 != null) { lista.AddRange(ViewBag.RespuestasP1); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean1 = BL.Respuestas.CleanP1(IdUsuario); }
            if (ViewBag.RespuestasP2 != null) { lista.AddRange(ViewBag.RespuestasP2); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean2 = BL.Respuestas.CleanP2(IdUsuario); }
            if (ViewBag.RespuestasP3 != null) { lista.AddRange(ViewBag.RespuestasP3); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean3 = BL.Respuestas.CleanP3(IdUsuario); }
            if (ViewBag.RespuestasP4 != null) { lista.AddRange(ViewBag.RespuestasP4); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean4 = BL.Respuestas.CleanP4(IdUsuario); }
            if (ViewBag.RespuestasP5 != null) { lista.AddRange(ViewBag.RespuestasP5); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean5 = BL.Respuestas.CleanP5(IdUsuario); }
            if (ViewBag.RespuestasP6 != null) { lista.AddRange(ViewBag.RespuestasP6); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean6 = BL.Respuestas.CleanP6(IdUsuario); }
            if (ViewBag.RespuestasP7 != null) { lista.AddRange(ViewBag.RespuestasP7); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean7 = BL.Respuestas.CleanP7(IdUsuario); }
            if (ViewBag.RespuestasP8 != null) { lista.AddRange(ViewBag.RespuestasP8); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean8 = BL.Respuestas.CleanP8(IdUsuario); }
            if (ViewBag.RespuestasP9 != null) { lista.AddRange(ViewBag.RespuestasP9); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9 = BL.Respuestas.CleanP9(IdUsuario); }
            if (ViewBag.RespuestasP9A != null) { lista.AddRange(ViewBag.RespuestasP9A); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9A = BL.Respuestas.CleanP9A(IdUsuario); }
            if (ViewBag.RespuestasP9B != null) { lista.AddRange(ViewBag.RespuestasP9B); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9B = BL.Respuestas.CleanP9B(IdUsuario); }
            if (ViewBag.RespuestasP10 != null) { lista.AddRange(ViewBag.RespuestasP10); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean10 = BL.Respuestas.CleanP10(IdUsuario); }
            if (lista != null) { int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean2 = BL.Respuestas.CleanP9B(IdUsuario); }
            //Session["RespuestasP1"] = null;
            //Session["RespuestasP2"] = null;
            //Session["RespuestasP3"] = null;
            //Session["RespuestasP4"] = null;
            //Session["RespuestasP5"] = null;
            //Session["RespuestasP6"] = null;
            //Session["RespuestasP7"] = null;
            //Session["RespuestasP8"] = null;
            //Session["RespuestasP9"] = null;
            //Session["RespuestasP9A"] = null;
            //Session["RespuestasP9B"] = null;
            ML.Result resultado = new ML.Result();
            resultado.answer = new List<ML.EmpleadoRespuesta>();
            resultado.answer = lista;
            Session["AvanceSeccion"] = 12;
            foreach (ML.EmpleadoRespuesta usuarioRespuesta in resultado.answer)
            {
                var result = BL.Respuestas.Add(usuarioRespuesta);
            }
            resultado.Correct = true;
            return Json("success");
        }
        [HttpPost]
        public ActionResult SaveAvanceP10(List<ML.EmpleadoRespuesta> lista)
        {
            //lista son las respuestas del ultimo form "demograficos"
            ViewBag.RespuestasP1 = Session["RespuestasP1"];
            ViewBag.RespuestasP2 = Session["RespuestasP2"];
            ViewBag.RespuestasP3 = Session["RespuestasP3"];
            ViewBag.RespuestasP4 = Session["RespuestasP4"];
            ViewBag.RespuestasP5 = Session["RespuestasP5"];
            ViewBag.RespuestasP6 = Session["RespuestasP6"];
            ViewBag.RespuestasP7 = Session["RespuestasP7"];
            ViewBag.RespuestasP8 = Session["RespuestasP8"];
            ViewBag.RespuestasP9 = Session["RespuestasP9"];
            ViewBag.RespuestasP9A = Session["RespuestasP9A"];
            ViewBag.RespuestasP9B = Session["RespuestasP9B"];

            if (ViewBag.RespuestasP1 != null) { lista.AddRange(ViewBag.RespuestasP1); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean1 = BL.Respuestas.CleanP1(IdUsuario); }
            if (ViewBag.RespuestasP2 != null) { lista.AddRange(ViewBag.RespuestasP2); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean2 = BL.Respuestas.CleanP2(IdUsuario); }
            if (ViewBag.RespuestasP3 != null) { lista.AddRange(ViewBag.RespuestasP3); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean3 = BL.Respuestas.CleanP3(IdUsuario); }
            if (ViewBag.RespuestasP4 != null) { lista.AddRange(ViewBag.RespuestasP4); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean4 = BL.Respuestas.CleanP4(IdUsuario); }
            if (ViewBag.RespuestasP5 != null) { lista.AddRange(ViewBag.RespuestasP5); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean5 = BL.Respuestas.CleanP5(IdUsuario); }
            if (ViewBag.RespuestasP6 != null) { lista.AddRange(ViewBag.RespuestasP6); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean6 = BL.Respuestas.CleanP6(IdUsuario); }
            if (ViewBag.RespuestasP7 != null) { lista.AddRange(ViewBag.RespuestasP7); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean7 = BL.Respuestas.CleanP7(IdUsuario); }
            if (ViewBag.RespuestasP8 != null) { lista.AddRange(ViewBag.RespuestasP8); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean8 = BL.Respuestas.CleanP8(IdUsuario); }
            if (ViewBag.RespuestasP9 != null) { lista.AddRange(ViewBag.RespuestasP9); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9 = BL.Respuestas.CleanP9(IdUsuario); }
            if (ViewBag.RespuestasP9A != null) { lista.AddRange(ViewBag.RespuestasP9A); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9A = BL.Respuestas.CleanP9A(IdUsuario); }
            if (ViewBag.RespuestasP9B != null) { lista.AddRange(ViewBag.RespuestasP9B); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean9B = BL.Respuestas.CleanP9B(IdUsuario); }
            if (ViewBag.RespuestasP10 != null) { lista.AddRange(ViewBag.RespuestasP10); int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean10 = BL.Respuestas.CleanP10(IdUsuario); }
            if (lista != null) { int IdUsuario = Convert.ToInt32(Session["EmpleadoEncuestado"]); var Clean2 = BL.Respuestas.CleanP10(IdUsuario); }
            //Session["RespuestasP1"] = null;
            //Session["RespuestasP2"] = null;
            //Session["RespuestasP3"] = null;
            //Session["RespuestasP4"] = null;
            //Session["RespuestasP5"] = null;
            //Session["RespuestasP6"] = null;
            //Session["RespuestasP7"] = null;
            //Session["RespuestasP8"] = null;
            //Session["RespuestasP9"] = null;
            //Session["RespuestasP9A"] = null;
            //Session["RespuestasP9B"] = null;
            //Session["RespuestasP10"] = null;
            ML.Result resultado = new ML.Result();
            resultado.answer = new List<ML.EmpleadoRespuesta>();
            resultado.answer = lista;
            Session["AvanceSeccion"] = 13;
            foreach (ML.EmpleadoRespuesta usuarioRespuesta in resultado.answer)
            {
                var result = BL.Respuestas.Add(usuarioRespuesta);
            }
            resultado.Correct = true;
            return Json("success");
        }

        [HttpGet]
        public ActionResult Reporte()
        {
            //Call query
            //var result = BL.Reporte.GetAllResultados();
            ViewBag.Event = "Carga del sitio completada";
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            return View(result);
        }
        
        public ActionResult ReporteGetAll()
        {
            ViewBag.Event = "Consulta de todos los registros";
            var result = BL.Reporte.GetAllResultados();

            List<ML.JsonReporte> jsonList = new List<ML.JsonReporte>();

            if (result.Objects != null)
            {
                foreach (ML.EmpleadoRespuesta emp in result.Objects)
                {
                    //resultados.Add(new SelectListItem { Text = "<tr>".ToString() });
                    //resultados.Add(new SelectListItem { Text = emp.Empleado.IdEmpleado.ToString() });
                    //resultados.Add(new SelectListItem { Text = emp.Empleado.ApellidoPaterno.ToString() });
                    //resultados.Add(new SelectListItem { Text = emp.Empleado.ApellidoMaterno.ToString() });
                    //resultados.Add(new SelectListItem { Text = emp.Empleado.Nombre.ToString() });
                    //resultados.Add(new SelectListItem { Text = "</tr>".ToString() });
                    ML.JsonReporte json = new ML.JsonReporte();
                    json.IdEmpleado = emp.Empleado.IdEmpleado;
                    json.ApellidoPaterno = emp.Empleado.ApellidoPaterno;
                    json.ApellidoMaterno = emp.Empleado.ApellidoMaterno;
                    json.Nombre = emp.Empleado.Nombre;
                    json.Puesto = emp.Empleado.Puesto;
                    json.FechaNaciemiento = emp.Empleado.FechaNaciemiento;
                    json.FechaAntiguedad = emp.Empleado.FechaAntiguedad;
                    json.Sexo = emp.Empleado.Sexo;
                    json.Correo = emp.Empleado.Correo;
                    json.TipoFuncion = emp.Empleado.Perfil.Descripcion;
                    json.CondicionTrabajo = emp.Empleado.CondicionTrabajo;
                    json.GradoAcademico = emp.Empleado.GradoAcademico;
                    json.UNIDAD = emp.Empleado.Departamento.Area.Company.CompanyCategoria.Descripcion;
                    json.DIRECION = emp.Empleado.Departamento.Area.Company.CompanyName;
                    json.AREA = emp.Empleado.Departamento.Area.Nombre;
                    json.DEPARTAMENTO = emp.Empleado.Departamento.Nombre;
                    json.SUBDEPARTAMENTO = emp.Empleado.Subdepartamento.Nombre;
                    json.EmpresaContratante = emp.Empleado.EmpresaContratante;
                    json.IdResponsableRH = emp.Empleado.IdResponsableRH;
                    json.NombreResponsableRH = emp.Empleado.NombreResponsableRH;
                    json.IdJefe = emp.Empleado.IdJefe;
                    json.NombreJefe = emp.Empleado.NombreJefe;
                    json.PuestoJefe = emp.Empleado.PuestoJefe;
                    json.IdRespinsableEstructura = emp.Empleado.IdRespinsableEstructura;
                    json.NombreResponsableEstrucutra = emp.Empleado.NombreResponsableEstrucutra;
                    json.ClaveAcceso = emp.Empleado.ClavesAcceso.ClaveAcceso;
                    json.RangoAntiguedad = emp.Empleado.RangoAntiguedad;
                    json.RangoEdad = emp.Empleado.RangoEdad;
                    json.EstatusENcuesta = emp.Empleado.EstatusEncuesta.Estatus;
                    json.EstatusEmpleado = emp.Empleado.EstatusEmpleado;

                    json.EMP_R1 = emp.EMP_R1;   json.EMP_R2 = emp.EMP_R2;   json.EMP_R3 = emp.EMP_R3;   json.EMP_R4 = emp.EMP_R4;   json.EMP_R5 = emp.EMP_R5;   json.EMP_R6 = emp.EMP_R6;   json.EMP_R7 = emp.EMP_R7;
                    json.EMP_R8 = emp.EMP_R8;   json.EMP_R9 = emp.EMP_R9;   json.EMP_R10 = emp.EMP_R10; json.EMP_R11 = emp.EMP_R11;  json.EMP_R12 = emp.EMP_R12;  json.EMP_R13 = emp.EMP_R13; json.EMP_R14 = emp.EMP_R14;
                    json.EMP_R15 = emp.EMP_R15;  json.EMP_R16 = emp.EMP_R16;  json.EMP_R17 = emp.EMP_R17; json.EMP_R18 = emp.EMP_R18;  json.EMP_R19 = emp.EMP_R19; json.EMP_R20 = emp.EMP_R20; json.EMP_R21 = emp.EMP_R21;
                    json.EMP_R22 = emp.EMP_R22;   json.EMP_R23 = emp.EMP_R23;  json.EMP_R24 = emp.EMP_R24; json.EMP_R25 = emp.EMP_R25;  json.EMP_R26 = emp.EMP_R26; json.EMP_R27 = emp.EMP_R27; json.EMP_R28 = emp.EMP_R28;
                    json.EMP_R29 = emp.EMP_R29;    json.EMP_R30 = emp.EMP_R30;  json.EMP_R31 = emp.EMP_R31; json.EMP_R32 = emp.EMP_R32;  json.EMP_R33 = emp.EMP_R33; json.EMP_R34 = emp.EMP_R34; json.EMP_R35 = emp.EMP_R35;
                    json.EMP_R36 = emp.EMP_R36;     json.EMP_R37 = emp.EMP_R37;  json.EMP_R38 = emp.EMP_R38; json.EMP_R39 = emp.EMP_R39;  json.EMP_R40 = emp.EMP_R40; json.EMP_R41 = emp.EMP_R41; json.EMP_R42 = emp.EMP_R42;
                    json.EMP_R43 = emp.EMP_R43;      json.EMP_R44 = emp.EMP_R44;  json.EMP_R45 = emp.EMP_R45; json.EMP_R46 = emp.EMP_R46;  json.EMP_R47 = emp.EMP_R47; json.EMP_R48 = emp.EMP_R48; json.EMP_R49 = emp.EMP_R49;
                    json.EMP_R50 = emp.EMP_R50;       json.EMP_R51 = emp.EMP_R51;  json.EMP_R52 = emp.EMP_R52; json.EMP_R53 = emp.EMP_R53;  json.EMP_R54 = emp.EMP_R54; json.EMP_R55 = emp.EMP_R55; json.EMP_R56 = emp.EMP_R56;
                    json.EMP_R57 = emp.EMP_R57;
                    json.EMP_R58 = emp.EMP_R58;
                    json.EMP_R59 = emp.EMP_R59;
                    json.EMP_R60 = emp.EMP_R60;

                    json.EMP_R61 = emp.EMP_R61;
                    json.EMP_R62 = emp.EMP_R62;
                    json.EMP_R63 = emp.EMP_R63;
                    json.EMP_R64 = emp.EMP_R64;
                    json.EMP_R65 = emp.EMP_R65;
                    json.EMP_R66 = emp.EMP_R66;
                    json.EMP_R67 = emp.EMP_R67;
                    json.EMP_R68 = emp.EMP_R68;
                    json.EMP_R69 = emp.EMP_R69;
                    json.EMP_R70 = emp.EMP_R70;
                    json.EMP_R71 = emp.EMP_R71;
                    json.EMP_R72 = emp.EMP_R72;
                    json.EMP_R73 = emp.EMP_R73;
                    json.EMP_R74 = emp.EMP_R74;
                    json.EMP_R75 = emp.EMP_R75;
                    json.EMP_R76 = emp.EMP_R76;
                    json.EMP_R77 = emp.EMP_R77;
                    json.EMP_R78 = emp.EMP_R78;
                    json.EMP_R79 = emp.EMP_R79;
                    json.EMP_R80 = emp.EMP_R80;

                    json.EMP_R81 = emp.EMP_R81;
                    json.EMP_R82 = emp.EMP_R82;
                    json.EMP_R83 = emp.EMP_R83;
                    json.EMP_R84 = emp.EMP_R84;
                    json.EMP_R85 = emp.EMP_R85;
                    json.EMP_R86 = emp.EMP_R86;

                    //Agregar EE

                    json.ARE_1 = emp.ARE_1;
                    json.ARE_2 = emp.ARE_2;
                    json.ARE_3 = emp.ARE_3;
                    json.ARE_4 = emp.ARE_4;
                    json.ARE_5 = emp.ARE_5;
                    json.ARE_6 = emp.ARE_6;
                    json.ARE_7 = emp.ARE_7;
                    json.ARE_8 = emp.ARE_8;
                    json.ARE_9 = emp.ARE_9;
                    json.ARE_10 = emp.ARE_10;
                    json.ARE_11 = emp.ARE_11;
                    json.ARE_12 = emp.ARE_12;
                    json.ARE_13 = emp.ARE_13;
                    json.ARE_14 = emp.ARE_14;
                    json.ARE_15 = emp.ARE_15;
                    json.ARE_16 = emp.ARE_16;
                    json.ARE_17 = emp.ARE_17;
                    json.ARE_18 = emp.ARE_18;
                    json.ARE_19 = emp.ARE_19;
                    json.ARE_20 = emp.ARE_20;


                    json.ARE_21 = emp.ARE_21;
                    json.ARE_22 = emp.ARE_22;
                    json.ARE_23 = emp.ARE_23;
                    json.ARE_24 = emp.ARE_24;
                    json.ARE_25 = emp.ARE_25;
                    json.ARE_26 = emp.ARE_26;
                    json.ARE_27 = emp.ARE_27;
                    json.ARE_28 = emp.ARE_28;
                    json.ARE_29 = emp.ARE_29;
                    json.ARE_30 = emp.ARE_30;
                    json.ARE_31 = emp.ARE_31;
                    json.ARE_32 = emp.ARE_32;
                    json.ARE_33 = emp.ARE_33;
                    json.ARE_34 = emp.ARE_34;
                    json.ARE_35 = emp.ARE_35;
                    json.ARE_36 = emp.ARE_36;
                    json.ARE_37 = emp.ARE_37;
                    json.ARE_38 = emp.ARE_38;
                    json.ARE_39 = emp.ARE_39;
                    json.ARE_40 = emp.ARE_40;

                    json.ARE_41 = emp.ARE_41;
                    json.ARE_42 = emp.ARE_42;
                    json.ARE_43 = emp.ARE_43;
                    json.ARE_44 = emp.ARE_44;
                    json.ARE_45 = emp.ARE_45;
                    json.ARE_46 = emp.ARE_46;
                    json.ARE_47 = emp.ARE_47;
                    json.ARE_48 = emp.ARE_48;
                    json.ARE_49 = emp.ARE_49;
                    json.ARE_50 = emp.ARE_50;
                    json.ARE_51 = emp.ARE_51;
                    json.ARE_52 = emp.ARE_52;
                    json.ARE_53 = emp.ARE_53;
                    json.ARE_54 = emp.ARE_54;
                    json.ARE_55 = emp.ARE_55;
                    json.ARE_56 = emp.ARE_56;
                    json.ARE_57 = emp.ARE_57;
                    json.ARE_58 = emp.ARE_58;
                    json.ARE_59 = emp.ARE_59;
                    json.ARE_60 = emp.ARE_60;

                    json.ARE_61 = emp.ARE_61;
                    json.ARE_62 = emp.ARE_62;
                    json.ARE_63 = emp.ARE_63;
                    json.ARE_64 = emp.ARE_64;
                    json.ARE_65 = emp.ARE_65;
                    json.ARE_66 = emp.ARE_66;
                    json.ARE_67 = emp.ARE_67;
                    json.ARE_68 = emp.ARE_68;
                    json.ARE_69 = emp.ARE_69;
                    json.ARE_70 = emp.ARE_70;
                    json.ARE_71 = emp.ARE_71;
                    json.ARE_72 = emp.ARE_72;
                    json.ARE_73 = emp.ARE_73;
                    json.ARE_74 = emp.ARE_74;
                    json.ARE_75 = emp.ARE_75;
                    json.ARE_76 = emp.ARE_76;
                    json.ARE_77 = emp.ARE_77;
                    json.ARE_78 = emp.ARE_78;
                    json.ARE_79 = emp.ARE_79;
                    json.ARE_80 = emp.ARE_80;

                    json.ARE_81 = emp.ARE_81;
                    json.ARE_82 = emp.ARE_82;
                    json.ARE_83 = emp.ARE_83;
                    json.ARE_84 = emp.ARE_84;
                    json.ARE_85 = emp.ARE_85;
                    json.ARE_86 = emp.ARE_86;


                    json.ESPECIAL_OPEN = emp.ESPECIAL_OPEN;
                    json.CAMBIAR_OPEN = emp.CAMBIAR_OPEN;
                    json.FORT_JEFE = emp.FORT_JEFE;
                    json.OPORT_JEFE = emp.OPORT_JEFE;
                    json.MOTIVA_TRAB = emp.MOTIVA_TRAB;
                    json.DEJAR_EMP = emp.DEJAR_EMP;
                    json.PRESION = emp.PRESION;
                    json.ANTIGUEDADR = emp.ANTIGUEDAD;
                    json.RANGO_EDADR = emp.RANGO_EDAD;
                    json.CONDICIONR = emp.CONDICION;
                    json.SEXOR = emp.SEXO;
                    json.ACADEMICOR = emp.ACADEMICO;
                    json.FUNCIONR = emp.FUNCION;
                    json.UNIDADRES =emp.UNIDAD;
                    json.DIRECIONRES = emp.DIRECION;
                    json.AREARES = emp.AREA;
                    json.DEPARTAMENTORES = emp.DEPARTAMENTO;
                    


                    jsonList.Add(json);//El nuevo valor sobreescrbe al anterior quedando n cantidda de veces el ultimo
                }

            }
            var data = JsonConvert.SerializeObject(jsonList);

            return Content(data);

        }


        
        
        public ActionResult ReporteByIdEmpleado(int IdEmpleado)
        {
            ViewBag.Event = "Consulta de registros tomando en cuenta el Id del Empleado";
            var result = BL.Reporte.GetResultadosByIdEmpleado(IdEmpleado);
            if (result.Objects.Count == 0)
            {
                ViewBag.Event = "No se encontro ningun registro que coincida con el Id de Empleado proporcionado";
            }

            List<ML.JsonReporte> jsonList = new List<ML.JsonReporte>();

            if (result.Objects != null)
            {
                foreach (ML.EmpleadoRespuesta emp in result.Objects)
                {
                    //resultados.Add(new SelectListItem { Text = "<tr>".ToString() });
                    //resultados.Add(new SelectListItem { Text = emp.Empleado.IdEmpleado.ToString() });
                    //resultados.Add(new SelectListItem { Text = emp.Empleado.ApellidoPaterno.ToString() });
                    //resultados.Add(new SelectListItem { Text = emp.Empleado.ApellidoMaterno.ToString() });
                    //resultados.Add(new SelectListItem { Text = emp.Empleado.Nombre.ToString() });
                    //resultados.Add(new SelectListItem { Text = "</tr>".ToString() });
                    ML.JsonReporte json = new ML.JsonReporte();
                    json.IdEmpleado = emp.Empleado.IdEmpleado;
                    json.ApellidoPaterno = emp.Empleado.ApellidoPaterno;
                    json.ApellidoMaterno = emp.Empleado.ApellidoMaterno;
                    json.Nombre = emp.Empleado.Nombre;
                    json.Puesto = emp.Empleado.Puesto;
                    json.FechaNaciemiento = emp.Empleado.FechaNaciemiento;
                    json.FechaAntiguedad = emp.Empleado.FechaAntiguedad;
                    json.Sexo = emp.Empleado.Sexo;
                    json.Correo = emp.Empleado.Correo;
                    json.TipoFuncion = emp.Empleado.Perfil.Descripcion;
                    json.CondicionTrabajo = emp.Empleado.CondicionTrabajo;
                    json.GradoAcademico = emp.Empleado.GradoAcademico;
                    json.UNIDAD = emp.Empleado.Departamento.Area.Company.CompanyCategoria.Descripcion;
                    json.DIRECION = emp.Empleado.Departamento.Area.Company.CompanyName;
                    json.AREA = emp.Empleado.Departamento.Area.Nombre;
                    json.DEPARTAMENTO = emp.Empleado.Departamento.Nombre;
                    json.SUBDEPARTAMENTO = emp.Empleado.Subdepartamento.Nombre;
                    json.EmpresaContratante = emp.Empleado.EmpresaContratante;
                    json.IdResponsableRH = emp.Empleado.IdResponsableRH;
                    json.NombreResponsableRH = emp.Empleado.NombreResponsableRH;
                    json.IdJefe = emp.Empleado.IdJefe;
                    json.NombreJefe = emp.Empleado.NombreJefe;
                    json.PuestoJefe = emp.Empleado.PuestoJefe;
                    json.IdRespinsableEstructura = emp.Empleado.IdRespinsableEstructura;
                    json.NombreResponsableEstrucutra = emp.Empleado.NombreResponsableEstrucutra;
                    json.ClaveAcceso = emp.Empleado.ClavesAcceso.ClaveAcceso;
                    json.RangoAntiguedad = emp.Empleado.RangoAntiguedad;
                    json.RangoEdad = emp.Empleado.RangoEdad;
                    json.EstatusENcuesta = emp.Empleado.EstatusEncuesta.Estatus;
                    json.EstatusEmpleado = emp.Empleado.EstatusEmpleado;

                    json.EMP_R1 = emp.EMP_R1; json.EMP_R2 = emp.EMP_R2; json.EMP_R3 = emp.EMP_R3; json.EMP_R4 = emp.EMP_R4; json.EMP_R5 = emp.EMP_R5; json.EMP_R6 = emp.EMP_R6; json.EMP_R7 = emp.EMP_R7;
                    json.EMP_R8 = emp.EMP_R8; json.EMP_R9 = emp.EMP_R9; json.EMP_R10 = emp.EMP_R10; json.EMP_R11 = emp.EMP_R11; json.EMP_R12 = emp.EMP_R12; json.EMP_R13 = emp.EMP_R13; json.EMP_R14 = emp.EMP_R14;
                    json.EMP_R15 = emp.EMP_R15; json.EMP_R16 = emp.EMP_R16; json.EMP_R17 = emp.EMP_R17; json.EMP_R18 = emp.EMP_R18; json.EMP_R19 = emp.EMP_R19; json.EMP_R20 = emp.EMP_R20; json.EMP_R21 = emp.EMP_R21;
                    json.EMP_R22 = emp.EMP_R22; json.EMP_R23 = emp.EMP_R23; json.EMP_R24 = emp.EMP_R24; json.EMP_R25 = emp.EMP_R25; json.EMP_R26 = emp.EMP_R26; json.EMP_R27 = emp.EMP_R27; json.EMP_R28 = emp.EMP_R28;
                    json.EMP_R29 = emp.EMP_R29; json.EMP_R30 = emp.EMP_R30; json.EMP_R31 = emp.EMP_R31; json.EMP_R32 = emp.EMP_R32; json.EMP_R33 = emp.EMP_R33; json.EMP_R34 = emp.EMP_R34; json.EMP_R35 = emp.EMP_R35;
                    json.EMP_R36 = emp.EMP_R36; json.EMP_R37 = emp.EMP_R37; json.EMP_R38 = emp.EMP_R38; json.EMP_R39 = emp.EMP_R39; json.EMP_R40 = emp.EMP_R40; json.EMP_R41 = emp.EMP_R41; json.EMP_R42 = emp.EMP_R42;
                    json.EMP_R43 = emp.EMP_R43; json.EMP_R44 = emp.EMP_R44; json.EMP_R45 = emp.EMP_R45; json.EMP_R46 = emp.EMP_R46; json.EMP_R47 = emp.EMP_R47; json.EMP_R48 = emp.EMP_R48; json.EMP_R49 = emp.EMP_R49;
                    json.EMP_R50 = emp.EMP_R50; json.EMP_R51 = emp.EMP_R51; json.EMP_R52 = emp.EMP_R52; json.EMP_R53 = emp.EMP_R53; json.EMP_R54 = emp.EMP_R54; json.EMP_R55 = emp.EMP_R55; json.EMP_R56 = emp.EMP_R56;
                    json.EMP_R57 = emp.EMP_R57;
                    json.EMP_R58 = emp.EMP_R58;
                    json.EMP_R59 = emp.EMP_R59;
                    json.EMP_R60 = emp.EMP_R60;

                    json.EMP_R61 = emp.EMP_R61;
                    json.EMP_R62 = emp.EMP_R62;
                    json.EMP_R63 = emp.EMP_R63;
                    json.EMP_R64 = emp.EMP_R64;
                    json.EMP_R65 = emp.EMP_R65;
                    json.EMP_R66 = emp.EMP_R66;
                    json.EMP_R67 = emp.EMP_R67;
                    json.EMP_R68 = emp.EMP_R68;
                    json.EMP_R69 = emp.EMP_R69;
                    json.EMP_R70 = emp.EMP_R70;
                    json.EMP_R71 = emp.EMP_R71;
                    json.EMP_R72 = emp.EMP_R72;
                    json.EMP_R73 = emp.EMP_R73;
                    json.EMP_R74 = emp.EMP_R74;
                    json.EMP_R75 = emp.EMP_R75;
                    json.EMP_R76 = emp.EMP_R76;
                    json.EMP_R77 = emp.EMP_R77;
                    json.EMP_R78 = emp.EMP_R78;
                    json.EMP_R79 = emp.EMP_R79;
                    json.EMP_R80 = emp.EMP_R80;

                    json.EMP_R81 = emp.EMP_R81;
                    json.EMP_R82 = emp.EMP_R82;
                    json.EMP_R83 = emp.EMP_R83;
                    json.EMP_R84 = emp.EMP_R84;
                    json.EMP_R85 = emp.EMP_R85;
                    json.EMP_R86 = emp.EMP_R86;

                    //Agregar EE

                    json.ARE_1 = emp.ARE_1;
                    json.ARE_2 = emp.ARE_2;
                    json.ARE_3 = emp.ARE_3;
                    json.ARE_4 = emp.ARE_4;
                    json.ARE_5 = emp.ARE_5;
                    json.ARE_6 = emp.ARE_6;
                    json.ARE_7 = emp.ARE_7;
                    json.ARE_8 = emp.ARE_8;
                    json.ARE_9 = emp.ARE_9;
                    json.ARE_10 = emp.ARE_10;
                    json.ARE_11 = emp.ARE_11;
                    json.ARE_12 = emp.ARE_12;
                    json.ARE_13 = emp.ARE_13;
                    json.ARE_14 = emp.ARE_14;
                    json.ARE_15 = emp.ARE_15;
                    json.ARE_16 = emp.ARE_16;
                    json.ARE_17 = emp.ARE_17;
                    json.ARE_18 = emp.ARE_18;
                    json.ARE_19 = emp.ARE_19;
                    json.ARE_20 = emp.ARE_20;


                    json.ARE_21 = emp.ARE_21;
                    json.ARE_22 = emp.ARE_22;
                    json.ARE_23 = emp.ARE_23;
                    json.ARE_24 = emp.ARE_24;
                    json.ARE_25 = emp.ARE_25;
                    json.ARE_26 = emp.ARE_26;
                    json.ARE_27 = emp.ARE_27;
                    json.ARE_28 = emp.ARE_28;
                    json.ARE_29 = emp.ARE_29;
                    json.ARE_30 = emp.ARE_30;
                    json.ARE_31 = emp.ARE_31;
                    json.ARE_32 = emp.ARE_32;
                    json.ARE_33 = emp.ARE_33;
                    json.ARE_34 = emp.ARE_34;
                    json.ARE_35 = emp.ARE_35;
                    json.ARE_36 = emp.ARE_36;
                    json.ARE_37 = emp.ARE_37;
                    json.ARE_38 = emp.ARE_38;
                    json.ARE_39 = emp.ARE_39;
                    json.ARE_40 = emp.ARE_40;

                    json.ARE_41 = emp.ARE_41;
                    json.ARE_42 = emp.ARE_42;
                    json.ARE_43 = emp.ARE_43;
                    json.ARE_44 = emp.ARE_44;
                    json.ARE_45 = emp.ARE_45;
                    json.ARE_46 = emp.ARE_46;
                    json.ARE_47 = emp.ARE_47;
                    json.ARE_48 = emp.ARE_48;
                    json.ARE_49 = emp.ARE_49;
                    json.ARE_50 = emp.ARE_50;
                    json.ARE_51 = emp.ARE_51;
                    json.ARE_52 = emp.ARE_52;
                    json.ARE_53 = emp.ARE_53;
                    json.ARE_54 = emp.ARE_54;
                    json.ARE_55 = emp.ARE_55;
                    json.ARE_56 = emp.ARE_56;
                    json.ARE_57 = emp.ARE_57;
                    json.ARE_58 = emp.ARE_58;
                    json.ARE_59 = emp.ARE_59;
                    json.ARE_60 = emp.ARE_60;

                    json.ARE_61 = emp.ARE_61;
                    json.ARE_62 = emp.ARE_62;
                    json.ARE_63 = emp.ARE_63;
                    json.ARE_64 = emp.ARE_64;
                    json.ARE_65 = emp.ARE_65;
                    json.ARE_66 = emp.ARE_66;
                    json.ARE_67 = emp.ARE_67;
                    json.ARE_68 = emp.ARE_68;
                    json.ARE_69 = emp.ARE_69;
                    json.ARE_70 = emp.ARE_70;
                    json.ARE_71 = emp.ARE_71;
                    json.ARE_72 = emp.ARE_72;
                    json.ARE_73 = emp.ARE_73;
                    json.ARE_74 = emp.ARE_74;
                    json.ARE_75 = emp.ARE_75;
                    json.ARE_76 = emp.ARE_76;
                    json.ARE_77 = emp.ARE_77;
                    json.ARE_78 = emp.ARE_78;
                    json.ARE_79 = emp.ARE_79;
                    json.ARE_80 = emp.ARE_80;

                    json.ARE_81 = emp.ARE_81;
                    json.ARE_82 = emp.ARE_82;
                    json.ARE_83 = emp.ARE_83;
                    json.ARE_84 = emp.ARE_84;
                    json.ARE_85 = emp.ARE_85;
                    json.ARE_86 = emp.ARE_86;

                    json.ESPECIAL_OPEN = emp.ESPECIAL_OPEN;
                    json.CAMBIAR_OPEN = emp.CAMBIAR_OPEN;
                    json.FORT_JEFE = emp.FORT_JEFE;
                    json.OPORT_JEFE = emp.OPORT_JEFE;
                    json.MOTIVA_TRAB = emp.MOTIVA_TRAB;
                    json.DEJAR_EMP = emp.DEJAR_EMP;
                    json.PRESION = emp.PRESION;
                    json.ANTIGUEDADR = emp.ANTIGUEDAD;
                    json.RANGO_EDADR = emp.RANGO_EDAD;
                    json.CONDICIONR = emp.CONDICION;
                    json.SEXOR = emp.SEXO;
                    json.ACADEMICOR = emp.ACADEMICO;
                    json.FUNCIONR = emp.FUNCION;
                    json.UNIDADRES = emp.UNIDAD;
                    json.DIRECIONRES = emp.DIRECION;
                    json.AREARES = emp.AREA;
                    json.DEPARTAMENTORES = emp.DEPARTAMENTO;


                    jsonList.Add(json);//El nuevo valor sobreescrbe al anterior quedando n cantidda de veces el ultimo
                }

            }
            var data = JsonConvert.SerializeObject(jsonList);

            return Content(data);

        }


        public ActionResult ReporteGetByUnidadNegocio(string UNegocio, string IdBD)
        {
            //Enviar Catalogo de unidades de negocio usando viewbag
            int idBD = Convert.ToInt32(IdBD);
            var result = BL.Reporte.GetResultadosByUnidadNegocio(UNegocio, idBD);

            //Llenar mi propiedad json
            List<ML.JsonReporte> jsonList = new List<ML.JsonReporte>();

            if (result.Objects != null)
            {
                foreach (ML.EmpleadoRespuesta emp in result.Objects)
                {
                    //resultados.Add(new SelectListItem { Text = "<tr>".ToString() });
                    //resultados.Add(new SelectListItem { Text = emp.Empleado.IdEmpleado.ToString() });
                    //resultados.Add(new SelectListItem { Text = emp.Empleado.ApellidoPaterno.ToString() });
                    //resultados.Add(new SelectListItem { Text = emp.Empleado.ApellidoMaterno.ToString() });
                    //resultados.Add(new SelectListItem { Text = emp.Empleado.Nombre.ToString() });
                    //resultados.Add(new SelectListItem { Text = "</tr>".ToString() });
                    ML.JsonReporte json = new ML.JsonReporte();
                    json.IdEmpleado = emp.Empleado.IdEmpleado;
                    json.ApellidoPaterno = emp.Empleado.ApellidoPaterno;
                    json.ApellidoMaterno = emp.Empleado.ApellidoMaterno;
                    json.Nombre = emp.Empleado.Nombre;
                    json.Puesto = emp.Empleado.Puesto;
                    json.FechaNaciemiento = emp.Empleado.FechaNaciemiento;
                    json.FechaAntiguedad = emp.Empleado.FechaAntiguedad;
                    json.Sexo = emp.Empleado.Sexo;
                    json.Correo = emp.Empleado.Correo;
                    json.TipoFuncion = emp.Empleado.Perfil.Descripcion;
                    json.CondicionTrabajo = emp.Empleado.CondicionTrabajo;
                    json.GradoAcademico = emp.Empleado.GradoAcademico;
                    json.UNIDAD = emp.Empleado.Departamento.Area.Company.CompanyCategoria.Descripcion;
                    json.DIRECION = emp.Empleado.Departamento.Area.Company.CompanyName;
                    json.AREA = emp.Empleado.Departamento.Area.Nombre;
                    json.DEPARTAMENTO = emp.Empleado.Departamento.Nombre;
                    json.SUBDEPARTAMENTO = emp.Empleado.Subdepartamento.Nombre;
                    json.EmpresaContratante = emp.Empleado.EmpresaContratante;
                    json.IdResponsableRH = emp.Empleado.IdResponsableRH;
                    json.NombreResponsableRH = emp.Empleado.NombreResponsableRH;
                    json.IdJefe = emp.Empleado.IdJefe;
                    json.NombreJefe = emp.Empleado.NombreJefe;
                    json.PuestoJefe = emp.Empleado.PuestoJefe;
                    json.IdRespinsableEstructura = emp.Empleado.IdRespinsableEstructura;
                    json.NombreResponsableEstrucutra = emp.Empleado.NombreResponsableEstrucutra;
                    json.ClaveAcceso = emp.Empleado.ClavesAcceso.ClaveAcceso;
                    json.RangoAntiguedad = emp.Empleado.RangoAntiguedad;
                    json.RangoEdad = emp.Empleado.RangoEdad;
                    json.EstatusENcuesta = emp.Empleado.EstatusEncuesta.Estatus;
                    json.EstatusEmpleado = emp.Empleado.EstatusEmpleado;

                    json.EMP_R1 = emp.EMP_R1; json.EMP_R2 = emp.EMP_R2; json.EMP_R3 = emp.EMP_R3; json.EMP_R4 = emp.EMP_R4; json.EMP_R5 = emp.EMP_R5; json.EMP_R6 = emp.EMP_R6; json.EMP_R7 = emp.EMP_R7;
                    json.EMP_R8 = emp.EMP_R8; json.EMP_R9 = emp.EMP_R9; json.EMP_R10 = emp.EMP_R10; json.EMP_R11 = emp.EMP_R11; json.EMP_R12 = emp.EMP_R12; json.EMP_R13 = emp.EMP_R13; json.EMP_R14 = emp.EMP_R14;
                    json.EMP_R15 = emp.EMP_R15; json.EMP_R16 = emp.EMP_R16; json.EMP_R17 = emp.EMP_R17; json.EMP_R18 = emp.EMP_R18; json.EMP_R19 = emp.EMP_R19; json.EMP_R20 = emp.EMP_R20; json.EMP_R21 = emp.EMP_R21;
                    json.EMP_R22 = emp.EMP_R22; json.EMP_R23 = emp.EMP_R23; json.EMP_R24 = emp.EMP_R24; json.EMP_R25 = emp.EMP_R25; json.EMP_R26 = emp.EMP_R26; json.EMP_R27 = emp.EMP_R27; json.EMP_R28 = emp.EMP_R28;
                    json.EMP_R29 = emp.EMP_R29; json.EMP_R30 = emp.EMP_R30; json.EMP_R31 = emp.EMP_R31; json.EMP_R32 = emp.EMP_R32; json.EMP_R33 = emp.EMP_R33; json.EMP_R34 = emp.EMP_R34; json.EMP_R35 = emp.EMP_R35;
                    json.EMP_R36 = emp.EMP_R36; json.EMP_R37 = emp.EMP_R37; json.EMP_R38 = emp.EMP_R38; json.EMP_R39 = emp.EMP_R39; json.EMP_R40 = emp.EMP_R40; json.EMP_R41 = emp.EMP_R41; json.EMP_R42 = emp.EMP_R42;
                    json.EMP_R43 = emp.EMP_R43; json.EMP_R44 = emp.EMP_R44; json.EMP_R45 = emp.EMP_R45; json.EMP_R46 = emp.EMP_R46; json.EMP_R47 = emp.EMP_R47; json.EMP_R48 = emp.EMP_R48; json.EMP_R49 = emp.EMP_R49;
                    json.EMP_R50 = emp.EMP_R50; json.EMP_R51 = emp.EMP_R51; json.EMP_R52 = emp.EMP_R52; json.EMP_R53 = emp.EMP_R53; json.EMP_R54 = emp.EMP_R54; json.EMP_R55 = emp.EMP_R55; json.EMP_R56 = emp.EMP_R56;
                    json.EMP_R57 = emp.EMP_R57;
                    json.EMP_R58 = emp.EMP_R58;
                    json.EMP_R59 = emp.EMP_R59;
                    json.EMP_R60 = emp.EMP_R60;

                    json.EMP_R61 = emp.EMP_R61;
                    json.EMP_R62 = emp.EMP_R62;
                    json.EMP_R63 = emp.EMP_R63;
                    json.EMP_R64 = emp.EMP_R64;
                    json.EMP_R65 = emp.EMP_R65;
                    json.EMP_R66 = emp.EMP_R66;
                    json.EMP_R67 = emp.EMP_R67;
                    json.EMP_R68 = emp.EMP_R68;
                    json.EMP_R69 = emp.EMP_R69;
                    json.EMP_R70 = emp.EMP_R70;
                    json.EMP_R71 = emp.EMP_R71;
                    json.EMP_R72 = emp.EMP_R72;
                    json.EMP_R73 = emp.EMP_R73;
                    json.EMP_R74 = emp.EMP_R74;
                    json.EMP_R75 = emp.EMP_R75;
                    json.EMP_R76 = emp.EMP_R76;
                    json.EMP_R77 = emp.EMP_R77;
                    json.EMP_R78 = emp.EMP_R78;
                    json.EMP_R79 = emp.EMP_R79;
                    json.EMP_R80 = emp.EMP_R80;

                    json.EMP_R81 = emp.EMP_R81;
                    json.EMP_R82 = emp.EMP_R82;
                    json.EMP_R83 = emp.EMP_R83;
                    json.EMP_R84 = emp.EMP_R84;
                    json.EMP_R85 = emp.EMP_R85;
                    json.EMP_R86 = emp.EMP_R86;

                    //Agregar EE

                    json.ARE_1 = emp.ARE_1;
                    json.ARE_2 = emp.ARE_2;
                    json.ARE_3 = emp.ARE_3;
                    json.ARE_4 = emp.ARE_4;
                    json.ARE_5 = emp.ARE_5;
                    json.ARE_6 = emp.ARE_6;
                    json.ARE_7 = emp.ARE_7;
                    json.ARE_8 = emp.ARE_8;
                    json.ARE_9 = emp.ARE_9;
                    json.ARE_10 = emp.ARE_10;
                    json.ARE_11 = emp.ARE_11;
                    json.ARE_12 = emp.ARE_12;
                    json.ARE_13 = emp.ARE_13;
                    json.ARE_14 = emp.ARE_14;
                    json.ARE_15 = emp.ARE_15;
                    json.ARE_16 = emp.ARE_16;
                    json.ARE_17 = emp.ARE_17;
                    json.ARE_18 = emp.ARE_18;
                    json.ARE_19 = emp.ARE_19;
                    json.ARE_20 = emp.ARE_20;


                    json.ARE_21 = emp.ARE_21;
                    json.ARE_22 = emp.ARE_22;
                    json.ARE_23 = emp.ARE_23;
                    json.ARE_24 = emp.ARE_24;
                    json.ARE_25 = emp.ARE_25;
                    json.ARE_26 = emp.ARE_26;
                    json.ARE_27 = emp.ARE_27;
                    json.ARE_28 = emp.ARE_28;
                    json.ARE_29 = emp.ARE_29;
                    json.ARE_30 = emp.ARE_30;
                    json.ARE_31 = emp.ARE_31;
                    json.ARE_32 = emp.ARE_32;
                    json.ARE_33 = emp.ARE_33;
                    json.ARE_34 = emp.ARE_34;
                    json.ARE_35 = emp.ARE_35;
                    json.ARE_36 = emp.ARE_36;
                    json.ARE_37 = emp.ARE_37;
                    json.ARE_38 = emp.ARE_38;
                    json.ARE_39 = emp.ARE_39;
                    json.ARE_40 = emp.ARE_40;

                    json.ARE_41 = emp.ARE_41;
                    json.ARE_42 = emp.ARE_42;
                    json.ARE_43 = emp.ARE_43;
                    json.ARE_44 = emp.ARE_44;
                    json.ARE_45 = emp.ARE_45;
                    json.ARE_46 = emp.ARE_46;
                    json.ARE_47 = emp.ARE_47;
                    json.ARE_48 = emp.ARE_48;
                    json.ARE_49 = emp.ARE_49;
                    json.ARE_50 = emp.ARE_50;
                    json.ARE_51 = emp.ARE_51;
                    json.ARE_52 = emp.ARE_52;
                    json.ARE_53 = emp.ARE_53;
                    json.ARE_54 = emp.ARE_54;
                    json.ARE_55 = emp.ARE_55;
                    json.ARE_56 = emp.ARE_56;
                    json.ARE_57 = emp.ARE_57;
                    json.ARE_58 = emp.ARE_58;
                    json.ARE_59 = emp.ARE_59;
                    json.ARE_60 = emp.ARE_60;

                    json.ARE_61 = emp.ARE_61;
                    json.ARE_62 = emp.ARE_62;
                    json.ARE_63 = emp.ARE_63;
                    json.ARE_64 = emp.ARE_64;
                    json.ARE_65 = emp.ARE_65;
                    json.ARE_66 = emp.ARE_66;
                    json.ARE_67 = emp.ARE_67;
                    json.ARE_68 = emp.ARE_68;
                    json.ARE_69 = emp.ARE_69;
                    json.ARE_70 = emp.ARE_70;
                    json.ARE_71 = emp.ARE_71;
                    json.ARE_72 = emp.ARE_72;
                    json.ARE_73 = emp.ARE_73;
                    json.ARE_74 = emp.ARE_74;
                    json.ARE_75 = emp.ARE_75;
                    json.ARE_76 = emp.ARE_76;
                    json.ARE_77 = emp.ARE_77;
                    json.ARE_78 = emp.ARE_78;
                    json.ARE_79 = emp.ARE_79;
                    json.ARE_80 = emp.ARE_80;

                    json.ARE_81 = emp.ARE_81;
                    json.ARE_82 = emp.ARE_82;
                    json.ARE_83 = emp.ARE_83;
                    json.ARE_84 = emp.ARE_84;
                    json.ARE_85 = emp.ARE_85;
                    json.ARE_86 = emp.ARE_86;

                    json.ESPECIAL_OPEN = emp.ESPECIAL_OPEN;
                    json.CAMBIAR_OPEN = emp.CAMBIAR_OPEN;
                    json.FORT_JEFE = emp.FORT_JEFE;
                    json.OPORT_JEFE = emp.OPORT_JEFE;
                    json.MOTIVA_TRAB = emp.MOTIVA_TRAB;
                    json.DEJAR_EMP = emp.DEJAR_EMP;
                    json.PRESION = emp.PRESION;
                    json.ANTIGUEDADR = emp.ANTIGUEDAD;
                    json.RANGO_EDADR = emp.RANGO_EDAD;
                    json.CONDICIONR = emp.CONDICION;
                    json.SEXOR = emp.SEXO;
                    json.ACADEMICOR = emp.ACADEMICO;
                    json.FUNCIONR = emp.FUNCION;
                    json.UNIDADRES = emp.UNIDAD;
                    json.DIRECIONRES = emp.DIRECION;
                    json.AREARES = emp.AREA;
                    json.DEPARTAMENTORES = emp.DEPARTAMENTO;


                    jsonList.Add(json);//El nuevo valor sobreescrbe al anterior quedando n cantidda de veces el ultimo
                }

            }
            var data = JsonConvert.SerializeObject(jsonList);

            return Content(data);
        }

        public ActionResult ReporteGetByCompany(string Company)
        {
            //Enviar Catalogo de unidades de negocio usando viewbag
            var result = BL.Reporte.GetResultadosByCompany(Company);

            //Llenar mi propiedad json
            List<ML.JsonReporte> jsonList = new List<ML.JsonReporte>();

            if (result.Objects != null)
            {
                foreach (ML.EmpleadoRespuesta emp in result.Objects)
                {

                    ML.JsonReporte json = new ML.JsonReporte();
                    json.IdEmpleado = emp.Empleado.IdEmpleado;
                    json.ApellidoPaterno = emp.Empleado.ApellidoPaterno;
                    json.ApellidoMaterno = emp.Empleado.ApellidoMaterno;
                    json.Nombre = emp.Empleado.Nombre;
                    json.Puesto = emp.Empleado.Puesto;
                    json.FechaNaciemiento = emp.Empleado.FechaNaciemiento;
                    json.FechaAntiguedad = emp.Empleado.FechaAntiguedad;
                    json.Sexo = emp.Empleado.Sexo;
                    json.Correo = emp.Empleado.Correo;
                    json.TipoFuncion = emp.Empleado.Perfil.Descripcion;
                    json.CondicionTrabajo = emp.Empleado.CondicionTrabajo;
                    json.GradoAcademico = emp.Empleado.GradoAcademico;
                    json.UNIDAD = emp.Empleado.Departamento.Area.Company.CompanyCategoria.Descripcion;
                    json.DIRECION = emp.Empleado.Departamento.Area.Company.CompanyName;
                    json.AREA = emp.Empleado.Departamento.Area.Nombre;
                    json.DEPARTAMENTO = emp.Empleado.Departamento.Nombre;
                    json.SUBDEPARTAMENTO = emp.Empleado.Subdepartamento.Nombre;
                    json.EmpresaContratante = emp.Empleado.EmpresaContratante;
                    json.IdResponsableRH = emp.Empleado.IdResponsableRH;
                    json.NombreResponsableRH = emp.Empleado.NombreResponsableRH;
                    json.IdJefe = emp.Empleado.IdJefe;
                    json.NombreJefe = emp.Empleado.NombreJefe;
                    json.PuestoJefe = emp.Empleado.PuestoJefe;
                    json.IdRespinsableEstructura = emp.Empleado.IdRespinsableEstructura;
                    json.NombreResponsableEstrucutra = emp.Empleado.NombreResponsableEstrucutra;
                    json.ClaveAcceso = emp.Empleado.ClavesAcceso.ClaveAcceso;
                    json.RangoAntiguedad = emp.Empleado.RangoAntiguedad;
                    json.RangoEdad = emp.Empleado.RangoEdad;
                    json.EstatusENcuesta = emp.Empleado.EstatusEncuesta.Estatus;
                    json.EstatusEmpleado = emp.Empleado.EstatusEmpleado;

                    json.EMP_R1 = emp.EMP_R1; json.EMP_R2 = emp.EMP_R2; json.EMP_R3 = emp.EMP_R3; json.EMP_R4 = emp.EMP_R4; json.EMP_R5 = emp.EMP_R5; json.EMP_R6 = emp.EMP_R6; json.EMP_R7 = emp.EMP_R7;
                    json.EMP_R8 = emp.EMP_R8; json.EMP_R9 = emp.EMP_R9; json.EMP_R10 = emp.EMP_R10; json.EMP_R11 = emp.EMP_R11; json.EMP_R12 = emp.EMP_R12; json.EMP_R13 = emp.EMP_R13; json.EMP_R14 = emp.EMP_R14;
                    json.EMP_R15 = emp.EMP_R15; json.EMP_R16 = emp.EMP_R16; json.EMP_R17 = emp.EMP_R17; json.EMP_R18 = emp.EMP_R18; json.EMP_R19 = emp.EMP_R19; json.EMP_R20 = emp.EMP_R20; json.EMP_R21 = emp.EMP_R21;
                    json.EMP_R22 = emp.EMP_R22; json.EMP_R23 = emp.EMP_R23; json.EMP_R24 = emp.EMP_R24; json.EMP_R25 = emp.EMP_R25; json.EMP_R26 = emp.EMP_R26; json.EMP_R27 = emp.EMP_R27; json.EMP_R28 = emp.EMP_R28;
                    json.EMP_R29 = emp.EMP_R29; json.EMP_R30 = emp.EMP_R30; json.EMP_R31 = emp.EMP_R31; json.EMP_R32 = emp.EMP_R32; json.EMP_R33 = emp.EMP_R33; json.EMP_R34 = emp.EMP_R34; json.EMP_R35 = emp.EMP_R35;
                    json.EMP_R36 = emp.EMP_R36; json.EMP_R37 = emp.EMP_R37; json.EMP_R38 = emp.EMP_R38; json.EMP_R39 = emp.EMP_R39; json.EMP_R40 = emp.EMP_R40; json.EMP_R41 = emp.EMP_R41; json.EMP_R42 = emp.EMP_R42;
                    json.EMP_R43 = emp.EMP_R43; json.EMP_R44 = emp.EMP_R44; json.EMP_R45 = emp.EMP_R45; json.EMP_R46 = emp.EMP_R46; json.EMP_R47 = emp.EMP_R47; json.EMP_R48 = emp.EMP_R48; json.EMP_R49 = emp.EMP_R49;
                    json.EMP_R50 = emp.EMP_R50; json.EMP_R51 = emp.EMP_R51; json.EMP_R52 = emp.EMP_R52; json.EMP_R53 = emp.EMP_R53; json.EMP_R54 = emp.EMP_R54; json.EMP_R55 = emp.EMP_R55; json.EMP_R56 = emp.EMP_R56;
                    json.EMP_R57 = emp.EMP_R57;
                    json.EMP_R58 = emp.EMP_R58;
                    json.EMP_R59 = emp.EMP_R59;
                    json.EMP_R60 = emp.EMP_R60;

                    json.EMP_R61 = emp.EMP_R61;
                    json.EMP_R62 = emp.EMP_R62;
                    json.EMP_R63 = emp.EMP_R63;
                    json.EMP_R64 = emp.EMP_R64;
                    json.EMP_R65 = emp.EMP_R65;
                    json.EMP_R66 = emp.EMP_R66;
                    json.EMP_R67 = emp.EMP_R67;
                    json.EMP_R68 = emp.EMP_R68;
                    json.EMP_R69 = emp.EMP_R69;
                    json.EMP_R70 = emp.EMP_R70;
                    json.EMP_R71 = emp.EMP_R71;
                    json.EMP_R72 = emp.EMP_R72;
                    json.EMP_R73 = emp.EMP_R73;
                    json.EMP_R74 = emp.EMP_R74;
                    json.EMP_R75 = emp.EMP_R75;
                    json.EMP_R76 = emp.EMP_R76;
                    json.EMP_R77 = emp.EMP_R77;
                    json.EMP_R78 = emp.EMP_R78;
                    json.EMP_R79 = emp.EMP_R79;
                    json.EMP_R80 = emp.EMP_R80;

                    json.EMP_R81 = emp.EMP_R81;
                    json.EMP_R82 = emp.EMP_R82;
                    json.EMP_R83 = emp.EMP_R83;
                    json.EMP_R84 = emp.EMP_R84;
                    json.EMP_R85 = emp.EMP_R85;
                    json.EMP_R86 = emp.EMP_R86;

                    //Agregar EE

                    json.ARE_1 = emp.ARE_1;
                    json.ARE_2 = emp.ARE_2;
                    json.ARE_3 = emp.ARE_3;
                    json.ARE_4 = emp.ARE_4;
                    json.ARE_5 = emp.ARE_5;
                    json.ARE_6 = emp.ARE_6;
                    json.ARE_7 = emp.ARE_7;
                    json.ARE_8 = emp.ARE_8;
                    json.ARE_9 = emp.ARE_9;
                    json.ARE_10 = emp.ARE_10;
                    json.ARE_11 = emp.ARE_11;
                    json.ARE_12 = emp.ARE_12;
                    json.ARE_13 = emp.ARE_13;
                    json.ARE_14 = emp.ARE_14;
                    json.ARE_15 = emp.ARE_15;
                    json.ARE_16 = emp.ARE_16;
                    json.ARE_17 = emp.ARE_17;
                    json.ARE_18 = emp.ARE_18;
                    json.ARE_19 = emp.ARE_19;
                    json.ARE_20 = emp.ARE_20;


                    json.ARE_21 = emp.ARE_21;
                    json.ARE_22 = emp.ARE_22;
                    json.ARE_23 = emp.ARE_23;
                    json.ARE_24 = emp.ARE_24;
                    json.ARE_25 = emp.ARE_25;
                    json.ARE_26 = emp.ARE_26;
                    json.ARE_27 = emp.ARE_27;
                    json.ARE_28 = emp.ARE_28;
                    json.ARE_29 = emp.ARE_29;
                    json.ARE_30 = emp.ARE_30;
                    json.ARE_31 = emp.ARE_31;
                    json.ARE_32 = emp.ARE_32;
                    json.ARE_33 = emp.ARE_33;
                    json.ARE_34 = emp.ARE_34;
                    json.ARE_35 = emp.ARE_35;
                    json.ARE_36 = emp.ARE_36;
                    json.ARE_37 = emp.ARE_37;
                    json.ARE_38 = emp.ARE_38;
                    json.ARE_39 = emp.ARE_39;
                    json.ARE_40 = emp.ARE_40;

                    json.ARE_41 = emp.ARE_41;
                    json.ARE_42 = emp.ARE_42;
                    json.ARE_43 = emp.ARE_43;
                    json.ARE_44 = emp.ARE_44;
                    json.ARE_45 = emp.ARE_45;
                    json.ARE_46 = emp.ARE_46;
                    json.ARE_47 = emp.ARE_47;
                    json.ARE_48 = emp.ARE_48;
                    json.ARE_49 = emp.ARE_49;
                    json.ARE_50 = emp.ARE_50;
                    json.ARE_51 = emp.ARE_51;
                    json.ARE_52 = emp.ARE_52;
                    json.ARE_53 = emp.ARE_53;
                    json.ARE_54 = emp.ARE_54;
                    json.ARE_55 = emp.ARE_55;
                    json.ARE_56 = emp.ARE_56;
                    json.ARE_57 = emp.ARE_57;
                    json.ARE_58 = emp.ARE_58;
                    json.ARE_59 = emp.ARE_59;
                    json.ARE_60 = emp.ARE_60;

                    json.ARE_61 = emp.ARE_61;
                    json.ARE_62 = emp.ARE_62;
                    json.ARE_63 = emp.ARE_63;
                    json.ARE_64 = emp.ARE_64;
                    json.ARE_65 = emp.ARE_65;
                    json.ARE_66 = emp.ARE_66;
                    json.ARE_67 = emp.ARE_67;
                    json.ARE_68 = emp.ARE_68;
                    json.ARE_69 = emp.ARE_69;
                    json.ARE_70 = emp.ARE_70;
                    json.ARE_71 = emp.ARE_71;
                    json.ARE_72 = emp.ARE_72;
                    json.ARE_73 = emp.ARE_73;
                    json.ARE_74 = emp.ARE_74;
                    json.ARE_75 = emp.ARE_75;
                    json.ARE_76 = emp.ARE_76;
                    json.ARE_77 = emp.ARE_77;
                    json.ARE_78 = emp.ARE_78;
                    json.ARE_79 = emp.ARE_79;
                    json.ARE_80 = emp.ARE_80;

                    json.ARE_81 = emp.ARE_81;
                    json.ARE_82 = emp.ARE_82;
                    json.ARE_83 = emp.ARE_83;
                    json.ARE_84 = emp.ARE_84;
                    json.ARE_85 = emp.ARE_85;
                    json.ARE_86 = emp.ARE_86;

                    json.ESPECIAL_OPEN = emp.ESPECIAL_OPEN;
                    json.CAMBIAR_OPEN = emp.CAMBIAR_OPEN;
                    json.FORT_JEFE = emp.FORT_JEFE;
                    json.OPORT_JEFE = emp.OPORT_JEFE;
                    json.MOTIVA_TRAB = emp.MOTIVA_TRAB;
                    json.DEJAR_EMP = emp.DEJAR_EMP;
                    json.PRESION = emp.PRESION;
                    json.ANTIGUEDADR = emp.ANTIGUEDAD;
                    json.RANGO_EDADR = emp.RANGO_EDAD;
                    json.CONDICIONR = emp.CONDICION;
                    json.SEXOR = emp.SEXO;
                    json.ACADEMICOR = emp.ACADEMICO;
                    json.FUNCIONR = emp.FUNCION;
                    json.UNIDADRES = emp.UNIDAD;
                    json.DIRECIONRES = emp.DIRECION;
                    json.AREARES = emp.AREA;
                    json.DEPARTAMENTORES = emp.DEPARTAMENTO;


                    jsonList.Add(json);//El nuevo valor sobreescrbe al anterior quedando n cantidda de veces el ultimo
                }

            }
            var data = JsonConvert.SerializeObject(jsonList);

            return Content(data);
        }


        public ActionResult ReporteGetByArea(string Area)
        {
            //Enviar Catalogo de unidades de negocio usando viewbag
            var result = BL.Reporte.GetResultadosByArea(Area);

            //Llenar mi propiedad json
            List<ML.JsonReporte> jsonList = new List<ML.JsonReporte>();

            if (result.Objects != null)
            {
                foreach (ML.EmpleadoRespuesta emp in result.Objects)
                {

                    ML.JsonReporte json = new ML.JsonReporte();
                    json.IdEmpleado = emp.Empleado.IdEmpleado;
                    json.ApellidoPaterno = emp.Empleado.ApellidoPaterno;
                    json.ApellidoMaterno = emp.Empleado.ApellidoMaterno;
                    json.Nombre = emp.Empleado.Nombre;
                    json.Puesto = emp.Empleado.Puesto;
                    json.FechaNaciemiento = emp.Empleado.FechaNaciemiento;
                    json.FechaAntiguedad = emp.Empleado.FechaAntiguedad;
                    json.Sexo = emp.Empleado.Sexo;
                    json.Correo = emp.Empleado.Correo;
                    json.TipoFuncion = emp.Empleado.Perfil.Descripcion;
                    json.CondicionTrabajo = emp.Empleado.CondicionTrabajo;
                    json.GradoAcademico = emp.Empleado.GradoAcademico;
                    json.UNIDAD = emp.Empleado.Departamento.Area.Company.CompanyCategoria.Descripcion;
                    json.DIRECION = emp.Empleado.Departamento.Area.Company.CompanyName;
                    json.AREA = emp.Empleado.Departamento.Area.Nombre;
                    json.DEPARTAMENTO = emp.Empleado.Departamento.Nombre;
                    json.SUBDEPARTAMENTO = emp.Empleado.Subdepartamento.Nombre;
                    json.EmpresaContratante = emp.Empleado.EmpresaContratante;
                    json.IdResponsableRH = emp.Empleado.IdResponsableRH;
                    json.NombreResponsableRH = emp.Empleado.NombreResponsableRH;
                    json.IdJefe = emp.Empleado.IdJefe;
                    json.NombreJefe = emp.Empleado.NombreJefe;
                    json.PuestoJefe = emp.Empleado.PuestoJefe;
                    json.IdRespinsableEstructura = emp.Empleado.IdRespinsableEstructura;
                    json.NombreResponsableEstrucutra = emp.Empleado.NombreResponsableEstrucutra;
                    json.ClaveAcceso = emp.Empleado.ClavesAcceso.ClaveAcceso;
                    json.RangoAntiguedad = emp.Empleado.RangoAntiguedad;
                    json.RangoEdad = emp.Empleado.RangoEdad;
                    json.EstatusENcuesta = emp.Empleado.EstatusEncuesta.Estatus;
                    json.EstatusEmpleado = emp.Empleado.EstatusEmpleado;

                    json.EMP_R1 = emp.EMP_R1; json.EMP_R2 = emp.EMP_R2; json.EMP_R3 = emp.EMP_R3; json.EMP_R4 = emp.EMP_R4; json.EMP_R5 = emp.EMP_R5; json.EMP_R6 = emp.EMP_R6; json.EMP_R7 = emp.EMP_R7;
                    json.EMP_R8 = emp.EMP_R8; json.EMP_R9 = emp.EMP_R9; json.EMP_R10 = emp.EMP_R10; json.EMP_R11 = emp.EMP_R11; json.EMP_R12 = emp.EMP_R12; json.EMP_R13 = emp.EMP_R13; json.EMP_R14 = emp.EMP_R14;
                    json.EMP_R15 = emp.EMP_R15; json.EMP_R16 = emp.EMP_R16; json.EMP_R17 = emp.EMP_R17; json.EMP_R18 = emp.EMP_R18; json.EMP_R19 = emp.EMP_R19; json.EMP_R20 = emp.EMP_R20; json.EMP_R21 = emp.EMP_R21;
                    json.EMP_R22 = emp.EMP_R22; json.EMP_R23 = emp.EMP_R23; json.EMP_R24 = emp.EMP_R24; json.EMP_R25 = emp.EMP_R25; json.EMP_R26 = emp.EMP_R26; json.EMP_R27 = emp.EMP_R27; json.EMP_R28 = emp.EMP_R28;
                    json.EMP_R29 = emp.EMP_R29; json.EMP_R30 = emp.EMP_R30; json.EMP_R31 = emp.EMP_R31; json.EMP_R32 = emp.EMP_R32; json.EMP_R33 = emp.EMP_R33; json.EMP_R34 = emp.EMP_R34; json.EMP_R35 = emp.EMP_R35;
                    json.EMP_R36 = emp.EMP_R36; json.EMP_R37 = emp.EMP_R37; json.EMP_R38 = emp.EMP_R38; json.EMP_R39 = emp.EMP_R39; json.EMP_R40 = emp.EMP_R40; json.EMP_R41 = emp.EMP_R41; json.EMP_R42 = emp.EMP_R42;
                    json.EMP_R43 = emp.EMP_R43; json.EMP_R44 = emp.EMP_R44; json.EMP_R45 = emp.EMP_R45; json.EMP_R46 = emp.EMP_R46; json.EMP_R47 = emp.EMP_R47; json.EMP_R48 = emp.EMP_R48; json.EMP_R49 = emp.EMP_R49;
                    json.EMP_R50 = emp.EMP_R50; json.EMP_R51 = emp.EMP_R51; json.EMP_R52 = emp.EMP_R52; json.EMP_R53 = emp.EMP_R53; json.EMP_R54 = emp.EMP_R54; json.EMP_R55 = emp.EMP_R55; json.EMP_R56 = emp.EMP_R56;
                    json.EMP_R57 = emp.EMP_R57;
                    json.EMP_R58 = emp.EMP_R58;
                    json.EMP_R59 = emp.EMP_R59;
                    json.EMP_R60 = emp.EMP_R60;

                    json.EMP_R61 = emp.EMP_R61;
                    json.EMP_R62 = emp.EMP_R62;
                    json.EMP_R63 = emp.EMP_R63;
                    json.EMP_R64 = emp.EMP_R64;
                    json.EMP_R65 = emp.EMP_R65;
                    json.EMP_R66 = emp.EMP_R66;
                    json.EMP_R67 = emp.EMP_R67;
                    json.EMP_R68 = emp.EMP_R68;
                    json.EMP_R69 = emp.EMP_R69;
                    json.EMP_R70 = emp.EMP_R70;
                    json.EMP_R71 = emp.EMP_R71;
                    json.EMP_R72 = emp.EMP_R72;
                    json.EMP_R73 = emp.EMP_R73;
                    json.EMP_R74 = emp.EMP_R74;
                    json.EMP_R75 = emp.EMP_R75;
                    json.EMP_R76 = emp.EMP_R76;
                    json.EMP_R77 = emp.EMP_R77;
                    json.EMP_R78 = emp.EMP_R78;
                    json.EMP_R79 = emp.EMP_R79;
                    json.EMP_R80 = emp.EMP_R80;

                    json.EMP_R81 = emp.EMP_R81;
                    json.EMP_R82 = emp.EMP_R82;
                    json.EMP_R83 = emp.EMP_R83;
                    json.EMP_R84 = emp.EMP_R84;
                    json.EMP_R85 = emp.EMP_R85;
                    json.EMP_R86 = emp.EMP_R86;

                    //Agregar EE

                    json.ARE_1 = emp.ARE_1;
                    json.ARE_2 = emp.ARE_2;
                    json.ARE_3 = emp.ARE_3;
                    json.ARE_4 = emp.ARE_4;
                    json.ARE_5 = emp.ARE_5;
                    json.ARE_6 = emp.ARE_6;
                    json.ARE_7 = emp.ARE_7;
                    json.ARE_8 = emp.ARE_8;
                    json.ARE_9 = emp.ARE_9;
                    json.ARE_10 = emp.ARE_10;
                    json.ARE_11 = emp.ARE_11;
                    json.ARE_12 = emp.ARE_12;
                    json.ARE_13 = emp.ARE_13;
                    json.ARE_14 = emp.ARE_14;
                    json.ARE_15 = emp.ARE_15;
                    json.ARE_16 = emp.ARE_16;
                    json.ARE_17 = emp.ARE_17;
                    json.ARE_18 = emp.ARE_18;
                    json.ARE_19 = emp.ARE_19;
                    json.ARE_20 = emp.ARE_20;


                    json.ARE_21 = emp.ARE_21;
                    json.ARE_22 = emp.ARE_22;
                    json.ARE_23 = emp.ARE_23;
                    json.ARE_24 = emp.ARE_24;
                    json.ARE_25 = emp.ARE_25;
                    json.ARE_26 = emp.ARE_26;
                    json.ARE_27 = emp.ARE_27;
                    json.ARE_28 = emp.ARE_28;
                    json.ARE_29 = emp.ARE_29;
                    json.ARE_30 = emp.ARE_30;
                    json.ARE_31 = emp.ARE_31;
                    json.ARE_32 = emp.ARE_32;
                    json.ARE_33 = emp.ARE_33;
                    json.ARE_34 = emp.ARE_34;
                    json.ARE_35 = emp.ARE_35;
                    json.ARE_36 = emp.ARE_36;
                    json.ARE_37 = emp.ARE_37;
                    json.ARE_38 = emp.ARE_38;
                    json.ARE_39 = emp.ARE_39;
                    json.ARE_40 = emp.ARE_40;

                    json.ARE_41 = emp.ARE_41;
                    json.ARE_42 = emp.ARE_42;
                    json.ARE_43 = emp.ARE_43;
                    json.ARE_44 = emp.ARE_44;
                    json.ARE_45 = emp.ARE_45;
                    json.ARE_46 = emp.ARE_46;
                    json.ARE_47 = emp.ARE_47;
                    json.ARE_48 = emp.ARE_48;
                    json.ARE_49 = emp.ARE_49;
                    json.ARE_50 = emp.ARE_50;
                    json.ARE_51 = emp.ARE_51;
                    json.ARE_52 = emp.ARE_52;
                    json.ARE_53 = emp.ARE_53;
                    json.ARE_54 = emp.ARE_54;
                    json.ARE_55 = emp.ARE_55;
                    json.ARE_56 = emp.ARE_56;
                    json.ARE_57 = emp.ARE_57;
                    json.ARE_58 = emp.ARE_58;
                    json.ARE_59 = emp.ARE_59;
                    json.ARE_60 = emp.ARE_60;

                    json.ARE_61 = emp.ARE_61;
                    json.ARE_62 = emp.ARE_62;
                    json.ARE_63 = emp.ARE_63;
                    json.ARE_64 = emp.ARE_64;
                    json.ARE_65 = emp.ARE_65;
                    json.ARE_66 = emp.ARE_66;
                    json.ARE_67 = emp.ARE_67;
                    json.ARE_68 = emp.ARE_68;
                    json.ARE_69 = emp.ARE_69;
                    json.ARE_70 = emp.ARE_70;
                    json.ARE_71 = emp.ARE_71;
                    json.ARE_72 = emp.ARE_72;
                    json.ARE_73 = emp.ARE_73;
                    json.ARE_74 = emp.ARE_74;
                    json.ARE_75 = emp.ARE_75;
                    json.ARE_76 = emp.ARE_76;
                    json.ARE_77 = emp.ARE_77;
                    json.ARE_78 = emp.ARE_78;
                    json.ARE_79 = emp.ARE_79;
                    json.ARE_80 = emp.ARE_80;

                    json.ARE_81 = emp.ARE_81;
                    json.ARE_82 = emp.ARE_82;
                    json.ARE_83 = emp.ARE_83;
                    json.ARE_84 = emp.ARE_84;
                    json.ARE_85 = emp.ARE_85;
                    json.ARE_86 = emp.ARE_86;

                    json.ESPECIAL_OPEN = emp.ESPECIAL_OPEN;
                    json.CAMBIAR_OPEN = emp.CAMBIAR_OPEN;
                    json.FORT_JEFE = emp.FORT_JEFE;
                    json.OPORT_JEFE = emp.OPORT_JEFE;
                    json.MOTIVA_TRAB = emp.MOTIVA_TRAB;
                    json.DEJAR_EMP = emp.DEJAR_EMP;
                    json.PRESION = emp.PRESION;
                    json.ANTIGUEDADR = emp.ANTIGUEDAD;
                    json.RANGO_EDADR = emp.RANGO_EDAD;
                    json.CONDICIONR = emp.CONDICION;
                    json.SEXOR = emp.SEXO;
                    json.ACADEMICOR = emp.ACADEMICO;
                    json.FUNCIONR = emp.FUNCION;
                    json.UNIDADRES = emp.UNIDAD;
                    json.DIRECIONRES = emp.DIRECION;
                    json.AREARES = emp.AREA;
                    json.DEPARTAMENTORES = emp.DEPARTAMENTO;


                    jsonList.Add(json);//El nuevo valor sobreescrbe al anterior quedando n cantidda de veces el ultimo
                }

            }
            var data = JsonConvert.SerializeObject(jsonList);

            return Content(data);
        }

        public ActionResult ReporteGetByDepartamento(string Dpto)
        {
            //Enviar Catalogo de unidades de negocio usando viewbag
            var result = BL.Reporte.GetResultadosByDepartamento(Dpto);

            //Llenar mi propiedad json
            List<ML.JsonReporte> jsonList = new List<ML.JsonReporte>();

            if (result.Objects != null)
            {
                foreach (ML.EmpleadoRespuesta emp in result.Objects)
                {

                    ML.JsonReporte json = new ML.JsonReporte();
                    json.IdEmpleado = emp.Empleado.IdEmpleado;
                    json.ApellidoPaterno = emp.Empleado.ApellidoPaterno;
                    json.ApellidoMaterno = emp.Empleado.ApellidoMaterno;
                    json.Nombre = emp.Empleado.Nombre;
                    json.Puesto = emp.Empleado.Puesto;
                    json.FechaNaciemiento = emp.Empleado.FechaNaciemiento;
                    json.FechaAntiguedad = emp.Empleado.FechaAntiguedad;
                    json.Sexo = emp.Empleado.Sexo;
                    json.Correo = emp.Empleado.Correo;
                    json.TipoFuncion = emp.Empleado.Perfil.Descripcion;
                    json.CondicionTrabajo = emp.Empleado.CondicionTrabajo;
                    json.GradoAcademico = emp.Empleado.GradoAcademico;
                    json.UNIDAD = emp.Empleado.Departamento.Area.Company.CompanyCategoria.Descripcion;
                    json.DIRECION = emp.Empleado.Departamento.Area.Company.CompanyName;
                    json.AREA = emp.Empleado.Departamento.Area.Nombre;
                    json.DEPARTAMENTO = emp.Empleado.Departamento.Nombre;
                    json.SUBDEPARTAMENTO = emp.Empleado.Subdepartamento.Nombre;
                    json.EmpresaContratante = emp.Empleado.EmpresaContratante;
                    json.IdResponsableRH = emp.Empleado.IdResponsableRH;
                    json.NombreResponsableRH = emp.Empleado.NombreResponsableRH;
                    json.IdJefe = emp.Empleado.IdJefe;
                    json.NombreJefe = emp.Empleado.NombreJefe;
                    json.PuestoJefe = emp.Empleado.PuestoJefe;
                    json.IdRespinsableEstructura = emp.Empleado.IdRespinsableEstructura;
                    json.NombreResponsableEstrucutra = emp.Empleado.NombreResponsableEstrucutra;
                    json.ClaveAcceso = emp.Empleado.ClavesAcceso.ClaveAcceso;
                    json.RangoAntiguedad = emp.Empleado.RangoAntiguedad;
                    json.RangoEdad = emp.Empleado.RangoEdad;
                    json.EstatusENcuesta = emp.Empleado.EstatusEncuesta.Estatus;
                    json.EstatusEmpleado = emp.Empleado.EstatusEmpleado;

                    json.EMP_R1 = emp.EMP_R1; json.EMP_R2 = emp.EMP_R2; json.EMP_R3 = emp.EMP_R3; json.EMP_R4 = emp.EMP_R4; json.EMP_R5 = emp.EMP_R5; json.EMP_R6 = emp.EMP_R6; json.EMP_R7 = emp.EMP_R7;
                    json.EMP_R8 = emp.EMP_R8; json.EMP_R9 = emp.EMP_R9; json.EMP_R10 = emp.EMP_R10; json.EMP_R11 = emp.EMP_R11; json.EMP_R12 = emp.EMP_R12; json.EMP_R13 = emp.EMP_R13; json.EMP_R14 = emp.EMP_R14;
                    json.EMP_R15 = emp.EMP_R15; json.EMP_R16 = emp.EMP_R16; json.EMP_R17 = emp.EMP_R17; json.EMP_R18 = emp.EMP_R18; json.EMP_R19 = emp.EMP_R19; json.EMP_R20 = emp.EMP_R20; json.EMP_R21 = emp.EMP_R21;
                    json.EMP_R22 = emp.EMP_R22; json.EMP_R23 = emp.EMP_R23; json.EMP_R24 = emp.EMP_R24; json.EMP_R25 = emp.EMP_R25; json.EMP_R26 = emp.EMP_R26; json.EMP_R27 = emp.EMP_R27; json.EMP_R28 = emp.EMP_R28;
                    json.EMP_R29 = emp.EMP_R29; json.EMP_R30 = emp.EMP_R30; json.EMP_R31 = emp.EMP_R31; json.EMP_R32 = emp.EMP_R32; json.EMP_R33 = emp.EMP_R33; json.EMP_R34 = emp.EMP_R34; json.EMP_R35 = emp.EMP_R35;
                    json.EMP_R36 = emp.EMP_R36; json.EMP_R37 = emp.EMP_R37; json.EMP_R38 = emp.EMP_R38; json.EMP_R39 = emp.EMP_R39; json.EMP_R40 = emp.EMP_R40; json.EMP_R41 = emp.EMP_R41; json.EMP_R42 = emp.EMP_R42;
                    json.EMP_R43 = emp.EMP_R43; json.EMP_R44 = emp.EMP_R44; json.EMP_R45 = emp.EMP_R45; json.EMP_R46 = emp.EMP_R46; json.EMP_R47 = emp.EMP_R47; json.EMP_R48 = emp.EMP_R48; json.EMP_R49 = emp.EMP_R49;
                    json.EMP_R50 = emp.EMP_R50; json.EMP_R51 = emp.EMP_R51; json.EMP_R52 = emp.EMP_R52; json.EMP_R53 = emp.EMP_R53; json.EMP_R54 = emp.EMP_R54; json.EMP_R55 = emp.EMP_R55; json.EMP_R56 = emp.EMP_R56;
                    json.EMP_R57 = emp.EMP_R57;
                    json.EMP_R58 = emp.EMP_R58;
                    json.EMP_R59 = emp.EMP_R59;
                    json.EMP_R60 = emp.EMP_R60;

                    json.EMP_R61 = emp.EMP_R61;
                    json.EMP_R62 = emp.EMP_R62;
                    json.EMP_R63 = emp.EMP_R63;
                    json.EMP_R64 = emp.EMP_R64;
                    json.EMP_R65 = emp.EMP_R65;
                    json.EMP_R66 = emp.EMP_R66;
                    json.EMP_R67 = emp.EMP_R67;
                    json.EMP_R68 = emp.EMP_R68;
                    json.EMP_R69 = emp.EMP_R69;
                    json.EMP_R70 = emp.EMP_R70;
                    json.EMP_R71 = emp.EMP_R71;
                    json.EMP_R72 = emp.EMP_R72;
                    json.EMP_R73 = emp.EMP_R73;
                    json.EMP_R74 = emp.EMP_R74;
                    json.EMP_R75 = emp.EMP_R75;
                    json.EMP_R76 = emp.EMP_R76;
                    json.EMP_R77 = emp.EMP_R77;
                    json.EMP_R78 = emp.EMP_R78;
                    json.EMP_R79 = emp.EMP_R79;
                    json.EMP_R80 = emp.EMP_R80;

                    json.EMP_R81 = emp.EMP_R81;
                    json.EMP_R82 = emp.EMP_R82;
                    json.EMP_R83 = emp.EMP_R83;
                    json.EMP_R84 = emp.EMP_R84;
                    json.EMP_R85 = emp.EMP_R85;
                    json.EMP_R86 = emp.EMP_R86;

                    //Agregar EE

                    json.ARE_1 = emp.ARE_1;
                    json.ARE_2 = emp.ARE_2;
                    json.ARE_3 = emp.ARE_3;
                    json.ARE_4 = emp.ARE_4;
                    json.ARE_5 = emp.ARE_5;
                    json.ARE_6 = emp.ARE_6;
                    json.ARE_7 = emp.ARE_7;
                    json.ARE_8 = emp.ARE_8;
                    json.ARE_9 = emp.ARE_9;
                    json.ARE_10 = emp.ARE_10;
                    json.ARE_11 = emp.ARE_11;
                    json.ARE_12 = emp.ARE_12;
                    json.ARE_13 = emp.ARE_13;
                    json.ARE_14 = emp.ARE_14;
                    json.ARE_15 = emp.ARE_15;
                    json.ARE_16 = emp.ARE_16;
                    json.ARE_17 = emp.ARE_17;
                    json.ARE_18 = emp.ARE_18;
                    json.ARE_19 = emp.ARE_19;
                    json.ARE_20 = emp.ARE_20;


                    json.ARE_21 = emp.ARE_21;
                    json.ARE_22 = emp.ARE_22;
                    json.ARE_23 = emp.ARE_23;
                    json.ARE_24 = emp.ARE_24;
                    json.ARE_25 = emp.ARE_25;
                    json.ARE_26 = emp.ARE_26;
                    json.ARE_27 = emp.ARE_27;
                    json.ARE_28 = emp.ARE_28;
                    json.ARE_29 = emp.ARE_29;
                    json.ARE_30 = emp.ARE_30;
                    json.ARE_31 = emp.ARE_31;
                    json.ARE_32 = emp.ARE_32;
                    json.ARE_33 = emp.ARE_33;
                    json.ARE_34 = emp.ARE_34;
                    json.ARE_35 = emp.ARE_35;
                    json.ARE_36 = emp.ARE_36;
                    json.ARE_37 = emp.ARE_37;
                    json.ARE_38 = emp.ARE_38;
                    json.ARE_39 = emp.ARE_39;
                    json.ARE_40 = emp.ARE_40;

                    json.ARE_41 = emp.ARE_41;
                    json.ARE_42 = emp.ARE_42;
                    json.ARE_43 = emp.ARE_43;
                    json.ARE_44 = emp.ARE_44;
                    json.ARE_45 = emp.ARE_45;
                    json.ARE_46 = emp.ARE_46;
                    json.ARE_47 = emp.ARE_47;
                    json.ARE_48 = emp.ARE_48;
                    json.ARE_49 = emp.ARE_49;
                    json.ARE_50 = emp.ARE_50;
                    json.ARE_51 = emp.ARE_51;
                    json.ARE_52 = emp.ARE_52;
                    json.ARE_53 = emp.ARE_53;
                    json.ARE_54 = emp.ARE_54;
                    json.ARE_55 = emp.ARE_55;
                    json.ARE_56 = emp.ARE_56;
                    json.ARE_57 = emp.ARE_57;
                    json.ARE_58 = emp.ARE_58;
                    json.ARE_59 = emp.ARE_59;
                    json.ARE_60 = emp.ARE_60;

                    json.ARE_61 = emp.ARE_61;
                    json.ARE_62 = emp.ARE_62;
                    json.ARE_63 = emp.ARE_63;
                    json.ARE_64 = emp.ARE_64;
                    json.ARE_65 = emp.ARE_65;
                    json.ARE_66 = emp.ARE_66;
                    json.ARE_67 = emp.ARE_67;
                    json.ARE_68 = emp.ARE_68;
                    json.ARE_69 = emp.ARE_69;
                    json.ARE_70 = emp.ARE_70;
                    json.ARE_71 = emp.ARE_71;
                    json.ARE_72 = emp.ARE_72;
                    json.ARE_73 = emp.ARE_73;
                    json.ARE_74 = emp.ARE_74;
                    json.ARE_75 = emp.ARE_75;
                    json.ARE_76 = emp.ARE_76;
                    json.ARE_77 = emp.ARE_77;
                    json.ARE_78 = emp.ARE_78;
                    json.ARE_79 = emp.ARE_79;
                    json.ARE_80 = emp.ARE_80;

                    json.ARE_81 = emp.ARE_81;
                    json.ARE_82 = emp.ARE_82;
                    json.ARE_83 = emp.ARE_83;
                    json.ARE_84 = emp.ARE_84;
                    json.ARE_85 = emp.ARE_85;
                    json.ARE_86 = emp.ARE_86;


                    json.ESPECIAL_OPEN = emp.ESPECIAL_OPEN;
                    json.CAMBIAR_OPEN = emp.CAMBIAR_OPEN;
                    json.FORT_JEFE = emp.FORT_JEFE;
                    json.OPORT_JEFE = emp.OPORT_JEFE;
                    json.MOTIVA_TRAB = emp.MOTIVA_TRAB;
                    json.DEJAR_EMP = emp.DEJAR_EMP;
                    json.PRESION = emp.PRESION;
                    json.ANTIGUEDADR = emp.ANTIGUEDAD;
                    json.RANGO_EDADR = emp.RANGO_EDAD;
                    json.CONDICIONR = emp.CONDICION;
                    json.SEXOR = emp.SEXO;
                    json.ACADEMICOR = emp.ACADEMICO;
                    json.FUNCIONR = emp.FUNCION;
                    json.UNIDADRES = emp.UNIDAD;
                    json.DIRECIONRES = emp.DIRECION;
                    json.AREARES = emp.AREA;
                    json.DEPARTAMENTORES = emp.DEPARTAMENTO;


                    jsonList.Add(json);//El nuevo valor sobreescrbe al anterior quedando n cantidda de veces el ultimo
                }

            }
            var data = JsonConvert.SerializeObject(jsonList);

            return Content(data);
        }


        //Llenar DDL por tipo 2
        public JsonResult GetCompanyAjaxReporte(string id)
        {
            var result = BL.Company.GetByCompanyCategoriaReporte(id);

            List<SelectListItem> Companies = new List<SelectListItem>();

            Companies.Add(new SelectListItem { Text = "Selecciona una opcion", Value = "0" });

            if (result.Objects != null)
            {
                foreach (ML.Company company in result.Objects)
                {
                    Companies.Add(new SelectListItem { Text = company.CompanyName.ToString(), Value = company.CompanyId.ToString() });



                }

            }

            return Json(new SelectList(Companies, "Value", "Text", JsonRequestBehavior.AllowGet));
        }

        public JsonResult GetAreaAjaxReporte(string id)
        {
            var result = BL.Area.AreaGetByCompanyIdReporte(id);

            List<SelectListItem> Areas = new List<SelectListItem>();

            Areas.Add(new SelectListItem { Text = "Selecciona una opcion", Value = "0" });

            if (result.Objects != null)
            {
                foreach (ML.Area area in result.Objects)
                {
                    Areas.Add(new SelectListItem { Text = area.Nombre.ToString(), Value = area.IdArea.ToString() });
                }
            }
            return Json(new SelectList(Areas, "Value", "Text", JsonRequestBehavior.AllowGet));
        }

        public JsonResult GetDepartamentoAjaxReporte(string id)
        {
            var result = BL.Departamento.GetByAreaReporte(id);

            List<SelectListItem> Departamentos = new List<SelectListItem>();

            Departamentos.Add(new SelectListItem { Text = "Selecciona una opcion", Value = "0" });

            if (result.Objects != null)
            {
                foreach (ML.Departamento departamento in result.Objects)
                {
                    Departamentos.Add(new SelectListItem { Text = departamento.Nombre.ToString(), Value = departamento.IdDepartamento.ToString() });
                }
            }
            return Json(new SelectList(Departamentos, "Value", "Text", JsonRequestBehavior.AllowGet));
        }


        
        public ActionResult GetRespuestasByIdEmpleado(int id)
        {
            ML.EmpleadoRespuesta empRes = new ML.EmpleadoRespuesta();
            empRes.Empleado = new ML.Empleado();
            empRes.Empleado.IdEmpleado = id;
            var result = BL.Respuestas.GetRespuestasCLByEmpleado(empRes);

            if (result.Objects.Count == 0)
            {
                Session["progreso"] = 0;
            }
            if (result.Objects.Count == 16)
            {
                Session["progreso"] = 8;
            }
            if (result.Objects.Count == 32)
            {
                Session["progreso"] = 16;
            }
            if (result.Objects.Count == 48)
            {
                Session["progreso"] = 24;
            }
            if (result.Objects.Count == 64)
            {
                Session["progreso"] = 32;
            }
            if (result.Objects.Count == 80)
            {
                Session["progreso"] = 40;
            }
            if (result.Objects.Count == 96)
            {
                Session["progreso"] = 48;
            }
            if (result.Objects.Count == 112)
            {
                Session["progreso"] = 56;
            }
            if (result.Objects.Count == 128)
            {
                Session["progreso"] = 64;
            }
            if (result.Objects.Count == 144)
            {
                Session["progreso"] = 72;
            }
            if (result.Objects.Count == 160)
            {
                Session["progreso"] = 80;
            }
            if (result.Objects.Count == 172)
            {
                Session["progreso"] = 86;
            }
            if (result.Objects.Count == 179)
            {
                Session["progreso"] = 93;
            }

            return Json("success");
            
        }



        [HttpPost]
        public ActionResult Autenticar(ML.Empleado empleado)
        {
            Session["RespuestasP1"] = null;
            Session["RespuestasP2"] = null;
            Session["RespuestasP3"] = null;
            Session["RespuestasP4"] = null;
            Session["RespuestasP5"] = null;
            Session["RespuestasP6"] = null;
            Session["RespuestasP7"] = null;
            Session["RespuestasP8"] = null;
            Session["RespuestasP9"] = null;
            Session["RespuestasP9A"] = null;
            Session["RespuestasP9B"] = null;
            Session["RespuestasP10"] = null;
            Session["EmpleadoEncuestado"] = null;


            var result = BL.Login.Autenticacion(empleado);//Validar que la persona existe

            if (result.Correct == false)
            {
                return Content(result.ErrorMessage);
            }

            Session["EmpleadoEncuestado"] = empleado.IdEmpleado;
            Session["EncuestaRealizar"] = 1;
            ML.EstatusEncuesta estatusEncuesta = new ML.EstatusEncuesta();
            estatusEncuesta.Empleado = new ML.Empleado();
            estatusEncuesta.Empleado.IdEmpleado = empleado.IdEmpleado;
            estatusEncuesta.Encuesta = new ML.Encuesta();
            estatusEncuesta.Encuesta.IdEncuesta = 1;

            //Desde este punto poner el estatus, en proceso
            try
            {
                if (DateTime.Now < result.InicioCL)
                {
                    ViewBag.MensajeLogin = "La encuesta aun no esta abierta. Puedes responderla a partir del día " + result.InicioCL;
                    empleado.MensajeEmpleado = "NoStart";
                    return Json("NoStart");
                }
                if (DateTime.Now > result.FinCL)
                {
                    ViewBag.MensajeLogin = "El periodo de aplicación de la encuesta ha terminado";
                    empleado.MensajeEmpleado = "AppEnd";
                    return Json("AppEnd");
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }



            //Validar que el estatus sea cero, si es uno ya no puedes hacerla
            var result2 = BL.Login.AutenticacionEstatus(estatusEncuesta);
            string Estatus = estatusEncuesta.Estatus;

            if (result.Correct == true && Estatus == "En proceso")//Tengo acceso y estatus es cero
            {
                Session["mensaje"] = "Success";
                return Json(Session["mensaje"]);
                //return View("Inicio");
            }
            else if (result.Correct == true && Estatus == "Terminada")//Tengo acceso y el estatus es uno
            {
                Session["mensaje"] = "Ya has realizado la encuesta!";
                return Json(Session["mensaje"]);//En ves de modal que sea Swal
                //ViewBag.Message = "Ya has realizado la encuesta. Gracias!";
                //return PartialView("Modal");
            }
            else if (result.Correct == false)//No tengo acceso
            {
                Session["mensaje"] = result.ErrorMessage;
                //Clave no valida
                return Json(Session["mensaje"]);//En vez de modal swal
                //ViewBag.Message = "Clave de acceso no válida!";
                //return PartialView("Modal");
            }
            else//Donde si existe la persona pero no hay ningun estatus
            {
                //Agrear Estatus no iniciada
                var resultadoEstatus = BL.Empleado.EstatusEncuestaAdd(estatusEncuesta);
                Session["mensaje"] = "Success";
                return Json(Session["mensaje"]);
            }
        }

    }
}