;

var _idKomponente;
var _idGjuha;
var _idNdermarrje;
var _idPerdoruesi;
var _idViti;
var gridaTrupi;

$(document).ready(function () {
    Init();
    changeName();
    DevExpress.localization.locale(_idGjuha == 0 ? "al" : "en");
    callWebServiceKomponenteVlere();
});


function Init() {
    myMesazh.shtoHandler();
}

function changeName() {
    _idKomponente = hfState.Get("_idKomponente");
    _idGjuha = hfState.Get("_idGjuha");
    _idNdermarrje = hfState.Get("_idNdermarrje");
    _idPerdoruesi = hfState.Get("_idPerdoruesi");
    _idViti = hfState.Get("_idViti");

    myFaqeCelje.changeName("B_KomponenteBuxhetiVlere.aspx", 0);
}

function menu_click(s, e) {
    var hfShtimModifikim = $("#hfShtimModifikim"); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    switch (e.item.name) {
        case "Ruaj":
            e.processOnServer = false;
            RuajKomponenteVlere();
            break;
        default:
            break;
    }
}

function RuajKomponenteVlere() {
    gridaTrupi.SaveCurrentValues();
    var komponenteBuxhetiVlere = gridaTrupi.GetData();
    Utils.shfaqLoadingGif();
    $.ajax({
        url: Utils.getServerApiUrl("Buxheti", "RuajKomponenteBuxhetiVlera"),
        data: JSON.stringify({ komponenteBuxhetiVlere: komponenteBuxhetiVlere, komponente: "B_KomponenteBuxhetiVlere.aspx", idNdermarrje: _idNdermarrje, idPerdoruesi: _idPerdoruesi, idViti: _idViti })
    }).done(SucceededCallbackRuajKomponenteVlere);
}

function SucceededCallbackRuajKomponenteVlere(result) {
    var status = ShfaqMesazh(result.mesazh);
    if(status)
        callWebServiceKomponenteVlere();
}

function ShfaqMesazh(mesazh) {
    if (!mesazh.Status)
        myMesazh.ShtoMesazhGabimi(mesazh.PershkrimMesazhi);
    else
        myMesazh.ShtoMesazhSuksesi(mesazh.PershkrimMesazhi);
    return mesazh.Status;
}

function callWebServiceKomponenteVlere() {
    Utils.shfaqLoadingGif();
    $.ajax({
        url: Utils.getServerApiUrl("Buxheti", "KtheKomponenteBuxhetiVlera"),
        data: JSON.stringify({ idNdermarrje: _idNdermarrje, idPerdoruesi:_idPerdoruesi })
    }).done(SucceededCallbackKomponente);
}

function SucceededCallbackKomponente(result) {
    bejGatiGride(result.dataSource);
    Utils.hiqLoadingGif();
}

//Lidhja e komponenteve me ndermarrjet
function bejGatiGride(dataSource) {
    gridaTrupi = new myDxDataGrid("gvKomponenteVlere", {
        dataSource: {
            store: {
                data: dataSource ? dataSource : new Array(),
                type: "array",
                key: ["Id", "IdKomponente", "IdQendraShendetesore"]
            }
        },
        keyExpr: "Id",
        focusStateEnabled: false,
        editing: {
            mode: "cell",
            allowUpdating: true
        },
        showRowLines: true,
        selection: {
            mode: "multiple",
            showCheckBoxesMode: "always"
        },
        filterRow: {
            visible: true
        },
        onCellPrepared: function (cell) {
            if (!cell.column.allowEditing)
                cell.cellElement.addClass("dxeDisabled_MetropolisBlue");
        }
    });

    gridaTrupi.SetColumnsFromConfig(pergatitKolona());
    gridaTrupi.AddDataSourceToColumn("Tipi", JSON.parse(hfState.Get("llojeKufizimi")), "value", "text", false);
    gridaTrupi.AddCustomOptionToColumns(["VleraMin", "VleraMax"], "dataType", "number");
}

function pergatitKolona() {
    var columns = new Array();
    columns.push({ KodiTrupi: "Komponente", PershkrimiTrupi: "Komponente", ReadonlyTrupi: true, VisibleTrupi: true });
    columns.push({ KodiTrupi: "Vlera", PershkrimiTrupi: "Vlera", ReadonlyTrupi: false, VisibleTrupi: true });
    columns.push({ KodiTrupi: "VleraMin", PershkrimiTrupi: "VleraMin", ReadonlyTrupi: false, VisibleTrupi: true });
    columns.push({ KodiTrupi: "VleraMax", PershkrimiTrupi: "VleraMax", ReadonlyTrupi: false, VisibleTrupi: true });
    columns.push({ KodiTrupi: "Tipi", PershkrimiTrupi: "Tipi", ReadonlyTrupi: false, VisibleTrupi: true });
    columns.push({ KodiTrupi: "QendraShendetesore", PershkrimiTrupi: "Qendra Shendetesore", ReadonlyTrupi: true, VisibleTrupi: true });
    return columns;
}