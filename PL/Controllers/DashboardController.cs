using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Dashboard()
        {
            //Nombre del administrador Logeado => Session["AdminLog"]
            //Perfiles que tiene el administrador logeado => Session["PerfilAdminLog"]
            //Permisos que tiene el administrador logeado => Session["Permisos"]
            //Modulos que tiene el administrador logeado => Session["Modulos"]
            //IdAdmin Session["IdAdministradorLogeado"]
            //Session["SuperAdmin"] => 0 no 1 si
            return View();
        }
        public ActionResult DashboardA()
        {
            //Nombre del administrador Logeado => Session["AdminLog"]
            //Perfiles que tiene el administrador logeado => Session["PerfilAdminLog"]
            //Permisos que tiene el administrador logeado => Session["Permisos"]
            //Modulos que tiene el administrador logeado => Session["Modulos"]
            HttpRuntime.Cache.Remove("logError");
            Session["logError"] = "";
            HttpRuntime.Cache.Remove("status");
            HttpRuntime.Cache.Remove("NombreBDSendEmail");
            Session["NombreBDSendEmail"] = "";
            HttpRuntime.Cache.Remove("TotalAEnviar");
            HttpRuntime.Cache.Remove("EnviadosCorrectos");

            return View("Dashboard");
        }
    }
}