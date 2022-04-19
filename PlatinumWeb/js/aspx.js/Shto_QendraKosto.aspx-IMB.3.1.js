; //variabla per te kaluar nga nje reshti i grides tek tjetri
var indexSel = 0;
//indexi i zgjedhur per modifikim
var indexModifiko;
//perdoret per te treguar nese duhet te mbushen tabet apo jo kjo perdoret qe tabet te mbushen vetem heren e pare dhe jo pas cdo here qe  kalohet nga nje tab tek tjetri
var mbush = true;
//perdoret per te daluar rastet kur bejme double click apo thjesht click ne menyre qe ne double click te kaloje tek tabi i dyte
var kaloTab = false;
//perdoret per griden e llogarive ne menyre qe te lodohet vetem ne momentin kur shkon ne tabin e saj
var lista = true;


//metodat per te hapur faqen e modifikimit me double click
function OnGridDoubleClick(index) {
    if (Utils.getUrlVar('lupe') == "true") {
        ZgjidhRreshtaNgaLupa();
    }
    else {
        trlQendra.SelectNode(trlQendra.GetFocusedNodeKey());
        indexModifiko = index;
        lista = true;
        mbushfusha();
    }
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
    changeName();
});

/*
Function: Poshte_click

perdoret per te selektuar rreshtin me poshte

Parameters:

e-eventi
*/
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, trlQendra, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, trlQendra, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, trlQendra, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, trlQendra, indexSel);
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
    myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, true, undefined, indexModifiko, pastrofusha, SucceededCallbackKonfig, resultkonf, colKontrollet, aktivizoFusha, colAtrTrupi);
    if (e.item.name == "OK") {
        e.processOnServer = false;
        ZgjidhRreshtaNgaLupa();
    }
    else if (e.item.name == 'Anullo') {
        e.processOnServer = false;
        window.parent.popupUniversal.Hide();
    }
}

function ZgjidhRreshtaNgaLupa() {
    var vjenNga = Utils.getUrlVar('vjenNga');
    var focusedNodeKey = trlQendra.GetFocusedNodeKey();
    if (Utils.getUrlVar('llojLupe') == "prind") {
        trlQendra.GetNodeValues(focusedNodeKey, 'Id;Kodi;Pershkrimi;IdMonedha;IdPrindi;Prindi;Monedha', OnGridSelectionComplete);
        return;
    }
    switch (vjenNga) {
        case 'raporti':
            if (trlQendra.GetVisibleSelectedNodeKeys().length == 0 && focusedNodeKey != -1 && focusedNodeKey != null)
                trlQendra.SelectNode(focusedNodeKey);
            if (focusedNodeKey == -1 || focusedNodeKey == null)
                return;
            trlQendra.GetSelectedNodeValues('Id;Kodi;Pershkrimi;IdMonedha;IdPrindi;Prindi', OnGridSelectionCompleteRaporti);
            break;
        default:
            if (vjenNga !== "PunonjesQK2") {
                if (trlQendra.GetVisibleSelectedNodeKeys().length == 0 && focusedNodeKey != -1 && focusedNodeKey != null)
                    trlQendra.SelectNode(focusedNodeKey);
                if (focusedNodeKey == -1 || focusedNodeKey == null)
                    return;
            }
            trlQendra.GetNodeValues(focusedNodeKey, 'Id;Kodi;Pershkrimi;IdMonedha;IdPrindi;Prindi', OnGridSelectionComplete);
            break;
    }
}

function OnGridSelectionCompleteRaporti(values) {
    switch (Utils.getUrlVar('vjenNga')) {
        case 'raporti':
            var s = new String();
            s = s + values[0];
            var vl = s.split(",");
            var kodi = vl[1];

            for (i = 1; i < values.length; i++)
                kodi = kodi + "," + values[i][1];
            window.parent.btneQenderKosto1.SetText(kodi);
            window.parent.btneQenderKosto1.SetFocus();
            break;
    }
    window.parent.popupUniversal.Hide();
}

function OnGridSelectionComplete(values) {
    if (values.length == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgQKZgjidhni1QK"));
        return;
    }

    switch (Utils.getUrlVar('vjenNga')) {
        case 'raporti': //Utils.getUrlVar('llojLupe') == "qk"
            var s = new String();
            s = s + values[0];
            var vl = s.split(",");
            var kodi = vl[1];

            for (i = 1; i < values.length; i++)
                kodi = kodi + "," + values[i][1];
            window.parent.btneQenderKosto1.SetText(kodi);
            window.parent.btneQenderKosto1.SetFocus();
            break;
        case "Shto_Skema":
            if (window.parent.editorKodi.GetText() != values[1]) {
                if (values[1] != "")
                    for (var i = 0; i < window.parent.gvTrupi.cpNoRows; i++) {
                        if (values[1] == window.parent.window['Kodi' + i].GetText()) {
                            window.parent.myMesazh.ShtoMesazhGabimi('Kjo qender eshte perdorur njehere ne kete skeme!')
                            values[1] = '';
                            values[2] = '';
                            break;
                        }
                    }

                window.parent.editorKodi.SetText(values[1]);
                window.parent.editorPershkrimi.SetText(values[3]);
            }
            window.parent.editorKodi.SetFocus();
            break;
        case 'Shto_Llogari':
            window.parent.qendraKostos_TextBox.SetText(values[1]);
            window.parent.qendraKostos_TextBox.SetFocus();
            break;
        case 'LupaLlogariShpejte':
            window.parent.qendraKostos_TextBox.SetText(values[1]);
            window.parent.qendraKostos_TextBox.SetFocus();
            break;
        case 'RegjistrimQendraKostoDXDATAGRID':
        case 'RegjistrimQendraKosto':
            if (Utils.getUrlVar('llojLupe') && Utils.getUrlVar('llojLupe') == "plote") {
                $.ajax({
                    url: Utils.getServerApiUrl("Rregjistrime", "eshtePrindQenderKosto"),
                    data: JSON.stringify({ idNdermarrje: hfState.Get('idNdermarrje'), Kodi: values[1] }),
                    pritPergjigje: true
                }).done(function (result) { SucceededCallbackeshtePrindQK(result, Utils.getUrlVar('vjenNga'), values[0]) });
            }
            else if (Utils.getUrlVar('vjenNga') !== 'RegjistrimQendraKostoDXDATAGRID'){
                window.parent.cmbQendraKosto.SetText(values[1]);
                window.parent.cmbQendraKosto.SetFocus();
                window.parent.popupUniversal.Hide();
            }
            break;
        case 'PunonjesQK2':
            window.parent.cmbQK2.SetSelectedIndex(window.parent.cmbQK2.AddItem(values[1], values[0]));;
            window.parent.cmbQK1.SetSelectedIndex(window.parent.cmbQK1.AddItem(values[5], values[4]));;
            window.parent.cmbQK2.SetFocus();
            break;
        case 'Raporti2':
            window.parent.btnQenderKosto2.SetText(values[1], values[0]);
            window.parent.btnQenderKosto1.SetText(values[5], values[4]);
            window.parent.btnQenderKosto2.SetFocus();
            break;
        case 'Raporti1':
            window.parent.btnQenderKosto2.SetText(values[1], values[0]);
            window.parent.btnQenderKosto2.SetFocus();
            break;
        case 'RegjistrimQendraKostoGrida':
            window.parent.editorKodi.SetText(values[1]);
            window.parent.editorEmertimi.SetText(values[2]);
            window.parent.editorKodi.SetFocus();
            break;
        case "Struktura":
            window.parent.Qendra.SetText(values[1]);
            window.parent.Qendra.SetValue(values[0]);
            window.parent.$('#hfQendra').val(values[1]);
            window.parent.Qendra.SetFocus();
            break;
        case 'PunonjesQK2Import':
            window.parent.editorGlobal.SetSelectedIndex(window.parent.editorGlobal.AddItem(values[1], values[0]));
            window.parent.editorGlobal.SetFocus();
            break;
        case "Import":
            window.parent.editorGlobal.SetText(values[1]);
            window.parent.editorGlobal.SetFocus(true);
            break;
        case 'PunonjesQK1':
            window.parent.cmbQK1.SetSelectedIndex(window.parent.cmbQK1.AddItem(values[1], values[0]));
            window.parent.cmbQK2.SetText('');
            window.parent.cmbQK1.SetFocus();
            break;
        case 'PunonjesQK1Import':
            window.parent.editorGlobal.SetSelectedIndex(window.parent.editorGlobal.AddItem(values[1], values[0]));
            window.parent.editorGlobal.SetFocus();
            window.parent.grupi = values[0];
            break;
        case "Raporti": //Utils.getUrlVar('llojLupe') == "prind"
            window.parent.btnQenderKosto1.SetText(values[1]);
            window.parent.btnQenderKosto2.SetText('');
            window.parent.btnQenderKosto1.SetFocus();
            break;
        default:
            if (Utils.getUrlVar('llojLupe') == "prind") {
                if (window.parent.cmbPrindi.GetText() != values[1]) {
                    window.parent.cmbPrindi.SetText(values[1]);
                    window.parent.cmbMonedha.SetText(values[6]);
                    window.parent.cmbMonedha.SetEnabled(false);
                }
                window.parent.cmbPrindi.SetFocus();
            }
            break;
    }
    if (!(Utils.getUrlVar('llojLupe') && Utils.getUrlVar('llojLupe') == "plote"))
        window.parent.popupUniversal.Hide();
}

function SucceededCallbackeshtePrindQK(result, vjenNga, idQendra) {
    if (result.eshtePrindQk) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgJoVeprimeMeQendrenPrind"));
    }
    else if (vjenNga === 'RegjistrimQendraKostoDXDATAGRID') {
        window.parent.regjQK.VendosQKdheLlogariDheObjektiveNeGrideNgaLupat(idQendra, 0, 0);
        window.parent.popupUniversal.Hide();
    }
    else {
        window.parent.cmbQendraKosto.SetText(result.Kodi);
        window.parent.cmbQendraKosto.SetFocus();
        window.parent.popupUniversal.Hide();
    }
}

//merr te dhenat e rreshtit te selektuar
function mbushfusha() {
    mbush = false;
    $('#hfShtimModifikim').val("modifikim");
    indexModifiko = trlQendra.GetFocusedNodeKey();
    if (indexModifiko == '' || indexModifiko == 0)
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgQKDuhetTeZgjdhni1Qender"));
    else {
        trlQendra.GetNodeValues(indexModifiko, 'Id;Kodi;Pershkrimi;IdMonedha;Aktiv;IdPrindi;Prindi;Niveli', OnGetRowValuesMod);
        Utils.shfaqLoadingGif();
    }
}

//mbush fushat me te dhenat e rreshtit te selektuar
function OnGetRowValuesMod(values) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;
    $('#hfId').val(values[0]);
    txtKodi.SetText(values[1]);
    if ($('#hfShtimModifikim').val() == "modifikim") {
        txtKodi.SetEnabled(false);
    }
    txtEmertimi.SetText(values[2]);
    cmbMonedha.SetValue(values[3]);
    cbAktiv.SetChecked(values[4]);
    if (values[6] !== "" && values[6] !== null)
        cmbPrindi.SetSelectedIndex(cmbPrindi.AddItem(values[6], values[5]));
    else cmbPrindi.SetText('');
    txtNiveli.SetText(values[7]);
    //therret web service per te kontrolluar nese kjo llogari standarte eshte e lidhur apo jo
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "eshteLidhur"),
        data: JSON.stringify({ idkomp: "903", kodkonfi: cmbKonfigurimi.GetText(), iddokumenti: values[0], idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: hfState.Get('idGjuha') })
    }).done(function (result) { SucceededCallbackLidhur(result, values[0]) });
    $.ajax({
        url: Utils.getServerApiUrl("Celje", "merrBuxhete"),
        data: JSON.stringify({ idqk: $('#hfId').val(), idNderviti: hfState.Get("idnderviti"), idllojbuxheti: 23, idviti: 0, eshteprojekt: false }),
        pritPergjigje: true
    }).done(function (result) {
        //some instrutions
        SucceededCallbackBuxhet(result);
    }).fail(function (err) {
        console.log(err);
    });

    if (kaloTab) {
        PageControl.SetActiveTabIndex(1);
        myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, $('#hfShtimModifikim'), Utils.getUrlVar('lupe') == "true");
    }
}

function SucceededCallbackBuxhet(result)
{
    cmbNdryshimi.ClearItems();
    for (i = 0; i < result.length; i++)
        cmbNdryshimi.AddItem(result[i], result[i]); //AddItem(teksti, vlera);
    cmbNdryshimi.SetSelectedIndex(0);
    if (cmbNdryshimi.GetText() !== "")
        dteDateAkt.SetDate(new Date(cmbNdryshimi.GetText().split('/')[1] + '/' + cmbNdryshimi.GetText().split('/')[0] + '/' + cmbNdryshimi.GetText().split('/')[2]));
    gvBuxheti.PerformCallback();//$('#hfId').val(), dteDateAkt.GetDate() PATI: Po i heq nga parametrat sepse nuk perdoren dhe kur jane null, japin error.
}
//therret funksionin qe ben enabled dhe disabled fushat sipas lidhjes
function SucceededCallbackLidhur(result, idObjekti) {
    
    if (idObjekti.toString() !== $('#hfId').val()) {
        Utils.hiqLoadingGif();;
        return;
    }
    var hf = $('#hfKontrollet')[0]; //mban te dhenat mbi kontrollet
    var hfLidhur = $("#hfLidhur");
    hfLidhur.val(result);
    //        aktivizoFusha(hf.value);
    aktivizoFusha(colKontrollet, colAtrTrupi, eval(result.toLowerCase()));
}
var arr = new Array();
var counter = 0;
var arr2 = new Array();
var counter2 = 0;
//pastron fushat per shtim dhe ben aktive fushat
function pastrofusha() {
    txtKodi.SetText('');
    txtEmertimi.SetText('');
    cmbMonedha.SetSelectedIndex(0);
    cbAktiv.SetChecked(true);
    cmbPrindi.SetText('');
    aktivizoFusha(colKontrollet, colAtrTrupi, false);
    gvBuxheti.PerformCallback(-1);
    arr = new Array();
    counter = 0;
    arr2 = new Array();
    counter2 = 0;
    arrVlerat = new Array();
    countvlerat = 0;
    cmbNdryshimi.ClearItems();
    dteDateAkt.SetDate(new Date());
    txtNiveli.SetText('1');
}
function ndryshoDate()
{
    dteDateAkt.SetDate(new Date(cmbNdryshimi.GetText().split('/')[1] + '/' + cmbNdryshimi.GetText().split('/')[0] + '/' + cmbNdryshimi.GetText().split('/')[2]));
    gvBuxheti.PerformCallback();
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
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
    trlQendra.PerformCallback(idKomp + ";" + kodKonf);
}

function callWebserviceKonfigurimiInit(idKomp, kodKonf) {
    $("#dvBurimi").show();
    var idGjuha = hfState.Get('idGjuha');
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
}
var resultkonf;
var colKontrollet, colAtrTrupi;

function SucceededCallbackKonfig(result) {
    if (result !== "" && result !== null) {
        colKontrollet = result.colKontrollet;
        colAtrTrupi = result.colAtrTrupi;

        resultkonf = result;
        LupaKontrollet(colKontrollet, colAtrTrupi);
        var hf = $('#hfKontrollet');
        var hfLidhur = $("#hfLidhur");
        var hfMod = $('#hfShtimModifikim');
        var arrTabela = ['tblQendraKosto','tblbuxheti'];
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "ASPxPageControl1_C");
    }

   // $("#dvBurimi").show();//$("#dvBurimi")[0].style.visibility = 'visible';
}

function LupaKontrollet(kontrollet, colAtrTrupi) {
    
}

function aktivizoFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $('#hfShtimModifikim');
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
}
function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);

    callWebserviceKonfigurimi("903", cmbKonfigurimi.GetText());
}
function ndryshoKonfiguriminInit() {

    callWebserviceKonfigurimiInit("903", cmbKonfigurimi.GetText());
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
		        evt.keyCode : evt.charCode;
}

function changeName() {
   // trlQendra.PerformCallback();
    var hf = $("#hfKonffillestar")[0];
    if (Utils.getUrlVar('id') == undefined)
        myFaqeCelje.changeName('Shto_QendraKosto.aspx', 0, hf);
    else
        myFaqeCelje.changeName('Shto_QendraKosto.aspx',Utils.getUrlVar('id'), hf);
    myMenu.PercaktoMenuSipasTabit(0, hfTeDrejta, $('#hfShtimModifikim'), Utils.getUrlVar('lupe') == "true");

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
    indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, trlQendra, "903", pastrofusha, hfTeDrejta, undefined, undefined, false);
}

function valido(s, e) {
    myFaqeCelje.valido(s, e, PageControl, hfTeDrejta, $('#hfShtimModifikim'));
}

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}
function BeginCallback(s, e) {

}
function enter() {
    if (window.event.keyCode == 13) {
        event.returnValue = false;
        event.cancel = true;
    }
}

var KPF;
function Prindi_Click() {
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgQKZgjidhniPrindin"), 'LupaQendraKostoPrind.aspx', 600, 560);
}

function cmbPrindiTextChanged(s, e) {
    var selectedItem = s.GetSelectedItem();
    if (selectedItem !== null && s.GetText() != '') {
        callWebserviceNdryshoiPrindi(selectedItem.value);
    } else {
        txtNiveli.SetText('1');
        cmbMonedha.SetEnabled(true);
    }
    
}

function callWebserviceNdryshoiPrindi(idPrindi) {
    $("#dvBurimi").show();
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Celje", "merrQkPrind"),
        data: JSON.stringify({ idqk: idPrindi })
    }).done(SuccededCallbackNdryshimPrindi);
}


function SuccededCallbackNdryshimPrindi(result) {
    if (result !== "" && result !== null) {
        cmbMonedha.SetValue(result.prindi.IdMonedha);
        txtNiveli.SetText(result.prindi.Niveli + 1);
        cmbMonedha.SetEnabled(false);
    }

}


//buxhetet
function ruajBuxhet() {
    var hidField = $("#hfBuxheti1")[0]; //shton vleren tek hidden field e cila perdoret me vone nga kodi
    var hidField2 = $("#hfBuxheti2")[0]; //shton vleren tek hidden field e cila perdoret me vone nga kodi
    var hidFieldShenime = $("#hfBuxhetiShenime")[0];
    myBuxhet.ruajBuxhet(hidField, hidField2, hidFieldShenime);
}
function ShtoBuxhet1(editor, key) {
    var hidField = $("#hfBuxheti1")[0]; //shton vleren tek hidden field e cila perdoret me vone nga kodi
    counter = myBuxhet.ShtoBuxhet1(editor, null, null, key, hidField, counter);
}
function ShtoBuxhet2(editor, key) {
    var hidField2 = $("#hfBuxheti2")[0]; //shton vleren tek hidden field e cila perdoret me vone nga kodi
    counter2 = myBuxhet.ShtoBuxhet2(editor, null, null, key, hidField2, counter2);
}
//metoda per te marre vlerat e ndryshuara te totalit dhe ndan vleren e tij ne te gjithe buxhetet e muajve
function ShtoTotal1(editor) {
    var hidField = $("#hfBuxheti1")[0]; //shton vleren tek hidden field e cila perdoret me vone nga kodi
    counter = myBuxhet.ShtoTotal1(editor, null, null, hidField, counter);
}

//metoda per te marre vlerat e ndryshuara te totalit dhe ndan vleren e tij ne te gjithe buxhetet e muajve
function ShtoTotal2(editor) {
    var hidField2 = $("#hfBuxheti2")[0]; //shton vleren tek hidden field e cila perdoret me vone nga kodi
    counter2 = myBuxhet.ShtoTotal2(editor, null, null, hidField2, counter2);
}

function Active_TabChanged(s, e) {
    indexModifiko = trlQendra.GetFocusedNodeKey();
    if (mbush) {
        if (indexModifiko != '' && indexModifiko !== 0) {
            OnGridDoubleClick(indexModifiko);
        }
        else {
            mbush = false;
            $('#hfShtimModifikim').val('shtim');
            $('#hfId').val(0); pastrofusha();
        }
    }
    kaloTab = false;
    myMenu.PercaktoMenuSipasTabit(e.tab.index, hfTeDrejta, $('#hfShtimModifikim'), Utils.getUrlVar('lupe') == "true");
}