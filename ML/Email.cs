using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Email
    {
        public int EstatusEncuesta { get; set; }
        public string To { get; set; } = string.Empty;
        public string From { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string BCC { get; set; } = string.Empty;
        public string CC { get; set; } = string.Empty;
        public int Priority { get; set; }
        public MailPriority MailPriority { get; set; } = new MailPriority {  };
        public string NombreDestinatario { get; set; } = string.Empty;
        public string NombrePlanAccion { get; set; } = string.Empty;
        public string Plantilla { get; set; } = string.Empty;
        public string Frecuencia { get; set; } = string.Empty;
        public enum TipoNotificacion {
            inicial = 1,
            WeekAfter = 2
        }
        public static string AsuntoNotificacionInicial = "Has sido elegido";
        public static string AsuntoNotificacionPrevia = "Estamos a punto de iniciar";
        public static string AsuntoNotificacionAvanceInicial = "No has registrado tu avance inicial";
        public static string AsuntoNotificacionAvanceNoLogrado = "Avances esperados no logrados";
        public static string AsuntoNotificacionAgradecimiento = "Agradecimiento por participación";



        public static string PlantillaContenidoAccionesNot1 = @"
            <fieldset style='margin-bottom: 1rem;'>
                <div style='width: 100%; height: 40px; background-color: #002060; display:flex; color: white;'>
                    <div style='width: 90%;vertical-align:middle; font-weight: bold;'>
                        <span style='margin-top: 0.5rem;position: absolute;margin-left: 1rem;'>#Categoria#</span>
                    </div>
                    <div style='width: 5%;font-weight: bold;font-weight: bold;margin-top: 0.5rem;margin-left: 1rem;'>
                        #100%#
                    </div>
                    <div style='width: 5%;font-weight: bold;'>
                        <img alt='Icono promedio' src='#icono#' style='width: 45px;height: 35px;' />
                    </div>
                </div>
                <div style='width: 100%; display: flex;margin-top: 1rem;'>
                    <input disabled='' type='text' value='#accion#' style='border: 1px solid; box-sizing: border-box; margin: 0; font-family: inherit; display: block; width: 100%; padding: .375rem .75rem; font-size: 1rem; font-weight: 400; line-height: 1.5; color: #212529; background-color: #fff; background-clip: padding-box; border: 1px solid #ced4da; -webkit-appearance: none; -moz-appearance: none; appearance: none; border-radius: .25rem; transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out;' />
                </div>
                <div style='width: 100%; display: flex;margin-top: 1rem;'>
                    <div style='width: 50%; padding-right: 15px;'>
                        <label>Periodicidad</label>
                        <input disabled='' type='text' value='#periodicidad#' style='border: 1px solid; box-sizing: border-box; margin: 0; font-family: inherit; display: block; width: 100%; padding: .375rem .75rem; font-size: 1rem; font-weight: 400; line-height: 1.5; color: #212529; background-color: #fff; background-clip: padding-box; border: 1px solid #ced4da; -webkit-appearance: none; -moz-appearance: none; appearance: none; border-radius: .25rem; transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out;' />
                    </div>
                    <div style='width: 25%; margin-top: 1rem;'>
                        <img alt='Icono fecha' style='--bs-gutter-x: 1.5rem; --bs-gutter-y: 0;box-sizing: border-box; vertical-align: middle; width: 30px; height: 30px; max-width: 30px; max-height: 30px; min-width: 30px; min-height: 30px; margin-top: .5rem !important;' src='http://www.diagnostic4u.com/img/icono-calendario.png' class='fas fa-calendar' /> <span style='vertical-align: middle; box-sizing: border-box; --bs-gutter-x: 1.5rem; --bs-gutter-y: 0;'>Inicia: #inicio#</span>
                    </div>
                    <div style='width: 25%; margin-top: 1rem;'>
                        <img alt='Icono fecha' style='--bs-gutter-x: 1.5rem; --bs-gutter-y: 0;box-sizing: border-box; vertical-align: middle; width: 30px; height: 30px; max-width: 30px; max-height: 30px; min-width: 30px; min-height: 30px; margin-top: .5rem !important;' src='http://www.diagnostic4u.com/img/icono-calendario.png' class='fas fa-calendar' /> <span style='vertical-align: middle; box-sizing: border-box; --bs-gutter-x: 1.5rem; --bs-gutter-y: 0;'>Concluye: #fin#</span>
                    </div>
                </div>
                <div style='width: 100%; display:flex; margin-top: 1rem;'>
                    <div style='width: 50%; padding-right: 15px;'>
                        <label>Objetivo</label>
                        <input disabled='' type='text' value='#objetivo#' style='border: 1px solid; box-sizing: border-box; margin: 0; font-family: inherit; display: block; width: 100%; padding: .375rem .75rem; font-size: 1rem; font-weight: 400; line-height: 1.5; color: #212529; background-color: #fff; background-clip: padding-box; border: 1px solid #ced4da; -webkit-appearance: none; -moz-appearance: none; appearance: none; border-radius: .25rem; transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out;' />
                    </div>
                    <div style='width: 50%; padding-right: 15px;'>
                        <label>Meta</label>
                        <input disabled='' type='text' value='#meta#' style='border: 1px solid; box-sizing: border-box; margin: 0; font-family: inherit; display: block; width: 100%; padding: .375rem .75rem; font-size: 1rem; font-weight: 400; line-height: 1.5; color: #212529; background-color: #fff; background-clip: padding-box; border: 1px solid #ced4da; -webkit-appearance: none; -moz-appearance: none; appearance: none; border-radius: .25rem; transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out;' />
                    </div>
                </div>
            </fieldset>
        ";


        public static string PlantillaAcciones = @"
            <fieldset style='margin-bottom: 1rem;'>
                    <table style='border: solid 1px #EEEEEE;width:100%;'>
                        <tbody style=''>
                        <tr style='background-color: #002856; color: #FFFFFF; padding: 10px;'>
                            <td colspan='2' style='font-weight:bold;padding: 5px 5px 5px 10px;'>#Categoria#</td>
                            <td style='padding: 5px 5px 5px 10px;'>#100%#</td>
                            <td style='padding: 5px 5px 5px 10px;'><img src='#icono#' width='30' height='' /></td>
                        </tr>
                        <tr style='padding: 10px; border: solid 1px #EEEEEE;'>
                            <td colspan='3' style='padding: 5px 5px 5px 10px;'>#accion#</td>
                        </tr>
                        </tbody>
                    </table>
                    <p style='display:none'>Perioricidad</p>
                    <table style='border: solid 1px #EEEEEE;width:100%'>
                        <tbody style=''>
                        <tr>
                            <td colspan='2' style='width: 50%;padding: 5px 5px 5px 10px;'>Perioricidad</td>
                        </tr>
                        <tr>
                            <td colspan='2' style='width: 50%;padding: 5px 5px 5px 10px;'>#periodicidad#</td>
                            <td style='padding: 5px 5px 5px 10px;'><img src='http://www.diagnostic4u.com/img/icono-calendario.png' width='20' height='20' style='max-width: 15px; border: solid 1px #EEEEEE;'> Inicia: #inicio#</td>
                            <td style='padding: 5px 5px 5px 10px;'><img src='http://www.diagnostic4u.com/img/icono-calendario.png' width='20' height='20' style='max-width: 15px; border: solid 1px #EEEEEE;'> Concluye: #fin#</td>
                        </tr>
                        </tbody>
                    </table>
                    <table style='border: solid 1px #EEEEEE;width:100%'>
                        <tbody style=''>
                        <tr>
                            <td style='width: 50%;padding: 5px 5px 5px 10px;'><p>Objetivo</p></td>
                            <td style='width: 50%;padding: 5px 5px 5px 10px;'><p>Meta</p></td>
                        </tr>
                        <tr>
                            <td style='width: 50%; border: solid 1px #EEEEEE;padding: 5px 5px 5px 10px;'>#objetivo#</td>
                            <td style='width: 50%; border: solid 1px #EEEEEE;padding: 5px 5px 5px 10px;'>#meta#</td>
                        </tr>
                        </tbody>
                    </table>
            </fieldset>";


        public static string solIcono = "http://www.diagnostic4u.com//img/ReporteoClima/Iconos/sol-icono.png";
        public static string solNubeIcono = "http://www.diagnostic4u.com//img/ReporteoClima/Iconos/solnube-icono.png";
        public static string nubeIcono = "http://www.diagnostic4u.com//img/ReporteoClima/Iconos/nube-icono.png";
        public static string lluviaIcono = "http://www.diagnostic4u.com//img/ReporteoClima/Iconos/lluvia-icono.png";

        public string PlantillaNotificacionInicial { get; set; } = "<body><h3>Plantilla Notificación Inicial</h3><p>Este es el contenido de la plantilla, por favor revisa la o las acciones que te toca dar seguimiento</p></body>";
        public string PlantillaNotificacionPrevia { get; set; } = "<body><h3>Plantilla Notificacion Previa</h3><p>Ya casi empezamos prepara tus cositas</p></body>";
        public string PlantillaSinAvanceInicial { get; set; } = "<body><h3>Plantilla Sin Avance Inicial</h3><p>Ya empezamos con las acciones de mejora, dale gas</p></body>";
        public string PlantillaAvanceNoLogrado { get; set; } = "<body><h3>Plantilla Avance No Logrado</h3><p>:( no lograste tu avance esperado</p></body>";
        public string PlantillaNotificacionAgradecimiento { get; set; } = "<body><h3>Plantilla Notificacion Agradecimiento</h3><p>Gracias por darle gas ;)</p></body>";
    }
}
