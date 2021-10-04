/*
 * funciones para el alta de la encuesta
*/
(function () {
    "use strict"
    angular.module("app", [])
			.controller("climaController", climaController);

    /* #region Controller */
    function climaController($http, $scope) {
        try {
            var vm = this;
            vm.demo = "Hello from Angular JS";
            vm.listUrl = ["Competencia/GetAll", "Competencia/GetAll_"]
            vm.listCompetencias = [];
            vm.listCategoria = [];

            /* Default functions */
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
            }// fin get()
            vm.post = function (url, objeto, functionOK, mostrarAnimacion) {
                url = vm.baseurl + url;
                $http.post(url, objeto)
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
            }//fin post() 
            function fillArray(url, arreglo, funcion) {
                vm.get(url, function (response) {
                    angular.copy(response, arreglo);
                    if (funcion != null)
                        funcion();
                },
                true);
            }
            vm.ocultarLoad = function () {
                document.getElementsByClassName("busy")[0].classList.add("loadInvisible");
                document.getElementsByClassName("busy")[0].classList.remove("loadVisible");
            }
            vm.mostrarLoad = function () {
                document.getElementsByClassName("busy")[0].classList.remove("loadInvisible");
                document.getElementsByClassName("busy")[0].classList.add("loadVisible");
            }

            vm.baseurl = "http://" + window.location.href.split('/')[2];
            vm.currentUrl = window.location.href.split('/')[3] + "/" + window.location.href.split('/')[4];

            /* Funciones para la administracion de competencias */
            vm.GetCompetencias = function () {
                try {
                    vm.mostrarLoad();
                    fillArray("/Competencia/GetByAdmin?aIdUsuarioCreacion=" + localStorage.getItem("idAdminLog"), vm.listCompetencias, function () {
                        console.log(vm.listCompetencias);
                        vm.ocultarLoad();
                    });
                } catch (aE) {
                    console.log(aE.message);
                }
            }

            vm.AgregarCompetencia = function () {
                try {
                    if (!isNullOrEmpty(document.getElementById("newCompetencia").value)) {
                        vm.mostrarLoad();
                        var competencia = JSON.parse(JSON.stringify(_objCompetencia));
                        competencia.Nombre = document.getElementById("newCompetencia").value;
                        var listCompetencia = JSON.parse(JSON.stringify(_listCompetencias));
                        listCompetencia.push(competencia);
                        vm.post("/Competencia/Add?aUsuarioCreacion=" + localStorage.getItem("nameAdminLog") + "&aIdUsuarioCreacion=" + localStorage.getItem("idAdminLog"), listCompetencia, function (response) {
                            console.log(response.data);
                            if (response.data == 0)
                                swal.fire("La competencia se agregó correctamente", "", "success");
                            if (response.data == 1)
                                swal.fire("Ocurrio un error al guardar la competencia", "", "error");
                            if (typeof (response.data) != "number")
                                swal.fire("Message: " + response.data, "", "info");
                            vm.GetCompetencias();
                            $('.newCompe').hide();
                            document.getElementById("newCompetencia").value = ""
                        });
                    }
                    else {
                        swal.fire("El nombre de la nueva competencia no puede estar vacío", "", "info");
                    }
                } catch (aE) {
                    console.log(aE.message);
                }
            }

            vm.UpdateCompetencia = function (e) {
                try {
                    console.log(e);
                    if (!isNullOrEmpty(e.target.parentNode.children[0].value)) {
                        vm.mostrarLoad();
                        var competencia = JSON.parse(JSON.stringify(_objCompetencia));
                        competencia.IdCompetencia = e.target.parentNode.children[0].attributes.idcompetencia.value;
                        competencia.Nombre = e.target.parentNode.children[0].value;
                        var listCompetencia = JSON.parse(JSON.stringify(_listCompetencias));
                        listCompetencia.push(competencia);
                        vm.post("/Competencia/Update?aUsuarioCreacion=" + localStorage.getItem("nameAdminLog") + "&aIdUsuarioCreacion=" + localStorage.getItem("idAdminLog"), listCompetencia, function (response) {
                            console.log(response.data);
                            if (response.data == 0)
                                swal.fire("La competencia se actualizó correctamente", "", "success");
                            if (response.data == 1)
                                swal.fire("Ocurrio un error al actualizar la competencia", "", "error");
                            if (response.data == 2)
                                swal.fire("No se encontró la competencia a actualizar", "", "error");
                            if (typeof (response.data) != "number")
                                swal.fire("Message: " + response.data, "", "info");
                            vm.GetCompetencias();
                        });
                    }
                    else {
                        swal.fire("El nombre de la nueva competencia no puede estar vacío", "", "info");
                    }
                } catch (aE) {
                    console.log(aE.message);
                }
            }

            vm.DeleteCompetencia = function (e) {
                try {
                    if (!isNullOrEmpty(e.target.parentNode.children[0].attributes.idCompetencia.value)) {
                        console.log(e);
                        vm.mostrarLoad();
                        var competencia = JSON.parse(JSON.stringify(_objCompetencia));
                        competencia.IdCompetencia = e.target.parentNode.children[0].attributes.idCompetencia.value;
                        vm.post("/Competencia/Delete?aUsuarioModificacion=" + localStorage.getItem("nameAdminLog"), competencia, function (response) {
                            console.log(response.data);
                            if (response.data == 0)
                                swal.fire("La competencia se eliminó correctamente", "", "success");
                            if (response.data == 1)
                                swal.fire("Ocurrio un error al eliminar la competencia", "", "error");
                            if (response.data == 2)
                                swal.fire("No se encontró la competencia a eliminar", "", "error");
                            if (typeof (response.data) != "number")
                                swal.fire("Message: " + response.data, "", "info");
                            vm.GetCompetencias();
                        });
                    }
                    else {
                        swal.fire("No fué posible detectar el id de la competencia a eliminar", "", "info");
                    }
                } catch (aE) {
                    console.log(aE.message);
                }
            }

            /* Funciones para la administracion de categorias y subcategorias */
            vm.AgregarCategoria = function () {
                try {
                    if (!isNullOrEmpty(document.getElementById("newCategoria").value)) {
                        vm.mostrarLoad();
                        var competencia = JSON.parse(JSON.stringify(_objCategoria));
                        competencia.Nombre = document.getElementById("newCategoria").value;
                        competencia.Descripcion = document.getElementById("newCategoriaDes").value;
                        var listCompetencia = [];
                        listCompetencia.push(competencia);
                        vm.post("/Competencia/AddCategoria?aUsuarioCreacion=" + localStorage.getItem("nameAdminLog") + "&aIdUsuarioCreacion=" + localStorage.getItem("idAdminLog"), listCompetencia, function (response) {
                            console.log(response.data);
                            if (response.data == 0)
                                swal.fire("La categoria se agregó correctamente", "", "success");
                            if (response.data == 1)
                                swal.fire("Ocurrio un error al guardar la categoria", "", "error");
                            if (typeof (response.data) != "number")
                                swal.fire("Message: " + response.data, "", "info");
                            vm.GetCategorias();
                            $('.newCompe').hide();
                            document.getElementById("newCategoria").value = "";
                            document.getElementById("newCategoriaDes").value = "";
                        });
                    }
                    else {
                        swal.fire("El nombre de la nueva categoria no puede estar vacío", "", "info");
                    }
                } catch (aE) {
                    console.log(aE.message);
                }
            }

            vm.GetCategorias = function () {
                try {
                    vm.mostrarLoad();
                    fillArray("/Competencia/GetCategoriaByAdmin?aIdUsuarioCreacion=" + localStorage.getItem("idAdminLog"), vm.listCategoria, function () {
                        console.log(vm.listCategoria);
                        var htmlContent = "";
                        vm.listCategoria.data.forEach(function (value, index, arr) {
                            if (vm.listCategoria.data[index].IdPadre == 0 || vm.listCategoria.data[index].IdCategoria == vm.listCategoria.data[index].IdPadre) {
                                // crear span
                                if (vm.listCategoria.data[index].IdCategoria < 40)
                                    htmlContent += "<span idCategoria='" + vm.listCategoria.data[index].IdCategoria + "' class='caret'>" + vm.listCategoria.data[index].Nombre + "</span>     <i title='Agregar subcategoria' class='fas fa-plus' style='cursor:pointer'></i>";
                                else
                                    htmlContent += "<span idCategoria='" + vm.listCategoria.data[index].IdCategoria + "' class='caret editable' style='border: 1px solid;'>" + vm.listCategoria.data[index].Nombre + "</span>     <i title='Agregar subcategoria' class='fas fa-plus' style='cursor:pointer'></i>";
                            }
                            // buscar todos los items cuyo id padre es igual al que esta en curso
                            var data = Enumerable.From(vm.listCategoria.data).Where('$.IdPadre ==' + vm.listCategoria.data[index].IdCategoria).ToArray();
                            //if (data.lenght > 0)
                                htmlContent += "<ul class='nested'>";
                            [].forEach.call(data, function (elem) {
                                if (elem.IdCategoria != elem.IdPadre) {
                                    if (elem.IdCategoria >= 40)
                                        htmlContent += "<li class='editable' style='border: 1px solid;' idCategoria='" + elem.IdCategoria + "'>" + elem.Nombre + "   <i ng-click='vm.DeleteCategorias($event)' style='cursor:pointer' class='fas fa-trash' title='Eliminar'></i></li>";
                                    else
                                        htmlContent += "<li idCategoria='" + elem.IdCategoria + "'>" + elem.Nombre + "</li>";
                                }
                            });
                            //if (data.lenght > 0)
                                htmlContent += "</ul>";
                        });
                        console.log(htmlContent);
                        document.getElementsByClassName("mergeContent")[0].innerHTML = htmlContent;

                        // edita categoria
                        $(".editable").dblclick(function (e) {
                            var categoria = JSON.parse(JSON.stringify(_objCategoria));
                            var idCategoriaUpdate = e.target.attributes.idCategoria.value;
                            swal.fire({
                                title: 'Ingresa los nuevos datos subcategoría',
                                //input: 'text',
                                html: '<label class="control-label">Ingresa el nuevo nombre de la nueva subcategoria</label><input class="form-control" id="swal-input1" class="swal2-input">' +
                                      '<label class="control-label">Ingresa la nueva descripción de la nueva subcategoria</label><input class="form-control" id="swal-input2" class="swal2-input">',
                                inputAttributes: {
                                    autocapitalize: 'off'
                                },
                                showCancelButton: true,
                                cancelButtonText: "Cancelar",
                                confirmButtonText: 'Aceptar',
                                showLoaderOnConfirm: true,
                                preConfirm: (login) => {
                                    categoria.IdCategoria = idCategoriaUpdate;
                                    categoria.Nombre = document.getElementById("swal-input1").value;
                                    categoria.Descripcion = document.getElementById("swal-input2").value;
                                    var list = [];
                                    list.push(categoria);
                                    // "Jose Antonio Murillo Garcia?aIdUsuarioCreacion=1"
                                    return fetch(vm.baseurl + "/Competencia/UpdateCategoria?aUsuarioCreacion=" + localStorage.getItem("nameAdminLog") + "&aIdUsuarioCreacion=" + localStorage.getItem("idAdminLog"),
                                        {
                                            method: 'POST', // or 'PUT'
                                            body: JSON.stringify(list), // data can be `string` or {object}!
                                            headers: {
                                                'Content-Type': 'application/json'
                                            }
                                        }
                                        )
                                      .then(response => {
                                          return response.json()
                                      })
                                    .catch(error => {
                                        Swal.showValidationMessage(
                                          `Request failed: ${error}`
                                        )
                                    })
                                },
                                allowOutsideClick: () => !swal.isLoading()
                            }).then((result) => {
                                if (result.isConfirmed) {
                                    if (result.value == 0) {
                                        swal.fire("La subcategoria se actualizó correctamente", "", "success").then(function () {
                                            vm.GetCategorias();
                                        });
                                    }
                                    else {
                                        swal.fire("Ocurrió un error al intentar guardar la subcategoria", "", "error");
                                    }
                                }
                            });
                        })
                        var toggler = document.getElementsByClassName("box");
                        var i;

                        for (i = 0; i < toggler.length; i++) {
                            toggler[i].addEventListener("click", function () {
                                this.parentElement.querySelector(".nested").classList.toggle("active");
                                this.classList.toggle("check-box");
                            });
                        }
                        $("i").click(function (e) {
                            if (e.target.id != "noSwal" && e.target.classList.contains("fa-plus")) {
                                // armar objeto
                                var list = [];
                                var categoria = JSON.parse(JSON.stringify(_objCategoria));
                                categoria.IdPadre = e.target.previousElementSibling.attributes.idCategoria.value;//e.target.parentNode.children[0].attributes.idCategoria.value;
                                swal.fire({
                                    title: 'Ingresa los datos de la nueva subcategoría',
                                    //input: 'text',
                                    html: '<label class="control-label">Ingresa el nombre de la nueva subcategoria</label><input class="form-control" id="swal-input1" class="swal2-input">' +
                                          '<label class="control-label">Ingresa la descripción de la nueva subcategoria</label><input class="form-control" id="swal-input2" class="swal2-input">',
                                    inputAttributes: {
                                        autocapitalize: 'off'
                                    },
                                    showCancelButton: true,
                                    cancelButtonText: "Cancelar",
                                    confirmButtonText: 'Aceptar',
                                    showLoaderOnConfirm: true,
                                    preConfirm: (login) => {
                                        categoria.Nombre = document.getElementById("swal-input1").value;
                                        categoria.Descripcion = document.getElementById("swal-input2").value;
                                        list.push(categoria);
                                        // "Jose Antonio Murillo Garcia?aIdUsuarioCreacion=1"
                                        return fetch(vm.baseurl + "/Competencia/AddCategoria?aUsuarioCreacion=" + localStorage.getItem("nameAdminLog") + "&aIdUsuarioCreacion=" + localStorage.getItem("idAdminLog"),
                                            {
                                                method: 'POST', // or 'PUT'
                                                body: JSON.stringify(list), // data can be `string` or {object}!
                                                headers: {
                                                    'Content-Type': 'application/json'
                                                }
                                            }
                                            )
                                          .then(response => {
                                              return response.json()
                                          })
                                        .catch(error => {
                                            Swal.showValidationMessage(
                                              `Request failed: ${error}`
                                            )
                                        })
                                    },
                                    allowOutsideClick: () => !swal.isLoading()
                                }).then((result) => {
                                    if (result.isConfirmed) {
                                        if (result.value == 0) {
                                            swal.fire("La subcategoria se agregó correctamente", "", "success").then(function () {
                                                vm.GetCategorias();
                                            });
                                        }
                                        else {
                                            swal.fire("Ocurrió un error al intentar guardar la subcategoria", "", "error");
                                        }
                                    }
                                })
                            }
                            if (e.target.classList.contains("fa-trash")) {
                                vm.DeleteCategorias(e)
                            }
                        });
                        vm.ocultarLoad();
                    });
                } catch (aE) {
                    console.log(aE.message);
                }
            }

            vm.DeleteCategorias = function (e) {
                var idCategoriaDelete = e.target.parentNode.attributes.idCategoria.value;
                // string aIdCategoria, string aUsuarioModificacion
                try {
                    if (!isNullOrEmpty(idCategoriaDelete)) {
                        console.log(e);
                        vm.mostrarLoad();
                        vm.post("/Competencia/DeleteCategoria?aUsuarioModificacion=" + localStorage.getItem("nameAdminLog") + "&aIdCategoria=" + idCategoriaDelete, [], function (response) {
                            console.log(response.data);
                            if (response.data == 0)
                                swal.fire("La competencia se eliminó correctamente", "", "success");
                            if (response.data == 1)
                                swal.fire("Ocurrio un error al eliminar la competencia", "", "error");
                            if (response.data == 2)
                                swal.fire("No se encontró la competencia a eliminar", "", "error");
                            if (typeof (response.data) != "number")
                                swal.fire("Message: " + response.data, "", "info");
                            vm.GetCategorias();
                        });
                    }
                    else {
                        swal.fire("No fué posible detectar el id de la competencia a eliminar", "", "info");
                    }
                } catch (aE) {
                    console.log(aE.message);
                }
            }

            vm.previewConfig = function myfunction() {
                var previewData = JSON.parse(localStorage.prevData);
                console.log(previewData);
            }


            $(document).keyup(function (e) {
                if ($("#newCateg").is(":focus") && (e.keyCode == 13)) {
                    vm.GetCategorias();
                }
            });

            function messageBoxError(data, err) {
                console.log(data);
                console.log(err);
            }

            function isNullOrEmpty(data) {
                if (data == undefined || data == null  || data == "")
                    return true;
                else
                    return false;
            }

            if (vm.currentUrl == vm.listUrl[0])
                vm.GetCompetencias();
            if (vm.currentUrl == vm.listUrl[1] || vm.currentUrl == "Competencia/GetAll_#frm")
                vm.GetCategorias();      

        } catch (aE){
            swal.fire(aE.message, "", "error");
        }
    }
})();