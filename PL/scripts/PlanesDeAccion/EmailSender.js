/*
 * Script del configurador de emails
 */
(function () {
    "use strict"

    angular.module("app", []).controller("emailSenderController", emailSenderController);

    function emailSenderController() {
        try {
            var vm = this;
            vm.Header = "Configura las notificaciones de tu plan de acción";

            $(document).ready(function () {
                $("textarea").summernote({ height: 200 });
                document.getElementsByTagName("textarea")[0].value = ModelNotificacion.PlantillaNotificacionInicial.trim();
                document.getElementsByTagName("textarea")[1].value = ModelNotificacion.PlantillaNotificacionPrevia.trim();
                document.getElementsByTagName("textarea")[2].value = ModelNotificacion.PlantillaSinAvanceInicial.trim();
                document.getElementsByTagName("textarea")[3].value = ModelNotificacion.PlantillaAvanceNoLogrado.trim();
                document.getElementsByTagName("textarea")[4].value = ModelNotificacion.PlantillaNotificacionAgradecimiento.trim();
                var select = document.createElement("select");
                select.classList.add("form-control");//note-btn btn btn-default btn-sm
                select.classList.add("btn");
                select.classList.add("btn-default");
                select.classList.add("btn-sm");
                select.classList.add("ml-1");
                select.options.add(new Option("-Prioridad-", null, false, false));
                select.options.add(new Option("-Baja-", 1, false, false));
                select.options.add(new Option("-Normal-", 0, false, false));
                select.options.add(new Option("-Alta-", 2, false, false));
                // Mostrar uno a uno los textarea
                // Agregar las funciones de CC, CCO, Prioridad
                //setTimeout(function () {
                //    [].forEach.call(document.getElementsByClassName("config-notificacion"), function (div) {
                //        div.getElementsByClassName("note-toolbar panel-heading")[0].innerHTML += '<div class="note-btn-group btn-group note-priority">' + (select.outerHTML) + '</div>'
                //    });
                //}, 10000);
            });

            vm.GuardarConfiguracion = function (e) {
                ModelNotificacion.TipoNotify = e.closests(".config-notificacion").id.value;
                vm.post("/PlanesDeAccion/ConfNotificacion/?IdPlanDeAccion=" + IdPlanDeAccion, ModelNotificacion, function (response) {
                    if (response.Correct) {
                        swal("La configuración de guardó correctamente", "", "success");
                    }
                    else {
                        swal("Ocurrión un error al intentar guardar la configuración", response.ErrorMessage, "error");
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

        } catch (aE) {
            alert(aE);
        }
    }
})();