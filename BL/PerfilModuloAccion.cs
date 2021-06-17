using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class PerfilModuloAccion
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.PerfilModuloAccion.SqlQuery("SELECT * FROM PerfilModuloAccion").ToList();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.PefilModuloAccion perfilModuloAccion = new ML.PefilModuloAccion();

                            perfilModuloAccion.IdPerfilModuloAccion = item.IdPerfilModuloAccion;
                            perfilModuloAccion.PerfilModulo = new ML.PerfilModulo();

                            perfilModuloAccion.PerfilModulo.IdPerfilModulo = Convert.ToInt32(item.IdPerfilModulo);

                            result.Objects.Add(perfilModuloAccion);
                            result.Correct = true;
                        }
                    }
                    else
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

        public static ML.Result Add(ML.PefilModuloAccion perfilModuloAccion)
        {
            ML.Result result = new ML.Result();

            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                context.Database.Log = Console.Write;

                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var query = context.Database.ExecuteSqlCommand("INSERT INTO PerfilModuloAccion (IdPerfilModulo, Accion, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4})", perfilModuloAccion.PerfilModulo.IdPerfilModulo, perfilModuloAccion.Accion, DateTime.Now, "dbo", "RH_Encuesta");

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

        public static ML.Result Delete(int IdPerfilModuloAccion)
        {
            ML.Result result = new ML.Result();

            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                context.Database.Log = Console.Write;
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var query = context.Database.ExecuteSqlCommand("DELETE FROM PErfilModuloAcion WHERE IdPerfilModuloAccion = {0}", IdPerfilModuloAccion);

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
    }
}
