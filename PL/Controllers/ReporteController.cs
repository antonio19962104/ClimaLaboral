using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using DL;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Diagnostics;
using System.Configuration;
using System.Data;

namespace PL.Controllers
{
    /*
     * los metodos que retornan un list<double> son auxiliares de apisController
     * losmetodos que retornan la misma lista pero en formato json son para los primeros reportes de clima 
    */
    public class ReporteController : Controller
    {
        List<List<int>> listaGlobal = new List<List<int>>();
        public ActionResult GetEstructuraForAbiertas(ML.CompanyCategoria CompanyCateg, int idBD)
        {
            /*Validar repetidos*/
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    context.Database.CommandTimeout = 300;
                    var query = context.getComentarios(CompanyCateg.Descripcion, idBD).ToList();
                    result.Objects = new List<object>();
                    if (query.Count < 1)
                    {
                        var jsons = Json(result.Objects, JsonRequestBehavior.AllowGet);
                        jsons.MaxJsonLength = int.MaxValue;
                        return new JsonResult { Data = jsons, MaxJsonLength = Int32.MaxValue };
                    }
                    if (query != null)
                    {
                        Session["Company"] = "";
                        Session["Area"] = "";
                        Session["Departamento"] = "";
                        Session["Subdepartamento"] = "";
                        foreach (var item in query)
                        {
                            ML.EmpleadoRespuesta empleadoRes = new ML.EmpleadoRespuesta();
                            empleadoRes.Empleado = new ML.Empleado();

                            empleadoRes.Empleado.DivisonMarca = item.DivisionMarca;
                            empleadoRes.Empleado.AreaAgencia = item.AreaAgencia;
                            empleadoRes.Empleado.Depto = item.Depto;

                            if (empleadoRes.Empleado.Depto != "-")
                            {
                                empleadoRes.Demografico = empleadoRes.Empleado.Depto;
                            }
                            else
                            if (empleadoRes.Empleado.AreaAgencia != "-")
                            {
                                empleadoRes.Demografico = empleadoRes.Empleado.AreaAgencia;
                            }
                            else
                            if (empleadoRes.Empleado.Depto == "-" || empleadoRes.Empleado.AreaAgencia == "-")
                            {
                                empleadoRes.Demografico = empleadoRes.Empleado.DivisonMarca;
                            }


                            empleadoRes.Abierta1 = item.EMP_R1;
                            empleadoRes.Abierta2 = item.EMP_R2;
                            empleadoRes.Abierta3 = item.EMP_R3;
                            empleadoRes.Abierta4 = item.EMP_R4;
                            empleadoRes.Abierta5 = item.EMP_R5;
                            empleadoRes.Abierta6 = item.EMP_R6;
                            empleadoRes.Abierta7 = item.EMP_R7;




                            result.Objects.Add(empleadoRes);
                            result.Correct = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            /**/
            //var result = BL.ReporteD4U.GetEstructuraForReporte(CompanyCateg.IdCompanyCategoria);
            var json = Json(result.Objects, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return new JsonResult { Data = json, MaxJsonLength = Int32.MaxValue };
        }
        [HttpPost]
        public ActionResult GetEstructuraForAbiertas(List<string> listUnidadNeg, int idBD)
        {
            /*Validar repetidos*///
            ML.Result result = new ML.Result();
            try
            {
                for (int i = (listUnidadNeg.Count - 1); i < 20; i++)
                    listUnidadNeg.Add("");
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    context.Database.CommandTimeout = 300;
                    var query = context.getComentariosMultiUnidad(listUnidadNeg[0], listUnidadNeg[1], listUnidadNeg[2], listUnidadNeg[3], listUnidadNeg[4]
                        , listUnidadNeg[5], listUnidadNeg[6], listUnidadNeg[7], listUnidadNeg[8], listUnidadNeg[9], listUnidadNeg[10], listUnidadNeg[11], idBD).ToList();
                    result.Objects = new List<object>();
                    if (query.Count < 1)
                    {
                        var jsons = Json(result.Objects, JsonRequestBehavior.AllowGet);
                        jsons.MaxJsonLength = int.MaxValue;
                        return new JsonResult { Data = jsons, MaxJsonLength = Int32.MaxValue };
                    }
                    if (query != null)
                    {
                        Session["Company"] = "";
                        Session["Area"] = "";
                        Session["Departamento"] = "";
                        Session["Subdepartamento"] = "";
                        foreach (var item in query)
                        {
                            ML.EmpleadoRespuesta empleadoRes = new ML.EmpleadoRespuesta();
                            empleadoRes.Empleado = new ML.Empleado();

                            empleadoRes.Empleado.DivisonMarca = item.DivisionMarca;
                            empleadoRes.Empleado.AreaAgencia = item.AreaAgencia;
                            empleadoRes.Empleado.Depto = item.Depto;

                            if (empleadoRes.Empleado.Depto != "-")
                            {
                                empleadoRes.Demografico = empleadoRes.Empleado.Depto;
                            }
                            else
                            if (empleadoRes.Empleado.AreaAgencia != "-")
                            {
                                empleadoRes.Demografico = empleadoRes.Empleado.AreaAgencia;
                            }
                            else
                            if (empleadoRes.Empleado.Depto == "-" || empleadoRes.Empleado.AreaAgencia == "-")
                            {
                                empleadoRes.Demografico = empleadoRes.Empleado.DivisonMarca;
                            }


                            empleadoRes.Abierta1 = item.EMP_R1;
                            empleadoRes.Abierta2 = item.EMP_R2;
                            empleadoRes.Abierta3 = item.EMP_R3;
                            empleadoRes.Abierta4 = item.EMP_R4;
                            empleadoRes.Abierta5 = item.EMP_R5;
                            empleadoRes.Abierta6 = item.EMP_R6;
                            empleadoRes.Abierta7 = item.EMP_R7;




                            result.Objects.Add(empleadoRes);
                            result.Correct = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            /**/
            //var result = BL.ReporteD4U.GetEstructuraForReporte(CompanyCateg.IdCompanyCategoria);
            var json = Json(result.Objects, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return new JsonResult { Data = json, MaxJsonLength = Int32.MaxValue };
        }
        public ActionResult ReporteAbiertas()
        {
            return View();
        }
        public ActionResult GetAll()
        {
            List<object> permisosEstructura = new List<object>();
            ViewBag.Permisos = Session["CompaniesPermisos"];
            permisosEstructura = ViewBag.Permisos;
            string companyDelAdminLog = Convert.ToString(Session["CompanyDelAdminLog"]);
            return View(BL.ReporteD4U.GetAll(permisosEstructura, companyDelAdminLog));
        }
        public ActionResult GetReportesByIdEncuesta(ML.Encuesta data)
        {
            var result = BL.ReporteD4U.GetReportesByIdEncuesta(data.IdEncuesta);

            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
        /*ActionMethod For Clima Laboral*/
        public ActionResult ReporteGlobalCL()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            return View(result);
        }
        public ActionResult ReporteGetAll()
        {
            ViewBag.Event = "Consulta de todos los registros";
            var result = BL.Reporte.GetAllResultados();

            List<ML.JsonReporte> jsonList = new List<ML.JsonReporte>();

            if (result.Objects != null)
            {
                foreach (ML.EmpleadoRespuesta emp in result.Objects)
                {
                    //resultados.Add(new SelectListItem { Text = "<tr>".ToString() });
                    //resultados.Add(new SelectListItem { Text = emp.Empleado.IdEmpleado.ToString() });
                    //resultados.Add(new SelectListItem { Text = emp.Empleado.ApellidoPaterno.ToString() });
                    //resultados.Add(new SelectListItem { Text = emp.Empleado.ApellidoMaterno.ToString() });
                    //resultados.Add(new SelectListItem { Text = emp.Empleado.Nombre.ToString() });
                    //resultados.Add(new SelectListItem { Text = "</tr>".ToString() });
                    ML.JsonReporte json = new ML.JsonReporte();
                    json.IdEmpleado = emp.Empleado.IdEmpleado;
                    json.ApellidoPaterno = emp.Empleado.ApellidoPaterno;
                    json.ApellidoMaterno = emp.Empleado.ApellidoMaterno;
                    json.Nombre = emp.Empleado.Nombre;
                    json.Puesto = emp.Empleado.Puesto;
                    json.FechaNaciemiento = emp.Empleado.FechaNaciemiento;
                    json.FechaAntiguedad = emp.Empleado.FechaAntiguedad;
                    json.Sexo = emp.Empleado.Sexo;
                    json.Correo = emp.Empleado.Correo;
                    json.TipoFuncion = emp.Empleado.Perfil.Descripcion;
                    json.CondicionTrabajo = emp.Empleado.CondicionTrabajo;
                    json.GradoAcademico = emp.Empleado.GradoAcademico;
                    json.UNIDAD = emp.Empleado.Departamento.Area.Company.CompanyCategoria.Descripcion;
                    json.DIRECION = emp.Empleado.Departamento.Area.Company.CompanyName;
                    json.AREA = emp.Empleado.Departamento.Area.Nombre;
                    json.DEPARTAMENTO = emp.Empleado.Departamento.Nombre;
                    json.SUBDEPARTAMENTO = emp.Empleado.Subdepartamento.Nombre;
                    json.EmpresaContratante = emp.Empleado.EmpresaContratante;
                    json.IdResponsableRH = emp.Empleado.IdResponsableRH;
                    json.NombreResponsableRH = emp.Empleado.NombreResponsableRH;
                    json.IdJefe = emp.Empleado.IdJefe;
                    json.NombreJefe = emp.Empleado.NombreJefe;
                    json.PuestoJefe = emp.Empleado.PuestoJefe;
                    json.IdRespinsableEstructura = emp.Empleado.IdRespinsableEstructura;
                    json.NombreResponsableEstrucutra = emp.Empleado.NombreResponsableEstrucutra;
                    json.ClaveAcceso = emp.Empleado.ClavesAcceso.ClaveAcceso;
                    json.RangoAntiguedad = emp.Empleado.RangoAntiguedad;
                    json.RangoEdad = emp.Empleado.RangoEdad;
                    json.EstatusENcuesta = emp.Empleado.EstatusEncuesta.Estatus;
                    json.EstatusEmpleado = emp.Empleado.EstatusEmpleado;

                    json.EMP_R1 = emp.EMP_R1; json.EMP_R2 = emp.EMP_R2; json.EMP_R3 = emp.EMP_R3; json.EMP_R4 = emp.EMP_R4; json.EMP_R5 = emp.EMP_R5; json.EMP_R6 = emp.EMP_R6; json.EMP_R7 = emp.EMP_R7;
                    json.EMP_R8 = emp.EMP_R8; json.EMP_R9 = emp.EMP_R9; json.EMP_R10 = emp.EMP_R10; json.EMP_R11 = emp.EMP_R11; json.EMP_R12 = emp.EMP_R12; json.EMP_R13 = emp.EMP_R13; json.EMP_R14 = emp.EMP_R14;
                    json.EMP_R15 = emp.EMP_R15; json.EMP_R16 = emp.EMP_R16; json.EMP_R17 = emp.EMP_R17; json.EMP_R18 = emp.EMP_R18; json.EMP_R19 = emp.EMP_R19; json.EMP_R20 = emp.EMP_R20; json.EMP_R21 = emp.EMP_R21;
                    json.EMP_R22 = emp.EMP_R22; json.EMP_R23 = emp.EMP_R23; json.EMP_R24 = emp.EMP_R24; json.EMP_R25 = emp.EMP_R25; json.EMP_R26 = emp.EMP_R26; json.EMP_R27 = emp.EMP_R27; json.EMP_R28 = emp.EMP_R28;
                    json.EMP_R29 = emp.EMP_R29; json.EMP_R30 = emp.EMP_R30; json.EMP_R31 = emp.EMP_R31; json.EMP_R32 = emp.EMP_R32; json.EMP_R33 = emp.EMP_R33; json.EMP_R34 = emp.EMP_R34; json.EMP_R35 = emp.EMP_R35;
                    json.EMP_R36 = emp.EMP_R36; json.EMP_R37 = emp.EMP_R37; json.EMP_R38 = emp.EMP_R38; json.EMP_R39 = emp.EMP_R39; json.EMP_R40 = emp.EMP_R40; json.EMP_R41 = emp.EMP_R41; json.EMP_R42 = emp.EMP_R42;
                    json.EMP_R43 = emp.EMP_R43; json.EMP_R44 = emp.EMP_R44; json.EMP_R45 = emp.EMP_R45; json.EMP_R46 = emp.EMP_R46; json.EMP_R47 = emp.EMP_R47; json.EMP_R48 = emp.EMP_R48; json.EMP_R49 = emp.EMP_R49;
                    json.EMP_R50 = emp.EMP_R50; json.EMP_R51 = emp.EMP_R51; json.EMP_R52 = emp.EMP_R52; json.EMP_R53 = emp.EMP_R53; json.EMP_R54 = emp.EMP_R54; json.EMP_R55 = emp.EMP_R55; json.EMP_R56 = emp.EMP_R56;
                    json.EMP_R57 = emp.EMP_R57;
                    json.EMP_R58 = emp.EMP_R58;
                    json.EMP_R59 = emp.EMP_R59;
                    json.EMP_R60 = emp.EMP_R60;

                    json.EMP_R61 = emp.EMP_R61;
                    json.EMP_R62 = emp.EMP_R62;
                    json.EMP_R63 = emp.EMP_R63;
                    json.EMP_R64 = emp.EMP_R64;
                    json.EMP_R65 = emp.EMP_R65;
                    json.EMP_R66 = emp.EMP_R66;
                    json.EMP_R67 = emp.EMP_R67;
                    json.EMP_R68 = emp.EMP_R68;
                    json.EMP_R69 = emp.EMP_R69;
                    json.EMP_R70 = emp.EMP_R70;
                    json.EMP_R71 = emp.EMP_R71;
                    json.EMP_R72 = emp.EMP_R72;
                    json.EMP_R73 = emp.EMP_R73;
                    json.EMP_R74 = emp.EMP_R74;
                    json.EMP_R75 = emp.EMP_R75;
                    json.EMP_R76 = emp.EMP_R76;
                    json.EMP_R77 = emp.EMP_R77;
                    json.EMP_R78 = emp.EMP_R78;
                    json.EMP_R79 = emp.EMP_R79;
                    json.EMP_R80 = emp.EMP_R80;

                    json.EMP_R81 = emp.EMP_R81;
                    json.EMP_R82 = emp.EMP_R82;
                    json.EMP_R83 = emp.EMP_R83;
                    json.EMP_R84 = emp.EMP_R84;
                    json.EMP_R85 = emp.EMP_R85;
                    json.EMP_R86 = emp.EMP_R86;

                    //Agregar EE

                    json.ARE_1 = emp.ARE_1;
                    json.ARE_2 = emp.ARE_2;
                    json.ARE_3 = emp.ARE_3;
                    json.ARE_4 = emp.ARE_4;
                    json.ARE_5 = emp.ARE_5;
                    json.ARE_6 = emp.ARE_6;
                    json.ARE_7 = emp.ARE_7;
                    json.ARE_8 = emp.ARE_8;
                    json.ARE_9 = emp.ARE_9;
                    json.ARE_10 = emp.ARE_10;
                    json.ARE_11 = emp.ARE_11;
                    json.ARE_12 = emp.ARE_12;
                    json.ARE_13 = emp.ARE_13;
                    json.ARE_14 = emp.ARE_14;
                    json.ARE_15 = emp.ARE_15;
                    json.ARE_16 = emp.ARE_16;
                    json.ARE_17 = emp.ARE_17;
                    json.ARE_18 = emp.ARE_18;
                    json.ARE_19 = emp.ARE_19;
                    json.ARE_20 = emp.ARE_20;


                    json.ARE_21 = emp.ARE_21;
                    json.ARE_22 = emp.ARE_22;
                    json.ARE_23 = emp.ARE_23;
                    json.ARE_24 = emp.ARE_24;
                    json.ARE_25 = emp.ARE_25;
                    json.ARE_26 = emp.ARE_26;
                    json.ARE_27 = emp.ARE_27;
                    json.ARE_28 = emp.ARE_28;
                    json.ARE_29 = emp.ARE_29;
                    json.ARE_30 = emp.ARE_30;
                    json.ARE_31 = emp.ARE_31;
                    json.ARE_32 = emp.ARE_32;
                    json.ARE_33 = emp.ARE_33;
                    json.ARE_34 = emp.ARE_34;
                    json.ARE_35 = emp.ARE_35;
                    json.ARE_36 = emp.ARE_36;
                    json.ARE_37 = emp.ARE_37;
                    json.ARE_38 = emp.ARE_38;
                    json.ARE_39 = emp.ARE_39;
                    json.ARE_40 = emp.ARE_40;

                    json.ARE_41 = emp.ARE_41;
                    json.ARE_42 = emp.ARE_42;
                    json.ARE_43 = emp.ARE_43;
                    json.ARE_44 = emp.ARE_44;
                    json.ARE_45 = emp.ARE_45;
                    json.ARE_46 = emp.ARE_46;
                    json.ARE_47 = emp.ARE_47;
                    json.ARE_48 = emp.ARE_48;
                    json.ARE_49 = emp.ARE_49;
                    json.ARE_50 = emp.ARE_50;
                    json.ARE_51 = emp.ARE_51;
                    json.ARE_52 = emp.ARE_52;
                    json.ARE_53 = emp.ARE_53;
                    json.ARE_54 = emp.ARE_54;
                    json.ARE_55 = emp.ARE_55;
                    json.ARE_56 = emp.ARE_56;
                    json.ARE_57 = emp.ARE_57;
                    json.ARE_58 = emp.ARE_58;
                    json.ARE_59 = emp.ARE_59;
                    json.ARE_60 = emp.ARE_60;

                    json.ARE_61 = emp.ARE_61;
                    json.ARE_62 = emp.ARE_62;
                    json.ARE_63 = emp.ARE_63;
                    json.ARE_64 = emp.ARE_64;
                    json.ARE_65 = emp.ARE_65;
                    json.ARE_66 = emp.ARE_66;
                    json.ARE_67 = emp.ARE_67;
                    json.ARE_68 = emp.ARE_68;
                    json.ARE_69 = emp.ARE_69;
                    json.ARE_70 = emp.ARE_70;
                    json.ARE_71 = emp.ARE_71;
                    json.ARE_72 = emp.ARE_72;
                    json.ARE_73 = emp.ARE_73;
                    json.ARE_74 = emp.ARE_74;
                    json.ARE_75 = emp.ARE_75;
                    json.ARE_76 = emp.ARE_76;
                    json.ARE_77 = emp.ARE_77;
                    json.ARE_78 = emp.ARE_78;
                    json.ARE_79 = emp.ARE_79;
                    json.ARE_80 = emp.ARE_80;

                    json.ARE_81 = emp.ARE_81;
                    json.ARE_82 = emp.ARE_82;
                    json.ARE_83 = emp.ARE_83;
                    json.ARE_84 = emp.ARE_84;
                    json.ARE_85 = emp.ARE_85;
                    json.ARE_86 = emp.ARE_86;


                    json.ESPECIAL_OPEN = emp.ESPECIAL_OPEN;
                    json.CAMBIAR_OPEN = emp.CAMBIAR_OPEN;
                    json.FORT_JEFE = emp.FORT_JEFE;
                    json.OPORT_JEFE = emp.OPORT_JEFE;
                    json.MOTIVA_TRAB = emp.MOTIVA_TRAB;
                    json.DEJAR_EMP = emp.DEJAR_EMP;
                    json.PRESION = emp.PRESION;
                    json.ANTIGUEDADR = emp.ANTIGUEDAD;
                    json.RANGO_EDADR = emp.RANGO_EDAD;
                    json.CONDICIONR = emp.CONDICION;
                    json.SEXOR = emp.SEXO;
                    json.ACADEMICOR = emp.ACADEMICO;
                    json.FUNCIONR = emp.FUNCION;
                    json.UNIDADRES = emp.UNIDAD;
                    json.DIRECIONRES = emp.DIRECION;
                    json.AREARES = emp.AREA;
                    json.DEPARTAMENTORES = emp.DEPARTAMENTO;



                    jsonList.Add(json);//El nuevo valor sobreescrbe al anterior quedando n cantidda de veces el ultimo
                }

            }
            var data = JsonConvert.SerializeObject(jsonList);

            return Content(data);

        }
        public ActionResult ReporteByIdEmpleado(int IdEmpleado)
        {
            ViewBag.Event = "Consulta de registros tomando en cuenta el Id del Empleado";
            var result = BL.Reporte.GetResultadosByIdEmpleado(IdEmpleado);
            if (result.Objects.Count == 0)
            {
                ViewBag.Event = "No se encontro ningun registro que coincida con el Id de Empleado proporcionado";
            }

            List<ML.JsonReporte> jsonList = new List<ML.JsonReporte>();

            if (result.Objects != null)
            {
                foreach (ML.EmpleadoRespuesta emp in result.Objects)
                {
                    //resultados.Add(new SelectListItem { Text = "<tr>".ToString() });
                    //resultados.Add(new SelectListItem { Text = emp.Empleado.IdEmpleado.ToString() });
                    //resultados.Add(new SelectListItem { Text = emp.Empleado.ApellidoPaterno.ToString() });
                    //resultados.Add(new SelectListItem { Text = emp.Empleado.ApellidoMaterno.ToString() });
                    //resultados.Add(new SelectListItem { Text = emp.Empleado.Nombre.ToString() });
                    //resultados.Add(new SelectListItem { Text = "</tr>".ToString() });
                    ML.JsonReporte json = new ML.JsonReporte();
                    json.IdEmpleado = emp.Empleado.IdEmpleado;
                    json.ApellidoPaterno = emp.Empleado.ApellidoPaterno;
                    json.ApellidoMaterno = emp.Empleado.ApellidoMaterno;
                    json.Nombre = emp.Empleado.Nombre;
                    json.Puesto = emp.Empleado.Puesto;
                    json.FechaNaciemiento = emp.Empleado.FechaNaciemiento;
                    json.FechaAntiguedad = emp.Empleado.FechaAntiguedad;
                    json.Sexo = emp.Empleado.Sexo;
                    json.Correo = emp.Empleado.Correo;
                    json.TipoFuncion = emp.Empleado.Perfil.Descripcion;
                    json.CondicionTrabajo = emp.Empleado.CondicionTrabajo;
                    json.GradoAcademico = emp.Empleado.GradoAcademico;
                    json.UNIDAD = emp.Empleado.Departamento.Area.Company.CompanyCategoria.Descripcion;
                    json.DIRECION = emp.Empleado.Departamento.Area.Company.CompanyName;
                    json.AREA = emp.Empleado.Departamento.Area.Nombre;
                    json.DEPARTAMENTO = emp.Empleado.Departamento.Nombre;
                    json.SUBDEPARTAMENTO = emp.Empleado.Subdepartamento.Nombre;
                    json.EmpresaContratante = emp.Empleado.EmpresaContratante;
                    json.IdResponsableRH = emp.Empleado.IdResponsableRH;
                    json.NombreResponsableRH = emp.Empleado.NombreResponsableRH;
                    json.IdJefe = emp.Empleado.IdJefe;
                    json.NombreJefe = emp.Empleado.NombreJefe;
                    json.PuestoJefe = emp.Empleado.PuestoJefe;
                    json.IdRespinsableEstructura = emp.Empleado.IdRespinsableEstructura;
                    json.NombreResponsableEstrucutra = emp.Empleado.NombreResponsableEstrucutra;
                    json.ClaveAcceso = emp.Empleado.ClavesAcceso.ClaveAcceso;
                    json.RangoAntiguedad = emp.Empleado.RangoAntiguedad;
                    json.RangoEdad = emp.Empleado.RangoEdad;
                    json.EstatusENcuesta = emp.Empleado.EstatusEncuesta.Estatus;
                    json.EstatusEmpleado = emp.Empleado.EstatusEmpleado;

                    json.EMP_R1 = emp.EMP_R1; json.EMP_R2 = emp.EMP_R2; json.EMP_R3 = emp.EMP_R3; json.EMP_R4 = emp.EMP_R4; json.EMP_R5 = emp.EMP_R5; json.EMP_R6 = emp.EMP_R6; json.EMP_R7 = emp.EMP_R7;
                    json.EMP_R8 = emp.EMP_R8; json.EMP_R9 = emp.EMP_R9; json.EMP_R10 = emp.EMP_R10; json.EMP_R11 = emp.EMP_R11; json.EMP_R12 = emp.EMP_R12; json.EMP_R13 = emp.EMP_R13; json.EMP_R14 = emp.EMP_R14;
                    json.EMP_R15 = emp.EMP_R15; json.EMP_R16 = emp.EMP_R16; json.EMP_R17 = emp.EMP_R17; json.EMP_R18 = emp.EMP_R18; json.EMP_R19 = emp.EMP_R19; json.EMP_R20 = emp.EMP_R20; json.EMP_R21 = emp.EMP_R21;
                    json.EMP_R22 = emp.EMP_R22; json.EMP_R23 = emp.EMP_R23; json.EMP_R24 = emp.EMP_R24; json.EMP_R25 = emp.EMP_R25; json.EMP_R26 = emp.EMP_R26; json.EMP_R27 = emp.EMP_R27; json.EMP_R28 = emp.EMP_R28;
                    json.EMP_R29 = emp.EMP_R29; json.EMP_R30 = emp.EMP_R30; json.EMP_R31 = emp.EMP_R31; json.EMP_R32 = emp.EMP_R32; json.EMP_R33 = emp.EMP_R33; json.EMP_R34 = emp.EMP_R34; json.EMP_R35 = emp.EMP_R35;
                    json.EMP_R36 = emp.EMP_R36; json.EMP_R37 = emp.EMP_R37; json.EMP_R38 = emp.EMP_R38; json.EMP_R39 = emp.EMP_R39; json.EMP_R40 = emp.EMP_R40; json.EMP_R41 = emp.EMP_R41; json.EMP_R42 = emp.EMP_R42;
                    json.EMP_R43 = emp.EMP_R43; json.EMP_R44 = emp.EMP_R44; json.EMP_R45 = emp.EMP_R45; json.EMP_R46 = emp.EMP_R46; json.EMP_R47 = emp.EMP_R47; json.EMP_R48 = emp.EMP_R48; json.EMP_R49 = emp.EMP_R49;
                    json.EMP_R50 = emp.EMP_R50; json.EMP_R51 = emp.EMP_R51; json.EMP_R52 = emp.EMP_R52; json.EMP_R53 = emp.EMP_R53; json.EMP_R54 = emp.EMP_R54; json.EMP_R55 = emp.EMP_R55; json.EMP_R56 = emp.EMP_R56;
                    json.EMP_R57 = emp.EMP_R57;
                    json.EMP_R58 = emp.EMP_R58;
                    json.EMP_R59 = emp.EMP_R59;
                    json.EMP_R60 = emp.EMP_R60;

                    json.EMP_R61 = emp.EMP_R61;
                    json.EMP_R62 = emp.EMP_R62;
                    json.EMP_R63 = emp.EMP_R63;
                    json.EMP_R64 = emp.EMP_R64;
                    json.EMP_R65 = emp.EMP_R65;
                    json.EMP_R66 = emp.EMP_R66;
                    json.EMP_R67 = emp.EMP_R67;
                    json.EMP_R68 = emp.EMP_R68;
                    json.EMP_R69 = emp.EMP_R69;
                    json.EMP_R70 = emp.EMP_R70;
                    json.EMP_R71 = emp.EMP_R71;
                    json.EMP_R72 = emp.EMP_R72;
                    json.EMP_R73 = emp.EMP_R73;
                    json.EMP_R74 = emp.EMP_R74;
                    json.EMP_R75 = emp.EMP_R75;
                    json.EMP_R76 = emp.EMP_R76;
                    json.EMP_R77 = emp.EMP_R77;
                    json.EMP_R78 = emp.EMP_R78;
                    json.EMP_R79 = emp.EMP_R79;
                    json.EMP_R80 = emp.EMP_R80;

                    json.EMP_R81 = emp.EMP_R81;
                    json.EMP_R82 = emp.EMP_R82;
                    json.EMP_R83 = emp.EMP_R83;
                    json.EMP_R84 = emp.EMP_R84;
                    json.EMP_R85 = emp.EMP_R85;
                    json.EMP_R86 = emp.EMP_R86;

                    //Agregar EE

                    json.ARE_1 = emp.ARE_1;
                    json.ARE_2 = emp.ARE_2;
                    json.ARE_3 = emp.ARE_3;
                    json.ARE_4 = emp.ARE_4;
                    json.ARE_5 = emp.ARE_5;
                    json.ARE_6 = emp.ARE_6;
                    json.ARE_7 = emp.ARE_7;
                    json.ARE_8 = emp.ARE_8;
                    json.ARE_9 = emp.ARE_9;
                    json.ARE_10 = emp.ARE_10;
                    json.ARE_11 = emp.ARE_11;
                    json.ARE_12 = emp.ARE_12;
                    json.ARE_13 = emp.ARE_13;
                    json.ARE_14 = emp.ARE_14;
                    json.ARE_15 = emp.ARE_15;
                    json.ARE_16 = emp.ARE_16;
                    json.ARE_17 = emp.ARE_17;
                    json.ARE_18 = emp.ARE_18;
                    json.ARE_19 = emp.ARE_19;
                    json.ARE_20 = emp.ARE_20;


                    json.ARE_21 = emp.ARE_21;
                    json.ARE_22 = emp.ARE_22;
                    json.ARE_23 = emp.ARE_23;
                    json.ARE_24 = emp.ARE_24;
                    json.ARE_25 = emp.ARE_25;
                    json.ARE_26 = emp.ARE_26;
                    json.ARE_27 = emp.ARE_27;
                    json.ARE_28 = emp.ARE_28;
                    json.ARE_29 = emp.ARE_29;
                    json.ARE_30 = emp.ARE_30;
                    json.ARE_31 = emp.ARE_31;
                    json.ARE_32 = emp.ARE_32;
                    json.ARE_33 = emp.ARE_33;
                    json.ARE_34 = emp.ARE_34;
                    json.ARE_35 = emp.ARE_35;
                    json.ARE_36 = emp.ARE_36;
                    json.ARE_37 = emp.ARE_37;
                    json.ARE_38 = emp.ARE_38;
                    json.ARE_39 = emp.ARE_39;
                    json.ARE_40 = emp.ARE_40;

                    json.ARE_41 = emp.ARE_41;
                    json.ARE_42 = emp.ARE_42;
                    json.ARE_43 = emp.ARE_43;
                    json.ARE_44 = emp.ARE_44;
                    json.ARE_45 = emp.ARE_45;
                    json.ARE_46 = emp.ARE_46;
                    json.ARE_47 = emp.ARE_47;
                    json.ARE_48 = emp.ARE_48;
                    json.ARE_49 = emp.ARE_49;
                    json.ARE_50 = emp.ARE_50;
                    json.ARE_51 = emp.ARE_51;
                    json.ARE_52 = emp.ARE_52;
                    json.ARE_53 = emp.ARE_53;
                    json.ARE_54 = emp.ARE_54;
                    json.ARE_55 = emp.ARE_55;
                    json.ARE_56 = emp.ARE_56;
                    json.ARE_57 = emp.ARE_57;
                    json.ARE_58 = emp.ARE_58;
                    json.ARE_59 = emp.ARE_59;
                    json.ARE_60 = emp.ARE_60;

                    json.ARE_61 = emp.ARE_61;
                    json.ARE_62 = emp.ARE_62;
                    json.ARE_63 = emp.ARE_63;
                    json.ARE_64 = emp.ARE_64;
                    json.ARE_65 = emp.ARE_65;
                    json.ARE_66 = emp.ARE_66;
                    json.ARE_67 = emp.ARE_67;
                    json.ARE_68 = emp.ARE_68;
                    json.ARE_69 = emp.ARE_69;
                    json.ARE_70 = emp.ARE_70;
                    json.ARE_71 = emp.ARE_71;
                    json.ARE_72 = emp.ARE_72;
                    json.ARE_73 = emp.ARE_73;
                    json.ARE_74 = emp.ARE_74;
                    json.ARE_75 = emp.ARE_75;
                    json.ARE_76 = emp.ARE_76;
                    json.ARE_77 = emp.ARE_77;
                    json.ARE_78 = emp.ARE_78;
                    json.ARE_79 = emp.ARE_79;
                    json.ARE_80 = emp.ARE_80;

                    json.ARE_81 = emp.ARE_81;
                    json.ARE_82 = emp.ARE_82;
                    json.ARE_83 = emp.ARE_83;
                    json.ARE_84 = emp.ARE_84;
                    json.ARE_85 = emp.ARE_85;
                    json.ARE_86 = emp.ARE_86;

                    json.ESPECIAL_OPEN = emp.ESPECIAL_OPEN;
                    json.CAMBIAR_OPEN = emp.CAMBIAR_OPEN;
                    json.FORT_JEFE = emp.FORT_JEFE;
                    json.OPORT_JEFE = emp.OPORT_JEFE;
                    json.MOTIVA_TRAB = emp.MOTIVA_TRAB;
                    json.DEJAR_EMP = emp.DEJAR_EMP;
                    json.PRESION = emp.PRESION;
                    json.ANTIGUEDADR = emp.ANTIGUEDAD;
                    json.RANGO_EDADR = emp.RANGO_EDAD;
                    json.CONDICIONR = emp.CONDICION;
                    json.SEXOR = emp.SEXO;
                    json.ACADEMICOR = emp.ACADEMICO;
                    json.FUNCIONR = emp.FUNCION;
                    json.UNIDADRES = emp.UNIDAD;
                    json.DIRECIONRES = emp.DIRECION;
                    json.AREARES = emp.AREA;
                    json.DEPARTAMENTORES = emp.DEPARTAMENTO;


                    jsonList.Add(json);//El nuevo valor sobreescrbe al anterior quedando n cantidda de veces el ultimo
                }

            }
            var data = JsonConvert.SerializeObject(jsonList);

            return Content(data);

        }
        public ActionResult ReporteGetByUnidadNegocio(List<string> listUNegocio, string idBD)
        {
            int _idBD = Convert.ToInt32(idBD);
            var result = BL.Reporte.GetResultadosByUnidadNegocio(listUNegocio, _idBD);
            
            List<ML.JsonReporte> jsonList = new List<ML.JsonReporte>();

            if (result.Objects != null)
            {
                foreach (ML.EmpleadoRespuesta emp in result.Objects)
                {
                    ML.JsonReporte json = new ML.JsonReporte();
                    json.IdEmpleado = emp.Empleado.IdEmpleado;
                    json.ApellidoPaterno = emp.Empleado.ApellidoPaterno;
                    json.ApellidoMaterno = emp.Empleado.ApellidoMaterno;
                    json.Nombre = emp.Empleado.Nombre;
                    json.Puesto = emp.Empleado.Puesto;
                    json.FechaNaciemiento = emp.Empleado.FechaNaciemiento;
                    json.FechaAntiguedad = emp.Empleado.FechaAntiguedad;
                    json.Sexo = emp.Empleado.Sexo;
                    json.Correo = emp.Empleado.Correo;
                    json.TipoFuncion = emp.Empleado.Perfil.Descripcion;
                    json.CondicionTrabajo = emp.Empleado.CondicionTrabajo;
                    json.GradoAcademico = emp.Empleado.GradoAcademico;
                    json.UNIDAD = emp.Empleado.Departamento.Area.Company.CompanyCategoria.Descripcion;
                    json.DIRECION = emp.Empleado.Departamento.Area.Company.CompanyName;
                    json.AREA = emp.Empleado.Departamento.Area.Nombre;
                    json.DEPARTAMENTO = emp.Empleado.Departamento.Nombre;
                    json.SUBDEPARTAMENTO = emp.Empleado.Subdepartamento.Nombre;
                    json.EmpresaContratante = emp.Empleado.EmpresaContratante;
                    json.IdResponsableRH = emp.Empleado.IdResponsableRH;
                    json.NombreResponsableRH = emp.Empleado.NombreResponsableRH;
                    json.IdJefe = emp.Empleado.IdJefe;
                    json.NombreJefe = emp.Empleado.NombreJefe;
                    json.PuestoJefe = emp.Empleado.PuestoJefe;
                    json.IdRespinsableEstructura = emp.Empleado.IdRespinsableEstructura;
                    json.NombreResponsableEstrucutra = emp.Empleado.NombreResponsableEstrucutra;
                    json.ClaveAcceso = emp.Empleado.ClavesAcceso.ClaveAcceso;
                    json.RangoAntiguedad = emp.Empleado.RangoAntiguedad;
                    json.RangoEdad = emp.Empleado.RangoEdad;
                    json.EstatusENcuesta = emp.Empleado.EstatusEncuesta.Estatus;
                    json.EstatusEmpleado = emp.Empleado.EstatusEmpleado;

                    json.EMP_R1 = emp.EMP_R1; json.EMP_R2 = emp.EMP_R2; json.EMP_R3 = emp.EMP_R3; json.EMP_R4 = emp.EMP_R4; json.EMP_R5 = emp.EMP_R5; json.EMP_R6 = emp.EMP_R6; json.EMP_R7 = emp.EMP_R7;
                    json.EMP_R8 = emp.EMP_R8; json.EMP_R9 = emp.EMP_R9; json.EMP_R10 = emp.EMP_R10; json.EMP_R11 = emp.EMP_R11; json.EMP_R12 = emp.EMP_R12; json.EMP_R13 = emp.EMP_R13; json.EMP_R14 = emp.EMP_R14;
                    json.EMP_R15 = emp.EMP_R15; json.EMP_R16 = emp.EMP_R16; json.EMP_R17 = emp.EMP_R17; json.EMP_R18 = emp.EMP_R18; json.EMP_R19 = emp.EMP_R19; json.EMP_R20 = emp.EMP_R20; json.EMP_R21 = emp.EMP_R21;
                    json.EMP_R22 = emp.EMP_R22; json.EMP_R23 = emp.EMP_R23; json.EMP_R24 = emp.EMP_R24; json.EMP_R25 = emp.EMP_R25; json.EMP_R26 = emp.EMP_R26; json.EMP_R27 = emp.EMP_R27; json.EMP_R28 = emp.EMP_R28;
                    json.EMP_R29 = emp.EMP_R29; json.EMP_R30 = emp.EMP_R30; json.EMP_R31 = emp.EMP_R31; json.EMP_R32 = emp.EMP_R32; json.EMP_R33 = emp.EMP_R33; json.EMP_R34 = emp.EMP_R34; json.EMP_R35 = emp.EMP_R35;
                    json.EMP_R36 = emp.EMP_R36; json.EMP_R37 = emp.EMP_R37; json.EMP_R38 = emp.EMP_R38; json.EMP_R39 = emp.EMP_R39; json.EMP_R40 = emp.EMP_R40; json.EMP_R41 = emp.EMP_R41; json.EMP_R42 = emp.EMP_R42;
                    json.EMP_R43 = emp.EMP_R43; json.EMP_R44 = emp.EMP_R44; json.EMP_R45 = emp.EMP_R45; json.EMP_R46 = emp.EMP_R46; json.EMP_R47 = emp.EMP_R47; json.EMP_R48 = emp.EMP_R48; json.EMP_R49 = emp.EMP_R49;
                    json.EMP_R50 = emp.EMP_R50; json.EMP_R51 = emp.EMP_R51; json.EMP_R52 = emp.EMP_R52; json.EMP_R53 = emp.EMP_R53; json.EMP_R54 = emp.EMP_R54; json.EMP_R55 = emp.EMP_R55; json.EMP_R56 = emp.EMP_R56;
                    json.EMP_R57 = emp.EMP_R57;
                    json.EMP_R58 = emp.EMP_R58;
                    json.EMP_R59 = emp.EMP_R59;
                    json.EMP_R60 = emp.EMP_R60;

                    json.EMP_R61 = emp.EMP_R61;
                    json.EMP_R62 = emp.EMP_R62;
                    json.EMP_R63 = emp.EMP_R63;
                    json.EMP_R64 = emp.EMP_R64;
                    json.EMP_R65 = emp.EMP_R65;
                    json.EMP_R66 = emp.EMP_R66;
                    json.EMP_R67 = emp.EMP_R67;
                    json.EMP_R68 = emp.EMP_R68;
                    json.EMP_R69 = emp.EMP_R69;
                    json.EMP_R70 = emp.EMP_R70;
                    json.EMP_R71 = emp.EMP_R71;
                    json.EMP_R72 = emp.EMP_R72;
                    json.EMP_R73 = emp.EMP_R73;
                    json.EMP_R74 = emp.EMP_R74;
                    json.EMP_R75 = emp.EMP_R75;
                    json.EMP_R76 = emp.EMP_R76;
                    json.EMP_R77 = emp.EMP_R77;
                    json.EMP_R78 = emp.EMP_R78;
                    json.EMP_R79 = emp.EMP_R79;
                    json.EMP_R80 = emp.EMP_R80;

                    json.EMP_R81 = emp.EMP_R81;
                    json.EMP_R82 = emp.EMP_R82;
                    json.EMP_R83 = emp.EMP_R83;
                    json.EMP_R84 = emp.EMP_R84;
                    json.EMP_R85 = emp.EMP_R85;
                    json.EMP_R86 = emp.EMP_R86;

                    //Agregar EE

                    json.ARE_1 = emp.ARE_1;
                    json.ARE_2 = emp.ARE_2;
                    json.ARE_3 = emp.ARE_3;
                    json.ARE_4 = emp.ARE_4;
                    json.ARE_5 = emp.ARE_5;
                    json.ARE_6 = emp.ARE_6;
                    json.ARE_7 = emp.ARE_7;
                    json.ARE_8 = emp.ARE_8;
                    json.ARE_9 = emp.ARE_9;
                    json.ARE_10 = emp.ARE_10;
                    json.ARE_11 = emp.ARE_11;
                    json.ARE_12 = emp.ARE_12;
                    json.ARE_13 = emp.ARE_13;
                    json.ARE_14 = emp.ARE_14;
                    json.ARE_15 = emp.ARE_15;
                    json.ARE_16 = emp.ARE_16;
                    json.ARE_17 = emp.ARE_17;
                    json.ARE_18 = emp.ARE_18;
                    json.ARE_19 = emp.ARE_19;
                    json.ARE_20 = emp.ARE_20;


                    json.ARE_21 = emp.ARE_21;
                    json.ARE_22 = emp.ARE_22;
                    json.ARE_23 = emp.ARE_23;
                    json.ARE_24 = emp.ARE_24;
                    json.ARE_25 = emp.ARE_25;
                    json.ARE_26 = emp.ARE_26;
                    json.ARE_27 = emp.ARE_27;
                    json.ARE_28 = emp.ARE_28;
                    json.ARE_29 = emp.ARE_29;
                    json.ARE_30 = emp.ARE_30;
                    json.ARE_31 = emp.ARE_31;
                    json.ARE_32 = emp.ARE_32;
                    json.ARE_33 = emp.ARE_33;
                    json.ARE_34 = emp.ARE_34;
                    json.ARE_35 = emp.ARE_35;
                    json.ARE_36 = emp.ARE_36;
                    json.ARE_37 = emp.ARE_37;
                    json.ARE_38 = emp.ARE_38;
                    json.ARE_39 = emp.ARE_39;
                    json.ARE_40 = emp.ARE_40;

                    json.ARE_41 = emp.ARE_41;
                    json.ARE_42 = emp.ARE_42;
                    json.ARE_43 = emp.ARE_43;
                    json.ARE_44 = emp.ARE_44;
                    json.ARE_45 = emp.ARE_45;
                    json.ARE_46 = emp.ARE_46;
                    json.ARE_47 = emp.ARE_47;
                    json.ARE_48 = emp.ARE_48;
                    json.ARE_49 = emp.ARE_49;
                    json.ARE_50 = emp.ARE_50;
                    json.ARE_51 = emp.ARE_51;
                    json.ARE_52 = emp.ARE_52;
                    json.ARE_53 = emp.ARE_53;
                    json.ARE_54 = emp.ARE_54;
                    json.ARE_55 = emp.ARE_55;
                    json.ARE_56 = emp.ARE_56;
                    json.ARE_57 = emp.ARE_57;
                    json.ARE_58 = emp.ARE_58;
                    json.ARE_59 = emp.ARE_59;
                    json.ARE_60 = emp.ARE_60;

                    json.ARE_61 = emp.ARE_61;
                    json.ARE_62 = emp.ARE_62;
                    json.ARE_63 = emp.ARE_63;
                    json.ARE_64 = emp.ARE_64;
                    json.ARE_65 = emp.ARE_65;
                    json.ARE_66 = emp.ARE_66;
                    json.ARE_67 = emp.ARE_67;
                    json.ARE_68 = emp.ARE_68;
                    json.ARE_69 = emp.ARE_69;
                    json.ARE_70 = emp.ARE_70;
                    json.ARE_71 = emp.ARE_71;
                    json.ARE_72 = emp.ARE_72;
                    json.ARE_73 = emp.ARE_73;
                    json.ARE_74 = emp.ARE_74;
                    json.ARE_75 = emp.ARE_75;
                    json.ARE_76 = emp.ARE_76;
                    json.ARE_77 = emp.ARE_77;
                    json.ARE_78 = emp.ARE_78;
                    json.ARE_79 = emp.ARE_79;
                    json.ARE_80 = emp.ARE_80;

                    json.ARE_81 = emp.ARE_81;
                    json.ARE_82 = emp.ARE_82;
                    json.ARE_83 = emp.ARE_83;
                    json.ARE_84 = emp.ARE_84;
                    json.ARE_85 = emp.ARE_85;
                    json.ARE_86 = emp.ARE_86;

                    json.ESPECIAL_OPEN = emp.ESPECIAL_OPEN;
                    json.CAMBIAR_OPEN = emp.CAMBIAR_OPEN;
                    json.FORT_JEFE = emp.FORT_JEFE;
                    json.OPORT_JEFE = emp.OPORT_JEFE;
                    json.MOTIVA_TRAB = emp.MOTIVA_TRAB;
                    json.DEJAR_EMP = emp.DEJAR_EMP;
                    json.PRESION = emp.PRESION;
                    json.ANTIGUEDADR = emp.ANTIGUEDAD;
                    json.RANGO_EDADR = emp.RANGO_EDAD;
                    json.CONDICIONR = emp.CONDICION;
                    json.SEXOR = emp.SEXO;
                    json.ACADEMICOR = emp.ACADEMICO;
                    json.FUNCIONR = emp.FUNCION;
                    json.UNIDADRES = emp.UNIDAD;
                    json.DIRECIONRES = emp.DIRECION;
                    json.AREARES = emp.AREA;
                    json.DEPARTAMENTORES = emp.DEPARTAMENTO;


                    jsonList.Add(json);//El nuevo valor sobreescrbe al anterior quedando n cantidda de veces el ultimo
                }

            }
            var data = JsonConvert.SerializeObject(jsonList);

            return Content(data);
        }
        public ActionResult ReporteGetByCompany(string Company)
        {
            //Enviar Catalogo de unidades de negocio usando viewbag
            var result = BL.Reporte.GetResultadosByCompany(Company);

            //Llenar mi propiedad json
            List<ML.JsonReporte> jsonList = new List<ML.JsonReporte>();

            if (result.Objects != null)
            {
                foreach (ML.EmpleadoRespuesta emp in result.Objects)
                {

                    ML.JsonReporte json = new ML.JsonReporte();
                    json.IdEmpleado = emp.Empleado.IdEmpleado;
                    json.ApellidoPaterno = emp.Empleado.ApellidoPaterno;
                    json.ApellidoMaterno = emp.Empleado.ApellidoMaterno;
                    json.Nombre = emp.Empleado.Nombre;
                    json.Puesto = emp.Empleado.Puesto;
                    json.FechaNaciemiento = emp.Empleado.FechaNaciemiento;
                    json.FechaAntiguedad = emp.Empleado.FechaAntiguedad;
                    json.Sexo = emp.Empleado.Sexo;
                    json.Correo = emp.Empleado.Correo;
                    json.TipoFuncion = emp.Empleado.Perfil.Descripcion;
                    json.CondicionTrabajo = emp.Empleado.CondicionTrabajo;
                    json.GradoAcademico = emp.Empleado.GradoAcademico;
                    json.UNIDAD = emp.Empleado.Departamento.Area.Company.CompanyCategoria.Descripcion;
                    json.DIRECION = emp.Empleado.Departamento.Area.Company.CompanyName;
                    json.AREA = emp.Empleado.Departamento.Area.Nombre;
                    json.DEPARTAMENTO = emp.Empleado.Departamento.Nombre;
                    json.SUBDEPARTAMENTO = emp.Empleado.Subdepartamento.Nombre;
                    json.EmpresaContratante = emp.Empleado.EmpresaContratante;
                    json.IdResponsableRH = emp.Empleado.IdResponsableRH;
                    json.NombreResponsableRH = emp.Empleado.NombreResponsableRH;
                    json.IdJefe = emp.Empleado.IdJefe;
                    json.NombreJefe = emp.Empleado.NombreJefe;
                    json.PuestoJefe = emp.Empleado.PuestoJefe;
                    json.IdRespinsableEstructura = emp.Empleado.IdRespinsableEstructura;
                    json.NombreResponsableEstrucutra = emp.Empleado.NombreResponsableEstrucutra;
                    json.ClaveAcceso = emp.Empleado.ClavesAcceso.ClaveAcceso;
                    json.RangoAntiguedad = emp.Empleado.RangoAntiguedad;
                    json.RangoEdad = emp.Empleado.RangoEdad;
                    json.EstatusENcuesta = emp.Empleado.EstatusEncuesta.Estatus;
                    json.EstatusEmpleado = emp.Empleado.EstatusEmpleado;

                    json.EMP_R1 = emp.EMP_R1; json.EMP_R2 = emp.EMP_R2; json.EMP_R3 = emp.EMP_R3; json.EMP_R4 = emp.EMP_R4; json.EMP_R5 = emp.EMP_R5; json.EMP_R6 = emp.EMP_R6; json.EMP_R7 = emp.EMP_R7;
                    json.EMP_R8 = emp.EMP_R8; json.EMP_R9 = emp.EMP_R9; json.EMP_R10 = emp.EMP_R10; json.EMP_R11 = emp.EMP_R11; json.EMP_R12 = emp.EMP_R12; json.EMP_R13 = emp.EMP_R13; json.EMP_R14 = emp.EMP_R14;
                    json.EMP_R15 = emp.EMP_R15; json.EMP_R16 = emp.EMP_R16; json.EMP_R17 = emp.EMP_R17; json.EMP_R18 = emp.EMP_R18; json.EMP_R19 = emp.EMP_R19; json.EMP_R20 = emp.EMP_R20; json.EMP_R21 = emp.EMP_R21;
                    json.EMP_R22 = emp.EMP_R22; json.EMP_R23 = emp.EMP_R23; json.EMP_R24 = emp.EMP_R24; json.EMP_R25 = emp.EMP_R25; json.EMP_R26 = emp.EMP_R26; json.EMP_R27 = emp.EMP_R27; json.EMP_R28 = emp.EMP_R28;
                    json.EMP_R29 = emp.EMP_R29; json.EMP_R30 = emp.EMP_R30; json.EMP_R31 = emp.EMP_R31; json.EMP_R32 = emp.EMP_R32; json.EMP_R33 = emp.EMP_R33; json.EMP_R34 = emp.EMP_R34; json.EMP_R35 = emp.EMP_R35;
                    json.EMP_R36 = emp.EMP_R36; json.EMP_R37 = emp.EMP_R37; json.EMP_R38 = emp.EMP_R38; json.EMP_R39 = emp.EMP_R39; json.EMP_R40 = emp.EMP_R40; json.EMP_R41 = emp.EMP_R41; json.EMP_R42 = emp.EMP_R42;
                    json.EMP_R43 = emp.EMP_R43; json.EMP_R44 = emp.EMP_R44; json.EMP_R45 = emp.EMP_R45; json.EMP_R46 = emp.EMP_R46; json.EMP_R47 = emp.EMP_R47; json.EMP_R48 = emp.EMP_R48; json.EMP_R49 = emp.EMP_R49;
                    json.EMP_R50 = emp.EMP_R50; json.EMP_R51 = emp.EMP_R51; json.EMP_R52 = emp.EMP_R52; json.EMP_R53 = emp.EMP_R53; json.EMP_R54 = emp.EMP_R54; json.EMP_R55 = emp.EMP_R55; json.EMP_R56 = emp.EMP_R56;
                    json.EMP_R57 = emp.EMP_R57;
                    json.EMP_R58 = emp.EMP_R58;
                    json.EMP_R59 = emp.EMP_R59;
                    json.EMP_R60 = emp.EMP_R60;

                    json.EMP_R61 = emp.EMP_R61;
                    json.EMP_R62 = emp.EMP_R62;
                    json.EMP_R63 = emp.EMP_R63;
                    json.EMP_R64 = emp.EMP_R64;
                    json.EMP_R65 = emp.EMP_R65;
                    json.EMP_R66 = emp.EMP_R66;
                    json.EMP_R67 = emp.EMP_R67;
                    json.EMP_R68 = emp.EMP_R68;
                    json.EMP_R69 = emp.EMP_R69;
                    json.EMP_R70 = emp.EMP_R70;
                    json.EMP_R71 = emp.EMP_R71;
                    json.EMP_R72 = emp.EMP_R72;
                    json.EMP_R73 = emp.EMP_R73;
                    json.EMP_R74 = emp.EMP_R74;
                    json.EMP_R75 = emp.EMP_R75;
                    json.EMP_R76 = emp.EMP_R76;
                    json.EMP_R77 = emp.EMP_R77;
                    json.EMP_R78 = emp.EMP_R78;
                    json.EMP_R79 = emp.EMP_R79;
                    json.EMP_R80 = emp.EMP_R80;

                    json.EMP_R81 = emp.EMP_R81;
                    json.EMP_R82 = emp.EMP_R82;
                    json.EMP_R83 = emp.EMP_R83;
                    json.EMP_R84 = emp.EMP_R84;
                    json.EMP_R85 = emp.EMP_R85;
                    json.EMP_R86 = emp.EMP_R86;

                    //Agregar EE

                    json.ARE_1 = emp.ARE_1;
                    json.ARE_2 = emp.ARE_2;
                    json.ARE_3 = emp.ARE_3;
                    json.ARE_4 = emp.ARE_4;
                    json.ARE_5 = emp.ARE_5;
                    json.ARE_6 = emp.ARE_6;
                    json.ARE_7 = emp.ARE_7;
                    json.ARE_8 = emp.ARE_8;
                    json.ARE_9 = emp.ARE_9;
                    json.ARE_10 = emp.ARE_10;
                    json.ARE_11 = emp.ARE_11;
                    json.ARE_12 = emp.ARE_12;
                    json.ARE_13 = emp.ARE_13;
                    json.ARE_14 = emp.ARE_14;
                    json.ARE_15 = emp.ARE_15;
                    json.ARE_16 = emp.ARE_16;
                    json.ARE_17 = emp.ARE_17;
                    json.ARE_18 = emp.ARE_18;
                    json.ARE_19 = emp.ARE_19;
                    json.ARE_20 = emp.ARE_20;


                    json.ARE_21 = emp.ARE_21;
                    json.ARE_22 = emp.ARE_22;
                    json.ARE_23 = emp.ARE_23;
                    json.ARE_24 = emp.ARE_24;
                    json.ARE_25 = emp.ARE_25;
                    json.ARE_26 = emp.ARE_26;
                    json.ARE_27 = emp.ARE_27;
                    json.ARE_28 = emp.ARE_28;
                    json.ARE_29 = emp.ARE_29;
                    json.ARE_30 = emp.ARE_30;
                    json.ARE_31 = emp.ARE_31;
                    json.ARE_32 = emp.ARE_32;
                    json.ARE_33 = emp.ARE_33;
                    json.ARE_34 = emp.ARE_34;
                    json.ARE_35 = emp.ARE_35;
                    json.ARE_36 = emp.ARE_36;
                    json.ARE_37 = emp.ARE_37;
                    json.ARE_38 = emp.ARE_38;
                    json.ARE_39 = emp.ARE_39;
                    json.ARE_40 = emp.ARE_40;

                    json.ARE_41 = emp.ARE_41;
                    json.ARE_42 = emp.ARE_42;
                    json.ARE_43 = emp.ARE_43;
                    json.ARE_44 = emp.ARE_44;
                    json.ARE_45 = emp.ARE_45;
                    json.ARE_46 = emp.ARE_46;
                    json.ARE_47 = emp.ARE_47;
                    json.ARE_48 = emp.ARE_48;
                    json.ARE_49 = emp.ARE_49;
                    json.ARE_50 = emp.ARE_50;
                    json.ARE_51 = emp.ARE_51;
                    json.ARE_52 = emp.ARE_52;
                    json.ARE_53 = emp.ARE_53;
                    json.ARE_54 = emp.ARE_54;
                    json.ARE_55 = emp.ARE_55;
                    json.ARE_56 = emp.ARE_56;
                    json.ARE_57 = emp.ARE_57;
                    json.ARE_58 = emp.ARE_58;
                    json.ARE_59 = emp.ARE_59;
                    json.ARE_60 = emp.ARE_60;

                    json.ARE_61 = emp.ARE_61;
                    json.ARE_62 = emp.ARE_62;
                    json.ARE_63 = emp.ARE_63;
                    json.ARE_64 = emp.ARE_64;
                    json.ARE_65 = emp.ARE_65;
                    json.ARE_66 = emp.ARE_66;
                    json.ARE_67 = emp.ARE_67;
                    json.ARE_68 = emp.ARE_68;
                    json.ARE_69 = emp.ARE_69;
                    json.ARE_70 = emp.ARE_70;
                    json.ARE_71 = emp.ARE_71;
                    json.ARE_72 = emp.ARE_72;
                    json.ARE_73 = emp.ARE_73;
                    json.ARE_74 = emp.ARE_74;
                    json.ARE_75 = emp.ARE_75;
                    json.ARE_76 = emp.ARE_76;
                    json.ARE_77 = emp.ARE_77;
                    json.ARE_78 = emp.ARE_78;
                    json.ARE_79 = emp.ARE_79;
                    json.ARE_80 = emp.ARE_80;

                    json.ARE_81 = emp.ARE_81;
                    json.ARE_82 = emp.ARE_82;
                    json.ARE_83 = emp.ARE_83;
                    json.ARE_84 = emp.ARE_84;
                    json.ARE_85 = emp.ARE_85;
                    json.ARE_86 = emp.ARE_86;

                    json.ESPECIAL_OPEN = emp.ESPECIAL_OPEN;
                    json.CAMBIAR_OPEN = emp.CAMBIAR_OPEN;
                    json.FORT_JEFE = emp.FORT_JEFE;
                    json.OPORT_JEFE = emp.OPORT_JEFE;
                    json.MOTIVA_TRAB = emp.MOTIVA_TRAB;
                    json.DEJAR_EMP = emp.DEJAR_EMP;
                    json.PRESION = emp.PRESION;
                    json.ANTIGUEDADR = emp.ANTIGUEDAD;
                    json.RANGO_EDADR = emp.RANGO_EDAD;
                    json.CONDICIONR = emp.CONDICION;
                    json.SEXOR = emp.SEXO;
                    json.ACADEMICOR = emp.ACADEMICO;
                    json.FUNCIONR = emp.FUNCION;
                    json.UNIDADRES = emp.UNIDAD;
                    json.DIRECIONRES = emp.DIRECION;
                    json.AREARES = emp.AREA;
                    json.DEPARTAMENTORES = emp.DEPARTAMENTO;


                    jsonList.Add(json);//El nuevo valor sobreescrbe al anterior quedando n cantidda de veces el ultimo
                }

            }
            var data = JsonConvert.SerializeObject(jsonList);

            return Content(data);
        }
        public ActionResult ReporteGetByArea(string Area)
        {
            //Enviar Catalogo de unidades de negocio usando viewbag
            var result = BL.Reporte.GetResultadosByArea(Area);

            //Llenar mi propiedad json
            List<ML.JsonReporte> jsonList = new List<ML.JsonReporte>();

            if (result.Objects != null)
            {
                foreach (ML.EmpleadoRespuesta emp in result.Objects)
                {

                    ML.JsonReporte json = new ML.JsonReporte();
                    json.IdEmpleado = emp.Empleado.IdEmpleado;
                    json.ApellidoPaterno = emp.Empleado.ApellidoPaterno;
                    json.ApellidoMaterno = emp.Empleado.ApellidoMaterno;
                    json.Nombre = emp.Empleado.Nombre;
                    json.Puesto = emp.Empleado.Puesto;
                    json.FechaNaciemiento = emp.Empleado.FechaNaciemiento;
                    json.FechaAntiguedad = emp.Empleado.FechaAntiguedad;
                    json.Sexo = emp.Empleado.Sexo;
                    json.Correo = emp.Empleado.Correo;
                    json.TipoFuncion = emp.Empleado.Perfil.Descripcion;
                    json.CondicionTrabajo = emp.Empleado.CondicionTrabajo;
                    json.GradoAcademico = emp.Empleado.GradoAcademico;
                    json.UNIDAD = emp.Empleado.Departamento.Area.Company.CompanyCategoria.Descripcion;
                    json.DIRECION = emp.Empleado.Departamento.Area.Company.CompanyName;
                    json.AREA = emp.Empleado.Departamento.Area.Nombre;
                    json.DEPARTAMENTO = emp.Empleado.Departamento.Nombre;
                    json.SUBDEPARTAMENTO = emp.Empleado.Subdepartamento.Nombre;
                    json.EmpresaContratante = emp.Empleado.EmpresaContratante;
                    json.IdResponsableRH = emp.Empleado.IdResponsableRH;
                    json.NombreResponsableRH = emp.Empleado.NombreResponsableRH;
                    json.IdJefe = emp.Empleado.IdJefe;
                    json.NombreJefe = emp.Empleado.NombreJefe;
                    json.PuestoJefe = emp.Empleado.PuestoJefe;
                    json.IdRespinsableEstructura = emp.Empleado.IdRespinsableEstructura;
                    json.NombreResponsableEstrucutra = emp.Empleado.NombreResponsableEstrucutra;
                    json.ClaveAcceso = emp.Empleado.ClavesAcceso.ClaveAcceso;
                    json.RangoAntiguedad = emp.Empleado.RangoAntiguedad;
                    json.RangoEdad = emp.Empleado.RangoEdad;
                    json.EstatusENcuesta = emp.Empleado.EstatusEncuesta.Estatus;
                    json.EstatusEmpleado = emp.Empleado.EstatusEmpleado;

                    json.EMP_R1 = emp.EMP_R1; json.EMP_R2 = emp.EMP_R2; json.EMP_R3 = emp.EMP_R3; json.EMP_R4 = emp.EMP_R4; json.EMP_R5 = emp.EMP_R5; json.EMP_R6 = emp.EMP_R6; json.EMP_R7 = emp.EMP_R7;
                    json.EMP_R8 = emp.EMP_R8; json.EMP_R9 = emp.EMP_R9; json.EMP_R10 = emp.EMP_R10; json.EMP_R11 = emp.EMP_R11; json.EMP_R12 = emp.EMP_R12; json.EMP_R13 = emp.EMP_R13; json.EMP_R14 = emp.EMP_R14;
                    json.EMP_R15 = emp.EMP_R15; json.EMP_R16 = emp.EMP_R16; json.EMP_R17 = emp.EMP_R17; json.EMP_R18 = emp.EMP_R18; json.EMP_R19 = emp.EMP_R19; json.EMP_R20 = emp.EMP_R20; json.EMP_R21 = emp.EMP_R21;
                    json.EMP_R22 = emp.EMP_R22; json.EMP_R23 = emp.EMP_R23; json.EMP_R24 = emp.EMP_R24; json.EMP_R25 = emp.EMP_R25; json.EMP_R26 = emp.EMP_R26; json.EMP_R27 = emp.EMP_R27; json.EMP_R28 = emp.EMP_R28;
                    json.EMP_R29 = emp.EMP_R29; json.EMP_R30 = emp.EMP_R30; json.EMP_R31 = emp.EMP_R31; json.EMP_R32 = emp.EMP_R32; json.EMP_R33 = emp.EMP_R33; json.EMP_R34 = emp.EMP_R34; json.EMP_R35 = emp.EMP_R35;
                    json.EMP_R36 = emp.EMP_R36; json.EMP_R37 = emp.EMP_R37; json.EMP_R38 = emp.EMP_R38; json.EMP_R39 = emp.EMP_R39; json.EMP_R40 = emp.EMP_R40; json.EMP_R41 = emp.EMP_R41; json.EMP_R42 = emp.EMP_R42;
                    json.EMP_R43 = emp.EMP_R43; json.EMP_R44 = emp.EMP_R44; json.EMP_R45 = emp.EMP_R45; json.EMP_R46 = emp.EMP_R46; json.EMP_R47 = emp.EMP_R47; json.EMP_R48 = emp.EMP_R48; json.EMP_R49 = emp.EMP_R49;
                    json.EMP_R50 = emp.EMP_R50; json.EMP_R51 = emp.EMP_R51; json.EMP_R52 = emp.EMP_R52; json.EMP_R53 = emp.EMP_R53; json.EMP_R54 = emp.EMP_R54; json.EMP_R55 = emp.EMP_R55; json.EMP_R56 = emp.EMP_R56;
                    json.EMP_R57 = emp.EMP_R57;
                    json.EMP_R58 = emp.EMP_R58;
                    json.EMP_R59 = emp.EMP_R59;
                    json.EMP_R60 = emp.EMP_R60;

                    json.EMP_R61 = emp.EMP_R61;
                    json.EMP_R62 = emp.EMP_R62;
                    json.EMP_R63 = emp.EMP_R63;
                    json.EMP_R64 = emp.EMP_R64;
                    json.EMP_R65 = emp.EMP_R65;
                    json.EMP_R66 = emp.EMP_R66;
                    json.EMP_R67 = emp.EMP_R67;
                    json.EMP_R68 = emp.EMP_R68;
                    json.EMP_R69 = emp.EMP_R69;
                    json.EMP_R70 = emp.EMP_R70;
                    json.EMP_R71 = emp.EMP_R71;
                    json.EMP_R72 = emp.EMP_R72;
                    json.EMP_R73 = emp.EMP_R73;
                    json.EMP_R74 = emp.EMP_R74;
                    json.EMP_R75 = emp.EMP_R75;
                    json.EMP_R76 = emp.EMP_R76;
                    json.EMP_R77 = emp.EMP_R77;
                    json.EMP_R78 = emp.EMP_R78;
                    json.EMP_R79 = emp.EMP_R79;
                    json.EMP_R80 = emp.EMP_R80;

                    json.EMP_R81 = emp.EMP_R81;
                    json.EMP_R82 = emp.EMP_R82;
                    json.EMP_R83 = emp.EMP_R83;
                    json.EMP_R84 = emp.EMP_R84;
                    json.EMP_R85 = emp.EMP_R85;
                    json.EMP_R86 = emp.EMP_R86;

                    //Agregar EE

                    json.ARE_1 = emp.ARE_1;
                    json.ARE_2 = emp.ARE_2;
                    json.ARE_3 = emp.ARE_3;
                    json.ARE_4 = emp.ARE_4;
                    json.ARE_5 = emp.ARE_5;
                    json.ARE_6 = emp.ARE_6;
                    json.ARE_7 = emp.ARE_7;
                    json.ARE_8 = emp.ARE_8;
                    json.ARE_9 = emp.ARE_9;
                    json.ARE_10 = emp.ARE_10;
                    json.ARE_11 = emp.ARE_11;
                    json.ARE_12 = emp.ARE_12;
                    json.ARE_13 = emp.ARE_13;
                    json.ARE_14 = emp.ARE_14;
                    json.ARE_15 = emp.ARE_15;
                    json.ARE_16 = emp.ARE_16;
                    json.ARE_17 = emp.ARE_17;
                    json.ARE_18 = emp.ARE_18;
                    json.ARE_19 = emp.ARE_19;
                    json.ARE_20 = emp.ARE_20;


                    json.ARE_21 = emp.ARE_21;
                    json.ARE_22 = emp.ARE_22;
                    json.ARE_23 = emp.ARE_23;
                    json.ARE_24 = emp.ARE_24;
                    json.ARE_25 = emp.ARE_25;
                    json.ARE_26 = emp.ARE_26;
                    json.ARE_27 = emp.ARE_27;
                    json.ARE_28 = emp.ARE_28;
                    json.ARE_29 = emp.ARE_29;
                    json.ARE_30 = emp.ARE_30;
                    json.ARE_31 = emp.ARE_31;
                    json.ARE_32 = emp.ARE_32;
                    json.ARE_33 = emp.ARE_33;
                    json.ARE_34 = emp.ARE_34;
                    json.ARE_35 = emp.ARE_35;
                    json.ARE_36 = emp.ARE_36;
                    json.ARE_37 = emp.ARE_37;
                    json.ARE_38 = emp.ARE_38;
                    json.ARE_39 = emp.ARE_39;
                    json.ARE_40 = emp.ARE_40;

                    json.ARE_41 = emp.ARE_41;
                    json.ARE_42 = emp.ARE_42;
                    json.ARE_43 = emp.ARE_43;
                    json.ARE_44 = emp.ARE_44;
                    json.ARE_45 = emp.ARE_45;
                    json.ARE_46 = emp.ARE_46;
                    json.ARE_47 = emp.ARE_47;
                    json.ARE_48 = emp.ARE_48;
                    json.ARE_49 = emp.ARE_49;
                    json.ARE_50 = emp.ARE_50;
                    json.ARE_51 = emp.ARE_51;
                    json.ARE_52 = emp.ARE_52;
                    json.ARE_53 = emp.ARE_53;
                    json.ARE_54 = emp.ARE_54;
                    json.ARE_55 = emp.ARE_55;
                    json.ARE_56 = emp.ARE_56;
                    json.ARE_57 = emp.ARE_57;
                    json.ARE_58 = emp.ARE_58;
                    json.ARE_59 = emp.ARE_59;
                    json.ARE_60 = emp.ARE_60;

                    json.ARE_61 = emp.ARE_61;
                    json.ARE_62 = emp.ARE_62;
                    json.ARE_63 = emp.ARE_63;
                    json.ARE_64 = emp.ARE_64;
                    json.ARE_65 = emp.ARE_65;
                    json.ARE_66 = emp.ARE_66;
                    json.ARE_67 = emp.ARE_67;
                    json.ARE_68 = emp.ARE_68;
                    json.ARE_69 = emp.ARE_69;
                    json.ARE_70 = emp.ARE_70;
                    json.ARE_71 = emp.ARE_71;
                    json.ARE_72 = emp.ARE_72;
                    json.ARE_73 = emp.ARE_73;
                    json.ARE_74 = emp.ARE_74;
                    json.ARE_75 = emp.ARE_75;
                    json.ARE_76 = emp.ARE_76;
                    json.ARE_77 = emp.ARE_77;
                    json.ARE_78 = emp.ARE_78;
                    json.ARE_79 = emp.ARE_79;
                    json.ARE_80 = emp.ARE_80;

                    json.ARE_81 = emp.ARE_81;
                    json.ARE_82 = emp.ARE_82;
                    json.ARE_83 = emp.ARE_83;
                    json.ARE_84 = emp.ARE_84;
                    json.ARE_85 = emp.ARE_85;
                    json.ARE_86 = emp.ARE_86;

                    json.ESPECIAL_OPEN = emp.ESPECIAL_OPEN;
                    json.CAMBIAR_OPEN = emp.CAMBIAR_OPEN;
                    json.FORT_JEFE = emp.FORT_JEFE;
                    json.OPORT_JEFE = emp.OPORT_JEFE;
                    json.MOTIVA_TRAB = emp.MOTIVA_TRAB;
                    json.DEJAR_EMP = emp.DEJAR_EMP;
                    json.PRESION = emp.PRESION;
                    json.ANTIGUEDADR = emp.ANTIGUEDAD;
                    json.RANGO_EDADR = emp.RANGO_EDAD;
                    json.CONDICIONR = emp.CONDICION;
                    json.SEXOR = emp.SEXO;
                    json.ACADEMICOR = emp.ACADEMICO;
                    json.FUNCIONR = emp.FUNCION;
                    json.UNIDADRES = emp.UNIDAD;
                    json.DIRECIONRES = emp.DIRECION;
                    json.AREARES = emp.AREA;
                    json.DEPARTAMENTORES = emp.DEPARTAMENTO;


                    jsonList.Add(json);//El nuevo valor sobreescrbe al anterior quedando n cantidda de veces el ultimo
                }

            }
            var data = JsonConvert.SerializeObject(jsonList);

            return Content(data);
        }
        public ActionResult ReporteGetByDepartamento(string Dpto)
        {
            //Enviar Catalogo de unidades de negocio usando viewbag
            var result = BL.Reporte.GetResultadosByDepartamento(Dpto);

            //Llenar mi propiedad json
            List<ML.JsonReporte> jsonList = new List<ML.JsonReporte>();

            if (result.Objects != null)
            {
                foreach (ML.EmpleadoRespuesta emp in result.Objects)
                {

                    ML.JsonReporte json = new ML.JsonReporte();
                    json.IdEmpleado = emp.Empleado.IdEmpleado;
                    json.ApellidoPaterno = emp.Empleado.ApellidoPaterno;
                    json.ApellidoMaterno = emp.Empleado.ApellidoMaterno;
                    json.Nombre = emp.Empleado.Nombre;
                    json.Puesto = emp.Empleado.Puesto;
                    json.FechaNaciemiento = emp.Empleado.FechaNaciemiento;
                    json.FechaAntiguedad = emp.Empleado.FechaAntiguedad;
                    json.Sexo = emp.Empleado.Sexo;
                    json.Correo = emp.Empleado.Correo;
                    json.TipoFuncion = emp.Empleado.Perfil.Descripcion;
                    json.CondicionTrabajo = emp.Empleado.CondicionTrabajo;
                    json.GradoAcademico = emp.Empleado.GradoAcademico;
                    json.UNIDAD = emp.Empleado.Departamento.Area.Company.CompanyCategoria.Descripcion;
                    json.DIRECION = emp.Empleado.Departamento.Area.Company.CompanyName;
                    json.AREA = emp.Empleado.Departamento.Area.Nombre;
                    json.DEPARTAMENTO = emp.Empleado.Departamento.Nombre;
                    json.SUBDEPARTAMENTO = emp.Empleado.Subdepartamento.Nombre;
                    json.EmpresaContratante = emp.Empleado.EmpresaContratante;
                    json.IdResponsableRH = emp.Empleado.IdResponsableRH;
                    json.NombreResponsableRH = emp.Empleado.NombreResponsableRH;
                    json.IdJefe = emp.Empleado.IdJefe;
                    json.NombreJefe = emp.Empleado.NombreJefe;
                    json.PuestoJefe = emp.Empleado.PuestoJefe;
                    json.IdRespinsableEstructura = emp.Empleado.IdRespinsableEstructura;
                    json.NombreResponsableEstrucutra = emp.Empleado.NombreResponsableEstrucutra;
                    json.ClaveAcceso = emp.Empleado.ClavesAcceso.ClaveAcceso;
                    json.RangoAntiguedad = emp.Empleado.RangoAntiguedad;
                    json.RangoEdad = emp.Empleado.RangoEdad;
                    json.EstatusENcuesta = emp.Empleado.EstatusEncuesta.Estatus;
                    json.EstatusEmpleado = emp.Empleado.EstatusEmpleado;

                    json.EMP_R1 = emp.EMP_R1; json.EMP_R2 = emp.EMP_R2; json.EMP_R3 = emp.EMP_R3; json.EMP_R4 = emp.EMP_R4; json.EMP_R5 = emp.EMP_R5; json.EMP_R6 = emp.EMP_R6; json.EMP_R7 = emp.EMP_R7;
                    json.EMP_R8 = emp.EMP_R8; json.EMP_R9 = emp.EMP_R9; json.EMP_R10 = emp.EMP_R10; json.EMP_R11 = emp.EMP_R11; json.EMP_R12 = emp.EMP_R12; json.EMP_R13 = emp.EMP_R13; json.EMP_R14 = emp.EMP_R14;
                    json.EMP_R15 = emp.EMP_R15; json.EMP_R16 = emp.EMP_R16; json.EMP_R17 = emp.EMP_R17; json.EMP_R18 = emp.EMP_R18; json.EMP_R19 = emp.EMP_R19; json.EMP_R20 = emp.EMP_R20; json.EMP_R21 = emp.EMP_R21;
                    json.EMP_R22 = emp.EMP_R22; json.EMP_R23 = emp.EMP_R23; json.EMP_R24 = emp.EMP_R24; json.EMP_R25 = emp.EMP_R25; json.EMP_R26 = emp.EMP_R26; json.EMP_R27 = emp.EMP_R27; json.EMP_R28 = emp.EMP_R28;
                    json.EMP_R29 = emp.EMP_R29; json.EMP_R30 = emp.EMP_R30; json.EMP_R31 = emp.EMP_R31; json.EMP_R32 = emp.EMP_R32; json.EMP_R33 = emp.EMP_R33; json.EMP_R34 = emp.EMP_R34; json.EMP_R35 = emp.EMP_R35;
                    json.EMP_R36 = emp.EMP_R36; json.EMP_R37 = emp.EMP_R37; json.EMP_R38 = emp.EMP_R38; json.EMP_R39 = emp.EMP_R39; json.EMP_R40 = emp.EMP_R40; json.EMP_R41 = emp.EMP_R41; json.EMP_R42 = emp.EMP_R42;
                    json.EMP_R43 = emp.EMP_R43; json.EMP_R44 = emp.EMP_R44; json.EMP_R45 = emp.EMP_R45; json.EMP_R46 = emp.EMP_R46; json.EMP_R47 = emp.EMP_R47; json.EMP_R48 = emp.EMP_R48; json.EMP_R49 = emp.EMP_R49;
                    json.EMP_R50 = emp.EMP_R50; json.EMP_R51 = emp.EMP_R51; json.EMP_R52 = emp.EMP_R52; json.EMP_R53 = emp.EMP_R53; json.EMP_R54 = emp.EMP_R54; json.EMP_R55 = emp.EMP_R55; json.EMP_R56 = emp.EMP_R56;
                    json.EMP_R57 = emp.EMP_R57;
                    json.EMP_R58 = emp.EMP_R58;
                    json.EMP_R59 = emp.EMP_R59;
                    json.EMP_R60 = emp.EMP_R60;

                    json.EMP_R61 = emp.EMP_R61;
                    json.EMP_R62 = emp.EMP_R62;
                    json.EMP_R63 = emp.EMP_R63;
                    json.EMP_R64 = emp.EMP_R64;
                    json.EMP_R65 = emp.EMP_R65;
                    json.EMP_R66 = emp.EMP_R66;
                    json.EMP_R67 = emp.EMP_R67;
                    json.EMP_R68 = emp.EMP_R68;
                    json.EMP_R69 = emp.EMP_R69;
                    json.EMP_R70 = emp.EMP_R70;
                    json.EMP_R71 = emp.EMP_R71;
                    json.EMP_R72 = emp.EMP_R72;
                    json.EMP_R73 = emp.EMP_R73;
                    json.EMP_R74 = emp.EMP_R74;
                    json.EMP_R75 = emp.EMP_R75;
                    json.EMP_R76 = emp.EMP_R76;
                    json.EMP_R77 = emp.EMP_R77;
                    json.EMP_R78 = emp.EMP_R78;
                    json.EMP_R79 = emp.EMP_R79;
                    json.EMP_R80 = emp.EMP_R80;

                    json.EMP_R81 = emp.EMP_R81;
                    json.EMP_R82 = emp.EMP_R82;
                    json.EMP_R83 = emp.EMP_R83;
                    json.EMP_R84 = emp.EMP_R84;
                    json.EMP_R85 = emp.EMP_R85;
                    json.EMP_R86 = emp.EMP_R86;

                    //Agregar EE

                    json.ARE_1 = emp.ARE_1;
                    json.ARE_2 = emp.ARE_2;
                    json.ARE_3 = emp.ARE_3;
                    json.ARE_4 = emp.ARE_4;
                    json.ARE_5 = emp.ARE_5;
                    json.ARE_6 = emp.ARE_6;
                    json.ARE_7 = emp.ARE_7;
                    json.ARE_8 = emp.ARE_8;
                    json.ARE_9 = emp.ARE_9;
                    json.ARE_10 = emp.ARE_10;
                    json.ARE_11 = emp.ARE_11;
                    json.ARE_12 = emp.ARE_12;
                    json.ARE_13 = emp.ARE_13;
                    json.ARE_14 = emp.ARE_14;
                    json.ARE_15 = emp.ARE_15;
                    json.ARE_16 = emp.ARE_16;
                    json.ARE_17 = emp.ARE_17;
                    json.ARE_18 = emp.ARE_18;
                    json.ARE_19 = emp.ARE_19;
                    json.ARE_20 = emp.ARE_20;


                    json.ARE_21 = emp.ARE_21;
                    json.ARE_22 = emp.ARE_22;
                    json.ARE_23 = emp.ARE_23;
                    json.ARE_24 = emp.ARE_24;
                    json.ARE_25 = emp.ARE_25;
                    json.ARE_26 = emp.ARE_26;
                    json.ARE_27 = emp.ARE_27;
                    json.ARE_28 = emp.ARE_28;
                    json.ARE_29 = emp.ARE_29;
                    json.ARE_30 = emp.ARE_30;
                    json.ARE_31 = emp.ARE_31;
                    json.ARE_32 = emp.ARE_32;
                    json.ARE_33 = emp.ARE_33;
                    json.ARE_34 = emp.ARE_34;
                    json.ARE_35 = emp.ARE_35;
                    json.ARE_36 = emp.ARE_36;
                    json.ARE_37 = emp.ARE_37;
                    json.ARE_38 = emp.ARE_38;
                    json.ARE_39 = emp.ARE_39;
                    json.ARE_40 = emp.ARE_40;

                    json.ARE_41 = emp.ARE_41;
                    json.ARE_42 = emp.ARE_42;
                    json.ARE_43 = emp.ARE_43;
                    json.ARE_44 = emp.ARE_44;
                    json.ARE_45 = emp.ARE_45;
                    json.ARE_46 = emp.ARE_46;
                    json.ARE_47 = emp.ARE_47;
                    json.ARE_48 = emp.ARE_48;
                    json.ARE_49 = emp.ARE_49;
                    json.ARE_50 = emp.ARE_50;
                    json.ARE_51 = emp.ARE_51;
                    json.ARE_52 = emp.ARE_52;
                    json.ARE_53 = emp.ARE_53;
                    json.ARE_54 = emp.ARE_54;
                    json.ARE_55 = emp.ARE_55;
                    json.ARE_56 = emp.ARE_56;
                    json.ARE_57 = emp.ARE_57;
                    json.ARE_58 = emp.ARE_58;
                    json.ARE_59 = emp.ARE_59;
                    json.ARE_60 = emp.ARE_60;

                    json.ARE_61 = emp.ARE_61;
                    json.ARE_62 = emp.ARE_62;
                    json.ARE_63 = emp.ARE_63;
                    json.ARE_64 = emp.ARE_64;
                    json.ARE_65 = emp.ARE_65;
                    json.ARE_66 = emp.ARE_66;
                    json.ARE_67 = emp.ARE_67;
                    json.ARE_68 = emp.ARE_68;
                    json.ARE_69 = emp.ARE_69;
                    json.ARE_70 = emp.ARE_70;
                    json.ARE_71 = emp.ARE_71;
                    json.ARE_72 = emp.ARE_72;
                    json.ARE_73 = emp.ARE_73;
                    json.ARE_74 = emp.ARE_74;
                    json.ARE_75 = emp.ARE_75;
                    json.ARE_76 = emp.ARE_76;
                    json.ARE_77 = emp.ARE_77;
                    json.ARE_78 = emp.ARE_78;
                    json.ARE_79 = emp.ARE_79;
                    json.ARE_80 = emp.ARE_80;

                    json.ARE_81 = emp.ARE_81;
                    json.ARE_82 = emp.ARE_82;
                    json.ARE_83 = emp.ARE_83;
                    json.ARE_84 = emp.ARE_84;
                    json.ARE_85 = emp.ARE_85;
                    json.ARE_86 = emp.ARE_86;


                    json.ESPECIAL_OPEN = emp.ESPECIAL_OPEN;
                    json.CAMBIAR_OPEN = emp.CAMBIAR_OPEN;
                    json.FORT_JEFE = emp.FORT_JEFE;
                    json.OPORT_JEFE = emp.OPORT_JEFE;
                    json.MOTIVA_TRAB = emp.MOTIVA_TRAB;
                    json.DEJAR_EMP = emp.DEJAR_EMP;
                    json.PRESION = emp.PRESION;
                    json.ANTIGUEDADR = emp.ANTIGUEDAD;
                    json.RANGO_EDADR = emp.RANGO_EDAD;
                    json.CONDICIONR = emp.CONDICION;
                    json.SEXOR = emp.SEXO;
                    json.ACADEMICOR = emp.ACADEMICO;
                    json.FUNCIONR = emp.FUNCION;
                    json.UNIDADRES = emp.UNIDAD;
                    json.DIRECIONRES = emp.DIRECION;
                    json.AREARES = emp.AREA;
                    json.DEPARTAMENTORES = emp.DEPARTAMENTO;


                    jsonList.Add(json);//El nuevo valor sobreescrbe al anterior quedando n cantidda de veces el ultimo
                }

            }
            var data = JsonConvert.SerializeObject(jsonList);

            return Content(data);
        }
        public JsonResult GetCompanyAjaxReporte(string id)
        {
            var result = BL.Company.GetByCompanyCategoriaReporte(id);

            List<SelectListItem> Companies = new List<SelectListItem>();

            Companies.Add(new SelectListItem { Text = "Selecciona una opcion", Value = "0" });

            if (result.Objects != null)
            {
                foreach (ML.Company company in result.Objects)
                {
                    Companies.Add(new SelectListItem { Text = company.CompanyName.ToString(), Value = company.CompanyId.ToString() });
                }

            }

            return Json(new SelectList(Companies, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        public JsonResult GetAreaAjaxReporte(string id)
        {
            var result = BL.Area.AreaGetByCompanyIdReporte(id);

            List<SelectListItem> Areas = new List<SelectListItem>();

            Areas.Add(new SelectListItem { Text = "Selecciona una opcion", Value = "0" });

            if (result.Objects != null)
            {
                foreach (ML.Area area in result.Objects)
                {
                    Areas.Add(new SelectListItem { Text = area.Nombre.ToString(), Value = area.IdArea.ToString() });
                }
            }
            return Json(new SelectList(Areas, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        public JsonResult GetDepartamentoAjaxReporte(string id)
        {
            var result = BL.Departamento.GetByAreaReporte(id);

            List<SelectListItem> Departamentos = new List<SelectListItem>();

            Departamentos.Add(new SelectListItem { Text = "Selecciona una opcion", Value = "0" });

            if (result.Objects != null)
            {
                foreach (ML.Departamento departamento in result.Objects)
                {
                    Departamentos.Add(new SelectListItem { Text = departamento.Nombre.ToString(), Value = departamento.IdDepartamento.ToString() });
                }
            }
            return Json(new SelectList(Departamentos, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        public ActionResult GetPerfilesTipoFuncion()
        {
            var result = BL.Perfil.GetAll();

            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetRespuestasByIdEmpleado(int id)
        {
            ML.EmpleadoRespuesta empRes = new ML.EmpleadoRespuesta();
            empRes.Empleado = new ML.Empleado();
            empRes.Empleado.IdEmpleado = id;
            var result = BL.Respuestas.GetRespuestasCLByEmpleado(empRes);

            if (result.Objects.Count == 0)
            {
                Session["progreso"] = 0;
            }
            if (result.Objects.Count == 16)
            {
                Session["progreso"] = 8;
            }
            if (result.Objects.Count == 32)
            {
                Session["progreso"] = 16;
            }
            if (result.Objects.Count == 48)
            {
                Session["progreso"] = 24;
            }
            if (result.Objects.Count == 64)
            {
                Session["progreso"] = 32;
            }
            if (result.Objects.Count == 80)
            {
                Session["progreso"] = 40;
            }
            if (result.Objects.Count == 96)
            {
                Session["progreso"] = 48;
            }
            if (result.Objects.Count == 112)
            {
                Session["progreso"] = 56;
            }
            if (result.Objects.Count == 128)
            {
                Session["progreso"] = 64;
            }
            if (result.Objects.Count == 144)
            {
                Session["progreso"] = 72;
            }
            if (result.Objects.Count == 160)
            {
                Session["progreso"] = 80;
            }
            if (result.Objects.Count == 172)
            {
                Session["progreso"] = 86;
            }
            if (result.Objects.Count == 179)
            {
                Session["progreso"] = 93;
            }

            return Json("success");

        }
        /*ActionMethod For ClimaLaboral => Porcentajes*/
        public ActionResult GetPorcentajeAfirmativas()
        {
            ML.Result items = new ML.Result();
            items.ListadoPreguntas = new List<ML.Preguntas>();
            return View(items);
        }
        //Resultados Globales


        //get View empty
        [HttpGet]
        public ActionResult GetPorcentajeAfrimativasByDemografico()
        {
            //Session["TotalPreguntas"] = null;
            //Session["RespuestasFavorables"] = null;
            //Session["RespuestasNegativas"] = null;
            //Session["Porcentaje"] = null;
            return View();
        }
        //Muestran el avance
        public ActionResult GetEstatusAvance()
        {
            var result = BL.ReporteD4U.GetEstatusAvance();

            return View(result);
        }
        public ActionResult GetAvanceByUNeg(string CompanyCategoria)
        {
            var result = BL.ReporteD4U.GetEstatusAvanceByUNEGocio(CompanyCategoria);
            return View("GetEstatusAvance", result);
        }



        //muestran grafico con porcentaje
        public ActionResult GetPorcentajeAfirmativasByDemoAntiguedad(ML.Empleado empleado)
        {
            var result = BL.ReporteD4U.GetPorcentajeAfirmativasByAntiguedad(empleado.RangoAntiguedad);
            Session["criterio"] = "para el rango de antiguedad de " + empleado.RangoAntiguedad;
            string JSon = "";
            if (result.Object != null)
            {
                JSon = "success";
                ML.ReporteD4U repo = new ML.ReporteD4U();
                repo.TotalPreguntas = ((ML.ReporteD4U)result.Object).TotalPreguntas;
                repo.TotalRespuestasFavorables = ((ML.ReporteD4U)result.Object).TotalRespuestasFavorables;
                repo.Porcentaje = ((ML.ReporteD4U)result.Object).Porcentaje;

                Session["TotalPreguntas"] = repo.TotalPreguntas;
                Session["RespuestasFavorables"] = repo.TotalRespuestasFavorables;
                Session["RespuestasNegativas"] = repo.TotalPreguntas - repo.TotalRespuestasFavorables;
                Session["Porcentaje"] = repo.Porcentaje;
                return Json(JSon, JsonRequestBehavior.AllowGet);
            }
            else
            {
                JSon = "error";
                return Json(JSon, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetPorcentajeAfirmativasByDemoRangoEdad(ML.Empleado empleado)
        {
            var result = BL.ReporteD4U.GetPorcentajeAfirmativasByDemoRangoEdad(empleado.RangoEdad);
            Session["criterio"] = "para el rango de edad de " + empleado.RangoEdad;
            string JSON = "";
            if (result.Object != null)
            {
                JSON = "success";
                ML.ReporteD4U repo = new ML.ReporteD4U();
                repo.TotalPreguntas = ((ML.ReporteD4U)result.Object).TotalPreguntas;
                repo.TotalRespuestasFavorables = ((ML.ReporteD4U)result.Object).TotalRespuestasFavorables;
                repo.Porcentaje = ((ML.ReporteD4U)result.Object).Porcentaje;

                Session["TotalPreguntas"] = repo.TotalPreguntas;
                Session["RespuestasFavorables"] = repo.TotalRespuestasFavorables;
                Session["RespuestasNegativas"] = repo.TotalPreguntas - repo.TotalRespuestasFavorables;
                Session["Porcentaje"] = repo.Porcentaje;
                return Json(JSON, JsonRequestBehavior.AllowGet);
            }
            else
            {
                JSON = "error";
                return Json(JSON, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetPorcentajeAfirmativasByDemoCondTrab(ML.Empleado empleado)
        {
            var result = BL.ReporteD4U.GetPorcentajeAfirmativasByDemoCondTrab(empleado.CondicionTrabajo);

            Session["criterio"] = "para la condicion de trabajo: " + empleado.CondicionTrabajo;
            string JSON = "";
            if (result.Object != null)
            {
                JSON = "success";
                ML.ReporteD4U repo = new ML.ReporteD4U();
                repo.TotalPreguntas = ((ML.ReporteD4U)result.Object).TotalPreguntas;
                repo.TotalRespuestasFavorables = ((ML.ReporteD4U)result.Object).TotalRespuestasFavorables;
                repo.Porcentaje = ((ML.ReporteD4U)result.Object).Porcentaje;

                Session["TotalPreguntas"] = repo.TotalPreguntas;
                Session["RespuestasFavorables"] = repo.TotalRespuestasFavorables;
                Session["RespuestasNegativas"] = repo.TotalPreguntas - repo.TotalRespuestasFavorables;
                Session["Porcentaje"] = repo.Porcentaje;
                return Json(JSON, JsonRequestBehavior.AllowGet);
            }
            else
            {
                JSON = "error";
                return Json(JSON, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetPorcentajeAfirmativasByDemoSexo(ML.Empleado empleado)
        {
            var result = BL.ReporteD4U.GetPorcentajeAfirmativasByDemoSexo(empleado.Sexo);

            Session["criterio"] = "para los empleados cuyo sexo es " + empleado.Sexo;
            string JSON = "";
            if (result.Object != null)
            {
                JSON = "success";
                ML.ReporteD4U repo = new ML.ReporteD4U();
                repo.TotalPreguntas = ((ML.ReporteD4U)result.Object).TotalPreguntas;
                repo.TotalRespuestasFavorables = ((ML.ReporteD4U)result.Object).TotalRespuestasFavorables;
                repo.Porcentaje = ((ML.ReporteD4U)result.Object).Porcentaje;

                Session["TotalPreguntas"] = repo.TotalPreguntas;
                Session["RespuestasFavorables"] = repo.TotalRespuestasFavorables;
                Session["RespuestasNegativas"] = repo.TotalPreguntas - repo.TotalRespuestasFavorables;
                Session["Porcentaje"] = repo.Porcentaje;
                return Json(JSON, JsonRequestBehavior.AllowGet);
            }
            else
            {
                JSON = "error";
                return Json(JSON, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetPorcentajeAfirmativasByDemoGradoAcad(ML.Empleado empleado)
        {
            var result = BL.ReporteD4U.GetPorcentajeAfirmativasByDemoGradoAcad(empleado.GradoAcademico);

            Session["criterio"] = "para los empleados cuyo grado academico es " + empleado.GradoAcademico;
            string JSON = "";
            if (result.Object != null)
            {
                JSON = "success";
                ML.ReporteD4U repo = new ML.ReporteD4U();
                repo.TotalPreguntas = ((ML.ReporteD4U)result.Object).TotalPreguntas;
                repo.TotalRespuestasFavorables = ((ML.ReporteD4U)result.Object).TotalRespuestasFavorables;
                repo.Porcentaje = ((ML.ReporteD4U)result.Object).Porcentaje;

                Session["TotalPreguntas"] = repo.TotalPreguntas;
                Session["RespuestasFavorables"] = repo.TotalRespuestasFavorables;
                Session["RespuestasNegativas"] = repo.TotalPreguntas - repo.TotalRespuestasFavorables;
                Session["Porcentaje"] = repo.Porcentaje;
                return Json(JSON, JsonRequestBehavior.AllowGet);
            }
            else
            {
                JSON = "error";
                return Json(JSON, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetPorcentajeAfirmativasByDemoFuncion(ML.Empleado empleado)
        {
            var result = BL.ReporteD4U.GetPorcentajeAfirmativasByDemoFuncion(empleado.TipoFuncion);

            Session["criterio"] = "para los empleados cuya funcion es " + empleado.TipoFuncion;
            string JSON = "";
            if (result.Object != null)
            {
                JSON = "success";
                ML.ReporteD4U repo = new ML.ReporteD4U();
                repo.TotalPreguntas = ((ML.ReporteD4U)result.Object).TotalPreguntas;
                repo.TotalRespuestasFavorables = ((ML.ReporteD4U)result.Object).TotalRespuestasFavorables;
                repo.Porcentaje = ((ML.ReporteD4U)result.Object).Porcentaje;

                Session["TotalPreguntas"] = repo.TotalPreguntas;
                Session["RespuestasFavorables"] = repo.TotalRespuestasFavorables;
                Session["RespuestasNegativas"] = repo.TotalPreguntas - repo.TotalRespuestasFavorables;
                Session["Porcentaje"] = repo.Porcentaje;
                return Json(JSON, JsonRequestBehavior.AllowGet);
            }
            else
            {
                JSON = "error";
                return Json(JSON, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetPorcentajeAfirmativasByDemoUNegocio(ML.Empleado empleado)
        {
            var result = BL.ReporteD4U.GetPorcentajeAfirmativasByDemoUNEGocio(empleado.UnidadNegocio);

            Session["criterio"] = "para los empleados que pertenecen a la unidad de negocio: " + empleado.UnidadNegocio;
            string JSON = "";
            if (result.Object != null)
            {
                JSON = "success";
                ML.ReporteD4U repo = new ML.ReporteD4U();
                repo.TotalPreguntas = ((ML.ReporteD4U)result.Object).TotalPreguntas;
                repo.TotalRespuestasFavorables = ((ML.ReporteD4U)result.Object).TotalRespuestasFavorables;
                repo.Porcentaje = ((ML.ReporteD4U)result.Object).Porcentaje;

                Session["TotalPreguntas"] = repo.TotalPreguntas;
                Session["RespuestasFavorables"] = repo.TotalRespuestasFavorables;
                Session["RespuestasNegativas"] = repo.TotalPreguntas - repo.TotalRespuestasFavorables;
                Session["Porcentaje"] = repo.Porcentaje;
                return Json(JSON, JsonRequestBehavior.AllowGet);
            }
            else
            {
                JSON = "error";
                return Json(JSON, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetPorcentajeAfirmativasByDemoDivis(ML.Empleado empleado)
        {
            var result = BL.ReporteD4U.GetPorcentajeAfirmativasByDemoDivis(empleado.DivisonMarca);

            Session["criterio"] = "para los empleados que pertenecen a la Division/Marca: " + empleado.DivisonMarca;
            string JSON = "";
            if (result.Object != null)
            {
                JSON = "success";
                ML.ReporteD4U repo = new ML.ReporteD4U();
                repo.TotalPreguntas = ((ML.ReporteD4U)result.Object).TotalPreguntas;
                repo.TotalRespuestasFavorables = ((ML.ReporteD4U)result.Object).TotalRespuestasFavorables;
                repo.Porcentaje = ((ML.ReporteD4U)result.Object).Porcentaje;

                Session["TotalPreguntas"] = repo.TotalPreguntas;
                Session["RespuestasFavorables"] = repo.TotalRespuestasFavorables;
                Session["RespuestasNegativas"] = repo.TotalPreguntas - repo.TotalRespuestasFavorables;
                Session["Porcentaje"] = repo.Porcentaje;
                return Json(JSON, JsonRequestBehavior.AllowGet);
            }
            else
            {
                JSON = "error";
                return Json(JSON, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetPorcentajeAfirmativasByDemoArea(ML.Empleado empleado)
        {
            var result = BL.ReporteD4U.GetPorcentajeAfirmativasByDemoArea(empleado.AreaAgencia);

            Session["criterio"] = "para los empleados que pertenecen al Area: " + empleado.AreaAgencia;
            string JSON = "";
            if (result.Object != null)
            {
                JSON = "success";
                ML.ReporteD4U repo = new ML.ReporteD4U();
                repo.TotalPreguntas = ((ML.ReporteD4U)result.Object).TotalPreguntas;
                repo.TotalRespuestasFavorables = ((ML.ReporteD4U)result.Object).TotalRespuestasFavorables;
                repo.Porcentaje = ((ML.ReporteD4U)result.Object).Porcentaje;

                Session["TotalPreguntas"] = repo.TotalPreguntas;
                Session["RespuestasFavorables"] = repo.TotalRespuestasFavorables;
                Session["RespuestasNegativas"] = repo.TotalPreguntas - repo.TotalRespuestasFavorables;
                Session["Porcentaje"] = repo.Porcentaje;
                return Json(JSON, JsonRequestBehavior.AllowGet);
            }
            else
            {
                JSON = "error";
                return Json(JSON, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetPorcentajeAfirmativasByDemoDepto(ML.Empleado empleado)
        {
            var result = BL.ReporteD4U.GetPorcentajeAfirmativasByDemoDepto(empleado.Depto);

            Session["criterio"] = "para los empleados que pertenecen al Departamento: " + empleado.Depto;
            string JSON = "";
            if (result.Object != null)
            {
                JSON = "success";
                ML.ReporteD4U repo = new ML.ReporteD4U();
                repo.TotalPreguntas = ((ML.ReporteD4U)result.Object).TotalPreguntas;
                repo.TotalRespuestasFavorables = ((ML.ReporteD4U)result.Object).TotalRespuestasFavorables;
                repo.Porcentaje = ((ML.ReporteD4U)result.Object).Porcentaje;

                Session["TotalPreguntas"] = repo.TotalPreguntas;
                Session["RespuestasFavorables"] = repo.TotalRespuestasFavorables;
                Session["RespuestasNegativas"] = repo.TotalPreguntas - repo.TotalRespuestasFavorables;
                Session["Porcentaje"] = repo.Porcentaje;
                return Json(JSON, JsonRequestBehavior.AllowGet);
            }
            else
            {
                JSON = "error";
                return Json(JSON, JsonRequestBehavior.AllowGet);
            }
        }




        //Muestran la descripcion de la preunta
        public ActionResult GetPorcentajeAfirmativasGlobal()
        {
            var result = new ML.Result();
            result.Objects = new List<object>();
            ML.Result items = new ML.Result();
            items.ListadoPreguntas = new List<ML.Preguntas>();
            for (int IdPregunta = 1; IdPregunta < 173; IdPregunta++)
            {
                var dta = (BL.ReporteD4U.GetPorcentajeAfirmativasGlobal(IdPregunta).Pregunta);
                items.ListadoPreguntas.Add(dta);
            }
            return Json(items.ListadoPreguntas, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPorcentajeAfirmativasByUNegocio(ML.Company company)
        {
            var result = new ML.Result();
            result.Objects = new List<object>();
            ML.Result items = new ML.Result();
            items.ListadoPreguntas = new List<ML.Preguntas>();
            for (int IdPregunta = 1; IdPregunta < 173; IdPregunta++)
            {
                var dta = (BL.ReporteD4U.GetPorcentajeAfirmativasByUNEGocio(IdPregunta, company.CompanyName).Pregunta);
                items.ListadoPreguntas.Add(dta);
            }
            return Json(items.ListadoPreguntas, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPorcentajeAfirmativasByAntiguedad(ML.Empleado empleado)
        {
            var result = new ML.Result();
            result.Objects = new List<object>();
            ML.Result items = new ML.Result();
            items.ListadoPreguntas = new List<ML.Preguntas>();
            for (int IdPregunta = 1; IdPregunta < 173; IdPregunta++)
            {
                var dta = (BL.ReporteD4U.GetPorcentajeAfirmativasByAntiguedad(IdPregunta, empleado.RangoAntiguedad).Pregunta);
                items.ListadoPreguntas.Add(dta);
            }
            return Json(items.ListadoPreguntas, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPorcentajeAfirmativasByRangoEdad(ML.Empleado empleado)
        {
            var result = new ML.Result();
            result.Objects = new List<object>();
            ML.Result items = new ML.Result();
            items.ListadoPreguntas = new List<ML.Preguntas>();
            for (int IdPregunta = 1; IdPregunta < 173; IdPregunta++)
            {
                var dta = (BL.ReporteD4U.GetPorcentajeAfirmativasByRangoEdad(IdPregunta, empleado.RangoEdad).Pregunta);
                items.ListadoPreguntas.Add(dta);
            }
            return Json(items.ListadoPreguntas, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPorcentajeAfirmativasByCondicionTrabajo(ML.Empleado empleado)
        {
            var result = new ML.Result();
            result.Objects = new List<object>();
            ML.Result items = new ML.Result();
            items.ListadoPreguntas = new List<ML.Preguntas>();
            for (int IdPregunta = 1; IdPregunta < 173; IdPregunta++)
            {
                var dta = (BL.ReporteD4U.GetPorcentajeAfirmativasByCondicionTrabajo(IdPregunta, empleado.CondicionTrabajo).Pregunta);
                items.ListadoPreguntas.Add(dta);
            }
            return Json(items.ListadoPreguntas, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPorcentajeAfirmativasBySexo(ML.Empleado empleado)
        {
            var result = new ML.Result();
            result.Objects = new List<object>();
            ML.Result items = new ML.Result();
            items.ListadoPreguntas = new List<ML.Preguntas>();
            for (int IdPregunta = 1; IdPregunta < 173; IdPregunta++)
            {
                var dta = (BL.ReporteD4U.GetPorcentajeAfirmativasBySexo(IdPregunta, empleado.Sexo).Pregunta);
                items.ListadoPreguntas.Add(dta);
            }
            return Json(items.ListadoPreguntas, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPorcentajeAfirmativasByGradoAcademico(ML.Empleado empleado)
        {
            var result = new ML.Result();
            result.Objects = new List<object>();
            ML.Result items = new ML.Result();
            items.ListadoPreguntas = new List<ML.Preguntas>();
            for (int IdPregunta = 1; IdPregunta < 173; IdPregunta++)
            {
                var dta = (BL.ReporteD4U.GetPorcentajeAfirmativasByGradoAcademico(IdPregunta, empleado.GradoAcademico).Pregunta);
                items.ListadoPreguntas.Add(dta);
            }
            return Json(items.ListadoPreguntas, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPorcentajeAfirmativasByFuncion(ML.Empleado empleado)
        {
            var result = new ML.Result();
            result.Objects = new List<object>();
            ML.Result items = new ML.Result();
            items.ListadoPreguntas = new List<ML.Preguntas>();
            for (int IdPregunta = 1; IdPregunta < 173; IdPregunta++)
            {
                var dta = (BL.ReporteD4U.GetPorcentajeAfirmativasByFuncion(IdPregunta, empleado.TipoFuncion).Pregunta);
                items.ListadoPreguntas.Add(dta);
            }
            return Json(items.ListadoPreguntas, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult GetCompanyAjaxReportes(ML.Company companydta)
        {
            var result = BL.Company.GetByCompanyCategoriaReporte(companydta.CompanyName);

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
        public ActionResult ReporteFinalByUNegocio()
        {
            string HtmlCodeAlertProgress = "<div id='loadReporte' class='alert alert-info col-lg-12 col-md-12 col-xs-12 col-sm-12' role='alert'> Un reporte se encuentra cargando en otra pestaña, al finalizar la carga se le notificará <div class='spinner-border float-right' role='status'> <span id='load' class='sr-only'>Loading...</span> </div> </div>";
            HttpRuntime.Cache.Add("statusReporteFinal", HtmlCodeAlertProgress, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(8),
                System.Web.Caching.CacheItemPriority.High, null);
            Session["statusReporte"] = HtmlCodeAlertProgress;
            return View(new ML.Encuesta());
        }
        public ActionResult ResetMessageLoad(string locationDownload)
        {
            string HtmlCodeAlertProgress = "<div id='loadReporte' style='margin-top:0px;' class='alert alert-success col-lg-12 col-md-12 col-xs-12 col-sm-12' role='alert'> El reporte ha sido completado. La descarga del mismo ya ya iniciado <div class='float-right' role='status'> <span style='cursor:pointer' onclick='ocultar()'>X</span> </div> </div>";
            HttpRuntime.Cache.Add("statusReporteFinal", HtmlCodeAlertProgress, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(8),
                System.Web.Caching.CacheItemPriority.High, null);
            Session["statusReporte"] = HtmlCodeAlertProgress;
            return Json(HtmlCodeAlertProgress, JsonRequestBehavior.AllowGet);
        }
        public ActionResult resetLoadNull()
        {
            Session["statusReporte"] = "";
            return Json("success", JsonRequestBehavior.AllowGet);
        }
        public ActionResult auxRep()
        {
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        //GetDemograficos For ReportFinal
        public ActionResult GetCondicionTrabajo()
        {
            var result = BL.Empleado.GetAllCondicionTrabajo();
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetGradoAcademico()
        {
            var result = BL.Empleado.GetAllGradoAcademico();
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAntiguedad()
        {
            var result = BL.Empleado.GetAllAntiguedad();
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetRangoEdad()
        {
            var result = BL.Empleado.GetAllRangoEdad();
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPreguntasCredibilidad()
        {
            var result = BL.ReporteD4U.GetPreguntasCredibilidad();
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPreguntasImparcialidad()
        {
            var result = BL.ReporteD4U.GetPreguntasImparcialidad();
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPreguntasOrgullo()
        {
            var result = BL.ReporteD4U.GetPreguntasOrgullo();
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPreguntasGesTalt()
        {
            var result = BL.ReporteD4U.GetPreguntasGESTALT();
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPreguntasCompañerismo()
        {
            var result = BL.ReporteD4U.GetPreguntasCompañerismo();
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPreguntasRespeto()
        {
            var result = BL.ReporteD4U.GetPreguntasRespeto();
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPreguntasCoaching()
        {
            var result = BL.ReporteD4U.GetPreguntasCoaching();
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPreguntasCambio()
        {
            var result = BL.ReporteD4U.GetPreguntasCambio();
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPreguntasBienestar()
        {
            var result = BL.ReporteD4U.GetPreguntasBienestar();
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCompaniesByUNegocio(ML.CompanyCategoria companyCategoria)
        {
            var result = BL.Company.GetByCompanyCategoriaForD4U(companyCategoria.IdCompanyCategoria);
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPreguntasAlineacionCultural()
        {
            var result = BL.ReporteD4U.GetPreguntasAlineacionCultural();
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }



        public ActionResult GetEstructura(ML.CompanyCategoria CompanyCateg, HttpSessionStateBase Session)
        {
            /*Validar repetidos*/
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    if (CompanyCateg.IdCompanyCategoria == 9)
                    {
                        var query = context.GetEstructuraByUNegocioD4U(CompanyCateg.IdCompanyCategoria).ToList();
                        result.Objects = new List<object>();
                        if (query != null)
                        {
                            Session["s"] = "";
                            Session["Company"] = "";
                            Session["Area"] = "";
                            Session["Departamento"] = "";
                            Session["Subdepartamento"] = "";
                            foreach (var item in query)
                            {
                                ML.Company company = new ML.Company();
                                company.CompanyCategoria = new ML.CompanyCategoria();
                                company.Area = new ML.Area();
                                company.Area.Departamento = new ML.Departamento();
                                company.Area.Departamento.Subdepartamento = new ML.Subdepartamento();

                                company.CompanyCategoria.Descripcion = item.UNIDAD_NEGOCIO;
                                company.CompanyCategoria.IdCompanyCategoria = item.ID_UNEGOCIO;
                                company.CompanyId = item.ID_COMPANY;
                                company.Area.IdArea = Convert.ToInt32(item.ID_AREA);
                                company.Area.Departamento.IdDepartamento = Convert.ToInt32(item.ID_DEPARTAMENTO);
                                company.Area.Departamento.Subdepartamento.IdSubdepartamento = Convert.ToInt32(item.ID_SUBDEPARTAMENTO);

                                string Company = Convert.ToString(Session["Company"]);
                                if (Company != item.COMPANY_NAME)
                                {
                                    company.CompanyName = item.COMPANY_NAME;
                                    Session["Company"] = item.COMPANY_NAME;
                                }
                                else
                                {

                                }
                                //Validar area
                                string Area = Convert.ToString(Session["Area"]);
                                if (Area != item.NOMBRE_AREA)
                                {
                                    company.Area.Nombre = item.NOMBRE_AREA;
                                    Session["Area"] = item.NOMBRE_AREA;
                                }
                                else
                                {

                                }
                                //Validar Departamento
                                string Departamento = Convert.ToString(Session["Departamento"]);
                                if (Departamento != item.NOMBRE_DEPARTAMENTO)
                                {
                                    company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                    Session["Departamento"] = item.NOMBRE_DEPARTAMENTO;
                                }
                                else
                                {

                                }
                                //Validar subdepartamento
                                string Subdepartamento = Convert.ToString(Session["Subdepartamento"]);
                                if (Subdepartamento != item.NOMBRE_SUBDEPARTAMENTO)
                                {
                                    company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                    Session["Subdepartamento"] = item.NOMBRE_SUBDEPARTAMENTO;
                                }
                                else
                                {

                                }

                                //company.CompanyName = item.COMPANY_NAME;
                                //company.Area.Nombre = item.NOMBRE_AREA;
                                //company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                //company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                //item.UNIDAD_NEGOCIO;
                                //item.COMPANY_NAME;
                                //item.NOMBRE_AREA;
                                //item.NOMBRE_DEPARTAMENTO;
                                //item.NOMBRE_SUBDEPARTAMENTO;

                                result.Objects.Add(company);
                                result.Correct = true;
                            }
                        }
                    }
                    else
                    {
                        var query = context.GetEstructuraByUNegocioD4USucces(CompanyCateg.IdCompanyCategoria).ToList();
                        result.Objects = new List<object>();
                        if (query != null)
                        {
                            Session["Company"] = "";
                            Session["Area"] = "";
                            Session["Departamento"] = "";
                            Session["Subdepartamento"] = "";
                            foreach (var item in query)
                            {
                                ML.Company company = new ML.Company();
                                company.CompanyCategoria = new ML.CompanyCategoria();
                                company.Area = new ML.Area();
                                company.Area.Departamento = new ML.Departamento();
                                company.Area.Departamento.Subdepartamento = new ML.Subdepartamento();

                                company.CompanyCategoria.Descripcion = item.UNIDAD_NEGOCIO;
                                company.CompanyCategoria.IdCompanyCategoria = item.ID_UNEGOCIO;
                                company.CompanyId = item.ID_COMPANY;
                                company.Area.IdArea = Convert.ToInt32(item.ID_AREA);
                                company.Area.Departamento.IdDepartamento = Convert.ToInt32(item.ID_DEPARTAMENTO);
                                company.Area.Departamento.Subdepartamento.IdSubdepartamento = Convert.ToInt32(item.ID_SUBDEPARTAMENTO);

                                string Company = Convert.ToString(Session["Company"]);
                                if (Company != item.COMPANY_NAME)
                                {
                                    company.CompanyName = item.COMPANY_NAME;
                                    Session["Company"] = item.COMPANY_NAME;
                                }
                                else
                                {

                                }
                                //Validar area
                                string Area = Convert.ToString(Session["Area"]);
                                if (Area != item.NOMBRE_AREA)
                                {
                                    company.Area.Nombre = item.NOMBRE_AREA;
                                    Session["Area"] = item.NOMBRE_AREA;
                                }
                                else
                                {

                                }
                                //Validar Departamento
                                string Departamento = Convert.ToString(Session["Departamento"]);
                                if (Departamento != item.NOMBRE_DEPARTAMENTO)
                                {
                                    company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                    Session["Departamento"] = item.NOMBRE_DEPARTAMENTO;
                                }
                                else
                                {

                                }
                                //Validar subdepartamento
                                string Subdepartamento = Convert.ToString(Session["Subdepartamento"]);
                                if (Subdepartamento != item.NOMBRE_SUBDEPARTAMENTO)
                                {
                                    company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                    Session["Subdepartamento"] = item.NOMBRE_SUBDEPARTAMENTO;
                                }
                                else
                                {

                                }

                                //company.CompanyName = item.COMPANY_NAME;
                                //company.Area.Nombre = item.NOMBRE_AREA;
                                //company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                //company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                //item.UNIDAD_NEGOCIO;
                                //item.COMPANY_NAME;
                                //item.NOMBRE_AREA;
                                //item.NOMBRE_DEPARTAMENTO;
                                //item.NOMBRE_SUBDEPARTAMENTO;

                                result.Objects.Add(company);
                                result.Correct = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                BL.LogReporteoClima.writteLog(result.ErrorMessage, new StackTrace());
            }
            /**/
            //var result = BL.ReporteD4U.GetEstructuraForReporte(CompanyCateg.IdCompanyCategoria);
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetEstructura(ML.CompanyCategoria CompanyCateg)
        {
            /*Validar repetidos*/
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    if (CompanyCateg.IdCompanyCategoria == 9)
                    {
                        var query = context.GetEstructuraByUNegocioD4U(CompanyCateg.IdCompanyCategoria).ToList();
                        result.Objects = new List<object>();
                        if (query != null)
                        {
                            Session["s"] = "";
                            Session["Company"] = "";
                            Session["Area"] = "";
                            Session["Departamento"] = "";
                            Session["Subdepartamento"] = "";
                            foreach (var item in query)
                            {
                                ML.Company company = new ML.Company();
                                company.CompanyCategoria = new ML.CompanyCategoria();
                                company.Area = new ML.Area();
                                company.Area.Departamento = new ML.Departamento();
                                company.Area.Departamento.Subdepartamento = new ML.Subdepartamento();

                                company.CompanyCategoria.Descripcion = item.UNIDAD_NEGOCIO;
                                company.CompanyCategoria.IdCompanyCategoria = item.ID_UNEGOCIO;
                                company.CompanyId = item.ID_COMPANY;
                                company.Area.IdArea = Convert.ToInt32(item.ID_AREA);
                                company.Area.Departamento.IdDepartamento = Convert.ToInt32(item.ID_DEPARTAMENTO);
                                company.Area.Departamento.Subdepartamento.IdSubdepartamento = Convert.ToInt32(item.ID_SUBDEPARTAMENTO);

                                string Company = Convert.ToString(Session["Company"]);
                                if (Company != item.COMPANY_NAME)
                                {
                                    company.CompanyName = item.COMPANY_NAME;
                                    Session["Company"] = item.COMPANY_NAME;
                                }
                                else
                                {

                                }
                                //Validar area
                                string Area = Convert.ToString(Session["Area"]);
                                if (Area != item.NOMBRE_AREA)
                                {
                                    company.Area.Nombre = item.NOMBRE_AREA;
                                    Session["Area"] = item.NOMBRE_AREA;
                                }
                                else
                                {

                                }
                                //Validar Departamento
                                string Departamento = Convert.ToString(Session["Departamento"]);
                                if (Departamento != item.NOMBRE_DEPARTAMENTO)
                                {
                                    company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                    Session["Departamento"] = item.NOMBRE_DEPARTAMENTO;
                                }
                                else
                                {

                                }
                                //Validar subdepartamento
                                string Subdepartamento = Convert.ToString(Session["Subdepartamento"]);
                                if (Subdepartamento != item.NOMBRE_SUBDEPARTAMENTO)
                                {
                                    company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                    Session["Subdepartamento"] = item.NOMBRE_SUBDEPARTAMENTO;
                                }
                                else
                                {

                                }

                                //company.CompanyName = item.COMPANY_NAME;
                                //company.Area.Nombre = item.NOMBRE_AREA;
                                //company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                //company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                //item.UNIDAD_NEGOCIO;
                                //item.COMPANY_NAME;
                                //item.NOMBRE_AREA;
                                //item.NOMBRE_DEPARTAMENTO;
                                //item.NOMBRE_SUBDEPARTAMENTO;

                                result.Objects.Add(company);
                                result.Correct = true;
                            }
                        }
                    }
                    else
                    {
                        var query = context.GetEstructuraByUNegocioD4USucces(CompanyCateg.IdCompanyCategoria).ToList();
                        result.Objects = new List<object>();
                        if (query != null)
                        {
                            Session["Company"] = "";
                            Session["Area"] = "";
                            Session["Departamento"] = "";
                            Session["Subdepartamento"] = "";
                            foreach (var item in query)
                            {
                                ML.Company company = new ML.Company();
                                company.CompanyCategoria = new ML.CompanyCategoria();
                                company.Area = new ML.Area();
                                company.Area.Departamento = new ML.Departamento();
                                company.Area.Departamento.Subdepartamento = new ML.Subdepartamento();

                                company.CompanyCategoria.Descripcion = item.UNIDAD_NEGOCIO;
                                company.CompanyCategoria.IdCompanyCategoria = item.ID_UNEGOCIO;
                                company.CompanyId = item.ID_COMPANY;
                                company.Area.IdArea = Convert.ToInt32(item.ID_AREA);
                                company.Area.Departamento.IdDepartamento = Convert.ToInt32(item.ID_DEPARTAMENTO);
                                company.Area.Departamento.Subdepartamento.IdSubdepartamento = Convert.ToInt32(item.ID_SUBDEPARTAMENTO);

                                string Company = Convert.ToString(Session["Company"]);
                                if (Company != item.COMPANY_NAME)
                                {
                                    company.CompanyName = item.COMPANY_NAME;
                                    Session["Company"] = item.COMPANY_NAME;
                                }
                                else
                                {

                                }
                                //Validar area
                                string Area = Convert.ToString(Session["Area"]);
                                if (Area != item.NOMBRE_AREA)
                                {
                                    company.Area.Nombre = item.NOMBRE_AREA;
                                    Session["Area"] = item.NOMBRE_AREA;
                                }
                                else
                                {

                                }
                                //Validar Departamento
                                string Departamento = Convert.ToString(Session["Departamento"]);
                                if (Departamento != item.NOMBRE_DEPARTAMENTO)
                                {
                                    company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                    Session["Departamento"] = item.NOMBRE_DEPARTAMENTO;
                                }
                                else
                                {

                                }
                                //Validar subdepartamento
                                string Subdepartamento = Convert.ToString(Session["Subdepartamento"]);
                                if (Subdepartamento != item.NOMBRE_SUBDEPARTAMENTO)
                                {
                                    company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                    Session["Subdepartamento"] = item.NOMBRE_SUBDEPARTAMENTO;
                                }
                                else
                                {

                                }

                                //company.CompanyName = item.COMPANY_NAME;
                                //company.Area.Nombre = item.NOMBRE_AREA;
                                //company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                //company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                //item.UNIDAD_NEGOCIO;
                                //item.COMPANY_NAME;
                                //item.NOMBRE_AREA;
                                //item.NOMBRE_DEPARTAMENTO;
                                //item.NOMBRE_SUBDEPARTAMENTO;

                                result.Objects.Add(company);
                                result.Correct = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                BL.LogReporteoClima.writteLog(result.ErrorMessage, new StackTrace());
            }
            /**/
            //var result = BL.ReporteD4U.GetEstructuraForReporte(CompanyCateg.IdCompanyCategoria);
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEstructura_lvl_2(ML.Company aCompany, HttpSessionStateBase aSession)
        {
            /*Validar repetidos*/
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    if (1 == 1)
                    {
                        var query = context.f_getEstructuraDescByCompanyId(aCompany.CompanyId).ToList();
                        result.Objects = new List<object>();
                        if (query != null)
                        {
                            aSession["Company"] = "";
                            aSession["Area"] = "";
                            aSession["Departamento"] = "";
                            aSession["Subdepartamento"] = "";
                            foreach (var item in query)
                            {
                                ML.Company company = new ML.Company();
                                company.CompanyCategoria = new ML.CompanyCategoria();
                                company.Area = new ML.Area();
                                company.Area.Departamento = new ML.Departamento();
                                company.Area.Departamento.Subdepartamento = new ML.Subdepartamento();

                                /*company.CompanyCategoria.Descripcion = item.UNIDAD_NEGOCIO;
                                company.CompanyCategoria.IdCompanyCategoria = item.ID_UNEGOCIO;*/
                                company.CompanyId = item.ID_COMPANY;
                                company.Area.IdArea = Convert.ToInt32(item.ID_AREA);
                                company.Area.Departamento.IdDepartamento = Convert.ToInt32(item.ID_DEPARTAMENTO);
                                company.Area.Departamento.Subdepartamento.IdSubdepartamento = Convert.ToInt32(item.ID_SUBDEPARTAMENTO);

                                string Company = Convert.ToString(aSession["Company"]);
                                if (Company != item.COMPANY_NAME)
                                {
                                    company.CompanyName = item.COMPANY_NAME;
                                    aSession["Company"] = item.COMPANY_NAME;
                                }
                                else
                                {

                                }
                                //Validar area
                                string Area = Convert.ToString(aSession["Area"]);
                                if (Area != item.NOMBRE_AREA)
                                {
                                    company.Area.Nombre = item.NOMBRE_AREA;
                                    aSession["Area"] = item.NOMBRE_AREA;
                                }
                                else
                                {

                                }
                                //Validar Departamento
                                string Departamento = Convert.ToString(aSession["Departamento"]);
                                if (Departamento != item.NOMBRE_DEPARTAMENTO)
                                {
                                    company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                    aSession["Departamento"] = item.NOMBRE_DEPARTAMENTO;
                                }
                                else
                                {

                                }
                                //Validar subdepartamento
                                string Subdepartamento = Convert.ToString(aSession["Subdepartamento"]);
                                if (Subdepartamento != item.NOMBRE_SUBDEPARTAMENTO)
                                {
                                    company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                    aSession["Subdepartamento"] = item.NOMBRE_SUBDEPARTAMENTO;
                                }
                                else
                                {

                                }

                                //company.CompanyName = item.COMPANY_NAME;
                                //company.Area.Nombre = item.NOMBRE_AREA;
                                //company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                //company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                //item.UNIDAD_NEGOCIO;
                                //item.COMPANY_NAME;
                                //item.NOMBRE_AREA;
                                //item.NOMBRE_DEPARTAMENTO;
                                //item.NOMBRE_SUBDEPARTAMENTO;

                                result.Objects.Add(company);
                                result.Correct = true;
                            }
                        }
                    }
                    /*else
                    {
                        var query = context.GetEstructuraByUNegocioD4USucces(aCompany.CompanyId).ToList();
                        result.Objects = new List<object>();
                        if (query != null)
                        {
                            Session["Company"] = "";
                            Session["Area"] = "";
                            Session["Departamento"] = "";
                            Session["Subdepartamento"] = "";
                            foreach (var item in query)
                            {
                                ML.Company company = new ML.Company();
                                company.CompanyCategoria = new ML.CompanyCategoria();
                                company.Area = new ML.Area();
                                company.Area.Departamento = new ML.Departamento();
                                company.Area.Departamento.Subdepartamento = new ML.Subdepartamento();

                                company.CompanyCategoria.Descripcion = item.UNIDAD_NEGOCIO;
                                company.CompanyCategoria.IdCompanyCategoria = item.ID_UNEGOCIO;
                                company.CompanyId = item.ID_COMPANY;
                                company.Area.IdArea = Convert.ToInt32(item.ID_AREA);
                                company.Area.Departamento.IdDepartamento = Convert.ToInt32(item.ID_DEPARTAMENTO);
                                company.Area.Departamento.Subdepartamento.IdSubdepartamento = Convert.ToInt32(item.ID_SUBDEPARTAMENTO);

                                string Company = Convert.ToString(Session["Company"]);
                                if (Company != item.COMPANY_NAME)
                                {
                                    company.CompanyName = item.COMPANY_NAME;
                                    Session["Company"] = item.COMPANY_NAME;
                                }
                                else
                                {

                                }
                                //Validar area
                                string Area = Convert.ToString(Session["Area"]);
                                if (Area != item.NOMBRE_AREA)
                                {
                                    company.Area.Nombre = item.NOMBRE_AREA;
                                    Session["Area"] = item.NOMBRE_AREA;
                                }
                                else
                                {

                                }
                                //Validar Departamento
                                string Departamento = Convert.ToString(Session["Departamento"]);
                                if (Departamento != item.NOMBRE_DEPARTAMENTO)
                                {
                                    company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                    Session["Departamento"] = item.NOMBRE_DEPARTAMENTO;
                                }
                                else
                                {

                                }
                                //Validar subdepartamento
                                string Subdepartamento = Convert.ToString(Session["Subdepartamento"]);
                                if (Subdepartamento != item.NOMBRE_SUBDEPARTAMENTO)
                                {
                                    company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                    Session["Subdepartamento"] = item.NOMBRE_SUBDEPARTAMENTO;
                                }
                                else
                                {

                                }

                                //company.CompanyName = item.COMPANY_NAME;
                                //company.Area.Nombre = item.NOMBRE_AREA;
                                //company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                //company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                //item.UNIDAD_NEGOCIO;
                                //item.COMPANY_NAME;
                                //item.NOMBRE_AREA;
                                //item.NOMBRE_DEPARTAMENTO;
                                //item.NOMBRE_SUBDEPARTAMENTO;

                                result.Objects.Add(company);
                                result.Correct = true;
                            }
                        }
                    }*/
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                BL.LogReporteoClima.writteLog(result.ErrorMessage, new StackTrace());
            }
            /**/
            //var result = BL.ReporteD4U.GetEstructuraForReporte(CompanyCateg.IdCompanyCategoria);
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEstructura_lvl_3(ML.Area aArea, HttpSessionStateBase aSession)
        {
            /*Validar repetidos*/
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    if (1 == 1)
                    {
                        var query = context.f_getEstructuraDescByIdArea(aArea.IdArea).ToList();
                        result.Objects = new List<object>();
                        if (query != null)
                        {
                            aSession["Company"] = "";
                            aSession["Area"] = "";
                            aSession["Departamento"] = "";
                            aSession["Subdepartamento"] = "";
                            foreach (var item in query)
                            {
                                ML.Company company = new ML.Company();
                                company.CompanyCategoria = new ML.CompanyCategoria();
                                company.Area = new ML.Area();
                                company.Area.Departamento = new ML.Departamento();
                                company.Area.Departamento.Subdepartamento = new ML.Subdepartamento();

                                /*company.CompanyCategoria.Descripcion = item.UNIDAD_NEGOCIO;
                                company.CompanyCategoria.IdCompanyCategoria = item.ID_UNEGOCIO;
                                company.CompanyId = item.ID_COMPANY;*/
                                company.Area.IdArea = Convert.ToInt32(item.ID_AREA);
                                company.Area.Departamento.IdDepartamento = Convert.ToInt32(item.ID_DEPARTAMENTO);
                                company.Area.Departamento.Subdepartamento.IdSubdepartamento = Convert.ToInt32(item.ID_SUBDEPARTAMENTO);

                                /*string Company = Convert.ToString(Session["Company"]);
                                if (Company != item.COMPANY_NAME)
                                {
                                    company.CompanyName = item.COMPANY_NAME;
                                    Session["Company"] = item.COMPANY_NAME;
                                }
                                else
                                {

                                }*/
                                //Validar area
                                string Area = Convert.ToString(aSession["Area"]);
                                if (Area != item.NOMBRE_AREA)
                                {
                                    company.Area.Nombre = item.NOMBRE_AREA;
                                    aSession["Area"] = item.NOMBRE_AREA;
                                }
                                else
                                {

                                }
                                //Validar Departamento
                                string Departamento = Convert.ToString(aSession["Departamento"]);
                                if (Departamento != item.NOMBRE_DEPARTAMENTO)
                                {
                                    company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                    aSession["Departamento"] = item.NOMBRE_DEPARTAMENTO;
                                }
                                else
                                {

                                }
                                //Validar subdepartamento
                                string Subdepartamento = Convert.ToString(aSession["Subdepartamento"]);
                                if (Subdepartamento != item.NOMBRE_SUBDEPARTAMENTO)
                                {
                                    company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                    aSession["Subdepartamento"] = item.NOMBRE_SUBDEPARTAMENTO;
                                }
                                else
                                {

                                }

                                //company.CompanyName = item.COMPANY_NAME;
                                //company.Area.Nombre = item.NOMBRE_AREA;
                                //company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                //company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                //item.UNIDAD_NEGOCIO;
                                //item.COMPANY_NAME;
                                //item.NOMBRE_AREA;
                                //item.NOMBRE_DEPARTAMENTO;
                                //item.NOMBRE_SUBDEPARTAMENTO;

                                result.Objects.Add(company);
                                result.Correct = true;
                            }
                        }
                    }
                    /*else
                    {
                        var query = context.GetEstructuraByUNegocioD4USucces(aCompany.CompanyId).ToList();
                        result.Objects = new List<object>();
                        if (query != null)
                        {
                            Session["Company"] = "";
                            Session["Area"] = "";
                            Session["Departamento"] = "";
                            Session["Subdepartamento"] = "";
                            foreach (var item in query)
                            {
                                ML.Company company = new ML.Company();
                                company.CompanyCategoria = new ML.CompanyCategoria();
                                company.Area = new ML.Area();
                                company.Area.Departamento = new ML.Departamento();
                                company.Area.Departamento.Subdepartamento = new ML.Subdepartamento();

                                company.CompanyCategoria.Descripcion = item.UNIDAD_NEGOCIO;
                                company.CompanyCategoria.IdCompanyCategoria = item.ID_UNEGOCIO;
                                company.CompanyId = item.ID_COMPANY;
                                company.Area.IdArea = Convert.ToInt32(item.ID_AREA);
                                company.Area.Departamento.IdDepartamento = Convert.ToInt32(item.ID_DEPARTAMENTO);
                                company.Area.Departamento.Subdepartamento.IdSubdepartamento = Convert.ToInt32(item.ID_SUBDEPARTAMENTO);

                                string Company = Convert.ToString(Session["Company"]);
                                if (Company != item.COMPANY_NAME)
                                {
                                    company.CompanyName = item.COMPANY_NAME;
                                    Session["Company"] = item.COMPANY_NAME;
                                }
                                else
                                {

                                }
                                //Validar area
                                string Area = Convert.ToString(Session["Area"]);
                                if (Area != item.NOMBRE_AREA)
                                {
                                    company.Area.Nombre = item.NOMBRE_AREA;
                                    Session["Area"] = item.NOMBRE_AREA;
                                }
                                else
                                {

                                }
                                //Validar Departamento
                                string Departamento = Convert.ToString(Session["Departamento"]);
                                if (Departamento != item.NOMBRE_DEPARTAMENTO)
                                {
                                    company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                    Session["Departamento"] = item.NOMBRE_DEPARTAMENTO;
                                }
                                else
                                {

                                }
                                //Validar subdepartamento
                                string Subdepartamento = Convert.ToString(Session["Subdepartamento"]);
                                if (Subdepartamento != item.NOMBRE_SUBDEPARTAMENTO)
                                {
                                    company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                    Session["Subdepartamento"] = item.NOMBRE_SUBDEPARTAMENTO;
                                }
                                else
                                {

                                }

                                //company.CompanyName = item.COMPANY_NAME;
                                //company.Area.Nombre = item.NOMBRE_AREA;
                                //company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                //company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                //item.UNIDAD_NEGOCIO;
                                //item.COMPANY_NAME;
                                //item.NOMBRE_AREA;
                                //item.NOMBRE_DEPARTAMENTO;
                                //item.NOMBRE_SUBDEPARTAMENTO;

                                result.Objects.Add(company);
                                result.Correct = true;
                            }
                        }
                    }*/
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                BL.LogReporteoClima.writteLog(result.ErrorMessage, new StackTrace());
            }
            /**/
            //var result = BL.ReporteD4U.GetEstructuraForReporte(CompanyCateg.IdCompanyCategoria);
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEstructura_lvl_4(ML.Departamento aDepartamento, HttpSessionStateBase aSession)
        {
            /*Validar repetidos*/
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    if (1 == 1)
                    {
                        var query = context.f_getEstructuraDescByIdDepartamento(aDepartamento.IdDepartamento).ToList();
                        result.Objects = new List<object>();
                        if (query != null)
                        {
                            aSession["Company"] = "";
                            aSession["Area"] = "";
                            aSession["Departamento"] = "";
                            aSession["Subdepartamento"] = "";
                            foreach (var item in query)
                            {
                                ML.Company company = new ML.Company();
                                company.CompanyCategoria = new ML.CompanyCategoria();
                                company.Area = new ML.Area();
                                company.Area.Departamento = new ML.Departamento();
                                company.Area.Departamento.Subdepartamento = new ML.Subdepartamento();

                                /*company.CompanyCategoria.Descripcion = item.UNIDAD_NEGOCIO;
                                company.CompanyCategoria.IdCompanyCategoria = item.ID_UNEGOCIO;
                                company.CompanyId = item.ID_COMPANY;
                                company.Area.IdArea = Convert.ToInt32(item.ID_AREA);*/
                                company.Area.Departamento.IdDepartamento = Convert.ToInt32(item.ID_DEPARTAMENTO);
                                company.Area.Departamento.Subdepartamento.IdSubdepartamento = Convert.ToInt32(item.ID_SUBDEPARTAMENTO);

                                /*string Company = Convert.ToString(Session["Company"]);
                                if (Company != item.COMPANY_NAME)
                                {
                                    company.CompanyName = item.COMPANY_NAME;
                                    Session["Company"] = item.COMPANY_NAME;
                                }
                                else
                                {

                                }*/
                                //Validar area
                                /*string Area = Convert.ToString(Session["Area"]);
                                if (Area != item.NOMBRE_AREA)
                                {
                                    company.Area.Nombre = item.NOMBRE_AREA;
                                    Session["Area"] = item.NOMBRE_AREA;
                                }
                                else
                                {

                                }*/
                                //Validar Departamento
                                string Departamento = Convert.ToString(aSession["Departamento"]);
                                if (Departamento != item.NOMBRE_DEPARTAMENTO)
                                {
                                    company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                    aSession["Departamento"] = item.NOMBRE_DEPARTAMENTO;
                                }
                                else
                                {

                                }
                                //Validar subdepartamento
                                string Subdepartamento = Convert.ToString(aSession["Subdepartamento"]);
                                if (Subdepartamento != item.NOMBRE_SUBDEPARTAMENTO)
                                {
                                    company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                    aSession["Subdepartamento"] = item.NOMBRE_SUBDEPARTAMENTO;
                                }
                                else
                                {

                                }

                                //company.CompanyName = item.COMPANY_NAME;
                                //company.Area.Nombre = item.NOMBRE_AREA;
                                //company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                //company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                //item.UNIDAD_NEGOCIO;
                                //item.COMPANY_NAME;
                                //item.NOMBRE_AREA;
                                //item.NOMBRE_DEPARTAMENTO;
                                //item.NOMBRE_SUBDEPARTAMENTO;

                                result.Objects.Add(company);
                                result.Correct = true;
                            }
                        }
                    }
                    /*else
                    {
                        var query = context.GetEstructuraByUNegocioD4USucces(aCompany.CompanyId).ToList();
                        result.Objects = new List<object>();
                        if (query != null)
                        {
                            Session["Company"] = "";
                            Session["Area"] = "";
                            Session["Departamento"] = "";
                            Session["Subdepartamento"] = "";
                            foreach (var item in query)
                            {
                                ML.Company company = new ML.Company();
                                company.CompanyCategoria = new ML.CompanyCategoria();
                                company.Area = new ML.Area();
                                company.Area.Departamento = new ML.Departamento();
                                company.Area.Departamento.Subdepartamento = new ML.Subdepartamento();

                                company.CompanyCategoria.Descripcion = item.UNIDAD_NEGOCIO;
                                company.CompanyCategoria.IdCompanyCategoria = item.ID_UNEGOCIO;
                                company.CompanyId = item.ID_COMPANY;
                                company.Area.IdArea = Convert.ToInt32(item.ID_AREA);
                                company.Area.Departamento.IdDepartamento = Convert.ToInt32(item.ID_DEPARTAMENTO);
                                company.Area.Departamento.Subdepartamento.IdSubdepartamento = Convert.ToInt32(item.ID_SUBDEPARTAMENTO);

                                string Company = Convert.ToString(Session["Company"]);
                                if (Company != item.COMPANY_NAME)
                                {
                                    company.CompanyName = item.COMPANY_NAME;
                                    Session["Company"] = item.COMPANY_NAME;
                                }
                                else
                                {

                                }
                                //Validar area
                                string Area = Convert.ToString(Session["Area"]);
                                if (Area != item.NOMBRE_AREA)
                                {
                                    company.Area.Nombre = item.NOMBRE_AREA;
                                    Session["Area"] = item.NOMBRE_AREA;
                                }
                                else
                                {

                                }
                                //Validar Departamento
                                string Departamento = Convert.ToString(Session["Departamento"]);
                                if (Departamento != item.NOMBRE_DEPARTAMENTO)
                                {
                                    company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                    Session["Departamento"] = item.NOMBRE_DEPARTAMENTO;
                                }
                                else
                                {

                                }
                                //Validar subdepartamento
                                string Subdepartamento = Convert.ToString(Session["Subdepartamento"]);
                                if (Subdepartamento != item.NOMBRE_SUBDEPARTAMENTO)
                                {
                                    company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                    Session["Subdepartamento"] = item.NOMBRE_SUBDEPARTAMENTO;
                                }
                                else
                                {

                                }

                                //company.CompanyName = item.COMPANY_NAME;
                                //company.Area.Nombre = item.NOMBRE_AREA;
                                //company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                //company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                //item.UNIDAD_NEGOCIO;
                                //item.COMPANY_NAME;
                                //item.NOMBRE_AREA;
                                //item.NOMBRE_DEPARTAMENTO;
                                //item.NOMBRE_SUBDEPARTAMENTO;

                                result.Objects.Add(company);
                                result.Correct = true;
                            }
                        }
                    }*/
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                BL.LogReporteoClima.writteLog(result.ErrorMessage, new StackTrace());
            }
            /**/
            //var result = BL.ReporteD4U.GetEstructuraForReporte(CompanyCateg.IdCompanyCategoria);
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }



        //GetPageGenerate
        [HttpGet]
        public ActionResult CustomReport()
        {
            return View();
        }
        public ActionResult GetAllUneg()
        {
            var result = BL.CompanyCategoria.GetAll();
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllArrays(List<int> resultados)
        {
            listaGlobal.Add(resultados);

            if (listaGlobal.Count() == 300)//Todas las filas
            {
                return Json(listaGlobal, JsonRequestBehavior.AllowGet);
            }
            return Json(listaGlobal, JsonRequestBehavior.AllowGet);
        }


        //Murillo García José Antonio
        /*Reporte completo clima laboral*/
        //Reporte master
        [HttpPost]
        public ActionResult GetEsperadas(ML.ReporteD4U model, int anioActual = 0)
        {
            try
            {
                // cuando cae a este metodo desde la vista el anio viene en cero y el valor verdadero cae en el modelo
                // cuando cae a este metodo desde otro metodo el anio viene en anioActual
                if (anioActual == 0)
                    anioActual = model.Anio;
                // el parametro para filtrar tambien por BD ya lo traigo cuando se invoca el metodo en la vista, falta agregarlo cuando viene desde
                // otro metodo
                Console.Write(model.IdBD);
                if (anioActual == 0)
                    Console.Write("");
                List<int> resultados = new List<int>();
                foreach (string item in model.ListFiltros)
                {
                    string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                    arreglo = aux.ToCharArray();
                    string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                    string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                    if (prefijo == "UNeg=>")
                        model.UnidadNegocioFilter = DataForFilter;

                    if (prefijo == "UNeg=>")
                    {
                        var result = BL.ReporteD4U.GetEsperadasByUNEGocio(DataForFilter, anioActual, model.IdBD);
                        resultados.Add(result);
                    }
                    if (prefijo == "Gene=>")
                    {
                        var result = BL.ReporteD4U.GetEsperadasByGenero(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                        resultados.Add(result);
                    }
                    if (prefijo == "Func=>")
                    {
                        var result = BL.ReporteD4U.GetEsperadasByFuncion(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                        resultados.Add(result);
                    }
                    if (prefijo == "CTra=>")
                    {
                        var result = BL.ReporteD4U.GetEsperadasByCondicionTrabajo(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                        resultados.Add(result);
                    }
                    if (prefijo == "GAca=>")
                    {
                        var result = BL.ReporteD4U.GetEsperadasByGradoAcademico(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                        resultados.Add(result);
                    }
                    if (prefijo == "RAnt=>")
                    {
                        var result = BL.ReporteD4U.GetEsperadasByRangoAntiguedad(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                        resultados.Add(result);
                    }
                    if (prefijo == "REda=>")
                    {
                        var result = BL.ReporteD4U.GetEsperadasByRangoEdad(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                        resultados.Add(result);
                    }
                    if (prefijo == "Comp=>")
                    {
                        var result = BL.ReporteD4U.GetEsperadasByCompany(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                        resultados.Add(result);
                    }
                    if (prefijo == "Area=>")
                    {
                        var result = BL.ReporteD4U.GetEsperadasByArea(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                        resultados.Add(result);
                    }
                    if (prefijo == "Dpto=>")
                    {
                        var result = BL.ReporteD4U.GetEsperadasByDepartamento(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                        resultados.Add(result);
                    }
                    if (prefijo == "SubD=>")
                    {
                        var result = BL.ReporteD4U.GetEsperadasBySubdepartamento(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                        resultados.Add(result);
                    }
                }
                //Guardar los datos en el nuevo historico

                return Json(resultados, JsonRequestBehavior.AllowGet);
            }
            catch (Exception aE)
            {
                var st = new StackTrace();
                var fs = st.GetFrame(0);
                BL.LogReporteoClima.writteLog(aE.Message, new StackTrace());
                return Json(false);
            }
        }
        public ActionResult GetContestadas(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            // falta hacer este filtrado
            Console.Write(model.IdBD + model.Anio);
            List<int> resultados = new List<int>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter
                if (prefijo == "UNeg=>")
                    model.UnidadNegocioFilter = DataForFilter;

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetContestadasByUNEGocio(DataForFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetContestadaByGenero(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetContestadaByFuncion(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetContestadaByCondicionTrabajo(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetContestadaByGradoAcademico(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetContestadaByRangoAntiguedad(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetContestadaByRangoEdad(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetContestadaByCompany(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetContestadaByArea(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetContestadaByDepartamento(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetContestadaBySubdepartamento(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        public List<double> GetPorcentajeAfirmativasEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            Console.Write(model.IdBD);
            if (anioActual == 0)
                Console.Write("");
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                // set unidad negocio
                if (prefijo == "UNeg=>")
                    model.UnidadNegocioFilter = DataForFilter;

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPorcentajeAfirmativasByUNEGocioEnfoqueEmpresa(model.IdPregunta, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPorcentajeAfirmativasByGeneroEnfoqueEmpresa(DataForFilter, model.IdPregunta, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "Func=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPorcentajeAfirmativasByFuncionEnfoqueEmpresa(DataForFilter, model.IdPregunta, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPorcentajeAfirmativasByCondicionTrabajoEnfoqueEmpresa(DataForFilter, model.IdPregunta, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPorcentajeAfirmativasByGradoAcademicoEnfoqueEmpresa(DataForFilter, model.IdPregunta, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPorcentajeAfirmativasByRangoAntiguedadEnfoqueEmpresa(DataForFilter, model.IdPregunta, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPorcentajeAfirmativasByRangoEdadEnfoqueEmpresa(DataForFilter, model.IdPregunta, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPorcentajeAfirmativasByCompanyEnfoqueEmpresa(DataForFilter, model.IdPregunta, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPorcentajeAfirmativasByAreaEnfoqueEmpresa(DataForFilter, model.IdPregunta, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPorcentajeAfirmativasByDepartamentoEnfoqueEmpresa(DataForFilter, model.IdPregunta, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPorcentajeAfirmativasBySubdepartamentoEnfoqueEmpresa(DataForFilter, model.IdPregunta, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
            }
            return resultados;
        }

        [HttpPost]
        public ActionResult GetPorcentajeAfirmativasEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0, string s = "")
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            Console.Write(model.IdBD);
            if (anioActual == 0)
                Console.Write("");
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter
                if (prefijo == "UNeg=>") { model.UnidadNegocioFilter = DataForFilter; }

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPorcentajeAfirmativasByUNEGocioEnfoqueEmpresa(model.IdPregunta, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPorcentajeAfirmativasByGeneroEnfoqueEmpresa(DataForFilter, model.IdPregunta, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "Func=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPorcentajeAfirmativasByFuncionEnfoqueEmpresa(DataForFilter, model.IdPregunta, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPorcentajeAfirmativasByCondicionTrabajoEnfoqueEmpresa(DataForFilter, model.IdPregunta, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPorcentajeAfirmativasByGradoAcademicoEnfoqueEmpresa(DataForFilter, model.IdPregunta, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPorcentajeAfirmativasByRangoAntiguedadEnfoqueEmpresa(DataForFilter, model.IdPregunta, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPorcentajeAfirmativasByRangoEdadEnfoqueEmpresa(DataForFilter, model.IdPregunta, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPorcentajeAfirmativasByCompanyEnfoqueEmpresa(DataForFilter, model.IdPregunta, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPorcentajeAfirmativasByAreaEnfoqueEmpresa(DataForFilter, model.IdPregunta, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPorcentajeAfirmativasByDepartamentoEnfoqueEmpresa(DataForFilter, model.IdPregunta, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPorcentajeAfirmativasBySubdepartamentoEnfoqueEmpresa(DataForFilter, model.IdPregunta, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }

        
        

        public ActionResult GetPorcentajeParticipacionEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {
            try
            {
                if (anioActual == 0)
                    anioActual = model.Anio;
                Console.Write(model.IdBD);
                if (anioActual == 0)
                    Console.Write("");
                List<double> resultados = new List<double>();
                foreach (string item in model.ListFiltros)
                {
                    string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                    arreglo = aux.ToCharArray();
                    string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                    string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                    //Set Data for filter

                    if (prefijo == "UNeg=>")
                    {
                        //Metodo por unidad de negocio
                        var result = BL.ReporteD4U.GetParticipacionByUNEGocio(DataForFilter, anioActual, model.IdBD);
                        if (Double.IsNaN(result) == true)
                        {
                            result = 0;
                        }
                        resultados.Add(result);
                    }
                    if (prefijo == "Gene=>")
                    {
                        //Metodo por genero
                        var result = BL.ReporteD4U.GetParticipacionByGenero(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                        if (Double.IsNaN(result) == true)
                        {
                            result = 0;
                        }
                        resultados.Add(result);
                    }

                    if (prefijo == "Func=>")
                    {
                        //Metodo por genero
                        var result = BL.ReporteD4U.GetParticipacionByFuncion(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                        if (Double.IsNaN(result) == true)
                        {
                            result = 0;
                        }
                        resultados.Add(result);
                    }
                    if (prefijo == "CTra=>")
                    {
                        //Metodo por genero
                        var result = BL.ReporteD4U.GetParticipacionByCondicionTrabajo(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                        if (Double.IsNaN(result) == true)
                        {
                            result = 0;
                        }
                        resultados.Add(result);
                    }
                    if (prefijo == "GAca=>")
                    {
                        //Metodo por genero
                        var result = BL.ReporteD4U.GetParticipacionByGradoAcademico(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                        if (Double.IsNaN(result) == true)
                        {
                            result = 0;
                        }
                        resultados.Add(result);
                    }
                    if (prefijo == "RAnt=>")
                    {
                        //Metodo por genero
                        var result = BL.ReporteD4U.GetParticipacionByRangoAntiguedad(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                        if (Double.IsNaN(result) == true)
                        {
                            result = 0;
                        }
                        resultados.Add(result);
                    }
                    if (prefijo == "REda=>")
                    {
                        //Metodo por genero
                        var result = BL.ReporteD4U.GetParticipacionByRangoEdad(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                        if (Double.IsNaN(result) == true)
                        {
                            result = 0;
                        }
                        resultados.Add(result);
                    }
                    if (prefijo == "Comp=>")
                    {
                        //Metodo por genero
                        var result = BL.ReporteD4U.GetParticipacionByCompany(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                        if (Double.IsNaN(result) == true)
                        {
                            result = 0;
                        }
                        resultados.Add(result);
                    }
                    if (prefijo == "Area=>")
                    {
                        //Metodo por genero
                        var result = BL.ReporteD4U.GetParticipacionByArea(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                        if (Double.IsNaN(result) == true)
                        {
                            result = 0;
                        }
                        resultados.Add(result);
                    }
                    if (prefijo == "Dpto=>")
                    {
                        //Metodo por genero
                        var result = BL.ReporteD4U.GetParticipacionByDepartamento(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                        if (Double.IsNaN(result) == true)
                        {
                            result = 0;
                        }
                        resultados.Add(result);
                    }
                    if (prefijo == "SubD=>")
                    {
                        //Metodo por genero
                        var result = BL.ReporteD4U.GetParticipacionBySubDepartamento(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                        if (Double.IsNaN(result) == true)
                        {
                            result = 0;
                        }
                        resultados.Add(result);
                    }

                }
                return Json(resultados, JsonRequestBehavior.AllowGet);
            }
            catch (Exception aE)
            {
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                BL.LogReporteoClima.writteLog(aE.Message, new StackTrace());
                return Json(false);
            }
        }

        /************************ENFOQUE EMPRESA************************/
        /************************ENFOQUE EMPRESA************************/
        /************************ENFOQUE EMPRESA************************/
        /************************ENFOQUE EMPRESA************************/
        /************************ENFOQUE EMPRESA************************/
        /************************ENFOQUE EMPRESA************************/
        /************************ENFOQUE EMPRESA************************/
        /************************ENFOQUE EMPRESA************************/
        //GetPromediosCredibilidadEE
        public JsonResult GetPromediosCreedibilidadEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            Console.Write(model.IdBD);
            if (anioActual == 0)
                Console.Write("");
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioCredibilidadByUNEGocioEE(DataForFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCredibilidadByGeneroEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCredibilidadByFuncionEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCredibilidadByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCredibilidadByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCredibilidadByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCredibilidadByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCredibilidadByCompanyEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCredibilidadByAreaEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCredibilidadByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCredibilidadBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPrmediosImparcialidadEE
        public JsonResult GetPromediosImparcialidadEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            Console.Write(model.IdBD);
            if (anioActual == 0)
                Console.Write("");
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioImparcialidadByUNEGocioEE(DataForFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioImparcialidadByGeneroEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioImparcialidadByFuncionEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioImparcialidadByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioImparcialidadByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioImparcialidadByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioImparcialidadByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioImparcialidadByCompanyEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioImparcialidadByAreaEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioImparcialidadByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioImparcialidadBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromediosOrgulloEE
        public JsonResult GetPromediosOrgulloEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            Console.Write(model.IdBD);
            if (anioActual == 0)
                Console.Write("");
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioOrgulloByUNEGocioEE(DataForFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOrgulloByGeneroEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOrgulloByFuncionEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOrgulloByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOrgulloByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOrgulloByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOrgulloByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOrgulloByCompanyEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOrgulloByAreaEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOrgulloByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOrgulloBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromediosRespetoEE
        public JsonResult GetPromediosRespetoEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            Console.Write(model.IdBD);
            if (anioActual == 0)
                Console.Write("");
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioRespetoByUNEGocioEE(DataForFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioRespetoByGeneroEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioRespetoByFuncionEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioRespetoByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioRespetoByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioRespetoByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioRespetoByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioRespetoByCompanyEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioRespetoByAreaEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioRespetoByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioRespetoBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromediosCompañerismoEE
        public JsonResult GetPromediosCompañerismoEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            Console.Write(model.IdBD);
            if (anioActual == 0)
                Console.Write("");
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioCompañerismoByUNEGocioEE(DataForFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCompañerismoByGeneroEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCompañerismoByFuncionEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCompañerismoByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCompañerismoByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCompañerismoByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCompañerismoByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCompañerismoByCompanyEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCompañerismoByAreaEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCompañerismoByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCompañerismoBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromediosCoachingEE
        public JsonResult GetPromediosCoachingEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            Console.Write(model.IdBD);
            if (anioActual == 0)
                Console.Write("");
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioCoachingByUNEGocioEE(DataForFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoachingByGeneroEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoachingByFuncionEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoachingByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoachingByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoachingByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoachingByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoachingByCompanyEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoachingByAreaEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoachingByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoachingBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPrmediosCambio
        public JsonResult GetPromediosCambioEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            Console.Write(model.IdBD);
            if (anioActual == 0)
                Console.Write("");
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioCambioByUNEGocioEE(DataForFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCambioByGeneroEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCambioByFuncionEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCambioByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCambioByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCambioByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCambioByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCambioByCompanyEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCambioByAreaEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCambioByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCambioBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromediosBienestarEE
        public ActionResult GetPromediosBienestarEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            Console.Write(model.IdBD);
            if (anioActual == 0)
                Console.Write("");
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioBienestarByUNEGocioEE(DataForFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBienestarByGeneroEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBienestarByFuncionEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBienestarByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBienestarByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBienestarByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBienestarByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBienestarByCompanyEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBienestarByAreaEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBienestarByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBienestarBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromediosAlineacionCulturalEE
        public ActionResult GetPromediosAlinCulturalEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            Console.Write(model.IdBD);
            if (anioActual == 0)
                Console.Write("");
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalByUNEGocioEE(DataForFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalByGeneroEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalByFuncionEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalByCompanyEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalByAreaEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromediosGestaltEE
        public ActionResult GetPromediosGestaltEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            Console.Write(model.IdBD);
            if (anioActual == 0)
                Console.Write("");
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioGestaltByUNEGocioEE(DataForFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioGestaltByGeneroEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioGestaltByFuncionEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioGestaltByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioGestaltByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioGestaltByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioGestaltByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioGestaltByCompanyEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioGestaltByAreaEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioGestaltByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioGestaltBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromedioConfianzaEE
        public JsonResult GetPromediosConfianzaEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            Console.Write(model.IdBD);
            if (anioActual == 0)
                Console.Write("");
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioConfianzaByUNEGocioEE(DataForFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioConfianzaByGeneroEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioConfianzaByFuncionEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioConfianzaByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioConfianzaByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioConfianzaByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioConfianzaByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioConfianzaByCompanyEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioConfianzaByAreaEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioConfianzaByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioConfianzaBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }

        //Get Total de impulsoresclave para el logro de resultados

        

        //GetPromedios 66 Reactivos
        public JsonResult GetPromedios66ReactivosEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            if (anioActual == 0)
                Console.Write("");
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioOneTo66ByUNEGocioEE(DataForFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOneTo66ByGeneroEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOneTo66ByFuncionEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOneTo66ByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOneTo66ByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOneTo66ByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOneTo66ByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOneTo66ByCompanyEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOneTo66ByAreaEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOneTo66ByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOneTo66BySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromedio 86 Reactivos
        public ActionResult GetPromedios86ReactivosEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            if (anioActual == 0)
                Console.Write("");
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedio86ReactivosByUNEGocioEE(DataForFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio86ReactivosByGeneroEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio86ReactivosByFuncionEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio86ReactivosByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio86ReactivosByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio86ReactivosByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio86ReactivosByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio86ReactivosByCompanyEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio86ReactivosByAreaEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio86ReactivosByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio86ReactivosBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromedio PracticasCultureales
        public JsonResult GetPromediosPracticasCulturealesEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            if (anioActual == 0)
                Console.Write("");
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesByUNEGocioEE(DataForFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesByGeneroEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesByFuncionEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesByCompanyEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesByAreaEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromedioReclutandoYDandoLaBienvenida
        public ActionResult GetPromediosReclutandoBienvenidaEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            if (anioActual == 0)
                Console.Write("");
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaByUNEGocioEE(DataForFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaByGeneroEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaByFuncionEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaByCompanyEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaByAreaEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromediosInspirando
        public ActionResult GetPromediosInspirandoEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {

            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioInspirandoByUNEGocioEE(DataForFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioInspirandoByGeneroEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioInspirandoByFuncionEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioInspirandoByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioInspirandoByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioInspirandoByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioInspirandoByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioInspirandoByCompanyEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioInspirandoByAreaEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioInspirandoByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioInspirandoBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromedioHablando
        public ActionResult GetPromediosHablandoEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {

            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioHablandoByUNEGocioEE(DataForFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHablandoByGeneroEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHablandoByFuncionEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHablandoByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHablandoByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHablandoByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHablandoByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHablandoByCompanyEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHablandoByAreaEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHablandoByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHablandoBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromedioEscuchando
        public ActionResult GetPromediosEscuchandoEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {

            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioEscuchandoByUNEGocioEE(DataForFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEscuchandoByGeneroEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEscuchandoByFuncionEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEscuchandoByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEscuchandoByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEscuchandoByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEscuchandoByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEscuchandoByCompanyEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEscuchandoByAreaEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEscuchandoByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEscuchandoBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromediosAgradeciendo
        public ActionResult GetPromediosAgradeciendoEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {

            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoByUNEGocioEE(DataForFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoByGeneroEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoByFuncionEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoByCompanyEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoByAreaEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromedioDesarrollando
        public ActionResult GetPromediosDesarrollandoEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoByUNEGocioEE(DataForFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoByGeneroEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoByFuncionEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoByCompanyEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoByAreaEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromedioCuidando
        public ActionResult GetPromediosCuidandoEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioCuidandoByUNEGocioEE(DataForFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCuidandoByGeneroEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCuidandoByFuncionEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCuidandoByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCuidandoByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCuidandoByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCuidandoByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCuidandoByCompanyEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCuidandoByAreaEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCuidandoByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCuidandoBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromedioPercepcionLugar
        public ActionResult GetPromediosPercepcionLugarEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarByUNEGocioEE(DataForFilter, anioActual, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarByGeneroEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarByFuncionEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarByCompanyEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarByAreaEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromedioCooperando
        public ActionResult GetPromediosCooperandoEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioCooperandoByUNEGocioEE(DataForFilter, anioActual, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCooperandoByGeneroEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCooperandoByFuncionEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCooperandoByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCooperandoByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCooperandoByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCooperandoByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCooperandoByCompanyEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCooperandoByAreaEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCooperandoByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCooperandoBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromedioAlineacionEstrategica
        public JsonResult GetPromediosAlineacionEstrategicaEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaByUNEGocioEE(DataForFilter, anioActual, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaByGeneroEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaByFuncionEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaByCompanyEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaByAreaEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromedioProcesosOrganizacionales
        public JsonResult GetPromediosProcesosOrganizacionalesEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganByUNEGocioEE(DataForFilter, anioActual, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganByGeneroEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganByFuncionEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganByCompanyEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganByAreaEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromedioPlanes de Carrera
        public ActionResult GetPromediosCarreraYPromocionPersEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionByUNEGocioEE(DataForFilter, anioActual, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionByGeneroEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionByFuncionEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionByCompanyEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionByAreaEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromedioCapacitacionYDesarrollo
        public ActionResult GetPromediosCapacitacionYDesarrolloEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloByUNEGocioEE(DataForFilter, anioActual, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloByGeneroEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloByFuncionEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloByCompanyEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloByAreaEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GEtPromedio ENVIROMENT
        public ActionResult GetPromediosEMPOWERMENTEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTByUNEGocioEE(DataForFilter, model.Anio, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTByGeneroEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTByFuncionEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTByCompanyEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTByAreaEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromedioEvalDesempeño
        public ActionResult GetPromediosEvalDesempeñoEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoByUNEGocioEE(DataForFilter, anioActual, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoByGeneroEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoByFuncionEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoByCompanyEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoByAreaEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPromediosEvalDesempeñoMoradoEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item; if (aux.Contains("_")) { aux = aux.Split('_')[1]; model.UnidadNegocioFilter = item.Split('_')[0]; }
                char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6); if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoMoradoByUNEGocioEE(DataForFilter, anioActual, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoMoradoByGeneroEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoMoradoByFuncionEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoMoradoByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoMoradoByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoMoradoByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoMoradoByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoMoradoByCompanyEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoMoradoByAreaEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoMoradoByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoMoradoBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPromediosEvalDesempeñoMoradoEnfoqueArea(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item; if (aux.Contains("_")) { aux = aux.Split('_')[1]; model.UnidadNegocioFilter = item.Split('_')[0]; }
                char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6); if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoMoradoByUNEGocioEA(DataForFilter, anioActual, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoMoradoByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoMoradoByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoMoradoByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoMoradoByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoMoradoByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoMoradoByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoMoradoByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoMoradoByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoMoradoByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoMoradoBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }

        //GetPromedioIntegracion
        public ActionResult GetPromediosIntegracionEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioIntegracionByUNEGocioEE(DataForFilter, anioActual, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioIntegracionByGeneroEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioIntegracionByFuncionEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioIntegracionByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioIntegracionByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioIntegracionByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioIntegracionByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioIntegracionByCompanyEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioIntegracionByAreaEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioIntegracionByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioIntegracionBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromedioNivelCoolaboracion
        public JsonResult GetPromediosNivelCoolaboracionEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionByUNEGocioEE(DataForFilter, anioActual, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionByGeneroEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionByFuncionEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionByCompanyEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionByAreaEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //getPromedioNivelCompromiso
        public JsonResult GetPromediosNivelCompromisoEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoByUNEGocioEE(DataForFilter, anioActual, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoByGeneroEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoByFuncionEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoByCompanyEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoByAreaEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromedioFacoteSocial
        public ActionResult GetPromediosFactorSocialEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioFactorSocialByUNEGocioEE(DataForFilter, model.Anio, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorSocialByGeneroEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorSocialByFuncionEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorSocialByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorSocialByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorSocialByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorSocialByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorSocialByCompanyEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorSocialByAreaEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorSocialByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorSocialBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPromediosFactorPsicoEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoByUNEGocioEE(DataForFilter, model.Anio, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoByGeneroEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoByFuncionEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoByCompanyEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoByAreaEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //Factor fisico
        public ActionResult GetPromediosFactorFisicoEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoByUNEGocioEE(DataForFilter, model.Anio, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoByGeneroEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoByFuncionEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoByCompanyEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoByAreaEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //Getpromedio Bio
        public ActionResult GetPromediosBioEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioBioByUNEGocioEE(DataForFilter, model.Anio, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBioByGeneroEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBioByFuncionEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBioByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBioByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBioByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBioByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBioByCompanyEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBioByAreaEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBioByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBioBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //Getpromedio Bio
        public ActionResult GetPromediosPsicoEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioPsicoByUNEGocioEE(DataForFilter, model.Anio, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPsicoByGeneroEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPsicoByFuncionEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPsicoByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPsicoByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPsicoByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPsicoByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPsicoByCompanyEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPsicoByAreaEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPsicoByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPsicoBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPromediosSocialEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioSocialByUNEGocioEE(DataForFilter, model.Anio, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioSocialByGeneroEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioSocialByFuncionEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioSocialByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioSocialByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioSocialByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioSocialByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioSocialByCompanyEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioSocialByAreaEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioSocialByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioSocialBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPromediosComunicacionEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioComunicacionByUNEGocioEE(DataForFilter, anioActual, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioComunicacionByGeneroEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioComunicacionByFuncionEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioComunicacionByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioComunicacionByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioComunicacionByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioComunicacionByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioComunicacionByCompanyEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioComunicacionByAreaEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioComunicacionByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioComunicacionBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPromediosEnpowerEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioEnpowerByUNEGocioEE(DataForFilter, anioActual, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerByGeneroEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerByFuncionEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerByCompanyEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerByAreaEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPromediosEnpowerNaranjaEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item; if (aux.Contains("_")) { aux = aux.Split('_')[1]; model.UnidadNegocioFilter = item.Split('_')[0]; }
                char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6); if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioEnpowerNaranjaByUNEGocioEE(DataForFilter, anioActual, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerNaranjaByGeneroEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerNaranjaByFuncionEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerNaranjaByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerNaranjaByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerNaranjaByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerNaranjaByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerNaranjaByCompanyEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerNaranjaByAreaEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerNaranjaByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerNaranjaBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPromediosEnpowerNaranjaEnfoqueArea(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item; if (aux.Contains("_")) { aux = aux.Split('_')[1]; model.UnidadNegocioFilter = item.Split('_')[0]; }
                char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6); if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioEnpowerNaranjaByUNEGocioEA(DataForFilter, anioActual, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerNaranjaByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerNaranjaByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerNaranjaByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerNaranjaByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerNaranjaByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerNaranjaByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerNaranjaByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerNaranjaByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerNaranjaByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerNaranjaBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPromediosCoordinacionEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioCoordinacionByUNEGocioEE(DataForFilter, anioActual, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoordinacionByGeneroEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoordinacionByFuncionEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoordinacionByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoordinacionByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoordinacionByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoordinacionByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoordinacionByCompanyEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoordinacionByAreaEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoordinacionByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoordinacionBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPromediosVisionEstrateEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateByUNEGocioEE(DataForFilter, anioActual, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateByGeneroEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateByFuncionEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateByCompanyEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateByAreaEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPromediosNivelDesempeñoEstrateEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoByUNEGocioEE(DataForFilter, model.Anio, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoByGeneroEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoByFuncionEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoByCompanyEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoByAreaEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }



        //**********************ENFOQUE AREA************************//
        //**********************ENFOQUE AREA************************//
        //**********************ENFOQUE AREA************************//
        //**********************ENFOQUE AREA************************//
        //**********************ENFOQUE AREA************************//
        //**********************ENFOQUE AREA************************//
        public JsonResult GetPromediosCreedibilidadEnfoqueArea(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioCredibilidadByUNEGocioEA(DataForFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCredibilidadByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCredibilidadByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCredibilidadByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCredibilidadByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCredibilidadByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCredibilidadByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCredibilidadByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCredibilidadByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCredibilidadByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCredibilidadBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPrmediosImparcialidadEA
        public JsonResult GetPromediosImparcialidadEnfoqueArea(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioImparcialidadByUNEGocioEA(DataForFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioImparcialidadByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioImparcialidadByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioImparcialidadByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioImparcialidadByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioImparcialidadByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioImparcialidadByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioImparcialidadByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioImparcialidadByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioImparcialidadByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioImparcialidadBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromediosOrgulloEA
        public JsonResult GetPromediosOrgulloEnfoqueArea(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioOrgulloByUNEGocioEA(DataForFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOrgulloByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOrgulloByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOrgulloByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOrgulloByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOrgulloByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOrgulloByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOrgulloByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOrgulloByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOrgulloByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOrgulloBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromediosRespetoEA
        public JsonResult GetPromediosRespetoEnfoqueArea(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioRespetoByUNEGocioEA(DataForFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioRespetoByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioRespetoByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioRespetoByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioRespetoByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioRespetoByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioRespetoByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioRespetoByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioRespetoByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioRespetoByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioRespetoBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromediosCompañerismoEA
        public JsonResult GetPromediosCompañerismoEnfoqueArea(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioCompañerismoByUNEGocioEA(DataForFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCompañerismoByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCompañerismoByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCompañerismoByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCompañerismoByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCompañerismoByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCompañerismoByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCompañerismoByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCompañerismoByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCompañerismoByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCompañerismoBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromediosCoachingEA
        public JsonResult GetPromediosCoachingEnfoqueArea(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioCoachingByUNEGocioEA(DataForFilter,anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoachingByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoachingByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoachingByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoachingByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoachingByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoachingByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoachingByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoachingByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoachingByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoachingBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPrmediosCambio
        public JsonResult GetPromediosCambioEnfoqueArea(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioCambioByUNEGocioEA(DataForFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCambioByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCambioByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCambioByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCambioByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCambioByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCambioByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCambioByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCambioByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCambioByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCambioBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromediosBienestarEA
        public ActionResult GetPromediosBienestarEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioBienestarByUNEGocioEA(DataForFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBienestarByGeneroEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBienestarByFuncionEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBienestarByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBienestarByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBienestarByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBienestarByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBienestarByCompanyEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBienestarByAreaEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBienestarByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBienestarBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromediosAlineacionCulturalEA
        public ActionResult GetPromediosAlinCulturalEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalByUNEGocioEA(DataForFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalByGeneroEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalByFuncionEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalByCompanyEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalByAreaEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromediosGestaltEA
        public ActionResult GetPromediosGestaltEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioGestaltByUNEGocioEA(DataForFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioGestaltByGeneroEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioGestaltByFuncionEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioGestaltByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioGestaltByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioGestaltByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioGestaltByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioGestaltByCompanyEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioGestaltByAreaEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioGestaltByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioGestaltBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromedioConfianzaEA
        public JsonResult GetPromediosConfianzaEnfoqueArea(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioConfianzaByUNEGocioEA(DataForFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioConfianzaByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioConfianzaByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioConfianzaByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioConfianzaByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioConfianzaByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioConfianzaByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioConfianzaByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioConfianzaByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioConfianzaByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioConfianzaBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }

        //Get Total de impulsoresclave para el logro de resultados



        //GetPromedios 66 Reactivos
        public JsonResult GetPromedios66ReactivosEnfoqueArea(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioOneTo66ByUNEGocioEA(DataForFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOneTo66ByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOneTo66ByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOneTo66ByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOneTo66ByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOneTo66ByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOneTo66ByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOneTo66ByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOneTo66ByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOneTo66ByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOneTo66BySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromedio 86 Reactivos
        public ActionResult GetPromedios86ReactivosEnfoqueArea(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedio86ReactivosByUNEGocioEA(DataForFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio86ReactivosByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio86ReactivosByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio86ReactivosByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio86ReactivosByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio86ReactivosByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio86ReactivosByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio86ReactivosByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio86ReactivosByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio86ReactivosByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio86ReactivosBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromedio PracticasCultureales
        public JsonResult GetPromediosPracticasCulturealesEnfoqueArea(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesByUNEGocioEA(DataForFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromedioReclutandoYDandoLaBienvenida
        public ActionResult GetPromediosReclutandoBienvenidaEnfoqueArea(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaByUNEGocioEA(DataForFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromediosInspirando
        public ActionResult GetPromediosInspirandoEnfoqueArea(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioInspirandoByUNEGocioEA(DataForFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioInspirandoByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioInspirandoByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioInspirandoByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioInspirandoByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioInspirandoByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioInspirandoByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioInspirandoByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioInspirandoByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioInspirandoByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioInspirandoBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromedioHablando
        public ActionResult GetPromediosHablandoEnfoqueArea(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioHablandoByUNEGocioEA(DataForFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHablandoByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHablandoByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHablandoByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHablandoByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHablandoByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHablandoByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHablandoByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHablandoByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHablandoByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHablandoBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromedioEscuchando
        public ActionResult GetPromediosEscuchandoEnfoqueArea(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioEscuchandoByUNEGocioEA(DataForFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEscuchandoByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEscuchandoByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEscuchandoByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEscuchandoByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEscuchandoByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEscuchandoByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEscuchandoByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEscuchandoByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEscuchandoByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEscuchandoBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromediosAgradeciendo
        public ActionResult GetPromediosAgradeciendoEnfoqueArea(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoByUNEGocioEA(DataForFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromedioDesarrollando
        public ActionResult GetPromediosDesarrollandoEnfoqueArea(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoByUNEGocioEA(DataForFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromedioCuidando
        public ActionResult GetPromediosCuidandoEnfoqueArea(ML.ReporteD4U model, int anioActual  = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioCuidandoByUNEGocioEA(DataForFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCuidandoByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCuidandoByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCuidandoByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCuidandoByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCuidandoByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCuidandoByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCuidandoByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCuidandoByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCuidandoByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCuidandoBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromedioPercepcionLugar
        public ActionResult GetPromediosPercepcionLugarEnfoqueArea(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarByUNEGocioEA(DataForFilter, anioActual, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromedioCooperando
        public ActionResult GetPromediosCooperandoEnfoqueArea(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioCooperandoByUNEGocioEA(DataForFilter, anioActual, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCooperandoByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCooperandoByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCooperandoByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCooperandoByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCooperandoByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCooperandoByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCooperandoByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCooperandoByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCooperandoByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCooperandoBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromedioAlineacionEstrategica
        public JsonResult GetPromediosAlineacionEstrategicaEnfoqueArea(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaByUNEGocioEA(DataForFilter, anioActual, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromedioProcesosOrganizacionales
        public JsonResult GetPromediosProcesosOrganizacionalesEnfoqueArea(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganByUNEGocioEA(DataForFilter, anioActual, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromedioPlanes de Carrera
        public ActionResult GetPromediosCarreraYPromocionPersEnfoqueArea(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionByUNEGocioEA(DataForFilter, anioActual, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromedioCapacitacionYDesarrollo
        public ActionResult GetPromediosCapacitacionYDesarrolloEnfoqueArea(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloByUNEGocioEA(DataForFilter, anioActual, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GEtPromedio ENVIROMENT
        public ActionResult GetPromediosEMPOWERMENTEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTByUNEGocioEA(DataForFilter, model.Anio, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTByGeneroEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTByFuncionEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTByCompanyEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTByAreaEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromedioEvalDesempeño
        public ActionResult GetPromediosEvalDesempeñoEnfoqueArea(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoByUNEGocioEA(DataForFilter, anioActual, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromedioIntegracion
        public ActionResult GetPromediosIntegracionEnfoqueArea(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioIntegracionByUNEGocioEA(DataForFilter, model.Anio, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioIntegracionByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioIntegracionByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioIntegracionByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioIntegracionByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioIntegracionByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioIntegracionByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioIntegracionByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioIntegracionByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioIntegracionByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioIntegracionBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromedioNivelCoolaboracion
        public JsonResult GetPromediosNivelCoolaboracionEnfoqueArea(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionByUNEGocioEA(DataForFilter, anioActual, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //getPromedioNivelCompromiso
        public JsonResult GetPromediosNivelCompromisoEnfoqueArea(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoByUNEGocioEA(DataForFilter, anioActual, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //GetPromedioFacoteSocial
        public ActionResult GetPromediosFactorSocialEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioFactorSocialByUNEGocioEA(DataForFilter, model.Anio, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorSocialByGeneroEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorSocialByFuncionEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorSocialByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorSocialByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorSocialByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorSocialByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorSocialByCompanyEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorSocialByAreaEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorSocialByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorSocialBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPromediosFactorPsicoEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoByUNEGocioEA(DataForFilter, model.Anio, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoByGeneroEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoByFuncionEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoByCompanyEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoByAreaEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //Factor fisico
        public ActionResult GetPromediosFactorFisicoEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoByUNEGocioEA(DataForFilter, model.Anio, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoByGeneroEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoByFuncionEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoByCompanyEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoByAreaEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //Getpromedio Bio
        public ActionResult GetPromediosBioEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioBioByUNEGocioEA(DataForFilter, model.Anio, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBioByGeneroEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBioByFuncionEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBioByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBioByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBioByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBioByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBioByCompanyEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBioByAreaEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBioByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBioBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //Getpromedio Bio
        public ActionResult GetPromediosPsicoEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioPsicoByUNEGocioEA(DataForFilter, model.Anio, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPsicoByGeneroEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPsicoByFuncionEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPsicoByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPsicoByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPsicoByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPsicoByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPsicoByCompanyEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPsicoByAreaEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPsicoByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPsicoBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPromediosSocialEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioSocialByUNEGocioEA(DataForFilter, model.Anio, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioSocialByGeneroEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioSocialByFuncionEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioSocialByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioSocialByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioSocialByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioSocialByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioSocialByCompanyEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioSocialByAreaEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioSocialByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioSocialBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPromediosComunicacionEnfoqueArea(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioComunicacionByUNEGocioEA(DataForFilter, anioActual, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioComunicacionByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioComunicacionByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioComunicacionByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioComunicacionByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioComunicacionByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioComunicacionByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioComunicacionByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioComunicacionByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioComunicacionByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioComunicacionBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPromediosEnpowerEnfoqueArea(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioEnpowerByUNEGocioEA(DataForFilter, anioActual, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPromediosCoordinacionEnfoqueArea(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioCoordinacionByUNEGocioEA(DataForFilter, anioActual, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoordinacionByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoordinacionByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoordinacionByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoordinacionByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoordinacionByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoordinacionByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoordinacionByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoordinacionByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoordinacionByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoordinacionBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPromediosVisionEstrateEnfoqueArea(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateByUNEGocioEA(DataForFilter, anioActual, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPromediosNivelDesempeñoEstrateEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoByUNEGocioEA(DataForFilter, model.Anio, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoByGeneroEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoByFuncionEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoByCompanyEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoByAreaEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, model.Anio, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }



        //Faltantes
        //ISO 9001:2015
        //Social psicologico y fisico
        public ActionResult GetPromediosISOEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioISOByUNEGocioEA(DataForFilter, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioISOByGeneroEA(DataForFilter, model.UnidadNegocioFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioISOByFuncionEA(DataForFilter, model.UnidadNegocioFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioISOByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioISOByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioISOByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioISOByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioISOByCompanyEA(DataForFilter, model.UnidadNegocioFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioISOByAreaEA(DataForFilter, model.UnidadNegocioFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioISOByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioISOBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPromediosISOEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioISOByUNEGocioEE(DataForFilter, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioISOByGeneroEE(DataForFilter, model.UnidadNegocioFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioISOByFuncionEE(DataForFilter, model.UnidadNegocioFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioISOByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioISOByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioISOByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioISOByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioISOByCompanyEE(DataForFilter, model.UnidadNegocioFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioISOByAreaEE(DataForFilter, model.UnidadNegocioFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioISOByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioISOBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }



        /*Metodos faltantes*/
        public JsonResult GetPromediosHabilidadesGerencialesEnfoqueEmpresa(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioHabilidadesGerencialesByUNEGocioEE(DataForFilter, anioActual, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHabilidadesGerencialesByGeneroEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHabilidadesGerencialesByFuncionEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHabilidadesGerencialesByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHabilidadesGerencialesByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHabilidadesGerencialesByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHabilidadesGerencialesRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHabilidadesGerencialesByCompanyEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHabilidadesGerencialesByAreaEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHabilidadesGerencialesByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHabilidadesGerencialesBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPromediosHabilidadesGerencialesEnfoqueArea(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioHabilidadesGerencialesByUNEGocioEA(DataForFilter, anioActual, model.IdBD);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHabilidadesGerencialesByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHabilidadesGerencialesByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHabilidadesGerencialesByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHabilidadesGerencialesByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHabilidadesGerencialesByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHabilidadesGerencialesRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHabilidadesGerencialesByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHabilidadesGerencialesByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHabilidadesGerencialesByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHabilidadesGerencialesBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetMejoresEE(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<ML.Queries> resultados = new List<ML.Queries>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetMejoresByUNEGocioEE(DataForFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMejoresByGeneroEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMejoresByFuncionEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMejoresByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMejoresByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMejoresByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMejoresRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMejoresByCompanyEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMejoresByAreaEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMejoresByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMejoresBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }

        //Crecimient
        public JsonResult GetMayorCrecimientoEE(ML.ReporteD4U model, string aEntidadId, HttpSessionStateBase Session, int idBaseDeDatos)
        {
            List<ML.Queries> resultados = new List<ML.Queries>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetMayorCrecimientoByUNEGocioEE(DataForFilter, Session["AnioActual_AnioHistorico"], aEntidadId, idBaseDeDatos);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "Gene=>")
                {
                }

                if (prefijo == "Func=>")
                {
                }
                if (prefijo == "CTra=>")
                {
                }
                if (prefijo == "GAca=>")
                {
                }
                if (prefijo == "RAnt=>")
                {
                }
                if (prefijo == "REda=>")
                {
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMayorCrecimientoByCompanyEE(DataForFilter, model.UnidadNegocioFilter, Session["AnioActual_AnioHistorico"], aEntidadId, idBaseDeDatos);
                    //var result = BL.ReporteD4U.GetMayorCrecimientoByCompanyEE(DataForFilter, model.UnidadNegocioFilter, Session["AnioActual_AnioHistorico"], aEntidadId);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMayorCrecimientoByAreaEE(DataForFilter, model.UnidadNegocioFilter, Session["AnioActual_AnioHistorico"], aEntidadId, idBaseDeDatos);
                    //var result = BL.ReporteD4U.GetMayorCrecimientoByAreaEE(DataForFilter, model.UnidadNegocioFilter, Session["AnioActual_AnioHistorico"], aEntidadId);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMayorCrecimientoByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, Session["AnioActual_AnioHistorico"], aEntidadId, idBaseDeDatos);
                    //var result = BL.ReporteD4U.GetMayorCrecimientoByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, Session["AnioActual_AnioHistorico"], aEntidadId);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMayorCrecimientoBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, Session["AnioActual_AnioHistorico"], aEntidadId, idBaseDeDatos);
                    //var result = BL.ReporteD4U.GetMayorCrecimientoBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, Session["AnioActual_AnioHistorico"], aEntidadId);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMayorCrecimientoEA(ML.ReporteD4U model, string aEntidadId, HttpSessionStateBase Session)
        {
            List<ML.Queries> resultados = new List<ML.Queries>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetMayorCrecimientoByUNEGocioEA(DataForFilter, Session["AnioActual_AnioHistorico"], aEntidadId);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                /*
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMayorCrecimientoByGeneroEA(DataForFilter, model.UnidadNegocioFilter, Session["AnioActual_AnioHistorico"], aEntidadId);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMayorCrecimientoByFuncionEA(DataForFilter, model.UnidadNegocioFilter, Session["AnioActual_AnioHistorico"], aEntidadId);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMayorCrecimientoByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, Session["AnioActual_AnioHistorico"], aEntidadId);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMayorCrecimientoByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, Session["AnioActual_AnioHistorico"], aEntidadId);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMayorCrecimientoByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, Session["AnioActual_AnioHistorico"], aEntidadId);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMayorCrecimientoRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, Session["AnioActual_AnioHistorico"], aEntidadId);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                */
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMayorCrecimientoByCompanyEA(DataForFilter, model.UnidadNegocioFilter, Session["AnioActual_AnioHistorico"], aEntidadId);
                    //var result = BL.ReporteD4U.GetMayorCrecimientoByCompanyEA(DataForFilter, model.UnidadNegocioFilter, Session["AnioActual_AnioHistorico"], aEntidadId);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMayorCrecimientoByAreaEA(DataForFilter, model.UnidadNegocioFilter, Session["AnioActual_AnioHistorico"], aEntidadId);
                    //var result = BL.ReporteD4U.GetMayorCrecimientoByAreaEA(DataForFilter, model.UnidadNegocioFilter, Session["AnioActual_AnioHistorico"], aEntidadId);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMayorCrecimientoByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, Session["AnioActual_AnioHistorico"], aEntidadId);
                    //var result = BL.ReporteD4U.GetMayorCrecimientoByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, Session["AnioActual_AnioHistorico"], aEntidadId);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMayorCrecimientoBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, Session["AnioActual_AnioHistorico"], aEntidadId);
                    //var result = BL.ReporteD4U.GetMayorCrecimientoBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, Session["AnioActual_AnioHistorico"], aEntidadId);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMayorCrecimientoEE(ML.ReporteD4U model, string aEntidadId, object AnioActual)
        {
            //Revisar que pasa con el año
            int Actual = Convert.ToInt32(AnioActual);
            int Anterior = Actual - 1;
            AnioActual = Actual + "_" + Anterior;
            List<ML.Queries> resultados = new List<ML.Queries>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetMayorCrecimientoByUNEGocioEE(DataForFilter, AnioActual, aEntidadId, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "Gene=>")
                {
                }

                if (prefijo == "Func=>")
                {
                }
                if (prefijo == "CTra=>")
                {
                }
                if (prefijo == "GAca=>")
                {
                }
                if (prefijo == "RAnt=>")
                {
                }
                if (prefijo == "REda=>")
                {
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMayorCrecimientoByCompanyEE(DataForFilter, model.UnidadNegocioFilter, AnioActual, aEntidadId, model.IdBD);
                    //var result = BL.ReporteD4U.GetMayorCrecimientoByCompanyEE(DataForFilter, model.UnidadNegocioFilter, Session["AnioActual_AnioHistorico"], aEntidadId);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMayorCrecimientoByAreaEE(DataForFilter, model.UnidadNegocioFilter, AnioActual, aEntidadId, model.IdBD);
                    //var result = BL.ReporteD4U.GetMayorCrecimientoByAreaEE(DataForFilter, model.UnidadNegocioFilter, Session["AnioActual_AnioHistorico"], aEntidadId);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMayorCrecimientoByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, AnioActual, aEntidadId, model.IdBD);
                    //var result = BL.ReporteD4U.GetMayorCrecimientoByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, Session["AnioActual_AnioHistorico"], aEntidadId);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMayorCrecimientoBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, AnioActual, aEntidadId, model.IdBD);
                    //var result = BL.ReporteD4U.GetMayorCrecimientoBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, Session["AnioActual_AnioHistorico"], aEntidadId);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMayorCrecimientoEA(ML.ReporteD4U model, string aEntidadId, object AnioActual)
        {
            int Actual = Convert.ToInt32(AnioActual);
            int Anterior = Actual - 1;
            AnioActual = Actual + "_" + Anterior;
            List<ML.Queries> resultados = new List<ML.Queries>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetMayorCrecimientoByUNEGocioEA(DataForFilter, AnioActual, aEntidadId);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                /*
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMayorCrecimientoByGeneroEA(DataForFilter, model.UnidadNegocioFilter, Session["AnioActual_AnioHistorico"], aEntidadId);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMayorCrecimientoByFuncionEA(DataForFilter, model.UnidadNegocioFilter, Session["AnioActual_AnioHistorico"], aEntidadId);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMayorCrecimientoByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, Session["AnioActual_AnioHistorico"], aEntidadId);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMayorCrecimientoByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, Session["AnioActual_AnioHistorico"], aEntidadId);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMayorCrecimientoByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, Session["AnioActual_AnioHistorico"], aEntidadId);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMayorCrecimientoRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, Session["AnioActual_AnioHistorico"], aEntidadId);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                */
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMayorCrecimientoByCompanyEA(DataForFilter, model.UnidadNegocioFilter, AnioActual, aEntidadId);
                    //var result = BL.ReporteD4U.GetMayorCrecimientoByCompanyEA(DataForFilter, model.UnidadNegocioFilter, Session["AnioActual_AnioHistorico"], aEntidadId);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMayorCrecimientoByAreaEA(DataForFilter, model.UnidadNegocioFilter, AnioActual, aEntidadId);
                    //var result = BL.ReporteD4U.GetMayorCrecimientoByAreaEA(DataForFilter, model.UnidadNegocioFilter, Session["AnioActual_AnioHistorico"], aEntidadId);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMayorCrecimientoByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, AnioActual, aEntidadId);
                    //var result = BL.ReporteD4U.GetMayorCrecimientoByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, Session["AnioActual_AnioHistorico"], aEntidadId);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMayorCrecimientoBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, AnioActual, aEntidadId);
                    //var result = BL.ReporteD4U.GetMayorCrecimientoBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, Session["AnioActual_AnioHistorico"], aEntidadId);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }
        //End crecimiento

        public JsonResult GetMejoresEA(ML.ReporteD4U model, int anioActual)
        {
            List<ML.Queries> resultados = new List<ML.Queries>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetMejoresByUNEGocioEA(DataForFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMejoresByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMejoresByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMejoresByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMejoresByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMejoresByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMejoresRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMejoresByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMejoresByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMejoresByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetMejoresBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPeoresEE(ML.ReporteD4U model, int anioActual = 0)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<ML.Queries> resultados = new List<ML.Queries>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPeoresByUNEGocioEE(DataForFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPeoresByGeneroEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPeoresByFuncionEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPeoresByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPeoresByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPeoresByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPeoresRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPeoresByCompanyEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPeoresByAreaEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPeoresByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPeoresBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPeoresEA(ML.ReporteD4U model, int anioActual)
        {
            if (anioActual == 0)
                anioActual = model.Anio;
            List<ML.Queries> resultados = new List<ML.Queries>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPeoresByUNEGocioEA(DataForFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPeoresByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPeoresByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPeoresByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPeoresByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPeoresByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPeoresRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPeoresByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPeoresByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPeoresByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPeoresBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getIndicadoresPermanencia(ML.ReporteD4U model, int anioActual)
        {
            List<ML.Queries> resultados = new List<ML.Queries>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetIndicadoresPermanenciaByUNEGocioEA(DataForFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetIndicadoresPermanenciaByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetIndicadoresPermanenciaByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetIndicadoresPermanenciaByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetIndicadoresPermanenciaByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetIndicadoresPermanenciaByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetIndicadoresPermanenciaByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetIndicadoresPermanenciaByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetIndicadoresPermanenciaByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetIndicadoresPermanenciaByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetIndicadoresPermanenciaBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getIndicadoresAbandono(ML.ReporteD4U model, int anioActual)
        {
            List<ML.Queries> resultados = new List<ML.Queries>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetIndicadoresAbandonoByUNEGocioEA(DataForFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetIndicadoresAbandonoByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetIndicadoresAbandonoByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetIndicadoresAbandonoByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetIndicadoresAbandonoByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetIndicadoresAbandonoByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetIndicadoresAbandonoByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetIndicadoresAbandonoByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetIndicadoresAbandonoByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetIndicadoresAbandonoByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetIndicadoresAbandonoBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }



        public JsonResult getComparativosPermanencia(ML.ReporteD4U model, int anioActual)
        {
            List<BL.ReporteD4U.Comparativo> resultados = new List<BL.ReporteD4U.Comparativo>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                if (DataForFilter == "TUR COM - GERENCIA GENERAL")
                {
                    Console.Write("");
                }
                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComparativoPermanenciaByUNEGocioEA(DataForFilter, anioActual, model.IdBD);
                    resultados.AddRange(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetComparativoPermanenciaByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.AddRange(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetComparativoPermanenciaByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.AddRange(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetComparativoPermanenciaByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.AddRange(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetComparativoPermanenciaByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.AddRange(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetComparativoPermanenciaByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.AddRange(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetComparativoPermanenciaByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.AddRange(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetComparativoPermanenciaByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.AddRange(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetComparativoPermanenciaByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.AddRange(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetComparativoPermanenciaByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.AddRange(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetComparativoPermanenciaBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.AddRange(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getComparativosAbandono(ML.ReporteD4U model, int anioActual)
        {
            List<BL.ReporteD4U.Comparativo> resultados = new List<BL.ReporteD4U.Comparativo>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter
                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComparativoAbandonoByUNEGocioEA(DataForFilter, anioActual, model.IdBD);
                    resultados.AddRange(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetComparativoAbandonoByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.AddRange(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetComparativoAbandonoByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.AddRange(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetComparativoAbandonoByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.AddRange(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetComparativoAbandonoByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.AddRange(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetComparativoAbandonoByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.AddRange(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetComparativoAbandonoByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.AddRange(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetComparativoAbandonoByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.AddRange(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetComparativoAbandonoByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.AddRange(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetComparativoAbandonoByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.AddRange(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetComparativoAbandonoBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.AddRange(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getComparativoEntidadesResultadoGeneralEE(ML.ReporteD4U model, int anioActual)
        {
            List<BL.ReporteD4U.comparativoGenerales> resultados = new List<BL.ReporteD4U.comparativoGenerales>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter
                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedio66ByUNEGocioEE(DataForFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio66ByGeneroEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio66ByFuncionEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio66ByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio66ByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio66ByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio66ByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio66ByCompanyEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio66ByAreaEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio66ByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio66BySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getComparativoEntidadesResultadoGeneralEA(ML.ReporteD4U model, int anioActual)
        {
            List<BL.ReporteD4U.comparativoGenerales> resultados = new List<BL.ReporteD4U.comparativoGenerales>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter
                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedio66ByUNEGocioEA(DataForFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio66ByGeneroEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio66ByFuncionEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio66ByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio66ByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio66ByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio66ByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio66ByCompanyEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio66ByAreaEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio66ByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio66BySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, anioActual, model.IdBD);
                    resultados.Add(result);
                }

            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }

        /*public JsonResult getReportePromedioGeneralPorAntiguedad_EE(ML.ReporteD4U model)
        {
            List<BL.ReporteD4U.comparativoGenerales> resultados = new List<BL.ReporteD4U.comparativoGenerales>();
            foreach (string item in model.ListFiltros)
            {
                var result = BL.ReporteD4U.getReportePromedioGeneralPorAntiguedad_EE(model);
                resultados.Add(result);
            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }*/
        //Vamos por el PartialView de Cicle2
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PwCicleEE(sendStringJson strjson)
            {
            var result = new ML.Result();
            //strjson.cadena = "[   {      'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUTOMOTRIZ</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='nublado'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/nublado.svg' src='/img/ReporteoClima/nublado.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>72.77%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:480</p></div></div></center></div></div></div>',      'id':'096174af-d18b-40f1-98f5-892e6d2560d1',      'children':[         {            'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - DIRECCION DE MARCA</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='sol'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/sol.svg' src='/img/ReporteoClima/sol.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>98.79%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:5</p></div></div></center></div></div></div>',            'id':'2c4d3c51-eba6-4ae7-860c-fd6e87e5f749',            'children':[               {                  'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - DIR - AEP</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='sol'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/sol.svg' src='/img/ReporteoClima/sol.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>100%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:1</p></div></div></center></div></div></div>',                  'id':'d63b1773-9db5-4c5c-a5cb-c471d5d6de75',                  'children':[                     {                        'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - DIR - AEP - DIRECCION DE MARCA</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='sol'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/sol.svg' src='/img/ReporteoClima/sol.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>100%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:1</p></div></div></center></div></div></div>',                        'id':'a6bd96e1-b6b2-4b8b-8275-6858a7b2ec7e',                        'children':[                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - DIR - AEP - DIR - -</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='sol'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/sol.svg' src='/img/ReporteoClima/sol.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>100%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:1</p></div></div></center></div></div></div>',                              'id':'2f04a73b-c8a3-47ea-b5f6-0f36ca5c9e6c',                              'data':{                                 '$color':'#f0af42'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - DIR - AEP - DIR - -',                              'Frecuencia':66,                              'Porcentaje':100,                              'HC':1                           }                        ],                        'data':{                           '$color':'#f0af42'                        },                        'tipoEntidad':4,                        'Entidad':'AUT - DIR - AEP - DIRECCION DE MARCA',                        'Frecuencia':66,                        'Porcentaje':100,                        'HC':1                     }                  ],                  'data':{                     '$color':'#f0af42'                  }               },               {                  'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - DIR - ASU</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='sol'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/sol.svg' src='/img/ReporteoClima/sol.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>96.97%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:1</p></div></div></center></div></div></div>',                  'id':'c59c50c5-cb81-4654-a336-955b7e05fe37',                  'children':[                     {                        'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - DIR - ASU - DIRECCION DE MARCA</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='sol'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/sol.svg' src='/img/ReporteoClima/sol.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>96.97%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:1</p></div></div></center></div></div></div>',                        'id':'0d0569e2-cbe3-4aa6-a22b-464d1c3b4b81',                        'children':[                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - DIR - ASU - DIR - -</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='sol'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/sol.svg' src='/img/ReporteoClima/sol.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>96.97%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:1</p></div></div></center></div></div></div>',                              'id':'abf9e1bf-595c-4a09-a1b6-d3e785890860',                              'data':{                                 '$color':'#f0af42'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - DIR - ASU - DIR - -',                              'Frecuencia':64,                              'Porcentaje':96.97,                              'HC':1                           }                        ],                        'data':{                           '$color':'#f0af42'                        },                        'tipoEntidad':4,                        'Entidad':'AUT - DIR - ASU - DIRECCION DE MARCA',                        'Frecuencia':64,                        'Porcentaje':96.97,                        'HC':1                     }                  ],                  'data':{                     '$color':'#f0af42'                  }               },               {                  'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - DIR - EAH</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='sol'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/sol.svg' src='/img/ReporteoClima/sol.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>100%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:1</p></div></div></center></div></div></div>',                  'id':'78e53c4a-5760-432f-8960-1cf4322a15ac',                  'children':[                     {                        'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - DIR - EAH - DIRECCION DE MARCA</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='sol'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/sol.svg' src='/img/ReporteoClima/sol.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>100%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:1</p></div></div></center></div></div></div>',                        'id':'2e02bfc2-2ed4-42bf-9e51-132eff056d67',                        'children':[                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - DIR - EAH - DIR - -</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='sol'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/sol.svg' src='/img/ReporteoClima/sol.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>100%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:1</p></div></div></center></div></div></div>',                              'id':'9c8bcfd8-e17d-4f39-af5e-5a782c762bea',                              'data':{                                 '$color':'#f0af42'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - DIR - EAH - DIR - -',                              'Frecuencia':66,                              'Porcentaje':100,                              'HC':1                           }                        ],                        'data':{                           '$color':'#f0af42'                        },                        'tipoEntidad':4,                        'Entidad':'AUT - DIR - EAH - DIRECCION DE MARCA',                        'Frecuencia':66,                        'Porcentaje':100,                        'HC':1                     }                  ],                  'data':{                     '$color':'#f0af42'                  }               },               {                  'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - DIR - EAN</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='sol'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/sol.svg' src='/img/ReporteoClima/sol.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>98.48%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:2</p></div></div></center></div></div></div>',                  'id':'2448ee1e-5d77-443d-b426-1ed2de8ec585',                  'children':[                     {                        'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - DIR - EAN - DIRECCION DE MARCA</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='sol'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/sol.svg' src='/img/ReporteoClima/sol.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>98.48%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:2</p></div></div></center></div></div></div>',                        'id':'02a2cff2-c4fb-4efc-b15f-351960147998',                        'children':[                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - DIR - EAN - DIR - -</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='sol'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/sol.svg' src='/img/ReporteoClima/sol.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>98.48%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:2</p></div></div></center></div></div></div>',                              'id':'3a0df825-78eb-4e38-aa9b-ea6a93cb67b9',                              'data':{                                 '$color':'#f0af42'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - DIR - EAN - DIR - -',                              'Frecuencia':130,                              'Porcentaje':98.48,                              'HC':2                           }                        ],                        'data':{                           '$color':'#f0af42'                        },                        'tipoEntidad':4,                        'Entidad':'AUT - DIR - EAN - DIRECCION DE MARCA',                        'Frecuencia':130,                        'Porcentaje':98.48,                        'HC':2                     }                  ],                  'data':{                     '$color':'#f0af42'                  }               }            ],            'data':{               '$color':'#f0af42'            }         },         {            'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - ELEGANTES</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='solnube'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/solnube.svg' src='/img/ReporteoClima/solnube.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>88.41%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:86</p></div></div></center></div></div></div>',            'id':'f647f54e-1819-436e-9a6a-6b8b134b9d0d',            'children':[               {                  'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - ELE - AEP</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='solnube'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/solnube.svg' src='/img/ReporteoClima/solnube.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>88.41%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:86</p></div></div></center></div></div></div>',                  'id':'7e1d6241-9e95-4d00-9229-0db89419ee95',                  'children':[                     {                        'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - ELE - AEP - ADMINISTRACION</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='nublado'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/nublado.svg' src='/img/ReporteoClima/nublado.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>76.19%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:7</p></div></div></center></div></div></div>',                        'id':'371bbdff-d136-4022-8f43-27ce000111c8',                        'children':[                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - ELE - AEP - ADM - -</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='nublado'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/nublado.svg' src='/img/ReporteoClima/nublado.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>76.19%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:7</p></div></div></center></div></div></div>',                              'id':'aca523cd-055e-4b2b-9683-a6d57d390191',                              'data':{                                 '$color':'#2f343a'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - ELE - AEP - ADM - -',                              'Frecuencia':352,                              'Porcentaje':76.19,                              'HC':7                           }                        ],                        'data':{                           '$color':'#2f343a'                        },                        'tipoEntidad':4,                        'Entidad':'AUT - ELE - AEP - ADMINISTRACION',                        'Frecuencia':352,                        'Porcentaje':76.19,                        'HC':7                     },                     {                        'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - ELE - AEP - COMERCIAL</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='sol'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/sol.svg' src='/img/ReporteoClima/sol.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>92.98%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:33</p></div></div></center></div></div></div>',                        'id':'44fbc556-5157-4cd1-8996-ea082731be31',                        'children':[                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - ELE - AEP - COM - NUEVOS</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='sol'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/sol.svg' src='/img/ReporteoClima/sol.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>93.51%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:14</p></div></div></center></div></div></div>',                              'id':'d4e9611b-9862-4902-a998-00a468898242',                              'data':{                                 '$color':'#f0af42'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - ELE - AEP - COM - NUEVOS',                              'Frecuencia':864,                              'Porcentaje':93.51,                              'HC':14                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - ELE - AEP - COM - SEMINUEVOS</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='solnube'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/solnube.svg' src='/img/ReporteoClima/solnube.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>88.64%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:6</p></div></div></center></div></div></div>',                              'id':'2cb860f9-dadf-49f8-b5f8-dc9bb9f0a0a7',                              'data':{                                 '$color':'#ea973e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - ELE - AEP - COM - SEMINUEVOS',                              'Frecuencia':351,                              'Porcentaje':88.64,                              'HC':6                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - ELE - AEP - COM - SOPORTE COMERCIAL</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='sol'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/sol.svg' src='/img/ReporteoClima/sol.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>94.41%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:13</p></div></div></center></div></div></div>',                              'id':'86a4f5df-538a-48f6-ab3d-dcd5cb0e0501',                              'data':{                                 '$color':'#f0af42'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - ELE - AEP - COM - SOPORTE COMERCIAL',                              'Frecuencia':810,                              'Porcentaje':94.41,                              'HC':13                           }                        ],                        'data':{                           '$color':'#f0af42'                        },                        'tipoEntidad':4,                        'Entidad':'AUT - ELE - AEP - COMERCIAL',                        'Frecuencia':2025,                        'Porcentaje':92.98,                        'HC':33                     },                     {                        'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - ELE - AEP - GERENCIA GENERAL</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='sol'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/sol.svg' src='/img/ReporteoClima/sol.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>92.05%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:8</p></div></div></center></div></div></div>',                        'id':'bb981324-fe3b-4145-af05-00fe0bc9eba0',                        'children':[                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - ELE - AEP - GER - -</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='sol'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/sol.svg' src='/img/ReporteoClima/sol.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>92.05%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:8</p></div></div></center></div></div></div>',                              'id':'17d3d6b6-e8a8-420c-b9e7-5db30d55312c',                              'data':{                                 '$color':'#f0af42'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - ELE - AEP - GER - -',                              'Frecuencia':486,                              'Porcentaje':92.05,                              'HC':8                           }                        ],                        'data':{                           '$color':'#f0af42'                        },                        'tipoEntidad':4,                        'Entidad':'AUT - ELE - AEP - GERENCIA GENERAL',                        'Frecuencia':486,                        'Porcentaje':92.05,                        'HC':8                     },                     {                        'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - ELE - AEP - MANTENIMIENTO Y LIMPIEZA</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='nublado'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/nublado.svg' src='/img/ReporteoClima/nublado.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>75.11%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:7</p></div></div></center></div></div></div>',                        'id':'205b670b-a14e-4fd6-bcd5-3296eeee9fc0',                        'children':[                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - ELE - AEP - MAN - -</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='nublado'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/nublado.svg' src='/img/ReporteoClima/nublado.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>75.11%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:7</p></div></div></center></div></div></div>',                              'id':'f7cf64ed-1547-4c8b-bfd7-ce23b3fa2379',                              'data':{                                 '$color':'#2f343a'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - ELE - AEP - MAN - -',                              'Frecuencia':347,                              'Porcentaje':75.11,                              'HC':7                           }                        ],                        'data':{                           '$color':'#2f343a'                        },                        'tipoEntidad':4,                        'Entidad':'AUT - ELE - AEP - MANTENIMIENTO Y LIMPIEZA',                        'Frecuencia':347,                        'Porcentaje':75.11,                        'HC':7                     },                     {                        'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - ELE - AEP - POSTVENTA</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='solnube'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/solnube.svg' src='/img/ReporteoClima/solnube.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>88.37%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:31</p></div></div></center></div></div></div>',                        'id':'4a551dfc-0740-4c34-b7c2-0f9049b753d5',                        'children':[                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - ELE - AEP - POS - HOJALATERIA Y PINTURA</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='sol'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/sol.svg' src='/img/ReporteoClima/sol.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>93.81%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:12</p></div></div></center></div></div></div>',                              'id':'5df5f8eb-2222-43c9-aff2-8ce943888207',                              'data':{                                 '$color':'#f0af42'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - ELE - AEP - POS - HOJALATERIA Y PINTURA',                              'Frecuencia':743,                              'Porcentaje':93.81,                              'HC':12                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - ELE - AEP - POS - REFACCIONES</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='solnube'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/solnube.svg' src='/img/ReporteoClima/solnube.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>83.03%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:5</p></div></div></center></div></div></div>',                              'id':'5fd27644-ddb5-452e-a328-a27667a26ccf',                              'data':{                                 '$color':'#ea973e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - ELE - AEP - POS - REFACCIONES',                              'Frecuencia':274,                              'Porcentaje':83.03,                              'HC':5                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - ELE - AEP - POS - SERVICIO</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='solnube'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/solnube.svg' src='/img/ReporteoClima/solnube.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>85.61%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:14</p></div></div></center></div></div></div>',                              'id':'850a407c-5c4f-4c30-95fc-16b9a8feda36',                              'data':{                                 '$color':'#ea973e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - ELE - AEP - POS - SERVICIO',                              'Frecuencia':791,                              'Porcentaje':85.61,                              'HC':14                           }                        ],                        'data':{                           '$color':'#ea973e'                        },                        'tipoEntidad':4,                        'Entidad':'AUT - ELE - AEP - POSTVENTA',                        'Frecuencia':1808,                        'Porcentaje':88.37,                        'HC':31                     }                  ],                  'data':{                     '$color':'#ea973e'                  }               }            ],            'data':{               '$color':'#ea973e'            }         },         {            'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXCELENCIA</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='nublado'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/nublado.svg' src='/img/ReporteoClima/nublado.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>72.11%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:274</p></div></div></center></div></div></div>',            'id':'3f1675c2-53cf-475e-a3b6-71450b58fc0f',            'children':[               {                  'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAC</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='nublado'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/nublado.svg' src='/img/ReporteoClima/nublado.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>72.07%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:55</p></div></div></center></div></div></div>',                  'id':'3f492e10-a73c-4d4e-b50a-d83c47d937e3',                  'children':[                     {                        'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAC - ADMINISTRACION</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='nublado'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/nublado.svg' src='/img/ReporteoClima/nublado.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>73.4%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:9</p></div></div></center></div></div></div>',                        'id':'79b6d855-6a00-485f-9275-6db0cca2493e',                        'children':[                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAC - ADM - ADMINITRACION</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='sol'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/sol.svg' src='/img/ReporteoClima/sol.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>96.46%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:3</p></div></div></center></div></div></div>',                              'id':'f5414c06-39b2-49ff-920a-8383e3a64eb6',                              'data':{                                 '$color':'#f0af42'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - EXC - EAC - ADM - ADMINITRACION',                              'Frecuencia':191,                              'Porcentaje':96.46,                              'HC':3                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAC - ADM - CONTABILIDAD</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>58.48%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:5</p></div></div></center></div></div></div>',                              'id':'24b26e93-15d3-44de-ab38-bbf150777c3d',                              'data':{                                 '$color':'#23282e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - EXC - EAC - ADM - CONTABILIDAD',                              'Frecuencia':193,                              'Porcentaje':58.48,                              'HC':5                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAC - ADM - CREDITO Y COBRANZA</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='nublado'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/nublado.svg' src='/img/ReporteoClima/nublado.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>78.79%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:1</p></div></div></center></div></div></div>',                              'id':'7e783e6c-91d3-4b7c-9246-fa652ba9b157',                              'data':{                                 '$color':'#2f343a'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - EXC - EAC - ADM - CREDITO Y COBRANZA',                              'Frecuencia':52,                              'Porcentaje':78.79,                              'HC':1                           }                        ],                        'data':{                           '$color':'#2f343a'                        },                        'tipoEntidad':4,                        'Entidad':'AUT - EXC - EAC - ADMINISTRACION',                        'Frecuencia':436,                        'Porcentaje':73.4,                        'HC':9                     },                     {                        'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAC - COMERCIAL</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>52.68%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:13</p></div></div></center></div></div></div>',                        'id':'d9eaa9bd-96c2-4b29-9272-2cc36424f7b7',                        'children':[                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAC - COM - NUEVOS</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>40.69%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:7</p></div></div></center></div></div></div>',                              'id':'2a5861ff-fd16-41a6-ab17-dbc897e99093',                              'data':{                                 '$color':'#23282e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - EXC - EAC - COM - NUEVOS',                              'Frecuencia':188,                              'Porcentaje':40.69,                              'HC':7                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAC - COM - SEMINUEVOS</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='solnube'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/solnube.svg' src='/img/ReporteoClima/solnube.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>86.36%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:2</p></div></div></center></div></div></div>',                              'id':'64d5201d-2b8b-44c4-9774-fa5e508cec62',                              'data':{                                 '$color':'#ea973e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - EXC - EAC - COM - SEMINUEVOS',                              'Frecuencia':114,                              'Porcentaje':86.36,                              'HC':2                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAC - COM - SOPORTE COMERCIAL</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>56.82%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:4</p></div></div></center></div></div></div>',                              'id':'1acb4047-4ace-4f1c-aafd-768b6822dc09',                              'data':{                                 '$color':'#23282e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - EXC - EAC - COM - SOPORTE COMERCIAL',                              'Frecuencia':150,                              'Porcentaje':56.82,                              'HC':4                           }                        ],                        'data':{                           '$color':'#23282e'                        },                        'tipoEntidad':4,                        'Entidad':'AUT - EXC - EAC - COMERCIAL',                        'Frecuencia':452,                        'Porcentaje':52.68,                        'HC':13                     },                     {                        'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAC - GERENCIA GENERAL</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='solnube'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/solnube.svg' src='/img/ReporteoClima/solnube.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>82.68%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:7</p></div></div></center></div></div></div>',                        'id':'5cd74378-a447-4e61-b7ff-7a20c2095dee',                        'children':[                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAC - GER - -</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='solnube'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/solnube.svg' src='/img/ReporteoClima/solnube.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>82.68%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:7</p></div></div></center></div></div></div>',                              'id':'bb238188-29ce-4210-921e-8248004085d2',                              'data':{                                 '$color':'#ea973e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - EXC - EAC - GER - -',                              'Frecuencia':382,                              'Porcentaje':82.68,                              'HC':7                           }                        ],                        'data':{                           '$color':'#ea973e'                        },                        'tipoEntidad':4,                        'Entidad':'AUT - EXC - EAC - GERENCIA GENERAL',                        'Frecuencia':382,                        'Porcentaje':82.68,                        'HC':7                     },                     {                        'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAC - POSTVENTA</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='nublado'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/nublado.svg' src='/img/ReporteoClima/nublado.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>78.44%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:26</p></div></div></center></div></div></div>',                        'id':'2972baf4-f5fe-4e53-a0af-208db570cf55',                        'children':[                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAC - POS - HOJALATERIA Y PINTURA</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>64.48%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:9</p></div></div></center></div></div></div>',                              'id':'1a0bfd3b-5b77-4b96-829f-cfad238b4ffa',                              'data':{                                 '$color':'#23282e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - EXC - EAC - POS - HOJALATERIA Y PINTURA',                              'Frecuencia':383,                              'Porcentaje':64.48,                              'HC':9                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAC - POS - REFACCIONES</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='solnube'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/solnube.svg' src='/img/ReporteoClima/solnube.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>88.1%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:7</p></div></div></center></div></div></div>',                              'id':'d715cd4a-2c05-49f7-a0aa-6dcf099e56c0',                              'data':{                                 '$color':'#ea973e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - EXC - EAC - POS - REFACCIONES',                              'Frecuencia':407,                              'Porcentaje':88.1,                              'HC':7                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAC - POS - SERVICIO</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='solnube'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/solnube.svg' src='/img/ReporteoClima/solnube.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>84.24%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:10</p></div></div></center></div></div></div>',                              'id':'b144d2d8-05ca-45e3-9789-74f8eb87e382',                              'data':{                                 '$color':'#ea973e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - EXC - EAC - POS - SERVICIO',                              'Frecuencia':556,                              'Porcentaje':84.24,                              'HC':10                           }                        ],                        'data':{                           '$color':'#2f343a'                        },                        'tipoEntidad':4,                        'Entidad':'AUT - EXC - EAC - POSTVENTA',                        'Frecuencia':1346,                        'Porcentaje':78.44,                        'HC':26                     }                  ],                  'data':{                     '$color':'#2f343a'                  }               },               {                  'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAH</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>63.78%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:87</p></div></div></center></div></div></div>',                  'id':'6c4cd523-78cd-469e-bb0b-3ed9d8f40c49',                  'children':[                     {                        'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAH - ADMINISTRACION</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>52.58%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:10</p></div></div></center></div></div></div>',                        'id':'2d3c0bef-39a9-41f2-b560-8f70e33929eb',                        'children':[                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAH - ADM - ADMINITRACION</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>65.53%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:4</p></div></div></center></div></div></div>',                              'id':'fca39d36-8357-4848-8f47-67c1a3c11802',                              'data':{                                 '$color':'#23282e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - EXC - EAH - ADM - ADMINITRACION',                              'Frecuencia':173,                              'Porcentaje':65.53,                              'HC':4                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAH - ADM - CONTABILIDAD</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>46.97%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:4</p></div></div></center></div></div></div>',                              'id':'297ce788-91c3-4e05-bda2-50c4ff2c3dde',                              'data':{                                 '$color':'#23282e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - EXC - EAH - ADM - CONTABILIDAD',                              'Frecuencia':124,                              'Porcentaje':46.97,                              'HC':4                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAH - ADM - CREDITO Y COBRANZA</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>37.88%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:2</p></div></div></center></div></div></div>',                              'id':'2c888b44-5c0b-4aaa-b79d-e920e0689b23',                              'data':{                                 '$color':'#23282e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - EXC - EAH - ADM - CREDITO Y COBRANZA',                              'Frecuencia':50,                              'Porcentaje':37.88,                              'HC':2                           }                        ],                        'data':{                           '$color':'#23282e'                        },                        'tipoEntidad':4,                        'Entidad':'AUT - EXC - EAH - ADMINISTRACION',                        'Frecuencia':347,                        'Porcentaje':52.58,                        'HC':10                     },                     {                        'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAH - COMERCIAL</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>69.53%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:37</p></div></div></center></div></div></div>',                        'id':'f8bbb984-ade3-485f-bca8-3a5750dd2904',                        'children':[                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAH - COM - NUEVOS</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>63.74%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:15</p></div></div></center></div></div></div>',                              'id':'ad6f1d80-a231-4812-b8ce-5df2b5f6d1c2',                              'data':{                                 '$color':'#23282e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - EXC - EAH - COM - NUEVOS',                              'Frecuencia':631,                              'Porcentaje':63.74,                              'HC':15                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAH - COM - PUNTO DE VENTA</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='nublado'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/nublado.svg' src='/img/ReporteoClima/nublado.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>70.94%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:11</p></div></div></center></div></div></div>',                              'id':'a5b39387-6860-47f5-8387-abb89048f0b6',                              'data':{                                 '$color':'#2f343a'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - EXC - EAH - COM - PUNTO DE VENTA',                              'Frecuencia':515,                              'Porcentaje':70.94,                              'HC':11                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAH - COM - SOPORTE COMERCIAL</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='nublado'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/nublado.svg' src='/img/ReporteoClima/nublado.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>76.03%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:11</p></div></div></center></div></div></div>',                              'id':'91423053-bff8-4abf-b41a-e64bc7645378',                              'data':{                                 '$color':'#2f343a'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - EXC - EAH - COM - SOPORTE COMERCIAL',                              'Frecuencia':552,                              'Porcentaje':76.03,                              'HC':11                           }                        ],                        'data':{                           '$color':'#23282e'                        },                        'tipoEntidad':4,                        'Entidad':'AUT - EXC - EAH - COMERCIAL',                        'Frecuencia':1698,                        'Porcentaje':69.53,                        'HC':37                     },                     {                        'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAH - GERENCIA GENERAL</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='nublado'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/nublado.svg' src='/img/ReporteoClima/nublado.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>75.95%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:8</p></div></div></center></div></div></div>',                        'id':'e158961c-3688-4d9e-9f61-975ca1282bb1',                        'children':[                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAH - GER - -</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='nublado'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/nublado.svg' src='/img/ReporteoClima/nublado.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>75.95%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:8</p></div></div></center></div></div></div>',                              'id':'8f44c253-8d2f-4f2b-a9ba-d084f32d0229',                              'data':{                                 '$color':'#2f343a'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - EXC - EAH - GER - -',                              'Frecuencia':401,                              'Porcentaje':75.95,                              'HC':8                           }                        ],                        'data':{                           '$color':'#2f343a'                        },                        'tipoEntidad':4,                        'Entidad':'AUT - EXC - EAH - GERENCIA GENERAL',                        'Frecuencia':401,                        'Porcentaje':75.95,                        'HC':8                     },                     {                        'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAH - MANTENIMIENTO Y LIMPIEZA</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>53.64%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:5</p></div></div></center></div></div></div>',                        'id':'76f275ad-02ba-471d-801e-44733284c4d8',                        'children':[                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAH - MAN - -</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>53.64%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:5</p></div></div></center></div></div></div>',                              'id':'b54b9f6c-560e-4fe1-89f4-968c0d9118df',                              'data':{                                 '$color':'#23282e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - EXC - EAH - MAN - -',                              'Frecuencia':177,                              'Porcentaje':53.64,                              'HC':5                           }                        ],                        'data':{                           '$color':'#23282e'                        },                        'tipoEntidad':4,                        'Entidad':'AUT - EXC - EAH - MANTENIMIENTO Y LIMPIEZA',                        'Frecuencia':177,                        'Porcentaje':53.64,                        'HC':5                     },                     {                        'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAH - POSTVENTA</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>58.31%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:27</p></div></div></center></div></div></div>',                        'id':'a567597f-f6c5-48b4-9559-58243d89903a',                        'children':[                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAH - POS - HOJALATERIA Y PINTURA</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='nublado'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/nublado.svg' src='/img/ReporteoClima/nublado.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>73.23%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:6</p></div></div></center></div></div></div>',                              'id':'d35ee0ef-40cc-4286-8a28-6b08f97e039a',                              'data':{                                 '$color':'#2f343a'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - EXC - EAH - POS - HOJALATERIA Y PINTURA',                              'Frecuencia':290,                              'Porcentaje':73.23,                              'HC':6                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAH - POS - REFACCIONES</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>57.95%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:8</p></div></div></center></div></div></div>',                              'id':'3807622c-cfb5-4266-bcb7-66e2ee028391',                              'data':{                                 '$color':'#23282e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - EXC - EAH - POS - REFACCIONES',                              'Frecuencia':306,                              'Porcentaje':57.95,                              'HC':8                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAH - POS - SERVICIO</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>51.63%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:13</p></div></div></center></div></div></div>',                              'id':'926b4876-145c-480f-8574-a926a85987b2',                              'data':{                                 '$color':'#23282e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - EXC - EAH - POS - SERVICIO',                              'Frecuencia':443,                              'Porcentaje':51.63,                              'HC':13                           }                        ],                        'data':{                           '$color':'#23282e'                        },                        'tipoEntidad':4,                        'Entidad':'AUT - EXC - EAH - POSTVENTA',                        'Frecuencia':1039,                        'Porcentaje':58.31,                        'HC':27                     }                  ],                  'data':{                     '$color':'#23282e'                  }               },               {                  'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAN</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='nublado'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/nublado.svg' src='/img/ReporteoClima/nublado.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>77.62%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:132</p></div></div></center></div></div></div>',                  'id':'18e9d2b1-d2af-472c-befc-b8d2f94146b1',                  'children':[                     {                        'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAN - ADMINISTRACION</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>67.91%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:17</p></div></div></center></div></div></div>',                        'id':'1b372f74-db66-47c6-b54c-39922e029d6c',                        'children':[                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAN - ADM - CONTABILIDAD</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='nublado'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/nublado.svg' src='/img/ReporteoClima/nublado.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>73.74%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:12</p></div></div></center></div></div></div>',                              'id':'9e30df1e-76bb-469a-8901-e1c87f9aad6f',                              'data':{                                 '$color':'#2f343a'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - EXC - EAN - ADM - CONTABILIDAD',                              'Frecuencia':584,                              'Porcentaje':73.74,                              'HC':12                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAN - ADM - CREDITO Y COBRANZA</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>64.77%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:4</p></div></div></center></div></div></div>',                              'id':'ff51ab13-3ce0-4245-b303-122d818d74fa',                              'data':{                                 '$color':'#23282e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - EXC - EAN - ADM - CREDITO Y COBRANZA',                              'Frecuencia':171,                              'Porcentaje':64.77,                              'HC':4                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAN - ADM - SISTEMAS</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>10.61%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:1</p></div></div></center></div></div></div>',                              'id':'cba43918-c99a-461b-9578-61936ff4805f',                              'data':{                                 '$color':'#23282e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - EXC - EAN - ADM - SISTEMAS',                              'Frecuencia':7,                              'Porcentaje':10.61,                              'HC':1                           }                        ],                        'data':{                           '$color':'#23282e'                        },                        'tipoEntidad':4,                        'Entidad':'AUT - EXC - EAN - ADMINISTRACION',                        'Frecuencia':762,                        'Porcentaje':67.91,                        'HC':17                     },                     {                        'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAN - COMERCIAL</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='solnube'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/solnube.svg' src='/img/ReporteoClima/solnube.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>83.64%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:50</p></div></div></center></div></div></div>',                        'id':'b95b4929-aaf0-4d27-bb7c-55fbd54fe0b7',                        'children':[                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAN - COM - BDC</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='sol'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/sol.svg' src='/img/ReporteoClima/sol.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>92.42%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:4</p></div></div></center></div></div></div>',                              'id':'c4e61874-67f2-449a-8158-997e59f8bcd3',                              'data':{                                 '$color':'#f0af42'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - EXC - EAN - COM - BDC',                              'Frecuencia':244,                              'Porcentaje':92.42,                              'HC':4                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAN - COM - GERENCIA COMERCIAL</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='nublado'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/nublado.svg' src='/img/ReporteoClima/nublado.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>78.57%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:14</p></div></div></center></div></div></div>',                              'id':'6c0400dd-c105-49c7-b5c0-0bf930b41796',                              'data':{                                 '$color':'#2f343a'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - EXC - EAN - COM - GERENCIA COMERCIAL',                              'Frecuencia':726,                              'Porcentaje':78.57,                              'HC':14                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAN - COM - INGENIO AUTOMOTRIZ</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='sol'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/sol.svg' src='/img/ReporteoClima/sol.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>96.97%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:4</p></div></div></center></div></div></div>',                              'id':'548586be-4610-4c7f-a00e-bdfb997c5ec0',                              'data':{                                 '$color':'#f0af42'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - EXC - EAN - COM - INGENIO AUTOMOTRIZ',                              'Frecuencia':256,                              'Porcentaje':96.97,                              'HC':4                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAN - COM - NUEVOS</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='solnube'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/solnube.svg' src='/img/ReporteoClima/solnube.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>84.24%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:15</p></div></div></center></div></div></div>',                              'id':'cf0028d3-73fe-4fa6-bd57-a8d991538965',                              'data':{                                 '$color':'#ea973e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - EXC - EAN - COM - NUEVOS',                              'Frecuencia':834,                              'Porcentaje':84.24,                              'HC':15                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAN - COM - PUNTO DE VENTA</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='solnube'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/solnube.svg' src='/img/ReporteoClima/solnube.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>83.79%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:10</p></div></div></center></div></div></div>',                              'id':'4a9a9f12-214d-4a61-b099-0a95fd1fb517',                              'data':{                                 '$color':'#ea973e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - EXC - EAN - COM - PUNTO DE VENTA',                              'Frecuencia':553,                              'Porcentaje':83.79,                              'HC':10                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAN - COM - SEMINUEVOS</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='nublado'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/nublado.svg' src='/img/ReporteoClima/nublado.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>74.24%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:3</p></div></div></center></div></div></div>',                              'id':'a0d6be9c-4c8d-4ee8-a00c-1c30927be8ae',                              'data':{                                 '$color':'#2f343a'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - EXC - EAN - COM - SEMINUEVOS',                              'Frecuencia':147,                              'Porcentaje':74.24,                              'HC':3                           }                        ],                        'data':{                           '$color':'#ea973e'                        },                        'tipoEntidad':4,                        'Entidad':'AUT - EXC - EAN - COMERCIAL',                        'Frecuencia':2760,                        'Porcentaje':83.64,                        'HC':50                     },                     {                        'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAN - GERENCIA GENERAL</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='solnube'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/solnube.svg' src='/img/ReporteoClima/solnube.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>82.2%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:8</p></div></div></center></div></div></div>',                        'id':'bd38a296-5ef1-4278-9e35-86d39f8d165a',                        'children':[                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAN - GER - -</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='solnube'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/solnube.svg' src='/img/ReporteoClima/solnube.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>82.2%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:8</p></div></div></center></div></div></div>',                              'id':'b695db18-f1bb-4564-a06e-64cf49b5bb16',                              'data':{                                 '$color':'#ea973e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - EXC - EAN - GER - -',                              'Frecuencia':434,                              'Porcentaje':82.2,                              'HC':8                           }                        ],                        'data':{                           '$color':'#ea973e'                        },                        'tipoEntidad':4,                        'Entidad':'AUT - EXC - EAN - GERENCIA GENERAL',                        'Frecuencia':434,                        'Porcentaje':82.2,                        'HC':8                     },                     {                        'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAN - MANTENIMIENTO Y LIMPIEZA</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='sol'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/sol.svg' src='/img/ReporteoClima/sol.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>98.92%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:7</p></div></div></center></div></div></div>',                        'id':'66d9f615-4688-4f05-9091-270eb408cac1',                        'children':[                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAN - MAN - -</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='sol'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/sol.svg' src='/img/ReporteoClima/sol.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>98.92%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:7</p></div></div></center></div></div></div>',                              'id':'f0bb4f91-9988-44b0-b75d-ce1cd27e6dad',                              'data':{                                 '$color':'#f0af42'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - EXC - EAN - MAN - -',                              'Frecuencia':457,                              'Porcentaje':98.92,                              'HC':7                           }                        ],                        'data':{                           '$color':'#f0af42'                        },                        'tipoEntidad':4,                        'Entidad':'AUT - EXC - EAN - MANTENIMIENTO Y LIMPIEZA',                        'Frecuencia':457,                        'Porcentaje':98.92,                        'HC':7                     },                     {                        'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAN - POSTVENTA</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='nublado'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/nublado.svg' src='/img/ReporteoClima/nublado.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>71.18%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:50</p></div></div></center></div></div></div>',                        'id':'31cef025-696c-4a01-8332-c72ac0900fe4',                        'children':[                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAN - POS - GERENCIA DE SERVICIO</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='nublado'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/nublado.svg' src='/img/ReporteoClima/nublado.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>70.29%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:18</p></div></div></center></div></div></div>',                              'id':'bccd775e-b41a-4df4-a628-6f0e0f0230db',                              'data':{                                 '$color':'#2f343a'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - EXC - EAN - POS - GERENCIA DE SERVICIO',                              'Frecuencia':835,                              'Porcentaje':70.29,                              'HC':18                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAN - POS - HOJALATERIA Y PINTURA</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='nublado'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/nublado.svg' src='/img/ReporteoClima/nublado.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>70.2%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:15</p></div></div></center></div></div></div>',                              'id':'a0a4cb9d-2447-4b63-827f-2e3a93476a25',                              'data':{                                 '$color':'#2f343a'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - EXC - EAN - POS - HOJALATERIA Y PINTURA',                              'Frecuencia':695,                              'Porcentaje':70.2,                              'HC':15                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAN - POS - JEFATURA DE TALLER</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='nublado'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/nublado.svg' src='/img/ReporteoClima/nublado.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>71.89%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:9</p></div></div></center></div></div></div>',                              'id':'3cef134c-0fff-4729-8bda-51a50d2fa189',                              'data':{                                 '$color':'#2f343a'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - EXC - EAN - POS - JEFATURA DE TALLER',                              'Frecuencia':427,                              'Porcentaje':71.89,                              'HC':9                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - EXC - EAN - POS - REFACCIONES</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='nublado'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/nublado.svg' src='/img/ReporteoClima/nublado.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>74.24%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:8</p></div></div></center></div></div></div>',                              'id':'eb3660fc-1427-4f0a-be90-a69782a16b4a',                              'data':{                                 '$color':'#2f343a'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - EXC - EAN - POS - REFACCIONES',                              'Frecuencia':392,                              'Porcentaje':74.24,                              'HC':8                           }                        ],                        'data':{                           '$color':'#2f343a'                        },                        'tipoEntidad':4,                        'Entidad':'AUT - EXC - EAN - POSTVENTA',                        'Frecuencia':2349,                        'Porcentaje':71.18,                        'HC':50                     }                  ],                  'data':{                     '$color':'#2f343a'                  }               }            ],            'data':{               '$color':'#2f343a'            }         },         {            'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - HERENCIA</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>38.74%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:35</p></div></div></center></div></div></div>',            'id':'88749789-9cec-45b5-b44f-bdde93816f42',            'children':[               {                  'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - HER - HAU</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>38.74%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:35</p></div></div></center></div></div></div>',                  'id':'40c6f5b2-58b5-4fdf-ae7a-ff916e2af41e',                  'children':[                     {                        'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - HER - HAU - ADMINISTRACION</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>34.55%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:5</p></div></div></center></div></div></div>',                        'id':'e0aa71a3-08f9-4dad-89e9-e5a34583a449',                        'children':[                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - HER - HAU - ADM - -</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>34.55%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:5</p></div></div></center></div></div></div>',                              'id':'6fde382b-1253-4c41-b5d8-6d9950b0fa91',                              'data':{                                 '$color':'#23282e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - HER - HAU - ADM - -',                              'Frecuencia':114,                              'Porcentaje':34.55,                              'HC':5                           }                        ],                        'data':{                           '$color':'#23282e'                        },                        'tipoEntidad':4,                        'Entidad':'AUT - HER - HAU - ADMINISTRACION',                        'Frecuencia':114,                        'Porcentaje':34.55,                        'HC':5                     },                     {                        'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - HER - HAU - COMERCIAL</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>37.63%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:12</p></div></div></center></div></div></div>',                        'id':'b64ec7a6-44ef-4ec6-adb1-e52b9d9b8e09',                        'children':[                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - HER - HAU - COM - LOGISTICA Y OPERACIÓN</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='nublado'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/nublado.svg' src='/img/ReporteoClima/nublado.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>76.52%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:2</p></div></div></center></div></div></div>',                              'id':'e71c6d75-947d-4211-8cce-3a1c937f6454',                              'data':{                                 '$color':'#2f343a'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - HER - HAU - COM - LOGISTICA Y OPERACIÓN',                              'Frecuencia':101,                              'Porcentaje':76.52,                              'HC':2                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - HER - HAU - COM - NUEVOS</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>29.85%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:10</p></div></div></center></div></div></div>',                              'id':'73deb125-bff6-4782-958c-007a28d131fb',                              'data':{                                 '$color':'#23282e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - HER - HAU - COM - NUEVOS',                              'Frecuencia':197,                              'Porcentaje':29.85,                              'HC':10                           }                        ],                        'data':{                           '$color':'#23282e'                        },                        'tipoEntidad':4,                        'Entidad':'AUT - HER - HAU - COMERCIAL',                        'Frecuencia':298,                        'Porcentaje':37.63,                        'HC':12                     },                     {                        'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - HER - HAU - GERENCIA GENERAL</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>58.33%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:4</p></div></div></center></div></div></div>',                        'id':'921e0f48-922b-4bb7-bdfd-6341009e152e',                        'children':[                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - HER - HAU - GER - -</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>58.33%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:4</p></div></div></center></div></div></div>',                              'id':'eabfc722-bc83-4597-9bc1-4d11715bce3f',                              'data':{                                 '$color':'#23282e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - HER - HAU - GER - -',                              'Frecuencia':154,                              'Porcentaje':58.33,                              'HC':4                           }                        ],                        'data':{                           '$color':'#23282e'                        },                        'tipoEntidad':4,                        'Entidad':'AUT - HER - HAU - GERENCIA GENERAL',                        'Frecuencia':154,                        'Porcentaje':58.33,                        'HC':4                     },                     {                        'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - HER - HAU - MANTENIMIENTO Y LIMPIEZA</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>50.51%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:3</p></div></div></center></div></div></div>',                        'id':'d5ba1d8e-f6d6-48d2-9eb2-3f7f621b9ad7',                        'children':[                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - HER - HAU - MAN - -</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>50.51%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:3</p></div></div></center></div></div></div>',                              'id':'37b314f4-94a6-4951-a121-931118c87c91',                              'data':{                                 '$color':'#23282e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - HER - HAU - MAN - -',                              'Frecuencia':100,                              'Porcentaje':50.51,                              'HC':3                           }                        ],                        'data':{                           '$color':'#23282e'                        },                        'tipoEntidad':4,                        'Entidad':'AUT - HER - HAU - MANTENIMIENTO Y LIMPIEZA',                        'Frecuencia':100,                        'Porcentaje':50.51,                        'HC':3                     },                     {                        'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - HER - HAU - POSTVENTA</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>31.54%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:11</p></div></div></center></div></div></div>',                        'id':'1b316603-0f4e-4343-beaf-6b1962a1923a',                        'children':[                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - HER - HAU - POS - HOJALATERIA Y PINTURA</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='solnube'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/solnube.svg' src='/img/ReporteoClima/solnube.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>85.61%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:2</p></div></div></center></div></div></div>',                              'id':'2abec650-afd8-4750-af67-d971ec5959c6',                              'data':{                                 '$color':'#ea973e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - HER - HAU - POS - HOJALATERIA Y PINTURA',                              'Frecuencia':113,                              'Porcentaje':85.61,                              'HC':2                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - HER - HAU - POS - REFACCIONES</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>16.67%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:2</p></div></div></center></div></div></div>',                              'id':'1a505f7d-7901-4f0a-a901-51447d95ab18',                              'data':{                                 '$color':'#23282e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - HER - HAU - POS - REFACCIONES',                              'Frecuencia':22,                              'Porcentaje':16.67,                              'HC':2                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - HER - HAU - POS - SERVICIO </center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>20.35%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:7</p></div></div></center></div></div></div>',                              'id':'26a95c24-5b01-4ef4-869f-379c2e0eb087',                              'data':{                                 '$color':'#23282e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - HER - HAU - POS - SERVICIO ',                              'Frecuencia':94,                              'Porcentaje':20.35,                              'HC':7                           }                        ],                        'data':{                           '$color':'#23282e'                        },                        'tipoEntidad':4,                        'Entidad':'AUT - HER - HAU - POSTVENTA',                        'Frecuencia':229,                        'Porcentaje':31.54,                        'HC':11                     }                  ],                  'data':{                     '$color':'#23282e'                  }               }            ],            'data':{               '$color':'#23282e'            }         },         {            'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - SAMURAI</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='nublado'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/nublado.svg' src='/img/ReporteoClima/nublado.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>71.46%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:80</p></div></div></center></div></div></div>',            'id':'b3bebfb9-18fc-4398-8f99-20d45cf81449',            'children':[               {                  'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - SAM - ASP</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='nublado'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/nublado.svg' src='/img/ReporteoClima/nublado.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>77.15%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:24</p></div></div></center></div></div></div>',                  'id':'31194dc3-601f-4f9e-8230-003985366c89',                  'children':[                     {                        'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - SAM - ASP - ADMINISTRACION</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>65.91%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:6</p></div></div></center></div></div></div>',                        'id':'30565148-18c9-461b-adf6-3e60eb161c86',                        'children':[                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - SAM - ASP - ADM - ADMINITRACION</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>69.7%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:1</p></div></div></center></div></div></div>',                              'id':'e8ef0113-2afe-4e8d-b6be-2047022ef323',                              'data':{                                 '$color':'#23282e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - SAM - ASP - ADM - ADMINITRACION',                              'Frecuencia':46,                              'Porcentaje':69.7,                              'HC':1                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - SAM - ASP - ADM - CONTABILIDAD</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>64.65%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:3</p></div></div></center></div></div></div>',                              'id':'4a368d9c-ffdd-44f5-9b4c-8bf8925801cb',                              'data':{                                 '$color':'#23282e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - SAM - ASP - ADM - CONTABILIDAD',                              'Frecuencia':128,                              'Porcentaje':64.65,                              'HC':3                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - SAM - ASP - ADM - INTENDENCIA</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>62.12%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:1</p></div></div></center></div></div></div>',                              'id':'24a8b6c5-9bd5-4269-a633-1e98b0e30960',                              'data':{                                 '$color':'#23282e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - SAM - ASP - ADM - INTENDENCIA',                              'Frecuencia':41,                              'Porcentaje':62.12,                              'HC':1                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - SAM - ASP - ADM - MANTENIMIENTO</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>69.7%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:1</p></div></div></center></div></div></div>',                              'id':'cba3c3ec-8e37-4304-9c35-1ab055539070',                              'data':{                                 '$color':'#23282e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - SAM - ASP - ADM - MANTENIMIENTO',                              'Frecuencia':46,                              'Porcentaje':69.7,                              'HC':1                           }                        ],                        'data':{                           '$color':'#23282e'                        },                        'tipoEntidad':4,                        'Entidad':'AUT - SAM - ASP - ADMINISTRACION',                        'Frecuencia':261,                        'Porcentaje':65.91,                        'HC':6                     },                     {                        'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - SAM - ASP - COMERCIAL</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='solnube'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/solnube.svg' src='/img/ReporteoClima/solnube.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>82.58%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:12</p></div></div></center></div></div></div>',                        'id':'50bd212e-6310-4995-8b04-809683ee84e6',                        'children':[                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - SAM - ASP - COM - NUEVOS</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='nublado'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/nublado.svg' src='/img/ReporteoClima/nublado.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>76.84%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:7</p></div></div></center></div></div></div>',                              'id':'f64ae92b-e959-4321-a538-9490cd8049c0',                              'data':{                                 '$color':'#2f343a'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - SAM - ASP - COM - NUEVOS',                              'Frecuencia':355,                              'Porcentaje':76.84,                              'HC':7                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - SAM - ASP - COM - SEMINUEVOS</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='sol'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/sol.svg' src='/img/ReporteoClima/sol.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>93.94%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:1</p></div></div></center></div></div></div>',                              'id':'d1abf9ea-ab94-406c-9b20-dd1d4fa0a248',                              'data':{                                 '$color':'#f0af42'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - SAM - ASP - COM - SEMINUEVOS',                              'Frecuencia':62,                              'Porcentaje':93.94,                              'HC':1                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - SAM - ASP - COM - SOPORTE COMERCIAL</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='solnube'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/solnube.svg' src='/img/ReporteoClima/solnube.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>89.77%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:4</p></div></div></center></div></div></div>',                              'id':'4b27fd90-b4d0-4a7b-9279-86ae44f5012a',                              'data':{                                 '$color':'#ea973e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - SAM - ASP - COM - SOPORTE COMERCIAL',                              'Frecuencia':237,                              'Porcentaje':89.77,                              'HC':4                           }                        ],                        'data':{                           '$color':'#ea973e'                        },                        'tipoEntidad':4,                        'Entidad':'AUT - SAM - ASP - COMERCIAL',                        'Frecuencia':654,                        'Porcentaje':82.58,                        'HC':12                     },                     {                        'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - SAM - ASP - POSTVENTA</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='nublado'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/nublado.svg' src='/img/ReporteoClima/nublado.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>77.53%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:6</p></div></div></center></div></div></div>',                        'id':'079cb952-b4fd-473d-9ee1-8c74f45a3b35',                        'children':[                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - SAM - ASP - POS - REFACCIONES</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>50%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:1</p></div></div></center></div></div></div>',                              'id':'f1ab8a93-f9ab-4b81-8435-1cf100d7bc91',                              'data':{                                 '$color':'#23282e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - SAM - ASP - POS - REFACCIONES',                              'Frecuencia':33,                              'Porcentaje':50,                              'HC':1                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - SAM - ASP - POS - SERVICIO </center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='solnube'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/solnube.svg' src='/img/ReporteoClima/solnube.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>83.03%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:5</p></div></div></center></div></div></div>',                              'id':'e62c81a5-5695-4e7e-b708-1a5c645f6b68',                              'data':{                                 '$color':'#ea973e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - SAM - ASP - POS - SERVICIO ',                              'Frecuencia':274,                              'Porcentaje':83.03,                              'HC':5                           }                        ],                        'data':{                           '$color':'#2f343a'                        },                        'tipoEntidad':4,                        'Entidad':'AUT - SAM - ASP - POSTVENTA',                        'Frecuencia':307,                        'Porcentaje':77.53,                        'HC':6                     }                  ],                  'data':{                     '$color':'#2f343a'                  }               },               {                  'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - SAM - ASU</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>69.02%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:56</p></div></div></center></div></div></div>',                  'id':'5e83efb8-f59e-43f0-8425-3734b8c4b9f7',                  'children':[                     {                        'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - SAM - ASU - ADMINISTRACION</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='nublado'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/nublado.svg' src='/img/ReporteoClima/nublado.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>74.24%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:8</p></div></div></center></div></div></div>',                        'id':'a95c4a10-f583-4047-b793-5bda1c686ded',                        'children':[                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - SAM - ASU - ADM - ADMINITRACION</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>59.85%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:2</p></div></div></center></div></div></div>',                              'id':'068f246a-4376-40dd-a42d-64067dce8c29',                              'data':{                                 '$color':'#23282e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - SAM - ASU - ADM - ADMINITRACION',                              'Frecuencia':79,                              'Porcentaje':59.85,                              'HC':2                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - SAM - ASU - ADM - CONTABILIDAD</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='solnube'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/solnube.svg' src='/img/ReporteoClima/solnube.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>86.36%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:2</p></div></div></center></div></div></div>',                              'id':'3a83d18c-0b88-4acb-9c7c-80e298cf2fc7',                              'data':{                                 '$color':'#ea973e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - SAM - ASU - ADM - CONTABILIDAD',                              'Frecuencia':114,                              'Porcentaje':86.36,                              'HC':2                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - SAM - ASU - ADM - CREDITO Y COBRANZA</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='sol'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/sol.svg' src='/img/ReporteoClima/sol.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>100%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:1</p></div></div></center></div></div></div>',                              'id':'dee9cf41-095a-4268-b810-882ef1f4e4ff',                              'data':{                                 '$color':'#f0af42'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - SAM - ASU - ADM - CREDITO Y COBRANZA',                              'Frecuencia':66,                              'Porcentaje':100,                              'HC':1                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - SAM - ASU - ADM - INTENDENCIA</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='solnube'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/solnube.svg' src='/img/ReporteoClima/solnube.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>88.64%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:2</p></div></div></center></div></div></div>',                              'id':'2b4b5f41-bdbd-4177-aeda-09d388bfb56f',                              'data':{                                 '$color':'#ea973e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - SAM - ASU - ADM - INTENDENCIA',                              'Frecuencia':117,                              'Porcentaje':88.64,                              'HC':2                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - SAM - ASU - ADM - MANTENIMIENTO</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>24.24%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:1</p></div></div></center></div></div></div>',                              'id':'aa022b4b-61d0-4b71-a3cc-956bdb42d71f',                              'data':{                                 '$color':'#23282e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - SAM - ASU - ADM - MANTENIMIENTO',                              'Frecuencia':16,                              'Porcentaje':24.24,                              'HC':1                           }                        ],                        'data':{                           '$color':'#2f343a'                        },                        'tipoEntidad':4,                        'Entidad':'AUT - SAM - ASU - ADMINISTRACION',                        'Frecuencia':392,                        'Porcentaje':74.24,                        'HC':8                     },                     {                        'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - SAM - ASU - COMERCIAL</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>65.76%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:20</p></div></div></center></div></div></div>',                        'id':'2e78db51-4be2-49c3-8575-527d636a1ba7',                        'children':[                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - SAM - ASU - COM - NUEVOS</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='nublado'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/nublado.svg' src='/img/ReporteoClima/nublado.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>75.48%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:11</p></div></div></center></div></div></div>',                              'id':'ffc7d026-725c-44d4-90c5-d1813aa63dff',                              'data':{                                 '$color':'#2f343a'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - SAM - ASU - COM - NUEVOS',                              'Frecuencia':548,                              'Porcentaje':75.48,                              'HC':11                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - SAM - ASU - COM - SEMINUEVOS</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>37.63%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:6</p></div></div></center></div></div></div>',                              'id':'1402a6aa-30e4-4397-bae1-39cfed21b452',                              'data':{                                 '$color':'#23282e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - SAM - ASU - COM - SEMINUEVOS',                              'Frecuencia':149,                              'Porcentaje':37.63,                              'HC':6                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - SAM - ASU - COM - SOPORTE COMERCIAL</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='solnube'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/solnube.svg' src='/img/ReporteoClima/solnube.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>86.36%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:3</p></div></div></center></div></div></div>',                              'id':'f2711b62-097c-417a-a1d0-63ebf90f7f30',                              'data':{                                 '$color':'#ea973e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - SAM - ASU - COM - SOPORTE COMERCIAL',                              'Frecuencia':171,                              'Porcentaje':86.36,                              'HC':3                           }                        ],                        'data':{                           '$color':'#23282e'                        },                        'tipoEntidad':4,                        'Entidad':'AUT - SAM - ASU - COMERCIAL',                        'Frecuencia':868,                        'Porcentaje':65.76,                        'HC':20                     },                     {                        'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - SAM - ASU - GERENCIA GENERAL</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='sol'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/sol.svg' src='/img/ReporteoClima/sol.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>96.54%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:7</p></div></div></center></div></div></div>',                        'id':'15ed77d8-b493-4545-8e9c-4030c5e9194d',                        'children':[                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - SAM - ASU - GER - -</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='sol'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/sol.svg' src='/img/ReporteoClima/sol.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>96.54%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:7</p></div></div></center></div></div></div>',                              'id':'f21dc662-3c54-4d93-976a-45f9e976eec4',                              'data':{                                 '$color':'#f0af42'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - SAM - ASU - GER - -',                              'Frecuencia':446,                              'Porcentaje':96.54,                              'HC':7                           }                        ],                        'data':{                           '$color':'#f0af42'                        },                        'tipoEntidad':4,                        'Entidad':'AUT - SAM - ASU - GERENCIA GENERAL',                        'Frecuencia':446,                        'Porcentaje':96.54,                        'HC':7                     },                     {                        'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - SAM - ASU - POSTVENTA</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>60.97%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:21</p></div></div></center></div></div></div>',                        'id':'206249d4-c83e-4690-a89f-475ec0db14d3',                        'children':[                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - SAM - ASU - POS - HOJALATERIA Y PINTURA</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>48.3%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:8</p></div></div></center></div></div></div>',                              'id':'8a4048ba-a1a8-409a-9862-53d12f6650f3',                              'data':{                                 '$color':'#23282e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - SAM - ASU - POS - HOJALATERIA Y PINTURA',                              'Frecuencia':255,                              'Porcentaje':48.3,                              'HC':8                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - SAM - ASU - POS - REFACCIONES</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='sol'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/sol.svg' src='/img/ReporteoClima/sol.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>92.42%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:3</p></div></div></center></div></div></div>',                              'id':'36baef04-8bcd-43b2-b5b5-8707282c40cf',                              'data':{                                 '$color':'#f0af42'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - SAM - ASU - POS - REFACCIONES',                              'Frecuencia':183,                              'Porcentaje':92.42,                              'HC':3                           },                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>AUT - SAM - ASU - POS - SERVICIO </center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='lluvia'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/lluvia.svg' src='/img/ReporteoClima/lluvia.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>61.67%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:10</p></div></div></center></div></div></div>',                              'id':'1647b627-a72d-403d-b023-8ff11fb21943',                              'data':{                                 '$color':'#23282e'                              },                              'tipoEntidad':5,                              'Entidad':'AUT - SAM - ASU - POS - SERVICIO ',                              'Frecuencia':407,                              'Porcentaje':61.67,                              'HC':10                           }                        ],                        'data':{                           '$color':'#23282e'                        },                        'tipoEntidad':4,                        'Entidad':'AUT - SAM - ASU - POSTVENTA',                        'Frecuencia':845,                        'Porcentaje':60.97,                        'HC':21                     }                  ],                  'data':{                     '$color':'#23282e'                  }               }            ],            'data':{               '$color':'#2f343a'            }         },         {            'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>COR - RECURSOS HUMANOS</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='solnube'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/solnube.svg' src='/img/ReporteoClima/solnube.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>85.61%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:2</p></div></div></center></div></div></div>',            'id':'1e3ab57a-fe8e-49de-9242-eef5157c54f7',            'children':[               {                  'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>COR - REC - DIR</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='sol'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/sol.svg' src='/img/ReporteoClima/sol.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>100%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:1</p></div></div></center></div></div></div>',                  'id':'40c72197-d973-4bb8-805f-5212ad9cacef',                  'children':[                     {                        'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>COR - REC - DIR - PRUEBA</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='sol'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/sol.svg' src='/img/ReporteoClima/sol.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>100%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:1</p></div></div></center></div></div></div>',                        'id':'fb8496a1-5d2a-4653-a3b2-7e3d1a0f42b6',                        'children':[                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>COR - REC - DIR - PRU - PRUEBA</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='sol'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/sol.svg' src='/img/ReporteoClima/sol.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>100%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:1</p></div></div></center></div></div></div>',                              'id':'10ca20fb-f83f-44ec-b24a-4d6cb6713089',                              'data':{                                 '$color':'#f0af42'                              },                              'tipoEntidad':5,                              'Entidad':'COR - REC - DIR - PRU - PRUEBA',                              'Frecuencia':66,                              'Porcentaje':100,                              'HC':1                           }                        ],                        'data':{                           '$color':'#f0af42'                        },                        'tipoEntidad':4,                        'Entidad':'COR - REC - DIR - PRUEBA',                        'Frecuencia':66,                        'Porcentaje':100,                        'HC':1                     }                  ],                  'data':{                     '$color':'#f0af42'                  }               },               {                  'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>COR - REC - DO</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='nublado'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/nublado.svg' src='/img/ReporteoClima/nublado.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>71.21%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:1</p></div></div></center></div></div></div>',                  'id':'54e91685-dfda-49f6-a1ea-e9039cfdee8e',                  'children':[                     {                        'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>COR - REC - DO - PRUEBA</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='nublado'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/nublado.svg' src='/img/ReporteoClima/nublado.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>71.21%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:1</p></div></div></center></div></div></div>',                        'id':'e9ea30bc-b91a-49a6-95ab-01002d71f315',                        'children':[                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>COR - REC - DO - PRU - PRUEBA</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='nublado'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/nublado.svg' src='/img/ReporteoClima/nublado.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>71.21%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:1</p></div></div></center></div></div></div>',                              'id':'40fef0bb-8c23-4d5a-b80a-575fb43217ff',                              'data':{                                 '$color':'#2f343a'                              },                              'tipoEntidad':5,                              'Entidad':'COR - REC - DO - PRU - PRUEBA',                              'Frecuencia':47,                              'Porcentaje':71.21,                              'HC':1                           }                        ],                        'data':{                           '$color':'#2f343a'                        },                        'tipoEntidad':4,                        'Entidad':'COR - REC - DO - PRUEBA',                        'Frecuencia':47,                        'Porcentaje':71.21,                        'HC':1                     }                  ],                  'data':{                     '$color':'#2f343a'                  }               }            ],            'data':{               '$color':'#ea973e'            }         },         {            'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>COR - SISTEMAS</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='sol'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/sol.svg' src='/img/ReporteoClima/sol.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>100%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:1</p></div></div></center></div></div></div>',            'id':'778de5bf-5b8f-481b-92cf-fb34cdbdd94c',            'children':[               {                  'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>COR - SIS - DES</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='sol'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/sol.svg' src='/img/ReporteoClima/sol.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>100%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:1</p></div></div></center></div></div></div>',                  'id':'c403c04a-fe9c-4534-85dc-00311654f080',                  'children':[                     {                        'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>COR - SIS - DES - PRUEBA</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='sol'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/sol.svg' src='/img/ReporteoClima/sol.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>100%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:1</p></div></div></center></div></div></div>',                        'id':'30175d21-a133-40af-b864-b083c46c9a02',                        'children':[                           {                              'name':'<div class='d_flex justify-content-center'><div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>COR - SIS - DES - PRU - PRUEBA</center></div><div class='row d_flex justify-content-center'><div class='col-5'><center><div class='sol'><center><img class='m-2 w-icon-impulsores' ng-src='/img/ReporteoClima/sol.svg' src='/img/ReporteoClima/sol.svg'></center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>100%</p><p class='yellow-clima f-17 mb-1 ng-binding'>HC:1</p></div></div></center></div></div></div>',                              'id':'658094e0-bb03-4e20-898e-5f7e697fbaff',                              'data':{                                 '$color':'#f0af42'                              },                              'tipoEntidad':5,                              'Entidad':'COR - SIS - DES - PRU - PRUEBA',                              'Frecuencia':66,                              'Porcentaje':100,                              'HC':1                           }                        ],                        'data':{                           '$color':'#f0af42'                        },                        'tipoEntidad':4,                        'Entidad':'COR - SIS - DES - PRUEBA',                        'Frecuencia':66,                        'Porcentaje':100,                        'HC':1                     }                  ],                  'data':{                     '$color':'#f0af42'                  },                  'tipoEntidad':3,                  'Entidad':'COR - SIS - DES',                  'Frecuencia':66,                  'Porcentaje':100,                  'HC':1               }            ],            'data':{               '$color':'#f0af42'            },            'tipoEntidad':2,            'Entidad':'COR - SISTEMAS',            'Frecuencia':66,            'Porcentaje':100,            'HC':1         }      ],      'data':{         '$color':'#2f343a'      }   }]";
            result.CURRENT_USER = strjson.cadena;
            result.CompanyDelAdmin = strjson.titulo;
            result.DefPass = strjson.enfoque;
            return PartialView("~/Views/ReporteoClima/EstructuraReporte/reportedinamicoEE.cshtml", result);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PwCicleEA(sendStringJson strjson)
        {
            var result = new ML.Result();
            result.CURRENT_USER = strjson.cadena;
            result.CompanyDelAdmin = strjson.titulo;
            result.DefPass = strjson.enfoque;
            return PartialView("~/Views/ReporteoClima/EstructuraReporte/reportedinamicoEA.cshtml", result);

        }

        public class sendStringJson
        {
            public string cadena { get; set; }
            public string titulo { get; set; }
            public string enfoque { get; set; }

        }

        #region SobreCargas con Año de aplicaicon

        #endregion

        public JsonResult GetUnidadNeg()
        {
            var data = BL.CompanyCategoria.GetAll();
            return Json(data, JsonRequestBehavior.AllowGet);
        }



        #region Metodos que no se usan

        /*public ActionResult GetImpulsoresClavesEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetImpulsoresClaveByUNEGocioEE(DataForFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetImpulsoresClaveByGeneroEE(DataForFilter, model.UnidadNegocioFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetImpulsoresClaveByFuncionEE(DataForFilter, model.UnidadNegocioFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetImpulsoresClaveByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetImpulsoresClaveByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetImpulsoresClaveByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetImpulsoresClaveByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetImpulsoresClaveByCompanyEE(DataForFilter, model.UnidadNegocioFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetImpulsoresClaveByAreaEE(DataForFilter, model.UnidadNegocioFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetImpulsoresClaveByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetImpulsoresClaveBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }*/

        /*public ActionResult GetImpulsoresClavesEA(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetImpulsoresClaveByUNEGocioEA(DataForFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetImpulsoresClaveByGeneroEA(DataForFilter, model.UnidadNegocioFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetImpulsoresClaveByFuncionEA(DataForFilter, model.UnidadNegocioFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetImpulsoresClaveByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetImpulsoresClaveByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetImpulsoresClaveByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetImpulsoresClaveByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetImpulsoresClaveByCompanyEA(DataForFilter, model.UnidadNegocioFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetImpulsoresClaveByAreaEA(DataForFilter, model.UnidadNegocioFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetImpulsoresClaveByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetImpulsoresClaveBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter, model.IdBD);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }*/

        /*public ActionResult GetValueComodinEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinByUNEGocioEnfoqueEmpresa(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinByGeneroEnfoqueEmpresa(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Func=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinByFuncionEnfoqueEmpresa(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinByCondicionTrabajoEnfoqueEmpresa(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinByGradoAcademicoEnfoqueEmpresa(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinByRangoAntiguedadEnfoqueEmpresa(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinByRangoEdadEnfoqueEmpresa(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinByCompanyEnfoqueEmpresa(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinByAreaEnfoqueEmpresa(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinByDepartamentoEnfoqueEmpresa(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinBySubdepartamentoEnfoqueEmpresa(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
            }

            return Json(resultados, JsonRequestBehavior.AllowGet);
        }*/

        /*public ActionResult GetValueComodinEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                string aux = item;if (aux.Contains("_")){aux = aux.Split('_')[1];model.UnidadNegocioFilter = item.Split('_')[0];} char[] arreglo = new char[50];
                arreglo = aux.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = aux.Remove(0, 6);  if (prefijo == "UNeg=>" && !aux.Contains("_")) { model.UnidadNegocioFilter = DataForFilter; }

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinByUNEGocioEnfoqueArea(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinByGeneroEnfoqueArea(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Func=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinByFuncionEnfoqueArea(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinByCondicionTrabajoEnfoqueArea(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinByGradoAcademicoEnfoqueArea(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinByRangoAntiguedadEnfoqueArea(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinByRangoEdadEnfoqueArea(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinByCompanyEnfoqueArea(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinByAreaEnfoqueArea(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinByDepartamentoEnfoqueArea(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinBySubdepartamentoEnfoqueArea(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
            }
            return Json(resultados, JsonRequestBehavior.AllowGet);
        }*/

        #endregion
    }
}