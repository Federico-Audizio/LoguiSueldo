/* Autocomplete de producto, jquery UI */
var personaRealID = $('#HiddenPersonaID').val();

window.onload = MostrarOcultarBotonesCtaCte();

$("#TipoPersonaID").change(function () {
    var permisoUsuarioLogueado = $("#PermisoUsuarioLogueado").val();
    if (permisoUsuarioLogueado != 3) {

        var tipoPersonaIDOriginal = $("#TipoPersonaIDOriginal").val();

        var saldoCliente = $("#SaldoCliente").val();
        saldoCliente = saldoCliente.replace(/\,/g, '');
        saldoCliente = parseFloat(saldoCliente);
        var saldoProveedor = $("#SaldoProveedor").val();
        saldoProveedor = saldoProveedor.replace(/\,/g, '');
        saldoProveedor = parseFloat(saldoProveedor);

        var productosProveedor = $("#ProductosProveedor").val();

        //VALIDAR QUE SI SELECCIONA CLIENTE SOLO, NO DEBE TENER SALDO PROVEEDOR
        var tipoPersonaID = $("#TipoPersonaID").val();

        //SIN SELECCION
        if (tipoPersonaID == 1) {
            if (saldoCliente != 0 || saldoProveedor != 0) {
                swal({
                    title: "ATENCIÓN",
                    icon: "error",
                    text: "La Persona NO debe tener saldo Cliente ni Proveedor para poder hacer asignarle Sin Selección.",

                    dangerMode: true,
                    closeOnClickOutside: false,
                    closeOnEsc: false,

                    buttons: {
                        confirm: {
                            text: "Aceptar",
                            value: true,
                            visible: true,
                            className: "",
                            closeModal: true
                        }
                    },
                });
                $("#TipoPersonaID").val(tipoPersonaIDOriginal);
            }
            else {
                if (productosProveedor > 0) {
                    swal({
                        title: "ATENCIÓN",
                        icon: "error",
                        text: "La Persona NO debe estar asignada a ningún producto para poder hacer asignarle Sin Selección.",

                        dangerMode: true,
                        closeOnClickOutside: false,
                        closeOnEsc: false,

                        buttons: {
                            confirm: {
                                text: "Aceptar",
                                value: true,
                                visible: true,
                                className: "",
                                closeModal: true
                            }
                        },
                    });
                    $("#TipoPersonaID").val(tipoPersonaIDOriginal);
                }
            }
        }

        //SI ES SOLO CLIENTE
        if (tipoPersonaID == 2) {
            if (saldoProveedor != 0) {
                swal({
                    title: "ATENCIÓN",
                    icon: "error",
                    text: "Para poder configurar a esta Persona sólo como Cliente, NO debe tener saldo Proveedor.",

                    dangerMode: true,
                    closeOnClickOutside: false,
                    closeOnEsc: false,

                    buttons: {
                        confirm: {
                            text: "Aceptar",
                            value: true,
                            visible: true,
                            className: "",
                            closeModal: true
                        }
                    },
                });
                $("#TipoPersonaID").val(tipoPersonaIDOriginal);
            }
            else {
                if (productosProveedor > 0) {
                    swal({
                        title: "ATENCIÓN",
                        icon: "error",
                        text: "La Persona NO debe estar asignada a ningún producto para poder hacer asignarle Sin Selección.",

                        dangerMode: true,
                        closeOnClickOutside: false,
                        closeOnEsc: false,

                        buttons: {
                            confirm: {
                                text: "Aceptar",
                                value: true,
                                visible: true,
                                className: "",
                                closeModal: true
                            }
                        },
                    });
                    $("#TipoPersonaID").val(tipoPersonaIDOriginal);
                }
            }
        }

        //SI ES SOLO PROVEEDOR
        if (tipoPersonaID == 3) {
            if (saldoCliente != 0) {
                swal({
                    title: "ATENCIÓN",
                    icon: "error",
                    text: "Para poder configurar a esta Persona sólo como Proveedor, NO debe tener saldo Cliente.",

                    dangerMode: true,
                    closeOnClickOutside: false,
                    closeOnEsc: false,

                    buttons: {
                        confirm: {
                            text: "Aceptar",
                            value: true,
                            visible: true,
                            className: "",
                            closeModal: true
                        }
                    },
                });
                $("#TipoPersonaID").val(tipoPersonaIDOriginal);
            }
        }
        MostrarOcultarBotonesCtaCte();
    }
});

function MostrarOcultarBotonesCtaCte() {
    var permisoUsuarioLogueado = $("#PermisoUsuarioLogueado").val();
    if (permisoUsuarioLogueado != 3) {
        var tipoPersonaID = $("#TipoPersonaID").val();
        //SIN SELECCIÓN
        if (tipoPersonaID == 1) {
            $("#BtnCtaCteCliente").addClass("ocultar");
            $("#BtnCtaCteProveedor").addClass("ocultar");
        }
        //SI ES SOLO CLIENTE
        if (tipoPersonaID == 2) {
            $("#BtnCtaCteCliente").removeClass("ocultar");
            $("#BtnCtaCteProveedor").addClass("ocultar");
        }
        //SI ES SOLO PROVEEDOR
        if (tipoPersonaID == 3) {
            $("#BtnCtaCteCliente").addClass("ocultar");
            $("#BtnCtaCteProveedor").removeClass("ocultar");
        }
        //SI ES CLIENTE Y PROVEEDOR
        if (tipoPersonaID == 4) {
            $("#BtnCtaCteCliente").removeClass("ocultar");
            $("#BtnCtaCteProveedor").removeClass("ocultar");
        }
    }
}




$('#agregar-usuario').click(function () {
    $('#div-btn-agregarusuario').addClass('ocultarCarga');
    $('#form-agregar-usuario').removeClass('ocultarCarga');
    $('#Div-Contraseña').removeClass('ocultarCarga');
    $('#Div-Email').removeClass('col-md-6');
    $('#Div-Email').addClass('col-md-4');
    $('#Div-PermisoID').removeClass('col-md-6');
    $('#Div-PermisoID').addClass('col-md-4');
    $('#AgregarUsuario').val(1);
});

$('#agregar-usuario-existente').click(function () {
    $('#div-btn-agregarusuario').addClass('ocultarCarga');
    $('#form-agregar-usuario').removeClass('ocultarCarga');
    $('#Div-Contraseña').addClass('ocultarCarga');
    $('#Div-Email').removeClass('col-md-4');
    $('#Div-Email').addClass('col-md-6');
    $('#Div-PermisoID').removeClass('col-md-4');
    $('#Div-PermisoID').addClass('col-md-6');
    $('#AgregarUsuario').val(2);
});

$('#btn-agregarusuario-Eliminar').click(function () {
    swal({
        title: "ATENCIÓN",
        text: "¿Está SEGURO de desvincular el Usuario de esta Persona? Si desvincula este Usuario las Configuraciones relacionadas al mismo se perderán.",
        icon: "warning",

        dangerMode: true,
        closeOnClickOutside: false,
        closeOnEsc: false,

        buttons: {
            cancel: {
                text: "Cancelar",
                value: null,
                visible: true,
                className: "",
                closeModal: true,
            },
            confirm: {
                text: "Aceptar",
                value: true,
                visible: true,
                className: "",
                closeModal: true
            }
        },
    })
        .then((willDelete) => {
            if (willDelete) {
                EstadoPermisoUsuario(true);
            }
            else {
                //txt = "You pressed Cancel!";
                return false;
            }
        });

});

$('#btn-agregarusuario-Vincular').click(function () {
    swal({
        title: "ATENCIÓN",
        text: "¿Está SEGURO de vincular nuevamente el Usuario de esta Persona?",
        icon: "warning",

        dangerMode: true,
        closeOnClickOutside: false,
        closeOnEsc: false,

        buttons: {
            cancel: {
                text: "Cancelar",
                value: null,
                visible: true,
                className: "",
                closeModal: true,
            },
            confirm: {
                text: "Aceptar",
                value: true,
                visible: true,
                className: "",
                closeModal: true
            }
        },
    })
        .then((willDelete) => {
            if (willDelete) {
                EstadoPermisoUsuario(false);
            }
            else {
                //txt = "You pressed Cancel!";
                return false;
            }
        });
});

function EstadoPermisoUsuario(Desvinculado) {
    var Email = $('#email-usuario').val();
    $.ajax({
        type: "POST",
        url: '@Url.Action("EstadoPermisoUsuario")',
        data: { Email: Email, PersonaID: personaRealID, Desvinculado: Desvinculado },
        success: function (resultado) {
            if (resultado == 5) {
                $('#error-email').removeClass('ocultarCarga');
                $('#error-email').append("*Su plan ha llegado al límite de Usuarios.");
            }
            else {
                cargarDatosUsuarios(Email = "", PermisoID = 0, Contrasenia = "", 0);
            }
        },
        error: function (result) {
            swal({
                title: "ATENCIÓN",
                icon: "error",
                text: "No se pudo eliminar el usuario.",

                dangerMode: true,
                closeOnClickOutside: false,
                closeOnEsc: false,

                buttons: {
                    confirm: {
                        text: "Aceptar",
                        value: true,
                        visible: true,
                        className: "",
                        closeModal: true
                    }
                },
            });
            //alert("No se pudo eliminar el usuario.");
        }
    });
}

$('#btn-agregarusuario-cancelar').click(function () {
    $('#div-btn-agregarusuario').removeClass('ocultarCarga');

    $("#email-usuario").val('');
    $('#error-email').empty();

    $("#contraseña-usuario").val('');
    $('#requerido-contraseña').addClass('ocultarCarga');

    $('#corto-contraseña').addClass('ocultarCarga');
    $('#longitud-minima').removeClass('ocultarCarga');

    $('#form-agregar-usuario').addClass('ocultarCarga');
});

$('#btn-agregarusuario-guardar').click(function () {

    var Email = $('#email-usuario').val();
    var PermisoID = $('#PermisoID').val();
    var Contrasenia = $('#contraseña-usuario').val();
    var UsuarioNuevoExistente = $('#AgregarUsuario').val();

    if (UsuarioNuevoExistente == 1 || UsuarioNuevoExistente == 2) {
        if (Email != "") {

            $('#error-email').empty();
            $('#error-email').addClass('ocultarCarga');

            if (UsuarioNuevoExistente == 2) {//USUARIO EXISTENTE
                cargarDatosUsuarios(Email, PermisoID, Contrasenia, UsuarioNuevoExistente);
            }
            else {//USUARIO NUEVO
                if (Contrasenia != "") {
                    $('#requerido-contraseña').addClass('ocultarCarga');

                    if (Contrasenia.length < 6 && UsuarioNuevoExistente == 1) {
                        $('#corto-contraseña').removeClass('ocultarCarga');
                        $('#longitud-minima').addClass('ocultarCarga');
                    }
                    else {
                        cargarDatosUsuarios(Email, PermisoID, Contrasenia, UsuarioNuevoExistente);
                    }

                }
                else {
                    $('#requerido-contraseña').removeClass('ocultarCarga');
                }
            }
        }
        else {
            $('#error-email').removeClass('ocultarCarga');
            $('#error-email').append("*El Email es Requerido");
        }
    }
});

$("#PermisoID").change(function () {
    $('#btn-agregarusuario-guardar').removeClass('ocultarCarga');
});

function cargarDatosUsuarios(Email, PermisoID, Contrasenia, UsuarioNuevoExistente) {
    MostrarCarga();
    $.ajax({
        type: "POST",
        url: '@Url.Action("GuardarDatosUsuario")',
        data: { Email: Email, PermisoID: PermisoID, Contraseña: Contrasenia, PersonaID: personaRealID, UsuarioNuevoExistente: UsuarioNuevoExistente },
        success: function (data) {

            $('#error-email').empty();

            $('#requerido-contraseña').addClass('ocultarCarga');
            $('#corto-contraseña').addClass('ocultarCarga');
            $('#longitud-minima').removeClass('ocultarCarga');

            if (data.Resultado == 0) {
                $('#error-email').addClass('ocultarCarga');
                $('#email-usuario').val(data.Email);
                //$('#contraseña-usuario').val(data.Contraseña);

                if (data.Email != "") {
                    //bloquear escritura de email y contraseña.
                    document.getElementById('email-usuario').readOnly = true;

                    if (data.PermisoID != 0) {
                        $("#PermisoID option[value=" + data.PermisoID + "]").attr("selected", true);
                    }
                    //volvemos a abrir ese formulario
                    document.getElementById("agregar-usuario-existente").click();
                    $('#AgregarUsuario').val(2);

                    //al tener datos ocultamos el boton cancelar
                    $('#btn-agregarusuario-cancelar').addClass('ocultarCarga');

                    $("#pestaña-credenciales").css("display", "initial");
                    //debemos mostrar el boton eliminar usuario

                    if (data.Desvinculado == false) {
                        if (data.Editar == false) {
                            $('#PermisoID').attr("disabled", true);
                        }
                        else {
                            $('#PermisoID').attr("disabled", false);
                        }
                        if (data.Eliminar == true) {
                            //MOSTRAR BOTON ELIMINAR
                            $('#btn-agregarusuario-Eliminar').removeClass('ocultarCarga');
                            $('#btn-agregarusuario-Vincular').addClass('ocultarCarga');
                        }

                        $('#email-usuario').removeClass('bordeRojoImporte');
                        $('#email-usuario').removeClass('importeRojo');

                        $('#PermisoID').removeClass('bordeRojoImporte');
                        $('#PermisoID').removeClass('importeRojo');
                    }
                    else {
                        //MOSTRAR BOTON VINCULAR
                        $('#email-usuario').addClass('bordeRojoImporte');
                        $('#email-usuario').addClass('importeRojo');
                        $('#PermisoID').attr("disabled", true);
                        $('#PermisoID').addClass('bordeRojoImporte');
                        $('#PermisoID').addClass('importeRojo');
                        $('#btn-agregarusuario-Eliminar').addClass('ocultarCarga');
                        $('#btn-agregarusuario-Vincular').removeClass('ocultarCarga');
                    }

                    $('#longitud-minima').addClass('ocultarCarga');

                    $('#btn-agregarusuario-guardar').addClass('ocultarCarga');
                }
            }
            if (data.Resultado == 1) {
                $('#error-email').removeClass('ocultarCarga');
                $('#error-email').append("*Formato de Email no válido.");
            }
            if (data.Resultado == 2) {
                $('#error-email').removeClass('ocultarCarga');
                $('#error-email').append("*El Email ingresado ya se encuentra relacionado con otra persona de esta Empresa.");
            }
            if (data.Resultado == 3) {
                $('#error-email').removeClass('ocultarCarga');
                $('#error-email').append("*El Email ingresado ya existe.");
            }
            if (data.Resultado == 4) {
                $('#error-email').removeClass('ocultarCarga');
                $('#error-email').append("*El Email ingresado no existe.");
            }
            if (data.Resultado == 5) {
                $('#error-email').removeClass('ocultarCarga');
                $('#error-email').append("*Su plan ha llegado al límite de Usuarios.");
            }

            var permisoUsuarioLogueado = $("#PermisoUsuarioLogueado").val();
            if (permisoUsuarioLogueado != 1) {
                $('#div-datos-usuarios').addClass('ocultarCarga');
                $('#mensaje-datos-usuarios').removeClass('ocultarCarga');
            }
            else {
                $('#div-datos-usuarios').removeClass('ocultarCarga');
                $('#mensaje-datos-usuarios').addClass('ocultarCarga');
            }

            setTimeout("detectarCarga();", 500);
        }
    });
}
//fin datos usuario

window.onload = cargarDatosUsuarios(Email = "", PermisoID = 0, Contrasenia = "", 0);

$("#LocalidadNombre").autocomplete({
    dataType: 'JSON',
    source: function (request, response) {
        jQuery.ajax({
            url: '/Localidades/BuscarLocalidad',
            type: "post",
            dataType: "json",
            data: {
                nombre: request.term
            },
            success: function (data) {
                response($.map(data, function (item) {
                    return {
                        id: item.LocalidadID,
                        value: item.NombreVista
                    }
                }))
            }
        })
    },
    select: function (e, ui) {
        $("#LocalidadID").val(ui.item.id);
    }
});

window.onload = SeleccionaTipoDocumento();

$("#TipoContribuyenteID").change(function () {
    BuscarTipoDocumento();
});

function BuscarTipoDocumento() {
    ActivarDesactivarDatosFiscales();
    //Se limpia el contenido del dropdownlist
    $("#TipoDocumentoID").empty();
    $.ajax({
        type: 'POST',
        //Llamado al metodo GetDepartByGeren en el controlador
        url: '@Url.Action("BuscarTipoDocumento")',
        dataType: 'json',
        //Parametros que se envian al metodo del controlador
        data: { id: $("#TipoContribuyenteID").val() },
        //En caso de resultado exitoso
        success: function (TipoDocumentos) {
            if (TipoDocumentos.length == 0) {
                $("#TipoDocumentoID").append('<option value="0">[No hay Tipo de documentos registrados]</option>');
            }
            else {
                $.each(TipoDocumentos, function (i, TipoDocumento) {
                    $("#TipoDocumentoID").append('<option value="' + TipoDocumento.Value + '">' +
                        TipoDocumento.Text + '</option>');
                });
                SeleccionaTipoDocumento();
            }
        },
        ////Mensaje de error en caso de fallo
        error: function (ex) {
            swal({
                title: "ATENCIÓN",
                icon: "error",
                text: "Fallo la busqueda de Tipo de documentos." + ex,

                dangerMode: true,
                closeOnClickOutside: false,
                closeOnEsc: false,

                buttons: {
                    confirm: {
                        text: "Aceptar",
                        value: true,
                        visible: true,
                        className: "",
                        closeModal: true
                    }
                },
            });
            //alert('Fallo la busqueda de Tipo de documentos.' + ex);
        }
    });
    return false;
};

$("#TipoDocumentoID").change(function () {
    SeleccionaTipoDocumento();
});

function SeleccionaTipoDocumento() {
    //BLOQUEAR NROTIPODOCUMENTO O NO
    var tipoDocumentoID = $("#TipoDocumentoID").val();
    if (tipoDocumentoID == 99) {
        $("#NroTipoDocumento").attr('disabled', 'true');
    }
    else {
        $("#NroTipoDocumento").removeAttr('disabled', 'true');
        DefinirMascara();
    }
};

function DefinirMascara() {
    var tipoDocumentoID = $("#TipoDocumentoID").val();
    if (tipoDocumentoID == 96) {
        // Definimos las mascaras para cada input
        $("#NroTipoDocumento").mask("99999999");
    }
    else {
        // Definimos las mascaras para cada input
        $("#NroTipoDocumento").mask("99-99999999-9");
    }
}

function ActivarDesactivarDatosFiscales() {
    let tipoContribuyenteID = $("#TipoContribuyenteID").val();
    if (tipoContribuyenteID == 1) {
        $("#pestañaDatosFiscales").removeClass("ocultar");
    }
    else {
        $("#pestañaDatosFiscales").addClass("ocultar");
    }
}

function BuscarExencionesTributo() {
    ActivarDesactivarDatosFiscales();
    var personaRealID = $('#HiddenPersonaID').val();
    $("#tbody-exencion-tributos").empty();
    $.ajax({
        type: "POST",
        url: '../../Personas/BuscarExencionesTributo',
        data: { PersonaID: personaRealID },
        success: function (exenciones) {
            $("#tbody-exencion-tributos").empty();

            $.each(exenciones, function (index, item) {

                $("#tbody-exencion-tributos").append("<tr class='altoBotonesTabla'>" +
                    "<td>" + item.TributoNombre + "</td>" +
                    "<td class='text-center'>" + item.ExencionDesdeString + "</td>" +
                    "<td class='text-center'>" + item.ExencionHastaString + "</td>" +
                    "<td class='tdBotones text-center'><a onclick='EliminarExencionTributo(" + item.ExencionTributoPersonaID + ")' class='btn-tabla' title='Eliminar Exención Tributo'><i class='fa fa-trash'></i></a></td>" +
                    "</tr>");

            });
        },
        error: function (result) {

        }
    });
}

function GuardarExencionTributo() {
    let fechaExencionDesde = $("#DatePickerFactura1").val();
    let fechaExencionHasta = $("#DatePickerFactura2").val();
    let otroTributoID = $("#OtroTributoID").val();
    let personaRealID = $('#HiddenPersonaID').val();
    $("#btn-GuardarExencionTributo").addClass("ocultar");
    $.ajax({
        type: "POST",
        url: '../../Personas/GuardarExencionTributo',
        data: { PersonaID: personaRealID, OtroTributoID: otroTributoID, Desde: fechaExencionDesde, Hasta: fechaExencionHasta },
        success: function (resultado) {
            $("#btn-GuardarExencionTributo").removeClass("ocultar");
            BuscarExencionesTributo();
        },
        error: function (result) {
            $("#btn-GuardarExencionTributo").removeClass("ocultar");
        }
    });
}

function EliminarExencionTributo(ExencionTributoPersonaID) {
    swal({
        title: "ATENCIÓN",
        text: "¿Está SEGURO de eliminar este Registro?",
        icon: "warning",

        dangerMode: true,
        closeOnClickOutside: false,
        closeOnEsc: false,

        buttons: {
            cancel: {
                text: "Cancelar",
                value: null,
                visible: true,
                className: "",
                closeModal: true,
            },
            confirm: {
                text: "Aceptar",
                value: true,
                visible: true,
                className: "",
                closeModal: true
            }
        },
    })
        .then((willDelete) => {
            if (willDelete) {
                let personaRealID = $('#HiddenPersonaID').val();
                $.ajax({
                    type: "POST",
                    url: '../../Personas/EliminarExencionTributo',
                    data: { PersonaID: personaRealID, ExencionTributoPersonaID: ExencionTributoPersonaID },
                    success: function (resultado) {
                        BuscarExencionesTributo();
                    },
                    error: function (result) {

                    }
                });
            }
            else {
                //txt = "You pressed Cancel!";
                return false;
            }
        });

}