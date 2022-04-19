; var widthLupaKPF = 700, heightLupaKPF = 500;
var widthLupaGrupi = 700, heightLupaGrupi = 500;
var widthLupaNenGrupi = 700, heightLupaNenGrupi = 500;
var widthLupaLogaria = 900, heightLupaLlogaria = 600;
var identifikuesperKPF = "LlogariShpejte";
var identikuesPerPopupGrupeLlogari = "LlogariShpejte";
var identikuesPerPopupNenGrupeLlogari = "LlogariShpejte";

$(window).load(function () {
    try {
        Init();
        ASPxPanel1.SetWidth(document.documentElement.clientWidth - 20);
    }
    catch (e) {
    }
});

$(window).bind('resize', function () {//po
    try {
        //document.getElementById("menu").style.width = document.documentElement.clientWidth - 50;
        ASPxPanel1.SetWidth(document.documentElement.clientWidth - 20);
    }
    catch (e) {
    }
}).trigger('resize');

/*
Function: menu_click

perdoret per veprimet e menuse ne javascript

Parameters:

e-eventi
*/
function menu_click(s, e) {
    if (e.item.name == 'Ruaj') {
        Utils.shfaqLoadingGif();;
        valido(s, e);
    }
    if (e.item.name == 'Anullo') {
        if (Utils.getUrlVar("vjenNga") == undefined) {
            $.ajax({                
                url: Utils.getServerApiUrl("Konfigurime", "merrNgaSessionURLllog"),
                data: JSON.stringify({ })
            }).done(Succeded);
            return;
        }
        window.parent.popupUniversal.Hide();
    }
}

function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);
    callWebserviceKonfigurimi("911", cmbKonfigurimi.GetText());
}

function ndryshoKonfiguriminInit() {
    callWebserviceKonfigurimiInit("911", cmbKonfigurimi.GetText());
}

function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.shtoHandlerSession();
    if (hf !== null) {
        lblKonfigurimi.SetText(hf.value.split(';')[1]);
        cmbKonfigurimi.SetText(hf.value.split(';')[0]);
        ndryshoKonfiguriminInit();
    }
    $("#hfRuaj").val("Ruaj");
    myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(EndRequestHandler);
    prm.add_endRequest(myMesazh.EndRequestTimer);
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ? evt.keyCode : evt.charCode;
}

function Init() {
    window.parent.popupUniversal.UpdatePosition();
    lengjth = window.history.length;
    changeName();
    var kodi = Utils.getUrlVar('kodi');
    var veprimi = Utils.getUrlVar('klonim');
    var idndermarje = hfState.Get('idNdermarrje');
    if (kodi) {
        if (veprimi == 'true') {
            $.ajax({
                url: Utils.getServerApiUrl("Rregjistrime", "ktheLlogariSipasKodit"),
                data: JSON.stringify({ kodi: kodi, idNdermarja: idndermarje })
            }).done(KonfiguroVleraFillestareModifikim);
        }
    }
}

function KonfiguroVleraFillestareModifikim(result) {
    if (result != null)
        mbushFushat(result);
}

function mbushFushat(values) {
    $('#hfId')[0].value = values.IdLlogari;
    txtNr.SetText(values.NrLlogari);
    txtEmerLlogarie1.SetText(values.EmerLlogari1);
    Utils.SelectComboItem(cmbMonedha, values.IdMonedha, values.KodiMonedha);
    Utils.SelectComboItem(cmbGrupi, values.Grupi, values.PershkrimiGrupiLlogaria);
    Utils.SelectComboItem(cmbNengrupi, values.Nengrupi, values.PershkrimiNenGrupiLlogaria);
    Utils.SelectComboItem(cmbKpf1, values.KPF1, values.KodiKPF1);
}

function valido(s, e) {
    myFaqeCelje.validim(s, e);
}

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}

function enter() {
    if (window.event.keyCode == 13) {
        event.returnValue = false;
        event.cancel = true;
    }
}

function callWebserviceKonfigurimi(idKomp, kodKonf) {
    var idGjuha = hfState.Get('idGjuha');
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
}

function callWebserviceKonfigurimiInit(idKomp, kodKonf) {
    var idGjuha = hfState.Get('idGjuha');
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
}

function SucceededCallbackKonfig(result) {
    $("#dvLlogari").show();//$("#dvLlogari")[0].style.visibility = 'visible';
    //$("#dvLlogari")[0].style.display = '';
    if (result != "" && result != null) {
        colKontrollet = result.colKontrollet;
        colAtrTrupi = result.colAtrTrupi;
        var kodniveli = result.kodniveli;
        resultkonf = result;
        LupaKontrollet(colKontrollet, colAtrTrupi);
        var hf = $('#hfKontrollet');
        var hfMod = $('#hfShtimModifikim');
        //hfMod.val('shtim');
        var arrTabela = ['tblInformacion'];
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "ASPxPanel");
        var monedha = hfState.Get("MonedhaNder");
        cmbMonedha.SetText(monedha);
    }
}

function LupaKontrollet(kontrollet, colAtrTrupi) {
    var hf1 = $("#hfLupaGrupi");
    var hf2 = $("#hfLupaNengrupi");
    var hf3 = $("#hfLupaKpf1");
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
        if (kontrollet[i].KodKontrolli == "cmbKpf1") {
            hf3.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
    }
}

function KPF1_Click() {
    var hf = $("#hfLupaKpf1")[0];
    var queryStr = hf.value;
    myButtonClickLupa.KPF_Click(hfState.Get("headerPopUpZgjidhLlogarineStandarte"), queryStr, widthLupaKPF, heightLupaKPF, 1);
}

function Nengrupi_Click(Grupi) {
    var hf = $("#hfLupaNengrupi")[0];
    var queryStr = hf.value;
    myButtonClickLupa.Nengrupi_Click(hfState.Get("headerPopUpZgjidhNenGrupin"), queryStr, widthLupaNenGrupi, heightLupaNenGrupi, Grupi);
}

function Grupi_Click() {
    var hf = $("#hfLupaGrupi")[0];
    var queryStr = hf.value;
    myButtonClickLupa.Grupi_Click(hfState.Get("headerPopUpZgjidhGrupin"), queryStr, widthLupaGrupi, heightLupaGrupi);
}

function Qendra_Click() {
    if (cmbLloji.GetValue() == 1)
        myButtonClickLupa.LupaUniversal_Click(hfState.Get("headerPopUpZgjidhQendrenKostos"), 'Shto_QendraKosto.aspx?lupe=true&llojLupe=qk&vjenNga=LupaLlogariShpejte', widthLupaLogaria, widthLupaLogaria);
    else myButtonClickLupa.LupaUniversal_Click(hfState.Get("headerPopUpZgjidhSkemenKostos"), 'LupaSkemaKosto.aspx?vjenNga=LupaLlogariShpejte', widthLupaLogaria, widthLupaLogaria);
}

/*
Function: EndRequestHandler

Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe array-t e perdorura.
Shiko funksionet  <ndryshoKonfigurimin>.
*/
function EndRequestHandler(sender, args) {
    var hf = $("#hfStatusi")[0];
    if (hf.value == "true") {
        switch (Utils.getUrlVar("vjenNga")) {
            case "undefined":
            case undefined:
                $.ajax({
                    url: Utils.getServerApiUrl("Konfigurime", "merrNgaSessionURLllog"),
                    data: JSON.stringify({})
                }).done(Succeded);
                break;
            case "Shto_Banka":
                if (hf.value == "true")
                    window.parent.hfState.Set("llog", hfId.value);
                $.ajax({
                    url: Utils.getServerApiUrl("Konfigurime", "merrNgaSessionURLllog"),
                    data: JSON.stringify({})
                }).done(Succeded);
                break;
            case "Shto_RegjistrimDokumentash":
                var rreshtaTeGrides = window.parent.$("#rowed5").jqGrid('getRowData');
                var rreshtILire = ktheRreshtiILire(rreshtaTeGrides);
                var idPerdoruesi = hfState.Get('idPerdoruesi');
                var idNdermarrje = hfState.Get('idNdermarrje');
                $.ajax({ url: Utils.getServerApiUrl("Rregjistrime", "KtheVleraLlogKodTvsh"), data: JSON.stringify({ kodi: txtNr.GetText(), index: rreshtILire, llojTvsh: window.parent.tvshkont, idPerdoruesi: idPerdoruesi, idNdermarrje: idNdermarrje, TaksaKF: window.parent.pageState.TaksaKF }) }).done(window.parent.SucceededCallbackLlogPlote);
                window.parent.enable();
                hf.value = "false";
                window.parent.popupUniversal.Hide();
                Utils.hiqLoadingGif();;
                break;
        }
    }
}

function ktheRreshtiILire(rreshtaTeGrides) {
    var rreshtILire = 0;
    for (i = 0; i < rreshtaTeGrides.length; i++) {
        if (rreshtaTeGrides[i].undefined !== "") {
            if (rreshtaTeGrides[i].txtKodi.toString().search('value') != -1) {
                var grida = window.parent.$("#rowed5");
                var idRow = grida.getLastSel2();
                if (window.parent.$('#txtKodi' + idRow).val() == "" && window.parent.$('#cmbLloji' + idRow).val() == 3) {
                    rreshtILire = idRow;
                    break;
                }
            }
            else if (rreshtaTeGrides[i].txtKodi == "" && (rreshtaTeGrides[i].cmbLloji == "Llogari")) {
                rreshtILire = window.parent.$("#rowed5").jqGrid('getDataIDs')[i];
                break;
            }
            else if (rreshtaTeGrides[i].txtKodi == "" && (rreshtaTeGrides[i].cmbLloji == "")) {
                rreshtILire = window.parent.$("#rowed5").jqGrid('getDataIDs')[i];
                var rreshti = window.parent.$('#rowed5').getRowData(rreshtILire);
                rreshti.cmbLloji = 'Llogari';
                rreshti.txtVleftaTVSH = 1;
                rreshti.txtVlefta = 1;
                rreshti.txtDtFillimi = new Date().format('dd/MM/yyyy');
                rreshti.txtDtMbarimi = new Date().format('dd/MM/yyyy');
                window.parent.$('#rowed5').jqGrid('setRowData', rreshtILire, rreshti);
                var rreshtitjeter = parseFloat(rreshtILire) + 1;
                if (window.parent.arrayReadOnlyKolonaGrides[window.parent.arrayReadOnlyKolonaGrides.length - 1] == 'True')
                    be = "<input id='butonFshi" + rreshtitjeter + "' type='image' value='Fshi' disabled='disabled' onmouseover='ndryshoImazhin(1," + rreshtitjeter + ")' onmouseout='ndryshoImazhin(0," + rreshtitjeter + ")'  src='images/square-icon.png'  onclick='fshiClicked(" + rreshtitjeter + ")'/>";
                else
                    be = "<input id='butonFshi" + rreshtitjeter + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + rreshtitjeter + ")' onmouseout='ndryshoImazhin(0," + rreshtitjeter + ")'  src='images/square-icon.png' onclick='fshiClicked(" + rreshtitjeter + ")'/>";
                var datarow = { txtFshi: be };
                var su = window.parent.$('#rowed5').jqGrid('addRowData', parseInt(rreshtitjeter), datarow);
                break;
            }
        }
    }
    return rreshtILire;
}

function Succeded(result) {
    window.location = result;
}

function ButtonClickNengrupi(s, e) {
    grida = false;
    if (cmbGrupi.GetText() != '')
        Nengrupi_Click(cmbGrupi.GetText());
    else myMesazh.ShtoMesazhGabimi(hfState.Get("msgLupaLlogariShpejteZgjidhniGrupin"));
}