using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Estatus
    {
        public static ML.Result EstatusGet()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    
                    var query = context.EstatusGetAllOk_().ToList();
                    result.Objects = new List<Object>();
                    if(query != null)
                    {
                        foreach(var obj in query)
                        {
                            ML.EstatusEncuesta estatus = new ML.EstatusEncuesta();

                            estatus.IdEstatusEncuesta = obj.IdEstatusEncuesta;
                            estatus.Estatus = obj.Estatus;
                            estatus.Encuesta = new ML.Encuesta();
                            estatus.Encuesta.Nombre = obj.NombreEncuesta;
                            estatus.Empleado = new ML.Empleado();
                            estatus.Empleado.Nombre = obj.NombreEmpleado;
                            estatus.Empleado.ApellidoPaterno = obj.ApellidoPaterno;
                            estatus.Empleado.ApellidoMaterno = obj.ApellidoMaterno;

                            result.Objects.Add(estatus);
                            result.Correct = true;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; BL.LogReporteoClima.writeLogEncuestaClima(ex, new StackTrace());
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Test()
        {
            //El ML lo manda como parametro yo lo pongo aqui solo para el ejemplo
            ML.EstatusEncuesta SE = new ML.EstatusEncuesta();
            SE.Estatus = "";
            SE.Encuesta = new ML.Encuesta();
            SE.Encuesta.IdEncuesta = 1;
            SE.Empleado = new ML.Empleado();
            SE.Empleado.IdEmpleado = 3;

            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Database.ExecuteSqlCommand("INSERT INTO EstatusEncuesta (Estatus, IdEncuesta, IdEmpleado) VALUES({0}, {1}, {2})", SE.Estatus, SE.Encuesta.IdEncuesta, SE.Empleado.IdEmpleado);
                    result.Correct = true;
                    context.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                result.Correct = false; BL.LogReporteoClima.writeLogEncuestaClima(ex, new StackTrace());
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
