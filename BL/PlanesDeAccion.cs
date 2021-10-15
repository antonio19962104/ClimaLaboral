using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class PlanesDeAccion
    {
        /// <summary>
        /// Obtiene las categorias en orden por promedio obtenido
        /// </summary>
        /// <param name="promedioSubCategorias"></param>
        /// <returns></returns>
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
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
            }
            return result;
        }
        /// <summary>
        /// Obtiene el promedio generar de una categoria
        /// </summary>
        /// <param name="promedioSubCategorias"></param>
        /// <returns></returns>
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
                return 0;
            }
        }
        /// <summary>
        /// Agrega o actualiza los promedios obtenidos de cada subcategoria
        /// </summary>
        /// <param name="promedioSubCategorias"></param>
        /// <returns></returns>
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
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
            }
            return result;
        }
        /// <summary>
        /// Obtiene los promedios de las subcategorias que previamenrte generó el job
        /// </summary>
        /// <param name="promedioSubCategorias"></param>
        /// <returns></returns>
        public static ML.Result GetPromediosSubCategorias(ML.PromedioSubCategorias promedioSubCategorias)
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
                    Console.WriteLine("Termne");
                }
            }
            catch (Exception)
            {

            }
        }
        /// <summary>
        /// Verifica si un area tiene ya generados sus promedios por subcategoria
        /// </summary>
        /// <param name="promedioSubCategorias"></param>
        /// <returns></returns>
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
                existe = false;
            }
            return existe;
        }
        public static ML.Result AddPlanDeAccion(ML.PlanDeAccion planDeAccion)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var dataPlanDeAccion = new DL.PlanDeAccion() {
                        Nombre = planDeAccion.Nombre,
                        IdUsuarioCreacion = planDeAccion.IdAdminCreate,
                        
                    };
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
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
            }
            result.Correct = true;
            return result;
        }
    }
}
