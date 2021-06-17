using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace PL.Controllers
{
    public class AdministradorController : Controller
    {
        public int GetIdFaltante()
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            int IdEmpleadoForInsert = 0;
            int NumeroInicial = 0;
            int NumeroFinal = 0;
            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                NumeroInicial = context.Empleado.Min(p => p.IdEmpleado);
                NumeroFinal = context.Empleado.Max(p => p.IdEmpleado);

            }
            string query = "WITH Secuencia AS ( SELECT " + NumeroInicial + " AS [Numero] union all SELECT Numero + 1 FROM Secuencia WHERE Numero < " + NumeroFinal + " ) SELECT top 1 s.Numero FROM Secuencia s WHERE NOT EXISTS (SELECT 1 FROM Empleado WHERE (IdEmpleado = s.Numero));";

            using (SqlConnection connection = new SqlConnection("Data Source=192.192.192.97;Initial Catalog=RH_Des;User ID=sa;Password=Pa$$w0rd01;"))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter dat_1 = new System.Data.SqlClient.SqlDataAdapter(query, connection);

                    dat_1.Fill(ds, "dat_1");
                    connection.Close();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            IdEmpleadoForInsert = Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0]);

            return IdEmpleadoForInsert;
        }
        public ActionResult GetAll()
        {
            int isSuperAdmin = Convert.ToInt32(Session["SuperAdmin"]);

            var CURRENT_IDEMPLEADO_LOG = Convert.ToInt32(Session["IdEmpleadoLog"]);
            //Administrador Log
            int CURRENTADMINLOG = Convert.ToInt32(Session["CurrentIdAdminLog"]);

            bool IsMaster = false;
            //Si es un master puede ver a master, si no es master a ellos no los puede ver
            ViewBag.perfilesAdmin = (Session["PerfilAdminLog"]);

            foreach (var item in ViewBag.perfilesAdmin)
            {
                if (item == "Administrador Master")
                {
                    IsMaster = true;
                }
                else {
                    IsMaster = false;
                }
            }
            Session["errorEmail"] = 0;
            if (IsMaster == true && isSuperAdmin == 1)
            {
                Session["confirm"] = 0;
                return View(BL.Administrador.ObtenTodos());
            }
            else
            if (IsMaster == true && isSuperAdmin == 0)
            {
                Session["confirm"] = 0;
                //CURRENTADMINLOG
                return View(BL.Administrador.GetAll(CURRENT_IDEMPLEADO_LOG, CURRENTADMINLOG, isSuperAdmin));
            }
            else if (IsMaster == false && isSuperAdmin == 0)
            {
                Session["confirm"] = 0;
                //CURRENTADMINLOG
                return View(BL.Administrador.GetAllExceptMaster(CURRENT_IDEMPLEADO_LOG, CURRENTADMINLOG, isSuperAdmin));
            }
            else if (IsMaster == false && isSuperAdmin == 1)
            {
                Session["confirm"] = 0;
                return View(BL.Administrador.ObtenTodos());
            }
            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {
            Session["IsMaster"] = 0;
            ML.Administrador admin = new ML.Administrador();
            bool Master = Convert.ToBoolean(Session["Master"]);

            if (Master == true)
            {
                //ñista perfiles
                var listPerfilesD4U = BL.PerfilD4U.GetAll();
                admin.ListPerfilD4U = listPerfilesD4U.Objects;
            }
            else if (Master == false)
            {
                //ñista perfiles
                var listPerfilesD4U = BL.PerfilD4U.GetAll();
                admin.ListPerfilD4U = listPerfilesD4U.Objects;
                admin.ListPerfilD4U.RemoveAt(0);
            }
            

            //Enviar lista de unidad de negocio
            var listCategoria = BL.CompanyCategoria.GetAll();
            admin.listUNegocio = new List<object>();
            admin.listUNegocio = listCategoria.Objects;

            admin.AdminSA = Convert.ToInt32(Session["SuperAdmin"]);

            return View(admin);
        }

        [HttpGet]

        public ActionResult AddWithNameAdmin(ML.Empleado emp)
        {
            Session["IsMaster"] = 0;
            int IdEmpleadoForNewAdmin = emp.IdEmpleado;
            var dataEmpleado = BL.Empleado.GetNombreByIdEmpleado(IdEmpleadoForNewAdmin);

            string nombreEm = ((ML.Empleado)dataEmpleado.Object).Nombre;
            string ApellPat = ((ML.Empleado)dataEmpleado.Object).ApellidoPaterno;
            string ApellMat = ((ML.Empleado)dataEmpleado.Object).ApellidoMaterno;

            string FullName = nombreEm + " " + ApellPat + " " + ApellMat;

            //Solo recibo idempleado el nombre lo obtengo del id
            ML.Administrador admin = new ML.Administrador();
            //Obtener si es un master
            var ismaster = BL.Administrador.IsMaster(IdEmpleadoForNewAdmin);

            if (ismaster.IsMaster == true)
            {
                admin.ListPerfilD4U = new List<object>();
                Session["IsMaster"] = 1;
                return View("Add", admin);
            }
            else
            {
                //OBTENER SI EXXTE
                var existe = BL.PerfilD4U.ExisteAdmin(IdEmpleadoForNewAdmin);
                if (existe.Exist == false)
                {
                    var listPerfilesD4U = BL.PerfilD4U.GetAll();
                    admin.ListPerfilD4U = listPerfilesD4U.Objects;
                }
                else
                {
                    var listPerfilesD4U = BL.PerfilD4U.GetByFiltrado(IdEmpleadoForNewAdmin);
                    admin.ListPerfilD4U = listPerfilesD4U.Objects;
                }

            }

            Session["IdEmpleadoForNewAdmin"] = IdEmpleadoForNewAdmin;
            ViewBag.NewAdmin = FullName;
            return View("Add", admin);
        }

        public ActionResult Redireccionar(ML.Administrador admin)
        {
            return View("Add", admin);
        }

        [HttpPost]
        public ActionResult Add(ML.Administrador administrador)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine("soy valido");
            }
            else
            {
                Console.WriteLine("NO soy valido");
            }
            administrador.CURRENT_USER = Convert.ToString(Session["AdminLog"]);
            //Save Empleado from ML.Administrador
            var resultInsertEmpleado = BL.Empleado.AddForAdmin(administrador);
            
            //Guaradado
            administrador.Empleado = new ML.Empleado();
            administrador.Empleado.IdEmpleado = resultInsertEmpleado.IdEmpleadoFromSP;

            //Hacer consulta de email de la persoma usando 
            //administrador.Empleado.IdEmpleado
            var getMail = BL.Empleado.GetById(administrador.Empleado.IdEmpleado);
            administrador.UserName = ((ML.Empleado)getMail.Object).Correo;




            int IDADMINISTRADORCREATE = Convert.ToInt32(Session["CurrentIdAdminLog"]);
            var result = BL.Administrador.Add(administrador, IDADMINISTRADORCREATE);
            //INSERT ON USUARIOCOMPANY

            if (administrador.AdminSA == 0)
            {
                foreach (var item in administrador.listaCompaniesForPermission)
                {
                    var getIdCompany = BL.Administrador.GetCompanyId(item);
                    foreach (ML.Company items in getIdCompany.Objects)
                    {
                        ML.Company companyItems = new ML.Company();
                        companyItems.CompanyId = items.CompanyId;
                        companyItems.CompanyName = items.CompanyName;
                        int companyIdForInsert = Convert.ToInt32(companyItems.CompanyId);
                        var query = BL.Administrador.AddusuarioCompany(result.UltimoAdminInsertado, companyIdForInsert, administrador.CURRENT_USER);
                    }
                }
            }
            else if(administrador.AdminSA == 1)
            {
                var query = BL.Administrador.PermisoToAllEnterprise(result.UltimoAdminInsertado, administrador.CURRENT_USER);
            }


            if (administrador.AdminSA == 0)
            {
                //Update sa = 0
                BL.Administrador.UpdateSA(result.UltimoAdminInsertado ,0);
            }
            else if (administrador.AdminSA == 1)
            {
                //Update sa = 1
                BL.Administrador.UpdateSA(result.UltimoAdminInsertado ,1);
            }
            
            //End insert usuariocompany

            Session["Acciones"] = administrador.Acciones;
            ViewBag.Acciones = administrador.Acciones;
            //Dependiendo del perfil de la personase guarda PerfilModulo(IdPerfil, IdModulo, IdAdministrador, BITACORA)
            //Aqui traigo el id del admin insertado => result.UltimoAdminInsertado;
            //administrador.PerfilD4U.IdPerfil trae el perfil


            if (administrador.PerfilD4U.IdPerfil == 1 || administrador.PerfilD4U.IdPerfil == 8)//Insertar en todas porque es Master o bien es Admin SA
            {
                BL.PerfilModulo.Add(administrador.PerfilD4U.IdPerfil, 1, result.UltimoAdminInsertado, administrador);
                BL.PerfilModulo.Add(administrador.PerfilD4U.IdPerfil, 2, result.UltimoAdminInsertado, administrador);
                BL.PerfilModulo.Add(administrador.PerfilD4U.IdPerfil, 3, result.UltimoAdminInsertado, administrador);
                BL.PerfilModulo.Add(administrador.PerfilD4U.IdPerfil, 4, result.UltimoAdminInsertado, administrador);
                BL.PerfilModulo.Add(administrador.PerfilD4U.IdPerfil, 5, result.UltimoAdminInsertado, administrador);
                BL.PerfilModulo.Add(administrador.PerfilD4U.IdPerfil, 6, result.UltimoAdminInsertado, administrador);
            }








            if (administrador.PerfilD4U.IdPerfil == 2)
            {
                BL.PerfilModulo.Add(administrador.PerfilD4U.IdPerfil, 3, result.UltimoAdminInsertado, administrador);
            }
            if (administrador.PerfilD4U.IdPerfil == 3)
            {
                BL.PerfilModulo.Add(administrador.PerfilD4U.IdPerfil, 2, result.UltimoAdminInsertado, administrador);
            }
            if (administrador.PerfilD4U.IdPerfil == 4)
            {
                BL.PerfilModulo.Add(administrador.PerfilD4U.IdPerfil, 1, result.UltimoAdminInsertado, administrador);
            }
            if (administrador.PerfilD4U.IdPerfil == 5)
            {
                BL.PerfilModulo.Add(administrador.PerfilD4U.IdPerfil, 4, result.UltimoAdminInsertado, administrador);
            }
            if (administrador.PerfilD4U.IdPerfil == 6)
            {
                BL.PerfilModulo.Add(administrador.PerfilD4U.IdPerfil, 5, result.UltimoAdminInsertado, administrador);
            }
            if (administrador.PerfilD4U.IdPerfil == 7)
            {
                BL.PerfilModulo.Add(administrador.PerfilD4U.IdPerfil, 6, result.UltimoAdminInsertado, administrador);
            }

            //Hacer envio de email
           // SendEmail();
           
            //Fin del envio
            Session["Acciones"] = null;
            ViewBag.Acciones = null;
            if (result.Correct == false)
            {
                ViewBag.ErrorMessage = result.ErrorMessage;
                return Json("error");
            }
            else
            {
                //string nombreNewAdmin = Convert.ToString(Session["NombreNewAdmin"]);
                string perfil = "";
                if (administrador.PerfilD4U.IdPerfil == 1)
                {
                    perfil = "Administrador Master";
                }
                if (administrador.PerfilD4U.IdPerfil == 2)
                {
                    perfil = "Administrador de Reportes";
                }
                if (administrador.PerfilD4U.IdPerfil == 3)
                {
                    perfil = "Administrador de Plantillas";
                }
                if (administrador.PerfilD4U.IdPerfil == 4)
                {
                    perfil = "Administrador de Encuestas";
                }
                if (administrador.PerfilD4U.IdPerfil == 5)
                {
                    perfil = "Administrador de Base de Datos";
                }
                if (administrador.PerfilD4U.IdPerfil == 6)
                {
                    perfil = "Administrador de Usuarios";
                }
                if (administrador.PerfilD4U.IdPerfil == 7)
                {
                    perfil = "Administrador de Empresas";
                }
                if (administrador.PerfilD4U.IdPerfil == 8)
                {
                    perfil = "Super Administrador";
                }

                var getDatosNewAdmin = BL.Empleado.GetById(administrador.Empleado.IdEmpleado);
                administrador.Empleado = new ML.Empleado();
                administrador.Empleado.Nombre = ((ML.Empleado)getDatosNewAdmin.Object).Nombre;
                administrador.Empleado.ApellidoPaterno = ((ML.Empleado)getDatosNewAdmin.Object).ApellidoPaterno;
                administrador.Empleado.ApellidoMaterno = ((ML.Empleado)getDatosNewAdmin.Object).ApellidoMaterno;
                administrador.Empleado.Correo = ((ML.Empleado)getDatosNewAdmin.Object).Correo;

                string nombreNewAdmin = administrador.Empleado.Nombre + " " + administrador.Empleado.ApellidoPaterno + " " + administrador.Empleado.ApellidoMaterno;

                SendEmail(nombreNewAdmin, perfil, administrador.UserName, result.DefPass);
                return Json("success");
            }
        }

        //[AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendEmail(string nombreNewAdmin, string perfil, string UserName, string Password)
        {
            //var result = BL.Email.SendEmail(nombreNewAdmin, perfil, UserName, Password);
            ML.Result result = new ML.Result();
            var fullUrl = this.Url.Action("SendEmail", "Administrador", new {  }, this.Request.Url.Scheme);
            string urlFinal = fullUrl.Substring(7, 4);

            string URLForEmail = "";

            URLForEmail = "http://diagnostic4u.com/LoginAdmin/Login";
            

            var body =
            "<p style='font-weight:bold;'>Que tal " + nombreNewAdmin + "</p>" +
            "<p>Has sido dado de alta dentro del portal de administración de encuestas <b>Diagnostic4U</b> bajo el perfil:</p>" +
            "<ul>   <li>" + perfil + "</li>    </ul>" +
            "<p>Tus claves de acceso son las siguientes: </p>" +
            "<p><b>Nombre de usuario: </b>" + UserName + "</p>" +
            "<p><b>Password: </b>" + Password + "</p></br>" +
            "<p>Accede entrando a: <a href='" +URLForEmail + "'><b>Diagnostic4U</b></a></p>" +
            "<p><img src='http://diagnostic4u.com/img/logo.png'></p></ br>" +
            "<small>Si ya cuenta con un perfil anterior a este tenga en cuenta que las claves de acceso son las mismas.</small>";
            var message = new MailMessage();
            message.To.Add(new MailAddress(UserName));
            //message.From = new MailAddress("jamurillo@grupoautofin.com");
            message.Subject = "Bienvenida a Diagnostic4U";
            message.Body = string.Format(body, "DIAGNOSTIC4U", "jamurillo@grupoautofin.com", "Aqui se envian  las claves de acceso al portal");
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                try
                {
                    smtp.Send(message);
                    result.Correct = true;
                }
                catch (SmtpException e)
                {
                    result.ErrorMessage = e.Message;
                    Console.WriteLine("Error: {0}", e.StatusCode);
                    result.Correct = false;
                    Session["errorEmail"] = 1;
                }
                finally
                {
                    smtp.Dispose();
                }

                //Session["error"] = result.ErrorMessage + " " + result.DefPass;
                
                return RedirectToAction("GetAll", "Administrador");
                
            }
        }
        public ActionResult EmailEnviado()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetModal()
        {

            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            ViewBag.Mensaje = "";
            return PartialView("SelectEmpleado", result);
        }

        [HttpPost]
        public ActionResult SearchOpen(ML.Empleado empleado)
        {
            var result = BL.Empleado.OpenSearch(empleado);

            if (result.Objects.Count == 0)
            {
                ViewBag.Mensaje = "No se encontraron resultados que coincidan con su búsqueda";
                ViewBag.NewAdmin = "";
            }
            foreach (ML.Empleado itemEmpleado in result.Objects)
            {
                ML.Empleado EmpResult = new ML.Empleado();
                EmpResult.IdEmpleado = itemEmpleado.IdEmpleado;
                EmpResult.Correo = itemEmpleado.Correo;
                EmpResult.Nombre = itemEmpleado.Nombre;
                EmpResult.ApellidoPaterno = itemEmpleado.ApellidoPaterno;
                EmpResult.ApellidoMaterno = itemEmpleado.ApellidoMaterno;

            }
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateEstatus(ML.Administrador admin)
        {
            admin.CURRENT_USER = Convert.ToString(Session["AdminLog"]);
            var result = BL.Administrador.UpdateEstatus(admin);
            if (result.Correct == false)
            {
                ViewBag.ErrorMessage = result.ErrorMessage;
                return RedirectToAction("GetAll");
            }else
            {
                return RedirectToAction("GetAll");
            }
        }

        public ActionResult GetPermisosByIdAdmin(ML.Administrador admin)//Este objeto trae la info del empleado
        {
            Session["IdEmpleadoForUpdatePermisos"] = admin.ID_EMPLEADO_FOR_UPDATE_PERMISOS;
            var result = BL.Administrador.GetPermisosByIdAdmin(admin);

            ML.Administrador administrador = new ML.Administrador();
            administrador.Empleado = new ML.Empleado();

            administrador.Empleado.Nombre = ((ML.Administrador)result.Object).Empleado.Nombre;
            administrador.Empleado.ApellidoPaterno = ((ML.Administrador)result.Object).Empleado.ApellidoPaterno;
            administrador.Empleado.ApellidoMaterno = ((ML.Administrador)result.Object).Empleado.ApellidoMaterno;
            ViewBag.NombreAdmin = administrador.Empleado.Nombre + " " + administrador.Empleado.ApellidoPaterno + " " + administrador.Empleado.ApellidoMaterno;


            ML.PerfilModulo perfilModulo = new ML.PerfilModulo();
            perfilModulo.PerfilD4U = new ML.PerfilD4U();
            //usar try catcjh
            perfilModulo.PerfilD4U.Descripcion = ((ML.PerfilModulo)result.ObjectAux).PerfilD4U.Descripcion;
            ViewBag.PerfilD4U = perfilModulo.PerfilD4U.Descripcion;

            Session["IdAdmin"] = ((ML.Administrador)result.Object).IdAdministrador;
            //Session["IdPerfil"] = ((ML.PerfilD4U)result.ObjectAux).IdPerfil;

            foreach (ML.PerfilModulo item in result.ObjectsAux)
            {
                ML.PerfilModulo perfM = new ML.PerfilModulo();
                perfM.PerfilD4U = new ML.PerfilD4U();

                perfM.PerfilD4U.IdPerfil = item.PerfilD4U.IdPerfil;
                Session["IdPerfil"] = perfM.PerfilD4U.IdPerfil;
            }

            foreach (ML.PerfilModulo item in result.ObjectsPermisos)
            {
                ML.PerfilModulo perfilM = new ML.PerfilModulo();
                perfilM.Modulo = new ML.Modulo();

                perfilM.Modulo.IdModulo = item.Modulo.IdModulo;

                Session["ModuloId"] = perfilM.Modulo.IdModulo;
            }

            /*Llamado a BD con el detalle de los permisos(Acciones de cada Modulo)*/
            //var results = BL.Administrador.GetAllPermisos(admin);
            //Session["permisos"] = result.ObjectsPermisos;


            return View("Permisos", result);
        }


        public ActionResult EditPermisos(ML.PerfilModulo perfilModulo)
        {
            perfilModulo.CURRENT_USER = Convert.ToString(Session["AdminLog"]);
            ML.Result result = new ML.Result();

            int IdPerfil = Convert.ToInt32(Session["IdPerfil"]);
  
            if (IdPerfil != 1)
            {
                int IdAdmin = Convert.ToInt32(Session["IdAdmin"]);
                IdPerfil = Convert.ToInt32(Session["IdPerfil"]);
                int IdModulo = Convert.ToInt32(Session["ModuloId"]);

                var results_0 = BL.PerfilModulo.Update(IdPerfil, IdModulo, IdAdmin, perfilModulo);
            }
            else
            {
                int IdAdmin = Convert.ToInt32(Session["IdAdmin"]);
                //IdModulo del 1 al 6
                var results1 = BL.PerfilModulo.Update(IdPerfil, 1, IdAdmin, perfilModulo);
                var results2 = BL.PerfilModulo.Update(IdPerfil, 2, IdAdmin, perfilModulo);
                var results3 = BL.PerfilModulo.Update(IdPerfil, 3, IdAdmin, perfilModulo);
                var results4 = BL.PerfilModulo.Update(IdPerfil, 4, IdAdmin, perfilModulo);
                var results5 = BL.PerfilModulo.Update(IdPerfil, 5, IdAdmin, perfilModulo);
                var results6 = BL.PerfilModulo.Update(IdPerfil, 6, IdAdmin, perfilModulo);
            }

            var results = true;

            //LLamar a la actualizacion de la session permisos tomando IdEmpleado
            //int IdAdministrador = Convert.ToInt32(Session["IdAdmin"]);
            //var newPermisos = BL.Administrador.RefreshPermisos(IdAdministrador);
            //Session["Permisos"] = newPermisos.ObjectsPermisos;

            if (results == true)
            {
                return Json("success");
            }
            else
            {
                return Json("error");
            }
        }

      
        public ActionResult ReenviarEmail(int IdAdministrador)
        {
            //Consultar datos del admin segun su id
            var result = BL.Administrador.GetById(IdAdministrador);

            string Nombre = ((ML.Administrador)result.Object).Empleado.Nombre;
            string ApellidoPaterno = ((ML.Administrador)result.Object).Empleado.ApellidoPaterno;
            string ApellidoMaterno = ((ML.Administrador)result.Object).Empleado.ApellidoMaterno;

            string fullNombreNewAdmin = Nombre + " " + ApellidoPaterno + " " + ApellidoMaterno;

            string perfil = ((ML.Administrador)result.Object).PerfilD4U.Descripcion;
            string UserName = ((ML.Administrador)result.Object).UserName;
            string Password = ((ML.Administrador)result.Object).Password;

            SendEmail(fullNombreNewAdmin, perfil, UserName, Password);
            int existeError = Convert.ToInt32(Session["errorEmail"]);
            if (existeError == 1)
            {
                Session["confirm"] = 0;
            }
            if (existeError != 1)
            {
                Session["confirm"] = 3;
            }
            

            
            return RedirectToAction("GetAllAux", "Administrador");
            
        }
        public ActionResult EmailMasivo()
        {
            //Obtener todos los Id de administrador
            var ListaId = BL.Administrador.GetAllId();

            foreach (ML.Administrador admin in ListaId.Objects)
            {
                ReenviarEmail(admin.IdAdministrador);
            }
            Session["confirm"] = 3;
            return RedirectToAction("GetAllAux");
        }

        public ActionResult EmailMasivoFromList(ML.Administrador admin)
        {
            
            foreach (int IdAdmin in admin.listId)
            {
                ReenviarEmail(IdAdmin);
            }
            Session["confirm"] = 3;
            return RedirectToAction("GetAllAux");
        }

        public ActionResult GetAllAux()
        {
            int AdminLogIsSA = Convert.ToInt32(Session["SuperAdmin"]);
            var CURRENT_IDEMPLEADO_LOG = Convert.ToInt32(Session["IdEmpleadoLog"]);
            int CURRENTADMINLOG = Convert.ToInt32(Session["CurrentIdAdminLog"]);
            //Validacion del que se puede visualizar
            bool IsMaster = false;
            //Si es un master puede ver a master, si no es master a ellos no los puede ver
            ViewBag.perfilesAdmin = (Session["PerfilAdminLog"]);

            foreach (var item in ViewBag.perfilesAdmin)
            {
                if (item == "Administrador Master")
                {
                    IsMaster = true;
                }
                else
                {
                    IsMaster = false;
                }
            }

            if (IsMaster == true && AdminLogIsSA == 1)
            {
                Session["confirm"] = 0;
                return View("GetAll", BL.Administrador.ObtenTodos());
            }
            else
            if (IsMaster == true && AdminLogIsSA == 0)
            {
                Session["confirm"] = 0;
                //CURRENTADMINLOG
                return View("GetAll", BL.Administrador.GetAll(CURRENT_IDEMPLEADO_LOG, CURRENTADMINLOG, AdminLogIsSA));
            }
            else if (IsMaster == false && AdminLogIsSA == 0)
            {
                Session["confirm"] = 0;
                //CURRENTADMINLOG
                return View("GetAll", BL.Administrador.GetAllExceptMaster(CURRENT_IDEMPLEADO_LOG, CURRENTADMINLOG, AdminLogIsSA));
            }
            else if (IsMaster == false && AdminLogIsSA == 1)
            {
                Session["confirm"] = 0;
                return View("GetAll", BL.Administrador.ObtenTodos());
            }
            return View("GetAll");
        }

        public ActionResult DeleteUsuario(ML.Administrador admin)
        {
            admin.CURRENT_USER = Convert.ToString(Session["AdminLog"]);
            var result = BL.Administrador.DeleteAdminFromEstatus(admin);

            if (result.Correct == true)
            {
                return RedirectToAction("GetAllEliminados", "Administrador");
            }
            else
            {
                return RedirectToAction("GetAll", "Administrador");
            }
        }

        public ActionResult GetAllEliminados()
        {
            int isSuperAdmin = Convert.ToInt32(Session["SuperAdmin"]);

            var CURRENT_IDEMPLEADO_LOG = Convert.ToInt32(Session["IdEmpleadoLog"]);
            int CURRENTADMINLOG = Convert.ToInt32(Session["CurrentIdAdminLog"]);

            bool IsMaster = false;
            //Si es un master puede ver a master, si no es master a ellos no los puede ver
            ViewBag.perfilesAdmin = (Session["PerfilAdminLog"]);

            foreach (var item in ViewBag.perfilesAdmin)
            {
                if (item == "Administrador Master")
                {
                    IsMaster = true;
                }
                else
                {
                    IsMaster = false;
                }
            }
            Session["errorEmail"] = 0;

            if (IsMaster == true && isSuperAdmin == 1)
            {
                Session["confirm"] = 0;
                return View(BL.Administrador.ObtenTodosEliminados());
            }
            else
            if (IsMaster == true && isSuperAdmin == 0)
            {
                Session["confirm"] = 0;
                return View(BL.Administrador.GetAllEliminadosForMaster(CURRENT_IDEMPLEADO_LOG, CURRENTADMINLOG));
            }
            else if(IsMaster == false && isSuperAdmin == 0)
            {
                Session["confirm"] = 0;
                return View(BL.Administrador.GetAllEliminadosForNotMaster(CURRENT_IDEMPLEADO_LOG, CURRENTADMINLOG));
            }
            else if (IsMaster == false && isSuperAdmin == 1)
            {
                Session["confirm"] = 0;
                return View(BL.Administrador.ObtenTodosEliminados());
            }
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            return View(result);
        }

        public ActionResult Restaurar(ML.Administrador admin)
        {
            var result = BL.Administrador.RestaurarAdmin(admin.IdAdministrador);

            if (result.Correct == true)
            {
                return RedirectToAction("GetAll", "Administrador");
            }
            else
            {
                return RedirectToAction("GetAllEliminados", "Administrador");
            }
        }

        public JsonResult GetCompanies(string Unegocio)
        {
            var result = BL.Company.GetByFilter(Unegocio);

            return Json(result.Objects, JsonRequestBehavior.AllowGet);

            //DL.RH_DesEntities context = new DL.RH_DesEntities();

            //List<DL.Company> allSearch = context.Company.SqlQuery("SELECT * FROM COMPANY").ToList();

            //return Json(allSearch, JsonRequestBehavior.AllowGet);
        }




        //////////////////////////////////
        public JsonResult GetCompanyAjax(string nombre)
        {
            var result = BL.Administrador.GetCompaniesByName(nombre);

            List<SelectListItem> Companies = new List<SelectListItem>();

            //Companies.Add(new SelectListItem { Text = "Selecciona una opcion", Value = "0" });

            if (result.Objects != null)
            {
                foreach (ML.Company company in result.Objects)
                {
                    Companies.Add(new SelectListItem { Text = company.CompanyName.ToString(), Value = company.CompanyId.ToString() });



                }

            }

            return Json(new SelectList(Companies, "Value", "Text", JsonRequestBehavior.AllowGet));
        }

        public ActionResult GetCompaniesByUNeg(ML.Company Uneg)
        {
            var result = BL.Company.GetByUNeg(Uneg.CompanyName);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditUsername(ML.Administrador admin)
        {
            var result = BL.Administrador.EditUsername(admin);
            if (result.Correct == true)
            {
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }
    }
}