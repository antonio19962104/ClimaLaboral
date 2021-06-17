using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empleado
    {
        //Debo conocer la Id de la encuesta y Id Empleado
        public static ML.Result EstatusEncuestaAdd(ML.EstatusEncuesta estatusEncuesta)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.EstatusEncuestaAgregar(estatusEncuesta.Encuesta.IdEncuesta, estatusEncuesta.Empleado.IdEmpleado);
                    var lastIdEstatusEncuesta = context.EstatusEncuesta.Max(o => o.IdEstatusEncuesta);
                    /*
                     * El Año en Estatus encuesta debe ser acorde a lo que se haya configurado en ConfigClimaLab
                     * JAMG
                     * 12/04/2021
                     * Buscar a BD en la que esta el empleado
                     * Buscar la configuracion segun la encuesta 1 y el id de BD
                    */
                    var q1 = context.Empleado.Select(o => new { o.IdEmpleado, o.Nombre, o.ApellidoPaterno, o.ApellidoMaterno, o.IdBaseDeDatos }).Where(o => o.IdEmpleado == estatusEncuesta.Empleado.IdEmpleado).FirstOrDefault();
                    var idBD = q1.IdBaseDeDatos;

                    var q2 = context.ConfigClimaLab.Select(o => o).Where(o => o.IdBaseDeDatos == idBD && o.IdEncuesta == 1).FirstOrDefault();
                    
                    //context.Database.ExecuteSqlCommand("UPDATE ESTATUSENCUESTA SET ANIO = {0} WHERE IDESTATUSENCUESTA = {1}", DateTime.Now.Year, lastIdEstatusEncuesta);
                    context.Database.ExecuteSqlCommand("UPDATE ESTATUSENCUESTA SET ANIO = {0} WHERE IDESTATUSENCUESTA = {1}", q2.PeriodoAplicacion, lastIdEstatusEncuesta);
                    context.SaveChanges();

                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; BL.LogReporteoClima.writeLogEncuestaClima(ex, new StackTrace());
                BL.NLogGeneratorFile.logError(ex, new StackTrace());
                BL.NLogGeneratorFile.logError("No se creó el estatus de encuesta del con los siguientes parametros. IdEncuesta: " + estatusEncuesta.Encuesta.IdEncuesta + ". IdEmpleado: " + estatusEncuesta.Empleado.IdEmpleado, new StackTrace());
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result EstatusEncuestaUpdate(ML.EstatusEncuesta estatusEncuesta)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    context.EstatusEncuestaUpdate(estatusEncuesta.Encuesta.IdEncuesta, estatusEncuesta.Empleado.IdEmpleado);

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
        }//Este metodo no se utiliza

        public static ML.Result EstatusEncuestaFinal(ML.EstatusEncuesta estatusEncuesta)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.EstatusEncuestaSuccessOkOk(estatusEncuesta.Empleado.IdEmpleado, estatusEncuesta.Encuesta.IdEncuesta);

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

        public static ML.Result OpenSearch(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    if (empleado.Nombre == null)
                    {
                        empleado.Nombre = "";
                    }
                    if (empleado.ApellidoPaterno == null)
                    {
                        empleado.ApellidoPaterno = "";
                    }
                    if (empleado.ApellidoMaterno == null)
                    {
                        empleado.ApellidoMaterno = "";
                    }

                    var query = context.OpenSearchOK(empleado.Nombre, empleado.ApellidoPaterno, empleado.ApellidoMaterno).ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Empleado Empleado = new ML.Empleado();

                            //Empleado.IdEmpleado = item.IdEmpleado;
                            Empleado.IdEmpleado = item.IdEmpleado;
                            Empleado.Nombre = item.Nombre;
                            Empleado.ApellidoPaterno = item.ApellidoPaterno;
                            Empleado.ApellidoMaterno = item.ApellidoMaterno;
                            //Empleado.NumeroEmpleado = Convert.ToInt32(item.NumeroEmpleado);

                            Empleado.UnidadNegocio = item.UnidadNegocio;
                            Empleado.DivisonMarca = item.DivisionMarca;
                            Empleado.AreaAgencia = item.AreaAgencia;
                            Empleado.Depto = item.Depto;
                            Empleado.Subdepto = item.Subdepartamento;
                            Empleado.Correo = item.Correo;

                            result.Objects.Add(Empleado);
                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.ErrorMessage = "No se encontaron coincidencias";
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

        public static ML.Result GetById(int idempleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Empleado.SqlQuery("SELECT * FROM EMPLEADO WHERE IDEMPLEADO = {0}", idempleado).ToList();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Empleado emp = new ML.Empleado();

                            emp.Correo = item.Correo;
                            emp.Nombre = item.Nombre;
                            emp.ApellidoPaterno = item.ApellidoPaterno;
                            emp.ApellidoMaterno = item.ApellidoMaterno;

                            result.Object = emp;
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
        
        public static ML.Result GetNombreByIdEmpleado(int IdEmpleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Empleado.SqlQuery("SELECT * FROM EMPLEADO WHERE IDEMPLEADO = {0}", IdEmpleado).ToList();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Empleado empleado = new ML.Empleado();

                            empleado.Nombre = item.Nombre;
                            empleado.ApellidoPaterno = item.ApellidoPaterno;
                            empleado.ApellidoMaterno = item.ApellidoMaterno;

                            result.Object = empleado;

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

        public static ML.Result AddForAdmin(ML.Administrador emp)
        {
            ML.Result result = new ML.Result();
            int getIdEmpleado = 0;
            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        //Si el empleado segun su email ya existe solo tomo su id
                        //Si el empleado no existe agregarlo
                        var exists = context.Empleado.SqlQuery("select * from Empleado	WHERE Empleado.Correo = {0} and IdBaseDeDatos is null", emp.Correo).ToList();

                        if (exists.Count == 0)//No existe
                        {
                            getIdEmpleado = context.Empleado.Max(p => p.IdEmpleado);
                            getIdEmpleado = getIdEmpleado + 1;
                            var query = context.Database.ExecuteSqlCommand("INSERT INTO Empleado (IdEmpleado, Nombre, ApellidoPaterno, ApellidoMaterno, Correo, UnidadNegocio, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8})",
                                                                                                    getIdEmpleado, emp.Nombre, emp.ApellidoPaterno, emp.ApellidoMaterno, emp.Correo, emp.UNegocio, DateTime.Now, emp.CURRENT_USER, "Diagnostic4U");
                        }
                        else if (exists.Count > 0)
                        {
                            foreach (var item in exists)
                            {
                                ML.Empleado empl = new ML.Empleado();
                                empl.IdEmpleado = Convert.ToInt32(item.IdEmpleado);

                                getIdEmpleado = empl.IdEmpleado;
                            }
                        }




                        result.IdEmpleadoFromSP = getIdEmpleado;
                        context.SaveChanges();
                        transaction.Commit();
                        result.Correct = true;
                    }
                    catch (Exception ex)
                    {
                        result.Correct = false; BL.LogReporteoClima.writeLogEncuestaClima(ex, new StackTrace());
                        result.ErrorMessage = ex.Message;
                        transaction.Rollback();
                    }
                }
            }
            return result;
        }

        //GetAll Of Demograficos
        public static ML.Result GetAllCondicionTrabajo()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.CondicionTrabajo.SqlQuery("SELECT * FROM CONDICIONTRABAJO").ToList();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.CondicionTrabajo condicon = new ML.CondicionTrabajo();

                            condicon.IdCondicionTrabajo = item.IdCondicionTrabajo;
                            condicon.Descripcion = item.Descripcion;

                            result.Objects.Add(condicon);
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
        public static ML.Result GetAllGradoAcademico()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.GradoAcademico.SqlQuery("SELECT * FROM GradoAcademico").ToList();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.GradoAcademico grado = new ML.GradoAcademico();

                            grado.IdGradoAcademico = item.IdGradoAcademico;
                            grado.Descripcion = item.Descripcion;

                            result.Objects.Add(grado);
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
        public static ML.Result GetAllAntiguedad()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Antiguedad.SqlQuery("SELECT * FROM ANTIGUEDAD").ToList();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Antiguedad ant = new ML.Antiguedad();

                            ant.IdAntiguedad = item.IdAndtiguedad;
                            ant.Descripcion = item.Descripcion;

                            result.Objects.Add(ant);
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
        public static ML.Result GetAllRangoEdad()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.RangoEdad.SqlQuery("SELECT * FROM RangoEdad").ToList();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.RangoEdad rangoEdad = new ML.RangoEdad();

                            rangoEdad.IdRangoEdad = item.IdRangoEdad;
                            rangoEdad.Descripcion = item.Descripcion;

                            result.Objects.Add(rangoEdad);
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

        public static ML.Result GetEmpleadoByIdDatabase(ML.ConfigClimaLab conf)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Empleado.SqlQuery("SELECT * FROM EMPLEADO WHERE IdBaseDeDatos = {0} and EstatusEmpleado = 'Activo'", conf.IdDatabase).ToList();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Empleado emp = new ML.Empleado();

                            emp.IdEmpleado = item.IdEmpleado;
                            emp.Nombre = item.Nombre;
                            emp.ApellidoPaterno = item.ApellidoPaterno;
                            emp.ApellidoMaterno = item.ApellidoMaterno;
                            emp.UnidadNegocio = item.UnidadNegocio;
                            emp.ClavesAcceso = new ML.ClavesAcceso();
                            emp.ClavesAcceso.ClaveAcceso = item.ClaveAcceso;
                            emp.Correo = item.Correo;

                            result.Objects.Add(emp);
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

        public static ML.Result Update(DL.Empleado item, DL.RH_DesEntities context)
        {
            ML.Result result = new ML.Result();
            try
            {
                var itemClave = new DL.ClavesAcceso();
                itemClave.ClaveAcceso = item.ClaveAcceso;
                var existClave = context.ClavesAcceso.SqlQuery("select * from ClavesAcceso where claveacceso = {0}", item.ClaveAcceso).ToList();
                if (existClave.Count == 0)
                {
                    context.ClavesAcceso.Add(itemClave);
                    context.SaveChanges();
                }
                
                var query = context.Database.ExecuteSqlCommand("UPDATE empleado SET " +
                                "Nombre = {0}, ApellidoPaterno = {1}, ApellidoMaterno = {2}, " +
                                "Puesto = {3}, FechaNacimiento = {4}, FechaAntiguedad = {5}, " +
                                "Sexo = {6}, Correo = {7}, TipoFuncion = {8}, CondicionTrabajo = {9}, " +
                                "GradoAcademico = {10}, UnidadNegocio = {11}, " +
                                "DivisionMarca = {12}, AreaAgencia = {13}, Depto = {14}, " +
                                "Subdepartamento = {15}, EmpresaContratante = {16}, " +
                                "IdResponsableRH = {17}, NombreResponsableRH = {18}, " +
                                "IdJefe = {19}, NombreJefe = {20}, PuestoJefe = {21}, " +
                                "IdResponsableEstructura = {22}, NombreResponsableEstructura = {23}, " +
                                "ClaveAcceso = {24}, RangoAntiguedad = {25}, RangoEdad = {26}, " +
                                "EstatusEmpleado = {27}, IdEmpleadoRH = {29}   " +
                                " WHERE idempleado = {28}",
                                item.Nombre, item.ApellidoPaterno, item.ApellidoMaterno,
                                item.Puesto, item.FechaNacimiento, item.FechaAntiguedad,
                                item.Sexo, item.Correo, item.TipoFuncion, item.CondicionTrabajo,
                                item.GradoAcademico, item.UnidadNegocio, item.DivisionMarca,
                                item.AreaAgencia, item.Departamento, item.Subdepartamento,
                                item.EmpresaContratante, item.IdResponsableRH,
                                item.NombreResponsableRH, item.IdJefe, item.NombreJefe, item.PuestoJefe,
                                item.IdResponsableEstructura, item.NombreResponsableEstructura,
                                item.ClaveAcceso, item.RangoAntiguedad, item.RangoEdad,
                                item.EstatusEmpleado, item.IdEmpleado, item.IdEmpleadoRH);
                result.Correct = true;
                //context.SaveChanges();
                //transaction.Commit();
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
