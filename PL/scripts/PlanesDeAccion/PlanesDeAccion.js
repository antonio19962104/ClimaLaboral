/*
 * Script del Modulo de Planes de Accion
 * 07/10/2021
 */
var AccionesPreGuardadas = [];
var IdAccion_ReAsignar = 0;
var model = {
    "AnioAplicacion": AnioAplicacion,
    "AreaAgencia": "AUT - ELE - AEP",
    "IdBaseDeDatos": IdBaseDeDatos,
    "IdEncuesta": IdEncuesta,
    "IdPregunta": 0
};
var modelNuevaAccion = {
    Descripcion: "",
    Rango: { IdRango: 0 },
    Estatus: { IdEstatus: 1 },
    Categoria: { IdCategoria: 0 },
    Encuesta: { IdEncuesta: IdEncuesta },
    BasesDeDatos: { IdBaseDeDatos: IdBaseDeDatos },
    AnioAplicacion: AnioAplicacion
};

(function () {
    "use strict"
    angular.module("app", [])
        .controller("planAccionController", planAccionController);

    function planAccionController($http, $scope) {
        try {
            var vm = this;
            vm.Modulo = "Configuración de Acciones de Mejora";
            vm.PlanDeAccion = Object;
            vm.EstructuraAFM = [];
            vm.PlanDeAccion.Id = 0;
            vm.PromedioSubCategorias = [];
            vm.CategoriasAgrupadas = [];
            vm.Categorias = [];
            vm.ListRangos = [];
            vm.BusquedaAcciones = [];
           
            $(document).ready(function () {
                $(".filter").on("keyup", function (text) {
                    document.getElementById("mergeBusqueda").innerHTML = "";
                    var texto = text.target.value;
                    var input = texto.toUpperCase();
                    var todosCard = $("#accordion input[type=text]");
                    [].forEach.call(todosCard, function (elem) {
                        var dataString = elem.value;
                        dataString = dataString.toUpperCase();
                        var existe = false;
                        existe = dataString.includes(input);
                        if (input != "") {
                            if (existe == true) {
                                document.getElementById("mergeBusqueda").innerHTML += "<div class='alert alert-primary'>" + dataString + "</div>";
                            }
                        }
                    });
                });
                //vm.ConsultaAreas();// Esta seccion se omite del alta de acciones ya que este conjunto es de uso genérico por encuesta
                vm.ConsultaAccionesGuardadas();
            });

            vm.ConsultaAreas = function () {
                vm.get("/PlanesDeAccion/GetAreasForPlanAccion/?IdBaseDeDatos=" + IdBaseDeDatos, function (response) {
                    if (response.Correct) {
                        [].forEach.call(response.Objects[0], function (item) {
                            vm.EstructuraAFM.push({ type: item.substring(0, 6), value: item.substring(6, (item.length)) });
                        });
                        var treeObject = vm.GenerarArbol();
                        var tw = new TreeView(
                            treeObject,
                            { showAlwaysCheckBox: true, fold: false });
                        document.getElementById("tree-view").appendChild(tw.root);
                        $("#tree-view p.group").css("display", "none");
                        $("#tree-view p.group:first").css("display", "");
                    }
                    else {
                        swal("Ocurrió un error al consultar las áreas", response.ErrorMessage, "error");
                    }
                });
                document.getElementById("accordion").classList.add("ng-hide");
            }

            vm.ConsultaAccionesGuardadas = function () {
                vm.post("/PlanesDeAccion/GetAcciones/?", modelNuevaAccion, function (response) {
                    if (response.Correct) {
                        $("#accordion .card-body").empty();
                        console.log(response.Objects);
                        AccionesPreGuardadas = response.Objects;
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
                                <input type="text" class="form-control" style="width: 95%;" placeholder="Acción de mejora">
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
                        text: item.value,
                        checked: false,
                        id: index,
                        children: [
                            /*Division*/
                        ]
                    });
                    /*Push Division*/
                    var Division = Enumerable.from(vm.EstructuraAFM).where(o => o.type == "Comp=>" && o.IdUnidadNegocio == item.IdUnidadNegocio).toList();
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
                    });
                });
                return treeObject;
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

            vm.post("/PlanesDeAccion/GetPromediosSubCategorias/", model, function (response) {
                if (response.Correct) {
                    vm.PromedioSubCategorias = JSON.parse(response.Objects[0].JsonData);
                    vm.CategoriasAgrupadas = vm.AgruparCategorias(vm.PromedioSubCategorias);
                    vm.Categorias = vm.Execute(vm.CategoriasAgrupadas);
                    [].forEach.call(vm.Categorias, function (categ) {
                        $("#nueva-categoria").append("<option value='" + categ.IdCategoria + "'>" + categ.Categoria + "</option>");
                    });
                    vm.ObtenerRangos();
                }
                else {
                    swal("Ocurrió un error al intentar consultar las subcategorias", response.ErrorMessage, "error");
                }
            });

            vm.ObtenerRangos = function () {
                vm.get("/PlanesDeAccion/GetRangos/", function (response) {
                    if (response.Correct) {
                        vm.ListRangos = [];
                        [].forEach.call(response.Objects, function (item) {
                            vm.ListRangos.push({ Id: item.IdRango, Descripcion: item.Descripcion });
                        });
                        vm.ListRangos.unshift({ Id: 0, Descripcion: "- Asignar rango -" });
                        /* 
                         * Esperar a que el front se pinte con el repeat
                         */
                        setTimeout(function () {
                            [].forEach.call(vm.ListRangos, function (item) {
                                $(".select-rango").append("<option value='" + item.Id + "'>" + item.Descripcion + "</option>")
                            });
                            $(".delete-accion").unbind();
                            $(".delete-accion").click(function (e) {
                                vm.EliminarAccion(e);
                            });
                        }, 500);
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
                        alert(data.statusText);
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

            vm.GuardarAccion = function (event) {
                var Accion = event.target.closest(".form-group").getElementsByTagName("input")[0].value;
                var Rango = event.target.closest(".form-group").getElementsByTagName("select")[0].value;
                var IdCategoria = event.target.closest(".card-body").attributes.idCategoria.value;
                if (IsNullOrEmpty(Accion) || Rango == "0") {
                    swal("Debes describir la acción de mejora y asignarle un rango", "", "info").then(function () {
                        return false;
                    });
                    SetCampoInvalido(event.target.closest(".form-group").getElementsByTagName("input")[0]);
                    SetCampoInvalido(event.target.closest(".form-group").getElementsByTagName("select")[0]);
                    return false;
                }
                SetCampoValido(event.target.closest(".form-group").getElementsByTagName("input")[0]);
                SetCampoValido(event.target.closest(".form-group").getElementsByTagName("select")[0]);
                var IdAccion = event.target.closest(".form-group").attributes.IdAccion == null ? 0 : event.target.closest(".form-group").attributes.IdAccion.value;
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
                        if (IdAccion == 0)
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
    switch (typeof(data)) {
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
    $("#grid").kendoGrid({
        dataSource: DataSubCategorias,
        columnMenu: {
            filterable: false
        },
        height: 680,
        editable: "incell",
        pageable: true,
        sortable: true,
        navigatable: true,
        resizable: true,
        reorderable: true,
        groupable: true,
        filterable: true,
        toolbar: ["excel", "pdf", "search"],
        columns: [
            {
                field: "IdCategoria",
                title: "IdCategoria",
                width: 300
            }, {
                field: "Nombre",
                title: "Nombre",
                width: 130,
            }, {
                field: "IdPadre",
                title: "IdPadre",
                width: 125
            }, {
                field: "NombrePadreCategoria",
                title: "NombrePadreCategoria",
                width: 125
            }, {
                field: "Promedio",
                title: "Promedio",
                width: 140
            }],
        batch: true,
        pageSize: 20,
        autoSync: true,
        schema: {
            model: {
                id: "IdCategoria",
                fields: {
                    IdCategoria: { editable: false, nullable: true },
                    Nombre: { type: "", editable: false },
                    IdPadre: { type: "number", editable: false },
                    Promedio: { type: "" },
                }
            }
        }
    });
}

var LimpiarModal = function () {
    $('#re-asignar-accion').modal('toggle');
    $("#nueva-categoria").val("0");
    $("#nuevo-rango").val("0");
}

var editAccion = function (e) {
    console.log(e);
    e.closest(".form-group").getElementsByTagName("input")[0].removeAttribute("disabled");
    e.closest(".form-group").getElementsByTagName("select")[0].removeAttribute("disabled");
}