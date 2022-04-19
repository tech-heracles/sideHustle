;

var _idKomponente;
var _idGjuha;
var _idNdermarrje;
var _idNdermViti;
var _idPerdoruesi;
var _idViti;
var colKontrollet;
var colAtrTrupi;
var objekteDefaultKonfig;
var myMesazh;
var ndryshuarNjehere;
var gridaTrupi;
var konfigurimet;
var idKonvertimi;
var llojKonvertimiNga;
var _kokaHapurBuxheti;
var _idNdermarrjeTrupiBija;
var rreshtIndex;

var identifikuesPerPopup = "RialokimBuxheti";
var varKonfig = {
    identifikuesPerLocalStorageKey: 'RialokimBuxheti'
};

var muajt = [{ id: 1, value: "Janar" },
            { id: 2, value: "Shkurt" },
            { id: 3, value: "Mars" },
            { id: 4, value: "Prill" },
            { id: 5, value: "Maj" },
            { id: 6, value: "Qershor" },
            { id: 7, value: "Korrik" },
            { id: 8, value: "Gusht" },
            { id: 9, value: "Shtator" },
            { id: 10, value: "Tetor" },
            { id: 11, value: "Nentor" },
            { id: 12, value: "Dhjetor" }];

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
    myFaqeCelje.changeName("B_Shto_RegjistrimRialokimBuxheti.aspx", 0);
}


function menu_click(s, e) {
    var teDrejtaNiveli = merrTeDrejtaNiveli(cmbNiveli.GetValue());
    switch (e.item.name) {
        case "Shto":
            e.processOnServer = false;
            $("#hfShtimModifikim").val("shtim")
            pastroFusha(true);
            break;
        case "Fshi":
            e.processOnServer = false;
            if (!teDrejtaNiveli.DFsh) {
                myMesazh.ShtoMesazhGabimi("Ju nuk keni te drejta fshirje per nenkategorine: " + cmbNiveli.GetText());
                return;
            }
            callWebserviceFshiDokumentBuxheti();
            break;
        case "Ruaj":
            if(!myFaqeCelje.validim(s, e))
                return;
            e.processOnServer = false;
            if (!teDrejtaNiveli.DShtim) {
                myMesazh.ShtoMesazhGabimi("Ju nuk keni te drejta te ruani dokument per nenkategorine: " + cmbNiveli.GetText());
                return;
            }
            RuajDokumentBuxheti(1);
            break;
        case "Draft":
            if (!myFaqeCelje.validim(s, e))
                return;
            e.processOnServer = false;
            if (!teDrejtaNiveli.DShtimDraft) {
                myMesazh.ShtoMesazhGabimi("Ju nuk keni te drejta te ruani dokument draft per nenkategorine: " + cmbNiveli.GetText());
                return;
            }
            RuajDokumentBuxheti(0);
            break;
        case "Posto":
            e.processOnServer = false;
            if (!teDrejtaNiveli.DAutoKonverto) {
                myMesazh.ShtoMesazhGabimi("Ju nuk keni te drejta te postoni dokument per nenkategorine: " + cmbNiveli.GetText());
                return;
            }
            callWebservicePostoDokumentBuxheti();
            break;
        case "Anullo":
            myFaqeCelje.kontrolloTeDrejta('B_RegjistrimBuxheti.aspx?lloji=rialokim');
            e.processOnServer = false;
            break;
        case "Pezullo":
            e.processOnServer = false;
            if (!teDrejtaNiveli.DPezullo) {
                myMesazh.ShtoMesazhGabimi("Ju nuk keni te drejta te refuzoni dokument per nenkategorine: " + cmbNiveli.GetText());
                return;
            }
            PezulloDokument(txtShenime.GetText());
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
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfigAmbjentiMeFormatNumrash"),
        data: JSON.stringify({ idKomp: _idKomponente, kodKonf: kodKonf, kodKontrolli: "", idObjekti: -1, shtim: eshteShtim, merrFormatKursi: false, merrGjitheKonf: true, idGjuha: _idGjuha, idPerdoruesi: _idPerdoruesi, idNdermarrje: _idNdermarrje })
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
        objekteDefaultKonfig = result.objekteDefault;
        formatNumriZgjedhur = result.formatNumri.KonfigTrupi[0];
        var hf = $("#hfKontrollet");
        var hfMod = $("#hfShtimModifikim");
        var arrTabela = ['tblFillim', 'tblFund'];
        var arrPrind = ['dvFillim', 'dvFundi'];
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, "", arrTabela, undefined, undefined, undefined, arrPrind);
        myFaqeCelje.rregulloGjeresiteFushave(arrTabela, true);
        Utils.konfiguroAccorditionPasKonfigDokumenti(varKonfig.identifikuesPerLocalStorageKey);

        pastroFusha(false);

        if (hfMod.val() == 'modifikim') {
            callWebserviceDokumentBuxheti();
        }
        else {
            bejGatiGride(result.colGrida);
        }


        ndryshuarNjehere = false;
    }
    Utils.hiqLoadingGif();
}

/*
Function: TextChangedNiveli
Therret funksionin <callWebserviceNiveli> per te vendosur templaten sipas nivelit te zgjedhur.
*/
function TextChangedNiveli() {//po
    var mod;
    if ($("#hfShtimModifikim").val != 'shtim')
        mod = true;
    else mod = false;
    if (cmbNiveli.GetText() != "") {
        callWebserviceNiveli(cmbNiveli.GetValue(), mod);
        percaktoMenuSipasTeDrejtavePerNivelin(cmbNiveli.GetValue());
    }
    else callWebserviceNiveli(0, mod);
}

function callWebserviceNiveli(idNiveli, mod) {//po
    try {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheTemplateNiveli"), data: JSON.stringify({ idNiveli: idNiveli, veprimi: "rialokimB", mod: mod, idPerdoruesi: _idPerdoruesi, idNdermarrje: _idNdermarrje, idGjuha: _idGjuha })
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
        data: JSON.stringify({ idKomponente: _idKomponente, idKonfigurim: cmbModeli.GetValue(), idNdermarrje: _idNdermarrje, idGjuha: _idGjuha, emerGride: 'dxDataGrid_trupRialokimi', idKokaBuxheti: $("#hfId").val() })
    }).done(SucceededCallbackDokumentBuxheti);
}

function RuajDokumentBuxheti(idStatusDok) {
    gridaTrupi.SaveCurrentValues();
    var kokaBuxheti = krijoKokeDokumenti(idStatusDok);
    var trupiBuxheti = krijoTrupDokumenti(kokaBuxheti.IdBuxhetiKoka);
    callWebserviceRuajDokument(kokaBuxheti, trupiBuxheti);
}

function callWebservicePostoDokumentBuxheti() {
    Utils.shfaqLoadingGif();
    var komponente = "B_Shto_RegjistrimRialokimBuxheti.aspx";
    $.ajax({
        url: Utils.getServerApiUrl("Buxheti", "PostoDokumentAlokimBuxheti"),
        data: JSON.stringify({ idKokaAlokimi: $("#hfId").val(), komponente: komponente, idNdermarrje: _idNdermarrje, idPerdoruesi: _idPerdoruesi, idViti: _idViti, idSkemaWF: 0, statusAprovimi: 0 })
    }).done(SucceededCallbackPostoDokument);
}

function callWebserviceRuajDokument(kokaBuxheti, trupiBuxheti) {
    Utils.shfaqLoadingGif();
    var komponente = "B_Shto_RegjistrimRialokimBuxheti.aspx";
    $.ajax({
        url: Utils.getServerApiUrl("Buxheti", "RuajDokumentBuxheti"),
        data: JSON.stringify({ komponente: komponente, idNdermarrje: _idNdermarrje, idPerdoruesi: _idPerdoruesi, idViti: _idViti, kokaBuxheti: kokaBuxheti, trupiBuxheti: trupiBuxheti, idSkemaWF: 0, statusAprovimi: 0, idEtapa: 0 }) //, hfNrAuto: hfNrAuto, hfNrAutoBuxheti: hfNrAutoShitje
    }).done(SucceededCallbackRuajDokument);
}

function callWebserviceFshiDokumentBuxheti(){
    Utils.shfaqLoadingGif();
    var komponente = "B_Shto_RegjistrimRialokimBuxheti.aspx";
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Buxheti", "FshiDokumentBuxheti"),
        data: JSON.stringify({ ids: new Array($('#hfId').val()), komponente: komponente })
    }).done(SucceededCallbackFshiDokument)
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
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, "", arrTabela, undefined, undefined, undefined, arrPrind);
        myFaqeCelje.rregulloGjeresiteFushave(arrTabela);
        pastroFusha(true);
    }
}

function SucceededCallbackFshiDokument(result) {
    if (result.mesazhSukses != "")
        myMesazh.ShtoMesazhSuksesi(result.mesazhSukses);
    if (result.mesazhGabim != "")
        myMesazh.ShtoMesazhGabimi(result.mesazhGabim);

    if (result.mesazhSukses != "")
        myFaqeCelje.kontrolloTeDrejta('B_RegjistrimBuxheti.aspx?lloji=rialokim');
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
    var kokaBuxheti = _kokaHapurBuxheti = result.kokaBuxheti;
    _idNdermarrjeTrupiBija = result.dsTrupiBuxheti[0].IdNdermarrje;
    $("#hfId").val(kokaBuxheti.IdBuxhetiKoka);
    mbushFushaKoke(kokaBuxheti, _idNdermarrjeTrupiBija);

    if (kokaBuxheti.IdStatusDok != 0)
        ASPxMenu1.GetItemByName('Draft').SetVisible(false);

    bejGatiGride(result.columnsKonfig, result.dsTrupiBuxheti);
    Utils.hiqLoadingGif();
}

var defaultObject;
function bejGatiGride(columnsKonfig, dataSource) {
    var buxhetiDefault = $.parseJSON(hfState.Get("buxhetiQeveritar"));
    var vitet = $.parseJSON(hfState.Get("vitet"));
    var periudhaDefault = cmbNiveli.GetText() == "Kthim Buxheti" ? 12 : 1;
    defaultObject = { IdBuxhetiTrupi: 0, IdKategoriBuxhetimi: null, Kodi: null, Pershkrimi: null, Buxheti: buxhetiDefault.Kodi, IdBuxheti: buxhetiDefault.IdLlojBuxheti, Viti: vitet[0].idViti, Periudha: periudhaDefault, Vlera: 0 };
    var ds = dataSource ? dataSource : [Utils.CloneObject(defaultObject)];
    var mbushje = false;
    if (dataSource) {
        mbushje = true;
    }
    gridaTrupi = new myDxDataGrid("dxDataGrid_trupRialokimi", {
        dataSource: ds,
        keyExpr: "IdBuxhetiTrupi",
        focusStateEnabled: false,
        editing:{
            mode: "cell",
            allowUpdating: true,
            texts: {
                confirmDeleteMessage: '',
                validationCancelChanges: ''
            }
        },
        searchPanel: { visible: false },
        showRowLines: true,
        sorting: { mode: "none" },
        formatNumriZgjedhur: formatNumriZgjedhur,
        addDeleteRowCommand: true,
        onCellPrepared: function (cell) {
            if (!cell.column.allowEditing)
                cell.cellElement.addClass("dxeDisabled_MetropolisBlue");
        },
        onEditorPreparing: function (e) {
            gridaTrupi.SetSelectionOnFocus(e);
        },
        onInitialized: function(e){

        },
        onContentReady: function (e) {
            if(!mbushje)
                gridaTrupi.ShtoRreshtBosh(defaultObject, "IdKategoriBuxhetimi");
            mbushje = false;
            gridaTrupi.SetLastFocusedCell();
        }
    });

    gridaTrupi.SetColumnsFromConfig(columnsKonfig); //percakton kolonat e grides
    gridaTrupi.AddDataSourceToColumn("Muaji", muajt, "id", "value", false);
    gridaTrupi.AddDataSourceToColumn("Viti", vitet, "idViti", "viti", false);
    gridaTrupi.AddAutocompleteToColumn("Buxheti", "IdBuxheti", undefined, merrLlojeBuxheti, "Zgjidhni buxhetin...", "value", "label", undefined, HapLupeBuxheti);
    gridaTrupi.AddAutocompleteToColumn("Kodi", "IdKategoriBuxhetimi", "Pershkrimi", merrKategoriBuxhetimi, "Zgjidhni kategorine...", "value", "label", "desc", HapLupeKategoriBuxhetimi);
}

function pastroFusha(fshiId) {
    if (fshiId)
        $("#hfId").val(null);
    txtNrDok.SetValue(null);
    txtQendraShendetesore.SetValue(null);
    dteDate.SetValue(Utils.ktheDateDefault($.parseJSON(hfState.Get("periudha"))));
    txtShenime.SetValue(null);
    _kokaHapurBuxheti = null;

    if (gridaTrupi) {
        gridaTrupi.Clean(new DevExpress.data.ArrayStore([Utils.CloneObject(defaultObject)]));
    }

    if ($("#hfShtimModifikim").val() == "shtim") {
        hfNrAuto.Clear();
        hfNrAutoShitje.Clear();
        myNrAuto.vendosNrAutomatik(colAtrTrupi, colKontrollet, dteDate.GetDate(), objekteDefaultKonfig);
    }

    myMenu.menuSipasTeDrejtaRegjistrim($("#hfShtimModifikim"), hfTeDrejta);
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, $("#hfShtimModifikim"), false, "");
    ASPxMenu1.GetItemByName('Draft').SetVisible(true);
}

function mbushFushaKoke(kokaBuxheti, idNdermQendra) {
    txtNrDok.SetValue(kokaBuxheti.NrDok);
    dteDate.SetValue(new Date(kokaBuxheti.DtDok));
    txtShenime.SetValue(kokaBuxheti.Shenime);
    if (kokaBuxheti.IdRaportDesign && $("#hfShtimModifikim").val() != 'konvertim')
        cmbFormatiPrintimit.SetValue(kokaBuxheti.IdRaportDesign);
    cmbModeli.SetValue(kokaBuxheti.IdKonfigAmbjente);
    callWebserviceMerrQendraShendetesore(idNdermQendra);
}


function krijoKokeDokumenti(idStatusDok) {
    var veprimi = $('#hfShtimModifikim').val();
    var idBuxhetiKoka = ($("#hfId").val() && veprimi == "modifikim") ? $("#hfId").val() : 0;
    var idKokaKonvertimiNga = veprimi == "konvertim" ? $("#hfId").val() : 0;
    var idLlojKonvertimiNga = veprimi == "konvertim" ? hfState.Get("llojKonvertimiNga") : '';

    var clsKokaBuxheti = {};

    clsKokaBuxheti.IdBuxhetiKoka = idBuxhetiKoka ? idBuxhetiKoka : 0;
    clsKokaBuxheti.IdNivel = cmbNiveli.GetValue();
    clsKokaBuxheti.IdKonfigAmbjente = cmbModeli.GetValue();
    clsKokaBuxheti.Viti = dteDate.GetValue().getFullYear();
    clsKokaBuxheti.NrDok = txtNrDok.GetValue();
    clsKokaBuxheti.DtDok = dteDate.GetDate();
    clsKokaBuxheti.IdStatusDok = idStatusDok;
    clsKokaBuxheti.IdNderm = _idNdermarrje;
    clsKokaBuxheti.IdNdermVit = _idNdermViti;
    clsKokaBuxheti.Totali = gridaTrupi.GetSummaryOfColumns(new Array("Vlera"), "sum");
    clsKokaBuxheti.TotaliPaTvsh = clsKokaBuxheti.Totali;
    clsKokaBuxheti.Shenime = txtShenime.GetValue();
    if (idBuxhetiKoka > 0)
        clsKokaBuxheti.IdPerdoruesi = _idPerdoruesi;
    else
        clsKokaBuxheti.IdKrijuesi = _idPerdoruesi;
    clsKokaBuxheti.IdRaportDesign = cmbFormatiPrintimit.GetValue() ? cmbFormatiPrintimit.GetValue() : 0;
    clsKokaBuxheti.IdKatDok = hfState.Get("idKatDok");
    clsKokaBuxheti.IdNdermPostuesi = _kokaHapurBuxheti ? _kokaHapurBuxheti.IdNdermPostuesi : 0;
    clsKokaBuxheti.KodNdermPostuesi = _kokaHapurBuxheti ? _kokaHapurBuxheti.KodNdermPostuesi : '';
    clsKokaBuxheti.IdDokPostuesi = _kokaHapurBuxheti ? _kokaHapurBuxheti.IdDokPostuesi : 0;

    return clsKokaBuxheti;
}

function krijoTrupDokumenti(idBuxhetiKoka) {
    var hfMod = $('#hfShtimModifikim');
    var trupi = gridaTrupi.GetData();

    var colTrupiBuxheti = new Array();
    var clsTrupiBuxheti = {};

    for (var i = 0; i < trupi.length; i++) {
        if (!(trupi[i].IdKategoriBuxhetimi && trupi[i].IdKategoriBuxhetimi > 0))
            continue;
        clsTrupiBuxheti = {};
        clsTrupiBuxheti.IdBuxhetiKoka = idBuxhetiKoka;
        clsTrupiBuxheti.IdKategoriBuxhetimi = trupi[i].IdKategoriBuxhetimi;
        clsTrupiBuxheti.Vlera = trupi[i].Vlera ? trupi[i].Vlera : 0;
        clsTrupiBuxheti.VleraPaTvsh = clsTrupiBuxheti.Vlera;
        clsTrupiBuxheti.Periudha = trupi[i].Periudha ? trupi[i].Periudha : 0;
        clsTrupiBuxheti.IdLLojPeriudhe = 1;
        clsTrupiBuxheti.IdBuxheti = trupi[i].IdBuxheti ? trupi[i].IdBuxheti : 0;
        clsTrupiBuxheti.LlogaritNeGjendje = 1;
        clsTrupiBuxheti.IdNdermarrje = trupi[i].IdNdermarrje ? trupi[i].IdNdermarrje : _idNdermarrje;
        colTrupiBuxheti.push(clsTrupiBuxheti);
    }
    return colTrupiBuxheti;
}


function PezulloDokument(shenimet) {
    NdryshoStatusDokumenti(8, shenimet);
}


function NdryshoStatusDokumenti(idStatusi, shenimet) {
    var komponente = "B_Shto_RegjistrimRialokimBuxheti.aspx";
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Buxheti", "NdryshoStatusDokumenti"),
        data: JSON.stringify({ idBuxhetiKoka: new Array($('#hfId').val()), idStatusDok: idStatusi, idPerdoruesi: _idPerdoruesi, idNdermarrje: _idNdermarrje, idViti: _idViti, komponente: komponente, shenime: shenimet })
    }).done(SucceededCallbackNdryshoStatusDokumenti)
}


function SucceededCallbackNdryshoStatusDokumenti(result) {
    if (result.mesazhSukses != "") 
        myMesazh.ShtoMesazhSuksesi(result.mesazhSukses);

    if (result.mesazhGabim != "")
        myMesazh.ShtoMesazhGabimi(result.mesazhGabim);

    Utils.hiqLoadingGif();
}

function callWebserviceMerrQendraShendetesore(idNdermarrje) {
    var komponente = "B_Shto_RegjistrimRialokimBuxheti.aspx";
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Buxheti", "KtheNdermarrjeSipasId"),
        data: JSON.stringify({ idNdermarrje: idNdermarrje })
    }).done(function (result) {
        txtQendraShendetesore.SetValue(result.NdermarrjeKodi);
    });
}

function percaktoMenuSipasTeDrejtavePerNivelin(idNiveli) {
    var teDrejtaNiveli = merrTeDrejtaNiveli(idNiveli);
    hfTeDrejta.Set('Modifikim', teDrejtaNiveli.DMod);
    hfTeDrejta.Set('Shtim', teDrejtaNiveli.DShtim);
    hfTeDrejta.Set('ModifikimDraft', teDrejtaNiveli.DModifikimDraft);
    hfTeDrejta.Set('ShtimDraft', teDrejtaNiveli.DShtimDraft);
    hfTeDrejta.Set('Pezullo', teDrejtaNiveli.DArkiva);
    hfTeDrejta.Set('Posto', teDrejtaNiveli.DKonverto);
    hfTeDrejta.Set('Fshi', teDrejtaNiveli.DFsh);
    myMenu.menuSipasTeDrejtaRegjistrim($("#hfShtimModifikim"), hfTeDrejta);
}

function merrTeDrejtaNiveli(idNiveli) {
    var teDrejtaNivele = JSON.parse(hfState.Get("teDrejtaNivele"));
    return teDrejtaNivele.filter(function (eDrejta) { return eDrejta.IdNivelRegjistrimi == idNiveli; })[0];
}

function HapLupeBuxheti(rowIndex) {
    rreshtIndex = rowIndex;
    popupUniversal.SetHeaderText("Zgjidh Llojin e Buxhetit");
    popupUniversal.SetContentUrl('B_LlojeBuxheti.aspx?idNdermarrje=' + (_idNdermarrjeTrupiBija ? _idNdermarrjeTrupiBija : _idNdermarrje));
    popupUniversal.SetSize(1000, 600);
    popupUniversal.Show();
}

function HapLupeKategoriBuxhetimi(rowIndex) {
    rreshtIndex = rowIndex;
    popupUniversal.SetHeaderText("Zgjidh Kategorine e Buxhetimit");
    popupUniversal.SetContentUrl('B_KategoriBuxhetimi.aspx?lupe=true&idNdermarrje=' + (_idNdermarrjeTrupiBija ? _idNdermarrjeTrupiBija : _idNdermarrje));
    popupUniversal.SetSize(1000, 600);
    popupUniversal.Show();
}

function merrLlojeBuxheti(value, rowIndex, perAcList) {
    var deferred = new jQuery.Deferred();
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Buxheti", "KtheACListeLlojeBuxheti"),
        data: JSON.stringify({ infixText: value ? value : "", idNdermarrje: (_idNdermarrjeTrupiBija ? _idNdermarrjeTrupiBija : _idNdermarrje) }),
        success: function (result) {
            if (!perAcList) {
                var objekti = result.filter(function (item) { return item.label == value || item.desc == value })[0];
                if (!objekti) return;
                gridaTrupi.VendosVleraNeDataSource(rowIndex, "Buxheti", "IdBuxheti", undefined, result[0], "label", "value", undefined);
                gridaTrupi.Refresh();
            }
            else
                deferred.resolve(result);
        }
    });
    return deferred.promise();
}


function merrKategoriBuxhetimi(value, rowIndex, perAcList) {
    var deferred = new jQuery.Deferred();
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Buxheti", "KtheACListeKategoriBuxhetimi"),
        data: JSON.stringify({ infixText: value ? value : "", idNdermarrje: (_idNdermarrjeTrupiBija ? _idNdermarrjeTrupiBija : _idNdermarrje) }),
        success: function (result) {
            if (!perAcList) {
                var objekti = result.filter(function (item) { return item.label == value || item.desc == value })[0];
                if (!objekti) return;
                gridaTrupi.VendosVleraNeDataSource(rowIndex, "Kodi", "IdKategoriBuxhetimi", "Pershkrimi", result[0], "label", "value", "desc");
                gridaTrupi.Refresh();
            }
            else
                deferred.resolve(result);
        }
    });
    return deferred.promise();
}

function VendosBuxhetNeTrup(idBuxheti, kodBuxheti, pershkrimi) {
    var dataSource = gridaTrupi.GetData();
    dataSource[rreshtIndex].IdBuxheti = idBuxheti;
    dataSource[rreshtIndex].Buxheti = kodBuxheti;
    gridaTrupi.Refresh();
    gridaTrupi.Grida.closeEditCell();
    rreshtIndex = null;
}

function VendosKategoriBuxhetiNeTrup(id, kodi, pershkrimi) {
    var dataSource = gridaTrupi.GetData();
    dataSource[rreshtIndex].IdKategoriBuxhetimi = id;
    dataSource[rreshtIndex].Kodi = kodi;
    dataSource[rreshtIndex].Pershkrimi = pershkrimi;
    gridaTrupi.Refresh();
    gridaTrupi.Grida.closeEditCell();
    rreshtIndex = null;
}