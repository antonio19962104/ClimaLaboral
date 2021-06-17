using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class ConfiguraRespuesta
    {
        public static ML.Result Add(ML.ConfiguraRespuesta configura, string CURRENTUSER)
        {
            ML.Result result = new ML.Result();

            try
            {
                string cadenaPreguntas = "";
                int bandera = 1;
                foreach (var idres in configura.ListPreguntas)
                {
                    if (bandera == 1)
                    {
                        cadenaPreguntas += " AND IDPREGUNTAOPEN =" + idres.ToString();
                    }
                    else {
                        cadenaPreguntas += " OR IDPREGUNTAOPEN ="+ idres.ToString(); 
                    }
                    bandera++;


                    //cadenaPreguntas += cadenaPreguntas+",";
                }               

                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {

                    //var exist = context.ConfiguraRespuesta.SqlQuery
                    //    ("SELECT * FROM CONFIGURARESPUESTA WHERE IDENCUESTA = {0} " + cadenaPreguntas + "  ", configura.IdEncuesta).ToList();

                    //if (exist.Count > 0)
                    //{
                    //    result.Correct = false;
                    //}
                    //else
                    //{
                    var borrado = context.Database.ExecuteSqlCommand("delete from ConfiguraRespuesta where IdRespuesta = {0} and idencuesta = {1}", configura.IdRespuesta, configura.IdEncuesta);
                    foreach (var idres in configura.ListPreguntas)
                    {
                        var query = context.Database.ExecuteSqlCommand
                        ("INSERT INTO ConfiguraRespuesta (IDENCUESTA, IDPREGUNTA, IDRESPUESTA, IDPREGUNTAOPEN, FECHAHORACREACION, USUARIOCREACION, PROGRAMACREACION) VALUES({0}, {1}, {2}, {3}, {4}, {5}, {6})",
                        configura.IdEncuesta, configura.IdPregunta, configura.IdRespuesta, idres, DateTime.Now, CURRENTUSER, "Diagnostic4U");
                        context.SaveChanges();
                    }
                       
                    result.Correct = true;
                //}

                    
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result getAllByIdEncuesta(int idEncuesta , int idPregunta)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.ConfiguraRespuesta.SqlQuery
                        ("SELECT * FROM CONFIGURARESPUESTA WHERE IDENCUESTA = {0} and IDPREGUNTAOPEN  = {1} ", idEncuesta,idPregunta).ToList();

                    if (query.Count >0 )
                    {
                        result.Correct = true;                                          
                    }
                    else
                    {                        
                        result.Correct = false;
                    }


                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static List<ML.ConfiguraRespuesta> getAllAnswersConfigByIdEncuestIdPregunta(int idEncuesta, int idPregunta)
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.ConfiguraRespuesta.SqlQuery
                        ("SELECT * FROM CONFIGURARESPUESTA WHERE IDENCUESTA = {0} and IdPregunta  = {1} ", idEncuesta, idPregunta).Select(x => x.IdRespuesta).Distinct().ToList();
                    List<ML.ConfiguraRespuesta> listaRespuestas = new List<ML.ConfiguraRespuesta>();
                    if (query.Count > 0)
                    {                       
                        foreach (var item in query)
                        {
                            ML.ConfiguraRespuesta respuesta = new ML.ConfiguraRespuesta();
                            respuesta.IdRespuesta = (int)item.Value;
                            listaRespuestas.Add(respuesta);
                        }
                        return listaRespuestas;
                    }
                    else
                    {
                        return listaRespuestas;
                    }


                }
            }
            catch (Exception ex)
            {
                
               return null;
            }
            //return listaRespuestas;
        }
        public static List<ML.ConfiguraRespuesta> getPregsToDisable(int idEncuesta)
        {
            List<ML.ConfiguraRespuesta> listConf = new List<ML.ConfiguraRespuesta>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var GetConfig3 = context.Preguntas.SqlQuery("select * from Preguntas inner join Respuestas on Preguntas.IdPregunta = Respuestas.IdPregunta where Respuestas.IdRespuesta > (SELECT MAX(IdRespuesta) AS Confi FROM ConfiguraRespuesta where IdEncuesta = {0}  and TerminaEncuesta = 3) and Preguntas.idEncuesta = {0}", idEncuesta);
                    if (GetConfig3.Count() > 0)
                    {
                        var list = GetConfig3.Select(o => o.IdPregunta).Distinct();
                        foreach (var item in list)
                        {
                            ML.ConfiguraRespuesta cnf = new ML.ConfiguraRespuesta();
                            cnf.IdPregunta = item;
                            listConf.Add(cnf);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return listConf;
            }
            return listConf;
        }
        public static bool coinciden(ML.Encuesta EncuestaRespuesta)
        {
            var listRespuestaTermina = GetRespTipo3(EncuestaRespuesta.IdEncuesta);
            var list = EncuestaRespuesta.ListarPreguntas.Where(m => m.TipoControl.IdTipoControl == 4).ToList();
            
            var listIdRespuestasForm = new List<int>();
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[i].Respuestas.Count; j++)
                {
                    for (int k = 0; k < listRespuestaTermina.Count; k++)
                    {
                        if (list[i].Respuestas[j].Selected == true && list[i].Respuestas[j].IdRespuesta == listRespuestaTermina[k])
                        {
                            listIdRespuestasForm.Add(list[i].Respuestas[j].IdRespuesta);
                        }
                    }
                }
            }
            bool coincide = false;
            //listIdRespuestasForm => listRespuestaTermina
            listIdRespuestasForm.Sort();
            listRespuestaTermina.Sort();
            var r = listRespuestaTermina.All(listIdRespuestasForm.Contains);
            if (listRespuestaTermina.All(listIdRespuestasForm.Contains) && listRespuestaTermina.Count > 0)
            {
                coincide = true;
            }
            else
            {
                coincide = false;
            }
            return coincide;
        }
        public static List<int> GetRespTipo3(int IdEncuesta)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            List<int> data = new List<int>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.ConfiguraRespuesta.SqlQuery("SELECT * FROM CONFIGURARESPUESTA WHERE IDENCUESTA = {0} AND TERMINAENCUESTA = 3", IdEncuesta);
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.ConfiguraRespuesta conf = new ML.ConfiguraRespuesta();
                            conf.IdRespuesta = (int) item.IdRespuesta;
                            data.Add(conf.IdRespuesta);
                            result.Objects.Add(conf);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return data;
        }
        public static List<ML.ConfiguraRespuesta> getAllAnswersConfigByIdEncuestIdPregunta(int idEncuesta, int idPregunta,int idRespuest)
        {
            int result = 0;
            List<ML.ConfiguraRespuesta> preguntasBloqueadas = new List<ML.ConfiguraRespuesta>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.ConfiguraRespuesta.SqlQuery
                        ("SELECT * FROM CONFIGURARESPUESTA WHERE IDENCUESTA = {0} and IdPregunta  = {1}  and IdRespuesta = {2}", idEncuesta, idPregunta,idRespuest).ToList();

                    if (query.Count > 0)
                    {                       
                        foreach (DL.ConfiguraRespuesta item in query)
                        {
                            ML.ConfiguraRespuesta preguntas = new ML.ConfiguraRespuesta();
                            preguntas.IdPreguntaOpen = item.IdPreguntaOpen == null ? 0 : (int)item.IdPreguntaOpen;
                            preguntas.TerminaEncuesta = item.TerminaEncuesta == null ? 0 : (int) item.TerminaEncuesta;
                            preguntasBloqueadas.Add(preguntas);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                result = ex.HResult;
            }
            return preguntasBloqueadas;
        }
        public static ML.Result getAllByIdEncuesta(int idEncuesta)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.ConfiguraRespuesta.SqlQuery
                        ("SELECT * FROM CONFIGURARESPUESTA WHERE IDENCUESTA = {0}", idEncuesta).ToList();

                    if (query.Count > 0)
                    {
                        //result.Correct = true;
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.ConfiguraRespuesta confRes = new ML.ConfiguraRespuesta();

                            confRes.IdPregunta = Convert.ToInt32(obj.IdPregunta);
                            confRes.IdPreguntaOpen = Convert.ToInt32(obj.IdPreguntaOpen);
                            confRes.IdRespuesta = Convert.ToInt32(obj.IdRespuesta);

                            result.Objects.Add(confRes);

                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.Correct = false;
                    }


                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result Update(ML.ConfiguraRespuesta configura, string CURRENTUSER)
        {
            ML.Result result = new ML.Result();

            try
            {
                //string cadenaPreguntas = "";
                //int bandera = 1;
                //foreach (var idres in configura.ListPreguntas)
                //{
                //    if (bandera == 1)
                //    {
                //        cadenaPreguntas += " AND IDPREGUNTAOPEN =" + idres.ToString();
                //    }
                //    else
                //    {
                //        cadenaPreguntas += " OR IDPREGUNTAOPEN =" + idres.ToString();
                //    }
                //    bandera++;


                //    //cadenaPreguntas += cadenaPreguntas+",";
                //}
                if (configura.CondicionTermina == 1)
                {
                    using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                    {
                        var delete = context.Database.ExecuteSqlCommand("DELETE FROM ConfiguraRespuesta WHERE IDRESPUESTA = {0}", configura.IdRespuesta);
                        var delete2 = context.Database.ExecuteSqlCommand("DELETE FROM ConfiguraRespuesta WHERE IDRESPUESTA = {0}", configura.IdRespuestaTermina);
                        var query = context.Respuestas.SqlQuery("SELECT * FROM RESPUESTAS WHERE IDRESPUESTA = {0}", configura.IdRespuesta).ToList();
                        int IdPregunta = 0;
                        foreach (var item in query)
                        {
                            IdPregunta = Convert.ToInt32(item.IdPregunta);
                        }
                        var getPreguntasSiguientes = context.Preguntas.SqlQuery("SELECT * FROM PREGUNTAS WHERE IDENCUESTA = {0} AND IDPREGUNTA > {1}", configura.IdEncuesta, IdPregunta).ToList();
                        configura.ListPreguntas = new List<int>();
                        foreach (var elem in getPreguntasSiguientes)
                        {
                            configura.ListPreguntas.Add(elem.IdPregunta);
                        }

                        var existe = context.Database.ExecuteSqlCommand("DELETE FROM ConfiguraRespuesta WHERE IDENCUESTA = {0} AND IDRESPUESTA = {1}",
                                configura.IdEncuesta, configura.IdRespuesta);
                        context.SaveChanges();
                        foreach (var idres in configura.ListPreguntas)
                        {
                            var insert = context.Database.ExecuteSqlCommand
                            ("INSERT INTO ConfiguraRespuesta (IDENCUESTA, IDPREGUNTA, IDRESPUESTA, IDPREGUNTAOPEN, FECHAHORACREACION, USUARIOCREACION, PROGRAMACREACION) VALUES({0}, {1}, {2}, {3}, {4}, {5}, {6})",
                            configura.IdEncuesta, IdPregunta, configura.IdRespuesta, idres, DateTime.Now, CURRENTUSER, "Diagnostic4U");
                            context.SaveChanges();
                            result.Correct = true;
                        }
                        result.Correct = true;
                    }
                }
                else
                {
                    using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                    {
                        var existe = context.Database.ExecuteSqlCommand("DELETE FROM ConfiguraRespuesta WHERE IDENCUESTA = {0} AND IDPREGUNTA = {1} AND IDRESPUESTA = {2}",
                                configura.IdEncuesta, configura.IdPregunta, configura.IdRespuesta);
                        context.SaveChanges();
                        foreach (var idres in configura.ListPreguntas)
                        {
                            var query = context.Database.ExecuteSqlCommand
                            ("INSERT INTO ConfiguraRespuesta (IDENCUESTA, IDPREGUNTA, IDRESPUESTA, IDPREGUNTAOPEN, FECHAHORACREACION, USUARIOCREACION, PROGRAMACREACION) VALUES({0}, {1}, {2}, {3}, {4}, {5}, {6})",
                            configura.IdEncuesta, configura.IdPregunta, configura.IdRespuesta, idres, DateTime.Now, CURRENTUSER, "Diagnostic4U");
                            context.SaveChanges();
                        }

                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
