using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Diagnostics;

namespace BL
{
    public class Departamento
    {
        public static ML.Result GetByArea(int IdArea)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.DepartamentoGetByAreaTipo1(IdArea).ToList();
                    result.Objects = new List<Object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Departamento departamento = new ML.Departamento();

                            departamento.IdDepartamento = obj.IdDepartamento;
                            departamento.Nombre = obj.NombreDepartamento;
                            departamento.Area = new ML.Area();
                            departamento.Area.Nombre = obj.NombreArea;

                            result.Objects.Add(departamento);

                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudieron obtener las areas";
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

        public static ML.Result GetByAreaTipo1(int IdArea)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Departamento.SqlQuery("SELECT * FROM DEPARTAMENTO WHERE TIPO > 0 AND IDAREA = {0}", IdArea);
                    result.Objects = new List<Object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Departamento departamento = new ML.Departamento();

                            departamento.IdDepartamento = obj.IdDepartamento;
                            departamento.Nombre = obj.Nombre;
                            result.Objects.Add(departamento);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudieron obtener las areas";
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

        public static ML.Result GetByAreaReporte(string IdArea)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.DepartamentoGetByAreaTipo2(IdArea).ToList();
                    result.Objects = new List<Object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Departamento departamento = new ML.Departamento();

                            departamento.IdDepartamento = obj.IdDepartamento;
                            departamento.Nombre = obj.NombreDepartamento;
                            departamento.Area = new ML.Area();
                            departamento.Area.Nombre = obj.NombreArea;

                            result.Objects.Add(departamento);

                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudieron obtener las areas";
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

        public static ML.Result AddForD4U(DL.Departamento departamentoData, DL.RH_DesEntities context, System.Data.Entity.DbContextTransaction transaction)
        {
            ML.Result result = new ML.Result();

            ML.Departamento departamento = new ML.Departamento();
            departamento.Nombre = departamentoData.Nombre;
            departamento.Area = new ML.Area();
            departamento.Area.IdArea = Convert.ToInt32(departamentoData.IdArea);
            departamento.IdEstatus = Convert.ToInt32(departamentoData.IdEstatus);

            try
            {
                var query = context.Database.ExecuteSqlCommand("INSERT INTO DEPARTAMENTO (NOMBRE, IDAREA, IDESTATUS, FECHAHORACREACION, USUARIOCREACION, PROGRAMACREACION) VALUES ({0}, {1}, {2}, {3}, {4}, {5})", departamento.Nombre, departamento.Area.IdArea, departamentoData.IdEstatus, DateTime.Now, "Alta Departamento", "Diagnostic4U");
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetDepartamentoByIdArea(int IdArea)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Departamento.SqlQuery("SELECT * FROM DEPARTAMENTO INNER JOIN TIPOESTATUS ON DEPARTAMENTO.IDESTATUS = TIPOESTATUS.IDESTATUS INNER JOIN AREA ON DEPARTAMENTO.IDAREA = AREA.IDAREA INNER JOIN COMPANY ON AREA.COMPANYID = COMPANY.COMPANYID WHERE AREA.IDAREA = {0} and (Departamento.IdEstatus = 1 or Departamento.IdEstatus = 2)", IdArea).ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Departamento departamento = new ML.Departamento();
                            departamento.Area = new ML.Area();
                            departamento.Area.Company = new ML.Company();
                            departamento.TipoEstatus = new ML.TipoEstatus();

                            departamento.IdDepartamento = item.IdDepartamento;
                            departamento.Nombre = item.Nombre;
                            departamento.IdEstatus = Convert.ToInt32(item.IdEstatus);
                            departamento.TipoEstatus.Descripcion = item.TipoEstatus.Descripcion;
                            departamento.Area.IdArea = item.Area.IdArea;
                            departamento.Area.Nombre = item.Area.Nombre;
                            departamento.Area.Company.CompanyId = item.Area.Company.CompanyId;
                            departamento.Area.Company.CompanyName = item.Area.Company.CompanyName;

                            result.Objects.Add(departamento);
                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.ErrorMessage = "No se obtuvieron datos";
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

        public static ML.Result Add(ML.Departamento departamento)
        {
            ML.Result result = new ML.Result();

            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var query = context.Database.ExecuteSqlCommand("INSERT INTO DEPARTAMENTO (NOMBRE, IDAREA, IDESTATUS) VALUES ({0}, {1}, {2})", departamento.Nombre, departamento.Area.IdArea, departamento.TipoEstatus.IdEstatus);

                        result.Correct = true;
                        context.SaveChanges();
                        transaction.Commit();

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

        public static ML.Result UpdateEstatus(ML.Departamento departamento)
        {
            ML.Result result = new ML.Result();

            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        int newEstatus;
                        if (departamento.IdentitificadorEstatus == 1)
                        {
                            newEstatus = 2;
                            var query = context.Database.ExecuteSqlCommand("UPDATE Departamento SET IdEstatus = {0} WHERE IdDepartamento = {1}", newEstatus, departamento.IdDepartamento);

                            result.Correct = true;
                            context.SaveChanges();
                            transaction.Commit();
                        }
                        else if (departamento.IdentitificadorEstatus == 2)
                        {
                            newEstatus = 1;
                            var query = context.Database.ExecuteSqlCommand("UPDATE Departamento SET IdEstatus = {0} WHERE IdDepartamento = {1}", newEstatus, departamento.IdDepartamento);

                            result.Correct = true;
                            context.SaveChanges();
                            transaction.Commit();
                        }
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

        //Validaciones de existencia

        public static ML.Result ValidarNombreArea(string NombreArea)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.ValidateNombreAreaOK_(NombreArea).ToList();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            result.ExisteArea = item.Value;
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

        public static ML.Result ValidarNombreCompany(string NombreCompany)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.ValidateCompanyNameOK_(NombreCompany).ToList();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            result.ExisteEmpresa = item.Value;
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

        public static ML.Result Delete(int IdDepartamento)
        {
            ML.Result result = new ML.Result();

            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var query = context.DeleteDepartamento(IdDepartamento);

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

        public static ML.Result GetAllEliminados()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Departamento.SqlQuery("SELECT * FROM DEPARTAMENTO INNER JOIN AREA ON DEPARTAMENTO.IDAREA = AREA.IDAREA INNER JOIN COMPANY ON AREA.COMPANYID = COMPANY.COMPANYID INNER JOIN COMPANYCATEGORIA ON COMPANY.IDCOMPANYCATEGORIA = COMPANYCATEGORIA.IDCOMPANYCATEGORIA INNER JOIN TIPOESTATUS ON DEPARTAMENTO.IDESTATUS = TIPOESTATUS.IDESTATUS  WHERE DEPARTAMENTO.IDESTATUS = 3").ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Departamento departamento = new ML.Departamento();
                            departamento.Area = new ML.Area();
                            departamento.Area.Company = new ML.Company();
                            departamento.Area.Company.CompanyCategoria = new ML.CompanyCategoria();
                            departamento.TipoEstatus = new ML.TipoEstatus();

                            departamento.IdDepartamento = item.IdDepartamento;
                            departamento.Nombre = item.Nombre;
                            departamento.TipoEstatus.IdEstatus = item.TipoEstatus.IdEstatus;
                            departamento.TipoEstatus.Descripcion = item.TipoEstatus.Descripcion;
                            departamento.Area.IdArea = item.Area.IdArea;
                            departamento.Area.Nombre = item.Area.Nombre;
                            departamento.Area.Company.CompanyId = item.Area.Company.CompanyId;
                            departamento.Area.Company.CompanyName = item.Area.Company.CompanyName;
                            departamento.Area.Company.CompanyCategoria.IdCompanyCategoria = item.Area.Company.CompanyCategoria.IdCompanyCategoria;
                            departamento.Area.Company.CompanyCategoria.Descripcion = item.Area.Company.CompanyCategoria.Descripcion;

                            result.Objects.Add(departamento);
                            result.Correct = true;

                        }
                    }

                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetDepartamentoById(int IdDepartamento)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Departamento.SqlQuery("SELECT * FROM DEPARTAMENTO INNER JOIN AREA ON DEPARTAMENTO.IDAREA = AREA.IDAREA INNER JOIN COMPANY ON AREA.COMPANYID = COMPANY.COMPANYID INNER JOIN COMPANYCATEGORIA ON COMPANY.IDCOMPANYCATEGORIA = COMPANYCATEGORIA.IDCOMPANYCATEGORIA INNER JOIN TIPOESTATUS ON DEPARTAMENTO.IDESTATUS = TIPOESTATUS.IDESTATUS  WHERE DEPARTAMENTO.IDDEPARTAMENTO = {0}", IdDepartamento).ToList();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Departamento depto = new ML.Departamento();
                            depto.TipoEstatus = new ML.TipoEstatus();
                            depto.Area = new ML.Area();
                            depto.Area.Company = new ML.Company();
                            depto.Area.Company.CompanyCategoria = new ML.CompanyCategoria();

                            depto.IdDepartamento = item.IdDepartamento;
                            depto.Nombre = item.Nombre;
                            depto.TipoEstatus.IdEstatus = item.TipoEstatus.IdEstatus;
                            depto.TipoEstatus.Descripcion = item.TipoEstatus.Descripcion;
                            depto.Area.IdArea = item.Area.IdArea;
                            depto.Area.Nombre = item.Area.Nombre;
                            depto.Area.Company.CompanyId = item.Area.Company.CompanyId;
                            depto.Area.Company.CompanyName = item.Area.Company.CompanyName;
                            depto.Area.Company.CompanyCategoria.IdCompanyCategoria = item.Area.Company.CompanyCategoria.IdCompanyCategoria;
                            depto.Area.Company.CompanyCategoria.Descripcion = item.Area.Company.CompanyCategoria.Descripcion;

                            result.Object = depto;
                            result.Correct = true;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result UpdateDepartamento(ML.Departamento departamento)
        {
            ML.Result result = new ML.Result();

            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {

                        var query = context.Database.ExecuteSqlCommand("UPDATE DEPARTAMENTO SET NOMBRE = {0}, IDESTATUS = {1}, IDAREA = {2}, FECHAHORAMODIFICACION = {3}, USUARIOMODIFICACION = {4}, PROGRAMAMODIFICACION = {5} WHERE IDDEPARTAMENTO = {6}"
                            , departamento.Nombre, departamento.TipoEstatus.IdEstatus, departamento.Area.IdArea, DateTime.Now, departamento.CURRENT_USER, "Diagnostic4U", departamento.IdDepartamento);

                        context.SaveChanges();
                        transaction.Commit();
                        result.Correct = true;

                    }
                    catch(Exception ex)
                    {
                        result.Correct = false;
                        result.ErrorMessage = ex.Message;
                        transaction.Rollback();
                    }
                }
            }
            return result;
        }
    }
}
