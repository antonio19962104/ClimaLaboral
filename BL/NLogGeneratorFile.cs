using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    /// <summary>
    /// Capa de Negocios del control de errores
    /// </summary>
    public class NLogGeneratorFile
    {
        public static NLog.Logger nlogData = NLog.LogManager.GetLogger(ML.LogTypes.LogPrintData);
        public static NLog.Logger nlogClimaDinamicoBackGroundJobReporte = NLog.LogManager.GetLogger(ML.LogTypes.LogClimaDinamicoBackGroundJobReporte);
        public static NLog.Logger nlogAccess = NLog.LogManager.GetLogger(ML.LogTypes.LogAccess);
        public static NLog.Logger nlogClimaDinamico = NLog.LogManager.GetLogger(ML.LogTypes.LogClimaDinamico);
        public static NLog.Logger nlogClimaDinamicoSMTP = NLog.LogManager.GetLogger(ML.LogTypes.nlogClimaDinamicoSMTP);
        public static NLog.Logger nlogClimaDinamicoFrontEnd = NLog.LogManager.GetLogger(ML.LogTypes.LogClimaDinamicoFrontEnd);
        public static NLog.Logger nlogModuloEncuestas = NLog.LogManager.GetLogger(ML.LogTypes.LogModuloEncuestas);
        public static NLog.Logger nlogModuloPlantillas = NLog.LogManager.GetLogger("LogModuloPlantillas");
        public static NLog.Logger nlogJobReportes = NLog.LogManager.GetLogger("LogJobReportes");
        public static NLog.Logger nlogPlanesDeAccion = NLog.LogManager.GetLogger("LogPlanesDeAccion");
        /// <summary>
        /// Log Job de Generacion del reporte grafico
        /// </summary>
        /// <param name="data"></param>
        /// <param name="st"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        public static void logBackGroundJobReporte(string data, StackTrace st, string a, int b, string c)
        {
            nlogClimaDinamicoBackGroundJobReporte.Info(data);
        }
        /// <summary>
        /// Log de Errores generales
        /// </summary>
        /// <param name="aE"></param>
        /// <param name="st"></param>
        public static void logError(Exception aE, StackTrace st)
        {
            try
            {
                if (st.GetFrame(0) != null)     nlogClimaDinamico.Error("Method:          " + st.GetFrame(0).GetMethod().Name);
                if (aE.Message != null)         nlogClimaDinamico.Error("Message:         " + aE.Message.Replace(System.Environment.NewLine, "-").Trim());
                if (aE != null)                 nlogClimaDinamico.Error("Exception:       " + aE.ToString().Replace(System.Environment.NewLine, "-").Trim());
                if (aE.InnerException != null)  nlogClimaDinamico.Error("Inner Exception: " + aE.InnerException.ToString().Replace(System.Environment.NewLine, "-").Trim());
                if (aE.StackTrace != null)      nlogClimaDinamico.Error("StackTrace:      " + aE.StackTrace.ToString().Replace(System.Environment.NewLine, "-").Trim());
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }
        /// <summary>
        /// Log de errores
        /// </summary>
        /// <param name="message"></param>
        /// <param name="st"></param>
        public static void logError(string message, StackTrace st)
        {
            nlogClimaDinamico.Error("Method: " + st.GetFrame(0).GetMethod().Name);
            nlogClimaDinamico.Error(message);
        }
        /// <summary>
        /// Log de objetos
        /// </summary>
        /// <param name="message"></param>
        public static void logData(string message)
        {
            nlogData.Info(message);
        }
        /// <summary>
        /// Log de informacion de procesos
        /// </summary>
        /// <param name="message"></param>
        /// <param name="st"></param>
        public static void logInfo(string message, StackTrace st) {
            nlogClimaDinamico.Info("Method: " + st.GetFrame(0).GetMethod().Name);
            nlogClimaDinamico.Info(message);
        }
        /// <summary>
        /// Log SMTP
        /// </summary>
        /// <param name="message"></param>
        /// <param name="aEstatus"></param>
        /// <param name="idUsuario"></param>
        public static void logInfoEmailSender(string message, ML.EstatusEmail aEstatus, int idUsuario)
        {
            nlogClimaDinamicoSMTP.Info(message);
            nlogClimaDinamicoSMTP.Info("Destinatario: " + aEstatus.Destinatario);
            nlogClimaDinamicoSMTP.Info("Encuesta:     " + aEstatus.Encuesta.IdEncuesta);
            nlogClimaDinamicoSMTP.Info("Base de Datos:" + aEstatus.BaseDeDatos.IdBaseDeDatos);
            nlogClimaDinamicoSMTP.Info("Idusuario:    " + idUsuario);
        }
        /// <summary>
        /// Log SMTP
        /// </summary>
        /// <param name="message"></param>
        /// <param name="IdEncuesta"></param>
        /// <param name="IdbaseDeDatos"></param>
        public static void logInfoEmailSender(string message, int? IdEncuesta, int? IdbaseDeDatos)
        {
            nlogClimaDinamicoSMTP.Info(message);
            nlogClimaDinamicoSMTP.Info("IdEncuesta:     " + IdEncuesta);
            nlogClimaDinamicoSMTP.Info("IdBaseDeDatos:  " + IdbaseDeDatos);
        }
        /// <summary>
        /// Log SMTP
        /// </summary>
        /// <param name="aE"></param>
        /// <param name="aEstatus"></param>
        /// <param name="idUsuario"></param>
        public static void logInfoEmailSender(Exception aE, ML.EstatusEmail aEstatus, int idUsuario)
        {
            nlogClimaDinamicoSMTP.Error("Exception:       " + aE);
            nlogClimaDinamicoSMTP.Info("Destinatario:     " + aEstatus.Destinatario);
            nlogClimaDinamicoSMTP.Info("Encuesta:         " + aEstatus.Encuesta.IdEncuesta);
            nlogClimaDinamicoSMTP.Info("Base de Datos:    " + aEstatus.BaseDeDatos.IdBaseDeDatos);
            nlogClimaDinamicoSMTP.Info("Idusuario:        " + idUsuario);
            nlogClimaDinamicoSMTP.Error("Message:         " + aE.Message);
            nlogClimaDinamicoSMTP.Error("Inner Exception: " + aE.InnerException);
            nlogClimaDinamicoSMTP.Error("StackTrace:      " + aE.StackTrace);
        }
        /// <summary>
        /// Log SMTP
        /// </summary>
        /// <param name="message"></param>
        /// <param name="success"></param>
        public static void logInfoEmailSender(string message, bool success)
        {
            if (success)
                nlogClimaDinamicoSMTP.Info(message);
            else
                nlogClimaDinamicoSMTP.Error(message);
        }
        /// <summary>
        /// Log eventos FrontEnd
        /// </summary>
        /// <param name="aClimaDinamico"></param>
        /// <returns></returns>
        public static bool logErrorFronEnd(ML.ClimaDinamico aClimaDinamico)
        {
            nlogClimaDinamicoFrontEnd.Error("IdEmpleado: " + aClimaDinamico.IdEmpleado);
            nlogClimaDinamicoFrontEnd.Error("IdEncuesta: " + aClimaDinamico.IdEncuesta);
            nlogClimaDinamicoFrontEnd.Error("Error:      " + aClimaDinamico.ErrorMessage);
            return true;
        }
        /// <summary>
        /// Log de acceso al portal
        /// </summary>
        /// <param name="aUsuario"></param>
        /// <param name="aNombre"></param>
        public static void logAccesCMS(string aUsuario, string aNombre)
        {
            nlogAccess.Info("Usuario: " + aUsuario);
            nlogAccess.Info("Nombre: " + aNombre);
        }
        /// <summary>
        /// Log de eliminación de reportes en \\10.5.2.101\RHDiagnostics\Reportes
        /// </summary>
        /// <param name="status"></param>
        /// <param name="reporte"></param>
        /// <param name="e"></param>
        public static void logJobDeleteReporte(bool status, string reporte, Exception e)
        {
            if (status)
            {
                nlogJobReportes.Info("El reporte " + reporte + " ha sido eliminado con exito");
            }
            else
            {
                nlogJobReportes.Error("El reporte " + reporte + " no pudo ser eliminado");
                nlogJobReportes.Error(e.Message);
            }
        }
        /// <summary>
        /// Log de insercion de datos del reporte gráfico
        /// </summary>
        /// <param name="status"></param>
        /// <param name="reporte"></param>
        /// <param name="e"></param>
        public static void logSaveReporte(bool status, string reporte, Exception e)
        {
            if (status)
            {
                nlogJobReportes.Info("El reporte ha sido guardado con exito en la ruta " + reporte);
            }
            else
            {
                nlogJobReportes.Error("El reporte " + reporte + " no pudo ser guardado");
                nlogJobReportes.Error(e.Message);
            }
        }

        #region Logs Por modulo
        /// <summary>
        /// Log Modulo de Encuestas
        /// </summary>
        /// <param name="aE"></param>
        /// <param name="st"></param>
        public static void logErrorModuloEncuestas(Exception aE, StackTrace st)
        {
            try
            {
                if (st.GetFrame(0) != null)     nlogModuloEncuestas.Error("Method:          " + st.GetFrame(0).GetMethod().Name);
                if (aE.Message != null)         nlogModuloEncuestas.Error("Message:         " + aE.Message.Replace(System.Environment.NewLine, "-").Trim());
                if (aE != null)                 nlogModuloEncuestas.Error("Exception:       " + aE.ToString().Replace(System.Environment.NewLine, "-").Trim());
                if (aE.InnerException != null)  nlogModuloEncuestas.Error("Inner Exception: " + aE.InnerException.ToString().Replace(System.Environment.NewLine, "-").Trim());
                if (aE.StackTrace != null)      nlogModuloEncuestas.Error("StackTrace:      " + aE.StackTrace.ToString().Replace(System.Environment.NewLine, "-").Trim());
            }
            catch (Exception e)
            {
                nlogModuloEncuestas.Error(e.Message);
            }
        }

        /// <summary>
        /// Log Modulo de Plantillas
        /// </summary>
        /// <param name="aE"></param>
        /// <param name="st"></param>
        public static void logErrorModuloPlantillas(Exception aE, StackTrace st)
        {
            try
            {
                if (st.GetFrame(0) != null)     nlogModuloPlantillas.Error("Method:          " + st.GetFrame(0).GetMethod().Name);
                if (aE.Message != null)         nlogModuloPlantillas.Error("Message:         " + aE.Message.Replace(System.Environment.NewLine, "-").Trim());
                if (aE != null)                 nlogModuloPlantillas.Error("Exception:       " + aE.ToString().Replace(System.Environment.NewLine, "-").Trim());
                if (aE.InnerException != null)  nlogModuloPlantillas.Error("Inner Exception: " + aE.InnerException.ToString().Replace(System.Environment.NewLine, "-").Trim());
                if (aE.StackTrace != null)      nlogModuloPlantillas.Error("StackTrace:      " + aE.StackTrace.ToString().Replace(System.Environment.NewLine, "-").Trim());
            }
            catch (Exception e)
            {
                nlogModuloPlantillas.Error(e.Message);
            }
        }



        /// <summary>
        /// Log de error del modulo de planes de accion
        /// </summary>
        /// <param name="aE"></param>
        /// <param name="st"></param>
        public static void logErrorModuloPlanesDeAccion(Exception aE, StackTrace st)
        {
            try
            {
                if (st.GetFrame(0) != null)     nlogPlanesDeAccion.Error("Method:          " + st.GetFrame(0).GetMethod().Name);
                if (aE.Message != null)         nlogPlanesDeAccion.Error("Message:         " + aE.Message.Replace(System.Environment.NewLine, "-").Trim());
                if (aE != null)                 nlogPlanesDeAccion.Error("Exception:       " + aE.ToString().Replace(System.Environment.NewLine, "-").Trim());
                if (aE.InnerException != null)  nlogPlanesDeAccion.Error("Inner Exception: " + aE.InnerException.ToString().Replace(System.Environment.NewLine, "-").Trim());
                if (aE.StackTrace != null)      nlogPlanesDeAccion.Error("StackTrace:      " + aE.StackTrace.ToString().Replace(System.Environment.NewLine, "-").Trim());
            }
            catch (Exception e)
            {
                nlogPlanesDeAccion.Error(e.Message);
            }
        }
        /// <summary>
        /// Log de informacion del modulo de planes de accion
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="myObject"></param>
        public static void logObjectsModuloPlanesDeAccion<T>(T myObject)
        {
            try
            {
                string json = "{";
                var properties = typeof(T).GetProperties();
                foreach (var property in properties)
                {
                    var value = Convert.ToString(property.GetValue(myObject));
                    if (!string.IsNullOrEmpty(value) && value != "0")
                        json += "'" + property.Name + "'" + ": " + "'" + value + "', ";
                }
                json = json.Substring(0, (json.Length - 2));
                json += "}";
                nlogPlanesDeAccion.Info(json);
            }
            catch (Exception aE)
            {
                logErrorModuloPlanesDeAccion(aE, new StackTrace());
            }
        }

        #endregion


    }
}
