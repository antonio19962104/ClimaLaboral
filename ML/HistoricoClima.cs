using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class minHistoricoClima
    {
        public int EntidadId { get; set; } = 0;
        public string EntidadNombre { get; set; } = String.Empty;
    }
    public class HistoricoClima
    {
        public decimal AuxProm66ReactEE { get; set; } = 0;
        public decimal AuxProm66ReactEA { get; set; } = 0;
        public int Enfoque { get; set; } = 0;//1=EE    2=EA
        public int IdHistorico { get; set; }
        public int? Anio { get; set; } = 0;
        public int? IdTipoEntidad { get; set; } = 0;
        public string Entidad { get; set; } = String.Empty;
        public int? EntidadId { get; set; } = 0;
        public string EntidadNombre { get; set; } = String.Empty;
        public decimal Promedio66R { get; set; } = 0;
        public decimal Promedio86R { get; set; } = 0;
        public int Esperadas { get; set; } = 0;
        public int Contestadas { get; set; } = 0;
        public int? HC { get; set; } = 0;
        public decimal? NivelParticipacion { get; set; } = 0;
        public decimal? CalificacionGlobal { get; set; } = 0;
        public decimal? NivelConfianza { get; set; } = 0;
        public decimal? NivelCompromiso { get; set; } = 0;
        public decimal? NivelColaboracion { get; set; } = 0;
        public decimal? Creedibilidad { get; set; } = 0;
        public decimal? Imparcialidad { get; set; } = 0;
        public decimal? Orgullo { get; set; } = 0;
        public decimal? Respeto { get; set; } = 0;
        public decimal? Companierismo { get; set; } = 0;
        public decimal? Coaching { get; set; } = 0;
        public decimal? HabilidadesGerenciales { get; set; } = 0;
        public decimal? AlineacionEstrategica { get; set; } = 0;
        public decimal? PracticasCulturales { get; set; } = 0;
        public decimal? ManejoDelCambio { get; set; } = 0;
        public decimal? ProcesosOrganizacionales { get; set; } = 0;

        public decimal? Bienestar { get; set; } = 0;
        public decimal? Bio { get; set; } = 0;
        public decimal? Psico { get; set; } = 0;
        public decimal? Social { get; set; } = 0;

        #region Sección demográfico { Entidadid, EntidadNombre, Promedio66R, Promedio86R, Esperadas, Contestadas }

        #endregion Sección demográfico


        /*
         * Seccion preguntas
        */
        public decimal? Preg1 { get; set; } = 0;
        public decimal? Preg2 { get; set; } = 0;
        public decimal? Preg3 { get; set; } = 0;
        public decimal? Preg4 { get; set; } = 0;
        public decimal? Preg5 { get; set; } = 0;
        public decimal? Preg6 { get; set; } = 0;
        public decimal? Preg7 { get; set; } = 0;
        public decimal? Preg8 { get; set; } = 0;
        public decimal? Preg9 { get; set; } = 0;
        public decimal? Preg10 { get; set; } = 0;
        public decimal? Preg11 { get; set; } = 0;
        public decimal? Preg12 { get; set; } = 0;
        public decimal? Preg13 { get; set; } = 0;
        public decimal? Preg14 { get; set; } = 0;
        public decimal? Preg15 { get; set; } = 0;
        public decimal? Preg16 { get; set; } = 0;
        public decimal? Preg17 { get; set; } = 0;
        public decimal? Preg18 { get; set; } = 0;
        public decimal? Preg19 { get; set; } = 0;
        public decimal? Preg20 { get; set; } = 0;
        public decimal? Preg21 { get; set; } = 0;
        public decimal? Preg22 { get; set; } = 0;
        public decimal? Preg23 { get; set; } = 0;
        public decimal? Preg24 { get; set; } = 0;
        public decimal? Preg25 { get; set; } = 0;
        public decimal? Preg26 { get; set; } = 0;
        public decimal? Preg27 { get; set; } = 0;
        public decimal? Preg28 { get; set; } = 0;
        public decimal? Preg29 { get; set; } = 0;
        public decimal? Preg30 { get; set; } = 0;
        public decimal? Preg31 { get; set; } = 0;
        public decimal? Preg32 { get; set; } = 0;
        public decimal? Preg33 { get; set; } = 0;
        public decimal? Preg34 { get; set; } = 0;
        public decimal? Preg35 { get; set; } = 0;
        public decimal? Preg36 { get; set; } = 0;
        public decimal? Preg37 { get; set; } = 0;
        public decimal? Preg38 { get; set; } = 0;
        public decimal? Preg39 { get; set; } = 0;
        public decimal? Preg40 { get; set; } = 0;
        public decimal? Preg41 { get; set; } = 0;
        public decimal? Preg42 { get; set; } = 0;
        public decimal? Preg43 { get; set; } = 0;
        public decimal? Preg44 { get; set; } = 0;
        public decimal? Preg45 { get; set; } = 0;
        public decimal? Preg46 { get; set; } = 0;
        public decimal? Preg47 { get; set; } = 0;
        public decimal? Preg48 { get; set; } = 0;
        public decimal? Preg49 { get; set; } = 0;
        public decimal? Preg50 { get; set; } = 0;
        public decimal? Preg51 { get; set; } = 0;
        public decimal? Preg52 { get; set; } = 0;
        public decimal? Preg53 { get; set; } = 0;
        public decimal? Preg54 { get; set; } = 0;
        public decimal? Preg55 { get; set; } = 0;
        public decimal? Preg56 { get; set; } = 0;
        public decimal? Preg57 { get; set; } = 0;
        public decimal? Preg58 { get; set; } = 0;
        public decimal? Preg59 { get; set; } = 0;
        public decimal? Preg60 { get; set; } = 0;
        public decimal? Preg61 { get; set; } = 0;
        public decimal? Preg62 { get; set; } = 0;
        public decimal? Preg63 { get; set; } = 0;
        public decimal? Preg64 { get; set; } = 0;
        public decimal? Preg65 { get; set; } = 0;
        public decimal? Preg66 { get; set; } = 0;
        public decimal? Preg67 { get; set; } = 0;
        public decimal? Preg68 { get; set; } = 0;
        public decimal? Preg69 { get; set; } = 0;
        public decimal? Preg70 { get; set; } = 0;
        public decimal? Preg71 { get; set; } = 0;
        public decimal? Preg72 { get; set; } = 0;
        public decimal? Preg73 { get; set; } = 0;
        public decimal? Preg74 { get; set; } = 0;
        public decimal? Preg75 { get; set; } = 0;
        public decimal? Preg76 { get; set; } = 0;
        public decimal? Preg77 { get; set; } = 0;
        public decimal? Preg78 { get; set; } = 0;
        public decimal? Preg79 { get; set; } = 0;
        public decimal? Preg80 { get; set; } = 0;
        public decimal? Preg81 { get; set; } = 0;
        public decimal? Preg82 { get; set; } = 0;
        public decimal? Preg83 { get; set; } = 0;
        public decimal? Preg84 { get; set; } = 0;
        public decimal? Preg85 { get; set; } = 0;
        public decimal? Preg86 { get; set; } = 0;

        

        public List<ML.MensajesError> MensajesValidacion { get; set; }
        public bool Error { get; set; } = false;

        public Director Director { get; set; } = new Director();
        public GerenteGeneral GerenteGeneral { get; set; } = new GerenteGeneral();
        public GerenteDepartamental GerenteDepartamental { get; set; } = new GerenteDepartamental();
        public Subgerente Subgerente { get; set; } = new Subgerente();
        public COORDINADOR_SUPERVISOR_JEFE COORDINADOR_SUPERVISOR_JEFE { get; set; } = new COORDINADOR_SUPERVISOR_JEFE();
        public Administrativo Administrativo { get; set; } = new Administrativo();
        public Comercial Comercial { get; set; } = new Comercial();
        public TECNICO_OPERATIVO TECNICO_OPERATIVO { get; set; } = new TECNICO_OPERATIVO();
        public Ant_menos_de_6_meses Ant_menos_de_6_meses { get; set; } = new Ant_menos_de_6_meses();
        public Ant_6_meses_1_anio Ant_6_meses_1_anio { get; set; } = new Ant_6_meses_1_anio();
        public Ant_1_a_2_anios Ant_1_a_2_anios { get; set; } = new Ant_1_a_2_anios();
        public Ant_3_a_5_anios Ant_3_a_5_anios { get; set; } = new Ant_3_a_5_anios();
        public Ant_6_a_10_anios Ant_6_a_10_anios { get; set; } = new Ant_6_a_10_anios();
        public Ant_mas_de_10_anios Ant_mas_de_10_anios { get; set; } = new Ant_mas_de_10_anios();
        public Sexo_Masculino Sexo_Masculino { get; set; } = new Sexo_Masculino();
        public Sexo_Femenino Sexo_Femenino { get; set; } = new Sexo_Femenino();
        public Primaria Primaria { get; set; } = new Primaria();
        public Secundaria Secundaria { get; set; } = new Secundaria();
        public Media_Tecnica Media_Tecnica { get; set; } = new Media_Tecnica();
        public Media_Superior Media_Superior { get; set; } = new Media_Superior();
        public Universidad_Incompleta Universidad_Incompleta { get; set; } = new Universidad_Incompleta();
        public Universidad_Completa Universidad_Completa { get; set; } = new Universidad_Completa();
        public PostGrado PostGrado { get; set; } = new PostGrado();
        public Planta Planta { get; set; } = new Planta();
        public Sindicalizado Sindicalizado { get; set; } = new Sindicalizado();
        public Honorarios Honorarios { get; set; } = new Honorarios();
        public Comisionistas Comisionistas { get; set; } = new Comisionistas();
        public Temporal Temporal { get; set; } = new Temporal();
        public Edad_18_A_22_ANIOS Edad_18_A_22_ANIOS { get; set; } = new Edad_18_A_22_ANIOS();
        public Edad_23_A_31_ANIOS Edad_23_A_31_ANIOS { get; set; } = new Edad_23_A_31_ANIOS();
        public Edad_32_A_39_ANIOS Edad_32_A_39_ANIOS { get; set; } = new Edad_32_A_39_ANIOS();
        public Edad_40_A_55_ANIOS Edad_40_A_55_ANIOS { get; set; } = new Edad_40_A_55_ANIOS();
        public Edad_56_ANIOS_O_MAS Edad_56_ANIOS_O_MAS { get; set; } = new Edad_56_ANIOS_O_MAS();
    }

    //Clases auxiliares
    //Funcion
    //public decimal? Director { get; set; } = 0;
    public class Director
    {
        public int EntidadId { get; set; } = 0;
        public string EntidadNombre { get; set; } = String.Empty;
        public decimal Promedio66R { get; set; } = 0;
        public decimal Promedio86R { get; set; } = 0;
        public int Esperadas { get; set; } = 0;
        public int Contestadas { get; set; } = 0;
    }
    public class GerenteGeneral
    {
        public int EntidadId { get; set; } = 0;
        public string EntidadNombre { get; set; } = String.Empty;
        public decimal Promedio66R { get; set; } = 0;
        public decimal Promedio86R { get; set; } = 0;
        public int Esperadas { get; set; } = 0;
        public int Contestadas { get; set; } = 0;
    }
    public class GerenteDepartamental
    {
        public int EntidadId { get; set; } = 0;
        public string EntidadNombre { get; set; } = String.Empty;
        public decimal Promedio66R { get; set; } = 0;
        public decimal Promedio86R { get; set; } = 0;
        public int Esperadas { get; set; } = 0;
        public int Contestadas { get; set; } = 0;
    }
    public class Subgerente
    {
        public int EntidadId { get; set; } = 0;
        public string EntidadNombre { get; set; } = String.Empty;
        public decimal Promedio66R { get; set; } = 0;
        public decimal Promedio86R { get; set; } = 0;
        public int Esperadas { get; set; } = 0;
        public int Contestadas { get; set; } = 0;
    }
    public class COORDINADOR_SUPERVISOR_JEFE
    {
        public int EntidadId { get; set; } = 0;
        public string EntidadNombre { get; set; } = String.Empty;
        public decimal Promedio66R { get; set; } = 0;
        public decimal Promedio86R { get; set; } = 0;
        public int Esperadas { get; set; } = 0;
        public int Contestadas { get; set; } = 0;
    }
    public class Administrativo
    {
        public int EntidadId { get; set; } = 0;
        public string EntidadNombre { get; set; } = String.Empty;
        public decimal Promedio66R { get; set; } = 0;
        public decimal Promedio86R { get; set; } = 0;
        public int Esperadas { get; set; } = 0;
        public int Contestadas { get; set; } = 0;
    }
    public class Comercial
    {
        public int EntidadId { get; set; } = 0;
        public string EntidadNombre { get; set; } = String.Empty;
        public decimal Promedio66R { get; set; } = 0;
        public decimal Promedio86R { get; set; } = 0;
        public int Esperadas { get; set; } = 0;
        public int Contestadas { get; set; } = 0;
    }
    public class TECNICO_OPERATIVO
    {
        public int EntidadId { get; set; } = 0;
        public string EntidadNombre { get; set; } = String.Empty;
        public decimal Promedio66R { get; set; } = 0;
        public decimal Promedio86R { get; set; } = 0;
        public int Esperadas { get; set; } = 0;
        public int Contestadas { get; set; } = 0;
    }
    //Antiguedad
    //public decimal? Ant_menos_de_6_meses { get; set; } = 0;
    public class Ant_menos_de_6_meses
    {
        public int EntidadId { get; set; } = 0;
        public string EntidadNombre { get; set; } = String.Empty;
        public decimal Promedio66R { get; set; } = 0;
        public decimal Promedio86R { get; set; } = 0;
        public int Esperadas { get; set; } = 0;
        public int Contestadas { get; set; } = 0;
    }
    public class Ant_6_meses_1_anio
    {
        public int EntidadId { get; set; } = 0;
        public string EntidadNombre { get; set; } = String.Empty;
        public decimal Promedio66R { get; set; } = 0;
        public decimal Promedio86R { get; set; } = 0;
        public int Esperadas { get; set; } = 0;
        public int Contestadas { get; set; } = 0;
    }
    public class Ant_1_a_2_anios
    {
        public int EntidadId { get; set; } = 0;
        public string EntidadNombre { get; set; } = String.Empty;
        public decimal Promedio66R { get; set; } = 0;
        public decimal Promedio86R { get; set; } = 0;
        public int Esperadas { get; set; } = 0;
        public int Contestadas { get; set; } = 0;
    }
    public class Ant_3_a_5_anios
    {
        public int EntidadId { get; set; } = 0;
        public string EntidadNombre { get; set; } = String.Empty;
        public decimal Promedio66R { get; set; } = 0;
        public decimal Promedio86R { get; set; } = 0;
        public int Esperadas { get; set; } = 0;
        public int Contestadas { get; set; } = 0;
    }
    public class Ant_6_a_10_anios
    {
        public int EntidadId { get; set; } = 0;
        public string EntidadNombre { get; set; } = String.Empty;
        public decimal Promedio66R { get; set; } = 0;
        public decimal Promedio86R { get; set; } = 0;
        public int Esperadas { get; set; } = 0;
        public int Contestadas { get; set; } = 0;
    }
    public class Ant_mas_de_10_anios
    {
        public int EntidadId { get; set; } = 0;
        public string EntidadNombre { get; set; } = String.Empty;
        public decimal Promedio66R { get; set; } = 0;
        public decimal Promedio86R { get; set; } = 0;
        public int Esperadas { get; set; } = 0;
        public int Contestadas { get; set; } = 0;
    }
    //Genero
    //public decimal? Sexo_Masculino { get; set; } = 0;
    public class Sexo_Masculino
    {
        public int EntidadId { get; set; } = 0;
        public string EntidadNombre { get; set; } = String.Empty;
        public decimal Promedio66R { get; set; } = 0;
        public decimal Promedio86R { get; set; } = 0;
        public int Esperadas { get; set; } = 0;
        public int Contestadas { get; set; } = 0;
    }
    public class Sexo_Femenino
    {
        public int EntidadId { get; set; } = 0;
        public string EntidadNombre { get; set; } = String.Empty;
        public decimal Promedio66R { get; set; } = 0;
        public decimal Promedio86R { get; set; } = 0;
        public int Esperadas { get; set; } = 0;
        public int Contestadas { get; set; } = 0;
    }
    //Academico
    //public decimal? Primaria { get; set; } = 0;
    public class Primaria
    {
        public int EntidadId { get; set; } = 0;
        public string EntidadNombre { get; set; } = String.Empty;
        public decimal Promedio66R { get; set; } = 0;
        public decimal Promedio86R { get; set; } = 0;
        public int Esperadas { get; set; } = 0;
        public int Contestadas { get; set; } = 0;
    }
    public class Secundaria
    {
        public int EntidadId { get; set; } = 0;
        public string EntidadNombre { get; set; } = String.Empty;
        public decimal Promedio66R { get; set; } = 0;
        public decimal Promedio86R { get; set; } = 0;
        public int Esperadas { get; set; } = 0;
        public int Contestadas { get; set; } = 0;
    }
    public class Media_Tecnica
    {
        public int EntidadId { get; set; } = 0;
        public string EntidadNombre { get; set; } = String.Empty;
        public decimal Promedio66R { get; set; } = 0;
        public decimal Promedio86R { get; set; } = 0;
        public int Esperadas { get; set; } = 0;
        public int Contestadas { get; set; } = 0;
    }
    public class Media_Superior
    {
        public int EntidadId { get; set; } = 0;
        public string EntidadNombre { get; set; } = String.Empty;
        public decimal Promedio66R { get; set; } = 0;
        public decimal Promedio86R { get; set; } = 0;
        public int Esperadas { get; set; } = 0;
        public int Contestadas { get; set; } = 0;
    }
    public class Universidad_Incompleta
    {
        public int EntidadId { get; set; } = 0;
        public string EntidadNombre { get; set; } = String.Empty;
        public decimal Promedio66R { get; set; } = 0;
        public decimal Promedio86R { get; set; } = 0;
        public int Esperadas { get; set; } = 0;
        public int Contestadas { get; set; } = 0;
    }
    public class Universidad_Completa
    {
        public int EntidadId { get; set; } = 0;
        public string EntidadNombre { get; set; } = String.Empty;
        public decimal Promedio66R { get; set; } = 0;
        public decimal Promedio86R { get; set; } = 0;
        public int Esperadas { get; set; } = 0;
        public int Contestadas { get; set; } = 0;
    }
    public class PostGrado
    {
        public int EntidadId { get; set; } = 0;
        public string EntidadNombre { get; set; } = String.Empty;
        public decimal Promedio66R { get; set; } = 0;
        public decimal Promedio86R { get; set; } = 0;
        public int Esperadas { get; set; } = 0;
        public int Contestadas { get; set; } = 0;
    }
    //Condicion trabajo
    //public decimal? Planta { get; set; } = 0;
    public class Planta
    {
        public int EntidadId { get; set; } = 0;
        public string EntidadNombre { get; set; } = String.Empty;
        public decimal Promedio66R { get; set; } = 0;
        public decimal Promedio86R { get; set; } = 0;
        public int Esperadas { get; set; } = 0;
        public int Contestadas { get; set; } = 0;
    }
    public class Sindicalizado
    {
        public int EntidadId { get; set; } = 0;
        public string EntidadNombre { get; set; } = String.Empty;
        public decimal Promedio66R { get; set; } = 0;
        public decimal Promedio86R { get; set; } = 0;
        public int Esperadas { get; set; } = 0;
        public int Contestadas { get; set; } = 0;
    }
    public class Honorarios
    {
        public int EntidadId { get; set; } = 0;
        public string EntidadNombre { get; set; } = String.Empty;
        public decimal Promedio66R { get; set; } = 0;
        public decimal Promedio86R { get; set; } = 0;
        public int Esperadas { get; set; } = 0;
        public int Contestadas { get; set; } = 0;
    }
    public class Comisionistas
    {
        public int EntidadId { get; set; } = 0;
        public string EntidadNombre { get; set; } = String.Empty;
        public decimal Promedio66R { get; set; } = 0;
        public decimal Promedio86R { get; set; } = 0;
        public int Esperadas { get; set; } = 0;
        public int Contestadas { get; set; } = 0;
    }
    public class Temporal
    {
        public int EntidadId { get; set; } = 0;
        public string EntidadNombre { get; set; } = String.Empty;
        public decimal Promedio66R { get; set; } = 0;
        public decimal Promedio86R { get; set; } = 0;
        public int Esperadas { get; set; } = 0;
        public int Contestadas { get; set; } = 0;
    }
    //Edad
    //public decimal? Edad_18_A_22_ANIOS { get; set; } = 0;
    public class Edad_18_A_22_ANIOS
    {
        public int EntidadId { get; set; } = 0;
        public string EntidadNombre { get; set; } = String.Empty;
        public decimal Promedio66R { get; set; } = 0;
        public decimal Promedio86R { get; set; } = 0;
        public int Esperadas { get; set; } = 0;
        public int Contestadas { get; set; } = 0;
    }
    public class Edad_23_A_31_ANIOS
    {
        public int EntidadId { get; set; } = 0;
        public string EntidadNombre { get; set; } = String.Empty;
        public decimal Promedio66R { get; set; } = 0;
        public decimal Promedio86R { get; set; } = 0;
        public int Esperadas { get; set; } = 0;
        public int Contestadas { get; set; } = 0;
    }
    public class Edad_32_A_39_ANIOS
    {
        public int EntidadId { get; set; } = 0;
        public string EntidadNombre { get; set; } = String.Empty;
        public decimal Promedio66R { get; set; } = 0;
        public decimal Promedio86R { get; set; } = 0;
        public int Esperadas { get; set; } = 0;
        public int Contestadas { get; set; } = 0;
    }
    public class Edad_40_A_55_ANIOS
    {
        public int EntidadId { get; set; } = 0;
        public string EntidadNombre { get; set; } = String.Empty;
        public decimal Promedio66R { get; set; } = 0;
        public decimal Promedio86R { get; set; } = 0;
        public int Esperadas { get; set; } = 0;
        public int Contestadas { get; set; } = 0;
    }
    public class Edad_56_ANIOS_O_MAS
    {
        public int EntidadId { get; set; } = 0;
        public string EntidadNombre { get; set; } = String.Empty;
        public decimal Promedio66R { get; set; } = 0;
        public decimal Promedio86R { get; set; } = 0;
        public int Esperadas { get; set; } = 0;
        public int Contestadas { get; set; } = 0;
    }

}
