document.addEventListener('DOMContentLoaded', function () {
    document.querySelector('form.form.login-form').addEventListener('submit', IniciarSesion);
})

function IniciarSesion(e) {
    e.preventDefault();
    var formulario = e.target;
    console.log(e)

    var email = $(formulario).find('#Email').val();
    var password = $(formulario).find('#Password').val();
    var rememberMe = $(formulario).find('#RememberMe').is(':checked');
    var btnSubmitter = formulario.querySelector('.btn-ingresar');
    $(formulario).find('.login-invalid').addClass('ocultar');
    $(formulario).find('#email-invalido-login').addClass('ocultar');
    $(formulario).find('#password-invalido').addClass('ocultar');
     
    if (!email) {
        $(formulario).find('#email-invalido-login').removeClass('ocultar');
        OcultarBtn(btnSubmitter)
        return;
    }
    if (!password) {
        $(formulario).find('#password-invalido').removeClass('ocultar');
        OcultarBtn(btnSubmitter)
        return;
    }

    var model = {
        Email: email,
        Password: password,
        RememberMe: rememberMe
    }

    console.log('Parametros: ', { model });

    //AjaxLogin
    $.ajax({
        type: 'POST',
        url: "../../Account/AjaxLogin",
        dataType: 'json',
        data: {
            model: model
        },
        success: function (resultado) {
            if (!resultado) {
                $(formulario).find('.login-invalid').removeClass('ocultar');
                OcultarBtn(btnSubmitter)
            } else {
                location.href = '../../Account/IngresoSistema';
            }

            console.log('Inicio de Sesión →', resultado)
        },
        error: function (err) {
            console.error('Ocurrio un error al tratar de inciar sesión', { Error: err });

        }
    });
}


function UsersRegister() {
    $('#login').addClass('ocultar');
    $('#register').removeClass('ocultar');
}

function UsersLogin() {
    $('#login').removeClass('ocultar');
    $('#register').addClass('ocultar');
}

// ESTA FUNCION ES PARA QUE REGISTRE EL USUARIO.
function UsuarioRegister(e) {
    var continuar = true;

    var email = $("#EmailRegistrar").val();
    var emailRegex = /^(([^<>()[\]\.,;:\s@\"]+(\.[^<>()[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;
    //VALIDAR QUE EL EMAIL INGRESADO TENGA EL FORMATO DE UN EMAIL DE ACUERDO A SU ESTRUCTURA
    if (emailRegex.test(email) == false) {
        continuar = true;
        $("#email-invalido").removeClass("ocultar");
    }
    else {
        $("#email-invalido").addClass("ocultar");
    }

    var password = $("#PasswordRegistrar").val();
    //VALIDAR QUE LA CONTRASEÑA CONTENGA 6 DIGITOS O MÁS
    if (password.length < 6) {
        continuar = false;
        $("#contraseña-invalida").removeClass("ocultar");
    }
    else {
        $("#contraseña-invalida").addClass("ocultar");
    }

    var confirmPassword = $("#ConfirmPassword").val();
    //VALIDAR QUE COINCIDAN LAS DOS CONTRASEÑAS
    if (password != confirmPassword) {
        continuar = false;
        $("#contraseña-diferente").removeClass("ocultar");
    }
    else {
        $("#contraseña-diferente").addClass("ocultar");
    }

    var nombreCompleto = $("#NombreCompleto").val();
    //VALIDAR QUE EL NOMBRE NO ESTE VACÍO
    if (nombreCompleto != "" && nombreCompleto != null) {
        $("#nombre-requerido").addClass("ocultar");
    }
    else {
        continuar = false;
        $("#nombre-requerido").removeClass("ocultar");

    }

    var telefono = $("#Telefono").val();
    //VALIDAR QUE EL TELÉFONO NO ESTE VACÍO O SEA 0 (CERO)
    if (telefono != "" && telefono != null && telefono != 0) {
        $("#telefono-requerido").addClass("ocultar");
    }
    else {
        continuar = false;
        $("#telefono-requerido").removeClass("ocultar");
    }

    var documento = $("#Documento").val();
    //VALIDAR QUE EL DOCUMENTO NO ESTE VACÍO O SEA 0 (CERO)
    if (documento != "" && documento != null && documento != 0) {
        $("#documento-requerido").addClass("ocultar");
    }
    else {
        continuar = false;
        $("#documento-requerido").removeClass("ocultar");
    }

    if (continuar == true) {
        OcultarBtn(e.target)
        $.ajax({
            type: 'POST',
            url: "../../Account/GuardarNuevoUsuario",
            dataType: 'json',
            data: { NombreCompleto: nombreCompleto, Telefono: telefono, Documento: documento, Email: email, Password: password, ConfirmPassword: confirmPassword },
            success: function (resultado) {
                if (resultado == 0) {
                    location.href = '../../Account/RegistrarEmpresa';
                }
                //EL EMAIL YA EXISTE EN EL SISTEMA
                if (resultado == 1) {
                    $("#email-existente").removeClass("ocultar");
                    OcultarBtn(e.target)
                }
                //EL CLIENTE YA EXISTE CON ESE DOCUMENTO
                if (resultado == 2) {
                    $("#documento-existente").removeClass("ocultar");
                    OcultarBtn(e.target)
                }
            },
            error: function (data) {
                /*
                * Se ejecuta si la peticón ha sido erronea
                * */
                alert("Problemas al tratar de enviar el formulario");
                location.href = '../../Account/Login';
            }
        });
    }
}


//window.onload = function () {
//    $('[data-toggle="tooltip"]').tooltip();
//};

//document.querySelector('.form .btn-ingresar').addEventListener('click', function (e) { OcultarBtn(e.target) })


function OcultarBtn(btn) {
    btn.classList.toggle('ocultar');
    btn.parentElement.querySelector('.loader').classList.toggle('ocultar');
}