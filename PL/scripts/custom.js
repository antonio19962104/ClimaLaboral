$(document).ready(function () {
    // Initialize Editor   
    try {
        $('.textarea-editor').summernote(
{
    height: 150,                 // set editor height
    minHeight: null,             // set minimum height of editor
    maxHeight: null,             // set maximum height of editor
    focus: false,
    placeholder: '',
    tabsize: 2,
    dialogsInBody: true,
    dialogsFade: false
});
        $('.textarea-editor-header').summernote(
            {
                toolbar: [
           ['picture', ['picture']]
                ],
                height: 100,                 // set editor height
                minHeight: null,             // set minimum height of editor
                maxHeight: null,             // set maximum height of editor
                focus: false,
                placeholder: '',
                tabsize: 2,
                dialogsInBody: true,
                dialogsFade: false
            });
    } catch (e) {
        console.log(e);
    }

});





function FillCompany() {
    var stateId = $('#CompanyCategoria').val();
    $.ajax({
        url: '/Encuesta/FillCompany',
        type: "GET",
        dataType: "JSON",
        data: { idCompanyCategoria: stateId },
        success: function (companies) {
            $("#Company").html(""); // clear before appending new list 
            $("#Area").html(""); // clear before appending new list 
            $("#Company").append(
                    $('<option></option>').val(0).html('Selecciona una Empresa'));
            $.each(companies.Objects, function (i, Objects) {
                $("#Company").append(
                    $('<option></option>').val(Objects.CompanyId).html(Objects.CompanyName));
            });
        }
    });
};

function FillArea() {
    var companyId = $('#CompanyCategoria').val();
    $.ajax({
        url: '/Encuesta/FillCompanyArea',
        type: "GET",
        dataType: "JSON",
        data: { idCompany: companyId },
        success: function (companies) {
            $("#Area").html(""); // clear before appending new list 
            $("#Area").append(
                    $('<option></option>').val(0).html('Selecciona una Area'));
            $.each(companies.Objects, function (i, Objects) {
                $("#Area").append(
                    $('<option></option>').val(Objects.IdArea).html(Objects.Nombre));
            });
        }
    });
};

$("#wrapper").addClass("animated fadeIn");

$(window).on("scroll", function () {
    if ($(window).scrollTop() > 115) {
        $("#header-survey").addClass("fixed-top");
        $("#header-survey").addClass("shadow-sm");
        $("#header").addClass("header-fixed");
        $("#page-title").addClass("page-title-fixed");
        $("#header").addClass("animated slideInDown");
        $("#page-title").addClass("animated slideInDown");
    } else {
        $("#header-survey").removeClass("fixed-top");
        $("#header-survey").removeClass("shadow-sm");
        $("#header").removeClass("header-fixed");
        $("#page-title").removeClass("page-title-fixed");
        $("#header").removeClass("animated slideInDown");
        $("#page-title").removeClass("animated slideInDown");
    }
});
$(".note-codable").bind("paste", function (event) {
    alert(event.type + ' - ' + event.clipboardData.getData("text/plain"));
    return false;
});
