/*
 * Script del configurador de emails
 */
(function () {
    "use strict"

    angular.module("app", []).controller("emailSenderController", emailSenderController);

    function emailSenderController($scope, $http) {
        try {
            var vm = this;
            vm.IdPlanDeAccion = 0;
            vm.Modulo = "Configura la notificaciones de tu plan de acción";
            vm.Plantilla = "";
            vm.Asunto = "";
            vm.Prioridad = [
                { Id: 0, Descripcion: "Normal" },
                { Id: 1, Descripcion: "Baja" },
                { Id: 2, Descripcion: "Alta" }
            ];
            vm.ListFrecuencia = [
                { Id: "", Descripcion: "Enviar al guardar" },
                { Id: "* * * * *", Descripcion: "Cada minuto" },
                { Id: "0 8 * * *", Descripcion: "Diario" },
                { Id: "0 0 */3 * *", Descripcion: "Cada tercer día" },
                { Id: "0 10 * * 1", Descripcion: "Semanal(Lunes)" },
                { Id: "0 9 1 * *", Descripcion: "Mensual(Primer día del mes)" }
            ];
            vm.PrioridadEmail = vm.Prioridad[0];
            vm.Frecuencia = vm.ListFrecuencia[0];

            $(document).ready(function () {
                document.getElementById("loading").style.display = "block";
                document.getElementsByTagName("textarea")[0].value = ModelNotificacion.PlantillaNotificacionInicial.trim();
                $("textarea").summernote({ height: 250 });
                $scope.$apply();
                vm.ObtenerPlanes();
                document.getElementById("loading").style.display = "none";
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

            vm.SetIdPlan = function (id) {
                document.getElementById("loading").style.display = "block";
                vm.IdPlanDeAccion = id;
                setTimeout(function () {
                    document.getElementById("loading").style.display = "none";
                }, 500);
            }

            vm.ChangeEstatusJob = function (IdPlan, IdJobsNotificacionesPDA, newEstatus) {
                var msg = "";
                vm.get("/PlanesDeAccion/ChangeEstatusJob/?IdPlan=" + IdPlan + "&IdJobsNotificacionesPDA=" + IdJobsNotificacionesPDA + "&NewEstatus=" + newEstatus, function (response) {
                    if (response.Correct) {
                        msg = newEstatus == 0 ? "detenido" : "iniciado";
                        swal("El envio de notificacion se ha " + msg + " correctamente", "", "success").then(function () {
                            vm.ObtenerPlanes();
                        });
                    }
                    else {
                        msg = newEstatus == 0 ? "detener" : "iniciar";
                        swal("Ocurrio un error al intentar " + msg + " la notificacion", response.ErrorMessage, "error");
                    }
                });
            }

            vm.EliminarJob = function (IdJobsNotificacionesPDA) {
                swal({
                    title: "Estas seguro de que quieres eliminar la notificación programada",
                    text: "",
                    icon: "info",
                    buttons: [
                        'Si',
                        'No, cancelar'
                    ],
                    dangerMode: false,
                    allowOutsideClick: false, closeOnClickOutside: false,
                }).then(function (isConfirm) {
                    if (isConfirm) {
                        vm.get("/PlanesDeAccion/EliminarJobNotificacion/?IdJobsNotificacionesPDA=" + IdJobsNotificacionesPDA, function (response) {
                            if (response.Correct) {
                                swal("La notificación fue eliminada correctamente", "", "success").then(function () {
                                    vm.ObtenerPlanes();
                                });
                            }
                            else {
                                swal("Ocurrió un error al intentar eliminar la notificación", response.ErrorMessage, "error");
                            }
                        });
                    }
                });
            }

            vm.GuardarConfiguracion = function (e) {
                ModelNotificacion.Subject = vm.Asunto;
                ModelNotificacion.Priority = vm.PrioridadEmail.Id;
                ModelNotificacion.Plantilla = document.getElementsByTagName("textarea")[0].value;
                ModelNotificacion.Frecuencia = vm.Frecuencia.Id;
                if (IsNullOrEmpty(ModelNotificacion.Subject) || IsNullOrEmpty(ModelNotificacion.Plantilla) || IsNullOrEmpty(vm.Frecuencia)) {
                    swal("Asegurate de haber llenado todos los campos requeridos", "", "info").then(function () {
                        return false;
                    });
                    return false;
                }
                if (!ModelNotificacion.Plantilla.includes("#Nombre#")) {
                    swal("Asegurate de haber colocado la etiqueta #Nombre# en el contenido de tu email", "", "info").then(function () {
                        return false;
                    });
                    return false;
                }
                if (!ModelNotificacion.Plantilla.includes("#Lista Acciones#")) {
                    swal("Asegurate de haber colocado la etiqueta #Lista Acciones# en el contenido de tu email", "", "info").then(function () {
                        return false;
                    });
                    return false;
                }
                vm.post("/PlanesDeAccion/ConfiguraNotificacion/?IdPlanDeAccion=" + vm.IdPlanDeAccion, ModelNotificacion, function (response) {
                    if (response.Data.Correct) {
                        if (ModelNotificacion.Frecuencia != "") {
                            swal("La programación de envio de notificaciones se guardó correctamente", "", "success").then(function () {
                                vm.ObtenerPlanes();
                            });
                        }
                        if (ModelNotificacion.Frecuencia == "") {
                            swal("El envío de la notificación se inició correctamente", "", "success").then(function () {
                                vm.ObtenerPlanes();
                            });
                        }
                    }
                    else {
                        swal("Ocurrión un error al intentar guardar la configuración de envío de notificaciones", response.Data.ErrorMessage, "error");
                    }
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
            function messageBoxError() {
                return false;
            }
            function IsNullOrEmpty(data) {
                if (data == null || data == undefined || data == "") {
                    return true;
                }
                return false;
            }

        } catch (aE) {
            alert(aE);
        }
    }
})();