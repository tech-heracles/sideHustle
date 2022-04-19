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
var nrWsRruges = 0;

//perdoren per konfigurimin e ambjentit
var colKushte, colAlterKusht, colKontrollet, colAtrTrupi;

jQuery(document).ready(function () {
    $(document).keydown(function (e) {//po
        enter();
        if (e.which == 13) {
            e.preventDefault();
        }
    });
    $(window).on("load", function () {
        //if ($('#hfValid').val() != "valid" && $('#hfValid').val() != "notValid")
        Init();
    });
});

function Init() {
    changeName();
}
function clickExport(e) {
    if (gv_AlphawebEnhancments.GetSelectedRowCount() == 0) {
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
    myMenu.aplikoFiltra(s, e, gv_AlphawebEnhancments, "4008", cmbKonfigurimi.GetText());
}

function gridFocusRowCanged(s, e) {
    mbush = true;
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}


function tabsActiveTabChanged(s, e) {
    indexModifiko = gv_AlphawebEnhancments.GetFocusedRowIndex();
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
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gv_AlphawebEnhancments, indexSel);
}

/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gv_AlphawebEnhancments, indexSel);
}

/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gv_AlphawebEnhancments, indexSel);
}

/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gv_AlphawebEnhancments, indexSel);
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
    //var hfKontrollet = $('#hfKontrollet');
    var ruajbuxhetet = false; //i here per i here

    myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, ruajbuxhetet, undefined, -1, pastrofusha, vendosKonfig, resultkonf, colKontrollet, aktivFusha, colAtrTrupi);

    if (e.item.name == 'Ruaj') {
        if (nrWsRruges !== 0) {
            myMesazh.ShtoMesazhGabimi("Po transferohen te dhenat, shypni perseri ruaj pas disa sekondash!");
            e.processOnServer = false;
            return;
        }
    }

    else if (e.item.name == 'GoldenNumbers') {
        PageControl.GetTab(1).SetVisible(true);
        PageControl.GetTab(0).SetVisible(false);
        PageControl.GetTab(1).SetText("Golden Number");
        PageControl.SetActiveTabIndex(1);
        if (ASPxMenu1.GetItemByName('Fshi') != null)
            ASPxMenu1.GetItemByName('Fshi').SetVisible(false);
        lblMsisdnERe.SetVisible(false);
        txtMsisdnERe.SetVisible(false);
        $('#hfLlojNumri').val("1");
        e.processOnServer = false;
        return;
    }

    else if (e.item.name == 'NormalNumbers') {
        PageControl.GetTab(1).SetVisible(true);
        PageControl.GetTab(0).SetVisible(false);
        PageControl.GetTab(1).SetText("Normal Number");
        PageControl.SetActiveTabIndex(1);
        if (ASPxMenu1.GetItemByName('Fshi') != null)
            ASPxMenu1.GetItemByName('Fshi').SetVisible(false);
        lblMsisdnERe.SetVisible(false);
        txtMsisdnERe.SetVisible(false);
        $('#hfLlojNumri').val("2");
        e.processOnServer = false;
        return;
    }

    else if (e.item.name == 'Anullo') {
        PageControl.GetTab(0).SetVisible(true);
        PageControl.GetTab(1).SetVisible(false);
        PageControl.SetActiveTabIndex(0);
        if (ASPxMenu1.GetItemByName('Fshi') != null)
            ASPxMenu1.GetItemByName('Fshi').SetVisible(true);
        myMenu.PercaktoMenuSipasTabit(0, hfTeDrejta, $('#hfShtimModifikim'));
        e.processOnServer = false;
        return;
    }

    else if (e.item.name == 'Pastro') {
        pastrofusha();
        e.processOnServer = false;
        return;
    }

    else if (e.item.name === 'Fshi') {
        e.processOnServer = false;

        if (gv_AlphawebEnhancments.GetSelectedRowCount() == 0) {
            myMesazh.ShtoMesazhGabimi("Ju lutem zgjidhni nje rresht!");
            return;
        }
        lblMsgbox.SetText("Ju keni zgjedhur #X rresht/a. Jeni i sigurt?".replace("#X", gv_AlphawebEnhancments.GetSelectedRowCount()));
        popFshi.Show();
    }
}

function mbushfusha() {
    //mbush = false;
    //$('#hfShtimModifikim').val("modifikim");
    //indexModifiko = gv_AlphawebEnhancments.GetFocusedRowIndex();
    //if (indexModifiko == -1)
    //    myMesazh.ShtoMesazhGabimi('Duhet te zgjidhni nje rresht!');
    //else
    //    gv_AlphawebEnhancments.GetRowValues(indexModifiko, 'IdAutomjeti;NrShasie;Targa;ModelAutomjeti;PershkrimModeli;VitProdhimi;Kilometra;KodMotorri;IdStatusDok;IdKlienti;Klienti;IdNdermarrje;IdKrijues;IdPerdorues;DtKrijimi;DtModifikimi;KodKlientFurnitor;KodModeli', OnGetRowValuesMod); LoadingPanel.Show();
}

//mbush fushat me te dhenat e rreshtit te selektuar
function OnGetRowValuesMod(values) {
    $('#hfId').val(values[0]);
    if ($('#hfShtimModifikim').val() == "modifikim") {
        //txtTarga.SetEnabled(false);
        txtNrShasie.SetEnabled(false);
    }
    txtNrShasie.SetText(values[1]);
    txtTarga.SetText(values[2]);

    txtVitProdhimi.SetText(values[5]);
    txtKilometra.SetText(values[6]);
    txtKodMotorri.SetText(values[7]);
    if (values[9] != null) {
        Utils.SelectComboItem(btneKlienti, values[9], values[10], values[16]);
    }
    else
        btneKlienti.SetSelectedIndex(-1);

    PlatinumWeb.wsfunc.eshteLidhur("4008", cmbKonfigurimi.GetText(), values[0], SucceededCallbackLidhur, myWS.webServiceFail);
    nrWsRruges++;

    if (kaloTab) {
        PageControl.SetActiveTabIndex(1);
        if (ASPxMenu1.GetItemByName('Fshi') != null)
            ASPxMenu1.GetItemByName('Fshi').SetVisible(false);
        myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, $('#hfShtimModifikim'));
    }
}

//therret funksionin qe ben enabled dhe disabled fushat sipas lidhjes
function SucceededCallbackLidhur(result) {
    nrWsRruges--;
    window.parent.SessionTimeout.sendKeepAlive();
    var hf = $('#hfKontrollet')[0]; //mban te dhenat mbi kontrollet
    var hfLidhur = $("#hfLidhur");
    hfLidhur.val(result);
    aktivFusha(colKontrollet, colAtrTrupi, eval(result.toLowerCase()));
}

//pastron fushat per shtim dhe ben aktive fushat
function pastrofusha() {
    var today = new Date();
    dtDokPromocionMsisdn.SetValue(today);
    dtDokPromocionMsisdn.SetEnabled(false);
    cmbDegeAdmin.SetEnabled(false);
    
    txtKodiFitues.SetText('');
    txtMsisdn.SetText('3556');
    txtMsisdnERe.SetText('3556');
    lblMsisdnERe.SetVisible(false);
    txtMsisdnERe.SetVisible(false);

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
    gv_AlphawebEnhancments.PerformCallback(idKomp + ";" + cmbKonfigurimi.GetText());
    PlatinumWeb.wsfunc.ktheKonfig(idKomp, kodKonf, SucceededCallbackKonfig, myWS.webServiceFail);
    nrWsRruges++;
}

function callWebserviceKonfigurimiInit(idKomp, kodKonf) {

    //$("#dvAlphawebEnhancment").show();
    var idGjuha = hfState.Get('idGjuha');
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);

    //PlatinumWeb.wsfunc.ktheKonfig(idKomp, kodKonf, SucceededCallbackKonfig, myWS.webServiceFail);
    nrWsRruges++;
}

function SucceededCallbackKonfig(result) {
    nrWsRruges--;
    window.parent.SessionTimeout.sendKeepAlive();
    PageControl.GetTab(1).SetVisible(false);
    vendosKonfig(result);
}

function vendosKonfig(result) {
    $("#dvAlphawebEnhancment")[0].style.visibility = 'visible';
    if (result != "" && result != null) {
        colKontrollet = result.colKontrollet;
        colAtrTrupi = result.colAtrTrupi;
        var colGrida = result.colGrida;
        colKushte = result.colKushte;
        colAlterKusht = result.colAlterKusht;
        var kodniveli = result.kodniveli;
        var konfLlojRreshti = result.konfLlojRreshti;
        resultkonf = result;
        var hf = $('#hfKontrollet');
        var hfLidhur = $("#hfLidhur");
        var hfMod = $('#hfShtimModifikim');
        var arrTabela = ['tblInformacion'];
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, 'cmbModeli', arrTabela, "ASPxPageControl1_C");
        txtMsisdn.SetText('3556');
        txtMsisdnERe.SetText('3556');
        lblMsisdnERe.SetVisible(false);
        txtMsisdnERe.SetVisible(false);
        if (PageControl.GetActiveTabIndex() == 1 && ASPxMenu1.GetItemByName('Fshi') != null)
            ASPxMenu1.GetItemByName('Fshi').SetVisible(false);
        else if (PageControl.GetActiveTabIndex() == 0 && ASPxMenu1.GetItemByName('Fshi') != null)
            ASPxMenu1.GetItemByName('Fshi').SetVisible(true);
    }
}

function aktivFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $('#hfShtimModifikim');
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
}

function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("PershkrimKonfigAmbjente"));
    callWebserviceKonfigurimi(4008, cmbKonfigurimi.GetText());
}

function ndryshoKonfiguriminInit() {
    callWebserviceKonfigurimiInit(4008, cmbKonfigurimi.GetText());
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ? evt.keyCode : evt.charCode;
}


function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('Shto_AlphawebEnhancments.aspx', 0, hf);
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
    indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, gv_AlphawebEnhancments, "4008", pastrofusha, hfTeDrejta, vendosKonfig, resultkonf);
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

function RidergoKodin(s, e) {
    e.processOnServer = false;
    if (txtMsisdn.GetText() == "") {
        myMesazh.ShtoMesazhGabimi("Ju lutem vendosni MSISDN-ne e klientit per te riderguar mesazhin!");
        return;

    }
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("OTC", "DergoKodMeSms"), data: JSON.stringify({ msisdn: txtMsisdn.GetText() })
    }).done(function (result) {
        if (result.StatusMesazhi)
            myMesazh.ShtoMesazhSuksesi(result.PershkrimMesazhi);
        else
            myMesazh.ShtoMesazhGabimi(result.PershkrimMesazhi)
    });
}

function ValidoKodinFitues(s, e) {
    e.processOnServer = false;
    if ($('#hfLlojNumri').val() == "1")//golden
    {
        if (txtMsisdn.GetText() == "" || txtMsisdn.GetText() == null) {
            $('#hfValid').val("notValid");
            myMesazh.ShtoMesazhGabimi("Ju lutem vendosni MSISDN-ne e klientit per te bere validimin!");
        }
        else if (txtKodiFitues.GetText() == "" || txtKodiFitues.GetText() == null)
            myMesazh.ShtoMesazhGabimi("Ju lutem vendosni kodin fitues te klientit per te bere validimin!");

        else {

            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("OTC", "ValidoGoldenMSISDN"), data: JSON.stringify({ msisdn: txtMsisdn.GetText(), kodiFitues: txtKodiFitues.GetText(), lloji: 1 })
            }).done(SucceededCallbackValidimKodFitues);
        }
    }
    else if ($('#hfLlojNumri').val() == "2")//normal
    {
        if (txtKodiFitues.GetText() == "" || txtKodiFitues.GetText() == null)
            myMesazh.ShtoMesazhGabimi("Ju lutem vendosni kodin fitues te klientit per te bere validimin!");

        else {

            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("OTC", "ValidoGoldenMSISDN"), data: JSON.stringify({ msisdn: "", kodiFitues: txtKodiFitues.GetText(), lloji: 2 })
            }).done(SucceededCallbackValidimKodFitues);
        }

    }
}

function SucceededCallbackValidimKodFitues(result) {
    var status = result[0];
    var mesazh = result[1];

    if (!status)
        myMesazh.ShtoMesazhGabimi(mesazh);

    else {
        lblMsisdnERe.SetVisible(true);
        txtMsisdnERe.SetVisible(true);
        txtMsisdn.SetEnabled(false);
        txtKodiFitues.SetEnabled(false);
        $('#hfValid').val("valid");
        myMesazh.ShtoMesazhSuksesi(mesazh);
    }
}
