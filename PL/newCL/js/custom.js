$(document).ready(function(){

  	$('#summernote').summernote({
        placeholder: '',
        tabsize: 2,
        height: 100,
        dialogsInBody: true,
		dialogsFade: false
    });

    $("#wrapper").addClass("animated fadeIn");

    $( "#navbarHeader ul li a" ).hover(function() {
      $( "#header" ).addClass( 'mb-4' );
    });

    $(window).on("scroll", function() {
	    if($(window).scrollTop() > 115) {
            $("#header-survey").addClass("fixed-top");
	        $("#header-survey").addClass("shadow-sm");
            $("#header").addClass("header-fixed");
            $("#page-title").addClass("page-title-fixed");
	    } else {
            $("#header-survey").removeClass("fixed-top");
	        $("#header-survey").removeClass("shadow-sm");
            $("#header").removeClass("header-fixed");
            $("#page-title").removeClass("page-title-fixed");
	    }
	});
});