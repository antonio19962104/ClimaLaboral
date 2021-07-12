(function () {
    "use strict"
    angular.module("app", [])
			.controller("documentacionController", documentacionController);

    /* #region Controller */
    function documentacionController($http, $scope) {
        try {
            var vm = this;
            vm.baseurl = "http://" + window.location.href.split('/')[2];
            vm.docBL = [];
            vm.docDL = [];
            vm.docML = [];
            vm.docPL = [];

            vm.get = function (url, functionOK, mostrarAnimacion) {
                url = vm.baseurl + url;
                $http.get(url, { headers: { 'Cache-Control': 'no-cache' } })
					.then(function (response) {
					    try {
					        if (messageBoxError(response.data))
					            return;
					        functionOK(response);
					    }
					    catch (aE) {
					        messageBoxError(aE);
					    }
					},
						function (error) {
						    //error                    
						    messageBoxError(error);
						})
					.finally(function () {
					});
            }

            vm.get("/Documentacion/GetDocumentacion", function (response) {
                vm.docBL = response.data[0];
                vm.docDL = response.data[1];
                vm.docML = response.data[2];
                vm.docPL = response.data[3];
                setTimeout(function () {
                    $("legend").click(function (e) {
                        copy(e.target.parentNode.innerText.replace("Copy\n", ""));
                    });
                }, 200);
            });

            function messageBoxError(s) {

            }
            
        } catch (e) {

        }
    }
})()