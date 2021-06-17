using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class ClimController : Controller
    {
        // GET: Clim
        public ActionResult SeccionLikert()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Preguntas.SqlQuery("select * from preguntas where idencuesta = 1").ToList();
                    foreach (var item in query)
                    {
                        ML.Preguntas preg = new ML.Preguntas();
                        preg.Pregunta = item.Pregunta;
                        result.Objects.Add(preg);
                    }
                }
            }
            catch (Exception ex)
            {
                result.Objects = new List<object>();
            }
            return View(result);
        }
    }
}