;
//variabla per te kaluar nga nje reshti i grides tek tjetri
var indexSel = 0;
var id;
var numur;
var pageState  = { webhook: {} };
var kategoria = "";
var indexModifiko;
var focuschange = false;
function OnGetRowValues(values) {
    //var hf = $("#hfLloji")[0];
    id = values[0];
    numur = values[1];
    focuschange = false;
    if (mbush )
        myMenu.ShikoClick(editor, 'ShtoVeprimBanka.aspx?lloji=' + Utils.getUrlVar('lloji') + '&id=' + grid_veprimeBankaKoka.GetRowKey(grid_veprimeBankaKoka.GetFocusedRowIndex()) + '&numer=' + values[1] + '&indexrow=' + indexModifiko + '&shtim_modifikim=modifikim');
}

//per filtrat
function checkText(s, e) {
    myMenu.checkText(s, e);
}

var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, grid_veprimeBankaKoka, "", "");
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

/*
Function: Poshte_click

perdoret per te selektuar rreshtin me poshte

Parameters:

e-eventi
*/
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, grid_veprimeBankaKoka, indexSel);
}

/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, grid_veprimeBankaKoka, indexSel);
}

/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, grid_veprimeBankaKoka, indexSel);
}

/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, grid_veprimeBankaKoka, indexSel);
}

/*
Function: menu_click

perdoret per veprimet e menuse ne javascript

Parameters:

e-eventi
*/
function menu_click(s, e) {
    //var hf = $("#hfLloji")[0];
    switch (e.item.name) {
        case 'Fshi':
            popFshi.Hide();
            grid_veprimeBankaKoka.GetSelectedFieldValues('IdKoka;IdKonfigAmbjente;Kase', SuccededCallbackSelect);
            e.processOnServer = false
            break;
        case 'Klono':
            if (id === null)
                myMesazh.ShtoMesazhGabimi(hfState.Get("regjisDokNukKeniAsnjeDokTeZgjedhur"));
            else
                myFaqeCelje.kontrolloTeDrejta('ShtoVeprimBanka.aspx?lloji=' + Utils.getUrlVar('lloji') + '&id=' + grid_veprimeBankaKoka.GetRowKey(grid_veprimeBankaKoka.GetFocusedRowIndex()) + '&numer=' + numur + '&indexrow=' + indexModifiko + '&shtim_modifikim=klonim');
            e.processOnServer = false;
            break;
        case 'Shiko':
            e.processOnServer = false;
            ShikoDokument();
            break;
        case 'Cancel':
            CancelClick(e);
            break;
        case 'Arkiva':
            ButtonClickArkiva();
            e.processOnServer = false;
            break;
        case 'Eksporto':
            e.processOnServer = false;
            grid_veprimeBankaKoka.GetSelectedFieldValues('IdKoka', OnGridSelectionCompleteEksport);
            break;
        case 'DergoEmail':
            e.processOnServer = false;
            grid_veprimeBankaKoka.GetSelectedFieldValues('IdKoka', OnGridSelectionCompleteDergoMeEmail);
            break;
        default:
            myMenu.menu_click_regjistrime(s, e, "ShtoVeprimBanka.aspx?lloji=" + Utils.getUrlVar('lloji') + '&shtim_modifikim=shtim&id=0', 'ShtoVeprimBanka.aspx?lloji=' + Utils.getUrlVar('lloji') + '&id=' + grid_veprimeBankaKoka.GetRowKey(grid_veprimeBankaKoka.GetFocusedRowIndex()) + '&numer=' + numur + '&indexrow=' + indexModifiko + '&shtim_modifikim=modifikim', grid_veprimeBankaKoka.GetSelectedRowCount());
            break;
    }
}

function OnGridSelectionCompleteEksport(values) {    
    if (Utils.getUrlVar('lloji') == 'arketim' || Utils.getUrlVar('lloji') == 'pagese')
        myButtonClickLupa.ButtonClickLupaEksporto(values, Utils.getUrlVar('lloji'), 3, 'Veprime Arke', 'Format Standart Veprime arke');
    else
        myButtonClickLupa.ButtonClickLupaEksporto(values, Utils.getUrlVar('lloji'), 4, 'Veprime Banke', 'Format Standart Veprime banke');
}

function OnGridSelectionCompleteDergoMeEmail(values) {
    if (values.length == 0) {
        myMesazh.ShtoMesazhGabimi("Zgjidhni nje dokument!");
        return;
    }
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "VeprimeArkaBanka_DergoFatureMeEmail"),
        data: JSON.stringify({ idsKokaDok: JSON.parse("[" + values + "]") })
    }).done(SuccededCallbacDergimiMeEMail);
}
function SuccededCallbacDergimiMeEMail(result) {
    if (result.informimi.PershkrimMesazhi != "")
        myMesazh.ShtoMesazhGabimi(result.informimi.PershkrimMesazhi);
    if (result.mesazhi.PershkrimMesazhi != "") {
        if (result.mesazhi.Status == false)
            myMesazh.ShtoMesazhGabimi(result.mesazhi.PershkrimMesazhi);
        else
            myMesazh.ShtoMesazhSuksesi(result.mesazhi.PershkrimMesazhi);
    }
}

function ButtonClickArkiva() {
    popupUniversal.SetHeaderText(hfState.Get("regjisDokZgjidhDokPerTeBashkengjitur"));
    popupUniversal.SetSize(738, 548);
    popupUniversal.SetContentUrl('LupaArkiva.aspx?vjenNga=Lista&veprimi=' + Utils.getUrlVar('lloji') + '&idDok=' + grid_veprimeBankaKoka.GetRowKey(grid_veprimeBankaKoka.GetFocusedRowIndex())
        //+ '&shtim_modifikim=modifikim'
        );
    popupUniversal.Show();
}

function closePopup(s, e) {
    popupUniversal.SetContentUrl('');
}

var mbush = false;
function OnGridDoubleClick(e, index) {
    if (!focuschange)
        ShikoDokument(index);
    else {
        indexModifiko = index;
        mbush = true;
    }
}

var editor;
//merr te dhenat e rreshtit te selektuar
function mbushfusha(e) {
    editor = e;
    indexModifiko = grid_veprimeBankaKoka.GetFocusedRowIndex();
    grid_veprimeBankaKoka.GetRowValues(indexModifiko, 'IdKoka;NrDokumenti', OnGetRowValues);
    focuschange = true;
}

function changeName() {
    var hfKonf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('VeprimeBanka.aspx?lloji=' + Utils.getUrlVar('lloji'), 0, hfKonf);
    myMesazh.InicializoTimer();
    myMesazh.shtoHandler();
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
    merrKonfigurimeWebhook(hfState.Get("idNdermarrje"))
}

function enter() {
    if (window.event.keyCode == 13) {
        event.returnValue = false;
        event.cancel = true;
    }
}

function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().texts[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().texts[0]);
    callWebserviceKonfigurimi("302", cmbKonfigurimi.GetText());
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "ktheIndexSelectedFilterPeriudhaKusht"),
        data: JSON.stringify({ idKonfigurimi: cmbKonfigurimi.GetValue() })
    }).done(SuccededCallbacPeriudhaKusht);
}

function SuccededCallbacPeriudhaKusht(result) {
    hfState.Set("PeriudhaSelektuar", result);
    radDtDok.SetSelectedIndex(result);
}
function onSelectionChanged(s, e) {
    hfState.Set('PeriudhaSelektuar', radDtDok.GetSelectedIndex());
    grid_veprimeBankaKoka.PerformCallback();
}
function ndryshoKonfiguriminInit() {
    merrKonfigurimeWebhook(hfState.Get("idNdermarrje"))

}

function callWebserviceKonfigurimi(idKomp, kodKonf) {
    grid_veprimeBankaKoka.PerformCallback(idKomp + ";" + kodKonf);
}

function EndRequestHandler(sender, args) {
    PrintPreview("cpHapFaqe");
    dergoWebhook(pageState.webhook);

}

function PrintPreview(propertyName) {
    var faqe = grid_veprimeBankaKoka[propertyName];
    if (faqe) {
        delete grid_veprimeBankaKoka[propertyName];
        window.open(faqe + "&scopeID=" + Utils.getUrlVar("scopeID"), "_blank");
    }
}

function clickExport(e) {
    if (grid_veprimeBankaKoka.GetSelectedRowCount() == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjdhniNjeNgaElementetEListes"));
        e.processOnServer = false;
    }
}

var nrreshtash = 1
function SuccededCallbackFshi(result) {
    if (result) {
        lblMsgbox.SetText(hfState.Get("regjisDokMsgFaturaEshtePrintNeKase"));
    }
    else lblMsgbox.SetText(hfState.Get("msgJuKeniZgjedhur") + nrreshtash + hfState.Get("msgRreshta") + hfState.Get("labelAdministrimiMsgJeniSigurt"));
    popFshi.Show();
}
function SuccededCallbackSelect(result) {
    nrreshtash = result.length;
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "kontrolloPrintuarKase"),
        data: JSON.stringify({ id: result, lloji: "arka" })
    }).done(SuccededCallbackFshi);
}

function CancelClick(e){
    popCancel.Hide();
    indexModifiko = grid_veprimeBankaKoka.GetFocusedRowIndex();
    e.processOnServer = false;
    grid_veprimeBankaKoka.GetRowValues(indexModifiko, 'IdKoka;IdKonfigAmbjente;Vlera;DateDokumenti;CustomerNumber;IdStatusDokumenti', function (result) {
        if (result[5] == 0) {
            myMesazh.ShtoMesazhGabimi("Arketimet me status draft nuk mund te anullohen!");
            return;
        }
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "kontrolloAnulluar"),
            data: JSON.stringify({ id: result, lloji: "arka" })
        }).done(SuccededCallbackCancel);
    });
}

function SuccededCallbackCancel(result) {
    $('#hfURL').val("");
    if (result[0]) {
        myMesazh.ShtoMesazhGabimi("Ky dokument eshte anulluar njehere dhe nuk mund te anullohet perseri");
        return;
    }

    if (!result[1]) {
        myMesazh.ShtoMesazhGabimi("Ky dokument nuk mund te anullohet");
        return;
    }
    $('#hfURL').val(result[2]);
    lblMsgbox1.SetText(result[3]);
    popCancel.Show();

}

function ShikoDokument(merrIndex) {
    // therrasim webservis qe te marrim konfigurimin e dokumentit qe te caktojm cilen URL per komponenten e duhur
    index = merrIndex == undefined ? grid_veprimeBankaKoka.GetFocusedRowIndex() : merrIndex;
    grid_veprimeBankaKoka.GetRowValues(index, 'IdKoka;IdKonfigAmbjente', function (result) {
        $.ajax({
            url: Utils.getServerApiUrl("Konfigurime", "ktheKonfigurimDok"),
            data: JSON.stringify({ IdKoka: result[0], lloji: Utils.getUrlVar('lloji') })
        }).done(SuccededCallbackPercaktoLlojinUrlSipasKonfig);
    });
}

function SuccededCallbackPercaktoLlojinUrlSipasKonfig(result) {
    myMenu.ShikoClick(editor, 'ShtoVeprimBanka.aspx?lloji=' + result + '&id=' + grid_veprimeBankaKoka.GetRowKey(grid_veprimeBankaKoka.GetFocusedRowIndex()) + '&numer=' + numur + '&indexrow=' + indexModifiko + '&shtim_modifikim=modifikim');
}
function merrKonfigurimeWebhook(idnderrmarje) {
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheKonfigurimeWebhook"), data: JSON.stringify({ idndermarje: idnderrmarje })
    }).done(SucceededCallbackWebhook);
}

function SucceededCallbackWebhook(result) {


    pageState.webhook = result;
    if ($('#vjennga').val() == "True")

        dergoWebhook(pageState.webhook);

}
function dergoWebhook(webhook)
{
    if ($('#fshi').val() == "Po")
        {
        for (var i = 0; i < webhook.length; i++) {
            if (webhook[i].Kategoria == 2)
                kategoria = "arketim"
            else if (webhook[i].Kategoria == 3)
                kategoria = "pagese"
            else kategoria = "";
            if ((webhook[i].Eventi == 3 || webhook[i].Eventi == 0 ) && kategoria == Utils.getUrlVar('lloji') && webhook[i].Aktive == true) {
                if ($('#hfObjektRuajtur').val() != "") {

                    $.ajax({
                        type: "POST",
                        url: webhook[i].Urlpritese,

                        data: JSON.stringify($('#hfObjektRuajtur').val()),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            if (response != null) {
                                alert("Eventi : " + response.Event + ", Kategri : " + response.Kategori + ", Mesazh :" + response.Mesazh);
                            } else {
                                console.log("Something went wrong");
                            }
                        },
                        failure: function (response) {
                            console.log(response.responseText);
                        },
                        error: function (response) {
                            console.log(response.responseText);
                        }
                    });
                }
            }

        }
        $('#vjennga').val("False");
        $('#hfObjektRuajtur').val("");}
}