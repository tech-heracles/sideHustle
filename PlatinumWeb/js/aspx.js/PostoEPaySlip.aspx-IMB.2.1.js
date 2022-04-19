;
//variabla per te kaluar nga nje reshti i grides tek tjetri
var indexSel = 0;
//indexi i zgjedhur per modifikim
var indexModifiko;
//perdoret per te treguar nese duhet te mbushen tabet apo jo kjo perdoret qe tabet te mbushen vetem heren e pare dhe jo pas cdo here qe  kalohet nga nje tab tek tjetri
var mbush = true;
//perdoret per te daluar rastet kur bejme double click apo thjesht click ne menyre qe ne double click te kaloje tek tabi i dyte
var kaloTab = false;
//perdoret per griden e periudhave qe te mbushet sipas ndryshimeve
var lista = true;
//metodat per te hapur faqen e modifikimit me double click
function checkText(s, e) {
    myMenu.checkText(s, e);
} var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, ASPxGridView_Vitet, "3032", cmbKonfigurimi.GetText());
}
function textChanged(s, e) {
    myMenu.textChanged(s, e);
}
function OnGridDoubleClick(index) {
    indexModifiko = index;
    lista = true;
    mbushfusha();
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
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, ASPxGridView_Vitet, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, ASPxGridView_Vitet, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, ASPxGridView_Vitet, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, ASPxGridView_Vitet, indexSel);
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
    if (e.item.name == 'Ruaj') {
       // if (isValidViti()) {
            merrTeDhena();
      //  }
        //else {
        //    e.processOnServer = false;
        //}
    }
    //else if (e.item.name == "MbyllVitin") {
    //    popMbyllje.Show();
    //    e.processOnServer = false;
    //}
}

/*
Kontrollon nese jane plotesuar skate data e fillimit dhe e mbarimit te vitit.
*/
function isValidViti() {
    //var dataFillimit = dteFillimiViti.GetDate();
    //var dataMbarimit = dteMbarimiViti.GetDate();
    //if (parseInt(dataFillimit.getDate()) != 1) {
    //    myMesazh.ShtoMesazhGabimi(hfState.Get("msgVitetDitaEFillimitTeVititDuhetTeJeteEParaEMuajitTeZgjedhur"));
    //    return false;
    //}

    //var muajiFillimit = parseInt(dataFillimit.getMonth() + 1);
    //var dtSkateMabarimit;
    //if (muajiFillimit == 1) {
    //    dtSkateMabarimit = new Date(dataFillimit.getFullYear(), 11, 31)
    //}
    //else {
    //    //Duke vendosur ne konstruktor vleren 0 per diten, kthehet dita e fundit e muajit
    //    dtSkateMabarimit = new Date(dataFillimit.getYear() + 1, muajiFillimit - 1, 0);
    //}
    //if ((dtSkateMabarimit.getTime() - dataMbarimit.getTime()) != 0) {
    //    myMesazh.ShtoMesazhGabimi(hfState.Get("msgVitetVitiDuhetTeJeteIPlote"));
    //    return false;
    //}
    //return true;
}

//merr te dhenat e rreshtit te selektuar
function mbushfusha() {
    mbush = false;
    $('#hfShtimModifikim')[0].value = "modifikim";
    indexModifiko = ASPxGridView_Vitet.GetFocusedRowIndex();
    if (indexModifiko == -1)
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgVitetDuhetTeZgjidhni1Vit"));
    else
        ASPxGridView_Vitet.GetRowValues(indexModifiko, 'IdViti;KodiViti;FillimiViti;MbarimiViti;PeriudhaLloji;PeriudhaHapjes;PeriudhaMbylljes;IdLlogMbylljeViti;MbyllurMe', OnGetRowValuesMod); Utils.shfaqLoadingGif();;
}
//mbush fushat me te dhenat e rreshtit te selektuar
function OnGetRowValuesMod(values) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;
    $('#hfId')[0].value = values[0];
   // txtKodi.SetText(values[1]);
    //dteFillimiViti.SetDate(values[2]);
    //dteMbarimiViti.SetDate(values[3]);
    //cmbPeriudhaLloji.SetValue(values[4]);
    //cbPeriudhaHapjes.SetChecked(values[5]);
    //cbPeriudhaMbylljes.SetChecked(values[6]);
    //txtLlogMbylljeViti.SetValue(values[7]);
    //    lblDateMbyllurMe.SetText(values[8].format('dd/MM/yyyy')); else lblDateMbyllurMe.SetText('');
    gvPeriudha.PerformCallback(indexModifiko);
    lista = false;
    var idGjuha = hfState.Get('idGjuha');
    var idNdermarrje = hfState.Get('idNdermarrje');
    //therret web service per te kontrolluar nese kjo llogari standarte eshte e lidhur apo jo
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "eshteLidhur"),
        data: JSON.stringify({ idkomp: "3032", kodkonfi: cmbKonfigurimi.GetText(), iddokumenti: values[0], idNdermarrje: idNdermarrje, idGjuha: idGjuha })
    }).done(function (result) { SucceededCallbackLidhur(result, values[0]) });
    if (kaloTab) {
    //    ASPxMenu1.GetItemByName('MbyllVitin').SetVisible(true);
        PageControl.SetActiveTabIndex(1);
        myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, $('#hfShtimModifikim'));
    }
}
function SucceededCallbackLlog(result) {
    txtLlogMbylljeViti.SetText(result);
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
function pastrofusha() {// eshte bere koment sepse per momentin nuk lejohet shtimi i viteve
 //   txtLlogMbylljeViti.SetValue('');
    //lblDateMbyllurMe.SetText('');
   // txtKodi.SetText('');
    //dteFillimiViti.SetText('');
    //dteMbarimiViti.SetText('');
    //cmbPeriudhaLloji.SetText('1 mujore');
    //cmbPeriudhaLloji.SetSelectedIndex(0);
  //  cbPeriudhaHapjes.SetChecked(false);
   // cbPeriudhaMbylljes.SetChecked(false);
    //var hf = $('#hfKontrollet')[0];
    //aktivizoFusha(colKontrollet, colAtrTrupi, false);
    //gvPeriudha.PerformCallback(-1);
    //lista = false;

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
    ASPxGridView_Vitet.PerformCallback(idKomp + ";" + kodKonf);
}

function callWebserviceKonfigurimiInit(idKomp, kodKonf) {
    var idGjuha = hfState.Get('idGjuha');
    $("#dvViti").show();
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
        var arrTabela = ['tblViti'];
        //myFaqeCelje.SucceededCallbackKonfig(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "#ASPxPageControl1_", "ASPxPageControl1_C");
        //myFaqeCelje.SucceededCallbackKonfigSlim(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "#ASPxPageControl1_", "ASPxPageControl1_C");
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "ASPxPageControl1_C");
    }
  //  $("#dvViti").show();//$("#dvViti")[0].style.visibility = 'visible';
}
//    function SucceededCallbackKonfigurimi(result) {
//        if (result !== "") {
//            resultkonf = result; var vlerat = '';
//            vlerat = result.split('*');
//            var kontrollet = vlerat[0].split(';');
//            var hf = $('#hfKontrollet')[0];
//            var hfLidhur = $("#hfLidhur")[0];
//            var hfMod = $('#hfShtimModifikim')[0];
//            var arrTabela = ['tblViti'];
//            Lupa(kontrollet);
//            myFaqeCelje.SucceededCallbackKonfigurimi(result, hf, hfLidhur, hfMod, '', arrTabela, "#ASPxPageControl1_", "ASPxPageControl1_C");
//            ASPxGridView_Vitet.PerformCallback("3032" + ";" + cmbKonfigurimi.GetText());
//        } $("#dvViti")[0].style.visibility = 'visible';
//    }

var identikuesPerPopupLlogari = "Vitet";
function LupaKontrollet(kontrollet, colAtrTrupi) {
    var hf1 = $("#hfLupaLlogari");
    for (var i = 0; i < kontrollet.length; i++) {
        if (kontrollet[i].KodKontrolli == "txtLlogMbylljeViti")
            hf1.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
    }
}
function llogari_TextChanged(s, e) {
    var s = txtLlogMbylljeViti.GetText().split(';');
    txtLlogMbylljeViti.SetText(s[0]);

}
function aktivizoFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $('#hfShtimModifikim');
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
}
function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);

    callWebserviceKonfigurimi("3032", cmbKonfigurimi.GetText());
}
function ndryshoKonfiguriminInit() {
    callWebserviceKonfigurimiInit("3032", cmbKonfigurimi.GetText());
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
		        evt.keyCode : evt.charCode;
}

function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('PostoEPaySlip.aspx', 0, hf);
 //   ASPxMenu1.GetItemByName('MbyllVitin').SetVisible(false);
    myMenu.PercaktoMenuSipasTabit(0, hfTeDrejta, $('#hfShtimModifikim'));
}

/*
Function: EndRequestHandler
    
Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe array-t e perdorura.
Shiko funksionet  <ndryshoKonfigurimin>.
*/
function EndRequestHandler(sender, args) {
    var hf = $("#hfStatusi")[0];
    var hfKontrollet = $('#hfKontrollet')[0];
    var hfShtimModifikim = $('#hfShtimModifikim')[0]; //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    var hfId = $('#hfId')[0];  //hidden fieldi qe ruan id  e rreshtit te selektuar
    //  indexModifiko = myFaqeCelje.EndRequestHandler(sender, args, hf, hfKontrollet, hfShtimModifikim, hfId, indexModifiko, PageControl, ASPxGridView_Vitet, "3032")
    if (hf.value == "true") {
        //              aktivizoFusha(hfKontrollet.value);
        //              window.mbush = false;
        //               hfShtimModifikim.value = "shtim"; //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
        //               hfId.value = 0; //hidden fieldi qe ruan id  e rreshtit te selektuar
        //               indexModifiko = -1; //indexi i reshtit te selektuar      
        //              pastrofusha();
        hf.value = "false";

        if (PageControl.GetActiveTabIndex() != 0)
        { 
          //  ASPxMenu1.GetItemByName('MbyllVitin').SetVisible(true);
            PageControl.SetActiveTabIndex(0); myMenu.PercaktoMenuSipasTabit(0, hfTeDrejta, $('#hfShtimModifikim'));
        }
        //  SucceededCallbackKonfigurimiInit(resultkonf);
        mbushfusha();
        ASPxGridView_Vitet.PerformCallback(3032 + ";" + cmbKonfigurimi.GetText());
        ASPxGridView_Vitet.ClearFilter();
    }
    else myMenu.PercaktoMenuSipasTabit(PageControl.GetActiveTabIndex(), hfTeDrejta, $('#hfShtimModifikim'));
    Utils.hiqLoadingGif();;


}
function valido(s, e) {
    myFaqeCelje.valido(s, e, PageControl, hfTeDrejta, $('#hfShtimModifikim'));
}

//function krijoTable(rresht, kolone, emerTabele) {
//    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
//}
var modifikim = false;
var kycje = new Array();
var posto = new Array();
var postom = new Array();
var postoa = new Array();
var count = 0;
var rreshta = 14;
function ShtoKycje(editor, key, counti) {
    rreshta = counti
} 
function ShtoPostoEPaySlip(editor, key, counti) {
    rreshta = counti
}
function merrTeDhena() {
    var hf = $('#hfKycje')[0]
    var hf1 = $('#hfposto')[0]
    var hf2 = $('#hfpostom')[0]
    var hf3 = $('#hfpostoa')[0]

    for (i = 0; i < rreshta; i++) {
        editor = Utils.ktheKontroll('kycur' + i);
        editor1 = Utils.ktheKontroll('posto' + i);
        editor2 = Utils.ktheKontroll('postom' + i);
        editor3 = Utils.ktheKontroll('postoa' + i);
        kycje[count] = i.toString() + ":" + editor.GetChecked();
        posto[count] = i.toString() + ":" + editor1.GetChecked();
        postom[count] = i.toString() + ":" + editor2.GetChecked();
        postoa[count] = i.toString() + ":" + editor3.GetChecked();
        //count += 1;
        count = count + 1;
    }

    count = 0;

    hf.value = kycje;
    hf1.value = posto;
    hf2.value = postom;
    hf3.value = postoa;
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
var widthLupaLlogaria = 600, heightLupaLlogaria = 600;
function Llogari_Click() {//thirret popup i llogarive
    var hf = $("#hfLupaLlogari")[0];
    var queryStr = hf.value;
    myButtonClickLupa.ButtonClickLlogaria(hfState.Get("headerPopUpText"), queryStr, widthLupaLlogaria, heightLupaLlogaria);
}
