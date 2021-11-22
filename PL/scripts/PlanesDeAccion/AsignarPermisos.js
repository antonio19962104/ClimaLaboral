var Estructura;
var AreaSeleccionada;
(function () {
    "use strict"
    angular.module("app", []).controller("permisosController", permisosController);

    function permisosController($http, $scope) {
        try {
            var vm = this;
            vm.Modulo = "Configura permisos";
            vm.ListAreas = [];
            [].forEach.call([1, 2, 3, 4, 5, 6, 7, 8, 9], function (value) {
                vm.ListAreas.push("Area " + value);
            });

            $(document).ready(function () {
                document.getElementById("loading").style.display = "block";
                vm.get("/PlanesDeAccion/ArbolEstructuraModuloPermisosPlanes/", function (response) {
                    document.getElementById("loading").style.display = "none";
                    if (response.Correct) {
                        console.log(response.Objects[0]);
                        Estructura = response.Objects[0];
                    }
                    else {
                        swal("");
                    }
                });
            });

            vm.ObtenerAdmins = function (e) {
                var area = e.target.value;
                AreaSeleccionada = area;
                document.getElementById("loading").style.display = "block";
                vm.get("/PlanesDeAccion/ObtenerAdmins/?Area=" + area, function (response) {
                    document.getElementById("loading").style.display = "none";
                    if (response.Correct) {
                        vm.ListAdmin = response.Objects;
                    }
                    else {
                        swal("");
                    }
                });
            }

            vm.ObtenerAdminElegidos = function () {
                var elegidos = [];
                var chkList = document.getElementsByClassName("divAdmins")[0].getElementsByTagName("input");
                [].forEach.call(chkList, function (chk) {
                    if (chk.checked)
                        elegidos.push(chk.value);
                });
                console.log(AreaSeleccionada);
                console.log(elegidos);
                vm.AgregarPermisos(AreaSeleccionada, elegidos);
            }

            vm.AgregarPermisos = function (area, elegidos) {
                document.getElementById("loading").style.display = "block";
                vm.post("/PlanesDeAccion/AddPermisosPlanes/?Area=" + area, elegidos, function (response) {
                    document.getElementById("loading").style.display = "none";
                    if (response.Correct) {
                        swal("Los permisos se agregaron correctamente", "", "success");
                    }
                    else {
                        swal("Ocurrió un error al intentar guardar los permisos", response.ErrorMessage, "error");
                    }
                });
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
        } catch (aE) {
            alert(aE);
        }
    }
})();