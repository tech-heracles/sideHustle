;

var mbush = true;
var kaloTab = false;
var _idKomponente;
var _idGjuha;
var _idNdermarrje;
var _idPerdoruesi;
var _idViti;
var resultkonf;
var colKontrollet, colAtrTrupi;
var indexModifiko;
var identifikuesPerPopup = "KomponenteBuxheti";
var komponenteEkzistuese;
var gridaTrupi;

$(window).on("load", function () {
    Init();
    changeName();
    DevExpress.localization.locale(_idGjuha == 0 ? "al" : "en");
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

    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName("B_KomponenteBuxheti.aspx", 0, hf);
    percaktoMenuSipasTabitDheTeDrejta(0);
}
function percaktoMenuSipasTabitDheTeDrejta(tabIndex) {
    myMenu.PercaktoMenuSipasTabit(tabIndex, hfTeDrejta, $("#hfShtimModifikim"));
}

function menu_click(s, e) {
    var hfShtimModifikim = $("#hfShtimModifikim"); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    var hfId = $("#hfId"); //hidden fieldi qe ruan id  e rreshtit te selektuar
    var indexModifiko = gvKomponenteBuxheti.GetFocusedRowIndex();
    switch (e.item.name) {
        case "Ruaj":
            e.processOnServer = false;
            if (!validoFusha(s, e))
                return;
            RuajKomponente();
            break;
        case "Fshi":
            FshiKomponente();
            e.processOnServer = false;
            break;
        default:
            myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl1, false, undefined, indexModifiko, pastroFusha, SucceededCallbackKonfig, resultkonf, colKontrollet, aktivizoFusha, colAtrTrupi);
            break;
    }
}

function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(";")[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(";")[0]);
    callWebserviceKonfigurimi(cmbKonfigurimi.GetText());
}

function ndryshoKonfiguriminInit() {
    callWebserviceKonfigurimiInit(cmbKonfigurimi.GetText());
}

function callWebserviceKonfigurimi(kodKonf) {
    Utils.shfaqLoadingGif();
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: _idKomponente, kodKonf: kodKonf, idNdermarrje: _idNdermarrje, idGjuha: _idGjuha })
    }).done(SucceededCallbackKonfig);
}

function callWebserviceKonfigurimiInit(kodKonf) {
    callWebserviceKonfigurimi(kodKonf);
}

function SucceededCallbackKonfig(result) {
    if (result != "" && result != null) {
        colKontrollet = result.colKontrollet;
        colAtrTrupi = result.colAtrTrupi;
        resultkonf = result;
        var hf = $("#hfKontrollet");
        var hfMod = $("#hfShtimModifikim");
        var arrTabela = ["tblInformacion"];
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, "", arrTabela, "PageControl1_C");
        myFaqeCelje.rregulloGjeresiteFushave(arrTabela);
        bejGatiGride(result.colGrida.filter(function (column) {
            return column.GridKokaEmri == 'gvKomponenteLidhjeBuxheti';
        }));
    }
    Utils.hiqLoadingGif();
}

function onActiveTabChanged(s, e) {
    hapRreshtPerModifikim();
    kaloTab = false; //ishte false
    percaktoMenuSipasTabitDheTeDrejta(PageControl1.GetActiveTabIndex());
    if (PageControl1.GetActiveTabIndex() == 2)
        gridaTrupi.Refresh();
}

function validoFusha(s, e) {
    return myFaqeCelje.valido(s, e, PageControl1, hfTeDrejta, $('#hfShtimModifikim'));
}

function hapRreshtPerModifikim() {
    var indexModifiko = gvKomponenteBuxheti.GetFocusedRowIndex();
    if (mbush) {
        if (indexModifiko != -1) {
            komponenteEkzistuese = null;
            OnGridDoubleClick();
        }
        else {
            mbush = false;
            $("#hfShtimModifikim")[0].value = "shtim";
            $("#hfId")[0].value = 0;
            pastroFusha();
        }
    }
}

function OnGridDoubleClick() {
    mbushfusha();
}

function mbushfusha() {
    mbush = false;
    $("#hfShtimModifikim")[0].value = "modifikim";
    var indexModifiko = gvKomponenteBuxheti.GetFocusedRowIndex();
    if (indexModifiko === -1)
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKategoriBuxhetimiZgjidhPerModifikim"));
    else {
        GetRowValues(indexModifiko);
        Utils.shfaqLoadingGif();
    }

}

//per ndryshim
function GetRowValues(index) {
    gvKomponenteBuxheti.GetRowValues(index, "Id", function (value) {
        aktivizoFusha();
        if ($("#hfShtimModifikim").val() == "shtim")
            return;
        $("#hfId").val(value);
        callWebServiceKomponente();

        if (kaloTab) {
            PageControl1.SetActiveTabIndex(1);
            percaktoMenuSipasTabitDheTeDrejta(1);
        }
    });
}


function aktivizoFusha() {
    var hfMod = $("#hfShtimModifikim");
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, false, "#PageControl1_C");
    kontrolleSetEnabled();
}


function pastroFusha() {
    $("#hfId").val(0);
    txtKodi.SetValue("");
    txtEmertimi.SetValue("");
    cmbBuxheti.SetSelectedIndex(-1);
    txtFormula.SetValue("");
    txtVleraMin.SetValue(null);
    txtVleraMax.SetValue(null);
    cbAktive.SetValue(true);
    cmbNjesia.SetSelectedIndex(0);
    cmbLlojKufizimi.SetSelectedIndex(0);
    cmbTipi.SetSelectedIndex(0);
    komponenteEkzistuese = null;
    VendosDataSourceNeGrideLidhje(new Array());
    aktivizoFusha();
}

function valido(s, e) {
    myFaqeCelje.valido(s, e, PageControl1, hfTeDrejta, $("#hfShtimModifikim"));
}

/*
Function: EndRequestHandler
    
Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe array-t e perdorura.
Shiko funksionet  <ndryshoKonfigurimin>.
*/
function EndRequestHandler(sender, args) {
    var hf = $("#hfStatusi");
    var hfKontrollet = $("#hfKontrollet");
    var hfShtimModifikim = $("#hfShtimModifikim"); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    var hfId = $("#hfId");  //hidden fieldi qe ruan id  e rreshtit te selektuar

    indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl1, gvKomponenteBuxheti, _idKomponente, pastroFusha, hfTeDrejta, undefined, undefined, false);

    percaktoMenuSipasTabitDheTeDrejta(PageControl1.GetActiveTabIndex());

    if (hfState.Get("ruajtjeFiltri") && btnFiltrat) {
        btnFiltrat.PerformCallback();
    }
    hfState.Set("ruajtjeFiltri", false);
}

var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvKomponenteBuxheti, _idKomponente, cmbKonfigurimi.GetText());
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

function textChanged(s, e) {
    hfState.Set("ruajtjeFiltri", true);
    myMenu.textChanged(s, e);
}


function checkText(s, e) {
    myMenu.checkText(s, e);
}


function Buxheti_Click() {
    popupUniversal.SetHeaderText(hfState.Get("lblLlojBuxheti"));
    popupUniversal.SetContentUrl('B_LlojeBuxheti.aspx');
    popupUniversal.SetSize(1000, 600);
    popupUniversal.Show();
}

function VendosLlojBuxhetiNeCombo(idLlojBuxheti, kodi, pershkrimi) {
    Utils.ShtoNeseNukGjendetDheSelektoCombo(cmbBuxheti, idLlojBuxheti, new Array(kodi, pershkrimi));
    NgarkoDataSourceLidhjeje(cmbBuxheti);
}

function RuajKomponente() {
    var komponenteBuxheti = krijoKomponente();
    var komponenteBuxhetiLidhje = gridaTrupi.Grida.getSelectedRowsData();
    Utils.shfaqLoadingGif();
    $.ajax({
        url: Utils.getServerApiUrl("Buxheti", "RuajKomponenteBuxheti"),
        data: JSON.stringify({ komponenteBuxheti: komponenteBuxheti, komponenteBuxhetiLidhje: komponenteBuxhetiLidhje, komponente: "B_KomponenteBuxheti.aspx", idNdermarrje: _idNdermarrje, idPerdoruesi: _idPerdoruesi, idViti: _idViti })
    }).done(SucceededCallbackRuajKomponente);
}

function SucceededCallbackRuajKomponente(result) {
    var status = ShfaqMesazh(result.mesazh);
    if (status) {
        pastroFusha();
        $("#hfShtimModifikim").val("shtim");
        aktivizoFusha();
        PageControl1.SetActiveTabIndex(1);
        gvKomponenteBuxheti.PerformCallback();
    }
    return;
}

function FshiKomponente() {
    Utils.shfaqLoadingGif();
    if (gvKomponenteBuxheti.GetSelectedRowCount() < 1)
    {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKompZgjidh"));
        return;
    }
    gvKomponenteBuxheti.GetSelectedFieldValues("Id", function (value) {
        $.ajax({
            url: Utils.getServerApiUrl("Buxheti", "FshiKomponenteBuxheti"),
            data: JSON.stringify({ idKomponenteBuxheti: value, komponente: "B_KomponenteBuxheti.aspx", idNdermarrje: _idNdermarrje, idPerdoruesi: _idPerdoruesi, idViti: _idViti })
        }).done(SucceededCallbackFshiKomponente);
    });
}

function SucceededCallbackFshiKomponente(result) {
    var status = ShfaqMesazh(result.mesazh);
    if (status && PageControl1.GetActiveTabIndex() != 0) {
        pastroFusha();
        PageControl1.SetActiveTabIndex(0);
    }
    gvKomponenteBuxheti.PerformCallback();
}

function ShfaqMesazh(mesazh) {
    if (!mesazh.Status)
        myMesazh.ShtoMesazhGabimi(mesazh.PershkrimMesazhi);
    else
        myMesazh.ShtoMesazhSuksesi(mesazh.PershkrimMesazhi);
    return mesazh.Status;
}

function callWebServiceKomponente() {
    Utils.shfaqLoadingGif();
    $.ajax({
        url: Utils.getServerApiUrl("Buxheti", "KtheKomponenteBuxhetiMeId"),
        data: JSON.stringify({ id: $('#hfId').val() })
    }).done(SucceededCallbackKomponente);
}

function SucceededCallbackKomponente(result) {
    komponenteEkzistuese = result.komponente;
    var buxhetiLidhur = result.buxhetiLidhur;

    txtKodi.SetValue(komponenteEkzistuese.Kodi);
    txtEmertimi.SetValue(komponenteEkzistuese.Pershkrimi);
    Utils.ShtoNeseNukGjendetDheSelektoCombo(cmbBuxheti, buxhetiLidhur.IdLlojBuxheti, buxhetiLidhur.Kodi);
    txtFormula.SetValue(komponenteEkzistuese.Formula);
    txtVleraMin.SetValue(komponenteEkzistuese.VleraMin);
    txtVleraMax.SetValue(komponenteEkzistuese.VleraMax);
    cbAktive.SetValue(komponenteEkzistuese.Aktive);
    cmbNjesia.SetValue(komponenteEkzistuese.Njesia);
    cmbLlojKufizimi.SetValue(komponenteEkzistuese.LlojKufizimi);
    cmbTipi.SetValue(komponenteEkzistuese.Tipi);
    kontrolleSetEnabled();
    NgarkoDataSourceLidhjeje(cmbBuxheti);
    Utils.hiqLoadingGif();
}

function shtoHiqBuxheteNgaComboBuxheti(unparsedObjectBuxheti) {
    var editedLlojeBuxheti = JSON.parse(unparsedObjectBuxheti);
    if (!editedLlojeBuxheti) return;

    var llojBuxheti;
    var existentBuxheti;
    for (var i = 0; i < editedLlojeBuxheti.llojeBuxheti.length; i++) {
        llojBuxheti = editedLlojeBuxheti.llojeBuxheti[i];
        if (cmbBuxheti.GetValue() == llojBuxheti.IdLlojBuxheti && editedLlojeBuxheti.veprimi == "fshi") {
            cmbBuxheti.SetSelectedIndex(-1);
            cmbBuxheti.ClearItems();
        }
    }
};


function krijoKomponente() {
    var komponenteRe = {};
    var modifikim = ($("#hfShtimModifikim").val() == "modifikim" && komponenteEkzistuese);
    komponenteRe.Id = modifikim ? komponenteEkzistuese.Id : 0;
    komponenteRe.IdNdermarrje = modifikim ? komponenteEkzistuese.IdNdermarrje : _idNdermarrje;
    komponenteRe.IdKrijuesi = modifikim ? komponenteEkzistuese.IdKrijuesi : _idPerdoruesi;
    komponenteRe.IdModifikuesi = modifikim ? _idPerdoruesi : 0;
    komponenteRe.DtKrijimi = modifikim ? komponenteEkzistuese.DtKrijimi : null;
    komponenteRe.DtModifikimi = modifikim ? komponenteEkzistuese.DtModifikimi : null;
    komponenteRe.Kodi = txtKodi.GetText();
    komponenteRe.Pershkrimi = txtEmertimi.GetText();
    komponenteRe.Tipi = cmbTipi.GetValue();
    komponenteRe.Njesia = cmbNjesia.GetValue();
    komponenteRe.IdBuxheti = cmbBuxheti.GetValue();
    komponenteRe.Formula = txtFormula.GetText();
    komponenteRe.VleraMin = txtVleraMin.GetValue();
    komponenteRe.VleraMax = txtVleraMax.GetValue();
    komponenteRe.LlojKufizimi = cmbLlojKufizimi.GetValue();
    komponenteRe.Aktive = cbAktive.GetValue();
    return komponenteRe;
}

//Lidhja e komponenteve me ndermarrjet
function bejGatiGride(columnsKonfig) {
    gridaTrupi = new myDxDataGrid("gvKomponenteLidhjeBuxheti", {
        dataSource: {
            store: {
                data: new Array(),
                type: "array",
                key: ["Id", "IdKomponente", "IdKategoriBuxhetimi"]
            }
        },
        keyExpr: "Id",
        focusStateEnabled: false,
        editing: {
            mode: "none",
            allowUpdating: false
        },
        showRowLines: true,
        selection: {
            mode: "multiple",
            showCheckBoxesMode: "always"
        },
        filterRow: {
            visible: true
        },
    });

    gridaTrupi.SetColumnsFromConfig(columnsKonfig);
}

function NgarkoDataSourceLidhjeje(s, e, ngaLostFocus) {
    if (ngaLostFocus && s.GetValue() > 0)
        return;
    if (s.GetValue() <= 0 || cmbTipi.GetText() != "Komponente") {
        VendosDataSourceNeGrideLidhje(new Array());
        return;
    }
    Utils.shfaqLoadingGif();
    $.ajax({
        url: Utils.getServerApiUrl("Buxheti", "KtheKategoriPerLidhjeSipasBuxhetit"),
        data: JSON.stringify({ idKomponenteBuxheti: $('#hfId').val(), idLlojBuxheti: cmbBuxheti.GetValue() })
    }).done(SucceededCallbackKomponenteLidhje);
}

function SucceededCallbackKomponenteLidhje(result) {
    VendosDataSourceNeGrideLidhje(result.komponenteLidhje);
}

function VendosDataSourceNeGrideLidhje(dataSource) {
    gridaTrupi.Grida.option("dataSource.store.data", dataSource);
    if (dataSource.length > 0) {
        var keys = dataSource.map(function (a) { return { Id: a.Id, IdKomponente: a.IdKomponente, IdKategoriBuxhetimi: a.IdKategoriBuxhetimi }; }).filter(function (key) { return key.Id != 0; });
        if (keys.length > 0) {
            cmbBuxheti.SetEnabled(false);
            gridaTrupi.Grida.selectRows(keys, true);
        }
    }
    Utils.hiqLoadingGif();
}

function kontrolleSetEnabled() {
    var njesia = cmbNjesia.GetText();
    txtFormula.SetEnabled(njesia != "Tabelare");
    lblFormula.SetEnabled(njesia != "Tabelare");
    cmbLlojKufizimi.SetEnabled(njesia != "Tabelare");
    lblLlojKufizimi.SetEnabled(njesia != "Tabelare");
    txtVleraMin.SetEnabled(njesia != "Tabelare");
    lblVleraMin.SetEnabled(njesia != "Tabelare");
    txtVleraMax.SetEnabled(njesia != "Tabelare");
    lblVleraMax.SetEnabled(njesia != "Tabelare");
}

function SetBtnFiltraValue(s, e) {
    cmbfiltra.SetValue(s.GetValue());
}