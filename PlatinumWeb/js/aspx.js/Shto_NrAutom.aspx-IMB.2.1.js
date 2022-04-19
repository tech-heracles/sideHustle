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
}

var btnFiltrat;

function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, ASPxGridView_Numrat, "128", cmbKonfigurimi.GetText());
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}
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
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, ASPxGridView_Numrat, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, ASPxGridView_Numrat, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, ASPxGridView_Numrat, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, ASPxGridView_Numrat, indexSel);
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
    myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, false, undefined, indexModifiko, pastrofusha, SucceededCallbackKonfig, resultkonf, colKontrollet, aktivizoFusha, colAtrTrupi);
    if (e.item.name == "Ruaj") {
        if (parseInt(gjatesia_TextBox.GetText()) > 100) {
            myMesazh.ShtoMesazhGabimi('Numri automatik nuk mund të ketë gjatësi më të madhe sesa 100 shifra!');
            e.processOnServer = false;
            return;
        }
        merrTeDhena();

    }
}

//merr te dhenat e rreshtit te selektuar
function mbushfusha() {
    mbush = false;
    $('#hfShtimModifikim')[0].value = "modifikim";
    indexModifiko = ASPxGridView_Numrat.GetFocusedRowIndex();
    if (indexModifiko == -1)
        myMesazh.ShtoMesazhGabimi('Duhet te zgjidhni nje numer automatik!');
    else
        ASPxGridView_Numrat.GetRowValues(indexModifiko, 'IdNrAutom;KodiNrAutom;EmertimiNrAutom;FillonNrAutom;MbaronNrAutom;HapiNrAutom;DrejtimiNrAutom;NgaDataNrAutom;DeriMeNrAutom;MajtasNrAutom;DjathtasNrAutom;KategoriaNrAutom;PeriudhaNrAutom;GjatesiaNrAutom;LajmeroPerparaNrFundit;Intervali', OnGetRowValuesMod);

    Utils.shfaqLoadingGif();;
}


//mbush fushat me te dhenat e rreshtit te selektuar
function OnGetRowValuesMod(values) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;
    $('#hfId')[0].value = values[0];
    kodi_TextBox.SetText(values[1]);
    emertimi_TextBox.SetText(values[2]);
    if (values[3] != 0)
        fillon_TextBox.SetText(values[3]);
    else
        fillon_TextBox.SetText('');
    if (values[4] != 0)
        mbaron_TextBox.SetText(values[4]);
    else
        mbaron_TextBox.SetText('');
    hapi_TextBox.SetText(values[5]);
    drejtimi_combobox.SetValue(values[6]);
    nga_data_DateEdit.SetDate(values[7]);
    if (values[8].getYear() != new Date("January 01, 9999").getYear())
        deri_me_DateEdit.SetDate(values[8]);
    else
        deri_me_DateEdit.SetDate();
    majtas_TextBox.SetText(values[9]);
    djathtas_TextBox.SetText(values[10]);

    

    kategoria_combobox.SetValue(values[11]);
    //$.ajax({
    //    url: Utils.getServerApiUrl("Rregjistrime", "KtheTeDhenaLlojPeriudheNew"),
    //    data: JSON.stringify({ kategoria: values[11], selectedIdLlojPeriudhe: values[12] })
    //}).done(SucceededCallbackPeriudhaNew);
    if (values[13] != 0)
        gjatesia_TextBox.SetText(values[13]);
    else
        gjatesia_TextBox.SetText('');
    txtLajmeroPerpara.SetText(values[14]);
    txtIntervali.SetText(values[15]);
    //therret web service per te kontrolluar nese kjo llogari standarte eshte e lidhur apo jo
    //$.ajax({
    //    url: Utils.getServerApiUrl("Konfigurime", "eshteLidhur"),
    //    data: JSON.stringify({ idkomp: "128", kodkonfi: cmbKonfigurimi.GetText(), iddokumenti: values[0], idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: hfState.Get('idGjuha') })
    //}).done(function (result) { SucceededCallbackLidhur(result, values[0]) });
    var klonim = ($("hfShtimModifikim").value === 'klonim');

    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "merrTeDhenaPerNumerAutomatik"),
        data: JSON.stringify({ idkomp: "128", kodkonfi: cmbKonfigurimi.GetText(), iddokumenti: values[0], idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: hfState.Get('idGjuha'), kategoria: values[11], selectedIdLlojPeriudhe: values[12], klonim: klonim})
    }).done(function (result) { SucceededCallbackMerrTeDhenaPerNumerAutomatik(result, values[0]) });


    grid_NrFunditAutomatik.PerformCallback(values[0]);
    if (kaloTab) {
        PageControl.SetActiveTabIndex(1);
        myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, $('#hfShtimModifikim'));
    }

}
function SucceededCallbackPeriudhaNew(colLlojPeriudhe) {
    llojperiudha_combobox.ClearItems();
    for (var i = 0; i < colLlojPeriudhe.colPeriudha.length; i++) {
        idPeriudha = colLlojPeriudhe.colPeriudha[i].IdLlojPeriudhe;
        emriPeriudha = colLlojPeriudhe.colPeriudha[i].LlojPeriudhePershkrimi;
        llojperiudha_combobox.AddItem(emriPeriudha, idPeriudha);
    }

    var idPeriudhaSelected = colLlojPeriudhe.selectedIdLlojPeriudhe;
    if (idPeriudhaSelected != -1)
        Utils.SelectComboItem(llojperiudha_combobox, idPeriudhaSelected);

    else
        llojperiudha_combobox.SetSelectedIndex(0);

}


function SucceededCallbackPeriudha(result) {
    llojperiudha_combobox.ClearItems();
    //  for (i = 0; i < result.colPeriudha; i++) {
    for (var i = 0; i < colLlojPeriudhe.Count; i++) {
        idPeriudha = result.colPeriudha[i].IdLlojPeriudhe;
        emriPeriudha = result.colPeriudha[i].LlojPeriudhePershkrimi;
        llojperiudha_combobox.AddItem(emriPeriudha, idPeriudha);
    }

    var idPeriudhaSelected = vlerat[vlerat.length - 1].split(';')[0];
    if (idPeriudhaSelected != -1)
        llojperiudha_combobox.SetValue(idPeriudhaSelected);
    else
        llojperiudha_combobox.SetSelectedIndex(0);

}

//caktivizon dy fushat e text-it kur ekziston nje numer automatik i fundit
function SuccededCallbackKaNumerTeFunditAutomatik(kaNrFunditAutomatik) {  
    if ($("#hfShtimModifikim").val() !== "modifikim")
        return;

    if (kaNrFunditAutomatik == 0)
        return;

    majtas_TextBox.SetEnabled(false);
    djathtas_TextBox.SetEnabled(false);
    fillon_TextBox.SetEnabled(false);
    llojperiudha_combobox.SetEnabled(false);   
}

function SucceededCallbackMerrTeDhenaPerNumerAutomatik(result, idObjekti) {
    SucceededCallbackLidhur(result.eshteLidhur, idObjekti);
    SucceededCallbackPeriudhaNew(result.KtheTeDhenaLlojPeriudheNew);
    SuccededCallbackKaNumerTeFunditAutomatik(result.kaNrAutomatik);
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


function aktivizoFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $('#hfShtimModifikim');

    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
}

//pastron fushat per shtim dhe ben aktive fushat
function pastrofusha() {
    kodi_TextBox.SetText('');
    emertimi_TextBox.SetText('');
    fillon_TextBox.SetText('');
    mbaron_TextBox.SetText('');    txtIntervali.SetText('');
    hapi_TextBox.SetText('1');
    drejtimi_combobox.SetSelectedIndex(0);
    nga_data_DateEdit.SetDate(new Date());
    deri_me_DateEdit.SetDate();
    majtas_TextBox.SetText('');
    djathtas_TextBox.SetText('');
    kategoria_combobox.SetSelectedIndex(-1);
    llojperiudha_combobox.SetSelectedIndex(-1);
    gjatesia_TextBox.SetText('');
    txtLajmeroPerpara.SetText('0');
    var hf = $('#hfKontrollet')[0];
    aktivizoFusha(colKontrollet, colAtrTrupi, false);
    grid_NrFunditAutomatik.PerformCallback(-1);
}

function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('Shto_NrAutom.aspx', 0, hf);
    myMenu.PercaktoMenuSipasTabit(0, hfTeDrejta, $('#hfShtimModifikim'));
}

function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);

    callWebserviceKonfigurimi("128", cmbKonfigurimi.GetText());
}
function ndryshoKonfiguriminInit() {

    callWebserviceKonfigurimiInit("128", cmbKonfigurimi.GetText());
}

function callWebserviceKonfigurimi(idKomp, kodKonf) {
    var idGjuha = hfState.Get('idGjuha');
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
    ASPxGridView_Numrat.PerformCallback(idKomp + ";" + kodKonf);
}

function callWebserviceKonfigurimiInit(idKomp, kodKonf) {
    var idGjuha = hfState.Get('idGjuha');
    $("#dvNrAutomatik").show();
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
        var hf = $('#hfKontrollet');

        var hfMod = $('#hfShtimModifikim');
        var arrTabela = ['tblKarakteristikat', 'tblAlokimi'];
        //                myFaqeCelje.SucceededCallbackKonfigurimi(result, hf, hfLidhur, hfMod, '', arrTabela, "#ASPxPageControl1_", "ASPxPageControl1_C");
        //myFaqeCelje.SucceededCallbackKonfig(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "#ASPxPageControl1_", "ASPxPageControl1_C");
        //myFaqeCelje.SucceededCallbackKonfigSlim(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "#ASPxPageControl1_", "ASPxPageControl1_C");
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "ASPxPageControl1_C");
    }

 //   $("#dvNrAutomatik").show();//$("#dvNrAutomatik")[0].style.visibility = 'visible';
}
//        function SucceededCallbackKonfigurimi(result) {
//            if (result != "") {
//                resultkonf = result;
//                var vlerat = '';
//                vlerat = result.split('*');
//                var kontrollet = vlerat[0].split(';');

//                var hf = $('#hfKontrollet')[0];
//                var hfLidhur = $("#hfLidhur")[0];
//                var hfMod = $('#hfShtimModifikim')[0];
//                var arrTabela = ['tblKarakteristikat', 'tblAlokimi'];
//                myFaqeCelje.SucceededCallbackKonfigurimi(result, hf, hfLidhur, hfMod, '', arrTabela, "#ASPxPageControl1_", "ASPxPageControl1_C");
//                ASPxGridView_Numrat.PerformCallback("128" + ";" + cmbKonfigurimi.GetText());
//            } $("#dvNrAutomatik")[0].style.visibility = 'visible';
//        }

function kategoriaIndexChange(s) {
    //llojperiudha_combobox.PerformCallback(parseInt(s.GetValue()));

    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "KtheTeDhenaLlojPeriudhe"),
        data: JSON.stringify({ vleratId: s.GetValue() + ';' + "-1" })
    }).done(SucceededCallbackPeriudha);
}

function kategoriaIndexChangeNew(kategoria) {
    var kategoria = kategoria.GetText() == "Shperndarje shpenzimesh" ? 8 : kategoria.GetValue();
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "KtheTeDhenaLlojPeriudheNew"),
        data: JSON.stringify({ kategoria: kategoria, selectedIdLlojPeriudhe: "-1" })
    }).done(SucceededCallbackPeriudhaNew);
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
    //            indexModifiko = myFaqeCelje.EndRequestHandler(sender, args, hf, hfKontrollet, hfShtimModifikim, hfId, indexModifiko, PageControl, ASPxGridView_Numrat, "128")
    indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, ASPxGridView_Numrat, "128", pastrofusha, hfTeDrejta);
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
function enter() {
    if (window.event.keyCode == 13) {
        event.returnValue = false;
        event.cancel = true;
    }
}
var arr = new Array();
function TextChangedVlera(editor, field, key) {
    arr[key] = editor.GetText();
}
function merrTeDhena() {
    for (i = 0; i < grid_NrFunditAutomatik.cpNoRows; i++) {
        arr[i] = Utils.ktheKontroll('txtVlera' + i).GetText();
    }
    $('#hfVlerat').val(JSON.stringify(arr));
}
function PageControl_ActiveTabChanging(s, e) {
}
function Active_TabChanged(s, e) {
    indexModifiko = ASPxGridView_Numrat.GetFocusedRowIndex();    
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