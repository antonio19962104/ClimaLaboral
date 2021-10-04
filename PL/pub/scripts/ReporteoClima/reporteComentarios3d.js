/* ==================  ================== */
/*                                        */
(function () {
    "use strict"
    angular.module("app2", []).controller("reporteComentariosController", reporteComentariosController);

    function reporteComentariosController($http, $scope) {
        try {
            var vm = this;
            vm.saludo = "Hello";
            vm.IdPregunta = 0;
            vm.keyWords_1 = [];
            vm.keyWords_2 = [];
            vm.keyWords_3 = [];
            vm.keyWords_4 = [];
            vm.keyWords_1.Objects = [];
            vm.keyWords_2.Objects = [];
            vm.keyWords_3.Objects = [];
            vm.keyWords_4.Objects = [];
            var script = document.scripts[0].src;
            var location = document.scripts[0].src.split('/')[2];
            vm.urlApis = "http://" + location + "/";
            vm.modelHistorico = Object;
            vm.model = [];
            vm.success = [];
            var count = 0;

            vm.getReporteDataPantalla_40 = function (noPregunta) {
                try {
                    var controls = $("#selector-level").find("input[type=checkbox]");
                    var listLevel = [];
                    [].forEach.call(controls, function (elem) {
                        if (elem.checked)
                            listLevel.push(elem.value);
                    });
                    vm.model = {
                        listUnudadesNeg: listUnidadNegocio,
                        niveles: listLevel,
                        idPregunta: noPregunta,
                        idBD: document.getElementById("DDLBD").value,
                        anio: document.getElementById("txtAnio").value
                    };
                    var valida = vm.validaGetData(noPregunta);
                    if (valida == true) {
                        if (noPregunta > 0) {
                            vm.isBusy = true;
                            fillArrayCustomHisto("apis/getDataNube3D/", vm.model, vm.getArray(noPregunta), function () {
                                console.log(vm.getArray(noPregunta));
                                vm.createGraph(vm.getArray(noPregunta), noPregunta);
                                count++;
                                if (count == 4)
                                    document.getElementById("loading").style.display = "none";
                            });
                        }
                        else {
                            swal("No se pudo reconocer el id de pregunta", "", "error");
                        }
                    }
                } catch (aE) {
                    console.log(aE);
                }
            }

            vm.validaGetData = function (noPreg) {
                try {
                    switch (noPreg) {
                        case 173:
                            if (vm.keyWords_1.Objects.length == 0) { return true; } else { return false }
                            break;
                        case 174:
                            if (vm.keyWords_2.Objects.length == 0) { return true; } else { return false }
                            break;
                        case 175:
                            if (vm.keyWords_3.Objects.length == 0) { return true; } else { return false }
                            break;
                        case 176:
                            if (vm.keyWords_4.Objects.length == 0) { return true; } else { return false }
                            break;
                    }
                } catch (aE) {
                    //vm.writteLog(aE.message, "vm.validGetData");
                    //swal(aE.message, "", "warning");
                }
            }

            vm.getArray = function (IdPregunta) {
                try {
                    if (IdPregunta == 173) {
                        return vm.keyWords_1;
                    }
                    if (IdPregunta == 174) {
                        return vm.keyWords_2;
                    }
                    if (IdPregunta == 175) {
                        return vm.keyWords_3;
                    }
                    if (IdPregunta == 176) {
                        return vm.keyWords_4;
                    }
                } catch (aE) {
                    vm.writteLog(aE.message, "vm.getArray");
                    //swal(aE.message, "", "warning");
                }
            }

            vm.createGraph = function (data, noPregunta) {
                try {
                    var words = data.data.Objects;

                    /*Conectores*/
                    var objLower = ["subir", "todas", "si", "va", "a", "ah", "ha", "las", "dice", "#", "todo", "hace", "nada", "pero", "todos", "hay", "poco", "sin", "ya", "1.", "1.que", "YA", "eso", "forma", "sea", "sin", "han", "Jesus", "1-", "alguna", "hace", "dar", "pesar", "de", "ah", "", " ", "poco", "hace", "hacer", "cualquier", "cualquiera", "puede", "solo", "ya", "sin", "pero", "ante", "a&b", "otro", "hable", "sabe", "crea", "buen", "ay", "area", "apoyan", "diferentes", "tener", "trabajar", "*Las", "apoya", "cual", "instalaciones", "preocupan", "permite", "hotel", "me", "mejor", "durante", "mes", "algo", "mucho", "hacemos", "y/o", "parte", "Hector,", "", "1.-", "2.-", "de", "para", "y", "el", "en", "que", "la", "DE", "los", "a", "se", "un", "nos", "con", "como", "siempre", "las", "es", ",", "algunos", "son", "se", "por", "un", "mi", "no", "sus", "ser", "lo", "nuestro", "su", "le", "muy", "mas", "del", "una", "cuando", "al", "o", "*", "1", "2", "3", "4", "5", "6", "7", "8", "9", ".", "cada", "dia", "uno", "bien", "nuestras", "nosotros", "\n", "tiene", "veces", "esta", "De", "tratan", "pasado", "subir", "ingrese", "tramite", ""];
                    console.log(objLower);
                    var objUpper = [];
                    objLower.forEach(function (value, index, arr) {
                        objUpper.push(objLower[index].toUpperCase());
                    });
                    /*Limpiar palabras*/
                    words.forEach(function (value, index, arr) {
                        if (objLower.indexOf(words[index].Palabra) != -1) {
                            words[index].Palabra = "";
                        }
                        if (objUpper.indexOf(words[index].Palabra) != -1) {
                            words[index].Palabra = "";
                        }
                    });

                    document.getElementById("lista" + noPregunta).innerHTML = "";
                    TagCanvas.Start('myCanvas' + noPregunta);
                    if (words.length > 85) {
                        for (var i = 0; i < 85; i++) {
                            if (words[i].Palabra != "") {
                                $("#lista" + noPregunta).append(
                                    '<li><a href="#mergeRespuestasA'+noPregunta+'">' + words[i].Palabra + "</a></li>"
                                );
                            }
                        }
                    }
                    if (words.length < 85) {
                        for (var i = 0; i < words.length; i++) {
                            if (words[i].Palabra != "") {
                                $("#lista" + noPregunta).append(
                                    '<li><a href="#">' + words[i].Palabra + "</a></li>"
                                );
                            }
                        }
                    }
                    try {
                        TagCanvas.Start('myCanvas' + noPregunta);
                    } catch (e) {
                        document.getElementById('myCanvasContainer' + noPregunta).style.display = 'none';
                    }
                } catch (aE) {
                    vm.writteLog(aE.message, "vm.createGraph");
                    //swal(aE.message, "", "warning");
                }
            }

            $(document).ready(function () {
                document.getElementById("lista173").onclick = (function (e) {
                    vm.palabra3DP1 = e.target.text;
                    vm.getComentariosByPalabra(173, vm.palabra3DP1)
                });
                document.getElementById("lista174").onclick = (function (e) {
                    vm.palabra3DP2 = e.target.text;
                    vm.getComentariosByPalabra(174, vm.palabra3DP2)
                });
                document.getElementById("lista175").onclick = (function (e) {
                    vm.palabra3DP3 = e.target.text;
                    vm.getComentariosByPalabra(175, vm.palabra3DP3)
                });
                document.getElementById("lista176").onclick = (function (e) {
                    vm.palabra3DP4 = e.target.text;
                    vm.getComentariosByPalabra(176, vm.palabra3DP4)
                });
            });
            
            vm.getComentariosByPalabra = function (pregunta, palabra) {
                var controls = $("#selector-level").find("input[type=checkbox]");
                var listLevel = [];
                [].forEach.call(controls, function (elem) {
                    if (elem.checked)
                        listLevel.push(elem.value);
                });
                vm.model = {
                    listUnudadesNeg: listUnidadNegocio,
                    niveles: listLevel,
                    idPregunta: pregunta,
                    idBD: document.getElementById("DDLBD").value,
                    anio: document.getElementById("txtAnio").value
                };
                document.getElementById("loading").style.display = "block";
                fillArrayCustomHisto("BackGroundJob/getComentariosByPalabra3D/?palabra=" + palabra + "/", vm.model, vm.success, function () {
                    console.log(vm.success);
                    document.getElementById("loading").style.display = "none";
                    document.getElementById("mergeRespuestasA" + pregunta).innerHTML = "";
                    $("#mergeRespuestasA" + pregunta).append(
                        '<p style="margin-bottom: 0px;">Palabra seleccionada: ' + palabra + '</p>'
                    );
                    for (var i = 0; i < vm.success.data.length; i++) {
                        $("#mergeRespuestasA" + pregunta).append(
                            '<li>' + vm.success.data[i] + '</li>'
                        );
                    }

                    /*resize*/
                    document.getElementById("myCanvas" + pregunta).offsetHeight;
                    document.getElementsByClassName("comentarios")[(pregunta - 1)].style.maxHeight = document.getElementById("myCanvas" + pregunta).offsetHeight + "px";
                });
            }

            vm.getDataReporte = function () {
                if (listUnidadNegocio.length == 0) {
                    swal("Debes elegir al menos una unidad de negocio", "", "info").then(function () {
                        return false;
                    });
                    return false;
                }
                document.getElementById("mergeRespuestasA173").innerHTML = "";
                document.getElementById("mergeRespuestasA174").innerHTML = "";
                document.getElementById("mergeRespuestasA175").innerHTML = "";
                document.getElementById("mergeRespuestasA176").innerHTML = "";
                count = 0;
                document.getElementById("loading").style.display = "block";
                vm.getReporteDataPantalla_40(173);
                vm.getReporteDataPantalla_40(174);
                vm.getReporteDataPantalla_40(175);
                vm.getReporteDataPantalla_40(176);
                document.getElementById('reporte').style.display = '';
            }

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

            //fillArrayCustomHisto
            function fillArrayCustomHisto(url, data, arreglo, funcion) {
                vm.post(url, data, function (response) {
                    angular.copy(response, arreglo);
                    if (funcion != null)
                        funcion();
                },
                    true);
            }

            

            vm.writteLog = function (ae, f) {

            }

            vm.writeLogFronEnd = function (ae) {

            }

            function messageBoxError() {
                return false;
            }

        } catch (aE) {
            swal(aE.message, "", "error");
        }
    }
})();