
function BusquedaTabla({ elemento, tabla, columnas = [], fnBotonAgregar }) {
    try {

        if (!elemento && !tabla) {
            throw 'Campos "elemento" y "tabla" no especificados.';
        }

        //BOTÓN EN LA BARRA DE BUSQUEDA PARA AGREGAR UN NUEVO ELEMENTO
        const BotonAgregar = function (elemento, fn) {

            if (typeof fn !== 'function') {
                return false;
            }

            var existe = function () {
                let btn = elemento.querySelector('.agregar-busqueda');
                if (btn) {
                    return btn;
                }
                return false;
            }

            //AGREGAR EL OTOÓN EN LA BARRA DE BÚSQUEDA
            var agregar = function () {
                let btnExiste = existe();
                if (!btnExiste) {
                    var btn = document.createElement('a');
                    btn.classList.add('agregar-busqueda');
                    btn.innerHTML = '<i class="fa fa-plus"></i> Agregar';
                    btn.addEventListener('click', fn);

                    elemento.appendChild(btn);
                }
            }

            //QUITAR EL BOTÓN EN LA BARRA DE BÚSQUEDA
            var quitar = function () {
                let btnExiste = existe();
                if (btnExiste) {
                    elemento.removeChild(btnExiste);
                }
            }

            return { agregar, quitar }

        }

        //FILTRAR EN LA TABLA MIENTRAS SE ESCRIBE EN LA BARRA DE BÚSQUEDA
        const Filtrar = function (tabla, filtro) {
            let filas = Array.from(tabla.querySelectorAll('tbody tr'));

            filas.forEach(fila => fila.classList.add('ocultar'));

            let filasMostrar = filas.filter(fila => {
                var columnas =
                    Array.from(fila.children)
                        .map(c => c.textContent.trim())
                        .filter(c => c.toLowerCase().includes(filtro.toLowerCase()))

                if (columnas.length) {
                    return fila;
                }
            });

            filasMostrar.forEach(fila => fila.classList.remove('ocultar'));

            return filasMostrar.length > 0;
        }

        elemento.style.position = 'relative';
        const inputBusqueda = elemento.querySelector('input#buscar');
        if (!inputBusqueda) {
            throw 'El "elemento" no contiene un input con id="buscar" como hijo.';
        }

        elemento.addEventListener('keyup', function (e) {
            let existenCoincidencias = Filtrar(tabla, e.target.value);
            let btnAgregar = BotonAgregar(elemento, fnBotonAgregar);

            if (btnAgregar) {
                if (!existenCoincidencias) {
                    btnAgregar.agregar();
                    return;
                }

                btnAgregar.quitar();
            }
        });

    } catch (err) {
        return console.error('%cError BusquedaTabla():', 'color:orangered', err);
    }
};