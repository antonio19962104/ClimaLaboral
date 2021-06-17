using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
namespace PL.Controllers
{
    public class LoginAdminController : Controller
    {
        // GET: LoginAdmin
        public ActionResult Login()
        {
            Session["resultLogin"] = 0;
            Session["AdminLog"] = null;
            Session["PerfilAdminLog"] = null;
            Session["Permisos"] = null;
            Session["Modulos"] = null;

            return View();
        }
        public ActionResult AutenticarAdmin(ML.Administrador admin)
        {
            string HtmlCodeAlertProgress = "";
            HttpRuntime.Cache.Add("statusReporteFinal", HtmlCodeAlertProgress, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(8),
                System.Web.Caching.CacheItemPriority.High, null);
            Session["statusReporte"] = "";
            Session["EmailCompleted"] = 0;
            Session["EstatusEmails"] = 0;
            var result = BL.Administrador.AutenticarAdmin(admin);

            Session["CompanyDelAdminLog"] = result.CompanyDelAdmin;
            Session["IdAdministradorLogeado"] = result.CURRENTIDADMINLOG;

            Session["SuperAdmin"] = result.IsSuperAdmin;

            try
            {
                if (result.PerfilesList != null || result.PerfilesList.Count != 0)
                {
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                return Json("error");
            }

            if (result.PerfilesList != null || result.PerfilesList.Count != 0)
            {
                foreach (string item in result.PerfilesList.ToList())
                {
                    if (item == "Administrador Master")
                    {
                        Session["Master"] = true;
                    }
                    else
                    {
                        Session["Master"] = false;
                    }
                }
                /****************GetCompaniesForPermission*****************************/
                Session["CurrentIdAdminLog"] = result.CURRENTIDADMINLOG;
                var getCompanies = BL.Administrador.GetCompaniesForPermisos(result.CURRENTIDADMINLOG);


                ViewBag.CompaniesPermisos = getCompanies.Objects;
                Session["CompaniesPermisos"] = getCompanies.Objects;
                /********************End GetCompanies***************************/


                if (result.Correct == true)
                {
                    //result contiene las acciones que se pueden realizar por el usuario
                    string Nombre = ((ML.PerfilModulo)result.Object).Administrador.Empleado.Nombre;
                    string ApellidoP = ((ML.PerfilModulo)result.Object).Administrador.Empleado.ApellidoPaterno;
                    string ApellidoM = ((ML.PerfilModulo)result.Object).Administrador.Empleado.ApellidoMaterno;

                    string color = ((ML.Administrador)result.DataColors).Company.Color;
                    string logo = ((ML.Administrador)result.DataColors).Company.LogoEmpresa;

                    Session["color"] = color;
                    Session["logo"] = logo;
                    //para obtener perfil sería iterar sobre un objects para obtener uno o los que salgan Objects.Aux
                    //string PerfilD4U = ((ML.PerfilModulo)result.Object).PerfilD4U.Descripcion;

                    Session["IdEmpleadoLog"] = result.CURRENT_IDEMPLEADOLOG;
                    Session["AdminLog"] = Nombre + " " + ApellidoP + " " + ApellidoM;
                    Session["PerfilAdminLog"] = result.PerfilesList;
                    Session["Permisos"] = result.ObjectsPermisos;
                    Session["Modulos"] = result.ObjectsAux;
                    BL.NLogGeneratorFile.logAccesCMS(admin.UserName, Nombre + " " + ApellidoP + " " + ApellidoM);
                    return Json("success");
                }
                else
                {
                    ViewBag.ErrorMessage = "Claves de acceso no válidas";
                    Session["resultLogin"] = 1;
                    return Json("error");
                }

            }
            else
            {
                return Json("error");
            }

            

            
        }

        public ActionResult IsLogged()
        {
            //logError => ex.message
            //statusTarea => Task.status
            Session["getErrorMessage"] = Convert.ToString(HttpRuntime.Cache.Get("logError"));
            var sessionEmailStatus = Convert.ToString(Session["EstatusEmails"]);//1 success: 0 nada: 2 error
            string session = Convert.ToString(Session["AdminLog"]);
            string statusCache = Convert.ToString(HttpRuntime.Cache.Get("status"));

            //Validar quien ve la notificacion
            string UsuarioEnvíaEncuesta = Convert.ToString(Session["UsuarioEnvia"]);
            string usuarioActual = Convert.ToString(Session["AdminLog"]);

            if (UsuarioEnvíaEncuesta != "")
            {
                Console.Write("");
            }

            if (session == null)
            {
                return Json("error");//Session caducada
            }
            else if (session == "")
            {
                return Json("error");//Session caducada
            }
            if (session != null && session != "" && statusCache == "")
            {
                return Json("success");//Session activa
            }
            else if(session != null && session != "" && statusCache == "1" && UsuarioEnvíaEncuesta == usuarioActual)
            {
                return Json("successAndEmails");
            }
            else if (session != null && session != "" && statusCache == "2" && UsuarioEnvíaEncuesta == usuarioActual)
            {
                return Json("errorEmail");
            }
            return Json("");
        }//

        public ActionResult validate()
        {
            return RedirectToAction("Login", "LoginAdmin");
        }


        [ValidateInput(false)]
        public ActionResult getHtml(ML.Encuesta encuesta)
        {
            Console.Write(encuesta.Descripcion);

            //encuesta.Descripcion.Replace('\\', ' ');

            return Json(encuesta.Descripcion, JsonRequestBehavior.AllowGet);
        }

        
    }
}