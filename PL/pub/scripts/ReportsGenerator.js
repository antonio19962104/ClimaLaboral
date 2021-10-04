/*Script Generador de Reportes*/
/*07/08/2020*/
var IdEncuesta = $('#TxtIdEncuesta').val();
var nombreEncuesta = $('#TxtNombreEncuesta').val();
let model = { idEncuesta: IdEncuesta };

$(document).ready(function () {
    swal({
        title: "Procesando...",
        text: "Espere...",
        imageUrl: "",
        icon: "/images/load.gif",
        closeOnClickOutside: false,
        showConfirmButton: false,
        allowOutsideClick: false
    });
    $('.swal-icon').addClass('load');
    $('.load').css('width', '25%');
    $('.load').css('height', '25%');
    $('swal-button-container').css('display', 'none');
    getParticipacion();
    createBarrasHorizontal();//Likerts del 8 al 11
    getPreguntasByIdEncuesta();
});

function GetDashBoard() {
    window.location.href = "/DashBoard/DashBoard"
}

function getParticipacion() {
    $.ajax({
        url: '@Url.Action("GetParticipacion")',
        data: model,
        type: 'GET',
        complete: function (Response) {
            if (Response.responseJSON != null) {
                //todas, terminadas, faltantes
                var pendientes = Response.responseJSON.NoIniciadas + Response.responseJSON.Iniciadas;
                $('#mergeEsperadas').text(Response.responseJSON.Esperadas);
                $('#mergeTerminadas').text(Response.responseJSON.Terminadas);
                $('#mergePendientes').text(pendientes);
            }
            else {
                swal('No se pudieron obtener los resultados de participación en la encuesta ' + nombreEncuesta, '', 'info');
            }
        }
    });
}
function getPreguntasByIdEncuesta() {
    var IdEncuesta = $('#TxtIdEncuesta').val();
    $.ajax({
        url: '@Url.Action("GetPreguntasByIdEncuesta")',
        data: model,
        type: 'GET',
        complete: function (Response) {
            if (Response.responseJSON != null) {
                $('.swal-icon').addClass('load');
                $('.load').css('width', '25%');
                $('.load').css('height', '25%');
                $('swal-button-container').css('display', 'none');
                for (var i = 0; i < Response.responseJSON.Objects.length; i++) {
                    var itemPregunta = Response.responseJSON.Objects[i];
                    var idTipoControl = itemPregunta.TipoControl.IdTipoControl;
                    switch (idTipoControl) {
                        case 1:
                            //showOpenAnswers(itemPregunta, idTipoControl);//Respuesta corta OK
                            break;
                        case 2:
                            //showOpenAnswers(itemPregunta, idTipoControl);//Respuesta larga OK
                            break;
                        case 3:
                            //createCakeGraphic(itemPregunta, idTipoControl);//CheckBox OK
                            break;
                        case 4:
                            //createCakeGraphic(itemPregunta, idTipoControl);//RadioButton OK
                            break;
                        case 5:
                            //createBarrasVertical(itemPregunta, idTipoControl);//Select OK
                            break;
                        case 6:
                            //createBarrasFaceFeel(itemPregunta, idTipoControl);//Sentimiento OK
                            break;
                        case 7:
                            //createBarrasVertical(itemPregunta, idTipoControl);//Rango OK
                            break;
                            //Casos del 8 al 11 se omiten porque se buscan en automatico en el ready
                        case 12:
                            //createBarrasHorizontalLikertDoble(itemPregunta, idTipoControl);//LikertDoble
                            break;
                        default:
                            swal('El tipo de gráfico para la pregunta: ' + itemPregunta.Pregunta + ' no está definido', '', 'info');
                            break;
                    }
                }
                swal({
                    title: "Los gráficos han terminado de cargarse",
                    text: "",
                    imageUrl: "",
                    icon: "success",
                    closeOnClickOutside: false,
                    showConfirmButton: false,
                    allowOutsideClick: false
                });
            }
            else {
                swal('No se pudieron obtener las preguntas de la encuesta ' + nombreEncuesta, '', 'info');
            }
        }
    });
}
//Obtener todas las respuestas para esa pregunta
//Obtener el conteo
function createCakeGraphic(itemPregunta, idTipoControl) {
    console.log('Creando grafico de Pastel');
    let modelPregunta = {
        idPregunta: itemPregunta.IdPregunta,
        idEncuesta: IdEncuesta
    };
    $.ajax({
        url: '@Url.Action("GetRespuestasByIdPregunta")',
        data: modelPregunta,
        type: 'GET',
        complete: function (Response) {
            if (Response.responseJSON != null) {
                console.log(Response.responseJSON);//Preguntas y frecuencias junto a pregunta
                var IdPregunta = Response.responseJSON.Objects[0].Pregunta.IdPregunta;
                var Pregunta = Response.responseJSON.Objects[0].Pregunta.Pregunta;

                var DivIdForPregunta = 'divForPregunta_' + IdPregunta;
                $('#mergeGraficoForRadioAndCheck').append('<div id="' + DivIdForPregunta + '" class="mb-5" style="width: 100%; height: 400px;"></div>');

                var listRespuestas = ['Respuestas'];
                var listFrecuencias = ['Frecuencias'];
                //var IdRespuesta = Response.responseJSON.Respuesta.IdRespuesta;
                //var Respuesta = Response.responseJSON.Respuesta.Respuesta;
                for (var i = 0; i < Response.responseJSON.Objects.length; i++) {
                    listRespuestas.push(Response.responseJSON.Objects[i].Respuesta);
                    listFrecuencias.push(Response.responseJSON.Objects[i].conteoByPregunta);
                }

                //FillGraphic--Descomentar lo siguiente
                google.charts.load("current", {packages:["corechart"]});
                google.charts.setOnLoadCallback(drawChart);
                function drawChart() {

                    var data = google.visualization.arrayToDataTable([
                        [listRespuestas[0], listFrecuencias[0]],
                        [listRespuestas[1], listFrecuencias[1]],
                        [listRespuestas[2], listFrecuencias[2]],
                        [listRespuestas[3], listFrecuencias[3]],
                        [listRespuestas[4], listFrecuencias[4]],
                        [listRespuestas[5], listFrecuencias[5]]
                    ]);
                            
                    var options = {
                        title: Pregunta,
                        pieHole: 0.4,
                    };
                    var chart = new google.visualization.PieChart(document.getElementById(DivIdForPregunta));
                    chart.draw(data, options);
                }
                //End Fill
            }
            else {
                swal('No se pudieron obtener las respuestas de la pregunta ' + Pregunta, '', 'info');
            }
        }
    });
}

function createBarrasHorizontalLikertDoble(itemPregunta, idTipoControl, IdEncuesta) {
    //Likert Doble => Probando
    console.log('Creando grafico de Barras horizontal para likert doble');
    var IdEncuesta = $('#TxtIdEncuesta').val();
    let modelPregunta = {
        idPregunta: itemPregunta.IdPregunta,
        idEncuesta: IdEncuesta,
        IdentificadorTipoControl: idTipoControl
    };
    $.ajax({
        url: '@Url.Action("GetRespuestasByIdPregunta")',
        data: modelPregunta,
        type: 'GET',
        complete: function (Response) {
            if (Response.responseJSON.Objects.length > 0) {
                console.log(Response.responseJSON);//Recibo el nombre de las dos columnas (Enfoques empresa y area)

                var IdPregunta = Response.responseJSON.Objects[0].Pregunta.IdPregunta;
                var Instrucciones = Response.responseJSON.Objects[0].Pregunta.Pregunta;//Instrucciones

                console.log(itemPregunta);
                var numPreguntasLikert = itemPregunta.ListPreguntasLikert.length;
                //Este es el numero de preguntas likert en el likert doble

                var columnaA = Response.responseJSON.Objects[0].Respuesta;
                var columnaB = Response.responseJSON.Objects[1].Respuesta;
                var listReactivosLikert = [];//Reactivo N
                for (var i = 0; i < itemPregunta.ListPreguntasLikert.length; i++) {
                    var reactLikertcolA = itemPregunta.ListPreguntasLikert[i].PreguntaLikert + ' (' + columnaA + ')';
                    var reactLikertcolB = itemPregunta.ListPreguntasLikert[i].PreguntaLikert + ' (' + columnaB + ')';
                    listReactivosLikert.push(reactLikertcolA);//PreguntasLikert
                    listReactivosLikert.push(reactLikertcolB);//PreguntasLikert
                }
                console.log(listReactivosLikert);

                var IdPregunta = itemPregunta.IdPregunta;
                let modelPreg = { idPregunta: IdPregunta }
                $.ajax({ //select * from UsuarioRespuestas where IdPregunta = 1881
                    url: '@Url.Action("GetRespuestasFromPregLikert")',
                    data: modelPreg, //1881
                    complete: function (Response) {
                        if(Response != null){
                            for (var i = 0; i < Response.responseJSON.Objects.length; i++) {
                                        
                            }
                        }
                    }
                });

                var DivIdForPregunta = 'divForPregunta_' + IdPregunta;
                $('#mergeGraficoLikertDoble').append('<div id="' + DivIdForPregunta + '" class="mb-5" style="width: 100%; height: 400px;"></div>');

                //Una lista por cada preguntaLikert
                var listRespuestas = ['Respuesta'];
                var listFrecuencias = ['Frecuencias'];

                var CasiSiempreFalso = 0;
                var FrecuentementeFalso = 0;
                var AVecesVerdadAVecesFalso = 0;
                var FrecuentementeVerdad = 0;
                var CasiSiempreVerdad = 0;
                //happierface
                //happyface
                for (var i = 0; i < Response.responseJSON.Objects.length; i++) {
                    if (Response.responseJSON.Objects[i].RespuestaUsuario == '1') {
                        CasiSiempreFalso = CasiSiempreFalso + 1;
                    }
                    if (Response.responseJSON.Objects[i].RespuestaUsuario == '2') {
                        FrecuentementeFalso = FrecuentementeFalso + 1;
                    }
                    if (Response.responseJSON.Objects[i].RespuestaUsuario == '3') {
                        AVecesVerdadAVecesFalso = AVecesVerdadAVecesFalso + 1;
                    }
                    if (Response.responseJSON.Objects[i].RespuestaUsuario == '4') {
                        FrecuentementeVerdad = FrecuentementeVerdad + 1;
                    }
                    if (Response.responseJSON.Objects[i].RespuestaUsuario == '5') {
                        CasiSiempreVerdad = CasiSiempreVerdad + 1;
                    }
                }
                listFrecuencias.push(CasiSiempreFalso);
                listFrecuencias.push(FrecuentementeFalso);
                listFrecuencias.push(AVecesVerdadAVecesFalso);
                listFrecuencias.push(FrecuentementeVerdad);
                listFrecuencias.push(CasiSiempreVerdad);

                if (idTipoControl == 8) {//DeAcuerdo
                    listRespuestas.push('Totalmente en Desacuerdo');
                    listRespuestas.push('En Desacuerdo');
                    listRespuestas.push('Indeciso');
                    listRespuestas.push('De Acuerdo');
                    listRespuestas.push('Totalmente de Acuerdo');
                }
                if (idTipoControl == 9) {//Frecuencia
                    listRespuestas.push('Nunca');
                    listRespuestas.push('Raramente');
                    listRespuestas.push('Ocasionalmente');
                    listRespuestas.push('Frecuentemente');
                    listRespuestas.push('Siempre');
                }
                if (idTipoControl == 10) {//Importancia
                    listRespuestas.push('No Importante');
                    listRespuestas.push('Poco Importante');
                    listRespuestas.push('Moderadamente Importante');
                    listRespuestas.push('Importante');
                    listRespuestas.push('Muy Importante');
                }
                if (idTipoControl == 11) {//Probabilidad
                    listRespuestas.push('Siempre es Falso');
                    listRespuestas.push('Usualmente es Falso');
                    listRespuestas.push('A veces es verdad / A veces es falso');
                    listRespuestas.push('Usualmente es verdad');
                    listRespuestas.push('Siempre es Verdad');
                }
                if (idTipoControl == 12) {//LikertDoble ClimaLab
                    listRespuestas.push('Casi Siempre es Falso');
                    listRespuestas.push('Frecuentemente es Falso');
                    listRespuestas.push('A veces es verdad / A veces es falso');
                    listRespuestas.push('Frecuentemente es verdad');
                    listRespuestas.push('Casi Siempre es Verdad');
                }


                //FillGraphic--Descomentar lo siguiente
                google.charts.load("current", { packages: ["corechart"] });
                google.charts.setOnLoadCallback(drawChart);
                function drawChart() {
                           
                    var data = google.visualization.arrayToDataTable([
                        ['Opciones', , listRespuestas[1], listRespuestas[2], listRespuestas[3], listRespuestas[4], listRespuestas[5]],
                        ['Reactivo 1', 10, 24, 20, 32, 18, 5, ''],
                        ['Reactivo 2', 16, 22, 23, 30, 16, 9, ''],
                        ['Reactivo 3', 28, 19, 29, 30, 12, 13, '']
                    ]);

                    var options = {
                        width: '100%',
                        height: 400,
                        legend: { position: 'top', maxLines: 3 },
                        bar: { groupWidth: '75%' },
                        isStacked: true,
                    };
                    var chart = new google.visualization.BarChart(document.getElementById(DivIdForPregunta));
                    chart.draw(data, options);
                }
                //End Fill
            }
            else {
                swal('No se pudieron obtener las respuestas de la pregunta ' + Pregunta, '', 'info');
            }
        }
    });
}

var listDataLikerts = [];

function createBarrasHorizontal() {//ready function
    //Likerts => OK
    //Obtener los resultados no de una sino de todas las preguntas de tipo likert en la encuesta
    let model = { idEncuesta: IdEncuesta }
    $.ajax({
        url: '@Url.Action("GetPreguntasLikertExceptDobleByEncuesta")',
        data: model,
        complete: function (Response) {
            if(Response != null){
                //Response.responseJSON.Objects => Pregunta
                //Response.responseJSON.ObjectsAux => Frecuencia
                var IdPregunta = Response.responseJSON.Objects[0].IdPregunta;//Tomo solo la primera de la lista como referencia
                var DivIdForPregunta = 'divForPregunta_' + IdPregunta;
                $('#mergeGraficoLikert').append('<div id="' + DivIdForPregunta + '" class="mb-5" style="width: 100%; height: 400px;"></div>');
                var dataForGraphics = [];


                for (var i = 0; i < Response.responseJSON.Objects.length; i++) {

                    var CasiSiempreFalso = 0;
                    var FrecuentementeFalso = 0;
                    var AVecesVerdadAVecesFalso = 0;
                    var FrecuentementeVerdad = 0;
                    var CasiSiempreVerdad = 0;

                    let modelPreguntaJS = {
                        Pregunta: null,
                        CSF: null,
                        FF: null,
                        AV: null,
                        FV: null,
                        CSV: null
                    };

                    let modelPreguntaJSColA = {
                        Pregunta: null,
                        CSF: null,
                        FF: null,
                        AV: null,
                        FV: null,
                        CSV: null
                    };

                    let modelPreguntaJSColB = {
                        Pregunta: null,
                        CSF: null,
                        FF: null,
                        AV: null,
                        FV: null,
                        CSV: null
                    };

                    if (Response.responseJSON.Objects[i].TipoControl.IdTipoControl == 12) {
                        //Obtengo ColA & ColB
                        var colA = Response.responseJSON.Objects[i].listUsuarioResp[0].Respuestas.Respuesta;
                        var colB = Response.responseJSON.Objects[i].listUsuarioResp[1].Respuestas.Respuesta;
                        var preguntaColA = '';
                        var preguntaColB = '';
                        for (var k = 0; k < Response.responseJSON.Objects[i].ListPreguntasLikert.length; k++) {
                            preguntaColA = Response.responseJSON.Objects[i].ListPreguntasLikert[k].PreguntaLikert + '(' + colA + ')';
                            preguntaColB = Response.responseJSON.Objects[i].ListPreguntasLikert[k].PreguntaLikert + '(' + colB + ')';
                            //}
                            var IdPreguntaLikertDobleaCTUAL = Response.responseJSON.Objects[i].ListPreguntasLikert[k].IdPreguntaLikert;

                            var CasiSiempreFalsoColA = 0;
                            var FrecuentementeFalsoColA = 0;
                            var AVecesVerdadAVecesFalsoColA = 0;
                            var FrecuentementeVerdadColA = 0;
                            var CasiSiempreVerdadColA = 0;

                            var CasiSiempreFalsoColB = 0;
                            var FrecuentementeFalsoColB = 0;
                            var AVecesVerdadAVecesFalsoColB = 0;
                            var FrecuentementeVerdadColB = 0;
                            var CasiSiempreVerdadColB = 0;

                            var numPreguntas = Response.responseJSON.Objects[i].ListPreguntasLikert.length;//numero de preguntas likert
                            numPreguntas = numPreguntas * 2;

                                    
                                    



                            for (var j = 0; j < Response.responseJSON.Objects[i].listUsuarioResp.length; j++) {//Traer el id de pregunta likert
                                //var IdPreguntaLikertDobleaCTUAL = Response.responseJSON.Objects[i].listUsuarioResp[j].Respuestas.PreguntasLikert.IdPreguntaLikert;//Aqui esta el Id de pregunta likert
                                if (Response.responseJSON.Objects[i].listUsuarioResp[j].RespuestaUsuario == '1' && Response.responseJSON.Objects[i].listUsuarioResp[j].Respuestas.PreguntasLikert.IdPreguntaLikert == IdPreguntaLikertDobleaCTUAL && Response.responseJSON.Objects[i].listUsuarioResp[j].Respuestas.Respuesta == colA) {
                                    CasiSiempreFalsoColA = CasiSiempreFalsoColA + 1;
                                }
                                if (Response.responseJSON.Objects[i].listUsuarioResp[j].RespuestaUsuario == '2' && Response.responseJSON.Objects[i].listUsuarioResp[j].Respuestas.PreguntasLikert.IdPreguntaLikert == IdPreguntaLikertDobleaCTUAL && Response.responseJSON.Objects[i].listUsuarioResp[j].Respuestas.Respuesta == colA) {
                                    FrecuentementeFalsoColA = FrecuentementeFalsoColA + 1;
                                }
                                if (Response.responseJSON.Objects[i].listUsuarioResp[j].RespuestaUsuario == '3' && Response.responseJSON.Objects[i].listUsuarioResp[j].Respuestas.PreguntasLikert.IdPreguntaLikert == IdPreguntaLikertDobleaCTUAL && Response.responseJSON.Objects[i].listUsuarioResp[j].Respuestas.Respuesta == colA) {
                                    AVecesVerdadAVecesFalsoColA = AVecesVerdadAVecesFalsoColA + 1;
                                }
                                if (Response.responseJSON.Objects[i].listUsuarioResp[j].RespuestaUsuario == '4' && Response.responseJSON.Objects[i].listUsuarioResp[j].Respuestas.PreguntasLikert.IdPreguntaLikert == IdPreguntaLikertDobleaCTUAL && Response.responseJSON.Objects[i].listUsuarioResp[j].Respuestas.Respuesta == colA) {
                                    FrecuentementeVerdadColA = FrecuentementeVerdadColA + 1;
                                }
                                if (Response.responseJSON.Objects[i].listUsuarioResp[j].RespuestaUsuario == '5' && Response.responseJSON.Objects[i].listUsuarioResp[j].Respuestas.PreguntasLikert.IdPreguntaLikert == IdPreguntaLikertDobleaCTUAL && Response.responseJSON.Objects[i].listUsuarioResp[j].Respuestas.Respuesta == colA) {
                                    CasiSiempreVerdadColA = CasiSiempreVerdadColA + 1;
                                }
                            }


                            for (var j = 0; j < Response.responseJSON.Objects[i].listUsuarioResp.length; j++) {//Traer el id de pregunta likert
                                //var IdPreguntaLikertDobleaCTUAL = Response.responseJSON.Objects[i].listUsuarioResp[j].Respuestas.PreguntasLikert.IdPreguntaLikert;//Aqui esta el Id de pregunta likert
                                if (Response.responseJSON.Objects[i].listUsuarioResp[j].RespuestaUsuario == '1' && Response.responseJSON.Objects[i].listUsuarioResp[j].Respuestas.PreguntasLikert.IdPreguntaLikert == IdPreguntaLikertDobleaCTUAL && Response.responseJSON.Objects[i].listUsuarioResp[j].Respuestas.Respuesta == colB) {
                                    CasiSiempreFalsoColB = CasiSiempreFalsoColB + 1;
                                }
                                if (Response.responseJSON.Objects[i].listUsuarioResp[j].RespuestaUsuario == '2' && Response.responseJSON.Objects[i].listUsuarioResp[j].Respuestas.PreguntasLikert.IdPreguntaLikert == IdPreguntaLikertDobleaCTUAL && Response.responseJSON.Objects[i].listUsuarioResp[j].Respuestas.Respuesta == colB) {
                                    FrecuentementeFalsoColB = FrecuentementeFalsoColB + 1;
                                }
                                if (Response.responseJSON.Objects[i].listUsuarioResp[j].RespuestaUsuario == '3' && Response.responseJSON.Objects[i].listUsuarioResp[j].Respuestas.PreguntasLikert.IdPreguntaLikert == IdPreguntaLikertDobleaCTUAL && Response.responseJSON.Objects[i].listUsuarioResp[j].Respuestas.Respuesta == colB) {
                                    AVecesVerdadAVecesFalsoColB = AVecesVerdadAVecesFalsoColB + 1;
                                }
                                if (Response.responseJSON.Objects[i].listUsuarioResp[j].RespuestaUsuario == '4' && Response.responseJSON.Objects[i].listUsuarioResp[j].Respuestas.PreguntasLikert.IdPreguntaLikert == IdPreguntaLikertDobleaCTUAL && Response.responseJSON.Objects[i].listUsuarioResp[j].Respuestas.Respuesta == colB) {
                                    FrecuentementeVerdadColB = FrecuentementeVerdadColB + 1;
                                }
                                if (Response.responseJSON.Objects[i].listUsuarioResp[j].RespuestaUsuario == '5' && Response.responseJSON.Objects[i].listUsuarioResp[j].Respuestas.PreguntasLikert.IdPreguntaLikert == IdPreguntaLikertDobleaCTUAL && Response.responseJSON.Objects[i].listUsuarioResp[j].Respuestas.Respuesta == colB) {
                                    CasiSiempreVerdadColB = CasiSiempreVerdadColB + 1;
                                }
                            }



                            let modelPreguntaJSColA = {
                                Pregunta: preguntaColA,
                                CSF: CasiSiempreFalsoColA,
                                FF: FrecuentementeFalsoColA,
                                AV: AVecesVerdadAVecesFalsoColA,
                                FV: FrecuentementeVerdadColA,
                                CSV: CasiSiempreVerdadColA
                            };

                            let modelPreguntaJSColB = {
                                Pregunta: preguntaColB,
                                CSF: CasiSiempreFalsoColB,
                                FF: FrecuentementeFalsoColB,
                                AV: AVecesVerdadAVecesFalsoColB,
                                FV: FrecuentementeVerdadColB,
                                CSV: CasiSiempreVerdadColB
                            };
                            dataForGraphics.push(modelPreguntaJSColA);
                            dataForGraphics.push(modelPreguntaJSColB);
                        }
                    }
                    else {
                        var Pregunta = Response.responseJSON.Objects[i].Pregunta;

                        for (var j = 0; j < Response.responseJSON.Objects[i].listUsuarioResp.length; j++) {
                            if (Response.responseJSON.Objects[i].listUsuarioResp[j].RespuestaUsuario == '1') {
                                CasiSiempreFalso = CasiSiempreFalso + 1;
                            }
                            if (Response.responseJSON.Objects[i].listUsuarioResp[j].RespuestaUsuario == '2') {
                                FrecuentementeFalso = FrecuentementeFalso + 1;
                            }
                            if (Response.responseJSON.Objects[i].listUsuarioResp[j].RespuestaUsuario == '3') {
                                AVecesVerdadAVecesFalso = AVecesVerdadAVecesFalso + 1;
                            }
                            if (Response.responseJSON.Objects[i].listUsuarioResp[j].RespuestaUsuario == '4') {
                                FrecuentementeVerdad = FrecuentementeVerdad + 1;
                            }
                            if (Response.responseJSON.Objects[i].listUsuarioResp[j].RespuestaUsuario == '5') {
                                CasiSiempreVerdad = CasiSiempreVerdad + 1;
                            }
                        }

                        modelPreguntaJS = {
                            Pregunta: Pregunta,
                            CSF: CasiSiempreFalso,
                            FF: FrecuentementeFalso,
                            AV: AVecesVerdadAVecesFalso,
                            FV: FrecuentementeVerdad,
                            CSV: CasiSiempreVerdad
                        };
                        dataForGraphics.push(modelPreguntaJS);
                    }
                }
                console.log(dataForGraphics);
                //FillGraphics
                google.charts.load("current", { packages: ["corechart"] });
                google.charts.setOnLoadCallback(drawChart);
                function drawChart() {
                    var data = null;

                    var long = dataForGraphics.length;


                    if (long == 1) {
                        data = google.visualization.arrayToDataTable([
                            ['Opciones', 'Opción 1(CSF)', 'Opción 2(FF)', 'Opción 3(AV)', 'Opción 4(FV)', 'Opción 5(CSV)', { role: 'annotation' }],
                            [dataForGraphics[0].Pregunta, dataForGraphics[0].CSF, dataForGraphics[0].FF, dataForGraphics[0].AV, dataForGraphics[0].FV, dataForGraphics[0].CSV, '']
                        ]);
                    }
                    if (long == 2) {
                        data = google.visualization.arrayToDataTable([
                            ['Opciones', 'Opción 1(CSF)', 'Opción 2(FF)', 'Opción 3(AV)', 'Opción 4(FV)', 'Opción 5(CSV)', { role: 'annotation' }],
                            [dataForGraphics[0].Pregunta, dataForGraphics[0].CSF, dataForGraphics[0].FF, dataForGraphics[0].AV, dataForGraphics[0].FV, dataForGraphics[0].CSV, ''],
                            [dataForGraphics[1].Pregunta, dataForGraphics[1].CSF, dataForGraphics[1].FF, dataForGraphics[1].AV, dataForGraphics[1].FV, dataForGraphics[1].CSV, '']
                        ]);
                    }
                    if (long == 3) {
                        data = google.visualization.arrayToDataTable([
                            ['Opciones', 'Opción 1(CSF)', 'Opción 2(FF)', 'Opción 3(AV)', 'Opción 4(FV)', 'Opción 5(CSV)', { role: 'annotation' }],
                            [dataForGraphics[0].Pregunta, dataForGraphics[0].CSF, dataForGraphics[0].FF, dataForGraphics[0].AV, dataForGraphics[0].FV, dataForGraphics[0].CSV, ''],
                            [dataForGraphics[1].Pregunta, dataForGraphics[1].CSF, dataForGraphics[1].FF, dataForGraphics[1].AV, dataForGraphics[1].FV, dataForGraphics[1].CSV, ''],
                            [dataForGraphics[2].Pregunta, dataForGraphics[2].CSF, dataForGraphics[2].FF, dataForGraphics[2].AV, dataForGraphics[2].FV, dataForGraphics[2].CSV, '']
                        ]);
                    }
                    if (long == 4) {
                        data = google.visualization.arrayToDataTable([
                            ['Opciones', 'Opción 1(CSF)', 'Opción 2(FF)', 'Opción 3(AV)', 'Opción 4(FV)', 'Opción 5(CSV)', { role: 'annotation' }],
                            [dataForGraphics[0].Pregunta, dataForGraphics[0].CSF, dataForGraphics[0].FF, dataForGraphics[0].AV, dataForGraphics[0].FV, dataForGraphics[0].CSV, ''],
                            [dataForGraphics[1].Pregunta, dataForGraphics[1].CSF, dataForGraphics[1].FF, dataForGraphics[1].AV, dataForGraphics[1].FV, dataForGraphics[1].CSV, ''],
                            [dataForGraphics[2].Pregunta, dataForGraphics[2].CSF, dataForGraphics[2].FF, dataForGraphics[2].AV, dataForGraphics[2].FV, dataForGraphics[2].CSV, ''],
                            [dataForGraphics[3].Pregunta, dataForGraphics[3].CSF, dataForGraphics[3].FF, dataForGraphics[3].AV, dataForGraphics[3].FV, dataForGraphics[3].CSV, '']
                        ]);
                    }
                    if (long == 5) {
                        data = google.visualization.arrayToDataTable([
                            ['Opciones', 'Opción 1(CSF)', 'Opción 2(FF)', 'Opción 3(AV)', 'Opción 4(FV)', 'Opción 5(CSV)', { role: 'annotation' }],
                            [dataForGraphics[0].Pregunta, dataForGraphics[0].CSF, dataForGraphics[0].FF, dataForGraphics[0].AV, dataForGraphics[0].FV, dataForGraphics[0].CSV, ''],
                            [dataForGraphics[1].Pregunta, dataForGraphics[1].CSF, dataForGraphics[1].FF, dataForGraphics[1].AV, dataForGraphics[1].FV, dataForGraphics[1].CSV, ''],
                            [dataForGraphics[2].Pregunta, dataForGraphics[2].CSF, dataForGraphics[2].FF, dataForGraphics[2].AV, dataForGraphics[2].FV, dataForGraphics[2].CSV, ''],
                            [dataForGraphics[3].Pregunta, dataForGraphics[3].CSF, dataForGraphics[3].FF, dataForGraphics[3].AV, dataForGraphics[3].FV, dataForGraphics[3].CSV, ''],
                            [dataForGraphics[4].Pregunta, dataForGraphics[4].CSF, dataForGraphics[4].FF, dataForGraphics[4].AV, dataForGraphics[4].FV, dataForGraphics[4].CSV, '']
                        ]);
                    }
                    if (long == 6) {
                        data = google.visualization.arrayToDataTable([
                            ['Opciones', 'Opción 1(CSF)', 'Opción 2(FF)', 'Opción 3(AV)', 'Opción 4(FV)', 'Opción 5(CSV)', { role: 'annotation' }],
                            [dataForGraphics[0].Pregunta, dataForGraphics[0].CSF, dataForGraphics[0].FF, dataForGraphics[0].AV, dataForGraphics[0].FV, dataForGraphics[0].CSV, ''],
                            [dataForGraphics[1].Pregunta, dataForGraphics[1].CSF, dataForGraphics[1].FF, dataForGraphics[1].AV, dataForGraphics[1].FV, dataForGraphics[1].CSV, ''],
                            [dataForGraphics[2].Pregunta, dataForGraphics[2].CSF, dataForGraphics[2].FF, dataForGraphics[2].AV, dataForGraphics[2].FV, dataForGraphics[2].CSV, ''],
                            [dataForGraphics[3].Pregunta, dataForGraphics[3].CSF, dataForGraphics[3].FF, dataForGraphics[3].AV, dataForGraphics[3].FV, dataForGraphics[3].CSV, ''],
                            [dataForGraphics[4].Pregunta, dataForGraphics[4].CSF, dataForGraphics[4].FF, dataForGraphics[4].AV, dataForGraphics[4].FV, dataForGraphics[4].CSV, ''],
                            [dataForGraphics[5].Pregunta, dataForGraphics[5].CSF, dataForGraphics[5].FF, dataForGraphics[5].AV, dataForGraphics[5].FV, dataForGraphics[5].CSV, '']
                        ]);
                    }
                    if (long == 7) {
                        data = google.visualization.arrayToDataTable([
                            ['Opciones', 'Opción 1(CSF)', 'Opción 2(FF)', 'Opción 3(AV)', 'Opción 4(FV)', 'Opción 5(CSV)', { role: 'annotation' }],
                            [dataForGraphics[0].Pregunta, dataForGraphics[0].CSF, dataForGraphics[0].FF, dataForGraphics[0].AV, dataForGraphics[0].FV, dataForGraphics[0].CSV, ''],
                            [dataForGraphics[1].Pregunta, dataForGraphics[1].CSF, dataForGraphics[1].FF, dataForGraphics[1].AV, dataForGraphics[1].FV, dataForGraphics[1].CSV, ''],
                            [dataForGraphics[2].Pregunta, dataForGraphics[2].CSF, dataForGraphics[2].FF, dataForGraphics[2].AV, dataForGraphics[2].FV, dataForGraphics[2].CSV, ''],
                            [dataForGraphics[3].Pregunta, dataForGraphics[3].CSF, dataForGraphics[3].FF, dataForGraphics[3].AV, dataForGraphics[3].FV, dataForGraphics[3].CSV, ''],
                            [dataForGraphics[4].Pregunta, dataForGraphics[4].CSF, dataForGraphics[4].FF, dataForGraphics[4].AV, dataForGraphics[4].FV, dataForGraphics[4].CSV, ''],
                            [dataForGraphics[5].Pregunta, dataForGraphics[5].CSF, dataForGraphics[5].FF, dataForGraphics[5].AV, dataForGraphics[5].FV, dataForGraphics[5].CSV, ''],
                            [dataForGraphics[6].Pregunta, dataForGraphics[6].CSF, dataForGraphics[6].FF, dataForGraphics[6].AV, dataForGraphics[6].FV, dataForGraphics[6].CSV, '']
                        ]);
                    }
                    if (long == 8) {
                        data = google.visualization.arrayToDataTable([
                            ['Opciones', 'Opción 1(CSF)', 'Opción 2(FF)', 'Opción 3(AV)', 'Opción 4(FV)', 'Opción 5(CSV)', { role: 'annotation' }],
                            [dataForGraphics[0].Pregunta, dataForGraphics[0].CSF, dataForGraphics[0].FF, dataForGraphics[0].AV, dataForGraphics[0].FV, dataForGraphics[0].CSV, ''],
                            [dataForGraphics[1].Pregunta, dataForGraphics[1].CSF, dataForGraphics[1].FF, dataForGraphics[1].AV, dataForGraphics[1].FV, dataForGraphics[1].CSV, ''],
                            [dataForGraphics[2].Pregunta, dataForGraphics[2].CSF, dataForGraphics[2].FF, dataForGraphics[2].AV, dataForGraphics[2].FV, dataForGraphics[2].CSV, ''],
                            [dataForGraphics[3].Pregunta, dataForGraphics[3].CSF, dataForGraphics[3].FF, dataForGraphics[3].AV, dataForGraphics[3].FV, dataForGraphics[3].CSV, ''],
                            [dataForGraphics[4].Pregunta, dataForGraphics[4].CSF, dataForGraphics[4].FF, dataForGraphics[4].AV, dataForGraphics[4].FV, dataForGraphics[4].CSV, ''],
                            [dataForGraphics[5].Pregunta, dataForGraphics[5].CSF, dataForGraphics[5].FF, dataForGraphics[5].AV, dataForGraphics[5].FV, dataForGraphics[5].CSV, ''],
                            [dataForGraphics[6].Pregunta, dataForGraphics[6].CSF, dataForGraphics[6].FF, dataForGraphics[6].AV, dataForGraphics[6].FV, dataForGraphics[6].CSV, ''],
                            [dataForGraphics[7].Pregunta, dataForGraphics[7].CSF, dataForGraphics[7].FF, dataForGraphics[7].AV, dataForGraphics[7].FV, dataForGraphics[7].CSV, '']
                        ]);
                    }
                    if (long == 9) {
                        data = google.visualization.arrayToDataTable([
                            ['Opciones', 'Opción 1(CSF)', 'Opción 2(FF)', 'Opción 3(AV)', 'Opción 4(FV)', 'Opción 5(CSV)', { role: 'annotation' }],
                            [dataForGraphics[0].Pregunta, dataForGraphics[0].CSF, dataForGraphics[0].FF, dataForGraphics[0].AV, dataForGraphics[0].FV, dataForGraphics[0].CSV, ''],
                            [dataForGraphics[1].Pregunta, dataForGraphics[1].CSF, dataForGraphics[1].FF, dataForGraphics[1].AV, dataForGraphics[1].FV, dataForGraphics[1].CSV, ''],
                            [dataForGraphics[2].Pregunta, dataForGraphics[2].CSF, dataForGraphics[2].FF, dataForGraphics[2].AV, dataForGraphics[2].FV, dataForGraphics[2].CSV, ''],
                            [dataForGraphics[3].Pregunta, dataForGraphics[3].CSF, dataForGraphics[3].FF, dataForGraphics[3].AV, dataForGraphics[3].FV, dataForGraphics[3].CSV, ''],
                            [dataForGraphics[4].Pregunta, dataForGraphics[4].CSF, dataForGraphics[4].FF, dataForGraphics[4].AV, dataForGraphics[4].FV, dataForGraphics[4].CSV, ''],
                            [dataForGraphics[5].Pregunta, dataForGraphics[5].CSF, dataForGraphics[5].FF, dataForGraphics[5].AV, dataForGraphics[5].FV, dataForGraphics[5].CSV, ''],
                            [dataForGraphics[6].Pregunta, dataForGraphics[6].CSF, dataForGraphics[6].FF, dataForGraphics[6].AV, dataForGraphics[6].FV, dataForGraphics[6].CSV, ''],
                            [dataForGraphics[7].Pregunta, dataForGraphics[7].CSF, dataForGraphics[7].FF, dataForGraphics[7].AV, dataForGraphics[7].FV, dataForGraphics[7].CSV, ''],
                            [dataForGraphics[8].Pregunta, dataForGraphics[8].CSF, dataForGraphics[8].FF, dataForGraphics[8].AV, dataForGraphics[8].FV, dataForGraphics[8].CSV, '']
                        ]);
                    }
                    if (long == 10) {
                        data = google.visualization.arrayToDataTable([
                            ['Opciones', 'Opción 1(CSF)', 'Opción 2(FF)', 'Opción 3(AV)', 'Opción 4(FV)', 'Opción 5(CSV)', { role: 'annotation' }],
                            [dataForGraphics[0].Pregunta, dataForGraphics[0].CSF, dataForGraphics[0].FF, dataForGraphics[0].AV, dataForGraphics[0].FV, dataForGraphics[0].CSV, ''],
                            [dataForGraphics[1].Pregunta, dataForGraphics[1].CSF, dataForGraphics[1].FF, dataForGraphics[1].AV, dataForGraphics[1].FV, dataForGraphics[1].CSV, ''],
                            [dataForGraphics[2].Pregunta, dataForGraphics[2].CSF, dataForGraphics[2].FF, dataForGraphics[2].AV, dataForGraphics[2].FV, dataForGraphics[2].CSV, ''],
                            [dataForGraphics[3].Pregunta, dataForGraphics[3].CSF, dataForGraphics[3].FF, dataForGraphics[3].AV, dataForGraphics[3].FV, dataForGraphics[3].CSV, ''],
                            [dataForGraphics[4].Pregunta, dataForGraphics[4].CSF, dataForGraphics[4].FF, dataForGraphics[4].AV, dataForGraphics[4].FV, dataForGraphics[4].CSV, ''],
                            [dataForGraphics[5].Pregunta, dataForGraphics[5].CSF, dataForGraphics[5].FF, dataForGraphics[5].AV, dataForGraphics[5].FV, dataForGraphics[5].CSV, ''],
                            [dataForGraphics[6].Pregunta, dataForGraphics[6].CSF, dataForGraphics[6].FF, dataForGraphics[6].AV, dataForGraphics[6].FV, dataForGraphics[6].CSV, ''],
                            [dataForGraphics[7].Pregunta, dataForGraphics[7].CSF, dataForGraphics[7].FF, dataForGraphics[7].AV, dataForGraphics[7].FV, dataForGraphics[7].CSV, ''],
                            [dataForGraphics[8].Pregunta, dataForGraphics[8].CSF, dataForGraphics[8].FF, dataForGraphics[8].AV, dataForGraphics[8].FV, dataForGraphics[8].CSV, ''],
                            [dataForGraphics[9].Pregunta, dataForGraphics[9].CSF, dataForGraphics[9].FF, dataForGraphics[9].AV, dataForGraphics[9].FV, dataForGraphics[9].CSV, '']
                        ]);
                    }
                            
                    var options = {
                        width: '100%',
                        height: 400,
                        legend: { position: 'top', maxLines: 3 },
                        bar: { groupWidth: '75%' },
                        isStacked: true,
                    };
                    var chart = new google.visualization.BarChart(document.getElementById(DivIdForPregunta));
                    if (long > 0) {
                        chart.draw(data, options);
                    }
                }
                //EndFill
            }
            else {
                console.log('No hay likerts en la encuesta');
            }
        }
    });
}
//*****************************************************************
function createBarrasVertical(itemPregunta, idTipoControl) {
    console.log('Creando grafico de Barras vertical');
    let modelPregunta = {
        idPregunta: itemPregunta.IdPregunta,
        idEncuesta: IdEncuesta
    };
    $.ajax({
        url: '@Url.Action("GetRespuestasByIdPregunta")',
        data: modelPregunta,
        type: 'GET',
        complete: function (Response) {
            if (Response.responseJSON != null) {
                if (idTipoControl == 7) {
                    //Rango
                    console.log(Response.responseJSON);//Preguntas y frecuencias junto a pregunta
                    var IdPregunta = Response.responseJSON.Objects[0].Preguntas.IdPregunta;
                    var Pregunta = Response.responseJSON.Objects[0].Preguntas.Pregunta;

                    var DivIdForPregunta = 'divForPregunta_' + IdPregunta;
                    $('#mergeGraficoForSelectAndRango').append('<div id="' + DivIdForPregunta + '" class="mb-5" style="width: 100%; height: 400px;"></div>');

                    var listRespuestas = ['Pregunta'];
                    var listFrecuencias = ['Respuesta'];
                    for (var i = 0; i < Response.responseJSON.Objects.length; i++) {
                        listFrecuencias.push(Response.responseJSON.Objects[i].RespuestaUsuario);
                    }

                    //FillGraphic
                    google.charts.load("current", { packages: ["corechart"] });
                    google.charts.setOnLoadCallback(drawChart);
                    function drawChart() {

                        var data = google.visualization.arrayToDataTable([
                            [Pregunta, listFrecuencias[0]],
                            [Pregunta, listFrecuencias[1]],
                            [Pregunta, listFrecuencias[2]],
                            [Pregunta, listFrecuencias[3]],
                            [Pregunta, listFrecuencias[4]],
                            [Pregunta, listFrecuencias[5]],
                            [Pregunta, listFrecuencias[6]],
                            [Pregunta, listFrecuencias[7]],
                            [Pregunta, listFrecuencias[8]],
                            [Pregunta, listFrecuencias[9]],
                            [Pregunta, listFrecuencias[10]],
                            [Pregunta, listFrecuencias[11]]
                        ]);

                        var options = {
                            title: Pregunta,
                            legend: { position: 'none' }
                        };
                        var chart = new google.visualization.Histogram(document.getElementById(DivIdForPregunta));
                        chart.draw(data, options);
                    }
                    //End Fill
                }
                else
                {
                    console.log(Response.responseJSON);//Preguntas y frecuencias junto a pregunta
                    var IdPregunta = Response.responseJSON.Objects[0].Pregunta.IdPregunta;
                    var Pregunta = Response.responseJSON.Objects[0].Pregunta.Pregunta;

                    var DivIdForPregunta = 'divForPregunta_' + IdPregunta;
                    $('#mergeGraficoForSelectAndRango').append('<div id="' + DivIdForPregunta + '" class="mb-5" style="width: 100%; height: 400px;"></div>');

                    var listRespuestas = ['Respuestas'];
                    var listFrecuencias = ['Frecuencias'];
                    //var IdRespuesta = Response.responseJSON.Respuesta.IdRespuesta;
                    //var Respuesta = Response.responseJSON.Respuesta.Respuesta;
                    for (var i = 0; i < Response.responseJSON.Objects.length; i++) {
                        listRespuestas.push(Response.responseJSON.Objects[i].Respuesta);
                        listFrecuencias.push(Response.responseJSON.Objects[i].conteoByPregunta);
                    }

                    //FillGraphic
                    google.charts.load("current", { packages: ["corechart"] });
                    google.charts.setOnLoadCallback(drawChart);
                    function drawChart() {

                        var data = google.visualization.arrayToDataTable([
                            [listRespuestas[0], listFrecuencias[0]],
                            [listRespuestas[1], listFrecuencias[1]],
                            [listRespuestas[2], listFrecuencias[2]],
                            [listRespuestas[3], listFrecuencias[3]],
                            [listRespuestas[4], listFrecuencias[4]],
                            [listRespuestas[5], listFrecuencias[5]],
                            [listRespuestas[0], listFrecuencias[6]],
                            [listRespuestas[1], listFrecuencias[7]],
                            [listRespuestas[2], listFrecuencias[8]],
                            [listRespuestas[3], listFrecuencias[9]],
                            [listRespuestas[4], listFrecuencias[10]],
                            [listRespuestas[5], listFrecuencias[11]]
                        ]);

                        var options = {
                            title: Pregunta,
                            legend: { position: 'none' }
                        };
                        var chart = new google.visualization.Histogram(document.getElementById(DivIdForPregunta));
                        chart.draw(data, options);
                    }
                    //End Fill
                }
            }
            else {
                swal('No se pudieron obtener las respuestas de la pregunta ' + Pregunta, '', 'info');
            }
        }
    });
}
//**************************************************************
function showOpenAnswers(itemPregunta, idTipoControl) {
    console.log('Mostrando las respuestas abiertas');
    let modelPregunta = {
        idPregunta: itemPregunta.IdPregunta,
        idEncuesta: IdEncuesta
    };
    $.ajax({
        url: '@Url.Action("GetRespuestasByIdPregunta")',
        data: modelPregunta,
        type: 'GET',
        complete: function (Response) {
            if (Response.responseJSON != null) {
                console.log(Response.responseJSON);//Preguntas y frecuencias junto a pregunta
                var IdPregunta = Response.responseJSON.Objects[0].Preguntas.IdPregunta;
                var Pregunta = Response.responseJSON.Objects[0].Preguntas.Pregunta;
                var IdForTotal = 'showTotal' + IdPregunta;
                var DivIdForPregunta = 'divForPregunta_' + IdPregunta;
                $('#mergeRespuestasAbiertas').append('<div id="' + DivIdForPregunta + '" class="" style="width: 100%; height: 400px;"></div>');

                var listRespuestas = [];
                //var IdRespuesta = Response.responseJSON.Respuesta.IdRespuesta;
                //var Respuesta = Response.responseJSON.Respuesta.Respuesta;
                var lista = '';
                for (var i = 0; i < Response.responseJSON.Objects.length; i++) {
                    var numeral = i + 1;
                    var Respuesta = Response.responseJSON.Objects[i].RespuestaUsuario;
                    lista += '<li class="tooltip">' + numeral + '.-' + Respuesta.substr(0, 10) + '<span class="tooltiptext">' + Response.responseJSON.Objects[i].RespuestaUsuario + '</span></li>';
                }
                //numeracion = i + 1;
                //listRespuestas.push(Response.responseJSON.Objects[i].RespuestaUsuario);
                //$('#' + IdForTotal).text('Total de respuestas: ' + Response.responseJSON.Objects.length);
                //$('#' + DivIdForPregunta).append('<p> ' + numeracion + '.-' + Response.responseJSON.Objects[i].RespuestaUsuario + ' </p>');
                $('#' + DivIdForPregunta).append(
                    '<div class="bg-white p-4">' + 
                        '<p><strong> ' + Pregunta + ' </strong></p>' + 
                        '<div class="row center-vertically">' + 
                        '<div class="col-sm-4 col-md-3 col-xl-2">' + 
                        '<div class="jumbotron p-2 mb-0 text-center">' + 
                        '<h1 class="text-white"> ' + Response.responseJSON.Objects.length + ' </h1><p class="color mb-0">Respuestas</p>' +
                        '</div>' +
                        '</div>' +
                        '<div class="col-sm-8 col-md-9 col-xl-10">' +
                        '<ol>' +
                        lista +
                        '</ol>' +
                        '</div></div></div>'
                );
            }
            else {
                swal('No se pudieron obtener las respuestas de la pregunta ' + Pregunta, '', 'info');
            }
        }
    });
}
//**************************************************************************************
function createBarrasFaceFeel(itemPregunta, idTipoControl) {
    console.log('Creando grafico FaceFeel');
    let modelPregunta = {
        idPregunta: itemPregunta.IdPregunta,
        idEncuesta: IdEncuesta
    };
    $.ajax({
        url: '@Url.Action("GetRespuestasByIdPregunta")',
        data: modelPregunta,
        type: 'GET',
        complete: function (Response) {
            if (Response.responseJSON.Objects.length > 0) {
                console.log(Response.responseJSON);//Preguntas y frecuencias junto a pregunta
                var IdPregunta = Response.responseJSON.Objects[0].Preguntas.IdPregunta;
                var Pregunta = Response.responseJSON.Objects[0].Preguntas.Pregunta;

                var DivIdForPregunta = 'divForPregunta_' + IdPregunta;
                $('#mergeGraficoCaritas').append('<div id="' + DivIdForPregunta + '" class="mb-5" style="width: 100%; height: 400px;"></div>');

                var listRespuestas = ['Carita'];
                var listFrecuencias = ['Frecuencias'];
                        
                var numBadFace = 0;
                var numRegularFace = 0;
                var numHappyFace = 0;
                var numHappierFace = 0;
                //happierface
                //happyface
                for (var i = 0; i < Response.responseJSON.Objects.length; i++) {
                    if (Response.responseJSON.Objects[i].RespuestaUsuario == 'badface') {
                        numBadFace = numBadFace + 1;
                    }
                    if (Response.responseJSON.Objects[i].RespuestaUsuario == 'regularface') {
                        numRegularFace = numRegularFace + 1;
                    }
                    if (Response.responseJSON.Objects[i].RespuestaUsuario == 'happyface') {
                        numHappyFace = numHappyFace + 1;
                    }
                    if (Response.responseJSON.Objects[i].RespuestaUsuario == 'happierface') {
                        numHappierFace = numHappierFace + 1;
                    }
                }
                listFrecuencias.push(numBadFace);
                listFrecuencias.push(numRegularFace);
                listFrecuencias.push(numHappyFace);
                listFrecuencias.push(numHappierFace);
                listRespuestas.push('BadFace');
                listRespuestas.push('RegularFace');
                listRespuestas.push('HappyFace');
                listRespuestas.push('HappierFace');
                        

                //FillGraphic--Descomentar lo siguiente
                google.charts.load("current", { packages: ["corechart"] });
                google.charts.setOnLoadCallback(drawChart);
                function drawChart() {

                    var data = google.visualization.arrayToDataTable([
                        [listRespuestas[0], listFrecuencias[0]],
                        [listRespuestas[1], listFrecuencias[1]],
                        [listRespuestas[2], listFrecuencias[2]],
                        [listRespuestas[3], listFrecuencias[3]],
                        [listRespuestas[4], listFrecuencias[4]]
                    ]);

                    var options = {
                        title: Pregunta,
                        width: '100%',
                        height: 400,
                        bar: { groupWidth: '95%' },
                        legend: { position: 'none' },
                    };
                    var chart = new google.visualization.ColumnChart(document.getElementById(DivIdForPregunta));
                    chart.draw(data, options);
                }
                //End Fill
            }
            else {
                swal('No se pudieron obtener las respuestas de la pregunta ' + Pregunta, '', 'info');
            }
        }
    });
}

$("body").mousemove(function (e) {
    $.ajax({
        type: 'POST',
        data: 2, traditional: true,
        url: '/LoginAdmin/IsLogged',
        contentType: 'application/json; charset=utf-8',
        succes: function (Response) {
            if (Response == "success") {
                console.log('Sesion activa');
            }
            if (Response == "error") {
                alert('Session expirada');
            }

        },
        complete: function (Response) {
            if (Response.responseJSON == "success") {
                console.log('Sesion activa');
            }
            if (Response.responseJSON == "error") {
                event.preventDefault();
                swal({
                    title: "La session ha expirado, serás redireccionado al Login",
                    text: "",
                    type: "error",
                    icon: "error",
                    closeOnClickOutside: false,
                }).then(function () {
                    window.location.href = "/LoginAdmin/Login";
                });
            }
            if (Response.responseJSON == "successAndEmails") {
                swal({
                    title: "El proceso de envío de email ha terminado. Presiona Ok para ver el estatus",
                    text: "",
                    type: "info",
                    icon: "info",
                    closeOnClickOutside: false,
                }).then(function () {
                    window.location.href = "/Encuesta/successEmail/";
                });
            }
            if (Response.responseJSON == "errorEmail") {
                swal({
                    title: "El proceso de envío de email ha terminado. Presiona Ok para ver el estatus",
                    text: "",
                    type: "info",
                    icon: "info",
                    closeOnClickOutside: false,
                }).then(function () {
                    window.location.href = "/Encuesta/errorEmail/";
                });
            }
        }
    });
});