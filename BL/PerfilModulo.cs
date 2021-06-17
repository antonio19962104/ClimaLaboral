using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class PerfilModulo
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.PerfilModulo.SqlQuery("SELECT * FROM PerfilModulo INNER JOIN PerfilD4U ON PerfilModulo.IdPerfil = PerfilD4U.IdPerfil INNER JOIN Modulo ON PerfilModulo.IdModulo = Modulo.IdModulo").ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.PerfilModulo PerfilModulo = new ML.PerfilModulo();

                            PerfilModulo.IdPerfilModulo = item.IdPerfilModulo;
                            PerfilModulo.PerfilD4U = new ML.PerfilD4U();
                            PerfilModulo.Modulo = new ML.Modulo();

                            PerfilModulo.PerfilD4U.IdPerfil = Convert.ToInt32(item.IdPerfil);
                            PerfilModulo.PerfilD4U.Descripcion = item.PerfilD4U.Descripcion;

                            PerfilModulo.Modulo.IdModulo = Convert.ToInt32(item.IdModulo);
                            PerfilModulo.Modulo.Nombre = item.Modulo.Nombre;

                            result.Objects.Add(PerfilModulo);
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

        public static ML.Result GetByIdPerfil(int IdPerfil)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.PerfilModulo.SqlQuery("SELECT * FROM PerfilModulo INNER JOIN PerfilD4U ON PerfilModulo.IdPerfil = PerfilD4U.IdPerfil INNER JOIN Modulo ON PerfilModulo.IdModulo = Modulo.IdModulo WHERE PerfilModulo.IdPerfil = {0}", IdPerfil).ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.PerfilModulo PerfilModulo = new ML.PerfilModulo();

                            PerfilModulo.IdPerfilModulo = item.IdPerfilModulo;
                            PerfilModulo.PerfilD4U = new ML.PerfilD4U();
                            PerfilModulo.Modulo = new ML.Modulo();

                            PerfilModulo.PerfilD4U.IdPerfil = Convert.ToInt32(item.IdPerfil);
                            PerfilModulo.PerfilD4U.Descripcion = item.PerfilD4U.Descripcion;

                            PerfilModulo.Modulo.IdModulo = Convert.ToInt32(item.IdModulo);
                            PerfilModulo.Modulo.Nombre = item.Modulo.Nombre;

                            result.Objects.Add(PerfilModulo);

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

        public static ML.Result GetByIdAdministrador(int IdAdministrador)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.PerfilModulo.SqlQuery("SELECT * FROM PerfilModulo INNER JOIN PerfilD4U ON PerfilModulo.IdPerfil = PerfilD4U.IdPerfil INNER JOIN Modulo ON PerfilModulo.IdModulo = Modulo.IdModulo INNER JOIN Administrador ON PerfilD4U.IdPerfil = Administrador.IdPerfil WHERE Administrador.IdAdministrador = {0}", IdAdministrador).ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.PerfilModulo PerfilModulo = new ML.PerfilModulo();

                            PerfilModulo.IdPerfilModulo = item.IdPerfilModulo;
                            PerfilModulo.PerfilD4U = new ML.PerfilD4U();
                            PerfilModulo.Modulo = new ML.Modulo();


                            PerfilModulo.PerfilD4U.IdPerfil = Convert.ToInt32(item.IdPerfil);
                            PerfilModulo.PerfilD4U.Descripcion = item.PerfilD4U.Descripcion;

                            PerfilModulo.Modulo.IdModulo = Convert.ToInt32(item.IdModulo);
                            PerfilModulo.Modulo.Nombre = item.Modulo.Nombre;

                            result.Objects.Add(PerfilModulo);
                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.ErrorMessage = "No se encontraron registros usando el Id del administrador proporcionado";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }//GetByIdAdministrador

        public static ML.Result Add(int IdPerfil, int IdModulo, int IdAdministrador, ML.Administrador admin)
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
                            ("INSERT INTO PerfilModulo (IdPerfil, IdModulo, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion, IdAdministrador) VALUES ({0}, {1}, {2}, {3}, {4}, {5})", 
                            IdPerfil, IdModulo, DateTime.Now, admin.CURRENT_USER, "D4U", IdAdministrador);

                        int IdPerfilModulo = context.PerfilModulo.Max(p => p.IdPerfilModulo);
                        //Tomar el Ultimo IDInsertado y guardar perfilModuloAccion
                        //(IdPerfilModulo, Accion)
                        if (IdPerfil == 1)
                        {
                            if (IdModulo == 1)//Encuestas
                            {
                                foreach (var item in admin.Acciones)
                                {
                                    if (item == "ListarEncuesta")
                                    {
                                        var queryAciones = context.Database.ExecuteSqlCommand
                                        ("INSERT INTO PerfilmoduloAccion (IdPerfilModulo, Accion, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4})",
                                        IdPerfilModulo, "ListarEncuesta", DateTime.Now, admin.CURRENT_USER, "D4U");
                                    }
                                    if (item == "CrearEncuesta")
                                    {
                                        var queryAciones = context.Database.ExecuteSqlCommand
                                        ("INSERT INTO PerfilmoduloAccion (IdPerfilModulo, Accion, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4})",
                                        IdPerfilModulo, "CrearEncuesta", DateTime.Now, admin.CURRENT_USER, "D4U");
                                    }
                                    if (item == "OpcEncuesta")
                                    {
                                        var queryAciones = context.Database.ExecuteSqlCommand
                                        ("INSERT INTO PerfilmoduloAccion (IdPerfilModulo, Accion, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4})",
                                        IdPerfilModulo, "OpcEncuesta", DateTime.Now, admin.CURRENT_USER, "D4U");
                                    }
                                }
                            }
                            if (IdModulo == 2)//Plantillas
                            {
                                foreach (var item in admin.Acciones)
                                {
                                    if (item == "ListarPlantilla")
                                    {
                                        var queryAciones = context.Database.ExecuteSqlCommand
                                        ("INSERT INTO PerfilmoduloAccion (IdPerfilModulo, Accion, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4})",
                                        IdPerfilModulo, "ListarPlantilla", DateTime.Now, admin.CURRENT_USER, "D4U");
                                    }
                                    if (item == "CrearPlantilla")
                                    {
                                        var queryAciones = context.Database.ExecuteSqlCommand
                                        ("INSERT INTO PerfilmoduloAccion (IdPerfilModulo, Accion, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4})",
                                        IdPerfilModulo, "CrearPlantilla", DateTime.Now, admin.CURRENT_USER, "D4U");
                                    }
                                    if (item == "OpcPlantilla")
                                    {
                                        var queryAciones = context.Database.ExecuteSqlCommand
                                        ("INSERT INTO PerfilmoduloAccion (IdPerfilModulo, Accion, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4})",
                                        IdPerfilModulo, "OpcPlantilla", DateTime.Now, admin.CURRENT_USER, "D4U");
                                    }
                                }
                            }
                            if (IdModulo == 3)//Reportes
                            {
                                foreach (var item in admin.Acciones)
                                {
                                    if (item == "ListarReporte")
                                    {
                                        var queryAciones = context.Database.ExecuteSqlCommand
                                        ("INSERT INTO PerfilmoduloAccion (IdPerfilModulo, Accion, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4})",
                                        IdPerfilModulo, "ListarReporte", DateTime.Now, admin.CURRENT_USER, "D4U");
                                    }
                                    if (item == "CrearReporte")
                                    {
                                        var queryAciones = context.Database.ExecuteSqlCommand
                                        ("INSERT INTO PerfilmoduloAccion (IdPerfilModulo, Accion, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4})",
                                        IdPerfilModulo, "CrearReporte", DateTime.Now, admin.CURRENT_USER, "D4U");
                                    }
                                    if (item == "OpcReporte")
                                    {
                                        var queryAciones = context.Database.ExecuteSqlCommand
                                        ("INSERT INTO PerfilmoduloAccion (IdPerfilModulo, Accion, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4})",
                                        IdPerfilModulo, "OpcReporte", DateTime.Now, admin.CURRENT_USER, "D4U");
                                    }
                                }
                            }
                            if (IdModulo == 4)//DB
                            {
                                foreach (var item in admin.Acciones)
                                {
                                    if (item == "ListarBD")
                                    {
                                        var queryAciones = context.Database.ExecuteSqlCommand
                                        ("INSERT INTO PerfilmoduloAccion (IdPerfilModulo, Accion, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4})",
                                        IdPerfilModulo, "ListarBD", DateTime.Now, admin.CURRENT_USER, "D4U");
                                    }
                                    if (item == "CrearBD")
                                    {
                                        var queryAciones = context.Database.ExecuteSqlCommand
                                        ("INSERT INTO PerfilmoduloAccion (IdPerfilModulo, Accion, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4})",
                                        IdPerfilModulo, "CrearBD", DateTime.Now, admin.CURRENT_USER, "D4U");
                                    }
                                    if (item == "OpcBD")
                                    {
                                        var queryAciones = context.Database.ExecuteSqlCommand
                                        ("INSERT INTO PerfilmoduloAccion (IdPerfilModulo, Accion, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4})",
                                        IdPerfilModulo, "OpcBD", DateTime.Now, admin.CURRENT_USER, "D4U");
                                    }
                                }
                            }
                            if (IdModulo == 5)//Usuarios
                            {
                                foreach (var item in admin.Acciones)
                                {
                                    if (item == "ListarUsuario")
                                    {
                                        var queryAciones = context.Database.ExecuteSqlCommand
                                        ("INSERT INTO PerfilmoduloAccion (IdPerfilModulo, Accion, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4})",
                                        IdPerfilModulo, "ListarUsuario", DateTime.Now, admin.CURRENT_USER, "D4U");
                                    }
                                    if (item == "CrearUsuario")
                                    {
                                        var queryAciones = context.Database.ExecuteSqlCommand
                                        ("INSERT INTO PerfilmoduloAccion (IdPerfilModulo, Accion, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4})",
                                        IdPerfilModulo, "CrearUsuario", DateTime.Now, admin.CURRENT_USER, "D4U");
                                    }
                                    if (item == "OpcUsuario")
                                    {
                                        var queryAciones = context.Database.ExecuteSqlCommand
                                        ("INSERT INTO PerfilmoduloAccion (IdPerfilModulo, Accion, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4})",
                                        IdPerfilModulo, "OpcUsuario", DateTime.Now, admin.CURRENT_USER, "D4U");
                                    }
                                }
                            }
                            if (IdModulo == 6)//Empresas
                            {
                                foreach (var item in admin.Acciones)
                                {
                                    if (item == "ListarEmpresa")
                                    {
                                        var queryAciones = context.Database.ExecuteSqlCommand
                                        ("INSERT INTO PerfilmoduloAccion (IdPerfilModulo, Accion, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4})",
                                        IdPerfilModulo, "ListarEmpresa", DateTime.Now, admin.CURRENT_USER, "D4U");
                                    }
                                    if (item == "CrearEmpresa")
                                    {
                                        var queryAciones = context.Database.ExecuteSqlCommand
                                        ("INSERT INTO PerfilmoduloAccion (IdPerfilModulo, Accion, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4})",
                                        IdPerfilModulo, "CrearEmpresa", DateTime.Now, admin.CURRENT_USER, "D4U");
                                    }
                                    if (item == "OpcEmpresa")
                                    {
                                        var queryAciones = context.Database.ExecuteSqlCommand
                                        ("INSERT INTO PerfilmoduloAccion (IdPerfilModulo, Accion, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4})",
                                        IdPerfilModulo, "OpcEmpresa", DateTime.Now, admin.CURRENT_USER, "D4U");
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (var item in admin.Acciones)
                            {
                                if (item != null)
                                {
                                    var queryAciones = context.Database.ExecuteSqlCommand
                                        ("INSERT INTO PerfilmoduloAccion (IdPerfilModulo, Accion, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4})",
                                        IdPerfilModulo, item, DateTime.Now, admin.CURRENT_USER, "D4U");
                                }
                            }
                        }

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

        public static ML.Result Update(int IdPerfil, int IdModulo, int IdAdmin, ML.PerfilModulo perfilM)
        {
            ML.Result result = new ML.Result();

            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                //context.Database.Log;
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        //Get IdPerfilModuloPara hacer borrado de Acciones
                        var query = context.PerfilModulo.SqlQuery("SELECT * FROM PerfilModulo WHERE IdAdministrador = {0} AND IdPerfil = {1} AND IdModulo = {2}", IdAdmin, IdPerfil, IdModulo).ToList();
                        foreach (var item in query)
                        {
                            ML.PerfilModulo gettingIdPerfilModulo = new ML.PerfilModulo();
                            gettingIdPerfilModulo.IdPerfilModulo = item.IdPerfilModulo;

                            var borrado = context.Database.ExecuteSqlCommand("DELETE FROM PerfilModuloAccion WHERE IdPerfilModulo = {0}", gettingIdPerfilModulo.IdPerfilModulo);

                            //insertar tomando el IdPerfilModulo que obtuve para borrar
                            if (IdPerfil == 1)
                            {
                                if (IdModulo == 1)//Encuestas
                                {
                                    foreach (var itemActions in perfilM.Acciones)
                                    {
                                        if (itemActions == "ListarEncuesta")
                                        {
                                            var queryAciones = context.Database.ExecuteSqlCommand
                                            ("INSERT INTO PerfilmoduloAccion (IdPerfilModulo, Accion, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4})",
                                            gettingIdPerfilModulo.IdPerfilModulo, "ListarEncuesta", DateTime.Now, perfilM.CURRENT_USER, "D4U");
                                        }
                                        if (itemActions == "CrearEncuesta")
                                        {
                                            var queryAciones = context.Database.ExecuteSqlCommand
                                            ("INSERT INTO PerfilmoduloAccion (IdPerfilModulo, Accion, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4})",
                                            gettingIdPerfilModulo.IdPerfilModulo, "CrearEncuesta", DateTime.Now, perfilM.CURRENT_USER, "D4U");
                                        }
                                        if (itemActions == "OpcEncuesta")
                                        {
                                            var queryAciones = context.Database.ExecuteSqlCommand
                                            ("INSERT INTO PerfilmoduloAccion (IdPerfilModulo, Accion, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4})",
                                            gettingIdPerfilModulo.IdPerfilModulo, "OpcEncuesta", DateTime.Now, perfilM.CURRENT_USER, "D4U");
                                        }
                                    }
                                }
                                if (IdModulo == 2)//Plantillas
                                {
                                    foreach (var itemActions in perfilM.Acciones)
                                    {
                                        if (itemActions == "ListarPlantilla")
                                        {
                                            var queryAciones = context.Database.ExecuteSqlCommand
                                            ("INSERT INTO PerfilmoduloAccion (IdPerfilModulo, Accion, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4})",
                                            gettingIdPerfilModulo.IdPerfilModulo, "ListarPlantilla", DateTime.Now, perfilM.CURRENT_USER, "D4U");
                                        }
                                        if (itemActions == "CrearPlantilla")
                                        {
                                            var queryAciones = context.Database.ExecuteSqlCommand
                                            ("INSERT INTO PerfilmoduloAccion (IdPerfilModulo, Accion, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4})",
                                            gettingIdPerfilModulo.IdPerfilModulo, "CrearPlantilla", DateTime.Now, perfilM.CURRENT_USER, "D4U");
                                        }
                                        if (itemActions == "OpcPlantilla")
                                        {
                                            var queryAciones = context.Database.ExecuteSqlCommand
                                            ("INSERT INTO PerfilmoduloAccion (IdPerfilModulo, Accion, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4})",
                                            gettingIdPerfilModulo.IdPerfilModulo, "OpcPlantilla", DateTime.Now, perfilM.CURRENT_USER, "D4U");
                                        }
                                    }
                                }
                                if (IdModulo == 3)//Reportes
                                {
                                    foreach (var itemActions in perfilM.Acciones)
                                    {
                                        if (itemActions == "ListarReporte")
                                        {
                                            var queryAciones = context.Database.ExecuteSqlCommand
                                            ("INSERT INTO PerfilmoduloAccion (IdPerfilModulo, Accion, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4})",
                                            gettingIdPerfilModulo.IdPerfilModulo, "ListarReporte", DateTime.Now, perfilM.CURRENT_USER, "D4U");
                                        }
                                        if (itemActions == "CrearReporte")
                                        {
                                            var queryAciones = context.Database.ExecuteSqlCommand
                                            ("INSERT INTO PerfilmoduloAccion (IdPerfilModulo, Accion, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4})",
                                            gettingIdPerfilModulo.IdPerfilModulo, "CrearReporte", DateTime.Now, perfilM.CURRENT_USER, "D4U");
                                        }
                                        if (itemActions == "OpcReporte")
                                        {
                                            var queryAciones = context.Database.ExecuteSqlCommand
                                            ("INSERT INTO PerfilmoduloAccion (IdPerfilModulo, Accion, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4})",
                                            gettingIdPerfilModulo.IdPerfilModulo, "OpcReporte", DateTime.Now, perfilM.CURRENT_USER, "D4U");
                                        }
                                    }
                                }
                                if (IdModulo == 4)//DB
                                {
                                    foreach (var itemActions in perfilM.Acciones)
                                    {
                                        if (itemActions == "ListarBD")
                                        {
                                            var queryAciones = context.Database.ExecuteSqlCommand
                                            ("INSERT INTO PerfilmoduloAccion (IdPerfilModulo, Accion, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4})",
                                            gettingIdPerfilModulo.IdPerfilModulo, "ListarBD", DateTime.Now, perfilM.CURRENT_USER, "D4U");
                                        }
                                        if (itemActions == "CrearBD")
                                        {
                                            var queryAciones = context.Database.ExecuteSqlCommand
                                            ("INSERT INTO PerfilmoduloAccion (IdPerfilModulo, Accion, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4})",
                                            gettingIdPerfilModulo.IdPerfilModulo, "CrearBD", DateTime.Now, perfilM.CURRENT_USER, "D4U");
                                        }
                                        if (itemActions == "OpcBD")
                                        {
                                            var queryAciones = context.Database.ExecuteSqlCommand
                                            ("INSERT INTO PerfilmoduloAccion (IdPerfilModulo, Accion, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4})",
                                            gettingIdPerfilModulo.IdPerfilModulo, "OpcBD", DateTime.Now, perfilM.CURRENT_USER, "D4U");
                                        }
                                    }
                                }
                                if (IdModulo == 5)//Usuarios
                                {
                                    foreach (var itemActions in perfilM.Acciones)
                                    {
                                        if (itemActions == "ListarUsuario")
                                        {
                                            var queryAciones = context.Database.ExecuteSqlCommand
                                            ("INSERT INTO PerfilmoduloAccion (IdPerfilModulo, Accion, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4})",
                                            gettingIdPerfilModulo.IdPerfilModulo, "ListarUsuario", DateTime.Now, perfilM.CURRENT_USER, "D4U");
                                        }
                                        if (itemActions == "CrearUsuario")
                                        {
                                            var queryAciones = context.Database.ExecuteSqlCommand
                                            ("INSERT INTO PerfilmoduloAccion (IdPerfilModulo, Accion, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4})",
                                            gettingIdPerfilModulo.IdPerfilModulo, "CrearUsuario", DateTime.Now, perfilM.CURRENT_USER, "D4U");
                                        }
                                        if (itemActions == "OpcUsuario")
                                        {
                                            var queryAciones = context.Database.ExecuteSqlCommand
                                            ("INSERT INTO PerfilmoduloAccion (IdPerfilModulo, Accion, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4})",
                                            gettingIdPerfilModulo.IdPerfilModulo, "OpcUsuario", DateTime.Now, perfilM.CURRENT_USER, "D4U");
                                        }
                                    }
                                }
                                if (IdModulo == 6)//Empresas
                                {
                                    foreach (var itemActions in perfilM.Acciones)
                                    {
                                        if (itemActions == "ListarEmpresa")
                                        {
                                            var queryAciones = context.Database.ExecuteSqlCommand
                                            ("INSERT INTO PerfilmoduloAccion (IdPerfilModulo, Accion, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4})",
                                            gettingIdPerfilModulo.IdPerfilModulo, "ListarEmpresa", DateTime.Now, perfilM.CURRENT_USER, "D4U");
                                        }
                                        if (itemActions == "CrearEmpresa")
                                        {
                                            var queryAciones = context.Database.ExecuteSqlCommand
                                            ("INSERT INTO PerfilmoduloAccion (IdPerfilModulo, Accion, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4})",
                                            gettingIdPerfilModulo.IdPerfilModulo, "CrearEmpresa", DateTime.Now, perfilM.CURRENT_USER, "D4U");
                                        }
                                        if (itemActions == "OpcEmpresa")
                                        {
                                            var queryAciones = context.Database.ExecuteSqlCommand
                                            ("INSERT INTO PerfilmoduloAccion (IdPerfilModulo, Accion, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4})",
                                            gettingIdPerfilModulo.IdPerfilModulo, "OpcEmpresa", DateTime.Now, perfilM.CURRENT_USER, "D4U");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                foreach (var itemActions in perfilM.Acciones)
                                {
                                    if (itemActions != "False")
                                    {
                                        var queryAciones = context.Database.ExecuteSqlCommand
                                            ("INSERT INTO PerfilmoduloAccion (IdPerfilModulo, Accion, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4})",
                                            gettingIdPerfilModulo.IdPerfilModulo, itemActions, DateTime.Now, perfilM.CURRENT_USER, "D4U");
                                    }
                                }
                            }
                            //Fin de la actualizacion mediante borrado-insercion

                            result.Correct = true;
                            context.SaveChanges();
                            transaction.Commit();
                        }



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

        public static ML.Result Delete(int IdPerfilModulo)
        {
            ML.Result result = new ML.Result();

            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                context.Database.Log = Console.Write;

                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var query = context.Database.ExecuteSqlCommand("DELETE FROM PerfilModulo WHERE IdPerfilModulo = {0}", IdPerfilModulo);

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
        }//Delete by IdPerfilModulo

        public static ML.Result DeleteByIdPerfil(int IdPerfil)
        {
            ML.Result result = new ML.Result();

            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                context.Database.Log = Console.Write;

                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var query = context.Database.ExecuteSqlCommand("DELETE FROM PerfilModulo WHERE PerfilModulo.IdPerfil = {0}", IdPerfil);

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
        }//Delete by Perfil

        public static ML.Result DeleteByIdAdministrador(int IdAdministrador)
        {
            ML.Result result = new ML.Result();

            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                context.Database.Log = Console.Write;

                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var query = context.Database.ExecuteSqlCommand("DELETE FROM perfilM from PerfilModulo perfilM INNER JOIN PerfilD4U perf ON perfilM.IdPerfil = perf.IdPerfil INNER JOIN Administrador ADM ON perf.IdPerfil = ADM.IdPerfil WHERE ADM.IdAdministrador = {0}", IdAdministrador);

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
        }//Delete by Administrador
    }
}
