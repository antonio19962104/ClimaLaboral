using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    /// <summary>
    /// Clase con los métodos del clima laboral dinámico
    /// </summary>
    public class ClimaDinamico
    {
        public static NLog.Logger nlogClimaDinamico = NLog.LogManager.GetLogger(ML.LogTypes.LogClimaDinamico);
        public static NLog.Logger nlogClimaDinamicoRespuestas = NLog.LogManager.GetLogger(ML.LogTypes.LogClimaDinamicoRespuestas);
        public static NLog.Logger nlogAutoguardadoClimaDinamico = NLog.LogManager.GetLogger(ML.LogTypes.LogAutoguardadoClimaDinamico);
        public static List<ML.Preguntas> getPreguntasByIdEncuesta(string aIdEncuesta)
        {
            var listPreguntas = new List<ML.Preguntas>();
            try
            {
                var aEncuesta = new ML.Encuesta() { IdEncuesta = Convert.ToInt32(aIdEncuesta) };
                var preguntas = new List<DL.Preguntas>();
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    // orden definido en la encuesta
                    var orden = (int?)context.Encuesta.Select(o => new { o.IdEncuesta, o.IdTipoOrden }).Where(o => o.IdEncuesta == aEncuesta.IdEncuesta).FirstOrDefault().IdTipoOrden == null ? 0 : (int)context.Encuesta.Select(o => new { o.IdEncuesta, o.IdTipoOrden }).Where(o => o.IdEncuesta == aEncuesta.IdEncuesta).First().IdTipoOrden;
                    // llamado de preguntas segun orden
                    if(orden == 1) // orden por competencia
                        preguntas = context.Preguntas.Select(o => o).Where(o => o.idEncuesta == aEncuesta.IdEncuesta && o.IdEstatus == 1).OrderBy(o => o.IdCompetencia).ToList();
                    if(orden == 2) // orden por pregunta padre (original)
                        preguntas = context.Preguntas.Select(o => o).Where(o => o.idEncuesta == aEncuesta.IdEncuesta && o.IdEstatus == 1).OrderBy(o => o.IdPreguntaPadre).ToList();
                    if(orden == 3) // orden personalizado
                        preguntas = context.Preguntas.Select(o => o).Where(o => o.idEncuesta == aEncuesta.IdEncuesta && o.IdEstatus == 1).OrderBy(o => o.IdOrden).ToList();
                    if (orden == 0) // orden por id de pregunta
                        preguntas = context.Preguntas.Select(o => o).Where(o => o.idEncuesta == aEncuesta.IdEncuesta).ToList();

                    int noReactivosEE = preguntas.Select(o => o).Where(o => o.IdEnfoque == 1 && o.Enfoque == "Enfoque Empresa" && o.IdTipoControl == 12).Count();
                    // quitar las repetidas den enfoque area
                    preguntas = preguntas.Select(o => o).Where(o => o.IdEnfoque == 1).ToList();
                    foreach (var item in preguntas)
                    {
                        ML.Preguntas pregunta = new ML.Preguntas();
                        pregunta.noReactivosEE = noReactivosEE;
                        pregunta.IdEncuesta = (int)item.idEncuesta;
                        pregunta.IdentificadorTipoControl = (int)item.IdTipoControl;
                        pregunta.IdTipoOrden = orden;
                        pregunta.IdPregunta = item.IdPregunta;
                        pregunta.IdPreguntaPadre = item.IdPreguntaPadre == null ? 0: (int)item.IdPreguntaPadre;
                        pregunta.Pregunta = item.Pregunta;
                        pregunta.Competencia = new ML.Competencia();
                        pregunta.Competencia.IdCompetencia = item.IdCompetencia;
                        pregunta.IdCompetencia = (int)item.IdCompetencia;
                        pregunta.Competencia.Nombre = context.Competencia.Select(o => o).Where(o => o.IdCompetencia == item.IdCompetencia).First().Nombre == null ? "" : context.Competencia.Select(o => o).Where(o => o.IdCompetencia == item.IdCompetencia).First().Nombre;
                        pregunta.IdOrden = (int?)item.IdOrden == null ? 0 : (int)item.IdOrden;
                        listPreguntas.Add(pregunta);
                    }
                    return listPreguntas;
                }
            }
            catch (Exception aE)
            {
                nlogClimaDinamico.Error("Method: " + new StackTrace().GetFrame(0).GetMethod().Name);
                nlogClimaDinamico.Error("Exception: " + aE);
                nlogClimaDinamico.Error("Inner Exception: " + aE.InnerException);
                nlogClimaDinamico.Error("StackTrace: " + aE.StackTrace);
                listPreguntas.Add(new ML.Preguntas { IdPregunta = 0 , Pregunta = aE.Message });
                return listPreguntas;
            }
        }
        public static string Autenticacion(ML.Empleado aEmpleado, string uid)
        {
            if (string.IsNullOrEmpty(uid) || uid == "null")
                return ML.ClimaDinamico.statusLogin.nullUID.ToString();
            var result = new ML.ClimaDinamico();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    //obtener id de encuesta mediante uid
                    var Encuesta = context.Encuesta.Select(o => o).Where(o => o.UID == uid).FirstOrDefault();
                    if (Encuesta == null)
                        return ML.ClimaDinamico.statusLogin.EncuestaNotFound.ToString();
                    int _idEncuesta = Encuesta.IdEncuesta;
                    // validar clave
                    var Empleado = context.Empleado.Select(o => new { o.IdEmpleado, o.ClaveAcceso }).Where(o => o.ClaveAcceso == aEmpleado.ClavesAcceso.ClaveAcceso).FirstOrDefault();
                    if (Empleado == null || Empleado.IdEmpleado <= 0)
                        return ML.ClimaDinamico.statusLogin.invalidkey.ToString();
                    // validar BD
                    var IdBaseDeDatos = from empleado in context.Empleado where empleado.IdEmpleado == Empleado.IdEmpleado join baseDeDatos in context.BasesDeDatos on empleado.IdBaseDeDatos equals baseDeDatos.IdBasesDeDatos select new { IdBaseDeDatos = empleado.IdBaseDeDatos }.IdBaseDeDatos;
                    int _idBase = (int)IdBaseDeDatos.First();
                    if (_idBase <= 0)
                        return ML.ClimaDinamico.statusLogin.BDNotFound.ToString();
                    // validar periodo
                    var periodoAplicaciona = context.ConfigClimaLab.Select(o => new { o.IdBaseDeDatos, o.FechaInicio, o.FechaFin, o.IdEncuesta }).Where(o => o.IdBaseDeDatos == _idBase && o.IdEncuesta == _idEncuesta).ToList();
                    var periodoAplicacion = periodoAplicaciona.Last();
                    if (periodoAplicacion == null)
                        return ML.ClimaDinamico.statusLogin.notFoundPeriodosApp.ToString();

                    if (DateTime.Now < periodoAplicacion.FechaInicio)
                        return ML.ClimaDinamico.statusLogin.noStart.ToString();
                    if (DateTime.Now > periodoAplicacion.FechaFin)
                        return ML.ClimaDinamico.statusLogin.AppEnd.ToString();

                    // validar estatus de encuesta
                    var estatusEncuesta = context.EstatusEncuesta.Select(o => o).Where(o => o.IdEmpleado == Empleado.IdEmpleado && o.IdEncuesta == periodoAplicacion.IdEncuesta).FirstOrDefault();
                    if (estatusEncuesta == null)
                    {
                        changeEstatusEncuestaClima((int)periodoAplicacion.IdEncuesta, Empleado.IdEmpleado, _idBase, 2);
                        nlogClimaDinamico.Info("No se encontro el estatus de encuesta del usuario: " + Empleado + " para la encuesta: " + periodoAplicacion.IdEncuesta + ". Ya fué agregado");
                    }

                    if (estatusEncuesta != null)
                    {
                        if (estatusEncuesta.Estatus == ML.ClimaDinamico.terminada)
                            return ML.ClimaDinamico.statusLogin.encuestaRealizada.ToString();

                        if (estatusEncuesta.Estatus == ML.ClimaDinamico.noIniciada)
                        {
                            changeEstatusEncuestaClima((int)periodoAplicacion.IdEncuesta, Empleado.IdEmpleado, _idBase, 2);
                        }
                    }
                    /*
                     * JAMG 30/04/2021
                     * se da el acceso a la encuesta a las personas con estatus de no iniciada y en proceso
                    */
                    return ML.ClimaDinamico.statusLogin.success.ToString() + "_" + Empleado.IdEmpleado + "_" + periodoAplicacion.IdEncuesta + "_" + _idBase;
                }
            }
            catch (Exception aE)
            {
                nlogClimaDinamico.Error("Method: " + new StackTrace().GetFrame(0).GetMethod().Name);
                nlogClimaDinamico.Error("Exception: " + aE);
                nlogClimaDinamico.Error("Inner Exception: " + aE.InnerException);
                nlogClimaDinamico.Error("StackTrace: " + aE.StackTrace);
                nlogClimaDinamico.Error("Params: " + aEmpleado.ClavesAcceso.ClaveAcceso);
                return ML.ClimaDinamico.statusLogin.Exception.ToString();
            }
        }
        public static List<ML.ClimaDinamico> getPreguntasFromEncuesta(string aIdEmpleado, string aIdEncuesta)
        {
            var list = new List<ML.ClimaDinamico>();
            try
            {
                int _idEmpleado = Convert.ToInt32(aIdEmpleado);
                int _idEncuesta = Convert.ToInt32(aIdEncuesta);
                using (DL.RH_DesEntities context =new DL.RH_DesEntities())
                {
                    var respuestas = context.EmpleadoRespuestas.Select(o => new { o.IdEmpleadoRespuestas, o.IdEmpleado, o.IdEncuesta, o.IdPregunta, o.IdEnfoque, o.RespuestaEmpleado }).Where(o => o.IdEmpleado == _idEmpleado && o.IdEncuesta == _idEncuesta).ToList();
                    if (respuestas != null)
                    {
                        foreach (var item in respuestas)
                        {
                            ML.ClimaDinamico model = new ML.ClimaDinamico();
                            model._idEmpleadoRespuestas = item.IdEmpleadoRespuestas;
                            model._idEmpleado = (int)item.IdEmpleado;
                            model._idEncuesta = (int)item.IdEncuesta;
                            model._idPregunta = (int)item.IdPregunta;
                            model._idEnfoque = (int)item.IdEnfoque;
                            model._respuestaEmpleado = item.RespuestaEmpleado;
                            model._idTipoControl = (int?)context.Preguntas.Select(o => o).Where(o => o.IdPregunta == item.IdPregunta).FirstOrDefault().IdTipoControl == null ? 0 : (int)context.Preguntas.Select(o => o).Where(o => o.IdPregunta == item.IdPregunta).FirstOrDefault().IdTipoControl;
                            list.Add(model);
                        }
                        return list;
                    }
                    else
                    {
                        list = new List<ML.ClimaDinamico>();
                        list.Add(new ML.ClimaDinamico { _idEmpleadoRespuestas = 0, _idEncuesta = 0, _idEmpleado = 0, _idPregunta = 0, _idEnfoque = 0, _respuestaEmpleado = "", hasRespuestas = false });
                        return list;
                    }
                }
            }
            catch (Exception aE)
            {
                nlogClimaDinamico.Error("Method: " + new StackTrace().GetFrame(0).GetMethod().Name);
                nlogClimaDinamico.Error("Exception: " + aE);
                nlogClimaDinamico.Error("Inner Exception: " + aE.InnerException);
                nlogClimaDinamico.Error("StackTrace: " + aE.StackTrace);
                nlogClimaDinamico.Error("IdEmpleado: " + aIdEmpleado);
                nlogClimaDinamico.Error("IdEncuesta: " + aIdEncuesta);
                list = new List<ML.ClimaDinamico>();
                list.Add(new ML.ClimaDinamico { _idEmpleadoRespuestas = 0, _idEncuesta = 0, _idEmpleado = 0, _idPregunta = 0, _idEnfoque = 0, _respuestaEmpleado = "Error", hasRespuestas = true });
                return list;
            }
        }
        public static ML.ClimaDinamico.statusGuardado SaveRespuesta(List<ML.ClimaDinamico> aListRespuestas)
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    int _idEncuesta = Convert.ToInt32(aListRespuestas[0].IdEncuesta);
                    int _idBaseDeDatos = Convert.ToInt32(aListRespuestas[0].IdBaseDeDatos);
                    var periodoApp = context.ConfigClimaLab.Select(o => o).Where(o => o.IdEncuesta == _idEncuesta && o.IdBaseDeDatos == _idBaseDeDatos).FirstOrDefault() == null ? new DL.ConfigClimaLab { PeriodoAplicacion = 0 } : context.ConfigClimaLab.Select(o => o).Where(o => o.IdEncuesta == _idEncuesta && o.IdBaseDeDatos == _idBaseDeDatos).FirstOrDefault();
                    foreach (var item in aListRespuestas)
                    {
                        // verificar si ya existe la respuesta a la pregunta
                        var exists = context.EmpleadoRespuestas.Select(o => o).Where(o => o.IdEmpleado == item.Empleado.IdEmpleado && o.IdEncuesta == item.Encuesta.IdEncuesta && o.IdPregunta == item.Preguntas.IdPregunta && o.IdEnfoque == item.IdEnfoque).FirstOrDefault();
                        if (exists == null) // insert
                        {
                            context.EmpleadoRespuestas.Add(new DL.EmpleadoRespuestas() {
                                IdPregunta = item.Preguntas.IdPregunta,
                                RespuestaEmpleado = item.RespuestaEmpleado,
                                IdRespuesta = item.Respuestas.IdRespuesta,
                                IdEnfoque = item.IdEnfoque,
                                IdEmpleado = item.Empleado.IdEmpleado,
                                FechaHoraCreacion = DateTime.Now, UsuarioCreacion = "Clima Dinamico", ProgramaCreacion = "Clima Dinamico", Anio = periodoApp.PeriodoAplicacion, IdEncuesta = item.Encuesta.IdEncuesta
                            });
                            context.SaveChanges();
                        }
                        if (exists != null) // update
                        {
                            exists.IdPregunta = item.Preguntas.IdPregunta;
                            exists.RespuestaEmpleado = item.RespuestaEmpleado;
                            exists.IdEnfoque = item.IdEnfoque;
                            exists.IdRespuesta = item.Respuestas.IdRespuesta;
                            exists.Anio = periodoApp.PeriodoAplicacion;
                            exists.IdEmpleado = item.Empleado.IdEmpleado;
                            exists.FechaHoraModificacion = DateTime.Now;
                            exists.UsuarioModificacion = "Clima Dinamico";
                            exists.ProgramaModificacion = "Clima Dinamico";
                            context.SaveChanges();
                        }
                        // guardar respuestas en log
                        nlogClimaDinamicoRespuestas.Info("Empleado: " + item.Empleado.IdEmpleado);
                        nlogClimaDinamicoRespuestas.Info("IdPregunta: " + item.Preguntas.IdPregunta);
                        nlogClimaDinamicoRespuestas.Info("Respuesta: " + item.RespuestaEmpleado);
                        nlogClimaDinamicoRespuestas.Info("Enfoque: " + item.IdEnfoque);
                    }
                    return ML.ClimaDinamico.statusGuardado.success;
                }
            }
            catch (Exception aE)
            {
                nlogClimaDinamico.Error("Method: " + new StackTrace().GetFrame(0).GetMethod().Name);
                nlogClimaDinamico.Error("Exception: " + aE);
                nlogClimaDinamico.Error("Inner Exception: " + aE.InnerException);
                nlogClimaDinamico.Error("StackTrace: " + aE.StackTrace);
                nlogClimaDinamico.Error("IdEmpleado: " + aListRespuestas[0].Empleado.IdEmpleado);
                return ML.ClimaDinamico.statusGuardado.error;
            }
        }
        public static ML.ClimaDinamico.statusGuardado AutoSave(ML.ClimaDinamico aClimaDinamico)
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    int _idEncuesta = Convert.ToInt32(aClimaDinamico.IdEncuesta);
                    int _idBaseDeDatos = Convert.ToInt32(aClimaDinamico.IdBaseDeDatos);
                    var periodoApp = context.ConfigClimaLab.Select(o => o).Where(o => o.IdEncuesta == _idEncuesta && o.IdBaseDeDatos == _idBaseDeDatos).FirstOrDefault() == null ? new DL.ConfigClimaLab { PeriodoAplicacion = 0 } : context.ConfigClimaLab.Select(o => o).Where(o => o.IdEncuesta == _idEncuesta && o.IdBaseDeDatos == _idBaseDeDatos).FirstOrDefault();
                    // verificar si ya existe la respuesta a la pregunta
                    var exists = context.EmpleadoRespuestas.Select(o => o).Where(o => o.IdEmpleado == aClimaDinamico.Empleado.IdEmpleado && o.IdEncuesta == aClimaDinamico.Encuesta.IdEncuesta && o.IdPregunta == aClimaDinamico.Preguntas.IdPregunta && o.IdEnfoque == aClimaDinamico.IdEnfoque).FirstOrDefault();
                    if (exists == null) // insert
                    {
                        context.EmpleadoRespuestas.Add(new DL.EmpleadoRespuestas()
                        {
                            IdPregunta = aClimaDinamico.Preguntas.IdPregunta,
                            RespuestaEmpleado = aClimaDinamico.RespuestaEmpleado,
                            IdRespuesta = aClimaDinamico.Respuestas.IdRespuesta,
                            IdEnfoque = aClimaDinamico.IdEnfoque,
                            IdEmpleado = aClimaDinamico.Empleado.IdEmpleado,
                            FechaHoraCreacion = DateTime.Now,
                            UsuarioCreacion = "Clima Dinamico",
                            ProgramaCreacion = "Clima Dinamico",
                            Anio = periodoApp.PeriodoAplicacion,
                            IdEncuesta = aClimaDinamico.Encuesta.IdEncuesta
                        });
                        context.SaveChanges();
                    }
                    if (exists != null) // update
                    {
                        exists.IdPregunta = aClimaDinamico.Preguntas.IdPregunta;
                        exists.Anio = periodoApp.PeriodoAplicacion;
                        exists.RespuestaEmpleado = aClimaDinamico.RespuestaEmpleado;
                        exists.IdEnfoque = aClimaDinamico.IdEnfoque;
                        exists.IdRespuesta = aClimaDinamico.Respuestas.IdRespuesta;
                        exists.IdEmpleado = aClimaDinamico.Empleado.IdEmpleado;
                        exists.FechaHoraModificacion = DateTime.Now;
                        exists.UsuarioModificacion = "Clima Dinamico";
                        exists.ProgramaModificacion = "Clima Dinamico";
                        context.SaveChanges();
                    }
                    // guardar respuestas en log
                    nlogAutoguardadoClimaDinamico.Info("Empleado: " + aClimaDinamico.Empleado.IdEmpleado);
                    nlogAutoguardadoClimaDinamico.Info("IdPregunta: " + aClimaDinamico.Preguntas.IdPregunta);
                    nlogAutoguardadoClimaDinamico.Info("Respuesta: " + aClimaDinamico.RespuestaEmpleado);
                    nlogAutoguardadoClimaDinamico.Info("Enfoque: " + aClimaDinamico.IdEnfoque);
                    
                    return ML.ClimaDinamico.statusGuardado.success;
                }
            }
            catch (Exception aE)
            {
                nlogClimaDinamico.Error("Method: " + new StackTrace().GetFrame(0).GetMethod().Name);
                nlogClimaDinamico.Error("Exception: " + aE);
                nlogClimaDinamico.Error("Inner Exception: " + aE.InnerException);
                nlogClimaDinamico.Error("StackTrace: " + aE.StackTrace);
                nlogClimaDinamico.Error("IdEmpleado: " + aClimaDinamico.Empleado.IdEmpleado);
                return ML.ClimaDinamico.statusGuardado.error;
            }
        }
        public static ML.ClimaDinamico.statusGuardado changeEstatusEncuestaClima(string aIdEncuesta, string aIdUsuario, string aIdBaseDeDatos, int aStatus)
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    int _idEncuesta = Convert.ToInt32(aIdEncuesta);
                    int _idBaseDeDatos = Convert.ToInt32(aIdBaseDeDatos);
                    int _idUsuario = Convert.ToInt32(aIdUsuario);
                    var periodoApp = context.ConfigClimaLab.Select(o => o).Where(o => o.IdEncuesta == _idEncuesta && o.IdBaseDeDatos == _idBaseDeDatos).FirstOrDefault() == null ? new DL.ConfigClimaLab { PeriodoAplicacion = 0 } : context.ConfigClimaLab.Select(o => o).Where(o => o.IdEncuesta == _idEncuesta && o.IdBaseDeDatos == _idBaseDeDatos).FirstOrDefault();
                    var data = context.EstatusEncuesta.Select(o => o).Where(o => o.IdEmpleado == _idUsuario && o.IdEncuesta == _idEncuesta).FirstOrDefault();
                    data.Anio = periodoApp.PeriodoAplicacion;
                    if (data != null)
                    {
                        switch (aStatus)
                        {
                            case 1:
                                data.Estatus = "No comenzada";
                                break;
                            case 2:
                                data.Estatus = "En proceso";
                                break;
                            case 3:
                                data.Estatus = "Terminada";
                                break;
                        }
                        data.FechaHoraModificacion = DateTime.Now;
                        data.UsuarioModificacion = "Clima Dinamico";
                        data.ProgramaModificacion = "Clima Dinamico";
                        context.SaveChanges();
                        nlogClimaDinamicoRespuestas.Info("Se cambió el estatus de encuesta exitosamente");
                        nlogClimaDinamicoRespuestas.Info("IdEmpleado: " + aIdUsuario);
                        nlogClimaDinamicoRespuestas.Info("IdEncuesta: " + aIdEncuesta);
                        nlogClimaDinamicoRespuestas.Info("Estatus: " + data.Estatus);
                        return ML.ClimaDinamico.statusGuardado.success;
                    }
                }
            }
            catch (Exception aE)
            {
                nlogClimaDinamico.Error("Method: " + new StackTrace().GetFrame(0).GetMethod().Name);
                nlogClimaDinamico.Error("Exception: " + aE);
                nlogClimaDinamico.Error("Inner Exception: " + aE.InnerException);
                nlogClimaDinamico.Error("StackTrace: " + aE.StackTrace);
                nlogClimaDinamicoRespuestas.Error("IdEmpleado: " + (aIdUsuario == null ? "0" : aIdUsuario));
                nlogClimaDinamicoRespuestas.Error("IdEncuesta: " + aIdEncuesta == null ? "0" : aIdEncuesta);
                nlogClimaDinamicoRespuestas.Error("Estatus: " + aStatus == null ? 0 : aStatus);
                return ML.ClimaDinamico.statusGuardado.error;
            }
            return ML.ClimaDinamico.statusGuardado.success;
        }
        public static ML.ClimaDinamico.statusGuardado changeEstatusEncuestaClima(int aIdEncuesta, int aIdUsuario, int aIdBaseDeDatos, int aStatus)
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    int _idEmpleado = Convert.ToInt32(aIdUsuario);
                    int _idEncuesta = Convert.ToInt32(aIdEncuesta);
                    var periodoApp = context.ConfigClimaLab.Select(o => o).Where(o => o.IdEncuesta == aIdEncuesta && o.IdBaseDeDatos == aIdBaseDeDatos).FirstOrDefault() == null ? new DL.ConfigClimaLab { PeriodoAplicacion = 0 } : context.ConfigClimaLab.Select(o => o).Where(o => o.IdEncuesta == aIdEncuesta && o.IdBaseDeDatos == aIdBaseDeDatos).FirstOrDefault();
                    var data = context.EstatusEncuesta.Select(o => o).Where(o => o.IdEmpleado == _idEmpleado && o.IdEncuesta == _idEncuesta).FirstOrDefault();
                    if (data != null)
                    {
                        data.Anio = periodoApp.PeriodoAplicacion;
                        data.FechaHoraModificacion = DateTime.Now;
                        data.UsuarioModificacion = "Clima Dinamico";
                        data.ProgramaModificacion = "Clima Dinamico";
                        switch (aStatus)
                        {
                            case 1:
                                data.Estatus = "No comenzada";
                                break;
                            case 2:
                                data.Estatus = "En proceso";
                                break;
                            case 3:
                                data.Estatus = "Terminada";
                                break;
                        }
                        data.FechaHoraModificacion = DateTime.Now;
                        data.UsuarioModificacion = "Clima Dinamico";
                        data.ProgramaModificacion = "Clima Dinamico";
                        context.SaveChanges();
                        nlogClimaDinamicoRespuestas.Info("Se cambió el estatus de encuesta exitosamente");
                        nlogClimaDinamicoRespuestas.Info("IdEmpleado: " + aIdUsuario);
                        nlogClimaDinamicoRespuestas.Info("IdEncuesta: " + aIdEncuesta);
                        nlogClimaDinamicoRespuestas.Info("Estatus: " + data.Estatus);
                        return ML.ClimaDinamico.statusGuardado.success;
                    }
                    else
                    {
                        var status = new DL.EstatusEncuesta()
                        {
                            Estatus = "En proceso",
                            IdEmpleado = aIdUsuario,
                            IdEncuesta = aIdEncuesta,
                            Anio = periodoApp.PeriodoAplicacion,
                            FechaHoraCreacion = DateTime.Now,
                            UsuarioCreacion = "Clima Dinamico",
                            ProgramaCreacion = "Clima Dinamico"
                        };
                        context.EstatusEncuesta.Add(status);
                        context.SaveChanges();
                        nlogClimaDinamicoRespuestas.Info("Se insertó el estatus de encuesta exitosamente");
                        nlogClimaDinamicoRespuestas.Info("IdEmpleado: " + status.IdEmpleado);
                        nlogClimaDinamicoRespuestas.Info("IdEncuesta: " + status.IdEncuesta);
                        nlogClimaDinamicoRespuestas.Info("Estatus: " + status.Estatus);
                        return ML.ClimaDinamico.statusGuardado.success;
                    }
                }
            }
            catch (Exception aE)
            {
                nlogClimaDinamico.Error("Method: " + new StackTrace().GetFrame(0).GetMethod().Name);
                nlogClimaDinamico.Error("Exception: " + aE);
                nlogClimaDinamico.Error("Inner Exception: " + aE.InnerException);
                nlogClimaDinamico.Error("StackTrace: " + aE.StackTrace);
                nlogClimaDinamicoRespuestas.Error("IdEmpleado: " + aIdUsuario == null ? 0 : aIdUsuario);
                nlogClimaDinamicoRespuestas.Error("IdEncuesta: " + aIdEncuesta == null ? 0 : aIdEncuesta);
                nlogClimaDinamicoRespuestas.Error("Estatus: " + aStatus == null ? 0 : aStatus);
                return ML.ClimaDinamico.statusGuardado.error;
            }
        }
        public static ML.ClimaDinamico getHtmlIntro(string aIdEncuesta)
        {
            try
            {
                int _idEncuesta = Convert.ToInt32(aIdEncuesta);
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var data = context.Encuesta.Select(o => o).Where(o => o.IdEncuesta == _idEncuesta).FirstOrDefault().CodeHTML == null ? ML.ClimaDinamico.defaultHtmlIntro : context.Encuesta.Select(o => o).Where(o => o.IdEncuesta == _idEncuesta).FirstOrDefault().CodeHTML;
                    return new ML.ClimaDinamico() { htmlCodeIntroduccion = data };
                }
            }
            catch (Exception aE)
            {
                nlogClimaDinamico.Error("Method: " + new StackTrace().GetFrame(0).GetMethod().Name);
                nlogClimaDinamico.Error("Exception: " + aE);
                nlogClimaDinamico.Error("Inner Exception: " + aE.InnerException);
                nlogClimaDinamico.Error("StackTrace: " + aE.StackTrace);
                return new ML.ClimaDinamico() { htmlCodeIntroduccion = "<p style='padding-top: 100px'>Ocurrió un error al generar el contenido</p><p>" + aE.Message + "</p>" };
            }
        }
        public static ML.ClimaDinamico getHtmlInstrucciones(string aIdEncuesta)
        {
            try
            {
                int _idEncuesta = Convert.ToInt32(aIdEncuesta);
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var data = context.Encuesta.Select(o => o).Where(o => o.IdEncuesta == _idEncuesta).FirstOrDefault().Instruccion == null ? ML.ClimaDinamico.defaultHtmlInstrucciones : context.Encuesta.Select(o => o).Where(o => o.IdEncuesta == _idEncuesta).FirstOrDefault().Instruccion;
                    return new ML.ClimaDinamico() { htmlCodeInstrucciones = data };
                }
            }
            catch (Exception aE)
            {
                nlogClimaDinamico.Error("Method: " + new StackTrace().GetFrame(0).GetMethod().Name);
                nlogClimaDinamico.Error("Exception: " + aE);
                nlogClimaDinamico.Error("Inner Exception: " + aE.InnerException);
                nlogClimaDinamico.Error("StackTrace: " + aE.StackTrace);
                return new ML.ClimaDinamico() { htmlCodeInstrucciones = "<p style='padding-top: 100px'>Ocurrió un error al generar el contenido</p><p>" + aE.Message + "</p>" };
            }
        }
        public static ML.ClimaDinamico getHtmlAgradecimiento(string aIdEncuesta)
        {
            try
            {
                int _idEncuesta = Convert.ToInt32(aIdEncuesta);
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var data = context.Encuesta.Select(o => o).Where(o => o.IdEncuesta == _idEncuesta).FirstOrDefault().Agradecimiento == null ? ML.ClimaDinamico.defaultHtmlGracias : context.Encuesta.Select(o => o).Where(o => o.IdEncuesta == _idEncuesta).FirstOrDefault().Agradecimiento;
                    return new ML.ClimaDinamico() { htmlAgradecimiento = data };
                }
            }
            catch (Exception aE)
            {
                nlogClimaDinamico.Error("Method: " + new StackTrace().GetFrame(0).GetMethod().Name);
                nlogClimaDinamico.Error("Exception: " + aE);
                nlogClimaDinamico.Error("Inner Exception: " + aE.InnerException);
                nlogClimaDinamico.Error("StackTrace: " + aE.StackTrace);
                return new ML.ClimaDinamico() { htmlAgradecimiento = "<p style='padding-top: 100px'>Ocurrió un error al generar el contenido</p>" };
            }
        }
        public static List<ML.Antiguedad> getRangoAntiguedad(string aIdEncuesta)
        {
            var list = new List<ML.Antiguedad>();
            try
            {
                int _idEncuesta = Convert.ToInt32(aIdEncuesta);
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Antiguedad.Select(o => o).ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Antiguedad antiguedad = new ML.Antiguedad();
                            antiguedad.IdAntiguedad = item.IdAndtiguedad;
                            antiguedad.Descripcion = item.Descripcion;
                            list.Add(antiguedad);
                        }
                    }
                }
            }
            catch (Exception aE)
            {
                nlogClimaDinamico.Error("Method: " + new StackTrace().GetFrame(0).GetMethod().Name);
                nlogClimaDinamico.Error("Exception: " + aE);
                nlogClimaDinamico.Error("Inner Exception: " + aE.InnerException);
                nlogClimaDinamico.Error("StackTrace: " + aE.StackTrace);
                return new List<ML.Antiguedad>();
            }
            return list;
        }

        public static List<ML.RangoEdad> getRangoEdad(string aIdEncuesta)
        {
            var list = new List<ML.RangoEdad>();
            try
            {
                int _idEncuesta = Convert.ToInt32(aIdEncuesta);
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.RangoEdad.Select(o => o).ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.RangoEdad rangoEdad = new ML.RangoEdad();
                            rangoEdad.IdRangoEdad = item.IdRangoEdad;
                            rangoEdad.Descripcion = item.Descripcion;
                            list.Add(rangoEdad);
                        }
                    }
                }
            }
            catch (Exception aE)
            {
                nlogClimaDinamico.Error("Method: " + new StackTrace().GetFrame(0).GetMethod().Name);
                nlogClimaDinamico.Error("Exception: " + aE);
                nlogClimaDinamico.Error("Inner Exception: " + aE.InnerException);
                nlogClimaDinamico.Error("StackTrace: " + aE.StackTrace);
                return new List<ML.RangoEdad>();
            }
            return list;
        }
        public static List<ML.CondicionTrabajo> getCondicionTrabajo(string aIdEncuesta)
        {
            var list = new List<ML.CondicionTrabajo>();
            try
            {
                int _idEncuesta = Convert.ToInt32(aIdEncuesta);
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.CondicionTrabajo.Select(o => o).ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.CondicionTrabajo condicionTrabajo = new ML.CondicionTrabajo();
                            condicionTrabajo.IdCondicionTrabajo = item.IdCondicionTrabajo;
                            condicionTrabajo.Descripcion = item.Descripcion;
                            list.Add(condicionTrabajo);
                        }
                    }
                }
            }
            catch (Exception aE)
            {
                nlogClimaDinamico.Error("Method: " + new StackTrace().GetFrame(0).GetMethod().Name);
                nlogClimaDinamico.Error("Exception: " + aE);
                nlogClimaDinamico.Error("Inner Exception: " + aE.InnerException);
                nlogClimaDinamico.Error("StackTrace: " + aE.StackTrace);
                return new List<ML.CondicionTrabajo>();
            }
            return list;
        }
        public static List<ML.Perfil> getPerfiles(string aIdEncuesta)
        {
            var list = new List<ML.Perfil>();
            try
            {
                int _idEncuesta = Convert.ToInt32(aIdEncuesta);
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Perfil.Select(o => o).ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Perfil perfil = new ML.Perfil();
                            perfil.IdPerfil = item.IdPerfil;
                            perfil.Descripcion = item.Descripcion;
                            list.Add(perfil);
                        }
                    }
                }
            }
            catch (Exception aE)
            {
                nlogClimaDinamico.Error("Method: " + new StackTrace().GetFrame(0).GetMethod().Name);
                nlogClimaDinamico.Error("Exception: " + aE);
                nlogClimaDinamico.Error("Inner Exception: " + aE.InnerException);
                nlogClimaDinamico.Error("StackTrace: " + aE.StackTrace);
                return new List<ML.Perfil>();
            }
            return list;
        }
        public static List<ML.GradoAcademico> getGradoAcademico(string aIdEncuesta)
        {
            var list = new List<ML.GradoAcademico>();
            try
            {
                int _idEncuesta = Convert.ToInt32(aIdEncuesta);
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.GradoAcademico.Select(o => o).ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.GradoAcademico gradoAcademico = new ML.GradoAcademico();
                            gradoAcademico.IdGradoAcademico = item.IdGradoAcademico;
                            gradoAcademico.Descripcion = item.Descripcion;
                            list.Add(gradoAcademico);
                        }
                    }
                }
            }
            catch (Exception aE)
            {
                nlogClimaDinamico.Error("Method: " + new StackTrace().GetFrame(0).GetMethod().Name);
                nlogClimaDinamico.Error("Exception: " + aE);
                nlogClimaDinamico.Error("Inner Exception: " + aE.InnerException);
                nlogClimaDinamico.Error("StackTrace: " + aE.StackTrace);
                return new List<ML.GradoAcademico>();
            }
            return list;
        }
        public static List<ML.CompanyCategoria> getUnidadNegocio(string aIdEncuesta)
        {
            var list = new List<ML.CompanyCategoria>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.CompanyCategoria.Select(o => o).ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.CompanyCategoria companyCategoria = new ML.CompanyCategoria();
                            companyCategoria.IdCompanyCategoria = item.IdCompanyCategoria;
                            companyCategoria.Descripcion = item.Descripcion;
                            list.Add(companyCategoria);
                        }
                    }
                }
            }
            catch (Exception aE)
            {
                nlogClimaDinamico.Error("Method: " + new StackTrace().GetFrame(0).GetMethod().Name);
                nlogClimaDinamico.Error("Exception: " + aE);
                nlogClimaDinamico.Error("Inner Exception: " + aE.InnerException);
                nlogClimaDinamico.Error("StackTrace: " + aE.StackTrace);
                return new List<ML.CompanyCategoria>();
            }
            return list;
        }
        // tipo de entidad para el reporte es 2
        public static List<ML.Company> getCompaniesByIdCompanyCategoria(string aIdCompanyCategoria)
        {
            var list = new List<ML.Company>();
            try
            {
                int _idCompanyCategoria = Convert.ToInt32(aIdCompanyCategoria);
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Company.Select(o => new { o.CompanyId, o.CompanyName, o.IdCompanyCategoria, o.Tipo }).Where(o => o.IdCompanyCategoria == _idCompanyCategoria && o.Tipo == 2).ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Company company = new ML.Company();
                            company.CompanyId = item.CompanyId;
                            company.CompanyName = item.CompanyName;
                            list.Add(company);
                        }
                    }
                }
            }
            catch (Exception aE)
            {
                nlogClimaDinamico.Error("Method: " + new StackTrace().GetFrame(0).GetMethod().Name);
                nlogClimaDinamico.Error("Exception: " + aE);
                nlogClimaDinamico.Error("Inner Exception: " + aE.InnerException);
                nlogClimaDinamico.Error("StackTrace: " + aE.StackTrace);
                return new List<ML.Company>();
            }
            return list;
        }
        public static ML.ClimaDinamico.statusLogin envioMasivoEmail(ML.ClimaDinamico aClimaDinamico)
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var Encuesta = context.Encuesta.Where(o => o.IdEncuesta == aClimaDinamico.IdEncuesta).FirstOrDefault();
                    if (Encuesta == null)
                        return ML.ClimaDinamico.statusLogin.EncuestaNotFound;
                    var BaseDeDatos = context.ConfigClimaLab.Where(o => o.IdEncuesta == Encuesta.IdEncuesta).FirstOrDefault();
                    if (BaseDeDatos == null)
                        return ML.ClimaDinamico.statusLogin.BDNotFound;
                    var Usuarios = context.Empleado.Where(o => o.IdBaseDeDatos == BaseDeDatos.IdBaseDeDatos).ToList();
                    string Redirect = aClimaDinamico.currentUrl + "/ClimaDinamico/Login/?uid=" + Encuesta.UID;
                    if (Usuarios != null)
                    {
                        if (Usuarios.Count > 0)
                        {
                            foreach (DL.Empleado user in Usuarios)
                            {
                                if (user.Correo == null)
                                {
                                    string msg = "El usuario: " + user.Nombre + " " + user.ApellidoPaterno + " " + user.ApellidoMaterno + " no cuenta con un email registrado";
                                    BL.NLogGeneratorFile.logError(msg, new StackTrace());
                                }
                                if (!isValidEmail(user.Correo))
                                {
                                    string msg = "El usuario: " + user.Nombre + " " + user.ApellidoPaterno + " " + user.ApellidoMaterno + " no cuenta con un email válido";
                                    BL.NLogGeneratorFile.logError(msg, new StackTrace());
                                }
                                if (aClimaDinamico.TipoMensaje == "default" && user.Correo != null && isValidEmail(user.Correo))
                                {
                                    string FullName = user.Nombre + " " + user.ApellidoPaterno + " " + user.ApellidoMaterno;
                                    var body =
                                    "<p style='font-weight:bold;'>Que tal " + FullName + "</p>" +
                                    "<p>Has sido dado de alta dentro del portal de encuestas <b>Diagnostic4U</b></p>" +
                                    "<p>Has sido elegido para contestar la encuesta: " + Encuesta.Nombre + " la cual estará activa desde el " + Encuesta.FechaInicio + " hasta el " + Encuesta.FechaFin + "</p>" +
                                    "<p>Tu clave de acceso es la siguiente: <b>" + user.ClaveAcceso + "</b></p>" +
                                    "<p>Accede entrando a: <a href='" + Redirect + "'><b>Diagnostic4U</b></a></p>" +
                                    "<p><img src=http://www.diagnostic4u.com/img/logo.png'></p></ br>";
                                    var message = new MailMessage();
                                    message.To.Add(new MailAddress(user.Correo));
                                    message.Subject = "Notificación Diagnostic4U";
                                    message.Body = string.Format(body, "DIAGNOSTIC4U", "jamurillo@grupoautofin.com", "Aqui se envian  las claves de acceso al portal");
                                    message.IsBodyHtml = true;
                                    ML.EstatusEmail estatusEmail = new ML.EstatusEmail();
                                    estatusEmail.Mensaje = body;
                                    estatusEmail.Destinatario = user.Correo;
                                    estatusEmail.BaseDeDatos = new ML.BasesDeDatos();
                                    estatusEmail.BaseDeDatos.IdBaseDeDatos = BaseDeDatos.IdBaseDeDatos;
                                    estatusEmail.Encuesta = new ML.Encuesta();
                                    estatusEmail.Encuesta.IdEncuesta = Encuesta.IdEncuesta;
                                    using (var smtp = new SmtpClient())
                                    {
                                        try
                                        {
                                            BL.Encuesta.AddToEstatusEmail(estatusEmail);
                                            smtp.Send(message);
                                            BL.Encuesta.UpdateFlagEmailToSuccess(estatusEmail, Encuesta.IdEncuesta, (int)BaseDeDatos.IdBaseDeDatos);
                                            BL.NLogGeneratorFile.logInfoEmailSender("Email enviado correctamente", estatusEmail, user.IdEmpleado);
                                        }
                                        catch (SmtpException ex)
                                        {
                                            if (ex is Exception || ex is SmtpException)
                                            {
                                                BL.NLogGeneratorFile.logInfoEmailSender(ex, estatusEmail, user.IdEmpleado);
                                                BL.Encuesta.UpdateFlagEmailToError(estatusEmail, ex, Encuesta.IdEncuesta, (int)BaseDeDatos.IdBaseDeDatos);
                                            }
                                        }
                                        finally
                                        {
                                            smtp.Dispose();
                                        }
                                    }
                                }
                                else if (aClimaDinamico.TipoMensaje != "default" && user.Correo != null)
                                {
                                    string FullName = user.Nombre + " " + user.ApellidoPaterno + " " + user.ApellidoMaterno;
                                    string mensaje = aClimaDinamico.Mensaje;
                                    mensaje = ReplaceDatosMensaje(mensaje, "*NombreUsuario*", FullName);
                                    mensaje = ReplaceDatosMensaje(mensaje, "*NombreEncuesta*", Encuesta.Nombre);
                                    mensaje = ReplaceDatosMensaje(mensaje, "*FechaInicio*", Convert.ToString(Encuesta.FechaInicio));
                                    mensaje = ReplaceDatosMensaje(mensaje, "*FechaFin*", Convert.ToString(Encuesta.FechaFin));
                                    mensaje = ReplaceDatosMensaje(mensaje, "*LinkEncuesta*", "<a style='font-weight:bold' href='" + Redirect + "'>Diagnostic4U</a>");
                                    mensaje = ReplaceDatosMensaje(mensaje, "*ClaveAcceso*", user.ClaveAcceso);
                                    var body = mensaje;
                                    var message = new MailMessage();
                                    message.To.Add(new MailAddress(user.Correo));
                                    message.Subject = "Notificación Diagnostic4U";
                                    message.Body = string.Format(body, "DIAGNOSTIC4U", "jamurillo@grupoautofin.com", "Aqui se envian las claves de acceso al portal");
                                    message.IsBodyHtml = true;

                                    ML.EstatusEmail estatusEmail = new ML.EstatusEmail();
                                    estatusEmail.Mensaje = body;
                                    estatusEmail.Destinatario = user.Correo;
                                    estatusEmail.BaseDeDatos = new ML.BasesDeDatos();
                                    estatusEmail.BaseDeDatos.IdBaseDeDatos = BaseDeDatos.IdBaseDeDatos;
                                    estatusEmail.Encuesta = new ML.Encuesta();
                                    estatusEmail.Encuesta.IdEncuesta = Encuesta.IdEncuesta;

                                    using (var smtp = new SmtpClient())
                                    {
                                        try
                                        {
                                            BL.Encuesta.AddToEstatusEmail(estatusEmail);
                                            smtp.Send(message);
                                            BL.Encuesta.UpdateFlagEmailToSuccess(estatusEmail, Encuesta.IdEncuesta, (int)BaseDeDatos.IdBaseDeDatos);
                                            BL.NLogGeneratorFile.logInfoEmailSender("Email enviado correctamente", estatusEmail, user.IdEmpleado);
                                        }
                                        catch (SmtpException ex)
                                        {
                                            BL.NLogGeneratorFile.logInfoEmailSender(ex, estatusEmail, user.IdEmpleado);
                                            BL.Encuesta.UpdateFlagEmailToError(estatusEmail, ex, Encuesta.IdEncuesta, (int)BaseDeDatos.IdBaseDeDatos);
                                        }
                                        finally
                                        {
                                            smtp.Dispose();
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            BL.NLogGeneratorFile.logInfoEmailSender("No se encontraron usuarios para el envio de email", Encuesta.IdEncuesta, BaseDeDatos.IdBaseDeDatos);
                        }
                    }
                }
            }
            catch (Exception aE)
            {
                nlogClimaDinamico.Error("Method: " + new StackTrace().GetFrame(0).GetMethod().Name);
                nlogClimaDinamico.Error("Exception: " + aE);
                nlogClimaDinamico.Error("Inner Exception: " + aE.InnerException);
                nlogClimaDinamico.Error("StackTrace: " + aE.StackTrace);
            }
            return ML.ClimaDinamico.statusLogin.success;
        }
        public static void SendEmailError(Exception aE)
        {
            var message = new MailMessage();
            message.To.Add("jamurillo@grupoautofin.com");
            message.Subject = "Notificaciones de error";
            message.IsBodyHtml = true;
            message.Body = @"<p>Ocurrió la siguiente excepción</p>";
        }
        private static string ReplaceDatosMensaje(string mensajeOriginal, string Find, string NewValue)
        {
            int Place = mensajeOriginal.IndexOf(Find);
            string result = mensajeOriginal.Remove(Place, Find.Length).Insert(Place, NewValue);
            return result;
        }
        public static void UpdateGeneracionEmpleado()
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var generacion = context.Generaciones.Select(o => o).ToList();
                    // 1976-05-07
                    var actual = DateTime.Now.ToString("MM-dd");
                    string query = String.Format("select * from Empleado where FechaNacimiento like '%{0}%'", actual);
                    var birthDatNow = context.Empleado.SqlQuery(query).ToList();
                    foreach (var item in birthDatNow)
                    {
                        // 134497
                        // 134870
                        var anio = item.FechaNacimiento.ToString().Substring(0, 4);
                        // segun el año comparar con catalogo de generaciones
                        
                    }
                }
            }
            catch (Exception aE)
            {
                nlogClimaDinamico.Error("Method: " + new StackTrace().GetFrame(0).GetMethod().Name);
                nlogClimaDinamico.Error("Exception: " + aE);
                nlogClimaDinamico.Error("Inner Exception: " + aE.InnerException);
                nlogClimaDinamico.Error("StackTrace: " + aE.StackTrace);
            }
        }
        /// <summary>
        /// Obtiene el listado de respuestas para una pregunta
        /// </summary>
        /// <param name="IdPregunta"></param>
        /// <returns>Listado del model de respuestas por Id de pregunta</returns>
        public static List<ML.Respuestas> GetRespuestasByIdPreguntaRB(int IdPregunta, int IdEncuesta, int IdEmpleado)
        {
            var list = new List<ML.Respuestas>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var data = context.Respuestas.Where(o => o.IdPregunta == IdPregunta && o.IdEstatus == 1).ToList();
                    if (data == null)
                        return new List<ML.Respuestas>();
                    foreach (var item in data)
                    {
                        ML.Respuestas respuestas = new ML.Respuestas() { IdRespuesta = item.IdRespuesta, Respuesta = item.Respuesta };
                        list.Add(respuestas);
                    }
                    // consultar si tiene respuesta
                    var resp = context.EmpleadoRespuestas.Where(o => o.IdPregunta == IdPregunta && o.IdEncuesta == IdEncuesta && o.IdEmpleado == IdEmpleado).FirstOrDefault();
                    if (resp != null)
                        list.Add(new ML.Respuestas { IdRespuesta = (int)resp.IdRespuesta, Respuesta = resp.RespuestaEmpleado, UsuarioCreacion = "EmpRes" });
                    return list;
                }
            }
            catch (Exception aE)
            {
                nlogClimaDinamico.Error("Method: " + new StackTrace().GetFrame(0).GetMethod().Name);
                nlogClimaDinamico.Error("Exception: " + aE);
                nlogClimaDinamico.Error("Inner Exception: " + aE.InnerException);
                nlogClimaDinamico.Error("StackTrace: " + aE.StackTrace);
                return new List<ML.Respuestas>();
            }
        }
        /// <summary>
        /// valida un email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>true si el email es valido o false en el caso contrario</returns>
        public static bool isValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Obtiene un listado de respuestas y la respuesta guardada
        /// </summary>
        /// <param name="IdPregunta"></param>
        /// <param name="IdEncuesta"></param>
        /// <param name="IdEmpleado"></param>
        /// <returns></returns>
        public static List<ML.Respuestas> GetRespuestasByIdPregunta(int IdPregunta, int IdEncuesta, int IdEmpleado)
        {
            try
            {
                var list = new List<ML.Respuestas>();
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Respuestas.Where(o => o.Preguntas.IdPregunta == IdPregunta && o.Preguntas.idEncuesta == IdEncuesta && o.IdEstatus == 1).ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Respuestas respuestas = new ML.Respuestas();
                            respuestas.IdRespuesta = item.IdRespuesta;
                            respuestas.Respuesta = item.Respuesta;
                            list.Add(respuestas);
                        }
                    }
                    // consultar si tiene respuesta
                    var resp = context.EmpleadoRespuestas.Where(o => o.IdPregunta == IdPregunta && o.IdEncuesta == IdEncuesta && o.IdEmpleado == IdEmpleado).FirstOrDefault();
                    if (resp != null)
                        list.Add(new ML.Respuestas { IdRespuesta = Convert.ToInt32(resp.RespuestaEmpleado), Respuesta = "EmpRes" });
                    return list;
                }
            }
            catch (Exception aE)
            {
                nlogClimaDinamico.Error("Method: " + new StackTrace().GetFrame(0).GetMethod().Name);
                nlogClimaDinamico.Error("Exception: " + aE);
                nlogClimaDinamico.Error("Inner Exception: " + aE.InnerException);
                nlogClimaDinamico.Error("StackTrace: " + aE.StackTrace);
                return new List<ML.Respuestas>();
            }
        }
        public static List<ML.CompanyCategoria> GetUnidadesNegocio(int IdEmpleado, int IdEncuesta)
        {
            try
            {
                var list = new List<ML.CompanyCategoria>();
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.CompanyCategoria.Where(o => o.IdCompanyCategoria != 10).ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.CompanyCategoria companyCategoria = new ML.CompanyCategoria();
                            companyCategoria.IdCompanyCategoria = item.IdCompanyCategoria;
                            companyCategoria.Descripcion = item.Descripcion;
                            list.Add(companyCategoria);
                        }
                    }
                    // consultar si tiene respuesta
                    var Preguntas = context.Preguntas.Where(o => o.idEncuesta == IdEncuesta && o.IdPreguntaPadre == 186).FirstOrDefault();
                    var resp = context.EmpleadoRespuestas.Where(o => o.IdPregunta == Preguntas.IdPregunta && o.IdEncuesta == IdEncuesta && o.IdEmpleado == IdEmpleado).FirstOrDefault();
                    if (resp != null)
                        list.Add(new ML.CompanyCategoria { IdCompanyCategoria = Convert.ToInt32(resp.RespuestaEmpleado), Descripcion = "EmpRes" });
                    return list;
                }
            }
            catch (Exception aE)
            {
                nlogClimaDinamico.Error("Method: " + new StackTrace().GetFrame(0).GetMethod().Name);
                nlogClimaDinamico.Error("Exception: " + aE);
                nlogClimaDinamico.Error("Inner Exception: " + aE.InnerException);
                nlogClimaDinamico.Error("StackTrace: " + aE.StackTrace);
                return new List<ML.CompanyCategoria>();
            }
        }
        public static List<ML.Company> GetCompanies(int IdCompanyCategoria, int IdEmpleado, int IdEncuesta)
        {
            try
            {
                var list = new List<ML.Company>();
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Company.Where(o => o.IdCompanyCategoria == IdCompanyCategoria && o.IdEstatus == 1).ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Company company = new ML.Company();
                            company.CompanyId = item.CompanyId;
                            company.CompanyName = item.CompanyName;
                            list.Add(company);
                        }
                    }
                    // consultar si tiene respuesta
                    var Preguntas = context.Preguntas.Where(o => o.idEncuesta == IdEncuesta && o.IdPreguntaPadre == 187).FirstOrDefault();
                    var resp = context.EmpleadoRespuestas.Where(o => o.IdPregunta == Preguntas.IdPregunta && o.IdEncuesta == IdEncuesta && o.IdEmpleado == IdEmpleado).FirstOrDefault();
                    if (resp != null)
                        list.Add(new ML.Company { CompanyId = Convert.ToInt32(resp.RespuestaEmpleado), CompanyName = "EmpRes" });
                    return list;
                }
            }
            catch (Exception aE)
            {
                nlogClimaDinamico.Error("Method: " + new StackTrace().GetFrame(0).GetMethod().Name);
                nlogClimaDinamico.Error("Exception: " + aE);
                nlogClimaDinamico.Error("Inner Exception: " + aE.InnerException);
                nlogClimaDinamico.Error("StackTrace: " + aE.StackTrace);
                return new List<ML.Company>();
            }
        }
        public static List<ML.Area> GetArea(int CompanyId, int IdEmpleado, int IdEncuesta)
        {
            try
            {
                var list = new List<ML.Area>();
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Area.Where(o => o.CompanyId == CompanyId && o.IdEstatus == 1).ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Area area = new ML.Area();
                            area.IdArea = item.IdArea;
                            area.Nombre = item.Nombre;
                            list.Add(area);
                        }
                    }
                    // consultar si tiene respuesta
                    var Preguntas = context.Preguntas.Where(o => o.idEncuesta == IdEncuesta && o.IdPreguntaPadre == 188).FirstOrDefault();
                    var resp = context.EmpleadoRespuestas.Where(o => o.IdPregunta == Preguntas.IdPregunta && o.IdEncuesta == IdEncuesta && o.IdEmpleado == IdEmpleado).FirstOrDefault();
                    if (resp != null)
                        list.Add(new ML.Area { IdArea = Convert.ToInt32(resp.RespuestaEmpleado), Nombre = "EmpRes" });
                    return list;
                }
            }
            catch (Exception aE)
            {
                nlogClimaDinamico.Error("Method: " + new StackTrace().GetFrame(0).GetMethod().Name);
                nlogClimaDinamico.Error("Exception: " + aE);
                nlogClimaDinamico.Error("Inner Exception: " + aE.InnerException);
                nlogClimaDinamico.Error("StackTrace: " + aE.StackTrace);
                return new List<ML.Area>();
            }
        }
        public static List<ML.Departamento> GetDepartamentos(int IdArea, int IdEmpleado, int IdEncuesta)
        {
            try
            {
                var list = new List<ML.Departamento>();
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Departamento.Where(o => o.IdArea == IdArea && o.IdEstatus == 1).ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Departamento departamento = new ML.Departamento();
                            departamento.IdDepartamento = item.IdDepartamento;
                            departamento.Nombre = item.Nombre;
                            list.Add(departamento);
                        }
                    }
                    // consultar si tiene respuesta
                    var Preguntas = context.Preguntas.Where(o => o.idEncuesta == IdEncuesta && o.IdPreguntaPadre == 189).FirstOrDefault();
                    var resp = context.EmpleadoRespuestas.Where(o => o.IdPregunta == Preguntas.IdPregunta && o.IdEncuesta == IdEncuesta && o.IdEmpleado == IdEmpleado).FirstOrDefault();
                    if (resp != null)
                        list.Add(new ML.Departamento { IdDepartamento = Convert.ToInt32(resp.RespuestaEmpleado), Nombre = "EmpRes" });
                    return list;
                }
            }
            catch (Exception aE)
            {
                nlogClimaDinamico.Error("Method: " + new StackTrace().GetFrame(0).GetMethod().Name);
                nlogClimaDinamico.Error("Exception: " + aE);
                nlogClimaDinamico.Error("Inner Exception: " + aE.InnerException);
                nlogClimaDinamico.Error("StackTrace: " + aE.StackTrace);
                return new List<ML.Departamento>();
            }
        }
        public static List<ML.Subdepartamento> GetSubDepartamentos(int IdDepartamento)
        {
            try
            {
                var list = new List<ML.Subdepartamento>();
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.SubDepartamento.Where(o => o.IdDepartamento == IdDepartamento && o.IdEstatus == 1).ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Subdepartamento subdepartamento = new ML.Subdepartamento();
                            subdepartamento.IdSubdepartamento = item.IdSubdepartamento;
                            subdepartamento.Nombre = item.Nombre;
                            list.Add(subdepartamento);
                        }
                    }
                    return list;
                }
            }
            catch (Exception aE)
            {
                nlogClimaDinamico.Error("Method: " + new StackTrace().GetFrame(0).GetMethod().Name);
                nlogClimaDinamico.Error("Exception: " + aE);
                nlogClimaDinamico.Error("Inner Exception: " + aE.InnerException);
                nlogClimaDinamico.Error("StackTrace: " + aE.StackTrace);
                return new List<ML.Subdepartamento>();
            }
        }
        public static ML.EmpleadoRespuesta GetRespuesta(int IdPregunta, int IdEmpleado)
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.EmpleadoRespuestas.Where(o => o.IdEmpleado == IdEmpleado && o.IdPregunta == IdPregunta).FirstOrDefault();
                    if (query != null)
                    {
                        ML.EmpleadoRespuesta res = new ML.EmpleadoRespuesta()
                        {
                            RespuestaEmpleado = query.RespuestaEmpleado,
                        };
                        return res;
                    }
                }
            }
            catch (Exception aE)
            {
                nlogClimaDinamico.Error("Method: " + new StackTrace().GetFrame(0).GetMethod().Name);
                nlogClimaDinamico.Error("Exception: " + aE);
                nlogClimaDinamico.Error("Inner Exception: " + aE.InnerException);
                nlogClimaDinamico.Error("StackTrace: " + aE.StackTrace);
                return new ML.EmpleadoRespuesta();
            }
            return new ML.EmpleadoRespuesta();
        }
    }
}
