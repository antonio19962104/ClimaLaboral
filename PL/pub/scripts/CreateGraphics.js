function graphicOpenAnswers(model) {
    $.ajax({
        url: '/ReporteD4U/GetRespuestasByIdPregunta/',
        data: model,
        type: 'GET',
        complete: function (Response) {
            if (Response.responseJSON != null) {
                console.log(Response.responseJSON);//Preguntas y frecuencias junto a pregunta

                var IdPregunta = 0;
                var Pregunta = "";
                try {
                    IdPregunta = Response.responseJSON.Objects[0].Preguntas.IdPregunta;
                    Pregunta = Response.responseJSON.Objects[0].Preguntas.Pregunta;
                } catch (e) {
                    IdPregunta = Response.responseJSON.Objects[0].Pregunta.IdPregunta;
                    Pregunta = Response.responseJSON.Objects[0].Pregunta.Pregunta;
                }

                try {
                    IdPregunta = Response.responseJSON.Objects[0].Pregunta.IdPregunta;
                    Pregunta = Response.responseJSON.Objects[0].Pregunta.Pregunta;
                } catch (e) {
                    IdPregunta = Response.responseJSON.Objects[0].Preguntas.IdPregunta;
                    Pregunta = Response.responseJSON.Objects[0].Preguntas.Pregunta;
                }

                var IdForTotal = 'showTotal' + IdPregunta;
                var DivIdForPregunta = 'divForPregunta_' + IdPregunta;
                var divPadre = 'padreForIdPregunta_' + IdPregunta;
                //listDivsPregunta.push(divPadre);
                //$('#mergeAllExceptOpenAndCarita').append('<div class="col-lg-6 mb-3" id="' + divPadre + '">   <div id="' + DivIdForPregunta + '" class="mb-5" style="width: 100%;"><div class="' + DivIdForPregunta + '"></div></div>  </div>');

                var listRespuestas = [];
                //var IdRespuesta = Response.responseJSON.Respuesta.IdRespuesta;
                //var Respuesta = Response.responseJSON.Respuesta.Respuesta;
                var lista = '';


                if (Response.responseJSON.Objects.length > 5) {
                    for (var i = 0; i < 5; i++) {
                        var numeral = i + 1;
                        var Respuesta = Response.responseJSON.Objects[i].RespuestaUsuario;
                        lista += '<li class="tooltip">' + numeral + '.-' + Respuesta.substr(0, 100) + '<span class="tooltiptext">' + Response.responseJSON.Objects[i].RespuestaUsuario + '</span></li>';
                    }
                }
                else {
                    for (var i = 0; i < Response.responseJSON.Objects.length; i++) {
                        var numeral = i + 1;
                        var Respuesta = Response.responseJSON.Objects[i].RespuestaUsuario;
                        lista += '<li class="tooltip">' + numeral + '.-' + Respuesta.substr(0, 100) + '<span class="tooltiptext">' + Response.responseJSON.Objects[i].RespuestaUsuario + '</span></li>';
                    }
                }

                $('#padreForIdPregunta_' + DivIdForPregunta.split('_')[1]).append(
                    '<div class="bg-white p-4">' +
                    '<p><strong> ' + Pregunta + ' </strong></p>' +
                    '<div class="row center-vertically">' +
                    '<div class="col-sm-4 col-md-4 col-xl-4" align="center">' +
                    '<div class="jumbotron p-2 mb-0 text-center">' +
                    '<h1 class="text-white"> ' + Response.responseJSON.Objects.length + ' </h1><p class="color mb-0">Respuestas</p>' +
                    '</div>' +
                    '</div>' +
                    '<div class="col-sm-8 col-md-8 col-xl-8">' +
                    '<ol>' +
                    lista +
                    '</ol>' +
                    '</div></div></div>'
                );
           
                var listRespuestas = '[';
                for (var i = 0; i < Response.responseJSON.Objects.length; i++) {
                    var res = '';
                    if (Response.responseJSON.Objects[i].RespuestaUsuario == null) {
                        Response.responseJSON.Objects[i].RespuestaUsuario = '';
                    }
                    try {
                        res = Response.responseJSON.Objects[i].RespuestaUsuario.trim();
                    } catch (e) {
                        res = Response.responseJSON.Objects[i].RespuestaUsuario;
                    }
                    res = res.replace('"', '');
                    res = res.replace(/  /g, " ");
                    res = res.replace(/   /g, " ");
                    res = res.replace(/    /g, " ");
                    res = res.replace(/     /g, " ");
                    res = res.replace(/      /g, " ");
                    res = res.replace(/       /g, " ");
                    res = res.replace(/        /g, " ");
                    res = res.replace(/         /g, " ");
                    res = res.replace(/          /g, " ");
                    res = res.replace(/\n|\r/g, " ");
                    res = res.replace('/', '');
                    res = res.replace('"', '');
                    res = res.replace('\\', '');
                    res = res.replace('"', '');
                    res = res.replace(/\n|\r/g, " ");
                    res = res.replace(/[`^\-?;:'",<>\{\}\[\]\\\/]/gi, '');
                    listRespuestas += '{"Respuesta":"' + res + '"},'
                }
                listRespuestas = listRespuestas.substring(0, listRespuestas.length - 1);
                listRespuestas += ']';
                var json = JSON.stringify(listRespuestas);
                ConvertJSONToCsv(listRespuestas, Pregunta, Pregunta, divPadre);
                $('#loadImage').remove();
                $('#padreForIdPregunta_' + IdPregunta).css('text-align', '');
                updateNumerarPreguntas();
            }
            else {

            }
        }
    });
}

/**/

function graphicCake(model) {
    console.log('Creando grafico de Pastel');
    $.ajax({
        url: '/ReporteD4U/GetRespuestasByIdPregunta/',
        data: model,
        type: 'GET',
        complete: function (Response) {
            if (Response.responseJSON != null) {
                console.log(Response.responseJSON);//Preguntas y frecuencias junto a pregunta
                var IdPregunta = 0;
                var Pregunta = "";
                try {
                    IdPregunta = Response.responseJSON.Objects[0].Pregunta.IdPregunta;
                    Pregunta = Response.responseJSON.Objects[0].Pregunta.Pregunta;
                } catch (e) {
                    IdPregunta = Response.responseJSON.Objects[0].Preguntas.IdPregunta;
                    Pregunta = Response.responseJSON.Objects[0].Preguntas.Pregunta;
                }

                if (IdPregunta == 4005) {
                    console.log(Response.responseJSON.ObjectsAux);
                }

                var DivIdForPregunta = 'divForPregunta_' + IdPregunta;
                var divPadre = 'padreForIdPregunta_' + IdPregunta
                //listDivsPregunta.push(divPadre);
                //$('#mergeAllExceptOpenAndCarita').append('<div class="col-sm-12 col-md-6 col-lg-4 mb-3 container-graph" id="' + divPadre + '">   <div id="' + DivIdForPregunta + '" class="mb-5" style="width: 100%; height: 400px;"><div class="' + DivIdForPregunta + '"></div></div> </div>');

                var listRespuestas = ['Respuestas'];
                var listFrecuencias = ['Frecuencias'];

                var listRespuestasAux = [];
                for (var i = 0; i < Response.responseJSON.Objects.length; i++) {
                    listRespuestasAux.push(Response.responseJSON.Objects[i].Respuesta);
                }

                if (Response.responseJSON.Objects[0].Pregunta.TipoControl.IdTipoControl == 3) {//Check
                    for (var i = 0; i < Response.responseJSON.Objects.length; i++) {
                        //1: "Lavadora"
                        //2: "Plancha"
                        //3: "Plancha"
                        //4: "Micro"
                        //5: "Micro"
                        //6: "Refrigerador"
                        //7: "Refrigerador"
                        if (!listRespuestas.includes(Response.responseJSON.Objects[i].Respuesta)) {
                            listRespuestas.push(Response.responseJSON.Objects[i].Respuesta);
                            var count = 0;
                            for (var j = 0; j < listRespuestasAux.length; ++j) {
                                if (listRespuestasAux[j] == Response.responseJSON.Objects[i].Respuesta)
                                    count++;
                            }
                            console.log(count);
                            listFrecuencias.push(count);
                        }
                    }
                }
                else {
                    for (var i = 0; i < Response.responseJSON.Objects.length; i++) {
                        listRespuestas.push(Response.responseJSON.Objects[i].Respuesta);
                        listFrecuencias.push(Response.responseJSON.Objects[i].conteoByPregunta);
                    }
                }

                

                    var data = google.visualization.arrayToDataTable([
                        [listRespuestas[0], listFrecuencias[0]],
                        [listRespuestas[1], listFrecuencias[1]],
                        [listRespuestas[2], listFrecuencias[2]],
                        [listRespuestas[3], listFrecuencias[3]],
                        [listRespuestas[4], listFrecuencias[4]],
                        [listRespuestas[5], listFrecuencias[5]],
                        [listRespuestas[6], listFrecuencias[6]],
                        [listRespuestas[7], listFrecuencias[7]],
                        [listRespuestas[8], listFrecuencias[8]],
                        [listRespuestas[9], listFrecuencias[9]],
                        [listRespuestas[10], listFrecuencias[10]],
                        [listRespuestas[11], listFrecuencias[11]],
                        [listRespuestas[12], listFrecuencias[12]],
                        [listRespuestas[13], listFrecuencias[13]],
                        [listRespuestas[14], listFrecuencias[14]],
                        [listRespuestas[15], listFrecuencias[15]],
                        [listRespuestas[16], listFrecuencias[16]],
                        [listRespuestas[17], listFrecuencias[17]],
                        [listRespuestas[18], listFrecuencias[18]],
                        [listRespuestas[19], listFrecuencias[19]],
                        [listRespuestas[20], listFrecuencias[20]],
                        [listRespuestas[21], listFrecuencias[21]],
                        [listRespuestas[22], listFrecuencias[22]],
                        [listRespuestas[23], listFrecuencias[23]],
                        [listRespuestas[24], listFrecuencias[24]],
                        [listRespuestas[25], listFrecuencias[25]]
                    ]);

                    //var options = {
                    //    title: Pregunta,
                    //    pieHole: 0.4,
                    //};
                    //var chart = new google.visualization.PieChart(document.getElementById(DivIdForPregunta));
                    //chart.draw(data, options);
                    CreateGraphicsRB(data, DivIdForPregunta, Pregunta);

                //}
                //End Fill
                var json = [
                    { 'Respuesta': listRespuestas[1], 'Frecuencia': listFrecuencias[1] },
                    { 'Respuesta': listRespuestas[2], 'Frecuencia': listFrecuencias[2] },
                    { 'Respuesta': listRespuestas[3], 'Frecuencia': listFrecuencias[3] },
                    { 'Respuesta': listRespuestas[4], 'Frecuencia': listFrecuencias[4] },
                    { 'Respuesta': listRespuestas[5], 'Frecuencia': listFrecuencias[5] },
                    { 'Respuesta': listRespuestas[6], 'Frecuencia': listFrecuencias[6] },
                    { 'Respuesta': listRespuestas[7], 'Frecuencia': listFrecuencias[7] },
                    { 'Respuesta': listRespuestas[8], 'Frecuencia': listFrecuencias[8] },
                    { 'Respuesta': listRespuestas[9], 'Frecuencia': listFrecuencias[9] },
                    { 'Respuesta': listRespuestas[10], 'Frecuencia': listFrecuencias[10] },
                    { 'Respuesta': listRespuestas[11], 'Frecuencia': listFrecuencias[11] },
                    { 'Respuesta': listRespuestas[12], 'Frecuencia': listFrecuencias[12] },
                    { 'Respuesta': listRespuestas[13], 'Frecuencia': listFrecuencias[13] },
                    { 'Respuesta': listRespuestas[14], 'Frecuencia': listFrecuencias[14] },
                    { 'Respuesta': listRespuestas[15], 'Frecuencia': listFrecuencias[15] },
                ]
                ConvertJSONToCsv(json, Pregunta, Pregunta, divPadre);
                $('#loadImage').remove();
                $('#padreForIdPregunta_' + IdPregunta).css('text-align', '');
                updateNumerarPreguntas();
            }
            else {

            }
        }
    });
}

/**/

function graphicBarrasVertical(model) {
    $.ajax({
        url: '/ReporteD4U/GetRespuestasByIdPregunta/',
        data: model,
        type: 'GET',
        complete: function (Response) {
            if (Response.responseJSON != null) {
                if (model.IdentificadorTipoControl == 7) {
                    //Rango
                    console.log(Response.responseJSON);//Preguntas y frecuencias junto a pregunta
                    var IdPregunta = 0;
                    var Pregunta = "";
                    try {
                        IdPregunta = Response.responseJSON.Objects[0].Preguntas.IdPregunta;
                        Pregunta = Response.responseJSON.Objects[0].Preguntas.Pregunta;
                    } catch (e) {
                        IdPregunta = Response.responseJSON.Objects[0].Pregunta.IdPregunta;
                        Pregunta = Response.responseJSON.Objects[0].Pregunta.Pregunta;
                    }


                    var DivIdForPregunta = 'divForPregunta_' + IdPregunta;
                    var divForPadre = 'padreForIdPregunta_' + IdPregunta;
                    //listDivsPregunta.push(divForPadre);
                    //$('#mergeAllExceptOpenAndCarita').append('<div class="col-sm-12 col-md-6 col-lg-4 mb-3 container-graph" id="' + divForPadre + '" >    <div id="' + DivIdForPregunta + '" class="mb-5" style="width: 100%; height: 400px;"><div class="' + DivIdForPregunta + '"></div></div>  </div>');

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
                            [Pregunta, listFrecuencias[11]],
                            [Pregunta, listFrecuencias[12]],
                            [Pregunta, listFrecuencias[13]],
                            [Pregunta, listFrecuencias[14]],
                            [Pregunta, listFrecuencias[15]],
                            [Pregunta, listFrecuencias[16]],
                            [Pregunta, listFrecuencias[17]],
                            [Pregunta, listFrecuencias[18]],
                            [Pregunta, listFrecuencias[19]],
                            [Pregunta, listFrecuencias[20]],
                            [Pregunta, listFrecuencias[21]],
                            [Pregunta, listFrecuencias[22]],
                            [Pregunta, listFrecuencias[23]],
                            [Pregunta, listFrecuencias[24]]
                        ]);

                        var options = {
                            title: Pregunta,
                            legend: { position: 'none' }
                        };
                        var chart = new google.visualization.Histogram(document.getElementById(DivIdForPregunta));
                        chart.draw(data, options);
                        var json = [
                            { 'Respuesta': Pregunta, 'Frecuencia': listFrecuencias[1] },
                            { 'Respuesta': Pregunta, 'Frecuencia': listFrecuencias[2] },
                            { 'Respuesta': Pregunta, 'Frecuencia': listFrecuencias[3] },
                            { 'Respuesta': Pregunta, 'Frecuencia': listFrecuencias[4] },
                            { 'Respuesta': Pregunta, 'Frecuencia': listFrecuencias[5] },
                            { 'Respuesta': Pregunta, 'Frecuencia': listFrecuencias[6] },
                            { 'Respuesta': Pregunta, 'Frecuencia': listFrecuencias[7] },
                            { 'Respuesta': Pregunta, 'Frecuencia': listFrecuencias[8] },
                            { 'Respuesta': Pregunta, 'Frecuencia': listFrecuencias[9] },
                            { 'Respuesta': Pregunta, 'Frecuencia': listFrecuencias[10] },
                            { 'Respuesta': Pregunta, 'Frecuencia': listFrecuencias[11] },
                            { 'Respuesta': Pregunta, 'Frecuencia': listFrecuencias[12] },
                            { 'Respuesta': Pregunta, 'Frecuencia': listFrecuencias[13] },
                            { 'Respuesta': Pregunta, 'Frecuencia': listFrecuencias[14] },
                            { 'Respuesta': Pregunta, 'Frecuencia': listFrecuencias[15] },
                            { 'Respuesta': Pregunta, 'Frecuencia': listFrecuencias[16] },
                            { 'Respuesta': Pregunta, 'Frecuencia': listFrecuencias[17] },
                            { 'Respuesta': Pregunta, 'Frecuencia': listFrecuencias[18] },
                            { 'Respuesta': Pregunta, 'Frecuencia': listFrecuencias[19] },
                            { 'Respuesta': Pregunta, 'Frecuencia': listFrecuencias[20] },
                            { 'Respuesta': Pregunta, 'Frecuencia': listFrecuencias[21] },
                            { 'Respuesta': Pregunta, 'Frecuencia': listFrecuencias[22] },
                            { 'Respuesta': Pregunta, 'Frecuencia': listFrecuencias[23] },
                            { 'Respuesta': Pregunta, 'Frecuencia': listFrecuencias[24] },
                            { 'Respuesta': Pregunta, 'Frecuencia': listFrecuencias[25] },
                            { 'Respuesta': Pregunta, 'Frecuencia': listFrecuencias[26] },
                            { 'Respuesta': Pregunta, 'Frecuencia': listFrecuencias[27] },
                            { 'Respuesta': Pregunta, 'Frecuencia': listFrecuencias[28] },
                            { 'Respuesta': Pregunta, 'Frecuencia': listFrecuencias[29] },
                            { 'Respuesta': Pregunta, 'Frecuencia': listFrecuencias[30] },
                            { 'Respuesta': Pregunta, 'Frecuencia': listFrecuencias[31] },
                        ]
                        ConvertJSONToCsv(json, Pregunta, Pregunta, divForPadre);
                        $('#padreForIdPregunta_' + model.IdPregunta).css('text-align', '');
                        updateNumerarPreguntas();
                    }
                    //End Fill
                }
                else {
                    console.log(Response.responseJSON);//Preguntas y frecuencias junto a pregunta

                    var IdPregunta = 0;
                    var Pregunta = "";
                    try {
                        IdPregunta = Response.responseJSON.Objects[0].Preguntas.IdPregunta;
                        Pregunta = Response.responseJSON.Objects[0].Preguntas.Pregunta;
                    } catch (e) {
                        IdPregunta = Response.responseJSON.Objects[0].Pregunta.IdPregunta;
                        Pregunta = Response.responseJSON.Objects[0].Pregunta.Pregunta;
                    }


                    //var IdPregunta = Response.responseJSON.Objects[0].Pregunta.IdPregunta;
                    //var Pregunta = Response.responseJSON.Objects[0].Pregunta.Pregunta;

                    var DivIdForPregunta = 'divForPregunta_' + IdPregunta;
                    var divPadre = 'padreForIdPregunta_' + IdPregunta;
                    //listDivsPregunta.push(divPadre);
                    //$('#mergeAllExceptOpenAndCarita').append('<div class="col-sm-12 col-md-6 col-lg-4 mb-3 container-graph" id="' + divPadre + '" >    <div id="' + DivIdForPregunta + '" class="mb-5" style="width: 100%; height: 400px;"><div class="' + DivIdForPregunta + '"></div></div>  </div>');
                    var listRespuestas = ['Respuestas'];
                    var listFrecuencias = ['Frecuencias'];
                    //var IdRespuesta = Response.responseJSON.Respuesta.IdRespuesta;
                    //var Respuesta = Response.responseJSON.Respuesta.Respuesta;
                    for (var i = 0; i < Response.responseJSON.Objects.length; i++) {
                        listRespuestas.push(Response.responseJSON.Objects[i].Respuesta);
                        listFrecuencias.push(Response.responseJSON.Objects[i].conteoByPregunta);
                    }

                    
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

                    CreateGraphicsSelect(data, DivIdForPregunta, Pregunta);
                    //End Fill
                    var json = [
                        { 'Respuesta': listRespuestas[1], 'Frecuencia': listFrecuencias[1] },
                        { 'Respuesta': listRespuestas[2], 'Frecuencia': listFrecuencias[2] },
                        { 'Respuesta': listRespuestas[3], 'Frecuencia': listFrecuencias[3] },
                        { 'Respuesta': listRespuestas[4], 'Frecuencia': listFrecuencias[4] },
                        { 'Respuesta': listRespuestas[5], 'Frecuencia': listFrecuencias[5] },
                        { 'Respuesta': listRespuestas[6], 'Frecuencia': listFrecuencias[6] },
                        { 'Respuesta': listRespuestas[7], 'Frecuencia': listFrecuencias[7] },
                        { 'Respuesta': listRespuestas[8], 'Frecuencia': listFrecuencias[8] },
                        { 'Respuesta': listRespuestas[9], 'Frecuencia': listFrecuencias[9] },
                        { 'Respuesta': listRespuestas[10], 'Frecuencia': listFrecuencias[10] },
                        { 'Respuesta': listRespuestas[11], 'Frecuencia': listFrecuencias[11] },
                        { 'Respuesta': listRespuestas[12], 'Frecuencia': listFrecuencias[12] },
                        { 'Respuesta': listRespuestas[13], 'Frecuencia': listFrecuencias[13] },
                        { 'Respuesta': listRespuestas[14], 'Frecuencia': listFrecuencias[14] },
                        { 'Respuesta': listRespuestas[15], 'Frecuencia': listFrecuencias[15] },
                    ]
                    ConvertJSONToCsv(json, Pregunta, Pregunta, divPadre);
                    $('#padreForIdPregunta_' + model.IdPregunta).css('text-align', '');
                    updateNumerarPreguntas();
                }
            }
            else {

            }
        }
    });
    $('#padreForIdPregunta_' + model.IdPregunta).css('text-align', '');
}

/**/

function graphicBarrasFaceFeel(model) {
    $.ajax({
        url: '/ReporteD4U/GetRespuestasByIdPregunta/',
        data: model,
        type: 'GET',
        complete: function (Response) {
            if (Response.responseJSON.Objects.length > 0) {
                console.log(Response.responseJSON);//Preguntas y frecuencias junto a pregunta
                var IdPregunta = Response.responseJSON.Objects[0].Preguntas.IdPregunta;
                var Pregunta = Response.responseJSON.Objects[0].Preguntas.Pregunta;

                var DivIdForPregunta = 'divForPregunta_' + IdPregunta;


                var divPadre = 'padreForIdPregunta_' + IdPregunta;
                //listDivsPregunta.push(divPadre);
                //$('#mergeAllExceptOpenAndCarita').append(
                //    '<div class="col-lg-6 mb-3 container-graph" id="' + divPadre + '">' +
                //    '<div id="" class="mb-3" style="width: 100%;">' +
                //    '<div class="bg-white p-4">' +
                //    '<p><strong>' + '' + '</strong></p>' +
                //    '<div class="row center-vertically"><div class="col-sm-4 col-md-4 col-xl-4" align="center"><div class="jumbotron p-2 mb-0 text-center">' +
                //    '<h1 class="text-white">' + Response.responseJSON.Objects.length + '</h1>' +
                //    '<p class="color mb-0">Respuestas</p></div></div>' +
                //    '<div class="col-sm-8 col-md-8 col-xl-8">' +
                //    '<div id="' + DivIdForPregunta + '" style="width: 100%; height: auto;"></div>  <div class="' + DivIdForPregunta + '"></div>' +
                //    '</div></div></div></div></div>'
                //);

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
                var json = [
                    { 'Respuesta': listRespuestas[1], 'Frecuencia': listFrecuencias[1] },
                    { 'Respuesta': listRespuestas[2], 'Frecuencia': listFrecuencias[2] },
                    { 'Respuesta': listRespuestas[3], 'Frecuencia': listFrecuencias[3] },
                    { 'Respuesta': listRespuestas[4], 'Frecuencia': listFrecuencias[4] },
                    { 'Respuesta': listRespuestas[5], 'Frecuencia': listFrecuencias[5] },
                    { 'Respuesta': listRespuestas[6], 'Frecuencia': listFrecuencias[6] },
                    { 'Respuesta': listRespuestas[7], 'Frecuencia': listFrecuencias[7] },
                    { 'Respuesta': listRespuestas[8], 'Frecuencia': listFrecuencias[8] },
                    { 'Respuesta': listRespuestas[9], 'Frecuencia': listFrecuencias[9] },
                    { 'Respuesta': listRespuestas[10], 'Frecuencia': listFrecuencias[10] },
                    { 'Respuesta': listRespuestas[11], 'Frecuencia': listFrecuencias[11] },
                    { 'Respuesta': listRespuestas[12], 'Frecuencia': listFrecuencias[12] },
                    { 'Respuesta': listRespuestas[13], 'Frecuencia': listFrecuencias[13] },
                    { 'Respuesta': listRespuestas[14], 'Frecuencia': listFrecuencias[14] },
                    { 'Respuesta': listRespuestas[15], 'Frecuencia': listFrecuencias[15] },
                ]
                ConvertJSONToCsv(json, Pregunta, Pregunta, divPadre);
                $('#padreForIdPregunta_' + model.IdPregunta).css('text-align', '');
                updateNumerarPreguntas();
            }
            else {

            }
        }
    });
}

/**/

function graphicBarrasHorizontal(IdEncuesta) {//ready function
    //Likerts => OK
    //Obtener los resultados no de una sino de todas las preguntas de tipo likert en la encuesta
    let model = { idEncuesta: IdEncuesta };
    var subseccionAnterior = 0;
    $.ajax({
        url: '/ReporteD4U/GetPreguntasLikertExceptDobleByEncuesta/',
        data: model,
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        complete: function (Response) {
            if (Response != null) {
                var json = null;
                var existenSubsecciones = false;
                var DivIdForPregunta = '';
                try {
                    for (var i = 0; i < Response.responseJSON.Data.length; i++) {
                        if (Response.responseJSON.Data[i].SubSeccion > 0) {
                            existenSubsecciones = true;
                        }
                    }
                    for (var i = 0; i < Response.responseJSON.Data.length; i++) {
                        if (existenSubsecciones == true) {
                            var IdPregunta = Response.responseJSON.Data[i].IdPregunta;//Tomo solo la primera de la lista como referencia
                            DivIdForPregunta = 'divForPregunta_' + IdPregunta;
                            var divPadre = 'padreForIdPregunta_' + IdPregunta
                            listDivsPregunta.push(divPadre);
                            $('#mergeAllExceptOpenAndCarita').append('<div class="col-sm-12 col-md-6 col-lg-4 mb-3 container-graph" id="' + divPadre + '" >   <div id="' + DivIdForPregunta + '" class="mb-5" style="width: 100%; height: 400px;"><div class="' + DivIdForPregunta + '"></div></div>  </div>');
                        }
                        else {
                            var IdPregunta = Response.responseJSON.Data[0].IdPregunta;//Tomo solo la primera de la lista como referencia
                            DivIdForPregunta = 'divForPregunta_' + IdPregunta;
                            var divPadre = 'padreForIdPregunta_' + IdPregunta
                            listDivsPregunta.push(divPadre);
                            $('#mergeAllExceptOpenAndCarita').append('<div class="col-sm-12 col-md-6 col-lg-4 mb-3 container-graph" id="' + divPadre + '" >   <div id="' + DivIdForPregunta + '" class="mb-5" style="width: 100%; height: 400px;"><div class="' + DivIdForPregunta + '"></div></div>  </div>');
                            break;
                        }
                    }
                } catch (e) {

                }
                var dataForGraphics = [];

                var flag = 0;
                for (var i = 0; i < Response.responseJSON.Data.length; i++) {
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



                    if (existenSubsecciones == false) {
                        if (Response.responseJSON.Data[i].TipoControl.IdTipoControl == 12) {
                            try {
                                var colA = Response.responseJSON.Data[i].listUsuarioResp[0].Respuestas.Respuesta;
                                var colB = Response.responseJSON.Data[i].listUsuarioResp[1].Respuestas.Respuesta;
                                var preguntaColA = '';
                                var preguntaColB = '';
                                for (var k = 0; k < Response.responseJSON.Data[i].ListPreguntasLikert.length; k++) {
                                    preguntaColA = Response.responseJSON.Data[i].ListPreguntasLikert[k].PreguntaLikert + '(' + colA + ')';
                                    preguntaColB = Response.responseJSON.Data[i].ListPreguntasLikert[k].PreguntaLikert + '(' + colB + ')';
                                    //}
                                    var IdPreguntaLikertDobleaCTUAL = Response.responseJSON.Data[i].ListPreguntasLikert[k].IdPreguntaLikert;

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

                                    var numPreguntas = Response.responseJSON.Data[i].ListPreguntasLikert.length;//numero de preguntas likert
                                    numPreguntas = numPreguntas * 2;


                                    for (var j = 0; j < Response.responseJSON.Data[i].listUsuarioResp.length; j++) {//Traer el id de pregunta likert
                                        //var IdPreguntaLikertDobleaCTUAL = Response.responseJSON.Objects[i].listUsuarioResp[j].Respuestas.PreguntasLikert.IdPreguntaLikert;//Aqui esta el Id de pregunta likert
                                        if (Response.responseJSON.Data[i].listUsuarioResp[j].RespuestaUsuario == '1' && Response.responseJSON.Data[i].listUsuarioResp[j].Respuestas.PreguntasLikert.IdPreguntaLikert == IdPreguntaLikertDobleaCTUAL && Response.responseJSON.Data[i].listUsuarioResp[j].Respuestas.Respuesta == colA) {
                                            CasiSiempreFalsoColA = CasiSiempreFalsoColA + 1;
                                        }
                                        if (Response.responseJSON.Data[i].listUsuarioResp[j].RespuestaUsuario == '2' && Response.responseJSON.Data[i].listUsuarioResp[j].Respuestas.PreguntasLikert.IdPreguntaLikert == IdPreguntaLikertDobleaCTUAL && Response.responseJSON.Data[i].listUsuarioResp[j].Respuestas.Respuesta == colA) {
                                            FrecuentementeFalsoColA = FrecuentementeFalsoColA + 1;
                                        }
                                        if (Response.responseJSON.Data[i].listUsuarioResp[j].RespuestaUsuario == '3' && Response.responseJSON.Data[i].listUsuarioResp[j].Respuestas.PreguntasLikert.IdPreguntaLikert == IdPreguntaLikertDobleaCTUAL && Response.responseJSON.Data[i].listUsuarioResp[j].Respuestas.Respuesta == colA) {
                                            AVecesVerdadAVecesFalsoColA = AVecesVerdadAVecesFalsoColA + 1;
                                        }
                                        if (Response.responseJSON.Data[i].listUsuarioResp[j].RespuestaUsuario == '4' && Response.responseJSON.Data[i].listUsuarioResp[j].Respuestas.PreguntasLikert.IdPreguntaLikert == IdPreguntaLikertDobleaCTUAL && Response.responseJSON.Data[i].listUsuarioResp[j].Respuestas.Respuesta == colA) {
                                            FrecuentementeVerdadColA = FrecuentementeVerdadColA + 1;
                                        }
                                        if (Response.responseJSON.Data[i].listUsuarioResp[j].RespuestaUsuario == '5' && Response.responseJSON.Data[i].listUsuarioResp[j].Respuestas.PreguntasLikert.IdPreguntaLikert == IdPreguntaLikertDobleaCTUAL && Response.responseJSON.Data[i].listUsuarioResp[j].Respuestas.Respuesta == colA) {
                                            CasiSiempreVerdadColA = CasiSiempreVerdadColA + 1;
                                        }
                                    }


                                    for (var j = 0; j < Response.responseJSON.Data[i].listUsuarioResp.length; j++) {//Traer el id de pregunta likert
                                        //var IdPreguntaLikertDobleaCTUAL = Response.responseJSON.Objects[i].listUsuarioResp[j].Respuestas.PreguntasLikert.IdPreguntaLikert;//Aqui esta el Id de pregunta likert
                                        if (Response.responseJSON.Data[i].listUsuarioResp[j].RespuestaUsuario == '1' && Response.responseJSON.Data[i].listUsuarioResp[j].Respuestas.PreguntasLikert.IdPreguntaLikert == IdPreguntaLikertDobleaCTUAL && Response.responseJSON.Data[i].listUsuarioResp[j].Respuestas.Respuesta == colB) {
                                            CasiSiempreFalsoColB = CasiSiempreFalsoColB + 1;
                                        }
                                        if (Response.responseJSON.Data[i].listUsuarioResp[j].RespuestaUsuario == '2' && Response.responseJSON.Data[i].listUsuarioResp[j].Respuestas.PreguntasLikert.IdPreguntaLikert == IdPreguntaLikertDobleaCTUAL && Response.responseJSON.Data[i].listUsuarioResp[j].Respuestas.Respuesta == colB) {
                                            FrecuentementeFalsoColB = FrecuentementeFalsoColB + 1;
                                        }
                                        if (Response.responseJSON.Data[i].listUsuarioResp[j].RespuestaUsuario == '3' && Response.responseJSON.Data[i].listUsuarioResp[j].Respuestas.PreguntasLikert.IdPreguntaLikert == IdPreguntaLikertDobleaCTUAL && Response.responseJSON.Data[i].listUsuarioResp[j].Respuestas.Respuesta == colB) {
                                            AVecesVerdadAVecesFalsoColB = AVecesVerdadAVecesFalsoColB + 1;
                                        }
                                        if (Response.responseJSON.Data[i].listUsuarioResp[j].RespuestaUsuario == '4' && Response.responseJSON.Data[i].listUsuarioResp[j].Respuestas.PreguntasLikert.IdPreguntaLikert == IdPreguntaLikertDobleaCTUAL && Response.responseJSON.Data[i].listUsuarioResp[j].Respuestas.Respuesta == colB) {
                                            FrecuentementeVerdadColB = FrecuentementeVerdadColB + 1;
                                        }
                                        if (Response.responseJSON.Data[i].listUsuarioResp[j].RespuestaUsuario == '5' && Response.responseJSON.Data[i].listUsuarioResp[j].Respuestas.PreguntasLikert.IdPreguntaLikert == IdPreguntaLikertDobleaCTUAL && Response.responseJSON.Data[i].listUsuarioResp[j].Respuestas.Respuesta == colB) {
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
                                    json = modelPreguntaJSColA;
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
                            } catch (e) {
                                //Pintar que aun no hay respuestas
                                //swal('Aún no se tienen respuestas en las preguntas de tipo Likert');
                            }
                        }
                        else {
                            var Pregunta = Response.responseJSON.Data[i].Pregunta;

                            CasiSiempreFalso = Response.responseJSON.Data[i].CSF;
                            FrecuentementeFalso = Response.responseJSON.Data[i].FF;
                            AVecesVerdadAVecesFalso = Response.responseJSON.Data[i].AV;
                            FrecuentementeVerdad = Response.responseJSON.Data[i].FV;
                            CasiSiempreVerdad = Response.responseJSON.Data[i].CSV;

                            modelPreguntaJS = {
                                Pregunta: Pregunta,
                                CSF: CasiSiempreFalso,
                                FF: FrecuentementeFalso,
                                AV: AVecesVerdadAVecesFalso,
                                FV: FrecuentementeVerdad,
                                CSV: CasiSiempreVerdad
                            };
                            dataForGraphics.push(modelPreguntaJS);
                            //CasiSiempreFalso = 0;
                            //FrecuentementeFalso = 0;
                            //AVecesVerdadAVecesFalso = 0;
                            //FrecuentementeVerdad = 0;
                            //CasiSiempreVerdad = 0;
                        }
                        var contador = 0;
                        console.log(dataForGraphics);
                        //FillGraphics
                        google.charts.load("current", { packages: ["corechart"] });
                        google.charts.setOnLoadCallback(drawChart);
                        function drawChart() {

                            var data = null;

                            var long = dataForGraphics.length;

                            var arrayDataForGraphics = CreateDataArray(dataForGraphics, long)

                            if (arrayDataForGraphics == null) {
                                $('#' + DivIdForPregunta).append('<h3>Aún no existen respuestas en la pregunta de tipo Likert para mostrar el gráfico</h3>');
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
                                chart.draw(arrayDataForGraphics, options);
                                if (contador == 0) {
                                    ConvertJSONToCsv(dataForGraphics, Pregunta, Pregunta, divPadre);
                                    contador = contador + 1;
                                }
                            }
                        }
                        //EndFill

                    }
                    else {
                        //getLikertDoble
                        if (Response.responseJSON.Data[i].TipoControl.IdTipoControl == 12 && Response.responseJSON.Data[i].TipoControl.IdTipoControl != 13) {
                            try {
                                var colA = Response.responseJSON.Data[i].listUsuarioResp[0].Respuestas.Respuesta;
                                var colB = Response.responseJSON.Data[i].listUsuarioResp[1].Respuestas.Respuesta;
                                var preguntaColA = '';
                                var preguntaColB = '';
                                for (var k = 0; k < Response.responseJSON.Data[i].ListPreguntasLikert.length; k++) {
                                    preguntaColA = Response.responseJSON.Data[i].ListPreguntasLikert[k].PreguntaLikert + '(' + colA + ')';
                                    preguntaColB = Response.responseJSON.Data[i].ListPreguntasLikert[k].PreguntaLikert + '(' + colB + ')';
                                    //}
                                    var IdPreguntaLikertDobleaCTUAL = Response.responseJSON.Data[i].ListPreguntasLikert[k].IdPreguntaLikert;

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

                                    var numPreguntas = Response.responseJSON.Data[i].ListPreguntasLikert.length;//numero de preguntas likert
                                    numPreguntas = numPreguntas * 2;


                                    for (var j = 0; j < Response.responseJSON.Data[i].listUsuarioResp.length; j++) {//Traer el id de pregunta likert
                                        //var IdPreguntaLikertDobleaCTUAL = Response.responseJSON.Objects[i].listUsuarioResp[j].Respuestas.PreguntasLikert.IdPreguntaLikert;//Aqui esta el Id de pregunta likert
                                        if (Response.responseJSON.Data[i].listUsuarioResp[j].RespuestaUsuario == '1' && Response.responseJSON.Data[i].listUsuarioResp[j].Respuestas.PreguntasLikert.IdPreguntaLikert == IdPreguntaLikertDobleaCTUAL && Response.responseJSON.Data[i].listUsuarioResp[j].Respuestas.Respuesta == colA) {
                                            CasiSiempreFalsoColA = CasiSiempreFalsoColA + 1;
                                        }
                                        if (Response.responseJSON.Data[i].listUsuarioResp[j].RespuestaUsuario == '2' && Response.responseJSON.Data[i].listUsuarioResp[j].Respuestas.PreguntasLikert.IdPreguntaLikert == IdPreguntaLikertDobleaCTUAL && Response.responseJSON.Data[i].listUsuarioResp[j].Respuestas.Respuesta == colA) {
                                            FrecuentementeFalsoColA = FrecuentementeFalsoColA + 1;
                                        }
                                        if (Response.responseJSON.Data[i].listUsuarioResp[j].RespuestaUsuario == '3' && Response.responseJSON.Data[i].listUsuarioResp[j].Respuestas.PreguntasLikert.IdPreguntaLikert == IdPreguntaLikertDobleaCTUAL && Response.responseJSON.Data[i].listUsuarioResp[j].Respuestas.Respuesta == colA) {
                                            AVecesVerdadAVecesFalsoColA = AVecesVerdadAVecesFalsoColA + 1;
                                        }
                                        if (Response.responseJSON.Data[i].listUsuarioResp[j].RespuestaUsuario == '4' && Response.responseJSON.Data[i].listUsuarioResp[j].Respuestas.PreguntasLikert.IdPreguntaLikert == IdPreguntaLikertDobleaCTUAL && Response.responseJSON.Data[i].listUsuarioResp[j].Respuestas.Respuesta == colA) {
                                            FrecuentementeVerdadColA = FrecuentementeVerdadColA + 1;
                                        }
                                        if (Response.responseJSON.Data[i].listUsuarioResp[j].RespuestaUsuario == '5' && Response.responseJSON.Data[i].listUsuarioResp[j].Respuestas.PreguntasLikert.IdPreguntaLikert == IdPreguntaLikertDobleaCTUAL && Response.responseJSON.Data[i].listUsuarioResp[j].Respuestas.Respuesta == colA) {
                                            CasiSiempreVerdadColA = CasiSiempreVerdadColA + 1;
                                        }
                                    }


                                    for (var j = 0; j < Response.responseJSON.Data[i].listUsuarioResp.length; j++) {//Traer el id de pregunta likert
                                        //var IdPreguntaLikertDobleaCTUAL = Response.responseJSON.Objects[i].listUsuarioResp[j].Respuestas.PreguntasLikert.IdPreguntaLikert;//Aqui esta el Id de pregunta likert
                                        if (Response.responseJSON.Data[i].listUsuarioResp[j].RespuestaUsuario == '1' && Response.responseJSON.Data[i].listUsuarioResp[j].Respuestas.PreguntasLikert.IdPreguntaLikert == IdPreguntaLikertDobleaCTUAL && Response.responseJSON.Data[i].listUsuarioResp[j].Respuestas.Respuesta == colB) {
                                            CasiSiempreFalsoColB = CasiSiempreFalsoColB + 1;
                                        }
                                        if (Response.responseJSON.Data[i].listUsuarioResp[j].RespuestaUsuario == '2' && Response.responseJSON.Data[i].listUsuarioResp[j].Respuestas.PreguntasLikert.IdPreguntaLikert == IdPreguntaLikertDobleaCTUAL && Response.responseJSON.Data[i].listUsuarioResp[j].Respuestas.Respuesta == colB) {
                                            FrecuentementeFalsoColB = FrecuentementeFalsoColB + 1;
                                        }
                                        if (Response.responseJSON.Data[i].listUsuarioResp[j].RespuestaUsuario == '3' && Response.responseJSON.Data[i].listUsuarioResp[j].Respuestas.PreguntasLikert.IdPreguntaLikert == IdPreguntaLikertDobleaCTUAL && Response.responseJSON.Data[i].listUsuarioResp[j].Respuestas.Respuesta == colB) {
                                            AVecesVerdadAVecesFalsoColB = AVecesVerdadAVecesFalsoColB + 1;
                                        }
                                        if (Response.responseJSON.Data[i].listUsuarioResp[j].RespuestaUsuario == '4' && Response.responseJSON.Data[i].listUsuarioResp[j].Respuestas.PreguntasLikert.IdPreguntaLikert == IdPreguntaLikertDobleaCTUAL && Response.responseJSON.Data[i].listUsuarioResp[j].Respuestas.Respuesta == colB) {
                                            FrecuentementeVerdadColB = FrecuentementeVerdadColB + 1;
                                        }
                                        if (Response.responseJSON.Data[i].listUsuarioResp[j].RespuestaUsuario == '5' && Response.responseJSON.Data[i].listUsuarioResp[j].Respuestas.PreguntasLikert.IdPreguntaLikert == IdPreguntaLikertDobleaCTUAL && Response.responseJSON.Data[i].listUsuarioResp[j].Respuestas.Respuesta == colB) {
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
                                    json = modelPreguntaJSColA;
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
                            } catch (e) {
                                //Pintar que aun no hay respuestas
                                //swal('Aún no se tienen respuestas en las preguntas de tipo Likert');
                            }
                        }
                        //GetLikertNormal
                        else if (Response.responseJSON.Data[i].TipoControl.IdTipoControl != 12 && Response.responseJSON.Data[i].TipoControl.IdTipoControl != 13) {
                            var Pregunta = Response.responseJSON.Data[i].Pregunta;

                            CasiSiempreFalso = Response.responseJSON.Data[i].CSF;
                            FrecuentementeFalso = Response.responseJSON.Data[i].FF;
                            AVecesVerdadAVecesFalso = Response.responseJSON.Data[i].AV;
                            FrecuentementeVerdad = Response.responseJSON.Data[i].FV;
                            CasiSiempreVerdad = Response.responseJSON.Data[i].CSV;

                            modelPreguntaJS = {
                                Pregunta: Pregunta,
                                CSF: CasiSiempreFalso,
                                FF: FrecuentementeFalso,
                                AV: AVecesVerdadAVecesFalso,
                                FV: FrecuentementeVerdad,
                                CSV: CasiSiempreVerdad
                            };
                            dataForGraphics.push(modelPreguntaJS);
                            //CasiSiempreFalso = 0;
                            //FrecuentementeFalso = 0;
                            //AVecesVerdadAVecesFalso = 0;
                            //FrecuentementeVerdad = 0;
                            //CasiSiempreVerdad = 0;
                        }

                        console.log(dataForGraphics);

                        var SubSeccion = Response.responseJSON.Data[i].SubSeccion;
                        if (subseccionAnterior != SubSeccion) {
                            //Merge en el div de subseccion que tenga
                            //FillGraphics
                            google.charts.load("current", { packages: ["corechart"] });
                            google.charts.setOnLoadCallback(drawChart);
                            function drawChart() {
                                var data = null;
                                var long = dataForGraphics.length;
                                var arrayDataForGraphics = CreateDataArray(dataForGraphics, long);

                                if (arrayDataForGraphics == null) {
                                    $('#' + DivIdForPregunta).append('<h3>Aún no existen respuestas en la pregunta de tipo Likert para mostrar el gráfico</h3>');
                                }
                                var options = {
                                    width: '100%',
                                    height: 400,
                                    legend: { position: 'top', maxLines: 3 },
                                    bar: { groupWidth: '75%' },
                                    isStacked: true,
                                };
                                var chart = new google.visualization.BarChart(document.getElementById('padreForIdPregunta_' + SubSeccion));
                                if (long > 0) {
                                    chart.draw(arrayDataForGraphics, options);
                                }
                            }//EndDrawChart
                            CreateGraphic(dataForGraphics, SubSeccion, Pregunta);
                            //ConvertJSONToCsv(dataForGraphics, Pregunta, Pregunta, divPadre);
                            subseccionAnterior = SubSeccion;
                            dataForGraphics = [];
                            $('#loadImage').remove();
                            $('#padreForIdPregunta_' + IdPregunta).css('text-align', '');
                            updateNumerarPreguntas();
                        }
                    }

                }//For
                //setTimeout('terminado()', 5000);

            }
            else {
                console.log('No hay likerts en la encuesta');
            }
            ordenar();
        }
    });
    ordenar();
}