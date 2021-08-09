using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BasesDeDatos
    {
        public static ML.Result getBaseDeDatosAll()
        {
            ML.Result result = new ML.Result();
            try {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var idParametro = 1;
                    var query = context.BasesDeDatos.SqlQuery("SELECT * FROM BasesDeDatos where IdEstatus = {0}",idParametro);
                    result.ListadoDeBaseDeDatos = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            //bases.IdBaseDeDatos = obj.IdBasesDeDatos;
                            //bases.Nombre = obj.Nombre;
                            //bases.TipoEstatus = new ML.TipoEstatus();
                            //bases.TipoEstatus.IdEstatus = obj.TipoEstatus.IdEstatus;
                            //bases.TipoEstatus.Descripcion = obj.TipoEstatus.Descripcion;
                            //bases.TipoBD = new ML.TipoBD();
                            //bases.TipoBD.IdTipoBD = obj.TipoBD.IdTipoBD;

                            ML.BasesDeDatos bases = new ML.BasesDeDatos();
                            bases.IdBaseDeDatos = obj.IdBasesDeDatos;
                            bases.Nombre = obj.Nombre;
                            bases.TipoEstatus = new ML.TipoEstatus();
                            bases.TipoEstatus.IdEstatus = obj.TipoEstatus.IdEstatus;
                            bases.TipoEstatus.Descripcion = obj.TipoEstatus.Descripcion;
                            bases.TipoBD = new ML.TipoBD();
                            bases.TipoBD.IdTipoBD = Convert.ToInt32(obj.IdTipoBD);
                            result.ListadoDeBaseDeDatos.Add(bases);
                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage ="No se encuentran Bases de Datos Registradas";
                    }

                }
            }
            catch (Exception aE)
            {
                result.Correct = false;
                result.ErrorMessage = aE.Message.ToString();
            }
            return result;
        }//Esta
        public static ML.Result getBaseDeDatosGenerica()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var idParametro = 1;
                    var query = context.BasesDeDatos.SqlQuery("SELECT * FROM BasesDeDatos where IdEstatus = {0} and IdTipoEncuesta = 3 AND BasesDeDatos.IdTipoBD = 2", idParametro);
                    result.ListadoDeBaseDeDatos = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.BasesDeDatos bases = new ML.BasesDeDatos();
                            bases.IdBaseDeDatos = obj.IdBasesDeDatos;
                            bases.Nombre = obj.Nombre;
                            bases.TipoEstatus = new ML.TipoEstatus();
                            bases.TipoEstatus.IdEstatus = obj.TipoEstatus.IdEstatus;
                            bases.TipoEstatus.Descripcion = obj.TipoEstatus.Descripcion;
                            result.ListadoDeBaseDeDatos.Add(bases);
                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encuentran Bases de Datos Registradas";
                    }

                }
            }
            catch (Exception aE)
            {
                result.Correct = false;
                result.ErrorMessage = aE.Message.ToString();
            }
            return result;
        }
        public static ML.Result getBaseDeDatosAnonima()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var idParametro = 1;
                    var query = context.BasesDeDatos.SqlQuery("SELECT * FROM BasesDeDatos "+
                        " INNER JOIN TipoEncuesta ON BasesDeDatos.IdTipoEncuesta = TipoEncuesta.IdTipoEncuesta "+
                        " where BasesDeDatos.IdEstatus ={0} AND BasesDeDatos.IdTipoBD = 2", idParametro);
                    result.ListadoDeBaseDeDatos = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.BasesDeDatos bases = new ML.BasesDeDatos();
                            bases.IdBaseDeDatos = obj.IdBasesDeDatos;
                            string concat = obj.TipoEncuesta.NombreTipoDeEncuesta;

                            bases.Nombre = obj.Nombre + " / " + concat;
                            //bases.TipoEncuesta = new ML.TipoEncuesta();
                            
                            bases.TipoEstatus = new ML.TipoEstatus();
                            bases.TipoEstatus.IdEstatus = obj.TipoEstatus.IdEstatus;
                            bases.TipoEstatus.Descripcion = obj.TipoEstatus.Descripcion;
                            result.ListadoDeBaseDeDatos.Add(bases);
                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encuentran Bases de Datos Registradas";
                    }

                }
            }
            catch (Exception aE)
            {
                result.Correct = false;
                result.ErrorMessage = aE.Message.ToString();
            }
            return result;
        }
        public static ML.Result getBaseDeDatosConfidencial()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var idParametro = 1;
                    var query = context.BasesDeDatos.SqlQuery("SELECT * FROM BasesDeDatos where IdEstatus = {0} and IdTipoEncuesta = 2 AND BasesDeDatos.IdTipoBD = 2", idParametro);
                    result.ListadoDeBaseDeDatos = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.BasesDeDatos bases = new ML.BasesDeDatos();
                            bases.IdBaseDeDatos = obj.IdBasesDeDatos;
                            bases.Nombre = obj.Nombre;
                            bases.TipoEstatus = new ML.TipoEstatus();
                            bases.TipoEstatus.IdEstatus = obj.TipoEstatus.IdEstatus;
                            bases.TipoEstatus.Descripcion = obj.TipoEstatus.Descripcion;
                            result.ListadoDeBaseDeDatos.Add(bases);
                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encuentran Bases de Datos Registradas";
                    }

                }
            }
            catch (Exception aE)
            {
                result.Correct = false;
                result.ErrorMessage = aE.Message.ToString();
            }
            return result;
        }

        public static ML.Result getBaseDeDatosAllForListado(List<object> permisosEstrucura, int miEmpresaorigen)
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
                        string CompanyIdFinal = " OR Company.CompanyId = " + CompanyId + " and BasesDeDatos.IdEstatus = 1 or BasesDeDatos.IdEstatus = 2 ";
                        WHEREsql += CompanyIdFinal;
                    }
                    WHEREsql += " OR COMPANY.COMPANYID = " + miEmpresaorigen + " and BasesDeDatos.IdEstatus = 1 or BasesDeDatos.IdEstatus = 2 "; 

                    Console.WriteLine(WHEREsql);

                    //End Get CompanyId
                    var idParametro = 1;
                    var query = context.BasesDeDatos.SqlQuery
                        ("SELECT * FROM BasesDeDatos INNER JOIN TIPOBD ON BASESDEDATOS.IDTIPOBD = TIPOBD.IDTIPOBD INNER JOIN Administrador ON BasesDeDatos.IdAdministradorCreate = Administrador.IdAdministrador INNER JOIN Empleado ON Administrador.IdEmpleado = Empleado.IdEmpleado INNER JOIN Company ON Administrador.CompanyId = Company.CompanyId " +
                        " where Company.CompanyId = 0  " + WHEREsql);

                    
                    result.ListadoDeBaseDeDatos = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.BasesDeDatos bases = new ML.BasesDeDatos();
                            bases.IdBaseDeDatos = obj.IdBasesDeDatos;
                            bases.Nombre = obj.Nombre;
                            bases.TipoEstatus = new ML.TipoEstatus();
                            bases.TipoEstatus.IdEstatus = obj.TipoEstatus.IdEstatus;
                            bases.TipoEstatus.Descripcion = obj.TipoEstatus.Descripcion;
                            bases.TipoBD = new ML.TipoBD();
                            bases.TipoBD.IdTipoBD = obj.TipoBD.IdTipoBD;
                            bases.TipoBD.Descripcion = obj.TipoBD.Descripcion;
                            result.ListadoDeBaseDeDatos.Add(bases);
                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encuentran Bases de Datos Registradas";
                    }

                }
            }
            catch (Exception aE)
            {
                result.Correct = false;
                result.ErrorMessage = aE.Message.ToString();
            }
            return result;
        }

        public static ML.Result GetDataFromIdBD(int IdBaseDeDatos, int IdTipoBD)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {


                    if (IdTipoBD == 2)
                    {
                        var query = context.Usuario.SqlQuery("SELECT * FROM Usuario LEFT JOIN TipoEstatus ON USUARIO.IDESTATUS = TIPOESTATUS.IDESTATUS LEFT JOIN Empleado ON Usuario.IdEmpleado = Empleado.IdEmpleado LEFT JOIN Perfil ON USUARIO.IDPERFIL = PERFIL.IDPERFIL INNER JOIN BasesDeDatos ON USUARIO.IdBaseDeDatos = BASESDEDATOS.IdBasesDeDatos WHERE (USUARIO.IdBaseDeDatos = {0} and usuario.idestatus = 1) or (USUARIO.IdBaseDeDatos = {0} and usuario.idestatus = 2)", IdBaseDeDatos).ToList();
                        result.Objects = new List<object>();

                        if (query != null)
                        {
                            foreach (var item in query)
                            {
                                ML.Usuario usuario = new ML.Usuario();
                                usuario.EstatusEncuesta = new ML.EstatusEncuesta();
                                usuario.TipoEstatus = new ML.TipoEstatus();
                                usuario.Empleado = new ML.Empleado();
                                usuario.Perfil = new ML.Perfil();
                                usuario.BaseDeDatos = new ML.BasesDeDatos();

                                usuario.BaseDeDatos.Nombre = item.BasesDeDatos.Nombre;

                                usuario.IdUsuario = item.IdUsuario;
                                usuario.Nombre = item.Nombre;
                                usuario.ApellidoPaterno = item.ApellidoPaterno;
                                usuario.ApellidoMaterno = item.ApellidoMaterno;
                                usuario.Username = item.UserName;
                                usuario.Password = item.Password;
                                usuario.Empleado.IdEmpleado = Convert.ToInt32(item.IdEmpleado);
                                //usuario.Perfil.Descripcion = item.Perfil.Descripcion;
                                usuario.Puesto = item.Puesto;


                                usuario.cadenaDateNacimiento = Convert.ToString(item.FechaNacimiento);
                                usuario.cadenaDateAntiguedad = Convert.ToString(item.FechaAntiguedad);

                                usuario.cadenaDateNacimiento = usuario.cadenaDateNacimiento.Substring(0, 10);
                                usuario.cadenaDateAntiguedad = usuario.cadenaDateAntiguedad.Substring(0, 10);

                                usuario.Sexo = item.Sexo;
                                usuario.Email = item.Email;
                                usuario.TipoFuncion = item.TipoFuncion;
                                usuario.CondicionTrabajo = item.CondicionTrabajo;
                                usuario.GradoAcademico = item.GradoAcademico;
                                usuario.UnidadNegocio = item.UnidadNegocio;
                                usuario.DivisionMarca = item.DivisionMarca;
                                usuario.AreaAgencia = item.AreaAgencia;
                                usuario.Departamento = item.Departamento;
                                usuario.Subdepartamento = item.Subdepartamento;
                                usuario.EmpresaContratante = item.EmpresaContratante;
                                usuario.IdResponsableRH = Convert.ToInt32(item.IdResponsableRH);
                                usuario.NombreResponsableRH = item.NombreResponsableRH;
                                usuario.IdJefe = Convert.ToInt32(item.IdJefe);
                                usuario.NombreJefe = item.NombreJefe;
                                usuario.PuestoJefe = item.PuestoJefe;
                                usuario.IdRespinsableEstructura = Convert.ToInt32(item.IdResponsableEstructura);
                                usuario.NombreResponsableEstructura = item.NombreResponsableEstructura;

                                usuario.ClaveAcceso = item.ClaveAcceso;

                                usuario.RangoAntiguedad = item.RangoAntiguedad;
                                usuario.RangoEdad = item.RangoEdad;
                                //usuario.EstatusEncuesta.Estatus = item.EstatusEncuesta.Estatus;
                                usuario.TipoEstatus.Descripcion = item.TipoEstatus.Descripcion;

                                usuario.CampoDeTexto_1 = item.CampoDeTexto_1;
                                usuario.CampoDeTexto_2 = item.CampoDeTexto_2;
                                usuario.CampoDeTexto_3 = item.CampoDeTexto_3;
                                usuario.CampoNumerico_1 = Convert.ToInt32(item.CampoNumerico_1);
                                usuario.CampoNumerico_2 = Convert.ToInt32(item.CampoNumerico_2);
                                usuario.CampoNumerico_3 = Convert.ToInt32(item.CampoNumerico_3);

                                result.Objects.Add(usuario);
                                result.Correct = true;
                            }
                        }
                    }
                    else if (IdTipoBD == 1)
                    {
                        var query = context.Empleado.SqlQuery("SELECT * FROM Empleado LEFT JOIN Perfil ON Empleado.IDPERFIL = PERFIL.IDPERFIL INNER JOIN BasesDeDatos ON Empleado.IdBaseDeDatos = BASESDEDATOS.IdBasesDeDatos WHERE Empleado.IdBaseDeDatos = {0} and Empleado.EstatusEmpleado != 'Eliminado'", IdBaseDeDatos).ToList();
                        result.Objects = new List<object>();

                        if (query != null)
                        {
                            foreach (var item in query)
                            {
                                ML.Empleado empleado = new ML.Empleado();
                                empleado.EstatusEncuesta = new ML.EstatusEncuesta();
                                empleado.Perfil = new ML.Perfil();
                                empleado.BaseDeDatos = new ML.BasesDeDatos();

                                empleado.BaseDeDatos.Nombre = item.BasesDeDatos.Nombre;
                                
                                empleado.IdEmpleado = item.IdEmpleado;
                                empleado.IdEmpleadoRH = (int)item.IdEmpleadoRH;
                                empleado.Nombre = item.Nombre;
                                empleado.ApellidoPaterno = item.ApellidoPaterno;
                                empleado.ApellidoMaterno = item.ApellidoMaterno;
                                
                                
                                empleado.Puesto = item.Puesto;

                                empleado.dateNacim = Convert.ToString(item.FechaNacimiento);
                                empleado.dateAntig = Convert.ToString(item.FechaAntiguedad);

                                empleado.dateNacim = empleado.dateNacim.Substring(0, 10);
                                empleado.dateAntig = empleado.dateAntig.Substring(0, 10);

                                empleado.Sexo = item.Sexo;
                                empleado.Correo = item.Correo;
                                empleado.TipoFuncion = item.TipoFuncion;
                                empleado.CondicionTrabajo = item.CondicionTrabajo;
                                empleado.GradoAcademico = item.GradoAcademico;
                                empleado.UnidadNegocio = item.UnidadNegocio;
                                empleado.DivisonMarca = item.DivisionMarca;
                                empleado.AreaAgencia = item.AreaAgencia;
                                empleado.Depto = item.Depto;
                                empleado.Subdepto = item.Subdepartamento;
                                empleado.EmpresaContratante = item.EmpresaContratante;
                                empleado.IdResponsableRH = Convert.ToInt32(item.IdResponsableRH);
                                empleado.NombreResponsableRH = item.NombreResponsableRH;
                                empleado.IdJefe = Convert.ToInt32(item.IdJefe);
                                empleado.NombreJefe = item.NombreJefe;
                                empleado.PuestoJefe = item.PuestoJefe;
                                empleado.IdRespinsableEstructura = Convert.ToInt32(item.IdResponsableEstructura);
                                empleado.NombreResponsableEstrucutra = item.NombreResponsableEstructura;

                                empleado.ClavesAcceso = new ML.ClavesAcceso();
                                empleado.ClavesAcceso.ClaveAcceso = item.ClaveAcceso;

                                empleado.RangoAntiguedad = item.RangoAntiguedad;
                                empleado.RangoEdad = item.RangoEdad;
                                //usuario.EstatusEncuesta.Estatus = item.EstatusEncuesta.Estatus;
                                empleado.EstatusEmpleado = item.EstatusEmpleado;


                                result.Objects.Add(empleado);
                                result.Correct = true;
                            }
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

        public static ML.Result Add(ML.BasesDeDatos BD, int idadmincreate, string currentuser)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Database.ExecuteSqlCommand("INSERT INTO BasesDeDatos (NOMBRE, IDESTATUS, idadministradorCreate, fechahoracreacion, usuariocreacion,programacreacion, IdTipoEncuesta, IdTipoBD) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7})", 
                                                                                            BD.Nombre, BD.TipoEstatus.IdEstatus, idadmincreate, DateTime.Now, currentuser, "Diagnostic4U", BD.TipoEncuesta.IdTipoEncuesta, BD.TipoBD.IdTipoBD);

                    context.SaveChanges();
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result UpdateEstatus(ML.BasesDeDatos BD, string CURRENTUser)
        {
            ML.Result result = new ML.Result();

            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (BD.IdentificadorEstatus == 1)
                        {
                            var query = context.Database.ExecuteSqlCommand("UPDATE BasesDeDatos SET IDESTATUS = {0}, FechaHoraModificacion = {2}, UsuarioModificacion = {3}, ProgramaModificacion = {4} WHERE IdBasesDeDatos = {1}", 2, BD.IdBaseDeDatos, DateTime.Now, CURRENTUser, "Diagnostic4U");
                            transaction.Commit();
                            context.SaveChanges();
                        }
                        else if (BD.IdentificadorEstatus == 2)
                        {
                            var query = context.Database.ExecuteSqlCommand("UPDATE BasesDeDatos SET IDESTATUS = {0}, FechaHoraModificacion = {2}, UsuarioModificacion = {3}, ProgramaModificacion = {4} WHERE IdBasesDeDatos = {1}", 1, BD.IdBaseDeDatos, DateTime.Now, CURRENTUser, "Diagnostic4U");
                            transaction.Commit();
                            context.SaveChanges();
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

        public static ML.Result Delete(ML.BasesDeDatos BD, string CURRENTUser)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Database.ExecuteSqlCommand("UPDATE BasesDeDatos SET IDESTATUS = {0}, FechaHoraEliminacion = {2}, UsuarioEliminacion = {3}, ProgramaEliminacion = {4} WHERE IdBasesDeDatos = {1}", 3, BD.IdBaseDeDatos, DateTime.Now, CURRENTUser, "Diagnostic4U");

                    var tipoBD = context.BasesDeDatos.SqlQuery("select * from BasesDeDatos where IdBasesDeDatos = {0}", BD.IdBaseDeDatos);
                    int tipo = 0;
                    foreach (var item in tipoBD)
                    {
                        tipo = (int) item.IdTipoBD;
                    }
                    if (tipo == 1)//Emple
                    {
                        context.Database.ExecuteSqlCommand("UPDATE Empleado SET EstatusEmpleado = 'Inactivo' FROM Empleado WHERE Empleado.IdBaseDeDatos = {0}", BD.IdBaseDeDatos);
                    }

                    context.SaveChanges();
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetEliminadas(List<object> permisosEstrucura)
        {
            ML.Result result = new ML.Result();
            result.ListadoDeBaseDeDatos = new List<object>();

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
                        string CompanyIdFinal = " OR Company.CompanyId = " + CompanyId + " and BasesDeDatos.IdEstatus = 3 ";
                        WHEREsql += CompanyIdFinal;
                    }

                    Console.WriteLine(WHEREsql);


                    var query = context.BasesDeDatos.SqlQuery("SELECT * FROM BasesDeDatos INNER JOIN Administrador ON BasesDeDatos.IdAdministradorCreate = Administrador.IdAdministrador INNER JOIN Empleado ON Administrador.IdEmpleado = Empleado.IdEmpleado INNER JOIN Company ON Administrador.CompanyId = Company.CompanyId " +
                        " where Company.CompanyId = 0  " + WHEREsql).ToList();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.BasesDeDatos bd = new ML.BasesDeDatos();

                            bd.IdBaseDeDatos = item.IdBasesDeDatos;
                            bd.Nombre = item.Nombre;
                            bd.TipoEstatus = new ML.TipoEstatus();
                            bd.TipoEstatus.IdEstatus = item.TipoEstatus.IdEstatus;
                            bd.TipoEstatus.Descripcion = item.TipoEstatus.Descripcion;

                            result.ListadoDeBaseDeDatos.Add(bd);
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

        public static ML.Result Restaurar(ML.BasesDeDatos BD, string CUrrentUser)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Database.ExecuteSqlCommand("UPDATE BasesDeDatos SET IDESTATUS = 1, FechaHoraModificacion = {0}, UsuarioModificacion = {1}, ProgramaModificacion = {2} WHERE IdBasesDeDatos = {3}", DateTime.Now, CUrrentUser, "Diagnostic4U", BD.IdBaseDeDatos);

                    var tipoBD = context.BasesDeDatos.SqlQuery("select * from BasesDeDatos where IdBasesDeDatos = {0}", BD.IdBaseDeDatos);
                    int tipo = 0;
                    foreach (var item in tipoBD)
                    {
                        tipo = (int)item.IdTipoBD;
                    }
                    if (tipo == 1)//Emple
                    {
                        context.Database.ExecuteSqlCommand("UPDATE Empleado SET EstatusEmpleado = 'Activo' FROM Empleado WHERE Empleado.IdBaseDeDatos = {0}", BD.IdBaseDeDatos);
                    }

                    context.SaveChanges();
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Encuesta GetBDClima()
        {
            ML.Result result = new ML.Result();
            ML.Encuesta encuesta = new ML.Encuesta();
            encuesta.Objetos = new List<object>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.BasesDeDatos.SqlQuery("SELECT * FROM BasesDeDatos where IdTipoBD = 1 AND IDESTATUS = 1").ToList();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.BasesDeDatos bd = new ML.BasesDeDatos();
                            bd.IdBaseDeDatos = item.IdBasesDeDatos;
                            bd.Nombre = item.Nombre;

                            encuesta.Objetos.Add(bd);
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
            return encuesta;
        }

        //GetBDByPermisos
        public static ML.Result getBaseDeDatosAnonimaByPermisos(List<object> permisosEstrucura, int miEmpresaorigen)
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
                        string CompanyIdFinal = " OR Company.CompanyId = " + CompanyId + " and BasesDeDatos.IdEstatus = 1 ";
                        WHEREsql += CompanyIdFinal;
                    }
                    WHEREsql += " OR Company.CompanyId = " + miEmpresaorigen + " and BasesDeDatos.IdEstatus = 1 ";
                    //End Get CompanyId
                    var idParametro = 1;
                    var query = context.BasesDeDatos.SqlQuery
                        ("SELECT * FROM BasesDeDatos INNER JOIN TIPOENCUESTA ON BASESDEDATOS.IDTIPOENCUESTA = TIPOENCUESTA.IDTIPOENCUESTA INNER JOIN TIPOBD ON BASESDEDATOS.IDTIPOBD = TIPOBD.IDTIPOBD INNER JOIN Administrador ON BasesDeDatos.IdAdministradorCreate = Administrador.IdAdministrador INNER JOIN Empleado ON Administrador.IdEmpleado = Empleado.IdEmpleado INNER JOIN Company ON Administrador.CompanyId = Company.CompanyId " +
                        " where Company.CompanyId = 0  " + WHEREsql);

                    result.ListadoDeBaseDeDatos = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.BasesDeDatos bases = new ML.BasesDeDatos();
                            bases.IdBaseDeDatos = obj.IdBasesDeDatos;
                            bases.Nombre = obj.Nombre + " / Anónima";
                            bases.TipoEstatus = new ML.TipoEstatus();
                            bases.TipoEstatus.IdEstatus = obj.TipoEstatus.IdEstatus;
                            bases.TipoEstatus.Descripcion = obj.TipoEstatus.Descripcion;
                            bases.TipoBD = new ML.TipoBD();
                            bases.TipoBD.IdTipoBD = obj.TipoBD.IdTipoBD;
                            bases.TipoBD.Descripcion = obj.TipoBD.Descripcion;
                            bases.TipoEncuesta = new ML.TipoEncuesta();
                            bases.TipoEncuesta.IdTipoEncuesta = obj.IdTipoEncuesta;
                            bases.TipoEncuesta.NombreTipoDeEncuesta = obj.TipoEncuesta.NombreTipoDeEncuesta;
                            //Solo agrego a Objects si el tipo de anonimo
                            //1   Anonima
                            //2   Confidencial
                            //3   Generica
                            if (bases.TipoBD.IdTipoBD == 2 && bases.TipoEncuesta.IdTipoEncuesta == 1)
                            {
                                result.ListadoDeBaseDeDatos.Add(bases);
                            }
                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encuentran Bases de Datos Registradas";
                    }

                }
            }
            catch (Exception aE)
            {
                result.Correct = false;
                result.ErrorMessage = aE.Message.ToString();
            }
            return result;
        }
        public static ML.Result getBaseDeDatosConfidencialByPermisos(List<object> permisosEstrucura, int miEmpresaorigen)
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
                        string CompanyIdFinal = " OR Company.CompanyId = " + CompanyId + " and BasesDeDatos.IdEstatus = 1 ";
                        WHEREsql += CompanyIdFinal;
                    }
                    WHEREsql += " OR Company.CompanyId = " + miEmpresaorigen + " and BasesDeDatos.IdEstatus = 1 ";
                    //End Get CompanyId
                    var idParametro = 1;
                    var query = context.BasesDeDatos.SqlQuery
                        ("SELECT * FROM BasesDeDatos INNER JOIN TIPOENCUESTA ON BASESDEDATOS.IDTIPOENCUESTA = TIPOENCUESTA.IDTIPOENCUESTA INNER JOIN TIPOBD ON BASESDEDATOS.IDTIPOBD = TIPOBD.IDTIPOBD INNER JOIN Administrador ON BasesDeDatos.IdAdministradorCreate = Administrador.IdAdministrador INNER JOIN Empleado ON Administrador.IdEmpleado = Empleado.IdEmpleado INNER JOIN Company ON Administrador.CompanyId = Company.CompanyId " +
                        " where Company.CompanyId = 0  " + WHEREsql);

                    result.ListadoDeBaseDeDatos = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.BasesDeDatos bases = new ML.BasesDeDatos();
                            bases.IdBaseDeDatos = obj.IdBasesDeDatos;
                            bases.Nombre = obj.Nombre + " / Confidencial";
                            bases.TipoEstatus = new ML.TipoEstatus();
                            bases.TipoEstatus.IdEstatus = obj.TipoEstatus.IdEstatus;
                            bases.TipoEstatus.Descripcion = obj.TipoEstatus.Descripcion;
                            bases.TipoBD = new ML.TipoBD();
                            bases.TipoBD.IdTipoBD = obj.TipoBD.IdTipoBD;
                            bases.TipoBD.Descripcion = obj.TipoBD.Descripcion;
                            bases.TipoEncuesta = new ML.TipoEncuesta();
                            bases.TipoEncuesta.IdTipoEncuesta = obj.IdTipoEncuesta;
                            bases.TipoEncuesta.NombreTipoDeEncuesta = obj.TipoEncuesta.NombreTipoDeEncuesta;
                            //Solo agrego a Objects si el tipo de anonimo
                            //1   Anonima
                            //2   Confidencial
                            //3   Generica
                            if (bases.TipoBD.IdTipoBD == 2 && bases.TipoEncuesta.IdTipoEncuesta == 2)
                            {
                                result.ListadoDeBaseDeDatos.Add(bases);
                            }
                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encuentran Bases de Datos Registradas";
                    }

                }
            }
            catch (Exception aE)
            {
                result.Correct = false;
                result.ErrorMessage = aE.Message.ToString();
            }
            return result;
        }
        public static ML.Result getBaseDeDatosGenericaByPermisos(List<object> permisosEstrucura, int miEmpresaOrigen)
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
                        string CompanyIdFinal = " OR Company.CompanyId = " + CompanyId + " and BasesDeDatos.IdEstatus = 1 ";
                        WHEREsql += CompanyIdFinal;
                    }
                    WHEREsql += " OR Company.CompanyId = " + miEmpresaOrigen + " and BasesDeDatos.IdEstatus = 1 ";
                    //End Get CompanyId
                    var idParametro = 1;
                    var query = context.BasesDeDatos.SqlQuery
                        ("SELECT * FROM BasesDeDatos INNER JOIN TIPOENCUESTA ON BASESDEDATOS.IDTIPOENCUESTA = TIPOENCUESTA.IDTIPOENCUESTA INNER JOIN TIPOBD ON BASESDEDATOS.IDTIPOBD = TIPOBD.IDTIPOBD INNER JOIN Administrador ON BasesDeDatos.IdAdministradorCreate = Administrador.IdAdministrador INNER JOIN Empleado ON Administrador.IdEmpleado = Empleado.IdEmpleado INNER JOIN Company ON Administrador.CompanyId = Company.CompanyId " +
                        " where Company.CompanyId = 0  " + WHEREsql);

                    result.ListadoDeBaseDeDatos = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.BasesDeDatos bases = new ML.BasesDeDatos();
                            bases.IdBaseDeDatos = obj.IdBasesDeDatos;
                            bases.Nombre = obj.Nombre + " / Genérica";
                            bases.TipoEstatus = new ML.TipoEstatus();
                            bases.TipoEstatus.IdEstatus = obj.TipoEstatus.IdEstatus;
                            bases.TipoEstatus.Descripcion = obj.TipoEstatus.Descripcion;
                            bases.TipoBD = new ML.TipoBD();
                            bases.TipoBD.IdTipoBD = obj.TipoBD.IdTipoBD;
                            bases.TipoBD.Descripcion = obj.TipoBD.Descripcion;
                            bases.TipoEncuesta = new ML.TipoEncuesta();
                            bases.TipoEncuesta.IdTipoEncuesta = obj.IdTipoEncuesta;
                            bases.TipoEncuesta.NombreTipoDeEncuesta = obj.TipoEncuesta.NombreTipoDeEncuesta;
                            //Solo agrego a Objects si el tipo de anonimo
                            //1   Anonima
                            //2   Confidencial
                            //3   Generica
                            if (bases.TipoBD.IdTipoBD == 2 && bases.TipoEncuesta.IdTipoEncuesta == 3)
                            {
                                result.ListadoDeBaseDeDatos.Add(bases);
                            }
                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encuentran Bases de Datos Registradas";
                    }

                }
            }
            catch (Exception aE)
            {
                result.Correct = false;
                result.ErrorMessage = aE.Message.ToString();
            }
            return result;
        }

        public static void AsignarDemograficoConcatenado(int IdBaseDeDatos)
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var listEmple = context.Empleado.Select(o => o).Where(o => o.IdBaseDeDatos == IdBaseDeDatos).ToList();
                    foreach (var item in listEmple)
                    {
                        item.DivisionMarca = string.Concat(item.UnidadNegocio.Substring(0, 4), " - ", item.DivisionMarca);
                        item.AreaAgencia = string.Concat(item.UnidadNegocio.Substring(0, 4), " - " + item.DivisionMarca.Substring(0, 4), " - " + item.AreaAgencia);
                        item.Depto = string.Concat(item.UnidadNegocio.Substring(0, 4), " - ", item.DivisionMarca.Substring(0, 4), " - ", item.AreaAgencia.Substring(0, 4), item.Depto);
                        item.Subdepartamento = string.Concat(item.UnidadNegocio.Substring(0, 4), " - ", item.DivisionMarca.Substring(0, 4), " - ", item.AreaAgencia.Substring(0, 4), " - ", item.Depto.Substring(0, 4), " - ", item.Subdepartamento);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
            }
        }

        /// <summary>
        /// Metodo que regresa el nombre de la base de una consulta de encuesta
        /// </summary>
        /// <param name="idBaseDatos">se necesita el numerico de Id de la base de datos</param>
        /// <returns>regresa un modelo de Base de datos con idBAse de datos, nombre base de datos y estatus de la base de datos</returns>
        public static ML.BasesDeDatos getBDbyIdEncuesta(int idBaseDatos)
        {
            ML.BasesDeDatos based = new ML.BasesDeDatos();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.BasesDeDatos.Select(o => o).Where(o => o.IdBasesDeDatos == idBaseDatos).ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            based.IdBaseDeDatos = item.IdBasesDeDatos;
                            based.Nombre = item.Nombre;
                            based.TipoEstatus = new ML.TipoEstatus();
                            based.TipoEstatus.IdEstatus = item.IdEstatus;
                        }
                    }
                   // return based;
                }
            }
            catch (Exception aE)
            {

                return null;
            }
            return based;

        }
    }
}
