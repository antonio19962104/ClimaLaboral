$(document).ready(function () {
    $('#mySidenav li span a').click(function () {
        if ($(this).hasClass('current')) {
            $(this).removeClass('current');
        } else {
            $(this).addClass('current');
        }
    });
    $(".closebtn").addClass("d-none");
    $(".item-menu").addClass("d-none");
    $(".openbtn").addClass("animated pulse slow");
});

try {
    document.getElementById("main").style.marginLeft = "50px";
} catch (e) {
    console.log(e.message);
}

function openNav() {
    document.getElementById("mySidenav").style.width = "250px";
    document.getElementById("main").style.marginLeft = "250px";
    $(".closebtn").removeClass("d-none");
    $(".openbtn").addClass("d-none animated fadeIn");
    $(".item-menu").removeClass("d-none");
    $(".item-menu a").addClass("animated fadeIn slow");
    $(".closebtn").addClass("animated rotateIn");
    $(".item-menu-icon").addClass("d-none");
}

function closeNav() {
    document.getElementById("mySidenav").style.width = "50px";
    document.getElementById("main").style.marginLeft = "50px";
    $(".closebtn").addClass("d-none animated fadeIn");
    $(".openbtn").removeClass("d-none");
    $(".item-menu").addClass("d-none");
    $(".item-menu a").removeClass("animated fadeIn slow");
    $(".closebtn").removeClass("animated rotateIn");
    $(".item-menu-icon").removeClass("d-none");
    $(".collapse").removeClass("show");
    $("#mySidenav li span a").removeClass("current");
}