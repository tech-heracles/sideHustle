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
    myMenu.aplikoFiltra(s, e, gvKategorite, "447", cmbKonfigurimi.GetText());
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
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvKategorite, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvKategorite, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvKategorite, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvKategorite, indexSel);
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
        Utils.vendosVlereNeHiddenField("cmbKonfigurimeFtpHf", cmbKonfigurimeFtp.GetText());
        merrTeDhenaFushaImporti();
    }
}

//merr te dhenat e rreshtit te selektuar
function mbushfusha() {
    mbush = false;
    $('#hfShtimModifikim').val("modifikim");
    indexModifiko = gvKategorite.GetFocusedRowIndex();
    if (indexModifiko == -1)
        myMesazh.ShtoMesazhGabimi('Duhet te zgjidhni nje serial!');
    else {
        gvKategorite.GetRowValues(indexModifiko, 'ID;Kategori;Pershkrimi;TipFormati;SimboliNdares;kodKonfigurimFtp;MeEmertimKolone', OnGetRowValuesMod);
        Utils.shfaqLoadingGif();
    }
}

//mbush fushat me te dhenat e rreshtit te selektuar
function OnGetRowValuesMod(values) {   
    if ($('#hfShtimModifikim').val() == "shtim")
        return;    
    var id = $('#hfShtimModifikim').val() == "modifikim" ? values[0] : 0;
    $('#hfId').val(id);
    txtKodi.SetText(values[1]);
    if ($('#hfShtimModifikim').val() == "modifikim") {        
        txtKodi.SetEnabled(false);
    }
    txtEmertimi.SetText(values[2]);
    rbTipi.SetValue(values[3]);
    if (values[4] == 'TAB') {
        cbSimboliNdares.SetChecked(true);
        txtSimboliNdares.SetText("");
    }
    else {
        cbSimboliNdares.SetChecked(false);
        txtSimboliNdares.SetText(values[4]);
    }
    
    gvImporti.UnselectAllRowsOnPage();
    gvImporti.PerformCallback(values[0] + ';modifiko');
    if (values[5] != null && values[5] != "")
        cmbKonfigurimeFtp.SetValue(values[5].split(','));
    else cmbKonfigurimeFtp.SetValue(null);
    cbMeEmertimKolone.SetChecked(values[6]);
    var idGjuha = hfState.Get('idGjuha');
    var idNdermarrje = hfState.Get('idNdermarrje');

    //therret web service per te kontrolluar nese kjo llogari standarte eshte e lidhur apo jo
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "eshteLidhur"),
        data: JSON.stringify({ idkomp: "447", kodkonfi: cmbKonfigurimi.GetText(), iddokumenti: id, idNdermarrje: idNdermarrje, idGjuha: idGjuha })
    }).done(function (result) { SucceededCallbackLidhur(result, id) });
    if (kaloTab) {
        PageControl.SetActiveTabIndex(1);
        myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, $('#hfShtimModifikim'));
    }
}

//therret funksionin qe ben enabled dhe disabled fushat sipas lidhjes
function SucceededCallbackLidhur(result, idObjekti) {

    if (idObjekti.toString() !== $('#hfId').val()) {
        Utils.hiqLoadingGif();
        return;
    }
    var hf = $('#hfKontrollet')[0]; //mban te dhenat mbi kontrollet
    var hfLidhur = $("#hfLidhur");
    hfLidhur.val(result);
    //        aktivizoFusha(hf.value);
    aktivizoFusha(colKontrollet, colAtrTrupi, eval(result.toLowerCase()));
}

//pastron fushat per shtim dhe ben aktive fushat
function pastrofusha() {
    txtKodi.SetText('');
    txtEmertimi.SetText('');
    rbTipi.SetSelectedIndex(0);
    txtSimboliNdares.SetText('');
    cbSimboliNdares.SetChecked(false);
    cbMeEmertimKolone.SetChecked(false);
    aktivizoFusha(colKontrollet, colAtrTrupi, false);
    gvImporti.UnselectAllRowsOnPage();
    gvImporti.PerformCallback('0;pastro');
    var hfLidhur = $("#hfLidhur");
    hfLidhur.val(false);
    cmbKonfigurimeFtp.SetValue(null);
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
    gvKategorite.PerformCallback(idKomp + ";" + kodKonf);
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

        var hf = $('#hfKontrollet');
        var hfLidhur = $("#hfLidhur");
        var hfMod = $('#hfShtimModifikim');
        var arrTabela = ['tblKategorite'];
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "ASPxPageControl1_C");
    }
    // $("#dvBurimi").show();
}



function aktivizoFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $('#hfShtimModifikim');
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
}
function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);

    callWebserviceKonfigurimi("447", cmbKonfigurimi.GetText());
}
function ndryshoKonfiguriminInit() {

    callWebserviceKonfigurimiInit("447", cmbKonfigurimi.GetText());
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
		        evt.keyCode : evt.charCode;
}

function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('Shto_KategoriSeriali.aspx', 0, hf);
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
    indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, gvKategorite, "447", pastrofusha, hfTeDrejta);
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

function Active_TabChanged(s, e) {
    indexModifiko = gvKategorite.GetFocusedRowIndex();
    if (mbush) {
        if (indexModifiko != -1) {
            OnGridDoubleClick(indexModifiko);
        }
        else {
            mbush = false;
            $('#hfShtimModifikim').val('shtim');
            $('#hfId').val(0); pastrofusha();
        }
    }
    kaloTab = false;
    myMenu.PercaktoMenuSipasTabit(e.tab.index, hfTeDrejta, $('#hfShtimModifikim'));

}

var arrKodi = new Array();
var editorKodi;
var indexCounter = 0;

function merrTeDhenaFushaImporti() {
    var fushat=[];
    var hf = $("#hfFushat")
    for (i = 0; i < gvImporti.cpImportNoRows; i++) {
        var fusha = {
            ID: gvImporti.GetRowKey(i),
            Fusha: Utils.ktheKontroll('Fusha' + i).GetText(),
            Emertimi: Utils.ktheKontroll('Emertimi' + i).GetText(),
            IsSelected: gvImporti.IsRowSelectedOnPage(i)
        }
        fushat.push(fusha);
    }
    hf.val(JSON.stringify(fushat));
}

function Init() {
    changeName();
    var options = JSON.parse(hfState.Get("colKonfigurimeFtp"));
    hfState.Remove("colKonfigurimeFtp");
    cmbKonfigurimeFtp = new MultiSelect({
        container: "tblKategorite",
        valueField: 'Kodi',
        labelField: 'Kodi',
        searchField: 'Kodi',
        options: options,
        multiSelectId: "cmbKonfigurimeFtp"
    });
}
