; //Deklarimi i variablave te faqes

var arrNrLlogarie = new Array();
var arrEmerLlogarie = new Array();
var arrVlefteLlogarie = new Array();
var editorNrLlogarie;
var editorEmerLlogarie;
var editorVlefteLlogarie;
var indexCounter;
var indeksPerEmerLlogarie;
var counterNrLlogarie = 0;
var counterVlefteLlogarie = 0;
var comboNrFature;

var arrNrFature = new Array();
var arrDtFature = new Array();
var arrFurnitori = new Array();
var arrArtikujt = new Array();
var arrKokaFB = new Array();

var arrTaksat = new Array();

var lastsel1 = 1;
var lastselsub = 1;
var lastselsubgrid = "";
var totaliSasive = 0;
var totaliVleftavePaTvsh = 0;
var totaliVleftesShpenzimit = 0;

//Variablat qe ruajne konfigurimin e grides se faturave te blerjes
var arrayIdKolonaGrides = new Array();
var arrayPershkrimiKolonaGrides = new Array();
var arrayVisibleKolonaGrides = new Array();
var arrayWidthKolonaGrides = new Array();
var arrayReadOnlyKolonaGrides = new Array();
var arrayRenditjeKolonaGrides = new Array();

//Variablat qe rujane konfigurimin e subgrides per trupin e faturave te blerjes
var arrayIdKolonaSubGrides = new Array();
var arrayPershkrimiKolonaSubGrides = new Array();
var arrayVisibleKolonaSubGrides = new Array();
var arrayWidthKolonaSubGrides = new Array();
var arrayReadOnlyKolonaSubGrides = new Array();
var arrayRenditjeKolonaSubGrides = new Array();
var arrayIndexTrupi = new Array();

var arrKodi = new Array();
var arrPershkrimi = new Array();
var arrVlera = new Array();
var arrTvsh = new Array();

var cou1 = 0;
var indeksi = -1;
var editorKodi;
var editorPershkrimi;
var editorVlera;
var editorTvsh;

var indexCounter = 0;
var lidhur = false;

var editorTransporti;
var editorSiguracion;
var editorTjera;
var editorVlDoganore;
var editorTaksa;

//Variablat qe percaktojne madhesine e lupave te faqes
var widthLupaPeriudha = 600;
var heightLupaPeriudha = 600;
var widthLupaFatura = 600;
var heightLupaFatura = 600;
var widthLupaNrDok = 600;
var heightLupaNrDok = 600;
var lastselsub = 0;

var identikuesPerPopupKursi = "ShtoFleteDoganore";
var llojkursi = 1; //perdoret per te ruajtur llojin e kursit te zgjedhur tek konfigurimi. Ne qofte se nuk ka asnje lloj te zgjedhur, atehere merret lloji i pare.
var widthLupaKursi = 950;
var heightLupaKursi = 560;

var varKonfig = {
    identifikuesPerLocalStorageKey: 'FleteDoganore'
};

jQuery(document).ready(function () {
    Utils.konfiguroAccorditionNeDocReady(varKonfig.identifikuesPerLocalStorageKey);
    $(window).on('resize', function () {
        try {
            if (Utils.isGridResized())//if (!Utils.resizeSplitter(165))
                return;
        }
        catch (ee) {
        }
        var grida = $("#rowed5");
        if ($('#divgride2').width() != null) {
            grida.setGridWidth($('#divgride2').width() - 20, true);
        }

    }).trigger('resize');



    /***********   Function: ekzekutohet sa here i behet resize faqes, dhe ben resize te grides  *******/


    $(window).on('load', function () {
        if ($('#hfShtimModifikim').val() == 'modifikim') {
            var arrid = JSON.parse($('#hfIdFaturave').val());
            grid_faturat.SelectRowsByKey(arrid);
        }
        Init();
        Utils.resizeSplitter();
    });

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

});
function ruajKolonatEGrides(grida) {
    var idGride = colGrida[0].IdKoka;
    var idGjuha = hfState.Get('_idGjuha');
    var idNdermarrje = hfState.Get('_idNdermarrje');
    var idPerdoruesi = hfState.Get('_idPerdoruesi');
    var idViti = hfState.Get('_idViti');
    myJQGrid.ruajKolonatEGrides(grida, idGride, idGjuha, idNdermarrje, idViti, idPerdoruesi)
}

function validim(s, e) {
    myFaqeCelje.validim(s, e);
}
function formGridColsArray() {//po
    var IdKonfigAmbjenteLupat = [];
    myJQGrid.formArrayKolGrides($("#HfGridCol"), arrayIdKolonaGrides, arrayPershkrimiKolonaGrides, arrayVisibleKolonaGrides, arrayReadOnlyKolonaGrides, lidhur, arrayWidthKolonaGrides, IdKonfigAmbjenteLupat, arrayRenditjeKolonaGrides);

}

/*
Merr nga hiddenFiled-i hfKolonaSubGride te dhenat mbi konfigurimin e subgrides ne trajten e nje objekti colTrupiGrida
Dhe i ruan keto te dhena ne vektoret perkates : arrayIdKolonaSubGrides, arrayPershkrimiKolonaSubGrides etj.
*/
function formSubGridColsArray() {     //po
    var colTrupiGrida = JSON.parse($('#hfKolonaSubGride').val());
    var rendit = new Array();
    for (var i = 0; i < (colTrupiGrida.length) ; i++) {
        arrayIdKolonaSubGrides[i] = colTrupiGrida[i].KodiTrupi;
        arrayPershkrimiKolonaSubGrides[i] = colTrupiGrida[i].PershkrimiTrupi;
        arrayWidthKolonaSubGrides[i] = colTrupiGrida[i].WidthTrupi;
        if (colTrupiGrida[i].VisibleTrupi == true)
            arrayVisibleKolonaSubGrides[i] = false;
        else
            arrayVisibleKolonaSubGrides[i] = true;
        if (lidhur == true)
            arrayReadOnlyKolonaSubGrides[i] = 'True';
        else
            if (colTrupiGrida[i].ReadonlyTrupi === true)
                arrayReadOnlyKolonaSubGrides[i] = 'True'
            else arrayReadOnlyKolonaSubGrides[i] = 'False';
        rendit[i] = colTrupiGrida[i].IndexTrupi;
    }

}



function PastroClick() {     //po
    var hf = $("#hfShtimModifikim"); ASPxMenu1.GetItemByName('FletaKontabel').SetVisible(false); ASPxMenu1.GetItemByName('Fshi').SetVisible(false); ASPxMenu1.GetItemByName('QendraKosto').SetVisible(false);
    hf.val("shtim"); myMenu.menuSipasTeDrejtaRegjistrim(hf, hfTeDrejta);
    pastro();
    ASPxMenu1.GetItemByName('Draft').SetVisible(true);
    pastroFushatKokes();
    ndryshoKonfigurimin();
    grid_faturat.UnselectRows();
    mbushGrideNgaHiddenFieldiFillestar();
    $('#ASPxSplitter1_hl').empty();
    click = false;
}


function changeName() { //po
    myFaqeCelje.shtoHandlerSession();
    try {
        if (Utils.getUrlVar('id') == undefined)
            window.parent.callWebServiceKtheInfoLart('Shto_FleteDoganore.aspx?lloji=' + Utils.getUrlVar('lloji'), 0);
        else
            window.parent.callWebServiceKtheInfoLart('Shto_FleteDoganore.aspx?lloji=' + Utils.getUrlVar('lloji'), Utils.getUrlVar('id'));
    } catch (e) { }
    myCookies.createCookie('adresa', window.location.href, 1);
}

var first = 0;
function Init() {       //po
    if (typeof (isPostBack) == "undefined") {
        document.getElementById("kokeKonfigurimi").innerHTML = hfState.Get("MenuKokeDokumenti");
        document.getElementById("trupKonfigurimi").innerHTML = hfState.Get("MenuTrupDokumenti");
        document.getElementById("fundKonfigurimi").innerHTML = hfState.Get("MenuFundDokumenti");
        identikuesPerPopupKlientFurnitori = "FleteDoganore";
        identikuesPerPopupArtikulli = "FleteDoganore";
        identifikuesPerPopupDetajime = "FleteDoganore";
        identifikuesPerPopupMagazina = "FleteDoganore";
        identikuesPerPopupLlogari = "FleteDoganore";
        identifikuesPerPopupDokumentat = "FleteDoganore";
        comboNrFature = false;
        changeName();
        //komentuar sepse ndikon ne mosllogaritjen e taksave kur hapet dok per here te pare ne modifikim
        //if ($('#hfShtimModifikim').val() == 'modifikim') {
        //    first = 1;
        //}
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(EndRequestHandler);
        myMesazh.shtoHandler();
        var hf = $("#hfKonffillestar");
        callWebserviceMonedheNdermarrje();
        ndryshoKonfigurimin();
        if (Utils.getUrlVar('lloji') == 'export') {
            gvTVSH.SetVisible(false);
        }
    }
}

//Funksioni qe perdoret per krijimin ne menyre dinamike te tabelave ne te cilat vendosen kontrollet e faqes
function krijoTable(rresht, kolone, emerTabele) {  //po
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}

function callWebserviceKonfigurimi(idKomp, kodKonf) { //po
    try {
        var idGjuha = hfState.Get('_idGjuha');
        var idNdermarrje = hfState.Get('_idNdermarrje');
        var idPerdoruesi = hfState.Get('_idPerdoruesi');
        if ($('#hfShtimModifikim').val() === "shtim")
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Konfigurime", "ktheKonfigAmbjentiMeFormatNumrash"),
                data: JSON.stringify({
                    idKomp: idKomp, kodKonf: kodKonf, kodKontrolli: '', idObjekti: -1, shtim: false, merrFormatKursi: true, merrGjitheKonf: false,
                    idGjuha: idGjuha, idPerdoruesi: idPerdoruesi, idNdermarrje: idNdermarrje
                })
            }).done(SucceededCallbackKonfig);
        else {
            var id = JSON.parse($("#hfIdFaturave").val());
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Konfigurime", "ktheKonfigAmbjentiMeFormatNumrash"),
                data: JSON.stringify({
                    idKomp: idKomp, kodKonf: kodKonf, kodKontrolli: -1, idObjekti: id[0], shtim: false, merrFormatKursi: true, merrGjitheKonf: false, idGjuha: idGjuha, idPerdoruesi: idPerdoruesi, idNdermarrje: idNdermarrje
                })
            }).done(SucceededCallbackKonfig);
        }
    } catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
    }
}

var colGrida;
var formatKursi;
var formatNumriZgjedhur;
//Merr konfigurimin e ambjentit dhe rregullon pozicionin dhe atributet e tjera te kontrolleve te faqes.
function SucceededCallbackKonfig(result) { //po
    //Pasi eshte marr konfigurimi i ambjentit, behen visible te gjithe divet qe permbajne kontollet.
    $("#dvgvFaturat").show();
    $("#dvgvTaksat").show();
    $("#dvFillim").show();
    $("#dvMes1").show();
    $("#dvMes2").show();
    $("#dvFund").show();
    $("#divgride1").show();
    $("#dvbtnFleteDoganore").show();
    if (cmbKonfigurimi.GetText() == 'FLDE') {
        $("#tblMes2")[0].className = 'CustomRenditKontrolleNje';
    }
    var grida = $("#rowed5");
    formatNumriZgjedhur = result.formatNumri;   //[7];
    formatKursi = result.formatKursi;   //[8];
    ruajFormatetNeGride(grida, formatNumriZgjedhur, formatKursi);

    var hf = $("#hfShtimModifikim");
    var hfLidhur = $("#hfLidhur");
    var hfFatura = $("#hfLupaFatura");

    //Marrim emrat e tabelave ku do vendosen kontrollet dhe div-et perkatese
    var arrTabela = ['tblFillim', "tblMes1", "tblMes2", "tblFund"];
    var arrPrind = ["dvFillim", "dvMes1", "dvMes2", "dvFund"];
    var colKontrollet = result.colKontroll;   //[0];
    var colAtrTrupi = result.colAtrTrupi;      //[1];

    //Thirret funksioni SucceededCallbackKonfig i cili ben pozicionimin e kontolleve ne faqe
    var vlerat = myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, undefined, hf, '', arrTabela, undefined, undefined, hfLidhur, arrPrind);

    colGrida = result.colGrida;    //[2];
    var colKushte = result.colKushte;   //[3];
    var colAlterKusht = result.colAlterKusht;   //[4];
    var kodniveli = result.kodniveli; //[5];
    var colSubGrida = result.konfLlojRreshti;    //[6];

    $('#hfKontrolletNrAutom').val(JSON.stringify(colKontrollet));
    $('#hfAtributeNrAutom').val(JSON.stringify(colAtrTrupi));
    if ($("input[id$='hfShtimModifikim']").val() == "shtim") {
        hfNrAuto.Clear();
        hfNrAutoShitje.Clear();
        vendosNrAutomatik(colAtrTrupi, colKontrollet);
    }

    for (var i = 0; i < colKontrollet.length; i++) {
        if (colKontrollet[i].KodKontrolli == "txtKursi") {
            if (colAtrTrupi[i].VlereDefault !== "")
                llojkursi = colAtrTrupi[i].VlereDefault;
            else
                llojkursi = 1;
            break;
        }
    }
    $('#HfGridCol').val(JSON.stringify(colGrida));
    var hidField1 = $("#hfKontabilizimi");
    hidField1.val(0);
    for (j = 0; j < colKushte.length; j++) {
        if (colKushte[j].Kodi == 'GJK') {
            if (colAlterKusht[j].Alternativa == 'Jo') {
                hidField1.val(0);
            }
            else {
                if (colAlterKusht[j].Alternativa == "Direkt")
                    hidField1.val(1);
                else
                    hidField1.val(2);
            }
        }
    }
    formGridColsArray();
    formSubGridColsArray();
    grida.setLastSel2(-1);
    grida.GridUnload("rowed5");
    inicializoGride();
    mbushGrideNgaHiddenFieldiFillestar();
    Utils.konfiguroAccorditionPasKonfigDokumenti(varKonfig.identifikuesPerLocalStorageKey);
}


function vendosNrAutomatik(colAtrTrupi, colKontrollet) {
    myNrAuto.vendosNrAutomatik(colAtrTrupi, colKontrollet, txtDtDok.GetDate());
}

//Ndryshon konfigurimin e ambjentit ne varesi te vleres se zgjdhur ne combobox-in cmbKonfigurimi
//Pershkrimin e konfigurimit te ri te zgjedhur e vendos te label-i lblKonfigurimi qe shfaqet ne
//fillim te faqes me background blu
function ndryshoKonfigurimin() {  //po
    var pershkKonfigAmb = cmbKonfigurimi.GetSelectedItem().GetColumnText("PershkrimKonfigAmbjente");
    if (pershkKonfigAmb != undefined) {
        lblKonfigurimi.SetText(pershkKonfigAmb);
        $('#kokeKonfigurimi').text(hfState.Get("MenuKokeDokumenti") +': ' + pershkKonfigAmb);
    }
    callWebserviceKonfigurimi(522, cmbKonfigurimi.GetSelectedItem().GetColumnText("KodKonfigAmbjente"));
}

function dateGotFocus(s, e) {
    window.setTimeout(function () { s.SetCaretPosition(0); }, 0);
}

/*
Function: menuClick

Percakton veprimin qe kryhet nese klikohet nje nga butonat e menuse.
*/
function menu_click(s, e) { //po
    myJQGrid.menuClick(s, e, "FleteDoganore.aspx?lloji=" + Utils.getUrlVar('lloji'), 'Shto_FleteDoganore.aspx?lloji=' + Utils.getUrlVar('lloji') + '&shtim_modifikim=shtim');
}
function Kthehu(mesazh) {
    myFaqeCelje.kontrolloTeDrejta("FleteDoganore.aspx?lloji=" + Utils.getUrlVar('lloji') + "&ruaj=po");
}
var click = false;

/*
Function: RuajClick

Therret funksionin <merrTeDhena> per te marre te dhenat e grides kur klikohet butoni Ruaj.
*/
function RuajClick(s, e) { //po
    if (click) {
        e.processOnServer = false; Utils.hiqLoadingGif();;
    }
    else {

        click = true;
        if (isValidKoka())
        { merrTeDhena(); }
        else {
            e.processOnServer = false; click = false; Utils.hiqLoadingGif();;
        }
    }
}

var grida

//metodat per te pastruar array kur perdoruesi shtyp butonin pastro
function pastro() {    //po
    counter = 0;
    counter2 = 0;
    countvlerat = 0;
    arrKodi = new Array();
    arrPershkrimi = new Array();
    arrVlera = new Array();
    arrTvsh = new Array();
}

function EndRequestHandler(sender, args) {   //po
    formatoFushaDevi();
    if ($('#hfqkmesazhi').val() == 'shfaqmesazh') {
        myMesazh.ShtoMesazh({ type: "confirm", layout: "center", modal: true, text: hfState.Get("msgDeshironiShperndarjeQendraKosto"), okClick: function () { Utils.hapPopUp(hfState.Get("msgShperndarjeNeQendratEKostos"), $('#hfUrl')); }, cancelClick: function () { Utils.JopopupClick($('#hfUrl')); } });
        $('#hfqkmesazhi').val('jo');
    }
    if ($('#hfqkmesazhi').val() == 'shfaqlupe') {
        Utils.hapPopUp(hfState.Get("msgShperndarjeNeQendratEKostos"), $('#hfUrl'));

    }
    var hf = $("#status1");
    if (hf.val() == "true") {
        if (Utils.getUrlVar('lloji') == 'export') {
            gvTVSH.SetVisible(false);
        }
        myFaqeCelje.kontrolloTeDrejta('Shto_FleteDoganore.aspx?lloji=' + Utils.getUrlVar('lloji') + '&shtim_modifikim=shtim', true);

    } else click = false;
}
function hapPopUp() {
    Utils.hapPopUp(hfState.Get("msgShperndarjeNeQendratEKostos"), $('#hfUrl'));
}
function JopopupClick() { Utils.JopopupClick($('#hfUrl')); }
function closePopup(s, e) {
    popupUniversal.SetContentUrl('');

}

function pastroFushatKokes() {   //po
    //var formatKursi = hfFormatNumri.Get("FormatKursi");
    //var vlDefaultVlefta = hfFormatNumri.Get("FormatZgjedhurVlefta");
    //if (formatKursi.indexOf('.') !== -1)
    //    vlDefaultKursi = '1.' + formatKursi.substring(formatKursi.indexOf('.') + 1);

    cmbMonedha.SetValue('');
    txtKursi.SetText('');
    txtVlFatura.SetText('0');
    txtVlMb.SetText('0');
    txtTransporti.SetText('0');
    txtSiguracion.SetText('0');
    txtTjera.SetText('0');
    txtVlDoganore.SetText('0');
    arrArtikujt = new Array();
    txtTransportTotal.SetText('0.00');
    txtSiguracionTotal.SetText('0.00');
    txtTjeraTotal.SetText('0.00');
    txtDoganTotal.SetText('0.00');
    txtTaksaTotal.SetText('0.00');
    $("#hfKodi").val('');
    $("#hfPershkrimi").val('');
    $("#hfVlera").val('');
    $("#hfTvsh").val('');
    $('#hfNorma').val('');
    $("#hfKodiTVSH").val('');
    $("#hfPershkrimiTVSH").val('');
    $("#hfVleraFaturuarTVSH").val('');
    $("#hfVleftaTvsh").val('');
    $('#hfNormaTVSH').val(''); $('#hfAQT').val('');
    $("input[id$='hfNrFature']").val('');
    $("input[id$='hfArtikulli']").val('');
    $("input[id$='hfLlog']").val('');
    $("input[id$='hfMagazina']").val('');
    $("input[id$='hfNjesia']").val('');
    $("input[id$='hfFurnitori']").val('');
    $("input[id$='hfTransport']").val('');
    $("input[id$='hfSiguracion']").val('');
    $("input[id$='hfTjera']").val('');
    $("input[id$='hfVlDogane']").val('');
    $("input[id$='hfTaksa']").val('');
    txtTotalTaksa.SetText('');
    arrKoka = new Array();
    arrArt = new Array();
    arrLlog = new Array();
    arrMag = new Array();
    arrNjesi = new Array();
    arrKf = new Array();
    arrvlerasub = new Array();
    txtTotalTVSH.SetText('');
    vendosDateDefault();
    lastselsub = ''; lastselsubgrid = '';
    txtNrDok.SetText('');
    gvTaksat.PerformCallback();
    gvTVSH.PerformCallback();

    var hf = $("#status1")[0];
    hf.value = "false";
}


function vendosDateDefault() {
    if ($("input[id$='hfShtimModifikim']").val() == "shtim") {
        try {
            var hfPeriudheObj = window.parent.lexoHfPeriudhe();
            txtDtDok.SetDate(Utils.ktheDateDefault(hfPeriudheObj));
            var dataSot = Utils.zeroOren(new Date());
            txtDtRegj.SetDate(dataSot);
        }
        catch (e) {
        }
    }
}

function merrTeDhenaTaksa() {  //po
    arrKodi = new Array();
    arrPershkrimi = new Array();
    arrVlera = new Array();
    arrTvsh = new Array();
    arrNorma = new Array();
    cou1 = 0;
    indexCounter = 0;
    var hidField1 = $("#hfKodi");
    var hidfield2 = $("#hfPershkrimi");
    var hidField3 = $("#hfVlera");
    var hidField4 = $("#hfTvsh");
    var hidField5 = $('#hfNorma');
    for (i = 0; i < gvTaksat.cpNoRows; i++) {
        editorKodi = Utils.ktheKontroll('txtKodi' + i);
        editorPershkrimi = Utils.ktheKontroll('txtPershkrimi' + i);
        editorVlera = Utils.ktheKontroll('txtVlera' + i);
        editorTvsh = Utils.ktheKontroll('txtTvsh' + i);
        editorNorma = Utils.ktheKontroll('txtNorma' + i);
        arrKodi[cou1] = editorKodi.GetText();
        arrPershkrimi[cou1] = editorPershkrimi.GetText();
        arrVlera[cou1] = editorVlera.GetText();
        arrTvsh[cou1] = editorTvsh.GetChecked();
        arrNorma[cou1] = editorNorma.GetText();
        cou1 = cou1 + 1;
    }
    hidField1.val(JSON.stringify(arrKodi));
    hidfield2.val(JSON.stringify(arrPershkrimi));
    hidField3.val(JSON.stringify(arrVlera));
    hidField4.val(JSON.stringify(arrTvsh));
    hidField5.val(JSON.stringify(arrNorma));
}

function merrTeDhenaTVSH() {  //po
    arrKodi = new Array();
    arrPershkrimi = new Array();
    arrVlera = new Array();
    arrTvsh = new Array();
    arrNorma = new Array();
    arrAQT = new Array();
    cou1 = 0;
    indexCounter = 0;
    var hidField1 = $("#hfKodiTVSH");
    var hidfield2 = $("#hfPershkrimiTVSH");
    var hidField3 = $("#hfVleraFaturuarTVSH");
    var hidField4 = $("#hfVleftaTvsh");
    var hidField5 = $('#hfNormaTVSH');
    var hidField6 = $('#hfAQT');
    for (i = 0; i < gvTVSH.cpNoRows; i++) {
        editorKodi = Utils.ktheKontroll('txtKodiTVSH' + i);
        editorPershkrimi = Utils.ktheKontroll('txtPershkrimiTVSH' + i);
        editorVlera = Utils.ktheKontroll('txtVleraFaturuar' + i);
        editorTvsh = Utils.ktheKontroll('txtVleftaTvsh' + i);
        editorNorma = Utils.ktheKontroll('txtNormaTVSH' + i);
        editorAQT = Utils.ktheKontroll('txtAQT' + i);
        arrKodi[cou1] = editorKodi.GetText();
        arrPershkrimi[cou1] = editorPershkrimi.GetText();
        arrVlera[cou1] = editorVlera.GetText();
        arrTvsh[cou1] = editorTvsh.GetText();
        arrNorma[cou1] = editorNorma.GetText();
        arrAQT[cou1] = editorAQT.GetValue();
        cou1 = cou1 + 1;
    }
    hidField1.val(JSON.stringify(arrKodi));
    hidfield2.val(JSON.stringify(arrPershkrimi));
    hidField3.val(JSON.stringify(arrVlera));
    hidField4.val(JSON.stringify(arrTvsh));
    hidField5.val(JSON.stringify(arrNorma));
    hidField6.val(JSON.stringify(arrAQT));
}

function _getKeyCode(evt) {  //po
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ? evt.keyCode : evt.charCode;
}

function FshiClicked(key) { //po
    merrTeDhenaTaksa();
    gvTaksat.PerformCallback(key);
}

function FshiClickedTVSH(key) {  //po
    merrTeDhenaTVSH();
    gvTVSH.PerformCallback(key);
    llogaritTotalTVSH();
}

var MonedhaNdermarrje;
/*
Function: callWebserviceMonedheNdermarrje

Therret funksionin <ktheMonedheNdermarrje> per te marre monedhen e ndermarrjes.
Shiko funksionin <SucceededCallbackNdermarrje>.
*/
function callWebserviceMonedheNdermarrje() { //po
    try {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheMonedheNdermarrje"),
            data: JSON.stringify({})
        }).done(SucceededCallbackNdermarrje);
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
    }
}

/*
Function: SucceededCallbackNdermarrje

Vendos te variabli <MonedhaNdermarrje> monedhen e ndermarrjes.
*/
function SucceededCallbackNdermarrje(result) {    //po
    MonedhaNdermarrje = result;
}

//kur humb fokusin fusha e kursit ne koken e fletes doganore llogaritet vlera ne MB
//si prodhim i vleres se faturuar me kursin, dhe me pas vendoset vlera ne textboxin perkates
function LostFocusKursiKokaFleteDoganore(editor, key) {  //po
    var hfLidhur = $("input[id$='hfLidhur']");
    var hf = $("input[id$='hfShtimModifikim']");
    var vleraMB;

    if (cmbMonedha.GetValue() != MonedhaNdermarrje) {
        var kursi = txtKursi.GetText();
        if (hf.val() === 'modifikim') {
            if (hfLidhur.val() === 'True')
                txtKursi.SetEnabled(false);
            else
                txtKursi.SetEnabled(true);
        }
        else
            txtKursi.SetEnabled(true);

        if (parseFloat(txtKursi.GetText()) == 0) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgKursiNukMundTeJeteZero"));
            txtKursi.SetText(1.00);
        }
        else if (isNaN(kursi) || kursi <= 0) {
            txtKursi.SetFocus();
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgKursiNumerPozitiv"));
            txtKursi.SetText(1.00);
        }
        else {
            vleraMB = (txtKursi.GetText() * txtVlFatura.GetText());
            txtVlMb.SetText(vleraMB.toString());
            LostFocusPerLlogaritjeTeVlDoganore(txtVlMb);
        }
    }
    else {
        txtKursi.SetText(1);
        txtKursi.SetEnabled(false);
        vleraMB = (txtKursi.GetText() * txtVlFatura.GetText());
        txtVlMb.SetText(vleraMB.toString());
        LostFocusPerLlogaritjeTeVlDoganore(txtVlMb);
    }
}

//vl doganim = vl MB + transport + siguracion + tjera
//Ne lost focus te ketyre kontrolleve duhet te rillogaritet Vl doganimit
function LostFocusPerLlogaritjeTeVlDoganore(editor, key) {//po
    var transp = txtTransporti.GetText();
    var sig = txtSiguracion.GetText();
    var tjera = txtTjera.GetText();
    var mb = txtVlMb.GetText();
    var itransp = isNaN(parseFloat(transp)) ? 0.00 : parseFloat(transp);
    var isig = isNaN(parseFloat(sig)) ? 0.00 : parseFloat(sig);
    var itjera = isNaN(parseFloat(tjera)) ? 0.00 : parseFloat(tjera);
    var imb = isNaN(parseFloat(mb)) ? 0.00 : parseFloat(mb);
    var dogan = itransp + isig + itjera + imb;
    txtVlDoganore.SetText(dogan);
    if (first == 0) {
        for (i = 0; i < gvTaksat.cpNoRows; i++) {
            editorKodi = Utils.ktheKontroll('txtKodi' + i);
            editorNorma = Utils.ktheKontroll('txtNorma' + i);
            editorVlera = Utils.ktheKontroll('txtVlera' + i);
            editorCheck = Utils.ktheKontroll('txtTvsh' + i);
            if (editorKodi.GetText() != "") {
                var taksat = $.parseJSON(hfState.Get("taksatCombo"));
                for (var j = 0; j < taksat.length; j++) {
                    if (taksat[j].KodTaksa != editorKodi.GetText())
                        continue;
                    if (taksat[j].Njesia.toLowerCase() == "perqindje")
                        editorVlera.SetText((isNaN(dogan * editorNorma.GetText() / 100)
                            ? 0
                            : dogan * editorNorma.GetText() / 100));
                    else
                        editorVlera.SetText(editorNorma.GetText());
                }
            }
        }
    }
    llogaritVlera();
}

function llogaritVlera() {  //po
    var transp = txtTransporti.GetText();
    var sig = txtSiguracion.GetText();
    var tjera = txtTjera.GetText();
    var mb = txtVlMb.GetText();
    var itransp = isNaN(parseFloat(transp)) ? 0.00 : parseFloat(transp);
    var isig = isNaN(parseFloat(sig)) ? 0.00 : parseFloat(sig);
    var itjera = isNaN(parseFloat(tjera)) ? 0.00 : parseFloat(tjera);
    var imb = isNaN(parseFloat(mb)) ? 0.00 : parseFloat(mb);
    var dogan = itransp + isig + itjera + imb;

    var shumataksa = 0;
    for (i = 0; i < gvTaksat.cpNoRows; i++) {
        editorVlera = Utils.ktheKontroll('txtVlera' + i);
        editorCheck = Utils.ktheKontroll('txtTvsh' + i);
        if (editorCheck.GetChecked())
            shumataksa = shumataksa + parseFloat(editorVlera.GetText());
    }
    llogaritTotalTaksa();
    var totali = 0;
    for (i = 0; i < gvTVSH.cpNoRows; i++) {
        editorVleraFaturuar = Utils.ktheKontroll('txtVleraFaturuar' + i);

        totali = totali + parseFloat(editorVleraFaturuar.GetText());
    }
    if (first == 0) {

        for (i = 0; i < gvTVSH.cpNoRows; i++) {
            editorKodi = Utils.ktheKontroll('txtKodiTVSH' + i);
            editorNorma = Utils.ktheKontroll('txtNormaTVSH' + i);
            editorVlera = Utils.ktheKontroll('txtVleftaTvsh' + i);
            editorVleraFaturuar = Utils.ktheKontroll('txtVleraFaturuar' + i);
            if (editorKodi.GetText() != "")
                editorVlera.SetText(parseFloat((parseFloat(editorVleraFaturuar.GetText()) * parseFloat(txtKursi.GetText() == "" ? "0" : txtKursi.GetText()) + (itransp + isig + itjera + shumataksa) * (totali == 0 ? 0 : (parseFloat(editorVleraFaturuar.GetText()) / totali))) * editorNorma.GetText() / 100));
        }
    }
    llogaritTotalTVSH();
}

function ndryshoVleraGride() {    //po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if (first == 0) {
        trans = isNaN(parseFloat(txtTransporti.GetText())) ? 0.00 : parseFloat(txtTransporti.GetText());
        vlFaturuar = isNaN(parseFloat(txtVlMb.GetText())) ? 0.00 : parseFloat(txtVlMb.GetText());
        sig = isNaN(parseFloat(txtSiguracion.GetText())) ? 0.00 : parseFloat(txtSiguracion.GetText());
        tj = isNaN(parseFloat(txtTjera.GetText())) ? 0.00 : parseFloat(txtTjera.GetText());
        dog = isNaN(parseFloat(txtVlDoganore.GetText())) ? 0.00 : parseFloat(txtVlDoganore.GetText());
        taksa = isNaN(parseFloat(txtTotalTaksa.GetText())) ? 0.00 : parseFloat(txtTotalTaksa.GetText());
        taksa = taksa + parseFloat(isNaN(parseFloat(txtTotalTVSH.GetText())) ? 0.00 : parseFloat(txtTotalTVSH.GetText()));
        kursi = isNaN(parseFloat(txtKursi.GetText())) ? 1.00 : parseFloat(txtKursi.GetText());
        var ids = grida.jqGrid('getDataIDs');


        var rreshtaTeGrides = grida.getRowData();
        for (j = 0; j < rreshtaTeGrides.length; j++) {

            grida.collapseSubGridRow(ids[j]);

            vlereFB = grida.getTekstQelize('txtVlMb', ids[j]);

            vlereFat = grida.getTekstQelize('txtVlefteFature', ids[j]);

            transgrid = ((trans * vlereFB) / vlFaturuar);
            siggrid = ((sig * vlereFB) / vlFaturuar);
            tjgrid = ((tj * vlereFB) / vlFaturuar);
            doggrid = ((dog * vlereFB) / vlFaturuar);

            taksagrid = ((taksa * vlereFB) / vlFaturuar);
            if ($('#txtTransporti' + ids[j]).val() == undefined) {
                var grida = jQuery("#rowed5");
                if (rreshtaTeGrides[j].txtNrFature != "") {
                    grida.setTekstQelize('txtTransporti', ids[j], transgrid);
                    grida.setTekstQelize('txtSiguracioni', ids[j], siggrid);
                    grida.setTekstQelize('txtTjera', ids[j], tjgrid);
                    grida.setTekstQelize('txtVlDoganore', ids[j], doggrid);
                    grida.setTekstQelize('txtTaksa', ids[j], taksagrid);
                    grida.setTekstQelize('txtKursi', ids[j], kursi);
                    grida.setTekstQelize('txtVlMb', ids[j], parseFloat(vlereFB));
                    grida.setTekstQelize('txtVlefteFature', ids[j], parseFloat(vlereFat));
                }
            }
            else {
                $('#txtTransporti' + idRresht).val(transgrid);
                $('#txtSiguracioni' + idRresht).val(siggrid);
                $('#txtTjera' + idRresht).val(tjgrid);
                $('#txtVlDoganore' + idRresht).val(doggrid);
                $('#txtTaksa' + idRresht).val(taksagrid);
            }
            ruajVlera(j, false);
        }
    }
    else if (first == 1) first = 0;
    else if (first == 2) first = 0;
    VendosTotaleTeShperndarjes(false);
}

function ButtonClickKerko(listUrl) { //po

    identifikuesPerPopupDokumentat = "FleteDoganore.aspx?lloji=" + Utils.getUrlVar('lloji');
    var queryString = {
        veprimi: 'FleteDoganore',
        listUrl: listUrl,
        niveli: $('#hfNiveli').val()
    };
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhDokumentin"), 'LupaDokumenta.aspx?' + Utils.KonvertoObjectQueryString(queryString), widthLupaFatura, heightLupaFatura);
}

function SucceededCallbackMonedheFaturaBlerje(result) {
    var hf7 = $("#hfKodMonedha")[0];
    hf7.value = result;
    //mbushet kombo e monedhes ne koken e fletes doganore sipas monedhes se faturave te zgjedhura
    cmbMonedha.SetText(hf7.value);
}

function callWebserviceAplikohetTVSHNeTakseApoJo(key) { //po
    var hf = $("#hfKodi")[0];
    var kodi = hf.value.split(';')[0].trim();
    var idndermarje = hfState.Get('_idNdermarrje');
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheAplikohetTVSHNeTakseApoJo"),
        data: JSON.stringify({
            kodTakse: kodi, idNderm: idndermarje, key: key
        })
    }).done(SucceededCallbackAplikohetTVSHNeTakse);
}

function SucceededCallbackAplikohetTVSHNeTakse(result) {
    if (result == null) {
        myMesazh.ShtoMesazhGabimi("Ndodhi nje gabim gjate aplikimit te tvsh!");
        return;
    }

    var editorTvsh = Utils.ktheKontroll("txtTvsh" + result.key);
    editorTvsh.SetChecked(result.aplikohet);
    llogaritVlera();
    if (result.key == gvTaksat.cpNoRows - 1) {//shtohet rresht i ri nqs jemi ne rreshtin e fundit
        indexCounter = indexCounter + 1;
        merrTeDhenaTaksa();
        gvTaksat.PerformCallback();
    }
}

function TextChangedKodi(editor, editorEmer, editorVlera, editorTvsh, key) { //po
    //var shifraPasPresjes = parseInt(hfFormatNumri.Get("ShifraPasPresjesVlefta"));
    var a = new Array();
    a = editor.GetText().toString();
    var hf = $("#hfKodi")[0];
    hf.value = editor.GetText();
    editor.SetText(hf.value.split(';')[0]);
    editorEmer.SetText(hf.value.split(';')[1]);
    editorNorma = Utils.ktheKontroll('txtNorma' + key);
    callWebserviceAplikohetTVSHNeTakseApoJo(key);
    var norma = parseFloat(hf.value.split(';')[2]);
    editorNorma.SetText(norma);
    var vleraTaksa;
    if (txtVlDoganore.GetText() != "") {
        var dogana = parseFloat(txtVlDoganore.GetText());
        if (hf.value.split(';')[3].toString().trim().toLowerCase() == "perqindje")
            vleraTaksa = ((norma * dogana) / 100);
        else
            vleraTaksa = norma;
    }
    else {
        vleraTaksa = 0; //hfFormatNumri.Get("FormatZgjedhurVlefta");
    }
    editorVlera.SetText(vleraTaksa.toString());

}

function llogaritTotalTaksa() {     //po
    var shuma = 0;
    for (i = 0; i < gvTaksat.cpNoRows; i++) {
        editorKodi = Utils.ktheKontroll('txtKodi' + i);
        editorVlera = Utils.ktheKontroll('txtVlera' + i);
        if (editorKodi.GetText() != "")
            shuma = shuma + parseFloat(isNaN(parseFloat(editorVlera.GetText())) ? 0 : parseFloat(editorVlera.GetText()));
    }
    txtTotalTaksa.SetText(shuma);
}

function TextChangedPershkrimi(editor, key) {  //po
    var a = new Array();
    var a = new Array();
    a = editor.GetText().toString();
    var hf = $("#hfPershkrimi")[0];
    hf.value = editor.GetText();

    if (key == gvTaksat.cpNoRows - 1) {//shton rresht kur jemi ne rreshtin e fundit
        indexCounter = indexCounter + 1; merrTeDhenaTaksa();
        gvTaksat.PerformCallback();
    }
    llogaritTotalTaksa();
}

function TextChangedVlefta(editor, key) {   //po
    var a = new Array();
    a = editor.GetText().toString().split(',');
    llogaritVlera();
}

function CheckedChangedTaksa(editor, key) {    //po
    llogaritVlera();
}

function TextChangedKodiTVSH(editor, editorEmer, editorVlera, key) {     //po
    var a = new Array();
    a = editor.GetText().toString();
    var hf = $("#hfKodiTVSH")[0];
    hf.value = editor.GetText();
    var selectedSkema = editor.GetSelectedItem();
    editorEmer.SetText(selectedSkema.GetColumnText('Pershkrimi'));
    editorNorma = Utils.ktheKontroll('txtNormaTVSH' + key);
    var norma = parseFloat(hf.value.split(';')[2]);
    editorNorma.SetText(selectedSkema.GetColumnText('NormaPerqindje'));
    var vleraTaksa;

    if (key == gvTVSH.cpNoRows - 1) {//shtohet rresht i ri nqs jemi ne rreshtin e fundit
        indexCounter = indexCounter + 1; merrTeDhenaTVSH();
        gvTVSH.PerformCallback();
    }
    llogaritVlera();
}

function llogaritTotalTVSH() {   //po
    var shuma = 0;
    for (i = 0; i < gvTVSH.cpNoRows; i++) {
        editorKodi = Utils.ktheKontroll('txtKodiTVSH' + i);
        editorVlera = Utils.ktheKontroll('txtVleftaTvsh' + i);
        if (editorKodi.GetText() != "")
            shuma = shuma + parseFloat(isNaN(parseFloat(editorVlera.GetText())) ? 0 : parseFloat(editorVlera.GetText()));
    }
    txtTotalTVSH.SetText(shuma);
    ndryshoVleraGride();
}

function TextChangedVleftaFaturuar(editor, key) {  //po
    llogaritVlera();
}

function TextChangedVleftaTVSH(editor, key) { //po
    llogaritTotalTVSH();
}

function inicializoGride() {  //po
    var classes = '';
    if (lidhur == true)
        classes = 'uigray';
    var arrayPershkrime = [arrayPershkrimiKolonaGrides[0], arrayPershkrimiKolonaGrides[1], arrayPershkrimiKolonaGrides[2], arrayPershkrimiKolonaGrides[3], arrayPershkrimiKolonaGrides[4], arrayPershkrimiKolonaGrides[5],
                arrayPershkrimiKolonaGrides[6], arrayPershkrimiKolonaGrides[7], arrayPershkrimiKolonaGrides[8], arrayPershkrimiKolonaGrides[9], arrayPershkrimiKolonaGrides[10], arrayPershkrimiKolonaGrides[11], arrayPershkrimiKolonaGrides[12]];
    var arrayModel = [
                    { name: arrayIdKolonaGrides[0], index: arrayIdKolonaGrides[0], width: arrayWidthKolonaGrides[0], hidden: arrayVisibleKolonaGrides[0], classes: classes, editable: false },
                    { name: arrayIdKolonaGrides[1], index: arrayIdKolonaGrides[1], width: arrayWidthKolonaGrides[1], hidden: arrayVisibleKolonaGrides[1], classes: classes, editable: false },
                    { name: arrayIdKolonaGrides[2], index: arrayIdKolonaGrides[2], width: arrayWidthKolonaGrides[2], hidden: arrayVisibleKolonaGrides[2], classes: classes, editable: false },
                    { name: arrayIdKolonaGrides[3], index: arrayIdKolonaGrides[3], width: arrayWidthKolonaGrides[3], hidden: arrayVisibleKolonaGrides[3], classes: classes, editable: false },
                    { name: arrayIdKolonaGrides[4], index: arrayIdKolonaGrides[4], width: arrayWidthKolonaGrides[4], hidden: arrayVisibleKolonaGrides[4], classes: classes, editable: false },
                    { name: arrayIdKolonaGrides[5], index: arrayIdKolonaGrides[5], width: arrayWidthKolonaGrides[5], hidden: arrayVisibleKolonaGrides[5], classes: classes, editable: false },
                    { name: arrayIdKolonaGrides[6], index: arrayIdKolonaGrides[6], width: arrayWidthKolonaGrides[6], hidden: arrayVisibleKolonaGrides[6], classes: classes, editable: false },
                    { name: arrayIdKolonaGrides[7], index: arrayIdKolonaGrides[7], width: arrayWidthKolonaGrides[7], hidden: arrayVisibleKolonaGrides[7], classes: classes, editable: (arrayReadOnlyKolonaSubGrides[7] == 'True') ? false : true, sortable: false, edittype: 'custom', editoptions: { custom_element: myelemVleftaTrans, custom_value: myJQGrid.myValueTextBox } },
                    { name: arrayIdKolonaGrides[8], index: arrayIdKolonaGrides[8], width: arrayWidthKolonaGrides[8], hidden: arrayVisibleKolonaGrides[8], classes: classes, editable: (arrayReadOnlyKolonaSubGrides[8] == 'True') ? false : true, sortable: false, edittype: 'custom', editoptions: { custom_element: myelemVleftaSig, custom_value: myJQGrid.myValueTextBox } },
                    { name: arrayIdKolonaGrides[9], index: arrayIdKolonaGrides[9], width: arrayWidthKolonaGrides[9], hidden: arrayVisibleKolonaGrides[9], classes: classes, editable: (arrayReadOnlyKolonaSubGrides[10] == 'True') ? false : true, sortable: false, edittype: 'custom', editoptions: { custom_element: myelemVleftaTjera, custom_value: myJQGrid.myValueTextBox } },
                    { name: arrayIdKolonaGrides[10], index: arrayIdKolonaGrides[10], width: arrayWidthKolonaGrides[10], hidden: arrayVisibleKolonaGrides[10], classes: classes, editable: true, sortable: false, edittype: 'custom', editoptions: { custom_element: myelemVleftaDog, custom_value: myJQGrid.myValueTextBox } },
                    { name: arrayIdKolonaGrides[11], index: arrayIdKolonaGrides[11], width: arrayWidthKolonaGrides[11], hidden: arrayVisibleKolonaGrides[11], classes: classes, editable: (arrayReadOnlyKolonaGrides[11] == 'True') ? false : true, sortable: false, edittype: 'custom', editoptions: { custom_element: myelemVleftaTaksa, custom_value: myJQGrid.myValueTextBox } },
                    { name: arrayIdKolonaGrides[12], index: arrayIdKolonaGrides[12], width: arrayWidthKolonaGrides[12], hidden: arrayVisibleKolonaGrides[12], editable: false, sorttype: "int", hidedlg: true }
    ];
    if ($("input[id$='hfLidhur']").val() == 'True')
        lidhur = true;
    else
        lidhur = false;


    var selektoriGrides = "#rowed5";

    var gridParams = {
        emergride: selektoriGrides,
        arrayPershkrime: arrayPershkrime,
        arrayModel: arrayModel,
        lidhur: lidhur,
        widthi: $('#divgride2')[0].offsetWidth,
        subgrid: true,
        lostFocusKoloneFundit: lostFocusKoloneFundit,
        inicializoSubGride: inicializoSubGride,
        mbushSubGridenRreshtit: mbushSubGridenRreshtit,

        konfigToolbar: {
            konfigGrid: $('#hfTeDrejtaKonfGride').val(),
            ruajKolonatEGrides: ruajKolonatEGrides
        }

    };
    return myJQGrid.initGride(gridParams);
    //myJQGrid.inicializoGride("#rowed5", arrayPershkrime, arrayModel, lidhur, lastsel2,
    //    null, null, null, null, null, null, $('#divgride2')[0].offsetWidth, true, undefined,
    //    undefined, undefined, undefined, undefined, undefined, undefined, $('#hfTeDrejtaKonfGride').val());
}

/*
Vendos formatet e numrave ne gride ne baze te emrit te kolones
*/
function ruajFormatetNeGride(grida, formatNumri, formatKursi) {
    if (typeof formatNumri == 'undefined')
        formatNumri = $.parseJSON(hfState.Get("formatMonedhe"));
    ndryshoKonfigFormatNumri(grida, formatNumri, formatKursi);
    vendosVleraDefaultNeGride(grida);
    vendosKonfigFormatNumri();
    return;
}

function ruajFormatetNeSubGride(grida, formatNumri, id, subgridid) {
    if (typeof formatNumri == 'undefined')
        formatNumri = $.parseJSON(hfState.Get("formatMonedhe"));

    grida.setShifraPasPresjes('txtSasia', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtTjera', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtTransport', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtCmimi', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtVleftaPaTvsh', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtVleraMB', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtSiguracion', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtVlDoganore', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtTaksaSub', formatNumri.ShifraPasPresjesVlefta);

    grida.setVlereDefault('txtSasia', 0);
    grida.setVlereDefault('txtTransport', 0);
    grida.setVlereDefault('txtTjera', 0);
    grida.setVlereDefault('txtCmimi', 0);
    grida.setVlereDefault('txtVleftaPaTvsh', 0);
    grida.setVlereDefault('txtVleraMB', 0);
    grida.setVlereDefault('txtSiguracion', 0);
    grida.setVlereDefault('txtVlDoganore', 0);
    grida.setVlereDefault('txtTaksaSub', 0);
    grida.formatoQelize('txtSasia', id);
    grida.formatoQelize('txtTjera', id);
    grida.formatoQelize('txtTransport', id);
    grida.formatoQelize('txtCmimi', id);
    grida.formatoQelize('txtVleftaPaTvsh', id);
    grida.formatoQelize('txtVleraMB', id);
    grida.formatoQelize('txtSiguracion', id);
    grida.formatoQelize('txtVlDoganore', id);
    grida.formatoQelize('txtTaksaSub', id);
    return;
}

/*
Vendos vlerat default te fushave ne gride ne baze te emrit te kolones
*/
function vendosVleraDefaultNeGride(grida) {
    grida.setVlereDefault('txtCmimi', 1);
    grida.setVlereDefault('txtVlefteFature', 0);
    grida.setVlereDefault('txtVlMb', 0);
    grida.setVlereDefault('txtSiguracioni', 0);
    grida.setVlereDefault('txtTransporti', 0);
    grida.setVlereDefault('txtTjera', 0);
    grida.setVlereDefault('txtVlDoganore', 0);
    grida.setVlereDefault('txtKursi', 1);
    return;
}


function ndryshoKonfigFormatNumri(grida, formatNumri, formatkursi) {
    ///<summary> metode per ndryshimin e formateve te nr ne gride ne baze te emrit te kolones. gjithashtu vendos formatin e nr dhe per fushat e devexpresit ne baze te emrit te kontrollit</summary>
    ///<param name="formatNumri"> objekt i tipit format nr me te dhenat e formatimit</param>
    ///<param name="formatkursi">sherben per te vendosur formatin e kursit ne varesi te monedhes </param>
    if (typeof formatNumri == 'undefined')
        formatNumri = $.parseJSON(hfState.Get("formatMonedhe"));
    else
        hfState.Set("formatMonedhe", JSON.stringify(formatNumri));

    grida.setShifraPasPresjes('txtVlefteFature', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtVlMb', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtTransporti', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtSiguracioni', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtSasia', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtTjera', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtVlDoganore', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtTaksa', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtKursi', formatNumri.ShifraPasPresjesVlefta);

    Utils.setFormatNumri(txtTransporti, formatNumri.ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtSiguracion, formatNumri.ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtTjera, formatNumri.ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtVlDoganore, formatNumri.ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtVlFatura, formatNumri.ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtVlMb, formatNumri.ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtSiguracionTotal, formatNumri.ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtTransportTotal, formatNumri.ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtTjeraTotal, formatNumri.ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtDoganTotal, formatNumri.ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtTaksaTotal, formatNumri.ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtTotalTVSH, formatNumri.ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtTotalTaksa, formatNumri.ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtKursi, formatkursi);
}

/*
Formaton vlerat e fushave sipas formatit perkates ne te gjithe rreshtat e grides
*/
function vendosKonfigFormatNumri() {
    var grida = $('#rowed5');
    var idTe = grida.jqGrid('getDataIDs');
    for (j = 0; j < idTe.length; j++) {
        var idRreshti = idTe[j];
        grida.formatoQelize('txtVlefteFature', idRreshti);
        grida.formatoQelize('txtVlMb', idRreshti);
        grida.formatoQelize('txtTransporti', idRreshti);
        grida.formatoQelize('txtSiguracioni', idRreshti);
        grida.formatoQelize('txtTjera', idRreshti);
        grida.formatoQelize('txtVlDoganore', idRreshti);
        grida.formatoQelize('txtTaksa', idRreshti);
        grida.formatoQelize('txtKursi', idRreshti);
    }
    formatoFushaDevi();
}

function formatoFushaDevi() {
    Utils.formatoTextBox(txtTransporti);
    Utils.formatoTextBox(txtSiguracion);
    Utils.formatoTextBox(txtTjera);
    Utils.formatoTextBox(txtVlDoganore);
    Utils.formatoTextBox(txtVlFatura);
    Utils.formatoTextBox(txtVlMb);
    Utils.formatoTextBox(txtKursi);
    Utils.formatoTextBox(txtSiguracionTotal);
    Utils.formatoTextBox(txtTransportTotal);
    Utils.formatoTextBox(txtTjeraTotal);
    Utils.formatoTextBox(txtDoganTotal);
    Utils.formatoTextBox(txtTaksaTotal);
    Utils.formatoTextBox(txtTotalTVSH);
    Utils.formatoTextBox(txtTotalTaksa);
}

function unformatoFushaDevi() {
    Utils.unFormatoTextBox(txtTransporti);
    Utils.unFormatoTextBox(txtSiguracion);
    Utils.unFormatoTextBox(txtTjera);
    Utils.unFormatoTextBox(txtVlDoganore);
    Utils.unFormatoTextBox(txtVlFatura);
    Utils.unFormatoTextBox(txtVlMb);
    Utils.unFormatoTextBox(txtKursi);
    Utils.unFormatoTextBox(txtSiguracionTotal);
    Utils.unFormatoTextBox(txtTransportTotal);
    Utils.unFormatoTextBox(txtTjeraTotal);
    Utils.unFormatoTextBox(txtDoganTotal);
    Utils.unFormatoTextBox(txtTaksaTotal);
    Utils.unFormatoTextBox(txtTotalTVSH);
    Utils.unFormatoTextBox(txtTotalTaksa);
}

function kursiFmatter(cellvalue, options, rowObject) {
    return myJQGrid.kursiFmatter(cellvalue, options, rowObject);
}

function mbushGrideNgaHiddenFieldi() {   //po
    var nrfature;
    var dtfature;
    var furnitori;
    var kokaFb;

    var mon = "";
    var krs;
    var vMB;
    var trans = 0.00;
    var sig = 0.00;
    var tj = 0.00;
    var dog = 0.00;
    var vlFaturuar = 0.00;

    var kodi;
    var pershk;
    var vlera;
    var tvsh;
    var shuma = 0;
    var fat = new Array();
    var taksa = 0.00;
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    lastsel1 = 1;
    grida.setLastSel2(1);
    idRresht = 1;
    grida.jqGrid('clearGridData');

    if (arrKoka != "")
        for (var i = 0; i < arrKoka.length; i++) {
            if (arrayReadOnlyKolonaGrides[12] == 'True' || lidhur)
                be = myJQGrid.myValueButtonFshi(true, idRresht, "#rowed5");
            else
                be = myJQGrid.myValueButtonFshi(false, idRresht, "#rowed5");
            shuma = shuma + parseFloat(arrKoka[i].TotaliMeZbritjeMeTVSH);
            nrfature = (arrKoka[i].NrDok == undefined) ? "" : arrKoka[i].NrDok;

            dtfature = (arrKoka[i].DtDok == undefined) ? "" : new Date(arrKoka[i].DtDok).format('dd/MM/yyyy');
            arrKoka[i].DtDok = new Date(arrKoka[i].DtDok).format('dd/MM/yyyy');

            furnitori = (arrKf[i].EmertimiKF == undefined) ? "" : arrKf[i].EmertimiKF;

            fat = (arrKoka[i].TotaliMeZbritjeMeTVSH == undefined) ? "" : arrKoka[i].TotaliMeZbritjeMeTVSH;
            vlereFB = isNaN(parseFloat(fat)) ? 0.00 : parseFloat(fat);

            mon = cmbMonedha.GetText();
            krs = isNaN(parseFloat(txtKursi.GetText())) ? kursi : parseFloat(txtKursi.GetText());
            vMB = krs * vlereFB;
            vMB = isNaN(parseFloat(vMB)) ? 0.00 : parseFloat(vMB);
            trans = isNaN(parseFloat(txtTransporti.GetText())) ? 0.00 : parseFloat(txtTransporti.GetText());
            vlFaturuar = isNaN(parseFloat(txtVlFatura.GetText())) ? 0.00 : parseFloat(txtVlFatura.GetText());
            sig = isNaN(parseFloat(txtSiguracion.GetText())) ? 0.00 : parseFloat(txtSiguracion.GetText());
            tj = isNaN(parseFloat(txtTjera.GetText())) ? 0.00 : parseFloat(txtTjera.GetText());
            dog = isNaN(parseFloat(txtVlDoganore.GetText())) ? 0.00 : parseFloat(txtVlDoganore.GetText());

            trans = ((trans * vlereFB) / vlFaturuar);
            sig = ((sig * vlereFB) / vlFaturuar);
            tj = ((tj * vlereFB) / vlFaturuar);
            dog = ((dog * vlereFB) / vlFaturuar);
            taksa = isNaN(parseFloat(txtTotalTaksa.GetText())) ? 0.00 : parseFloat(txtTotalTaksa.GetText());
            taksa = taksa + parseFloat(isNaN(parseFloat(txtTotalTVSH.GetText())) ? 0.00 : parseFloat(txtTotalTVSH.GetText()));
            taksa = ((taksa * vlereFB) / vlFaturuar);

            var datarow = {
                txtNrFature: nrfature, txtDtFature: dtfature, txtFurnitori: furnitori, txtVlefteFature: vlereFB,
                txtMonedha: mon, txtKursi: krs, txtVlMb: vMB, txtTransporti: trans, txtSiguracioni: sig, txtTjera: tj, txtVlDoganore: dog, txtTaksa: taksa, txtFshi: be
            };
            var su;
            if (nrfature != "") //behet kontrolli qe te mos shtohet rresht bosh ne gride
                su = grida.addRowData(parseInt(idRresht), datarow);
            ruajVlera(i, false);
            idRresht = idRresht + 1;
            grida.setLastSel2(idRresht);
        }
    txtVlFatura.SetText(shuma);
}

function mbushGrideNgaHiddenFieldiFillestar() {   //po
    var grida = $('#rowed5');
    var vlefta = 0.0;
    if ($("input[id$='hfNrFature']").val() == "") {
        grida.jqGrid('clearGridData');
        return;
    }
    arrKoka = JSON.parse($("input[id$='hfNrFature']").val());
    arrArt = JSON.parse($("input[id$='hfArtikulli']").val());
    arrLlog = JSON.parse($("input[id$='hfLlog']").val());
    arrMag = JSON.parse($("input[id$='hfMagazina']").val());
    arrNjesi = JSON.parse($("input[id$='hfNjesia']").val());
    arrKf = JSON.parse($("input[id$='hfFurnitori']").val());
    arrtransp = JSON.parse($("input[id$='hfTransport']").val());
    arrsig = JSON.parse($("input[id$='hfSiguracion']").val());
    arrtjera = JSON.parse($("input[id$='hfTjera']").val());
    arrvldog = JSON.parse($("input[id$='hfVlDogane']").val());
    arrtaksa = JSON.parse($("input[id$='hfTaksa']").val());
    arrvlerasub = JSON.parse($("input[id$='hfVleraSub']").val());
    var nrfature;
    var dtfature;
    var furnitori;
    var kokaFb;
    var mon = "";
    var krs;
    var vMB;
    var trans = vlefta;
    var sig = vlefta;
    var tj = vlefta;
    var dog = vlefta;
    var vlFaturuar = vlefta;
    var shuma = vlefta;
    var kodi;
    var pershk;
    var vlera;
    var tvsh;
    var fat = new Array();
    var taksa = vlefta;
    var idRresht = grida.getLastSel2();
    lastsel1 = 1;
    grida.setLastSel2(1);
    idRresht = 1;
    grida.jqGrid('clearGridData');
    if (arrKoka != "")
        for (var i = 0; i < arrKoka.length; i++) {
            if (arrayReadOnlyKolonaGrides[12] == 'True' || lidhur)
                be = myJQGrid.myValueButtonFshi(true, idRresht, "#rowed5");
            else
                be = myJQGrid.myValueButtonFshi(false, idRresht, "#rowed5");
            shuma = shuma + parseFloat(arrKoka[i].TotaliMeZbritjeMeTVSH);
            nrfature = (arrKoka[i].NrDok == undefined) ? "" : arrKoka[i].NrDok;

            dtfature = (arrKoka[i].DtDok == undefined) ? "" : new Date(arrKoka[i].DtDok).format('dd/MM/yyyy');
            arrKoka[i].DtDok = new Date(arrKoka[i].DtDok).format('dd/MM/yyyy');
            furnitori = (arrKf[i].EmertimiKF == undefined) ? "" : arrKf[i].EmertimiKF;
            fat = (arrKoka[i].TotaliMeZbritjeMeTVSH == undefined) ? "" : arrKoka[i].TotaliMeZbritjeMeTVSH;
            vlereFB = isNaN(parseFloat(fat)) ? vlefta : parseFloat(fat);
            mon = cmbMonedha.GetText();
            krs = isNaN(parseFloat(txtKursi.GetText())) ? kursi : parseFloat(txtKursi.GetText());
            vMB = krs * vlereFB;
            vMB = isNaN(parseFloat(vMB)) ? vlefta : parseFloat(vMB);
            trans = ((arrtransp[i] == undefined) ? vlefta : arrtransp[i]);;
            sig = ((arrsig[i] == undefined) ? vlefta : arrsig[i]);
            tj = ((arrtjera[i] == undefined) ? vlefta : arrtjera[i]);
            dog = ((arrvldog[i] == undefined) ? vlefta : arrvldog[i]);
            taksa = ((arrtaksa[i] == undefined) ? vlefta : arrtaksa[i]);
            var datarow = {
                txtNrFature: nrfature, txtDtFature: dtfature, txtFurnitori: furnitori, txtVlefteFature: vlereFB,
                txtMonedha: mon, txtKursi: krs, txtVlMb: vMB, txtTransporti: trans, txtSiguracioni: sig, txtTjera: tj, txtVlDoganore: dog, txtTaksa: taksa
                    , txtFshi: be
            };
            var su;
            if (nrfature != "") //behet kontrolli qe te mos shtohet rresht bosh ne gride
                su = grida.addRowData(parseInt(idRresht), datarow);
            ruajVlera(i, true);
            idRresht = idRresht + 1;
            grida.setLastSel2(idRresht);
        }
    VendosTotaleTeShperndarjes(true);
}

function lostFocusKoloneFundit() {      //po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    idRresht = grida.lostFocusKoloneFundit();
}

function inicializoSubGride(subgrid_table_id) {   //po
    var grida = $('#rowed5');
    var classes = '';
    if (lidhur == true)
        classes = 'uigray';
    jQuery("#" + subgrid_table_id).jqGrid
    ({
        datatype: "local",
        colNames: [arrayPershkrimiKolonaSubGrides[0], arrayPershkrimiKolonaSubGrides[1], arrayPershkrimiKolonaSubGrides[2], arrayPershkrimiKolonaSubGrides[3], arrayPershkrimiKolonaSubGrides[4], arrayPershkrimiKolonaSubGrides[5], arrayPershkrimiKolonaSubGrides[6],
        arrayPershkrimiKolonaSubGrides[7], arrayPershkrimiKolonaSubGrides[8], arrayPershkrimiKolonaSubGrides[9], arrayPershkrimiKolonaSubGrides[10], arrayPershkrimiKolonaSubGrides[11], arrayPershkrimiKolonaSubGrides[12]],

        colModel: [
            { name: arrayIdKolonaSubGrides[0], index: arrayIdKolonaSubGrides[0], width: arrayWidthKolonaSubGrides[0], hidden: arrayVisibleKolonaSubGrides[0], editable: false, sorttype: "int" },
            { name: arrayIdKolonaSubGrides[1], index: arrayIdKolonaSubGrides[1], width: arrayWidthKolonaSubGrides[1], hidden: arrayVisibleKolonaSubGrides[1], editable: false },
            { name: arrayIdKolonaSubGrides[2], index: arrayIdKolonaSubGrides[2], width: arrayWidthKolonaSubGrides[2], hidden: arrayVisibleKolonaSubGrides[2], editable: false },
            { name: arrayIdKolonaSubGrides[3], index: arrayIdKolonaSubGrides[3], width: arrayWidthKolonaSubGrides[3], hidden: arrayVisibleKolonaSubGrides[3], editable: false },
            { name: arrayIdKolonaSubGrides[4], index: arrayIdKolonaSubGrides[4], width: arrayWidthKolonaSubGrides[4], hidden: arrayVisibleKolonaSubGrides[4], classes: classes, editable: false },
            { name: arrayIdKolonaSubGrides[5], index: arrayIdKolonaSubGrides[5], width: arrayWidthKolonaSubGrides[5], hidden: arrayVisibleKolonaSubGrides[5], editable: false },
            { name: arrayIdKolonaSubGrides[6], index: arrayIdKolonaSubGrides[6], width: arrayWidthKolonaSubGrides[1], hidden: arrayVisibleKolonaSubGrides[6], editable: false },
            { name: arrayIdKolonaSubGrides[7], index: arrayIdKolonaSubGrides[7], width: arrayWidthKolonaSubGrides[2], hidden: arrayVisibleKolonaSubGrides[7], editable: false },
            { name: arrayIdKolonaSubGrides[8], index: arrayIdKolonaSubGrides[8], width: arrayWidthKolonaSubGrides[3], hidden: arrayVisibleKolonaSubGrides[8], editable: false },
            { name: arrayIdKolonaSubGrides[9], index: arrayIdKolonaSubGrides[9], width: arrayWidthKolonaSubGrides[4], hidden: arrayVisibleKolonaSubGrides[9], editable: false },
            { name: arrayIdKolonaSubGrides[10], index: arrayIdKolonaSubGrides[10], width: arrayWidthKolonaSubGrides[10], hidden: arrayVisibleKolonaSubGrides[10], editable: false },
            { name: arrayIdKolonaSubGrides[11], index: arrayIdKolonaSubGrides[11], width: arrayWidthKolonaSubGrides[11], hidden: arrayVisibleKolonaSubGrides[11], editable: false },
             { name: arrayIdKolonaSubGrides[12], index: arrayIdKolonaSubGrides[12], width: arrayWidthKolonaSubGrides[12], hidden: arrayVisibleKolonaSubGrides[12], classes: classes, editable: (arrayReadOnlyKolonaSubGrides[12] == 'True') ? false : true, sortable: false, edittype: 'custom', editoptions: { custom_element: myelemVleftaTaksa2, custom_value: myJQGrid.myValueTextBoxSub } }//,
            //{ name: arrayIdKolonaSubGrides[12], index: arrayIdKolonaSubGrides[12], width: arrayWidthKolonaSubGrides[12], hidden: arrayVisibleKolonaSubGrides[12], editable: false }
        ],
        caption: "",
        sortable: false,
        cellsubmit: 'clientArray',
        height: 'auto',
        onCellSelect: function (id, icol) {
            if (lidhur == false) {
                grid = subgrid_table_id;
                if (grid != "" && grid != lastselsubgrid && lastselsubgrid != "") {
                    jQuery("#" + lastselsubgrid).saveRow(lastselsub, false, 'clientArray');
                }
                jQuery('#' + subgrid_table_id).saveRow(lastselsub, false, 'clientArray');
                lastselsub = id;
                lastselsubgrid = grid;

                jQuery('#' + subgrid_table_id).editRow(id);
                $('#' + subgrid_table_id).setlastSel('lastselsub');
                $('#' + subgrid_table_id).setlastGrid('lastselsubgrid');
            }
        }
    });
    $("#" + subgrid_table_id).jqGrid('setGridWidth', grida.jqGrid('getGridParam', 'width') - 30, true);
    renditKolonatSubGrides();
}

function renditKolonatSubGrides() {     //po
    myJQGrid.renditKolonatGrides("#rowed5", arrayRenditjeKolonaSubGrides);
}

function mbushSubGridenRreshtit(id) {   //po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var colArtikujt = arrArt[id - 1];
    var colLlog = arrLlog[id - 1];
    var colMag = arrMag[id - 1];
    var colNjesi = arrNjesi[id - 1];
    var Koka = arrKoka[id - 1];
    var subgrid_table_id = "rowed5_" + id + "_t";
    var art = "";
    var emer = "";
    var mag = "";
    var njes = "";
    var sas = "";
    var cm = "";
    var vl = "";
    var vlMb = "";
    var trans = "";
    var sig = "";
    var tj = "";
    var vlDog = "";
    var tks = "";
    var indeks = 1;

    var taksa = "";

    var hidField7 = $("input[id$='hfVlera']");

    var totaliTaksa = 0;
    var arrVlera = new Array();
    var vlerat = new Array();

    arrVlera = JSON.stringify(hidField7.val());
    for (var i = 0; i < arrVlera.length; i++) {
        totaliTaksa = totaliTaksa + parseFloat(arrVlera[i]);
    }

    for (var i = 0; i < colArtikujt.length; i++) {
        if (colArtikujt[i].KodArtikulli != null) {
            art = colArtikujt[i].KodArtikulli;
            emer = colArtikujt[i].PershkrimArtikulli;
        }
        else {
            art = colLlog[i].NrLlogari;
            emer = colLlog[i].EmerLlogari1;
        }
        mag = colMag[i].Kodi;
        njes = colNjesi[i].KodNjesia;
        sas = (Koka.OColTrupiShitje[i].Sasia);
        cm = (Koka.OColTrupiShitje[i].Cmimi);
        krs = (isNaN(parseFloat(txtKursi.GetText())) ? kursi : parseFloat(txtKursi.GetText()));
        vl = (Koka.OColTrupiShitje[i].VleftaPaTvsh * (1 - Koka.Zbritje / Koka.Totali));
        vlMb = (Koka.OColTrupiShitje[i].VleftaPaTvsh * krs * (1 - Koka.Zbritje / Koka.Totali));
        vl = isNaN(parseFloat(vl)) ? vlefta : parseFloat(vl);
        var rreshtaTeGrides = grida.getRowData(id);

        if ($('#txtTransporti' + id).val() == undefined) {
            if (rreshtaTeGrides.txtNrFature != "") {
                trans = parseFloat(rreshtaTeGrides.txtTransporti.toString());
                sig = parseFloat(rreshtaTeGrides.txtSiguracioni.toString());
                tj = parseFloat(rreshtaTeGrides.txtTjera.toString());
                dog = parseFloat(rreshtaTeGrides.txtVlDoganore.toString());
                tks = parseFloat(rreshtaTeGrides.txtTaksa.toString());
            }
        }
        else {
            trans = parseFloat($('#txtTransporti' + idRresht).val());
            sig = parseFloat($('#txtSiguracioni' + idRresht).val());
            tj = parseFloat($('#txtTjera' + idRresht).val());
            dog = parseFloat($('#txtVlDoganore' + idRresht).val());
            tks = parseFloat($('#txtTaksa' + idRresht).val());
        }
        if (arrvlerasub[id - 1] != undefined && arrvlerasub[id - 1][i] != undefined) {
            trans = arrvlerasub[id - 1][i].Transporti;
            sig = arrvlerasub[id - 1][i].Siguracioni;
            tj = arrvlerasub[id - 1][i].Tjera;
            vlDog = arrvlerasub[id - 1][i].Dogana;
            tks = arrvlerasub[id - 1][i].Taksa;
        }
        else {
            vlFaturuar = isNaN(parseFloat(rreshtaTeGrides.txtVlMb)) ? vlefta : parseFloat(rreshtaTeGrides.txtVlMb);
            trans = ((trans * vlMb) / vlFaturuar);
            sig = ((sig * vlMb) / vlFaturuar);
            tj = ((tj * vlMb) / vlFaturuar);
            vlDog = ((dog * vlMb) / vlFaturuar);
            tks = ((tks * vlMb) / vlFaturuar);
        }
        var row = { txtArtikulli: art, txtEmertimi: emer, txtMagazina: mag, txtNjesia: njes, txtSasia: sas, txtCmimi: cm, txtVleftaPaTvsh: vl, txtVleraMB: vlMb, txtTransport: trans, txtSiguracion: sig, txtTjera: tj, txtVlDoganore: vlDog, txtTaksaSub: tks };

        jQuery("#" + subgrid_table_id).addRowData(indeks, row);
        ruajFormatetNeSubGride(jQuery("#" + subgrid_table_id), formatNumriZgjedhur, indeks, subgrid_table_id);
        indeks = indeks + 1;
    }
}

function fshiClicked(idrreshti) {    //po
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    grida.collapseSubGridRow(idrreshti);
    myJQGrid.fshiClicked(idrreshti, "#rowed5", inicializoGride);
    if (idRow == 0) grida.jqGrid('clearGridData');
    arrArt[idrreshti - 1] = "";
    arrLlog[idrreshti - 1] = "";
    arrKoka[idrreshti - 1] = "";
    arrMag[idrreshti - 1] = "";
    arrNjesi[idrreshti - 1] = "";
    arrKf[idrreshti - 1] = "";
    arrvlerasub[idrreshti - 1] = new Array();
    VendosTotaleTeShperndarjes(false);
}

function ndryshoImazhin(nr, index) {     //po
    myJQGrid.ndryshoImazhin(nr, index);
}

function merrTeDhena() {//merren te dhenat qe ka grida
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    grida.jqGrid('saveRow', idRow, false, 'clientArray');
    var rreshtaTeGrides = grida.getTeDhenaRreshti();
    $('#gridDataObject').val(JSON.stringify(rreshtaTeGrides));
    var hidField1 = $("#hfNrFature");
    var hidField2 = $("#hfTransport");
    var hidField3 = $("#hfSiguracion");
    var hidField4 = $("#hfTjera");
    var hidField5 = $("#hfVlDogane");
    var hidField6 = $("#hfTaksa");
    var hidField7 = $("#hfVleraSub");
    hidField1.val(JSON.stringify(arrKoka));
    hidField2.val(JSON.stringify(arrtransp));
    hidField3.val(JSON.stringify(arrsig));
    hidField4.val(JSON.stringify(arrtjera));
    hidField5.val(JSON.stringify(arrvldog));
    hidField6.val(JSON.stringify(arrtaksa));
    hidField7.val(JSON.stringify(arrvlerasub));
    merrTeDhenaTaksa();
    merrTeDhenaTVSH();
    unformatoFushaDevi();
}

function ButtonClickFleteDoganore() {//po
    var furnitori = "";
    var artikulli = "";
    var mag = "";
    if ($("#hfIdFaturave").val() == "" || $("#hfIdFaturave").val() == "[]") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniNjeFature"));
        return;
    }
    var hidField = JSON.parse($("#hfIdFaturave").val());
    try {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheFaturaPerFleteDoganoreobj"),
            data: JSON.stringify({ pars: hidField })
        }).done(SucceededCallbackFleteDoganore);
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
    }
}

var arrKoka = new Array();
var arrArt = new Array();
var arrLlog = new Array();
var arrMag = new Array();
var arrNjesi = new Array();
var arrKf = new Array();

function SucceededCallbackFleteDoganore(result) {//po
    var grida = $("#rowed5");
    arrKoka = result[0];
    arrArt = result[1];
    arrLlog = result[2];
    arrMag = result[3];
    arrNjesi = result[4];
    arrKf = result[5];
    arrvlerasub = new Array();
    grida.jqGrid('clearGridData');
    if ($('#hfShtimModifikim').val() == 'modifikim') {
        llogaritTotalTaksa();
        llogaritTotalTVSH();
    }


    mbushGrideNgaHiddenFieldi();
    VendosTotaleTeShperndarjes(false);
}

function myelemVleftaTaksa(value, options) {  //po
    var idRow = $('#rowed5').getLastSel2();
    return myJQGrid.myElemVleftaSipasFormatNumri(value, options, arrayReadOnlyKolonaGrides[11], idRow, 'txtTaksa', 'ShperndaNeSubgrid');
}
function myelemVleftaTaksa2(value, options) {  //po

    return myJQGrid.myElemVleftaSipasFormatNumri(value, options, arrayReadOnlyKolonaSubGrides[12], lastselsubgrid + 's' + lastselsub, 'txtTaksaSub', 'changetaksa2');
}

function myelemVleftaDog(value, options) {  //po
    var idRow = $('#rowed5').getLastSel2();
    return myJQGrid.myElemVleftaSipasFormatNumri(value, options, arrayReadOnlyKolonaGrides[10], idRow, 'txtVlDoganore', 'ShperndaNeSubgrid');
}

function myelemVleftaTrans(value, options) {  //po
    var idRow = $('#rowed5').getLastSel2();
    return myJQGrid.myElemVleftaSipasFormatNumri(value, options, arrayReadOnlyKolonaGrides[7], idRow, 'txtTransporti', 'ShperndaNeSubgrid');
}

function myelemVleftaSig(value, options) {    //po
    var idRow = $('#rowed5').getLastSel2();
    return myJQGrid.myElemVleftaSipasFormatNumri(value, options, arrayReadOnlyKolonaGrides[8], idRow, 'txtSiguracioni', 'ShperndaNeSubgrid');
}

function myelemVleftaTjera(value, options) {  //po
    var idRow = $('#rowed5').getLastSel2();
    return myJQGrid.myElemVleftaSipasFormatNumri(value, options, arrayReadOnlyKolonaGrides[9], idRow, 'txtTjera', 'ShperndaNeSubgrid');
}

function myelemVlefteFature(value, options) {
    var idRow = $('#rowed5').getLastSel2();
    return myJQGrid.myElemVleftaSipasFormatNumri(value, options, arrayReadOnlyKolonaGrides[9], idRow, 'txtVlefteFature', 'ShperndaNeSubgrid');
}

function isValidKoka() {  //po
    var formatNumri = $.parseJSON(hfState.Get("formatMonedhe"));
    var formatNrVlefta = formatNumri.ShifraPasPresjesVlefta;
    if (txtNrDok.GetText() == "") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgShenoniNumrinEDokumentit"));
        return false;
    }
    else if (txtDtDok.GetDate() == null) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniNjeDateDokumenti"));
        return false;
    }
    else if (txtDtRegj.GetDate() == null) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniNjeDateRegjstrimi"));
        return false;
    }
    else if (isNaN(parseFloat(txtTransporti.GetText())) ? 0.00 : parseFloat(txtTransporti.GetText()).toFixed(formatNrVlefta) != parseFloat(txtTransportTotal.GetText()).toFixed(formatNrVlefta)) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgVleraTransportiNdryshme"));
        return false;
    }
    else if (isNaN(parseFloat(txtSiguracion.GetText())) ? 0.00 : parseFloat(txtSiguracion.GetText()).toFixed(formatNrVlefta) != parseFloat(txtSiguracionTotal.GetText()).toFixed(formatNrVlefta)) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgSiguracionNdryshme"));
        return false;
    }
    else if (isNaN(parseFloat(txtTjera.GetText())) ? 0.00 : parseFloat(txtTjera.GetText()).toFixed(formatNrVlefta) != parseFloat(txtTjeraTotal.GetText()).toFixed(formatNrVlefta)) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgVleraTjeraNdryshme"));
        return false;
    }
    else if (isNaN(parseFloat(txtVlDoganore.GetText())) ? 0.00 : parseFloat(txtVlDoganore.GetText()).toFixed(formatNrVlefta) != parseFloat(txtDoganTotal.GetText()).toFixed(formatNrVlefta)) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgVleraDoganeNdryshme"));
        return false;
    }
    else if (isNaN(parseFloat(txtTotalTaksa.GetText()) + parseFloat(txtTotalTVSH.GetText())) ? 0.00 : (parseFloat(txtTotalTaksa.GetText()) + parseFloat(txtTotalTVSH.GetText())).toFixed(2) != parseFloat(txtTaksaTotal.GetText()).toFixed(2)) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgVleraTaksaNdryshme"));
        return false;
    }
    else
        return true;
}

var arrtransp = new Array();
var arrsig = new Array();
var arrtjera = new Array();
var arrvldog = new Array();
var arrtaksa = new Array();
var arrvlerasub = new Array();

function ruajVlera(idrreshti, fill) {  //po
    var idRow = $('#rowed5').getLastSel2();
    grida = $('#rowed5');
    if (idrreshti == undefined)
        idrreshti = idRow;
    var koka = arrKoka[idrreshti];
    var rreshtaTeGrides = grida.getRowData();
    var trans = 0.00;
    var sig = 0.00;
    var tj = 0.00;
    var vldog = 0.00;
    var tk = 0.00;
    if (rreshtaTeGrides[idrreshti] != undefined) {
        trans = grida.getTekstQelize('txtTransporti', idrreshti + 1);

        sig = grida.getTekstQelize('txtSiguracioni', idrreshti + 1);
        tj = grida.getTekstQelize('txtTjera', idrreshti + 1);
        vldog = grida.getTekstQelize('txtVlDoganore', idrreshti + 1);
        tk = grida.getTekstQelize('txtTaksa', idrreshti + 1);
        ndryshovlerasubgride(idrreshti, trans, sig, tj, vldog, tk, koka, fill);
    }
    if (trans === " ")
        trans = $('#txtTransporti' + idRow).val();
    if (sig === " ")
        sig = $('#txtSiguracioni' + idRow).val();
    if (tj === " ")
        tj = $('#txtTjera' + idRow).val();
    if (vldog === " ")
        vldog = $('#txtVlDoganore' + idRow).val();
    if (tk === " ")
        tk = $('#txtTaksa' + idRow).val();



    if (koka != "") {
        arrtransp[idrreshti] = trans;
        arrsig[idrreshti] = sig;

        arrtjera[idrreshti] = tj;
        arrvldog[idrreshti] = vldog;
        arrtaksa[idrreshti] = tk;
    }
}
function ndryshovlerasubgride(idrreshti, trans, sig, tj, vldog, tk, koka, fill) {
    var rreshtaTeGridesSub = jQuery("#rowed5_" + (parseInt(idrreshti) + 1) + "_t").getRowData();
    if (arrvlerasub[idrreshti] == undefined)
        arrvlerasub[idrreshti] = new Array();
    if (rreshtaTeGridesSub.length != undefined) {
        for (var m = 0; m < rreshtaTeGridesSub.length; m++) {
            arrvlerasub[idrreshti][m] = new Object();
            arrvlerasub[idrreshti][m].Taksa = jQuery("#rowed5_" + (parseInt(idrreshti) + 1) + "_t").getTekstQelize('txtTaksaSub', (m + 1), "rowed5_" + (parseInt(idrreshti) + 1) + "_t" + 's' + (m + 1));
            arrvlerasub[idrreshti][m].Siguracioni = jQuery("#rowed5_" + (parseInt(idrreshti) + 1) + "_t").getTekstQelize('txtSiguracion', (m + 1), "rowed5_" + (parseInt(idrreshti) + 1) + "_t" + 's' + (m + 1));
            arrvlerasub[idrreshti][m].Transporti = jQuery("#rowed5_" + (parseInt(idrreshti) + 1) + "_t").getTekstQelize('txtTransport', (m + 1), "rowed5_" + (parseInt(idrreshti) + 1) + "_t" + 's' + (m + 1));
            arrvlerasub[idrreshti][m].Tjera = jQuery("#rowed5_" + (parseInt(idrreshti) + 1) + "_t").getTekstQelize('txtTjera', (m + 1), "rowed5_" + (parseInt(idrreshti) + 1) + "_t" + 's' + (m + 1));
            arrvlerasub[idrreshti][m].Dogana = jQuery("#rowed5_" + (parseInt(idrreshti) + 1) + "_t").getTekstQelize('txtVlDoganore', (m + 1), "rowed5_" + (parseInt(idrreshti) + 1) + "_t" + 's' + (m + 1));
        }
    }
    else {
        var vl = "";
        var vlMb = "";
        if (koka != undefined && koka != "")
            for (var m = 0; m < koka.OColTrupiShitje.length; m++) {
                if (arrvlerasub[idrreshti][m] != undefined && fill)
                    return;
                if (arrvlerasub[idrreshti][m] == undefined) {
                    arrvlerasub[idrreshti][m] = new Object();
                }
                krs = (isNaN(parseFloat(txtKursi.GetText())) ? kursi : parseFloat(txtKursi.GetText()));
                vl = (koka.OColTrupiShitje[m].VleftaPaTvsh * (1 - koka.Zbritje / koka.Totali));
                vlMb = (koka.OColTrupiShitje[m].VleftaPaTvsh * krs * (1 - koka.Zbritje / koka.Totali));

                var grida = jQuery("#rowed5");

                vlFaturuar = grida.getTekstQelize('txtVlMb', idrreshti + 1);; //isNaN(parseFloat(rreshtaTeGrides.txtVlMb)) ? vlefta : parseFloat(rreshtaTeGrides.txtVlMb);
                arrvlerasub[idrreshti][m].Transporti = ((trans * vlMb) / vlFaturuar);
                arrvlerasub[idrreshti][m].Siguracioni = ((sig * vlMb) / vlFaturuar);
                arrvlerasub[idrreshti][m].Tjera = ((tj * vlMb) / vlFaturuar);
                arrvlerasub[idrreshti][m].Dogana = ((vldog * vlMb) / vlFaturuar);
                arrvlerasub[idrreshti][m].Taksa = ((tk * vlMb) / vlFaturuar);

            }
    }
}
function VendosTotaleTeShperndarjes(fill) { //po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var totalTransp = 0.00;
    var totalSig = 0.00;
    var totalTj = 0.00;
    var totalDog = 0.00;
    var totalTaks = 0.00;

    var ids = grida.getDataIDs();
    var rreshtaTeGrides = grida.getRowData();
    for (j = 0; j < rreshtaTeGrides.length; j++) {

        if ($('#txtTransporti' + ids[j]).val() == undefined) {
            if (rreshtaTeGrides[j].txtNrFature != "") {
                totalTransp = parseFloat(totalTransp) + parseFloat(grida.getTekstQelize('txtTransporti', ids[j]));
                totalSig = parseFloat(totalSig) + parseFloat(grida.getTekstQelize('txtSiguracioni', ids[j]));
                totalTj = parseFloat(totalTj) + parseFloat(grida.getTekstQelize('txtTjera', ids[j]));
                totalDog = parseFloat(totalDog) + parseFloat(grida.getTekstQelize('txtVlDoganore', ids[j]));
                totalTaks = parseFloat(totalTaks) + parseFloat(grida.getTekstQelize('txtTaksa', ids[j]));
            }
        }
        else {
            totalTransp = parseFloat(totalTransp) + parseFloat($('#txtTransporti' + idRresht).val());
            totalSig = parseFloat(totalSig) + parseFloat($('#txtSiguracioni' + idRresht).val());
            totalTj = parseFloat(totalTj) + parseFloat($('#txtTjera' + idRresht).val());
            totalDog = parseFloat(totalDog) + parseFloat($('#txtVlDoganore' + idRresht).val());
            totalTaks = parseFloat(totalTaks) + parseFloat($('#txtTaksa' + idRresht).val());
        }
        ruajVlera(j, fill);
    }
    txtTransportTotal.SetText(totalTransp);
    txtSiguracionTotal.SetText(totalSig);
    txtTjeraTotal.SetText(totalTj);
    txtDoganTotal.SetText(totalDog);
    txtTaksaTotal.SetText(totalTaks);
}
function changetaksa2() {
    if (isNaN($('#txtTaksaSub' + lastselsubgrid + 's' + lastselsub).val())) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgVleftaTaksaNumer"));
        $('#txtTaksaSub' + lastselsubgrid + 's' + lastselsub).focus();

    }

    //  $('#' + lastselsubgrid).formatoQelize('txtTaksaSub', lastselsub, 0, lastselsubgrid + 's' + lastselsub);
    VendosTotaleTeShperndarjes(false);

}
function ShperndaNeSubgrid() {  //po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var subTransp = 0.00;
    var subSig = 0.00;
    var subTj = 0.00;
    var subDog = 0.00;
    var subTak = 0.00;
    var gridTransp = 0.00;
    var gridSig = 0.00;
    var gridTj = 0.00;
    var gridDog = 0.00;
    var gridTak = 0.00;
    var vlFatura = 0.00;

    if (isNaN($('#txtTransporti' + idRresht).val().replace(',', ''))) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgVleraTransportNumer"));
        $('#txtTransporti' + idRresht).focus();
    }
    if (isNaN($('#txtSiguracioni' + idRresht).val().replace(',', ''))) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgVleftaSiguracionNumer"));
        $('#txtSiguracioni' + idRresht).focus();
    }
    if (isNaN($('#txtTjera' + idRresht).val().replace(',', ''))) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgVleftaTjeraNumer"));
        $('#txtTjera' + idRresht).focus();
    }
    if (isNaN($('#txtTaksa' + idRresht).val().replace(',', ''))) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgVleftaTaksaNumer"));
        $('#txtTaksa' + idRresht).focus();
    }

    $('#txtVlDoganore' + idRresht).val(parseFloat(grida.getTekstQelize('txtVlMb', idRresht)) + (isNaN(parseFloat(grida.getTekstQelize('txtTransporti', idRresht))) ? 0 : parseFloat(grida.getTekstQelize('txtTransporti', idRresht)))
            + (isNaN(parseFloat(grida.getTekstQelize('txtSiguracioni', idRresht))) ? 0 : parseFloat(grida.getTekstQelize('txtSiguracioni', idRresht))) + (isNaN(parseFloat(grida.getTekstQelize('txtTjera', idRresht))) ? 0 : parseFloat(grida.getTekstQelize('txtTjera', idRresht))));

    grida.expandSubGridRow(idRresht);
    var rreshtaTeGrides = jQuery("#rowed5_" + idRresht + "_t").getRowData();
    for (j = 0; j < rreshtaTeGrides.length; j++) {
        subTransp = parseFloat((parseFloat(rreshtaTeGrides[j].txtVleraMB.toString().replace(',', '')) / parseFloat((grida.getRowData(idRresht).txtVlMb).replace(',', ''))) * parseFloat($('#txtTransporti' + idRresht).val().replace(',', '')));
        subSig = parseFloat((parseFloat(rreshtaTeGrides[j].txtVleraMB.toString().replace(',', '')) / parseFloat((grida.getRowData(idRresht).txtVlMb).replace(',', ''))) * parseFloat($('#txtSiguracioni' + idRresht).val().replace(',', '')));
        subTj = parseFloat((parseFloat(rreshtaTeGrides[j].txtVleraMB.toString().replace(',', '')) / parseFloat((grida.getRowData(idRresht).txtVlMb).replace(',', ''))) * parseFloat($('#txtTjera' + idRresht).val().replace(',', '')));
        subDog = parseFloat((parseFloat(rreshtaTeGrides[j].txtVleraMB.toString().replace(',', '')) / parseFloat((grida.getRowData(idRresht).txtVlMb).replace(',', ''))) * parseFloat($('#txtVlDoganore' + idRresht).val().replace(',', '')));
        subTak = parseFloat((parseFloat(rreshtaTeGrides[j].txtVleraMB.toString().replace(',', '')) / parseFloat((grida.getRowData(idRresht).txtVlMb).replace(',', ''))) * parseFloat($('#txtTaksa' + idRresht).val().replace(',', '')));

        rreshtaTeGrides[j].txtTransport = subTransp;
        rreshtaTeGrides[j].txtSiguracion = subSig;
        rreshtaTeGrides[j].txtTjera = subTj;
        rreshtaTeGrides[j].txtVlDoganore = subDog;
        rreshtaTeGrides[j].txtTaksaSub = subTak;
        jQuery("#rowed5_" + idRresht + "_t").setCell(jQuery("#rowed5_" + idRresht + "_t").getDataIDs()[j], 'txtTransport', rreshtaTeGrides[j].txtTransport, 'clientArray', '');
        jQuery("#rowed5_" + idRresht + "_t").setCell(jQuery("#rowed5_" + idRresht + "_t").getDataIDs()[j], 'txtSiguracion', rreshtaTeGrides[j].txtSiguracion, 'clientArray', '');
        jQuery("#rowed5_" + idRresht + "_t").setCell(jQuery("#rowed5_" + idRresht + "_t").getDataIDs()[j], 'txtTjera', rreshtaTeGrides[j].txtTjera, 'clientArray', '');
        jQuery("#rowed5_" + idRresht + "_t").setCell(jQuery("#rowed5_" + idRresht + "_t").getDataIDs()[j], 'txtVlDoganore', rreshtaTeGrides[j].txtVlDoganore, 'clientArray', '');
        jQuery("#rowed5_" + idRresht + "_t").setCell(jQuery("#rowed5_" + idRresht + "_t").getDataIDs()[j], 'txtTaksaSub', rreshtaTeGrides[j].txtTaksaSub, 'clientArray', '');
    }
    VendosTotaleTeShperndarjes(false);
}

function callWebserviceKursi(name) {   //po
    try {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "merrKursiSipasMonedhesDatesDheLlojit"), data: JSON.stringify({ idMonedha: name ? name : 0, date: txtDtDok.GetDate(), lloji: llojkursi })
        }).done(SucceededCallbackKursi);
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
    }
}

function dateChanged() {
    callWebserviceKursi(cmbMonedha.GetValue());
    var hfKontrollet = JSON.parse($('#hfKontrolletNrAutom').val());
    var atributet = JSON.parse($('#hfAtributeNrAutom').val());
    vendosNrAutomatik(atributet, hfKontrollet);
}

function callWebserviceFatureEZhdoganuar(name) {   //po
    try {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheFatureZhdoganuar"),
            data: JSON.stringify({ idt: name })
        }).done(SucceededCallbackZhdoganuar);
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
    }
}

function SucceededCallbackZhdoganuar(rezult) {   //po
    if (rezult !== "")
        myMesazh.ShtoMesazhInformues(rezult);
}

function SucceededCallbackKursi(result) {
    if (result == "0") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimKursiMonedha"));
        txtKursi.SetText("1");
        kursifundit = 1;
    }
    else {
        txtKursi.SetText(result);
        kursifundit = result;
    }
    LostFocusKursiKokaFleteDoganore(txtKursi, 1);
}

/*
Function: SelectionChangedGridFaturat

Kur ndryshojme selection-in e grides se faturave.
*/
function SelectionChangedGridFaturat(visibleIndex) {  //po
    if ($('#hfShtimModifikim').val() != 'modifikim') {
        llogaritTotalTaksa();
        llogaritTotalTVSH();
        grid_faturat.GetSelectedFieldValues('IdDokumenti;Vlefta;IdMonedha', OnGridFaturatSelectionComplete);
    }
    else {
        //ne rast se nuk do behet plotesim i fushave te tjera,
        //bej vetem vendosjen ne hf te idve te faturave
        grid_faturat.GetSelectedFieldValues('IdDokumenti;IdMonedha', OnGridFaturatSelectionCompleteChangeIdFaturat);
    }
}

function OnGridFaturatSelectionComplete(values) { //po
    var shuma = 0.00;
    var id = new Array();
    var zhdoganim = true;
    if (values.length !== 0) {
        if (values.length == 1) {
            if (cmbMonedha.GetValue() != values[0][2]) {
                cmbMonedha.SetValue(values[0][2]);
                callWebserviceKursi(values[0][2]);
            }
        }
        for (var i = 0; i < values.length; i++) {
            if (values[i][2] != cmbMonedha.GetValue()) {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgFaturaMonedhaNdryshme"));
                grid_faturat.UnselectRowsByKey(values[i][0]);
                zhdoganim = false;
                continue;
            }
            else
                shuma = shuma + parseFloat(values[i][1]);
            id[i] = values[i][0];
        }
        if (id[0] !== undefined && id[0] !== "" && id[0] !== null) {
            var idGjuha = hfState.Get('_idGjuha');
            var idndermarrje = hfState.Get('_idNdermarrje');
            $.ajax({
                url: Utils.getServerApiUrl("Konfigurime", "ktheKonfigurimFormatNumri"),
                data: JSON.stringify({ idKonfigurim: cmbKonfigurimi.GetValue(), idNdermarrje: idndermarrje, kodKontrolli: "", idObjekt: id[0], shtim: true, idKomponente: 522, merrFormatKursi: true, idGjuha: idGjuha })
            }).done(SucceededCallbackFormatNumri);
        }
    }
    $('#hfIdFaturave').val(JSON.stringify(id));
    if (first == 0 && zhdoganim)
        callWebserviceFatureEZhdoganuar(id);
    merrTeDhenaTaksa();
    merrTeDhenaTVSH();
    txtVlFatura.SetText(shuma);
    if (values.length == 0) {
        try {
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "ktheFaturaPerFleteDoganoreobj"),
                data: JSON.stringify({ pars: id })
            }).done(SucceededCallbackFleteDoganore);
        }
        catch (e) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
        }
    }
}

function OnGridFaturatSelectionCompleteChangeIdFaturat(values) { //po
    var id = new Array();
    var nr = 0;
    if (values.length != 0) {
        for (var i = 0; i < values.length; i++) {
            if (values[i][1] != cmbMonedha.GetValue()) {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgFaturaMonedhaNdryshme"));
                grid_faturat.UnselectRowsByKey(values[i][0]);
            } else {
                id[nr] = values[i][0]; nr++;
            }
        }
    }
    $('#hfIdFaturave').val(JSON.stringify(id));
}

function SucceededCallbackFormatNumri(result) {
    var grida = $("#rowed5");
    formatNumriZgjedhur = result[0];
    formatKursi = result[1];
    ndryshoKonfigFormatNumri(grida, formatNumriZgjedhur, formatKursi);
    vendosKonfigFormatNumri();
    var id = $("#hfIdFaturave").val();
    if (Utils.getUrlVar('lloji') == 'import' && first == 0) {
        gvTVSH.PerformCallback('TVSH|' + id);
        merrTeDhenaTVSH();
    }
    merrTeDhenaTaksa()
    gvTaksat.PerformCallback();
    LostFocusKursiKokaFleteDoganore(txtKursi, 1);
}

function ButtonClickKursi(s, e) {
    if (cmbMonedha.GetText() !== '') {
        popupUniversal.SetHeaderText(hfState.Get("msgZgjidhKurs"));
        popupUniversal.SetContentUrl('LupaKursiShpejte.aspx?idMonedha=' + cmbMonedha.GetValue() + '&llojKursi=' + llojkursi);
        popupUniversal.SetSize(widthLupaKursi, heightLupaKursi);
        popupUniversal.Show();
    }
}
