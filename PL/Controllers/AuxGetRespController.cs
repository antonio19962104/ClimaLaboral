using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class AuxGetRespController : Controller
    {
        // GET: AuxGetResp
        public ActionResult GetResP1(ML.EmpleadoRespuesta EmpleadoRespuestas)
        {
            var getRespuestas = BL.Respuestas.GetRespuestasCLByEmpleado(EmpleadoRespuestas);

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
                RespuestaFinal.Add(Respuesta1[i]);
            }
            ViewBag.Answers1 = RespuestaFinal;
            return View("EncuestaP1");
        }
        public ActionResult GetResP2(ML.EmpleadoRespuesta EmpleadoRespuestas)
        {
            var getRespuestas = BL.Respuestas.GetRespuestasCLByEmpleado(EmpleadoRespuestas);

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
        public ActionResult GetResP3(ML.EmpleadoRespuesta EmpleadoRespuestas)
        {
            var getRespuestas = BL.Respuestas.GetRespuestasCLByEmpleado(EmpleadoRespuestas);

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
        public ActionResult GetResP4(ML.EmpleadoRespuesta EmpleadoRespuestas)
        {
            var getRespuestas = BL.Respuestas.GetRespuestasCLByEmpleado(EmpleadoRespuestas);

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
        public ActionResult GetResP5(ML.EmpleadoRespuesta EmpleadoRespuestas)
        {
            var getRespuestas = BL.Respuestas.GetRespuestasCLByEmpleado(EmpleadoRespuestas);

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
        public ActionResult GetResP6(ML.EmpleadoRespuesta EmpleadoRespuestas)
        {
            var getRespuestas = BL.Respuestas.GetRespuestasCLByEmpleado(EmpleadoRespuestas);

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
        public ActionResult GetResP7(ML.EmpleadoRespuesta EmpleadoRespuestas)
        {
            var getRespuestas = BL.Respuestas.GetRespuestasCLByEmpleado(EmpleadoRespuestas);

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
        public ActionResult GetResP8(ML.EmpleadoRespuesta EmpleadoRespuestas)
        {
            var getRespuestas = BL.Respuestas.GetRespuestasCLByEmpleado(EmpleadoRespuestas);

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
        public ActionResult GetResP9(ML.EmpleadoRespuesta EmpleadoRespuestas)
        {
            var getRespuestas = BL.Respuestas.GetRespuestasCLByEmpleado(EmpleadoRespuestas);

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
        public ActionResult GetResP9A(ML.EmpleadoRespuesta EmpleadoRespuestas)
        {
            var getRespuestas = BL.Respuestas.GetRespuestasCLByEmpleado(EmpleadoRespuestas);

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
        public ActionResult GetResP9B(ML.EmpleadoRespuesta EmpleadoRespuestas)
        {
            var getRespuestas = BL.Respuestas.GetRespuestasCLByEmpleado(EmpleadoRespuestas);

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
        public ActionResult GetResP10(ML.EmpleadoRespuesta EmpleadoRespuestas)
        {
            var getRespuestas = BL.Respuestas.GetRespuestasCLByEmpleado(EmpleadoRespuestas);

            List<string> Respuesta1 = new List<string>();
            List<string> Respuesta1Final = new List<string>();
            Respuesta1Final.Add("ValorDefault");
            Respuesta1.Add("ValorDefault");
            foreach (ML.ResEmpleado obj in getRespuestas.Objects)
            {
                Respuesta1.Add(obj.RespuestaEmpleado);
            }

            for (int i = 173; i < 180; i++)
            {
                Respuesta1Final.Add(Respuesta1[i]);
            }


            ViewBag.Answers10 = Respuesta1Final;
            return View("EncuestaP10");
        }
    }
}