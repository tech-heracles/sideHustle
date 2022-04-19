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

$(document).ready(function () {
    changeName();
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
});

function checkText(s, e) {
    myMenu.checkText(s, e);
} var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvDegeAdministrative, "163", cmbKonfigurimi.GetText());
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
function Qendra_Click() {
    if (cmbLloji.GetValue() == 1)
        myButtonClickLupa.LupaUniversal_Click(hfState.Get("headerPopUpZgjidhQendrenKostos"), 'Shto_QendraKosto.aspx?lupe=true&llojLupe=qk&vjenNga=Shto_Llogari', 900, 600);
    else myButtonClickLupa.LupaUniversal_Click(hfState.Get('headerPopUpZgjidhSkemenKostos'), 'LupaSkemaKosto.aspx?vjenNga=Shto_Llogari', 600, 600);
}
/*
Function: Poshte_click

perdoret per te selektuar rreshtin me poshte

Parameters:

e-eventi
*/
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvDegeAdministrative, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvDegeAdministrative, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvDegeAdministrative, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvDegeAdministrative, indexSel);
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
    //        myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, false);
    myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, false, undefined, indexModifiko, pastrofusha, SucceededCallbackKonfig, resultkonf, colKontrollet, aktivizoFusha, colAtrTrupi);
}

//merr te dhenat e rreshtit te selektuar
function mbushfusha() {
    mbush = false;
    $('#hfShtimModifikim')[0].value = "modifikim";
    indexModifiko = gvDegeAdministrative.GetFocusedRowIndex();
    if (indexModifiko == -1)
        myMesazh.ShtoMesazhGabimi(hfState.Get('msgDegetAdministrativeZgjidhDegen'));
    else
        gvDegeAdministrative.GetRowValues(indexModifiko, 'IdDegeAdministrative;Kodi;Pershkrimi;Adresa;Aktiv;DateRegjistrimi;QendraKostos;IdSkemaQendraKosto;LlojQendre;Qendra', OnGetRowValuesMod); Utils.shfaqLoadingGif();;
}
//mbush fushat me te dhenat e rreshtit te selektuar
function OnGetRowValuesMod(values) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;
    $('#hfId')[0].value = values[0];
    txtKodi.SetText(values[1]);
    txtPershkrimi.SetText(values[2]);
    txtAdresa.SetText(values[3]);

    cbAktiv.SetChecked(values[4]);

    dteDtRegjistrimi.SetDate(values[5]);
    cmbLloji.SetValue(values[8]);
    var idNdermarrje = hfState.Get('idNdermarrje');
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "merrKodNjesieBiznesiDegeAdministrative"),
        data: JSON.stringify({ kodi: values[1], idNdermarrje: idNdermarrje })
    }).done(function (result) { SucceededCallbackKodNjesieBiznesiDegeAdministrative(result) });
    if (values[8] == 1)
        qendraKostos_TextBox.SetSelectedIndex(qendraKostos_TextBox.AddItem(values[9], values[6]));
    else qendraKostos_TextBox.SetSelectedIndex(qendraKostos_TextBox.AddItem(values[9], values[7]));
    var idGjuha = hfState.Get('idGjuha');
    //therret web service per te kontrolluar nese kjo llogari standarte eshte e lidhur apo jo
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "eshteLidhur"),
        data: JSON.stringify({ idkomp: "163", kodkonfi: cmbKonfigurimi.GetText(), iddokumenti: values[0], idNdermarrje: idNdermarrje, idGjuha: idGjuha })
    }).done(function (result) { SucceededCallbackLidhur(result, values[0]) });
    if (kaloTab) {
        PageControl.SetActiveTabIndex(1);
        myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, $('#hfShtimModifikim'));
    }
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
function SucceededCallbackKodNjesieBiznesiDegeAdministrative(result) {
    txtKodBiznesi.SetText(result.kodNjesieBiznesi);
}
//pastron fushat per shtim dhe ben aktive fushat
function pastrofusha() {
    txtKodi.SetText('');
    txtPershkrimi.SetText('');
    txtAdresa.SetText('');
    cbAktiv.SetChecked(true);
    qendraKostos_TextBox.SetSelectedIndex(-1);
    qendraKostos_TextBox.SetText('');
    cmbLloji.SetSelectedIndex(0);
    dteDtRegjistrimi.SetDate(new Date());
    var hf = $('#hfKontrollet')[0];
    aktivizoFusha(colKontrollet, colAtrTrupi, false);
    txtKodBiznesi.SetText('');
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
    gvDegeAdministrative.PerformCallback(idKomp + ";" + kodKonf);
}

function callWebserviceKonfigurimiInit(idKomp, kodKonf) {
    $("#dvDega").show();
    var idGjuha = hfState.Get('idGjuha');
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
        //            var kodniveli = result[5];
        //                var konfLlojRreshti = result[6];
        resultkonf = result;
        LupaKontrollet(colKontrollet, colAtrTrupi);
        var hf = $('#hfKontrollet');
        var hfMod = $('#hfShtimModifikim');
        var arrTabela = ['tblDegeAdministrative'];
        //myFaqeCelje.SucceededCallbackKonfig(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "#ASPxPageControl1_", "ASPxPageControl1_C");
        //myFaqeCelje.SucceededCallbackKonfigSlim(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "#ASPxPageControl1_", "ASPxPageControl1_C");
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "ASPxPageControl1_C");
    }

  //  $("#dvDega").show();//$("#dvDega")[0].style.visibility = 'visible';
}
//    function SucceededCallbackKonfigurimi(result) {
//        if (result != "") {
//            resultkonf = result;
//            var vlerat = '';
//            vlerat = result.split('*');
//            var kontrollet = vlerat[0].split(';');
//            Lupa(kontrollet);
//            var hf = $('#hfKontrollet')[0];
//            var hfLidhur = $("#hfLidhur")[0];
//            var hfMod = $('#hfShtimModifikim')[0];
//            var arrTabela = ['tblDegeAdministrative'];
//            myFaqeCelje.SucceededCallbackKonfigurimi(result, hf, hfLidhur, hfMod, '', arrTabela, "#ASPxPageControl1_", "ASPxPageControl1_C");
//            gvDegeAdministrative.PerformCallback("163" + ";" + cmbKonfigurimi.GetText());
//        } $("#dvDega")[0].style.visibility = 'visible';
//    }

function LupaKontrollet(kontrollet, colAtrTrupi) {
    //        var hf6 = $("#hfLupaAutorizimi")[0];
    //        for (var i = 0; i < kontrollet.length - 1; i++) {
    //            if (kontrollet[i].split(',')[0] == "cmbAutorizimi")
    //                hf6.value = kontrollet[i].split(',')[11].toString();
    //        }
}
function aktivizoFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $('#hfShtimModifikim');
    //        var hfLidhur = $("#hfLidhur")[0];
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
}
function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);

    callWebserviceKonfigurimi("163", cmbKonfigurimi.GetText());
}
function ndryshoKonfiguriminInit() {

    callWebserviceKonfigurimiInit("163", cmbKonfigurimi.GetText());
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
		        evt.keyCode : evt.charCode;
}

function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('Shto_DegeAdministrative.aspx',0, hf);
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
    //        indexModifiko = myFaqeCelje.EndRequestHandler(sender, args, hf, hfKontrollet, hfShtimModifikim, hfId, indexModifiko, PageControl, gvDegeAdministrative, "163")
    indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, gvDegeAdministrative, "163", pastrofusha, hfTeDrejta);
}

function valido(s, e) {
    myFaqeCelje.valido(s, e, PageControl, hfTeDrejta, $('#hfShtimModifikim'));
}

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}
var KPF;
function Autorizime_Click() {
    var hf = $("#hfLupaAutorizimi")[0];
    var queryStr = hf.value;
    myButtonClickLupa.Autorizime_Click('Zgjidh autorizimet', queryStr, 400, 400);
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
function Active_TabChanged(s, e) {
    indexModifiko = gvDegeAdministrative.GetFocusedRowIndex();    
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
    myMenu.PercaktoMenuSipasTabit(e.tab.index, hfTeDrejta,$('#hfShtimModifikim'));
                  
}