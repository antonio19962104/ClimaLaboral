using System;
using ML;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace BL
{
    public class Preguntas
    {
        public static Result getEnfoquePregunta() {
            Result result = new Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var IdParametro = 1;
                    //var query = context.Preguntas.SqlQuery("SELECT * FROM Preguntas  where IdEstatus = {0}",IdParametro);                    
                    var query = context.Preguntas.Select(m => new { m.Enfoque}).Distinct();
                    result.ListadoEnfoquesPregunta = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Preguntas enfoques = new ML.Preguntas();
                            enfoques.Enfoque = obj.Enfoque;
                           // enfoques.IdPregunta = obj.IdPregunta;//No se ocupa de referencia, solo ocupamos los textos CAMOS 10/03/20                             
                            result.ListadoEnfoquesPregunta.Add(enfoques);
                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.Correct = false; BL.LogReporteoClima.writeLogEncuestaClima("No se encontraron Enfoques de Preguntas", new StackTrace());
                        result.ErrorMessage = "No se encontraron Enfoques de Preguntas";
                    }
                }
            }
            catch (Exception aE)
            {
                result.Correct = false; BL.LogReporteoClima.writeLogEncuestaClima(aE, new StackTrace());
                result.ErrorMessage = aE.Message.ToString();
            }
            return result;
        }
        public static IList<ML.Preguntas> getAllPreguntasByIdEncuesta(int idEncuesta, string idUsuarioAdmin) {
            try {
                IList<ML.Preguntas> result = new List<ML.Preguntas>();
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var IdParametro = 1;
                    var query = context.Preguntas.SqlQuery("SELECT * FROM Preguntas "+
                        " where IdEstatus = {0} and idEncuesta = {1}",IdParametro,idEncuesta);
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Preguntas pregunta = new ML.Preguntas();
                            var listadoTipoControl = BL.TipoControl.getTipoControl();
                            var listadoCompetenciaPreguntas = BL.Competencia.getCompetencias(idUsuarioAdmin);
                            var listadoEnfoquePregunta = BL.Preguntas.getEnfoquePregunta();
                            pregunta.ListTipoControl = listadoTipoControl.ListadoTipoControl;
                            pregunta.ListCompetencia = listadoCompetenciaPreguntas.ListadoCompetenciasPregunta;
                            pregunta.ListEnfoque = listadoEnfoquePregunta.ListadoEnfoquesPregunta;
                            pregunta.UniqueId = Guid.NewGuid();
                            pregunta.Pregunta = obj.Pregunta;
                            pregunta.Seccion = Convert.ToInt32(obj.Seccion);
                            pregunta.Encabezado = obj.EncabezadoSeccion;                    
                            pregunta.IdPregunta = obj.IdPregunta;
                            pregunta.IdEncuesta = idEncuesta;
                            pregunta.TipoEstatus = new ML.TipoEstatus();
                            pregunta.TipoEstatus.IdEstatus = obj.IdEstatus;
                            pregunta.TipoControl = new ML.TipoControl();
                            pregunta.TipoControl.IdTipoControl = Convert.ToInt32(obj.IdTipoControl);
                            pregunta.Valoracion = obj.Valoracion;
                            pregunta.RespuestaCondicion = obj.RespuestaCondicion;
                            pregunta.PreguntasCondicion = obj.PreguntasCondicion;
                            var queryRespuestas = context.Respuestas.SqlQuery("SELECT *  FROM  Respuestas "+
                                "  where IdPregunta = {0} and IdEstatus = {1}",obj.IdPregunta,IdParametro);
                            IList<ML.Respuestas> resulRespuestas = new List<ML.Respuestas>();
                            if (queryRespuestas != null)
                            {
                                foreach (var objR in queryRespuestas)
                                {
                                    ML.Respuestas respuesta = new ML.Respuestas();
                                    respuesta.IdRespuesta = objR.IdRespuesta;
                                    respuesta.Pregunta = new ML.Preguntas();
                                    respuesta.Pregunta.TipoControl = new ML.TipoControl();
                                    respuesta.Pregunta.TipoControl.IdTipoControl = Convert.ToInt32(obj.IdTipoControl);
                                    respuesta.Respuesta = objR.Respuesta;
                                    respuesta.IdPadreObjeto = "NewCuestion["+pregunta.UniqueId.ToString()+"]";

                                    resulRespuestas.Add(respuesta);
                                }
                            }
                            pregunta.NewAnswer = resulRespuestas;



                            result.Add(pregunta);
                        }
                        return result;
                    }
                    else
                    { }
                   
                }

            }
            catch (Exception aE)
            {
                return null;
            }         
            return null;
        }

        public static IList<ML.Preguntas> getAllPreguntasByIdEncuestaEdit(int idEncuesta,string idUsuarioAdmin)
        {
            try
            {
                IList<ML.Preguntas> result = new List<ML.Preguntas>();
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var IdParametro = 1;
                    var query = context.Preguntas.SqlQuery("SELECT * FROM Preguntas " +
                        " where IdEstatus = {0} and idEncuesta = {1}", IdParametro, idEncuesta);
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Preguntas pregunta = new ML.Preguntas();
                            var listadoTipoControl = BL.TipoControl.getTipoControl();
                            var listadoCompetenciaPreguntas = BL.Competencia.getCompetencias(idUsuarioAdmin);
                            var listadoEnfoquePregunta = BL.Preguntas.getEnfoquePregunta();
                            pregunta.ListTipoControl = listadoTipoControl.ListadoTipoControl;
                            pregunta.ListCompetencia = listadoCompetenciaPreguntas.ListadoCompetenciasPregunta;
                            pregunta.ListEnfoque = listadoEnfoquePregunta.ListadoEnfoquesPregunta;
                            pregunta.UniqueId = Guid.NewGuid();
                            pregunta.Pregunta = obj.Pregunta;
                            pregunta.IdPregunta = obj.IdPregunta;
                            pregunta.IdEncuesta = idEncuesta;
                            pregunta.IdCompetencia = (Int32)obj.IdCompetencia;
                            pregunta.Obligatoria = Convert.ToBoolean(obj.Obligatoria);
                            pregunta.TipoEstatus = new ML.TipoEstatus();
                            pregunta.TipoEstatus.IdEstatus = obj.IdEstatus;
                            pregunta.TipoControl = new ML.TipoControl();
                            pregunta.TipoControl.IdTipoControl = Convert.ToInt32(obj.IdTipoControl);
                            pregunta.Valoracion = obj.Valoracion;
                            pregunta.RespuestaCondicion = obj.RespuestaCondicion;
                            pregunta.PreguntasCondicion = obj.PreguntasCondicion;
                            IList<ML.Respuestas> resulRespuestas = new List<ML.Respuestas>();
                            if (Convert.ToInt32(obj.IdTipoControl) != 12)
                            {
                                var queryRespuestas = context.Respuestas.SqlQuery("SELECT top 20 *  FROM  Respuestas " +
                                "  where IdPregunta = {0} and IdEstatus = {1}", obj.IdPregunta, IdParametro);
                                if (queryRespuestas != null)
                                {
                                    foreach (var objR in queryRespuestas)
                                    {
                                        ML.Respuestas respuesta = new ML.Respuestas();
                                        respuesta.IdRespuesta = objR.IdRespuesta;
                                        respuesta.Pregunta = new ML.Preguntas();
                                        respuesta.Pregunta.TipoControl = new ML.TipoControl();
                                        respuesta.Pregunta.TipoControl.IdTipoControl = Convert.ToInt32(obj.IdTipoControl);
                                        respuesta.Pregunta.IdPregunta = obj.IdPregunta;
                                        respuesta.Respuesta = objR.Respuesta;
                                        respuesta.IdPadreObjeto = "NewCuestionEdit[" + obj.IdPregunta.ToString()+ "]";
                                        respuesta.TipoEstatus = new TipoEstatus();
                                        respuesta.TipoEstatus.IdEstatus = objR.IdEstatus;
                                        resulRespuestas.Add(respuesta);
                                    }
                                }
                            }
                            else
                            {
                                //var selectDisRespuesta = context.Respuestas.Select(m => m.Respuesta).Distinct();                                
                                var queryRespuestasLD = context.Respuestas.SqlQuery("SELECT  top 2 *  FROM  Respuestas " +
                                "  where IdPregunta = {0} and IdEstatus = {1}", obj.IdPregunta, IdParametro);

                                if (queryRespuestasLD != null)
                                {
                                    foreach (var objR in queryRespuestasLD)
                                    {
                                        ML.Respuestas respuesta = new ML.Respuestas();
                                        respuesta.IdRespuesta = objR.IdRespuesta;
                                        respuesta.Pregunta = new ML.Preguntas();
                                        respuesta.Pregunta.TipoControl = new ML.TipoControl();
                                        respuesta.Pregunta.TipoControl.IdTipoControl = Convert.ToInt32(obj.IdTipoControl);
                                        respuesta.Pregunta.IdPregunta = obj.IdPregunta;
                                        respuesta.Respuesta = objR.Respuesta;
                                        respuesta.Verdadero = 1;
                                        respuesta.IdPadreObjeto = "NewCuestionEdit[" + obj.IdPregunta.ToString() + "]";

                                        resulRespuestas.Add(respuesta);
                                    }

                                    var queryRespuestasLDL = context.PreguntasLikert.SqlQuery("SELECT * FROM  PreguntasLikert " +
                               "  where IdPregunta = {0} and IdEstatus = {1} and idEncuesta ={2}", obj.IdPregunta, IdParametro, obj.idEncuesta);
                                    if (queryRespuestasLDL != null)
                                    {
                                        foreach (var objRL in queryRespuestasLDL)
                                        {
                                            ML.Respuestas respuesta = new ML.Respuestas();
                                            respuesta.IdRespuesta = objRL.idPreguntasLikert;
                                            respuesta.Pregunta = new ML.Preguntas();
                                            respuesta.Pregunta.TipoControl = new ML.TipoControl();
                                            respuesta.Pregunta.TipoControl.IdTipoControl = Convert.ToInt32(obj.IdTipoControl);
                                            respuesta.Pregunta.IdPregunta = obj.IdPregunta;
                                            respuesta.Respuesta = objRL.Pregunta;
                                            respuesta.Verdadero = 2;
                                            respuesta.IdPadreObjeto = "NewCuestionEdit[" + obj.IdPregunta.ToString() + "]";

                                            resulRespuestas.Add(respuesta);
                                        }


                                    }
                                }
                            }


                            pregunta.NewAnswerEdit = resulRespuestas;
                            result.Add(pregunta);
                        }
                        return result;
                    }
                    else
                    { }

                }

            }
            catch (Exception aE)
            {
                return null;
            }
            return null;
        }
        public static List<ML.Preguntas> getPreguntasByIdEncuesta(int idEncuesta)
        {
            try {
                List<ML.Preguntas> result = new List<ML.Preguntas>();               
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var IdParametro = 1;
                    var query = context.Preguntas.SqlQuery("SELECT * FROM Preguntas " +
                        " where IdEstatus = {0} and idEncuesta = {1}", IdParametro, idEncuesta);
                    if (query != null)
                    {
                        int numPregunta = 1;
                        foreach (var obj in query)
                        {

                            ML.Preguntas pregunta = new ML.Preguntas();
                            pregunta.Pregunta = obj.Pregunta;
                            pregunta.Obligatoria = Convert.ToBoolean(obj.Obligatoria);
                            pregunta.Seccion = Convert.ToInt32(obj.Seccion);
                            pregunta.SubSeccion = obj.SubSeccion != null ? (int)obj.SubSeccion : 0;
                            pregunta.Encabezado = obj.EncabezadoSeccion;
                            pregunta.IdPregunta = obj.IdPregunta;
                            var consultaConfig = BL.ConfiguraRespuesta.getAllByIdEncuesta(idEncuesta, obj.IdPregunta);
                            pregunta.isDisplay = consultaConfig.Correct;
                            pregunta.Obligatoria = (bool)obj.Obligatoria;
                            
                            pregunta.TipoEstatus = new ML.TipoEstatus();
                            pregunta.TipoEstatus.IdEstatus = obj.IdEstatus;
                            pregunta.TipoControl = new ML.TipoControl();
                            pregunta.TipoControl.IdTipoControl = obj.IdTipoControl;
                            if (pregunta.TipoControl.IdTipoControl == 13 && numPregunta > 1)
                            {
                                pregunta.NumeroPregunta = numPregunta - 1;
                            }

                            if (pregunta.TipoControl.IdTipoControl == 5)
                            {
                                Console.WriteLine("");
                            }

                            pregunta.Valoracion = obj.Valoracion;
                            pregunta.RespuestaCondicion = obj.RespuestaCondicion;
                            pregunta.PreguntasCondicion = obj.PreguntasCondicion;
                            List<ML.Respuestas> resulRespuestas = new List<ML.Respuestas>();
                            List<object> resResp = new List<object>();
                            var queryRespuestas = context.Respuestas.SqlQuery("SELECT *  FROM  Respuestas " +
                                "  where IdPregunta = {0} and IdEstatus = {1}", obj.IdPregunta, IdParametro);

                                if (queryRespuestas != null)
                                {
                                    foreach (var objR in queryRespuestas)
                                    {
                                        ML.Respuestas respuesta = new ML.Respuestas();
                                        respuesta.IdRespuesta = objR.IdRespuesta;
                                    respuesta.Respuesta = objR.Respuesta;
                                        respuesta.Pregunta = new ML.Preguntas();
                                        respuesta.Selected = Convert.ToBoolean(objR.Selected);
                                        respuesta.Pregunta.TipoControl = new ML.TipoControl();
                                        respuesta.Pregunta.TipoControl.IdTipoControl = obj.IdTipoControl;
                                    if (obj.IdTipoControl != 12) {
                                        respuesta.Pregunta.Pregunta = obj.Pregunta;
                                    }
                                    else {
                                        var consultaPreguntaLikert = context.PreguntasLikert.SqlQuery("SELECT * FROM PreguntasLikert where idPregunta = {0} and  idPreguntasLikert = {1}",obj.IdPregunta,objR.IdPreguntaLikertD).Distinct().ToList();
                                        if (consultaPreguntaLikert.Count > 0)
                                        {
                                            foreach (DL.PreguntasLikert objPregunta in consultaPreguntaLikert)
                                            {
                                                respuesta.Pregunta.Pregunta = objPregunta.Pregunta;
                                                respuesta.Pregunta.IdPregunta = objPregunta.idPreguntasLikert;
                                            }
                                        }
                                       
                                    }
                                    

                                        if (obj.IdTipoControl == 1 || obj.IdTipoControl == 2 || obj.IdTipoControl == 6 || obj.IdTipoControl == 7 || obj.IdTipoControl == 8 || obj.IdTipoControl == 9 || obj.IdTipoControl == 10 || obj.IdTipoControl == 11)
                                        {
                                            respuesta.Respuesta = "";
                                        }
                                        else
                                        {
                                            respuesta.Respuesta = objR.Respuesta;
                                        }
                                        respuesta.UsuarioRespuestas = new ML.UsuarioRespuestas();
                                        respuesta.UsuarioRespuestas.RespuestaUsuario = "";
                                        //respuesta.UsuarioRespuestas.Respuestas = ;
                                        resulRespuestas.Add(respuesta);
                                        resResp.Add(respuesta);
                                    }
                                }
                                                       

                            
                            pregunta.Respuestas = resulRespuestas;
                            pregunta.ListadoRespuestas = resResp;
                            result.Add(pregunta);
                            numPregunta++;
                        }
                        return result;
                    }
                }
            }
            catch (Exception aE)
            {
                return null;
            }
            return null;
        }
        public static ML.Result getPreguntasByIdEncuestaConfigura(int idEncuesta,int idPregunta)
        {
            try
            {
                ML.Result result = new ML.Result();
                result.Objects = new List<object>();
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var IdParametro = 1;
                    var query = context.Preguntas.SqlQuery("SELECT * FROM Preguntas " +
                        " where IdEstatus = {0} and idEncuesta = {1} and idPregunta <> {2} and idPregunta > {2} and idPregunta NOT IN (SELECT Distinct IdPregunta FROM ConfiguraRespuesta where IdEncuesta = {1})", IdParametro, idEncuesta,idPregunta);
                    if (query != null)
                    {
                        int numPregunta = 1;
                        foreach (var obj in query)
                        {
                            ML.Preguntas pregunta = new ML.Preguntas();
                            pregunta.Pregunta = obj.Pregunta;
                            pregunta.IdPregunta = obj.IdPregunta;
                            pregunta.TipoControl = new ML.TipoControl();
                            pregunta.TipoControl.IdTipoControl = obj.IdTipoControl;
                            pregunta.Seccion = (int)obj.Seccion;
                            //pregunta.NumeroPregunta = numPregunta;
                            //pregunta.TipoEstatus = new ML.TipoEstatus();
                            //pregunta.TipoEstatus.IdEstatus = obj.IdEstatus;
                            //pregunta.TipoControl = new ML.TipoControl();
                            //pregunta.TipoControl.IdTipoControl = obj.IdTipoControl;
                            //pregunta.Valoracion = obj.Valoracion;
                            //pregunta.RespuestaCondicion = obj.RespuestaCondicion;
                            //pregunta.PreguntasCondicion = obj.PreguntasCondicion;                                                       
                            result.Objects.Add(pregunta);
                            numPregunta++;
                        }
                        return result;
                    }
                }
            }
            catch (Exception aE)
            {
                return null;
            }
            return null;
        }

        public static ML.Result getPreguntasByIdEncuestaConfiguraForEdit(int idEncuesta, int idPregunta)
        {
            try
            {
                ML.Result result = new ML.Result();
                result.Objects = new List<object>();
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var IdParametro = 1;
                    var query = context.Preguntas.SqlQuery("SELECT * FROM Preguntas " +
                        " where IdEstatus = {0} and idEncuesta = {1} and idPregunta <> {2} and idPregunta > {2}", IdParametro, idEncuesta, idPregunta);
                    if (query != null)
                    {
                        int numPregunta = 1;
                        foreach (var obj in query)
                        {
                            ML.Preguntas pregunta = new ML.Preguntas();
                            pregunta.Pregunta = obj.Pregunta;
                            pregunta.IdPregunta = obj.IdPregunta;
                            pregunta.Seccion = (int) obj.Seccion;
                            pregunta.TipoControl = new ML.TipoControl();
                            pregunta.TipoControl.IdTipoControl = obj.IdTipoControl;
                            //pregunta.NumeroPregunta = numPregunta;
                            //pregunta.TipoEstatus = new ML.TipoEstatus();
                            //pregunta.TipoEstatus.IdEstatus = obj.IdEstatus;
                            //pregunta.TipoControl = new ML.TipoControl();
                            //pregunta.TipoControl.IdTipoControl = obj.IdTipoControl;
                            //pregunta.Valoracion = obj.Valoracion;
                            //pregunta.RespuestaCondicion = obj.RespuestaCondicion;
                            //pregunta.PreguntasCondicion = obj.PreguntasCondicion;                                                       
                            result.Objects.Add(pregunta);
                            numPregunta++;
                        }
                        return result;
                    }
                }
            }
            catch (Exception aE)
            {
                return null;
            }
            return null;
        }
        public static ML.Result getConfByRespuesta(int IdEncuesta, int IdRespuesta)
        {
            ML.Result result = new Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.ConfiguraRespuesta.SqlQuery("SELECT * FROM ConfiguraRespuesta WHERE idencuesta = {0} and idrespuesta = {1}", IdEncuesta, IdRespuesta).ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.ConfiguraRespuesta conf = new ML.ConfiguraRespuesta();

                            conf.IdRespuesta = Convert.ToInt32(item.IdRespuesta);
                            conf.IdPreguntaOpen = Convert.ToInt32(item.IdPreguntaOpen);

                            result.Objects.Add(conf);
                            result.Correct = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; BL.LogReporteoClima.writeLogEncuestaClima(ex, new StackTrace());
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result getConfByIdEncuesta(int IdEncuesta)
        {
            ML.Result result = new Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.ConfiguraRespuesta.SqlQuery("SELECT * FROM ConfiguraRespuesta WHERE idencuesta = {0}", IdEncuesta).ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.ConfiguraRespuesta conf = new ML.ConfiguraRespuesta();

                            conf.IdRespuesta = Convert.ToInt32(item.IdRespuesta);
                            conf.IdPreguntaOpen = Convert.ToInt32(item.IdPreguntaOpen);
                            conf.TerminaEncuesta = Convert.ToInt32(item.TerminaEncuesta);

                            var getPregunta = context.Preguntas.SqlQuery("SELECT * FROM PREGUNTAs WHERE IDPREGUNTA = {0}", conf.IdPreguntaOpen).ToList();
                            foreach (var elem in getPregunta)
                            {
                                conf.itemPreguntaOpen = elem.Pregunta;
                            }

                            result.Objects.Add(conf);
                            result.Correct = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; BL.LogReporteoClima.writeLogEncuestaClima(ex, new StackTrace());
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetPreguntasBydIdEncuesta(int IdEncuesta)
        {
            ML.Result result = new Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Preguntas.SqlQuery("SELECT * FROM PREGUNTAS WHERE IDENCUESTA = {0}", IdEncuesta).ToList();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Preguntas preguntas = new ML.Preguntas();

                            preguntas.Pregunta = item.Pregunta;

                            result.Objects.Add(preguntas);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; BL.LogReporteoClima.writeLogEncuestaClima(ex, new StackTrace());
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result getReactivos()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Preguntas.SqlQuery("SELECT * FROM PREGUNTAS WHERE IDENCUESTA = 1 AND IDPREGUNTA BETWEEN 1 AND 86").ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Preguntas pregunta = new ML.Preguntas();
                            pregunta.IdPregunta = item.IdPregunta;
                            pregunta.Pregunta = item.Pregunta;

                            result.Objects.Add(pregunta);
                        }
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; BL.LogReporteoClima.writeLogEncuestaClima(ex, new StackTrace());
                result.ErrorMessage = ex.Message;
                var st = new StackTrace();
                var ft = st.GetFrame(0);
                BL.LogReporteoClima.writteLog(result.ErrorMessage, new StackTrace());
            }
            return result;
        }
        public static IList<ML.Preguntas> getAllPreguntasByIdEncuestaOrden(int idEncuesta)
        {
            try
            {
                IList<ML.Preguntas> result = new List<ML.Preguntas>();
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var IdParametro = 1;
                    var query = context.Preguntas.SqlQuery("SELECT * FROM Preguntas " +
                        " where IdEstatus = {0} and idEncuesta = {1} order by IdPreguntaPadre", IdParametro, idEncuesta);
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            if (obj.IdEnfoque == 1)
                            {
                                ML.Preguntas pregunta = new ML.Preguntas();
                                pregunta.UniqueId = Guid.NewGuid();
                                pregunta.Pregunta = obj.Pregunta;
                                pregunta.Seccion = Convert.ToInt32(obj.Seccion);
                                pregunta.Encabezado = obj.EncabezadoSeccion;
                                pregunta.IdPregunta = obj.IdPregunta;
                                pregunta.IdPreguntaPadre = (Int32)obj.IdPreguntaPadre;
                                pregunta.IdEncuesta = idEncuesta;
                                pregunta.IdCompetencia = (Int32)obj.IdCompetencia;
                                pregunta.TipoEstatus = new ML.TipoEstatus();
                                pregunta.TipoEstatus.IdEstatus = obj.IdEstatus;
                                //var queryRespuestas = context.Respuestas.SqlQuery("SELECT *  FROM  Respuestas " +
                                //    "  where IdPregunta = {0} and IdEstatus = {1}", obj.IdPregunta, IdParametro);
                                //IList<ML.Respuestas> resulRespuestas = new List<ML.Respuestas>();
                                //if (queryRespuestas != null)
                                //{
                                //    foreach (var objR in queryRespuestas)
                                //    {
                                //        ML.Respuestas respuesta = new ML.Respuestas();
                                //        respuesta.IdRespuesta = objR.IdRespuesta;
                                //        respuesta.Pregunta = new ML.Preguntas();
                                //        respuesta.Pregunta.TipoControl = new ML.TipoControl();
                                //        respuesta.Pregunta.TipoControl.IdTipoControl = Convert.ToInt32(obj.IdTipoControl);
                                //        respuesta.Respuesta = objR.Respuesta;
                                //        respuesta.IdPadreObjeto = "NewCuestion[" + pregunta.UniqueId.ToString() + "]";

                                //        resulRespuestas.Add(respuesta);
                                //    }
                                //}
                                //pregunta.NewAnswer = resulRespuestas;
                                result.Add(pregunta);
                            }
                        }
                        return result;
                    }
                    else
                    { }

                }

            }
            catch (Exception aE)
            {
                return null;
            }
            return null;

        }
        public static ML.Result getPreguntasEditEncuestaCL(int idEncuesta, int idCompetencia)
        {
            Result result = new Result();
            result.ListadoPreguntaCompetencias = new List<ML.Preguntas>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Preguntas.SqlQuery("SELECT * FROM Preguntas where idEncuesta = {0} and IdEstatus = 1 and IdCompetencia = {1}",idEncuesta,idCompetencia).ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            if (item.IdEnfoque == 1)
                            {

                                //preguntaE = item.Pregunta;
                                ML.Preguntas preguntaCompetencia = new ML.Preguntas();
                                preguntaCompetencia.IdPregunta = item.IdPregunta;
                                preguntaCompetencia.IdPreguntaPadre = (Int32)item.IdPreguntaPadre;
                                preguntaCompetencia.IdEnfoque = Convert.ToInt32(item.IdEnfoque);
                                preguntaCompetencia.Pregunta = item.Pregunta;//preguntaE;
                                preguntaCompetencia.Valoracion = item.Valoracion;
                                preguntaCompetencia.Enfoque = item.Enfoque;
                                preguntaCompetencia.Competencia = new ML.Competencia();
                                preguntaCompetencia.Competencia.IdCompetencia = idCompetencia;
                                preguntaCompetencia.Competencia.Nombre =BL.Competencia.getNombreCompetenciabyId(idCompetencia).CURRENT_USER.ToString();
                                preguntaCompetencia.Respuestas = new List<ML.Respuestas>();
                                ML.Respuestas respuestaPC = new ML.Respuestas();
                                respuestaPC.Respuesta = "#RespuestaLibre";
                                respuestaPC.Pregunta = new ML.Preguntas();                                
                                preguntaCompetencia.Respuestas.Add(respuestaPC);
                                result.ListadoPreguntaCompetencias.Add(preguntaCompetencia);
                            }
                        }

                    }
                }
            }
            catch (Exception aE)
            {
                result.Correct = false;
                result.ErrorMessage = aE.Message;                
            }
            return result;
        }
    }
}
