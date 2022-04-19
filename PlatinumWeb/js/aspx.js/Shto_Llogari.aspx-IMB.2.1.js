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
var identifikuesperKPF = "Shto_Llogari.aspx";
var widthLupaLogaria = 900, heightLupaLlogaria = 600;
var widthLupaKPF = 680, heightLupaKPF = 600;
var widthLupaGrupi = 680, heightLupaGrupi = 600;
var widthLupaNenGrupi = 680, heightLupaNenGrupi = 600;
var widthLupaAutorizime = 680, heightLupaAutorizime = 600;

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

    var options = JSON.parse(hfState.Get("colAutorizime"));
    hfState.Remove("colAutorizime");
    cmbAutorizimi = new MultiSelect({
        container: "tblGrupimi",
        valueField: 'KodiAutorizim',
        labelField: 'KodiAutorizim',
        searchField: 'KodiAutorizim',
        options: options,
        meLupe: true,
        onButtonClickLupa: Autorizime_Click,
        multiSelectId: "cmbAutorizimi"

    })
    changeName();
});

//per filtrat
function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, grid_ListLlogarish, "107", cmbKonfigurimi.GetText());
}
function Objektiva_Click() {
    myButtonClickLupa.LupaUniversal_Click( hfState.Get("msgZgjidhniObjektivenEKostos"), 'LupaObjektivaKosto.aspx?vjenNga=Shto_Punonjes', widthLupaLogaria, widthLupaLogaria);
}
function Kategori_Click() {
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("headerPopUpZgjidhKategorineEShpenzimit"), 'LupaKategoriShpenzimi.aspx?lloji=JoPrind', widthLupaLogaria, widthLupaLogaria);
}
function Qendra_Click() {
    if (cmbLloji.GetValue() == 1)
        myButtonClickLupa.LupaUniversal_Click(hfState.Get("headerPopUpZgjidhQendrenKostos"), 'Shto_QendraKosto.aspx?lupe=true&llojLupe=qk&vjenNga=Shto_Llogari', widthLupaLogaria, widthLupaLogaria);
    else myButtonClickLupa.LupaUniversal_Click( hfState.Get("headerPopUpZgjidhSkemenKostos"), 'LupaSkemaKosto.aspx?vjenNga=Shto_Llogari', widthLupaLogaria, widthLupaLogaria);
}
function textChanged(s, e) {
    myMenu.textChanged(s, e);
}
//metodat per te hapur faqen e modifikimit me double click
function OnGridDoubleClick(index) {
    indexModifiko = index;
    lista = true;
    mbushfusha();

    //callWebservice();    te drejtat
}
//        //therrasin web service per te pare ne kete te drejta
//        function callWebservice() {
//            var emerPlusVeprim = 'Llogaria.aspx;Modifiko';
//        }

//        // This is the callback function that
//        // processes the Web Service return value.
//        function SucceededCallback(result) {
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
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, grid_ListLlogarish, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, grid_ListLlogarish, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, grid_ListLlogarish, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, grid_ListLlogarish, indexSel);
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
    //            myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, true);
    
    myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, false, undefined, indexModifiko, pastrofusha, SucceededCallbackKonfig, resultkonf, colKontrollet, aktivizoFusha, colAtrTrupi);
    if (e.item.name == 'Ruaj') {
        Utils.vendosVlereNeHiddenField("cmbAutorizimiHf", cmbAutorizimi.GetText());
        pastro();
    }
}

//merr te dhenat e rreshtit te selektuar
function mbushfusha() {
    mbush = false;
    $('#hfShtimModifikim')[0].value = "modifikim";
    indexModifiko = grid_ListLlogarish.GetFocusedRowIndex();
    if (indexModifiko == -1)
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgShtoLlogariDuhetTeZgjidhniNjeLlogari"));
    else
        grid_ListLlogarish.GetRowValues(indexModifiko, 'IdLlogari;NrLlogari;EmerLlogari1;EmerLlogari2;QendraKostos;KodiKPF1;KodiKPF2;KodiKPF3;NivelTakse;KodiMonedha;PershkrimiGrupiLlogaria;PershkrimiNenGrupiLlogaria;LlogariKonsoliduese;LlogariKoresponduese;NivelTakse;IdObjektivaKosto;Objektiva;IdSkemaQendraKosto;LlojQendre;Qendra;IdKategoriShpenzimi;KategoriShpenzimi;Aktiv;Shenime1;Shenime2;Shenime3;Shenime4;Shenime5;EmerLlogari_fr', OnGetRowValuesMod);
    Utils.shfaqLoadingGif();;

}
//mbush fushat me te dhenat e rreshtit te selektuar
function OnGetRowValuesMod(values) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;
    $('#hfId')[0].value = values[0];
    txtNr.SetText(values[1]);
    if ($('#hfShtimModifikim').val() == "modifikim")
        txtNr.SetEnabled(false);
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "callWsGetAutorizimeSipasLlojitDheIdLidhese"),
        data: JSON.stringify({ idLidhese: values[0], kodLloji: 'Llogari', idPerdorues: hfState.Get('idPerdoruesi')})
    }).done(SucceededCallbackKtheAutorizime);
    txtEmerLlogarie1.SetText(values[2]);
    txtEmerLlogarie2.SetText(values[3]);
    txtEmerLlogarieFr.SetText(values[28]);
    cmbMonedha.SetText(values[9]);
    cmbGrupi.SetText(values[10]);
    cmbNengrupi.SetText(values[11]);
    numri_TextBox.SetText(values[1]);
    emer_TextBox.SetText(values[2]);
    if (values[5] != null && values[5] != "")
        FSC_1_Debi_TextBox.SetText(values[5]);
    else FSC_1_Debi_TextBox.SetValue(null);
    if (values[6] != null && values[6] != "")
        FSC_2_Debi_TextBox.SetText(values[6]);
    else FSC_2_Debi_TextBox.SetValue(null);
    if (values[7] != null && values[7] != "")
        FSC_3_Debi_TextBox.SetText(values[7]);
    else FSC_3_Debi_TextBox.SetValue(null);
//    qendraKostos_TextBox.SetText(values[4]);
    //nivelTakse_ComboBox.SetText(values[8]);
    if (values[12] != 0)
        llogKonsoliduese_TextBox.SetValue(values[12]);
    if (values[13] != 0)
        llogKorresponduese_TextBox.SetValue(values[13]);
    if (values[14] != 0)
        nivelTakse_ComboBox.SetValue(values[14]);
    else
        nivelTakse_ComboBox.SetValue(null);
    if (nivelTakse_ComboBox.FindItemByText(nivelTakse_ComboBox.GetInputElement().value) == null)
        nivelTakse_ComboBox.SetValue(null);
    cmbObjektiva.SetSelectedIndex(cmbObjektiva.AddItem(values[16], values[15]));
      cmbKategori.SetSelectedIndex(cmbKategori.AddItem(values[21], values[20]));
    cmbLloji.SetValue(values[18]);
    if(values[18]==1)
        qendraKostos_TextBox.SetSelectedIndex(qendraKostos_TextBox.AddItem(values[19], values[4]));
    else qendraKostos_TextBox.SetSelectedIndex(qendraKostos_TextBox.AddItem(values[19], values[17]));   
    txtNr6.SetText(values[1]);
    txtEmertimi6.SetText(values[2]);
    checkAktivLlogaria.SetChecked(values[22]);
    txtShenime1.SetText(values[23]);
    txtShenime2.SetText(values[24]);
    txtShenime3.SetText(values[25]);
    txtShenime4.SetText(values[26]);
    txtShenime5.SetText(values[27]);

    var idGjuha = hfState.Get('idGjuha');
    var idNdermarrje = hfState.Get('idNdermarrje');
    //therret web service per te kontrolluar nese kjo llogari standarte eshte e lidhur apo jo
    $.ajax({      
        url: Utils.getServerApiUrl("Konfigurime", "eshteLidhur"),
        data: JSON.stringify({ idkomp: "107", kodkonfi: cmbKonfigurimi.GetText(), iddokumenti: values[0], idNdermarrje: idNdermarrje, idGjuha: idGjuha })
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
    //            var hf = $('#hfKontrollet')[0]; //mban te dhenat mbi kontrollet
    var hfLidhur = $("#hfLidhur")[0];
    hfLidhur.value = result;
    aktivizoFusha(colKontrollet, colAtrTrupi, eval(result.toLowerCase()));

}
//pastron fushat per shtim dhe ben aktive fushat
function pastrofusha() {
    txtNr.SetText('');
    txtEmerLlogarie1.SetText('');
    txtEmerLlogarie2.SetText('');
    txtEmerLlogarieFr.SetText('');
    cmbAutorizimi.SetValue(null);
    hf = $('#hfMonedhaNder')[0].value;
    cmbMonedha.SetText(hf);
    cmbGrupi.SetSelectedIndex(-1);
    cmbNengrupi.SetSelectedIndex(-1);
    cmbNengrupi.SetText('');
    numri_TextBox.SetText('');
    emer_TextBox.SetText('');
    cmbLloji.SetSelectedIndex(0);
    FSC_1_Debi_TextBox.SetSelectedIndex(-1);
    FSC_2_Debi_TextBox.SetSelectedIndex(-1); cmbObjektiva.SetSelectedIndex(-1); cmbKategori.SetSelectedIndex(-1);
    cmbObjektiva.SetText('');cmbKategori.SetText('');
    FSC_3_Debi_TextBox.SetSelectedIndex(-1); qendraKostos_TextBox.SetSelectedIndex(-1);
    qendraKostos_TextBox.SetText('');
    nivelTakse_ComboBox.SetSelectedIndex(-1);
    llogKonsoliduese_TextBox.SetText('');
    llogKorresponduese_TextBox.SetText('');   
    txtNr6.SetText('');
    txtEmertimi6.SetText('');
    checkAktivLlogaria.SetChecked(true);
    txtShenime1.SetText('');
    txtShenime2.SetText('');
    txtShenime3.SetText('');
    txtShenime4.SetText('');
    txtShenime5.SetText('');

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
    grid_ListLlogarish.PerformCallback(idKomp + ";" + kodKonf);
}

function callWebserviceKonfigurimiInit(idKomp, kodKonf) {
    $("#dvLlogaria").show();
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
        //                var kodniveli = result[5];
        //                var konfLlojRreshti = result[6];
        resultkonf = result;
        LupaKontrollet(colKontrollet, colAtrTrupi);
        //                Lupa(kontrollet);
        var hf = $('#hfKontrollet');
        var hfLidhur = $("#hfLidhur");
        var hfMod = $('#hfShtimModifikim');
        var arrTabela = ['tblLlogaria', 'tblGrupimi'];
        //myFaqeCelje.SucceededCallbackKonfig(colKontrollet, colAtrTrupi, hf, hfMod, 'cmbModeli', arrTabela, "#ASPxPageControl1_", "ASPxPageControl1_C");
        //myFaqeCelje.SucceededCallbackKonfigSlim(colKontrollet, colAtrTrupi, hf, hfMod, 'cmbModeli', arrTabela, "#ASPxPageControl1_", "ASPxPageControl1_C");
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, 'cmbModeli', arrTabela, "ASPxPageControl1_C");
    }

   // $("#dvLlogari").show();//$("#dvLlogaria")[0].style.visibility = 'visible';
}
//        function SucceededCallbackKonfigurimi(result) {
//            if (result != "") {
//                resultkonf = result;
//                var vlerat = '';
//                vlerat = result.split('*');
//                var kontrollet = vlerat[0].split(';');
//                Lupa(kontrollet);
//                var hf = $('#hfKontrollet')[0];
//                var hfLidhur = $("#hfLidhur")[0];
//                var hfMod = $('#hfShtimModifikim')[0];
//                var arrTabela = ['tblLlogaria', 'tblGrupimi'];
//                myFaqeCelje.SucceededCallbackKonfigurimi(result, hf, hfLidhur, hfMod, 'cmbModeli', arrTabela, "#ASPxPageControl1_", "ASPxPageControl1_C");
//                grid_ListLlogarish.PerformCallback("107" + ";" + cmbKonfigurimi.GetText());
//            } $("#dvLlogaria")[0].style.visibility = 'visible';
//        }
function LupaKontrollet(kontrollet, colAtrTrupi) {
    var hf1 = $("#hfLupaGrupi");
    var hf2 = $("#hfLupaNengrupi");
    var hf3 = $("#hfLupaKpf1");
    var hf4 = $("#hfLupaKpf2");
    var hf5 = $("#hfLupaKpf3");
    var hf6 = $("#hfLupaAutorizimi");
    var hf7 = $("#hfLupaLlogKons");
    var hf8 = $("#hfLupaLlogKorr");
    for (var i = 0; i < kontrollet.length; i++) {

        //id e konfigurimit te lupes vendoset ne hidden field
        //vlera e saj do t'i kalohet si query string hapjes se popUp-it
        if (kontrollet[i].KodKontrolli == "cmbGrupi") {
            hf1.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "cmbNengrupi") {
            hf2.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "FSC_1_Debi_TextBox") {
            hf3.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "FSC_2_Debi_TextBox") {
            hf4.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "FSC_3_Debi_TextBox") {
            hf5.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "cmbAutorizimi") {
            hf6.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
        if (kontrollet[i].KodKontrolli == "llogKonsoliduese_TextBox") {
            hf7.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
    }
}
function aktivizoFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $('#hfShtimModifikim');
    //            var hfLidhur = $("#hfLidhur")[0];
    //            myFaqeCelje.aktivizoFusha(vlerat, hfMod, hfLidhur, '#ASPxPageControl1_');
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
}
function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("PershkrimKonfigAmbjente"));
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("KodKonfigAmbjente"));
    callWebserviceKonfigurimi("107", cmbKonfigurimi.GetText());
}
function ndryshoKonfiguriminInit() {
    callWebserviceKonfigurimiInit("107", cmbKonfigurimi.GetText());
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ? evt.keyCode : evt.charCode;
}

function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('Shto_Llogari.aspx',0, hf);
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
    //            indexModifiko = myFaqeCelje.EndRequestHandler(sender, args, hf, hfKontrollet, hfShtimModifikim, hfId, indexModifiko, PageControl, grid_ListLlogarish, "107")
    indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, grid_ListLlogarish, "107", pastrofusha, hfTeDrejta);
}

var arrVlerat = new Array();
var countvlerat = 0;
var editorValues = new Object();
var editorValues1 = new Object();
var identikuesPerPopupGrupeLlogari = "Shto_Llogari";
var identikuesPerPopupNenGrupeLlogari = "Shto_Llogari";
var KPF;
function Autorizime_Click() {
    KPF = 0;
    var hf = $("#hfLupaAutorizimi")[0];
    var queryStr = hf.value + '&autorizimet=' + cmbAutorizimi.GetText();
    myButtonClickLupa.Autorizime_Click(hfState.Get("headerPopUpZgjidhAutorizimet"), queryStr, widthLupaAutorizime, heightLupaAutorizime);
}
var grida;
function Grupi_Click() {
    var hf = $("#hfLupaGrupi")[0];
    var queryStr = hf.value;
    myButtonClickLupa.Grupi_Click(hfState.Get("headerPopUpZgjidhGrupin"), queryStr, widthLupaGrupi, heightLupaGrupi);
}

function Nengrupi_Click(Grupi) {
    var hf = $("#hfLupaNengrupi")[0];
    var queryStr = hf.value;
    myButtonClickLupa.Nengrupi_Click(hfState.Get("headerPopUpZgjidhNenGrupin"), queryStr, widthLupaNenGrupi, heightLupaNenGrupi, Grupi);
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
    var hf = $("#hfStatusi")[0];
    hf.value = "false";
    arrVlerat = new Array();
    countvlerat = 0;
}
//metodat per te hapur popupet e KPF dhe te llogarise konsoliduese dhe llogarise korresponduese

function KPF1_Click() {
    var hf = $("#hfLupaKpf1")[0];
    var queryStr = hf.value;
    txtLlog.SetText('KPF1');
    myButtonClickLupa.KPF_Click(hfState.Get("headerPopUpZgjidhLlogarineStandarte"), queryStr, widthLupaKPF, heightLupaKPF, 1);
}

function KPF2_Click() {
    var hf = $("#hfLupaKpf2")[0];
    var queryStr = hf.value;
    txtLlog.SetText('KPF2');
    myButtonClickLupa.KPF_Click(hfState.Get("headerPopUpZgjidhLlogarineStandarte"), queryStr, widthLupaKPF, heightLupaKPF, 2);
}

function KPF3_Click() {
    var hf = $("#hfLupaKpf3")[0];
    var queryStr = hf.value;
    txtLlog.SetText('KPF3');
    myButtonClickLupa.KPF_Click(hfState.Get("headerPopUpZgjidhLlogarineStandarte"), queryStr, widthLupaKPF, heightLupaKPF, 3);
}

function LlogKorr_Click() {
    var hf = $("#hfLupaLlogKorr")[0];
    var queryStr = hf.value;
    txtLlog.SetText('korr');
    myButtonClickLupa.ButtonClickLlogaria(hfState.Get("headerPopUpText"), queryStr, widthLupaLogaria, heightLupaLlogaria);
}
function LlogKons_Click() {
    var hf = $("#hfLupaLlogKons")[0];
    var queryStr = hf.value;
    txtLlog.SetText('kons');
    myButtonClickLupa.ButtonClickLlogaria(hfState.Get("headerPopUpText"), queryStr, widthLupaLogaria, heightLupaLlogaria);
}

//merr vlerat e fushave shtese
function ShtoStringOrDate(editor, key) {//rasti per string dhe date
    var hidFieldf = $("#hfFushatShtese")[0]; //shton vleren tek hidden field e cila perdoret me vone nga kodi
    countvlerat = myFushaShtese.ShtoStringOrDate(editor, key, hidFieldf, countvlerat);
}
function ShtoIntOrDouble(editor, key) {//rasti per int dhe double
    var hidFieldf = $("#hfFushatShtese")[0]; //shton vleren tek hidden field e cila perdoret me vone nga kodi
    countvlerat = myFushaShtese.ShtoIntOrDouble(editor, key, hidFieldf, countvlerat);
}
function ShtoCheck(editor, key) {//rasti per check box
    var hidFieldf = $("#hfFushatShtese")[0]; //shton vleren tek hidden field e cila perdoret me vone nga kodi
    countvlerat = myFushaShtese.ShtoCheck(editor, key, hidFieldf, countvlerat);
}
function ShtoList(editor, key) {//rasti per list box
    var hidFieldf = $("#hfFushatShtese")[0]; //shton vleren tek hidden field e cila perdoret me vone nga kodi
    countvlerat = myFushaShtese.ShtoList(editor, key, hidFieldf, countvlerat);
}

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

function activeTabChanged(s, e) {
    indexModifiko = grid_ListLlogarish.GetFocusedRowIndex();
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
    myMenu.PercaktoMenuSipasTabit(e.tab.index, hfTeDrejta, $('#hfShtimModifikim'));
}

function onNdryshimFokusi() {
    try {
        if (PageControl.GetActiveTabIndex() == 0)
            mbush = true;
    } catch (e) { }
}

function kontrolloGrupin(s, e) {
    grida = false;
    if(cmbGrupi.GetText()!= '')
        Nengrupi_Click(cmbGrupi.GetText());
    else myMesazh.ShtoMesazhGabimi('Zgjidhni grupin'); 
}

function clickExport(e) {
    if (grid_ListLlogarish.GetSelectedRowCount() == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjdhniNjeNgaElementetEListes"));
        e.processOnServer = false;
    }
}

function TextChanged_txtNr(s, e) {
    numri_TextBox.SetText(txtNr.GetText());
    txtNr6.SetText(txtNr.GetText());
}
function TextChanged_txtEmerLlogarie1(s, e) {
    emer_TextBox.SetText(txtEmerLlogarie1.GetText());
    txtEmertimi6.SetText(txtEmerLlogarie1.GetText());                                                 
}
function TextChanged_numri_TextBox(s, e) {
    txtNr.SetText(numri_TextBox.GetText());

    txtNr6.SetText(numri_TextBox.GetText());	
}
function TextChanged_emer_TextBox(s, e) {
    txtEmerLlogarie1.SetText(emer_TextBox.GetText());
    txtEmertimi6.SetText(emer_TextBox.GetText());                                   
}
function TextChanged_txtNr6(s, e) {
    txtNr.SetText(txtNr6.GetText());
    numri_TextBox.SetText(txtNr6.GetText());
}
function TextChanged_txtEmertimi6(s, e) {
    txtEmerLlogarie1.SetText(txtEmertimi6.GetText());
    emer_TextBox.SetText(txtEmertimi6.GetText());                                    
}