;  //variabla per te kaluar nga nje reshti i grides tek tjetri
var indexSel = 0;
var indexSel2 = 0;
var indexSel3 = 0;
var widthLupaKPF = 600, heightLupaKPF = 600;
var widthLupaAutorizime = 600, heightLupaAutorizime = 600;

var levizNgaShigjetat = false;
//indexi i zgjedhur per modifikim
var indexModifiko;
//perdoret per te treguar nese duhet te mbushen tabet apo jo kjo perdoret qe tabet te mbushen vetem heren e pare dhe jo pas cdo here qe  kalohet nga nje tab tek tjetri
var mbush = false;
//perdoret per te daluar rastet kur bejme double click apo thjesht click ne menyre qe ne double click te kaloje tek tabi i dyte
var kaloTab = false;
//perdoret per griden e llogarive ne menyre qe te lodohet vetem ne momentin kur shkon ne tabin e saj
var lista = true;

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
                break;
            default:
                break;
        }
    });
    var options = JSON.parse(hfState.Get("colAutorizime"));
    hfState.Remove("colAutorizime");
    cmbAutorizimi = new MultiSelect({
        container: "tblLlogaria",
        valueField: 'KodiAutorizim',
        labelField: 'KodiAutorizim',
        searchField: 'KodiAutorizim',
        options: options,
        meLupe: true,
        onButtonClickLupa: Autorizime1_Click,
        multiSelectId: "cmbAutorizimi"

    })
    changeName();
});

function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, grid1, "110", cmbKonfigurimi.GetText());
    myMenu.aplikoFiltra(s, e, grid2, "110", cmbKonfigurimi.GetText());
    myMenu.aplikoFiltra(s, e, grid3, "110", cmbKonfigurimi.GetText());
}
function textChanged(s, e) {
    myMenu.textChanged(s, e);
}
//metodat per te hapur faqen e modifikimit me double click
function OnGridDoubleClick(index) {
    indexModifiko = index;
    lista = true;
    mbushfusha();
    //callWebservice1();      te drejtat
}
function OnGridDoubleClick2(index) {
    indexModifiko = index;
    lista = true;
    mbushfusha();
    // callWebservice2();
}
function OnGridDoubleClick3(index) {
    lista = true;
    indexModifiko = index;
    mbushfusha();
    //   callWebservice3();
}
//        //therrasin web service per te pare ne kete te drejta
//        function callWebservice1() {
//            var emerPlusVeprim = 'KPF.aspx;Modifiko';
//        }
//        function callWebservice2() {
//            var emerPlusVeprim = 'KPF.aspx;Modifiko';
//        }
//        function callWebservice3() {
//            var emerPlusVeprim = 'KPF.aspx;Modifiko';
//        }
//        // This is the callback function that
//        // processes the Web Service return value.
//        function SucceededCallback1(result) {
//            if (result == "true") {
//                mbushfusha();
//            }
//            else {
//                alert("Nuk ke te drejta per te kryer kete veprim");
//            }
//        }
//        function SucceededCallback2(result) {
//            if (result == "true") {
//                mbushfusha();
//            }
//            else {
//                alert("Nuk ke te drejta per te kryer kete veprim");
//            }
//        }
//        function SucceededCallback3(result) {
//            if (result == "true") {
//                mbushfusha();
//            }
//            else {
//                alert("Nuk ke te drejta per te kryer kete veprim");
//            }
//        }
/*
Function: Poshte_click

perdoret per te selektuar rreshtin me poshte

Parameters:

e-eventi
*/
function Poshte_click(e) {
    if ($('#HiddenField4')[0].value == 0) {
        indexSel = myMenu.JSlevizNeGride.Poshte_click(e, grid1, indexSel);
    }
    else if ($('#HiddenField4')[0].value == 1) {
        indexSel2 = myMenu.JSlevizNeGride.Poshte_click(e, grid2, indexSel2);
    }
    else if ($('#HiddenField4')[0].value == 2) {
        indexSel3 = myMenu.JSlevizNeGride.Poshte_click(e, grid3, indexSel3);
    }
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    if ($('#HiddenField4')[0].value == 0) {
        indexSel = myMenu.JSlevizNeGride.Lart_click(e, grid1, indexSel);
    }
    else if ($('#HiddenField4')[0].value == 1) {
        indexSel2 = myMenu.JSlevizNeGride.Lart_click(e, grid2, indexSel2);
    }
    else if ($('#HiddenField4')[0].value == 2) {
        indexSel3 = myMenu.JSlevizNeGride.Lart_click(e, grid3, indexSel3);
    }
}

/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes

Parameters:

e-eventi
*/
function Fillim_click(e) {
    if ($('#HiddenField4')[0].value == 0) {
        indexSel = myMenu.JSlevizNeGride.Fillim_click(e, grid1, indexSel);
    }
    else if ($('#HiddenField4')[0].value == 1) {
        indexSel2 = myMenu.JSlevizNeGride.Fillim_click(e, grid2, indexSel2);
    }
    else if ($('#HiddenField4')[0].value == 2) {
        indexSel3 = myMenu.JSlevizNeGride.Fillim_click(e, grid3, indexSel3);
    }
}

/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    if ($('#HiddenField4')[0].value == 0) {
        indexSel = myMenu.JSlevizNeGride.Fund_click(e, grid1, indexSel);
    }
    else if ($('#HiddenField4')[0].value == 1) {
        indexSel2 = myMenu.JSlevizNeGride.Fund_click(e, grid2, indexSel2);
    }
    else if ($('#HiddenField4')[0].value == 2) {
        indexSel3 = myMenu.JSlevizNeGride.Fund_click(e, grid3, indexSel3);
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
    //            myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, false);
    if (e.item.name == 'Ruaj') {
        mbush = false;
        Utils.vendosVlereNeHiddenField("cmbAutorizimiHf", cmbAutorizimi.GetText());
    }
    myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, false, undefined, indexModifiko, pastrofusha, SucceededCallbackKonfig, resultkonf, colKontrollet, aktivizoFusha, colAtrTrupi);
}

//merr te dhenat e rreshtit te selektuar
function mbushfusha() {
    mbush = false;
    $('#hfShtimModifikim')[0].value = "modifikim";
    if ($('#HiddenField4')[0].value == 0) {
        indexModifiko = grid1.GetFocusedRowIndex();
        if (indexModifiko == -1)
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgDuhetTeZgjidhni1StruktureLlogarie"));
        else
            grid1.GetRowValues(indexModifiko, 'IdKPF;KodiKPF;EmertimiKPF;NiveliKPF;IdAutorizimi;Inaktiv;ShenimeKPF;Prind;EmertimiKPF_fr', OnGetRowValuesMod);
    }
    else if ($('#HiddenField4')[0].value == 1) {
        indexModifiko = grid2.GetFocusedRowIndex();
        if (indexModifiko == -1)
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgDuhetTeZgjidhni1StruktureLlogarie"));
        else
            grid2.GetRowValues(indexModifiko, 'IdKPF;KodiKPF;EmertimiKPF;NiveliKPF;IdAutorizimi;Inaktiv;ShenimeKPF;Prind;EmertimiKPF_fr', OnGetRowValuesMod);
    }
    else if ($('#HiddenField4')[0].value == 2) {
        indexModifiko = grid3.GetFocusedRowIndex();
        if (indexModifiko == -1)
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgDuhetTeZgjidhni1StruktureLlogarie"));
        else
            grid3.GetRowValues(indexModifiko, 'IdKPF;KodiKPF;EmertimiKPF;NiveliKPF;IdAutorizimi;Inaktiv;ShenimeKPF;Prind;EmertimiKPF_fr', OnGetRowValuesMod);
    }
    Utils.shfaqLoadingGif();
}

//mbush fushat me te dhenat e rreshtit te selektuar
function OnGetRowValuesMod(values) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;
    $('#hfId')[0].value = values[0];
    txtKodi.SetText(values[1]);
    txtEmertimi.SetText(values[2]);
    cmbNiveleKPF.SetText(values[3]);
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "callWsGetAutorizimeSipasLlojitDheIdLidhese"),
        data: JSON.stringify({ idLidhese: values[0], kodLloji: 'KPF', idPerdorues: hfState.Get('idPerdoruesi')})
    }).done(SuksesAutorizimi);
    cbInaktiv.SetChecked(values[5]); //ka ndryshuar si emertim nga inaktiv ne aktiv dhe zgjidhen te kundertat
    kodi_TextBox.SetText(values[1]);
    emertimi_TextBox.SetText(values[2]);
    txtShenime.SetText(values[6]);
    txtEmerLlogarieFr.SetText(values[8]);
    if (values[7] != null && values[7] != "")
        cmbPrindi.SetText(values[7]);
    else cmbPrindi.SetValue(null);
    txtEmertimiLlog.SetText(values[2]);
    txtKodiLlog.SetText(values[1]);
    listBoxLlogarite.PerformCallback(indexModifiko);
    var idGjuha = hfState.Get('idGjuha');
    var idNdermarrje = hfState.Get('idNdermarrje');
    //therret web service per te kontrolluar nese kjo llogari standarte eshte e lidhur apo jo
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "eshteLidhur"),
        data: JSON.stringify({ idkomp: "110", kodkonfi: cmbKonfigurimi.GetText(), iddokumenti: values[0], idNdermarrje: idNdermarrje, idGjuha: idGjuha })
    }).done(function (result) { SucceededCallbackLidhur(result, values[0]); });

    if (kaloTab) {
        PageControl.SetActiveTabIndex(1);
        myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, $('#hfShtimModifikim'));
    }
}
function SuksesAutorizimi (result) {
    if (result.idLidhese == $('#hfId').val())
        cmbAutorizimi.SetText(result.autorizime);
    else cmbAutorizimi.SetValue(null);
}
//therret funksionin qe ben enabled dhe disabled fushat sipas lidhjes
function SucceededCallbackLidhur(result, idObjekti) {
    
    if (idObjekti.toString() !== $('#hfId').val()) {
        Utils.hiqLoadingGif();
        return;
    }
    var hf = $('#hfKontrollet')[0]; //mban te dhenat mbi kontrollet
    var hfLidhur = $("#hfLidhur")[0];
    hfLidhur.value = result;
    aktivizoFusha(colKontrollet, colAtrTrupi, eval(result.toLowerCase()));

}
//pastron fushat per shtim dhe ben aktive fushat
function pastrofusha() {
    txtKodi.SetText('');
    txtEmertimi.SetText('');
    cmbNiveleKPF.SetText('1');
    cmbAutorizimi.SetValue(null);
    cbInaktiv.SetChecked(true);
    kodi_TextBox.SetText('');
    emertimi_TextBox.SetText('');
    txtShenime.SetText('');
    txtEmerLlogarieFr.SetText('');
    cmbPrindi.SetValue(null);
    txtEmertimiLlog.SetText('');
    txtKodiLlog.SetText('');
    var hf = $('#hfKontrollet')[0];
    aktivizoFusha(colKontrollet, colAtrTrupi, false);
    listBoxLlogarite.PerformCallback(-1);
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
function OnGridSelectionChanged2(e) {
    indexSel2 = myMenu.JSlevizNeGride.OnGridSelectionChanged(e, indexSel2);
}
function OnGridSelectionChanged3(e) {
    indexSel3 = myMenu.JSlevizNeGride.OnGridSelectionChanged(e, indexSel3);
}
//therret web service per konfigurimet
function callWebserviceKonfigurimi(idKomp, kodKonf) {
    var idGjuha = hfState.Get('idGjuha');
    $.ajax({   
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
    grid1.PerformCallback(idKomp + ";" + kodKonf);
    grid2.PerformCallback(idKomp + ";" + kodKonf);
    grid3.PerformCallback(idKomp + ";" + kodKonf);
}
function callWebserviceKonfigurimiInit(idKomp, kodKonf) {
  //  $("#dvKPF").show();
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
//vendos kontrollet sipas konfigurimit
function SucceededCallbackKonfig(result) {
   // $("#dvKPF").show();//$("#dvKPF")[0].style.visibility = 'visible';
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
        var arrTabela = ['tblLlogaria', 'tblShenime'];
        //                myFaqeCelje.SucceededCallbackKonfigurimi(result, hf, hfLidhur, hfMod, '', arrTabela, "#ASPxPageControl1_", "ASPxPageControl1_C");
        //myFaqeCelje.SucceededCallbackKonfig(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "#ASPxPageControl1_", "ASPxPageControl1_C");
        //myFaqeCelje.SucceededCallbackKonfigSlim(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "#ASPxPageControl1_", "ASPxPageControl1_C");
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "ASPxPageControl1_C");
    }
}
//        function SucceededCallbackKonfigurimi(result) {
//            $("#dvKPF")[0].style.visibility = 'visible';
//            if (result != "") {
//                resultkonf = result;
//                var vlerat = '';
//                vlerat = result.split('*');
//                var kontrollet = vlerat[0].split(';');
//                Lupa(kontrollet);

//                var hf = $('#hfKontrollet')[0];
//                var hfLidhur = $("#hfLidhur")[0];
//                var hfMod = $('#hfShtimModifikim')[0];
//                var arrTabela = ['tblLlogaria', 'tblShenime'];
//                myFaqeCelje.SucceededCallbackKonfigurimi(result, hf, hfLidhur, hfMod, '', arrTabela, "#ASPxPageControl1_", "ASPxPageControl1_C");
//                grid1.PerformCallback("110" + ";" + cmbKonfigurimi.GetText());
//                grid2.PerformCallback("110" + ";" + cmbKonfigurimi.GetText());
//                grid3.PerformCallback("110" + ";" + cmbKonfigurimi.GetText());
//            }
//        }


function LupaKontrollet(kontrollet, colAtrTrupi) {
    var hf3 = $("#hfLupaKpf1");
    var hf1 = $("#hfLupaAutorizimi");
    for (var i = 0; i < kontrollet.length; i++) {
        if (kontrollet[i].KodKontrolli == "cmbAutorizimi")
            hf1.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
        if (kontrollet[i].KodKontrolli == "cmbPrindi")
            hf3.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
    }
}
//ben aktive ose jo fushat sipas lidhjes
function aktivizoFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $('#hfShtimModifikim');
    if (hfMod.val() == "modifikim")
        txtKodiLlog.SetEnabled(false);
    else txtKodiLlog.SetEnabled(true);
    var hfLidhur = $("#hfLidhur")[0];
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
}
function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);
    callWebserviceKonfigurimi("110", cmbKonfigurimi.GetText());
}
function ndryshoKonfiguriminInit() {
    $("#dvKPF").show();
    callWebserviceKonfigurimiInit("110", cmbKonfigurimi.GetText());
}
function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('Shto_KPF.aspx', 0, hf);
    myMenu.PercaktoMenuSipasTabit(0, hfTeDrejta, $('#hfShtimModifikim'));
}
var grida;
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
    switch ($('#HiddenField4').val()) {
        case '0':
            //                    indexModifiko = myFaqeCelje.EndRequestHandler(sender, args, hf, hfKontrollet, hfShtimModifikim, hfId, indexModifiko, PageControl, grid1, "110")
            indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, grid1, "110", pastrofusha, hfTeDrejta);
            break;
        case '1':
            //                    indexModifiko = myFaqeCelje.EndRequestHandler(sender, args, hf, hfKontrollet, hfShtimModifikim, hfId, indexModifiko, PageControl, grid2, "110")
            indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, grid2, "110", pastrofusha, hfTeDrejta);
            break;
        case '2':
            //                    indexModifiko = myFaqeCelje.EndRequestHandler(sender, args, hf, hfKontrollet, hfShtimModifikim, hfId, indexModifiko, PageControl, grid3, "110")
            indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, grid3, "110", pastrofusha, hfTeDrejta);
            break;
        default:
            break;
    }
    grid2.PerformCallback("110", cmbKonfigurimi.GetText());
    grid3.PerformCallback("110", cmbKonfigurimi.GetText());
}
var arr = new Array();
var counter = 0;
var arr2 = new Array();
var counter2 = 0;
//metodat per te pastruar array kur perdoruesi shtyp butonin pastro
function pastro() {
    arr = new Array();
    counter = 0;
    arr2 = new Array();
    counter2 = 0;
}
function Autorizime1_Click() {
    KPF = 0;
    var hf = $("#hfLupaAutorizimi")[0];
    var queryStr = hf.value + '&autorizimet=' + cmbAutorizimi.GetText();
    myButtonClickLupa.Autorizime_Click(hfState.Get("headerPopUpZgjidhAutorizimet"), queryStr, widthLupaAutorizime, heightLupaAutorizime);
}
var txtLlog = "kpf";

function KPF1_Click() {
    var hf = $("#hfLupaKpf1")[0];
    var queryStr = hf.value;
    if ($('#HiddenField4')[0].value == 0) {
        myButtonClickLupa.KPF_Click(hfState.Get("headerPopUpZgjidhLlogarineStandarte"), queryStr, widthLupaKPF, heightLupaKPF, 1);
    }
    if ($('#HiddenField4')[0].value == 1) {
        myButtonClickLupa.KPF_Click(hfState.Get("headerPopUpZgjidhLlogarineStandarte"), queryStr, widthLupaKPF, heightLupaKPF, 2);
    }
    if ($('#HiddenField4')[0].value == 2) {
        myButtonClickLupa.KPF_Click(hfState.Get("headerPopUpZgjidhLlogarineStandarte"), queryStr, widthLupaKPF, heightLupaKPF, 3);
    }
}
function tabi() {
    $("input[id$='hfTab']")[0].value = ASPxPageControl2.GetActiveTab().index;
}
//validon te dhenat
function valido(s, e) {
    myFaqeCelje.valido(s, e, PageControl, hfTeDrejta, $('#hfShtimModifikim'));
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

function tabsActiveTabChanging(s, e) {
    var hf;
    if ($('#HiddenField4')[0].value == 0) {
        indexModifiko = grid1.GetFocusedRowIndex();
        if (mbush) {
            if (indexModifiko != -1) {
                OnGridDoubleClick(indexModifiko);
            }
            else {
                mbush = false;
                $('#hfShtimModifikim')[0].value = 'shtim';
                $('#hfId')[0].value = 0; pastrofusha();
            }
        }
        kaloTab = false;
    }
    else if ($('#HiddenField4')[0].value == 1) {
        indexModifiko = grid2.GetFocusedRowIndex();
        if (mbush) {
            if (indexModifiko != -1) {
                OnGridDoubleClick2(indexModifiko);
            }
            else {
                mbush = false;
                $('#hfShtimModifikim')[0].value = 'shtim';
                $('#hfId')[0].value = 0; pastrofusha();
            }
        }
        kaloTab = false;
    }
    else if ($('#HiddenField4')[0].value == 2) {
        indexModifiko = grid3.GetFocusedRowIndex();
        if (mbush) {
            if (indexModifiko != -1) {
                OnGridDoubleClick3(indexModifiko);
            }
            else {
                mbush = false;
                $('#hfShtimModifikim')[0].value = 'shtim';
                $('#hfId')[0].value = 0; pastrofusha();
            }
        }
        kaloTab = false;
    }
    myMenu.PercaktoMenuSipasTabit(e.tab.index, hfTeDrejta, $('#hfShtimModifikim'));
}
function Selected_IndexChanged(s, e) {
    var s=cmbPrindi.GetText().split(';');
    cmbPrindi.SetText(s[0]);
    txtKodi.SetText(s[0]);
    kodi_TextBox.SetText(txtKodi.GetText());
    if(s[2]!=undefined)
        cmbNiveleKPF.SetText(parseInt(s[2])+1);
    txtKodiLlog.SetText(txtKodi.GetText());
	
}
function TextChanged_txtKodi(s, e) {
    kodi_TextBox.SetText(txtKodi.GetText());
    txtKodiLlog.SetText(txtKodi.GetText());
	
}
function TextChanged_txtEmertimi(s, e) {
    emertimi_TextBox.SetText(txtEmertimi.GetText());
    txtEmertimiLlog.SetText(txtEmertimi.GetText());                                     
}
function TextChanged_kodi_TextBox(s, e) {
    txtKodi.SetText(kodi_TextBox.GetText());
    txtKodiLlog.SetText(kodi_TextBox.GetText());
	
}
function TextChanged_emertimi_TextBox(s, e) {
    txtEmertimi.SetText(emertimi_TextBox.GetText());
    txtEmertimiLlog.SetText(emertimi_TextBox.GetText());
}
function TextChanged_txtKodiLlog(s, e) {
    txtKodi.SetText(txtKodiLlog.GetText());
    kodi_TextBox.SetText(txtKodiLlog.GetText());
	
}
function TextChanged_txtEmertimiLlog(s, e) {
    txtEmertimi.SetText(txtEmertimiLlog.GetText());
    emertimi_TextBox.SetText(txtEmertimiLlog.GetText());
                                          
}