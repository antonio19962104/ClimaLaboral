using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BL
{
    public class PerfilD4U
    {
        public static ML.Result Add(ML.PerfilD4U PerfilD4U)
        {
            ML.Result result = new ML.Result();

            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                context.Database.Log = Console.Write;

                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var query = context.Database.ExecuteSqlCommand
                            ("INSERT INTO PerfilD4U (Descripcion, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3})", PerfilD4U.Descripcion, DateTime.Now, "dbo", "RH_Encuesta");

                        result.Correct = true;
                        context.SaveChanges();
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

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.PerfilD4U.SqlQuery("SELECT * FROM PerfilD4U ORDER BY IdPerfil ASC").ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.PerfilD4U perfilD4U = new ML.PerfilD4U();

                            perfilD4U.IdPerfil = item.IdPerfil;
                            perfilD4U.Descripcion = item.Descripcion;

                            result.Objects.Add(perfilD4U);
                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.ErrorMessage = "No se obtuvieron Perfiles de administrador D4U";
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
        public static ML.Result GetByFiltrado(int IdEmpleadoForNewAdmin)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.getPerfilFiltrado(IdEmpleadoForNewAdmin).ToList();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.PerfilD4U perfilD4U = new ML.PerfilD4U();

                            perfilD4U.IdPerfil = item.IdPerfil;
                            perfilD4U.Descripcion = item.Descripcion;

                            result.Objects.Add(perfilD4U);
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

        public static ML.Result ExisteAdmin(int IdEmpleadoForNewAdmin)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Administrador.SqlQuery("SELECT * FROM Administrador WHERE IdEmpleado = {0}", IdEmpleadoForNewAdmin).ToList();

                    if (query.Count == 0)
                    {
                        result.Exist = false;
                    }
                    else
                    {
                        result.Exist = true;
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

        public static ML.Result GetById(int IdPerfilD4U)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.PerfilD4U.SqlQuery("SELECT * FROM PerfilD4U WHERE IdPerfil = {0}", IdPerfilD4U);

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.PerfilD4U perfilD4U = new ML.PerfilD4U();

                            perfilD4U.IdPerfil = item.IdPerfil;
                            perfilD4U.Descripcion = item.Descripcion;

                            result.Object = perfilD4U;

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

        public static ML.Result GetPerfilByEmpleado(int IdEmpleado)
        {

            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.PerfilD4U.SqlQuery("SELECT * FROM PerfilD4U INNER JOIN Administrador ON PerfilD4U.IdPerfil = Administrador.IdPerfil WHERE Administrador.IdEmpleado = {0}", IdEmpleado).ToList();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.PerfilD4U perfilD = new ML.PerfilD4U();

                            perfilD.IdPerfil = Convert.ToInt32(item.IdPerfil);
                            perfilD.Descripcion = item.Descripcion;

                            result.Objects.Add(perfilD);

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
