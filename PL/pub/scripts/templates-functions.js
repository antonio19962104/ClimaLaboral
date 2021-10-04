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

function openNav(e) {
    document.getElementById("mySidenav").style.width = "250px";
    document.getElementById("main").style.marginLeft = "250px";
    $(".closebtn").removeClass("d-none");
    $(".closebtn").css("display", "")
    $(".openbtn").addClass("animated fadeIn");
    $(".openbtn").css("display", "none")
    $(".item-menu").removeClass("d-none");
    $(".item-menu").css("display", "")
    $(".item-menu a").addClass("animated fadeIn slow");
    $(".closebtn").addClass("animated rotateIn");
    //$(".item-menu-icon").addClass("d-none");
    $(".item-menu-icon").css("display", "none")

    try {
        switch (e.attributes.href.value) {
            case "#collapseSurvey":
                document.getElementById("collapseTemplates").classList.remove("show");
                document.getElementById("collapseReports").classList.remove("show");
                document.getElementById("collapseDatabase").classList.remove("show");
                document.getElementById("collapseUsers").classList.remove("show");
                document.getElementById("collapseEnterprises").classList.remove("show");
                break;
            case "#collapseTemplates":
                document.getElementById("collapseSurvey").classList.remove("show");
                document.getElementById("collapseReports").classList.remove("show");
                document.getElementById("collapseDatabase").classList.remove("show");
                document.getElementById("collapseUsers").classList.remove("show");
                document.getElementById("collapseEnterprises").classList.remove("show");
                break;
            case "#collapseReports":
                document.getElementById("collapseSurvey").classList.remove("show");
                document.getElementById("collapseTemplates").classList.remove("show");
                document.getElementById("collapseDatabase").classList.remove("show");
                document.getElementById("collapseUsers").classList.remove("show");
                document.getElementById("collapseEnterprises").classList.remove("show");
                break;
            case "#collapseDatabase":
                document.getElementById("collapseSurvey").classList.remove("show");
                document.getElementById("collapseTemplates").classList.remove("show");
                document.getElementById("collapseReports").classList.remove("show");
                document.getElementById("collapseUsers").classList.remove("show");
                document.getElementById("collapseEnterprises").classList.remove("show");
                break;
            case "#collapseUsers":
                document.getElementById("collapseSurvey").classList.remove("show");
                document.getElementById("collapseTemplates").classList.remove("show");
                document.getElementById("collapseReports").classList.remove("show");
                document.getElementById("collapseDatabase").classList.remove("show");
                document.getElementById("collapseEnterprises").classList.remove("show");
                break;
            case "#collapseEnterprises":
                document.getElementById("collapseSurvey").classList.remove("show");
                document.getElementById("collapseTemplates").classList.remove("show");
                document.getElementById("collapseReports").classList.remove("show");
                document.getElementById("collapseDatabase").classList.remove("show");
                document.getElementById("collapseUsers").classList.remove("show");
                break;
            default:
        }
    } catch (e) {

    }
}

function closeNav() {
    document.getElementById("mySidenav").style.width = "50px";
    document.getElementById("main").style.marginLeft = "50px";
    $(".closebtn").addClass("animated fadeIn");
    $(".closebtn").css("display", "none")
    $(".openbtn").removeClass("d-none");
    $(".openbtn").css("display", "")
    //$(".item-menu").addClass("d-none");
    $(".item-menu").css("display", "none")
    $(".item-menu a").removeClass("animated fadeIn slow");
    $(".closebtn").removeClass("animated rotateIn");
    $(".item-menu-icon").removeClass("d-none");
    $(".item-menu-icon").css("display", "")
    $(".item-menu-icon").css("display", "")
    $(".collapse").removeClass("show");
    $("#mySidenav li span a").removeClass("current");
}