using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class ReportesClimaController : Controller
    {
        public ActionResult ReporteGetByUnidadNegocio(string UNegocio, string idBD)
        {
            int _idBD = Convert.ToInt32(idBD);
            var result = BL.ReportesClima.GetResultadosByUnidadNegocio(UNegocio, _idBD);

            List<ML.JsonReporte> jsonList = new List<ML.JsonReporte>();

            if (result.Objects != null)
            {
                foreach (ML.EmpleadoRespuesta emp in result.Objects)
                {
                    ML.JsonReporte json = new ML.JsonReporte();
                    json.IdEmpleado = emp.Empleado.IdEmpleado;
                    json.ApellidoPaterno = emp.Empleado.ApellidoPaterno;
                    json.ApellidoMaterno = emp.Empleado.ApellidoMaterno;
                    json.Nombre = emp.Empleado.Nombre;
                    json.Puesto = emp.Empleado.Puesto;
                    json.FechaNaciemiento = emp.Empleado.FechaNaciemiento;
                    json.FechaAntiguedad = emp.Empleado.FechaAntiguedad;
                    json.Sexo = emp.Empleado.Sexo;
                    json.Correo = emp.Empleado.Correo;
                    json.TipoFuncion = emp.Empleado.Perfil.Descripcion;
                    json.CondicionTrabajo = emp.Empleado.CondicionTrabajo;
                    json.GradoAcademico = emp.Empleado.GradoAcademico;
                    json.UNIDAD = emp.Empleado.Departamento.Area.Company.CompanyCategoria.Descripcion;
                    json.DIRECION = emp.Empleado.Departamento.Area.Company.CompanyName;
                    json.AREA = emp.Empleado.Departamento.Area.Nombre;
                    json.DEPARTAMENTO = emp.Empleado.Departamento.Nombre;
                    json.SUBDEPARTAMENTO = emp.Empleado.Subdepartamento.Nombre;
                    json.EmpresaContratante = emp.Empleado.EmpresaContratante;
                    json.IdResponsableRH = emp.Empleado.IdResponsableRH;
                    json.NombreResponsableRH = emp.Empleado.NombreResponsableRH;
                    json.IdJefe = emp.Empleado.IdJefe;
                    json.NombreJefe = emp.Empleado.NombreJefe;
                    json.PuestoJefe = emp.Empleado.PuestoJefe;
                    json.IdRespinsableEstructura = emp.Empleado.IdRespinsableEstructura;
                    json.NombreResponsableEstrucutra = emp.Empleado.NombreResponsableEstrucutra;
                    json.ClaveAcceso = emp.Empleado.ClavesAcceso.ClaveAcceso;
                    json.RangoAntiguedad = emp.Empleado.RangoAntiguedad;
                    json.RangoEdad = emp.Empleado.RangoEdad;
                    json.EstatusENcuesta = emp.Empleado.EstatusEncuesta.Estatus;
                    json.EstatusEmpleado = emp.Empleado.EstatusEmpleado;

                    json.EMP_R1 = emp.EMP_R1; json.EMP_R2 = emp.EMP_R2; json.EMP_R3 = emp.EMP_R3; json.EMP_R4 = emp.EMP_R4; json.EMP_R5 = emp.EMP_R5; json.EMP_R6 = emp.EMP_R6; json.EMP_R7 = emp.EMP_R7;
                    json.EMP_R8 = emp.EMP_R8; json.EMP_R9 = emp.EMP_R9; json.EMP_R10 = emp.EMP_R10; json.EMP_R11 = emp.EMP_R11; json.EMP_R12 = emp.EMP_R12; json.EMP_R13 = emp.EMP_R13; json.EMP_R14 = emp.EMP_R14;
                    json.EMP_R15 = emp.EMP_R15; json.EMP_R16 = emp.EMP_R16; json.EMP_R17 = emp.EMP_R17; json.EMP_R18 = emp.EMP_R18; json.EMP_R19 = emp.EMP_R19; json.EMP_R20 = emp.EMP_R20; json.EMP_R21 = emp.EMP_R21;
                    json.EMP_R22 = emp.EMP_R22; json.EMP_R23 = emp.EMP_R23; json.EMP_R24 = emp.EMP_R24; json.EMP_R25 = emp.EMP_R25; json.EMP_R26 = emp.EMP_R26; json.EMP_R27 = emp.EMP_R27; json.EMP_R28 = emp.EMP_R28;
                    json.EMP_R29 = emp.EMP_R29; json.EMP_R30 = emp.EMP_R30; json.EMP_R31 = emp.EMP_R31; json.EMP_R32 = emp.EMP_R32; json.EMP_R33 = emp.EMP_R33; json.EMP_R34 = emp.EMP_R34; json.EMP_R35 = emp.EMP_R35;
                    json.EMP_R36 = emp.EMP_R36; json.EMP_R37 = emp.EMP_R37; json.EMP_R38 = emp.EMP_R38; json.EMP_R39 = emp.EMP_R39; json.EMP_R40 = emp.EMP_R40; json.EMP_R41 = emp.EMP_R41; json.EMP_R42 = emp.EMP_R42;
                    json.EMP_R43 = emp.EMP_R43; json.EMP_R44 = emp.EMP_R44; json.EMP_R45 = emp.EMP_R45; json.EMP_R46 = emp.EMP_R46; json.EMP_R47 = emp.EMP_R47; json.EMP_R48 = emp.EMP_R48; json.EMP_R49 = emp.EMP_R49;
                    json.EMP_R50 = emp.EMP_R50; json.EMP_R51 = emp.EMP_R51; json.EMP_R52 = emp.EMP_R52; json.EMP_R53 = emp.EMP_R53; json.EMP_R54 = emp.EMP_R54; json.EMP_R55 = emp.EMP_R55; json.EMP_R56 = emp.EMP_R56;
                    json.EMP_R57 = emp.EMP_R57;
                    json.EMP_R58 = emp.EMP_R58;
                    json.EMP_R59 = emp.EMP_R59;
                    json.EMP_R60 = emp.EMP_R60;

                    json.EMP_R61 = emp.EMP_R61;
                    json.EMP_R62 = emp.EMP_R62;
                    json.EMP_R63 = emp.EMP_R63;
                    json.EMP_R64 = emp.EMP_R64;
                    json.EMP_R65 = emp.EMP_R65;
                    json.EMP_R66 = emp.EMP_R66;
                    json.EMP_R67 = emp.EMP_R67;
                    json.EMP_R68 = emp.EMP_R68;
                    json.EMP_R69 = emp.EMP_R69;
                    json.EMP_R70 = emp.EMP_R70;
                    json.EMP_R71 = emp.EMP_R71;
                    json.EMP_R72 = emp.EMP_R72;
                    json.EMP_R73 = emp.EMP_R73;
                    json.EMP_R74 = emp.EMP_R74;
                    json.EMP_R75 = emp.EMP_R75;
                    json.EMP_R76 = emp.EMP_R76;
                    json.EMP_R77 = emp.EMP_R77;
                    json.EMP_R78 = emp.EMP_R78;
                    json.EMP_R79 = emp.EMP_R79;
                    json.EMP_R80 = emp.EMP_R80;

                    json.EMP_R81 = emp.EMP_R81;
                    json.EMP_R82 = emp.EMP_R82;
                    json.EMP_R83 = emp.EMP_R83;
                    json.EMP_R84 = emp.EMP_R84;
                    json.EMP_R85 = emp.EMP_R85;
                    json.EMP_R86 = emp.EMP_R86;

                    //Agregar EE

                    json.ARE_1 = emp.ARE_1;
                    json.ARE_2 = emp.ARE_2;
                    json.ARE_3 = emp.ARE_3;
                    json.ARE_4 = emp.ARE_4;
                    json.ARE_5 = emp.ARE_5;
                    json.ARE_6 = emp.ARE_6;
                    json.ARE_7 = emp.ARE_7;
                    json.ARE_8 = emp.ARE_8;
                    json.ARE_9 = emp.ARE_9;
                    json.ARE_10 = emp.ARE_10;
                    json.ARE_11 = emp.ARE_11;
                    json.ARE_12 = emp.ARE_12;
                    json.ARE_13 = emp.ARE_13;
                    json.ARE_14 = emp.ARE_14;
                    json.ARE_15 = emp.ARE_15;
                    json.ARE_16 = emp.ARE_16;
                    json.ARE_17 = emp.ARE_17;
                    json.ARE_18 = emp.ARE_18;
                    json.ARE_19 = emp.ARE_19;
                    json.ARE_20 = emp.ARE_20;


                    json.ARE_21 = emp.ARE_21;
                    json.ARE_22 = emp.ARE_22;
                    json.ARE_23 = emp.ARE_23;
                    json.ARE_24 = emp.ARE_24;
                    json.ARE_25 = emp.ARE_25;
                    json.ARE_26 = emp.ARE_26;
                    json.ARE_27 = emp.ARE_27;
                    json.ARE_28 = emp.ARE_28;
                    json.ARE_29 = emp.ARE_29;
                    json.ARE_30 = emp.ARE_30;
                    json.ARE_31 = emp.ARE_31;
                    json.ARE_32 = emp.ARE_32;
                    json.ARE_33 = emp.ARE_33;
                    json.ARE_34 = emp.ARE_34;
                    json.ARE_35 = emp.ARE_35;
                    json.ARE_36 = emp.ARE_36;
                    json.ARE_37 = emp.ARE_37;
                    json.ARE_38 = emp.ARE_38;
                    json.ARE_39 = emp.ARE_39;
                    json.ARE_40 = emp.ARE_40;

                    json.ARE_41 = emp.ARE_41;
                    json.ARE_42 = emp.ARE_42;
                    json.ARE_43 = emp.ARE_43;
                    json.ARE_44 = emp.ARE_44;
                    json.ARE_45 = emp.ARE_45;
                    json.ARE_46 = emp.ARE_46;
                    json.ARE_47 = emp.ARE_47;
                    json.ARE_48 = emp.ARE_48;
                    json.ARE_49 = emp.ARE_49;
                    json.ARE_50 = emp.ARE_50;
                    json.ARE_51 = emp.ARE_51;
                    json.ARE_52 = emp.ARE_52;
                    json.ARE_53 = emp.ARE_53;
                    json.ARE_54 = emp.ARE_54;
                    json.ARE_55 = emp.ARE_55;
                    json.ARE_56 = emp.ARE_56;
                    json.ARE_57 = emp.ARE_57;
                    json.ARE_58 = emp.ARE_58;
                    json.ARE_59 = emp.ARE_59;
                    json.ARE_60 = emp.ARE_60;

                    json.ARE_61 = emp.ARE_61;
                    json.ARE_62 = emp.ARE_62;
                    json.ARE_63 = emp.ARE_63;
                    json.ARE_64 = emp.ARE_64;
                    json.ARE_65 = emp.ARE_65;
                    json.ARE_66 = emp.ARE_66;
                    json.ARE_67 = emp.ARE_67;
                    json.ARE_68 = emp.ARE_68;
                    json.ARE_69 = emp.ARE_69;
                    json.ARE_70 = emp.ARE_70;
                    json.ARE_71 = emp.ARE_71;
                    json.ARE_72 = emp.ARE_72;
                    json.ARE_73 = emp.ARE_73;
                    json.ARE_74 = emp.ARE_74;
                    json.ARE_75 = emp.ARE_75;
                    json.ARE_76 = emp.ARE_76;
                    json.ARE_77 = emp.ARE_77;
                    json.ARE_78 = emp.ARE_78;
                    json.ARE_79 = emp.ARE_79;
                    json.ARE_80 = emp.ARE_80;

                    json.ARE_81 = emp.ARE_81;
                    json.ARE_82 = emp.ARE_82;
                    json.ARE_83 = emp.ARE_83;
                    json.ARE_84 = emp.ARE_84;
                    json.ARE_85 = emp.ARE_85;
                    json.ARE_86 = emp.ARE_86;

                    json.ESPECIAL_OPEN = emp.ESPECIAL_OPEN;
                    json.CAMBIAR_OPEN = emp.CAMBIAR_OPEN;
                    json.FORT_JEFE = emp.FORT_JEFE;
                    json.OPORT_JEFE = emp.OPORT_JEFE;
                    json.MOTIVA_TRAB = emp.MOTIVA_TRAB;
                    json.DEJAR_EMP = emp.DEJAR_EMP;
                    json.PRESION = emp.PRESION;
                    json.ANTIGUEDADR = emp.ANTIGUEDAD;
                    json.RANGO_EDADR = emp.RANGO_EDAD;
                    json.CONDICIONR = emp.CONDICION;
                    json.SEXOR = emp.SEXO;
                    json.ACADEMICOR = emp.ACADEMICO;
                    json.FUNCIONR = emp.FUNCION;
                    json.UNIDADRES = emp.UNIDAD;
                    json.DIRECIONRES = emp.DIRECION;
                    json.AREARES = emp.AREA;
                    json.DEPARTAMENTORES = emp.DEPARTAMENTO;


                    jsonList.Add(json);//El nuevo valor sobreescrbe al anterior quedando n cantidda de veces el ultimo
                }

            }
            var data = JsonConvert.SerializeObject(jsonList);

            return Content(data);
        }
    }
}
