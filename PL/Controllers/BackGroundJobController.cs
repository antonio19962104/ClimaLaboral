﻿using Aspose.Pdf;
using Hangfire;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
//Nuevas
//using DVAInterfaces.SmartITWeb;
/*using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.PlatformAbstractions;*/

namespace PL.Controllers
{
    public class BackGroundJobController : Controller
    {
        #region props
        public ApisController apis = new ApisController();
        public JsonResult objProcesosOrg { get; set; }
        private int AnioActual { get; set; } = 0;
        private int AnioHistorico { get; set; } = 0;
        private bool hasHistorico { get; set; } = false;
        private object historicoEE { get; set; } = new object();
        private object historicoEA { get; set; } = new object();
        private JsonResult objEsperadas { get; set; } = new JsonResult();
        private List<int> Esperadas { get; set; } = new List<int>();
        private JsonResult objParticipacion { get; set; } = new JsonResult();
        private List<double> participacion { get; set; } = new List<double>();
        private JsonResult objCalificacionGlobal { get; set; } = new JsonResult();
        private JsonResult objConfianza { get; set; } = new JsonResult();
        private JsonResult objNivelCompromiso { get; set; } = new JsonResult();
        private JsonResult objNivelColaboracion { get; set; } = new JsonResult();
        private JsonResult objCredibilidad { get; set; } = new JsonResult();
        private JsonResult objImparcialidad { get; set; } = new JsonResult();
        private JsonResult objOrgullo { get; set; } = new JsonResult();
        private JsonResult objRespeto { get; set; } = new JsonResult();
        private JsonResult objCompanierismo { get; set; } = new JsonResult();
        public JsonResult objCoaching { get; set; } = new JsonResult();
        public JsonResult objHabgerenciales { get; set; } = new JsonResult();
        public JsonResult objAlineacionEstrategica { get; set; } = new JsonResult();
        public JsonResult objPracticasCulturales { get; set; } = new JsonResult();
        public JsonResult objCambio { get; set; } = new JsonResult();
        public JsonResult objMejoresEE { get; set; } = new JsonResult();
        public JsonResult objMejoresEA { get; set; } = new JsonResult();
        public JsonResult objMayorCrecimientoEE { get; set; } = new JsonResult();
        public JsonResult objMayorCrecimientoEA { get; set; } = new JsonResult();
        public JsonResult objpeoresEE { get; set; } = new JsonResult();
        public JsonResult objpeoresEA { get; set; } = new JsonResult();
        public JsonResult objBienestarEE { get; set; } = new JsonResult();
        public JsonResult objBienestarEA { get; set; } = new JsonResult();
        public JsonResult objPermanencia { get; set; } = new JsonResult();
        public JsonResult objAbandono { get; set; } = new JsonResult();
        public JsonResult objComparativoPermanencia { get; set; } = new JsonResult();
        public JsonResult objComparativoAbandono { get; set; } = new JsonResult();
        public JsonResult objComparativoEntidadesResultadoGeneralEE { get; set; } = new JsonResult();
        public JsonResult objComparativoEntidadesResultadoGeneralEA { get; set; } = new JsonResult();
        public JsonResult objComparativoResultadoGeneralPorNivelesEE { get; set; } = new JsonResult();
        public JsonResult objComparativoResultadoGeneralPorNivelesEA { get; set; } = new JsonResult();
        public JsonResult objComparativoPorAntiguedadEE { get; set; } = new JsonResult();
        public JsonResult objComparativoPorAntiguedadEA { get; set; } = new JsonResult();
        public JsonResult objComparativoPorGeneroEE { get; set; } = new JsonResult();
        public JsonResult objComparativoPorGeneroEA { get; set; } = new JsonResult();
        public JsonResult objComparativoPorGradoAcademicoEE { get; set; } = new JsonResult();
        public JsonResult objComparativoPorGradoAcademicoEA { get; set; } = new JsonResult();
        public JsonResult objComparativoPorCondicionTrabajoEE { get; set; } = new JsonResult();
        public JsonResult objComparativoPorCondicionTrabajoEA { get; set; } = new JsonResult();
        public JsonResult objComparativoPorFuncionEE { get; set; } = new JsonResult();
        public JsonResult objComparativoPorFuncionEA { get; set; } = new JsonResult();
        public JsonResult objComparativoPorRangoEdadEE { get; set; } = new JsonResult();
        public JsonResult objComparativoPorRangoEdadEA { get; set; } = new JsonResult();
        public JsonResult objNube1 { get; set; } = new JsonResult();
        public JsonResult objNube2 { get; set; } = new JsonResult();
        public JsonResult objNube3 { get; set; } = new JsonResult();
        public JsonResult objNube4 { get; set; } = new JsonResult();
        #endregion props
        public ActionResult execute(ML.Historico aHistorico)
        {
            HttpSessionStateBase Session = this.Session;
            BackgroundJob.Enqueue(() => BackgroundJobCreateReport(aHistorico));
            return Json("success");
        }
        public ActionResult executeGeneral(ML.Historico aHistorico)
        {
            HttpSessionStateBase Session = this.Session;
            BackgroundJob.Enqueue(() => BackgroundJobCreateReportNivelGAFM(aHistorico));
            return Json("success");
        }
        public static string getUnidadNegocio(string entidad, int idTipoEntidad, int IdBD)
        {
            string query = string.Empty;
            switch (idTipoEntidad)
            {
                case 1:
                    query = string.Format("select distinct UnidadNegocio from Empleado where IdBaseDeDatos = {0} and DivisionMarca = '{1}'", IdBD, entidad);
                    return entidad;
                    break;
                case 2:
                    query = string.Format("select distinct UnidadNegocio from Empleado where IdBaseDeDatos = {0} and DivisionMarca = '{1}'", IdBD, entidad);
                    break;
                case 3:
                    query = string.Format("select distinct UnidadNegocio from Empleado where IdBaseDeDatos = {0} and AreaAgencia = '{1}'", IdBD, entidad);
                    break;
                case 4:
                    query = string.Format("select distinct UnidadNegocio from Empleado where IdBaseDeDatos = {0} and Depto = '{1}'", IdBD, entidad);
                    break;
                case 5:
                    query = string.Format("select distinct UnidadNegocio from Empleado where IdBaseDeDatos = {0} and Subdepartamento = '{1}'", IdBD, entidad);
                    break;
                default:
                    break;
            }
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
            {
                DataSet ds = new DataSet();
                SqlDataAdapter data = new SqlDataAdapter(query, conn);
                data.Fill(ds, "data");
                var unidad = "";
                try
                {
                    unidad = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                }
                catch (Exception)
                {
                    unidad = "";
                }
                return unidad;
            }
        }
        /// <summary>
        /// Genera el reporte
        /// </summary>
        /// <param name="aHistorico"></param>
        /// <returns></returns>
        public ActionResult BackgroundJobCreateReport(ML.Historico aHistorico)
        {
            try
            {
                Console.Write(aHistorico.IdBaseDeDatos);
                #region validar
                BL.LogReporteoClima.writteLogJobReporte("Entré a generar el reporte", new StackTrace(), aHistorico.CurrentUsr, aHistorico.IdTipoEntidad, aHistorico.EntidadNombre);
                AnioActual = (int)aHistorico.Anio + 1;
                AnioHistorico = (int)aHistorico.Anio;
                string criterioBusquedaSeleccionado = Convert.ToString(aHistorico.IdTipoEntidad);
                string unidadNeg = getUnidadNegocio(aHistorico.EntidadNombre, (int)aHistorico.IdTipoEntidad, aHistorico.IdBaseDeDatos);
                if (boolExisteReporte(aHistorico, AnioActual, aHistorico.nivelDetalle, aHistorico.IdBaseDeDatos) == true)
                {
                    eliminarReporte(aHistorico, AnioActual, aHistorico.nivelDetalle, aHistorico.IdBaseDeDatos);
                }
                #endregion validar
                #region calcular data

                if (criterioBusquedaSeleccionado == "1")
                {
                    /* Se debe generar el reporte a nivel GAFM * El de la Uniad de negocio actual se saca de todos estos calculos * El de las otras unidades de negocio se sacaria de la tabla de historicos tomando el mismo año que se esta consultando en AnioActual*/
                }

                // obtener los hijos de la estructura GAFM todos los niveles bajo de la entidad seleccionada
                // var aFiltrosHijosEstructura = getHijosEstructura(idUneg, aHistorico.IdTipoEntidad, aHistorico.EntidadId);
                var aFiltrosHijosEstructura = getEstructuraFromExcel((int)aHistorico.IdTipoEntidad, aHistorico.IdBaseDeDatos, aHistorico.EntidadNombre);
                var descendientesForBienestar = aFiltrosHijosEstructura;
                /*
                 * Ahora bienestar traerá toda la info de todos los hijos y nietos etc del padre
                 * jamurillo 23/09/2021
                 * Adjuntar el campo type para saber su prefijo, sobrecargar el metodo
                 */
                //Ajustar array de entidades segun el nivel de detalle
                if (!aHistorico.nivelDetalle.Contains("1"))
                    descendientesForBienestar = descendientesForBienestar.Where(o => o.type != "UNeg=>").ToList();
                if (!aHistorico.nivelDetalle.Contains("2"))
                    descendientesForBienestar = descendientesForBienestar.Where(o => o.type != "Comp=>").ToList();
                if (!aHistorico.nivelDetalle.Contains("3"))
                    descendientesForBienestar = descendientesForBienestar.Where(o => o.type != "Area=>").ToList();
                if (!aHistorico.nivelDetalle.Contains("4"))
                    descendientesForBienestar = descendientesForBienestar.Where(o => o.type != "Dpto=>").ToList();
                if (!aHistorico.nivelDetalle.Contains("5"))
                    descendientesForBienestar = descendientesForBienestar.Where(o => o.type != "SubD=>").ToList();


                var estructuraReporteNivelDetalleBasico = aFiltrosHijosEstructura;
                var auxNivelDetalle = aHistorico.nivelDetalle.ElementAt(0).ToString() + aHistorico.nivelDetalle.ElementAt(1).ToString();
                if (!auxNivelDetalle.Contains("1"))
                    estructuraReporteNivelDetalleBasico = estructuraReporteNivelDetalleBasico.Where(o => o.type != "UNeg=>").ToList();
                if (!auxNivelDetalle.Contains("2"))
                    estructuraReporteNivelDetalleBasico = estructuraReporteNivelDetalleBasico.Where(o => o.type != "Comp=>").ToList();
                if (!auxNivelDetalle.Contains("3"))
                    estructuraReporteNivelDetalleBasico = estructuraReporteNivelDetalleBasico.Where(o => o.type != "Area=>").ToList();
                if (!auxNivelDetalle.Contains("4"))
                    estructuraReporteNivelDetalleBasico = estructuraReporteNivelDetalleBasico.Where(o => o.type != "Dpto=>").ToList();
                if (!auxNivelDetalle.Contains("5"))
                    estructuraReporteNivelDetalleBasico = estructuraReporteNivelDetalleBasico.Where(o => o.type != "SubD=>").ToList();


                //descendientesForBienestar Son los hijos segun todo el nivel de detalle que se elige y se usa para las graficas de barras
                //estructuraReporteNivelDetalleBasico Son unicamente los hijos al nivel de detalle por default y se usa para grafico de bienestar y indicadores generales y los otros 2

                // descendientesForBienestar en base a este array de estructura obtener los datos de las primeras 3 pantallas
                objParticipacion = apis.getParticipacion_(criterioBusquedaSeleccionado, estructuraReporteNivelDetalleBasico, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                objEsperadas = apis.getEncuestasEsperadas_(criterioBusquedaSeleccionado, estructuraReporteNivelDetalleBasico, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                objCalificacionGlobal = apis.getCalificacionGlobal_(criterioBusquedaSeleccionado, estructuraReporteNivelDetalleBasico, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                objConfianza = apis.getConfianza_(criterioBusquedaSeleccionado, estructuraReporteNivelDetalleBasico, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                objNivelCompromiso = apis.getNivelCompromiso_(criterioBusquedaSeleccionado, estructuraReporteNivelDetalleBasico, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                objNivelColaboracion = apis.getNivelColaboracion_(criterioBusquedaSeleccionado, estructuraReporteNivelDetalleBasico, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                objCredibilidad = apis.getCredibilidad_(criterioBusquedaSeleccionado, estructuraReporteNivelDetalleBasico, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                objImparcialidad = apis.getImparcialidad_(criterioBusquedaSeleccionado, estructuraReporteNivelDetalleBasico, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                objOrgullo = apis.getOrgullo_(criterioBusquedaSeleccionado, estructuraReporteNivelDetalleBasico, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                objRespeto = apis.getRespeto_(criterioBusquedaSeleccionado, estructuraReporteNivelDetalleBasico, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                objCompanierismo = apis.getCompanierismo_(criterioBusquedaSeleccionado, estructuraReporteNivelDetalleBasico, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                objCoaching = apis.getCoaching_(criterioBusquedaSeleccionado, estructuraReporteNivelDetalleBasico, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                objHabgerenciales = apis.getHabGerenciales_(criterioBusquedaSeleccionado, estructuraReporteNivelDetalleBasico, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                objAlineacionEstrategica = apis.getAlineacionEstrategica_(criterioBusquedaSeleccionado, estructuraReporteNivelDetalleBasico, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                objPracticasCulturales = apis.getPracticasCulturales_(criterioBusquedaSeleccionado, estructuraReporteNivelDetalleBasico, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                objCambio = apis.getCambio_(criterioBusquedaSeleccionado, estructuraReporteNivelDetalleBasico, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                objProcesosOrg = apis.getProcesosOrga_(criterioBusquedaSeleccionado, estructuraReporteNivelDetalleBasico, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);



                objMejoresEE = apis.getReactivosMejorClasificadosEE(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                objMejoresEA = apis.getReactivosMejorClasificadosEA(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                objMayorCrecimientoEE = apis.getReactivosMayorCrecimietoEE(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, Convert.ToString(aHistorico.EntidadId), AnioActual, aHistorico.IdBaseDeDatos);
                objMayorCrecimientoEA = apis.getReactivosMayorCrecimietoEA(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, Convert.ToString(aHistorico.EntidadId), AnioActual, aHistorico.IdBaseDeDatos);
                objpeoresEE = apis.getReactivosPeorClasificadosEE(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                objpeoresEA = apis.getReactivosPeorClasificadosEA(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                // obtener los hijos de la estructura GAFM un nivel bajo de la entidad seleccionada
                // var aFiltrosEntUnNivelAbajo = getFiltros(idUneg, aHistorico.IdTipoEntidad, aHistorico.EntidadId, aHistorico.EntidadNombre);
                var aFiltrosEntUnNivelAbajo = getEstructuraUnNivelFromExcel((int)aHistorico.IdTipoEntidad, aHistorico.IdBaseDeDatos, aHistorico.EntidadNombre);
                if (aFiltrosEntUnNivelAbajo.Count > 0)
                    aFiltrosEntUnNivelAbajo.RemoveAt(0);
                /*
                 * Bienestar solamente traia los elementos de un nivel abajo del padre
                 * jamurillo 23/09/2021
                 * objBienestarEE = apis.getPorcentajePsicoSocialEE(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, aFiltrosEntUnNivelAbajo, AnioActual, aHistorico.IdBaseDeDatos);//ok
                 * objBienestarEA = apis.getPorcentajePsicoSocialEA(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, aFiltrosEntUnNivelAbajo, AnioActual, aHistorico.IdBaseDeDatos);//ok usan el mismo metodo que EE
                 */
                objPermanencia = apis.getIndicadoresPermanencia(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);//ajustar query
                objAbandono = apis.getIndicadoresAbandono(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);//ajustar query
                objComparativoPermanencia = apis.getComparativoPermanencia(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, aFiltrosEntUnNivelAbajo, AnioActual, aHistorico.IdBaseDeDatos);//ajustar query
                objComparativoAbandono = apis.getComparativoAbandono(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, aFiltrosEntUnNivelAbajo, AnioActual, aHistorico.IdBaseDeDatos);//ajustar query


                //Este ya no usa filtros un nivel abajo porque esto puede cambiar a otro tipo de nivel de detalle no consecutivo
                var hijos = descendientesForBienestar;
                objComparativoEntidadesResultadoGeneralEE = apis.getComparativoEntidadesResultadoGeneralEE(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, descendientesForBienestar, AnioActual, aHistorico.IdBaseDeDatos);//ok
                objComparativoEntidadesResultadoGeneralEA = apis.getComparativoEntidadesResultadoGeneralEA(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, descendientesForBienestar, AnioActual, aHistorico.IdBaseDeDatos);//ok

                

                objBienestarEE = apis.getPorcentajePsicoSocialEE(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, estructuraReporteNivelDetalleBasico, AnioActual, aHistorico.IdBaseDeDatos);//ok
                objBienestarEA = apis.getPorcentajePsicoSocialEA(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, estructuraReporteNivelDetalleBasico, AnioActual, aHistorico.IdBaseDeDatos);//ok usan el mismo metodo que EE

                objComparativoResultadoGeneralPorNivelesEE = apis.getComparativoResultadoGeneralPorNivelesEE(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, aFiltrosHijosEstructura, AnioActual, aHistorico.IdBaseDeDatos);//ok
                objComparativoResultadoGeneralPorNivelesEA = apis.getComparativoResultadoGeneralPorNivelesEA(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, aFiltrosHijosEstructura, AnioActual, aHistorico.IdBaseDeDatos);//ok


                //Recalcular segun los demograficos encontrados en el excel de la Base de datos
                objComparativoPorAntiguedadEE = apis.getComparativoPorAntiguedadEE(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, AnioActual, aHistorico.IdBaseDeDatos);
                objComparativoPorAntiguedadEA = apis.getComparativoPorAntiguedadEA(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, AnioActual, aHistorico.IdBaseDeDatos);
                objComparativoPorGeneroEE = apis.getComparativoPorGeneroEE(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, AnioActual, aHistorico.IdBaseDeDatos);
                objComparativoPorGeneroEA = apis.getComparativoPorGeneroEA(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, AnioActual, aHistorico.IdBaseDeDatos);
                objComparativoPorGradoAcademicoEE = apis.getComparativoPorGradoAcademicoEE(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, AnioActual, aHistorico.IdBaseDeDatos);
                objComparativoPorGradoAcademicoEA = apis.getComparativoPorGradoAcademicoEA(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, AnioActual, aHistorico.IdBaseDeDatos);
                objComparativoPorCondicionTrabajoEE = apis.getComparativoPorCondicionTrabajoEE(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, AnioActual, aHistorico.IdBaseDeDatos);
                objComparativoPorCondicionTrabajoEA = apis.getComparativoPorCondicionTrabajoEA(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, AnioActual, aHistorico.IdBaseDeDatos);
                objComparativoPorFuncionEE = apis.getComparativoPorFuncionEE(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, AnioActual, aHistorico.IdBaseDeDatos);
                objComparativoPorFuncionEA = apis.getComparativoPorFuncionEA(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, AnioActual, aHistorico.IdBaseDeDatos);
                objComparativoPorRangoEdadEE = apis.getComparativoPorRangoEdadEE(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, AnioActual, aHistorico.IdBaseDeDatos);
                objComparativoPorRangoEdadEA = apis.getComparativoPorRangoEdadEA(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, AnioActual, aHistorico.IdBaseDeDatos);
                objNube1 = apis.getDatosNube(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, "1", AnioActual, aHistorico.IdBaseDeDatos);
                objNube2 = apis.getDatosNube(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, "2", AnioActual, aHistorico.IdBaseDeDatos);
                objNube3 = apis.getDatosNube(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, "3", AnioActual, aHistorico.IdBaseDeDatos);
                objNube4 = apis.getDatosNube(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, "4", AnioActual, aHistorico.IdBaseDeDatos);
                #endregion #region calcular data

                #region Asignar alias al objeto
                objCalificacionGlobal.ContentType = "dataCalificacionGlobal";
                objConfianza.ContentType = "dataConfianza";
                objNivelCompromiso.ContentType = "dataNivelCompromiso";
                objNivelColaboracion.ContentType = "dataNivelColaboracion";
                objCredibilidad.ContentType = "dataCredibilidad";
                objImparcialidad.ContentType = "dataImparcialidad";
                objOrgullo.ContentType = "dataOrgullo";
                objRespeto.ContentType = "dataRespeto";
                objCompanierismo.ContentType = "dataCompanierismo";
                objCoaching.ContentType = "dataCoaching";
                objHabgerenciales.ContentType = "dataHabGerenciales";
                objAlineacionEstrategica.ContentType = "dataAlineacionEstrategica";
                objPracticasCulturales.ContentType = "dataPracticasCulturales";
                objCambio.ContentType = "dataCambio";
                objProcesosOrg.ContentType = "dataProcesosOrg";
                objMejoresEE.ContentType = "dataMejoresEE";
                objMejoresEA.ContentType = "dataMejoresEA";
                objMayorCrecimientoEE.ContentType = "dataCrecimientoEE";
                objMayorCrecimientoEA.ContentType = "dataCrecimientoEA";
                objpeoresEE.ContentType = "dataPeoresEE";
                objpeoresEA.ContentType = "dataPeoresEA";
                objBienestarEE.ContentType = "dataBienestarEE";
                objBienestarEA.ContentType = "dataBienestarEA";
                objPermanencia.ContentType = "dataPermanencia";
                objAbandono.ContentType = "dataAbandono";
                objComparativoPermanencia.ContentType = "dataComparativoPermanencia";
                objComparativoAbandono.ContentType = "dataComparativoAbandono";
                objComparativoEntidadesResultadoGeneralEE.ContentType = "dataComparativoEntidadesResultadoGeneralEE";
                objComparativoEntidadesResultadoGeneralEA.ContentType = "dataComparativoEntidadesResultadoGeneralEA";
                objComparativoResultadoGeneralPorNivelesEE.ContentType = "dataComparativoNivelesEE";
                objComparativoResultadoGeneralPorNivelesEA.ContentType = "dataComparativoNivelesEA";
                objComparativoPorAntiguedadEE.ContentType = "dataComparativoPorAntiguedadEE";
                objComparativoPorAntiguedadEA.ContentType = "dataComparativoPorAntiguedadEA";
                objComparativoPorGeneroEE.ContentType = "dataGeneroEE";
                objComparativoPorGeneroEA.ContentType = "dataGeneroEA";
                objComparativoPorGradoAcademicoEE.ContentType = "dataAcademicoEE";
                objComparativoPorGradoAcademicoEA.ContentType = "dataAcademicoEA";
                objComparativoPorCondicionTrabajoEE.ContentType = "dataCondicionTraEE";
                objComparativoPorCondicionTrabajoEA.ContentType = "dataCondicionTraEA";
                objComparativoPorFuncionEE.ContentType = "dataFuncionEE";
                objComparativoPorFuncionEA.ContentType = "dataFuncionEA";
                objComparativoPorRangoEdadEE.ContentType = "dataEdadEE";
                objComparativoPorRangoEdadEA.ContentType = "dataEdadEA";
                objNube1.ContentType = "dataNube1";
                objNube2.ContentType = "dataNube2";
                objNube3.ContentType = "dataNube3";
                objNube4.ContentType = "dataNube4";
                #endregion Asignar alias al objeto
                var list = new List<JsonResult>();
                #region Guardar resultados
                //guradar cadena json 1
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objEsperadas.Data, AnioActual, "dataEsperadas", aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objParticipacion.Data, AnioActual, "dataParticipacion", aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objCalificacionGlobal, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objConfianza, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objNivelCompromiso, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objNivelColaboracion, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                //guardar cadena json 2
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objCredibilidad, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objImparcialidad, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objOrgullo, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objRespeto, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objCompanierismo, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                //guardar 3
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objCoaching, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objHabgerenciales, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objAlineacionEstrategica, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objPracticasCulturales, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objCambio, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objProcesosOrg, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                //
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objMejoresEE, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objMejoresEA, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                //
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objMayorCrecimientoEE, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objMayorCrecimientoEA, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                //
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objpeoresEE, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objpeoresEA, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                //
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objBienestarEE, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objBienestarEA, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                //
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objPermanencia, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objAbandono, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                //
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objComparativoPermanencia, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objComparativoAbandono, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                //
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objComparativoEntidadesResultadoGeneralEE, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objComparativoEntidadesResultadoGeneralEA, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                //
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objComparativoResultadoGeneralPorNivelesEE, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objComparativoResultadoGeneralPorNivelesEA, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                //
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objComparativoPorAntiguedadEE, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objComparativoPorAntiguedadEA, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                //
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objComparativoPorGeneroEE, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objComparativoPorGeneroEA, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                //
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objComparativoPorGradoAcademicoEE, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objComparativoPorGradoAcademicoEA, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                //
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objComparativoPorCondicionTrabajoEE, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objComparativoPorCondicionTrabajoEA, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                //
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objComparativoPorFuncionEE, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objComparativoPorFuncionEA, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                //
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objComparativoPorRangoEdadEE, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objComparativoPorRangoEdadEA, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                //
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objNube1, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objNube2, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objNube3, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objNube4, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);

                #endregion guardar resultados
                //Pasar estatus del JobReporte a 1 y notificar
                BL.LogReporteoClima.updateStatusReporte(aHistorico.EntidadNombre, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle, aHistorico.IdBaseDeDatos);
                BL.LogReporteoClima.sendMail(aHistorico.EntidadNombre, AnioActual, aHistorico.CurrentUsr, aHistorico.currentURL, aHistorico.ps, aHistorico.nivelDetalle, aHistorico.opc, criterioBusquedaSeleccionado, aHistorico.enfoqueSeleccionado, aHistorico.nivelDetalle, aHistorico.IdBaseDeDatos);
                BL.LogReporteoClima.writteLogJobReporte("He terminado de generar el reporte", new System.Diagnostics.StackTrace(), aHistorico.CurrentUsr, aHistorico.IdTipoEntidad, aHistorico.EntidadNombre);
                return Json(true);
            }
            catch (Exception aE)
            {
                BL.LogReporteoClima.writteLogJobReporte(aE, new System.Diagnostics.StackTrace());
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return Json(aE.Message);
            }
        }


        /// <summary>
        /// Genera el reporte grafico tomando el nivel grupo autofin como padre
        /// </summary>
        /// <param name="aHistorico"></param>
        /// <param name="IdBD"></param>
        /// <returns></returns>
        public ActionResult BackgroundJobCreateReportNivelGAFM(ML.Historico aHistorico)
        {
            try
            {
                Console.Write(aHistorico.IdBaseDeDatos);
                #region validar
                BL.LogReporteoClima.writteLogJobReporte("Entré a generar el reporte", new StackTrace(), aHistorico.CurrentUsr, aHistorico.IdTipoEntidad, aHistorico.EntidadNombre);
                AnioActual = (int)aHistorico.Anio + 1;
                AnioHistorico = (int)aHistorico.Anio;
                string criterioBusquedaSeleccionado = Convert.ToString(aHistorico.IdTipoEntidad);
                string unidadNeg = "";//getUnidadNegocio(aHistorico.EntidadNombre, (int)aHistorico.IdTipoEntidad, aHistorico.IdBaseDeDatos);
                if (boolExisteReporte(aHistorico, AnioActual, aHistorico.nivelDetalle, aHistorico.IdBaseDeDatos) == true)
                {
                    eliminarReporte(aHistorico, AnioActual, aHistorico.nivelDetalle, aHistorico.IdBaseDeDatos);
                }
                #endregion validar
                #region calcular data
                var aFiltrosHijosEstructura = getEstructuraFromExcel((int)aHistorico.IdTipoEntidad, aHistorico.IdBaseDeDatos, aHistorico.EntidadNombre);
                var descendientesForBienestar = aFiltrosHijosEstructura;
                if (!aHistorico.nivelDetalle.Contains("1"))
                    descendientesForBienestar = descendientesForBienestar.Where(o => o.type != "UNeg=>").ToList();
                if (!aHistorico.nivelDetalle.Contains("2"))
                    descendientesForBienestar = descendientesForBienestar.Where(o => o.type != "Comp=>").ToList();
                if (!aHistorico.nivelDetalle.Contains("3"))
                    descendientesForBienestar = descendientesForBienestar.Where(o => o.type != "Area=>").ToList();
                if (!aHistorico.nivelDetalle.Contains("4"))
                    descendientesForBienestar = descendientesForBienestar.Where(o => o.type != "Dpto=>").ToList();
                if (!aHistorico.nivelDetalle.Contains("5"))
                    descendientesForBienestar = descendientesForBienestar.Where(o => o.type != "SubD=>").ToList();

                var estructuraReporteNivelDetalleBasico = aFiltrosHijosEstructura;
                var auxNivelDetalle = aHistorico.nivelDetalle.ElementAt(0).ToString() + aHistorico.nivelDetalle.ElementAt(1).ToString();
                if (!auxNivelDetalle.Contains("1"))
                    estructuraReporteNivelDetalleBasico = estructuraReporteNivelDetalleBasico.Where(o => o.type != "UNeg=>").ToList();
                if (!auxNivelDetalle.Contains("2"))
                    estructuraReporteNivelDetalleBasico = estructuraReporteNivelDetalleBasico.Where(o => o.type != "Comp=>").ToList();
                if (!auxNivelDetalle.Contains("3"))
                    estructuraReporteNivelDetalleBasico = estructuraReporteNivelDetalleBasico.Where(o => o.type != "Area=>").ToList();
                if (!auxNivelDetalle.Contains("4"))
                    estructuraReporteNivelDetalleBasico = estructuraReporteNivelDetalleBasico.Where(o => o.type != "Dpto=>").ToList();
                if (!auxNivelDetalle.Contains("5"))
                    estructuraReporteNivelDetalleBasico = estructuraReporteNivelDetalleBasico.Where(o => o.type != "SubD=>").ToList();

                var aFiltrosEntUnNivelAbajo = getEstructuraUnNivelFromExcel((int)aHistorico.IdTipoEntidad, aHistorico.IdBaseDeDatos, aHistorico.EntidadNombre);

                // A este bloqueya se les añadió el dato del papá (Nivel cero)
                objParticipacion = apis.getParticipacion_(criterioBusquedaSeleccionado, estructuraReporteNivelDetalleBasico, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                objEsperadas = apis.getEncuestasEsperadas_(criterioBusquedaSeleccionado, estructuraReporteNivelDetalleBasico, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                objCalificacionGlobal = apis.getCalificacionGlobal_(criterioBusquedaSeleccionado, estructuraReporteNivelDetalleBasico, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                objConfianza = apis.getConfianza_(criterioBusquedaSeleccionado, estructuraReporteNivelDetalleBasico, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                objNivelCompromiso = apis.getNivelCompromiso_(criterioBusquedaSeleccionado, estructuraReporteNivelDetalleBasico, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                objNivelColaboracion = apis.getNivelColaboracion_(criterioBusquedaSeleccionado, estructuraReporteNivelDetalleBasico, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                objCredibilidad = apis.getCredibilidad_(criterioBusquedaSeleccionado, estructuraReporteNivelDetalleBasico, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                objImparcialidad = apis.getImparcialidad_(criterioBusquedaSeleccionado, estructuraReporteNivelDetalleBasico, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                objOrgullo = apis.getOrgullo_(criterioBusquedaSeleccionado, estructuraReporteNivelDetalleBasico, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                objRespeto = apis.getRespeto_(criterioBusquedaSeleccionado, estructuraReporteNivelDetalleBasico, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                objCompanierismo = apis.getCompanierismo_(criterioBusquedaSeleccionado, estructuraReporteNivelDetalleBasico, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                objCoaching = apis.getCoaching_(criterioBusquedaSeleccionado, estructuraReporteNivelDetalleBasico, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                objHabgerenciales = apis.getHabGerenciales_(criterioBusquedaSeleccionado, estructuraReporteNivelDetalleBasico, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                objAlineacionEstrategica = apis.getAlineacionEstrategica_(criterioBusquedaSeleccionado, estructuraReporteNivelDetalleBasico, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                objPracticasCulturales = apis.getPracticasCulturales_(criterioBusquedaSeleccionado, estructuraReporteNivelDetalleBasico, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                objCambio = apis.getCambio_(criterioBusquedaSeleccionado, estructuraReporteNivelDetalleBasico, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                objProcesosOrg = apis.getProcesosOrga_(criterioBusquedaSeleccionado, estructuraReporteNivelDetalleBasico, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);


                //Faltantes
                //A este bloque le falta añadir al papa (Nivel cero)
                //Este ya no usa filtros un nivel abajo porque esto puede cambiar a otro tipo de nivel de detalle no consecutivo
                var hijos = descendientesForBienestar;
                objComparativoEntidadesResultadoGeneralEE = apis.getComparativoEntidadesResultadoGeneralEE(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, descendientesForBienestar, AnioActual, aHistorico.IdBaseDeDatos);//ok
                objComparativoEntidadesResultadoGeneralEA = apis.getComparativoEntidadesResultadoGeneralEA(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, descendientesForBienestar, AnioActual, aHistorico.IdBaseDeDatos);//ok
                objBienestarEE = apis.getPorcentajePsicoSocialEE(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, estructuraReporteNivelDetalleBasico, AnioActual, aHistorico.IdBaseDeDatos);
                objBienestarEA = apis.getPorcentajePsicoSocialEA(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, estructuraReporteNivelDetalleBasico, AnioActual, aHistorico.IdBaseDeDatos);
                objComparativoResultadoGeneralPorNivelesEE = apis.getComparativoResultadoGeneralPorNivelesEE(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, descendientesForBienestar, AnioActual, aHistorico.IdBaseDeDatos);//ok
                objComparativoResultadoGeneralPorNivelesEA = apis.getComparativoResultadoGeneralPorNivelesEA(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, descendientesForBienestar, AnioActual, aHistorico.IdBaseDeDatos);//ok
                //Hasta aqui son los faltantes de agregar papá nivel cero




                objPermanencia = apis.getIndicadoresPermanencia(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);//Solo padre OK
                objAbandono = apis.getIndicadoresAbandono(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);//Solo padre OK
                objComparativoPermanencia = apis.getComparativoPermanencia(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, aFiltrosEntUnNivelAbajo, AnioActual, aHistorico.IdBaseDeDatos);//Solo hijos OK
                objComparativoAbandono = apis.getComparativoAbandono(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, aFiltrosEntUnNivelAbajo, AnioActual, aHistorico.IdBaseDeDatos);//Solo hijos OK
                // Datos que corresponden al nivel cero especificamente, se debe crear un metodo como los de GetByUnidad, GetByGenero
                // Pero enfocado solo al Id de base de datos para ser totalmente generico
                objMejoresEE = apis.getReactivosMejorClasificadosEE(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);//OK
                objMejoresEA = apis.getReactivosMejorClasificadosEA(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);//OK
                objMayorCrecimientoEE = apis.getReactivosMayorCrecimietoEE(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, Convert.ToString(aHistorico.EntidadId), AnioActual, aHistorico.IdBaseDeDatos);//OK
                objMayorCrecimientoEA = apis.getReactivosMayorCrecimietoEA(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, Convert.ToString(aHistorico.EntidadId), AnioActual, aHistorico.IdBaseDeDatos);//OK
                objpeoresEE = apis.getReactivosPeorClasificadosEE(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                objpeoresEA = apis.getReactivosPeorClasificadosEA(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);

                //Falta agregar el metodo nivel cero
                //Recalcular segun los demograficos encontrados en el excel de la Base de datos
                objComparativoPorAntiguedadEE = apis.getComparativoPorAntiguedadEE(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, AnioActual, aHistorico.IdBaseDeDatos);
                objComparativoPorAntiguedadEA = apis.getComparativoPorAntiguedadEA(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, AnioActual, aHistorico.IdBaseDeDatos);
                objComparativoPorGeneroEE = apis.getComparativoPorGeneroEE(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, AnioActual, aHistorico.IdBaseDeDatos);
                objComparativoPorGeneroEA = apis.getComparativoPorGeneroEA(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, AnioActual, aHistorico.IdBaseDeDatos);
                objComparativoPorGradoAcademicoEE = apis.getComparativoPorGradoAcademicoEE(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, AnioActual, aHistorico.IdBaseDeDatos);
                objComparativoPorGradoAcademicoEA = apis.getComparativoPorGradoAcademicoEA(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, AnioActual, aHistorico.IdBaseDeDatos);
                objComparativoPorCondicionTrabajoEE = apis.getComparativoPorCondicionTrabajoEE(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, AnioActual, aHistorico.IdBaseDeDatos);
                objComparativoPorCondicionTrabajoEA = apis.getComparativoPorCondicionTrabajoEA(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, AnioActual, aHistorico.IdBaseDeDatos);
                objComparativoPorFuncionEE = apis.getComparativoPorFuncionEE(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, AnioActual, aHistorico.IdBaseDeDatos);
                objComparativoPorFuncionEA = apis.getComparativoPorFuncionEA(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, AnioActual, aHistorico.IdBaseDeDatos);
                objComparativoPorRangoEdadEE = apis.getComparativoPorRangoEdadEE(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, AnioActual, aHistorico.IdBaseDeDatos);
                objComparativoPorRangoEdadEA = apis.getComparativoPorRangoEdadEA(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, AnioActual, aHistorico.IdBaseDeDatos);
                #endregion #region calcular data

                #region Asignar alias al objeto
                objCalificacionGlobal.ContentType = "dataCalificacionGlobal";
                objConfianza.ContentType = "dataConfianza";
                objNivelCompromiso.ContentType = "dataNivelCompromiso";
                objNivelColaboracion.ContentType = "dataNivelColaboracion";
                objCredibilidad.ContentType = "dataCredibilidad";
                objImparcialidad.ContentType = "dataImparcialidad";
                objOrgullo.ContentType = "dataOrgullo";
                objRespeto.ContentType = "dataRespeto";
                objCompanierismo.ContentType = "dataCompanierismo";
                objCoaching.ContentType = "dataCoaching";
                objHabgerenciales.ContentType = "dataHabGerenciales";
                objAlineacionEstrategica.ContentType = "dataAlineacionEstrategica";
                objPracticasCulturales.ContentType = "dataPracticasCulturales";
                objCambio.ContentType = "dataCambio";
                objProcesosOrg.ContentType = "dataProcesosOrg";
                objMejoresEE.ContentType = "dataMejoresEE";
                objMejoresEA.ContentType = "dataMejoresEA";
                objMayorCrecimientoEE.ContentType = "dataCrecimientoEE";
                objMayorCrecimientoEA.ContentType = "dataCrecimientoEA";
                objpeoresEE.ContentType = "dataPeoresEE";
                objpeoresEA.ContentType = "dataPeoresEA";
                objBienestarEE.ContentType = "dataBienestarEE";
                objBienestarEA.ContentType = "dataBienestarEA";
                objPermanencia.ContentType = "dataPermanencia";
                objAbandono.ContentType = "dataAbandono";
                objComparativoPermanencia.ContentType = "dataComparativoPermanencia";
                objComparativoAbandono.ContentType = "dataComparativoAbandono";
                objComparativoEntidadesResultadoGeneralEE.ContentType = "dataComparativoEntidadesResultadoGeneralEE";
                objComparativoEntidadesResultadoGeneralEA.ContentType = "dataComparativoEntidadesResultadoGeneralEA";
                objComparativoResultadoGeneralPorNivelesEE.ContentType = "dataComparativoNivelesEE";
                objComparativoResultadoGeneralPorNivelesEA.ContentType = "dataComparativoNivelesEA";
                objComparativoPorAntiguedadEE.ContentType = "dataComparativoPorAntiguedadEE";
                objComparativoPorAntiguedadEA.ContentType = "dataComparativoPorAntiguedadEA";
                objComparativoPorGeneroEE.ContentType = "dataGeneroEE";
                objComparativoPorGeneroEA.ContentType = "dataGeneroEA";
                objComparativoPorGradoAcademicoEE.ContentType = "dataAcademicoEE";
                objComparativoPorGradoAcademicoEA.ContentType = "dataAcademicoEA";
                objComparativoPorCondicionTrabajoEE.ContentType = "dataCondicionTraEE";
                objComparativoPorCondicionTrabajoEA.ContentType = "dataCondicionTraEA";
                objComparativoPorFuncionEE.ContentType = "dataFuncionEE";
                objComparativoPorFuncionEA.ContentType = "dataFuncionEA";
                objComparativoPorRangoEdadEE.ContentType = "dataEdadEE";
                objComparativoPorRangoEdadEA.ContentType = "dataEdadEA";
                objNube1.ContentType = "dataNube1";
                objNube2.ContentType = "dataNube2";
                objNube3.ContentType = "dataNube3";
                objNube4.ContentType = "dataNube4";
                #endregion Asignar alias al objeto
                var list = new List<JsonResult>();
                #region Guardar resultados
                //guradar cadena json 1
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objEsperadas.Data, AnioActual, "dataEsperadas", aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objParticipacion.Data, AnioActual, "dataParticipacion", aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objCalificacionGlobal, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objConfianza, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objNivelCompromiso, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objNivelColaboracion, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                //guardar cadena json 2
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objCredibilidad, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objImparcialidad, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objOrgullo, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objRespeto, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objCompanierismo, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                //guardar 3
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objCoaching, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objHabgerenciales, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objAlineacionEstrategica, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objPracticasCulturales, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objCambio, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objProcesosOrg, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                //
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objMejoresEE, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objMejoresEA, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                //
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objMayorCrecimientoEE, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objMayorCrecimientoEA, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                //
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objpeoresEE, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objpeoresEA, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                //
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objBienestarEE, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objBienestarEA, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                //
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objPermanencia, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objAbandono, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                //
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objComparativoPermanencia, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objComparativoAbandono, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                //
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objComparativoEntidadesResultadoGeneralEE, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objComparativoEntidadesResultadoGeneralEA, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                //
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objComparativoResultadoGeneralPorNivelesEE, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objComparativoResultadoGeneralPorNivelesEA, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                //
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objComparativoPorAntiguedadEE, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objComparativoPorAntiguedadEA, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                //
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objComparativoPorGeneroEE, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objComparativoPorGeneroEA, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                //
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objComparativoPorGradoAcademicoEE, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objComparativoPorGradoAcademicoEA, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                //
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objComparativoPorCondicionTrabajoEE, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objComparativoPorCondicionTrabajoEA, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                //
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objComparativoPorFuncionEE, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objComparativoPorFuncionEA, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                //
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objComparativoPorRangoEdadEE, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objComparativoPorRangoEdadEA, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                //
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objNube1, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objNube2, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objNube3, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);
                BL.LogReporteoClima.addJsonReporte(aHistorico.EntidadId, aHistorico.EntidadNombre, objNube4, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle);

                #endregion guardar resultados
                //Pasar estatus del JobReporte a 1 y notificar
                BL.LogReporteoClima.updateStatusReporte(aHistorico.EntidadNombre, AnioActual, aHistorico.CurrentUsr, aHistorico.nivelDetalle, aHistorico.IdBaseDeDatos);
                BL.LogReporteoClima.sendMail(aHistorico.EntidadNombre, AnioActual, aHistorico.CurrentUsr, aHistorico.currentURL, aHistorico.ps, aHistorico.nivelDetalle, aHistorico.opc, criterioBusquedaSeleccionado, aHistorico.enfoqueSeleccionado, aHistorico.nivelDetalle, aHistorico.IdBaseDeDatos);
                BL.LogReporteoClima.writteLogJobReporte("He terminado de generar el reporte", new System.Diagnostics.StackTrace(), aHistorico.CurrentUsr, aHistorico.IdTipoEntidad, aHistorico.EntidadNombre);
                return Json(true);
            }
            catch (Exception aE)
            {
                BL.LogReporteoClima.writteLogJobReporte(aE, new System.Diagnostics.StackTrace());
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return Json(aE.Message);
            }
        }


        public class Model
        {
            public ML.Result Result { get; set; } = new ML.Result();
            public ML.Historico Historico { get; set; } = new ML.Historico();
        }

        [System.Web.Http.HttpPost]        
        public JsonResult GuardarOrdenReporte([FromBody] Model model)
        {
            var modelResult = model.Result;//model["Result"].ToObject<ML.Result>();
            var modelHistorico = model.Historico;//model["Historico"].ToObject<ML.Historico>();
            var result2 = BL.ReporteD4U.GuardarOrdenReporte(modelResult, modelHistorico);
            return Json(result2, JsonRequestBehavior.AllowGet);
        }

        class finalCols
        {
            public string type { get; set; }
            public string value { get; set; }
        }
        //getFiltros(idUneg, aHistorico.IdTipoEntidad, aHistorico.EntidadId, aHistorico.EntidadNombre);
        public static List<string> getFiltros(int unidadNegocio, int? criterioSeleccionado, int? entidadId, string entidadNombre)
        {
            int IentidadId = Convert.ToInt32(entidadId);
            //Trae el nivel inmediato inferior para pintar el grid de bienestar
            BackGroundJobController s = new BackGroundJobController();
            var estructura = new List<object>();
            //getEstructura(unidadNegocio);
            switch (criterioSeleccionado)
            {
                case 1:
                    estructura = GetEstructura(new ML.CompanyCategoria { IdCompanyCategoria = unidadNegocio });
                    break;
                case 2:
                    estructura = GetEstructura_lvl_2(new ML.Company { CompanyId = entidadId });
                    break;
                case 3:
                    estructura = GetEstructura_lvl_3(new ML.Area { IdArea = IentidadId });
                    break;
                case 4:
                    estructura = GetEstructura_lvl_4(new ML.Departamento { IdDepartamento = IentidadId });
                    break;
                default:
                    break;
            }



            var finalColumnas = new List<finalCols>();
            //data.Data
            foreach (ML.Company item in estructura)
            {
                if (!String.IsNullOrEmpty(item.CompanyName) && criterioSeleccionado == 1)
                {
                    finalColumnas.Add(new finalCols { type = "Comp=>", value = item.CompanyName });
                }
                if (!String.IsNullOrEmpty(item.Area.Nombre) && criterioSeleccionado == 2)
                {
                    finalColumnas.Add(new finalCols { type = "Area=>", value = item.Area.Nombre });
                }
                if (!String.IsNullOrEmpty(item.Area.Departamento.Nombre) && criterioSeleccionado == 3)
                {
                    finalColumnas.Add(new finalCols { type = "Dpto=>", value = item.Area.Departamento.Nombre });
                }
                if (!String.IsNullOrEmpty(item.Area.Departamento.Subdepartamento.Nombre) && criterioSeleccionado == 4)
                {
                    finalColumnas.Add(new finalCols { type = "SubD=>", value = item.Area.Departamento.Subdepartamento.Nombre });
                }
            }
            var ColumnasByCompanyCategoria = new List<finalCols>();


            //seccionar para solo tomar donde inicia el nombre de la entidad y hasta donde encuentra otra del tipo que use en criterio


            switch (criterioSeleccionado)
            {
                case 1:
                    ColumnasByCompanyCategoria.AddRange(finalColumnas.Select(o => o)
                    .Where(o => o.type.Contains("Comp"))
                    .OrderBy(o => o.value)
                    .ToArray());
                    break;
                case 2:
                    ColumnasByCompanyCategoria.AddRange(finalColumnas.Select(o => o)
                    .Where(o => o.type.Contains("Area"))
                    .OrderBy(o => o.value)
                    .ToArray());
                    break;
                case 3:
                    ColumnasByCompanyCategoria.AddRange(finalColumnas.Select(o => o)
                    .Where(o => o.type.Contains("Dpto"))
                    .OrderBy(o => o.value)
                    .ToArray());
                    break;
                case 4:
                    ColumnasByCompanyCategoria.AddRange(finalColumnas.Select(o => o)
                    .Where(o => o.type.Contains("SubD"))
                    .OrderBy(o => o.value)
                    .ToArray());
                    break;
                default:
                    BL.LogReporteoClima.writteLog("En la funcion getFiltros no se encontro un caso", new System.Diagnostics.StackTrace());
                    break;
            }

            var arrayStringFiltros = new List<string>();

            foreach (var item in ColumnasByCompanyCategoria)
            {
                arrayStringFiltros.Add(item.value);
            }
            return arrayStringFiltros;
        }
        //getHijosEstructura(idUneg, aHistorico.IdTipoEntidad, aHistorico.EntidadId);
        public static List<ApisController.myCustomArray> getHijosEstructura(int unidadNegocio, int? criterioSeleccionado, int? entidadId)
        {
            int IentidadId = Convert.ToInt32(entidadId);
            //Trae el nivel inmediato inferior para pintar el grid de bienestar
            BackGroundJobController s = new BackGroundJobController();
            var estructura = new List<object>();//getEstructura(unidadNegocio);
            switch (criterioSeleccionado)
            {
                case 1:
                    estructura = GetEstructura(new ML.CompanyCategoria { IdCompanyCategoria = unidadNegocio });
                    break;
                case 2:
                    estructura = GetEstructura_lvl_2(new ML.Company { CompanyId = entidadId });
                    break;
                case 3:
                    estructura = GetEstructura_lvl_3(new ML.Area { IdArea = IentidadId });
                    break;
                case 4:
                    estructura = GetEstructura_lvl_4(new ML.Departamento { IdDepartamento = IentidadId });
                    break;
                default:
                    break;
            }


            var finalColumnas = new List<ApisController.myCustomArray>();
            //data.Data
            foreach (ML.Company item in estructura)
            {
                if (!String.IsNullOrEmpty(item.CompanyName))
                {
                    finalColumnas.Add(new ApisController.myCustomArray { type = "Comp=>", value = item.CompanyName });
                }
                if (!String.IsNullOrEmpty(item.Area.Nombre))
                {
                    finalColumnas.Add(new ApisController.myCustomArray { type = "Area=>", value = item.Area.Nombre });
                }
                if (!String.IsNullOrEmpty(item.Area.Departamento.Nombre))
                {
                    finalColumnas.Add(new ApisController.myCustomArray { type = "Dpto=>", value = item.Area.Departamento.Nombre });
                }
                if (!String.IsNullOrEmpty(item.Area.Departamento.Subdepartamento.Nombre))
                {
                    finalColumnas.Add(new ApisController.myCustomArray { type = "SubD=>", value = item.Area.Departamento.Subdepartamento.Nombre });
                }
            }
            return finalColumnas;
        }

        public static List<object> getEstructura(int unidadNegocio)
        {
            var Session = HttpRuntime.Cache; // HttpContextBase;
            ML.Result result = new ML.Result();
            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                if (unidadNegocio == 9)
                {
                    var query = context.GetEstructuraByUNegocioD4U(unidadNegocio).ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        Session["s"] = "";
                        Session["Company"] = "";
                        Session["Area"] = "";
                        Session["Departamento"] = "";
                        Session["Subdepartamento"] = "";
                        foreach (var item in query)
                        {
                            ML.Company company = new ML.Company();
                            company.CompanyCategoria = new ML.CompanyCategoria();
                            company.Area = new ML.Area();
                            company.Area.Departamento = new ML.Departamento();
                            company.Area.Departamento.Subdepartamento = new ML.Subdepartamento();

                            company.CompanyCategoria.Descripcion = item.UNIDAD_NEGOCIO;
                            company.CompanyCategoria.IdCompanyCategoria = item.ID_UNEGOCIO;
                            company.CompanyId = item.ID_COMPANY;
                            company.Area.IdArea = Convert.ToInt32(item.ID_AREA);
                            company.Area.Departamento.IdDepartamento = Convert.ToInt32(item.ID_DEPARTAMENTO);
                            company.Area.Departamento.Subdepartamento.IdSubdepartamento = Convert.ToInt32(item.ID_SUBDEPARTAMENTO);

                            string Company = Convert.ToString(Session["Company"]);
                            if (Company != item.COMPANY_NAME)
                            {
                                company.CompanyName = item.COMPANY_NAME;
                                Session["Company"] = item.COMPANY_NAME;
                            }
                            else
                            {

                            }
                            //Validar area
                            string Area = Convert.ToString(Session["Area"]);
                            if (Area != item.NOMBRE_AREA)
                            {
                                company.Area.Nombre = item.NOMBRE_AREA;
                                Session["Area"] = item.NOMBRE_AREA == null ? "" : item.NOMBRE_AREA;
                            }
                            else
                            {

                            }
                            //Validar Departamento
                            string Departamento = Convert.ToString(Session["Departamento"]);
                            if (Departamento != item.NOMBRE_DEPARTAMENTO)
                            {
                                company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                Session["Departamento"] = item.NOMBRE_DEPARTAMENTO == null ? "" : item.NOMBRE_DEPARTAMENTO;
                            }
                            else
                            {

                            }
                            //Validar subdepartamento
                            string Subdepartamento = Convert.ToString(Session["Subdepartamento"]);
                            if (Subdepartamento != item.NOMBRE_SUBDEPARTAMENTO)
                            {
                                company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                Session["Subdepartamento"] = item.NOMBRE_SUBDEPARTAMENTO == null ? "" : item.NOMBRE_SUBDEPARTAMENTO;
                            }
                            else
                            {

                            }
                            result.Objects.Add(company);
                            result.Correct = true;
                        }
                    }
                }
                else
                {
                    var query = context.GetEstructuraByUNegocioD4USucces(unidadNegocio).ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        Session["Company"] = "";
                        Session["Area"] = "";
                        Session["Departamento"] = "";
                        Session["Subdepartamento"] = "";
                        foreach (var item in query)
                        {
                            ML.Company company = new ML.Company();
                            company.CompanyCategoria = new ML.CompanyCategoria();
                            company.Area = new ML.Area();
                            company.Area.Departamento = new ML.Departamento();
                            company.Area.Departamento.Subdepartamento = new ML.Subdepartamento();

                            company.CompanyCategoria.Descripcion = item.UNIDAD_NEGOCIO;
                            company.CompanyCategoria.IdCompanyCategoria = item.ID_UNEGOCIO;
                            company.CompanyId = item.ID_COMPANY;
                            company.Area.IdArea = Convert.ToInt32(item.ID_AREA);
                            company.Area.Departamento.IdDepartamento = Convert.ToInt32(item.ID_DEPARTAMENTO);
                            company.Area.Departamento.Subdepartamento.IdSubdepartamento = Convert.ToInt32(item.ID_SUBDEPARTAMENTO);

                            string Company = Convert.ToString(Session["Company"]);
                            if (Company != item.COMPANY_NAME)
                            {
                                company.CompanyName = item.COMPANY_NAME;
                                Session["Company"] = item.COMPANY_NAME;
                            }
                            else
                            {

                            }
                            //Validar area
                            string Area = Convert.ToString(Session["Area"]);
                            if (Area != item.NOMBRE_AREA)
                            {
                                company.Area.Nombre = item.NOMBRE_AREA;
                                Session["Area"] = item.NOMBRE_AREA;
                            }
                            else
                            {

                            }
                            //Validar Departamento
                            string Departamento = Convert.ToString(Session["Departamento"]);
                            if (Departamento != item.NOMBRE_DEPARTAMENTO)
                            {
                                company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                Session["Departamento"] = item.NOMBRE_DEPARTAMENTO;
                            }
                            else
                            {

                            }
                            //Validar subdepartamento
                            string Subdepartamento = Convert.ToString(Session["Subdepartamento"]);
                            if (Subdepartamento != item.NOMBRE_SUBDEPARTAMENTO)
                            {
                                company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                Session["Subdepartamento"] = item.NOMBRE_SUBDEPARTAMENTO;
                            }
                            else
                            {

                            }
                            result.Objects.Add(company);
                            result.Correct = true;
                        }
                    }
                }
            }
            return (result.Objects);
        }




        public static List<object> GetEstructura(ML.CompanyCategoria CompanyCateg)
        {
            /*Validar repetidos*/
            var Session = HttpRuntime.Cache;
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    if (CompanyCateg.IdCompanyCategoria == 9)
                    {
                        var query = context.GetEstructuraByUNegocioD4U(CompanyCateg.IdCompanyCategoria).ToList();
                        result.Objects = new List<object>();
                        if (query != null)
                        {
                            Session["s"] = "";
                            Session["Company"] = "";
                            Session["Area"] = "";
                            Session["Departamento"] = "";
                            Session["Subdepartamento"] = "";
                            foreach (var item in query)
                            {
                                ML.Company company = new ML.Company();
                                company.CompanyCategoria = new ML.CompanyCategoria();
                                company.Area = new ML.Area();
                                company.Area.Departamento = new ML.Departamento();
                                company.Area.Departamento.Subdepartamento = new ML.Subdepartamento();

                                company.CompanyCategoria.Descripcion = item.UNIDAD_NEGOCIO;
                                company.CompanyCategoria.IdCompanyCategoria = item.ID_UNEGOCIO;
                                company.CompanyId = item.ID_COMPANY;
                                company.Area.IdArea = Convert.ToInt32(item.ID_AREA);
                                company.Area.Departamento.IdDepartamento = Convert.ToInt32(item.ID_DEPARTAMENTO);
                                company.Area.Departamento.Subdepartamento.IdSubdepartamento = Convert.ToInt32(item.ID_SUBDEPARTAMENTO);

                                string Company = Convert.ToString(Session["Company"]);
                                if (Company != item.COMPANY_NAME)
                                {
                                    company.CompanyName = item.COMPANY_NAME;
                                    Session["Company"] = item.COMPANY_NAME == null ? "" : item.COMPANY_NAME;
                                }
                                else
                                {

                                }
                                //Validar area
                                string Area = Convert.ToString(Session["Area"]);
                                if (Area != item.NOMBRE_AREA)
                                {
                                    company.Area.Nombre = item.NOMBRE_AREA;
                                    Session["Area"] = item.NOMBRE_AREA == null ? "" : item.NOMBRE_AREA;
                                }
                                else
                                {

                                }
                                //Validar Departamento
                                string Departamento = Convert.ToString(Session["Departamento"]);
                                if (Departamento != item.NOMBRE_DEPARTAMENTO)
                                {
                                    company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                    Session["Departamento"] = item.NOMBRE_DEPARTAMENTO == null ? "" : item.NOMBRE_DEPARTAMENTO;
                                }
                                else
                                {

                                }
                                //Validar subdepartamento
                                string Subdepartamento = Convert.ToString(Session["Subdepartamento"]);
                                if (Subdepartamento != item.NOMBRE_SUBDEPARTAMENTO)
                                {
                                    company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                    Session["Subdepartamento"] = item.NOMBRE_SUBDEPARTAMENTO == null ? "" : item.NOMBRE_SUBDEPARTAMENTO;
                                }
                                else
                                {

                                }

                                //company.CompanyName = item.COMPANY_NAME;
                                //company.Area.Nombre = item.NOMBRE_AREA;
                                //company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                //company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                //item.UNIDAD_NEGOCIO;
                                //item.COMPANY_NAME;
                                //item.NOMBRE_AREA;
                                //item.NOMBRE_DEPARTAMENTO;
                                //item.NOMBRE_SUBDEPARTAMENTO;

                                result.Objects.Add(company);
                                result.Correct = true;
                            }
                        }
                    }
                    else
                    {
                        var query = context.GetEstructuraByUNegocioD4USucces(CompanyCateg.IdCompanyCategoria).ToList();
                        result.Objects = new List<object>();
                        if (query != null)
                        {
                            Session["Company"] = "";
                            Session["Area"] = "";
                            Session["Departamento"] = "";
                            Session["Subdepartamento"] = "";
                            foreach (var item in query)
                            {
                                ML.Company company = new ML.Company();
                                company.CompanyCategoria = new ML.CompanyCategoria();
                                company.Area = new ML.Area();
                                company.Area.Departamento = new ML.Departamento();
                                company.Area.Departamento.Subdepartamento = new ML.Subdepartamento();

                                company.CompanyCategoria.Descripcion = item.UNIDAD_NEGOCIO;
                                company.CompanyCategoria.IdCompanyCategoria = item.ID_UNEGOCIO;
                                company.CompanyId = item.ID_COMPANY;
                                company.Area.IdArea = Convert.ToInt32(item.ID_AREA);
                                company.Area.Departamento.IdDepartamento = Convert.ToInt32(item.ID_DEPARTAMENTO);
                                company.Area.Departamento.Subdepartamento.IdSubdepartamento = Convert.ToInt32(item.ID_SUBDEPARTAMENTO);

                                string Company = Convert.ToString(Session["Company"]);
                                if (Company != item.COMPANY_NAME)
                                {
                                    company.CompanyName = item.COMPANY_NAME;
                                    Session["Company"] = item.COMPANY_NAME == null ? "" : item.COMPANY_NAME;
                                }
                                else
                                {

                                }
                                //Validar area
                                string Area = Convert.ToString(Session["Area"]);
                                if (Area != item.NOMBRE_AREA)
                                {
                                    company.Area.Nombre = item.NOMBRE_AREA;
                                    Session["Area"] = item.NOMBRE_AREA == null ? "" : item.NOMBRE_AREA;
                                }
                                else
                                {

                                }
                                //Validar Departamento
                                string Departamento = Convert.ToString(Session["Departamento"]);
                                if (Departamento != item.NOMBRE_DEPARTAMENTO)
                                {
                                    company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                    Session["Departamento"] = item.NOMBRE_DEPARTAMENTO == null ? "" : item.NOMBRE_DEPARTAMENTO;
                                }
                                else
                                {

                                }
                                //Validar subdepartamento
                                string Subdepartamento = Convert.ToString(Session["Subdepartamento"]);
                                if (Subdepartamento != item.NOMBRE_SUBDEPARTAMENTO)
                                {
                                    company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                    Session["Subdepartamento"] = item.NOMBRE_SUBDEPARTAMENTO == null ? "" : item.NOMBRE_SUBDEPARTAMENTO;
                                }
                                else
                                {

                                }

                                //company.CompanyName = item.COMPANY_NAME;
                                //company.Area.Nombre = item.NOMBRE_AREA;
                                //company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                //company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                //item.UNIDAD_NEGOCIO;
                                //item.COMPANY_NAME;
                                //item.NOMBRE_AREA;
                                //item.NOMBRE_DEPARTAMENTO;
                                //item.NOMBRE_SUBDEPARTAMENTO;

                                result.Objects.Add(company);
                                result.Correct = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                BL.LogReporteoClima.writteLog(result.ErrorMessage, new StackTrace());
                BL.NLogGeneratorFile.logError(ex, new StackTrace());
            }
            /**/
            //var result = BL.ReporteD4U.GetEstructuraForReporte(CompanyCateg.IdCompanyCategoria);
            return result.Objects;
        }

        public static List<object> GetEstructura_lvl_2(ML.Company aCompany)
        {
            /*Validar repetidos*/
            var aSession = HttpRuntime.Cache;
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    if (1 == 1)
                    {
                        var query = context.f_getEstructuraDescByCompanyId(aCompany.CompanyId).ToList();
                        result.Objects = new List<object>();
                        if (query != null)
                        {
                            aSession["Company"] = "";
                            aSession["Area"] = "";
                            aSession["Departamento"] = "";
                            aSession["Subdepartamento"] = "";
                            foreach (var item in query)
                            {
                                ML.Company company = new ML.Company();
                                company.CompanyCategoria = new ML.CompanyCategoria();
                                company.Area = new ML.Area();
                                company.Area.Departamento = new ML.Departamento();
                                company.Area.Departamento.Subdepartamento = new ML.Subdepartamento();

                                /*company.CompanyCategoria.Descripcion = item.UNIDAD_NEGOCIO;
                                company.CompanyCategoria.IdCompanyCategoria = item.ID_UNEGOCIO;*/
                                company.CompanyId = item.ID_COMPANY;
                                company.Area.IdArea = Convert.ToInt32(item.ID_AREA);
                                company.Area.Departamento.IdDepartamento = Convert.ToInt32(item.ID_DEPARTAMENTO);
                                company.Area.Departamento.Subdepartamento.IdSubdepartamento = Convert.ToInt32(item.ID_SUBDEPARTAMENTO);

                                string Company = Convert.ToString(aSession["Company"]);
                                if (Company != item.COMPANY_NAME)
                                {
                                    company.CompanyName = item.COMPANY_NAME;
                                    aSession["Company"] = item.COMPANY_NAME == null ? "" : item.COMPANY_NAME;
                                }
                                else
                                {

                                }
                                //Validar area
                                string Area = Convert.ToString(aSession["Area"]);
                                if (Area != item.NOMBRE_AREA)
                                {
                                    company.Area.Nombre = item.NOMBRE_AREA;
                                    aSession["Area"] = item.NOMBRE_AREA == null ? "" : item.NOMBRE_AREA;
                                }
                                else
                                {

                                }
                                //Validar Departamento
                                string Departamento = Convert.ToString(aSession["Departamento"]);
                                if (Departamento != item.NOMBRE_DEPARTAMENTO)
                                {
                                    company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                    aSession["Departamento"] = item.NOMBRE_DEPARTAMENTO == null ? "" : item.NOMBRE_DEPARTAMENTO;
                                }
                                else
                                {

                                }
                                //Validar subdepartamento
                                string Subdepartamento = Convert.ToString(aSession["Subdepartamento"]);
                                if (Subdepartamento != item.NOMBRE_SUBDEPARTAMENTO)
                                {
                                    company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                    aSession["Subdepartamento"] = item.NOMBRE_SUBDEPARTAMENTO == null ? "" : item.NOMBRE_SUBDEPARTAMENTO;
                                }
                                else
                                {

                                }

                                //company.CompanyName = item.COMPANY_NAME;
                                //company.Area.Nombre = item.NOMBRE_AREA;
                                //company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                //company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                //item.UNIDAD_NEGOCIO;
                                //item.COMPANY_NAME;
                                //item.NOMBRE_AREA;
                                //item.NOMBRE_DEPARTAMENTO;
                                //item.NOMBRE_SUBDEPARTAMENTO;

                                result.Objects.Add(company);
                                result.Correct = true;
                            }
                        }
                    }
                    /*else
                    {
                        var query = context.GetEstructuraByUNegocioD4USucces(aCompany.CompanyId).ToList();
                        result.Objects = new List<object>();
                        if (query != null)
                        {
                            Session["Company"] = "";
                            Session["Area"] = "";
                            Session["Departamento"] = "";
                            Session["Subdepartamento"] = "";
                            foreach (var item in query)
                            {
                                ML.Company company = new ML.Company();
                                company.CompanyCategoria = new ML.CompanyCategoria();
                                company.Area = new ML.Area();
                                company.Area.Departamento = new ML.Departamento();
                                company.Area.Departamento.Subdepartamento = new ML.Subdepartamento();

                                company.CompanyCategoria.Descripcion = item.UNIDAD_NEGOCIO;
                                company.CompanyCategoria.IdCompanyCategoria = item.ID_UNEGOCIO;
                                company.CompanyId = item.ID_COMPANY;
                                company.Area.IdArea = Convert.ToInt32(item.ID_AREA);
                                company.Area.Departamento.IdDepartamento = Convert.ToInt32(item.ID_DEPARTAMENTO);
                                company.Area.Departamento.Subdepartamento.IdSubdepartamento = Convert.ToInt32(item.ID_SUBDEPARTAMENTO);

                                string Company = Convert.ToString(Session["Company"]);
                                if (Company != item.COMPANY_NAME)
                                {
                                    company.CompanyName = item.COMPANY_NAME;
                                    Session["Company"] = item.COMPANY_NAME;
                                }
                                else
                                {

                                }
                                //Validar area
                                string Area = Convert.ToString(Session["Area"]);
                                if (Area != item.NOMBRE_AREA)
                                {
                                    company.Area.Nombre = item.NOMBRE_AREA;
                                    Session["Area"] = item.NOMBRE_AREA;
                                }
                                else
                                {

                                }
                                //Validar Departamento
                                string Departamento = Convert.ToString(Session["Departamento"]);
                                if (Departamento != item.NOMBRE_DEPARTAMENTO)
                                {
                                    company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                    Session["Departamento"] = item.NOMBRE_DEPARTAMENTO;
                                }
                                else
                                {

                                }
                                //Validar subdepartamento
                                string Subdepartamento = Convert.ToString(Session["Subdepartamento"]);
                                if (Subdepartamento != item.NOMBRE_SUBDEPARTAMENTO)
                                {
                                    company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                    Session["Subdepartamento"] = item.NOMBRE_SUBDEPARTAMENTO;
                                }
                                else
                                {

                                }

                                //company.CompanyName = item.COMPANY_NAME;
                                //company.Area.Nombre = item.NOMBRE_AREA;
                                //company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                //company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                //item.UNIDAD_NEGOCIO;
                                //item.COMPANY_NAME;
                                //item.NOMBRE_AREA;
                                //item.NOMBRE_DEPARTAMENTO;
                                //item.NOMBRE_SUBDEPARTAMENTO;

                                result.Objects.Add(company);
                                result.Correct = true;
                            }
                        }
                    }*/
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                BL.LogReporteoClima.writteLog(result.ErrorMessage, new StackTrace());
                BL.NLogGeneratorFile.logError(ex, new StackTrace());
            }
            /**/
            //var result = BL.ReporteD4U.GetEstructuraForReporte(CompanyCateg.IdCompanyCategoria);
            return result.Objects;
        }

        public static List<object> GetEstructura_lvl_3(ML.Area aArea)
        {
            /*Validar repetidos*/
            var aSession = HttpRuntime.Cache;
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    if (1 == 1)
                    {
                        var query = context.f_getEstructuraDescByIdArea(aArea.IdArea).ToList();
                        result.Objects = new List<object>();
                        if (query != null)
                        {
                            aSession["Company"] = "";
                            aSession["Area"] = "";
                            aSession["Departamento"] = "";
                            aSession["Subdepartamento"] = "";
                            foreach (var item in query)
                            {
                                ML.Company company = new ML.Company();
                                company.CompanyCategoria = new ML.CompanyCategoria();
                                company.Area = new ML.Area();
                                company.Area.Departamento = new ML.Departamento();
                                company.Area.Departamento.Subdepartamento = new ML.Subdepartamento();

                                /*company.CompanyCategoria.Descripcion = item.UNIDAD_NEGOCIO;
                                company.CompanyCategoria.IdCompanyCategoria = item.ID_UNEGOCIO;
                                company.CompanyId = item.ID_COMPANY;*/
                                company.Area.IdArea = Convert.ToInt32(item.ID_AREA);
                                company.Area.Departamento.IdDepartamento = Convert.ToInt32(item.ID_DEPARTAMENTO);
                                company.Area.Departamento.Subdepartamento.IdSubdepartamento = Convert.ToInt32(item.ID_SUBDEPARTAMENTO);

                                /*string Company = Convert.ToString(Session["Company"]);
                                if (Company != item.COMPANY_NAME)
                                {
                                    company.CompanyName = item.COMPANY_NAME;
                                    Session["Company"] = item.COMPANY_NAME;
                                }
                                else
                                {

                                }*/
                                //Validar area
                                string Area = Convert.ToString(aSession["Area"]);
                                if (Area != item.NOMBRE_AREA)
                                {
                                    company.Area.Nombre = item.NOMBRE_AREA;
                                    aSession["Area"] = item.NOMBRE_AREA == null ? "" : item.NOMBRE_AREA;
                                }
                                else
                                {

                                }
                                //Validar Departamento
                                string Departamento = Convert.ToString(aSession["Departamento"]);
                                if (Departamento != item.NOMBRE_DEPARTAMENTO)
                                {
                                    company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                    aSession["Departamento"] = item.NOMBRE_DEPARTAMENTO == null ? "" : item.NOMBRE_DEPARTAMENTO;
                                }
                                else
                                {

                                }
                                //Validar subdepartamento
                                string Subdepartamento = Convert.ToString(aSession["Subdepartamento"]);
                                if (Subdepartamento != item.NOMBRE_SUBDEPARTAMENTO)
                                {
                                    company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                    aSession["Subdepartamento"] = item.NOMBRE_SUBDEPARTAMENTO == null ? "" : item.NOMBRE_SUBDEPARTAMENTO;
                                }
                                else
                                {

                                }

                                //company.CompanyName = item.COMPANY_NAME;
                                //company.Area.Nombre = item.NOMBRE_AREA;
                                //company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                //company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                //item.UNIDAD_NEGOCIO;
                                //item.COMPANY_NAME;
                                //item.NOMBRE_AREA;
                                //item.NOMBRE_DEPARTAMENTO;
                                //item.NOMBRE_SUBDEPARTAMENTO;

                                result.Objects.Add(company);
                                result.Correct = true;
                            }
                        }
                    }
                    /*else
                    {
                        var query = context.GetEstructuraByUNegocioD4USucces(aCompany.CompanyId).ToList();
                        result.Objects = new List<object>();
                        if (query != null)
                        {
                            Session["Company"] = "";
                            Session["Area"] = "";
                            Session["Departamento"] = "";
                            Session["Subdepartamento"] = "";
                            foreach (var item in query)
                            {
                                ML.Company company = new ML.Company();
                                company.CompanyCategoria = new ML.CompanyCategoria();
                                company.Area = new ML.Area();
                                company.Area.Departamento = new ML.Departamento();
                                company.Area.Departamento.Subdepartamento = new ML.Subdepartamento();

                                company.CompanyCategoria.Descripcion = item.UNIDAD_NEGOCIO;
                                company.CompanyCategoria.IdCompanyCategoria = item.ID_UNEGOCIO;
                                company.CompanyId = item.ID_COMPANY;
                                company.Area.IdArea = Convert.ToInt32(item.ID_AREA);
                                company.Area.Departamento.IdDepartamento = Convert.ToInt32(item.ID_DEPARTAMENTO);
                                company.Area.Departamento.Subdepartamento.IdSubdepartamento = Convert.ToInt32(item.ID_SUBDEPARTAMENTO);

                                string Company = Convert.ToString(Session["Company"]);
                                if (Company != item.COMPANY_NAME)
                                {
                                    company.CompanyName = item.COMPANY_NAME;
                                    Session["Company"] = item.COMPANY_NAME;
                                }
                                else
                                {

                                }
                                //Validar area
                                string Area = Convert.ToString(Session["Area"]);
                                if (Area != item.NOMBRE_AREA)
                                {
                                    company.Area.Nombre = item.NOMBRE_AREA;
                                    Session["Area"] = item.NOMBRE_AREA;
                                }
                                else
                                {

                                }
                                //Validar Departamento
                                string Departamento = Convert.ToString(Session["Departamento"]);
                                if (Departamento != item.NOMBRE_DEPARTAMENTO)
                                {
                                    company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                    Session["Departamento"] = item.NOMBRE_DEPARTAMENTO;
                                }
                                else
                                {

                                }
                                //Validar subdepartamento
                                string Subdepartamento = Convert.ToString(Session["Subdepartamento"]);
                                if (Subdepartamento != item.NOMBRE_SUBDEPARTAMENTO)
                                {
                                    company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                    Session["Subdepartamento"] = item.NOMBRE_SUBDEPARTAMENTO;
                                }
                                else
                                {

                                }

                                //company.CompanyName = item.COMPANY_NAME;
                                //company.Area.Nombre = item.NOMBRE_AREA;
                                //company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                //company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                //item.UNIDAD_NEGOCIO;
                                //item.COMPANY_NAME;
                                //item.NOMBRE_AREA;
                                //item.NOMBRE_DEPARTAMENTO;
                                //item.NOMBRE_SUBDEPARTAMENTO;

                                result.Objects.Add(company);
                                result.Correct = true;
                            }
                        }
                    }*/
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                BL.LogReporteoClima.writteLog(result.ErrorMessage, new StackTrace());
                BL.NLogGeneratorFile.logError(ex, new StackTrace());
            }
            /**/
            //var result = BL.ReporteD4U.GetEstructuraForReporte(CompanyCateg.IdCompanyCategoria);
            return result.Objects;
        }

        public static List<object> GetEstructura_lvl_4(ML.Departamento aDepartamento)
        {
            /*Validar repetidos*/
            var aSession = HttpRuntime.Cache;
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    if (1 == 1)
                    {
                        var query = context.f_getEstructuraDescByIdDepartamento(aDepartamento.IdDepartamento).ToList();
                        result.Objects = new List<object>();
                        if (query != null)
                        {
                            aSession["Company"] = "";
                            aSession["Area"] = "";
                            aSession["Departamento"] = "";
                            aSession["Subdepartamento"] = "";
                            foreach (var item in query)
                            {
                                ML.Company company = new ML.Company();
                                company.CompanyCategoria = new ML.CompanyCategoria();
                                company.Area = new ML.Area();
                                company.Area.Departamento = new ML.Departamento();
                                company.Area.Departamento.Subdepartamento = new ML.Subdepartamento();

                                /*company.CompanyCategoria.Descripcion = item.UNIDAD_NEGOCIO;
                                company.CompanyCategoria.IdCompanyCategoria = item.ID_UNEGOCIO;
                                company.CompanyId = item.ID_COMPANY;
                                company.Area.IdArea = Convert.ToInt32(item.ID_AREA);*/
                                company.Area.Departamento.IdDepartamento = Convert.ToInt32(item.ID_DEPARTAMENTO);
                                company.Area.Departamento.Subdepartamento.IdSubdepartamento = Convert.ToInt32(item.ID_SUBDEPARTAMENTO);

                                /*string Company = Convert.ToString(Session["Company"]);
                                if (Company != item.COMPANY_NAME)
                                {
                                    company.CompanyName = item.COMPANY_NAME;
                                    Session["Company"] = item.COMPANY_NAME;
                                }
                                else
                                {

                                }*/
                                //Validar area
                                /*string Area = Convert.ToString(Session["Area"]);
                                if (Area != item.NOMBRE_AREA)
                                {
                                    company.Area.Nombre = item.NOMBRE_AREA;
                                    Session["Area"] = item.NOMBRE_AREA;
                                }
                                else
                                {

                                }*/
                                //Validar Departamento
                                string Departamento = Convert.ToString(aSession["Departamento"]);
                                if (Departamento != item.NOMBRE_DEPARTAMENTO)
                                {
                                    company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                    aSession["Departamento"] = item.NOMBRE_DEPARTAMENTO == null ? "" : item.NOMBRE_DEPARTAMENTO;
                                }
                                else
                                {

                                }
                                //Validar subdepartamento
                                string Subdepartamento = Convert.ToString(aSession["Subdepartamento"]);
                                if (Subdepartamento != item.NOMBRE_SUBDEPARTAMENTO)
                                {
                                    company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                    aSession["Subdepartamento"] = item.NOMBRE_SUBDEPARTAMENTO == null ? "" : item.NOMBRE_SUBDEPARTAMENTO;
                                }
                                else
                                {

                                }

                                //company.CompanyName = item.COMPANY_NAME;
                                //company.Area.Nombre = item.NOMBRE_AREA;
                                //company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                //company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                //item.UNIDAD_NEGOCIO;
                                //item.COMPANY_NAME;
                                //item.NOMBRE_AREA;
                                //item.NOMBRE_DEPARTAMENTO;
                                //item.NOMBRE_SUBDEPARTAMENTO;

                                result.Objects.Add(company);
                                result.Correct = true;
                            }
                        }
                    }
                    /*else
                    {
                        var query = context.GetEstructuraByUNegocioD4USucces(aCompany.CompanyId).ToList();
                        result.Objects = new List<object>();
                        if (query != null)
                        {
                            Session["Company"] = "";
                            Session["Area"] = "";
                            Session["Departamento"] = "";
                            Session["Subdepartamento"] = "";
                            foreach (var item in query)
                            {
                                ML.Company company = new ML.Company();
                                company.CompanyCategoria = new ML.CompanyCategoria();
                                company.Area = new ML.Area();
                                company.Area.Departamento = new ML.Departamento();
                                company.Area.Departamento.Subdepartamento = new ML.Subdepartamento();

                                company.CompanyCategoria.Descripcion = item.UNIDAD_NEGOCIO;
                                company.CompanyCategoria.IdCompanyCategoria = item.ID_UNEGOCIO;
                                company.CompanyId = item.ID_COMPANY;
                                company.Area.IdArea = Convert.ToInt32(item.ID_AREA);
                                company.Area.Departamento.IdDepartamento = Convert.ToInt32(item.ID_DEPARTAMENTO);
                                company.Area.Departamento.Subdepartamento.IdSubdepartamento = Convert.ToInt32(item.ID_SUBDEPARTAMENTO);

                                string Company = Convert.ToString(Session["Company"]);
                                if (Company != item.COMPANY_NAME)
                                {
                                    company.CompanyName = item.COMPANY_NAME;
                                    Session["Company"] = item.COMPANY_NAME;
                                }
                                else
                                {

                                }
                                //Validar area
                                string Area = Convert.ToString(Session["Area"]);
                                if (Area != item.NOMBRE_AREA)
                                {
                                    company.Area.Nombre = item.NOMBRE_AREA;
                                    Session["Area"] = item.NOMBRE_AREA;
                                }
                                else
                                {

                                }
                                //Validar Departamento
                                string Departamento = Convert.ToString(Session["Departamento"]);
                                if (Departamento != item.NOMBRE_DEPARTAMENTO)
                                {
                                    company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                    Session["Departamento"] = item.NOMBRE_DEPARTAMENTO;
                                }
                                else
                                {

                                }
                                //Validar subdepartamento
                                string Subdepartamento = Convert.ToString(Session["Subdepartamento"]);
                                if (Subdepartamento != item.NOMBRE_SUBDEPARTAMENTO)
                                {
                                    company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                    Session["Subdepartamento"] = item.NOMBRE_SUBDEPARTAMENTO;
                                }
                                else
                                {

                                }

                                //company.CompanyName = item.COMPANY_NAME;
                                //company.Area.Nombre = item.NOMBRE_AREA;
                                //company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                //company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                //item.UNIDAD_NEGOCIO;
                                //item.COMPANY_NAME;
                                //item.NOMBRE_AREA;
                                //item.NOMBRE_DEPARTAMENTO;
                                //item.NOMBRE_SUBDEPARTAMENTO;

                                result.Objects.Add(company);
                                result.Correct = true;
                            }
                        }
                    }*/
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                BL.LogReporteoClima.writteLog(result.ErrorMessage, new StackTrace());
                BL.NLogGeneratorFile.logError(ex, new StackTrace());
            }
            /**/
            //var result = BL.ReporteD4U.GetEstructuraForReporte(CompanyCateg.IdCompanyCategoria);
            return result.Objects;
        }


        #region getReporteDataPantalla_6
        public ActionResult getEsperadas(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataEsperadas"));
        }
        public ActionResult getParticipacion(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataParticipacion"));
        }
        public ActionResult getCalificacionGlobal(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataCalificacionGlobal"));
        }
        public ActionResult getConfianza(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataConfianza"));
        }
        public ActionResult getNivelCompromiso(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataNivelCompromiso"));
        }
        public ActionResult getNivelColaboracion(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataNivelColaboracion"));
        }
        #endregion getReporteDataPantalla_6
        #region getReporteDataPantalla_7
        public ActionResult getCredibilidad(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataCredibilidad"));
        }
        public ActionResult getImparcialidad(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataImparcialidad"));
        }
        public ActionResult getOrgullo(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataOrgullo"));
        }
        public ActionResult getRespeto(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataRespeto"));
        }
        public ActionResult getCompanierismo(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataCompanierismo"));
        }
        #endregion getReporteDataPantalla_7
        #region getReporteDataPantalla_8
        public ActionResult getCoaching(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataCoaching"));
        }
        public ActionResult getHabGerenciales(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataHabGerenciales"));
        }
        public ActionResult getAlineacionEstrategica(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataAlineacionEstrategica"));
        }
        public ActionResult getPracticasCulturales(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataPracticasCulturales"));
        }
        public ActionResult getCambio(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataCambio"));
        }
        public ActionResult getProcesosOrga(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataProcesosOrg"));
        }
        #endregion getReporteDataPantalla_8
        #region Pantallas unicas
        public ActionResult getReactivosMejorClasificadosEE(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataMejoresEE"));
        }
        public ActionResult getReactivosMejorClasificadosEA(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataMejoresEA"));
        }
        public ActionResult getReactivosMayorCrecimietoEE(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataCrecimientoEE"));
        }
        public ActionResult getReactivosMayorCrecimietoEA(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataCrecimientoEA"));
        }
        public ActionResult getReactivosPeorClasificadosEE(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataPeoresEE"));
        }
        public ActionResult getReactivosPeorClasificadosEA(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataPeoresEA"));
        }
        public ActionResult getPorcentajePsicoSocialEE(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataBienestarEE"));
        }
        public ActionResult getPorcentajePsicoSocialEA(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataBienestarEA"));
        }
        public ActionResult getIndicadoresPermanencia(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataPermanencia"));
        }
        public ActionResult getIndicadoresAbandono(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataAbandono"));
        }
        public ActionResult getComparativoPermanencia(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataComparativoPermanencia"));
        }
        public ActionResult getComparativoAbandono(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataComparativoAbandono"));
        }
        #endregion Pantallas unicas
        #region Pantallas dobles
        public ActionResult getComparativoEntidadesResultadoGeneralEE(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataComparativoEntidadesResultadoGeneralEE"));
        }
        public ActionResult getComparativoEntidadesResultadoGeneralEA(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataComparativoEntidadesResultadoGeneralEA"));
        }
        public ActionResult getComparativoResultadoGeneralPorNivelesEE(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataComparativoNivelesEE"));
        }
        public ActionResult getComparativoResultadoGeneralPorNivelesEA(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataComparativoNivelesEA"));
        }
        public ActionResult getComparativoPorAntiguedadEE(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataComparativoPorAntiguedadEE"));
        }
        public ActionResult getComparativoPorAntiguedadEA(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataComparativoPorAntiguedadEA"));
        }
        public ActionResult getComparativoPorGeneroEE(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataGeneroEE"));
        }
        public ActionResult getComparativoPorGeneroEA(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataGeneroEA"));
        }
        //dataAcademicoEE
        public ActionResult getComparativoPorGradoAcademicoEE(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataAcademicoEE"));
        }
        public ActionResult getComparativoPorGradoAcademicoEA(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataAcademicoEA"));
        }
        //dataCondicionTraEE
        public ActionResult getComparativoPorCondicionTrabajoEE(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataCondicionTraEE"));
        }
        public ActionResult getComparativoPorCondicionTrabajoEA(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataCondicionTraEA"));
        }
        public ActionResult getComparativoPorFuncionEE(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataFuncionEE"));
        }
        public ActionResult getComparativoPorFuncionEA(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataFuncionEA"));
        }
        public ActionResult getComparativoPorRangoEdadEE(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataEdadEE"));
        }
        public ActionResult getComparativoPorRangoEdadEA(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataEdadEA"));
        }
        public ActionResult getDatosNube1(ML.Historico aHistorico)
        {
            var data = BL.LogReporteoClima.getJsonString(aHistorico, "dataNube1");
            var json = new JsonResult()
            {
                Data = data
            };
            return Content(data);
        }
        public ActionResult getDatosNube2(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataNube2"));
        }
        public ActionResult getDatosNube3(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataNube3"));
        }
        public ActionResult getDatosNube4(ML.Historico aHistorico)
        {
            return Content(BL.LogReporteoClima.getJsonString(aHistorico, "dataNube4"));
        }
        #endregion pantallas dobles
        //Aux
        public static List<int> getTipoInt(JsonResult data)
        {
            var lvl1 = data.Data;
            var lvl1Json = (JsonResult)lvl1;
            var lvl2 = lvl1Json.Data;

            var array = (List<int>)lvl2;

            var s = array[0];

            return array;
        }
        public static List<double> getTipoDecimal(JsonResult data)
        {
            var lvl1 = data.Data;
            var lvl1Json = (JsonResult)lvl1;
            var lvl2 = lvl1Json.Data;

            var array = (List<double>)lvl2;

            var s = array[0];

            return array;
        }
        public static string UnidadNegocioFromEntidad(string entidadNombre, int? entidadId, int? tipoEntidad)
        {
            string Unidad = "";
            if (entidadId == null)
            {
                Unidad = entidadNombre + "_" + 0;
            }
            else
            {
                switch (tipoEntidad)
                {
                    case 1:
                        Unidad = entidadNombre;
                        Unidad = BL.LogReporteoClima.getUnidadNegocioByUnidad(entidadId);
                        break;
                    case 2:
                        Unidad = BL.LogReporteoClima.getUnidadNegocioByCompany(entidadId);
                        break;
                    case 3:
                        Unidad = BL.LogReporteoClima.getUnidadNegocioByArea(entidadId);
                        break;
                    case 4:
                        Unidad = BL.LogReporteoClima.getUnidadNegocioByDepartamento(entidadId);
                        break;
                    default:
                        BL.LogReporteoClima.writteLogJobReporte(new Exception { }, new System.Diagnostics.StackTrace());
                        Unidad = "";
                        break;
                }
            }
            return Unidad;
        }
        public ActionResult existeReporte(ML.Historico aHistorico)
        {
            aHistorico.Anio = aHistorico.Anio + 1;
            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                if (NumeroColumnas(aHistorico) > 11)
                {
                    return Content("Excede");
                }
                var query = context.Demo.Select(o => o).Where(o => o.Anio == aHistorico.Anio && o.NivelDetalle == aHistorico.nivelDetalle /*&& o.EntidadId == aHistorico.EntidadId*/ && o.EntidadNombre == aHistorico.EntidadNombre && o.status == 1 && o.usuario == aHistorico.CurrentUsr).ToList();
                if (query.Count > 0)
                {
                    return Content("true");
                }
                return Content("false");
            }
        }

        public static int NumeroColumnas(ML.Historico historico)
        {
            try
            {
                string auxNivelDetalle = historico.nivelDetalle.ElementAt(0).ToString() + historico.nivelDetalle.ElementAt(1).ToString();
                var descendientesForBienestar = getEstructuraFromExcel((int)historico.IdTipoEntidad, historico.IdBaseDeDatos, historico.EntidadNombre);
                //Ajustar array de entidades segun el nivel de detalle
                if (!auxNivelDetalle.Contains("1"))
                    descendientesForBienestar = descendientesForBienestar.Where(o => o.type != "UNeg=>").ToList();
                if (!auxNivelDetalle.Contains("2"))
                    descendientesForBienestar = descendientesForBienestar.Where(o => o.type != "Comp=>").ToList();
                if (!auxNivelDetalle.Contains("3"))
                    descendientesForBienestar = descendientesForBienestar.Where(o => o.type != "Area=>").ToList();
                if (!auxNivelDetalle.Contains("4"))
                    descendientesForBienestar = descendientesForBienestar.Where(o => o.type != "Dpto=>").ToList();
                if (!auxNivelDetalle.Contains("5"))
                    descendientesForBienestar = descendientesForBienestar.Where(o => o.type != "SubD=>").ToList();
                switch (historico.enfoqueSeleccionado)
                {
                    case 1:
                    case 2:
                        return descendientesForBienestar.Count * 1;// una columna
                    case 0:
                        return descendientesForBienestar.Count * 2;//doble columna
                    default:
                        return 0;
                        break;
                }
            }
            catch (Exception aE)
            {
                return 0;
            }
        }

        public bool boolExisteReporte(ML.Historico aHistorico, int anio, string NivelDetalle, int IdBD)
        {
            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                var query = context.Demo.Select(o => o).Where(o => o.IdBaseDeDatos == IdBD && o.Anio == anio && o.NivelDetalle == NivelDetalle/* && o.EntidadId == aHistorico.EntidadId*/ && o.EntidadNombre == aHistorico.EntidadNombre && o.status == 1 && o.usuario == aHistorico.CurrentUsr).ToList();
                if (query.Count > 0)
                {
                    return true;
                }
                return false;
            }
        }
        public void eliminarReporte(ML.Historico aHistorico, int anio, string NivelDetalle, int idbd)
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Demo.Select(o => o).Where(o => o.IdBaseDeDatos == idbd && o.Anio == anio && o.NivelDetalle == NivelDetalle /*&& o.EntidadId == aHistorico.EntidadId*/ && o.EntidadNombre == aHistorico.EntidadNombre && o.status == 1 && o.usuario == aHistorico.CurrentUsr).ToList();
                    if (query.Count > 0)
                    {
                        context.Demo.RemoveRange(query);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                BL.LogReporteoClima.writteLogJobReporte(aE, new StackTrace());
            }
        }
        public ActionResult getByIndicador([FromUri]int id, [FromBody]ML.Historico aHistorico)
        {
            AnioActual = (int)aHistorico.Anio + 1;
            AnioHistorico = (int)aHistorico.Anio;
            string criterioBusquedaSeleccionado = Convert.ToString(aHistorico.IdTipoEntidad);
            if (aHistorico.EntidadId == 0)
                aHistorico.EntidadId = getIdEntidad(criterioBusquedaSeleccionado, aHistorico.EntidadNombre);
            string uneg = UnidadNegocioFromEntidad(aHistorico.EntidadNombre, aHistorico.EntidadId, aHistorico.IdTipoEntidad);//Turismo_9

            string unidadNeg = uneg.Split('_')[0];
            int idUneg = Convert.ToInt32(uneg.Split('_')[1]);
            var data = new JsonResult();

            switch (id)
            {
                case 1:
                    data = apis.getParticipacion(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                    break;
                case 2:
                    data = apis.getCalificacionGlobal(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                    break;
                case 3:
                    data = apis.getConfianza(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                    break;
                case 4:
                    data = apis.getNivelCompromiso(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                    break;
                case 5:
                    data = apis.getNivelColaboracion(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                    break;
                case 6:
                    data = apis.getCredibilidad(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                    break;
                case 7:
                    data = apis.getImparcialidad(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                    break;
                case 8:
                    data = apis.getOrgullo(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                    break;
                case 9:
                    data = apis.getRespeto(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                    break;
                case 10:
                    data = apis.getCompanierismo(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                    break;
                default:
                    break;
            }
            return data;
        }
        public ActionResult getByIndicador_2([FromUri]int id, [FromBody]ML.Historico aHistorico)
        {
            AnioActual = (int)aHistorico.Anio + 1;
            AnioHistorico = (int)aHistorico.Anio;
            string criterioBusquedaSeleccionado = Convert.ToString(aHistorico.IdTipoEntidad);
            if (aHistorico.EntidadId == 0)
                aHistorico.EntidadId = getIdEntidad(criterioBusquedaSeleccionado, aHistorico.EntidadNombre);
            string uneg = UnidadNegocioFromEntidad(aHistorico.EntidadNombre, aHistorico.EntidadId, aHistorico.IdTipoEntidad);//Turismo_9

            string unidadNeg = uneg.Split('_')[0];
            int idUneg = Convert.ToInt32(uneg.Split('_')[1]);
            var data = new JsonResult();

            switch (id)
            {
                case 1:
                    data = apis.getParticipacion(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                    break;
                case 2:
                    data = apis.getCalificacionGlobal(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                    break;
                case 3:
                    data = apis.getConfianza(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                    break;
                case 4:
                    data = apis.getNivelCompromiso(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                    break;
                case 5:
                    data = apis.getNivelColaboracion(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                    break;
                case 6:
                    data = apis.getCredibilidad(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                    break;
                case 7:
                    data = apis.getImparcialidad(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                    break;
                case 8:
                    data = apis.getOrgullo(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                    break;
                case 9:
                    data = apis.getRespeto(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                    break;
                case 10:
                    data = apis.getCompanierismo(criterioBusquedaSeleccionado, aHistorico.EntidadNombre, unidadNeg, AnioActual, aHistorico.IdBaseDeDatos);
                    break;
                default:
                    break;
            }
            return data;
        }

        public ActionResult getByIndicador_3([FromUri]int id, [FromBody]List<ML.minHistorico> aHistorico)
        {
            AnioActual = (int)aHistorico[0].Anio + 1;
            AnioHistorico = (int)aHistorico[0].Anio;
            //iteracion
            var result = new List<object>();
            foreach (var item in aHistorico)
            {
                string criterioBusquedaSeleccionado = Convert.ToString(item.IdTipoEntidad);
                if (item.EntidadId == 0)
                    item.EntidadId = getIdEntidad(criterioBusquedaSeleccionado, item.EntidadNombre);
                string uneg = UnidadNegocioFromEntidad(item.EntidadNombre, item.EntidadId, item.IdTipoEntidad);//Turismo_9

                string unidadNeg = uneg.Split('_')[0];
                int idUneg = Convert.ToInt32(uneg.Split('_')[1]);
                var data = new JsonResult();

                switch (id)
                {
                    case 1:
                        data = apis.getParticipacion(criterioBusquedaSeleccionado, item.EntidadNombre, unidadNeg, AnioActual, item.IdBaseDeDatos);
                        result.Add(data.Data);
                        break;
                    case 2:
                        data = apis.getCalificacionGlobal(criterioBusquedaSeleccionado, item.EntidadNombre, unidadNeg, AnioActual, item.IdBaseDeDatos);
                        result.Add(data.Data);
                        break;
                    case 3:
                        data = apis.getConfianza(criterioBusquedaSeleccionado, item.EntidadNombre, unidadNeg, AnioActual, item.IdBaseDeDatos);
                        result.Add(data.Data);
                        break;
                    case 4:
                        data = apis.getNivelCompromiso(criterioBusquedaSeleccionado, item.EntidadNombre, unidadNeg, AnioActual, item.IdBaseDeDatos);
                        result.Add(data.Data);
                        break;
                    case 5:
                        data = apis.getNivelColaboracion(criterioBusquedaSeleccionado, item.EntidadNombre, unidadNeg, AnioActual, item.IdBaseDeDatos);
                        result.Add(data.Data);
                        break;
                    case 6:
                        data = apis.getCredibilidad(criterioBusquedaSeleccionado, item.EntidadNombre, unidadNeg, AnioActual, item.IdBaseDeDatos);
                        result.Add(data.Data);
                        break;
                    case 7:
                        data = apis.getImparcialidad(criterioBusquedaSeleccionado, item.EntidadNombre, unidadNeg, AnioActual, item.IdBaseDeDatos);
                        result.Add(data.Data);
                        break;
                    case 8:
                        data = apis.getOrgullo(criterioBusquedaSeleccionado, item.EntidadNombre, unidadNeg, AnioActual, item.IdBaseDeDatos);
                        result.Add(data.Data);
                        break;
                    case 9:
                        data = apis.getRespeto(criterioBusquedaSeleccionado, item.EntidadNombre, unidadNeg, AnioActual, item.IdBaseDeDatos);
                        result.Add(data.Data);
                        break;
                    case 10:
                        data = apis.getCompanierismo(criterioBusquedaSeleccionado, item.EntidadNombre, unidadNeg, AnioActual, item.IdBaseDeDatos);
                        result.Add(data.Data);
                        break;
                    default:
                        break;
                }
            }
            return Json(result);
        }

        public ActionResult getDataReporteCorpo(ML.Historico aHistorico)
        {
            try
            {
                aHistorico.Anio = aHistorico.Anio + 1;
                var data = BL.ReporteD4U.getDataReporteCorpo(aHistorico);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception aE)
            {
                BL.LogReporteoClima.writteLogJobReporte(aE, new StackTrace());
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return Json(aE.Message);
            }
        }

        public ActionResult getByReactivo([FromUri]int id, [FromBody]List<ML.minHistorico> aHistorico)
        {
            var result = new List<object>();
            AnioActual = (int)aHistorico[0].Anio + 1;
            AnioHistorico = (int)aHistorico[0].Anio;
            foreach (var item in aHistorico)
            {
                string criterioBusquedaSeleccionado = Convert.ToString(item.IdTipoEntidad);
                if (item.EntidadId == 0)
                    item.EntidadId = getIdEntidad(criterioBusquedaSeleccionado, item.EntidadNombre);
                string uneg = UnidadNegocioFromEntidad(item.EntidadNombre, item.EntidadId, item.IdTipoEntidad);//Turismo_9

                string unidadNeg = uneg.Split('_')[0];
                int idUneg = Convert.ToInt32(uneg.Split('_')[1]);
                var data = new JsonResult();

                ReporteController repo = new ReporteController();
                item.EntidadNombre = getPrefijo(item.IdTipoEntidad, item.EntidadNombre);
                var model = new ML.ReporteD4U { ListFiltros = new List<string>() { item.EntidadNombre }, IdPregunta = id, UnidadNegocioFilter = unidadNeg };
                var data1 = repo.GetPorcentajeAfirmativasEnfoqueEmpresa(model, AnioActual);
                //EA
                model.IdPregunta = getIdPreguntaEA(model.IdPregunta);
                var data2 = repo.GetPorcentajeAfirmativasEnfoqueEmpresa(model, AnioActual);

                var list = new List<double>();
                list.AddRange(data1);
                list.AddRange(data2);
                result.Add(list);
            }
            return Json(result);
        }

        public ActionResult getComentariosByPalabra(int id, string palabra, ML.Historico aHistorico)
        {
            palabra = palabra.Replace("/", "");
            aHistorico.Anio = aHistorico.Anio + 1;
            var data = BL.ReporteD4U.getComentariosByPalabra(id, palabra, aHistorico);
            return Json(data);
        }

        public ActionResult getComentariosByPalabra3D(string palabra, BL.ReporteD4U.modelRep model)
        {
            palabra = palabra.Replace("/", "");
            var data = BL.ReporteD4U.getComentariosByPalabra(palabra, model);
            return Json(data);
        }

        public static int getIdPreguntaEA(int idpreguntaEE)
        {
            return idpreguntaEE + 86;
        }
        public static int getIdEntidad(string tipo, string nombre)
        {

            var data = 0;
            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                int type = Convert.ToInt32(tipo);
                switch (type)
                {
                    case 2:
                        data = context.Company.Select(o => o).Where(o => o.CompanyName == nombre).FirstOrDefault().CompanyId;
                        break;
                    case 3:
                        data = context.Area.Select(o => o).Where(o => o.Nombre == nombre).FirstOrDefault().IdArea;
                        break;
                    case 4:
                        data = context.Departamento.Select(o => o).Where(o => o.Nombre == nombre).FirstOrDefault().IdDepartamento;
                        break;
                    case 5:
                        data = context.SubDepartamento.Select(o => o).Where(o => o.Nombre == nombre).FirstOrDefault().IdSubdepartamento;
                        break;
                    default:
                        break;
                }
            }
            return data;
        }
        public static string getPrefijo(int? tipo, string nombre)
        {
            var data = "";
            switch (tipo)
            {
                case 1:
                    data = "UNeg=>" + nombre;
                    break;
                case 2:
                    data = "Comp=>" + nombre;
                    break;
                case 3:
                    data = "Area=>" + nombre;
                    break;
                case 4:
                    data = "Dpto=>" + nombre;
                    break;
                case 5:
                    data = "SubD=>" + nombre;
                    break;
                default:
                    break;
            }
            return data;
        }
        
        // obtener estructura afm mediante Layout
        // obtener un nivel hacia abajo
        public static List<string> getEstructuraUnNivelFromExcel(int idTipoEntidad, int IdBD, string entidadNombre)
        {
            switch (idTipoEntidad)
            {
                case 0:
                    return BL.EstructuraAFMReporte.GetUnidadesDeNegocioByIdBD(IdBD);
                case 1:
                    return BL.EstructuraAFMReporte.GetCompaniesByCompanyCategoria(IdBD, entidadNombre);
                case 2:
                    return BL.EstructuraAFMReporte.GetAreasByCompany(IdBD, entidadNombre);
                case 3:
                    return BL.EstructuraAFMReporte.GetDepartamentosByArea(IdBD, entidadNombre);
                case 4:
                    return BL.EstructuraAFMReporte.GetSubDepartamentosByDepartamento(IdBD, entidadNombre);
                default:
                    return new List<string>();
            }
        }

        // obtener todos los niveles hacia abajo
        public static List<PL.Controllers.ApisController.myCustomArray> getEstructuraFromExcel(int IdTipoEntidad, int IdBD, string entidadNombre)
        {
            var listS = new List<string>();
            var list = new List<ApisController.myCustomArray>();
            try
            {
                switch (IdTipoEntidad)
                {
                    case 0:
                        // Nivel GAFM (obtener todas las unidades de negocio mas sus hijos etc)
                        listS = BL.EstructuraAFMReporte.GetEstructuraGAFMForJob_lvl0(IdBD, entidadNombre);
                        break;
                    case 1:
                        listS = BL.EstructuraAFMReporte.GetEstructuraGAFMForJob_lvl1(IdBD, entidadNombre);
                        break;
                    case 2:
                        listS = BL.EstructuraAFMReporte.GetEstructuraGAFMForJob_lvl2(IdBD, entidadNombre);
                        break;
                    default:
                    case 3:
                        listS = BL.EstructuraAFMReporte.GetEstructuraGAFMForJob_lvl3(IdBD, entidadNombre);
                        break;
                    case 4:
                        listS = BL.EstructuraAFMReporte.GetEstructuraGAFMForJob_lvl4(IdBD, entidadNombre);
                        break;
                }
                listS = listS.Where(o => o.Contains(" - -") == false).ToList();
                foreach (var item in listS)
                {
                    string type = item.Substring(0, 6);
                    string value = item.Substring(6, item.Length - 6);
                    list.Add(new ApisController.myCustomArray { type = type, value = value });
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
            }
            return list;
        }

        public static List<PL.Controllers.ApisController.myCustomArray> getEstructuraFromExcelRDinamico(int IdTipoEntidad, int IdBD, string entidadNombre, List<string> niveles)
        {
            var listS = new List<string>();
            var list = new List<ApisController.myCustomArray>();
            try
            {
                switch (IdTipoEntidad)
                {
                    case 1:
                        listS = BL.EstructuraAFMReporte.GetEstructuraGAFMForJob_lvl1ForRDinamico(IdBD, entidadNombre, niveles);
                        break;
                    case 2:
                        listS = BL.EstructuraAFMReporte.GetEstructuraGAFMForJob_lvl2ForRDinamico(IdBD, entidadNombre, niveles);// aqui no se usa
                        break;
                    default:
                    case 3:
                        listS = BL.EstructuraAFMReporte.GetEstructuraGAFMForJob_lvl3ForRDinamico(IdBD, entidadNombre, niveles);// aqui no se usa
                        break;
                    case 4:
                        listS = BL.EstructuraAFMReporte.GetEstructuraGAFMForJob_lvl4ForRDinamico(IdBD, entidadNombre, niveles);// aqui no se usa
                        break;
                }
                listS = listS.Where(o => o.Contains(" - -") == false).ToList();
                foreach (var item in listS)
                {

                    string type = item.Substring(0, 6);
                    string value = item.Substring(6, item.Length - 6);
                    list.Add(new ApisController.myCustomArray { type = type, value = value });
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
            }
            return list;
        }
        public ActionResult Power()
        {
            // Load PDF document
            Document pdfDocument = new Document(@"\\10.5.2.101\\ClimaLaboral\\reporte.pdf");
            PptxSaveOptions pptxOptions = new PptxSaveOptions();
            // Save output file
            pptxOptions.OptimizeTextBoxes = true;
            pptxOptions.SeparateImages = true;
            pptxOptions.ExtractOcrSublayerOnly = true;
            pdfDocument.Save(@"\\10.5.2.101\\ClimaLaboral\\PDF to PPT.ppt", pptxOptions);
            return Json(string.Empty);
        }
        public JsonResult GetPromo()
        {
            try
            {
                var contentHtml = string.Empty;
                HttpWebRequest request = WebRequest.Create("http://autofin.com/auto") as HttpWebRequest;
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string resp = reader.ReadToEnd();
                    contentHtml = resp;
                }
                var img = new List<string>();
                var linkParser = new Regex(@"\b(?:https?://|www\.)\S+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                var rawString = contentHtml;
                foreach (Match m in linkParser.Matches(rawString))
                    img.Add(m.Value);
                img = img.Where(o => o.Contains("http://www.autofin.com/pub/media/wysiwyg/Promociones")).ToList();
                var ls = new List<string>();
                foreach (var item in img)
                    ls.Add(item.Substring(0, (item.IndexOf("jpg") + 3)));
                ls = ls.Distinct().ToList();
                var listPromos = new List<prom>();
                for (int i = 0; i < ls.Count; i++)
                {
                    if (i < (ls.Count - 1))
                    {
                        var promo = new prom();
                        promo.Id = i;
                        promo.ImagenDesktop = ls[i];
                        promo.ImagenMobile = ls[i + 1];
                        promo.estatus = 1;
                        listPromos.Add(promo);
                    }
                }
                var indicesRemove = new List<int>();
                for (int i = 0; i < listPromos.Count; i++)
                {
                    if (i % 2 != 0)
                    {
                        indicesRemove.Add(i);
                    }
                }
                foreach (var item in indicesRemove.OrderByDescending(o => o))
                {
                    listPromos.RemoveAt(item);
                }
                return Json(listPromos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new List<string>(), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetPromoC()
        {
            try
            {
                var contentHtml = string.Empty;
                HttpWebRequest request = WebRequest.Create("http://autofin.com/casa") as HttpWebRequest;
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string resp = reader.ReadToEnd();
                    contentHtml = resp;
                }
                var img = new List<string>();
                var linkParser = new Regex(@"\b(?:https?://|www\.)\S+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                var rawString = contentHtml;
                foreach (Match m in linkParser.Matches(rawString))
                    img.Add(m.Value);
                img = img.Where(o => o.Contains("http://www.autofin.com/pub/media/wysiwyg/aima")).ToList();
                var ls = new List<string>();
                foreach (var item in img)
                    ls.Add(item.Substring(0, (item.IndexOf("jpg") + 3)));
                ls = ls.Distinct().ToList();
                var listPromos = new List<prom>();
                for (int i = 0; i < ls.Count; i++)
                {
                    if (i < (ls.Count - 1))
                    {
                        var promo = new prom();
                        promo.Id = i;
                        promo.ImagenDesktop = ls[i];
                        promo.ImagenMobile = ls[i + 1];
                        promo.estatus = 1;
                        listPromos.Add(promo);
                    }
                }
                var indicesRemove = new List<int>();
                for (int i = 0; i < listPromos.Count; i++)
                {
                    if (i % 2 != 0)
                    {
                        indicesRemove.Add(i);
                    }
                }
                foreach (var item in indicesRemove.OrderByDescending(o => o))
                {
                    listPromos.RemoveAt(item);
                }
                listPromos.Add(new prom() { Id = 0, estatus = 1, ImagenDesktop = "http://www.autofin.com/pub/media/wysiwyg/casa-informacion.jpg", ImagenMobile = "http://www.autofin.com/pub/media/wysiwyg/casa-informacion.jpg" });
                return Json(listPromos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new List<string>(), JsonRequestBehavior.AllowGet);
            }
        }

        public class prom
        {
            public int Id { get; set; }
            public string ImagenDesktop { get; set; }
            public string ImagenMobile { get; set; }
            public int estatus { get; set; }
        }
    }
}