using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Area
    {
        public static ML.Result AreaGetByCompanyId(int CompanyId)
        {
            ML.Result result = new ML.Result();
            
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.AreaGetByCompanyIdEmpresaOKOK(CompanyId).ToList();
                    result.Objects = new List<Object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Area area = new ML.Area();

                            area.IdArea = obj.IdArea;
                            area.Nombre = obj.Nombre;
                            area.Company = new ML.Company();
                            area.Company.CompanyId = obj.CompanyId;
                            area.Company.CompanyName = obj.CompanyName;
                            area.Company.CompanyCategoria = new ML.CompanyCategoria();
                            area.Company.CompanyCategoria.IdCompanyCategoria = obj.IdCompanyCategoria;
                            area.Company.CompanyCategoria.Descripcion = obj.Descripcion;
                            area.TipoEstatus = new ML.TipoEstatus();
                            area.TipoEstatus.IdEstatus = obj.IdEstatus;
                            area.TipoEstatus.Descripcion = obj.DescripcionEstatus;

                            result.Objects.Add(area);

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

        public static ML.Result AreaGetByCompanyIdReporte(string CompanyId)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.AreaGetByCompanyIdDemoReporteOKOK(CompanyId).ToList();
                    result.Objects = new List<Object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Area area = new ML.Area();

                            area.IdArea = obj.IdArea;
                            area.Nombre = obj.Nombre;
                            area.Company = new ML.Company();
                            area.Company.CompanyName = obj.CompanyName;

                            result.Objects.Add(area);

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

        public static ML.Result Update(ML.Area area)
        {
            ML.Result result = new ML.Result();

            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var query = context.Database.ExecuteSqlCommand("UPDATE Area SET Nombre = {0} WHERE IdArea = {1}", area.Nombre, area.IdArea);

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

        public static ML.Result UpdateEstatus(ML.Area area)
        {
            ML.Result result = new ML.Result();

            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        int newEstatus;
                        if (area.IdentitificadorEstatus == 1)
                        {
                            newEstatus = 2;
                            var query = context.UpdateEstatusFromAreaOKOK_(area.IdArea, newEstatus); //newEstatus, area.IdArea);

                            result.Correct = true;
                            context.SaveChanges();
                            transaction.Commit();
                        }
                        else if (area.IdentitificadorEstatus == 2)
                        {
                            newEstatus = 1;
                            var query = context.UpdateEstatusFromAreaOKOK_(area.IdArea, newEstatus);

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

        public static ML.Result UpdateEstatusFromModal(ML.Area area, DL.RH_DesEntities context, System.Data.Entity.DbContextTransaction transaction)
        {
            ML.Result result = new ML.Result();

                    try
                    {
                
                            
                            var query = context.UpdateEstatusFromAreaOK_(area.IdArea, area.IdentitificadorEstatus); //newEstatus, area.IdArea);

                            result.Correct = true;
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

        public static ML.Result Add(ML.Area area)
        {
            ML.Result result = new ML.Result();

            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {

                        var query = context.Database.ExecuteSqlCommand("INSERT INTO AREA (NOMBRE, IDESTATUS, COMPANYID, FECHAHORACREACION, USUARIOCREACION, PROGRAMACREACION) VALUES ({0}, {1}, {2}, {3}, {4}, {5})", area.Nombre, area.TipoEstatus.IdEstatus, area.Company.CompanyId, DateTime.Now, area.CURRENT_USER, "Diagnostic4U");

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

        public static ML.Result GetById(int IdArea)
        {
            ML.Result result = new ML.Result();

            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                try
                {
                    var query = context.Area.SqlQuery("SELECT * FROM AREA INNER JOIN Company ON Area.CompanyId = Company.CompanyId INNER JOIN CompanyCategoria ON Company.IdCompanyCategoria = CompanyCategoria.IdCompanyCategoria INNER JOIN TipoEstatus ON Area.IdEstatus = TipoEstatus.IdEstatus WHERE AREA.IDAREA = {0}", IdArea).ToList();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Area area = new ML.Area();
                            area.Company = new ML.Company();
                            area.Company.CompanyCategoria = new ML.CompanyCategoria();
                            area.TipoEstatus = new ML.TipoEstatus();

                            area.IdArea = item.IdArea;
                            area.Nombre = item.Nombre;
                            area.Company.CompanyId = Convert.ToInt32(item.CompanyId);
                            area.Company.CompanyName = item.Company.CompanyName;
                            area.Company.CompanyCategoria.IdCompanyCategoria = item.Company.CompanyCategoria.IdCompanyCategoria;
                            area.Company.CompanyCategoria.Descripcion = item.Company.CompanyCategoria.Descripcion;
                            area.TipoEstatus.IdEstatus = Convert.ToInt32(item.IdEstatus);
                            area.TipoEstatus.Descripcion = item.TipoEstatus.Descripcion;

                            result.Object = area;
                            result.Correct = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.Correct = false;
                    result.ErrorMessage = ex.Message;
                }
            }
            return result;
        }

        public static ML.Result UpdateD4U(ML.Area area)
        {
            ML.Result result = new ML.Result();

            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var query = context.Database.ExecuteSqlCommand("UPDATE AREA SET NOMBRE = {0}, COMPANYID = {1}, IDESTATUS = {2}, FECHAHORAMODIFICACION = {3}, USUARIOMODIFICACION = {4}, PROGRAMAMODIFICACION = {5} where IdArea = {6}", area.Nombre, area.Company.CompanyId, area.TipoEstatus.IdEstatus, DateTime.Now, area.CURRENT_USER, "Diagnostic4U", area.IdArea);

                        //Call UpdateFromArea para que afecte tambien a departamentos
                        area.IdentitificadorEstatus = Convert.ToInt32(area.TipoEstatus.IdEstatus);
                        UpdateEstatusFromModal(area, context, transaction);

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

        public static ML.Result Delete(int IdArea)
        {
            ML.Result result = new ML.Result();

            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var query = context.DeleteArea(IdArea);

                        context.SaveChanges();
                        transaction.Commit();
                        result.Correct = true;
                    }
                    catch (Exception ex)
                    {
                        result.ErrorMessage = ex.Message;
                        result.Correct = false;
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
                    var query = context.Area.SqlQuery("SELECT * FROM AREA INNER JOIN COMPANY ON AREA.COMPANYID = COMPANY.COMPANYID INNER JOIN COMPANYCATEGORIA ON COMPANY.IDCOMPANYCATEGORIA = COMPANYCATEGORIA.IDCOMPANYCATEGORIA INNER JOIN TIPOESTATUS ON AREA.IDESTATUS = TIPOESTATUS.IDESTATUS WHERE AREA.IDESTATUS = 3").ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Area area = new ML.Area();
                            area.TipoEstatus = new ML.TipoEstatus();
                            area.Company = new ML.Company();
                            area.Company.CompanyCategoria = new ML.CompanyCategoria();

                            area.IdArea = item.IdArea;
                            area.Nombre = item.Nombre;
                            area.TipoEstatus.IdEstatus = item.TipoEstatus.IdEstatus;
                            area.TipoEstatus.Descripcion = item.TipoEstatus.Descripcion;
                            area.Company.CompanyId = item.Company.CompanyId;
                            area.Company.CompanyName = item.Company.CompanyName;
                            area.Company.CompanyCategoria.IdCompanyCategoria = item.Company.CompanyCategoria.IdCompanyCategoria;
                            area.Company.CompanyCategoria.Descripcion = item.Company.CompanyCategoria.Descripcion;

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
        public static ML.Result GetAreaByEmpresa(int CompanyId)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Area.SqlQuery("SELECT * FROM AREA WHERE COMPANYID = {0} and tipo = 2", CompanyId).ToList();
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

        public static ML.Result GetAreaByEmpresaTipo1(int CompanyId)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Area.SqlQuery("SELECT * FROM AREA WHERE COMPANYID = {0} and tipo = 1", CompanyId).ToList();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Area area = new ML.Area();
                            area.IdArea = item.IdArea;
                            area.Nombre = item.Nombre;

                            result.Objects.Add(area);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                var st = new StackTrace();
                var ft = st.GetFrame(0);
                BL.LogReporteoClima.writteLog(ex.Message, new StackTrace());
            }
            return result;
        }
        public static ML.Result GetAreaByEmpresaTipo2(int CompanyId)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Area.SqlQuery("SELECT * FROM AREA WHERE COMPANYID = {0} and tipo = 2", CompanyId).ToList();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Area area = new ML.Area();
                            area.IdArea = item.IdArea;
                            area.Nombre = item.Nombre;

                            result.Objects.Add(area);
                        }
                        result.Correct = true;
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
