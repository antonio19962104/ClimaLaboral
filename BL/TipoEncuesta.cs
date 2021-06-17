using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class TipoEncuesta
    {
        public static ML.Result getAllTipoEncuesta()
        {
            ML.Result result = new ML.Result();
            try {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var IdParametro = 1;
                    var query = context.TipoEncuesta.SqlQuery("SELECT * FROM TipoEncuesta WHERE IdEstatus = {0} ORDER BY NombreTipoDeEncuesta",IdParametro);
                    result.ListadoTipoEncuesta = new List<ML.TipoEncuesta>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.TipoEncuesta tipoEncuestas = new ML.TipoEncuesta();
                            tipoEncuestas.IdTipoEncuesta = obj.IdTipoEncuesta;
                            tipoEncuestas.NombreTipoDeEncuesta = obj.NombreTipoDeEncuesta;
                            tipoEncuestas.IdPadre = Convert.ToInt32(obj.IdPadre);                           
                            tipoEncuestas.TipoEstatus = new ML.TipoEstatus();
                            tipoEncuestas.TipoEstatus.IdEstatus = obj.TipoEstatus.IdEstatus;
                            result.ListadoTipoEncuesta.Add(tipoEncuestas);
                            result.Correct = true;                          
                        }
                    }
                    else {
                        result.Correct = false;
                        result.ErrorMessage = "No encontramos Tipo de Encuesta";
                    }
                }
                    
                }
            catch (Exception aE)
            {
                result.Correct = false;
                result.ErrorMessage = aE.Message.ToString();
            }
            return result;
        }
    }
}
