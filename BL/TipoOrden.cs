using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class TipoOrden
    {
        public static ML.Result getAllTipoOrden() {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.TipoOrden.SqlQuery("SELECT * FROM TipoOrden").ToList();
                    result.ListTipoOrden = new List<ML.TipoOrden>();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.TipoOrden tipoOrden = new ML.TipoOrden();
                            tipoOrden.IdTipoOrden = item.IdTipoOrden;
                            tipoOrden.Descripcion = item.Descripcion;
                            result.ListTipoOrden.Add(tipoOrden);
                            result.Correct = true;
                        }
                    }
                    else {

                        result.Correct = false;
                        result.ErrorMessage = "No encontramos Tipo de Orden en la Base de datos";
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
