/*
 * Script del Modulo de Planes de Accion - Alta
 * 25/10/2021
 */
(function () {
    "use strict"
    angular.module("app", [])
        .controller("planAccionController", planAccionController);

    function planAccionController($http, $scope) {
        try {           
            var vm = this;
            vm.Modulo = "Configuración de Acciones de Mejora";
            if (IdEncuesta == 0) {
                vm.Modulo = "Configuración de Acciones de ayuda para el usuario";
            }
            vm.solIcono = "/img/ReporteoClima/Iconos/sol-icono.png";
            vm.solNubeIcono = "/img/ReporteoClima/Iconos/solnube-icono.png";
            vm.nubeIcono = "/img/ReporteoClima/Iconos/nube-icono.png";
            vm.lluviaIcono = "/img/ReporteoClima/Iconos/lluvia-icono.png";
            vm.ListadoEncuestasCL = [];
            vm.EstructuraAFM = [];
            vm.NombreEncuesta = "";
            vm.IdEncuesta = 0;
            vm.PeriodoEncuesta = "";
            vm.BaseEncuesta = "";
            vm.IdBaseEncuesta = "";
            vm.textAreaAgencia = "";
            vm.PromedioSubCategorias = [];
            vm.CategoriasAgrupadas = [];
            vm.Categorias = [];
            vm.AccionesModel = JSON.parse(JSON.stringify(_modelAcciones));
            vm.PlanDeAccionModel = JSON.parse(JSON.stringify(_modelPlanDeAccion));
            vm.AccionesPlanModel = JSON.parse(JSON.stringify(_modelAccionesPlan));
            vm.ResponsablePlanModel = JSON.parse(JSON.stringify(_modelResponsablePlan));
            vm.ListadoAccionesModel = [];
            vm.ListRangos = [];
            vm.IdRango = 0;
            vm.IdCategoria = 0;
            var modelNuevaAccion = {
                Descripcion: "",
                Rango: { IdRango: 0 },
                Estatus: { IdEstatus: 1 },
                Categoria: { IdCategoria: 0 },
                Encuesta: { IdEncuesta: IdEncuesta },
                BasesDeDatos: { IdBaseDeDatos: IdBaseDeDatos },
                AnioAplicacion: AnioAplicacion
            };
            var IdAccion = 0;
            vm.ListPeriodicidad = [];
            $(document).ready(function () {
                vm.ObtenerPeriodicidad();
                document.getElementById("loading").style.display = "block";
                $(".btn-parallelogram").css("display", "none");
                vm.ConsultaEncuestaPM();
                 vm.ObtenerRangos();//Para obtener el vm.ListRangos
            });

            vm.ConsultaEncuestaPM = function myfunction() {
                document.getElementById("gridEncuestaPM").style.display = "block";
                vm.get("/PlanesDeAccion/GetEncuestaPM/", function (response) {                    
                    console.log(response);
                    vm.ListadoEncuestasCL = [];
                    vm.ListadoEncuestasCL = response;
                    vm.gridEncuestasPM();
                    setTimeout(function () {
                        document.getElementById("loading").style.display = "none";
                    },500);                    
                });
            }

            vm.gridEncuestasPM = function () {
                try {
                    if ($("#gridEncuestaPM").data("kendoGrid")) {
                        $("#gridEncuestaPM").data("kendoGrid").destroy();
                        $("#gridEncuestaPM").empty();
                    }

                    $.getScript("http://demo.climalaboral.divisionautomotriz.com/scripts/kendo-all.js", function () {
                        $("#gridEncuestaPM").kendoGrid({
                            dataSource: {
                                transport: {
                                    read: function (e) {
                                        e.success(vm.ListadoEncuestasCL);
                                    }
                                },
                                batch: true,
                                schema: {
                                    model: {
                                        fields: {
                                            Nombre: {
                                                editable: false
                                            },
                                        }
                                    }
                                }, sort: { field: "BasesDeDatos.Nombre", dir: "desc"
                            }
                            },
                            navigatable: true,
                            groupable: false,
                            sortable: true,
                            selectable: "multiple",
                            reorderable: true,
                            resizable: true,
                            filterable: false,
                            columnMenu: false,
                            pageable: {
                                refresh: false,
                                pageSize: 10,
                                pageSizes: [5, 10, 20, 50, 100]
                            },
                            //height : 300, 
                            columns: [
                        {
                            field: "Nombre",
                            title: "Nombre",
                            headerAttributes: {
                                style: "text-align: center; vertical-align: middle; white-space: pre-wrap;"
                            }
                        },
                        {
                            field: "periodo",
                            title: "Periodo",
                            headerAttributes: {
                                style: "text-align: center; vertical-align: middle; white-space: pre-wrap;"
                            }
                        },
                        {
                            field: "BasesDeDatos.Nombre",
                            title: "Base de Datos",
                            headerAttributes: {
                                style: "text-align: center; vertical-align: middle; white-space: pre-wrap;"
                            }
                        },
                        {
                            field: "BasesDeDatos.IdBaseDeDatos",
                            title: "Id BD",
                            headerAttributes: {
                                style: "text-align: center; vertical-align: middle; white-space: pre-wrap;"
                            }
                        },
                        {
                            title: "Agrega Plan de Acción",
                            headerAttributes: {
                                style: "text-align: center; vertical-align: middle; white-space: pre-wrap;"
                            },
                            command: [{
                                text: "Agregar",
                                click: vm.addEncuestaPdA
                            }]

                        }
                            ],
                            editable: false,

                        })
                    });
                    //document.getElementById("loading").style.display = "none";
                }
                catch (aE) {
                    messageBoxError(aE);
                }
            }

            vm.addEncuestaPdA = function (e) {
                document.getElementById("loading").style.display = "block";
                e.preventDefault();
                var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
                document.getElementById("section1").style.display = "none";
                document.getElementById("section2").style.display = "block";
                IdBaseDeDatos = dataItem.BasesDeDatos.IdBaseDeDatos;
                vm.NombreEncuesta = dataItem.Nombre;
                vm.PeriodoEncuesta = dataItem.periodo;
                vm.BaseEncuesta = dataItem.BasesDeDatos.Nombre;
                vm.IdBaseEncuesta = dataItem.BasesDeDatos.IdBaseDeDatos;
                vm.IdEncuesta = dataItem.IdEncuesta;
                //se llenan las acciones 
                vm.AccionesModel.AnioAplicacion = dataItem.periodo;
                vm.AccionesModel.Encuesta.IdEncuesta = dataItem.IdEncuesta;
                vm.AccionesModel.BasesDeDatos.IdBaseDeDatos = dataItem.BasesDeDatos.IdBaseDeDatos;
                vm.AccionesModel.Descripcion = "";
                vm.post("/PlanesDeAccion/GetAcciones/?", vm.AccionesModel, function (response) {
                    if (response.Correct) {
                        console.log(response.Objects);
                        vm.ListadoAccionesModel = response.Objects;
                    }
                    else {
                        swal("Ocurrió un error al intentar consultar las acciones de ayuda", response.ErrorMessage, "error");
                    }
                });
                vm.ConsultaAreas();
                setTimeout(function () {
                    document.getElementById("loading").style.display = "none";
                }, 800);
            }

            vm.ConsultaAreas = function () {
                vm.get("/PlanesDeAccion/GetAreasForPlanAccion/?IdBaseDeDatos=" + IdBaseDeDatos, function (response) {
                    if (response.Correct) {
                        vm.EstructuraAFM = [];
                        [].forEach.call(response.Objects[0], function (item) {                          
                            vm.EstructuraAFM.push({ type: item.substring(0, 6), value: item.substring(6, (item.length)) });
                        });
                        var treeObject = vm.GenerarArbol();
                        var tw = new TreeView(
                            treeObject,
                            { showAlwaysCheckBox: true, fold: false });
                        $("#tree-view").empty();
                        document.getElementById("tree-view").appendChild(tw.root);
                        $("#tree-view p.group").css("display", "none");
                        $("#tree-view p.group:first").css("display", "");
                        
                    }
                    else {
                        swal("Ocurrió un error al consultar las áreas", response.ErrorMessage, "error");
                    }
                });
                //document.getElementById("accordion").classList.add("ng-hide");
            }

            vm.ConsultaAccionesAyuda = function () {
                vm.post("/PlanesDeAccion/GetAccionesAyuda/?", modelNuevaAccion, function (response) {
                    if (response.Correct) {
                        [].forEach.call(response.Objects, function (accion) {
                            $("#merge-acciones-ayuda").append('<span>' + accion.Descripcion + '</span>');
                        });
                    }
                });
            }

            vm.ConsultaAccionesGuardadas = function () {
                vm.post("/PlanesDeAccion/GetAcciones/?", modelNuevaAccion, function (response) {
                    if (response.Correct) {
                        $("#accordion .card-body").empty();
                        console.log(response.Objects);
                        AccionesPreGuardadas = response.Objects;
                        if (IdEncuesta > 0) {
                            [].forEach.call(response.Objects, function (accion) {
                                $("#merge-new-acciones-idcat-" + accion.Categoria.IdCategoria).append(
                                    `
                                <div class="form-group ng-scope" idAccion="` + accion.IdAccionDeMejora + `">
                                    <div class="form-inline">
                                        <div class="col-8">
                                            <button class="btn btn-sm re-asignar-accion"><i class="fas fa-sync-alt"></i></button>
                                            <input disabled value="` + accion.Descripcion + `" type="text" class="form-control is-valid" style="width: 95%;" placeholder="Acción de mejora">
                                        </div>
                                        <div class="col-3">
                                            <select disabled class="form-control select-rango is-valid" style="width: 100%;"></select>
                                        </div>
                                        <div class="col-1" style="display:block ruby;padding: 0;">
                                            <button class="btn edit-accion" onclick="editAccion(this);"><i class="fas fa-edit"></i></button>
                                            <button class="btn delete-accion"><i class="fas fa-trash"></i></button>
                                            <button class="btn save-action" onclick="GuardarAccion(this)"><i class="fas fa-save" style="color: #28a745;"></i></button>
                                        </div>
                                    </div>
                                </div>
                                `
                                );
                                $(".delete-accion").unbind();
                                $("#merge-new-acciones-idcat-" + accion.Categoria.IdCategoria + " .delete-accion").click(function (e) {
                                    vm.EliminarAccion(e);
                                });
                            });
                            setTimeout(function () {
                                var AuxIdCategoria;
                                var AuxIndex;
                                [].forEach.call(response.Objects, function (accion, index) {
                                    if (AuxIdCategoria != accion.Categoria.IdCategoria) {
                                        AuxIndex = 0;
                                    }
                                    AuxIdCategoria = accion.Categoria.IdCategoria;
                                    var itemPadre = document.getElementById("merge-new-acciones-idcat-" + accion.Categoria.IdCategoria);
                                    var select = itemPadre.getElementsByTagName("select")[AuxIndex];
                                    [].forEach.call(vm.ListRangos, function (item) {
                                        select.options.add(new Option(item.Descripcion, item.Id, false, false));
                                    });
                                    select.value = accion.Rango.IdRango;
                                    AuxIndex++;
                                });
                                vm.AgregarAccionesDefault();
                                $(".re-asignar-accion").click(function (e) {
                                    IdAccion_ReAsignar = e.target.closest(".form-group").attributes.IdAccion.value;
                                    $('#re-asignar-accion').modal('toggle');
                                });
                            }, 500);
                        }
                        else {
                            CrearGridSubCategorias();
                        }
                    }
                    else {
                        swal("Ocurrió un error al intentar consultar las acciones guardadas", response.ErrorMessage, "error");
                    }
                });
            }

            vm.ReAsignarAccion = function () {
                if (IdAccion_ReAsignar == 0) {
                    swal("Debes elegir la acción a re asignar", "", "info").then(function () {
                        return;
                    });
                    return;
                }
                if (document.getElementById("nueva-categoria").value == "0" || document.getElementById("nuevo-rango").value == "0") {
                    SetCampoInvalido(document.getElementById("nueva-categoria"));
                    SetCampoInvalido(document.getElementById("nuevo-rango"));
                    swal("Debes elegir la nueva categoria y el nuevo rango", "", "info").then(function () {
                        return;
                    });
                    return;
                }
                vm.get("/PlanesDeAccion/ReAsignar/?IdAccion=" + IdAccion_ReAsignar + "&IdCategoria=" + document.getElementById("nueva-categoria").value + "&IdRango=" + document.getElementById("nuevo-rango").value, function (response) {
                    if (response.Correct) {
                        swal("La acción de mejora ha sido re asignada exitosamente", "", "success").then(function () {
                            LimpiarModal();
                            vm.ConsultaAccionesGuardadas();
                        });
                    }
                    else {
                        swal("Ocurrió un error al intentar re asignar la acción", "", "error").then(function () {
                            LimpiarModal();
                        });
                    }
                });
            }

            vm.AgregarAccionesDefault = function () {
                var content =
                    `<div class="form-group">
                        <div class="form-inline">
                            <div class="col-8">
                                <input type="text" class ="form-control is-valid" style="width: 95%;" placeholder="Acción de mejora">
                            </div>
                            <div class="col-3">
                                <select class="form-control select-rango" style="width: 100%;"></select>
                            </div>
                            <div class="col-1">
                                <button class="btn delete-accion"><i class="fas fa-trash"></i></button>
                                <button class="btn save-action" onclick="GuardarAccion(this)"><i class="fas fa-save"></i></button>
                            </div>
                        </div>
                    </div>`;
                [].forEach.call(document.getElementById("accordion").children, function (item) {
                    var agregarOpciones = false;
                    if (item.getElementsByClassName("form-group").length == 0) {
                        agregarOpciones = true;
                        for (var i = 0; i < 1; i++) {
                            item.getElementsByClassName("card-body")[0].innerHTML += content;
                        }
                        if (agregarOpciones == true) {
                            [].forEach.call(item.getElementsByTagName("select"), function (select) {
                                if (select.options.length == 0) {
                                    [].forEach.call(vm.ListRangos, function (item) {
                                        select.options.add(new Option(item.Descripcion, item.Id, false, false));
                                    });
                                }
                            });
                            $(".delete-accion").unbind();
                            $(".delete-accion").click(function (e) {
                                vm.EliminarAccion(e);
                            });
                        }
                    }
                });
            }

            vm.GenerarArbol = function () {
                var uidUnidad, uidCompany, uidArea, uidDepto, uidSubd;
                var treeObject = [];
                /*Relacionar objeto*/
                [].forEach.call(vm.EstructuraAFM, function (item) {
                    if (item.type == "UNeg=>") {
                        uidUnidad = "UNEG_" + item.value + vm.getUid();
                    }
                    if (item.type == "Comp=>") {
                        uidCompany = "Comp_" + item.value + vm.getUid();
                    }
                    if (item.type == "Area=>") {
                        uidArea = "Area_" + item.value + vm.getUid();
                    }
                    if (item.type == "Dpto=>") {
                        uidDepto = "Depto_" + item.value + vm.getUid();
                    }
                    if (item.type == "SubD=>") {
                        uidSubd = "Subd_" + item.value + vm.getUid();
                    }

                    switch (item.type) {
                        case "UNeg=>":
                            item.IdUnidadNegocio = uidUnidad;
                            break;
                        case "Comp=>":
                            item.IdUnidadNegocio = uidUnidad;
                            item.CompanyId = uidCompany;
                            break;
                        case "Area=>":
                            item.IdUnidadNegocio = uidUnidad;
                            item.CompanyId = uidCompany;
                            item.IdArea = uidArea;
                            break;
                        case "Dpto=>":
                            item.IdUnidadNegocio = uidUnidad;
                            item.CompanyId = uidCompany;
                            item.IdArea = uidArea;
                            item.IdDepartamento = uidDepto;
                            break;
                        case "SubD=>":
                            item.IdUnidadNegocio = uidUnidad;
                            item.CompanyId = uidCompany;
                            item.IdArea = uidArea;
                            item.IdDepartamento = uidDepto;
                            item.IdSubdepartamento = uidSubd;
                            break;
                        default:
                    }
                });
                /*Push Unidad de Negocio*/
                [].forEach.call(vm.EstructuraAFM, function (item, index) {
                    treeObject.push({
                        isUnidad: true,
                        text: item.value,
                        checked: false,
                        id: index,
                        children: [
                            /*Division*/
                        ]
                    });
                    /*Push Division*/
                    var Division = Enumerable.from(vm.EstructuraAFM).where(o => o.type == "Comp=>" && o.IdUnidadNegocio == item.IdUnidadNegocio).toList();
                    if (Division.length == 0) {
                        delete treeObject[index];
                    }
                    else {
                        [].forEach.call(Division, function (elem, index2) {
                            treeObject[index].children.push({
                                text: elem.value,
                                checked: false,
                                id: 0,
                                children: [
                                    /*Area Agencia*/
                                ]
                            });
                            /*Push Area*/
                            var AreaAgencia = Enumerable.from(vm.EstructuraAFM).where(o => o.type == "Area=>" && o.CompanyId == elem.CompanyId).toList();
                            if (AreaAgencia.length == 0) {
                                delete treeObject[index].children[index2];
                            }
                            else {
                                [].forEach.call(AreaAgencia, function (elem) {
                                    treeObject[index].children[index2].children.push({
                                        text: elem.value,
                                        checked: false,
                                        id: 0,
                                        children: [
                                            /*Division*/
                                        ]
                                    });
                                });
                            }

                        });
                    }

                   
                });
                treeObject = Enumerable.from(treeObject).where(o => !o.text.includes("-")).toList();
                var treeFinal = [];
                treeFinal = [{
                    text: "Base De Datos",
                    checked: false,
                    id: 0,
                    children: treeObject
                }];               
                return treeFinal;
            }

            vm.getUid = function () {
                return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
                    var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
                    return v.toString(16);
                });
            }

            vm.get = function (url, functionOK, mostrarAnimacion) {
                $http.get(url, { headers: { 'Cache-Control': 'no-cache' } })
                    .then(function (response) {
                        try {
                            if (messageBoxError(response))
                                return;
                            functionOK(response.data);
                        }
                        catch (aE) {
                            messageBoxError(aE);
                        }
                    },
                        function (error) {
                            /*error*/
                            messageBoxError(error);
                        })
                    .finally(function () {
                    });
            }/*fin get()*/

            vm.post = function (url, objeto, functionOK, mostrarAnimacion) {
                $http.post(url, objeto)
                    .then(function (response) {
                        try {
                            if (messageBoxError(response))
                                return;
                            functionOK(response.data);
                        }
                        catch (aE) {
                            messageBoxError(aE);
                        }
                    },
                        function (error) {
                            /*error*/
                            messageBoxError(error);
                        })
                    .finally(function () {
                    });
            }/*fin post()*/

            function fillArray(url, arreglo, funcion) {
                vm.get(url, function (response) {
                    angular.copy(response, arreglo);
                    if (funcion != null)
                        funcion();
                },
                    true);
            }

            //obtiene los papas de la seleccion
            //var prent1 = this.parentNode;
            //var parent1Txt = "";
            //var parent2Txt = "";
            //var parent3Txt = "";
            //var txtAreaAgencia = "";
            //parent1Txt = prent1.parentNode.childNodes[1].data.text;
            //parent2Txt = prent1.parentNode.parentNode.children[1].data.text;
            //parent3Txt = prent1.children[1].data.text
            //txtAreaAgencia = parent2Txt + " - " + parent1Txt + " - " + parent3Txt;
            //fillSubCategorias(txtAreaAgencia);
            $(function () {
                $('#tree-view').on('click', '.btn-info', function (e) {
                    document.getElementById("loading").style.display = "block";
                    //alert("Entramos " + e.value);
                    var param3 = e.target.data.text;                 
                    vm.textAreaAgencia = param3;                   
                    // se llenan las subcategorias
                    var modelF = {
                        "AnioAplicacion": vm.PeriodoEncuesta,
                        "AreaAgencia": vm.textAreaAgencia, /*Se llena en base al area seleccionada*/
                        "IdBaseDeDatos": vm.IdBaseEncuesta,
                        "IdEncuesta": vm.IdEncuesta,
                        "IdPregunta": 0
                    };
                    vm.post("/PlanesDeAccion/GetPromediosSubCategoriasByAreaAgencia/", modelF, function (response) {
                        if (response.Correct) {
                            vm.Categorias = []; vm.PromedioSubCategorias = []; vm.CategoriasAgrupadas = [];
                            vm.PromedioSubCategorias = JSON.parse(response.Objects[0].JsonData);
                            vm.CategoriasAgrupadas = vm.AgruparCategorias(vm.PromedioSubCategorias);                            
                            vm.Categorias = vm.ExecutePA(vm.CategoriasAgrupadas);

                            // Guarda los promedios de las categorias
                            vm.GuardarPromediosCategorias(vm.Categorias);

                            $("#listAcciones").empty();
                            [].forEach.call(vm.Categorias, function (item) {
                                item.PromedioGeneral = Math.round(item.PromedioGeneral * 100) / 100;
                                $("#listAcciones").append(
                                   `<div id= "cat_` + item.IdCategoria + `" style="padding: 0 .5rem !important; display:none;">
                                        <div class ="title-header-blue">
                                            <div class ="row">
                                                <div class ="col-10">
                                                <span class ="title-header-section"> `+ item.Categoria +` </span>
                                                <span class ="title-header-section ico"> `+ item.PromedioGeneral +`%       </span>
                                                </div>
                                                <div class ="col-2 text-center">
                                                    <button title="Regresar" type="button" idcat = "` + item.IdCategoria + `" class ="btn btn-secondary btn-action ml-1 btn-back-action"><i idcat = "` + item.IdCategoria + `" class ="far fa-arrow-alt-circle-left"></i></button>
                                                    <button title="Agregar Accion" type="button" idcat = "` + item.IdCategoria + `" class ="btn btn-secondary btn-action ml-1 btn-add-action"><i idcat = "` + item.IdCategoria + `" class ="fas fa-plus-circle"></i></button>
                                                </div>
                                            </div>
                                        </div>
                                        <div class ="form-group">
                                            <div id="accordion"></div>
                                        </div>
                                    </div>`);
                                //Se inserta el icono
                                var imagenIco = vm.setIconoSVG(item.PromedioGeneral);
                                if (imagenIco != "") {
                                    $("#cat_" + item.IdCategoria + " .ico").append(`<img src="` + imagenIco + `" class="img-fluid" style="background-color: white;width: 4%;">`);
                                }
                                [].forEach.call(vm.ListRangos, function (items) {
                                    if (item.PromedioGeneral >= parseFloat(items.valor1) && item.PromedioGeneral <= parseFloat(items.valor2))
                                    {
                                        item.IdRango = items.Id;
                                        item.Rango = items.Descripcion;
                                    };
                                });
                            });

                            document.getElementById("section2").style.display = "none";
                            document.getElementById("section3").style.display = "block";
                            document.getElementById("HselecCat").style.display = "block";
                            document.getElementById("BselecCat").style.display = "block";
                            $.getScript("http://demo.climalaboral.divisionautomotriz.com/scripts/kendo-all.js", function () {
                                $("#gridCategoriasPM").kendoGrid({
                                    dataSource: {
                                        transport: {
                                            read: function (e) {
                                                e.success(vm.Categorias);
                                            }
                                        },
                                        batch: true,
                                        schema: {
                                            model: {
                                                    fields: {
                                                        Categoria: { editable: false },
                                                        PromedioGeneral: { type: "number", editable: false },
                                                        IdCategoria: {type: "button", editable: false},
                                                        Rango: {editable:false},
                                                    }
                                            }
                                        }, sort: {
                                            field: "PromedioGeneral", dir: "desc"
                                        }
                                    },
                                    columnMenu: {
                                        filterable: false
                                    },
                                    navigatable: true,
                                    groupable: false,
                                    sortable: true,
                                    selectable: "multiple",
                                    reorderable: true,
                                    resizable: true,
                                    filterable: false,
                                    columnMenu: false,
                                    pageable: {
                                        refresh: false,
                                        pageSize: 10,
                                        pageSizes: [5, 10, 20, 50, 100]
                                    },
                                    //height : 300, 
                                    columns: [
                                    {
                                        field: "Categoria",
                                        title: "Categoria",
                                        headerAttributes: { style: "text-align: center; vertical-align: middle; white-space: pre-wrap;" },
                                        editable: false
                                    }, {
                                        field: "PromedioGeneral",
                                        title: "Promedio",
                                        headerAttributes: { style: "text-align: center; vertical-align: middle; white-space: pre-wrap;" },
                                        attributes: { class: "text-center" },
                                        template: function (dataItem) {
                                            try {
                                                dataItem.PromedioGeneral = parseFloat(dataItem.PromedioGeneral);
                                                if (dataItem.PromedioGeneral < 70) {
                                                    return dataItem.PromedioGeneral +"%";
                                                }
                                                if (dataItem.PromedioGeneral >= 70 && dataItem.PromedioGeneral < 80) {
                                                    return dataItem.PromedioGeneral + "%";
                                                }
                                                if (dataItem.PromedioGeneral >= 80 && dataItem.PromedioGeneral < 90) {
                                                    return dataItem.PromedioGeneral + "%";
                                                }
                                                if (dataItem.PromedioGeneral >= 90 && dataItem.PromedioGeneral <= 100) {
                                                    return dataItem.PromedioGeneral + "%";
                                                }
                                            } catch (aE) {
                                                return "no";
                                            }
                                        },
                                    },
                                    {
                                        field: "Rango",
                                        title: "Icono",
                                        editable: false,
                                        headerAttributes: { style: "text-align: center; vertical-align: middle; white-space: pre-wrap;" },
                                        attributes: { class: "text-center" },
                                        template: function (dataItem) {
                                            try {
                                                dataItem.PromedioGeneral = parseFloat(dataItem.PromedioGeneral);
                                                if (dataItem.PromedioGeneral < 70) {
                                                    return " <img src='/img/ReporteoClima/Iconos/lluvia-icono.png' style='width: 18%;'>";
                                                }
                                                if (dataItem.PromedioGeneral >= 70 && dataItem.PromedioGeneral < 80) {
                                                    return " <img src='/img/ReporteoClima/Iconos/nube-icono.png' style='width: 18%;'>";
                                                }
                                                if (dataItem.PromedioGeneral >= 80 && dataItem.PromedioGeneral < 90) {
                                                    return " <img src='/img/ReporteoClima/Iconos/solnube-icono.png' style='width: 18%;'>";
                                                }
                                                if (dataItem.PromedioGeneral >= 90 && dataItem.PromedioGeneral <= 100) {
                                                    return " <img src='/img/ReporteoClima/Iconos/sol-icono.png' style='width: 18%;'>";
                                                }
                                            } catch (aE) {
                                                return "no";
                                            }
                                        },
                                    },
                                    {
                                        title: "Seleccionar",
                                        headerAttributes: { style: "text-align: center; vertical-align: middle; white-space: pre-wrap;" },
                                        attributes: { class: "text-center" },
                                        template: "<span class='btn btn-info'>Seleccionar</span>"
                                    }
                                    ],
                                    editable: false,

                                })
                            });
                            //vm.ObtenerRangos();
                        }
                        else {
                            swal("Ocurrió un error al intentar consultar las subcategorias", response.ErrorMessage, "error");
                        }
                    });
                    setTimeout(function () {
                        document.getElementById("loading").style.display = "none";
                    }, 500);

                })
                $('#gridCategoriasPM').on('click', '.btn-info', function (e) {
                    document.getElementById("loading").style.display = "block";
                    document.getElementById("HselecCat").style.display = "none";
                    document.getElementById("BselecCat").style.display = "none";
                    $(".btn-parallelogram").css("display", "block");
                    var grid = $('#gridCategoriasPM').data().kendoGrid;
                    var dataItem = grid.dataItem($(this).closest('tr'));
                    //se obtiene el id de Rango
                    vm.IdRango = dataItem.IdRango;
                    //se obtiene el id de categoria
                    vm.IdCategoria =dataItem.IdCategoria;
                    //Se abre el contenedor de la categoria con el listado de las acciones
                    document.getElementById("cat_" + dataItem.IdCategoria).style.display = "block";
                    //Se crea el listado de las acciones filtrando por el id de Categoria
                    //Se consulta las acciones * idCategoria
                    //se verifica si existen acciones en el acordion de categoria
                    var accionesXcategoria = $("#cat_"+dataItem.IdCategoria+" #accordion")[0].children.length;
                    if (accionesXcategoria == 0) {
                        var accionesXcat = Enumerable.from(vm.ListadoAccionesModel).where(o => o.Categoria.IdCategoria == dataItem.IdCategoria && o.Rango.IdRango == vm.IdRango).toList();
                        if (accionesXcat.length > 0) {

                            [].forEach.call(accionesXcat, function (item) {
                                var numAcciones = $("#accordion .card").length;
                                if (numAcciones == 0) {
                                    numAcciones = 1;
                                }
                                else {
                                    numAcciones = numAcciones + 1
                                }
                                $("#cat_" + dataItem.IdCategoria + " #accordion").append(`
                                <div class ="card">
	                               <div class ="card-header" id="heading`+ item.IdAccionDeMejora + `_` + numAcciones + `" >
		                               <h5 class ="mb-0">
			                               <span class ="btn btn-link col-10" data-toggle="collapse" data-target= "#collapse`+ item.IdAccionDeMejora + `_` + numAcciones + `" aria-expanded="true" aria-controls= "collapse` + item.IdAccionDeMejora + `_` + numAcciones + `" >
				                               <input type="text" placeholder="Ingresa nombre de la acción" value= "`+ item.Descripcion + `" class ="form-control txt-Nom-Acc" />
			                               </span>
                                           <span class="col-2">
                                           <i class ="far fa-times-circle eliminaAccion" style="cursor: pointer;"></i>
                                           </span>
		                               </h5>
	                               </div>

	                               <div id= "collapse`+ item.IdAccionDeMejora + `_` + numAcciones + `" class ="collapse" aria-labelledby= "heading` + item.IdAccionDeMejora + `_` + numAcciones + `" data-parent = "#accordion" >
		                               <div class ="card-body">
			                               <div class ="row form-group">
                                                <div class ="col-3">Periodicidad: </div>
                                                <div class ="col-5">
                                                <input type="hidden" placeholder="Ingresa periocidad" class ="form-control frm-periodicidad" />                                                
                                                <select class ="form-control frm-periodicidadS select-periodicidad`+item.IdAccionDeMejora+`" style="width: 100%;"></select>
                                                </div>
                                                <div class ="col-2"><i class ="far fa-calendar-alt"></i>Inicia:<input type="date" placeholder="Ingresa fecha de inicio" class ="form-control frm-fecini" /></div>
                                                <div class ="col-2"><i class ="far fa-calendar-alt"></i>Concluya:<input type="date" placeholder="Ingresa fecha de término" class ="form-control frm-fecfin" /></div>
                                            </div>
                                            <div class ="row form-group">
                                                <div class ="col-9">Objetivo: </div>
                                                <div class ="col-3">Meta: </div>
                                            </div>
                                            <div class ="row form-group">
                                                <div  class ="col-9"><input type="text" placeholder="Objetivo a alcanzar con la acción" class ="form-control frm-objetivo" /></div>
                                                <div  class ="col-3"><input type="text" placeholder="Ingresa meta" class ="form-control frm-meta" /></div>
                                            </div>
                                            <div class ="row form-group">
                                                <div class ="col-6"><i class = "fas fa-plus add-responsable" idAccion= `+item.IdAccionDeMejora+` idcat="`+ item.IdAccionDeMejora+`" ></i> Responsable: </div>
                                                <div class ="col-6">Email: </div>
                                            </div>
                                            <section class = "reponsables_`+ item.IdAccionDeMejora + `  frm-responsables" >
                                            <div class ="row form-group">
                                                <div class ="col-6"><input type="text" placeholder="Ingresa nombre del responsable" class ="form-control frm-respon" /></div>
                                                <div class ="col-6"><input type="text" placeholder="Ingresa correo electrónico" class ="form-control frm-respon-email" /></div>
                                            </div>
                                            </section>
                                            <div class ="row form-group">
                                                <div class ="col-3">Comentarios: </div>
                                                <div class ="col-9"><input type="text" placeholder="Ingresa comentarios" class ="form-control frm-comentarios" /></div>
                                            </div>
                                            <input type="hidden" class ="newIdAccion" value= "`+item.IdAccionDeMejora+ `" />
                                            <input type="hidden" class ="idCat" value= "`+dataItem.IdCategoria+ `" />
		                               </div>
	                               </div>
                               </div>
                                `
                                );
                                var idselectAccion = ".select-periodicidad" + item.IdAccionDeMejora;
                                [].forEach.call(vm.ListPeriodicidad, function (item) {
                                    $(idselectAccion).append("<option value='" + item.Id + "'>" + item.Descripcion + "</option>");
                                });

                            });
                            //se agrega el select de Periodiicidad
                            

                        }
                        else {
                            $("#cat_" + dataItem.IdCategoria + " #accordion").append("<div class ='col-12 notieneAcc'><center><strong>No cuenta con Acciones asignadas</strong></center></div>");
                        }
                    }

                    setTimeout(function () {
                        document.getElementById("loading").style.display = "none";
                    }, 500);
                });
                $('#listAcciones').on('click', '.btn-add-action', function (e) {
                    var idCat = e.target.attributes.idcat.value;
                    var idTipo = 0;
                    swal("Ésta acción será Genérica?", {
                        buttons: {
                            cancel: "Cancelar",
                            si: {
                                text: "Si",
                                value: "si",
                            },
                            no: {
                                text: "No",
                                value:"no",
                            },
                        },
                    })
                    .then((value) => {
                        switch (value) {

                            case "no":
                                //swal("No es generica!");
                                idTipo = 1;
                                vm.AddAccionPlan(idCat,idTipo);
                                break;

                            case "si":
                                //swal("Si!", "Es Acción Generica!", "success");
                                idTipo = 2;
                                vm.AddAccionPlan(idCat,idTipo);
                                break;

                            default:
                                swal("Acción Cancelada!");
                        }
                    });

                });


                $('#listAcciones').on('click', '.btn-back-action', function (e) {
                    //se agregan las acciones al modelo General
                    document.getElementById("loading").style.display = "block";
                    var validarEnvio = false;
                    vm.NombreEncuesta; vm.PeriodoEncuesta; vm.BaseEncuesta; vm.textAreaAgencia;
                    vm.IdRango; vm.IdCategoria;
                    vm.PlanDeAccionModel.Nombre = vm.NombreEncuesta + "_" + vm.PeriodoEncuesta + "_" + vm.BaseEncuesta + "_" + vm.textAreaAgencia;
                    vm.PlanDeAccionModel.Area = vm.textAreaAgencia;
                    vm.PlanDeAccionModel.IdEncuesta = vm.IdEncuesta;
                    vm.PlanDeAccionModel.IdBaseDeDatos = vm.IdBaseEncuesta;
                    vm.PlanDeAccionModel.AnioAplicacion = vm.PeriodoEncuesta;
                    //se localiza las acciones
                    var accionesLocalizadas = $("body").find("#accordion .card:visible");
                    //se valida por lo menos una acción
                    if (accionesLocalizadas.length == 0) {
                        swal("Debes agregar por lo menos una acción a la categoria", "", "info").then(function () {
                            document.getElementById("loading").style.display = "none";
                            return false;
                        });
                        document.getElementById("loading").style.display = "none";
                        return false;
                    }
                    //se valida los datos de las acciones
                    [].forEach.call(accionesLocalizadas, function (item) {
                        var idAccionAltaV = item.getElementsByClassName("newIdAccion")[0].value;
                        var idPeriodicidad = "frm-periodicidad" + idAccionAltaV;
                        var NombreV = item.getElementsByClassName("txt-Nom-Acc")[0].value;
                        var PeriodicidadV = item.getElementsByClassName("frm-periodicidadS")[0].value;
                        var FechaInicioV = item.getElementsByClassName("frm-fecini")[0].value;
                        var FechaFinV = item.getElementsByClassName("frm-fecfin")[0].value;
                        var ObjetivoV = item.getElementsByClassName("frm-objetivo")[0].value;
                        var MetaV = item.getElementsByClassName("frm-meta")[0].value;
                        var ComentariosV = item.getElementsByClassName("frm-comentarios")[0].value;
                        if (NombreV == "" || NombreV == "null") {
                            swal("Debes agregar nombre a la acción", "", "info").then(function () {
                                SetCampoInvalido(item.getElementsByClassName("txt-Nom-Acc")[0]);
                                validarEnvio = false;
                                return false;
                            });
                            validarEnvio = false;
                            return false;
                        } else { validarEnvio = true; }
                        if (FechaInicioV == "") {
                            swal("Debes agregar una fecha inicio a la acción", "", "info").then(function () {
                                SetCampoInvalido(item.getElementsByClassName("frm-fecini")[0]);
                                validarEnvio = false;
                                return false;
                            });
                            validarEnvio = false;
                            return false;
                        } else { validarEnvio = true; }
                        if (FechaFinV == "") {
                            swal("Debes agregar una fecha fin a la acción", "", "info").then(function () {
                                SetCampoInvalido(item.getElementsByClassName("frm-fecfin")[0]);
                                validarEnvio = false;
                                return false;
                            });
                            validarEnvio = false;
                            return false;
                        } else { validarEnvio = true; }
                        if (ObjetivoV == "") {
                            swal("Debes agregar objetivo a la acción", "", "info").then(function () {
                                SetCampoInvalido(item.getElementsByClassName("frm-objetivo")[0]);
                                validarEnvio = false;
                                return false;
                            });
                            validarEnvio = false;
                            return false;
                        } else { validarEnvio = true; }
                        if (MetaV == "") {
                            swal("Debes agregar meta a la acción", "", "info").then(function () {
                                SetCampoInvalido(item.getElementsByClassName("frm-meta")[0]);
                                validarEnvio = false;
                                return false;
                            });
                            validarEnvio = false;
                            return false;
                        } else { validarEnvio = true; }
                        if (ComentariosV == "") {
                            swal("Debes agregar comentarios a la acción", "", "info").then(function () {
                                SetCampoInvalido(item.getElementsByClassName("frm-comentarios")[0]);
                                validarEnvio = false;
                                return false;
                            });
                            validarEnvio = false;
                            return false;
                        } else { validarEnvio = true; }
                        //se validan los responsables

                        var lresponsablesV = $(".reponsables_" + idAccionAltaV + "").find(".form-group");
                        [].forEach.call(lresponsablesV, function (itemr) {
                            var NombreVR = itemr.getElementsByClassName("frm-respon")[0].value;
                            var EmailVR = itemr.getElementsByClassName("frm-respon-email")[0].value;
                            if (NombreVR == "") {
                                swal("Debes agregar nombre de responsable", "", "info").then(function () {
                                    SetCampoInvalido(item.getElementsByClassName("frm-respon")[0]);
                                    validarEnvio = false;
                                    return false;
                                });
                                validarEnvio = false;
                                return false;
                            } else { validarEnvio = true; }
                            if (EmailVR == "") {
                                swal("Debes agregar email de responsable", "", "info").then(function () {
                                    SetCampoInvalido(item.getElementsByClassName("frm-respon-email")[0]);
                                    validarEnvio = false;
                                    return false;
                                });
                                validarEnvio = false;
                                return false;
                            } else { validarEnvio = true; }
                        });

                    });
                    //Despues de validar que no tenga campos vacios
                    //por el numero de acciones que tenga el Plan de Accion                   
                    if (validarEnvio) {
                        document.getElementById("loading").style.display = "block";
                        [].forEach.call(accionesLocalizadas, function (item) {
                            var idAccionAltaV = item.getElementsByClassName("newIdAccion")[0].value;
                            var idPeriodicidad = "frm-periodicidad" + idAccionAltaV;
                            var idAccionAlta = item.getElementsByClassName("newIdAccion")[0].value;
                            vm.AccionesPlanModel.IdAccion = idAccionAlta;
                            vm.AccionesPlanModel.PlanDeAccion.Nombre = item.getElementsByClassName("txt-Nom-Acc")[0].value;
                            vm.AccionesPlanModel.Periodicidad = item.getElementsByClassName("frm-periodicidadS")[0].value;
                            vm.AccionesPlanModel.FechaInicio = item.getElementsByClassName("frm-fecini")[0].value;
                            vm.AccionesPlanModel.FechaFin = item.getElementsByClassName("frm-fecfin")[0].value;
                            vm.AccionesPlanModel.Objetivo = item.getElementsByClassName("frm-objetivo")[0].value;
                            vm.AccionesPlanModel.Meta = item.getElementsByClassName("frm-meta")[0].value;
                            vm.AccionesPlanModel.Comentarios = item.getElementsByClassName("frm-comentarios")[0].value;

                            var lresponsables = $(".reponsables_" + idAccionAlta + "").find(".form-group");
                            [].forEach.call(lresponsables, function (itemr) {
                                vm.ResponsablePlanModel.Responsable.Nombre = itemr.getElementsByClassName("frm-respon")[0].value;
                                vm.ResponsablePlanModel.Responsable.Email = itemr.getElementsByClassName("frm-respon-email")[0].value;
                                vm.AccionesPlanModel.ListadoResponsables.push(vm.ResponsablePlanModel);
                                vm.ResponsablePlanModel = JSON.parse(JSON.stringify(_modelResponsablePlan));
                            });
                            //vm.AccionesPlanModel.ListadoResponsables.push(AccionesPlanObj);
                            vm.PlanDeAccionModel.ListAcciones.push(vm.AccionesPlanModel);
                            vm.AccionesPlanModel = JSON.parse(JSON.stringify(_modelAccionesPlan));
                        });
                        console.log(vm.PlanDeAccionModel);                       
                    }
                    //se agregarón las acciones al modelo General
                    var iddiv = e.target.parentElement.attributes.idcat.value;
                    document.getElementById("HselecCat").style.display = "block";
                    document.getElementById("BselecCat").style.display = "block";
                    document.getElementById("cat_"+iddiv).style.display = "none";                                      
                    document.getElementById("loading").style.display = "none";
                });
                //regresa a la seccion de Arbol de Areas
                $("#section3").on("click", ".btn-quitaArea", function () {
                    document.getElementById("HselecCat").style.display = "none";
                    document.getElementById("BselecCat").style.display = "none";
                    document.getElementById("section2").style.display = "block";
                    document.getElementById("section3").style.display = "none";
                    var entidad = $("#listAcciones div");
                    entidad.css("display", "none");
                    //empty grid
                    vm.limpiaGridCategoriasPorArea();
                });
                $("#section2").on("click", ".btn-quitaBase", function () {                   
                    document.getElementById("section1").style.display = "block";
                    document.getElementById("section2").style.display = "none";
                    //empty grid
                    vm.limpiaGridCategoriasPorArea();
                });

                //Agrega responsable al listado
                $("#section3").on("click", ".add-responsable", function (e) {
                    var sidCat = e.target.attributes.idcat.value;
                    var sidAccion = e.target.attributes.idAccion.value;
                    var arg1 = "";
                    var arg2 = "";
                    var arg3 = "";
                    arg1 = "#collapse" + sidCat;
                    arg2 = ".reponsables_"+sidAccion;
                    arg3 = arg1 + " " + arg2;
                    $("#section3 "+arg2).append(`<div class ="row form-group">
                                                 <div class ="col-6"><input type="text" placeholder="Ingresa nombre del responsable" class ="form-control frm-respon" /></div>
                                                <div class ="col-6"><input type="text" placeholder="Ingresa correo electrónico" class ="form-control frm-respon-email" /></div>
                                            </div>`);
                   // console.log(e);
                });
                //guardar todo el plan de accion
                $("#page-title").on("click", ".btn-parallelogram", function (e) {
                    document.getElementById("loading").style.display = "block";
                    var validarEnvio = false;
                    vm.NombreEncuesta; vm.PeriodoEncuesta; vm.BaseEncuesta; vm.textAreaAgencia;
                    vm.IdRango; vm.IdCategoria;                   
                    vm.PlanDeAccionModel.Nombre = vm.NombreEncuesta + "_" + vm.PeriodoEncuesta + "_" + vm.BaseEncuesta + "_" + vm.textAreaAgencia;
                    vm.PlanDeAccionModel.Area = vm.textAreaAgencia;
                    vm.PlanDeAccionModel.IdEncuesta = vm.IdEncuesta;
                    vm.PlanDeAccionModel.IdBaseDeDatos = vm.IdBaseEncuesta;
                    vm.PlanDeAccionModel.AnioAplicacion = vm.PeriodoEncuesta;
                    //se localiza las acciones
                    var accionesLocalizadas = $("body").find("#accordion .card:visible");
                    //se valida por lo menos una acción
                    if (accionesLocalizadas.length == 0) {
                        swal("Debes agregar por lo menos una acción a la categoria", "", "info").then(function () {
                            document.getElementById("loading").style.display = "none";
                            return false;
                        });
                        document.getElementById("loading").style.display = "none";
                        return false;
                    }                   
                    //se valida los datos de las acciones
                    [].forEach.call(accionesLocalizadas, function (item) {
    var idAccionAltaV = item.getElementsByClassName("newIdAccion")[0].value;
    var idPeriodicidad = "frm-periodicidad" + idAccionAltaV;
    var NombreV = item.getElementsByClassName("txt-Nom-Acc")[0].value;
   // var PeriodicidadV = item.getElementsByClassName(idPeriodicidad)[0].value;
    var FechaInicioV = item.getElementsByClassName("frm-fecini")[0].value;
    var FechaFinV = item.getElementsByClassName("frm-fecfin")[0].value;
    var ObjetivoV = item.getElementsByClassName("frm-objetivo")[0].value;
    var MetaV = item.getElementsByClassName("frm-meta")[0].value;
    var ComentariosV = item.getElementsByClassName("frm-comentarios")[0].value;
    if (NombreV == "" || NombreV == "null") {
        swal("Debes agregar nombre a la acción", "", "info").then(function () {
            SetCampoInvalido(item.getElementsByClassName("txt-Nom-Acc")[0]);
            validarEnvio = false;
            return false;
        });
        validarEnvio = false;
        return false;
    } else { validarEnvio = true; }
    //if (PeriodicidadV == "") {
    //    swal("Debes agregar periodicidad a la acción", "", "info").then(function () {
    //        //SetCampoInvalido(item.getElementsByClassName("frm-periodicidad" + idAccionAltaV + "")[0]);
    //        validarEnvio = false;
    //        return false;
    //    });
    //    validarEnvio = false;
    //    return false;       
    //} else { validarEnvio = true; }
    if (FechaInicioV == "") {
        swal("Debes agregar una fecha inicio a la acción", "", "info").then(function () {
            SetCampoInvalido(item.getElementsByClassName("frm-fecini")[0]);
            validarEnvio = false;
            return false;
        });
        validarEnvio = false;
        return false;
    } else { validarEnvio = true; }
    if (FechaFinV == "") {
        swal("Debes agregar una fecha fin a la acción", "", "info").then(function () {
            SetCampoInvalido(item.getElementsByClassName("frm-fecfin")[0]);
            validarEnvio = false;
            return false;
        });
        validarEnvio = false;
        return false;
    } else { validarEnvio = true; }
    if (ObjetivoV == "") {
        swal("Debes agregar objetivo a la acción", "", "info").then(function () {
            SetCampoInvalido(item.getElementsByClassName("frm-objetivo")[0]);
            validarEnvio = false;
            return false;
        });
        validarEnvio = false;
        return false;
    } else { validarEnvio = true; }
    if (MetaV == "") {
        swal("Debes agregar meta a la acción", "", "info").then(function () {
            SetCampoInvalido(item.getElementsByClassName("frm-meta")[0]);
            validarEnvio = false;
            return false;
        });
        validarEnvio = false;
        return false;
    } else { validarEnvio = true; }
    if (ComentariosV == "") {
        swal("Debes agregar comentarios a la acción", "", "info").then(function () {
            SetCampoInvalido(item.getElementsByClassName("frm-comentarios")[0]);
            validarEnvio = false;
            return false;
        });
        validarEnvio = false;
        return false;
    } else { validarEnvio = true; }
    //se validan los responsables
    
    var lresponsablesV = $(".reponsables_" + idAccionAltaV + "").find(".form-group");    
    [].forEach.call(lresponsablesV, function (itemr) {
        var NombreVR = itemr.getElementsByClassName("frm-respon")[0].value;
        var EmailVR = itemr.getElementsByClassName("frm-respon-email")[0].value;       
        if (NombreVR == ""){
            swal("Debes agregar nombre de responsable", "", "info").then(function () {
                SetCampoInvalido(item.getElementsByClassName("frm-respon")[0]);
                validarEnvio = false;
                return false;
            });
            validarEnvio = false;
            return false;
        } else { validarEnvio = true; }
        if (EmailVR == "") {
            swal("Debes agregar email de responsable", "", "info").then(function () {
                SetCampoInvalido(item.getElementsByClassName("frm-respon-email")[0]);
                validarEnvio = false;
                return false;
            });
            validarEnvio = false;
            return false;
        } else { validarEnvio = true; }
        });
    
});
                    //Despues de validar que no tenga campos vacios
                    //por el numero de acciones que tenga el Plan de Accion                   
if (validarEnvio) {
    document.getElementById("loading").style.display = "block";
    [].forEach.call(accionesLocalizadas, function (item) {
        var idAccionAltaV = item.getElementsByClassName("newIdAccion")[0].value;
        var idPeriodicidad = "frm-periodicidad" + idAccionAltaV;
                        var idAccionAlta = item.getElementsByClassName("newIdAccion")[0].value;
                        vm.AccionesPlanModel.IdAccion = idAccionAlta;
                        vm.AccionesPlanModel.PlanDeAccion.Nombre = item.getElementsByClassName("txt-Nom-Acc")[0].value;
                        vm.AccionesPlanModel.Periodicidad = item.getElementsByClassName("frm-periodicidadS")[0].value;
                        vm.AccionesPlanModel.FechaInicio = item.getElementsByClassName("frm-fecini")[0].value;
                        vm.AccionesPlanModel.FechaFin = item.getElementsByClassName("frm-fecfin")[0].value;
                        vm.AccionesPlanModel.Objetivo = item.getElementsByClassName("frm-objetivo")[0].value;
                        vm.AccionesPlanModel.Meta = item.getElementsByClassName("frm-meta")[0].value;
                        vm.AccionesPlanModel.Comentarios = item.getElementsByClassName("frm-comentarios")[0].value;

                        var lresponsables = $(".reponsables_" + idAccionAlta + "").find(".form-group");
                        [].forEach.call(lresponsables, function (itemr) {
                            vm.ResponsablePlanModel.Responsable.Nombre = itemr.getElementsByClassName("frm-respon")[0].value;
                            vm.ResponsablePlanModel.Responsable.Email = itemr.getElementsByClassName("frm-respon-email")[0].value;
                            vm.AccionesPlanModel.ListadoResponsables.push(vm.ResponsablePlanModel);
                            vm.ResponsablePlanModel = JSON.parse(JSON.stringify(_modelResponsablePlan));
                        });
                        //vm.AccionesPlanModel.ListadoResponsables.push(AccionesPlanObj);
                        vm.PlanDeAccionModel.ListAcciones.push(vm.AccionesPlanModel);
                        vm.AccionesPlanModel = JSON.parse(JSON.stringify(_modelAccionesPlan));
                    });
                    console.log(vm.PlanDeAccionModel);
                    vm.post("/PlanesDeAccion/AddPlanDeAccion/", vm.PlanDeAccionModel, function (response) {
                        if (response.Correct) {
                            document.getElementById("loading").style.display = "none";
                            swal("El Plan de Acción ha sido agregado con éxito", "", "success");
                            setTimeout(function () {
                                window.location.href = "/PlanesDeAccion/Create";
                            }, 800);
                            
                        }
                        else {
                            document.getElementById("loading").style.display = "none";
                            swal("Ocurrio un problema con el Alta de Plan de Acción", response.ErrorMessage, "error");
                            setTimeout(function () {
                                window.location.href = "/PlanesDeAccion/Create";
                            }, 1800);
                        }
                    });
                }


                   
                });
                //elimina la accion del listado de acciones
                $("body").on("click", ".eliminaAccion", function (e) {
                    //e.target.parentNode.parentNode.parentNode.parentNode.remove();
                    e.target.closest(".card").remove();
                });
            });

            if (IdEncuesta > 0) {
                /*Obtiene las subcategorias configuradas en una encuesta, aqui todavia sin importar los promedios obtenidos*/
                vm.post("/PlanesDeAccion/GetSubCategoriasByIdEncuesta/", model, function (response) {
                    if (response.Correct) {
                        vm.PromedioSubCategorias = JSON.parse(response.Objects[0].JsonData);
                        vm.CategoriasAgrupadas = vm.AgruparCategorias(vm.PromedioSubCategorias);
                        vm.Categorias = vm.Execute(vm.CategoriasAgrupadas);
                        [].forEach.call(vm.Categorias, function (item) {
                            item.PromedioGeneral = Math.round(item.PromedioGeneral * 100) / 100;
                        });
                        [].forEach.call(vm.Categorias, function (categ) {
                            $("#nueva-categoria").append("<option value='" + categ.IdCategoria + "'>" + categ.Categoria + "</option>");
                        });
                        //vm.ObtenerRangos();
                    }
                    else {
                        swal("Ocurrió un error al intentar consultar las subcategorias", response.ErrorMessage, "error");
                    }
                });
            }
            vm.limpiaGridCategoriasPorArea = function () {
                if ($("#gridCategoriasPM").data("kendoGrid")) {
                    $("#gridCategoriasPM").data("kendoGrid").destroy();
                    $("#gridCategoriasPM").empty();
                }
            }

            vm.setIconoSVG = function (value) {
                try {
                    value = parseFloat(value);
                    if (value < 70.99) {
                        return vm.lluviaIcono;
                    }
                    if (value >= 70 && value < 80.99) {
                        return vm.nubeIcono;
                    }
                    if (value >= 80 && value < 90.99) {
                        return vm.solNubeIcono;
                    }
                    if (value >= 90 && value <= 100) {
                        return vm.solIcono;
                    }
                } catch (aE) {
                    return "";
                }
            }

            vm.ObtenerRangos = function () {
                vm.get("/PlanesDeAccion/GetRangos/", function (response) {
                    if (response.Correct) {                       
                        [].forEach.call(response.Objects, function (item) {
                            var separa = item.Descripcion.split(" - ");
                            separa[0] = parseFloat(separa[0]);
                            separa[1] = parseFloat(separa[1]);
                            separa[1] = separa[1] + .99;
                            vm.ListRangos.push({ Id: item.IdRango, Descripcion: item.Descripcion, valor1: separa[0], valor2:separa[1] });
                        });
                       // vm.ListRangos.unshift({ Id: 0, Descripcion: "- Asignar rango -" });
                        /* 
                         * Esperar a que el front se pinte con el repeat
                         */
                        //setTimeout(function () {
                        //    [].forEach.call(vm.ListRangos, function (item) {
                        //        $(".select-rango").append("<option value='" + item.Id + "'>" + item.Descripcion + "</option>")
                        //    });
                        //    $(".delete-accion").unbind();
                        //    $(".delete-accion").click(function (e) {
                        //        vm.EliminarAccion(e);
                        //    });
                        //}, 500);
                    }
                    else {
                        swal("Ocurrió un error al consultar los rangos", response.ErrorMessage, "error");
                    }
                });
            }

            vm.AgruparCategorias = function (DataSubCategorias) {
                /*DataSubCategorias*/
                var Categorias = Enumerable.from(DataSubCategorias).distinct(o => o.IdPadre).toList();
                [].forEach.call(Categorias, function (item) {
                    var itemCategoria = Enumerable.from(DataSubCategorias).where(o => o.IdPadre == item.IdPadre).toList();
                    vm.CategoriasAgrupadas.push(itemCategoria);
                });
                return vm.CategoriasAgrupadas;
            }

            vm.Execute = function (DataCategorias) {
                var auxCategorias = Array();
                [].forEach.call(DataCategorias, function (subcategoria) {
                    var sumatoria = 0;
                    [].forEach.call(subcategoria, function (item) {
                        sumatoria += item.Promedio;
                    });
                    auxCategorias.push({ IdCategoria: subcategoria[0].IdPadre, Categoria: subcategoria[0].NombrePadreCategoria, PromedioGeneral: (sumatoria / subcategoria.length) });
                });
                return auxCategorias;
            }
            vm.ExecutePA = function (DataCategorias) {
                var auxCategorias = Array();
                [].forEach.call(DataCategorias, function (subcategoria) {
                    var sumatoria = 0;
                    [].forEach.call(subcategoria, function (item) {
                        sumatoria += item.Promedio;
                    });
                    auxCategorias.push({ IdCategoria: subcategoria[0].IdPadre, Categoria: subcategoria[0].NombrePadreCategoria, PromedioGeneral: (sumatoria / subcategoria.length), IdRango:0,Rango:"" });
                });
                return auxCategorias;
            }

            function messageBoxError(data) {
                var error;
                switch (data.status) {
                    case 200:
                        return false;
                    case 400:
                        alert("400 - Bad Request");
                        error = true;
                        break;
                    case 401:
                        alert("401 - Unauthorized");
                        error = true;
                        break;
                    case 403:
                        alert("403 - Forbidden");
                        error = true;
                        break;
                    case 404:
                        alert("404 - Not Found");
                        error = true;
                        break;
                    case 408:
                        alert("408 - Request TimeOut");
                        error = true;
                        break;
                    case 500:
                        alert("500 - Internal Server Error");
                        error = true;
                        break;
                    case 502:
                        alert("502 - Bad Gateway");
                        error = true;
                        break;
                    case 503:
                        alert("503 - Service Unavailable");
                        error = true;
                        break;
                    default:
                        alert("messageBoxError: " + data.message);
                        error = true;
                }
                if (error) {
                    $("#mergeError").append(data.data);
                    $(".divError").show();
                    $('#formular').modal('toggle');
                }
                return error;
            }

            vm.GuardarPlanDeAccion = function () {
                vm.get("/PlanesDeAccion/AddPlan/?Nombre=" + document.getElementById("NombrePlanAccion").value, function (response) {
                    if (response.Correct) {
                        vm.PlanDeAccion.Id = response.NewId;
                        swal("El plan de acción se agregó correctamente", "", "success").then(function () {
                            /*Mostrar configurador de acciones*/
                            document.getElementById("accordion").classList.remove("ng-hide");
                        });
                    }
                    else {
                        swal("Ocurrió un error al intentar guardar el plan de acción", response.ErrorMessage, "error");
                    }
                });
            }

            vm.AgregarNuevaAccion = function (IdCategoria) {
                $("#merge-new-acciones-idcat-" + IdCategoria).append(NuevaAccionHtmlContent);
                [].forEach.call($("#merge-new-acciones-idcat-" + IdCategoria + " .select-rango"), function (select) {
                    if (select.options.length == 0) {
                        [].forEach.call(vm.ListRangos, function (item) {
                            select.options.add(new Option(item.Descripcion, item.Id, false, false));
                        });
                    }
                });
                $(".delete-accion").unbind();
                $(".delete-accion").click(function (e) {
                    vm.EliminarAccion(e);
                });
            }
            vm.GuardaPlanDeAccion = function (modelPlanDeAccion) {

            }
            vm.GuardarAccion = function (event, message) {
                var primeraseccion = event.ownerDocument.children[0].children[1].getElementsByClassName("section1");
               
                var IdAccion = 0;
                modelNuevaAccion = {
                    Descripcion: Accion,
                    Rango: { IdRango: Rango },
                    Estatus: { IdEstatus: 1 },
                    Categoria: { IdCategoria: IdCategoria },
                    Encuesta: { IdEncuesta: IdEncuesta },
                    BasesDeDatos: { IdBaseDeDatos: IdBaseDeDatos },
                    AnioAplicacion: AnioAplicacion,
                    IdAccionDeMejora: IdAccion
                }
                vm.post("/PlanesDeAccion/AddAccion/", modelNuevaAccion, function (response) {
                    if (response.Correct) {
                        event.target.closest(".form-group").getElementsByClassName("fas fa-save")[0].style.color = "#28a745";
                        /*Asignar a la accion de mejora Su Id de Accion que debe retornarse en la peticion*/
                        event.target.closest(".form-group").setAttribute("IdAccion", response.NewId);
                        if (IdAccion == 0 && message != 0)
                            swal("La acción ha sido agregada con éxito", "", "success");
                        if (IdAccion > 0)
                            swal("La acción ha sido actualizada con éxito", "", "success");
                        event.target.closest(".form-group").getElementsByTagName("input")[0].setAttribute("disabled", "");
                        event.target.closest(".form-group").getElementsByTagName("select")[0].setAttribute("disabled", "");
                    }
                    else {
                        if (IdAccion == 0)
                            swal("Ocurrió un error al intentar guardar la acción", response.ErrorMessage, "error");
                        if (IdAccion > 0)
                            swal("Ocurrió un error al intentar actualizar la acción", response.ErrorMessage, "error");
                    }
                });
            }

            vm.EliminarAccion = function (e) {
                var parent = e.target.closest(".form-group");
                if (parent.attributes.IdAccion == null) {
                    /*Solo se elimina del dom*/
                    swal({
                        title: "¿Estás seguro de que deseas eliminar la acción de mejora?",
                        text: "",
                        icon: "info",
                        buttons: [
                            'No',
                            'Si'
                        ],
                        dangerMode: false,
                        allowOutsideClick: false,
                        closeOnClickOutside: false,
                    }).then(function (isConfirm) {
                        if (isConfirm)
                            e.target.closest(".form-group").remove();
                    });
                }
                else {
                    if (parent.attributes.IdAccion.value > 0) {
                        /*Se elimina del dom pero tambien de la base de datos (Baja lógica)*/
                        vm.get("/PlanesDeAccion/DeleteAccion/?IdAccion=" + parent.attributes.IdAccion.value, function (response) {
                            if (response.Correct) {
                                swal("La acción ha sido eliminada con éxito", "", "success").then(function () {
                                    e.target.closest(".form-group").remove();
                                });
                            }
                            else {
                                swal("Ocurrió un error al intentar eliminar la acción", response.ErrorMessage, "error");
                            }
                        });
                    }
                }
            }

            vm.GuardaAccionAyuda = function (e) {
                console.log(e);
                var IdAccion = e.target.closest("#edita-acciones").getElementsByTagName("input")[0].value;
                var Descripcion = e.target.closest("#edita-acciones").getElementsByTagName("input")[1].value;
                if (Descripcion == "") {
                    swal("Debes describir la acción de mejora", "", "info").then(function () {
                        return false;
                    });
                    return false;
                }
                /* Peticion para editar la accion */
                modelNuevaAccion = {
                    Descripcion: Descripcion,
                    Estatus: { IdEstatus: 1 },
                    Encuesta: { IdEncuesta: 0 },
                    BasesDeDatos: { IdBaseDeDatos: 0 },
                    AnioAplicacion: 0,
                    IdAccionDeMejora: IdAccion
                }
                vm.post("/PlanesDeAccion/AddAccion/", modelNuevaAccion, function (response) {
                    if (response.Correct) {
                        if (IdAccion == 0 || IdAccion == "")
                            swal("La acción ha sido agregada con éxito", "", "success").then(function () {
                                vm.ConsultaAccionesGuardadas();
                            });
                        if (IdAccion > 0)
                            swal("La acción ha sido actualizada con éxito", "", "success").then(function () {
                                vm.ConsultaAccionesGuardadas();
                            });
                        LimpiarModal();
                    }
                    else {
                        if (IdAccion == 0)
                            swal("Ocurrió un error al intentar guardar la acción", response.ErrorMessage, "error");
                        if (IdAccion > 0)
                            swal("Ocurrió un error al intentar actualizar la acción", response.ErrorMessage, "error");
                    }
                });
            }

            vm.GuardarPromediosCategorias = function (dataCategorias) {
                document.getElementById("loading").style.display = "block";
                var ListModel = [];
                [].forEach.call(dataCategorias, function (categoria) {
                    let model = {
                        Area: vm.textAreaAgencia,
                        BasesDeDatos: { IdBaseDeDatos: vm.IdBaseEncuesta },
                        Categoria: { IdCategoria: categoria.IdCategoria },
                        Encuesta: { IdEncuesta: vm.IdEncuesta },
                        Promedio: categoria.PromedioGeneral
                    };
                    ListModel.push(model);
                });
                vm.post("/PlanesDeAccion/GuardarPromediosPorCategoria/", ListModel, function (response) {
                    if (response.Correct) {
                        document.getElementById("loading").style.display = "none";
                        console.log("GuardarPromediosPorCategoria exitoso");
                    }
                    else {
                        document.getElementById("loading").style.display = "none";
                        swal("Ocurrió un error en el proceso", response.ErrorMessage, "error");
                    }
                });
            }

            vm.ObtenerPeriodicidad = function () {
                vm.get("/PlanesDeAccion/GetPeriodicidad/", function (response) {
                    if (response.Correct) {
                        console.log(response.Objects);
                        [].forEach.call(response.Objects, function (item) {
                            vm.ListPeriodicidad.push({Id: item.IdPeriodicidad, Descripcion: item.Descripcion});
                        });
                        //vm.ListPeriodicidad = response.Objects;
                    }
                    
                });

            }

            vm.AddAccionPlan = function (idCat,idTipo) {
                // da de alta la accion en BD
                modelNuevaAccion = {
                    Descripcion: "",
                    Rango: { IdRango: vm.IdRango },
                    Estatus: { IdEstatus: 1 },
                    Categoria: { IdCategoria: idCat },
                    Encuesta: { IdEncuesta: vm.IdEncuesta },
                    BasesDeDatos: { IdBaseDeDatos: IdBaseDeDatos },
                    Tipo:idTipo,
                    AnioAplicacion: vm.PeriodoEncuesta
                }
                //se oculta el aviso de no cuenta con acciones
                var numNocuenta = $("#accordion .notieneAcc").length;
                if (numNocuenta == 1) {
                    //indica que tiene aviso de No cuenta con Acciones asignadas
                    $("#cat_" + idCat + " #accordion").empty()
                }
                var numAcciones = $("#cat_"+idCat+" #accordion .card:visible").length;
                if (numAcciones == 0) {
                    numAcciones = 1;
                }
                else {
                    numAcciones = numAcciones + 1
                }
                vm.post("/PlanesDeAccion/AddAccion/", modelNuevaAccion, function (response) {
                    if (response.Correct) {
                        $("#cat_" + idCat + " #accordion").append(`
                                <div class ="card">
	                               <div class ="card-header" id= "heading`+ response.NewId + `_` + numAcciones + `" >
		                               <h5 class ="mb-0">
			                               <span class ="btn btn-link col-10" data-toggle="collapse" data-target= "#collapse`+ response.NewId + `_` + numAcciones + `" aria-expanded="true" aria-controls= "collapse` + response.NewId + `_` + numAcciones + `" >
				                              <input type="text" placeholder="Ingresa el nombre de la acción" class ="form-control txt-Nom-Acc" />
			                               </span>
                                           <span class ="col-2">
                                           <i class ="far fa-times-circle eliminaAccion" style="cursor: pointer;"></i>
                                           </span>
		                               </h5>
	                               </div>

	                               <div id= "collapse`+ response.NewId + `_` + numAcciones + `" class ="collapse" aria-labelledby= "heading` + response.NewId + `_` + numAcciones + `" data-parent="#accordion">
		                               <div class ="card-body">
			                               <div class ="row form-group">
                                                <div class ="col-3">Periodicidad: </div>
                                                <div class ="col-5">
                                                <input type="hidden" placeholder="Ingresa periocidad" class ="form-control frm-periodicidad" />
                                                <select class = "form-control frm-periodicidadS select-periodicidad`+ response.NewId + `" style="width: 100%;">
                                                </select>
                                                </div>
                                                <div class ="col-2"><i class ="far fa-calendar-alt"></i>Inicia:<input type="date" placeholder="Ingresa fecha de inicio" class ="form-control frm-fecini" /></div>
                                                <div class ="col-2"><i class ="far fa-calendar-alt"></i>Concluya:<input type="date" placeholder="Ingresa fecha de término" class ="form-control frm-fecfin" /></div>
                                            </div>
                                            <div class ="row form-group">
                                                <div class ="col-9">Objetivo: </div>
                                                <div class ="col-3">Meta: </div>
                                            </div>
                                            <div class ="row form-group">
                                                <div  class ="col-9"><input type="text" placeholder="Objetivo a alcanzar con la acción" class ="form-control frm-objetivo" /></div>
                                                <div  class ="col-3"><input type="text" placeholder="Ingresa meta" class ="form-control frm-meta" /></div>
                                            </div>
                                            <div class ="row form-group">
                                                <div class ="col-6"><i class = "fas fa-plus add-responsable" idAccion= "`+ response.NewId + `" idcat= "` + idCat + `" ></i> Responsable: </div>
                                                <div class ="col-6">Email: </div>
                                            </div>
                                            <section class = "reponsables_`+ response.NewId + ` frm-responsables" >
                                            <div class ="row form-group">
                                                <div class ="col-6"><input type="text" placeholder="Ingresa nombre del responsable" class ="form-control frm-respon" /></div>
                                                <div class ="col-6"><input type="text" placeholder="Ingresa correo electrónico" class ="form-control frm-respon-email" /></div>
                                            </div>
                                            </section>
                                            <div class ="row form-group">
                                                <div class ="col-3">Comentarios: </div>
                                                <div class ="col-9"><input type="text" placeholder="Ingresa comentarios" class ="form-control frm-comentarios" /></div>
                                            </div>
                                            <input type="hidden" class ="newIdAccion" value= "`+ response.NewId + `" />
                                            <input type="hidden" class ="idCat" value= "`+ idCat + `" />
		                               </div>
	                               </div>
                               </div>
                                `
                            );
                        //se agrega el select de Periodiicidad
                        var idselectAccion = ".select-periodicidad" + response.NewId;
                        [].forEach.call(vm.ListPeriodicidad, function (item) {
                            $(idselectAccion).append("<option value='" + item.Id + "'>" + item.Descripcion + "</option>");
                        });
                        //asigna el id creado de accion en el cabezero del accordion insertado
                        $("#heading" + response.NewId + "_" + numAcciones + " .txt-Nom-Acc")[0].setAttribute("IdAccion", "" + response.NewId + "")
                        if (IdAccion == 0)
                            swal("La acción ha sido agregada con éxito", "", "success");
                        if (IdAccion > 0)
                            swal("La acción ha sido actualizada con éxito", "", "success");
                    }
                    else {
                        if (IdAccion == 0)
                            swal("Ocurrió un error al intentar guardar la acción", response.ErrorMessage, "error");
                        if (IdAccion > 0)
                            swal("Ocurrió un error al intentar actualizar la acción", response.ErrorMessage, "error");
                    }
                });
            }
        } catch (aE) {
            alert(aE.message);
        }
    }
})();

var NuevaAccionHtmlContent =
    `<div class="form-group">
        <div class="form-inline">
            <div class="col-8">
                <input type="text" class="form-control" style="width: 100%;" placeholder="Acción de mejora" />
            </div>
            <div class="col-3">
                <select class="form-control select-rango" style="width: 100%;"></select>
            </div>
            <div class="col-1">
                <button class="btn delete-accion"><i class="fas fa-remove"></i></button>
                <button class="btn save-action" onclick="GuardarAccion(this)"><i class="fas fa-save"></i></button>
            </div>
        </div>
    </div>`;

var GuardarAccion = function (event) {
    var primeraseccion = event.ownerDocument.children[0].children[1].getElementsByClassName("section1");
    var segundaseccion = event.ownerDocument.children[0].children[1].getElementsByClassName("sectionActions");
    var obten1 = primeraseccion[0].getElementsByClassName("ng-binding");
    [].forEach.call(obten1, function (item1) {

    });

    var Accion = event.closest(".form-group").getElementsByTagName("input")[0].value;
    var Rango = event.closest(".form-group").getElementsByTagName("select")[0].value;
    var IdCategoria = event.closest(".card-body").attributes.idCategoria.value;
    if (IsNullOrEmpty(Accion) || Rango == "0") {
        swal("Debes describir la acción de mejora y asignarle un rango", "", "info").then(function () {
            return false;
        });
        SetCampoInvalido(event.closest(".form-group").getElementsByTagName("input")[0]);
        SetCampoInvalido(event.closest(".form-group").getElementsByTagName("select")[0]);
        return false;
    }
    SetCampoValido(event.closest(".form-group").getElementsByTagName("input")[0]);
    SetCampoValido(event.closest(".form-group").getElementsByTagName("select")[0]);
    var IdAccion = event.closest(".form-group").attributes.IdAccion == null ? 0 : event.closest(".form-group").attributes.IdAccion.value;

    modelNuevaAccion = {
        Descripcion: Accion,
        Rango: { IdRango: Rango },
        Estatus: { IdEstatus: 1 },
        Categoria: { IdCategoria: IdCategoria },
        Encuesta: { IdEncuesta: IdEncuesta },
        BasesDeDatos: { IdBaseDeDatos: IdBaseDeDatos },
        AnioAplicacion: AnioAplicacion,
        IdAccionDeMejora: IdAccion
    }
    $.ajax({
        url: "/PlanesDeAccion/AddAccion/",
        type: "POST",
        data: modelNuevaAccion,
        success: function (response) {
            if (response.Correct) {
                event.closest(".form-group").getElementsByClassName("fas fa-save")[0].style.color = "#28a745";
                /*Asignar a la accion de mejora Su Id de Accion que debe retornarse en la peticion*/
                event.closest(".form-group").setAttribute("IdAccion", response.NewId);
                if (IdAccion == 0)
                    swal("La acción ha sido agregada con éxito", "", "success");
                if (IdAccion > 0)
                    swal("La acción ha sido actualizada con éxito", "", "success");
                event.closest(".form-group").getElementsByTagName("input")[0].setAttribute("disabled", "");
                event.closest(".form-group").getElementsByTagName("select")[0].setAttribute("disabled", "");
            }
            else {
                if (IdAccion == 0)
                    swal("Ocurrió un error al intentar guardar la acción", response.ErrorMessage, "error");
                if (IdAccion > 0)
                    swal("Ocurrió un error al intentar actualizar la acción", response.ErrorMessage, "error");
            }
        },
        error: function (err) {
            if (err.status == 404) {
                alert("404 - Recurso no encontrado");
            }
            else {
                alert(err.status);
            }
        }
    });
}

var IsNullOrEmpty = function (data) {
    var isNull = false;
    if (data == null || data == undefined)
        isNull = true;
    switch (typeof (data)) {
        case "string":
            if (data == "") {
                isNull = true;
            }
            break;
        case "object":
            if (data.length == 0) {
                isNull = true;
            }
            break;
    }
    return isNull;
}

var SetCampoInvalido = function (DomElem) {
    DomElem.classList.remove("is-valid");
    DomElem.classList.add("is-invalid");
}

var SetCampoValido = function (DomElem) {
    DomElem.classList.remove("is-invalid");
    DomElem.classList.add("is-valid");
}

var BuscaAcciones = function (filtro) {
    $('*:contains(' + filtro + ')').each(function () {
        if ($(this).children().length < 1)
            $(this).css("border", "solid 2px red")
    });
}

var CrearScript = function (src) {
    var script = document.createElement('script');
    script.src = src;
    document.head.appendChild(script);
}

var CrearGridSubCategorias = function () {
    setTimeout(function () {
        if (AccionesPreGuardadas.length == 0) {
            swal("No se encontrarón acciones guardadas", "", "info");
        }
        else {
            $("#grid-Acciones").kendoGrid({
                dataSource: AccionesPreGuardadas,
                columnMenu: {
                    filterable: false
                },
                height: 500,
                editable: "",
                pageable: true,
                sortable: true,
                navigatable: true,
                resizable: true,
                reorderable: true,
                columns: [
                    {
                        field: "IdAccionDeMejora",
                        title: "IdAccionDeMejora",
                        width: 50,
                        editable: false
                    }, {
                        field: "Descripcion",
                        title: "Descripcion",
                        width: 150,
                    }, {
                        field: "Editar",
                        title: "Editar",
                        width: 80,
                        template: "<button class='btn btn-info'>Editar</button>"
                    }
                ],
                batch: true,
                pageSize: 20,
                autoSync: true,
                schema: {
                    model: {
                        id: "IdAccionDeMejora",
                        fields: {
                            IdAccionDeMejora: { editable: false },
                            Descripcion: { type: "", editable: true },
                            IdCategoria: { type: "button", editable: true },
                        }
                    }
                }
            });
        }
    }, 500);
}

var LimpiarModal = function () {
    if (IdEncuesta > 0) {
        $('#re-asignar-accion').modal('toggle');
        $("#nueva-categoria").val("0");
        $("#nuevo-rango").val("0");
    }
    if (IdEncuesta == 0) {
        $('#edita-acciones').modal('toggle');
        $("#edita-acciones input")[0].value = "";
        $("#edita-acciones input")[1].value = "";
    }
}

var editAccion = function (e) {
    console.log(e);
    e.closest(".form-group").getElementsByTagName("input")[0].removeAttribute("disabled");
    e.closest(".form-group").getElementsByTagName("select")[0].removeAttribute("disabled");
}

//var EditAccionAyuda = function (e, accion) {
//    console.log(e);
//    $("#edita-acciones .select-rango").empty();
//    [].forEach.call(listRangos, function (rango) {
//        $("#edita-acciones .select-rango").append('<option>' + rango.Descripcion + '</option>');
//    });
//    if (e.id == "nueva-accion") {
//        $("#edita-acciones").modal('toggle');
//    }
//    else {
//        var IdAccion = e.closest(".k-master-row").getElementsByTagName("td")[0].innerText;
//        var Descripcion = e.closest(".k-master-row").getElementsByTagName("td")[1].innerText;
//        $("#edita-acciones").modal('toggle');
//        $("#edita-acciones input")[0].value = IdAccion;
//        $("#edita-acciones input")[1].value = Descripcion;
//    }
//    if (accion == "agregar") {
//        $("#edita-acciones .header-modal")[0].innerText = "Agregar nueva acción"
//    }
//    else {
//        $("#edita-acciones .header-modal")[0].innerText = "Editar acción";
//    }
//}