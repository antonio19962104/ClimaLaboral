using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Administrador
    {
        public static ML.Result UpdateIdRH(int IdAdmin, int IdRH)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var admin = context.Administrador.Where(o => o.IdAdministrador == IdAdmin).FirstOrDefault();
                    if (admin != null)
                        admin.IdRH = IdRH;
                    context.SaveChanges();
                    result.Correct = true;
                }
            }
            catch (Exception aE)
            {
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
            }
            return result;
        }
        public static ML.Result AutenticarAdmin(ML.Administrador admin)
        {
            //print_r(new List<ML.Result>())
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    //Obtener el id del admin y el id de empleado validando si existe y si esta activo
                    var queryAdmin = context.Administrador.SqlQuery("SELECT * FROM Administrador inner join Company on Administrador.companyId = Company.CompanyId  INNER JOIN Empleado ON Administrador.IdEmpleado = Empleado.IdEmpleado WHERE Administrador.IdEstatus = 1 and Administrador.UserName = {0} COLLATE Latin1_General_CS_AS AND Administrador.Password = {1} COLLATE Latin1_General_CS_AS", admin.UserName, admin.Password).ToList();
                    ML.Administrador administradorData = new ML.Administrador();
                    if (queryAdmin != null)
                    {
                        foreach (var item in queryAdmin)
                        {
                            administradorData.IdAdministrador = item.IdAdministrador;
                            administradorData.Empleado = new ML.Empleado();
                            administradorData.Empleado.IdEmpleado = Convert.ToInt32(item.IdEmpleado);
                            administradorData.Company = new ML.Company();
                            administradorData.Company.CompanyId = item.CompanyId;
                            administradorData.Company.Color = item.Company.Color;
                            administradorData.Company.LogoEmpresa = item.Company.LogoEmpresa;

                            administradorData.AdminSA = Convert.ToInt32(item.AdminSA);
                            result.IsSuperAdmin = (int)administradorData.AdminSA;

                            result.DataColors = administradorData;
                            result.CURRENT_IDEMPLEADOLOG = administradorData.Empleado.IdEmpleado;
                            result.CURRENTIDADMINLOG = administradorData.IdAdministrador;
                            result.CompanyDelAdmin = Convert.ToString(administradorData.Company.CompanyId);
                        }
                    }

                    //Obtener los perfiles que posee el administrador
                    var queryPerfiles = context.GetPerfilesForAdminDashBoard(administradorData.Empleado.IdEmpleado).ToList();
                    result.PerfilesList = new List<string>();
                    if (queryPerfiles != null)
                    {
                        foreach (var item in queryPerfiles)
                        {
                            ML.PerfilModulo perfilMod = new ML.PerfilModulo();
                            perfilMod.PerfilD4U = new ML.PerfilD4U();

                            perfilMod.PerfilD4U.Descripcion = item.Descripcion;

                            result.PerfilesList.Add(perfilMod.PerfilD4U.Descripcion);

                        }
                    }
                    
                    foreach (string item in result.PerfilesList.ToList())
                    {
                        if (item == "Administrador Master")
                        {
                            result.IsMaster = true;
                        }
                        else
                        {
                            result.IsMaster = false;
                        }
                    }

                    //Validar query
                    if (result.IsMaster == true)
                    {
                        //Si es master obtener por id de admin
                        var query = context.GetAllPermisosFullOK(administradorData.IdAdministrador).ToList();
                        result.Objects = new List<object>();
                        result.ObjectsPermisos = new List<object>();
                        result.ObjectsAux = new List<object>();
                        if (query != null)
                        {
                            foreach (var item in query)
                            {
                                ML.PerfilModulo perfilModulo = new ML.PerfilModulo();
                                perfilModulo.Administrador = new ML.Administrador();
                                perfilModulo.Administrador.Empleado = new ML.Empleado();
                                perfilModulo.PerfilD4U = new ML.PerfilD4U();
                                perfilModulo.Modulo = new ML.Modulo();
                                perfilModulo.PerfilModuloAccion = new ML.PerfilModuloAccion();
                                perfilModulo.Acciones = new List<string>();
                                /*
                                Get Modulos & Acciones from Admin
                                Get Full Name Empleado    
                                Solo puede acceder si esta activo
                                */
                                perfilModulo.Administrador.IdAdministrador = Convert.ToInt32(item.IdAdministrador);
                                perfilModulo.Administrador.Empleado.Nombre = item.Nombre;
                                perfilModulo.Administrador.Empleado.ApellidoPaterno = item.ApellidoPaterno;
                                perfilModulo.Administrador.Empleado.ApellidoMaterno = item.ApellidoMaterno;
                                perfilModulo.PerfilD4U.Descripcion = item.DescripcionPerfil;
                                perfilModulo.Modulo.IdModulo = Convert.ToInt32(item.IdModulo);
                                perfilModulo.Modulo.Nombre = item.NombreModulo;
                                perfilModulo.PerfilModuloAccion.Accion = item.Accion;

                                result.ObjectsPermisos.Add(perfilModulo);
                                result.ObjectsAux.Add(perfilModulo);//Tomar todos los modulos y si hay repetidos limpiarlos
                                result.Object = perfilModulo;

                                result.Correct = true;
                            }
                        }
                    }
                    else
                    {
                        //No es master => obtener por Id de empleado
                        //var query = context.ObtenerPermisos(administradorData.Empleado.IdEmpleado).ToList();
                        string querysql = "SELECT * " +
                        "FROM Administrador " +


                        "INNER JOIN Empleado ON Administrador.IdEmpleado = Empleado.IdEmpleado " +

                        "INNER JOIN PerfilD4U ON Administrador.IdPerfil = PerfilD4U.IdPerfil " +

                        "INNER JOIN PerfilModulo ON Administrador.IdAdministrador = PerfilModulo.IdAdministrador " +

                        "INNER JOIN Modulo ON PerfilModulo.IdModulo = Modulo.IdModulo " +

                        "INNER JOIN PerfilModuloAccion ON PerfilModulo.IdPerfilModulo = PerfilModuloAccion.IdPerfilModulo " +

                        "WHERE Administrador.IdEmpleado = {0} ";
                        var query = context.GetPermisosList_(administradorData.Empleado.IdEmpleado).ToList();

                        result.Objects = new List<object>();
                        result.ObjectsPermisos = new List<object>();
                        result.ObjectsAux = new List<object>();
                        if (query != null)
                        {
                            foreach (var item in query)
                            {
                                
                                ML.PerfilModulo perfilModulo = new ML.PerfilModulo();
                                perfilModulo.Administrador = new ML.Administrador();
                                perfilModulo.Administrador.Empleado = new ML.Empleado();
                                perfilModulo.PerfilD4U = new ML.PerfilD4U();
                                perfilModulo.Modulo = new ML.Modulo();
                                perfilModulo.PerfilModuloAccion = new ML.PerfilModuloAccion();
                                perfilModulo.Acciones = new List<string>();
                                /*
                                Get Modulos & Acciones from Admin
                                Get Full Name Empleado    
                                Solo puede acceder si esta activo
                                */
                                perfilModulo.Administrador.IdAdministrador = Convert.ToInt32(item.IdAdministrador);
                                perfilModulo.Administrador.Empleado.Nombre = item.NombreEmpleado;
                                perfilModulo.Administrador.Empleado.ApellidoPaterno = item.ApellidoPaterno;
                                perfilModulo.Administrador.Empleado.ApellidoMaterno = item.ApellidoMaterno;
                                perfilModulo.PerfilD4U.Descripcion = item.Descripcion;
                                

                                perfilModulo.Modulo.IdModulo = Convert.ToInt32(item.IdModulo);
                                perfilModulo.Modulo.Nombre = item.NombreModulo;
                                perfilModulo.PerfilModuloAccion.Accion = item.Accion;

                                result.ObjectsPermisos.Add(perfilModulo);
                                result.ObjectsAux.Add(perfilModulo);//Tomar todos los modulos y si hay repetidos limpiarlos
                                result.Object = perfilModulo;

                                result.Correct = true;
                            }
                        }
                    }

                   

                        //Obtener modulos sin replicar
                        //var queryMods = context.PerfilModulo.SqlQuery("SELECT * FROM PerfilModulo LEFT JOIN Administrador ON PerfilModulo.IdPerfil = Administrador.IdPerfil LEFT JOIN Empleado ON Administrador.IdEmpleado = Empleado.IdEmpleado LEFT JOIN PerfilD4U ON Administrador.IdPerfil = PerfilD4U.IdPerfil LEFT JOIN Modulo ON PerfilModulo.IdModulo = Modulo.IdModulo	WHERE Empleado.IdEmpleado = {0}", administradorData.Empleado.IdEmpleado).ToList();
                        var queryMods = context.GETMODULO(administradorData.Empleado.IdEmpleado).ToList();
                        result.ObjectsAux = new List<object>();

                    if (queryMods != null)
                    {
                        foreach (var item in queryMods)
                        {
                            ML.PerfilModulo perfM = new ML.PerfilModulo();
                            perfM.Modulo = new ML.Modulo();
                            perfM.PerfilD4U = new ML.PerfilD4U();

                            perfM.Modulo.IdModulo = Convert.ToInt32(item.IdModulo);
                            perfM.Modulo.Nombre = item.Nombre;

                            result.ObjectsAux.Add(perfM);
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
        public static ML.Result Add(ML.Administrador administrador, int IdAdminCreate)
        {
            ML.Result result = new ML.Result();
            
            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                context.Database.Log = Console.Write;

                using (var transaction = context.Database.BeginTransaction())
                {

                    try
                    {
                        //Si el administrador ya existe tomar sus claves e insrtarlas en este nuevo registro para que conserve las mismas claves
                        var exists = context.Administrador.SqlQuery("select * from Administrador	INNER JOIN Empleado ON Administrador.IdEmpleado = Empleado.IdEmpleado	WHERE Empleado.IdEmpleado = {0}", administrador.Empleado.IdEmpleado).ToList();
                        
                        if (exists.Count == 0)//No existe
                        {
                            var query = context.Database.ExecuteSqlCommand
                            ("INSERT INTO Administrador (IdEmpleado, IdPerfil, IdEstatus, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion, UserName, Password, CompanyId, IdAdministradorCreate, IdRH) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10})",
                            administrador.Empleado.IdEmpleado, administrador.PerfilD4U.IdPerfil, administrador.TipoEstatus.IdEstatus, DateTime.Now, administrador.CURRENT_USER, "D4U", administrador.UserName, administrador.Password, administrador.Company.CompanyId, IdAdminCreate, administrador.IdRH);
                            int IdAdminInsertado = context.Administrador.Max(p => p.IdAdministrador);
                            result.UltimoAdminInsertado = IdAdminInsertado;
                            result.DefPass = administrador.Password;
                        }
                        else if(exists.Count >= 1)
                        {
                            foreach (var item in exists)
                            {
                                result.DefUsername = item.UserName;//Obtengo el username que ya existe
                                result.DefPass = item.Password;//Obtengo el Pass que ya existe
                            }
                            var query = context.Database.ExecuteSqlCommand
                                ("INSERT INTO Administrador (IdEmpleado, IdPerfil, IdEstatus, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion, UserName, Password, CompanyId, IdAdministradorCreate, IDRH) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10})",
                                administrador.Empleado.IdEmpleado, administrador.PerfilD4U.IdPerfil, administrador.TipoEstatus.IdEstatus, DateTime.Now, administrador.CURRENT_USER, "D4U", result.DefUsername, result.DefPass, administrador.Company.CompanyId, IdAdminCreate, administrador.IdRH);
                            int IdAdminInsertado = context.Administrador.Max(p => p.IdAdministrador);
                            result.UltimoAdminInsertado = IdAdminInsertado;
                            result.DefPass = result.DefPass;
                        }

                        

                            
                 
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

        public static ML.Result AddAdminModPlanes(ML.Administrador administrador, int IdAdminCreate)
        {
            ML.Result result = new ML.Result();

            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                context.Database.Log = Console.Write;

                using (var transaction = context.Database.BeginTransaction())
                {

                    try
                    {
                        //Si el administrador ya existe tomar sus claves e insrtarlas en este nuevo registro para que conserve las mismas claves
                        var exists = context.Administrador.SqlQuery("select * from Administrador	INNER JOIN Empleado ON Administrador.IdEmpleado = Empleado.IdEmpleado	WHERE Empleado.IdEmpleado = {0}", administrador.Empleado.IdEmpleado).ToList();

                        if (exists.Count == 0)//No existe
                        {
                            var query = context.Database.ExecuteSqlCommand
                            ("INSERT INTO Administrador (IdEmpleado, IdPerfil, IdEstatus, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion, UserName, Password, CompanyId, IdAdministradorCreate) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9})",
                            administrador.Empleado.IdEmpleado, administrador.PerfilD4U.IdPerfil, administrador.TipoEstatus.IdEstatus, DateTime.Now, administrador.CURRENT_USER, "D4U", administrador.UserName, administrador.Password, administrador.Company.CompanyId, IdAdminCreate);
                            int IdAdminInsertado = context.Administrador.Max(p => p.IdAdministrador);
                            result.UltimoAdminInsertado = IdAdminInsertado;
                            result.DefPass = administrador.Password;
                        }
                        else
                        {
                            result.UltimoAdminInsertado = exists.ElementAt(0).IdAdministrador;
                        }
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

        public static ML.Result GetAll(int CURRENT_IDEMPLEADOLOG, int IDCURRENTADMINLOG, int AdminLogSA)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.GetAdminForMasterOK__(CURRENT_IDEMPLEADOLOG, IDCURRENTADMINLOG).ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Administrador admin = new ML.Administrador();

                            admin.IdAdministrador = item.IdAdministrador;
                            admin.Empleado = new ML.Empleado();
                            admin.Empleado.IdEmpleado = Convert.ToInt32(item.IdEmpleado);
                            admin.Empleado.Nombre = item.Nombre;
                            admin.Empleado.ApellidoPaterno = item.ApellidoPaterno;
                            admin.Empleado.ApellidoMaterno = item.ApellidoMaterno;
                            admin.PerfilD4U = new ML.PerfilD4U();
                            admin.PerfilD4U.IdPerfil = Convert.ToInt32(item.IdPerfil);
                            admin.PerfilD4U.Descripcion = item.DescripcionPerfil;
                            admin.TipoEstatus = new ML.TipoEstatus();
                            admin.TipoEstatus.IdEstatus = Convert.ToInt32(item.IdEstatus);
                            var isSuperAdmin = context.Administrador.SqlQuery("SELECT * FROM ADMINISTRADOR WHERE IDADMINISTRADOR = {0}", admin.IdAdministrador);
                            admin.AdminSA = 0;
                            foreach (var obj in isSuperAdmin)
                            {
                                admin.UserName = obj.UserName;
                                if (obj.AdminSA == 1)
                                {
                                    admin.AdminSA = 1;
                                }
                            }

                            if (AdminLogSA == 1)
                            {
                                result.Objects.Add(admin);
                            }
                            else if (AdminLogSA == 0)
                            {
                                if (admin.AdminSA == 0)
                                {
                                    result.Objects.Add(admin);
                                }
                            }
                            


                            
                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.ErrorMessage = "No se encontraron registros de administrador";
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

        public static ML.Result GetAllExceptMaster(int CURRENT_IDEMPLEADOLOG, int IdEmpleadoLog, int AdminLogIsSuperAdmin)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.GetAdminForNotMasterOK__(CURRENT_IDEMPLEADOLOG, IdEmpleadoLog).ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Administrador admin = new ML.Administrador();
                            admin.IdAdministrador = item.IdAdministrador;
                            admin.Empleado = new ML.Empleado();
                            admin.Empleado.IdEmpleado = Convert.ToInt32(item.IdEmpleado);
                            admin.Empleado.Nombre = item.Nombre;
                            admin.Empleado.ApellidoPaterno = item.ApellidoPaterno;
                            admin.Empleado.ApellidoMaterno = item.ApellidoMaterno;
                            admin.PerfilD4U = new ML.PerfilD4U();
                            admin.PerfilD4U.IdPerfil = Convert.ToInt32(item.IdPerfil);
                            admin.PerfilD4U.Descripcion = item.DescripcionPerfil;
                            admin.TipoEstatus = new ML.TipoEstatus();
                            admin.TipoEstatus.IdEstatus = Convert.ToInt32(item.IdEstatus);
                            var isSA = context.Administrador.SqlQuery("SELECT * FROM ADMINISTRADOR WHERE IDADMINISTRADOR = {0}", admin.IdAdministrador);
                            int IsSuperAdmin = 0;
                            foreach (var obj in isSA)
                            {
                                admin.UserName = obj.UserName;
                                if (obj.AdminSA == 1)
                                {
                                    IsSuperAdmin = 1;
                                }
                            }

                            if (AdminLogIsSuperAdmin == 1)
                            {
                                result.Objects.Add(admin);
                            }
                            else if (AdminLogIsSuperAdmin == 0)
                            {
                                if (admin.AdminSA == 0)
                                {
                                    result.Objects.Add(admin);
                                }
                            }


                            result.Objects.Add(admin);
                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.ErrorMessage = "No se obtuvieron registros de administradores";
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

        public static ML.Result GetById(int IdAministrador)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Administrador.SqlQuery("SELECT * FROM ADMINISTRADOR INNER JOIN Empleado ON Administrador.IdEmpleado = Empleado.IdEmpleado INNER JOIN PerfilD4U ON Administrador.IdPerfil = PerfilD4U.IdPerfil WHERE Administrador.IdAdministrador = {0}", IdAministrador);

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Administrador admin = new ML.Administrador();

                            admin.IdAdministrador = item.IdAdministrador;
                            admin.Empleado = new ML.Empleado();
                            admin.Empleado.IdEmpleado = Convert.ToInt32(item.IdEmpleado);
                            admin.Empleado.Nombre = item.Empleado.Nombre;
                            admin.Empleado.ApellidoPaterno = item.Empleado.ApellidoPaterno;
                            admin.Empleado.ApellidoMaterno = item.Empleado.ApellidoMaterno;
                            admin.PerfilD4U = new ML.PerfilD4U();
                            admin.PerfilD4U.IdPerfil = Convert.ToInt32(item.IdPerfil);
                            admin.PerfilD4U.Descripcion = item.PerfilD4U.Descripcion;
                            admin.UserName = item.UserName;
                            admin.Password = item.Password;

                            result.Object = admin;
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

        public static ML.Result GetByIdPerfil(int IdPerfil)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Administrador.SqlQuery("SELECT * FROM ADMINISTRADOR INNER JOIN Empleado ON Administrador.IdEmpleado = Empleado.IdEmpleado INNER JOIN PerfilD4U ON Administrador.IdPerfil = PerfilD4U.IdPerfil WHERE Administrador.IdPerfil = {0}", IdPerfil).ToList();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Administrador admin = new ML.Administrador();

                            admin.IdAdministrador = item.IdAdministrador;
                            admin.Empleado = new ML.Empleado();
                            admin.Empleado.IdEmpleado = Convert.ToInt32(item.IdEmpleado);
                            admin.Empleado.Nombre = item.Empleado.Nombre;
                            admin.Empleado.ApellidoPaterno = item.Empleado.ApellidoPaterno;
                            admin.Empleado.ApellidoMaterno = item.Empleado.ApellidoMaterno;
                            admin.PerfilD4U = new ML.PerfilD4U();
                            admin.PerfilD4U.IdPerfil = Convert.ToInt32(item.IdPerfil);
                            admin.PerfilD4U.Descripcion = item.PerfilD4U.Descripcion;

                            result.Objects.Add(admin);

                            result.Correct = true;

                        }
                    }
                    else
                    {
                        result.ErrorMessage = "No se encontraron registros de administradores bajo el perfil seleccionado";
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

        public static ML.Result Update(ML.Administrador administrador)
        {
            ML.Result result = new ML.Result();

            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                context.Database.Log = Console.Write;

                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var query = context.Database.ExecuteSqlCommand("UPDATE Administrador SET IdEmpleado = {0}, IdPerfil = {1}, FechaHoraModificacion = {2}, UsuarioModificacion = {3}, ProgramaModificacion = {4} WHERE IdAdministrador = {5}", administrador.Empleado.IdEmpleado, administrador.PerfilD4U.IdPerfil, DateTime.Now, "dbo", "RH_Encuesta", administrador.IdAdministrador);

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

        public static ML.Result Delete(int IdAdministrador)
        {
            ML.Result result = new ML.Result();

            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                context.Database.Log = Console.Write;

                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var query = context.Database.ExecuteSqlCommand("DELETE FROM Administrador WHERE IdAdministrador = {0}", IdAdministrador);

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

        public static ML.Result UpdateEstatus(ML.Administrador admin)
        {
            ML.Result result = new ML.Result();
            //admin.CURRENT_USER
            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                context.Database.Log = Console.Write;
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        int newEstatus;
                        if (admin.IdentificadorEstatus == 1)
                        {
                            newEstatus = 2;
                            var query = context.Database.ExecuteSqlCommand
                            ("UPDATE Administrador SET IdEstatus = {0}, FechaHoraModificacion = {1}, UsuarioModificacion = {2}, ProgramaModificacion = {3} WHERE IdAdministrador = {4}", 
                                                        newEstatus, DateTime.Now, admin.CURRENT_USER, "Diagnostic4U", admin.IdAdministrador);

                            result.Correct = true;
                            context.SaveChanges();
                            transaction.Commit();
                        }
                        else if (admin.IdentificadorEstatus == 2)
                        {
                            newEstatus = 1;
                            var query = context.Database.ExecuteSqlCommand
                            ("UPDATE Administrador SET IdEstatus = {0}, FechaHoraModificacion = {1}, UsuarioModificacion = {2}, ProgramaModificacion = {3} WHERE IdAdministrador = {4}", 
                                                        newEstatus, DateTime.Now, admin.CURRENT_USER, "Diagnostic4U", admin.IdAdministrador);

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

        public static ML.Result GetPermisosByIdAdmin(ML.Administrador admin)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    
                    var queryAux = context.Administrador.SqlQuery("SELECT * FROM Administrador INNER JOIN Empleado ON Administrador.IdEmpleado = Empleado.IdEmpleado WHERE IdAdministrador = {0}", admin.IdAdministrador).ToList();
                    foreach (var item in queryAux)
                    {
                        ML.Administrador administradorData = new ML.Administrador();
                        administradorData.Empleado = new ML.Empleado();

                        administradorData.IdRH = item.IdRH == null ? 0 : (int)item.IdRH;
                        administradorData.IdAdministrador = item.IdAdministrador;
                        administradorData.Empleado.Nombre = item.Empleado.Nombre;
                        administradorData.Empleado.ApellidoPaterno = item.Empleado.ApellidoPaterno;
                        administradorData.Empleado.ApellidoMaterno = item.Empleado.ApellidoMaterno;

                        result.Object = administradorData;
                    }

                    /*
                     Meter en Object.Aux los datos de la encuestas
                     */
                    var queryMods = context.PerfilModulo.SqlQuery("SELECT * FROM PerfilModulo LEFT JOIN Administrador ON PerfilModulo.IdPerfil = Administrador.IdPerfil LEFT JOIN Empleado ON Administrador.IdEmpleado = Empleado.IdEmpleado LEFT JOIN PerfilD4U ON Administrador.IdPerfil = PerfilD4U.IdPerfil LEFT JOIN Modulo ON PerfilModulo.IdModulo = Modulo.IdModulo	WHERE Administrador.IdAdministrador = {0} and PerfilModulo.IdAdministrador = {1}", admin.IdAdministrador, admin.IdAdministrador).ToList();
                    result.ObjectsAux = new List<object>();
                    foreach (var item in queryMods)
                    {
                        ML.PerfilModulo perfM = new ML.PerfilModulo();
                        perfM.Modulo = new ML.Modulo();
                        perfM.PerfilD4U = new ML.PerfilD4U();

                        perfM.Modulo.IdModulo = item.Modulo.IdModulo;
                        perfM.Modulo.Nombre = item.Modulo.Nombre;

                        perfM.PerfilD4U.IdPerfil = item.PerfilD4U.IdPerfil;
                        perfM.PerfilD4U.Descripcion = item.PerfilD4U.Descripcion;
                        result.ObjectAux = perfM;
                        result.ObjectsAux.Add(perfM);
                    }


                    var queryPerfD4u = context.Administrador.SqlQuery("SELECT * FROM Administrador INNER JOIN PerfilD4U on Administrador.IdPerfil = PerfilD4U.IdPerfil where Administrador.IdAdministrador = {0}", admin.IdAdministrador).ToList();

                    foreach (var item in queryPerfD4u)
                    {
                        ML.PerfilModulo perf = new ML.PerfilModulo();
                        perf.PerfilD4U = new ML.PerfilD4U();

                        perf.PerfilD4U.Descripcion = item.PerfilD4U.Descripcion;
                        result.ObjectAux = perf;
                    }

                    
                    /*All permission and actions*/
                    var queryPermisosAccion = context.GetAllPermisosFullOK(admin.IdAdministrador).ToList();
                    result.ObjectsPermisos = new List<object>();
                    if (queryPermisosAccion != null)
                    {
                        foreach (var item in queryPermisosAccion)
                        {
                            ML.PerfilModulo perfilModulo = new ML.PerfilModulo();
                            perfilModulo.Administrador = new ML.Administrador();
                            perfilModulo.Empleado = new ML.Empleado();
                            perfilModulo.PerfilD4U = new ML.PerfilD4U();
                            perfilModulo.Modulo = new ML.Modulo();
                            perfilModulo.PerfilModuloAccion = new ML.PerfilModuloAccion();

                            perfilModulo.Administrador.IdAdministrador = Convert.ToInt32(item.IdAdministrador);
                            perfilModulo.Empleado.Nombre = item.Nombre;
                            perfilModulo.Empleado.ApellidoPaterno = item.ApellidoPaterno;
                            perfilModulo.Empleado.ApellidoMaterno = item.ApellidoMaterno;
                            perfilModulo.PerfilD4U.Descripcion = item.DescripcionPerfil;
                            perfilModulo.Modulo.IdModulo = Convert.ToInt32(item.IdModulo);
                            perfilModulo.Modulo.Nombre = item.NombreModulo;
                            perfilModulo.PerfilModuloAccion.Accion = item.Accion;

                            result.ObjectsPermisos.Add(perfilModulo);

                            result.Correct = true;

                        }
                    }
                    //Fin de todos permsiso con accion





                    var query = context.GetAllPermisosFull(admin.IdAdministrador).ToList();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.PerfilModulo perfilModulo = new ML.PerfilModulo();
                            
                            perfilModulo.Empleado = new ML.Empleado();
                            perfilModulo.PerfilD4U = new ML.PerfilD4U();
                            perfilModulo.Modulo = new ML.Modulo();


                            //perfilModulo.PerfilD4U.Descripcion = item.DescripcionPerfil;

                            perfilModulo.Modulo.Nombre = item.NombreModulo;

                            result.Objects.Add(perfilModulo);
                            //result.ObjectAux = perfilModulo;
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

        public static ML.Result GetAllPermisos(ML.Administrador admin)//Get All Permission with actions
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var queryPermisosAccion = context.GetAllPermisosOk(admin.IdAdministrador).ToList();
                    result.ObjectsPermisos = new List<object>();
                    if (queryPermisosAccion != null)
                    {
                        foreach (var item in queryPermisosAccion)
                        {
                            ML.PerfilModulo perfilModulo = new ML.PerfilModulo();
                            perfilModulo.Administrador = new ML.Administrador();
                            perfilModulo.Empleado = new ML.Empleado();
                            perfilModulo.PerfilD4U = new ML.PerfilD4U();
                            perfilModulo.Modulo = new ML.Modulo();
                            perfilModulo.PerfilModuloAccion = new ML.PerfilModuloAccion();

                            perfilModulo.Administrador.IdAdministrador = Convert.ToInt32(item.IdAdministrador);
                            perfilModulo.Empleado.Nombre = item.Nombre;
                            perfilModulo.Empleado.ApellidoPaterno = item.ApellidoPaterno;
                            perfilModulo.Empleado.ApellidoMaterno = item.ApellidoMaterno;
                            perfilModulo.PerfilD4U.Descripcion = item.DescripcionPerfil;
                            perfilModulo.Modulo.Nombre = item.NombreModulo;
                            perfilModulo.PerfilModuloAccion.Accion = item.Accion;

                            result.ObjectsPermisos.Add(perfilModulo);

                            result.Correct = true;

                        }
                    }
                    else
                    {
                        result.ErrorMessage = "No se pudieron obtener los resultados";
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

        public static ML.Result IsMaster(int IdEmpleadoForNewAdmin)
        {
            //
            //            SELECT* FROM PerfilD4U INNER JOIN Administrador ON PerfilD4U.IdPerfil = Administrador.IdPerfil
            //WHERE Administrador.IdEmpleado = 55555
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.PerfilD4U.SqlQuery("SELECT* FROM PerfilD4U INNER JOIN Administrador ON PerfilD4U.IdPerfil = Administrador.IdPerfil WHERE Administrador.IdEmpleado = {0} ", IdEmpleadoForNewAdmin).ToList();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.PerfilD4U perf = new ML.PerfilD4U();
                            perf.IdPerfil = item.IdPerfil;
                            perf.Descripcion = item.Descripcion;

                            if (perf.IdPerfil == 1)
                            {
                                result.IsMaster = true;
                            }
                            else
                            {
                                result.IsMaster = false;
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
		 /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static ML.Result isResponsable(string email)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var responsable = context.Responsable.Where(o => o.Email == email).FirstOrDefault();
                    if (responsable != null)
                    {
                        result.Correct = true;
                        result.NewId = responsable.IdResponsable;
                    }
                    else
                    {
                        result.Correct = true;
                        result.NewId = 0;
                    }
                }
            }
            catch (Exception aE)
            {
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.NewId = 0;
            }
            return result;

        }
        public static ML.Result GetAllId()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Administrador.SqlQuery("SELECT * FROM ADMINISTRADOR WHERE IDESTATUS = 1").ToList();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Administrador admin = new ML.Administrador();

                            admin.IdAdministrador = item.IdAdministrador;

                            result.Objects.Add(admin);
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

        public static ML.Result RefreshPermisos(int IdAdministrador)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.GetAllPermisosFullOK(IdAdministrador).ToList();
                    result.ObjectsPermisos = new List<object>();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.PerfilModulo perfilModulo = new ML.PerfilModulo();
                            perfilModulo.Administrador = new ML.Administrador();
                            perfilModulo.Administrador.Empleado = new ML.Empleado();
                            perfilModulo.PerfilD4U = new ML.PerfilD4U();
                            perfilModulo.Modulo = new ML.Modulo();
                            perfilModulo.PerfilModuloAccion = new ML.PerfilModuloAccion();
                            perfilModulo.Acciones = new List<string>();
                            /*
                            Get Modulos & Acciones from Admin
                            Get Full Name Empleado    
                            Solo puede acceder si esta activo
                            */
                            perfilModulo.Administrador.IdAdministrador = Convert.ToInt32(item.IdAdministrador);
                            perfilModulo.Administrador.Empleado.Nombre = item.Nombre;
                            perfilModulo.Administrador.Empleado.ApellidoPaterno = item.ApellidoPaterno;
                            perfilModulo.Administrador.Empleado.ApellidoMaterno = item.ApellidoMaterno;
                            perfilModulo.PerfilD4U.Descripcion = item.DescripcionPerfil;
                            perfilModulo.Modulo.IdModulo = Convert.ToInt32(item.IdModulo);
                            perfilModulo.Modulo.Nombre = item.NombreModulo;
                            perfilModulo.PerfilModuloAccion.Accion = item.Accion;

                            result.ObjectsPermisos.Add(perfilModulo);
                     

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

        public static ML.Result DeleteAdminFromEstatus(ML.Administrador admin)
        {
            ML.Result result = new ML.Result();

            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var query = context.Database.ExecuteSqlCommand
                            ("UPDATE ADMINISTRADOR SET IDESTATUS = 3, FechaHoraEliminacion = {0}, UsuarioEliminacion = {1}, ProgramaEliminacion = {2} WHERE IDADMINISTRADOR = {3}", 
                                                                        DateTime.Now, admin.CURRENT_USER, "Diagnostic4U" , admin.IdAdministrador);

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

        public static ML.Result GetAllEliminadosForMaster(int CURRENT_IDEMPLEADO_LOG, int CURRENTADMINLOG)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.GetAdminEliminadosForMasterOK__(CURRENT_IDEMPLEADO_LOG, CURRENTADMINLOG).ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Administrador admin = new ML.Administrador();

                            admin.IdAdministrador = item.IdAdministrador;
                            admin.Empleado = new ML.Empleado();
                            admin.Empleado.IdEmpleado = Convert.ToInt32(item.IdEmpleado);
                            admin.Empleado.Nombre = item.Nombre;
                            admin.Empleado.ApellidoPaterno = item.ApellidoPaterno;
                            admin.Empleado.ApellidoMaterno = item.ApellidoMaterno;
                            admin.PerfilD4U = new ML.PerfilD4U();
                            admin.PerfilD4U.IdPerfil = Convert.ToInt32(item.IdPerfil);
                            admin.PerfilD4U.Descripcion = item.DescripcionPerfil;
                            admin.TipoEstatus = new ML.TipoEstatus();
                            admin.TipoEstatus.IdEstatus = Convert.ToInt32(item.IdEstatus);
                            admin.TipoEstatus.Descripcion = item.DescripcionEstatus;

                            result.Objects.Add(admin);
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

        public static ML.Result GetAllEliminadosForNotMaster(int CURRENT_IDEMPLEADO_LOG, int IdAdminLog)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.GetAdminEliminadosForNotMasterOK__(CURRENT_IDEMPLEADO_LOG, IdAdminLog).ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Administrador admin = new ML.Administrador();

                            admin.IdAdministrador = item.IdAdministrador;
                            admin.Empleado = new ML.Empleado();
                            admin.Empleado.IdEmpleado = Convert.ToInt32(item.IdEmpleado);
                            admin.Empleado.Nombre = item.Nombre;
                            admin.Empleado.ApellidoPaterno = item.ApellidoPaterno;
                            admin.Empleado.ApellidoMaterno = item.ApellidoMaterno;
                            admin.PerfilD4U = new ML.PerfilD4U();
                            admin.PerfilD4U.IdPerfil = Convert.ToInt32(item.IdPerfil);
                            admin.PerfilD4U.Descripcion = item.DescripcionPerfil;
                            admin.TipoEstatus = new ML.TipoEstatus();
                            admin.TipoEstatus.IdEstatus = Convert.ToInt32(item.IdEstatus);
                            admin.TipoEstatus.Descripcion = item.DescripcionEstatus;

                            result.Objects.Add(admin);
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

        public static ML.Result RestaurarAdmin(int IdAdmin)
        {
            ML.Result result = new ML.Result();

            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var query = context.Database.ExecuteSqlCommand
                            ("UPDATE ADMINISTRADOR SET IDESTATUS = 1 WHERE IDADMINISTRADOR = {0}", IdAdmin);

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



        public static ML.Result GetCompaniesByName(string name)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Company.SqlQuery("select * from COMPANY INNER JOIN CompanyCategoria ON Company.IdCompanyCategoria = CompanyCategoria.IdCompanyCategoria WHERE CompanyCategoria.Descripcion = {0} and company.tipo = 1", name ).ToList();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Company company = new ML.Company();

                            company.CompanyId = item.CompanyId;
                            company.CompanyName = item.CompanyName;

                            result.Objects.Add(company);

                            result.Correct = true;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
            }
            return result;
        }

        //GetCompanyId
        public static ML.Result GetCompanyId(string companyname)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Company.SqlQuery("SELECT * FROM COMPANY WHERE COMPANYNAME = {0}", companyname).ToList();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Company cmpany = new ML.Company();
                            cmpany.CompanyId = item.CompanyId;
                            cmpany.CompanyName = item.CompanyName;

                            result.Objects.Add(cmpany);
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
        public static ML.Result AddusuarioCompany(int iDaDMINISTRADOR, int Companyid, string currentUser)
        {
            ML.Result result = new ML.Result();

            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        //administrador.CURRENT_USER
                        var query = context.Database.ExecuteSqlCommand("INSERT INTO ADMINISTRADORCOMPANY (IDADMINISTRADOR, COMPANYID, FECHAHORACREACION, USUARIOCREACION, PROGRAMACREACION) VALUES ({0}, {1}, {2}, {3}, {4})", iDaDMINISTRADOR, Companyid, DateTime.Now, currentUser, "Diagnostic4U");

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

        public static ML.Result GetCompaniesForPermisos(int IdAdministrador)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.AdministradorCompany.SqlQuery("SELECT * FROM AdministradorCompany INNER JOIN Administrador ON AdministradorCOMPANY.IDADMINISTRADOR = Administrador.IdAdministrador INNER JOIN Company ON AdministradorCOMPANY.COMPANYID = Company.CompanyId INNER JOIN Empleado ON Administrador.IdEmpleado = Empleado.IdEmpleado where Administrador.IdAdministrador = {0}", IdAdministrador).ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.AdministradorCompany adminCompany = new ML.AdministradorCompany();
                            adminCompany.Administrador = new ML.Administrador();
                            adminCompany.Company = new ML.Company();

                            adminCompany.Administrador.IdAdministrador = item.Administrador.IdAdministrador;
                            adminCompany.Company.CompanyId = item.Company.CompanyId;
                            adminCompany.Company.CompanyName = item.Company.CompanyName;

                            result.Objects.Add(adminCompany);
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

        //Recovery Account
        public static ML.Administrador FindByUserName(string UserName)
        {
            ML.Administrador admin = new ML.Administrador();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Administrador.SqlQuery("SELECT * FROM ADMINISTRADOR INNER JOIN EMPLEADO ON ADMINISTRADOR.IDEMPLEADO = EMPLEADO.IDEMPLEADO WHERE ADMINISTRADOR.USERNAME = {0}", UserName).ToList();
                    if (query.Count > 0)
                    {
                        foreach (var item in query)
                        {
                            admin.IdAdministrador = item.IdAdministrador;
                            admin.Empleado = new ML.Empleado();
                            admin.Empleado.Nombre = item.Empleado.Nombre;
                            admin.Empleado.ApellidoPaterno = item.Empleado.ApellidoPaterno;
                            admin.Empleado.ApellidoMaterno = item.Empleado.ApellidoMaterno;
                            admin.UserName = item.UserName;
                        }
                    }
                    else
                    {
                        admin = null;
                    }
                }
            }
            catch (Exception ex)
            {
                admin = null;
            }
            return admin;
        }
        public static string GeneratePasswordResetToken(int userId)
        {
            int size = 30;
            char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[4 * size];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(size);
            for (int i = 0; i < size; i++)
            {
                var rnd = BitConverter.ToUInt32(data, i * 4);
                var idx = rnd % chars.Length;

                result.Append(chars[idx]);
            }

            return result.ToString();
        }
        public static ML.Result ResetPassword(int IdAdmin, string Code, string newPassword)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Database.ExecuteSqlCommand("UPDATE Administrador SET Password = {0} WHERE IdAdministrador = {1}", newPassword, IdAdmin);

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
        public static ML.Result SendEmailRecovery(string correo, string Subject, string message)
        {
            ML.Result result = new ML.Result();
            var body =
            "<p style='font-weight:bold;'>Que tal</p>" +
            "<p>Has solicitado el restablecimiento de tu contraseña para el portal <b>Diagnostic4U</b></p>" +
            "<p> "+ message +" </p>" +
            "<p><img src='http://demo.climalaboral.divisionautomotriz.com/img/logo.png'></p></ br>";
            var msgEmail = new MailMessage();
            msgEmail.To.Add(new MailAddress(correo));
            msgEmail.Subject = Subject;
            msgEmail.Body = string.Format(body, "DIAGNOSTIC4U", "jamurillo@grupoautofin.com", "Aqui se envian  las claves de acceso al portal");
            msgEmail.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                try
                {
                    smtp.Send(msgEmail);
                    result.Correct = true;
                }
                catch (Exception ex)
                {
                    result.ErrorMessage = ex.Message;
                    result.Correct = false;
                }
                finally
                {
                    smtp.Dispose();
                }
            }
            return result;
        }


        //User SA
        public static ML.Result PermisoToAllEnterprise(int IdAdmin, string AdminCreate)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var getAllCompanies = context.Company.SqlQuery("SELECT * FROM COMPANY").ToList();
                    foreach (var item in getAllCompanies)
                    {
                        var insertPermisos = context.Database.ExecuteSqlCommand
                            ("INSERT INTO ADMINISTRADORCOMPANY (IDADMINISTRADOR, COMPANYID, FECHAHORACREACION, USUARIOCREACION, PROGRAMACREACION) VALUES ({0}, {1}, {2}, {3}, {4})", IdAdmin, item.CompanyId, DateTime.Now, AdminCreate, "Diagnostic4U");

                        context.SaveChanges();
                        result.Correct = true;
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


        public static ML.Result UpdateSA(int IdAdmin, int AdminSA)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Database.ExecuteSqlCommand("UPDATE ADMINISTRADOR SET ADMINSA = {0} WHERE IDADMINISTRADOR = {1}", AdminSA, IdAdmin);

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

        public static ML.Result EditUsername(ML.Administrador admin)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    //Get idempleado de ese admi
                    var query = context.Administrador.SqlQuery("SELECT * FROM ADMINISTRADOR WHERE IDADMINISTRADOR = {0}", admin.IdAdministrador).ToList();
                    ML.Administrador administrador = new ML.Administrador();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            administrador.IdAdministrador = item.IdAdministrador;
                            administrador.Empleado = new ML.Empleado();
                            administrador.Empleado.IdEmpleado = Convert.ToInt32(item.IdEmpleado);
                        }
                    }
                    var updateEmple = context.Database.ExecuteSqlCommand("UPDATE EMPLEADO SET CORREO = {0} WHERE IDEMPLEADO = {1}", admin.UserName, administrador.Empleado.IdEmpleado);
                    var updateAdmin = context.Database.ExecuteSqlCommand("UPDATE ADMINISTRADOR SET USERNAME = {0} WHERE IDADMINISTRADOR = {1}", admin.UserName, admin.IdAdministrador);

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

        public static ML.Result ObtenTodos()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Administrador.SqlQuery("SELECT * FROM ADMINISTRADOR WHERE IDESTATUS = 1 or idestatus = 2");

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Administrador admin = new ML.Administrador();

                            admin.IdAdministrador = item.IdAdministrador;
                            admin.Empleado = new ML.Empleado();
                            admin.Empleado.IdEmpleado = Convert.ToInt32(item.IdEmpleado);
                            admin.Empleado.Nombre = item.Empleado.Nombre;
                            admin.Empleado.ApellidoPaterno = item.Empleado.ApellidoPaterno;
                            admin.Empleado.ApellidoMaterno = item.Empleado.ApellidoMaterno;
                            admin.PerfilD4U = new ML.PerfilD4U();
                            admin.PerfilD4U.IdPerfil = Convert.ToInt32(item.IdPerfil);
                            admin.PerfilD4U.Descripcion = item.PerfilD4U.Descripcion;
                            admin.TipoEstatus = new ML.TipoEstatus();
                            admin.TipoEstatus.IdEstatus = Convert.ToInt32(item.IdEstatus);
                            admin.UserName = item.UserName;

                            result.Objects.Add(admin);
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
        public static ML.Result ObtenTodosEliminados()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Administrador.SqlQuery("SELECT * FROM ADMINISTRADOR WHERE IDESTATUS = 3");

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Administrador admin = new ML.Administrador();

                            admin.IdAdministrador = item.IdAdministrador;
                            admin.Empleado = new ML.Empleado();
                            admin.Empleado.IdEmpleado = Convert.ToInt32(item.IdEmpleado);
                            admin.Empleado.Nombre = item.Empleado.Nombre;
                            admin.Empleado.ApellidoPaterno = item.Empleado.ApellidoPaterno;
                            admin.Empleado.ApellidoMaterno = item.Empleado.ApellidoMaterno;
                            admin.PerfilD4U = new ML.PerfilD4U();
                            admin.PerfilD4U.IdPerfil = Convert.ToInt32(item.IdPerfil);
                            admin.PerfilD4U.Descripcion = item.PerfilD4U.Descripcion;
                            admin.TipoEstatus = new ML.TipoEstatus();
                            admin.TipoEstatus.IdEstatus = Convert.ToInt32(item.IdEstatus);
                            admin.UserName = item.UserName;

                            result.Objects.Add(admin);
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
