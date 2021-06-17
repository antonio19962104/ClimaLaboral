using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class HistoricoClimaLaboral
    {
        /*public static string Create(List<ML.Historico> aHistorico, object currentUser)
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            int aux = 0;
                            foreach (ML.Historico item in aHistorico)
                            {
                                var model = Mapping_Historicos(item, currentUser);
                                if (model != null)
                                {
                                    var maxId = context.Historico.Max<DL.Historico, int>(o => o.IdHistorico) + aux;
                                    var save = Save(model, context, transaction, maxId);
                                    if (!String.IsNullOrEmpty(save))
                                    {
                                        return save;
                                    }
                                }
                                else
                                {
                                    BL.LogReporteoClima.writteLog("Error al crear el modelo para guardar EmpleadoRespuestas masivamente", new StackTrace());
                                    return "Error al crear el modelo para guardar EmpleadoRespuestas masivamente";
                                }
                                aux = aux + 1;
                            }
                        }
                        catch (Exception aE)
                        {
                            transaction.Rollback();
                            BL.LogReporteoClima.writteLog(aE, new StackTrace());
                            return aE.Message;
                        }
                        context.SaveChanges();
                        transaction.Commit();
                        return "Los históricos fueron guardados correctamente";
                    }
                }
            }
            catch (Exception aE)
            {
                BL.LogReporteoClima.writteLog(aE, new StackTrace());
                return aE.Message;
            }
        }*/
        public static string Create(List<ML.HistoricoClima> aHistorico, object currentUser)
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            int aux = 0;
                            foreach (ML.HistoricoClima item in aHistorico)
                            {
                                var model = Mapping_Historicos(item, currentUser);
                                if (model != null)
                                {
                                    var maxId = context.HistoricoClima.Max<DL.HistoricoClima, int>(o => o.IdHistorico) + aux;
                                    var save = Save(model, context, transaction, maxId);
                                    if (!String.IsNullOrEmpty(save))
                                    {
                                        return save;
                                    }
                                }
                                else
                                {
                                    BL.LogReporteoClima.writteLog("Error al crear el modelo para guardar EmpleadoRespuestas masivamente", new StackTrace());
                                    return "Error al crear el modelo para guardar EmpleadoRespuestas masivamente";
                                }
                                aux = aux + 1;
                            }
                        }
                        catch (Exception aE)
                        {
                            transaction.Rollback();
                            BL.LogReporteoClima.writteLog(aE, new StackTrace());
                            return aE.Message;
                        }
                        context.SaveChanges();
                        transaction.Commit();
                        return "Los históricos fueron guardados correctamente";
                    }
                }
            }
            catch (Exception aE)
            {
                BL.LogReporteoClima.writteLog(aE, new StackTrace());
                return aE.Message;
            }
        }
        /*public static DL.Historico Mapping_Historicos(ML.Historico aHistorico, object currentUser)
        {
            var model = new DL.Historico();
            try
            {
                #region crear modelo DL
                model = new DL.Historico
                {
                    //EE y generales
                    anio = aHistorico.Anio,
                    alineacionEstrategicaEE = aHistorico.AlineacionEstrategicaEE,
                    calificacionGlobalEE = aHistorico.CalificacionGlobalEE,
                    coachingEE = aHistorico.CoachingEE,
                    compañerismoEE = aHistorico.CompanierismoEE,
                    creedibilidadEE = aHistorico.CreedibilidadEE,
                    entidadId = aHistorico.EntidadId,
                    entidadNombre = aHistorico.EntidadNombre,
                    habilidadesGerencialesEE = aHistorico.HabilidadesGerencialesEE,
                    hc = aHistorico.HC,
                    idTipoEntidad = aHistorico.IdTipoEntidad,
                    imparcialidadEE = aHistorico.ImparcialidadEE,
                    manejoDelCambioEE = aHistorico.ManejoDelCambioEE,
                    nivelColaboracionEE = aHistorico.NivelColaboracionEE,
                    nivelCompromisoEE = aHistorico.NivelCompromisoEE,
                    nivelConfianzaEE = aHistorico.NivelConfianzaEE,
                    nivelParticipacionEE = aHistorico.NivelParticipacionEE,
                    orgulloEE = aHistorico.OrgulloEE,
                    practicasCulturalesEE = aHistorico.PracticasCulturalesEE,
                    procesosOrganizacionalesEE = aHistorico.ProcesosOrganizacionalesEE,
                    respetoEE = aHistorico.RespetoEE,
                    entidad = aHistorico.Entidad,
                    //EA
                    respetoEA = aHistorico.RespetoEA,
                    procesosOrganizacionalesEA = aHistorico.ProcesosOrganizacionalesEA,
                    practicasCulturalesEA = aHistorico.PracticasCulturalesEA,
                    orgulloEA = aHistorico.OrgulloEA,
                    nivelConfianzaEA = aHistorico.NivelConfianzaEA,
                    nivelCompromisoEA = aHistorico.NivelCompromisoEA,
                    alineacionEstrategicaEA = aHistorico.AlineacionEstrategicaEA,
                    calificacionGlobalEA = aHistorico.CalificacionGlobalEA,
                    coachingEA = aHistorico.CoachingEA,
                    compañerismoEA = aHistorico.CompanierismoEA,
                    creedibilidadEA = aHistorico.CreedibilidadEA,
                    habilidadesGerencialesEA = aHistorico.HabilidadesGerencialesEA,
                    imparcialidadEA = aHistorico.ImparcialidadEA,
                    manejoDelCambioEA = aHistorico.ManejoDelCambioEA,
                    nivelColaboracionEA = aHistorico.NivelColaboracionEA,


                    Preg1EE = aHistorico.Preg1EE,
                    Preg2EE = aHistorico.Preg2EE,
                    Preg3EE = aHistorico.Preg3EE,
                    Preg4EE = aHistorico.Preg4EE,
                    Preg5EE = aHistorico.Preg5EE,
                    Preg6EE = aHistorico.Preg6EE,
                    Preg7EE = aHistorico.Preg7EE,
                    Preg8EE = aHistorico.Preg8EE,
                    Preg9EE = aHistorico.Preg9EE,
                    Preg10EE = aHistorico.Preg10EE,
                    Preg11EE = aHistorico.Preg11EE,
                    Preg12EE = aHistorico.Preg12EE,
                    Preg13EE = aHistorico.Preg13EE,
                    Preg14EE = aHistorico.Preg14EE,
                    Preg15EE = aHistorico.Preg15EE,
                    Preg16EE = aHistorico.Preg16EE,
                    Preg17EE = aHistorico.Preg17EE,
                    Preg18EE = aHistorico.Preg18EE,
                    Preg19EE = aHistorico.Preg19EE,
                    Preg20EE = aHistorico.Preg20EE,
                    Preg21EE = aHistorico.Preg21EE,
                    Preg22EE = aHistorico.Preg22EE,
                    Preg23EE = aHistorico.Preg23EE,
                    Preg24EE = aHistorico.Preg24EE,
                    Preg25EE = aHistorico.Preg25EE,
                    Preg26EE = aHistorico.Preg26EE,
                    Preg27EE = aHistorico.Preg27EE,
                    Preg28EE = aHistorico.Preg28EE,
                    Preg29EE = aHistorico.Preg29EE,
                    Preg30EE = aHistorico.Preg30EE,
                    Preg31EE = aHistorico.Preg31EE,
                    Preg32EE = aHistorico.Preg32EE,
                    Preg33EE = aHistorico.Preg33EE,
                    Preg34EE = aHistorico.Preg34EE,
                    Preg35EE = aHistorico.Preg35EE,
                    Preg36EE = aHistorico.Preg36EE,
                    Preg37EE = aHistorico.Preg37EE,
                    Preg38EE = aHistorico.Preg38EE,
                    Preg39EE = aHistorico.Preg39EE,
                    Preg40EE = aHistorico.Preg40EE,
                    Preg41EE = aHistorico.Preg41EE,
                    Preg42EE = aHistorico.Preg42EE,
                    Preg43EE = aHistorico.Preg43EE,
                    Preg44EE = aHistorico.Preg44EE,
                    Preg45EE = aHistorico.Preg45EE,
                    Preg46EE = aHistorico.Preg46EE,
                    Preg47EE = aHistorico.Preg47EE,
                    Preg48EE = aHistorico.Preg48EE,
                    Preg49EE = aHistorico.Preg49EE,
                    Preg50EE = aHistorico.Preg50EE,
                    Preg51EE = aHistorico.Preg51EE,
                    Preg52EE = aHistorico.Preg52EE,
                    Preg53EE = aHistorico.Preg53EE,
                    Preg54EE = aHistorico.Preg54EE,
                    Preg55EE = aHistorico.Preg55EE,
                    Preg56EE = aHistorico.Preg56EE,
                    Preg57EE = aHistorico.Preg57EE,
                    Preg58EE = aHistorico.Preg58EE,
                    Preg59EE = aHistorico.Preg59EE,
                    Preg60EE = aHistorico.Preg60EE,
                    Preg61EE = aHistorico.Preg61EE,
                    Preg62EE = aHistorico.Preg62EE,
                    Preg63EE = aHistorico.Preg63EE,
                    Preg64EE = aHistorico.Preg64EE,
                    Preg65EE = aHistorico.Preg65EE,
                    Preg66EE = aHistorico.Preg66EE,
                    Preg67EE = aHistorico.Preg67EE,
                    Preg68EE = aHistorico.Preg68EE,
                    Preg69EE = aHistorico.Preg69EE,
                    Preg70EE = aHistorico.Preg70EE,
                    Preg71EE = aHistorico.Preg71EE,
                    Preg72EE = aHistorico.Preg72EE,
                    Preg73EE = aHistorico.Preg73EE,
                    Preg74EE = aHistorico.Preg74EE,
                    Preg75EE = aHistorico.Preg75EE,
                    Preg76EE = aHistorico.Preg76EE,
                    Preg77EE = aHistorico.Preg77EE,
                    Preg78EE = aHistorico.Preg78EE,
                    Preg79EE = aHistorico.Preg79EE,
                    Preg80EE = aHistorico.Preg80EE,
                    Preg81EE = aHistorico.Preg81EE,
                    Preg82EE = aHistorico.Preg82EE,
                    Preg83EE = aHistorico.Preg83EE,
                    Preg84EE = aHistorico.Preg84EE,
                    Preg85EE = aHistorico.Preg85EE,
                    Preg86EE = aHistorico.Preg86EE,

                    Preg1EA = aHistorico.Preg1EA,
                    Preg2EA = aHistorico.Preg2EA,
                    Preg3EA = aHistorico.Preg3EA,
                    Preg4EA = aHistorico.Preg4EA,
                    Preg5EA = aHistorico.Preg5EA,
                    Preg6EA = aHistorico.Preg6EA,
                    Preg7EA = aHistorico.Preg7EA,
                    Preg8EA = aHistorico.Preg8EA,
                    Preg9EA = aHistorico.Preg9EA,
                    Preg10EA = aHistorico.Preg10EA,
                    Preg11EA = aHistorico.Preg11EA,
                    Preg12EA = aHistorico.Preg12EA,
                    Preg13EA = aHistorico.Preg13EA,
                    Preg14EA = aHistorico.Preg14EA,
                    Preg15EA = aHistorico.Preg15EA,
                    Preg16EA = aHistorico.Preg16EA,
                    Preg17EA = aHistorico.Preg17EA,
                    Preg18EA = aHistorico.Preg18EA,
                    Preg19EA = aHistorico.Preg19EA,
                    Preg20EA = aHistorico.Preg20EA,
                    Preg21EA = aHistorico.Preg21EA,
                    Preg22EA = aHistorico.Preg22EA,
                    Preg23EA = aHistorico.Preg23EA,
                    Preg24EA = aHistorico.Preg24EA,
                    Preg25EA = aHistorico.Preg25EA,
                    Preg26EA = aHistorico.Preg26EA,
                    Preg27EA = aHistorico.Preg27EA,
                    Preg28EA = aHistorico.Preg28EA,
                    Preg29EA = aHistorico.Preg29EA,
                    Preg30EA = aHistorico.Preg30EA,
                    Preg31EA = aHistorico.Preg31EA,
                    Preg32EA = aHistorico.Preg32EA,
                    Preg33EA = aHistorico.Preg33EA,
                    Preg34EA = aHistorico.Preg34EA,
                    Preg35EA = aHistorico.Preg35EA,
                    Preg36EA = aHistorico.Preg36EA,
                    Preg37EA = aHistorico.Preg37EA,
                    Preg38EA = aHistorico.Preg38EA,
                    Preg39EA = aHistorico.Preg39EA,
                    Preg40EA = aHistorico.Preg40EA,
                    Preg41EA = aHistorico.Preg41EA,
                    Preg42EA = aHistorico.Preg42EA,
                    Preg43EA = aHistorico.Preg43EA,
                    Preg44EA = aHistorico.Preg44EA,
                    Preg45EA = aHistorico.Preg45EA,
                    Preg46EA = aHistorico.Preg46EA,
                    Preg47EA = aHistorico.Preg47EA,
                    Preg48EA = aHistorico.Preg48EA,
                    Preg49EA = aHistorico.Preg49EA,
                    Preg50EA = aHistorico.Preg50EA,
                    Preg51EA = aHistorico.Preg51EA,
                    Preg52EA = aHistorico.Preg52EA,
                    Preg53EA = aHistorico.Preg53EA,
                    Preg54EA = aHistorico.Preg54EA,
                    Preg55EA = aHistorico.Preg55EA,
                    Preg56EA = aHistorico.Preg56EA,
                    Preg57EA = aHistorico.Preg57EA,
                    Preg58EA = aHistorico.Preg58EA,
                    Preg59EA = aHistorico.Preg59EA,
                    Preg60EA = aHistorico.Preg60EA,
                    Preg61EA = aHistorico.Preg61EA,
                    Preg62EA = aHistorico.Preg62EA,
                    Preg63EA = aHistorico.Preg63EA,
                    Preg64EA = aHistorico.Preg64EA,
                    Preg65EA = aHistorico.Preg65EA,
                    Preg66EA = aHistorico.Preg66EA,
                    Preg67EA = aHistorico.Preg67EA,
                    Preg68EA = aHistorico.Preg68EA,
                    Preg69EA = aHistorico.Preg69EA,
                    Preg70EA = aHistorico.Preg70EA,
                    Preg71EA = aHistorico.Preg71EA,
                    Preg72EA = aHistorico.Preg72EA,
                    Preg73EA = aHistorico.Preg73EA,
                    Preg74EA = aHistorico.Preg74EA,
                    Preg75EA = aHistorico.Preg75EA,
                    Preg76EA = aHistorico.Preg76EA,
                    Preg77EA = aHistorico.Preg77EA,
                    Preg78EA = aHistorico.Preg78EA,
                    Preg79EA = aHistorico.Preg79EA,
                    Preg80EA = aHistorico.Preg80EA,
                    Preg81EA = aHistorico.Preg81EA,
                    Preg82EA = aHistorico.Preg82EA,
                    Preg83EA = aHistorico.Preg83EA,
                    Preg84EA = aHistorico.Preg84EA,
                    Preg85EA = aHistorico.Preg85EA,
                    Preg86EA = aHistorico.Preg86EA,
                    //Asignar demograficos
                    CGEA_Ant_6_a_10_anios = aHistorico.CGEA_Ant_6_a_10_anios,
                    CGEA_Ant_menos_de_6_meses = aHistorico.CGEA_Ant_menos_de_6_meses,
                    CGEE_Ant_6_a_10_anios = aHistorico.CGEE_Ant_6_a_10_anios,
                    CGEE_Ant_menos_de_6_meses = aHistorico.CGEE_Ant_menos_de_6_meses,
                    CG_EA_18_A_22_ANIOS = aHistorico.CG_EA_18_A_22_ANIOS,
                    CG_EA_23_A_31_ANIOS = aHistorico.CG_EA_23_A_31_ANIOS,
                    CG_EA_32_A_39_ANIOS = aHistorico.CG_EA_32_A_39_ANIOS,
                    CG_EA_40_A_55_ANIOS = aHistorico.CG_EA_40_A_55_ANIOS,
                    CG_EA_56_ANIOS_O_MAS = aHistorico.CG_EA_56_ANIOS_O_MAS,
                    CG_EA_Administrativo = aHistorico.CG_EA_Administrativo,
                    CG_EA_Ant_1_a_2_anios = aHistorico.CG_EA_Ant_1_a_2_anios,
                    CG_EA_Ant_3_a_5_anios = aHistorico.CG_EA_Ant_3_a_5_anios,
                    CG_EA_Ant_6_meses_1_anio = aHistorico.CG_EA_Ant_6_meses_1_anio,
                    CG_EA_Ant_mas_de_10_anios = aHistorico.CG_EA_Ant_mas_de_10_anios,
                    CG_EA_Comercial = aHistorico.CG_EA_Comercial,
                    CG_EA_Comisionistas = aHistorico.CG_EA_Comisionistas,
                    CG_EA_COORDINADOR_SUPERVISOR_JEFE = aHistorico.CG_EA_COORDINADOR_SUPERVISOR_JEFE,
                    CG_EA_Director = aHistorico.CG_EA_Director,
                    CG_EA_GerenteDepartamental = aHistorico.CG_EA_GerenteDepartamental,
                    CG_EA_GerenteGeneral = aHistorico.CG_EA_GerenteGeneral,
                    CG_EA_Honorarios = aHistorico.CG_EA_Honorarios,
                    CG_EA_Media_Superior = aHistorico.CG_EA_Media_Superior,
                    CG_EA_Media_Tecnica = aHistorico.CG_EA_Media_Tecnica,
                    CG_EA_Planta = aHistorico.CG_EA_Planta,
                    CG_EA_PostGrado = aHistorico.CG_EA_PostGrado,
                    CG_EA_Primaria = aHistorico.CG_EA_Primaria,
                    CG_EA_Secundaria = aHistorico.CG_EA_Secundaria,
                    CG_EA_Sexo_Femenino = aHistorico.CG_EA_Sexo_Femenino,
                    CG_EA_Sexo_Masculino = aHistorico.CG_EA_Sexo_Masculino,
                    CG_EA_Sindicalizado  = aHistorico.CG_EA_Sindicalizado,
                    CG_EA_Subgerente = aHistorico.CG_EA_Subgerente,
                    CG_EA_TECNICO_OPERATIVO = aHistorico.CG_EA_TECNICO_OPERATIVO,
                    CG_EA_Temporal = aHistorico.CG_EA_Temporal,
                    CG_EA_Universidad_Completa = aHistorico.CG_EA_Universidad_Completa,
                    CG_EA_Universidad_Incompleta = aHistorico.CG_EA_Universidad_Incompleta,
                    CG_EE_18_A_22_ANIOS = aHistorico.CG_EE_18_A_22_ANIOS,
                    CG_EE_23_A_31_ANIOS = aHistorico.CG_EE_23_A_31_ANIOS,
                    CG_EE_32_A_39_ANIOS = aHistorico.CG_EE_32_A_39_ANIOS,
                    CG_EE_40_A_55_ANIOS = aHistorico.CG_EE_40_A_55_ANIOS,
                    CG_EE_56_ANIOS_O_MAS = aHistorico.CG_EE_56_ANIOS_O_MAS,
                    CG_EE_Administrativo = aHistorico.CG_EE_Administrativo,
                    CG_EE_Ant_1_a_2_anios = aHistorico.CG_EE_Ant_1_a_2_anios,
                    CG_EE_Ant_3_a_5_anios = aHistorico.CG_EE_Ant_3_a_5_anios,
                    CG_EE_Ant_6_meses_1_anio = aHistorico.CG_EE_Ant_6_meses_1_anio,
                    CG_EE_Ant_mas_de_10_anios = aHistorico.CG_EE_Ant_mas_de_10_anios,
                    CG_EE_Comercial = aHistorico.CG_EE_Comercial,
                    CG_EE_Comisionistas = aHistorico.CG_EE_Comisionistas,
                    CG_EE_COORDINADOR_SUPERVISOR_JEFE = aHistorico.CG_EE_COORDINADOR_SUPERVISOR_JEFE,
                    CG_EE_Director = aHistorico.CG_EE_Director,
                    CG_EE_GerenteDepartamental = aHistorico.CG_EE_GerenteDepartamental,
                    CG_EE_GerenteGeneral = aHistorico.CG_EE_GerenteGeneral,
                    CG_EE_Honorarios = aHistorico.CG_EE_Honorarios,
                    CG_EE_Media_Superior = aHistorico.CG_EE_Media_Superior,
                    CG_EE_Media_Tecnica = aHistorico.CG_EE_Media_Tecnica,
                    CG_EE_Planta = aHistorico.CG_EE_Planta,
                    CG_EE_PostGrado = aHistorico.CG_EE_PostGrado,
                    CG_EE_Primaria = aHistorico.CG_EE_Primaria,
                    CG_EE_Secundaria = aHistorico.CG_EE_Secundaria,
                    CG_EE_TECNICO_OPERATIVO = aHistorico.CG_EE_TECNICO_OPERATIVO,
                    CG_EE_Sexo_Femenino = aHistorico.CG_EE_Sexo_Femenino,
                    CG_EE_Sexo_Masculino = aHistorico.CG_EE_Sexo_Masculino,
                    CG_EE_Sindicalizado = aHistorico.CG_EE_Sindicalizado,
                    CG_EE_Subgerente = aHistorico.CG_EE_Subgerente,
                    CG_EE_Temporal = aHistorico.CG_EE_Temporal,
                    CG_EE_Universidad_Completa = aHistorico.CG_EE_Universidad_Completa,
                    CG_EE_Universidad_Incompleta = aHistorico.CG_EE_Universidad_Incompleta
                };
                #endregion crear modelo DL
            }
            catch (Exception aE)
            {
                BL.LogReporteoClima.writteLog(aE, new StackTrace());
                return null;
            }
            return model;
        }*/
        public static DL.HistoricoClima Mapping_Historicos(ML.HistoricoClima aHistorico, object currentUser)
        {
            var model = new DL.HistoricoClima();
            try
            {
                /*Recrear mapeo*/
                #region crear modelo DL
                model = new DL.HistoricoClima
                {
                    // y generales
                    Anio = aHistorico.Anio,
                    AlineacionEstrategica = aHistorico.AlineacionEstrategica,
                    CalificacionGlobal = aHistorico.CalificacionGlobal,
                    Coaching = aHistorico.Coaching,
                    Companierismo = aHistorico.Companierismo,
                    Creedibilidad = aHistorico.Creedibilidad,
                    EntidadId = aHistorico.EntidadId,
                    EntidadNombre = aHistorico.EntidadNombre,
                    HabilidadesGerenciales = aHistorico.HabilidadesGerenciales,
                    HC = aHistorico.Contestadas,
                    IdTipoEntidad = aHistorico.IdTipoEntidad,
                    Imparcialidad = aHistorico.Imparcialidad,
                    ManejoDelCambio = aHistorico.ManejoDelCambio,
                    NivelColaboracion = aHistorico.NivelColaboracion,
                    
                    NivelCompromiso = aHistorico.NivelCompromiso,
                    NivelConfianza = aHistorico.NivelConfianza,
                    NivelParticipacion = aHistorico.NivelParticipacion,
                    Orgullo = aHistorico.Orgullo,
                    PracticasCulturales = aHistorico.PracticasCulturales,
                    ProcesosOrganizacionales = aHistorico.ProcesosOrganizacionales,
                    Respeto = aHistorico.Respeto,
                    Entidad = aHistorico.Entidad,
                    
                    Bienestar = aHistorico.Bienestar,
                    Bio = aHistorico.Bio,
                    Psico = aHistorico.Psico,
                    Social = aHistorico.Social,
                    //


                    Preg1 = aHistorico.Preg1,
                    Preg2 = aHistorico.Preg2,
                    Preg3 = aHistorico.Preg3,
                    Preg4 = aHistorico.Preg4,
                    Preg5 = aHistorico.Preg5,
                    Preg6 = aHistorico.Preg6,
                    Preg7 = aHistorico.Preg7,
                    Preg8 = aHistorico.Preg8,
                    Preg9 = aHistorico.Preg9,
                    Preg10 = aHistorico.Preg10,
                    Preg11 = aHistorico.Preg11,
                    Preg12 = aHistorico.Preg12,
                    Preg13 = aHistorico.Preg13,
                    Preg14 = aHistorico.Preg14,
                    Preg15 = aHistorico.Preg15,
                    Preg16 = aHistorico.Preg16,
                    Preg17 = aHistorico.Preg17,
                    Preg18 = aHistorico.Preg18,
                    Preg19 = aHistorico.Preg19,
                    Preg20 = aHistorico.Preg20,
                    Preg21 = aHistorico.Preg21,
                    Preg22 = aHistorico.Preg22,
                    Preg23 = aHistorico.Preg23,
                    Preg24 = aHistorico.Preg24,
                    Preg25 = aHistorico.Preg25,
                    Preg26 = aHistorico.Preg26,
                    Preg27 = aHistorico.Preg27,
                    Preg28 = aHistorico.Preg28,
                    Preg29 = aHistorico.Preg29,
                    Preg30 = aHistorico.Preg30,
                    Preg31 = aHistorico.Preg31,
                    Preg32 = aHistorico.Preg32,
                    Preg33 = aHistorico.Preg33,
                    Preg34 = aHistorico.Preg34,
                    Preg35 = aHistorico.Preg35,
                    Preg36 = aHistorico.Preg36,
                    Preg37 = aHistorico.Preg37,
                    Preg38 = aHistorico.Preg38,
                    Preg39 = aHistorico.Preg39,
                    Preg40 = aHistorico.Preg40,
                    Preg41 = aHistorico.Preg41,
                    Preg42 = aHistorico.Preg42,
                    Preg43 = aHistorico.Preg43,
                    Preg44 = aHistorico.Preg44,
                    Preg45 = aHistorico.Preg45,
                    Preg46 = aHistorico.Preg46,
                    Preg47 = aHistorico.Preg47,
                    Preg48 = aHistorico.Preg48,
                    Preg49 = aHistorico.Preg49,
                    Preg50 = aHistorico.Preg50,
                    Preg51 = aHistorico.Preg51,
                    Preg52 = aHistorico.Preg52,
                    Preg53 = aHistorico.Preg53,
                    Preg54 = aHistorico.Preg54,
                    Preg55 = aHistorico.Preg55,
                    Preg56 = aHistorico.Preg56,
                    Preg57 = aHistorico.Preg57,
                    Preg58 = aHistorico.Preg58,
                    Preg59 = aHistorico.Preg59,
                    Preg60 = aHistorico.Preg60,
                    Preg61 = aHistorico.Preg61,
                    Preg62 = aHistorico.Preg62,
                    Preg63 = aHistorico.Preg63,
                    Preg64 = aHistorico.Preg64,
                    Preg65 = aHistorico.Preg65,
                    Preg66 = aHistorico.Preg66,
                    Preg67 = aHistorico.Preg67,
                    Preg68 = aHistorico.Preg68,
                    Preg69 = aHistorico.Preg69,
                    Preg70 = aHistorico.Preg70,
                    Preg71 = aHistorico.Preg71,
                    Preg72 = aHistorico.Preg72,
                    Preg73 = aHistorico.Preg73,
                    Preg74 = aHistorico.Preg74,
                    Preg75 = aHistorico.Preg75,
                    Preg76 = aHistorico.Preg76,
                    Preg77 = aHistorico.Preg77,
                    Preg78 = aHistorico.Preg78,
                    Preg79 = aHistorico.Preg79,
                    Preg80 = aHistorico.Preg80,
                    Preg81 = aHistorico.Preg81,
                    Preg82 = aHistorico.Preg82,
                    Preg83 = aHistorico.Preg83,
                    Preg84 = aHistorico.Preg84,
                    Preg85 = aHistorico.Preg85,
                    Preg86 = aHistorico.Preg86,


                    //Asignar demograficos
                    Ant_6_a_10_anios = aHistorico.Ant_6_a_10_anios.Promedio66R + "_" + aHistorico.Ant_6_a_10_anios.Promedio86R + "_" + aHistorico.Ant_6_a_10_anios.Contestadas,
                    Ant_menos_de_6_meses = aHistorico.Ant_menos_de_6_meses.Promedio66R + "_" + aHistorico.Ant_menos_de_6_meses.Promedio86R + "_" + aHistorico.Ant_menos_de_6_meses.Contestadas,
                    Edad_18_A_22_ANIOS = aHistorico.Edad_18_A_22_ANIOS.Promedio66R + "_" + aHistorico.Edad_18_A_22_ANIOS.Promedio86R + "_" + aHistorico.Edad_18_A_22_ANIOS.Contestadas,
                    Edad_23_A_31_ANIOS = aHistorico.Edad_23_A_31_ANIOS.Promedio66R + "_" + aHistorico.Edad_23_A_31_ANIOS.Promedio86R + "_" + aHistorico.Edad_23_A_31_ANIOS.Contestadas,
                    Edad_32_A_39_ANIOS = aHistorico.Edad_32_A_39_ANIOS.Promedio66R + "_" + aHistorico.Edad_32_A_39_ANIOS.Promedio86R + "_" + aHistorico.Edad_32_A_39_ANIOS.Contestadas,
                    Edad_40_A_55_ANIOS = aHistorico.Edad_40_A_55_ANIOS.Promedio66R + "_" + aHistorico.Edad_40_A_55_ANIOS.Promedio86R + "_" + aHistorico.Edad_40_A_55_ANIOS.Contestadas,
                    Edad_56_ANIOS_O_MAS = aHistorico.Edad_56_ANIOS_O_MAS.Promedio66R + "_" + aHistorico.Edad_56_ANIOS_O_MAS.Promedio86R + "_" + aHistorico.Edad_56_ANIOS_O_MAS.Contestadas,
                    Administrativo = aHistorico.Administrativo.Promedio66R + "_" + aHistorico.Administrativo.Promedio86R + "_" + aHistorico.Administrativo.Contestadas,
                    Ant_1_a_2_anios = aHistorico.Ant_1_a_2_anios.Promedio66R + "_" + aHistorico.Ant_1_a_2_anios.Promedio86R + "_" + aHistorico.Ant_1_a_2_anios.Contestadas,
                    Ant_3_a_5_anios = aHistorico.Ant_3_a_5_anios.Promedio66R + "_" + aHistorico.Ant_3_a_5_anios.Promedio86R + "_" + aHistorico.Ant_3_a_5_anios.Contestadas,
                    Ant_6_meses_1_anio = aHistorico.Ant_6_meses_1_anio.Promedio66R + "_" + aHistorico.Ant_6_meses_1_anio.Promedio86R + "_" + aHistorico.Ant_6_meses_1_anio.Contestadas,
                    Ant_mas_de_10_anios = aHistorico.Ant_mas_de_10_anios.Promedio66R + "_" + aHistorico.Ant_mas_de_10_anios.Promedio86R + "_" + aHistorico.Ant_mas_de_10_anios.Contestadas,
                    Comercial = aHistorico.Comercial.Promedio66R + "_" + aHistorico.Comercial.Promedio86R + "_" + aHistorico.Comercial.Contestadas,
                    Comisionistas = aHistorico.Comisionistas.Promedio66R + "_" + aHistorico.Comisionistas.Promedio86R + "_" + aHistorico.Comisionistas.Contestadas,
                    COORDINADOR_SUPERVISOR_JEFE = aHistorico.COORDINADOR_SUPERVISOR_JEFE.Promedio66R + "_" + aHistorico.COORDINADOR_SUPERVISOR_JEFE.Promedio86R + "_" + aHistorico.COORDINADOR_SUPERVISOR_JEFE.Contestadas,
                    Director = aHistorico.Director.Promedio66R + "_" + aHistorico.Director.Promedio86R + "_" + aHistorico.Director.Contestadas,
                    GerenteDepartamental = aHistorico.GerenteDepartamental.Promedio66R + "_" + aHistorico.GerenteDepartamental.Promedio86R + "_" + aHistorico.GerenteDepartamental.Contestadas,
                    GerenteGeneral = aHistorico.GerenteGeneral.Promedio66R + "_" + aHistorico.GerenteGeneral.Promedio86R + "_" + aHistorico.GerenteGeneral.Contestadas,
                    Honorarios = aHistorico.Honorarios.Promedio66R + "_" + aHistorico.Honorarios.Promedio86R + "_" + aHistorico.Honorarios.Contestadas,
                    Media_Superior = aHistorico.Media_Superior.Promedio66R + "_" + aHistorico.Media_Superior.Promedio86R + "_" + aHistorico.Media_Superior.Contestadas,
                    Media_Tecnica = aHistorico.Media_Tecnica.Promedio66R + "_" + aHistorico.Media_Tecnica.Promedio86R + "_" + aHistorico.Media_Tecnica.Contestadas,
                    Planta = aHistorico.Planta.Promedio66R + "_" + aHistorico.Planta.Promedio86R + "_" + aHistorico.Planta.Contestadas,
                    PostGrado = aHistorico.PostGrado.Promedio66R + "_" + aHistorico.PostGrado.Promedio86R + "_" + aHistorico.PostGrado.Contestadas,
                    Primaria = aHistorico.Primaria.Promedio66R + "_" + aHistorico.Primaria.Promedio86R + "_" + aHistorico.Primaria.Contestadas,
                    Secundaria = aHistorico.Secundaria.Promedio66R + "_" + aHistorico.Secundaria.Promedio86R + "_" + aHistorico.Secundaria.Contestadas,
                    Sexo_Femenino = aHistorico.Sexo_Femenino.Promedio66R + "_" + aHistorico.Sexo_Femenino.Promedio86R + "_" + aHistorico.Sexo_Femenino.Contestadas,
                    Sexo_Masculino = aHistorico.Sexo_Masculino.Promedio66R + "_" + aHistorico.Sexo_Masculino.Promedio86R + "_" + aHistorico.Sexo_Masculino.Contestadas,
                    Sindicalizado = aHistorico.Sindicalizado.Promedio66R + "_" + aHistorico.Sindicalizado.Promedio86R + "_" + aHistorico.Sindicalizado.Contestadas,
                    Subgerente = aHistorico.Subgerente.Promedio66R + "_" + aHistorico.Subgerente.Promedio86R + "_" + aHistorico.Subgerente.Contestadas,
                    TECNICO_OPERATIVO = aHistorico.TECNICO_OPERATIVO.Promedio66R + "_" + aHistorico.TECNICO_OPERATIVO.Promedio86R + "_" + aHistorico.TECNICO_OPERATIVO.Contestadas,
                    Temporal = aHistorico.Temporal.Promedio66R + "_" + aHistorico.Temporal.Promedio86R + "_" + aHistorico.Temporal.Contestadas,
                    Universidad_Completa = aHistorico.Universidad_Completa.Promedio66R + "_" + aHistorico.Universidad_Completa.Promedio86R + "_" + aHistorico.Universidad_Completa.Contestadas,
                    Universidad_Incompleta = aHistorico.Universidad_Incompleta.Promedio66R + "_" + aHistorico.Universidad_Incompleta.Promedio86R + "_" + aHistorico.Universidad_Incompleta.Contestadas,
                    Contestadas = aHistorico.Contestadas,
                    Enfoque = aHistorico.Enfoque == 1 ? "Enfoque empresa" : aHistorico.Enfoque == 2 ? "Enfoque Area" : "",
                    Esperadas = aHistorico.Esperadas,
                    Promedio66R = aHistorico.Promedio66R,
                    Promedio86R = aHistorico.Promedio86R
                    //GerenteGeneral = aHistorico.GerenteGeneral
                };
                #endregion crear modelo DL

            }
            catch (Exception aE)
            {
                BL.LogReporteoClima.writteLog(aE, new StackTrace());
                return null;
            }
            return model;
        }
        public static DL.HistoricoClima Mapping_Historicos_FromReporte(ML.HistoricoClima aHistorico, object currentUser)
        {
            var model = new DL.HistoricoClima();
            try
            {
                /*Recrear mapeo*/
                #region crear modelo DL
                model = new DL.HistoricoClima
                {
                    // y generales
                    Anio = aHistorico.Anio,
                    AlineacionEstrategica = aHistorico.AlineacionEstrategica,
                    CalificacionGlobal = aHistorico.CalificacionGlobal,
                    Coaching = aHistorico.Coaching,
                    Companierismo = aHistorico.Companierismo,
                    Creedibilidad = aHistorico.Creedibilidad,
                    EntidadId = aHistorico.EntidadId,
                    EntidadNombre = aHistorico.EntidadNombre,
                    HabilidadesGerenciales = aHistorico.HabilidadesGerenciales,
                    HC = aHistorico.HC,
                    IdTipoEntidad = aHistorico.IdTipoEntidad,
                    Imparcialidad = aHistorico.Imparcialidad,
                    ManejoDelCambio = aHistorico.ManejoDelCambio,
                    NivelColaboracion = aHistorico.NivelColaboracion,

                    NivelCompromiso = aHistorico.NivelCompromiso,
                    NivelConfianza = aHistorico.NivelConfianza,
                    NivelParticipacion = aHistorico.NivelParticipacion,
                    Orgullo = aHistorico.Orgullo,
                    PracticasCulturales = aHistorico.PracticasCulturales,
                    ProcesosOrganizacionales = aHistorico.ProcesosOrganizacionales,
                    Respeto = aHistorico.Respeto,
                    Entidad = aHistorico.Entidad,

                    Bienestar = aHistorico.Bienestar,
                    Bio = aHistorico.Bio,
                    Psico = aHistorico.Psico,
                    Social = aHistorico.Social,
                    //


                    Preg1 = aHistorico.Preg1,
                    Preg2 = aHistorico.Preg2,
                    Preg3 = aHistorico.Preg3,
                    Preg4 = aHistorico.Preg4,
                    Preg5 = aHistorico.Preg5,
                    Preg6 = aHistorico.Preg6,
                    Preg7 = aHistorico.Preg7,
                    Preg8 = aHistorico.Preg8,
                    Preg9 = aHistorico.Preg9,
                    Preg10 = aHistorico.Preg10,
                    Preg11 = aHistorico.Preg11,
                    Preg12 = aHistorico.Preg12,
                    Preg13 = aHistorico.Preg13,
                    Preg14 = aHistorico.Preg14,
                    Preg15 = aHistorico.Preg15,
                    Preg16 = aHistorico.Preg16,
                    Preg17 = aHistorico.Preg17,
                    Preg18 = aHistorico.Preg18,
                    Preg19 = aHistorico.Preg19,
                    Preg20 = aHistorico.Preg20,
                    Preg21 = aHistorico.Preg21,
                    Preg22 = aHistorico.Preg22,
                    Preg23 = aHistorico.Preg23,
                    Preg24 = aHistorico.Preg24,
                    Preg25 = aHistorico.Preg25,
                    Preg26 = aHistorico.Preg26,
                    Preg27 = aHistorico.Preg27,
                    Preg28 = aHistorico.Preg28,
                    Preg29 = aHistorico.Preg29,
                    Preg30 = aHistorico.Preg30,
                    Preg31 = aHistorico.Preg31,
                    Preg32 = aHistorico.Preg32,
                    Preg33 = aHistorico.Preg33,
                    Preg34 = aHistorico.Preg34,
                    Preg35 = aHistorico.Preg35,
                    Preg36 = aHistorico.Preg36,
                    Preg37 = aHistorico.Preg37,
                    Preg38 = aHistorico.Preg38,
                    Preg39 = aHistorico.Preg39,
                    Preg40 = aHistorico.Preg40,
                    Preg41 = aHistorico.Preg41,
                    Preg42 = aHistorico.Preg42,
                    Preg43 = aHistorico.Preg43,
                    Preg44 = aHistorico.Preg44,
                    Preg45 = aHistorico.Preg45,
                    Preg46 = aHistorico.Preg46,
                    Preg47 = aHistorico.Preg47,
                    Preg48 = aHistorico.Preg48,
                    Preg49 = aHistorico.Preg49,
                    Preg50 = aHistorico.Preg50,
                    Preg51 = aHistorico.Preg51,
                    Preg52 = aHistorico.Preg52,
                    Preg53 = aHistorico.Preg53,
                    Preg54 = aHistorico.Preg54,
                    Preg55 = aHistorico.Preg55,
                    Preg56 = aHistorico.Preg56,
                    Preg57 = aHistorico.Preg57,
                    Preg58 = aHistorico.Preg58,
                    Preg59 = aHistorico.Preg59,
                    Preg60 = aHistorico.Preg60,
                    Preg61 = aHistorico.Preg61,
                    Preg62 = aHistorico.Preg62,
                    Preg63 = aHistorico.Preg63,
                    Preg64 = aHistorico.Preg64,
                    Preg65 = aHistorico.Preg65,
                    Preg66 = aHistorico.Preg66,
                    Preg67 = aHistorico.Preg67,
                    Preg68 = aHistorico.Preg68,
                    Preg69 = aHistorico.Preg69,
                    Preg70 = aHistorico.Preg70,
                    Preg71 = aHistorico.Preg71,
                    Preg72 = aHistorico.Preg72,
                    Preg73 = aHistorico.Preg73,
                    Preg74 = aHistorico.Preg74,
                    Preg75 = aHistorico.Preg75,
                    Preg76 = aHistorico.Preg76,
                    Preg77 = aHistorico.Preg77,
                    Preg78 = aHistorico.Preg78,
                    Preg79 = aHistorico.Preg79,
                    Preg80 = aHistorico.Preg80,
                    Preg81 = aHistorico.Preg81,
                    Preg82 = aHistorico.Preg82,
                    Preg83 = aHistorico.Preg83,
                    Preg84 = aHistorico.Preg84,
                    Preg85 = aHistorico.Preg85,
                    Preg86 = aHistorico.Preg86,


                    //Asignar demograficos
                    Ant_6_a_10_anios = aHistorico.Ant_6_a_10_anios.Promedio66R + "_" + aHistorico.Ant_6_a_10_anios.Promedio86R + "_" + aHistorico.Ant_6_a_10_anios.Contestadas,
                    Ant_menos_de_6_meses = aHistorico.Ant_menos_de_6_meses.Promedio66R + "_" + aHistorico.Ant_menos_de_6_meses.Promedio86R + "_" + aHistorico.Ant_menos_de_6_meses.Contestadas,
                    Edad_18_A_22_ANIOS = aHistorico.Edad_18_A_22_ANIOS.Promedio66R + "_" + aHistorico.Edad_18_A_22_ANIOS.Promedio86R + "_" + aHistorico.Edad_18_A_22_ANIOS.Contestadas,
                    Edad_23_A_31_ANIOS = aHistorico.Edad_23_A_31_ANIOS.Promedio66R + "_" + aHistorico.Edad_23_A_31_ANIOS.Promedio86R + "_" + aHistorico.Edad_23_A_31_ANIOS.Contestadas,
                    Edad_32_A_39_ANIOS = aHistorico.Edad_32_A_39_ANIOS.Promedio66R + "_" + aHistorico.Edad_32_A_39_ANIOS.Promedio86R + "_" + aHistorico.Edad_32_A_39_ANIOS.Contestadas,
                    Edad_40_A_55_ANIOS = aHistorico.Edad_40_A_55_ANIOS.Promedio66R + "_" + aHistorico.Edad_40_A_55_ANIOS.Promedio86R + "_" + aHistorico.Edad_40_A_55_ANIOS.Contestadas,
                    Edad_56_ANIOS_O_MAS = aHistorico.Edad_56_ANIOS_O_MAS.Promedio66R + "_" + aHistorico.Edad_56_ANIOS_O_MAS.Promedio86R + "_" + aHistorico.Edad_56_ANIOS_O_MAS.Contestadas,
                    Administrativo = aHistorico.Administrativo.Promedio66R + "_" + aHistorico.Administrativo.Promedio86R + "_" + aHistorico.Administrativo.Contestadas,
                    Ant_1_a_2_anios = aHistorico.Ant_1_a_2_anios.Promedio66R + "_" + aHistorico.Ant_1_a_2_anios.Promedio86R + "_" + aHistorico.Ant_1_a_2_anios.Contestadas,
                    Ant_3_a_5_anios = aHistorico.Ant_3_a_5_anios.Promedio66R + "_" + aHistorico.Ant_3_a_5_anios.Promedio86R + "_" + aHistorico.Ant_3_a_5_anios.Contestadas,
                    Ant_6_meses_1_anio = aHistorico.Ant_6_meses_1_anio.Promedio66R + "_" + aHistorico.Ant_6_meses_1_anio.Promedio86R + "_" + aHistorico.Ant_6_meses_1_anio.Contestadas,
                    Ant_mas_de_10_anios = aHistorico.Ant_mas_de_10_anios.Promedio66R + "_" + aHistorico.Ant_mas_de_10_anios.Promedio86R + "_" + aHistorico.Ant_mas_de_10_anios.Contestadas,
                    Comercial = aHistorico.Comercial.Promedio66R + "_" + aHistorico.Comercial.Promedio86R + "_" + aHistorico.Comercial.Contestadas,
                    Comisionistas = aHistorico.Comisionistas.Promedio66R + "_" + aHistorico.Comisionistas.Promedio86R + "_" + aHistorico.Comisionistas.Contestadas,
                    COORDINADOR_SUPERVISOR_JEFE = aHistorico.COORDINADOR_SUPERVISOR_JEFE.Promedio66R + "_" + aHistorico.COORDINADOR_SUPERVISOR_JEFE.Promedio86R + "_" + aHistorico.COORDINADOR_SUPERVISOR_JEFE.Contestadas,
                    Director = aHistorico.Director.Promedio66R + "_" + aHistorico.Director.Promedio86R + "_" + aHistorico.Director.Contestadas,
                    GerenteDepartamental = aHistorico.GerenteDepartamental.Promedio66R + "_" + aHistorico.GerenteDepartamental.Promedio86R + "_" + aHistorico.GerenteDepartamental.Contestadas,
                    GerenteGeneral = aHistorico.GerenteGeneral.Promedio66R + "_" + aHistorico.GerenteGeneral.Promedio86R + "_" + aHistorico.GerenteGeneral.Contestadas,
                    Honorarios = aHistorico.Honorarios.Promedio66R + "_" + aHistorico.Honorarios.Promedio86R + "_" + aHistorico.Honorarios.Contestadas,
                    Media_Superior = aHistorico.Media_Superior.Promedio66R + "_" + aHistorico.Media_Superior.Promedio86R + "_" + aHistorico.Media_Superior.Contestadas,
                    Media_Tecnica = aHistorico.Media_Tecnica.Promedio66R + "_" + aHistorico.Media_Tecnica.Promedio86R + "_" + aHistorico.Media_Tecnica.Contestadas,
                    Planta = aHistorico.Planta.Promedio66R + "_" + aHistorico.Planta.Promedio86R + "_" + aHistorico.Planta.Contestadas,
                    PostGrado = aHistorico.PostGrado.Promedio66R + "_" + aHistorico.PostGrado.Promedio86R + "_" + aHistorico.PostGrado.Contestadas,
                    Primaria = aHistorico.Primaria.Promedio66R + "_" + aHistorico.Primaria.Promedio86R + "_" + aHistorico.Primaria.Contestadas,
                    Secundaria = aHistorico.Secundaria.Promedio66R + "_" + aHistorico.Secundaria.Promedio86R + "_" + aHistorico.Secundaria.Contestadas,
                    Sexo_Femenino = aHistorico.Sexo_Femenino.Promedio66R + "_" + aHistorico.Sexo_Femenino.Promedio86R + "_" + aHistorico.Sexo_Femenino.Contestadas,
                    Sexo_Masculino = aHistorico.Sexo_Masculino.Promedio66R + "_" + aHistorico.Sexo_Masculino.Promedio86R + "_" + aHistorico.Sexo_Masculino.Contestadas,
                    Sindicalizado = aHistorico.Sindicalizado.Promedio66R + "_" + aHistorico.Sindicalizado.Promedio86R + "_" + aHistorico.Sindicalizado.Contestadas,
                    Subgerente = aHistorico.Subgerente.Promedio66R + "_" + aHistorico.Subgerente.Promedio86R + "_" + aHistorico.Subgerente.Contestadas,
                    TECNICO_OPERATIVO = aHistorico.TECNICO_OPERATIVO.Promedio66R + "_" + aHistorico.TECNICO_OPERATIVO.Promedio86R + "_" + aHistorico.TECNICO_OPERATIVO.Contestadas,
                    Temporal = aHistorico.Temporal.Promedio66R + "_" + aHistorico.Temporal.Promedio86R + "_" + aHistorico.Temporal.Contestadas,
                    Universidad_Completa = aHistorico.Universidad_Completa.Promedio66R + "_" + aHistorico.Universidad_Completa.Promedio86R + "_" + aHistorico.Universidad_Completa.Contestadas,
                    Universidad_Incompleta = aHistorico.Universidad_Incompleta.Promedio66R + "_" + aHistorico.Universidad_Incompleta.Promedio86R + "_" + aHistorico.Universidad_Incompleta.Contestadas,
                    Contestadas = aHistorico.HC,
                    Enfoque = aHistorico.Enfoque == 1 ? "Enfoque empresa" : aHistorico.Enfoque == 2 ? "Enfoque Area" : "",
                    Esperadas = aHistorico.Esperadas,
                    Promedio66R = aHistorico.Promedio66R,
                    Promedio86R = aHistorico.Promedio86R
                    //GerenteGeneral = aHistorico.GerenteGeneral
                };
                #endregion crear modelo DL

            }
            catch (Exception aE)
            {
                BL.LogReporteoClima.writteLog(aE, new StackTrace());
                return null;
            }
            return model;
        }
        /*public static string Save(DL.Historico aHistorico, DL.RH_DesEntities aContext, System.Data.Entity.DbContextTransaction aTransaction, int maxId)
        {
            try
            {
                try
                {
                    aHistorico.IdHistorico = maxId + 1;
                    aContext.Historico.Add(aHistorico);
                }
                catch (Exception ex)
                {
                    aHistorico.IdHistorico = 0 + 1;
                    aContext.Historico.Add(aHistorico);
                    BL.LogReporteoClima.writteLog(ex, new StackTrace());
                }
            }
            catch (Exception aE)
            {
                BL.LogReporteoClima.writteLog(aE, new StackTrace());
                return "Error" + aE.Message;
            }
            return "";
        }*/

        public static string Save(DL.HistoricoClima aHistorico, DL.RH_DesEntities aContext, System.Data.Entity.DbContextTransaction aTransaction, int maxId)
        {
            try
            {
                try
                {
                    aHistorico.IdHistorico = maxId + 1;
                    aContext.HistoricoClima.Add(aHistorico);
                }
                catch (Exception ex)
                {
                    aHistorico.IdHistorico = 0 + 1;
                    aContext.HistoricoClima.Add(aHistorico);
                    BL.LogReporteoClima.writteLog(ex, new StackTrace());
                }
            }
            catch (Exception aE)
            {
                BL.LogReporteoClima.writteLog(aE, new StackTrace());
                return "Error" + aE.Message;
            }
            return "";
        }

        #region apis 
        /*public static bool existeHistorico(ML.Historico aHistorico)
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var exists = context.Historico.Select(o => new { o.anio, o.entidadId, o.entidadNombre }).Where(o => o.anio == aHistorico.Anio && o.entidadId == aHistorico.EntidadId && o.entidadNombre == aHistorico.EntidadNombre).ToList();
                    if (exists.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception aE)
            {
                BL.LogReporteoClima.writteLog(aE, new StackTrace());
                return false;
            }
        }*/
        public static bool existeHistorico_2(ML.Historico aHistorico)
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var existsEE = context.HistoricoClima.Select(o => new { o.Anio, o.EntidadId, o.EntidadNombre, o.Enfoque }).Where(o => o.Anio == aHistorico.Anio && o.EntidadId == aHistorico.EntidadId && o.EntidadNombre == aHistorico.EntidadNombre && o.Enfoque == "Enfoque empresa").ToList();
                    var existsEA = context.HistoricoClima.Select(o => new { o.Anio, o.EntidadId, o.EntidadNombre, o.Enfoque }).Where(o => o.Anio == aHistorico.Anio && o.EntidadId == aHistorico.EntidadId && o.EntidadNombre == aHistorico.EntidadNombre && o.Enfoque == "Enfoque Area").ToList();
                    if (existsEE.Count > 0 && existsEA.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception aE)
            {
                BL.LogReporteoClima.writteLog(aE, new StackTrace());
                return false;
            }
        }
        /*public static List<ML.Historico> getHistorico(ML.Historico aHistorico)
        {
            var list = new List<ML.Historico>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Historico.Select(o => o)
                        .Where(o => o.entidadId == aHistorico.EntidadId && o.entidadNombre == aHistorico.EntidadNombre && o.anio == aHistorico.Anio).ToList();
                    
                    if (query != null)
                    {
                        if (query.Count > 0)
                        {
                            foreach (var item in query)
                            {
                                #region llenar objeto
                                ML.Historico historico = new ML.Historico()
                                {
                                    AlineacionEstrategicaEE = item.alineacionEstrategicaEE,
                                    Anio = item.anio,
                                    CalificacionGlobalEE = item.calificacionGlobalEE,
                                    CoachingEE = item.coachingEE,
                                    CompanierismoEE = item.compañerismoEE,
                                    CreedibilidadEE = item.creedibilidadEE,
                                    Entidad = item.entidad,
                                    EntidadId = (int)item.entidadId,
                                    EntidadNombre = item.entidadNombre,
                                    IdTipoEntidad = item.idTipoEntidad,
                                    HabilidadesGerencialesEE = item.habilidadesGerencialesEE,
                                    HC = item.hc,
                                    IdHistorico = item.IdHistorico,
                                    ImparcialidadEE = item.imparcialidadEE,
                                    ManejoDelCambioEE = item.manejoDelCambioEE,
                                    NivelColaboracionEE = item.nivelColaboracionEE,
                                    NivelCompromisoEE = item.nivelCompromisoEE,
                                    NivelConfianzaEE = item.nivelConfianzaEE,
                                    NivelParticipacionEE = item.nivelParticipacionEE,
                                    OrgulloEE = item.orgulloEE,
                                    PracticasCulturalesEE = item.practicasCulturalesEE,
                                    ProcesosOrganizacionalesEE = item.procesosOrganizacionalesEE,
                                    RespetoEE = item.respetoEE,
                                    //EA
                                    AlineacionEstrategicaEA = (decimal)item.alineacionEstrategicaEA,
                                    CalificacionGlobalEA = (decimal)item.calificacionGlobalEA,
                                    CoachingEA = (decimal)item.coachingEA,
                                    CompanierismoEA = (decimal)item.compañerismoEA,
                                    CreedibilidadEA = (decimal)item.creedibilidadEA,
                                    HabilidadesGerencialesEA = (decimal)item.habilidadesGerencialesEA,
                                    ImparcialidadEA = (decimal)item.imparcialidadEA,
                                    ManejoDelCambioEA = (decimal)item.manejoDelCambioEA,
                                    NivelColaboracionEA = (decimal)item.nivelColaboracionEA,
                                    NivelCompromisoEA = (decimal)item.nivelCompromisoEA,
                                    NivelConfianzaEA = (decimal)item.nivelConfianzaEA,
                                    OrgulloEA = (decimal)item.orgulloEA,
                                    PracticasCulturalesEA = (decimal)item.practicasCulturalesEA,
                                    ProcesosOrganizacionalesEA = (decimal)item.procesosOrganizacionalesEA,
                                    RespetoEA = (decimal)item.respetoEA,
                                    Preg1EE = item.Preg1EE,
                                    Preg2EE = item.Preg2EE,
                                    Preg3EE = item.Preg3EE,
                                    Preg4EE = item.Preg4EE,
                                    Preg5EE = item.Preg5EE,
                                    Preg6EE = item.Preg6EE,
                                    Preg7EE = item.Preg7EE,
                                    Preg8EE = item.Preg8EE,
                                    Preg9EE = item.Preg9EE,
                                    Preg10EE = item.Preg10EE,
                                    Preg11EE = item.Preg11EE,
                                    Preg12EE = item.Preg12EE,
                                    Preg13EE = item.Preg13EE,
                                    Preg14EE = item.Preg14EE,
                                    Preg15EE = item.Preg15EE,
                                    Preg16EE = item.Preg16EE,
                                    Preg17EE = item.Preg17EE,
                                    Preg18EE = item.Preg18EE,
                                    Preg19EE = item.Preg19EE,
                                    Preg20EE = item.Preg20EE,
                                    Preg21EE = item.Preg21EE,
                                    Preg22EE = item.Preg22EE,
                                    Preg23EE = item.Preg23EE,
                                    Preg24EE = item.Preg24EE,
                                    Preg25EE = item.Preg25EE,
                                    Preg26EE = item.Preg26EE,
                                    Preg27EE = item.Preg27EE,
                                    Preg28EE = item.Preg28EE,
                                    Preg29EE = item.Preg29EE,
                                    Preg30EE = item.Preg30EE,
                                    Preg31EE = item.Preg31EE,
                                    Preg32EE = item.Preg32EE,
                                    Preg33EE = item.Preg33EE,
                                    Preg34EE = item.Preg34EE,
                                    Preg35EE = item.Preg35EE,
                                    Preg36EE = item.Preg36EE,
                                    Preg37EE = item.Preg37EE,
                                    Preg38EE = item.Preg38EE,
                                    Preg39EE = item.Preg39EE,
                                    Preg40EE = item.Preg40EE,
                                    Preg41EE = item.Preg41EE,
                                    Preg42EE = item.Preg42EE,
                                    Preg43EE = item.Preg43EE,
                                    Preg44EE = item.Preg44EE,
                                    Preg45EE = item.Preg45EE,
                                    Preg46EE = item.Preg46EE,
                                    Preg47EE = item.Preg47EE,
                                    Preg48EE = item.Preg48EE,
                                    Preg49EE = item.Preg49EE,
                                    Preg50EE = item.Preg50EE,
                                    Preg51EE = item.Preg51EE,
                                    Preg52EE = item.Preg52EE,
                                    Preg53EE = item.Preg53EE,
                                    Preg54EE = item.Preg54EE,
                                    Preg55EE = item.Preg55EE,
                                    Preg56EE = item.Preg56EE,
                                    Preg57EE = item.Preg57EE,
                                    Preg58EE = item.Preg58EE,
                                    Preg59EE = item.Preg59EE,
                                    Preg60EE = item.Preg60EE,
                                    Preg61EE = item.Preg61EE,
                                    Preg62EE = item.Preg62EE,
                                    Preg63EE = item.Preg63EE,
                                    Preg64EE = item.Preg64EE,
                                    Preg65EE = item.Preg65EE,
                                    Preg66EE = item.Preg66EE,
                                    Preg67EE = item.Preg67EE,
                                    Preg68EE = item.Preg68EE,
                                    Preg69EE = item.Preg69EE,
                                    Preg70EE = item.Preg70EE,
                                    Preg71EE = item.Preg71EE,
                                    Preg72EE = item.Preg72EE,
                                    Preg73EE = item.Preg73EE,
                                    Preg74EE = item.Preg74EE,
                                    Preg75EE = item.Preg75EE,
                                    Preg76EE = item.Preg76EE,
                                    Preg77EE = item.Preg77EE,
                                    Preg78EE = item.Preg78EE,
                                    Preg79EE = item.Preg79EE,
                                    Preg80EE = item.Preg80EE,
                                    Preg81EE = item.Preg81EE,
                                    Preg82EE = item.Preg82EE,
                                    Preg83EE = item.Preg83EE,
                                    Preg84EE = item.Preg84EE,
                                    Preg85EE = item.Preg85EE,
                                    Preg86EE = item.Preg86EE,

                                    Preg1EA = item.Preg1EA,
                                    Preg2EA = item.Preg2EA,
                                    Preg3EA = item.Preg3EA,
                                    Preg4EA = item.Preg4EA,
                                    Preg5EA = item.Preg5EA,
                                    Preg6EA = item.Preg6EA,
                                    Preg7EA = item.Preg7EA,
                                    Preg8EA = item.Preg8EA,
                                    Preg9EA = item.Preg9EA,
                                    Preg10EA = item.Preg10EA,
                                    Preg11EA = item.Preg11EA,
                                    Preg12EA = item.Preg12EA,
                                    Preg13EA = item.Preg13EA,
                                    Preg14EA = item.Preg14EA,
                                    Preg15EA = item.Preg15EA,
                                    Preg16EA = item.Preg16EA,
                                    Preg17EA = item.Preg17EA,
                                    Preg18EA = item.Preg18EA,
                                    Preg19EA = item.Preg19EA,
                                    Preg20EA = item.Preg20EA,
                                    Preg21EA = item.Preg21EA,
                                    Preg22EA = item.Preg22EA,
                                    Preg23EA = item.Preg23EA,
                                    Preg24EA = item.Preg24EA,
                                    Preg25EA = item.Preg25EA,
                                    Preg26EA = item.Preg26EA,
                                    Preg27EA = item.Preg27EA,
                                    Preg28EA = item.Preg28EA,
                                    Preg29EA = item.Preg29EA,
                                    Preg30EA = item.Preg30EA,
                                    Preg31EA = item.Preg31EA,
                                    Preg32EA = item.Preg32EA,
                                    Preg33EA = item.Preg33EA,
                                    Preg34EA = item.Preg34EA,
                                    Preg35EA = item.Preg35EA,
                                    Preg36EA = item.Preg36EA,
                                    Preg37EA = item.Preg37EA,
                                    Preg38EA = item.Preg38EA,
                                    Preg39EA = item.Preg39EA,
                                    Preg40EA = item.Preg40EA,
                                    Preg41EA = item.Preg41EA,
                                    Preg42EA = item.Preg42EA,
                                    Preg43EA = item.Preg43EA,
                                    Preg44EA = item.Preg44EA,
                                    Preg45EA = item.Preg45EA,
                                    Preg46EA = item.Preg46EA,
                                    Preg47EA = item.Preg47EA,
                                    Preg48EA = item.Preg48EA,
                                    Preg49EA = item.Preg49EA,
                                    Preg50EA = item.Preg50EA,
                                    Preg51EA = item.Preg51EA,
                                    Preg52EA = item.Preg52EA,
                                    Preg53EA = item.Preg53EA,
                                    Preg54EA = item.Preg54EA,
                                    Preg55EA = item.Preg55EA,
                                    Preg56EA = item.Preg56EA,
                                    Preg57EA = item.Preg57EA,
                                    Preg58EA = item.Preg58EA,
                                    Preg59EA = item.Preg59EA,
                                    Preg60EA = item.Preg60EA,
                                    Preg61EA = item.Preg61EA,
                                    Preg62EA = item.Preg62EA,
                                    Preg63EA = item.Preg63EA,
                                    Preg64EA = item.Preg64EA,
                                    Preg65EA = item.Preg65EA,
                                    Preg66EA = item.Preg66EA,
                                    Preg67EA = item.Preg67EA,
                                    Preg68EA = item.Preg68EA,
                                    Preg69EA = item.Preg69EA,
                                    Preg70EA = item.Preg70EA,
                                    Preg71EA = item.Preg71EA,
                                    Preg72EA = item.Preg72EA,
                                    Preg73EA = item.Preg73EA,
                                    Preg74EA = item.Preg74EA,
                                    Preg75EA = item.Preg75EA,
                                    Preg76EA = item.Preg76EA,
                                    Preg77EA = item.Preg77EA,
                                    Preg78EA = item.Preg78EA,
                                    Preg79EA = item.Preg79EA,
                                    Preg80EA = item.Preg80EA,
                                    Preg81EA = item.Preg81EA,
                                    Preg82EA = item.Preg82EA,
                                    Preg83EA = item.Preg83EA,
                                    Preg84EA = item.Preg84EA,
                                    Preg85EA = item.Preg85EA,
                                    Preg86EA = item.Preg86EA
                                };
                                #endregion llenar objeto
                                list.Add(historico);
                            }
                        }
                        else
                        {
                            ML.Historico historico = new ML.Historico()
                            {
                                #region llenar objeto
                                AlineacionEstrategicaEE = 0,
                                Anio = aHistorico.Anio,
                                CalificacionGlobalEE = 0,
                                CoachingEE = 0,
                                CompanierismoEE = 0,
                                CreedibilidadEE = 0,
                                Entidad = aHistorico.EntidadNombre,
                                EntidadId = aHistorico.EntidadId,
                                EntidadNombre = aHistorico.EntidadNombre,
                                IdTipoEntidad = 0,
                                HabilidadesGerencialesEE = 0,
                                HC = 0,
                                IdHistorico = 0,
                                ImparcialidadEE = 0,
                                ManejoDelCambioEE = 0,
                                NivelColaboracionEE = 0,
                                NivelCompromisoEE = 0,
                                NivelConfianzaEE = 0,
                                NivelParticipacionEE = 0,
                                OrgulloEE = 0,
                                PracticasCulturalesEE = 0,
                                ProcesosOrganizacionalesEE = 0,
                                RespetoEE = 0,
                                //EA
                                AlineacionEstrategicaEA = 0,
                                CalificacionGlobalEA = 0,
                                CoachingEA = 0,
                                CompanierismoEA = 0,
                                CreedibilidadEA = 0,
                                HabilidadesGerencialesEA = 0,
                                ImparcialidadEA = 0,
                                ManejoDelCambioEA = 0,
                                NivelColaboracionEA = 0,
                                NivelCompromisoEA = 0,
                                NivelConfianzaEA = 0,
                                OrgulloEA = 0,
                                PracticasCulturalesEA = 0,
                                ProcesosOrganizacionalesEA = 0,
                                RespetoEA = 0,
                                Preg1EE = 0,
                                Preg2EE = 0,
                                Preg3EE = 0,
                                Preg4EE = 0,
                                Preg5EE = 0,
                                Preg6EE = 0,
                                Preg7EE = 0,
                                Preg8EE = 0,
                                Preg9EE = 0,
                                Preg10EE = 0,
                                Preg11EE = 0,
                                Preg12EE = 0,
                                Preg13EE = 0,
                                Preg14EE = 0,
                                Preg15EE = 0,
                                Preg16EE = 0,
                                Preg17EE = 0,
                                Preg18EE = 0,
                                Preg19EE = 0,
                                Preg20EE = 0,
                                Preg21EE = 0,
                                Preg22EE = 0,
                                Preg23EE = 0,
                                Preg24EE = 0,
                                Preg25EE = 0,
                                Preg26EE = 0,
                                Preg27EE = 0,
                                Preg28EE = 0,
                                Preg29EE = 0,
                                Preg30EE = 0,
                                Preg31EE = 0,
                                Preg32EE = 0,
                                Preg33EE = 0,
                                Preg34EE = 0,
                                Preg35EE = 0,
                                Preg36EE = 0,
                                Preg37EE = 0,
                                Preg38EE = 0,
                                Preg39EE = 0,
                                Preg40EE = 0,
                                Preg41EE = 0,
                                Preg42EE = 0,
                                Preg43EE = 0,
                                Preg44EE = 0,
                                Preg45EE = 0,
                                Preg46EE = 0,
                                Preg47EE = 0,
                                Preg48EE = 0,
                                Preg49EE = 0,
                                Preg50EE = 0,
                                Preg51EE = 0,
                                Preg52EE = 0,
                                Preg53EE = 0,
                                Preg54EE = 0,
                                Preg55EE = 0,
                                Preg56EE = 0,
                                Preg57EE = 0,
                                Preg58EE = 0,
                                Preg59EE = 0,
                                Preg60EE = 0,
                                Preg61EE = 0,
                                Preg62EE = 0,
                                Preg63EE = 0,
                                Preg64EE = 0,
                                Preg65EE = 0,
                                Preg66EE = 0,
                                Preg67EE = 0,
                                Preg68EE = 0,
                                Preg69EE = 0,
                                Preg70EE = 0,
                                Preg71EE = 0,
                                Preg72EE = 0,
                                Preg73EE = 0,
                                Preg74EE = 0,
                                Preg75EE = 0,
                                Preg76EE = 0,
                                Preg77EE = 0,
                                Preg78EE = 0,
                                Preg79EE = 0,
                                Preg80EE = 0,
                                Preg81EE = 0,
                                Preg82EE = 0,
                                Preg83EE = 0,
                                Preg84EE = 0,
                                Preg85EE = 0,
                                Preg86EE = 0,

                                Preg1EA = 0,
                                Preg2EA = 0,
                                Preg3EA = 0,
                                Preg4EA = 0,
                                Preg5EA = 0,
                                Preg6EA = 0,
                                Preg7EA = 0,
                                Preg8EA = 0,
                                Preg9EA = 0,
                                Preg10EA = 0,
                                Preg11EA = 0,
                                Preg12EA = 0,
                                Preg13EA = 0,
                                Preg14EA = 0,
                                Preg15EA = 0,
                                Preg16EA = 0,
                                Preg17EA = 0,
                                Preg18EA = 0,
                                Preg19EA = 0,
                                Preg20EA = 0,
                                Preg21EA = 0,
                                Preg22EA = 0,
                                Preg23EA = 0,
                                Preg24EA = 0,
                                Preg25EA = 0,
                                Preg26EA = 0,
                                Preg27EA = 0,
                                Preg28EA = 0,
                                Preg29EA = 0,
                                Preg30EA = 0,
                                Preg31EA = 0,
                                Preg32EA = 0,
                                Preg33EA = 0,
                                Preg34EA = 0,
                                Preg35EA = 0,
                                Preg36EA = 0,
                                Preg37EA = 0,
                                Preg38EA = 0,
                                Preg39EA = 0,
                                Preg40EA = 0,
                                Preg41EA = 0,
                                Preg42EA = 0,
                                Preg43EA = 0,
                                Preg44EA = 0,
                                Preg45EA = 0,
                                Preg46EA = 0,
                                Preg47EA = 0,
                                Preg48EA = 0,
                                Preg49EA = 0,
                                Preg50EA = 0,
                                Preg51EA = 0,
                                Preg52EA = 0,
                                Preg53EA = 0,
                                Preg54EA = 0,
                                Preg55EA = 0,
                                Preg56EA = 0,
                                Preg57EA = 0,
                                Preg58EA = 0,
                                Preg59EA = 0,
                                Preg60EA = 0,
                                Preg61EA = 0,
                                Preg62EA = 0,
                                Preg63EA = 0,
                                Preg64EA = 0,
                                Preg65EA = 0,
                                Preg66EA = 0,
                                Preg67EA = 0,
                                Preg68EA = 0,
                                Preg69EA = 0,
                                Preg70EA = 0,
                                Preg71EA = 0,
                                Preg72EA = 0,
                                Preg73EA = 0,
                                Preg74EA = 0,
                                Preg75EA = 0,
                                Preg76EA = 0,
                                Preg77EA = 0,
                                Preg78EA = 0,
                                Preg79EA = 0,
                                Preg80EA = 0,
                                Preg81EA = 0,
                                Preg82EA = 0,
                                Preg83EA = 0,
                                Preg84EA = 0,
                                Preg85EA = 0,
                                Preg86EA = 0


                                #endregion llenar objeto
                            };
                            list.Add(historico);
                        }
                    }
                }
            }
            catch (Exception aE)
            {
                BL.LogReporteoClima.writteLog(aE, new StackTrace());
                return new List<ML.Historico>();
            }
            return list;
        }*/

        public static List<ML.HistoricoClima> getHistorico_2EE(ML.Historico aHistorico)
        {
            var list = new List<ML.HistoricoClima>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.HistoricoClima.Select(o => o)
                        .Where(o => o.EntidadId == aHistorico.EntidadId && o.EntidadNombre == aHistorico.EntidadNombre && o.Anio == aHistorico.Anio && o.Enfoque == "Enfoque empresa").ToList();
                    var queryEE = context.HistoricoClima.Select(o => new { o.Promedio66R, o.Enfoque, o.EntidadId, o.EntidadNombre }).Where(o => (o.EntidadId == aHistorico.EntidadId && o.Enfoque == "Enfoque Empresa") || (o.EntidadNombre == aHistorico.EntidadNombre && o.Enfoque == "Enfoque Empresa")).FirstOrDefault();
                    if (query != null)
                    {
                        if (query.Count > 0)
                        {
                            foreach (var item in query)
                            {
                                #region llenar objeto
                                ML.HistoricoClima historico = new ML.HistoricoClima()
                                {
                                    Bienestar = item.Bienestar,
                                    Bio = item.Bio,
                                    Psico = item.Psico,
                                    Social = item.Social,
                                    AuxProm66ReactEE = (decimal)queryEE.Promedio66R,
                                    AlineacionEstrategica = item.AlineacionEstrategica,
                                    Anio = item.Anio,
                                    CalificacionGlobal = item.CalificacionGlobal,
                                    Coaching = item.Coaching,
                                    Companierismo = item.Companierismo,
                                    Creedibilidad = item.Creedibilidad,
                                    Entidad = item.Entidad,
                                    EntidadId = (int)item.EntidadId,
                                    EntidadNombre = item.EntidadNombre,
                                    IdTipoEntidad = item.IdTipoEntidad,
                                    HabilidadesGerenciales = item.HabilidadesGerenciales,
                                    HC = item.HC,
                                    IdHistorico = item.IdHistorico,
                                    Imparcialidad = item.Imparcialidad,
                                    ManejoDelCambio = item.ManejoDelCambio,
                                    NivelColaboracion = item.NivelColaboracion,
                                    NivelCompromiso = item.NivelCompromiso,
                                    NivelConfianza = item.NivelConfianza,
                                    NivelParticipacion = item.NivelParticipacion,
                                    Orgullo = item.Orgullo,
                                    PracticasCulturales = item.PracticasCulturales,
                                    ProcesosOrganizacionales = item.ProcesosOrganizacionales,
                                    Respeto = item.Respeto,
                                    //
                                    Preg1 = item.Preg1,
                                    Preg2 = item.Preg2,
                                    Preg3 = item.Preg3,
                                    Preg4 = item.Preg4,
                                    Preg5 = item.Preg5,
                                    Preg6 = item.Preg6,
                                    Preg7 = item.Preg7,
                                    Preg8 = item.Preg8,
                                    Preg9 = item.Preg9,
                                    Preg10 = item.Preg10,
                                    Preg11 = item.Preg11,
                                    Preg12 = item.Preg12,
                                    Preg13 = item.Preg13,
                                    Preg14 = item.Preg14,
                                    Preg15 = item.Preg15,
                                    Preg16 = item.Preg16,
                                    Preg17 = item.Preg17,
                                    Preg18 = item.Preg18,
                                    Preg19 = item.Preg19,
                                    Preg20 = item.Preg20,
                                    Preg21 = item.Preg21,
                                    Preg22 = item.Preg22,
                                    Preg23 = item.Preg23,
                                    Preg24 = item.Preg24,
                                    Preg25 = item.Preg25,
                                    Preg26 = item.Preg26,
                                    Preg27 = item.Preg27,
                                    Preg28 = item.Preg28,
                                    Preg29 = item.Preg29,
                                    Preg30 = item.Preg30,
                                    Preg31 = item.Preg31,
                                    Preg32 = item.Preg32,
                                    Preg33 = item.Preg33,
                                    Preg34 = item.Preg34,
                                    Preg35 = item.Preg35,
                                    Preg36 = item.Preg36,
                                    Preg37 = item.Preg37,
                                    Preg38 = item.Preg38,
                                    Preg39 = item.Preg39,
                                    Preg40 = item.Preg40,
                                    Preg41 = item.Preg41,
                                    Preg42 = item.Preg42,
                                    Preg43 = item.Preg43,
                                    Preg44 = item.Preg44,
                                    Preg45 = item.Preg45,
                                    Preg46 = item.Preg46,
                                    Preg47 = item.Preg47,
                                    Preg48 = item.Preg48,
                                    Preg49 = item.Preg49,
                                    Preg50 = item.Preg50,
                                    Preg51 = item.Preg51,
                                    Preg52 = item.Preg52,
                                    Preg53 = item.Preg53,
                                    Preg54 = item.Preg54,
                                    Preg55 = item.Preg55,
                                    Preg56 = item.Preg56,
                                    Preg57 = item.Preg57,
                                    Preg58 = item.Preg58,
                                    Preg59 = item.Preg59,
                                    Preg60 = item.Preg60,
                                    Preg61 = item.Preg61,
                                    Preg62 = item.Preg62,
                                    Preg63 = item.Preg63,
                                    Preg64 = item.Preg64,
                                    Preg65 = item.Preg65,
                                    Preg66 = item.Preg66,
                                    Preg67 = item.Preg67,
                                    Preg68 = item.Preg68,
                                    Preg69 = item.Preg69,
                                    Preg70 = item.Preg70,
                                    Preg71 = item.Preg71,
                                    Preg72 = item.Preg72,
                                    Preg73 = item.Preg73,
                                    Preg74 = item.Preg74,
                                    Preg75 = item.Preg75,
                                    Preg76 = item.Preg76,
                                    Preg77 = item.Preg77,
                                    Preg78 = item.Preg78,
                                    Preg79 = item.Preg79,
                                    Preg80 = item.Preg80,
                                    Preg81 = item.Preg81,
                                    Preg82 = item.Preg82,
                                    Preg83 = item.Preg83,
                                    Preg84 = item.Preg84,
                                    Preg85 = item.Preg85,
                                    Preg86 = item.Preg86,
                                    Administrativo =
                                        new ML.Administrativo {
                                            Promedio66R = Convert.ToDecimal(item.Administrativo.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Administrativo.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Administrativo.Split('_')[2]),
                                        },
                                    Ant_1_a_2_anios =
                                        new ML.Ant_1_a_2_anios
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Ant_1_a_2_anios.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Ant_1_a_2_anios.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Ant_1_a_2_anios.Split('_')[2]),
                                        },
                                    Ant_3_a_5_anios =
                                        new ML.Ant_3_a_5_anios
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Ant_3_a_5_anios.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Ant_3_a_5_anios.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Ant_3_a_5_anios.Split('_')[2])
                                        },
                                    Ant_6_a_10_anios = 
                                        new ML.Ant_6_a_10_anios
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Ant_6_a_10_anios.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Ant_6_a_10_anios.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Ant_6_a_10_anios.Split('_')[2])
                                        },
                                    Ant_6_meses_1_anio = 
                                        new ML.Ant_6_meses_1_anio
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Ant_6_meses_1_anio.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Ant_6_meses_1_anio.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Ant_6_meses_1_anio.Split('_')[2])
                                        },
                                    Ant_mas_de_10_anios = 
                                        new ML.Ant_mas_de_10_anios
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Ant_mas_de_10_anios.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Ant_mas_de_10_anios.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Ant_mas_de_10_anios.Split('_')[2])
                                        },
                                    Ant_menos_de_6_meses = 
                                    new ML.Ant_menos_de_6_meses
                                    {
                                        Promedio66R = Convert.ToDecimal(item.Ant_menos_de_6_meses.Split('_')[0]),
                                        Promedio86R = Convert.ToDecimal(item.Ant_menos_de_6_meses.Split('_')[1]),
                                        Contestadas = Convert.ToInt32(item.Ant_menos_de_6_meses.Split('_')[2])
                                    },
                                    Comercial = 
                                        new ML.Comercial
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Comercial.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Comercial.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Comercial.Split('_')[2])
                                        },
                                    Comisionistas = 
                                        new ML.Comisionistas
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Comisionistas.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Comisionistas.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Comisionistas.Split('_')[2])
                                        },
                                    COORDINADOR_SUPERVISOR_JEFE = 
                                        new ML.COORDINADOR_SUPERVISOR_JEFE
                                        {
                                            Promedio66R = Convert.ToDecimal(item.COORDINADOR_SUPERVISOR_JEFE.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.COORDINADOR_SUPERVISOR_JEFE.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.COORDINADOR_SUPERVISOR_JEFE.Split('_')[2])
                                        },
                                    Director = 
                                        new ML.Director
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Director.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Director.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Director.Split('_')[2])
                                        },
                                    Edad_18_A_22_ANIOS = 
                                        new ML.Edad_18_A_22_ANIOS
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Edad_18_A_22_ANIOS.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Edad_18_A_22_ANIOS.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Edad_18_A_22_ANIOS.Split('_')[2])
                                        },
                                    Edad_23_A_31_ANIOS = 
                                        new ML.Edad_23_A_31_ANIOS
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Edad_23_A_31_ANIOS.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Edad_23_A_31_ANIOS.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Edad_23_A_31_ANIOS.Split('_')[2])
                                        },
                                    Edad_32_A_39_ANIOS =
                                        new ML.Edad_32_A_39_ANIOS
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Edad_32_A_39_ANIOS.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Edad_32_A_39_ANIOS.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Edad_32_A_39_ANIOS.Split('_')[2])
                                        },
                                    Edad_40_A_55_ANIOS = 
                                        new ML.Edad_40_A_55_ANIOS
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Edad_40_A_55_ANIOS.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Edad_40_A_55_ANIOS.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Edad_40_A_55_ANIOS.Split('_')[2])
                                        },
                                    Edad_56_ANIOS_O_MAS = 
                                        new ML.Edad_56_ANIOS_O_MAS
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Edad_56_ANIOS_O_MAS.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Edad_56_ANIOS_O_MAS.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Edad_56_ANIOS_O_MAS.Split('_')[2])
                                        },
                                    GerenteDepartamental = 
                                        new ML.GerenteDepartamental
                                        {
                                            Promedio66R = Convert.ToDecimal(item.GerenteDepartamental.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.GerenteDepartamental.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.GerenteDepartamental.Split('_')[2])
                                        },
                                    GerenteGeneral = 
                                        new ML.GerenteGeneral
                                        {
                                            Promedio66R = Convert.ToDecimal(item.GerenteGeneral.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.GerenteGeneral.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.GerenteGeneral.Split('_')[2])
                                        },
                                    Honorarios = 
                                        new ML.Honorarios
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Honorarios.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Honorarios.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Honorarios.Split('_')[2])
                                        },
                                    Media_Superior = 
                                        new ML.Media_Superior
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Media_Superior.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Media_Superior.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Media_Superior.Split('_')[2])
                                        },
                                    Media_Tecnica = 
                                        new ML.Media_Tecnica
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Media_Tecnica.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Media_Tecnica.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Media_Tecnica.Split('_')[2])
                                        },
                                    Planta = 
                                    new ML.Planta
                                    {
                                        Promedio66R = Convert.ToDecimal(item.Planta.Split('_')[0]),
                                        Promedio86R = Convert.ToDecimal(item.Planta.Split('_')[1]),
                                        Contestadas = Convert.ToInt32(item.Planta.Split('_')[2])
                                    },
                                    PostGrado = 
                                        new ML.PostGrado
                                        {
                                            Promedio66R = Convert.ToDecimal(item.PostGrado.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.PostGrado.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.PostGrado.Split('_')[2])
                                        },
                                    Primaria = 
                                        new ML.Primaria
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Primaria.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Primaria.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Primaria.Split('_')[2])
                                        },
                                    Secundaria = 
                                        new ML.Secundaria
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Secundaria.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Secundaria.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Secundaria.Split('_')[2])
                                        },
                                    Sexo_Femenino = 
                                        new ML.Sexo_Femenino
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Sexo_Femenino.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Sexo_Femenino.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Sexo_Femenino.Split('_')[2])
                                        },
                                    Sexo_Masculino =
                                        new ML.Sexo_Masculino
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Sexo_Masculino.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Sexo_Masculino.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Sexo_Masculino.Split('_')[2])
                                        },
                                    Sindicalizado = 
                                        new ML.Sindicalizado
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Sindicalizado.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Sindicalizado.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Sindicalizado.Split('_')[2])
                                        },
                                    Subgerente = 
                                        new ML.Subgerente
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Subgerente.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Subgerente.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Subgerente.Split('_')[2])
                                        },
                                    TECNICO_OPERATIVO =
                                        new ML.TECNICO_OPERATIVO
                                        {
                                            Promedio66R = Convert.ToDecimal(item.TECNICO_OPERATIVO.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.TECNICO_OPERATIVO.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.TECNICO_OPERATIVO.Split('_')[2])
                                        },
                                    Temporal =
                                        new ML.Temporal
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Temporal.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Temporal.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Temporal.Split('_')[2])
                                        },
                                    Universidad_Completa =
                                        new ML.Universidad_Completa
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Universidad_Completa.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Universidad_Completa.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Universidad_Completa.Split('_')[2])
                                        },
                                    Universidad_Incompleta = 
                                        new ML.Universidad_Incompleta
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Universidad_Incompleta.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Universidad_Incompleta.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Universidad_Incompleta.Split('_')[2])
                                        },
                                    Contestadas = (int)item.Contestadas,
                                    //aHistorico.Enfoque == 1 ? "Enfoque empresa" : aHistorico.Enfoque == 2 ? "Enfoque Area" : "",
                                    Enfoque = item.Enfoque == "Enfoque empresa" ? 1 : item.Enfoque == "Enfoque Area" ? 2 : 0,
                                    Esperadas = (int)item.Esperadas,
                                    Promedio66R = (decimal)item.Promedio66R,
                                    Promedio86R = (decimal)item.Promedio86R
                                };
                                #endregion llenar objeto
                                list.Add(historico);
                            }
                        }
                        else
                        {
                            #region llenar objeto
                            ML.HistoricoClima historico = new ML.HistoricoClima()
                            {
                                Bienestar = 0,
                                Bio = 0,
                                Psico = 0,
                                Social = 0,
                                AuxProm66ReactEE = 0,
                                AlineacionEstrategica = 0, //.AlineacionEstrategica,
                                Anio = 0, //.Anio,
                                CalificacionGlobal = 0, //.CalificacionGlobal,
                                Coaching = 0, //.Coaching,
                                Companierismo = 0, //.Companierismo,
                                Creedibilidad = 0, //.Creedibilidad,
                                Entidad = "", //.Entidad,
                                EntidadId = (int)aHistorico.EntidadId, //.EntidadId,
                                EntidadNombre = aHistorico.EntidadNombre, //.EntidadNombre,
                                IdTipoEntidad = 0, //.IdTipoEntidad,
                                HabilidadesGerenciales = 0, //.HabilidadesGerenciales,
                                HC = 0, //.HC,
                                IdHistorico = 0, //.IdHistorico,
                                Imparcialidad = 0, //.Imparcialidad,
                                ManejoDelCambio = 0, //.ManejoDelCambio,
                                NivelColaboracion = 0, //.NivelColaboracion,
                                NivelCompromiso = 0, //.NivelCompromiso,
                                NivelConfianza = 0, //.NivelConfianza,
                                NivelParticipacion = 0, //.NivelParticipacion,
                                Orgullo = 0, //.Orgullo,
                                PracticasCulturales = 0, //.PracticasCulturales,
                                ProcesosOrganizacionales = 0, //.ProcesosOrganizacionales,
                                Respeto = 0, //.Respeto,
                                             //
                                Preg1 = 0, //.Preg1,
                                Preg2 = 0, //.Preg2,
                                Preg3 = 0, //.Preg3,
                                Preg4 = 0, //.Preg4,
                                Preg5 = 0, //.Preg5,
                                Preg6 = 0, //.Preg6,
                                Preg7 = 0, //.Preg7,
                                Preg8 = 0, //.Preg8,
                                Preg9 = 0, //.Preg9,
                                Preg10 = 0, //.Preg10,
                                Preg11 = 0, //.Preg11,
                                Preg12 = 0, //.Preg12,
                                Preg13 = 0, //.Preg13,
                                Preg14 = 0, //.Preg14,
                                Preg15 = 0, //.Preg15,
                                Preg16 = 0, //.Preg16,
                                Preg17 = 0, //.Preg17,
                                Preg18 = 0, //.Preg18,
                                Preg19 = 0, //.Preg19,
                                Preg20 = 0, //.Preg20,
                                Preg21 = 0, //.Preg21,
                                Preg22 = 0, //.Preg22,
                                Preg23 = 0, //.Preg23,
                                Preg24 = 0, //.Preg24,
                                Preg25 = 0, //.Preg25,
                                Preg26 = 0, //.Preg26,
                                Preg27 = 0, //.Preg27,
                                Preg28 = 0, //.Preg28,
                                Preg29 = 0, //.Preg29,
                                Preg30 = 0, //.Preg30,
                                Preg31 = 0, //.Preg31,
                                Preg32 = 0, //.Preg32,
                                Preg33 = 0, //.Preg33,
                                Preg34 = 0, //.Preg34,
                                Preg35 = 0, //.Preg35,
                                Preg36 = 0, //.Preg36,
                                Preg37 = 0, //.Preg37,
                                Preg38 = 0, //.Preg38,
                                Preg39 = 0, //.Preg39,
                                Preg40 = 0, //.Preg40,
                                Preg41 = 0, //.Preg41,
                                Preg42 = 0, //.Preg42,
                                Preg43 = 0, //.Preg43,
                                Preg44 = 0, //.Preg44,
                                Preg45 = 0, //.Preg45,
                                Preg46 = 0, //.Preg46,
                                Preg47 = 0, //.Preg47,
                                Preg48 = 0, //.Preg48,
                                Preg49 = 0, //.Preg49,
                                Preg50 = 0, //.Preg50,
                                Preg51 = 0, //.Preg51,
                                Preg52 = 0, //.Preg52,
                                Preg53 = 0, //.Preg53,
                                Preg54 = 0, //.Preg54,
                                Preg55 = 0, //.Preg55,
                                Preg56 = 0, //.Preg56,
                                Preg57 = 0, //.Preg57,
                                Preg58 = 0, //.Preg58,
                                Preg59 = 0, //.Preg59,
                                Preg60 = 0, //.Preg60,
                                Preg61 = 0, //.Preg61,
                                Preg62 = 0, //.Preg62,
                                Preg63 = 0, //.Preg63,
                                Preg64 = 0, //.Preg64,
                                Preg65 = 0, //.Preg65,
                                Preg66 = 0, //.Preg66,
                                Preg67 = 0, //.Preg67,
                                Preg68 = 0, //.Preg68,
                                Preg69 = 0, //.Preg69,
                                Preg70 = 0, //.Preg70,
                                Preg71 = 0, //.Preg71,
                                Preg72 = 0, //.Preg72,
                                Preg73 = 0, //.Preg73,
                                Preg74 = 0, //.Preg74,
                                Preg75 = 0, //.Preg75,
                                Preg76 = 0, //.Preg76,
                                Preg77 = 0, //.Preg77,
                                Preg78 = 0, //.Preg78,
                                Preg79 = 0, //.Preg79,
                                Preg80 = 0, //.Preg80,
                                Preg81 = 0, //.Preg81,
                                Preg82 = 0, //.Preg82,
                                Preg83 = 0, //.Preg83,
                                Preg84 = 0, //.Preg84,
                                Preg85 = 0, //.Preg85,
                                Preg86 = 0, //.Preg86,
                                Administrativo =
                                    new ML.Administrativo
                                    {
                                        Promedio66R = 0, // //.Administrativo.Split('_')[0]),
                                            Promedio86R = 0, // //.Administrativo.Split('_')[1]),
                                            Contestadas = 0, //, //.Administrativo.Split('_')[2]),
                                        },
                                Ant_1_a_2_anios =
                                    new ML.Ant_1_a_2_anios
                                    {
                                        Promedio66R = 0, // //.Ant_1_a_2_anios.Split('_')[0]),
                                            Promedio86R = 0, // //.Ant_1_a_2_anios.Split('_')[1]),
                                            Contestadas = 0, //, //.Ant_1_a_2_anios.Split('_')[2]),
                                        },
                                Ant_3_a_5_anios =
                                    new ML.Ant_3_a_5_anios
                                    {
                                        Promedio66R = 0, // //.Ant_3_a_5_anios.Split('_')[0]),
                                            Promedio86R = 0, // //.Ant_3_a_5_anios.Split('_')[1]),
                                            Contestadas = 0, //, //.Ant_3_a_5_anios.Split('_')[2])
                                        },
                                Ant_6_a_10_anios =
                                    new ML.Ant_6_a_10_anios
                                    {
                                        Promedio66R = 0, // //.Ant_6_a_10_anios.Split('_')[0]),
                                            Promedio86R = 0, // //.Ant_6_a_10_anios.Split('_')[1]),
                                            Contestadas = 0, //, //.Ant_6_a_10_anios.Split('_')[2])
                                        },
                                Ant_6_meses_1_anio =
                                    new ML.Ant_6_meses_1_anio
                                    {
                                        Promedio66R = 0, // //.Ant_6_meses_1_anio.Split('_')[0]),
                                            Promedio86R = 0, // //.Ant_6_meses_1_anio.Split('_')[1]),
                                            Contestadas = 0, //, //.Ant_6_meses_1_anio.Split('_')[2])
                                        },
                                Ant_mas_de_10_anios =
                                    new ML.Ant_mas_de_10_anios
                                    {
                                        Promedio66R = 0, // //.Ant_mas_de_10_anios.Split('_')[0]),
                                            Promedio86R = 0, // //.Ant_mas_de_10_anios.Split('_')[1]),
                                            Contestadas = 0, //, //.Ant_mas_de_10_anios.Split('_')[2])
                                        },
                                Ant_menos_de_6_meses =
                                new ML.Ant_menos_de_6_meses
                                {
                                    Promedio66R = 0, // //.Ant_menos_de_6_meses.Split('_')[0]),
                                        Promedio86R = 0, // //.Ant_menos_de_6_meses.Split('_')[1]),
                                        Contestadas = 0, //, //.Ant_menos_de_6_meses.Split('_')[2])
                                    },
                                Comercial =
                                    new ML.Comercial
                                    {
                                        Promedio66R = 0, // //.Comercial.Split('_')[0]),
                                            Promedio86R = 0, // //.Comercial.Split('_')[1]),
                                            Contestadas = 0, //, //.Comercial.Split('_')[2])
                                        },
                                Comisionistas =
                                    new ML.Comisionistas
                                    {
                                        Promedio66R = 0, // //.Comisionistas.Split('_')[0]),
                                            Promedio86R = 0, // //.Comisionistas.Split('_')[1]),
                                            Contestadas = 0, //, //.Comisionistas.Split('_')[2])
                                        },
                                COORDINADOR_SUPERVISOR_JEFE =
                                    new ML.COORDINADOR_SUPERVISOR_JEFE
                                    {
                                        Promedio66R = 0, // //.COORDINADOR_SUPERVISOR_JEFE.Split('_')[0]),
                                            Promedio86R = 0, // //.COORDINADOR_SUPERVISOR_JEFE.Split('_')[1]),
                                            Contestadas = 0, //, //.COORDINADOR_SUPERVISOR_JEFE.Split('_')[2])
                                        },
                                Director =
                                    new ML.Director
                                    {
                                        Promedio66R = 0, // //.Director.Split('_')[0]),
                                            Promedio86R = 0, // //.Director.Split('_')[1]),
                                            Contestadas = 0, //, //.Director.Split('_')[2])
                                        },
                                Edad_18_A_22_ANIOS =
                                    new ML.Edad_18_A_22_ANIOS
                                    {
                                        Promedio66R = 0, // //.Edad_18_A_22_ANIOS.Split('_')[0]),
                                            Promedio86R = 0, // //.Edad_18_A_22_ANIOS.Split('_')[1]),
                                            Contestadas = 0, //, //.Edad_18_A_22_ANIOS.Split('_')[2])
                                        },
                                Edad_23_A_31_ANIOS =
                                    new ML.Edad_23_A_31_ANIOS
                                    {
                                        Promedio66R = 0, // //.Edad_23_A_31_ANIOS.Split('_')[0]),
                                            Promedio86R = 0, // //.Edad_23_A_31_ANIOS.Split('_')[1]),
                                            Contestadas = 0, //, //.Edad_23_A_31_ANIOS.Split('_')[2])
                                        },
                                Edad_32_A_39_ANIOS =
                                    new ML.Edad_32_A_39_ANIOS
                                    {
                                        Promedio66R = 0, // //.Edad_32_A_39_ANIOS.Split('_')[0]),
                                            Promedio86R = 0, // //.Edad_32_A_39_ANIOS.Split('_')[1]),
                                            Contestadas = 0, //, //.Edad_32_A_39_ANIOS.Split('_')[2])
                                        },
                                Edad_40_A_55_ANIOS =
                                    new ML.Edad_40_A_55_ANIOS
                                    {
                                        Promedio66R = 0, // //.Edad_40_A_55_ANIOS.Split('_')[0]),
                                            Promedio86R = 0, // //.Edad_40_A_55_ANIOS.Split('_')[1]),
                                            Contestadas = 0, //, //.Edad_40_A_55_ANIOS.Split('_')[2])
                                        },
                                Edad_56_ANIOS_O_MAS =
                                    new ML.Edad_56_ANIOS_O_MAS
                                    {
                                        Promedio66R = 0, // //.Edad_56_ANIOS_O_MAS.Split('_')[0]),
                                            Promedio86R = 0, // //.Edad_56_ANIOS_O_MAS.Split('_')[1]),
                                            Contestadas = 0, //, //.Edad_56_ANIOS_O_MAS.Split('_')[2])
                                        },
                                GerenteDepartamental =
                                    new ML.GerenteDepartamental
                                    {
                                        Promedio66R = 0, // //.GerenteDepartamental.Split('_')[0]),
                                            Promedio86R = 0, // //.GerenteDepartamental.Split('_')[1]),
                                            Contestadas = 0, //, //.GerenteDepartamental.Split('_')[2])
                                        },
                                GerenteGeneral =
                                    new ML.GerenteGeneral
                                    {
                                        Promedio66R = 0, // //.GerenteGeneral.Split('_')[0]),
                                            Promedio86R = 0, // //.GerenteGeneral.Split('_')[1]),
                                            Contestadas = 0, //, //.GerenteGeneral.Split('_')[2])
                                        },
                                Honorarios =
                                    new ML.Honorarios
                                    {
                                        Promedio66R = 0, // //.Honorarios.Split('_')[0]),
                                            Promedio86R = 0, // //.Honorarios.Split('_')[1]),
                                            Contestadas = 0, //, //.Honorarios.Split('_')[2])
                                        },
                                Media_Superior =
                                    new ML.Media_Superior
                                    {
                                        Promedio66R = 0, // //.Media_Superior.Split('_')[0]),
                                            Promedio86R = 0, // //.Media_Superior.Split('_')[1]),
                                            Contestadas = 0, //, //.Media_Superior.Split('_')[2])
                                        },
                                Media_Tecnica =
                                    new ML.Media_Tecnica
                                    {
                                        Promedio66R = 0, // //.Media_Tecnica.Split('_')[0]),
                                            Promedio86R = 0, // //.Media_Tecnica.Split('_')[1]),
                                            Contestadas = 0, //, //.Media_Tecnica.Split('_')[2])
                                        },
                                Planta =
                                new ML.Planta
                                {
                                    Promedio66R = 0, // //.Planta.Split('_')[0]),
                                        Promedio86R = 0, // //.Planta.Split('_')[1]),
                                        Contestadas = 0, //, //.Planta.Split('_')[2])
                                    },
                                PostGrado =
                                    new ML.PostGrado
                                    {
                                        Promedio66R = 0, // //.PostGrado.Split('_')[0]),
                                            Promedio86R = 0, // //.PostGrado.Split('_')[1]),
                                            Contestadas = 0, //, //.PostGrado.Split('_')[2])
                                        },
                                Primaria =
                                    new ML.Primaria
                                    {
                                        Promedio66R = 0, // //.Primaria.Split('_')[0]),
                                            Promedio86R = 0, // //.Primaria.Split('_')[1]),
                                            Contestadas = 0, //, //.Primaria.Split('_')[2])
                                        },
                                Secundaria =
                                    new ML.Secundaria
                                    {
                                        Promedio66R = 0, // //.Secundaria.Split('_')[0]),
                                            Promedio86R = 0, // //.Secundaria.Split('_')[1]),
                                            Contestadas = 0, //, //.Secundaria.Split('_')[2])
                                        },
                                Sexo_Femenino =
                                    new ML.Sexo_Femenino
                                    {
                                        Promedio66R = 0, // //.Sexo_Femenino.Split('_')[0]),
                                            Promedio86R = 0, // //.Sexo_Femenino.Split('_')[1]),
                                            Contestadas = 0, //, //.Sexo_Femenino.Split('_')[2])
                                        },
                                Sexo_Masculino =
                                    new ML.Sexo_Masculino
                                    {
                                        Promedio66R = 0, // //.Sexo_Masculino.Split('_')[0]),
                                            Promedio86R = 0, // //.Sexo_Masculino.Split('_')[1]),
                                            Contestadas = 0, //, //.Sexo_Masculino.Split('_')[2])
                                        },
                                Sindicalizado =
                                    new ML.Sindicalizado
                                    {
                                        Promedio66R = 0, // //.Sindicalizado.Split('_')[0]),
                                            Promedio86R = 0, // //.Sindicalizado.Split('_')[1]),
                                            Contestadas = 0, //, //.Sindicalizado.Split('_')[2])
                                        },
                                Subgerente =
                                    new ML.Subgerente
                                    {
                                        Promedio66R = 0, // //.Subgerente.Split('_')[0]),
                                            Promedio86R = 0, // //.Subgerente.Split('_')[1]),
                                            Contestadas = 0, //, //.Subgerente.Split('_')[2])
                                        },
                                TECNICO_OPERATIVO =
                                    new ML.TECNICO_OPERATIVO
                                    {
                                        Promedio66R = 0, // //.TECNICO_OPERATIVO.Split('_')[0]),
                                            Promedio86R = 0, // //.TECNICO_OPERATIVO.Split('_')[1]),
                                            Contestadas = 0, //, //.TECNICO_OPERATIVO.Split('_')[2])
                                        },
                                Temporal =
                                    new ML.Temporal
                                    {
                                        Promedio66R = 0, // //.Temporal.Split('_')[0]),
                                            Promedio86R = 0, // //.Temporal.Split('_')[1]),
                                            Contestadas = 0, //, //.Temporal.Split('_')[2])
                                        },
                                Universidad_Completa =
                                    new ML.Universidad_Completa
                                    {
                                        Promedio66R = 0, // //.Universidad_Completa.Split('_')[0]),
                                            Promedio86R = 0, // //.Universidad_Completa.Split('_')[1]),
                                            Contestadas = 0, //, //.Universidad_Completa.Split('_')[2])
                                        },
                                Universidad_Incompleta =
                                    new ML.Universidad_Incompleta
                                    {
                                        Promedio66R = 0, // //.Universidad_Incompleta.Split('_')[0]),
                                            Promedio86R = 0, // //.Universidad_Incompleta.Split('_')[1]),
                                            Contestadas = 0, //, //.Universidad_Incompleta.Split('_')[2])
                                        },
                                Contestadas = (int)0, //.Contestadas,
                                                      //aHistorico.Enfoque == 1 ? "Enfoque empresa" : aHistorico.Enfoque == 2 ? "Enfoque Area" : "",
                                Enfoque = 0, //.Enfoque == "Enfoque empresa" ? 1 : 0, //.Enfoque == "Enfoque Area" ? 2 : 0,
                                Esperadas = (int)0, //.Esperadas,
                                Promedio66R = (decimal)0, //.Promedio66R,
                                Promedio86R = (decimal)0, //.Promedio86R
                            };
                            #endregion llenar objeto
                            list.Add(historico);
                        }
                    }
                }
            }
            catch (Exception aE)
            {
                BL.LogReporteoClima.writteLog(aE, new StackTrace());
                return new List<ML.HistoricoClima>();
            }
            return list;
        }

        public static List<ML.HistoricoClima> getHistorico_2EEBienestar(ML.Historico aHistorico)
        {
            var list = new List<ML.HistoricoClima>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.HistoricoClima.Select(o => o)
                        .Where(o => o.EntidadNombre == aHistorico.EntidadNombre && o.Anio == aHistorico.Anio && o.Enfoque == "Enfoque empresa").ToList();

                    if (query != null)
                    {
                        if (query.Count > 0)
                        {
                            foreach (var item in query)
                            {
                                #region llenar objeto
                                ML.HistoricoClima historico = new ML.HistoricoClima()
                                {
                                    Bienestar = item.Bienestar,
                                    Bio = item.Bio,
                                    Psico = item.Psico,
                                    Social = item.Social,
                                    AlineacionEstrategica = item.AlineacionEstrategica,
                                    Anio = item.Anio,
                                    CalificacionGlobal = item.CalificacionGlobal,
                                    Coaching = item.Coaching,
                                    Companierismo = item.Companierismo,
                                    Creedibilidad = item.Creedibilidad,
                                    Entidad = item.Entidad,
                                    EntidadId = (int)item.EntidadId,
                                    EntidadNombre = item.EntidadNombre,
                                    IdTipoEntidad = item.IdTipoEntidad,
                                    HabilidadesGerenciales = item.HabilidadesGerenciales,
                                    HC = item.HC,
                                    IdHistorico = item.IdHistorico,
                                    Imparcialidad = item.Imparcialidad,
                                    ManejoDelCambio = item.ManejoDelCambio,
                                    NivelColaboracion = item.NivelColaboracion,
                                    NivelCompromiso = item.NivelCompromiso,
                                    NivelConfianza = item.NivelConfianza,
                                    NivelParticipacion = item.NivelParticipacion,
                                    Orgullo = item.Orgullo,
                                    PracticasCulturales = item.PracticasCulturales,
                                    ProcesosOrganizacionales = item.ProcesosOrganizacionales,
                                    Respeto = item.Respeto,
                                    //
                                    Preg1 = item.Preg1,
                                    Preg2 = item.Preg2,
                                    Preg3 = item.Preg3,
                                    Preg4 = item.Preg4,
                                    Preg5 = item.Preg5,
                                    Preg6 = item.Preg6,
                                    Preg7 = item.Preg7,
                                    Preg8 = item.Preg8,
                                    Preg9 = item.Preg9,
                                    Preg10 = item.Preg10,
                                    Preg11 = item.Preg11,
                                    Preg12 = item.Preg12,
                                    Preg13 = item.Preg13,
                                    Preg14 = item.Preg14,
                                    Preg15 = item.Preg15,
                                    Preg16 = item.Preg16,
                                    Preg17 = item.Preg17,
                                    Preg18 = item.Preg18,
                                    Preg19 = item.Preg19,
                                    Preg20 = item.Preg20,
                                    Preg21 = item.Preg21,
                                    Preg22 = item.Preg22,
                                    Preg23 = item.Preg23,
                                    Preg24 = item.Preg24,
                                    Preg25 = item.Preg25,
                                    Preg26 = item.Preg26,
                                    Preg27 = item.Preg27,
                                    Preg28 = item.Preg28,
                                    Preg29 = item.Preg29,
                                    Preg30 = item.Preg30,
                                    Preg31 = item.Preg31,
                                    Preg32 = item.Preg32,
                                    Preg33 = item.Preg33,
                                    Preg34 = item.Preg34,
                                    Preg35 = item.Preg35,
                                    Preg36 = item.Preg36,
                                    Preg37 = item.Preg37,
                                    Preg38 = item.Preg38,
                                    Preg39 = item.Preg39,
                                    Preg40 = item.Preg40,
                                    Preg41 = item.Preg41,
                                    Preg42 = item.Preg42,
                                    Preg43 = item.Preg43,
                                    Preg44 = item.Preg44,
                                    Preg45 = item.Preg45,
                                    Preg46 = item.Preg46,
                                    Preg47 = item.Preg47,
                                    Preg48 = item.Preg48,
                                    Preg49 = item.Preg49,
                                    Preg50 = item.Preg50,
                                    Preg51 = item.Preg51,
                                    Preg52 = item.Preg52,
                                    Preg53 = item.Preg53,
                                    Preg54 = item.Preg54,
                                    Preg55 = item.Preg55,
                                    Preg56 = item.Preg56,
                                    Preg57 = item.Preg57,
                                    Preg58 = item.Preg58,
                                    Preg59 = item.Preg59,
                                    Preg60 = item.Preg60,
                                    Preg61 = item.Preg61,
                                    Preg62 = item.Preg62,
                                    Preg63 = item.Preg63,
                                    Preg64 = item.Preg64,
                                    Preg65 = item.Preg65,
                                    Preg66 = item.Preg66,
                                    Preg67 = item.Preg67,
                                    Preg68 = item.Preg68,
                                    Preg69 = item.Preg69,
                                    Preg70 = item.Preg70,
                                    Preg71 = item.Preg71,
                                    Preg72 = item.Preg72,
                                    Preg73 = item.Preg73,
                                    Preg74 = item.Preg74,
                                    Preg75 = item.Preg75,
                                    Preg76 = item.Preg76,
                                    Preg77 = item.Preg77,
                                    Preg78 = item.Preg78,
                                    Preg79 = item.Preg79,
                                    Preg80 = item.Preg80,
                                    Preg81 = item.Preg81,
                                    Preg82 = item.Preg82,
                                    Preg83 = item.Preg83,
                                    Preg84 = item.Preg84,
                                    Preg85 = item.Preg85,
                                    Preg86 = item.Preg86,
                                    Administrativo =
                                        new ML.Administrativo
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Administrativo.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Administrativo.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Administrativo.Split('_')[2]),
                                        },
                                    Ant_1_a_2_anios =
                                        new ML.Ant_1_a_2_anios
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Ant_1_a_2_anios.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Ant_1_a_2_anios.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Ant_1_a_2_anios.Split('_')[2]),
                                        },
                                    Ant_3_a_5_anios =
                                        new ML.Ant_3_a_5_anios
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Ant_3_a_5_anios.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Ant_3_a_5_anios.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Ant_3_a_5_anios.Split('_')[2])
                                        },
                                    Ant_6_a_10_anios =
                                        new ML.Ant_6_a_10_anios
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Ant_6_a_10_anios.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Ant_6_a_10_anios.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Ant_6_a_10_anios.Split('_')[2])
                                        },
                                    Ant_6_meses_1_anio =
                                        new ML.Ant_6_meses_1_anio
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Ant_6_meses_1_anio.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Ant_6_meses_1_anio.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Ant_6_meses_1_anio.Split('_')[2])
                                        },
                                    Ant_mas_de_10_anios =
                                        new ML.Ant_mas_de_10_anios
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Ant_mas_de_10_anios.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Ant_mas_de_10_anios.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Ant_mas_de_10_anios.Split('_')[2])
                                        },
                                    Ant_menos_de_6_meses =
                                    new ML.Ant_menos_de_6_meses
                                    {
                                        Promedio66R = Convert.ToDecimal(item.Ant_menos_de_6_meses.Split('_')[0]),
                                        Promedio86R = Convert.ToDecimal(item.Ant_menos_de_6_meses.Split('_')[1]),
                                        Contestadas = Convert.ToInt32(item.Ant_menos_de_6_meses.Split('_')[2])
                                    },
                                    Comercial =
                                        new ML.Comercial
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Comercial.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Comercial.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Comercial.Split('_')[2])
                                        },
                                    Comisionistas =
                                        new ML.Comisionistas
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Comisionistas.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Comisionistas.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Comisionistas.Split('_')[2])
                                        },
                                    COORDINADOR_SUPERVISOR_JEFE =
                                        new ML.COORDINADOR_SUPERVISOR_JEFE
                                        {
                                            Promedio66R = Convert.ToDecimal(item.COORDINADOR_SUPERVISOR_JEFE.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.COORDINADOR_SUPERVISOR_JEFE.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.COORDINADOR_SUPERVISOR_JEFE.Split('_')[2])
                                        },
                                    Director =
                                        new ML.Director
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Director.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Director.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Director.Split('_')[2])
                                        },
                                    Edad_18_A_22_ANIOS =
                                        new ML.Edad_18_A_22_ANIOS
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Edad_18_A_22_ANIOS.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Edad_18_A_22_ANIOS.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Edad_18_A_22_ANIOS.Split('_')[2])
                                        },
                                    Edad_23_A_31_ANIOS =
                                        new ML.Edad_23_A_31_ANIOS
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Edad_23_A_31_ANIOS.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Edad_23_A_31_ANIOS.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Edad_23_A_31_ANIOS.Split('_')[2])
                                        },
                                    Edad_32_A_39_ANIOS =
                                        new ML.Edad_32_A_39_ANIOS
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Edad_32_A_39_ANIOS.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Edad_32_A_39_ANIOS.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Edad_32_A_39_ANIOS.Split('_')[2])
                                        },
                                    Edad_40_A_55_ANIOS =
                                        new ML.Edad_40_A_55_ANIOS
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Edad_40_A_55_ANIOS.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Edad_40_A_55_ANIOS.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Edad_40_A_55_ANIOS.Split('_')[2])
                                        },
                                    Edad_56_ANIOS_O_MAS =
                                        new ML.Edad_56_ANIOS_O_MAS
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Edad_56_ANIOS_O_MAS.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Edad_56_ANIOS_O_MAS.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Edad_56_ANIOS_O_MAS.Split('_')[2])
                                        },
                                    GerenteDepartamental =
                                        new ML.GerenteDepartamental
                                        {
                                            Promedio66R = Convert.ToDecimal(item.GerenteDepartamental.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.GerenteDepartamental.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.GerenteDepartamental.Split('_')[2])
                                        },
                                    GerenteGeneral =
                                        new ML.GerenteGeneral
                                        {
                                            Promedio66R = Convert.ToDecimal(item.GerenteGeneral.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.GerenteGeneral.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.GerenteGeneral.Split('_')[2])
                                        },
                                    Honorarios =
                                        new ML.Honorarios
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Honorarios.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Honorarios.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Honorarios.Split('_')[2])
                                        },
                                    Media_Superior =
                                        new ML.Media_Superior
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Media_Superior.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Media_Superior.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Media_Superior.Split('_')[2])
                                        },
                                    Media_Tecnica =
                                        new ML.Media_Tecnica
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Media_Tecnica.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Media_Tecnica.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Media_Tecnica.Split('_')[2])
                                        },
                                    Planta =
                                    new ML.Planta
                                    {
                                        Promedio66R = Convert.ToDecimal(item.Planta.Split('_')[0]),
                                        Promedio86R = Convert.ToDecimal(item.Planta.Split('_')[1]),
                                        Contestadas = Convert.ToInt32(item.Planta.Split('_')[2])
                                    },
                                    PostGrado =
                                        new ML.PostGrado
                                        {
                                            Promedio66R = Convert.ToDecimal(item.PostGrado.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.PostGrado.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.PostGrado.Split('_')[2])
                                        },
                                    Primaria =
                                        new ML.Primaria
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Primaria.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Primaria.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Primaria.Split('_')[2])
                                        },
                                    Secundaria =
                                        new ML.Secundaria
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Secundaria.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Secundaria.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Secundaria.Split('_')[2])
                                        },
                                    Sexo_Femenino =
                                        new ML.Sexo_Femenino
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Sexo_Femenino.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Sexo_Femenino.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Sexo_Femenino.Split('_')[2])
                                        },
                                    Sexo_Masculino =
                                        new ML.Sexo_Masculino
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Sexo_Masculino.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Sexo_Masculino.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Sexo_Masculino.Split('_')[2])
                                        },
                                    Sindicalizado =
                                        new ML.Sindicalizado
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Sindicalizado.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Sindicalizado.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Sindicalizado.Split('_')[2])
                                        },
                                    Subgerente =
                                        new ML.Subgerente
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Subgerente.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Subgerente.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Subgerente.Split('_')[2])
                                        },
                                    TECNICO_OPERATIVO =
                                        new ML.TECNICO_OPERATIVO
                                        {
                                            Promedio66R = Convert.ToDecimal(item.TECNICO_OPERATIVO.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.TECNICO_OPERATIVO.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.TECNICO_OPERATIVO.Split('_')[2])
                                        },
                                    Temporal =
                                        new ML.Temporal
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Temporal.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Temporal.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Temporal.Split('_')[2])
                                        },
                                    Universidad_Completa =
                                        new ML.Universidad_Completa
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Universidad_Completa.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Universidad_Completa.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Universidad_Completa.Split('_')[2])
                                        },
                                    Universidad_Incompleta =
                                        new ML.Universidad_Incompleta
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Universidad_Incompleta.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Universidad_Incompleta.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Universidad_Incompleta.Split('_')[2])
                                        },
                                    Contestadas = (int)item.Contestadas,
                                    //aHistorico.Enfoque == 1 ? "Enfoque empresa" : aHistorico.Enfoque == 2 ? "Enfoque Area" : "",
                                    Enfoque = item.Enfoque == "Enfoque empresa" ? 1 : item.Enfoque == "Enfoque Area" ? 2 : 0,
                                    Esperadas = (int)item.Esperadas,
                                    Promedio66R = (decimal)item.Promedio66R,
                                    Promedio86R = (decimal)item.Promedio86R
                                };
                                #endregion llenar objeto
                                list.Add(historico);
                            }
                        }
                        else
                        {
                            #region llenar objeto
                            ML.HistoricoClima historico = new ML.HistoricoClima()
                            {
                                Bienestar = 0,
                                Bio = 0,
                                Psico = 0,
                                Social = 0,
                                AlineacionEstrategica = 0, //.AlineacionEstrategica,
                                Anio = 0, //.Anio,
                                CalificacionGlobal = 0, //.CalificacionGlobal,
                                Coaching = 0, //.Coaching,
                                Companierismo = 0, //.Companierismo,
                                Creedibilidad = 0, //.Creedibilidad,
                                Entidad = "", //.Entidad,
                                EntidadId = (int)aHistorico.EntidadId, //.EntidadId,
                                EntidadNombre = aHistorico.EntidadNombre, //.EntidadNombre,
                                IdTipoEntidad = 0, //.IdTipoEntidad,
                                HabilidadesGerenciales = 0, //.HabilidadesGerenciales,
                                HC = 0, //.HC,
                                IdHistorico = 0, //.IdHistorico,
                                Imparcialidad = 0, //.Imparcialidad,
                                ManejoDelCambio = 0, //.ManejoDelCambio,
                                NivelColaboracion = 0, //.NivelColaboracion,
                                NivelCompromiso = 0, //.NivelCompromiso,
                                NivelConfianza = 0, //.NivelConfianza,
                                NivelParticipacion = 0, //.NivelParticipacion,
                                Orgullo = 0, //.Orgullo,
                                PracticasCulturales = 0, //.PracticasCulturales,
                                ProcesosOrganizacionales = 0, //.ProcesosOrganizacionales,
                                Respeto = 0, //.Respeto,
                                             //
                                Preg1 = 0, //.Preg1,
                                Preg2 = 0, //.Preg2,
                                Preg3 = 0, //.Preg3,
                                Preg4 = 0, //.Preg4,
                                Preg5 = 0, //.Preg5,
                                Preg6 = 0, //.Preg6,
                                Preg7 = 0, //.Preg7,
                                Preg8 = 0, //.Preg8,
                                Preg9 = 0, //.Preg9,
                                Preg10 = 0, //.Preg10,
                                Preg11 = 0, //.Preg11,
                                Preg12 = 0, //.Preg12,
                                Preg13 = 0, //.Preg13,
                                Preg14 = 0, //.Preg14,
                                Preg15 = 0, //.Preg15,
                                Preg16 = 0, //.Preg16,
                                Preg17 = 0, //.Preg17,
                                Preg18 = 0, //.Preg18,
                                Preg19 = 0, //.Preg19,
                                Preg20 = 0, //.Preg20,
                                Preg21 = 0, //.Preg21,
                                Preg22 = 0, //.Preg22,
                                Preg23 = 0, //.Preg23,
                                Preg24 = 0, //.Preg24,
                                Preg25 = 0, //.Preg25,
                                Preg26 = 0, //.Preg26,
                                Preg27 = 0, //.Preg27,
                                Preg28 = 0, //.Preg28,
                                Preg29 = 0, //.Preg29,
                                Preg30 = 0, //.Preg30,
                                Preg31 = 0, //.Preg31,
                                Preg32 = 0, //.Preg32,
                                Preg33 = 0, //.Preg33,
                                Preg34 = 0, //.Preg34,
                                Preg35 = 0, //.Preg35,
                                Preg36 = 0, //.Preg36,
                                Preg37 = 0, //.Preg37,
                                Preg38 = 0, //.Preg38,
                                Preg39 = 0, //.Preg39,
                                Preg40 = 0, //.Preg40,
                                Preg41 = 0, //.Preg41,
                                Preg42 = 0, //.Preg42,
                                Preg43 = 0, //.Preg43,
                                Preg44 = 0, //.Preg44,
                                Preg45 = 0, //.Preg45,
                                Preg46 = 0, //.Preg46,
                                Preg47 = 0, //.Preg47,
                                Preg48 = 0, //.Preg48,
                                Preg49 = 0, //.Preg49,
                                Preg50 = 0, //.Preg50,
                                Preg51 = 0, //.Preg51,
                                Preg52 = 0, //.Preg52,
                                Preg53 = 0, //.Preg53,
                                Preg54 = 0, //.Preg54,
                                Preg55 = 0, //.Preg55,
                                Preg56 = 0, //.Preg56,
                                Preg57 = 0, //.Preg57,
                                Preg58 = 0, //.Preg58,
                                Preg59 = 0, //.Preg59,
                                Preg60 = 0, //.Preg60,
                                Preg61 = 0, //.Preg61,
                                Preg62 = 0, //.Preg62,
                                Preg63 = 0, //.Preg63,
                                Preg64 = 0, //.Preg64,
                                Preg65 = 0, //.Preg65,
                                Preg66 = 0, //.Preg66,
                                Preg67 = 0, //.Preg67,
                                Preg68 = 0, //.Preg68,
                                Preg69 = 0, //.Preg69,
                                Preg70 = 0, //.Preg70,
                                Preg71 = 0, //.Preg71,
                                Preg72 = 0, //.Preg72,
                                Preg73 = 0, //.Preg73,
                                Preg74 = 0, //.Preg74,
                                Preg75 = 0, //.Preg75,
                                Preg76 = 0, //.Preg76,
                                Preg77 = 0, //.Preg77,
                                Preg78 = 0, //.Preg78,
                                Preg79 = 0, //.Preg79,
                                Preg80 = 0, //.Preg80,
                                Preg81 = 0, //.Preg81,
                                Preg82 = 0, //.Preg82,
                                Preg83 = 0, //.Preg83,
                                Preg84 = 0, //.Preg84,
                                Preg85 = 0, //.Preg85,
                                Preg86 = 0, //.Preg86,
                                Administrativo =
                                    new ML.Administrativo
                                    {
                                        Promedio66R = 0, // //.Administrativo.Split('_')[0]),
                                        Promedio86R = 0, // //.Administrativo.Split('_')[1]),
                                        Contestadas = 0, //, //.Administrativo.Split('_')[2]),
                                    },
                                Ant_1_a_2_anios =
                                    new ML.Ant_1_a_2_anios
                                    {
                                        Promedio66R = 0, // //.Ant_1_a_2_anios.Split('_')[0]),
                                        Promedio86R = 0, // //.Ant_1_a_2_anios.Split('_')[1]),
                                        Contestadas = 0, //, //.Ant_1_a_2_anios.Split('_')[2]),
                                    },
                                Ant_3_a_5_anios =
                                    new ML.Ant_3_a_5_anios
                                    {
                                        Promedio66R = 0, // //.Ant_3_a_5_anios.Split('_')[0]),
                                        Promedio86R = 0, // //.Ant_3_a_5_anios.Split('_')[1]),
                                        Contestadas = 0, //, //.Ant_3_a_5_anios.Split('_')[2])
                                    },
                                Ant_6_a_10_anios =
                                    new ML.Ant_6_a_10_anios
                                    {
                                        Promedio66R = 0, // //.Ant_6_a_10_anios.Split('_')[0]),
                                        Promedio86R = 0, // //.Ant_6_a_10_anios.Split('_')[1]),
                                        Contestadas = 0, //, //.Ant_6_a_10_anios.Split('_')[2])
                                    },
                                Ant_6_meses_1_anio =
                                    new ML.Ant_6_meses_1_anio
                                    {
                                        Promedio66R = 0, // //.Ant_6_meses_1_anio.Split('_')[0]),
                                        Promedio86R = 0, // //.Ant_6_meses_1_anio.Split('_')[1]),
                                        Contestadas = 0, //, //.Ant_6_meses_1_anio.Split('_')[2])
                                    },
                                Ant_mas_de_10_anios =
                                    new ML.Ant_mas_de_10_anios
                                    {
                                        Promedio66R = 0, // //.Ant_mas_de_10_anios.Split('_')[0]),
                                        Promedio86R = 0, // //.Ant_mas_de_10_anios.Split('_')[1]),
                                        Contestadas = 0, //, //.Ant_mas_de_10_anios.Split('_')[2])
                                    },
                                Ant_menos_de_6_meses =
                                new ML.Ant_menos_de_6_meses
                                {
                                    Promedio66R = 0, // //.Ant_menos_de_6_meses.Split('_')[0]),
                                    Promedio86R = 0, // //.Ant_menos_de_6_meses.Split('_')[1]),
                                    Contestadas = 0, //, //.Ant_menos_de_6_meses.Split('_')[2])
                                },
                                Comercial =
                                    new ML.Comercial
                                    {
                                        Promedio66R = 0, // //.Comercial.Split('_')[0]),
                                        Promedio86R = 0, // //.Comercial.Split('_')[1]),
                                        Contestadas = 0, //, //.Comercial.Split('_')[2])
                                    },
                                Comisionistas =
                                    new ML.Comisionistas
                                    {
                                        Promedio66R = 0, // //.Comisionistas.Split('_')[0]),
                                        Promedio86R = 0, // //.Comisionistas.Split('_')[1]),
                                        Contestadas = 0, //, //.Comisionistas.Split('_')[2])
                                    },
                                COORDINADOR_SUPERVISOR_JEFE =
                                    new ML.COORDINADOR_SUPERVISOR_JEFE
                                    {
                                        Promedio66R = 0, // //.COORDINADOR_SUPERVISOR_JEFE.Split('_')[0]),
                                        Promedio86R = 0, // //.COORDINADOR_SUPERVISOR_JEFE.Split('_')[1]),
                                        Contestadas = 0, //, //.COORDINADOR_SUPERVISOR_JEFE.Split('_')[2])
                                    },
                                Director =
                                    new ML.Director
                                    {
                                        Promedio66R = 0, // //.Director.Split('_')[0]),
                                        Promedio86R = 0, // //.Director.Split('_')[1]),
                                        Contestadas = 0, //, //.Director.Split('_')[2])
                                    },
                                Edad_18_A_22_ANIOS =
                                    new ML.Edad_18_A_22_ANIOS
                                    {
                                        Promedio66R = 0, // //.Edad_18_A_22_ANIOS.Split('_')[0]),
                                        Promedio86R = 0, // //.Edad_18_A_22_ANIOS.Split('_')[1]),
                                        Contestadas = 0, //, //.Edad_18_A_22_ANIOS.Split('_')[2])
                                    },
                                Edad_23_A_31_ANIOS =
                                    new ML.Edad_23_A_31_ANIOS
                                    {
                                        Promedio66R = 0, // //.Edad_23_A_31_ANIOS.Split('_')[0]),
                                        Promedio86R = 0, // //.Edad_23_A_31_ANIOS.Split('_')[1]),
                                        Contestadas = 0, //, //.Edad_23_A_31_ANIOS.Split('_')[2])
                                    },
                                Edad_32_A_39_ANIOS =
                                    new ML.Edad_32_A_39_ANIOS
                                    {
                                        Promedio66R = 0, // //.Edad_32_A_39_ANIOS.Split('_')[0]),
                                        Promedio86R = 0, // //.Edad_32_A_39_ANIOS.Split('_')[1]),
                                        Contestadas = 0, //, //.Edad_32_A_39_ANIOS.Split('_')[2])
                                    },
                                Edad_40_A_55_ANIOS =
                                    new ML.Edad_40_A_55_ANIOS
                                    {
                                        Promedio66R = 0, // //.Edad_40_A_55_ANIOS.Split('_')[0]),
                                        Promedio86R = 0, // //.Edad_40_A_55_ANIOS.Split('_')[1]),
                                        Contestadas = 0, //, //.Edad_40_A_55_ANIOS.Split('_')[2])
                                    },
                                Edad_56_ANIOS_O_MAS =
                                    new ML.Edad_56_ANIOS_O_MAS
                                    {
                                        Promedio66R = 0, // //.Edad_56_ANIOS_O_MAS.Split('_')[0]),
                                        Promedio86R = 0, // //.Edad_56_ANIOS_O_MAS.Split('_')[1]),
                                        Contestadas = 0, //, //.Edad_56_ANIOS_O_MAS.Split('_')[2])
                                    },
                                GerenteDepartamental =
                                    new ML.GerenteDepartamental
                                    {
                                        Promedio66R = 0, // //.GerenteDepartamental.Split('_')[0]),
                                        Promedio86R = 0, // //.GerenteDepartamental.Split('_')[1]),
                                        Contestadas = 0, //, //.GerenteDepartamental.Split('_')[2])
                                    },
                                GerenteGeneral =
                                    new ML.GerenteGeneral
                                    {
                                        Promedio66R = 0, // //.GerenteGeneral.Split('_')[0]),
                                        Promedio86R = 0, // //.GerenteGeneral.Split('_')[1]),
                                        Contestadas = 0, //, //.GerenteGeneral.Split('_')[2])
                                    },
                                Honorarios =
                                    new ML.Honorarios
                                    {
                                        Promedio66R = 0, // //.Honorarios.Split('_')[0]),
                                        Promedio86R = 0, // //.Honorarios.Split('_')[1]),
                                        Contestadas = 0, //, //.Honorarios.Split('_')[2])
                                    },
                                Media_Superior =
                                    new ML.Media_Superior
                                    {
                                        Promedio66R = 0, // //.Media_Superior.Split('_')[0]),
                                        Promedio86R = 0, // //.Media_Superior.Split('_')[1]),
                                        Contestadas = 0, //, //.Media_Superior.Split('_')[2])
                                    },
                                Media_Tecnica =
                                    new ML.Media_Tecnica
                                    {
                                        Promedio66R = 0, // //.Media_Tecnica.Split('_')[0]),
                                        Promedio86R = 0, // //.Media_Tecnica.Split('_')[1]),
                                        Contestadas = 0, //, //.Media_Tecnica.Split('_')[2])
                                    },
                                Planta =
                                new ML.Planta
                                {
                                    Promedio66R = 0, // //.Planta.Split('_')[0]),
                                    Promedio86R = 0, // //.Planta.Split('_')[1]),
                                    Contestadas = 0, //, //.Planta.Split('_')[2])
                                },
                                PostGrado =
                                    new ML.PostGrado
                                    {
                                        Promedio66R = 0, // //.PostGrado.Split('_')[0]),
                                        Promedio86R = 0, // //.PostGrado.Split('_')[1]),
                                        Contestadas = 0, //, //.PostGrado.Split('_')[2])
                                    },
                                Primaria =
                                    new ML.Primaria
                                    {
                                        Promedio66R = 0, // //.Primaria.Split('_')[0]),
                                        Promedio86R = 0, // //.Primaria.Split('_')[1]),
                                        Contestadas = 0, //, //.Primaria.Split('_')[2])
                                    },
                                Secundaria =
                                    new ML.Secundaria
                                    {
                                        Promedio66R = 0, // //.Secundaria.Split('_')[0]),
                                        Promedio86R = 0, // //.Secundaria.Split('_')[1]),
                                        Contestadas = 0, //, //.Secundaria.Split('_')[2])
                                    },
                                Sexo_Femenino =
                                    new ML.Sexo_Femenino
                                    {
                                        Promedio66R = 0, // //.Sexo_Femenino.Split('_')[0]),
                                        Promedio86R = 0, // //.Sexo_Femenino.Split('_')[1]),
                                        Contestadas = 0, //, //.Sexo_Femenino.Split('_')[2])
                                    },
                                Sexo_Masculino =
                                    new ML.Sexo_Masculino
                                    {
                                        Promedio66R = 0, // //.Sexo_Masculino.Split('_')[0]),
                                        Promedio86R = 0, // //.Sexo_Masculino.Split('_')[1]),
                                        Contestadas = 0, //, //.Sexo_Masculino.Split('_')[2])
                                    },
                                Sindicalizado =
                                    new ML.Sindicalizado
                                    {
                                        Promedio66R = 0, // //.Sindicalizado.Split('_')[0]),
                                        Promedio86R = 0, // //.Sindicalizado.Split('_')[1]),
                                        Contestadas = 0, //, //.Sindicalizado.Split('_')[2])
                                    },
                                Subgerente =
                                    new ML.Subgerente
                                    {
                                        Promedio66R = 0, // //.Subgerente.Split('_')[0]),
                                        Promedio86R = 0, // //.Subgerente.Split('_')[1]),
                                        Contestadas = 0, //, //.Subgerente.Split('_')[2])
                                    },
                                TECNICO_OPERATIVO =
                                    new ML.TECNICO_OPERATIVO
                                    {
                                        Promedio66R = 0, // //.TECNICO_OPERATIVO.Split('_')[0]),
                                        Promedio86R = 0, // //.TECNICO_OPERATIVO.Split('_')[1]),
                                        Contestadas = 0, //, //.TECNICO_OPERATIVO.Split('_')[2])
                                    },
                                Temporal =
                                    new ML.Temporal
                                    {
                                        Promedio66R = 0, // //.Temporal.Split('_')[0]),
                                        Promedio86R = 0, // //.Temporal.Split('_')[1]),
                                        Contestadas = 0, //, //.Temporal.Split('_')[2])
                                    },
                                Universidad_Completa =
                                    new ML.Universidad_Completa
                                    {
                                        Promedio66R = 0, // //.Universidad_Completa.Split('_')[0]),
                                        Promedio86R = 0, // //.Universidad_Completa.Split('_')[1]),
                                        Contestadas = 0, //, //.Universidad_Completa.Split('_')[2])
                                    },
                                Universidad_Incompleta =
                                    new ML.Universidad_Incompleta
                                    {
                                        Promedio66R = 0, // //.Universidad_Incompleta.Split('_')[0]),
                                        Promedio86R = 0, // //.Universidad_Incompleta.Split('_')[1]),
                                        Contestadas = 0, //, //.Universidad_Incompleta.Split('_')[2])
                                    },
                                Contestadas = (int)0, //.Contestadas,
                                                      //aHistorico.Enfoque == 1 ? "Enfoque empresa" : aHistorico.Enfoque == 2 ? "Enfoque Area" : "",
                                Enfoque = 0, //.Enfoque == "Enfoque empresa" ? 1 : 0, //.Enfoque == "Enfoque Area" ? 2 : 0,
                                Esperadas = (int)0, //.Esperadas,
                                Promedio66R = (decimal)0, //.Promedio66R,
                                Promedio86R = (decimal)0, //.Promedio86R
                            };
                            #endregion llenar objeto
                            list.Add(historico);
                        }
                    }
                }
            }
            catch (Exception aE)
            {
                BL.LogReporteoClima.writteLog(aE, new StackTrace());
                return new List<ML.HistoricoClima>();
            }
            return list;
        }

        public static List<ML.HistoricoClima> getHistorico_2EABienestar(ML.Historico aHistorico)
        {
            var list = new List<ML.HistoricoClima>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.HistoricoClima.Select(o => o)
                        .Where(o => o.EntidadNombre == aHistorico.EntidadNombre && o.Anio == aHistorico.Anio && o.Enfoque == "Enfoque Area").ToList();

                    if (query != null)
                    {
                        if (query.Count > 0)
                        {
                            foreach (var item in query)
                            {
                                #region llenar objeto
                                ML.HistoricoClima historico = new ML.HistoricoClima()
                                {
                                    Bienestar = item.Bienestar,
                                    Bio = item.Bio,
                                    Psico = item.Psico,
                                    Social = item.Social,
                                    AlineacionEstrategica = item.AlineacionEstrategica,
                                    Anio = item.Anio,
                                    CalificacionGlobal = item.CalificacionGlobal,
                                    Coaching = item.Coaching,
                                    Companierismo = item.Companierismo,
                                    Creedibilidad = item.Creedibilidad,
                                    Entidad = item.Entidad,
                                    EntidadId = (int)item.EntidadId,
                                    EntidadNombre = item.EntidadNombre,
                                    IdTipoEntidad = item.IdTipoEntidad,
                                    HabilidadesGerenciales = item.HabilidadesGerenciales,
                                    HC = item.HC,
                                    IdHistorico = item.IdHistorico,
                                    Imparcialidad = item.Imparcialidad,
                                    ManejoDelCambio = item.ManejoDelCambio,
                                    NivelColaboracion = item.NivelColaboracion,
                                    NivelCompromiso = item.NivelCompromiso,
                                    NivelConfianza = item.NivelConfianza,
                                    NivelParticipacion = item.NivelParticipacion,
                                    Orgullo = item.Orgullo,
                                    PracticasCulturales = item.PracticasCulturales,
                                    ProcesosOrganizacionales = item.ProcesosOrganizacionales,
                                    Respeto = item.Respeto,
                                    //
                                    Preg1 = item.Preg1,
                                    Preg2 = item.Preg2,
                                    Preg3 = item.Preg3,
                                    Preg4 = item.Preg4,
                                    Preg5 = item.Preg5,
                                    Preg6 = item.Preg6,
                                    Preg7 = item.Preg7,
                                    Preg8 = item.Preg8,
                                    Preg9 = item.Preg9,
                                    Preg10 = item.Preg10,
                                    Preg11 = item.Preg11,
                                    Preg12 = item.Preg12,
                                    Preg13 = item.Preg13,
                                    Preg14 = item.Preg14,
                                    Preg15 = item.Preg15,
                                    Preg16 = item.Preg16,
                                    Preg17 = item.Preg17,
                                    Preg18 = item.Preg18,
                                    Preg19 = item.Preg19,
                                    Preg20 = item.Preg20,
                                    Preg21 = item.Preg21,
                                    Preg22 = item.Preg22,
                                    Preg23 = item.Preg23,
                                    Preg24 = item.Preg24,
                                    Preg25 = item.Preg25,
                                    Preg26 = item.Preg26,
                                    Preg27 = item.Preg27,
                                    Preg28 = item.Preg28,
                                    Preg29 = item.Preg29,
                                    Preg30 = item.Preg30,
                                    Preg31 = item.Preg31,
                                    Preg32 = item.Preg32,
                                    Preg33 = item.Preg33,
                                    Preg34 = item.Preg34,
                                    Preg35 = item.Preg35,
                                    Preg36 = item.Preg36,
                                    Preg37 = item.Preg37,
                                    Preg38 = item.Preg38,
                                    Preg39 = item.Preg39,
                                    Preg40 = item.Preg40,
                                    Preg41 = item.Preg41,
                                    Preg42 = item.Preg42,
                                    Preg43 = item.Preg43,
                                    Preg44 = item.Preg44,
                                    Preg45 = item.Preg45,
                                    Preg46 = item.Preg46,
                                    Preg47 = item.Preg47,
                                    Preg48 = item.Preg48,
                                    Preg49 = item.Preg49,
                                    Preg50 = item.Preg50,
                                    Preg51 = item.Preg51,
                                    Preg52 = item.Preg52,
                                    Preg53 = item.Preg53,
                                    Preg54 = item.Preg54,
                                    Preg55 = item.Preg55,
                                    Preg56 = item.Preg56,
                                    Preg57 = item.Preg57,
                                    Preg58 = item.Preg58,
                                    Preg59 = item.Preg59,
                                    Preg60 = item.Preg60,
                                    Preg61 = item.Preg61,
                                    Preg62 = item.Preg62,
                                    Preg63 = item.Preg63,
                                    Preg64 = item.Preg64,
                                    Preg65 = item.Preg65,
                                    Preg66 = item.Preg66,
                                    Preg67 = item.Preg67,
                                    Preg68 = item.Preg68,
                                    Preg69 = item.Preg69,
                                    Preg70 = item.Preg70,
                                    Preg71 = item.Preg71,
                                    Preg72 = item.Preg72,
                                    Preg73 = item.Preg73,
                                    Preg74 = item.Preg74,
                                    Preg75 = item.Preg75,
                                    Preg76 = item.Preg76,
                                    Preg77 = item.Preg77,
                                    Preg78 = item.Preg78,
                                    Preg79 = item.Preg79,
                                    Preg80 = item.Preg80,
                                    Preg81 = item.Preg81,
                                    Preg82 = item.Preg82,
                                    Preg83 = item.Preg83,
                                    Preg84 = item.Preg84,
                                    Preg85 = item.Preg85,
                                    Preg86 = item.Preg86,
                                    Administrativo =
                                        new ML.Administrativo
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Administrativo.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Administrativo.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Administrativo.Split('_')[2]),
                                        },
                                    Ant_1_a_2_anios =
                                        new ML.Ant_1_a_2_anios
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Ant_1_a_2_anios.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Ant_1_a_2_anios.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Ant_1_a_2_anios.Split('_')[2]),
                                        },
                                    Ant_3_a_5_anios =
                                        new ML.Ant_3_a_5_anios
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Ant_3_a_5_anios.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Ant_3_a_5_anios.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Ant_3_a_5_anios.Split('_')[2])
                                        },
                                    Ant_6_a_10_anios =
                                        new ML.Ant_6_a_10_anios
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Ant_6_a_10_anios.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Ant_6_a_10_anios.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Ant_6_a_10_anios.Split('_')[2])
                                        },
                                    Ant_6_meses_1_anio =
                                        new ML.Ant_6_meses_1_anio
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Ant_6_meses_1_anio.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Ant_6_meses_1_anio.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Ant_6_meses_1_anio.Split('_')[2])
                                        },
                                    Ant_mas_de_10_anios =
                                        new ML.Ant_mas_de_10_anios
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Ant_mas_de_10_anios.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Ant_mas_de_10_anios.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Ant_mas_de_10_anios.Split('_')[2])
                                        },
                                    Ant_menos_de_6_meses =
                                    new ML.Ant_menos_de_6_meses
                                    {
                                        Promedio66R = Convert.ToDecimal(item.Ant_menos_de_6_meses.Split('_')[0]),
                                        Promedio86R = Convert.ToDecimal(item.Ant_menos_de_6_meses.Split('_')[1]),
                                        Contestadas = Convert.ToInt32(item.Ant_menos_de_6_meses.Split('_')[2])
                                    },
                                    Comercial =
                                        new ML.Comercial
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Comercial.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Comercial.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Comercial.Split('_')[2])
                                        },
                                    Comisionistas =
                                        new ML.Comisionistas
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Comisionistas.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Comisionistas.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Comisionistas.Split('_')[2])
                                        },
                                    COORDINADOR_SUPERVISOR_JEFE =
                                        new ML.COORDINADOR_SUPERVISOR_JEFE
                                        {
                                            Promedio66R = Convert.ToDecimal(item.COORDINADOR_SUPERVISOR_JEFE.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.COORDINADOR_SUPERVISOR_JEFE.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.COORDINADOR_SUPERVISOR_JEFE.Split('_')[2])
                                        },
                                    Director =
                                        new ML.Director
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Director.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Director.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Director.Split('_')[2])
                                        },
                                    Edad_18_A_22_ANIOS =
                                        new ML.Edad_18_A_22_ANIOS
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Edad_18_A_22_ANIOS.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Edad_18_A_22_ANIOS.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Edad_18_A_22_ANIOS.Split('_')[2])
                                        },
                                    Edad_23_A_31_ANIOS =
                                        new ML.Edad_23_A_31_ANIOS
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Edad_23_A_31_ANIOS.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Edad_23_A_31_ANIOS.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Edad_23_A_31_ANIOS.Split('_')[2])
                                        },
                                    Edad_32_A_39_ANIOS =
                                        new ML.Edad_32_A_39_ANIOS
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Edad_32_A_39_ANIOS.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Edad_32_A_39_ANIOS.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Edad_32_A_39_ANIOS.Split('_')[2])
                                        },
                                    Edad_40_A_55_ANIOS =
                                        new ML.Edad_40_A_55_ANIOS
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Edad_40_A_55_ANIOS.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Edad_40_A_55_ANIOS.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Edad_40_A_55_ANIOS.Split('_')[2])
                                        },
                                    Edad_56_ANIOS_O_MAS =
                                        new ML.Edad_56_ANIOS_O_MAS
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Edad_56_ANIOS_O_MAS.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Edad_56_ANIOS_O_MAS.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Edad_56_ANIOS_O_MAS.Split('_')[2])
                                        },
                                    GerenteDepartamental =
                                        new ML.GerenteDepartamental
                                        {
                                            Promedio66R = Convert.ToDecimal(item.GerenteDepartamental.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.GerenteDepartamental.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.GerenteDepartamental.Split('_')[2])
                                        },
                                    GerenteGeneral =
                                        new ML.GerenteGeneral
                                        {
                                            Promedio66R = Convert.ToDecimal(item.GerenteGeneral.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.GerenteGeneral.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.GerenteGeneral.Split('_')[2])
                                        },
                                    Honorarios =
                                        new ML.Honorarios
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Honorarios.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Honorarios.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Honorarios.Split('_')[2])
                                        },
                                    Media_Superior =
                                        new ML.Media_Superior
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Media_Superior.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Media_Superior.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Media_Superior.Split('_')[2])
                                        },
                                    Media_Tecnica =
                                        new ML.Media_Tecnica
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Media_Tecnica.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Media_Tecnica.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Media_Tecnica.Split('_')[2])
                                        },
                                    Planta =
                                    new ML.Planta
                                    {
                                        Promedio66R = Convert.ToDecimal(item.Planta.Split('_')[0]),
                                        Promedio86R = Convert.ToDecimal(item.Planta.Split('_')[1]),
                                        Contestadas = Convert.ToInt32(item.Planta.Split('_')[2])
                                    },
                                    PostGrado =
                                        new ML.PostGrado
                                        {
                                            Promedio66R = Convert.ToDecimal(item.PostGrado.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.PostGrado.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.PostGrado.Split('_')[2])
                                        },
                                    Primaria =
                                        new ML.Primaria
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Primaria.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Primaria.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Primaria.Split('_')[2])
                                        },
                                    Secundaria =
                                        new ML.Secundaria
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Secundaria.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Secundaria.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Secundaria.Split('_')[2])
                                        },
                                    Sexo_Femenino =
                                        new ML.Sexo_Femenino
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Sexo_Femenino.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Sexo_Femenino.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Sexo_Femenino.Split('_')[2])
                                        },
                                    Sexo_Masculino =
                                        new ML.Sexo_Masculino
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Sexo_Masculino.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Sexo_Masculino.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Sexo_Masculino.Split('_')[2])
                                        },
                                    Sindicalizado =
                                        new ML.Sindicalizado
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Sindicalizado.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Sindicalizado.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Sindicalizado.Split('_')[2])
                                        },
                                    Subgerente =
                                        new ML.Subgerente
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Subgerente.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Subgerente.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Subgerente.Split('_')[2])
                                        },
                                    TECNICO_OPERATIVO =
                                        new ML.TECNICO_OPERATIVO
                                        {
                                            Promedio66R = Convert.ToDecimal(item.TECNICO_OPERATIVO.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.TECNICO_OPERATIVO.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.TECNICO_OPERATIVO.Split('_')[2])
                                        },
                                    Temporal =
                                        new ML.Temporal
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Temporal.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Temporal.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Temporal.Split('_')[2])
                                        },
                                    Universidad_Completa =
                                        new ML.Universidad_Completa
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Universidad_Completa.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Universidad_Completa.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Universidad_Completa.Split('_')[2])
                                        },
                                    Universidad_Incompleta =
                                        new ML.Universidad_Incompleta
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Universidad_Incompleta.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Universidad_Incompleta.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Universidad_Incompleta.Split('_')[2])
                                        },
                                    Contestadas = (int)item.Contestadas,
                                    //aHistorico.Enfoque == 1 ? "Enfoque empresa" : aHistorico.Enfoque == 2 ? "Enfoque Area" : "",
                                    Enfoque = item.Enfoque == "Enfoque empresa" ? 1 : item.Enfoque == "Enfoque Area" ? 2 : 0,
                                    Esperadas = (int)item.Esperadas,
                                    Promedio66R = (decimal)item.Promedio66R,
                                    Promedio86R = (decimal)item.Promedio86R
                                };
                                #endregion llenar objeto
                                list.Add(historico);
                            }
                        }
                        else
                        {
                            #region llenar objeto
                            ML.HistoricoClima historico = new ML.HistoricoClima()
                            {
                                Bienestar = 0,
                                Bio = 0,
                                Psico = 0,
                                Social = 0,
                                AlineacionEstrategica = 0, //.AlineacionEstrategica,
                                Anio = 0, //.Anio,
                                CalificacionGlobal = 0, //.CalificacionGlobal,
                                Coaching = 0, //.Coaching,
                                Companierismo = 0, //.Companierismo,
                                Creedibilidad = 0, //.Creedibilidad,
                                Entidad = "", //.Entidad,
                                EntidadId = (int)aHistorico.EntidadId, //.EntidadId,
                                EntidadNombre = aHistorico.EntidadNombre, //.EntidadNombre,
                                IdTipoEntidad = 0, //.IdTipoEntidad,
                                HabilidadesGerenciales = 0, //.HabilidadesGerenciales,
                                HC = 0, //.HC,
                                IdHistorico = 0, //.IdHistorico,
                                Imparcialidad = 0, //.Imparcialidad,
                                ManejoDelCambio = 0, //.ManejoDelCambio,
                                NivelColaboracion = 0, //.NivelColaboracion,
                                NivelCompromiso = 0, //.NivelCompromiso,
                                NivelConfianza = 0, //.NivelConfianza,
                                NivelParticipacion = 0, //.NivelParticipacion,
                                Orgullo = 0, //.Orgullo,
                                PracticasCulturales = 0, //.PracticasCulturales,
                                ProcesosOrganizacionales = 0, //.ProcesosOrganizacionales,
                                Respeto = 0, //.Respeto,
                                             //
                                Preg1 = 0, //.Preg1,
                                Preg2 = 0, //.Preg2,
                                Preg3 = 0, //.Preg3,
                                Preg4 = 0, //.Preg4,
                                Preg5 = 0, //.Preg5,
                                Preg6 = 0, //.Preg6,
                                Preg7 = 0, //.Preg7,
                                Preg8 = 0, //.Preg8,
                                Preg9 = 0, //.Preg9,
                                Preg10 = 0, //.Preg10,
                                Preg11 = 0, //.Preg11,
                                Preg12 = 0, //.Preg12,
                                Preg13 = 0, //.Preg13,
                                Preg14 = 0, //.Preg14,
                                Preg15 = 0, //.Preg15,
                                Preg16 = 0, //.Preg16,
                                Preg17 = 0, //.Preg17,
                                Preg18 = 0, //.Preg18,
                                Preg19 = 0, //.Preg19,
                                Preg20 = 0, //.Preg20,
                                Preg21 = 0, //.Preg21,
                                Preg22 = 0, //.Preg22,
                                Preg23 = 0, //.Preg23,
                                Preg24 = 0, //.Preg24,
                                Preg25 = 0, //.Preg25,
                                Preg26 = 0, //.Preg26,
                                Preg27 = 0, //.Preg27,
                                Preg28 = 0, //.Preg28,
                                Preg29 = 0, //.Preg29,
                                Preg30 = 0, //.Preg30,
                                Preg31 = 0, //.Preg31,
                                Preg32 = 0, //.Preg32,
                                Preg33 = 0, //.Preg33,
                                Preg34 = 0, //.Preg34,
                                Preg35 = 0, //.Preg35,
                                Preg36 = 0, //.Preg36,
                                Preg37 = 0, //.Preg37,
                                Preg38 = 0, //.Preg38,
                                Preg39 = 0, //.Preg39,
                                Preg40 = 0, //.Preg40,
                                Preg41 = 0, //.Preg41,
                                Preg42 = 0, //.Preg42,
                                Preg43 = 0, //.Preg43,
                                Preg44 = 0, //.Preg44,
                                Preg45 = 0, //.Preg45,
                                Preg46 = 0, //.Preg46,
                                Preg47 = 0, //.Preg47,
                                Preg48 = 0, //.Preg48,
                                Preg49 = 0, //.Preg49,
                                Preg50 = 0, //.Preg50,
                                Preg51 = 0, //.Preg51,
                                Preg52 = 0, //.Preg52,
                                Preg53 = 0, //.Preg53,
                                Preg54 = 0, //.Preg54,
                                Preg55 = 0, //.Preg55,
                                Preg56 = 0, //.Preg56,
                                Preg57 = 0, //.Preg57,
                                Preg58 = 0, //.Preg58,
                                Preg59 = 0, //.Preg59,
                                Preg60 = 0, //.Preg60,
                                Preg61 = 0, //.Preg61,
                                Preg62 = 0, //.Preg62,
                                Preg63 = 0, //.Preg63,
                                Preg64 = 0, //.Preg64,
                                Preg65 = 0, //.Preg65,
                                Preg66 = 0, //.Preg66,
                                Preg67 = 0, //.Preg67,
                                Preg68 = 0, //.Preg68,
                                Preg69 = 0, //.Preg69,
                                Preg70 = 0, //.Preg70,
                                Preg71 = 0, //.Preg71,
                                Preg72 = 0, //.Preg72,
                                Preg73 = 0, //.Preg73,
                                Preg74 = 0, //.Preg74,
                                Preg75 = 0, //.Preg75,
                                Preg76 = 0, //.Preg76,
                                Preg77 = 0, //.Preg77,
                                Preg78 = 0, //.Preg78,
                                Preg79 = 0, //.Preg79,
                                Preg80 = 0, //.Preg80,
                                Preg81 = 0, //.Preg81,
                                Preg82 = 0, //.Preg82,
                                Preg83 = 0, //.Preg83,
                                Preg84 = 0, //.Preg84,
                                Preg85 = 0, //.Preg85,
                                Preg86 = 0, //.Preg86,
                                Administrativo =
                                    new ML.Administrativo
                                    {
                                        Promedio66R = 0, // //.Administrativo.Split('_')[0]),
                                        Promedio86R = 0, // //.Administrativo.Split('_')[1]),
                                        Contestadas = 0, //, //.Administrativo.Split('_')[2]),
                                    },
                                Ant_1_a_2_anios =
                                    new ML.Ant_1_a_2_anios
                                    {
                                        Promedio66R = 0, // //.Ant_1_a_2_anios.Split('_')[0]),
                                        Promedio86R = 0, // //.Ant_1_a_2_anios.Split('_')[1]),
                                        Contestadas = 0, //, //.Ant_1_a_2_anios.Split('_')[2]),
                                    },
                                Ant_3_a_5_anios =
                                    new ML.Ant_3_a_5_anios
                                    {
                                        Promedio66R = 0, // //.Ant_3_a_5_anios.Split('_')[0]),
                                        Promedio86R = 0, // //.Ant_3_a_5_anios.Split('_')[1]),
                                        Contestadas = 0, //, //.Ant_3_a_5_anios.Split('_')[2])
                                    },
                                Ant_6_a_10_anios =
                                    new ML.Ant_6_a_10_anios
                                    {
                                        Promedio66R = 0, // //.Ant_6_a_10_anios.Split('_')[0]),
                                        Promedio86R = 0, // //.Ant_6_a_10_anios.Split('_')[1]),
                                        Contestadas = 0, //, //.Ant_6_a_10_anios.Split('_')[2])
                                    },
                                Ant_6_meses_1_anio =
                                    new ML.Ant_6_meses_1_anio
                                    {
                                        Promedio66R = 0, // //.Ant_6_meses_1_anio.Split('_')[0]),
                                        Promedio86R = 0, // //.Ant_6_meses_1_anio.Split('_')[1]),
                                        Contestadas = 0, //, //.Ant_6_meses_1_anio.Split('_')[2])
                                    },
                                Ant_mas_de_10_anios =
                                    new ML.Ant_mas_de_10_anios
                                    {
                                        Promedio66R = 0, // //.Ant_mas_de_10_anios.Split('_')[0]),
                                        Promedio86R = 0, // //.Ant_mas_de_10_anios.Split('_')[1]),
                                        Contestadas = 0, //, //.Ant_mas_de_10_anios.Split('_')[2])
                                    },
                                Ant_menos_de_6_meses =
                                new ML.Ant_menos_de_6_meses
                                {
                                    Promedio66R = 0, // //.Ant_menos_de_6_meses.Split('_')[0]),
                                    Promedio86R = 0, // //.Ant_menos_de_6_meses.Split('_')[1]),
                                    Contestadas = 0, //, //.Ant_menos_de_6_meses.Split('_')[2])
                                },
                                Comercial =
                                    new ML.Comercial
                                    {
                                        Promedio66R = 0, // //.Comercial.Split('_')[0]),
                                        Promedio86R = 0, // //.Comercial.Split('_')[1]),
                                        Contestadas = 0, //, //.Comercial.Split('_')[2])
                                    },
                                Comisionistas =
                                    new ML.Comisionistas
                                    {
                                        Promedio66R = 0, // //.Comisionistas.Split('_')[0]),
                                        Promedio86R = 0, // //.Comisionistas.Split('_')[1]),
                                        Contestadas = 0, //, //.Comisionistas.Split('_')[2])
                                    },
                                COORDINADOR_SUPERVISOR_JEFE =
                                    new ML.COORDINADOR_SUPERVISOR_JEFE
                                    {
                                        Promedio66R = 0, // //.COORDINADOR_SUPERVISOR_JEFE.Split('_')[0]),
                                        Promedio86R = 0, // //.COORDINADOR_SUPERVISOR_JEFE.Split('_')[1]),
                                        Contestadas = 0, //, //.COORDINADOR_SUPERVISOR_JEFE.Split('_')[2])
                                    },
                                Director =
                                    new ML.Director
                                    {
                                        Promedio66R = 0, // //.Director.Split('_')[0]),
                                        Promedio86R = 0, // //.Director.Split('_')[1]),
                                        Contestadas = 0, //, //.Director.Split('_')[2])
                                    },
                                Edad_18_A_22_ANIOS =
                                    new ML.Edad_18_A_22_ANIOS
                                    {
                                        Promedio66R = 0, // //.Edad_18_A_22_ANIOS.Split('_')[0]),
                                        Promedio86R = 0, // //.Edad_18_A_22_ANIOS.Split('_')[1]),
                                        Contestadas = 0, //, //.Edad_18_A_22_ANIOS.Split('_')[2])
                                    },
                                Edad_23_A_31_ANIOS =
                                    new ML.Edad_23_A_31_ANIOS
                                    {
                                        Promedio66R = 0, // //.Edad_23_A_31_ANIOS.Split('_')[0]),
                                        Promedio86R = 0, // //.Edad_23_A_31_ANIOS.Split('_')[1]),
                                        Contestadas = 0, //, //.Edad_23_A_31_ANIOS.Split('_')[2])
                                    },
                                Edad_32_A_39_ANIOS =
                                    new ML.Edad_32_A_39_ANIOS
                                    {
                                        Promedio66R = 0, // //.Edad_32_A_39_ANIOS.Split('_')[0]),
                                        Promedio86R = 0, // //.Edad_32_A_39_ANIOS.Split('_')[1]),
                                        Contestadas = 0, //, //.Edad_32_A_39_ANIOS.Split('_')[2])
                                    },
                                Edad_40_A_55_ANIOS =
                                    new ML.Edad_40_A_55_ANIOS
                                    {
                                        Promedio66R = 0, // //.Edad_40_A_55_ANIOS.Split('_')[0]),
                                        Promedio86R = 0, // //.Edad_40_A_55_ANIOS.Split('_')[1]),
                                        Contestadas = 0, //, //.Edad_40_A_55_ANIOS.Split('_')[2])
                                    },
                                Edad_56_ANIOS_O_MAS =
                                    new ML.Edad_56_ANIOS_O_MAS
                                    {
                                        Promedio66R = 0, // //.Edad_56_ANIOS_O_MAS.Split('_')[0]),
                                        Promedio86R = 0, // //.Edad_56_ANIOS_O_MAS.Split('_')[1]),
                                        Contestadas = 0, //, //.Edad_56_ANIOS_O_MAS.Split('_')[2])
                                    },
                                GerenteDepartamental =
                                    new ML.GerenteDepartamental
                                    {
                                        Promedio66R = 0, // //.GerenteDepartamental.Split('_')[0]),
                                        Promedio86R = 0, // //.GerenteDepartamental.Split('_')[1]),
                                        Contestadas = 0, //, //.GerenteDepartamental.Split('_')[2])
                                    },
                                GerenteGeneral =
                                    new ML.GerenteGeneral
                                    {
                                        Promedio66R = 0, // //.GerenteGeneral.Split('_')[0]),
                                        Promedio86R = 0, // //.GerenteGeneral.Split('_')[1]),
                                        Contestadas = 0, //, //.GerenteGeneral.Split('_')[2])
                                    },
                                Honorarios =
                                    new ML.Honorarios
                                    {
                                        Promedio66R = 0, // //.Honorarios.Split('_')[0]),
                                        Promedio86R = 0, // //.Honorarios.Split('_')[1]),
                                        Contestadas = 0, //, //.Honorarios.Split('_')[2])
                                    },
                                Media_Superior =
                                    new ML.Media_Superior
                                    {
                                        Promedio66R = 0, // //.Media_Superior.Split('_')[0]),
                                        Promedio86R = 0, // //.Media_Superior.Split('_')[1]),
                                        Contestadas = 0, //, //.Media_Superior.Split('_')[2])
                                    },
                                Media_Tecnica =
                                    new ML.Media_Tecnica
                                    {
                                        Promedio66R = 0, // //.Media_Tecnica.Split('_')[0]),
                                        Promedio86R = 0, // //.Media_Tecnica.Split('_')[1]),
                                        Contestadas = 0, //, //.Media_Tecnica.Split('_')[2])
                                    },
                                Planta =
                                new ML.Planta
                                {
                                    Promedio66R = 0, // //.Planta.Split('_')[0]),
                                    Promedio86R = 0, // //.Planta.Split('_')[1]),
                                    Contestadas = 0, //, //.Planta.Split('_')[2])
                                },
                                PostGrado =
                                    new ML.PostGrado
                                    {
                                        Promedio66R = 0, // //.PostGrado.Split('_')[0]),
                                        Promedio86R = 0, // //.PostGrado.Split('_')[1]),
                                        Contestadas = 0, //, //.PostGrado.Split('_')[2])
                                    },
                                Primaria =
                                    new ML.Primaria
                                    {
                                        Promedio66R = 0, // //.Primaria.Split('_')[0]),
                                        Promedio86R = 0, // //.Primaria.Split('_')[1]),
                                        Contestadas = 0, //, //.Primaria.Split('_')[2])
                                    },
                                Secundaria =
                                    new ML.Secundaria
                                    {
                                        Promedio66R = 0, // //.Secundaria.Split('_')[0]),
                                        Promedio86R = 0, // //.Secundaria.Split('_')[1]),
                                        Contestadas = 0, //, //.Secundaria.Split('_')[2])
                                    },
                                Sexo_Femenino =
                                    new ML.Sexo_Femenino
                                    {
                                        Promedio66R = 0, // //.Sexo_Femenino.Split('_')[0]),
                                        Promedio86R = 0, // //.Sexo_Femenino.Split('_')[1]),
                                        Contestadas = 0, //, //.Sexo_Femenino.Split('_')[2])
                                    },
                                Sexo_Masculino =
                                    new ML.Sexo_Masculino
                                    {
                                        Promedio66R = 0, // //.Sexo_Masculino.Split('_')[0]),
                                        Promedio86R = 0, // //.Sexo_Masculino.Split('_')[1]),
                                        Contestadas = 0, //, //.Sexo_Masculino.Split('_')[2])
                                    },
                                Sindicalizado =
                                    new ML.Sindicalizado
                                    {
                                        Promedio66R = 0, // //.Sindicalizado.Split('_')[0]),
                                        Promedio86R = 0, // //.Sindicalizado.Split('_')[1]),
                                        Contestadas = 0, //, //.Sindicalizado.Split('_')[2])
                                    },
                                Subgerente =
                                    new ML.Subgerente
                                    {
                                        Promedio66R = 0, // //.Subgerente.Split('_')[0]),
                                        Promedio86R = 0, // //.Subgerente.Split('_')[1]),
                                        Contestadas = 0, //, //.Subgerente.Split('_')[2])
                                    },
                                TECNICO_OPERATIVO =
                                    new ML.TECNICO_OPERATIVO
                                    {
                                        Promedio66R = 0, // //.TECNICO_OPERATIVO.Split('_')[0]),
                                        Promedio86R = 0, // //.TECNICO_OPERATIVO.Split('_')[1]),
                                        Contestadas = 0, //, //.TECNICO_OPERATIVO.Split('_')[2])
                                    },
                                Temporal =
                                    new ML.Temporal
                                    {
                                        Promedio66R = 0, // //.Temporal.Split('_')[0]),
                                        Promedio86R = 0, // //.Temporal.Split('_')[1]),
                                        Contestadas = 0, //, //.Temporal.Split('_')[2])
                                    },
                                Universidad_Completa =
                                    new ML.Universidad_Completa
                                    {
                                        Promedio66R = 0, // //.Universidad_Completa.Split('_')[0]),
                                        Promedio86R = 0, // //.Universidad_Completa.Split('_')[1]),
                                        Contestadas = 0, //, //.Universidad_Completa.Split('_')[2])
                                    },
                                Universidad_Incompleta =
                                    new ML.Universidad_Incompleta
                                    {
                                        Promedio66R = 0, // //.Universidad_Incompleta.Split('_')[0]),
                                        Promedio86R = 0, // //.Universidad_Incompleta.Split('_')[1]),
                                        Contestadas = 0, //, //.Universidad_Incompleta.Split('_')[2])
                                    },
                                Contestadas = (int)0, //.Contestadas,
                                                      //aHistorico.Enfoque == 1 ? "Enfoque empresa" : aHistorico.Enfoque == 2 ? "Enfoque Area" : "",
                                Enfoque = 0, //.Enfoque == "Enfoque empresa" ? 1 : 0, //.Enfoque == "Enfoque Area" ? 2 : 0,
                                Esperadas = (int)0, //.Esperadas,
                                Promedio66R = (decimal)0, //.Promedio66R,
                                Promedio86R = (decimal)0, //.Promedio86R
                            };
                            #endregion llenar objeto
                            list.Add(historico);
                        }
                    }
                }
            }
            catch (Exception aE)
            {
                BL.LogReporteoClima.writteLog(aE, new StackTrace());
                return new List<ML.HistoricoClima>();
            }
            return list;
        }

        public static List<ML.HistoricoClima> getHistorico_2EA(ML.Historico aHistorico)
        {
            var list = new List<ML.HistoricoClima>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.HistoricoClima.Select(o => o)
                        .Where(o => o.EntidadId == aHistorico.EntidadId && o.EntidadNombre == aHistorico.EntidadNombre && o.Anio == aHistorico.Anio && o.Enfoque == "Enfoque Area").ToList();
                    var queryEA = context.HistoricoClima.Select(o => new { o.Promedio66R, o.Enfoque, o.EntidadId, o.EntidadNombre }).Where(o => (o.EntidadId == aHistorico.EntidadId && o.Enfoque == "Enfoque Area") || (o.EntidadNombre == aHistorico.EntidadNombre && o.Enfoque == "Enfoque Area")).FirstOrDefault();
                    if (query != null)
                    {
                        if (query.Count > 0)
                        {
                            foreach (var item in query)
                            {
                                #region llenar objeto
                                ML.HistoricoClima historico = new ML.HistoricoClima()
                                {
                                    Bienestar = item.Bienestar,
                                    Bio = item.Bio,
                                    Psico = item.Psico,
                                    Social = item.Social,
                                    AuxProm66ReactEA = (decimal)queryEA.Promedio66R,
                                    AlineacionEstrategica = item.AlineacionEstrategica,
                                    Anio = item.Anio,
                                    CalificacionGlobal = item.CalificacionGlobal,
                                    Coaching = item.Coaching,
                                    Companierismo = item.Companierismo,
                                    Creedibilidad = item.Creedibilidad,
                                    Entidad = item.Entidad,
                                    EntidadId = (int)item.EntidadId,
                                    EntidadNombre = item.EntidadNombre,
                                    IdTipoEntidad = item.IdTipoEntidad,
                                    HabilidadesGerenciales = item.HabilidadesGerenciales,
                                    HC = item.HC,
                                    IdHistorico = item.IdHistorico,
                                    Imparcialidad = item.Imparcialidad,
                                    ManejoDelCambio = item.ManejoDelCambio,
                                    NivelColaboracion = item.NivelColaboracion,
                                    NivelCompromiso = item.NivelCompromiso,
                                    NivelConfianza = item.NivelConfianza,
                                    NivelParticipacion = item.NivelParticipacion,
                                    Orgullo = item.Orgullo,
                                    PracticasCulturales = item.PracticasCulturales,
                                    ProcesosOrganizacionales = item.ProcesosOrganizacionales,
                                    Respeto = item.Respeto,
                                    //
                                    Preg1 = item.Preg1,
                                    Preg2 = item.Preg2,
                                    Preg3 = item.Preg3,
                                    Preg4 = item.Preg4,
                                    Preg5 = item.Preg5,
                                    Preg6 = item.Preg6,
                                    Preg7 = item.Preg7,
                                    Preg8 = item.Preg8,
                                    Preg9 = item.Preg9,
                                    Preg10 = item.Preg10,
                                    Preg11 = item.Preg11,
                                    Preg12 = item.Preg12,
                                    Preg13 = item.Preg13,
                                    Preg14 = item.Preg14,
                                    Preg15 = item.Preg15,
                                    Preg16 = item.Preg16,
                                    Preg17 = item.Preg17,
                                    Preg18 = item.Preg18,
                                    Preg19 = item.Preg19,
                                    Preg20 = item.Preg20,
                                    Preg21 = item.Preg21,
                                    Preg22 = item.Preg22,
                                    Preg23 = item.Preg23,
                                    Preg24 = item.Preg24,
                                    Preg25 = item.Preg25,
                                    Preg26 = item.Preg26,
                                    Preg27 = item.Preg27,
                                    Preg28 = item.Preg28,
                                    Preg29 = item.Preg29,
                                    Preg30 = item.Preg30,
                                    Preg31 = item.Preg31,
                                    Preg32 = item.Preg32,
                                    Preg33 = item.Preg33,
                                    Preg34 = item.Preg34,
                                    Preg35 = item.Preg35,
                                    Preg36 = item.Preg36,
                                    Preg37 = item.Preg37,
                                    Preg38 = item.Preg38,
                                    Preg39 = item.Preg39,
                                    Preg40 = item.Preg40,
                                    Preg41 = item.Preg41,
                                    Preg42 = item.Preg42,
                                    Preg43 = item.Preg43,
                                    Preg44 = item.Preg44,
                                    Preg45 = item.Preg45,
                                    Preg46 = item.Preg46,
                                    Preg47 = item.Preg47,
                                    Preg48 = item.Preg48,
                                    Preg49 = item.Preg49,
                                    Preg50 = item.Preg50,
                                    Preg51 = item.Preg51,
                                    Preg52 = item.Preg52,
                                    Preg53 = item.Preg53,
                                    Preg54 = item.Preg54,
                                    Preg55 = item.Preg55,
                                    Preg56 = item.Preg56,
                                    Preg57 = item.Preg57,
                                    Preg58 = item.Preg58,
                                    Preg59 = item.Preg59,
                                    Preg60 = item.Preg60,
                                    Preg61 = item.Preg61,
                                    Preg62 = item.Preg62,
                                    Preg63 = item.Preg63,
                                    Preg64 = item.Preg64,
                                    Preg65 = item.Preg65,
                                    Preg66 = item.Preg66,
                                    Preg67 = item.Preg67,
                                    Preg68 = item.Preg68,
                                    Preg69 = item.Preg69,
                                    Preg70 = item.Preg70,
                                    Preg71 = item.Preg71,
                                    Preg72 = item.Preg72,
                                    Preg73 = item.Preg73,
                                    Preg74 = item.Preg74,
                                    Preg75 = item.Preg75,
                                    Preg76 = item.Preg76,
                                    Preg77 = item.Preg77,
                                    Preg78 = item.Preg78,
                                    Preg79 = item.Preg79,
                                    Preg80 = item.Preg80,
                                    Preg81 = item.Preg81,
                                    Preg82 = item.Preg82,
                                    Preg83 = item.Preg83,
                                    Preg84 = item.Preg84,
                                    Preg85 = item.Preg85,
                                    Preg86 = item.Preg86,
                                    Administrativo =
                                        new ML.Administrativo
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Administrativo.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Administrativo.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Administrativo.Split('_')[2]),
                                        },
                                    Ant_1_a_2_anios =
                                        new ML.Ant_1_a_2_anios
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Ant_1_a_2_anios.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Ant_1_a_2_anios.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Ant_1_a_2_anios.Split('_')[2]),
                                        },
                                    Ant_3_a_5_anios =
                                        new ML.Ant_3_a_5_anios
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Ant_3_a_5_anios.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Ant_3_a_5_anios.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Ant_3_a_5_anios.Split('_')[2])
                                        },
                                    Ant_6_a_10_anios =
                                        new ML.Ant_6_a_10_anios
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Ant_6_a_10_anios.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Ant_6_a_10_anios.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Ant_6_a_10_anios.Split('_')[2])
                                        },
                                    Ant_6_meses_1_anio =
                                        new ML.Ant_6_meses_1_anio
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Ant_6_meses_1_anio.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Ant_6_meses_1_anio.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Ant_6_meses_1_anio.Split('_')[2])
                                        },
                                    Ant_mas_de_10_anios =
                                        new ML.Ant_mas_de_10_anios
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Ant_mas_de_10_anios.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Ant_mas_de_10_anios.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Ant_mas_de_10_anios.Split('_')[2])
                                        },
                                    Ant_menos_de_6_meses =
                                    new ML.Ant_menos_de_6_meses
                                    {
                                        Promedio66R = Convert.ToDecimal(item.Ant_menos_de_6_meses.Split('_')[0]),
                                        Promedio86R = Convert.ToDecimal(item.Ant_menos_de_6_meses.Split('_')[1]),
                                        Contestadas = Convert.ToInt32(item.Ant_menos_de_6_meses.Split('_')[2])
                                    },
                                    Comercial =
                                        new ML.Comercial
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Comercial.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Comercial.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Comercial.Split('_')[2])
                                        },
                                    Comisionistas =
                                        new ML.Comisionistas
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Comisionistas.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Comisionistas.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Comisionistas.Split('_')[2])
                                        },
                                    COORDINADOR_SUPERVISOR_JEFE =
                                        new ML.COORDINADOR_SUPERVISOR_JEFE
                                        {
                                            Promedio66R = Convert.ToDecimal(item.COORDINADOR_SUPERVISOR_JEFE.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.COORDINADOR_SUPERVISOR_JEFE.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.COORDINADOR_SUPERVISOR_JEFE.Split('_')[2])
                                        },
                                    Director =
                                        new ML.Director
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Director.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Director.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Director.Split('_')[2])
                                        },
                                    Edad_18_A_22_ANIOS =
                                        new ML.Edad_18_A_22_ANIOS
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Edad_18_A_22_ANIOS.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Edad_18_A_22_ANIOS.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Edad_18_A_22_ANIOS.Split('_')[2])
                                        },
                                    Edad_23_A_31_ANIOS =
                                        new ML.Edad_23_A_31_ANIOS
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Edad_23_A_31_ANIOS.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Edad_23_A_31_ANIOS.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Edad_23_A_31_ANIOS.Split('_')[2])
                                        },
                                    Edad_32_A_39_ANIOS =
                                        new ML.Edad_32_A_39_ANIOS
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Edad_32_A_39_ANIOS.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Edad_32_A_39_ANIOS.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Edad_32_A_39_ANIOS.Split('_')[2])
                                        },
                                    Edad_40_A_55_ANIOS =
                                        new ML.Edad_40_A_55_ANIOS
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Edad_40_A_55_ANIOS.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Edad_40_A_55_ANIOS.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Edad_40_A_55_ANIOS.Split('_')[2])
                                        },
                                    Edad_56_ANIOS_O_MAS =
                                        new ML.Edad_56_ANIOS_O_MAS
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Edad_56_ANIOS_O_MAS.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Edad_56_ANIOS_O_MAS.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Edad_56_ANIOS_O_MAS.Split('_')[2])
                                        },
                                    GerenteDepartamental =
                                        new ML.GerenteDepartamental
                                        {
                                            Promedio66R = Convert.ToDecimal(item.GerenteDepartamental.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.GerenteDepartamental.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.GerenteDepartamental.Split('_')[2])
                                        },
                                    GerenteGeneral =
                                        new ML.GerenteGeneral
                                        {
                                            Promedio66R = Convert.ToDecimal(item.GerenteGeneral.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.GerenteGeneral.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.GerenteGeneral.Split('_')[2])
                                        },
                                    Honorarios =
                                        new ML.Honorarios
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Honorarios.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Honorarios.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Honorarios.Split('_')[2])
                                        },
                                    Media_Superior =
                                        new ML.Media_Superior
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Media_Superior.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Media_Superior.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Media_Superior.Split('_')[2])
                                        },
                                    Media_Tecnica =
                                        new ML.Media_Tecnica
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Media_Tecnica.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Media_Tecnica.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Media_Tecnica.Split('_')[2])
                                        },
                                    Planta =
                                    new ML.Planta
                                    {
                                        Promedio66R = Convert.ToDecimal(item.Planta.Split('_')[0]),
                                        Promedio86R = Convert.ToDecimal(item.Planta.Split('_')[1]),
                                        Contestadas = Convert.ToInt32(item.Planta.Split('_')[2])
                                    },
                                    PostGrado =
                                        new ML.PostGrado
                                        {
                                            Promedio66R = Convert.ToDecimal(item.PostGrado.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.PostGrado.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.PostGrado.Split('_')[2])
                                        },
                                    Primaria =
                                        new ML.Primaria
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Primaria.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Primaria.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Primaria.Split('_')[2])
                                        },
                                    Secundaria =
                                        new ML.Secundaria
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Secundaria.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Secundaria.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Secundaria.Split('_')[2])
                                        },
                                    Sexo_Femenino =
                                        new ML.Sexo_Femenino
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Sexo_Femenino.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Sexo_Femenino.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Sexo_Femenino.Split('_')[2])
                                        },
                                    Sexo_Masculino =
                                        new ML.Sexo_Masculino
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Sexo_Masculino.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Sexo_Masculino.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Sexo_Masculino.Split('_')[2])
                                        },
                                    Sindicalizado =
                                        new ML.Sindicalizado
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Sindicalizado.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Sindicalizado.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Sindicalizado.Split('_')[2])
                                        },
                                    Subgerente =
                                        new ML.Subgerente
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Subgerente.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Subgerente.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Subgerente.Split('_')[2])
                                        },
                                    TECNICO_OPERATIVO =
                                        new ML.TECNICO_OPERATIVO
                                        {
                                            Promedio66R = Convert.ToDecimal(item.TECNICO_OPERATIVO.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.TECNICO_OPERATIVO.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.TECNICO_OPERATIVO.Split('_')[2])
                                        },
                                    Temporal =
                                        new ML.Temporal
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Temporal.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Temporal.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Temporal.Split('_')[2])
                                        },
                                    Universidad_Completa =
                                        new ML.Universidad_Completa
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Universidad_Completa.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Universidad_Completa.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Universidad_Completa.Split('_')[2])
                                        },
                                    Universidad_Incompleta =
                                        new ML.Universidad_Incompleta
                                        {
                                            Promedio66R = Convert.ToDecimal(item.Universidad_Incompleta.Split('_')[0]),
                                            Promedio86R = Convert.ToDecimal(item.Universidad_Incompleta.Split('_')[1]),
                                            Contestadas = Convert.ToInt32(item.Universidad_Incompleta.Split('_')[2])
                                        },
                                    Contestadas = (int)item.Contestadas,
                                    //aHistorico.Enfoque == 1 ? "Enfoque empresa" : aHistorico.Enfoque == 2 ? "Enfoque Area" : "",
                                    Enfoque = item.Enfoque == "Enfoque empresa" ? 1 : item.Enfoque == "Enfoque Area" ? 2 : 0,
                                    Esperadas = (int)item.Esperadas,
                                    Promedio66R = (decimal)item.Promedio66R,
                                    Promedio86R = (decimal)item.Promedio86R
                                };
                                #endregion llenar objeto
                                list.Add(historico);
                            }
                        }
                        else
                        {
                            #region llenar objeto
                            ML.HistoricoClima historico = new ML.HistoricoClima()
                            {
                                Bienestar = 0,
                                Bio = 0,
                                Psico = 0,
                                Social = 0,
                                AuxProm66ReactEA = 0,
                                AlineacionEstrategica = 0, //.AlineacionEstrategica,
                                Anio = 0, //.Anio,
                                CalificacionGlobal = 0, //.CalificacionGlobal,
                                Coaching = 0, //.Coaching,
                                Companierismo = 0, //.Companierismo,
                                Creedibilidad = 0, //.Creedibilidad,
                                Entidad = "", //.Entidad,
                                EntidadId = (int)aHistorico.EntidadId, //.EntidadId,
                                EntidadNombre = aHistorico.EntidadNombre, //.EntidadNombre,
                                IdTipoEntidad = 0, //.IdTipoEntidad,
                                HabilidadesGerenciales = 0, //.HabilidadesGerenciales,
                                HC = 0, //.HC,
                                IdHistorico = 0, //.IdHistorico,
                                Imparcialidad = 0, //.Imparcialidad,
                                ManejoDelCambio = 0, //.ManejoDelCambio,
                                NivelColaboracion = 0, //.NivelColaboracion,
                                NivelCompromiso = 0, //.NivelCompromiso,
                                NivelConfianza = 0, //.NivelConfianza,
                                NivelParticipacion = 0, //.NivelParticipacion,
                                Orgullo = 0, //.Orgullo,
                                PracticasCulturales = 0, //.PracticasCulturales,
                                ProcesosOrganizacionales = 0, //.ProcesosOrganizacionales,
                                Respeto = 0, //.Respeto,
                                             //
                                Preg1 = 0, //.Preg1,
                                Preg2 = 0, //.Preg2,
                                Preg3 = 0, //.Preg3,
                                Preg4 = 0, //.Preg4,
                                Preg5 = 0, //.Preg5,
                                Preg6 = 0, //.Preg6,
                                Preg7 = 0, //.Preg7,
                                Preg8 = 0, //.Preg8,
                                Preg9 = 0, //.Preg9,
                                Preg10 = 0, //.Preg10,
                                Preg11 = 0, //.Preg11,
                                Preg12 = 0, //.Preg12,
                                Preg13 = 0, //.Preg13,
                                Preg14 = 0, //.Preg14,
                                Preg15 = 0, //.Preg15,
                                Preg16 = 0, //.Preg16,
                                Preg17 = 0, //.Preg17,
                                Preg18 = 0, //.Preg18,
                                Preg19 = 0, //.Preg19,
                                Preg20 = 0, //.Preg20,
                                Preg21 = 0, //.Preg21,
                                Preg22 = 0, //.Preg22,
                                Preg23 = 0, //.Preg23,
                                Preg24 = 0, //.Preg24,
                                Preg25 = 0, //.Preg25,
                                Preg26 = 0, //.Preg26,
                                Preg27 = 0, //.Preg27,
                                Preg28 = 0, //.Preg28,
                                Preg29 = 0, //.Preg29,
                                Preg30 = 0, //.Preg30,
                                Preg31 = 0, //.Preg31,
                                Preg32 = 0, //.Preg32,
                                Preg33 = 0, //.Preg33,
                                Preg34 = 0, //.Preg34,
                                Preg35 = 0, //.Preg35,
                                Preg36 = 0, //.Preg36,
                                Preg37 = 0, //.Preg37,
                                Preg38 = 0, //.Preg38,
                                Preg39 = 0, //.Preg39,
                                Preg40 = 0, //.Preg40,
                                Preg41 = 0, //.Preg41,
                                Preg42 = 0, //.Preg42,
                                Preg43 = 0, //.Preg43,
                                Preg44 = 0, //.Preg44,
                                Preg45 = 0, //.Preg45,
                                Preg46 = 0, //.Preg46,
                                Preg47 = 0, //.Preg47,
                                Preg48 = 0, //.Preg48,
                                Preg49 = 0, //.Preg49,
                                Preg50 = 0, //.Preg50,
                                Preg51 = 0, //.Preg51,
                                Preg52 = 0, //.Preg52,
                                Preg53 = 0, //.Preg53,
                                Preg54 = 0, //.Preg54,
                                Preg55 = 0, //.Preg55,
                                Preg56 = 0, //.Preg56,
                                Preg57 = 0, //.Preg57,
                                Preg58 = 0, //.Preg58,
                                Preg59 = 0, //.Preg59,
                                Preg60 = 0, //.Preg60,
                                Preg61 = 0, //.Preg61,
                                Preg62 = 0, //.Preg62,
                                Preg63 = 0, //.Preg63,
                                Preg64 = 0, //.Preg64,
                                Preg65 = 0, //.Preg65,
                                Preg66 = 0, //.Preg66,
                                Preg67 = 0, //.Preg67,
                                Preg68 = 0, //.Preg68,
                                Preg69 = 0, //.Preg69,
                                Preg70 = 0, //.Preg70,
                                Preg71 = 0, //.Preg71,
                                Preg72 = 0, //.Preg72,
                                Preg73 = 0, //.Preg73,
                                Preg74 = 0, //.Preg74,
                                Preg75 = 0, //.Preg75,
                                Preg76 = 0, //.Preg76,
                                Preg77 = 0, //.Preg77,
                                Preg78 = 0, //.Preg78,
                                Preg79 = 0, //.Preg79,
                                Preg80 = 0, //.Preg80,
                                Preg81 = 0, //.Preg81,
                                Preg82 = 0, //.Preg82,
                                Preg83 = 0, //.Preg83,
                                Preg84 = 0, //.Preg84,
                                Preg85 = 0, //.Preg85,
                                Preg86 = 0, //.Preg86,
                                Administrativo =
                                    new ML.Administrativo
                                    {
                                        Promedio66R = 0, // //.Administrativo.Split('_')[0]),
                                        Promedio86R = 0, // //.Administrativo.Split('_')[1]),
                                        Contestadas = 0, //, //.Administrativo.Split('_')[2]),
                                    },
                                Ant_1_a_2_anios =
                                    new ML.Ant_1_a_2_anios
                                    {
                                        Promedio66R = 0, // //.Ant_1_a_2_anios.Split('_')[0]),
                                        Promedio86R = 0, // //.Ant_1_a_2_anios.Split('_')[1]),
                                        Contestadas = 0, //, //.Ant_1_a_2_anios.Split('_')[2]),
                                    },
                                Ant_3_a_5_anios =
                                    new ML.Ant_3_a_5_anios
                                    {
                                        Promedio66R = 0, // //.Ant_3_a_5_anios.Split('_')[0]),
                                        Promedio86R = 0, // //.Ant_3_a_5_anios.Split('_')[1]),
                                        Contestadas = 0, //, //.Ant_3_a_5_anios.Split('_')[2])
                                    },
                                Ant_6_a_10_anios =
                                    new ML.Ant_6_a_10_anios
                                    {
                                        Promedio66R = 0, // //.Ant_6_a_10_anios.Split('_')[0]),
                                        Promedio86R = 0, // //.Ant_6_a_10_anios.Split('_')[1]),
                                        Contestadas = 0, //, //.Ant_6_a_10_anios.Split('_')[2])
                                    },
                                Ant_6_meses_1_anio =
                                    new ML.Ant_6_meses_1_anio
                                    {
                                        Promedio66R = 0, // //.Ant_6_meses_1_anio.Split('_')[0]),
                                        Promedio86R = 0, // //.Ant_6_meses_1_anio.Split('_')[1]),
                                        Contestadas = 0, //, //.Ant_6_meses_1_anio.Split('_')[2])
                                    },
                                Ant_mas_de_10_anios =
                                    new ML.Ant_mas_de_10_anios
                                    {
                                        Promedio66R = 0, // //.Ant_mas_de_10_anios.Split('_')[0]),
                                        Promedio86R = 0, // //.Ant_mas_de_10_anios.Split('_')[1]),
                                        Contestadas = 0, //, //.Ant_mas_de_10_anios.Split('_')[2])
                                    },
                                Ant_menos_de_6_meses =
                                new ML.Ant_menos_de_6_meses
                                {
                                    Promedio66R = 0, // //.Ant_menos_de_6_meses.Split('_')[0]),
                                    Promedio86R = 0, // //.Ant_menos_de_6_meses.Split('_')[1]),
                                    Contestadas = 0, //, //.Ant_menos_de_6_meses.Split('_')[2])
                                },
                                Comercial =
                                    new ML.Comercial
                                    {
                                        Promedio66R = 0, // //.Comercial.Split('_')[0]),
                                        Promedio86R = 0, // //.Comercial.Split('_')[1]),
                                        Contestadas = 0, //, //.Comercial.Split('_')[2])
                                    },
                                Comisionistas =
                                    new ML.Comisionistas
                                    {
                                        Promedio66R = 0, // //.Comisionistas.Split('_')[0]),
                                        Promedio86R = 0, // //.Comisionistas.Split('_')[1]),
                                        Contestadas = 0, //, //.Comisionistas.Split('_')[2])
                                    },
                                COORDINADOR_SUPERVISOR_JEFE =
                                    new ML.COORDINADOR_SUPERVISOR_JEFE
                                    {
                                        Promedio66R = 0, // //.COORDINADOR_SUPERVISOR_JEFE.Split('_')[0]),
                                        Promedio86R = 0, // //.COORDINADOR_SUPERVISOR_JEFE.Split('_')[1]),
                                        Contestadas = 0, //, //.COORDINADOR_SUPERVISOR_JEFE.Split('_')[2])
                                    },
                                Director =
                                    new ML.Director
                                    {
                                        Promedio66R = 0, // //.Director.Split('_')[0]),
                                        Promedio86R = 0, // //.Director.Split('_')[1]),
                                        Contestadas = 0, //, //.Director.Split('_')[2])
                                    },
                                Edad_18_A_22_ANIOS =
                                    new ML.Edad_18_A_22_ANIOS
                                    {
                                        Promedio66R = 0, // //.Edad_18_A_22_ANIOS.Split('_')[0]),
                                        Promedio86R = 0, // //.Edad_18_A_22_ANIOS.Split('_')[1]),
                                        Contestadas = 0, //, //.Edad_18_A_22_ANIOS.Split('_')[2])
                                    },
                                Edad_23_A_31_ANIOS =
                                    new ML.Edad_23_A_31_ANIOS
                                    {
                                        Promedio66R = 0, // //.Edad_23_A_31_ANIOS.Split('_')[0]),
                                        Promedio86R = 0, // //.Edad_23_A_31_ANIOS.Split('_')[1]),
                                        Contestadas = 0, //, //.Edad_23_A_31_ANIOS.Split('_')[2])
                                    },
                                Edad_32_A_39_ANIOS =
                                    new ML.Edad_32_A_39_ANIOS
                                    {
                                        Promedio66R = 0, // //.Edad_32_A_39_ANIOS.Split('_')[0]),
                                        Promedio86R = 0, // //.Edad_32_A_39_ANIOS.Split('_')[1]),
                                        Contestadas = 0, //, //.Edad_32_A_39_ANIOS.Split('_')[2])
                                    },
                                Edad_40_A_55_ANIOS =
                                    new ML.Edad_40_A_55_ANIOS
                                    {
                                        Promedio66R = 0, // //.Edad_40_A_55_ANIOS.Split('_')[0]),
                                        Promedio86R = 0, // //.Edad_40_A_55_ANIOS.Split('_')[1]),
                                        Contestadas = 0, //, //.Edad_40_A_55_ANIOS.Split('_')[2])
                                    },
                                Edad_56_ANIOS_O_MAS =
                                    new ML.Edad_56_ANIOS_O_MAS
                                    {
                                        Promedio66R = 0, // //.Edad_56_ANIOS_O_MAS.Split('_')[0]),
                                        Promedio86R = 0, // //.Edad_56_ANIOS_O_MAS.Split('_')[1]),
                                        Contestadas = 0, //, //.Edad_56_ANIOS_O_MAS.Split('_')[2])
                                    },
                                GerenteDepartamental =
                                    new ML.GerenteDepartamental
                                    {
                                        Promedio66R = 0, // //.GerenteDepartamental.Split('_')[0]),
                                        Promedio86R = 0, // //.GerenteDepartamental.Split('_')[1]),
                                        Contestadas = 0, //, //.GerenteDepartamental.Split('_')[2])
                                    },
                                GerenteGeneral =
                                    new ML.GerenteGeneral
                                    {
                                        Promedio66R = 0, // //.GerenteGeneral.Split('_')[0]),
                                        Promedio86R = 0, // //.GerenteGeneral.Split('_')[1]),
                                        Contestadas = 0, //, //.GerenteGeneral.Split('_')[2])
                                    },
                                Honorarios =
                                    new ML.Honorarios
                                    {
                                        Promedio66R = 0, // //.Honorarios.Split('_')[0]),
                                        Promedio86R = 0, // //.Honorarios.Split('_')[1]),
                                        Contestadas = 0, //, //.Honorarios.Split('_')[2])
                                    },
                                Media_Superior =
                                    new ML.Media_Superior
                                    {
                                        Promedio66R = 0, // //.Media_Superior.Split('_')[0]),
                                        Promedio86R = 0, // //.Media_Superior.Split('_')[1]),
                                        Contestadas = 0, //, //.Media_Superior.Split('_')[2])
                                    },
                                Media_Tecnica =
                                    new ML.Media_Tecnica
                                    {
                                        Promedio66R = 0, // //.Media_Tecnica.Split('_')[0]),
                                        Promedio86R = 0, // //.Media_Tecnica.Split('_')[1]),
                                        Contestadas = 0, //, //.Media_Tecnica.Split('_')[2])
                                    },
                                Planta =
                                new ML.Planta
                                {
                                    Promedio66R = 0, // //.Planta.Split('_')[0]),
                                    Promedio86R = 0, // //.Planta.Split('_')[1]),
                                    Contestadas = 0, //, //.Planta.Split('_')[2])
                                },
                                PostGrado =
                                    new ML.PostGrado
                                    {
                                        Promedio66R = 0, // //.PostGrado.Split('_')[0]),
                                        Promedio86R = 0, // //.PostGrado.Split('_')[1]),
                                        Contestadas = 0, //, //.PostGrado.Split('_')[2])
                                    },
                                Primaria =
                                    new ML.Primaria
                                    {
                                        Promedio66R = 0, // //.Primaria.Split('_')[0]),
                                        Promedio86R = 0, // //.Primaria.Split('_')[1]),
                                        Contestadas = 0, //, //.Primaria.Split('_')[2])
                                    },
                                Secundaria =
                                    new ML.Secundaria
                                    {
                                        Promedio66R = 0, // //.Secundaria.Split('_')[0]),
                                        Promedio86R = 0, // //.Secundaria.Split('_')[1]),
                                        Contestadas = 0, //, //.Secundaria.Split('_')[2])
                                    },
                                Sexo_Femenino =
                                    new ML.Sexo_Femenino
                                    {
                                        Promedio66R = 0, // //.Sexo_Femenino.Split('_')[0]),
                                        Promedio86R = 0, // //.Sexo_Femenino.Split('_')[1]),
                                        Contestadas = 0, //, //.Sexo_Femenino.Split('_')[2])
                                    },
                                Sexo_Masculino =
                                    new ML.Sexo_Masculino
                                    {
                                        Promedio66R = 0, // //.Sexo_Masculino.Split('_')[0]),
                                        Promedio86R = 0, // //.Sexo_Masculino.Split('_')[1]),
                                        Contestadas = 0, //, //.Sexo_Masculino.Split('_')[2])
                                    },
                                Sindicalizado =
                                    new ML.Sindicalizado
                                    {
                                        Promedio66R = 0, // //.Sindicalizado.Split('_')[0]),
                                        Promedio86R = 0, // //.Sindicalizado.Split('_')[1]),
                                        Contestadas = 0, //, //.Sindicalizado.Split('_')[2])
                                    },
                                Subgerente =
                                    new ML.Subgerente
                                    {
                                        Promedio66R = 0, // //.Subgerente.Split('_')[0]),
                                        Promedio86R = 0, // //.Subgerente.Split('_')[1]),
                                        Contestadas = 0, //, //.Subgerente.Split('_')[2])
                                    },
                                TECNICO_OPERATIVO =
                                    new ML.TECNICO_OPERATIVO
                                    {
                                        Promedio66R = 0, // //.TECNICO_OPERATIVO.Split('_')[0]),
                                        Promedio86R = 0, // //.TECNICO_OPERATIVO.Split('_')[1]),
                                        Contestadas = 0, //, //.TECNICO_OPERATIVO.Split('_')[2])
                                    },
                                Temporal =
                                    new ML.Temporal
                                    {
                                        Promedio66R = 0, // //.Temporal.Split('_')[0]),
                                        Promedio86R = 0, // //.Temporal.Split('_')[1]),
                                        Contestadas = 0, //, //.Temporal.Split('_')[2])
                                    },
                                Universidad_Completa =
                                    new ML.Universidad_Completa
                                    {
                                        Promedio66R = 0, // //.Universidad_Completa.Split('_')[0]),
                                        Promedio86R = 0, // //.Universidad_Completa.Split('_')[1]),
                                        Contestadas = 0, //, //.Universidad_Completa.Split('_')[2])
                                    },
                                Universidad_Incompleta =
                                    new ML.Universidad_Incompleta
                                    {
                                        Promedio66R = 0, // //.Universidad_Incompleta.Split('_')[0]),
                                        Promedio86R = 0, // //.Universidad_Incompleta.Split('_')[1]),
                                        Contestadas = 0, //, //.Universidad_Incompleta.Split('_')[2])
                                    },
                                Contestadas = (int)0, //.Contestadas,
                                                      //aHistorico.Enfoque == 1 ? "Enfoque empresa" : aHistorico.Enfoque == 2 ? "Enfoque Area" : "",
                                Enfoque = 0, //.Enfoque == "Enfoque empresa" ? 1 : 0, //.Enfoque == "Enfoque Area" ? 2 : 0,
                                Esperadas = (int)0, //.Esperadas,
                                Promedio66R = (decimal)0, //.Promedio66R,
                                Promedio86R = (decimal)0, //.Promedio86R
                            };
                            #endregion llenar objeto
                            list.Add(historico);
                        }
                    }
                }
            }
            catch (Exception aE)
            {
                BL.LogReporteoClima.writteLog(aE, new StackTrace());
                return new List<ML.HistoricoClima>();
            }
            return list;
        }

        /*public static ML.Historico getPorcentajePreguntas(List<ML.Historico> aHistorico)
        {
            var list = new ML.Historico();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    foreach (var item in aHistorico)
                    {
                        var query = context.Historico.Select(o => o).Where(o => o.entidadId == item.EntidadId && o.entidadNombre == item.EntidadNombre).ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                foreach (var obj in query)
                                {
                                    ML.Historico historico = new ML.Historico()
                                    {
                                        Preg1EE = obj.Preg1EE,
                                        Preg2EE = obj.Preg2EE
                                    };
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception aE)
            {
                BL.LogReporteoClima.writteLog(aE, new StackTrace());
            }
            return list;
        }*/

        public static List<ML.HistoricoClima> getPromedioGeneral(List<ML.minHistorico> aHistorico)
        {
            var list = new List<ML.HistoricoClima>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    foreach (var item in aHistorico)
                    {
                        //select Promedio66R, Enfoque from HistoricoClima where EntidadId = 9 order by Enfoque desc
                        var queryEE = context.HistoricoClima.Select(o => new { o.Promedio66R, o.Enfoque, o.EntidadId, o.EntidadNombre }).Where(o => (o.EntidadId == item.EntidadId && o.Enfoque == "Enfoque Empresa") || (o.EntidadNombre == item.EntidadNombre && o.Enfoque == "Enfoque Empresa")).ToList();
                        var queryEA = context.HistoricoClima.Select(o => new { o.Promedio66R, o.Enfoque, o.EntidadId, o.EntidadNombre }).Where(o => (o.EntidadId == item.EntidadId && o.Enfoque == "Enfoque Area") || (o.EntidadNombre == item.EntidadNombre && o.Enfoque == "Enfoque Area")).ToList();
                        if (queryEE != null)
                        {
                            if (queryEE.Count > 0)
                            {
                                foreach (var obj in queryEE)
                                {
                                    ML.HistoricoClima historico = new ML.HistoricoClima();
                                    historico.EntidadId = obj.EntidadId;
                                    historico.EntidadNombre = obj.EntidadNombre;
                                    historico.AuxProm66ReactEE = (decimal)obj.Promedio66R;
                                    //historico.CalificacionGlobalEA = obj.calificacionGlobalEA;
                                    foreach (var objAux in queryEA)
                                    {
                                        historico.AuxProm66ReactEA = (decimal)objAux.Promedio66R;
                                    }
                                    list.Add(historico);
                                }
                            }
                            else
                            {
                                ML.HistoricoClima historico = new ML.HistoricoClima();
                                historico.EntidadId = 0;
                                historico.EntidadNombre = item.EntidadNombre;
                                historico.AuxProm66ReactEE = 0;
                                historico.AuxProm66ReactEA = 0;
                                //historico.CalificacionGlobalEA = 0;
                                list.Add(historico);
                            }
                        }
                    }
                }
            }
            catch (Exception aE)
            {
                BL.LogReporteoClima.writteLog(aE, new StackTrace());
                return new List<ML.HistoricoClima>();
            }
            return list;
        }

        /*public static string AddHistorico(ML.Historico aHistorico, object aCurrentUser)
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var model = Mapping_Historicos(aHistorico, aCurrentUser);
                    if (model != null)
                    {
                        var query = context.Historico.Add(model);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception aE)
            {
                BL.LogReporteoClima.writteLog(aE, new StackTrace());
                return aE.Message;
            }
            return "El historico se creó correctamente";
        }*/

        public static List<ML.HistoricoClima> getHistoricoBienestarEE(ML.Historico aFiltros)
        {

            return new List<ML.HistoricoClima>();
        }

        public static string AddHistoricoFromReporte(ML.HistoricoClima aHistorico, object aCurrentUser)
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            //Obtener porcentajes de las preguntas 1 a 86
                            aHistorico = GetPromedio66React(aHistorico);
                            var model = Mapping_Historicos_FromReporte(aHistorico, aCurrentUser);
                            if (model != null)
                            {
                                var maxId = context.HistoricoClima.Max<DL.HistoricoClima, int>(o => o.IdHistorico);
                                var save = Save(model, context, transaction, maxId);
                                if (!String.IsNullOrEmpty(save))
                                {
                                    return save;
                                }
                            }
                            else
                            {
                                BL.LogReporteoClima.writteLog("Error al crear el modelo para guardar EmpleadoRespuestas masivamente", new StackTrace());
                                return "Error al crear el modelo para guardar EmpleadoRespuestas masivamente";
                            }
                        }
                        catch (Exception aE)
                        {
                            transaction.Rollback();
                            BL.LogReporteoClima.writteLog(aE, new StackTrace());
                            return aE.Message;
                        }
                        context.SaveChanges();
                        transaction.Commit();
                        return "Los históricos fueron guardados correctamente";
                    }
                }
            }
            catch (Exception aE)
            {
                BL.LogReporteoClima.writteLog(aE, new StackTrace());
                return aE.Message;
            }
        }
        public static ML.HistoricoClima GetPromedio66React(ML.HistoricoClima obj)
        {
            try
            {
                string MethodName = "";
                switch (obj.IdTipoEntidad)
                {
                    case 1: MethodName = "UNegocio"; break;
                    case 2: MethodName = "Company"; break;
                    case 3: MethodName = "Area"; break;
                    case 4: MethodName = "Departamento"; break;
                    case 5: MethodName = "SubDepartamento"; break;
                    default:
                        BL.LogReporteoClima.writteLog("El objeto de la entidad " + obj.EntidadNombre + " no contiene el atributo idTipoEntidad por lo que no se obtuvieron el porcentaje de las preguntas", new StackTrace());
                        break;
                }
                var pActuales = new ML.HistoricoClima();
                if (obj.Enfoque == 1)
                {
                    pActuales = BL.ReporteD4U.obtenerPorcentajeActualDeLasPreguntasEE(Convert.ToInt32(obj.Anio), obj.EntidadNombre, MethodName);
                    obj.Promedio86R = ObtenerPromedio86REE(obj.EntidadNombre, MethodName, (int)obj.Anio);
                    obj.Promedio86R = Math.Round(obj.Promedio86R, 2);
                }
                if (obj.Enfoque == 2)
                {
                    pActuales = BL.ReporteD4U.obtenerPorcentajeActualDeLasPreguntasEA(Convert.ToInt32(obj.Anio), obj.EntidadNombre, MethodName);
                    obj.Promedio86R = ObtenerPromedio86REA(obj.EntidadNombre, MethodName, (int)obj.Anio);
                    obj.Promedio86R = Math.Round(obj.Promedio86R, 2);
                }

                obj.Preg1 = pActuales.Preg1;
                obj.Preg2 = pActuales.Preg2;
                obj.Preg3 = pActuales.Preg3;
                obj.Preg4 = pActuales.Preg4;
                obj.Preg5 = pActuales.Preg5; obj.Preg6 = pActuales.Preg6; obj.Preg7 = pActuales.Preg7; obj.Preg8 = pActuales.Preg8; obj.Preg9 = pActuales.Preg9; obj.Preg10 = pActuales.Preg10; obj.Preg11 = pActuales.Preg11; obj.Preg12 = pActuales.Preg12; obj.Preg13 = pActuales.Preg13; obj.Preg14 = pActuales.Preg14; obj.Preg15 = pActuales.Preg15; obj.Preg16 = pActuales.Preg16; obj.Preg17 = pActuales.Preg17; obj.Preg18 = pActuales.Preg18; obj.Preg19 = pActuales.Preg19; obj.Preg20 = pActuales.Preg20; obj.Preg21 = pActuales.Preg21; obj.Preg22 = pActuales.Preg22; obj.Preg23 = pActuales.Preg23; obj.Preg24 = pActuales.Preg24; obj.Preg25 = pActuales.Preg25; obj.Preg26 = pActuales.Preg26; obj.Preg27 = pActuales.Preg27; obj.Preg28 = pActuales.Preg28; obj.Preg29 = pActuales.Preg29; obj.Preg30 = pActuales.Preg30; obj.Preg31 = pActuales.Preg31; obj.Preg32 = pActuales.Preg32; obj.Preg33 = pActuales.Preg33; obj.Preg34 = pActuales.Preg34; obj.Preg35 = pActuales.Preg35; obj.Preg36 = pActuales.Preg36; obj.Preg37 = pActuales.Preg37; obj.Preg38 = pActuales.Preg38; obj.Preg39 = pActuales.Preg39; obj.Preg40 = pActuales.Preg40; obj.Preg41 = pActuales.Preg41; obj.Preg42 = pActuales.Preg42; obj.Preg43 = pActuales.Preg43; obj.Preg44 = pActuales.Preg44; obj.Preg45 = pActuales.Preg45; obj.Preg46 = pActuales.Preg46; obj.Preg47 = pActuales.Preg47; obj.Preg48 = pActuales.Preg48; obj.Preg49 = pActuales.Preg49; obj.Preg50 = pActuales.Preg50; obj.Preg51 = pActuales.Preg51; obj.Preg52 = pActuales.Preg52; obj.Preg53 = pActuales.Preg53; obj.Preg54 = pActuales.Preg54; obj.Preg55 = pActuales.Preg55; obj.Preg56 = pActuales.Preg56; obj.Preg57 = pActuales.Preg57; obj.Preg58 = pActuales.Preg58; obj.Preg59 = pActuales.Preg59; obj.Preg60 = pActuales.Preg60; obj.Preg61 = pActuales.Preg61; obj.Preg62 = pActuales.Preg62; obj.Preg63 = pActuales.Preg63; obj.Preg64 = pActuales.Preg64; obj.Preg65 = pActuales.Preg65; obj.Preg66 = pActuales.Preg66; obj.Preg67 = pActuales.Preg67; obj.Preg68 = pActuales.Preg68; obj.Preg69 = pActuales.Preg69; obj.Preg70 = pActuales.Preg70; obj.Preg71 = pActuales.Preg71; obj.Preg72 = pActuales.Preg72; obj.Preg73 = pActuales.Preg73; obj.Preg74 = pActuales.Preg74; obj.Preg75 = pActuales.Preg75; obj.Preg76 = pActuales.Preg76; obj.Preg77 = pActuales.Preg77; obj.Preg78 = pActuales.Preg78; obj.Preg79 = pActuales.Preg79; obj.Preg80 = pActuales.Preg80; obj.Preg81 = pActuales.Preg81; obj.Preg82 = pActuales.Preg82; obj.Preg83 = pActuales.Preg83; obj.Preg84 = pActuales.Preg84; obj.Preg85 = pActuales.Preg85; obj.Preg86 = pActuales.Preg86;

                //Terminadas HC, Constatadas
                obj.HC = pActuales.HC;
                //obj.NivelParticipacion = pActuales.NivelParticipacion;
                return obj;
            }
            catch (Exception aE)
            {
                BL.LogReporteoClima.writteLog(aE, new StackTrace());
                return obj;
            }
        }

        public static decimal MathRound(decimal? value){
            decimal val = (decimal)value;
            return Math.Round(val, 2);
        }

        public static decimal ObtenerPromedio86REE(string valor, string filtro, int anioActual)
        {
            try
            {
                if (filtro.Contains("UNegocio"))
                    filtro = "UnidadNegocio";
                if (filtro.Contains("Company"))
                    filtro = "DivisionMarca";
                if (filtro.Contains("Area"))
                    filtro = "AreaAgencia";
                if (filtro.Contains("Departamento"))
                    filtro = "Depto";
                if (filtro.Contains("SubDepartamento"))
                    filtro = "Subdepartamento";
                var terminadas = BL.ReporteD4U.getTerminadas(filtro, valor, anioActual);
                terminadas = terminadas * 86;
                string query = @"SELECT Empleado.IdEmpleado
                            FROM EmpleadoRespuestas
                            left JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            left JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            left JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado
                            WHERE
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 1 AND 86 AND EmpleadoRespuestas.RespuestaEmpleado = 'Casi siempre es verdad'  AND Empleado.{0} = '{1}')
                            OR
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 1 AND 86 AND EmpleadoRespuestas.RespuestaEmpleado = 'Frecuentemente es verdad'AND Empleado.{0} = '{1}')";
                query = query.Replace("{0}", filtro);
                query = query.Replace("{1}", valor);
                System.Data.DataSet ds = new System.Data.DataSet();
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    connection.Open();
                    SqlDataAdapter dat_1 = new System.Data.SqlClient.SqlDataAdapter(query, connection);
                    dat_1.Fill(ds, "dat_1");
                }
                var data = ds.Tables[0].Rows.Count;
                decimal val = ((decimal)data / terminadas) * 100;
                return Math.Round(val, 2);
            }
            catch (Exception aE)
            {
                BL.LogReporteoClima.writteLog(aE, new StackTrace());
                return 0;
            }
        }
        //87 - 172
        public static decimal ObtenerPromedio86REA(string valor, string filtro, int anioActual)
        {
            try
            {
                if (filtro.Contains("UNegocio"))
                    filtro = "UnidadNegocio";
                if (filtro.Contains("Company"))
                    filtro = "DivisionMarca";
                if (filtro.Contains("Area"))
                    filtro = "AreaAgencia";
                if (filtro.Contains("Departamento"))
                    filtro = "Depto";
                if (filtro.Contains("SubDepartamento"))
                    filtro = "Subdepartamento";
                var terminadas = BL.ReporteD4U.getTerminadas(filtro, valor, anioActual);
                terminadas = terminadas * 86;
                string query = @"SELECT Empleado.IdEmpleado
                            FROM EmpleadoRespuestas
                            left JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            left JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            left JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado
                            WHERE
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 87 AND 172 AND EmpleadoRespuestas.RespuestaEmpleado = 'Casi siempre es verdad'  AND Empleado.{0} = '{1}')
                            OR
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 87 AND 172 AND EmpleadoRespuestas.RespuestaEmpleado = 'Frecuentemente es verdad'AND Empleado.{0} = '{1}')";
                query = query.Replace("{0}", filtro);
                query = query.Replace("{1}", valor);
                System.Data.DataSet ds = new System.Data.DataSet();
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    connection.Open();
                    SqlDataAdapter dat_1 = new System.Data.SqlClient.SqlDataAdapter(query, connection);
                    dat_1.Fill(ds, "dat_1");
                }
                var data = ds.Tables[0].Rows.Count;
                decimal val = ((decimal)data / terminadas) * 100;
                return Math.Round(val, 2);
            }
            catch (Exception aE)
            {
                BL.LogReporteoClima.writteLog(aE, new StackTrace());
                return 0;
            }
        }
        #endregion

    }
}
