//variabla per te kaluar nga nje reshti i grides tek tjetri
var indexSel = 0;
//indexi i zgjedhur per modifikim
var indexModifiko;
//perdoret per te treguar nese duhet te mbushen tabet apo jo kjo perdoret qe tabet te mbushen vetem heren e pare dhe jo pas cdo here qe  kalohet nga nje tab tek tjetri
var mbush = false;
//perdoret per te daluar rastet kur bejme double click apo thjesht click ne menyre qe ne double click te kaloje tek tabi i dyte
var kaloTab = false;
//perdoret per griden e llogarive ne menyre qe te lodohet vetem ne momentin kur shkon ne tabin e saj
var lista = true;
//perdoret per te kontrolluar pergjigjet e kthyera nga webservices

//perdoren per hapjen e lupes se klientit
var widthLupaKlient = 850;
var heightLupaKlient = 550;
var identikuesPerPopupKlientFurnitori;

//perdoren per konfigurimin e ambjentit
var colKushte, colAlterKusht, colKontrollet, colAtrTrupi;

jQuery(document).ready(function () {
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

function Init() {
    changeName();
}
function clickExport(e) {
    if (gvPajisje.GetSelectedRowCount() == 0) {
        myMesazh.ShtoMesazhGabimi('Duhet te zgjidhni nje nga elementet e listes!');
        e.processOnServer = false;
    }
}
function enter() {
    if (window.event.keyCode == 13) {
        event.returnValue = false;
        event.cancel = true;
    }
}

function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvPajisje, "2030", cmbKonfigurimi.GetText());
}

function gridFocusRowCanged(s, e) {
    mbush = true;
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

function tabsActiveTabChanged(s, e) {
    indexModifiko = gvPajisje.GetFocusedRowIndex();
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

/*
Function: Poshte_click

perdoret per te selektuar rreshtin me poshte

Parameters:

e-eventi
*/
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvPajisje, indexSel);
}

/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvPajisje, indexSel);
}

/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvPajisje, indexSel);
}

/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvPajisje, indexSel);
}

/*
Function: menu_click

perdoret per veprimet e menuse ne javascript

Parameters:

e-eventi
*/
function menu_click(s, e) {
    var hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    var hfId = $('#hfId'); //hidden fieldi qe ruan id  e rreshtit te selektuar
    //            var hfKontrollet = $('#hfKontrollet');
    var ruajbuxhetet = false; //i here per i here
    myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, ruajbuxhetet, undefined, indexModifiko, pastrofusha, vendosKonfig, resultkonf, colKontrollet, aktivFusha, colAtrTrupi);
    if (e.item.name == 'Ruaj') {
        if (!Utils.nrWsRrugesManager.kanePerfunduarWs()) {
            myMesazh.ShtoMesazhGabimi("Po transferohen te dhenat, shypni perseri ruaj pas disa sekondash!");
            e.processOnServer = false;
            return;
        }
    }
}


function mbushfusha() {
    mbush = false;
    $('#hfShtimModifikim').val("modifikim");
    indexModifiko = gvPajisje.GetFocusedRowIndex();
    if (indexModifiko == -1)
        myMesazh.ShtoMesazhGabimi('Duhet te zgjidhni nje pajisje!');
    else {
        gvPajisje.GetRowValues(indexModifiko, 'IdPajisje;UUID;Kodi;Fjalekalimi;Idklienti;Regjistruar;Aktive;KoeficentFitimi;Klienti;KodKlientFurnitor;KoeficentKonsumi', OnGetRowValuesMod);
        Utils.shfaqLoadingGif();;
    }
}

//mbush fushat me te dhenat e rreshtit te selektuar
function OnGetRowValuesMod(values) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;

    $('#hfId').val(values[0]);
   
    txtKodi.SetText(values[1]);
    txtEmertimi.SetText(values[2]);
   
    if (values[4] != null) {
        Utils.SelectComboItem(btneKlienti, values[4], values[9], values[8]);
        btneKlienti.SetEnabled(false);
    }
    else {
        btneKlienti.SetSelectedIndex(-1);
        btneKlienti.SetEnabled(true);
    }
    cbAprovuar.SetChecked(values[5]);
    cbAktiv.SetChecked(values[6]);
    txtKoeficentFitimi.SetText(values[7]);
    txtKoeficentKonsumi.SetText(values[10]);
    //cmbLlojKonvertimesh.SetValue(values[8]);
    //cmbLlojKonvertimesh.SetText(values[11]);
    txtFjalekalimi.SetText('');
    var idGjuha = hfState.Get('idGjuha');
    var idNdermarrje = hfState.Get('idNdermarrje');
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Konfigurime", "eshteLidhur"),
        data: JSON.stringify({ idkomp: "2030", kodkonfi: cmbKonfigurimi.GetText(), iddokumenti: values[0], idNdermarrje: idNdermarrje, idGjuha: idGjuha })
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
    var hfLidhur = $("#hfLidhur");
    hfLidhur.val(result);
    aktivFusha(colKontrollet, colAtrTrupi, eval(result.toLowerCase()));
}

//pastron fushat per shtim dhe ben aktive fushat
function pastrofusha() {
    txtKodi.SetText('');
    txtEmertimi.SetText('');
    txtFjalekalimi.SetText('');
    cbAprovuar.SetChecked(false);
    cbAktiv.SetChecked(false);
    txtKoeficentFitimi.SetText('');
    txtKoeficentKonsumi.SetText('');
    cmbLlojKonvertimesh.SetValue('');
    cmbLlojKonvertimesh.SetText('');
    btneKlienti.SetSelectedIndex(-1);
    $('#hfLidhur').val('False');
    aktivFusha(colKontrollet, colAtrTrupi, false);
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
    gvPajisje.PerformCallback(idKomp + ";" + cmbKonfigurimi.GetText());
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
}

function callWebserviceKonfigurimiInit(idKomp, kodKonf) {
    $("#dvPajisje").show();
    var idGjuha = hfState.Get('idGjuha');
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
}

function SucceededCallbackKonfig(result) {
    vendosKonfig(result);
}

function vendosKonfig(result) {
    // $("#dvAutomjete").show();//$("#dvAutomjete")[0].style.visibility = 'visible';
    if (result != "" && result != null) {
        colKontrollet = result.colKontrollet;
        colAtrTrupi = result.colAtrTrupi;
        var colGrida = result.colGrida;
        colKushte = result.colKushte;
        colAlterKusht = result.colAlterKusht;
        var kodniveli = result.kodniveli;
        var konfLlojRreshti = result.konfLlojRreshti;
        resultkonf = result;
        LupaKontrollet(colKontrollet, colAtrTrupi);
        var hf = $('#hfKontrollet');
        var hfLidhur = $("#hfLidhur");
        var hfMod = $('#hfShtimModifikim');
        var arrTabela = ['tblInformacion'];
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, 'cmbModeli', arrTabela, "ASPxPageControl1_C");
        if (hfMod.val() == "shtim" || hfMod.val() == "klonim") {
            hfNrAuto.Clear();
            hfNrAutoKF.Clear();
            vendosNrAutomatik(colAtrTrupi, colKontrollet);
        }
    }
}


function LupaKontrollet(kontrollet, colAtrTrupi) {
    var hf1 = $("#hfLupaKlienti");
    for (var i = 0; i < kontrollet.length; i++) {
        //id e konfigurimit te lupes vendoset ne hidden field
        //vlera e saj do t'i kalohet si query string hapjes se popUp-it
        if (kontrollet[i].KodKontrolli == "btneKlienti") {
            hf1.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
    }
}

function aktivFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $('#hfShtimModifikim');
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
    if (btneKlienti.GetText() !== "")
        btneKlienti.SetEnabled(false);
    else
        btneKlienti.SetEnabled(true);
}

function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("PershkrimKonfigAmbjente"));
    callWebserviceKonfigurimi(2030, cmbKonfigurimi.GetText());
}

function ndryshoKonfiguriminInit() {
    callWebserviceKonfigurimiInit(2030, cmbKonfigurimi.GetText());
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ? evt.keyCode : evt.charCode;
}

function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('Pajisje.aspx', 0, hf);
    myMenu.PercaktoMenuSipasTabit(0, hfTeDrejta, $("#hfShtimModifikim"));
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
    indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, gvPajisje, "2030", pastrofusha, hfTeDrejta, vendosKonfig, resultkonf);
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

function onNdryshimFokusi() {
    try {
        if (PageControl.GetActiveTabIndex() == 0)
            mbush = true;
    } catch (e) { }
}

function Klienti_Click() {
    var hf = $("#hfLupaKlienti")[0];
    var queryStr = hf.value;
    identikuesPerPopupKlientFurnitori = "ShtoAutomjet";
    myButtonClickLupa.ButtonClickFurnitori(hfState.Get("headerZgjidhKlientFurnitorin"), queryStr, "Klient", widthLupaKlient, heightLupaKlient);
}

function textChangedKlienti(s, e) {
    if (isNaN(btneKlienti.GetValue())) {
        btneKlienti.SetText('');
        btneKlienti.Focus();
        return;
    }
}