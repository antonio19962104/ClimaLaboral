using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Respuestas
    {
        public static ML.Result Add(ML.EmpleadoRespuesta EmpleadoRespuestas)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    //IdPregunta, RespuestaUsuario, IdRespuesta, IdEmpleado
                    var query = context.RespuestasAdd(EmpleadoRespuestas.Pregunta.IdPregunta, EmpleadoRespuestas.RespuestaEmpleado, EmpleadoRespuestas.Respuesta.IdRespuesta, EmpleadoRespuestas.Empleado.IdEmpleado);
                    var lastInsert = context.EmpleadoRespuestas.Max(o => o.IdEmpleadoRespuestas);
                    /*
                     * El Año en Estatus encuesta debe ser acorde a lo que se haya configurado en ConfigClimaLab
                     * JAMG
                     * 12/04/2021
                     * Buscar a BD en la que esta el empleado
                     * Buscar la configuracion segun la encuesta 1 y el id de BD
                    */
                    var q1 = context.Empleado.Select(o => new { o.IdEmpleado, o.Nombre, o.ApellidoPaterno, o.ApellidoMaterno, o.IdBaseDeDatos }).Where(o => o.IdEmpleado == EmpleadoRespuestas.Empleado.IdEmpleado).FirstOrDefault();
                    var idBD = q1.IdBaseDeDatos;

                    var q2 = context.ConfigClimaLab.Select(o => o).Where(o => o.IdBaseDeDatos == idBD && o.IdEncuesta == 1).FirstOrDefault();

                    context.Database.ExecuteSqlCommand("UPDATE EMPLEADORESPUESTAS SET ANIO = {0} WHERE IDEMPLEADORESPUESTAS = {1}", q2.PeriodoAplicacion, lastInsert);
                    context.SaveChanges();

                    result.Correct = true;
                }
            }
            catch(Exception ex)
            {
                result.Correct = false; BL.LogReporteoClima.writeLogEncuestaClima(ex, new StackTrace());
                result.ErrorMessage = ex.Message;
            }
            return result;
        }


        public static ML.Result Update(ML.EmpleadoRespuesta EmpleadoRespuestas)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.RespuestasUpdate(EmpleadoRespuestas.Pregunta.IdPregunta, EmpleadoRespuestas.RespuestaEmpleado);

                    context.SaveChanges();
                    result.Correct = true;

                }
            }
            catch (Exception ex)
            {
                result.Correct = false; BL.LogReporteoClima.writeLogEncuestaClima(ex, new StackTrace());
                result.ErrorMessage = ex.Message;
            }
            return result;
        }


        public static ML.Result GetRespuestasCLByEmpleado(ML.EmpleadoRespuesta empleadoRes)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.EmpleadoRespuestas.SqlQuery("SELECT * FROM EmpleadoRespuestas WHERE IdEmpleado = {0} ORDER BY IdPregunta ASC", empleadoRes.Empleado.IdEmpleado).ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.ResEmpleado ResEmpleado = new ML.ResEmpleado();

                            ResEmpleado.Preguntas = new ML.Preguntas();
                            ResEmpleado.Preguntas.IdPregunta = Convert.ToInt32(obj.IdPregunta);
                            ResEmpleado.RespuestaEmpleado = obj.RespuestaEmpleado;

                            result.Objects.Add(ResEmpleado);

                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.Correct = false; BL.LogReporteoClima.writeLogEncuestaClima("La consulta no trajo ningun resultado", new StackTrace());
                        result.ErrorMessage = "La consulta no trajo ningun resultado";
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


        public static ML.Result GetRespuestasUsuarioP1(int IdEmpleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.GetRespuestasUsuarioP1(IdEmpleado).ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.EmpleadoRespuesta EmpleadoRespuestas = new ML.EmpleadoRespuesta();

                            EmpleadoRespuestas.Pregunta = new ML.Preguntas();
                            EmpleadoRespuestas.Pregunta.IdPregunta = Convert.ToInt32(obj.IdPregunta);
                            EmpleadoRespuestas.RespuestaEmpleado = obj.RespuestaEmpleado;

                            result.Objects.Add(EmpleadoRespuestas);
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

        public static ML.Result CleanP1(int IdEmpleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {

                    //Antes de insertar llamo al metodo para borrar y volver a escribir
                    context.CleanP1(IdEmpleado);
                    context.SaveChanges();
                    result.Correct = true;

                }
            }
            catch (Exception ex)
            {
                result.Correct = false; BL.LogReporteoClima.writeLogEncuestaClima(ex, new StackTrace());
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result CleanP2(int IdEmpleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {

                    //Antes de insertar llamo al metodo para borrar y volver a escribir
                    context.CleanP2(IdEmpleado);
                    context.SaveChanges();
                    result.Correct = true;

                }
            }
            catch (Exception ex)
            {
                result.Correct = false; BL.LogReporteoClima.writeLogEncuestaClima(ex, new StackTrace());
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result CleanP3(int IdEmpleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {

                    //Antes de insertar llamo al metodo para borrar y volver a escribir
                    context.CleanP3(IdEmpleado);
                    context.SaveChanges();
                    result.Correct = true;

                }
            }
            catch (Exception ex)
            {
                result.Correct = false; BL.LogReporteoClima.writeLogEncuestaClima(ex, new StackTrace());
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result CleanP4(int IdEmpleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.CleanP4(IdEmpleado);
                    context.SaveChanges();
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; BL.LogReporteoClima.writeLogEncuestaClima(ex, new StackTrace());
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result CleanP5(int IdEmpleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.CleanP5(IdEmpleado);
                    context.SaveChanges();
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; BL.LogReporteoClima.writeLogEncuestaClima(ex, new StackTrace());
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result CleanP6(int IdEmpleado) 
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.CleanP6(IdEmpleado);
                    context.SaveChanges();
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; BL.LogReporteoClima.writeLogEncuestaClima(ex, new StackTrace());
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result CleanP7(int IdEmpleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.CleanP7(IdEmpleado);
                    context.SaveChanges();
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; BL.LogReporteoClima.writeLogEncuestaClima(ex, new StackTrace());
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result CleanP8(int IdEmpleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.CleanP8(IdEmpleado);
                    context.SaveChanges();
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; BL.LogReporteoClima.writeLogEncuestaClima(ex, new StackTrace());
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result CleanP9(int IdEmpleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.CleanP9(IdEmpleado);
                    context.SaveChanges();
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; BL.LogReporteoClima.writeLogEncuestaClima(ex, new StackTrace());
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result CleanP9A(int IdEmpleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.CleanP9A(IdEmpleado);
                    context.SaveChanges();
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; BL.LogReporteoClima.writeLogEncuestaClima(ex, new StackTrace());
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result CleanP9B(int IdEmpleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.CleanP9B(IdEmpleado);
                    context.SaveChanges();
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; BL.LogReporteoClima.writeLogEncuestaClima(ex, new StackTrace());
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result CleanP10(int IdEmpleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.CleanP10(IdEmpleado);
                    context.SaveChanges();
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; BL.LogReporteoClima.writeLogEncuestaClima(ex, new StackTrace());
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result CleanP11(int IdEmpleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.CleanP11(IdEmpleado);
                    context.SaveChanges();
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; BL.LogReporteoClima.writeLogEncuestaClima(ex, new StackTrace());
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetResP1BD(ML.EmpleadoRespuesta empleadoRespuesta)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.GetResP1(empleadoRespuesta.Empleado.IdEmpleado).ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.EmpleadoRespuesta empRes = new ML.EmpleadoRespuesta();

                            empRes.Pregunta = new ML.Preguntas();
                            empRes.Pregunta.IdPregunta = Convert.ToInt32(obj.IdPregunta);
                            empRes.RespuestaEmpleado = obj.RespuestaEmpleado;

                            result.Objects.Add(empRes);

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

        public static ML.Result GetResP2BD(ML.EmpleadoRespuesta empleadoRespuesta)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.GetResP2(empleadoRespuesta.Empleado.IdEmpleado).ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach(var obj in query)
                        {
                            ML.EmpleadoRespuesta empRes = new ML.EmpleadoRespuesta();

                            empRes.Pregunta = new ML.Preguntas();
                            empRes.Pregunta.IdPregunta = Convert.ToInt32(obj.IdPregunta);
                            empRes.RespuestaEmpleado = obj.RespuestaEmpleado;

                            result.Objects.Add(empRes);

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

        public static ML.Result GetResP3BD(ML.EmpleadoRespuesta empleadoRespuesta)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.GetResP3(empleadoRespuesta.Empleado.IdEmpleado).ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.EmpleadoRespuesta empRes = new ML.EmpleadoRespuesta();

                            empRes.Pregunta = new ML.Preguntas();
                            empRes.Pregunta.IdPregunta = Convert.ToInt32(obj.IdPregunta);
                            empRes.RespuestaEmpleado = obj.RespuestaEmpleado;

                            result.Objects.Add(empRes);

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
        public static ML.Result GetResP4BD(ML.EmpleadoRespuesta empleadoRespuesta)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.GetResP4(empleadoRespuesta.Empleado.IdEmpleado).ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.EmpleadoRespuesta empRes = new ML.EmpleadoRespuesta();

                            empRes.Pregunta = new ML.Preguntas();
                            empRes.Pregunta.IdPregunta = Convert.ToInt32(obj.IdPregunta);
                            empRes.RespuestaEmpleado = obj.RespuestaEmpleado;

                            result.Objects.Add(empRes);

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

        public static ML.Result GetResP5BD(ML.EmpleadoRespuesta empleadoRespuesta)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.GetResP5(empleadoRespuesta.Empleado.IdEmpleado).ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.EmpleadoRespuesta empRes = new ML.EmpleadoRespuesta();

                            empRes.Pregunta = new ML.Preguntas();
                            empRes.Pregunta.IdPregunta = Convert.ToInt32(obj.IdPregunta);
                            empRes.RespuestaEmpleado = obj.RespuestaEmpleado;

                            result.Objects.Add(empRes);

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
        public static ML.Result GetResP6BD(ML.EmpleadoRespuesta empleadoRespuesta)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.GetResP6(empleadoRespuesta.Empleado.IdEmpleado).ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.EmpleadoRespuesta empRes = new ML.EmpleadoRespuesta();

                            empRes.Pregunta = new ML.Preguntas();
                            empRes.Pregunta.IdPregunta = Convert.ToInt32(obj.IdPregunta);
                            empRes.RespuestaEmpleado = obj.RespuestaEmpleado;

                            result.Objects.Add(empRes);

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
        public static ML.Result GetResP7BD(ML.EmpleadoRespuesta empleadoRespuesta)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.GetResP7(empleadoRespuesta.Empleado.IdEmpleado).ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.EmpleadoRespuesta empRes = new ML.EmpleadoRespuesta();

                            empRes.Pregunta = new ML.Preguntas();
                            empRes.Pregunta.IdPregunta = Convert.ToInt32(obj.IdPregunta);
                            empRes.RespuestaEmpleado = obj.RespuestaEmpleado;

                            result.Objects.Add(empRes);

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
        public static ML.Result GetResP8BD(ML.EmpleadoRespuesta empleadoRespuesta)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.GetResP8(empleadoRespuesta.Empleado.IdEmpleado).ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.EmpleadoRespuesta empRes = new ML.EmpleadoRespuesta();

                            empRes.Pregunta = new ML.Preguntas();
                            empRes.Pregunta.IdPregunta = Convert.ToInt32(obj.IdPregunta);
                            empRes.RespuestaEmpleado = obj.RespuestaEmpleado;

                            result.Objects.Add(empRes);

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
        public static ML.Result GetResP9BD(ML.EmpleadoRespuesta empleadoRespuesta)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.GetResP9(empleadoRespuesta.Empleado.IdEmpleado).ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.EmpleadoRespuesta empRes = new ML.EmpleadoRespuesta();

                            empRes.Pregunta = new ML.Preguntas();
                            empRes.Pregunta.IdPregunta = Convert.ToInt32(obj.IdPregunta);
                            empRes.RespuestaEmpleado = obj.RespuestaEmpleado;

                            result.Objects.Add(empRes);

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
        public static ML.Result GetResP9ABD(ML.EmpleadoRespuesta empleadoRespuesta)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.GetResP9A(empleadoRespuesta.Empleado.IdEmpleado).ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.EmpleadoRespuesta empRes = new ML.EmpleadoRespuesta();

                            empRes.Pregunta = new ML.Preguntas();
                            empRes.Pregunta.IdPregunta = Convert.ToInt32(obj.IdPregunta);
                            empRes.RespuestaEmpleado = obj.RespuestaEmpleado;

                            result.Objects.Add(empRes);

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
        public static ML.Result GetResP9BBD(ML.EmpleadoRespuesta empleadoRespuesta)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.GetResP9B(empleadoRespuesta.Empleado.IdEmpleado).ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.EmpleadoRespuesta empRes = new ML.EmpleadoRespuesta();

                            empRes.Pregunta = new ML.Preguntas();
                            empRes.Pregunta.IdPregunta = Convert.ToInt32(obj.IdPregunta);
                            empRes.RespuestaEmpleado = obj.RespuestaEmpleado;

                            result.Objects.Add(empRes);

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
        public static ML.Result GetResP10BD(ML.EmpleadoRespuesta empleadoRespuesta)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.GetResP10(empleadoRespuesta.Empleado.IdEmpleado).ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.EmpleadoRespuesta empRes = new ML.EmpleadoRespuesta();

                            empRes.Pregunta = new ML.Preguntas();
                            empRes.Pregunta.IdPregunta = Convert.ToInt32(obj.IdPregunta);
                            empRes.RespuestaEmpleado = obj.RespuestaEmpleado;

                            result.Objects.Add(empRes);

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

        public static ML.Result Prueba()
        {
            ML.Result result = new ML.Result();
            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                context.Database.Log = Console.Write;

                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {

                        var query = context.EstatusEncuestaAddOKK(1, 25);
                        context.SaveChanges();


                        var qyeri = context.Plantillas;
                        context.SaveChanges();//se guarda el cambio

                         

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        result.ErrorMessage = ex.Message;
                        transaction.Rollback();
                        Console.WriteLine("Error occurred.");
                    }
                }
            }
            return result;
        }
        public static string RegresaNombreRespuesta(int idRespuesta)
        {
            string respuesta = "";
            try
            {  
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var resultado = context.Respuestas.Single(x => x.IdRespuesta == idRespuesta);
                    respuesta = resultado.Respuesta.ToString();
                }
            }
            catch (Exception aE)
            {
                respuesta = "Error"+ aE.StackTrace.ToString();
            }

                    return respuesta;
        }

        public static bool ValidaDuplicadoRespuesta(int idUsuarioRespuesta, int idEncuesta)
        {
            try {
                    using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                    {
                    var query = context.UsuarioRespuestas.SqlQuery(" SELECT * FROM UsuarioRespuestas "+
                    "WHERE IdUsuario = {0} AND IdEncuesta = {1}",idUsuarioRespuesta,idEncuesta).ToList();
                    if (query.Count > 0)
                    {
                        return false;
                    }
                    else
                    {
                        return false;
                    }
                    
                    }
                }
            catch (Exception aE)
            {
                return false;
            }
            //return false;
        }

        public static ML.Result getDatosNube()
        {
            var result = new ML.Result();
            result.Objects = new List<object>();
            System.Data.DataSet ds = new System.Data.DataSet();
            try
            {
                var SqlQuery = @"SELECT TOP 85 RespuestaEmpleado, COUNT(200) AS mas_popular
                                FROM EmpleadoRespuestas
                                WHERE 
                                (EmpleadoRespuestas.IdPregunta = 173 and RespuestaEmpleado != 'NADA' and RespuestaEmpleado != 'SIN COMENTARIOS' and RespuestaEmpleado != 'NINGUNA' and RespuestaEmpleado != 'SIN COMENTARIO' and RespuestaEmpleado != 'NINGUNO' and RespuestaEmpleado != 'NO' and RespuestaEmpleado != 'TODO BIEN' and RespuestaEmpleado != 'SI' and RespuestaEmpleado != 'NO APLICA' and RespuestaEmpleado != 'NO LO SE' and RespuestaEmpleado != 'TODAS' and RespuestaEmpleado != 'N/A' and RespuestaEmpleado != '.' and RespuestaEmpleado != 'NO SE' and RespuestaEmpleado != 'TODO ESTA BIEN' and RespuestaEmpleado != 'NA' and RespuestaEmpleado != 'nada.' and RespuestaEmpleado != 'no cambiaria nada' and RespuestaEmpleado != '...' and RespuestaEmpleado != '....' and RespuestaEmpleado != '..' and RespuestaEmpleado != 'X' and RespuestaEmpleado != 'NOSE' and RespuestaEmpleado != 'ninguna.' and RespuestaEmpleado != 'No tiene' and RespuestaEmpleado != 'a' and RespuestaEmpleado != 'SIN COMENTAR' and RespuestaEmpleado != 'sin comentarios.' and RespuestaEmpleado != 'NADA TODO ESTA BIEN' and RespuestaEmpleado != '-' and RespuestaEmpleado != 'en nada' and RespuestaEmpleado != 'S/C' and RespuestaEmpleado != 'NO LOSE' and RespuestaEmpleado != 'si con mentario' AND RespuestaEmpleado != 'desconosco' AND RespuestaEmpleado != 'no se.') or 
                                (EmpleadoRespuestas.IdPregunta = 174 and RespuestaEmpleado != 'NADA' and RespuestaEmpleado != 'SIN COMENTARIOS' and RespuestaEmpleado != 'NINGUNA' and RespuestaEmpleado != 'SIN COMENTARIO' and RespuestaEmpleado != 'NINGUNO' and RespuestaEmpleado != 'NO' and RespuestaEmpleado != 'TODO BIEN' and RespuestaEmpleado != 'SI' and RespuestaEmpleado != 'NO APLICA' and RespuestaEmpleado != 'NO LO SE' and RespuestaEmpleado != 'TODAS' and RespuestaEmpleado != 'N/A' and RespuestaEmpleado != '.' and RespuestaEmpleado != 'NO SE' and RespuestaEmpleado != 'TODO ESTA BIEN' and RespuestaEmpleado != 'NA' and RespuestaEmpleado != 'nada.' and RespuestaEmpleado != 'no cambiaria nada' and RespuestaEmpleado != '...' and RespuestaEmpleado != '....' and RespuestaEmpleado != '..' and RespuestaEmpleado != 'X' and RespuestaEmpleado != 'NOSE' and RespuestaEmpleado != 'ninguna.' and RespuestaEmpleado != 'No tiene' and RespuestaEmpleado != 'a' and RespuestaEmpleado != 'SIN COMENTAR' and RespuestaEmpleado != 'sin comentarios.' and RespuestaEmpleado != 'NADA TODO ESTA BIEN' and RespuestaEmpleado != '-' and RespuestaEmpleado != 'en nada' and RespuestaEmpleado != 'S/C' and RespuestaEmpleado != 'NO LOSE' and RespuestaEmpleado != 'si con mentario' AND RespuestaEmpleado != 'desconosco' AND RespuestaEmpleado != 'no se.') or
                                (EmpleadoRespuestas.IdPregunta = 175 and RespuestaEmpleado != 'NADA' and RespuestaEmpleado != 'SIN COMENTARIOS' and RespuestaEmpleado != 'NINGUNA' and RespuestaEmpleado != 'SIN COMENTARIO' and RespuestaEmpleado != 'NINGUNO' and RespuestaEmpleado != 'NO' and RespuestaEmpleado != 'TODO BIEN' and RespuestaEmpleado != 'SI' and RespuestaEmpleado != 'NO APLICA' and RespuestaEmpleado != 'NO LO SE' and RespuestaEmpleado != 'TODAS' and RespuestaEmpleado != 'N/A' and RespuestaEmpleado != '.' and RespuestaEmpleado != 'NO SE' and RespuestaEmpleado != 'TODO ESTA BIEN' and RespuestaEmpleado != 'NA' and RespuestaEmpleado != 'nada.' and RespuestaEmpleado != 'no cambiaria nada' and RespuestaEmpleado != '...' and RespuestaEmpleado != '....' and RespuestaEmpleado != '..' and RespuestaEmpleado != 'X' and RespuestaEmpleado != 'NOSE' and RespuestaEmpleado != 'ninguna.' and RespuestaEmpleado != 'No tiene' and RespuestaEmpleado != 'a' and RespuestaEmpleado != 'SIN COMENTAR' and RespuestaEmpleado != 'sin comentarios.' and RespuestaEmpleado != 'NADA TODO ESTA BIEN' and RespuestaEmpleado != '-' and RespuestaEmpleado != 'en nada' and RespuestaEmpleado != 'S/C' and RespuestaEmpleado != 'NO LOSE' and RespuestaEmpleado != 'si con mentario' AND RespuestaEmpleado != 'desconosco' AND RespuestaEmpleado != 'no se.') or 
                                (EmpleadoRespuestas.IdPregunta = 176 and RespuestaEmpleado != 'NADA' and RespuestaEmpleado != 'SIN COMENTARIOS' and RespuestaEmpleado != 'NINGUNA' and RespuestaEmpleado != 'SIN COMENTARIO' and RespuestaEmpleado != 'NINGUNO' and RespuestaEmpleado != 'NO' and RespuestaEmpleado != 'TODO BIEN' and RespuestaEmpleado != 'SI' and RespuestaEmpleado != 'NO APLICA' and RespuestaEmpleado != 'NO LO SE' and RespuestaEmpleado != 'TODAS' and RespuestaEmpleado != 'N/A' and RespuestaEmpleado != '.' and RespuestaEmpleado != 'NO SE' and RespuestaEmpleado != 'TODO ESTA BIEN' and RespuestaEmpleado != 'NA' and RespuestaEmpleado != 'nada.' and RespuestaEmpleado != 'no cambiaria nada' and RespuestaEmpleado != '...' and RespuestaEmpleado != '....' and RespuestaEmpleado != '..' and RespuestaEmpleado != 'X' and RespuestaEmpleado != 'NOSE' and RespuestaEmpleado != 'ninguna.' and RespuestaEmpleado != 'No tiene' and RespuestaEmpleado != 'a' and RespuestaEmpleado != 'SIN COMENTAR' and RespuestaEmpleado != 'sin comentarios.' and RespuestaEmpleado != 'NADA TODO ESTA BIEN' and RespuestaEmpleado != '-' and RespuestaEmpleado != 'en nada' and RespuestaEmpleado != 'S/C' and RespuestaEmpleado != 'NO LOSE' and RespuestaEmpleado != 'si con mentario' AND RespuestaEmpleado != 'desconosco' AND RespuestaEmpleado != 'no se.')
                                group by EmpleadoRespuestas.RespuestaEmpleado
                                ORDER BY 2 DESC";
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    connection.Open();
                    SqlDataAdapter dat_1 = new System.Data.SqlClient.SqlDataAdapter(SqlQuery, connection);
                    dat_1.Fill(ds, "dat_1");
                }
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    var nube = new datosNube();
                    nube.Palabra = ds.Tables[0].Rows[i].ItemArray[0] == null ? "" : Convert.ToString(ds.Tables[0].Rows[i].ItemArray[0]);
                    nube.Frecuencia = ds.Tables[0].Rows[i].ItemArray[1] == null ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[1]);
                    result.Objects.Add(nube);
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false; BL.LogReporteoClima.writeLogEncuestaClima(ex, new StackTrace());
                result.ErrorMessage = ex.Message;
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                BL.LogReporteoClima.writteLog(result.ErrorMessage, new StackTrace());
            }
            return result;
        }
        public class datosNube
        {
            public int Frecuencia { get; set; }
            public string Palabra { get; set; }
        }
    }
}
