// ACA HACEMOS EL FOCO AL CODIGO ALFA.
$("#btn-nueva-persona").click(function () {
    setTimeout("ActivarFoco();", 500);
});
function ActivarFoco() {
    $("#NombreCompleto").focus();
}

window.onload = SeleccionaTipoDocumento();

$("#TipoContribuyenteID").change(function () {
    BuscarTipoDocumento();
});

function BuscarTipoDocumento() {
    //Se limpia el contenido del dropdownlist
    $("#TipoDocumentoID").empty();
    $.ajax({
        type: 'POST',
        //Llamado al metodo GetDepartByGeren en el controlador
        url: '../../Personas/BuscarTipoDocumento',
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
                        className: "botonVerdeSweetAlert",
                        closeModal: true
                    }
                },
            });
        }
    });
    return false;
}

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

//*******************************//***************************************************************//***************************//
//******************************// INICIO de la sección para la OPCION DE AGREGAR NUEVA PERSONA //****************************//
// REGISTRAMOS EL PRODUCTO.
function RegistrarPersonaNueva(guardarIgual = false) {
    $('#btn-espereNuevo').removeClass('ocultar');
    $('#btn-guardarNuevo').addClass('ocultar');
    $('#requerido-NombreCompleto').addClass('ocultar');
    $('#requerido-NroTipoDocumento').addClass('ocultar');
    var nombreCompleto = $('#NombreCompleto').val();
    var nombreFantasia = $('#NombreFantasia').val();
    var tipoPersonaID = $('#TipoPersonaIDCrear').val();
    var tipoContribuyenteID = $('#TipoContribuyenteID').val();
    var tipoDocumentoID = $('#TipoDocumentoID').val();
    var nroTipoDocumento = $('#NroTipoDocumento').val();
    //let zonaID = $("#ZonaIDCrear").val();

    var registrar = true;

    if (nombreCompleto == "") {
        registrar = false;
        $('#requerido-NombreCompleto').removeClass('ocultar');
    }

    if (tipoDocumentoID != 99 && nroTipoDocumento == "") {
        registrar = false;
        $('#requerido-NroTipoDocumento').removeClass('ocultar');
    }

    if (registrar == true) {
        $.ajax({
            type: "POST",
            url: '../../Personas/RegistrarPersonaNueva',
            data: {
                NombreCompleto: nombreCompleto, NombreFantasia: nombreFantasia, TipoPersonaID: tipoPersonaID, TipoContribuyenteID: tipoContribuyenteID,
                TipoDocumentoID: tipoDocumentoID, NroTipoDocumento: nroTipoDocumento, GuardarIgual: guardarIgual
            },
            success: function (resultado) {
                $('#btn-espereNuevo').addClass('ocultar');
                $('#btn-guardarNuevo').removeClass('ocultar');
                if (resultado.ID != 0) {
                    $('#ModalCrearPersona').modal('hide');

                    location.href = "../../Personas/Edit/" + resultado.ID;
                }
                else {
                    swal({
                        title: "ATENCIÓN",
                        text: "Ya existe una persona con el mismo Nº Documento / CUIT. ¿Desea registrarlo de todas formas?",
                        icon: "warning",

                        dangerMode: true,
                        closeOnClickOutside: false,
                        closeOnEsc: false,

                        buttons: {
                            cancel: {
                                text: "No",
                                value: null,
                                visible: true,
                                className: "botonRojoSweetAlert",
                                closeModal: true,
                            },
                            confirm: {
                                text: "Si",
                                value: true,
                                visible: true,
                                className: "botonVerdeSweetAlert",
                                closeModal: true
                            }
                        },
                    })
                        .then((willDelete) => {
                            if (willDelete) {
                                RegistrarPersonaNueva(true);
                            }
                            else {
                                //SI PRESIONA CANCELAR
                                return false;
                            }
                        });
                }
            },
            error: function (result) {
                $('#btn-espereNuevo').addClass('ocultar');
                $('#btn-guardarNuevo').removeClass('ocultar');
                swal({
                    title: "ATENCIÓN",
                    icon: "error",
                    text: "Error al registrar el Persona.",

                    dangerMode: true,
                    closeOnClickOutside: false,
                    closeOnEsc: false,

                    buttons: {
                        confirm: {
                            text: "Aceptar",
                            value: true,
                            visible: true,
                            className: "botonVerdeSweetAlert",
                            closeModal: true
                        }
                    },
                });
            }
        });
    }
    else {
        $('#btn-espereNuevo').addClass('ocultar');
        $('#btn-guardarNuevo').removeClass('ocultar');
    }
}

