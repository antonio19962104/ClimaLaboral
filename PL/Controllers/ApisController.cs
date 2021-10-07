using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using RealObjects.PDFreactor.Webservice.Client;
using System.IO;
using System.Web;
using OfficeOpenXml;
using System.Security.Cryptography;
using System.Text;

namespace PL.Controllers
{
    public class ApisController : Controller
    {
        /// <summary>
        /// Capa intermedia de consulta
        /// </summary>
        private readonly ReporteController dataLayer = new ReporteController();

        #region Llenado de catalogos
        public JsonResult getCompanyCategoria()
        {
            //return Json(new JsonResult { Data = BL.CompanyCategoria.GetAll(), JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue });
            return Json(BL.CompanyCategoria.GetAll(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getCompanyByCompanyCategoria(string Id)
        {
            int IdCC = String.IsNullOrEmpty(Id) ? 0 : Convert.ToInt32(Id);
            return Json(BL.Company.GetByCompanyCategoriaForD4UTipo1(IdCC), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getAreaByCompany(string Id)
        {
            int IdC = String.IsNullOrEmpty(Id) ? 0 : Convert.ToInt32(Id);
            return Json(BL.Area.GetAreaByEmpresaTipo2(IdC), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getDepartamentoByArea(string Id)
        {
            int IdA = String.IsNullOrEmpty(Id) ? 0 : Convert.ToInt32(Id);
            return Json(BL.Departamento.GetByAreaTipo1(IdA), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getSubDepartamentoByIdDepartamento(string Id)
        {
            int IdD = String.IsNullOrEmpty(Id) ? 0 : Convert.ToInt32(Id);
            return Json(BL.SubDepartamento.GetByIdDepartamento(IdD), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getReactivos()
        {
            return Json(BL.Preguntas.getReactivos(), JsonRequestBehavior.AllowGet);
        }
        #endregion Llenado de catalogos

        #region Pantalla 1
        public JsonResult getEncuestasEsperadas(string aCriterioBusquedaSeleccionado, string aFiltro, string aUnidadDeNegocio, int anioActual, int IdBaseDeDatos)
        {
            /*string Actual_Historico = Convert.ToString(Session["AnioActual_AnioHistorico"]);
            int Actual = Convert.ToInt32(Actual_Historico.Split('_')[0]);
            int Histor = Convert.ToInt32(Actual_Historico.Split('_')[1]);*/
            var model = getObjReporte(aCriterioBusquedaSeleccionado, aFiltro, aUnidadDeNegocio, IdBaseDeDatos);
            return Json(dataLayer.GetEsperadas(model, anioActual), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getParticipacion(string aCriterioBusquedaSeleccionado, string aFiltro, string aUnidadDeNegocio, int anioActual, int IdBaseDeDatos)
        {
            /*string Actual_Historico = Convert.ToString(Session["AnioActual_AnioHistorico"]);
            int Actual = Convert.ToInt32(Actual_Historico.Split('_')[0]);
            int Histor = Convert.ToInt32(Actual_Historico.Split('_')[1]);*/
            var model = getObjReporte(aCriterioBusquedaSeleccionado, aFiltro, aUnidadDeNegocio, IdBaseDeDatos);
            return Json(dataLayer.GetPorcentajeParticipacionEnfoqueEmpresa(model, anioActual), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getConfianza(string aCriterioBusquedaSeleccionado, string aFiltro, string aUnidadDeNegocio, int anioActual, int IdBaseDeDatos)
        {
            var model = getObjReporte(aCriterioBusquedaSeleccionado, aFiltro, aUnidadDeNegocio, IdBaseDeDatos);
            var EE = dataLayer.GetPromediosConfianzaEnfoqueEmpresa(model, anioActual);
            var EA = dataLayer.GetPromediosConfianzaEnfoqueArea(model, anioActual);
            return Json(dataReport(EE, EA), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getCalificacionGlobal(string aCriterioBusquedaSeleccionado, string aFiltro, string aUnidadDeNegocio, int anioActual, int IdBaseDeDatos)
        {
            var model = getObjReporte(aCriterioBusquedaSeleccionado, aFiltro, aUnidadDeNegocio, IdBaseDeDatos);
            var EE = dataLayer.GetPromedios66ReactivosEnfoqueEmpresa(model, anioActual);
            var EA = dataLayer.GetPromedios66ReactivosEnfoqueArea(model, anioActual);
            return Json(dataReport(EE, EA), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getNivelCompromiso(string aCriterioBusquedaSeleccionado, string aFiltro, string aUnidadDeNegocio, int anioActual, int IdBaseDeDatos)
        {
            var model = getObjReporte(aCriterioBusquedaSeleccionado, aFiltro, aUnidadDeNegocio, IdBaseDeDatos);
            var EE = dataLayer.GetPromediosNivelCompromisoEnfoqueEmpresa(model, anioActual);
            var EA = dataLayer.GetPromediosNivelCompromisoEnfoqueArea(model, anioActual);
            return Json(dataReport(EE, EA), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getNivelColaboracion(string aCriterioBusquedaSeleccionado, string aFiltro, string aUnidadDeNegocio, int anioActual, int IdBaseDeDatos)
        {
            var model = getObjReporte(aCriterioBusquedaSeleccionado, aFiltro, aUnidadDeNegocio, IdBaseDeDatos);
            var EE = dataLayer.GetPromediosNivelCoolaboracionEnfoqueEmpresa(model, anioActual);
            var EA = dataLayer.GetPromediosNivelCoolaboracionEnfoqueArea(model, anioActual);
            return Json(dataReport(EE, EA), JsonRequestBehavior.AllowGet);
        }

        #endregion Pantalla 1

        #region Pantalla 2
        public JsonResult getCredibilidad(string aCriterioBusquedaSeleccionado, string aFiltro, string aUnidadDeNegocio, int anioActual, int IdBaseDeDatos)
        {
            var model = getObjReporte(aCriterioBusquedaSeleccionado, aFiltro, aUnidadDeNegocio, IdBaseDeDatos);
            var EE = dataLayer.GetPromediosCreedibilidadEnfoqueEmpresa(model, anioActual);
            var EA = dataLayer.GetPromediosCreedibilidadEnfoqueArea(model, anioActual);
            return Json(dataReport(EE, EA), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getImparcialidad(string aCriterioBusquedaSeleccionado, string aFiltro, string aUnidadDeNegocio, int anioActual, int IdBaseDeDatos)
        {
            var model = getObjReporte(aCriterioBusquedaSeleccionado, aFiltro, aUnidadDeNegocio, IdBaseDeDatos);
            var EE = dataLayer.GetPromediosImparcialidadEnfoqueEmpresa(model, anioActual);
            var EA = dataLayer.GetPromediosImparcialidadEnfoqueArea(model, anioActual);
            return Json(dataReport(EE, EA), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getOrgullo(string aCriterioBusquedaSeleccionado, string aFiltro, string aUnidadDeNegocio, int anioActual, int IdBaseDeDatos)
        {
            var model = getObjReporte(aCriterioBusquedaSeleccionado, aFiltro, aUnidadDeNegocio, IdBaseDeDatos);
            var EE = dataLayer.GetPromediosOrgulloEnfoqueEmpresa(model, anioActual);
            var EA = dataLayer.GetPromediosOrgulloEnfoqueArea(model, anioActual);
            return Json(dataReport(EE, EA), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getRespeto(string aCriterioBusquedaSeleccionado, string aFiltro, string aUnidadDeNegocio, int anioActual, int IdBaseDeDatos)
        {
            var model = getObjReporte(aCriterioBusquedaSeleccionado, aFiltro, aUnidadDeNegocio, IdBaseDeDatos);
            var EE = dataLayer.GetPromediosRespetoEnfoqueEmpresa(model, anioActual);
            var EA = dataLayer.GetPromediosRespetoEnfoqueArea(model, anioActual);
            return Json(dataReport(EE, EA), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getCompanierismo(string aCriterioBusquedaSeleccionado, string aFiltro, string aUnidadDeNegocio, int anioActual, int IdBaseDeDatos)
        {
            var model = getObjReporte(aCriterioBusquedaSeleccionado, aFiltro, aUnidadDeNegocio, IdBaseDeDatos);
            var EE = dataLayer.GetPromediosCompañerismoEnfoqueEmpresa(model, anioActual);
            var EA = dataLayer.GetPromediosCompañerismoEnfoqueArea(model, anioActual);
            return Json(dataReport(EE, EA), JsonRequestBehavior.AllowGet);
        }

        #endregion Pantalla 2

        #region Pantalla 3
        public JsonResult getCoaching(string aCriterioBusquedaSeleccionado, string aFiltro, string aUnidadDeNegocio, int anioActual, int IdBaseDeDatos)
        {
            var model = getObjReporte(aCriterioBusquedaSeleccionado, aFiltro, aUnidadDeNegocio, IdBaseDeDatos);
            var EE = dataLayer.GetPromediosCoachingEnfoqueEmpresa(model, anioActual);
            var EA = dataLayer.GetPromediosCoachingEnfoqueArea(model, anioActual);
            return Json(dataReport(EE, EA), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getHabGerenciales(string aCriterioBusquedaSeleccionado, string aFiltro, string aUnidadDeNegocio, int anioActual, int IdBaseDeDatos)
        {
            var model = getObjReporte(aCriterioBusquedaSeleccionado, aFiltro, aUnidadDeNegocio, IdBaseDeDatos);
            var EE = dataLayer.GetPromediosHabilidadesGerencialesEnfoqueEmpresa(model, anioActual);
            var EA = dataLayer.GetPromediosHabilidadesGerencialesEnfoqueArea(model, anioActual);
            return Json(dataReport(EE, EA), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getAlineacionEstrategica(string aCriterioBusquedaSeleccionado, string aFiltro, string aUnidadDeNegocio, int anioActual, int IdBaseDeDatos)
        {
            var model = getObjReporte(aCriterioBusquedaSeleccionado, aFiltro, aUnidadDeNegocio, IdBaseDeDatos);
            var EE = dataLayer.GetPromediosAlineacionEstrategicaEnfoqueEmpresa(model, anioActual);
            var EA = dataLayer.GetPromediosAlineacionEstrategicaEnfoqueArea(model, anioActual);
            return Json(dataReport(EE, EA), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getPracticasCulturales(string aCriterioBusquedaSeleccionado, string aFiltro, string aUnidadDeNegocio, int anioActual, int IdBaseDeDatos)
        {
            var model = getObjReporte(aCriterioBusquedaSeleccionado, aFiltro, aUnidadDeNegocio, IdBaseDeDatos);
            var EE = dataLayer.GetPromediosPracticasCulturealesEnfoqueEmpresa(model, anioActual);
            var EA = dataLayer.GetPromediosPracticasCulturealesEnfoqueArea(model, anioActual);
            return Json(dataReport(EE, EA), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getCambio(string aCriterioBusquedaSeleccionado, string aFiltro, string aUnidadDeNegocio, int anioActual, int IdBaseDeDatos)
        {
            var model = getObjReporte(aCriterioBusquedaSeleccionado, aFiltro, aUnidadDeNegocio, IdBaseDeDatos);
            var EE = dataLayer.GetPromediosCambioEnfoqueEmpresa(model, anioActual);
            var EA = dataLayer.GetPromediosCambioEnfoqueArea(model, anioActual);
            return Json(dataReport(EE, EA), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getProcesosOrga(string aCriterioBusquedaSeleccionado, string aFiltro, string aUnidadDeNegocio, int anioActual, int IdBaseDeDatos)
        {
            var model = getObjReporte(aCriterioBusquedaSeleccionado, aFiltro, aUnidadDeNegocio, IdBaseDeDatos);
            var EE = dataLayer.GetPromediosProcesosOrganizacionalesEnfoqueEmpresa(model, anioActual);
            var EA = dataLayer.GetPromediosProcesosOrganizacionalesEnfoqueArea(model, anioActual);
            return Json(dataReport(EE, EA), JsonRequestBehavior.AllowGet);
        }
        #endregion Pantalla 3

        #region Pantalla 4 Reactivos mejor clasificados
        public JsonResult getReactivosMejorClasificadosEE(string aCriterioBusquedaSeleccionado, string aFiltro, string aUnidadDeNegocio, int anioActual, int IdBaseDeDatos)
        {
            var model = getObjReporte(aCriterioBusquedaSeleccionado, aFiltro, aUnidadDeNegocio, IdBaseDeDatos);
            var EE = dataLayer.GetMejoresEE(model, anioActual);
            return Json(EE, JsonRequestBehavior.AllowGet);
        }

        //Crecimiento
        public JsonResult getReactivosMayorCrecimietoEE(string aCriterioBusquedaSeleccionado, string aFiltro, string aUnidadDeNegocio, string aEntidadId, int IdBaseDeDatos)
        {
            //Aqui ya trae la data para pintar mayor crecimiento y menor crecimiento EE
            var model = getObjReporte(aCriterioBusquedaSeleccionado, aFiltro, aUnidadDeNegocio, IdBaseDeDatos);
            var EE = dataLayer.GetMayorCrecimientoEE(model, aEntidadId, this.Session, IdBaseDeDatos);
            return Json(EE, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getReactivosMayorCrecimietoEE(string aCriterioBusquedaSeleccionado, string aFiltro, string aUnidadDeNegocio, string aEntidadId, int AnioActual, int IdBaseDeDatos)
        {
            //Aqui ya trae la data para pintar mayor crecimiento y menor crecimiento EE
            var model = getObjReporte(aCriterioBusquedaSeleccionado, aFiltro, aUnidadDeNegocio, IdBaseDeDatos);
            var EE = dataLayer.GetMayorCrecimientoEE(model, aEntidadId, AnioActual);
            return Json(EE, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getReactivosMayorCrecimietoEA(string aCriterioBusquedaSeleccionado, string aFiltro, string aUnidadDeNegocio, string aEntidadId, int IdBaseDeDatos)
        {
            //Aqui ya trae la data para pintar mayor crecimiento y menor crecimiento EE
            var model = getObjReporte(aCriterioBusquedaSeleccionado, aFiltro, aUnidadDeNegocio, IdBaseDeDatos);
            var EE = dataLayer.GetMayorCrecimientoEA(model, aEntidadId, this.Session);
            return Json(EE, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getReactivosMayorCrecimietoEA(string aCriterioBusquedaSeleccionado, string aFiltro, string aUnidadDeNegocio, string aEntidadId, int AnioActual, int IdBaseDeDatos)
        {
            //Aqui ya trae la data para pintar mayor crecimiento y menor crecimiento EE
            var model = getObjReporte(aCriterioBusquedaSeleccionado, aFiltro, aUnidadDeNegocio, IdBaseDeDatos);
            var EE = dataLayer.GetMayorCrecimientoEA(model, aEntidadId, AnioActual);
            return Json(EE, JsonRequestBehavior.AllowGet);
        }

        //end Crecimiento

        public JsonResult getReactivosMejorClasificadosEA(string aCriterioBusquedaSeleccionado, string aFiltro, string aUnidadDeNegocio, int AnioActual, int IdBaseDeDatos)
        {
            var model = getObjReporte(aCriterioBusquedaSeleccionado, aFiltro, aUnidadDeNegocio, IdBaseDeDatos);
            var EA = dataLayer.GetMejoresEA(model, AnioActual);
            return Json(EA, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getReactivosPeorClasificadosEE(string aCriterioBusquedaSeleccionado, string aFiltro, string aUnidadDeNegocio, int AnioActual, int IdBaseDeDatos)
        {
            var model = getObjReporte(aCriterioBusquedaSeleccionado, aFiltro, aUnidadDeNegocio, IdBaseDeDatos);
            var EE = dataLayer.GetPeoresEE(model, AnioActual);
            return Json(EE, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getReactivosPeorClasificadosEA(string aCriterioBusquedaSeleccionado, string aFiltro, string aUnidadDeNegocio, int AnioActual, int IdBaseDeDatos)
        {
            var model = getObjReporte(aCriterioBusquedaSeleccionado, aFiltro, aUnidadDeNegocio, IdBaseDeDatos);
            var EA = dataLayer.GetPeoresEA(model, AnioActual);
            return Json(EA, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getPorcentajePsicoSocialEE(string aCriterioBusquedaSeleccionado, string aFiltro, string aUnidadDeNegocio, [FromBody] List<string> aFiltros, int anioActual, int IdBaseDeDatos)
        {
            aFiltros = aFiltros == null ? new List<string>() : aFiltros;
            var data = new List<double>();
            var model = getObjReporte(aCriterioBusquedaSeleccionado, aFiltro, aUnidadDeNegocio, IdBaseDeDatos);
            foreach (var item in aFiltros)
            {
                /*
                 * Segun el criterio de Busqueda seleccioando es el prefijo
                 * criterio 1: pref: Comp=>
                 * criterio 2: pref: Area=>
                 * criterio 3: pref: Dpto=>
                 * criterio 4: pref: SubD=>
                 */
                model.ListFiltros.Add(setPrefijoForPsicoSocial(aCriterioBusquedaSeleccionado, item));
            }
            model.IdPregunta = 67;
            var fila1 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);
            model.IdPregunta = 71;
            var fila2 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);
            model.IdPregunta = 75;
            var fila3 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);
            model.IdPregunta = 84;
            var fila4 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);

            model.IdPregunta = 79;
            var fila5 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);
            model.IdPregunta = 81;
            var fila6 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);
            model.IdPregunta = 83;
            var fila7 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);
            model.IdPregunta = 86;
            var fila8 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);

            model.IdPregunta = 69;
            var fila9 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);
            model.IdPregunta = 73;
            var fila10 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);
            model.IdPregunta = 77;
            var fila11 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);
            model.IdPregunta = 80;
            var fila12 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);

            //el js para pintar dara saltos en el array segun el numero de afiltros
            data.AddRange(fila1);
            data.AddRange(fila2);
            data.AddRange(fila3);
            data.AddRange(fila4);
            data.AddRange(fila5);
            data.AddRange(fila6);
            data.AddRange(fila7);
            data.AddRange(fila8);
            data.AddRange(fila9);
            data.AddRange(fila10);
            data.AddRange(fila11);
            data.AddRange(fila12);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getPorcentajePsicoSocialEA(string aCriterioBusquedaSeleccionado, string aFiltro, string aUnidadDeNegocio, [FromBody]List<string> aFiltros, int anioActual, int IdBaseDeDatos)
        {
            aFiltros = aFiltros == null ? new List<string>() : aFiltros;
            var data = new List<double>();
            var model = getObjReporte(aCriterioBusquedaSeleccionado, aFiltro, aUnidadDeNegocio, IdBaseDeDatos);
            foreach (var item in aFiltros)
            {
                /*Validar prefijo segun criterio de busqueda*/
                model.ListFiltros.Add(setPrefijoForPsicoSocial(aCriterioBusquedaSeleccionado, item));
            }
            model.IdPregunta = 153;
            var fila1 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);
            model.IdPregunta = 157;
            var fila2 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);
            model.IdPregunta = 161;
            var fila3 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);
            model.IdPregunta = 170;
            var fila4 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);

            model.IdPregunta = 165;
            var fila5 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);
            model.IdPregunta = 167;
            var fila6 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);
            model.IdPregunta = 169;
            var fila7 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);
            model.IdPregunta = 172;
            var fila8 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);

            model.IdPregunta = 155;
            var fila9 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);
            model.IdPregunta = 159;
            var fila10 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);
            model.IdPregunta = 163;
            var fila11 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);
            model.IdPregunta = 166;
            var fila12 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);

            data.AddRange(fila1);
            data.AddRange(fila2);
            data.AddRange(fila3);
            data.AddRange(fila4);
            data.AddRange(fila5);
            data.AddRange(fila6);
            data.AddRange(fila7);
            data.AddRange(fila8);
            data.AddRange(fila9);
            data.AddRange(fila10);
            data.AddRange(fila11);
            data.AddRange(fila12);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // Nuevo ajuste a los calculos de bienestar para obtener todos los hijos
        public JsonResult getPorcentajePsicoSocialEE(string aCriterioBusquedaSeleccionado, string aFiltro, string aUnidadDeNegocio, [FromBody] List<myCustomArray> aFiltros, int anioActual, int IdBaseDeDatos)
        {
            aFiltros = aFiltros == null ? new List<myCustomArray>() : aFiltros;
            var data = new List<double>();
            var model = getObjReporte(aCriterioBusquedaSeleccionado, aFiltro, aUnidadDeNegocio, IdBaseDeDatos);
            foreach (var item in aFiltros)
            {
                /*
                 * type: prefijo
                 * value: nombre de la entidad
                 */
                model.ListFiltros.Add(item.type + item.value);
            }
            model.ListFiltros.RemoveAt(0);
            model.IdPregunta = 67;
            var fila1 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);
            model.IdPregunta = 71;
            var fila2 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);
            model.IdPregunta = 75;
            var fila3 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);
            model.IdPregunta = 84;
            var fila4 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);

            model.IdPregunta = 79;
            var fila5 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);
            model.IdPregunta = 81;
            var fila6 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);
            model.IdPregunta = 83;
            var fila7 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);
            model.IdPregunta = 86;
            var fila8 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);

            model.IdPregunta = 69;
            var fila9 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);
            model.IdPregunta = 73;
            var fila10 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);
            model.IdPregunta = 77;
            var fila11 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);
            model.IdPregunta = 80;
            var fila12 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);

            //el js para pintar dara saltos en el array segun el numero de afiltros
            data.AddRange(fila1);
            data.AddRange(fila2);
            data.AddRange(fila3);
            data.AddRange(fila4);
            data.AddRange(fila5);
            data.AddRange(fila6);
            data.AddRange(fila7);
            data.AddRange(fila8);
            data.AddRange(fila9);
            data.AddRange(fila10);
            data.AddRange(fila11);
            data.AddRange(fila12);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        // Nuevo ajuste a los calculos de bienestar para obtener todos los hijos
        public JsonResult getPorcentajePsicoSocialEA(string aCriterioBusquedaSeleccionado, string aFiltro, string aUnidadDeNegocio, [FromBody] List<myCustomArray> aFiltros, int anioActual, int IdBaseDeDatos)
        {
            aFiltros = aFiltros == null ? new List<myCustomArray>() : aFiltros;
            var data = new List<double>();
            var model = getObjReporte(aCriterioBusquedaSeleccionado, aFiltro, aUnidadDeNegocio, IdBaseDeDatos);
            foreach (var item in aFiltros)
            {
                /*Validar prefijo segun criterio de busqueda*/
                model.ListFiltros.Add(item.type + item.value);
            }
            model.ListFiltros.RemoveAt(0);
            model.IdPregunta = 153;
            var fila1 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);
            model.IdPregunta = 157;
            var fila2 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);
            model.IdPregunta = 161;
            var fila3 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);
            model.IdPregunta = 170;
            var fila4 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);

            model.IdPregunta = 165;
            var fila5 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);
            model.IdPregunta = 167;
            var fila6 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);
            model.IdPregunta = 169;
            var fila7 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);
            model.IdPregunta = 172;
            var fila8 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);

            model.IdPregunta = 155;
            var fila9 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);
            model.IdPregunta = 159;
            var fila10 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);
            model.IdPregunta = 163;
            var fila11 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);
            model.IdPregunta = 166;
            var fila12 = dataLayer.GetPorcentajeAfirmativasEnfoqueEmpresa(model, anioActual);

            data.AddRange(fila1);
            data.AddRange(fila2);
            data.AddRange(fila3);
            data.AddRange(fila4);
            data.AddRange(fila5);
            data.AddRange(fila6);
            data.AddRange(fila7);
            data.AddRange(fila8);
            data.AddRange(fila9);
            data.AddRange(fila10);
            data.AddRange(fila11);
            data.AddRange(fila12);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getIndicadoresPermanencia(string aCriterioBusquedaSeleccionado, string aFiltro, string aUnidadDeNegocio, int anioActual, int IdBaseDeDatos)
        {
            var model = getObjReporte(aCriterioBusquedaSeleccionado, aFiltro, aUnidadDeNegocio, IdBaseDeDatos);
            var data = dataLayer.getIndicadoresPermanencia(model, anioActual);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getIndicadoresAbandono(string aCriterioBusquedaSeleccionado, string aFiltro, string aUnidadDeNegocio, int anioActual, int IdBaseDeDatos)
        {
            var model = getObjReporte(aCriterioBusquedaSeleccionado, aFiltro, aUnidadDeNegocio, IdBaseDeDatos);
            var data = dataLayer.getIndicadoresAbandono(model, anioActual);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getComparativoPermanencia(string aCriterioBusquedaSeleccionado, string aFiltro, string aUnidadDeNegocio, [FromBody]List<string> aFiltros, int anioActual, int IdBaseDeDatos)
        {
            aFiltros = aFiltros == null ? new List<string>() : aFiltros;
            var model = getObjReporte(aCriterioBusquedaSeleccionado, aFiltro, aUnidadDeNegocio, IdBaseDeDatos);
            model.ListFiltros.RemoveAt(0);
            foreach (var item in aFiltros)
            {
                model.ListFiltros.Add(setPrefijoForPsicoSocial(aCriterioBusquedaSeleccionado, item));
            }
            var data = dataLayer.getComparativosPermanencia(model, anioActual);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getComparativoAbandono(string aCriterioBusquedaSeleccionado, string aFiltro, string aUnidadDeNegocio, [FromBody]List<string> aFiltros, int anioActual, int IdBaseDeDatos)
        {
            aFiltros = aFiltros == null ? new List<string>() : aFiltros;
            var model = getObjReporte(aCriterioBusquedaSeleccionado, aFiltro, aUnidadDeNegocio, IdBaseDeDatos);
            model.ListFiltros.RemoveAt(0);
            foreach (var item in aFiltros)
            {
                model.ListFiltros.Add(setPrefijoForPsicoSocial(aCriterioBusquedaSeleccionado, item));
            }
            var data = dataLayer.getComparativosAbandono(model, anioActual);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //Punto Numero 14
        public JsonResult getComparativoEntidadesResultadoGeneralEE(string aCriterioBusquedaSeleccionado, string aFiltro, string aUnidadDeNegocio, [FromBody]List<string> aFiltros, int anioActual, int IdBaseDeDatos)
        {
            aFiltros = aFiltros == null ? new List<string>() : aFiltros;
            var model = getObjReporte(aCriterioBusquedaSeleccionado, aFiltro, aUnidadDeNegocio, IdBaseDeDatos);
            model.ListFiltros.RemoveAt(0);
            foreach (var item in aFiltros)
            {
                model.ListFiltros.Add(setPrefijoForPsicoSocial(aCriterioBusquedaSeleccionado, item));
            }
            var data = dataLayer.getComparativoEntidadesResultadoGeneralEE(model, anioActual);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getComparativoEntidadesResultadoGeneralEA(string aCriterioBusquedaSeleccionado, string aFiltro, string aUnidadDeNegocio, [FromBody]List<string> aFiltros, int anioActual, int IdBaseDeDatos)
        {
            aFiltros = aFiltros == null ? new List<string>() : aFiltros;
            var model = getObjReporte(aCriterioBusquedaSeleccionado, aFiltro, aUnidadDeNegocio, IdBaseDeDatos);
            model.ListFiltros.RemoveAt(0);
            foreach (var item in aFiltros)
            {
                model.ListFiltros.Add(setPrefijoForPsicoSocial(aCriterioBusquedaSeleccionado, item));
            }
            var data = dataLayer.getComparativoEntidadesResultadoGeneralEA(model, anioActual);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        //Punto Numero 15
        public JsonResult getComparativoResultadoGeneralPorNivelesEE(string aCriterioBusquedaSeleccionado, string aFiltro, string aUnidadDeNegocio, [FromBody]List<ApisController.myCustomArray> aFiltros, int anioActual, int IdBaseDeDatos)
        {
            aFiltros = aFiltros == null ? new List<myCustomArray>() : aFiltros;
            var model = getObjReporte(aCriterioBusquedaSeleccionado, aFiltro, aUnidadDeNegocio, IdBaseDeDatos);
            foreach (var item in aFiltros)
            {
                model.ListFiltros.Add(String.Concat(item.type, item.value));
            }
            model.ListFiltros = limpiarDuplicados(model.ListFiltros);
            var data = dataLayer.getComparativoEntidadesResultadoGeneralEE(model, anioActual);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getComparativoResultadoGeneralPorNivelesEA(string aCriterioBusquedaSeleccionado, string aFiltro, string aUnidadDeNegocio, [FromBody]List<myCustomArray> aFiltros, int anioActual, int IdBaseDeDatos)
        {
            aFiltros = aFiltros == null ? new List<myCustomArray>() : aFiltros;
            var model = getObjReporte(aCriterioBusquedaSeleccionado, aFiltro, aUnidadDeNegocio, IdBaseDeDatos);
            foreach (var item in aFiltros)
            {
                model.ListFiltros.Add(String.Concat(item.type, item.value));
            }
            model.ListFiltros = limpiarDuplicados(model.ListFiltros);
            var data = dataLayer.getComparativoEntidadesResultadoGeneralEA(model, anioActual);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getComparativoPorAntiguedadEE(string filtroEntidadAFM, string valorEntidadAFM, int anioActual, int IdBD)
        {
            var filtro = getNameColumn(filtroEntidadAFM);
            var data = BL.ReporteD4U.getComparativoPorAntiguedadEE(filtro, valorEntidadAFM, anioActual, IdBD);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getComparativoPorAntiguedadEA(string filtroEntidadAFM, string valorEntidadAFM, int anioActual, int IdBD)
        {
            var filtro = getNameColumn(filtroEntidadAFM);
            var data = BL.ReporteD4U.getComparativoPorAntiguedadEA(filtro, valorEntidadAFM, anioActual, IdBD);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getComparativoPorGeneroEE(string filtroEntidadAFM, string valorEntidadAFM, int anioActual, int IdBD)
        {
            var filtro = getNameColumn(filtroEntidadAFM);
            var data = BL.ReporteD4U.getComparativoPorGeneroEE(filtro, valorEntidadAFM, anioActual, IdBD);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getComparativoPorGeneroEA(string filtroEntidadAFM, string valorEntidadAFM, int anioActual, int idBD)
        {
            var filtro = getNameColumn(filtroEntidadAFM);
            var data = BL.ReporteD4U.getComparativoPorGeneroEA(filtro, valorEntidadAFM, anioActual, idBD);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getComparativoPorGradoAcademicoEE(string filtroEntidadAFM, string valorEntidadAFM, int anioActual, int idBD)
        {
            var filtro = getNameColumn(filtroEntidadAFM);
            var data = BL.ReporteD4U.getComparativoPorGradoAcademicoEE(filtro, valorEntidadAFM, anioActual, idBD);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getComparativoPorGradoAcademicoEA(string filtroEntidadAFM, string valorEntidadAFM, int anioActual, int idBD)
        {
            var filtro = getNameColumn(filtroEntidadAFM);
            var data = BL.ReporteD4U.getComparativoPorGradoAcademicoEA(filtro, valorEntidadAFM, anioActual, idBD);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getComparativoPorCondicionTrabajoEE(string filtroEntidadAFM, string valorEntidadAFM, int anioActual, int IdBD)
        {
            var filtro = getNameColumn(filtroEntidadAFM);
            var data = BL.ReporteD4U.getComparativoPorCondicionTrabajoEE(filtro, valorEntidadAFM, anioActual, IdBD);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getComparativoPorCondicionTrabajoEA(string filtroEntidadAFM, string valorEntidadAFM, int anioActual, int IdBD)
        {
            var filtro = getNameColumn(filtroEntidadAFM);
            var data = BL.ReporteD4U.getComparativoPorCondicionTrabajoEA(filtro, valorEntidadAFM, anioActual, IdBD);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getComparativoPorFuncionEE(string filtroEntidadAFM, string valorEntidadAFM, int anioActual, int idBD)
        {
            var filtro = getNameColumn(filtroEntidadAFM);
            var data = BL.ReporteD4U.getComparativoPorFuncionEE(filtro, valorEntidadAFM, anioActual, idBD);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getComparativoPorFuncionEA(string filtroEntidadAFM, string valorEntidadAFM, int anioActual, int idBD)
        {
            var filtro = getNameColumn(filtroEntidadAFM);
            var data = BL.ReporteD4U.getComparativoPorFuncionEA(filtro, valorEntidadAFM, anioActual, idBD);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getComparativoPorRangoEdadEE(string filtroEntidadAFM, string valorEntidadAFM, int anioActual, int idBD)
        {
            var filtro = getNameColumn(filtroEntidadAFM);
            var data = BL.ReporteD4U.getComparativoPorRangoEdadEE(filtro, valorEntidadAFM, anioActual, idBD);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getComparativoPorRangoEdadEA(string filtroEntidadAFM, string valorEntidadAFM, int anioActual, int IdBD)
        {
            var filtro = getNameColumn(filtroEntidadAFM);
            var data = BL.ReporteD4U.getComparativoPorRangoEdadEA(filtro, valorEntidadAFM, anioActual, IdBD);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getDatosNube(string aFiltro, string aValor, string aIdPregunta, int anioActual, int IdBD)
        {
            int IdPregunta = aIdPregunta == "" ? 0 : Convert.ToInt32(aIdPregunta);
            var model = getObjReporte(aFiltro, aValor, IdPregunta);
            return Json(BL.ReporteD4U.getDatosNube(model, anioActual, IdBD), JsonRequestBehavior.AllowGet);
        }

        
        public JsonResult getDataNube3D(BL.ReporteD4U.modelRep model)
        {
            var data = BL.ReporteD4U.getDataNube3D(model);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /*public JsonResult getDataReporteDinamico(List<ML.minHistorico> aFiltro)
        {
            return Json(BL.ReporteD4U.getDataReporteDinamico(aFiltro));
        }*/

        public ML.ReporteD4U getObjReporte(string aFiltro, string aValor, int aIdPregunta)
        {
            var model = new ML.ReporteD4U();
            try
            {
                model.filtro = getNameColumn(aFiltro);
                model.filtroValor = aValor;
                model.IdPregunta = aIdPregunta;
            }
            catch (Exception aE)
            {
                BL.LogReporteoClima.writteLog(aE, new StackTrace());
            }
            return model;
        }

        //aFiltro hace referencia a criterioBusquedaSeleccionado
        public static string getNameColumn(string aFiltro)
        {
            switch (aFiltro)
            {
                case "1":
                    return "UnidadNegocio";
                    break;
                case "2":
                    return "DivisionMarca";
                    break;
                case "3":
                    return "AreaAgencia";
                    break;
                case "4":
                    return "Depto";
                    break;
                default:
                    return "";
                    break;
            }
        }

        /*
         * Nuevos metodos usando los queries globales
        */
        /*public JsonResult getReportePromedioGeneralPorAntiguedad_EE(string aCriterioBusquedaSeleccionado, string aFiltro, string aValor, string aFiltroEntidadAFM, string aValorEntidadAFM)
        {
            var model = getObjReporte(aCriterioBusquedaSeleccionado, aFiltro, aValor, aFiltroEntidadAFM, aValorEntidadAFM);
            var data = dataLayer.getReportePromedioGeneralPorAntiguedad_EE(model);
            return Json(data, JsonRequestBehavior.AllowGet);
        }*/


        #endregion Pantalla 4 Reactivos mejor clasificados

        #region Obtener Estructura de la Empresa
        /// <summary>
        /// Metodo que obtiene toda la estructura a partir de una unidad de neogocio(UN, Comp, Area, Depto, SubDpto)
        /// </summary>
        /// <param name="aUnidadNegocio"></param>
        /// <returns></returns>
        public JsonResult getEstructuraDescByUnidadNegocio(string aUnidadNegocio)
        {
            aUnidadNegocio = aUnidadNegocio == null ? "0" : aUnidadNegocio;
            return Json(dataLayer.GetEstructura(new ML.CompanyCategoria { IdCompanyCategoria = Convert.ToInt32(aUnidadNegocio) }, this.HttpContext.Session), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Metodo que obtiene toda la estructura a partir de una Empresa(Company, Area, Depto, SubDpto)
        /// </summary>
        /// <param name="aCompany"></param>
        /// <returns></returns>
        public JsonResult getEstructuraDescByCompany(string aCompany)
        {
            aCompany = String.IsNullOrEmpty(aCompany) ? "0" : aCompany;
            return Json(dataLayer.GetEstructura_lvl_2(new ML.Company { CompanyId = Convert.ToInt32(aCompany) }, this.HttpContext.Session), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Metodo que obtiene toda la estructura a partir de un Area(Area, Depto, SubDpto)
        /// </summary>
        /// <param name="aArea"></param>
        /// <returns></returns>
        public JsonResult getEstructuraByArea(string aArea)
        {
            aArea = String.IsNullOrEmpty(aArea) ? "0" : aArea;
            return Json(dataLayer.GetEstructura_lvl_3(new ML.Area { IdArea = Convert.ToInt32(aArea) }, this.HttpContext.Session), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Metodo que obtiene toda la estructura a partir de un Departamento(Depto, SubDpto)
        /// </summary>
        /// <param name="aDepartamento"></param>
        /// <returns></returns>
        public JsonResult getEstructuraByDepartamento(string aDepartamento)
        {
            aDepartamento = String.IsNullOrEmpty(aDepartamento) ? "0" : aDepartamento;
            return Json(dataLayer.GetEstructura_lvl_4(new ML.Departamento { IdDepartamento = Convert.ToInt32(aDepartamento) }, this.HttpContext.Session), JsonRequestBehavior.AllowGet);
        }
        #endregion Obtener Estructura de la Empresa

        #region Metodos Auxiliares
        public string setPrefijo(string aCriterio, string aFiltro)
        {
            switch (aCriterio)
            {
                case "1":
                    return "UNeg=>" + aFiltro;
                    break;
                case "2":
                    return "Comp=>" + aFiltro;
                    break;
                case "3":
                    return "Area=>" + aFiltro;
                    break;
                case "4":
                    return "Dpto=>" + aFiltro;
                    break;
                case "5":
                    return "SubD=>" + aFiltro;
                    break;
                default:
                    return "";
                    break;
            }
        }

        public string setPrefijoForPsicoSocial(string aCriterio, string aFiltro)
        {
            switch (aCriterio)
            {
                case "1":
                    return "Comp=>" + aFiltro;
                    break;
                case "2":
                    return "Area=>" + aFiltro;
                    break;
                case "3":
                    return "Dpto=>" + aFiltro;
                    break;
                case "4":
                    return "SubD=>" + aFiltro;
                    break;
                default:
                    return "";
                    break;
            }
        }

        public ML.ReporteD4U getObjReporte(string aCriterioBusquedaSeleccionado, string aFiltro, string aUnidadDeNegocio, int IdBaseDeDatos)
        {
            if (IdBaseDeDatos == 0)
                BL.NLogGeneratorFile.logError("El parametro de Base de Datos llegó el cero", new StackTrace());
            ML.ReporteD4U model = new ML.ReporteD4U();
            try
            {
                model.IdBD = IdBaseDeDatos;
                model.UnidadNegocioFilter = aUnidadDeNegocio;
                model.ListFiltros = new List<string>();
                aFiltro = setPrefijo(aCriterioBusquedaSeleccionado, aFiltro);
                model.ListFiltros.Add(aFiltro);
            }
            catch (Exception aE)
            {
                var st = new StackTrace();
                BL.LogReporteoClima.writteLog(aE, new StackTrace());
            }
            return model;
        }

        
        public List<double> dataReport(JsonResult EE, JsonResult EA)
        {
            try
            {
                var list = new List<double>();
                var jsonEE = JsonConvert.SerializeObject(EE.Data);
                var jsonEA = JsonConvert.SerializeObject(EA.Data);

                list.Add(getPorcentFromJSONResult(jsonEE));
                list.Add(getPorcentFromJSONResult(jsonEA));
                return list;
            }
            catch (Exception aE)
            {
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                BL.LogReporteoClima.writteLog(aE, new StackTrace());
                return new List<double>();
            }
        }

        /// <summary>
        /// Convierte el jsonResult a una cadena con formato de arreglo de decimales
        /// </summary>
        /// <param name="EE"></param>
        /// <param name="EA"></param>
        /// <returns></returns>
        public List<string> dataReportAux(JsonResult EE, JsonResult EA)
        {
            try
            {
                var list = new List<string>();
                var jsonEE = JsonConvert.SerializeObject(EE.Data);
                var jsonEA = JsonConvert.SerializeObject(EA.Data);

                list.Add((jsonEE));
                list.Add((jsonEA));
                return list;
            }
            catch (Exception aE)
            {
                var st = new StackTrace();
                BL.LogReporteoClima.writteLog(aE, new StackTrace());
                return new List<string>();
            }
        }

        public double getPorcentFromJSONResult(string data)
        {
            try
            {
                data = data.Replace("[", "");
                data = data.Replace("]", "");
                data = data.Replace('[', '0');
                data = data.Replace(']', '0');
                return Convert.ToDouble(data);
            }
            catch (Exception aE)
            {
                var st = new StackTrace();
                var sf = st.GetFrame(0);
                BL.LogReporteoClima.writteLog(aE, new StackTrace());
                return 0;
            }
        }

        public static List<string> limpiarDuplicados(List<string> filtros)
        {
            return filtros.Distinct().ToList();
        }
        #endregion Metodos Auxiliares

        #region Clases Auxiliares
        public class customFilter
        {
            public string type { get; set; }
            public string value { get; set; }
        }

        public class myCustomArray
        {
            public string type { get; set; }
            public string value { get; set; }
            public int IdUnidadNegocio { get; set; }
            public int CompanyId { get; set; }
            public int IdArea { get; set; }
            public int IdDepartamento { get; set; }
        }
        #endregion Clases Auxiliares

        #region Logs de Error
        public JsonResult writteLogFronEnd(string ex, string funcionName, string currentUsr)
        {
            return Json("Creacion de Log FontEnd correcta: " + BL.LogReporteoClima.writteLogFrontEnd(ex, funcionName, currentUsr), JsonRequestBehavior.AllowGet);
        }
        [System.Web.Mvc.HttpPost]
        public JsonResult writteLogFronEndData([FromBody]List<ML.Historico> aFiltros, string funcionName, string currentUsr, string entidadReporte)
        {
            return Json("Creacion de Log FontEnd correcta: " + BL.LogReporteoClima.writeLogDataRequest(aFiltros, funcionName, currentUsr, entidadReporte), JsonRequestBehavior.AllowGet);
        }
        #endregion Logs de Error

        #region Carga Masiva de Históricos
        /*public JsonResult CargarHistorico(FormCollection formCollection)
        {
            try
            {
                var currentUser = Session["AdminLog"];
                HttpPostedFileBase file = Request.Files["postedFile"];
                string fileName = file.FileName;
                string fileExtension = Path.GetExtension(file.FileName);
                string fileContentType = file.ContentType;
                byte[] fileBytes = new byte[file.ContentLength];
                if (fileExtension == ".xlsx")
                {
                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;
                        var listModel = new List<ML.Historico>();
                        if (noOfRow > 1)
                        {
                            for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                            {
                                ML.Historico model = new ML.Historico();
                                model.IdTipoEntidad = 0;
                                model.Entidad = workSheet.Cells[rowIterator, 1].Value == null ? "" : Convert.ToString(workSheet.Cells[rowIterator, 1].Value);
                                model.Anio = workSheet.Cells[rowIterator, 2].Value == null ? 0 : Convert.ToInt32(workSheet.Cells[rowIterator, 2].Value);
                                model.HC = workSheet.Cells[rowIterator, 3].Value == null ? 2000 : Convert.ToInt32(workSheet.Cells[rowIterator, 3].Value);
                                model.NivelParticipacionEE = workSheet.Cells[rowIterator, 4].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[rowIterator, 4].Value);
                                model.CalificacionGlobalEE = workSheet.Cells[rowIterator, 5].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[rowIterator, 5].Value);
                                model.NivelConfianzaEE = workSheet.Cells[rowIterator, 6].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[rowIterator, 6].Value);
                                model.NivelCompromisoEE = workSheet.Cells[rowIterator, 7].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[rowIterator, 7].Value);
                                model.NivelColaboracionEE = workSheet.Cells[rowIterator, 8].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[rowIterator, 8].Value);
                                model.CreedibilidadEE = workSheet.Cells[rowIterator, 9].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[rowIterator, 9].Value);
                                model.ImparcialidadEE = workSheet.Cells[rowIterator, 10].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[rowIterator, 10].Value);
                                model.OrgulloEE = workSheet.Cells[rowIterator, 11].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[rowIterator, 11].Value);
                                model.RespetoEE = workSheet.Cells[rowIterator, 12].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[rowIterator, 12].Value);
                                model.CompanierismoEE = workSheet.Cells[rowIterator, 13].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[rowIterator, 13].Value);
                                model.CoachingEE = workSheet.Cells[rowIterator, 14].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[rowIterator, 14].Value);
                                model.HabilidadesGerencialesEE = workSheet.Cells[rowIterator, 15].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[rowIterator, 15].Value);
                                model.AlineacionEstrategicaEE = workSheet.Cells[rowIterator, 16].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[rowIterator, 16].Value);
                                model.PracticasCulturalesEE = workSheet.Cells[rowIterator, 17].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[rowIterator, 17].Value);
                                model.ManejoDelCambioEE = workSheet.Cells[rowIterator, 18].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[rowIterator, 18].Value);
                                model.ProcesosOrganizacionalesEE = workSheet.Cells[rowIterator, 19].Value == null ? 0 : Convert.ToDecimal(workSheet.Cells[rowIterator, 19].Value);
                                if (!ModelState.IsValid)
                                {
                                    model.MensajesValidacion = new List<ML.MensajesError>();
                                    return Json(model.MensajesValidacion = Models.MensajesValidacion.getMensajesValidacion(ModelState));
                                }
                                listModel.Add(model);
                            }
                            BL.HistoricoClimaLaboral.Create(listModel, currentUser);
                        }
                    }
                }
            }
            catch (Exception aE)
            {
                BL.LogReporteoClima.writteLog(aE, new StackTrace());
                return Json(aE.Message);
            }
            return Json("Los historicos se cargaron correctamente");
        }*/
        /*[System.Web.Mvc.HttpPost]
        public JsonResult CargarHistorico(List<ML.Historico> aFiltros)
        {
            try
            {
                var currentUser = Session["AdminLog"];
                foreach (var item in aFiltros)
                {
                    if (!ModelState.IsValid)
                    {
                        item.MensajesValidacion = new List<ML.MensajesError>();
                        return Json(item.MensajesValidacion = Models.MensajesValidacion.getMensajesValidacion(ModelState));
                    }
                }
                return Json(BL.HistoricoClimaLaboral.Create(aFiltros, currentUser));
            }
            catch (Exception aE)
            {
                BL.LogReporteoClima.writteLog(aE, new StackTrace());
                return Json(aE.Message);
            }
            //return Json("Los historicos se cargaron correctamente");
        }*/
        [System.Web.Mvc.HttpPost]
        public JsonResult CargarHistorico_2(List<ML.HistoricoClima> aFiltros)
        {
            try
            {
                var currentUser = Session["AdminLog"];
                foreach (var item in aFiltros)
                {
                    if (!ModelState.IsValid)
                    {
                        item.MensajesValidacion = new List<ML.MensajesError>();
                        return Json(item.MensajesValidacion = Models.MensajesValidacion.getMensajesValidacion(ModelState));
                    }
                }
                return Json(BL.HistoricoClimaLaboral.Create(aFiltros, currentUser));
            }
            catch (Exception aE)
            {
                BL.LogReporteoClima.writteLog(aE, new StackTrace());
                return Json(aE.Message);
            }
            //return Json("Los historicos se cargaron correctamente");
        }
        public static string GeneratePasswordResetToken(int userId)
        {
            int size = 30;
            char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[4 * size];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(size);
            for (int i = 0; i < size; i++)
            {
                var rnd = BitConverter.ToUInt32(data, i * 4);
                var idx = rnd % chars.Length;

                result.Append(chars[idx]);
            }

            return result.ToString();
        }
        #endregion

        #region pdf
        public JsonResult dataPDF()
        {
            PDFreactor pdfReactor = new PDFreactor("https://cloud.pdfreactor.com/service/rest")
            {
                Timeout = 0
            };

            var config = new Configuration
            {
                Document = "https://stackoverflow.com/"
            };
            pdfReactor.Convert(config);
            Result data = pdfReactor.Convert(config);
            BinaryWriter binWriter = new BinaryWriter(new FileStream("\\\\10.5.2.101\\RHDiagnostics\\log\\test.pdf",
                FileMode.Create,
                FileAccess.Write));
            binWriter.Write(data.Document);
            binWriter.Close();


            return Json(binWriter);
        }
        public JsonResult dataPDFs(string url)
        {
            var b64String = "";
            try
            {
                PDFreactor pdfReactor = new PDFreactor("https://cloud.pdfreactor.com/service/rest")
                {
                    Timeout = 0
                };
                pdfReactor.ApiKey = "302c021425af701e834d96f4377c5555e478f9a37262e47d0214148b4d5bb14a7db0fff8c01f5b9e07041a472cc3";
                var config = new Configuration
                {
                    Document = url
                };
                pdfReactor.Convert(config);
                Result data = pdfReactor.Convert(config);
                BinaryWriter binWriter = new BinaryWriter(new FileStream("\\\\10.5.2.101\\RHDiagnostics\\log\\test.pdf",
                    FileMode.Create,
                    FileAccess.Write));
                binWriter.Write(data.Document);
                binWriter.Close();

                b64String = Convert.ToBase64String(data.Document);

                return Json(b64String, JsonRequestBehavior.AllowGet);
            }
            catch (Exception aE)
            {
                BL.LogReporteoClima.writteLog(aE, new StackTrace());
            }
            return Json(b64String, JsonRequestBehavior.AllowGet);
        }
        #endregion pdf

        public ActionResult DownloadLayout_1()
        {
            string file = "LayoutPreguntasPorcentaje - Clima Laboral.xlsx";
            string fullPath = Path.Combine(Server.MapPath("~/resources/"), file);
            return File(fullPath, "application/vnd.ms-excel", file);
        }
        public ActionResult DownloadLayout_2()
        {
            string file = "LayoutHistoricos.xlsx";
            string fullPath = Path.Combine(Server.MapPath("~/resources/"), file);
            return File(fullPath, "application/vnd.ms-excel", file);
        }

        public JsonResult getEstructuraFromExcel(int IdTipoEntidad, int IdBD, string entidadNombre)
        {
            var listS = new List<string>();
            var list = new List<myCustomArray>();
            try
            {
                switch (IdTipoEntidad)
                {
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
                foreach (var item in listS)
                {
                    string type = item.Substring(0, 6);
                    string value = item.Substring(6, item.Length - 6);
                    list.Add(new myCustomArray { type = type, value = value });
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getCompanyByCompanyCategoriaFromExcel(int IdBaseDeDatos, string UnidadNegocio)
        {
            return Json(BL.EstructuraAFMReporte.GetCompaniesByCompanyCategoria(IdBaseDeDatos, UnidadNegocio), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getAreaByCompanyFromExcel(int IdBaseDeDatos, string Company)
        {
            return Json(BL.EstructuraAFMReporte.GetAreasByCompany(IdBaseDeDatos, Company), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getDepartamentoByAreaFromExcel(int IdBaseDeDatos, string Area)
        {
            return Json(BL.EstructuraAFMReporte.GetDepartamentosByArea(IdBaseDeDatos, Area), JsonRequestBehavior.AllowGet);
        }


        /* Metodos para los cambios en el reporte
         * 06/10/2021
         * jamurillo
         */
        public JsonResult getParticipacion_(string aCriterioBusquedaSeleccionado, List<myCustomArray> hijos, string aUnidadDeNegocio, int anioActual, int IdBaseDeDatos)
        {
            var model = getAuxObjReporte(hijos, IdBaseDeDatos, anioActual);
            return Json(dataLayer.GetPorcentajeParticipacionEnfoqueEmpresa(model, anioActual), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getEncuestasEsperadas_(string aCriterioBusquedaSeleccionado, List<myCustomArray> hijos, string aUnidadDeNegocio, int anioActual, int IdBaseDeDatos)
        {
            var model = getAuxObjReporte(hijos, IdBaseDeDatos, anioActual);
            return Json(dataLayer.GetEsperadas(model, anioActual), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getCalificacionGlobal_(string aCriterioBusquedaSeleccionado, List<myCustomArray> hijos, string aUnidadDeNegocio, int anioActual, int IdBaseDeDatos)
        {
            var model = getAuxObjReporte(hijos, IdBaseDeDatos, anioActual);
            var EE = dataLayer.GetPromedios66ReactivosEnfoqueEmpresa(model, anioActual);
            var EA = dataLayer.GetPromedios66ReactivosEnfoqueArea(model, anioActual);
            return Json(dataReportAux(EE, EA), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getConfianza_(string aCriterioBusquedaSeleccionado, List<myCustomArray> hijos, string aUnidadDeNegocio, int anioActual, int IdBaseDeDatos)
        {
            var model = getAuxObjReporte(hijos, IdBaseDeDatos, anioActual);
            var EE = dataLayer.GetPromediosConfianzaEnfoqueEmpresa(model, anioActual);
            var EA = dataLayer.GetPromediosConfianzaEnfoqueArea(model, anioActual);
            return Json(dataReportAux(EE, EA), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getNivelCompromiso_(string aCriterioBusquedaSeleccionado, List<myCustomArray> hijos, string aUnidadDeNegocio, int anioActual, int IdBaseDeDatos)
        {
            var model = getAuxObjReporte(hijos, IdBaseDeDatos, anioActual);
            var EE = dataLayer.GetPromediosNivelCompromisoEnfoqueEmpresa(model, anioActual);
            var EA = dataLayer.GetPromediosNivelCompromisoEnfoqueArea(model, anioActual);
            return Json(dataReportAux(EE, EA), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getNivelColaboracion_(string aCriterioBusquedaSeleccionado, List<myCustomArray> hijos, string aUnidadDeNegocio, int anioActual, int IdBaseDeDatos)
        {
            var model = getAuxObjReporte(hijos, IdBaseDeDatos, anioActual);
            var EE = dataLayer.GetPromediosNivelCoolaboracionEnfoqueEmpresa(model, anioActual);
            var EA = dataLayer.GetPromediosNivelCoolaboracionEnfoqueArea(model, anioActual);
            return Json(dataReportAux(EE, EA), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getCredibilidad_(string aCriterioBusquedaSeleccionado, List<myCustomArray> hijos, string aUnidadDeNegocio, int anioActual, int IdBaseDeDatos)
        {
            var model = getAuxObjReporte(hijos, IdBaseDeDatos, anioActual);
            var EE = dataLayer.GetPromediosCreedibilidadEnfoqueEmpresa(model, anioActual);
            var EA = dataLayer.GetPromediosCreedibilidadEnfoqueArea(model, anioActual);
            return Json(dataReportAux(EE, EA), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getImparcialidad_(string aCriterioBusquedaSeleccionado, List<myCustomArray> hijos, string aUnidadDeNegocio, int anioActual, int IdBaseDeDatos)
        {
            var model = getAuxObjReporte(hijos, IdBaseDeDatos, anioActual);
            var EE = dataLayer.GetPromediosImparcialidadEnfoqueEmpresa(model, anioActual);
            var EA = dataLayer.GetPromediosImparcialidadEnfoqueArea(model, anioActual);
            return Json(dataReportAux(EE, EA), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getOrgullo_(string aCriterioBusquedaSeleccionado, List<myCustomArray> hijos, string aUnidadDeNegocio, int anioActual, int IdBaseDeDatos)
        {
            var model = getAuxObjReporte(hijos, IdBaseDeDatos, anioActual);
            var EE = dataLayer.GetPromediosOrgulloEnfoqueEmpresa(model, anioActual);
            var EA = dataLayer.GetPromediosOrgulloEnfoqueArea(model, anioActual);
            return Json(dataReportAux(EE, EA), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getRespeto_(string aCriterioBusquedaSeleccionado, List<myCustomArray> hijos, string aUnidadDeNegocio, int anioActual, int IdBaseDeDatos)
        {
            var model = getAuxObjReporte(hijos, IdBaseDeDatos, anioActual);
            var EE = dataLayer.GetPromediosRespetoEnfoqueEmpresa(model, anioActual);
            var EA = dataLayer.GetPromediosRespetoEnfoqueArea(model, anioActual);
            return Json(dataReportAux(EE, EA), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getCompanierismo_(string aCriterioBusquedaSeleccionado, List<myCustomArray> hijos, string aUnidadDeNegocio, int anioActual, int IdBaseDeDatos)
        {
            var model = getAuxObjReporte(hijos, IdBaseDeDatos, anioActual);
            var EE = dataLayer.GetPromediosCompañerismoEnfoqueEmpresa(model, anioActual);
            var EA = dataLayer.GetPromediosCompañerismoEnfoqueArea(model, anioActual);
            return Json(dataReportAux(EE, EA), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getCoaching_(string aCriterioBusquedaSeleccionado, List<myCustomArray> hijos, string aUnidadDeNegocio, int anioActual, int IdBaseDeDatos)
        {
            var model = getAuxObjReporte(hijos, IdBaseDeDatos, anioActual);
            var EE = dataLayer.GetPromediosCoachingEnfoqueEmpresa(model, anioActual);
            var EA = dataLayer.GetPromediosCoachingEnfoqueArea(model, anioActual);
            return Json(dataReportAux(EE, EA), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getHabGerenciales_(string aCriterioBusquedaSeleccionado, List<myCustomArray> hijos, string aUnidadDeNegocio, int anioActual, int IdBaseDeDatos)
        {
            var model = getAuxObjReporte(hijos, IdBaseDeDatos, anioActual);
            var EE = dataLayer.GetPromediosHabilidadesGerencialesEnfoqueEmpresa(model, anioActual);
            var EA = dataLayer.GetPromediosHabilidadesGerencialesEnfoqueArea(model, anioActual);
            return Json(dataReportAux(EE, EA), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getAlineacionEstrategica_(string aCriterioBusquedaSeleccionado, List<myCustomArray> hijos, string aUnidadDeNegocio, int anioActual, int IdBaseDeDatos)
        {
            var model = getAuxObjReporte(hijos, IdBaseDeDatos, anioActual);
            var EE = dataLayer.GetPromediosAlineacionEstrategicaEnfoqueEmpresa(model, anioActual);
            var EA = dataLayer.GetPromediosAlineacionEstrategicaEnfoqueArea(model, anioActual);
            return Json(dataReportAux(EE, EA), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getPracticasCulturales_(string aCriterioBusquedaSeleccionado, List<myCustomArray> hijos, string aUnidadDeNegocio, int anioActual, int IdBaseDeDatos)
        {
            var model = getAuxObjReporte(hijos, IdBaseDeDatos, anioActual);
            var EE = dataLayer.GetPromediosPracticasCulturealesEnfoqueEmpresa(model, anioActual);
            var EA = dataLayer.GetPromediosPracticasCulturealesEnfoqueArea(model, anioActual);
            return Json(dataReportAux(EE, EA), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getCambio_(string aCriterioBusquedaSeleccionado, List<myCustomArray> hijos, string aUnidadDeNegocio, int anioActual, int IdBaseDeDatos)
        {
            var model = getAuxObjReporte(hijos, IdBaseDeDatos, anioActual);
            var EE = dataLayer.GetPromediosCambioEnfoqueEmpresa(model, anioActual);
            var EA = dataLayer.GetPromediosCambioEnfoqueArea(model, anioActual);
            return Json(dataReportAux(EE, EA), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getProcesosOrga_(string aCriterioBusquedaSeleccionado, List<myCustomArray> hijos, string aUnidadDeNegocio, int anioActual, int IdBaseDeDatos)
        {
            var model = getAuxObjReporte(hijos, IdBaseDeDatos, anioActual);
            var EE = dataLayer.GetPromediosProcesosOrganizacionalesEnfoqueEmpresa(model, anioActual);
            var EA = dataLayer.GetPromediosProcesosOrganizacionalesEnfoqueArea(model, anioActual);
            return Json(dataReportAux(EE, EA), JsonRequestBehavior.AllowGet);
        }
        public ML.ReporteD4U getAuxObjReporte(List<myCustomArray> aFiltros, int IdBaseDeDatos, int Anio)
        {
            var model = new ML.ReporteD4U();
            model.ListFiltros = new List<string>();
            model.IdBD = IdBaseDeDatos;
            model.Anio = Anio;
            try
            {
                foreach (var item in aFiltros)
                {
                    model.ListFiltros.Add(item.type + item.value);
                }
            }
            catch (Exception aE)
            {
                BL.LogReporteoClima.writteLog(aE, new StackTrace());
            }
            return model;
        }
    }
}
