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
        /// <summary>
        /// Metodo para obtener el tipo de orden para el nuevo listado de Encuestas CAMOS 20072021
        /// </summary>
        /// <param name="idTipoOrden">Id de Tipo de orden</param>
        /// <returns>Regresa el idtipo de orden y el nombre del tipo de orden</returns>
        public static ML.TipoOrden getTipoOrdenById(int idTipoOrden)
        {
            ML.TipoOrden orden = new ML.TipoOrden();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.TipoOrden.Select(o => o).Where(o => o.IdTipoOrden == idTipoOrden).ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            orden.IdTipoOrden = item.IdTipoOrden;
                            orden.Descripcion = item.Descripcion;                            
                        }
                    }
                }
            }
            catch (Exception aE)
            {
                return null;                
            }
            return orden;

        }
    }
}
