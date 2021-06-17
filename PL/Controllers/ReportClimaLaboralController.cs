using Hangfire;
using Hangfire.MemoryStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class ReportClimaLaboralController : Controller
    {
        List<object> finalList = new List<object>();
        [ValidateInput(false)]
        public ActionResult InitLoadEE(ML.ReporteD4U reporte, string tableHTML)
        {
            //Validar si el reporte ya existe.
            //Tomar su avance y empezar de ahi
            //136
            string UnidadNegocioValida = reporte.ListFiltros[0];
            string tablaHTMLValida = tableHTML;
            double avance = 0;
            string StringAvance = Convert.ToString(avance);
            var existe = BL.Reporte.ExisteReportCL(UnidadNegocioValida, tablaHTMLValida, "Enfoque Empresa");

            StringAvance = Convert.ToString(existe.AvanceDouble);
            //Si el progreso es 100 ya no lo hagas
            if (existe.Exist == true && StringAvance != "100")
            {
                int IdReportCL = existe.idEncuestaAlta;
                avance = existe.AvanceDouble;
                StringAvance = Convert.ToString(avance);
                CalcClimController calc = new CalcClimController();

                if (avance < 0.5) { goto Uno; }
                if (avance == 0.74) { goto Dos; }
                if (avance == 1.47) { goto Tres; }
                if (avance == 2.21) { goto Cuatro; }
                if (avance == 2.94) { goto Cinco; }
                if (avance == 3.68) { goto Seis; }
                if (avance == 4.41) { goto Siete; }
                if (avance == 5.15) { goto Ocho; }
                if (avance == 5.88) { goto Nueve; }
                if (avance == 6.62) { goto Diez; }
                if (avance == 7.35) { goto Once; }
                if (avance == 8.09) { goto Doce; }
                if (avance == 8.82) { goto Trece; }
                if (avance == 9.56) { goto Catorce; }
                if (avance == 10.29) { goto Quince; }
                if (avance == 11.03) { goto Dieciseis; }
                if (avance == 11.76) { goto Diecisiete; }
                if (avance == 12.50) { goto Dieciocho; }
                if (avance == 13.24) { goto Diecinueve; }
                if (avance == 13.97) { goto Veinte; }
                if (avance == 14.71) { goto Veintiuno; }
                if (avance == 15.44) { goto Veintidos; }
                if (avance == 16.18) { goto VeintiTres; }
                if (avance == 16.91) { goto Veinticuatro; }
                if (avance == 17.65) { goto Veinticinco; }
                if (avance == 18.38) { goto Ventiseis; }
                if (avance == 19.12) { goto Veintisiete; }
                if (avance == 19.85) { goto Veintiocho; }
                if (avance == 20.59) { goto VeintiNueve; }
                if (avance == 21.32) { goto Treinta; }
                if (avance == 22.06) { goto TrintayUno; }
                if (avance == 22.79) { goto Treintaydos; }
                if (avance == 23.53) { goto trintaytres; }
                if (avance == 24.26) { goto trintay4; }
                if (avance == 25.00) { goto trintay5; }
                if (avance == 25.74) { goto treintay6; }
                if (avance == 26.47) { goto trintay7; }
                if (avance == 27.21) { goto treintay8; }
                if (avance == 27.94) { goto treintay9; }
                if (avance == 28.68) { goto cuarenta; }
                if (avance == 29.41) { goto cuarentay1; }
                if (avance == 30.15) { goto cuarentay2; }
                if (avance == 30.88) { goto cuarentay3; }
                if (avance == 31.62) { goto cuarentay4; }
                if (avance == 32.35) { goto cuarentay5; }
                if (avance == 33.09) { goto cuarentay6; }
                if (avance == 33.82) { goto cuarentay7; }
                if (avance == 34.56) { goto cuarentay8; }
                if (avance == 35.29) { goto cuarentay9; }
                if (avance == 36.03) { goto cincuenta; }
                if (avance == 36.76) { goto cincuentay1; }
                if (avance == 37.50) { goto cincuentay2; }
                if (avance == 38.24) { goto cincuentay3; }
                if (avance == 38.97) { goto cuncuentay4; }
                if (avance == 39.71) { goto cincuentay5; }
                if (avance == 40.44) { goto cincuentay6; }
                if (avance == 41.18) { goto cincuentay7; }
                if (avance == 41.91) { goto cincuentay8; }
                if (avance == 42.65) { goto cincuentay9; }
                if (avance == 43.38) { goto sesenta; }
                if (avance == 44.12) { goto sesentay1; }
                if (avance == 44.85) { goto sesentay2; }
                if (avance == 45.59) { goto sesentay3; }
                if (avance == 46.32) { goto sesentay4; }
                if (avance == 47.06) { goto sesentay5; }
                if (avance == 47.79) { goto sesentay6; }
                if (avance == 48.53) { goto sesentay7; }
                if (avance == 49.26) { goto sesentay8; }
                if (avance == 50.00) { goto sesentay9; }
                if (avance == 50.74) { goto setenta; }
                if (avance == 51.47) { goto setentay1; }
                if (avance == 52.21) { goto setentay2; }
                if (avance == 52.94) { goto setentay3; }
                if (avance == 53.68) { goto setentay4; }
                if (avance == 54.41) { goto setentay5; }
                if (avance == 55.15) { goto setentay6; }
                if (avance == 55.88) { goto setentay7; }
                if (avance == 56.62) { goto setentay8; }
                if (avance == 57.35) { goto setentay9; }
                if (avance == 58.09) { goto ochenta; }
                if (avance == 58.82) { goto ochentay1; }
                if (avance == 59.56) { goto ochentay2; }
                if (avance == 60.29) { goto ochentay3; }
                if (avance == 61.03) { goto ochentay4; }
                if (avance == 61.76) { goto ochentay5; }
                if (avance == 62.50) { goto ochentay6; }
                if (avance == 63.24) { goto ochentay7; }
                if (avance == 63.97) { goto ochentay8; }
                if (avance == 64.71) { goto ochentay9; }
                if (avance == 65.44) { goto noventa; }
                if (avance == 66.18) { goto noventay1; }
                if (avance == 66.91) { goto noventay2; }
                if (avance == 67.65) { goto noventay3; }
                if (avance == 68.38) { goto noventay4; }
                if (avance == 69.12) { goto noventay5; }
                if (avance == 69.85) { goto noventay6; }
                if (avance == 70.59) { goto noventay7; }
                if (avance == 71.32) { goto noventay8; }
                if (avance == 72.06) { goto noventay9; }
                if (avance == 72.79) { goto cien; }
                if (avance == 73.53) { goto ciento1; }
                if (avance == 74.26) { goto ciento2; }
                if (avance == 75.00) { goto ciento3; }
                if (avance == 75.74) { goto ciento4; }
                if (avance == 76.47) { goto ciento5; }
                if (avance == 77.21) { goto ciento6; }
                if (avance == 77.94) { goto ciento7; }
                if (avance == 78.68) { goto ciento8; }
                if (avance == 79.41) { goto ciento9; }
                if (avance == 80.15) { goto ciento10; }
                if (avance == 80.88) { goto ciento11; }
                if (avance == 81.62) { goto ciento12; }
                if (avance == 82.35) { goto ciento13; }
                if (avance == 83.09) { goto ciento14; }
                if (avance == 83.82) { goto ciento15; }
                if (avance == 84.56) { goto ciento16; }
                if (avance == 85.29) { goto ciento17; }
                if (avance == 86.03) { goto ciento18; }
                if (avance == 86.76) { goto ciento19; }
                if (avance == 87.50) { goto ciento20; }
                if (avance == 88.24) { goto ciento21; }
                if (avance == 88.97) { goto ciento22; }
                if (avance == 89.71) { goto ciento23; }
                if (avance == 90.44) { goto ciento24; }
                if (avance == 91.18) { goto ciento25; }
                if (avance == 91.91) { goto ciento26; }
                if (avance == 92.65) { goto ciento27; }
                if (avance == 93.38) { goto ciento28; }
                if (avance == 94.12) { goto ciento29; }
                if (avance == 94.85) { goto ciento30; }
                if (avance == 95.59) { goto ciento31; }
                if (avance == 96.32) { goto ciento32; }
                if (avance == 97.06) { goto ciento33; }
                if (avance == 97.79) { goto ciento34; }
                if (avance == 98.53) { goto ciento35; }
                if (avance == 99.26) { goto ciento36; }
                if (avance == 100.00) { goto ciento37; }


            Uno:
                var list1 = calc.GetEsperadas(reporte);
                BL.Reporte.SaveList(list1, 0.74, IdReportCL);
                //
                Dos:
                var list2 = calc.GetContestadas(reporte);
                BL.Reporte.SaveList(list2, 1.47, IdReportCL);

                //Custom with numberQuestions
                //
                Tres:
                reporte.IdPregunta = 2;
                var list3 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list3, 2.21, IdReportCL);
                //
                Cuatro:
                reporte.IdPregunta = 3;
                var list4 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list4, 2.94, IdReportCL);
                //
                Cinco:
                reporte.IdPregunta = 4;
                var list5 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list5, 3.68, IdReportCL);
                //
                Seis:
                reporte.IdPregunta = 1;
                var list6 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list6, 4.41, IdReportCL);
                //
                Siete:
                reporte.IdPregunta = 10;
                var list7 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list7, 5.15, IdReportCL);
                //
                Ocho:
                reporte.IdPregunta = 9;
                var list8 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list8, 5.88, IdReportCL);
                //
                Nueve:
                reporte.IdPregunta = 11;
                var list9 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list9, 6.62, IdReportCL);
                //
                Diez:
                reporte.IdPregunta = 5;
                var list10 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list10, 7.35, IdReportCL);
                //
                Once:
                reporte.IdPregunta = 14;
                var list11 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list11, 8.09, IdReportCL);
                //
                Doce:
                reporte.IdPregunta = 12;
                var list12 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list12, 8.82, IdReportCL);
                //
                Trece:
                reporte.IdPregunta = 7;
                var list13 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list13, 9.56, IdReportCL);
                //
                Catorce:
                reporte.IdPregunta = 8;
                var list14 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list14, 10.29, IdReportCL);
                //
                Quince:
                reporte.IdPregunta = 6;
                var list15 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list15, 11.03, IdReportCL);
                //
                Dieciseis:
                reporte.IdPregunta = 30;
                var list16 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list16, 11.76, IdReportCL);
                //
                Diecisiete:
                reporte.IdPregunta = 28;
                var list17 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list17, 12.50, IdReportCL);
                //
                Dieciocho:
                reporte.IdPregunta = 33;
                var list18 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list18, 13.24, IdReportCL);
                //
                Diecinueve:
                reporte.IdPregunta = 35;
                var list19 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list19, 13.97, IdReportCL);
                //
                Veinte:
                reporte.IdPregunta = 32;
                var list20 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list20, 14.71, IdReportCL);
                //
                Veintiuno:
                reporte.IdPregunta = 36;
                var list21 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list21, 15.44, IdReportCL);
                //****************************************************************
                Veintidos:
                reporte.IdPregunta = 37;
                var list22 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list22, 16.18, IdReportCL);
                //
                VeintiTres:
                reporte.IdPregunta = 34;
                var list23 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list23, 16.91, IdReportCL);
                //
                Veinticuatro:
                reporte.IdPregunta = 38;
                var list24 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list24, 17.65, IdReportCL);
                //
                Veinticinco:
                reporte.IdPregunta = 39;
                var list25 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list25, 18.38, IdReportCL);
                //
                Ventiseis:
                reporte.IdPregunta = 31;
                var list26 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list26, 19.12, IdReportCL);
                //
                Veintisiete:
                reporte.IdPregunta = 43;
                var list27 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list27, 19.85, IdReportCL);
                //
                Veintiocho:
                reporte.IdPregunta = 41;
                var list28 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list28, 20.59, IdReportCL);
                //
                VeintiNueve:
                reporte.IdPregunta = 42;
                var list29 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list29, 21.32, IdReportCL);
                //
                Treinta:
                reporte.IdPregunta = 46;
                var list30 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list30, 22.06, IdReportCL);
                //
                TrintayUno:
                reporte.IdPregunta = 45;
                var list31 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list31, 22.79, IdReportCL);
                //
                Treintaydos:
                reporte.IdPregunta = 44;
                var list32 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list32, 23.53, IdReportCL);
                //
                trintaytres:
                reporte.IdPregunta = 40;
                var list33 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list33, 24.26, IdReportCL);
                //
                trintay4:
                reporte.IdPregunta = 22;
                var list34 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list34, 25, IdReportCL);
                //
                trintay5:
                reporte.IdPregunta = 27;
                var list35 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list35, 25.74, IdReportCL);
                //
                treintay6:
                reporte.IdPregunta = 47;
                var list36 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list36, 26.47, IdReportCL);
                //
                trintay7:
                reporte.IdPregunta = 49;
                var list37 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list37, 27.21, IdReportCL);
                //
                treintay8:
                reporte.IdPregunta = 58;
                var list38 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list38, 27.94, IdReportCL);
                //
                treintay9:
                reporte.IdPregunta = 59;
                var list39 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list39, 28.68, IdReportCL);
                //
                cuarenta:
                reporte.IdPregunta = 51;
                var list40 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list40, 29.41, IdReportCL);
                //
                cuarentay1:
                reporte.IdPregunta = 57;
                var list41 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list41, 30.15, IdReportCL);
                //
                cuarentay2:
                reporte.IdPregunta = 54;
                var list42 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list42, 30.88, IdReportCL);
                //
                cuarentay3:
                reporte.IdPregunta = 50;
                var list43 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list43, 31.62, IdReportCL);
                //
                cuarentay4:
                reporte.IdPregunta = 55;
                var list44 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list44, 32.35, IdReportCL);
                //
                cuarentay5:
                reporte.IdPregunta = 56;
                var list45 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list45, 33.09, IdReportCL);
                //
                cuarentay6:
                reporte.IdPregunta = 53;
                var list46 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list46, 33.82, IdReportCL);
                //
                cuarentay7:
                reporte.IdPregunta = 16;
                var list47 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list47, 34.56, IdReportCL);
                //
                cuarentay8:
                reporte.IdPregunta = 21;
                var list48 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list48, 35.29, IdReportCL);
                //
                cuarentay9:
                reporte.IdPregunta = 15;
                var list49 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list49, 36.03, IdReportCL);
                //
                cincuenta:
                reporte.IdPregunta = 17;
                var list50 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list50, 36.76, IdReportCL);
                //
                cincuentay1:
                reporte.IdPregunta = 18;
                var list51 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list51, 37.50, IdReportCL);
                //
                cincuentay2:
                reporte.IdPregunta = 19;
                var list52 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list52, 38.24, IdReportCL);
                //
                cincuentay3:
                reporte.IdPregunta = 48;
                var list53 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list53, 38.97, IdReportCL);
                //
                cuncuentay4:
                reporte.IdPregunta = 23;
                var list54 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list54, 39.71, IdReportCL);
                //
                cincuentay5:
                reporte.IdPregunta = 26;
                var list55 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list55, 40.44, IdReportCL);
                //
                cincuentay6:
                reporte.IdPregunta = 52;
                var list56 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list56, 41.18, IdReportCL);
                //
                cincuentay7:
                reporte.IdPregunta = 20;
                var list57 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list57, 41.91, IdReportCL);
                //
                cincuentay8:
                reporte.IdPregunta = 25;
                var list58 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list58, 42.65, IdReportCL);
                //
                cincuentay9:
                reporte.IdPregunta = 60;
                var list59 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list59, 43.38, IdReportCL);
                //
                sesenta:
                reporte.IdPregunta = 61;
                var list60 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list60, 44.12, IdReportCL);
                //
                sesentay1:
                reporte.IdPregunta = 62;
                var list61 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list61, 44.85, IdReportCL);
                //
                sesentay2:
                reporte.IdPregunta = 63;
                var list62 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list62, 45.59, IdReportCL);
                //
                sesentay3:
                reporte.IdPregunta = 64;
                var list63 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list63, 46.32, IdReportCL);
                //
                sesentay4:
                reporte.IdPregunta = 65;
                var list64 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list64, 47.06, IdReportCL);
                //
                sesentay5:
                reporte.IdPregunta = 66;
                var list65 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list65, 47.79, IdReportCL);
                //
                sesentay6:
                reporte.IdPregunta = 13;
                var list66 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list66, 48.53, IdReportCL);
                //
                sesentay7:
                reporte.IdPregunta = 24;
                var list67 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list67, 49.26, IdReportCL);
                //
                sesentay8:
                reporte.IdPregunta = 29;
                var list68 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list68, 50, IdReportCL);
                //
                sesentay9:
                reporte.IdPregunta = 67;
                var list69 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list69, 50.74, IdReportCL);
                //
                setenta:
                reporte.IdPregunta = 71;
                var list70 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list70, 51.47, IdReportCL);
                //
                setentay1:
                reporte.IdPregunta = 75;
                var list71 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list71, 52.21, IdReportCL);
                //
                setentay2:
                reporte.IdPregunta = 84;
                var list72 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list72, 52.94, IdReportCL);
                //
                setentay3:
                reporte.IdPregunta = 79;
                var list73 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list73, 53.68, IdReportCL);
                //
                setentay4:
                reporte.IdPregunta = 81;
                var list74 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list74, 54.41, IdReportCL);
                //
                setentay5:
                reporte.IdPregunta = 83;
                var list75 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list75, 55.15, IdReportCL);
                //
                setentay6:
                reporte.IdPregunta = 86;
                var list76 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list76, 55.88, IdReportCL);
                //
                setentay7:
                reporte.IdPregunta = 69;
                var list77 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list77, 56.62, IdReportCL);
                //
                setentay8:
                reporte.IdPregunta = 73;
                var list78 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list78, 57.35, IdReportCL);
                //
                setentay9:
                reporte.IdPregunta = 77;
                var list79 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list79, 58.09, IdReportCL);
                //
                ochenta:
                reporte.IdPregunta = 80;
                var list80 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list80, 58.82, IdReportCL);
                //
                ochentay1:
                reporte.IdPregunta = 68;
                var list81 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list81, 59.56, IdReportCL);
                //
                ochentay2:
                reporte.IdPregunta = 70;
                var list82 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list82, 60.29, IdReportCL);
                //
                ochentay3:
                reporte.IdPregunta = 72;
                var list83 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list83, 61.03, IdReportCL);
                //
                ochentay4:
                reporte.IdPregunta = 74;
                var list84 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list84, 61.76, IdReportCL);
                //
                ochentay5:
                reporte.IdPregunta = 76;
                var list85 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list85, 62.50, IdReportCL);
                //
                ochentay6:
                reporte.IdPregunta = 78;
                var list86 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list86, 63.24, IdReportCL);
                //
                ochentay7:
                reporte.IdPregunta = 82;
                var list87 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list87, 63.97, IdReportCL);
                //
                ochentay8:
                reporte.IdPregunta = 85;
                var list88 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list88, 64.71, IdReportCL);
                /*
                 Fin de promedios de preguntas individuales
                 Inicio de promedios por categorias
                 */
                //ochentay9:
                //var list89 = calc.GetValueComodinEnfoqueEmpresa(reporte);
                //BL.Reporte.SaveList(list89, 63.67, IdReportCL);
                //
                ochentay9:
                var list90 = calc.GetPorcentajeParticipacionEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list90, 65.44, IdReportCL);
                //
                noventa:
                var list91 = calc.GetPromediosCreedibilidadEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list91, 66.18, IdReportCL);
                //
                noventay1:
                var list92 = calc.GetPromediosImparcialidadEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list92, 66.91, IdReportCL);
                //
                noventay2:
                var list93 = calc.GetPromediosOrgulloEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list93, 67.65, IdReportCL);
                //
                noventay3:
                var list94 = calc.GetPromediosRespetoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list94, 68.38, IdReportCL);
                //
                noventay4:
                var list95 = calc.GetPromediosCompañerismoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list95, 69.12, IdReportCL);
                //
                noventay5:
                var list96 = calc.GetPromediosCoachingEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list96, 69.85, IdReportCL);
                //
                noventay6:
                var list97 = calc.GetPromediosCambioEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list97, 70.59, IdReportCL);
                //
                noventay7:
                var list98 = calc.GetPromediosBienestarEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list98, 71.32, IdReportCL);
                //
                noventay8:
                var list99 = calc.GetPromediosAlinCulturalEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list99, 72.06, IdReportCL);
                //
                noventay9:
                var list100 = calc.GetPromediosGestaltEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list100, 72.79, IdReportCL);
                //
                //ciento1:
                //var list101 = calc.GetPromediosConfianzaEnfoqueEmpresa(reporte);
                //BL.Reporte.SaveList(list101, 72.20, IdReportCL);
                //
                cien:
                var list102 = calc.GetPromedios66ReactivosEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list102, 73.53, IdReportCL);
                //
                ciento1:
                var list103 = calc.GetPromedios86ReactivosEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list103, 74.26, IdReportCL);
                //
                //ciento4:
                //var list104 = calc.GetPromediosPracticasCulturealesEnfoqueEmpresa(reporte);
                //BL.Reporte.SaveList(list104, 74.41, IdReportCL);
                //
                ciento2:
                var list105 = calc.GetPromediosReclutandoBienvenidaEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list105, 75, IdReportCL);
                //
                ciento3:
                var list106 = calc.GetPromediosInspirandoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list106, 75.74, IdReportCL);
                //
                ciento4:
                var list107 = calc.GetPromediosHablandoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list107, 76.47, IdReportCL);
                //
                ciento5:
                var list108 = calc.GetPromediosEscuchandoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list108, 77.21, IdReportCL);
                //
                ciento6:
                var list109 = calc.GetPromediosAgradeciendoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list109, 77.94, IdReportCL);
                //
                ciento7:
                var list110 = calc.GetPromediosDesarrollandoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list110, 78.68, IdReportCL);
                //
                ciento8:
                var list111 = calc.GetPromediosCuidandoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list111, 79.41, IdReportCL);
                //
                ciento9:
                var list112 = calc.GetPromediosPercepcionLugarEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list112, 80.15, IdReportCL);
                //
                ciento10:
                var list113 = calc.GetPromediosCooperandoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list113, 80.88, IdReportCL);
                //
                ciento11:
                var list114 = calc.GetPromediosAlineacionEstrategicaEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list114, 81.62, IdReportCL);
                //
                //ciento15:
                //var list115 = calc.GetPromediosProcesosOrganizacionalesEnfoqueEmpresa(reporte);
                //BL.Reporte.SaveList(list115, 82.20, IdReportCL);
                //
                ciento12:
                reporte.IdPregunta = 25;
                var list116 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list116, 82.35, IdReportCL);
                //
                ciento13:
                reporte.IdPregunta = 6;
                var list117 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list117, 83.09, IdReportCL);
                //
                ciento14:
                var list118 = calc.GetPromediosCarreraYPromocionPersEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list118, 83.82, IdReportCL);
                //
                ciento15:
                var list119 = calc.GetPromediosCapacitacionYDesarrolloEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list119, 84.56, IdReportCL);
                //
                ciento16:
                var list120 = calc.GetPromediosEMPOWERMENTEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list120, 85.29, IdReportCL);
                //
                ciento17:
                var list121 = calc.GetPromediosEvalDesempeñoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list121, 86.03, IdReportCL);
                //
                ciento18:
                reporte.IdPregunta = 28;
                var list122 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list122, 86.76, IdReportCL);
                //
                ciento19:
                reporte.IdPregunta = 30;
                var list123 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list123, 87.50, IdReportCL);
                //
                ciento20:
                var list124 = calc.GetPromediosIntegracionEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list124, 88.24, IdReportCL);
                //
                ciento21:
                reporte.IdPregunta = 39;
                var list125 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list125, 88.97, IdReportCL);
                //
                ciento22:
                var list126 = calc.GetPromediosNivelCoolaboracionEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list126, 89.71, IdReportCL);
                //
                ciento23:
                var list127 = calc.GetPromediosNivelCompromisoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list127, 90.44, IdReportCL);
                //
                ciento24:
                var list128 = calc.GetPromediosFactorSocialEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list128, 91.18, IdReportCL);
                //
                ciento25:
                var list129 = calc.GetPromediosFactorPsicoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list129, 91.91, IdReportCL);
                //
                ciento26:
                var list130 = calc.GetPromediosFactorFisicoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list130, 92.65, IdReportCL);
                //
                ciento27:
                var list131 = calc.GetPromediosAlinCulturalEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list131, 93.38, IdReportCL);
                //
                ciento28:
                var list132 = calc.GetPromediosBienestarEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list132, 94.12, IdReportCL);
                //
                ciento29:
                var list133 = calc.GetPromediosBioEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list133, 94.85, IdReportCL);
                //
                ciento30:
                var list134 = calc.GetPromediosPsicoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list134, 95.59, IdReportCL);
                //
                ciento31:
                var list135 = calc.GetPromediosFactorSocialEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list135, 96.32, IdReportCL);
                //
                ciento32:
                var list136 = calc.GetPromediosComunicacionEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list136, 97.06, IdReportCL);
                //
                ciento33:
                var list137 = calc.GetPromediosEnpowerEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list137, 97.79, IdReportCL);
                //
                ciento34:
                var list138 = calc.GetPromediosCoordinacionEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list138, 98.53, IdReportCL);
                //e
                ciento35:
                var list139 = calc.GetPromediosVisionEstrateEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list139, 99.26, IdReportCL);
                //
                ciento36:
                var list140 = calc.GetPromediosNivelDesempeñoEstrateEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list140, 100, IdReportCL);
                ciento37:
                Console.WriteLine("");
                //***************************************************************************************
            }
            else if(existe.Exist == false)//JAMG Ajustar porcentajes
            {
                var ressult = BL.Reporte.AddGenerarClimaLabEE(reporte, tableHTML);
                int IdReportCL = Convert.ToInt32(ressult.Object);
                CalcClimController calc = new CalcClimController();
                var list1 = calc.GetEsperadas(reporte);
                BL.Reporte.SaveList(list1, 0.74, IdReportCL);
                var list2 = calc.GetContestadas(reporte);
                BL.Reporte.SaveList(list2, 1.47, IdReportCL);

                //Custom with numberQuestions
                //
                reporte.IdPregunta = 2;
                var list3 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list3, 2.21, IdReportCL);
                //
                reporte.IdPregunta = 3;
                var list4 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list4, 2.94, IdReportCL);
                //
                reporte.IdPregunta = 4;
                var list5 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list5, 3.68, IdReportCL);
                //
                reporte.IdPregunta = 1;
                var list6 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list6, 4.41, IdReportCL);
                //
                reporte.IdPregunta = 10;
                var list7 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list7, 5.15, IdReportCL);
                //
                reporte.IdPregunta = 9;
                var list8 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list8, 5.88, IdReportCL);
                //
                reporte.IdPregunta = 11;
                var list9 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list9, 6.62, IdReportCL);
                //
                reporte.IdPregunta = 5;
                var list10 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list10, 7.35, IdReportCL);
                //
                reporte.IdPregunta = 14;
                var list11 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list11, 8.09, IdReportCL);
                //
                reporte.IdPregunta = 12;
                var list12 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list12, 8.82, IdReportCL);
                //
                reporte.IdPregunta = 7;
                var list13 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list13, 9.56, IdReportCL);
                //
                reporte.IdPregunta = 8;
                var list14 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list14, 10.29, IdReportCL);
                //
                reporte.IdPregunta = 6;
                var list15 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list15, 11.03, IdReportCL);
                //
                reporte.IdPregunta = 30;
                var list16 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list16, 11.76, IdReportCL);
                //
                reporte.IdPregunta = 28;
                var list17 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list17, 12.50, IdReportCL);
                //
                reporte.IdPregunta = 33;
                var list18 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list18, 13.24, IdReportCL);
                //
                reporte.IdPregunta = 35;
                var list19 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list19, 13.97, IdReportCL);
                //
                reporte.IdPregunta = 32;
                var list20 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list20, 14.71, IdReportCL);
                //
                reporte.IdPregunta = 36;
                var list21 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list21, 15.44, IdReportCL);
                //****************************************************************
                reporte.IdPregunta = 37;
                var list22 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list22, 16.18, IdReportCL);
                //
                reporte.IdPregunta = 34;
                var list23 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list23, 16.91, IdReportCL);
                //
                reporte.IdPregunta = 38;
                var list24 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list24, 17.65, IdReportCL);
                //
                reporte.IdPregunta = 39;
                var list25 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list25, 18.38, IdReportCL);
                //
                reporte.IdPregunta = 31;
                var list26 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list26, 19.12, IdReportCL);
                //
                reporte.IdPregunta = 43;
                var list27 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list27, 19.85, IdReportCL);
                //
                reporte.IdPregunta = 41;
                var list28 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list28, 20.59, IdReportCL);
                //
                reporte.IdPregunta = 42;
                var list29 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list29, 21.32, IdReportCL);
                //
                reporte.IdPregunta = 46;
                var list30 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list30, 22.06, IdReportCL);
                //
                reporte.IdPregunta = 45;
                var list31 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list31, 22.79, IdReportCL);
                //
                reporte.IdPregunta = 44;
                var list32 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list32, 23.53, IdReportCL);
                //
                reporte.IdPregunta = 40;
                var list33 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list33, 24.26, IdReportCL);
                //
                reporte.IdPregunta = 22;
                var list34 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list34, 25, IdReportCL);
                //
                reporte.IdPregunta = 27;
                var list35 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list35, 25.74, IdReportCL);
                //
                reporte.IdPregunta = 47;
                var list36 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list36, 26.47, IdReportCL);
                //
                reporte.IdPregunta = 49;
                var list37 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list37, 27.21, IdReportCL);
                //
                reporte.IdPregunta = 58;
                var list38 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list38, 27.94, IdReportCL);
                //
                reporte.IdPregunta = 59;
                var list39 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list39, 28.68, IdReportCL);
                //
                reporte.IdPregunta = 51;
                var list40 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list40, 29.41, IdReportCL);
                //
                reporte.IdPregunta = 57;
                var list41 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list41, 30.15, IdReportCL);
                //
                reporte.IdPregunta = 54;
                var list42 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list42, 30.88, IdReportCL);
                //
                reporte.IdPregunta = 50;
                var list43 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list43, 31.62, IdReportCL);
                //
                reporte.IdPregunta = 55;
                var list44 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list44, 32.35, IdReportCL);
                //
                reporte.IdPregunta = 56;
                var list45 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list45, 33.09, IdReportCL);
                //
                reporte.IdPregunta = 53;
                var list46 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list46, 33.82, IdReportCL);
                //
                reporte.IdPregunta = 16;
                var list47 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list47, 34.56, IdReportCL);
                //
                reporte.IdPregunta = 21;
                var list48 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list48, 35.29, IdReportCL);
                //
                reporte.IdPregunta = 15;
                var list49 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list49, 36.03, IdReportCL);
                //
                reporte.IdPregunta = 17;
                var list50 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list50, 36.76, IdReportCL);
                //
                reporte.IdPregunta = 18;
                var list51 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list51, 37.50, IdReportCL);
                //
                reporte.IdPregunta = 19;
                var list52 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list52, 38.24, IdReportCL);
                //
                reporte.IdPregunta = 48;
                var list53 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list53, 38.97, IdReportCL);
                //
                reporte.IdPregunta = 23;
                var list54 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list54, 39.71, IdReportCL);
                //
                reporte.IdPregunta = 26;
                var list55 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list55, 40.44, IdReportCL);
                //
                reporte.IdPregunta = 52;
                var list56 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list56, 41.18, IdReportCL);
                //
                reporte.IdPregunta = 20;
                var list57 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list57, 41.91, IdReportCL);
                //
                reporte.IdPregunta = 25;
                var list58 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list58, 42.65, IdReportCL);
                //
                reporte.IdPregunta = 60;
                var list59 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list59, 43.38, IdReportCL);
                //
                reporte.IdPregunta = 61;
                var list60 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list60, 44.12, IdReportCL);
                //
                reporte.IdPregunta = 62;
                var list61 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list61, 44.85, IdReportCL);
                //
                reporte.IdPregunta = 63;
                var list62 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list62, 45.59, IdReportCL);
                //
                reporte.IdPregunta = 64;
                var list63 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list63, 46.32, IdReportCL);
                //
                reporte.IdPregunta = 65;
                var list64 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list64, 47.06, IdReportCL);
                //
                reporte.IdPregunta = 66;
                var list65 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list65, 47.79, IdReportCL);
                //
                reporte.IdPregunta = 13;
                var list66 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list66, 48.53, IdReportCL);
                //
                reporte.IdPregunta = 24;
                var list67 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list67, 49.26, IdReportCL);
                //
                reporte.IdPregunta = 29;
                var list68 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list68, 50, IdReportCL);
                //
                reporte.IdPregunta = 67;
                var list69 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list69, 50.74, IdReportCL);
                //
                reporte.IdPregunta = 71;
                var list70 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list70, 51.47, IdReportCL);
                //
                reporte.IdPregunta = 75;
                var list71 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list71, 52.21, IdReportCL);
                //
                reporte.IdPregunta = 84;
                var list72 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list72, 52.94, IdReportCL);
                //
                reporte.IdPregunta = 79;
                var list73 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list73, 53.68, IdReportCL);
                //
                reporte.IdPregunta = 81;
                var list74 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list74, 54.41, IdReportCL);
                //
                reporte.IdPregunta = 83;
                var list75 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list75, 55.15, IdReportCL);
                //
                reporte.IdPregunta = 86;
                var list76 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list76, 55.88, IdReportCL);
                //
                reporte.IdPregunta = 69;
                var list77 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list77, 56.62, IdReportCL);
                //
                reporte.IdPregunta = 73;
                var list78 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list78, 57.35, IdReportCL);
                //
                reporte.IdPregunta = 77;
                var list79 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list79, 58.09, IdReportCL);
                //
                reporte.IdPregunta = 80;
                var list80 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list80, 58.82, IdReportCL);
                //
                reporte.IdPregunta = 68;
                var list81 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list81, 59.56, IdReportCL);
                //
                reporte.IdPregunta = 70;
                var list82 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list82, 60.29, IdReportCL);
                //
                reporte.IdPregunta = 72;
                var list83 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list83, 61.03, IdReportCL);
                //
                reporte.IdPregunta = 74;
                var list84 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list84, 61.76, IdReportCL);
                //
                reporte.IdPregunta = 76;
                var list85 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list85, 62.50, IdReportCL);
                //
                reporte.IdPregunta = 78;
                var list86 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list86, 63.24, IdReportCL);
                //
                reporte.IdPregunta = 82;
                var list87 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list87, 63.97, IdReportCL);
                //
                reporte.IdPregunta = 85;
                var list88 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list88, 64.71, IdReportCL);
                /*
                 Fin de promedios de preguntas individuales
                 Inicio de promedios por categorias
                 */
                //var list89 = calc.GetValueComodinEnfoqueEmpresa(reporte);
                //BL.Reporte.SaveList(list89, 63.67, IdReportCL);
                //
                var list90 = calc.GetPorcentajeParticipacionEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list90, 65.44, IdReportCL);
                //
                var list91 = calc.GetPromediosCreedibilidadEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list91, 66.18, IdReportCL);
                //
                var list92 = calc.GetPromediosImparcialidadEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list92, 66.91, IdReportCL);
                //
                var list93 = calc.GetPromediosOrgulloEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list93, 67.65, IdReportCL);
                //
                var list94 = calc.GetPromediosRespetoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list94, 68.38, IdReportCL);
                //
                var list95 = calc.GetPromediosCompañerismoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list95, 69.12, IdReportCL);
                //
                var list96 = calc.GetPromediosCoachingEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list96, 69.85, IdReportCL);
                //
                var list97 = calc.GetPromediosCambioEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list97, 70.59, IdReportCL);
                //
                var list98 = calc.GetPromediosBienestarEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list98, 71.32, IdReportCL);
                //
                var list99 = calc.GetPromediosAlinCulturalEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list99, 72.06, IdReportCL);
                //
                var list100 = calc.GetPromediosGestaltEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list100, 72.79, IdReportCL);
                //
                //var list101 = calc.GetPromediosConfianzaEnfoqueEmpresa(reporte);
                //BL.Reporte.SaveList(list101, 72.20, IdReportCL);
                //
                var list102 = calc.GetPromedios66ReactivosEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list102, 73.53, IdReportCL);
                //
                var list103 = calc.GetPromedios86ReactivosEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list103, 74.26, IdReportCL);
                //
                //var list104 = calc.GetPromediosPracticasCulturealesEnfoqueEmpresa(reporte);
                //BL.Reporte.SaveList(list104, 74.41, IdReportCL);
                //
                var list105 = calc.GetPromediosReclutandoBienvenidaEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list105, 75, IdReportCL);
                //
                var list106 = calc.GetPromediosInspirandoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list106, 75.74, IdReportCL);
                //
                var list107 = calc.GetPromediosHablandoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list107, 76.47, IdReportCL);
                //
                var list108 = calc.GetPromediosEscuchandoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list108, 77.21, IdReportCL);
                //
                var list109 = calc.GetPromediosAgradeciendoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list109, 77.94, IdReportCL);
                //
                var list110 = calc.GetPromediosDesarrollandoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list110, 78.68, IdReportCL);
                //
                var list111 = calc.GetPromediosCuidandoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list111, 79.41, IdReportCL);
                //
                var list112 = calc.GetPromediosPercepcionLugarEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list112, 80.15, IdReportCL);
                //
                var list113 = calc.GetPromediosCooperandoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list113, 80.88, IdReportCL);
                //
                var list114 = calc.GetPromediosAlineacionEstrategicaEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list114, 81.62, IdReportCL);
                //
                //var list115 = calc.GetPromediosProcesosOrganizacionalesEnfoqueEmpresa(reporte);
                //BL.Reporte.SaveList(list115, 82.20, IdReportCL);
                //
                reporte.IdPregunta = 25;
                var list116 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list116, 82.35, IdReportCL);
                //
                reporte.IdPregunta = 6;
                var list117 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list117, 83.09, IdReportCL);
                //
                var list118 = calc.GetPromediosCarreraYPromocionPersEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list118, 83.88, IdReportCL);
                //
                var list119 = calc.GetPromediosCapacitacionYDesarrolloEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list119, 84.56, IdReportCL);
                //
                var list120 = calc.GetPromediosEMPOWERMENTEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list120, 85.29, IdReportCL);
                //
                var list121 = calc.GetPromediosEvalDesempeñoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list121, 86.03, IdReportCL);
                //
                reporte.IdPregunta = 28;
                var list122 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list122, 86.76, IdReportCL);
                //
                reporte.IdPregunta = 30;
                var list123 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list123, 87.50, IdReportCL);
                //
                var list124 = calc.GetPromediosIntegracionEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list124, 88.24, IdReportCL);
                //
                reporte.IdPregunta = 39;
                var list125 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list125, 88.97, IdReportCL);
                //
                var list126 = calc.GetPromediosNivelCoolaboracionEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list126, 89.71, IdReportCL);
                //
                var list127 = calc.GetPromediosNivelCompromisoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list127, 90.44, IdReportCL);
                //
                var list128 = calc.GetPromediosFactorSocialEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list128, 91.18, IdReportCL);
                //
                var list129 = calc.GetPromediosFactorPsicoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list129, 91.91, IdReportCL);
                //
                var list130 = calc.GetPromediosFactorFisicoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list130, 92.65, IdReportCL);
                //
                var list131 = calc.GetPromediosAlinCulturalEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list131, 93.38, IdReportCL);
                //
                var list132 = calc.GetPromediosBienestarEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list132, 94.12, IdReportCL);
                //
                var list133 = calc.GetPromediosBioEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list133, 94.85, IdReportCL);
                //
                var list134 = calc.GetPromediosPsicoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list134, 95.59, IdReportCL);
                //
                var list135 = calc.GetPromediosFactorSocialEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list135, 96.32, IdReportCL);
                //
                var list136 = calc.GetPromediosComunicacionEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list136, 97.06, IdReportCL);
                //
                var list137 = calc.GetPromediosEnpowerEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list137, 97.79, IdReportCL);
                //
                var list138 = calc.GetPromediosCoordinacionEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list138, 98.53, IdReportCL);
                //
                var list139 = calc.GetPromediosVisionEstrateEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list139, 99.26, IdReportCL);
                //
                var list140 = calc.GetPromediosNivelDesempeñoEstrateEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list140, 100, IdReportCL);

            }
            else if (existe.Exist == true && StringAvance == "100")
            {
                //Nada
            }
            return View();
        }

        //Enfoque empresa
        
        [ValidateInput(false)]
        public ActionResult InitLoadEA(ML.ReporteD4U reporte, string tableHTML)
        {
            //Validar si el reporte ya existe.
            //Tomar su avance y empezar de ahi
            string UnidadNegocioValida = reporte.ListFiltros[0];
            string tablaHTMLValida = tableHTML;
            double avance = 0;
            string StringAvance = Convert.ToString(avance);
            var existe = BL.Reporte.ExisteReportCL(UnidadNegocioValida, tablaHTMLValida, "Enfoque Area");
            //Si el progreso es 100 ya no lo hagas
            if (existe.Exist == true && StringAvance != "100")
            {
                int IdReportCL = existe.idEncuestaAlta;
                avance = existe.AvanceDouble;
                StringAvance = Convert.ToString(avance);
                CalcClimController calc = new CalcClimController();


                if (avance < 0.5) { goto Uno; }
                if (avance == 0.74) { goto Dos; }
                if (avance == 1.47) { goto Tres; }
                if (avance == 2.21) { goto Cuatro; }
                if (avance == 2.94) { goto Cinco; }
                if (avance == 3.68) { goto Seis; }
                if (avance == 4.41) { goto Siete; }
                if (avance == 5.15) { goto Ocho; }
                if (avance == 5.88) { goto Nueve; }
                if (avance == 6.62) { goto Diez; }
                if (avance == 7.35) { goto Once; }
                if (avance == 8.09) { goto Doce; }
                if (avance == 8.82) { goto Trece; }
                if (avance == 9.56) { goto Catorce; }
                if (avance == 10.29) { goto Quince; }
                if (avance == 11.03) { goto Dieciseis; }
                if (avance == 11.76) { goto Diecisiete; }
                if (avance == 12.50) { goto Dieciocho; }
                if (avance == 13.24) { goto Diecinueve; }
                if (avance == 13.97) { goto Veinte; }
                if (avance == 14.71) { goto Veintiuno; }
                if (avance == 15.44) { goto Veintidos; }
                if (avance == 16.18) { goto VeintiTres; }
                if (avance == 16.91) { goto Veinticuatro; }
                if (avance == 17.65) { goto Veinticinco; }
                if (avance == 18.38) { goto Ventiseis; }
                if (avance == 19.12) { goto Veintisiete; }
                if (avance == 19.85) { goto Veintiocho; }
                if (avance == 20.59) { goto VeintiNueve; }
                if (avance == 21.32) { goto Treinta; }
                if (avance == 22.06) { goto TrintayUno; }
                if (avance == 22.79) { goto Treintaydos; }
                if (avance == 23.53) { goto trintaytres; }
                if (avance == 24.26) { goto trintay4; }
                if (avance == 25.00) { goto trintay5; }
                if (avance == 25.74) { goto treintay6; }
                if (avance == 26.47) { goto trintay7; }
                if (avance == 27.21) { goto treintay8; }
                if (avance == 27.94) { goto treintay9; }
                if (avance == 28.68) { goto cuarenta; }
                if (avance == 29.41) { goto cuarentay1; }
                if (avance == 30.15) { goto cuarentay2; }
                if (avance == 30.88) { goto cuarentay3; }
                if (avance == 31.62) { goto cuarentay4; }
                if (avance == 32.35) { goto cuarentay5; }
                if (avance == 33.09) { goto cuarentay6; }
                if (avance == 33.82) { goto cuarentay7; }
                if (avance == 34.56) { goto cuarentay8; }
                if (avance == 35.29) { goto cuarentay9; }
                if (avance == 36.03) { goto cincuenta; }
                if (avance == 36.76) { goto cincuentay1; }
                if (avance == 37.50) { goto cincuentay2; }
                if (avance == 38.24) { goto cincuentay3; }
                if (avance == 38.97) { goto cuncuentay4; }
                if (avance == 39.71) { goto cincuentay5; }
                if (avance == 40.44) { goto cincuentay6; }
                if (avance == 41.18) { goto cincuentay7; }
                if (avance == 41.91) { goto cincuentay8; }
                if (avance == 42.65) { goto cincuentay9; }
                if (avance == 43.38) { goto sesenta; }
                if (avance == 44.12) { goto sesentay1; }
                if (avance == 44.85) { goto sesentay2; }
                if (avance == 45.59) { goto sesentay3; }
                if (avance == 46.32) { goto sesentay4; }
                if (avance == 47.06) { goto sesentay5; }
                if (avance == 47.79) { goto sesentay6; }
                if (avance == 48.53) { goto sesentay7; }
                if (avance == 49.26) { goto sesentay8; }
                if (avance == 50.00) { goto sesentay9; }
                if (avance == 50.74) { goto setenta; }
                if (avance == 51.47) { goto setentay1; }
                if (avance == 52.21) { goto setentay2; }
                if (avance == 52.94) { goto setentay3; }
                if (avance == 53.68) { goto setentay4; }
                if (avance == 54.41) { goto setentay5; }
                if (avance == 55.15) { goto setentay6; }
                if (avance == 55.88) { goto setentay7; }
                if (avance == 56.62) { goto setentay8; }
                if (avance == 57.35) { goto setentay9; }
                if (avance == 58.09) { goto ochenta; }
                if (avance == 58.82) { goto ochentay1; }
                if (avance == 59.56) { goto ochentay2; }
                if (avance == 60.29) { goto ochentay3; }
                if (avance == 61.03) { goto ochentay4; }
                if (avance == 61.76) { goto ochentay5; }
                if (avance == 62.50) { goto ochentay6; }
                if (avance == 63.24) { goto ochentay7; }
                if (avance == 63.97) { goto ochentay8; }
                if (avance == 64.71) { goto ochentay9; }
                if (avance == 65.44) { goto noventa; }
                if (avance == 66.18) { goto noventay1; }
                if (avance == 66.91) { goto noventay2; }
                if (avance == 67.65) { goto noventay3; }
                if (avance == 68.38) { goto noventay4; }
                if (avance == 69.12) { goto noventay5; }
                if (avance == 69.85) { goto noventay6; }
                if (avance == 70.59) { goto noventay7; }
                if (avance == 71.32) { goto noventay8; }
                if (avance == 72.06) { goto noventay9; }
                if (avance == 72.79) { goto cien; }
                if (avance == 73.53) { goto ciento1; }
                if (avance == 74.26) { goto ciento2; }
                if (avance == 75.00) { goto ciento3; }
                if (avance == 75.74) { goto ciento4; }
                if (avance == 76.47) { goto ciento5; }
                if (avance == 77.21) { goto ciento6; }
                if (avance == 77.94) { goto ciento7; }
                if (avance == 78.68) { goto ciento8; }
                if (avance == 79.41) { goto ciento9; }
                if (avance == 80.15) { goto ciento10; }
                if (avance == 80.88) { goto ciento11; }
                if (avance == 81.62) { goto ciento12; }
                if (avance == 82.35) { goto ciento13; }
                if (avance == 83.09) { goto ciento14; }
                if (avance == 83.82) { goto ciento15; }
                if (avance == 84.56) { goto ciento16; }
                if (avance == 85.29) { goto ciento17; }
                if (avance == 86.03) { goto ciento18; }
                if (avance == 86.76) { goto ciento19; }
                if (avance == 87.50) { goto ciento20; }
                if (avance == 88.24) { goto ciento21; }
                if (avance == 88.97) { goto ciento22; }
                if (avance == 89.71) { goto ciento23; }
                if (avance == 90.44) { goto ciento24; }
                if (avance == 91.18) { goto ciento25; }
                if (avance == 91.91) { goto ciento26; }
                if (avance == 92.65) { goto ciento27; }
                if (avance == 93.38) { goto ciento28; }
                if (avance == 94.12) { goto ciento29; }
                if (avance == 94.85) { goto ciento30; }
                if (avance == 95.59) { goto ciento31; }
                if (avance == 96.32) { goto ciento32; }
                if (avance == 97.06) { goto ciento33; }
                if (avance == 97.79) { goto ciento34; }
                if (avance == 98.53) { goto ciento35; }
                if (avance == 99.26) { goto ciento36; }
                if (avance == 100.00) { goto ciento37; }
            Uno:
                var list1 = calc.GetEsperadas(reporte);
                BL.Reporte.SaveList(list1, 0.74, IdReportCL);
            //
            Dos:
                var list2 = calc.GetContestadas(reporte);
                BL.Reporte.SaveList(list2, 1.47, IdReportCL);

            //Custom with numberQuestions
            //
            Tres:
                reporte.IdPregunta = 88;
                var list3 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list3, 2.21, IdReportCL);
            //
            Cuatro:
                reporte.IdPregunta = 89;
                var list4 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list4, 2.94, IdReportCL);
            //
            Cinco:
                reporte.IdPregunta = 90;
                var list5 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list5, 3.68, IdReportCL);
            //
            Seis:
                reporte.IdPregunta = 87;
                var list6 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list6, 4.41, IdReportCL);
            //
            Siete:
                reporte.IdPregunta = 96;
                var list7 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list7, 5.15, IdReportCL);
            //
            Ocho:
                reporte.IdPregunta = 95;
                var list8 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list8, 5.88, IdReportCL);
            //
            Nueve:
                reporte.IdPregunta = 97;
                var list9 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list9, 6.62, IdReportCL);
            //
            Diez:
                reporte.IdPregunta = 91;
                var list10 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list10, 7.35, IdReportCL);
            //
            Once:
                reporte.IdPregunta = 100;
                var list11 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list11, 8.09, IdReportCL);
            //
            Doce:
                reporte.IdPregunta = 98;
                var list12 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list12, 8.82, IdReportCL);
            //
            Trece:
                reporte.IdPregunta = 93;
                var list13 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list13, 9.56, IdReportCL);
            //
            Catorce:
                reporte.IdPregunta = 94;
                var list14 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list14, 10.29, IdReportCL);
            //
            Quince:
                reporte.IdPregunta = 92;
                var list15 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list15, 11.03, IdReportCL);
            //
            Dieciseis:
                reporte.IdPregunta = 116;
                var list16 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list16, 11.76, IdReportCL);
            //
            Diecisiete:
                reporte.IdPregunta = 114;
                var list17 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list17, 12.50, IdReportCL);
            //
            Dieciocho:
                reporte.IdPregunta = 119;
                var list18 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list18, 13.24, IdReportCL);
            //
            Diecinueve:
                reporte.IdPregunta = 121;
                var list19 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list19, 13.97, IdReportCL);
            //
            Veinte:
                reporte.IdPregunta = 118;
                var list20 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list20, 14.71, IdReportCL);
            //
            Veintiuno:
                reporte.IdPregunta = 122;
                var list21 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list21, 15.44, IdReportCL);
            //****************************************************************
            Veintidos:
                reporte.IdPregunta = 123;
                var list22 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list22, 16.18, IdReportCL);
            //
            VeintiTres:
                reporte.IdPregunta = 120;
                var list23 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list23, 16.91, IdReportCL);
            //
            Veinticuatro:
                reporte.IdPregunta = 124;
                var list24 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list24, 17.65, IdReportCL);
            //
            Veinticinco:
                reporte.IdPregunta = 125;
                var list25 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list25, 18.38, IdReportCL);
            //
            Ventiseis:
                reporte.IdPregunta = 117;
                var list26 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list26, 19.12, IdReportCL);
            //
            Veintisiete:
                reporte.IdPregunta = 129;
                var list27 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list27, 19.85, IdReportCL);
            //
            Veintiocho:
                reporte.IdPregunta = 127;
                var list28 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list28, 20.59, IdReportCL);
            //
            VeintiNueve:
                reporte.IdPregunta = 128;
                var list29 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list29, 21.32, IdReportCL);
            //
            Treinta:
                reporte.IdPregunta = 132;
                var list30 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list30, 22.06, IdReportCL);
            //
            TrintayUno:
                reporte.IdPregunta = 131;
                var list31 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list31, 22.79, IdReportCL);
            //
            Treintaydos:
                reporte.IdPregunta = 130;
                var list32 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list32, 23.53, IdReportCL);
            //
            trintaytres:
                reporte.IdPregunta = 126;
                var list33 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list33, 24.26, IdReportCL);
            //
            trintay4:
                reporte.IdPregunta = 108;
                var list34 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list34, 25, IdReportCL);
            //
            trintay5:
                reporte.IdPregunta = 113;
                var list35 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list35, 25.74, IdReportCL);
            //
            treintay6:
                reporte.IdPregunta = 133;
                var list36 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list36, 26.47, IdReportCL);
            //
            trintay7:
                reporte.IdPregunta = 135;
                var list37 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list37, 27.21, IdReportCL);
            //
            treintay8:
                reporte.IdPregunta = 144;
                var list38 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list38, 27.94, IdReportCL);
            //
            treintay9:
                reporte.IdPregunta = 145;
                var list39 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list39, 28.68, IdReportCL);
            //
            cuarenta:
                reporte.IdPregunta = 137;
                var list40 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list40, 29.41, IdReportCL);
            //
            cuarentay1:
                reporte.IdPregunta = 143;
                var list41 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list41, 30.15, IdReportCL);
            //
            cuarentay2:
                reporte.IdPregunta = 140;
                var list42 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list42, 30.88, IdReportCL);
            //
            cuarentay3:
                reporte.IdPregunta = 136;
                var list43 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list43, 31.62, IdReportCL);
            //
            cuarentay4:
                reporte.IdPregunta = 141;
                var list44 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list44, 32.35, IdReportCL);
            //
            cuarentay5:
                reporte.IdPregunta = 142;
                var list45 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list45, 33.09, IdReportCL);
            //
            cuarentay6:
                reporte.IdPregunta = 139;
                var list46 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list46, 33.82, IdReportCL);
            //
            cuarentay7:
                reporte.IdPregunta = 102;
                var list47 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list47, 34.56, IdReportCL);
            //
            cuarentay8:
                reporte.IdPregunta = 107;
                var list48 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list48, 35.29, IdReportCL);
            //
            cuarentay9:
                reporte.IdPregunta = 101;
                var list49 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list49, 36.03, IdReportCL);
            //
            cincuenta:
                reporte.IdPregunta = 103;
                var list50 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list50, 36.76, IdReportCL);
            //
            cincuentay1:
                reporte.IdPregunta = 104;
                var list51 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list51, 37.50, IdReportCL);
            //
            cincuentay2:
                reporte.IdPregunta = 105;
                var list52 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list52, 38.24, IdReportCL);
            //
            cincuentay3:
                reporte.IdPregunta = 134;
                var list53 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list53, 38.97, IdReportCL);
            //
            cuncuentay4:
                reporte.IdPregunta = 109;
                var list54 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list54, 39.71, IdReportCL);
            //
            cincuentay5:
                reporte.IdPregunta = 112;
                var list55 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list55, 40.44, IdReportCL);
            //
            cincuentay6:
                reporte.IdPregunta = 138;
                var list56 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list56, 41.18, IdReportCL);
            //
            cincuentay7:
                reporte.IdPregunta = 106;
                var list57 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list57, 41.91, IdReportCL);
            //
            cincuentay8:
                reporte.IdPregunta = 111;
                var list58 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list58, 42.65, IdReportCL);
            //
            cincuentay9:
                reporte.IdPregunta = 146;
                var list59 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list59, 43.38, IdReportCL);
            //
            sesenta:
                reporte.IdPregunta = 147;
                var list60 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list60, 44.12, IdReportCL);
            //
            sesentay1:
                reporte.IdPregunta = 148;
                var list61 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list61, 44.85, IdReportCL);
            //
            sesentay2:
                reporte.IdPregunta = 149;
                var list62 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list62, 45.59, IdReportCL);
            //
            sesentay3:
                reporte.IdPregunta = 150;
                var list63 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list63, 46.32, IdReportCL);
            //
            sesentay4:
                reporte.IdPregunta = 151;
                var list64 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list64, 47.06, IdReportCL);
            //
            sesentay5:
                reporte.IdPregunta = 152;
                var list65 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list65, 47.79, IdReportCL);
            //
            sesentay6:
                reporte.IdPregunta = 99;
                var list66 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list66, 48.53, IdReportCL);
            //
            sesentay7:
                reporte.IdPregunta = 110;
                var list67 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list67, 49.26, IdReportCL);
            //
            sesentay8:
                reporte.IdPregunta = 115;
                var list68 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list68, 50, IdReportCL);
            //
            sesentay9:
                reporte.IdPregunta = 153;
                var list69 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list69, 50.74, IdReportCL);
            //
            setenta:
                reporte.IdPregunta = 157;
                var list70 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list70, 51.47, IdReportCL);
            //
            setentay1:
                reporte.IdPregunta = 161;
                var list71 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list71, 52.21, IdReportCL);
            //
            setentay2:
                reporte.IdPregunta = 170;
                var list72 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list72, 52.94, IdReportCL);
            //
            setentay3:
                reporte.IdPregunta = 165;
                var list73 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list73, 53.68, IdReportCL);
            //
            setentay4:
                reporte.IdPregunta = 167;
                var list74 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list74, 54.41, IdReportCL);
            //
            setentay5:
                reporte.IdPregunta = 169;
                var list75 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list75, 55.15, IdReportCL);
            //
            setentay6:
                reporte.IdPregunta = 172;
                var list76 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list76, 55.88, IdReportCL);
            //
            setentay7:
                reporte.IdPregunta = 155;
                var list77 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list77, 56.62, IdReportCL);
            //
            setentay8:
                reporte.IdPregunta = 159;
                var list78 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list78, 57.35, IdReportCL);
            //
            setentay9:
                reporte.IdPregunta = 163;
                var list79 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list79, 58.09, IdReportCL);
            //
            ochenta:
                reporte.IdPregunta = 166;
                var list80 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list80, 58.82, IdReportCL);
            //
            ochentay1:
                reporte.IdPregunta = 154;
                var list81 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list81, 59.56, IdReportCL);
            //
            ochentay2:
                reporte.IdPregunta = 156;
                var list82 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list82, 60.29, IdReportCL);
            //
            ochentay3:
                reporte.IdPregunta = 158;
                var list83 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list83, 61.03, IdReportCL);
            //
            ochentay4:
                reporte.IdPregunta = 160;
                var list84 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list84, 61.76, IdReportCL);
            //
            ochentay5:
                reporte.IdPregunta = 162;
                var list85 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list85, 62.50, IdReportCL);
            //
            ochentay6:
                reporte.IdPregunta = 164;
                var list86 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list86, 63.24, IdReportCL);
            //
            ochentay7:
                reporte.IdPregunta = 168;
                var list87 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list87, 63.97, IdReportCL);
            //
            ochentay8:
                reporte.IdPregunta = 171;
                var list88 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list88, 64.71, IdReportCL);
            /*
             Fin de promedios de preguntas individuales
             Inicio de promedios por categorias
             */
            //ochentay9:
            //var list89 = calc.GetValueComodinEnfoqueArea(reporte);
            //BL.Reporte.SaveList(list89, 63.67, IdReportCL);
            //
            ochentay9:
                var list90 = calc.GetPorcentajeParticipacionEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list90, 65.44, IdReportCL);
            //
            noventa:
                var list91 = calc.GetPromediosCreedibilidadEnfoqueArea(reporte);
                BL.Reporte.SaveList(list91, 66.18, IdReportCL);
            //
            noventay1:
                var list92 = calc.GetPromediosImparcialidadEnfoqueArea(reporte);
                BL.Reporte.SaveList(list92, 66.91, IdReportCL);
            //
            noventay2:
                var list93 = calc.GetPromediosOrgulloEnfoqueArea(reporte);
                BL.Reporte.SaveList(list93, 67.65, IdReportCL);
            //
            noventay3:
                var list94 = calc.GetPromediosRespetoEnfoqueArea(reporte);
                BL.Reporte.SaveList(list94, 68.38, IdReportCL);
            //
            noventay4:
                var list95 = calc.GetPromediosCompañerismoEnfoqueArea(reporte);
                BL.Reporte.SaveList(list95, 69.12, IdReportCL);
            //
            noventay5:
                var list96 = calc.GetPromediosCoachingEnfoqueArea(reporte);
                BL.Reporte.SaveList(list96, 69.85, IdReportCL);
            //
            noventay6:
                var list97 = calc.GetPromediosCambioEnfoqueArea(reporte);
                BL.Reporte.SaveList(list97, 70.59, IdReportCL);
            //
            noventay7:
                var list98 = calc.GetPromediosBienestarEnfoqueArea(reporte);
                BL.Reporte.SaveList(list98, 71.32, IdReportCL);
            //
            noventay8:
                var list99 = calc.GetPromediosAlinCulturalEnfoqueArea(reporte);
                BL.Reporte.SaveList(list99, 72.06, IdReportCL);
            //
            noventay9:
                var list100 = calc.GetPromediosGestaltEnfoqueArea(reporte);
                BL.Reporte.SaveList(list100, 72.79, IdReportCL);
            //
            //ciento1:
            //var list101 = calc.GetPromediosConfianzaEnfoqueArea(reporte);
            //BL.Reporte.SaveList(list101, 72.20, IdReportCL);
            //
            cien:
                var list102 = calc.GetPromedios66ReactivosEnfoqueArea(reporte);
                BL.Reporte.SaveList(list102, 73.53, IdReportCL);
            //
            ciento1:
                var list103 = calc.GetPromedios86ReactivosEnfoqueArea(reporte);
                BL.Reporte.SaveList(list103, 74.26, IdReportCL);
            //
            //ciento4:
            //var list104 = calc.GetPromediosPracticasCulturealesEnfoqueArea(reporte);
            //BL.Reporte.SaveList(list104, 74.41, IdReportCL);
            //
            ciento2:
                var list105 = calc.GetPromediosReclutandoBienvenidaEnfoqueArea(reporte);
                BL.Reporte.SaveList(list105, 75, IdReportCL);
            //
            ciento3:
                var list106 = calc.GetPromediosInspirandoEnfoqueArea(reporte);
                BL.Reporte.SaveList(list106, 75.74, IdReportCL);
            //
            ciento4:
                var list107 = calc.GetPromediosHablandoEnfoqueArea(reporte);
                BL.Reporte.SaveList(list107, 76.47, IdReportCL);
            //
            ciento5:
                var list108 = calc.GetPromediosEscuchandoEnfoqueArea(reporte);
                BL.Reporte.SaveList(list108, 77.21, IdReportCL);
            //
            ciento6:
                var list109 = calc.GetPromediosAgradeciendoEnfoqueArea(reporte);
                BL.Reporte.SaveList(list109, 77.94, IdReportCL);
            //
            ciento7:
                var list110 = calc.GetPromediosDesarrollandoEnfoqueArea(reporte);
                BL.Reporte.SaveList(list110, 78.68, IdReportCL);
            //
            ciento8:
                var list111 = calc.GetPromediosCuidandoEnfoqueArea(reporte);
                BL.Reporte.SaveList(list111, 79.41, IdReportCL);
            //
            ciento9:
                var list112 = calc.GetPromediosPercepcionLugarEnfoqueArea(reporte);
                BL.Reporte.SaveList(list112, 80.15, IdReportCL);
            //
            ciento10:
                var list113 = calc.GetPromediosCooperandoEnfoqueArea(reporte);
                BL.Reporte.SaveList(list113, 80.88, IdReportCL);
            //
            ciento11:
                var list114 = calc.GetPromediosAlineacionEstrategicaEnfoqueArea(reporte);
                BL.Reporte.SaveList(list114, 81.62, IdReportCL);
            //
            //ciento15:
            //var list115 = calc.GetPromediosProcesosOrganizacionalesEnfoqueArea(reporte);
            //BL.Reporte.SaveList(list115, 82.20, IdReportCL);
            //
            ciento12:
                reporte.IdPregunta = 111;
                var list116 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list116, 82.35, IdReportCL);
            //
            ciento13:
                reporte.IdPregunta = 92;
                var list117 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list117, 83.09, IdReportCL);
            //
            ciento14:
                var list118 = calc.GetPromediosCarreraYPromocionPersEnfoqueArea(reporte);
                BL.Reporte.SaveList(list118, 83.82, IdReportCL);
            //
            ciento15:
                var list119 = calc.GetPromediosCapacitacionYDesarrolloEnfoqueArea(reporte);
                BL.Reporte.SaveList(list119, 84.56, IdReportCL);
            //
            ciento16:
                var list120 = calc.GetPromediosEMPOWERMENTEnfoqueArea(reporte);
                BL.Reporte.SaveList(list120, 85.29, IdReportCL);
            //
            ciento17:
                var list121 = calc.GetPromediosEvalDesempeñoEnfoqueArea(reporte);
                BL.Reporte.SaveList(list121, 86.03, IdReportCL);
            //
            ciento18:
                reporte.IdPregunta = 114;
                var list122 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list122, 86.76, IdReportCL);
            //
            ciento19:
                reporte.IdPregunta = 116;
                var list123 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list123, 87.50, IdReportCL);
            //
            ciento20:
                var list124 = calc.GetPromediosIntegracionEnfoqueArea(reporte);
                BL.Reporte.SaveList(list124, 88.24, IdReportCL);
            //
            ciento21:
                reporte.IdPregunta = 125;
                var list125 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list125, 88.97, IdReportCL);
            //
            ciento22:
                var list126 = calc.GetPromediosNivelCoolaboracionEnfoqueArea(reporte);
                BL.Reporte.SaveList(list126, 89.71, IdReportCL);
            //
            ciento23:
                var list127 = calc.GetPromediosNivelCompromisoEnfoqueArea(reporte);
                BL.Reporte.SaveList(list127, 90.44, IdReportCL);
            //
            ciento24:
                var list128 = calc.GetPromediosFactorSocialEnfoqueArea(reporte);
                BL.Reporte.SaveList(list128, 91.18, IdReportCL);
            //
            ciento25:
                var list129 = calc.GetPromediosFactorPsicoEnfoqueArea(reporte);
                BL.Reporte.SaveList(list129, 91.91, IdReportCL);
            //
            ciento26:
                var list130 = calc.GetPromediosFactorFisicoEnfoqueArea(reporte);
                BL.Reporte.SaveList(list130, 92.65, IdReportCL);
            //
            ciento27:
                var list131 = calc.GetPromediosAlinCulturalEnfoqueArea(reporte);
                BL.Reporte.SaveList(list131, 93.38, IdReportCL);
            //
            ciento28:
                var list132 = calc.GetPromediosBienestarEnfoqueArea(reporte);
                BL.Reporte.SaveList(list132, 94.12, IdReportCL);
            //
            ciento29:
                var list133 = calc.GetPromediosBioEnfoqueArea(reporte);
                BL.Reporte.SaveList(list133, 94.85, IdReportCL);
            //
            ciento30:
                var list134 = calc.GetPromediosPsicoEnfoqueArea(reporte);
                BL.Reporte.SaveList(list134, 95.59, IdReportCL);
            //
            ciento31:
                var list135 = calc.GetPromediosFactorSocialEnfoqueArea(reporte);
                BL.Reporte.SaveList(list135, 96.32, IdReportCL);
            //
            ciento32:
                var list136 = calc.GetPromediosComunicacionEnfoqueArea(reporte);
                BL.Reporte.SaveList(list136, 97.06, IdReportCL);
            //
            ciento33:
                var list137 = calc.GetPromediosEnpowerEnfoqueArea(reporte);
                BL.Reporte.SaveList(list137, 97.79, IdReportCL);
            //
            ciento34:
                var list138 = calc.GetPromediosCoordinacionEnfoqueArea(reporte);
                BL.Reporte.SaveList(list138, 98.53, IdReportCL);
            //e
            ciento35:
                var list139 = calc.GetPromediosVisionEstrateEnfoqueArea(reporte);
                BL.Reporte.SaveList(list139, 99.26, IdReportCL);
            //
            ciento36:
                var list140 = calc.GetPromediosNivelDesempeñoEstrateEnfoqueArea(reporte);
                BL.Reporte.SaveList(list140, 100, IdReportCL);
            ciento37:
                Console.WriteLine("");
            }
            else if (existe.Exist == false)//JAMG Ajustar porcentajes
            {
                var ressult = BL.Reporte.AddGenerarClimaLabEA(reporte, tableHTML);
                int IdReportCL = Convert.ToInt32(ressult.Object);
                CalcClimController calc = new CalcClimController();

                //var list1 = calc.GetEsperadas(reporte);
                //BL.Reporte.SaveList(list1, 0.74, IdReportCL);
                //var list2 = calc.GetContestadas(reporte);
                //BL.Reporte.SaveList(list2, 1.47, IdReportCL);
                var list1 = calc.GetEsperadas(reporte);
                BL.Reporte.SaveList(list1, 0.74, IdReportCL);
            //
                var list2 = calc.GetContestadas(reporte);
                BL.Reporte.SaveList(list2, 1.47, IdReportCL);

            //Custom with numberQuestions
            //
                reporte.IdPregunta = 2;
                var list3 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list3, 2.21, IdReportCL);
            //
                reporte.IdPregunta = 3;
                var list4 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list4, 2.94, IdReportCL);
            //
                reporte.IdPregunta = 4;
                var list5 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list5, 3.68, IdReportCL);
            //
                reporte.IdPregunta = 1;
                var list6 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list6, 4.41, IdReportCL);
            //
                reporte.IdPregunta = 10;
                var list7 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list7, 5.15, IdReportCL);
            //
                reporte.IdPregunta = 9;
                var list8 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list8, 5.88, IdReportCL);
            //
                reporte.IdPregunta = 11;
                var list9 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list9, 6.62, IdReportCL);
            //
                reporte.IdPregunta = 5;
                var list10 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list10, 7.35, IdReportCL);
            //
                reporte.IdPregunta = 14;
                var list11 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list11, 8.09, IdReportCL);
            //
                reporte.IdPregunta = 12;
                var list12 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list12, 8.82, IdReportCL);
            //
                reporte.IdPregunta = 7;
                var list13 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list13, 9.56, IdReportCL);
            //
                reporte.IdPregunta = 8;
                var list14 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list14, 10.29, IdReportCL);
            //
                reporte.IdPregunta = 6;
                var list15 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list15, 11.03, IdReportCL);
            //
                reporte.IdPregunta = 30;
                var list16 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list16, 11.76, IdReportCL);
            //
                reporte.IdPregunta = 28;
                var list17 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list17, 12.50, IdReportCL);
            //
                reporte.IdPregunta = 33;
                var list18 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list18, 13.24, IdReportCL);
            //
                reporte.IdPregunta = 35;
                var list19 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list19, 13.97, IdReportCL);
            //
                reporte.IdPregunta = 32;
                var list20 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list20, 14.71, IdReportCL);
            //
                reporte.IdPregunta = 36;
                var list21 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list21, 15.44, IdReportCL);
            //****************************************************************
                reporte.IdPregunta = 37;
                var list22 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list22, 16.18, IdReportCL);
            //
                reporte.IdPregunta = 34;
                var list23 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list23, 16.91, IdReportCL);
            //
                reporte.IdPregunta = 38;
                var list24 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list24, 17.65, IdReportCL);
            //
                reporte.IdPregunta = 39;
                var list25 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list25, 18.38, IdReportCL);
            //
                reporte.IdPregunta = 31;
                var list26 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list26, 19.12, IdReportCL);
            //
                reporte.IdPregunta = 43;
                var list27 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list27, 19.85, IdReportCL);
            //
                reporte.IdPregunta = 41;
                var list28 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list28, 20.59, IdReportCL);
            //
                reporte.IdPregunta = 42;
                var list29 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list29, 21.32, IdReportCL);
            //
                reporte.IdPregunta = 46;
                var list30 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list30, 22.06, IdReportCL);
            //
                reporte.IdPregunta = 45;
                var list31 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list31, 22.79, IdReportCL);
            //
                reporte.IdPregunta = 44;
                var list32 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list32, 23.53, IdReportCL);
            //
                reporte.IdPregunta = 40;
                var list33 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list33, 24.26, IdReportCL);
            //
                reporte.IdPregunta = 22;
                var list34 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list34, 25, IdReportCL);
            //
                reporte.IdPregunta = 27;
                var list35 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list35, 25.74, IdReportCL);
            //
                reporte.IdPregunta = 47;
                var list36 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list36, 26.47, IdReportCL);
            //
                reporte.IdPregunta = 49;
                var list37 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list37, 27.21, IdReportCL);
            //
                reporte.IdPregunta = 58;
                var list38 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list38, 27.94, IdReportCL);
            //
                reporte.IdPregunta = 59;
                var list39 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list39, 28.68, IdReportCL);
            //
                reporte.IdPregunta = 51;
                var list40 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list40, 29.41, IdReportCL);
            //
                reporte.IdPregunta = 57;
                var list41 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list41, 30.15, IdReportCL);
            //
                reporte.IdPregunta = 54;
                var list42 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list42, 30.88, IdReportCL);
            //
                reporte.IdPregunta = 50;
                var list43 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list43, 31.62, IdReportCL);
            //
                reporte.IdPregunta = 55;
                var list44 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list44, 32.35, IdReportCL);
            //
                reporte.IdPregunta = 56;
                var list45 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list45, 33.09, IdReportCL);
            //
                reporte.IdPregunta = 53;
                var list46 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list46, 33.82, IdReportCL);
            //
                reporte.IdPregunta = 16;
                var list47 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list47, 34.56, IdReportCL);
            //
                reporte.IdPregunta = 21;
                var list48 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list48, 35.29, IdReportCL);
            //
                reporte.IdPregunta = 15;
                var list49 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list49, 36.03, IdReportCL);
            //
                reporte.IdPregunta = 17;
                var list50 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list50, 36.76, IdReportCL);
            //
                reporte.IdPregunta = 18;
                var list51 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list51, 37.50, IdReportCL);
            //
                reporte.IdPregunta = 19;
                var list52 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list52, 38.24, IdReportCL);
            //
                reporte.IdPregunta = 48;
                var list53 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list53, 38.97, IdReportCL);
            //
                reporte.IdPregunta = 23;
                var list54 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list54, 39.71, IdReportCL);
            //
                reporte.IdPregunta = 26;
                var list55 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list55, 40.44, IdReportCL);
            //
                reporte.IdPregunta = 52;
                var list56 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list56, 41.18, IdReportCL);
            //
                reporte.IdPregunta = 20;
                var list57 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list57, 41.91, IdReportCL);
            //
                reporte.IdPregunta = 25;
                var list58 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list58, 42.65, IdReportCL);
            //
                reporte.IdPregunta = 60;
                var list59 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list59, 43.38, IdReportCL);
            //
            sesenta:
                reporte.IdPregunta = 61;
                var list60 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list60, 44.12, IdReportCL);
                //
                reporte.IdPregunta = 62;
                var list61 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list61, 44.85, IdReportCL);
                //
                reporte.IdPregunta = 63;
                var list62 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list62, 45.59, IdReportCL);
                //
                reporte.IdPregunta = 64;
                var list63 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list63, 46.32, IdReportCL);
                //
                reporte.IdPregunta = 65;
                var list64 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list64, 47.06, IdReportCL);
                //
                reporte.IdPregunta = 66;
                var list65 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list65, 47.79, IdReportCL);
                //
                reporte.IdPregunta = 13;
                var list66 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list66, 48.53, IdReportCL);
                //
                reporte.IdPregunta = 24;
                var list67 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list67, 49.26, IdReportCL);
                //
                reporte.IdPregunta = 29;
                var list68 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list68, 50, IdReportCL);
                //
                reporte.IdPregunta = 67;
                var list69 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list69, 50.74, IdReportCL);
                //
                reporte.IdPregunta = 71;
                var list70 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list70, 51.47, IdReportCL);
                //
                reporte.IdPregunta = 75;
                var list71 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list71, 52.21, IdReportCL);
                //
                reporte.IdPregunta = 84;
                var list72 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list72, 52.94, IdReportCL);
                //
                reporte.IdPregunta = 79;
                var list73 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list73, 53.68, IdReportCL);
                //
                reporte.IdPregunta = 81;
                var list74 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list74, 54.41, IdReportCL);
                //
                reporte.IdPregunta = 83;
                var list75 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list75, 55.15, IdReportCL);
                //
                reporte.IdPregunta = 86;
                var list76 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list76, 55.88, IdReportCL);
                //
                reporte.IdPregunta = 69;
                var list77 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list77, 56.62, IdReportCL);
                //
                reporte.IdPregunta = 73;
                var list78 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list78, 57.35, IdReportCL);
                //
                reporte.IdPregunta = 77;
                var list79 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list79, 58.09, IdReportCL);
                //
                reporte.IdPregunta = 80;
                var list80 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list80, 58.82, IdReportCL);
                //
                reporte.IdPregunta = 68;
                var list81 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list81, 59.56, IdReportCL);
                //
                reporte.IdPregunta = 70;
                var list82 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list82, 60.29, IdReportCL);
                //
                reporte.IdPregunta = 72;
                var list83 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list83, 61.03, IdReportCL);
                //
                reporte.IdPregunta = 74;
                var list84 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list84, 61.76, IdReportCL);
                //
                reporte.IdPregunta = 76;
                var list85 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list85, 62.50, IdReportCL);
                //
                reporte.IdPregunta = 78;
                var list86 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list86, 63.24, IdReportCL);
                //
                reporte.IdPregunta = 82;
                var list87 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list87, 63.97, IdReportCL);
                //
                reporte.IdPregunta = 85;
                var list88 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list88, 64.71, IdReportCL);
                /*
                 Fin de promedios de preguntas individuales
                 Inicio de promedios por categorias
                 */
                //ochentay9:
                //var list89 = calc.GetValueComodinEnfoqueEmpresa(reporte);
                //BL.Reporte.SaveList(list89, 63.67, IdReportCL);
                //
                var list90 = calc.GetPorcentajeParticipacionEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list90, 65.44, IdReportCL);
                //
                var list91 = calc.GetPromediosCreedibilidadEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list91, 66.18, IdReportCL);
                //
                var list92 = calc.GetPromediosImparcialidadEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list92, 66.91, IdReportCL);
                //
                var list93 = calc.GetPromediosOrgulloEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list93, 67.65, IdReportCL);
                //
                var list94 = calc.GetPromediosRespetoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list94, 68.38, IdReportCL);
                //
                var list95 = calc.GetPromediosCompañerismoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list95, 69.12, IdReportCL);
                //
                var list96 = calc.GetPromediosCoachingEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list96, 69.85, IdReportCL);
                //
                var list97 = calc.GetPromediosCambioEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list97, 70.59, IdReportCL);
                //
                var list98 = calc.GetPromediosBienestarEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list98, 71.32, IdReportCL);
                //
                var list99 = calc.GetPromediosAlinCulturalEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list99, 72.06, IdReportCL);
                //
                var list100 = calc.GetPromediosGestaltEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list100, 72.79, IdReportCL);
                //
                //ciento1:
                //var list101 = calc.GetPromediosConfianzaEnfoqueEmpresa(reporte);
                //BL.Reporte.SaveList(list101, 72.20, IdReportCL);
                //
                var list102 = calc.GetPromedios66ReactivosEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list102, 73.53, IdReportCL);
                //
                var list103 = calc.GetPromedios86ReactivosEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list103, 74.26, IdReportCL);
                //
                var list105 = calc.GetPromediosReclutandoBienvenidaEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list105, 75, IdReportCL);
                //
                var list106 = calc.GetPromediosInspirandoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list106, 75.74, IdReportCL);
                //
                var list107 = calc.GetPromediosHablandoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list107, 76.47, IdReportCL);
                //
                var list108 = calc.GetPromediosEscuchandoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list108, 77.21, IdReportCL);
                //
                var list109 = calc.GetPromediosAgradeciendoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list109, 77.94, IdReportCL);
                //
                var list110 = calc.GetPromediosDesarrollandoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list110, 78.68, IdReportCL);
                //
                var list111 = calc.GetPromediosCuidandoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list111, 79.41, IdReportCL);
                //
                var list112 = calc.GetPromediosPercepcionLugarEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list112, 80.15, IdReportCL);
                //
                var list113 = calc.GetPromediosCooperandoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list113, 80.88, IdReportCL);
                //
                var list114 = calc.GetPromediosAlineacionEstrategicaEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list114, 81.62, IdReportCL);
                //
                //ciento15:
                //var list115 = calc.GetPromediosProcesosOrganizacionalesEnfoqueEmpresa(reporte);
                //BL.Reporte.SaveList(list115, 82.20, IdReportCL);
                //
                reporte.IdPregunta = 25;
                var list116 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list116, 82.35, IdReportCL);
                //
                reporte.IdPregunta = 6;
                var list117 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list117, 83.09, IdReportCL);
                //
                var list118 = calc.GetPromediosCarreraYPromocionPersEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list118, 83.82, IdReportCL);
                //
                var list119 = calc.GetPromediosCapacitacionYDesarrolloEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list119, 84.56, IdReportCL);
                //
                var list120 = calc.GetPromediosEMPOWERMENTEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list120, 85.29, IdReportCL);
                //
                var list121 = calc.GetPromediosEvalDesempeñoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list121, 86.03, IdReportCL);
                //
                reporte.IdPregunta = 28;
                var list122 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list122, 86.76, IdReportCL);
                //
                reporte.IdPregunta = 30;
                var list123 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list123, 87.50, IdReportCL);
                //
                var list124 = calc.GetPromediosIntegracionEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list124, 88.24, IdReportCL);
                //
                reporte.IdPregunta = 39;
                var list125 = calc.GetPorcentajeAfirmativasEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list125, 88.97, IdReportCL);
                //
                var list126 = calc.GetPromediosNivelCoolaboracionEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list126, 89.71, IdReportCL);
                //
                var list127 = calc.GetPromediosNivelCompromisoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list127, 90.44, IdReportCL);
                //
                var list128 = calc.GetPromediosFactorSocialEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list128, 91.18, IdReportCL);
                //
                var list129 = calc.GetPromediosFactorPsicoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list129, 91.91, IdReportCL);
                //
                var list130 = calc.GetPromediosFactorFisicoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list130, 92.65, IdReportCL);
                //
                var list131 = calc.GetPromediosAlinCulturalEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list131, 93.38, IdReportCL);
                //
                var list132 = calc.GetPromediosBienestarEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list132, 94.12, IdReportCL);
                //
                var list133 = calc.GetPromediosBioEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list133, 94.85, IdReportCL);
                //
                var list134 = calc.GetPromediosPsicoEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list134, 95.59, IdReportCL);
                //
                var list135 = calc.GetPromediosFactorSocialEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list135, 96.32, IdReportCL);
                //
                var list136 = calc.GetPromediosComunicacionEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list136, 97.06, IdReportCL);
                //
                var list137 = calc.GetPromediosEnpowerEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list137, 97.79, IdReportCL);
                //
                var list138 = calc.GetPromediosCoordinacionEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list138, 98.53, IdReportCL);
                //e
                var list139 = calc.GetPromediosVisionEstrateEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list139, 99.26, IdReportCL);
                //
                var list140 = calc.GetPromediosNivelDesempeñoEstrateEnfoqueEmpresa(reporte);
                BL.Reporte.SaveList(list140, 100, IdReportCL);
           
                Console.WriteLine("");
            }
            return View();
        }
        [ValidateInput(false)]
        [AutomaticRetry(Attempts = 0)]
        public ActionResult JobReporte(ML.ReporteD4U reporte)
        {
            if (reporte.Enfoque == "EE")
            {
                //save
                var ressult = BL.Reporte.AddGenerarClimaLabEE(reporte, reporte.tableHTML);
                int IdReportCL = Convert.ToInt32(ressult.Object);
                RecurringJob.AddOrUpdate(() => InitLoadEE(reporte, reporte.tableHTML), "* */2 * * *");//Cada hora
            }
            else if (reporte.Enfoque == "EA")
            {
                //save
                var ressult = BL.Reporte.AddGenerarClimaLabEA(reporte, reporte.tableHTML);
                int IdReportCL = Convert.ToInt32(ressult.Object);
                RecurringJob.AddOrUpdate(() => InitLoadEA(reporte, reporte.tableHTML), "* */2 * * *");//Cada hora
            }
            return Json("success");
        }
        public ActionResult GetReportsgeneratorsCL()
        {
            var data = BL.Reporte.getgeneratorsCL();
            return Json(data.Objects, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ContinueGeneratorReport()
        {
            return View();
        }
        public ActionResult GetTableForReport(ML.ReportCL reportCL)
        {
            var result = BL.Reporte.GetTableForReport(reportCL);
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAllDataFromReport(ML.ReportCL report)
        {
            var result = BL.Reporte.GetAllDataFromReport(report);
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public ActionResult SetTableFill(ML.ReportCL report)
        {
            var result = BL.Reporte.SetTableFill(report);
            return Json("success", JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTableFill1aMitad(ML.ReportCL report)
        {
            var result = BL.Reporte.GetTableFillPart1(report);

            var json = Json(result.Object, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            //return new JsonResult { Data = json, MaxJsonLength = Int32.MaxValue };
            return new JsonResult()
            {
                Data = json,
                MaxJsonLength = 86753090,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public ActionResult GetTableFill2aMitad(ML.ReportCL report)
        {
            var result = BL.Reporte.GetTableFillPart2(report);

            var json = Json(result.Object, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;

            return new JsonResult()
            {
                Data = json,
                MaxJsonLength = 86753090,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}