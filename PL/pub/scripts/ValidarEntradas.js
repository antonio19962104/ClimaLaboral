function validarCamposa() {
    var TxtNombreAdmin = $('#TxtNombreAdmin').val();
    var Perfil = $('#DDLPerfilD4U').val();
    var Estatus = $('#Estatus').val();

    if (TxtNombreAdmin == null || Perfil == 0 || Estatus == 0) {
        $("#ErrorMessage").text('Debes llenar todos los campos');
        return false;
    }
}

$("#DDLPerfilD4U").change(function () {
    var PerfilD4U = $('#DDLPerfilD4U').val();

    if (PerfilD4U == 0) {
        $('#ModuloEncuestas').css('display', 'none');
        $('#ModuloPlantillas').css('display', 'none');
        $('#ModuloReportes').css('display', 'none');
        $('#ModuloBD').css('display', 'none');
        $('#Modulousuarios').css('display', 'none');
        $('#ModuloEmpresas').css('display', 'none');
    }
    if (PerfilD4U == 1) {
        $('#ModuloEncuestas').css('display', 'block');
        $('#ModuloPlantillas').css('display', 'block');
        $('#ModuloReportes').css('display', 'block');
        $('#ModuloBD').css('display', 'block');
        $('#Modulousuarios').css('display', 'block');
        $('#ModuloEmpresas').css('display', 'none');
    }
    if (PerfilD4U == 2) {
        $('#ModuloEncuestas').css('display', 'none');
        $('#ModuloPlantillas').css('display', 'none');
        $('#ModuloReportes').css('display', 'block');
        $('#ModuloBD').css('display', 'none');
        $('#Modulousuarios').css('display', 'none');
        $('#ModuloEmpresas').css('display', 'none');
    }
    if (PerfilD4U == 3) {
        $('#ModuloEncuestas').css('display', 'none');
        $('#ModuloPlantillas').css('display', 'block');
        $('#ModuloReportes').css('display', 'none');
        $('#ModuloBD').css('display', 'none');
        $('#Modulousuarios').css('display', 'none');
        $('#ModuloEmpresas').css('display', 'none');
    }
    if (PerfilD4U == 4) {
        $('#ModuloEncuestas').css('display', 'block');
        $('#ModuloPlantillas').css('display', 'none');
        $('#ModuloReportes').css('display', 'none');
        $('#ModuloBD').css('display', 'none');
        $('#Modulousuarios').css('display', 'none');
        $('#ModuloEmpresas').css('display', 'none');
    }
    if (PerfilD4U == 5) {
        $('#ModuloEncuestas').css('display', 'none');
        $('#ModuloPlantillas').css('display', 'none');
        $('#ModuloReportes').css('display', 'none');
        $('#ModuloBD').css('display', 'block');
        $('#Modulousuarios').css('display', 'none');
        $('#ModuloEmpresas').css('display', 'none');
    }
    if (PerfilD4U == 6) {
        $('#ModuloEncuestas').css('display', 'none');
        $('#ModuloPlantillas').css('display', 'none');
        $('#ModuloReportes').css('display', 'none');
        $('#ModuloBD').css('display', 'none');
        $('#Modulousuarios').css('display', 'block');
        $('#ModuloEmpresas').css('display', 'none');
    }
    if (PerfilD4U == 7) {
        $('#ModuloEncuestas').css('display', 'none');
        $('#ModuloPlantillas').css('display', 'none');
        $('#ModuloReportes').css('display', 'none');
        $('#ModuloBD').css('display', 'none');
        $('#Modulousuarios').css('display', 'none');
        $('#ModuloEmpresas').css('display', 'block');
    }
});

function verPermisos() {

    //var IdEmpleado = @Session["IdEmpleadoForNewAdmin"];
    var IdPerfil = $('#DDLPerfilD4U').val();
    var Estatus = $('#Estatus').val();

    var ListarEncuesta = $('#chkEncuestasListar').val();
    var CrearEncuesta = $('#chkEncuestasCrear').val();
    var OpcEncuesta = $('#chkEncuestasOpc').val();

    var ListarPlantilla = $('#chkPlantillasListar').val();
    var CrearPlantilla = $('#chkPlantillasCrear').val();
    var OpcPlantilla = $('#chkPlantillasOpc').val();

    var ListarReporte = $('#chkReportesListar').val();
    var CrearReporte = $('#chkReportesCrear').val();
    var OpcReporte = $('#chkReportesOpc').val();
    
    var ListarEncuesta = $('#chkEncuestasListar').val();
    var CrearEncuesta = $('#chkEncuestasCrear').val();
    var OpcEncuesta = $('#chkEncuestasOpc').val();

    var ListarBD = $('#chkBDListar').val();
    var CrearBD = $('#chkBDCrear').val();
    var OpcBD = $('#chkBDOpc').val();

    var ListarUsuario = $('#chkUsuariosListar').val();
    var CrearUsuario = $('#chkUsuariosCrear').val();
    var OpcUsuario = $('#chkUsuariosOpc').val();

    var ListarEmpresa = $('#chkEmpresasListar').val();
    var CrearEmpresa = $('#chkEmpresasCrear').val();
    var OpcEmpresa = $('#chkEmpresasOpc').val();




}