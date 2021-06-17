using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class ReportesClima
    {
        public static ML.Result GetResultadosByUnidadNegocio(string IdUnidadNegocio, int IdBD)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    var getPreguntasFromEncuesta = "select * from preguntas where idencuesta = 1";

                    var query = queryReporteLayoutRespuestas(IdUnidadNegocio, IdBD, new List<int>());
                }


                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    context.Database.CommandTimeout = 300;
                    var query = context.GetByUnidad(IdUnidadNegocio, IdBD).ToList();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.EmpleadoRespuesta EmpleadoRespuesta = new ML.EmpleadoRespuesta();
                            EmpleadoRespuesta.Empleado = new ML.Empleado();
                            EmpleadoRespuesta.Empleado.Perfil = new ML.Perfil();
                            EmpleadoRespuesta.Empleado.EstatusEncuesta = new ML.EstatusEncuesta();
                            EmpleadoRespuesta.Empleado.ClavesAcceso = new ML.ClavesAcceso();
                            EmpleadoRespuesta.Empleado.Departamento = new ML.Departamento();
                            EmpleadoRespuesta.Empleado.Departamento.Area = new ML.Area();
                            EmpleadoRespuesta.Empleado.Departamento.Area.Company = new ML.Company();
                            EmpleadoRespuesta.Empleado.Departamento.Area.Company.CompanyCategoria = new ML.CompanyCategoria();
                            //Relacion Estatus Encuesta  EmpleadoRespuesta.Empleado.
                            EmpleadoRespuesta.Empleado.Subdepartamento = new ML.Subdepartamento();

                            //Fill Model
                            EmpleadoRespuesta.Empleado.IdEmpleado = Convert.ToInt32(item.ID_EMPLEADO);
                            EmpleadoRespuesta.Empleado.ApellidoPaterno = item.A_PATERNO;
                            EmpleadoRespuesta.Empleado.ApellidoMaterno = item.A_MATERNO;
                            EmpleadoRespuesta.Empleado.Nombre = item.NOMBRE_EMPLEADO;
                            EmpleadoRespuesta.Empleado.Puesto = item.PUESTO;
                            EmpleadoRespuesta.Empleado.FechaNaciemiento = Convert.ToDateTime(item.F_NACIM);
                            EmpleadoRespuesta.Empleado.FechaAntiguedad = Convert.ToDateTime(item.F_ANTIG);
                            EmpleadoRespuesta.Empleado.Sexo = item.SEXO;
                            EmpleadoRespuesta.Empleado.Correo = item.EMAIL;
                            EmpleadoRespuesta.Empleado.Perfil.Descripcion = item.TIPO_FUNCION;
                            EmpleadoRespuesta.Empleado.CondicionTrabajo = item.CONDIC_TRAB;
                            EmpleadoRespuesta.Empleado.GradoAcademico = item.GRADO_ACADEM;
                            EmpleadoRespuesta.Empleado.Departamento.Area.Company.CompanyCategoria.Descripcion = item.UNIDAD_NEGOCIO;
                            EmpleadoRespuesta.Empleado.Departamento.Area.Company.CompanyName = item.DIVIS_MARCA;
                            EmpleadoRespuesta.Empleado.Departamento.Area.Nombre = item.AREA_AGENCIA;
                            EmpleadoRespuesta.Empleado.Departamento.Nombre = item.DEPARTAMENTO;
                            EmpleadoRespuesta.Empleado.Subdepartamento.Nombre = item.SUBDEPARTAMENTO;
                            EmpleadoRespuesta.Empleado.EmpresaContratante = item.EMP_CONTRATANTE;
                            EmpleadoRespuesta.Empleado.IdResponsableRH = Convert.ToInt32(item.ID_RESPONS_RH);
                            EmpleadoRespuesta.Empleado.NombreResponsableRH = item.NOM_RESPONS_RH;
                            EmpleadoRespuesta.Empleado.IdJefe = Convert.ToInt32(item.ID_JEFE);
                            EmpleadoRespuesta.Empleado.NombreJefe = item.NOMBRE_JEFE;
                            EmpleadoRespuesta.Empleado.PuestoJefe = item.PUESTO_JEFE;
                            EmpleadoRespuesta.Empleado.IdRespinsableEstructura = Convert.ToInt32(item.ID_RESPONS_EST);
                            EmpleadoRespuesta.Empleado.NombreResponsableEstrucutra = item.NOM_RESP_ESTRUC;
                            EmpleadoRespuesta.Empleado.ClavesAcceso.ClaveAcceso = item.CVE_ACCESO;
                            EmpleadoRespuesta.Empleado.RangoAntiguedad = item.RANGO_ANTIG;
                            EmpleadoRespuesta.Empleado.RangoEdad = item.RANGO_EDAD;
                            //Falta estatusencuesta
                            EmpleadoRespuesta.Empleado.EstatusEncuesta.Estatus = item.STS_ENCUESTA;
                            EmpleadoRespuesta.Empleado.EstatusEmpleado = item.ESTATUS_COLABORADOR;

                            EmpleadoRespuesta.EMP_R1 = item.EMP_R1;
                            EmpleadoRespuesta.EMP_R2 = item.EMP_R2;
                            EmpleadoRespuesta.EMP_R3 = item.EMP_R3;
                            EmpleadoRespuesta.EMP_R4 = item.EMP_R4;
                            EmpleadoRespuesta.EMP_R5 = item.EMP_R5;
                            EmpleadoRespuesta.EMP_R6 = item.EMP_R6;
                            EmpleadoRespuesta.EMP_R7 = item.EMP_R7;
                            EmpleadoRespuesta.EMP_R8 = item.EMP_R8;
                            EmpleadoRespuesta.EMP_R9 = item.EMP_R9;
                            EmpleadoRespuesta.EMP_R10 = item.EMP_R10;

                            EmpleadoRespuesta.EMP_R11 = item.EMP_R11;
                            EmpleadoRespuesta.EMP_R12 = item.EMP_R12;
                            EmpleadoRespuesta.EMP_R13 = item.EMP_R13;
                            EmpleadoRespuesta.EMP_R14 = item.EMP_R14;
                            EmpleadoRespuesta.EMP_R15 = item.EMP_R15;
                            EmpleadoRespuesta.EMP_R16 = item.EMP_R16;
                            EmpleadoRespuesta.EMP_R17 = item.EMP_R17;
                            EmpleadoRespuesta.EMP_R18 = item.EMP_R18;
                            EmpleadoRespuesta.EMP_R19 = item.EMP_R19;
                            EmpleadoRespuesta.EMP_R20 = item.EMP_R20;

                            EmpleadoRespuesta.EMP_R21 = item.EMP_R21;
                            EmpleadoRespuesta.EMP_R22 = item.EMP_R22;
                            EmpleadoRespuesta.EMP_R23 = item.EMP_R23;
                            EmpleadoRespuesta.EMP_R24 = item.EMP_R24;
                            EmpleadoRespuesta.EMP_R25 = item.EMP_R25;
                            EmpleadoRespuesta.EMP_R26 = item.EMP_R26;
                            EmpleadoRespuesta.EMP_R27 = item.EMP_R27;
                            EmpleadoRespuesta.EMP_R28 = item.EMP_R28;
                            EmpleadoRespuesta.EMP_R29 = item.EMP_R29;
                            EmpleadoRespuesta.EMP_R30 = item.EMP_R30;

                            EmpleadoRespuesta.EMP_R31 = item.EMP_R31;
                            EmpleadoRespuesta.EMP_R32 = item.EMP_R32;
                            EmpleadoRespuesta.EMP_R33 = item.EMP_R33;
                            EmpleadoRespuesta.EMP_R34 = item.EMP_R34;
                            EmpleadoRespuesta.EMP_R35 = item.EMP_R35;
                            EmpleadoRespuesta.EMP_R36 = item.EMP_R36;
                            EmpleadoRespuesta.EMP_R37 = item.EMP_R37;
                            EmpleadoRespuesta.EMP_R38 = item.EMP_R38;
                            EmpleadoRespuesta.EMP_R39 = item.EMP_R39;
                            EmpleadoRespuesta.EMP_R40 = item.EMP_R40;

                            EmpleadoRespuesta.EMP_R41 = item.EMP_R41;
                            EmpleadoRespuesta.EMP_R42 = item.EMP_R42;
                            EmpleadoRespuesta.EMP_R43 = item.EMP_R43;
                            EmpleadoRespuesta.EMP_R44 = item.EMP_R44;
                            EmpleadoRespuesta.EMP_R45 = item.EMP_R45;
                            EmpleadoRespuesta.EMP_R46 = item.EMP_R46;
                            EmpleadoRespuesta.EMP_R47 = item.EMP_R47;
                            EmpleadoRespuesta.EMP_R48 = item.EMP_R48;
                            EmpleadoRespuesta.EMP_R49 = item.EMP_R49;
                            EmpleadoRespuesta.EMP_R50 = item.EMP_R50;

                            EmpleadoRespuesta.EMP_R51 = item.EMP_R51;
                            EmpleadoRespuesta.EMP_R52 = item.EMP_R52;
                            EmpleadoRespuesta.EMP_R53 = item.EMP_R53;
                            EmpleadoRespuesta.EMP_R54 = item.EMP_R54;
                            EmpleadoRespuesta.EMP_R55 = item.EMP_R55;
                            EmpleadoRespuesta.EMP_R56 = item.EMP_R56;
                            EmpleadoRespuesta.EMP_R57 = item.EMP_R57;
                            EmpleadoRespuesta.EMP_R58 = item.EMP_R58;
                            EmpleadoRespuesta.EMP_R59 = item.EMP_R59;
                            EmpleadoRespuesta.EMP_R60 = item.EMP_R60;

                            EmpleadoRespuesta.EMP_R61 = item.EMP_R61;
                            EmpleadoRespuesta.EMP_R62 = item.EMP_R62;
                            EmpleadoRespuesta.EMP_R63 = item.EMP_R63;
                            EmpleadoRespuesta.EMP_R64 = item.EMP_R64;
                            EmpleadoRespuesta.EMP_R65 = item.EMP_R65;
                            EmpleadoRespuesta.EMP_R66 = item.EMP_R66;
                            EmpleadoRespuesta.EMP_R67 = item.EMP_R67;
                            EmpleadoRespuesta.EMP_R68 = item.EMP_R68;
                            EmpleadoRespuesta.EMP_R69 = item.EMP_R69;
                            EmpleadoRespuesta.EMP_R70 = item.EMP_R70;

                            EmpleadoRespuesta.EMP_R71 = item.EMP_R71;
                            EmpleadoRespuesta.EMP_R72 = item.EMP_R72;
                            EmpleadoRespuesta.EMP_R73 = item.EMP_R73;
                            EmpleadoRespuesta.EMP_R74 = item.EMP_R74;
                            EmpleadoRespuesta.EMP_R75 = item.EMP_R75;
                            EmpleadoRespuesta.EMP_R76 = item.EMP_R76;
                            EmpleadoRespuesta.EMP_R77 = item.EMP_R77;
                            EmpleadoRespuesta.EMP_R78 = item.EMP_R78;
                            EmpleadoRespuesta.EMP_R79 = item.EMP_R79;
                            EmpleadoRespuesta.EMP_R80 = item.EMP_R80;

                            EmpleadoRespuesta.EMP_R81 = item.EMP_R81;
                            EmpleadoRespuesta.EMP_R82 = item.EMP_R82;
                            EmpleadoRespuesta.EMP_R83 = item.EMP_R83;
                            EmpleadoRespuesta.EMP_R84 = item.EMP_R84;
                            EmpleadoRespuesta.EMP_R85 = item.EMP_R85;
                            EmpleadoRespuesta.EMP_R86 = item.EMP_R86;

                            //Mapping Enfoque Area
                            EmpleadoRespuesta.ARE_1 = item.ARE_1;
                            EmpleadoRespuesta.ARE_2 = item.ARE_2;
                            EmpleadoRespuesta.ARE_3 = item.ARE_3;
                            EmpleadoRespuesta.ARE_4 = item.ARE_4;
                            EmpleadoRespuesta.ARE_5 = item.ARE_5;
                            EmpleadoRespuesta.ARE_6 = item.ARE_6;
                            EmpleadoRespuesta.ARE_7 = item.ARE_7;
                            EmpleadoRespuesta.ARE_8 = item.ARE_8;
                            EmpleadoRespuesta.ARE_9 = item.ARE_9;
                            EmpleadoRespuesta.ARE_10 = item.ARE_10;

                            EmpleadoRespuesta.ARE_11 = item.ARE_11;
                            EmpleadoRespuesta.ARE_12 = item.ARE_12;
                            EmpleadoRespuesta.ARE_13 = item.ARE_13;
                            EmpleadoRespuesta.ARE_14 = item.ARE_14;
                            EmpleadoRespuesta.ARE_15 = item.ARE_15;
                            EmpleadoRespuesta.ARE_16 = item.ARE_16;
                            EmpleadoRespuesta.ARE_17 = item.ARE_17;
                            EmpleadoRespuesta.ARE_18 = item.ARE_18;
                            EmpleadoRespuesta.ARE_19 = item.ARE_19;
                            EmpleadoRespuesta.ARE_20 = item.ARE_20;

                            EmpleadoRespuesta.ARE_21 = item.ARE_21;
                            EmpleadoRespuesta.ARE_22 = item.ARE_22;
                            EmpleadoRespuesta.ARE_23 = item.ARE_23;
                            EmpleadoRespuesta.ARE_24 = item.ARE_24;
                            EmpleadoRespuesta.ARE_25 = item.ARE_25;
                            EmpleadoRespuesta.ARE_26 = item.ARE_26;
                            EmpleadoRespuesta.ARE_27 = item.ARE_27;
                            EmpleadoRespuesta.ARE_28 = item.ARE_28;
                            EmpleadoRespuesta.ARE_29 = item.ARE_29;
                            EmpleadoRespuesta.ARE_30 = item.ARE_30;

                            EmpleadoRespuesta.ARE_31 = item.ARE_31;
                            EmpleadoRespuesta.ARE_32 = item.ARE_32;
                            EmpleadoRespuesta.ARE_33 = item.ARE_33;
                            EmpleadoRespuesta.ARE_34 = item.ARE_34;
                            EmpleadoRespuesta.ARE_35 = item.ARE_35;
                            EmpleadoRespuesta.ARE_36 = item.ARE_36;
                            EmpleadoRespuesta.ARE_37 = item.ARE_37;
                            EmpleadoRespuesta.ARE_38 = item.ARE_38;
                            EmpleadoRespuesta.ARE_39 = item.ARE_39;
                            EmpleadoRespuesta.ARE_40 = item.ARE_40;

                            EmpleadoRespuesta.ARE_41 = item.ARE_41;
                            EmpleadoRespuesta.ARE_42 = item.ARE_42;
                            EmpleadoRespuesta.ARE_43 = item.ARE_43;
                            EmpleadoRespuesta.ARE_44 = item.ARE_44;
                            EmpleadoRespuesta.ARE_45 = item.ARE_45;
                            EmpleadoRespuesta.ARE_46 = item.ARE_46;
                            EmpleadoRespuesta.ARE_47 = item.ARE_47;
                            EmpleadoRespuesta.ARE_48 = item.ARE_48;
                            EmpleadoRespuesta.ARE_49 = item.ARE_49;
                            EmpleadoRespuesta.ARE_50 = item.ARE_50;

                            EmpleadoRespuesta.ARE_51 = item.ARE_51;
                            EmpleadoRespuesta.ARE_52 = item.ARE_52;
                            EmpleadoRespuesta.ARE_53 = item.ARE_53;
                            EmpleadoRespuesta.ARE_54 = item.ARE_54;
                            EmpleadoRespuesta.ARE_55 = item.ARE_55;
                            EmpleadoRespuesta.ARE_56 = item.ARE_56;
                            EmpleadoRespuesta.ARE_57 = item.ARE_57;
                            EmpleadoRespuesta.ARE_58 = item.ARE_58;
                            EmpleadoRespuesta.ARE_59 = item.ARE_59;
                            EmpleadoRespuesta.ARE_60 = item.ARE_60;

                            EmpleadoRespuesta.ARE_61 = item.ARE_61;
                            EmpleadoRespuesta.ARE_62 = item.ARE_62;
                            EmpleadoRespuesta.ARE_63 = item.ARE_63;
                            EmpleadoRespuesta.ARE_64 = item.ARE_64;
                            EmpleadoRespuesta.ARE_65 = item.ARE_65;
                            EmpleadoRespuesta.ARE_66 = item.ARE_66;
                            EmpleadoRespuesta.ARE_67 = item.ARE_67;
                            EmpleadoRespuesta.ARE_68 = item.ARE_68;
                            EmpleadoRespuesta.ARE_69 = item.ARE_69;
                            EmpleadoRespuesta.ARE_70 = item.ARE_70;

                            EmpleadoRespuesta.ARE_71 = item.ARE_71;
                            EmpleadoRespuesta.ARE_72 = item.ARE_72;
                            EmpleadoRespuesta.ARE_73 = item.ARE_73;
                            EmpleadoRespuesta.ARE_74 = item.ARE_74;
                            EmpleadoRespuesta.ARE_75 = item.ARE_75;
                            EmpleadoRespuesta.ARE_76 = item.ARE_76;
                            EmpleadoRespuesta.ARE_77 = item.ARE_77;
                            EmpleadoRespuesta.ARE_78 = item.ARE_78;
                            EmpleadoRespuesta.ARE_79 = item.ARE_79;
                            EmpleadoRespuesta.ARE_80 = item.ARE_80;

                            EmpleadoRespuesta.ARE_81 = item.ARE_81;
                            EmpleadoRespuesta.ARE_82 = item.ARE_82;
                            EmpleadoRespuesta.ARE_83 = item.ARE_83;
                            EmpleadoRespuesta.ARE_84 = item.ARE_84;
                            EmpleadoRespuesta.ARE_85 = item.ARE_85;
                            EmpleadoRespuesta.ARE_86 = item.ARE_86;

                            //Mapping Open
                            EmpleadoRespuesta.ESPECIAL_OPEN = item.ESPECIAL_OPEN;
                            EmpleadoRespuesta.CAMBIAR_OPEN = item.CAMBIAR_OPEN;
                            EmpleadoRespuesta.FORT_JEFE = item.FORT_JEFE;
                            EmpleadoRespuesta.OPORT_JEFE = item.OPORT_JEFE;
                            EmpleadoRespuesta.MOTIVA_TRAB = item.MOTIVA_TRAB;
                            EmpleadoRespuesta.DEJAR_EMP = item.DEJAR_EMP;
                            EmpleadoRespuesta.PRESION = item.PRESION;
                            EmpleadoRespuesta.ANTIGUEDAD = item.ANTIGÜEDADR;
                            EmpleadoRespuesta.RANGO_EDAD = item.RANGO_EDADR;
                            EmpleadoRespuesta.CONDICION = item.CONDICIONR;
                            EmpleadoRespuesta.SEXO = item.SEXOR;
                            EmpleadoRespuesta.ACADEMICO = item.ACADEMICOR;
                            EmpleadoRespuesta.FUNCION = item.FUNCIONR;
                            EmpleadoRespuesta.UNIDAD = item.UNIDADR;
                            EmpleadoRespuesta.DIRECION = item.DIRECCIONR;
                            EmpleadoRespuesta.AREA = item.AREAR;
                            EmpleadoRespuesta.DEPARTAMENTO = item.DEPARTAMENTOR;

                            result.Objects.Add(EmpleadoRespuesta);
                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.ErrorMessage = "No se encontraron registros";
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
                result.Correct = false;
            }
            return result;
        }

        public static string queryReporteLayoutRespuestas(string Entidad, int BD, List<int> listPreguntas)
        {
            return string.Empty;
        }
    }
}
