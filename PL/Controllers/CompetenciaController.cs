using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class CompetenciaController : Controller
    {
        // GET: Competencia
        public ActionResult GetAll()
        {
            return View();
        }
        public ActionResult GetAll_()
        {
            return View("GetAll_Categoria");
        }

        // Administracion de competencias
        public ActionResult GetByAdmin(string aIdUsuarioCreacion)
        {
            return Json(BL.Competencia.GetByAdmin(aIdUsuarioCreacion), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Add(List<ML.Competencia> aListCompetencias, string aUsuarioCreacion, string aIdUsuarioCreacion)
        {
            if (aListCompetencias == null || aListCompetencias.Count == 0)
                return Json("No hay competencias para guardar", JsonRequestBehavior.AllowGet);
            else
                return Json(BL.Competencia.Add(aListCompetencias, aUsuarioCreacion, aIdUsuarioCreacion), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Update(List<ML.Competencia> aListCompetencias, string aUsuarioCreacion, string aIdUsuarioCreacion)
        {
            if (aListCompetencias == null || aListCompetencias.Count == 0)
                return Json("No hay competencias para editar", JsonRequestBehavior.AllowGet);
            else
                return Json(BL.Competencia.Update(aListCompetencias, aUsuarioCreacion), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(ML.Competencia aCompetencia, string aUsuarioModificacion)
        {
            return Json(BL.Competencia.Delete(aCompetencia, aUsuarioModificacion), JsonRequestBehavior.AllowGet);
        }
        // Administracion de categorias
        public ActionResult GetCategoriaByAdmin(string aIdUsuarioCreacion)
        {
            return Json(BL.Competencia.GetCategoriaByAdmin(aIdUsuarioCreacion), JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddCategoria(List<ML.Categoria> aListCategoria, string aUsuarioCreacion, string aIdUsuarioCreacion)
        {
            if (aListCategoria == null || aListCategoria.Count == 0)
                return Json("No hay categorias para guardar", JsonRequestBehavior.AllowGet);
            else
                return Json(BL.Competencia.AddCategoria(aListCategoria, aUsuarioCreacion, aIdUsuarioCreacion), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateCategoria(List<ML.Categoria> aListCategoria, string aUsuarioCreacion, string aIdUsuarioCreacion)
        {
            if (aListCategoria == null || aListCategoria.Count == 0)
                return Json("No hay competencias para editar", JsonRequestBehavior.AllowGet);
            else
                return Json(BL.Competencia.UpdateCategoria(aListCategoria, aUsuarioCreacion), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteCategoria(string aIdCategoria, string aUsuarioModificacion)
        {
            return Json(BL.Competencia.DeleteCategoria(aIdCategoria, aUsuarioModificacion), JsonRequestBehavior.AllowGet);
        }
    }
}