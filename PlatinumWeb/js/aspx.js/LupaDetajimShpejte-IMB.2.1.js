/*
Function: menu_click

perdoret per veprimet e menuse ne javascript

Parameters:

e-eventi
*/
function menu_click(s, e) {
    var hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    //    var hfId = $('#hfId'); //hidden fieldi qe ruan id  e rreshtit te selektuar
    //    //            var hfKontrollet = $('#hfKontrollet');
    //    var ruajbuxhetet = false; //i here per i here
    if (e.item.name == 'Ruaj') {
        Utils.shfaqLoadingGif();
        valido(s, e);
    }
    if (e.item.name == 'Anullo') {
        window.parent.popupUniversal.Hide();
    }
    if (e.item.name == 'OK') {
        e.processOnServer = false;
        OnGridSelectionChanged();
    }
}

function OnGridSelectionChanged() {
    gvLupaDetajime.GetSelectedFieldValues('IdDetajimArtikulli;KodDetajimArtikulli;KategoriDetajimi', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {

    if (Utils.getUrlVar("vjenNga") == undefined || Utils.getUrlVar("vjenNga") == "") {
        var kodi = '';
        for (var i = 0; i < values.length; i++) {
            if (kodi == '')
                kodi = values[i][1];
            else
                kodi = kodi + ',' + values[i][1];
        } if (window.parent.identifikuesPerPopupDetajime == "RegjistrimDokumentash") {
            window.parent.editorDetajimi.value = kodi;
            window.parent.arr[3][window.parent.keyGlobal] = window.parent.keyGlobal.toString() + ":" + kodi;
        }
        if (window.parent.identifikuesPerPopupDetajime == "raportDetajime") {
            window.parent.editorGlobal.SetText(kodi);
        }
        else {//kur thirret nga celja e artikullit
            window.parent.editordetajimi.SetText(kodi);
            window.parent.editorKategoriDetajimi.SetValue(values[0][2]);
            window.parent.editorCheckBoxDetajimi.SetChecked(true);
        }

        window.parent.popupUniversal.Hide();
    }
    else if (Utils.getUrlVar("vjenNga") === "Shto_Planifikim") {
        window.parent.vendosDetajim({ IdDetajimArtikulli: values[0][0], KodDetajimArtikulli: values[0][1] }, Utils.getUrlVar("lloji"));
        window.parent.popupUniversal.Hide();
    }
    else
        Utils.ShtoDetajimeNeGride(values);
}

function callWebserviceKonfigurimiInit(idKomp, kodKonf) {
    var idGjuha = hfState.Get('idGjuha');
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
}

function callWebserviceKonfigurimi(idKomp, kodKonf) {
    var idGjuha = hfState.Get('idGjuha');
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
}

function SucceededCallbackKonfig(result) {
    $("#dvDetajime").show();
    if (result != "" && result != null) {
        colKontrollet = result.colKontrollet;
        colAtrTrupi = result.colAtrTrupi;
        var kodniveli = result.kodniveli;
        resultkonf = result;
        var hf = $('#hfKontrollet');
        var hfMod = $('#hfShtimModifikim');
        hfMod.val('shtim');
        var arrTabela = ['tblInformacion'];
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "ASPxPanel");
    }
    InitiComboKategoriaLloji();
}

function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);
    callWebserviceKonfigurimi("442", cmbKonfigurimi.GetText());
}

function ndryshoKonfiguriminInit() {
    callWebserviceKonfigurimiInit("442", cmbKonfigurimi.GetText());
}

function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.shtoHandlerSession();

    if (hf !== null) {
        lblKonfigurimi.SetText(hf.value.split(';')[1]);
        cmbKonfigurimi.SetText(hf.value.split(';')[0]);
        ndryshoKonfiguriminInit();
    }
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(EndRequestHandler);
    prm.add_endRequest(myMesazh.EndRequestTimer);
}

var arr = new Array();
var counter = 0;
var arr2 = new Array();
var counter2 = 0;

/*
Function: EndRequestHandler
    
Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe array-t e perdorura.
Shiko funksionet  <ndryshoKonfigurimin>.
*/
function EndRequestHandler(sender, args) {
    var hf = $("#hfStatusi");
    if (hf.val() == "true") {
        window.mbush = false;
        $('#hfShtimModifikim').val("shtim"); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim     
        VendosDetajimNeCombo();
        pastrofusha();
        hf.val("false");
        SucceededCallbackKonfig(resultkonf);
        gvLupaDetajime.PerformCallback("442;" + cmbKonfigurimi.GetText());
        gvLupaDetajime.ClearFilter();
    }
    Utils.hiqLoadingGif();;
}

function VendosDetajimNeCombo() {
    if(window.parent != undefined && window.parent.identifikuesPerPopupDetajime != undefined && window.parent.identifikuesPerPopupDetajime == "Kodbar" 
       && window.parent.window.parent != undefined && window.parent.window.parent.identifikuesPerPopupKodbare != undefined && window.parent.window.parent.identifikuesPerPopupKodbare == "ShtoArtikull")
    {
        var control;
        if (Utils.getUrlVar("lloji") == 1)
            control = window.parent.window.parent.btnDetajim1Nga;
        else
            control = window.parent.window.parent.btnDetajim2Nga;

        var currentValue = control.GetValue();
        if (Utils.IsNullOrEmpty(currentValue))
            currentValue = txtKodi.GetText();
        else
            currentValue += "," + txtKodi.GetText();

        control.SetValue(currentValue);
    }
}


//pastron fushat per shtim dhe ben aktive fushat
function pastrofusha() {
    txtKodi.SetText('');
    txtPershkrimi.SetText('');
    var hf = $('#hfKontrollet')[0];
    aktivizoFusha(colKontrollet, colAtrTrupi, false);
}

function aktivizoFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $('#hfShtimModifikim');
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
}

function Succeded(result) {
    window.location = result;
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ? evt.keyCode : evt.charCode;
}

function valido(s, e) {
    myFaqeCelje.validim(s, e);
}

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}

var lengjth = 0;

function Init() {
    try {
        window.parent.popupUniversal.UpdatePosition();
        lengjth = window.history.length;
        changeName();
        gvLupaDetajime.SetFocusedRowIndex(0);
    }
    catch (err) {
    }
}

function enter() {
    if (window.event.keyCode == 13) {
        event.returnValue = false;
        event.cancel = true;
    }
}

function InitiComboKategoriaLloji() {
    if (typeof(Utils.getUrlVar("vjenNga")) != "undefined" && Utils.getUrlVar("vjenNga") != "undefined" && Utils.getUrlVar("vjenNga") != "") {
        if (typeof (Utils.getUrlVar("kodDet")) != "undefined" && Utils.getUrlVar("kodDet") != "undefined") {
            var kodi = Utils.getUrlVar("kodDet");
            txtKodi.SetText(kodi);
        }
    }

    var kategoria = Utils.getUrlVar("veprimi");
    if (typeof (Utils.getUrlVar("idArtikulli")) == "undefined" || Utils.getUrlVar("idArtikulli") == ""
        || Utils.getUrlVar("idArtikulli") == "0"
        || !(kategoria == undefined || kategoria == "0" || kategoria == ""))
        enableComboKategoriaDheLloji(kategoria);
    else {
        try {
            $.ajax({
                url: Utils.getServerApiUrl("Rregjistrime", "ktheIdKategoriDetajimi"),
                data: JSON.stringify({ lloji: Utils.getUrlVar("lloji"), idArt: Utils.getUrlVar("idArtikulli") })
            }).done(SucceededCallbackDetajimi);
        }
        catch (e) {
            myMesazh.ShtoMesazhGabimi('Ndodhi nje gabim gjate marrjes se kategorise se detajimit!');
        }
    }
}

function enableComboKategoriaDheLloji(kategoria) {
    switch (kategoria) {
        case "1": //Detajim
            cmbKategoria.SetEnabled(false);
            cmbKategoria.SetValue(1);
            cmbLloji.ClearItems();
            cmbLloji.AddItem("Alfanumerik", 1);
            cmbLloji.AddItem("Numerik", 2);
            cmbLloji.AddItem("Date", 3);
            cmbLloji.SetSelectedIndex(0);
            cmbLloji.SetEnabled(true);
            txtPershkrimi.SetEnabled(true);
            break;
        case "2": //Serial
            cmbKategoria.SetEnabled(false);
            cmbKategoria.SetValue(2);
            cmbLloji.ClearItems();
            cmbLloji.AddItem("Alfanumerik", 1);
            cmbLloji.AddItem("Numerik", 2);
            cmbLloji.SetEnabled(true);
            txtPershkrimi.SetEnabled(true);
            break;
        case "3": //Date skadence
            cmbKategoria.SetEnabled(false);
            cmbKategoria.SetValue(3);
            cmbLloji.ClearItems();
            cmbLloji.AddItem("Date", 3);
            cmbLloji.SetEnabled(false);
            cmbLloji.SetSelectedIndex(0);
            txtPershkrimi.SetEnabled(false);
            break;
        case "4": //Seri
            cmbKategoria.SetEnabled(false);
            cmbKategoria.SetValue(4);
            cmbLloji.ClearItems();
            cmbLloji.AddItem("Alfanumerik", 1);
            cmbLloji.AddItem("Numerik", 2);
            cmbLloji.SetEnabled(true);
            cmbLloji.SetSelectedIndex(0);
            txtPershkrimi.SetEnabled(false);
            break;
        default:
            cmbLloji.ClearItems();
            cmbKategoria.SetSelectedIndex(-1);
            cmbLloji.SetEnabled(true);
            cmbKategoria.SetEnabled(true);
            txtKodi.SetEnabled(true);
            txtPershkrimi.SetEnabled(true);
            break;
    }
}

function SucceededCallbackDetajimi(result) {
    enableComboKategoriaDheLloji(result);
}

function TextChangedKategoria() {
    if (cmbKategoria.GetValue() == "3")//date skadence
    {
        cmbLloji.ClearItems();
        cmbLloji.AddItem("Date", 3);
        cmbLloji.SetEnabled(false);
        cmbLloji.SetSelectedIndex(0);
        txtPershkrimi.SetEnabled(false);
        txtPershkrimi.SetText(' ');
    }
    else if (cmbKategoria.GetValue() == "2")//Serial
    {
        cmbLloji.ClearItems();
        cmbLloji.AddItem("Alfanumerik", 1);
        cmbLloji.AddItem("Numerik", 2);
        cmbLloji.SetEnabled(true);
        txtPershkrimi.SetEnabled(true);
    }
    else if (cmbKategoria.GetValue() == "1")//Detajim
    {
        cmbLloji.ClearItems();
        cmbLloji.AddItem("Alfanumerik", 1);
        cmbLloji.SetSelectedIndex(0);
        cmbLloji.SetEnabled(false);
        txtPershkrimi.SetEnabled(true);
    }
    else if (cmbKategoria.GetValue() == "4") //Seri
    {
        cmbLloji.ClearItems();
        cmbLloji.AddItem("Alfanumerik", 1);
        cmbLloji.AddItem("Numerik", 2);
        cmbLloji.SetEnabled(true);
        cmbLloji.SetSelectedIndex(0);
        txtPershkrimi.SetEnabled(false);
        txtPershkrimi.SetText(' ');
    }
}
