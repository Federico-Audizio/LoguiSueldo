var MantenerFiltros = (function () {
    if (!window.sessionStorage) {
        return;
    }

    var recuperarFiltros = function (filtros, filtrar) {
        console.log('recuperarFiltros()');
        var _filtros = JSON.parse(sessionStorage.getItem('filtros'));

        if (filtrar && _filtros) {
            filtros.forEach(function (filtro) {
                if (_filtros[filtro.name]) {
                    filtro.element.value = _filtros[filtro.name];
                }                
                //if (sessionStorage.getItem(filtro.name)) {
                //    filtro.element.value = sessionStorage.getItem(filtro.name);
                //}
            })
        }

        sessionStorage.clear();
    }



    var agregarFiltros = function (filtros) {
        console.log('agregarFiltros()');

        //var _filtros = filtros.map(function (filtro) {
        //    return {
        //        [filtro.name]: filtro.element.value
        //    }
        //});

        var _filtros = filtros.reduce(function (acc, filtro) {
            acc[filtro.name] = filtro.element.value;
            return acc;
        }, {});

        var filtrosJSON = JSON.stringify(_filtros);

        if (filtrosJSON) {
            sessionStorage.setItem('filtros', filtrosJSON);
        }
        
        //filtros.forEach(filtro => {
        //    sessionStorage.setItem(filtro.name, filtro.element.value);
        //})
    }


    var iniciar = function (filtros, filtrar, fn) {

        document.addEventListener('DOMContentLoaded', function () {
            recuperarFiltros(filtros, filtrar);
            fn();
        });

        window.addEventListener('beforeunload', function () {
            agregarFiltros(filtros);
        });
    }

    return {
        iniciar: iniciar
    };

})();