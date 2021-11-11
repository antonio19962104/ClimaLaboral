(function () {
    "use strict"
    angular.module("app", []).controller("seguimientoController", seguimientoController);

    function seguimientoController($http, $scope) {
        try {
            var vm = this;
            vm.Modulo = "Consulta de Planes de Acción";
            vm.ListPlanesDeAccion = [];
            vm.ListAcciones = [];

            $(document).ready(function () {
                document.getElementById("loading").style.display = "block";
                vm.ObtenerPlanes();
            });

            vm.ObtenerPlanes = function () {
                vm.get("/PlanesDeAccion/GetPlanes/?IdResponsable=" + IdResponsable, function (response) {
                    if (response.Correct) {
                        if (response.Objects.length == 0)
                            swal("No se encontraron planes de acción en donde te encuestres participando", "", "info");
                        vm.ListPlanesDeAccion = response.Objects;
                    }
                    else {
                        swal("Ocurrió un error al intentar obtener los planes de acción", response.ErrorMessage, "error");
                    }
                    document.getElementById("loading").style.display = "none";
                });
            }

            vm.MostrarSeguimientoAcciones = function (e, IdPlan) {
                document.getElementById("loading").style.display = "block";
                vm.get("/PlanesDeAccion/GetAccionesByIdResponsable/?IdPlan=" + IdPlan + "&IdResponsable=" + IdResponsable, function (response) {
                    if (response.Correct) {
                        vm.Modulo = e.target.closest(".alert").innerText;
                        vm.ListAcciones = response.Objects;
                    }
                    else {
                        swal("Ocurrió un error al intentar obtener las acciones", response.ErrorMessage, "error");
                    }
                    document.getElementById("loading").style.display = "none";
                });
            }

            vm.AgregarArchivos = function () {
                var formData = new FormData();
                var chosser = document.getElementById("fileChosser");
                [].forEach.call(chosser.files, function (file, index) {
                    formData.append(fileChosser + "_" + index, file);
                });
                $.ajax({
                    url: "/PlanesDeAccion/AgregaArchivosSeguimieto/?IdPlan=1&IdAccion=1",
                    type: "POST",
                    data: formData,
                    contentType: false,
                    processData: false,
                    success: function (response) {
                        if (response.Correct) {
                            swal("Las evidencias fueron agregadas con éxito", "", "success");
                        }
                        else {
                            swal("Ocurrió un error al intentar guardar las evidencias", response.ErrorMessage, "error");
                        }
                    },
                    error: function (err) {
                        swal("Ocurrió un error al intentar guardar las evidencias", "", "error");
                    }
                });
            }



            /*#region basics functions*/
            function messageBoxError(data) {
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
            /*#endregion request functions*/
        } catch (aE) {
            alert(aE.message);
        }
    }
})();