using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Net;
using Newtonsoft.Json;
using System.Data.Entity.Core.Objects;
using System.Data;
using System.Net.Mail;
using System.Configuration;
using System.Web;
using System.Net.Http;
using ML;
using System.Diagnostics;
using System.Web.Mvc;

namespace BL
{
    public class LogReporteoClima
    {
        public static NLog.Logger nlogJobReporteo = NLog.LogManager.GetLogger(ML.LogTypes.LogJobReporteoClima);
        public static NLog.Logger nlogFrontEndReporteo = NLog.LogManager.GetLogger(ML.LogTypes.LogFrontEndReporteoClima);
        public static NLog.Logger nlogBackEndReporteo = NLog.LogManager.GetLogger(ML.LogTypes.LogBackEndReporteoClima);
        public static NLog.Logger nlogEncuestaClima = NLog.LogManager.GetLogger(ML.LogTypes.LogEncuestaClima);
        public static NLog.Logger nlogExceptionLog = NLog.LogManager.GetLogger(ML.LogTypes.LogException);
        public static NLog.Logger nlogDiagnostics4U = NLog.LogManager.GetLogger(ML.LogTypes.LogDiagnostics4U);
        public static void writeLogEncuestaClima(Exception e, StackTrace st)
        {
            try
            {
                var path1 = @"\\10.5.2.101\RHDiagnostics\log\LogEncuestaClimaLaboral.log";
                var fullPath1 = Path.GetFullPath(path1);
                if (!File.Exists(fullPath1))
                {
                    string createText = "Log Create At " + DateTime.Now + Environment.NewLine;
                    File.WriteAllText(fullPath1, createText);
                }
                string
                appendText1 = Environment.NewLine + "Metodo: " + st.GetFrame(0).GetMethod().Name + " " + DateTime.Now + Environment.NewLine;
                appendText1 += "Excepcion: " + e.Message + Environment.NewLine;
                appendText1 += "Trace: " + e.StackTrace + Environment.NewLine;
                appendText1 += "Inner Exception: " + e.InnerException + Environment.NewLine;
                File.AppendAllText(fullPath1, appendText1);
                nlogEncuestaClima.Debug(e);
                nlogEncuestaClima.Debug("Method: " + st.GetFrame(0).GetMethod().Name);
                nlogEncuestaClima.Debug("Messsage: " + e.Message);
                nlogEncuestaClima.Debug("Inner Exception: " + e.InnerException);
                nlogEncuestaClima.Debug("StackTrace: " + e.StackTrace);
            }
            catch (Exception ex)
            {
                nlogExceptionLog.Error(ex);
                nlogExceptionLog.Error("Messsage: " + ex.Message);
                nlogExceptionLog.Error("Inner Excepcion: " + ex.InnerException);
                nlogExceptionLog.Error("StackTrace: : " + ex.StackTrace);
            }
        }
        public static void writeLogEncuestaClima(string e, StackTrace st)
        {
            try
            {
                var path1 = @"\\10.5.2.101\RHDiagnostics\log\LogEncuestaClimaLaboral.log";
                var fullPath1 = Path.GetFullPath(path1);
                if (!File.Exists(fullPath1))
                {
                    string createText = "Log Create At " + DateTime.Now + Environment.NewLine;
                    File.WriteAllText(fullPath1, createText);
                }
                string
                appendText1 = Environment.NewLine + "Metodo: " + st.GetFrame(0).GetMethod().Name + " " + DateTime.Now + Environment.NewLine;
                appendText1 += "Excepcion: " + e + Environment.NewLine;
                File.AppendAllText(fullPath1, appendText1);
                nlogEncuestaClima.Debug(e);
                nlogEncuestaClima.Debug("Method: " + st.GetFrame(0).GetMethod().Name);
            }
            catch (Exception ex)
            {
                nlogExceptionLog.Error(ex);
                nlogExceptionLog.Error("Messsage: " + ex.Message);
                nlogExceptionLog.Error("Inner Excepcion: " + ex.InnerException);
                nlogExceptionLog.Error("StackTrace: : " + ex.StackTrace);
            }
        }
        public static bool writteLog(Exception e, StackTrace st)
        {
            try
            {
                //string 
                //appendText1 = Environment.NewLine + "Metodo: " + st.GetFrame(0).GetMethod().Name + " " + DateTime.Now + Environment.NewLine;
                //appendText1 += "Excepcion: " + e.Message + Environment.NewLine;
                //appendText1 += "Trace: " + e.StackTrace + Environment.NewLine;
                //BL.NLogGeneratorFile.logError(e, new StackTrace());
                //appendText1 += "Inner Exception: " + e.InnerException + Environment.NewLine;
                //File.AppendAllText(fullPath1, appendText1);
                //nlogDiagnostics4U.Debug(e);
                //nlogDiagnostics4U.Debug("Method: " + st.GetFrame(0).GetMethod().Name);
                //nlogDiagnostics4U.Debug("Messsage: " + e.Message);
                //nlogDiagnostics4U.Debug("Inner Exception: " + e.InnerException);
                //nlogDiagnostics4U.Debug("StackTrace: " + e.StackTrace);
            }
            catch (Exception ex)
            {
                nlogExceptionLog.Error(ex);
                nlogExceptionLog.Error("Messsage: " + ex.Message);
                nlogExceptionLog.Error("Inner Excepcion: " + ex.InnerException);
                nlogExceptionLog.Error("StackTrace: : " + ex.StackTrace);
                return false;
            }
            return true;
        }
        
        public static bool writteLogJobReporte(Exception e, StackTrace st)
        {
            try
            {
                var path1 = @"\\10.5.2.101\RHDiagnostics\log\LogJobReporteClima.log";
                var fullPath1 = Path.GetFullPath(path1);
                if (!File.Exists(fullPath1))
                {
                    string createText = "Log Create At " + DateTime.Now + Environment.NewLine;
                    File.WriteAllText(fullPath1, createText);
                }
                string
                appendText1 = Environment.NewLine + "Metodo: " + st.GetFrame(0).GetMethod().Name + " " + DateTime.Now + Environment.NewLine;
                appendText1 += "Excepcion: " + e.Message + Environment.NewLine;
                appendText1 += "Trace: " + e.StackTrace + Environment.NewLine;
                appendText1 += "Inner Exception: " + e.InnerException + Environment.NewLine;
                File.AppendAllText(fullPath1, appendText1);
                nlogJobReporteo.Debug(e);
                nlogJobReporteo.Debug("Method: " + st.GetFrame(0).GetMethod().Name);
                nlogJobReporteo.Debug("Messsage: " + e.Message);
                nlogJobReporteo.Debug("Inner Exception: " + e.InnerException);
                nlogJobReporteo.Debug("StackTrace: " + e.StackTrace);
            }
            catch (Exception ex)
            {
                nlogExceptionLog.Error(ex);
                nlogExceptionLog.Error("Messsage: " + ex.Message);
                nlogExceptionLog.Error("Inner Excepcion: " + ex.InnerException);
                nlogExceptionLog.Error("StackTrace: : " + ex.StackTrace);
                return false;
            }
            return true;
        }
        public static bool writteLogJobReporte(string e, StackTrace st, string usuario, int? tipoEntidad, string entidad)
        {
            string
                appendText1 = "";
            try
            {
                var path1 = @"\\10.5.2.101\RHDiagnostics\log\LogJobReporteClima.log";
                var fullPath1 = Path.GetFullPath(path1);
                if (!File.Exists(fullPath1))
                {
                    string createText = "Log Create At " + DateTime.Now + Environment.NewLine;
                    File.WriteAllText(fullPath1, createText);
                }
                if (e == "Entré a generar el reporte")
                {
                    appendText1 += Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + "/****************************************************************************************************************/";
                }
                appendText1 += Environment.NewLine + "Metodo: " + st.GetFrame(0).GetMethod().Name + " " + DateTime.Now + Environment.NewLine;
                appendText1 += "Mensaje: " + e + Environment.NewLine;
                appendText1 += "Usuario: " + usuario + Environment.NewLine;
                appendText1 += "TipoEntidad: " + tipoEntidad + Environment.NewLine;
                appendText1 += "Entidad: " + entidad + Environment.NewLine;
                File.AppendAllText(fullPath1, appendText1);
                nlogJobReporteo.Info(e);
                nlogJobReporteo.Info("Method: " + st.GetFrame(0).GetMethod().Name);
                nlogJobReporteo.Info("Usuario: " + usuario);
                nlogJobReporteo.Info("Tipo Entidad: " + tipoEntidad);
                nlogJobReporteo.Info("Entidad: " + entidad);
            }
            catch (Exception ex)
            {
                nlogExceptionLog.Error(ex);
                nlogExceptionLog.Error("Messsage: " + ex.Message);
                nlogExceptionLog.Error("Inner Excepcion: " + ex.InnerException);
                nlogExceptionLog.Error("StackTrace: : " + ex.StackTrace);
                return false;
            }
            return true;
        }
        public static bool writteLog(string excepcionMessage, StackTrace st)
        {
            try
            {
                var path1 = @"\\10.5.2.101\RHDiagnostics\log\LogReporteoClimaBackEnd.log";
                var fullPath1 = Path.GetFullPath(path1);
                if (!File.Exists(fullPath1))
                {
                    string createText = "Log Create At " + DateTime.Now + Environment.NewLine;
                    File.WriteAllText(fullPath1, createText);
                }
                string
                appendText1 = Environment.NewLine + "Method: " + st.GetFrame(0).GetMethod().Name + " " + DateTime.Now + Environment.NewLine;
                appendText1 += "Excepcion: " + excepcionMessage + Environment.NewLine;
                File.AppendAllText(fullPath1, appendText1);
                nlogBackEndReporteo.Debug(excepcionMessage);
                nlogBackEndReporteo.Debug("Method: " + st.GetFrame(0).GetMethod().Name);
            }
            catch (Exception ex)
            {
                nlogExceptionLog.Error(ex);
                nlogExceptionLog.Error("Messsage: " + ex.Message);
                nlogExceptionLog.Error("Inner Excepcion: " + ex.InnerException);
                nlogExceptionLog.Error("StackTrace: : " + ex.StackTrace);
                return false;
            }
            return true;
        }
        public static bool writteLogCron(string message, StackTrace st)
        {
            try
            {
                var path1 = @"\\10.5.2.101\RHDiagnostics\log\LogCronEveryMinute.log";
                var fullPath1 = Path.GetFullPath(path1);
                if (!File.Exists(fullPath1))
                {
                    string createText = "Cron Create At " + DateTime.Now + Environment.NewLine;
                    File.WriteAllText(fullPath1, createText);
                }
                string
                appendText1 = Environment.NewLine + "Method: " + st.GetFrame(0).GetMethod().Name + " " + DateTime.Now + Environment.NewLine;
                appendText1 += "Mensaje: " + message + Environment.NewLine;
                File.AppendAllText(fullPath1, appendText1);
            }
            catch (Exception ex)
            {
                nlogExceptionLog.Error(ex);
                nlogExceptionLog.Error("Messsage: " + ex.Message);
                nlogExceptionLog.Error("Inner Excepcion: " + ex.InnerException);
                nlogExceptionLog.Error("StackTrace: : " + ex.StackTrace);
                return false;
            }
            return true;
        }
        public static bool writteLogCronOneMore(string message, StackTrace st)
        {
            try
            {
                var path1 = @"\\10.5.2.101\RHDiagnostics\log\LogCronOnlyOneMore.log";
                var fullPath1 = Path.GetFullPath(path1);
                if (!File.Exists(fullPath1))
                {
                    string createText = "Cron Create At " + DateTime.Now + Environment.NewLine;
                    File.WriteAllText(fullPath1, createText);
                }
                string
                appendText1 = Environment.NewLine + "Method: " + st.GetFrame(0).GetMethod().Name + " " + DateTime.Now + Environment.NewLine;
                appendText1 += "Mensaje: " + message + Environment.NewLine;
                File.AppendAllText(fullPath1, appendText1);
            }
            catch (Exception ex)
            {
                nlogExceptionLog.Error(ex);
                nlogExceptionLog.Error("Messsage: " + ex.Message);
                nlogExceptionLog.Error("Inner Excepcion: " + ex.InnerException);
                nlogExceptionLog.Error("StackTrace: : " + ex.StackTrace);
                return false;
            }
            return true;
        }
        public static bool writteLogFrontEnd(string excepcionMessage, string funcion, string currentUsr)
        {
            try
            {
                var path1 = @"\\10.5.2.101\RHDiagnostics\log\LogReporteoClimaFronEnd.log";
                var fullPath1 = Path.GetFullPath(path1);
                if (!File.Exists(fullPath1))
                {
                    string createText = "Log Create At " + DateTime.Now + Environment.NewLine;
                    File.WriteAllText(fullPath1, createText);
                }
                string appendText1 = Environment.NewLine + "Usuario: " + currentUsr + " " + DateTime.Now + Environment.NewLine;
                appendText1 += "Funcion: " + funcion + Environment.NewLine;
                appendText1 += "Excepcion: " + excepcionMessage.Split('J', 'A', 'M', 'G')[0] + Environment.NewLine;
                appendText1 += "Excepcion: " + excepcionMessage + Environment.NewLine;
                File.AppendAllText(fullPath1, appendText1);
                nlogFrontEndReporteo.Debug(excepcionMessage);
                nlogFrontEndReporteo.Debug("Funcion: " + funcion);
                nlogFrontEndReporteo.Debug("Usuario: " + currentUsr);
            }
            catch (Exception ex)
            {
                nlogExceptionLog.Error(ex);
                nlogExceptionLog.Error("Messsage: " + ex.Message);
                nlogExceptionLog.Error("Inner Excepcion: " + ex.InnerException);
                nlogExceptionLog.Error("StackTrace: : " + ex.StackTrace);
                return false;
            }
            return true;
        }
        public static bool writeLogDataRequest(List<ML.Historico> data, string funcion, string currentUsr, string entidadReporte)
        {
            try
            {
                var path1 = @"\\10.5.2.101\RHDiagnostics\log\" + entidadReporte + ".log";
                var fullPath1 = Path.GetFullPath(path1);
                if (!File.Exists(fullPath1))
                {
                    string createText = "Log Create At " + DateTime.Now + Environment.NewLine;
                    File.WriteAllText(fullPath1, createText);
                }
                string appendText1 = "Usuario: " + currentUsr + " " + DateTime.Now + Environment.NewLine;
                appendText1 += "Funcion: " + funcion + DateTime.Now + Environment.NewLine;
                appendText1 += "Data: " + Environment.NewLine;
                foreach (var item in data)
                {
                    appendText1 += item.EntidadNombre;
                }
               File.AppendAllText(fullPath1, appendText1);
                
            }
            catch (Exception ex)
            {
                nlogExceptionLog.Error(ex);
                nlogExceptionLog.Error("Messsage: " + ex.Message);
                nlogExceptionLog.Error("Inner Excepcion: " + ex.InnerException);
                nlogExceptionLog.Error("StackTrace: : " + ex.StackTrace);
                return false;
            }
            return true;
        }
        public static string getUnidadNegocioByUnidad(int? Unidadneg)
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var UnidadNegocio = context.CompanyCategoria.Select(o => o).Where(o => o.IdCompanyCategoria == Unidadneg).FirstOrDefault();

                    return UnidadNegocio.Descripcion + "_" + UnidadNegocio.IdCompanyCategoria;
                }
            }
            catch (Exception aE)
            {
                BL.LogReporteoClima.writteLogJobReporte(aE, new StackTrace());
                return aE.Message;
            }
        }
        public static string getUnidadNegocioByCompany(int? CompanyId)
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var company = context.Company.Select(o => new { o.CompanyName, o.CompanyId, o.IdCompanyCategoria }).Where(o => o.CompanyId == CompanyId).FirstOrDefault();
                    var UnidadNegocio = context.CompanyCategoria.Select(o => o).Where(o => o.IdCompanyCategoria == company.IdCompanyCategoria).FirstOrDefault();

                    return UnidadNegocio.Descripcion + "_" + UnidadNegocio.IdCompanyCategoria;
                }
            }
            catch (Exception aE)
            {
                BL.LogReporteoClima.writteLogJobReporte(aE, new StackTrace());
                return aE.Message;
            }
        }
        public static string getUnidadNegocioByArea(int? IdArea)
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var Area = context.Area.Select(o => new { o.Nombre, o.IdArea, o.CompanyId }).Where(o => o.IdArea == IdArea).FirstOrDefault();
                    var company = context.Company.Select(o => new { o.CompanyName, o.CompanyId, o.IdCompanyCategoria }).Where(o => o.CompanyId == Area.CompanyId).FirstOrDefault();
                    var UnidadNegocio = context.CompanyCategoria.Select(o => o).Where(o => o.IdCompanyCategoria == company.IdCompanyCategoria).FirstOrDefault();

                    return UnidadNegocio.Descripcion + "_" + UnidadNegocio.IdCompanyCategoria;
                }
            }
            catch (Exception aE)
            {
                BL.LogReporteoClima.writteLogJobReporte(aE, new StackTrace());
                return aE.Message;
            }
        }
        public static string getUnidadNegocioByDepartamento(int? IdDepartamento)
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var Departamento = context.Departamento.Select(o => new { o.Nombre, o.IdDepartamento, o.IdArea }).Where(o => o.IdDepartamento == IdDepartamento).FirstOrDefault();
                    var Area = context.Area.Select(o => new { o.Nombre, o.IdArea, o.CompanyId }).Where(o => o.IdArea == Departamento.IdArea).FirstOrDefault();
                    var company = context.Company.Select(o => new { o.CompanyName, o.CompanyId, o.IdCompanyCategoria }).Where(o => o.CompanyId == Area.CompanyId).FirstOrDefault();
                    var UnidadNegocio = context.CompanyCategoria.Select(o => o).Where(o => o.IdCompanyCategoria == company.IdCompanyCategoria).FirstOrDefault();

                    return UnidadNegocio.Descripcion + "_" + UnidadNegocio.IdCompanyCategoria;
                }
            }
            catch (Exception aE)
            {
                BL.LogReporteoClima.writteLogJobReporte(aE, new StackTrace());
                return aE.Message;
            }
        }
        public static string addJsonReporte(int? entidadId, string entidadNombre, object data, int AnioActual, string usuario, string nivelDetalle)
        {
            var jsonData = (JsonResult)data;
            var jsonEE = JsonConvert.SerializeObject(jsonData);
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var demo = new DL.Demo { EntidadId = entidadId, EntidadNombre = entidadNombre, jsonString = jsonEE, Anio = AnioActual, objName = jsonData.ContentType, status = 0, usuario = usuario, NivelDetalle = nivelDetalle };
                    demo.FechaHoraCreacion = DateTime.Now; demo.UsuarioCreacion = usuario; demo.ProgramaCreacion = "JobCreacionReporte";
                    var query = context.Demo.Add(demo);
                    context.SaveChanges();
                    BL.LogReporteoClima.writteLogJobReporte(jsonEE, new StackTrace(), usuario, 0, entidadNombre);
                    var getJson = JsonConvert.DeserializeObject<JsonResult>(jsonEE);
                    return "Success";
                }
            }
            catch (Exception aE)
            {
                BL.LogReporteoClima.writteLogJobReporte(aE, new StackTrace());
                return aE.Message;
            }
        }
        public static string addJsonReporte(int? entidadId, string entidadNombre, object data, int AnioActual, string objname, string usuario, string nivelDetalle)
        {
            var jsonData = (JsonResult)data;
            var jsonEE = JsonConvert.SerializeObject(jsonData);
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var demo = new DL.Demo { EntidadId = entidadId, EntidadNombre = entidadNombre, jsonString = jsonEE, Anio = AnioActual, objName = objname, status = 0, usuario = usuario, NivelDetalle = nivelDetalle };
                    demo.FechaHoraCreacion = DateTime.Now; demo.UsuarioCreacion = usuario; demo.ProgramaCreacion = "JobCreacionReporte";
                    var query = context.Demo.Add(demo);
                    context.SaveChanges();
                    BL.LogReporteoClima.writteLogJobReporte(jsonEE, new StackTrace(), usuario, 0, entidadNombre);
                    var getJson = JsonConvert.DeserializeObject<JsonResult>(jsonEE);
                    return "Success";
                }
            }
            catch (Exception aE)
            {
                BL.LogReporteoClima.writteLogJobReporte(aE, new StackTrace());
                return aE.Message;
            }
        }
        public static bool updateStatusReporte(string entidadNombre, int AnioActual, string user, string nivelDetalle, int IdBD)
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    //ENTIDADID = {0} AND
                    context.Database.ExecuteSqlCommand("UPDATE DEMO SET STATUS = 1, IdBaseDeDatos = {0} WHERE ENTIDADNOMBRE = {1} AND ANIO = {2} AND USUARIO = {3} AND NivelDetalle = {4}", IdBD, entidadNombre, AnioActual, user, nivelDetalle);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception aE)
            {
                BL.LogReporteoClima.writteLogJobReporte(aE, new StackTrace());
                return false;
            }
        }
        /// <summary>
        /// Job que elimina los reportes con fecha de creacion mayor a 30 dias
        /// </summary>
        public static void DeleteReportes()
        {
            try
            {
                var ruta = @"\\\\10.5.2.101\\RHDiagnostics\\Reportes\\";
                var folders = Directory.GetDirectories(ruta).ToList();
                foreach (var folder in folders)
                {
                    var files = Directory.GetFiles(folder).ToList();
                    foreach (var reporte in files)
                    {
                        DateTime fechaCreacion = File.GetCreationTime(reporte);
                        if (DateTime.Now <= fechaCreacion.AddDays(30))
                            BL.NLogGeneratorFile.nlogJobReportes.Info("El reporte " + reporte + " aun tiene una vigencia hasta el dia " + fechaCreacion.AddDays(30));
                        if (DateTime.Now > fechaCreacion.AddDays(30))
                        {
                            try
                            {
                                File.Delete(reporte);
                                BL.NLogGeneratorFile.logJobDeleteReporte(true, reporte, new Exception());
                            }
                            catch (Exception e)
                            {
                                BL.NLogGeneratorFile.logJobDeleteReporte(false, reporte, e);
                            }
                        }
                    }
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.nlogJobReportes.Error("Excepcion en el Job");
                BL.NLogGeneratorFile.nlogJobReportes.Error(aE);
            }
        }

        public static void GetJobs()
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var jobList = context.Job.ToList();
                    foreach (var Jobitem in jobList)
                    {
                        var status = context.State.Select(o => o).Where(o => o.Id == Jobitem.StateId && o.JobId == Jobitem.Id).FirstOrDefault();
                        Jobitem.State.Add(status);
                    }
                }
            }
            catch (Exception aE)
            {

            }
        }

        public static string sendMail(string entidadName, int Anio, string UsuarioSolicita, string url, string ps, string nivelDetalle, int opc, string criterioBusquedaSeleccionado, int enfoqueSeleccionado, string lvlDetalle, int IdBD = 0)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            string token = string.Empty;
            BL.Seguridad seguridad = new Seguridad();
            ps = seguridad.DesencriptarCadena(ps);
            //usuario|password|opc|tipoEntidad|entidadNombre|anio|enfoque|lvlDetalle|IdBD
            token = UsuarioSolicita + "|" + ps + "|"+opc+"|"+criterioBusquedaSeleccionado+"|"+entidadName+"|"+Anio+"|"+ enfoqueSeleccionado + "|" + lvlDetalle + "|" + IdBD;
            token = seguridad.EncriptarCadena(token);
            if (string.IsNullOrEmpty(UsuarioSolicita))
            {
                UsuarioSolicita = "jamurillo@grupoautofin.com";
            }
            string nivelDetalleMsg = (nivelDetalle.Contains("1") == true ? "Unidad de Negocio - " : "") +
                                     (nivelDetalle.Contains("2") == true ? "Dirección - " : "") +
                                     (nivelDetalle.Contains("3") == true ? "Área - " : "") +
                                     (nivelDetalle.Contains("4") == true ? "Departamento - " : "") +
                                     (nivelDetalle.Contains("5") == true ? "Subdepartamento" : "");
            var body =
                 "<p style='font-weight:bold;'>Que tal " + GetFullNameByAdmin(UsuarioSolicita, ps) + "</p>" +
                "<p>La creacion del reporte de la entidad " + entidadName + " " + " del año " + Anio + " con el nivel de detalle: " + nivelDetalleMsg + " ha finalizado </p>" +
                "<p>Consultalo dando click en a siguiente imágen</p>" +
                "<p><a href='" + url + "reporteoClima/Index/?token=" + token + "'><img src='http://www.diagnostic4u.com/img/Logo_emails.png' style='border-radius: 5px;'></a></p>";
             //body += "Accede a <a href='"+ url + "" +"'></a>";
             var message = new MailMessage();
            message.To.Add(new MailAddress(UsuarioSolicita));
            message.Subject = "Notificación Diagnostic4U";
            message.Body = string.Format(body, "DIAGNOSTIC4U", "", "");
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                try
                {
                    smtp.Send(message);
                    BL.NLogGeneratorFile.logInfoEmailSender("Email enviado al usuario: " + UsuarioSolicita + " para notificar el fin de la creacion del reporte de: " + entidadName, true);
                }
                catch (SmtpException ex)
                {
                    BL.LogReporteoClima.writteLogJobReporte(ex, new StackTrace());
                    BL.NLogGeneratorFile.logInfoEmailSender("Ocurrio un error al enviar el email al usuario: " + UsuarioSolicita + " para notificar la creacion del reporte", false);
                    return ex.Message;
                }
                finally
                {
                    smtp.Dispose();
                }
            }
            return "success";
        }

        public static string GetFullNameByAdmin(string usr, string pass)
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var admin = context.Administrador.Where(o => o.UserName == usr && o.Password == pass).FirstOrDefault();
                    var empleado = context.Empleado.Where(o => o.IdEmpleado == admin.IdEmpleado).FirstOrDefault();
                    return empleado.Nombre + " " + empleado.ApellidoPaterno + " " + empleado.ApellidoMaterno;
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return usr;
            }
        }
        public static string getJsonString(ML.Historico aHistorico, string aliasObj)
        {
            aHistorico.Anio = aHistorico.Anio + 1;
            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                var query = context.Demo.Select(o => o).Where(o => o.IdBaseDeDatos == aHistorico.IdBaseDeDatos && o.Anio == aHistorico.Anio && o.NivelDetalle == aHistorico.nivelDetalle /*&& o.EntidadId == aHistorico.EntidadId*/ && o.EntidadNombre == aHistorico.EntidadNombre && o.objName == aliasObj && o.usuario == aHistorico.CurrentUsr).FirstOrDefault();
                if (query != null)
                {
                    return (query.jsonString);
                }
            }
            return String.Empty;
        }
        public static string GetUnidadNegocioByName(string entidad, int IdBD)
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var data = new List<DL.Empleado>();

                    data = context.Empleado.Where(o => o.UnidadNegocio == entidad && o.IdBaseDeDatos == IdBD).ToList();
                    if (data.Count > 0)
                    {
                        return data[0].UnidadNegocio;
                    }
                    data = context.Empleado.Where(o => o.DivisionMarca == entidad && o.IdBaseDeDatos == IdBD).ToList();
                    if (data.Count > 0)
                    {
                        return data[0].UnidadNegocio;
                    }
                    data = context.Empleado.Where(o => o.AreaAgencia == entidad && o.IdBaseDeDatos == IdBD).ToList();
                    if (data.Count > 0)
                    {
                        return data[0].UnidadNegocio;
                    }
                    data = context.Empleado.Where(o => o.Depto == entidad && o.IdBaseDeDatos == IdBD).ToList();
                    if (data.Count > 0)
                    {
                        return data[0].UnidadNegocio;
                    }
                    data = context.Empleado.Where(o => o.Subdepartamento == entidad && o.IdBaseDeDatos == IdBD).ToList();
                    if (data.Count > 0)
                    {
                        return data[0].UnidadNegocio;
                    }
                }
            }
            catch (Exception aE)
            {
                return string.Empty;
            }
            return string.Empty;
        }
    }
}
