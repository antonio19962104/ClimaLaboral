// regular expression
var expRegLetras = /^[a-zA-ZñÑáéíóúÁÉÍÓÚ ]*$/;
var expRegEmail = /^(([^<>()[\]\.,;:\s@@\"]+(\.[^<>()[\]\.,;:\s@@\"]+)*)|(\".+\"))@@(([^<>()[\]\.,;:\s@@\"]+\.)+[^<>()[\]\.,;:\s@@\"]{2,})$/i;
(function () {
    
})();

function isNullOrEmpty(data) {
    return data == "" || data == null || data == undefined
}

function validaEmail(e) {
    if (expRegEmail.test(e.target.value))
        setSuccessColor(e);
    else
        setErrorColor(e);
}

function setSuccessColor(e) {
    e.target.style.border = "1px solid green";
}

function setErrorColor(e) {
    e.target.style.border = "1px solid red";
}

function validaFormulario(id) {
    var valid = true;
    var controls = $("#formReporte").find("input:required,select:required").filter(":visible");
    [].forEach.call(controls, function (elem) {
        if (!elem.value) {
            setErrorColor(elem);
            valid = false;
        }
        else {
            setSuccessColor(elem);
        }
    });
    return valid;
}