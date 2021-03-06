using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class EmpleadoRespuesta
    {
        public int IdEnfoque { get; set; }
        public int IdEmpleadoRespuestas { get; set; }
        public ML.Empleado Empleado { get; set; }
        public ML.Preguntas Pregunta { get; set; }
        public ML.Respuestas Respuesta { get; set; }
        public string RespuestaEmpleado { get; set; }
        public DateTime FechaHoraCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string ProgramaCreacion { get; set; }
        public DateTime FechaHoraModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string ProgramaModificacion { get; set; }
        public DateTime FechaHoraEliminacion { get; set; }
        public string UsuarioEliminacion { get; set; }
        public string ProgramaEliminacion { get; set; }
        public List<string> ListaRespuestas { get; set; }
        
        //Mapping cada Respuesta
        public string EMP_R1 { get; set; }
        public string EMP_R2 { get; set; }
        public string EMP_R3 { get; set; }
        public string EMP_R4 { get; set; }
        public string EMP_R5 { get; set; }
        public string EMP_R6 { get; set; }
        public string EMP_R7 { get; set; }
        public string EMP_R8 { get; set; }
        public string EMP_R9 { get; set; }
        public string EMP_R10 { get; set; }
        public string EMP_R11 { get; set; }
        public string EMP_R12 { get; set; }
        public string EMP_R13 { get; set; }
        public string EMP_R14 { get; set; }
        public string EMP_R15 { get; set; }
        public string EMP_R16 { get; set; }
        public string EMP_R17 { get; set; }
        public string EMP_R18 { get; set; }
        public string EMP_R19 { get; set; }
        public string EMP_R20 { get; set; }
        public string EMP_R21 { get; set; }
        public string EMP_R22 { get; set; }
        public string EMP_R23 { get; set; }
        public string EMP_R24 { get; set; }
        public string EMP_R25 { get; set; }
        public string EMP_R26 { get; set; }
        public string EMP_R27 { get; set; }
        public string EMP_R28 { get; set; }
        public string EMP_R29 { get; set; }
        public string EMP_R30 { get; set; }

        public string EMP_R31 { get; set; }
        public string EMP_R32 { get; set; }
        public string EMP_R33 { get; set; }
        public string EMP_R34 { get; set; }
        public string EMP_R35 { get; set; }
        public string EMP_R36 { get; set; }
        public string EMP_R37 { get; set; }
        public string EMP_R38 { get; set; }
        public string EMP_R39 { get; set; }
        public string EMP_R40 { get; set; }

        public string EMP_R41 { get; set; }
        public string EMP_R42 { get; set; }
        public string EMP_R43 { get; set; }
        public string EMP_R44 { get; set; }
        public string EMP_R45 { get; set; }
        public string EMP_R46 { get; set; }
        public string EMP_R47 { get; set; }
        public string EMP_R48 { get; set; }
        public string EMP_R49 { get; set; }
        public string EMP_R50 { get; set; }
        public string Demografico { get; set; }
        public string EMP_R51 { get; set; }
        public string EMP_R52 { get; set; }
        public string EMP_R53 { get; set; }
        public string EMP_R54 { get; set; }
        public string EMP_R55 { get; set; }
        public string EMP_R56 { get; set; }
        public string EMP_R57 { get; set; }
        public string EMP_R58 { get; set; }
        public string EMP_R59 { get; set; }
        public string EMP_R60 { get; set; }

        public string EMP_R61 { get; set; }
        public string EMP_R62 { get; set; }
        public string EMP_R63 { get; set; }
        public string EMP_R64 { get; set; }
        public string EMP_R65 { get; set; }
        public string EMP_R66 { get; set; }
        public string EMP_R67 { get; set; }
        public string EMP_R68 { get; set; }
        public string EMP_R69 { get; set; }
        public string EMP_R70 { get; set; }

        public string EMP_R71 { get; set; }
        public string EMP_R72 { get; set; }
        public string EMP_R73 { get; set; }
        public string EMP_R74 { get; set; }
        public string EMP_R75 { get; set; }
        public string EMP_R76 { get; set; }
        public string EMP_R77 { get; set; }
        public string EMP_R78 { get; set; }
        public string EMP_R79 { get; set; }
        public string EMP_R80 { get; set; }

        public string EMP_R81 { get; set; }
        public string EMP_R82 { get; set; }
        public string EMP_R83 { get; set; }
        public string EMP_R84 { get; set; }
        public string EMP_R85 { get; set; }
        public string EMP_R86 { get; set; }


        //Mapping Enfoque Area
        public string ARE_1 { get; set; }
        public string ARE_2 { get; set; }
        public string ARE_3 { get; set; }
        public string ARE_4 { get; set; }
        public string ARE_5 { get; set; }
        public string ARE_6 { get; set; }
        public string ARE_7 { get; set; }
        public string ARE_8 { get; set; }
        public string ARE_9 { get; set; }
        public string ARE_10 { get; set; }

        public string ARE_11 { get; set; }
        public string ARE_12 { get; set; }
        public string ARE_13 { get; set; }
        public string ARE_14 { get; set; }
        public string ARE_15 { get; set; }
        public string ARE_16 { get; set; }
        public string ARE_17 { get; set; }
        public string ARE_18 { get; set; }
        public string ARE_19 { get; set; }
        public string ARE_20 { get; set; }

        public string ARE_21 { get; set; }
        public string ARE_22 { get; set; }
        public string ARE_23 { get; set; }
        public string ARE_24 { get; set; }
        public string ARE_25 { get; set; }
        public string ARE_26 { get; set; }
        public string ARE_27 { get; set; }
        public string ARE_28 { get; set; }
        public string ARE_29 { get; set; }
        public string ARE_30 { get; set; }

        public string ARE_31 { get; set; }
        public string ARE_32 { get; set; }
        public string ARE_33 { get; set; }
        public string ARE_34 { get; set; }
        public string ARE_35 { get; set; }
        public string ARE_36 { get; set; }
        public string ARE_37 { get; set; }
        public string ARE_38 { get; set; }
        public string ARE_39 { get; set; }
        public string ARE_40 { get; set; }

        public string ARE_41 { get; set; }
        public string ARE_42 { get; set; }
        public string ARE_43 { get; set; }
        public string ARE_44 { get; set; }
        public string ARE_45 { get; set; }
        public string ARE_46 { get; set; }
        public string ARE_47 { get; set; }
        public string ARE_48 { get; set; }
        public string ARE_49 { get; set; }
        public string ARE_50 { get; set; }

        public string ARE_51 { get; set; }
        public string ARE_52 { get; set; }
        public string ARE_53 { get; set; }
        public string ARE_54 { get; set; }
        public string ARE_55 { get; set; }
        public string ARE_56 { get; set; }
        public string ARE_57 { get; set; }
        public string ARE_58 { get; set; }
        public string ARE_59 { get; set; }
        public string ARE_60 { get; set; }

        public string ARE_61 { get; set; }
        public string ARE_62 { get; set; }
        public string ARE_63 { get; set; }
        public string ARE_64 { get; set; }
        public string ARE_65 { get; set; }
        public string ARE_66 { get; set; }
        public string ARE_67 { get; set; }
        public string ARE_68 { get; set; }
        public string ARE_69 { get; set; }
        public string ARE_70 { get; set; }

        public string ARE_71 { get; set; }
        public string ARE_72 { get; set; }
        public string ARE_73 { get; set; }
        public string ARE_74 { get; set; }
        public string ARE_75 { get; set; }
        public string ARE_76 { get; set; }
        public string ARE_77 { get; set; }
        public string ARE_78 { get; set; }
        public string ARE_79 { get; set; }
        public string ARE_80 { get; set; }

        public string ARE_81 { get; set; }
        public string ARE_82 { get; set; }
        public string ARE_83 { get; set; }
        public string ARE_84 { get; set; }
        public string ARE_85 { get; set; }
        public string ARE_86 { get; set; }

        //Mapping open Questions
        public string ESPECIAL_OPEN { get; set; }
        public string CAMBIAR_OPEN { get; set; }
        public string FORT_JEFE { get; set; }
        public string OPORT_JEFE { get; set; }
        public string MOTIVA_TRAB { get; set; }
        public string DEJAR_EMP { get; set; }
        public string PRESION { get; set; }
        public string ANTIGUEDAD { get; set; }
        public string RANGO_EDAD { get; set; }
        public string CONDICION { get; set; }
        public string SEXO { get; set; }
        public string ACADEMICO { get; set; }
        public string FUNCION { get; set; }
        public string UNIDAD { get; set; }
        public string DIRECION { get; set; }
        public string AREA { get; set; }
        public string DEPARTAMENTO { get; set; }
        public string Abierta1 { get; set; }
        public string Abierta2 { get; set; }
        public string Abierta3 { get; set; }
        public string Abierta4 { get; set; }
        public string Abierta5 { get; set; }
        public string Abierta6 { get; set; }
        public string Abierta7 { get; set; }

    }
}
