/*
 * AngularJS
 * This file is for all connects to database
 * Use for load and save
*/
/*
 * Si cambian los margenes del div de los rb se debe ajustar la regla css desde el js 
 * Quitar las lineas 
 * 279, 299
 * Donde se forza a que el tipo de orden es el 1
 * En el guardado de respuestas agregar el periodo de aplicacion
 * Guardar en sesion tambien el id de la BD a la que pertenece el usuario
*/
(function () {
    "use strict"
    angular.module("app", [])
			.controller("climaController", climaController);

    /* #region Controller */
    function climaController($http, $scope) {
        try {
            var vm = this;
            vm.autoguardado = true;
            vm.respondida = 0;
            vm.progreso = 0;
            vm.htmlContentIntro = "";
            // vm.isBusy = false;
            vm.noReactivos = 0;
            vm.urlApis = "http://" + window.location.href.split('/')[2] + "/ClimaDinamico/";
            /* variables textos */
            vm.enfoqueA = "Enfoque Empresa";
            vm.enfoqueB = "Enfoque Área";
            vm.bienvenida = "Bienvenido(a) a la encuesta de Clima laboral";
            vm.introduccion = "A continuación, se te presentaran una serie de reactivos que te pedimos respondas con honestidad según el grado que mejor refleje tu punto de vista, de acuerdo con la siguiente escala:";
            vm.descripcionIntro1 = 'Para realizar la encuesta deberás responder a cada reactivo desde dos enfoques, pensando en la situacion actual de "la empresa y todos los jefes" y pensando en la situación actual de "tu área de trabajo y jefe directo". Algunos reactivos refieren a cuestiones personales, por lo que deberás responderlos de la misma forma en ambos enfoques.'; 
            vm.descripcionIntro2 = 'Toma en cuenta que esta encuesta es confidencial y que la información que proporciones será procesada por un equipo ético y especializado que presentará resultados de manera general y nunca de manera particular.';
            vm.descripcionIntro3 = 'Recuerda que la calidad de la información y las acciones de mejora que se deriven de ella dependerán en la honestidad con que respondas.';
            vm.likertInstruccion1 = 'Responde a cada reactivo desde los dos enfoques, pensando en la situación actual de "la empresa y todos los jefes" y pensando en la situacion actual de "tu área de trabajo y jefe directo".';
            vm.likertInstruccion2 = "Los reactivos que refieren a cuestiones personales, deberás responderlos de la misma forma en ambos enfoques.";
            vm.val5 = "Casi siempre es falso";
            vm.val4 = "Frecuentemente es falso";
            vm.val3 = "A veces es falso / A veces es verdad";
            vm.val2 = "Frecuentemente es verdad";
            vm.val1 = "Casi siempre es verdad";
            vm.objValores = [];
            vm.objValores.push({ id: 5, value: vm.val5 });
            vm.objValores.push({ id: 4, value: vm.val4 });
            vm.objValores.push({ id: 3, value: vm.val3 });
            vm.objValores.push({ id: 2, value: vm.val2 });
            vm.objValores.push({ id: 1, value: vm.val1 });
            /* fin variables textos */
            vm.mensaje = "JAMG";
            vm.ClaveAcceso = "XWIa4otM";
            vm.seccionesEncuesta = [
                { Id: 1, Name: "Login" },
                { Id: 2, Name: "Introduccion" },
                { Id: 3, Name: "Likerts" },
                { Id: 4, Name: "Abiertas" },
                { Id: 5, Name: "Demograficos" }
            ];
            vm.surveySection = 0;
            vm.preguntas = [
                { IdPregunta: 1, Pregunta: "Los jefes me mantienen informado acerca de asuntos y cambios importantes" }, 
                { IdPregunta: 2, Pregunta: "Los jefes comunican claramente sus expectativas" }, 
                { IdPregunta: 3, Pregunta: "Puedo hacer a los jefes cualquier pregunta razonable y recibir una respuesta clara" }, 
                { IdPregunta: 4, Pregunta: "Los jefes son accesibles y es fácil hablar con ellos" }, 
                { IdPregunta: 5, Pregunta: "Los Los jefes manejan el negocio de forma competente" }, 
                { IdPregunta: 6, Pregunta: "Los jefes contratan gente de acuerdo a las necesidades de la empresa" }, 
                { IdPregunta: 7, Pregunta: "Los jefes asignan y coordinan bien al personal" }, 
                { IdPregunta: 8, Pregunta: "Los jefes confían que los colaboradores hacen un buen trabajo sin supervisarlos continuamente" }
            ];

            if (window.location.href.split("/")[4].toLowerCase() == "login") {
                localStorage.clear();
            }
            

            /* Variables */
            vm.result = Object;
            vm.redirectSuccess = "http://" + window.location.href.split('/')[2] + "/ClimaDinamico/Introduccion";
            vm.redirectLogin = "http://" + window.location.href.split('/')[2] + "/ClimaDinamico/Login";
            vm.seccionActiva = vm.seccionesEncuesta[0];
            vm.listPeticiones = [];
            vm.empleadoRespuestas = [];
            vm.dataRespondidasForm = [];

            vm.listPreguntas = [];



            $(document).ready(function () {
                // vm.mostrarLoad();
                /*if (window.location.href == "http://" + window.location.href.split('/')[2] + "/ClimaDinamico/Introduccion") {
                    // getTextos de introduccion
                    vm.get("getHtmlIntro/?aIdEncuesta=" + localStorage.getItem("idEncuesta"), function (response) {
                        vm.htmlContentIntro = response.data.htmlCodeIntroduccion;
                        document.getElementsByClassName("row-header-clima")[0].innerHTML += response.data.htmlCodeIntroduccion;
                    });
                }
                if (window.location.href == "http://" + window.location.href.split('/')[2] + "/ClimaDinamico/Encuesta") {
                    // getTextos de instrucciones
                    vm.get("getHtmlEncuesta", function (response) {
                        vm.dataEncuestaHTML = response.data;
                    });
                }*/
            });

            /* Default functions */
            vm.get = function (url, functionOK, mostrarAnimacion) {
                if (mostrarAnimacion)
                    console.log();
                //vm.isBusy = true;
                url = vm.urlApis + url;
                $http.get(url, { headers: { 'Cache-Control': 'no-cache' } })
					.then(function (response) {
					    try {
					        if (messageBoxError(response))
					            return;
					        functionOK(response);
					    }
					    catch (aE) {
					        messageBoxError(aE);
					        vm.writeLogFronEnd(aE);
					    }
					},
						function (error) {
						    //error                    
						    messageBoxError(error);
						})
					.finally(function () {
					    if (mostrarAnimacion)
					        console.log();
					        // vm.isBusy = false;
					});
            }// fin get()

            vm.post = function (url, objeto, functionOK, mostrarAnimacion) {
                if (mostrarAnimacion)
                    console.log();
                    //vm.isBusy = true;
                url = vm.urlApis + url;
                $http.post(url, objeto)
					.then(function (response) {
					    try {
					        if (messageBoxError(response))
					            return;
					        functionOK(response);
					    }
					    catch (aE) {
					        messageBoxError(aE);
					        vm.writeLogFronEnd(aE);
					    }
					},
						function (error) {
						    //error                    
						    messageBoxError(error);
						})
					.finally(function () {
					    if (mostrarAnimacion)
					        console.log();
					        //vm.isBusy = false;
					});
            }//fin post() 

            function fillArray(url, arreglo, funcion) {
                vm.get(url, function (response) {
                    angular.copy(response, arreglo);
                    if (funcion != null)
                        funcion();
                },
                true);
            }

            vm.writeLogFronEnd = function (aException) {
                let model = {
                    IdEmpleado: localStorage.getItem("idEmpleado"),
                    IdEncuesta: localStorage.getItem("idEncuesta"),
                    ErrorMessage: aException.message
                };
                vm.post("LogFronEnd", model, function (response) {
                    console.log(response);
                });
            }

            
            vm.autoSave = function (e) {
                /*
                 * funcion de autoguardado
                 * cada que se elige una opcion en el listado de radio buttons
                 * se envia a BD
                 * este tipo de autoguardado de activa segun la bandera del inicio del script vm.autoguardado
                */
                try {
                    if (e.target.type == "radio" && e.target.name != "pregPerm") {
                        var value = $("input:radio[name=" + e.target.name + "]:checked").val();
                        var enfoque = 0;
                        if (e.target.name.split('-')[1] == "EE") { enfoque = 1 }
                        if (e.target.name.split('-')[1] == "EA") { enfoque = 2 }
                        if (vm.autoguardado && !vm.isNullOrEmpty(value)) {
                            var respuesta = $("input:radio[name=" + e.target.name + "]:checked").val();
                            vm.modelRespuesta = {
                                RespuestaEmpleado: respuesta,
                                Preguntas: { IdPregunta: e.target.name.split("-")[2], IdEnfoque: 1 },
                                Respuestas: { IdRespuesta: e.target.name.split("-")[2] },
                                Empleado: { IdEmpleado: localStorage.getItem("idEmpleado") },
                                Encuesta: { IdEncuesta: localStorage.getItem("idEncuesta") },
                                IdEnfoque: enfoque
                            };
                            // post
                            console.log(vm.modelRespuesta);
                            vm.post("AutoSave/?aIdBaseDeDatos=" + localStorage.getItem("idBaseDeDatos") + "&aIdEncuesta=" + localStorage.getItem("idEncuesta"), vm.modelRespuesta, function (response) {
                                console.log(response);
                                if (response.data == 1)
                                    swal.fire("Ha ocurrido un error al hacer el guardado de la respuesta", "", "error");
                            });
                        }
                    }
                    if (e.target.type == "select-one" || e.target.type == "textarea") {
                        var respuesta = e.target.value;
                        if (vm.autoguardado && !vm.isNullOrEmpty(respuesta)) {
                            vm.modelRespuesta = {
                                RespuestaEmpleado: respuesta,
                                Preguntas: { IdPregunta: e.target.attributes.idPregunta.value },
                                Respuestas: { IdRespuesta: e.target.attributes.idPregunta.value },
                                Empleado: { IdEmpleado: localStorage.getItem("idEmpleado") },
                                Encuesta: { IdEncuesta: localStorage.getItem("idEncuesta") },
                                IdEnfoque: 1
                            };
                            // post
                            console.log(vm.modelRespuesta);
                            vm.post("AutoSave/?aIdBaseDeDatos=" + localStorage.getItem("idBaseDeDatos") + "&aIdEncuesta=" + localStorage.getItem("idEncuesta"), vm.modelRespuesta, function (response) {
                                console.log(response);
                                if (response.data == 1)
                                    swal.fire("Ha ocurrido un error al hacer el guardado de la respuesta", "", "error");
                            });
                        }
                    }
                    if (e.target.type == "radio" && e.target.name == "pregPerm") {
                        var respuesta = e.target.value;
                        if (vm.autoguardado && !vm.isNullOrEmpty(respuesta)) {
                            vm.modelRespuesta = {
                                RespuestaEmpleado: respuesta,
                                Preguntas: { IdPregunta: e.target.attributes.idPregunta.value },
                                Respuestas: { IdRespuesta: e.target.attributes.idPregunta.value },
                                Empleado: { IdEmpleado: localStorage.getItem("idEmpleado") },
                                Encuesta: { IdEncuesta: localStorage.getItem("idEncuesta") },
                                IdEnfoque: 1
                            };
                            // post
                            console.log(vm.modelRespuesta);
                            vm.post("AutoSave/?aIdBaseDeDatos=" + localStorage.getItem("idBaseDeDatos") + "&aIdEncuesta=" + localStorage.getItem("idEncuesta"), vm.modelRespuesta, function (response) {
                                console.log(response);
                                if (response.data == 1)
                                    swal.fire("Ha ocurrido un error al hacer el guardado de la respuesta", "", "error");
                            });
                        }
                    }
                } catch (aE) {
                    console.log(aE);
                    vm.writeLogFronEnd(aE);
                }
            }
            
            vm.prev = function () {
                vm.surveySection--;
                if (vm.surveySection == 0) {
                    window.location.href = "/ClimaDinamico/Introduccion/"
                }
                vm.activateSurveySection(vm.surveySection);
            }
            
            vm.getParamByUrl = function (key) {
                try {
                    var url_string = window.location.href;
                    var url = new URL(url_string);
                    var param = url.searchParams.get(key);
                    console.log(param);
                    return param;
                } catch (aE) {
                    return "";
                }
            }

            /* Funciones */
            vm.Autenticar = function () {
                vm.mostrarLoad();
                vm.ClaveAcceso = vm.ClaveAcceso.trim();
                if (vm.isNullOrEmpty(vm.ClaveAcceso)) {
                    swal.fire("Captura la clave que te ha sido asignada. Si no cuentas con clave acércate con tu responsable de Recursos Humanos", "", "warning");
                }
                else {
                    var model = {
                        ClavesAcceso: { ClaveAcceso: vm.ClaveAcceso }
                    };
                    var uid = vm.getParamByUrl("uid");
                    vm.post("Autenticar/?uid=" + uid, model, function (response) {
                        vm.ocultarLoad();
                        console.log(response);
                        switch (response.data) {
                            case "EncuestaNotFound":
                                swal.fire("Encuesta no encontrada", "", "info");
                                break;
                            case "nullUID":
                                swal.fire("Verifica la url, ingresa con la liga que llegó a tu correo", "", "warning");
                                break;
                            case "invalidkey":
                                swal.fire("La clave es invalida", "", "warning");
                                break;
                            case "encuestaRealizada":
                                swal.fire("Esta clave ya ha sido utilizada", "", "warning");
                                break;
                            case "noStart":
                                swal.fire("El periodo de aplicacion de la encuesta no ha iniciado");
                                break;
                            case "AppEnd":
                                swal.fire("El periodo de aplicación de la encuesta ha terminado", "", "info");
                                break;
                            case "BDNotFound":
                                swal.fire("No se encontró la base de datos a la que pertenece el usuario", "", "info");
                                break;
                            case "Exception":
                                swal.fire("Ocurrió un error al autenticar al usuario", "", "info");
                                break;
                            case 6:
                                window.location.href = vm.redirectSuccess;
                                break;
                            case "notFoundPeriodosApp":
                                swal.fire("No se encontraron periodos de aplicacion para la encuesta", "", "info");
                                break;
                            default:
                        }
                    });
                }
            }

            vm.fillFormulario = function () {
                /* 
                 *consulta de las respuestas
                 *armar el nombre del radio con "Pregunta" + (si enfoque es 1 : EE :: si enfque es 2 : EA + IdPregunta)
                 *value es la respuesta que se obtuvo
                */
                vm.mostrarLoad();
                fillArray("getPreguntasFromEncuesta/?aIdEmpleado=" + localStorage.getItem("idEmpleado") + "&aIdEncuesta=" + localStorage.getItem("idEncuesta"), vm.empleadoRespuestas, function () {
                    console.log(vm.empleadoRespuestas);
                    vm.empleadoRespuestas = vm.empleadoRespuestas.data;
                    // meter las respuestas en el array de respondidas form
                    // Pregunta-EE-7711
                    vm.empleadoRespuestas.forEach(function (value, index, arr) {
                        vm.mostrarLoad();
                        var enfoque = "";
                        if (vm.empleadoRespuestas[index]._idEnfoque == 1)
                            enfoque = "EE";
                        if (vm.empleadoRespuestas[index]._idEnfoque == 2)
                            enfoque = "EA";
                        vm.dataRespondidasForm.push("Pregunta-" + enfoque + "-" + vm.empleadoRespuestas[index]._idPregunta);
                    });
                    "Pregunta-" + vm.empleadoRespuestas[0]._idEnfoque == 1 ? "EE-" : "EA-" + vm.empleadoRespuestas[0]._idPregunta

                    vm.getProgress();
                    if (vm.empleadoRespuestas.length == 0)
                        return false;
                    if (vm.empleadoRespuestas[0]._idEmpleado == 0 && vm.empleadoRespuestas[0].hasRespuestas == true && vm.empleadoRespuestas[0]._respuestaEmpleado == "Error") {
                        swal.fire("Ocurrió un error al consultar tus respuestas anteriores", "", "info").then(function () {
                            window.location.href = vm.redirectLogin;
                        });
                    }
                    if (vm.empleadoRespuestas[0]._idEmpleado == 0 && vm.empleadoRespuestas[0].hasRespuestas == false) {
                        // el usuario no tiene respuestas grabadas con anterioridad
                    }
                    else {
                        // jQuery $('input[name="Pregunta-EA-8"][value="Casi siempre es falso"]').prop("checked", "true");
                        // JS document.querySelectorAll('input[name="Pregunta-EE-6"][value="A veces es falso / A veces es verdad"]')[0].checked = true;
                        vm.empleadoRespuestas.forEach(function (value, index, arr) {
                            var enfoque = "";
                            if (vm.empleadoRespuestas[index]._idTipoControl == 12) { // likert
                                if (vm.empleadoRespuestas[index]._idEnfoque == 1) { enfoque = "EE" }
                                if (vm.empleadoRespuestas[index]._idEnfoque == 2) { enfoque = "EA" }
                                document.querySelectorAll('input[name="Pregunta-' + enfoque + '-' + vm.empleadoRespuestas[index]._idPregunta + '"][value="' + vm.empleadoRespuestas[index]._respuestaEmpleado + '"]')[0].checked = true;
                                var elemInput = document.querySelectorAll('input[name="Pregunta-' + enfoque + '-' + vm.empleadoRespuestas[index]._idPregunta + '"][value="' + vm.empleadoRespuestas[index]._respuestaEmpleado + '"]')[0];
                                // elemInput.id buscar el label con este for
                                var labels = $("#" + elemInput.parentNode.id).find("label");
                                for (var i = 0; i < labels.length; i++) {
                                    if (labels[i].attributes.for.value == elemInput.id) {
                                        var styles = getComputedStyle(labels[i]);
                                        labels[i].style.backgroundColor = styles.borderInlineEndColor;
                                    }
                                }
                            }
                            if (vm.empleadoRespuestas[index]._idTipoControl == 2) { // textarea
                                // obtener textareas y consultar cual trae el atributo id pregunta segun la iteracion y sobre ese pintar la respuesta
                                var items = $("body").find("textarea");
                                [].forEach.call(items, function (dataR) {
                                    if (dataR.attributes.idPregunta.value == vm.empleadoRespuestas[index]._idPregunta) {
                                        dataR.value = vm.empleadoRespuestas[index]._respuestaEmpleado;
                                    }
                                });
                            }
                            if (vm.empleadoRespuestas[index]._idTipoControl == 5) { // select
                                var items = $("body").find("select");
                                [].forEach.call(items, function (dataR) {
                                    if (dataR.attributes.idPregunta.value == vm.empleadoRespuestas[index]._idPregunta) {
                                        dataR.value = vm.empleadoRespuestas[index]._respuestaEmpleado;
                                    }
                                });
                            }
                            if (vm.empleadoRespuestas[index]._idTipoControl == 4) { // radio
                                var items = $("body").find("input[type=radio]"); // escluir los divs de los likert EnfE
                                items = $(".radio-simple input[type=radio]");
                                [].forEach.call(items, function (dataR) {
                                    if (dataR.attributes.idPregunta.value == vm.empleadoRespuestas[index]._idPregunta && dataR.name == "pregPerm" && dataR.value == vm.empleadoRespuestas[index]._respuestaEmpleado) {
                                        document.querySelectorAll('input[name="pregPerm"][value="No"]')[0].checked = true;
                                    }
                                });
                            }
                        });
                        vm.ocultarLoad();
                    }
                });
            }

            vm.getPreguntasByEncuesta = function () {
                vm.mostrarLoad();
                fillArray("getPreguntasByIdEncuesta/?aIdEncuesta=" + localStorage.getItem("idEncuesta"), vm.listPreguntas, function () {
                    vm.mostrarLoad();
                    console.log(vm.listPreguntas.data);
                    vm.listPreguntas = vm.listPreguntas.data;
                    // crear orden
                    //vm.listPreguntas[0].IdTipoOrden = 0;
                    switch (vm.listPreguntas[0].IdTipoOrden) {
                        case 0: // Orden por id de pregunta (8, 8)
                            vm.listPreguntas = Enumerable.From(vm.listPreguntas).OrderBy('$.IdPregunta').ToArray();
                            break;
                        case 1: // orden por competencia
                            vm.listPreguntas = Enumerable.From(vm.listPreguntas).OrderBy('$.Competencia.IdCompetencia').ToArray();
                            break;
                        case 2: // orden por pregunta padre (8, 8)
                            vm.listPreguntas = Enumerable.From(vm.listPreguntas).OrderBy('$.IdPreguntaPadre').ToArray();
                            break;
                        case 3: // orden personalizado (8, 8)
                            vm.listPreguntas = Enumerable.From(vm.listPreguntas).OrderBy('$.IdOrden').ToArray();
                            break;
                        default:
                    }
                    console.log(vm.listPreguntas);
                    // numero de reactivos que pertenecen a enfoques 1
                    vm.noReactivos = vm.listPreguntas[0].noReactivosEE;
                    vm.totalPreguntas = vm.listPreguntas.length + vm.noReactivos;
                    vm.getProgress();
                    // obtener el numero de secciones totales en la encuesta
                    //vm.listPreguntas[0].IdTipoOrden = 0;
                    if (vm.listPreguntas[0].IdTipoOrden == 0 || vm.listPreguntas[0].IdTipoOrden == 2 || vm.listPreguntas[0].IdTipoOrden == 3) { // orden por id de pregunta
                        vm.totalSecciones = vm.listPreguntas.length / 8; // 11 likert + permanencia y demografico
                        document.getElementsByClassName("total-secciones")[0].innerText = Math.round(vm.totalSecciones);
                    }
                    if (vm.listPreguntas[0].IdTipoOrden == 1) { // orden por competencia
                        var compes = Enumerable.From(vm.listPreguntas).Distinct('$.Competencia.IdCompetencia').ToArray();
                        vm.totalSecciones = compes.length; // permanencia y demografico ya van incluidas
                        document.getElementsByClassName("total-secciones")[0].innerText = vm.totalSecciones;
                    }
                    setTimeout(function () {
                        // activar la funcion de autosave cuando todos los radio ya este  cargados
                        vm.mostrarLoad();
                        $("input:radio").click(function (e) {
                            vm.autoSave(e);
                        });
                        $("input:radio").change(function (e) {
                            vm.autoSave(e);
                        });
                        $("select").change(function (e) {
                            vm.autoSave(e);
                        });
                        $("textarea").change(function (e) {
                            vm.autoSave(e);
                        });
                        // seccionar
                        vm.surveySection = 1;
                        vm.activateSurveySection(vm.surveySection);
                        // rellenar las respuestas anteriores
                        vm.fillFormulario();
                        // pintar color
                        $(".lbl").click(function (e) {
                            var elem = e.target;
                            var style = getComputedStyle(elem);
                            var labels = $("#" + e.target.parentNode.id).find("label");
                            $("#" + e.target.attributes.for.value == null ? "xc" : e.target.attributes.for.value).attr("checked", "true");
                            document.getElementById(e.target.attributes.for.value).checked = true;
                            [].forEach.call(labels, function (data) {
                                if (data.attributes.for.value == e.target.attributes.for.value)
                                    data.style.backgroundColor = style.borderInlineEndColor;
                                if (data.attributes.for.value != e.target.attributes.for.value)
                                    data.style.backgroundColor = "#fff";
                            });
                        });
                        $("input:radio").change(function (e) {
                            if (!vm.isNullOrEmpty(e.target.parentNode.id)) {
                                var labels = $("#" + e.target.parentNode.id).find("label");
                                var style = Object;
                                try {
                                    [].forEach.call(labels, function (dataR) {
                                        if (dataR.attributes.for.value == e.target.attributes.id.value) {
                                            style = getComputedStyle(dataR);
                                            dataR.style.backgroundColor = style.borderInlineEndColor;
                                        }
                                        if (dataR.attributes.for.value != e.target.attributes.id.value) {
                                            dataR.style.backgroundColor = "#fff";
                                        }
                                    });
                                } catch (aE) {
                                    console.log(aE);
                                    vm.writeLogFronEnd(aE);
                                }
                            }
                        });
                        $("input:radio").click(function (e) {
                            var labels = $("#" + e.target.parentNode.id == "" ? "xc" : e.target.parentNode.id).find("label");
                            var style = Object;
                            try {
                                [].forEach.call(labels, function (dataR) {
                                    if (dataR.attributes.for.value == e.target.attributes.id.value) {
                                        style = getComputedStyle(dataR);
                                        dataR.style.backgroundColor = style.borderInlineEndColor;
                                    }
                                    if (dataR.attributes.for.value != e.target.attributes.id.value) {
                                        dataR.style.backgroundColor = "#fff";
                                    }
                                });
                            } catch (aE) {
                                console.log(aE);
                                vm.writeLogFronEnd(aE);
                            }
                        });
                        // activar la validacion en un click para pasar de Error control a válido
                        $("input:radio").click(function (e) {
                            if (e.target.name != "pregPerm") {
                                var elems = $("input:radio[name=" + e.target.name + "]");
                                var nameItem1 = e.target.name;
                                var nameItem2 = "";
                                if (e.target.name.includes("-EE-")) {
                                    nameItem2 = nameItem1.replace("-EE-", "-EA-");
                                    /* sumar */
                                    //nameItem2 = nameItem2.split('-')[0] + "-" + nameItem2.split('-')[1] + "-" + (parseInt(nameItem2.split('-')[2]) + vm.noReactivos);
                                    nameItem2 = nameItem2.split('-')[0] + "-" + nameItem2.split('-')[1] + "-" + (parseInt(nameItem2.split('-')[2]) + 1);
                                }
                                if (e.target.name.includes("-EA-")) {
                                    nameItem2 = nameItem1.replace("-EA-", "-EE-");
                                    /* restar */
                                    //nameItem2 = nameItem2.split('-')[0] + "-" + nameItem2.split('-')[1] + "-" + (parseInt(nameItem2.split('-')[2]) - vm.noReactivos);
                                    nameItem2 = nameItem2.split('-')[0] + "-" + nameItem2.split('-')[1] + "-" + (parseInt(nameItem2.split('-')[2]) - 1);
                                }
                                var val1 = $("input:radio[name=" + nameItem1 + "]:checked").val(); // EE
                                var val2 = $("input:radio[name=" + nameItem2 + "]:checked").val(); // EA

                                if (!vm.isNullOrEmpty(val1) && !vm.dataRespondidasForm.includes(nameItem1))
                                    vm.dataRespondidasForm.push(nameItem1);
                                if (!vm.isNullOrEmpty(val2) && !vm.dataRespondidasForm.includes(nameItem2))
                                    vm.dataRespondidasForm.push(nameItem2);
                                // asignar el progreo en base a lo ya respondido
                                vm.getProgress();
                                [].forEach.call(elems, function (data) {
                                    if ((isNullOrEmpty(val1) && data.parentNode.parentNode.parentNode.style.backgroundColor == "rgb(255, 221, 221)") || (isNullOrEmpty(val2) && data.parentNode.parentNode.parentNode.style.backgroundColor == "rgb(255, 221, 221)")) {
                                        data.parentNode.parentNode.parentNode.style.backgroundColor = "rgb(255, 221, 221)";
                                        return true;
                                    }
                                    if (data.parentNode.parentNode.parentNode.classList.contains("bg-color")) {
                                        data.parentNode.parentNode.parentNode.style.backgroundColor = "#efefef";
                                        return true;
                                    }
                                    if (!data.parentNode.parentNode.parentNode.classList.contains("bg-color")) {
                                        data.parentNode.parentNode.parentNode.style.backgroundColor = "#fff";
                                        return true;
                                    }
                                });
                            }
                            else {
                                if (!vm.isNullOrEmpty(e.target.value)) {
                                    e.target.parentNode.parentNode.parentNode.style.backgroundColor = "#fff";
                                }
                                else {
                                    e.target.parentNode.parentNode.parentNode.style.backgroundColor = "rgb(255, 221, 221)";
                                }
                            }
                        });
                        $("input:radio").change(function (e) {
                            if (e.target.name != "pregPerm") {
                                var elems = $("input:radio[name=" + e.target.name + "]");
                                var nameItem1 = e.target.name;
                                var nameItem2 = "";
                                if (e.target.name.includes("-EE-")) {
                                    nameItem2 = nameItem1.replace("-EE-", "-EA-");
                                    /* sumar */
                                    //nameItem2 = nameItem2.split('-')[0] + "-" + nameItem2.split('-')[1] + "-" + (parseInt(nameItem2.split('-')[2]) + vm.noReactivos);
                                    nameItem2 = nameItem2.split('-')[0] + "-" + nameItem2.split('-')[1] + "-" + (parseInt(nameItem2.split('-')[2]) + 1);
                                }
                                if (e.target.name.includes("-EA-")) {
                                    nameItem2 = nameItem1.replace("-EA-", "-EE-");
                                    /* restar */
                                    //nameItem2 = nameItem2.split('-')[0] + "-" + nameItem2.split('-')[1] + "-" + (parseInt(nameItem2.split('-')[2]) - vm.noReactivos);
                                    nameItem2 = nameItem2.split('-')[0] + "-" + nameItem2.split('-')[1] + "-" + (parseInt(nameItem2.split('-')[2]) - 1);
                                }
                                var val1 = $("input:radio[name=" + nameItem1 + "]:checked").val(); // EE
                                var val2 = $("input:radio[name=" + nameItem2 + "]:checked").val(); // EA

                                if (!vm.isNullOrEmpty(val1) && !vm.dataRespondidasForm.includes(nameItem1))
                                    vm.dataRespondidasForm.push(nameItem1);
                                if (!vm.isNullOrEmpty(val2) && !vm.dataRespondidasForm.includes(nameItem2))
                                    vm.dataRespondidasForm.push(nameItem2);
                                // asignar el progreo en base a lo ya respondido
                                vm.getProgress();
                                [].forEach.call(elems, function (data) {
                                    if ((isNullOrEmpty(val1) && data.parentNode.parentNode.parentNode.style.backgroundColor == "rgb(255, 221, 221)") || (isNullOrEmpty(val2) && data.parentNode.parentNode.parentNode.style.backgroundColor == "rgb(255, 221, 221)")) {
                                        data.parentNode.parentNode.parentNode.style.backgroundColor = "rgb(255, 221, 221)";
                                        return true;
                                    }
                                    if (data.parentNode.parentNode.parentNode.classList.contains("bg-color")) {
                                        data.parentNode.parentNode.parentNode.style.backgroundColor = "#efefef";
                                        return true;
                                    }
                                    if (!data.parentNode.parentNode.parentNode.classList.contains("bg-color")) {
                                        data.parentNode.parentNode.parentNode.style.backgroundColor = "#fff";
                                        return true;
                                    }
                                });
                            }
                            else {
                                if (!vm.isNullOrEmpty(e.target.value)) {
                                    e.target.parentNode.parentNode.parentNode.style.backgroundColor = "#fff";
                                }
                                else {
                                    e.target.parentNode.parentNode.parentNode.style.backgroundColor = "rgb(255, 221, 221)";
                                }
                            }
                        });

                        $("select").change(function (e) {
                            if (!vm.isNullOrEmpty(e.target.value)) {
                                e.target.style.backgroundColor = "#fff";
                            }
                        });
                        $("textarea").change(function (e) {
                            if (!vm.isNullOrEmpty(e.target.value)) {
                                e.target.style.backgroundColor = "#fff";
                            }
                        });

                        var space = (document.getElementById("div_0_EE").offsetWidth - 130); // restar padding y los 20px de cada radio
                        space = space / 4; // entre los 4 margin-left
                        createCSSSelector('label + label', 'margin-left:' + space + "px !important");
                    }, 500);
                });
            }

            vm.getIUD = function () {
                return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
                    var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
                    return v.toString(16);
                });
            }

            vm.activateSurveySection = function (section) { // 2
                try {
                    vm.seccionesTotales = Math.round(vm.totalSecciones);
                    var elems = document.getElementsByClassName("mergePreguntas");
                    switch (vm.listPreguntas[0].IdTipoOrden) {
                        case 0: // orden por id de pregunta
                            [].forEach.call(elems, function (item, index) {
                                if (parseInt(item.attributes.idPreguntaPadre.value) > (section - 1) * 8 && parseInt(item.attributes.idPreguntaPadre.value) <= (section * 8))
                                    item.style.display = "";
                                else 
                                    item.style.display = "none";
                                vm.totalSecciones = Math.round(vm.totalSecciones);
                                if (section == vm.totalSecciones - 1 || section == vm.totalSecciones) {
                                    // sin importar la numeracion mostrar permanencia en section == vm.totalSecciones - 1
                                    if ((item.attributes.nombreCompetencia.value) == "Permanencia" && section == (vm.totalSecciones - 1))
                                        item.style.display = "";
                                    if ((item.attributes.nombreCompetencia.value) != "Permanencia" && section == (vm.totalSecciones - 1))
                                        item.style.display = "none";
                                    // sin importar la numeracion mostrar demografico en section == vm.totalSecciones
                                    if ((item.attributes.nombreCompetencia.value) == "Demografico" && section == (vm.totalSecciones))
                                        item.style.display = "";
                                    if ((item.attributes.nombreCompetencia.value) != "Demografico" && section == (vm.totalSecciones))
                                        item.style.display = "none";
                                }
                            });
                            break;
                        case 1: // order por competencia, funciona para encuesta completa o para encuesta con solo algunas preguntas
                            [].forEach.call(elems, function (item) {
                                if (parseInt(item.attributes.idCompetencia.value) == section)
                                    item.style.display = "";
                                if (parseInt(item.attributes.idCompetencia.value) != section)
                                    item.style.display = "none";
                            });
                            break;
                        case 2: // orden por pregunta padre, falta agregar el caso donde el la encuesta tiene solo algunas preguntas
                            [].forEach.call(elems, function (item, index) {
                                /*if (elems[0].attributes.idPreguntaPadre.value == 1 && false == true) { // caso base
                                    if (parseInt(item.attributes.idPreguntaPadre.value) > (section - 1) * 8 && parseInt(item.attributes.idPreguntaPadre.value) <= (section * 8))
                                        item.style.display = "";
                                    else
                                        item.style.display = "none";
                                }*/
                                // caso para encuestas con todas o con solo algunas preguntas
                                if (parseInt(item.attributes.idconsecutivo.value) > (section - 1) * 8 && parseInt(item.attributes.idconsecutivo.value) <= (section * 8))
                                    item.style.display = "";
                                else
                                    item.style.display = "none";
                                
                                if (item.attributes.idCompetencia.value == 13 && section == (Math.round(vm.totalSecciones) - 1)) // Permanencia
                                    item.style.display = "";
                                if (item.attributes.idCompetencia.value == 14 && section == Math.round(vm.totalSecciones)) // Demografico
                                    item.style.display = "";
                            });
                            break;
                        case 3: // orden personalizado IdOrden
                            [].forEach.call(elems, function (item, index) {
                                if (parseInt(item.attributes.idOrden.value) > (section - 1) * 8 && parseInt(item.attributes.idOrden.value) <= (section * 8))
                                    item.style.display = "";
                                else
                                    item.style.display = "none";
                            });
                            break;
                        default:
                            swal.fire("No se ha podido organizar el contenido de la encuesta", "", "info")
                    }
                } catch (aE) {
                    console.log(aE);
                    vm.writeLogFronEnd(aE);
                }
            }

            if (window.location.href.split("/")[4].toLowerCase() == "encuesta") {
                vm.surveySection = 1;
            }
            vm.activateSurveySection(vm.surveySection);

            vm.next = function () {

            }

            vm.ocultarLoad = function (e) {
                document.getElementsByClassName("busy")[0].classList.add("loadInvisible");
                document.getElementsByClassName("busy")[0].classList.remove("loadVisible");
            }

            vm.mostrarLoad = function () {
                document.getElementsByClassName("busy")[0].classList.remove("loadInvisible");
                document.getElementsByClassName("busy")[0].classList.add("loadVisible");
            }

            vm.getProgress = function () {
                try {
                    // vm.empleadoRespuestas son los reactivos que ya respondi previamente
                    // vm.dataRespondidasForm son las que voy respondiendo actualmente
                    // totalPreguntas son el numero total de reactivos de la encuesta
                    // var progreso = ((vm.empleadoRespuestas.length + vm.dataRespondidasForm.length) / vm.totalPreguntas) * 100;
                    var progreso = ((vm.dataRespondidasForm.length) / vm.totalPreguntas) * 100;
                    progreso = Math.round(progreso * 100) / 100;
                    if (progreso.toString() == "NaN")
                        progreso = 0;
                    vm.progreso = progreso;
                    document.getElementsByClassName("progress-clima")[0].style.width = vm.progreso + "%";
                    document.getElementsByClassName("progress-clima")[0].style.backgroundColor = vm.getColorProgress(vm.progreso);
                    document.getElementsByClassName("txt-progreso")[0].innerText = "PROGRESO " + vm.progreso + "%";

                    return String(progreso);
                } catch (aE) {
                    console.log(aE);
                    vm.writeLogFronEnd(aE);
                }
            }

            vm.getColorProgress = function (progress) {
                if (progress < 21)
                    return "#f15a24";
                if (progress > 20 && progress < 41)
                    return "#f7931e";
                if (progress > 40 && progress < 61)
                    return "#cccc01";
                if (progress > 60 && progress < 81)
                    return "#8cc63f";
                if (progress > 80 && progress <= 100)
                    return "#39b54a";
            }

            vm.saveSeccion = function (showMesage) {
                vm.listEmpleadoRespuestasEE = [];
                vm.listEmpleadoRespuestasEA = [];
                //if (showMesage == 1)
                vm.mostrarLoad();
                // obtener inputs de la seccion Enfoque empresa
                try {
                    vm.listEmpleadoRespuestasEE = vm.getRespuestasEnfoqueEmpresa();
                    vm.listEmpleadoRespuestasEA = vm.getRespuestasEnfoqueArea();
                    console.table(vm.listEmpleadoRespuestasEE);
                    console.table(vm.listEmpleadoRespuestasEA);
                    if (vm.isNullOrEmpty(vm.listEmpleadoRespuestasEE) || vm.isNullOrEmpty(vm.listEmpleadoRespuestasEA)) {
                        vm.ocultarLoad();
                        swal.fire("Debes responder a todos los reactivos", "", "info").then(function () {
                            return false;
                        });
                    }
                    else {
                        vm.post("SaveAvance/?aIdBaseDeDatos=" + localStorage.getItem("idBaseDeDatos") + "&aIdEncuesta=" + localStorage.getItem("idEncuesta"), vm.listEmpleadoRespuestasEE, function (response) {
                            console.log(response);
                            vm.post("SaveAvance/?aIdBaseDeDatos=" + localStorage.getItem("idBaseDeDatos") + "&aIdEncuesta=" + localStorage.getItem("idEncuesta"), vm.listEmpleadoRespuestasEA, function (response) {
                                console.log(response);
                                if (showMesage == 0) {
                                    if (response.data == 0) { // ok
                                        vm.ocultarLoad();
                                        vm.surveySection++;
                                        vm.activateSurveySection(vm.surveySection);
                                        return true;
                                    }
                                    if (response.data == 1) {
                                        swal.fire("Ocurrión un error al guardar tus respuestas de esta sección", "", "error");
                                    }
                                }
                                if (showMesage == 1) {
                                    vm.ocultarLoad();
                                    if (response.data == 0) {
                                        vm.surveySection++;
                                        swal.fire("Tu avance se guardó correctamente", "", "success").then(function myfunction() {
                                            vm.activateSurveySection(vm.surveySection);
                                        });
                                    }
                                    if (response.data == 1) { swal.fire("Ocurrió un error al intentar guardar tu avance", "", "error"); }
                                }
                                if (showMesage == 2) {
                                    vm.ocultarLoad();
                                    if (response.data == 0) {
                                        window.location.href = "/ClimaDinamico/Thanks/?idEncuesta=" + localStorage.getItem("idEncuesta") + "&idUsuario=" + localStorage.getItem("idEmpleado") + "&idBaseDeDatos=" + localStorage.getItem("idBaseDeDatos"); // recibir id de encuesta
                                    }
                                    if (response.data == 1)
                                        swal.fire("Ocurrió un error al terminar la encuesta", "", "error");
                                }
                                return response.data;
                            });
                        });
                    }
                } catch (aE) {
                    console.log(aE);
                    vm.writeLogFronEnd(aE);
                }
            }

            vm.getRespuestasEnfoqueEmpresa = function () {
                vm.data = [];
                try {
                    var inputVisibleEE = $(".EE").find("input:radio:visible");
                    var selectVisible = $("body").find("select:visible");
                    var textareaVisible = $("body").find("textarea:visible");
                    var names = Enumerable.From(inputVisibleEE).Distinct('$.name').OrderBy('$.name').ToArray();
                    for (var i = 0; i < selectVisible.length; i++) {
                        names.push(selectVisible[i]);
                    }
                    for (var i = 0; i < textareaVisible.length; i++) {
                        names.push(textareaVisible[i]);
                    }
                    if (!vm.validarForm(names)) {
                        swal.fire("Debes responder a todos los reactivos", "", "info").then(function () {
                            return false;
                        });
                    }
                    else {
                        names.forEach(function (value, index, arr) {
                            vm.modelRespuesta = JSON.parse(JSON.stringify(objEmpleadoRes)); // inicializar objeto
                            if (names[index].type == "radio") {
                                var respuesta = $("input:radio[name=" + names[index].name + "]:checked").val();
                                vm.modelRespuesta = {
                                    RespuestaEmpleado: respuesta,
                                    Preguntas: { IdPregunta: names[index].attributes.idPregunta.value, IdEnfoque: 1 },
                                    Respuestas: { IdRespuesta: names[index].attributes.idPregunta.value },
                                    Empleado: { IdEmpleado: localStorage.getItem("idEmpleado") },
                                    Encuesta: { IdEncuesta: localStorage.getItem("idEncuesta") },
                                    IdEnfoque: 1
                                };
                            }
                            if (names[index].type == "select-one" || names[index].type == "textarea") {
                                var respuesta = names[index].value;
                                vm.modelRespuesta = {
                                    RespuestaEmpleado: respuesta,
                                    Preguntas: { IdPregunta: names[index].attributes.idPregunta.value, IdEnfoque: 1 },
                                    Respuestas: { IdRespuesta: names[index].attributes.idPregunta.value },
                                    Empleado: { IdEmpleado: localStorage.getItem("idEmpleado") },
                                    Encuesta: { IdEncuesta: localStorage.getItem("idEncuesta") },
                                    IdEnfoque: 1
                                };
                            }
                            vm.data.push(vm.modelRespuesta);
                        });
                        return vm.data;
                    }
                } catch (aE) {
                    console.log(aE);
                    vm.writeLogFronEnd(aE);
                }
            }

            vm.validarForm = function (names) {
                var isValid = true;
                names.forEach(function (value, index, arr) {
                    if (names[index].type == "radio") {
                        var respuesta = $("input:radio[name=" + names[index].name + "]:checked").val();
                        if (vm.isNullOrEmpty(respuesta)) {
                            var elems = $("input:radio[name=" + names[index].name + "]");
                            for (var i = 0; i < elems.length; i++) {
                                elems[i].parentNode.parentNode.parentNode.style.backgroundColor = "rgb(255, 221, 221)";
                            }
                            isValid = false;
                        }
                    }
                    if (names[index].type == "select-one" || names[index].type == "textarea") {
                        if (vm.isNullOrEmpty(names[index].value)) {
                            names[index].style.backgroundColor = "rgb(255, 221, 221)";
                            isValid = false;
                        }   
                    }
                });
                return isValid;
            }

            vm.isNullOrEmpty = function (data) {
                if (data == "" || data == null || data == undefined) {
                    return true;
                }
                else {
                    return false;
                }
            }

            vm.Count = function (arr) {
                if (typeof (arr) == "object" || typeof (arr) == "Array") {
                    return arr.length;
                }
                else {
                    swal.fire("El objeto enviado no es un array válido");
                }
            }

            vm.getRespuestasEnfoqueArea = function () {
                vm.data = [];
                try {
                    var inputVisibleEA = $(".EA").find("input:radio:visible");
                    var selectVisible = $("body").find("select:visible");
                    var textareaVisible = $("body").find("textarea:visible");
                    var names = Enumerable.From(inputVisibleEA).Distinct('$.name').OrderBy('$.name').ToArray();
                    for (var i = 0; i < selectVisible.length; i++) {
                        names.push(selectVisible[i]);
                    }
                    for (var i = 0; i < textareaVisible.length; i++) {
                        names.push(textareaVisible[i]);
                    }
                    if (!vm.validarForm(names)) {
                        swal.fire("Debes responder a todos los reactivos", "", "info").then(function () {
                            return false;
                        });
                    }
                    else {
                        names.forEach(function (value, index, arr) {
                            vm.modelRespuesta = JSON.parse(JSON.stringify(objEmpleadoRes)); // inicializar objeto
                            if (names[index].type == "radio") {
                                var respuesta = $("input:radio[name=" + names[index].name + "]:checked").val();
                                vm.modelRespuesta = {
                                    RespuestaEmpleado: respuesta,
                                    Preguntas: { IdPregunta: names[index].attributes.idPregunta.value, IdEnfoque: 1 },
                                    Respuestas: { IdRespuesta: names[index].attributes.idPregunta.value },
                                    Empleado: { IdEmpleado: localStorage.getItem("idEmpleado") },
                                    Encuesta: { IdEncuesta: localStorage.getItem("idEncuesta") },
                                    IdEnfoque: 2
                                };
                            }
                            if (names[index].type == "select-one" || names[index].type == "textarea") {
                                var respuesta = names[index].value;
                                vm.modelRespuesta = {
                                    RespuestaEmpleado: respuesta,
                                    Preguntas: { IdPregunta: names[index].attributes.idPregunta.value, IdEnfoque: 1 },
                                    Respuestas: { IdRespuesta: names[index].attributes.idPregunta.value },
                                    Empleado: { IdEmpleado: localStorage.getItem("idEmpleado") },
                                    Encuesta: { IdEncuesta: localStorage.getItem("idEncuesta") },
                                    IdEnfoque: 1
                                };
                            }
                            vm.data.push(vm.modelRespuesta);
                        });
                        return vm.data;
                    }
                } catch (aE) {
                    console.log(aE);
                    vm.writeLogFronEnd(aE);
                }
            }

            /*vm.getRespuestasEnfoqueArea = function () {
                vm.data = [];
                var inputVisibleEE = $(".EA").find("input:radio:visible");
                var selectVisible = $("body").find("select:visible");
                var textareaVisible = $("body").find("textarea:visible");
                var names = Enumerable.From(inputVisibleEE).Distinct('$.name').OrderBy('$.name').ToArray();
                for (var i = 0; i < selectVisible.length; i++) {
                    names.push(selectVisible[i]);
                }
                for (var i = 0; i < textareaVisible.length; i++) {
                    names.push(textareaVisible[i]);
                }
                if (!vm.validarForm(names)) {
                    swal.fire("Debes responder a todos los reactivos", "", "info").then(function () {
                        return false;
                    });
                }
                else {
                    names.forEach(function (value, index, arr) {
                        vm.modelRespuesta = JSON.parse(JSON.stringify(objEmpleadoRes)); // inicializar objeto
                        var respuesta = $("input:radio[name=" + names[index].name + "]:checked").val();
                        vm.modelRespuesta = {
                            RespuestaEmpleado: respuesta,
                            Preguntas: { IdPregunta: names[index].name.split("-")[2], IdEnfoque: 2 },
                            Respuestas: { IdRespuesta: names[index].name.split("-")[2] },
                            Empleado: { IdEmpleado: localStorage.getItem("idEmpleado") },
                            Encuesta: { IdEncuesta: localStorage.getItem("idEncuesta") },
                            IdEnfoque: 2
                        };
                        vm.data.push(vm.modelRespuesta);
                    });
                    return vm.data;
                }
            }*/
           
            var messageBoxError = function (response) {
                try {
                    if (response.status == 200){
                        return false;
                    } else {
                        vm.ocultarLoad();
                        swal.fire("Ha ocurrido un error", "Status: " + response.status + ". response: " + response.statusText, "error. url: " + response.config.url);
                        return true;
                    }
                } catch (aE) {
                    console.error(aE);
                }
            }

        } catch (aE){
            alert(aE.message);
        }
    }
})();