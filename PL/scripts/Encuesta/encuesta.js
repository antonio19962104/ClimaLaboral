(function () {
    "use strict"
    angular.module("app", []).controller("encuestaController", encuestaController);
    function encuestaController($scope,$http) {
        try {
            var vm = this;
            vm.notificacionInicial = true;
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
                //document.getElementById("loading").style.display = "block";
                //document.getElementsByTagName("textarea")[0].value = ModelNotificacion.PlantillaNotificacionInicial.trim();
                
                $scope.$apply();               
                //document.getElementById("loading").style.display = "none";
            });
            $('#accordion').on('click', '#addNoti', function () {
                var quitaClase = $("#accordion").find(".collapse");
                [].forEach.call(quitaClase, function (item) {
                    item.classList.remove("show");
                });
                var numNotificaciones = $("#accordion .card").length;
                numNotificaciones = numNotificaciones + 1;
                $("#accordion").append(`
                        <div class ="card">
                           <div class ="card-header" id= "heading`+ numNotificaciones + `" >
                              <h5 class ="mb-0">
                                 <span class ="btn btn-link col-10" data-toggle="collapse" data-target= "#collapse`+ numNotificaciones + `" aria-expanded="true" aria-controls= "collapse` + numNotificaciones + `" >
                                 <input type="text" placeholder="Ingresa nombre de la acción" value="Notificación Personalizada" class ="form-control txt-Nom-Acc" />
                                 </span>
                                 <span class ="col-2">
                                 <i class ="far fa-times-circle eliminaAccion" style="cursor: pointer;"></i>
                                 </span>
                              </h5>
                           </div>
                           <div id= "collapse`+ numNotificaciones + `" class ="collapse show" aria-labelledby= "heading` + numNotificaciones + `" data-parent = "#accordion" >
                              <div class ="card-body">
                                 <div class ="md-form mb-5">
                                    <label data-error="wrong" data-success="right" for="inputName">Personaliza tu email de invitación</label>
                                    <div class ="form-group">
                                       <label class ="control-label" style="font-weight:bold">El mensaje debe contener las palabras clave con los asteriscos como se muestra en el ejemplo del mensaje predeterminado</label>
                                       <label class ="control-label" style="font-weight:bold">
                                       *NombreUsuario*
                                       *NombreEncuesta*
                                       *FechaInicio*
                                       *FechaFin*
                                       </label>
                                    </div>
                                    <div class ="md-form mb-5">
                                       <label data-error="wrong" data-success="right" for="inputName">Mensaje predeterminado para el envio de invitaciones</label>
                                       <textarea rows="10" value="" id="msgCustom" style="resize:none;width:100%" class = "form-control input-lg Textarea-editor`+ numNotificaciones + `"></textarea>
                                    </div>
                                    <div class ="row col-12">
                                       <div class ="col-4 p-1">
                                          <label class ="control-label">Prioridad del email</label>
                                          <select class ="form-control selectPrioridad" id="selectPrioridad`+ numNotificaciones + `">
                                          </select>
                                       </div>
                                       <div class ="col-4 p-1">
                                          <label class ="control-label">Asunto del email</label>
                                          <input type="text" class ="form-control frm-asunto" placeholder="Asunto"/>
                                       </div>

                                        <div class ="col-4 p-1">
                                          <label class ="control-label">Estatus de los destinatarios de la notificación</label>
                                          <select class="form-control estatus-encuesta">
                                            <option value="0">Todos los encuestados</option>
                                            <option value="1">No comenzada</option>
                                            <option value="2">En proceso</option>
                                            <option value="3">Terminadas</option>
                                          </select>
                                       </div>
                                        

                                       <!--Customizar expresion cron-->
                                       <div class ="col-4 p-1">
                                          <label class ="control-label">Frecuencia de envío</label>
                                          <select idDivNoti =`+ numNotificaciones + ` class ="form-control frm-frecuencia" id="selectFrecuencia` + numNotificaciones + `">
                                          </select>
                                       </div>
                                       <div class ="col-4 p-1" id= "divSelectDia`+ numNotificaciones + `"  style="display:none;">
                                          <label class ="control-label">Día de la semana para el envío</label>
                                          <select class ="form-control selectDia" id= "selectDia`+ numNotificaciones + `" >
                                          </select>
                                       </div>
                                       <div class ="col-4 p-1" id= "divSelectHora`+ numNotificaciones + `"  style="display:none;">
                                          <label class ="control-label">Horario de envío</label>
                                          <select class="form-control selectHora" id= "selectHora`+ numNotificaciones +`" >
                                          </select>
                                       </div>
                                    </div>
                                    <button class="btn btn-success" onclick="document.getElementById('trigger-notificacion-custom').click();">
                                        Agregar la notificación personalizada
                                        <i class="fas fa-envelope"></i>
                                    </button>
                                 </div>
                              </div>
                           </div>
                        </div>
                `);
                $('.Textarea-editor' + numNotificaciones).summernote({ height: 150, minHeight: null, maxHeight: null, focus: false, placeholder: '', tabsize: 2, dialogsInBody: true, dialogsFade: false });
                var idselectPrioridad = "#selectPrioridad" + numNotificaciones;
                [].forEach.call(vm.Prioridad, function (item) {
                    $(idselectPrioridad).append("<option value='" + item.Id + "'>" + item.Descripcion + "</option>");
                });
                var idselectFrecuencia = "#selectFrecuencia" + numNotificaciones;
                [].forEach.call(vm.FrecuencyList, function (item) {
                    $(idselectFrecuencia).append("<option value='"+item.Id+"'>"+item.Descripcion+"</option>");
                });
                var idselectDia = "#selectDia" + numNotificaciones;
                [].forEach.call(vm.DayList, function (item) {
                    $(idselectDia).append("<option value='" + item.Id + "'>" + item.Descripcion + "</option>");
                });
                var idselectHora = "#selectHora" + numNotificaciones;
                [].forEach.call(vm.HoraryList, function (item) {
                    $(idselectHora).append("<option value='" + item.Id + "'>" + item.Descripcion + "</option>");
                });
            });
            $("body").on("click", ".eliminaAccion", function (e) {
                e.target.closest(".card").remove();
            });
            $("body").on("change", ".frm-frecuencia", function (e) {
                var selector = e.target;
                var idSectionActive = selector.attributes[0].value;
                var iddivH = "#divSelectHora"+idSectionActive;
                $(iddivH)[0].style.display = "block";
                if (selector.value != "3") {
                    var iddivD = "#divSelectDia" + idSectionActive;
                    $(iddivD)[0].style.display = "none";
                }
                else {
                    var iddivD = "#divSelectDia" + idSectionActive;
                    $(iddivD)[0].style.display = "block";
                }
                console.log(e);
            });

            function IsNullOrEmpty(data) {
                if (data == null || data == undefined || data == "") {
                    return true;
                }
                return false;
            }

            vm.CreateCronExpression = function () {
                // Validar con los nuevos campos
                vm.Frecuency.Id = $(".frm-frecuencia:visible").val();
                vm.Frecuency.Id = parseInt(vm.Frecuency.Id);
                if (vm.Frecuency.Id == 0)
                    return "";
                var opc = $(".selectHora:visible").val();
                vm.Horary.Descripcion = $(".selectHora:visible option[value=" + opc + "]")[0].innerText;
                vm.Day.Id = $(".selectDia:visible").val();

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
                ModelNotificacion.Subject = $(".frm-asunto:visible").val();
                ModelNotificacion.Priority = $(".selectPrioridad:visible").val();
                ModelNotificacion.Plantilla = $(".panel-body:visible")[0].innerHTML;
                ModelNotificacion.Frecuencia = vm.CreateCronExpression();
                ModelNotificacion.EstatusEncuesta = $(".estatus-encuesta:visible").val();
                if (IsNullOrEmpty(ModelNotificacion.Subject) || IsNullOrEmpty(ModelNotificacion.Plantilla) || IsNullOrEmpty(vm.Frecuencia)) {
                    swal("Asegurate de haber llenado todos los campos requeridos", "", "info").then(function () {
                        return false;
                    });
                    return false;
                }
                // *NombreUsuario* *NombreEncuesta* *FechaInicio* *FechaFin* 
                if (!ModelNotificacion.Plantilla.includes("*NombreUsuario*")) {
                    swal("Asegurate de haber colocado la etiqueta *NombreUsuario* en el contenido de tu email", "", "info").then(function () {
                        return false;
                    });
                    return false;
                }
                if (!ModelNotificacion.Plantilla.includes("*NombreEncuesta*")) {
                    swal("Asegurate de haber colocado la etiqueta *NombreEncuesta* en el contenido de tu email", "", "info").then(function () {
                        return false;
                    });
                    return false;
                }
                if (!ModelNotificacion.Plantilla.includes("*FechaInicio*")) {
                    swal("Asegurate de haber colocado la etiqueta *FechaInicio* en el contenido de tu email", "", "info").then(function () {
                        return false;
                    });
                    return false;
                }
                if (!ModelNotificacion.Plantilla.includes("*FechaFin*")) {
                    swal("Asegurate de haber colocado la etiqueta *FechaFin* en el contenido de tu email", "", "info").then(function () {
                        return false;
                    });
                    return false;
                }
                vm.post("/Encuesta/ConfiguraNotificacion/?IdEncuesta=" + globalIdEncuesta + "&IdBaseDeDatos=" + globalBD, ModelNotificacion, function (response) {
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
            function messageBoxError() {
                return false;
            }

        } catch (aE) {
            alert(aE);
        }
    }
})();