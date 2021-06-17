using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class ReporteClimaDinamicoController : Controller
    {
        private Models.ReporteClimaDinamico model = new Models.ReporteClimaDinamico();
        //ML.modelReporte aModel = new ML.modelReporte() { anioActual = 2020, entidadNombre = "TURISMO", idEncuesta = 1, idEnfoque = 1, idTipoEntidad = 1 };
        // GET: ReporteClimaDinamico
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult creaReporte(ML.Historico aHistorico)
        {
            try
            {
                int _conteoExiste = 0;
                double dataComp1 = 0;
                double dataComp2 = 0;
                double dataComp3 = 0;
                aHistorico.CurrentUsr = "jamurillo";
                Stopwatch st = new Stopwatch();
                st.Start();
                int AnioActual = (int)aHistorico.Anio + 1;
                int AnioHistorico = (int)aHistorico.Anio;
                string uneg = BL.ReporteClimaDinamico.UnidadNegocioFromEntidad(aHistorico.EntidadNombre, aHistorico.EntidadId, aHistorico.IdTipoEntidad);//Turismo_9
                string unidadNeg = uneg.Split('_')[0];
                int idUneg = Convert.ToInt32(uneg.Split('_')[1]);
                if (BL.ReporteClimaDinamico.boolExisteReporte(aHistorico, AnioActual))
                    BL.ReporteClimaDinamico.eliminarReporte(aHistorico, AnioActual);
                ML.modelReporte aModel = new ML.modelReporte()
                {
                    anioActual = AnioActual,
                    entidadNombre = aHistorico.EntidadNombre,
                    idEntidad = (int)aHistorico.EntidadId,
                    idTipoEntidad = (int)aHistorico.IdTipoEntidad,
                    idEnfoque = 1,
                    idEncuesta = aHistorico.idEncuesta
                };
                /*
                 * se obtienen las entidades hijas
                 * se itera dos veces para calcular ambos enfoques
                */
                var aFiltrosEntUnNivelAbajo = new List<ML.finalCols>();
                aFiltrosEntUnNivelAbajo.Add(new ML.finalCols { type = BL.ReporteClimaDinamico.getPrefijoByIdTipoEntidad(aModel.idTipoEntidad), value = aModel.entidadNombre });
                aFiltrosEntUnNivelAbajo.AddRange(getFiltros(idUneg, aModel.idTipoEntidad, aModel.idEntidad, aModel.entidadNombre));
                var aFiltrosHijosEstructura = new List<ApisController.myCustomArray>();
                aFiltrosHijosEstructura.Add(new ApisController.myCustomArray { type = BL.ReporteClimaDinamico.getPrefijoByIdTipoEntidad(aModel.idTipoEntidad), value = aModel.entidadNombre });
                aFiltrosHijosEstructura.AddRange(getHijosEstructura(idUneg, aHistorico.IdTipoEntidad, aHistorico.EntidadId));
                for (int i = 1; i < 3; i++)
                {
                    _conteoExiste = 0;
                    if (i == 2)
                        Console.WriteLine();
                    ML.modelReporte aModelDinEnf = new ML.modelReporte()
                    {
                        anioActual = AnioActual,
                        entidadNombre = aHistorico.EntidadNombre,
                        idEntidad = (int)aHistorico.EntidadId,
                        idTipoEntidad = (int)aHistorico.IdTipoEntidad,
                        idEnfoque = i,
                        idEncuesta = aHistorico.idEncuesta
                    };
                    
                    #region getDataReporte parte 1
                    // calificacion global
                    model.objCalificacionGlobal.Data = BL.ReporteClimaDinamico.getPromedio66R(aModelDinEnf);
                    BL.ReporteClimaDinamico.addJsonReporte(aModel.idEntidad, aModel.entidadNombre, model.objCalificacionGlobal, AnioActual, "dataConfianza", aHistorico.CurrentUsr, i);
                    
                    // Dato Compuesto => Confianza se obtiene de la competencia 3, 5, 7
                    aModelDinEnf.idCompetencia = 3;
                    if (BL.ReporteClimaDinamico.isActiveCompetencia(aModelDinEnf.idCompetencia, aModelDinEnf.idEncuesta)) { dataComp1 = BL.ReporteClimaDinamico.getPorcentajeAfirmativasByCompetencia(aModelDinEnf); _conteoExiste++; }
                    aModelDinEnf.idCompetencia = 5;
                    if (BL.ReporteClimaDinamico.isActiveCompetencia(aModelDinEnf.idCompetencia, aModelDinEnf.idEncuesta)) { dataComp2 = BL.ReporteClimaDinamico.getPorcentajeAfirmativasByCompetencia(aModelDinEnf); _conteoExiste++; }
                    aModelDinEnf.idCompetencia = 7;
                    if (BL.ReporteClimaDinamico.isActiveCompetencia(aModelDinEnf.idCompetencia, aModelDinEnf.idEncuesta)) { dataComp3 = BL.ReporteClimaDinamico.getPorcentajeAfirmativasByCompetencia(aModelDinEnf); _conteoExiste++; }
                    double confianza = (dataComp1 + dataComp2 + dataComp3) / _conteoExiste;
                    model.objConfianza.Data = BL.Truncate.TruncateNumber(confianza);
                    BL.ReporteClimaDinamico.addJsonReporte(aModel.idEntidad, aModel.entidadNombre, model.objConfianza, AnioActual, "dataConfianza", aHistorico.CurrentUsr, i);
                    
                    // Categoria => Nivel De Comprosmiso
                    aModelDinEnf.idSubCategoria = 24;
                    if (BL.ReporteClimaDinamico.isActiveCategoria(aModelDinEnf.idSubCategoria, aModelDinEnf.idEncuesta))
                        model.objNivelCompromiso.Data = BL.ReporteClimaDinamico.getPorcentajeAfirmativasByCategoria(aModelDinEnf);
                    BL.ReporteClimaDinamico.addJsonReporte(aModel.idEntidad, aModel.entidadNombre, model.objNivelCompromiso, AnioActual, "dataNivelCompromiso", aHistorico.CurrentUsr, i);
                    
                    // Categoria => Nivel De Colaboracion (hace falta primero que se guarden las configuraciones)
                    aModelDinEnf.idSubCategoria = 23;
                    if (BL.ReporteClimaDinamico.isActiveCategoria(aModelDinEnf.idSubCategoria, aModelDinEnf.idEncuesta))
                        model.objNivelColaboracion.Data = BL.ReporteClimaDinamico.getPorcentajeAfirmativasByCategoria(aModelDinEnf);
                    BL.ReporteClimaDinamico.addJsonReporte(aModel.idEntidad, aModel.entidadNombre, model.objNivelColaboracion, AnioActual, "dataNivelColaboracion", aHistorico.CurrentUsr, i);

                    // Competencia => Credibilidad
                    aModelDinEnf.idCompetencia = 3;
                    if (BL.ReporteClimaDinamico.isActiveCompetencia(aModelDinEnf.idCompetencia, aModelDinEnf.idEncuesta))
                        model.objCredibilidad.Data = BL.ReporteClimaDinamico.getPorcentajeAfirmativasByCompetencia(aModelDinEnf);
                    BL.ReporteClimaDinamico.addJsonReporte(aModel.idEntidad, aModel.entidadNombre, model.objCredibilidad, AnioActual, "dataCredibilidad", aHistorico.CurrentUsr, i);

                    // Competencia => Imparcialidad
                    aModelDinEnf.idCompetencia = 5;
                    if (BL.ReporteClimaDinamico.isActiveCompetencia(aModelDinEnf.idCompetencia, aModelDinEnf.idEncuesta))
                        model.objImparcialidad.Data = BL.ReporteClimaDinamico.getPorcentajeAfirmativasByCompetencia(aModelDinEnf);
                    BL.ReporteClimaDinamico.addJsonReporte(aModel.idEntidad, aModel.entidadNombre, model.objImparcialidad, AnioActual, "dataImparcialidad", aHistorico.CurrentUsr, i);

                    // Competencia => Orgullo
                    aModelDinEnf.idCompetencia = 6;
                    if (BL.ReporteClimaDinamico.isActiveCompetencia(aModelDinEnf.idCompetencia, aModelDinEnf.idEncuesta))
                        model.objOrgullo.Data = BL.ReporteClimaDinamico.getPorcentajeAfirmativasByCompetencia(aModelDinEnf);
                    BL.ReporteClimaDinamico.addJsonReporte(aModel.idEntidad, aModel.entidadNombre, model.objOrgullo, AnioActual, "dataOrgullo", aHistorico.CurrentUsr, i);

                    // Competencia => Respeto
                    aModelDinEnf.idCompetencia = 7;
                    if (BL.ReporteClimaDinamico.isActiveCompetencia(aModelDinEnf.idCompetencia, aModelDinEnf.idEncuesta))
                        model.objRespeto.Data = BL.ReporteClimaDinamico.getPorcentajeAfirmativasByCompetencia(aModelDinEnf);
                    BL.ReporteClimaDinamico.addJsonReporte(aModel.idEntidad, aModel.entidadNombre, model.objRespeto, AnioActual, "dataRespeto", aHistorico.CurrentUsr, i);

                    // Competencia => Companierismo
                    aModelDinEnf.idCompetencia = 2;
                    if (BL.ReporteClimaDinamico.isActiveCompetencia(aModelDinEnf.idCompetencia, aModelDinEnf.idEncuesta))
                        model.objCompanierismo.Data = BL.ReporteClimaDinamico.getPorcentajeAfirmativasByCompetencia(aModelDinEnf);
                    BL.ReporteClimaDinamico.addJsonReporte(aModel.idEntidad, aModel.entidadNombre, model.objCompanierismo, AnioActual, "dataCompanierismo", aHistorico.CurrentUsr, i);

                    // Competencia => Coaching
                    aModelDinEnf.idCompetencia = 1;
                    if (BL.ReporteClimaDinamico.isActiveCompetencia(aModelDinEnf.idCompetencia, aModelDinEnf.idEncuesta))
                        model.objCoaching.Data = BL.ReporteClimaDinamico.getPorcentajeAfirmativasByCompetencia(aModelDinEnf);
                    BL.ReporteClimaDinamico.addJsonReporte(aModel.idEntidad, aModel.entidadNombre, model.objCoaching, AnioActual, "dataCoaching", aHistorico.CurrentUsr, i);

                    // Categoria => Habilidades Gerenciales (hace falta primero que se guarden las configuraciones)
                    aModelDinEnf.idSubCategoria = 34;
                    if (BL.ReporteClimaDinamico.isActiveCategoria(aModelDinEnf.idSubCategoria, aModelDinEnf.idEncuesta))
                        model.objHabgerenciales.Data = BL.ReporteClimaDinamico.getPorcentajeAfirmativasByCategoria(aModelDinEnf);
                    BL.ReporteClimaDinamico.addJsonReporte(aModel.idEntidad, aModel.entidadNombre, model.objHabgerenciales, AnioActual, "dataHabGerenciales", aHistorico.CurrentUsr, i);

                    // Categoria => Alineacion Estrategica (hace falta primero que se guarden las configuraciones)
                    aModelDinEnf.idSubCategoria = 11;
                    if (BL.ReporteClimaDinamico.isActiveCategoria(aModelDinEnf.idSubCategoria, aModelDinEnf.idEncuesta))
                        model.objAlineacionEstrategica.Data = BL.ReporteClimaDinamico.getPorcentajeAfirmativasByCategoria(aModelDinEnf);
                    BL.ReporteClimaDinamico.addJsonReporte(aModel.idEntidad, aModel.entidadNombre, model.objAlineacionEstrategica, AnioActual, "dataAlineacionEstrategica", aHistorico.CurrentUsr, i);

                    // Categoria => Practicas Culturales (hace falta primero que se guarden las configuraciones)
                    aModelDinEnf.idSubCategoria = 1;
                    if (BL.ReporteClimaDinamico.isActiveCategoria(aModelDinEnf.idSubCategoria, aModelDinEnf.idEncuesta))
                        model.objPracticasCulturales.Data = BL.ReporteClimaDinamico.getPorcentajeAfirmativasByCategoria(aModelDinEnf);
                    BL.ReporteClimaDinamico.addJsonReporte(aModel.idEntidad, aModel.entidadNombre, model.objPracticasCulturales, AnioActual, "dataPracticasCulturales", aHistorico.CurrentUsr, i);

                    // Competencia => Cambio
                    aModelDinEnf.idCompetencia = 8;
                    if (BL.ReporteClimaDinamico.isActiveCompetencia(aModelDinEnf.idCompetencia, aModelDinEnf.idEncuesta))
                        model.objCambio.Data = BL.ReporteClimaDinamico.getPorcentajeAfirmativasByCompetencia(aModelDinEnf);
                    BL.ReporteClimaDinamico.addJsonReporte(aModel.idEntidad, aModel.entidadNombre, model.objCambio, AnioActual, "dataCambio", aHistorico.CurrentUsr, i);

                    // Categoria => Procesos Organizacionales (hace falta primero que se guarden las configuraciones)
                    aModelDinEnf.idSubCategoria = 12;
                    if (BL.ReporteClimaDinamico.isActiveCategoria(aModelDinEnf.idSubCategoria, aModelDinEnf.idEncuesta))
                        model.objProcesosOrg.Data = BL.ReporteClimaDinamico.getPorcentajeAfirmativasByCategoria(aModelDinEnf);
                    BL.ReporteClimaDinamico.addJsonReporte(aModel.idEntidad, aModel.entidadNombre, model.objProcesosOrg, AnioActual, "dataProcesosOrg", aHistorico.CurrentUsr, i);

                    // General => Reactivos Mejor Clasificados
                    model.objMejoresEE.Data = BL.ReporteClimaDinamico.getMejores(aModelDinEnf);
                    BL.ReporteClimaDinamico.addJsonReporte(aModel.idEntidad, aModel.entidadNombre, model.objMejoresEE, AnioActual, "dataMejores", aHistorico.CurrentUsr, i);
                    
                    // General => Reactivos Peor Clasificados
                    model.objPeoresEE.Data = BL.ReporteClimaDinamico.getPeores(aModelDinEnf);
                    BL.ReporteClimaDinamico.addJsonReporte(aModel.idEntidad, aModel.entidadNombre, model.objPeoresEE, AnioActual, "dataPeores", aHistorico.CurrentUsr, i);

                    // General => Reactivos Con Mayor Crecimiento (ya aqui incluye los de menor crecimiento)
                    model.objMayorCrecimientoEE.Data = BL.ReporteClimaDinamico.getCrecimiento(aModelDinEnf);
                    BL.ReporteClimaDinamico.addJsonReporte(aModel.idEntidad, aModel.entidadNombre, model.objMayorCrecimientoEE, AnioActual, "dataCrecimiento", aHistorico.CurrentUsr, i);

                    #endregion getDataReporte parte 1

                    #region getDataReporte parte 2
                    var listPorcentajesBienestar = new List<double>();
                    /*
                     * obtener las subcategorias que conforman a bienestar
                     * obtener las preguntas de cada subcategoria
                     * iterar cada pregunta por cada entidad hija
                    */
                    
                    var subcategoriasBienestar = BL.ReporteClimaDinamico.getSubCategoriasByCategoria(30, aModelDinEnf.idEncuesta);
                    var listPreguntas = new List<int>();
                    foreach (var item in subcategoriasBienestar)
                        listPreguntas.AddRange(BL.ReporteClimaDinamico.getPreguntasBySubCategoria(item, aModelDinEnf.idEncuesta));
                    foreach (var idPregunta in listPreguntas)
                    {
                        foreach (var entidad in aFiltrosEntUnNivelAbajo)
                        {
                            ML.modelReporte modelAux = new ML.modelReporte()
                            {
                                idPregunta = idPregunta,
                                anioActual = aModelDinEnf.anioActual,
                                idTipoEntidad = BL.ReporteClimaDinamico.getIdTipoEntidadByPrefijo(entidad.type),
                                entidadNombre = entidad.value,
                                idEnfoque = aModelDinEnf.idEnfoque,
                                idEncuesta = aModelDinEnf.idEncuesta
                            };
                            listPorcentajesBienestar.Add(BL.ReporteClimaDinamico.getPorcentajeAfirmativasByIdPregunta(modelAux));
                        }
                    }
                    model.objBienestarEE.Data = listPorcentajesBienestar;
                    BL.ReporteClimaDinamico.addJsonReporte(aModel.idEntidad, aModel.entidadNombre, model.objBienestarEE, AnioActual, "dataBienestar", aHistorico.CurrentUsr, i);
                    #endregion
                    
                    #region getDataReporte parte 3
                    // comparativo por entidades del primer nivel
                    var list = new List<ML.modelComparativo66React>();
                    foreach (var item in aFiltrosEntUnNivelAbajo)
                    {
                        var modelAux = new ML.modelReporte()
                        {
                            anioActual = AnioActual,
                            idTipoEntidad = BL.ReporteClimaDinamico.getIdTipoEntidadByPrefijo(item.type),
                            entidadNombre = item.value,
                            idEnfoque = i,
                            idEncuesta = aModel.idEncuesta
                        };
                        double porcentaje = BL.ReporteClimaDinamico.getPromedio66R(modelAux);
                        ML.modelComparativo66React modelComparativo66React = new ML.modelComparativo66React
                        {
                            Entidad = modelAux.entidadNombre,
                            Frecuencia = 0,
                            HC = Convert.ToInt32(BL.ReporteClimaDinamico.getEncuestasTerminadas(modelAux)),
                            Porcentaje = porcentaje,
                            Porcentaje86React = 0,
                            tipoEntidad = modelAux.idTipoEntidad
                        };
                        list.Add(modelComparativo66React);
                    }
                    model.objComparativoEntidadesResultadoGeneralEE.Data = list;
                    BL.ReporteClimaDinamico.addJsonReporte(aModel.idEntidad, aModel.entidadNombre, model.objComparativoEntidadesResultadoGeneralEE, AnioActual, "dataComparativoEntidadesResultadoGeneral", aHistorico.CurrentUsr, i);

                    // comparativo por entidades de todos los niveles hijos
                    list = new List<ML.modelComparativo66React>();
                    foreach (var item in aFiltrosHijosEstructura)
                    {
                        var modelAux = new ML.modelReporte()
                        {
                            anioActual = AnioActual,
                            idTipoEntidad = BL.ReporteClimaDinamico.getIdTipoEntidadByPrefijo(item.type),
                            entidadNombre = item.value,
                            idEnfoque = i,
                            idEncuesta = aModel.idEncuesta
                        };
                        double porcentaje = BL.ReporteClimaDinamico.getPromedio66R(modelAux);
                        ML.modelComparativo66React modelComparativo66React = new ML.modelComparativo66React
                        {
                            Entidad = modelAux.entidadNombre,
                            Frecuencia = 0,
                            HC = Convert.ToInt32(BL.ReporteClimaDinamico.getEncuestasTerminadas(modelAux)),
                            Porcentaje = porcentaje,
                            Porcentaje86React = 0,
                            tipoEntidad = modelAux.idTipoEntidad
                        };
                        list.Add(modelComparativo66React);
                    }
                    model.objComparativoResultadoGeneralPorNivelesEE.Data = list;
                    BL.ReporteClimaDinamico.addJsonReporte(aModel.idEntidad, aModel.entidadNombre, model.objComparativoResultadoGeneralPorNivelesEE, AnioActual, "dataComparativoNiveles", aHistorico.CurrentUsr, i);
                    




                    // Demografico
                    // comparativo por antiguedad
                    list = new List<ML.modelComparativo66React>();
                    var Antiguedades = BL.ReporteClimaDinamico.getListAntiguedad(aModel.idEncuesta);
                    foreach (var item in Antiguedades)
                    {
                        var modelAux = new ML.modelReporte()
                        {
                            filtroDemografico = "rangoantiguedad",
                            anioActual = AnioActual,
                            entidadNombre = aModel.entidadNombre,
                            idEncuesta = aModel.idEncuesta,
                            idEnfoque = i,
                            idTipoEntidad = aModel.idTipoEntidad,
                            valorFiltroDemografico = item,
                            idEntidad = aModel.idEntidad,
                        };
                        double porcentaje = BL.ReporteClimaDinamico.getPromedio66R(modelAux);
                        ML.modelComparativo66React modelComparativo66React = new ML.modelComparativo66React
                        {
                            Entidad = modelAux.entidadNombre,
                            Frecuencia = 0,
                            HC = Convert.ToInt32(BL.ReporteClimaDinamico.getEncuestasTerminadas(modelAux)),
                            Porcentaje = porcentaje,
                            Porcentaje86React = 0,
                            tipoEntidad = modelAux.idTipoEntidad,
                            propiedadDemografica = item
                        };
                        list.Add(modelComparativo66React);
                    }
                    model.objComparativoPorAntiguedadEE.Data = list;
                    BL.ReporteClimaDinamico.addJsonReporte(aModel.idEntidad, aModel.entidadNombre, model.objComparativoPorAntiguedadEE, AnioActual, "dataComparativoPorAntiguedad", aHistorico.CurrentUsr, i);

                    // comparativo por genero
                    list = new List<ML.modelComparativo66React>();
                    var Generos = BL.ReporteClimaDinamico.getListGenero(aModel.idEncuesta);
                    foreach (var item in Generos)
                    {
                        var modelAux = new ML.modelReporte()
                        {
                            filtroDemografico = "sexo",
                            anioActual = AnioActual,
                            entidadNombre = aModel.entidadNombre,
                            idEncuesta = aModel.idEncuesta,
                            idEnfoque = i,
                            idTipoEntidad = aModel.idTipoEntidad,
                            valorFiltroDemografico = item,
                            idEntidad = aModel.idEntidad,
                        };
                        double porcentaje = BL.ReporteClimaDinamico.getPromedio66R(modelAux);
                        ML.modelComparativo66React modelComparativo66React = new ML.modelComparativo66React
                        {
                            Entidad = modelAux.entidadNombre,
                            Frecuencia = 0,
                            HC = Convert.ToInt32(BL.ReporteClimaDinamico.getEncuestasTerminadas(modelAux)),
                            Porcentaje = porcentaje,
                            Porcentaje86React = 0,
                            tipoEntidad = modelAux.idTipoEntidad,
                            propiedadDemografica = item
                        };
                        list.Add(modelComparativo66React);
                    }
                    model.objComparativoPorGeneroEE.Data = list;
                    BL.ReporteClimaDinamico.addJsonReporte(aModel.idEntidad, aModel.entidadNombre, model.objComparativoPorGeneroEE, AnioActual, "dataGenero", aHistorico.CurrentUsr, i);

                    // comparativo por grado academico
                    list = new List<ML.modelComparativo66React>();
                    var GradoAcademicoList = BL.ReporteClimaDinamico.getListGradoAcademico(aModel.idEncuesta);
                    foreach (var item in GradoAcademicoList)
                    {
                        var modelAux = new ML.modelReporte()
                        {
                            filtroDemografico = "gradoacademico",
                            anioActual = AnioActual,
                            entidadNombre = aModel.entidadNombre,
                            idEncuesta = aModel.idEncuesta,
                            idEnfoque = i,
                            idTipoEntidad = aModel.idTipoEntidad,
                            valorFiltroDemografico = item,
                            idEntidad = aModel.idEntidad,
                        };
                        double porcentaje = BL.ReporteClimaDinamico.getPromedio66R(modelAux);
                        ML.modelComparativo66React modelComparativo66React = new ML.modelComparativo66React
                        {
                            Entidad = modelAux.entidadNombre,
                            Frecuencia = 0,
                            HC = Convert.ToInt32(BL.ReporteClimaDinamico.getEncuestasTerminadas(modelAux)),
                            Porcentaje = porcentaje,
                            Porcentaje86React = 0,
                            tipoEntidad = modelAux.idTipoEntidad,
                            propiedadDemografica = item
                        };
                        list.Add(modelComparativo66React);
                    }
                    model.objComparativoPorGradoAcademicoEE.Data = list;
                    BL.ReporteClimaDinamico.addJsonReporte(aModel.idEntidad, aModel.entidadNombre, model.objComparativoPorGradoAcademicoEE, AnioActual, "dataAcademico", aHistorico.CurrentUsr, i);

                    // comparativo por condicion de trabajo
                    list = new List<ML.modelComparativo66React>();
                    var CondicionTrabajoList = BL.ReporteClimaDinamico.getListCondicionTrabajo(aModel.idEncuesta);
                    foreach (var item in CondicionTrabajoList)
                    {
                        var modelAux = new ML.modelReporte()
                        {
                            filtroDemografico = "condiciontrabajo",
                            anioActual = AnioActual,
                            entidadNombre = aModel.entidadNombre,
                            idEncuesta = aModel.idEncuesta,
                            idEnfoque = i,
                            idTipoEntidad = aModel.idTipoEntidad,
                            valorFiltroDemografico = item,
                            idEntidad = aModel.idEntidad,
                        };
                        double porcentaje = BL.ReporteClimaDinamico.getPromedio66R(modelAux);
                        ML.modelComparativo66React modelComparativo66React = new ML.modelComparativo66React
                        {
                            Entidad = modelAux.entidadNombre,
                            Frecuencia = 0,
                            HC = Convert.ToInt32(BL.ReporteClimaDinamico.getEncuestasTerminadas(modelAux)),
                            Porcentaje = porcentaje,
                            Porcentaje86React = 0,
                            tipoEntidad = modelAux.idTipoEntidad,
                            propiedadDemografica = item
                        };
                        list.Add(modelComparativo66React);
                    }
                    model.objComparativoPorCondicionTrabajoEE.Data = list;
                    BL.ReporteClimaDinamico.addJsonReporte(aModel.idEntidad, aModel.entidadNombre, model.objComparativoPorCondicionTrabajoEE, AnioActual, "dataCondicionTra", aHistorico.CurrentUsr, i);

                    // comparativo por funcion
                    list = new List<ML.modelComparativo66React>();
                    var FuncionList = BL.ReporteClimaDinamico.getListPerfil(aModel.idEncuesta);
                    foreach (var item in FuncionList)
                    {
                        var modelAux = new ML.modelReporte()
                        {
                            filtroDemografico = "tipofuncion",
                            anioActual = AnioActual,
                            entidadNombre = aModel.entidadNombre,
                            idEncuesta = aModel.idEncuesta,
                            idEnfoque = i,
                            idTipoEntidad = aModel.idTipoEntidad,
                            valorFiltroDemografico = item,
                            idEntidad = aModel.idEntidad,
                        };
                        double porcentaje = BL.ReporteClimaDinamico.getPromedio66R(modelAux);
                        ML.modelComparativo66React modelComparativo66React = new ML.modelComparativo66React
                        {
                            Entidad = modelAux.entidadNombre,
                            Frecuencia = 0,
                            HC = Convert.ToInt32(BL.ReporteClimaDinamico.getEncuestasTerminadas(modelAux)),
                            Porcentaje = porcentaje,
                            Porcentaje86React = 0,
                            tipoEntidad = modelAux.idTipoEntidad,
                            propiedadDemografica = item
                        };
                        list.Add(modelComparativo66React);
                    }
                    model.objComparativoPorFuncionEE.Data = list;
                    BL.ReporteClimaDinamico.addJsonReporte(aModel.idEntidad, aModel.entidadNombre, model.objComparativoPorFuncionEE, AnioActual, "dataFuncion", aHistorico.CurrentUsr, i);

                    // comparativo por edad
                    list = new List<ML.modelComparativo66React>();
                    var edadesList = BL.ReporteClimaDinamico.getListEdad(aModel.idEncuesta);
                    foreach (var item in edadesList)
                    {
                        var modelAux = new ML.modelReporte()
                        {
                            filtroDemografico = "rangoedad",
                            anioActual = AnioActual,
                            entidadNombre = aModel.entidadNombre,
                            idEncuesta = aModel.idEncuesta,
                            idEnfoque = i,
                            idTipoEntidad = aModel.idTipoEntidad,
                            valorFiltroDemografico = item,
                            idEntidad = aModel.idEntidad,
                        };
                        double porcentaje = BL.ReporteClimaDinamico.getPromedio66R(modelAux);
                        ML.modelComparativo66React modelComparativo66React = new ML.modelComparativo66React
                        {
                            Entidad = modelAux.entidadNombre,
                            Frecuencia = 0,
                            HC = Convert.ToInt32(BL.ReporteClimaDinamico.getEncuestasTerminadas(modelAux)),
                            Porcentaje = porcentaje,
                            Porcentaje86React = 0,
                            tipoEntidad = modelAux.idTipoEntidad,
                            propiedadDemografica = item
                        };
                        list.Add(modelComparativo66React);
                    }
                    model.objComparativoPorRangoEdadEE.Data = list;
                    BL.ReporteClimaDinamico.addJsonReporte(aModel.idEntidad, aModel.entidadNombre, model.objComparativoPorRangoEdadEE, AnioActual, "dataEdad", aHistorico.CurrentUsr, i);


                    #endregion
                }

                #region seccion 4
                // Resultados que no dependen de enfoque
                model.objEsperadas.Data = BL.ReporteClimaDinamico.getEncuestasEsperadas(aModel);
                model.objParticipacion.Data = BL.ReporteClimaDinamico.getPorcentajeParticipacion(aModel);
                BL.ReporteClimaDinamico.addJsonReporte(aModel.idEntidad, aModel.entidadNombre, model.objEsperadas, AnioActual, "dataEsperadas", aHistorico.CurrentUsr, 3);

                // data permanencia
                aModel.idCompetencia = 13;
                model.objPermanencia.Data = BL.ReporteClimaDinamico.getDataPermanenciaAbandono(aModel);
                BL.ReporteClimaDinamico.addJsonReporte(aModel.idEntidad, aModel.entidadNombre, model.objPermanencia, AnioActual, "dataPermanencia", aHistorico.CurrentUsr, 3);


                // data abandono
                aModel.idCompetencia = 15;
                model.objAbandono.Data = BL.ReporteClimaDinamico.getDataPermanenciaAbandono(aModel);
                BL.ReporteClimaDinamico.addJsonReporte(aModel.idEntidad, aModel.entidadNombre, model.objAbandono, AnioActual, "dataAbandono", aHistorico.CurrentUsr, 3);

                // comparativo permanencia
                var listCompara = new List<ML.modelPermanenciaAbandono>();
                foreach (var item in aFiltrosEntUnNivelAbajo)
                {
                    var modelAux = new ML.modelReporte()
                    {
                        idCompetencia = 13,
                        anioActual = aModel.anioActual,
                        idTipoEntidad = BL.ReporteClimaDinamico.getIdTipoEntidadByPrefijo(item.type),
                        entidadNombre = item.value,
                        idEnfoque = 1,
                        idEncuesta = aModel.idEncuesta
                    };
                    listCompara.AddRange(BL.ReporteClimaDinamico.getDataPermanenciaAbandono(modelAux));
                }
                model.objComparativoPermanencia.Data = listCompara;
                BL.ReporteClimaDinamico.addJsonReporte(aModel.idEntidad, aModel.entidadNombre, model.objComparativoPermanencia, AnioActual, "dataComparativoPermanencia", aHistorico.CurrentUsr, 3);

                // comparativo abandono
                listCompara = new List<ML.modelPermanenciaAbandono>();
                foreach (var item in aFiltrosEntUnNivelAbajo)
                {
                    var modelAux = new ML.modelReporte()
                    {
                        idCompetencia = 15,
                        anioActual = AnioActual,
                        idTipoEntidad = BL.ReporteClimaDinamico.getIdTipoEntidadByPrefijo(item.type),
                        entidadNombre = item.value,
                        idEnfoque = 1,
                        idEntidad = 0,
                        idEncuesta = aModel.idEncuesta
                    };
                    listCompara.AddRange(BL.ReporteClimaDinamico.getDataPermanenciaAbandono(modelAux));
                }
                model.objComparativoAbandono.Data = listCompara;
                BL.ReporteClimaDinamico.addJsonReporte(aModel.idEntidad, aModel.entidadNombre, model.objComparativoAbandono, AnioActual, "dataComparativoAbandono", aHistorico.CurrentUsr, 3);
                /*
                 * para guardar si el enfoque es 2 los guarda como data EA y si es 1 data EE
                 * model.obj*** para que grabe la estrucura json
                */
                #endregion seccion 4

                st.Stop();
                Console.WriteLine(st.Elapsed.TotalSeconds);
                Console.WriteLine(st.Elapsed.TotalMinutes);
                BL.ReporteClimaDinamico.print_r<Models.ReporteClimaDinamico>(model, new StackTrace(), st.Elapsed.TotalMinutes);

                // poner activo el reporte
                BL.ReporteClimaDinamico.updateStatusReporte(aModel.idEntidad, aModel.entidadNombre, AnioActual, aHistorico.CurrentUsr);
                // BL.LogReporteoClima.sendMail(aHistorico.EntidadNombre, (int)aHistorico.EntidadId, AnioActual, aHistorico.CurrentUsr, aHistorico.currentURL);
                BL.LogReporteoClima.writteLogJobReporte("He terminado de generar el reporte", new System.Diagnostics.StackTrace(), aHistorico.CurrentUsr, aModel.idTipoEntidad, aModel.entidadNombre);
                return new JsonResult();
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return Json(aE);
            }
        }


        /// <summary>
        /// obtiene el nivel inmediato inferior para pintar el grid de bienestar
        /// </summary>
        /// <param name="unidadNegocio"></param>
        /// <param name="criterioSeleccionado"></param>
        /// <param name="entidadId"></param>
        /// <param name="entidadNombre"></param>
        /// <returns>Listado de strings de las entidades de un nivel inferior</returns>
        public static List<ML.finalCols> getFiltros(int unidadNegocio, int? criterioSeleccionado, int? entidadId, string entidadNombre)
        {
            int _entidadId = Convert.ToInt32(entidadId);
            BackGroundJobController s = new BackGroundJobController();
            var estructura = new List<object>();
            switch (criterioSeleccionado)
            {
                case 1:
                    estructura = GetEstructura(new ML.CompanyCategoria { IdCompanyCategoria = unidadNegocio });
                    break;
                case 2:
                    estructura = GetEstructura_lvl_2(new ML.Company { CompanyId = entidadId });
                    break;
                case 3:
                    estructura = GetEstructura_lvl_3(new ML.Area { IdArea = _entidadId });
                    break;
                case 4:
                    estructura = GetEstructura_lvl_4(new ML.Departamento { IdDepartamento = _entidadId });
                    break;
                default:
                    break;
            }
            var finalColumnas = new List<ML.finalCols>();
            foreach (ML.Company item in estructura)
            {
                if (!String.IsNullOrEmpty(item.CompanyName) && criterioSeleccionado == 1)
                {
                    finalColumnas.Add(new ML.finalCols { type = "Comp=>", value = item.CompanyName });
                }
                if (!String.IsNullOrEmpty(item.Area.Nombre) && criterioSeleccionado == 2)
                {
                    finalColumnas.Add(new ML.finalCols { type = "Area=>", value = item.Area.Nombre });
                }
                if (!String.IsNullOrEmpty(item.Area.Departamento.Nombre) && criterioSeleccionado == 3)
                {
                    finalColumnas.Add(new ML.finalCols { type = "Dpto=>", value = item.Area.Departamento.Nombre });
                }
                if (!String.IsNullOrEmpty(item.Area.Departamento.Subdepartamento.Nombre) && criterioSeleccionado == 4)
                {
                    finalColumnas.Add(new ML.finalCols { type = "SubD=>", value = item.Area.Departamento.Subdepartamento.Nombre });
                }
            }
            var ColumnasByCompanyCategoria = new List<ML.finalCols>();
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
                    BL.NLogGeneratorFile.logError("Metodo getFiltros en el ReporteClimaDinamicoController, no se encontró un caso coincidente", new StackTrace());
                    break;
            }
            var arrayStringFiltros = new List<string>();
            foreach (var item in ColumnasByCompanyCategoria)
            {
                arrayStringFiltros.Add(item.value);
            }
            // return arrayStringFiltros;
            return ColumnasByCompanyCategoria;
        }

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

        /// <summary>
        /// obtiene el listado de empresas en base a una unidad de negocio
        /// </summary>
        /// <param name="CompanyCateg"></param>
        /// <returns></returns>
        public static List<object> GetEstructura(ML.CompanyCategoria CompanyCateg)
        {
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
                                //Validar area
                                string Area = Convert.ToString(Session["Area"]);
                                if (Area != item.NOMBRE_AREA)
                                {
                                    company.Area.Nombre = item.NOMBRE_AREA;
                                    Session["Area"] = item.NOMBRE_AREA == null ? "" : item.NOMBRE_AREA;
                                }
                                //Validar Departamento
                                string Departamento = Convert.ToString(Session["Departamento"]);
                                if (Departamento != item.NOMBRE_DEPARTAMENTO)
                                {
                                    company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                    Session["Departamento"] = item.NOMBRE_DEPARTAMENTO == null ? "" : item.NOMBRE_DEPARTAMENTO;
                                }
                                //Validar subdepartamento
                                string Subdepartamento = Convert.ToString(Session["Subdepartamento"]);
                                if (Subdepartamento != item.NOMBRE_SUBDEPARTAMENTO)
                                {
                                    company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                    Session["Subdepartamento"] = item.NOMBRE_SUBDEPARTAMENTO == null ? "" : item.NOMBRE_SUBDEPARTAMENTO;
                                }
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
                                //Validar area
                                string Area = Convert.ToString(Session["Area"]);
                                if (Area != item.NOMBRE_AREA)
                                {
                                    company.Area.Nombre = item.NOMBRE_AREA;
                                    Session["Area"] = item.NOMBRE_AREA == null ? "" : item.NOMBRE_AREA;
                                }
                                //Validar Departamento
                                string Departamento = Convert.ToString(Session["Departamento"]);
                                if (Departamento != item.NOMBRE_DEPARTAMENTO)
                                {
                                    company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                    Session["Departamento"] = item.NOMBRE_DEPARTAMENTO == null ? "" : item.NOMBRE_DEPARTAMENTO;
                                }
                                //Validar subdepartamento
                                string Subdepartamento = Convert.ToString(Session["Subdepartamento"]);
                                if (Subdepartamento != item.NOMBRE_SUBDEPARTAMENTO)
                                {
                                    company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                    Session["Subdepartamento"] = item.NOMBRE_SUBDEPARTAMENTO == null ? "" : item.NOMBRE_SUBDEPARTAMENTO;
                                }
                                result.Objects.Add(company);
                                result.Correct = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BL.NLogGeneratorFile.logError(ex, new StackTrace());
                return new List<object>();
            }
            return result.Objects;
        }

        /// <summary>
        /// obtiene el listado de areas en base a una empresa
        /// </summary>
        /// <param name="aCompany"></param>
        /// <returns></returns>
        public static List<object> GetEstructura_lvl_2(ML.Company aCompany)
        {
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
                                //Validar area
                                string Area = Convert.ToString(aSession["Area"]);
                                if (Area != item.NOMBRE_AREA)
                                {
                                    company.Area.Nombre = item.NOMBRE_AREA;
                                    aSession["Area"] = item.NOMBRE_AREA == null ? "" : item.NOMBRE_AREA;
                                }
                                //Validar Departamento
                                string Departamento = Convert.ToString(aSession["Departamento"]);
                                if (Departamento != item.NOMBRE_DEPARTAMENTO)
                                {
                                    company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                    aSession["Departamento"] = item.NOMBRE_DEPARTAMENTO == null ? "" : item.NOMBRE_DEPARTAMENTO;
                                }
                                //Validar subdepartamento
                                string Subdepartamento = Convert.ToString(aSession["Subdepartamento"]);
                                if (Subdepartamento != item.NOMBRE_SUBDEPARTAMENTO)
                                {
                                    company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                    aSession["Subdepartamento"] = item.NOMBRE_SUBDEPARTAMENTO == null ? "" : item.NOMBRE_SUBDEPARTAMENTO;
                                }
                                result.Objects.Add(company);
                                result.Correct = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BL.NLogGeneratorFile.logError(ex, new StackTrace());
            }
            return result.Objects;
        }

        /// <summary>
        /// obtiene el listado de departamentos en base a una area
        /// </summary>
        /// <param name="aArea"></param>
        /// <returns></returns>
        public static List<object> GetEstructura_lvl_3(ML.Area aArea)
        {
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

                                company.Area.IdArea = Convert.ToInt32(item.ID_AREA);
                                company.Area.Departamento.IdDepartamento = Convert.ToInt32(item.ID_DEPARTAMENTO);
                                company.Area.Departamento.Subdepartamento.IdSubdepartamento = Convert.ToInt32(item.ID_SUBDEPARTAMENTO);

                                //Validar area
                                string Area = Convert.ToString(aSession["Area"]);
                                if (Area != item.NOMBRE_AREA)
                                {
                                    company.Area.Nombre = item.NOMBRE_AREA;
                                    aSession["Area"] = item.NOMBRE_AREA == null ? "" : item.NOMBRE_AREA;
                                }
                                //Validar Departamento
                                string Departamento = Convert.ToString(aSession["Departamento"]);
                                if (Departamento != item.NOMBRE_DEPARTAMENTO)
                                {
                                    company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                    aSession["Departamento"] = item.NOMBRE_DEPARTAMENTO == null ? "" : item.NOMBRE_DEPARTAMENTO;
                                }
                                //Validar subdepartamento
                                string Subdepartamento = Convert.ToString(aSession["Subdepartamento"]);
                                if (Subdepartamento != item.NOMBRE_SUBDEPARTAMENTO)
                                {
                                    company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                    aSession["Subdepartamento"] = item.NOMBRE_SUBDEPARTAMENTO == null ? "" : item.NOMBRE_SUBDEPARTAMENTO;
                                }
                                result.Objects.Add(company);
                                result.Correct = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BL.NLogGeneratorFile.logError(ex, new StackTrace());
            }
            return result.Objects;
        }

        /// <summary>
        /// obtiene el listado de subdepartamentos en base a un departamento
        /// </summary>
        /// <param name="aDepartamento"></param>
        /// <returns></returns>
        public static List<object> GetEstructura_lvl_4(ML.Departamento aDepartamento)
        {
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

                                company.Area.Departamento.IdDepartamento = Convert.ToInt32(item.ID_DEPARTAMENTO);
                                company.Area.Departamento.Subdepartamento.IdSubdepartamento = Convert.ToInt32(item.ID_SUBDEPARTAMENTO);

                                //Validar Departamento
                                string Departamento = Convert.ToString(aSession["Departamento"]);
                                if (Departamento != item.NOMBRE_DEPARTAMENTO)
                                {
                                    company.Area.Departamento.Nombre = item.NOMBRE_DEPARTAMENTO;
                                    aSession["Departamento"] = item.NOMBRE_DEPARTAMENTO == null ? "" : item.NOMBRE_DEPARTAMENTO;
                                }
                                //Validar subdepartamento
                                string Subdepartamento = Convert.ToString(aSession["Subdepartamento"]);
                                if (Subdepartamento != item.NOMBRE_SUBDEPARTAMENTO)
                                {
                                    company.Area.Departamento.Subdepartamento.Nombre = item.NOMBRE_SUBDEPARTAMENTO;
                                    aSession["Subdepartamento"] = item.NOMBRE_SUBDEPARTAMENTO == null ? "" : item.NOMBRE_SUBDEPARTAMENTO;
                                }
                                result.Objects.Add(company);
                                result.Correct = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BL.NLogGeneratorFile.logError(ex, new StackTrace());
            }
            return result.Objects;
        }
    }
}