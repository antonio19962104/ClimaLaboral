using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    /// <summary>
    /// Controlador de la documentación del proyecto
    /// </summary>
    public class DocumentacionController : Controller
    {
        /// <summary>
        /// Vista de la documentación del proyecto
        /// </summary>
        /// <returns>Vista de la documentación del proyecto</returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Obtiene un objeto con la documentacion del proyecto
        /// </summary>
        /// <returns>Objeto con la documentacion del proyecto</returns>
        public JsonResult GetDocumentacion()
        {
            var list = new List<BL.Documentacion.Doc>();
            list.Add(BL.Documentacion.GetBussinessLayerDocumentation());
            list.Add(BL.Documentacion.GetDataLayerDocumentation());
            list.Add(BL.Documentacion.GetModelLayerDocumentation());
            list.Add(BL.Documentacion.GetPresentationLayerDocumentation());
            return Json(list, JsonRequestBehavior.AllowGet);
        }


    }
}