using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class ClimaDinamico
    {
        public int IdBaseDeDatos { get; set; } = 0;
        public string currentUrl { get; set; }
        public string TipoMensaje { get; set; }
        public string Mensaje { get; set; }
        public enum statusLogin {
            invalidkey = 0, // clave no valida
            encuestaRealizada = 1, //la encuesta ya fue realizada
            noStart = 2, // el periodo de aplicacion no ha iniciado
            AppEnd = 3, // el periodo de aplicacion ya termino
            BDNotFound = 4, // no se encontro la bd del empleado
            Exception = 5, // se genero una excepcion el autenticar
            success = 6, // autenticacion exitosa
            notFoundPeriodosApp = 7, // no se encontraron periodos de aplicacion
            statusEncuestaNotFound = 8, // 
            nullUID = 9, // el uid de la encuesta es null o vacio
            EncuestaNotFound = 10 // la encuesta no se encontró mediante el uid
        }
        public enum statusGuardado
        {
            success = 0,
            error = 1,
            itemNotFound = 2
        }
        public int IdEnfoque { get; set; } = 0;
        public static string noIniciada { get; set; } = "No Iniciada";
        public static string enProceso { get; set; } = "En proceso";
        public static string terminada { get; set; } = "Terminada";
        public int IdEmpleado { get; set; } = 0;
        public int IdEncuesta { get; set; } = 0;
        public string ErrorMessage { get; set; }

        public string RespuestaEmpleado { get; set; } = "";
        public ML.Preguntas Preguntas { get; set; }
        public ML.Respuestas Respuestas { get; set; }
        public ML.Empleado Empleado { get; set; }
        public ML.Encuesta Encuesta { get; set; }


        public int _idEmpleadoRespuestas { get; set; } = 0;
        public int _idEmpleado { get; set; } = 0;
        public int _idEncuesta { get; set; } = 0;
        public int _idPregunta { get; set; } = 0;
        public int _idEnfoque { get; set; } = 0;
        public string _respuestaEmpleado { get; set; } = "";
        public int _idTipoControl { get; set; } = 0;
        public bool hasRespuestas { get; set; }

        public string htmlCodeIntroduccion { get; set; } = "";
        public string htmlCodeInstrucciones { get; set; } = "";
        public string htmlAgradecimiento { get; set; } = "";

        /* default */
        public static string defaultHtmlIntro { get; set; } = "<div class='row main-content' style='padding-top:100px;' ng-init='vm.seccionesEncuesta.Id = 2'>   <div class='col-xs-12 col-sm-12 col-md-12 col-lg-5 col-xl-4 imagen-intro' id='img-intro'></div>   <div class='col-xs-12 col-sm-12 col-md-12 col-lg-7 col-xl-8' style='padding-top:30px; padding-left:30px; padding-right:30px;'>      <p class='welcome'>Bienvenido(a) a la encuesta de Clima laboral</p>      <p class='text-justify'>A continuación, se te presentaran una serie de reactivos que te pedimos respondas con honestidad según el grado que mejor refleje tu punto de vista, de acuerdo con la siguiente escala:</p>      <div class='row center-vertically'>         <div class='col-xs-3 col-sm-4 col-md-3 col-lg-4 col-xl-2 des-likert' style='background-color:rgb(241, 90, 36);'>            <p class='val'>Casi siempre es verdad</p>         </div>         <div class='col-xs-3 col-sm-4 col-md-3 col-lg-4 col-xl-2 des-likert' style='background-color:rgb(247, 147, 30);'>            <p class='val'>Frecuentemente es verdad</p>         </div>         <div class='col-xs-3 col-sm-4 col-md-3 col-lg-4 col-xl-2 des-likert' id='val3' style='background-color: #cccc01;'>            <p class='val'>A veces es falso / A veces es verdad</p>         </div>         <div class='col-xs-3 col-sm-4 col-md-3 col-lg-4 col-xl-2 des-likert' style='background-color:rgb(140, 198, 63);'>            <p class='val'>Frecuentemente es falso</p>         </div>         <div class='col-xs-3 col-sm-4 col-md-3 col-lg-4 col-xl-2 des-likert' style='background-color:rgb(57, 181, 74);'>            <p class='val'>Casi siempre es falso</p>         </div>      </div>      <p class='text-justify' style='padding-top:35px;'>Para realizar la encuesta deberás responder a cada reactivo desde dos enfoques, pensando en la situacion actual de 'la empresa y todos los jefes' y pensando en la situación actual de 'tu área de trabajo y jefe directo'. Algunos reactivos refieren a cuestiones personales, por lo que deberás responderlos de la misma forma en ambos enfoques.</p>      <p class='text-justify'>Toma en cuenta que esta encuesta es confidencial y que la información que proporciones será procesada por un equipo ético y especializado que presentará resultados de manera general y nunca de manera particular.</p>      <p class='text-justify'>Recuerda que la calidad de la información y las acciones de mejora que se deriven de ella dependerán en la honestidad con que respondas.</p>      <div class='row center-vertically' style='text-align:center;padding-top:15px;'>         <div class='col-xs-12 col-sm-12 col-md-6 col-lg-8 col-xl-4'>            <p class='welcome'>¡Tu participación es muy importante!</p>            <p class='welcome'>Para comenzar con tu aplicación, presiona el boton continuar.</p>         </div>      </div>      <div class='row center-vertically'>         <div class='col-xs-12 col-sm-12 col-md-7 col-lg-5 col-xl-4 btn-continuar text-center'>            <p class='btn-continuar' style='padding: 0.8rem;'>Continuar</p>         </div>      </div>   </div></div>";
        public static string defaultHtmlInstrucciones { get; set; } = "<div class='col-xs-12 col-sm-12 col-md-12 col-lg-8 col-xl-8'>   <p class='instrucciones-likert text-justify'>Responde a cada reactivo desde los dos enfoques, pensando en la situación actual de 'la empresa y todos los jefes' y pensando en la situacion actual de 'tu área de trabajo y jefe directo'.</p>   <p class='instrucciones-likert text-justify'>Los reactivos que refieren a cuestiones personales, deberás responderlos de la misma forma en ambos enfoques.</p></div>";
        public static string defaultHtmlGracias { get; set; } = "<p class='text-center' style='padding-top: 35px;'>¡Tus respuestas fueron guardadas exitosamente!<br> Gracias por participar en la encuesta de Clima Laboral "+ DateTime.Now.Year +"</p>";

    }
}
