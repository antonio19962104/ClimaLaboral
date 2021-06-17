using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Web.Mvc;

namespace BL
{
    public class ReporteClimaDinamico
    {
        // numero de encuestas terminadas
        public static double getEncuestasTerminadas(ML.modelReporte aModel)
        {
            try
            {
                DataSet ds = new DataSet();
                using (SqlConnection connectionSqlServer = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    string query = getQueryEncuestasTerminadas(aModel);
                    SqlDataAdapter data = new SqlDataAdapter(query, connectionSqlServer);
                    data.Fill(ds, "data");
                    aModel.result = ds.Tables[0].Rows.Count;
                }
                if (!string.IsNullOrEmpty(aModel.filtroDemografico))
                    aModel.result = aplicarFiltroDemografico(ds, aModel);
            }
            catch (Exception aE)
            {
                NLogGeneratorFile.logError(aE, new StackTrace());
                return 0;
            }
            return aModel.result.TruncateNumber();
        }
        // numero de encuestas esperadas
        public static double getEncuestasEsperadas(ML.modelReporte aModel)
        {
            try
            {
                DataSet ds = new DataSet();
                using (SqlConnection connectionSqlServer = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    string query = getQueryEncuestasEsperadas(aModel);
                    SqlDataAdapter data = new SqlDataAdapter(query, connectionSqlServer);
                    data.Fill(ds, "data");
                    aModel.result = ds.Tables[0].Rows.Count;
                }
                if (!string.IsNullOrEmpty(aModel.filtroDemografico))
                    aModel.result = aplicarFiltroDemografico(ds, aModel);
            }
            catch (Exception aE)
            {
                NLogGeneratorFile.logError(aE, new StackTrace());
                return 0;
            }
            return aModel.result.TruncateNumber();
        }
        // porcentaje de participacion en la encuesta
        public static double getPorcentajeParticipacion(ML.modelReporte aModel)
        {
            try
            {
                double terminadas = getEncuestasTerminadas(aModel);
                double esperadas = getEncuestasEsperadas(aModel);
                return ((terminadas / esperadas) * 100).TruncateNumber();
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return 0; ;
            }
        }
        // numero de respuestas afirmativas por pregunta
        public static double getAfirmativasByIdPregunta(ML.modelReporte aModel)
        {
            try
            {
                DataSet ds = new DataSet();
                using (SqlConnection connectionSqlServer = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    string query = getQueryAfirmativasByReactivo(aModel);
                    SqlDataAdapter data = new SqlDataAdapter(query, connectionSqlServer);
                    data.Fill(ds, "data");
                    aModel.result = ds.Tables[0].Rows.Count;
                }
                if (!string.IsNullOrEmpty(aModel.filtroDemografico))
                    aModel.result = aplicarFiltroDemografico(ds, aModel);
            }
            catch (Exception aE)
            {
                NLogGeneratorFile.logError(aE, new StackTrace());
                return 0;
            }
            return aModel.result.TruncateNumber();
        }
        // porcentaje de respuestas afirmativas por id de pregunta
        public static double getPorcentajeAfirmativasByIdPregunta(ML.modelReporte aModel)
        {
            try
            {
                double afirmativas = getAfirmativasByIdPregunta(aModel);
                double terminadas = getEncuestasTerminadas(aModel);
                aModel.result = (afirmativas / terminadas) * 100;
            }
            catch (Exception aE)
            {
                NLogGeneratorFile.logError(aE, new StackTrace());
                return 0;
            }
            return aModel.result.TruncateNumber();
        }
        // numero de respuestas afirmativas por competencia
        public static double getAfirmativasByCompetencia(ML.modelReporte aModel)
        {
            try
            {
                DataSet ds = new DataSet();
                using (SqlConnection connectionSqlServer = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    string query = getQueryAfirmativasByCompetencia(aModel);
                    SqlDataAdapter data = new SqlDataAdapter(query, connectionSqlServer);
                    data.Fill(ds, "data");
                    aModel.result = ds.Tables[0].Rows.Count;
                }
                if (!string.IsNullOrEmpty(aModel.filtroDemografico))
                    aModel.result = aplicarFiltroDemografico(ds, aModel);
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return 0;
            }
            return aModel.result.TruncateNumber();
        }
        // porcentaje de respuestas afirmativas por competencia
        public static double getPorcentajeAfirmativasByCompetencia(ML.modelReporte aModel)
        {
            try
            {
                double afirmativas = getAfirmativasByCompetencia(aModel);
                double terminadas = getEncuestasTerminadas(aModel) * getNumPreguntasByCompetencia(aModel.idCompetencia, aModel.idEncuesta);
                aModel.result = (afirmativas / terminadas) * 100;
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return 0;
            }
            return aModel.result.TruncateNumber();
        }
        // numero de respuestas afirmativas por categoria-subcategoria
        public static double getAfirmativasByCategoria(ML.modelReporte aModel)
        {
            try
            {
                DataSet ds = new DataSet();
                using (SqlConnection connectionSqlServer = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    string query = getQueryAfirmativasBySubCategoria(aModel);
                    SqlDataAdapter data = new SqlDataAdapter(query, connectionSqlServer);
                    data.Fill(ds, "data");
                    aModel.result = ds.Tables[0].Rows.Count;
                }
                if (!string.IsNullOrEmpty(aModel.filtroDemografico))
                    aModel.result = aplicarFiltroDemografico(ds, aModel);
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return 0;
            }
            return aModel.result.TruncateNumber();
        }
        // porcentaje de respuestas afirmativas por categoria-subcategoria
        public static double getPorcentajeAfirmativasByCategoria(ML.modelReporte aModel)
        {
            try
            {
                // validar si tiene subcategorias o no
                if (tieneSubCategorias(aModel.idSubCategoria, aModel.idEncuesta))
                {
                    var subCategorias = getSubCategoriasFromCategorias(aModel.idSubCategoria);
                    double sumaGlobal = 0;
                    foreach (var item in subCategorias)
                    {
                        // obtener las preguntas de cada subc
                        var preguntas = getPreguntasBySubCategoria(item, aModel.idEncuesta);
                        double sum = 0;
                        foreach (var idpregunta in preguntas)
                        {
                            var modelAux = new ML.modelReporte();
                            modelAux = aModel;
                            modelAux.idPregunta = idpregunta;
                            double afirmativas = getPorcentajeAfirmativasByIdPregunta(modelAux);
                            sum += afirmativas;
                        }
                        sumaGlobal += (sum / preguntas.Count).TruncateNumber();
                    }
                    aModel.result = sumaGlobal / subCategorias.Count;
                }
                else
                {
                    double afirmativas = getAfirmativasByCategoria(aModel);
                    double terminadas = getEncuestasTerminadas(aModel) * getNumPreguntasByCategoria(aModel.idSubCategoria, aModel.idEncuesta);
                    aModel.result = (afirmativas / terminadas) * 100;
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return 0;
            }
            return aModel.result.TruncateNumber();
        }
        // reactivos mejor clasificados
        public static List<ML.modelMejoresPeores> getMejores(ML.modelReporte aModel)
        {
            try
            {
                var list = new List<ML.modelMejoresPeores>();
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    string query = getQueryMejores(aModel);
                    SqlDataAdapter data = new SqlDataAdapter(query, conn);
                    data.Fill(ds, "data");
                }
                var terminadas = getEncuestasTerminadas(aModel);
                foreach (DataRow dataRow in ds.Tables[0].Rows)
                {
                    var model = new ML.modelMejoresPeores()
                    {
                        IdPregunta = Convert.ToInt32(dataRow.ItemArray[0]),
                        Pregunta = Convert.ToString(dataRow.ItemArray[1]),
                        Porcentaje = ((Convert.ToDouble(dataRow.ItemArray[2]) / terminadas) * 100).TruncateNumber()
                    };
                    list.Add(model);
                }
                return list;
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return new List<ML.modelMejoresPeores>();
            }
        }
        // reactivos peor clasificados
        public static List<ML.modelMejoresPeores> getPeores(ML.modelReporte aModel)
        {
            try
            {
                var list = new List<ML.modelMejoresPeores>();
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    string query = getQueryPeores(aModel);
                    SqlDataAdapter data = new SqlDataAdapter(query, conn);
                    data.Fill(ds, "data");
                }
                var terminadas = getEncuestasTerminadas(aModel);
                foreach (DataRow dataRow in ds.Tables[0].Rows)
                {
                    var model = new ML.modelMejoresPeores()
                    {
                        IdPregunta = Convert.ToInt32(dataRow.ItemArray[0]),
                        Pregunta = Convert.ToString(dataRow.ItemArray[1]),
                        Porcentaje = ((Convert.ToDouble(dataRow.ItemArray[2]) / terminadas) * 100).TruncateNumber()
                    };
                    list.Add(model);
                }
                return list;
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return new List<ML.modelMejoresPeores>();
            }
        }
        // reactivos con mayor-menor crecimiento
        public static List<ML.modelCrecimientoReactivo> getCrecimiento(ML.modelReporte aModel)
        {
            try
            {
                var list = new List<ML.modelCrecimientoReactivo>();
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    string query = getQueryCrecimientoByReactivo(aModel);
                    SqlDataAdapter data = new SqlDataAdapter(query, conn);
                    data.Fill(ds, "data");
                }
                var terminadas = getEncuestasTerminadas(aModel);
                foreach (DataRow dataRow in ds.Tables[0].Rows)
                {
                    int _idPregunta = Convert.ToInt32(dataRow.ItemArray[0]);
                    aModel.idPregunta = _idPregunta;
                    var model = new ML.modelCrecimientoReactivo()
                    {
                        IdPregunta = _idPregunta,
                        PorcentajeActual = ((Convert.ToDouble(dataRow.ItemArray[3]) / terminadas) * 100).TruncateNumber(),
                        DiferenciaActualAnterior = ((Convert.ToDouble(dataRow.ItemArray[3]) / terminadas) * 100).TruncateNumber() - getValorHistoricoByPregunta(aModel)
                    };
                    model.DiferenciaActualAnterior = model.DiferenciaActualAnterior.TruncateNumber();
                    list.Add(model);
                }
                return list;
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return new List<ML.modelCrecimientoReactivo>();
            }
        }
        // calificacion global (promedio de 66 reactivos)
        public static double getPromedio66R(ML.modelReporte aModel)
        {
            try
            {
                DataSet ds = new DataSet();
                double afirmativas = 0;
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    string query = getQueryPromedio66Reactivos(aModel);
                    SqlDataAdapter data = new SqlDataAdapter(query, conn);
                    data.Fill(ds, "data");
                    afirmativas = Convert.ToDouble(ds.Tables[0].Rows.Count);
                }
                if (!string.IsNullOrEmpty(aModel.filtroDemografico))
                    afirmativas = aplicarFiltroDemografico(ds, aModel);
                var terminadas = getEncuestasTerminadas(aModel) * getNumPreguntasActivasByEncuesta(aModel.idEncuesta, 66, aModel.idEnfoque);
                aModel.result = ((afirmativas / terminadas) * 100);
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return 0;
            }
            return aModel.result.TruncateNumber();
        }
        public static double getPorcentaje86Reactivos(ML.modelReporte aModel)
        {
            try
            {
                DataSet ds = new DataSet();
                double afirmativas = 0;
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    string query = getQueryPromedio66Reactivos(aModel);
                    SqlDataAdapter data = new SqlDataAdapter(query, conn);
                    data.Fill(ds, "data");
                    afirmativas = Convert.ToDouble(ds.Tables[0].Rows.Count);
                }
                if (!string.IsNullOrEmpty(aModel.filtroDemografico))
                    afirmativas = aplicarFiltroDemografico(ds, aModel);
                var terminadas = getEncuestasTerminadas(aModel) * getNumPreguntasActivasByEncuesta(aModel.idEncuesta, 66, aModel.idEnfoque);
                aModel.result = ((afirmativas / terminadas) * 100);
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return 0;
            }
            return aModel.result.TruncateNumber();
        }
        // reporteo de la competencia de permanencia
        public static List<ML.modelPermanenciaAbandono> getDataPermanenciaAbandono(ML.modelReporte aModel)
        {
            try
            {
                var list = new List<ML.modelPermanenciaAbandono>();
                var listPreg = new List<int>();
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    listPreg.AddRange(getPreguntasByCompetencia(aModel.idCompetencia, aModel.idEncuesta, 1));
                    double _terminadas = getEncuestasTerminadas(aModel);
                    foreach (var idpregunta in listPreg)
                    {
                        string query = String.Format(ML.modelReporte.queryPermanencia, idpregunta, getTipoEntidad(aModel.idTipoEntidad), aModel.entidadNombre, aModel.anioActual, aModel.idEncuesta);
                        SqlDataAdapter data = new SqlDataAdapter(query, conn);
                        data.Fill(ds, "data");// respuesta, frecuencia
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            ML.modelPermanenciaAbandono modelPermanenciaAbandono = new ML.modelPermanenciaAbandono
                            {
                                IdPregunta = idpregunta,
                                Porcentaje = ((Convert.ToDouble(row.ItemArray[1]) / _terminadas) * 100).TruncateNumber(),
                                Pregunta = Convert.ToString(row.ItemArray[0]),
                                Frecuencia = Convert.ToInt32(row.ItemArray[1]),
                                EntidadNombre = aModel.entidadNombre,
                                IdEntidad = aModel.idEntidad,
                                IdTipoEntidad = aModel.idTipoEntidad
                            };
                            list.Add(modelPermanenciaAbandono);
                        }
                    }
                }
                return list;
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return new List<ML.modelPermanenciaAbandono>();
            }
        }

        /// <summary>
        /// obtiene el numero de preguntas activas en un rango de 1 a n en el campo de IdPreguntaPadre
        /// </summary>
        /// <param name="aIdEncuesta"></param>
        /// <param name="aNumPreguntas"></param>
        /// <returns></returns>
        public static double getNumPreguntasActivasByEncuesta(int aIdEncuesta, int aNumPreguntas, int aIdEnfoque)
        {
            try
            {
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    string query = String.Format("select idpregunta from preguntas where idencuesta = {0} and idestatus = 1 and idpreguntaPadre between 1 and {1} and idenfoque = {2}", aIdEncuesta, aNumPreguntas, aIdEnfoque);
                    SqlDataAdapter data = new SqlDataAdapter(query, conn);
                    data.Fill(ds, "data");
                    return Convert.ToDouble(ds.Tables[0].Rows.Count);
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return 0;
            }
        }

        public static bool isActivePregunta(int aIdPregunta, int aIdEncuesta)
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var data = context.Preguntas.Select(o => new { o.idEncuesta, o.IdPregunta, o.IdEstatus }).Where(o => o.idEncuesta == aIdEncuesta && o.IdPregunta == o.IdPregunta && o.IdEstatus == 1).FirstOrDefault();
                    if (data != null)
                        return true;
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return false;
            }
            return true;
        }

        public static bool isActiveCompetencia(int aIdCompetencia, int aIdEncuesta)
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var data = context.Competencia.Select(o => new { o.IdCompetencia, o.IdEstatus }).Where(o => o.IdCompetencia == aIdCompetencia && o.IdEstatus == 1).FirstOrDefault();
                    if (data != null)
                        return true;
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return false;
            }
            return true;
        }

        public static bool isActiveCategoria(int aIdCategoria, int aidEncuesta)
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var data = context.Categoria.Select(o => new { o.IdCategoria, o.Estatus }).Where(o => o.IdCategoria == aIdCategoria && o.Estatus == 1).FirstOrDefault();
                    if (data != null)
                        return true;
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return false;
            }
            return true;
        }


        #region generar queries dinamicos
        public static string getQueryAfirmativasByReactivo(ML.modelReporte aModel)
        {
            try
            {
                return String.Format(ML.modelReporte.queryAfirmativasByReactivo, aModel.idPregunta, aModel.anioActual, getTipoEntidad(aModel.idTipoEntidad), aModel.entidadNombre, aModel.idEnfoque, aModel.idEncuesta);
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return string.Empty;
            }
        }
        public static string getQueryEncuestasTerminadas(ML.modelReporte aModel)
        {
            try
            {
                return String.Format(ML.modelReporte.queryEncuestasTerminadas, aModel.anioActual, getTipoEntidad(aModel.idTipoEntidad), aModel.entidadNombre, aModel.idEncuesta);
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return string.Empty;
            }
        }
        public static string getQueryEncuestasEsperadas(ML.modelReporte aModel)
        {
            try
            {
                return String.Format(ML.modelReporte.queryEncuestasEsperadas, aModel.anioActual, getTipoEntidad(aModel.idTipoEntidad), aModel.entidadNombre, aModel.idEncuesta);
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return string.Empty;
            }
        }
        public static string getQueryAfirmativasByCompetencia(ML.modelReporte aModel)
        {
            try
            {
                return String.Format(ML.modelReporte.queryPorcentajeAfirmativasByCompetencia, aModel.idCompetencia, aModel.anioActual, getTipoEntidad(aModel.idTipoEntidad), aModel.entidadNombre, aModel.idEnfoque, aModel.idEncuesta);
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return string.Empty;
            }
        }
        public static string getQueryAfirmativasBySubCategoria(ML.modelReporte aModel)
        {
            try
            {
                return String.Format(ML.modelReporte.queryPorcentajeAfirmativasBySubCategoria, aModel.idSubCategoria, aModel.anioActual, getTipoEntidad(aModel.idTipoEntidad), aModel.entidadNombre, aModel.idEnfoque, aModel.idEncuesta);
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return string.Empty;
            }
        }
        public static string getQueryMejores(ML.modelReporte aModel)
        {
            try
            {
                return String.Format(ML.modelReporte.queryMejores, aModel.anioActual, aModel.idEnfoque, aModel.idEncuesta, getTipoEntidad(aModel.idTipoEntidad), aModel.entidadNombre);
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return string.Empty;
            }
        }
        public static string getQueryPeores(ML.modelReporte aModel)
        {
            try
            {
                return String.Format(ML.modelReporte.queryPeores, aModel.anioActual, aModel.idEnfoque, aModel.idEncuesta, getTipoEntidad(aModel.idTipoEntidad), aModel.entidadNombre);
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return string.Empty;
            }
        }
        public static string getQueryCrecimientoByReactivo(ML.modelReporte aModel)
        {
            try
            {
                return String.Format(ML.modelReporte.queryCrecimientoByReactivo, aModel.anioActual, aModel.idEncuesta, getTipoEntidad(aModel.idTipoEntidad), aModel.entidadNombre, aModel.idEnfoque);
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return string.Empty;
            }
        }
        public static string getQueryPromedio66Reactivos(ML.modelReporte aModel)
        {
            try
            {
                return string.Format(ML.modelReporte.queryPromedio66Reactivos, aModel.anioActual, aModel.idEncuesta, getTipoEntidad(aModel.idTipoEntidad), aModel.entidadNombre, aModel.idEnfoque);
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return string.Empty;
            }
        }
        public static string getQueryPermanencia(ML.modelReporte aModel)
        {
            try
            {
                return String.Format(ML.modelReporte.queryPermanencia, aModel.idPregunta, getTipoEntidad(aModel.idTipoEntidad), aModel.entidadNombre, aModel.anioActual, aModel.idEncuesta);
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return string.Empty;
            }
        }
        #endregion

        public static List<int> getPreguntasByCompetencia(int aIdCompetencia, int aIdEncuesta, int aIdEnfoque)
        {
            var list = new List<int>();
            try
            {
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    string query = String.Format("select idpregunta from Preguntas where IdCompetencia = {0} and IdEstatus = 1 and idEncuesta = {1} and IdEnfoque = {2}", aIdCompetencia, aIdEncuesta, aIdEnfoque);
                    SqlDataAdapter data = new SqlDataAdapter(query, conn);
                    data.Fill(ds, "data");
                }
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    list.Add(Convert.ToInt32(item.ItemArray[0]));
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return new List<int>();
            }
            return list;
        }

        /// <summary>
        /// obtencion de la diferencia 0de un reactivo respeto a su historico
        /// </summary>
        /// <param name="aModel"></param>
        /// <returns>double con la diferencia</returns>
        public static double getValorHistoricoByPregunta(ML.modelReporte aModel)
        {
            try
            {
                DataSet ds = new DataSet();
                string enfoque = aModel.idEnfoque == 1 ? "Enfoque empresa" : "Enfoque Area";
                int cantidadPreguntas1Enfoque = getCantidadPreguntas1Enfoque(aModel.idEncuesta);
                if (aModel.idEnfoque == 2)
                    aModel.idPregunta -= cantidadPreguntas1Enfoque;

                string cve_columna = "Preg" + aModel.idPregunta;
                int anioHistorico = aModel.anioActual - 1;
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    string query = String.Format("select " + cve_columna + " from HistoricoClima where EntidadId = {0} and EntidadNombre = '{1}' and Anio = {2} and IdTipoEntidad = {3} and Enfoque = '{4}'", aModel.idEntidad, aModel.entidadNombre, anioHistorico, aModel.idTipoEntidad, enfoque);
                    SqlDataAdapter data = new SqlDataAdapter(query, conn);
                    data.Fill(ds, "data");
                }
                // obtener el valor de la propiedad Preg + aModel.IdPregunta
                var value = Convert.ToDouble(ds.Tables[0].Rows[0].ItemArray[0]);
                return value.TruncateNumber();
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return 0;
            }
        }

        public static int getCantidadPreguntas1Enfoque(int aIdEncuesta)
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    // select * from Preguntas where IdEnfoque = 2 and idEncuesta = 1
                    var query = context.Preguntas.Select(o => new { o.IdEnfoque, o.idEncuesta }).Where(o => o.IdEnfoque == 2 && o.idEncuesta == aIdEncuesta).Count();
                    return query;
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return 0;
            }
        }

        /// <summary>
        /// obtener valoracion por cada pregunta (valoracion en competencia y en subcategoria)
        /// </summary>
        /// <param name="aIdEncuesta"></param>
        /// <param name="aIdPregunta"></param>
        /// <returns>modelo de valoraciones model.ValorPregunta, model.valorPorSubcategoria</returns>
        public static ML.modelValoraciones getValoraciones(int aIdEncuesta, int aIdPregunta, int aIdSubCategoria, int aIdCompetencia)
        {
            var model = new ML.modelValoraciones();
            try
            {
                model.IdEncuesta = aIdEncuesta;
                model.IdPregunta = aIdPregunta;
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    model.ValorPregunta = Convert.ToDouble(context.Preguntas.Select(o => new { o.idEncuesta, o.IdPregunta, o.Valoracion, o.IdCompetencia }).Where(o => o.idEncuesta == aIdEncuesta && o.IdPregunta == aIdPregunta && o.IdCompetencia == aIdCompetencia).FirstOrDefault().Valoracion);
                    model.valorPorSubcategoria = Convert.ToDouble(context.ValoracionPreguntaPorSubcategoria.Select(o => new { o.IdEncuesta, o.IdPregunta, o.Valor, o.IdSubcategoria }).Where(o => o.IdEncuesta == aIdEncuesta && o.IdPregunta == aIdPregunta && o.IdSubcategoria == aIdSubCategoria).FirstOrDefault().Valor);
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return new ML.modelValoraciones();
            }
            return model;
        }

        public static double getValoracionByIdPregunta(int aIdEncuesta, int aIdPregunta)
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var data = context.Preguntas.Select(o => new { o.idEncuesta, o.IdPregunta, o.Valoracion }).Where(o => o.idEncuesta == aIdEncuesta && o.IdPregunta == aIdPregunta).FirstOrDefault();
                    if (data != null)
                        return Convert.ToDouble(data.Valoracion);
                    else
                        return 0;
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return 0;
            }
        }

        public static double getValoracionPreguntaBySubCategoria(int aIdEncuesta, int aIdPregunta, int aIdSubCategoria)
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var data = context.ValoracionPreguntaPorSubcategoria.Select(o => o).Where(o => o.IdEncuesta == aIdEncuesta && o.IdPregunta == aIdPregunta && o.IdSubcategoria == aIdSubCategoria).FirstOrDefault();
                    if (data != null)
                        return Convert.ToDouble(data.Valor);
                    else
                        return 0;
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return 0;
            }
        }

        /// <summary>
        /// obtener la descripcion de la entidad en base al id
        /// </summary>
        /// <param name="aIdTipoEntidad"></param>
        /// <returns>cadena de la descripcion del tipo de entidad</returns>
        public static string getTipoEntidad(int aIdTipoEntidad)
        {
            switch (aIdTipoEntidad)
            {
                case 1:
                    return "UnidadNegocio";
                case 2:
                    return "DivisionMarca";
                case 3:
                    return "AreaAgencia";
                case 4:
                    return "Depto";
                case 5:
                    return "Subdepartamento";
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// obtencion del numero de preguntas asociadas a una competencia
        /// </summary>
        /// <param name="aIdCompetencia"></param>
        /// <param name="aIdEncuesta"></param>
        /// <returns>numero de preguntas pertenecientes a una competencia</returns>
        public static int getNumPreguntasByCompetencia(int aIdCompetencia, int aIdEncuesta)
        {
            try
            {
                // var preguntas = context.Preguntas.Select(o => new { o.IdPregunta, o.IdCompetencia, o.idEncuesta, o.IdEnfoque }).Where(o => o.IdCompetencia == aIdCompetencia && o.idEncuesta == aIdEncuesta && o.IdEnfoque == 1).ToList(); // solo tomamos las de enfoque empresa debido a la duplicidad
                using (SqlConnection connectionSqlServer = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    DataSet ds = new DataSet();
                    string query = String.Format("select idpregunta from preguntas where idcompetencia = {0} and idencuesta = {1} and idenfoque = 1 and idestatus = 1", aIdCompetencia, aIdEncuesta);
                    SqlDataAdapter data = new SqlDataAdapter(query, connectionSqlServer);
                    data.Fill(ds, "data");
                    return ds.Tables[0].Rows.Count;
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return 0;
            }
        }

        public static List<int> getPreguntasBySubCategoria(int aIdSubCategoria, int aIdEncuesta)
        {
            try
            {
                var list = new List<int>();
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    string query = String.Format(@"select Preguntas.* from ValoracionPreguntaPorSubcategoria
                        inner join Preguntas on ValoracionPreguntaPorSubcategoria.IdPregunta = Preguntas.IdPregunta
                        where ValoracionPreguntaPorSubcategoria.IdEncuesta = {0} and ValoracionPreguntaPorSubcategoria.IdSubcategoria = {1}", aIdEncuesta, aIdSubCategoria);
                    SqlDataAdapter data = new SqlDataAdapter(query, conn);
                    data.Fill(ds, "data");
                }
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    list.Add(Convert.ToInt32(row.ItemArray[0]));
                }
                return list;
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return new List<int>();
            }
        }


        public static List<int> getSubCategoriasByCategoria(int aIdCategoria, int aIdEncuesta)
        {
            try
            {
                var list = new List<int>();
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    string query = String.Format("select idCategoria from Categoria where IdPadre = {0}", aIdCategoria, aIdEncuesta);
                    SqlDataAdapter data = new SqlDataAdapter(query, conn);
                    data.Fill(ds, "data");
                }
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    list.Add(Convert.ToInt32(row.ItemArray[0]));
                }
                return list;
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return new List<int>();
            }
        }

        public static bool tieneSubCategorias(int aIdCategoria, int aIdEncuesta)
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var categoria = context.Categoria.Where(o => o.IdCategoria == aIdCategoria).FirstOrDefault();
                    var hijosCateg = context.Categoria.Where(o => o.IdPadre == categoria.IdCategoria && o.Estatus == 1).ToList();
                    if (hijosCateg != null && hijosCateg.Count > 0 && categoria.IdCategoria != hijosCateg[0].IdCategoria && categoria.Nombre != hijosCateg[0].Nombre)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return false;
            }
        }
        public static List<int> getSubCategoriasFromCategorias(int aIdCategoria)
        {
            try
            {
                var list = new List<int>();
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    string query = String.Format("select idcategoria from categoria where idpadre = {0} and estatus = 1", aIdCategoria);
                    SqlDataAdapter data = new SqlDataAdapter(query, conn);
                    data.Fill(ds, "data");
                }
                foreach (DataRow row in ds.Tables[0].Rows)
                    list.Add(Convert.ToInt32(row.ItemArray[0]));
                return list;
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return new List<int>();
            }
        }

        /// <summary>
        /// obtencion del numero de preguntas asociadas a una categoria - subcategoria
        /// </summary>
        /// <param name="aIdSubCategoria"></param>
        /// <param name="aIdEncuesta"></param>
        /// <returns>numero de preguntas pertenecientes a una categoria</returns>
        public static int getNumPreguntasByCategoria(int aIdSubCategoria, int aIdEncuesta)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    DataSet ds = new DataSet();
                    string query = String.Format("select * from ValoracionPreguntaPorSubcategoria where idencuesta = {0} and idsubcategoria = {1} and idenfoque = 1 and idestatus = 1", aIdEncuesta, aIdSubCategoria);
                    query = String.Format(@"select * from ValoracionPreguntaPorSubcategoria
                                            inner join Preguntas on ValoracionPreguntaPorSubcategoria.idpregunta = Preguntas.IdPregunta
                                            where 
                                            ValoracionPreguntaPorSubcategoria.idencuesta = {0} and 
                                            ValoracionPreguntaPorSubcategoria.idsubcategoria = {1} and 
                                            Preguntas.idenfoque = 1 and 
                                            Preguntas.idestatus = 1", aIdEncuesta, aIdSubCategoria);
                    conn.Open();
                    SqlDataAdapter data = new SqlDataAdapter(query, conn);
                    data.Fill(ds, "data");
                    return ds.Tables[0].Rows.Count;
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return 0;
            }
        }

        /// <summary>
        /// aplicacion del filtrado demografico en caso de ser necesario
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="aModel"></param>
        /// <returns>cantidad de afirmativas con un filtro demografico</returns>
        public static double aplicarFiltroDemografico(DataSet ds, ML.modelReporte aModel)
        {
            try
            {
                var list = new List<DL.Empleado>();
                // convert DataSet to Entity
                /*foreach (DataRow dataRow in ds.Tables[0].Rows)
                {
                    var e = GetEntity<DL.Empleado>(dataRow);
                    list.Add(e);
                }*/

                // var productNames = from products in ds.Tables[0].AsEnumerable() where (string)products.ItemArray[5] /*Field<string>("Sexo")*/ == "MASCULINO" select products; //products.Field<string>("Sexo");

                switch (aModel.filtroDemografico.ToLower())
                {
                    case "sexo":
                        // return list.Where(o => o.Sexo.ToUpper() == aModel.valorFiltroDemografico.ToUpper()).ToList().Count();
                        var data1 = from products in ds.Tables[0].AsEnumerable() where (string)products.ItemArray[5] == aModel.valorFiltroDemografico.ToUpper() select products;
                        return data1.Count();
                    case "puesto":
                        // return list.Where(o => o.Puesto.ToUpper() == aModel.valorFiltroDemografico.ToUpper()).ToList().Count();
                        var data2 = from products in ds.Tables[0].AsEnumerable() where (string)products.ItemArray[2] == aModel.valorFiltroDemografico.ToUpper() select products;
                        return data2.Count();
                    case "condiciontrabajo":
                        // return list.Where(o => o.CondicionTrabajo.ToUpper() == aModel.valorFiltroDemografico.ToUpper()).ToList().Count();
                        var data3 = from products in ds.Tables[0].AsEnumerable() where (string)products.ItemArray[0] == aModel.valorFiltroDemografico.ToUpper() select products;
                        return data3.Count();
                    case "gradoacademico":
                        // return list.Where(o => o.GradoAcademico.ToUpper() == aModel.valorFiltroDemografico.ToUpper()).ToList().Count();
                        var data4 = from products in ds.Tables[0].AsEnumerable() where (string)products.ItemArray[1] == aModel.valorFiltroDemografico.ToUpper() select products;
                        return data4.Count();
                    case "rangoantiguedad":
                        // return list.Where(o => o.RangoAntiguedad.ToUpper() == aModel.valorFiltroDemografico.ToUpper()).ToList().Count();
                        var data5 = from products in ds.Tables[0].AsEnumerable() where (string)products.ItemArray[3] == aModel.valorFiltroDemografico.ToUpper() select products;
                        return data5.Count();
                    case "rangoedad":
                        // return list.Where(o => o.RangoEdad.ToUpper() == aModel.valorFiltroDemografico.ToUpper()).ToList().Count();
                        var data6 = from products in ds.Tables[0].AsEnumerable() where (string)products.ItemArray[4] == aModel.valorFiltroDemografico.ToUpper() select products;
                        return data6.Count();
                    case "tipofuncion":
                        // return list.Where(o => o.TipoFuncion.ToUpper() == aModel.valorFiltroDemografico.ToUpper()).ToList().Count();
                        var data7 = from products in ds.Tables[0].AsEnumerable() where (string)products.ItemArray[6] == aModel.valorFiltroDemografico.ToUpper() select products;
                        return data7.Count();
                    default:
                        return 0;
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return 0;
            }
        }

        /// <summary>
        /// obtener listado de las competencias existentes en una encuesta
        /// </summary>
        /// <param name="aIdEncuesta"></param>
        /// <returns>listado con los id de competencia</returns>
        public static List<int> getCompetenciasByIdEncuesta(int aIdEncuesta)
        {
            try
            {
                var list = new List<int>();
                DataSet ds = new DataSet();
                using (SqlConnection connectionSqlServer = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    string query = String.Format(@"select distinct Preguntas.IdCompetencia, Competencia.Nombre from Preguntas inner join Competencia on Preguntas.IdCompetencia = Competencia.IdCompetencia where idEncuesta = {0} and preguntas.idestatus = 1 and competencia.idestatus = 1", aIdEncuesta);
                    SqlDataAdapter data = new SqlDataAdapter(query, connectionSqlServer);
                    data.Fill(ds, "data");
                }
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    list.Add(Convert.ToInt32(item.ItemArray[0]));
                }
                return list;
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return new List<int>();
            }
        }

        /// <summary>
        /// obtener listado de las categorias-subcategorias existentes en una encuesta
        /// </summary>
        /// <param name="aIdEncuesta"></param>
        /// <returns>listado con los id de categoria</returns>
        public static List<int> getCategoriasByIdEncuesta(int aIdEncuesta)
        {
            try
            {
                var list = new List<int>();
                DataSet ds = new DataSet();
                using (SqlConnection connectionSqlServer = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    string query = string.Format("select * from ValoracionPreguntaPorSubcategoria where idencuesta = {0}", aIdEncuesta);
                    SqlDataAdapter data = new SqlDataAdapter(query, connectionSqlServer);
                    data.Fill(ds, "data");
                }
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    list.Add(Convert.ToInt32(item.ItemArray[0]));
                }
                return list;
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return new List<int>();
            }
        }

        /// <summary>
        /// convierte un DataSet a entidades Entity Framework
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <returns>objeto del del namespace DL</returns>
        public static T GetEntity<T>(DataRow row) where T : new()
        {
            var entity = new T();
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                //Get the description attribute and set value in DataSet to Entity
                var descriptionAttribute = (DescriptionAttribute)property.GetCustomAttributes(typeof(DescriptionAttribute), true).SingleOrDefault();
                if (descriptionAttribute == null)
                    continue;

                property.SetValue(entity, row[descriptionAttribute.Description]);
            }
            return entity;
        }

        /// <summary>
        /// itera los valores de una lista de objetos e imprime los valores de las propiedades en un log
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="st"></param>
        /// <returns></returns>
        public static T print_r<T>(List<T> data, StackTrace st) where T : new()
        {
            foreach (var elem in data)
            {
                BL.NLogGeneratorFile.logData("/------------------------------------------------------------------------/");
                BL.NLogGeneratorFile.logData("Tipo: " + typeof(T).FullName);
                BL.NLogGeneratorFile.logData("Method: " + st.GetFrame(0).GetMethod());
                var properties = typeof(T).GetProperties();
                foreach (var property in properties)
                {
                    var value = Convert.ToString(property.GetValue(elem));
                    if (!string.IsNullOrEmpty(value))
                        BL.NLogGeneratorFile.logData("Propiedad: " + property.Name + ". Valor: " + value);
                }
            }
            return new T();
        }

        /// <summary>
        /// itera las propiedades de un objeto y las imprime en un log
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="st"></param>
        /// <returns></returns>
        public static T print_r<T>(object data, StackTrace st) where T : new()
        {
            BL.NLogGeneratorFile.logData("/------------------------------------------------------------------------/");
            BL.NLogGeneratorFile.logData("Tipo: " + typeof(T).FullName);
            BL.NLogGeneratorFile.logData("Method: " + st.GetFrame(0).GetMethod());
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var value = Convert.ToString(property.GetValue(data));
                if (!string.IsNullOrEmpty(value))
                    BL.NLogGeneratorFile.logData("Propiedad: " + property.Name + ". Valor: " + value);
            }
            return new T();
        }

        public static T print_r<T>(object data, StackTrace st, double n) where T : new()
        {
            BL.NLogGeneratorFile.logData("/------------------------------------------------------------------------/");
            BL.NLogGeneratorFile.logData("Tipo: " + typeof(T).FullName);
            BL.NLogGeneratorFile.logData("Method: " + st.GetFrame(0).GetMethod());
            BL.NLogGeneratorFile.logData("Tiempo de ejecucion del reporte: " + n);
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var value = JsonConvert.SerializeObject(property.GetValue(data));
                if (!string.IsNullOrEmpty(value))
                    BL.NLogGeneratorFile.logData("Propiedad: " + property.Name + ". Valor: " + value);
            }
            return new T();
        }

        /// <summary>
        /// valida si ya existe un registro historico
        /// </summary>
        /// <param name="aHistorico"></param>
        /// <param name="anio"></param>
        /// <returns></returns>
        public static bool boolExisteReporte(ML.Historico aHistorico, int anio)
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.DemoReporteClimaDinamico.Select(o => o).Where(o => o.Anio == anio && o.EntidadId == aHistorico.EntidadId && o.EntidadNombre == aHistorico.EntidadNombre && o.status == 1 && o.usuario == aHistorico.CurrentUsr).ToList();
                    if (query.Count > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return false;
            }
        }

        /// <summary>
        /// elimina el reporte existente para actualizarlo
        /// </summary>
        /// <param name="aHistorico"></param>
        /// <param name="anio"></param>
        public static void eliminarReporte(ML.Historico aHistorico, int anio)
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.DemoReporteClimaDinamico.Select(o => o).Where(o => o.Anio == anio && o.EntidadId == aHistorico.EntidadId && o.EntidadNombre == aHistorico.EntidadNombre && o.status == 1 && o.usuario == aHistorico.CurrentUsr).ToList();
                    if (query.Count > 0)
                    {
                        context.DemoReporteClimaDinamico.RemoveRange(query);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                BL.LogReporteoClima.writteLogJobReporte(aE, new System.Diagnostics.StackTrace());
            }
        }

        /// <summary>
        /// Obtener la unidad de negocio a la que pertenece la entidad
        /// </summary>
        /// <param name="entidadNombre"></param>
        /// <param name="entidadId"></param>
        /// <param name="tipoEntidad"></param>
        /// <returns>cadena del nombre de la unidad de negocio con su respectivo id</returns>
        public static string UnidadNegocioFromEntidad(string entidadNombre, int? entidadId, int? tipoEntidad)
        {
            string Unidad;
            switch (tipoEntidad)
            {
                case 1:
                    Unidad = getUnidadNegocioByUnidad(entidadId);
                    break;
                case 2:
                    Unidad = getUnidadNegocioByCompany(entidadId);
                    break;
                case 3:
                    Unidad = getUnidadNegocioByArea(entidadId);
                    break;
                case 4:
                    Unidad = getUnidadNegocioByDepartamento(entidadId);
                    break;
                default:
                    BL.NLogGeneratorFile.logError("Metodo UnidadNegocioFromEntidad en la clase BL.ReporteClimaDinamico no se encontró un casó coincidente", new StackTrace());
                    Unidad = "";
                    break;
            }
            return Unidad;
        }

        public static string getUnidadNegocioByUnidad(int? Unidadneg)
        {
            try
            {
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    string query = String.Format("select descripcion, idcompanycategoria from companycategoria where idcompanycategoria = {0}", Unidadneg);
                    SqlDataAdapter data = new SqlDataAdapter(query, conn);
                    data.Fill(ds, "data");

                    return Convert.ToString(ds.Tables[0].Rows[0].ItemArray[0]) + "_" + Convert.ToString(ds.Tables[0].Rows[0].ItemArray[1]);
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
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    string queryCompany = String.Format("select idcompanyCategoria from company where companyid = {0}", CompanyId);
                    SqlDataAdapter data = new SqlDataAdapter(queryCompany, conn);
                    data.Fill(ds, "data");

                    return getUnidadNegocioByUnidad(Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0]));
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
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    string queryArea = String.Format("select companyid from area where idarea = {0}", IdArea);
                    SqlDataAdapter data = new SqlDataAdapter(queryArea, conn);
                    data.Fill(ds, "data");

                    return getUnidadNegocioByCompany(Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0]));
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
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    string queryDpto = String.Format("select * from Departamento where iddepartamento = {0}", IdDepartamento);
                    SqlDataAdapter data = new SqlDataAdapter(queryDpto, conn);
                    data.Fill(ds, "data");

                    return getUnidadNegocioByArea(Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0]));
                }
            }
            catch (Exception aE)
            {
                BL.LogReporteoClima.writteLogJobReporte(aE, new StackTrace());
                return aE.Message;
            }
        }

        public static int getIdTipoEntidadByPrefijo(string aPrefijo)
        {
            try
            {
                switch (aPrefijo.ToLower())
                {
                    case "uneg=>":
                        return 1;
                    case "comp=>":
                        return 2;
                    case "area=>":
                        return 3;
                    case "dpto=>":
                        return 4;
                    case "subd=>":
                        return 5;
                    default:
                        BL.NLogGeneratorFile.logError("Metodo getIdTipoEntidadByPrefijo, no se encontro un caso coincidente", new StackTrace());
                        return 0;
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return 0;
            }
        }
        public static string getPrefijoByIdTipoEntidad(int aIdTipoEntidad)
        {
            try
            {
                switch (aIdTipoEntidad)
                {
                    case 1:
                        return "UNeg=>";
                    case 2:
                        return "Comp=>";
                    case 3:
                        return "Area=>";
                    case 4:
                        return "Dpto=>";
                    case 5:
                        return "Subd=>";
                    default:
                        BL.NLogGeneratorFile.logError("Metodo getPrefijoByIdTipoEntidad, no se encontro un caso coincidente", new StackTrace());
                        return string.Empty;
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return string.Empty;
            }
        }

        public static List<string> getListAntiguedad(int aIdEncuesta)
        {
            try
            {
                var list = new List<string>();
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    string query = String.Format("select * from antiguedad");
                    SqlDataAdapter data = new SqlDataAdapter(query, conn);
                    data.Fill(ds, "data");
                }
                foreach (DataRow row in ds.Tables[0].Rows)
                    list.Add(Convert.ToString(row.ItemArray[1]));
                return list;
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return new List<string>();
            }
        }
        public static List<string> getListGenero(int aIdEncuesta)
        {
            try
            {
                var list = new List<string>();
                list.Add("Masculino");
                list.Add("Femenino");
                return list;
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return new List<string>();
            }
        }
        public static List<string> getListGradoAcademico(int aIdEncuesta)
        {
            try
            {
                var list = new List<string>();
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    string query = String.Format("select * from GradoAcademico");
                    SqlDataAdapter data = new SqlDataAdapter(query, conn);
                    data.Fill(ds, "data");
                }
                foreach (DataRow row in ds.Tables[0].Rows)
                    list.Add(Convert.ToString(row.ItemArray[1]));
                return list;
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return new List<string>();
            }
        }
        public static List<string> getListCondicionTrabajo(int aIdEncuesta)
        {
            try
            {
                var list = new List<string>();
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    string query = String.Format("select * from CondicionTrabajo");
                    SqlDataAdapter data = new SqlDataAdapter(query, conn);
                    data.Fill(ds, "data");
                }
                foreach (DataRow row in ds.Tables[0].Rows)
                    list.Add(Convert.ToString(row.ItemArray[1]));
                return list;
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return new List<string>();
            }
        }
        public static List<string> getListPerfil(int aIdEncuesta)
        {
            try
            {
                var list = new List<string>();
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    string query = String.Format("select * from Perfil");
                    SqlDataAdapter data = new SqlDataAdapter(query, conn);
                    data.Fill(ds, "data");
                }
                foreach (DataRow row in ds.Tables[0].Rows)
                    list.Add(Convert.ToString(row.ItemArray[1]));
                return list;
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return new List<string>();
            }
        }
        public static List<string> getListEdad(int aIdEncuesta)
        {
            try
            {
                var list = new List<string>();
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    string query = String.Format("select * from RangoEdad");
                    SqlDataAdapter data = new SqlDataAdapter(query, conn);
                    data.Fill(ds, "data");
                }
                foreach (DataRow row in ds.Tables[0].Rows)
                    list.Add(Convert.ToString(row.ItemArray[1]));
                return list;
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return new List<string>();
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
                    var demo = new DL.DemoReporteClimaDinamico { EntidadId = entidadId, EntidadNombre = entidadNombre, jsonString = jsonEE, Anio = AnioActual, objName = jsonData.ContentType, status = 0, usuario = usuario };
                    var query = context.DemoReporteClimaDinamico.Add(demo);
                    context.SaveChanges();
                    BL.LogReporteoClima.writteLogJobReporte(jsonEE, new StackTrace(), usuario, 0, entidadNombre);
                    var getJson = JsonConvert.DeserializeObject<JsonResult>(jsonEE);
                    return "Success";
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return aE.Message;
            }
        }
        public static string addJsonReporte(int? entidadId, string entidadNombre, object data, int AnioActual, string objname, string usuario, int enfoque)
        {
            var jsonData = (JsonResult)data;
            var jsonEE = JsonConvert.SerializeObject(jsonData);
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    objname = enfoque == 1 ? objname + "EE" : objname + "EA";
                    var demo = new DL.DemoReporteClimaDinamico { EntidadId = entidadId, EntidadNombre = entidadNombre, jsonString = jsonEE, Anio = AnioActual, objName = objname, status = 0, usuario = usuario };
                    demo.FechaHoraCreacion = DateTime.Now; demo.UsuarioCreacion = usuario; demo.ProgramaCreacion = "JobCreacionReporte";
                    var query = context.DemoReporteClimaDinamico.Add(demo);
                    context.SaveChanges();
                    BL.LogReporteoClima.writteLogJobReporte(jsonEE, new StackTrace(), usuario, 0, entidadNombre);
                    var getJson = JsonConvert.DeserializeObject<JsonResult>(jsonEE);
                    return "Success";
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return aE.Message;
            }
        }
        public static bool updateStatusReporte(int entidadId, string entidadNombre, int AnioActual, string user)
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    context.Database.ExecuteSqlCommand("UPDATE DemoReporteClimaDinamico SET STATUS = 1 WHERE ENTIDADID = {0} AND ENTIDADNOMBRE = {1} AND ANIO = {2} AND USUARIO = {3}", entidadId, entidadNombre, AnioActual, user);
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
        public static string sendMail(string entidadName, int entidadId, int Anio, string UsuarioSolicita, string url)
        {
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
                var query = context.DemoReporteClimaDinamico.Select(o => o).Where(o => o.Anio == aHistorico.Anio && o.EntidadId == aHistorico.EntidadId && o.EntidadNombre == aHistorico.EntidadNombre && o.objName == aliasObj && o.usuario == aHistorico.CurrentUsr).FirstOrDefault();
                if (query != null)
                {
                    return (query.jsonString);
                }
            }
            return String.Empty;
        }
    }
}
