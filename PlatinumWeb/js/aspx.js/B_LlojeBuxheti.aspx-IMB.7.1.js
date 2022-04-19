;
var editmode = false;
var indexEdit = -1;
var indexModifiko;
var mbush = true;
var kaloTab = false;
var _idKomponente;
var _idGjuha;
var _idNdermarrje;
var indexModifiko;

$(window).on("load", function () {
    Init();
});


function Init() {
    changeName();
    myMesazh.shtoHandler();
    myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    ASPxMenu1.GetItemByName('Ruaj').SetVisible(false);
    editmode = false;
    indexEdit = -1;
}

function changeName() {
    _idKomponente = hfState.Get("_idKomponente");
    _idGjuha = hfState.Get("_idGjuha");
    _idNdermarrje = hfState.Get("_idNdermarrje");
    myFaqeCelje.changeName("B_LlojeBuxheti.aspx", 0);
}

function menu_click(s, e) {
    var hfRuaj = $('#hfRuaj');
    switch (e.item.name) {
        case "OK":
            e.processOnServer = false;
            ZgjidhRreshtaNgaLupa();
            return;
        case "Anullo":
            e.processOnServer = false;
            window.parent.popupUniversal.Hide();
            return;
        default:
            myMenu.menu_click_celjevogel(s, e, hfRuaj, gvLlojBuxheti, hfTeDrejta);
            break;
    }

}

function Row_DblClick(s, e) {
    gvLlojBuxheti.StartEditRow(e.visibleIndex);
    ASPxMenu1.GetItemByName('Ruaj').SetVisible(true);
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}


var btnFiltrat;
function aplikoFiltra(s, e) {
    $('#hfRuaj').val('Filtra');
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLlojBuxheti, _idKomponente, 1);
}

function textChanged(s, e) {
    hfState.Set("ruajtjeFiltri", true);
    myMenu.textChanged(s, e);
}


function checkText(s, e) {
    myMenu.checkText(s, e);
}


function ZgjidhRreshtaNgaLupa() {
    gvLlojBuxheti.GetSelectedFieldValues('IdLlojBuxheti;Kodi;Pershkrimi', OnGridSelectionComplete);
}

function OnGridSelectionComplete(value) {
    if (value.length == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgLlojeBuxhetiZgjidh"));
        return;
    }
    switch (window.parent.identifikuesPerPopup){
        case "KategoriBuxhetimi":
            var cmbValues = window.parent.cmbBuxheti.GetValue();
            for (var i = 0; i < value.length; i++)
                cmbValues.push(value[i][0]);
            window.parent.cmbBuxheti.SetValue(cmbValues);
            break;
        case "PerfitimBuxheti":
        case "RialokimBuxheti":
            if (value.length > 1) {
                myMesazh.ShtoMesazhGabimi("Mund te zgjidhni vetem nje lloj buxheti nga lista.");
                return;
            }
            window.parent.VendosBuxhetNeTrup(value[0][0], value[0][1], value[0][2]);
            break;
        default:
            break;
    }

    window.parent.popupUniversal.Hide();
}


function callWebservice() {
    var emer = 'B_LlojeBuxheti.aspx';
    var veprim = 'Modifiko';
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "eshteVeprimILejuar"),
        data: JSON.stringify({ emerKomponente: emer, veprimi: veprim })
    }).done(SucceededCallback);
}

function SucceededCallback(result) {
    if (result == "true") {
        switchEditMode(indexModifiko);
    }
    else {
        alert(hfTeDrejta.Get("msgNukKeniDrejtaPerVeprim"));
    }
}

function switchEditMode(index) {
    $("#hfRuaj").val('Modifiko');
    ASPxMenu1.GetItemByName('Ruaj').SetVisible(true);
    myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    gvKategoriDok.StartEditRow(index);
    indexEdit = index;
}

function EndCallbackGrida(s, e) {
    myMesazh.ShtoMesazhNgaGrida(gvLlojBuxheti);
    var editedLlojeBuxheti;

    if (window.parent.shtoHiqBuxheteNgaComboBuxheti) {
        if (!gvLlojBuxheti["cpEditedLlojeBuxheti"]) return;
        window.parent.shtoHiqBuxheteNgaComboBuxheti(gvLlojBuxheti["cpEditedLlojeBuxheti"]); //rasti i shtimit ose i modifikimit (callback i grides)eti(editedLlojeBuxheti);
        delete gvLlojBuxheti["cpEditedLlojeBuxheti"];
    }
}

/*
Function: EndRequestHandler
    
Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe array-t e perdorura.
Shiko funksionet  <ndryshoKonfigurimin>.
*/
function EndRequestHandler(sender, args) {
    gvLlojBuxheti.PerformCallback();

    if (hfState.Get("ruajtjeFiltri") && btnFiltrat) {
        btnFiltrat.PerformCallback();
    }
    hfState.Set("ruajtjeFiltri", false);

    if (window.parent.shtoHiqBuxheteNgaComboBuxheti) {
        if (!hfState.Get("editedLlojeBuxheti")) return;
        window.parent.shtoHiqBuxheteNgaComboBuxheti(hfState.Get("editedLlojeBuxheti")); //rasti i fshirjes (callback nga endRequestHandler pas fshirjes)
        hfState.Set("editedLlojeBuxheti", null);
    }
}