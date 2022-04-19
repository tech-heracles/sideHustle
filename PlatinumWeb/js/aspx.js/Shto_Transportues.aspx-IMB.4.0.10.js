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
                break;
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
    myMenu.aplikoFiltra(s, e, ASPxGridView_Transportues, "913", cmbKonfigurimi.GetText());
}

function gridFocusRowCanged(s, e) {
    mbush = true;
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

//metodat per te hapur faqen e modifikimit me double click
function OnGridDoubleClick(index) {
    if (!hfState.Get("lupe")) {
        indexModifiko = index;
        lista = true;
        mbushfusha();
    }
    else {
        ZgjidhRreshtaNgaLupa();
    }
}

function tabsActiveTabChanged(s, e) {
    indexModifiko = ASPxGridView_Transportues.GetFocusedRowIndex();
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
    aktivTab(1, true);
    aktivTab(2, true);
    myMenu.PercaktoMenuSipasTabit(e.tab.index, hfTeDrejta, $('#hfShtimModifikim'));
}

/*
Function: Poshte_click

perdoret per te selektuar rreshtin me poshte

Parameters:

e-eventi
*/
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, ASPxGridView_Transportues, indexSel);
}

/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, ASPxGridView_Transportues, indexSel);
}

/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, ASPxGridView_Transportues, indexSel);
}

/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, ASPxGridView_Transportues, indexSel);
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
    
    var ruajbuxhetet = false; //i here per i here
    myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, ruajbuxhetet, undefined, indexModifiko, pastrofusha, vendosKonfig, resultkonf, colKontrollet, aktivFusha, colAtrTrupi);
    switch(e.item.name){
        case "Ruaj":
            if (!Utils.nrWsRrugesManager.kanePerfunduarWs()) {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgPoTransferohetTeDhenatShtypniPerseriRuaj"));
                e.processOnServer = false;
                return;
            }
            break;
        case "OK":
            e.processOnServer = false;
            ZgjidhRreshtaNgaLupa();
            break;
        default:
            break;
    }
}

function ZgjidhRreshtaNgaLupa() {
    ASPxGridView_Transportues.GetSelectedFieldValues('IdTransportues;Emertimi;Nipt;Adresa;Tel;IdStatusDok;IdKrijues;IdNdermarrje;IdPerdorues;DtKrijimi;DtModifikimi;Targa;Aktiv;Lloji', OnGridSelectionComplete);
}

function OnGridSelectionComplete(value) {
    if (value.length == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniNjeTransportues"));
        return;
    }
    hide = true;
    var emri = ''; var nipt = ''; var id = ''; var adresa = ''; var tel = ''; aktiv = true;
    if (value.length > 1) {
        for (i = 0; i < value.length - 1; i++) {
            var values = value[i];

            id = id + values[0] + ",";
        }
        values = value[value.length - 1];

        id = id + values[0];
    }
    else {

        values = value[0];
        id = values[0];
        emri = values[1];
        nipt = values[2];
        adresa = values[3];
        tel = values[4];
        aktiv = values[5];
    }
    switch (window.parent.identikuesPerPopupTransportuesi) {

        case "RegjistrimDokumentash":
            if (value.length > 1) {
                myMesazh.ShtoMesazhGabimi("Nuk mund te zgjidhni me shume se nje rresht!");
                hide = false;
            }
            else if (values[13] == "Operator") {
                myMesazh.ShtoMesazhGabimi("Keni zgjedhur Operator, duhet te zgjidhni nje Transportues!");
                hide = false;
            }
            else {
                if (window.parent.btnTransportues.GetText() != '') {
                    window.parent.btnTransportues.SetText('');
                    window.parent.btnTransportues.SetSelectedIndex(-1);
                }
                window.parent.btnTransportues.SetValue(values[0]);
                window.parent.btnTransportues.SetText(values[1]);
                window.parent.btnTransportues.Focus(true);
            }
            break;
        case "RegjistrimMagazine":
            if (value.length > 1) {
                myMesazh.ShtoMesazhGabimi("Nuk mund te zgjidhni me shume se nje rresht!");
                hide = false;
            }
            else if (values[13] == "Operator") {
                myMesazh.ShtoMesazhGabimi("Keni zgjedhur Operator, duhet te zgjidhni nje Transportues!");
                hide = false;
            }
            else {
                if (window.parent.btnTransportuesi.GetText() != '') {
                    window.parent.btnTransportuesi.SetText('');
                    window.parent.btnTransportuesi.SetSelectedIndex(-1);
                }
                window.parent.btnTransportuesi.SetValue(values[0]);
                window.parent.btnTransportuesi.SetText(values[1]);
                window.parent.btnTransportuesi.Focus(true);
                window.parent.txtTarga2.SetText(values[11]);

            }
            break;
        case "Import":
            window.parent.editorGlobal.SetText(values[1]);
            window.parent.editorGlobal.SetFocus(true);
            break;
        default:
            break;
    }
    if (hide) {
        window.parent.popupUniversal.Hide();
    }
}

function mbushfusha() {
    mbush = false;
    $('#hfShtimModifikim').val("modifikim");
    indexModifiko = ASPxGridView_Transportues.GetFocusedRowIndex();
    if (indexModifiko == -1)
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniNjeTransportues"));
    else
        ASPxGridView_Transportues.GetRowValues(indexModifiko, 'IdTransportues;Emertimi;Nipt;Adresa;Tel;IdStatusDok;IdKrijues;IdNdermarrje;IdPerdorues;DtKrijimi;DtModifikimi;Targa;Aktiv;Kodi;Emri;Mbiemri;Lloji', OnGetRowValuesMod);
}

//mbush fushat me te dhenat e rreshtit te selektuar
function OnGetRowValuesMod(values) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;

    var tabIndex = values[16] == 'Transportues' ? 1 : 2;
    $('#hfId').val(values[0]);
    txtTransportues.SetText(values[1]);
    txtNIPTTransp.SetText(values[2]);
    txtAdresaTransp.SetText(values[3]);
    txtTelTransp.SetText(values[4]);
    txtTarga.SetText(values[11]);
    cbAktiv.SetChecked(values[12]);
    txtKodiOp.SetText(values[13]);
    txtEmriOp.SetText(values[14]);
    txtMbiemriOp.SetText(values[15]);
    cbAktivOp.SetChecked(values[12]);
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Konfigurime", "ktheTipiIdTransportuesi"),
        data: JSON.stringify({ idTransportues: values[0]})
    }).done(function (result) {
        SucceededCallbackTipiId(result);
    });
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Konfigurime", "eshteLidhur"),
        data: JSON.stringify({ idkomp: "913", kodkonfi: cmbKonfigurimi.GetText(), iddokumenti: values[0], idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: hfState.Get('idGjuha') })
    }).done(function (result) {
        SucceededCallbackLidhur(result, values[0]);
    });

    if (kaloTab) {
        PageControl.SetActiveTabIndex(tabIndex);
        myMenu.PercaktoMenuSipasTabit(tabIndex, hfTeDrejta, $('#hfShtimModifikim'));
        aktivTab(tabIndex == 1 ? 2 : 1, false);
    }
}

function kodiOpLostFocus(s, e) {
    aktivTab(1, false);
}

function transportuesLostFocus(s, e) {
    aktivTab(2, false);
}

function aktivTab(tabIndex, isEnabled) {
    PageControl.tabs[tabIndex].SetEnabled(isEnabled);
    PageControl.tabs[tabIndex].clientVisible = isEnabled;
}

//therret funksionin qe ben enabled dhe disabled fushat sipas lidhjes
function SucceededCallbackLidhur(result, idObjekti) {
   
    if (idObjekti.toString() !== $('#hfId').val()) {
        Utils.hiqLoadingGif();
        return;
    }
    $("#hfLidhur").val(result);
    aktivFusha(colKontrollet, colAtrTrupi, eval(result.toLowerCase()));

    if ($('#hfShtimModifikim').val() == "modifikim") {
        txtTransportues.SetEnabled(false);
        txtKodiOp.SetEnabled(false);
    }
}
function SucceededCallbackTipiId(result) {

    cmbTipiId.SetText(result);
}

//pastron fushat per shtim dhe ben aktive fushat
function pastrofusha() {
    
    txtTransportues.SetText('');
    txtNIPTTransp.SetText('');
    txtAdresaTransp.SetText('');
    txtTelTransp.SetText('');
    txtTarga.SetText('');
    cbAktiv.SetChecked(true);
    txtKodiOp.SetText('');
    txtKodiOp.SetText('');
    txtEmriOp.SetText('');
    txtMbiemriOp.SetText('');
    cbAktivOp.SetChecked(true);
    $('#hfLidhur').val('False');
    aktivFusha(colKontrollet, colAtrTrupi, false);
    aktivTab(1, true);
    aktivTab(2, true);
    cmbTipiId.SetText("NUIS");
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
    
    ASPxGridView_Transportues.PerformCallback(idKomp + ";" + cmbKonfigurimi.GetText());
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: hfState.Get('idGjuha') })
    }).done(SucceededCallbackKonfig);
}

function callWebserviceKonfigurimiInit(idKomp, kodKonf) {

    $("#dvTransportues").show();
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: hfState.Get('idGjuha') })
    }).done(SucceededCallbackKonfig);
}

function SucceededCallbackKonfig(result) {
    vendosKonfig(result);
}

function vendosKonfig(result) {
    //$("#dvTransportues").show();
    //$("#dvTransportues")[0].style.visibility = 'visible';
    if (result != "" && result != null) {
        colKontrollet = result.colKontrollet;
        colAtrTrupi = result.colAtrTrupi;
        colKushte = result.colKushte;
        colAlterKusht = result.colAlterKusht;
        resultkonf = result;
        var hf = $('#hfKontrollet');
        var hfMod = $('#hfShtimModifikim');
        var arrTabela = ['tblTransportues', 'tblOperatore'];
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, 'cmbModeli', arrTabela, "ASPxPageControl1_C");
    }
}

function aktivFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $('#hfShtimModifikim');
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
}

function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("PershkrimKonfigAmbjente"));
    callWebserviceKonfigurimi(913, cmbKonfigurimi.GetText());
}

function ndryshoKonfiguriminInit() {
   // $("#dvTransportues").show();
    callWebserviceKonfigurimiInit(913, cmbKonfigurimi.GetText());
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ? evt.keyCode : evt.charCode;
}

function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('Shto_Transportues.aspx',0, hf);
    myMenu.PercaktoMenuSipasTabit(0, hfTeDrejta, $("#hfShtimModifikim"));
}

/*
Function: EndRequestHandler
    
Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe array-t e perdorura.
Shiko funksionet  <ndryshoKonfigurimin>.
*/
function EndRequestHandler(sender, args) {
    var hf = $("#hfStatusi");
    var hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    var hfId = $('#hfId');  //hidden fieldi qe ruan id  e rreshtit te selektuar
    var hfTransportuesFshire = $('#hfTransportuesFshire').val();

    if (hfTransportuesFshire != 0){
        transportuesFshire = window.parent.btnTransportues.FindItemByValue(hfTransportuesFshire);
        window.parent.btnTransportues.RemoveItem(transportuesFshire.index);
    }
    indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, ASPxGridView_Transportues, "913", pastrofusha, hfTeDrejta, vendosKonfig, resultkonf);
    pastrofusha();
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
    }
    catch (e) { }
}


