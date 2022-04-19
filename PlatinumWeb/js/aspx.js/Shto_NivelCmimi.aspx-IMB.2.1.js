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
var widthLupaPrindi = 600, heightLupaPrindi = 600;
//per filtrat
var widthLupaAutorizime = 600;
var heightLupaAutorizime = 600;
var identifikuesPerPopupAutorizime = 'ShtoNivelCmimi';
var identifikuesPerPopupNivelCmimeNga = 'Shto Nivel'
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

function checkText(s, e) {
    myMenu.checkText(s, e);
} var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvNivelCmimi, "410", cmbKonfigurimi.GetText());
}
function textChanged(s, e) {
    myMenu.textChanged(s, e);
}
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
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvNivelCmimi, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvNivelCmimi, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvNivelCmimi, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvNivelCmimi, indexSel);
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
Utils.vendosVlereNeHiddenField("cmbAutorizimiHf", cmbAutorizimi.GetText());
    myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, false, undefined, indexModifiko, pastrofusha, SucceededCallbackKonfig, resultkonf, colKontrollet, aktivizoFusha, colAtrTrupi);

}

//merr te dhenat e rreshtit te selektuar
function mbushfusha() {
    mbush = false;
    $('#hfShtimModifikim')[0].value = "modifikim";
    indexModifiko = gvNivelCmimi.GetFocusedRowIndex();
    if (indexModifiko == -1)
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhni1NivelCmimi"));
    else
        gvNivelCmimi.GetRowValues(indexModifiko, 'IdNivelCmimi;KodNivelCmimi;PershkrimNivelCmimi;IdPrindi;LlojiNivelCmimi;IdMonedha;BrutoNetoNivelCmimi;PrioritetiNivelCmimi;NjesiTeVarura;TeVaruraNgaMonedha;NivelCmimiBaze;KodPrindi;PershkrimPrindi;Detajim;IdCmimRetail;PershkrimRetail', OnGetRowValuesMod);
       // gvNivelCmimi.GetRowValues(indexModifiko, 'IdNivelCmimi;KodNivelCmimi;PershkrimNivelCmimi;IdPrindi;LlojiNivelCmimi;IdMonedha;BrutoNetoNivelCmimi;PrioritetiNivelCmimi;NjesiTeVarura;TeVaruraNgaMonedha;NivelCmimiBaze;KodPrindi;PershkrimPrindi', OnGetRowValuesMod);
    Utils.shfaqLoadingGif();;
}

//mbush fushat me te dhenat e rreshtit te selektuar
function OnGetRowValuesMod(values) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;
    $('#hfId')[0].value = values[0];
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "callWsGetAutorizimeSipasLlojitDheIdLidhese"),
        data: JSON.stringify({ idLidhese: values[0], kodLloji: 'NivelCmimi', idPerdorues: hfState.Get('idPerdoruesi')})
    }).done(SucceededCallbackKtheAutorizime);
    if ($('#hfShtimModifikim').val() == "modifikim")
        txtKodi.SetEnabled(false);
    txtKodi.SetText(values[1]);
    txtEmertimi.SetText(values[2]);
    if (values[3] != 0 || values[3] !=null) {
        Utils.SelectComboItem(btneEmertimPrindi, values[3], values[11], values[12]);
        btneEmertimPrindi.SetValue(values[3]);
        var s = btneEmertimPrindi.GetText().split(',');
        btneEmertimPrindi.SetText(s[1]);
    }
    else btneEmertimPrindi.SetValue(null);
    cmbLloji.SetValue(values[4]);
    cmbMonedha.SetValue(values[5]);
    cmbBrutoNeto.SetValue(values[6]);
    cmbPrioriteti.SetValue(values[7]);
    cbNjesiTeVarura.SetChecked(values[8]);
    cbTeVaruraNgaMonedha.SetChecked(values[9]);
    cbNivelCmimBaze.SetChecked(values[10]);
    cmbDetajim.SetValue(values[13]);
    if (values[14] != 0) {
        btneCmimRetail.SetValue(values[15]);
    }
    else
        btneCmimRetail.SetValue(null);

    enable();
    var idGjuha = hfState.Get('idGjuha');
    var idNdermarrje = hfState.Get('idNdermarrje');
    //therret web service per te kontrolluar nese kjo llogari standarte eshte e lidhur apo jo
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "eshteLidhur"),
        data: JSON.stringify({ idkomp: "129", kodkonfi: cmbKonfigurimi.GetText(), iddokumenti: values[0], idNdermarrje: idNdermarrje, idGjuha: idGjuha })
    }).done(function (result) { SucceededCallbackLidhur(result, values[0]) });
    if (kaloTab) {
        PageControl.SetActiveTabIndex(1);
        myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, $('#hfShtimModifikim'));
    }
}

function SucceededCallbackKtheAutorizime(result) {
    if (result.idLidhese == $('#hfId').val())
        cmbAutorizimi.SetText(result.autorizime);
    else cmbAutorizimi.SetValue(null);
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
    //            aktivizoFusha(hf.value);
    aktivizoFusha(colKontrollet, colAtrTrupi, eval(result.toLowerCase()));
}
//pastron fushat per shtim dhe ben aktive fushat
function pastrofusha() {
    txtKodi.SetText('');
    txtEmertimi.SetText('');
    btneEmertimPrindi.SetValue(null);
    cmbLloji.SetSelectedIndex(0);
    cmbMonedha.SetValue(null);
    cmbBrutoNeto.SetText('');
    cmbPrioriteti.SetText(1);
    cbNjesiTeVarura.SetChecked(false);
    cbTeVaruraNgaMonedha.SetChecked(false);
    cbNivelCmimBaze.SetChecked(false);
    cmbAutorizimi.SetValue(null);
    cmbDetajim.SetSelectedIndex(0);
    btneCmimRetail.SetValue(null);
    enable();
    var hf = $('#hfKontrollet')[0];
    aktivizoFusha(colKontrollet, colAtrTrupi, false);
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
    gvNivelCmimi.PerformCallback(idKomp + ";" + kodKonf);
}

function callWebserviceKonfigurimiInit(idKomp, kodKonf) {
    var idGjuha = hfState.Get('idGjuha');
    $("#dvNiveli").show();
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
}

var resultkonf;
var colKontrollet, colAtrTrupi;
//        var colAlterKusht;
//        var colKushte;
function SucceededCallbackKonfig(result) {
    if (result != "" && result != null) {
        colKontrollet = result.colKontrollet;
        colAtrTrupi = result.colAtrTrupi;
        //                var colGrida = result[2];
        //                colKushte = result[3];
        //                colAlterKusht = result[4];
        //                var kodniveli = result[5];
        //                var konfLlojRreshti = result[6];
        resultkonf = result;
        LupaKontrollet(colKontrollet, colAtrTrupi);
        var hf = $('#hfKontrollet');
        var hfLidhur = $("#hfLidhur");
        var hfMod = $('#hfShtimModifikim');
        var arrTabela = ['tblNivelCmimi'];
        //myFaqeCelje.SucceededCallbackKonfig(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "#ASPxPageControl1_", "ASPxPageControl1_C");
        //myFaqeCelje.SucceededCallbackKonfigSlim(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "#ASPxPageControl1_", "ASPxPageControl1_C");
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "ASPxPageControl1_C");
    }

 //   $("#dvNiveli").show();//$("#dvNiveli")[0].style.visibility = 'visible';
}

function LupaKontrollet(kontrollet, colAtrTrupi) {
    var hf1 = $("#hfLupaPrindi");
    var hf2 = $("#hfLupaAutorizimi");
    for (var i = 0; i < kontrollet.length; i++) {
        //id e konfigurimit te lupes vendoset ne hidden field
        //vlera e saj do t'i kalohet si query string hapjes se popUp-it
        if (kontrollet[i].KodKontrolli == "btneEmertimPrindi")
            hf1.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
        if (kontrollet[i].KodKontrolli == "cmbAutorizimi") {
            hf2.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
    }
}

function aktivizoFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $('#hfShtimModifikim');
    //            myFaqeCelje.aktivizoFusha(vlerat, hfMod, hfLidhur, '#ASPxPageControl1_');
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
}
function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);
    callWebserviceKonfigurimi("410", cmbKonfigurimi.GetText());
}
function ndryshoKonfiguriminInit() {
    callWebserviceKonfigurimiInit("410", cmbKonfigurimi.GetText());
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
		        evt.keyCode : evt.charCode;
}

function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('Shto_NivelCmimi.aspx', 0, hf);
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
    //            indexModifiko = myFaqeCelje.EndRequestHandler(sender, args, hf, hfKontrollet, hfShtimModifikim, hfId, indexModifiko, PageControl, gvNivelCmimi, "410")
   btneEmertimPrindi.PerformCallback();
   indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, gvNivelCmimi, "410", pastrofusha, hfTeDrejta);
}


var identifikuesPerPopupNiveli = "Modifiko Nivel";

var KPF;
function NivelCmimi_Click() {
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhNivelinECmimitPrind"), 'LupaNivelCmimiPrind.aspx', widthLupaPrindi, heightLupaPrindi);
}

//pastron fushat
function Init() {
    var options = JSON.parse(hfState.Get("colAutorizime"));
    hfState.Remove("colAutorizime");
    cmbAutorizimi = new MultiSelect({
        container: "tblNivelCmimi",
        valueField: 'KodiAutorizim',
        labelField: 'KodiAutorizim',
        searchField: 'KodiAutorizim',
        options: options,
        meLupe: true,
        onButtonClickLupa: Autorizime_Click,
        multiSelectId: "cmbAutorizimi"

    })
    changeName();
}
function valido(s, e) {
    myFaqeCelje.valido(s, e, PageControl, hfTeDrejta, $('#hfShtimModifikim'));
}

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}
function enable() {
    if (btneEmertimPrindi.GetText() == '') {
        cmbLloji.SetEnabled(true);
        cmbBrutoNeto.SetEnabled(true);
    }
    else { cmbLloji.SetEnabled(false); cmbBrutoNeto.SetEnabled(false); }
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

function Autorizime_Click() {
    KPF = 0;
    var hf = $("#hfLupaAutorizimi")[0];
    var queryStr = hf.value + '&autorizimet=' + cmbAutorizimi.GetText();
    myButtonClickLupa.Autorizime_Click(hfState.Get("MsgBlerjeShitjeAutorizime"), queryStr, widthLupaAutorizime, heightLupaAutorizime);
}

function Selected_IndexChanged() {
    var s = btneEmertimPrindi.GetText().split(','); 
    btneEmertimPrindi.SetText(s[1]); 
    cmbMonedha.SetText(s[3]); 
    cmbLloji.SetText(s[2]); 
    cmbBrutoNeto.SetText(s[4]); 
    var listefushash = $('#hfPrioriteteMax')[0].value.split(','); 
    for (i = 0; i < listefushash.length; i++) { var liste = listefushash[i].split(':'); 
        if (liste[0] == s[0]) cmbPrioriteti.SetValue(liste[1]); } 
    enable();
}

function Active_TabChanged(s, e) {
    indexModifiko = gvNivelCmimi.GetFocusedRowIndex();    
    if(mbush)
    { 
        if(indexModifiko !=-1)
        {
            OnGridDoubleClick(indexModifiko); 
        }
        else 
        {   
            mbush = false;
            $('#hfShtimModifikim')[0].value = 'shtim'; 
            $('#hfId')[0].value = 0; pastrofusha();
        }
    }
    kaloTab=false;
    myMenu.PercaktoMenuSipasTabit(e.tab.index, hfTeDrejta, $('#hfShtimModifikim'));
                  
}
function MerrNivelCmimiNga_Click() {
    var widthLupaNivelCmimi = 900, heightLupaNivelCmimi = 600;
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhniNivelinECmimit"), 'LupaNivelCmimi.aspx', widthLupaNivelCmimi, heightLupaNivelCmimi);
}
