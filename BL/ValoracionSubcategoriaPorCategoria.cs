using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.ComponentModel;

namespace BL
{
    public class ValoracionSubcategoriaPorCategoria
    {
        public static decimal getValConfig(int idEncuesta, int idSubcategoria)
        {
            decimal valor = 0;
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.ValoracionSubcategoriaPorCategoria.Select(o => o).Where(o => o.IdEncuesta == idEncuesta && o.IdSubcategoria == idSubcategoria).ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            valor = (decimal)item.Valor;
                        }
                    }
                    return valor;
                }                
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new System.Diagnostics.StackTrace());
                return valor;                
            }           
        }
    }
}
