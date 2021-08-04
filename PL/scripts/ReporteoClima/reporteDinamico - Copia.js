(function () {
    "use strict"
    angular.module("app", []).controller("reporteDinamicoController", reporteDinamicoController);
    function reporteDinamicoController($http, $scope) {
        try {
            var vm = this;
            vm.saludo = "Hello from AngularJS";
            vm.urlApis = window.location.href.split("/")[0] + "//" + window.location.href.split('/')[2];
            vm.dataEE = [];
            vm.dataEA = [];
            vm.nuevo = [];

            vm.getDataReporte = function () {
                // armar modelo
                var controls = $("#selector-level").find("input[type=checkbox]");
                var listLevel = [];
                [].forEach.call(controls, function (elem) {
                    if (elem.checked)
                        listLevel.push(elem.value);
                });
                vm.model = {
                    listUnudadesNeg: listUnidadNegocio,
                    niveles: listLevel,
                    idPregunta: 0,
                    idBD: document.getElementById("DDLBD").value,
                    anio: document.getElementById("txtAnio").value
                };
                vm.UNSeleccionada = listUnidadNegocio[0];
                document.getElementById("loading").style.display = "none";
                vm.post("/ReporteoClima/ReporteDinamicoEE", vm.model, function (response) {
                    console.log(response.data);
                    var arrFinal = [];
                    for (var i = 0; i < response.data.length; i++) {
                        arrFinal = arrFinal.concat(response.data[i].Data);
                    }
                    vm.ComparativoGeneralPorNivelesEE.Data = arrFinal;
                    vm.seccionarArrayEERDinamico();
                    /*vm.post("/ReporteoClima/ReporteDinamicoEA", vm.model, function (response1) {
                        console.log(response1.data.Data.Data);
                        vm.ComparativoGeneralPorNivelesEA.Data = response1.data.Data.Data;
                        vm.seccionarArrayEARDinamico();
                    });*/
                });
            }
            vm.ComparativoGeneralPorNivelesEE = [];
            vm.ComparativoGeneralPorNivelesEE.Data = Object;
            vm.historialDinamico = [];
            vm.historialDinamicoEA = [];
            vm.seccionarArrayEERDinamico = function () {
                try {
                    // POR UNIDAD DE NEGOCIO TIPOENTIDAD = 1
                    if (vm.ComparativoGeneralPorNivelesEE.Data != undefined) {
                        if (vm.ComparativoGeneralPorNivelesEE.Data.length > 0 && vm.historialDinamico.length < vm.ComparativoGeneralPorNivelesEE.Data.length) {
                            var flag = 0;
                            var initialIndex = 0;
                            var finalIndex = 0;
                            vm.newArray = [];
                            vm.jsonGrafica = [];
                            var ultimoInicial = 0;
                            var banderaPapa = 0;
                            var banderaPapaHijo = -1;
                            var banderaPapaHijoHijo = -1;
                            var colorClase = "";
                            var colorBloque = "";
                            var iconoClase = "";

                            for (var i = 0; i < vm.ComparativoGeneralPorNivelesEE.Data.length; i++) {
                                if (i == 129) {
                                    // alert("posicion 129");
                                }
                                if (vm.ComparativoGeneralPorNivelesEE.Data[i].tipoEntidad == 2 && i < vm.ComparativoGeneralPorNivelesEE.Data.length - 1) {
                                    flag = flag + 1;
                                    if (flag == 1) {
                                        initialIndex = i;
                                    }
                                    if (flag == 2) {
                                        finalIndex = i;
                                        for (var j = initialIndex; j < finalIndex; j++) {
                                            vm.cuerpoContenidoHtml = "<div class='d_flex justify-content-center'>" +
                                                "<div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>" +
                                                "{{1}}" +
                                                "</center></div><div class='row d_flex justify-content-center'><div class='col-5'>" +
                                                "<center><div class='{{2}}'><center><img class='m-2 w-icon-impulsores' ng-src='{{3}}' src='{{3}}'>" +
                                                "</center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>{{4}}%</p>" +
                                                "<p class='yellow-clima f-17 mb-1 ng-binding'>HC:{{5}}</p>" +
                                                "</div></div></center></div></div></div>";
                                            vm.historialDinamico.push(i);
                                            if (vm.ComparativoGeneralPorNivelesEE.Data[j].tipoEntidad == 2) {
                                                colorClase = vm.setClase(vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje);
                                                colorBloque = vm.setClaseBGColor(colorClase);
                                                iconoClase = vm.setIcono(vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje);
                                                vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{1}}", vm.ComparativoGeneralPorNivelesEE.Data[j].Entidad);
                                                vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{2}}", colorClase);
                                                vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{3}}", iconoClase);
                                                vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{3}}", iconoClase);
                                                vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{4}}", vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje);
                                                vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{5}}", vm.ComparativoGeneralPorNivelesEE.Data[j].HC);
                                                try {
                                                    // VALIDAR UNIDAD
                                                    for (var p = 0; p < vm.jsonGrafica.length; p++) {
                                                        if (vm.jsonGrafica[p].name.includes(vm.ComparativoGeneralPorNivelesEE.Data[j].Entidad.substr(0, 3))) {
                                                            vm.jsonGrafica[p].children.push({
                                                                name: vm.cuerpoContenidoHtml, id: vm.getUid(), children: [], data: {
                                                                    $color: colorBloque
                                                                }
                                                            });
                                                        }
                                                    }
                                                } catch (e) {
                                                    console.log(e);
                                                }
                                                vm.cuerpoContenidoHtml = "";
                                            }

                                            if (vm.ComparativoGeneralPorNivelesEE.Data[j].tipoEntidad >= 3) {
                                                if (vm.ComparativoGeneralPorNivelesEE.Data[j].tipoEntidad == 3) {
                                                    colorClase = vm.setClase(vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje);
                                                    colorBloque = vm.setClaseBGColor(colorClase);
                                                    iconoClase = vm.setIcono(vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje);
                                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{1}}", vm.ComparativoGeneralPorNivelesEE.Data[j].Entidad);
                                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{2}}", colorClase);
                                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{3}}", iconoClase);
                                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{3}}", iconoClase);
                                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{4}}", vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje);
                                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{5}}", vm.ComparativoGeneralPorNivelesEE.Data[j].HC);

                                                    try {
                                                        // VALIDAR UNIDAD
                                                        for (var p = 0; p < vm.jsonGrafica.length; p++) {
                                                            if (vm.jsonGrafica[p].name.includes(vm.ComparativoGeneralPorNivelesEE.Data[j].Entidad.substr(0, 3))) {
                                                                vm.jsonGrafica[p].children[banderaPapa].children.push({ name: vm.cuerpoContenidoHtml, id: vm.getUid(), children: [], data: { $color: colorBloque } });
                                                            }
                                                        }
                                                    } catch (e) {
                                                        console.log(e);
                                                    }
                                                    banderaPapaHijo = banderaPapaHijo + 1;
                                                    banderaPapaHijoHijo = -1;
                                                    vm.cuerpoContenidoHtml = "";
                                                }

                                                if (vm.ComparativoGeneralPorNivelesEE.Data[j].tipoEntidad >= 4) {

                                                    if (vm.ComparativoGeneralPorNivelesEE.Data[j].tipoEntidad == 4) {
                                                        colorClase = vm.setClase(vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje);
                                                        colorBloque = vm.setClaseBGColor(colorClase);
                                                        iconoClase = vm.setIcono(vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje);
                                                        vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{1}}", vm.ComparativoGeneralPorNivelesEE.Data[j].Entidad);
                                                        vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{2}}", colorClase);
                                                        vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{3}}", iconoClase);
                                                        vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{3}}", iconoClase);
                                                        vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{4}}", vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje);
                                                        vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{5}}", vm.ComparativoGeneralPorNivelesEE.Data[j].HC);
                                                        try {
                                                            // VALIDAR UNIDAD
                                                            for (var p = 0; p < vm.jsonGrafica.length; p++) {
                                                                if (vm.jsonGrafica[p].name.includes(vm.ComparativoGeneralPorNivelesEE.Data[j].Entidad.substr(0, 3))) {
                                                                    vm.jsonGrafica[p].children[banderaPapa].children[banderaPapaHijo].children.push({
                                                                        name: vm.cuerpoContenidoHtml, id: vm.getUid(), children: [], data: { $color: colorBloque }, tipoEntidad: vm.ComparativoGeneralPorNivelesEE.Data[j].tipoEntidad, Entidad: vm.ComparativoGeneralPorNivelesEE.Data[j].Entidad, Frecuencia: vm.ComparativoGeneralPorNivelesEE.Data[j].Frecuencia, Porcentaje: vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje, HC: vm.ComparativoGeneralPorNivelesEE.Data[j].HC
                                                                    });
                                                                }
                                                            }
                                                        } catch (e) {
                                                            console.log(e);
                                                        }
                                                        banderaPapaHijoHijo = banderaPapaHijoHijo + 1;
                                                        vm.cuerpoContenidoHtml = "";
                                                    }
                                                }
                                                if (vm.ComparativoGeneralPorNivelesEE.Data[j].tipoEntidad == 5) {
                                                    colorClase = vm.setClase(vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje);
                                                    colorBloque = vm.setClaseBGColor(colorClase);
                                                    iconoClase = vm.setIcono(vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje);
                                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{1}}", vm.ComparativoGeneralPorNivelesEE.Data[j].Entidad);
                                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{2}}", colorClase);
                                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{3}}", iconoClase);
                                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{3}}", iconoClase);
                                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{4}}", vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje);
                                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{5}}", vm.ComparativoGeneralPorNivelesEE.Data[j].HC);
                                                    try {
                                                        // VALIDAR UNIDAD
                                                        for (var p = 0; p < vm.jsonGrafica.length; p++) {
                                                            if (vm.jsonGrafica[p].name.includes(vm.ComparativoGeneralPorNivelesEE.Data[j].Entidad.substr(0, 3))) {
                                                                vm.jsonGrafica[p].children[banderaPapa].children[banderaPapaHijo].children[banderaPapaHijoHijo].children.push({
                                                                    name: vm.cuerpoContenidoHtml, id: vm.getUid(), data: { $color: colorBloque }, tipoEntidad: vm.ComparativoGeneralPorNivelesEE.Data[j].tipoEntidad, Entidad: vm.ComparativoGeneralPorNivelesEE.Data[j].Entidad, Frecuencia: vm.ComparativoGeneralPorNivelesEE.Data[j].Frecuencia, Porcentaje: vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje, HC: vm.ComparativoGeneralPorNivelesEE.Data[j].HC
                                                                });
                                                            }
                                                        }
                                                    } catch (e) {
                                                        console.log(e);
                                                    }
                                                    vm.cuerpoContenidoHtml = "";
                                                }
                                            }
                                            if (j == finalIndex - 1) {
                                                banderaPapa = banderaPapa + 1;
                                            }

                                        }
                                        /*console.log(vm.newArray);
                                        vm.pintarCollecionEE(vm.newArray);
                                        vm.jsonGrafica.push(vm.newArray);
                                        console.log(vm.jsonGrafica);
                                        Reiniciar*/
                                        i = i - 1;
                                        ultimoInicial = finalIndex;
                                        initialIndex = 0;
                                        finalIndex = 0;
                                        flag = 0;
                                        banderaPapaHijo = -1;
                                        banderaPapaHijoHijo = -1;
                                        vm.newArray = [];
                                    }
                                }
                                else if (i == vm.ComparativoGeneralPorNivelesEE.Data.length - 1) {
                                    var inicial = ultimoInicial;
                                    /*console.log("Indices para secionar");
                                    console.log("Inicio:" + inicial + ". Fin: " + vm.ComparativoGeneralPorNivelesEE.Data.length - 1);*/
                                    for (var j = inicial; j < (vm.ComparativoGeneralPorNivelesEE.Data.length); j++) {
                                        vm.cuerpoContenidoHtml = "<div class='d_flex justify-content-center'>" +
                                            "<div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>" +
                                            "{{1}}" +
                                            "</center></div><div class='row d_flex justify-content-center'><div class='col-5'>" +
                                            "<center><div class='{{2}}'><center><img class='m-2 w-icon-impulsores' ng-src='{{3}}' src='{{3}}'>" +
                                            "</center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>{{4}}%</p>" +
                                            "<p class='yellow-clima f-17 mb-1 ng-binding'>HC:{{5}}</p>" +
                                            "</div></div></center></div></div></div>";
                                        if (vm.ComparativoGeneralPorNivelesEE.Data[j].tipoEntidad == 2) {
                                            colorClase = vm.setClase(vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje);
                                            colorBloque = vm.setClaseBGColor(colorClase);
                                            iconoClase = vm.setIcono(vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje);
                                            vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{1}}", vm.ComparativoGeneralPorNivelesEE.Data[j].Entidad);
                                            vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{2}}", colorClase);
                                            vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{3}}", iconoClase);
                                            vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{3}}", iconoClase);
                                            vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{4}}", vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje);
                                            vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{5}}", vm.ComparativoGeneralPorNivelesEE.Data[j].HC);
                                            try {
                                                // VALIDAR UNIDAD
                                                for (var p = 0; p < vm.jsonGrafica.length; p++) {
                                                    if (vm.jsonGrafica[p].name.includes(vm.ComparativoGeneralPorNivelesEE.Data[j].Entidad.substr(0, 3))) {
                                                        vm.jsonGrafica[p].children.push({ name: vm.cuerpoContenidoHtml, id: vm.getUid(), children: [], data: { $color: colorBloque }, tipoEntidad: vm.ComparativoGeneralPorNivelesEE.Data[j].tipoEntidad, Entidad: vm.ComparativoGeneralPorNivelesEE.Data[j].Entidad, Frecuencia: vm.ComparativoGeneralPorNivelesEE.Data[j].Frecuencia, Porcentaje: vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje, HC: vm.ComparativoGeneralPorNivelesEE.Data[j].HC });
                                                    }
                                                }
                                            } catch (e) {
                                                console.log(e);
                                            }
                                            vm.cuerpoContenidoHtml = "";
                                        }
                                        if (vm.ComparativoGeneralPorNivelesEE.Data[j].tipoEntidad >= 3) {
                                            if (vm.ComparativoGeneralPorNivelesEE.Data[j].tipoEntidad == 3) {
                                                colorClase = vm.setClase(vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje);
                                                iconoClase = vm.setIcono(vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje);
                                                vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{1}}", vm.ComparativoGeneralPorNivelesEE.Data[j].Entidad);
                                                vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{2}}", colorClase);
                                                vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{3}}", iconoClase);
                                                vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{3}}", iconoClase);
                                                vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{4}}", vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje);
                                                vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{5}}", vm.ComparativoGeneralPorNivelesEE.Data[j].HC);
                                                try {
                                                    // VALIDAR UNIDAD
                                                    for (var p = 0; p < vm.jsonGrafica.length; p++) {
                                                        if (vm.jsonGrafica[p].name.includes(vm.ComparativoGeneralPorNivelesEE.Data[j].Entidad.substr(0, 3))) {
                                                            vm.jsonGrafica[p].children.push({ name: vm.cuerpoContenidoHtml, id: vm.getUid(), children: [], data: { $color: colorBloque }, tipoEntidad: vm.ComparativoGeneralPorNivelesEE.Data[j].tipoEntidad, Entidad: vm.ComparativoGeneralPorNivelesEE.Data[j].Entidad, Frecuencia: vm.ComparativoGeneralPorNivelesEE.Data[j].Frecuencia, Porcentaje: vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje, HC: vm.ComparativoGeneralPorNivelesEE.Data[j].HC });
                                                        }
                                                    }
                                                } catch (e) {
                                                    console.log(e);
                                                }
                                                banderaPapaHijo = banderaPapaHijo + 1;
                                                vm.cuerpoContenidoHtml = "";
                                            }

                                            if (vm.ComparativoGeneralPorNivelesEE.Data[j].tipoEntidad >= 4) {
                                                if (vm.ComparativoGeneralPorNivelesEE.Data[j].tipoEntidad == 4) {
                                                    colorClase = vm.setClase(vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje);
                                                    colorBloque = vm.setClaseBGColor(colorClase);
                                                    iconoClase = vm.setIcono(vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje);
                                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{1}}", vm.ComparativoGeneralPorNivelesEE.Data[j].Entidad);
                                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{2}}", colorClase);
                                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{3}}", iconoClase);
                                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{3}}", iconoClase);
                                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{4}}", vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje);
                                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{5}}", vm.ComparativoGeneralPorNivelesEE.Data[j].HC);
                                                    try {
                                                        // VALIDAR UNIDAD
                                                        for (var p = 0; p < vm.jsonGrafica.length; p++) {
                                                            if (vm.jsonGrafica[p].name.includes(vm.ComparativoGeneralPorNivelesEE.Data[j].Entidad.substr(0, 3))) {
                                                                vm.jsonGrafica[p].children[banderaPapa].children[banderaPapaHijo].children.push({
                                                                    name: vm.cuerpoContenidoHtml, id: vm.getUid(), children: [], data: { $color: colorBloque }, tipoEntidad: vm.ComparativoGeneralPorNivelesEE.Data[j].tipoEntidad, Entidad: vm.ComparativoGeneralPorNivelesEE.Data[j].Entidad, Frecuencia: vm.ComparativoGeneralPorNivelesEE.Data[j].Frecuencia, Porcentaje: vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje, HC: vm.ComparativoGeneralPorNivelesEE.Data[j].HC
                                                                });
                                                            }
                                                        }
                                                    } catch (e) {
                                                        console.log(e);
                                                    }
                                                    banderaPapaHijoHijo = banderaPapaHijoHijo + 1;
                                                    vm.cuerpoContenidoHtml = "";
                                                }
                                            }
                                            if (vm.ComparativoGeneralPorNivelesEE.Data[j].tipoEntidad == 5) {
                                                colorClase = vm.setClase(vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje);
                                                colorBloque = vm.setClaseBGColor(colorClase);
                                                iconoClase = vm.setIcono(vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje);
                                                vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{1}}", vm.ComparativoGeneralPorNivelesEE.Data[j].Entidad);
                                                vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{2}}", colorClase);
                                                vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{3}}", iconoClase);
                                                vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{3}}", iconoClase);
                                                vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{4}}", vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje);
                                                vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{5}}", vm.ComparativoGeneralPorNivelesEE.Data[j].HC);
                                                try {
                                                    // VALIDAR UNIDAD
                                                    for (var p = 0; p < vm.jsonGrafica.length; p++) {
                                                        if (vm.jsonGrafica[p].name.includes(vm.ComparativoGeneralPorNivelesEE.Data[j].Entidad.substr(0, 3))) {
                                                            vm.jsonGrafica[p].children[banderaPapa].children[banderaPapaHijo].children[banderaPapaHijoHijo].children.push({
                                                                name: vm.cuerpoContenidoHtml, id: vm.getUid(), data: { $color: colorBloque }, tipoEntidad: vm.ComparativoGeneralPorNivelesEE.Data[j].tipoEntidad, Entidad: vm.ComparativoGeneralPorNivelesEE.Data[j].Entidad, Frecuencia: vm.ComparativoGeneralPorNivelesEE.Data[j].Frecuencia, Porcentaje: vm.ComparativoGeneralPorNivelesEE.Data[j].Porcentaje, HC: vm.ComparativoGeneralPorNivelesEE.Data[j].HC
                                                            });
                                                        }
                                                    }
                                                } catch (e) {
                                                    console.log(e);
                                                }
                                                vm.cuerpoContenidoHtml = "";
                                            }
                                        }
                                        if (j == vm.ComparativoGeneralPorNivelesEE.Data.length - 1) {
                                            banderaPapa = banderaPapa + 1;
                                        }

                                    }
                                    /*console.log(vm.jsonGrafica);
                                    Reiniciar
                                    i = i - 1; */
                                    ultimoInicial = 0;
                                    initialIndex = 0;
                                    finalIndex = 0;
                                    flag = 0;
                                    banderaPapaHijo = -1;
                                    banderaPapaHijoHijo = -1;
                                    vm.newArray = [];
                                }
                                else if (vm.ComparativoGeneralPorNivelesEE.Data[i].tipoEntidad == 1 && i < vm.ComparativoGeneralPorNivelesEE.Data.length - 1) {
                                    vm.cuerpoContenidoHtml = "<div class='d_flex justify-content-center'>" +
                                        "<div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>" +
                                        "{{1}}" +
                                        "</center></div><div class='row d_flex justify-content-center'><div class='col-5'>" +
                                        "<center><div class='{{2}}'><center><img class='m-2 w-icon-impulsores' ng-src='{{3}}' src='{{3}}'>" +
                                        "</center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>{{4}}%</p>" +
                                        "<p class='yellow-clima f-17 mb-1 ng-binding'>HC:{{5}}</p>" +
                                        "</div></div></center></div></div></div>";
                                    colorClase = vm.setClase(vm.ComparativoGeneralPorNivelesEE.Data[i].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEE.Data[i].Porcentaje);
                                    colorBloque = vm.setClaseBGColor(colorClase);
                                    iconoClase = vm.setIcono(vm.ComparativoGeneralPorNivelesEE.Data[i].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEE.Data[i].Porcentaje);
                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{1}}", vm.ComparativoGeneralPorNivelesEE.Data[i].Entidad);
                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{2}}", colorClase);
                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{3}}", iconoClase);
                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{3}}", iconoClase);
                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{4}}", vm.ComparativoGeneralPorNivelesEE.Data[i].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEE.Data[0].Porcentaje);
                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{5}}", vm.ComparativoGeneralPorNivelesEE.Data[i].HC);
                                    try {
                                        vm.jsonGrafica.push({ name: vm.cuerpoContenidoHtml, id: vm.getUid(), children: [], data: { $color: colorBloque } });// home
                                        // vm.jsonGrafica.children.push()
                                    } catch (e) {
                                        console.log(e);
                                    }
                                    vm.cuerpoContenidoHtml = "";
                                }
                            }
                        }

                        // validar si es una o dos uneg
                        if (vm.jsonGrafica.length > 1) {
                            vm.cuerpoContenidoHtml = "<div class='d_flex justify-content-center'>" +
                                "<div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>" +
                                "{{1}}" +
                                "</center></div><div class='row d_flex justify-content-center'><div class='col-5'>" +
                                "<center><div class='{{2}}'><center><img class='m-2 w-icon-impulsores' ng-src='{{3}}' src='{{3}}'>" +
                                "</center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>{{4}}%</p>" +
                                "<p class='yellow-clima f-17 mb-1 ng-binding'>HC:{{5}}</p>" +
                                "</div></div></center></div></div></div>";
                            colorClase = vm.setClase(vm.ComparativoGeneralPorNivelesEE.Data[0].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEE.Data[0].Porcentaje);
                            colorBloque = vm.setClaseBGColor(colorClase);
                            iconoClase = vm.setIcono(vm.ComparativoGeneralPorNivelesEE.Data[0].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEE.Data[0].Porcentaje);
                            vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{1}}", "Home");
                            vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{2}}", colorClase);
                            vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{3}}", iconoClase);
                            vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{3}}", iconoClase);
                            vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{4}}", vm.ComparativoGeneralPorNivelesEE.Data[0].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEE.Data[0].Porcentaje);
                            vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{5}}", vm.ComparativoGeneralPorNivelesEE.Data[0].HC);

                            vm.nuevo.push({ name: vm.cuerpoContenidoHtml, id: vm.getUid(), children: vm.jsonGrafica, data: { $color: colorBloque } });
                        }
                        var cargaJson = Object;
                        if (vm.jsonGrafica.length > 1) {
                            cargaJson = JSON.stringify(vm.nuevo);
                        }
                        if (vm.jsonGrafica.length == 1) {
                            cargaJson = JSON.stringify(vm.jsonGrafica);
                        }
                        
                        cargaJson = cargaJson.slice(1, -1);
                        let modelGrafico = { strJson: cargaJson }
                        let model = { cadena: cargaJson, titulo: vm.UNSeleccionada, enfoque: vm.enfoqueTexto }
                        $.ajax({
                            url: '/Reporte/PwCicleEE/',
                            type: 'POST',
                            data: model,
                            success: function (Response) {
                                if (Response != null) {
                                    $("#IdEE").append(Response);
                                    initLoad();
                                    setTimeout(function () {
                                        if (1 == 1) {
                                            $("#infovis").empty();
                                            initLoad();
                                        }
                                    }, 5000);
                                }
                            }
                        });
                    }
                } catch (aE) {
                    vm.writteLog(aE.message, "vm.SeccionarArrayEERDinamico");
                    swal(aE.message, "", "warning");
                }

            }
            vm.seccionarArrayEARDinamico = function () {
                try {
                    if (vm.ComparativoGeneralPorNivelesEA.Data != undefined) {
                        if (vm.ComparativoGeneralPorNivelesEA.Data.length > 0 && vm.historialDinamicoEA.length < vm.ComparativoGeneralPorNivelesEA.Data.length) {
                            var flag = 0;
                            var initialIndex = 0;
                            var finalIndex = 0;
                            vm.newArray = [];
                            vm.jsonGraficaEA = [];
                            var ultimoInicial = 0;
                            var banderaPapa = 0;
                            var banderaPapaHijo = -1;
                            var banderaPapaHijoHijo = -1;
                            var colorClase = "";
                            var colorBloque = "";
                            var iconoClase = "";

                            for (var i = 0; i < vm.ComparativoGeneralPorNivelesEA.Data.length; i++) {

                                if (vm.ComparativoGeneralPorNivelesEA.Data[i].tipoEntidad == 2 && i < vm.ComparativoGeneralPorNivelesEA.Data.length - 1) {/*Si en la ultima ya no encuentra nada aqui se cierra el recorrido*/
                                    flag = flag + 1;
                                    if (flag == 1) {
                                        initialIndex = i;
                                    }
                                    if (flag == 2) {
                                        finalIndex = i;
                                        for (var j = initialIndex; j < finalIndex; j++) {
                                            vm.cuerpoContenidoHtml = "<div class='d_flex justify-content-center'>" +
                                                "<div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>" +
                                                "{{1}}" +
                                                "</center></div><div class='row d_flex justify-content-center'><div class='col-5'>" +
                                                "<center><div class='{{2}}'><center><img class='m-2 w-icon-impulsores' ng-src='{{3}}' src='{{3}}'>" +
                                                "</center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>{{4}}%</p>" +
                                                "<p class='yellow-clima f-17 mb-1 ng-binding'>HC:{{5}}</p>" +
                                                "</div></div></center></div></div></div>";
                                            vm.historialDinamicoEA.push(i);
                                            if (vm.ComparativoGeneralPorNivelesEA.Data[j].tipoEntidad == 2) {
                                                colorClase = vm.setClase(vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje);
                                                colorBloque = vm.setClaseBGColor(colorClase);
                                                iconoClase = vm.setIcono(vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje);
                                                vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{1}}", vm.ComparativoGeneralPorNivelesEA.Data[j].Entidad);
                                                vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{2}}", colorClase);
                                                vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{3}}", iconoClase);
                                                vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{3}}", iconoClase);
                                                vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{4}}", vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje);
                                                vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{5}}", vm.ComparativoGeneralPorNivelesEA.Data[j].HC);
                                                try {
                                                    vm.jsonGraficaEA[0].children.push({
                                                        name: vm.cuerpoContenidoHtml, id: vm.getUid(), children: [], data: {
                                                            $color: colorBloque
                                                        }
                                                    });
                                                } catch (e) {

                                                }
                                                vm.cuerpoContenidoHtml = "";
                                            }

                                            if (vm.ComparativoGeneralPorNivelesEA.Data[j].tipoEntidad >= 3) {
                                                if (vm.ComparativoGeneralPorNivelesEA.Data[j].tipoEntidad == 3) {
                                                    colorClase = vm.setClase(vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje);
                                                    colorBloque = vm.setClaseBGColor(colorClase);
                                                    iconoClase = vm.setIcono(vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje);
                                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{1}}", vm.ComparativoGeneralPorNivelesEA.Data[j].Entidad);
                                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{2}}", colorClase);
                                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{3}}", iconoClase);
                                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{3}}", iconoClase);
                                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{4}}", vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje);
                                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{5}}", vm.ComparativoGeneralPorNivelesEA.Data[j].HC);
                                                    try {
                                                        vm.jsonGraficaEA[0].children[banderaPapa].children.push({ name: vm.cuerpoContenidoHtml, id: vm.getUid(), children: [], data: { $color: colorBloque } });
                                                    } catch (e) {

                                                    }
                                                    banderaPapaHijo = banderaPapaHijo + 1;
                                                    banderaPapaHijoHijo = -1;
                                                    vm.cuerpoContenidoHtml = "";
                                                }

                                                if (vm.ComparativoGeneralPorNivelesEA.Data[j].tipoEntidad >= 4) {

                                                    if (vm.ComparativoGeneralPorNivelesEA.Data[j].tipoEntidad == 4) {
                                                        colorClase = vm.setClase(vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje);
                                                        colorBloque = vm.setClaseBGColor(colorClase);
                                                        iconoClase = vm.setIcono(vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje);
                                                        vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{1}}", vm.ComparativoGeneralPorNivelesEA.Data[j].Entidad);
                                                        vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{2}}", colorClase);
                                                        vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{3}}", iconoClase);
                                                        vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{3}}", iconoClase);
                                                        vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{4}}", vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje);
                                                        vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{5}}", vm.ComparativoGeneralPorNivelesEA.Data[j].HC);
                                                        try {
                                                            vm.jsonGraficaEA[0].children[banderaPapa].children[banderaPapaHijo].children.push({
                                                                name: vm.cuerpoContenidoHtml, id: vm.getUid(), children: [], data: { $color: colorBloque }, tipoEntidad: vm.ComparativoGeneralPorNivelesEA.Data[j].tipoEntidad, Entidad: vm.ComparativoGeneralPorNivelesEA.Data[j].Entidad, Frecuencia: vm.ComparativoGeneralPorNivelesEA.Data[j].Frecuencia, Porcentaje: vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje, HC: vm.ComparativoGeneralPorNivelesEA.Data[j].HC
                                                            });
                                                        } catch (e) {

                                                        }
                                                        banderaPapaHijoHijo = banderaPapaHijoHijo + 1;
                                                        vm.cuerpoContenidoHtml = "";
                                                    }
                                                }
                                                if (vm.ComparativoGeneralPorNivelesEA.Data[j].tipoEntidad == 5) {
                                                    colorClase = vm.setClase(vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje);
                                                    colorBloque = vm.setClaseBGColor(colorClase);
                                                    iconoClase = vm.setIcono(vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje);
                                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{1}}", vm.ComparativoGeneralPorNivelesEA.Data[j].Entidad);
                                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{2}}", colorClase);
                                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{3}}", iconoClase);
                                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{3}}", iconoClase);
                                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{4}}", vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje);
                                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{5}}", vm.ComparativoGeneralPorNivelesEA.Data[j].HC);
                                                    try {
                                                        try {
                                                            vm.jsonGraficaEA[0].children[banderaPapa].children[banderaPapaHijo].children[banderaPapaHijoHijo].children.push({
                                                                name: vm.cuerpoContenidoHtml, id: vm.getUid(), data: { $color: colorBloque }, tipoEntidad: vm.ComparativoGeneralPorNivelesEA.Data[j].tipoEntidad, Entidad: vm.ComparativoGeneralPorNivelesEA.Data[j].Entidad, Frecuencia: vm.ComparativoGeneralPorNivelesEA.Data[j].Frecuencia, Porcentaje: vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje, HC: vm.ComparativoGeneralPorNivelesEA.Data[j].HC
                                                            });
                                                        } catch (e) {

                                                        }
                                                    } catch (e) {
                                                        console.log(e);
                                                    }
                                                    vm.cuerpoContenidoHtml = "";
                                                }
                                            }
                                            if (j == finalIndex - 1) {
                                                banderaPapa = banderaPapa + 1;
                                            }

                                        }
                                        /*console.log(vm.newArray);
                                        vm.pintarCollecionEE(vm.newArray);
                                        vm.jsonGrafica.push(vm.newArray);*/
                                        console.log(vm.jsonGraficaEA);
                                        /*Reiniciar*/
                                        i = i - 1;
                                        ultimoInicial = finalIndex;
                                        initialIndex = 0;
                                        finalIndex = 0;
                                        flag = 0;
                                        banderaPapaHijo = -1;
                                        banderaPapaHijoHijo = -1;
                                        vm.newArray = [];
                                    }
                                }
                                else if (i == vm.ComparativoGeneralPorNivelesEA.Data.length - 1) {
                                    var inicial = ultimoInicial;
                                    /*console.log("Indices para secionar");
                                    console.log("Inicio:" + inicial + ". Fin: " + vm.ComparativoGeneralPorNivelesEA.Data.length - 1);*/
                                    for (var j = inicial; j < (vm.ComparativoGeneralPorNivelesEA.Data.length); j++) {
                                        vm.cuerpoContenidoHtml = "<div class='d_flex justify-content-center'>" +
                                            "<div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>" +
                                            "{{1}}" +
                                            "</center></div><div class='row d_flex justify-content-center'><div class='col-5'>" +
                                            "<center><div class='{{2}}'><center><img class='m-2 w-icon-impulsores' ng-src='{{3}}' src='{{3}}'>" +
                                            "</center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>{{4}}%</p>" +
                                            "<p class='yellow-clima f-17 mb-1 ng-binding'>HC:{{5}}</p>" +
                                            "</div></div></center></div></div></div>";
                                        if (vm.ComparativoGeneralPorNivelesEA.Data[j].tipoEntidad == 2) {
                                            colorClase = vm.setClase(vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje);
                                            colorBloque = vm.setClaseBGColor(colorClase);
                                            iconoClase = vm.setIcono(vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje);
                                            vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{1}}", vm.ComparativoGeneralPorNivelesEA.Data[j].Entidad);
                                            vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{2}}", colorClase);
                                            vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{3}}", iconoClase);
                                            vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{3}}", iconoClase);
                                            vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{4}}", vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje);
                                            vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{5}}", vm.ComparativoGeneralPorNivelesEA.Data[j].HC);
                                            try {
                                                vm.jsonGraficaEA[0].children.push({ name: vm.cuerpoContenidoHtml, id: vm.getUid(), children: [], data: { $color: colorBloque }, tipoEntidad: vm.ComparativoGeneralPorNivelesEA.Data[j].tipoEntidad, Entidad: vm.ComparativoGeneralPorNivelesEA.Data[j].Entidad, Frecuencia: vm.ComparativoGeneralPorNivelesEA.Data[j].Frecuencia, Porcentaje: vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje, HC: vm.ComparativoGeneralPorNivelesEA.Data[j].HC });
                                            } catch (e) {

                                            }
                                            vm.cuerpoContenidoHtml = "";
                                        }
                                        if (vm.ComparativoGeneralPorNivelesEA.Data[j].tipoEntidad >= 3) {
                                            if (vm.ComparativoGeneralPorNivelesEA.Data[j].tipoEntidad == 3) {
                                                colorClase = vm.setClase(vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje);
                                                iconoClase = vm.setIcono(vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje);
                                                vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{1}}", vm.ComparativoGeneralPorNivelesEA.Data[j].Entidad);
                                                vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{2}}", colorClase);
                                                vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{3}}", iconoClase);
                                                vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{3}}", iconoClase);
                                                vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{4}}", vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje);
                                                vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{5}}", vm.ComparativoGeneralPorNivelesEA.Data[j].HC);
                                                try {
                                                    vm.jsonGraficaEA[0].children[banderaPapa].children.push({ name: vm.cuerpoContenidoHtml, id: vm.getUid(), children: [], data: { $color: colorBloque }, tipoEntidad: vm.ComparativoGeneralPorNivelesEA.Data[j].tipoEntidad, Entidad: vm.ComparativoGeneralPorNivelesEA.Data[j].Entidad, Frecuencia: vm.ComparativoGeneralPorNivelesEA.Data[j].Frecuencia, Porcentaje: vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje, HC: vm.ComparativoGeneralPorNivelesEA.Data[j].HC });
                                                } catch (e) {

                                                }
                                                banderaPapaHijo = banderaPapaHijo + 1;
                                                vm.cuerpoContenidoHtml = "";
                                            }

                                            if (vm.ComparativoGeneralPorNivelesEA.Data[j].tipoEntidad >= 4) {
                                                if (vm.ComparativoGeneralPorNivelesEA.Data[j].tipoEntidad == 4) {
                                                    colorClase = vm.setClase(vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje);
                                                    colorBloque = vm.setClaseBGColor(colorClase);
                                                    iconoClase = vm.setIcono(vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje);
                                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{1}}", vm.ComparativoGeneralPorNivelesEA.Data[j].Entidad);
                                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{2}}", colorClase);
                                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{3}}", iconoClase);
                                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{3}}", iconoClase);
                                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{4}}", vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje);
                                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{5}}", vm.ComparativoGeneralPorNivelesEA.Data[j].HC);
                                                    try {
                                                        vm.jsonGraficaEA[0].children[banderaPapa].children[banderaPapaHijo].children.push({
                                                            name: vm.cuerpoContenidoHtml, id: vm.getUid(), children: [], data: { $color: colorBloque }, tipoEntidad: vm.ComparativoGeneralPorNivelesEA.Data[j].tipoEntidad, Entidad: vm.ComparativoGeneralPorNivelesEA.Data[j].Entidad, Frecuencia: vm.ComparativoGeneralPorNivelesEA.Data[j].Frecuencia, Porcentaje: vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje, HC: vm.ComparativoGeneralPorNivelesEA.Data[j].HC
                                                        });
                                                    } catch (e) {

                                                    }
                                                    banderaPapaHijoHijo = banderaPapaHijoHijo + 1;
                                                    vm.cuerpoContenidoHtml = "";
                                                }
                                            }
                                            if (vm.ComparativoGeneralPorNivelesEA.Data[j].tipoEntidad == 5) {
                                                colorClase = vm.setClase(vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje);
                                                colorBloque = vm.setClaseBGColor(colorClase);
                                                iconoClase = vm.setIcono(vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje);
                                                vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{1}}", vm.ComparativoGeneralPorNivelesEA.Data[j].Entidad);
                                                vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{2}}", colorClase);
                                                vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{3}}", iconoClase);
                                                vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{3}}", iconoClase);
                                                vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{4}}", vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje);
                                                vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{5}}", vm.ComparativoGeneralPorNivelesEA.Data[j].HC);
                                                try {
                                                    vm.jsonGraficaEA[0].children[banderaPapa].children[banderaPapaHijo].children[banderaPapaHijoHijo].children.push({
                                                        name: vm.cuerpoContenidoHtml, id: vm.getUid(), data: { $color: colorBloque }, tipoEntidad: vm.ComparativoGeneralPorNivelesEA.Data[j].tipoEntidad, Entidad: vm.ComparativoGeneralPorNivelesEA.Data[j].Entidad, Frecuencia: vm.ComparativoGeneralPorNivelesEA.Data[j].Frecuencia, Porcentaje: vm.ComparativoGeneralPorNivelesEA.Data[j].Porcentaje, HC: vm.ComparativoGeneralPorNivelesEA.Data[j].HC
                                                    });
                                                } catch (e) {
                                                    console.log(e);
                                                }
                                                vm.cuerpoContenidoHtml = "";
                                            }
                                        }
                                        if (j == vm.ComparativoGeneralPorNivelesEA.Data.length - 1) {
                                            banderaPapa = banderaPapa + 1;
                                        }

                                    }
                                    /*console.log(vm.jsonGrafica);
                                    Reiniciar
                                    i = i - 1; */
                                    ultimoInicial = 0;
                                    initialIndex = 0;
                                    finalIndex = 0;
                                    flag = 0;
                                    banderaPapaHijo = -1;
                                    banderaPapaHijoHijo = -1;
                                    vm.newArray = [];
                                }
                                else if (vm.ComparativoGeneralPorNivelesEA.Data[i].tipoEntidad == 1 && i < vm.ComparativoGeneralPorNivelesEA.Data.length - 1) {
                                    vm.cuerpoContenidoHtml = "<div class='d_flex justify-content-center'>" +
                                        "<div class='d_flex justify-content-center mt-3 mb-5 pb-5'><center>" +
                                        "{{1}}" +
                                        "</center></div><div class='row d_flex justify-content-center'><div class='col-5'>" +
                                        "<center><div class='{{2}}'><center><img class='m-2 w-icon-impulsores' ng-src='{{3}}' src='{{3}}'>" +
                                        "</center><div class='clima-porcentaje robotoblack porcentaje-clima'><p class='mb-n2 f-21 ng-binding'>{{4}}%</p>" +
                                        "<p class='yellow-clima f-17 mb-1 ng-binding'>HC:{{5}}</p>" +
                                        "</div></div></center></div></div></div>";
                                    colorClase = vm.setClase(vm.ComparativoGeneralPorNivelesEA.Data[0].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEA.Data[0].Porcentaje);
                                    colorBloque = vm.setClaseBGColor(colorClase);
                                    iconoClase = vm.setIcono(vm.ComparativoGeneralPorNivelesEA.Data[0].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEA.Data[0].Porcentaje);
                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{1}}", vm.ComparativoGeneralPorNivelesEA.Data[0].Entidad);
                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{2}}", colorClase);
                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{3}}", iconoClase);
                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{3}}", iconoClase);
                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{4}}", vm.ComparativoGeneralPorNivelesEA.Data[0].Porcentaje == 'NaN' ? 0 : vm.ComparativoGeneralPorNivelesEA.Data[0].Porcentaje);
                                    vm.cuerpoContenidoHtml = vm.cuerpoContenidoHtml.replace("{{5}}", vm.ComparativoGeneralPorNivelesEA.Data[0].HC);
                                    try {
                                        vm.jsonGraficaEA.push({ name: vm.cuerpoContenidoHtml, id: vm.getUid(), children: [], data: { $color: colorBloque } });
                                    } catch (e) {

                                    }
                                    vm.cuerpoContenidoHtml = "";
                                }
                            }
                        }
                        /*console.log(vm.jsonGrafica);
                        document.getElementById("infovisEA").innerHTML;*/
                        var cargaJsonEA = JSON.stringify(vm.jsonGraficaEA);
                        ///cargaJsonEA = cargaJsonEA.slice(1, -1);
                        let modelGrafico = { strJson: cargaJsonEA }
                        let model = { cadena: cargaJsonEA, titulo: vm.UNSeleccionada }
                        $.ajax({
                            url: '/Reporte/PwCicleEA/',
                            type: 'POST',
                            data: model,
                            success: function (Response) {
                                if (Response != null) {
                                    $("#IdEA").append(Response);
                                    initLoadEA();
                                    /*vm.isBusy=true;*/
                                    setTimeout(function () {
                                        if (1 == 1) {
                                            $("#infovisEA").empty();
                                            initLoadEA();
                                            $scope.$apply(function () {
                                                vm.isBusy = false;
                                            });

                                        }
                                    }, 5000);
                                }
                            }
                        });
                    }

                } catch (aE) {
                    vm.writteLog(aE.message, "vm.SeccionarArrayEARDinamico");
                    swal(aE.message, "", "warning");
                }

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

            vm.getUid = function () {
                return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
                    var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
                    return v.toString(16);
                });
            }
            vm.imgNublado = "/img/ReporteoClima/nublado.svg";
            vm.imgLluvia = "/img/ReporteoClima/lluvia.svg";
            vm.imgSol = "/img/ReporteoClima/sol.svg";
            vm.imgSolNube = "/img/ReporteoClima/solnube.svg";
            vm.setClase = function (value) {
                try {
                    if (value == 'NaN')
                        value = 0;
                    value = parseFloat(value);
                    if (value < 70) {
                        return "lluvia"
                    }
                    if (value >= 70 && value < 80) {
                        return "nublado";
                    }
                    if (value >= 80 && value < 90) {
                        return "solnube";
                    }
                    if (value >= 90 && value <= 100) {
                        return "sol";
                    }
                    if (value > 100) {
                        swal("Existe un porcentaje mayor a 100, verificalo", "", "warning");
                    }
                } catch (aE) {
                    ////swal(aE.message, "", "warning");
                }
            }
            vm.setClaseBGColor = function (value) {
                try {
                    switch (value) {
                        case "sol":
                            return "#f0af42";
                            break;
                        case "solnube":
                            return "#ea973e";
                            break;
                        case "nublado":
                            return "#2f343a";
                            break;
                        case "lluvia":
                            return "#23282e";
                            break;
                        default:
                            return "#f0af42";
                            break;

                    }
                }
                catch (aE) {
                    ////swal(aE.message, "", "warning");
                }
            }


            vm.setIcono = function (value) {
                try {
                    if (value == 'NaN')
                        value = 0;
                    value = parseFloat(value);
                    if (value < 70) {
                        /*lluvia*/
                        return vm.imgLluvia;
                    }
                    if (value >= 70 && value < 80) {
                        /*nube*/
                        return vm.imgNublado;
                    }
                    if (value >= 80 && value < 90) {
                        /*solnube*/
                        return vm.imgSolNube;
                    }
                    if (value >= 90 && value <= 100) {
                        /*sol*/
                        return vm.imgSol;
                    }
                    if (value > 100) {
                        swal("Existe un porcentaje mayor a 100, verificalo", "", "warning");
                    }
                } catch (aE) {
                    ////swal(aE.message, "", "warning");
                }
            }

            vm.writteLog = function (ae, f) {  }

            vm.writeLogFronEnd = function (ae) {  }

            function messageBoxError() { return false; }

        } catch (aE) {
            swal(aE.message, "", "error")
        }
    }
})();