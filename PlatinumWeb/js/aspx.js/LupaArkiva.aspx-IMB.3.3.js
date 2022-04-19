
$(document).ready(function () {
    var responseFromUrl =  Utils.getUrlVar("idDok");
    var idDok = responseFromUrl ? responseFromUrl : 0;
    var idKategoria = Utils.KategoriArkive[Utils.getUrlVar("veprimi")];
    Arkiva.Init(idDok, idKategoria, fileManager);
});
$(window).on('pageshow',
    function () {Arkiva.SetDefaultThumb();
    }
    );



