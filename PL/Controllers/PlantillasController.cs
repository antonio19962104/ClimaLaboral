using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PL.Models;

namespace PL.Controllers
{
    public class PlantillasController : Controller
    {       
        // GET: Plantillas
        public ActionResult Plantilla()
        {
            var idempleado = Session["IdEmpleadoLog"];
            var result = BL.Plantillas.getPlantillas(Convert.ToInt32(idempleado));
            return View(result);
        }
        [HttpGet]
        public ActionResult Edit(int IdPlantilla)
        {
            string usuarioRegistra =Convert.ToString(Session["Nombre"]);
            ML.Result ListadoTipoEstatus = BL.Plantillas.getAllTipoEstatus();
            ML.Result ListadoHeaders = BL.Plantillas.getAllHeaderPlantilla();
            ML.Result ListadoDetalles = BL.Plantillas.getAllDetallePlantilla();
            ML.Result ListadoFooters = BL.Plantillas.getAllFooterPlantilla();
            ML.Result result = BL.Plantillas.getPlantillaById(IdPlantilla);
            ML.Plantillas item = result.EditaPlantillas;            
            ML.Result ListadoAling = BL.Plantillas.getAlingAll();            
            item.aling = ListadoAling.Aling;
            item.EstatusList = ListadoTipoEstatus.ListadoTipoEstatus;
            item.UsuarioModificacion = usuarioRegistra;
            item.EstatusList = ListadoTipoEstatus.ListadoTipoEstatus;
            item.HeaderList = ListadoHeaders.ListadoHeadersPlantilla;
            item.DetalleList = ListadoDetalles.ListadoDetallePlantilla;
            item.FooterList = ListadoFooters.ListadoFooterPlantilla;
            return View(item);            
        }
        public ActionResult EditaPlantilla(int IdPlantilla)
        {
            ML.Result ListadoHeaders = BL.Plantillas.getAllHeaderPlantilla();
            ML.Result ListadoDetalles = BL.Plantillas.getAllDetallePlantilla();
            ML.Result ListadoFooters = BL.Plantillas.getAllFooterPlantilla();
            ML.Result ListadoTipoPlantilla = BL.TipoPlantilla.getAllTipoPlantilla();
            ML.Result result = BL.Plantillas.getPlantillaById(IdPlantilla);
            ML.Plantillas Edita = new ML.Plantillas();
            Edita = result.EditaPlantillas;
            Edita.HeaderList = ListadoHeaders.ListadoHeadersPlantilla;
            Edita.DetalleList = ListadoDetalles.ListadoDetallePlantilla;
            Edita.FooterList = ListadoFooters.ListadoFooterPlantilla;
            Edita.TipoPlantillaList = ListadoTipoPlantilla.ListTipoPlantilla;

            return View(Edita);
        }
        public ActionResult DuplicaPlantillaPersonal(int IdPlantilla)
        {
            ML.Result result = BL.Plantillas.getPlantillaById(IdPlantilla);
            ML.Plantillas DuplicaPersonal = new ML.Plantillas();
            DuplicaPersonal = result.EditaPlantillas;
            //var contenidoBody = new ML.BodyPlantilla();
            DuplicaPersonal.Nombre = DuplicaPersonal.Nombre + "_Copy";
            //DuplicaPersonal.BodyPlantilla = contenidoBody;
            DuplicaPersonal.UsuarioCreacion = Session["IdEmpleadoLog"].ToString();

            var resultAdd = BL.Plantillas.AddPlantilla(DuplicaPersonal);
            if (resultAdd.Correct)
            {
                ViewBag.Message = "La Plantilla se Inserto correctamente";

                //return RedirectToAction("Plantilla");
                return Json("success");
            }
            else
            {
                ViewBag.Message = "La Plantilla no se ha podido Insertar";
                //return RedirectToAction("Create");
                ViewBag.ErrorMessage = result.ErrorMessage;
                return Json("error");
            }

        }
        public ActionResult DuplicaPlantilla(int IdPlantilla)
        {
            ML.Result ListadoHeaders = BL.Plantillas.getAllHeaderPlantilla();
            ML.Result ListadoDetalles = BL.Plantillas.getAllDetallePlantilla();
            ML.Result ListadoFooters = BL.Plantillas.getAllFooterPlantilla();
            ML.Result ListadoTipoPlantilla = BL.TipoPlantilla.getAllTipoPlantilla();
            ML.Result result = BL.Plantillas.getPlantillaById(IdPlantilla);
            ML.Plantillas Duplica = new ML.Plantillas();
            Duplica = result.EditaPlantillas;
            //Duplica.HeaderList = ListadoHeaders.ListadoHeadersPlantilla;
            Duplica.DetalleList = ListadoDetalles.ListadoDetallePlantilla;
            //Duplica.FooterList = ListadoFooters.ListadoFooterPlantilla;
            Duplica.TipoPlantillaList = ListadoTipoPlantilla.ListTipoPlantilla;

            return View(Duplica);

        }
        [HttpPost]       
        [ValidateInput(false)]
        public ActionResult Edit(ML.Plantillas plantilla)
        {           
           
           plantilla.UsuarioModificacion = Session["IdEmpleadoLog"].ToString();
            var result = BL.Plantillas.UpdatePlantilla(plantilla);
            if (result.Correct)
            {
                ViewBag.Message = "La Plantilla se Actualizó correctamente";
                //return RedirectToAction("Plantilla");
                return Json("success");
            }
            else {
                ViewBag.Message = "La Plantilla no se ha podido Actualizar";
                //return RedirectToAction("Plantilla");
                ViewBag.ErrorMessage = result.ErrorMessage;
                return Json("error");
            }
            //return View();
        }
       public ActionResult Create()
        {
            ML.Result ListadoTipoEstatus = BL.Plantillas.getAllTipoEstatus();
            ML.Result ListadoHeaders = BL.Plantillas.getAllHeaderPlantilla();
            ML.Result ListadoDetalles = BL.Plantillas.getAllDetallePlantilla();
            ML.Result ListadoFooters = BL.Plantillas.getAllFooterPlantilla();
            ML.Result ListadoAling = BL.Plantillas.getAlingAll();
            ML.Plantillas item = new ML.Plantillas();
            item.aling = ListadoAling.Aling;
            item.EstatusList = ListadoTipoEstatus.ListadoTipoEstatus;
            item.HeaderList = ListadoHeaders.ListadoHeadersPlantilla;
            item.DetalleList = ListadoDetalles.ListadoDetallePlantilla;
            item.FooterList = ListadoFooters.ListadoFooterPlantilla;

            return View(item);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateInput(false)]        
        public ActionResult Create(ML.Plantillas plantilla)
        {
            if(plantilla.HeaderPlantilla.IdHeaderPlantilla > 0)
            {
                ML.Result ListadoHeaders = BL.Plantillas.getAllHeaderPlantilla();
                var contenidoHeader = ListadoHeaders.ListadoHeadersPlantilla.FirstOrDefault(x => x.IdHeaderPlantilla == plantilla.HeaderPlantilla.IdHeaderPlantilla);
                if (!contenidoHeader.CodeHTML.Contains("Ninguno"))
                {
                    plantilla.HeaderPlantilla.CodeHTML = contenidoHeader.CodeHTML;                 
                    plantilla.HeaderPlantilla.IdPlantillaDefinida = plantilla.HeaderPlantilla.IdHeaderPlantilla;                   
                }
            }
            if (plantilla.DetallePlantilla.IdDetallePlantilla > 0)
            {
                var idNuevoDetallePlantilla=0;
                switch (plantilla.DetallePlantilla.IdDetallePlantilla)
                {
                    case 2:
                        idNuevoDetallePlantilla = 44;
                        break;
                    case 4:
                        idNuevoDetallePlantilla = 45;
                        break;
                    case 5:
                        idNuevoDetallePlantilla = 46;
                        break;
                    default:
                        break;
                }
                ML.Result ListadoDetalles = BL.Plantillas.getAllDetallePlantillaAll();
                var contenidoDetalle = ListadoDetalles.ListadoDetallePlantilla.FirstOrDefault(x => x.IdDetallePlantilla == idNuevoDetallePlantilla);
                if (!contenidoDetalle.CodeHTML.Contains("Ninguno")) {
                    plantilla.DetallePlantilla.CodeHTML = contenidoDetalle.CodeHTML;
                    plantilla.DetallePlantilla.IdPlantillaDefinida = plantilla.DetallePlantilla.IdDetallePlantilla;//plantilla.DetallePlantilla.IdDetallePlantilla;
                }                
            }
            if (plantilla.FooterPlantilla.IdFooterPlantilla > 0)
            {
                ML.Result ListadoFooters = BL.Plantillas.getAllFooterPlantilla();
                var contenidoFooter = ListadoFooters.ListadoFooterPlantilla.FirstOrDefault(x =>x.IdFooterPlantilla == plantilla.FooterPlantilla.IdFooterPlantilla);
                if (!contenidoFooter.CodeHTML.Contains("Ninguno")) {
                    plantilla.FooterPlantilla.CodeHTML = contenidoFooter.CodeHTML;                   
                    plantilla.FooterPlantilla.IdPlantillaDefinida = plantilla.FooterPlantilla.IdFooterPlantilla;
                }
            }
            //var contenidoBody = new ML.BodyPlantilla();
            //plantilla.BodyPlantilla = contenidoBody;
            plantilla.UsuarioCreacion = Session["IdEmpleadoLog"].ToString();

            var result = BL.Plantillas.AddPlantilla(plantilla);
            if (result.Correct)
            {
                ViewBag.Message = "La Plantilla se Inserto correctamente";

                //return RedirectToAction("Plantilla");
                return Json("success");
            }
            else
            {
                ViewBag.Message = "La Plantilla no se ha podido Insertar";
                //return RedirectToAction("Create");
                ViewBag.ErrorMessage = result.ErrorMessage;
                return Json("error");
            }

        }
        public ActionResult Details()
        {           
            return View();
        }
        public ActionResult Delete(int idPlantilla)
        {
            var result = BL.Plantillas.DeletePlantilla(idPlantilla);
            if (result.Correct)
            {
                ViewBag.Message = "La Plantilla se ha eliminado";
                return RedirectToAction("Plantilla");
            }
            else
            {
                ViewBag.Message = "No se ha podido eliminar la Plantilla";
                return RedirectToAction("Plantilla");
            }     
            //return View();
        }
        public ActionResult DeletePlantilla(ML.Plantillas plantilla)
        {
            int idPlantilla = Convert.ToInt32(plantilla.IdPlantilla);
            var result = BL.Plantillas.DeletePlantilla(idPlantilla);
            if (result.Correct)
            {
                return Json("success");
            }
            else
            {
                return Json("error");
            }
            //return View();
        }
        public ActionResult Preview(int idPlantilla)
        {
            var result = BL.Plantillas.getPViewPlantillaById(idPlantilla);
            return View(result);
        }
        public ActionResult Plantillas()
        {
            var idempleado = Session["IdEmpleadoLog"];
            var result = BL.Plantillas.getPlantillas(Convert.ToInt32(idempleado));
            return View(result);
        }
        public ActionResult creaPlantilla()
        {
            ML.Result ListadoTipoEstatus = BL.Plantillas.getAllTipoEstatus();
            ML.Result ListadoHeaders = BL.Plantillas.getAllHeaderPlantilla();
            ML.Result ListadoDetalles = BL.Plantillas.getAllDetallePlantilla();
            ML.Result ListadoFooters = BL.Plantillas.getAllFooterPlantilla();
            ML.Result ListadoAling = BL.Plantillas.getAlingAll();
            ML.Result ListadoTipoPlantilla = BL.TipoPlantilla.getAllTipoPlantilla();
            ML.Plantillas item = new ML.Plantillas();
            item.aling = ListadoAling.Aling;
            item.EstatusList = ListadoTipoEstatus.ListadoTipoEstatus;
            item.HeaderList = ListadoHeaders.ListadoHeadersPlantilla;
            item.DetalleList = ListadoDetalles.ListadoDetallePlantilla;
            item.FooterList = ListadoFooters.ListadoFooterPlantilla;
            item.TipoPlantillaList = ListadoTipoPlantilla.ListTipoPlantilla;

            return View(item);
        }
    }
}