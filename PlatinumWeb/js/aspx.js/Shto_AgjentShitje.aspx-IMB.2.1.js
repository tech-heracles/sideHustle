; var editorGlobal;
var widthLupaLlogaria = 660;
var heightLupaLlogaria = 600;
var widthLupaNivelZbritje = 600;
var heightLupaNivelZbritje = 600;
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
    var options = JSON.parse(hfState.Get("colAutorizime"));
    hfState.Remove("colAutorizime");
    cmbAutorizimi = new MultiSelect({
        container: "tblAgjenteShitje",
        valueField: 'KodiAutorizim',
        labelField: 'KodiAutorizim',
        searchField: 'KodiAutorizim',
        options: options,
        meLupe: true,
        onButtonClickLupa: Autorizime_Click,
        multiSelectId: "cmbAutorizimi"

    })
    changeName();
});

function callWebserviceKonfigurimi(idKomp, kodKonf) {
    var idGjuha = hfState.Get('idGjuha');
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
}

function callWebserviceKonfigurimiInit(idKomp, kodKonf) {
    $("#dvAgjenteShitje").show()
    var idGjuha = hfState.Get('idGjuha');
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
}

function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("PershkrimKonfigAmbjente"));
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("KodKonfigAmbjente"));

    callWebserviceKonfigurimi("149", cmbKonfigurimi.GetText());
}

var editorValues = new Object();
var identikuesPerPopupLlogari = "AgjenteShitje";

function Autorizime_Click() {
    KPF = 0;
    var hf = $("#hfLupaAutorizimi")[0];
    var queryStr = hf.value;
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhniAutorizimet"), 'LupaAutorizim.aspx?autorizimet=' + cmbAutorizimi.GetText(), widthLupaNivelZbritje, heightLupaNivelZbritje);
}
function Perdorues_Click() {
    var hf = $("#hfLupaAutorizimi")[0];
    var queryStr = hf.value;
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhniPerdoruesin"), 'LupaPerdorues.aspx?vjenNga=Agjenti&&perdoruesi=' + btnPerdoruesi.GetText(), widthLupaNivelZbritje, heightLupaNivelZbritje);
}

function Drejtori_Click() {
    var queryStr = Utils.KonvertoObjectQueryString({
        vjenNga: 'Agjenti',
        arsye: 'zgjidhDrejtor',
        agjenti: $('#hfId')[0].value,
        drejtori: cmbDrejtori.GetValue(),

    });
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhniDrejtorin"), 'LupaPerdorues.aspx?' + queryStr, widthLupaNivelZbritje, heightLupaNivelZbritje);
}

function ndryshoKonfiguriminInit() {
    //callWebserviceKonfigurimiInit("149" + ";" + cmbKonfigurimi.GetText());
    callWebserviceKonfigurimiInit("149", cmbKonfigurimi.GetText());
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ? evt.keyCode : evt.charCode;
}



function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('Shto_AgjentShitje.aspx', 0, hf);
    myMenu.PercaktoMenuSipasTabit(0, hfTeDrejta, $('#hfShtimModifikim'));
}

var grida;
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
    //        indexModifiko = myFaqeCelje.EndRequestHandler(sender, args, hf, hfKontrollet, hfShtimModifikim, hfId, indexModifiko, PageControl, gvAfateMaturimi, "426")
    indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, grid_AgjenteShitje, "149", pastrofusha, hfTeDrejta);
}

function Init() {
    editorValues["KodiAgjentShitje"] = "";
    editorValues["EmriAgjentShitje"] = "";
    editorValues["MbiemriAgjentShitje"] = "";
    editorValues["TelAgjentShitje"] = "";
    editorValues["FaxAgjentShitje"] = "";
    editorValues["EmailAgjentShitje"] = "";
    editorValues["IdQyteti"] = "";
    editorValues["PerqindjeAgjentShitje"] = "";
    editorValues["IdLlogari"] = "";
    var hf = document.getElementById("HiddenField1");
    hf.value = '';
}

function InitLlogari() {
    var hf = document.getElementById('HiddenField1');

    if (hf.value == '') {
        editorValues["KodiAgjentShitje"] = "";
        editorValues["EmriAgjentShitje"] = "";
        editorValues["MbiemriAgjentShitje"] = "";
        editorValues["TelAgjentShitje"] = "";
        editorValues["FaxAgjentShitje"] = "";
        editorValues["EmailAgjentShitje"] = "";
        editorValues["IdQyteti"] = "";
        editorValues["PerqindjeAgjentShitje"] = "";
        editorValues["IdLlogari"] = "";
        hf.value = editorValues["KodiAgjentShitje"] + ";" + editorValues["EmriAgjentShitje"] + ";"
                + editorValues["MbiemriAgjentShitje"] + ";" + editorValues["TelAgjentShitje"] + ";"
                + editorValues["FaxAgjentShitje"] + ";" + editorValues["EmailAgjentShitje"] + ";"
                + editorValues["IdQyteti"] + ";" + editorValues["PerqindjeAgjentShitje"] + ";"
                + editorValues["IdLlogari"];
    }
    var listeFushash = hf.value.split(';');
    IdLlogari = Utils.ktheKontroll('IdLlogari');

    KodiAgjentShitje.SetText(listeFushash[0]);
    EmriAgjentShitje.SetText(listeFushash[1]);
    MbiemriAgjentShitje.SetText(listeFushash[2]);
    TelAgjentShitje.SetText(listeFushash[3]);
    FaxAgjentShitje.SetText(listeFushash[4]);
    EmailAgjentShitje.SetText(listeFushash[5]);
    if (listeFushash[6] != "")
        IdQyteti.SetText(listeFushash[6]);
    PerqindjeAgjentShitje.SetText(listeFushash[7]);
    IdLlogari.SetText(listeFushash[8]);

}

var IdLlogari;
var grida;

function Llogari_Click() {
    var hf = $("#hfLupaLlogaria")[0];
    var queryStr = hf.value;
    myButtonClickLupa.ButtonClickLlogaria(hfState.Get("popupAdministrimiUniversal"), queryStr, widthLupaLlogaria, heightLupaLlogaria);
}



function ProcessTextChanged(fieldName, value) {
    editorValues[fieldName] = value;
    var hf = document.getElementById("HiddenField1");
    hf.value = editorValues["KodiAgjentShitje"] + ";" + editorValues["EmriAgjentShitje"] + ";"
                + editorValues["MbiemriAgjentShitje"] + ";" + editorValues["TelAgjentShitje"] + ";"
                + editorValues["FaxAgjentShitje"] + ";" + editorValues["EmailAgjentShitje"] + ";"
                + editorValues["IdQyteti"] + ";" + editorValues["PerqindjeAgjentShitje"] + ";"
                + editorValues["IdLlogari"];
}

function KeyPresLlogari(kodi, editor, key) {
    if (kodi == 13) {
        indeksi = key;
        IdLlogari = Utils.ktheKontroll('IdLlogari');
        grida = true;
        Llogari_Click();

        editorValues["IdLlogari"] = IdLlogari.GetValue();

        var hf = document.getElementById("HiddenField1");
        hf.value = editorValues["KodiAgjentShitje"] + ";" + editorValues["EmriAgjentShitje"] + ";"
                + editorValues["MbiemriAgjentShitje"] + ";" + editorValues["TelAgjentShitje"] + ";"
                + editorValues["FaxAgjentShitje"] + ";" + editorValues["EmailAgjentShitje"] + ";"
                + editorValues["IdQyteti"] + ";" + editorValues["PerqindjeAgjentShitje"] + ";"
                + editorValues["IdLlogari"];
    }
}

function LostFocusLlogari(key) {
    indeksi = key;
    IdLlogari = Utils.ktheKontroll('IdLlogari');
    editorValues["IdLlogari"] = IdLlogari.GetText();

    var hf = document.getElementById("HiddenField1");
    hf.value = editorValues["KodiAgjentShitje"] + ";" + editorValues["EmriAgjentShitje"] + ";"
                + editorValues["MbiemriAgjentShitje"] + ";" + editorValues["TelAgjentShitje"] + ";"
                + editorValues["FaxAgjentShitje"] + ";" + editorValues["EmailAgjentShitje"] + ";"
                + editorValues["IdQyteti"] + ";" + editorValues["PerqindjeAgjentShitje"] + ";"
                + editorValues["IdLlogari"];
}


function ButtonClickedLlogari(editor, key) {
    indeksi = key;
    IdLlogari = editor;
    grida = true;
    Llogari_Click();
}

function TextChangedLlogari(key) {
    indeksi = key;
    var a = new Array();
    IdLlogari = Utils.ktheKontroll('IdLlogari');

    a = IdLlogari.GetText().toString().split(',');

    IdLlogari.SetText(a[0]);

    editorValues["IdLlogari"] = IdLlogari.GetText();

    var hf = document.getElementById("HiddenField1");
    hf.value = editorValues["KodiAgjentShitje"] + ";" + editorValues["EmriAgjentShitje"] + ";"
                + editorValues["MbiemriAgjentShitje"] + ";" + editorValues["TelAgjentShitje"] + ";"
                + editorValues["FaxAgjentShitje"] + ";" + editorValues["EmailAgjentShitje"] + ";"
                + editorValues["IdQyteti"] + ";" + editorValues["PerqindjeAgjentShitje"] + ";"
                + editorValues["IdLlogari"];
}



function valido(s, e) {
    myFaqeCelje.valido(s, e, PageControl, hfTeDrejta, $('#hfShtimModifikim'));
}

//pati

//variabla per te kaluar nga nje reshti i grides tek tjetri
var indexSel = 0;
//indexi i zgjedhur per modifikim
var indexModifiko;
//perdoret per te treguar nese duhet te mbushen tabet apo jo kjo perdoret qe tabet te mbushen vetem heren e pare dhe jo pas cdo here qe  kalohet nga nje tab tek tjetri
var mbush = true;
//perdoret per te daluar rastet kur bejme double click apo thjesht click ne menyre qe ne double click te kaloje tek tabi i dyte
var kaloTab = false;
//perdoret per griden e llogarive ne menyre qe te lodohet vetem ne momentin kur shkon ne tabin e saj
var lista = true;
var btnFiltrat;
var colKontrollet, colAtrTrupi;

function checkText(s, e) {
    myMenu.checkText(s, e);
}

function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, grid_AgjenteShitje, "149", cmbKonfigurimi.GetText());
}
function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

function menu_click(s, e) {
    Utils.vendosVlereNeHiddenField("cmbAutorizimiHf", cmbAutorizimi.GetText());
    hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    hfId = $('#hfId'); //hidden fieldi qe ruan id  e rreshtit te selektuar
    myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, false, undefined, indexModifiko, pastrofusha, SucceededCallbackKonfig, resultkonf, colKontrollet, aktivizoFusha, colAtrTrupi);
}



function aktivizoFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $('#hfShtimModifikim');
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
}

function SucceededCallbackKonfig(result) {
    if (result != "" && result != null) {
        colKontrollet = result.colKontrollet;
        colAtrTrupi = result.colAtrTrupi;
        resultkonf = result;
        //Lupa(kontrollet);
        LupaKontrollet(colKontrollet, colAtrTrupi);
        var hf = $('#hfKontrollet');
        var hfLidhur = $("#hfLidhur");
        var hfMod = $('#hfShtimModifikim');
        var arrTabela = ['tblAgjenteShitje'];
        //myFaqeCelje.SucceededCallbackKonfig(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "#ASPxPageControl1_", "ASPxPageControl1_C");
        // myFaqeCelje.SucceededCallbackKonfigSlim(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "#ASPxPageControl1_", "ASPxPageControl1_C");
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "ASPxPageControl1_C");
    }
    //$("#dvAgjenteShitje").show();//$("#dvAgjenteShitje")[0].style.visibility = 'visible';
}

function LupaKontrollet(kontrollet, colAtrTrupi) {
    var hf1 = $("#hfLupaLlogaria");
    var hf2 = $("#hfLupaAutorizimi");
    for (var i = 0; i < kontrollet.length; i++) {
        //id e konfigurimit te lupes vendoset ne hidden field
        //vlera e saj do t'i kalohet si query string hapjes se popUp-it
        if (kontrollet[i].KodKontrolli == "txtLlogari") {
            hf1.val(colAtrTrupi[i].IdKonfigAmbjenteLupa)
            continue;
        }
        if (kontrollet[i].KodKontrolli == "cmbAutorizimi") {
            hf2.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
            continue;
        }
    }
}

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}

function OnGridDoubleClick(index) {
    indexModifiko = index;
    lista = true;
    mbushfusha();
}

function mbushfusha() {
    mbush = false;
    $('#hfShtimModifikim')[0].value = "modifikim";
    indexModifiko = grid_AgjenteShitje.GetFocusedRowIndex();
    if (indexModifiko == -1)
        myMesazh.ShtoMesazhGabimi(hfState.Get("MsgBlerjeShitjeAfatMaturimi"));
    else
        grid_AgjenteShitje.GetRowValues(indexModifiko, 'IdAgjentShitje;KodiAgjentShitje;EmriAgjentShitje;MbiemriAgjentShitje;TelAgjentShitje;FaxAgjentShitje;EmailAgjentShitje;IdQyteti;PerqindjeAgjentShitje;IdLlogari;IdNdermarje;IdKonfig;IdStatusDok;IdPerdoruesi;DtKrijimi;DtModifikimi;IdPerdoruesMobile;PerdoruesUsernameMobile;IdDrejtori', OnGetRowValuesMod);
    Utils.shfaqLoadingGif();;
}

function OnGetRowValuesMod(values) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;
    $('#hfId')[0].value = values[0];
    kodiTextBox.SetText(values[1]);
    if ($('#hfShtimModifikim').val() == "modifikim") {
        kodiTextBox.SetEnabled(false);
    }
    emriTextBox.SetText(values[2]);
    mbiemriTextBox.SetText(values[3]);
    telTextBox.SetText(values[4]);
    faxTextBox.SetText(values[5]);
    emailTextBox.SetText(values[6]);
    qytetiASPxComboBox.SetValue(values[7]);
    perqindjeASPxTextBox.SetText(values[8]);
    if (values[17] != null && values[17] != "")
        btnPerdoruesi.SetText(values[17]);
    else btnPerdoruesi.SetValue(null);
    if (values[18] != null && values[18] != "")
        cmbDrejtori.SetValue(values[18]);
    else   cmbDrejtori.SetValue(null);
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "callWsGetAutorizimeSipasLlojitDheIdLidhese"),
        data: JSON.stringify({ idLidhese: values[0], kodLloji: 'AgjentShitje', idPerdorues: hfState.Get('idPerdoruesi') })
    }).done(SucceededCallbackKtheAutorizime);

    if (values[9] != null) {
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "ktheNrLlogarie"),
            data: JSON.stringify({ prefixText: values[9] })
        }).done(SucceededCallbackLlogariID);
    }
    else {
        txtLlogari.SetValue(null);
    }
    cmbAutorizimi.SetValue(null);
    var idGjuha = hfState.Get('idGjuha');
    var idNdermarrje = hfState.Get('idNdermarrje');
    //therret web service per te kontrolluar nese kjo llogari standarte eshte e lidhur apo jo
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "eshteLidhur"),
        data: JSON.stringify({ idkomp: "426", kodkonfi: cmbKonfigurimi.GetText(), iddokumenti: values[0], idNdermarrje: idNdermarrje, idGjuha: idGjuha })
    }).done(function (result) { SucceededCallbackLidhur(result, values[0]) });
    if (kaloTab) {
        PageControl.SetActiveTabIndex(1);
        myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, $('#hfShtimModifikim'));
    }
}
function SucceededCallbackLlogariID(result) {
    txtLlogari.SetText(result);
}

function SucceededCallbackKtheAutorizime(result) {
    if (result.idLidhese == $('#hfId').val())
        cmbAutorizimi.SetText(result.autorizime);
    else cmbAutorizimi.SetValue(null);
}

function pastrofusha() {
    kodiTextBox.SetText('');
    emriTextBox.SetText('');
    mbiemriTextBox.SetText('');
    telTextBox.SetText('');
    faxTextBox.SetText('');
    emailTextBox.SetText('');
    perqindjeASPxTextBox.SetText('');
    qytetiASPxComboBox.SetValue('');
    txtLlogari.SetValue('');
    cmbAutorizimi.SetValue(null);
    btnPerdoruesi.SetValue(null);
    var hf = $('#hfKontrollet')[0];
    aktivizoFusha(colKontrollet, colAtrTrupi, false);
}

function SucceededCallbackLidhur(result, idObjekti) {

    if (idObjekti.toString() !== $('#hfId').val()) {
        Utils.hiqLoadingGif();;
        return;
    }
    //        var hf = $('#hfKontrollet')[0]; //mban te dhenat mbi kontrollet
    var hfLidhur = $("#hfLidhur")[0];
    hfLidhur.value = result;
    aktivizoFusha(colKontrollet, colAtrTrupi, eval(result.toLowerCase()));
}

function LostFocusLlogaria() {
    var vlera = txtLlogari.GetText();
    if (vlera != '')
        $.ajax({
            url: Utils.getServerApiUrl("Konfigurime", "ekzistonNrLlogarie"),
            data: JSON.stringify({ prefixText: vlera, idNdermarrje: hfState.Get('idNdermarrje') })
        }).done(SucceededCallbackLlogaria);
}

function SucceededCallbackLlogaria(result) {
    if (result == false) {
        txtLlogari.SetValue(null);
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgCeljeArkaBankaLlogariaNukEkziston"));
    }
}

function nrLlogariChange() {
    var s = txtLlogari.GetText().split(';');
    txtLlogari.SetText(s[0]);
}

function activeTabChanged(s, e) {
    indexModifiko = grid_AgjenteShitje.GetFocusedRowIndex();
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
