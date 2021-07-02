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
                //EmailNotification

                try
                {
                    int n1 = 2;int n2 = 0;
                    var data = (n1 / n2);
                }
                catch (Exception ae)
                {
                    NLog.Logger demo = NLog.LogManager.GetLogger("EmailNotification");
                    demo.Debug("Modulo demo", st.GetFrame(0).GetMethod().Name, ae, ae.Message, ae.InnerException, ae.StackTrace);
                }

                var path1 = @"\\10.5.2.101\RHDiagnostics\log\LogPortalDeEncuestas.log";
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
                nlogDiagnostics4U.Debug(e);
                nlogDiagnostics4U.Debug("Method: " + st.GetFrame(0).GetMethod().Name);
                nlogDiagnostics4U.Debug("Messsage: " + e.Message);
                nlogDiagnostics4U.Debug("Inner Exception: " + e.InnerException);
                nlogDiagnostics4U.Debug("StackTrace: " + e.StackTrace);
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
        public static string addJsonReporte(int? entidadId, string entidadNombre, object data, int AnioActual, string usuario)
        {
            var jsonData = (JsonResult)data;
            var jsonEE = JsonConvert.SerializeObject(jsonData);
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var demo = new DL.Demo { EntidadId = entidadId, EntidadNombre = entidadNombre, jsonString = jsonEE, Anio = AnioActual, objName = jsonData.ContentType, status = 0, usuario = usuario };
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
        public static string addJsonReporte(int? entidadId, string entidadNombre, object data, int AnioActual, string objname, string usuario)
        {
            var jsonData = (JsonResult)data;
            var jsonEE = JsonConvert.SerializeObject(jsonData);
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var demo = new DL.Demo { EntidadId = entidadId, EntidadNombre = entidadNombre, jsonString = jsonEE, Anio = AnioActual, objName = objname, status = 0, usuario = usuario };
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
        public static bool updateStatusReporte(string entidadNombre, int AnioActual, string user)
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    //ENTIDADID = {0} AND
                    context.Database.ExecuteSqlCommand("UPDATE DEMO SET STATUS = 1 WHERE ENTIDADNOMBRE = {1} AND ANIO = {2} AND USUARIO = {3}", 0, entidadNombre, AnioActual, user);
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
        public static string sendMail(string entidadName, int Anio, string UsuarioSolicita, string url)
        {
            if (UsuarioSolicita.GetType().Name == "Email")
            {

            }
            var body = "<p>La creacion del reporte de la entidad</p>" +  
                            "<p>" + entidadName + " " + " del año " + Anio + " ha finalizado </p> <br />";
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
                }
                catch (SmtpException ex)
                {
                    BL.LogReporteoClima.writteLogJobReporte(ex, new StackTrace());
                    return ex.Message;
                }
                finally
                {
                    smtp.Dispose();
                }
            }
            return "success";
        }
        public static string getJsonString(ML.Historico aHistorico, string aliasObj)
        {
            aHistorico.Anio = aHistorico.Anio + 1;
            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                var query = context.Demo.Select(o => o).Where(o => o.Anio == aHistorico.Anio && o.EntidadId == aHistorico.EntidadId && o.EntidadNombre == aHistorico.EntidadNombre && o.objName == aliasObj && o.usuario == aHistorico.CurrentUsr).FirstOrDefault();
                if (query != null)
                {
                    return (query.jsonString);
                }
            }
            return String.Empty;
        }
    }
}
