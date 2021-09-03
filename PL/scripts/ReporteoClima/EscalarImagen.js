﻿var pdf = Object;
pdf = new jsPDF("landscape", "px", "a4");
var imagenesPDF = [];
var baderaFinalizacion = 0;
var contadorPaginas = 0;
var imagenesCount = 0;
var historicoImagen = [];
var docReporte = new jsPDF('landscape', 'pt', 'letter');
var divs =
    ["tab-portada", "tab-portada-azul", "tab-introduccion-amarillo", "tab-iconografia",
        "tab-indicadores-generales", "tab-indicadores-categoria", "tab-impulsores-clave",
        "tab-mejores-ee", "tab-mejores-ea", "tab-peores-ee", "tab-peores-ea",
        "tab-crecimiento-ee", "tab-crecimiento-ea", "tab-decremento-ee", "tab-decremento-ea",
        "tab-bienestar-ee", "tab-bienestar-ea",
        "tab-indicadores-permanencia", "tab-comparativo-permanencia", "tab-comparativo-abandono",
        "tab-generales-departamento-ee", "tab-generales-departamento-ea",
        "pegarReseccionado", "pegarReseccionadoEA",
        "tab-estr-one-level-ee", "tab-estr-one-level-ea",
        "tab-antiguedad-ee", "tab-antiguedad-ea",
        "tab-genero-ee", "tab-genero-ea",
        "tab-grado-aca-ee", "tab-grado-aca-ea",
        "tab-c-trab-ee", "tab-c-trab-ea",
        "tab-funcion-ee", "tab-funcion-ea",
        "tab-edad-ee", "tab-edad-ea"
    ];
var contadorD = 0;
async function crearReportePDF() {
    if (contadorD == 0)
        docReporte.deletePage(1);
    contadorD++;
    //docReporte.deletePage(2);
    docReporte.save('Reporte_Grafico.pdf');
}


function addPadding(list, val) {
    [].forEach.call(list, function (item) {
        if (val == null) {
            item.style.paddingTop = "350px";
        }
        if (val == 1) {
            item.style.paddingTop = "400px";
        }
    });
}
function deletePadding(list) {
    [].forEach.call(list, function (item) {
        item.style.removeProperty("padding-top");
    });
}

/**
 * Crea copia de un canvas con la escala indicada
 * @param {any} base64
 * @param {any} screenWidth
 */
async function start(base64, screenWidth, idElement) {
    if (base64 != "" && base64 != null && idElement != null) {
        var idElement = idElement + "_canvas";
        canvasElement = document.createElement('canvas');
        canvasElement.setAttribute("id", idElement);
        document.getElementById("mergeS").appendChild(canvasElement);
        var myCanvas = document.getElementById(idElement);

        var ctx = myCanvas.getContext("2d");
        var canvas = myCanvas;
        var tempCanvas = document.createElement("canvas");
        document.getElementById("mergeS").appendChild(tempCanvas);
        var tctx = tempCanvas.getContext("2d");

        var img = new Image();
        img.crossOrigin = 'anonymous';
        img.src = base64;

        img.onload = function () {
            document.getElementById("mergeS").appendChild(img);
            myCanvas.width = img.width;
            myCanvas.height = img.height;
            ctx.drawImage(img, 10, 10, img.width, img.height);
            /*
             * Escala 1.8 para el doc
             * Escala 1.7 para impulsores clave
            */
            var escala = 0;
            if (img.height >= 1427)
                escala = (630 * 1.7) / img.width;
            else
                escala = (630 * 1.8) / img.width;
            resizeTo(myCanvas, escala, tempCanvas);
            imagenesCount++;

            /* ===== Creacion de pdf ===== */
            var resolucion = $(window).width();
            var elem = document.getElementById("mergeS");
            var canvas = elem.getElementsByTagName('canvas');

            var width = pdf.internal.pageSize.width;
            var height = pdf.internal.pageSize.height;

            if (img.src != null) {
                try {
                    var elem = document.getElementById("mergeS");
                    var canvas = elem.getElementsByTagName('canvas');
                    for (var j = 0; j < canvas.length; j++) {
                        if (j % 2 == 0) {
                            imgData = canvas[j];
                            canvas[j].toBlob(function (blob) {
                                var reader = new FileReader();

                                reader.onloadend = function () {
                                    if (baderaFinalizacion != parseInt(localStorage.getItem("paginasReporte"))) {
                                        //console.log(reader.result);

                                        imgData = reader.result;
                                        //console.log(imgData);


                                        if (historicoImagen.includes(imgData) == false && imgData != "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/4gIoSUNDX1BST0ZJTEUAAQEAAAIYAAAAAAIQAABtbnRyUkdCIFhZWiAAAAAAAAAAAAAAAABhY3NwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAA9tYAAQAAAADTLQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAlkZXNjAAAA8AAAAHRyWFlaAAABZAAAABRnWFlaAAABeAAAABRiWFlaAAABjAAAABRyVFJDAAABoAAAAChnVFJDAAABoAAAAChiVFJDAAABoAAAACh3dHB0AAAByAAAABRjcHJ0AAAB3AAAADxtbHVjAAAAAAAAAAEAAAAMZW5VUwAAAFgAAAAcAHMAUgBHAEIAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAFhZWiAAAAAAAABvogAAOPUAAAOQWFlaIAAAAAAAAGKZAAC3hQAAGNpYWVogAAAAAAAAJKAAAA+EAAC2z3BhcmEAAAAAAAQAAAACZmYAAPKnAAANWQAAE9AAAApbAAAAAAAAAABYWVogAAAAAAAA9tYAAQAAAADTLW1sdWMAAAAAAAAAAQAAAAxlblVTAAAAIAAAABwARwBvAG8AZwBsAGUAIABJAG4AYwAuACAAMgAwADEANv/bAEMAAwICAgICAwICAgMDAwMEBgQEBAQECAYGBQYJCAoKCQgJCQoMDwwKCw4LCQkNEQ0ODxAQERAKDBITEhATDxAQEP/bAEMBAwMDBAMECAQECBALCQsQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEP/AABEIAJYBLAMBIgACEQEDEQH/xAAVAAEBAAAAAAAAAAAAAAAAAAAACf/EABQQAQAAAAAAAAAAAAAAAAAAAAD/xAAUAQEAAAAAAAAAAAAAAAAAAAAA/8QAFBEBAAAAAAAAAAAAAAAAAAAAAP/aAAwDAQACEQMRAD8AlUAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAD//2Q==") {
                                            if (contadorPaginas > 0)
                                                pdf.addPage();

                                            pdf.addImage(imgData, 'JPEG', -5, -4);
                                            historicoImagen.push(imgData);
                                            contadorPaginas++;
                                        }

                                        if (contadorPaginas == parseInt(localStorage.getItem("paginasReporte"))) {
                                            pdf.deletePage(2);
                                            pdf.save("ReporteGrafico_" + "_UnidadNegocio" + "_" + 2021 + ".pdf");
                                            document.getElementsByClassName("busy")[1].style.display = "none";
                                            baderaFinalizacion = parseInt(localStorage.getItem("paginasReporte"));
                                        }
                                    }
                                };
                                reader.readAsDataURL(blob);

                            }, "image/jpeg");
                        }
                    }
                } catch (e) {
                    swal(e.message, "", "error");
                }
            }
        }
    }
}

/**
 * 
 * @param {any} canvas
 * @param {any} pct
 * @param {any} tempCanvas
 */
function resizeTo(canvas, pct, tempCanvas) {
    try {
        var cw = canvas.width;
        var ch = canvas.height;

        tempCanvas.width = cw;
        tempCanvas.height = ch;
        var tctx = tempCanvas.getContext("2d");
        tctx.drawImage(canvas, 0, 0);
        canvas.width *= pct;
        canvas.height *= pct;
        var ctx = canvas.getContext('2d');
        ctx.drawImage(tempCanvas, 0, 0, cw, ch, 0, 0, cw * pct, ch * pct);
    } catch (e) {
        alert(e.message);
    }
}


var getUid = function () {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
}
var histo = [];
var pruebaExp = function () {
    document.getElementsByClassName("busy")[1].style.display = "block";
    for (var i = 0; i < divs.length; i++) {
        var paginaActiva;
        if (document.getElementById(divs[i]).offsetWidth > 0) {
            paginaActiva = divs[i];
            break;
        }
    }
    document.getElementById(paginaActiva).style.width = "631.4175px";
    document.getElementById(paginaActiva).style.height = "446.46px";
    if (paginaActiva == "tab-portada") {
        var divPrint = document.getElementById(paginaActiva);
        divPrint.getElementsByClassName("portada-bg")[0].style.padding = "29px 25px 80px";
        divPrint.getElementsByClassName("circle-bg")[0].style.padding = "0px";
        var entidad = $("h1.text-white:visible");
        entidad.css("font-size", "1.75rem");
        entidad.css("line-height", "60px");
        entidad.css("padding-bottom", "25px");
        entidad.css("margin-top", "15px");
        entidad.removeClass("mt-3");
        divPrint.getElementsByTagName("img")[0].style.marginTop = "0px";
        divPrint.getElementsByTagName("img")[0].classList.remove("mt-5");
        divPrint.getElementsByClassName("date-portada")[0].style.marginTop = "31px";
    }
    if (paginaActiva == "tab-portada-azul") {
        var divPrint = document.getElementById(paginaActiva);
        divPrint.getElementsByClassName("introduccion-bg")[0].style.marginTop = "0px";
        divPrint.getElementsByClassName("introduccion-bg")[0].style.setProperty("margin-left", "0px", "important");
        divPrint.getElementsByClassName("introduccion-bg")[0].style.setProperty("margin-right", "0px", "important");
        divPrint.getElementsByClassName("introduccion-bg")[0].style.setProperty("padding", "0px", "important");
        divPrint.getElementsByClassName("set-padding-pdf")[0].classList.remove("col-xl-7");
        divPrint.getElementsByClassName("set-padding-pdf")[0].classList.add("col-xl-6");
        divPrint.getElementsByClassName("set-padding-pdf")[1].classList.remove("col-xl-7");
        divPrint.getElementsByClassName("set-padding-pdf")[1].classList.add("col-xl-6");
        var entidad = $("h1.text-white:visible");
        entidad.css("font-size", "1.75rem");
        entidad.css("line-height", "30px");
        var entidad2 = $("h2.text-white:visible");
        entidad2.css("line-height", "28px");
        divPrint.getElementsByTagName("img")[1].classList.add("mt-5");
        var elemento = entidad2[0];
        elemento.style.setProperty("font-size", "1rem", "important");
        elemento = entidad2[1];
        elemento.style.setProperty("font-size", "1rem", "important");
        divPrint.getElementsByTagName("hr")[0].classList.add("mt-0");
    }
    if (paginaActiva == "tab-introduccion-amarillo") {
        var divPrint = document.getElementById(paginaActiva);
        divPrint.getElementsByClassName("clima-laboral")[0].style.setProperty("padding", "0px", "important");
        divPrint.getElementsByClassName("clima-laboral")[0].style.setProperty("margin-left", "0px", "important");
        divPrint.getElementsByClassName("clima-laboral")[0].style.setProperty("margin-right", "0px", "important");
        divPrint.getElementsByClassName("clima-laboral")[0].style.setProperty("padding-top", "1.5rem", "important");
        var entidad = $("button.btn-outline-dark:visible")[0];
        entidad.style.setProperty("font-size", ".9rem", "important");
        entidad.style.setProperty("margin-bottom", "35px", "important");
        entidad.style.setProperty("padding", "6px 9px", "important");
        var entidad2 = $(".yellow-clima.mt-n3:visible");
        entidad2.css("font-size", "20px");
        entidad2.css("padding-left", "10px");
        var entidad3 = $("#tab-introduccion-amarillo p:visible");
        entidad3.css("font-size", "12px");
        entidad3.css("padding-left", "10px");
        entidad3.css("padding-right", "14px");
        var entidad4 = $("#tab-introduccion-amarillo .row .mt-5:visible");
        entidad4.removeClass("mt-5");
        entidad4.addClass("mt-2");
        var entidad5 = $(".clima-laboral2 > .row > .col-2")[0];
        entidad5.style.setProperty("margin-left", "0px", "important");
        entidad5.style.setProperty("margin-right", "0px", "important");
        var entidad6 = $(".clima-laboral2:visible")[0];
        entidad6.style.setProperty("padding", "0px", "important");
        entidad6.style.setProperty("margin-left", "0px", "important");
        entidad6.style.setProperty("margin-right", "0px", "important");
        var entidad7 = $(".clima-laboral2 .row:visible")[0];
        entidad7.style.setProperty("margin-left", "0px", "important");
        entidad7.style.setProperty("margin-right", "0px", "important");
        document.getElementsByClassName("clima-laboral2-2")[0].style.setProperty("margin-left", "0px", "important");
        document.getElementsByClassName("clima-laboral2-2")[0].style.setProperty("margin-right", "0px", "important");
        document.getElementsByClassName("clima-laboral2-2")[0].style.setProperty("padding-right", "0px");
        document.getElementsByClassName("clima-laboral2-2")[0].style.setProperty("padding-left", " 0px");
        document.getElementsByClassName("p-clima2")[0].style.setProperty("padding", "0px", "important");
        $(".p-clima2 .btn")[0].style.setProperty("margin-left", "0px", "important");
        $(".p-clima2 .btn")[0].style.setProperty("font-size", "0.9rem", "important");
        $(".p-clima2 .btn")[0].style.setProperty("margin-top", "28px", "important");
        $(".p-clima2 .btn")[0].style.setProperty("padding", "0.375rem 0.75rem", "important");
        $(".p-clima2 > .row")[0].children[0].style.setProperty("padding-left", "0px", "important");
        $(".p-clima2 > .row")[0].children[0].style.setProperty("padding-right", "0px", "important");
        $(".p-clima2 > .row")[0].children[1].style.setProperty("padding-left", "10px", "important");
        $(".p-clima2 > .row")[0].children[1].style.setProperty("padding-right", "0px", "important");
        $(".p-clima2 > .row")[1].children[0].style.setProperty("padding-left", "0px", "important");
        $(".p-clima2 > .row")[1].children[0].style.setProperty("padding-right", "0px", "important");
        $(".p-clima2 > .row")[1].children[1].style.setProperty("padding-left", "10px", "important");
        $(".p-clima2 > .row")[1].children[1].style.setProperty("padding-right", "0px", "important");
        $(".p-clima2 > .row")[2].children[0].style.setProperty("padding-left", "0px", "important");
        $(".p-clima2 > .row")[2].children[0].style.setProperty("padding-right", "0px", "important");
        $(".p-clima2 > .row")[2].children[1].style.setProperty("padding-left", "10px", "important");
        $(".p-clima2 > .row")[2].children[1].style.setProperty("padding-right", "0px", "important");
        $(".p-clima2 > .row")[3].children[0].style.setProperty("padding-left", "0px", "important");
        $(".p-clima2 > .row")[3].children[0].style.setProperty("padding-right", "0px", "important");
        $(".p-clima2 > .row")[3].children[1].style.setProperty("padding-left", "10px", "important");
        $(".p-clima2 > .row")[3].children[1].style.setProperty("padding-right", "0px", "important");
        $(".clima-laboral2 .yellow-clima").css("font-size", "15px");
        $(".arrow-clima2").css("margin-left", "1px");
        document.getElementsByClassName("p-clima2")[0].children[0].outerHTML = "<center>" + document.getElementsByClassName("p-clima2")[0].children[0].outerHTML + "</center>";
        var img1 = $(".bg-clima3 img");
        img1[0].style.display = "none";
        img1[2].style.display = "none";
        $(".bg-clima3 span")[0].style.setProperty("font-size", ".9rem", "important");
        $(".bg-clima3 span")[0].style.setProperty("margin", "31px 0 0 0", "important");
        $(".bg-clima3 span")[0].style.removeProperty("font-family");
        $(".bg-clima3 img")[1].style.setProperty("max-width", "70%", "important");
        $(".bg-clima3 i")[0].style.setProperty("margin-top", "110px", "important");
        $(".bg-clima3 h3")[0].style.setProperty("font-size", "20px");
        $(".bg-clima3 img")[1].classList.remove("mt-3");
        $(".bg-clima3 img")[1].classList.add("mt-5");
        $(".bg-clima3 img")[1].classList.add("mb-3");
        $("#tab-introduccion-amarillo .row")[0].style.width = "644px";
    }
    if (paginaActiva == "tab-iconografia") {
        document.getElementById(paginaActiva).style.backgroundColor = "#FFF";
        $("#tab-iconografia p")[0].style.display = "none";
        $("#tab-iconografia h2")[0].style.fontSize = "23px";
        $("#tab-iconografia")[0].style.setProperty("padding", "0px 10px", "important");
        $("#tab-iconografia .card-block")[0].style.setProperty("padding", ".8rem", "important");
        $("#tab-iconografia .card-block")[0].classList.remove("mt-5");
        $("#tab-iconografia .card-block")[0].classList.add("mt-3");
        $("#tab-iconografia .card-block")[0].classList.remove("mb-5");
        $("#tab-iconografia .card-block")[0].classList.add("mb-2");
        $("#tab-iconografia .card")[0].style.height = "360px";
        $("#tab-iconografia h3").css("font-size", "22px");
        $("#tab-iconografia a").css("padding", "9px");
        $("#tab-iconografia a").css("width", "100%");



    }
    if (!histo.includes(paginaActiva)) {
        histo.push(paginaActiva);
        html2canvas(document.getElementById(paginaActiva), {
            image: { type: 'jpeg', quality: 0.98 },
            html2canvas: { scale: 2 }
        }).then(function (canvas) {
            var imgData = canvas.toDataURL("image/jpeg", 1.0);
            // document.getElementById("img-" + paginaActiva).width = width + "px";
            // document.getElementById("img-" + paginaActiva).height = "px";
            var pdf = new jsPDF('l', 'px'/*, [842, 595]*/);
            docReporte.addPage(631.4175, 446.46);
            docReporte.addImage(imgData, 'JPEG', 0, 0, 631.4175, 446.46);//400
            document.getElementsByClassName("busy")[1].style.display = "none";
            returnEstilosWeb(paginaActiva);
        });
    }
    else {
        document.getElementsByClassName("busy")[1].style.display = "none";
    }

}

var otraopcion = function () {
    var paginaActiva;
    for (var i = 0; i < divs.length; i++) {

        if (document.getElementById(divs[i]).offsetWidth > 0) {
            paginaActiva = divs[i];
            break;
        }
    }
    var element = document.getElementById(paginaActiva);
    var opt = {
        margin: 1,
        filename: 'myfile.pdf',
        image: { type: 'jpeg', quality: 0.98 },
        html2canvas: { scale: 2 },
        jsPDF: { unit: 'mm', format: 'letter', orientation: 'landscape' }

    };
    // New Promise-based usage:
    html2pdf().from(element).set(opt).save();
}

var returnEstilosWeb = function (paginaActiva) {
    if (paginaActiva == "tab-portada") {
        try {
            var divPrint = document.getElementById("tab-portada");
            document.getElementById("tab-portada").getElementsByTagName("h1")[1].removeAttribute("style");
            divPrint.getElementsByClassName("portada-bg")[0].removeAttribute("style");
            divPrint.getElementsByClassName("circle-bg")[0].removeAttribute("style");
            var entidad = $("h1.text-white")[0];
            entidad.removeAttribute("style");
            entidad.removeAttribute("style");
            entidad.removeAttribute("style");
            entidad.removeAttribute("style");
            entidad.classList.remove("mt-3");
            divPrint.getElementsByTagName("img")[0].removeAttribute("style");
            divPrint.getElementsByTagName("img")[0].classList.add("mt-5");
            divPrint.getElementsByClassName("date-portada")[0].removeAttribute("style");
            divPrint.removeAttribute("style")
        } catch (e) {

        }
    }
    if (paginaActiva == "tab-portada-azul") {
        try {
            var divPrint = document.getElementById("tab-portada-azul");
            divPrint.getElementsByClassName("introduccion-bg")[0].removeAttribute("style");
            divPrint.getElementsByClassName("introduccion-bg")[0].removeAttribute("style");
            divPrint.getElementsByClassName("introduccion-bg")[0].removeAttribute("style");
            divPrint.getElementsByClassName("introduccion-bg")[0].removeAttribute("style");
            divPrint.getElementsByClassName("set-padding-pdf")[0].classList.add("col-xl-7");
            divPrint.getElementsByClassName("set-padding-pdf")[0].classList.remove("col-xl-6");
            divPrint.getElementsByClassName("set-padding-pdf")[1].classList.add("col-xl-7");
            divPrint.getElementsByClassName("set-padding-pdf")[1].classList.remove("col-xl-6");
            var entidad = $("h1.text-white");
            entidad[0].removeAttribute("style");
            entidad[0].removeAttribute("style");
            var entidad2 = $("h2.text-white");
            entidad2[0].removeAttribute("style");
            divPrint.getElementsByTagName("img")[1].classList.remove("mt-5");
            var elemento = entidad2[0];
            elemento.removeAttribute("style");
            elemento = entidad2[1];
            elemento.removeAttribute("style");
            divPrint.getElementsByTagName("hr")[0].classList.remove("mt-0");
            divPrint.removeAttribute("style");
            divPrint.getElementsByClassName("set-padding-pdf")[0].classList.replace("col-xl-7", "col-xl-6");
            divPrint.getElementsByClassName("set-padding-pdf")[1].classList.replace("col-xl-7", "col-xl-6");
        } catch (e) {

        }
    }
    if (paginaActiva == "tab-introduccion-amarillo") {
        try {
            var divPrint = document.getElementById("tab-introduccion-amarillo");
            divPrint.getElementsByClassName("clima-laboral")[0].removeAttribute("style");
            divPrint.getElementsByClassName("clima-laboral")[0].removeAttribute("style");
            divPrint.getElementsByClassName("clima-laboral")[0].removeAttribute("style");
            divPrint.getElementsByClassName("clima-laboral")[0].removeAttribute("style");
            var entidad = $("button.btn-outline-dark")[0];
            entidad.removeAttribute("style");
            entidad.removeAttribute("style");
            entidad.removeAttribute("style");
            var entidad2 = $(".yellow-clima.mt-n3");
            entidad2[0].removeAttribute("style");
            entidad2[0].removeAttribute("style");
            var entidad3 = $("#tab-introduccion-amarillo p")[0];
            entidad3.removeAttribute("style");
            entidad3.removeAttribute("style");
            entidad3.removeAttribute("style");
            var entidad4 = $("#tab-introduccion-amarillo .row .mt-5");
            entidad4.addClass("mt-5");
            entidad4.removeClass("mt-2");
            var entidad5 = $(".clima-laboral2 > .row > .col-2")[0];
            entidad5.removeAttribute("style");
            entidad5.removeAttribute("style");
            var entidad6 = $(".clima-laboral2")[0];
            entidad6.removeAttribute("style");
            entidad6.removeAttribute("style");
            entidad6.removeAttribute("style");
            var entidad7 = $(".clima-laboral2 .row")[0];
            entidad7.removeAttribute("style");
            entidad7.removeAttribute("style");
            document.getElementsByClassName("clima-laboral2-2")[0].removeAttribute("style");
            document.getElementsByClassName("clima-laboral2-2")[0].removeAttribute("style");
            document.getElementsByClassName("clima-laboral2-2")[0].removeAttribute("style");
            document.getElementsByClassName("clima-laboral2-2")[0].removeAttribute("style");
            document.getElementsByClassName("p-clima2")[0].removeAttribute("style");
            $(".p-clima2 .btn")[0].removeAttribute("style");
            $(".p-clima2 .btn")[0].removeAttribute("style");
            $(".p-clima2 .btn")[0].removeAttribute("style");
            $(".p-clima2 .btn")[0].removeAttribute("style");
            $(".p-clima2 > .row")[0].children[0].removeAttribute("style");
            $(".p-clima2 > .row")[0].children[0].removeAttribute("style");
            $(".p-clima2 > .row")[0].children[1].removeAttribute("style");
            $(".p-clima2 > .row")[0].children[1].removeAttribute("style");
            $(".p-clima2 > .row")[1].children[0].removeAttribute("style");
            $(".p-clima2 > .row")[1].children[0].removeAttribute("style");
            $(".p-clima2 > .row")[1].children[1].removeAttribute("style");
            $(".p-clima2 > .row")[1].children[1].removeAttribute("style");
            $(".p-clima2 > .row")[2].children[0].removeAttribute("style");
            $(".p-clima2 > .row")[2].children[0].removeAttribute("style");
            $(".p-clima2 > .row")[2].children[1].removeAttribute("style");
            $(".p-clima2 > .row")[2].children[1].removeAttribute("style");
            $(".p-clima2 > .row")[3].children[0].removeAttribute("style");
            $(".p-clima2 > .row")[3].children[0].removeAttribute("style");
            $(".p-clima2 > .row")[3].children[1].removeAttribute("style");
            $(".p-clima2 > .row")[3].children[1].removeAttribute("style");
            $(".clima-laboral2 .yellow-clima")[0].removeAttribute("style");
            $(".arrow-clima2")[0].removeAttribute("style");
            document.getElementsByClassName("p-clima2")[0].children[0].outerHTML = document.getElementsByClassName("p-clima2")[0].children[0].outerHTML.replace("<center>", "")
            var img1 = $(".bg-clima3 img");
            img1[0].removeAttribute("style");
            img1[2].removeAttribute("style");
            $(".bg-clima3 span")[0].removeAttribute("style");
            $(".bg-clima3 span")[0].removeAttribute("style");
            $(".bg-clima3 span")[0].removeAttribute("style");
            $(".bg-clima3 img")[1].removeAttribute("style");
            $(".bg-clima3 i")[0].removeAttribute("style");
            $(".bg-clima3 h3")[0].removeAttribute("style");
            $(".bg-clima3 img")[1].classList.add("mt-3");
            $(".bg-clima3 img")[1].classList.remove("mt-5");
            $(".bg-clima3 img")[1].classList.remove("mb-3");
            $("#tab-introduccion-amarillo .row")[0].removeAttribute("style");
            [].forEach.call(document.getElementById("tab-introduccion-amarillo").getElementsByClassName("yellow-clima"), function (item) {
                item.removeAttribute("style");
            })
            try {
                [].forEach.call(document.getElementById("tab-introduccion-amarillo").getElementsByClassName("yellow-clima"), function (item) {
                    item.parentNode.children[1].removeAttribute("style")
                });
            } catch (ae) { }
            [].forEach.call(document.getElementById("tab-introduccion-amarillo").getElementsByClassName("row mt-2"), function (item) {
                item.classList.remove("mt-2");
                item.classList.add("mt-5");
                item.classList.remove("mt-2");
                item.classList.add("mt-5");
            });
            document.getElementsByClassName("arrow-clima2")[3].removeAttribute("style");
            document.getElementsByClassName("arrow-clima2")[2].removeAttribute("style");
            document.getElementsByClassName("arrow-clima2")[1].removeAttribute("style");
            document.getElementsByClassName("arrow-clima2")[0].removeAttribute("style");
            document.getElementById("tab-introduccion-amarillo").getElementsByClassName("btn-outline-dark")[0].removeAttribute("style");
            document.getElementById("tab-introduccion-amarillo").getElementsByClassName("btn-outline-dark")[0].classList.remove("mt-2");
            document.getElementById("tab-introduccion-amarillo").getElementsByClassName("btn-outline-dark")[0].classList.add("mt-5");
            document.getElementById("tab-introduccion-amarillo").getElementsByClassName("btn-outline-light")[0].classList.remove("mt-2");
            document.getElementById("tab-introduccion-amarillo").getElementsByClassName("btn-outline-light")[0].classList.add("mt-5");
            document.getElementById("tab-introduccion-amarillo").getElementsByClassName("row mt-2")[0].classList.replace("mt-2", "mt-5");
            document.getElementById("tab-4").getElementsByClassName("container-fluid px-lg-5")[0].removeAttribute("style");
            document.getElementsByClassName("bg-clima3")[0].getElementsByTagName("span")[0].setAttribute("style", "padding: 0;border-style: none;border-radius: 0px !important;font-family: 'robotobold';padding: .5rem 1rem;font-size: 1.25rem !important;line-height: 1.5 !important;color: #f8f9fa !important;border-color: #f8f9fa !important;display: inline-block;font-weight: 400 !important; color: #212529;text-align: center;vertical-align: middle !important;-webkit-user-select: none;-moz-user-select: none;-ms-user-select: none;user-select: none;background-color: transparent;border: 1px solid #f8f9fa !important;padding: .375rem .75rem;font-size: 1rem;line-height: 1.5;text-transform: none;margin: 0;")
            divPrint.removeAttribute("style");
        } catch (e) {

        }
    }
    if (paginaActiva == "tab-iconografia") {
        try {
            var divPrint = document.getElementById("tab-iconografia");
            document.getElementById("tab-iconografia").removeAttribute("style");
            document.getElementById("tab-5").getElementsByClassName("content-wrap p-0")[0].removeAttribute("style");
            document.getElementById("tab-iconografia").getElementsByClassName("card mb-4 mb-md-0")[0].removeAttribute("style");
            $("#tab-iconografia p")[0].removeAttribute("style");
            $("#tab-iconografia h2")[0].removeAttribute("style");
            $("#tab-iconografia")[0].removeAttribute("style");
            $("#tab-iconografia .card-block")[0].removeAttribute("style");
            $("#tab-iconografia .card-block")[0].classList.add("mt-5");
            $("#tab-iconografia .card-block")[0].classList.remove("mt-3");
            $("#tab-iconografia .card-block")[0].classList.add("mb-5");
            $("#tab-iconografia .card-block")[0].classList.remove("mb-2");
            $("#tab-iconografia .card")[0].removeAttribute("style");
            $("#tab-iconografia h3")[0].removeAttribute("style");
            $("#tab-iconografia a")[0].removeAttribute("style");
            $("#tab-iconografia a")[0].removeAttribute("style");
            [].forEach.call(divPrint.getElementsByClassName("btn-indicadores"), function (elem) {
                elem.removeAttribute("style")
            });
            [].forEach.call(document.getElementById("tab-iconografia").getElementsByClassName("yellow-clima"), function (elem) {
                elem.removeAttribute("style")
            });
            [].forEach.call(document.getElementById("tab-5").getElementsByClassName("btn btn-indicadores"), function (elem) {
                elem.removeAttribute("style");
            });
            document.getElementById("tab-5").getElementsByClassName("btn btn-indicadores")[0].removeAttribute("style");
            document.getElementById("tab-5").getElementsByClassName("btn btn-indicadores")[1].removeAttribute("style");
            document.getElementById("tab-5").getElementsByClassName("btn btn-indicadores")[2].removeAttribute("style");
            document.getElementById("tab-5").getElementsByClassName("btn btn-indicadores")[3].removeAttribute("style");
            [].forEach.call(document.getElementById("tab-iconografia").getElementsByClassName("yellow-clima"), function (item) {
                item.removeAttribute("style");
            });
        } catch (e) {

        }
    }
}
var doc = new jsPDF("landscape"); // using defaults: orientation=portrait, unit=mm, size=A4
var createDoc_v2 = function (tab) {
    var divHeight = $('#' + tab).height();
    var divWidth = $('#' + tab).width();
    var ratio = divHeight / divWidth;
    html2canvas(document.getElementById(tab), {
        height: divHeight,
        width: divWidth,
        onrendered: function (canvas) {
            var image = canvas.toDataURL("image/jpeg");
            var width = doc.internal.pageSize.width;
            var height = doc.internal.pageSize.height;
            height = ratio * width;
            doc.addPage(width, height);
            doc.addImage(image, 'JPEG', 0, 0, width, height);
            //doc.save('myPage.pdf'); //Download the rendered PDF.
        }
    });
}

