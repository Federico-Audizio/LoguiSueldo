function marcar(source) {
    checkboxes = document.getElementsByTagName('input'); //obtenemos todos los controles del tipo Input
    for (i = 0; i < checkboxes.length; i++) //recoremos todos los controles
    {
        if (checkboxes[i].type == "checkbox" && checkboxes[i].name == "ProductosSeleccionados") //solo si es un checkbox entramos
        {
            checkboxes[i].checked = source.checked; //si es un checkbox le damos el valor del checkbox que lo llamó (Marcar/Desmarcar Todos)
        }
    }
    if (source.checked == false) {
        $("#btn-submit").addClass("ocultar");
    }
    else {
        $("#btn-submit").removeClass("ocultar");
    }
}

function ContarCheck() { //AL HACER CLICK EN EL CHECK DE CUERPO DE TABLA
    var contador = 0;
    // Recorremos todos los checkbox para contar los que estan seleccionados
    $("input[name=ProductosSeleccionados]").each(function () {
        if ($(this).is(":checked"))
            contador++;
    });
    if (contador > 0) {
        $("#btn-submit").removeClass("ocultar");
    }
    else {
        $("#btn-submit").addClass("ocultar");
    }

    var cantidadCheckBox = document.getElementsByName("ProductosSeleccionados").length;
    //console.log("Hay " + cantidadCheckBox + " elementos");
    //console.log("Hay " + contador + " contados");
    if (cantidadCheckBox == contador) {
        document.getElementById("checkboxTodos").checked = true;
    }
    else {
        document.getElementById("checkboxTodos").checked = false;
    }
}