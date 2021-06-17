using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using OfficeOpenXml;
namespace PL.Controllers
{
    public class EmpresaController : Controller
    {
        //[IdAdministradorCreate]
        //Session["CurrentIdAdminLog"]
        // GET: Empresa
        public ActionResult GetAll()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            result.CompanyCategoriaList = new List<object>();

            List<object> permisosEstructura = new List<object>();
            ViewBag.Permisos = Session["CompaniesPermisos"];
            permisosEstructura = ViewBag.Permisos;
            var getCompany = BL.Empresa.GetAll(permisosEstructura);

            var getCategoria = BL.CompanyCategoria.GetAll();

            result.CompanyCategoriaList = getCategoria.Objects;
            result.Objects = getCompany.Objects;


            return View(result);
        }
        [HttpGet]
        public ActionResult Add()
        {
            var result = BL.CompanyCategoria.GetAll();
            ML.Company company = new ML.Company();
            company.CompanyCategoria = new ML.CompanyCategoria();
            company.CompanyCategoria.ListCompanyCategoria = new List<object>();
            company.CompanyCategoria.ListCompanyCategoria = result.Objects;

            return View(company);
        }
        [HttpPost]
        public ActionResult Add(ML.Company company)
        {
            company.CURRENT_USER = Convert.ToString(Session["AdminLog"]);
            int IDADMINISTRADORCREATE = Convert.ToInt32(Session["CurrentIdAdminLog"]);
            var result = BL.Empresa.Add(company, IDADMINISTRADORCREATE);

            //uPDATE ADMINSITRADORCOMPANY
            
            var getCompanies = BL.Administrador.GetCompaniesForPermisos(IDADMINISTRADORCREATE);
            ViewBag.CompaniesPermisos = getCompanies.Objects;
            Session["CompaniesPermisos"] = getCompanies.Objects;

            if (result.Correct == true)
            {
                return Json("success");
            }
            else
            {
                ViewBag.ErrorMessage = result.ErrorMessage;
                return Json("error");
            }
        }

        public ActionResult AdministrarAreaDepto(int CompanyId)
        {
            Session["CompanyIdForAddArea"] = CompanyId;

            if (Session["LogError"] == null)
            {
                Session["LogError"] = 9;
            }

            var result = BL.Area.AreaGetByCompanyId(CompanyId);

            return View(result);
        }
        [HttpPost]
        public ActionResult AdministrarAreaDepto(HttpPostedFileBase postedFile)
        {
            Session["MsgError"] = "";
            int CompanyId = Convert.ToInt32(Session["CompanyIdForAddArea"]);
            Session["LogError"] = null;
            if (postedFile != null)
            {
                try
                {
                    string fileExtension = Path.GetExtension(postedFile.FileName);

                    if (fileExtension != ".xlx" && fileExtension != ".xlsx")
                    {
                        Session["MsgError"] = "Mala extension";
                        ViewBag.Message = 1;
                        Session["LogError"] = ViewBag.Message;
                        return Redirect(Request.UrlReferrer.ToString());
                    }

                    string folderPath = Server.MapPath("~/UploadedFiles/");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    var filePath = folderPath + Path.GetFileName(postedFile.FileName);
                    postedFile.SaveAs(filePath);

                    string excelConString = "";

                    switch (fileExtension)
                    {
                        case ".xls":
                            excelConString = "Provider = Microsoft.Jet.OLEDB.4.0; Data Source = { 0 }; Extended Properties = 'Excel 8.0;HDR=YES'";
                            break;
                        case ".xlsx":
                            excelConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'";
                            break;
                    }

                    //Leer los datos de la primer hoja
                    DataTable dt = new DataTable();
                    excelConString = string.Format(excelConString, filePath);

                    using (OleDbConnection excelOledbConnection = new OleDbConnection(excelConString))
                    {
                        using (OleDbCommand excelDBCommand = new OleDbCommand())
                        {
                            using (OleDbDataAdapter excelDataAdapter = new OleDbDataAdapter())
                            {
                                excelDBCommand.Connection = excelOledbConnection;

                                excelOledbConnection.Open();

                                DataTable excelSchema = GetSchemaFromExcel(excelOledbConnection);

                                string sheetName = excelSchema.Rows[0]["TABLE_NAME"].ToString();
                                excelOledbConnection.Close();

                                excelOledbConnection.Open();
                                excelDBCommand.CommandText = "SELECT * FROM [" + sheetName + "]";
                                excelDataAdapter.SelectCommand = excelDBCommand;
                                //Fill tabla from adapter
                                excelDataAdapter.Fill(dt);
                                excelOledbConnection.Close();
                            }
                        }
                    }
                    //Insertar a la tabla Area
                    using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                    {
                        using (var transaction = context.Database.BeginTransaction())
                        {
                            try
                            {
                                if (dt.Rows.Count == 0)
                                {
                                    Session["LogError"] = 6;
                                    Session["MsgError"] = "no hay comumnas";
                                    return Redirect(Request.UrlReferrer.ToString());
                                }
                                else
                                {
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        context.Area.Add(GetAreaFromExcelRow(row));
                                    }
                                    context.SaveChanges();
                                    transaction.Commit();
                                }
                            }
                            catch (Exception ex)
                            {
                                Session["MsgError"] = "Entro al rollback" + ex.Message;
                                transaction.Rollback();
                                ViewBag.Message = 3;
                                Session["LogError"] = ViewBag.Message;
                                return Redirect(Request.UrlReferrer.ToString());
                            }
                        }
                    }
                    ViewBag.Message = "Los datos se importaron exitosamente";
                }
                catch (Exception ex)
                {
                    Session["MsgError"] = "error en el cath de la linea 177" + ex.Message;
                    ViewBag.Message = 4;
                    Session["LogError"] = ViewBag.Message;
                    return Redirect(Request.UrlReferrer.ToString());
                }
            }
            else
            {
                Session["MsgError"] = "Error en la linea 185";
                ViewBag.Message = 5;
                Session["LogError"] = ViewBag.Message;
                return Redirect(Request.UrlReferrer.ToString());
            }
            Session["MsgError"] = "linea 190";
            Session["LogError"] = 10;
            return Redirect(Request.UrlReferrer.ToString());
        }

        private static DataTable GetSchemaFromExcel(OleDbConnection excelOledbConnection)
        {
            return excelOledbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        }
        private DL.Area GetAreaFromExcelRow(DataRow row)
        {
            return new DL.Area
            {
                Nombre = row[0].ToString(),
                CompanyId = Convert.ToInt32(Session["CompanyIdForAddArea"]),
                FechaHoraCreacion = DateTime.Now,
                UsuarioCreacion = Convert.ToString(Session["AdminLog"]),
                ProgramaCreacion = "Diagnostic4U",
                IdEstatus = Convert.ToInt32(row[1])
            };
        }

        [HttpPost]
        public ActionResult UpdateArea(List<ML.Area> data)
        {
            bool resultado = false;
            string mensajeError = "";
            foreach (ML.Area itemArea in data)
            {
                var resultUpdate = BL.Area.Update(itemArea);
                resultado = resultUpdate.Correct;
                mensajeError = resultUpdate.ErrorMessage;
            }

            if (resultado == false)
            {
                ViewBag.Message = mensajeError;
                return Json("error");
            }
            else
            {
                return Json("success");
            }

        }

        public ActionResult UpdateEstatus(ML.Area area)
        {
            var result = BL.Area.UpdateEstatus(area);
            if (result.Correct == false)
            {
                ViewBag.ErrorMessage = result.ErrorMessage;
                int CompanyId = Convert.ToInt32(Session["CompanyIdForAddArea"]);
                var GetAreaByCompanyId = BL.Area.AreaGetByCompanyId(CompanyId);
                return View("AdministrarAreaDepto", GetAreaByCompanyId);
            }
            else
            {
                int CompanyId = Convert.ToInt32(Session["CompanyIdForAddArea"]);
                var GetAreaByCompanyId = BL.Area.AreaGetByCompanyId(CompanyId);
                return View("AdministrarAreaDepto", GetAreaByCompanyId);
            }
        }

        public FileResult DownloadLayoutArea()
        {
            string file = "LayoutArea.xlsx";
            string fullPath = Path.Combine(Server.MapPath("~/resources/"), file);
            return File(fullPath, "application/vnd.ms-excel", file);
        }
        public FileResult DownloadLayoutDepto()
        {
            string file = "Layout Departamento.xlsx";
            string fullPath = Path.Combine(Server.MapPath("~/resources/"), file);
            return File(fullPath, "application/vnd.ms-excel", file);
        }
        public ActionResult GetModalEdit(ML.Company company)
        {
            return PartialView("ModalEdit");
        }

        public ActionResult GetCompanyById(int CompanyId)
        {
            var result = BL.Empresa.GetByComoanyId(CompanyId);

            ML.Company company = new ML.Company();
            company.CompanyCategoria = new ML.CompanyCategoria();
            company.TipoEstatus = new ML.TipoEstatus();

            company.CompanyId = ((ML.Company)result.Object).CompanyId;
            company.CompanyName = ((ML.Company)result.Object).CompanyName;
            company.CompanyCategoria.IdCompanyCategoria = ((ML.Company)result.Object).CompanyCategoria.IdCompanyCategoria;
            company.CompanyCategoria.Descripcion = ((ML.Company)result.Object).CompanyCategoria.Descripcion;
            company.TipoEstatus.IdEstatus = ((ML.Company)result.Object).TipoEstatus.IdEstatus;
            company.TipoEstatus.Descripcion = ((ML.Company)result.Object).TipoEstatus.Descripcion;

            company.Color = ((ML.Company)result.Object).Color;
            company.LogoEmpresa = ((ML.Company)result.Object).LogoEmpresa;

            if (result.Correct == true)
            {
                return Json(company, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("error");
            }
        }

        [HttpPost]
        public ActionResult Update(ML.Company company)
        {
            company.CURRENT_USER = Convert.ToString(Session["AdminLog"]);
            var result = BL.Empresa.Update(company);

            if (result.Correct == true)
            {
                return Json("success");
            }
            else
            {
                return Json("error");
            }
        }

        public ActionResult UpdateEstatusCompany(ML.Company company)
        {
            company.CURRENT_USER = Convert.ToString(Session["AdminLog"]);
            var result = BL.Empresa.UpdateEstatus(company);
            if (result.Correct == false)
            {
                //Send ViewBag
                return RedirectToAction("GetAll", "Empresa");
            }
            else
            {
                return RedirectToAction("GetAll", "Empresa");
            }
        }

        [HttpPost]
        public ActionResult AddArea(ML.Area area)
        {
            area.CURRENT_USER = Convert.ToString(Session["AdminLog"]);

            var result = BL.Area.Add(area);

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
        public ActionResult AddDepartamentoFromExcelFile(HttpPostedFileBase postedFileDepto)
        {
            Session["ErrorMessage"] = null;
            if (postedFileDepto != null)
            {
                try
                {
                    string fileExtension = Path.GetExtension(postedFileDepto.FileName);

                    if (fileExtension != ".xls" && fileExtension != ".xlsx")
                    {
                        ViewBag.Message = 1;
                        Session["ErrorMessage"] = ViewBag.Message;
                        return Redirect(Request.UrlReferrer.ToString());
                    }

                    string folderPath = Server.MapPath("~/UploadedFiles/");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    var filePath = folderPath + Path.GetFileName(postedFileDepto.FileName);
                    postedFileDepto.SaveAs(filePath);

                    string excelConString = "";

                    switch (fileExtension)
                    {
                        case ".xls":
                            excelConString = "Provider = Microsoft.Jet.OLEDB.4.0; Data Source = { 0 }; Extended Properties = 'Excel 8.0;HDR=YES'";
                            break;
                        case ".xlsx":
                            excelConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'";
                            break;
                    }

                    DataTable dt = new DataTable();
                    excelConString = string.Format(excelConString, filePath);

                    using (OleDbConnection excelOledbConnection = new OleDbConnection(excelConString))
                    {
                        using (OleDbCommand excelDBCommand = new OleDbCommand())
                        {
                            using (OleDbDataAdapter excelDataAdapter = new OleDbDataAdapter())
                            {
                                excelDBCommand.Connection = excelOledbConnection;

                                excelOledbConnection.Open();

                                DataTable excelSchema = GetSchemaFromExcel(excelOledbConnection);

                                string sheetName = excelSchema.Rows[0]["TABLE_NAME"].ToString();
                                excelOledbConnection.Close();

                                excelOledbConnection.Open();
                                excelDBCommand.CommandText = "SELECT * FROM [" + sheetName + "]";
                                excelDataAdapter.SelectCommand = excelDBCommand;
                                //Fill tabla from adapter
                                excelDataAdapter.Fill(dt);
                                excelOledbConnection.Close();
                            }
                        }
                    }
                    //Insercion SQL
                    using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                    {
                        context.Database.Log = Console.Write;
                        using (var transaction = context.Database.BeginTransaction())
                        {
                            try
                            {
                                if (dt.Rows.Count == 0)
                                {
                                    Session["ErrorMessage"] = 6;
                                    return Redirect(Request.UrlReferrer.ToString());
                                }
                                else
                                {
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        //var query =  //BL.Departamento.AddForD4U(row, context, transaction);
                                        //if (query.ErrorMessage != null)
                                        //{
                                        //    transaction.Rollback();
                                        //    Session["ErrorMessage"] = 2;
                                        //    return Redirect(Request.UrlReferrer.ToString());
                                        //}
                                    }
                                    //El error sale al entrar a commit
                                    context.SaveChanges();
                                    transaction.Commit();
                                }

                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                ViewBag.Message = 3;
                                Session["ErrorMessage"] = ViewBag.Message;
                                return Redirect(Request.UrlReferrer.ToString());
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    //Code exception
                    ViewBag.Message = 4;
                    Session["ErrorMessage"] = ViewBag.Message;
                    return Redirect(Request.UrlReferrer.ToString());
                }
            }
            else
            {
                ViewBag.Message = 5;
                Session["ErrorMessage"] = ViewBag.Message;
                return Redirect(Request.UrlReferrer.ToString());
            }

            Session["ErrorMessage"] = 10;
            return Redirect(Request.UrlReferrer.ToString());
        }


        private static DataTable GetSchemaFromExcelDepto(OleDbConnection excelOledbConnection)
        {
            return excelOledbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        }
        private ML.Departamento GetDeptoFromExcelRow(DataRow row)
        {
            return new ML.Departamento
            {
                Nombre = row[0].ToString(),
                NombreArea = row[1].ToString(),
                NombreCompany = row[2].ToString(),
                IdEstatus = Convert.ToInt32(row[3]),

                FechaHoraCreacion = DateTime.Now,
                UsuarioCreacion = Convert.ToString(Session["AdminLog"]),
                ProgramaCreacion = "Diagnostic4U"
            };
        }

        public JsonResult GetCompanyAjax(int id)
        {
            var result = BL.Empresa.GetCompanyByIdCompanyCategoria(id);

            List<SelectListItem> Companies = new List<SelectListItem>();

            //Companies.Add(new SelectListItem { Text = "Selecciona una opcion", Value = "0" });

            if (result.Objects != null)
            {
                foreach (ML.Company company in result.Objects)
                {
                    Companies.Add(new SelectListItem { Text = company.CompanyName.ToString(), Value = company.CompanyId.ToString() });



                }

            }

            return Json(new SelectList(Companies, "Value", "Text", JsonRequestBehavior.AllowGet));
        }

        public ActionResult GetDepartamentoByIdArea(int IdArea)
        {
            if (Session["ErrorMessage"] == null)
            {
                Session["ErrorMessage"] = 9;
            }
            Session["CURRENT_AREA"] = IdArea;
            var result = BL.Departamento.GetDepartamentoByIdArea(IdArea);

            return View(result);
        }

        [HttpPost]
        public ActionResult AddDepartamento(ML.Departamento departamento)
        {
            var result = BL.Departamento.Add(departamento);

            if (result.Correct == true)
            {
                return Json("success");
            }
            else
            {
                return Json("error");
            }
        }

        public ActionResult UpdateEstatusDepartamento(ML.Departamento departamento)
        {
            var result = BL.Departamento.UpdateEstatus(departamento);

            if (result.Correct == true)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al intentar actualizar el estatus " + result.ErrorMessage;
                return Redirect(Request.UrlReferrer.ToString());
            }
        }

        public ActionResult GetAreaById(int IdArea)
        {
            ML.Area area = new ML.Area();
            area.TipoEstatus = new ML.TipoEstatus();
            area.Company = new ML.Company();
            area.Company.CompanyCategoria = new ML.CompanyCategoria();

            var resultData = BL.Area.GetById(IdArea);

            //Mapping properties
            area.IdArea = ((ML.Area)resultData.Object).IdArea;
            area.Nombre = ((ML.Area)resultData.Object).Nombre;

            area.TipoEstatus.IdEstatus = ((ML.Area)resultData.Object).TipoEstatus.IdEstatus;
            area.TipoEstatus.Descripcion = ((ML.Area)resultData.Object).TipoEstatus.Descripcion;

            area.Company.CompanyId = ((ML.Area)resultData.Object).Company.CompanyId;
            area.Company.CompanyName = ((ML.Area)resultData.Object).Company.CompanyName;

            area.Company.CompanyCategoria.IdCompanyCategoria = ((ML.Area)resultData.Object).Company.CompanyCategoria.IdCompanyCategoria;
            area.Company.CompanyCategoria.Descripcion = ((ML.Area)resultData.Object).Company.CompanyCategoria.Descripcion;

            if (resultData.Correct == true)
            {
                return Json(area, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("error");
            }

        }

        [HttpPost]
        public ActionResult UpdateAreaD4U(ML.Area area)
        {
            area.CURRENT_USER = Convert.ToString(Session["AdminLog"]);
            var result = BL.Area.UpdateD4U(area);

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
        public ActionResult DeleteEmpresa(ML.Company company)
        {
            company.CURRENT_USER = Convert.ToString(Session["AdminLog"]);
            var result = BL.Empresa.Delete(company);

            if (result.Correct == true)
            {
                return Json("success");
            }
            else
            {
                return Json("error");
            }
        }

        public ActionResult DeleteArea(ML.Area area)
        {
            var result = BL.Area.Delete(area.IdArea);

            if (result.Correct == true)
            {
                return Json("success");
            }
            else
            {
                return Json("error");
            }
        }

        public ActionResult DeleteDepartamento(ML.Departamento departamento)
        {
            var result = BL.Departamento.Delete(departamento.IdDepartamento);

            if (result.Correct == true)
            {
                return Json("success");
            }
            else
            {
                return Json("error");
            }
        }

        /*
         Seccion para las empresas con estatus de eliminadas
         Company
         Area
         Departamento
        */
        public ActionResult GetAllEliminadas()
        {
            ML.Result result = new ML.Result();
            result.ListCompanyDelete = new List<object>();
            result.ListAreaDelete = new List<object>();
            result.ListDepartamentoDelete = new List<object>();

            var resultListCompany = BL.Empresa.GetAllEliminados();
            var resultListArea = BL.Area.GetAllEliminados();
            var resultListDepartamento = BL.Departamento.GetAllEliminados();

            result.ListCompanyDelete = resultListCompany.Objects;
            result.ListAreaDelete = resultListArea.Objects;
            result.ListDepartamentoDelete = resultListDepartamento.Objects;

            return View(result);
        }

        public ActionResult ModEmpresasEliminarDefEmpresaAreaDepto(ML.Company company)
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    if (company.CompanyName == "Empresa")
                    {
                        var query = context.Database.ExecuteSqlCommand("UPDATE COMPANY SET IDESTATUS = 6 where companyid = {0}", company.CompanyId);
                        context.SaveChanges();
                    }
                    if (company.CompanyName == "Area")
                    {
                        var query = context.Database.ExecuteSqlCommand("UPDATE area SET IDESTATUS = 6 where idarea = {0}", company.CompanyId);
                        context.SaveChanges();
                    }
                    if (company.CompanyName == "Departamento")
                    {
                        var query = context.Database.ExecuteSqlCommand("UPDATE departamento SET IDESTATUS = 6 where iddepartamento = {0}", company.CompanyId);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
            return Json("success");
        }

        [HttpGet]
        public ActionResult GetDepartamentoById(ML.Departamento departamento)
        {
            var result = BL.Departamento.GetDepartamentoById(departamento.IdDepartamento);

            ML.Departamento depto = new ML.Departamento();
            depto.TipoEstatus = new ML.TipoEstatus();
            depto.Area = new ML.Area();
            depto.Area.Company = new ML.Company();
            depto.Area.Company.CompanyCategoria = new ML.CompanyCategoria();

            depto.IdDepartamento = ((ML.Departamento)result.Object).IdDepartamento;
            depto.Nombre = ((ML.Departamento)result.Object).Nombre;
            depto.TipoEstatus.IdEstatus = ((ML.Departamento)result.Object).TipoEstatus.IdEstatus;
            depto.TipoEstatus.Descripcion = ((ML.Departamento)result.Object).TipoEstatus.Descripcion;
            depto.Area.IdArea = ((ML.Departamento)result.Object).Area.IdArea;
            depto.Area.Nombre = ((ML.Departamento)result.Object).Area.Nombre;
            depto.Area.Company.CompanyId = ((ML.Departamento)result.Object).Area.Company.CompanyId;
            depto.Area.Company.CompanyName = ((ML.Departamento)result.Object).Area.Company.CompanyName;
            depto.Area.Company.CompanyCategoria.IdCompanyCategoria = ((ML.Departamento)result.Object).Area.Company.CompanyCategoria.IdCompanyCategoria;
            depto.Area.Company.CompanyCategoria.Descripcion = ((ML.Departamento)result.Object).Area.Company.CompanyCategoria.Descripcion;

            if (result.Correct == true)
            {
                return Json(depto, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("error");
            }
        }

        public JsonResult GetAreaAjax(int id)
        {
            var result = BL.Empresa.GetAreaByCompanyId(id);

            List<SelectListItem> Areas = new List<SelectListItem>();

            //Companies.Add(new SelectListItem { Text = "Selecciona una opcion", Value = "0" });

            if (result.Objects != null)
            {
                foreach (ML.Area area in result.Objects)
                {
                    Areas.Add(new SelectListItem { Text = area.Nombre.ToString(), Value = area.IdArea.ToString() });
                }
            }
            return Json(new SelectList(Areas, "Value", "Text", JsonRequestBehavior.AllowGet));
        }

        public ActionResult UpdateDepartamento(ML.Departamento departamento)
        {
            departamento.CURRENT_USER = Convert.ToString(Session["AdminLog"]);
            var result = BL.Departamento.UpdateDepartamento(departamento);

            if (result.Correct == true)
            {
                return Json("success");
            }
            else
            {
                return Json("error");
            }
        }

        public ActionResult AddSubDepartamento(ML.Subdepartamento subdepto)
        {
            subdepto.CURRENT_USER = Convert.ToString(Session["AdminLog"]);

            var result = BL.SubDepartamento.Add(subdepto);

            if (result.Correct == true)
            {
                return Json("success");
            }
            else
            {
                return Json("error");
            }
        }

        public ActionResult GetAllSubDepartamento()
        {
            var result = BL.SubDepartamento.GetAllNotDeleted();

            return View(result);
        }

        //Carga masiva
        public ActionResult BulkArea(FormCollection formCollection)
        {
            var areaList = new List<DL.Area>();
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
                                        string estatus = workSheet.Cells[rowIterator, 2].Value.ToString();
                                    }
                                    catch (Exception)
                                    {
                                        workSheet.Cells[rowIterator, 2].Value = "Activo";
                                    }
                                    if ((workSheet.Cells[rowIterator, 2].Value.ToString()) == "Activo" || (workSheet.Cells[rowIterator, 2].Value.ToString()) == "activo")
                                    {
                                        ID_ESTATUS = 1;
                                        var area = new DL.Area();
                                        try
                                        {
                                            area.Nombre = (workSheet.Cells[rowIterator, 1].Value.ToString());
                                        }
                                        catch (Exception)
                                        {
                                            continue;
                                        }
                                        area.IdEstatus = ID_ESTATUS;
                                        area.CompanyId = Convert.ToInt32(Session["CompanyIdForAddArea"]);
                                        area.FechaHoraCreacion = DateTime.Now;
                                        area.UsuarioCreacion = Convert.ToString(Session["AdminLog"]);
                                        area.ProgramaCreacion = "DIagnostic4U";
                                        areaList.Add(area);
                                    }
                                    else if ((workSheet.Cells[rowIterator, 2].Value.ToString()) == "Inactivo" || (workSheet.Cells[rowIterator, 2].Value.ToString()) == "inactivo")
                                    {
                                        ID_ESTATUS = 2;
                                        var area = new DL.Area();
                                        try
                                        {
                                            area.Nombre = (workSheet.Cells[rowIterator, 1].Value.ToString());
                                        }
                                        catch (Exception)
                                        {
                                            continue;
                                        }
                                        area.IdEstatus = ID_ESTATUS;
                                        area.CompanyId = Convert.ToInt32(Session["CompanyIdForAddArea"]);
                                        area.FechaHoraCreacion = DateTime.Now;
                                        area.UsuarioCreacion = Convert.ToString(Session["AdminLog"]);
                                        area.ProgramaCreacion = "DIagnostic4U";
                                        areaList.Add(area);
                                    }
                                    else if ((workSheet.Cells[rowIterator, 2].Value.ToString()) == "")
                                    {
                                        ID_ESTATUS = 1;
                                        var area = new DL.Area();
                                        try
                                        {
                                            area.Nombre = (workSheet.Cells[rowIterator, 1].Value.ToString());
                                        }
                                        catch (Exception)
                                        {
                                            continue;
                                        }
                                        area.IdEstatus = ID_ESTATUS;
                                        area.CompanyId = Convert.ToInt32(Session["CompanyIdForAddArea"]);
                                        area.FechaHoraCreacion = DateTime.Now;
                                        area.UsuarioCreacion = Convert.ToString(Session["AdminLog"]);
                                        area.ProgramaCreacion = "DIagnostic4U";
                                        areaList.Add(area);
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
                        foreach (var item in areaList)
                        {
                            context.Area.Add(item);
                        }
                        context.SaveChanges();
                        transaction.Commit();
                        result.Correct = true;
                        Session["LogError"] = 10;
                        return Redirect(Request.UrlReferrer.ToString());
                    }
                    catch (Exception ex)
                    {
                        result.Correct = false;
                        result.ErrorMessage = ex.Message;
                        transaction.Rollback();
                        Session["LogError"] = 3;
                        return Redirect(Request.UrlReferrer.ToString());
                    }
                }
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        //Carga masiva
        public ActionResult BulkDepartamento(FormCollection formCollection, ML.Result area)
        {
            var departamentoList = new List<DL.Departamento>();
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
                                        string stats = workSheet.Cells[rowIterator, 2].Value.ToString();
                                    }
                                    catch (Exception)
                                    {
                                        workSheet.Cells[rowIterator, 2].Value = "Activo";
                                    }
                                    if ((workSheet.Cells[rowIterator, 2].Value.ToString()) == "Activo" || (workSheet.Cells[rowIterator, 2].Value.ToString()) == "activo")
                                    {
                                        ID_ESTATUS = 1;
                                        var departamento = new DL.Departamento();
                                        try
                                        {
                                            departamento.Nombre = (workSheet.Cells[rowIterator, 1].Value.ToString());
                                        }
                                        catch (Exception)
                                        {
                                            continue;
                                        }
                                        departamento.IdArea = area.IdentificadorArea;
                                        departamento.IdEstatus = ID_ESTATUS;
                                        departamento.FechaHoraCreacion = DateTime.Now;
                                        departamento.UsuarioCreacion = Convert.ToString(Session["AdminLog"]);
                                        departamento.ProgramaCreacion = "DIagnostic4U";
                                        departamentoList.Add(departamento);
                                    }
                                    else if ((workSheet.Cells[rowIterator, 2].Value.ToString()) == "Inactivo" || (workSheet.Cells[rowIterator, 2].Value.ToString()) == "inactivo")
                                    {
                                        ID_ESTATUS = 2;
                                        var departamento = new DL.Departamento();
                                        try
                                        {
                                            departamento.Nombre = (workSheet.Cells[rowIterator, 1].Value.ToString());
                                        }
                                        catch (Exception)
                                        {
                                            continue;
                                        }
                                        departamento.IdArea = area.IdentificadorArea;
                                        departamento.IdEstatus = ID_ESTATUS;
                                        departamento.FechaHoraCreacion = DateTime.Now;
                                        departamento.UsuarioCreacion = Convert.ToString(Session["AdminLog"]);
                                        departamento.ProgramaCreacion = "DIagnostic4U";
                                        departamentoList.Add(departamento);
                                    }
                                    else if ((workSheet.Cells[rowIterator, 2].Value.ToString()) == "")
                                    {
                                        ID_ESTATUS = 1;
                                        var departamento = new DL.Departamento();
                                        try
                                        {
                                            departamento.Nombre = (workSheet.Cells[rowIterator, 1].Value.ToString());
                                        }
                                        catch (Exception)
                                        {
                                            continue;
                                        }
                                        departamento.IdArea = area.IdentificadorArea;
                                        departamento.IdEstatus = ID_ESTATUS;
                                        departamento.FechaHoraCreacion = DateTime.Now;
                                        departamento.UsuarioCreacion = Convert.ToString(Session["AdminLog"]);
                                        departamento.ProgramaCreacion = "DIagnostic4U";
                                        departamentoList.Add(departamento);
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
                        foreach (var item in departamentoList)
                        {
                            var resultBulk = BL.Departamento.AddForD4U(item, context, transaction);

                            if (resultBulk.Correct == false)
                            {
                                //Cancelar todo
                                transaction.Rollback();
                                Session["LogError"] = 2;
                                return Redirect(Request.UrlReferrer.ToString());
                            }
                        }
                        context.SaveChanges();
                        transaction.Commit();
                        result.Correct = true;
                        Session["LogError"] = 10;
                        return Redirect(Request.UrlReferrer.ToString());
                    }
                    catch (Exception ex)
                    {
                        result.Correct = false;
                        result.ErrorMessage = ex.Message;
                        transaction.Rollback();
                        Session["LogError"] = 3;
                        return Redirect(Request.UrlReferrer.ToString());
                    }
                }
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult ResetLogError()
        {
            Session["LogError"] = 9;
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUNegocio()
        {
            var result = BL.CompanyCategoria.GetAll();

            List<SelectListItem> Companies = new List<SelectListItem>();

            Companies.Add(new SelectListItem { Text = "Selecciona una opcion", Value = "0" });

            if (result.Objects != null)
            {
                foreach (ML.CompanyCategoria company in result.Objects)
                {
                    Companies.Add(new SelectListItem { Text = company.Descripcion.ToString(), Value = company.IdCompanyCategoria.ToString() });
                }

            }
            return Json(new SelectList(Companies, "Value", "Text", JsonRequestBehavior.AllowGet));
        }

        public ActionResult AddUNegocio(ML.CompanyCategoria companyCateg)
        {
            ML.Result result = new ML.Result();
            string currentuser = Convert.ToString(Session["AdminLog"]);
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Database.ExecuteSqlCommand("INSERT INTO COMPANYCATEGORIA (DESCRIPCION, FECHAHORACREACION, PROGRAMACREACION, USUARIOCREACION) VALUES ({0}, {1}, {2}, {3})", companyCateg.Descripcion, DateTime.Now, "Diagnostic4U", currentuser);

                    result.Correct = true;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
                result.Correct = false;
            }
            if (result.Correct == false)
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
            else
                return Json("success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllEmpresas()
        {
            var result = BL.Empresa.GetAllEmpresas();
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAllAreas()
        {
            var result = BL.Empresa.GetAllAreas();
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAllDepartamentos()
        {
            var result = BL.Empresa.GetAllDepartamentos();
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
    }
}