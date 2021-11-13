using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
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
        /// <param name="IdPlan"></param>
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
        /// <param name="IdAccion"></param>
        /// <param name="IdPlan"></param>
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

                    var datapromediosSubCategorias = context.PromedioSubCategorias.Where(o => o.AnioAplicacion == promedioSubCategorias.AnioAplicacion && o.AreaAgencia == o.AreaAgencia && o.IdBaseDeDatos == promedioSubCategorias.IdBaseDeDatos && o.IdEncuesta == promedioSubCategorias.IdEncuesta).ToList();
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
        /// </summary>
        /// <param name="planDeAccion"></param>
        /// <param name="IdUsuarioAcyual"></param>
        /// <param name="UsuarioActual"></param>
        /// <returns></returns>
        public static ML.Result AddPlanDeAccion(ML.PlanDeAccion planDeAccion, string UsuarioActual, int IdUsuarioAcyual)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    //alta de plan de accion
                    var dataPlanDeAccion = new DL.PlanDeAccion() {
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
                    int idPlanDeAccion = (int)context.PlanDeAccion.Max(q => q.IdPlanDeAccion);
                    // alta de acciones por categoria
                    foreach (ML.AccionesPlan item in planDeAccion.ListAcciones)
                    {
                        var actualizaAccion = context.Acciones.Where(o => o.IdAccion == item.IdAccion).FirstOrDefault();
                        if (actualizaAccion != null)
                        {
                            actualizaAccion.Descripcion = item.PlanDeAccion.Nombre.Trim();
                                
                        }
                        DL.AccionesPlan acciones = new DL.AccionesPlan() {
                            IdPlanDeAccion = idPlanDeAccion,
                            IdAccion = item.IdAccion,
                            Periodicidad = item.Periodicidad,
                            FechaInicio = item.FechaInicio,
                            FechaFin = item.FechaFin,
                            Objetivo =item.Objetivo,
                            Meta= item.Meta,
                            Comentarios= item.Comentarios,
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
                            DL.Responsable responsablePA = new DL.Responsable()
                            {
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

                            DL.ResponsablesAccionesPlan respon = new DL.ResponsablesAccionesPlan() {
                                IdAccionesPlan= idAccionPlan,
                                IdResponsable = idresponsable,
                                FechaHoraCreacion = DateTime.Now,
                                UsuarioCreacion = UsuarioActual,
                                ProgramaCreacion = "Modulo Planes de Acción Alta - Responsable AccionPlan"
                            };
                            context.ResponsablesAccionesPlan.Add(respon);
                            context.SaveChanges();

                        }

                    }
                    
                    result.Correct = true;
                }
            }
            catch (Exception aE)
            {
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
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
                            rango.Descripcion = item.Descripcion;
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
                    if (accionDeMejora.Encuesta.IdEncuesta > 0)// Son las acciones para una encuesta especifica
                    {
                        var dataAcciones = context.Acciones.
                        Where(o =>
                        o.IdEncuesta == accionDeMejora.Encuesta.IdEncuesta &&
                        o.IdBaseDeDatos == accionDeMejora.BasesDeDatos.IdBaseDeDatos &&
                        o.AnioAplicacion == accionDeMejora.AnioAplicacion &&
                        o.IdEstatus == 1 && o.Tipo == 1).ToList().OrderBy(o => o.IdCategoria);
                        if (dataAcciones != null)
                        {
                            foreach (var item in dataAcciones)
                            {
                                ML.AccionDeMejora accion = new ML.AccionDeMejora();
                                accion.IdAccionDeMejora = item.IdAccion;
                                accion.Descripcion = item.Descripcion;
                                accion.Categoria.IdCategoria = item.Categoria.IdCategoria;
                                accion.Rango.IdRango = item.Rango.IdRango;
                                result.Objects.Add(accion);
                            }
                            result.Correct = true;
                        }
                    }
                    else // Son las acciones de ayuda de Noé, las cuales son genericas sin depender de una encuesta
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
                                result.Objects.Add(accion);
                            }
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
                    if (accionDeMejora.Encuesta.IdEncuesta == 0)
                    {
                        if (accionDeMejora.IdAccionDeMejora == 0)
                        {
                            DL.Acciones accion = new DL.Acciones()
                            {
                                Descripcion = accionDeMejora.Descripcion,
                                IdEstatus = accionDeMejora.Estatus.IdEstatus,
                                Tipo = 2,
                                FechaHoraCreacion = DateTime.Now,
                                UsuarioCreacion = UsuarioActual,
                                ProgramaCreacion = "Modulo Planes de Acción (Acciones de ayuda)"
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
                            Tipo = 1,
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
                            Accion.Tipo = 1;
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
        #endregion Generales Modulo



        #region Notificaciones
        /// <summary>
        /// Envía un email de notificación a los responsables las acciones al guardar un nuevo Plan
        /// trigger: Al guardar un plan de accion
        /// </summary>
        /// <returns></returns>
        public static void NotificacionInicialResponsables(int IdPlanDeAccion)
        {
            try
            {
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
                            var relacional = context.ResponsablesAccionesPlan.Where(o => o.IdResponsable == usuarioResponsable.IdResponsable).ToList();
                            foreach (var item in relacional)
                            {
                                var acciones = context.AccionesPlan.Where(o => o.IdAccionesPlan == item.IdAccionesPlan).ToList();
                                List<ML.AccionDeMejora> ListAcciones = new List<ML.AccionDeMejora>();
                                foreach (var elem in acciones)
                                {
                                    var accion = context.Acciones.Where(o => o.IdAccion == elem.IdAccion).FirstOrDefault();
                                    ML.AccionDeMejora accionDeMejora = new ML.AccionDeMejora();
                                    accionDeMejora.IdAccionDeMejora = accion.IdAccion;
                                    accionDeMejora.Descripcion = accion.Descripcion;
                                    accionDeMejora.Categoria.IdCategoria = (int)accion.IdCategoria;
                                    ListAcciones.Add(accionDeMejora);
                                }
                                BL.Email.EnvioNotificaciones(1, usuarioResponsable.Email, ListAcciones, usuarioResponsable, IdPlanDeAccion);
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
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var AccionesPlan = context.AccionesPlan.Where(o => o.FechaInicio == DateTime.Now.AddDays(7)).ToList();
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
                                    var PlanDeAccion = context.PlanDeAccion.Where(o => o.IdPlanDeAccion == accionPlan.IdPlanDeAccion).FirstOrDefault();
                                    var usuarioResponsable = context.Responsable.Where(o => o.IdResponsable == item.IdResponsable).FirstOrDefault();

                                    //BL.Email.EnvioNotificaciones(2, usuarioResponsable.Email);
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
        public static void NotificacionNoRegistraAvance()
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    //Falta el filtro de que no haya registrado avance
                    var AccionesPlan = context.AccionesPlan.Where(o => o.FechaInicio < DateTime.Now).ToList();
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
                                    var PlanDeAccion = context.PlanDeAccion.Where(o => o.IdPlanDeAccion == accionPlan.IdPlanDeAccion).FirstOrDefault();
                                    var usuarioResponsable = context.Responsable.Where(o => o.IdResponsable == item.IdResponsable).FirstOrDefault();
                                    //BL.Email.EnvioNotificaciones(3, usuarioResponsable.Email);
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
         • 2 El sistema enviará un correo de agradecimiento a los responsables una vez que se haya logrado el 100% de avance en la acción de mejora del plan
         */
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
                message = message.Replace("rutaEmailHTML", "http://diagnostic4u.com/templates/enviados/email_" + uid + ".html");
                var ruta = @"\\\\10.5.2.101\\RHDiagnostics\\templates\\enviados\\";
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
        /// <returns></returns>
        public static ML.Result GetPlanes(int UserId, bool IsResponsable)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    // Si es un usuario responsable solo puede ver los planes en donde es participante
                    // Si es un usuario admnistrador de planes de accion puede solo ver los planes de acción que ha creado
                    if (IsResponsable)
                    {
                        var ResponsableAccionesPlan = context.ResponsablesAccionesPlan.Where(o => o.IdResponsable == UserId);
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

                                    result.Objects.Add(planDeAccion);
                                }
                            }
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        var PlanesDeAccion = context.PlanDeAccion.Where(o => o.IdUsuarioCreacion == UserId);
                        foreach (var plan in PlanesDeAccion)
                        {
                            ML.PlanDeAccion planDeAccion = new ML.PlanDeAccion();
                            planDeAccion.IdPlanDeAccion = plan.IdPlanDeAccion;
                            planDeAccion.Nombre = plan.Nombre;
                            planDeAccion.IdEncuesta = (int)plan.IdEncuesta;
                            planDeAccion.IdBaseDeDatos = (int)plan.IdBaseDeDatos;

                            result.Objects.Add(planDeAccion);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception aE)
            {
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
            }
            return result;
        }
        /// <summary>
        /// 
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
                                accionesPlan.PlanDeAccion.IdPlanDeAccion = IdPlanDeAccion;
                                accionesPlan.PlanDeAccion.Nombre = NombrePlanDeAccion;
                                accionesPlan.sFechaInicio = FechaInicio;
                                accionesPlan.sFechaFin = FechaFin;
                                accionesPlan.Objetivo = Objetivo;
                                accionesPlan.Meta = Meta;
                                accionesPlan.Comentarios = Comentarios;
                                accionesPlan.ListResponsable.Add(new ML.Responsable() { Nombre = NombreResponsable, Email = EmailResponsable });
                                accionesPlan.AccionesDeMejora.IdAccionDeMejora = IdAccion;
                                accionesPlan.AccionesDeMejora.Descripcion = DescripcionAccion;
                                accionesPlan.AccionesDeMejora.Categoria.IdCategoria = IdCategoria;
                                accionesPlan.AccionesDeMejora.Categoria.Descripcion = DescripcionCategoria;
                                accionesPlan.PorcentajeAvance = Convert.ToDecimal(accionPlan.PorcentajeAvance);
                                var Seguimiento = context.Seguimiento.Where(o => o.IdResponsableAccionesPlan == ResponsableAccionesPlan.IdResponsablesAccionesPlan).ToList();
                                foreach (var seguimento in Seguimiento)
                                {
                                    var SeguimientoEvidencia = context.SeguimientoEvidencia.Where(o => o.IdSeguimiento == seguimento.IdSeguimiento).ToList();
                                    foreach (var seguimientoEvidencia in SeguimientoEvidencia)
                                    {
                                        var evidencia = context.Evidencia.Where(o => o.IdEvidencia == seguimientoEvidencia.IdEvidencia && o.IdEstatus == 1).FirstOrDefault();
                                        if (evidencia != null)
                                            accionesPlan.Atachments.Add(evidencia.Ruta);
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
                                // El porcentaje de la categoria se obtiene al vuelo en el js

                                result.Objects.Add(accionesPlan);
                            }
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        // Obtener todas las acciones del plan ya que es un admin
                        var AccionesPlan = context.AccionesPlan.Where(o => o.IdPlanDeAccion == IdPlan);
                        foreach (var accionPlan in AccionesPlan)
                        {
                            int IdPlanDeAccion = (int)accionPlan.IdPlanDeAccion;
                            string NombrePlanDeAccion = context.PlanDeAccion.Where(o => o.IdPlanDeAccion == IdPlanDeAccion).FirstOrDefault().Nombre;
                            DateTime FechaFin = (DateTime)accionPlan.FechaFin;
                            int IdAccion = (int)accionPlan.IdAccion;
                            var DataAccion = context.Acciones.Where(o => o.IdAccion == IdAccion).FirstOrDefault();
                            string DescripcionAccion = DataAccion.Descripcion;
                            int IdCategoria = (int)DataAccion.IdCategoria;
                            string DescripcionCategoria = context.Categoria.Where(o => o.IdCategoria == IdCategoria).FirstOrDefault().Nombre;
                            
                            ML.AccionesPlan accionesPlan = new ML.AccionesPlan();
                            accionesPlan.PlanDeAccion.IdPlanDeAccion = IdPlanDeAccion;
                            accionesPlan.PlanDeAccion.Nombre = NombrePlanDeAccion;
                            accionesPlan.FechaFin = FechaFin;
                            accionesPlan.AccionesDeMejora.IdAccionDeMejora = IdAccion;
                            accionesPlan.AccionesDeMejora.Descripcion = DescripcionAccion;
                            accionesPlan.AccionesDeMejora.Categoria.IdCategoria = IdCategoria;
                            accionesPlan.AccionesDeMejora.Categoria.Descripcion = DescripcionCategoria;
                            accionesPlan.PorcentajeAvance = Convert.ToDecimal(accionPlan.PorcentajeAvance);
                            // El porcentaje de la categoria se obtiene al vuelo en el js

                            result.Objects.Add(accionesPlan);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception aE)
            {
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
        /// <returns></returns>
        public static ML.Result GuardarRutaArchivo(string ruta, string IdPlan, string IdAccion, int IdResponsable)
        {
            ML.Result result = new ML.Result();
            try
            {
                int _idPlan = Convert.ToInt32(IdPlan.Split('_')[1]);//"IdPlan_3"
                int _idAccion = Convert.ToInt32(IdAccion.Split('_')[1]);
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    DL.Evidencia evidencia = new DL.Evidencia();
                    evidencia.Ruta = ruta;
                    evidencia.IdEstatus = 1;
                    context.Evidencia.Add(evidencia);
                    context.SaveChanges();//Agregar evidencia

                    var AccionesPlan = context.AccionesPlan.Where(o => o.IdPlanDeAccion == _idPlan && o.IdAccion == _idAccion).FirstOrDefault();
                    var ResponsableAccionesPlan = context.ResponsablesAccionesPlan.Where(o => o.IdAccionesPlan == AccionesPlan.IdAccionesPlan).FirstOrDefault();
                    DL.Seguimiento seguimiento = new DL.Seguimiento();
                    seguimiento.IdResponsableAccionesPlan = ResponsableAccionesPlan.IdResponsablesAccionesPlan;
                    context.Seguimiento.Add(seguimiento);
                    context.SaveChanges();//Agregar seguimiento

                    DL.SeguimientoEvidencia seguimientoEvidencia = new DL.SeguimientoEvidencia();
                    seguimientoEvidencia.IdSeguimiento = seguimiento.IdSeguimiento;
                    seguimientoEvidencia.IdEvidencia = evidencia.IdEvidencia;
                    context.SeguimientoEvidencia.Add(seguimientoEvidencia);
                    context.SaveChanges();//Agregar SeguimientoEvidencia
                }
            }
            catch (Exception aE)
            {
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
                string serverRoute = ruta.Replace("http://diagnostic4u.com/PlanesDeAccion//", @"\\\\10.5.2.101\\RHDiagnostics\\PlanesDeAccion\\");
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
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
            }
            return result;
        }
        #endregion Seguimiento
    }
}
