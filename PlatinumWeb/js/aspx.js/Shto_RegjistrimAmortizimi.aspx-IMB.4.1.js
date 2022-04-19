;
var formati = 6;


Array.prototype.ekzistonElementi = function (value) {
    var i;
    for (var i = 0, loopCnt = this.length; i < loopCnt; i++) {
        if (this[i] == value) {
            return true;
        }
    }
    return false;
};



var varKonfig = {
    identifikuesPerLocalStorageKey: 'Amortizimi'
};

$(document).ready(function () {

    Utils.konfiguroAccorditionNeDocReady(varKonfig.identifikuesPerLocalStorageKey);

    $(window).on('resize', function () {
        try {
            if (Utils.isGridResized())//if (!Utils.resizeSplitter(165))
                return;
        }
        catch (ee) {
        }

    }).trigger('resize');
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
        Utils.resizeSplitter();

    });
});

var identifikuesPerPopupDokumentat = "Amortizimi";

var KPF;


function ButtonClickKerko(listUrl) { 
    var niveli;
    if (cmbLloji.GetText() != "")
        niveli = cmbLloji.GetValue();
    else niveli = 1;
  var  queryString = {
        veprimi: 'Amortizimi',
        niveli: niveli,
        listUrl: listUrl
  };
  popupUniversal.SetContentUrl('LupaDokumenta.aspx?' + Utils.KonvertoObjectQueryString(queryString));
    popupUniversal.SetHeaderText(hfState.Get("msgZgjidhDokumentin"));
 
    popupUniversal.SetSize(950, 560);
    popupUniversal.Show();
}

function Init() {
    // PatchJQuery.myPatchJQuery();
    if (typeof (isPostBack) == "undefined") {
        var hf = $("input[id$='hfKonffillestar']")[0];
        document.getElementById("kokeKonfigurimi").innerHTML = hfState.Get("MenuKokeDokumenti");
        document.getElementById("trupKonfigurimi").innerHTML = hfState.Get("MenuTrupDokumenti");
       //document.getElementById("fundKonfigurimi").innerHTML = hfState.Get("MenuFundDokumenti");

        cmbKonfigurimi.SetText(hf.value.split(';')[0]);
        lblKonfigurimi.SetText(hf.value.split(';')[1]);
        callWebserviceKonfigurimi("1003", cmbKonfigurimi.GetText());
        changeName();
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(EndRequestHandler);
        myMesazh.shtoHandler();
    }
}

/*
Vendos formatet e numrave ne gride ne baze te emrit te kolones
*/
function ruajFormatetNeGride(formatNumri) {
    if (typeof formatNumri == 'undefined')
        formatNumri = $.parseJSON(hfState.Get("formatMonedhe"));
    ndryshoKonfigFormatNumri(formatNumri);
    vendosKonfigFormatNumri();
    return;
}

function ndryshoKonfigFormatNumri(formatNumri, formatkursi) {
    ///<summary> metode per ndryshimin e formateve te nr ne gride ne baze te emrit te kolones. gjithashtu vendos formatin e nr dhe per fushat e devexpresit ne baze te emrit te kontrollit</summary>
    ///<param name="formatNumri"> objekt i tipit format nr me te dhenat e formatimit</param>
    ///<param name="formatkursi">sherben per te vendosur formatin e kursit ne varesi te monedhes </param>
    if (typeof formatNumri == 'undefined')
        formatNumri = $.parseJSON(hfState.Get("formatMonedhe"));
    else
        hfState.Set("formatMonedhe", JSON.stringify(formatNumri));

    Utils.setFormatNumri(txtVlefta, formatNumri.KonfigTrupi[0].ShifraPasPresjesVlefta);
}

/*
Formaton vlerat e fushave sipas formatit perkates ne te gjithe rreshtat e grides
*/
function vendosKonfigFormatNumri() {
    formatoFushaDevi();
}

function formatoFushaDevi() {
    Utils.formatoTextBox(txtVlefta);
}

function gjejFormatSipasMonedhes(idmonedha) {
    var formatNumri = $.parseJSON(hfState.Get("formatMonedhe"));
    for (i = 0; i < formatNumri.KonfigTrupi.length; i++)
        if (formatNumri.KonfigTrupi[i].IdMonedha == idmonedha) {
            return formatNumri.KonfigTrupi[i]
        }
    return formatNumri.KonfigTrupi[0];
}

var arrformatevlefta = new Array();
var arrformatevleftaqk = new Array();
var arrformatevleftamon = new Array();

function unformatoFushaDevi() {
    Utils.unFormatoTextBox(txtVlefta);
}

function changeName() {
    myFaqeCelje.shtoHandlerSession();
    try {
        if (Utils.getUrlVar('id') == undefined)
            window.parent.callWebServiceKtheInfoLart('Shto_RegjistrimAmortizimi.aspx', 0);
        else
            window.parent.callWebServiceKtheInfoLart('Shto_RegjistrimAmortizimi.aspx', Utils.getUrlVar('id'));
        window.parent.createCookie('adresa', window.location.href, 1);
    }
    catch (ee) {
    }
}

function callWebserviceKonfigurimi(idKomp, kodKonf) {
    try {
        var idGjuha = hfState.Get('idGjuha');
        var idNdermarrje = hfState.Get('idNdermarrje');
        var idPerdoruesi = hfState.Get('idPerdoruesi');
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Konfigurime", "ktheKonfigAmbjentiMeFormatNumrash"),
            data: JSON.stringify({
                idKomp: idKomp, kodKonf: kodKonf, kodKontrolli: "", idObjekti: -1, shtim: true, merrFormatKursi: true, merrGjitheKonf: true, idGjuha: idGjuha, idPerdoruesi: idPerdoruesi, idNdermarrje: idNdermarrje
            })
        }).done(SucceededCallbackKonfig);
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
    }
}

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}

var azhornim = false;
var kushtet;
var colKushte, colAlterKusht, colKontrollet, colAtrTrupi;
var colGrida;

function SucceededCallbackKonfig(result) {
    var hf = $("input[id$='hfShtimModifikim']");
    var hfLidhur = $("input[id$='hfLidhur']");
    var arrTabela = ['tblAmortizimi', "tblFund"];
    var arrPrind = ["dvFillim", "dvFundi"];
    var colKontrollet = result.colKontroll;   //[0];
    var colAtrTrupi = result.colAtrTrupi;   //[1];
    var vlerat = myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, undefined, hf, '', arrTabela, undefined, undefined, hfLidhur, arrPrind);
    colKontrollet = result.colKontroll;  //[0];
    colAtrTrupi = result.colAtrTrupi;   //[1];
    colGrida = result.colGrida;  //[2];
    colKushte = result.colKushte;   //[3];
    colAlterKusht = result.colAlterKusht;  //[4];
    var kodniveli = result.kodniveli;   //[5];
    var konfLlojRreshti = result.konfLlojRreshti;  //[6];
    $('#hfKontrolletNrAutom').val(JSON.stringify(colKontrollet));
    $('#hfAtributeNrAutom').val(JSON.stringify(colAtrTrupi));
    if ($("input[id$='hfShtimModifikim']").val() == "shtim" || $("input[id$='hfShtimModifikim']").val() == "klonim") {
        hfNrAuto.Clear();
        hfNrAutoShitje.Clear();
        vendosNrAutomatik(colAtrTrupi, colKontrollet);
    }
    $("input[id$='HfGridCol']").val(JSON.stringify(colGrida));
    azhornim = false;

    formatNumriZgjedhur = result.formatNumri;  //[7];
    formatKursi = result.formatKursi; //[8];

    //            mbushim hidden fields me vleren e konfigurimit te lupes per secilin kontroll qe ka lupe
    var hfDok = $("input[id$='hfLupaDokumenti']");
    $("#dvFillim").show();
    $("#dvFundi").show();
    $('#hfKontrolletNrAutom').val(JSON.stringify(colKontrollet));
    $('#hfAtributeNrAutom').val(JSON.stringify(colAtrTrupi));

    ruajFormatetNeGride(formatNumriZgjedhur);

    Utils.konfiguroAccorditionPasKonfigDokumenti(varKonfig.identifikuesPerLocalStorageKey);

    if (hf.val() == 'shtim')
        lblKrijuesi.SetText($("input[id$='hfPerdoruesAktual']").val());
    if (hfLidhur.val() == 'True') {
        btnLlogarit.SetEnabled(false);
        gvAsete.SetEnabled(false);
    }
    else {
        btnLlogarit.SetEnabled(true);
        gvAsete.SetEnabled(true);
    }
}

function ndryshoKonfigurimin() {
    if (cmbKonfigurimi.GetText().split(';').length > 1) {
        lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
        $('#kokeKonfigurimi').text(hfState.Get("MenuKokeDokumenti") +': ' + cmbKonfigurimi.GetText().split(';')[1]);
    }
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);
    callWebserviceKonfigurimi("1003", cmbKonfigurimi.GetText());
}

var trupibosh;

function pastroTextBoxet() {
    txtNrDok.SetText('');
    dteDtDok.SetDate(new Date());
    txtShenime.SetText('');
    lblKrijuesi.SetText($("input[id$='hfPerdoruesAktual']").val());
    $("input[id$='hfStatus']")[0].value = "false";
    txtVlefta.SetText('0.00');

    cmbStandarti.SetSelectedIndex(0);
    cmbMagazina.SetSelectedIndex(-1);
    vendosDateDefault();
    grid_faturat.PerformCallback('pastro');
    gvAsete.UnselectRows();
}

function vendosDateDefault() {
    if ($("input[id$='hfShtimModifikim']").val() == "shtim") {
        try {
            var hfPeriudheObj = window.parent.lexoHfPeriudhe();
            dteDtDok.SetDate(Utils.ktheDateDefault(hfPeriudheObj));
            var dataSot = Utils.zeroOren(new Date());
        }
        catch (ee) {
        }
    }
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ? evt.keyCode : evt.charCode;
}

function EndRequestHandler(sender, args) {
    // This code is executed after a successful async. postback
    formatoFushaDevi();
 
    if ($('#hfqkmesazhi').val() == 'shfaqmesazh') {
        myMesazh.ShtoMesazh({ type: "confirm", layout: "center", modal: true, text: hfState.Get("msgDeshironiShperndarjeQendraKosto"), okClick: function () { Utils.hapPopUp(hfState.Get("msgShperndarjeNeQendratEKostos"), $('#hfUrl')); }, cancelClick: function () { Utils.JopopupClick($('#hfUrl')); } });
    }
    if ($('#hfqkmesazhi').val() == 'shfaqlupe') {
        Utils.hapPopUp(hfState.Get("msgShperndarjeNeQendratEKostos"), $('#hfUrl'));
    }
    var statusRuajtjeQendra = $("input[id$='hfStatus']")[0];
    // Utils.hiqLoadingGif();;
    if (statusRuajtjeQendra.value == "true") {
        ASPxMenu1.GetItemByName('Shto').SetVisible(true);
        myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimAmortizimi.aspx', true);
    }
    else
        click = false;
}

function validoClientSide() {
    if (txtNrDok.GetText() == '') {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgVendosniNumrinEDokumentit"));
        return false;
    }
    else if (dteDtDok.GetDate() == null) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniNjeDateDokumenti"));
        return false;
    }
    else if (dteDtRegjistrimi.GetDate() == null) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniNjeDateRegjstrimi"));
        return false;
    }

    else if (grid_faturat.cpNoRows == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgAmortizimiZgjidhniTePakten1Aset"));
        return false;
    }
    else return true;
}

function PastroClick() {
    ASPxMenu1.GetItemByName('Fshi').SetVisible(false);
    ASPxMenu1.GetItemByName('Draft').SetVisible(true);
    ASPxMenu1.GetItemByName('Shto').SetVisible(true);
    ASPxMenu1.GetItemByName('FletaKontabel').SetVisible(false);
    ASPxMenu1.GetItemByName('QendraKosto').SetVisible(false);
    var statusRuajtjeQendra = $("input[id$='hfStatus']")[0];
    var hf = $("input[id$='hfShtimModifikim']");
    hf.val("shtim");
    pastroTextBoxet();
    myMenu.menuSipasTeDrejtaRegjistrim(hf, hfTeDrejta);
    TextChangedLloji();
    statusRuajtjeQendra.value = "false";
    $('#ASPxSplitter1_hl').empty();
    click = false;
}

function callWebserviceNiveliNew(lloji, tipi, mod) {
    try {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheTemplateteNivelit"),
            data: JSON.stringify({ lloji: lloji, veprimi: tipi, mod: mod })
        }).done(SucceededCallbackNiveliNew);
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
    }
}

function SucceededCallbackNiveliNew(colModelet) {
    cmbKonfigurimi.ClearItems();
    for (i = 0; i < colModelet.length; i++) {
        cmbKonfigurimi.AddItem([colModelet[i].KodKonfigAmbjente, colModelet[i].PershkrimKonfigAmbjente], colModelet[i].IdKonfigAmbjente); //AddItem(teksti, vlera);
    }
    cmbKonfigurimi.SelectIndex(0);
    ndryshoKonfigurimin();
}

var click = false; //perdoret qe perdoruesi te mos shtype dyhere ruaj
function menu_click(s, e) {
    if (click) {
        e.processOnServer = false;
    }
    else {
        click = true;
        if (e.item.name == 'Ruaj') {

            myFaqeCelje.validim(s, e);

            if (validoClientSide()) {
                Utils.shfaqLoadingGif();;
                RuajClick(s, e);
            }
            else {
                e.processOnServer = false;
                click = false;
                Utils.hiqLoadingGif();;
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgPlotesoniTeGjithaFushat"));
            }
        }
        else if (e.item.name == 'Draft') {
            myFaqeCelje.validim(s, e);

            if (validoClientSide()) {
                Utils.shfaqLoadingGif();;
                RuajClick(s, e);
            }
            else {
                e.processOnServer = false;
                click = false;
                Utils.hiqLoadingGif();;
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgPlotesoniTeGjithaFushat"));
            }
        }


        else if (e.item.name == 'Fshi') {
            popFshi.Show(); e.processOnServer = false; click = false;
        }
        else if (e.item.name == 'Kerko') {
            myFaqeCelje.kontrolloTeDrejta("RegjistrimAmortizimi.aspx", null, true);
            e.processOnServer = false; click = false;
        }
        else if (e.item.name == 'Shto') {
            $("input[id$='hfLidhur']").val(false);

            myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimAmortizimi.aspx', true); e.processOnServer = false; click = false;
        }
        else if (e.item.name == 'Anullo') {
            myFaqeCelje.kontrolloTeDrejta("RegjistrimAmortizimi.aspx");
            e.processOnServer = false;
            click = false;
        }

    }

}
function Kthehu(mesazh) {
    myFaqeCelje.kontrolloTeDrejta("RegjistrimAmortizimi.aspx?ruaj=po");

}

/*
Function: RuajClick

Validon dokumentin dhe therret funksionin <merrTeDhena> per te marre te dhenat e grides kur klikohet butoni Ruaj.
*/

function RuajClick(s, e) {
    if (validoClientSide()) {

    }
    else {
        e.processOnServer = false; click = false; Utils.hiqLoadingGif();;
    }
}

function vendosNrAutomatik(colAtrTrupi, colKontrollet) {
    myNrAuto.vendosNrAutomatik(colAtrTrupi, colKontrollet, dteDtDok.GetDate());
}

function DateChanged() {
    var hfKontrollet = JSON.parse($('#hfKontrolletNrAutom').val());
    var atributet = JSON.parse($('#hfAtributeNrAutom').val());
    vendosNrAutomatik(atributet, hfKontrollet);
}

var identifikuesPerPopupMagazina = "RegjistrimAmortizimi";

function ButtonClickMagazina() {
    popupUniversal.SetHeaderText(hfState.Get("msgZgjidhMagazinen"));
    popupUniversal.SetContentUrl('LupaMagazina.aspx?aqt=true');
    popupUniversal.SetSize(600, 600);
    popupUniversal.Show();
}

function dateGotFocus(s, e) {
    window.setTimeout(function () { s.SetCaretPosition(0); }, 0);
}
function onTaskRunning() {
    window.parent.SessionTimeout.sendKeepAlive();
}
function onTaskDone() {
    if (ProgressBar1.getValue() == 100)
        myMesazh.ShtoMesazhSuksesi(hfState.Get("msgAmortizimiLlogaritjaPerfundoiMeSukses"));
    else {
        myMesazh.ShtoMesazhInformues(hfState.Get("msgAmortizimiLlogaritjaUNdaluaTek") + ProgressBar1.getValue() + ' %');
    }
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "merrTotalAmortizimi"),
        data: JSON.stringify({})
    }).done(SucceededCallbackTotali);
    grid_faturat.PerformCallback();
}

function SucceededCallbackTotali(result) {
    txtVlefta.SetText(result.totali);
    if (result.mesazhi != "")
        myMesazh.ShtoMesazhInformues(result.mesazhi);
}

function onTaskError() {
    myMesazh.ShtoMesazhGabimi(hfState.Get("msgAmortizimiGabimGjatELlogaritjes"));
}

function kundert() {

    if (gvAsete.cpNoRows > 10 * (gvAsete.cpNoPage + 1)) {
        for (l = 10 * gvAsete.cpNoPage; l < 10 * (gvAsete.cpNoPage + 1) ; l++) {

            gvAsete.SelectRowOnPage(l, !gvAsete.IsRowSelectedOnPage(l));

        }
    }
    else {
        for (l = 10 * gvAsete.cpNoPage; l < gvAsete.cpNoRows; l++) {

            gvAsete.SelectRowOnPage(l, !gvAsete.IsRowSelectedOnPage(l));


        }
    }
}
function KlikoTeGjitha() {
    gvAsete.SelectAllRowsOnPage();

}
function HiqTeGjitha() {
    gvAsete.UnselectRows();


}
/*
Function: TextChangedLloji
    
Ben ndryshime ne gride ne varesi te llojit te veprimit te zgjedhur (hyrje, Dalje apo Transferim)
*/
function TextChangedLloji() {
    var lloji = cmbLloji.GetText();

    var mod;
    if ($("#hfShtimModifikim")[0].value == 'modifikim')
        mod = true;
    else mod = false;
    if (cmbLloji.GetText() != "")
        callWebserviceNiveliNew(cmbLloji.GetValue(), 'amortizimi', mod);
    else
        callWebserviceNiveliNew('', 'amortizimi', mod);
}
function FshiClickedArtPerb(key) {
    grid_faturat.PerformCallback(key);
}

function showPopUpShperndarjeQK(s, e) {
    popMesazhQK.Hide();
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgShperndarjeNeQendratEKostos"), $('#hfUrl').val(), 900, 600);
}
function konrtolloRreshtaTeZgjedhur(s, e) {
    
    $.ajax({
        async: false,
        url: Utils.getServerApiUrl("Rregjistrime", "mbushMagazinenSipasID"),
        data: JSON.stringify({ kodi: cmbMagazina.GetText().substr(0, cmbMagazina.GetText().indexOf(' ')), idNdermarrje: hfState.Get("idNdermarrje") })
    }).done(function (id) {
        if (id == 1) {
            myMesazh.ShtoMesazhGabimi("Nuk mund te kryeni veprime me magazine per artikujt afatshkurter!");
            e.processOnServer = false;
            return;
        }
    });
    if (gvAsete.GetSelectedRowCount() == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniArtikuj"));
        e.processOnServer = false;
    }
    else {
        Utils.shfaqLoadingGif();;
        ProgressBar1.startTask();
    }
}

function clickExport(e) {
    if (grid_faturat.GetVisibleRowsOnPage() == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGridaEFaturaveEshteBosh"));
        e.processOnServer = false;
    }
}
function grid_faturatEndCallback(s, e)
{ 
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "merrTotalAmortizimi"),
        data: JSON.stringify({})
    }).done(SucceededCallbackTotali);
}
function Active_TabChanged(s, e) {
    indexModifiko = gvRregullat.GetFocusedRowIndex();    
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