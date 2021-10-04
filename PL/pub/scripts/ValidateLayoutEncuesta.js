var excelValido = false;
var propiertiesExcel = null;
var excelData = null;
var arrayForExcel = ["Secciones", "Encabezado", "Pregunta", "Obligatoria", "Tipo de Respuesta", "Respuesta 1", "Respuesta 2"];
function ExportToTable() {  
    $('#exceltable tr').remove();
    var archivo = $('#encuestaFile').val();
    $('#frameExcel').attr('src', archivo);
    var extension = archivo.split('.')[1];
    if (extension == 'xlsx') {  
        var xlsxflag = false; /*Flag for checking whether excel is .xls format or .xlsx format*/  
        if ($("#encuestaFile").val().toLowerCase().indexOf(".xlsx") > 0) {
            xlsxflag = true;  
        }  
        /*Checks whether the browser supports HTML5*/  
        if (typeof (FileReader) != "undefined") {  
            var reader = new FileReader();  
            reader.onload = function (e) {  
                var data = e.target.result;  
                /*Converts the excel data in to object*/  
                if (xlsxflag) {  
                    var workbook = XLSX.read(data, { type: 'binary' });  
                    propiertiesExcel = workbook;
                }  
                else {  
                    var workbook = XLS.read(data, { type: 'binary' });  
                    propiertiesExcel = workbook;
                }  
                /*Gets all the sheetnames of excel in to a variable*/  
                var sheet_name_list = workbook.SheetNames;  
  
                var cnt = 0; /*This is used for restricting the script to consider only first sheet of excel*/  
                sheet_name_list.forEach(function (y) { /*Iterate through all sheets*/  
                    /*Convert the cell value to Json*/
                    if (xlsxflag) {  
                        var exceljson = XLSX.utils.sheet_to_json(workbook.Sheets[y]);  
                        excelData = exceljson;
                    }  
                    else {  
                        var exceljson = XLS.utils.sheet_to_row_object_array(workbook.Sheets[y]); 
                        excelData = excelData;
                        if (exceljson == null) {
                            excelValido = false;
                            swal('El archivo no contiene informacion para guardar', '', 'info');
                        }
                    }  
                    if (exceljson.length > 0 && cnt == 0) {  
                        BindTable(exceljson, '#exceltable');  
                        cnt++;  
                    }  
                });  
                var rows = $('#exceltable tr').length;
                var cols = [];
                
                try {
                    cols.push(propiertiesExcel.Sheets.Encuesta.A1.h);
                    cols.push(propiertiesExcel.Sheets.Encuesta.B1.h);
                    cols.push(propiertiesExcel.Sheets.Encuesta.C1.h);
                    cols.push(propiertiesExcel.Sheets.Encuesta.D1.h);
                    cols.push(propiertiesExcel.Sheets.Encuesta.E1.h);
                    cols.push(propiertiesExcel.Sheets.Encuesta.F1.h);
                    cols.push(propiertiesExcel.Sheets.Encuesta.G1.h);
                } catch (e) {
                    swal('El archivo no coincide con la estrucutura solicitada', '', 'info');
                    excelValido = false;
                }
                
                var jsonArrayCorrect = JSON.stringify(arrayForExcel);
                var jsonArrayInput = JSON.stringify(cols);
                //Validacion
                if (rows == 0) {
                    excelValido = false;
                    swal('El archivo que ha elegido no contiene informacion para importar', '', 'info');
                }
                else{
                    excelValido = true;
                    //$('#exceltable').DataTable();
                    $('#exceltable').hide(); 
                }
                    
                if (jsonArrayCorrect != jsonArrayInput) {
                    excelValido = false;
                    swal('El archivo no coincide con la estrucura del layout especificada.', '', 'info');
                }
                else if (rows > 1 && jsonArrayCorrect == jsonArrayInput && extension == 'xlsx') {
                    excelValido = true;
                    //$('#exceltable').DataTable();
                    $('#exceltable').hide(); 
                }
                else if(rows < 2){
                    swal('El archivo que ha elegido no contiene informacion para importar', '', 'info');
                }
            }  
            if (xlsxflag) {/*If excel file is .xlsx extension than creates a Array Buffer from excel*/  
                reader.readAsArrayBuffer($("#encuestaFile")[0].files[0]);
            }  
            else {  
                reader.readAsBinaryString($("#encuestaFile")[0].files[0]);
            }  
        }  
        else {  
            swal("Your browser does not support HTML5!", "", "info");  
        }  
    }  
    else {  
        swal("Formato de archivo no admitido", "", "info");  
    }  
}  
function BindTable(jsondata, tableid) {/*Function used to convert the JSON array to Html Table*/  
    tableid = '#cuerpoTabla';
    var columns = BindTableHeader(jsondata, tableid); /*Gets all the column headings of Excel*/  
    for (var i = 0; i < jsondata.length; i++) {  
        var row$ = $('<tr/>');  
        for (var colIndex = 0; colIndex < columns.length; colIndex++) {  
            var cellValue = jsondata[i][columns[colIndex]];  
            if (cellValue == null)  
                cellValue = "";  
            row$.append($('<td/>').html(cellValue));  
        }  
        $(tableid).append(row$);  
    }  
}  
function BindTableHeader(jsondata, tableid) {/*Function used to get all column names from JSON and bind the html table header*/  
    tableid = "#filaHead";
    var columnSet = [];  
    var headerTr$ = $('<tr/>');  
    for (var i = 0; i < jsondata.length; i++) {  
        var rowHash = jsondata[i];  
        for (var key in rowHash) {  
            if (rowHash.hasOwnProperty(key)) {  
                if ($.inArray(key, columnSet) == -1) {/*Adding each unique column names to a variable array*/  
                    columnSet.push(key);  
                    headerTr$.append($('<th/>').html(key));  
                }  
            }  
        }  
    }  
    $(tableid).append(headerTr$);  
    return columnSet;  
}