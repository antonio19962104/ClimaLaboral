using System;
using ML;
using DL;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace BL
{
    public class Competencia
    {
        public static NLog.Logger nlogClimaDinamico = NLog.LogManager.GetLogger(ML.LogTypes.LogClimaDinamico);
        public static Result getCompetencias(string aIdUsuario)
        {
            int idUsuarioAdm = Convert.ToInt32(aIdUsuario);
            Result result = new Result();
            try
            {
                using (RH_DesEntities context = new RH_DesEntities())
                {
                    //var IdParametro = 1;
                    //var query = context.Competencia.SqlQuery("SELECT * FROM Competencia where IdEstatus = {0} order by Nombre",IdParametro);
                    var query = context.Competencia.Select(o => o).Where(o => o.IdEstatus == 1 && o.IdAdminCreate == idUsuarioAdm || o.IdCompetencia < 15).ToList();
                    result.ListadoCompetenciasPregunta = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Competencia competencias = new ML.Competencia();
                            competencias.IdCompetencia = obj.IdCompetencia;
                            competencias.Nombre = obj.Nombre;                            
                            result.ListadoCompetenciasPregunta.Add(competencias);
                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontrarod Categorias de Pregunta";
                    }
                }
            }
            catch (Exception aE)
            {
                result.Correct = false;
                result.ErrorMessage = aE.Message.ToString();
            }
                return result;
        }

        public static Result getCompetenciasConPreguntas(string aIdUsuario)
                {
                    int idUsuarioAdm = Convert.ToInt32(aIdUsuario);
                    Result result = new Result();
                    try
                    {
                        using (RH_DesEntities context = new RH_DesEntities())
                        {
                            //var IdParametro = 1;
                            //var query = context.Competencia.SqlQuery("SELECT * FROM Competencia where IdEstatus = {0} order by Nombre",IdParametro);
                            var query = context.Competencia.Select(o => o).Where(o => o.IdEstatus == 1 && o.IdAdminCreate == idUsuarioAdm || o.IdCompetencia < 18).ToList();
                            result.ListadoCompetenciasPregunta = new List<object>();
                            if (query != null)
                            {
                                foreach (var obj in query)
                                {
                                    ML.Competencia competencias = new ML.Competencia();
                                    competencias.IdCompetencia = obj.IdCompetencia;
                                    competencias.Nombre = obj.Nombre;
                                    competencias.PreguntasPorCompetencia = new List<ML.Preguntas>();
                                    competencias.PreguntasPorCompetencia = getPreguntasByCompetencia(obj.IdCompetencia).ListadoPreguntaCompetencias;
                                    result.ListadoCompetenciasPregunta.Add(competencias);
                                    result.Correct = true;
                                }
                            }
                            else
                            {
                                result.Correct = false;
                                result.ErrorMessage = "No se encontrarod Categorias de Pregunta";
                            }
                        }
                    }
                    catch (Exception aE)
                    {
                        result.Correct = false;
                        result.ErrorMessage = aE.Message.ToString();
                    }
                        return result;
                }

        // Administracion de competencias
        public static List<ML.Competencia> GetByAdmin(string aIdUsuarioCreacion)
        {
            var list = new List<ML.Competencia>();
            try
            {
                int _idUsuario = Convert.ToInt32(aIdUsuarioCreacion);
                using (DL.RH_DesEntities context = new RH_DesEntities())
                {
                    var query = context.Competencia.Select(o => o).Where(o => o.IdEstatus == 1 && o.IdAdminCreate == _idUsuario || o.IdCompetencia < 15).OrderBy(o => o.IdCompetencia).ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Competencia compe = new ML.Competencia()
                            {
                                IdCompetencia = item.IdCompetencia,
                                Nombre = item.Nombre
                            };
                            list.Add(compe);
                        }
                    }
                    return list;
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return new List<ML.Competencia>();
            }
        }
        public static ML.ClimaDinamico.statusGuardado Add(List<ML.Competencia> aListCompetencia, string aUsuarioCreacion, string aIdUsuarioCreacion)
        {
            try
            {
                var dlListCompetencia = MappingConfigurations.MappingCompetencia(aListCompetencia, aUsuarioCreacion, aIdUsuarioCreacion);
                using (DL.RH_DesEntities context = new RH_DesEntities())
                {
                    context.Competencia.AddRange(dlListCompetencia);
                    context.SaveChanges();
                    return ML.ClimaDinamico.statusGuardado.success;
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return ML.ClimaDinamico.statusGuardado.error;
            }
        }
        public static ML.ClimaDinamico.statusGuardado Update(List<ML.Competencia> aListCompetencia, string aUsuarioModificacion)
        {
            try
            {
                using (DL.RH_DesEntities context = new RH_DesEntities())
                {
                    foreach (var item in aListCompetencia)
                    {
                        var compe = context.Competencia.Select(o => o).Where(o => o.IdCompetencia == item.IdCompetencia).FirstOrDefault();
                        if (compe != null)
                        {
                            compe.Nombre = item.Nombre;
                            compe.FechaHoraModificacion = DateTime.Now;
                            compe.UsuarioModificacion = aUsuarioModificacion;
                            compe.ProgramaModificacion = "Modulo de competencias";
                            context.SaveChanges();
                        }
                        else
                        {
                            BL.NLogGeneratorFile.logError("No se encontró la competencia con id " + item.IdCompetencia, new StackTrace());
                            return ML.ClimaDinamico.statusGuardado.itemNotFound;
                        }
                    }
                    return ML.ClimaDinamico.statusGuardado.success;
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return ML.ClimaDinamico.statusGuardado.error;
            }
        }
        public static ML.ClimaDinamico.statusGuardado Delete(ML.Competencia aCompetencia, string aUsuarioModificacion)
        {
            try
            {
                using (DL.RH_DesEntities context = new RH_DesEntities())
                {
                    var compe = context.Competencia.Select(o => o).Where(o => o.IdCompetencia == aCompetencia.IdCompetencia).FirstOrDefault();
                    if (compe != null)
                    {
                        compe.IdEstatus = 2;
                        compe.FechaHoraEliminacion = DateTime.Now;
                        compe.UsuarioEliminacion = aUsuarioModificacion;
                        compe.ProgramaEliminacion = "Modulo de competencias";
                        context.SaveChanges();
                        return ML.ClimaDinamico.statusGuardado.success;
                    }
                    else
                    {
                        BL.NLogGeneratorFile.logError("No se encontró la competencia con id " + aCompetencia.IdCompetencia, new StackTrace());
                        return ML.ClimaDinamico.statusGuardado.itemNotFound;
                    }
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return ML.ClimaDinamico.statusGuardado.error;
            }
        }
        // Administracion de categorias
        public static List<ML.Categoria> GetCategoriaByAdmin(string aIdUsuarioCreacion)
        {
            var list = new List<ML.Categoria>();
            try
            {
                int _idUsuario = Convert.ToInt32(aIdUsuarioCreacion);
                using (DL.RH_DesEntities context = new RH_DesEntities())
                {
                    var query = context.Categoria.Select(o => o).Where(o => o.Estatus == 1 && o.IdAdminCreate == _idUsuario || o.IdCategoria < 40).OrderBy(o => o.IdCategoria).ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Categoria catego = new ML.Categoria()
                            {
                                IdCategoria = item.IdCategoria,
                                Nombre = item.Nombre,
                                IdPadre = Convert.ToInt32(item.IdPadre == null ? 0 : item.IdPadre)
                            };
                            list.Add(catego);
                        }
                    }
                    return list;
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return new List<ML.Categoria>();
            }
        }
        public static ML.ClimaDinamico.statusGuardado AddCategoria(List<ML.Categoria> aListCategoria, string aUsuarioCreacion, string aIdUsuarioCreacion)
        {
            try
            {
                var dlListCompetencia = MappingConfigurations.MappingCategoria(aListCategoria, aUsuarioCreacion, aIdUsuarioCreacion);
                using (DL.RH_DesEntities context = new RH_DesEntities())
                {
                    context.Categoria.AddRange(dlListCompetencia);
                    context.SaveChanges();
                    return ML.ClimaDinamico.statusGuardado.success;
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return ML.ClimaDinamico.statusGuardado.error;
            }
        }
        public static ML.ClimaDinamico.statusGuardado DeleteCategoria(string aIdCategoria, string aUsuarioModificacion)
        {
            try
            {
                int _idCategoria = Convert.ToInt32(aIdCategoria);
                using (DL.RH_DesEntities context = new RH_DesEntities())
                {
                    var compe = context.Categoria.Select(o => o).Where(o => o.IdCategoria == _idCategoria).FirstOrDefault();
                    if (compe != null)
                    {
                        compe.Estatus = 2;
                        compe.FechaHoraEliminacion = DateTime.Now;
                        compe.UsuarioEliminacion = aUsuarioModificacion;
                        compe.ProgramaEliminacion = "Modulo de categorias";
                        context.SaveChanges();
                        return ML.ClimaDinamico.statusGuardado.success;
                    }
                    else
                    {
                        BL.NLogGeneratorFile.logError("No se encontró la competencia con id " + _idCategoria, new StackTrace());
                        return ML.ClimaDinamico.statusGuardado.itemNotFound;
                    }
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return ML.ClimaDinamico.statusGuardado.error;
            }
        }
        public static ML.ClimaDinamico.statusGuardado UpdateCategoria(List<ML.Categoria> aListCategoria, string aUsuarioModificacion)
        {
            try
            {
                using (DL.RH_DesEntities context = new RH_DesEntities())
                {
                    foreach (var item in aListCategoria)
                    {
                        var compe = context.Categoria.Select(o => o).Where(o => o.IdCategoria == item.IdCategoria).FirstOrDefault();
                        if (compe != null)
                        {
                            compe.Nombre = item.Nombre;
                            compe.Descripcion = item.Descripcion;
                            compe.FechaHoraModificacion = DateTime.Now;
                            compe.UsuarioModificacion = aUsuarioModificacion;
                            compe.ProgramaModificacion = "Modulo de competencias";
                            context.SaveChanges();
                        }
                        else
                        {
                            BL.NLogGeneratorFile.logError("No se encontró la categoria con id " + item.IdCategoria, new StackTrace());
                            return ML.ClimaDinamico.statusGuardado.itemNotFound;
                        }
                    }
                    return ML.ClimaDinamico.statusGuardado.success;
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return ML.ClimaDinamico.statusGuardado.error;
            }
        }
        public static Result getNombreCompetenciabyId(int aIdCompentencia)
        {
            Result result = new Result();
            try
            {
                using (RH_DesEntities context = new RH_DesEntities())
                {
                    var IdParametro = 1;
                    var query = context.Competencia.SqlQuery("SELECT * FROM Competencia where IdEstatus = {0} and IdCompetencia = {1} order by Nombre", IdParametro, aIdCompentencia);
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            result.CURRENT_USER = obj.Nombre;
                        }
                    }
                }

            }
            catch (Exception aE)
            {
                result.Correct = false;
                result.ErrorMessage = aE.Message.ToString();
            }
            return result;
        }
        public static Result getPreguntasByCompetencia(int aIdCompetencia)
        {
            Result result = new Result();
            try
            {
                using (RH_DesEntities context = new RH_DesEntities())
                {
                    var query = context.Preguntas.SqlQuery("SELECT * FROM Preguntas  INNER JOIN Competencia ON Preguntas.IdCompetencia = Competencia.IdCompetencia where Preguntas.idEncuesta = 1 and Preguntas.IdEstatus = 1 and Preguntas.IdCompetencia = {0}", aIdCompetencia);
                    string preguntaE = "";
                    result.ListadoPreguntaCompetencias = new List<ML.Preguntas>();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            if (item.IdEnfoque == 1)
                            {
                                preguntaE = item.Pregunta;
                                ML.Preguntas preguntaCompetencia = new ML.Preguntas();
                                preguntaCompetencia.IdPreguntaPadre = item.IdPregunta;
                                preguntaCompetencia.IdEnfoque = Convert.ToInt32(item.IdEnfoque);
                                preguntaCompetencia.Pregunta = preguntaE;
                                preguntaCompetencia.Valoracion = item.Valoracion;
                                preguntaCompetencia.Enfoque = item.Enfoque;
                                preguntaCompetencia.Competencia = new ML.Competencia();
                                preguntaCompetencia.Competencia.IdCompetencia = aIdCompetencia;
                                preguntaCompetencia.Competencia.Nombre = getNombreCompetenciabyId(aIdCompetencia).CURRENT_USER.ToString();
                                preguntaCompetencia.Respuestas = new List<ML.Respuestas>();
                                ML.Respuestas respuestaPC = new ML.Respuestas();
                                respuestaPC.Respuesta = "#RespuestaLibre";
                                //respuestaPC.Categoria = new ML.Preguntas();
                                //respuestaPC.Pregunta.IdPreguntaPadre = getIdPregPadArByIdPrePadEmp(item.IdPregunta).Avance;
                                preguntaCompetencia.Respuestas.Add(respuestaPC);
                                result.ListadoPreguntaCompetencias.Add(preguntaCompetencia);
                            }
                        }

                    }
                }
            }
            catch (Exception aE)
            {
                result.Correct = false;
                result.ErrorMessage = aE.Message.ToString();

            }
            return result;
        }
        public static ML.Result getIdPregPadArByIdPrePadEmp(int idPreguntaPadre)
        {
            ML.Result result = new Result();
            try
            {
                using (DL.RH_DesEntities contextpp = new DL.RH_DesEntities())
                {
                    var querypp = contextpp.Preguntas.Where(m => m.IdPregunta == idPreguntaPadre).ToList();//context.Preguntas.SqlQuery("SELECT * FROM PREGUNTAS WHERE IdPregunta = {0}", idPreguntaPadre).ToList();
                    if (querypp != null)
                    {
                        foreach (var item in querypp)
                        {
                            var preguntaEE = "";
                            preguntaEE = item.Pregunta.Trim();
                            var queryEA = contextpp.Preguntas.Where(n => n.Pregunta == preguntaEE && n.IdEnfoque == 2).ToList();//context.Preguntas.SqlQuery("SELECT * FROM PREGUNTAS WHERE PREGUNTA = '{0}' AND  IDENFOQUE = 2", preguntaEE).ToList();
                            if (queryEA != null)
                            {
                                foreach (var obj in queryEA)
                                {
                                    result.Avance = obj.IdPregunta;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception aE)
            {
                result.Correct = false;
                result.ErrorMessage = aE.Message.ToString();
            }
            return result;
        }
    }
}
