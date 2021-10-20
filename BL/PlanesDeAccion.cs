using System;
using System.Collections.Generic;
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
        /// <summary>
        /// Agrega un nuevo plan de acción
        /// </summary>
        /// <param name="planDeAccion"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Obtiene los rangos establecidos para los planes de acción
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Obtiene las acciones preguardadas para una encuesta
        /// </summary>
        /// <param name="accionDeMejora"></param>
        /// <returns></returns>
        public static ML.Result GetAcciones(ML.AccionDeMejora accionDeMejora)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var dataAcciones = context.Acciones.
                        Where(o =>
                        o.IdEncuesta == accionDeMejora.Encuesta.IdEncuesta &&
                        o.IdBaseDeDatos == accionDeMejora.BasesDeDatos.IdBaseDeDatos &&
                        o.AnioAplicacion == accionDeMejora.AnioAplicacion &&
                        o.IdEstatus == 1).ToList().OrderBy(o => o.IdCategoria);
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
        /// Agrega una nueva acción de mejora
        /// </summary>
        /// <param name="accionDeMejora"></param>
        /// <param name="UsuarioActual"></param>
        /// <returns></returns>
        public static ML.Result AddAccion(ML.AccionDeMejora accionDeMejora, string UsuarioActual)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
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
                            FechaHoraCreacion = DateTime.Now,
                            UsuarioCreacion = UsuarioActual,
                            ProgramaCreacion = "Modulo Planes de Acción"
                        };
                        context.Acciones.Add(accion);
                        context.SaveChanges();
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
                            Accion.FechaHoraModificacion = DateTime.Now;
                            Accion.UsuarioModificacion = UsuarioActual;
                            Accion.ProgramaModificacion = "Modulo Planes de Acción";
                        }
                        context.SaveChanges();
                        result.Correct = true;
                        result.NewId = Accion.IdAccion;
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
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontró la acción con el Id: " + IdAccion;
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
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
            }
            return result;
        }

    }
}
