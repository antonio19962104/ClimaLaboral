$(document).ready(function () {
    document.getElementById("DDLBD").addEventListener("change", function (e) {
        // cargar la estrucura que existe en el layout de BD
        try {
            var IdBaseDeDatos = e.target.value;
            document.getElementById("loading").style.display = "block";
            $.ajax({
                url: '/EstructuraAFMReporte/GetEstructuraDemografica/?IdBaseDeDatos=' + IdBaseDeDatos,
                type: 'GET',
                success: function (listEstructura) {
                    $.ajax({
                        url: '/EstructuraAFMReporte/GetCompanyCategoria/?IdBaseDeDatos=' + IdBaseDeDatos,
                        type: 'GET',
                        async: false,
                        cache: false,
                        success: function (listCompanyCategoria) {
                            document.getElementById("loading").style.display = "none";
                            $(".sectionConf").show();
                            destroyMultiSelect('UnidadNegocio');
                            document.getElementById("UnidadNegocio").innerHTML = "";
                            [].forEach.call(listCompanyCategoria, function (companyCategoria) {
                                $('#UnidadNegocio').append('<option value="' + companyCategoria + '">' + companyCategoria + '</option>');
                            });
                            // create MultiSelect from select HTML element
                            var required = $("#UnidadNegocio").kendoMultiSelect().data("kendoMultiSelect");
                            required.bind("change", function (e) {
                                document.getElementById("loading").style.display = "block";
                                var list = this.value();
                                /* 
                                 * list contiene las unidades de negocio del kendo-select
                                 * 
                                */ 
                                document.getElementById("resultadosGenerales").colSpan = list.length;
                                $('#mergeFuncion').empty();
                                var txt = "";
                                [].forEach.call(list, function (unidad, index) {
                                    $('#mergeFuncion').append('<th class="text-center" scope="col"><strong>UNeg=>' + unidad + '</strong></th>');
                                    txt += unidad + " ";
                                });
                                var enfoque = document.getElementById("enfoque").value == 1 ? "Enfoque empresa" : "Enfoque área";
                                document.getElementById("descripcionReporte").textContent = "Clima Laboral " + txt + enfoque + " " + document.getElementById("txtAnio").value;
                                mergeDemograficos(listEstructura, IdBaseDeDatos, list);
                                if (list.length == 0) {
                                    $('#mergeFuncion').empty();
                                    $('#cellUNeg').text('');
                                }
                            });
                        }, error: function (err_CompanyCategoria) {
                            document.getElementById("loading").style.display = "none";
                            console.log(err_CompanyCategoria);
                        }
                    });
                }, error: function (err_Funcion) {
                    document.getElementById("loading").style.display = "none";
                    console.log(err_Funcion);
                }
            });
        } catch (aE) {
            document.getElementById("loading").style.display = "none";
            console.log(aE);
        }
    });
    function mergeDemograficos(listEstructura, IdBaseDeDatos, list) {
        try {
            document.getElementsByClassName("k-multiselect-wrap")[0].style.height = "inherit";
            document.getElementById("sexo").colSpan = 2;
            document.getElementById("funcion").colSpan = Enumerable.from(listEstructura).where(o => o.includes("_TF")).toList().length;
            document.getElementById("condicionTrabajo").colSpan = Enumerable.from(listEstructura).where(o => o.includes("_CT")).toList().length;
            document.getElementById("gradoAcademico").colSpan = Enumerable.from(listEstructura).where(o => o.includes("_GA")).toList().length;
            document.getElementById("Antiguedad").colSpan = Enumerable.from(listEstructura).where(o => o.includes("_RA")).toList().length;
            document.getElementById("rangoEdad").colSpan = Enumerable.from(listEstructura).where(o => o.includes("_RE")).toList().length;
            $('#mergeFuncion').append('<th class="text-center" scope="col"><strong>Gene=>Masculino</strong></th>');
            $('#mergeFuncion').append('<th class="text-center" scope="col"><strong>Gene=>Femenino</strong></th>');
            [].forEach.call(listEstructura, function (item) {
                if (item.split("_")[1] == "TF")
                    $('#mergeFuncion').append('<th class="text-center" scope="col"><strong>Func=>' + item.split("_")[0] + '</strong></th>');
                if (item.split("_")[1] == "CT")
                    $('#mergeFuncion').append('<th class="text-center" scope="col"><strong>CTra=>' + item.split("_")[0] + '</strong></th>');
                if (item.split("_")[1] == "GA")
                    $('#mergeFuncion').append('<th class="text-center" scope="col"><strong>GAca=>' + item.split("_")[0] + '</strong></th>');
                if (item.split("_")[1] == "RA")
                    $('#mergeFuncion').append('<th class="text-center" scope="col"><strong>RAnt=>' + item.split("_")[0] + '</strong></th>');
                if (item.split("_")[1] == "RE")
                    $('#mergeFuncion').append('<th class="text-center" scope="col"><strong>REda=>' + item.split("_")[0] + '</strong></th>');
            });
            mergeEstructuraGAFM(IdBaseDeDatos, list);
        } catch (aE) {
            document.getElementById("loading").style.display = "none";
            console.log(aE);
        }
    }

    function mergeEstructuraGAFM(IdBaseDeDatos, list) {
        document.getElementById("loading").style.display = "block";
        var listFilter = list;
        var listLevel = [];
        try {
            var controls = $("#selector-level").find("input[type=checkbox]");
            [].forEach.call(controls, function (elem) {
                if (elem.checked)
                    listLevel.push(elem.value);
            });
            $.ajax({
                url: '/EstructuraAFMReporte/GetEstructuraGAFM/?IdBaseDeDatos=' + IdBaseDeDatos,
                type: 'POST',
                data: { data: list, level: listLevel },
                success: function (listData) {
                    // con list se filtra cuales de las entidades van o no van
                    $(".newColsParent").remove();
                    var counter = 0;
                    var indiceUneg = 0;
                    [].forEach.call(listData, function (item, index) {
                        if (!IsNullOrEmpty(item)) {
                            if (item.includes("UNeg=>") && listLevel.length > 1) {
                                $("#Encabezado").append("<th id='col-uneg-" + index + "' class='text-center newColsParent'>" + item + "</th>");
                                indiceUneg = index;
                                if (listFilter.length == 1)
                                    document.getElementById("col-uneg-" + index).colSpan = listData.length;
                            }
                            if (item.includes("Comp=>")) {
                                $('#mergeFuncion').append('<th class="text-center egafm" style="background-color:#2F75B5;" scope="col"><strong>' + item + '</strong></th>');
                                counter++;
                            }
                            if (item.includes("Area=>")) {
                                $('#mergeFuncion').append('<th class="text-center egafm" style="background-color:#9BC2E6;" scope="col"><strong>' + item + '</strong></th>');
                                counter++;
                            }
                            if (item.includes("Dpto=>")) {
                                $('#mergeFuncion').append('<th class="text-center egafm" style="background-color:#DDEBF7;" scope="col"><strong>' + item + '</strong></th>');
                                counter++;
                            }
                            if (item.includes("SubD=>")) {
                                $('#mergeFuncion').append('<th class="text-center egafm" style="background-color:#ffffff;" scope="col"><strong>' + item + '</strong></th>');
                                counter++;
                            }

                            if (item.includes("UNeg=>") && listLevel.length > 1 && index > 0 && listFilter.length > 1) {
                                var aux = index - (counter + 1)
                                if (document.getElementById("col-uneg-" + aux) != null)
                                    document.getElementById("col-uneg-" + aux).colSpan = counter;
                                counter = 0;
                            }
                            if ((index) == listData.length - 1) {
                                // ultima vuelta
                                if (document.getElementById("col-uneg-" + indiceUneg) != null)
                                    document.getElementById("col-uneg-" + indiceUneg).colSpan = counter;
                            }
                        }
                    });
                    document.getElementById("myTable").style.display = "block";
                    document.getElementById("loading").style.display = "none";
                }, error: function (err_EstructuraGAFM) {
                    document.getElementById("loading").style.display = "none";
                    console.log(err_EstructuraGAFM);
                }
            });
        } catch (aE) {
            document.getElementById("loading").style.display = "none";
            console.log(aE);
        }
    }

    document.getElementById("UnidadNegocio").addEventListener("change", function (e) {
        var required = $("#UnidadNegocio").kendoMultiSelect().data("kendoMultiSelect");
        $('#cellUNeg').text('UNeg=>' + required.value());
    });

    function destroyMultiSelect(id) {
        $('#' + id).unwrap('.k-multiselect').show().empty();
        $(".k-multiselect-wrap").remove();
    }

    document.getElementById("txtAnio").addEventListener("change", function (e) {
        document.getElementById("descripcionReporte").textContent = document.getElementById("descripcionReporte").textContent.substring(0, document.getElementById("descripcionReporte").textContent.length - 4) + e.target.value;
    });

    document.getElementById("enfoque").addEventListener("change", function (e) {
        if (e.target.value == 1)
            document.getElementById("descripcionReporte").textContent = document.getElementById("descripcionReporte").textContent.replace("Enfoque área", "Enfoque empresa");
        if (e.target.value == 2)
            document.getElementById("descripcionReporte").textContent = document.getElementById("descripcionReporte").textContent.replace("Enfoque empresa", "Enfoque área");
    });

    $(".selector").click(function () {
        $(".egafm").remove();
        var list = document.getElementsByClassName("k-button");
        var uneg = [];
        [].forEach.call(list, function (elem) {
            uneg.push(elem.textContent);
        });
        var idBD = document.getElementById("DDLBD").value;
        mergeEstructuraGAFM(idBD, uneg);
    });

    function IsNullOrEmpty(data) {
        if (data == null || data == undefined || data == "")
            return true;
        else
            return false;
    }
});