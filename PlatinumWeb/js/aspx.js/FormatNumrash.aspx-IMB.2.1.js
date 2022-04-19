 ;
var arr = new Array();
var counter = 0;
var arr2 = new Array();
var counter2 = 0;

var btnFiltrat;
function checkText(s, e) {
    myMenu.checkText(s, e);
}

//variabla per te kaluar nga nje reshti i grides tek tjetri
var indexSel = 0;
//indexi i zgjedhur per modifikim
var indexModifiko;
//perdoret per te treguar nese duhet te mbushen tabet apo jo kjo perdoret qe tabet te mbushen vetem heren e pare dhe jo pas cdo here qe  kalohet nga nje tab tek tjetri
var mbush = false;
//perdoret per te ruajtur trupin e formatit te numrit
var colTrupiKonfig;


function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvFormati, "157", "");
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

$(document).ready(function () {
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

/*
Function: Poshte_click

perdoret per te selektuar rreshtin me poshte

Parameters:

e-eventi
*/
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvFormati, indexSel);
}

/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvFormati, indexSel);
}

/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvFormati, indexSel);
}

/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvFormati, indexSel);
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ? evt.keyCode : evt.charCode;
}

//metodat per te hapur faqen e modifikimit me double click
function OnGridDoubleClick(index) {
    //colTrupiKonfig = new Array();
    indexModifiko = index;
    lista = true;
    mbushfusha();
}

//merr te dhenat e rreshtit te selektuar
function mbushfusha() {
    mbush = false;
    $('#hfShtimModifikim').val("modifikim");
    indexModifiko = gvFormati.GetFocusedRowIndex();
    if (indexModifiko == -1)
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgShtoArtikullZgjidhArt"));
    else
        gvFormati.GetRowValues(indexModifiko, 'IdFormatKonfig;IdKategoria;IdNdermarrja;Kodi;Emertimi;IdKrijuesi;DtKrijimi;DtModifikimi;IdStatusDok;Kategoria', OnGetRowValuesMod);
}

//mbush fushat me te dhenat e rreshtit te selektuar
function OnGetRowValuesMod(values) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;

    $('#hfId').val(values[0]);
    if ($('#hfShtimModifikim').val() == "modifikim")
        txtKodi.SetEnabled(false);
    else
        txtKodi.SetEnabled(true);
    txtKodi.SetText(values[3]);
    txtEmertimi.SetText(values[4]);
    Kategoria_ComboBox.SetValue(values[1]);
    Kategoria_ComboBox.SetText(values[9]);
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "merrKonfigFormatNrTrupi"),
        data: JSON.stringify({ idKokaFormatNr: values[0], idPerdoruesi: hfState.Get('idPerdoruesi'), idNdermarrje: hfState.Get('idNdermarrje') })
    }).done(SucceededCallbackTrupiKonfig);
    if (kaloTab) {
        PageControl.SetActiveTabIndex(1);
        myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, $('#hfShtimModifikim'));
    }
}

function SucceededCallbackTrupiKonfig(result) {
    colTrupiKonfig = result;
    ShfaqTeDhenatTrupi();
}

function ShfaqTeDhenatTrupi() {
    if (colTrupiKonfig != undefined || colTrupiKonfig != null)
        if (gvTrupiFormatNr.cpNoRows > 15 * (gvTrupiFormatNr.cpNoPage + 1))
            for (i = 15 * gvTrupiFormatNr.cpNoPage; i < 15 * (gvTrupiFormatNr.cpNoPage + 1) ; i++) {
        
                editorSasia = Utils.ktheKontroll('FormatSasia' + i);
                editorCmimi = Utils.ktheKontroll('FormatCmimi' + i);
                editorVlefta = Utils.ktheKontroll('FormatVlefta' + i);
                editorZbritja = Utils.ktheKontroll('FormatZbritja' + i);

                editorSasia.SetValue(colTrupiKonfig[i].ShifraPasPresjesSasia);
                editorCmimi.SetValue(colTrupiKonfig[i].ShifraPasPresjesCmimi);
                editorVlefta.SetValue(colTrupiKonfig[i].ShifraPasPresjesVlefta);
                editorZbritja.SetValue(colTrupiKonfig[i].ShifraPasPresjesZbritja);
            }
        else {
            for (i = 15 * gvTrupiFormatNr.cpNoPage; i < gvTrupiFormatNr.cpNoRows; i++) {
                editorSasia = Utils.ktheKontroll('FormatSasia' + i);
                editorCmimi = Utils.ktheKontroll('FormatCmimi' + i);
                editorVlefta = Utils.ktheKontroll('FormatVlefta' + i);
                editorZbritja = Utils.ktheKontroll('FormatZbritja' + i);

                editorSasia.SetValue(colTrupiKonfig[i].ShifraPasPresjesSasia);
                editorCmimi.SetValue(colTrupiKonfig[i].ShifraPasPresjesCmimi);
                editorVlefta.SetValue(colTrupiKonfig[i].ShifraPasPresjesVlefta);
                editorZbritja.SetValue(colTrupiKonfig[i].ShifraPasPresjesZbritja);
            }
        }
}

function merrTeDhenatTrupi(s, e) {//merren te dhenat qe ka grida
    if (colTrupiKonfig != undefined || colTrupiKonfig != null) {
        if (gvTrupiFormatNr.cpNoRows > 15 * (gvTrupiFormatNr.cpNoPage + 1))
            for (i = 15 * gvTrupiFormatNr.cpNoPage; i < 15 * (gvTrupiFormatNr.cpNoPage + 1); i++) {
                editorSasia = Utils.ktheKontroll('FormatSasia' + i);
                editorCmimi = Utils.ktheKontroll('FormatCmimi' + i);
                editorVlefta = Utils.ktheKontroll('FormatVlefta' + i);
                editorZbritja = Utils.ktheKontroll('FormatZbritja' + i);
                colTrupiKonfig[i].ShifraPasPresjesSasia = editorSasia.GetValue();
                colTrupiKonfig[i].ShifraPasPresjesCmimi = editorCmimi.GetValue();
                colTrupiKonfig[i].ShifraPasPresjesVlefta = editorVlefta.GetValue();
                colTrupiKonfig[i].ShifraPasPresjesZbritja = editorZbritja.GetValue();
                delete colTrupiKonfig[i].__type;
            }
        else {
            for (i = 15 * gvTrupiFormatNr.cpNoPage; i < gvTrupiFormatNr.cpNoRows; i++) {
                editorSasia = Utils.ktheKontroll('FormatSasia' + i);
                editorCmimi = Utils.ktheKontroll('FormatCmimi' + i);
                editorVlefta = Utils.ktheKontroll('FormatVlefta' + i);
                editorZbritja = Utils.ktheKontroll('FormatZbritja' + i);
                colTrupiKonfig[i].ShifraPasPresjesSasia = editorSasia.GetValue();
                colTrupiKonfig[i].ShifraPasPresjesCmimi = editorCmimi.GetValue();
                colTrupiKonfig[i].ShifraPasPresjesVlefta = editorVlefta.GetValue();
                colTrupiKonfig[i].ShifraPasPresjesZbritja = editorZbritja.GetValue();
                delete colTrupiKonfig[i].__type;
            }
        }
        $('#hfTrupiKonfig').val(JSON.stringify(colTrupiKonfig));
    }
}


function Init() {
    myFaqeCelje.changeName('FormatNumrash.aspx',0, null);
    myMenu.PercaktoMenuSipasTabit(0, hfTeDrejta, $("#hfShtimModifikim"));
}

function changeName() {
    myFaqeCelje.changeNameRegjistrime('FormatNumrash.aspx',0);
}

function menu_click(s, e) {
    var hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    var hfId = $('#hfId'); //hidden fieldi qe ruan id  e rreshtit te selektuar
    var ruajbuxhetet = false; //i here per i here
    myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, ruajbuxhetet, false, indexModifiko, pastrofusha, null, null, undefined, undefined, undefined);
    if (e.item.name == 'Ruaj') {
        merrTeDhenatTrupi();
    }
    if (e.item.name == 'Shto')
        txtKodi.SetEnabled(true);
}

function valido(s, e) {
    myFaqeCelje.valido(s, e, PageControl, hfTeDrejta, $('#hfShtimModifikim'));
}

function pastrofusha() {    
    txtKodi.SetText('');
    txtEmertimi.SetText('');
    Kategoria_ComboBox.SetSelectedIndex(-1);
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "merrKonfigFormatNrTrupi"),
        data: JSON.stringify({ idKokaFormatNr: -1, idPerdoruesi: hfState.Get('idPerdoruesi'), idNdermarrje: hfState.Get('idNdermarrje') })
    }).done(SucceededCallbackTrupiKonfig);
}

function pastroHf() {
    $("#hfKategori")[0].value = "";
    $("#hfMonedha")[0].value = "";
    $("#hfSasi")[0].value = "";
    $("#hfCmimi")[0].value = "";
    $("#hfVlefta")[0].value = "";
    $("#hfZbritje")[0].value = "";
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

function TextChangedMonedha(s, e) {
    var hfMon = $("#hfMonedha")[0];
    var mon = s.GetText();
    s.SetText(mon.split(',')[0]);
    hfMon.value = mon.split(',')[0];
}

function ButtonClickedMonedha(s, e) {

}

function InitMonedha(s, e) {
    var hfMon = $("#hfMonedha")[0];
    hfMon.value = s.GetText();
}

function TextChangedFormatNr(s, e, emerFushe) {
    var hfFormat;
    var formati = s.GetText();
    if (emerFushe == "IdFormatSasia") {
        hfFormat = $("#hfSasi")[0];
        hfFormat.value = formati.split(',')[1];
    }
    else if (emerFushe == "IdFormatCmimi") {
        hfFormat = $("#hfCmimi")[0];
        hfFormat.value = formati.split(',')[1];
    }
    else if (emerFushe == "IdFormatVlefta") {
        hfFormat = $("#hfVlefta")[0];
        hfFormat.value = formati.split(',')[1];
    }
    else if (emerFushe == "IdFormatZbritja") {
        hfFormat = $("#hfZbritje")[0];
        hfFormat.value = formati.split(',')[1];
    }
    s.SetText(formati.split(',')[1]);
}

function TextChangedKategoria(s, e) {
    //var kategoria = s.GetText();
    var hfKat = $("#hfKategori")[0];
    hfKat.value = s.GetText();
}

function InitKategoria(s, e) {
    var hfKat = $("#hfKategori")[0];
    hfKat.value = s.GetText();
}

function InitFormatNr(s, e, emerFushe) {
    var hfFormat;
    var vlera = s.GetText();
    if (s.GetValue() == null)
        vlera = '0';
    if (emerFushe == "IdFormatSasia") {
        hfFormat = $("#hfSasi")[0];
        hfFormat.value = vlera;
    }
    else if (emerFushe == "IdFormatCmimi") {
        hfFormat = $("#hfCmimi")[0];
        hfFormat.value = vlera;
    }
    else if (emerFushe == "IdFormatVlefta") {
        hfFormat = $("#hfVlefta")[0];
        hfFormat.value = vlera;
    }
    else if (emerFushe == "IdFormatZbritja") {
        hfFormat = $("#hfZbritje")[0];
        hfFormat.value = vlera;
    }
    s.SetText(vlera);
}

function onNdryshimFokusi() {
    try {
        if (PageControl.GetActiveTabIndex() == 0)
            mbush = true;
    } catch (e) { }
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
    indexModifiko = myFaqeCelje.EndRequestHandlerPas(sender, args, hf, hfShtimModifikim, hfId, indexModifiko, PageControl, gvFormati, "157", hfTeDrejta)
    myMenu.PercaktoMenuSipasTabit(PageControl.GetActiveTabIndex(), hfTeDrejta, $('#hfShtimModifikim'));
}

function tabsActiveTabChanged(s, e) {
    indexModifiko = gvFormati.GetFocusedRowIndex();
    if (mbush) {
        if (indexModifiko !== -1) {
            OnGridDoubleClick(indexModifiko);
        }
        else {
            mbush = false;
            $('#hfShtimModifikim').val('shtim');
            $('#hfId').val(0);
            pastrofusha();
        }
    }
    kaloTab = false;
    myMenu.PercaktoMenuSipasTabit(e.tab.index, hfTeDrejta, $('#hfShtimModifikim'));
}

function TextChangedFormati(fusha, editori, indeksi) {

}

function SelectedIndexFormati(fusha, editori, indeksi) {

}

function Kategoria_ComboBoxSelectedIndexChanged(s, e) {
    
}
function Click_ButtonOk(s, e) {
    popFshi.Hide();
    Utils.shfaqLoadingGif();;
}
function Row_DblClick(s, e) {
    OnGridDoubleClick(e.visibleIndex); 
    kaloTab=true;
}