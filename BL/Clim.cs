using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Clim
    {
        public static ML.Result GetAllQuestions()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Preguntas.SqlQuery("SELECT * FROM PREGUNTAS WHERE IDENCUESTA = 1 AND IDPREGUNTA BETWEEN 1 AND 86").ToList();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Preguntas pregunta = new ML.Preguntas();
                            pregunta.IdPregunta = item.IdPregunta;
                            pregunta.Pregunta = item.Pregunta;
                            pregunta.Seccion = Convert.ToInt32(item.Seccion);
                            pregunta.TipoControl = new ML.TipoControl();
                            pregunta.TipoControl.IdTipoControl = item.IdTipoControl;

                            result.Objects.Add(pregunta);
                            result.Correct = true;
                        }
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
        public static ML.Result GetQuestinosOpen()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Preguntas.SqlQuery("SELECT * FROM PREGUNTAS INNER JOIN TIPOCONTROL ON PREGUNTA.IDTIPOCONTROL = TIPOCONTROL.IDTIPOCONTROL WHERE IDPREGUNTA BETWEEN 173 AND 189").ToList();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Preguntas pregunta = new ML.Preguntas();

                            pregunta.IdPregunta = item.IdPregunta;
                            pregunta.Pregunta = item.Pregunta;
                            pregunta.Seccion = Convert.ToInt32(item.Seccion);
                            pregunta.TipoControl = new ML.TipoControl();
                            pregunta.TipoControl.IdTipoControl = item.IdTipoControl;

                            result.Objects.Add(pregunta);
                            result.Correct = true;
                        }
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
