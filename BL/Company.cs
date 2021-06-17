using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Company
    {
        public static ML.Result GetByCompanyCategoria(int IdCompanyCategoria)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.CompanyGetByIdCategoriaDemoOK(IdCompanyCategoria).ToList();
                    result.Objects = new List<Object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Company company = new ML.Company();

                            company.CompanyId = obj.CompanyId;
                            company.CompanyName = obj.CompanyName;


                            result.Objects.Add(company);
                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo obtener el filtrado de Direccion General / Propiedad";
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

        public static ML.Result GetByCompanyCategoriaReporte(string IdCompanyCategoria)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.CompanyGetByIdCategoriaTipo2OKOK(IdCompanyCategoria).ToList();
                    result.Objects = new List<Object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Company company = new ML.Company();

                            company.CompanyId = obj.CompanyId;
                            company.CompanyName = obj.CompanyName;

                            result.Objects.Add(company);
                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo obtener el filtrado de Direccion General / Propiedad";
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

        public static ML.Result GetAllCompany()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Company.SqlQuery("SELECT *  FROM Company "+
                        "  order by CompanyName asc");
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Company company = new ML.Company();
                            company.CompanyId = obj.CompanyId;
                            company.CompanyName = obj.CompanyName;
                            result.Objects.Add(company);
                            result.Correct = true;
                        }
                    }
                    else {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo obtener el listado de Empresas";
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

        public static ML.Result GetByFilter(string Filtro)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            string querySQL = "SELECT * FROM COMPANY WHERE CompanyName LIKE '" + Filtro + "%' AND TIPO = 1";
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Company.SqlQuery(querySQL).ToList();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Company company = new ML.Company();

                            company.CompanyId = item.CompanyId;
                            company.CompanyName = item.CompanyName;

                            result.Objects.Add(company);
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
        public static ML.Result GetByCompanyCategoriaForD4U(int IdCompanyCategoria)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Company.SqlQuery("SELECT * FROM COMPANY WHERE COMPANY.TIPO = 2 AND COMPANY.IDCOMPANYCATEGORIA = {0}", IdCompanyCategoria).ToList();
                    result.Objects = new List<Object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Company company = new ML.Company();

                            company.CompanyId = obj.CompanyId;
                            company.CompanyName = obj.CompanyName;

                            result.Objects.Add(company);
                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo obtener el filtrado de Direccion General / Propiedad";
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

        public static ML.Result GetByCompanyCategoriaForD4UTipo1(int IdCompanyCategoria)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = new List<DL.Company>();
                    if (IdCompanyCategoria == 9)
                    {
                        query = context.Company.SqlQuery("SELECT * FROM COMPANY WHERE COMPANY.TIPO = 2 AND COMPANY.IDCOMPANYCATEGORIA = {0}", IdCompanyCategoria).ToList();
                    }
                    else
                    {
                        query = context.Company.SqlQuery("SELECT * FROM COMPANY WHERE COMPANY.TIPO = 1 AND COMPANY.IDCOMPANYCATEGORIA = {0}", IdCompanyCategoria).ToList();
                    }
                    
                    result.Objects = new List<Object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Company company = new ML.Company();

                            company.CompanyId = obj.CompanyId;
                            company.CompanyName = obj.CompanyName;

                            result.Objects.Add(company);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo obtener el filtrado de Direccion General / Propiedad";
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

        public static ML.Result GetByUNeg(string Uneg)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Company.SqlQuery("SELECT * FROM COMPANY INNER JOIN COMPANYCATEGORIA ON COMPANY.IDCOMPANYCATEGORIA = COMPANYCATEGORIA.IDCOMPANYCATEGORIA WHERE COMPANYCATEGORIA.DESCRIPCION = {0} and Company.Tipo = 1", Uneg).ToList();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Company comp = new ML.Company();
                            comp.CompanyId = item.CompanyId;
                            comp.CompanyName = item.CompanyName;

                            result.Objects.Add(comp);
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
