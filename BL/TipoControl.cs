using System;
using ML;
using DL;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL 
{
    public class TipoControl
    {
        public static Result getTipoControl()
        {
            Result result = new Result();
            try
            {
                using (RH_DesEntities context = new RH_DesEntities())
                {
                    var IdParametro = 1;
                    var query = context.TipoControl.SqlQuery("SELECT * FROM TipoControl WHERE IdEstatus = {0} ORDER BY NOMBRE",IdParametro);
                    result.ListadoTipoControl = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.TipoControl controles = new ML.TipoControl();
                            controles.IdTipoControl = obj.IdTipoControl;
                            controles.Nombre = obj.Nombre;
                            result.ListadoTipoControl.Add(controles);
                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encuentra algun Tipo de Control";
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
public static Result getTipoControlCL()
        {
            Result result = new Result();
            try
            {
                using (RH_DesEntities context = new RH_DesEntities())
                {
                    var IdParametro = 1;
                    var query = context.TipoControl.SqlQuery("SELECT * FROM TipoControl where IdTipoControl IN (12,4,5) and IdEstatus = {0} ORDER BY NOMBRE", IdParametro);
                    result.ListadoTipoControl = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.TipoControl controles = new ML.TipoControl();
                            controles.IdTipoControl = obj.IdTipoControl;
                            controles.Nombre = obj.Nombre;
                            result.ListadoTipoControl.Add(controles);
                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encuentra algun Tipo de Control";
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
