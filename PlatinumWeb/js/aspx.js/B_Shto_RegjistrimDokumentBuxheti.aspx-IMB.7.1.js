;
;

var _idKomponente;
var _idGjuha;
var _idNdermarrje;
var _idNdermViti;
var _idPerdoruesi;
var _idViti;
var colKontrollet;
var colAtrTrupi;
var colKushte;
var objekteDefaultKonfig;
var myMesazh;
var gridaTrupi;
var konfigurimet;
var _kokaHapurBuxheti;
var _idNdermarrjeTrupiBija;
var identifikuesPerPopupLlogari = "PerfitimBuxhetimi";
var identifikuesPerPopup = "PerfitimBuxheti";
var identikuesPerPopupKlientFurnitori = "PerfitimBuxheti";
var identifikuesPerPopupBanka = "PerfitimBuxheti";
var idKonvertimi;
var llojKonvertimiNga;
var idSkemaWF;
var idEtapa;
var nivelCmimi;
var ndryshoCmimeShitje = false;
var memoryArt;
var lupaDoktoriKonfig;

var varKonfig = {
    identifikuesPerLocalStorageKey: 'PerfitimBuxheti'
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
    memoryArt = new memory("IdArtikulli");
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
    myFaqeCelje.changeName("B_Shto_RegjistrimDokumentBuxheti.aspx?lloji=" + Utils.getUrlVar('lloji'), 0);
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
            if (!myFaqeCelje.validim(s, e))
                return;
            e.processOnServer = false;
            if (!teDrejtaNiveli.DShtim && ($("#hfShtimModifikim").val() != "konvertim" || cmbNiveli.GetSelectedItem().texts[0] != "IB")) {
                myMesazh.ShtoMesazhGabimi("Ju nuk keni te drejta te ruani dokument per nenkategorine: " + cmbNiveli.GetText());
                return;
            }
            RuajDokumentBuxheti(1, 0);
            break;
        case "Draft":
            if (!myFaqeCelje.validim(s, e))
                return;
            e.processOnServer = false;
            if (!teDrejtaNiveli.DShtimDraft) {
                myMesazh.ShtoMesazhGabimi("Ju nuk keni te drejta te ruani dokument draft per nenkategorine: " + cmbNiveli.GetText());
                return;
            }
            RuajDokumentBuxheti(0, 0);
            break;
        case "Anullo":
            myFaqeCelje.kontrolloTeDrejta('B_RegjistrimBuxheti.aspx?lloji=' + Utils.getUrlVar('lloji'));
            e.processOnServer = false;
            break;
        case "Posto":
            e.processOnServer = false;
            if (!teDrejtaNiveli.DAutoKonverto) {
                myMesazh.ShtoMesazhGabimi("Ju nuk keni te drejta te postoni dokument per nenkategorine: " + cmbNiveli.GetText());
                return;
            }
            callWebservicePostoDokumentBuxheti();
            break;
        case "Pezullo":
            e.processOnServer = false;
            if (!teDrejtaNiveli.DPezullo) {
                myMesazh.ShtoMesazhGabimi("Ju nuk keni te drejta te refuzoni dokument per nenkategorine: " + cmbNiveli.GetText());
                return;
            }
            PezulloDokument(txtShenime.GetText());
            break;
        case "Konverto":
            ButtonClickKonverto();
            e.processOnServer = false;
            break;
        case "Komento":
            if (Utils.getUrlVar('vjenNga') == 'aprovim' || Utils.getUrlVar('vjenNga') == 'kerkese')
                myButtonClickLupa.LupaUniversal_Click('Komentet', 'LupaKomente.aspx?idetapa=' + Utils.getUrlVar('idetapa') + '&nrprocesi=' + Utils.getUrlVar('nrprocesi') + '&veprimi=Gjitha&idkategoria=179', 600, 500);
            else {
                $.ajax({
                    pritPergjigje: true,
                    url: Utils.getServerApiUrl("Rregjistrime", "gjejEtapeDokumenti"),
                    data: JSON.stringify({ idperdoruesi: _idPerdoruesi, idDok: $("#hfId").val(), lloji: "planifikimEkzekutimi" })
                }).done(SucededCallbackLupaKomente);
            }
            e.processOnServer = false;
            break;
        case "Refuzo":
            if (!myFaqeCelje.validim(s, e))
                return;
            e.processOnServer = false;
            //if (!teDrejtaNiveli.DShtimDraft) {
            //    myMesazh.ShtoMesazhGabimi("Ju nuk keni te drejta per kete veprim per nenkategorine: " + cmbNiveli.GetText());
            //    return;
            //}
            RuajDokumentBuxheti(0, 3, idEtapa);
            break;
        case "Delego":
            if (!myFaqeCelje.validim(s, e))
                return;
            e.processOnServer = false;
            //if (!teDrejtaNiveli.DShtimDraft) {
            //    myMesazh.ShtoMesazhGabimi("Ju nuk keni te drejta per kete veprim per nenkategorine: " + cmbNiveli.GetText());
            //    return;
            //}
            RuajDokumentBuxheti(0, 4, idEtapa);
            break;
        case "Modifiko":
            if (!myFaqeCelje.validim(s, e))
                return;
            e.processOnServer = false;
            //if (!teDrejtaNiveli.DShtimDraft) {
            //    myMesazh.ShtoMesazhGabimi("Ju nuk keni te drejta per kete veprim per nenkategorine: " + cmbNiveli.GetText());
            //    return;
            //}
            RuajDokumentBuxheti(0, 0, idEtapa);
            break;
        case "Aprovo": 
            if (!myFaqeCelje.validim(s, e))
                return;
            e.processOnServer = false;
            //if (!teDrejtaNiveli.DShtimDraft) {
            //    myMesazh.ShtoMesazhGabimi("Ju nuk keni te drejta per kete veprim per nenkategorine: " + cmbNiveli.GetText());
            //    return;
            //}
            RuajDokumentBuxheti(0, 1, idEtapa);
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
    var eshteShtim = $("#hfShtimModifikim").val() == 'shtim';
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
        idSkemaWF = 0;
        idetapa = 0;
        colKontrollet = result.colKontroll;
        colAtrTrupi = result.colAtrTrupi;
        objekteDefaultKonfig = result.objekteDefault;
        formatNumriZgjedhur = result.formatNumri.KonfigTrupi[0];
        colKushte = result.colKushte;
        
        var hf = $("#hfKontrollet");
        var hfMod = $("#hfShtimModifikim");
        var arrTabela = ['tblFillim','tblFund'];
        var arrPrind = ['dvFillim', 'dvFundi'];

        if (hfState.Get("idKatDok") == 179) {
            for (var j = 0; j < colKushte.length; j++) {
                var kusht = colKushte[j];
                lblStatusApr.SetVisible(false);
                lblStatusAprovimi.SetVisible(false);
                switch (kusht.Kodi) {
                    case "ZSP":
                        idSkemaWF = kusht.Vlera;
                        var isNotModifikim = (hfMod.val() == 'modifikim' ? false : true);
                        if (!isNotModifikim) { // && lblStatusAprovimi.GetText() != ""
                            $.ajax({
                                pritPergjigje: true,
                                url: Utils.getServerApiUrl("Rregjistrime", "MerrMenuPerPerdoruesSipasSkemes"), data: JSON.stringify({ idskema: kusht.Vlera, idperdoruesi: _idPerdoruesi, isNotModifikim: isNotModifikim, status: lblStatusAprovimi.GetText(), idkokashitje: $("#hfId").val(), kodkonf: cmbModeli.GetText(), idlloji: 179 })
                            }).done(percaktoMenuItemsSipasSkemesWF);
                        }
                        else bejVisibleMenuBuxhetiAprovimi(false, false, false, false, false, true, true);

                        if (kusht.Vlera != 0 && !isNotModifikim) {
                            lblStatusApr.SetVisible(true);
                            lblStatusAprovimi.SetVisible(true);
                            if (Utils.getUrlVar('vjenNga') != "undefined") // etapa e hapur
                                idetapa = Utils.getUrlVar('idetapa');
                            else {//etapa e fundit kur hapet nga shitja
                                $.ajax({
                                    pritPergjigje: true,
                                    url: Utils.getServerApiUrl("Rregjistrime", "gjejEtapeDokumenti"), data: JSON.stringify({ idperdoruesi: _idPerdoruesi, idDok: $("#hfId").val(), lloji: "planifikimEkzekutimi" })
                                }).done(vendosIdEtapeAprovimi);
                            }
                        }
                        break;
                    default:
                        bejVisibleMenuBuxhetiAprovimi(false, false, false, false, false, true, true);
                        break;
                }
            }
        }

        if (Utils.getUrlVar("lloji") == "perfitim") {
            var idKontrollDoktori = colKontrollet.filter(function (item) { return item.KodKontrolli == "btnDoktori" })[0].IdKontrolli;
            lupaDoktori = colAtrTrupi.filter(function (item) { return item.IdKontroll == idKontrollDoktori })[0].IdKonfigAmbjenteLupa;
        }

        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, "", arrTabela, undefined, undefined, undefined, arrPrind);
        myFaqeCelje.rregulloGjeresiteFushave(arrTabela);
        Utils.konfiguroAccorditionPasKonfigDokumenti(varKonfig.identifikuesPerLocalStorageKey);

        Utils.setFormatNumri(txtTotali, formatNumriZgjedhur.ShifraPasPresjesVlefta || 2);
        Utils.setFormatNumri(txtTotaliPaTvsh, formatNumriZgjedhur.ShifraPasPresjesVlefta || 2);
        Utils.setFormatNumri(txtTvsh, formatNumriZgjedhur.ShifraPasPresjesVlefta || 2);

        pastroFusha(false);

        if (hfMod.val() == 'modifikim' || hfMod.val() == 'konvertim') {
            callWebserviceDokumentBuxheti();
        }
        else {
            bejGatiGride(result.colGrida);
        }
    }
    Utils.hiqLoadingGif();
}

/*
Function: TextChangedNiveli
Therret funksionin <callWebserviceNiveli> per te vendosur templaten sipas nivelit te zgjedhur.
*/
function TextChangedNiveli() {//po
    var mod;
    if ($("#hfShtimModifikim").val() != 'shtim')
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
            url: Utils.getServerApiUrl("Rregjistrime", "ktheTemplateNiveli"), data: JSON.stringify({ idNiveli: idNiveli, veprimi: Utils.getUrlVar("lloji")+"B", mod: mod, idPerdoruesi: _idPerdoruesi, idNdermarrje: _idNdermarrje, idGjuha: _idGjuha })
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
    if ($("#hfShtimModifikim").val() == "konvertim")
        $.ajax({
            url: Utils.getServerApiUrl("Buxheti", "ktheDokumentBuxhetiNgaKonvertimi"),
            data: JSON.stringify({ idKomponente: _idKomponente, idKonfigurim: cmbModeli.GetValue(), idNdermarrje: _idNdermarrje, idGjuha: _idGjuha, emerGride: 'dxDataGrid_trupPerfitimi', idKokaBuxheti: $("#hfId").val(), llojKonvertimiNga: hfState.Get("llojKonvertimiNga")})
        }).done(SucceededCallbackDokumentBuxheti);
    else
        $.ajax({
            url: Utils.getServerApiUrl("Buxheti", "KtheDokumentBuxheti"),
            data: JSON.stringify({ idKomponente: _idKomponente, idKonfigurim: cmbModeli.GetValue(), idNdermarrje: _idNdermarrje, idGjuha: _idGjuha, emerGride: 'dxDataGrid_trupPerfitimi', idKokaBuxheti: $("#hfId").val() })
        }).done(SucceededCallbackDokumentBuxheti);
}

function RuajDokumentBuxheti(idStatusDok, statusAprovimi, idEtapa) {
    unFormatoFushaDevi();
    gridaTrupi.SaveCurrentValues();
    var kokaBuxheti = krijoKokeDokumenti(idStatusDok);
    var trupiBuxheti = krijoTrupDokumenti(kokaBuxheti.IdBuxhetiKoka);
    callWebserviceRuajDokument(kokaBuxheti, trupiBuxheti, statusAprovimi, idEtapa);
}

function callWebserviceRuajDokument(kokaBuxheti, trupiBuxheti, statusAprovimi, idEtapa) {
    Utils.shfaqLoadingGif();
    var komponente = "B_Shto_RegjistrimDokumentBuxheti.aspx?lloji=" + Utils.getUrlVar('lloji');
    $.ajax({
        url: Utils.getServerApiUrl("Buxheti", "RuajDokumentBuxheti"),
        data: JSON.stringify({ komponente: komponente, idNdermarrje: _idNdermarrje, idPerdoruesi: _idPerdoruesi, idViti: _idViti, kokaBuxheti: kokaBuxheti, trupiBuxheti: trupiBuxheti, idSkemaWF: idSkemaWF, statusAprovimi: statusAprovimi, idEtapa: idEtapa })
    }).done(SucceededCallbackRuajDokument);
}

function callWebserviceFshiDokumentBuxheti() {
    Utils.shfaqLoadingGif();
    var komponente = "B_Shto_RegjistrimDokumentBuxheti.aspx?lloji=" + Utils.getUrlVar('lloji');
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Buxheti", "FshiDokumentBuxheti"),
        data: JSON.stringify({ ids: new Array($('#hfId').val()), komponente: komponente })
    }).done(SucceededCallbackFshiDokument)
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
        return;
    }
    formatoFushaDevi();
}

function SucceededCallbackFshiDokument(result) {
    if (result.mesazhSukses != "")
        myMesazh.ShtoMesazhSuksesi(result.mesazhSukses);
    if (result.mesazhGabim != "")
        myMesazh.ShtoMesazhGabimi(result.mesazhGabim);

    if (result.mesazhSukses != "")
        myFaqeCelje.kontrolloTeDrejta('B_RegjistrimBuxheti.aspx?lloji=' + Utils.getUrlVar('lloji'));
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
    _idNdermarrjeTrupiBija = result.dsTrupiBuxheti[0].IdNdermarrje;
    var kokaBuxheti = _kokaHapurBuxheti = result.kokaBuxheti;
    if ($("#hfShtimModifikim").val() != 'konvertim')
        $("#hfId").val(kokaBuxheti.IdBuxhetiKoka);
    else
    {
        kokaBuxheti.KodNdermPostuesi = "";
        kokaBuxheti.IdNdermPostuesi = 0;
        kokaBuxheti.IdDokPostuesi = 0;
    }
    mbushFushaKoke(kokaBuxheti, _idNdermarrjeTrupiBija, result.objekteKoke);

    if (kokaBuxheti.IdStatusDok != 0 && $("#hfShtimModifikim").val() != 'konvertim')
        ASPxMenu1.GetItemByName('Draft').SetVisible(false);

    bejGatiGride(result.columnsKonfig, result.dsTrupiBuxheti);
    Utils.hiqLoadingGif();
}

var defaultObject;
var mbushje = false;
function bejGatiGride(columnsKonfig, dataSource) {
    var buxhetiDefault = $.parseJSON(hfState.Get("buxhetiSekondar"));
    var eshtePerfitim = Utils.getUrlVar("lloji") == "perfitim";
    defaultObject = { IdBuxhetiTrupi: 0, IdLlojObjekti: eshtePerfitim ? 1 : 3, Zeri: null, IdObjekti: null, Pershkrimi: null, Buxheti: eshtePerfitim ? buxhetiDefault.Kodi : null, IdBuxheti: eshtePerfitim ? buxhetiDefault.IdLlojBuxheti : null, VleraPaTvsh: 0, TVSH: 0, Vlera: 0, Sasia: 1, Cmimi: 0 }
    var ds = dataSource ? dataSource : [Utils.CloneObject(defaultObject)];
    if (dataSource) {
        mbushje = true;
    }
    gridaTrupi = new myDxDataGrid("dxDataGrid_trupPerfitimi", {
        dataSource: ds,
        keyExpr: "IdBuxhetiTrupi",
        focusStateEnabled: false,
        editing: {
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
            if (!e.parentType == "dataRow")
                return;
            gridaTrupi.SetSelectionOnFocus(e);
            if (e.dataField === "IdLlojObjekti")
                onChangeValueLlojObjektiFunction(e);
            if (e.dataField === "Vlera" || e.dataField === "VleraPaTvsh" || e.dataField === "TVSH" || e.dataField === "Cmimi" || e.dataField === "Sasia") {
                e.editorOptions.disabled = ((e.row.data.IdLlojObjekti === 3 && (e.dataField == "Sasia" || e.dataField == "Cmimi")) || (e.row.data.IdLlojObjekti === 1 && !ndryshoCmimeShitje && e.dataField != "TVSH" && e.dataField != "Sasia"));
                onChangeValueVlerat(e);
            }
        },
        onContentReady: function (e) {
            if (!mbushje) {
                gridaTrupi.ShtoRreshtBosh(defaultObject, "Zeri");
            }
            gridaTrupi.SetLastFocusedCell();
            llogaritVleratTotale();
        }
    });

    gridaTrupi.SetColumnsFromConfig(columnsKonfig); //percakton kolonat e grides
    gridaTrupi.AddDataSourceToColumn("IdLlojObjekti", $.parseJSON(hfState.Get("llojeObjekti")), "IdLlojObjekti", "LlojObjekti", false);
    gridaTrupi.AddAutocompleteToColumn("Zeri", "IdObjekti", "Pershkrimi", merrLlogari, "Zgjidhni zerin...", "value", "label", "desc", HapLupeLlogarie, onValueChangedZeri);
    gridaTrupi.AddAutocompleteToColumn("Buxheti", "IdBuxheti", undefined, merrLlojeBuxheti, "Zgjidhni buxhetin...", "value", "label", undefined, HapLupeBuxheti);
    callWebServiceListeTaksash();
    if ($("#hfShtimModifikim").val() == 'konvertim') {
        gridaTrupi.Options.allowNewRowAdding = false;
        gridaTrupi.AddCustomOptionToColumns(["IdLlojObjekti", "Zeri", "Buxheti", "TVSH"], "allowEditing", false);
    }
}

var rreshtIndex;


function onChangeValueLlojObjektiFunction(e) {
    var rowIndex = e.row && e.row.rowIndex;
    e.editorOptions.onValueChanged = function (e) {
        gridaTrupi.GetData()[rowIndex].IdLlojObjekti = e.value;
        //gridaTrupi.VendosVleraNeDataSource(rowIndex, "Zeri", "IdObjekti", "Pershkrimi", null, "label", "value", "desc");
        //gridaTrupi.VendosVleraNeDataSource(rowIndex, "Buxheti", "IdBuxheti", undefined, null, "label", "value", undefined);
        gridaTrupi.PastroFusha(rowIndex, defaultObject, ["Zeri", "IdObjekti", "Pershkrimi", "Vlera", "VleraPaTvsh", "TVSH", "Vlera", "Sasia", "Cmimi"]);
    }
}

function VendosBuxhetNeTrup(idBuxheti, kodBuxheti, pershkrimi) {
    var dataSource = gridaTrupi.GetData();
    dataSource[rreshtIndex].IdBuxheti = idBuxheti;
    dataSource[rreshtIndex].Buxheti = kodBuxheti;
    gridaTrupi.Refresh();
    gridaTrupi.Grida.closeEditCell();
    rreshtIndex = null;
}
function HapLupeLlogarie(rowIndex) {
    rreshtIndex = rowIndex;
    var idNdermarrjeLlogarie = (_idNdermarrjeTrupiBija ? _idNdermarrjeTrupiBija : _idNdermarrje);
    var idPerdoruesLlogarie = (_kokaHapurBuxheti ? _kokaHapurBuxheti.IdKrijuesi : _idPerdoruesi);
    var filterArtikulli;

    switch (cmbNiveli.GetSelectedItem().texts[0]) {
        case "PIB":
        case "IB":
            filterArtikulli = "&aqt=aqt&filterKlasa=1";
            break;
        case "PEB":
        case "EB":
            filterArtikulli = "&filterKlasa=1";
            break;
        default:
            filterArtikulli = "&filterKlasa=3";
            break;
    }

    var dataSource = gridaTrupi.GetData();
    switch (dataSource[rreshtIndex].IdLlojObjekti) {
        case 3:
            popupUniversal.SetHeaderText("Zgjidh Llogarine");
            popupUniversal.SetContentUrl('LupaLlogaria.aspx?idNdermarrje=' + idNdermarrjeLlogarie + '&idPerdoruesi=' + idPerdoruesLlogarie);
            break;
        case 1:
            popupUniversal.SetHeaderText("Zgjidh Artikullin");
            popupUniversal.SetContentUrl('LupaArtikull.aspx?shitshem=true&idNdermarrje=' + idNdermarrjeLlogarie + filterArtikulli );
            break;
        default:
            break;
    }
    popupUniversal.SetSize(750, 600);
    popupUniversal.Show();
}

function HapLupeBuxheti(rowIndex) {
    var idLlogaria = merrIdLlogariNgaRreshti(rowIndex);
    rreshtIndex = rowIndex;
    var eshtePerfitim = Utils.getUrlVar("lloji") == "perfitim";
    popupUniversal.SetHeaderText("Zgjidh Llojin e Buxhetit");
    popupUniversal.SetContentUrl('B_LlojeBuxheti.aspx?idNdermarrje=' + (_idNdermarrjeTrupiBija ? _idNdermarrjeTrupiBija : _idNdermarrje) + (!eshtePerfitim ? ("&idLlogaria=" + idLlogaria) : ""));
    popupUniversal.SetSize(1000, 600);
    popupUniversal.Show();
}

function VendosLlogariArtikullNeTrup(kodi, id, pershkrimi, IdLlogariBlerje, IdLlogariTeTrete) {
    var dataSource = gridaTrupi.GetData();
    dataSource[rreshtIndex].IdObjekti = id;
    dataSource[rreshtIndex].Zeri = kodi;
    dataSource[rreshtIndex].Pershkrimi = pershkrimi;
    gridaTrupi.Refresh();
    gridaTrupi.Grida.closeEditCell();
    callWebServiceCmimArtikulli(kodi, rreshtIndex);
    if (gridaTrupi.GetData()[rreshtIndex].IdLlojObjekti == 1 && id > 0)
        memoryArt.Set({ IdArtikulli: id, KodArtikulli: kodi, PershkrimArtikulli: pershkrimi, IdLlogariBlerje: IdLlogariBlerje, IdLlogariTeTrete: IdLlogariTeTrete });
    merrLlojeBuxheti("", rreshtIndex, false, true);
    rreshtIndex = null;
}

var cellDisplayText;
function Selecteditemchange(e, cellInfo) {
    var selectedvalue = e.component.option("value");
    $("#SelectedValue").html("This is only to show the selected value " + selectedvalue);
}

function merrLlogari(value, rowIndex, perAcList) {
    var deferred = new jQuery.Deferred();
    var wsData, wsURL;
    var idNderm = _idNdermarrjeTrupiBija ? _idNdermarrjeTrupiBija : _idNdermarrje;
    var idPerd = _kokaHapurBuxheti ? _kokaHapurBuxheti.IdKrijuesi : _idPerdoruesi;
    var merrVetemAqt = (cmbNiveli.GetSelectedItem().texts[0] == "PIB" || cmbNiveli.GetSelectedItem().texts[0] == "IB");
    var llojObjekti = gridaTrupi.GetData()[rowIndex].IdLlojObjekti;
    switch (llojObjekti) {
        case 3:
            wsData = { infixText: value ? value : "", pershk: 1, idNdermarrje: idNderm, idPerdoruesi: idPerd };
            wsURL = Utils.getServerApiUrl("Rregjistrime", "ktheACListeLlogarish");
            break;
        case 1:
            wsData = { infixText: value ? value : "", pershk: 1, grup: "", idNdermarrje: idNderm, idPerdoruesi: idPerd, artikujTeShitshem: true, merrVetemAfatgjate: merrVetemAqt, merrSipasDetajimit: false, klasa: Utils.getUrlVar("lloji") == "perfitim" ? "Sherbim" : "Inventar" };
            wsURL = Utils.getServerApiUrl("Rregjistrime", "ktheACListeArtikujshKodPershkKodbarFull");
            break;
        default:
            return;
    }
    $.ajax({
        pritPergjigje: true,
        url: wsURL,
        data: JSON.stringify(wsData),
        success: function (result) {
            if (!perAcList) {
                var objekti = result.filter(function (item) { return item.label.toLowerCase() == value.toLowerCase() || item.desc.toLowerCase() == value.toLowerCase() })[0];
                if (!objekti) return;
                gridaTrupi.VendosVleraNeDataSource(rowIndex, "Zeri", "IdObjekti", "Pershkrimi", result[0], "label", "value", "desc", onValueChangedZeri);
                gridaTrupi.Refresh();
            }
            else
                deferred.resolve(result);
        }
    });
    return deferred.promise();
}

function merrLlojeBuxheti(value, rowIndex, perAcList, ngarkoTeParin) {
    var idLlogaria = merrIdLlogariNgaRreshti(rowIndex);
    var eshtePerfitim = Utils.getUrlVar("lloji") == "perfitim";
    var deferred = new jQuery.Deferred();
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Buxheti", eshtePerfitim ? "KtheACListeLlojeBuxheti" : "KtheACListeLlojeBuxhetiSipasLlogarise"),
        data: JSON.stringify({ infixText: value ? value : "", idLlogaria: idLlogaria, idNdermarrje: (_idNdermarrjeTrupiBija ? _idNdermarrjeTrupiBija : _idNdermarrje) }),
        success: function (result) {
            if (!perAcList) {
                var objekti = ngarkoTeParin ? result[0] : result.filter(function (item) { return item.label.toLowerCase() == value.toLowerCase() || item.desc.toLowerCase() == value.toLowerCase() })[0];
                if (!objekti) return;
                gridaTrupi.VendosVleraNeDataSource(rowIndex, "Buxheti", "IdBuxheti", undefined, objekti, "label", "value", undefined);
                gridaTrupi.Refresh();
            }
            else
                deferred.resolve(result);
        }
    });
    return deferred.promise();
}

function callWebServiceListeTaksash() {
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Buxheti", "ktheListeTaksashSipasNdermarrjes"),
        data: JSON.stringify({ idPerdoruesi: _idPerdoruesi, idNdermarrje: (_idNdermarrjeTrupiBija ? _idNdermarrjeTrupiBija : _idNdermarrje) })
    }).done(function (result) {
        gridaTrupi.AddDataSourceToColumn("TVSH", result, "IdTaksa", "KodTaksa", false);
        mbushje = false;
    });
}

function pastroFusha(fshiId) {
    if (fshiId)
        $("#hfId").val(null);
    txtNrDok.SetValue(null);
    dteDate.SetValue(Utils.ktheDateDefault($.parseJSON(hfState.Get("periudha"))));
    txtShenime.SetValue(null);
    txtTotali.SetText(0);
    txtTotaliPaTvsh.SetText(0);
    txtTvsh.SetText(0);
    cmbLlojVeprimi.SetSelectedIndex(0);
    cmbEntiteti.SetSelectedIndex(-1);
    btnDoktori.SetSelectedIndex(-1);
    lblStatusAprovimi.SetText('');

    if(Utils.getUrlVar("lloji") == 'perfitim')
        cmbBurimi.SetSelectedIndex(0);
    else
        cmbBurimi.SetSelectedIndex(-1);

    if (gridaTrupi) {
        gridaTrupi.Clean(new DevExpress.data.ArrayStore([Utils.CloneObject(defaultObject)]));
    }
    if ($("#hfShtimModifikim").val() == "shtim") {
        hfNrAuto.Clear();
        hfNrAutoShitje.Clear();
        myNrAuto.vendosNrAutomatik(colAtrTrupi, colKontrollet, dteDate.GetDate(), objekteDefaultKonfig);

        if (gridaTrupi) {
            gridaTrupi.Options.allowNewRowAdding = true;
            gridaTrupi.AddCustomOptionToColumns(["IdLlojObjekti", "Zeri", "Buxheti", "TVSH"], "allowEditing", true);
        }
    }

    myMenu.menuSipasTeDrejtaRegjistrim($("#hfShtimModifikim"), hfTeDrejta);
    ndryshoCmimeShitje = hfTeDrejta.Get("NdryshoCmimShitje");

    percaktoVisibleMenuBuxheti($("#hfShtimModifikim").val() == 'modifikim');
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, $("#hfShtimModifikim"), false, "");
    ASPxMenu1.GetItemByName('Draft').SetVisible(true);

    if ($("#hfShtimModifikim").val() == "konvertim" && cmbNiveli.GetSelectedItem().texts[0] == "IB") {
        ASPxMenu1.GetItemByName('Ruaj').SetEnabled(true);
    }

    ASPxMenu1.GetItemByName('Draft').SetVisible(true);
    formatoFushaDevi();
}

function mbushFushaKoke(kokaBuxheti, idNdermQendra, objekteKoke) {
    txtNrDok.SetValue(kokaBuxheti.NrDok);
    dteDate.SetValue(new Date(kokaBuxheti.DtDok));
    txtShenime.SetValue(($('#hfShtimModifikim').val() == 'konvertim' && kokaBuxheti.IdKatDok == 179) ? '' : kokaBuxheti.Shenime);
    if (kokaBuxheti.IdRaportDesign && $('#hfShtimModifikim').val() != 'konvertim')
        cmbFormatiPrintimit.SetValue(kokaBuxheti.IdRaportDesign);
    cmbModeli.SetValue($('#hfShtimModifikim').val() == 'konvertim'? Utils.getUrlVar("konfigurim") : kokaBuxheti.IdKonfigAmbjente);
    txtTotali.SetText(kokaBuxheti.Totali);
    txtTotaliPaTvsh.SetText(kokaBuxheti.TotaliPaTvsh);
    txtTvsh.SetText(kokaBuxheti.Totali - kokaBuxheti.TotaliPaTvsh);
    cmbLlojVeprimi.SetValue(kokaBuxheti.LlojVeprimiGjenerimi);
    if (objekteKoke.entiteti.value > 0) {
        Utils.ShtoNeseNukGjendetDheSelektoCombo(cmbEntiteti, objekteKoke.entiteti.value, objekteKoke.entiteti.text);
        cmbEntiteti.SetValue(kokaBuxheti.IdEntiteti);
    }
    nivelCmimi = objekteKoke.idNivelCmimi;
    var eshteDokGjenerues = hfState.Get("eshteDokGjenerues");
    cmbLlojVeprimi.SetEnabled(!eshteDokGjenerues);
    cmbEntiteti.SetEnabled(!eshteDokGjenerues);
    hfState.Set("eshteDokGjenerues", false);
    cmbBurimi.SetValue(kokaBuxheti.IdBurimi);

    if (objekteKoke.doktori.value > 0) {
        Utils.ShtoNeseNukGjendetDheSelektoCombo(btnDoktori, objekteKoke.doktori.value, objekteKoke.doktori.text);
        btnDoktori.SetValue(kokaBuxheti.Doktori);
    }

    formatoFushaDevi();
}


function krijoKokeDokumenti(idStatusDok) {
    var veprimi = $('#hfShtimModifikim').val();
    var idBuxhetiKoka = ($("#hfId").val() && veprimi == "modifikim") ? $("#hfId").val() : 0;
    var idKokaKonvertimiNga = veprimi == "konvertim" ? $("#hfId").val() : veprimi == "modifikim" && _kokaHapurBuxheti ? _kokaHapurBuxheti.IdKokaKonvertimiNga : 0;
    var idLlojKonvertimiNga = veprimi == "konvertim" ? hfState.Get("llojKonvertimiNga") : veprimi == "modifikim" && _kokaHapurBuxheti ? _kokaHapurBuxheti.LlojKonfigKonvertimiNga : '';

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
    clsKokaBuxheti.Totali = txtTotali.GetText();
    clsKokaBuxheti.TotaliPaTvsh = txtTotaliPaTvsh.GetText();
    clsKokaBuxheti.Shenime = txtShenime.GetValue();
    if (idBuxhetiKoka > 0) {
        clsKokaBuxheti.IdPerdoruesi = _idPerdoruesi;
    }
    else
        clsKokaBuxheti.IdKrijuesi = _idPerdoruesi;
    clsKokaBuxheti.IdRaportDesign = cmbFormatiPrintimit.GetValue() ? cmbFormatiPrintimit.GetValue() : 0;
    clsKokaBuxheti.IdKatDok = hfState.Get("idKatDok");
    clsKokaBuxheti.IdEntiteti = cmbEntiteti.GetValue() ? cmbEntiteti.GetValue() : 0;
    clsKokaBuxheti.LlojVeprimiGjenerimi = cmbLlojVeprimi.GetValue() ? cmbLlojVeprimi.GetValue() : 0;
    clsKokaBuxheti.IdRaportDesign = cmbFormatiPrintimit.GetValue() ? cmbFormatiPrintimit.GetValue() : 0;
    clsKokaBuxheti.IdKokaKonvertimiNga = idKokaKonvertimiNga; //Id Nga Po Konvertohet per me vone
    clsKokaBuxheti.LlojKonfigKonvertimiNga = idLlojKonvertimiNga; //Lloji nga po konvertohet per me vone

    clsKokaBuxheti.IdNdermPostuesi = _kokaHapurBuxheti ? _kokaHapurBuxheti.IdNdermPostuesi : 0;
    clsKokaBuxheti.KodNdermPostuesi = _kokaHapurBuxheti ? _kokaHapurBuxheti.KodNdermPostuesi : '';
    clsKokaBuxheti.IdDokPostuesi = _kokaHapurBuxheti ? _kokaHapurBuxheti.IdDokPostuesi : 0;

    clsKokaBuxheti.DtKrijimi = _kokaHapurBuxheti ? _kokaHapurBuxheti.DtKrijimi : dteDate.GetDate();

    clsKokaBuxheti.StatusAprovimi = _kokaHapurBuxheti ? (_kokaHapurBuxheti.StatusAprovimi ? _kokaHapurBuxheti.StatusAprovimi : 0) : 0;
    clsKokaBuxheti.IdBurimi = cmbBurimi.GetValue() ? cmbBurimi.GetValue() : 0;
    clsKokaBuxheti.Doktori = btnDoktori.GetValue() ? btnDoktori.GetValue() : 0;
    return clsKokaBuxheti;
}

function krijoTrupDokumenti(idBuxhetiKoka) {
    var hfMod = $('#hfShtimModifikim');
    gridaTrupi.SaveCurrentValues();
    var trupi = gridaTrupi.GetData();

    var colTrupiBuxheti = new Array();
    var clsTrupiBuxheti = {};
    for (var i = 0; i < trupi.length; i++) {
        if (!(trupi[i].IdObjekti && trupi[i].IdObjekti > 0))
            continue;
        clsTrupiBuxheti = {};
        clsTrupiBuxheti.IdBuxhetiKoka = idBuxhetiKoka;
        clsTrupiBuxheti.IdObjekti = trupi[i].IdObjekti;
        clsTrupiBuxheti.LlojObjekti = trupi[i].IdLlojObjekti;
        clsTrupiBuxheti.Vlera = trupi[i].Vlera ? trupi[i].Vlera : 0;
        clsTrupiBuxheti.VleraPaTvsh = trupi[i].VleraPaTvsh ? trupi[i].VleraPaTvsh : 0;
        clsTrupiBuxheti.IdTvsh = trupi[i].TVSH ? trupi[i].TVSH : 0;
        clsTrupiBuxheti.Periudha = trupi[i].Periudha ? trupi[i].Periudha : 0;
        clsTrupiBuxheti.IdBuxheti = trupi[i].IdBuxheti ? trupi[i].IdBuxheti : 0;
        clsTrupiBuxheti.LlogaritNeGjendje = 1;
        clsTrupiBuxheti.IdNdermarrje = _idNdermarrjeTrupiBija ? _idNdermarrjeTrupiBija : _idNdermarrje;
        clsTrupiBuxheti.IdTrupiKonvertimiNga = hfMod.val() == "konvertim" ? trupi[i].IdBuxhetiTrupi : hfMod.val() == "modifikim" && trupi[i].IdTrupiKonvertimiNga ? trupi[i].IdTrupiKonvertimiNga : 0;
        clsTrupiBuxheti.LlojKonvertimiNga = hfMod.val() == "konvertim" ? hfState.Get("llojKonvertimiNga") : hfMod.val() == "modifikim" && trupi[i].LlojKonvertimiNga ? trupi[i].LlojKonvertimiNga : '';
        clsTrupiBuxheti.Cmimi = trupi[i].Cmimi ? trupi[i].Cmimi : 0;
        clsTrupiBuxheti.Sasia = trupi[i].Sasia ? trupi[i].Sasia : 0;
        colTrupiBuxheti.push(clsTrupiBuxheti);
    }
    return colTrupiBuxheti;
}


function ButtonClickEntiteti() {
    switch (cmbLlojVeprimi.GetText()) {
        case "Shitje":
            myButtonClickLupa.LupaUniversal_Click("Zgjidhni Klientin", "LupaKlientFurnitor.aspx?KlientapoFurnitor=Klient", 950, 560);
            break;
        case "Blerje":
            myButtonClickLupa.LupaUniversal_Click("Zgjidhni Furnitorin", "LupaKlientFurnitor.aspx?KlientapoFurnitor=Furnitor", 950, 560);
            break;
        case "Arketim":
        case "Pagese":
            myButtonClickLupa.LupaUniversal_Click("Zgjidhni Arken", "LupaBanka.aspx?arkabanka=3&arka=false", 950, 560);
            break;
    }
}


function onChangeValueVlerat(e) {
    var dataField = e.dataField;
    var rowIndex = e.row && e.row.rowIndex;
    e.editorOptions.onValueChanged = function (e) {
        vendosVleraNeRresht(e.value, dataField, gridaTrupi.GetData()[rowIndex]) ;
    }
}

function vendosVleraNeRresht(value, dataField, rowData) {
    var taksaItem, TVSH = 0;
    taksaItem = gridaTrupi.GetColumnDataSourceItem("TVSH", dataField == "TVSH" ? value : rowData.TVSH);
    if (!taksaItem)
        return;
    TVSH = taksaItem.Njesia == 'Perqindje' ? (taksaItem.NormaPerqindje / 100) : taksaItem.NormaPerqindje;
    switch (dataField) {
        case "Vlera":
            rowData.Vlera = value;
            rowData.VleraPaTvsh = rowData.Vlera * (1 - TVSH);
            rowData.Cmimi = rowData.VleraPaTvsh / rowData.Sasia;
            break;
        case "VleraPaTvsh":
            rowData.VleraPaTvsh = value;
            rowData.Vlera = rowData.VleraPaTvsh * (1 + TVSH);
            rowData.Cmimi = rowData.VleraPaTvsh / rowData.Sasia;
            break;
        case "TVSH":
            rowData.TVSH = value;
            rowData.Vlera = rowData.VleraPaTvsh * (1 + TVSH);
            break;
        case "Sasia":
            rowData.Sasia = value;
            rowData.VleraPaTvsh = rowData.Cmimi * rowData.Sasia;
            rowData.Vlera = rowData.VleraPaTvsh * (1 + TVSH);
            break;
        case "Cmimi":
            rowData.Cmimi = value;
            rowData.VleraPaTvsh = rowData.Cmimi * rowData.Sasia;
            rowData.Vlera = rowData.VleraPaTvsh * (1 + TVSH);
            break;
    }
    gridaTrupi.Refresh();
}

function llogaritVleratTotale() {
    var totali = gridaTrupi.GetSummaryOfColumns(new Array("Vlera"), "sum");
    var totaliPaTvsh = gridaTrupi.GetSummaryOfColumns(new Array("VleraPaTvsh"), "sum");
    txtTotali.SetText(totali);
    txtTotaliPaTvsh.SetText(totaliPaTvsh);
    txtTvsh.SetText(totali - totaliPaTvsh);
}

function formatoFushaDevi() {
    Utils.formatoTextBox(txtTotali);
    Utils.formatoTextBox(txtTotaliPaTvsh);
    Utils.formatoTextBox(txtTvsh);
}

function unFormatoFushaDevi() {
    Utils.unFormatoTextBox(txtTotali);
    Utils.unFormatoTextBox(txtTotaliPaTvsh);
    Utils.unFormatoTextBox(txtTvsh);
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
    hfTeDrejta.Set("NdryshoCmimShitje", teDrejtaNiveli.DNdryshoCmimShitje);
    ndryshoCmimeShitje = teDrejtaNiveli.DNdryshoCmimShitje;
    myMenu.menuSipasTeDrejtaRegjistrim($("#hfShtimModifikim"), hfTeDrejta);
}

function merrTeDrejtaNiveli(idNiveli) {
    var teDrejtaNivele = JSON.parse(hfState.Get("teDrejtaNivele"));
    return teDrejtaNivele.filter(function (eDrejta) { return eDrejta.IdNivelRegjistrimi == idNiveli; })[0];
}

function konverto() {
    if (cmbKonverto.GetText() == 'EB' || cmbKonverto.GetText() == 'IB')
        myFaqeCelje.kontrolloTeDrejta('B_Shto_RegjistrimDokumentBuxheti.aspx?lloji=ekzekutim' + '&id=' + idKonvertimi + '&shtim_modifikim=konvertim&niveli=' + cmbKonverto.GetValue() + '&konfigurim=' + cmbKonf.GetValue() + '&llojKonvertimiNga=' + llojKonvertimiNga);
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

function percaktoVisibleMenuBuxheti(eshteModifikim) {
    var veprimi = Utils.getUrlVar('lloji');
    switch (veprimi) {
        case 'planifikimEkzekutimi':
            bejVisibleMenuBuxheti(veprimi, eshteModifikim, eshteModifikim, false, eshteModifikim); // (visiblePosto, visibleKonverto, visibleGjenero, visibleFshi)
            break;
        case 'ekzekutim':
            bejVisibleMenuBuxheti(veprimi, false, eshteModifikim, eshteModifikim, eshteModifikim);
            break;
        default:
            bejVisibleMenuBuxheti(veprimi, false, false, eshteModifikim, eshteModifikim);
            break;
    }
}

function bejVisibleMenuBuxheti(veprimi, visiblePosto, visibleKonverto, visibleGjenero, visibleFshi) {
    ASPxMenu1.GetItemByName('Fshi').SetVisible(visibleFshi);
    //if (veprimi == 'planifikimEkzekutimi') {
    //    ASPxMenu1.GetItemByName('Posto').SetVisible(visiblePosto);
    //    ASPxMenu1.GetItemByName('Konverto').SetVisible(visibleKonverto);
    //}
    //if (veprimi == 'ekzekutim')
    //    ASPxMenu1.GetItemByName('Gjenero').SetVisible(visibleGjenero);
}

function percaktoMenuItemsSipasSkemesWF(result)
{
    bejVisibleMenuBuxhetiAprovimi(result[2], result[3], result[4], result[5], result[6], result[0], result[1]);
}
function bejVisibleMenuBuxhetiAprovimi(result2, result3, result4, result5, result6, result0, result1) {
    bejVisibleMenuBuxhetiAprovimiItem('Aprovo', result2);
    bejVisibleMenuBuxhetiAprovimiItem('Refuzo', result3);
    bejVisibleMenuBuxhetiAprovimiItem('Delego', result4);
    bejVisibleMenuBuxhetiAprovimiItem('Komento', result5);
    bejVisibleMenuBuxhetiAprovimiItem('Modifiko', result6);
    bejVisibleMenuBuxhetiAprovimiItem('Ruaj', result0);
    bejVisibleMenuBuxhetiAprovimiItem('Draft', result1);
}

function bejVisibleMenuBuxhetiAprovimiItem(itemName, visible) {
    if (ASPxMenu1.GetItemByName(itemName) != undefined && ASPxMenu1.GetItemByName(itemName) != null)
        ASPxMenu1.GetItemByName(itemName).SetVisible(visible);
}

function SucededCallbackLupaKomente(result) {
    myButtonClickLupa.LupaUniversal_Click('Komentet', 'LupaKomente.aspx?idetapa=' + result.IdEtapa + '&nrprocesi=' + result.NrProcesi + '&veprimi=Gjitha&idkategoria=179', 600, 500);
}

function callWebservicePostoDokumentBuxheti() {
    Utils.shfaqLoadingGif();
    $.ajax({
        url: Utils.getServerApiUrl("Buxheti", "PostoDokumentAlokimBuxheti"),
        data: JSON.stringify({ idKokaAlokimi: $("#hfId").val(), komponente: _idKomponente, idNdermarrje: _idNdermarrje, idPerdoruesi: _idPerdoruesi, idViti: _idViti, idSkemaWF: idSkemaWF, statusAprovimi: 1 })
    }).done(SucceededCallbackPostoDokument);
}

function SucceededCallbackPostoDokument(result) {
    ShfaqMesazh(result.mesazh);
    Utils.hiqLoadingGif();
} 

function vendosIdEtapeAprovimi(result) {
    idEtapa = result.IdEtapa;
}

function onValueChangedZeri(rowIndex, zeriObjekt) {
    if (zeriObjekt.value && zeriObjekt.value > 0)
        callWebServiceCmimArtikulli(zeriObjekt.label, rowIndex);

    if (Utils.getUrlVar("lloji") == "perfitim")
        return;

    if (!(zeriObjekt.value || zeriObjekt.value > 0)) {
        gridaTrupi.VendosVleraNeDataSource(rowIndex, "Buxheti", "IdBuxheti", undefined, null, "label", "value", undefined);
        return;
    }
    if (gridaTrupi.GetData()[rowIndex].IdLlojObjekti == 1)
        memoryArt.Set(zeriObjekt.objekti);
    merrLlojeBuxheti("", rowIndex, false, true);
}

function PezulloDokument(shenime) {
    NdryshoStatusDokumenti(8, shenime);
}

function NdryshoStatusDokumenti(idStatusi, shenime) {
    var komponente = "B_Shto_RegjistrimDokumentBuxheti.aspx?lloji=" + Utils.getUrlVar("lloji");
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Buxheti", "NdryshoStatusDokumenti"),
        data: JSON.stringify({ idBuxhetiKoka: new Array($('#hfId').val()), idStatusDok: idStatusi, idPerdoruesi: _idPerdoruesi, idNdermarrje: _idNdermarrje, idViti: _idViti, komponente: komponente, shenime: shenime })
    }).done(SucceededCallbackNdryshoStatusDokumenti)
}

function SucceededCallbackNdryshoStatusDokumenti(result) {
    if (result.mesazhSukses != "") 
        myMesazh.ShtoMesazhSuksesi(result.mesazhSukses);

    if (result.mesazhGabim != "")
        myMesazh.ShtoMesazhGabimi(result.mesazhGabim);

    Utils.hiqLoadingGif();
}

function callWebServiceCmimArtikulli(kodi, rreshtIndex) {
    var dataSource = gridaTrupi.GetData();
    if (dataSource[rreshtIndex].IdLlojObjekti != 1)
        return;
    var idNderm = _idNdermarrjeTrupiBija ? _idNdermarrjeTrupiBija : _idNdermarrje;
    var idPerd = _kokaHapurBuxheti ? _kokaHapurBuxheti.IdKrijuesi : _idPerdoruesi;
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheCmimArtikulli"),
        data: JSON.stringify({ idNdermarrje: idNderm, idPerdoruesi: idPerd, kodArtikulli: kodi, nivelCmimi: nivelCmimi ? nivelCmimi : 0, njesia: 1, date: dteDate.GetText(), monedha: "Lek", kursi: 1, idRreshti: rreshtIndex, sasi: dataSource[rreshtIndex].Sasia, shitjeblerje: Utils.getUrlVar("lloji") == "perfitim" ? 0 : 1, detajim: "" })
    }).done(function (result) {
        SucceededCallbackCmimArtikulli(result);
    });
}

function SucceededCallbackCmimArtikulli(result) {
    var index = result[0];
    var cmimi = result[1];
    var rowData = gridaTrupi.GetData()[index];
    rowData.Cmimi = cmimi;
    var vleraPaTvsh = rowData.Sasia * cmimi;
    vendosVleraNeRresht(vleraPaTvsh, "VleraPaTvsh", rowData);
}

function cmbLlojVeprimiChanged(s, e) {
    cmbEntiteti.ClearItems();
    nivelCmimi = 0;
}

function valueChangedEntiteti(s, e) {
    if ($("#hfShtimModifikim").val() == "konvertim")
        return;

    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheKlientFurnitorSipasId"),
        data: JSON.stringify({ idKlientFurnitor : s.GetValue()})
    }).done(function (result) {
        var klientFurnitor = result.klientFurnitor;
        nivelCmimi = klientFurnitor.IdNivelCmimi;
        updateCmimiGrid();
    });
}

function updateCmimiGrid() {
    var dataSource = gridaTrupi.GetData();
    var rreshtaMeArtikuj = dataSource.filter(function (item) { return item.IdLlojObjekti == 1 });
    if (!rreshtaMeArtikuj || rreshtaMeArtikuj.length <= 0)
        return;
    var idNderm = _idNdermarrjeTrupiBija ? _idNdermarrjeTrupiBija : _idNdermarrje;
    var idPerd = _kokaHapurBuxheti ? _kokaHapurBuxheti.IdKrijuesi : _idPerdoruesi;
    var keys = rreshtaMeArtikuj.map(function (item) { return item["IdBuxhetiTrupi"]; });
    Utils.shfaqLoadingGif();
    async.map(keys, function (key, callbackMap) {
        var rowData = dataSource.filter(function (item) { return item.IdBuxhetiTrupi == key })[0];
        var kodi = rowData.Zeri;
        var sasia = rowData.Sasia;
        if (kodi == undefined) {
            callbackMap(null);
            Utils.hiqLoadingGif();
            return;
        }

        if (kodi != "") {
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "ktheCmimArtikulli"),
                data: JSON.stringify({ idNdermarrje: idNderm, idPerdoruesi: idPerd, kodArtikulli: kodi, nivelCmimi: nivelCmimi ? nivelCmimi : 0, njesia: 1, date: dteDate.GetText(), monedha: "Lek", kursi: 1, idRreshti: key, sasi: sasia, shitjeblerje: Utils.getUrlVar("lloji") == "perfitim" ? 0 : 1, detajim: "" })
            }).done(function (result) {
                var cmimi = result[1];
                rowData.Cmimi = cmimi;
                var vleraPaTvsh = sasia * cmimi;
                vendosVleraNeRresht(vleraPaTvsh, "VleraPaTvsh", rowData);
                Utils.hiqLoadingGif();
            });
        }
    },
    function (err, result) {
    });
}

function merrIdLlogariNgaRreshti(rowIndex) {
    var idLlogaria = 0;
    var llojObjekti = gridaTrupi.GetData()[rowIndex].IdLlojObjekti;
    var kodNiveli = cmbNiveli.GetSelectedItem().texts[0];
    switch (llojObjekti) {
        case 3:
            idLlogaria = gridaTrupi.GetData()[rowIndex].IdObjekti;
            break;
        case 1:
            var objektArt = memoryArt.Get(gridaTrupi.GetData()[rowIndex].IdObjekti);
            if (kodNiveli == "PIB" || kodNiveli == "IB")
                idLlogaria = objektArt ? objektArt.IdLlogariTeTrete : 0;
            else
                idLlogaria = objektArt ? objektArt.IdLlogariBlerje : 0;
            break;
    }
    return idLlogaria != null ? idLlogaria : 0;
}

function ButtonClickDoktori() {
    var qrString = "";
    if (lupaDoktoriKonfig && lupaDoktoriKonfig > 0)
        qrString = ("&idKonfigAmbjente=" + lupaDoktoriKonfig);
    popupUniversal.SetContentUrl('LupaAutomjeti.aspx' + qrString);
    popupUniversal.SetHeaderText(hfState.Get("msgZgjidhAutomjetin"));
    popupUniversal.SetSize(600, 500);
    popupUniversal.Show();
}