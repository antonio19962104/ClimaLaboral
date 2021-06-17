using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class SubDepartamento
    {
        public static ML.Result GetAllNotDeleted()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.SubDepartamento.SqlQuery("SELECT * FROM SUBDEPARTAMENTO INNER JOIN DEPARTAMENTO ON SUBDEPARTAMENTO.IDDEPARTAMENTO = DEPARTAMENTO.IDDEPARTAMENTO INNER JOIN AREA ON DEPARTAMENTO.IDAREA = AREA.IDAREA INNER JOIN COMPANY ON AREA.COMPANYID = COMPANY.COMPANYID INNER JOIN COMPANYCATEGORIA ON COMPANY.IDCOMPANYCATEGORIA = COMPANYCATEGORIA.IDCOMPANYCATEGORIA").ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Subdepartamento subdepto = new ML.Subdepartamento();
                            subdepto.Departamento = new ML.Departamento();
                            subdepto.Departamento.Area = new ML.Area();
                            subdepto.Departamento.Area.Company = new ML.Company();
                            subdepto.Departamento.Area.Company.CompanyCategoria = new ML.CompanyCategoria();

                            subdepto.IdSubdepartamento = item.IdSubdepartamento;
                            subdepto.Nombre = item.Nombre;

                            subdepto.Departamento.IdDepartamento = item.Departamento.IdDepartamento;
                            subdepto.Departamento.Nombre = item.Departamento.Nombre;

                            subdepto.Departamento.Area.IdArea = item.Departamento.Area.IdArea;
                            subdepto.Departamento.Area.Nombre = item.Departamento.Area.Nombre;

                            subdepto.Departamento.Area.Company.CompanyId = item.Departamento.Area.Company.CompanyId;
                            subdepto.Departamento.Area.Company.CompanyName = item.Departamento.Area.Company.CompanyName;

                            subdepto.Departamento.Area.Company.CompanyCategoria.IdCompanyCategoria = item.Departamento.Area.Company.CompanyCategoria.IdCompanyCategoria;
                            subdepto.Departamento.Area.Company.CompanyCategoria.Descripcion = item.Departamento.Area.Company.CompanyCategoria.Descripcion;

                            result.Objects.Add(subdepto);
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
        public static ML.Result Add(ML.Subdepartamento subdepto)
        {
            ML.Result result = new ML.Result();

            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var query = context.SubDepartamento.SqlQuery("INSERT INTO SUBDEPARTAMENTO (NOMBRE, IDDEPARTAMENTO, IDESTATUS, FECHAHORACREACION, USUARIOCREACION, PROGRAMACREACION) VALUES ({0}, {1}, {2}, {3}, {4}, {5})", subdepto.Nombre, subdepto.Departamento.IdDepartamento, subdepto.TipoEstatus.IdEstatus, DateTime.Now, subdepto.CURRENT_USER, "Diagnostic4U");

                        context.SaveChanges();
                        transaction.Commit();
                        result.Correct = true;
                    }
                    catch (Exception ex)
                    {
                        result.Correct = false;
                        result.ErrorMessage = ex.Message;
                        transaction.Rollback();
                    }
                }
            }
            return result;
        }
        public static ML.Result GetByIdDepartamento(int id)
        {
            ML.Result result = new ML.Result();
            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                try
                {
                    var query = context.SubDepartamento.SqlQuery("SELECT * FROM SUBDEPARTAMENTO WHERE IDDEPARTAMENTO = {0}", id).ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Subdepartamento sub = new ML.Subdepartamento();
                            sub.IdSubdepartamento = item.IdSubdepartamento;
                            sub.Nombre = item.Nombre;

                            result.Objects.Add(sub);
                        }
                    }
                    result.Correct = true;
                }
                catch (Exception ex)
                {
                    result.Correct = false;
                    result.ErrorMessage = ex.Message;
                    var st = new StackTrace();
                    var ft = st.GetFrame(0);
                    BL.LogReporteoClima.writteLog(result.ErrorMessage, new StackTrace());
                }
            }
            return result;
        }
    }
}
