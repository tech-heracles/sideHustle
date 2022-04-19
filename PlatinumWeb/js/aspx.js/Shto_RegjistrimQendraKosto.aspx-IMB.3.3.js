; //  PatchJQuery.myPatchJQuery();
var formati = 6;
var pershk = 1;
var arrFormati = new Array();
var arrayMeMonedha = new Array();
var arrayIdKolonaGrides = new Array();
var arrayPershkrimiKolonaGrides = new Array();
var arrayVisibleKolonaGrides = new Array();
var arrayWidthKolonaGrides = new Array();
var arrayReadOnlyKolonaGrides = new Array();
var arrayRenditjeKolonaGrides = new Array();
var arrayIndexTrupi = new Array();
var colNorma = new Array();
var keyGlobal;
var editorKodi;
var editorEmertimi;
var editorVlera;
var numerReshtashQeShtohen;
var identifikuesPerPopupDokumentat = "QendraKosto";
var identikuesPerPopupLlogari = "RegjistrimQendraKostoDXDATAGRID";
var KPF;
var varKonfig = {
    identifikuesPerLocalStorageKey: 'RegjistrimQendraKosto'
};
var azhornim = false;
var kushtet;
var colKushte, colAlterKusht, colKontrollet, colAtrTrupi;
var colGrida;
var formatNumriZgjedhur;
var trupibosh;

var regjQK;
var gridaQK;

Array.prototype.ekzistonElementi = function (value) {
    
    for (var i = 0, loopCnt = this.length; i < loopCnt; i++) {
        if (this[i] == value) {
            return true;
        }
    }
    return false;
};

$(document).ready(function () {
    Utils.konfiguroAccorditionNeDocReady(varKonfig.identifikuesPerLocalStorageKey);
    $(window).on('resize', function () {
        try {
            if (Utils.isGridResized())
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
                break;
            default:
                break;
        }
    });
    $(window).on('load', function () {
        Init();
        Utils.resizeSplitter();
    });
});

function ButtonClickKerko(listUrl) {
    /*
    Function: ButtonClickKerko    
    Hap lupen e dokumentave.
    */
    var queryString = {
        veprimi: 'QendraKosto',
        listUrl: listUrl
    };
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhDokumentin"), 'LupaDokumenta.aspx?' + Utils.KonvertoObjectQueryString(queryString), 950, 560);
}

function Init() {
    if (typeof (isPostBack) == "undefined") {
        document.getElementById("kokeKonfigurimi").innerHTML = hfState.Get("MenuKokeDokumenti");
        document.getElementById("trupKonfigurimi").innerHTML = hfState.Get("MenuTrupDokumenti");
        document.getElementById("fundKonfigurimi").innerHTML = hfState.Get("MenuFundDokumenti");
        var hf = $("input[id$='hfKonffillestar']")[0];

        //cmbKonfigurimi.SetText(hf.value.split(';')[0]);
        $('#kokeKonfigurimi').text(hfState.Get("MenuKokeDokumenti") + ': ' + hf.value.split(';')[1]);
        callWebserviceKonfigurimi("906", hf.value.split(';')[0]);
        changeName();
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(EndRequestHandler);
        myMesazh.shtoHandler();
    }
}

function ruajFormatetNeGride(formatNumri) {
    /*
    Vendos formatet e numrave ne gride ne baze te emrit te kolones
    */
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
    Utils.setFormatNumri(txtKrediMonedhaBaze, formatNumri.KonfigTrupi[0].ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtDebiMonedhaBaze, formatNumri.KonfigTrupi[0].ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtKrediDiferenca, formatNumri.KonfigTrupi[0].ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtDebiDiferenca, formatNumri.KonfigTrupi[0].ShifraPasPresjesVlefta);
}

/*
Formaton vlerat e fushave sipas formatit perkates ne te gjithe rreshtat e grides
*/
function vendosKonfigFormatNumri() {
    formatoFushaDevi();
}
function dateGotFocus(s, e) {
    window.setTimeout(function () { s.SetCaretPosition(0); }, 0);
}
function formatoFushaDevi() {
    Utils.formatoTextBox(txtKrediMonedhaBaze);
    Utils.formatoTextBox(txtDebiMonedhaBaze);
    Utils.formatoTextBox(txtDebiDiferenca);
    Utils.formatoTextBox(txtKrediDiferenca);
}

function gjejFormatSipasMonedhes(idmonedha) {
    var formatNumri = $.parseJSON(hfState.Get("formatMonedhe"));
    for (i = 0; i < formatNumri.KonfigTrupi.length; i++)
        if (formatNumri.KonfigTrupi[i].IdMonedha == idmonedha) {
            return formatNumri.KonfigTrupi[i];
        }
    return formatNumri.KonfigTrupi[0];
}

var arrformatevlefta = new Array();
var arrformatevleftaqk = new Array();
var arrformatevleftamon = new Array();

function unformatoFushaDevi() {
    Utils.unFormatoTextBox(txtKrediMonedhaBaze);
    Utils.unFormatoTextBox(txtDebiMonedhaBaze);
    Utils.unFormatoTextBox(txtDebiDiferenca);
    Utils.unFormatoTextBox(txtKrediDiferenca);
}

function changeName() {
    myFaqeCelje.shtoHandlerSession();
    try {
        if (Utils.getUrlVar('id') == undefined)
            window.parent.callWebServiceKtheInfoLart('Shto_RegjistrimQendraKosto.aspx', 0);
        else
            window.parent.callWebServiceKtheInfoLart('Shto_RegjistrimQendraKosto.aspx', Utils.getUrlVar('id'));
        window.parent.createCookie('adresa', window.location.href, 1);
    } catch (ee) { }
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
                idKomp: idKomp, kodKonf: kodKonf, kodKontrolli: "", idObjekti: -1, shtim: true, merrFormatKursi: true, merrGjitheKonf: true,
                idGjuha: idGjuha, idPerdoruesi: idPerdoruesi, idNdermarrje: idNdermarrje
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


function SucceededCallbackKonfig(result) {
    var hf = $("input[id$='hfShtimModifikim']");
    vendosDateDefault();
    var hfLidhur = $("input[id$='hfLidhur']");
    var arrTabela = ['tblFleteKontabel', "tblTotale"];
    var arrPrind = ["dvFillim", "dvFundi"];
    colKontrollet = result.colKontroll;
    colAtrTrupi = result.colAtrTrupi;
    var vlerat = myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, undefined, hf, '', arrTabela, undefined, undefined, hfLidhur, arrPrind);
    colKontrollet = result.colKontroll;
    colAtrTrupi = result.colAtrTrupi;
    colGrida = result.colGrida;
    colKushte = result.colKushte;
    colAlterKusht = result.colAlterKusht;
    $("input[id$='HfGridCol']").val(JSON.stringify(colGrida));
    azhornim = false;
    txtNrDok.SetEnabled(hf.val() == "shtim");
    dteDtDok.SetEnabled(hf.val() == "shtim");
    dteDtDok.SetEnabled(hf.val() == "klonim");

    formatNumriZgjedhur = result.formatNumri;
    formatKursi = result.formatKursi;
    regjQK = new dxQendraKosto();
    gridaQK = regjQK.initGridQK({ ngaThirret: 'regj', formatNr: formatNumriZgjedhur, konfigurimGride: colGrida, PershkrimiKokes: txtShenime.GetText() }, hf.val(), { idKoka: hfState.Get("id"), dteDtDok: dteDtDok.date.toDateString(), dteDtRegj: dteDtRegjistrimi.date.toDateString(), idKonfig: cmbKonfigurimi.GetValue() }, { idDokGjenerues: hfState.Get("idDokGjenerues"), idKonfigGjenerues: 0, llogariFK: null });
    regjQK.setGridEditableOrNot(hfLidhur.val() == 'True');
    //cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);
    $("#gvGrida").show();
    $("#dvFillim").show();
    $("#dvFundi").show();
    $('#hfKontrolletNrAutom').val(JSON.stringify(colKontrollet));
    $('#hfAtributeNrAutom').val(JSON.stringify(colAtrTrupi));

    ruajFormatetNeGride(formatNumriZgjedhur);
    Utils.konfiguroAccorditionPasKonfigDokumenti(varKonfig.identifikuesPerLocalStorageKey);
}

function ndryshoKonfigurimin() {
    var cmbModKontroll = cmbKonfigurimi.GetSelectedItem();
    var pershkKonfigAmb = cmbModKontroll.GetColumnText("PershkrimKonfigAmbjente");
    if (pershkKonfigAmb != undefined)   
        $('#kokeKonfigurimi').text(hfState.Get("MenuKokeDokumenti") + ': ' + pershkKonfigAmb);
    callWebserviceKonfigurimi("906", cmbKonfigurimi.GetText());
}


function pastro() {
    txtNrDok.SetText('');
    txtNrDok.SetEnabled(true);
    dteDtDok.SetDate(new Date());
    txtShenime.SetText('');

    $("input[id$='hfStatusRuajtje']")[0].value = "false";
    txtDebiDiferenca.SetText('0.00');
    txtKrediDiferenca.SetText('0.00');

    txtDebiMonedhaBaze.SetText('0.00');
    txtKrediMonedhaBaze.SetText('0.00');
    cmbLloji.SetSelectedIndex(0);
    cmbQendraKosto.SetValue(null);
    cmbObjektiva.SetValue(null);
    txtNrRef.SetText('');
    vendosDateDefault();
    hfState.Set("idDokGjenerues", 0);
}

function vendosDateDefault() {
    var hf = $("input[id$='hfShtimModifikim']").val();
    switch (hf) {
        case "klonim":
            var dataSot = Utils.zeroOren(new Date());
            dteDtRegjistrimi.SetDate(dataSot);
            break;
        case "shtim":
            try {
                var hfPeriudheObj = window.parent.lexoHfPeriudhe();
                dteDtDok.SetDate(Utils.ktheDateDefault(hfPeriudheObj));
            }
            catch (ee) {
                console.log(ee);
            }
            break;
    }
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ? evt.keyCode : evt.charCode;
}

function EndRequestHandler(sender, args) {
    // This code is executed after a successful async. postback
    formatoFushaDevi();
    var statusRuajtje = $("input[id$='hfStatusRuajtje']")[0];
    var statusRuajtjeQendra = $("input[id$='hfStatusQendra']")[0];
    if (statusRuajtjeQendra.value == "true") {
        ASPxMenu1.GetItemByName('Shto').SetVisible(true);
        myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimQendraKosto.aspx', true);
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
    else return true;
}

function PastroClick() {
    ASPxMenu1.GetItemByName('Fshi').SetVisible(false); ASPxMenu1.GetItemByName('Draft').SetVisible(true);
    var statusRuajtjeQendra = $("input[id$='hfStatusQendra']")[0];
    var hf = $("input[id$='hfShtimModifikim']");
    hf.val("shtim");
    pastro();
    myMenu.menuSipasTeDrejtaRegjistrim(hf, hfTeDrejta);
    callWebserviceNiveliNew('', 'qendrakosto', false);
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
    } catch (e) {
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

function SucceededCallbackNiveli(result) {
    /*
    Function: SucceededCallbackNiveli
        
    Ndryshon vleren e zgjedhur te konfigurimit sipas nivelit te zgjedhur.
    Therret funksionin <ndryshoKonfigurimin>.
    */
    var vlerat = '';
    vlerat = result.split('|');
    cmbKonfigurimi.ClearItems();
    for (i = 0; i < vlerat.length - 1; i++) {
        var arr = vlerat[i].split(',')[1].split(';');
        cmbKonfigurimi.AddItem(arr, vlerat[i].split(',')[0]); //AddItem(teksti, vlera);
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
        if (e.item.name == 'Ruaj' || e.item.name == 'Draft') {
            myFaqeCelje.validim(s, e);
            e.processOnServer = false;
            if (validoClientSide()) {
                Utils.shfaqLoadingGif();
                regjQK.ruajRegjQK(true, txtNrDok.GetText(), txtNrRef.GetText() == "" ? 0 : txtNrRef.GetText(), txtShenime.GetText(), (e.item.name == 'Ruaj') ? 1 : 0, "Shto_RegjistrimQendraKosto.aspx", $('#hfLidhur').val().toString().toLowerCase() == 'true', dteDtDok.date.toDateString(), dteDtRegjistrimi.date.toDateString());
            }
            else {
                click = false;
                Utils.hiqLoadingGif();
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgPlotesoniTeGjithaFushat"));
            }
        }
        else if (e.item.name == "Klono") {
            $("input[id$='hfShtimModifikim']").val('klonim');
            $("input[id$='hfLidhur']").val(false);
            lidhur = false;
            click = false;
            myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, $("input[id$='hfShtimModifikim']"), '');
            $('#ASPxSplitter1_hl').empty();
            ruajFormatetNeGride(formatNumriZgjedhur);
            txtNrRef.SetText('');
            ASPxMenu1.GetItemByName('Shto').SetVisible(true);
            vendosDateDefault();
            myMenu.menuSipasTeDrejtaRegjistrim($("input[id$='hfShtimModifikim']"), hfTeDrejta);
        }
        else if (e.item.name == 'Fshi') {
            popFshi.Show();
            e.processOnServer = false;
            click = false;
        }
        else if (e.item.name == 'Kerko') {
            myFaqeCelje.kontrolloTeDrejta("RegjistrimQendraKosto.aspx", null, true);
            e.processOnServer = false;
            click = false;
        }
        else if (e.item.name == 'Shto') {
            $("input[id$='hfLidhur']").val(false);
            myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimQendraKosto.aspx', true);
            e.processOnServer = true;
            click = false;
        }
        else if (e.item.name == 'Anullo') {
            myFaqeCelje.kontrolloTeDrejta("RegjistrimQendraKosto.aspx");
            e.processOnServer = false;
            click = false;
        }

    }
}

function Kthehu(mesazh) {
    myFaqeCelje.kontrolloTeDrejta("RegjistrimQendraKosto.aspx?ruaj=po");
}

function ButtonClickQendraKosto(mag) {//po
    if (cmbLloji.GetValue() == 1)
        myButtonClickLupa.LupaUniversal_Click(hfState.Get("headerPopUpZgjidhQendrenKostos"), 'Shto_QendraKosto.aspx?lupe=true&llojLupe=plote&vjenNga=RegjistrimQendraKosto', 900, 600);
    else myButtonClickLupa.LupaUniversal_Click(hfState.Get("headerPopUpZgjidhSkemenKostos"), 'LupaSkemaKosto.aspx?vjenNga=RegjistrimQendraKosto', 600, 600);

}
function ButtonClickObjektiva() {
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhniObjektivenEKostos"), 'LupaObjektivaKosto.aspx?vjenNga=Shto_Punonjes', 600, 600);
}
/*
Function: TextChangedMagazina
    
Vendos ne gride magazinen qe zgjidhet te koka
*/
function TextChangedQendraKosto() {//po
    var hfLidhur = $("input[id$='hfLidhur']");
    if (hfLidhur.val() == 'True')
        return;
    magazina1 = cmbQendraKosto.GetText();
    if (magazina1 != "")
        regjQK.vendosQKapoSkemeNeGride(cmbLloji.GetValue(), cmbQendraKosto.GetText(), $('#hfShtimModifikim').val());
}
function TextChangedObjektiva() {//po
    var hfLidhur = $("input[id$='hfLidhur']");
    if (hfLidhur.val() == 'True')
        return;
    magazina1 = cmbObjektiva.GetText();
    if (magazina1 != "")
        regjQK.vendosObjektiveNeGride(cmbObjektiva.GetText());
}
function TextChangedPershkrimi(s, e) {//po
    regjQK.vendosPershkrimNeGride(txtShenime.GetText());
}

function InitArtPerb() {
    keyGlobal = -1;
    numerReshtashQeShtohen = 1;
}

var indeksiArtPerb;
var tekstiShkruar;

function DateChanged() {
    regjQK.kontrolloNdryshimeKursiNgaNdryshimiDates(dteDtDok.date.toDateString());
}

var editorNjesia;

function llogaritTotalet(totaletMonBaze) {
    if (totaletMonBaze == undefined || totaletMonBaze == null)
        totaletMonBaze = regjQK.ktheTotaleDebiKrediMonBaze();
    txtDebiMonedhaBaze.SetText(totaletMonBaze.Debi);
    txtKrediMonedhaBaze.SetText(totaletMonBaze.Kredi);
    txtDebiDiferenca.SetText(totaletMonBaze.Debi - totaletMonBaze.Kredi > 0 ? totaletMonBaze.Debi - totaletMonBaze.Kredi : 0);
    txtKrediDiferenca.SetText(totaletMonBaze.Debi - totaletMonBaze.Kredi < 0 ? totaletMonBaze.Kredi - totaletMonBaze.Debi : 0);
}