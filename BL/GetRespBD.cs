using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class GetRespBD
    {
        public static List<string> GetResP1(ML.EmpleadoRespuesta EmpleadoRespuestas)
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
            ////ViewBag.Answers1 = RespuestaFinal;
            return (RespuestaFinal);
        }
        public static List<string> GetResP2(ML.EmpleadoRespuesta EmpleadoRespuestas)
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
            ////Bag.Answers2 = Respuesta1Final;
            return(Respuesta1Final);
        }
        public static List<string> GetResP3(ML.EmpleadoRespuesta EmpleadoRespuestas)
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
            //Bag.Answers3 = Respuesta1Final;
            return(Respuesta1Final);
        }
        public static List<string> GetResP4(ML.EmpleadoRespuesta EmpleadoRespuestas)
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
            //Bag.Answers4 = Respuesta1Final;
            return (Respuesta1Final);
        }
        public static List<string> GetResP5(ML.EmpleadoRespuesta EmpleadoRespuestas)
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
            //Bag.Answers5 = Respuesta1Final;
            return (Respuesta1Final);
        }
        public static List<string> GetResP6(ML.EmpleadoRespuesta EmpleadoRespuestas)
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
            //Bag.Answers6 = Respuesta1Final;
            return (Respuesta1Final);
        }
        public static List<string> GetResP7(ML.EmpleadoRespuesta EmpleadoRespuestas)
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
            //Bag.Answers7 = Respuesta1Final;
            return (Respuesta1Final);
        }
        public List<string> GetResP8(ML.EmpleadoRespuesta EmpleadoRespuestas)
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
            //Bag.Answers8 = Respuesta1Final;
            return (Respuesta1Final);
        }
        public List<string> GetResP9(ML.EmpleadoRespuesta EmpleadoRespuestas)
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
            //Bag.Answers9 = Respuesta1Final;
            return (Respuesta1Final);
        }
        public List<string> GetResP9A(ML.EmpleadoRespuesta EmpleadoRespuestas)
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
            //Bag.Answers9A = Respuesta1Final;
            return (Respuesta1Final);
        }
        public List<string> GetResP9B(ML.EmpleadoRespuesta EmpleadoRespuestas)
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

            //Bag.Answers9B = Respuesta1Final;
            return (Respuesta1Final);
        }
        public List<string> GetResP10(ML.EmpleadoRespuesta EmpleadoRespuestas)
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


            //Bag.Answers10 = Respuesta1Final;
            return (Respuesta1Final);
        }
    }
}
