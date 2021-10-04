/*Get Array By number of Likerts*/
function CreateDataArray(dataForGraphics, long) {
    var dataArray = null;
    if (long > 0) {
        dataArray = new google.visualization.DataTable();
        dataArray.addColumn('string', 'Pregunta');
        dataArray.addColumn('number', 'Casi siempre falso');
        dataArray.addColumn('number', 'Frecuentemente falso');
        dataArray.addColumn('number', 'A veces');
        dataArray.addColumn('number', 'Frecuentemnete es verdad');
        dataArray.addColumn('number', 'Casi siempre es verdad');
        var data = 0;
        for (i = 0; i < dataForGraphics.length; i++) {
            data = data + 1;
            dataArray.addRow([ data + '.-' + dataForGraphics[i].Pregunta, dataForGraphics[i].CSF, dataForGraphics[i].FF, dataForGraphics[i].AV, dataForGraphics[i].FV, dataForGraphics[i].CSV]);
        }
    }
    return dataArray;
}