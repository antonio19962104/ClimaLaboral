using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BL
{
    public class Reporte
    {
        public static ML.Result GetAllResultados()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {

                    //context.Database.Connection.Open();
                    context.Database.CommandTimeout = 240;
                    var query = context.GetAllResultOK().ToList();
                    result.Objects = new List<object>();
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
                        result.ErrorMessage = "No se obtuvieron registros";
                    }
                    context.Database.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetResultadosByIdEmpleado(int IdEmpleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.GetByIdEmpleadoResultOK(IdEmpleado).ToList();
                    result.Objects = new List<object>();
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
                        result.ErrorMessage = "No se obtuvieron registros";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result GetResultadosByUnidadNegocio(string IdUnidadNegocio, int IdBD)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            // if (IdUnidadNegocio == "Sin Unidad de Negocio")
            try
            {
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
        public static ML.Result GetResultadosByUnidadNegocio(List<string> listUnidadNeg, int IdBD)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            for (int i = (listUnidadNeg.Count - 1); i < 20; i++)
                listUnidadNeg.Add("");
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    context.Database.CommandTimeout = 300;
                    var query = context.GetByMultiUnidad(listUnidadNeg[0], listUnidadNeg[1], listUnidadNeg[2], listUnidadNeg[3], listUnidadNeg[4], 
                                                         listUnidadNeg[5] ,listUnidadNeg[6], listUnidadNeg[7], listUnidadNeg[8], listUnidadNeg[9], 
                                                         listUnidadNeg[10], listUnidadNeg[11], listUnidadNeg[12], listUnidadNeg[13], listUnidadNeg[14], IdBD).ToList();

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

        public static ML.Result GetResultadosByCompany(string Company)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.GetByCompanyOK(Company).ToList();
                    result.Objects = new List<object>();

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

        public static ML.Result GetResultadosByArea(string Company)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.GetByAreaOK(Company).ToList();
                    result.Objects = new List<object>();

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

        public static ML.Result GetResultadosByDepartamento(string dpto)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.GetByDepartamentoOK(dpto).ToList();
                    result.Objects = new List<object>();

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

        public static ML.Result Add(int IdEncuesta, string NombreEncuesta)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {

                    var EXIST = context.EncuestaReporte.SqlQuery("SELECT * FROM EncuestaReporte WHERE IDENCUESTA = {0}", IdEncuesta).ToList();

                    if (EXIST.Count > 1)
                    {

                    }
                    else
                    {
                        var query = context.Database.ExecuteSqlCommand("INSERT INTO REPORTE (NOMBRE, DESCRIPCION, LOCATION) VALUES ({0}, {1}, {2})", "Respuestas de la encuesta: " + NombreEncuesta, "Listado de las respuestas de la encuesta", "/Encuesta/ViewReporte?IdEncuesta=" + IdEncuesta);// Encuesta / ViewReporte ? IdEncuesta = 19

                        int idreporte = context.Reporte.Max(p => p.IdReporte);

                        var query_2 = context.Database.ExecuteSqlCommand("INSERT INTO ENCUESTAREPORTE (IDENCUESTA, IDREPORTE) VALUES ({0}, {1})", IdEncuesta, idreporte);

                        context.SaveChanges();
                    }

                    
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetRespuestas(int IdEncuesta)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            DataSet dataset = new DataSet();
            dataset.Tables.Add("data", "set");
            result.DataSet = dataset;
            int TipoEncuesta = 0;
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    context.Database.CommandTimeout = 240;
                    var getTipo = context.Encuesta.SqlQuery("SELECT * FROM ENCUESTA WHERE IDENCUESTA = {0}", IdEncuesta);
                    if (getTipo != null)
                    {
                        foreach (var item in getTipo)
                        {
                            ML.Encuesta encuesta = new ML.Encuesta();
                            encuesta.MLTipoEncuesta = new ML.TipoEncuesta();
                            encuesta.MLTipoEncuesta.IdTipoEncuesta = Convert.ToInt32(item.IdTipoEncuesta);

                            result.Object = encuesta.MLTipoEncuesta.IdTipoEncuesta;
                            result.idTipoEncuesta = Convert.ToInt32(encuesta.MLTipoEncuesta.IdTipoEncuesta);
                        }
                    }

                    //GetData
                    //si tipoencuesta es anonimo o confidencial no usa join a usuario
                    //Si es generica usa el join
                    int tipoEncuesta = Convert.ToInt32(result.Object);

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        //Reporte D4U
        public static ML.UsuarioEstatusEncuesta GetParticipacion(int idEncuesta)
        {
            ML.Result result = new ML.Result();
            ML.UsuarioEstatusEncuesta usrEstatus = new ML.UsuarioEstatusEncuesta();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    //SELECT * FROM UsuarioEstatusEncuesta WHERE IdEncuesta = 176 AND IdEstatusEncuestaD4U = 3
                    var getNoIniciada = context.UsuarioEstatusEncuesta.SqlQuery("SELECT * FROM UsuarioEstatusEncuesta INNER JOIN USUARIO ON USUARIOESTATUSENCUESTA.IDUSUARIO = USUARIO.IDUSUARIO WHERE IdEncuesta = {0} AND IdEstatusEncuestaD4U = 1 AND USUARIO.IDESTATUS = 1", idEncuesta).ToList();
                    var getIniciadas = context.UsuarioEstatusEncuesta.SqlQuery("SELECT * FROM USUARIOESTATUSENCUESTA INNER JOIN USUARIO ON USUARIOESTATUSENCUESTA.IDUSUARIO = USUARIO.IDUSUARIO WHERE IDENCUESTA = {0} AND IDESTATUSENCUESTAD4U = 2 AND USUARIO.IDESTATUS = 1", idEncuesta).ToList();
                    var getTerminadas = context.UsuarioEstatusEncuesta.SqlQuery("SELECT * FROM USUARIOESTATUSENCUESTA INNER JOIN USUARIO ON USUARIOESTATUSENCUESTA.IDUSUARIO = USUARIO.IDUSUARIO WHERE IDENCUESTA = {0} AND IDESTATUSENCUESTAD4U = 3  AND USUARIO.IDESTATUS = 1", idEncuesta).ToList();

                    usrEstatus.NoIniciadas = getNoIniciada.Count;
                    usrEstatus.Iniciadas = getIniciadas.Count;
                    usrEstatus.Terminadas = getTerminadas.Count;

                    usrEstatus.Esperadas = usrEstatus.NoIniciadas + usrEstatus.Iniciadas + usrEstatus.Terminadas;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return usrEstatus;
        }
        public static ML.Result GetPreguntasByIdEncuesta(int IdEncuesta)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Preguntas.SqlQuery("SELECT * FROM PREGUNTAS INNER JOIN TIPOCONTROL ON PREGUNTAS.IDTIPOCONTROL = TIPOCONTROL.IDTIPOCONTROL WHERE PREGUNTAS.IDENCUESTA = {0} ORDER BY PREGUNTAS.IDPREGUNTA", IdEncuesta).ToList();

                    foreach (var item in query)
                    {
                        if (item.IdTipoControl != 12)
                        {
                            ML.Preguntas pregunta = new ML.Preguntas();
                            pregunta.IdPregunta = item.IdPregunta;
                            pregunta.Pregunta = item.Pregunta;
                            pregunta.TipoControl = new ML.TipoControl();
                            pregunta.TipoControl.IdTipoControl = item.IdTipoControl;
                            pregunta.TipoControl.Nombre = item.TipoControl1.Nombre;

                            result.Objects.Add(pregunta);
                            result.Correct = true;
                        }
                        else
                        {
                            //TipoLikertDoble => Consultar en Preguntas likert con el idPregunta que traigo 
                            ML.Preguntas pregunta = new ML.Preguntas();
                            pregunta.IdPregunta = item.IdPregunta;//Con este busco
                            pregunta.Pregunta = item.Pregunta;//Instruccion
                            pregunta.TipoControl = new ML.TipoControl();
                            pregunta.TipoControl.IdTipoControl = item.IdTipoControl;
                            pregunta.TipoControl.Nombre = item.TipoControl1.Nombre;
                            pregunta.ListPreguntasLikert = new List<ML.PreguntasLikert>();

                            var buscaPregLikert = context.PreguntasLikert.SqlQuery("SELECT * FROM PREGUNTASLIKERT WHERE IDPREGUNTA = {0} AND IDENCUESTA = {1}", pregunta.IdPregunta, IdEncuesta).ToList();
                            if (buscaPregLikert != null)
                            {
                                foreach (var itemLik in buscaPregLikert)
                                {
                                    ML.PreguntasLikert pregLik = new ML.PreguntasLikert();
                                    pregLik.IdPreguntaLikert = itemLik.idPreguntasLikert;
                                    pregLik.PreguntaLikert = itemLik.Pregunta;

                                    pregunta.ListPreguntasLikert.Add(pregLik);
                                }
                            }
                            result.Objects.Add(pregunta);
                            result.Correct = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result GetRespuestasByIdPregunta(ML.Preguntas pregunta)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            result.ObjectsAux = new List<object>();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    if (pregunta.IdentificadorTipoControl != 12)
                    {
                        if (pregunta.IdentificadorTipoControl == 1 || pregunta.IdentificadorTipoControl == 2)//Abiertas
                        {
                            var queryOpens = context.UsuarioRespuestas.SqlQuery("SELECT * FROM USUARIORESPUESTAS inner join usuarioestatusencuesta on usuariorespuestas.idusuario = usuarioestatusencuesta.idusuario INNER JOIN PREGUNTAS ON USUARIORESPUESTAS.IDPREGUNTA = PREGUNTAS.IDPREGUNTA INNER JOIN USUARIO ON USUARIORESPUESTAS.IDUSUARIO = USUARIO.IDUSUARIO  WHERE USUARIORESPUESTAS.IDPREGUNTA = {0} AND USUARIORESPUESTAS.IDESTATUS = 1 AND USUARIO.IDESTATUS = 1 and usuarioestatusencuesta.idestatusencuestad4u = 3 and UsuarioEstatusEncuesta.IdEncuesta = {1}", pregunta.IdPregunta, pregunta.IdEncuesta);
                            foreach (var item in queryOpens)
                            {
                                ML.UsuarioRespuestas usuarioRespuestas = new ML.UsuarioRespuestas();
                                usuarioRespuestas.Preguntas = new ML.Preguntas();
                                usuarioRespuestas.Preguntas.IdPregunta = Convert.ToInt32(item.IdPregunta);
                                usuarioRespuestas.Preguntas.Pregunta = item.Preguntas.Pregunta;
                                usuarioRespuestas.RespuestaUsuario = item.RespuestaUsuario;

                                result.Objects.Add(usuarioRespuestas);
                            }
                            return result;
                        }

                        if (pregunta.IdentificadorTipoControl == 3 || pregunta.IdentificadorTipoControl == 4 || pregunta.IdentificadorTipoControl == 5)//Radio y check
                        {
                            if (pregunta.IdPregunta == 4007)
                            {
                                Console.Write("");
                            }
                            var query = context.Respuestas.SqlQuery("SELECT * FROM RESPUESTAS INNER JOIN PREGUNTAS ON RESPUESTAS.IDPREGUNTA = PREGUNTAS.IDPREGUNTA WHERE RESPUESTAS.IDPREGUNTA = {0} AND PREGUNTAS.IDENCUESTA = {1}", pregunta.IdPregunta, pregunta.IdEncuesta).ToList();
                            if (query != null)
                            {
                                foreach (var item in query)
                                {
                                    ML.Respuestas respuestas = new ML.Respuestas();
                                    respuestas.Pregunta = new ML.Preguntas();
                                    respuestas.IdRespuesta = item.IdRespuesta;
                                    respuestas.Respuesta = item.Respuesta;
                                    respuestas.Pregunta.IdPregunta = item.Preguntas.IdPregunta;
                                    respuestas.Pregunta.Pregunta = item.Preguntas.Pregunta;
                                    respuestas.Pregunta.TipoControl = new ML.TipoControl();
                                    respuestas.Pregunta.TipoControl.IdTipoControl = item.Preguntas.IdTipoControl;
                                    var getConteo = context.UsuarioRespuestas.SqlQuery("SELECT * FROM USUARIORESPUESTAS inner join usuarioestatusencuesta on usuariorespuestas.idusuario = usuarioestatusencuesta.idusuario  INNER JOIN USUARIO ON USUARIORESPUESTAS.IDUSUARIO = USUARIO.IDUSUARIO  WHERE USUARIORESPUESTAS.IDENCUESTA = {0} AND USUARIORESPUESTAS.IDRESPUESTA = {1} AND USUARIORESPUESTAS.IDESTATUS = 1 AND USUARIO.IDESTATUS = 1 and usuarioestatusencuesta.idestatusencuestad4u = 3 and UsuarioEstatusEncuesta.IdEncuesta = {0}", pregunta.IdEncuesta, respuestas.IdRespuesta).ToList();
                                    List<int> idusr = new List<int>();
                                    foreach (var obj in getConteo)
                                    {
                                        if (obj.IdUsuario == 31483 && obj.IdUsuarioRespuestas == 58439)
                                        {
                                            var s = 0;
                                        }
                                        if (respuestas.IdRespuesta == 0)
                                        {
                                            var d = 0;
                                        }
                                        int id = (int)obj.IdUsuario;
                                        idusr.Add(id);
                                    }
                                    if (respuestas.Pregunta.TipoControl.IdTipoControl == 3)
                                    {
                                        foreach (var obj in getConteo)
                                        {
                                            if (obj.Selected == true)
                                            {
                                                respuestas.conteoByPregunta = getConteo.Count;
                                                result.Objects.Add(respuestas);
                                                result.ObjectsAux.Add(idusr);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        respuestas.conteoByPregunta = getConteo.Count;
                                        result.Objects.Add(respuestas);
                                        result.ObjectsAux.Add(idusr);
                                    }

                                    
                                    result.Correct = true;
                                }
                            }
                        }

                        //if (pregunta.IdentificadorTipoControl == 5)//DDL
                        //{

                        //}

                        if (pregunta.IdentificadorTipoControl > 5)//Rango
                        {
                            var getRangosNumericos = context.UsuarioRespuestas.SqlQuery("SELECT * FROM USUARIORESPUESTAS inner join usuarioestatusencuesta on usuariorespuestas.idusuario = usuarioestatusencuesta.idusuario INNER JOIN USUARIO ON USUARIORESPUESTAS.IDUSUARIO = USUARIO.IDUSUARIO INNER JOIN Preguntas ON UsuarioRespuestas.IdPregunta = Preguntas.IdPregunta WHERE USUARIORESPUESTAS.IDENCUESTA = {0} AND USUARIORESPUESTAS.IDPREGUNTA = {1} AND USUARIORESPUESTAS.IDESTATUS = 1 and usuario.idestatus = 1 and usuarioestatusencuesta.idestatusencuestad4u = 3 and UsuarioEstatusEncuesta.IdEncuesta = {0}", pregunta.IdEncuesta, pregunta.IdPregunta).ToList();
                            if (getRangosNumericos != null)
                            {
                                foreach (var item in getRangosNumericos)
                                {
                                    ML.UsuarioRespuestas resp = new ML.UsuarioRespuestas();
                                    resp.Preguntas = new ML.Preguntas();
                                    resp.RespuestaUsuario = item.RespuestaUsuario;
                                    resp.Preguntas.IdPregunta = item.Preguntas.IdPregunta;
                                    resp.Preguntas.Pregunta = item.Preguntas.Pregunta;
                                    result.Objects.Add(resp);
                                    result.Correct = true;
                                }
                            }
                        }
                    }
                    //else
                    //{
                    //    //Tipo likert doble => Get NombreColumnas
                    //    var query = context.Respuestas.SqlQuery("SELECT * FROM RESPUESTAS INNER JOIN PREGUNTAS ON RESPUESTAS.IDPREGUNTA = PREGUNTAS.IDPREGUNTA WHERE RESPUESTAS.IDPREGUNTA = {0} AND PREGUNTAS.IDENCUESTA = {1}", pregunta.IdPregunta, pregunta.IdEncuesta).ToList();
                    //    if (query != null)
                    //    {
                    //        foreach (var item in query)
                    //        {
                    //            ML.Respuestas resp = new ML.Respuestas();
                    //            resp.Respuesta = item.Respuesta;//Columna A & B
                    //            resp.Pregunta = new ML.Preguntas();
                    //            resp.Pregunta.Pregunta = item.Preguntas.Pregunta;//Instrucciones LikertD
                    //            resp.listPreguntasLikert = new List<ML.PreguntasLikert>();

                    //            var getPregLikert = context.PreguntasLikert.SqlQuery("SELECT * FROM PREGUNTASLIKERT WHERE IDENCUESTA = {0} AND IDPREGUNTA = {1}", pregunta.IdEncuesta, pregunta.IdPregunta).ToList();
                    //            if (getPregLikert != null)
                    //            {
                    //                foreach (var itemPreglik in getPregLikert)
                    //                {
                    //                    ML.PreguntasLikert pregLikert = new ML.PreguntasLikert();
                    //                    pregLikert.IdPreguntaLikert = Convert.ToInt32(itemPreglik.idPregunta);
                    //                    pregLikert.PreguntaLikert = itemPreglik.Pregunta;

                    //                    resp.listPreguntasLikert.Add(pregLikert);
                    //                }
                    //            }
                    //            result.Objects.Add(resp);
                    //            result.Correct = true;
                    //        }
                    //    }
                    //    result.Correct = true;
                    //}
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static int getIndexListIdPregunta(int caso)
        {
            int index = 0;
            switch (caso)
            {
                case 36: index = 0; break;
                case 37: index = 1; break;
                case 38: index = 2; break;
                case 39: index = 3; break;
                case 40: index = 4; break;
                case 41: index = 5; break;
                case 42: index = 6; break;
                case 43: index = 7; break;
                case 44: index = 8; break;
                case 45: index = 9; break;
                case 46: index = 10; break;
                case 47: index = 11; break;
                case 48: index = 12; break;
                case 49: index = 13; break;
                case 50: index = 14; break;
                case 51: index = 15; break;
                case 52: index = 16; break;
                case 53: index = 17; break;
                case 54: index = 18; break;
                case 55: index = 19; break;
                case 56: index = 20; break;
                case 57: index = 21; break;
                case 58: index = 22; break;
                case 59: index = 23; break;
                case 60: index = 24; break;
                case 61: index = 25; break;
                case 62: index = 26; break;
                case 63: index = 27; break;
                case 64: index = 28; break;
                case 65: index = 29; break;
                case 66: index = 30; break;
                case 67: index = 31; break;
                case 68: index = 32; break;
                case 69: index = 33; break;
                case 70: index = 34; break;
                case 71: index = 35; break;
                case 72: index = 36; break;
                case 73: index = 37; break;
                case 74: index = 38; break;
                case 75: index = 39; break;
                case 76: index = 40; break;
                case 77: index = 41; break;
                case 78: index = 42; break;
                case 79: index = 43; break;
                case 80: index = 44; break;
                case 81: index = 45; break;
                case 82: index = 46; break;
                case 83: index = 47; break;

                case 84: index = 48; break;
                case 85: index = 49; break;
                case 86: index = 50; break;
                case 87: index = 51; break;
                case 88: index = 52; break;
                case 89: index = 53; break;
                case 90: index = 54; break;
                case 91: index = 55; break;
                case 92: index = 56; break;
                case 93: index = 57; break;
                case 94: index = 58; break;
                case 95: index = 59; break;
                case 96: index = 60; break;
                case 97: index = 61; break;
                case 98: index = 62; break;
                case 99: index = 63; break;
                case 100: index = 64; break;
                case 101: index = 65; break;
                case 102: index = 66; break;
                case 103: index = 67; break;
                case 104: index = 68; break;
                case 105: index = 69; break;
                case 106: index = 70; break;
            }
            return index;
        }

        public static ML.Result GetPreguntasLikertExceptDobleByEncuesta(int IdEncuesta)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            result.ObjectsAux = new List<object>();
            var csf = 0;
            var ff = 0;
            var av = 0;
            var fv = 0;
            var csv = 0;
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    //Get Preguntas tipo Likert
                    var query = context.Preguntas.SqlQuery("SELECT * FROM Preguntas WHERE IdTipoControl BETWEEN 8 AND 13 AND IDENCUESTA = {0}", IdEncuesta).ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Preguntas preguntas = new ML.Preguntas();
                            preguntas.TipoControl = new ML.TipoControl();
                            preguntas.listUsuarioResp = new List<ML.UsuarioRespuestas>();
                            preguntas.IdPregunta = item.IdPregunta;
                            preguntas.Pregunta = item.Pregunta;
                            preguntas.TipoControl.IdTipoControl = item.IdTipoControl;
                            //Validar ya que puede caer nulo
                            preguntas.SubSeccion = item.SubSeccion == null ? 0 : (int) item.SubSeccion;

                            if (preguntas.TipoControl.IdTipoControl != 12)
                            {
                                var frec = context.UsuarioRespuestas.SqlQuery("SELECT * FROM USUARIORESPUESTAS inner join usuarioestatusencuesta on usuariorespuestas.idusuario = usuarioestatusencuesta.idusuario inner join usuario on USUARIORESPUESTAS.idusuario = usuario.idusuario WHERE usuariorespuestas.IDPREGUNTA = {0} AND USUARIORESPUESTAS.IDESTATUS = 1 and usuario.idestatus = 1 and usuarioestatusencuesta.idestatusencuestad4u = 3 and UsuarioRespuestas.IdEncuesta = {1} and UsuarioEstatusEncuesta.IdEncuesta = {1}", preguntas.IdPregunta, IdEncuesta).ToList();
                                if (frec != null)
                                {
                                    foreach (var items in frec)
                                    {
                                        ML.UsuarioRespuestas userResp = new ML.UsuarioRespuestas();
                                        //userResp.Respuestas = new ML.Respuestas();
                                        userResp.RespuestaUsuario = items.RespuestaUsuario;

                                        //userResp.Respuestas.Respuesta = items.Respuestas == null ? "" : items.Respuestas.Respuesta;//ColA & ColB
                                        //                                                           //getIdDe pregunta Likert
                                        //userResp.Respuestas.PreguntasLikert = new ML.PreguntasLikert();
                                        //userResp.Respuestas.PreguntasLikert.IdPreguntaLikert = items.Respuestas == null ? 0 : Convert.ToInt32(items.Respuestas.IdPreguntaLikertD);

                                        //preguntas.listUsuarioResp.Add(userResp);
                                        //result.Correct = true;
                                        switch (userResp.RespuestaUsuario)
                                        {
                                            case "1":
                                                csf = csf + 1;
                                                break;
                                            case "2":
                                                ff = ff + 1;
                                                break;
                                            case "3":
                                                av = av + 1;
                                                break;
                                            case "4":
                                                fv = fv + 1;
                                                break;
                                            case "5":
                                                csv = csv + 1;
                                                break;
                                            default:
                                                Console.Write("" + preguntas.IdPregunta);
                                                break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                var frec = context.UsuarioRespuestas.SqlQuery("SELECT * FROM USUARIORESPUESTAS inner join usuarioestatusencuesta on usuariorespuestas.idusuario = usuarioestatusencuesta.idusuario inner join usuario on USUARIORESPUESTAS.idusuario = usuario.idusuario inner join Respuestas on UsuarioRespuestas.IdRespuesta = Respuestas.IdRespuesta inner join PreguntasLikert on Respuestas.IdPreguntaLikertD = PreguntasLikert.idPreguntasLikert WHERE usuariorespuestas.IDPREGUNTA = {0} AND USUARIORESPUESTAS.IDESTATUS = 1 and usuario.idestatus = 1 and usuarioestatusencuesta.idestatusencuestad4u = 3 and UsuarioRespuestas.IdEncuesta = {1} and UsuarioEstatusEncuesta.IdEncuesta = {1} order by Respuestas.IdPreguntaLikertD", preguntas.IdPregunta, IdEncuesta).ToList();
                                if (frec != null)
                                {
                                    foreach (var items in frec)
                                    {
                                        ML.UsuarioRespuestas userResp = new ML.UsuarioRespuestas();
                                        userResp.Respuestas = new ML.Respuestas();
                                        userResp.RespuestaUsuario = items.RespuestaUsuario;
                                        userResp.Respuestas.Respuesta = items.Respuestas.Respuesta;//ColA & ColB
                                                                                                   //getIdDe pregunta Likert
                                        userResp.Respuestas.PreguntasLikert = new ML.PreguntasLikert();
                                        userResp.Respuestas.PreguntasLikert.IdPreguntaLikert = Convert.ToInt32(items.Respuestas.IdPreguntaLikertD);

                                        preguntas.listUsuarioResp.Add(userResp);
                                        result.Correct = true;
                                    }
                                }
                            }

                            
                            var GetPregLikert = context.PreguntasLikert.SqlQuery("SELECT * FROM PREGUNTASLIKERT WHERE IDPREGUNTA = {0}", preguntas.IdPregunta).ToList();
                            preguntas.ListPreguntasLikert = new List<ML.PreguntasLikert>();
                            if (GetPregLikert != null)
                            {
                                foreach (var itemPregLikert in GetPregLikert)
                                {
                                    ML.PreguntasLikert preguntasLikert = new ML.PreguntasLikert();
                                    preguntasLikert.IdPreguntaLikert = Convert.ToInt32(itemPregLikert.idPreguntasLikert);
                                    preguntasLikert.PreguntaLikert = itemPregLikert.Pregunta;

                                    preguntas.ListPreguntasLikert.Add(preguntasLikert);
                                }
                            }
                            preguntas.CSF = csf;
                            preguntas.FF = ff;
                            preguntas.AV = av;
                            preguntas.FV = fv;
                            preguntas.CSV = csv;
                            result.Objects.Add(preguntas);
                            result.Correct = true;
                            csf = 0;
                            ff = 0;
                            av = 0;
                            fv = 0;
                            csv = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
                return result;
        }

        public static ML.Result GetRespuestasFromPregLikert(int IdPregunta)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.UsuarioRespuestas.SqlQuery("select * from UsuarioRespuestas where IdPregunta = {0}", IdPregunta).ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.UsuarioRespuestas usuarioRes = new ML.UsuarioRespuestas();
                            usuarioRes.Preguntas = new ML.Preguntas();
                            usuarioRes.Preguntas.IdPregunta = Convert.ToInt32(item.IdPregunta);
                            usuarioRes.RespuestaUsuario = item.RespuestaUsuario;

                            result.Objects.Add(usuarioRes);
                            result.Correct = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        //CreateReporte
        public static ML.Result CreateReporte(ML.AuxReporte reporte)
        {
            ML.Result result = new ML.Result();
            string urlReporteGrafico = "/ReporteD4U/Reporte?IdEncuesta=" + reporte.IdEncuesta;
            string urlReporteTabla = "/Encuesta/ViewReporte?IdEncuesta=" + reporte.IdEncuesta;
            string urlReporte = "";
            bool existe = false;
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    //validar existencia
                    if (reporte.TipoReporte == "Grafico")
                    {
                        urlReporte = urlReporteGrafico;
                        var existQuery = context.Reporte.SqlQuery("SELECT * FROM REPORTE WHERE LOCATION = {0}", urlReporteGrafico).ToList();
                        if (existQuery.Count() > 0) { existe = true; }
                    }
                    if (reporte.TipoReporte == "Tabla")
                    {
                        urlReporte = urlReporteTabla;
                        var existQuery = context.Reporte.SqlQuery("SELECT * FROM REPORTE WHERE LOCATION = {0}", urlReporteTabla).ToList();
                        if (existQuery.Count() > 0) { existe = true; }
                    }
                    /***************************************/
                    if (existe == false)
                    {
                        var query = context.Database.ExecuteSqlCommand("INSERT INTO REPORTE (NOMBRE, DESCRIPCION, location, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4}, {5})", reporte.TituloReporte, reporte.DescripcionReporte, urlReporte, DateTime.Now, "Alta Reporte", "Diagnostic4U");
                        var maxIdReporte = context.Reporte.Max(m => m.IdReporte);
                        var query_ = context.Database.ExecuteSqlCommand("INSERT INTO ENCUESTAREPORTE (IDENCUESTA, IDREPORTE) VALUES ({0}, {1})", reporte.IdEncuesta, maxIdReporte);
                    }
                    context.SaveChanges();
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        //SaveProcess ClimaLaboral
        public static ML.Result SaveList(List<double> lista, double progreso, int IdReporteCL)
        {
            ML.Result result = new ML.Result();
            int flag = 0;
            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in lista)
                        {
                            var query = context.Database.ExecuteSqlCommand("INSERT INTO DATAREPORTCL (DATAr, PROGRESS, IdReportCL) VALUES ({0}, {1}, {2})",
                                item, progreso, IdReporteCL);
                        }
                        result.Correct = true;
                        context.SaveChanges();
                        transaction.Commit();
                        flag = 1;
                    }
                    catch (Exception ex)
                    {
                        result.Correct = false;
                        result.ErrorMessage = ex.Message;
                        transaction.Rollback();
                    }
                }
            }
            if (flag == 0)//Reintenta
            {
                Reporte.SaveList(lista, progreso, IdReporteCL);
            }
            return result;
        }
        public static ML.Result AddGenerarClimaLabEE(ML.ReporteD4U report, string tableHTML)
        {
            ML.Result result = new ML.Result();
            try
            {
                string NombreReporte = "Clima Laboral Enfoque Empresa " + report.ListFiltros[0] + " " + DateTime.Now;
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Database.ExecuteSqlCommand("INSERT INTO REPORTCL (NOMBRE, TABLEHTML, fechahoracreacion, usuariocreacion, programacreacion, unidadnegocio, enfoque, noColumnas) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7})",
                        NombreReporte, tableHTML, DateTime.Now, "GeneraReporte", "Diagnostic4U", report.ListFiltros[0], "Enfoque Empresa", report.noColumnas);
                    context.SaveChanges();
                    result.Object = context.REPORTCL.Max(m => m.IDREPORTCL);
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result AddGenerarClimaLabEA(ML.ReporteD4U report, string tableHTML)
        {
            ML.Result result = new ML.Result();
            try
            {
                string NombreReporte = "Clima Laboral Enfoque Area " + report.ListFiltros[0] + " " + DateTime.Now;
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Database.ExecuteSqlCommand("INSERT INTO REPORTCL (NOMBRE, TABLEHTML, fechahoracreacion, usuariocreacion, programacreacion, unidadnegocio, enfoque, noColumnas) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7})",
                        NombreReporte, tableHTML, DateTime.Now, "GeneraReporte", "Diagnostic4U", report.ListFiltros[0], "Enfoque Area", report.noColumnas);
                    context.SaveChanges();
                    result.Object = context.REPORTCL.Max(m => m.IDREPORTCL);
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result getgeneratorsCL()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            List<double> progresos = new List<double>();
            double avance = 0.0;
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.REPORTCL.SqlQuery("SELECT * FROM REPORTCL").ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            progresos = new List<double>();
                            ML.ReportCL reportCL = new ML.ReportCL();
                            reportCL.IdReportCL = item.IDREPORTCL;
                            reportCL.Nombre = item.NOMBRE;
                            reportCL.tableHTML = item.TABLEHTML;
                            try
                            {
                                reportCL.TableHTMLFill = item.TABLEHTMLFILL.Substring(0, 10);
                            }
                            catch (Exception)
                            {
                                reportCL.TableHTMLFill = "";
                            }
                            reportCL.UnidadNegocio = item.UnidadNegocio;
                            reportCL.Enfoque = item.Enfoque;
                            //GETAvance
                            var getAvance = context.DATAREPORTCL.SqlQuery("SELECT * FROM DATAREPORTCL WHERE IDREPORTCL = {0}", reportCL.IdReportCL).ToList();
                            foreach (var elem in getAvance)
                            {
                                try
                                {
                                    progresos.Add(Convert.ToDouble(elem.PROGRESS));
                                }
                                catch (Exception)
                                {
                                    progresos.Add(0.0);
                                }
                            }
                            if (progresos.Count == 0)
                            {
                                avance = 0.0;
                            }
                            else
                            {
                                avance = progresos.Max();
                            }
                            reportCL.ProgresoGeneral = Convert.ToString(avance);

                            result.Objects.Add(reportCL);
                        }
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result ExisteReportCL(string UnidadNegocio, string tablaHTML, string Enfoque)
        {
            ML.Result result = new ML.Result();
            List<double> progresos = new List<double>();
            int ReporteCL = 0;
            int NOCOLS = 0;
            double avance = 0.0;
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.REPORTCL.SqlQuery("select * from reportcl where unidadnegocio = {0} and Enfoque = {2} ", UnidadNegocio, tablaHTML, Enfoque).ToList();
                    if (query.Count > 0)
                    {
                        result.Exist = true;
                        //GET Avance
                        //El avance debe contener el numero de items que trae la propiedad columnas, de lo contrario eliminarlos y regresar el avance al anterior
                        foreach (var item in query)
                        {
                            ML.ReportCL reportcl = new ML.ReportCL();
                            reportcl.IdReportCL = item.IDREPORTCL;
                            ReporteCL = reportcl.IdReportCL;
                            reportcl.noColumnas = (int)item.noColumnas;
                            NOCOLS = reportcl.noColumnas;
                            result.idEncuestaAlta = reportcl.IdReportCL;
                            var getAvance = context.DATAREPORTCL.SqlQuery("SELECT * FROM DATAREPORTCL WHERE IDREPORTCL = {0}", reportcl.IdReportCL).ToList();
                            if (getAvance.Count == 0)
                            {
                                avance = 0.0;
                            }
                            else
                            {
                                foreach (var elem in getAvance)
                                {
                                    progresos.Add(Convert.ToDouble(elem.PROGRESS));
                                }
                                avance = progresos.Max();
                            }
                            
                            
                        }
                        result.AvanceDouble = avance;
                        string advance = Convert.ToString(result.AvanceDouble);
                        //Revisa si ese progreso tiene los n items de columnas
                        //var verifica = context.REPORTCL.SqlQuery("SELECT * FROM DATAREPORTCL WHERE IDREPORTCL = {0} AND PROGRESS = '" + result.AvanceDouble + "'", ReporteCL).ToList();
                        var verifica = context.DATAREPORTCL.Where(x => x.PROGRESS.Contains(advance)).ToList();
                        
                            var elimina = context.Database.ExecuteSqlCommand("DELETE FROM DATAREPORTCL WHERE IDREPORTCL = {0} AND PROGRESS = {1}", ReporteCL, result.AvanceDouble);
                            context.SaveChanges();
                            //Set Avance uno menos
                            result.AvanceDouble = BL.Reporte.GetAvance(ReporteCL);
                        
                        result.Exist = true;
                    }
                    else
                    {
                        result.Exist = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static double GetAvance(int IdReporteCL)
        {
            List<double> progresos = new List<double>();
            double avance = 0.0; ;
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var getAvance = context.DATAREPORTCL.SqlQuery("SELECT * FROM DATAREPORTCL WHERE IDREPORTCL = {0}", IdReporteCL).ToList();
                    foreach (var elem in getAvance)
                    {
                        progresos.Add(Convert.ToDouble(elem.PROGRESS));
                    }
                    avance = progresos.Max();
                }
            }
            catch (Exception ex)
            {
                
            }
            return avance;
        }

        public static ML.Result GetTableForReport(ML.ReportCL reportcl)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.REPORTCL.SqlQuery("SELECT * FROM REPORTCL WHERE IDREPORTCL = {0}", reportcl.IdReportCL).ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            result.Objects.Add(item.TABLEHTML);
                            result.Objects.Add(item.noColumnas);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetAllDataFromReport(ML.ReportCL reportCL)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.DATAREPORTCL.SqlQuery("select * from datareportcl WHERE IDREPORTCL = {0}", reportCL.IdReportCL).ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.DataReportCL data = new ML.DataReportCL();
                            data.DataR = item.DATAR;

                            result.Objects.Add(data.DataR);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result SetTableFill(ML.ReportCL report)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Database.ExecuteSqlCommand("UPDATE REPORTCL SET TABLEHTMLFILL = {0} WHERE IDREPORTCL = {1}", report.tableHTML, report.IdReportCL);

                    context.SaveChanges();
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetTableFillPart1(ML.ReportCL report)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.REPORTCL.SqlQuery("select * from reportcl where idreportcl = {0}", report.IdReportCL).ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            var length = item.TABLEHTMLFILL.Length;
                            length = length / 2;
                            result.Object = item.TABLEHTMLFILL;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result GetTableFillPart2(ML.ReportCL report)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.REPORTCL.SqlQuery("select * from reportcl where idreportcl = {0}", report.IdReportCL).ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            var length = item.TABLEHTMLFILL.Length;
                            var MinLength = (length / 2) + 1;
                            var maxLength = item.TABLEHTMLFILL.Length;
                            result.Object = item.TABLEHTMLFILL.Substring(MinLength, maxLength);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        //public static ML.Result SendMailAndDownloadLink()
        //{

        //}
    }
}
