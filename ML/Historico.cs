using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class minHistorico
    {
        public int IdBaseDeDatos { get; set; } = 0;
        public int EntidadId { get; set; } = 0;
        public string EntidadNombre { get; set; } = String.Empty;
        public int Anio { get; set; } = 0;
        public int IdTipoEntidad { get; set; }
    }
    public class Historico
    {
        //usuario|password|opc|tipoEntidad|entidadNombre|anio
        public int opc { get; set; } = 0;
        public int tipoEntidad { get; set; } = 0;
        public string EntidadName { get; set; } = "";
        
        public int enfoqueSeleccionado { get; set; }

        public string nivelDetalle { get; set; }
        public string ps { get; set; } = string.Empty;
        public int IdBaseDeDatos { get; set; } = 0;
        public int idEncuesta { get; set; }
        public string currentURL { get; set; } = "";
        public string CurrentUsr { get; set; } = String.Empty;
        [Key]
        public int IdHistorico { get; set; }
        [Required(ErrorMessage = "El Año es requerido")]
        [Range(2000, 5000, ErrorMessage = "El año no puede ser menor a 2000")]
        public int? Anio { get; set; } = 0;
        [Required(ErrorMessage = "Debes seleccionar el tipo de entidad al que pertenece el histórico")]
        [Range(1, 4, ErrorMessage = "El tipo de entidad debe estar entre 1 y 4")]
        public int? IdTipoEntidad { get; set; } = 0;//1...4
        public string Entidad { get; set; } = String.Empty;
        public int? EntidadId { get; set; } = 0;
        [Required(AllowEmptyStrings = false, ErrorMessage = "El nombre de la entidad es requerido")]
        public string EntidadNombre { get; set; } = String.Empty;
        [Required(ErrorMessage = "El Head Count es requerido")]
        public int? HC { get; set; } = 0;
        [Required(ErrorMessage = "El porcentaje de nivel de participacion es requerido")]
        public decimal? NivelParticipacionEE { get; set; } = 0;
        [Required(ErrorMessage = "El calificacion global es requerida")]
        public decimal? CalificacionGlobalEE { get; set; } = 0;
        [Required(ErrorMessage = "El nivel de confianza es requerido")]
        public decimal? NivelConfianzaEE { get; set; } = 0;
        [Required(ErrorMessage = "El porcentaje de nivel de compromiso es requerido")]
        public decimal? NivelCompromisoEE { get; set; } = 0;
        [Required(ErrorMessage = "El porcentaje de nivel de colaboración es requerido")]
        public decimal? NivelColaboracionEE { get; set; } = 0;
        [Required(ErrorMessage = "El porcentaje de credibilidad es requerida")]
        public decimal? CreedibilidadEE { get; set; } = 0;
        [Required(ErrorMessage = "El porcentaje de imparcialidad es requerida")]
        public decimal? ImparcialidadEE { get; set; } = 0;
        [Required(ErrorMessage = "El porcentaje de orgullo es requerido")]
        public decimal? OrgulloEE { get; set; } = 0;
        [Required(ErrorMessage = "El porcentaje de respeto es requerido")]
        public decimal? RespetoEE { get; set; } = 0;
        [Required(ErrorMessage = "El porcentaje de compañerismo es requerido")]
        public decimal? CompanierismoEE { get; set; } = 0;
        [Required(ErrorMessage = "El porcentaje de coaching es requerido")]
        public decimal? CoachingEE { get; set; } = 0;
        [Required(ErrorMessage = "El porcentaje de habilidades genrenciales es requerido")]
        public decimal? HabilidadesGerencialesEE { get; set; } = 0;
        [Required(ErrorMessage = "El porcentaje de alineación estratégica es requerido")]
        public decimal? AlineacionEstrategicaEE { get; set; } = 0;
        [Required(ErrorMessage = "El porcentaje de prácticas culturales es requerido")]
        public decimal? PracticasCulturalesEE { get; set; } = 0;
        [Required(ErrorMessage = "El porcentaje de manejo del cambio es requerido")]
        public decimal? ManejoDelCambioEE { get; set; } = 0;
        [Required(ErrorMessage = "El porcentaje de procesos organizacionales es requerido")]
        public decimal? ProcesosOrganizacionalesEE { get; set; } = 0;


        public int Esperadas { get; set; } = 0;
        public int Contestadas { get; set; } = 0;

        public decimal? CalificacionGlobalEA { get; set; } = 0;
        public decimal? NivelConfianzaEA { get; set; } = 0;
        public decimal? NivelCompromisoEA { get; set; } = 0;
        public decimal? NivelColaboracionEA { get; set; } = 0;
        public decimal? CreedibilidadEA { get; set; } = 0;
        public decimal? ImparcialidadEA { get; set; } = 0;
        public decimal? OrgulloEA { get; set; } = 0;
        public decimal? RespetoEA { get; set; } = 0;
        public decimal? CompanierismoEA { get; set; } = 0;
        public decimal? CoachingEA { get; set; } = 0;
        public decimal? HabilidadesGerencialesEA { get; set; } = 0;
        public decimal? AlineacionEstrategicaEA { get; set; } = 0;
        public decimal? PracticasCulturalesEA { get; set; } = 0;
        public decimal? ManejoDelCambioEA { get; set; } = 0;
        public decimal? ProcesosOrganizacionalesEA { get; set; } = 0;

        #region Sección demográfico
        //Antiguedad
        public decimal? CGEE_Ant_menos_de_6_meses { get; set; } = 0;
        public decimal? CG_EE_Ant_6_meses_1_anio { get; set; } = 0;
        public decimal? CG_EE_Ant_1_a_2_anios { get; set; } = 0;
        public decimal? CG_EE_Ant_3_a_5_anios { get; set; } = 0;
        public decimal? CGEE_Ant_6_a_10_anios { get; set; } = 0;
        public decimal? CG_EE_Ant_mas_de_10_anios { get; set; } = 0;
        //Genero
        public decimal? CG_EE_Sexo_Masculino { get; set; } = 0;
        public decimal? CG_EE_Sexo_Femenino { get; set; } = 0;
        //Academico
        public decimal? CG_EE_Primaria { get; set; } = 0;
        public decimal? CG_EE_Secundaria { get; set; } = 0;
        public decimal? CG_EE_Media_Tecnica { get; set; } = 0;
        public decimal? CG_EE_Media_Superior { get; set; } = 0;
        public decimal? CG_EE_Universidad_Incompleta { get; set; } = 0;
        public decimal? CG_EE_Universidad_Completa { get; set; } = 0;
        public decimal? CG_EE_PostGrado { get; set; } = 0;
        //Condicion trabajo
        public decimal? CG_EE_Planta { get; set; } = 0;
        public decimal? CG_EE_Sindicalizado { get; set; } = 0;
        public decimal? CG_EE_Honorarios { get; set; } = 0;
        public decimal? CG_EE_Comisionistas { get; set; } = 0;
        public decimal? CG_EE_Temporal { get; set; } = 0;
        //Funcion
        public decimal? CG_EE_Director { get; set; } = 0;
        public decimal? CG_EE_GerenteGeneral { get; set; } = 0;
        public decimal? CG_EE_GerenteDepartamental { get; set; } = 0;
        public decimal? CG_EE_Subgerente { get; set; } = 0;
        public decimal? CG_EE_COORDINADOR_SUPERVISOR_JEFE { get; set; } = 0;
        public decimal? CG_EE_Administrativo { get; set; } = 0;
        public decimal? CG_EE_Comercial { get; set; } = 0;
        public decimal? CG_EE_TECNICO_OPERATIVO { get; set; } = 0;
        //Edad
        public decimal? CG_EE_18_A_22_ANIOS { get; set; } = 0;
        public decimal? CG_EE_23_A_31_ANIOS { get; set; } = 0;
        public decimal? CG_EE_32_A_39_ANIOS { get; set; } = 0;
        public decimal? CG_EE_40_A_55_ANIOS { get; set; } = 0;
        public decimal? CG_EE_56_ANIOS_O_MAS { get; set; } = 0;

        //Sección demográfico EA
        //Antiguedad
        public decimal? CGEA_Ant_menos_de_6_meses { get; set; } = 0;
        public decimal? CG_EA_Ant_6_meses_1_anio { get; set; } = 0;
        public decimal? CG_EA_Ant_1_a_2_anios { get; set; } = 0;
        public decimal? CG_EA_Ant_3_a_5_anios { get; set; } = 0;
        public decimal? CGEA_Ant_6_a_10_anios { get; set; } = 0;
        public decimal? CG_EA_Ant_mas_de_10_anios { get; set; } = 0;
        //Genero
        public decimal? CG_EA_Sexo_Masculino { get; set; } = 0;
        public decimal? CG_EA_Sexo_Femenino { get; set; } = 0;
        //Academico
        public decimal? CG_EA_Primaria { get; set; } = 0;
        public decimal? CG_EA_Secundaria { get; set; } = 0;
        public decimal? CG_EA_Media_Tecnica { get; set; } = 0;
        public decimal? CG_EA_Media_Superior { get; set; } = 0;
        public decimal? CG_EA_Universidad_Incompleta { get; set; } = 0;
        public decimal? CG_EA_Universidad_Completa { get; set; } = 0;
        public decimal? CG_EA_PostGrado { get; set; } = 0;
        //Condicion trabajo
        public decimal? CG_EA_Planta { get; set; } = 0;
        public decimal? CG_EA_Sindicalizado { get; set; } = 0;
        public decimal? CG_EA_Honorarios { get; set; } = 0;
        public decimal? CG_EA_Comisionistas { get; set; } = 0;
        public decimal? CG_EA_Temporal { get; set; } = 0;
        //Funcion
        public decimal? CG_EA_Director { get; set; } = 0;
        public decimal? CG_EA_GerenteGeneral { get; set; } = 0;
        public decimal? CG_EA_GerenteDepartamental { get; set; } = 0;
        public decimal? CG_EA_Subgerente { get; set; } = 0;
        public decimal? CG_EA_COORDINADOR_SUPERVISOR_JEFE { get; set; } = 0;
        public decimal? CG_EA_Administrativo { get; set; } = 0;
        public decimal? CG_EA_Comercial { get; set; } = 0;
        public decimal? CG_EA_TECNICO_OPERATIVO { get; set; } = 0;
        //Edad
        public decimal? CG_EA_18_A_22_ANIOS { get; set; } = 0;
        public decimal? CG_EA_23_A_31_ANIOS { get; set; } = 0;
        public decimal? CG_EA_32_A_39_ANIOS { get; set; } = 0;
        public decimal? CG_EA_40_A_55_ANIOS { get; set; } = 0;
        public decimal? CG_EA_56_ANIOS_O_MAS { get; set; } = 0;
        #endregion Seccion demografico

        /*
         * Seccion preguntas
        */
        public decimal? Preg1EE { get; set; } = 0;
        public decimal? Preg2EE { get; set; } = 0;
        public decimal? Preg3EE { get; set; } = 0;
        public decimal? Preg4EE { get; set; } = 0;
        public decimal? Preg5EE { get; set; } = 0;
        public decimal? Preg6EE { get; set; } = 0;
        public decimal? Preg7EE { get; set; } = 0;
        public decimal? Preg8EE { get; set; } = 0;
        public decimal? Preg9EE { get; set; } = 0;
        public decimal? Preg10EE { get; set; } = 0;
        public decimal? Preg11EE { get; set; } = 0;
        public decimal? Preg12EE { get; set; } = 0;
        public decimal? Preg13EE { get; set; } = 0;
        public decimal? Preg14EE { get; set; } = 0;
        public decimal? Preg15EE { get; set; } = 0;
        public decimal? Preg16EE { get; set; } = 0;
        public decimal? Preg17EE { get; set; } = 0;
        public decimal? Preg18EE { get; set; } = 0;
        public decimal? Preg19EE { get; set; } = 0;
        public decimal? Preg20EE { get; set; } = 0;
        public decimal? Preg21EE { get; set; } = 0;
        public decimal? Preg22EE { get; set; } = 0;
        public decimal? Preg23EE { get; set; } = 0;
        public decimal? Preg24EE { get; set; } = 0;
        public decimal? Preg25EE { get; set; } = 0;
        public decimal? Preg26EE { get; set; } = 0;
        public decimal? Preg27EE { get; set; } = 0;
        public decimal? Preg28EE { get; set; } = 0;
        public decimal? Preg29EE { get; set; } = 0;
        public decimal? Preg30EE { get; set; } = 0;
        public decimal? Preg31EE { get; set; } = 0;
        public decimal? Preg32EE { get; set; } = 0;
        public decimal? Preg33EE { get; set; } = 0;
        public decimal? Preg34EE { get; set; } = 0;
        public decimal? Preg35EE { get; set; } = 0;
        public decimal? Preg36EE { get; set; } = 0;
        public decimal? Preg37EE { get; set; } = 0;
        public decimal? Preg38EE { get; set; } = 0;
        public decimal? Preg39EE { get; set; } = 0;
        public decimal? Preg40EE { get; set; } = 0;
        public decimal? Preg41EE { get; set; } = 0;
        public decimal? Preg42EE { get; set; } = 0;
        public decimal? Preg43EE { get; set; } = 0;
        public decimal? Preg44EE { get; set; } = 0;
        public decimal? Preg45EE { get; set; } = 0;
        public decimal? Preg46EE { get; set; } = 0;
        public decimal? Preg47EE { get; set; } = 0;
        public decimal? Preg48EE { get; set; } = 0;
        public decimal? Preg49EE { get; set; } = 0;
        public decimal? Preg50EE { get; set; } = 0;
        public decimal? Preg51EE { get; set; } = 0;
        public decimal? Preg52EE { get; set; } = 0;
        public decimal? Preg53EE { get; set; } = 0;
        public decimal? Preg54EE { get; set; } = 0;
        public decimal? Preg55EE { get; set; } = 0;
        public decimal? Preg56EE { get; set; } = 0;
        public decimal? Preg57EE { get; set; } = 0;
        public decimal? Preg58EE { get; set; } = 0;
        public decimal? Preg59EE { get; set; } = 0;
        public decimal? Preg60EE { get; set; } = 0;
        public decimal? Preg61EE { get; set; } = 0;
        public decimal? Preg62EE { get; set; } = 0;
        public decimal? Preg63EE { get; set; } = 0;
        public decimal? Preg64EE { get; set; } = 0;
        public decimal? Preg65EE { get; set; } = 0;
        public decimal? Preg66EE { get; set; } = 0;
        public decimal? Preg67EE { get; set; } = 0;
        public decimal? Preg68EE { get; set; } = 0;
        public decimal? Preg69EE { get; set; } = 0;
        public decimal? Preg70EE { get; set; } = 0;
        public decimal? Preg71EE { get; set; } = 0;
        public decimal? Preg72EE { get; set; } = 0;
        public decimal? Preg73EE { get; set; } = 0;
        public decimal? Preg74EE { get; set; } = 0;
        public decimal? Preg75EE { get; set; } = 0;
        public decimal? Preg76EE { get; set; } = 0;
        public decimal? Preg77EE { get; set; } = 0;
        public decimal? Preg78EE { get; set; } = 0;
        public decimal? Preg79EE { get; set; } = 0;
        public decimal? Preg80EE { get; set; } = 0;
        public decimal? Preg81EE { get; set; } = 0;
        public decimal? Preg82EE { get; set; } = 0;
        public decimal? Preg83EE { get; set; } = 0;
        public decimal? Preg84EE { get; set; } = 0;
        public decimal? Preg85EE { get; set; } = 0;
        public decimal? Preg86EE { get; set; } = 0;

        public decimal? Preg1EA { get; set; } = 0;
        public decimal? Preg2EA { get; set; } = 0;
        public decimal? Preg3EA { get; set; } = 0;
        public decimal? Preg4EA { get; set; } = 0;
        public decimal? Preg5EA { get; set; } = 0;
        public decimal? Preg6EA { get; set; } = 0;
        public decimal? Preg7EA { get; set; } = 0;
        public decimal? Preg8EA { get; set; } = 0;
        public decimal? Preg9EA { get; set; } = 0;
        public decimal? Preg10EA { get; set; } = 0;
        public decimal? Preg11EA { get; set; } = 0;
        public decimal? Preg12EA { get; set; } = 0;
        public decimal? Preg13EA { get; set; } = 0;
        public decimal? Preg14EA { get; set; } = 0;
        public decimal? Preg15EA { get; set; } = 0;
        public decimal? Preg16EA { get; set; } = 0;
        public decimal? Preg17EA { get; set; } = 0;
        public decimal? Preg18EA { get; set; } = 0;
        public decimal? Preg19EA { get; set; } = 0;
        public decimal? Preg20EA { get; set; } = 0;
        public decimal? Preg21EA { get; set; } = 0;
        public decimal? Preg22EA { get; set; } = 0;
        public decimal? Preg23EA { get; set; } = 0;
        public decimal? Preg24EA { get; set; } = 0;
        public decimal? Preg25EA { get; set; } = 0;
        public decimal? Preg26EA { get; set; } = 0;
        public decimal? Preg27EA { get; set; } = 0;
        public decimal? Preg28EA { get; set; } = 0;
        public decimal? Preg29EA { get; set; } = 0;
        public decimal? Preg30EA { get; set; } = 0;
        public decimal? Preg31EA { get; set; } = 0;
        public decimal? Preg32EA { get; set; } = 0;
        public decimal? Preg33EA { get; set; } = 0;
        public decimal? Preg34EA { get; set; } = 0;
        public decimal? Preg35EA { get; set; } = 0;
        public decimal? Preg36EA { get; set; } = 0;
        public decimal? Preg37EA { get; set; } = 0;
        public decimal? Preg38EA { get; set; } = 0;
        public decimal? Preg39EA { get; set; } = 0;
        public decimal? Preg40EA { get; set; } = 0;
        public decimal? Preg41EA { get; set; } = 0;
        public decimal? Preg42EA { get; set; } = 0;
        public decimal? Preg43EA { get; set; } = 0;
        public decimal? Preg44EA { get; set; } = 0;
        public decimal? Preg45EA { get; set; } = 0;
        public decimal? Preg46EA { get; set; } = 0;
        public decimal? Preg47EA { get; set; } = 0;
        public decimal? Preg48EA { get; set; } = 0;
        public decimal? Preg49EA { get; set; }= 0;
        public decimal? Preg50EA { get; set; }= 0;
        public decimal? Preg51EA { get; set; }= 0;
        public decimal? Preg52EA { get; set; }= 0;
        public decimal? Preg53EA { get; set; }= 0;
        public decimal? Preg54EA { get; set; }= 0;
        public decimal? Preg55EA { get; set; }= 0;
        public decimal? Preg56EA { get; set; }= 0;
        public decimal? Preg57EA { get; set; }= 0;
        public decimal? Preg58EA { get; set; }= 0;
        public decimal? Preg59EA { get; set; }= 0;
        public decimal? Preg60EA { get; set; }= 0;
        public decimal? Preg61EA { get; set; }= 0;
        public decimal? Preg62EA { get; set; }= 0;
        public decimal? Preg63EA { get; set; }= 0;
        public decimal? Preg64EA { get; set; }= 0;
        public decimal? Preg65EA { get; set; }= 0;
        public decimal? Preg66EA { get; set; }= 0;
        public decimal? Preg67EA { get; set; }= 0;
        public decimal? Preg68EA { get; set; }= 0;
        public decimal? Preg69EA { get; set; }= 0;
        public decimal? Preg70EA { get; set; }= 0;
        public decimal? Preg71EA { get; set; }= 0;
        public decimal? Preg72EA { get; set; }= 0;
        public decimal? Preg73EA { get; set; }= 0;
        public decimal? Preg74EA { get; set; }= 0;
        public decimal? Preg75EA { get; set; }= 0;
        public decimal? Preg76EA { get; set; }= 0;
        public decimal? Preg77EA { get; set; }= 0;
        public decimal? Preg78EA { get; set; }= 0;
        public decimal? Preg79EA { get; set; }= 0;
        public decimal? Preg80EA { get; set; }= 0;
        public decimal? Preg81EA { get; set; }= 0;
        public decimal? Preg82EA { get; set; }= 0;
        public decimal? Preg83EA { get; set; }= 0;
        public decimal? Preg84EA { get; set; }= 0;
        public decimal? Preg85EA { get; set; }= 0;
        public decimal? Preg86EA { get; set; }= 0;

        public List<ML.MensajesError> MensajesValidacion { get; set; }

        public bool Error { get; set; } = false;
    }
}
