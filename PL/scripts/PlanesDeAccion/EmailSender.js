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

            vm.FrecuencyList = [
                { Id: 0, Descripcion: "Enviar al guardar" },
                { Id: 1, Descripcion: "Diaria" },
                { Id: 2, Descripcion: "Cada tercer día" },
                { Id: 3, Descripcion: "Semanal" },
                { Id: 4, Descripcion: "Mensual" },
            ];
            vm.Frecuency = vm.FrecuencyList[0];

            vm.HoraryList = [
                { Id: 1, Descripcion: "00:00 am" },
                { Id: 2, Descripcion: "00:30 am" },
                { Id: 3, Descripcion: "01:00 am" },
                { Id: 4, Descripcion: "01:30 am" },
                { Id: 5, Descripcion: "02:00 am" },
                { Id: 6, Descripcion: "02:30 am" },
                { Id: 7, Descripcion: "03:00 am" },
                { Id: 8, Descripcion: "03:30 am" },
                { Id: 9, Descripcion: "04:00 am" },
                { Id: 10, Descripcion: "04:30 am" },
                { Id: 11, Descripcion: "05:00 am" },
                { Id: 12, Descripcion: "05:30 am" },
                { Id: 13, Descripcion: "06:00 am" },
                { Id: 14, Descripcion: "06:30 am" },
                { Id: 15, Descripcion: "07:00 am" },
                { Id: 16, Descripcion: "07:30 am" },
                { Id: 17, Descripcion: "08:00 am" },
                { Id: 18, Descripcion: "08:30 am" },
                { Id: 19, Descripcion: "09:00 am" },
                { Id: 20, Descripcion: "09:30 am" },
                { Id: 21, Descripcion: "10:00 am" },
                { Id: 22, Descripcion: "10:30 am" },
                { Id: 23, Descripcion: "11:00 am" },
                { Id: 24, Descripcion: "11:30 am" },
                { Id: 25, Descripcion: "12:00 am" },
                { Id: 26, Descripcion: "12:30 pm" },
                { Id: 27, Descripcion: "13:00 pm" },
                { Id: 28, Descripcion: "13:30 pm" },
                { Id: 29, Descripcion: "14:00 pm" },
                { Id: 30, Descripcion: "14:30 pm" },
                { Id: 31, Descripcion: "15:00 pm" },
                { Id: 32, Descripcion: "15:30 pm" },
                { Id: 33, Descripcion: "16:00 pm" },
                { Id: 34, Descripcion: "16:30 pm" },
                { Id: 35, Descripcion: "17:00 pm" },
                { Id: 36, Descripcion: "17:30 pm" },
                { Id: 37, Descripcion: "18:00 pm" },
                { Id: 38, Descripcion: "18:30 pm" },
                { Id: 39, Descripcion: "19:00 pm" },
                { Id: 40, Descripcion: "19:30 pm" },
                { Id: 41, Descripcion: "20:00 pm" },
                { Id: 42, Descripcion: "20:30 pm" },
                { Id: 43, Descripcion: "21:00 pm" },
                { Id: 44, Descripcion: "21:30 pm" },
                { Id: 45, Descripcion: "22:00 pm" },
                { Id: 46, Descripcion: "22:30 pm" },
                { Id: 47, Descripcion: "23:00 pm" },
                { Id: 48, Descripcion: "23:30 pm" },
            ];
            vm.Horary = vm.HoraryList[0];

            vm.DayList = [
                { Id: 0, Descripcion: "Domingo" },
                { Id: 1, Descripcion: "Lunes" },
                { Id: 2, Descripcion: "Martes" },
                { Id: 3, Descripcion: "Miercoles" },
                { Id: 4, Descripcion: "Jueves" },
                { Id: 5, Descripcion: "Viernes" },
                { Id: 6, Descripcion: "Sabado" },
            ];
            vm.Day = vm.DayList[0];

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

            vm.SetIdPlan = function (id, nombre) {
                document.getElementById("loading").style.display = "block";
                vm.IdPlanDeAccion = id;
                vm.Modulo = "Configura la notificaciones de el plan de acción: " + nombre;
                vm.Day = vm.DayList[0];
                vm.Frecuency = vm.FrecuencyList[0];
                vm.Horary = vm.HoraryList[0];
                vm.Asunto = "";
                vm.PrioridadEmail = vm.Prioridad[0];
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

            vm.CreateCronExpression = function () {
                if (vm.Frecuency.Id != 0) {
                    var minute, hour, dayMonth, month, dayWeek;
                    minute = parseInt(vm.Horary.Descripcion.split(":")[1])
                    hour = vm.Horary.Descripcion.split(":")[0];
                    dayMonth = "*";
                    month = "*";
                    if (vm.Frecuency.Id == 1)//diario
                        dayWeek = "*";
                    if (vm.Frecuency.Id == 2) {//cada tercer dia
                        dayMonth = "*/3";
                        dayWeek = "*";
                    }
                    if (vm.Frecuency.Id == 3)
                        dayWeek = vm.Day.Id;//Un día en especial
                    if (vm.Frecuency.Id == 4) {
                        dayMonth = 1;//El día uno de cada mes
                        dayWeek = "*";
                    }
                    return minute + " " + hour + " " + dayMonth + " " + month + " " + dayWeek;
                }
                else {
                    return "";
                }
            }

            vm.GuardarConfiguracion = function (e) {
                ModelNotificacion.Subject = vm.Asunto;
                ModelNotificacion.Priority = vm.PrioridadEmail.Id;
                ModelNotificacion.Plantilla = document.getElementsByTagName("textarea")[0].value;
                ModelNotificacion.Frecuencia = vm.CreateCronExpression();
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