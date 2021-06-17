using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Modulo
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Modulo.SqlQuery("SELECT * FROM Modulo").ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Modulo modulo = new ML.Modulo();

                            modulo.IdModulo = item.IdModulo;
                            modulo.Nombre = item.Nombre;

                            result.Objects.Add(modulo);
                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.ErrorMessage = "No se encontraron módulos";
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

        public static ML.Result GetById(int IdModulo)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Modulo.SqlQuery("SELECT * FROM Modulo WHERE IdModulo = {0}", IdModulo).ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Modulo modulo = new ML.Modulo();

                            modulo.IdModulo = item.IdModulo;
                            modulo.Nombre = item.Nombre;

                            result.Objects.Add(modulo);

                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.ErrorMessage = "No se obtivieron registros de modulos con el Id seleccionado";
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

        public static ML.Result Update(ML.Modulo modulo)
        {
            ML.Result result = new ML.Result();

            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                context.Database.Log = Console.Write;

                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var query = context.Database.ExecuteSqlCommand("UPDATE Modulo SET Nombre = {0}, FechaHoraModificacion = {1}, UsuarioModificacion = {2}, ProgramaModificacion = {3} WHERE IdModulo = {4}", modulo.Nombre, DateTime.Now, "dbo", "RH_Encuesta", modulo.IdModulo);

                        result.Correct = true;

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
