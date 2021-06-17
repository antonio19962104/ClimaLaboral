using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empresa
    {
        public static ML.Result GetAll(List<object> permisosEstrucura)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {

                    //Get CompanyIdForPermisos
                    List<string> CompanyIds = new List<string>();
                    string WHEREsql = "";
                    foreach (ML.AdministradorCompany item in permisosEstrucura)
                    {
                        string CompanyId = Convert.ToString(item.Company.CompanyId);
                        string CompanyIdFinal = " OR Company.CompanyId = " + CompanyId + "";
                        WHEREsql += CompanyIdFinal;
                    }

                    Console.WriteLine(WHEREsql);


                    var query = context.Company.SqlQuery("SELECT * FROM Company " +
                        "INNER JOIN CompanyCategoria ON Company.IdCompanyCategoria = CompanyCategoria.IdCompanyCategoria " +
                        "INNER JOIN TipoEstatus ON Company.IdEstatus = TipoEstatus.IdEstatus " +
                        "WHERE Company.CompanyId = 0  " + WHEREsql + "order by Company.CompanyName").ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Company company = new ML.Company();
                            company.CompanyCategoria = new ML.CompanyCategoria();
                            //get estatus
                            company.CompanyId = item.CompanyId;
                            company.CompanyName = item.CompanyName;
                            company.CompanyCategoria.IdCompanyCategoria = item.CompanyCategoria.IdCompanyCategoria;
                            company.CompanyCategoria.Descripcion = item.CompanyCategoria.Descripcion;
                            company.TipoEstatus = new ML.TipoEstatus();
                            company.TipoEstatus.IdEstatus = item.TipoEstatus.IdEstatus;
                            company.TipoEstatus.Descripcion = item.TipoEstatus.Descripcion;
                            company.LogoEmpresa = item.LogoEmpresa;
                            company.Color = item.Color;
                            company.TipoEmpresa = item.TipoEmpresa;

                            if (company.TipoEstatus.IdEstatus == 1 || company.TipoEstatus.IdEstatus == 2)
                            {
                                result.Objects.Add(company);
                            }
                            result.Correct = true;
                        }
                    }
                    else if (query.Count == 0)
                    {
                        result.ErrorMessage = "No se obtuvieron registros";
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

        public static ML.Result GetByCompanyCategoria(int IdCompanyCategoria)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Company.SqlQuery("SELECT * FROM Company INNER JOIN CompanyCategoria ON Company.IdCompanyCategoria = CompanyCategoria.IdCompanyCategoria WHERE CompanyCategoria.IdCompanyCategoria = {0}", IdCompanyCategoria).ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Company company = new ML.Company();
                            company.CompanyCategoria = new ML.CompanyCategoria();

                            company.CompanyId = item.CompanyId;
                            company.CompanyName = item.CompanyName;
                            company.CompanyCategoria.IdCompanyCategoria = item.CompanyCategoria.IdCompanyCategoria;
                            company.CompanyCategoria.Descripcion = item.CompanyCategoria.Descripcion;

                            result.Objects.Add(company);

                            result.Correct = true;
                        }
                    }
                    else if (query.Count == 0)
                    {
                        result.ErrorMessage = "No se obtuvieron registros";
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

        public static ML.Result GetByComoanyId(int CompanyId)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Company.SqlQuery("SELECT * FROM Company INNER JOIN CompanyCategoria ON Company.IdCompanyCategoria = CompanyCategoria.IdCompanyCategoria INNER JOIN TipoEstatus ON Company.IdEstatus = TipoEstatus.IdEstatus WHERE Company.CompanyId = {0}", CompanyId).ToList();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Company company = new ML.Company();
                            company.CompanyCategoria = new ML.CompanyCategoria();
                            company.TipoEstatus = new ML.TipoEstatus();

                            company.CompanyId = item.CompanyId;
                            company.CompanyName = item.CompanyName;
                            company.CompanyCategoria.IdCompanyCategoria = item.CompanyCategoria.IdCompanyCategoria;
                            company.CompanyCategoria.Descripcion = item.CompanyCategoria.Descripcion;
                            company.TipoEstatus.IdEstatus = item.TipoEstatus.IdEstatus;
                            company.TipoEstatus.Descripcion = item.TipoEstatus.Descripcion;
                            company.LogoEmpresa = item.LogoEmpresa;
                            company.Color = item.Color;

                            result.Object = company;

                            result.Correct = true;

                        }
                    }
                    else if (query.Count == 0)
                    {
                        result.ErrorMessage = "No se encontraron registros";
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

        public static ML.Result Add(ML.Company company, int IdAdminCreate)
        {
            ML.Result result = new ML.Result();

            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        //GEt value of last insert (id is not identity)
                        int UltimoInsertado = context.Company.Max(p => p.CompanyId);
                        int newId = UltimoInsertado + 1;
                        var query = context.Database.ExecuteSqlCommand
                            ("INSERT INTO Company (CompanyId, CompanyName, IdCompanyCategoria, IdEstatus, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion, LogoEmpresa, Color, TipoEmpresa, IdAdministradorCreate, Tipo) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11})",
                                                    newId, company.CompanyName, company.CompanyCategoria.IdCompanyCategoria, company.TipoEstatus.IdEstatus, DateTime.Now, company.CURRENT_USER, "Diagnostic4U", company.LogoEmpresa, company.Color, company.TipoEmpresa, IdAdminCreate, 1);
                        var queryAux = context.Database.ExecuteSqlCommand("INSERT INTO ADMINISTRADORCOMPANY (IDADMINISTRADOR, COMPANYID) VALUES ({0}, {1})", IdAdminCreate, newId);


                        //Insertar la nueva empresa a AdministradorCompany de los admin SA
                        var getAdminSA = context.Administrador.SqlQuery("SELECT * FROM ADMINISTRADOR WHERE ADMINSA = 1").ToList();
                        foreach (var item in getAdminSA)
                        {
                            var insertAdminCompany = context.Database.ExecuteSqlCommand
                            ("INSERT INTO ADMINISTRADORCOMPANY (IDADMINISTRADOR, COMPANYID, FECHAHORACREACION, USUARIOCREACION, PROGRAMACREACION) VALUES ({0}, {1}, {2}, {3}, {4})", item.IdAdministrador, newId, DateTime.Now, IdAdminCreate, "Diagnostic4U");
                        }


                        context.SaveChanges();
                        result.Correct = true;
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        result.Correct = false;
                        result.ErrorMessage = ex.Message;
                    }
                }
            }
            return result;
        }

        public static ML.Result Update(ML.Company company)
        {
            ML.Result result = new ML.Result();

            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (company.LogoEmpresa == "" || company.LogoEmpresa == null || company.Color == "" || company.Color == null)
                        {
                            var query = context.Database.ExecuteSqlCommand
                                ("UPDATE COMPANY SET CompanyName = {0}, IdCompanyCategoria = {1}, IdEstatus = {2}, FechaHoraModificacion = {3}, UsuarioModificacion = {4}, ProgramaModificacion = {5} WHERE CompanyId = {6}",
                                company.CompanyName, company.CompanyCategoria.IdCompanyCategoria, company.TipoEstatus.IdEstatus, DateTime.Now, company.CURRENT_USER, "Diagnostic4U", company.CompanyId);
                        }
                        else
                        {
                            var query = context.Database.ExecuteSqlCommand
                                ("UPDATE COMPANY SET CompanyName = {0}, IdCompanyCategoria = {1}, IdEstatus = {2}, FechaHoraModificacion = {3}, UsuarioModificacion = {4}, ProgramaModificacion = {5}, LogoEmpresa = {7}, Color = {8} WHERE CompanyId = {6}",
                                company.CompanyName, company.CompanyCategoria.IdCompanyCategoria, company.TipoEstatus.IdEstatus, DateTime.Now, company.CURRENT_USER, "Diagnostic4U", company.CompanyId, company.LogoEmpresa, company.Color);

                        }
                        company.IdentificadorEstatus = company.TipoEstatus.IdEstatus;
                        UpdateEstatusFromModalCompany(company, context, transaction);

                        transaction.Commit();
                        context.SaveChanges();
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

        public static ML.Result Delete(ML.Company company)
        {
            ML.Result result = new ML.Result();

            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var query = context.DeleteEmpresa(company.CompanyId, company.CURRENT_USER);

                        transaction.Commit();
                        context.SaveChanges();
                        result.Correct = true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        result.Correct = false;
                        result.ErrorMessage = ex.Message;
                    }
                }
            }
            return result;
        }

        public static ML.Result UpdateEstatus(ML.Company company)
        {
            ML.Result result = new ML.Result();

            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {

                        int newEstatus;
                        if (company.IdentificadorEstatus == 1)
                        {
                            newEstatus = 2;
                            var query = context.UpdateEstatusFromCompany(company.CompanyId, newEstatus);

                            result.Correct = true;
                            context.SaveChanges();
                            transaction.Commit();
                        }
                        else if (company.IdentificadorEstatus == 2)
                        {
                            newEstatus = 1;
                            var query = context.UpdateEstatusFromCompany(company.CompanyId, newEstatus);

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

        public static ML.Result UpdateEstatusFromModalCompany(ML.Company company, DL.RH_DesEntities context, System.Data.Entity.DbContextTransaction transaction)
        {
            ML.Result result = new ML.Result();

                    try
                    {


                            var query = context.UpdateEstatusFromCompany(company.CompanyId, company.IdentificadorEstatus);

                            //result.Correct = true;
                            //context.SaveChanges();
                            //transaction.Commit();



                    }
                    catch (Exception ex)
                    {
                        result.Correct = false;
                        result.ErrorMessage = ex.Message;
                        //transaction.Rollback();
                    }

            return result;
        }

        public static ML.Result GetCompanyByIdCompanyCategoria(int IdCompanyCategoria)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Company.SqlQuery("SELECT * FROM Company WHERE IdCompanyCategoria = {0} order by CompanyName asc", IdCompanyCategoria).ToList();
                    result.Objects = new List<object>();
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

        public static ML.Result GetAllEliminados()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Company.SqlQuery("SELECT * FROM COMPANY INNER JOIN COMPANYCATEGORIA ON COMPANY.IDCOMPANYCATEGORIA = COMPANYCATEGORIA.IDCOMPANYCATEGORIA INNER JOIN TIPOESTATUS ON COMPANY.IDESTATUS = TIPOESTATUS.IDESTATUS WHERE COMPANY.IDESTATUS = 3").ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Company company = new ML.Company();
                            company.CompanyCategoria = new ML.CompanyCategoria();
                            company.TipoEstatus = new ML.TipoEstatus();

                            company.CompanyId = item.CompanyId;
                            company.CompanyName = item.CompanyName;
                            company.CompanyCategoria.IdCompanyCategoria = item.CompanyCategoria.IdCompanyCategoria;
                            company.CompanyCategoria.Descripcion = item.CompanyCategoria.Descripcion;
                            company.TipoEstatus.IdEstatus = item.TipoEstatus.IdEstatus;
                            company.TipoEstatus.Descripcion = item.TipoEstatus.Descripcion;

                            result.Objects.Add(company);
                            result.Correct = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
                result.Correct = false;
            }
            return result;
        }

        public static ML.Result GetAreaByCompanyId(int id)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Area.SqlQuery("SELECT * FROM AREA WHERE COMPANYID = {0}", id).ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Area area = new ML.Area();

                            area.IdArea = item.IdArea;
                            area.Nombre = item.Nombre;

                            result.Objects.Add(area);
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

        public static ML.Result GetEmpresasIngreso()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                   // var query = context.EncuestaIngreso.Select(m => m.Empresa).Distinct().OrderBy().ToList();
                    var query = (from o in context.EncuestaIngreso
                                 select new
                                 {
                                     o.Empresa
                                 }).Distinct().OrderBy(x => x.Empresa);

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.EncuestaIngreso enc = new ML.EncuestaIngreso();
                            enc.Empresa = item.Empresa;

                            result.Objects.Add(enc);
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

        public static ML.Result GetAllEmpresas()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Company.SqlQuery("SELECT * FROM COMPANY").ToList();
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

        public static ML.Result GetAllAreas()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Area.SqlQuery("SELECT * FROM AREA").ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Area area = new ML.Area();
                            area.IdArea = item.IdArea;
                            area.Nombre = item.Nombre;

                            result.Objects.Add(area);
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

        public static ML.Result GetAllDepartamentos()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Departamento.SqlQuery("SELECT * FROM DEPARTAMENTO").ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Departamento departamento = new ML.Departamento();
                            departamento.IdDepartamento = item.IdDepartamento;
                            departamento.Nombre = item.Nombre;

                            result.Objects.Add(departamento);
                            result.Correct = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = true;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
