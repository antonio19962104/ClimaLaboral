using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class CompanyCategoria
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.CompanyCategoriaGetAll().ToList();
                    result.Objects = new List<Object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.CompanyCategoria companyCategoria = new ML.CompanyCategoria();

                            companyCategoria.IdCompanyCategoria = obj.IdCompanyCategoria;
                            companyCategoria.Descripcion = obj.Descripcion;

                            result.Objects.Add(companyCategoria);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron Categorias";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                var st = new StackTrace();
                var ft = st.GetFrame(0);
                BL.LogReporteoClima.writteLog(result.ErrorMessage, new StackTrace());
            }
            return result;
        }

        
    }
}
