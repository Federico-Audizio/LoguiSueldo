function AtajosTeclado({ agregarNuevoElemento, realizarBusqueda }) {
    document.addEventListener('keyup', function (e) {
        //console.log(e.keyCode)
        let keyCode = e.keyCode;
        let altKey = e.altKey;

        //Alt + "+"
        if ((keyCode === 107 || keyCode === 187) && altKey) {
            if (typeof agregarNuevoElemento === 'function') {
                agregarNuevoElemento();
                return;
            }
        }

        //Alt + "b"
        if (keyCode === 66 && altKey) {
            if (typeof realizarBusqueda === 'function') {
                realizarBusqueda();
                return;
            }
        }
    });
}