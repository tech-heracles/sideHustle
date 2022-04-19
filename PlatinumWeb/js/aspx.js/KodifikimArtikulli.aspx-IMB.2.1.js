; var editmode = false;
var indexEdit = -1;
var indexModifiko;
var grida = "gvKodifikimArtikulli";
var identifikuesPerPopupKodifikimin = "Kodifikim";
//perdoret per te treguar nese duhet te mbushen tabet apo jo kjo perdoret qe tabet te mbushen vetem heren e pare dhe jo pas cdo here qe  kalohet nga nje tab tek tjetri
var mbush = false;
//per filtrat
function checkText(s, e) {
    myMenu.checkText(s, e);
}

var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    $('#hfRuaj').val('Filtra');
    if (grida == "gvKodifikimArtikulli")
        myMenu.aplikoFiltra(s, e, gvKodifikimArtikulli, "", '');
    else if (grida == "gvKodifikimArtikulliGr2")
        myMenu.aplikoFiltra(s, e, gvKodifikimArtikulliGr2, "", '');
    else if (grida == "gvKodifikimArtikulliGr3")
        myMenu.aplikoFiltra(s, e, gvKodifikimArtikulliGr3, "", '');
}

$(document).ready(function () {
    visibleTabs();
    $(document).keydown(function (e) {
        switch (e.which) {
            case 13:
                e.preventDefault();
                break;
            case 116: //F5
                window.parent.rifresko = true;
                break;
            case 82:
                if (e.ctrlKey) //ctrl+r
                    window.parent.rifresko = true;
            default:
                break;
        }
    });
    $(window).on('load', function () {
        Init();
    });
});

function visibleTabs() {
      switch (Utils.getUrlVar("llojKodifikimi")) {
        case "1":
            PageControl.GetTabByName('Grupimi 1').SetVisible(true);
            PageControl.GetTabByName('Grupimi 2').SetVisible(false);
            PageControl.GetTabByName('Grupimi 3').SetVisible(false);
            break;
        case "2":
            PageControl.GetTabByName('Grupimi 1').SetVisible(false);
            PageControl.GetTabByName('Grupimi 2').SetVisible(true);
            PageControl.GetTabByName('Grupimi 3').SetVisible(false);
            break;
        case "3":
            PageControl.GetTabByName('Grupimi 1').SetVisible(false);
            PageControl.GetTabByName('Grupimi 2').SetVisible(false);
            PageControl.GetTabByName('Grupimi 3').SetVisible(true);
            break;
        default:
            PageControl.GetTabByName('Grupimi 1').SetVisible(true);
            PageControl.GetTabByName('Grupimi 2').SetVisible(true);
            PageControl.GetTabByName('Grupimi 3').SetVisible(true);
            break;
    }
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

function RowDblClickGrida(index, emergrida) {
    if (Utils.getUrlVar("lupe") == 'true') {
        ZgjidhRreshtaNgaLupa();
        return;
    }
    if (Utils.getUrlVar("llojiart") == "aqt") {
        kaloTab = true;
        indexModifiko = index
        lista = true;
        mbushfusha();
        return;
    }
    callWebservice();
    indexModifiko = index;
    grida = emergrida;
}

/*
Function: Poshte_click

perdoret per te selektuar rreshtin me poshte

Parameters:

e-eventi
*/
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvKodifikimArtikulli, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvKodifikimArtikulli, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvKodifikimArtikulli, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvKodifikimArtikulli, indexSel);
}
function prindClick() {
    editorPrind = btneEmertimPrindi;
    editorNiveli = txtNiveli;
    identifikuesPerPopupKodifikimin = "KodifikimAQT";
    KodifikimArtikulli_Click();
}

function KodifikimArtikulli_Click() {
    var llojartikulli = false;
    if (Utils.getUrlVar("llojiart") == 'aqt')
        llojartikulli = true;
     if (grida == "gvKodifikimArtikulli")
        myButtonClickLupa.LupaUniversal_Click(hfState.Get("headerPopUpZgjidhKodifikiminArtikullit"), 'LupaKodifikimArtikulli.aspx?llojartikulli=' + llojartikulli + '&llojKodifikimi=' + 1, 500, 500);
    else if (grida == "gvKodifikimArtikulliGr2")
        myButtonClickLupa.LupaUniversal_Click(hfState.Get("headerPopUpZgjidhKodifikiminArtikullit"), 'LupaKodifikimArtikulli.aspx?llojartikulli=' + llojartikulli + '&llojKodifikimi=' + 2, 500, 500);
    else if (grida == "gvKodifikimArtikulliGr3")
        myButtonClickLupa.LupaUniversal_Click(hfState.Get("headerPopUpZgjidhKodifikiminArtikullit"), 'LupaKodifikimArtikulli.aspx?llojartikulli=' + llojartikulli + '&llojKodifikimi=' + 3, 500, 500);
}


function InitPrind() {
    var hf = $('#hfPrindi');
    var hf1 = $('#hfNiveli');
    editorPrind = Utils.ktheKontroll('IdPrindi');
    // if (hf.value != "")
    // hf.value = editorPrindi.GetText();

    editorNiveli = Utils.ktheKontroll('NivelKodifikimi');
    if (!editorNiveli || !editorPrind) return;
    if ($('#hfRuaj').val() != 'Modifiko')
        if (hf1.val() != "")
            editorNiveli.SetValue(hf1.val());
    enable();
    if (editorPrind.GetText() != "") {
        if (grida == "gvKodifikimArtikulli")
            gvKodifikimArtikulli.GetRowValues(gvKodifikimArtikulli.GetFocusedRowIndex(), 'IdKodifikimi;KodKodifikimi', ktheKodPrind);
        else if (grida == "gvKodifikimArtikulliGr2")
            gvKodifikimArtikulliGr2.GetRowValues(gvKodifikimArtikulliGr2.GetFocusedRowIndex(), 'IdKodifikimi;KodKodifikimi', ktheKodPrind);
        else if (grida == "gvKodifikimArtikulliGr3")
            gvKodifikimArtikulliGr3.GetRowValues(gvKodifikimArtikulliGr3.GetFocusedRowIndex(), 'IdKodifikimi;KodKodifikimi', ktheKodPrind);
    }
}

function ktheKodPrind(result) {
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "ktheKodPrindiGrupArtikull"),
        data: JSON.stringify({ idja: result[0] })
    }).done(mbushHfPrindi);
}

function mbushHfPrindi(result) {
    if ($("#hfPrindi").val() == "") {
        var hf = $('#hfPrindi');
        hf.val(result);
    }
}


function ndryshimTabi(tab) {
    ASPxMenu1.GetItemByName('Ruaj').SetVisible(false);
    if (tab.GetText() == hfState.Get("labelGrupimKlientPare")) {
        grida = "gvKodifikimArtikulli";
        $('#hfGrup').val(0);
        if (gvKodifikimArtikulliGr2.IsEditing() == true)
            gvKodifikimArtikulliGr2.CancelEdit();
        if (gvKodifikimArtikulliGr3.IsEditing() == true)
            gvKodifikimArtikulliGr3.CancelEdit();
    }
    else if (tab.GetText() == hfState.Get("labelGrupimKlientDyte")) {
        $('#hfGrup').val(1);
        grida = "gvKodifikimArtikulliGr2";
        if (gvKodifikimArtikulli.IsEditing() == true)
            gvKodifikimArtikulli.CancelEdit();
        if (gvKodifikimArtikulliGr3.IsEditing() == true)
            gvKodifikimArtikulliGr3.CancelEdit();
    }
    else if (tab.GetText() == hfState.Get("labelGrupimKlientTrete")) {
        $('#hfGrup').val(2);
        grida = "gvKodifikimArtikulliGr3";
        if (gvKodifikimArtikulli.IsEditing() == true)
            gvKodifikimArtikulli.CancelEdit();
        if (gvKodifikimArtikulliGr2.IsEditing() == true)
            gvKodifikimArtikulliGr2.CancelEdit();
    }
}

function pastro() {
    var hf = $("#hfPrindi");
    hf.val("");
    hfArkiva.Clear();
    $("#hfArkivaDokId").val("");
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ? evt.keyCode : evt.charCode;
}

function callWebservice() {
    var emer = 'KodifikimArtikulli.aspx?llojiart=' + Utils.getUrlVar("llojiart");
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "eshteVeprimILejuar"),
        data: JSON.stringify({ emerKomponente: emer, veprimi: 'Modifiko' })
    }).done(SucceededCallback);
}

// This is the callback function that
// processes the Web Service return value.
function SucceededCallback(result) {
    if (result == "true") {
        switchEditMode(indexModifiko, grida);
    }
    else {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukKeniDrejtaPerVeprim"));
    }
}

function switchEditMode(index, grida) {
    $("#hfRuaj").val('Modifiko');
    ASPxMenu1.GetItemByName('Ruaj').SetVisible(true);
    myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    pastro();
    if (grida == "gvKodifikimArtikulli")
        gvKodifikimArtikulli.StartEditRow(index);
    else if (grida == "gvKodifikimArtikulliGr2")
        gvKodifikimArtikulliGr2.StartEditRow(index);
    else if (grida == "gvKodifikimArtikulliGr3")
        gvKodifikimArtikulliGr3.StartEditRow(index);
    indexEdit = index;
}

function Init() {
    changeName();
    PageControl.GetTabByName('Informacion').SetVisible(Utils.getUrlVar("llojiart") == 'aqt');  
    ASPxMenu1.GetItemByName('Ruaj').SetVisible(false);    
    myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    editmode = false;
    indexEdit = -1;
}

function changeName() {
    try {
        myFaqeCelje.changeNameRegjistrime('KodifikimArtikulli.aspx?llojiart=' + Utils.getUrlVar("llojiart"), 0);
    }
    catch (e) { console.log("HApur si lupe, nuk ka nevoje te ndryshohet emri tek menu bar!"); }
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(EndRequestHandler);
}


var editorPrind;
var editorNiveli;
var editorKodi;
var editorGrupi;

function KeyPresPrind(kodi, editor, key) {//kur shtypet nje key per kolonen Autorizimeve
    if (kodi == 13) {
        indeksi = key;
        editorPrind = Utils.ktheKontroll('IdPrindi');
        editorNiveli = Utils.ktheKontroll('NivelKodifikimi');
        if (!editorNiveli || !editorPrind) return;
        identifikuesPerPopupKodifikimin = "Kodifikim";
        KodifikimArtikulli_Click();

        var hf1 = $("#hfNiveli");
        hf1.val(editorNiveli.GetText());
    }
}

function LostFocusPrind(key) {//kur humb fokusin kolona Autorizimeve
    indeksi = key;
    editorPrind = Utils.ktheKontroll('IdPrindi');
    editorNiveli = Utils.ktheKontroll('NivelKodifikimi');
    if (!editorPrind || !editorNiveli) return;
    var hf1 = $("#hfNiveli");
    hf1.val(editorNiveli.GetText());
}

function enable() {//kur humb fokusin kolona Autorizimeve      
    editorPrind = Utils.ktheKontroll('IdPrindi');
    editorKodi = Utils.ktheKontroll('KodKodifikimi');
    if (!editorPrind || !editorKodi) return;
    if ($("#hfRuaj").val() == "Modifiko") {
        editorPrind.SetEnabled(true);
 
        editorKodi.SetEnabled(false);
    }
    else {
        editorPrind.SetEnabled(true);
        editorKodi.SetEnabled(true);
    }
}

function ButtonClickedPrind(editor, key) {//kur klikon butonin e kolones Autorizimeve
    indeksi = key;
    editorPrind = Utils.ktheKontroll('IdPrindi');
    editorNiveli = Utils.ktheKontroll('NivelKodifikimi');
    if (!editorPrind || !editorNiveli) return;
    identifikuesPerPopupKodifikimin = "Kodifikim";
    KodifikimArtikulli_Click();
    var hf1 = $("#hfNiveli");
    hf1.val(editorNiveli.GetText());
}

function TextChangedPrind(key) {//kur ndryshon texti tek kolona Autorizimeve
    indeksi = key;
    editorPrind = Utils.ktheKontroll('IdPrindi');
    editorNiveli = Utils.ktheKontroll('NivelKodifikimi');
    if (!editorPrind || !editorNiveli) return;
    var a = new Array();
    a = editorPrind.GetText().toString().split(',');
    editorPrind.SetText(a[1]);
    if (a[2] != undefined)
        editorNiveli.SetText(parseInt(a[2]) + 1);
    var hf = $("#hfPrindi");
    hf.val(a[0]);
    var hf1 = $("#hfNiveli");
    hf1.val(editorNiveli.GetText());
    //var pershkKonfigAmb = cmbModeli.GetSelectedItem().GetColumnText("PershkrimKonfigAmbjente");
    //if (pershkKonfigAmb != undefined)
    //    lblKonfigurimi.SetText(pershkKonfigAmb);
}

function ndryshoPrindi(s, e) {
    var niveli = btneEmertimPrindi.GetSelectedItem();
    if (niveli != null)
        txtNiveli.SetText(parseInt(niveli.GetColumnText('NivelKodifikimi')) + 1);
    else
        txtNiveli.SetText("1");
    merrSkeme(s);
}

function merrSkeme(s) {
    if (s.GetValue() == null)
        return;
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "merrSkeme"),
        data: JSON.stringify({ idkodifikim: s.GetValue() })
    }).done(SucceededCallbackSkemaPrindi);
    gvAmortizimi.PerformCallback(s.GetValue());
}

function SucceededCallbackSkemaPrindi(result) {
    cmbFormati.SetValue(result.IdFormatiSerial);
    Utils.SelectComboItem(btneSkema, result.IdSkemaKontabel, null);
    Utils.SelectComboItem(btneLlogInv, result.IdLlogariInventari, null);
    Utils.SelectComboItem(btneLlogTretet, result.IdLlogariNeProces, null);
    Utils.SelectComboItem(btnLlogPakesim, result.IdLlogariPakesimi, null);
    Utils.SelectComboItem(btnLlogShpe, result.IdLlogariShpenzimi, null);
    Utils.SelectComboItem(btneLlogBle, result.IdLlogariVlere, null);
    Utils.SelectComboItem(btneLlogShit, result.IdLlogariShitje, null);
    Utils.SelectComboItem(cmbLlogAmortizimi, result.IdLlogariAmortizimi, null);

    //btneLlogInv.SetValue(result.IdLlogariInventari);
    //btneLlogBle.SetValue(result.IdLlogariVlere);
    //btneLlogShit.SetValue(result.IdLlogariShitje);
    //btneLlogTretet.SetValue(result.IdLlogariNeProces);

    //btnLlogShpe.SetValue(result.IdLlogariShpenzimi);
    //cmbLlogAmortizimi.SetValue(result.IdLlogariAmortizimi);
}

function menu_click(s, e) {
    if (Utils.getUrlVar("llojiart") == "aqt")
        menu_click_aqt(s, e);
    else
        menu_click_art(s, e);
}

function menu_click_art(s, e) {
    var hfRuaj = $('#hfRuaj');
    var gridaObj = grida == "gvKodifikimArtikulli" ? gvKodifikimArtikulli : grida == "gvKodifikimArtikulliGr2" ? gvKodifikimArtikulliGr2 : gvKodifikimArtikulliGr3;
    myMenu.menu_click_celjevogel(s, e, hfRuaj, gridaObj, hfTeDrejta);
    switch (e.item.name) {
        case 'Shto':
            ASPxMenu1.GetItemByName('Ruaj').SetVisible(true);
            var hf1 = $("#hfNiveli");
            hf1.val(1);
            pastro();
            break;
        case "Sinkronizo":
            myButtonClickLupa.LupaUniversal_Click(hfState.Get("headerPopUpTextZgjidhNdermarrjet"), 'LupaNdermarjeBij.aspx?vjenNga=KodifikimArtikulli', 500, 500);
            break;
        case 'Modifiko':
            ASPxMenu1.GetItemByName('Ruaj').SetVisible(true);
            pastro();
            break;
        case 'Arkiva':
            if ((PageControl.GetActiveTab() == PageControl.GetTabByName("Grupimi 1") && gvKodifikimArtikulli.IsEditing() == true) || (PageControl.GetActiveTab() == PageControl.GetTabByName("Grupimi 2") && gvKodifikimArtikulliGr2.IsEditing() == true) || (PageControl.GetActiveTab() == PageControl.GetTabByName("Grupimi 3") && gvKodifikimArtikulliGr3.IsEditing() == true)) {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgRuajGrupinDheCaktoArkive"));
                e.processOnServer = false;
                return;
            }
            ButtonClickArkiva();
            e.processOnServer = false;
            break;
        case "OK":
            e.processOnServer = false;
            ZgjidhRreshtaNgaLupa();
            break;

    }
}

function menu_click_aqt(s, e) {
    hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    hfId = $('#hfId'); //hidden fieldi qe ruan id  e rreshtit te selektuar
    switch (e.item.name) {
        case 'Fshi':
            myMenu.FshiClick(e);
            break;
        case 'Shto':
            mbush = false;
            hfShtimModifikim.val("shtim"); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
            hfId.val(0); //hidden fieldi qe ruan id  e rreshtit te selektuar
            indexModifiko = -1; //indexi i reshtit te selektuar
            pastrofusha();
            PageControl.SetActiveTabIndex(3);
            myMenu.PercaktoMenuSipasTabit(3, hfTeDrejta, hfShtimModifikim);
            e.processOnServer = false;
            break;
        case 'Ruaj':
            Utils.shfaqLoadingGif();;
            myMenu.RuajClick(s, e, false, PageControl);
            merrTeDhenaNorma();
            break;
        case 'Modifiko':
            myMenu.ModifikoClick(e);
            break;
        case "OK":
            e.processOnServer = false;
            ZgjidhRreshtaNgaLupa();
            break;
    }
}

function ZgjidhRreshtaNgaLupa() {
    var grida;
    switch (Utils.getUrlVar("llojKodifikimi")) {
        case "1":
            grida = gvKodifikimArtikulli;
            break;
        case "2":
            grida = gvKodifikimArtikulliGr2;
            break;
        case "3":
            grida = gvKodifikimArtikulliGr3;
            break;
    }
    grida.GetSelectedFieldValues('IdKodifikimi;KodKodifikimi;PershkrimKodifikimi;NivelKodifikimi;IdLlogariPakesimi;LlogPakesim', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    var s = new String();
    //s += values[0];
    s = s + values[0];
    var vl = s.split(",");
    switch (window.parent.grida) {
        case "1":
            Utils.SelectComboItem(window.parent.btneKodifikimi1, vl[0], vl[1]);
            window.parent.btneKodifikimi1.SetFocus(true);
            window.parent.UpdateGrida(vl[0]);
            Utils.SelectComboItem(window.parent.btnLlogPakesim, vl[4], vl[5]);
            break;
        case "2":
            Utils.SelectComboItem(window.parent.btneKodifikimi2, vl[0], vl[1]);
            window.parent.btneKodifikimi2.SetFocus(true);
            break;
        case "3":
            Utils.SelectComboItem(window.parent.btneKodifikimi3, vl[0], vl[1]);
            window.parent.btneKodifikimi3.SetFocus(true);
            break;
    }
    window.parent.popupUniversal.Hide();
}

function EndCallbackGrida(s, e) {
    if ($('#hfRuaj').val() == 'Modifiko' || $('#hfRuaj').val() == 'Ruaj') {
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "merrMesazhNgaSesioni"),
            data: JSON.stringify({})
        }).done(SucceededCallbackMesazhi);
    }
}
function SucceededCallbackMesazhi(result) {
    if (result == undefined || result == null || result == ':')
        return;
    var msgColor = result.substring(result.lastIndexOf(':') + 1);
    var msgDesc = result.substring(0, result.lastIndexOf(':'));
    switch (msgColor) {
        case "Green":
            myMesazh.ShtoMesazhSuksesi(msgDesc);
            ASPxMenu1.GetItemByName('Ruaj').SetVisible(false);
            break;
        case "Red":
            myMesazh.ShtoMesazhGabimi(msgDesc);
            break;
        default:
            break;
    }
}

var widthLupaLlogaria = 750;
var heightLupaLlogaria = 600;
var widthLupaSkema = 1150;
var heightLupaSkema = 600;
function kontrolloSkema() {


}
function Skema_Click() {

    var queryStr = '';
    var parametri = Utils.getUrlVar('llojiart');
    myButtonClickLupa.Skema_Click(hfState.Get("headerZgjidhSkemenkont"), queryStr, widthLupaSkema, heightLupaSkema, 1, parametri);
}
function LlogariI_Click() {
    var hf = $("#hfLupaLlogInv")[0];
    var queryStr = hf.value;
    myButtonClickLupa.ButtonClickLlogaria(hfState.Get("popupAdministrimiUniversal"), queryStr, widthLupaLlogaria, heightLupaLlogaria);
}

function LlogariB_Click() {
    var hf = $("#hfLupaLlogBle")[0];
    var queryStr = hf.value;
    myButtonClickLupa.ButtonClickLlogaria(hfState.Get("popupAdministrimiUniversal"), queryStr, widthLupaLlogaria, heightLupaLlogaria);
}

function LlogariS_Click() {
    var hf = $("#hfLupaLlogShit")[0];
    var queryStr = hf.value;
    identikuesPerPopupLlogari = '';
    myButtonClickLupa.ButtonClickLlogaria(hfState.Get("popupAdministrimiUniversal"), queryStr, widthLupaLlogaria, heightLupaLlogaria);
}

function LlogariShp_Click() {
    var hf = $("#hfLupaLlogShpe")[0];
    var queryStr = hf.value;

    myButtonClickLupa.ButtonClickLlogaria(hfState.Get("popupAdministrimiUniversal"), queryStr, widthLupaLlogaria, heightLupaLlogaria);
}

function LlogariAmor_Click() {
    var hf = $("#hfLupaLlogAmortizimi")[0];
    var queryStr = hf.value;
    myButtonClickLupa.ButtonClickLlogaria(hfState.Get("popupAdministrimiUniversal"), queryStr, widthLupaLlogaria, heightLupaLlogaria);
}

function LlogariT_Click() {
    var hf = $("#hfLupaLlogTretet")[0];
    var queryStr = hf.value;
    myButtonClickLupa.ButtonClickLlogaria(hfState.Get("popupAdministrimiUniversal"), queryStr, widthLupaLlogaria, heightLupaLlogaria);
}

function LlogariPakesim_Click() {
    var hf = $("#hfLupaLlogPakesim")[0];
    var queryStr = hf.value;

    myButtonClickLupa.ButtonClickLlogaria(hfState.Get("popupAdministrimiUniversal"), queryStr, widthLupaLlogaria, heightLupaLlogaria);
}

function btneSkemaTextChanged(s, e) {
    var selectedSkema = btneSkema.GetSelectedItem();
    if (selectedSkema != null) {
        Utils.SelectComboItem(btneLlogInv, null, selectedSkema.GetColumnText('NrLlogariInventari'));
        Utils.SelectComboItem(btneLlogTretet, null, selectedSkema.GetColumnText('NrLlogariTekTeTretet'));
        Utils.SelectComboItem(btnLlogShpe, null, selectedSkema.GetColumnText('NrLlogariShpenzimi'));
        Utils.SelectComboItem(btneLlogBle, null, selectedSkema.GetColumnText('NrLlogariBlerje'));
        Utils.SelectComboItem(btneLlogShit, null, selectedSkema.GetColumnText('NrLlogariShitje'));
        Utils.SelectComboItem(cmbLlogAmortizimi, null, selectedSkema.GetColumnText('NrLlogariAmortizimi'));
        Utils.SelectComboItem(btnLlogPakesim, null, selectedSkema.GetColumnText('NrLlogariPakesimi'));
    }
}

function pastrofusha() {
    txtKodi.SetText('');
    txtEmertimi.SetText('');
    txtKodi.SetEnabled(true);
    cmbFormati.SetSelectedIndex(0);
    btneEmertimPrindi.SetText('');
    btneEmertimPrindi.ClearItems();;
    txtNiveli.SetText('1');
    $('#hfLidhur').val('False');
    btneSkema.SetSelectedIndex(-1);
    btneLlogInv.SetSelectedIndex(-1);
    btneLlogBle.SetSelectedIndex(-1);
    btneLlogShit.SetSelectedIndex(-1);
    btneLlogTretet.SetSelectedIndex(-1);
    btnLlogPakesim.SetSelectedIndex(-1);
    btnLlogShpe.SetSelectedIndex(-1);
    cmbLlogAmortizimi.SetSelectedIndex(-1);
    visibleLlog();
    aktivizoFusha(false);
    gvAmortizimi.PerformCallback();
}

function valido(s, e) {
    myFaqeCelje.validim(s, e);//, PageControl, hfTeDrejta, $('#hfShtimModifikim'));
}

/*
Function: EndRequestHandler
    
Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe array-t e perdorura.
Shiko funksionet  <ndryshoKonfigurimin>.
*/
function EndRequestHandler(sender, args) {
    var hf = $("#hfStatusi");
    var hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    var hfId = $('#hfId');  //hidden fieldi qe ruan id  e rreshtit te selektuar    
    if (hf.val() == "true") {
        if (hfShtimModifikim.val() != "modifikim") {
            mbush = false;
            hfShtimModifikim.val("shtim"); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
            hfId.val(0); //hidden fieldi qe ruan id  e rreshtit te selektuar
            indexModifiko = -1; //indexi i reshtit te selektuar      
            pastrofusha();
            hf.val("false");
            if (PageControl.GetActiveTabIndex() != 0) {
                PageControl.SetActiveTabIndex(3);
                myMenu.PercaktoMenuSipasTabit(3, hfTeDrejta, hfShtimModifikim);
            }
            if (Utils.getUrlVar("lupe") == 'true') {
                switch (Utils.getUrlVar("llojKodifikimi")) {
                    case "1":
                        gvKodifikimArtikulli.PerformCallback();
                        break;
                    case "2":
                        gvKodifikimArtikulliGr2.PerformCallback();
                        break;
                    case "3":
                        gvKodifikimArtikulliGr3.PerformCallback();
                        break;
                }
            }
            else {
                gvKodifikimArtikulli.PerformCallback();
                gvKodifikimArtikulliGr2.PerformCallback();
                gvKodifikimArtikulliGr3.PerformCallback();
            }
        }
        else {
            hf.val("false");
            if (PageControl.GetActiveTabIndex() != 0) {
                if ($('#hfGrup').val() == 0) {
                    PageControl.SetActiveTabIndex(0);
                    myMenu.PercaktoMenuSipasTabit(0, hfTeDrejta, hfShtimModifikim);
                    gvKodifikimArtikulli.PerformCallback();
                }
                else if ($('#hfGrup').val() == 1) {
                    PageControl.SetActiveTabIndex(1);
                    myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, hfShtimModifikim);
                    gvKodifikimArtikulliGr2.PerformCallback();
                }
                else {
                    PageControl.SetActiveTabIndex(2);
                    myMenu.PercaktoMenuSipasTabit(2, hfTeDrejta, hfShtimModifikim);
                    gvKodifikimArtikulliGr3.PerformCallback();
                }
            }
            else if (PageControl.GetActiveTabIndex() == 0)
                gvKodifikimArtikulli.PerformCallback();
        }
    }
    else
        myMenu.PercaktoMenuSipasTabit(PageControl.GetActiveTabIndex(), hfTeDrejta, hfShtimModifikim);
    visibleTabs();
    Utils.hiqLoadingGif();;
    return indexModifiko;
}

function aktivizoFusha(result) {
    if (result == "true") {
        cmbFormati.SetEnabled(false);
        btneEmertimPrindi.SetEnabled(false);
        btneSkema.SetEnabled(false);;
        btneLlogInv.SetEnabled(false);
        btneLlogBle.SetEnabled(false);
        btneLlogShit.SetEnabled(false);
        btneLlogTretet.SetEnabled(false);
        btnLlogPakesim.SetEnabled(false);
        btnLlogShpe.SetEnabled(false);
        cmbLlogAmortizimi.SetEnabled(false);
    }
    else {
        cmbFormati.SetEnabled(true);
        btneEmertimPrindi.SetEnabled(true);
        btneSkema.SetEnabled(true);
        btneLlogInv.SetEnabled(true);
        btneLlogBle.SetEnabled(true);
        btneLlogShit.SetEnabled(true);
        btneLlogTretet.SetEnabled(true);
        btnLlogPakesim.SetEnabled(true);
        btnLlogShpe.SetEnabled(true);
        cmbLlogAmortizimi.SetEnabled(true);
    }
}

//therret funksionin qe ben enabled dhe disabled fushat sipas lidhjes
function SucceededCallbackLidhur(result) {
    var hfLidhur = $("#hfLidhur")[0];
    hfLidhur.value = result;
    aktivizoFusha(result);
}

//merr te dhenat e rreshtit te selektuar
function mbushfusha() {
    mbush = false;
    $('#hfShtimModifikim')[0].value = "modifikim";

    if ($('#hfGrup').val() == 0) {
        indexModifiko = gvKodifikimArtikulli.GetFocusedRowIndex();
        if (indexModifiko == -1)
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniNivelCmimi"));
        else
            gvKodifikimArtikulli.GetRowValues(indexModifiko, 'IdKodifikimi;KodKodifikimi;PershkrimKodifikimi;IdPrindi;NivelKodifikimi;IdSkemaKontabel;IdLlogariVlere;IdLlogariShitje;IdLlogariInventari;IdLlogariShpenzimi;IdLlogariNeProces;IdLlogariAmortizimi;IdFormatiSerial;Prindi;Skema;LlogAmortizimi;LlogInventari;LlogNeProces;LlogShitje;LlogShpenzimi;LlogVlere;Formati;PershkrimPrind;NivelPrind;LlogPakesim;IdLlogariPakesimi', OnGetRowValuesMod);
    }
    else if ($('#hfGrup').val() == 1) {
        indexModifiko = gvKodifikimArtikulliGr2.GetFocusedRowIndex();
        if (indexModifiko == -1)
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniNivelCmimi"));
        else
            gvKodifikimArtikulliGr2.GetRowValues(indexModifiko, 'IdKodifikimi;KodKodifikimi;PershkrimKodifikimi;IdPrindi;NivelKodifikimi;IdSkemaKontabel;IdLlogariVlere;IdLlogariShitje;IdLlogariInventari;IdLlogariShpenzimi;IdLlogariNeProces;IdLlogariAmortizimi;IdFormatiSerial;Prindi;Skema;LlogAmortizimi;LlogInventari;LlogNeProces;LlogShitje;LlogShpenzimi;LlogVlere;Formati;PershkrimPrind;NivelPrind;LlogPakesim;IdLlogariPakesimi', OnGetRowValuesMod);
    } else if ($('#hfGrup').val() == 2) {
        indexModifiko = gvKodifikimArtikulliGr3.GetFocusedRowIndex();
        if (indexModifiko == -1)
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniNivelCmimi"));
        else
            gvKodifikimArtikulliGr3.GetRowValues(indexModifiko, 'IdKodifikimi;KodKodifikimi;PershkrimKodifikimi;IdPrindi;NivelKodifikimi;IdSkemaKontabel;IdLlogariVlere;IdLlogariShitje;IdLlogariInventari;IdLlogariShpenzimi;IdLlogariNeProces;IdLlogariAmortizimi;IdFormatiSerial;Prindi;Skema;LlogAmortizimi;LlogInventari;LlogNeProces;LlogShitje;LlogShpenzimi;LlogVlere;Formati;PershkrimPrind;NivelPrind;LlogPakesim;IdLlogariPakesimi', OnGetRowValuesMod);
    }

    Utils.shfaqLoadingGif();;
}

function visibleLlog() {
    //llogaria pakesim vlere dalje e dukshme vetem per ndermarjet buxhetor
    var llojNdermarrje = hfState.Get('llojNdermarrje');
    if (llojNdermarrje == 2) {  ///nestila duhen komentuar per alphabank dhe c'komentuar per publikimet e tjera
        btnLlogPakesim.SetVisible(true);
        lblLlogPakesim.SetVisible(true);
    }///nestila duhen komentuar per alphabank dhe c'komentuar per publikimet e tjera
    else {
        btnLlogPakesim.SetVisible(false);
        lblLlogPakesim.SetVisible(false);
    }

    if ($('#hfGrup').val() == 1 || $('#hfGrup').val() == 2) {
        cmbFormati.SetVisible(false);
        btneSkema.SetVisible(false);
        btneLlogInv.SetVisible(false);
        btneLlogBle.SetVisible(false);
        btneLlogShit.SetVisible(false);
        btneLlogTretet.SetVisible(false);
        btnLlogShpe.SetVisible(false);
        btnLlogPakesim.SetVisible(false);
        cmbLlogAmortizimi.SetVisible(false);
        lblFormati.SetVisible(false);
        lblSkema.SetVisible(false);
        lblLlogInv.SetVisible(false);
        lblLlogBle.SetVisible(false);
        lblLlogShit.SetVisible(false);
        lblLlogTretet.SetVisible(false);
        lblLlogShpe.SetVisible(false);
        lblLlogAmortizimi.SetVisible(false);
        lblLlogPakesim.SetVisible(false);
        gvAmortizimi.SetVisible(false);
    }
    else {
        cmbFormati.SetVisible(true);
        btneSkema.SetVisible(true);
        btneLlogInv.SetVisible(true);
        btneLlogBle.SetVisible(true);
        btneLlogShit.SetVisible(true);
        btneLlogTretet.SetVisible(true);
        btnLlogShpe.SetVisible(true);

        cmbLlogAmortizimi.SetVisible(true);
        lblFormati.SetVisible(true);
        lblSkema.SetVisible(true);
        lblLlogInv.SetVisible(true);
        lblLlogBle.SetVisible(true);
        lblLlogShit.SetVisible(true);
        lblLlogTretet.SetVisible(true);
        lblLlogShpe.SetVisible(true);
        lblLlogAmortizimi.SetVisible(true);

        gvAmortizimi.SetVisible(true);
        if (llojNdermarrje == 2) {///nestila duhen komentuar per alphabank dhe c'komentuar per publikimet e tjera
            btnLlogPakesim.SetVisible(true);
            lblLlogPakesim.SetVisible(true);
        }
    }
}

//mbush fushat me te dhenat e rreshtit te selektuar
function OnGetRowValuesMod(values) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;
    $('#hfId').val(values[0]);
    if ($('#hfShtimModifikim').val() == "modifikim")
        txtKodi.SetEnabled(false);
    txtKodi.SetText(values[1]);
    txtEmertimi.SetText(values[2]);
    if (values[3] != null)
        Utils.SelectComboItem(btneEmertimPrindi, values[3], [values[13], values[22], values[23].toString()]);
    else btneEmertimPrindi.SetText('');
    cmbFormati.SetValue(values[12]);
    txtNiveli.SetText(values[4]);
    Utils.SelectComboItem(btneSkema, values[5], values[14]); //, 
    Utils.SelectComboItem(btneLlogInv, values[8], values[16]);
    Utils.SelectComboItem(btneLlogBle, values[6], values[20]);
    Utils.SelectComboItem(btneLlogShit, values[7], values[18]);
    Utils.SelectComboItem(btneLlogTretet, values[10], values[17]);
    Utils.SelectComboItem(btnLlogShpe, values[9], values[19]);
    Utils.SelectComboItem(cmbLlogAmortizimi, values[11], values[15]);
    Utils.SelectComboItem(btnLlogPakesim, values[25], values[24]);
    visibleLlog();
    gvAmortizimi.PerformCallback();
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "kaVeprimeKodifikim"),
        data: JSON.stringify({ iddokumenti: values[0] })
    }).done(SucceededCallbackLidhur);
    Utils.hiqLoadingGif();;
    if (kaloTab) {
        PageControl.SetActiveTabIndex(3);
        myMenu.PercaktoMenuSipasTabit(3, hfTeDrejta, $('#hfShtimModifikim'));
    }
}

function onNdryshimFokusi() {
    try {
        if (PageControl.GetActiveTabIndex() == 0 || PageControl.GetActiveTabIndex() == 1 || PageControl.GetActiveTabIndex() == 2)
            mbush = true;
    } catch (e) { }
}


function tabsActiveTabChanged(s, e) {
    if (Utils.getUrlVar("llojiart") == "aqt") {
        if ($('#hfGrup').val() == 0) {
            indexModifiko = gvKodifikimArtikulli.GetFocusedRowIndex();
            if (mbush) {
                if (indexModifiko !== -1) {
                    RowDblClickGrida(indexModifiko, 'gvKodifikimArtikulli');
                }
                else {
                    mbush = false;
                    $('#hfShtimModifikim').val('shtim');
                    $('#hfId').val(0);

                }
            }
        }
        else {
            if ($('#hfGrup').val() == 1) {
                indexModifiko = gvKodifikimArtikulliGr2.GetFocusedRowIndex();
                if (mbush) {
                    if (indexModifiko !== -1) {
                        RowDblClickGrida(indexModifiko, 'gvKodifikimArtikulliGr2');
                    }
                    else {
                        mbush = false;
                        $('#hfShtimModifikim').val('shtim');
                        $('#hfId').val(0);
                        pastrofusha();
                    }
                }
            }
            else if ($('#hfGrup').val() == 2) {
                indexModifiko = gvKodifikimArtikulliGr3.GetFocusedRowIndex();
                if (mbush) {
                    if (indexModifiko !== -1) {
                        RowDblClickGrida(indexModifiko, 'gvKodifikimArtikulliGr3');
                    }
                    else {
                        mbush = false;
                        $('#hfShtimModifikim').val('shtim');
                        $('#hfId').val(0);
                        pastrofusha();
                    }
                }
            }
        }
        kaloTab = false;
        myMenu.PercaktoMenuSipasTabit(e.tab.index, hfTeDrejta, $('#hfShtimModifikim'));
        ASPxMenu1.GetItemByName('OK').SetVisible(Utils.getUrlVar("lupe") == 'true');
        if (e.tab.index == 1 || e.tab.index == 2) {
            ASPxMenu1.GetItemByName('Ruaj').SetVisible(false);
            ASPxMenu1.GetItemByName('Modifiko').SetVisible(true);
        }
    }
}

function merrTeDhenaNorma() {//merren te dhenat qe ka grida
    var gridDataObject = $('#gridDataObject');
    colNorma = new Array();
    if (Utils.getUrlVar("llojiart") == "aqt") {
        if ($('#hfGrup').val() == 0) {
            for (i = 0; i < gvAmortizimi.cpRowCount; i++) {
                colNorma[i] = new Object();
                colNorma[i].Standart = Utils.ktheKontroll('lblStandart' + i).GetText();
                colNorma[i].IdLlojAmortizimi = Utils.ktheKontroll('cmbLlojAmortizimi' + i).GetValue();
                colNorma[i].NormeMagazine = Utils.ktheKontroll('cmbNormeMagazine' + i).GetText();
                colNorma[i].Norme = Utils.ktheKontroll('txtNorme' + i).GetText();
            }
        }
    }
    gridDataObject.val(JSON.stringify(colNorma));
}

function ButtonClickArkiva() {
    indexModifiko = gvKodifikimArtikulli.GetRowKey(gvKodifikimArtikulli.GetFocusedRowIndex())
    if (indexModifiko == -1)
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKodifikimArtikullZgjidhGrup"));
    else {
        popupUniversal.SetHeaderText(hfState.Get("regjisDokZgjidhDokPerTeBashkengjitur"));
        popupUniversal.SetSize(738, 548);
        //popupUniversal.SetContentUrl('LupaArkiva.aspx?vjenNga=Lista&veprimi=grupArtikull&idDok=' + indexModifiko + '&shtim_modifikim=modifikim');
        popupUniversal.SetContentUrl('LupaArkiva.aspx?vjenNga=Lista&veprimi=grupArtikull&idDok=' + indexModifiko
            //+ '&shtim_modifikim=modifikim'
            + "&tmpfolder=" + hfArkiva.Get("rootFolder"));
        popupUniversal.Show();
    }
}

function OnChangePrindi(s, e) {
    var v = s.GetText().split(',');
    s.SetText(v[0]);
}

function OnChange(s, e) {
    var v = s.GetText().split(';');
    s.SetText(v[0]);
}