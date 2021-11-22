var IdPlanDeAccion = 0;
var IdAccionSeleccionada = 0;
(function () {
    "use strict"
    angular.module("app", []).controller("seguimientoController", seguimientoController);

    function seguimientoController($http, $scope) {
        try {
            var vm =this;
            vm.Modulo ="Consulta de Planes de Acción";
            vm.ListPlanesDeAccion =[];
            vm.ListAcciones = [];
            vm.solIcono = "/img/ReporteoClima/Iconos/sol-icono.png";
            vm.solNubeIcono = "/img/ReporteoClima/Iconos/solnube-icono.png";
            vm.nubeIcono = "/img/ReporteoClima/Iconos/nube-icono.png";
            vm.lluviaIcono = "/img/ReporteoClima/Iconos/lluvia-icono.png";

            if (vm.ListAcciones.length == 0)
                vm.Modulo = "Consulta de Planes de Acción";
            else
                vm.Modulo = "Seguimiento de tu Plan de Acción";

            $(document).ready(function () {
                CrearScript("/scripts/PlanesDeAccion/printThis.js");
                document.getElementById("loading").style.display ="block";
                $("#busqueda").on("keyup", function (text) {
                    $("#mergePlan .alert-info").hide();
                    var texto =text.target.value;
                    if (texto.length > 0) {
                        var input =texto.toUpperCase();
                        var todosCard =$("#mergePlan .alert-info");
                        [].forEach.call(todosCard, function (elem) {
                            var dataString =elem.innerText;
                            dataString =dataString.toUpperCase();
                            var existe =false;
                            existe =dataString.includes(input);
                            if (input !="") {
                                if (existe ==true) {
                                    elem.style.display ="block"
                                }
                            }
                        });
                    }
                    else {
                        $("#mergePlan .alert-info").show();
                    }
                });
                vm.ObtenerPlanes();
            });

            vm.ObtenerPlanes =function () {
                vm.get("/PlanesDeAccion/GetPlanes/?IdResponsable=" + IdResponsable, function (response) {
                    if (response.Correct) {
                        if (response.Objects.length ==0)
                            swal("No se encontraron planes de acción en donde te encuestres participando", "", "info");
                        vm.ListPlanesDeAccion =response.Objects;
                    }
                    else {
                        swal("Ocurrió un error al intentar obtener los planes de acción", response.ErrorMessage, "error");
                    }
                    document.getElementById("loading").style.display ="none";
                });
            }

            vm.ObtenerPromedioAvancePlan = function (IdPlan) {
                var avanceGeneral = Enumerable.from(vm.ListPlanesDeAccion).where(o => o.IdPlanDeAccion == IdPlan).firstOrDefault();
                document.getElementById("avance-plan").innerText = avanceGeneral.PorcentajeAvance + "%";
            }

            vm.MostrarSeguimientoAcciones = function (e, IdPlan) {
                IdPlanDeAccion = IdPlan;
                document.getElementById("loading").style.display ="block";
                vm.get("/PlanesDeAccion/GetAccionesByIdResponsable/?IdPlan=" + IdPlan + "&IdResponsable=" + IdResponsable, function (response) {
                    if (response.Correct) {
                        vm.Modulo ="Seguimiento de tu Plan de Acción";
                        //response.Objects[0].AccionesDeMejora.Descripcion = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.";
                        vm.ListAcciones =response.Objects;
                        //[].forEach.call([2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30], function (item) {
                        //    vm.ListAcciones.push(response.Objects[0]);
                        //});
                        if (e != null) {
                            $("#mergePlan .col").hide();
                            e.target.closest(".alert-info").removeAttribute("style")
                            e.target.closest(".col").style.display = "";
                        }
                    }
                    else {
                        swal("Ocurrió un error al intentar obtener las acciones", response.ErrorMessage, "error");
                    }
                    document.getElementById("loading").style.display ="none";
                });
            }

            vm.CerrarDetallePlan =function (e) {
                swal({
                    title: "¿Estás seguro de que deseas salir?",
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
                    if (isConfirm) {
                        document.getElementById("loading").style.display ="block";
                        if (vm.ListAcciones.length > 0) {
                            vm.ListAcciones = [];
                            $("#mergePlan .col").show();
                            /*
                             min-width: 255px; min-height: 138px;*/
                            $(".alert-info").css("min-width", "255px");
                            $(".alert-info").css("min-height", "138px");
                            $(".alert-info").css("max-width", "255px");
                            $(".alert-info").css("max-height", "138px");
                            vm.Modulo = "Consulta de Planes de Acción";
                            $scope.$apply()
                        }
                        document.getElementById("loading").style.display ="none";
                    }
                });
            }

            vm.DetalleAccion = function (IdAccion) {
                vm.Modulo = "Tarjeta de Acciones de Mejora";
                IdAccionSeleccionada = IdAccion;
                $(".tool-bar").hide();
                $("#mergePlan .col").hide();
                /*document.getElementById("mergePlan").children[0].style.display = "none";*/
                /* document.getElementById("mergePlan").children[1].style.display = "none"; */
                var accionPlan = Enumerable.from(vm.ListAcciones).where(o => o.AccionesDeMejora.IdAccionDeMejora == IdAccion).firstOrDefault();
                var htmlContent = templateDetalleAccion;
                htmlContent = vm.replaceHtmlContent(htmlContent, accionPlan);
                $("#mergePlan").append(htmlContent);

                if (IdResponsable == 0) {// Usuario admin
                    [].forEach.call(accionPlan.ListResponsable, function (responsable, index) {
                        $("#mergeResponsables").append(
                            `<div class="row form-group" id="IdResponsable_` + responsable.IdResponsable + `">
                                <div class="col-6"><input type="text" disabled value="` + responsable.Nombre + `" placeholder="Ingresa nombre del responsable" class="form-control frm-respon"></div>
                                <div class="col-6"><input type="text" disabled value="` + responsable.Email + `" placeholder="Ingresa correo electrónico" class="form-control frm-respon-email"></div>
                            </div>`
                        );
                        if (accionPlan.ListResponsable[index].Atachments.length > 0) {
                            [].forEach.call(accionPlan.ListResponsable[index].Atachments, function (evidencia) {
                                var ruta = evidencia.replace("\\\\\\\\10.5.2.101\\\\ClimaLaboral\\\\", "http://demo.climalaboral.divisionautomotriz.com/");
                                $("#IdResponsable_" + responsable.IdResponsable).append(
                                    `<a target="blank" href="` + ruta + `"><small class="ml-3" style="display:block;">` + ruta + `</small></a>`
                                );
                            });
                        }
                        else {
                            $("#IdResponsable_" + responsable.IdResponsable).append(
                                `<small class="ml-3" style="color:red;">El responsable aun no tiene evidencias guardadas</small>`
                            );
                        }
                    });
                }

                $(".delete-file").unbind();
                [].forEach.call(document.getElementsByClassName("delete-file"), function (elem) {
                    elem.addEventListener("click", function (e) {
                        vm.EliminarArchivo(e);
                    });
                });
                var content = document.getElementById('listAcciones');
                var parent = content.parentNode;
                parent.insertBefore(content, parent.firstChild);
                document.getElementsByClassName("btn-back-action-detalle")[0].addEventListener("click", function () {
                    vm.Modulo = "Seguimiento de tu Plan de Acción";
                    $scope.$apply()
                    document.getElementById("listAcciones").remove();
                    document.getElementById("mergePlan").children[0].style.display = "";
                    $(".tool-bar").show();
                });
                document.getElementsByClassName("btnGuardarDetalleAccion")[0].addEventListener("click", function () {
                    if (IdResponsable > 0) {
                        vm.AgregarArchivos();
                    }
                    else {
                        vm.GuardarAvances(accionPlan.AccionesDeMejora.IdAccionDeMejora, IdPlanDeAccion);
                    }
                });
            }

            vm.GuardarAvances = function (IdAccion, IdPlan) {
                var porcentaje = document.getElementById("avance-accion-seleccionada").value;
                if (parseFloat(porcentaje) < 0 || porcentaje == "") {
                    swal("No se puede registrar un avance negativo", "", "info").then(function () {
                        return false;
                    });
                    return false;
                }
                let model = {
                    AccionesDeMejora: { IdAccionDeMejora: IdAccion },
                    PlanDeAccion: { IdPlanDeAccion: IdPlan },
                    PorcentajeAvance: porcentaje
                }
                vm.post("/PlanesDeAccion/GuardarAvances/", model, function (response) {
                    if (response.Correct) {
                        swal("Los avances fueron actualizados correctamente", "", "success").then(function () {
                            document.getElementById("avance-accion-seleccionada").value = porcentaje;
                            vm.MostrarSeguimientoAcciones(null, IdPlanDeAccion);
                        });
                    }
                    else {
                        swal("Ocurrió un error al intentar guardar el avance", response.ErrorMessage, "error");
                    }
                });
            }

            vm.replaceHtmlContent = function (html, accionPlan) {
                html = html.replace("#competencia#", accionPlan.AccionesDeMejora.Categoria.Descripcion);
                html = html.replace("#promedio#", accionPlan.AccionesDeMejora.Categoria.Promedio);
                html = html.replace("#icono#", vm.setIconoSVG(accionPlan.AccionesDeMejora.Categoria.Promedio));
                html = html.replace("#accion#", accionPlan.AccionesDeMejora.Descripcion);
                html = html.replace("#periodicidad#", accionPlan.DescripcionPeriodicidad);
                html = html.replace("#inicio#", accionPlan.sFechaInicio);
                html = html.replace("#fin#", accionPlan.sFechaFin);
                html = html.replace("#objetivo#", accionPlan.Objetivo);
                html = html.replace("#meta#", accionPlan.Meta);
                html = html.replace("#comentarios#", accionPlan.Comentarios);
                html = html.replace("#avance#", accionPlan.PorcentajeAvance);

                var avanceGeneral = Enumerable.from(vm.ListPlanesDeAccion).where(o => o.IdPlanDeAccion == IdPlanDeAccion).firstOrDefault();
                html = html.replace("#cumplimiento#", avanceGeneral.PorcentajeAvance);
                if (IdResponsable > 0) {// Perfil Responsable
                    var evidencias = vm.ObtenerRutaAtachment(accionPlan.Atachments);
                    html = html.replace("#name_responsable#", accionPlan.ListResponsable[0].Nombre);
                    html = html.replace("#email_responsable#", accionPlan.ListResponsable[0].Email);
                    html = html.replace("#listAtachments#", evidencias);
                }
                else { // Perfil administrador creador del plan
                    // Las modificaciones se hacen despues de pegar el contenido
                }
                return html;
            }

            vm.ObtenerRutaAtachment = function (data) {
                var cadena = "";
                [].forEach.call(data, function (item) {
                    var fileName = item.replace("\\\\\\\\10.5.2.101\\\\ClimaLaboral\\\\", "http://demo.climalaboral.divisionautomotriz.com/");     
                    cadena += '<i class="fas fa-close delete-file" title="Eliminar archivo" style="cursor:pointer"></i><a target="blank" href="' + fileName + '"><small style="display: block;">' + fileName + '</small></a>';
                });
                if (cadena.length == 0)
                    cadena = '<small style="display: block;color:red;">Aun no se suben evidencias</small>';
                return cadena;
            }

            vm.setIconoSVG = function (value) {
                try {
                    value = parseFloat(value);
                    if (value < 70) {
                        return vm.lluviaIcono;
                    }
                    if (value >= 70 && value < 80) {
                        return vm.nubeIcono;
                    }
                    if (value >= 80 && value < 90) {
                        return vm.solNubeIcono;
                    }
                    if (value >= 90 && value <= 100) {
                        return vm.solIcono;
                    }
                    if (value > 100) {
                        swal("Existe un porcentaje mayor a 100, verificalo", "", "warning");
                    }
                } catch (aE) {

                }
            }

            vm.PrintElem =function () {
                $(".tool-bar").hide();
                var data =$("#printDiv").html()
                var mywindow =window.open('', 'new div', 'height=1080,width=1920');
                mywindow.document.write('<html><head><title></title>');
                mywindow.document.write('<link rel="stylesheet" href="http://diagnostic4u.com/css/custom-administrator.css" type="text/css" />');
                mywindow.document.write('<link rel="stylesheet" href="http://diagnostic4u.com/css/bootstrap.min.css" type="text/css" />');
                mywindow.document.write('<style>        .list-acciones span, small{display: block;}.errspan {float: right;margin - right: 15px;margin - top: -25px;position: relative;z-index: 2;}   </style></head><body >');
                mywindow.document.write(data);
                mywindow.document.write('</body></html>');
                mywindow.document.close();
                mywindow.focus();
                mywindow.setTimeout(function () { mywindow.print(); }, 1000);
                /* mywindow.close(); */
                $(".tool-bar").show();
                //await $("#printDiv").printThis();
                return true;
            }

            vm.Exportar =function () {
                $(".tool-bar").hide();
                var doc =new jspdf();
                var div =document.getElementById('printDiv');
                $('html,body').scrollTop(0);
                doc.addHTML(div, 0, 0, { /* options */
                    image: { type: 'jpeg', quality: 0.98 },
                    html2canvas: { scale: 2, logging: true }
                },
                    function () {
                        doc.save();
                        $(".tool-bar").show();
                    }
                );
            }

            vm.AgregarArchivos =function () {
                var formData =new FormData();
                var chosser =document.getElementById("fileChosser");
                if (chosser.files.length > 0) {
                    [].forEach.call(chosser.files, function (file, index) {
                        formData.append(fileChosser + "_" + index, file);
                    });

                    var res = Array.from(formData.entries(), ([key, prop]) => (
                        {
                            [key]: {
                                "ContentLength":
                                    typeof prop === "string"
                                        ? prop.length
                                        : prop.size
                            }
                        }));

                    if (res[0]["[object HTMLInputElement]_0"].ContentLength / 1000000 > 20) {
                        swal("No se pueden cargar archivos mayores a 20 MB", "", "info").then(function () {
                            return false;
                        });
                        return false;
                    }

                    $.ajax({
                        url: "/PlanesDeAccion/AgregaArchivosSeguimieto/?IdPlan=" + IdPlanDeAccion + "&IdAccion=" + IdAccionSeleccionada,
                        type: "POST",
                        data: formData,
                        contentType: false,
                        processData: false,
                        success: function (response) {
                            if (response.Correct) {
                                swal("Las evidencias fueron agregadas con éxito", "", "success").then(function () {
                                    /* Actualizar archivos */
                                    $("#listadoDocs").empty();
                                    var cadena = "";
                                    [].forEach.call(response.Atachment, function (item) {
                                        var fileName = item.replace("\\\\\\\\10.5.2.101\\\\ClimaLaboral\\\\", "http://demo.climalaboral.divisionautomotriz.com/");
                                        cadena += '<i class="fas fa-close delete-file" title="Eliminar archivo" style="cursor:pointer"></i><a target="blank" href="' + fileName + '"><small style="display: block;">' + fileName + '</small></a>';
                                    });
                                    $("#listadoDocs").append(cadena);
                                    $(".delete-file").unbind();
                                    [].forEach.call(document.getElementsByClassName("delete-file"), function (elem) {
                                        elem.addEventListener("click", function (e) {
                                            vm.EliminarArchivo(e);
                                        });
                                    });
                                });
                            }
                            else {
                                swal("Ocurrió un error al intentar guardar las evidencias", response.ErrorMessage, "error");
                            }
                        },
                        error: function (err) {
                            swal("Ocurrió un error al intentar guardar las evidencias", err, "error");
                        }
                    });
                }
                else {
                    swal("Debes elegir minimo un archivo para poder guardar tus evidencias", "", "info");
                }
            }

            vm.EliminarArchivo = function (e) {
                var ruta = e.target.nextElementSibling.href;
                var txt = ruta;
                txt = txt.split("//")[txt.split("//").length - 1];
                [].forEach.call([1, 2, 3, 4, 5], function (elem) {
                    txt = txt.replace("%20", " ");
                });
                swal({
                    title: "¿Estás seguro de que deseas eliminar la evidencia evidencia:  [" + txt + "]?",
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
                    if (isConfirm) {
                        document.getElementById("loading").style.display = "block";
                        vm.get("/PlanesDeAccion/EliminarEvidencia/?ruta=" + ruta, function (response) {
                            document.getElementById("loading").style.display = "none";
                            if (response.Correct) {
                                swal("La evidencia se eliminó correctamente", "", "success").then(function () {
                                    /* Remover eliminado */
                                    e.target.nextElementSibling.remove();
                                    e.target.remove();
                                });
                            }
                            else {
                                swal("Ocurrió un error al intentar eliminar la evidencia", response.ErrorMessage, "error");
                            }
                        });
                    }
                });
            }

            /*#region basics functions*/
            function messageBoxError(data) {
                document.getElementById("loading").style.display = "none";
                var error;
                if (data.data =="SessionTimeOut") {
                    swal("Tu sesión ha expirado", "", "info").then(function () {
                        window.location.href ="/LoginAdmin/Login/";
                    });
                    return false;
                }
                switch (data.status) {
                    case 200:
                        return false;
                    case 400:
                        alert("400 - Bad Request");
                        error =true;
                        break;
                    case 401:
                        alert("401 - Unauthorized");
                        error =true;
                        break;
                    case 403:
                        alert("403 - Forbidden");
                        error =true;
                        break;
                    case 404:
                        alert("404 - Not Found");
                        error =true;
                        break;
                    case 408:
                        alert("408 - Request TimeOut");
                        error =true;
                        break;
                    case 500:
                        alert("500 - Internal Server Error");
                        error =true;
                        break;
                    case 502:
                        alert("502 - Bad Gateway");
                        error =true;
                        break;
                    case 503:
                        alert("503 - Service Unavailable");
                        error =true;
                        break;
                    default:
                        alert("messageBoxError: " + data);
                        error =true;
                }
                return error;
            }
            vm.getUid =function () {
                return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
                    var r =Math.random() * 16 | 0, v =c =='x' ? r : (r & 0x3 | 0x8);
                    return v.toString(16);
                });
            }
            vm.get =function (url, functionOK, mostrarAnimacion) {
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
            vm.post =function (url, objeto, functionOK, mostrarAnimacion) {
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
                    if (funcion !=null)
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


var templateDetalleAccion = IdResponsable > 0 ? `
<div class="sectionActions col" id="listAcciones">
    <div id="cat_1" style="padding: 0px 0.5rem !important; display: block;">
        <div class="title-header-blue">
            <div class="row">
                <div class="col-10">
                    <span class="title-header-section"> #competencia#    #promedio#% <img class="ml-3" src="#icono#" style="width: 35px;height: auto;" /> </span>
                </div>
                <div class="col-2 text-center">
                    <button title="Regresar" type="button" idcat="1" class="btn btn-secondary btn-action ml-1 btn-back-action-detalle"><i idcat="1" class="far fa-arrow-alt-circle-left"></i></button>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div id="accordion">
                <div class="card">
                    <div class="card-header" id="heading29_1">
                        <h5 class="mb-0">
                            <span class="btn btn-link col-12" data-toggle="collapse">
                                <input type="text" disabled placeholder="Ingresa nombre de la acción" value="#accion#" class="form-control txt-Nom-Acc">
                            </span>
                        </h5>
                    </div>

                    <div id="collapse29_1" class="collapse show" aria-labelledby="heading29_1" data-parent="#accordion" style="">
                        <div class="card-body">
                            <div class="row form-group">
                                <div class="col-3">Periodicidad: </div>
                                <div class="col-5"><input type="text" disabled placeholder="Ingresa periocidad" value="#periodicidad#" class="form-control frm-periodicidad"></div>
                                <div class="col-2"><i class="far fa-calendar-alt"></i>Inicia:<input disabled type="text" value="#inicio#" placeholder="Ingresa fecha de inicio" class="form-control frm-fecini"></div>
                                <div class="col-2"><i class="far fa-calendar-alt"></i>Concluya:<input disabled type="text" value="#fin#" placeholder="Ingresa fecha de término" class="form-control frm-fecfin"></div>
                            </div>
                            <div class="row form-group">
                                <div class="col-9">Objetivo: </div>
                                <div class="col-3">Meta: </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-9"><input type="text" disabled value="#objetivo#" placeholder="Objetivo a alcanzar con la acción" class="form-control frm-objetivo"></div>
                                <div class="col-3"><input type="text" disabled value="#meta#" placeholder="Ingresa meta" class="form-control frm-meta"></div>
                            </div>
                            <div class="row form-group">
                                <div class="col-6">Responsable: </div>
                                <div class="col-6">Email: </div>
                            </div>
                            <section class="reponsables_29  frm-responsables">
                                <div class="row form-group">
                                    <div class="col-6"><input type="text" disabled value="#name_responsable#" placeholder="Ingresa nombre del responsable" class="form-control frm-respon"></div>
                                    <div class="col-6"><input type="text" disabled value="#email_responsable#" placeholder="Ingresa correo electrónico" class="form-control frm-respon-email"></div>
                                </div>
                            </section>
                            <div class="row form-group">
                                <div class="col-3">Comentarios: </div>
                                <div class="col-9"><input type="text" disabled value="#comentarios#" placeholder="Ingresa comentarios" class="form-control frm-comentarios"></div>
                            </div>

                            <div class="row form-group col-6" style="float:left;">
                                <div class="col-4">Adjuntar archivos de evidencia: </div>
                                <div class="col-8">
                                    <input hidden type="file" placeholder="Selecciona" class="form-control frm-archivos" id="fileChosser" name="fileChosser" multiple="multiple" />
                                    <label for="fileChosser"><i class="fas fa-paperclip fa-lg"></i> Adjuntar archivos de evidencia</label>
                                </div>
                                <div id="listadoDocs" class="offset-4" style="border: 1px solid #FFC000;border-radius: 10px;padding: 5px 10px 5px 10px;">
                                    #listAtachments#
                                </div>
                            </div>

                            <div class="row form-group col-6" style="">
                                <div class="col -6">
                                    <span style="display: block;">Avance: #avance#%</span>
                                    <span style="display: block;">Cumplimiento de la acción: #cumplimiento#%</span>
                                </div>
                            </div>

                            <div class="row form-group mt-4" style="float:right;">
                                <input type="button" class="btn btn-info btnGuardarDetalleAccion" value="Guardar">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
`
: 
`
<div class="sectionActions col" id="listAcciones">
    <div id="cat_1" style="padding: 0px 0.5rem !important; display: block;">
        <div class="title-header-blue">
            <div class="row">
                <div class="col-10">
                    <span class="title-header-section"> #competencia#    #promedio#% <img class="ml-3" src="#icono#" style="width: 35px;height: auto;" /> </span>
                </div>
                <div class="col-2 text-center">
                    <button title="Regresar" type="button" idcat="1" class="btn btn-secondary btn-action ml-1 btn-back-action-detalle"><i idcat="1" class="far fa-arrow-alt-circle-left"></i></button>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div id="accordion">
                <div class="card">
                    <div class="card-header" id="heading29_1">
                        <h5 class="mb-0">
                            <span class="btn btn-link col-12" data-toggle="collapse">
                                <input type="text" disabled placeholder="Ingresa nombre de la acción" value="#accion#" class="form-control txt-Nom-Acc">
                            </span>
                        </h5>
                    </div>

                    <div id="collapse29_1" class="collapse show" aria-labelledby="heading29_1" data-parent="#accordion" style="">
                        <div class="card-body">
                            <div class="row form-group">
                                <div class="col-3">Periodicidad: </div>
                                <div class="col-5"><input type="text" disabled placeholder="Ingresa periocidad" value="#periodicidad#" class="form-control frm-periodicidad"></div>
                                <div class="col-2"><i class="far fa-calendar-alt"></i>Inicia:<input disabled type="text" value="#inicio#" placeholder="Ingresa fecha de inicio" class="form-control frm-fecini"></div>
                                <div class="col-2"><i class="far fa-calendar-alt"></i>Concluya:<input disabled type="text" value="#fin#" placeholder="Ingresa fecha de término" class="form-control frm-fecfin"></div>
                            </div>
                            <div class="row form-group">
                                <div class="col-9">Objetivo: </div>
                                <div class="col-3">Meta: </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-9"><input type="text" disabled value="#objetivo#" placeholder="Objetivo a alcanzar con la acción" class="form-control frm-objetivo"></div>
                                <div class="col-3"><input type="text" disabled value="#meta#" placeholder="Ingresa meta" class="form-control frm-meta"></div>
                            </div>
                            <div class="row form-group">
                                <div class="col-3">Comentarios: </div>
                                <div class="col-9"><input type="text" disabled value="#comentarios#" placeholder="Ingresa comentarios" class="form-control frm-comentarios"></div>
                            </div>


                            <section id="mergeResponsables" class="reponsables_29  frm-responsables">
                                Responsables: 
                            </section>


                            <div class="row form-group col-6" style="">
                                <div class="col -6">
                                    <label>Avance: </label>
                                    <input id="avance-accion-seleccionada" type="text" value="#avance#" class="form-control" style="display: block;" />
                                    <i class="fas fa-percent fa-sm errspan"></i>
                                    <label>Cumplimiento de la acción: </label>
                                    <input type="text" value="#cumplimiento#" class="form-control" style="display: block;">
                                    <i class="fas fa-percent fa-sm errspan"></i>
                                </div>
                            </div>

                            <div class="row form-group mt-4" style="float:right;">
                                <input type="button" class="btn btn-info btnGuardarDetalleAccion" value="Guardar">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
`;

var CrearScript = function (src) {
    var script = document.createElement('script');
    script.src = src;
    document.head.appendChild(script);
}