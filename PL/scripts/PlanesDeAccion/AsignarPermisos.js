var Estructura;
var AreaSeleccionada;
(function () {
    "use strict"
    angular.module("app", []).controller("permisosController", permisosController);

    function permisosController($http, $scope) {
        try {
            var vm = this;
            vm.Modulo = "Configura permisos";
            vm.idDB = 0;
            vm.direccionS = "";
            vm.unidadS = "";           
            $(document).ready(function () {
                document.getElementById("loading").style.display = "block";
                vm.get("/PlanesDeAccion/ArbolEstructuraModuloPermisosPlanes/", function (response) {
                    document.getElementById("loading").style.display = "none";
                    if (response.Correct) {
                        //console.log(response.Objects[0]);
                        Estructura = response.Objects[0];
                        vm.ListPlanesDeAccion = response.Objects[0];
                        console.log(vm.ListPlanesDeAccion);
                        setTimeout(function () {
                            var treeObject = vm.ListPlanesDeAccion;
                            var tw = new TreeView(
                                treeObject,
                                { showAlwaysCheckBox: true, fold: false });
                            $("#tree-view").empty();
                            document.getElementById("tree-view").appendChild(tw.root);
                        }, 800);
                        
                    }
                    else {
                        swal("");
                    }
                });
            });

            vm.ObtenerAdmins = function (e) {
               // var area = e.target.value;
                AreaSeleccionada = e;
                document.getElementById("loading").style.display = "block";
                vm.get("/PlanesDeAccion/ObtenerAdmins/?Area=" +AreaSeleccionada, function (response) {
                    document.getElementById("loading").style.display = "none";
                    if (response.Correct) {
                        // Si el atributo Selected viene en true el check del admin debe colocarse como seleccionado
                        vm.ListAdmin = response.Objects;
                        vm.ListAdmin.sort(function (a, b) {
                            var a1 = a.Nombre, b1 = b.Nombre;
                            if (a1 == b1) return 0;
                            return a1 > b1 ? 1 : -1;
                        });
                        //se pinta el grid Kendo
                        $.getScript("http://demo.climalaboral.divisionautomotriz.com/scripts/kendo-all.js", function () {
                            $("#gridListAdmin").kendoGrid({
                                dataSource: {
                                    transport: {
                                        read: function (e) {
                                            e.success(vm.ListAdmin);
                                        }
                                    },
                                    batch: true,
                                    schema: {
                                        model: {
                                            fields: {
                                                IdAdministrador: { editable: false },
                                                Nombre: { editable: false },
                                                Selected: { type: "boolean" },
                                            }
                                        }
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
                                filterable: true,
                                columnMenu: false,
                                pageable: {
                                    refresh: false,
                                    pageSize: 10,
                                    pageSizes: [5, 10, 20, 50, 100]
                                },
                                //height : 300, 
                                columns: [
                                {
                                    field: "IdAdministrador",
                                    title: "ID",
                                    headerAttributes: { style: "text-align: left; vertical-align: middle; white-space: pre-wrap;" },
                                    editable: false
                                }, {
                                    field: "Nombre",
                                    title: "Nombre",
                                    headerAttributes: { style: "text-align: left; vertical-align: middle; white-space: pre-wrap;" },
                                    //attributes: { class: "text-center" },
                                },
                                {
                                    field: "Selected",
                                    title: "Selecciona",
                                    editable: false,
                                    headerAttributes: { style: "text-align: center; vertical-align: middle; white-space: pre-wrap;" },
                                    attributes: { class: "text-center" },
                                    template: '<input type="checkbox" #= Selected ? \'checked="checked"\' : "" # class="chkbx-Admin" />',
                                    width: 110,
                                    //template: {"#=dirtyField(data,'Selected')#<input type='checkbox' #= Selected ? \'checked='checked'\' : ''# class='chkbx k-checkbox' />", width: 110}
                                },
                                ],
                                editable: false,

                            })
                        });
                       
                    }
                    else {
                        swal("");
                    }
                });
            }

            $("#gridListAdmin").on("click", "input.chkbx-Admin", function (e) {
                debugger

                var grid = $("#gridListAdmin").data("kendoGrid");
                var adminSelected = grid.dataItem($(e.target).closest("tr"));

                if (this.checked) {
                    Enumerable.from(vm.ListAdmin).where('$.IdAdministrador =="' + adminSelected.IdAdministrador + '"').firstOrDefault().Selected = true;
                } else {
                    Enumerable.from(vm.ListAdmin).where('$.IdAdministrador =="' + adminSelected.IdAdministrador + '"').firstOrDefault().Selected = false;
                }                
                $('#gridListAdmin').data('kendoGrid').dataSource.read();
                $('#gridListAdmin').data('kendoGrid').refresh();


            });
            vm.ObtenerAdminElegidos = function () {
                var elegidos = [];
                //var chkList = document.getElementsByClassName("divAdmins")[0].getElementsByTagName("input");
                [].forEach.call(vm.ListAdmin, function (chk) {
                    if (chk.Selected)
                        elegidos.push(chk.IdAdministrador);
                });
                console.log(AreaSeleccionada);
                console.log(elegidos);
                vm.AgregarPermisos(AreaSeleccionada, elegidos);
            }

            vm.AgregarPermisos = function (area, elegidos) {
                document.getElementById("loading").style.display = "block";
                vm.post("/PlanesDeAccion/AddPermisosPlanes/?Area=" + area + "&IdBD=" + vm.idDB + "&Direccion=" + vm.direccionS + "&Unidad="+vm.unidadS, elegidos, function (response) {
                    document.getElementById("loading").style.display = "none";
                    if (response.Correct) {
                        swal("Los permisos se agregaron correctamente", "", "success").then(function () {
                            $(".collapse").removeClass("show");
                            if ($("#gridListAdmin").data("kendoGrid")) {
                                $("#gridListAdmin").data("kendoGrid").destroy();
                                $("#gridListAdmin").empty();
                            }
                            $("#areaSeleccionada").css("display", "none");
                            vm.ListAdmin = [];
                            $scope.$apply();
                        });
                        
                    }
                    else {
                        swal("Ocurrió un error al intentar guardar los permisos", response.ErrorMessage, "error");
                    }
                });
            }


            vm.consultaAreas = function (Objects, index) {
                vm.EstructuraAFM = [];
                [].forEach.call(Objects[0], function (item) {
                    vm.EstructuraAFM.push({ type: item.substring(0, 6), value: item.substring(6, (item.length)) });
                });
                if (vm.EstructuraAFM.length > 0) {
                    var treeObject = vm.GenerarArbol(vm.EstructuraAFM);
                    var tw = new TreeView(
                        treeObject,
                        { showAlwaysCheckBox: true, fold: false });
                    $("#tree-view" + index).empty();
                    document.getElementById("tree-view" + index).appendChild(tw.root);
                    $("#tree-view" + index + " p.group").css("display", "none");
                    $("#tree-view" + index + " p.group:first").css("display", "");
                }
            }
            vm.GenerarArbol = function (ObjectAFM) {
                var uidUnidad, uidCompany, uidArea, uidDepto, uidSubd;
                var treeObject = [];
                /*Relacionar objeto*/
                [].forEach.call(ObjectAFM, function (item) {
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
                [].forEach.call(ObjectAFM, function (item, index) {
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
                    var Division = Enumerable.from(ObjectAFM).where(o => o.type == "Comp=>" && o.IdUnidadNegocio == item.IdUnidadNegocio).toList();
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
                        var AreaAgencia = Enumerable.from(ObjectAFM).where(o => o.type == "Area=>" && o.CompanyId == elem.CompanyId).toList();
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
                treeObject = Enumerable.from(treeObject).where(o => !o.text.includes("-")).toList();
                var treeFinal = [];
                treeFinal = [{
                    text: "Selecciona",
                    checked: false,
                    id: 0,
                    children: treeObject
                }];
                return treeFinal;
            }

            
            function messageBoxError(data) {
                document.getElementById("loading").style.display = "none";
                var error;
                if (data.data == "SessionTimeOut") {
                    swal("Tu sesión ha expirado", "", "info").then(function () {
                        window.location.href = "/LoginAdmin/Login/";
                    });
                    return false;
                }
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
                        alert("messageBoxError: " + data);
                        error = true;
                }
                return error;
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

            $(function () {
                $('#accordion').on('click', '.btn-info', function (e) {
                    var padre = e.target.closest(".collapse");                    
                    var direccion = e.target.closest("p .group");
                    var unidad = e.target.parentNode.parentNode.parentNode.childNodes[1];
                    vm.unidadS = unidad.attributes[1].value;
                    vm.direccionS =direccion.parentElement.childNodes[1].attributes[1].value;
                    vm.idDB = padre.attributes.idbd.value;
                    var areaSelecionada = e.target.data.text;
                    var colocaArea = $("#areaSeleccionada")[0];
                    colocaArea.style.display = "block"
                    colocaArea.innerText = "";
                    colocaArea.innerText ="Configuarando el Área: "+areaSelecionada;
                    vm.ObtenerAdmins(areaSelecionada);
                    //var parent = e.target.closest(".card-body")
                });
            })
        } catch (aE) {
            alert(aE);
        }
    }
})();