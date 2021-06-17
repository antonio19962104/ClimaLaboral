using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult GetLogin()
        {
            var res = BL.Encuesta.Demo();
            return View("Login");
        }
    }
}