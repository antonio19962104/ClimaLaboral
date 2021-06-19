using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result Update(DL.Usuario item, DL.RH_DesEntities context, System.Data.Entity.DbContextTransaction transaction)
        {
            ML.Result result = new ML.Result();
            try
            {
                var query = context.Database.ExecuteSqlCommand("UPDATE USUARIO SET " +
                                "Nombre = {0}, ApellidoPaterno = {1}, ApellidoMaterno = {2}, " +
                                "Puesto = {3}, FechaNacimiento = {4}, FechaAntiguedad = {5}, " + 
                                "Sexo = {6}, Email = {7}, TipoFuncion = {8}, CondicionTrabajo = {9}, " +
                                "GradoAcademico = {10}, UnidadNegocio = {11}, " + 
                                "DivisionMarca = {12}, AreaAgencia = {13}, Departamento = {14}, " +
                                "Subdepartamento = {15}, EmpresaContratante = {16}, " +
                                "IdResponsableRH = {17}, NombreResponsableRH = {18}, " +
                                "IdJefe = {19}, NombreJefe = {20}, PuestoJefe = {21}, " +
                                "IdResponsableEstructura = {22}, NombreResponsableEstructura = {23}, " +
                                "ClaveAcceso = {24}, RangoAntiguedad = {25}, RangoEdad = {26}, " +
                                "IdEstatus = {27}, CampoNumerico_1 = {28}, CampoDeTexto_1 = {29}, " +
                                "CampoNumerico_2 = {30}, CampoDeTexto_2 = {31}, " +
                                "CampoNumerico_3 = {32}, CampoDeTexto_3 = {33}, Password = {35} " +
                                "WHERE IDUSUARIO = {34}",
                                item.Nombre, item.ApellidoPaterno, item.ApellidoMaterno,
                                item.Puesto, item.FechaNacimiento, item.FechaAntiguedad,
                                item.Sexo, item.Email, item.TipoFuncion, item.CondicionTrabajo,
                                item.GradoAcademico, item.UnidadNegocio, item.DivisionMarca,
                                item.AreaAgencia, item.Departamento, item.Subdepartamento, 
                                item.EmpresaContratante, item.IdResponsableRH,
                                item.NombreResponsableRH, item.IdJefe, item.NombreJefe, item.PuestoJefe,
                                item.IdResponsableEstructura, item.NombreResponsableEstructura, 
                                item.ClaveAcceso, item.RangoAntiguedad, item.RangoEdad, 
                                item.IdEstatus, item.CampoNumerico_1, item.CampoDeTexto_1,
                                item.CampoNumerico_2, item.CampoDeTexto_2, 
                                item.CampoNumerico_3, item.CampoDeTexto_3, item.IdUsuario, item.Password);

                result.Correct = true;
                //context.SaveChanges();
                //transaction.Commit();
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Usuario.SqlQuery("SELECT * FROM Usuario LEFT JOIN TipoEstatus ON USUARIO.IDESTATUS = TIPOESTATUS.IDESTATUS LEFT JOIN Empleado ON Usuario.IdEmpleado = Empleado.IdEmpleado LEFT JOIN Perfil ON USUARIO.IDPERFIL = PERFIL.IDPERFIL INNER JOIN BasesDeDatos ON USUARIO.IdBaseDeDatos = BASESDEDATOS.IdBasesDeDatos WHERE usuario.IDUSUARIO = {0}", IdUsuario).ToList();

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
                            usuario.FechaNacimiento = Convert.ToDateTime(item.FechaNacimiento);
                            usuario.FechaAntiguedad = Convert.ToDateTime(item.FechaAntiguedad);
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
                            usuario.TipoEstatus.IdEstatus = item.TipoEstatus.IdEstatus;

                            usuario.CampoDeTexto_1 = item.CampoDeTexto_1;
                            usuario.CampoDeTexto_2 = item.CampoDeTexto_2;
                            usuario.CampoDeTexto_3 = item.CampoDeTexto_3;
                            usuario.CampoNumerico_1 = Convert.ToInt32(item.CampoNumerico_1);
                            usuario.CampoNumerico_2 = Convert.ToInt32(item.CampoNumerico_2);
                            usuario.CampoNumerico_3 = Convert.ToInt32(item.CampoNumerico_3);

                            result.Object = usuario;
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

        public static ML.Result ActualizarUsuario(ML.Usuario user)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    string SqlQuery = "UPDATE Usuario SET NOMBRE = {0}, APELLIDOPATERNO = {1}, APELLIDOMATERNO = {2}, Password = {3}, Puesto = {4}, FechaNacimiento = {5}, FechaAntiguedad = {6}, Sexo = {7}, Email = {8}, TipoFuncion = {9}, CondicionTrabajo = {10}, GradoAcademico = {11}, UnidadNegocio = {12}, DivisionMarca = {13}, AreaAgencia = {14}, Departamento = {15}, Subdepartamento = {16}, EmpresaContratante = {17}, IdResponsableRH = {18}, NombreResponsableRH = {19}, IdJefe = {20}, NombreJefe = {21}, PuestoJefe = {22}, IdResponsableEstructura = {23}, NombreResponsableEstructura = {24}, ClaveAcceso = {25}, RangoAntiguedad = {26}, RangoEdad = {27}, IdEstatus = {28}, CampoNumerico_1 = {29}, CampoNumerico_2 = {30}, CampoNumerico_3 = {31}, CampoDeTexto_1 = {32}, CampoDeTexto_2 = {33}, CampoDeTexto_3 = {34} WHERE IDUSUARIO = {35}";

                    var query = context.Database.ExecuteSqlCommand(SqlQuery,
                        user.Nombre, user.ApellidoPaterno, user.ApellidoMaterno,
                        user.Password, user.Puesto, user.FechaNacimiento, user.FechaAntiguedad,
                        user.Sexo, user.Email, user.TipoFuncion, user.CondicionTrabajo,
                        user.GradoAcademico, user.UnidadNegocio, user.DivisionMarca,
                        user.AreaAgencia, user.Departamento, user.Subdepartamento,
                        user.EmpresaContratante, user.IdResponsableRH, user.NombreResponsableRH,
                        user.IdJefe, user.NombreJefe, user.PuestoJefe, user.IdRespinsableEstructura,
                        user.NombreResponsableEstructura, user.ClaveAcceso, user.RangoAntiguedad,
                        user.RangoEdad, user.IdEstatus,
                        user.CampoNumerico_1, user.CampoNumerico_2, user.CampoNumerico_3,
                        user.CampoDeTexto_1, user.CampoDeTexto_2, user.CampoDeTexto_3,
                        user.IdUsuario
                        );
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
        //Empleado
        public static ML.Result GetByIdEmpleado(int IdEmpledo)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Empleado.SqlQuery("SELECT * FROM Empleado LEFT JOIN Perfil ON Empleado.IDPERFIL = PERFIL.IDPERFIL INNER JOIN BasesDeDatos ON Empleado.IdBaseDeDatos = BASESDEDATOS.IdBasesDeDatos WHERE Empleado.IdEmpleado = {0}", IdEmpledo).ToList();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Empleado empleado = new ML.Empleado();
                            
                            empleado.Perfil = new ML.Perfil();
                            empleado.BaseDeDatos = new ML.BasesDeDatos();

                            empleado.BaseDeDatos.Nombre = item.BasesDeDatos.Nombre;

                            empleado.IdEmpleado = item.IdEmpleado;
                            empleado.Nombre = item.Nombre;
                            empleado.ApellidoPaterno = item.ApellidoPaterno;
                            empleado.ApellidoMaterno = item.ApellidoMaterno;
                            //empleado.Username = item.UserName;
                            //empleado.Password = item.Password;
                            //empleado.IdEmpleado = Convert.ToInt32(item.IdEmpleado);
                            //usuario.Perfil.Descripcion = item.Perfil.Descripcion;
                            empleado.Puesto = item.Puesto;
                            empleado.FechaNaciemiento = Convert.ToDateTime(item.FechaNacimiento);
                            empleado.FechaAntiguedad = Convert.ToDateTime(item.FechaAntiguedad);
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
                            

                            result.Object = empleado;
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
        public static ML.Result ActualizarEmpleado(ML.Empleado emp)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    string SqlQuery = "UPDATE Empleado SET NOMBRE = {0}, APELLIDOPATERNO = {1}, APELLIDOMATERNO = {2}, Puesto = {4}, FechaNacimiento = {5}, FechaAntiguedad = {6}, Sexo = {7}, Correo = {8}, TipoFuncion = {9}, CondicionTrabajo = {10}, GradoAcademico = {11}, UnidadNegocio = {12}, DivisionMarca = {13}, AreaAgencia = {14}, Depto = {15}, Subdepartamento = {16}, EmpresaContratante = {17}, IdResponsableRH = {18}, NombreResponsableRH = {19}, IdJefe = {20}, NombreJefe = {21}, PuestoJefe = {22}, IdResponsableEstructura = {23}, NombreResponsableEstructura = {24}, RangoAntiguedad = {26}, RangoEdad = {27}, EstatusEmpleado = {28}, IdEmpleadoRH = {36} WHERE IdEmpleado = {35}";
                    if (emp.EstatusEmpleado == "" || emp.EstatusEmpleado == null)
                    {
                        emp.EstatusEmpleado = "Activo";
                    }
                    var query = context.Database.ExecuteSqlCommand(SqlQuery,
                        emp.Nombre, emp.ApellidoPaterno, emp.ApellidoMaterno,
                        "", emp.Puesto, emp.FechaNaciemiento, emp.FechaAntiguedad,
                        emp.Sexo, emp.Correo, emp.TipoFuncion, emp.CondicionTrabajo,
                        emp.GradoAcademico, emp.UnidadNegocio, emp.DivisonMarca,
                        emp.AreaAgencia, emp.Departamento, emp.Subdepartamento,
                        emp.EmpresaContratante, emp.IdResponsableRH, emp.NombreResponsableRH,
                        emp.IdJefe, emp.NombreJefe, emp.PuestoJefe, emp.IdRespinsableEstructura,
                        emp.NombreResponsableEstrucutra, emp.ClavesAcceso.ClaveAcceso, emp.RangoAntiguedad,
                        emp.RangoEdad, emp.EstatusEmpleado,
                        "", "", "",
                        "", "", "",
                        emp.IdEmpleado, emp.IdEmpleadoRH
                        );
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

        //AddEstatusEncuesta
        public static ML.Result AddEstatus(int idEncuesta, int IdBaseDeDatos, DL.RH_DesEntities context)
        {
            ML.Result result = new ML.Result();

            try
            {
                var getUsuarios = context.Usuario.SqlQuery("SELECT * FROM USUARIO WHERE IdBaseDeDatos = {0}", IdBaseDeDatos).ToList();
                if (getUsuarios != null)
                {
                    foreach (var item in getUsuarios)
                    {
                        var insert = context.Database.ExecuteSqlCommand
                            ("INSERT INTO UsuarioEstatusEncuesta (IdUsuario, IdEncuesta, IdEstatusEncuestaD4U, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4}, {5})",
                                                                 item.IdUsuario, idEncuesta, 1, DateTime.Now, "CrearEncuesta", "Diagnostic4U");
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
        //Set Iniciada
        public static ML.Result updateEstatusEncuesta(int IdEncuesta, int Idusuario, int IdEstatus)
        {
            ML.Result result = new ML.Result();

            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                //using (var transaction = context.Database.BeginTransaction())
                //{
                    try
                    {
                        var validaDuplicado = context.UsuarioEstatusEncuesta.SqlQuery("SELECT * FROM UsuarioEstatusEncuesta WHERE IDENCUESTA = {0} AND IDUSUARIO = {1}", IdEncuesta, Idusuario);
                        foreach (var item in validaDuplicado)
                        {
                            if (item.IdEstatusEncuestaD4U == 3)
                            {
                                result.IsContestada = true;
                            }
                        }
                        if (result.IsContestada != true)
                        {
                            var query = context.Database.ExecuteSqlCommand("UPDATE UsuarioEstatusEncuesta SET IdEstatusEncuestaD4U = {0}, FechaHoraModificacion = {3}, UsuarioModificacion = {4}, ProgramaModificacion = 'Encuesta' WHERE IdUsuario = {1} AND IdEncuesta = {2}", IdEstatus, Idusuario, IdEncuesta, DateTime.Now, Idusuario);
                        }
                        
                        result.Correct = true;
                        context.SaveChanges();
                        var path1 = @"\\10.5.2.101\RHDiagnostics\log\Log" + IdEncuesta + "_" + Idusuario + ".txt";
                        var fullPath1 = Path.GetFullPath(path1);
                        if (!File.Exists(fullPath1))
                        {
                            string createText = "Log" + Environment.NewLine;
                            File.WriteAllText(fullPath1, createText);
                        }
                        string appendText1 = "Actualización de estatus" + " " + DateTime.Now + Environment.NewLine;
                        File.AppendAllText(fullPath1, appendText1);
                }
                    catch (Exception ex)
                    {
                        result.Correct = false;
                        result.ErrorMessage = ex.Message;
                        var path1 = @"\\10.5.2.101\RHDiagnostics\log\Log" + IdEncuesta + "_" + Idusuario + ".txt";
                        var fullPath1 = Path.GetFullPath(path1);
                        if (!File.Exists(fullPath1))
                        {
                            string createText = "Log" + Environment.NewLine;
                            File.WriteAllText(fullPath1, createText);
                        }
                        string appendText1 = "Falló la actualización de estatus " + ex.Message + " " + DateTime.Now + Environment.NewLine;
                        File.AppendAllText(fullPath1, appendText1);
                }
                //}
            }
            return result;
        }

        public static ML.Result insertEstatusEncuesta(int IdEncuesta, int Idusuario, int status)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Database.ExecuteSqlCommand("INSERT INTO UsuarioEstatusEncuesta (IDENCUESTA, IDUSUARIO, IDESTATUSENCUESTAD4U) VALUES ({0}, {1}, {2})", IdEncuesta, Idusuario, status);

                    context.SaveChanges();
                    result.Correct = true;
                    var path1 = @"\\10.5.2.101\RHDiagnostics\log\Log" + IdEncuesta + "_" + Idusuario + ".txt";
                    var fullPath1 = Path.GetFullPath(path1);
                    if (!File.Exists(fullPath1))
                    {
                        string createText = "Log" + Environment.NewLine;
                        File.WriteAllText(fullPath1, createText);
                    }
                    string appendText1 = "Actualizacion de estatus a terminada" + " " + DateTime.Now + Environment.NewLine;
                    File.AppendAllText(fullPath1, appendText1);
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                var path1 = @"\\10.5.2.101\RHDiagnostics\log\Log" + IdEncuesta + "_" + Idusuario + ".txt";
                var fullPath1 = Path.GetFullPath(path1);
                if (!File.Exists(fullPath1))
                {
                    string createText = "Log" + Environment.NewLine;
                    File.WriteAllText(fullPath1, createText);
                }
                string appendText1 = "Falló la actualizacion de estatus." + ex.Message + " " + DateTime.Now + Environment.NewLine;
                File.AppendAllText(fullPath1, appendText1);
            }
            return result;
        }

        public static void saveDevice(int IdEncuesta, int IdUsuario, bool IsMobileDevice, string Version, string Browser, string ip)
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var navAndIP = Browser + ". IP del usuario " + ip + ". ";
                    var query = context.Database.ExecuteSqlCommand("UPDATE USUARIOESTATUSENCUESTA SET isMobileInicio = {0}, NavegadorInicio = {1}, VersionInicio = {2} WHERE IDUSUARIO = {3} AND IDENCUESTA = {4}",
                        IsMobileDevice, navAndIP, Version, IdUsuario, IdEncuesta);
                    context.SaveChanges();

                    var path = @"\\10.5.2.101\RHDiagnostics\log\Log" + IdEncuesta + "_" + IdUsuario + ".txt";
                    var fullPath = Path.GetFullPath(path);
                    if (!File.Exists(fullPath))
                    {
                        string createText = "Log" + Environment.NewLine;
                        File.WriteAllText(fullPath, createText);
                    }
                    string appendText = "Dispositivo movil: " + IsMobileDevice + ". Navegador: " + Browser + ". Version: " + Version + DateTime.Now + Environment.NewLine;
                    File.AppendAllText(fullPath, appendText);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void saveDeviceFinal(int IdEncuesta, int IdUsuario, bool IsMobileDevice, string Version, string Browser, string ip)
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var navAndIP = Browser + ". IP del usuario " + ip + ". ";
                    var query = context.Database.ExecuteSqlCommand("UPDATE USUARIOESTATUSENCUESTA SET isMobileFinal = {0}, NavegadorFinal = {1}, VersionFinal = {2} WHERE IDUSUARIO = {3} AND IDENCUESTA = {4}",
                        IsMobileDevice, navAndIP, Version, IdUsuario, IdEncuesta);
                    context.SaveChanges();

                    var path = @"\\10.5.2.101\RHDiagnostics\log\Log" + IdEncuesta + "_" + IdUsuario + ".txt";
                    var fullPath = Path.GetFullPath(path);
                    if (!File.Exists(fullPath))
                    {
                        string createText = "Log" + Environment.NewLine;
                        File.WriteAllText(fullPath, createText);
                    }
                    string appendText = "Dispositivo movil: " + IsMobileDevice + ". Navegador: " + Browser + ". Version: " + Version + DateTime.Now + Environment.NewLine;
                    File.AppendAllText(fullPath, appendText);
                }
            }
            catch (Exception)
            {

            }
        }


    }
}
