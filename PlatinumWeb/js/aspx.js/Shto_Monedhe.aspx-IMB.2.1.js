//variabla per te kaluar nga nje reshti i grides tek tjetri
var indexSel = 0;
//indexi i zgjedhur per modifikim
var indexModifiko;
//perdoret per te treguar nese duhet te mbushen tabet apo jo kjo perdoret qe tabet te mbushen vetem heren e pare dhe jo pas cdo here qe  kalohet nga nje tab tek tjetri
var mbush = true;
//perdoret per te daluar rastet kur bejme double click apo thjesht click ne menyre qe ne double click te kaloje tek tabi i dyte
var kaloTab = false;
//perdoret per griden e llogarive ne menyre qe te lodohet vetem ne momentin kur shkon ne tabin e saj
var lista = true;
var widthLupaLogaria = 750, heightLupaLlogaria = 600;
var widthLupaAutorizime = 600, heightLupaAutorizime = 600;
var ndryshuarFormatNr = false;



//per filtrat
function checkText(s, e) {
    myMenu.checkText(s, e);
}

var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, ASPxGridView_Monedhat, "133", cmbKonfigurimi.GetText());
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

var ndryshuarKurs = false;

//metodat per te hapur faqen e modifikimit me double click
function OnGridDoubleClick(index) {
    indexModifiko = index;
    lista = true;
    mbushfusha();
}

/*
Function: Poshte_click

perdoret per te selektuar rreshtin me poshte

Parameters:

e-eventi
*/
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, ASPxGridView_Monedhat, indexSel);
}

/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, ASPxGridView_Monedhat, indexSel);
}

/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, ASPxGridView_Monedhat, indexSel);
}

/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, ASPxGridView_Monedhat, indexSel);
}

function formatoFushaDevi() {
    if (gvKurset.cpNoRows > 15 * (gvKurset.cpNoPage + 1))
        for (i = 15 * gvKurset.cpNoPage; i < 15 * (gvKurset.cpNoPage + 1) ; i++) {
            Utils.setFormatNumri(Utils.ktheKontroll('VleraKursi' + i), btneFormatNumri.GetValue());
            Utils.formatoTextBox(Utils.ktheKontroll('VleraKursi' + i));
        }
    else
        for (i = 15 * gvKurset.cpNoPage; i < gvKurset.cpNoRows; i++) {
            Utils.setFormatNumri(Utils.ktheKontroll('VleraKursi' + i), btneFormatNumri.GetValue());
            Utils.formatoTextBox(Utils.ktheKontroll('VleraKursi' + i));
        }
}

/*
Function: menu_click

perdoret per veprimet e menuse ne javascript

Parameters:

e-eventi
*/
function menu_click(s, e) {
    hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    hfId = $('#hfId'); //hidden fieldi qe ruan id  e rreshtit te selektuar
    if (e.item.name == "Ruaj") {
        Utils.vendosVlereNeHiddenField("cmbAutorizimiHf", cmbAutorizimi.GetText());
        Utils.shfaqLoadingGif();;
        merrTeDhenaKursi();
    }
    myMenu.menu_click(s, e, hfShtimModifikim, hfId, monedhat_PageControl, false, undefined, indexModifiko, pastrofusha, SucceededCallbackKonfig, resultkonf, colKontrollet, aktivizoFusha, colAtrTrupi);
}

function merrTeDhenaKursi(s, e) {//merren te dhenat qe ka grida e kurseve 
    if ((colKurset.length > 0 || ndryshuarKurs) && (colKurset != undefined || colKurset != null)) {
        if (gvKurset.cpNoRows > 15 * (gvKurset.cpNoPage + 1))
            for (i = 15 * gvKurset.cpNoPage; i < 15 * (gvKurset.cpNoPage + 1) ; i++) {
                editorLloji = Utils.ktheKontroll('LlojKursi' + i);
                editorVlera = Utils.ktheKontroll('VleraKursi' + i);
                editorData = Utils.ktheKontroll('DataKursit' + i);
                editorNjesia = Utils.ktheKontroll('NjesiaKursit' + i);
                if (colKurset[i] == undefined) {
                    colKurset[i] = new Object();
                }
                colKurset[i].LlojKursi = editorLloji.GetValue();
                colKurset[i].VleraKursi = editorVlera.GetText();
                colKurset[i].DataKursit = editorData.GetText();
                colKurset[i].NjesiaKursit = editorNjesia.GetText();
            }
        else
            for (i = 15 * gvKurset.cpNoPage; i < gvKurset.cpNoRows; i++) {
                editorLloji = Utils.ktheKontroll('LlojKursi' + i);
                editorVlera = Utils.ktheKontroll('VleraKursi' + i);
                editorData = Utils.ktheKontroll('DataKursit' + i);
                editorNjesia = Utils.ktheKontroll('NjesiaKursit' + i);
                if (colKurset[i] == undefined) {
                    colKurset[i] = new Object();
                }
                colKurset[i].LlojKursi = editorLloji.GetValue();
                colKurset[i].VleraKursi = editorVlera.GetText();
                colKurset[i].DataKursit = editorData.GetText();
                colKurset[i].NjesiaKursit = editorNjesia.GetText();
            }
        $('#hfKurset').val(JSON.stringify(colKurset));
    }
}

function ShfaqTeDhenat() {
    if (colKurset != undefined || colKurset != null)
        if (gvKurset.cpNoRows > 15 * (gvKurset.cpNoPage + 1))
            for (i = 15 * gvKurset.cpNoPage; i < 15 * (gvKurset.cpNoPage + 1) ; i++) {
                editorLloji = Utils.ktheKontroll('LlojKursi' + i);
                editorVlera = Utils.ktheKontroll('VleraKursi' + i);
                editorData = Utils.ktheKontroll('DataKursit' + i);
                editorNjesia = Utils.ktheKontroll('NjesiaKursit' + i);
                if (colKurset[i] != undefined) {
                    editorLloji.SetValue(colKurset[i].LlojKursi);
                    editorVlera.SetText(colKurset[i].VleraKursi);
                    editorData.SetValue(new Date(colKurset[i].DataKursit));
                    editorNjesia.SetText(colKurset[i].NjesiaKursit);
                }
                else {
                    editorLloji.SetValue(1);
                    editorVlera.SetText(1);
                    var date = new Date();
                    var dateString = date.getDate() + "/" + (date.getMonth() + 1) + "/" + date.getFullYear().toString();
                    editorData.SetText(dateString);
                    editorNjesia.SetText(1);
                }
                Utils.setFormatNumri(Utils.ktheKontroll('Kursi' + i), btneFormatNumri.GetValue());
                Utils.formatoTextBox(Utils.ktheKontroll('Kursi' + i));
            }
        else {
            for (i = 15 * gvKurset.cpNoPage; i < gvKurset.cpNoRows; i++) {
                editorLloji = Utils.ktheKontroll('LlojKursi' + i);
                editorVlera = Utils.ktheKontroll('VleraKursi' + i);
                editorData = Utils.ktheKontroll('DataKursit' + i);
                editorNjesia = Utils.ktheKontroll('NjesiaKursit' + i);
                if (colKurset[i] != undefined) {
                    editorLloji.SetValue(colKurset[i].LlojKursi);
                    editorVlera.SetText(colKurset[i].VleraKursi);
                    editorData.SetValue(new Date(colKurset[i].DataKursit));
                    editorNjesia.SetText(colKurset[i].NjesiaKursit);
                }
                else {
                    editorLloji.SetValue(1);
                    editorVlera.SetText(1);
                    var date = new Date();
                    var dateString = date.getDate() + "/" + (date.getMonth() + 1) + "/" + date.getFullYear().toString();
                    editorData.SetText(dateString);
                    editorNjesia.SetText(1);
                }
                Utils.setFormatNumri(Utils.ktheKontroll('VleraKursi' + i), btneFormatNumri.GetValue());
                Utils.formatoTextBox(Utils.ktheKontroll('VleraKursi' + i));
            }
        }
}

//merr te dhenat e rreshtit te selektuar
function mbushfusha() {
    mbush = false;
    $('#hfShtimModifikim')[0].value = "modifikim";
    indexModifiko = ASPxGridView_Monedhat.GetFocusedRowIndex();
    if (indexModifiko == -1)
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgMonedhaDuhetTeZgjidhni1Monedhe"));
    else
        ASPxGridView_Monedhat.GetRowValues(indexModifiko, 'IdMonedha;KodiMonedha;PershkrimiMonedha;AktivMonedha;IdLlogFitimi;IdLlogHumbje;IdNivelAutorizimi;IdFormatNrKursi', OnGetRowValuesMod);
    Utils.shfaqLoadingGif();;
}

//mbush fushat me te dhenat e rreshtit te selektuar
function OnGetRowValuesMod(values) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;
    ndryshuarFormatNr = false;
    $('#hfId')[0].value = values[0];
    kodi_TextBox.SetText(values[1]);
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "callWsGetAutorizimeSipasLlojitDheIdLidhese"),
        data: JSON.stringify({ idLidhese: values[0], kodLloji: 'Monedha', idPerdorues: hfState.Get('idPerdoruesi')})
    }).done(SucceededCallbackKtheAutorizime);
    pershkrimiTextBox.SetText(values[2]);
    active_CheckBox.SetChecked(values[3]);
    if (values[4] != 0) {
        btneLlogFitimi.SetValue(values[4]);
        var s = btneLlogFitimi.GetText().split(';');
        btneLlogFitimi.SetText(s[0]);
    }
    else  btneLlogFitimi.SetValue(null);
    if (values[5] != 0) {
        btneLlogHumbje.SetValue(values[5]);
        var s = btneLlogHumbje.GetText().split(';');
        btneLlogHumbje.SetText(s[0]);
    } else btneLlogHumbje.SetValue(null);
    if (values[7] == null || values[7] == undefined)
        btneFormatNumri.SetText('');
    else
        btneFormatNumri.SetValue(values[7]);
    formatoFushaDevi();

    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheNrLlogarish"),
        data: JSON.stringify({ ids: ([values[4], values[5]]) })
    }).done(SucceededCallback);

    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheKurseSipasIdMonedhe"),
        data: JSON.stringify({ idMon: values[0] })
    }).done(SucceededCallbackKurse);
    gvHistoriku.PerformCallback(indexModifiko);

    var idGjuha = hfState.Get('idGjuha');
    var idNdermarrje = hfState.Get('idNdermarrje');
    //therret web service per te kontrolluar nese kjo llogari standarte eshte e lidhur apo jo
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "eshteLidhur"),
        data: JSON.stringify({ idkomp: "133", kodkonfi: cmbKonfigurimi.GetText(), iddokumenti: values[0], idNdermarrje: idNdermarrje, idGjuha: idGjuha })
    }).done(function (result) { SucceededCallbackLidhur(result, values[0]) });
    if (kaloTab) {
        monedhat_PageControl.SetActiveTabIndex(1);
        myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, $('#hfShtimModifikim'));
    }
}
function SucceededCallback(result) {
    var btneLlogFitimiResult = result.filter(function (el) { return el.IDLLOGARI == btneLlogFitimi.GetText() })[0];
    btneLlogFitimi.SetText(btneLlogFitimiResult.NRLLOGARI);
    var btneLlogHumbjeResult = result.filter(function (el) { return el.IDLLOGARI == btneLlogHumbje.GetText() })[0];
    btneLlogHumbje.SetText(btneLlogHumbjeResult.NRLLOGARI);

}

var colKurset;
function SucceededCallbackKurse(result) {
    colKurset = result;
    ShfaqTeDhenat();
}

function SucceededCallbackKtheAutorizime(result) {
    if (result.idLidhese == $('#hfId').val())
        cmbAutorizimi.SetText(result.autorizime);
    else cmbAutorizimi.SetValue(null);
}

function endcallback() {
    merrTeDhenaKursi();
}

//therret funksionin qe ben enabled dhe disabled fushat sipas lidhjes
function SucceededCallbackLidhur(result, idObjekti) {
   
    if (idObjekti.toString() !== $('#hfId').val()) {
        Utils.hiqLoadingGif();;
        return;
    }
    var hf = $('#hfKontrollet')[0]; //mban te dhenat mbi kontrollet
    var hfLidhur = $("#hfLidhur")[0];
    hfLidhur.value = result;
    aktivizoFusha(colKontrollet, colAtrTrupi, eval(result.toLowerCase()));
}

//pastron fushat per shtim dhe ben aktive fushat
function pastrofusha() {
    ndryshuarFormatNr = false;
    ndryshuarDate = false;
    ndryshuarKurs = false;
    kodi_TextBox.SetText('');
    pershkrimiTextBox.SetText('');
    active_CheckBox.SetChecked(true);
    btneLlogFitimi.SetValue(null);
    btneLlogHumbje.SetValue(null);
    cmbAutorizimi.SetValue(null);
    btneFormatNumri.SetSelectedIndex(-1);
    btneFormatNumri.SetText('');
    var hf = $('#hfKontrollet')[0];
    aktivizoFusha(colKontrollet, colAtrTrupi, false);
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheKurseSipasIdMonedhe"),
        data: JSON.stringify({ idMon: -1 })
    }).done(SucceededCallbackKurse);
    gvHistoriku.PerformCallback(-1);
}

/* 
Function: OnGridSelectionChanged

perdoret per te ruajtur indexin e selektimit

Parameters:

e-eventi
*/
function OnGridSelectionChanged(e) {
    indexSel = myMenu.JSlevizNeGride.OnGridSelectionChanged(e, indexSel);
}

function callWebserviceKonfigurimi(idKomp, kodKonf) {
    var idGjuha = hfState.Get('idGjuha');
    $("#dvMonedha").show();
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
    ASPxGridView_Monedhat.PerformCallback(idKomp + ";" + kodKonf);
}

function callWebserviceKonfigurimiInit(idKomp, kodKonf) {
    var idGjuha = hfState.Get('idGjuha');
    $("#dvMonedha").show();
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
}

var resultkonf;
var colKontrollet, colAtrTrupi;

function SucceededCallbackKonfig(result) {
    if (result != "" && result != null) {
        colKontrollet = result.colKontrollet;
        colAtrTrupi = result.colAtrTrupi;
        resultkonf = result;
        var hf = $('#hfKontrollet');
        var hfLidhur = $("#hfLidhur");
        var hfMod = $('#hfShtimModifikim');
        var arrTabela = ['tblMonedha', 'tblKursi'];
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "ASPxPageControl1_C");
    }
    // $("#dvMonedha").show();//$("#dvMonedha")[0].style.visibility = 'visible';
}

function aktivizoFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $('#hfShtimModifikim');
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
}

function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);
    callWebserviceKonfigurimi("133", cmbKonfigurimi.GetText());
}

function ndryshoKonfiguriminInit() {
    callWebserviceKonfigurimiInit("133", cmbKonfigurimi.GetText());
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
		        evt.keyCode : evt.charCode;
}

function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('Shto_Monedhe.aspx', 0, hf);
    myMenu.PercaktoMenuSipasTabit(0, hfTeDrejta, $('#hfShtimModifikim'));
}

/*
Function: EndRequestHandler
    
Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe array-t e perdorura.
Shiko funksionet  <ndryshoKonfigurimin>.
*/
function EndRequestHandler(sender, args) {
    var hf = $("#hfStatusi");
    var hfKontrollet = $('#hfKontrollet');
    var hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    var hfId = $('#hfId');  //hidden fieldi qe ruan id  e rreshtit te selektuar
    //        indexModifiko = myFaqeCelje.EndRequestHandler(sender, args, hf, hfKontrollet, hfShtimModifikim, hfId, indexModifiko, monedhat_PageControl, ASPxGridView_Monedhat, "133")
    indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, monedhat_PageControl, ASPxGridView_Monedhat, "133", pastrofusha, hfTeDrejta);
}

var editorValues = new Object();
var arr = new Array();
var identifikuesPerPopupLlogari = "Modifiko_Monedhe";
var kodiMonedha = '';

function Init() {
    changeName();
    for (i = 0; i < 6; i++)
        arr[i] = new Array();
    var options = JSON.parse(hfState.Get("colAutorizime"));
    hfState.Remove("colAutorizime");
    cmbAutorizimi = new MultiSelect({
        container: "tblMonedha",
        valueField: 'KodiAutorizim',
        labelField: 'KodiAutorizim',
        searchField: 'KodiAutorizim',
        options: options,
        meLupe: true,
        onButtonClickLupa: Autorizime_Click,
        multiSelectId: "cmbAutorizimi"

    })
}

var fitim;
var editorLlogFitimi;
var editorLlogHumbje;
var editorAutorizime;

function Llogari_Click() {//thirret popup i llogarive
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("headerPopUpText"), 'LupaLlogaria.aspx', widthLupaLogaria, heightLupaLlogaria);
}

function vendosMonedhen(tab) {//per te vendosur ne labelin e monedhes kodin e monedhes qe po shtohet
    if (tab == 2) {
        if (kodi_TextBox.GetText() != '')
            monedha_label.SetText(kodi_TextBox.GetText());
        else {
            monedhat_PageControl.SetActiveTab(monedhat_PageControl.GetTabByName('Tab0'));
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgMonedhaShenoniMonedhenPastajKurset"));
        }
    }
}

function merrTeDhena() {//merren te dhenat qe ka grida
    for (i = 0; i < 1; i++) {    //per momentin jane fshehur 5 kurset e tjera kur te shtohen 1 duhet 6
        editorvlera = Utils.ktheKontroll('VleraKursi' + i);
        editorData = Utils.ktheKontroll('DataKursit' + i);
        editorNjesia = Utils.ktheKontroll('NjesiaKursit' + i);
        editorPershkrimi = Utils.ktheKontroll('LlojKursi' + i);
        arr[1][i] = i + ':' + editorvlera.GetText();
        arr[2][i] = i + ':' + editorData.GetText();
        arr[3][i] = i + ':' + editorNjesia.GetText();
        arr[4][i] = i + ":" + editorPershkrimi.GetText();
        arr[0][i] = i + ":" + (parseInt(i) + parseInt(1));
    }
}

function TextChangedVleraKursit(editor, field, key) {
    var editorData = Utils.ktheKontroll('DataKursit' + key);
    var date = new Date();
    dateString = date.getDate() + "/" + (date.getMonth() + 1) + "/" + date.getFullYear().toString();//.substr(2, 2)
    if (!ndryshuarDate)
        editorData.SetText(dateString);
    ndryshuarKurs = true;
    arr[2][key] = key.toString() + ":" + dateString;

    if (editor.GetText() == '.')
        editor.SetText("0.");
    //if (!(parseFloat(editor.GetText()) == 1) && (hfState.Get('MonedheNderm') == kodi_TextBox.GetText())) {
    //    myMesazh.ShtoMesazhGabimi(hfState.Get("msgKursiNje"));
    //    if (btneFormatNumri.GetText() === "")
    //        editor.SetText(1.00);
    //    else {
    //        var formatNrKursi = btneFormatNumri.GetText();
    //        if (formatNrKursi.indexOf('.') !== -1)
    //            editor.SetText('1.' + formatNrKursi.substring(formatNrKursi.indexOf('.') + 1));
    //        else editor.SetText(1);
    //    }
    //}
    if (!(parseFloat(editor.GetValue()) == 1)  && (hfState.Get('MonedheNderm') == kodi_TextBox.GetText())) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKursiNje"));
        if (btneFormatNumri.GetText() === "")
            editor.SetText(1.00);
        else {
            var formatNrKursi = btneFormatNumri.GetText();
            if (formatNrKursi.indexOf('.') !== -1)
                editor.SetText('1.' + formatNrKursi.substring(formatNrKursi.indexOf('.') + 1));
            else editor.SetText(1);
        }
    }
    if (parseInt(editor.GetValue()) + '' == 'NaN') {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKursiDuhetNumer"));
        if (btneFormatNumri.GetText() === "")
            editor.SetText(1.00);
        else {
            var formatNrKursi = btneFormatNumri.GetText();
            if (formatNrKursi.indexOf('.') !== -1)
                editor.SetText('1.' + formatNrKursi.substring(formatNrKursi.indexOf('.') + 1));
            else editor.SetText(1);
        }
    }
    if (parseFloat(editor.GetValue()) <= 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKursiNumerPozitiv"));
        if (btneFormatNumri.GetText() === "")
            editor.SetText(1.00);
        else {
            var formatNrKursi = btneFormatNumri.GetText();
            if (formatNrKursi.indexOf('.') !== -1)
                editor.SetText('1.' + formatNrKursi.substring(formatNrKursi.indexOf('.') + 1));
            else editor.SetText(1);
        }
    }
    arr[1][key] = key.toString() + ":" + editor.GetText();
}

function LostFocusVleraKursit(editor, field, key) {
    if (parseFloat(editor.GetText()) <= 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKursiNukMundTeJeteZero"));
        editor.SetText(1);
    }
}

function TextChangedLlojKursit(editor, field, key) {
    arr[4][key] = key.toString() + ":" + editor.GetText();
}

var ndryshuarDate = false;
function TextChangedDataKursit(editor, field, key) {
    ndryshuarDate = true;
    arr[2][key] = key.toString() + ":" + editor.GetText();
}

function formoStringPerCallback() {
    var str = "";
    for (i = 0; i < arr.length; i++) {
        str = str + arr[i] + ';';
    }
    return str;
}

function ruajKurse() {
    var hf = $("#hfKurset")[0];
    hf.value = formoStringPerCallback();
}

function Autorizime_Click() {
    KPF = 0;
    var queryStr = "0&autorizimet=" + cmbAutorizimi.GetText();

    myButtonClickLupa.Autorizime_Click(hfState.Get("headerPopUpZgjidhAutorizimet"), queryStr, widthLupaAutorizime, heightLupaAutorizime);
}

function valido(s, e) {
    myFaqeCelje.valido(s, e, monedhat_PageControl, hfTeDrejta, $('#hfShtimModifikim'));
}

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}

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

function formatNrButtonClick() {
    btneFormatNumri.ShowDropDown();
}

function ActiveTabChanged(s, e) {
    vendosMonedhen(e.tab.index);
    indexModifiko = ASPxGridView_Monedhat.GetFocusedRowIndex();
    if (mbush) {
        if (indexModifiko != -1) {
            OnGridDoubleClick(indexModifiko);
        }
        else {
            mbush = false;
            $('#hfShtimModifikim')[0].value = 'shtim';
            $('#hfId')[0].value = 0;
            pastrofusha();
        }
    }
    kaloTab = false;
    myMenu.PercaktoMenuSipasTabit(e.tab.index, hfTeDrejta, $('#hfShtimModifikim'));
    if (monedhat_PageControl.GetActiveTabIndex() == 2 && ndryshuarFormatNr)
        gvKurset.PerformCallback(indexModifiko);
}

function textChangedFormatNr() {
    ndryshuarFormatNr = true;
}