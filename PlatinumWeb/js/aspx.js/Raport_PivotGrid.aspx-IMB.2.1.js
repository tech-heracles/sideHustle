;
var editorValues = new Object();
//variabla per te kaluar nga nje reshti i grides tek tjetri
var indexSel = 0;
//indexi i zgjedhur per modifikim
var indexModifiko;
//perdoret per te treguar nese duhet te mbushen tabet apo jo kjo perdoret qe tabet te mbushen vetem heren e pare dhe jo pas cdo here qe  kalohet nga nje tab tek tjetri
var mbush = true;
var lista = true;
//perdoret per te daluar rastet kur bejme double click apo thjesht click ne menyre qe ne double click te kaloje tek tabi i dyte
var kaloTab = false;
var KPF = 0;
var lastSel = 1;
var lastSelShitje = 1;

var lastSelBlerje = 1;
var lastSelKlient = 1;
var lastSelFurnitor = 1;
var lastSelMagazina = 1;
var lastSelArtikull = 1;
var lastSelKontabiliteti = 1;
var lastSelLlogarite = 1;

var lastSelAnketa = 1;
var lastSelAgjenti = 1;

var lastSelPunonjesi = 1;
var lastSelListepagesa = 1;

var lastSelOrganika = 1;
//var lastSelInventariPerdorues = 1;
//var lastSelInventariVite = 1;
//var lastSelEvidencaStatistikore = 1;


var lastSelRow = 1;
var lastSelColumn = 1;
var lastSelFilter = 1;
var lastSelData = 1;
var idModuli;
var idGjuha;
var shikoKonfig = false;
// Fshin te dhenat e grides nga sessioni ne window unload
$(window).on('unload', function () {
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "fshiTeDhenatPivotGrideNgaSessioni"),
        data: JSON.stringify({})
    }).done(Utils.fshiSessionFailCheck);
});
$(document).ready(function () {
    idGjuha = hfState.Get("idGjuha");
})
/*
Function: menu_click
perdoret per veprimet e menuse ne javascript
Parameters: e-eventi
*/
function menu_click(s, e) {
    hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    hfId = $('#hfId'); //hidden fieldi qe ruan id  e rreshtit te selektuar
    //myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, false, false);
    if (e.item.name == 'Ruaj') {
        mbush = false;
        valido(s, e);
        ruajTeDhenatKonfigNeHiddenFielde();
    }
    else if (e.item.name == 'Shto') {
        indexModifiko = -1;
        mbush = false;
        //shikoKonfig = false;
        PageControl.SetActiveTabIndex(1);
        e.processOnServer = false;
        pastrofusha();
        aktivizoFusha();
    }
    else if (e.item.name == 'Fshi') {
        if (!Utils.kaRreshtaTeSelektuarGrida(ASPxGridView_KonfigPivotGrid))
        {
            myMesazh.ShtoMesazhGabimi(hfState.Get("lblRaportMesazhNukKeniZgjedhur"));
            return;
        }
        popFshi.Show();
        e.processOnServer = false;
    }
    else if (e.item.name == 'Shiko') {
        e.processOnServer = false;
        if (PageControl.GetActiveTabIndex() != 2) {
            mbush = false;
            PageControl.SetActiveTabIndex(2);
        }
        ShikoKonfigRaporti();
    }
    else if (e.item.name == 'Klono') {
        kaloTab = true;
        lista = true;
        hfShtimModifikim.val("klonim");
        mbushfusha();
        e.processOnServer = false;
        hfId.value = 0;
    }
    else if (e.item.name == 'Modifiko') {
        kaloTab = true;
        lista = true;
        hfShtimModifikim.val("modifikim");
        mbushfusha();
        e.processOnServer = false;
    }
    else if (e.item.name == "Anullo") {
        e.processOnServer = false;
        if (PageControl.GetActiveTabIndex() == 0) {
            window.location = "Raportet.aspx?idmod=19";
        }
        else if (PageControl.GetActiveTabIndex() == 1 || PageControl.GetActiveTabIndex() == 2) {
            PageControl.SetActiveTabIndex(0);
        }
    }
    myMenu.PercaktoMenuSipasTabit(PageControl.GetActiveTabIndex(), hfTeDrejta, hfShtimModifikim);
}

function checkText(s, e) {
    myMenu.checkText(s, e);
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function changeName() {
    if (Utils.getUrlVar('idModuli') == undefined)
        myFaqeCelje.changeName('Raport_PivotGrid.aspx', 0, null);
    else
        myFaqeCelje.changeName('Raport_PivotGrid.aspx?idModuli=' + Utils.getUrlVar('idModuli'), 0, null);
    myMenu.PercaktoMenuSipasTabit(0, hfTeDrejta, $('#hfShtimModifikim'));
    idGjuha = hfState.Get("idGjuha");
}

// Ne double click te nje konfigurimi krijohet mundesia e editimit te ketij te fundit
function OnGridDoubleClick(index) {
    lista = true;
    $('#hfShtimModifikim').val('modifikim');
    mbushfusha();
}

//merr te dhenat e rreshtit te selektuar
function mbushfusha() {
    mbush = false;
    indexModifiko = ASPxGridView_KonfigPivotGrid.GetFocusedRowIndex();
    if (indexModifiko == -1)
        myMesazh.ShtoMesazhGabimi(HfMsgKonfig.Get("MsgZgjidhKonfig"));
    else ASPxGridView_KonfigPivotGrid.GetRowValues(indexModifiko, 'IdKonfPivotGridaKoka;EmriPivotGridKoka;PershkrimiPivotGridKoka', OnGetRowValuesMod);
}

//mbush fushat me te dhenat e rreshtit te selektuar
function OnGetRowValuesMod(values) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;
    $('#hfId').val(values[0]);

    $.ajax(
        {
            url: Utils.getServerApiUrl("Rregjistrime", "callWsGetAutorizimeSipasLlojitDheIdLidhese"),
            data: JSON.stringify({ idLidhese: values[0], kodLloji: 'RaportePivotGrid', idPerdorues: hfState.Get('idPerdoruesi') })
        }).done(DoneCallbackAutorizime);

    if ($('#hfShtimModifikim').val() != "shtim" && $('#hfShtimModifikim').val() != "klonim")
        kodi_TextBox.SetEnabled(false);
    else
        kodi_TextBox.SetEnabled(true);
    kodi_TextBox.SetText(values[1]);
    pershkrimi_TextBox.SetText(values[2]);
    callWebserviceKonfigRaporti(idGjuha, values[0], idModuli);
    if (kaloTab) {
        PageControl.SetActiveTabIndex(1);
        myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, $('#hfShtimModifikim'));
    }
}

function DoneCallbackAutorizime(result) {
    if (!result)
        return;
    if (result.idLidhese == $('#hfId').val())
        cmbAutorizimi.SetText(result.autorizime);
}

function callWebserviceKonfigRaporti(idGjuha, idKonfig, idModuli) {
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "ktheKonfigurimRaporti"),
        data: JSON.stringify({ idGjuha: idGjuha, idKonfigRaporti: idKonfig, idModuli: idModuli })
    }).done(SuccededCallbackKonfigRaporti);
}

// ******  Merr te dhenat e nje konfigurimi te caktuar (ose konfigurim default ne rastin e shtimit), dhe mbush gridat e fushave,
// zonave, dhe ne ratin kur po modifikohet nje konfigurim ekzistues nderton raportin e pivot grides   *******//

function ResetCombo(combo) {
    combo.ClearItems();
}

function SuccededCallbackKonfigRaporti(result) {
    var colKolonatKonfig = result;
    pastroGridat();
    ResetCombo(fushatDateTimePG);
    ResetCombo(fushatNumerikePG);
    ResetCombo(filterDate);

    lastSelShitje = 1;
    lastSelBlerje = 1;
    lastSelKlient = 1;
    lastSelFurnitor = 1;
    lastSelArtikull = 1;
    lastSelLlogarite = 1;

    lastSelAnketa = 1;
    lastSelAgjenti = 1;

    lastSelPunonjesi = 1;
    lastSelListepagesa = 1;
    lastSelRow = 1;
    lastSelColumn = 1;
    lastSelFilter = 1;
    lastSelData = 1;
    lastSelOrganika = 1;
    //lastSelInventariPerdorues = 1;
    //lastSelInventariVite = 1;
    //lastSelEvidencaStatistikore = 1;

    var idkol, rendi, emerKolShfaq, emerTabDB, emerKolDB, width, grupimKol, tipiKol, analitikTot, llojGrupimi, dataRow, su;

    for (var i = 0; i < colKolonatKonfig.length; i++) {
        idkol = colKolonatKonfig[i].IdKolonaPG;
        emerKolShfaq = colKolonatKonfig[i].EmerKoloneShfaq;
        emerTabDB = colKolonatKonfig[i].EmerTabeleDB;
        emerKolDB = colKolonatKonfig[i].EmerKoloneDB;
        grupimKol = colKolonatKonfig[i].GrupimKolone;
        llojGrupimi = colKolonatKonfig[i].LlojGrupimi;
        tipiKol = colKolonatKonfig[i].TipiKolones;
        analitikTot = colKolonatKonfig[i].AnalitikTotal;

        if ($('#hfShtimModifikim').val() == "shtim" || (($('#hfShtimModifikim').val() == "modifikim" || $('#hfShtimModifikim').val() == "klonim") && colKolonatKonfig[i].Visibility == false)) {
            dataRow = {
                idKolonaPivotGrid: idkol,
                emrikolonesshfaq: emerKolShfaq,
                emerTabeleDB: emerTabDB,
                emerKoloneDB: emerKolDB,
                grupimKolone: grupimKol,
                tipiKolones: tipiKol,
                analitikTotal: analitikTot,
                llojGrupimi: llojGrupimi
            };
            su = jQuery("#tblFushat" + grupimKol).addRowData(Utils.ktheKontroll("lastSel" + grupimKol), dataRow);
            //ls = ls + 1;
            eval("lastSel" + grupimKol + "=" + (Utils.ktheKontroll("lastSel" + grupimKol) + 1));
            continue;
        }

        rendi = colKolonatKonfig[i].Rendi;
        zona = colKolonatKonfig[i].Zona;
        width = colKolonatKonfig[i].Width;
        dataRow = {
            idKolonaPivotGrid: idkol,
            rendi: rendi,
            width: width,
            emrikolonesshfaq: emerKolShfaq,
            emerTabeleDB: emerTabDB,
            emerKoloneDB: emerKolDB,
            grupimKolone: grupimKol,
            zona: zona,
            tipiKolones: tipiKol,
            analitikTotal: analitikTot,
            llojGrupimi: llojGrupimi
        };
        su = jQuery("#tbl" + zona).addRowData(Utils.ktheKontroll("lastSel" + zona), dataRow);
        eval("lastSel" + zona + "=" + (Utils.ktheKontroll("lastSel" + zona) + 1));

        //mbushim combon e fushave numerike dhe datetime

        if (tipiKol === "Numeric") {
            HfGrupimi.Set(emerKolDB, llojGrupimi);

            var llojGrupimiNgaDB = cmbVepLlogaritese.FindItemByValue(llojGrupimi);
            if (llojGrupimiNgaDB !== null)
                fushatNumerikePG.AddItem(emerKolShfaq, emerKolDB);
            else
                fushatNumerikePG.AddItem(emerKolShfaq, emerKolDB);
        }
        else if (tipiKol === "DateTime") {
            fushatDateTimePG.AddItem(emerKolShfaq, emerKolDB);
            filterDate.AddItem(emerKolShfaq, emerKolDB);
        }
    }
  //  if (shikoKonfig === true)    //pati
        ShikoKonfigRaporti();
}

function ShikoKonfigRaporti() {
    ShfaqFiltrat(ruajTeDhenatKonfigNeHiddenFielde());
    hfState.Set("DateDokumentiVisibility", $('#dateDokumenti').css('visibility'));
    //callbackCheckBox.PerformCallback();
    if (filterDate.GetItemCount() > 0 && $('#dateDokumenti').css('visibility') != "hidden")
        filterDate.SetSelectedIndex(0);
    if (fushatDateTimePG.GetItemCount() > 0 && $('#dateDokumenti').css('visibility') != "hidden")
        fushatDateTimePG.SetSelectedIndex(0);

    ASPxPivotGridRaporti.PerformCallback($('#dateDokumenti').css('visibility'));
}

function ruajTeDhenatKonfigNeHiddenFielde() {
    var rreshtat = merrTeDhena("#tblRow", "Row");
    var shtyllat = merrTeDhena("#tblColumn", "Column");
    var teDhenat = merrTeDhena("#tblData", "Data");
    var filtrat = merrTeDhena("#tblFilter", "Filter");
    var fushat = rreshtat.concat(shtyllat);
    fushat = fushat.concat(teDhenat);
    fushat = fushat.concat(filtrat);
    $('#fushatRaporti').val(JSON.stringify(fushat));
    return fushat;
}

// ne varesi te tipit te te dhenave qe ka zgjedhur perdoruesi, do te afishohen filtrat e raportit
function ShfaqFiltrat(fushat) {
    $('#dateDokumenti').hide();
    $('#dateDokumenti').css('visibility', 'hidden');
    $('#vepLlogaritese').hide();//.css('visibility', 'hidden');
    $('#perioda').hide();//.css('visibility', 'hidden');
    var filtatShfaq = new Array();
    var nrFiltra = 0;
    var kaDataNumeric = false;

    for (var i = 0; i < fushat.length; i++) {
        if (fushat[i].tipiKolones == "DateTime" && (fushat[i].zona == "Column" || fushat[i].zona == "Row" || fushat[i].zona == "Filter")) {
            filtatShfaq[nrFiltra++] = "dateDokumenti";
            filtatShfaq[nrFiltra++] = "perioda";
        }
        if (fushat[i].tipiKolones == "Numeric" && fushat[i].zona == "Data") {
            kaDataNumeric = true;
        }
        if (fushat[i].tipiKolones != "Numeric" && fushat[i].zona == "Data") {
        }
    }

    if (kaDataNumeric) filtatShfaq[nrFiltra++] = "vepLlogaritese";

    filtatShfaq[nrFiltra++] = "tipiGrafikut";

    var filtratRaport = navBarFiltrat.GetGroupByName("filtratRaport");
    if (nrFiltra != 0) {
        $('#filtratRaportDiv').css({ height: (8 + (35 * nrFiltra)) });
        for (var i = 0; i < nrFiltra; i++) {
            $('#' + filtatShfaq[i]).show();
            $('#' + filtatShfaq[i]).css({ display: 'inline', visibility: 'visible', left: '1%', width: '100%', top: 1 + '%' });
        }
        filtratRaport.SetExpanded(true);
    }
    else {
        $('#filtratRaportDiv').css({ height: 0 });
        filtratRaport.SetExpanded(false);
    }
}

//Per te dhenat e konfigurimit behet kontrolli nese width dhe height jane brenda vlerave te lejuara
//dhe per secilen nga rreshtat e kthyer shtohet dhe zona, pra nese eshte rresht, shtyll, filter apo data
function merrTeDhena(emriGrides, zona) {
    var rreshtaTeGrides = $(emriGrides).getRowData();
    for (var i = 0; i < rreshtaTeGrides.length; i++) {
        var idRow = $(emriGrides).getDataIDs()[i];
        var width = $('#' + idRow + '_width');
        if (width[0] != undefined)
            rreshtaTeGrides[i].width = width[0].value;
        rreshtaTeGrides[i].fshi = "";
        rreshtaTeGrides[i].zona = zona;
    }
    return rreshtaTeGrides;
}

// Ben pastrimin e gridave ne momentin qe do shtohet nje konfigurim i ri, apo
// do hapet nje konfigurim ekzistues
function pastroGridat() {
    if ($("input[id$='HfGrupimKolone']").val() == undefined || $("input[id$='HfGrupimKolone']").val() == "")
        return;
    var colGrupimKolone = JSON.parse($("input[id$='HfGrupimKolone']").val());
    for (var i = 0; i < colGrupimKolone.length; i++) {
        $("#tblFushat" + colGrupimKolone[i].EmerGrupimKolone).jqGrid("clearGridData", true).trigger("reloadGrid");
    }
    $("#tblFilter").jqGrid("clearGridData", true).trigger("reloadGrid");
    $("#tblRow").jqGrid("clearGridData", true).trigger("reloadGrid");
    $("#tblColumn").jqGrid("clearGridData", true).trigger("reloadGrid");
    $("#tblData").jqGrid("clearGridData", true).trigger("reloadGrid");
}

//pastron fushat per shtim dhe ben aktiv kodin, i cili nuk eshte i editueshem
//pas ruajtjes se konfigurimit
function pastroKontrollet() {
    kodi_TextBox.SetText('');
    pershkrimi_TextBox.SetText('');
}

//Ben disable kodin e konfigurimit ne rastin e modifikimit, ose enable ne rastet e tjera
function aktivizoFusha(kontrolli) {
    var hfShtimModifikim = $('#hfShtimModifikim')[0];
    if ($('#hfShtimModifikim').val() == "modifikim")
        kodi_TextBox.SetEnabled(false);
    else
        kodi_TextBox.SetEnabled(true);
}

// Thirret ne momentin qe duhet te shtojme nje konfigurim te ri
// Pastron fushat e kodit dhe pershkrimit si dhe ben reset te gridave
// te fushave si dhe te pivot grides
function pastrofusha() {
    pastroKontrollet();
    disableWebChartControls();

    $('#hfShtimModifikim')[0].value = "shtim";
    callWebserviceKonfigRaporti(idGjuha, -1, idModuli);
}

/*
Function: Poshte_click  -- perdoret per te selektuar rreshtin me poshte
Parameters:  e-eventi
*/
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, ASPxGridView_KonfigPivotGrid, indexSel);
}

/*Function: Lart_click -- perdoret per te selektuar rreshtin me lart
Parameters: e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, ASPxGridView_KonfigPivotGrid, indexSel);
}

/*
Function: Fillim_click -- perdoret per te shkuar ne fillim te faqes
Parameters: e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, ASPxGridView_KonfigPivotGrid, indexSel);
}

/*
Function: Fund_click -- perdoret per te shkuar ne fund te faqes
Parameters: e-eventi
*/

function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, ASPxGridView_KonfigPivotGrid, indexSel);
}

/*
Function: OnGridSelectionChanged perdoret per te ruajtur indexin e selektimit
Parameters: e-eventi
*/
function OnGridSelectionChanged(e) {
    indexSel = myMenu.JSlevizNeGride.OnGridSelectionChanged(e, indexSel);
    CallbackState.Set("firstCallback", "1");
}

/*
Function: EndRequestHandler

Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe gridat e konfigurimit
Shiko funksionet <pastro>, <pastrofusha>.
*/
function EndRequestHandler(sender, args) {
    var hfStatus = $("#hfStatusi");
    var hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    var hfId = $('#hfId');
    if (hfStatus.val() == "true") {
        if (hfShtimModifikim.val() != "modifikim") {
            hfShtimModifikim.val("shtim"); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
            aktivizoFusha();
            window.mbush = false;
            window.shikoKonfig = false;
            hfId.val(0); //hidden fieldi qe ruan id  e rreshtit te selektuar
            indexModifiko = -1; //indexi i reshtit te selektuar
            pastrofusha();
            hfStatus.val("false");

            if (PageControl.GetActiveTabIndex() != 0) {
                PageControl.SetActiveTabIndex(1);
                myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, $('#hfShtimModifikim'));
            }
        }
        else {
            hfStatus.val("false");

            if (PageControl.GetActiveTabIndex() != 0)
            { PageControl.SetActiveTabIndex(0); myMenu.PercaktoMenuSipasTabit(0, hfTeDrejta, hfShtimModifikim); }
        }
        ASPxGridView_KonfigPivotGrid.PerformCallback("662;;");
        ASPxGridView_KonfigPivotGrid.ClearFilter();
    }
    else myMenu.PercaktoMenuSipasTabit(PageControl.GetActiveTabIndex(), hfTeDrejta, $('#hfShtimModifikim'));
    Utils.hiqLoadingGif();;
}

function valido(s, e) {
    myFaqeCelje.valido(s, e, PageControl, hfTeDrejta, $('#hfShtimModifikim'));
}

var btnFiltrat;
function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

function enter() {
    if (window.event.keyCode == 13) {
        event.returnValue = false;
        event.cancel = true;
    }
}

jQuery(document).ready(function () {
    inicializoGridat();
    mbush = true;
    disableWebChartControls();
    idModuli = Utils.getUrlVar('idModuli');
    if (typeof window.parent.lblFaqja !== "undefined")
        changeName();
    else {
        myMenu.PercaktoMenuSipasTabit(0, hfTeDrejta, $('#hfShtimModifikim'));
        if (Utils.getUrlVar('vjenNga') === "CRM")
            ASPxMenu1.GetItemByName('Anullo').SetVisible(false);
        idGjuha = hfState.Get("idGjuha");
    }
});

// **** Ky funksion perdoret per te inicializuar gridat duke percaktuar kolonat dhe te dhena te tjera per to  ***** //
function inicializoGridat() {
    if ($("input[id$='HfGrupimKolone']").val() == undefined || $("input[id$='HfGrupimKolone']").val() == "")
        return;
    var colGrupimKolone = JSON.parse($("input[id$='HfGrupimKolone']").val());

    var teDrejtaRap = JSON.parse(hfTeDrejtaRaporti.Get("teDrejtaRap"));
    var enabled = teDrejtaRap.DMod || teDrejtaRap.DShtim;

    for (var i = 0; i < colGrupimKolone.length; i++) {
        myJQGridFushat.inicializoGride('#tblFushat' + colGrupimKolone[i].EmerGrupimKolone, '#divFushat' + colGrupimKolone[i].EmerGrupimKolone + 'Pager', colGrupimKolone[i].PershkrimGrupimKolone, colGrupimKolone[i].EmerGrupimKolone, Utils.ktheKontroll("lastSel" + colGrupimKolone[i].EmerGrupimKolone), idGjuha, enabled);
        if (colGrupimKolone.length == 1)
            resizeJqGridWidth('tblFushat' + colGrupimKolone[i].EmerGrupimKolone, "contentDiv", 2.1);
        else
            resizeJqGridWidth('tblFushat' + colGrupimKolone[i].EmerGrupimKolone, "contentDiv", colGrupimKolone.length + ((colGrupimKolone.length - 1) / 10));
        $('#tblFushat' + colGrupimKolone[i].EmerGrupimKolone).css("display", 'inline');
        $('#divFushat' + colGrupimKolone[i].EmerGrupimKolone + 'Pager').css("display", 'inline');

    }


    myJQGridPivot.inicializoGride("#tblFilter", '#divFilterPager', 'Report Filter', 'Filter', lastSelFilter, idGjuha, enabled);
    myJQGridPivot.inicializoGride("#tblColumn", '#divColumnPager', 'Report Columns', 'Column', lastSelColumn, idGjuha, enabled);
    myJQGridPivot.inicializoGride("#tblRow", '#divRowPager', 'Report Rows', 'Row', lastSelRow, idGjuha, enabled);
    myJQGridPivot.inicializoGride("#tblData", '#divDataPager', 'Report Data', 'Data', lastSelData, idGjuha, enabled);

    resizeJqGridWidth("tblFilter", "contentDiv", 2.1);
    resizeJqGridWidth("tblColumn", "contentDiv", 2.1);
    resizeJqGridWidth("tblRow", "contentDiv", 2.1);
    resizeJqGridWidth("tblData", "contentDiv", 2.1);
}

function klickselectedvaluedok(theRadio, e) {
    if (theRadio.GetSelectedIndex() != -1) {
        var rblCaseControlDok = theRadio.GetSelectedItem().index;
        // theRadio.GetSelectedItem().value;
        if (rblCaseControlDok == 1) {
            //(rblCaseControlDok == 'Periudha') {
            txtNgaDok.SetEnabled(true);
            txtDeriDok.SetEnabled(true);
        }
        else {
            txtNgaDok.SetEnabled(false);
            txtDeriDok.SetEnabled(false);
        }
    }
}

function ngaDokDateChanged(s, e) {
    if (txtDeriDok.GetDate() < txtNgaDok.GetDate())
        txtDeriDok.SetDate(txtNgaDok.GetDate());
}

function resizeJqGridWidth(grid_id, div_id, koeficenti) {
    $(window).bind('resize', function () {
        $('#' + grid_id).setGridWidth($('#' + div_id).width() / koeficenti, true); //Resized to new width as per window
    }).trigger('resize');
}

//Perdoret per te bere enable ose disable buton undo dhe redo te pivotgrides
function UpdateButtonStates() {
    if (ASPxClientUtils.IsExists(ASPxPivotGridRaporti.cpIsUndoEnabled))
        btnUndo.SetEnabled(ASPxPivotGridRaporti.cpIsUndoEnabled);
    if (ASPxClientUtils.IsExists(ASPxPivotGridRaporti.cpIsRedoEnabled))
        btnRedo.SetEnabled(ASPxPivotGridRaporti.cpIsRedoEnabled);
    //checkboxet e totaleve




    ColumnGrandTotal.SetChecked(ASPxPivotGridRaporti.cpGrandTotalKolona == true);
    ColumnTotal.SetChecked(ASPxPivotGridRaporti.cpColumnTotal == true);
    RowGrandTotal.SetChecked(ASPxPivotGridRaporti.cpRowGrandTotal == true);
    HiqVleraZero.SetChecked(ASPxPivotGridRaporti.cpHiqVleraZero == true);
    RowTotal.SetChecked(ASPxPivotGridRaporti.cpRowTotal == true);
    CallbackState.Set("firstCallback", "0");
}

function activeTabChanging(s, e) {
    indexModifiko = ASPxGridView_KonfigPivotGrid.GetFocusedRowIndex();
    //if (e.tab.index == 2) {
    //    e.processOnServer = false;
    //    shikoKonfig = true;
    //}
    //else shikoKonfig = false;
    if (mbush) {
        if (indexModifiko != -1) {
            OnGridDoubleClick(indexModifiko);
            //mbushfusha();
        }
        else {
            mbush = false;
            $('#hfShtimModifikim').val('shtim');
            $('#hfId').val(0); pastrofusha();
        }
    }
    kaloTab = false;
    //    if (e.tab.index == 2) {
    //        e.processOnServer = false;
    //        ShikoKonfigRaporti();
    //    }
    myMenu.PercaktoMenuSipasTabit(e.tab.index, hfTeDrejta, $('#hfShtimModifikim'));
    if (e.tab.index == 0) {
        ASPxGridView_KonfigPivotGrid.ClearFilter();
    }
}

function ASPxPivotGridRaportiAfterCallback(s, e) {
    UpdateButtonStates();
    if (WebChart.GetVisible() == true)
        WebChart.PerformCallback();
}

function merrIdKonfig(val) {
    callWebserviceKonfigRaporti(idGjuha, val[0], idModuli);
}

function ButtonGrafikClick(s, e) {
    WebChart.SetVisible(true);
    tipiGrafikutLabel.SetEnabled(true);
    cmbGrafiku.SetEnabled(true);
    saVleraNeGrafikLabel.SetEnabled(true);
    saVleraNeGrafikLabel.SetVisible(true);
    cmbSaVleraNeGrafik.SetEnabled(true);
    cmbSaVleraNeGrafik.SetVisible(true);
    sipasKolonaveChk.SetEnabled(true);
    sipasKolonaveChk.SetVisible(true);
    btnPaGrafik.SetEnabled(true);
    CellValueThreshold.SetEnabled(true);
    CellValueThreshold.SetVisible(true);
    shfaqZeroLabel.SetEnabled(true);
    shfaqZeroLabel.SetVisible(true);
    exportButtonGrafik.SetEnabled(true);
    exportButtonGrafik.SetVisible(true);
    cmbExportGrafik.SetEnabled(true);
    cmbExportGrafik.SetVisible(true);
    btnGrafik.SetEnabled(false);

    WebChart.PerformCallback();
}

function ButtonPaGrafikClick(s, e) {
    disableWebChartControls();
    //    WebChart.SetVisible(false);
    //    tipiGrafikutLabel.SetEnabled(false);
    //    cmbGrafiku.SetEnabled(false);
    //    saVleraNeGrafikLabel.SetEnabled(false);
    //    cmbSaVleraNeGrafik.SetEnabled(false);
    //    sipasKolonaveChk.SetEnabled(false);
    //    btnGrafik.SetEnabled(true);
    //    btnPaGrafik.SetEnabled(false);
}

function disableWebChartControls() {
    WebChart.SetVisible(false);
    tipiGrafikutLabel.SetEnabled(false);
    cmbGrafiku.SetEnabled(false);
    saVleraNeGrafikLabel.SetEnabled(false);
    saVleraNeGrafikLabel.SetVisible(false);
    cmbSaVleraNeGrafik.SetEnabled(false);
    cmbSaVleraNeGrafik.SetVisible(false);
    sipasKolonaveChk.SetEnabled(false);
    sipasKolonaveChk.SetVisible(false);
    shfaqZeroLabel.SetEnabled(false);
    shfaqZeroLabel.SetVisible(false);
    CellValueThreshold.SetEnabled(false);
    CellValueThreshold.SetVisible(false);
    exportButtonGrafik.SetEnabled(false);
    exportButtonGrafik.SetVisible(false);
    cmbExportGrafik.SetEnabled(false);
    cmbExportGrafik.SetVisible(false);
    btnGrafik.SetEnabled(true);
    btnPaGrafik.SetEnabled(false);
}

function eksportoGrafik(s, e) {
    var formati = cmbExportGrafik.GetText().toLowerCase();
    if (formati == 'excel xlsx')
        formati = 'xlsx';
    else if (formati == 'excel xls')
        formati = 'xls';
    WebChart.SaveToDisk(formati);
}

function fushatNumerikeSelectedChanged(s, e) {
    var item = fushatNumerikePG.GetSelectedItem();

    if (item !== null) {
        var grupimiTani = HfGrupimi.Get(item.value);
        cmbVepLlogaritese.SetSelectedItem(cmbVepLlogaritese.FindItemByValue(grupimiTani));
    }
}

function cmbveprimeLlogariteseSelectedChanged(s, e) {
    var item = fushatNumerikePG.GetSelectedItem();

    if (item !== null) {
        var llojGrupimi = cmbVepLlogaritese.GetSelectedItem();
        HfGrupimi.Set(item.value, llojGrupimi.value);
    }
}

function fushatDateTimeSelectedChanged(s, e) {
    var item = fushatDateTimePG.GetSelectedItem();

    if (item !== null) {
        var perioda = HfGroupIntervalet.Get(item.value);
        cmbPerioda.SetSelectedItem(cmbPerioda.FindItemByValue(perioda))
    }
}

function periodatSelectedChanged(s, e) {
    var item = fushatDateTimePG.GetSelectedItem();
    var perioda = cmbPerioda.GetSelectedItem();
    if (item !== null && perioda !== null) {
        HfGroupIntervalet.Set(item.value, perioda.value);
    }
}

function Autorizime_Click() {
    var hf = $("#hfLupaAutorizimi")[0];
    var queryStr = hf.value;
    var qstrAutorizimet = '?autorizimet=' + cmbAutorizimi.GetText();
    //    myButtonClickLupa.Autorizime_Click('Zgjidh autorizimet', queryStr, widthLupaAutorizime, heightLupaAutorizime);
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhniAutorizimet"), 'LupaAutorizim.aspx' + qstrAutorizimet, 700, 600);
}