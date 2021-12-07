using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DL;

namespace PL.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class RangoController : Controller
    {
        private RH_DesEntities db = new RH_DesEntities();

        // GET: Rango
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(db.Rango.ToList());
        }

        // GET: Rango/Details/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rango rango = db.Rango.Find(id);
            if (rango == null)
            {
                return HttpNotFound();
            }
            return View(rango);
        }

        // GET: Rango/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rango/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdRango,Descripcion,Hasta,Desde,Hasta,FechaHoraCreacion,UsuarioCreacion,ProgramaCreacion,FechaHoraModificacion,UsuarioModificacion,ProgramaModificacion,FechaHoraEliminacion,UsuarioEliminacion,ProgramaEliminacion")] Rango rango)
        {
            if (ModelState.IsValid)
            {
                db.Rango.Add(rango);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rango);
        }

        // GET: Rango/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rango rango = db.Rango.Find(id);
            if (rango == null)
            {
                return HttpNotFound();
            }
            return View(rango);
        }

        // POST: Rango/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdRango,Descripcion,Desde,Hasta,FechaHoraCreacion,UsuarioCreacion,ProgramaCreacion,FechaHoraModificacion,UsuarioModificacion,ProgramaModificacion,FechaHoraEliminacion,UsuarioEliminacion,ProgramaEliminacion")] Rango rango)
        {
            if (ModelState.IsValid)
            {
                string UsuarioActual = Session["AdminLog"] == null ? "Invitado" : Session["AdminLog"].ToString();                
                rango.UsuarioModificacion = UsuarioActual;
                rango.FechaHoraModificacion = DateTime.Now;
                rango.ProgramaModificacion = "Planes de Acción / Edición de Rangos";
                db.Entry(rango).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rango);
        }

        // GET: Rango/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rango rango = db.Rango.Find(id);
            if (rango == null)
            {
                return HttpNotFound();
            }
            return View(rango);
        }

        // POST: Rango/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rango rango = db.Rango.Find(id);
            db.Rango.Remove(rango);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
