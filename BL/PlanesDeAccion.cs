//using DocumentFormat.OpenXml.Math;
//using DocumentFormat.OpenXml.Packaging;
using CronExpressionDescriptor;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    /// <summary>
    /// Capa de Negocios del Módulo de Planes de Acción
    /// </summary>
    public class PlanesDeAccion
    {
        #region Generales Modulo
        /// <summary>
        /// Obtiene la informacion de un plan de accion especifico
        /// </summary>
        /// <param name="IdPlan" type="int"></param>
        /// <returns></returns>
        public static ML.PlanDeAccion GetPlanById(int IdPlan)
        {
            ML.PlanDeAccion planDeAccion = new ML.PlanDeAccion();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var data = context.PlanDeAccion.Where(o => o.IdPlanDeAccion == IdPlan).FirstOrDefault();
                    planDeAccion.IdPlanDeAccion = data.IdPlanDeAccion;
                    planDeAccion.IdBaseDeDatos = (int)data.IdBaseDeDatos;
                    planDeAccion.IdEncuesta = (int)data.IdEncuesta;
                    planDeAccion.Nombre = data.Nombre;
                    planDeAccion.Area = data.Area;
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
            }
            return planDeAccion;
        }
        /// <summary>
        /// Obtiene acciones plan(Detalle de lo que se configura en el plan de accion)
        /// </summary>
        /// <param name="IdAccion" type="int"></param>
        /// <param name="IdPlan" type="int"></param>
        /// <returns></returns>
        public static ML.AccionesPlan ObtenerAccionesPlan(int IdAccion, int IdPlan)
        {
            ML.AccionesPlan accionesPlan = new ML.AccionesPlan();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var data = context.AccionesPlan.Where(o => o.IdPlanDeAccion == IdPlan && o.IdAccion == IdAccion).FirstOrDefault();
                    accionesPlan.IdAccionesPlan = data.IdAccionesPlan;
                    accionesPlan.AccionesDeMejora.IdAccionDeMejora = (int)data.IdAccion;
                    accionesPlan.Periodicidad = (int)data.Periodicidad;
                    accionesPlan.FechaInicio = (DateTime)data.FechaInicio;
                    accionesPlan.FechaFin = (DateTime)data.FechaFin;
                    accionesPlan.Objetivo = data.Objetivo;
                    accionesPlan.Meta = data.Meta;
                    accionesPlan.Comentarios = data.Comentarios;
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
            }
            return accionesPlan;
        }
        /// <summary>
        /// Agrega los promedios obtenidos por cada categoria en un area encuesta y base de datos especifica
        /// </summary>
        /// <param name="promediosCategorias"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public static ML.Result GuardarPromediosPorCategoria(List<ML.PromediosCategorias> promediosCategorias, string usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    foreach (var promedioCategoria in promediosCategorias)
                    {
                        var existe = context.PromediosCategorias.Where(o => 
                            o.Area == promedioCategoria.Area && 
                            o.IdBaseDeDatos == promedioCategoria.BasesDeDatos.IdBaseDeDatos &&
                            o.IdCategoria == promedioCategoria.Categoria.IdCategoria &&
                            o.IdEncuesta == promedioCategoria.Encuesta.IdEncuesta).FirstOrDefault();
                        if (existe == null) // Insercion
                        {
                            DL.PromediosCategorias promedio = new DL.PromediosCategorias()
                            {
                                Area = promedioCategoria.Area,
                                IdBaseDeDatos = promedioCategoria.BasesDeDatos.IdBaseDeDatos,
                                IdCategoria = promedioCategoria.Categoria.IdCategoria,
                                IdEncuesta = promedioCategoria.Encuesta.IdEncuesta,
                                Promedio = promedioCategoria.Promedio,
                                FechaHoraCreacion = DateTime.Now,
                                UsuarioCreacion = usuario,
                                ProgramaCreacion = "GuardarPromediosPorCategoria"
                            };
                            context.PromediosCategorias.Add(promedio);
                        }
                        else // Actualizacion
                        {
                            existe.Promedio = promedioCategoria.Promedio;
                            existe.FechaHoraModificacion = DateTime.Now;
                            existe.UsuarioModificacion = usuario;
                            existe.ProgramaModificacion = "GuardarPromediosPorCategoria";
                        }
                    }
                    context.SaveChanges();
                    result.Correct = true;
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
            }
            return result;
        }
        /// <summary>
        /// Obtiene el promedio que obtuvo una categoria en un area, encuesta y base de datos especifica
        /// </summary>
        /// <param name="promediosCategorias"></param>
        /// <returns></returns>
        public static decimal ObtenerPromedioCategoria(ML.PromediosCategorias promediosCategorias)
        {
            decimal result = 0;
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var data = context.PromediosCategorias.
                        Where(o => 
                        o.Area == promediosCategorias.Area && 
                        o.IdEncuesta == promediosCategorias.Encuesta.IdEncuesta && 
                        o.IdBaseDeDatos == promediosCategorias.BasesDeDatos.IdBaseDeDatos &&
                        o.IdCategoria == promediosCategorias.Categoria.IdCategoria).FirstOrDefault();
                    if (data != null)
                        result = (decimal)data.Promedio;
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
            }
            return result;
        }
        /// <summary>
        /// Obtiene las encuestas de clima laboral para porder configurar sus acciones y planes
        /// </summary>
        /// <returns>Objeto ML.Result</returns>
        public static ML.Result GetEncuestasClima()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var encuestas = context.ConfigClimaLab.Where(o => o.Encuesta.IdEstatus == 1 && o.FechaFin < DateTime.Now).ToList();
                    if (encuestas != null)
                    {
                        foreach (var item in encuestas)
                        {
                            ML.Encuesta encuesta = new ML.Encuesta();
                            encuesta.IdEncuesta = (int)item.IdEncuesta;
                            encuesta.Nombre = item.Encuesta.Nombre;
                            encuesta.cadenaInicio = item.FechaInicio.ToString();
                            encuesta.cadenaFin = item.FechaFin.ToString();
                            encuesta.BasesDeDatos = new ML.BasesDeDatos();
                            encuesta.BasesDeDatos.IdBaseDeDatos = item.IdBaseDeDatos;
                            encuesta.BasesDeDatos.Nombre = item.BasesDeDatos.Nombre;
                            encuesta.Anio = (int)item.PeriodoAplicacion;

                            result.Objects.Add(encuesta);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
            }
            return result;
        }
        /// <summary>
        /// Obtiene las categorias en orden por promedio obtenido
        /// </summary>
        /// <param name="promedioSubCategorias">Objeto ML.PromedioSubCategorias</param>
        /// <returns>Objeto ML.Result</returns>
        public static ML.Result JobGenerarPromedioSubCategorias(ML.PromedioSubCategorias promedioSubCategorias)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            var historicoCategoria = new List<int>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    //Obtener el listado de categorias que conforman una encuesta
                    var categoriasByEncuesta = context.ValoracionPreguntaPorSubcategoria.Where(o => o.IdEncuesta == promedioSubCategorias.IdEncuesta && o.IdEstatus == 1).OrderBy(o => o.IdSubcategoria).ToList();
                    foreach (var categoria in categoriasByEncuesta)
                    {
                        if (!historicoCategoria.Contains((int)categoria.IdSubcategoria))
                        {
                            //Obtener las preguntas que pertenecen a cada categoria
                            var preguntasByCategoria = categoriasByEncuesta.Where(o => o.IdSubcategoria == categoria.IdSubcategoria).ToList();
                            historicoCategoria.Add((int)categoria.IdSubcategoria);
                            decimal sumaPromedios = 0;
                            foreach (var pregunta in preguntasByCategoria)
                            {
                                promedioSubCategorias.IdPregunta = (int)pregunta.IdPregunta;
                                sumaPromedios += GetPromedioByIdPregunta(promedioSubCategorias);
                            }
                            ML.Categoria categoriaModel = new ML.Categoria();
                            categoriaModel.Nombre = BL.Categoria.getNombreSubCategoria((int)categoria.IdSubcategoria);
                            categoriaModel.Promedio = Math.Round(sumaPromedios / preguntasByCategoria.Count(), 2);
                            categoriaModel.IdPadre = BL.Categoria.getIdPadreSubCategoria((int)categoria.IdSubcategoria);
                            categoriaModel.NombrePadreCategoria = BL.Categoria.getNombreCatByIdCat(categoriaModel.IdPadre);
                            //Sumar los promedios de todas las subcategorias y meterlas al resultado del la Categoria padre
                            BL.NLogGeneratorFile.nlogPlanesDeAccion.Info("JobGenerarPromedioSubCategorias: Valores obtenidos de la categoria: " + categoriaModel.Nombre);
                            BL.NLogGeneratorFile.logObjectsModuloPlanesDeAccion(categoriaModel);
                            result.Objects.Add(categoriaModel);
                        }
                    }
                    promedioSubCategorias.JsonData = Newtonsoft.Json.JsonConvert.SerializeObject(result.Objects);
                    if (AddDataPromedioSubCategorias(promedioSubCategorias).Correct)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Falló el guardado de los datos";
                        result.ex = new Exception();
                    }
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
            }
            return result;
        }
        /// <summary>
        /// Obtiene el promedio generar de una categoria
        /// </summary>
        /// <param name="promedioSubCategorias">Objeto ML.PromedioSubCategorias</param>
        /// <returns>Promedio obtenido de la pregunta en Decimal</returns>
        public static decimal GetPromedioByIdPregunta(ML.PromedioSubCategorias promedioSubCategorias)
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    int noEmpleados = 0;
                    var empleadosByEncuesta = context.Empleado.Where(o => o.IdBaseDeDatos == promedioSubCategorias.IdBaseDeDatos && o.AreaAgencia == promedioSubCategorias.AreaAgencia && o.EstatusEmpleado == "Activo").ToList();
                    foreach (var empleado in empleadosByEncuesta)
                    {
                        var estatusEncuesta = context.EstatusEncuesta.Where(o => o.IdEmpleado == empleado.IdEmpleado && o.Anio == promedioSubCategorias.AnioAplicacion).FirstOrDefault();
                        if (estatusEncuesta != null)
                        {
                            if (estatusEncuesta.Estatus == "Terminada")
                                noEmpleados++;
                        }
                    }
                    var noAfirmativas = context.EmpleadoRespuestas.Where(o => o.Empleado.IdBaseDeDatos == promedioSubCategorias.IdBaseDeDatos && o.Empleado.EstatusEmpleado == "Activo" && o.RespuestaEmpleado.Contains("e es verdad") && o.IdPregunta == promedioSubCategorias.IdPregunta && o.Empleado.AreaAgencia == promedioSubCategorias.AreaAgencia && o.Anio == promedioSubCategorias.AnioAplicacion).ToList();
                    return Math.Round((noAfirmativas.Count() / (decimal)noEmpleados) * 100, 2);
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                return 0;
            }
        }
        /// <summary>
        /// Agrega o actualiza los promedios obtenidos de cada subcategoria
        /// </summary>
        /// <param name="promedioSubCategorias">Objeto ML.PromedioSubCategorias</param>
        /// <returns>Objeto ML.Result</returns>
        public static ML.Result AddDataPromedioSubCategorias(ML.PromedioSubCategorias promedioSubCategorias)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var dataPromedioSubCategorias = context.PromedioSubCategorias.Where(o => o.AnioAplicacion == promedioSubCategorias.AnioAplicacion && o.AreaAgencia == promedioSubCategorias.AreaAgencia && o.IdBaseDeDatos == promedioSubCategorias.IdBaseDeDatos && o.IdEncuesta == promedioSubCategorias.IdEncuesta).FirstOrDefault();
                    if (dataPromedioSubCategorias == null)
                    {
                        var data = new DL.PromedioSubCategorias()
                        {
                            AnioAplicacion = promedioSubCategorias.AnioAplicacion,
                            AreaAgencia = promedioSubCategorias.AreaAgencia,
                            IdBaseDeDatos = promedioSubCategorias.IdBaseDeDatos,
                            IdEncuesta = promedioSubCategorias.IdEncuesta,
                            JsonData = promedioSubCategorias.JsonData,
                            FechaHoraCreacion = DateTime.Now,
                            ProgramaCreacion = "Job Generator Data",
                            UsuarioCreacion = "HangFire Job"
                        };
                        context.PromedioSubCategorias.Add(data);
                        context.SaveChanges();
                        result.Correct = true;
                    }
                    if (dataPromedioSubCategorias != null)
                    {
                        if (dataPromedioSubCategorias.IdPromedioSubCategorias > 0)
                        {
                            dataPromedioSubCategorias.JsonData = promedioSubCategorias.JsonData;
                            context.SaveChanges();
                            result.Correct = true;
                        }
                    }
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
            }
            return result;
        }
        /// <summary>
        /// Obtiene los promedios de las subcategorias que previamenrte generó el job (el promedio no importa solo lo configurado en la encuesta)
        /// </summary>
        /// <param name="promedioSubCategorias">Objeto ML.PromedioSubCategorias</param>
        /// <returns>Objeto ML.Result</returns>
        public static ML.Result GetSubCategoriasByIdEncuesta(ML.PromedioSubCategorias promedioSubCategorias)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<Object>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {

                    var datapromediosSubCategorias = context.PromedioSubCategorias.Where(o => o.IdBaseDeDatos == 2114 && o.IdEncuesta == 1).ToList();
                    var data = datapromediosSubCategorias.ElementAt(0);
                    if (data == null)
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Los datos aún no se encuentran generados";
                    }
                    if (data.IdPromedioSubCategorias > 0)
                    {
                        ML.PromedioSubCategorias promedioSub = new ML.PromedioSubCategorias();
                        promedioSub.IdPromedioSubCategorias = data.IdPromedioSubCategorias;
                        promedioSub.JsonData = data.JsonData;
                        result.Objects.Add(promedioSub);
                        result.Correct = true;
                    }
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
            }
            return result;
        }
        /// <summary>
        /// Obtiene los promedios de las subcategorias que previamenrte generó el job (con promedios)
        /// </summary>
        /// <param name="promedioSubCategorias">Objeto ML.PromedioSubCategorias</param>
        /// <returns>Objeto ML.Result</returns>
        public static ML.Result GetPromediosSubCategoriasByAreaAgencia(ML.PromedioSubCategorias promedioSubCategorias)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<Object>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {

                    var datapromediosSubCategorias = context.PromedioSubCategorias.Where(o => o.AnioAplicacion == promedioSubCategorias.AnioAplicacion && o.AreaAgencia == promedioSubCategorias.AreaAgencia && o.IdBaseDeDatos == promedioSubCategorias.IdBaseDeDatos && o.IdEncuesta == promedioSubCategorias.IdEncuesta).ToList();
                    var data = datapromediosSubCategorias.ElementAt(0);
                    if (data == null)
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Los datos aún no se encuentran generados";
                    }
                    if (data.IdPromedioSubCategorias > 0)
                    {
                        ML.PromedioSubCategorias promedioSub = new ML.PromedioSubCategorias();
                        promedioSub.IdPromedioSubCategorias = data.IdPromedioSubCategorias;
                        promedioSub.JsonData = data.JsonData;
                        result.Objects.Add(promedioSub);
                        result.Correct = true;
                    }
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
            }
            return result;
        }
        /// <summary>
        /// Ejecuta el job de generacion de resultados para planes de accion
        /// </summary>
        public static void EjecutaJob()
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    BL.NLogGeneratorFile.nlogPlanesDeAccion.Info("El Job EjecutaJob ha iniciado");
                    // (Sobre la encuesta original) Verificar encuestas de clima laboral con fecha fin cumplida
                    var encuestaClimaTerminadasList = context.ConfigClimaLab.Where(o => o.FechaFin < DateTime.Now && o.Encuesta.IdEstatus == 1).ToList();
                    foreach (var encuesta in encuestaClimaTerminadasList)
                    {
                        // Obtener las areas existentes en esa encuesta
                        var listAreasPorEmpleado = context.Empleado.Select(o => new { o.IdBaseDeDatos, o.EstatusEmpleado, o.AreaAgencia }).Where(o => o.IdBaseDeDatos == encuesta.IdBaseDeDatos && o.EstatusEmpleado == "Activo").ToList();
                        var listAreas = listAreasPorEmpleado.Select(o => o.AreaAgencia).ToList();
                        listAreas = listAreas.Distinct().ToList();
                        foreach (var area in listAreas)
                        {
                            ML.PromedioSubCategorias promedioSubCategorias = new ML.PromedioSubCategorias()
                            {
                                AnioAplicacion = (int)encuesta.PeriodoAplicacion,
                                AreaAgencia = area,
                                IdBaseDeDatos = (int)encuesta.IdBaseDeDatos,
                                IdEncuesta = (int)encuesta.IdEncuesta,
                            };
                            if (!TieneRegistroDePromedios(promedioSubCategorias))
                            {
                                JobGenerarPromedioSubCategorias(promedioSubCategorias);
                            }
                        }
                    }
                    // (Sobre encuestas dinamicas de clima laboral)
                    // Este paso se omite ya que todas las encuestas de clima aun que sean dinamicas graban su registro en config clima lab
                    /*var encuestaClimaTerminadasList2 = context.Encuesta.Where(o => o.FechaFin < DateTime.Now && o.IdEstatus == 1).ToList();
                    foreach (var encuesta in encuestaClimaTerminadasList2)
                    {
                        // Obtener las areas existentes en esa encuesta
                        var listAreasPorEmpleado = context.Empleado.Select(o => new { o.IdBaseDeDatos, o.EstatusEmpleado, o.AreaAgencia }).Where(o => o.IdBaseDeDatos == encuesta.IdBasesDeDatos && o.EstatusEmpleado == "Activo").ToList();
                        var listAreas = listAreasPorEmpleado.Select(o => o.AreaAgencia).ToList();
                        listAreas = listAreas.Distinct().ToList();
                        listAreas = listAreas.Where(o => o == "-" == false).ToList();
                        listAreas = listAreas.Where(o => o == "- -" == false).ToList();
                        listAreas = listAreas.Where(o => o == "- - " == false).ToList();
                        foreach (var area in listAreas)
                        {
                            if (encuesta.IdBasesDeDatos != null)
                            {
                                var configEncuesta = context.ConfigClimaLab.Where(o => o.IdEncuesta == encuesta.IdEncuesta && o.IdBaseDeDatos == encuesta.IdBasesDeDatos).FirstOrDefault();
                                ML.PromedioSubCategorias promedioSubCategorias = new ML.PromedioSubCategorias()
                                {
                                    AnioAplicacion = (int)configEncuesta.PeriodoAplicacion,
                                    AreaAgencia = area,
                                    IdBaseDeDatos = (int)encuesta.IdBasesDeDatos,
                                    IdEncuesta = (int)encuesta.IdEncuesta,
                                };
                                if (!TieneRegistroDePromedios(promedioSubCategorias))//Solo si no existe lo genera de nuevo
                                {
                                    JobGenerarPromedioSubCategorias(promedioSubCategorias);
                                }
                            }
                        }
                    }*/
                    BL.NLogGeneratorFile.nlogPlanesDeAccion.Info("El job de generación de promedios de subcategorias ha terminado");
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
            }
        }
        /// <summary>
        /// Verifica si un area tiene ya generados sus promedios por subcategoria
        /// </summary>
        /// <param name="promedioSubCategorias">Objeto ML.PromedioSubCategorias</param>
        /// <returns>Bool</returns>
        public static bool TieneRegistroDePromedios(ML.PromedioSubCategorias promedioSubCategorias)
        {
            bool existe = false;
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var data = context.PromedioSubCategorias.Where(o =>
                    o.AnioAplicacion == promedioSubCategorias.AnioAplicacion &&
                    o.AreaAgencia == promedioSubCategorias.AreaAgencia &&
                    o.IdBaseDeDatos == promedioSubCategorias.IdBaseDeDatos &&
                    o.IdEncuesta == promedioSubCategorias.IdEncuesta
                    ).ToList();

                    if (data != null)
                    {
                        if (data.Count > 0)
                        {
                            existe = true;
                        }
                        if (data.Count == 0)
                        {
                            existe = false;
                        }
                    }
                    if (data == null)
                    {
                        existe = false;
                    }
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                existe = false;
            }
            return existe;
        }
        /// <summary>
        /// Agrega un nuevo plan de acción
		  /// Se agrega una nueva Accion Plan con el Id nuevo del Plan de Acción
        /// Se valida la existencia de los Responsables en dos tablas Responsables y Administradores
        /// Se graba ResponsablesAccionesPlan por responsables
        /// Se registra Responsable
        /// Se agregan los prefiles segun el repsonsable por idAdministrador
        /// En el caso de no existir en niguna tabla, se insertan an las dos tablas Responsables y Administradores
        /// 
        /// </summary>
        /// <param name="planDeAccion"></param>
        /// <param name="IdUsuarioAcyual"></param>
        /// <param name="UsuarioActual"></param>
        /// <returns></returns>
        public static ML.Result AddPlanDeAccion(ML.PlanDeAccion planDeAccion, string UsuarioActual, int IdUsuarioAcyual)
        {
            ML.Result result = new ML.Result();
			int idPlanDeAccion = 0;
            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                using (var transaction = context.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    try
                    {
                        //alta de plan de accion
                        var dataPlanDeAccion = new DL.PlanDeAccion()
                        {
                            Nombre = planDeAccion.Nombre,
                            IdUsuarioCreacion = IdUsuarioAcyual,
                            IdEncuesta = planDeAccion.IdEncuesta,
                            FechaHoraCreacion = DateTime.Now,
                            UsuarioCreacion = UsuarioActual,
                            ProgramaCreacion = "Modulo Planes de Acción Alta",
                            IdBaseDeDatos = planDeAccion.IdBaseDeDatos,
                            Area = planDeAccion.Area
                        };
                        context.PlanDeAccion.Add(dataPlanDeAccion);
                        context.SaveChanges();
                        idPlanDeAccion = (int)context.PlanDeAccion.Max(q => q.IdPlanDeAccion);
                        // alta de acciones por categoria
                        foreach (ML.AccionesPlan item in planDeAccion.ListAcciones)
                        {
                            var actualizaAccion = context.Acciones.Where(o => o.IdAccion == item.IdAccion).FirstOrDefault();
                            if (actualizaAccion != null)
                            {
                                actualizaAccion.Descripcion = item.PlanDeAccion.Nombre.Trim();
                                actualizaAccion.UsuarioModificacion = UsuarioActual;
                                actualizaAccion.FechaHoraModificacion = DateTime.Now;
                                actualizaAccion.ProgramaModificacion = "Modulo Planes de Acción Alta - Creacion de Acción";

                            }
                            context.SaveChanges();
                            DL.AccionesPlan acciones = new DL.AccionesPlan()
                            {
                                IdPlanDeAccion = idPlanDeAccion,
                                IdAccion = item.IdAccion,
                                Periodicidad = item.Periodicidad,
                                FechaInicio = item.FechaInicio,
                                FechaFin = item.FechaFin,
                                Objetivo = item.Objetivo,
                                Meta = item.Meta,
                                Comentarios = item.Comentarios,
                                FechaHoraCreacion = DateTime.Now,
                                UsuarioCreacion = UsuarioActual,
                                ProgramaCreacion = "Modulo Planes de Acción Alta - Acciones"
                            };
                            context.AccionesPlan.Add(acciones);
                            context.SaveChanges();
                            var idAccionPlan = context.AccionesPlan.Max(o => o.IdAccionesPlan);
                            //alta de responsables
                            foreach (ML.ResponsablesAccionesPlan responsable in item.ListadoResponsables)
                            {
                                 var existeResponsableEnAdmin = ExisteResponsableenAdministrador(responsable.Responsable.Email, context);
                                var existeResponsable = ExisteResponsable(responsable.Responsable.Email, context);
                                //Si existe en Admin, entonces preguntamos si existe en Responsables
                                if (existeResponsableEnAdmin.Correct)
                                {
                                    //Si NO existe en Responsable se inserta en tabla de Responsable y se obtien el id para las relaciones
                                    if (!existeResponsable.Correct)
                                    {
                                        // se inserta en Responsables porque no existe, se obtiene el idResponsable
                                        DL.Responsable responsablePA = new DL.Responsable()
                                        {
                                            IdAdministrador = existeResponsableEnAdmin.CURRENTIDADMINLOG,
                                            Nombre = responsable.Responsable.Nombre,
                                            ApellidoPaterno = responsable.Responsable.ApellidoPaterno,
                                            ApellidoMaterno = responsable.Responsable.ApellidoMaterno,
                                            Email = responsable.Responsable.Email,
                                            FechaHoraCreacion = DateTime.Now,
                                            UsuarioCreacion = UsuarioActual,
                                            ProgramaCreacion = "Modulo Planes de Acción Alta - Responsable"
                                        };
                                        context.Responsable.Add(responsablePA);
                                        context.SaveChanges();
                                        var idresponsable = context.Responsable.Max(o => o.IdResponsable);

                                        DL.ResponsablesAccionesPlan respon1 = new DL.ResponsablesAccionesPlan()
                                        {
                                            IdAccionesPlan = idAccionPlan,
                                            IdResponsable = idresponsable,
                                            FechaHoraCreacion = DateTime.Now,
                                            UsuarioCreacion = UsuarioActual,
                                            ProgramaCreacion = "Modulo Planes de Acción Alta - Responsable AccionPlan"
                                        };
                                        context.ResponsablesAccionesPlan.Add(respon1);
                                        context.SaveChanges();
                                        AgregarPerfilPlanesDeAccionN(existeResponsable.CURRENT_IDEMPLEADOLOG);
                                    }
                                    else
                                    {
                                        DL.ResponsablesAccionesPlan respon = new DL.ResponsablesAccionesPlan()
                                        {
                                            IdAccionesPlan = idAccionPlan,
                                            IdResponsable = existeResponsable.CURRENTIDADMINLOG,
                                            FechaHoraCreacion = DateTime.Now,
                                            UsuarioCreacion = UsuarioActual,
                                            ProgramaCreacion = "Modulo Planes de Acción Alta - Responsable AccionPlan"
                                        };
                                        context.ResponsablesAccionesPlan.Add(respon);
                                        context.SaveChanges();
                                        //Agregar perfil sobre un usuario Administrador existente
                                        AgregarPerfilPlanesDeAccionN(existeResponsable.CURRENT_IDEMPLEADOLOG);
                                    }
                                }
                                //Si NO existe en Administrador, se inserta primero en empleado y despues en administrador y se obtiene el IdAdministrador
                                //Se inserta en Résponsable y se obtiene su IdResponsable
                                //se Inserta Permisos Perfil
                                //Se inserta relacion REsponsableAccion
                                else
                                {
                                    //Se inserta en Empleados - BAsico -
                                    ML.Administrador empleadoAdd = new ML.Administrador();
                                    empleadoAdd.Nombre = responsable.Responsable.Nombre;
                                    empleadoAdd.ApellidoPaterno = "";
                                    empleadoAdd.ApellidoMaterno = "";
                                    empleadoAdd.Correo = responsable.Responsable.Email;
                                    empleadoAdd.UsuarioCreacion = UsuarioActual;
                                    empleadoAdd.FechaHoraCreacion = DateTime.Now;
                                    empleadoAdd.ProgramaCreacion = "Modulo Planes de Acción Alta - Responsable Alta Empleado";
                                    var altaEmpleado = Empleado.AddForAdmin(empleadoAdd);


                                    //Se inserta en Admin y se obtiene el IdAdministrador                                                                          
                                    //administrador.UserName, 
                                    //administrador.Password, 
                                    //administrador.Company.CompanyId, 
                                    //IdAdminCreate
                                    ML.Administrador adminAdd = new ML.Administrador();
                                    adminAdd.Empleado = new ML.Empleado();
                                    adminAdd.Empleado.IdEmpleado = altaEmpleado.IdEmpleadoFromSP;
                                    adminAdd.PerfilD4U = new ML.PerfilD4U();
                                    adminAdd.PerfilD4U.IdPerfil = 9;//perfi de Administracion de Planes de Acción
                                    adminAdd.TipoEstatus = new ML.TipoEstatus();
                                    adminAdd.TipoEstatus.IdEstatus = 1;
                                    adminAdd.UsuarioCreacion = UsuarioActual;
                                    adminAdd.FechaHoraCreacion = DateTime.Now;
                                    adminAdd.ProgramaCreacion = "Modulo Planes de Acción Alta - Responsable Alta Administrador";
                                    adminAdd.UserName = responsable.Responsable.Email;
                                    adminAdd.Password = BL.Administrador.GeneratePasswordResetToken(altaEmpleado.IdEmpleadoFromSP);
                                    adminAdd.Company = new ML.Company();
                                    adminAdd.Company.CompanyId = 1;                                   
                                    var altaAdministrador = BL.Administrador.AddAdminModPlanes(adminAdd, IdUsuarioAcyual);

                                    if (altaAdministrador.Correct)
                                    {
                                        //Se agregan los perfiles de seguimiento de Acciones al IdAdministrador
                                        AgregarPerfilPlanesDeAccionN(altaAdministrador.UltimoAdminInsertado);

                                        DL.Responsable responsablePA = new DL.Responsable()
                                        {
                                            IdAdministrador = altaAdministrador.UltimoAdminInsertado,
                                            Nombre = responsable.Responsable.Nombre,
                                            ApellidoPaterno = responsable.Responsable.ApellidoPaterno,
                                            ApellidoMaterno = responsable.Responsable.ApellidoMaterno,
                                            Email = responsable.Responsable.Email,
                                            FechaHoraCreacion = DateTime.Now,
                                            UsuarioCreacion = UsuarioActual,
                                            ProgramaCreacion = "Modulo Planes de Acción Alta - Responsable"
                                        };
                                        context.Responsable.Add(responsablePA);
                                        context.SaveChanges();
                                        var idresponsable = context.Responsable.Max(o => o.IdResponsable);

                                        DL.ResponsablesAccionesPlan respon = new DL.ResponsablesAccionesPlan()
                                        {
                                            IdAccionesPlan = idAccionPlan,
                                            IdResponsable = idresponsable,
                                            FechaHoraCreacion = DateTime.Now,
                                            UsuarioCreacion = UsuarioActual,
                                            ProgramaCreacion = "Modulo Planes de Acción Alta - Responsable AccionPlan"
                                        };
                                        context.ResponsablesAccionesPlan.Add(respon);
                                        context.SaveChanges();
                                    }
                                    else
                                    {
                                        result.Correct = false;
                                        transaction.Rollback();
                                        return result;

                                }
                            }
                        }
                    }  
                    result.Correct = true;
                }
            catch (Exception aE)
            {
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
				transaction.Rollback();
            }
                    if (result.Correct)
                    {
                        transaction.Commit();
                        //TriggerNotificacionInicial(idPlanDeAccion);
                        BackgroundJob.Enqueue(() => NotificacionInicialResponsables(idPlanDeAccion));
                    }

			}
			}
            return result;
        }        
		/// <summary>
        /// Obtiene los rangos establecidos para los planes de acción
        /// </summary>
        /// <returns>Objeto ML.Result</returns>
        public static ML.Result GetRangos()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var dataRangos = context.Rango.ToList();
                    if (dataRangos != null)
                    {
                        foreach (var item in dataRangos)
                        {
                            ML.Rango rango = new ML.Rango();
                            rango.IdRango = item.IdRango;
                            rango.Descripcion = item.Descripcion.Replace("\r\n", "");
                            rango.Desde = (int)item.Desde;
                            rango.Hasta = (int)item.Hasta;
                            result.Objects.Add(rango);
                        }
                        if (dataRangos.Count == 0)
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se encontraron rangos";
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Falló la consulta de rangos";
                    }
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
            }
            result.Correct = true;
            return result;
        }
        /// <summary>
        /// Obtiene las acciones preguardadas para una encuesta
        /// Las acciones tipo 2 son las del catalogo global
        /// </summary>
        /// <param name="accionDeMejora">Objeto ML.AccionDeMejora</param>
        /// <returns>Objeto ML.Result</returns>
        public static ML.Result GetAcciones(ML.AccionDeMejora accionDeMejora)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    if (accionDeMejora.Encuesta.IdEncuesta == 0)// Son las acciones genericas
                    {
                        var dataAcciones = context.Acciones.
                        Where(o =>
                        o.IdEstatus == 1 && o.Tipo == 2).ToList().OrderBy(o => o.IdCategoria);
                        if (dataAcciones != null)
                        {
                            foreach (var item in dataAcciones)
                            {
                                ML.AccionDeMejora accion = new ML.AccionDeMejora();
                                accion.IdAccionDeMejora = item.IdAccion;
                                accion.Descripcion = item.Descripcion;
                                var rango = context.Rango.Where(o => o.IdRango == item.IdRango).FirstOrDefault();
                                accion.Rango.IdRango = rango.IdRango;
                                accion.Rango.Descripcion = rango.Descripcion;
                                var categoria = context.Categoria.Where(o => o.IdCategoria == item.IdCategoria).FirstOrDefault();
                                accion.Categoria.IdCategoria = categoria.IdCategoria;
                                accion.Categoria.Nombre = categoria.Nombre;
                                result.Objects.Add(accion);
                            }
                            result.Correct = true;
                        }
                    }
                    else // Son las acciones para una encuesta especifica
                    {
                        //Acciones especificas
                        var dataAcciones = context.Acciones.
                        Where(o =>
                        o.IdEstatus == 1 && o.Tipo == 1 && 
                        o.IdEncuesta == accionDeMejora.Encuesta.IdEncuesta &&
                        o.IdBaseDeDatos == accionDeMejora.BasesDeDatos.IdBaseDeDatos &&
                        o.AnioAplicacion == accionDeMejora.AnioAplicacion).ToList().OrderBy(o => o.IdCategoria);
                        if (dataAcciones != null)
                        {
                            foreach (var item in dataAcciones)
                            {
                                ML.AccionDeMejora accion = new ML.AccionDeMejora();
                                accion.IdAccionDeMejora = item.IdAccion;
                                accion.Descripcion = item.Descripcion;
                                var rango = context.Rango.Where(o => o.IdRango == item.IdRango).FirstOrDefault();
                                accion.Rango.IdRango = rango.IdRango;
                                accion.Rango.Descripcion = rango.Descripcion;
                                var categoria = context.Categoria.Where(o => o.IdCategoria == item.IdCategoria).FirstOrDefault();
                                accion.Categoria.IdCategoria = categoria.IdCategoria;
                                accion.Categoria.Nombre = categoria.Nombre;
                                result.Objects.Add(accion);
                            }
                            result.Correct = true;
                        }
                        //Acciones globales
                        var dataAccionesGlobales = context.Acciones.
                        Where(o =>
                        o.IdEstatus == 1 && o.Tipo == 2).ToList().OrderBy(o => o.IdCategoria);
                        if (dataAccionesGlobales != null)
                        {
                            foreach (var item in dataAccionesGlobales)
                            {
                                ML.AccionDeMejora accion = new ML.AccionDeMejora();
                                accion.IdAccionDeMejora = item.IdAccion;
                                accion.Descripcion = item.Descripcion;
                                accion.Categoria.IdCategoria = item.Categoria.IdCategoria;
                                var rango = context.Rango.Where(o => o.IdRango == item.IdRango).FirstOrDefault();
                                accion.Rango.IdRango = rango.IdRango;
                                accion.Rango.Descripcion = rango.Descripcion;
                                var categoria = context.Categoria.Where(o => o.IdCategoria == item.IdCategoria).FirstOrDefault();
                                accion.Categoria.IdCategoria = categoria.IdCategoria;
                                accion.Categoria.Nombre = categoria.Nombre;
                                result.Objects.Add(accion);
                            }
                            result.Correct = true;
                        }
                    }
                    result.Atachment.Add(BL.PlanesDeAccion.CrearGridAcciones(result.Objects));
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
            }
            return result;
        }
        /// <summary>
        /// Crea el archivo xlsx con la informacion de las acciones para su descarga
        /// </summary>
        /// <param name="Objects"></param>
        /// <returns></returns>
        public static string CrearGridAcciones(List<object> Objects)
        {
            string ruta = string.Empty;
            List<string> ListIdAccion = new List<string>();
            List<string> ListDescripcion = new List<string>();
            List<string> ListCategoria = new List<string>();
            List<string> ListRangos = new List<string>();
            CatalogosValidacionExcel_JAMG96_Net_Framework.Result result = new CatalogosValidacionExcel_JAMG96_Net_Framework.Result();
            try
            {
                List<CatalogosValidacionExcel_JAMG96_Net_Framework.ModelValidator> modelValidators = new List<CatalogosValidacionExcel_JAMG96_Net_Framework.ModelValidator>();
                foreach (ML.AccionDeMejora item in Objects)
                {
                    ListIdAccion.Add(item.IdAccionDeMejora.ToString());
                    ListDescripcion.Add(item.Descripcion);
                    ListCategoria.Add(item.Categoria.Nombre);
                    ListRangos.Add(item.Rango.Descripcion);
                }
                CatalogosValidacionExcel_JAMG96_Net_Framework.GridBuilder gridBuilder = new CatalogosValidacionExcel_JAMG96_Net_Framework.GridBuilder();
                modelValidators.Add(new CatalogosValidacionExcel_JAMG96_Net_Framework.ModelValidator() { AnchoColumna = 10, Catalogo = ListIdAccion, CeldaValidar = "A1", HeadelCol = "Id", HexColorHead = "" });
                modelValidators.Add(new CatalogosValidacionExcel_JAMG96_Net_Framework.ModelValidator() { AnchoColumna = 140, Catalogo = ListDescripcion, CeldaValidar = "B1", HeadelCol = "Descripción", HexColorHead = "" });
                modelValidators.Add(new CatalogosValidacionExcel_JAMG96_Net_Framework.ModelValidator() { AnchoColumna = 70, Catalogo = ListCategoria, CeldaValidar = "C1", HeadelCol = "Categoria", HexColorHead = "" });
                modelValidators.Add(new CatalogosValidacionExcel_JAMG96_Net_Framework.ModelValidator() { AnchoColumna = 15, Catalogo = ListRangos, CeldaValidar = "D1", HeadelCol = "Rango", HexColorHead = "" });

                result = gridBuilder.CreateExcelWithData(modelValidators, @"\\\\10.5.2.101\\" + ConfigurationManager.AppSettings["templateLocation"].ToString() + @"\\resources\\Grids\\");
                if (result.Correct)
                    ruta = result.SuccessMessage;
                else
                    ruta = result.ErrorMessage;
                ruta = ruta.Replace(@"\\\\10.5.2.101\\" + ConfigurationManager.AppSettings["templateLocation"].ToString(), ConfigurationManager.AppSettings["urlTemplateLocation"].ToString());
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
            }
            return ruta;
        }
        /// <summary>
        /// Obtiene las acciones de ayuda
        /// </summary>
        /// <param name="accionDeMejora">Objeto ML.AccionDeMejora</param>
        /// <returns>Objeto ML.Result</returns>
        public static ML.Result GetAccionesAyuda(ML.AccionDeMejora accionDeMejora)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var dataAcciones = context.Acciones.
                        Where(o =>
                        o.IdEstatus == 1 && o.Tipo == 2).ToList().OrderBy(o => o.IdCategoria);
                    if (dataAcciones != null)
                    {
                        foreach (var item in dataAcciones)
                        {
                            ML.AccionDeMejora accion = new ML.AccionDeMejora();
                            accion.IdAccionDeMejora = item.IdAccion;
                            accion.Descripcion = item.Descripcion;
                            accion.Categoria.IdCategoria = (int)item.IdCategoria;
                            accion.Categoria.Descripcion = item.Categoria.Nombre;
                            accion.Rango.IdRango = item.Rango.IdRango;
                            accion.Rango.Descripcion = item.Rango.Descripcion;
                            result.Objects.Add(accion);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
            }
            return result;
        }
        /// <summary>
        /// Agrega una nueva acción de mejora
        /// </summary>
        /// <param name="accionDeMejora" type="ML.AccionDeMejora"></param>
        /// <param name="UsuarioActual" type="String"></param>
        /// <returns>Objeto ML.Result</returns>
        public static ML.Result AddAccion(ML.AccionDeMejora accionDeMejora, string UsuarioActual)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    if (accionDeMejora.Encuesta.IdEncuesta == 0)//Acciones globales (para que cuando haya clima dinamico se diferencien del clima original se guarda el id de encuesta)
                    {
                        if (accionDeMejora.IdAccionDeMejora == 0)
                        {
                            DL.Acciones accion = new DL.Acciones()
                            {
                                Descripcion = accionDeMejora.Descripcion,
                                IdRango = accionDeMejora.Rango.IdRango,
                                IdEstatus = accionDeMejora.Estatus.IdEstatus,
                                IdCategoria = accionDeMejora.Categoria.IdCategoria,
                                IdEncuesta = 1,
                                //IdEncuesta = accionDeMejora.Encuesta.IdEncuesta,
                                //IdBaseDeDatos = accionDeMejora.BasesDeDatos.IdBaseDeDatos,
                                //AnioAplicacion = accionDeMejora.AnioAplicacion,
                                Tipo = 2,
                                FechaHoraCreacion = DateTime.Now,
                                UsuarioCreacion = UsuarioActual,
                                ProgramaCreacion = "Modulo Planes de Acción - Acciones Globales"
                            };
                            context.Acciones.Add(accion);
                            context.SaveChanges();
                            BL.NLogGeneratorFile.nlogPlanesDeAccion.Info("Se ha agregado exitosamente la acccion de ayuda: " + accion.IdAccion);
                            result.Correct = true;
                            return result;
                        }
                        if (accionDeMejora.IdAccionDeMejora > 0)
                        {
                            var Accion = context.Acciones.Where(o => o.IdAccion == accionDeMejora.IdAccionDeMejora).FirstOrDefault();
                            if (Accion != null)
                            {
                                Accion.Descripcion = accionDeMejora.Descripcion;
                                Accion.IdRango = accionDeMejora.Rango.IdRango;
                                Accion.IdEstatus = accionDeMejora.Estatus.IdEstatus;
                                Accion.IdCategoria = accionDeMejora.Categoria.IdCategoria;
                                Accion.Tipo = 2;
                                Accion.FechaHoraModificacion = DateTime.Now;
                                Accion.UsuarioModificacion = UsuarioActual;
                                Accion.ProgramaModificacion = "Modulo Planes de Acción (Acciones de ayuda)";
                            }
                            context.SaveChanges();
                            BL.NLogGeneratorFile.nlogPlanesDeAccion.Info("Se ha actualizado exitosamente la accion de ayuda: " + Accion.IdAccion);
                            result.Correct = true;
                            return result;
                        }
                    }
                    else //Acciones especificas
                    {
                        if (accionDeMejora.IdAccionDeMejora == 0)// Insert
                        {
                            DL.Acciones accion = new DL.Acciones()
                            {
                                Descripcion = accionDeMejora.Descripcion,
                                IdRango = accionDeMejora.Rango.IdRango,
                                IdEstatus = accionDeMejora.Estatus.IdEstatus,
                                IdCategoria = accionDeMejora.Categoria.IdCategoria,
                                IdEncuesta = accionDeMejora.Encuesta.IdEncuesta,
                                IdBaseDeDatos = accionDeMejora.BasesDeDatos.IdBaseDeDatos,
                                AnioAplicacion = accionDeMejora.AnioAplicacion,
                                Tipo = accionDeMejora.Tipo,
                                FechaHoraCreacion = DateTime.Now,
                                UsuarioCreacion = UsuarioActual,
                                ProgramaCreacion = "Modulo Planes de Acción"
                            };
                            context.Acciones.Add(accion);
                            context.SaveChanges();
                            BL.NLogGeneratorFile.nlogPlanesDeAccion.Info("Se ha agregado exitosamente la accion: " + accion.IdAccion);
                            result.Correct = true;
                            result.NewId = accion.IdAccion;
                        }
                        if (accionDeMejora.IdAccionDeMejora > 0)// Update
                        {
                            var Accion = context.Acciones.Where(o => o.IdAccion == accionDeMejora.IdAccionDeMejora).FirstOrDefault();
                            if (Accion != null)
                            {
                                Accion.Descripcion = accionDeMejora.Descripcion;
                                Accion.IdRango = accionDeMejora.Rango.IdRango;
                                Accion.IdEstatus = accionDeMejora.Estatus.IdEstatus;
                                Accion.IdCategoria = accionDeMejora.Categoria.IdCategoria;
                                Accion.IdEncuesta = accionDeMejora.Encuesta.IdEncuesta;
                                Accion.IdBaseDeDatos = accionDeMejora.BasesDeDatos.IdBaseDeDatos;
                                Accion.AnioAplicacion = accionDeMejora.AnioAplicacion;
                                Accion.Tipo = accionDeMejora.Tipo;
                                Accion.FechaHoraModificacion = DateTime.Now;
                                Accion.UsuarioModificacion = UsuarioActual;
                                Accion.ProgramaModificacion = "Modulo Planes de Acción";
                            }
                            context.SaveChanges();
                            BL.NLogGeneratorFile.nlogPlanesDeAccion.Info("Se ha actualizado exitosamente la acción: " + Accion.IdAccion);
                            result.Correct = true;
                            result.NewId = Accion.IdAccion;
                        }
                    }
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
            }
            return result;
        }
        /// <summary>
        /// Eliminación lógica de una acción
        /// </summary>
        /// <param name="IdAccion"></param>
        /// <param name="UsuarioActual"></param>
        /// <returns></returns>
        public static ML.Result DeleteAccion(int IdAccion, string UsuarioActual)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var dataAccion = context.Acciones.Where(o => o.IdAccion == IdAccion).FirstOrDefault();
                    if (dataAccion != null)
                    {
                        dataAccion.IdEstatus = 2;
                        dataAccion.FechaHoraEliminacion = DateTime.Now;
                        dataAccion.UsuarioEliminacion = UsuarioActual;
                        dataAccion.ProgramaEliminacion = "Modulo Planes de Acción";
                        context.SaveChanges();
                        BL.NLogGeneratorFile.nlogPlanesDeAccion.Info("La accion: " + IdAccion + " fue eliminada");
                        result.Correct = true;
                    }
                    else
                    {
                        BL.NLogGeneratorFile.nlogPlanesDeAccion.Error("No se ha encontrado la accion: " + IdAccion + " que se pretendia eliminar");
                        result.Correct = false;
                        result.ErrorMessage = "No se encontró la acción con el Id: " + IdAccion;
                    }
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
            }
            return result;
        }
        /// <summary>
        /// Re asigna una acción a una nueva categoria, con una nuevo Rango
        /// </summary>
        /// <param name="IdAccion"></param>
        /// <param name="IdCategoria"></param>
        /// <param name="IdRango"></param>
        /// <returns></returns>
        public static ML.Result ReAsignar(string IdAccion, string IdCategoria, string IdRango)
        {
            ML.Result result = new ML.Result();
            try
            {
                if (string.IsNullOrEmpty(IdAccion) || string.IsNullOrEmpty(IdCategoria) || string.IsNullOrEmpty(IdRango))
                {
                    result.Correct = false;
                    result.ErrorMessage = "Los valores enviados no pueden estar vacios";
                    return result;
                }
                int auxIdAccion = Convert.ToInt32(IdAccion);
                int auxIdCategoria = Convert.ToInt32(IdCategoria);
                int auxIdRango = Convert.ToInt32(IdRango);
                if (auxIdAccion == 0 || auxIdCategoria == 0 || auxIdRango == 0)
                {
                    result.Correct = false;
                    result.ErrorMessage = "Los valores enviados no pueden ser cero";
                    return result;
                }
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var data = context.Acciones.Where(o => o.IdAccion == auxIdAccion).FirstOrDefault();
                    if (data != null)
                    {
                        data.IdCategoria = auxIdCategoria;
                        data.IdRango = auxIdRango;
                        context.SaveChanges();
                        result.Correct = true;
                    }
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
            }
            return result;
        }
        /// <summary>
        /// Agrega un conjunto de acciones
        /// </summary>
        /// <param name="ListAcciones"></param>
        /// <param name="UsuarioActual"></param>
        /// <returns></returns>
        public static ML.Result AddAcciones(List<ML.AccionDeMejora> ListAcciones, string UsuarioActual)
        {
            ML.Result result = new ML.Result();
            bool status = true;
            try
            {
                foreach (var item in ListAcciones)
                {
                    var resultado = AddAccion(item, UsuarioActual);
                    if (!resultado.Correct)
                    {
                        status = false;
                        result.ErrorMessage = resultado.ErrorMessage;
                        result.ex = resultado.ex;
                        break;
                    }
                }
                result.Correct = status;
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
            }
            return result;
        }
        /// <summary>
        /// Agrega el perfil de administrador de Planes de Acción al admin seleccionado
        /// </summary>
        /// <param name="IdAdministrador"></param>
        /// <returns></returns>
        public static ML.Result AgregarPerfilPlanesDeAccion(int IdAdministrador)
        {
            ML.Result result = new ML.Result();
            try
            {
                List<string> acciones = new List<string>();
                acciones.Add("AdministrarAcciones");
                acciones.Add("CrearPlanes");
                acciones.Add("ListarPlanes");
                List<DL.PerfilModuloAccion> ListPerfilModuloAccion = new List<DL.PerfilModuloAccion>();
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    //select* from PerfilModulo where IdAdministrador = 56
                    var admin = context.Administrador.Where(o => o.IdAdministrador == IdAdministrador).FirstOrDefault();
                    DL.PerfilModulo perfilModulo = new DL.PerfilModulo()
                    {
                        IdPerfil = admin.IdPerfil,
                        IdModulo = 7,//Id del Modulo de planes de accion
                        IdAdministrador = admin.IdAdministrador
                    };
                    context.PerfilModulo.Add(perfilModulo);
                    context.SaveChanges();
                    BL.NLogGeneratorFile.nlogPlanesDeAccion.Info("Se agrego el PerfilModulo con el nuevo modulo al administrador: " + IdAdministrador);
                    foreach (var accion in acciones)
                    {
                        DL.PerfilModuloAccion perfilModuloAccion = new DL.PerfilModuloAccion()
                        {
                            IdPerfilModulo = perfilModulo.IdPerfilModulo,
                            Accion = accion
                        };
                        ListPerfilModuloAccion.Add(perfilModuloAccion);
                    }
                    context.PerfilModuloAccion.AddRange(ListPerfilModuloAccion);

                    context.SaveChanges();
                    BL.NLogGeneratorFile.nlogPlanesDeAccion.Info("Se agregaron las acciones en la tabla PerfilModuloAccion al administrador: " + IdAdministrador);
                    result.Correct = true;
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdAdministrador"></param>
        /// <returns></returns>
        public static ML.Result AgregarPerfilPlanesDeAccionN(int IdAdministrador)
        {
            ML.Result result = new ML.Result();
            try
            {
                List<string> acciones = new List<string>();
                acciones.Add("Seguimiento");
                //acciones.Add("CrearPlanes");
                //acciones.Add("ListarPlanes");
                List<DL.PerfilModuloAccion> ListPerfilModuloAccion = new List<DL.PerfilModuloAccion>();
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    //select* from PerfilModulo where IdAdministrador = 56
                    var admin = context.Administrador.Where(o => o.IdAdministrador == IdAdministrador).FirstOrDefault();
                    DL.PerfilModulo perfilModulo = new DL.PerfilModulo()
                    {
                        IdPerfil = admin.IdPerfil,
                        IdModulo = 7,//Id del Modulo de planes de accion
                        IdAdministrador = admin.IdAdministrador
                    };
                    context.PerfilModulo.Add(perfilModulo);
                    context.SaveChanges();
                    BL.NLogGeneratorFile.nlogPlanesDeAccion.Info("Se agrego el PerfilModulo con el nuevo modulo al administrador: " + IdAdministrador);
                    foreach (var accion in acciones)
                    {
                        DL.PerfilModuloAccion perfilModuloAccion = new DL.PerfilModuloAccion()
                        {
                            IdPerfilModulo = perfilModulo.IdPerfilModulo,
                            Accion = accion
                        };
                        ListPerfilModuloAccion.Add(perfilModuloAccion);
                    }
                    context.PerfilModuloAccion.AddRange(ListPerfilModuloAccion);

                    context.SaveChanges();
                    BL.NLogGeneratorFile.nlogPlanesDeAccion.Info("Se agregaron las acciones en la tabla PerfilModuloAccion al administrador: " + IdAdministrador);
                    result.Correct = true;
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
            }
            return result;
        }
        #endregion Generales Modulo


        /// <summary>
        /// 
        /// </summary>
        /// <param name="aEmail"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static ML.Result ExisteResponsable(string aEmail,DL.RH_DesEntities context)
        {
            ML.Result result = new ML.Result();
            try
            {
                //using ( = new DL.RH_DesEntities())
                //{
                    var existente = false;                   
                    var existeResponsableBD = context.Responsable.Where(o => o.Email == aEmail).FirstOrDefault();
                    if (existeResponsableBD != null)
                    {
                        result.CURRENTIDADMINLOG = existeResponsableBD.IdResponsable;
						   result.CURRENT_IDEMPLEADOLOG = (Int32)existeResponsableBD.IdAdministrador;
                        existente = true;
                    }
                    result.Correct = existente;
                //}
                    
            }
            catch (Exception aE)
            {
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
            }
            return result;
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aEmail"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static ML.Result ExisteResponsableenAdministrador(string aEmail,DL.RH_DesEntities context)
        {
            ML.Result result = new ML.Result();
            try
            {
               // using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                //{
                    var existeResAdm = context.Administrador.Where(o => o.UserName == aEmail && o.IdEstatus == 1).FirstOrDefault();
                    if (existeResAdm != null)
                    {                                                
                        result.Correct = true;
                        result.CURRENTIDADMINLOG = existeResAdm.IdAdministrador;                        
                    }
                    else
                    {
                        result.Correct = false;                        
                    }
                //}

                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }

		/// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ML.Result GetPeriodicidad()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var dataPeriodicidad = context.Periodicidad.ToList();
                    if (dataPeriodicidad != null)
                    {
                        foreach (var item in dataPeriodicidad)
                        {
                            ML.Periodicidad periodicidad = new ML.Periodicidad();
                            periodicidad.IdPeriodicidad = item.IdPeriodicidad;
                            periodicidad.Descripcion = item.Descripcion;
                            result.Objects.Add(periodicidad);
                        }
                        if (dataPeriodicidad.Count == 0)
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se encontraron periodos";
                        }

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Falló la consulta de Periodicidad";
                    }
                }

            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE,new StackTrace());
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
            }
            result.Correct = true;
            return result;
        }
        #region Notificaciones
        /// <summary>
        /// Disparador del job para el envio de notificacion inicial
        /// </summary>
        /// <param name="IdPlanDeAccion"></param>
        public static void TriggerNotificacionInicial(int IdPlanDeAccion)
        {
            BackgroundJob.Enqueue(() => NotificacionInicialResponsables(IdPlanDeAccion));
        }
        /// <summary>
        /// Disparador del job para el envio de notificacion previa
        /// </summary>
        public static void TriggerNotificacionPrevia()
        {
            BackgroundJob.Enqueue(() => NotificacionPrevia());
        }
        /// <summary>
        /// Disparador del job para el envio de notificacion cuando no se ha registrado el avance inicial
        /// </summary>
        public static void TriggerNotificacionSinAvanceInicial()
        {
            BackgroundJob.Enqueue(() => NotificacionNoRegistraAvanceInicial());
        }
        /// <summary>
        /// Disparador del job para el envio de notificacion agradecimiento
        /// </summary>
        public static void TriggerNotificacionAgradecimiento()
        {
            BackgroundJob.Enqueue(() => NotificacionAgradecimiento());
        }

        /// <summary>
        /// Envia un email de notificacion a los usuarios seleccionados con la plantilla configurada
        /// </summary>
        /// <param name="IdPlanDeAccion"></param>
        /// <param name="Asunto"></param>
        /// <param name="Plantilla"></param>
        /// <param name="prioridad"></param>
        public static void EnvioEmailCustom(int IdPlanDeAccion, int prioridad, string Asunto, string Plantilla)
        {
            try
            {
                List<string> EmailDestinatario = new List<string>();
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var PlanDeAccion = context.PlanDeAccion.Where(o => o.IdPlanDeAccion == IdPlanDeAccion).FirstOrDefault();
                    var AccionesPlan = context.AccionesPlan.Where(o => o.IdPlanDeAccion == PlanDeAccion.IdPlanDeAccion).ToList();
                    foreach (var accionPlan in AccionesPlan)
                    {
                        var responsablesAccion = context.ResponsablesAccionesPlan.Where(o => o.IdAccionesPlan == accionPlan.IdAccionesPlan).ToList();
                        foreach (var responsable in responsablesAccion)
                        {
                            var usuarioResponsable = context.Responsable.Where(o => o.IdResponsable == responsable.IdResponsable).FirstOrDefault();
                            var accountResponsable = context.Administrador.Where(o => o.IdAdministrador == usuarioResponsable.IdAdministrador).FirstOrDefault();
                            var relacional = context.ResponsablesAccionesPlan.Where(o => o.IdResponsable == usuarioResponsable.IdResponsable).ToList();
                            List<ML.AccionDeMejora> ListAcciones = new List<ML.AccionDeMejora>();
                            foreach (var item in relacional)
                            {
                                var acciones = context.AccionesPlan.Where(o => o.IdAccionesPlan == item.IdAccionesPlan && o.IdPlanDeAccion == PlanDeAccion.IdPlanDeAccion).ToList();
                                foreach (var elem in acciones)
                                {
                                    var accion = context.Acciones.Where(o => o.IdAccion == elem.IdAccion).FirstOrDefault();
                                    ML.AccionDeMejora accionDeMejora = new ML.AccionDeMejora();
                                    accionDeMejora.IdAccionDeMejora = accion.IdAccion;
                                    accionDeMejora.Descripcion = accion.Descripcion;
                                    accionDeMejora.Categoria.IdCategoria = (int)accion.IdCategoria;
                                    ListAcciones.Add(accionDeMejora);
                                }
                            }
                            if (!EmailDestinatario.Contains(usuarioResponsable.Email))
                            {
                                BL.Email.EnvioNotificacionesCustom(1, usuarioResponsable.Email, ListAcciones, usuarioResponsable, accountResponsable, IdPlanDeAccion, prioridad, Asunto, Plantilla);
                                EmailDestinatario.Add(usuarioResponsable.Email);
                            }
                        }
                    }
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
            }
        }
        /// <summary>
        /// Envía un email de notificación a los responsables las acciones al guardar un nuevo Plan
        /// trigger: Al guardar un plan de accion
        /// </summary>
        /// <returns></returns>
        public static void NotificacionInicialResponsables(int IdPlanDeAccion)
        {
            try
            {
                List<string> EmailDestinatario = new List<string>();
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var PlanDeAccion = context.PlanDeAccion.Where(o => o.IdPlanDeAccion == IdPlanDeAccion).FirstOrDefault();
                    var AccionesPlan = context.AccionesPlan.Where(o => o.IdPlanDeAccion == PlanDeAccion.IdPlanDeAccion).ToList();
                    foreach (var accionPlan in AccionesPlan)
                    {
                        var responsablesAccion = context.ResponsablesAccionesPlan.Where(o => o.IdAccionesPlan == accionPlan.IdAccionesPlan).ToList();
                        foreach (var responsable in responsablesAccion)
                        {
                            var usuarioResponsable = context.Responsable.Where(o => o.IdResponsable == responsable.IdResponsable).FirstOrDefault();
                            var accountResponsable = context.Administrador.Where(o => o.IdAdministrador == usuarioResponsable.IdAdministrador).FirstOrDefault();
                            var relacional = context.ResponsablesAccionesPlan.Where(o => o.IdResponsable == usuarioResponsable.IdResponsable).ToList();
                            List<ML.AccionDeMejora> ListAcciones = new List<ML.AccionDeMejora>();
                            foreach (var item in relacional)
                            {
                                var acciones = context.AccionesPlan.Where(o => o.IdAccionesPlan == item.IdAccionesPlan && o.IdPlanDeAccion == PlanDeAccion.IdPlanDeAccion).ToList();
                                foreach (var elem in acciones)
                                {
                                    var accion = context.Acciones.Where(o => o.IdAccion == elem.IdAccion).FirstOrDefault();
                                    ML.AccionDeMejora accionDeMejora = new ML.AccionDeMejora();
                                    accionDeMejora.IdAccionDeMejora = accion.IdAccion;
                                    accionDeMejora.Descripcion = accion.Descripcion;
                                    accionDeMejora.Categoria.IdCategoria = (int)accion.IdCategoria;
                                    ListAcciones.Add(accionDeMejora);
                                }
                            }
                            if (!EmailDestinatario.Contains(usuarioResponsable.Email))
                            {
                                BL.Email.EnvioNotificaciones(1, usuarioResponsable.Email, ListAcciones, usuarioResponsable, accountResponsable, IdPlanDeAccion);
                                EmailDestinatario.Add(usuarioResponsable.Email);
                            }
                        }
                    }
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
            }
        }
        /// <summary>
        /// Envía un email de notificación a los responsables las acciones una semana antes de iniciar la accion que le corresponde
        /// trigger: Una semana antes del inicio de las acciones
        /// </summary>
        public static void NotificacionPrevia()
        {
            try
            {
                List<int> HistoricoIdResponsable = new List<int>();
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var auxDate = DateTime.Now.AddDays(7);
                    var AccionesPlan = context.AccionesPlan.Where(o => o.FechaInicio <= auxDate && o.NotificacionPrevia != 1).ToList();
                    if (AccionesPlan != null)
                    {
                        foreach (var accionPlan in AccionesPlan)
                        {
                            var Accion = context.Acciones.Where(o => o.IdAccion == accionPlan.IdAccion).FirstOrDefault();
                            if (Accion != null)
                            {
                                var Responsables = context.ResponsablesAccionesPlan.Where(o => o.IdAccionesPlan == accionPlan.IdAccionesPlan).ToList();
                                foreach (var item in Responsables)
                                {
                                    if (HistoricoIdResponsable.Contains((int)item.IdResponsable) == false)
                                    {
                                        HistoricoIdResponsable.Add((int)item.IdResponsable);
                                        var PlanDeAccion = context.PlanDeAccion.Where(o => o.IdPlanDeAccion == accionPlan.IdPlanDeAccion).FirstOrDefault();
                                        var usuarioResponsable = context.Responsable.Where(o => o.IdResponsable == item.IdResponsable).FirstOrDefault();
                                        var accountResponsable = context.Administrador.Where(o => o.IdAdministrador == usuarioResponsable.IdAdministrador).FirstOrDefault();
                                        var relacional = context.ResponsablesAccionesPlan.Where(o => o.IdResponsable == usuarioResponsable.IdResponsable).ToList();
                                        List<ML.AccionDeMejora> ListAcciones = new List<ML.AccionDeMejora>();
                                        foreach (var elem in relacional)
                                        {
                                            var acciones = context.AccionesPlan.Where(o => o.IdAccionesPlan == elem.IdAccionesPlan && o.IdPlanDeAccion == PlanDeAccion.IdPlanDeAccion).ToList();
                                            foreach (var elem2 in acciones)
                                            {
                                                var accion = context.Acciones.Where(o => o.IdAccion == elem2.IdAccion).FirstOrDefault();
                                                ML.AccionDeMejora accionDeMejora = new ML.AccionDeMejora();
                                                accionDeMejora.IdAccionDeMejora = accion.IdAccion;
                                                accionDeMejora.Descripcion = accion.Descripcion;
                                                accionDeMejora.Categoria.IdCategoria = (int)accion.IdCategoria;
                                                ListAcciones.Add(accionDeMejora);
                                            }
                                        }
                                        bool status = BL.Email.EnvioNotificaciones(2, usuarioResponsable.Email, ListAcciones, usuarioResponsable, accountResponsable, PlanDeAccion.IdPlanDeAccion);
                                        if (status) //Envio exitoso
                                        {
                                            accionPlan.NotificacionPrevia = 1;
                                            context.SaveChanges();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
            }
        }
        /// <summary>
        /// Envia un email de notificacion a los responsables cuya acion ya inició pero no han registrado avance
        /// </summary>
        public static void NotificacionNoRegistraAvanceInicial()
        {
            try
            {
                List<int> HistoricoIdResponsable = new List<int>();
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    //Falta el filtro de que no haya registrado avance
                    var AccionesPlan = context.AccionesPlan.Where(o => o.FechaInicio == DateTime.Now && o.PorcentajeAvance <= 0).ToList();
                    if (AccionesPlan != null)
                    {
                        foreach (var accionPlan in AccionesPlan)
                        {
                            var Accion = context.Acciones.Where(o => o.IdAccion == accionPlan.IdAccion).FirstOrDefault();
                            if (Accion != null)
                            {
                                var Responsables = context.ResponsablesAccionesPlan.Where(o => o.IdAccionesPlan == accionPlan.IdAccionesPlan).ToList();
                                foreach (var item in Responsables)
                                {
                                    if (HistoricoIdResponsable.Contains((int)item.IdResponsable) == false)
                                    {
                                        var PlanDeAccion = context.PlanDeAccion.Where(o => o.IdPlanDeAccion == accionPlan.IdPlanDeAccion).FirstOrDefault();
                                        var usuarioResponsable = context.Responsable.Where(o => o.IdResponsable == item.IdResponsable).FirstOrDefault();
                                        var accountResponsable = context.Administrador.Where(o => o.IdAdministrador == usuarioResponsable.IdAdministrador).FirstOrDefault();
                                        var relacional = context.ResponsablesAccionesPlan.Where(o => o.IdResponsable == usuarioResponsable.IdResponsable).ToList();
                                        List<ML.AccionDeMejora> ListAcciones = new List<ML.AccionDeMejora>();
                                        foreach (var elem in relacional)
                                        {
                                            var acciones = context.AccionesPlan.Where(o => o.IdAccionesPlan == elem.IdAccionesPlan && o.IdPlanDeAccion == PlanDeAccion.IdPlanDeAccion).ToList();
                                            foreach (var elem2 in acciones)
                                            {
                                                var accion = context.Acciones.Where(o => o.IdAccion == elem2.IdAccion).FirstOrDefault();
                                                ML.AccionDeMejora accionDeMejora = new ML.AccionDeMejora();
                                                accionDeMejora.IdAccionDeMejora = accion.IdAccion;
                                                accionDeMejora.Descripcion = accion.Descripcion;
                                                accionDeMejora.Categoria.IdCategoria = (int)accion.IdCategoria;
                                                ListAcciones.Add(accionDeMejora);
                                            }
                                        }
                                        BL.Email.EnvioNotificaciones(3, usuarioResponsable.Email, ListAcciones, usuarioResponsable, accountResponsable, PlanDeAccion.IdPlanDeAccion);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
            }
        }

        /*
         * 1 Cuando el porcentaje de avance no corresponda a lo esperado es decir exista un retraso. 
         *      Por ejemplo cuando el avance deba ser al 25% el registro sea menor o cuando el avance deba estar al 50% o al 75% y de igual forma la captura sea menor.  
         */

        /// <summary>
        /// Envia un email de agradecimiento a los responsables cuya accion ha terminado
        /// </summary>
        public static void NotificacionAgradecimiento()
        {
            try
            {
                List<int> HistoricoIdResponsable = new List<int>();
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var AccionesPlan = context.AccionesPlan.Where(o => o.FechaInicio < DateTime.Now && o.Cumplimiento == 100 && o.NotificacionFinal != 1).ToList();
                    if (AccionesPlan != null)
                    {
                        foreach (var accionPlan in AccionesPlan)
                        {
                            var Accion = context.Acciones.Where(o => o.IdAccion == accionPlan.IdAccion).FirstOrDefault();
                            if (Accion != null)
                            {
                                var Responsables = context.ResponsablesAccionesPlan.Where(o => o.IdAccionesPlan == accionPlan.IdAccionesPlan).ToList();
                                foreach (var item in Responsables)
                                {
                                    if (HistoricoIdResponsable.Contains((int)item.IdResponsable) == false)
                                    {
                                        var PlanDeAccion = context.PlanDeAccion.Where(o => o.IdPlanDeAccion == accionPlan.IdPlanDeAccion).FirstOrDefault();
                                        var usuarioResponsable = context.Responsable.Where(o => o.IdResponsable == item.IdResponsable).FirstOrDefault();
                                        var accountResponsable = context.Administrador.Where(o => o.IdAdministrador == usuarioResponsable.IdAdministrador).FirstOrDefault();
                                        var relacional = context.ResponsablesAccionesPlan.Where(o => o.IdResponsable == usuarioResponsable.IdResponsable).ToList();
                                        List<ML.AccionDeMejora> ListAcciones = new List<ML.AccionDeMejora>();
                                        foreach (var elem in relacional)
                                        {
                                            var acciones = context.AccionesPlan.Where(o => o.IdAccionesPlan == elem.IdAccionesPlan && o.IdPlanDeAccion == PlanDeAccion.IdPlanDeAccion).ToList();
                                            foreach (var elem2 in acciones)
                                            {
                                                var accion = context.Acciones.Where(o => o.IdAccion == elem2.IdAccion).FirstOrDefault();
                                                ML.AccionDeMejora accionDeMejora = new ML.AccionDeMejora();
                                                accionDeMejora.IdAccionDeMejora = accion.IdAccion;
                                                accionDeMejora.Descripcion = accion.Descripcion;
                                                accionDeMejora.Categoria.IdCategoria = (int)accion.IdCategoria;
                                                ListAcciones.Add(accionDeMejora);
                                            }
                                        }
                                        var status = BL.Email.EnvioNotificaciones(5, usuarioResponsable.Email, ListAcciones, usuarioResponsable, accountResponsable, PlanDeAccion.IdPlanDeAccion);
                                        if (status) //Envio exitoso
                                        {
                                            accionPlan.NotificacionFinal = 1;
                                            context.SaveChanges();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
            }
        }
        /// <summary>
        /// Crea la vista web del correo de notificacion
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string CrearVistaWebEmail(string message)
        {
            int intento = 0;
            try
            {
                string uid = Guid.NewGuid().ToString();
                message = message.Replace("rutaEmailHTML", (ConfigurationManager.AppSettings["urlTemplateLocation"].ToString() + "/templates/enviados/email_" + uid + ".html"));
                var ruta = @"\\\\10.5.2.101\\"+ ConfigurationManager.AppSettings["templateLocation"].ToString() + @"\\templates\\enviados\\";
                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);
                File.WriteAllText(ruta + @"email_" + uid + ".html", message);
            }
            catch (Exception aE)
            {
                intento++;
                BL.NLogGeneratorFile.nlogPlanesDeAccion.Error("No se pudo crear la vista web del email de notificacion");
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                if (intento <= 2)
                    CrearVistaWebEmail(message);
                else
                    return message;
            }
            return message;
        }
        #endregion


        #region Seguimiento
        /// <summary>
        /// Obtiene los planes de accion a los cuales tiene acceso el usuario
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="IsResponsable"></param>
        /// <param name="IdResponsable"></param>
        /// <param name="isSA"></param>
        /// <returns></returns>
        public static ML.Result GetPlanes(int UserId, bool IsResponsable, string IdResponsable, bool isSA)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            List<int> ArrayIdPlan = new List<int>();
            try
            {
                int _idResponsable = Convert.ToInt32(IdResponsable);
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    // Si es un usuario responsable solo puede ver los planes en donde es participante
                    // Si es un usuario admnistrador de planes de accion puede solo ver los planes de acción que ha creado
                    // Si es un usuario SA puede ver todo
                    if (isSA)
                    {
                        var PlanesDeAccion = context.PlanDeAccion.ToList();
                        foreach (var plan in PlanesDeAccion)
                        {
                            ML.PlanDeAccion planDeAccion = new ML.PlanDeAccion();
                            planDeAccion.IdPlanDeAccion = plan.IdPlanDeAccion;
                            planDeAccion.Nombre = plan.Nombre;
                            planDeAccion.IdEncuesta = (int)plan.IdEncuesta;
                            planDeAccion.IdBaseDeDatos = (int)plan.IdBaseDeDatos;
                            planDeAccion.PorcentajeAvance = GetPorcentajeAvancePlan(plan.IdPlanDeAccion);
                            planDeAccion.ListJobsNotificaciones = BL.PlanesDeAccion.ObtenerNotificacionesProgramadas(planDeAccion.IdPlanDeAccion);
                            planDeAccion.sFechaHoraCreacion = Convert.ToString(plan.FechaHoraCreacion);

                            result.Objects.Add(planDeAccion);
                        }
                        result.Correct = true;
                    }
                    if (IsResponsable && !isSA)
                    {
                        var ResponsableAccionesPlan = context.ResponsablesAccionesPlan.Where(o => o.IdResponsable == _idResponsable);
                        foreach (var responsableAccionesPlan in ResponsableAccionesPlan)
                        {
                            var AccionesPlan = context.AccionesPlan.Where(o => o.IdAccionesPlan == responsableAccionesPlan.IdAccionesPlan);
                            foreach (var accionesPlan in AccionesPlan)
                            {
                                var PlanesDeAccion = context.PlanDeAccion.Where(o => o.IdPlanDeAccion == accionesPlan.IdPlanDeAccion);
                                foreach (var plan in PlanesDeAccion)
                                {
                                    ML.PlanDeAccion planDeAccion = new ML.PlanDeAccion();
                                    planDeAccion.IdPlanDeAccion = plan.IdPlanDeAccion;
                                    planDeAccion.Nombre = plan.Nombre;
                                    planDeAccion.IdEncuesta = (int)plan.IdEncuesta;
                                    planDeAccion.IdBaseDeDatos = (int)plan.IdBaseDeDatos;
                                    planDeAccion.PorcentajeAvance = GetPorcentajeAvancePlan(planDeAccion.IdPlanDeAccion);
                                    planDeAccion.ListJobsNotificaciones = BL.PlanesDeAccion.ObtenerNotificacionesProgramadas(planDeAccion.IdPlanDeAccion);
                                    planDeAccion.sFechaHoraCreacion = Convert.ToString(plan.FechaHoraCreacion);
                                    if (!ArrayIdPlan.Contains(planDeAccion.IdPlanDeAccion))
                                    {
                                        result.Objects.Add(planDeAccion);
                                        ArrayIdPlan.Add(planDeAccion.IdPlanDeAccion);
                                    }
                                }
                            }
                        }
                        result.Correct = true;
                    }
                    if (!IsResponsable && !isSA)
                    {
                        var PlanesDeAccion = context.PlanDeAccion.Where(o => o.IdUsuarioCreacion == UserId);
                        foreach (var plan in PlanesDeAccion)
                        {
                            ML.PlanDeAccion planDeAccion = new ML.PlanDeAccion();
                            planDeAccion.IdPlanDeAccion = plan.IdPlanDeAccion;
                            planDeAccion.Nombre = plan.Nombre;
                            planDeAccion.IdEncuesta = (int)plan.IdEncuesta;
                            planDeAccion.IdBaseDeDatos = (int)plan.IdBaseDeDatos;
                            planDeAccion.PorcentajeAvance = GetPorcentajeAvancePlan(plan.IdPlanDeAccion);
                            planDeAccion.ListJobsNotificaciones = BL.PlanesDeAccion.ObtenerNotificacionesProgramadas(planDeAccion.IdPlanDeAccion);
                            planDeAccion.sFechaHoraCreacion = Convert.ToString(plan.FechaHoraCreacion);

                            result.Objects.Add(planDeAccion);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
            }
            return result;
        }
        /// <summary>
        /// Obtiene el porcentaje de avance general en un plan de acción
        /// </summary>
        /// <param name="IdPlan"></param>
        /// <returns></returns>
        public static decimal GetPorcentajeAvancePlan(int IdPlan)
        {
            decimal result = 0;
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var accionesPlan = context.AccionesPlan.Where(o => o.IdPlanDeAccion == IdPlan).ToList();
                    if (accionesPlan.Count > 0)
                    {
                        foreach (var accionPlan in accionesPlan)
                        {
                            result += accionPlan.Cumplimiento == null ? 0 : Convert.ToDecimal(accionPlan.Cumplimiento);
                        }
                        result /= accionesPlan.Count;
                    }
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                result = 0;
            }
            return result;
        }
        /// <summary>
        /// Obtiene las acciones de un responsable
        /// </summary>
        /// <param name="IdResponsable"></param>
        /// <param name="IdPlan"></param>
        /// <returns></returns>
        public static ML.Result GetAccionesByIdResponsable(int IdPlan, int IdResponsable)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    if (IdResponsable > 0)
                    {
                        var ResponsableAcciones = context.ResponsablesAccionesPlan.Where(o => o.IdResponsable == IdResponsable);
                        foreach (var responsablesAcciones in ResponsableAcciones)
                        {
                            var AccionesPlan = context.AccionesPlan.Where(o => o.IdAccionesPlan == responsablesAcciones.IdAccionesPlan && o.IdPlanDeAccion == IdPlan);
                            foreach (var accionPlan in AccionesPlan)
                            {
                                int IdPlanDeAccion = (int)accionPlan.IdPlanDeAccion;
                                var PlanDeAccion = context.PlanDeAccion.Where(o => o.IdPlanDeAccion == IdPlanDeAccion).FirstOrDefault();
                                string NombrePlanDeAccion = PlanDeAccion.Nombre;
                                string FechaInicio = accionPlan.FechaInicio.ToString().Substring(0, 10);
                                string FechaFin = accionPlan.FechaFin.ToString().Substring(0, 10);
                                string Objetivo = accionPlan.Objetivo;
                                string Comentarios = accionPlan.Comentarios;
                                string Meta = accionPlan.Meta;
                                var ResponsableAccionesPlan = accionPlan.ResponsablesAccionesPlan.FirstOrDefault();
                                int IdRes = (int)ResponsableAccionesPlan.IdResponsable;
                                var Responsable = context.Responsable.Where(o=> o.IdResponsable == IdRes).FirstOrDefault();
                                string NombreResponsable = string.Concat(Responsable.Nombre, " ", Responsable.ApellidoPaterno, " ", Responsable.ApellidoMaterno);
                                string EmailResponsable = Responsable.Email;
                                int IdAccion = (int)accionPlan.IdAccion;
                                var DataAccion = context.Acciones.Where(o => o.IdAccion == IdAccion).FirstOrDefault();
                                string DescripcionAccion = DataAccion.Descripcion;
                                int IdCategoria = (int)DataAccion.IdCategoria;
                                string DescripcionCategoria = context.Categoria.Where(o => o.IdCategoria == IdCategoria).FirstOrDefault().Nombre;

                                ML.AccionesPlan accionesPlan = new ML.AccionesPlan();
                                accionesPlan.IdAccionesPlan = accionPlan.IdAccionesPlan;
                                accionesPlan.AvanceGeneral = GetPorcentajeAvancePlan(IdPlan);
                                accionesPlan.PlanDeAccion.IdPlanDeAccion = IdPlanDeAccion;
                                accionesPlan.PlanDeAccion.Nombre = NombrePlanDeAccion;
                                accionesPlan.sFechaInicio = FechaInicio;
                                accionesPlan.sFechaFin = FechaFin;
                                accionesPlan.Objetivo = Objetivo;
                                accionesPlan.Meta = Meta;
                                accionesPlan.Comentarios = Comentarios;
                                accionesPlan.ListResponsable.Add(new ML.Responsable() { IdResponsable = Responsable.IdResponsable, Nombre = NombreResponsable, Email = EmailResponsable });
                                accionesPlan.AccionesDeMejora.IdAccionDeMejora = IdAccion;
                                accionesPlan.AccionesDeMejora.Descripcion = DescripcionAccion;
                                accionesPlan.AccionesDeMejora.Categoria.IdCategoria = IdCategoria;
                                accionesPlan.AccionesDeMejora.Categoria.Descripcion = DescripcionCategoria;
                                accionesPlan.PorcentajeAvance = Convert.ToDecimal(accionPlan.PorcentajeAvance);
                                accionesPlan.Cumplimiento = Convert.ToDecimal(accionPlan.Cumplimiento);
                                accionesPlan.DescripcionPeriodicidad = accionPlan.Periodicidad == 0 ? "Periodicidad no configurada" : context.Periodicidad.Where(o => o.IdPeriodicidad == accionPlan.Periodicidad).FirstOrDefault().Descripcion;
                                var Seguimiento = context.Seguimiento.Where(o => o.IdResponsableAccionesPlan == ResponsableAccionesPlan.IdResponsablesAccionesPlan).ToList();
                                foreach (var seguimento in Seguimiento)
                                {
                                    var SeguimientoEvidencia = context.SeguimientoEvidencia.Where(o => o.IdSeguimiento == seguimento.IdSeguimiento).ToList();
                                    foreach (var seguimientoEvidencia in SeguimientoEvidencia)
                                    {
                                        var evidencia = context.Evidencia.Where(o => o.IdEvidencia == seguimientoEvidencia.IdEvidencia && o.IdEstatus == 1).FirstOrDefault();
                                        if (evidencia != null)
                                        {
                                            accionesPlan.Atachments.Add(string.Concat(evidencia.Ruta, ""));
                                        }
                                    }
                                }
                                ML.PromediosCategorias promediosCategorias = new ML.PromediosCategorias()
                                {
                                    Area = PlanDeAccion.Area,
                                    BasesDeDatos = new ML.BasesDeDatos() { IdBaseDeDatos = PlanDeAccion.IdBaseDeDatos },
                                    Categoria = new ML.Categoria() { IdCategoria = (int)DataAccion.IdCategoria },
                                    Encuesta = new ML.Encuesta() { IdEncuesta = (int)PlanDeAccion.IdEncuesta }
                                };
                                var promedio = ObtenerPromedioCategoria(promediosCategorias);
                                accionesPlan.AccionesDeMejora.Categoria.Promedio = promedio;
                                accionesPlan.AccionesDeMejora.Categoria.Icono = BL.Email.ObtenerIconoEmail(promedio);
                                result.Objects.Add(accionesPlan);
                            }
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        // Obtener todas las acciones del plan ya que es un admin
                        /*
                         * Homologar lo que se tiene en la validacion de cuando entra un usuario responsable, solo que en el arreglo de responsables 
                         * dejarlo abierto a que pueden ser varios
                         * Asi mismo los atachments mostrarlos segun del responsable que los haya subido
                         * y en el fron iterar la pantilla en la parte de responsables de accion
                         */
                        var AccionesPlan = context.AccionesPlan.Where(o => o.IdPlanDeAccion == IdPlan);
                        foreach (var accionPlan in AccionesPlan)
                        {
                            int IdPlanDeAccion = (int)accionPlan.IdPlanDeAccion;
                            var PlanDeAccion = context.PlanDeAccion.Where(o => o.IdPlanDeAccion == IdPlanDeAccion).FirstOrDefault();
                            string NombrePlanDeAccion = PlanDeAccion.Nombre;
                            string FechaInicio = accionPlan.FechaInicio.ToString().Substring(0, 10);
                            string FechaFin = accionPlan.FechaFin.ToString().Substring(0, 10);
                            string Objetivo = accionPlan.Objetivo;
                            string Comentarios = accionPlan.Comentarios;
                            string Meta = accionPlan.Meta;
                            int IdAccion = (int)accionPlan.IdAccion;
                            var DataAccion = context.Acciones.Where(o => o.IdAccion == IdAccion).FirstOrDefault();
                            string DescripcionAccion = DataAccion.Descripcion;
                            int IdCategoria = (int)DataAccion.IdCategoria;
                            string DescripcionCategoria = context.Categoria.Where(o => o.IdCategoria == IdCategoria).FirstOrDefault().Nombre;

                            // Listado de responsables
                            ML.AccionesPlan accionesPlan = new ML.AccionesPlan();
                            accionesPlan.IdAccionesPlan = accionPlan.IdAccionesPlan;
                            accionesPlan.AvanceGeneral = GetPorcentajeAvancePlan(IdPlan);
                            var ResponsableAccionesPlan = accionPlan.ResponsablesAccionesPlan.ToList();
                            int index = 0;
                            foreach (var responsableAccionPlan in ResponsableAccionesPlan)
                            {
                                int IdRes = (int)responsableAccionPlan.IdResponsable;
                                var Responsable = context.Responsable.Where(o => o.IdResponsable == IdRes).FirstOrDefault();
                                string NombreResponsable = string.Concat(Responsable.Nombre, " ", Responsable.ApellidoPaterno, " ", Responsable.ApellidoMaterno);
                                string EmailResponsable = Responsable.Email;
                                accionesPlan.ListResponsable.Add(new ML.Responsable() { IdResponsable = IdRes, Nombre = NombreResponsable, Email = EmailResponsable });
                                var Seguimiento = context.Seguimiento.Where(o => o.IdResponsableAccionesPlan == responsableAccionPlan.IdResponsablesAccionesPlan).ToList();
                                foreach (var seguimento in Seguimiento)
                                {
                                    var SeguimientoEvidencia = context.SeguimientoEvidencia.Where(o => o.IdSeguimiento == seguimento.IdSeguimiento).ToList();
                                    foreach (var seguimientoEvidencia in SeguimientoEvidencia)
                                    {
                                        var evidencia = context.Evidencia.Where(o => o.IdEvidencia == seguimientoEvidencia.IdEvidencia && o.IdEstatus == 1).FirstOrDefault();
                                        if (evidencia != null)
                                            accionesPlan.ListResponsable[index].Atachments.Add(evidencia.Ruta);
                                    }
                                }
                                index++;
                            }
                            //Fin Listado de responsables
                            accionesPlan.PlanDeAccion.IdPlanDeAccion = IdPlanDeAccion;
                            accionesPlan.PlanDeAccion.Nombre = NombrePlanDeAccion;
                            accionesPlan.sFechaInicio = FechaInicio;
                            accionesPlan.sFechaFin = FechaFin;
                            accionesPlan.Objetivo = Objetivo;
                            accionesPlan.Meta = Meta;
                            accionesPlan.Comentarios = Comentarios;
                            accionesPlan.AccionesDeMejora.IdAccionDeMejora = IdAccion;
                            accionesPlan.AccionesDeMejora.Descripcion = DescripcionAccion;
                            accionesPlan.AccionesDeMejora.Categoria.IdCategoria = IdCategoria;
                            accionesPlan.AccionesDeMejora.Categoria.Descripcion = DescripcionCategoria;
                            accionesPlan.PorcentajeAvance = Convert.ToDecimal(accionPlan.PorcentajeAvance);
                            accionesPlan.Cumplimiento = Convert.ToDecimal(accionPlan.Cumplimiento);
                            accionesPlan.DescripcionPeriodicidad = accionPlan.Periodicidad == 0 ? "Periodicidad no configurada" : context.Periodicidad.Where(o => o.IdPeriodicidad == accionPlan.Periodicidad).FirstOrDefault().Descripcion;

                            ML.PromediosCategorias promediosCategorias = new ML.PromediosCategorias()
                            {
                                Area = PlanDeAccion.Area,
                                BasesDeDatos = new ML.BasesDeDatos() { IdBaseDeDatos = PlanDeAccion.IdBaseDeDatos },
                                Categoria = new ML.Categoria() { IdCategoria = (int)DataAccion.IdCategoria },
                                Encuesta = new ML.Encuesta() { IdEncuesta = (int)PlanDeAccion.IdEncuesta }
                            };
                            var promedio = ObtenerPromedioCategoria(promediosCategorias);
                            accionesPlan.AccionesDeMejora.Categoria.Promedio = promedio;
                            accionesPlan.AccionesDeMejora.Categoria.Icono = BL.Email.ObtenerIconoEmail(promedio);
                            result.Objects.Add(accionesPlan);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
            }
            return result;
        }
        /// <summary>
        /// Guarda la ruta del archivo en la tabla Evidencias
        /// </summary>
        /// <param name="ruta"></param>
        /// <param name="IdAccion"></param>
        /// <param name="IdPlan"></param>
        /// <param name="IdResponsable"></param>
        /// <param name="comentario"></param>
        /// <returns></returns>
        public static ML.Result GuardarRutaArchivo(string ruta, string IdPlan, string IdAccion, int IdResponsable, string comentario)
        {
            ML.Result result = new ML.Result();
            try
            {
                comentario = "###Responsable: " + comentario + "###";
                int _idPlan = Convert.ToInt32(IdPlan.Split('_')[1]);//"IdPlan_3"
                int _idAccion = Convert.ToInt32(IdAccion.Split('_')[1]);
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    DL.Evidencia evidencia = new DL.Evidencia();
                    evidencia.Ruta = ruta;
                    evidencia.IdEstatus = 1;
                    evidencia.FechaHoraCreacion = DateTime.Now;
                    evidencia.UsuarioCreacion = Convert.ToString(IdResponsable);
                    evidencia.ProgramaCreacion = "Carga de evidencias";
                    context.Evidencia.Add(evidencia);
                    context.SaveChanges();//Agregar evidencia

                    var AccionesPlan = context.AccionesPlan.Where(o => o.IdPlanDeAccion == _idPlan && o.IdAccion == _idAccion).FirstOrDefault();
                    var ResponsableAccionesPlan = context.ResponsablesAccionesPlan.Where(o => o.IdAccionesPlan == AccionesPlan.IdAccionesPlan && o.IdResponsable == IdResponsable).FirstOrDefault();
                    DL.Seguimiento seguimiento = new DL.Seguimiento();
                    seguimiento.IdResponsableAccionesPlan = ResponsableAccionesPlan.IdResponsablesAccionesPlan;
                    seguimiento.FechaHoraCreacion = DateTime.Now;
                    seguimiento.UsuarioCreacion = Convert.ToString(IdResponsable);
                    seguimiento.ProgramaCreacion = "Carga de evidencias";
                    context.Seguimiento.Add(seguimiento);
                    context.SaveChanges();//Agregar seguimiento

                    DL.SeguimientoEvidencia seguimientoEvidencia = new DL.SeguimientoEvidencia();
                    seguimientoEvidencia.IdSeguimiento = seguimiento.IdSeguimiento;
                    seguimientoEvidencia.IdEvidencia = evidencia.IdEvidencia;
                    seguimientoEvidencia.Comentario += comentario;
                    seguimientoEvidencia.FechaHoraCreacion = DateTime.Now;
                    seguimientoEvidencia.UsuarioCreacion = Convert.ToString(IdResponsable);
                    seguimientoEvidencia.ProgramaCreacion = "Carga de evidencias";
                    context.SeguimientoEvidencia.Add(seguimientoEvidencia);
                    context.SaveChanges();//Agregar SeguimientoEvidencia
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
            }
            return result;
        }
        /// <summary>
        /// Obtiene los atachments en una ruta especificada
        /// </summary>
        /// <param name="ruta"></param>
        /// <returns></returns>
        public static List<string> ObtenerAtachments(string ruta)
        {
            List<string> result = new List<string>();
            try
            {
                var folders = Directory.GetDirectories(ruta).ToList();
                foreach (var folder in folders)
                {
                    var files = Directory.GetFiles(folder).ToList();
                    foreach (var fileItem in files)
                    {
                        result.Add(fileItem);
                    }
                }
                if (folders.Count == 0)
                {
                    var files = Directory.GetFiles(ruta).ToList();
                    foreach (var fileItem in files)
                    {
                        result.Add(fileItem);
                    }
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                result = new List<string>();
            }
            return result;
        }
        /// <summary>
        /// Elimina un archivo del conjunto de evidencias
        /// </summary>
        /// <param name="ruta"></param>
        /// <returns></returns>
        public static ML.Result EliminarEvidencia(string ruta)
        {
            ML.Result result = new ML.Result();
            try
            {
                //\\\\10.5.2.101\\RHDiagnostics\\PlanesDeAccion\\IdPlan_3\\IdAccion_67\\IdResponsable_1\\Configurador Planes de Acción por Categoría V3 (3).pptx
                // \\\\10.5.2.101\\RHDiagnostics\\PlanesDeAccion\\IdPlan_3\\IdAccion_67\\IdResponsable_1\\Reporte de Encuesta DNC.xlsx
                string serverRoute = ruta.Replace((ConfigurationManager.AppSettings["urlTemplateLocation"].ToString() + "/PlanesDeAccion//"), (@"\\\\10.5.2.101\\" + ConfigurationManager.AppSettings["templateLocation"].ToString() + @"\\PlanesDeAccion\\"));
                serverRoute = serverRoute.Replace("/", @"\");
                File.Delete(serverRoute);
                result.Correct = true;

                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var evidencia = context.Evidencia.Where(O => O.Ruta == serverRoute).FirstOrDefault();
                    if (evidencia != null)
                    {
                        evidencia.IdEstatus = 2;
                        context.SaveChanges();
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo encontrar la ruta de la evidencia";
                    }
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
            }
            return result;
        }
        /// <summary>
        /// Obtiene la descripcion de una periodicidad
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static ML.Periodicidad ObtenerPeriodicidadById(int Id)
        {
            ML.Periodicidad periodicidad = new ML.Periodicidad();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var data = context.Periodicidad.Where(o => o.IdPeriodicidad == Id).FirstOrDefault();
                    if (data != null)
                    {
                        periodicidad.IdPeriodicidad = data.IdPeriodicidad;
                        periodicidad.Descripcion = data.Descripcion;
                    }
                    else
                    {
                        periodicidad.Descripcion = "Periodicidad no establecida";
                    }
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
            }
            return periodicidad;
        }
        /// <summary>
        /// Actualiza el avance de una accion en un plan especifico
        /// </summary>
        /// <param name="accionesPlan"></param>
        /// <returns></returns>
        public static ML.Result GuardarAvances(ML.AccionesPlan accionesPlan)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var accionP = context.AccionesPlan.Where(o => o.IdAccionesPlan == accionesPlan.IdAccionesPlan && o.IdPlanDeAccion == accionesPlan.PlanDeAccion.IdPlanDeAccion && o.IdAccion == accionesPlan.AccionesDeMejora.IdAccionDeMejora).FirstOrDefault();
                    if (accionP != null)
                    {
                        accionP.PorcentajeAvance = accionesPlan.PorcentajeAvance;
                        accionP.Cumplimiento = accionesPlan.Cumplimiento;
                        context.SaveChanges();
                        result.Correct = true;
                    }
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
            }
            return result;
        }
        /// <summary>
        /// Obtiene los comentarios existentes sobre una evidencia
        /// </summary>
        /// <param name="IdAccionesPlan"></param>
        /// <param name="IdResponsable"></param>
        /// <returns></returns>
        public static ML.Result GetComentarios(int IdAccionesPlan, int IdResponsable)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                // Segun el IdAccionesPlan Buscar en responsableaccuonesPlan con ese id
                // Segun el idResponsableaccionesPlan biscar en seguimiento con el idresponsavlesAccionesplan
                // con el id de seguimiento buscar en seguimiento evidencia
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var responsableAccionesPlan = context.ResponsablesAccionesPlan.Where(o => o.IdAccionesPlan == IdAccionesPlan && o.IdResponsable == IdResponsable).FirstOrDefault();
                    var seguimiento = context.Seguimiento.Where(o => o.IdResponsableAccionesPlan == responsableAccionesPlan.IdResponsablesAccionesPlan).FirstOrDefault();
                    var seguimientoEvidencia = context.SeguimientoEvidencia.Where(o => o.IdSeguimiento == seguimiento.IdSeguimiento).FirstOrDefault();
                    if (!string.IsNullOrEmpty(seguimientoEvidencia.Comentario))
                    {
                        var comentarios = seguimientoEvidencia.Comentario.Split(new string[] { "###" }, StringSplitOptions.None);
                        comentarios = comentarios.Where(o => o != null && o != "").ToArray();
                        result.Objects.Add(comentarios);
                    }
                    result.Correct = true;
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
            }
            return result;
        }
        /// <summary>
        /// Agrega un comentario nuevo sobre la evidencia especificada
        /// </summary>
        /// <param name="IdAccionesPlan"></param>
        /// <param name="IdResponsable"></param>
        /// <param name="comentario"></param>
        /// <param name="sessionResponsableId"></param>
        /// <returns></returns>
        public static ML.Result AgregarComentarios(int IdAccionesPlan, int IdResponsable, string comentario, int sessionResponsableId)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    if (sessionResponsableId == 0)
                    {
                        // Si no es un responsable el que agrega el comentario entonces va asi
                        comentario = "###" + comentario + "###";
                    }
                    else
                    {
                        // Si es un responsable el que agrega el comentario entonces debe ser asi
                        comentario = "###Responsable: " + comentario + "###";
                    }
                    
                    var responsableAccionesPlan = context.ResponsablesAccionesPlan.Where(o => o.IdAccionesPlan == IdAccionesPlan && o.IdResponsable == IdResponsable).FirstOrDefault();
                    var seguimiento = context.Seguimiento.Where(o => o.IdResponsableAccionesPlan == responsableAccionesPlan.IdResponsablesAccionesPlan).FirstOrDefault();
                    var seguimientoEvidencia = context.SeguimientoEvidencia.Where(o => o.IdSeguimiento == seguimiento.IdSeguimiento).FirstOrDefault();

                    seguimientoEvidencia.Comentario += comentario;
                    context.SaveChanges();

                    result.Objects = GetComentarios(IdAccionesPlan, IdResponsable).Objects;
                    result.Correct = true;
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
            }
            return result;
        }
        #endregion Seguimiento


        #region Permisos
        /// <summary>
        /// Agrega los permisos a un listado de usuarios en un area especificada
        /// </summary>
        /// <param name="Area"></param>
        /// <param name="Admins"></param>
        /// <param name="currentAdmin"></param>
        /// <param name="IdBD"></param>
        /// <param name="Unidad"></param>
        /// <param name="Direccion"></param>
        /// <returns></returns>
        public static ML.Result AddPermisosPlanes(string Area, List<int> Admins, string currentAdmin, int IdBD, string Direccion, string Unidad)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    foreach (var item in Admins)
                    {
                        DL.PDAPermisos pDAPermisos = new DL.PDAPermisos()
                        {
                            Area = Area,
                            IdAdministrador = item,
                            IdBaseDeDatos = IdBD,
                            Direccion = Direccion,
                            Unidad = Unidad,
                            IdEstatus = 1,
                            FechaHoraCreacion = DateTime.Now,
                            UsuarioCreacion = currentAdmin,
                            ProgramaCreacion = "Alta de permisos PDA"
                        };
                        context.PDAPermisos.Add(pDAPermisos);
                        context.SaveChanges();
                        ConfigurarPerfil(item, currentAdmin);
                    }
                    result.Correct = true;
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
            }
            return result;
        }
        /// <summary>
        /// Deshabilita los permisos del administrador al area especificada
        /// </summary>
        /// <param name="IdAdmin"></param>
        /// <param name="area"></param>
        /// <returns>objeto de la clase result</returns>
        public static ML.Result DesactivarPermiso(int IdAdmin, string area)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var PDAPErmiso = context.PDAPermisos.Where(o => o.IdAdministrador == IdAdmin && o.Area == area).ToList();
                    foreach (var item in PDAPErmiso)
                    {
                        if (item != null)
                            item.IdEstatus = 2;
                    }
                    context.SaveChanges();
                    result.Correct = true;
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
            }
            return result;
        }
        /// <summary>
        /// Agrega el perfil modulo y asigna las acciones a un administrador en el modulo de Planes de Accion
        /// </summary>
        /// <param name="IdAdmin"></param>
        /// <param name="currentAdmin"></param>
        /// <returns></returns>
        public static ML.Result ConfigurarPerfil(int IdAdmin, string currentAdmin)
        {
            List<string> acciones = new List<string>();
            acciones.Add("Seguimiento");
            acciones.Add("ListarPlanes");
            acciones.Add("CrearPlanes");
            acciones.Add("AdministrarAcciones");
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var tienePerfilPDA = context.PerfilModulo.Where(o => o.IdAdministrador == IdAdmin && o.IdModulo == 7).FirstOrDefault();
                    if (tienePerfilPDA != null) // Solo se agrega la accion
                    {
                        foreach (var itemAccion in acciones)
                        {
                            DL.PerfilModuloAccion perfilModuloAccion = new DL.PerfilModuloAccion()
                            {
                                Accion = itemAccion,
                                IdPerfilModulo = tienePerfilPDA.IdPerfilModulo,
                                FechaHoraCreacion = DateTime.Now,
                                UsuarioCreacion = currentAdmin,
                                ProgramaCreacion = "Alta de permisos PDA"
                            };
                            context.PerfilModuloAccion.Add(perfilModuloAccion);
                            context.SaveChanges();
                        }
                        
                        result.Correct = true;
                    }
                    else // Se agrega el perfil modulo y despues la accion
                    {
                        var perfilAdmin = context.PerfilModulo.Where(o => o.IdAdministrador == IdAdmin).FirstOrDefault();
                        DL.PerfilModulo perfilModulo = new DL.PerfilModulo()
                        {
                            IdAdministrador = IdAdmin,
                            IdPerfil = perfilAdmin.IdPerfil,
                            IdModulo = 7
                        };
                        context.PerfilModulo.Add(perfilModulo);
                        context.SaveChanges();
                        foreach (var itemAccion in acciones)
                        {
                            DL.PerfilModuloAccion perfilModuloAccion = new DL.PerfilModuloAccion()
                            {
                                Accion = itemAccion,
                                IdPerfilModulo = perfilModulo.IdPerfilModulo,
                                FechaHoraCreacion = DateTime.Now,
                                UsuarioCreacion = currentAdmin,
                                ProgramaCreacion = "Alta de permisos PDA"
                            };
                            context.PerfilModuloAccion.Add(perfilModuloAccion);
                            context.SaveChanges();
                        }

                        
                        result.Correct = true;
                    }
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
            }
            return result;
        }
        /// <summary>
        /// Obtiene listado de administradores que no tienen aun asignado el permiso al area seleccionada
        /// </summary>
        /// <param name="Area"></param>
        /// <returns></returns>
        public static ML.Result ObtenerAdmins(string Area)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var data = context.Administrador.Where(o => o.IdEstatus == 1);
                    foreach (var item in data)
                    {
                        var empleado = context.Empleado.Where(o => o.IdEmpleado == item.IdEmpleado).FirstOrDefault();
                        if (empleado == null)
                            empleado = new DL.Empleado() { Nombre = "", ApellidoPaterno = "", ApellidoMaterno = "" };
                        ML.Administrador administrador = new ML.Administrador();
                        administrador.IdRH = item.IdRH == null ? 0 : (int)item.IdRH;
                        administrador.IdAdministrador = item.IdAdministrador;
                        administrador.Nombre = String.Concat(empleado.Nombre, " ", empleado.ApellidoPaterno, " ", empleado.ApellidoMaterno);
                        administrador.UserName = item.UserName;
                        // Inicio: Verificar si el admin pertecene a alguna area agencia para ya ponerla marcada
                        administrador.Selected = BL.PlanesDeAccion.TieneAccesoAreaByLayout(administrador.IdAdministrador, Area);
                        //Fin: Verificar si el admin pertecene a alguna area agencia para ya ponerla marcada
                        // Solo se muestra si el usuario no tiene asignado el permiso a esta area ya previamente
                        if (TienePermisoPDA(item.IdAdministrador, Area))
                            administrador.Selected = ML.Administrador.Data.verdadero;
                        else
                            administrador.Selected = ML.Administrador.Data.falso;
                        if (item.AdminSA == 1)
                            administrador.Selected = ML.Administrador.Data.verdadero;
                        
                        if (item.AdminSA == 0)
                            administrador.AdminSA = null;
                        if (item.AdminSA == null)
                            administrador.AdminSA = null;
                        if (item.AdminSA != 0 && item.AdminSA != null)
                            administrador.AdminSA = Convert.ToInt32(item.AdminSA);
                        
                        result.Objects.Add(administrador);
                    }
                    result.Correct = true;
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
            }
            return result;
        }
        /// <summary>
        /// Valida si un administrador tiene permisos a un area determinada
        /// </summary>
        /// <param name="IdAdmin"></param>
        /// <param name="Area"></param>
        /// <returns></returns>
        public static bool TienePermisoPDA(int IdAdmin, string Area)
        {
            bool result = false;
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var existe = context.PDAPermisos.Where(o => o.IdAdministrador == IdAdmin && o.Area == Area && o.IdEstatus == 1).FirstOrDefault();
                    if (existe != null)
                        result = true;
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
            }
            return result;
        }
        /// <summary>
        /// Obtiene los permisos asignados de un administrador
        /// </summary>
        /// <param name="IdAdmin"></param>
        /// <returns></returns>
        public static List<ML.PDAPermisos> ObtenerPermisosPDA(int IdAdmin)
        {
            List<ML.PDAPermisos> ListPDAPermisos = new List<ML.PDAPermisos>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var permisos = context.PDAPermisos.Where(o => o.IdAdministrador == IdAdmin && o.IdEstatus == 1);
                    foreach (var itemPermiso in permisos)
                    {
                        ML.PDAPermisos pDAPermisos = new ML.PDAPermisos();
                        pDAPermisos.IdPDAPermisos = itemPermiso.IdPDAPermisos;
                        pDAPermisos.IdAdministrador = (int)itemPermiso.IdAdministrador;
                        pDAPermisos.Area = itemPermiso.Area;

                        ListPDAPermisos.Add(pDAPermisos);
                    }
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
            }
            return ListPDAPermisos;
        }
        /// <summary>
        /// Verifica por el id de RH si un admins tiene acceso al area que se seleccionó
        /// </summary>
        /// <param name="IdAdmin"></param>
        /// <param name="Area"></param>
        /// <returns></returns>
        public static ML.Administrador.Data TieneAccesoAreaByLayout(int IdAdmin, string Area)
        {
            var result = new ML.Administrador.Data();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var admin = context.Administrador.Where(o => o.IdAdministrador == IdAdmin).FirstOrDefault();
                    if (admin != null)
                    {
                        if (admin.IdRH != null)
                        {
                            var data = context.Empleado.Select(o => new { o.IdJefe, o.IdResponsableEstructura, o.IdResponsableRH, o.AreaAgencia }).Where(o => o.IdJefe == admin.IdRH || o.IdResponsableEstructura == admin.IdRH || o.IdResponsableRH == admin.IdRH).ToList();
                            List<string> Areas = data.Select(o => o.AreaAgencia).ToList();
                            if (Areas.Contains(Area))
                                result = ML.Administrador.Data.verdadero;
                            else
                                result = ML.Administrador.Data.falso;
                        }
                        else
                        {
                            result = ML.Administrador.Data.falso;
                        }
                    }
                    else
                    {
                        result = ML.Administrador.Data.falso;
                    }
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                result = ML.Administrador.Data.falso;
            }
            return result;
        }
        public static ML.PlanDeAccion DetallePlanDeAccion(int IdPlan)
        {
            ML.PlanDeAccion planDeAccion = new ML.PlanDeAccion();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var PlanDeAccion = context.PlanDeAccion.Where(o => o.IdPlanDeAccion == IdPlan).FirstOrDefault();
                    if (PlanDeAccion != null)
                    {
                        // Model
                        planDeAccion.IdPlanDeAccion = PlanDeAccion.IdPlanDeAccion;
                        planDeAccion.Nombre = PlanDeAccion.Nombre;
                        // Model
                        var AccionesPlan = context.AccionesPlan.Where(o => o.IdPlanDeAccion == PlanDeAccion.IdPlanDeAccion);
                        foreach (var accionPlan in AccionesPlan)
                        {
                            var Accion = context.Acciones.Where(o => o.IdAccion == accionPlan.IdAccion).FirstOrDefault();
                            var ResponsablesAccion = context.ResponsablesAccionesPlan.Where(o => o.IdAccionesPlan == accionPlan.IdAccionesPlan).ToList();
                            foreach (var responable in ResponsablesAccion)
                            {
                                var Responsable = context.Responsable.Where(o => o.IdResponsable == responable.IdResponsable).FirstOrDefault();
                                var seguimiento = context.Seguimiento.Where(o => o.IdResponsableAccionesPlan == responable.IdResponsablesAccionesPlan).ToList();
                                if (seguimiento.Count > 0)
                                {
                                    foreach (var seg in seguimiento)
                                    {
                                        var seguimientoEvidencia = context.SeguimientoEvidencia.Where(o => o.IdSeguimiento == seg.IdSeguimiento).FirstOrDefault();
                                        var evidencia = context.Evidencia.Where(o => o.IdEvidencia == seguimientoEvidencia.IdEvidencia).FirstOrDefault();
                                        // Model
                                        planDeAccion.ListAcciones.Add(
                                            new ML.AccionesPlan()
                                            {
                                                IdAccionesPlan = accionPlan.IdAccionesPlan,
                                                sFechaInicio = Convert.ToString(accionPlan.FechaInicio),
                                                sFechaFin = Convert.ToString(accionPlan.FechaFin),
                                                Comentarios = accionPlan.Comentarios,
                                                Meta = accionPlan.Meta,
                                                Objetivo = accionPlan.Objetivo,
                                                AccionesDeMejora = new ML.AccionDeMejora()
                                                {
                                                    IdAccionDeMejora = Accion.IdAccion,
                                                    Descripcion = Accion.Descripcion,
                                                },
                                                ListResponsable = new List<ML.Responsable>() { new ML.Responsable()
                                                {
                                                    IdResponsable = Responsable.IdResponsable,
                                                    Nombre = Responsable.Nombre,
                                                    ApellidoPaterno = Responsable.ApellidoPaterno,
                                                    ApellidoMaterno = Responsable.ApellidoMaterno,
                                                    Email = Responsable.Email,
                                                    Atachments = new List<string>(){ evidencia.Ruta }
                                                }
                                                }
                                            });
                                        // Model
                                    }
                                }
                                else
                                {
                                    // Model
                                    planDeAccion.ListAcciones.Add(
                                        new ML.AccionesPlan()
                                        {
                                            IdAccionesPlan = accionPlan.IdAccionesPlan,
                                            sFechaInicio = Convert.ToString(accionPlan.FechaInicio),
                                            sFechaFin = Convert.ToString(accionPlan.FechaFin),
                                            Comentarios = accionPlan.Comentarios,
                                            Meta = accionPlan.Meta,
                                            Objetivo = accionPlan.Objetivo,
                                            AccionesDeMejora = new ML.AccionDeMejora()
                                            {
                                                IdAccionDeMejora = Accion.IdAccion,
                                                Descripcion = Accion.Descripcion,
                                            },
                                            ListResponsable = new List<ML.Responsable>() { new ML.Responsable()
                                            {
                                                IdResponsable = Responsable.IdResponsable,
                                                Nombre = Responsable.Nombre,
                                                ApellidoPaterno = Responsable.ApellidoPaterno,
                                                ApellidoMaterno = Responsable.ApellidoMaterno,
                                                Email = Responsable.Email,
                                            }
                                            }
                                        });
                                    // Model
                                }
                            }
                        }
                    }
                    Console.Write(planDeAccion);
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
            }
            return planDeAccion;
        }
        #endregion Permisos

        #region Actualizar Layout Carga masiva de acciones
        /// <summary>
        /// Retorna un listado de strings para llenar el layout
        /// </summary>
        /// <returns></returns>
        public static List<string> GetCategorias()
        {
            List<string> list = new List<string>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var categorias = context.PromediosCategorias.Select(o => o.IdCategoria).Distinct().ToList();
                    foreach (var cat in categorias)
                    {
                        var nameCat = context.Categoria.Where(o => o.IdCategoria == cat.Value).FirstOrDefault();
                        if (nameCat != null)
                            list.Add(nameCat.Nombre);
                    }
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
            }
            return list;
        }
        /// <summary>
        /// Retorna un listado de strings para llenar el layout
        /// </summary>
        /// <returns></returns>
        public static List<string> GetRangosForExcel()
        {
            List<string> list = new List<string>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var rangos = context.Rango.ToList();
                    foreach (var rango in rangos)
                    {
                        if (rango != null)
                            list.Add(rango.Descripcion);
                    }
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
            }
            return list;
        }
        /// <summary>
        /// Retorna el id de categoria por su nombre
        /// </summary>
        /// <param name="NombreC"></param>
        /// <returns></returns>
        public static int GetIdCategoriaByName(string NombreC)
        {
            int idCategoria = 0;
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var categoria = context.Categoria.Where(o => o.Nombre == NombreC).FirstOrDefault();
                    if (categoria != null)
                        idCategoria = categoria.IdCategoria;
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
            }
            return idCategoria;
        }
        /// <summary>
        /// Retorna el id de rango por su nombre
        /// </summary>
        /// <param name="NombreR"></param>
        /// <returns></returns>
        public static int GetIdRangoByName(string NombreR)
        {
            int idRango = 0;
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var rango = context.Rango.Where(o => o.Descripcion == NombreR).FirstOrDefault();
                    if (rango != null)
                        idRango = rango.IdRango;
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
            }
            return idRango;
        }
        /// <summary>
        /// Agrega un proceso automatizado de envio de notificaciones
        /// </summary>
        /// <param name="IdPlanDeAccion"></param>
        /// <param name="UID"></param>
        /// <param name="frecuencia"></param>
        /// <param name="currentUser"></param>
        /// <param name="plantilla"></param>
        /// <param name="priority"></param>
        /// <param name="subject"></param>
        /// <param name="IdBD"></param>
        /// <param name="IdEncuesta"></param>
        /// <returns></returns>
        public static bool AgregarJobNotificacionesPDA(int IdPlanDeAccion, string UID, string currentUser, string frecuencia, string plantilla, int priority, string subject, int IdEncuesta, int IdBD)
        {
            bool result = false;
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    // Validar cuando metr cad param
                    DL.JobsNotificacionesPDA jobsNotificacionesPDA = new DL.JobsNotificacionesPDA()
                    {
                        IdEncuesta = IdEncuesta,
                        IdBaseDeDatos = IdBD,
                        IdPlanDeAccion = IdPlanDeAccion,
                        JobId = UID,
                        IdEstatus = 1,
                        CronExpression = frecuencia,
                        Plantilla = plantilla,
                        Priority = priority,
                        Subject = subject,
                        FechaHoraCreacion = DateTime.Now,
                        UsuarioCreacion = currentUser,
                        ProgramaCreacion = IdPlanDeAccion == 0 ? "AgregarJobNotificacionesEncuesta" : "AgregarJobNotificacionesPDA"
                    };
                    context.JobsNotificacionesPDA.Add(jobsNotificacionesPDA);
                    context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// Obtiene el o los jobs relacionados al envio de notificaciones programadas de un plan de accion
        /// </summary>
        /// <param name="IdPlanDeAccion"></param>
        /// <returns></returns>
        public static List<ML.JobsNotificacionesPDA> ObtenerNotificacionesProgramadas(int IdPlanDeAccion)
        {
            List< ML.JobsNotificacionesPDA> result = new List<ML.JobsNotificacionesPDA>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var data = context.JobsNotificacionesPDA.Where(o => o.IdPlanDeAccion == IdPlanDeAccion);
                    foreach (var job in data)
                    {
                        ML.JobsNotificacionesPDA jobsNotificacionesPDA = new ML.JobsNotificacionesPDA();
                        jobsNotificacionesPDA.IdJobsNotificacionesPDA = job.IdJobsNotificacionesPDA;
                        jobsNotificacionesPDA.JobId = job.JobId;
                        jobsNotificacionesPDA.IdEstatus = (int)job.IdEstatus;
                        jobsNotificacionesPDA.IdPlanDeAccion = (int)job.IdPlanDeAccion;
                        jobsNotificacionesPDA.CronExpression = job.CronExpression;
                        jobsNotificacionesPDA.Periodicidad = ExpressionDescriptor.GetDescription(job.CronExpression, new Options() { Locale = "es" });
                        if (jobsNotificacionesPDA.CronExpression.Equals("0 8 * * *"))
                            jobsNotificacionesPDA.Periodicidad = $"Todos los días {jobsNotificacionesPDA.Periodicidad}";

                        result.Add(jobsNotificacionesPDA);
                    }
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
            }
            return result;
        }
        /// <summary>
        /// Cambia el estatus de un job de notificaciones
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="IdPlan"></param>
        /// <param name="IdJobsNotificacionesPDA"></param>
        /// <param name="NewEstatus"></param>
        /// <returns></returns>
        public static ML.Result ChangeEstatusJob(int IdPlan, int IdJobsNotificacionesPDA, int NewEstatus, string currentUser)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var job = context.JobsNotificacionesPDA.Where(o => o.IdPlanDeAccion == IdPlan && o.IdJobsNotificacionesPDA == IdJobsNotificacionesPDA).FirstOrDefault();
                    if (job != null)
                    {
                        job.IdEstatus = NewEstatus;
                        job.FechaHoraModificacion = DateTime.Now;
                        job.UsuarioModificacion = currentUser;
                        job.ProgramaModificacion = "ChangeEstatusJob";
                        context.SaveChanges();
                        if (NewEstatus == 0)
                            KillJobsNotificacionesPDA();
                        if (NewEstatus == 1)
                        {
                            // Volver a generar el job segun lo que esta en la tabla de control
                            // var job = context.JobsNotificacionesPDA.Where(o => o.JobId == item.JobId).FirstOrDefault();
                            RecurringJob.AddOrUpdate(job.JobId, () => BL.PlanesDeAccion.EnvioEmailCustom((int)job.IdPlanDeAccion, (int)job.Priority, job.Subject, job.Plantilla), job.CronExpression, TimeZoneInfo.Local);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
            }
            return result;
        }
        /// <summary>
        /// Detiene los procesos automatizados de envio de notificaciones
        /// </summary>
        public static void KillJobsNotificacionesPDA()
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var JobsNotificacionesKill = context.JobsNotificacionesPDA.Where(o => o.IdEstatus == 0).ToList();
                    foreach (var jobKill in JobsNotificacionesKill)
                    {
                        RecurringJob.RemoveIfExists(jobKill.JobId);
                    }
                    var JobsNotificacionesRestart = context.JobsNotificacionesPDA.Where(o => o.IdEstatus == 1).ToList();
                    foreach (var jobRestart in JobsNotificacionesRestart)
                    {
                        // Volver a generar el job segun lo que esta en la tabla de control
                        // var job = context.JobsNotificacionesPDA.Where(o => o.JobId == item.JobId).FirstOrDefault();
                        RecurringJob.AddOrUpdate(jobRestart.JobId, () => BL.PlanesDeAccion.EnvioEmailCustom((int)jobRestart.IdPlanDeAccion, (int)jobRestart.Priority, jobRestart.Subject, jobRestart.Plantilla), jobRestart.CronExpression, TimeZoneInfo.Local);
                    }
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloPlanesDeAccion(aE, new StackTrace());
            }
        }
        /// <summary>
        /// Elimina la tarea de la cola y elimina permanentemente su registro
        /// </summary>
        /// <param name="IdJobsNotificacionesPDA"></param>
        /// <returns></returns>
        public static ML.Result EliminarJobNotificacion(int IdJobsNotificacionesPDA)
        {
            var result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var data = context.JobsNotificacionesPDA.Where(o => o.IdJobsNotificacionesPDA == IdJobsNotificacionesPDA).FirstOrDefault();
                    if (data != null)
                    {
                        RecurringJob.RemoveIfExists(data.JobId);
                        context.JobsNotificacionesPDA.Remove(data);
                        context.SaveChanges();
                        result.Correct = true;
                    }
                }
            }
            catch (Exception aE)
            {
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
                throw;
            }
            return result;
        }
        #endregion
    }
}
