(function () {
    "use strinct"
    angular.module("app", []).controller("encuestaController", encuestaController);

    function encuestaController($http, $scope) {
		var vm = this;
		vm.urlApis = "http://" + window.location.href.split('/')[2] + "/Encuesta/";

		vm.get = function (url, functionOK, mostrarAnimacion) {
			if (mostrarAnimacion)
				console.log();
			//vm.isBusy = true;
			url = vm.urlApis + url;
			$http.get(url, { headers: { 'Cache-Control': 'no-cache' } })
				.then(function (response) {
					try {
						if (messageBoxError(response))
							return;
						functionOK(response);
					}
					catch (aE) {
						messageBoxError(aE);
						vm.writeLogFronEnd(aE);
					}
				},
					function (error) {
						//error                    
						messageBoxError(error);
					})
				.finally(function () {
					if (mostrarAnimacion)
						console.log();
					// vm.isBusy = false;
				});
		}// fin get()

		vm.post = function (url, objeto, functionOK, mostrarAnimacion) {
			if (mostrarAnimacion)
				console.log();
			//vm.isBusy = true;
			url = vm.urlApis + url;
			$http.post(url, objeto)
				.then(function (response) {
					try {
						if (messageBoxError(response))
							return;
						functionOK(response);
					}
					catch (aE) {
						messageBoxError(aE);
						vm.writeLogFronEnd(aE);
					}
				},
					function (error) {
						//error                    
						messageBoxError(error);
					})
				.finally(function () {
					if (mostrarAnimacion)
						console.log();
					//vm.isBusy = false;
				});
		}//fin post() 

		function fillArray(url, arreglo, funcion) {
			vm.get(url, function (response) {
				angular.copy(response, arreglo);
				if (funcion != null)
					funcion();
			},
				true);
		}

		vm.EmailsEncuesta = JSON.parse(JSON.stringify(modelEmailsEncuesta));

		vm.sendEmails = function () {
			vm.EmailsEncuesta.Asunto = vm.asunto;
			vm.EmailsEncuesta.Prioridad = vm.prioridad;
			vm.EmailsEncuesta.Template = vm.template;
			vm.EmailsEncuesta.IdEncuesta = vm.encuesta;
			vm.EmailsEncuesta.IdBaseDeDatos = vm.baseDeDatos;
			vm.EmailsEncuesta.TipoEnvio = vm.tipoEnvio;
			vm.EmailsEncuesta.CC = "";
			vm.EmailsEncuesta.CurrentAdmin = localStorage.getItem("nameAdminLog");
			console.log(vm.EmailsEncuesta);

			vm.post("SendEmailsCustom", vm.EmailsEncuesta, function (reponse) {

			});
		}

		$(document).ready(function () {
			$("#email-message").summernote();
		});
    }
})()