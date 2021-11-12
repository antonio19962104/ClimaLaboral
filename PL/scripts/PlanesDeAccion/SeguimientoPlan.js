﻿(function () {
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

            $(document).ready(function () {
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

            vm.MostrarSeguimientoAcciones =function (e, IdPlan) {
                document.getElementById("loading").style.display ="block";
                vm.get("/PlanesDeAccion/GetAccionesByIdResponsable/?IdPlan=" + IdPlan + "&IdResponsable=" + IdResponsable, function (response) {
                    if (response.Correct) {
                        vm.Modulo ="Seguimiento de tu Plan de Acción";
                        response.Objects[0].AccionesDeMejora.Descripcion ="Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.";
                        vm.ListAcciones =response.Objects;
                        [].forEach.call([2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30], function (item) {
                            vm.ListAcciones.push(response.Objects[0]);
                        });
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
                            vm.ListAcciones =[];
                            $scope.$apply()
                        }
                        document.getElementById("loading").style.display ="none";
                    }
                });
            }

            vm.DetalleAccion =function (IdAccion) {
                document.getElementById("mergePlan").children[0].style.display = "none";
                //document.getElementById("mergePlan").children[1].style.display = "none";
                var accionPlan = Enumerable.from(vm.ListAcciones).where(o => o.AccionesDeMejora.IdAccionDeMejora == IdAccion).firstOrDefault();
                var htmlContent = templateDetalleAccion;
                htmlContent = vm.replaceHtmlContent(htmlContent, accionPlan);
                $("#mergePlan").append(htmlContent);
                var content = document.getElementById('listAcciones');
                var parent = content.parentNode;
                parent.insertBefore(content, parent.firstChild);
                document.getElementsByClassName("btn-back-action-detalle")[0].addEventListener("click", function () {
                    document.getElementById("listAcciones").remove();
                    document.getElementById("mergePlan").children[0].style.display = "";
                });
            }

            vm.replaceHtmlContent = function (html, accionPlan) {
                var evidencias = vm.ObtenerRutaAtachment(accionPlan.Atachments);
                html = html.replace("#competencia#", accionPlan.AccionesDeMejora.Categoria.Descripcion);
                html = html.replace("#promedio#", accionPlan.AccionesDeMejora.Categoria.Promedio);
                html = html.replace("#icono#", vm.setIconoSVG(accionPlan.AccionesDeMejora.Categoria.Promedio));
                html = html.replace("#accion#", accionPlan.AccionesDeMejora.Descripcion);
                html = html.replace("#periodicidad#", accionPlan.Periodicidad);
                html = html.replace("#inicio#", accionPlan.sFechaInicio);
                html = html.replace("#fin#", accionPlan.sFechaFin);
                html = html.replace("#objetivo#", accionPlan.Objetivo);
                html = html.replace("#meta#", accionPlan.Meta);
                html = html.replace("#name_responsable#", accionPlan.ListResponsable[0].Nombre);
                html = html.replace("#email_responsable#", accionPlan.ListResponsable[0].Email);
                html = html.replace("#comentarios#", accionPlan.Comentarios);
                html = html.replace("#avance#", accionPlan.PorcentajeAvance);
                html = html.replace("#cumplimiento#", "");
                html = html.replace("#listAtachments#", evidencias);
                return html;
            }

            vm.ObtenerRutaAtachment = function (data) {
                var cadena = "";
                [].forEach.call(data, function (item) {
                    cadena += '<small style="display: block;">' + item + '</small>';
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
                //mywindow.close();
                $(".tool-bar").show();
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
                        swal("Ocurrió un error al intentar guardar las evidencias", err, "error");
                    }
                });
            }



            /*#region basics functions*/
            function messageBoxError(data) {
                var error;
                if (data.data.Correct ==undefined) {
                    alert(data);
                }
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


var templateDetalleAccion = `
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
                                <input type="text" placeholder="Ingresa nombre de la acción" value="#accion#" class="form-control txt-Nom-Acc">
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
                                <div class="col-8"><input type="file" placeholder="Selecciona" class="form-control frm-archivos"></div>
                                <div id="listadoDocs" class="offset-4">
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
                                <input type="button" class="btn btn-info" value="Guardar">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
`;