using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class TipoPlantilla
    {
        public static ML.Result getAllTipoPlantilla()
        {
            ML.Result result = new ML.Result();
            try {
                using (DL.RH_DesEntities context= new DL.RH_DesEntities())
                {
                    var IdActivo = 1;
                    var query = context.TipoPlantilla.SqlQuery("SELECT * FROM TipoPlantilla where IdEstatus = {0}",IdActivo);
                    result.ListTipoPlantilla = new List<ML.TipoPlantilla>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.TipoPlantilla tipoPlantilla = new ML.TipoPlantilla();
                            tipoPlantilla.IdTipoPlantilla = obj.IdTipoPlantilla;
                            tipoPlantilla.Nombre = obj.Nombre.ToString();
                            result.ListTipoPlantilla.Add(tipoPlantilla);
                            result.Correct = true;
                        }
                    }
                    else {
                        result.Correct = false;
                        result.ErrorMessage = "No encontramos Tipo de Plantillas";
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
