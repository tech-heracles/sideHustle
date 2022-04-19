;
//perdoret per te kontrolluar pergjigjet e kthyera nga webservices

//perdoren per hapjen e lupes se klientit
var widthLupaKlient = 850;
var heightLupaKlient = 550;
var identikuesPerPopupKlientFurnitori;

//perdoren per hapjen e lupes se modelit te automjetit
var widthLupaModelAutomjeti = 900;
var heightLupaModelAutomjeti = 600;
var identikuesPerPopupModelAutomjeti;

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
        //window.parent.popupUniversal.Hide();
        if (Utils.getUrlVar("vjenNga") == undefined) {
            $.ajax({
                url: Utils.getServerApiUrl("Konfigurime", "merrNgaSessionURLLupaShpejte"),
                data: JSON.stringify({ lupa: 'LupaAutomjetShpejte' })
            }).done(Succeded);
            return;
        }
        else window.parent.popupUniversal.Hide();
    }
}

function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);
    callWebserviceKonfigurimi("916", cmbKonfigurimi.GetText());
}

function ndryshoKonfiguriminInit() {
    callWebserviceKonfigurimiInit("916", cmbKonfigurimi.GetText());
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
    $("#dvAutomjet").show();
    if (result != "" && result != null) {
        colKontrollet = result.colKontrollet;
        colAtrTrupi = result.colAtrTrupi;
        var kodniveli = result.kodniveli;
        resultkonf = result;
        LupaKontrollet(colKontrollet, colAtrTrupi);
        var hf = $('#hfKontrollet');
        var hfMod = $('#hfShtimModifikim');
        hfMod.val('shtim');
        var arrTabela = ['tblInformacion'];
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "ASPxPanel");
        if (hfMod.val() == "shtim") {
            hfNrAuto.Clear();
            hfNrAutoKF.Clear();
            vendosNrAutomatik(colAtrTrupi, colKontrollet);
        }
        var klient = Utils.getUrlVar('klienti');
        if (typeof (klient) != "undefined") {
            if (klient != "undefined") {
                btneKlienti.SetValue(Utils.getUrlVar('klienti'));
                $.ajax({ pritPergjigje: true, url: Utils.getServerApiUrl("Rregjistrime", "merrEmertimKlienti"), data: JSON.stringify({ idKlient: Utils.getUrlVar('klienti') }) }).done(SucceededCallbackKlienti);
            }
        }
    }
}

function vendosNrAutomatik(colAtrTrupi, colKontrollet) {
    myNrAuto.vendosNrAutomatik(colAtrTrupi, colKontrollet, new Date());
}

function SucceededCallbackKlienti(result) {
    btneKlienti.SetText(result);
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

function Klienti_Click() {
    var hf = $("#hfLupaKlienti")[0];
    var queryStr = hf.value;
    identikuesPerPopupKlientFurnitori = "LupaAutomjetShpejte";
    myButtonClickLupa.ButtonClickFurnitori(hfState.Get("headerZgjidhKlientFurnitorin"), queryStr, "Klient", widthLupaKlient, heightLupaKlient);
}

function textChangedKlienti(s, e) {
    if (isNaN(btneKlienti.GetValue())) {
        btneKlienti.SetText('');
        btneKlienti.Focus();
        return;
    }
}

function ModelAuto_Click() {
    identikuesPerPopupModelAutomjeti = "LupaAutomjetShpejte";
    myButtonClickLupa.ButtonClickModelAutomjeti(widthLupaModelAutomjeti, heightLupaModelAutomjeti);
}

function textChangedModelAuto(s, e) {
    if (isNaN(cmbModelAuto.GetValue())) {
        cmbModelAuto.SetText('');
        cmbModelAuto.Focus();
        return;
    }
}

/*
Function: EndRequestHandler

Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe array-t e perdorura.
Shiko funksionet  <ndryshoKonfigurimin>.
*/
function EndRequestHandler(sender, args) {
    var hf = $("#hfStatusi")[0];
    if (hf.value == "true") {
        if (Utils.getUrlVar("vjenNga") == undefined) {
            $.ajax({
                url: Utils.getServerApiUrl("Konfigurime", "merrNgaSessionURLLupaShpejte"),
                data: JSON.stringify({ lupa: 'LupaAutomjetShpejte' })
            }).done(Succeded);
            return;
        }
            //else if (Utils.getUrlVar("vjenNga") == 'Shto_RegjistrimDokumentash' || Utils.getUrlVar("vjenNga") == 'Shto_RegjistrimMagazine' || Utils.getUrlVar("vjenNga") == 'ShtoVeprimBanka') {

        else if (Utils.getUrlVar("vjenNga") == 'Shto_RegjistrimDokumentash') {
            window.parent.btneAutomjeti.SetValue(hfId.value);
            window.parent.btneAutomjeti.SetText(txtNrShasie.GetText());
            window.parent.txtTarga.SetText(txtTarga.GetText());
            if (window.parent.btnKlienti.GetText() == '')
                window.parent.btnKlienti.SetValue(btneKlienti.GetValue());
            window.parent.callWebserviceKF(btneKlienti.GetValue(), false);
        }
        else if (Utils.getUrlVar("vjenNga") == 'Shto_RegjistrimMagazine') {
            window.parent.btneAutomjeti.SetValue(hfId.value);
            window.parent.btneAutomjeti.SetText(txtNrShasie.GetText());
            window.parent.txtTarga.SetText(txtTarga.GetText());
            if (window.parent.btneKlientFurnitori.GetText() == '')
                window.parent.btneKlientFurnitori.SetValue(btneKlienti.GetValue());
        }
        //else if (Utils.getUrlVar("vjenNga") == 'ShtoVeprimBanka') {
        //    window.parent.btneAutomjet.SetValue(hfId.value);
        //    window.parent.btneAutomjet.SetText(txtNrShasie.GetText());
        //    window.parent.txtTarga.SetText(txtTarga.GetText());
        //}
        //}
        hf.value = "false";
        window.parent.popupUniversal.Hide();
    }
    Utils.hiqLoadingGif();;
}

function Succeded(result) {
    window.location = result;
}