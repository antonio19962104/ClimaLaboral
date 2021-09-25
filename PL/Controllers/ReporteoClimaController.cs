﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class ReporteoClimaController : Controller
    {
        BackGroundJobController BackGroundJobController = new BackGroundJobController();
        ApisController apis = new ApisController();
        // GET: ReporteoClima
        public ActionResult configurar()
        {
            return View("ConfigirarReporte");
        }
        public ActionResult Portada()
        {
            return View("~/Views/ReporteoClima/EstructuraReporte/no1_Portada.cshtml");
        }
        public ActionResult Index(string token = "")
        {
            if (!string.IsNullOrEmpty(token))
            {
                //token = "576B6E58355245594E5750564957504E315977465A716932646263463331344C67434974635268523261764E486E4D414D6B50303531526E777557484A744A4C61476D784C652B7268316E423051456C39474B4539413D3D";
                //tomar claves del token
                LoginAdminController loginAdminController = new LoginAdminController();
                BL.Seguridad seguridad = new BL.Seguridad();
                var credenciales = seguridad.DesencriptarCadena(token);
                ML.Administrador administrador = new ML.Administrador();
                administrador.UserName = credenciales.Split('|')[0];
                administrador.Password = credenciales.Split('|')[1];
                var result = loginAdminController.AutenticarAdmin_(administrador, this.Session);
                if (result.Data.ToString() == "success")
                {
                    return View("~/Views/ReporteoClima/IndexReporte.cshtml", new ML.Result() { Correct = true });
                }
                else
                {
                    return View("~/Views/ReporteoClima/IndexReporte.cshtml", new ML.Result() { Correct = false });
                }
            }
            return View("~/Views/ReporteoClima/IndexReporte.cshtml", new ML.Result() { Correct = true });
        }
        public JsonResult GetFiltrosR(string cadena)
        {
            BL.Seguridad seguridad = new BL.Seguridad();
            return Json(seguridad.DesencriptarCadena(cadena), JsonRequestBehavior.AllowGet);
        }
        public ActionResult CargarHistorico()
        {
            return View("~/Views/ReporteoClima/CargarHistorico/CargarHistorico.cshtml");
        }
        public JsonResult cronCrearReporte()
        {
            return Json(true);
        }
        public ActionResult ReporteComentarios3d()
        {
            return View(new ML.Encuesta());
        }
        public ActionResult ReporteDinamico()
        {
            return View(new ML.Encuesta());
        }

        public JsonResult ReporteDinamicoEE(BL.ReporteD4U.modelRep modelRep)
        {
            var data = new List<object>();
            foreach (var item in modelRep.listUnudadesNeg)
            {
                var aFiltrosHijosEstructura = BackGroundJobController.getEstructuraFromExcelRDinamico(1, modelRep.idBD, item, modelRep.niveles);
                var objComparativoResultadoGeneralPorNivelesEE = apis.getComparativoResultadoGeneralPorNivelesEE("1", item, item, aFiltrosHijosEstructura, modelRep.anio, modelRep.idBD);//ok
                data.Add(objComparativoResultadoGeneralPorNivelesEE.Data);
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ReporteDinamicoEA(BL.ReporteD4U.modelRep modelRep)
        {
            var data = new List<object>();
            foreach (var item in modelRep.listUnudadesNeg)
            {
                var aFiltrosHijosEstructura = BackGroundJobController.getEstructuraFromExcelRDinamico(1, modelRep.idBD, item, modelRep.niveles);
                var objComparativoResultadoGeneralPorNivelesEA = apis.getComparativoResultadoGeneralPorNivelesEA("1", item, item, aFiltrosHijosEstructura, modelRep.anio, modelRep.idBD);//ok
                data.Add(objComparativoResultadoGeneralPorNivelesEA.Data);
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public static string getFiltro(string f)
        {
            switch (f)
            {
                case "company":
                    return "Comp=>";
                case "area":
                    return "area=>";
                case "departamento":
                    return "Dpto=>";
                case "subdepartamento":
                    return "SubD=>";
                default:
                    return "";
            }
        }
        public JsonResult SaveImage(ML.Result result)
        {
            return Json(CrearImagenEnDirectorio(result.tableBody, result.ParteGuardada, result.CURRENT_USER));
        }

        [HttpPost]
        public JsonResult SavePDF(FormCollection result)
        {
            HttpPostedFileBase file = Request.Files["mypdf"];
            return Json(result);
        }

        public static bool CrearImagenEnDirectorio(string cadenaBase64, string seccion, string usr)
        {
            try
            {
                string base64 = getValidB64(cadenaBase64);
                string ext = getExt(cadenaBase64);
                var nombreImagen = "Imagen_" + seccion + "_reporte." + ext;
                var ruta = @"\\10.5.2.101\RHDiagnostics\media\";
                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);
                if (System.IO.File.Exists(ruta + nombreImagen))
                    System.IO.File.Delete(ruta + nombreImagen);
                byte[] bytes = Convert.FromBase64String(base64);
                System.IO.File.WriteAllBytes(ruta + nombreImagen, bytes);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public static string getValidB64(string cadena)
        {
            string b64 = ""; string ext = "";
            if (cadena.Contains("jpg;"))
            {
                b64 = cadena.Remove(0, 22);
                ext = "jpg";
            }
            if (cadena.Contains("jpeg;"))
            {
                b64 = cadena.Remove(0, 23);
                ext = "jpeg";
            }
            if (cadena.Contains("png;"))
            {
                b64 = cadena.Remove(0, 22);
                ext = "png";
            }

            return b64;
        }

        public static string getExt(string cadena)
        {
            string b64 = ""; string ext = "";
            if (cadena.Contains("jpg;"))
            {
                b64 = cadena.Remove(0, 22);
                ext = "jpg";
            }
            if (cadena.Contains("jpeg;"))
            {
                b64 = cadena.Remove(0, 23);
                ext = "jpeg";
            }
            if (cadena.Contains("png;"))
            {
                b64 = cadena.Remove(0, 22);
                ext = "png";
            }

            return ext;
        }
    }
}