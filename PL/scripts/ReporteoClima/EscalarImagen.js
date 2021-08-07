var pdf = Object;
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

async function crearReportePDF() {
    docReporte.deletePage(1);
    docReporte.deletePage(2);
    docReporte.save('Reporte_Grafico.pdf');
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