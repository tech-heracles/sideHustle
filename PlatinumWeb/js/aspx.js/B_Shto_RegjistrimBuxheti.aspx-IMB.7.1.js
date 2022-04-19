;

var _idKomponente;
var _idGjuha;
var _idNdermarrje;
var _idNdermViti;
var _idPerdoruesi;
var _idViti;
var colKontrollet;
var colAtrTrupi;
var myMesazh;
var ndryshuarNjehere;
var gridaTrupi;
var konfigurimet;
var idKonvertimi;
var llojKonvertimiNga;

var varKonfig = {
    identifikuesPerLocalStorageKey: 'Buxheti'
};

$(document).ready(function () {
    $(document).on("keydown", function (e) {//po
        switch (e.which) {
            case 13:
                e.preventDefault();
                break;
            case 116: //F5
                if (window.parent !== undefined)
                    window.parent.rifresko = true;
                break;
            case 82:
                if (e.ctrlKey) //ctrl+r
                    window.parent.rifresko = true;
            default:
                break;
        }
    });
    Utils.konfiguroAccorditionNeDocReady(varKonfig.identifikuesPerLocalStorageKey);
});

$(window).on("load", function () {
    Init();
    DevExpress.localization.locale(_idGjuha == 0 ? "al" : "en");
});


function Init() {
    changeName();
    myMesazh.shtoHandler();
    ndryshoKonfigurimin();
}


function changeName() {
    _idKomponente = hfState.Get("_idKomponente");
    _idGjuha = hfState.Get("_idGjuha");
    _idNdermarrje = hfState.Get("_idNdermarrje");
    _idNdermViti = hfState.Get("_idNdermarrjeVit");
    _idPerdoruesi = hfState.Get("_idPerdoruesi");
    _idViti = hfState.Get("_idViti");
    myFaqeCelje.changeName("B_Shto_RegjistrimBuxheti.aspx?planifikim_miratim=" + Utils.getUrlVar('planifikim_miratim'), 0);
}


function menu_click(s, e) {
    switch (e.item.name) {
        case "Shto":
            e.processOnServer = false;
            $("#hfShtimModifikim").val("shtim")
            pastroFusha(true);
            break;
        case "Fshi":
            e.processOnServer = false;
            callWebserviceFshiDokumentBuxheti();
            break;
        case "Ruaj":
            if(!myFaqeCelje.validim(s, e))
                return;
            e.processOnServer = false;
            RuajDokumentBuxheti(1);
            break;
        case "Draft":
            if (!myFaqeCelje.validim(s, e))
                return;
            e.processOnServer = false;
            RuajDokumentBuxheti(0);
            break;
        case "Posto":
            e.processOnServer = false;
            callWebservicePostoDokumentBuxheti();
            break;
        case "Anullo":
            myFaqeCelje.kontrolloTeDrejta('B_RegjistrimBuxheti.aspx?lloji=' + Utils.getUrlVar('planifikim_miratim'));
            e.processOnServer = false;
            break;
        case "Gjenero":
            e.processOnServer = false;
            GjeneroDokumentPlanifikimi();
            break;
        case "Konverto":
            ButtonClickKonverto();
            e.processOnServer = false;
            break;
        case "Eksporto":
            e.processOnServer = false;
            callWebserviceEksportoDokumentMiratimBuxheti();
            break;
        default:
            break;
    }
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}


function checkText(s, e) {
    myMenu.checkText(s, e);
}



function ndryshoKonfigurimin() {
    var cmbModKontroll = cmbModeli.GetSelectedItem();
    var pershkKonfigAmb = cmbModKontroll.GetColumnText("PershkrimKonfigAmbjente");
    var pershKokeDok = "Kokë Dokumenti";
    if (pershkKonfigAmb != undefined) {
        lblKonfigurimi.SetText(pershkKonfigAmb);
        lblKonfigurimi.SetVisible(false);
        $('#kokeKonfigurimi').text(pershKokeDok + ': ' + pershkKonfigAmb);
    }
    callWebserviceKonfigurimi(cmbModKontroll.text);
}

function ndryshoKonfiguriminInit() {
    callWebserviceKonfigurimiInit(cmbModeli.GetText());
}

function callWebserviceKonfigurimi(kodKonf) {
    Utils.shfaqLoadingGif();
    var eshteShtim = $("#hfShtimModifikim") == 'shtim';
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfigDB"),
        data: JSON.stringify({ idKomp: _idKomponente, kodKonf: kodKonf, idNdermarrje: _idNdermarrje, kodKontrollKlienti: "", idKlienti: -1, shtim: eshteShtim, merrFormatKursi: false, idGjuha: _idGjuha })
    }).done(SucceededCallbackKonfig);
}

function callWebserviceKonfigurimiInit(kodKonf) {
    callWebserviceKonfigurimi(kodKonf);
}

var formatNumriZgjedhur;
function SucceededCallbackKonfig(result) {
    if (result != "" && result != null) {
        colKontrollet = result.colKontroll;
        colAtrTrupi = result.colAtrTrupi;
        resultkonf = result;
        formatNumriZgjedhur = result.formatNumri;
        var hf = $("#hfKontrollet");
        var hfMod = $("#hfShtimModifikim");
        var arrTabela = ['tblFillim'];
        var arrPrind = ["dvFillim"];
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, "", arrTabela, undefined, undefined, undefined, arrPrind);
        myFaqeCelje.rregulloGjeresiteFushave(arrTabela);
        Utils.konfiguroAccorditionPasKonfigDokumenti(varKonfig.identifikuesPerLocalStorageKey);

        Utils.setFormatNumri(txtBuxhetiPlanifikuar, formatNumriZgjedhur.ShifraPasPresjesVlefta || 2);
        Utils.setFormatNumri(txtBuxhetiFaktik, formatNumriZgjedhur.ShifraPasPresjesVlefta || 2);

        pastroFusha(false);

        percaktoVisibleMenuBuxheti(hfMod.val() == 'modifikim');
        if (hfMod.val() == "konvertim") {
            cmbModeli.SetEnabled(false);
            cmbNiveli.SetEnabled(false);
        }
    }
    Utils.hiqLoadingGif();
}

/*
Function: TextChangedNiveli
Therret funksionin <callWebserviceNiveli> per te vendosur templaten sipas nivelit te zgjedhur.
*/
function TextChangedNiveli() {//po
    var hidField1 = document.getElementById("hfVeprimi");
    var mod;
    if (pageState.lloji != 'shtim')
        mod = true;
    else mod = false;
    if (cmbNiveli.GetText() != "") {
        callWebserviceNiveli(cmbNiveli.GetValue(), hidField1.value, mod);
        percaktoMenuSipasTeDrejtavePerNivelin(cmbNiveli.GetValue());
    }
    else callWebserviceNiveli(0, hidField1.value, mod);
}

function callWebserviceNiveli(idNiveli, veprimi, mod) {//po
    try {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheTemplateNiveli"), data: JSON.stringify({ idNiveli: idNiveli, veprimi: veprimi, mod: mod, idPerdoruesi: pageState.idPerdoruesi, idNdermarrje: pageState.idNdermarrje, idGjuha: pageState.idGjuha })
        }).done(SucceededCallbackNiveli);
    }
    catch (e) {
        console.log(e.message)
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTeDhenave"));
    }
}

/*
Function: SucceededCallbackNiveli

Mbush combo-n modeli me vlerat sipas nivelit te zgjedhur
*/
var selektoKonfigurim = false;

function SucceededCallbackNiveli(colModelet) {
    if ($("#hfShtimModifikim").val() == "shtim") {
        cmbModeli.ClearItems();
        for (i = 0; i < colModelet.length; i++) {
            cmbModeli.AddItem([colModelet[i].KodKonfigAmbjente, colModelet[i].PershkrimKonfigAmbjente], colModelet[i].IdKonfigAmbjente);
        }
        cmbModeli.SelectIndex(0);
    }
    ndryshoKonfigurimin();
}


function callWebserviceDokumentBuxheti() {
    Utils.shfaqLoadingGif();
    $.ajax({
        url: Utils.getServerApiUrl("Buxheti", "KtheDokumentBuxheti"),
        data: JSON.stringify({ idKomponente: _idKomponente, idKonfigurim: cmbModeli.GetValue(), idNdermarrje: _idNdermarrje, idGjuha: _idGjuha, emerGride: 'dxDataGrid_trupBuxheti', idKokaBuxheti: $("#hfId").val() })
    }).done(SucceededCallbackDokumentBuxheti);
}

function RuajDokumentBuxheti(idStatusDok) {
    unFormatoFushaDevi();
    var kokaBuxheti = krijoKokeDokumenti(idStatusDok);
    var trupiBuxheti = krijoTrupDokumenti(kokaBuxheti.IdBuxhetiKoka);
    callWebserviceRuajDokument(kokaBuxheti, trupiBuxheti);
}

function callWebservicePostoDokumentBuxheti() {
    if (Utils.getUrlVar("planifikim_miratim") == "planifikim")
        myMesazh.ShtoMesazhGabimi("Nuk mund te postoni dokumentat e planifikim buxhetit");
    Utils.shfaqLoadingGif();
    var komponente = "B_Shto_RegjistrimBuxheti.aspx?planifikim_miratim=" + Utils.getUrlVar("planifikim_miratim");
    $.ajax({
        url: Utils.getServerApiUrl("Buxheti", "PostoDokumentMiratimPlanifikimBuxheti"),
        data: JSON.stringify({ idKokaBuxheti: $("#hfId").val(), komponente: komponente, idNdermarrje: _idNdermarrje, idPerdoruesi: _idPerdoruesi, idViti: _idViti })
    }).done(SucceededCallbackPostoDokument);
}

function callWebserviceRuajDokument(kokaBuxheti, trupiBuxheti) {
    Utils.shfaqLoadingGif();
    var veprimi = Utils.getUrlVar("planifikim_miratim");
    var komponente = "B_Shto_RegjistrimBuxheti.aspx?planifikim_miratim=" + veprimi;
    $.ajax({
        url: Utils.getServerApiUrl("Buxheti", "RuajDokumentBuxheti"),
        data: JSON.stringify({ komponente: komponente, idNdermarrje: _idNdermarrje, idPerdoruesi: _idPerdoruesi, idViti: _idViti, kokaBuxheti: kokaBuxheti, trupiBuxheti: trupiBuxheti, idSkemaWF: 0, statusAprovimi: 0, idEtapa: 0 })
    }).done(SucceededCallbackRuajDokument);
}

function callWebserviceFshiDokumentBuxheti(){
    Utils.shfaqLoadingGif();
    var komponente = "B_Shto_RegjistrimBuxheti.aspx?planifikim_miratim=" + Utils.getUrlVar("planifikim_miratim");
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Buxheti", "FshiDokumentBuxheti"),
        data: JSON.stringify({ ids: new Array($('#hfId').val()), komponente: komponente })
    }).done(SucceededCallbackFshiDokumentBuxheti)
}

function SucceededCallbackPostoDokument(result) {
    ShfaqMesazh(result.mesazh);
    Utils.hiqLoadingGif();
}

function SucceededCallbackRuajDokument(result) {
    var status = ShfaqMesazh(result.mesazh);
    if (status) {
        $("#hfShtimModifikim").val("shtim");
        var hf = $("#hfKontrollet");
        var hfMod = $("#hfShtimModifikim");
        var arrTabela = ["tblFillim"];
        var arrPrind = ["dvFillim"];
        if (Utils.getUrlVar("planifikim_miratim") == "miratim") {
            hfMod.val("modifikim");
            $('#hfId').val(result.idKokaBuxheti);
            pastroFusha(false);
        }
        else
            pastroFusha(true);
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, "", arrTabela, undefined, undefined, undefined, arrPrind);
        myFaqeCelje.rregulloGjeresiteFushave(arrTabela);

    }
    formatoFushaDevi();
}

function SucceededCallbackFshiDokumentBuxheti(result) {
    if (result.mesazhSukses != "")
        myMesazh.ShtoMesazhSuksesi(result.mesazhSukses);
    if (result.mesazhGabim != "")
        myMesazh.ShtoMesazhGabimi(result.mesazhGabim);

    if (result.mesazhSukses != "")
        myFaqeCelje.kontrolloTeDrejta('B_RegjistrimBuxheti.aspx?lloji=' + Utils.getUrlVar('planifikim_miratim'));
    Utils.hiqLoadingGif();

}

function ShfaqMesazh(mesazh) {
    if (!mesazh.Status)
        myMesazh.ShtoMesazhGabimi(mesazh.PershkrimMesazhi);
    else
        myMesazh.ShtoMesazhSuksesi(mesazh.PershkrimMesazhi);
    return mesazh.Status;
}

function SucceededCallbackDokumentBuxheti(result) {
    var kokaBuxheti = result.kokaBuxheti;
    var dataSourceTrupi = result.dsTrupiBuxheti;
    var columnsKonfig = result.columnsKonfig;
    $("#hfId").val(kokaBuxheti.IdBuxhetiKoka);

    if (kokaBuxheti.IdBuxhetiKoka > 0)
        mbushFushaKoke(kokaBuxheti);
    if (kokaBuxheti.IdStatusDok != 0) {
        ASPxMenu1.GetItemByName('Gjenero').SetEnabled(false);
        if($("#hfShtimModifikim").val() != 'konvertim')
            ASPxMenu1.GetItemByName('Draft').SetVisible(false);
    }

    bejGatiGride(columnsKonfig, dataSourceTrupi);
    Utils.hiqLoadingGif();
}


function bejGatiGride(columnsKonfig, dataSource) {
    gridaTrupi = new myDxDataGrid("dxDataGrid_trupBuxheti", {
        dataSource: dataSource ? dataSource : new Array(),
        keyExpr: "IdKategoriBuxhetimi",
        formatNumriZgjedhur: formatNumriZgjedhur,
        editorColumnOption: { step: 0 },
        onCellPrepared: function (cell) {
            if (!cell.column.allowEditing && (cell.column.dataField == "VleraPlanifikuar" || cell.column.dataField == "VleraFaktike"))
                cell.cellElement.addClass("dxeDisabled_MetropolisBlue");
        },
        onRowUpdated: function () {
            ndryshuarNjehere = true;
        }
    });

    gridaTrupi.SetColumnsFromConfig(columnsKonfig); //percakton kolonat e grides
    gridaTrupi.SetGroupingColumnsByOrder(new Array("NdermarrjeNiveli1", "NdermarrjeNiveli2", "NdermarrjeNiveli3")); //percakton kolonat per grupim te vlerave (nivelet e grupimit sipas rradhes)
    gridaTrupi.SetSummaryColumns(new Array("VleraPlanifikuar", "VleraFaktike"), "sum"); //percakton kolonat per te cilat do behen summary dhe lloji i veprimit
}

function bejUpdateVleratKolonesNeTrup(s, e) {
    var totaliFaktik = txtBuxhetiFaktik.GetValue();
    var totaliPlanifikuar = txtBuxhetiPlanifikuar.GetText();

    if (($('#hfId').val() > 0 && $("#hfShtimModifikim").val() != 'konvertim') || !(totaliPlanifikuar > 0) || ndryshuarNjehere)//($('#hfId').val() > 0) ||
        return;

    if (isNaN(totaliFaktik) || isNaN(totaliPlanifikuar)) {
        s.SetValue(null);
        s.SetFocus();
        return;
    }

    var arrayDs = gridaTrupi.GetData();
    for (var i = 0; i < arrayDs.length; i++) {
        arrayDs[i].VleraFaktike = (arrayDs[i].VleraPlanifikuar / totaliPlanifikuar) * totaliFaktik;
    }
    gridaTrupi.Refresh();
}

function pastroFusha(fshiId) {
    if (fshiId)
        $("#hfId").val(null);
    cmbViti.SetSelectedIndex(0);
    txtNrDok.SetValue(null);
    dteDate.SetValue(Utils.ktheDateDefault($.parseJSON(hfState.Get("periudha"))));
    txtShenime.SetValue(null);
    txtBuxhetiPlanifikuar.SetText(0);
    txtBuxhetiFaktik.SetText(0);
    callWebserviceDokumentBuxheti();
    percaktoVisibleMenuBuxheti($("#hfShtimModifikim").val() == 'modifikim');
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, $("#hfShtimModifikim"), false, "");
    formatoFushaDevi();
    ASPxMenu1.GetItemByName('Gjenero').SetEnabled(true);
    ASPxMenu1.GetItemByName('Draft').SetVisible(true);
    ndryshuarNjehere = false;
}

function mbushFushaKoke(kokaBuxheti) {
    cmbViti.SetValue(kokaBuxheti.Viti);
    txtNrDok.SetValue(kokaBuxheti.NrDok);
    dteDate.SetValue(new Date(kokaBuxheti.DtDok));
    txtShenime.SetValue(kokaBuxheti.Shenime);
    txtBuxhetiPlanifikuar.SetText(kokaBuxheti.TotaliPlanifikuar);
    txtBuxhetiFaktik.SetText(kokaBuxheti.Totali);
    if (kokaBuxheti.IdRaportDesign && $("#hfShtimModifikim").val() != 'konvertim')
        cmbFormatiPrintimit.SetValue(kokaBuxheti.IdRaportDesign);
    formatoFushaDevi();
}

function formatoFushaDevi() {
    Utils.formatoTextBox(txtBuxhetiFaktik);
    Utils.formatoTextBox(txtBuxhetiPlanifikuar);
}

function unFormatoFushaDevi() {
    Utils.unFormatoTextBox(txtBuxhetiFaktik);
    Utils.unFormatoTextBox(txtBuxhetiPlanifikuar);
}

function percaktoVisibleMenuBuxheti(eshteModifikim) {
    var veprimi = Utils.getUrlVar('planifikim_miratim');
    switch (veprimi) {
        case 'planifikim':
            bejVisibleMenuBuxheti(false, eshteModifikim, true, eshteModifikim); // (visiblePosto, visibleKonverto, visibleGjenero, visibleFshi)
            break;
        case 'miratim':
            bejVisibleMenuBuxheti(eshteModifikim, eshteModifikim, false, eshteModifikim);
            break;
        default:
            bejVisibleMenuBuxheti(false, false, false, eshteModifikim);
            break;
    }
}

function bejVisibleMenuBuxheti(visiblePosto, visibleKonverto, visibleGjenero, visibleFshi) {
    ASPxMenu1.GetItemByName('Posto').SetVisible(visiblePosto);
    ASPxMenu1.GetItemByName('Konverto').SetVisible(visibleKonverto);
    ASPxMenu1.GetItemByName('Gjenero').SetVisible(visibleGjenero);
    ASPxMenu1.GetItemByName('Fshi').SetVisible(visibleFshi);
}

function krijoKokeDokumenti(idStatusDok) {
    var hfMod = $('#hfShtimModifikim');
    var idBuxhetiKoka = ($("#hfId").val() && hfMod.val() == "modifikim") ? $("#hfId").val() : 0;
    var idKokaKonvertimiNga = hfMod.val() == "konvertim" ? $("#hfId").val() : 0;
    var idLlojKonvertimiNga = hfMod.val() == "konvertim" ? hfState.Get("llojKonvertimiNga") : '';

    var clsKokaBuxheti = {};

    clsKokaBuxheti.IdBuxhetiKoka = idBuxhetiKoka ? idBuxhetiKoka : 0;
    clsKokaBuxheti.IdNivel = cmbNiveli.GetValue();
    clsKokaBuxheti.IdKonfigAmbjente = cmbModeli.GetValue();
    clsKokaBuxheti.Viti = cmbViti.GetValue();
    clsKokaBuxheti.NrDok = txtNrDok.GetValue();
    clsKokaBuxheti.DtDok = dteDate.GetDate();
    if (Utils.getUrlVar('planifikim_miratim') == 'planifikim') {
        clsKokaBuxheti.Totali = txtBuxhetiPlanifikuar.GetValue() ? txtBuxhetiPlanifikuar.GetValue() : 0;
        clsKokaBuxheti.TotaliPlanifikuar = clsKokaBuxheti.Totali;
        clsKokaBuxheti.TotaliPaTvsh = clsKokaBuxheti.Totali;
    }
    else if (Utils.getUrlVar('planifikim_miratim') == 'miratim') {
        clsKokaBuxheti.TotaliPlanifikuar = txtBuxhetiPlanifikuar.GetValue() ? txtBuxhetiPlanifikuar.GetValue() : 0;
        clsKokaBuxheti.Totali = txtBuxhetiFaktik.GetValue() ? txtBuxhetiFaktik.GetValue() : 0;
        clsKokaBuxheti.TotaliPaTvsh = clsKokaBuxheti.Totali;
    }
    clsKokaBuxheti.IdStatusDok = idStatusDok;
    clsKokaBuxheti.IdNderm = _idNdermarrje;
    clsKokaBuxheti.IdNdermVit = _idNdermViti;
    clsKokaBuxheti.Shenime = txtShenime.GetValue();
    if (idBuxhetiKoka > 0)
        clsKokaBuxheti.IdPerdoruesi = _idPerdoruesi;
    else
        clsKokaBuxheti.IdKrijuesi = _idPerdoruesi;
    clsKokaBuxheti.IdRaportDesign = cmbFormatiPrintimit.GetValue() ? cmbFormatiPrintimit.GetValue() : 0;
    clsKokaBuxheti.IdKokaKonvertimiNga = idKokaKonvertimiNga; //Id Nga Po Konvertohet per me vone
    clsKokaBuxheti.LlojKonfigKonvertimiNga = idLlojKonvertimiNga; //Lloji nga po konvertohet per me vone
    clsKokaBuxheti.IdKatDok = hfState.Get("idKatDok");

    return clsKokaBuxheti;
}

function krijoTrupDokumenti(idBuxhetiKoka) {
    var hfMod = $('#hfShtimModifikim');
    gridaTrupi.SaveCurrentValues();
    var trupi = gridaTrupi.GetData();

    var colTrupiBuxheti = new Array();
    var clsTrupiBuxheti = {};
    var idBuxhetiTrupi = 0;

    for(var i=0; i<trupi.length; i++){
        clsTrupiBuxheti = {};
        idBuxhetiTrupi = (trupi[i].IdBuxhetiTrupi && hfMod.val() == "modifikim") ? trupi[i].IdBuxhetiTrupi : 0;

        clsTrupiBuxheti.IdBuxhetiKoka = idBuxhetiKoka;
        clsTrupiBuxheti.IdBuxhetiTrupi = idBuxhetiTrupi;
        clsTrupiBuxheti.IdKategoriBuxhetimi = trupi[i].IdKategoriBuxhetimi ? trupi[i].IdKategoriBuxhetimi : 0;
        clsTrupiBuxheti.IdNdermarrje = trupi[i].IdNdermarrjeNiveli3 ? trupi[i].IdNdermarrjeNiveli3 : 0;
        clsTrupiBuxheti.IdTrupiKonvertimiNga = hfMod.val() == "konvertim" ? trupi[i].IdBuxhetiTrupi : 0;
        clsTrupiBuxheti.LlojKonvertimiNga = hfMod.val() == "konvertim" ? hfState.Get("llojKonvertimiNga") : '';
        if (Utils.getUrlVar('planifikim_miratim') == 'miratim') {
            clsTrupiBuxheti.Vlera = trupi[i].VleraFaktike ? trupi[i].VleraFaktike : 0;
            clsTrupiBuxheti.VleraPlanifikuar = trupi[i].VleraPlanifikuar ? trupi[i].VleraPlanifikuar : 0;
            clsTrupiBuxheti.VleraPaTvsh = clsTrupiBuxheti.Vlera;
        }
        else if (Utils.getUrlVar('planifikim_miratim') == 'planifikim') {
            clsTrupiBuxheti.VleraPlanifikuar = trupi[i].VleraPlanifikuar ? trupi[i].VleraPlanifikuar : 0;
            clsTrupiBuxheti.Vlera = clsTrupiBuxheti.VleraPlanifikuar;
            clsTrupiBuxheti.VleraPaTvsh = clsTrupiBuxheti.Vlera;
        }
        colTrupiBuxheti.push(clsTrupiBuxheti);
    }
    return colTrupiBuxheti;
}


function konverto() {
    if (cmbKonverto.GetText() == 'MB')
        myFaqeCelje.kontrolloTeDrejta('B_Shto_RegjistrimBuxheti.aspx?planifikim_miratim=miratim' + '&id=' + idKonvertimi + '&shtim_modifikim=konvertim&niveli=' + cmbKonverto.GetValue() + '&konfigurim=' + cmbKonf.GetValue() + '&llojKonvertimiNga=' + llojKonvertimiNga);
    if (cmbKonverto.GetText() == 'AB')
        myFaqeCelje.kontrolloTeDrejta('B_Shto_RegjistrimAlokimBuxheti.aspx?id=' + idKonvertimi + '&shtim_modifikim=konvertim&niveli=' + cmbKonverto.GetValue() + '&konfigurim=' + cmbKonf.GetValue() + '&llojKonvertimiNga=' + llojKonvertimiNga);
}

function ndryshoNiveli(s, e) {
    cmbKonf.ClearItems();
    for (i = 0; i < konfigurimet.length; i++)
        if (konfigurimet[i].IdNivel == s.GetValue())
            cmbKonf.AddItem(konfigurimet[i].KodKonfigAmbjente, konfigurimet[i].IdKonfigAmbjente); //AddItem(teksti, vlera);
    cmbKonf.SelectIndex(0);
}

function SuccededCallbackKonvertime(result) {
    Utils.hiqLoadingGif();
    var nivelet = result.nivelet;
    var colKonfig = result.colKonfig;
    if (result.mesazh !== "Nuk jane konvertuar") {
        myMesazh.ShtoMesazhGabimi(result.mesazh);
        return;
    }
    if (nivelet.length == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgDokNukMundTeKonvertohet"));
        return;
    }
    cmbKonverto.BeginUpdate();
    cmbKonverto.ClearItems();

    for (i = 0; i < nivelet.length; i++) {
        cmbKonverto.AddItem(nivelet[i].Kodi, nivelet[i].IdNivel); //AddItem(teksti, vlera);
    }

    cmbKonverto.EndUpdate();
    cmbKonverto.SelectIndex(0);

    cmbKonf.BeginUpdate();
    cmbKonf.ClearItems();
    for (i = 0; i < colKonfig.length; i++)
        if (colKonfig[i].IdNivel == cmbKonverto.GetValue())
            cmbKonf.AddItem(colKonfig[i].KodKonfigAmbjente, colKonfig[i].IdKonfigAmbjente); //AddItem(teksti, vlera);
    cmbKonf.EndUpdate();
    cmbKonf.SelectIndex(0);
    konfigurimet = colKonfig;
    idKonvertimi = result.id;
    llojKonvertimiNga = result.llojKonvertimiNga;
    popKonvertim.Show();
}

function ButtonClickKonverto() {
    Utils.shfaqLoadingGif();
    $.ajax({
        pritPergjigje: true, url: Utils.getServerApiUrl("Buxheti", "KontrolloKonvertuar"),
        data: JSON.stringify({ id: $("#hfId").val(), idKonfigKonvertimiNga: cmbModeli.GetValue(), idNdermarrje: _idNdermarrje, idPerdoruesi: _idPerdoruesi, idGjuha: _idGjuha })
    }).done(SuccededCallbackKonvertime);
}

function GjeneroDokumentPlanifikimi() {
    Utils.shfaqLoadingGif();
    $.ajax({
        pritPergjigje: true, url: Utils.getServerApiUrl("Buxheti", "GjeneroPlanifikimBuxheti"),
        data: JSON.stringify({ idKomponente: _idKomponente, idKonfigurim: cmbModeli.GetValue(), idKokaBuxheti: $("#hfId").val() })
    }).done(SuccededGjeneroDokumentPlanifikimi);
}

function SuccededGjeneroDokumentPlanifikimi(result) {
    var status = ShfaqMesazh(result.mesazh);
    if (!status)
        return;

    gridaTrupi.Grida.option("dataSource", new DevExpress.data.ArrayStore(result.dsTrupiBuxheti));
    gridaTrupi.Refresh();
    txtBuxhetiPlanifikuar.SetText(gridaTrupi.GetSummaryOfColumns(["VleraPlanifikuar"], "sum"));
}


function callWebserviceEksportoDokumentMiratimBuxheti() {
    Utils.shfaqLoadingGif();
    unFormatoFushaDevi();
    var wsUrl = Utils.getServerApiUrl('Buxheti', 'EksportoDokumentMiratimBuxheti');
    var params = { idNdermarrje: _idNdermarrje, idkokabuxheti: $("#hfId").val(), idKomponente: _idKomponente, data: dteDate.GetText(), idNiveli: cmbNiveli.GetValue(), llojDok: cmbModeli.GetText(), nrDok: txtNrDok.GetValue(), totaliFaktik: txtBuxhetiFaktik.GetText() };
    Utils.EksportoDokumentNgaWebService(wsUrl, params);
    formatoFushaDevi();
    Utils.hiqLoadingGif();
}