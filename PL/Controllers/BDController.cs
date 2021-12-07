using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using OfficeOpenXml;
using Microsoft.Office.Interop.Excel;
using System.Data;

namespace PL.Controllers
{
    public class BDController : Controller
    {
        const string excepcionIdEmpleadoDuplicado = "Violation of PRIMARY KEY constraint 'PK__Empleado__CE6D8B9E282DF8C2'. Cannot insert duplicate key in object 'dbo.Empleado'.\r\nThe statement has been terminated.";
        const string excepcionConexion = "";
        public ActionResult GetLastEmapleado()
        {
            int json = 0;
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    int maxInsertEmpleado = context.Empleado.Max(p => p.IdEmpleado);
                    json = maxInsertEmpleado;
                }
            }
            catch (Exception)
            {
                json = 0;
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        // GET: BD
        public ActionResult GetAll()
        {
            List<object> permisosEstructura = new List<object>();
            ViewBag.Permisos = Session["CompaniesPermisos"];
            permisosEstructura = ViewBag.Permisos;
            return View(BL.BasesDeDatos.getBaseDeDatosAll());
        }
        public ActionResult GetAllForListado()
        {
            List<object> permisosEstructura = new List<object>();
            ViewBag.Permisos = Session["CompaniesPermisos"];
            permisosEstructura = ViewBag.Permisos;
            var miEmpresaOrigen = Convert.ToInt32(Session["CompanyDelAdminLog"]);
            bool SA = false;
            int IsSA = Convert.ToInt32(Session["SuperAdmin"]);
            if (IsSA == 1)
                SA = true;
            return View("GetAll", BL.BasesDeDatos.getBaseDeDatosAllForListado(permisosEstructura, miEmpresaOrigen, SA));
        }

        public JsonResult GetAllForListadoJSON()
        {
            List<object> permisosEstructura = new List<object>();
            ViewBag.Permisos = Session["CompaniesPermisos"];
            permisosEstructura = ViewBag.Permisos;
            var miEmpresaOrigen = Convert.ToInt32(Session["CompanyDelAdminLog"]);
            bool SA = false;
            int IsSA = Convert.ToInt32(Session["SuperAdmin"]);
            if (IsSA == 1)
                SA = true;
            return Json(BL.BasesDeDatos.getBaseDeDatosAllForListado(permisosEstructura, miEmpresaOrigen, SA), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllForListadoJSONRpt()
        {
            int IsSA = Convert.ToInt32(Session["SuperAdmin"]);
            int IdAdmin = Convert.ToInt32(Session["IdAdministradorLogeado"]);
            if (IsSA == 1)
                // Obtener resultadospara un SA(all BD)
                return Json(BL.BasesDeDatos.getBaseDeDatosAllForListado(new List<object>(), 0, true), JsonRequestBehavior.AllowGet);
            else
                // Obtener resultados segun permisosPDA
                return Json(BL.BasesDeDatos.GetAllForListadoJSONRpt(IdAdmin), JsonRequestBehavior.AllowGet);
        }

        public SelectList Lista()
        {
            List<SelectListItem> ListaItems = new List<SelectListItem>();
            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                ListaItems.Add(new SelectListItem() { Text = "Selecciona", Value = "0" });
                var ListEncuestas = context.Encuesta;
                foreach (var encuesta in ListEncuestas)
                {
                    SelectListItem selectListItem = new SelectListItem();
                    selectListItem.Value = encuesta.IdEncuesta.ToString();
                    selectListItem.Text = encuesta.Nombre;

                    ListaItems.Add(selectListItem);
                }
            }
            SelectList selectList = new SelectList(ListaItems, ListaItems[0]);
            return selectList;
        }

        public ActionResult GetDataFromBD(ML.BasesDeDatos BD)
        {
            int IdBaseDD = Convert.ToInt32(BD.IdBaseDeDatos);
            int IdTipoBD = Convert.ToInt32(BD.IdTipoBD);

            var result = BL.BasesDeDatos.GetDataFromIdBD(IdBaseDD, IdTipoBD);

            Session["IdBD"] = IdBaseDD;
            Session["TipoBD"] = IdTipoBD;
            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                var querySQL = context.BasesDeDatos.SqlQuery("SELECT * FROM BasesDeDatos WHERE IdBasesDeDatos = {0}", BD.IdBaseDeDatos).ToList();
                foreach (var item in querySQL)
                {
                    ML.BasesDeDatos bd = new ML.BasesDeDatos();
                    bd.Nombre = item.Nombre;
                    Session["BDNombre"] = bd.Nombre;
                }
            }
            return View(result);
        }
        public ActionResult Add()
        {
            if (Session["LogError"] == null)
            {
                Session["LogError"] = 9;
            }
            ML.BasesDeDatos bd = new ML.BasesDeDatos();
            bd.listTipoEncuesta = new List<ML.TipoEncuesta>();
            var lista = BL.TipoEncuesta.getAllTipoEncuesta();
            bd.listTipoEncuesta = lista.ListadoTipoEncuesta;
            return View(bd);
        }
        [HttpPost]
        public ActionResult Add(ML.BasesDeDatos BD)
        {
            string CURRENT_USER = Convert.ToString(Session["AdminLog"]);
            int IDADMINISTRADORCREATE = Convert.ToInt32(Session["CurrentIdAdminLog"]);
            var result = BL.BasesDeDatos.Add(BD, IDADMINISTRADORCREATE, CURRENT_USER);
            if (result.Correct == true)
            {
                return Json("success");
            }
            else
            {
                return Json("error");
            }
        }
        [HttpPost]
        public ActionResult AddUsuarios(FormCollection formCollection)
        {
            //Get last IdEncuesta
            int lastInsertBD = 0;
            using (DL.RH_DesEntities contextOne = new DL.RH_DesEntities())
            {
                lastInsertBD = contextOne.BasesDeDatos.Max(p => p.IdBasesDeDatos);
            }
            var userList = new List<DL.Usuario>();
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["postedFile"];
                if ((file != null) && (file.ContentLength > 0) && (!string.IsNullOrEmpty(file.FileName)))
                {
                    string fileName = file.FileName;
                    string fileExtension = Path.GetExtension(file.FileName);
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));

                    if (fileExtension == ".xlsx")
                    {
                        using (var package = new ExcelPackage(file.InputStream))
                        {
                            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                            var currentSheet = package.Workbook.Worksheets;
                            var workSheet = currentSheet.First();
                            var noOfCol = workSheet.Dimension.End.Column;
                            var noOfRow = workSheet.Dimension.End.Row;

                            if (noOfRow > 1)
                            {
                                for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                                {
                                    int ID_ESTATUS;
                                    try
                                    {
                                        var status = workSheet.Cells[rowIterator, 29].Value.ToString();
                                    }
                                    catch (Exception)
                                    {
                                        workSheet.Cells[rowIterator, 29].Value = "Activo";
                                    }

                                    if ((workSheet.Cells[rowIterator, 29].Value.ToString()) == "ACTIVO" || (workSheet.Cells[rowIterator, 29].Value.ToString()) == "Activo")
                                    {
                                        string PassGenerate = "";
                                        if (workSheet.Cells[rowIterator, 5].Value != null)
                                        {
                                            PassGenerate = workSheet.Cells[rowIterator, 5].Value.ToString();
                                        }
                                        else
                                        {
                                            PassGenerate = BL.GeneratorPass.GetUniqueKey(8);
                                        }
                                        
                                        ID_ESTATUS = 1;
                                        var usuario = new DL.Usuario();
                                        usuario.Password = PassGenerate;
                                        if (workSheet.Cells[rowIterator, 2].Value != null)
                                        {
                                            usuario.Nombre = (workSheet.Cells[rowIterator, 2].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 3].Value != null)
                                        {
                                            usuario.ApellidoPaterno = (workSheet.Cells[rowIterator, 3].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 4].Value != null)
                                        {
                                            usuario.ApellidoMaterno = (workSheet.Cells[rowIterator, 4].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 6].Value != null)
                                        {
                                            usuario.Puesto = (workSheet.Cells[rowIterator, 6].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 7].Value != null)
                                        {
                                            usuario.FechaNacimiento = Convert.ToDateTime(workSheet.Cells[rowIterator, 7].Value.ToString());
                                        }
                                        else
                                        {
                                            usuario.FechaNacimiento = Convert.ToDateTime("01/01/1800");
                                        }
                                        if (workSheet.Cells[rowIterator, 8].Value != null)
                                        {
                                            usuario.FechaAntiguedad = Convert.ToDateTime(workSheet.Cells[rowIterator, 8].Value.ToString());
                                        }
                                        else
                                        {
                                            usuario.FechaAntiguedad = Convert.ToDateTime("01/01/1800");
                                        }
                                        if (workSheet.Cells[rowIterator, 9].Value != null)
                                        {
                                            usuario.Sexo = (workSheet.Cells[rowIterator, 9].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 10].Value != null)
                                        {
                                            usuario.Email = (workSheet.Cells[rowIterator, 10].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 11].Value != null)
                                        {
                                            usuario.TipoFuncion = (workSheet.Cells[rowIterator, 11].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 12].Value != null)
                                        {
                                            usuario.CondicionTrabajo = (workSheet.Cells[rowIterator, 12].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 13].Value != null)
                                        {
                                            usuario.GradoAcademico = (workSheet.Cells[rowIterator, 13].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 14].Value != null)
                                        {
                                            usuario.UnidadNegocio = (workSheet.Cells[rowIterator, 14].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 15].Value != null)
                                        {
                                            usuario.DivisionMarca = (workSheet.Cells[rowIterator, 15].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 16].Value != null)
                                        {
                                            usuario.AreaAgencia = (workSheet.Cells[rowIterator, 16].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 17].Value != null)
                                        {
                                            usuario.Departamento = (workSheet.Cells[rowIterator, 17].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 18].Value != null)
                                        {
                                            usuario.Subdepartamento = (workSheet.Cells[rowIterator, 18].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 19].Value != null)
                                        {
                                            usuario.EmpresaContratante = (workSheet.Cells[rowIterator, 19].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 20].Value != null)
                                        {
                                            usuario.IdResponsableRH = Convert.ToInt32(workSheet.Cells[rowIterator, 20].Value);
                                        }
                                        if (workSheet.Cells[rowIterator, 21].Value != null)
                                        {
                                            usuario.NombreResponsableRH = (workSheet.Cells[rowIterator, 21].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 22].Value != null)
                                        {
                                            usuario.IdJefe = Convert.ToInt32((workSheet.Cells[rowIterator, 22].Value));
                                        }
                                        if (workSheet.Cells[rowIterator, 23].Value != null)
                                        {
                                            usuario.NombreJefe = (workSheet.Cells[rowIterator, 23].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 24].Value != null)
                                        {
                                            usuario.PuestoJefe = (workSheet.Cells[rowIterator, 24].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 25].Value != null)
                                        {
                                            usuario.IdResponsableEstructura = Convert.ToInt32((workSheet.Cells[rowIterator, 25].Value));
                                        }
                                        if (workSheet.Cells[rowIterator, 26].Value != null)
                                        {
                                            usuario.NombreResponsableEstructura = (workSheet.Cells[rowIterator, 26].Value).ToString();
                                        }
                                        
                                        usuario.ClaveAcceso = PassGenerate;

                                        if (workSheet.Cells[rowIterator, 27].Value != null)
                                        {
                                            usuario.RangoAntiguedad = (workSheet.Cells[rowIterator, 27].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 28].Value != null)
                                        {
                                            usuario.RangoEdad = (workSheet.Cells[rowIterator, 28].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 30].Value != null)
                                        {
                                            usuario.CampoNumerico_1 = Convert.ToInt32(workSheet.Cells[rowIterator, 30].Value);
                                        }
                                        if (workSheet.Cells[rowIterator, 31].Value != null)
                                        {
                                            usuario.CampoDeTexto_1 = (workSheet.Cells[rowIterator, 31].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 32].Value != null)
                                        {
                                            usuario.CampoNumerico_2 = Convert.ToInt32(workSheet.Cells[rowIterator, 32].Value);
                                        }
                                        if (workSheet.Cells[rowIterator, 33].Value != null)
                                        {
                                            usuario.CampoDeTexto_2 = (workSheet.Cells[rowIterator, 33].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 34].Value != null)
                                        {
                                            usuario.CampoNumerico_3 = Convert.ToInt32(workSheet.Cells[rowIterator, 34].Value);
                                        }
                                        if ((workSheet.Cells[rowIterator, 35].Value != null))
                                        {
                                            usuario.CampoDeTexto_3 = (workSheet.Cells[rowIterator, 35].Value).ToString();
                                        }
                                        usuario.IdEstatus = ID_ESTATUS;
                                        usuario.IdBaseDeDatos = lastInsertBD;
                                        usuario.FechaHoraCreacion = DateTime.Now;
                                        usuario.UsuarioCreacion = Convert.ToString(Session["AdminLog"]);
                                        usuario.ProgramaCreacion = "DIagnostic4U";
                                        userList.Add(usuario);
                                    }
                                    else if ((workSheet.Cells[rowIterator, 29].Value.ToString()) == "INACTIVO" || (workSheet.Cells[rowIterator, 29].Value.ToString()) == "Inactivo")
                                    {
                                        string PassGenerate = "";
                                        if (workSheet.Cells[rowIterator, 5].Value != null)
                                        {
                                            PassGenerate = workSheet.Cells[rowIterator, 5].Value.ToString();
                                        }
                                        else
                                        {
                                            PassGenerate = BL.GeneratorPass.GetUniqueKey(8);
                                        }
                                        ID_ESTATUS = 2;
                                        var usuario = new DL.Usuario();
                                        usuario.Password = PassGenerate;
                                        if (workSheet.Cells[rowIterator, 2].Value != null)
                                        {
                                            usuario.Nombre = (workSheet.Cells[rowIterator, 2].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 3].Value != null)
                                        {
                                            usuario.ApellidoPaterno = (workSheet.Cells[rowIterator, 3].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 4].Value != null)
                                        {
                                            usuario.ApellidoMaterno = (workSheet.Cells[rowIterator, 4].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 6].Value != null)
                                        {
                                            usuario.Puesto = (workSheet.Cells[rowIterator, 6].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 7].Value != null)
                                        {
                                            usuario.FechaNacimiento = Convert.ToDateTime(workSheet.Cells[rowIterator, 7].Value.ToString());
                                        }
                                        else
                                        {
                                            usuario.FechaNacimiento = Convert.ToDateTime("01/01/1800");
                                        }
                                        if (workSheet.Cells[rowIterator, 8].Value != null)
                                        {
                                            usuario.FechaAntiguedad = Convert.ToDateTime(workSheet.Cells[rowIterator, 8].Value.ToString());
                                        }
                                        else
                                        {
                                            usuario.FechaAntiguedad = Convert.ToDateTime("01/01/1800");
                                        }
                                        if (workSheet.Cells[rowIterator, 9].Value != null)
                                        {
                                            usuario.Sexo = (workSheet.Cells[rowIterator, 9].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 10].Value != null)
                                        {
                                            usuario.Email = (workSheet.Cells[rowIterator, 10].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 11].Value != null)
                                        {
                                            usuario.TipoFuncion = (workSheet.Cells[rowIterator, 11].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 12].Value != null)
                                        {
                                            usuario.CondicionTrabajo = (workSheet.Cells[rowIterator, 12].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 13].Value != null)
                                        {
                                            usuario.GradoAcademico = (workSheet.Cells[rowIterator, 13].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 14].Value != null)
                                        {
                                            usuario.UnidadNegocio = (workSheet.Cells[rowIterator, 14].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 15].Value != null)
                                        {
                                            usuario.DivisionMarca = (workSheet.Cells[rowIterator, 15].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 16].Value != null)
                                        {
                                            usuario.AreaAgencia = (workSheet.Cells[rowIterator, 16].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 17].Value != null)
                                        {
                                            usuario.Departamento = (workSheet.Cells[rowIterator, 17].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 18].Value != null)
                                        {
                                            usuario.Subdepartamento = (workSheet.Cells[rowIterator, 18].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 19].Value != null)
                                        {
                                            usuario.EmpresaContratante = (workSheet.Cells[rowIterator, 19].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 20].Value != null)
                                        {
                                            usuario.IdResponsableRH = Convert.ToInt32(workSheet.Cells[rowIterator, 20].Value);
                                        }
                                        if (workSheet.Cells[rowIterator, 21].Value != null)
                                        {
                                            usuario.NombreResponsableRH = (workSheet.Cells[rowIterator, 21].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 22].Value != null)
                                        {
                                            usuario.IdJefe = Convert.ToInt32((workSheet.Cells[rowIterator, 22].Value));
                                        }
                                        if (workSheet.Cells[rowIterator, 23].Value != null)
                                        {
                                            usuario.NombreJefe = (workSheet.Cells[rowIterator, 23].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 24].Value != null)
                                        {
                                            usuario.PuestoJefe = (workSheet.Cells[rowIterator, 24].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 25].Value != null)
                                        {
                                            usuario.IdResponsableEstructura = Convert.ToInt32((workSheet.Cells[rowIterator, 25].Value));
                                        }
                                        if (workSheet.Cells[rowIterator, 26].Value != null)
                                        {
                                            usuario.NombreResponsableEstructura = (workSheet.Cells[rowIterator, 26].Value).ToString();
                                        }

                                        usuario.ClaveAcceso = PassGenerate;

                                        if (workSheet.Cells[rowIterator, 27].Value != null)
                                        {
                                            usuario.RangoAntiguedad = (workSheet.Cells[rowIterator, 27].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 28].Value != null)
                                        {
                                            usuario.RangoEdad = (workSheet.Cells[rowIterator, 28].Value).ToString();
                                        }

                                        if (workSheet.Cells[rowIterator, 30].Value != null)
                                        {
                                            usuario.CampoNumerico_1 = Convert.ToInt32(workSheet.Cells[rowIterator, 30].Value);
                                        }
                                        if (workSheet.Cells[rowIterator, 31].Value != null)
                                        {
                                            usuario.CampoDeTexto_1 = (workSheet.Cells[rowIterator, 31].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 32].Value != null)
                                        {
                                            usuario.CampoNumerico_2 = Convert.ToInt32(workSheet.Cells[rowIterator, 32].Value);
                                        }
                                        if (workSheet.Cells[rowIterator, 33].Value != null)
                                        {
                                            usuario.CampoDeTexto_2 = (workSheet.Cells[rowIterator, 33].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 34].Value != null)
                                        {
                                            usuario.CampoNumerico_3 = Convert.ToInt32(workSheet.Cells[rowIterator, 34].Value);
                                        }
                                        if ((workSheet.Cells[rowIterator, 35].Value != null))
                                        {
                                            usuario.CampoDeTexto_3 = (workSheet.Cells[rowIterator, 35].Value).ToString();
                                        }
                                        usuario.IdEstatus = ID_ESTATUS;
                                        usuario.IdBaseDeDatos = lastInsertBD;
                                        usuario.FechaHoraCreacion = DateTime.Now;
                                        usuario.UsuarioCreacion = Convert.ToString(Session["AdminLog"]);
                                        usuario.ProgramaCreacion = "DIagnostic4U";
                                        userList.Add(usuario);
                                    }
                                }
                            }
                            else
                            {
                                Session["LogError"] = 6;
                                return Json("ERROR");
                            }
                        }
                    }
                    else
                    {
                        //Formato no valido
                        Session["LogError"] = 1;
                        return Json("ERROR");
                    }
                }
                else
                {
                    //No eligio archivo
                    Session["LogError"] = 5;
                    return Json("ERROR");
                }
            }
            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {

                ML.Result result = new ML.Result();
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in userList)
                        {
                            context.Usuario.Add(item);
                        }
                        //context.BulkInsertAsync(userList, options => options.ColumnPrimaryKeyExpression = c => c.IdUsuario);
                        //foreach (var item in userList)
                        //{
                        //    context.Usuario.Add(item);
                        //    //context.BulkInsert(item);
                        //}
                        context.SaveChanges();
                        transaction.Commit();
                        result.Correct = true;
                        Session["LogError"] = 10;
                        return Json("success");
                    }
                    catch (Exception ex)
                    {
                        result.Correct = false;
                        result.ErrorMessage = ex.Message;
                        transaction.Rollback();
                        Session["LogError"] = 3;
                        return Json("ERROR");
                    }
                }
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult UpdateUsuarios(FormCollection formCollection)
        {
            //Obtener ultima BD insertada
            int lastInsertBD = Convert.ToInt32(Session["IdBD"]);

            var userList = new List<DL.Usuario>();
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["postedFile_"];
                if ((file != null) && (file.ContentLength > 0) && (!string.IsNullOrEmpty(file.FileName)))
                {
                    string fileName = file.FileName;
                    string fileExtension = Path.GetExtension(file.FileName);
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));

                    if (fileExtension == ".xlsx")
                    {
                        using (var package = new ExcelPackage(file.InputStream))
                        {
                            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                            var currentSheet = package.Workbook.Worksheets;
                            var workSheet = currentSheet.First();
                            var noOfCol = workSheet.Dimension.End.Column;
                            var noOfRow = workSheet.Dimension.End.Row;

                            if (noOfRow > 2)
                            {
                                for (int rowIterator = 3; rowIterator <= noOfRow; rowIterator++)
                                {
                                    int ID_ESTATUS;
                                    if ((workSheet.Cells[rowIterator, 31].Value.ToString()) == "Activo" || (workSheet.Cells[rowIterator, 31].Value.ToString()) == "ACTIVO")
                                    {
                                        ID_ESTATUS = 1;
                                        var usuario = new DL.Usuario();

                                        //new
                                        usuario.IdUsuario = Convert.ToInt32(workSheet.Cells[rowIterator, 2].Value);

                                        if (workSheet.Cells[rowIterator, 3].Value != null)
                                        {
                                            usuario.Password = (workSheet.Cells[rowIterator, 3].Value).ToString();
                                        }else
                                        {
                                            usuario.Password = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 4].Value != null)
                                        {
                                            usuario.Nombre = (workSheet.Cells[rowIterator, 4].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.Nombre = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 5].Value != null)
                                        {
                                            usuario.ApellidoPaterno = (workSheet.Cells[rowIterator, 5].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.ApellidoPaterno = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 6].Value != null)
                                        {
                                            usuario.ApellidoMaterno = (workSheet.Cells[rowIterator, 6].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.ApellidoMaterno = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 7].Value != null)
                                        {
                                            usuario.Puesto = (workSheet.Cells[rowIterator, 7].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.Puesto = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 8].Value != null)
                                        {
                                            usuario.FechaNacimiento = Convert.ToDateTime(workSheet.Cells[rowIterator, 8].Value.ToString());
                                        }
                                        else
                                        {
                                            usuario.FechaNacimiento = Convert.ToDateTime("01/01/1800");
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 9].Value != null)
                                        {
                                            usuario.FechaAntiguedad = Convert.ToDateTime(workSheet.Cells[rowIterator, 9].Value.ToString());
                                        }
                                        else
                                        {
                                            usuario.FechaAntiguedad = Convert.ToDateTime("01/01/1800");
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 10].Value != null)
                                        {
                                            usuario.Sexo = (workSheet.Cells[rowIterator, 10].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.Sexo = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 11].Value != null)
                                        {
                                            usuario.Email = (workSheet.Cells[rowIterator, 11].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.Email = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 12].Value != null)
                                        {
                                            usuario.TipoFuncion = (workSheet.Cells[rowIterator, 12].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.TipoFuncion = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 13].Value != null)
                                        {
                                            usuario.CondicionTrabajo = (workSheet.Cells[rowIterator, 13].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.CondicionTrabajo = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 14].Value != null)
                                        {
                                            usuario.GradoAcademico = (workSheet.Cells[rowIterator, 14].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.GradoAcademico = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 15].Value != null)
                                        {
                                            usuario.UnidadNegocio = (workSheet.Cells[rowIterator, 15].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.UnidadNegocio = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 16].Value != null)
                                        {
                                            usuario.DivisionMarca = (workSheet.Cells[rowIterator, 16].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.DivisionMarca = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 17].Value != null)
                                        {
                                            usuario.AreaAgencia = (workSheet.Cells[rowIterator, 17].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.AreaAgencia = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 18].Value != null)
                                        {
                                            usuario.Departamento = (workSheet.Cells[rowIterator, 18].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.Departamento = "";
                                        }
                                        // 
                                        if (workSheet.Cells[rowIterator, 19].Value != null)
                                        {
                                            usuario.Subdepartamento = (workSheet.Cells[rowIterator, 19].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.Subdepartamento = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 20].Value != null)
                                        {
                                            usuario.EmpresaContratante = (workSheet.Cells[rowIterator, 20].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.EmpresaContratante = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 21].Value != null)
                                        {
                                            usuario.IdResponsableRH = Convert.ToInt32(workSheet.Cells[rowIterator, 21].Value);
                                        }
                                        else
                                        {
                                            usuario.IdResponsableRH = 0;
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 22].Value != null)
                                        {
                                            usuario.NombreResponsableRH = (workSheet.Cells[rowIterator, 22].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.NombreResponsableRH = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 23].Value != null)
                                        {
                                            usuario.IdJefe = Convert.ToInt32((workSheet.Cells[rowIterator, 23].Value));
                                        }
                                        else
                                        {
                                            usuario.IdJefe = 0;
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 24].Value != null)
                                        {
                                            usuario.NombreJefe = (workSheet.Cells[rowIterator, 24].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.NombreJefe = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 25].Value != null)
                                        {
                                            usuario.PuestoJefe = (workSheet.Cells[rowIterator, 25].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.PuestoJefe = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 26].Value != null)
                                        {
                                            usuario.IdResponsableEstructura = Convert.ToInt32((workSheet.Cells[rowIterator, 26].Value));
                                        }
                                        else
                                        {
                                            usuario.IdResponsableEstructura = 0;
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 27].Value != null)
                                        {
                                            usuario.NombreResponsableEstructura = (workSheet.Cells[rowIterator, 27].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.NombreResponsableEstructura = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 3].Value != null)
                                        {
                                            usuario.ClaveAcceso = (workSheet.Cells[rowIterator, 3].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.ClaveAcceso = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 29].Value != null)
                                        {
                                            usuario.RangoAntiguedad = (workSheet.Cells[rowIterator, 29].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.RangoAntiguedad = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 30].Value != null)
                                        {
                                            usuario.RangoEdad = (workSheet.Cells[rowIterator, 30].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.RangoEdad = "";
                                        }
                                        //
                                        //
                                        if (workSheet.Cells[rowIterator, 32].Value != null)
                                        {
                                            usuario.CampoNumerico_1 = Convert.ToInt32(workSheet.Cells[rowIterator, 32].Value);
                                        }
                                        else
                                        {
                                            usuario.CampoNumerico_1 = 0;
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 33].Value != null)
                                        {
                                            usuario.CampoDeTexto_1 = (workSheet.Cells[rowIterator, 33].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.CampoDeTexto_1 = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 34].Value != null)
                                        {
                                            usuario.CampoNumerico_2 = Convert.ToInt32(workSheet.Cells[rowIterator, 34].Value);
                                        }
                                        else
                                        {
                                            usuario.CampoNumerico_2 = 0;
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 35].Value != null)
                                        {
                                            usuario.CampoDeTexto_2 = (workSheet.Cells[rowIterator, 35].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.CampoDeTexto_2 = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 36].Value != null)
                                        {
                                            usuario.CampoNumerico_3 = Convert.ToInt32(workSheet.Cells[rowIterator, 36].Value);
                                        }
                                        else
                                        {
                                            usuario.CampoNumerico_3 = 0;
                                        }
                                        //
                                        if ((workSheet.Cells[rowIterator, 37].Value != null))
                                        {
                                            usuario.CampoDeTexto_3 = (workSheet.Cells[rowIterator, 37].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.CampoDeTexto_3 = "";
                                        }

                                        usuario.IdEstatus = ID_ESTATUS;

                                        usuario.IdBaseDeDatos = lastInsertBD;
                                        usuario.FechaHoraModificacion = DateTime.Now;
                                        usuario.UsuarioModificacion = Convert.ToString(Session["AdminLog"]);
                                        usuario.ProgramaModificacion = "DIagnostic4U";
                                        userList.Add(usuario);


                                    }
                                    else if ((workSheet.Cells[rowIterator, 31].Value.ToString()) == "Inactivo" || (workSheet.Cells[rowIterator, 31].Value.ToString()) == "INACTIVO")
                                    {
                                        ID_ESTATUS = 2;
                                        var usuario = new DL.Usuario();
                                        usuario.IdUsuario = Convert.ToInt32(workSheet.Cells[rowIterator, 2].Value);

                                        usuario.IdUsuario = Convert.ToInt32(workSheet.Cells[rowIterator, 2].Value);

                                        if (workSheet.Cells[rowIterator, 3].Value != null)
                                        {
                                            usuario.Password = (workSheet.Cells[rowIterator, 3].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.Password = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 4].Value != null)
                                        {
                                            usuario.Nombre = (workSheet.Cells[rowIterator, 4].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.Nombre = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 5].Value != null)
                                        {
                                            usuario.ApellidoPaterno = (workSheet.Cells[rowIterator, 5].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.ApellidoPaterno = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 6].Value != null)
                                        {
                                            usuario.ApellidoMaterno = (workSheet.Cells[rowIterator, 6].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.ApellidoMaterno = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 7].Value != null)
                                        {
                                            usuario.Puesto = (workSheet.Cells[rowIterator, 7].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.Puesto = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 8].Value != null)
                                        {
                                            usuario.FechaNacimiento = Convert.ToDateTime(workSheet.Cells[rowIterator, 8].Value.ToString());
                                        }
                                        else
                                        {
                                            usuario.FechaNacimiento = Convert.ToDateTime("01/01/1800");
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 9].Value != null)
                                        {
                                            usuario.FechaAntiguedad = Convert.ToDateTime(workSheet.Cells[rowIterator, 9].Value.ToString());
                                        }
                                        else
                                        {
                                            usuario.FechaAntiguedad = Convert.ToDateTime("01/01/1800");
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 10].Value != null)
                                        {
                                            usuario.Sexo = (workSheet.Cells[rowIterator, 10].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.Sexo = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 11].Value != null)
                                        {
                                            usuario.Email = (workSheet.Cells[rowIterator, 11].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.Email = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 12].Value != null)
                                        {
                                            usuario.TipoFuncion = (workSheet.Cells[rowIterator, 12].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.TipoFuncion = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 13].Value != null)
                                        {
                                            usuario.CondicionTrabajo = (workSheet.Cells[rowIterator, 13].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.CondicionTrabajo = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 14].Value != null)
                                        {
                                            usuario.GradoAcademico = (workSheet.Cells[rowIterator, 14].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.GradoAcademico = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 15].Value != null)
                                        {
                                            usuario.UnidadNegocio = (workSheet.Cells[rowIterator, 15].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.UnidadNegocio = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 16].Value != null)
                                        {
                                            usuario.DivisionMarca = (workSheet.Cells[rowIterator, 16].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.DivisionMarca = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 17].Value != null)
                                        {
                                            usuario.AreaAgencia = (workSheet.Cells[rowIterator, 17].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.AreaAgencia = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 18].Value != null)
                                        {
                                            usuario.Departamento = (workSheet.Cells[rowIterator, 18].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.Departamento = "";
                                        }
                                        // 
                                        if (workSheet.Cells[rowIterator, 19].Value != null)
                                        {
                                            usuario.Subdepartamento = (workSheet.Cells[rowIterator, 19].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.Subdepartamento = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 20].Value != null)
                                        {
                                            usuario.EmpresaContratante = (workSheet.Cells[rowIterator, 20].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.EmpresaContratante = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 21].Value != null)
                                        {
                                            usuario.IdResponsableRH = Convert.ToInt32(workSheet.Cells[rowIterator, 21].Value);
                                        }
                                        else
                                        {
                                            usuario.IdResponsableRH = 0;
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 22].Value != null)
                                        {
                                            usuario.NombreResponsableRH = (workSheet.Cells[rowIterator, 22].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.NombreResponsableRH = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 23].Value != null)
                                        {
                                            usuario.IdJefe = Convert.ToInt32((workSheet.Cells[rowIterator, 23].Value));
                                        }
                                        else
                                        {
                                            usuario.IdJefe = 0;
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 24].Value != null)
                                        {
                                            usuario.NombreJefe = (workSheet.Cells[rowIterator, 24].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.NombreJefe = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 25].Value != null)
                                        {
                                            usuario.PuestoJefe = (workSheet.Cells[rowIterator, 25].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.PuestoJefe = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 26].Value != null)
                                        {
                                            usuario.IdResponsableEstructura = Convert.ToInt32((workSheet.Cells[rowIterator, 26].Value));
                                        }
                                        else
                                        {
                                            usuario.IdResponsableEstructura = 0;
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 27].Value != null)
                                        {
                                            usuario.NombreResponsableEstructura = (workSheet.Cells[rowIterator, 27].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.NombreResponsableEstructura = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 3].Value != null)
                                        {
                                            usuario.ClaveAcceso = (workSheet.Cells[rowIterator, 3].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.ClaveAcceso = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 29].Value != null)
                                        {
                                            usuario.RangoAntiguedad = (workSheet.Cells[rowIterator, 29].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.RangoAntiguedad = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 30].Value != null)
                                        {
                                            usuario.RangoEdad = (workSheet.Cells[rowIterator, 30].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.RangoEdad = "";
                                        }
                                        //
                                        //
                                        if (workSheet.Cells[rowIterator, 32].Value != null)
                                        {
                                            usuario.CampoNumerico_1 = Convert.ToInt32(workSheet.Cells[rowIterator, 32].Value);
                                        }
                                        else
                                        {
                                            usuario.CampoNumerico_1 = 0;
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 33].Value != null)
                                        {
                                            usuario.CampoDeTexto_1 = (workSheet.Cells[rowIterator, 33].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.CampoDeTexto_1 = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 34].Value != null)
                                        {
                                            usuario.CampoNumerico_2 = Convert.ToInt32(workSheet.Cells[rowIterator, 34].Value);
                                        }
                                        else
                                        {
                                            usuario.CampoNumerico_2 = 0;
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 35].Value != null)
                                        {
                                            usuario.CampoDeTexto_2 = (workSheet.Cells[rowIterator, 35].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.CampoDeTexto_2 = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 36].Value != null)
                                        {
                                            usuario.CampoNumerico_3 = Convert.ToInt32(workSheet.Cells[rowIterator, 36].Value);
                                        }
                                        else
                                        {
                                            usuario.CampoNumerico_3 = 0;
                                        }
                                        //
                                        if ((workSheet.Cells[rowIterator, 37].Value != null))
                                        {
                                            usuario.CampoDeTexto_3 = (workSheet.Cells[rowIterator, 37].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.CampoDeTexto_3 = "";
                                        }

                                        usuario.IdEstatus = ID_ESTATUS;

                                        usuario.IdBaseDeDatos = lastInsertBD;
                                        usuario.FechaHoraModificacion = DateTime.Now;
                                        usuario.UsuarioModificacion = Convert.ToString(Session["AdminLog"]);
                                        usuario.ProgramaModificacion = "DIagnostic4U";
                                        userList.Add(usuario);
                                    }


                                }
                            }
                            else
                            {
                                Session["LogError"] = 6;
                                return Json("ERROR", JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    else
                    {
                        //Formato no valido
                        Session["LogError"] = 1;
                        return Json("ERROR", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    //No eligio archivo
                    Session["LogError"] = 5;
                    return Json("ERROR", JsonRequestBehavior.AllowGet);
                }
            }
            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                ML.Result result = new ML.Result();
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in userList)
                        {
                            BL.Usuario.Update(item, context, transaction);
                            context.SaveChanges();
                        }
                        //context.SaveChanges();
                        transaction.Commit();
                        result.Correct = true;
                        Session["LogError"] = 10;
                        return Json("success", JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception ex)
                    {
                        result.Correct = false;
                        result.ErrorMessage = ex.Message;
                        transaction.Rollback();
                        Session["LogError"] = 3;
                        return Json("ERROR", JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult UpdateEmpleadosBulk(FormCollection formCollection)
        {
            //Obtener ultima BD insertada
            int lastInsertBD = Convert.ToInt32(Session["IdBD"]);

            var userList = new List<DL.Empleado>();
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["postedFile_"];
                if ((file != null) && (file.ContentLength > 0) && (!string.IsNullOrEmpty(file.FileName)))
                {
                    string fileName = file.FileName;
                    string fileExtension = Path.GetExtension(file.FileName);
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));

                    if (fileExtension == ".xlsx")
                    {
                        using (var package = new ExcelPackage(file.InputStream))
                        {
                            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                            var currentSheet = package.Workbook.Worksheets;
                            var workSheet = currentSheet.First();
                            var noOfCol = workSheet.Dimension.End.Column;
                            var noOfRow = workSheet.Dimension.End.Row;

                            if (noOfRow > 2)
                            {
                                for (int rowIterator = 3; rowIterator <= noOfRow; rowIterator++)
                                {
                                    int ID_ESTATUS;
                                    if ((workSheet.Cells[rowIterator, 32].Value.ToString()) == "Activo" || (workSheet.Cells[rowIterator, 32].Value.ToString()) == "ACTIVO")
                                    {
                                        ID_ESTATUS = 1;
                                        var empleado = new DL.Empleado();

                                        //new
                                        empleado.IdEmpleado = Convert.ToInt32(workSheet.Cells[rowIterator, 2].Value);

                                        if (workSheet.Cells[rowIterator, 3].Value != null)
                                        {
                                            empleado.IdEmpleadoRH = Convert.ToInt32((workSheet.Cells[rowIterator, 3].Value));
                                        }
                                        else
                                        {
                                            empleado.IdEmpleadoRH = 0;
                                        }

                                        if (workSheet.Cells[rowIterator, 4].Value != null)
                                        {
                                            empleado.ClaveAcceso = (workSheet.Cells[rowIterator, 4].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.ClaveAcceso = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 5].Value != null)
                                        {
                                            empleado.Nombre = (workSheet.Cells[rowIterator, 5].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.Nombre = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 6].Value != null)
                                        {
                                            empleado.ApellidoPaterno = (workSheet.Cells[rowIterator, 6].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.ApellidoPaterno = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 7].Value != null)
                                        {
                                            empleado.ApellidoMaterno = (workSheet.Cells[rowIterator, 7].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.ApellidoMaterno = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 8].Value != null)
                                        {
                                            empleado.Puesto = (workSheet.Cells[rowIterator, 8].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.Puesto = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 9].Value != null)
                                        {
                                            empleado.FechaNacimiento = Convert.ToDateTime(workSheet.Cells[rowIterator, 9].Value.ToString());
                                        }
                                        else
                                        {
                                            empleado.FechaNacimiento = Convert.ToDateTime("01/01/1800");
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 10].Value != null)
                                        {
                                            empleado.FechaAntiguedad = Convert.ToDateTime(workSheet.Cells[rowIterator, 10].Value.ToString());
                                        }
                                        else
                                        {
                                            empleado.FechaAntiguedad = Convert.ToDateTime("01/01/1800");
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 11].Value != null)
                                        {
                                            empleado.Sexo = (workSheet.Cells[rowIterator, 11].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.Sexo = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 12].Value != null)
                                        {
                                            empleado.Correo = (workSheet.Cells[rowIterator, 12].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.Correo = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 13].Value != null)
                                        {
                                            empleado.TipoFuncion = (workSheet.Cells[rowIterator, 13].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.TipoFuncion = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 14].Value != null)
                                        {
                                            empleado.CondicionTrabajo = (workSheet.Cells[rowIterator, 14].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.CondicionTrabajo = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 15].Value != null)
                                        {
                                            empleado.GradoAcademico = (workSheet.Cells[rowIterator, 15].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.GradoAcademico = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 16].Value != null)
                                        {
                                            empleado.UnidadNegocio = (workSheet.Cells[rowIterator, 16].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.UnidadNegocio = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 17].Value != null)
                                        {
                                            empleado.DivisionMarca = (workSheet.Cells[rowIterator, 17].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.DivisionMarca = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 18].Value != null)
                                        {
                                            empleado.AreaAgencia = (workSheet.Cells[rowIterator, 18].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.AreaAgencia = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 19].Value != null)
                                        {
                                            empleado.Depto = (workSheet.Cells[rowIterator, 19].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.Depto = "";
                                        }
                                        // 
                                        if (workSheet.Cells[rowIterator, 20].Value != null)
                                        {
                                            empleado.Subdepartamento = (workSheet.Cells[rowIterator, 20].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.Subdepartamento = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 21].Value != null)
                                        {
                                            empleado.EmpresaContratante = (workSheet.Cells[rowIterator, 21].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.EmpresaContratante = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 22].Value != null)
                                        {
                                            empleado.IdResponsableRH = Convert.ToInt32(workSheet.Cells[rowIterator, 22].Value);
                                        }
                                        else
                                        {
                                            empleado.IdResponsableRH = 0;
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 23].Value != null)
                                        {
                                            empleado.NombreResponsableRH = (workSheet.Cells[rowIterator, 23].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.NombreResponsableRH = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 24].Value != null)
                                        {
                                            empleado.IdJefe = Convert.ToInt32((workSheet.Cells[rowIterator, 24].Value));
                                        }
                                        else
                                        {
                                            empleado.IdJefe = 0;
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 25].Value != null)
                                        {
                                            empleado.NombreJefe = (workSheet.Cells[rowIterator, 25].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.NombreJefe = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 26].Value != null)
                                        {
                                            empleado.PuestoJefe = (workSheet.Cells[rowIterator, 26].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.PuestoJefe = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 27].Value != null)
                                        {
                                            empleado.IdResponsableEstructura = Convert.ToInt32((workSheet.Cells[rowIterator, 27].Value));
                                        }
                                        else
                                        {
                                            empleado.IdResponsableEstructura = 0;
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 28].Value != null)
                                        {
                                            empleado.NombreResponsableEstructura = (workSheet.Cells[rowIterator, 28].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.NombreResponsableEstructura = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 4].Value != null)
                                        {
                                            empleado.ClaveAcceso = (workSheet.Cells[rowIterator, 4].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.ClaveAcceso = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 30].Value != null)
                                        {
                                            empleado.RangoAntiguedad = (workSheet.Cells[rowIterator, 30].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.RangoAntiguedad = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 31].Value != null)
                                        {
                                            empleado.RangoEdad = (workSheet.Cells[rowIterator, 31].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.RangoEdad = "";
                                        }
                                        //
                                        //

                                        empleado.EstatusEmpleado = "Activo";

                                        empleado.IdBaseDeDatos = lastInsertBD;
                                        empleado.FechaHoraModificacion = DateTime.Now;
                                        empleado.UsuarioModificacion = Convert.ToString(Session["AdminLog"]);
                                        empleado.ProgramaModificacion = "DIagnostic4U";
                                        userList.Add(empleado);


                                    }
                                    else if ((workSheet.Cells[rowIterator, 32].Value.ToString()) == "Inactivo" || (workSheet.Cells[rowIterator, 32].Value.ToString()) == "INACTIVO")
                                    {
                                        ID_ESTATUS = 2;
                                        var empleado = new DL.Empleado();
                                        empleado.IdEmpleado = Convert.ToInt32(workSheet.Cells[rowIterator, 2].Value);

                                        empleado.IdEmpleado = Convert.ToInt32(workSheet.Cells[rowIterator, 2].Value);
                                        if (workSheet.Cells[rowIterator, 3].Value != null)
                                        {
                                            empleado.IdEmpleadoRH = Convert.ToInt32((workSheet.Cells[rowIterator, 3].Value));
                                        }
                                        else
                                        {
                                            empleado.IdEmpleadoRH = 0;
                                        }


                                        if (workSheet.Cells[rowIterator, 4].Value != null)
                                        {
                                            empleado.ClaveAcceso = (workSheet.Cells[rowIterator, 4].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.ClaveAcceso = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 5].Value != null)
                                        {
                                            empleado.Nombre = (workSheet.Cells[rowIterator, 5].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.Nombre = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 6].Value != null)
                                        {
                                            empleado.ApellidoPaterno = (workSheet.Cells[rowIterator, 6].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.ApellidoPaterno = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 7].Value != null)
                                        {
                                            empleado.ApellidoMaterno = (workSheet.Cells[rowIterator, 7].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.ApellidoMaterno = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 8].Value != null)
                                        {
                                            empleado.Puesto = (workSheet.Cells[rowIterator, 8].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.Puesto = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 9].Value != null)
                                        {
                                            empleado.FechaNacimiento = Convert.ToDateTime(workSheet.Cells[rowIterator, 9].Value.ToString());
                                        }
                                        else
                                        {
                                            empleado.FechaNacimiento = Convert.ToDateTime("01/01/1800");
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 10].Value != null)
                                        {
                                            empleado.FechaAntiguedad = Convert.ToDateTime(workSheet.Cells[rowIterator, 10].Value.ToString());
                                        }
                                        else
                                        {
                                            empleado.FechaAntiguedad = Convert.ToDateTime("01/01/1800");
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 11].Value != null)
                                        {
                                            empleado.Sexo = (workSheet.Cells[rowIterator, 11].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.Sexo = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 12].Value != null)
                                        {
                                            empleado.Correo = (workSheet.Cells[rowIterator, 12].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.Correo = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 13].Value != null)
                                        {
                                            empleado.TipoFuncion = (workSheet.Cells[rowIterator, 13].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.TipoFuncion = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 14].Value != null)
                                        {
                                            empleado.CondicionTrabajo = (workSheet.Cells[rowIterator, 14].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.CondicionTrabajo = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 15].Value != null)
                                        {
                                            empleado.GradoAcademico = (workSheet.Cells[rowIterator, 15].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.GradoAcademico = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 16].Value != null)
                                        {
                                            empleado.UnidadNegocio = (workSheet.Cells[rowIterator, 16].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.UnidadNegocio = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 17].Value != null)
                                        {
                                            empleado.DivisionMarca = (workSheet.Cells[rowIterator, 17].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.DivisionMarca = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 18].Value != null)
                                        {
                                            empleado.AreaAgencia = (workSheet.Cells[rowIterator, 18].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.AreaAgencia = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 19].Value != null)
                                        {
                                            empleado.Depto = (workSheet.Cells[rowIterator, 19].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.Depto = "";
                                        }
                                        // 
                                        if (workSheet.Cells[rowIterator, 20].Value != null)
                                        {
                                            empleado.Subdepartamento = (workSheet.Cells[rowIterator, 20].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.Subdepartamento = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 21].Value != null)
                                        {
                                            empleado.EmpresaContratante = (workSheet.Cells[rowIterator, 21].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.EmpresaContratante = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 22].Value != null)
                                        {
                                            empleado.IdResponsableRH = Convert.ToInt32(workSheet.Cells[rowIterator, 22].Value);
                                        }
                                        else
                                        {
                                            empleado.IdResponsableRH = 0;
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 23].Value != null)
                                        {
                                            empleado.NombreResponsableRH = (workSheet.Cells[rowIterator, 23].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.NombreResponsableRH = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 24].Value != null)
                                        {
                                            empleado.IdJefe = Convert.ToInt32((workSheet.Cells[rowIterator, 24].Value));
                                        }
                                        else
                                        {
                                            empleado.IdJefe = 0;
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 25].Value != null)
                                        {
                                            empleado.NombreJefe = (workSheet.Cells[rowIterator, 25].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.NombreJefe = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 26].Value != null)
                                        {
                                            empleado.PuestoJefe = (workSheet.Cells[rowIterator, 26].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.PuestoJefe = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 27].Value != null)
                                        {
                                            empleado.IdResponsableEstructura = Convert.ToInt32((workSheet.Cells[rowIterator, 27].Value));
                                        }
                                        else
                                        {
                                            empleado.IdResponsableEstructura = 0;
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 28].Value != null)
                                        {
                                            empleado.NombreResponsableEstructura = (workSheet.Cells[rowIterator, 28].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.NombreResponsableEstructura = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 4].Value != null)
                                        {
                                            empleado.ClaveAcceso = (workSheet.Cells[rowIterator, 4].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.ClaveAcceso = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 30].Value != null)
                                        {
                                            empleado.RangoAntiguedad = (workSheet.Cells[rowIterator, 30].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.RangoAntiguedad = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 31].Value != null)
                                        {
                                            empleado.RangoEdad = (workSheet.Cells[rowIterator, 31].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.RangoEdad = "";
                                        }
                                        //
                                        

                                        empleado.EstatusEmpleado = "Inactivo";

                                        empleado.IdBaseDeDatos = lastInsertBD;
                                        empleado.FechaHoraModificacion = DateTime.Now;
                                        empleado.UsuarioModificacion = Convert.ToString(Session["AdminLog"]);
                                        empleado.ProgramaModificacion = "DIagnostic4U";
                                        userList.Add(empleado);
                                    }


                                }
                            }
                            else
                            {
                                Session["LogError"] = 6;
                                return Json("ERROR", JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    else
                    {
                        //Formato no valido
                        Session["LogError"] = 1;
                        return Json("ERROR", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    //No eligio archivo
                    Session["LogError"] = 5;
                    return Json("ERROR", JsonRequestBehavior.AllowGet);
                }
            }
            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                ML.Result result = new ML.Result();
               
                    try
                    {
                        foreach (var item in userList)
                        {
                            BL.Empleado.Update(item, context);
                            //context.SaveChanges();
                        }
                        context.SaveChanges();
                        //transaction.Commit();
                        result.Correct = true;
                        Session["LogError"] = 10;
                        return Json("success", JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception ex)
                    {
                        result.Correct = false;
                        result.ErrorMessage = ex.Message;
                        //transaction.Rollback();
                        Session["LogError"] = 3;
                        return Json("ERROR", JsonRequestBehavior.AllowGet);
                    }
                
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult ResetLogError()
        {
            Session["LogError"] = 9;
            return Json("success", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddToExistDatabase(FormCollection formCollection)
        {
            int usuariosAgregados = 0;
            int lastInsertBD = Convert.ToInt32(Session["IdBD"]);

            //get Tipo BD
            int tipoBD = Convert.ToInt32(Session["TipoBD"]);


            if (tipoBD == 2)
            {
                string PassGenerate = "";

                var userList = new List<DL.Usuario>();
                if (Request != null)
                {
                    HttpPostedFileBase file = Request.Files["postedFile"];
                    if ((file != null) && (file.ContentLength > 0) && (!string.IsNullOrEmpty(file.FileName)))
                    {
                        string fileName = file.FileName;
                        string fileExtension = Path.GetExtension(file.FileName);
                        string fileContentType = file.ContentType;
                        byte[] fileBytes = new byte[file.ContentLength];
                        var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));

                        if (fileExtension == ".xlsx")
                        {
                            using (var package = new ExcelPackage(file.InputStream))
                            {
                                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                                var currentSheet = package.Workbook.Worksheets;
                                var workSheet = currentSheet.First();
                                var noOfCol = workSheet.Dimension.End.Column;
                                var noOfRow = workSheet.Dimension.End.Row;

                                if (noOfRow > 1)
                                {
                                    for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                                    {
                                        try
                                        {
                                            string validacion = workSheet.Cells[rowIterator, 29].Value.ToString();
                                        }
                                        catch (Exception ex)
                                        {
                                            workSheet.Cells[rowIterator, 29].Value = "ACTIVO";
                                        }

                                        int ID_ESTATUS;
                                        if ((workSheet.Cells[rowIterator, 29].Value.ToString()) == "ACTIVO" || (workSheet.Cells[rowIterator, 29].Value.ToString()) == "Activo")
                                        {
                                            if (workSheet.Cells[rowIterator, 5].Value != null)
                                            {
                                                PassGenerate = workSheet.Cells[rowIterator, 5].Value.ToString();
                                            }
                                            else
                                            {
                                                PassGenerate = BL.GeneratorPass.GetUniqueKey(8);
                                            }
                                            ID_ESTATUS = 1;
                                            var usuario = new DL.Usuario();
                                            //usuario.UserName = (workSheet.Cells[rowIterator, 1].Value).ToString();
                                            usuario.Password = PassGenerate;
                                            //usuario.IdEmpleado = Convert.ToInt32(workSheet.Cells[rowIterator, 3].Value);
                                            if (workSheet.Cells[rowIterator, 2].Value != null)
                                            {
                                                usuario.Nombre = (workSheet.Cells[rowIterator, 2].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 3].Value != null)
                                            {
                                                usuario.ApellidoPaterno = (workSheet.Cells[rowIterator, 3].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 4].Value != null)
                                            {
                                                usuario.ApellidoMaterno = (workSheet.Cells[rowIterator, 4].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 6].Value != null)
                                            {
                                                usuario.Puesto = (workSheet.Cells[rowIterator, 6].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 7].Value != null)
                                            {
                                                usuario.FechaNacimiento = Convert.ToDateTime(workSheet.Cells[rowIterator, 7].Value.ToString());
                                            }
                                            else
                                            {
                                                usuario.FechaNacimiento = Convert.ToDateTime("01/01/1800");
                                            }
                                            if (workSheet.Cells[rowIterator, 8].Value != null)
                                            {
                                                usuario.FechaAntiguedad = Convert.ToDateTime(workSheet.Cells[rowIterator, 8].Value.ToString());
                                            }
                                            else
                                            {
                                                usuario.FechaAntiguedad = Convert.ToDateTime("01/01/1800");
                                            }
                                            if (workSheet.Cells[rowIterator, 9].Value != null)
                                            {
                                                usuario.Sexo = (workSheet.Cells[rowIterator, 9].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 10].Value != null)
                                            {
                                                usuario.Email = (workSheet.Cells[rowIterator, 10].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 11].Value != null)
                                            {
                                                usuario.TipoFuncion = (workSheet.Cells[rowIterator, 11].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 12].Value != null)
                                            {
                                                usuario.CondicionTrabajo = (workSheet.Cells[rowIterator, 12].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 13].Value != null)
                                            {
                                                usuario.GradoAcademico = (workSheet.Cells[rowIterator, 13].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 14].Value != null)
                                            {
                                                usuario.UnidadNegocio = (workSheet.Cells[rowIterator, 14].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 15].Value != null)
                                            {
                                                usuario.DivisionMarca = (workSheet.Cells[rowIterator, 15].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 16].Value != null)
                                            {
                                                usuario.AreaAgencia = (workSheet.Cells[rowIterator, 16].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 17].Value != null)
                                            {
                                                usuario.Departamento = (workSheet.Cells[rowIterator, 17].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 18].Value != null)
                                            {
                                                usuario.Subdepartamento = (workSheet.Cells[rowIterator, 18].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 19].Value != null)
                                            {
                                                usuario.EmpresaContratante = (workSheet.Cells[rowIterator, 19].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 20].Value != null)
                                            {
                                                usuario.IdResponsableRH = Convert.ToInt32(workSheet.Cells[rowIterator, 20].Value);
                                            }
                                            if (workSheet.Cells[rowIterator, 21].Value != null)
                                            {
                                                usuario.NombreResponsableRH = (workSheet.Cells[rowIterator, 21].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 22].Value != null)
                                            {
                                                usuario.IdJefe = Convert.ToInt32((workSheet.Cells[rowIterator, 22].Value));
                                            }
                                            if (workSheet.Cells[rowIterator, 23].Value != null)
                                            {
                                                usuario.NombreJefe = (workSheet.Cells[rowIterator, 23].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 24].Value != null)
                                            {
                                                usuario.PuestoJefe = (workSheet.Cells[rowIterator, 24].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 25].Value != null)
                                            {
                                                usuario.IdResponsableEstructura = Convert.ToInt32((workSheet.Cells[rowIterator, 25].Value));
                                            }
                                            if (workSheet.Cells[rowIterator, 26].Value != null)
                                            {
                                                usuario.NombreResponsableEstructura = (workSheet.Cells[rowIterator, 26].Value).ToString();
                                            }

                                            usuario.ClaveAcceso = PassGenerate;

                                            if (workSheet.Cells[rowIterator, 27].Value != null)
                                            {
                                                usuario.RangoAntiguedad = (workSheet.Cells[rowIterator, 27].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 28].Value != null)
                                            {
                                                usuario.RangoEdad = (workSheet.Cells[rowIterator, 28].Value).ToString();
                                            }

                                            if (workSheet.Cells[rowIterator, 30].Value != null)
                                            {
                                                usuario.CampoNumerico_1 = Convert.ToInt32(workSheet.Cells[rowIterator, 30].Value);
                                            }
                                            if (workSheet.Cells[rowIterator, 31].Value != null)
                                            {
                                                usuario.CampoDeTexto_1 = (workSheet.Cells[rowIterator, 31].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 32].Value != null)
                                            {
                                                usuario.CampoNumerico_2 = Convert.ToInt32(workSheet.Cells[rowIterator, 32].Value);
                                            }
                                            if (workSheet.Cells[rowIterator, 33].Value != null)
                                            {
                                                usuario.CampoDeTexto_2 = (workSheet.Cells[rowIterator, 33].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 34].Value != null)
                                            {
                                                usuario.CampoNumerico_3 = Convert.ToInt32(workSheet.Cells[rowIterator, 34].Value);
                                            }
                                            if ((workSheet.Cells[rowIterator, 35].Value != null))
                                            {
                                                usuario.CampoDeTexto_3 = (workSheet.Cells[rowIterator, 35].Value).ToString();
                                            }




                                            usuario.IdEstatus = ID_ESTATUS;

                                            usuario.IdBaseDeDatos = lastInsertBD;
                                            usuario.FechaHoraCreacion = DateTime.Now;
                                            usuario.UsuarioCreacion = Convert.ToString(Session["AdminLog"]);
                                            usuario.ProgramaCreacion = "DIagnostic4U";
                                            userList.Add(usuario);


                                        }
                                        else if ((workSheet.Cells[rowIterator, 29].Value.ToString()) == "INACTIVO" || (workSheet.Cells[rowIterator, 29].Value.ToString()) == "Inactivo")
                                        {
                                            if (workSheet.Cells[rowIterator, 5].Value != null)
                                            {
                                                PassGenerate = workSheet.Cells[rowIterator, 5].Value.ToString();
                                            }
                                            else
                                            {
                                                PassGenerate = BL.GeneratorPass.GetUniqueKey(8);
                                            }

                                            ID_ESTATUS = 2;
                                            var usuario = new DL.Usuario();
                                            usuario.UserName = (workSheet.Cells[rowIterator, 1].Value).ToString();
                                            usuario.Password = PassGenerate;
                                            //usuario.IdEmpleado = Convert.ToInt32(workSheet.Cells[rowIterator, 3].Value);
                                            if (workSheet.Cells[rowIterator, 2].Value != null)
                                            {
                                                usuario.Nombre = (workSheet.Cells[rowIterator, 2].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 3].Value != null)
                                            {
                                                usuario.ApellidoPaterno = (workSheet.Cells[rowIterator, 3].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 4].Value != null)
                                            {
                                                usuario.ApellidoMaterno = (workSheet.Cells[rowIterator, 4].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 6].Value != null)
                                            {
                                                usuario.Puesto = (workSheet.Cells[rowIterator, 6].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 7].Value != null)
                                            {
                                                usuario.FechaNacimiento = Convert.ToDateTime(workSheet.Cells[rowIterator, 7].Value.ToString());
                                            }
                                            else
                                            {
                                                usuario.FechaNacimiento = Convert.ToDateTime("01/01/1800");
                                            }
                                            if (workSheet.Cells[rowIterator, 8].Value != null)
                                            {
                                                usuario.FechaAntiguedad = Convert.ToDateTime(workSheet.Cells[rowIterator, 8].Value.ToString());
                                            }
                                            else
                                            {
                                                usuario.FechaAntiguedad = Convert.ToDateTime("01/01/1800");
                                            }
                                            if (workSheet.Cells[rowIterator, 9].Value != null)
                                            {
                                                usuario.Sexo = (workSheet.Cells[rowIterator, 9].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 10].Value != null)
                                            {
                                                usuario.Email = (workSheet.Cells[rowIterator, 10].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 11].Value != null)
                                            {
                                                usuario.TipoFuncion = (workSheet.Cells[rowIterator, 11].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 12].Value != null)
                                            {
                                                usuario.CondicionTrabajo = (workSheet.Cells[rowIterator, 12].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 13].Value != null)
                                            {
                                                usuario.GradoAcademico = (workSheet.Cells[rowIterator, 13].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 14].Value != null)
                                            {
                                                usuario.UnidadNegocio = (workSheet.Cells[rowIterator, 14].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 15].Value != null)
                                            {
                                                usuario.DivisionMarca = (workSheet.Cells[rowIterator, 15].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 16].Value != null)
                                            {
                                                usuario.AreaAgencia = (workSheet.Cells[rowIterator, 16].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 17].Value != null)
                                            {
                                                usuario.Departamento = (workSheet.Cells[rowIterator, 17].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 18].Value != null)
                                            {
                                                usuario.Subdepartamento = (workSheet.Cells[rowIterator, 18].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 19].Value != null)
                                            {
                                                usuario.EmpresaContratante = (workSheet.Cells[rowIterator, 19].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 20].Value != null)
                                            {
                                                usuario.IdResponsableRH = Convert.ToInt32(workSheet.Cells[rowIterator, 20].Value);
                                            }
                                            if (workSheet.Cells[rowIterator, 21].Value != null)
                                            {
                                                usuario.NombreResponsableRH = (workSheet.Cells[rowIterator, 21].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 22].Value != null)
                                            {
                                                usuario.IdJefe = Convert.ToInt32((workSheet.Cells[rowIterator, 22].Value));
                                            }
                                            if (workSheet.Cells[rowIterator, 23].Value != null)
                                            {
                                                usuario.NombreJefe = (workSheet.Cells[rowIterator, 23].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 24].Value != null)
                                            {
                                                usuario.PuestoJefe = (workSheet.Cells[rowIterator, 24].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 25].Value != null)
                                            {
                                                usuario.IdResponsableEstructura = Convert.ToInt32((workSheet.Cells[rowIterator, 25].Value));
                                            }
                                            if (workSheet.Cells[rowIterator, 26].Value != null)
                                            {
                                                usuario.NombreResponsableEstructura = (workSheet.Cells[rowIterator, 26].Value).ToString();
                                            }

                                            usuario.ClaveAcceso = PassGenerate;

                                            if (workSheet.Cells[rowIterator, 27].Value != null)
                                            {
                                                usuario.RangoAntiguedad = (workSheet.Cells[rowIterator, 27].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 28].Value != null)
                                            {
                                                usuario.RangoEdad = (workSheet.Cells[rowIterator, 28].Value).ToString();
                                            }

                                            if (workSheet.Cells[rowIterator, 30].Value != null)
                                            {
                                                usuario.CampoNumerico_1 = Convert.ToInt32(workSheet.Cells[rowIterator, 30].Value);
                                            }
                                            if (workSheet.Cells[rowIterator, 31].Value != null)
                                            {
                                                usuario.CampoDeTexto_1 = (workSheet.Cells[rowIterator, 31].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 32].Value != null)
                                            {
                                                usuario.CampoNumerico_2 = Convert.ToInt32(workSheet.Cells[rowIterator, 32].Value);
                                            }
                                            if (workSheet.Cells[rowIterator, 33].Value != null)
                                            {
                                                usuario.CampoDeTexto_2 = (workSheet.Cells[rowIterator, 33].Value).ToString();
                                            }
                                            if (workSheet.Cells[rowIterator, 34].Value != null)
                                            {
                                                usuario.CampoNumerico_3 = Convert.ToInt32(workSheet.Cells[rowIterator, 34].Value);
                                            }
                                            if ((workSheet.Cells[rowIterator, 35].Value != null))
                                            {
                                                usuario.CampoDeTexto_3 = (workSheet.Cells[rowIterator, 35].Value).ToString();
                                            }



                                            usuario.IdEstatus = ID_ESTATUS;

                                            usuario.IdBaseDeDatos = lastInsertBD;
                                            usuario.FechaHoraCreacion = DateTime.Now;
                                            usuario.UsuarioCreacion = Convert.ToString(Session["AdminLog"]);
                                            usuario.ProgramaCreacion = "DIagnostic4U";
                                            userList.Add(usuario);
                                        }


                                    }
                                }
                                else
                                {
                                    Session["LogError"] = 6;
                                    return Redirect(Request.UrlReferrer.ToString());
                                }
                            }
                        }
                        else
                        {
                            //Formato no valido
                            Session["LogError"] = 1;
                            return Redirect(Request.UrlReferrer.ToString());
                        }
                    }
                    else
                    {
                        //No eligio archivo
                        Session["LogError"] = 5;
                        return Redirect(Request.UrlReferrer.ToString());
                    }
                }
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {

                    ML.Result result = new ML.Result();
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            //context.BulkInsert(userList, options => options.ColumnPrimaryKeyExpression = c => c.IdUsuario);
                            int bd = 0;
                            foreach (var item in userList)
                            {
                                usuariosAgregados = userList.Count();
                                context.Usuario.Add(item);
                                var lastUserId = context.Usuario.Max(m => m.IdUsuario);
                                int IDBD = (int)item.IdBaseDeDatos;
                                bd = IDBD;
                            }
                            context.SaveChanges();
                            transaction.Commit();
                            result.Correct = true;
                            ViewBag.Message = "Los usuarios han sido agregados exitosamente.";

                            using (DL.RH_DesEntities con = new DL.RH_DesEntities())
                            {
                                List<int> idUsr = new List<int>();
                                var getUsuaris = con.Usuario.SqlQuery("SELECT * FROM USUARIO WHERE IDBASEDEDATOS = {0}", lastInsertBD).ToList();
                                foreach (var item in getUsuaris)
                                {
                                    idUsr.Add(item.IdUsuario);
                                }
                                var query = con.Encuesta.SqlQuery("SELECT * FROM Encuesta WHERE IdBasesDeDatos = {0}", lastInsertBD).ToList();

                                var items = idUsr.Count() - usuariosAgregados;
                                foreach (var item in query)
                                {
                                    for (int i = items; i < idUsr.Count; i++)
                                    {
                                        var Insert = con.Database.ExecuteSqlCommand("INSERT INTO UsuarioEstatusEncuesta (IdUsuario, IdEncuesta, IdEstatusEncuestaD4U, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) values ({0}, {1}, {2}, {3}, {4}, {5})",
                                                idUsr[i], item.IdEncuesta, 1, DateTime.Now, "Agregausuario", "Diagnostic4U");
                                        con.SaveChanges();
                                    }
                                }
                            }
                            return PartialView("ValidacionModal");
                        }
                        catch (Exception ex)
                        {
                            result.Correct = false;
                            result.ErrorMessage = ex.Message;
                            transaction.Rollback();
                            ViewBag.Message = "Ha ocurrido un error al registrar a los usuarios.";
                            return PartialView("ValidacionModal");
                        }
                    }
                }
            }
            else if (tipoBD == 1)
            {
                //lastInsertBD
                var empleadoList = new List<DL.Empleado>();
                if (Request != null)
                {
                    HttpPostedFileBase file = Request.Files["postedFile"];
                    if ((file != null) && (file.ContentLength > 0) && (!string.IsNullOrEmpty(file.FileName)))
                    {
                        string fileName = file.FileName;
                        string fileExtension = Path.GetExtension(file.FileName);
                        string fileContentType = file.ContentType;
                        byte[] fileBytes = new byte[file.ContentLength];
                        var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));

                        if (fileExtension == ".xlsx")
                        {
                            using (var package = new ExcelPackage(file.InputStream))
                            {
                                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                                var currentSheet = package.Workbook.Worksheets;
                                var workSheet = currentSheet.First();
                                var noOfCol = workSheet.Dimension.End.Column;
                                var noOfRow = workSheet.Dimension.End.Row;

                                if (noOfRow > 1)
                                {
                                    for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                                    {

                                        var empleado = new DL.Empleado();

                                        if (workSheet.Cells[rowIterator, 28].Value != null)
                                        {
                                            empleado.EstatusEmpleado = (workSheet.Cells[rowIterator, 28].Value.ToString());
                                        }
                                        else
                                        {
                                            empleado.EstatusEmpleado = "Activo";
                                        }


                                        if (workSheet.Cells[rowIterator, 1].Value != null)
                                        {
                                            empleado.IdEmpleadoRH = Convert.ToInt32(workSheet.Cells[rowIterator, 1].Value);
                                        }
                                        if (workSheet.Cells[rowIterator, 2].Value != null)
                                        {
                                            empleado.Nombre = (workSheet.Cells[rowIterator, 2].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 3].Value != null)
                                        {
                                            empleado.ApellidoPaterno = (workSheet.Cells[rowIterator, 3].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 4].Value != null)
                                        {
                                            empleado.ApellidoMaterno = (workSheet.Cells[rowIterator, 4].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 5].Value != null)
                                        {
                                            empleado.Puesto = (workSheet.Cells[rowIterator, 5].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 6].Value != null)
                                        {
                                            empleado.FechaNacimiento = Convert.ToDateTime(workSheet.Cells[rowIterator, 6].Value.ToString());
                                        }
                                        else
                                        {
                                            empleado.FechaNacimiento = Convert.ToDateTime("01/01/1800");
                                        }
                                        if (workSheet.Cells[rowIterator, 7].Value != null)
                                        {
                                            empleado.FechaAntiguedad = Convert.ToDateTime(workSheet.Cells[rowIterator, 7].Value.ToString());
                                        }
                                        else
                                        {
                                            empleado.FechaAntiguedad = Convert.ToDateTime("01/01/1800");
                                        }
                                        if (workSheet.Cells[rowIterator, 8].Value != null)
                                        {
                                            empleado.Sexo = (workSheet.Cells[rowIterator, 8].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 9].Value != null)
                                        {
                                            empleado.Correo = (workSheet.Cells[rowIterator, 9].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 10].Value != null)
                                        {
                                            empleado.TipoFuncion = (workSheet.Cells[rowIterator, 10].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 11].Value != null)
                                        {
                                            empleado.CondicionTrabajo = (workSheet.Cells[rowIterator, 11].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 12].Value != null)
                                        {
                                            empleado.GradoAcademico = (workSheet.Cells[rowIterator, 12].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 13].Value != null)
                                        {
                                            empleado.UnidadNegocio = (workSheet.Cells[rowIterator, 13].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 14].Value != null)
                                        {
                                            empleado.DivisionMarca = (workSheet.Cells[rowIterator, 14].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 15].Value != null)
                                        {
                                            empleado.AreaAgencia = (workSheet.Cells[rowIterator, 15].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 16].Value != null)
                                        {
                                            empleado.Depto = (workSheet.Cells[rowIterator, 16].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 17].Value != null)
                                        {
                                            empleado.Subdepartamento = (workSheet.Cells[rowIterator, 17].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 18].Value != null)
                                        {
                                            empleado.EmpresaContratante = (workSheet.Cells[rowIterator, 18].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 19].Value != null)
                                        {
                                            empleado.IdResponsableRH = Convert.ToInt32(workSheet.Cells[rowIterator, 19].Value);
                                        }
                                        if (workSheet.Cells[rowIterator, 20].Value != null)
                                        {
                                            empleado.NombreResponsableRH = (workSheet.Cells[rowIterator, 20].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 21].Value != null)
                                        {
                                            empleado.IdJefe = Convert.ToInt32((workSheet.Cells[rowIterator, 21].Value));
                                        }
                                        if (workSheet.Cells[rowIterator, 22].Value != null)
                                        {
                                            empleado.NombreJefe = (workSheet.Cells[rowIterator, 22].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 23].Value != null)
                                        {
                                            empleado.PuestoJefe = (workSheet.Cells[rowIterator, 23].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 24].Value != null)
                                        {
                                            empleado.IdResponsableEstructura = Convert.ToInt32((workSheet.Cells[rowIterator, 24].Value));
                                        }
                                        if (workSheet.Cells[rowIterator, 25].Value != null)
                                        {
                                            empleado.NombreResponsableEstructura = (workSheet.Cells[rowIterator, 25].Value).ToString();
                                        }

                                        empleado.ClaveAcceso = BL.GeneratorPass.GetUniqueKey(8);

                                        if (workSheet.Cells[rowIterator, 26].Value != null)
                                        {
                                            empleado.RangoAntiguedad = (workSheet.Cells[rowIterator, 26].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 27].Value != null)
                                        {
                                            empleado.RangoEdad = (workSheet.Cells[rowIterator, 27].Value).ToString();
                                        }



                                        empleado.IdBaseDeDatos = lastInsertBD;
                                        empleado.FechaHoraCreacion = DateTime.Now;
                                        empleado.UsuarioCreacion = Convert.ToString(Session["AdminLog"]);
                                        empleado.ProgramaCreacion = "DIagnostic4U";
                                        empleadoList.Add(empleado);
                                        //Save ClaveAcceso






                                    }
                                }
                                else
                                {
                                    Session["LogError"] = 6;
                                    return Redirect(Request.UrlReferrer.ToString());
                                }
                            }
                        }
                        else
                        {
                            //Formato no valido
                            Session["LogError"] = 1;
                            return Redirect(Request.UrlReferrer.ToString());
                        }
                    }
                    else
                    {
                        //No eligio archivo
                        Session["LogError"] = 5;
                        return Redirect(Request.UrlReferrer.ToString());
                    }
                }
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {

                    ML.Result result = new ML.Result();
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            foreach (var item in empleadoList)
                            {
                                DL.ClavesAcceso clave = new DL.ClavesAcceso();
                                clave.ClaveAcceso = item.ClaveAcceso;
                                context.ClavesAcceso.Add(clave);

                                var newIdEmpleado = context.Empleado.Max(m => m.IdEmpleado) + 1;
                                item.IdEmpleado = newIdEmpleado;
                                context.Empleado.Add(item);
                                context.SaveChanges();

                                //Agregar estatus de encuesta
                                DL.EstatusEncuesta st = new DL.EstatusEncuesta();
                                st.IdEncuesta = 1;
                                st.IdEmpleado = item.IdEmpleado;
                                st.Estatus = "No comenzada";
                                context.EstatusEncuesta.Add(st);
                            }

                            context.SaveChanges();
                            transaction.Commit();
                            result.Correct = true;
                            Session["LogError"] = 10;
                            ViewBag.Message = "Los usuarios han sido agregados exitosamente";
                            return PartialView("ValidacionModal");
                        }
                        catch (Exception ex)
                        {
                            string ExcepcionGenerada = "";
                            result.Correct = false;
                            result.ErrorMessage = ex.InnerException.InnerException.Message;
                            if (result.ErrorMessage == excepcionIdEmpleadoDuplicado)
                            {
                                ExcepcionGenerada = "Los Id de Empleado en el layout ya se encuentran registrados.";
                            }
                            else
                            {
                                ExcepcionGenerada = "Ha ocurrido un error al intentar agregar a los usuarios.";
                            }
                            transaction.Rollback();
                            Session["LogError"] = 3;
                            ViewBag.Message = ExcepcionGenerada;
                            return PartialView("ValidacionModal");
                        }
                    }
                }
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult DownloadLayout()
        {
            string file = "LayoutUsuarios.xlsx";
            string fullPath = Path.Combine(Server.MapPath("~/resources/"), file);
            return File(fullPath, "application/vnd.ms-excel", file);
        }
        public ActionResult DownloadLayoutClimaLaboral()
        {
            string file = "LayoutUsuarios - Clima Laboral.xlsx";
            string fullPath = Path.Combine(Server.MapPath("~/resources/"), file);
            return File(fullPath, "application/vnd.ms-excel", file);
        }

        public ActionResult UpdateEstatus(ML.BasesDeDatos BD)
        {
            string currentUser = Convert.ToString(Session["AdminLog"]);
            var result = BL.BasesDeDatos.UpdateEstatus(BD, currentUser);
            if (result.Correct == false)
            {
                return RedirectToAction("GetAllForListado", "BD");
            }
            else
            {
                return RedirectToAction("GetAllForListado", "BD");
            }
        }

        public ActionResult DeleteBD(ML.BasesDeDatos BD)
        {
            string currentUser = Convert.ToString(Session["AdminLog"]);
            var result = BL.BasesDeDatos.Delete(BD, currentUser);
            if (result.Correct == false)
            {
                return RedirectToAction("GetEliminadas", "BD");
            }
            else
            {
                return RedirectToAction("GetEliminadas", "BD");
            }
        }

        public ActionResult GetEliminadas()
        {
            List<object> permisosEstructura = new List<object>();
            ViewBag.Permisos = Session["CompaniesPermisos"];
            permisosEstructura = ViewBag.Permisos;

            return View(BL.BasesDeDatos.GetEliminadas(permisosEstructura));
        }
        public ActionResult Restaurar(ML.BasesDeDatos BD)
        {
            string currentUser = Convert.ToString(Session["AdminLog"]);
            var result = BL.BasesDeDatos.Restaurar(BD, currentUser);

            if (result.Correct == false)
            {
                return RedirectToAction("GetEliminadas", "BD");
            }
            else
            {
                return RedirectToAction("GetAllForListado", "BD");
            }
        }

        public ActionResult GetUsuarioById(ML.Usuario user)
        {
            var result = BL.Usuario.GetById(user.IdUsuario);

            ML.Usuario usuario = new ML.Usuario();
            usuario.EstatusEncuesta = new ML.EstatusEncuesta();
            usuario.TipoEstatus = new ML.TipoEstatus();
            usuario.Empleado = new ML.Empleado();
            usuario.Perfil = new ML.Perfil();
            usuario.BaseDeDatos = new ML.BasesDeDatos();

            usuario.BaseDeDatos.Nombre = ((ML.Usuario)result.Object).BaseDeDatos.Nombre;

            usuario.IdUsuario = ((ML.Usuario)result.Object).IdUsuario;
            usuario.Nombre = ((ML.Usuario)result.Object).Nombre;
            usuario.ApellidoPaterno = ((ML.Usuario)result.Object).ApellidoPaterno;
            usuario.ApellidoMaterno = ((ML.Usuario)result.Object).ApellidoMaterno;
            usuario.Username = ((ML.Usuario)result.Object).Username;
            usuario.Password = ((ML.Usuario)result.Object).Password;
            usuario.Empleado.IdEmpleado = ((ML.Usuario)result.Object).Empleado.IdEmpleado;

            usuario.Puesto = ((ML.Usuario)result.Object).Puesto;
            DateTime nacimiento = ((ML.Usuario)result.Object).FechaNacimiento;
            usuario.dateNacim = nacimiento.ToString("yyyy-MM-dd");
            
            DateTime antiguedad = ((ML.Usuario)result.Object).FechaAntiguedad;
            usuario.dateAntig = antiguedad.ToString("yyyy-MM-dd");


            usuario.Sexo = ((ML.Usuario)result.Object).Sexo;
            usuario.Email = ((ML.Usuario)result.Object).Email;
            usuario.TipoFuncion = ((ML.Usuario)result.Object).TipoFuncion;
            usuario.CondicionTrabajo = ((ML.Usuario)result.Object).CondicionTrabajo;
            usuario.GradoAcademico = ((ML.Usuario)result.Object).GradoAcademico;
            usuario.UnidadNegocio = ((ML.Usuario)result.Object).UnidadNegocio;
            usuario.DivisionMarca = ((ML.Usuario)result.Object).DivisionMarca;
            usuario.AreaAgencia = ((ML.Usuario)result.Object).AreaAgencia;
            usuario.Departamento = ((ML.Usuario)result.Object).Departamento;
            usuario.Subdepartamento = ((ML.Usuario)result.Object).Subdepartamento;
            usuario.EmpresaContratante = ((ML.Usuario)result.Object).EmpresaContratante;
            usuario.IdResponsableRH = ((ML.Usuario)result.Object).IdResponsableRH;
            usuario.NombreResponsableRH = ((ML.Usuario)result.Object).NombreResponsableRH;
            usuario.IdJefe = ((ML.Usuario)result.Object).IdJefe;
            usuario.NombreJefe = ((ML.Usuario)result.Object).NombreJefe;
            usuario.PuestoJefe = ((ML.Usuario)result.Object).PuestoJefe;
            usuario.IdRespinsableEstructura = ((ML.Usuario)result.Object).IdRespinsableEstructura;
            usuario.NombreResponsableEstructura = ((ML.Usuario)result.Object).NombreResponsableEstructura;

            usuario.ClaveAcceso = ((ML.Usuario)result.Object).ClaveAcceso;

            usuario.RangoAntiguedad = ((ML.Usuario)result.Object).RangoAntiguedad;
            usuario.RangoEdad = ((ML.Usuario)result.Object).RangoEdad;

            usuario.TipoEstatus.IdEstatus = ((ML.Usuario)result.Object).TipoEstatus.IdEstatus;

            usuario.CampoDeTexto_1 = ((ML.Usuario)result.Object).CampoDeTexto_1;
            usuario.CampoDeTexto_2 = ((ML.Usuario)result.Object).CampoDeTexto_2;
            usuario.CampoDeTexto_3 = ((ML.Usuario)result.Object).CampoDeTexto_3;
            usuario.CampoNumerico_1 = ((ML.Usuario)result.Object).CampoNumerico_1;
            usuario.CampoNumerico_2 = ((ML.Usuario)result.Object).CampoNumerico_2;
            usuario.CampoNumerico_3 = ((ML.Usuario)result.Object).CampoNumerico_3;

            return Json(usuario, JsonRequestBehavior.AllowGet);
        }

        
        public ActionResult UpdateOneUsuario(ML.Usuario user)
        {
            var result = BL.Usuario.ActualizarUsuario(user);

            if (result.Correct == true)
            {
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult AddUsuariosClimaLaboral(FormCollection formCollection)
        {
            //Obtener ultima BD insertada
            //inerrtar anio en estatus encuesta
            int lastInsertBD = 0;
            var maxIdEmpleado = 0;
            using (DL.RH_DesEntities contextOne = new DL.RH_DesEntities())
            {
                lastInsertBD = contextOne.BasesDeDatos.Max(p => p.IdBasesDeDatos);
            }



            var empleadoList = new List<DL.Empleado>();
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["postedFile"];
                if ((file != null) && (file.ContentLength > 0) && (!string.IsNullOrEmpty(file.FileName)))
                {
                    string fileName = file.FileName;
                    string fileExtension = Path.GetExtension(file.FileName);
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));

                    if (fileExtension == ".xlsx")
                    {
                        using (var package = new ExcelPackage(file.InputStream))
                        {
                            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                            var currentSheet = package.Workbook.Worksheets;
                            var workSheet = currentSheet.First();
                            var noOfCol = workSheet.Dimension.End.Column;
                            var noOfRow = workSheet.Dimension.End.Row;

                            if (noOfRow > 1)
                            {
                                for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                                {   
                                        
                                        var empleado = new DL.Empleado();
                                        
                                        if (workSheet.Cells[rowIterator, 28].Value != null)
                                        {
                                        empleado.EstatusEmpleado = (workSheet.Cells[rowIterator, 28].Value.ToString());
                                        }
                                        else
                                        {
                                        empleado.EstatusEmpleado = "Activo";
                                        }

                                    

                                        //IdEmpleado de Recursos Humanos(Puede ser repetido y venir vacio)
                                        if (workSheet.Cells[rowIterator, 1].Value != null)
                                        {
                                            empleado.IdEmpleadoRH = Convert.ToInt32(workSheet.Cells[rowIterator, 1].Value);
                                        }
                                        if (workSheet.Cells[rowIterator, 2].Value != null)
                                        {
                                            empleado.Nombre = (workSheet.Cells[rowIterator, 2].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 3].Value != null)
                                        {
                                            empleado.ApellidoPaterno = (workSheet.Cells[rowIterator, 3].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 4].Value != null)
                                        {
                                            empleado.ApellidoMaterno = (workSheet.Cells[rowIterator, 4].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 5].Value != null)
                                        {
                                            empleado.Puesto = (workSheet.Cells[rowIterator, 5].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 6].Value != null)
                                        {
                                            empleado.FechaNacimiento = Convert.ToDateTime(workSheet.Cells[rowIterator, 6].Value.ToString());
                                        }
                                        else
                                        {
                                            empleado.FechaNacimiento = Convert.ToDateTime("01/01/1800");
                                        }
                                        if (workSheet.Cells[rowIterator, 7].Value != null)
                                        {
                                            empleado.FechaAntiguedad = Convert.ToDateTime(workSheet.Cells[rowIterator, 7].Value.ToString());
                                        }
                                        else
                                        {
                                            empleado.FechaAntiguedad = Convert.ToDateTime("01/01/1800");
                                        }
                                        if (workSheet.Cells[rowIterator, 8].Value != null)
                                        {
                                            empleado.Sexo = (workSheet.Cells[rowIterator, 8].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 9].Value != null)
                                        {
                                            empleado.Correo = (workSheet.Cells[rowIterator, 9].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 10].Value != null)
                                        {
                                            empleado.TipoFuncion = (workSheet.Cells[rowIterator, 10].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 11].Value != null)
                                        {
                                            empleado.CondicionTrabajo = (workSheet.Cells[rowIterator, 11].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 12].Value != null)
                                        {
                                            empleado.GradoAcademico = (workSheet.Cells[rowIterator, 12].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 13].Value != null)
                                        {
                                            empleado.UnidadNegocio = (workSheet.Cells[rowIterator, 13].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 14].Value != null)
                                        {
                                            empleado.DivisionMarca = (workSheet.Cells[rowIterator, 14].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 15].Value != null)
                                        {
                                            empleado.AreaAgencia = (workSheet.Cells[rowIterator, 15].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 16].Value != null)
                                        {
                                            empleado.Depto = (workSheet.Cells[rowIterator, 16].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 17].Value != null)
                                        {
                                            empleado.Subdepartamento = (workSheet.Cells[rowIterator, 17].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 18].Value != null)
                                        {
                                            empleado.EmpresaContratante = (workSheet.Cells[rowIterator, 18].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 19].Value != null)
                                        {
                                            empleado.IdResponsableRH = Convert.ToInt32(workSheet.Cells[rowIterator, 19].Value);
                                        }
                                        if (workSheet.Cells[rowIterator, 20].Value != null)
                                        {
                                            empleado.NombreResponsableRH = (workSheet.Cells[rowIterator, 20].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 21].Value != null)
                                        {
                                            empleado.IdJefe = Convert.ToInt32((workSheet.Cells[rowIterator, 21].Value));
                                        }
                                        if (workSheet.Cells[rowIterator, 22].Value != null)
                                        {
                                            empleado.NombreJefe = (workSheet.Cells[rowIterator, 22].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 23].Value != null)
                                        {
                                            empleado.PuestoJefe = (workSheet.Cells[rowIterator, 23].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 24].Value != null)
                                        {
                                            empleado.IdResponsableEstructura = Convert.ToInt32((workSheet.Cells[rowIterator, 24].Value));
                                        }
                                        if (workSheet.Cells[rowIterator, 25].Value != null)
                                        {
                                            empleado.NombreResponsableEstructura = (workSheet.Cells[rowIterator, 25].Value).ToString();
                                        }

                                        empleado.ClaveAcceso = BL.GeneratorPass.GetUniqueKey(8);

                                        if (workSheet.Cells[rowIterator, 26].Value != null)
                                        {
                                            empleado.RangoAntiguedad = (workSheet.Cells[rowIterator, 26].Value).ToString();
                                        }
                                        if (workSheet.Cells[rowIterator, 27].Value != null)
                                        {
                                            empleado.RangoEdad = (workSheet.Cells[rowIterator, 27].Value).ToString();
                                        }

                                        

                                        empleado.IdBaseDeDatos = lastInsertBD;
                                        empleado.FechaHoraCreacion = DateTime.Now;
                                        empleado.UsuarioCreacion = Convert.ToString(Session["AdminLog"]);
                                        empleado.ProgramaCreacion = "DIagnostic4U";
                                        empleadoList.Add(empleado);
                                        //Save ClaveAcceso


                                    
                                    


                                }
                            }
                            else
                            {
                                Session["LogError"] = 6;
                                return Json("ERROR");
                            }
                        }
                    }
                    else
                    {
                        //Formato no valido
                        Session["LogError"] = 1;
                        return Json("ERROR");
                    }
                }
                else
                {
                    //No eligio archivo
                    Session["LogError"] = 5;
                    return Json("ERROR");
                }
            }
            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {

                ML.Result result = new ML.Result();
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in empleadoList)
                        {
                            maxIdEmpleado = context.Empleado.Max(m => m.IdEmpleado) + 1;
                            item.IdEmpleado = maxIdEmpleado;
                            //Instanciar nueva clave de acceso
                            DL.ClavesAcceso clave = new DL.ClavesAcceso();
                            clave.ClaveAcceso = item.ClaveAcceso;
                            context.ClavesAcceso.Add(clave);
                            //Crear empleado
                            context.Empleado.Add(item);
                            context.SaveChanges();
                            //Crear registro que hace trigger
                            /*
                            INSERT INTO EstatusEncuesta (Estatus, IdEncuesta, IdEmpleado, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion)
		                    select 'No comenzada', 1, IdEmpleado, CURRENT_TIMESTAMP, CURRENT_USER, 'RH_Encuesta' from Empleado --WHERE UNIDAD DE NEGOCIO = 'TURISMO'

                            INSERT INTO EmpleadoRespuestas (IdPregunta, RespuestaEmpleado, IdEmpleado)
		                    SELECT 1, NULL, IdEmpleado from Empleado -- WHERE UNIDAD DE NEGOCIO = 'TURISMO' 
                            */
                            DL.EstatusEncuesta stats = new DL.EstatusEncuesta();
                            stats.Estatus = "No comenzada";
                            stats.IdEncuesta = 1;
                            stats.IdEmpleado = item.IdEmpleado;
                            stats.FechaHoraCreacion = DateTime.Now;
                            stats.UsuarioCreacion = Convert.ToString(Session["AdminLog"]);
                            stats.ProgramaCreacion = "Diagnostic4U";
                            //stats.Anio = ;
                            context.EstatusEncuesta.Add(stats);
                            //Add primer respuesta
                            DL.EmpleadoRespuestas empres = new DL.EmpleadoRespuestas();
                            empres.IdPregunta = 1;
                            empres.RespuestaEmpleado = "";
                            empres.IdEmpleado = item.IdEmpleado;
                            context.EmpleadoRespuestas.Add(empres);
                        }

                        context.SaveChanges();
                        transaction.Commit();
                        result.Correct = true;
                        Session["LogError"] = 10;
                        return Json("success");
                    }
                    catch (Exception ex)
                    {
                        result.Correct = false;
                        result.ErrorMessage = ex.Message;
                        transaction.Rollback();
                        Session["LogError"] = 3;
                        return Json("ERROR");
                    }
                }
            }
            return Redirect(Request.UrlReferrer.ToString());
        }



        //EMplado
        public ActionResult GetEmpleadoById(ML.Empleado empleado)
        {
            var result = BL.Usuario.GetByIdEmpleado(empleado.IdEmpleado);

            ML.Empleado emp = new ML.Empleado();
            emp.EstatusEncuesta = new ML.EstatusEncuesta();

            emp.Perfil = new ML.Perfil();
            emp.BaseDeDatos = new ML.BasesDeDatos();

            emp.BaseDeDatos.Nombre = ((ML.Empleado)result.Object).BaseDeDatos.Nombre;

            emp.IdEmpleado = ((ML.Empleado)result.Object).IdEmpleado;
            emp.Nombre = ((ML.Empleado)result.Object).Nombre;
            emp.ApellidoPaterno = ((ML.Empleado)result.Object).ApellidoPaterno;
            emp.ApellidoMaterno = ((ML.Empleado)result.Object).ApellidoMaterno;
            //emp.Username = ((ML.Empleado)result.Object).Username;
            //emp.Password = ((ML.Empleado)result.Object).Password;
            emp.IdEmpleado = ((ML.Empleado)result.Object).IdEmpleado;

            emp.Puesto = ((ML.Empleado)result.Object).Puesto;
            DateTime nacimiento = ((ML.Empleado)result.Object).FechaNaciemiento;
            emp.dateNacim = nacimiento.ToString("yyyy-MM-dd");

            DateTime antiguedad = ((ML.Empleado)result.Object).FechaAntiguedad;
            emp.dateAntig = antiguedad.ToString("yyyy-MM-dd");


            emp.Sexo = ((ML.Empleado)result.Object).Sexo;
            emp.Correo = ((ML.Empleado)result.Object).Correo;
            emp.TipoFuncion = ((ML.Empleado)result.Object).TipoFuncion;
            emp.CondicionTrabajo = ((ML.Empleado)result.Object).CondicionTrabajo;
            emp.GradoAcademico = ((ML.Empleado)result.Object).GradoAcademico;
            emp.UnidadNegocio = ((ML.Empleado)result.Object).UnidadNegocio;
            emp.DivisonMarca = ((ML.Empleado)result.Object).DivisonMarca;
            emp.AreaAgencia = ((ML.Empleado)result.Object).AreaAgencia;
            emp.Depto = ((ML.Empleado)result.Object).Depto;
            emp.Subdepto = ((ML.Empleado)result.Object).Subdepto;
            emp.EmpresaContratante = ((ML.Empleado)result.Object).EmpresaContratante;
            emp.IdResponsableRH = ((ML.Empleado)result.Object).IdResponsableRH;
            emp.NombreResponsableRH = ((ML.Empleado)result.Object).NombreResponsableRH;
            emp.IdJefe = ((ML.Empleado)result.Object).IdJefe;
            emp.NombreJefe = ((ML.Empleado)result.Object).NombreJefe;
            emp.PuestoJefe = ((ML.Empleado)result.Object).PuestoJefe;
            emp.IdRespinsableEstructura = ((ML.Empleado)result.Object).IdRespinsableEstructura;
            emp.NombreResponsableEstrucutra = ((ML.Empleado)result.Object).NombreResponsableEstrucutra;

            emp.ClavesAcceso = new ML.ClavesAcceso();
            emp.ClavesAcceso.ClaveAcceso = ((ML.Empleado)result.Object).ClavesAcceso.ClaveAcceso;

            emp.RangoAntiguedad = ((ML.Empleado)result.Object).RangoAntiguedad;
            emp.RangoEdad = ((ML.Empleado)result.Object).RangoEdad;

            emp.EstatusEmpleado = ((ML.Empleado)result.Object).EstatusEmpleado;

            

            return Json(emp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateOneEmpleado(ML.Empleado user)
        {
            var result = BL.Usuario.ActualizarEmpleado(user);

            if (result.Correct == true)
            {
                return Json("success");
            }
            else
            {
                return Json("error");
            }
        }

        public ActionResult OverwriteDatabaseUsuarios(FormCollection formCollection)
        {
            List<int> IdUsuarios = new List<int>();
            int lastInsertBD = Convert.ToInt32(Session["IdBD"]);
            var userList = new List<DL.Usuario>();
            var newUsersList = new List<DL.Usuario>();
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["postedFileOverride_"];
                if ((file != null) && (file.ContentLength > 0) && (!string.IsNullOrEmpty(file.FileName)))
                {
                    string fileName = file.FileName;
                    string fileExtension = Path.GetExtension(file.FileName);
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));

                    if (fileExtension == ".xlsx")
                    {
                        using (var package = new ExcelPackage(file.InputStream))
                        {
                            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                            var currentSheet = package.Workbook.Worksheets;
                            var workSheet = currentSheet.First();
                            var noOfCol = workSheet.Dimension.End.Column;
                            var noOfRow = workSheet.Dimension.End.Row;

                            if (noOfRow > 2)
                            {
                                for (int rowIterator = 3; rowIterator <= noOfRow; rowIterator++)
                                {
                                    int ID_ESTATUS;
                                    //Usuarios ya existentes que traen IdUsuario y Estatus Activo
                                    if ( ((workSheet.Cells[rowIterator, 2].Value != null) && (workSheet.Cells[rowIterator, 31].Value.ToString()) == "Activo") || ((workSheet.Cells[rowIterator, 2].Value != null) && (workSheet.Cells[rowIterator, 31].Value.ToString()) == "ACTIVO") )
                                    {
                                        ID_ESTATUS = 1;
                                        var usuario = new DL.Usuario();
                                        usuario.IdUsuario = Convert.ToInt32(workSheet.Cells[rowIterator, 2].Value);
                                        IdUsuarios.Add(usuario.IdUsuario);
                                        if (workSheet.Cells[rowIterator, 3].Value != null)
                                        {
                                            usuario.Password = (workSheet.Cells[rowIterator, 3].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.Password = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 4].Value != null)
                                        {
                                            usuario.Nombre = (workSheet.Cells[rowIterator, 4].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.Nombre = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 5].Value != null)
                                        {
                                            usuario.ApellidoPaterno = (workSheet.Cells[rowIterator, 5].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.ApellidoPaterno = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 6].Value != null)
                                        {
                                            usuario.ApellidoMaterno = (workSheet.Cells[rowIterator, 6].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.ApellidoMaterno = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 7].Value != null)
                                        {
                                            usuario.Puesto = (workSheet.Cells[rowIterator, 7].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.Puesto = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 8].Value != null)
                                        {
                                            usuario.FechaNacimiento = Convert.ToDateTime(workSheet.Cells[rowIterator, 8].Value.ToString());
                                        }
                                        else
                                        {
                                            usuario.FechaNacimiento = Convert.ToDateTime("01/01/1800");
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 9].Value != null)
                                        {
                                            usuario.FechaAntiguedad = Convert.ToDateTime(workSheet.Cells[rowIterator, 9].Value.ToString());
                                        }
                                        else
                                        {
                                            usuario.FechaAntiguedad = Convert.ToDateTime("01/01/1800");
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 10].Value != null)
                                        {
                                            usuario.Sexo = (workSheet.Cells[rowIterator, 10].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.Sexo = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 11].Value != null)
                                        {
                                            usuario.Email = (workSheet.Cells[rowIterator, 11].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.Email = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 12].Value != null)
                                        {
                                            usuario.TipoFuncion = (workSheet.Cells[rowIterator, 12].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.TipoFuncion = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 13].Value != null)
                                        {
                                            usuario.CondicionTrabajo = (workSheet.Cells[rowIterator, 13].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.CondicionTrabajo = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 14].Value != null)
                                        {
                                            usuario.GradoAcademico = (workSheet.Cells[rowIterator, 14].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.GradoAcademico = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 15].Value != null)
                                        {
                                            usuario.UnidadNegocio = (workSheet.Cells[rowIterator, 15].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.UnidadNegocio = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 16].Value != null)
                                        {
                                            usuario.DivisionMarca = (workSheet.Cells[rowIterator, 16].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.DivisionMarca = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 17].Value != null)
                                        {
                                            usuario.AreaAgencia = (workSheet.Cells[rowIterator, 17].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.AreaAgencia = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 18].Value != null)
                                        {
                                            usuario.Departamento = (workSheet.Cells[rowIterator, 18].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.Departamento = "";
                                        }
                                        // 
                                        if (workSheet.Cells[rowIterator, 19].Value != null)
                                        {
                                            usuario.Subdepartamento = (workSheet.Cells[rowIterator, 19].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.Subdepartamento = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 20].Value != null)
                                        {
                                            usuario.EmpresaContratante = (workSheet.Cells[rowIterator, 20].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.EmpresaContratante = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 21].Value != null)
                                        {
                                            usuario.IdResponsableRH = Convert.ToInt32(workSheet.Cells[rowIterator, 21].Value);
                                        }
                                        else
                                        {
                                            usuario.IdResponsableRH = 0;
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 22].Value != null)
                                        {
                                            usuario.NombreResponsableRH = (workSheet.Cells[rowIterator, 22].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.NombreResponsableRH = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 23].Value != null)
                                        {
                                            usuario.IdJefe = Convert.ToInt32((workSheet.Cells[rowIterator, 23].Value));
                                        }
                                        else
                                        {
                                            usuario.IdJefe = 0;
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 24].Value != null)
                                        {
                                            usuario.NombreJefe = (workSheet.Cells[rowIterator, 24].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.NombreJefe = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 25].Value != null)
                                        {
                                            usuario.PuestoJefe = (workSheet.Cells[rowIterator, 25].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.PuestoJefe = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 26].Value != null)
                                        {
                                            usuario.IdResponsableEstructura = Convert.ToInt32((workSheet.Cells[rowIterator, 26].Value));
                                        }
                                        else
                                        {
                                            usuario.IdResponsableEstructura = 0;
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 27].Value != null)
                                        {
                                            usuario.NombreResponsableEstructura = (workSheet.Cells[rowIterator, 27].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.NombreResponsableEstructura = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 3].Value != null)
                                        {
                                            usuario.ClaveAcceso = (workSheet.Cells[rowIterator, 3].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.ClaveAcceso = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 29].Value != null)
                                        {
                                            usuario.RangoAntiguedad = (workSheet.Cells[rowIterator, 29].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.RangoAntiguedad = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 30].Value != null)
                                        {
                                            usuario.RangoEdad = (workSheet.Cells[rowIterator, 30].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.RangoEdad = "";
                                        }
                                        //
                                        //
                                        if (workSheet.Cells[rowIterator, 32].Value != null)
                                        {
                                            usuario.CampoNumerico_1 = Convert.ToInt32(workSheet.Cells[rowIterator, 32].Value);
                                        }
                                        else
                                        {
                                            usuario.CampoNumerico_1 = 0;
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 33].Value != null)
                                        {
                                            usuario.CampoDeTexto_1 = (workSheet.Cells[rowIterator, 33].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.CampoDeTexto_1 = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 34].Value != null)
                                        {
                                            usuario.CampoNumerico_2 = Convert.ToInt32(workSheet.Cells[rowIterator, 34].Value);
                                        }
                                        else
                                        {
                                            usuario.CampoNumerico_2 = 0;
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 35].Value != null)
                                        {
                                            usuario.CampoDeTexto_2 = (workSheet.Cells[rowIterator, 35].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.CampoDeTexto_2 = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 36].Value != null)
                                        {
                                            usuario.CampoNumerico_3 = Convert.ToInt32(workSheet.Cells[rowIterator, 36].Value);
                                        }
                                        else
                                        {
                                            usuario.CampoNumerico_3 = 0;
                                        }
                                        //
                                        if ((workSheet.Cells[rowIterator, 37].Value != null))
                                        {
                                            usuario.CampoDeTexto_3 = (workSheet.Cells[rowIterator, 37].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.CampoDeTexto_3 = "";
                                        }

                                        usuario.IdEstatus = ID_ESTATUS;

                                        usuario.IdBaseDeDatos = lastInsertBD;
                                        usuario.FechaHoraModificacion = DateTime.Now;
                                        usuario.UsuarioModificacion = Convert.ToString(Session["AdminLog"]);
                                        usuario.ProgramaModificacion = "DIagnostic4U";
                                        userList.Add(usuario);


                                    }
                                    //Usuarios existentes con su Id de usuario y estatus Inactivo
                                    else if (   ((workSheet.Cells[rowIterator, 2].Value != null) && (workSheet.Cells[rowIterator, 31].Value.ToString()) == "Inactivo") || ((workSheet.Cells[rowIterator, 2].Value != null) && (workSheet.Cells[rowIterator, 31].Value.ToString()) == "INACTIVO")  )
                                    {
                                        ID_ESTATUS = 2;
                                        var usuario = new DL.Usuario();
                                        usuario.IdUsuario = Convert.ToInt32(workSheet.Cells[rowIterator, 2].Value);

                                        usuario.IdUsuario = Convert.ToInt32(workSheet.Cells[rowIterator, 2].Value);
                                        IdUsuarios.Add(usuario.IdUsuario);
                                        if (workSheet.Cells[rowIterator, 3].Value != null)
                                        {
                                            usuario.Password = (workSheet.Cells[rowIterator, 3].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.Password = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 4].Value != null)
                                        {
                                            usuario.Nombre = (workSheet.Cells[rowIterator, 4].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.Nombre = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 5].Value != null)
                                        {
                                            usuario.ApellidoPaterno = (workSheet.Cells[rowIterator, 5].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.ApellidoPaterno = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 6].Value != null)
                                        {
                                            usuario.ApellidoMaterno = (workSheet.Cells[rowIterator, 6].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.ApellidoMaterno = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 7].Value != null)
                                        {
                                            usuario.Puesto = (workSheet.Cells[rowIterator, 7].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.Puesto = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 8].Value != null)
                                        {
                                            usuario.FechaNacimiento = Convert.ToDateTime(workSheet.Cells[rowIterator, 8].Value.ToString());
                                        }
                                        else
                                        {
                                            usuario.FechaNacimiento = Convert.ToDateTime("01/01/1800");
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 9].Value != null)
                                        {
                                            usuario.FechaAntiguedad = Convert.ToDateTime(workSheet.Cells[rowIterator, 9].Value.ToString());
                                        }
                                        else
                                        {
                                            usuario.FechaAntiguedad = Convert.ToDateTime("01/01/1800");
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 10].Value != null)
                                        {
                                            usuario.Sexo = (workSheet.Cells[rowIterator, 10].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.Sexo = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 11].Value != null)
                                        {
                                            usuario.Email = (workSheet.Cells[rowIterator, 11].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.Email = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 12].Value != null)
                                        {
                                            usuario.TipoFuncion = (workSheet.Cells[rowIterator, 12].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.TipoFuncion = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 13].Value != null)
                                        {
                                            usuario.CondicionTrabajo = (workSheet.Cells[rowIterator, 13].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.CondicionTrabajo = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 14].Value != null)
                                        {
                                            usuario.GradoAcademico = (workSheet.Cells[rowIterator, 14].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.GradoAcademico = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 15].Value != null)
                                        {
                                            usuario.UnidadNegocio = (workSheet.Cells[rowIterator, 15].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.UnidadNegocio = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 16].Value != null)
                                        {
                                            usuario.DivisionMarca = (workSheet.Cells[rowIterator, 16].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.DivisionMarca = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 17].Value != null)
                                        {
                                            usuario.AreaAgencia = (workSheet.Cells[rowIterator, 17].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.AreaAgencia = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 18].Value != null)
                                        {
                                            usuario.Departamento = (workSheet.Cells[rowIterator, 18].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.Departamento = "";
                                        }
                                        // 
                                        if (workSheet.Cells[rowIterator, 19].Value != null)
                                        {
                                            usuario.Subdepartamento = (workSheet.Cells[rowIterator, 19].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.Subdepartamento = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 20].Value != null)
                                        {
                                            usuario.EmpresaContratante = (workSheet.Cells[rowIterator, 20].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.EmpresaContratante = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 21].Value != null)
                                        {
                                            usuario.IdResponsableRH = Convert.ToInt32(workSheet.Cells[rowIterator, 21].Value);
                                        }
                                        else
                                        {
                                            usuario.IdResponsableRH = 0;
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 22].Value != null)
                                        {
                                            usuario.NombreResponsableRH = (workSheet.Cells[rowIterator, 22].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.NombreResponsableRH = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 23].Value != null)
                                        {
                                            usuario.IdJefe = Convert.ToInt32((workSheet.Cells[rowIterator, 23].Value));
                                        }
                                        else
                                        {
                                            usuario.IdJefe = 0;
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 24].Value != null)
                                        {
                                            usuario.NombreJefe = (workSheet.Cells[rowIterator, 24].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.NombreJefe = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 25].Value != null)
                                        {
                                            usuario.PuestoJefe = (workSheet.Cells[rowIterator, 25].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.PuestoJefe = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 26].Value != null)
                                        {
                                            usuario.IdResponsableEstructura = Convert.ToInt32((workSheet.Cells[rowIterator, 26].Value));
                                        }
                                        else
                                        {
                                            usuario.IdResponsableEstructura = 0;
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 27].Value != null)
                                        {
                                            usuario.NombreResponsableEstructura = (workSheet.Cells[rowIterator, 27].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.NombreResponsableEstructura = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 3].Value != null)
                                        {
                                            usuario.ClaveAcceso = (workSheet.Cells[rowIterator, 3].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.ClaveAcceso = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 29].Value != null)
                                        {
                                            usuario.RangoAntiguedad = (workSheet.Cells[rowIterator, 29].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.RangoAntiguedad = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 30].Value != null)
                                        {
                                            usuario.RangoEdad = (workSheet.Cells[rowIterator, 30].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.RangoEdad = "";
                                        }
                                        //
                                        //
                                        if (workSheet.Cells[rowIterator, 32].Value != null)
                                        {
                                            usuario.CampoNumerico_1 = Convert.ToInt32(workSheet.Cells[rowIterator, 32].Value);
                                        }
                                        else
                                        {
                                            usuario.CampoNumerico_1 = 0;
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 33].Value != null)
                                        {
                                            usuario.CampoDeTexto_1 = (workSheet.Cells[rowIterator, 33].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.CampoDeTexto_1 = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 34].Value != null)
                                        {
                                            usuario.CampoNumerico_2 = Convert.ToInt32(workSheet.Cells[rowIterator, 34].Value);
                                        }
                                        else
                                        {
                                            usuario.CampoNumerico_2 = 0;
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 35].Value != null)
                                        {
                                            usuario.CampoDeTexto_2 = (workSheet.Cells[rowIterator, 35].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.CampoDeTexto_2 = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 36].Value != null)
                                        {
                                            usuario.CampoNumerico_3 = Convert.ToInt32(workSheet.Cells[rowIterator, 36].Value);
                                        }
                                        else
                                        {
                                            usuario.CampoNumerico_3 = 0;
                                        }
                                        //
                                        if ((workSheet.Cells[rowIterator, 37].Value != null))
                                        {
                                            usuario.CampoDeTexto_3 = (workSheet.Cells[rowIterator, 37].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.CampoDeTexto_3 = "";
                                        }

                                        usuario.IdEstatus = ID_ESTATUS;

                                        usuario.IdBaseDeDatos = lastInsertBD;
                                        usuario.FechaHoraModificacion = DateTime.Now;
                                        usuario.UsuarioModificacion = Convert.ToString(Session["AdminLog"]);
                                        usuario.ProgramaModificacion = "DIagnostic4U";
                                        userList.Add(usuario);
                                    }
                                    //Usuarios por agregar a la BD
                                    else if ((workSheet.Cells[rowIterator, 2].Value == null))
                                    {
                                        ID_ESTATUS = 1;
                                        var usuario = new DL.Usuario();
                                        //usuario.IdUsuario = Convert.ToInt32(workSheet.Cells[rowIterator, 2].Value);
                                        //IdUsuarios.Add(usuario.IdUsuario);
                                        if (workSheet.Cells[rowIterator, 3].Value != null)
                                        {
                                            usuario.Password = (workSheet.Cells[rowIterator, 3].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.Password = BL.GeneratorPass.GetUniqueKey(8);
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 4].Value != null)
                                        {
                                            usuario.Nombre = (workSheet.Cells[rowIterator, 4].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.Nombre = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 5].Value != null)
                                        {
                                            usuario.ApellidoPaterno = (workSheet.Cells[rowIterator, 5].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.ApellidoPaterno = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 6].Value != null)
                                        {
                                            usuario.ApellidoMaterno = (workSheet.Cells[rowIterator, 6].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.ApellidoMaterno = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 7].Value != null)
                                        {
                                            usuario.Puesto = (workSheet.Cells[rowIterator, 7].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.Puesto = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 8].Value != null)
                                        {
                                            usuario.FechaNacimiento = Convert.ToDateTime(workSheet.Cells[rowIterator, 8].Value.ToString());
                                        }
                                        else
                                        {
                                            usuario.FechaNacimiento = Convert.ToDateTime("01/01/1800");
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 9].Value != null)
                                        {
                                            usuario.FechaAntiguedad = Convert.ToDateTime(workSheet.Cells[rowIterator, 9].Value.ToString());
                                        }
                                        else
                                        {
                                            usuario.FechaAntiguedad = Convert.ToDateTime("01/01/1800");
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 10].Value != null)
                                        {
                                            usuario.Sexo = (workSheet.Cells[rowIterator, 10].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.Sexo = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 11].Value != null)
                                        {
                                            usuario.Email = (workSheet.Cells[rowIterator, 11].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.Email = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 12].Value != null)
                                        {
                                            usuario.TipoFuncion = (workSheet.Cells[rowIterator, 12].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.TipoFuncion = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 13].Value != null)
                                        {
                                            usuario.CondicionTrabajo = (workSheet.Cells[rowIterator, 13].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.CondicionTrabajo = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 14].Value != null)
                                        {
                                            usuario.GradoAcademico = (workSheet.Cells[rowIterator, 14].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.GradoAcademico = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 15].Value != null)
                                        {
                                            usuario.UnidadNegocio = (workSheet.Cells[rowIterator, 15].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.UnidadNegocio = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 16].Value != null)
                                        {
                                            usuario.DivisionMarca = (workSheet.Cells[rowIterator, 16].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.DivisionMarca = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 17].Value != null)
                                        {
                                            usuario.AreaAgencia = (workSheet.Cells[rowIterator, 17].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.AreaAgencia = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 18].Value != null)
                                        {
                                            usuario.Departamento = (workSheet.Cells[rowIterator, 18].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.Departamento = "";
                                        }
                                        // 
                                        if (workSheet.Cells[rowIterator, 19].Value != null)
                                        {
                                            usuario.Subdepartamento = (workSheet.Cells[rowIterator, 19].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.Subdepartamento = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 20].Value != null)
                                        {
                                            usuario.EmpresaContratante = (workSheet.Cells[rowIterator, 20].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.EmpresaContratante = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 21].Value != null)
                                        {
                                            usuario.IdResponsableRH = Convert.ToInt32(workSheet.Cells[rowIterator, 21].Value);
                                        }
                                        else
                                        {
                                            usuario.IdResponsableRH = 0;
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 22].Value != null)
                                        {
                                            usuario.NombreResponsableRH = (workSheet.Cells[rowIterator, 22].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.NombreResponsableRH = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 23].Value != null)
                                        {
                                            usuario.IdJefe = Convert.ToInt32((workSheet.Cells[rowIterator, 23].Value));
                                        }
                                        else
                                        {
                                            usuario.IdJefe = 0;
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 24].Value != null)
                                        {
                                            usuario.NombreJefe = (workSheet.Cells[rowIterator, 24].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.NombreJefe = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 25].Value != null)
                                        {
                                            usuario.PuestoJefe = (workSheet.Cells[rowIterator, 25].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.PuestoJefe = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 26].Value != null)
                                        {
                                            usuario.IdResponsableEstructura = Convert.ToInt32((workSheet.Cells[rowIterator, 26].Value));
                                        }
                                        else
                                        {
                                            usuario.IdResponsableEstructura = 0;
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 27].Value != null)
                                        {
                                            usuario.NombreResponsableEstructura = (workSheet.Cells[rowIterator, 27].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.NombreResponsableEstructura = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 3].Value != null)
                                        {
                                            usuario.ClaveAcceso = (workSheet.Cells[rowIterator, 3].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.ClaveAcceso = BL.GeneratorPass.GetUniqueKey(8);
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 29].Value != null)
                                        {
                                            usuario.RangoAntiguedad = (workSheet.Cells[rowIterator, 29].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.RangoAntiguedad = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 30].Value != null)
                                        {
                                            usuario.RangoEdad = (workSheet.Cells[rowIterator, 30].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.RangoEdad = "";
                                        }
                                        //
                                        //
                                        if (workSheet.Cells[rowIterator, 32].Value != null)
                                        {
                                            usuario.CampoNumerico_1 = Convert.ToInt32(workSheet.Cells[rowIterator, 32].Value);
                                        }
                                        else
                                        {
                                            usuario.CampoNumerico_1 = 0;
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 33].Value != null)
                                        {
                                            usuario.CampoDeTexto_1 = (workSheet.Cells[rowIterator, 33].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.CampoDeTexto_1 = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 34].Value != null)
                                        {
                                            usuario.CampoNumerico_2 = Convert.ToInt32(workSheet.Cells[rowIterator, 34].Value);
                                        }
                                        else
                                        {
                                            usuario.CampoNumerico_2 = 0;
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 35].Value != null)
                                        {
                                            usuario.CampoDeTexto_2 = (workSheet.Cells[rowIterator, 35].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.CampoDeTexto_2 = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 36].Value != null)
                                        {
                                            usuario.CampoNumerico_3 = Convert.ToInt32(workSheet.Cells[rowIterator, 36].Value);
                                        }
                                        else
                                        {
                                            usuario.CampoNumerico_3 = 0;
                                        }
                                        //
                                        if ((workSheet.Cells[rowIterator, 37].Value != null))
                                        {
                                            usuario.CampoDeTexto_3 = (workSheet.Cells[rowIterator, 37].Value).ToString();
                                        }
                                        else
                                        {
                                            usuario.CampoDeTexto_3 = "";
                                        }

                                        usuario.IdEstatus = ID_ESTATUS;

                                        usuario.IdBaseDeDatos = lastInsertBD;
                                        usuario.FechaHoraCreacion = DateTime.Now;
                                        usuario.UsuarioCreacion = Convert.ToString(Session["AdminLog"]);
                                        usuario.ProgramaCreacion = "DIagnostic4U";
                                        newUsersList.Add(usuario);
                                    }


                                }
                            }
                            else
                            {
                                Session["LogError"] = 6;
                                return Json("ERROR", JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    else
                    {
                        //Formato no valido
                        Session["LogError"] = 1;
                        return Json("ERROR", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    //No eligio archivo
                    Session["LogError"] = 5;
                    return Json("ERROR", JsonRequestBehavior.AllowGet);
                }
            }
            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                ML.Result result = new ML.Result();
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        List<int> newsIdUsr = new List<int>();
                        foreach (var item in userList)
                        {
                            BL.Usuario.Update(item, context, transaction);//Update existentes
                        }
                        foreach (var item in newUsersList)
                        {
                            context.Usuario.Add(item);//Agregar nuevos
                            context.SaveChanges();
                            var getEncuestasByBD = context.Encuesta.SqlQuery("SELECT * FROM ENCUESTA WHERE IdBasesDeDatos = {0}", item.IdBaseDeDatos);
                            foreach (var ELEM in getEncuestasByBD)
                            {
                                var Insert = context.Database.ExecuteSqlCommand("INSERT INTO UsuarioEstatusEncuesta (IdUsuario, IdEncuesta, IdEstatusEncuestaD4U, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) values ({0}, {1}, {2}, {3}, {4}, {5})",
                                                item.IdUsuario, ELEM.IdEncuesta, 1, DateTime.Now, "OverWriteBD", "Diagnostic4U");
                            }
                            var idusr = context.Usuario.Max(m => m.IdUsuario);
                            newsIdUsr.Add(idusr);
                        }
                        var getAllUsr = context.Usuario.SqlQuery("SELECT * FROM USUARIO WHERE IdBaseDeDatos = {0}", lastInsertBD).Select(m => m.IdUsuario).ToList();
                        foreach (var item in getAllUsr)
                        {
                            if (!IdUsuarios.Contains(item) && !newsIdUsr.Contains(item))
                            {//Eliminar los que no vengan en la lista
                                var query = context.Database.ExecuteSqlCommand("UPDATE USUARIO SET IDESTATUS = 6 WHERE IDUSUARIO = {0} AND IdBaseDeDatos = {1}", item, lastInsertBD);
                            }
                        }
                        context.SaveChanges();
                        transaction.Commit();
                        result.Correct = true;
                        Session["LogError"] = 10;
                        return Json("success", JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception ex)
                    {
                        result.Correct = false;
                        result.ErrorMessage = ex.Message;
                        transaction.Rollback();
                        Session["LogError"] = 3;
                        return Json("ERROR", JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        //
        public ActionResult OverwriteDatabaseEmpleado(FormCollection formCollection)
        {
            List<int> IdUsuarios = new List<int>();
            int lastInsertBD = Convert.ToInt32(Session["IdBD"]);
            var empList = new List<DL.Empleado>();
            var newUsersList = new List<DL.Empleado>();
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["postedFileOverride_"];
                if ((file != null) && (file.ContentLength > 0) && (!string.IsNullOrEmpty(file.FileName)))
                {
                    string fileName = file.FileName;
                    string fileExtension = Path.GetExtension(file.FileName);
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));

                    if (fileExtension == ".xlsx")
                    {
                        using (var package = new ExcelPackage(file.InputStream))
                        {
                            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                            var currentSheet = package.Workbook.Worksheets;
                            var workSheet = currentSheet.First();
                            var noOfCol = workSheet.Dimension.End.Column;
                            var noOfRow = workSheet.Dimension.End.Row;

                            if (noOfRow > 2)
                            {
                                for (int rowIterator = 3; rowIterator <= noOfRow; rowIterator++)
                                {
                                    int ID_ESTATUS;
                                    //Usuarios ya existentes que traen IdUsuario y Estatus Activo
                                    if (((workSheet.Cells[rowIterator, 2].Value != null) && (workSheet.Cells[rowIterator, 32].Value.ToString()) == "Activo") || ((workSheet.Cells[rowIterator, 2].Value != null) && (workSheet.Cells[rowIterator, 32].Value.ToString()) == "ACTIVO"))
                                    {
                                        ID_ESTATUS = 1;
                                        var empleado = new DL.Empleado();
                                        empleado.IdEmpleado = Convert.ToInt32(workSheet.Cells[rowIterator, 2].Value);
                                        IdUsuarios.Add(empleado.IdEmpleado);
                                        if (workSheet.Cells[rowIterator, 3].Value != null)
                                        {
                                            empleado.IdEmpleadoRH = Convert.ToInt32(workSheet.Cells[rowIterator, 3].Value);
                                        }
                                        else
                                        {
                                            empleado.IdEmpleadoRH = 0;
                                        }



                                        if (workSheet.Cells[rowIterator, 4].Value != null)
                                        {
                                            empleado.ClaveAcceso = (workSheet.Cells[rowIterator, 4].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.ClaveAcceso = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 5].Value != null)
                                        {
                                            empleado.Nombre = (workSheet.Cells[rowIterator, 5].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.Nombre = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 6].Value != null)
                                        {
                                            empleado.ApellidoPaterno = (workSheet.Cells[rowIterator, 6].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.ApellidoPaterno = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 7].Value != null)
                                        {
                                            empleado.ApellidoMaterno = (workSheet.Cells[rowIterator, 7].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.ApellidoMaterno = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 8].Value != null)
                                        {
                                            empleado.Puesto = (workSheet.Cells[rowIterator, 8].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.Puesto = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 9].Value != null)
                                        {
                                            empleado.FechaNacimiento = Convert.ToDateTime(workSheet.Cells[rowIterator, 9].Value.ToString());
                                        }
                                        else
                                        {
                                            empleado.FechaNacimiento = Convert.ToDateTime("01/01/1800");
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 10].Value != null)
                                        {
                                            empleado.FechaAntiguedad = Convert.ToDateTime(workSheet.Cells[rowIterator, 10].Value.ToString());
                                        }
                                        else
                                        {
                                            empleado.FechaAntiguedad = Convert.ToDateTime("01/01/1800");
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 11].Value != null)
                                        {
                                            empleado.Sexo = (workSheet.Cells[rowIterator, 11].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.Sexo = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 12].Value != null)
                                        {
                                            empleado.Correo = (workSheet.Cells[rowIterator, 12].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.Correo = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 13].Value != null)
                                        {
                                            empleado.TipoFuncion = (workSheet.Cells[rowIterator, 13].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.TipoFuncion = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 14].Value != null)
                                        {
                                            empleado.CondicionTrabajo = (workSheet.Cells[rowIterator, 14].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.CondicionTrabajo = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 15].Value != null)
                                        {
                                            empleado.GradoAcademico = (workSheet.Cells[rowIterator, 15].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.GradoAcademico = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 16].Value != null)
                                        {
                                            empleado.UnidadNegocio = (workSheet.Cells[rowIterator, 16].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.UnidadNegocio = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 17].Value != null)
                                        {
                                            empleado.DivisionMarca = (workSheet.Cells[rowIterator, 17].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.DivisionMarca = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 18].Value != null)
                                        {
                                            empleado.AreaAgencia = (workSheet.Cells[rowIterator, 18].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.AreaAgencia = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 19].Value != null)
                                        {
                                            empleado.Depto = (workSheet.Cells[rowIterator, 19].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.Depto = "";
                                        }
                                        // 
                                        if (workSheet.Cells[rowIterator, 20].Value != null)
                                        {
                                            empleado.Subdepartamento = (workSheet.Cells[rowIterator, 20].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.Subdepartamento = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 21].Value != null)
                                        {
                                            empleado.EmpresaContratante = (workSheet.Cells[rowIterator, 21].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.EmpresaContratante = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 22].Value != null)
                                        {
                                            empleado.IdResponsableRH = Convert.ToInt32(workSheet.Cells[rowIterator, 22].Value);
                                        }
                                        else
                                        {
                                            empleado.IdResponsableRH = 0;
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 23].Value != null)
                                        {
                                            empleado.NombreResponsableRH = (workSheet.Cells[rowIterator, 23].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.NombreResponsableRH = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 24].Value != null)
                                        {
                                            empleado.IdJefe = Convert.ToInt32((workSheet.Cells[rowIterator, 24].Value));
                                        }
                                        else
                                        {
                                            empleado.IdJefe = 0;
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 25].Value != null)
                                        {
                                            empleado.NombreJefe = (workSheet.Cells[rowIterator, 25].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.NombreJefe = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 26].Value != null)
                                        {
                                            empleado.PuestoJefe = (workSheet.Cells[rowIterator, 26].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.PuestoJefe = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 27].Value != null)
                                        {
                                            empleado.IdResponsableEstructura = Convert.ToInt32((workSheet.Cells[rowIterator, 27].Value));
                                        }
                                        else
                                        {
                                            empleado.IdResponsableEstructura = 0;
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 28].Value != null)
                                        {
                                            empleado.NombreResponsableEstructura = (workSheet.Cells[rowIterator, 28].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.NombreResponsableEstructura = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 4].Value != null)
                                        {
                                            empleado.ClaveAcceso = (workSheet.Cells[rowIterator, 4].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.ClaveAcceso = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 30].Value != null)
                                        {
                                            empleado.RangoAntiguedad = (workSheet.Cells[rowIterator, 30].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.RangoAntiguedad = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 31].Value != null)
                                        {
                                            empleado.RangoEdad = (workSheet.Cells[rowIterator, 31].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.RangoEdad = "";
                                        }
                                        //
                                        //
                                        

                                        empleado.EstatusEmpleado = "Activo";

                                        empleado.IdBaseDeDatos = lastInsertBD;
                                        empleado.FechaHoraModificacion = DateTime.Now;
                                        empleado.UsuarioModificacion = Convert.ToString(Session["AdminLog"]);
                                        empleado.ProgramaModificacion = "DIagnostic4U";
                                        empList.Add(empleado);


                                    }
                                    //Usuarios existentes con su Id de usuario y estatus Inactivo
                                    else if (((workSheet.Cells[rowIterator, 2].Value != null) && (workSheet.Cells[rowIterator, 32].Value.ToString()) == "Inactivo") || ((workSheet.Cells[rowIterator, 2].Value != null) && (workSheet.Cells[rowIterator, 32].Value.ToString()) == "INACTIVO"))
                                    {
                                        ID_ESTATUS = 2;
                                        var empleado = new DL.Empleado();
                                        empleado.IdEmpleado = Convert.ToInt32(workSheet.Cells[rowIterator, 2].Value);

                                        empleado.IdEmpleado = Convert.ToInt32(workSheet.Cells[rowIterator, 2].Value);
                                        IdUsuarios.Add(empleado.IdEmpleado);

                                        if (workSheet.Cells[rowIterator, 3].Value != null)
                                        {
                                            empleado.IdEmpleadoRH = Convert.ToInt32(workSheet.Cells[rowIterator, 3].Value);
                                        }
                                        else
                                        {
                                            empleado.IdEmpleadoRH = 0;
                                        }


                                        if (workSheet.Cells[rowIterator, 4].Value != null)
                                        {
                                            empleado.ClaveAcceso = (workSheet.Cells[rowIterator, 4].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.ClaveAcceso = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 5].Value != null)
                                        {
                                            empleado.Nombre = (workSheet.Cells[rowIterator, 5].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.Nombre = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 6].Value != null)
                                        {
                                            empleado.ApellidoPaterno = (workSheet.Cells[rowIterator, 6].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.ApellidoPaterno = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 7].Value != null)
                                        {
                                            empleado.ApellidoMaterno = (workSheet.Cells[rowIterator, 7].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.ApellidoMaterno = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 8].Value != null)
                                        {
                                            empleado.Puesto = (workSheet.Cells[rowIterator, 8].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.Puesto = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 9].Value != null)
                                        {
                                            empleado.FechaNacimiento = Convert.ToDateTime(workSheet.Cells[rowIterator, 9].Value.ToString());
                                        }
                                        else
                                        {
                                            empleado.FechaNacimiento = Convert.ToDateTime("01/01/1800");
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 10].Value != null)
                                        {
                                            empleado.FechaAntiguedad = Convert.ToDateTime(workSheet.Cells[rowIterator, 10].Value.ToString());
                                        }
                                        else
                                        {
                                            empleado.FechaAntiguedad = Convert.ToDateTime("01/01/1800");
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 11].Value != null)
                                        {
                                            empleado.Sexo = (workSheet.Cells[rowIterator, 11].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.Sexo = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 12].Value != null)
                                        {
                                            empleado.Correo = (workSheet.Cells[rowIterator, 12].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.Correo = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 13].Value != null)
                                        {
                                            empleado.TipoFuncion = (workSheet.Cells[rowIterator, 13].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.TipoFuncion = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 14].Value != null)
                                        {
                                            empleado.CondicionTrabajo = (workSheet.Cells[rowIterator, 14].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.CondicionTrabajo = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 15].Value != null)
                                        {
                                            empleado.GradoAcademico = (workSheet.Cells[rowIterator, 15].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.GradoAcademico = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 16].Value != null)
                                        {
                                            empleado.UnidadNegocio = (workSheet.Cells[rowIterator, 16].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.UnidadNegocio = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 17].Value != null)
                                        {
                                            empleado.DivisionMarca = (workSheet.Cells[rowIterator, 17].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.DivisionMarca = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 18].Value != null)
                                        {
                                            empleado.AreaAgencia = (workSheet.Cells[rowIterator, 18].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.AreaAgencia = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 19].Value != null)
                                        {
                                            empleado.Depto = (workSheet.Cells[rowIterator, 19].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.Depto = "";
                                        }
                                        // 
                                        if (workSheet.Cells[rowIterator, 20].Value != null)
                                        {
                                            empleado.Subdepartamento = (workSheet.Cells[rowIterator, 20].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.Subdepartamento = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 21].Value != null)
                                        {
                                            empleado.EmpresaContratante = (workSheet.Cells[rowIterator, 21].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.EmpresaContratante = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 22].Value != null)
                                        {
                                            empleado.IdResponsableRH = Convert.ToInt32(workSheet.Cells[rowIterator, 22].Value);
                                        }
                                        else
                                        {
                                            empleado.IdResponsableRH = 0;
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 23].Value != null)
                                        {
                                            empleado.NombreResponsableRH = (workSheet.Cells[rowIterator, 23].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.NombreResponsableRH = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 24].Value != null)
                                        {
                                            empleado.IdJefe = Convert.ToInt32((workSheet.Cells[rowIterator, 24].Value));
                                        }
                                        else
                                        {
                                            empleado.IdJefe = 0;
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 25].Value != null)
                                        {
                                            empleado.NombreJefe = (workSheet.Cells[rowIterator, 25].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.NombreJefe = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 26].Value != null)
                                        {
                                            empleado.PuestoJefe = (workSheet.Cells[rowIterator, 26].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.PuestoJefe = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 27].Value != null)
                                        {
                                            empleado.IdResponsableEstructura = Convert.ToInt32((workSheet.Cells[rowIterator, 27].Value));
                                        }
                                        else
                                        {
                                            empleado.IdResponsableEstructura = 0;
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 28].Value != null)
                                        {
                                            empleado.NombreResponsableEstructura = (workSheet.Cells[rowIterator, 28].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.NombreResponsableEstructura = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 4].Value != null)
                                        {
                                            empleado.ClaveAcceso = (workSheet.Cells[rowIterator, 4].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.ClaveAcceso = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 30].Value != null)
                                        {
                                            empleado.RangoAntiguedad = (workSheet.Cells[rowIterator, 30].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.RangoAntiguedad = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 31].Value != null)
                                        {
                                            empleado.RangoEdad = (workSheet.Cells[rowIterator, 31].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.RangoEdad = "";
                                        }
                                        //
                                        //

                                        empleado.EstatusEmpleado = "Inactivo";

                                        empleado.IdBaseDeDatos = lastInsertBD;
                                        empleado.FechaHoraModificacion = DateTime.Now;
                                        empleado.UsuarioModificacion = Convert.ToString(Session["AdminLog"]);
                                        empleado.ProgramaModificacion = "DIagnostic4U";
                                        empList.Add(empleado);
                                    }
                                    //Usuarios por agregar a la BD
                                    else if ((workSheet.Cells[rowIterator, 2].Value == null))
                                    {
                                        ID_ESTATUS = 1;
                                        var empleado = new DL.Empleado();
                                        //usuario.IdUsuario = Convert.ToInt32(workSheet.Cells[rowIterator, 2].Value);
                                        if (workSheet.Cells[rowIterator, 3].Value != null)
                                        {
                                            empleado.IdEmpleadoRH = Convert.ToInt32(workSheet.Cells[rowIterator, 3].Value);
                                        }
                                        else
                                        {
                                            empleado.IdEmpleadoRH = 0;
                                        }
                                        //IdUsuarios.Add(usuario.IdUsuario);
                                        if (workSheet.Cells[rowIterator, 4].Value != null)
                                        {
                                            empleado.ClaveAcceso = (workSheet.Cells[rowIterator, 4].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.ClaveAcceso = BL.GeneratorPass.GetUniqueKey(8);
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 5].Value != null)
                                        {
                                            empleado.Nombre = (workSheet.Cells[rowIterator, 5].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.Nombre = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 6].Value != null)
                                        {
                                            empleado.ApellidoPaterno = (workSheet.Cells[rowIterator, 6].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.ApellidoPaterno = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 7].Value != null)
                                        {
                                            empleado.ApellidoMaterno = (workSheet.Cells[rowIterator, 7].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.ApellidoMaterno = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 8].Value != null)
                                        {
                                            empleado.Puesto = (workSheet.Cells[rowIterator, 8].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.Puesto = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 9].Value != null)
                                        {
                                            empleado.FechaNacimiento = Convert.ToDateTime(workSheet.Cells[rowIterator, 9].Value.ToString());
                                        }
                                        else
                                        {
                                            empleado.FechaNacimiento = Convert.ToDateTime("01/01/1800");
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 10].Value != null)
                                        {
                                            empleado.FechaAntiguedad = Convert.ToDateTime(workSheet.Cells[rowIterator, 10].Value.ToString());
                                        }
                                        else
                                        {
                                            empleado.FechaAntiguedad = Convert.ToDateTime("01/01/1800");
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 11].Value != null)
                                        {
                                            empleado.Sexo = (workSheet.Cells[rowIterator, 11].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.Sexo = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 12].Value != null)
                                        {
                                            empleado.Correo = (workSheet.Cells[rowIterator, 12].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.Correo = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 13].Value != null)
                                        {
                                            empleado.TipoFuncion = (workSheet.Cells[rowIterator, 13].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.TipoFuncion = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 14].Value != null)
                                        {
                                            empleado.CondicionTrabajo = (workSheet.Cells[rowIterator, 14].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.CondicionTrabajo = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 15].Value != null)
                                        {
                                            empleado.GradoAcademico = (workSheet.Cells[rowIterator, 15].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.GradoAcademico = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 16].Value != null)
                                        {
                                            empleado.UnidadNegocio = (workSheet.Cells[rowIterator, 16].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.UnidadNegocio = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 17].Value != null)
                                        {
                                            empleado.DivisionMarca = (workSheet.Cells[rowIterator, 17].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.DivisionMarca = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 18].Value != null)
                                        {
                                            empleado.AreaAgencia = (workSheet.Cells[rowIterator, 18].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.AreaAgencia = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 19].Value != null)
                                        {
                                            empleado.Depto = (workSheet.Cells[rowIterator, 19].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.Depto = "";
                                        }
                                        // 
                                        if (workSheet.Cells[rowIterator, 20].Value != null)
                                        {
                                            empleado.Subdepartamento = (workSheet.Cells[rowIterator, 20].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.Subdepartamento = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 21].Value != null)
                                        {
                                            empleado.EmpresaContratante = (workSheet.Cells[rowIterator, 21].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.EmpresaContratante = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 22].Value != null)
                                        {
                                            empleado.IdResponsableRH = Convert.ToInt32(workSheet.Cells[rowIterator, 22].Value);
                                        }
                                        else
                                        {
                                            empleado.IdResponsableRH = 0;
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 23].Value != null)
                                        {
                                            empleado.NombreResponsableRH = (workSheet.Cells[rowIterator, 23].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.NombreResponsableRH = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 24].Value != null)
                                        {
                                            empleado.IdJefe = Convert.ToInt32((workSheet.Cells[rowIterator, 24].Value));
                                        }
                                        else
                                        {
                                            empleado.IdJefe = 0;
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 25].Value != null)
                                        {
                                            empleado.NombreJefe = (workSheet.Cells[rowIterator, 25].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.NombreJefe = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 26].Value != null)
                                        {
                                            empleado.PuestoJefe = (workSheet.Cells[rowIterator, 26].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.PuestoJefe = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 27].Value != null)
                                        {
                                            empleado.IdResponsableEstructura = Convert.ToInt32((workSheet.Cells[rowIterator, 27].Value));
                                        }
                                        else
                                        {
                                            empleado.IdResponsableEstructura = 0;
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 28].Value != null)
                                        {
                                            empleado.NombreResponsableEstructura = (workSheet.Cells[rowIterator, 28].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.NombreResponsableEstructura = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 4].Value != null)
                                        {
                                            empleado.ClaveAcceso = (workSheet.Cells[rowIterator, 4].Value).ToString();
                                        }
                                        else
                                        {
                                            //empleado.ClaveAcceso = empleado.ClaveAcceso;
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 30].Value != null)
                                        {
                                            empleado.RangoAntiguedad = (workSheet.Cells[rowIterator, 30].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.RangoAntiguedad = "";
                                        }
                                        //
                                        if (workSheet.Cells[rowIterator, 31].Value != null)
                                        {
                                            empleado.RangoEdad = (workSheet.Cells[rowIterator, 31].Value).ToString();
                                        }
                                        else
                                        {
                                            empleado.RangoEdad = "";
                                        }
                                        //
                                        //
                                        
                                        empleado.EstatusEmpleado = "Activo";

                                        empleado.IdBaseDeDatos = lastInsertBD;
                                        empleado.FechaHoraCreacion = DateTime.Now;
                                        empleado.UsuarioCreacion = Convert.ToString(Session["AdminLog"]);
                                        empleado.ProgramaCreacion = "DIagnostic4U";
                                        newUsersList.Add(empleado);
                                    }


                                }
                            }
                            else
                            {
                                Session["LogError"] = 6;
                                return Json("ERROR", JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    else
                    {
                        //Formato no valido
                        Session["LogError"] = 1;
                        return Json("ERROR", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    //No eligio archivo
                    Session["LogError"] = 5;
                    return Json("ERROR", JsonRequestBehavior.AllowGet);
                }
            }
            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                ML.Result result = new ML.Result();
                //using (var transaction = context.Database.BeginTransaction())
                //{
                try
                {
                    List<int> newsIdUsr = new List<int>();
                        foreach (var item in empList)
                        {
                            BL.Empleado.Update(item, context);//Update existentes
                        }
                        foreach (var item in newUsersList)
                        {
                            var itemClave = new DL.ClavesAcceso();
                            itemClave.ClaveAcceso = item.ClaveAcceso;
                            var existClave = context.ClavesAcceso.SqlQuery("select * from ClavesAcceso where claveacceso = {0}", item.ClaveAcceso).ToList();
                            if (existClave.Count == 0)
                            {
                                context.ClavesAcceso.Add(itemClave);
                                context.SaveChanges();
                            }
                            item.IdEmpleado = context.Empleado.Max(m => m.IdEmpleado) + 1;
                            context.Empleado.Add(item);//Agregar nuevos
                            newsIdUsr.Add(item.IdEmpleado);
                        //Agregar estatus a climaLabral
                        DL.EstatusEncuesta st = new DL.EstatusEncuesta();
                        st.IdEncuesta = 1;
                        st.IdEmpleado = item.IdEmpleado;
                        st.Estatus = "No comenzada";
                        context.EstatusEncuesta.Add(st);
                        }
                        var getAllUsr = context.Empleado.SqlQuery("SELECT * FROM empleado WHERE IdBaseDeDatos = {0}", lastInsertBD).Select(m => m.IdEmpleado).ToList();
                        foreach (var item in getAllUsr)
                        {
                            if (!IdUsuarios.Contains(item) && !newsIdUsr.Contains(item))
                            {//Eliminar los que no vengan en la lista
                                var query = context.Database.ExecuteSqlCommand("UPDATE empleado SET estatusEmpleado = 'Eliminado' WHERE idempleado = {0} AND IdBaseDeDatos = {1}", item, lastInsertBD);
                            }
                        }
                        context.SaveChanges();
                        //transaction.Commit();
                        result.Correct = true;
                        Session["LogError"] = 10;
                        return Json("success", JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception ex)
                    {
                        result.Correct = false;
                        result.ErrorMessage = ex.Message;
                        //transaction.Rollback();
                        Session["LogError"] = 3;
                        return Json("ERROR", JsonRequestBehavior.AllowGet);
                    }
                //}
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult createExcelLayouy()
        {
            Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel.Workbook worKbooK;
            Microsoft.Office.Interop.Excel.Worksheet worKsheeT;
            Microsoft.Office.Interop.Excel.Range celLrangE;
            excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Visible = false;
            excel.DisplayAlerts = false;
            worKbooK = excel.Workbooks.Add(Type.Missing);
            worKsheeT = (Microsoft.Office.Interop.Excel.Worksheet)worKbooK.ActiveSheet; worKsheeT.Name = "StudentRepoertCard";
            worKsheeT.Range[worKsheeT.Cells[1, 1], worKsheeT.Cells[1, 8]].Merge();
            worKsheeT.Cells[1, 1] = "Student Report Card";
            worKsheeT.Cells.Font.Size = 15;
            int rowcount = 2;

            foreach (DataRow datarow in ExportToExcel().Rows)
            {
                rowcount += 1;
                for (int i = 1; i <= ExportToExcel().Columns.Count; i++)
                {

                    if (rowcount == 3)
                    {
                        worKsheeT.Cells[2, i] = ExportToExcel().Columns[i - 1].ColumnName;
                        worKsheeT.Cells.Font.Color = System.Drawing.Color.Black;

                    }

                    worKsheeT.Cells[rowcount, i] = datarow[i - 1].ToString();

                    if (rowcount > 3)
                    {
                        if (i == ExportToExcel().Columns.Count)
                        {
                            if (rowcount % 2 == 0)
                            {
                                celLrangE = worKsheeT.Range[worKsheeT.Cells[rowcount, 1], worKsheeT.Cells[rowcount, ExportToExcel().Columns.Count]];
                            }

                        }
                    }

                }

            }

            celLrangE = worKsheeT.Range[worKsheeT.Cells[1, 1], worKsheeT.Cells[rowcount, ExportToExcel().Columns.Count]];
            celLrangE.EntireColumn.AutoFit();
            Microsoft.Office.Interop.Excel.Borders border = celLrangE.Borders;
            border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border.Weight = 2d;

            celLrangE = worKsheeT.Range[worKsheeT.Cells[1, 1], worKsheeT.Cells[2, ExportToExcel().Columns.Count]];

            worKbooK.SaveAs(@"\\10.5.2.101\RHDiagnostics\log\ExcelDemo.xlsx");
            worKbooK.Close();
            excel.Quit();

            string file = "ExcelDemo.xlsx";
            string fullPath = @"\\10.5.2.101\RHDiagnostics\log\"+  file;
            return File(fullPath, "application/vnd.ms-excel", file);


            // https://www.c-sharpcorner.com/UploadFile/bd6c67/how-to-create-excel-file-using-C-Sharp/
            // return Json("");
        }
        public static System.Data.DataTable ExportToExcel()
        {
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Sex", typeof(string));
            table.Columns.Add("Subject1", typeof(int));
            table.Columns.Add("Subject2", typeof(int));
            table.Columns.Add("Subject3", typeof(int));
            table.Columns.Add("Subject4", typeof(int));
            table.Columns.Add("Subject5", typeof(int));
            table.Columns.Add("Subject6", typeof(int));
            table.Rows.Add(1, "Amar", "M", 78, 59, 72, 95, 83, 77);
            table.Rows.Add(2, "Mohit", "M", 76, 65, 85, 87, 72, 90);
            return table;
        }
    }
}
