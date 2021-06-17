using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Login
    {
        public static ML.Result Autenticacion(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            //empleado.ClavesAcceso = new ML.ClavesAcceso();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Empleado.SqlQuery("select * from empleado where Empleado.ClaveAcceso = {0} COLLATE Latin1_General_CS_AS", empleado.ClavesAcceso.ClaveAcceso).ToList();
                    
                    //Inner join con BaseDeDatos para obtener fecha fin e inicio

                    if (query.Count() > 0)
                    {
                        result.Correct = true;

                        foreach(var obj in query)
                        {
                            empleado.IdEmpleado = obj.IdEmpleado;
                        }

                        //Validar BD
                        var queryDB = 
                            context.Empleado.SqlQuery
                            ("select * from Empleado INNER JOIN BasesDeDatos ON Empleado.IdBaseDeDatos = BasesDeDatos.IdBasesDeDatos WHERE IdEmpleado = {0}", empleado.IdEmpleado).ToList();

                        if (queryDB.Count() > 0)
                        {
                            foreach (var item in queryDB)
                            {
                                ML.Empleado emp = new ML.Empleado();
                                emp.BaseDeDatos = new ML.BasesDeDatos();
                                emp.BaseDeDatos.IdBaseDeDatos = item.IdBaseDeDatos;

                                result.Object = emp.BaseDeDatos.IdBaseDeDatos;
                            }
                            //Query periodo
                            int IdDatabase = Convert.ToInt32(result.Object);
                            var queryPerdiodo = context.ConfigClimaLab.SqlQuery("SELECT * FROM ConfigClimaLab WHERE IdBaseDeDatos = {0}", IdDatabase).ToList();
                            if (queryPerdiodo.Count > 0)
                            {
                                foreach (var item in queryPerdiodo)
                                {
                                    ML.ConfigClimaLab conf = new ML.ConfigClimaLab();
                                    conf.FechaInicio = Convert.ToDateTime(item.FechaInicio);
                                    conf.FechaFin = Convert.ToDateTime(item.FechaFin);

                                    result.InicioCL = conf.FechaInicio;
                                    result.FinCL = conf.FechaFin;
                                }
                            }
                            else
                            {
                                result.Correct = false;
                            }

                        }else
                        {
                            result.Correct = false;
                        }
                    }
                    else
                    {
                        result.ErrorMessage = "Clave de acceso no válida";
                        result.Correct = false;
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

        public static ML.Result AutenticacionEstatus(ML.EstatusEncuesta estatusEncuesta)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.LoginEstatusOk(estatusEncuesta.Empleado.IdEmpleado, estatusEncuesta.Encuesta.IdEncuesta).ToList();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            
                            estatusEncuesta.Estatus = obj.Estatus;

                            //result.Objects.Add(estatusEncuesta);
                            result.Correct = true;
                        }
                    }
                    estatusEncuesta = null;
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
