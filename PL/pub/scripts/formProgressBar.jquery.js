$.fn.formProgressBar = function (options) {
    //Progresso por seccion
    function getAllPreguntasVisible() {
        //Actualiza los items visibles en caso de bifurcación
        //var formElements = $('#formularioEncuesta').find('input,textarea,select').filter('[required]:visible').toArray();
        //var formElements = $('#formularioEncuesta').find('input,textarea,select').filter(':visible').toArray();
        var formElements = $('#formularioEncuesta').find('input[type="radio"], input[type="text"], input[type="checkbox"], textarea,select').filter(':visible').toArray();

        //Buscar items cuyo nombre inicia con likert y agregarlos a formElements
        var input = document.getElementsByTagName("input");
        var inputList = Array.prototype.slice.call(input);
        inputList.forEach(function (itemHTML, index) {
            var name = itemHTML.name;
            var nameForValid = name.substr(0, 6);
            var ancho = itemHTML.offsetWidth;
            var alto = itemHTML.offsetHeight;
            var left = itemHTML.offsetLeft;
            var top = itemHTML.offsetTop;
            var isVisible = false;
            if (ancho > 0 && alto > 0) {
                isVisible = true;
            }
            if (nameForValid == 'likert' && isVisible == true) {
                console.log("Index " + index + " | itemHTML: " + itemHTML)
                formElements.push(itemHTML);
            }
        });

        arr = [];
        formElements.map(function (item) {
            arr[item.name] = 0;
            if (item.checked)
                arr[item.name] = 1;
        });
        console.log(Object.keys(arr).length);
        var longitudActualizada = Object.keys(arr).length;
        return longitudActualizada;
    }

    function isElementVisible(element) { 
        try {
            if (element[0].offsetWidth > 0 ||
           element[0].offsetHeight > 0 || element[0].offsetLeft > 0 ||
            element[0].offsetRight > 0 || element[0].offsetTop > 0) {
                console.log(element);
                return true;
            }
            else {
                return false;
            }
        } catch (e) {
            return false;
        }
    } 
    function updateRequeridos(formElements) {
        //var formElements = $(this).find("input[required], textarea[required], select[required]").toArray();
        //My Code
        //var formElements = $(this).find('input,textarea,select').filter('[required]:visible').toArray();
        //var items = $('input,textarea,select').filter('[required]:visible').toArray();
        //console.log(items);
        //End My Code
        arr = [];
        formElements.map(function (item) {

            arr[item.name] = 0;
            if (item.checked)
                arr[item.name] = 1;
        })
        this.bindElements();
    }
    function updateItems() {
        var formFields = this.elementsRequied();
        this.bindElements();
        return this;
    }

    var defaults = {
        readCount: false,
        validClass: 'valid',
        invalidClass: 'error',
        percentCounting: false,
        transitionTime: 0,
        height: 10,
        transitionType: 'ease' //ease, linear, ease-in, ease-out, ease-in-out
    };

    var settings = $.extend({}, defaults, options);

    $("body").prepend("<div id='jQueryProgressFormBar'><div></div></div>")
    $("#jQueryProgressFormBar").css("position", "fixed").css("top", 0).css("left", 0).css("width", "100%").css("height", settings.height).css("z-index", 10000)
    $("#jQueryProgressFormBar>div").css({
        WebkitTransition: 'all ' + settings.transitionTime + 'ms ' + settings.transitionType,
        MozTransition: 'all ' + settings.transitionTime + 'ms ' + settings.transitionType,
        MsTransition: 'all ' + settings.transitionTime + 'ms ' + settings.transitionType,
        OTransition: 'all ' + settings.transitionTime + 'ms ' + settings.transitionType,
        transition: 'all ' + settings.transitionTime + 'ms ' + settings.transitionType
    }).css("height", settings.height);

    this.elementsRequied = function () {
        var formElements = $(this).find('input,textarea,select').filter('[required]:visible').toArray();
        //var formElements = $(this).find('input,textarea,select').filter(':visible').toArray();

        arr = [];
        formElements.map(function (item) {

            arr[item.name] = 0;
            if (item.checked)
                arr[item.name] = 1;
        })
        return arr;
    }

    
        
    

    //Progress bar update
    this.renderBar = function () {
        //Update All items
        var formElements = $('#formularioEncuesta').find('input,textarea,select').filter('[required]:visible').toArray();
        //var formElements = $('#formularioEncuesta').find('input,textarea,select').toArray();

        //Buscar items cuyo nombre inicia con likert y agregarlos a formElements
        var input = document.getElementsByTagName("input");
        var inputList = Array.prototype.slice.call(input);
        //inputList.forEach(function (itemHTML, index) {
        //    var name = itemHTML.name;
        //    var nameForValid = name.substr(0, 6);
        //    var ancho = itemHTML.offsetWidth;
        //    var alto = itemHTML.offsetHeight;
        //    var left = itemHTML.offsetLeft;
        //    var top = itemHTML.offsetTop;
        //    var isVisible = false;
        //    if (ancho > 0 && alto > 0) {
        //        isVisible = true;
        //    }
        //    if (nameForValid == 'likert') {
        //        console.log("Index " + index + " | itemHTML: " + itemHTML)
        //        formElements.push(itemHTML);
        //    }
        //});

        arr = [];
        formElements.map(function (item) {
            arr[item.name] = 0;
            if (item.checked)
                arr[item.name] = 1;
        });
        console.log(Object.keys(arr).length);
        var longitudActualizada = Object.keys(arr).length;
        
        longitudActualizada = getAllPreguntasVisible();

        //inicializa valores
        var correctFields = 0
        var length = 0;
        var error = false;
        this.refresh = function () {
            formFields = this.elementsRequied();
        }
        
        //Update Items in form
        this.refresh = function () {
            formFields = this.elementsRequied();
        }
        
        refresh = function () {
            formFields = this.elementsRequied();
            return formFields;
        }

        
        
        //And isVisible = true
        

        for (var item in formFields) {
            try {
                var elemHTML = document.getElementsByName(item);
                var BloquePreg = $('#' + elemHTML[0].id).closest('.BloquePreg');
                //var valor = document.getElementsByName(item)[0].checked;
                var visibility = isElementVisible(elemHTML);
                console.log('valor de cada item: ' + formFields[item]);
                if (formFields[item] == 1 && BloquePreg[0].style.display == 'block' && elemHTML[0].classList.contains('validarPreguntas')) {
                    correctFields++;
                }
                if (formFields[item] == -1)
                    error = true;
                length++;
            } catch (e) {

            }
        }

        
        //var percentOfSuccess = (correctFields / longitudActualizada * 100).toFixed(2);
        var percentOfSuccess = (correctFields / totalHabilitadas * 100).toFixed(2);
        if (settings.percentCounting)
            $("#jQueryProgressFormBar>div").text(Math.round(percentOfSuccess) + " %")
        $("#jQueryProgressFormBar>div").css("width", percentOfSuccess + "%")
        if (error == true)
            $("#jQueryProgressFormBar>div").addClass('warn')
        else
            $("#jQueryProgressFormBar>div").removeClass('warn').removeClass('error')

    }
    this.bindElements = function () {
        var editBar = this.renderBar
        //var formElements = $(this).find('input,textarea,select').filter('[required]:visible').toArray();
        $('input[type="range"]:required').change(function () {
            if ($(this).val() >= 0) {
                formFields[$(this).attr("name")] = 1
                editBar();
            } else {
                formFields[$(this).attr("name")] = -1
                editBar();
            }
        });

        $('input[type="range"]').change(function () {
            if ($(this).val() >= 0) {
                formFields[$(this).attr("name")] = 1
                editBar();
            } else {
                formFields[$(this).attr("name")] = -1
                editBar();
            }
        });

        $('input[type="radio"]').change(function () {
            var isLikert = false;
            var name = this.name;
            var nameLik = name.substr(0, 6);
            if (nameLik == 'likert') {
                if ($(this).is(':checked')) {
                    formFields[$(this).attr("name")] = 1
                    editBar();
                } else {
                    formFields[$(this).attr("name")] = -1
                    editBar();
                }
            }
        });



        $('input[type="text"]:required').keyup(function () {
            if ($(this).val() != "") {
                formFields[$(this).attr("name")] = 1
                editBar();
            } else {
                formFields[$(this).attr("name")] = -1
                editBar();
            }
        });

        $('input[type="text"]').keyup(function () {
            if ($(this).val() != "") {
                formFields[$(this).attr("name")] = 1
                editBar();
            } else {
                formFields[$(this).attr("name")] = -1
                editBar();
            }
        });

        $('textarea[required]').keyup(function () {
            if ($(this).val() != "") {
                formFields[$(this).attr("name")] = 1
                editBar();
            } else {
                formFields[$(this).attr("name")] = -1
                editBar();
            }
        });

        $('textarea').keyup(function () {
            if ($(this).val() != "") {
                formFields[$(this).attr("name")] = 1
                editBar();
            } else {
                formFields[$(this).attr("name")] = -1
                editBar();
            }
        });


        $('form').find("input[required], textarea[required], select[required]").change(function () {//2DA
        //$('form').find("input, textarea, select").change(function () {//fINAL
        //$(this).find("input, textarea, select").filter('[required]:visible').change(function () {//pRIM
            if (!settings.readCount) {
                switch ($(this).prop('nodeName')) {
                    case "INPUT":
                        switch ($(this).attr("type")) {
                            case "text":
                                if ($(this).val() != "") {
                                    formFields[$(this).attr("name")] = 1
                                } else {
                                    formFields[$(this).attr("name")] = -1
                                }
                                break;
                            case "email":
                                var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                                if (re.test(String($(this).val()).toLowerCase())) {
                                    formFields[$(this).attr("name")] = 1
                                } else {
                                    formFields[$(this).attr("name")] = -1
                                }
                                break;
                            case "number":
                                if ($.isNumeric($(this).val())) {
                                    formFields[$(this).attr("name")] = 1
                                } else {
                                    formFields[$(this).attr("name")] = -1
                                }
                                break;
                            case "range":
                                if ($(this).val() >= 0) {
                                    formFields[$(this).attr("name")] = 1
                                } else {
                                    formFields[$(this).attr("name")] = -1
                                }
                                break;
                            case "checkbox":
                                var chk = $('form').find('input[name="' + this.name + '"]');
                                var isCheck = 0;
                                for (var i = 0; i < chk.length; i++) {
                                    if (chk[i].checked == true) {
                                        isCheck++;
                                    }
                                }
                                if ($(this).is(':checked') && isCheck == 1) {
                                    formFields[$(this).attr("name")] = 1
                                }
                                else if (isCheck == 0) {
                                    formFields[$(this).attr("name")] = -1
                                }
                                break;
                            case "radio":
                                if ($(this).is(':checked')) {
                                    formFields[$(this).attr("name")] = 1
                                } else {
                                    formFields[$(this).attr("name")] = -1
                                }
                                break;
                        }
                        break;
                    case "SELECT":
                        if ($(this).val() != "" && $(this).val() != 0) {
                            formFields[$(this).attr("name")] = 1
                        } else {
                            formFields[$(this).attr("name")] = -1
                        }
                        break;
                    case "TEXTAREA":
                        if ($(this).val() != "") {
                            formFields[$(this).attr("name")] = 1
                        } else {
                            formFields[$(this).attr("name")] = -1
                        }
                        break;
                }

            } else {
                switch ($(this).attr("type")) {
                    case "checkbox":
                        var chk = $('form').find('input[name="' + this.name + '"]');
                        var isCheck = 0;
                        for (var i = 0; i < chk.length; i++) {
                            if (chk[i].checked == true) {
                                isCheck ++;
                            }
                        }
                        if ($(this).is(':checked') && isCheck == 1) {
                            formFields[$(this).attr("name")] = 1
                        }
                        else if (isCheck == 0) {
                            formFields[$(this).attr("name")] = -1
                        }
                        break;
                    case "radio":
                        if ($(this).is(':checked')) {
                            formFields[$(this).attr("name")] = 1
                        } else {
                            formFields[$(this).attr("name")] = -1
                        }
                        break;
                }
            }
            editBar();
        })

        if (settings.readCount) {
            //var $div = $(this).find("input[required], textarea[required], select[required]");
            var $div = $(this).find("input, textarea, select");
            //var $div = $(this).find('input,textarea,select').filter('[required]:visible').toArray();
            var observer = new MutationObserver(function (mutations) {
                mutations.forEach(function (mutation) {
                    if (mutation.attributeName === "class") {
                        if ($(mutation.target).hasClass(settings.validClass)) {
                            formFields[$(mutation.target).attr("name")] = 1
                        } else if ($(mutation.target).hasClass(settings.invalidClass)) {
                            formFields[$(mutation.target).attr("name")] = -1
                        }
                        editBar();
                    }
                });
            });
            $div.map(function (item) {
                observer.observe($div[item], {
                    attributes: true
                });
            })
        }
        ;
        $(this).submit(function (e) {
            if ($("#jQueryProgressFormBar>div").hasClass('warn')) {
                $("#jQueryProgressFormBar>div").removeClass("warn").addClass("error")
                e.preventDefault()
                return
            }
            var correctFields = 0

            for (var item in formFields) {
                if (formFields[item] == 1) {
                    correctFields++;
                }
                length++;
            }
            //update longitud
            //var formElements = $('#formularioEncuesta').find('input,textarea,select').filter('[required]:visible').toArray();
            var formElements = $('#formularioEncuesta').find('input,textarea,select').filter(':visible').toArray();
            arr = [];
            formElements.map(function (item) {
                arr[item.name] = 0;
                if (item.checked)
                    arr[item.name] = 1;
            });
            console.log(Object.keys(arr).length);
            var longitudActualizada = Object.keys(arr).length;

            //alert(totalHabilitadas);
            longitudActualizada = getAllPreguntasVisible();

            //if (correctFields != longitudActualizada) {//era diferente de
            //    $("#jQueryProgressFormBar>div").removeClass("warn").addClass("error")
            //    e.preventDefault()
            //    return
            //}
            if (correctFields != totalHabilitadas) {//era diferente de
                $("#jQueryProgressFormBar>div").removeClass("warn").addClass("error")
                e.preventDefault()
                return
            }

            $("#jQueryProgressFormBar>div").removeClass("warn").removeClass("error")


        })
    }

    var formFields = this.elementsRequied();



    this.bindElements()

    return this;
};
var correctas = 0;
function restaContestadas() {
    correctas = correctFields - 1;
    return correctas;
}
$.fn.formProgressBar
