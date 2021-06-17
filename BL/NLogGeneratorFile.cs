using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class NLogGeneratorFile
    {
        public static NLog.Logger nlogData = NLog.LogManager.GetLogger(ML.LogTypes.LogPrintData);
        public static NLog.Logger nlogClimaDinamicoBackGroundJobReporte = NLog.LogManager.GetLogger(ML.LogTypes.LogClimaDinamicoBackGroundJobReporte);
        public static NLog.Logger nlogAccess = NLog.LogManager.GetLogger(ML.LogTypes.LogAccess);
        public static NLog.Logger nlogClimaDinamico = NLog.LogManager.GetLogger(ML.LogTypes.LogClimaDinamico);
        public static NLog.Logger nlogClimaDinamicoSMTP = NLog.LogManager.GetLogger(ML.LogTypes.nlogClimaDinamicoSMTP);
        public static NLog.Logger nlogClimaDinamicoFrontEnd = NLog.LogManager.GetLogger(ML.LogTypes.LogClimaDinamicoFrontEnd);
        public static void logBackGroundJobReporte(string data, StackTrace st, string a, int b, string c)
        {
            nlogClimaDinamicoBackGroundJobReporte.Error("/---------------------------------------------------------------------------------/");
            nlogClimaDinamicoBackGroundJobReporte.Info(data);
        }
        public static void logError(Exception aE, StackTrace st)
        {
            nlogClimaDinamico.Error("/---------------------------------------------------------------------------------/");
            nlogClimaDinamico.Error("Method: " + st.GetFrame(0).GetMethod().Name);
            nlogClimaDinamico.Error("Message: " + aE.Message);
            nlogClimaDinamico.Error("Exception: " + aE);
            nlogClimaDinamico.Error("Inner Exception: " + aE.InnerException);
            nlogClimaDinamico.Error("StackTrace: " + aE.StackTrace);
        }
        public static void logError(string message, StackTrace st)
        {
            nlogClimaDinamico.Error("/---------------------------------------------------------------------------------/");
            nlogClimaDinamico.Error("Method: " + st.GetFrame(0).GetMethod().Name);
            nlogClimaDinamico.Error(message);
        }
        public static void logData(string message)
        {
            nlogData.Info(message);
        }
        public static void logInfo(string message, StackTrace st) {
            nlogClimaDinamico.Info("/---------------------------------------------------------------------------------/");
            nlogClimaDinamico.Info("Method: " + st.GetFrame(0).GetMethod().Name);
            nlogClimaDinamico.Info(message);
        }
        public static void logInfoEmailSender(string message, ML.EstatusEmail aEstatus, int idUsuario)
        {
            nlogClimaDinamicoSMTP.Info("/---------------------------------------------------------------------------------/");
            nlogClimaDinamicoSMTP.Info(message);
            nlogClimaDinamicoSMTP.Info("Destinatario: " + aEstatus.Destinatario);
            nlogClimaDinamicoSMTP.Info("Encuesta: " + aEstatus.Encuesta.IdEncuesta);
            nlogClimaDinamicoSMTP.Info("Base de Datos: " + aEstatus.BaseDeDatos.IdBaseDeDatos);
            nlogClimaDinamicoSMTP.Info("Idusuario: " + idUsuario);
        }
        public static void logInfoEmailSender(string message, int? IdEncuesta, int? IdbaseDeDatos)
        {
            nlogClimaDinamicoSMTP.Info("/---------------------------------------------------------------------------------/");
            nlogClimaDinamicoSMTP.Info(message);
            nlogClimaDinamicoSMTP.Info("IdEncuesta: " + IdEncuesta);
            nlogClimaDinamicoSMTP.Info("IdBaseDeDatos: " + IdbaseDeDatos);
        }
        public static void logInfoEmailSender(Exception aE, ML.EstatusEmail aEstatus, int idUsuario)
        {
            nlogClimaDinamicoSMTP.Error("/---------------------------------------------------------------------------------/");
            nlogClimaDinamicoSMTP.Error("Exception: " + aE);
            nlogClimaDinamicoSMTP.Info("Destinatario: " + aEstatus.Destinatario);
            nlogClimaDinamicoSMTP.Info("Encuesta: " + aEstatus.Encuesta.IdEncuesta);
            nlogClimaDinamicoSMTP.Info("Base de Datos: " + aEstatus.BaseDeDatos.IdBaseDeDatos);
            nlogClimaDinamicoSMTP.Info("Idusuario: " + idUsuario);
            nlogClimaDinamicoSMTP.Error("Message: " + aE.Message);
            nlogClimaDinamicoSMTP.Error("Inner Exception: " + aE.InnerException);
            nlogClimaDinamicoSMTP.Error("StackTrace: " + aE.StackTrace);
        }
        public static bool logErrorFronEnd(ML.ClimaDinamico aClimaDinamico)
        {
            nlogClimaDinamicoFrontEnd.Error("/---------------------------------------------------------------------------------/");
            nlogClimaDinamicoFrontEnd.Error("IdEmpleado: " + aClimaDinamico.IdEmpleado);
            nlogClimaDinamicoFrontEnd.Error("IdEncuesta: " + aClimaDinamico.IdEncuesta);
            nlogClimaDinamicoFrontEnd.Error("Error: " + aClimaDinamico.ErrorMessage);
            return true;
        }
        public static void logAccesCMS(string aUsuario, string aNombre)
        {
            nlogAccess.Info("Usuario: " + aUsuario);
            nlogAccess.Info("Nombre: " + aNombre);
        }
    }
}
