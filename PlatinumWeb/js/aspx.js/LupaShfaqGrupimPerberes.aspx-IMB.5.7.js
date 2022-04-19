

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

$(window).load(function () {
    try {
        gvLupaGrupimPerberes.SetWidth(document.documentElement.clientWidth - 20);   
        Init();
    }
    catch (e) {
    }
});

$(window).bind('resize', function () {
    try {
        gvLupaGrupimPerberes.SetWidth(document.documentElement.clientWidth - 20);
    }
    catch (e) {
    }
}).trigger('resize');

$(window).on('unload', function () {
});

function Init() {
        myFaqeCelje.shtoHandlerSession();
        var idNdermarrje = hfState.Get('idNdermarrje');
        var idGjuha = hfState.Get('idGjuha');
  
        $.ajax({
            url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
            data: JSON.stringify({ idKomp: 919, kodKonf: 'LP/ShfaqGrPerb', idNdermarrje: idNdermarrje, idGjuha: idGjuha })
        }).done(SucceededCallbackKonfig);
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaGrupimPerberes.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaGrupimPerberes.GetVisibleRowsOnPage() - 1) {
            gvLupaGrupimPerberes.SetFocusedRowIndex(0);
        }
        else {
            gvLupaGrupimPerberes.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaGrupimPerberes.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
       // OnGridSelectionChanged();
    }
}

var colKushte, colAlterKusht, colKontrollet, colAtrTrupi;
function SucceededCallbackKonfig(result) {
    $("#dvArtikulli").show();
    if (result != "" && result != null) {
        colKontrollet = result.colKontrollet;
        colAtrTrupi = result.colAtrTrupi;
        resultkonf = result;
        var hf = $('#hfKontrollet');
        var hfMod = $('#hfShtimModifikim');
        hfMod.val("shtim");
        var hfLidhur = $("#hfLidhur");
        hfLidhur.value = false;
        var arrTabela = ['tblInformacion'];
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "ASPxPanel", undefined, hfLidhur);
        //if (cbGjendje.GetVisible() || cbKosto.GetVisible()) {
        //    lblMagazina.SetVisible(true);
        //    btneMagazina.SetVisible(true);
        //    if (Utils.getUrlVar('idMag') !== '' && Utils.getUrlVar('idMag') !== undefined && Utils.getUrlVar('idMag') != -1) {
        //        var idMagazina = parseInt(Utils.getUrlVar('idMag'));
        //        btneMagazina.SetValue(idMagazina);
        //    }
        //}
        //else {
        //    lblMagazina.SetVisible(false);
        //    btneMagazina.SetVisible(false);
        //}
        //var hfMag = $("#hfLupaMagazina");
        //for (var i = 0; i < colKontrollet.length; i++) {
        //    if (colKontrollet[i].KodKontrolli == "btneMagazina") {
        //        hfMag.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
        //        continue;
        //    }
        //}
    }
}


function Succeded() {
    window.parent.myButtonClickLupa.LupaUniversal_Click(hfState.Get("JQgridShtoArtikull"), 'LupaArtikullShpejte.aspx', 1100, 600);
}

//metoda per te shfaqur popupin e filtrave
function Filtra_Click() {
    window.parent.myButtonClickLupa.LupaUniversal_Click(hfState.Get("headerShtoFilter"), 'LupaFiltra.aspx?grida=gvLupaGrupimPerberes&page=LupaArtikull.aspx&idKonfigAmbjente=577', 850, 450);
    popFiltra.Show();
}

function gup(name) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\?&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(window.location.href);
    if (results == null)
        return "";
    else
        return results[1];
}

function MerrGjendjeKosto() {
    //var mag = -1;
    //if (btneMagazina.GetText() !== '')
    //    mag = btneMagazina.GetValue();
    //gvLupaGrupimPerberes.PerformCallback("kerko;610;LP/Art;" + mag);
}

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}

/*
Function: ButtonClickMagazina
    
Hap lupen e magazinave.
*/
function ButtonClickMagazina() {//po
 //   myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhMagazinen"), 'LupaMagazina.aspx?idKonfigAmbjente=' + $('#hfLupaMagazina').val(), 600, 560);
}