;
var pageState = {
    parametraMenuClick: null,
    gridaSelector: "#rowed5",
    identifikuesRreshti: null,
    LejoMagazinaNdryshe: false,
    lajmerimmagazineDes : 0
};
var kodkodbar = 1;
var lidhur = false;
var arrayMeMagazina = new Array();
var colMagazina;
var colMagazina2;
var magazina1;
var magazina2;
var njesiaZgjedhur;
var totaletSasiveDetajimeve = new Array();
var arrayIdKolonaGrides = new Array();
var arrayPershkrimiKolonaGrides = new Array();
var arrayVisibleKolonaGrides = new Array();
var arrayWidthKolonaGrides = new Array();
var arrayReadOnlyKolonaGrides = new Array();
var arrayRenditjeKolonaGrides = new Array();
var arrMeNjesi = new Array();
var widthLupaArtikull = 850;
var heightLupaArtikull = 600;
var widthLupaKF = 800;
var heightLupaKF = 600;
var widthLupaMakro = 600;
var heightLupaMakro = 600;
var widthLupaMagazina = 600;
var heightLupaMagazina = 600;
var widthLupaPeriudha = 600;
var heightLupaPeriudha = 600;
var widthLupaKerko = 950;
var heightLupaKerko = 600;
var widthLupaDetajim = 600;
var heightLupaDetajim = 600;
var editorData;
var STR_sasiaNumer = 'Sasia duhet të jetë numer!';
var widthLupaAutomjet = 800;
var heightLupaAutomjet = 600;
var widthLupaTransportues = 800;
var heightLupaTransportues = 600;
var identifikuesPerPopupAutomjet = "Shto_RegjistrimMagazine";
var click = false;
var detajim1 = '';
var detajim2 = '';
var mbushMagSipasKushtit = false;
var memoryArt;
var previousMag = undefined;
var kaSeriale = false;
var kaPyetjeHapurMagazina = false;

var varKonfig = {
    identifikuesPerLocalStorageKey: 'Magazina'
};

$(document).ready(function () {
    memoryArt = new memory("IdArtikulli");

    Utils.konfiguroAccorditionNeDocReady(varKonfig.identifikuesPerLocalStorageKey);
    $(window).on('load', function () {

        Init();
        Utils.resizeSplitter();

    });
});
$(document).keydown(function (e) {
    switch (e.which) {
        case 13:
            e.preventDefault();
            break;
        case 120:
            e.preventDefault();
            if ($(e.target).prop('tagName') === 'INPUT' && $(e.target).prop('type') === 'text')
                $(e.target).trigger('blur');
            var eventData = {
                item: {
                    name: 'Ruaj',
                    index: 0
                },
                processOnServer: true
            };
            var sender = 'tastiera';
            menu_click(sender, eventData);
            break;
        case 116: //F5
            if (window.parent !== undefined)
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
$(window).on('resize', function () {
    try {
        if (Utils.isGridResized())//if (!Utils.resizeSplitter(145))
            return;
    }
    catch (ee) {
        console.log(ee);
    }
    var grida = $('#rowed5');
    if ($('#divgride2').width() != null) {
        myJQGrid.fixGridWidth(grida, $('#divgride2'));
    }
}).trigger('resize');

function Init() {
    if (typeof (isPostBack) == "undefined") {
        editorData = dteDtDok;
        var hf = document.getElementById("hfKonffillestar");
        cmbKonfigurimi.SetText(hf.value);
        //ndryshoKonfigurimin();
        document.getElementById("kokeKonfigurimi").innerHTML = hfState.Get("MenuKokeDokumenti");
        document.getElementById("trupKonfigurimi").innerHTML = hfState.Get("MenuTrupDokumenti");
        document.getElementById("fundKonfigurimi").innerHTML = hfState.Get("MenuFundDokumenti");
        pageState.idNdermarrje = hfState.Get("idNdermarrje");
        pageState.idPerdoruesi = hfState.Get("idPerdoruesi");
        pageState.idViti = hfState.Get("idViti");
        pageState.idGjuha = hfState.Get("idGjuha");
        pageState.identifikuesPopUp = "RegjistrimMagazine";
        identifikuesPerPopupDokumentat = "Shto_RegjistrimMagazine.aspx";        
        identikuesPerPopupKlientFurnitori = "RegjistrimMagazine";
        identikuesPerPopupArtikulli = "RegjistrimMagazine";
        identifikuesPerPopupDetajime = "RegjistrimMagazine";
        identifikuesPerPopupMakro = "RegjistrimMagazine";
        identifikuesPerPopupMagazina = "RegjistrimMagazine";
        pageState.vendosMagazineNgaLupa = function (params) {
			vendosMagazinenNgaLupa("txtMagazina",params);
        };
        pageState.vendosMagazine2NgaLupa = function (params) {
			vendosMagazinenNgaLupa("txtMagazina2",params);
        };

        if (hfState.Contains("KaSeriale")) {
            kaSeriale = hfState.Get("KaSeriale");
            hfState.Remove("KaSeriale");
        }
        pageState.kushte = {};
        pageState.kushte.kushtSerialSinkron = true;
        pageState.kushte.VCVS = false;
        pageState.kushte.F;
        // vendosja e tesktit te menuve koke, trup dhe fund
        document.getElementById("kokeKonfigurimi").innerHTML = hfState.Get("MenuKokeDokumenti");
        document.getElementById("trupKonfigurimi").innerHTML = hfState.Get("MenuTrupDokumenti");
        document.getElementById("fundKonfigurimi").innerHTML = hfState.Get("MenuFundDokumenti");
        changeName();
        myMesazh.shtoHandler();
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(EndRequestHandler);
        ngarkoKategoriSerialesh();
    }
}

/*
Function: inicializoGride

Inicializon griden e trupit. Konfiguron kolonat e grides dhe percakton veprimin qe kryhet onCellSelect.
*/
function inicializoGride(isLidhur) {
    if (arrayPershkrimiKolonaGrides.length == 0)
        return;
    var classes = '';
    var mosShtoRresht = !lejomod;
    if (hfState.Get("GjeneruarNgaMema"))
        isLidhur = true;
    if (isLidhur && $("input[id$='hfShtimModifikim']").val() == 'modifikim') {
        if (hfState.Get("IdStatusDok") == 0 && !hfState.Get("LMDET"))
            classes = 'uigray';
        else {
            isLidhur = false;
            mosShtoRresht = true;
        }
    }
    var arrayPershkrime = [arrayPershkrimiKolonaGrides[0], arrayPershkrimiKolonaGrides[1], arrayPershkrimiKolonaGrides[2], arrayPershkrimiKolonaGrides[3],
        arrayPershkrimiKolonaGrides[4], arrayPershkrimiKolonaGrides[5], arrayPershkrimiKolonaGrides[6], arrayPershkrimiKolonaGrides[7], arrayPershkrimiKolonaGrides[8],
        arrayPershkrimiKolonaGrides[9], arrayPershkrimiKolonaGrides[10], arrayPershkrimiKolonaGrides[11], arrayPershkrimiKolonaGrides[12], arrayPershkrimiKolonaGrides[13],
        arrayPershkrimiKolonaGrides[14], arrayPershkrimiKolonaGrides[15], arrayPershkrimiKolonaGrides[16], arrayPershkrimiKolonaGrides[17], arrayPershkrimiKolonaGrides[18],
        arrayPershkrimiKolonaGrides[19], arrayPershkrimiKolonaGrides[20], arrayPershkrimiKolonaGrides[21], arrayPershkrimiKolonaGrides[22], arrayPershkrimiKolonaGrides[23],
        arrayPershkrimiKolonaGrides[24], arrayPershkrimiKolonaGrides[25], arrayPershkrimiKolonaGrides[26]];
    var arrayModel = [
        { name: arrayIdKolonaGrides[0], index: arrayIdKolonaGrides[0], width: arrayWidthKolonaGrides[0], hidden: arrayVisibleKolonaGrides[0], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemNrRendor, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[1], index: arrayIdKolonaGrides[1], width: arrayWidthKolonaGrides[1], hidden: arrayVisibleKolonaGrides[1], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myElemCombo, custom_value: myJQGrid.myValueCombo } },
        { name: arrayIdKolonaGrides[2], index: arrayIdKolonaGrides[2], width: arrayWidthKolonaGrides[2], hidden: arrayVisibleKolonaGrides[2], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myElemKodi, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[3], index: arrayIdKolonaGrides[3], width: arrayWidthKolonaGrides[3], hidden: arrayVisibleKolonaGrides[3], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myElemKodbari, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[4], index: arrayIdKolonaGrides[4], width: arrayWidthKolonaGrides[4], hidden: arrayVisibleKolonaGrides[4], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myElemEmertimi, custom_value: myJQGrid.myValueTextBox } },

        { name: arrayIdKolonaGrides[5], index: arrayIdKolonaGrides[5], width: arrayWidthKolonaGrides[5], hidden: arrayVisibleKolonaGrides[5], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemDetajimet, custom_value: myJQGrid.myValueTextBox } },

        { name: arrayIdKolonaGrides[6], index: arrayIdKolonaGrides[6], width: arrayWidthKolonaGrides[6], hidden: arrayVisibleKolonaGrides[6], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemDetajimet2, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[7], index: arrayIdKolonaGrides[7], width: arrayWidthKolonaGrides[7], hidden: arrayVisibleKolonaGrides[7], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemComboMagazina, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[8], index: arrayIdKolonaGrides[8], width: arrayWidthKolonaGrides[8], hidden: arrayVisibleKolonaGrides[8], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemComboNjesiaSup, custom_value: myJQGrid.myValueCombo } },
        { name: arrayIdKolonaGrides[9], index: arrayIdKolonaGrides[9], width: arrayWidthKolonaGrides[9], hidden: hfState.Get("LLN") == 'Ndryshim sasie' ? false : arrayVisibleKolonaGrides[9], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemSasia, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[10], index: arrayIdKolonaGrides[10], width: arrayWidthKolonaGrides[10], hidden: hfState.Get("LLN") == 'Ndryshim sasie' ? false : arrayVisibleKolonaGrides[10], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemCmimi, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[11], index: arrayIdKolonaGrides[11], width: arrayWidthKolonaGrides[11], hidden: arrayVisibleKolonaGrides[11], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemVlefta, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[12], index: arrayIdKolonaGrides[12], width: arrayWidthKolonaGrides[12], hidden: arrayVisibleKolonaGrides[12], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemComboMagazina2, custom_value: myvalueComboMagazina2 } },
        { name: arrayIdKolonaGrides[13], index: arrayIdKolonaGrides[13], width: arrayWidthKolonaGrides[13], hidden: arrayVisibleKolonaGrides[13], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myelemIdKodi, custom_value: myJQGrid.myValueTextBox }, hidedlg: true },
        { name: arrayIdKolonaGrides[14], index: arrayIdKolonaGrides[14], width: arrayWidthKolonaGrides[14], hidden: arrayVisibleKolonaGrides[14], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myelemIdTrupiRezervimi, custom_value: myJQGrid.myValueTextBox }, hidedlg: true },
        { name: arrayIdKolonaGrides[15], index: arrayIdKolonaGrides[15], width: arrayWidthKolonaGrides[15], hidden: arrayVisibleKolonaGrides[15], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myelemIdTrupiKonvertimi, custom_value: myJQGrid.myValueTextBox }, hidedlg: true },
        { name: arrayIdKolonaGrides[16], index: arrayIdKolonaGrides[16], width: arrayWidthKolonaGrides[16], hidden: arrayVisibleKolonaGrides[16], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemSeriali, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[17], index: arrayIdKolonaGrides[17], width: arrayWidthKolonaGrides[17], hidden: arrayVisibleKolonaGrides[17], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myelemIdTrupiKonvertimiUSH, custom_value: myJQGrid.myValueTextBox }, hidedlg: true },
        { name: arrayIdKolonaGrides[18], index: arrayIdKolonaGrides[18], width: arrayWidthKolonaGrides[18], hidden: arrayVisibleKolonaGrides[18], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myelemIdTrupiKonvertimiUD, custom_value: myJQGrid.myValueTextBox }, hidedlg: true },
        { name: arrayIdKolonaGrides[19], index: arrayIdKolonaGrides[19], width: arrayWidthKolonaGrides[19], hidden: arrayVisibleKolonaGrides[19], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myelemIdTrupi, custom_value: myJQGrid.myValueTextBox }, hidedlg: true },
        { name: arrayIdKolonaGrides[20], index: arrayIdKolonaGrides[20], width: arrayWidthKolonaGrides[20], hidden: arrayVisibleKolonaGrides[20], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemMagPershkrim, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[21], index: arrayIdKolonaGrides[21], width: arrayWidthKolonaGrides[21], hidden: arrayVisibleKolonaGrides[21], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myelemIdKthimi, custom_value: myJQGrid.myValueTextBox }, hidedlg: true },
        { name: arrayIdKolonaGrides[22], index: arrayIdKolonaGrides[22], width: arrayWidthKolonaGrides[22], hidden: arrayVisibleKolonaGrides[22], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myelemIdTrupiShitjeGjenerimi, custom_value: myJQGrid.myValueTextBox }, hidedlg: true },
        { name: arrayIdKolonaGrides[23], index: arrayIdKolonaGrides[23], width: arrayWidthKolonaGrides[23], hidden: arrayVisibleKolonaGrides[23], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemShenime, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[24], index: arrayIdKolonaGrides[24], width: arrayWidthKolonaGrides[24], hidden: arrayVisibleKolonaGrides[24], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myElemKodiSet, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[25], index: arrayIdKolonaGrides[25], width: arrayWidthKolonaGrides[25], hidden: arrayVisibleKolonaGrides[25], classes: classes, sorttype: "int", editable: true, edittype: 'custom', editoptions: { custom_element: myElemButtonSerialeUnike, custom_value: myElemButtonSerialeUnike }, hidedlg: true },
        { name: arrayIdKolonaGrides[26], index: arrayIdKolonaGrides[26], width: arrayWidthKolonaGrides[26], hidden: arrayVisibleKolonaGrides[26], classes: classes, sorttype: "int", editable: true, edittype: 'custom', editoptions: { custom_element: myElemButtonFshi, custom_value: myValueButtonFshi }, hidedlg: true }
    ];

    var selektoriGrides = "#rowed5";

    var gridParams = {
        emergride: selektoriGrides,
        arrayPershkrime: arrayPershkrime,
        arrayModel: arrayModel,
        lidhur: isLidhur,
        emerEditorKodi: "txtKodi",
        emerEditorLloji: "txtKategoria",
        emerEditorDetajimi1: 'txtDetajimi',
        emerEditorDetajim2: 'txtDetajimi2t',
        emerEditorCmimi: "txtCmimi",
        cmimzero: cmimzero,
        emerMag1: 'txtMagazina',
        emerMag2: 'txtMagazina2',
        widthi: $('#divgride2').width() - 5,
        fokus: fokusi,
        mosshtorresht: mosShtoRresht,
        emerEditorMagazina: "txtMagazina",
        emerEditorMagazina2: "txtMagazina2",
        emerEditorPershkrimMagazina: "txtPershkrimmag",
        butonMagazina: btneMagazina,
        butonMagazina2: btneMagazina2,
        resetRreshtKorent: resetRreshtKorent,
        magazinaPare: colMagazina[0],
        afterSaveFunc: shtoArtikullSet,
        autocompleteList: [
         { emerEditor: "txtKodi", selectFunc: selectFunc, changeFunc: changeFunc, shtoDataKod: false },
         { emerEditor: 'txtDetajimi', selectFunc: selectFunc2, changeFunc: changeFunc2, shtoDataKod: false },
         { emerEditor: 'txtDetajimi2t', selectFunc: selectFunc3, changeFunc: changeFunc3, shtoDataKod: false },
         { emerEditor: "txtCmimi", selectFunc: selectFunc6, changeFunc: changeFunc6, shtoDataKod: false },
         { emerEditor: "txtIdArtikullSet", selectFunc: selectFunc4, changeFunc: changeFunc4, shtoDataKod: false },
         { emerEditor: "txtMagazina", selectFunc: selectFuncMag, changeFunc: changeFuncMag, shtoDataKod: false },
         { emerEditor: "txtMagazina2", selectFunc: selectFuncMag, changeFunc: changeFuncMag, shtoDataKod: false }
        ],
        konfigToolbar: {
            konfigGrid: $('#hfTeDrejtaKonfGride').val(),
            shtoArtikull: $('#hfTeDrejtaArtRi').val(),
            ruajKolonatEGrides: ruajKolonatEGrides,
            hapPopUpRi: hapPopUpRi,
            hapPopUpModifikoArt: hapPopUpModifikoArt,
            exportExcel: true,        //Ben enable exportin e F
            EmerExporti: "Hyrje/Dalje", //Emri i filet .xls qe gjenerohet
            hapNgarkimSerialesh: hapNgarkimSerialesh,
            importoSeriale: $("#hfTeDrejtaImportoSeriale").val()
        }
    };
    return myJQGrid.initGride(gridParams);
}

/*
Vendos formatet e numrave ne gride ne baze te emrit te kolones
*/
function ruajFormatetNeGride(grida, formatNumri) {
    if (typeof formatNumri == 'undefined')
        formatNumri = $.parseJSON(hfState.Get("formatMonedhe"));
    ndryshoKonfigFormatNumri(grida, formatNumri);
    vendosVleraDefaultNeGride(grida);
    vendosKonfigFormatNumri();
    return;
}

/*
Vendos vlerat default te fushave ne gride ne baze te emrit te kolones
*/
function vendosVleraDefaultNeGride(grida) {
    grida.setVlereDefault('txtCmimi', 1);
    grida.setVlereDefault('txtVlefta', 1);
    grida.setVlereDefault('txtSasia', 1);
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
    grida.setShifraPasPresjes('txtCmimi', formatNumri.ShifraPasPresjesCmimi);
    grida.setShifraPasPresjes('txtVlefta', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtSasia', formatNumri.ShifraPasPresjesSasia);
    Utils.setFormatNumri(txtVlefta, formatNumri.ShifraPasPresjesVlefta);
}

/*
Formaton vlerat e fushave sipas formatit perkates.
*/
function vendosKonfigFormatNumri() {
    var grida = $('#rowed5');
    var idTe = grida.jqGrid('getDataIDs');
    for (j = 0; j < idTe.length; j++) {
        var idRreshti = idTe[j];
        grida.formatoQelize('txtSasia', idRreshti);
        grida.formatoQelize('txtCmimi', idRreshti);
        grida.formatoQelize('txtVlefta', idRreshti);
    }
    formatoFushaDevi();
}

function formatoFushaDevi() {
    Utils.formatoTextBox(txtVlefta);
}

function unformatoFushaDevi() {
    Utils.unFormatoTextBox(txtVlefta);
}

function dateGotFocus(s, e) {
    window.setTimeout(function () { s.SetCaretPosition(0); }, 0);
}

/*
Function: myElemNrRendor

Nderton nje textbox per te vendosur nr rendor te rreshtit.
*/
function myElemNrRendor(value, options) {//po
    disabled = koloneDisabled() || arrayReadOnlyKolonaGrides[0];
    var grida = $("#rowed5");
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemNrRendor(value, options, idRresht, 'txtNrRendor', grida);
}

function myelemMagPershkrim(value, options) {
    var disabled = koloneDisabled() || (arrayReadOnlyKolonaGrides[20] == 'True') || !lejomod;
    var idRresht = $("#rowed5").getLastSel2();
    return myJQGrid.myElemIdKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[20]);
}

function merrMagazinatKoka() {
    var magArtKoka = "";
    var magDestKoka = "";
    if (btneMagazina.GetSelectedItem() != null)
        magArtKoka = btneMagazina.GetSelectedItem().GetColumnText('Kodi');
    if (btneMagazina2.GetSelectedItem() != null)
        magDestKoka = btneMagazina2.GetSelectedItem().GetColumnText('Kodi');
    return { magArtKoka: magArtKoka, magDestKoka: magDestKoka };
}

function callWebserviceMerrArtikullMeID(params){
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraArtMeID"),
        data: JSON.stringify({
            idja: params.idja,
            rreshti: params.rreshti,
            data: params.data,
            iddetajim: params.iddetajim,
            magazina: params.magazina,
            magazinatKoka: params.magazinatKoka,
            merrMagMeAutorizim: params.merrMagMeAutorizim,
            meDetajim: params.meDetajim,
            sasiaNeGride: params.sasiaNeGride,
            magazinaDest: params.magazinaDest,
            idNdermarrje: pageState.idNdermarrje,
            idPerdoruesi: pageState.idPerdoruesi,
            njesiDef: njesiDef,
            sasiaNeRresht: params.sasiaNeRresht,
            merrCmim: Utils.getUrlVar('lloj') == 'hyrje'
        })
    }).done(SucceededCallbackArtNew);
}

function callWebserviceMerrArtikujMeID(params) {
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraArtikujMeID"),
        data: JSON.stringify(params)}
    ).done(SucceededCallbackArtikuj);
}

function callWebserviceMerrArtikullMeKodOseKodbar(params) {
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraArtMeKodOseKodBar"),
        data: JSON.stringify({
            kodi: params.kodi,
            index: params.index,
            date: params.date,
            iddetajim: params.iddetajim,
            magazina: params.magazina,
            magazinatKoka: params.magazinatKoka,
            merrMagMeAutorizim: params.merrMagMeAutorizim,
            idMagazina: params.idMagazina,
            meDetajim: params.meDetajim,
            sasiaNeGride: params.sasiaNeGride,
            magazinaDest: params.magazinaDest,
            idNdermarrje: pageState.idNdermarrje,
            idPerdorues: pageState.idPerdoruesi,
            merrSipasDetajimit: hfState.Get("NAGDN"),
            njesiDef: njesiDef,
            sasiaNeRresht: params.sasiaNeRresht,
            merrCmim: Utils.getUrlVar('lloj') == 'hyrje'
        })
    }).done(function (result) { SucceededCallbackArtNew(result); postShtimModifikimNgaLupa(params); });
}

function postShtimModifikimNgaLupa(object) {
    if (!object.ngaLupa)
        return;
    popupUniversal.Hide();
    $("#rowed5").jqGrid("setSelection", object.index);
}

function selectFunc(event, ui, emerfushe, idArt, kodArt) {
    var detajim = -1;
    var lloj = Utils.getUrlVar('lloj');
    var grida = $("#rowed5");
    var idRreshti = grida.getLastSel2();
    var magazine = grida.getTekstQelize('txtMagazina', idRreshti);
    var sasiaNeRresht = grida.getTekstQelize('txtSasia', idRreshti);
    var kodArtShtim = (ui !== null && ui.item != null) ? ui.item.label : kodArt;
    var magazinatKoka = merrMagazinatKoka();
    var merrMagMeAutorizim = hfState.Get("merrMagazinatMeAutorizim") == "Po";
    var magazineDest = grida.getTekstQelize('txtMagazina2', idRreshti);
    var sasiaNeGride = myJQGrid.ktheObjektMeSasitePerArtikullinMeDetajimNeGride(kodArtShtim, grida, $('#hfShtimModifikim').val(), $('#hfId').val(), false, true, undefined, { detajimi: 'txtDetajimi', detajimi2: 'txtDetajimi2t', njesia: 'txtNjesia', sasia: 'txtSasia', kodi: 'txtKodi', kodbari: 'txtKodbari', idKodi: 'txtIdKodi' });

    if (idArt != undefined && kodArt != undefined && idArt != "" && kodArt != "") {
        $(emerfushe).val(kodArt);
        if (lloj == 'hyrje') {
            callWebserviceMerrArtikullMeID({ idja: idArt, rreshti: idRreshti, data: dteDtDok.GetDate(), iddetajim: detajim, magazina: magazine, sasiaNeGride: sasiaNeGride, magazinatKoka: magazinatKoka, merrMagMeAutorizim: merrMagMeAutorizim, meDetajim: false, magazinaDest: "", sasiaNeRresht: sasiaNeRresht});
        }
        else {
            callWebserviceMerrArtikullMeID({ idja: idArt, rreshti: idRreshti, data: dteDtDok.GetDate(), iddetajim: 0, magazina: magazine, sasiaNeGride: sasiaNeGride, magazinatKoka: magazinatKoka, merrMagMeAutorizim: merrMagMeAutorizim, meDetajim: true, magazinaDest: magazineDest, sasiaNeRresht: sasiaNeRresht });
        }
        return false;
    }

    if (ui !== null && ui.item != null) {
        grida.setTekstQelize('txtKodi', idRreshti, ui.item.label);
        if (lloj == 'hyrje') {
            callWebserviceMerrArtikullMeID({ idja: ui.item.value, rreshti: idRreshti, data: dteDtDok.GetDate(), iddetajim: detajim, magazina: magazine, sasiaNeGride: sasiaNeGride, magazinatKoka: magazinatKoka, merrMagMeAutorizim: merrMagMeAutorizim, meDetajim: false, magazinaDest: "", sasiaNeRresht: sasiaNeRresht });
        }
        else {
            callWebserviceMerrArtikullMeID({ idja: ui.item.value, rreshti: idRreshti, data: dteDtDok.GetDate(), iddetajim: 0, magazina: magazine, sasiaNeGride: sasiaNeGride, magazinatKoka: magazinatKoka, merrMagMeAutorizim: merrMagMeAutorizim, meDetajim: true, magazinaDest: magazineDest, sasiaNeRresht: sasiaNeRresht });
        }
        return false;
    }
}

function selectFunc4(event, ui, emerfushe, idArt, kodArt) {
    var grida = $("#rowed5");
    var idRreshti = grida.getLastSel2();
    if (idArt != undefined && kodArt != undefined && idArt != "" && kodArt != "") {
        $(emerfushe).val(kodArt);
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraArtMeIDSet"),
            data: JSON.stringify({ idja: idArt, rreshti: idRreshti })
        }).done(SucceededCallbackArtSet);
        return false;
    }
    if (ui.item != null) {
        $(emerfushe).val(ui.item.label);
    }
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraArtMeIDSet"),
        data: JSON.stringify({ idja: ui.item.value, rreshti: idRreshti })
    }).done(SucceededCallbackArtSet);

    return false;
}

function changeFunc(event, ui, emerKodi, index) {
    var grida = $('#rowed5');
    var idRreshti = grida.getLastSel2();
    var detajim = -1;
    var lloj = Utils.getUrlVar('lloj');
    var rreshtRi = true;
    var kodArtShtim = grida.getTekstQelize('txtKodi', idRreshti);
    if (index != grida.getLastSel2()) {
        rreshtRi = false;
        kodArtShtim = grida.getTekstQelize('txtKodi', index);
    }

    var sasiaNeGride = myJQGrid.ktheObjektMeSasitePerArtikullinMeDetajimNeGride(kodArtShtim, grida, $('#hfShtimModifikim').val(), $('#hfId').val(), false, rreshtRi, index, { detajimi: 'txtDetajimi', detajimi2: 'txtDetajimi2t', njesia: 'txtNjesia', sasia: 'txtSasia', kodi: 'txtKodi', kodbari: 'txtKodbari', idKodi: 'txtIdKodi' });
    var sasiaNeRresht = grida.getTekstQelize('txtSasia', idRreshti);
    var magazinatKoka = merrMagazinatKoka();
    var merrMagMeAutorizim = hfState.Get("merrMagazinatMeAutorizim") == "Po";
        var magazine = grida.getTekstQelize('txtMagazina', index);
        var magazineDest = grida.getTekstQelize('txtMagazina2', index);
        var vleraKodit = grida.getTekstQelize('txtKodi', index);

    if (vleraKodit != "") {
        if (lloj == 'hyrje') {
            callWebserviceMerrArtikullMeKodOseKodbar({
                kodi: vleraKodit, index: index, date: dteDtDok.GetDate(), iddetajim: detajim, magazina: magazine, sasiaNeGride: sasiaNeGride,
                magazinatKoka: magazinatKoka, merrMagMeAutorizim: merrMagMeAutorizim, idMagazina: 0, meDetajim: false, magazinaDest: "", sasiaNeRresht: sasiaNeRresht
            });
        }
        else {
            callWebserviceMerrArtikullMeKodOseKodbar({
                kodi: vleraKodit, index: index, date: dteDtDok.GetDate(), magazina: magazine, idMagazina: 0, sasiaNeGride: sasiaNeGride,
                magazinatKoka: magazinatKoka, merrMagMeAutorizim: merrMagMeAutorizim, meDetajim: true, iddetajim: 0, magazinaDest: magazineDest, sasiaNeRresht: sasiaNeRresht
            });
        }
        return;
    }
    console.log('vendosArt({idRreshti: ' + index + ', artikulli: null});');
    vendosArt({ idRreshti: index, artikulli: null });
}

function changeFunc4(event, ui, emerKodi, index) {
    var grida = $('#rowed5');
    var vleraKodit = grida.getTekstQelize('txtIdArtikullSet', index);
    if (vleraKodit == "")
        return;
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraArtMeKodSet"),
        data: JSON.stringify({ kodi: vleraKodit, index: index, idNdermarrje: pageState.idNdermarrje, idPerdorues: pageState.idPerdoruesi })
    }).done(SucceededCallbackArtSet);
}

function selectFunc2(event, ui, emerfushe, iddetajim, koddetajim, idRreshtiPare) { //po
    selectFuncDetajim(ui, emerfushe, iddetajim, koddetajim, idRreshtiPare, 1);
}

function selectFunc3(event, ui, emerfushe, iddetajim, koddetajim, idRreshtiPare) { //po
    selectFuncDetajim(ui, emerfushe, iddetajim, koddetajim, idRreshtiPare, 2);
}

function selectFuncDetajim(ui, emerfushe, iddetajim, koddetajim, idRreshtiPare, lloji) { //po
    var grida = $("#rowed5");
    var idRresht;
    if (idRreshtiPare)
        idRresht = idRreshtiPare;
    else
        idRresht = grida.getLastSel2();
    var idja = (iddetajim != undefined && koddetajim != undefined && iddetajim != "" && koddetajim != "") ? iddetajim : ui.item != null ? ui.item.value : 0;
    var kodi = (iddetajim != undefined && koddetajim != undefined && iddetajim != "" && koddetajim != "") ? koddetajim : ui.item != null ? ui.item.label : '';
    if (kodi == '' && idja == 0)
        return;
    $(emerfushe).val(kodi);
    var magazine = grida.getTekstQelize('txtMagazina', idRresht);
    var idKokaMagazina = lloji == 1 ? (hfState.Get("HyrjeGjeneruarNgaMema") || !lejomod ? Utils.getUrlVar('id') : 0) : 0;
    var dokumentTransferimiOwn = lloji == 1 ? hfState.Get("HyrjeGjeneruarNgaMema") : false;
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraDetajimMeID"),
        data: JSON.stringify({
            idja: idja, index: idRresht, lloji: lloji, kodartikulli: grida.getTekstQelize('txtKodi', idRresht), idkokamagazina: idKokaMagazina,
            idNdermarrje: pageState.idNdermarrje, idPerdorues: pageState.idPerdoruesi, magazine: magazine, date: dteDtDok.GetText(),
            kontrolloImeiFifo: false, listeImei: new Array(), promocione: false, DokumentTransferimiOwn: dokumentTransferimiOwn, merrPerberesit: false
        })
    }).done(SucceededCallbackDetajim);
    return false;
}
function changeFunc2(event, ui, emerKodi, index) {//po
    changeFuncDetajimi(index, 1);
}

function changeFunc3(event, ui, emerKodi, index) {//po
    changeFuncDetajimi(index, 2);
}

function changeFuncDetajimi(index, lloji) {
    var grida = $("#rowed5");
    var data = grida.getTeDhenaRreshti(index);
    var rreshti = (data.length > 0) ? data[0] : data;
    var detajimi = lloji == 1 ? rreshti.txtDetajimi : rreshti.txtDetajimi2t;
    if (detajimi != "" && rreshti.txtKodi != "") {
        var magazine = grida.getTekstQelize('txtMagazina', index);
        var idKokaMag = lloji == 1 ? (hfState.Get("HyrjeGjeneruarNgaMema") || !lejomod ? Utils.getUrlVar('id') : 0) : 0;
        var dokumentTransferimiOwn = lloji == 1 ? hfState.Get("HyrjeGjeneruarNgaMema") : false;
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraDetajimMeKod"),
            data: JSON.stringify({
                kodi: detajimi, index: index, lloji: lloji, kodartikulli: rreshti.txtKodi, idkokamag: idKokaMag, idNdermarrje: pageState.idNdermarrje,
                idPerdorues: pageState.idPerdoruesi, magazine: magazine, date: dteDtDok.GetText(), kontrolloImeiFifo: false, listeImei: new Array(),
                promocione: false, DokumentTransferimiOwn: dokumentTransferimiOwn, merrPerberesit: false
            })
        }).done(SucceededCallbackDetajim);
    }
    else {
        var art = HfArt.Get(grida.getTekstQelize('txtIdKodi', index));
        if (!art)
            return;
        var idKategoriDetajimi = lloji == 1 ? art.IdKategoriDetajimi : art.IdKategoriDetajimi2;
        if (art && idKategoriDetajimi)
            vendosDetajim([index, null, lloji, idKategoriDetajimi, null]);
    }
}

function resetRreshtKorent(idRreshti) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    var idkodi = grida.getTekstQelize('txtIdKodi', idRreshti);//heq serialet
    if (hfSeriale.Contains(idkodi + '_' + idRreshti))
        hfSeriale.Remove(idkodi + '_' + idRreshti);
    if (hfSasiSeriale.Contains(idkodi + '_' + idRreshti))
        hfSasiSeriale.Remove(idkodi + '_' + idRreshti);
    if (idRreshti == idRow && $('#txtKodi' + idRow).val() != undefined) {
        grida.setTekstQelize('txtCmimi', idRreshti);
        grida.setTekstQelize('txtKodi', idRreshti, '');
        grida.setTekstQelize('txtIdArtikullSet', idRreshti, '');
        grida.setTekstQelize('txtIdKodi', idRreshti, ''); grida.setTekstQelize('txtIdTrupi', idRreshti, '0');
        grida.setTekstQelize('txtIdTrupiKonvertimi', idRreshti, '0');
        grida.setTekstQelize('txtIdTrupiKonvertimiUSH', idRreshti, '0');
        grida.setTekstQelize('txtIdTrupiKonvertimiUD', idRreshti, '0');
        grida.setTekstQelize('txtIdTrupiRezervimi', idRreshti, '0'); grida.setTekstQelize('txtIdKthimi', idRreshti, '0'); grida.setTekstQelize('txtIdTrupiShitjeGjenerimi', idRreshti, '0');
        grida.setTekstQelize('txtEmertimi', idRreshti, '');
        grida.setTekstQelize('txtKodbari', idRreshti, '');
        grida.setTekstQelize('txtSasia', idRreshti);
        grida.setTekstQelize('txtVlefta', idRreshti);
        grida.setTekstQelize('txtSerial', idRreshti, '');
        grida.setTekstQelize('txtDetajimi', idRreshti, '');
        grida.setTekstQelize('txtDetajimi2t', idRreshti, '');
        var objTmp = new Object();
        objTmp.value = "";
        objTmp.text = "";
        var arrayOptions = new Array(1);
        arrayOptions[0] = objTmp;
        $('#txtNjesia' + idRreshti).replaceWith(myJQGrid.myElemCombo("", 'txtNjesia', idRreshti, changeNjesia, arrayOptions, true).children()[0]);
        $('#' + 'cbTVSH' + idRreshti).replaceWith(myJQGrid.myElemCombo("", 'cbTVSH', idRreshti, changeNjesia, arrayOptions, true).children()[0]);
        updateTotalet();
        return;
    }
    grida.jqGrid('delRowData', idRreshti);
    grida.rregulloNrRendorMeTeMadh(idRreshti);
    return;
}

function kontrolloGjendjeMinMaxNeGride(artikulli, gjendjetot, gjendjeMag, magazina) {
    if (!artikulli) {
        console.info("Te te vije rende, artikulli eshte null");
        return;
    }

    var grida = $("#rowed5");
    var ids = grida.getDataIDs();
    var totali = 0;
    var totaliMag = 0;

    // Ne modifikim
    var sasiaNeGridParaTot = 0;
    var sasiaNeGridParaMag = 0;
    if (colTrupMag != undefined && colTrupMag != null && magazina != "") {
        for (var i = 0; i < colTrupMag.length; i++) {
            if (colTrupMag[i].IdArtikulli == artikulli.IdArtikulli) {
                if (colTrupMag[i].IdMag == colNjesAdminis.find(function (o) { return o.Kodi === magazina; }).IdNjesiAdministrative)
                    sasiaNeGridParaMag += colTrupMag[i].Sasia;
                sasiaNeGridParaTot += colTrupMag[i].Sasia;
            }
        }
    }

    for (var i = 0; i < ids.length; i++) {
        if (grida.getTekstQelize('txtKodi', ids[i]) === artikulli.KodArtikulli) {
            if (grida.getTekstQelize('txtMagazina', ids[i]) == magazina) {
                if (artikulli.KodNjesia1 === grida.getTekstQelize('txtNjesia', ids[i]))
                    totaliMag = totaliMag + parseFloat(grida.getTekstQelize('txtSasia', ids[i]));
                else
                    totaliMag = totaliMag + parseFloat((grida.getTekstQelize('txtSasia', ids[i]) * artikulli.KoeficientArtikulli));
            }
            if (artikulli.KodNjesia1 === grida.getTekstQelize('txtNjesia', ids[i]))
                totali = totali + parseFloat(grida.getTekstQelize('txtSasia', ids[i]));
            else
                totali = totali + parseFloat((grida.getTekstQelize('txtSasia', ids[i]) * artikulli.KoeficientArtikulli));
        }
    }

    var sasi, hyrjeDalje;
    if (Utils.getUrlVar("lloj") == 'hyrje') {
        hyrjeDalje = true;
        if (magazina != "")
            sasi = gjendjeMag - sasiaNeGridParaMag + totaliMag;
        else
            sasi = gjendjetot - sasiaNeGridParaTot + totali;
    }
    else {
        hyrjeDalje = false;
        if (magazina != "")
            sasi = gjendjeMag - sasiaNeGridParaMag - totaliMag;
        else
            sasi = gjendjetot - sasiaNeGridParaTot - totali;
    }

    if ((Gjendjemin) || (Gjendjemax))
        myJQGrid.WarnGjendje(Gjendjemin, Gjendjemax, sasi, magazina, artikulli.KodArtikulli, hyrjeDalje);
    else
        myJQGrid.WarnGjendje(artikulli.MinimumArtikulli, artikulli.MaximumArtikulli, sasi, magazina, artikulli.KodArtikulli, hyrjeDalje);
}

function vendosDetajim(result) {
    var grida = $("#rowed5");
    var idRow = grida.getLastSel2();
    if (result == null)
        return;   
    var idRreshti = result[0];
    var detajim = result[1];
    var lloji = result[2];
    var kategoria = result[3];
    var det = "";
    var detKontroll = "";
    if (lloji == 1) {
        det = "#txtDetajimi" + idRow;
        detKontroll = "txtDetajimi";
    }
    else {
        det = "#txtDetajimi2t" + idRow;
        detKontroll = "txtDetajimi2t";
    }
    var kaDetajimArtikulli = HfArt.Get(grida.getTekstQelize('txtIdKodi', idRreshti)).DetajimArtikulli;
    if (!kaDetajimArtikulli && grida.getTekstQelize(detKontroll, idRreshti) != '') {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgArtikullpaDetajim"));
        grida.setTekstQelize(detKontroll, idRreshti, undefined);
        $(det).data('Kodi', '');
        return;
    }
    if (detajim && detajim.IdDetajimArtikulli > 0) {
        if (result[6] && !result[6].Status && result[6].KodMesazhi == 501) {// detajimi ekziston por nuk eshte i lidhur me artikullin.
            if (detajim.KategoriDetajimi != kategoria) {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgDetajimMeKategoriTjeter"));
                grida.setTekstQelize(detKontroll, idRreshti, '');
                $(det).data('Kodi', '');
                return;
            }
            if (lloji == 1)
                identifikuesPyetje = 'detajimi1Lidhje';
            else identifikuesPyetje = 'detajimi2Lidhje';
            pageState.identifikuesRreshti = idRreshti;
            if (kategoria == 3 || kategoria == 4) {               
                Utils.RuajLidhjeDetajim(lloji, idRreshti, grida, pageState.idNdermarrje, pageState.idPerdoruesi);
            }
            else
                myMesazh.ShtoPyetje(hfState.Get("msgDetajimJoLidhur"));
            return;
        }
        //else vazhdon poshte (pas else te if-it me siper)
    }
    else
        if (detajim == null || detajim == undefined || detajim.IdDetajimArtikulli == -1 || detajim.IdDetajimArtikulli == 0) {
            switch (kategoria) {
                case 1: //detajim
                    myMesazh.vendosClient();
                    ShtoPyetjeCeljeDetajim(grida, detKontroll, idRreshti, kategoria, lloji);
                    return;
                case 3: //date skadence
                    if (grida.getTekstQelize(detKontroll, idRreshti) != '') {
                        if ((Date.parseLocale(grida.getTekstQelize(detKontroll, idRreshti), "dd/MM/yyyy")) == null && grida.getTekstQelize(detKontroll, idRreshti) != '') {
                            myMesazh.ShtoMesazhGabimi(hfState.Get("msgKodiDateSkadenceDuhetFormat"));
                            if (idRreshti == idRow) {
                                grida.setTekstQelize(detKontroll, idRreshti, '');
                                $(det).data('Kodi', '');
                            }
                            else
                                grida.SetTekst(detKontroll, idRreshti, '');
                        }
                        else {
                            CelDheLidhDetajimMeArtikull(grida.getTekstQelize('txtKodi', idRreshti), grida.getTekstQelize('txtIdKodi', idRreshti), lloji, kategoria, 3, grida.getTekstQelize(detKontroll, idRreshti));
                        }
                    }
                    return;
                case 4: //seri
                    if (grida.getTekstQelize(detKontroll, idRreshti) != '')
                        CelDheLidhDetajimMeArtikull(grida.getTekstQelize('txtKodi', idRreshti), grida.getTekstQelize('txtIdKodi', idRreshti), lloji, kategoria, 1, grida.getTekstQelize(detKontroll, idRreshti));
                    return;
                case 2: //serial
                    if (!detajim && grida.getTekstQelize(detKontroll, idRreshti) != "") {
                        ShtoPyetjeCeljeDetajim(grida, detKontroll, idRreshti, kategoria, lloji);
                        return;
                    }
                    if (idRreshti == idRow && grida.getTekstQelize('txtKodi', idRow) != undefined) {
                        $(det).val('');
                        $(det).data('Kodi', '');
                        if (detajim != null && detajim.IdDetajimArtikulli == 0 && (!lejomod || hfState.Get("HyrjeGjeneruarNgaMema"))) {
                            if (!lejomod && Utils.getUrlVar('lloj') == 'hyrje')
                                myMesazh.ShtoMesazhGabimi(hfState.Get("msgSerialiVendosurNukITakonBlerjes"));
                            else if (hfState.Get("HyrjeGjeneruarNgaMema"))
                                myMesazh.ShtoMesazhGabimi("IMEI " + result[7] + " per artikullin " + grida.getTekstQelize('txtKodi', idRow) + " nuk i perket ketij dokumenti!");
                        }
                        else
                            if (detajim != null)
                                myMesazh.ShtoMesazhGabimi(hfState.Get("msgDetajimiNukEkziston"));
                        return;
                    }
                    var rreshti = grida.getRowData(idRreshti);
                    if (lloji == 1)
                        rreshti.txtDetajimi = '';
                    else
                        rreshti.txtDetajimi2t = '';
                    if (!lejomod && detajim != null && detajim.IdDetajimArtikulli == 0)
                        myMesazh.ShtoMesazhGabimi(hfState.Get("msgSerialiVendosurNukITakonBlerjes"));
                    else if (detajim != null)
                        myMesazh.ShtoMesazhGabimi(hfState.Get("msgDetajimiNukEkziston"));
                    return;
                default:
                    if (idRreshti == idRow && grida.getTekstQelize('txtKodi', idRow) != undefined) {
                        $(det).val('');
                        $(det).data('Kodi', '');
                        if (!lejomod && detajim != null && detajim.IdDetajimArtikulli == 0 && Utils.getUrlVar('lloj') == 'hyrje')
                            myMesazh.ShtoMesazhGabimi(hfState.Get("msgSerialiVendosurNukITakonBlerjes"));
                        else if (detajim != null)
                            myMesazh.ShtoMesazhGabimi(hfState.Get("msgDetajimiNukEkziston"));
                        return;
                    }
                    var rreshti = grida.getRowData(idRreshti);
                    if (lloji == 1) rreshti.txtDetajimi = '';
                    else rreshti.txtDetajimi2t = '';
                    grida.jqGrid('setRowData', idRreshti, rreshti);
                    if (!lejomod && detajim != null && detajim.IdDetajimArtikulli == 0)
                        myMesazh.ShtoMesazhGabimi(hfState.Get("msgSerialiVendosurNukITakonBlerjes"));
                    else if (detajim != null)
                        myMesazh.ShtoMesazhGabimi(hfState.Get("msgDetajimiNukEkziston"));
                    return;
            }
        }

    if (lloji == 1) {
        grida.setTekstQelize('txtDetajimi', idRreshti, detajim.KodDetajimArtikulli);
        if (idRreshti == idRow)
            $(det).data('Kodi', detajim.KodDetajimArtikulli);
        if ((kategoria == 3 || kategoria == 4) && Utils.getUrlVar('lloj') != 'hyrje') {
            var art = HfArt.Get(grida.getTekstQelize('txtIdKodi', idRreshti));
            if ((kategoria == 3 && art.IdKategoriDetajimi2 == 4) || (kategoria == 4 && art.IdKategoriDetajimi2 == 3)) {
                var det2 = result[4];
                grida.setTekstQelize('txtDetajimi2t', idRreshti, det2.KodDetajimArtikulli);
                if (idRreshti == idRow) {
                    var detajimi2 = "#txtDetajimi2t" + idRreshti;
                    $(detajimi2).data('Kodi', det2.KodDetajimArtikulli);
                }
            }
        }
    }
    else {
        grida.setTekstQelize('txtDetajimi2t', idRreshti, detajim.KodDetajimArtikulli);
        if (idRreshti == idRow)
            $(det).data('Kodi', detajim.KodDetajimArtikulli);
    }
    callWebServiceInfoRow();
}

function ShtoPyetjeCeljeDetajim(grida, idDet, idRreshti, kategoria, lloji) {
    if (grida.getTekstQelize(idDet, idRreshti) != "" && kategoria != 0) {
        if (lloji == 1)
            identifikuesPyetje = 'detajimi1';
        else
            identifikuesPyetje = 'detajimi2';
        pageState.identifikuesRreshti = idRreshti;
        myMesazh.ShtoPyetje(hfState.Get("msgDetajimiVendosurNukEkzistonDoniTaCelni"));
    }
}

function CelDheLidhDetajimMeArtikull(kodiArtikullit, idArtikulli, detajimPareApoDyte, kategoria, llojDetajimi, kodDetajimi) {
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "CelDheLidhDetajimMeArtikull"),
        showLoading: true,
        data: JSON.stringify({
            idArtikulli: idArtikulli,
            detajimPareApoDyte: detajimPareApoDyte,
            llojDetajimi: llojDetajimi,
            kategoriDetajimi: kategoria,
            kodDetajimi: kodDetajimi
        })
    }).done(function (result) {
        if (result.mesazhi.Status)
            myMesazh.ShtoMesazhInformues(result.mesazhi.PershkrimMesazhi);
        else
            myMesazh.ShtoMesazhSesioni(result.mesazhi);
    });
}

function SucceededCallbackArtikuj(result) {
    for ( var i=0; i< result.length; i++)
    {
        if (result[i] == null)
            console.log('vendosArt({idRreshti: null, artikulli: null});');
        else
            console.log('vendosArt({idRreshti: ' + result[i].idRreshti + ', artikulli: plot});');
        vendosArt(result[i]);
    }
}

function SucceededCallbackArtNew(result) {
    if (result == null)
        console.log('vendosArt({idRreshti: null, artikulli: null});');
    else
        console.log('vendosArt({idRreshti: ' + result.idRreshti + ', artikulli: plot});');
    vendosArt(result);
}

function SucceededCallbackArtSet(result) {
    if (result == null)
        console.log('vendosArtSet([null, null]);');
    else
        console.log('vendosArtSet([' + result[0] + ', plot]);');
    vendosArtSet(result);
}

function vendosArt(result) {
    var grida = $("#rowed5");
    if (result == null)
        return;
    var idRreshti = result.idRreshti;
    var idRow = grida.getLastSel2();
    artikulli = result.artikulli;
    gjendjetot = result.gjendjetot;
    gjendjeMag = result.gjendja;
    var detajimi = result.detajimi;
    var detajimi2 = result.detajimi2;
    var kodbarsel = result.kodbarsel;
    var magazinaArt = result.magazinatTrupi!= undefined ? (result.magazinatTrupi[0] != undefined ? result.magazinatTrupi[0].magazina : "") : "";
    var magazinaDest = result.magazinatTrupi!= undefined ? (result.magazinatTrupi[1] != undefined ? result.magazinatTrupi[1].magazina : "") : "";
    var vleraKodit = grida.getTekstQelize('txtKodi', idRreshti);
    var emerArt = '#txtEmertimi' + idRreshti;

    if (!artikulli) {
        if (vleraKodit != "")
            myMesazh.ShtoMesazhGabimi(hfState.Get("lblMsgArtikulliNukEkziston"));
        resetRreshtKorent(idRreshti);
        return;
    }
   
    if (artikulli.LlojiArt && (cmbLloji.GetText() === 'UD' || cmbLloji.GetText() === 'UH')) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKyDokNukPerdoretPerArtAfgj"));
        resetRreshtKorent(idRreshti);
        return;
    }

    if (myJQGrid.checkIfIsTheSame(grida, artikulli, detajimi, idRreshti, grida.getTekstQelize('txtKategoria', idRreshti)))
        return;//eshte i njejti artikull

    if (artikulli.Klasa == 4)
        $('#' + idRreshti + '_txtSerialUnik').removeAttr("disabled");

    if (artikulli.Aktiv == false) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgArtikulliEshteInaktiv"));
        resetRreshtKorent(idRreshti);
        return;
    }
    if (artikulli.Klasa === 2 || artikulli.Klasa === 3) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukKryhenVeprimeMeArtikujTePastokueshem"));
        resetRreshtKorent(idRreshti);
        return;
    }
    var lloji = cmbKonfigurimi.GetText();
    if (hfState.Get("LAPP") == "Jo" && (artikulli.Klasa === 5 || artikulli.Klasa === 6) && Utils.getUrlVar("lloj") == 'hyrje' && (lloji != 'VKM')) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("NukKryhenVeprimeMeArtikujProdhimOseProdhimNeProces"));
        resetRreshtKorent(idRreshti);
        return;
    }
    if (artikulli.Klasa === 4) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukMundTeKryeniVeprimeMeArtikujTePerbere"));
        resetRreshtKorent(idRreshti);
        return;
    }
    if (grida.getTekstQelize('txtKategoria', idRreshti) == 'Artikull') {//heq serialet e artikullit te vjeter ne rast se po modifikohet i njejti rresht
        var idkodi = grida.getTekstQelize('txtIdKodi', idRreshti);
        if (hfSeriale.Contains(idkodi + '_' + idRreshti))
            hfSeriale.Remove(idkodi + '_' + idRreshti);
        if (hfSasiSeriale.Contains(idkodi + '_' + idRreshti))
            hfSasiSeriale.Remove(idkodi + '_' + idRreshti);
    }

    if (artikulli.KodArtikulli != "" && detajimi != null) {//detajimi pare
        grida.setTekstQelize('txtDetajimi', idRreshti, detajimi.KodDetajimArtikulli);
        $('#txtDetajimi' + idRreshti).data('Kodi', detajimi.KodDetajimArtikulli);
    }
    else {
        grida.setTekstQelize('txtDetajimi', idRreshti, '');
        $('#txtDetajimi' + idRreshti).data('Kodi', '');
    }

    if (artikulli.KodArtikulli != "" && detajimi2 != null) {//detajimi dyte
        grida.setTekstQelize('txtDetajimi2t', idRreshti, detajimi2.KodDetajimArtikulli);
        $('#txtDetajimi2t' + idRreshti).data('Kodi', detajimi2.KodDetajimArtikulli);
    }
    else {
        grida.setTekstQelize('txtDetajimi2t', idRreshti, '');
        $('#txtDetajimi2t' + idRreshti).data('Kodi', '');
    }

    grida.setTekstQelize('txtKodi', idRreshti, artikulli.KodArtikulli);
    myJQGrid.closeAutocomplete('txtKodi', idRreshti);
    grida.setTekstQelize('txtIdKodi', idRreshti, artikulli.IdArtikulli);

    var comboNjesia = $('#' + arrayIdKolonaGrides[8] + idRreshti);
    if (grida.getTekstQelize('txtKodi', idRreshti) != undefined && idRreshti == idRow) {
        if (kodbarsel) {
            var njesia = result.njesia;
            if (njesia == 1)
                comboNjesia.replaceWith($(myelemComboNjesiaSup(artikulli.KodNjesia1, null, idRreshti)).children()[0]);
            else
                comboNjesia.replaceWith($(myelemComboNjesiaSup(artikulli.KodNjesia2, null, idRreshti)).children()[0]);
        }
        else {
            if (njesiDef == 1)
                comboNjesia.replaceWith($(myelemComboNjesiaSup(artikulli.KodNjesia1, null, idRreshti)).children()[0]);
            else
                comboNjesia.replaceWith($(myelemComboNjesiaSup(artikulli.KodNjesia2, null, idRreshti)).children()[0]);
        }
        callWebServiceInfoRow();
    }
    else {
        if (kodbarsel) {
            var njesia = result.njesia;
            if (njesia == 1)
                grida.setTekstQelize('txtNjesia', idRreshti, artikulli.KodNjesia1);
            else
                grida.setTekstQelize('txtNjesia', idRreshti, artikulli.KodNjesia2);
        }
        else {
            if (njesiDef == 1)
                grida.setTekstQelize('txtNjesia', idRreshti, artikulli.KodNjesia1);
            else
                grida.setTekstQelize('txtNjesia', idRreshti, artikulli.KodNjesia2);
        }
    }
    if (!kontrolloRow(idRreshti))
        return;
    artikulli['txtMagazina'] = magazinaArt;
    artikulli['txtMagazina2'] = magazinaDest;
    memoryArt.Set(artikulli);
    HfArt.Set(artikulli.IdArtikulli, artikulli);

    var kodbar = "";
    if (artikulli.OColKodbare.length > 0)
        kodbar = artikulli.OColKodbare[0].Pershkrimi;
    if (hfState.Get("kodbarikurkodi") == false)
        kodbar = "";

    grida.setTekstQelize('txtKodbari', idRreshti, (kodbarsel != undefined && kodbarsel != null && kodbarsel !== "" ? kodbarsel : kodbar));

    grida.setTekstQelize('txtMagazina', idRreshti, magazinaArt.Kodi);
    grida.vendosTeDhenaPerQelizen('txtMagazina', idRreshti, "IshMagazina", magazinaArt.Kodi);
    grida.setTekstQelize('txtPershkrimmag', idRreshti, magazinaArt.Pershkrimi);
    grida.setTekstQelize('txtMagazina2', idRreshti, magazinaDest.Kodi);

    if (pershk == 1) {
        grida.setTekstQelize('txtEmertimi', idRreshti, artikulli.PershkrimArtikulli);
        $(emerArt).attr('title', artikulli.PershkrimArtikulli);
    }
    else
        if (artikulli.PershkrimiAngArtikulli !== "")
            grida.setTekstQelize('txtEmertimi', idRreshti, artikulli.PershkrimiAngArtikulli);
        else
            grida.setTekstQelize('txtEmertimi', idRreshti, artikulli.PershkrimArtikulli);

    if (Utils.getUrlVar('lloj') == 'hyrje' && result.cmimi) {
        SucceededCallbackCmimArtikulliRow(result.cmimi);
    }

    if (gjendjeartminmax == 1) {
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "merrGjendjeArtikulliMag"),
            data: JSON.stringify({ idartikulli: artikulli.IdArtikulli, mag: grida.getTekstQelize('txtMagazina', idRreshti), idNdermarrje: pageState.idNdermarrje })
        }).done(SucceededCallbackGjendjeArtikulli);
    }
    updateTotalet();
}

function MerrTeDhenaPerArtikujSet() {
    var grida = $(pageState.gridaSelector);
    var ids = grida.getDataIDs();
    var artParePerberes = "";
    var setiAktual = "";
    var data = {};
    var magazinatKoka = merrMagazinatKoka();
    var merrMagMeAutorizim = hfState.Get("merrMagazinatMeAutorizim") == "Po";
    var j = 0;
    for (var i = 0; i < ids.length; i++) {
        var row = grida.getTeDhenaRreshti(ids[i])[0];
        if (row.txtIdArtikullSet == "") {
            artParePerberes = "";
            setiAktual = "";
            continue;
        }
        if (setiAktual == row.txtIdArtikullSet) {
            if (artParePerberes != row.txtKodi)
                continue;
        }
        setiAktual = row.txtIdArtikullSet;
        data[j] = {
            kodi: setiAktual, index: ids[i], date: dteDtDok.GetDate(),
            sasia: row.txtSasia, magazinatKoka: magazinatKoka, merrMagMeAutorizim: merrMagMeAutorizim,
            idNdermarrje: pageState.idNdermarrje, idPerdoruesi: pageState.idPerdoruesi, idArtikulli: row.txtIdKodi
        };
        j++;
    }

    if (data.length == 0)
        return;

    $.ajax({
        pritPergjigje: true,
        showLoading: false,
        url: Utils.getServerApiUrl("Rregjistrime", "merrGjitheArtikujtSet"),
        data: JSON.stringify(data)
    }).done(function (result) {
        for (i = 0; i < result.length; i++)
            SucceededCallbackSet(result[i], data[i].kodi, true);
    });
}

function shtoArtikullSet() {
    var grida = $("#rowed5");
    idRow = grida.getLastSel2();
    var vleraSetKodit = grida.getTekstQelize('txtIdArtikullSet', idRow);
    var vleraKodit = grida.getTekstQelize('txtKodi', idRow);
    var vleraSasia = grida.getTekstQelize('txtSasia', idRow);
    var magazinatKoka = merrMagazinatKoka();
    var merrMagMeAutorizim = hfState.Get("merrMagazinatMeAutorizim") == "Po";

    if (vleraSetKodit != "" && vleraKodit == "") {
        $.ajax({
            pritPergjigje: true,
            showLoading: true,
            url: Utils.getServerApiUrl("Rregjistrime", "merrArtikujSet"),
            data: JSON.stringify({
                kodi: vleraSetKodit, index: idRow, date: dteDtDok.GetDate(), sasia: vleraSasia, magazinatKoka: magazinatKoka, merrMagMeAutorizim: merrMagMeAutorizim,
                idNdermarrje: pageState.idNdermarrje, idPerdoruesi: pageState.idPerdoruesi
            })
        }).done(function (result) { SucceededCallbackSet(result, vleraSetKodit, false); });
        return;
    }
}
var colPerberesList = new Array();
function SucceededCallbackSet(result, seti, vetemPerberesit) {
    var grida = $("#rowed5");
    idRreshti = grida.getLastSel2();
    if (!vetemPerberesit)
        grida.jqGrid('saveRow', idRreshti, null, 'clientArray', {});
    if (result === null)
        return;

    colartikulli = result[1];
    colPerberes = result[3];
    var length = Object.keys(colartikulli).length;
    var colPerberesLength = colPerberesList.length;
    for (ind = 0; ind < length; ind++) {
        if (!vetemPerberesit) {
            colartikulli[ind].idRreshti = idRreshti + ind;
            vendosArt(colartikulli[ind]);
            grida.setTekstQelize('txtIdArtikullSet', idRreshti + ind, seti);
            grida.setTekstQelize('txtSasia', idRreshti + ind, result[2][ind]);
            grida.setLastSel2(idRreshti + ind);
            grida.jqGrid('editRow', idRreshti + ind, false);
            grida.jqGrid('saveRow', idRreshti + ind, null, 'clientArray', {});
        }
        colPerberes[ind].IdRreshti = (vetemPerberesit? result[0] : idRreshti) + ind;
        colPerberes[ind].Id = colPerberesLength;
    }
    colPerberesList = colPerberesList.concat(colPerberes);
    if (!vetemPerberesit) {
        grida.setLastSel2(idRreshti);
        grida.jqGrid('editRow', idRreshti, false);
        grida.shtoRresht({}, idRreshti + length);
        grida.selektoRreshtin(idRreshti + length, true);
        grida.rregulloNrRendor("txtNrRendor");
    }
}

function vendosArtSet(result) {
    var grida = $("#rowed5");
    if (result !== null) {
        var idRreshti = result[0];
        artikulli = result[1];
        var kodi = "#txtIdArtikullSet" + idRreshti;
        var vleraKodit = grida.getTekstQelize('txtIdArtikullSet', idRreshti);
        if (!artikulli) {
            if (vleraKodit != "")
                myMesazh.ShtoMesazhGabimi(hfState.Get("lblMsgArtikulliNukEkziston"));
            resetRreshtKorent(idRreshti);
            return;
        }
        if (artikulli.Aktiv == false) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgArtikulliEshteInaktiv"));
            resetRreshtKorent(idRreshti);
            return;
        }
        if (artikulli.Klasa != 4) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukMundTeKryeniVeprimeMeArtikujJoTePerbere"));
            resetRreshtKorent(idRreshti);
            return;
        }
        grida.setTekstQelize('txtKodi', idRreshti, '');
        if (grida.getTekstQelize('txtIdArtikullSet', idRreshti) != undefined) {
            grida.setTekstQelize('txtIdArtikullSet', idRreshti, artikulli.KodArtikulli);
            myJQGrid.closeAutocomplete('txtIdArtikullSet', idRreshti);
            return;
        }
        if (grida.getTekstQelize('txtIdArtikullSet', idRreshti) == artikulli.KodArtikulli) {
            return; //eshte i njejti artikull
        }
        grida.setTekstQelize('txtIdArtikullSet', idRreshti, artikulli.KodArtikulli);
        return;
    }
}

function SucceededCallbackDetajim(result) {//po
    vendosDetajim(result);
}

function callWebServiceInfoRow(idRow) {
    if (!eshteInfoHapur()) return;
    var grida = $('#rowed5');
    if (idRow === undefined)
        idRow = grida.getLastSel2();
    var idKodi = grida.getTekstQelize('txtIdKodi', idRow);
    if (idKodi === "" || idKodi === undefined)
        return;
    if (infoArt) {
        var detajim = grida.getTekstQelize('txtDetajimi', idRow);
        detajim = (detajim == undefined || detajim == '') ? -1 : detajim;
        var detajim2 = grida.getTekstQelize('txtDetajimi2t', idRow);
        detajim2 = (detajim2 == undefined || detajim2 == '') ? -1 : detajim2;
        var mag = grida.getTekstQelize('txtMagazina', idRow);
        mag = (mag == undefined || mag == '') ? -1 : mag;
        var idViti = hfState.Get('idViti');
        pastroInfoArt();
        $.ajax({ url: Utils.getServerApiUrl("Rregjistrime", "mbushInfoArtikulliMeDetajime"), data: JSON.stringify({ idkodi: idKodi, data: dteDtDok.GetDate(), detajim: detajim, index: idRow, idInfo: idInfoArt, detajim2: detajim2, idViti: idViti, idPerdoruesi: pageState.idPerdoruesi, idklient: 0, mag: mag, njesiart: "", idKarta: 0 }) }).done(SucceededCallbackInfoArt);
    }
}

function SucceededCallbackChangeMag(result) {//po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    grida.setTekstQelize('txtPershkrimmag', idRresht, result);
}

function pastroInfoArt() {
    navbar.GetGroupByName('Artikulli').SetExpanded(false);
    lbxZgjedhur.ClearItems();
}

function vendosInfo(listBoxInfo, result) {
    listBoxInfo.ClearItems();
    if (result.vlerat === null)
        for (var i = 0; i < result.colInfoTrupi.length; i++) {
            listBoxInfo.AddItem([result.colInfoTrupi[i].PershkrimKolone, ''], result.colInfoTrupi[i].EmerKolone);
        }
    else {
        if (result.colInfoTrupi.length != result.vlerat.length) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgKaGabimTekRezultatiInfos"));
            return;
        }
        for (var i = 0; i < result.colInfoTrupi.length; i++) {
            listBoxInfo.AddItem([result.colInfoTrupi[i].PershkrimKolone, result.vlerat[i].toString()], result.colInfoTrupi[i].EmerKolone);
        }
    }
    refreshInfoLists();
}

function refreshInfoLists() {
    var artExpanded = navbar.GetGroupByName('Artikulli').GetExpanded();
    navbar.GetGroupByName('Artikulli').SetExpanded(!artExpanded);
    navbar.GetGroupByName('Artikulli').SetExpanded(artExpanded);
}

function SucceededCallbackInfoArt(result) {
    var idRresht = $('#rowed5').getLastSel2();
    if (result.idRreshti == idRresht && result.colInfoTrupi && result.colInfoTrupi.length != 0) {
        navbar.GetGroupByName('Artikulli').SetExpanded(true);
        vendosInfo(lbxZgjedhur, result);
    }
}

function eshteInfoHapur() {
    return !splitter.GetPane(1).IsCollapsed();
}

function spliterPaneExpanding(s, e) {
    hapMbyllInfo(true);
    var idRresht = $('#rowed5').getLastSel2();
    callWebServiceInfoRow(idRresht);
}

function spliterPaneCollapsing(s, e) {
    hapMbyllInfo(false);
}

function spliterPaneCollapsed(s, e) {
    var grida = $('#rowed5');
    if ($('#divgride2').width() != null) {
        myJQGrid.fixGridWidth(grida, $('#divgride2'));
    }
}

function spliterPaneResized(s, e) {
    spliterPaneCollapsed(s, e);
}

function hapMbyllInfo(infoExpanded) {
    if (infoExpanded)
        splitter.GetPane(1).SetSize(300);//madhesia default
    if (infoExpanded && (infoArt)) {
        if (!eshteInfoHapur())
            splitter.GetPane(1).Expand();
        if (infoArt)
            navbar.GetGroupByName('Artikulli').SetVisible(true);
        else
            navbar.GetGroupByName('Artikulli').SetVisible(false);
        navbar.GetGroupByName('Artikulli').SetExpanded(false);
    }
    else if (!infoExpanded && eshteInfoHapur()) {
        if (infoArt)
            navbar.GetGroupByName('Artikulli').SetVisible(true);
        else
            navbar.GetGroupByName('Artikulli').SetVisible(false);
        navbar.GetGroupByName('Artikulli').SetExpanded(false);
    }
    else {
        if (eshteInfoHapur())
            splitter.GetPane(1).CollapseBackward();
        navbar.GetGroupByName('Artikulli').SetVisible(false);
    }
}

function formGridColsArray(isLidhur) {
    var hfGridKod = $('#hfGridaKodi');
    var hfGridDetajim = $('#hfGridaDetajimi');
    var IdKonfigAmbjenteLupat = [{ hfVar: hfGridKod, kodiText: "txtKodi" }, { hfVar: hfGridDetajim, kodiText: "txtDetajimi" }];
    myJQGrid.formArrayKolGrides($("input[id$='HfGridCol']"), arrayIdKolonaGrides, arrayPershkrimiKolonaGrides, arrayVisibleKolonaGrides, arrayReadOnlyKolonaGrides, isLidhur, arrayWidthKolonaGrides, IdKonfigAmbjenteLupat, arrayRenditjeKolonaGrides);
}

/*
Function: myElemCmimi

Nderton nje textbox per te vendosur cmimin.
*/
function myElemCmimi(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var disable = 'False';
    if (koloneDisabled() || arrayReadOnlyKolonaGrides[10] == 'True' || !lejomod || cmbLloji.GetText() === 'FD' || cmbLloji.GetText() === 'UD')
        disable = 'True';
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: disable, indexRow: idRresht, id: "txtCmimi", onKeyDown: vendosVleftat, onFocusout: kontrolloVlereBosh });
    //return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, disable, idRresht, 'txtCmimi', vendosVleftat, hfFormatNumri, kontrolloVlereBosh);
}

function myElemCombo(value) {//po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var disabled = false;
    var objTmp;
    var arrayOptions = new Array(2);
    objTmp = new Object();
    objTmp.value = 1;
    objTmp.text = 'Artikull';
    arrayOptions[0] = objTmp;
    objTmp = new Object();
    objTmp.value = 2;
    objTmp.text = 'Makro';
    arrayOptions[1] = objTmp;

    if (koloneDisabled() || arrayReadOnlyKolonaGrides[1] == 'True' || !lejomod) {
        disabled = true;
    }
    return myJQGrid.myElemCombo(value, arrayIdKolonaGrides[1], idRresht, change, arrayOptions, disabled);
}

function change() {//po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    resetRreshtKorent(idRresht);
}

/*
Function: myElemKodi

Nderton nje textbox dhe nje buton per te zgjedhur artikullin ose makron sipas llojit te zgjedhur tek combo Kategoria
*/
function myElemKodi(value, options) {
    var idRresht = $('#rowed5').getLastSel2();
    disabled = koloneDisabled() || !lejomod ? 'True' : arrayReadOnlyKolonaGrides[2];
    return myJQGrid.myElemKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[2], ButtonClickKodi, keyPressKodi, changeFunc, fokusi, lostFocusKoloneFundit);
}
function myElemKodiSet(value, options) {
    var idRresht = $('#rowed5').getLastSel2();
    disabled = koloneDisabled() || !lejomod ? 'True' : arrayReadOnlyKolonaGrides[24];
    var vleraSetKodit = $('#rowed5').getTekstQelize('txtIdArtikullSet', idRresht);
    var vleraKodit = $('#rowed5').getTekstQelize('txtKodi', idRresht);
    if (vleraKodit != "")
        disabled = 'True';
        return myJQGrid.myElemKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[24], ButtonClickKodiSet, keyPressKodiSet, changeFunc4, fokusi, lostFocusKoloneFundit);
}
function myelemSeriali(value, options) {//po
    var idRresht = $('#rowed5').getLastSel2();
    return myJQGrid.myElemKodi(value, options, koloneDisabled() || !lejomod ? 'True' : arrayReadOnlyKolonaGrides[16], idRresht, arrayIdKolonaGrides[16], ButtonClickSeriali, keyPressSeriali, changeFuncSeriali);
}
function myelemIdKodi(value, options) {//po
    var idRresht = $('#rowed5').getLastSel2();
    return myJQGrid.myElemIdKodi(value, options,(koloneDisabled() ||  !lejomod) ? 'True' : arrayReadOnlyKolonaGrides[13], idRresht, arrayIdKolonaGrides[13]);
}

function myelemIdTrupiKonvertimi(value, options) {//po
    var idRresht = $('#rowed5').getLastSel2();
    disabled = !lejomod ? 'True' : arrayReadOnlyKolonaGrides[15];
    return myJQGrid.myElemIdKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[15]);
}

function myelemIdTrupiKonvertimiUSH(value, options) {//po
    var idRresht = $('#rowed5').getLastSel2();
    disabled = !lejomod ? 'True' : arrayReadOnlyKolonaGrides[17];
    return myJQGrid.myElemIdKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[17]);
}

function myelemIdTrupiKonvertimiUD(value, options) {//po
    var idRresht = $('#rowed5').getLastSel2();
    disabled = !lejomod ? 'True' : arrayReadOnlyKolonaGrides[18];
    return myJQGrid.myElemIdKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[18]);
}
function myelemIdTrupi(value, options) {//po
    var idRresht = $('#rowed5').getLastSel2();
    disabled = !lejomod ? 'True' : arrayReadOnlyKolonaGrides[19];
    return myJQGrid.myElemIdKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[19]);
}
function myelemIdKthimi(value, options) {//po
    var idRresht = $('#rowed5').getLastSel2();
    disabled =(koloneDisabled() ||  !lejomod )? 'True' : arrayReadOnlyKolonaGrides[21];
    return myJQGrid.myElemIdKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[21]);
}
function myelemIdTrupiShitjeGjenerimi(value, options) {//po
    var idRresht = $('#rowed5').getLastSel2();
    disabled = (koloneDisabled() || !lejomod ) ? 'True' : arrayReadOnlyKolonaGrides[22];
    return myJQGrid.myElemIdKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[22]);
}
function myelemIdTrupiRezervimi(value, options) {//po
    var idRresht = $('#rowed5').getLastSel2();
    disabled = (koloneDisabled() || !lejomod )? 'True' : arrayReadOnlyKolonaGrides[14];
    return myJQGrid.myElemIdKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[14]);
}
function keyPressSeriali(e) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if (e.which == 13) {
        var serial = grida.getTekstQelize('txtSerial', idRresht);
        var idkodi = grida.getTekstQelize('txtIdKodi', idRresht);
        if (grida.getTekstQelize('txtKategoria', idRresht) == "Makro" || grida.getTekstQelize('txtKodi', idRresht) == "") {
            myMesazh.ShtoMesazhGabimi('Ju lutem zgjidhni nje artikull  afatgjate!');
            grida.setTekstQelize('txtSerial', idRresht, "");
            return;
        }
        if (grida.getTekstQelize('txtKategoria', idRresht) === "Artikull" && HfArt.Contains(idkodi)) {
            var artikulli = HfArt.Get(idkodi);
            if (!artikulli.LlojiArt) {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniNjeArtikullAfatGjate"));
                grida.setTekstQelize('txtSerial', idRresht, "");
                return;
            }
        }
        if (serial != "" && serial != "Me serial" && serial != "Pa serial") {
            var gridIds = grida.jqGrid('getDataIDs');
            var arrayMeSeriale = new Array();
            var a = 0;
            var sasishumaperserial = 0;
            for (i = 0; i < gridIds.length; i++) {
                if (idRresht == gridIds[i])
                    continue;
                if (hfSeriale.Contains(idkodi + '_' + gridIds[i])) {
                    arrayMeSeriale[a] = hfSeriale.Get(idkodi + '_' + gridIds[i]);
                    if (JSON.parse(arrayMeSeriale[a])[0].AqtSerialKod == serial)//per serialet e ndashem kemi vetem nje serial per resht dhe marrim sasine
                        sasishumaperserial += hfSasiSeriale.Get(idkodi + '_' + gridIds[i]);// grida.getTekstQelize('txtSasia', gridIds[i])
                    a++;
                }
            }

            $.ajax({
                url: Utils.getServerApiUrl("Rregjistrime", "CelSerialNqsNukEkziston"),
                data: JSON.stringify({
                    kodserial: serial, idndermarje: pageState.idNdermarrje, idperdoruesi: pageState.idPerdoruesi, idartikulli: idkodi, lastsel: idRresht,
                    serialeekzistuese: hfSeriale.Contains(idkodi + '_' + idRresht) ? hfSeriale.Get(idkodi + '_' + idRresht) : "", serialeteperdoruraNeKeteFature: arrayMeSeriale,
                    celnqsnukekziston: Utils.getUrlVar("lloj") == 'hyrje' ? true : false, magazina: grida.getTekstQelize('txtMagazina', idRresht), sasishuma: sasishumaperserial, iddokmodmagazine: $('#hfId').val()
                })
            }).done(SucceededCallbackSerial);
        }
        grida.setTekstQelize('txtSerial', idRresht, "");
    }
}
function SucceededCallbackSerial(result) {
    if (result[2] != "") {
        myMesazh.ShtoMesazhGabimi(result[2]);
        return;
    }
    var grida = $('#rowed5');
    var artikulli = HfArt.Get(result[0]);
    if (artikulli.MeSerial)
        grida.setTekstQelize('txtSasia', result[4], result[3]);
    vendosVleftat('txtSasia', result[4]);
    if (hfSeriale.Contains(result[0] + '_' + result[4])) {
        hfSeriale.Set(result[0] + '_' + result[4], result[1]);
    }
    else hfSeriale.Add(result[0] + '_' + result[4], result[1]);

    if (hfSasiSeriale.Contains(result[0] + '_' + result[4])) {
        hfSasiSeriale.Set(result[0] + '_' + result[4], artikulli.MeSerial ? 1 : grida.getTekstQelize('txtSasia', result[4]));
    }
    else hfSasiSeriale.Add(result[0] + '_' + result[4], artikulli.MeSerial ? 1 : grida.getTekstQelize('txtSasia', result[4]));
}
function changeFuncSeriali() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var idkodi = grida.getTekstQelize('txtIdKodi', idRresht);
    if (hfSeriale.Contains(idkodi + '_' + idRresht) && hfSeriale.Get(idkodi + '_' + idRresht) != "[]")
        grida.setTekstQelize('txtSerial', idRresht, "Me serial");
    else grida.setTekstQelize('txtSerial', idRresht, "Pa serial");
}
function ButtonClickSeriali() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var idkodi = grida.getTekstQelize('txtIdKodi', idRresht);
    if (grida.getTekstQelize('txtKodi', idRresht) == "" || grida.getTekstQelize('txtKategoria', idRresht) == "Makro") {
        myMesazh.ShtoMesazhGabimi('Ju lutem zgjidhni nje artikull  afatgjate!');
        return;
    }
    if (grida.getTekstQelize('txtKategoria', idRresht) === "Artikull" && HfArt.Contains(idkodi)) {
        var artikulli = HfArt.Get(idkodi);
        if (!artikulli.LlojiArt) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniNjeArtikullAfatGjate"));
            return;
        }

        myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhniSerialet"), 'LupaSeriale.aspx?vjennga=' + Utils.getUrlVar("lloj") + '&magazina=' + grida.getTekstQelize('txtMagazina', idRresht) + '&id=' + grida.getTekstQelize('txtIdKodi', idRresht) + '&lastsel=' + idRresht + '&mecope=' + !artikulli.MeSerial + '&sasia=' + (artikulli.MeSerial ? grida.getTekstQelize('txtSasia', idRresht) : 1) + '&data=' + dteDtDok.GetText() + '&iddokmag=' + $('#hfId').val(), 900, 600);
    }
}

function ButtonClickArkiva() {//po
    popupUniversal.SetHeaderText(hfState.Get("regjisDokZgjidhDokPerTeBashkengjitur"));
    popupUniversal.SetSize(738, 548);

    popupUniversal.SetContentUrl('LupaArkiva.aspx?vjenNga=RegjistrimMagazine&veprimi=magazina' + '&idDok=' + $('#hfArkivaDokId').val() + "&tmpfolder=" + hfArkiva.Get("rootFolder"));
    //popupUniversal.SetContentUrl('LupaArkiva.aspx?vjenNga=RegjistrimMagazine&veprimi=magazina' + '&idDok=' + $('#hfArkivaDokId').val() + '&shtim_modifikim=' + $('#hfShtimModifikim').val());
    popupUniversal.Show();
}

function keyPressKodi(event) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    try {
        var vlera = event.target.value;
        if (vlera == "")
            $('#' + arrayIdKolonaGrides[2] + idRresht).autocomplete("close");
        else
            callWebserviceKodi(vlera);
    }
    catch (e) { }
}
function keyPressKodiSet(event) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    try {
        var vlera = event.target.value;
        if (vlera == "")
            $('#' + arrayIdKolonaGrides[24] + idRresht).autocomplete("close");
        else
            callWebserviceKodiSet(vlera);
    }
    catch (e) { }
}

function myValueButtonFshi(elem, operation, value) {
    var idRresht = $('#rowed5').getLastSel2();
        return myJQGrid.myValueButtonFshi(koloneDisabled() || arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1] == 'True' || !lejomod, idRresht, "#rowed5");
}

/*
Function: myElemButon

Nderton nje buton per te fshire nje rresht te grides
*/
function myElemButtonFshi() {
    var idRresht = $('#rowed5').getLastSel2();
    callWebServiceInfoRow();
    if (koloneDisabled() || arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1] == 'True' || !lejomod)
        return myJQGrid.myElemButtonFshi(true, idRresht, "#rowed5", lostFocusKoloneFundit);
    else
        return myJQGrid.myElemButtonFshi(false, idRresht, "#rowed5", lostFocusKoloneFundit);
}

function myElemButtonSerialeUnike() {
    var grida = $(pageState.gridaSelector);
    var idRow = grida.getLastSel2();
    var idSeti = grida.getTekstQelize('txtIdArtikullSet', idRow);
    var disabled = idSeti == "";
    return myJQGrid.myElemButtonSerialetUnike(disabled, idRow);
}

function ButtonSerialeUnikeClicked(index, event) {
    event.preventDefault();
    var grida = $(pageState.gridaSelector);
    var idkodi = grida.getTekstQelize('txtIdKodi', index);
    var seti = colPerberesList.filter(function (s) { return s.IdArtikulli == idkodi && s.IdRreshti == index; });
    HapLupeSerialeshUnike(seti[0].IdSeti);
}

/*
Function: myElemEmertimi

Nderton nje textbox ku vendoset emertimi i artikullit apo makros
*/
function myElemEmertimi(value) {
    var idRresht = $('#rowed5').getLastSel2();
    return myJQGrid.myElemEmertimi(value, koloneDisabled() || !lejomod ? 'True' : arrayReadOnlyKolonaGrides[4], idRresht, arrayIdKolonaGrides[4]); // 'txtEmertimi'
}

function myElemKodbari(value) {//po
    var idRresht = $('#rowed5').getLastSel2();
    return myJQGrid.myElemEmertimi(value, koloneDisabled() || !lejomod ? 'True' : arrayReadOnlyKolonaGrides[3], idRresht, arrayIdKolonaGrides[3]);
}

/*
Function: myElemKodi

Nderton nje textbox dhe nje buton per te zgjedhur detajimin e artikullit.
*/
function myElemDetajimet(value, options) {
    var idRresht = $('#rowed5').getLastSel2();
    disabled = (arrayReadOnlyKolonaGrides[5] == 'True') || (!lejomod && (vlerakushti == "Po"));
    if (koloneDisabled())
        disabled = false;
    return myJQGrid.myElemKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[5], ButtonClickDetajimet, keyPressDet, changeFunc2, fokusi, lostFocusKoloneFundit);
}

function myElemDetajimet2(value, options) {
    var idRresht = $('#rowed5').getLastSel2();
    disabled = koloneDisabled() || !lejomod ? 'True' : arrayReadOnlyKolonaGrides[6];
    return myJQGrid.myElemKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[6], ButtonClickDetajimet2, keyPressDet2, changeFunc3, fokusi, lostFocusKoloneFundit);
}

/*
Function: myElemShenime
Nderton nje textbox per te vendosur shenime tek rreshtat e grides.
*/
function myElemShenime(value) {//po
    var idRresht = $('#rowed5').getLastSel2();
    disabled = koloneDisabled() ||  (arrayReadOnlyKolonaGrides[23] == 'True') || !lejomod;
    return myJQGrid.myElemEmertimi(value, disabled, idRresht, arrayIdKolonaGrides[23]);
}

/*
Function: myElemSasia

Nderton nje textbox per te vendosur sasine.
*/
function myElemSasia(value, options) {//po
    var disable = 'False';
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if (koloneDisabled() || arrayReadOnlyKolonaGrides[9] == 'True' || !(vlerakushti == "Po" || vlerakushti == ""))
        disable = 'True';
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: disable, indexRow: idRresht, id: "txtSasia", onFocusout: function (id, indexRow, e) { vendosVleftat(id, indexRow, e); changedSasia(); }});
    //return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, disable, idRresht, 'txtSasia', vendosVleftat, hfFormatNumri, changedSasia);
}

function changedSasia() {
    var grida = $('#rowed5');
    var idRreshti = grida.getLastSel2();
    var idkodi = grida.getTekstQelize('txtIdKodi', idRreshti);//heq serialet
    if (HfArt.Contains(idkodi)) {
        if (!HfArt.Get(idkodi).MeSeriale && hfSeriale.Contains(idkodi + '_' + idRreshti)) {
            if (hfSasiSeriale.Contains(idkodi + '_' + idRreshti)) {
                hfSasiSeriale.Set(idkodi + '_' + idRreshti, grida.getTekstQelize('txtSasia', idRreshti));
            }
        }
    }
    kontrolloVlereBosh();
    myJQGrid.cancelQuickChangeSasi('txtSasia' + idRreshti, grida.getTekstQelize('txtKodi', idRreshti), grida.getTekstQelize('txtSasia', idRreshti), grida.getTekstQelize('txtNjesia', idRreshti), changeCmimSipasSasi);
}

function changeCmimSipasSasi(kod, sasia, last, njesia) {
    if (kod != "" && pageState.kushte.VCVS && pageState.lloji != 'modifikim') {//&&pageState.veprimi != 'blerje'
        callWebserviceCmimArtikulliRow(kod, dteDtDok.GetText(), njesia, last, sasia);
    }
}

function callWebserviceCmimArtikulliRow(kodartikulli, date, njesi, idRreshti, sasi) {//po
    myJQGrid.beginWebserviceCmimArtikulliRow(idRreshti, "txtCmimi");
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheCmimArtikulliRowNivelBaze"),
        data: JSON.stringify({
            idNdermarrje: pageState.idNdermarrje, idPerdoruesi: pageState.idPerdoruesi, kodArtikulli: kodartikulli, date: date, njesia: njesi, idRreshti: idRreshti, sasi: sasi, shitjeblerje: 1
        })
    }).done(function (res) {
        myJQGrid.endWebserviceCmimArtikulliRow(idRreshti, "txtCmimi");        
        SucceededCallbackCmimArtikulliRow(res);
    });
}

/*
Function: SucceededCallbackCmimArtikulli
    SucceededCallbackCmimArtikulliRow
Vendos cmimin qe i eshte caktuar artikullit(nese ka).
*/
function SucceededCallbackCmimArtikulliRow(result) {//po
    var grida = $(pageState.gridaSelector);
    var idRreshti = result[0];
    var cm = result[1];
    if (grida.getTekstQelize('txtKodi', idRreshti) == '') {
        return;
    }

    
    if (cm != '' && cm != '0' && cm != null && !Utils.IsNullOrEmpty(cm)) {        
        grida.setTekstQelize('txtCmimi', idRreshti, cm);
    }
    else
        grida.setTekstQelize('txtCmimi', idRreshti);
    vendosVleftat('txtCmimi', idRreshti);
}


//function beginWebserviceCmimArtikulliRow(idRreshti) {
//    var counterCmimi = 0;   
//    if ($("#txtCmimi" + idRreshti).data('pending') && $("#txtCmimi" + idRreshti).data('pending') != "")
//        counterCmimi = parseInt($("#txtCmimi" + idRreshti).data('pending'));
//    $("#txtCmimi" + idRreshti).data('pending', counterCmimi + 1);
//}

//function endWebserviceCmimArtikulliRow(idRreshti) {
//    if ($("#txtCmimi" + idRreshti).data('pending') && $("#txtCmimi" + idRreshti).data('pending') != "") {
//        var counterCmimi = $("#txtCmimi" + idRreshti).data('pending');
//        if (counterCmimi == 1)
//            $("#txtCmimi" + idRreshti).removeData('pending');
//        else
//            $("#txtCmimi" + idRreshti).data('pending', counterCmimi - 1);
//    }
//}


/*
Function: myElemVlefta

Nderton nje textbox per te vendosur vleften.
*/
function myElemVlefta(value, options) {
    var disable = 'False';
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var readonly = 'False';
    if (koloneDisabled() || arrayReadOnlyKolonaGrides[11] == 'True' || !lejomod || cmbLloji.GetText() == 'FD' || cmbLloji.GetText() === 'UD')
        disable = 'True';
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: disable, indexRow: idRresht, id: "txtVlefta", onKeyDown: vendosVleftat, onFocusout: kontrolloVlereBosh });
    //return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, disable, idRresht, 'txtVlefta', vendosVleftat, hfFormatNumri, kontrolloVlereBosh);
}

var pyeturDoni = 0;
function kontrolloRow(rowid) {
    var grida = $("#rowed5");
    var lloji = grida.getTekstQelize('txtKategoria', rowid);
    var detajim1 = grida.getTekstQelize('txtDetajimi', rowid);
    var detajim2 = grida.getTekstQelize('txtDetajimi2t', rowid);
    var name = grida.getTekstQelize('txtKodi', rowid);
    var njesia = grida.getTekstQelize('txtNjesia', rowid);
    if ((konfirmimArt == 1 && pyeturDoni == 0) || shtoSasiNeRreshtTeRi == false) {
        var gridIds = grida.jqGrid('getDataIDs');
        for (i = 0; i < gridIds.length; i++) {
            if (rowid == gridIds[i])
                continue;
            var editorkodi = grida.getTekstQelize('txtKodi', gridIds[i]);
            var editordetajimi1 = grida.getTekstQelize('txtDetajimi', gridIds[i]);
            var editordetajimi2 = grida.getTekstQelize('txtDetajimi2t', gridIds[i]);
            var editorlloji = grida.getTekstQelize('txtKategoria', gridIds[i]);
            var editornjesia = grida.getTekstQelize('txtNjesia', gridIds[i]);
            if (editorkodi == name && editordetajimi1 == detajim1 && editordetajimi2 == detajim2 && editorkodi != "" && editorlloji == lloji && editornjesia == njesia) {
                if (shtoSasiNeRreshtTeRi == false) {
                    var sasia = parseFloat(grida.getTekstQelize('txtSasia', gridIds[i])) + parseFloat(grida.getTekstQelize('txtSasia', rowid));
                    grida.setTekstQelize('txtSasia', gridIds[i], sasia);
                    console.log('txtSasia:' + sasia);
                    vendosVleftat('txtSasia', gridIds[i]);
                    resetRreshtKorent(rowid);
                    //var cmimi = grida.getTekstQelize('txtCmimi', gridIds[i]);
                    //grida.setTekstQelize('txtVlefta', gridIds[i], (parseFloat(sasia) + 1) * parseFloat(cmimi));
                    //resetRreshtKorent(rowid);
                    //updateTotalet();
                    //if (fokusi == 1) {
                    //    grida.jqGrid('saveRow', rowid, null, 'clientArray');
                    //    grida.jqGrid('setSelection', gridIds[i], true);
                    //    lastsel2 = gridIds[i];
                    //    grida.jqGrid('editRow', lastsel2, false);
                    //    $('#txtSasia' + lastsel2).focus();
                    //}
                    //  alert(fokusi);
                    return false;
                }
                if (konfirmimArt == 1) {
                    myMesazh.vendosClient();
                    identifikuesPyetje = "artikulli";
                    rreshtidyfish = rowid;
                    myMesazh.ShtoPyetje(hfState.Get("msgEkzistonArtikullNeGride"), true);
                    pyeturDoni = 1;
                    return true;
                }
            }
        }
    }
    return true;
}

/*
Function: fshiClicked

Fshin nje rresht te grides

Parameters:

index - Id e rreshtit qe do fshihet
*/
function fshiClicked(index) {
    var grida = $('#rowed5');
    if (grida.getTekstQelize('txtKategoria', index) == 'Artikull') {
        var idkodi = grida.getTekstQelize('txtIdKodi', index);
        var idSet = grida.getTekstQelize('txtIdArtikullSet', index);
        if (hfSeriale.Contains(idkodi + '_' + index))
            hfSeriale.Remove(idkodi + '_' + index);
        if (hfSasiSeriale.Contains(idkodi + '_' + index))
            hfSasiSeriale.Remove(idkodi + '_' + index);

        var art = memoryArt.Get(idkodi);
        if (art != undefined && art.IdFormatSeriali != 0) {
            FshiSeriale(idkodi, grida.getTekstQelize("txtMagazina", index), !Utils.IsNullOrEmpty(idSet));
        }
        if (art != undefined && idSet != '') {
            HiqArtikullSetNgaPerberesit(index, idSet, idkodi);
        }
    }
    var ids = grida.getDataIDs();
    for (var i = 0; i < ids.length; i++) {
        if (ids[i] <= index)
            continue;
        if (grida.getTekstQelize('txtNrRendor', ids[i]) == undefined || typeof (grida.getTekstQelize('txtNrRendor', ids[i])) == 'undefined' || grida.getTekstQelize('txtNrRendor', ids[i]) == '')
            continue;
        grida.setTekstQelize('txtNrRendor', ids[i], parseInt(grida.getTekstQelize('txtNrRendor', ids[i])) - 1);
    }
    myJQGrid.fshiClicked(index, "#rowed5", inicializoGride);
    updateTotalet();
}
function HiqArtikullSetNgaPerberesit(idRreshti, Seti, idArtikulli) {
    var perberes = Utils.findInArray(colPerberesList, function (p) {
        return p.IdRreshti == idRreshti && p.KodSeti == Seti && p.IdArtikulli == idArtikulli;
    });
    if (!perberes)
        return;

    var kaPerberesTeKetijSetiNeGride = false;

    var grida = $(pageState.gridaSelector);
    for (i = 0; i < colPerberesList.length; i++) {
        var current = colPerberesList[i];
        if (current.Id != perberes.Id || current.IdRreshti == perberes.IdRreshti)
            continue;

        if (grida.getTeDhenaRreshti(current.IdRreshti)[0].txtIdKodi)
        {
            kaPerberesTeKetijSetiNeGride = true;
            break;
        }
            
    }

    var newColPerberesList = new Array();

    if (!kaPerberesTeKetijSetiNeGride) {
        for (j = 0; j < colPerberesList.length; j++) {
            var current = colPerberesList[j];
            if (current.Id != perberes.Id)
                newColPerberesList.push(current);

        }
        colPerberesList = newColPerberesList;
    }
    return;
}
function FshiSeriale(idArtikulli, mag, set) {
    $.ajax({
        pritPergjigje: true,
        showLoading: true,
        url: Utils.getServerApiUrl("SerialeUnike", "HiqSerialetPerArtikullDheMagazine"),
        data: JSON.stringify({ idArtikulli: idArtikulli, mag: pageState.veprimi == "blerje" ? "" : mag, guidString: hfState.Get("guidString"), idNdermarrje: pageState.idNdermarrje, Set: set })
    }).done(function (result) {
        kaSeriale = result;
    });
}

var textNjesiZgjedhur;
/*
Function: myelemComboNjesia

Nderton nje combo qe mbushet me njesite e artikullit te zgjedhur. Therret funksionin <ktheNjesiArtikulli>.
*/
function myelemComboNjesiaSup(value, options, idRreshti) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    if (idRreshti === undefined)
        idRreshti = idRow;
    var lloji = grida.getTekstQelize('txtKategoria', idRreshti);
    var idArt = grida.getTekstQelize('txtIdKodi', idRreshti);

    var arrayOptions;
    if (lloji === "Artikull" && HfArt.Contains(idArt)) {
        var artikulli = HfArt.Get(idArt);
        arrayOptions = grida.formoArrayOptinosNjesia(artikulli);
        if (value === '')
            value = arrayOptions[0].text;
        if (idRreshti == idRow)
            return myJQGrid.myElemCombo(value, 'txtNjesia', idRreshti, changeNjesia, arrayOptions, koloneDisabled() || arrayReadOnlyKolonaGrides[8]);
        else {
            grida.jqGrid('setCell', idRreshti, 'txtNjesia', value, '', '', disabled);
            return arrayOptions[0];
        }
    }
    var objNjesi = new Object();
    objNjesi.value = "";
    objNjesi.text = "";
    arrayOptions = new Array(1);
    arrayOptions[0] = objNjesi;
    var kodi = grida.getTekstQelize('txtKodi', idRreshti);
    if (lloji === "Artikull" && kodi !== "")
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "ktheNjesiArtikulliComboMeKodRow"),
            data: JSON.stringify({ kodi: kodi, index: idRreshti, textNjesiZgjedhur: value, idNdermarrje: pageState.idNdermarrje })
        }).done(SucceededCallbackComboNjesiArtikulli);

    return myJQGrid.myElemCombo(value, 'txtNjesia', idRreshti, changeNjesia, arrayOptions, true);
}

/*
Function: myelemComboMagazina

Nderton nje combobox per te zgjedhur magazinen.
*/
function myelemComboMagazina(value, options, index) {
    var grida = $("#rowed5");
    if (index == undefined)
        index = grida.getLastSel2();
    var disabled = koloneDisabled() || (arrayReadOnlyKolonaGrides[7] == 'True') || !lejomod;
    return myJQGrid.myElemKodi(value, options, disabled, index, arrayIdKolonaGrides[7], ButtonClickMagazinaTrupi, keyPressMagazina, changeFuncMag, lostFocusKoloneFundit);

}


function myelemComboMagazina2(value, options, index) {
    var grida = $("#rowed5");
    if (index == undefined)
        index = grida.getLastSel2();
    var disabled = koloneDisabled() || (arrayReadOnlyKolonaGrides[12] == 'True' || !lejomod || hfState.Get("kushtTransferim") == "Jo");
    return myJQGrid.myElemKodi(value, options, disabled, index, arrayIdKolonaGrides[12], ButtonClickMagazinaDestTrupi, keyPressMagazina2, changeFuncMag, lostFocusKoloneFundit);
}

function changeMag() {
    Gjendjemax = 0;
    Gjendjemin = 0;
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    ///<summary> metode per ndryshimin e vleres se magazines. kontrollon nese magazina eksiston tek comboja e magazines dhe therret webservicet e infos nqs ka info <summary>
    if ($('#txtMagazina' + idRresht + ' option').length == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKjoMagazineNukEkziston"));
        $('#txtMagazina' + idRresht).val('');
    }
    var idkodi = grida.getTekstQelize('txtIdKodi', idRresht);//heq serialet

    if (hfSeriale.Contains(idkodi + '_' + idRresht)) {
        hfSeriale.Remove(idkodi + '_' + idRresht);
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgSerialetEArtikullitUhoqenSepseNukIPerkasinKesajMagazines"));
        grida.setTekstQelize('txtSerial', idRresht, 'Pa serial');
    }
    if (hfSasiSeriale.Contains(idkodi + '_' + idRresht)) {
        hfSasiSeriale.Remove(idkodi + '_' + idRresht);
    }
    var mag = grida.getTekstQelize('txtMagazina', idRresht);
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "kthePershkrimMagSipasKodit"),
        data: JSON.stringify({ kodi: mag, idNdermarrje: pageState.idNdermarrje })
    }).done(SucceededCallbackChangeMag);
    callWebServiceInfoRow(idRresht);
}

function SucceededCallbackComboNjesiArtikulli(comboListNjesiArt) {
    var grida = $("#rowed5");
    var idRow = grida.getLastSel2();
    var idreshti = comboListNjesiArt[0];
    var textzgjedhur = comboListNjesiArt[1];
    comboListNjesiArt = comboListNjesiArt[2];
    if (idreshti == idRow && grida.getTekstQelize('txtKodi', idRow) != undefined) {
        if (arrayReadOnlyKolonaGrides[8] == 'True' || !lejomod)
            $('#' + arrayIdKolonaGrides[8] + idRow).replaceWith(myJQGrid.myElemCombo(textzgjedhur, arrayIdKolonaGrides[8], idRow, changeNjesia, comboListNjesiArt, true).children()[0]);
        else
            $('#' + arrayIdKolonaGrides[8] + idRow).replaceWith(myJQGrid.myElemCombo(textzgjedhur, arrayIdKolonaGrides[8], idRow, changeNjesia, comboListNjesiArt, false).children()[0]);
    }
    else {
        grida.setTekstQelize('txtNjesia', idRreshti, textzgjedhur);
    }
}

function changeNjesia() {
    var grida = $('#rowed5');
    var idRreshti = grida.getLastSel2();
    if (!kontrolloRow(idRreshti))
        return;
    callWebserviceCmimArtikulliRow(grida.getTekstQelize('txtKodi', idRreshti), dteDtDok.GetText(), grida.getTekstQelize('txtNjesia', idRreshti), idRreshti, grida.getTekstQelize('txtSasia', idRreshti));
    minmax();
}

function minmax() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var detajim = -1;
    if (grida.getTekstQelize('txtDetajimi', idRresht) != "")
        detajim = grida.getTekstQelize('txtDetajimi', idRresht);
    if (gjendjeartminmax == 1) {
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "merrGjendjeArtikulliMag"),
            data: JSON.stringify({ idartikulli: artikulli.IdArtikulli, mag: grida.getTekstQelize('txtMagazina', idRresht), idNdermarrje: pageState.idNdermarrje })
        }).done(SucceededCallbackGjendjeArtikulli);
    }
}

function JoClick(s, e) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    switch (identifikuesPyetje) {
        case "artikulli":
            pyeturDoni = 0;
            resetRreshtKorent(rreshtidyfish);
            break;
        case "magazina":
            pageState.LejoMagazinaNdryshe = false;
            break;
        case 'detajimi1':
        case 'detajimi1Lidhje':
            grida.SetTekst('txtDetajimi', pageState.identifikuesRreshti, '');
            break;
        case 'detajimi2':
        case 'detajimi2Lidhje':
            grida.SetTekst('txtDetajimi2t', pageState.identifikuesRreshti, '');
            break;
        case 'FshiDokMag':
        case 'Rivleresimi':
            break;
        default:
            alert('Pyetje e panjohur');           
            break;
    }
}

var queryString;
function PoClick(s, e) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    switch (identifikuesPyetje) {
        case "artikulli":
            pyeturDoni = 0;
            break;
        case "magazina":           
            menuClick(pageState.parametraMenuClick[0], pageState.parametraMenuClick[1], pageState.parametraMenuClick[2]);
            break;
        case 'detajimi1':
            mesazhList.SetSelectedIndex(-1);
            btnPo.SetVisible(false);
            btnJo.SetVisible(false);
            hlClose.SetVisible(false);
            queryString = 'vjenNga=Shto_RegjistrimMagazine&kodArtikulli=' + grida.getTekstQelize("txtKodi", pageState.identifikuesRreshti) + '&lloji=1&veprimi=' + HfArt.Get(grida.getTekstQelize('txtIdKodi', pageState.identifikuesRreshti)).IdKategoriDetajimi + '&kodDet=' + grida.getTekstQelize('txtDetajimi', pageState.identifikuesRreshti);
            myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgShtoDetajim"), 'LupaDetajimShpejte.aspx?' + queryString, 900, 600);
            break;
        case 'detajimi2':
            mesazhList.SetSelectedIndex(-1);
            btnPo.SetVisible(false);
            btnJo.SetVisible(false);
            hlClose.SetVisible(false);
            queryString = 'vjenNga=Shto_RegjistrimMagazine&kodArtikulli=' + grida.getTekstQelize("txtKodi", pageState.identifikuesRreshti) + '&lloji=2&veprimi=' + HfArt.Get(grida.getTekstQelize('txtIdKodi', pageState.identifikuesRreshti)).IdKategoriDetajimi2 + '&kodDet=' + grida.getTekstQelize('txtDetajimi2t', pageState.identifikuesRreshti);
            myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgShtoDetajim"), 'LupaDetajimShpejte.aspx?' + queryString, 900, 600);
            break;
        case 'FshiDokMag':
            FshiDokumentMagazine();
            break;
        case 'Rivleresimi':
            Rivleresim();
            break;
        case 'detajimi1Lidhje':
            Utils.shfaqLoadingGif();
            Utils.RuajLidhjeDetajim(1, pageState.identifikuesRreshti, grida, pageState.idNdermarrje, pageState.idPerdoruesi);
            break;
        case 'detajimi2Lidhje':
            Utils.shfaqLoadingGif();
            Utils.RuajLidhjeDetajim(2, pageState.identifikuesRreshti, grida, pageState.idNdermarrje, pageState.idPerdoruesi);  
            break;
        default:
            alert(hfState.Get("msgPyetjePanjohur"));
            break;
    }
}

/*
Function: SucceededCallbackGjendjeMinMax

Kontrollon sasine e vendosur per nje artikull. Vlera nuk duhet te jete < se gjendja minimale ose > se gjendja maksimale e artikullit
*/
function SucceededCallbackGjendjeMinMax(result) {
    var grida = $("#rowed5");
    var idRresht = grida.getLastSel2();
    gjendjetot = result[1];
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "merrGjendjeArtikulliMag"),
        data: JSON.stringify({ idartikulli: artikulli.IdArtikulli, mag: grida.getTekstQelize('txtMagazina', idRresht), idNdermarrje: pageState.idNdermarrje })
    }).done(SucceededCallbackGjendjeArtikulli);
}

var Gjendjemin, Gjendjemax, artikulli, gjendjetot, gjendjeMag;
function SucceededCallbackGjendjeArtikulli(colGjendjeArt, art) {
    var magazina = '';
    if (colGjendjeArt != null && colGjendjeArt.length > 0) {
        magazina = colGjendjeArt[0].Magazina;
        Gjendjemin = colGjendjeArt[0].GjendjaMin;
        Gjendjemax = colGjendjeArt[0].GjendjaMax;
    }
    else {
        Gjendjemin = null;
        Gjendjemax = null;
    }
    kontrolloGjendjeMinMaxNeGride((art && art.KodArtikulli) ? art : artikulli, gjendjetot, gjendjeMag, magazina);
}

/*
Function: callWebserviceKodi

Sugjeron listen e artikujve kur shkruajme te kodi.
Shiko funksionin <SucceededCallbackKodi>.
*/
function callWebserviceKodi(vlera) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var kategoria = grida.getTekstQelize('txtKategoria', idRresht);
    if (kategoria == "Artikull") //Artikull
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "ktheACListeArtikujshKodPershkKodbarEShpejt"), data: JSON.stringify({ infixText: vlera, pershk: pershk, grup: '', idNdermarrje: pageState.idNdermarrje, idPerdoruesi: pageState.idPerdoruesi, artikujTeShitshem: (pageState.veprimi == 'shitje' || pageState.veprimi == 'shitjediscount' || pageState.veprimi == 'bazaar') ? true : false, merrVetemAfatgjate: false, merrSipasDetajimit: hfState.Get("NAGDN"), klasa: "" })
        }).done(SucceededCallbackKodi);
}

function callWebserviceKodiSet(vlera) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "ktheACListeArtikujshKodPershkKodbarEShpejtSet"), data: JSON.stringify({ infixText: vlera, pershk: pershk, grup: '', idNdermarrje: pageState.idNdermarrje, idPerdoruesi: pageState.idPerdoruesi, artikujTeShitshem: false })
    }).done(SucceededCallbackKodiSet);
}

/*
Function: mbushArrayMagazinat

Therret funksionin <ktheMagazinatClientSide> per te marre magazinat e ndermarrjes
Shiko funksionin <SucceededCallbackMagazinat>.
*/
function mbushArrayMagazinat() {//po
    var colMag = $('#hfTmpColMag').val();
    if (colMag != '') {
        colMagazina = $.parseJSON(colMag);
        colMagazina2 = $.parseJSON(colMag);
    }
}


function SucceededCallbackMagazinat(result) {//po
    $('#hfTmpColMag').val(JSON.stringify(result));
    colMagazina2 = result;
    mbushMagSipasKushtit = false;
    callWebserviceMagazina();
    var lidhur = ($("input[id$='hfLidhur']").val().toLowerCase() === 'true');
    inicializoGride(lidhur);
    mbushGrideNgaHiddenFieldet(lidhur);
}

/*
Function: SucceededCallbackKodi

Sugjeron listen e artikujve kur shkruajme te kodi.
*/
function SucceededCallbackKodi(result) {
    var idRresht = $('#rowed5').getLastSel2();
    myJQGrid.SucceededCallbackKodi(result, '#txtKodi' + idRresht);
}
function SucceededCallbackKodiSet(result) {
    var idRresht = $('#rowed5').getLastSel2();
    myJQGrid.SucceededCallbackKodi(result, '#txtIdArtikullSet' + idRresht);
}

function callWebserviceDetajimi(vlera, lloji) {//po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var kodArtikulli = grida.getTekstQelize('txtKodi', idRresht);
    if (kodArtikulli == "")
        return;

    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "ktheListeDetajimeshArtikulliNew"), data: JSON.stringify({ infixText: vlera, art: kodArtikulli, lloji: lloji, idNdermarrje: pageState.idNdermarrje, idPerdoruesi: pageState.idPerdoruesi, merrPerberesit: false, date: new Date(), sipasGjendjes: true, detajimi1: grida.getTekstQelize('txtDetajimi', idRresht), mag: grida.getTekstQelize('txtMagazina', idRresht) })
    }).done(function (result) {
        SucceededCallbackDetajimiAutoComplete(result);
    });
}

function SucceededCallbackDetajimiAutoComplete(result) {//po
    var emerEditor = result.lloji == 1 ? '#txtDetajimi' : '#txtDetajimi2t';
    var idRresht = $('#rowed5').getLastSel2();
    myJQGrid.SucceededCallbackKodiDetajimi(result.autocomplete, emerEditor + idRresht, result.kategoriDet, result.infixText);
}

/*
Function: lostFocusKoloneFundit

Percakton veprimin qe kryhet kur heqim fokusin nga kolona e fundit e gride (ruhet rreshti korent dhe shtohet nje rresht i ri bosh i editueshem).
Ketu kolona e fundit eshte "Vlefta" (sepse eshte rasti kur nuk po behet transferim).
*/
function lostFocusKoloneFundit() {
    $("#rowed5").lostFocusKoloneFundit();
    //jQuery("#txtMagazina" + lastsel2).focus();
    //jQuery("#txtMagazina" + lastsel2).blur();
    //jQuery("#txtKodi" + lastsel2).focus();
}

/*
Function: lostFocusKoloneFunditTransferim

Percakton veprimin qe kryhet kur heqim fokusin nga kolona e fundit e gride (ruhet rreshti korent dhe shtohet nje rresht i ri bosh i editueshem).
Ketu kolona e fundit eshte "Magazina Destinacion".
*/
function lostFocusKoloneFunditTransferim() {
    var idRresht = $('#rowed5').getLastSel2();
    var mag1 = $('#txtMagazina' + idRresht + ' option:selected').text();
    //if (cmbKonfigurimi.GetText() == "FDT" || cmbKonfigurimi.GetText() == "FDTK") {
    if (hfState.Get("kushtTransferim") === "Po") {
        var mag2 = $('#txtMagazina2' + idRresht + ' option:selected').text();
        if (mag1 == mag2 && mag1 != "") {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukMundTeBeniTransferimNeTeNjejtenMagazine"));
        }
    }

    else {
        vendosCmimin();
    }
}
var colTrupMag, colNjesAdminis;
function mbushGrideNgaHiddenFieldet(isLidhur) {
    var grida = $("#rowed5");
    var idRresht = grida.getLastSel2();
    if (arrayPershkrimiKolonaGrides.length == 0)
        return;
    if ($('#hfShtimModifikim').val() == "shtim") {
        return;
    }
    if ($('#hfShtimModifikim').val() == "modifikim" || $('#hfShtimModifikim').val()==="shtimraport" || $('#hfShtimModifikim').val() === "rezervim" || $('#hfShtimModifikim').val() === "klonim" || $('#hfShtimModifikim').val() === "konvertim" || $('#hfShtimModifikim').val() === "inventarizim") {
        
           
        colTrupMag = JSON.parse($('#HfColTrupMag').val());
        var colArt = JSON.parse($('#HfColArt').val());
        var colArtSet = JSON.parse($('#HfColArtSet').val());
        var colKodbari = JSON.parse($('#HfColKodbare').val());
        var colDetArt = JSON.parse($('#HfColDetArt').val());
        var colDetArt2 = JSON.parse($('#HfColDetArt2').val());
        colNjesAdminis = JSON.parse($('#HfColNjesAdminis').val());
        var colNjesiArt = JSON.parse($('#HfColNjesiArt').val());
        var colKodbar;
        if ($('#hfShtimModifikim').val() === "inventarizim")
            colKodbar = JSON.parse($('#HfKodbari').val());
        var colNjesAdminisDest;
        if ($('#HfColNjesAdminisDest').val() !== "")
            colNjesAdminisDest = JSON.parse($('#HfColNjesAdminisDest').val());
        var konfAmb = JSON.parse($('#HfKonfAmb').val());
        grida.setLastSel2(1);
        idRresht = 1;
        grida.jqGrid('clearGridData');
        var kategoria, kodi, emertimi, detajimet, detajimet2, magazina, njesia, sasia, cmimi, vlefta, magazina2, idkodi, idtrupirez, idtrupikonv, idtrupikonvush, idtrupikonvud, idtrupi, serial, idkthimi, idtrupishitjegjenerimi, pershkrimmag, shenime, kodbar, artikulliset;
      
        for (var i = 0; i < colArt.length; i++) {
            if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides - 1] == 'True' || isLidhur || !lejomod)
                be = myJQGrid.myValueButtonFshi(true, idRresht, "#rowed5");
            else be = myJQGrid.myValueButtonFshi(false, idRresht, "#rowed5");
            detajimet2 = (colDetArt2[i].KodDetajimArtikulli == null) ? "" : colDetArt2[i].KodDetajimArtikulli;
            magazina = colNjesAdminis[i].Kodi;
            pershkrimmag = colNjesAdminis[i].Pershkrimi;
            if (hfSeriale.Contains(colTrupMag[i].IdArtikulli + '_' + idRresht) && hfSeriale.Get(colTrupMag[i].IdArtikulli + '_' + idRresht) != "[]") {
                serial = 'Me serial';
            }
            else serial = 'Pa serial';
            njesia = colNjesiArt[i].KodNjesia;
            if ($('#HfColNjesAdminisDest').val() !== "" && colNjesAdminisDest.length > i)
                magazina2 = colNjesAdminisDest[i].Kodi;
            else if (btneMagazina2.GetText() != "") magazina2 = btneMagazina2.GetSelectedItem().GetColumnText('Kodi');
                // btneMagazina2.GetText().split(' ')[0];
            else magazina2 = colMagazina2[0].Kodi;
            if ($('#hfShtimModifikim').val() === "rezervim" || $('#hfShtimModifikim').val() === "konvertim") {
                sasia = (colTrupMag[i].Sasimbetur == null) ? "" : colTrupMag[i].Sasimbetur;
                if (magdefault == 0) {
                    magazina = colNjesAdminis[i].Kodi;
                    pershkrimmag = colNjesAdminis[i].Pershkrimi;
                    if (magazina2 == "" || magazina2 == null) magazina2 = magazina;
                }
                else {
                    if (btneMagazina.GetText() != "") {
                        magazina = btneMagazina.GetSelectedItem().GetColumnText('Kodi');
                        pershkrimmag = btneMagazina.GetSelectedItem().GetColumnText('Pershkrimi');
                    }
                        //btneMagazina.GetText().split(' ')[0];
                    else {
                        magazina = colNjesAdminis[i].Kodi;
                        pershkrimmag = colNjesAdminis[i].Pershkrimi;
                    }

                    if (btneMagazina2.GetText() != "")
                        magazina2 = btneMagazina2.GetSelectedItem().GetColumnText('Kodi');
                        //btneMagazina2.GetText().split(' ')[0];
                    else magazina2 = colNjesAdminis[i].Kodi;
                }
            }

            if (colTrupMag[i].IdLlojVeprimi == 1)
                kategoria = "Artikull";
            else
                kategoria = "Makro";
            HfArt.Set(colArt[i].IdArtikulli, colArt[i]);
            kodi = colArt[i].KodArtikulli;
            idkodi = colArt[i].IdArtikulli;
            artikulliset = colArtSet[i].KodArtikulli;
         
           
            
            if ($('#hfShtimModifikim').val() === "inventarizim" && Utils.getUrlVar('llojinv') == 'ash')
                kodbar = colKodbar[i];

            else if ($('#hfShtimModifikim').val() === "konvertim") {
                if (colKodbari.length > 0)
                    kodbar = colKodbari[i].Pershkrimi;

                else kodbar = "";

            }
            else if (colKodbari.length > 0) {
                var myKodbarElem = Utils.ktheElement(colKodbari, "IdTrupiMagazina", colTrupMag[i].IdTrupiMagazina);
                kodbar = myKodbarElem ? myKodbarElem.Pershkrimi : "";
            }
            else kodbar = "";

            if (pershk == 1)
                emertimi = colArt[i].PershkrimArtikulli;
            else
                emertimi = colArt[i].PershkrimiAngArtikulli;
            detajimet = (colDetArt[i].KodDetajimArtikulli == null) ? "" : colDetArt[i].KodDetajimArtikulli;
            detajimet2 = (colDetArt2[i].KodDetajimArtikulli == null) ? "" : colDetArt2[i].KodDetajimArtikulli;
            magazina = colNjesAdminis[i].Kodi;
            pershkrimmag = colNjesAdminis[i].Pershkrimi;
            if (hfSeriale.Contains(colTrupMag[i].IdArtikulli + '_' + idRresht) && hfSeriale.Get(colTrupMag[i].IdArtikulli + '_' + idRresht) != "[]") {
                serial = 'Me serial';
            }
            else serial = 'Pa serial';
            njesia = colNjesiArt[i].KodNjesia;
            if ($('#HfColNjesAdminisDest').val() !== "" && colNjesAdminisDest.length > i)
                magazina2 = colNjesAdminisDest[i].Kodi;
            else if (btneMagazina2.GetText() != "") magazina2 = btneMagazina2.GetSelectedItem().GetColumnText('Kodi');
                // btneMagazina2.GetText().split(' ')[0];
            else magazina2 = colMagazina2[0].Kodi;
            if ($('#hfShtimModifikim').val() === "rezervim" || $('#hfShtimModifikim').val() === "konvertim") {
                sasia = (colTrupMag[i].Sasimbetur == null) ? "" : colTrupMag[i].Sasimbetur;
                if (magdefault == 0) {
                    magazina = colNjesAdminis[i].Kodi;
                    pershkrimmag = colNjesAdminis[i].Pershkrimi;
                    if (magazina2 == "" || magazina2 == null) magazina2 = magazina;
                }
                else {
                    if (btneMagazina.GetText() != "") {
                        magazina = btneMagazina.GetSelectedItem().GetColumnText('Kodi');
                        pershkrimmag = btneMagazina.GetSelectedItem().GetColumnText('Pershkrimi');
                    }
                        //btneMagazina.GetText().split(' ')[0];
                    else {
                        magazina = colNjesAdminis[i].Kodi;
                        pershkrimmag = colNjesAdminis[i].Pershkrimi;
                    }

                    if (btneMagazina2.GetText() != "")
                        magazina2 = btneMagazina2.GetSelectedItem().GetColumnText('Kodi');
                        //btneMagazina2.GetText().split(' ')[0];
                    else magazina2 = colNjesAdminis[i].Kodi;
                }
            }
            else
                sasia = colTrupMag[i].Sasia;
            cmimi = colTrupMag[i].Cmimi;
            if (sasia == 0)
                vlefta = colTrupMag[i].Vlefta;
            else vlefta = parseFloat(sasia * cmimi);
            idtrupirez = (colTrupMag[i].IdTrupiRezervimi == null) ? "" : colTrupMag[i].IdTrupiRezervimi;
            idtrupikonv = (colTrupMag[i].IdTrupiKonvertimFSH == null) ? "" : colTrupMag[i].IdTrupiKonvertimFSH;
            idtrupikonvush = (colTrupMag[i].IdTrupiKonvertimUSH == null) ? "" : colTrupMag[i].IdTrupiKonvertimUSH;
            idtrupikonvud = (colTrupMag[i].IdTrupiKonvertimUD == null) ? "" : colTrupMag[i].IdTrupiKonvertimUD;
            idkthimi = (colTrupMag[i].IdKthimi == null) ? "" : colTrupMag[i].IdKthimi;
            idtrupishitjegjenerimi = (colTrupMag[i].IdTrupiShitjeGjenerimi == null) ? "" : colTrupMag[i].IdTrupiShitjeGjenerimi;
            shenime = (colTrupMag[i].Shenime == null) ? "" : colTrupMag[i].Shenime;
            idtrupi = colTrupMag[i].IdTrupiMagazina;

            var serialeUnikeButton = myJQGrid.myElemButtonSerialetUnike(PercaktoButtonSerialeUnikeEnabled(artikulliset), idRresht);

            var datarow = {
                txtNrRendor: i + 1, txtKategoria: kategoria, txtKodi: kodi, txtKodbari: kodbar, txtEmertimi: emertimi, txtDetajimi: detajimet, txtDetajimi2t: detajimet2, txtMagazina: magazina,
                txtNjesia: njesia, txtMagazina2: magazina2, txtIdKodi: idkodi, txtIdTrupiRezervimi: idtrupirez, txtIdTrupiKonvertimi: idtrupikonv, txtIdTrupiKonvertimiUSH: idtrupikonvush, txtIdTrupiKonvertimiUD: idtrupikonvud, txtIdTrupi: idtrupi, txtSerial: serial, txtpershkrimmag: pershkrimmag, txtIdKthimi: idkthimi, txtIdTrupiShitjeGjenerimi: idtrupishitjegjenerimi,
                txtShenime: shenime, txtIdArtikullSet: artikulliset, txtSerialUnik: serialeUnikeButton, txtFshi: be
            };
            var su = grida.jqGrid('addRowData', parseInt(idRresht), datarow);
            
            memoryArt.Set(colArt[i]);
            grida.setTekstQelize('txtSasia', idRresht, sasia);
            grida.setTekstQelize('txtCmimi', idRresht, cmimi);
            grida.setTekstQelize('txtVlefta', idRresht, vlefta);
            grida.setTekstQelize('txtPershkrimmag', idRresht, pershkrimmag);

            idRresht = idRresht + 1;
            grida.setLastSel2(idRresht);
        }
        updateTotalet();
    }

    MerrTeDhenaPerArtikujSet();
}
function PercaktoButtonSerialeUnikeEnabled(artikulliset) {
    return Utils.IsNullOrEmpty(artikulliset);
}

var arrKategoria = new Array();
var arrKodi = new Array();
var arrEmertimi = new Array();
var arrDetajimet = new Array();
var arrMagazina = new Array();
var arrNjesia = new Array();
var arrSasia = new Array();
var arrCmimi = new Array();
var arrVlefta = new Array();
var arrMagazina2 = new Array();
var counter1 = 0;
var counter2 = 0;
var counter3 = 0;
var counter4 = 0;
var counter5 = 0;
var counter6 = 0;
var counter7 = 0;
var counter8 = 0;
var counter9 = 0;
var counter10 = 0;
var indeksi = -1;
var indexCounter;
var identifikuesPerPopupDokumentat;
var identikuesPerPopupKlientFurnitori;
var identikuesPerPopupArtikulli;
var identifikuesPerPopupDetajime;
var identifikuesPerPopupMakro;
var identifikuesPerPopupMagazina;
var NivelCmimi; // variabel qe mban nivelin e cmimit qe i eshte caktuar klientit qe kemi zgjedhur

function changeName() {
    myFaqeCelje.shtoHandlerSession();
    try {
        if (Utils.getUrlVar('id') == undefined)
            window.parent.callWebServiceKtheInfoLart('Shto_RegjistrimMagazine.aspx?lloj=' + Utils.getUrlVar('lloj'), 0);
        else
            window.parent.callWebServiceKtheInfoLart('Shto_RegjistrimMagazine.aspx?lloj=' + Utils.getUrlVar('lloj'), Utils.getUrlVar('id'));
    } catch (e) { }
    myCookies.createCookie('adresa', window.location.href, 1);
}

/*
Function: ButtonClickKerko

Hap lupen e dokumentave.
*/
function ButtonClickKerko(listUrl) {
    var niveli;
    if (cmbLloji.GetText() != "")
        niveli = cmbLloji.GetValue();
    else niveli = 1;
    var magazina = btneMagazina.GetText().split(' ')[0];
    var degeAdmin = '';
    if (cmbDegeAdministrative.GetSelectedItem() != null)
        degeAdmin = cmbDegeAdministrative.GetSelectedItem().GetColumnText('Kodi');
    var queryString = {
        veprimi: 'RegjistrimMagazine',
        lloji: Utils.getUrlVar('lloj'),
        niveli: niveli,
        njesia: magazina,
        degeAdmin: degeAdmin,
        listUrl: listUrl
    };

    popupUniversal.SetHeaderText(hfState.Get("msgZgjidhDokumentin"));
    popupUniversal.SetContentUrl('LupaDokumenta.aspx?' + Utils.KonvertoObjectQueryString(queryString));
    popupUniversal.SetSize(widthLupaKerko, heightLupaKerko);
    popupUniversal.Show();
}

/*
Function: callWebserviceKonfigurimi

Therret funksionin <ktheKonfigurim> per te vendosur konfigurimin per dokumentin.
Shiko funksionin <SucceededCallbackKonfigurimi>.
*/
function callWebserviceKonfigurimi(idKomp, kodKonf, pastroSeriale) {
    try {
        var shtim = $('#hfShtimModifikim').val() != 'modifikim' && $('#hfShtimModifikim').val() != 'klonim';
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Konfigurime", "ktheKonfigAmbjentiMeFormatNumrash"),
            data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, kodKontrolli: '', idObjekti: -1, shtim: shtim, merrFormatKursi: false, merrGjitheKonf: false, idGjuha: pageState.idGjuha, dateDok: dteDtDok.GetDate(), idPerdoruesi: pageState.idPerdoruesi, idNdermarrje: pageState.idNdermarrje })
        }).done(SucceededCallbackKonfig);
        // Ia kalojme false parametrin shtim, sepse ne rastin e magazines duhet marre formati i numrit per monedhen baze pavaresisht klientit dhe nuk duhet te kontrollojme nese ka ndonje vlere default te konfigurimi per klientin.
        if (shtim) {
            $.ajax({
                pritPergjigje: true, url: Utils.getServerApiUrl("Rregjistrime", "ktheGrupimDokumentashNderm"),
                data: JSON.stringify({ kodkonfig: kodKonf, idNdermarrje: pageState.idNdermarrje, idPerdoruesi: pageState.idPerdoruesi })
            }).done(Utils.SucceededCallbackGrupimDokumentash);
        }
        if (pastroSeriale) {
            $.ajax({
                pritPergjigje: false,
                showLoading: true,
                data: JSON.stringify({
                    guidString: hfState.Get("guidString")
                }),
                url: Utils.getServerApiUrl("SerialeUnike", "HiqSerialet")
            });
            kaSeriale = false;
        }
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
    }
}

function getInfoArtStructure(idInfoArt) {
    try {
        $.ajax({ url: Utils.getServerApiUrl("Rregjistrime", "getInfoArtStructure"), data: JSON.stringify({ idkoka: idInfoArt, idNdermarrje: pageState.idNdermarrje }) }).done(SuccededCallbackInfoArtStructure);
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTeDhenave"));
    }
}

function SuccededCallbackInfoArtStructure(result) {
    vendosInfo(lbxZgjedhur, result, true);
}

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}

var infoArt = false;
var idInfoArt = 0;
var kushtet; var colKushte; var colAlterKusht;
var fokusi = 0; var info = false, lejomod = true, vlerakushti = "";
var colGrida;
var formatNumriZgjedhur;
var shtoSasiNeRreshtTeRi = true;

var ruajlocalstorage = false;
function SucceededCallbackKonfig(result) {
    if ($("input[id$='hfShtimModifikim']").val() == "shtim" || $("input[id$='hfShtimModifikim']").val() == "shtimraport")
        pastroFushatKokes();
    //btneMagazina2.SetText('');
    var hf = $("input[id$='hfShtimModifikim']");
    var hfAuto = $("#hfLupaAutomjet");
    var hfLidhur = $("input[id$='hfLidhur']");
    var hfTr = $("#hfLupaTransportues");
    var arrTabela = ['tblFillim', "tblFund"];
    var arrPrind = ["dvFillim", "dvFundi"];
    var colKontrollet = result.colKontroll;    //[0];
    var colAtrTrupi = result.colAtrTrupi;     //[1];
    var grida = $('#rowed5');
    formatNumriZgjedhur = result.formatNumri;     // [7];
    var vlerat = myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, undefined, hf, '', arrTabela, undefined, undefined, hfLidhur, arrPrind, undefined, undefined, undefined, hfState.Get("vleraDefaultKlonimi"));
    $("#divgride1").show();
    $("#dvFillim").show();
    cmbKonfigurimi.ShowDropDown();
    cmbKonfigurimi.HideDropDown();
    $("#dvFundi").show();

    if ($("input[id$='hfShtimModifikim']").val() == "inventarizim") {
        cmbLloji.SetEnabled(false);
        cmbKonfigurimi.SetEnabled(false);
    }
    vendosDateDefault();
    NivelCmimi = 0;
    callWebserviceKF(0);
    colGrida = result.colGrida;  //[2];
    colKushte = result.colKushte;   //[3];
    colAlterKusht = result.colAlterKusht;   //[4];
    var kodniveli = result.kodniveli;    //[5];
    $('#HfGridCol').val(JSON.stringify(colGrida));
    var hfrivleresim = $('#hfKontrollRivleresim');
    LostFocusSerial(); TextChangeFile();
    $('#hfKontrolletNrAutom').val(JSON.stringify(colKontrollet));
    $('#hfAtributeNrAutom').val(JSON.stringify(colAtrTrupi));
    if ($("input[id$='hfShtimModifikim']").val() == "shtim" || $("input[id$='hfShtimModifikim']").val() == "shtimraport" || $("input[id$='hfShtimModifikim']").val() == "klonim" || $("input[id$='hfShtimModifikim']").val() == "inventarizim" || $("input[id$='hfShtimModifikim']").val() == "rezervim" || $("input[id$='hfShtimModifikim']").val() == "konvertim") {
        hfNrAuto.Clear();
        hfNrAutoShitje.Clear();
        vendosNrAutomatik(colAtrTrupi, colKontrollet);
    }
    if ($("input[id$='hfShtimModifikim']").val() != "shtim") {
        cmbLloji.SetEnabled(false);
        cmbKonfigurimi.SetEnabled(false);
    }
    vendosVleraDefaultTeKoka(colKontrollet, colAtrTrupi, result.objekteDefault);

    pershk = 1;
    konfirmimArt = 2;
    njesiDef = 1; shtoSasiNeRreshtTeRi = true;
    cmimzero = 0;
    regjistrimKF = 1;
    lajmerimmagazine = 0;
  
    
    //  btneKlientFurnitori.SetEnabled(false);
    gjendjeartminmax = 2;
    kodkodbar = 1;
    ruajlocalstorage = false;
    var hidField1 = $("#hfKontabilizimi")[0];
    hidField1.value = 0;
    var modinfo = 0;
    for (j = 0; j < colKushte.length; j++) {
        if (colKushte[j].Kodi == 'RPKF') {
            if (colAlterKusht[j].Alternativa == 'Pa klient/furnitor') {
                regjistrimKF = 1;
                btneKlientFurnitori.SetEnabled(false);
            }
            else
                if (colAlterKusht[j].Alternativa == 'Nje klient/furnitor') {
                    regjistrimKF = 2;
                }
                else {
                    regjistrimKF = 3;
                }
        }
        if (colKushte[j].Kodi == 'ZADHG') {
            if (colAlterKusht[j].Alternativa == 'Shto ne rresht te ri')
                shtoSasiNeRreshtTeRi = true;
            else shtoSasiNeRreshtTeRi = false;
        }
        if (colKushte[j].Kodi == 'NKAKNKA') {
            if (colAlterKusht[j].Alternativa == 'Po')
                hfState.Set("kodbarikurkodi", true);
            else
                hfState.Set("kodbarikurkodi", false);
        }


        if (colKushte[j].Kodi == 'LDKDMN') {
            if (colAlterKusht[j].Alternativa == 'Po')
                lajmerimmagazine = 1;
            else
                lajmerimmagazine = 0;
        }
        if (colKushte[j].Kodi == 'DMT') {
            if (colAlterKusht[j].Alternativa == 'Po')
                pageState.lajmerimmagazineDes = 1;
            else
                pageState.lajmerimmagazineDes = 0;
        }
        if (colKushte[j].Kodi == 'MKS') {
            pageState.kushte.kushtSerialSinkron = colAlterKusht[j].Alternativa == "Sinkrone";
        }
        if (colKushte[j].Kodi == 'VCVS') {
            pageState.kushte.VCVS = colAlterKusht[j].Alternativa == "Po";
        }
        if (colKushte[j].Kodi == 'LPNJAG') {
            if (colAlterKusht[j].Alternativa == 'Po')
                konfirmimArt = 1;
            else
                konfirmimArt = 2;
        }
        if (colKushte[j].Kodi == 'P') {
            if (colAlterKusht[j].Alternativa == 'Pershkrimi 1')
                pershk = 1;
            else
                pershk = 2;
        }
        if (colKushte[j].Kodi == 'DMT') {
            if (colAlterKusht[j].Alternativa == 'Po')
                hfState.Set("kushtTransferim", "Po");
            else
                hfState.Set("kushtTransferim", "Jo");
        }
        if (colKushte[j].Kodi == 'DOKMEKONF') {
            if (colAlterKusht[j].Alternativa == 'Po')
                hfState.Set("kushtKonfirmim", "Po");
            else
                hfState.Set("kushtKonfirmim", "Jo");
        }
        if (colKontrollet[j].KodKontrolli == "btneAutomjeti") {
            hfAuto.val(colAtrTrupi[j].IdKonfigAmbjenteLupa.toString());
            continue;
        }
        if (colKontrollet[i].KodKontrolli == "btnTransportuesi") {
            hfTr.val(colAtrTrupi[i].IdKonfigAmbjenteLupa.toString());
            continue;
        }
        if (colKushte[j].Kodi == 'KR') {
            if (colAlterKusht[j].Alternativa == 'Po')
                hfrivleresim.val(true);
            else
                hfrivleresim.val(false);
        }
        if (hfState.Get("kushtTransferim") === "Jo") {
            btnTransportuesi.SetVisible(true);
            lbltransportuesi.SetVisible(true);
        }
        if (colKushte[j].Kodi == 'ZIA') {
            if (colKushte[j].Vlera != "0") {
                infoArt = true;
                idInfoArt = colKushte[j].Vlera;
                hapMbyllInfo($('#hfHapurMbyllur').val() == 'True');
                $.ajax({
                    url: Utils.getServerApiUrl("Rregjistrime", "ruajNeSessionTrupInfoArt"),
                    data: JSON.stringify({ idkoka: colKushte[j].Vlera })
                }).done(Succedcallback);

            }
            else {
                $.ajax({
                    url: Utils.getServerApiUrl("Rregjistrime", "ruajNeSessionTrupInfoArt"),
                    data: JSON.stringify({ idkoka: -1 })
                }).done(Succedcallback);
                infoArt = false;
            }
        }

        if (colKushte[j].Kodi == 'NJ') {
            if (colAlterKusht[j].Alternativa == 'Njesia 1')
                njesiDef = 1;
            else
                njesiDef = 2;
        }
        if (colKushte[j].Kodi == 'PMDKonv') {
            if (colAlterKusht[j].Alternativa == 'Jo')
                magdefault = 0;
            else
                magdefault = 1;
        }
        if (colKushte[j].Kodi == 'CZ') {
            if (colAlterKusht[j].Alternativa == 'Lajmerues') {
                cmimzero = 1;
            }
            else if (colAlterKusht[j].Alternativa == 'Bllokues')
                cmimzero = 2;
        }
        if (colKushte[j].Kodi == 'LMD') {
            vlerakushti = colAlterKusht[j].Alternativa;
            if (colAlterKusht[j].Alternativa == 'Po') {
                lejomod = true;
            }
            else lejomod = false;
        }
        if (colKushte[j].Kodi == 'KGJMM') {
            if (colAlterKusht[j].Alternativa == 'Po') {
                gjendjeartminmax = 1;
            }
            else
                gjendjeartminmax = 2;
        }
        if (colKushte[j].Kodi == 'GJK') {
            if (colAlterKusht[j].Alternativa == 'Jo') {
                hidField1.value = 0;
            }
            else
                if (colAlterKusht[j].Alternativa == "Direkt")
                    hidField1.value = 1;
                else
                    hidField1.value = 2;
        }
        if (colKushte[j].Kodi == 'RVF') {
            if (colAlterKusht[j].Alternativa == 'Jo')
                ruajlocalstorage = false;
            else
                ruajlocalstorage = true;
        }
        if (colKushte[j].Kodi == 'IA') {
            if (colAlterKusht[j].Alternativa == 'Kod')
                kodkodbar = 1;
            else
                kodkodbar = 2;
        }
        if (colKushte[j].Kodi == 'F') {
            if (colAlterKusht[j].Alternativa == 'Sasia')
                fokusi = 1;
            else if (colAlterKusht[j].Alternativa == 'Rreshti tjeter') fokusi = 2;
            else fokusi = 0;
            pageState.kushte.F = fokusi;
        }
        if (colKushte[j].Kodi == 'LAPP') {
            if (colAlterKusht[j].Alternativa == 'Po')
                hfState.Set("LAPP", "Po");
            else
                hfState.Set("LAPP", "Jo");
        }

        if (colKushte[j].Kodi == 'PDET1ART') {
            if (colAlterKusht[j].Alternativa == 'Po')
                hfState.Set("plotesoDetajim1", true);
            else
                hfState.Set("plotesoDetajim1", false);
        }

        if (colKushte[j].Kodi == 'PDET2ART') {
            if (colAlterKusht[j].Alternativa == 'PO')
                hfState.Set("plotesoDetajim2", true);
            else
                hfState.Set("plotesoDetajim2", false);
        }
        if (colKushte[j].Kodi == "MNSA") {
            hfState.Set("MNSA", colAlterKusht[j].Alternativa == "Jo");
        }
        if(colKushte[j].Kodi == "LMDET"){
            hfState.Set("LMDET", colAlterKusht[j].Alternativa == "Po");
        }
        if (cmbLloji.GetText() === 'FH' || cmbLloji.GetText() === 'FD') {
            if (colKushte[j].Kodi == 'MAGDESTAUTOR') {
                if (colAlterKusht[j].Alternativa == 'Jo' && colAlterKusht[j].Alternativa !== hfState.Get("merrMagazinatMeAutorizim")) {
                    hfState.Set("merrMagazinatMeAutorizim", "Jo");
                    mbushMagSipasKushtit = true;
                    $.ajax({
                        url: Utils.getServerApiUrl("Rregjistrime", "ktheArrayMagazinatClientSideTeSerializuar"),
                        data: JSON.stringify({ merrMeAutorizim: false, idNdermarrje: pageState.idNdermarrje, idPerdoruesi: pageState.idPerdoruesi })
                    }).done(SucceededCallbackMagazinat);
                }
            }
        }
        if (colKushte[j].Kodi == "NAGDN") {
            hfState.Set("NAGDN", colAlterKusht[j].Alternativa == "Po");
        }
    }
    RenditjeCheck(false);
    grida.jqGrid('GridUnload', "rowed5");
    var isLidhur = (hfLidhur.val().toLowerCase() === 'true');
    formGridColsArray(isLidhur);
    grida.setLastSel2(-1);
    grida = $('#rowed5');

    ruajFormatetNeGride(grida, formatNumriZgjedhur);

    if (!mbushMagSipasKushtit) {
        mbushMagSipasKushtit = false;
        mbushArrayMagazinat();
        callWebserviceMagazina();
        inicializoGride(isLidhur);
        mbushGrideNgaHiddenFieldet(isLidhur);
    }

    var lloji = cmbLloji.GetText();
    if (lloji == 'FH')  //Hyrje
    {
        if (cmbKonfigurimi.GetText() != 'FHNV') {
            btneMagazina2.SetEnabled(false);
        }
    }
    else if (lloji == 'FD') //Dalje
    {
        if ($('#hfShtimModifikim').val() == "konvertim")
            txtTarga.SetEnabled(false);

        if (hfState.Get("kushtTransferim") === "Jo" && cmbKonfigurimi.GetText() != 'FDNV') {
            btneMagazina2.SetEnabled(false);
            if (cmbKonfigurimi.GetText() == 'FDS') {
                btneAutomjeti.SetEnabled(false);
                txtTarga.SetEnabled(false);
            }
        }
        else {
            btneMagazina2.SetVisible(true);
            lblMagazina2.SetVisible(true);
        }
        if (hfState.Get("kushtTransferim") === "Jo") {
            txtNIVFSH.SetVisible(false);
            txtWTNIC.SetVisible(false);
            lblNIVFSH.SetVisible(false);
            lblWTNIC.SetVisible(false);
        }
    }
    lblKonfigurimi.SetVisible(false);
    Utils.konfiguroAccorditionPasKonfigDokumenti(varKonfig.identifikuesPerLocalStorageKey);

    if (ruajlocalstorage) {
        if (localStorage.degeadmmagazina && cmbDegeAdministrative.GetText() == '') {
            cmbDegeAdministrative.SetText(localStorage.degeadmmagazina);
            TextChangedDega();
        }
        if (localStorage.magazinamag && btneMagazina.GetText() == '') {
            btneMagazina.SetText(localStorage.magazinamag);
            TextChangedMagazina();
        }
        if (localStorage.magazinamag2 && btneMagazina2.GetText() == '')
            btneMagazina2.SetText(localStorage.magazinamag2);
        if (localStorage.datedokmag && localStorage.datedokmag !== "" && dteDtDok.GetDate().format('dd/MM/yyyy') === Utils.ktheDateDefault(window.parent.lexoHfPeriudhe()).format('dd/MM/yyyy')) {
            dteDtDok.SetDate(new Date(JSON.parse(localStorage.datedokmag)));
            DateChanged(dteDtDok, undefined);
        }
    }
    previousMag = btneMagazina.GetSelectedItem();
}

$(window).unload(function () {
    localStorage.degeadmmagazina = '';
    localStorage.magazinamag = '';
    localStorage.magazinamag2 = '';
    localStorage.datedokmag = '';
});
function DateChanged(s, e) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if ($('#hfShtimModifikim').val() != 'modifikim') {
        var hfKontrollet = JSON.parse($('#hfKontrolletNrAutom').val());
        var atributet = JSON.parse($('#hfAtributeNrAutom').val());
        vendosNrAutomatik(atributet, hfKontrollet);
    }
    callWebServiceInfoRow(idRresht);
}

var pershk = 1;
var konfirmimArt = 2;
var njesiDef = 1;
var regjistrimKF = 1;
var gjendjeartminmax = 2;
var cmimzero = 0;
var magdefault = 0;
/*
Function: ndryshoKonfigurimin

Therret funksionin <callWebserviceKonfigurimi> per te vendosur nje konfigurim te ri.
*/
function ndryshoKonfigurimin(pastroSeriale) {
    var pershkKonfigAmb = cmbKonfigurimi.GetSelectedItem().GetColumnText("PershkrimKonfigAmbjente");
    if (pershkKonfigAmb != undefined) {
        lblKonfigurimi.SetText(pershkKonfigAmb);
        $('#kokeKonfigurimi').text(hfState.Get("MenuKokeDokumenti") +': ' + pershkKonfigAmb);
    }
    callWebserviceKonfigurimi(510, cmbKonfigurimi.GetText().split(';')[0], pastroSeriale);
}

/*
Function: ButtonClickFurnitori

Hap lupen e klienteve/furnitoreve.
*/
function ButtonClickFurnitori() {
    var klientfurnitor;
    var hfKl = document.getElementById("hfLupaKlientFurnitor");
    var queryStr = hfKl.value;
    for (j = 0; j < colKushte.length; j++)
        if (colKushte[j].Kodi == 'KF')
            klientfurnitor = colAlterKusht[j].Alternativa;
    popupUniversal.SetHeaderText(hfState.Get("msgZgjidhKF"));
    if (klientfurnitor == "Klient")
        popupUniversal.SetContentUrl('LupaKlientFurnitor.aspx?veprimi=1&idKonfigAmbjente=' + queryStr);
    else if (klientfurnitor == "Furnitor")
        popupUniversal.SetContentUrl('LupaKlientFurnitor.aspx?veprimi=2&idKonfigAmbjente=' + queryStr);
    else popupUniversal.SetContentUrl('LupaKlientFurnitor.aspx?idKonfigAmbjente=' + queryStr);
    popupUniversal.SetSize(widthLupaKF, heightLupaKF);
    popupUniversal.Show();
}

/*
Function: ButtonClickLlogaria

Hap lupen e dokumentave.
*/
function ButtonClickLlogaria() {
    var hfLlog = $("#hfLlogaria");
    var queryStr = hfLlog.val(); identikuesPerPopupLlogari = "Magazina";
    myButtonClickLupa.ButtonClickLlogaria('Zgjidh Llogarine', queryStr, 600, 600);
}
/*
Function: SucceededCallbackNiveli

Ndryshon vleren e zgjedhur te konfigurimit sipas nivelit te zgjedhur.
Therret funksionin <ndryshoKonfigurimin>.
*/
function ButtonClickMagazina2() {
    identifikuesPerPopupMagazina = 'RegjistrimMagazine';
    var queryStr;
    if ((cmbKonfigurimi.GetText() != 'FHNV' && cmbKonfigurimi.GetText() != 'FDNV') || identifikuesMagazina == 'Mag1') {
        var hfKl = document.getElementById("hfLupaMagazina");
        queryStr = hfKl.value;
        popupUniversal.SetHeaderText(hfState.Get("msgZgjidhMagazinen"));
        popupUniversal.SetContentUrl('LupaMagazina.aspx?idKonfigAmbjente=' + queryStr + '&kushtMerrMagMeAutorizim=' + hfState.Get("merrMagazinatMeAutorizim"));
    }
    else {
        var hfNjVartese = $("#hfLupaNjesiVartese")[0];
        queryStr = hfNjVartese.value;
        if (queryStr == undefined)
            queryStr = 1;
        popupUniversal.SetHeaderText(hfState.Get("msgZgjidhNjesiVartese"));
        popupUniversal.SetContentUrl('LupaNjesiVartese.aspx?idKonfigAmbjente=' + queryStr);
    }
    popupUniversal.SetSize(widthLupaMagazina, heightLupaMagazina);
    popupUniversal.Show();
}

function ButtonClickMagazina() {
    identifikuesPerPopupMagazina = 'RegjistrimMagazine';
    var queryStr;
    if ((cmbKonfigurimi.GetText() != 'FHNV' && cmbKonfigurimi.GetText() != 'FDNV') || identifikuesMagazina == 'Mag1') {
        var hfKl = document.getElementById("hfLupaMagazina");
        queryStr = hfKl.value;
        popupUniversal.SetHeaderText(hfState.Get("msgZgjidhMagazinen"));
        popupUniversal.SetContentUrl('LupaMagazina.aspx?idKonfigAmbjente=' + queryStr);
    } else {
        var hfNjVartese = $("#hfLupaNjesiVartese")[0];
        queryStr = hfNjVartese.value;
        if (queryStr == undefined)
            queryStr = 1;
        popupUniversal.SetHeaderText(hfState.Get("msgZgjidhNjesiVartese"));
        popupUniversal.SetContentUrl('LupaNjesiVartese.aspx?idKonfigAmbjente=' + queryStr);
    }
    popupUniversal.SetSize(widthLupaMagazina, heightLupaMagazina);
    popupUniversal.Show();
}

function ButtonClickMagazinaTrupi() {
    identifikuesMagazina = "Mag1";
    hapLupeMagazinaTrupi();
}

function ButtonClickMagazinaDestTrupi() {
    identifikuesMagazina = "Mag2";
    hapLupeMagazinaTrupi();
}

function hapLupeMagazinaTrupi() {
    identifikuesPerPopupMagazina = 'RegjistrimMagazine_Trupi';
    var idRreshti = $('#rowed5').getLastSel2();
    var aqt = kthellojArt(idRreshti) == 1 ? true : false;
    var hfKl = document.getElementById("hfLupaMagazina");
    var queryStr = hfKl.value;
    popupUniversal.SetHeaderText(hfState.Get("msgZgjidhMagazinen"));
    popupUniversal.SetContentUrl('LupaMagazina.aspx?idKonfigAmbjente=' + queryStr + '&aqt=' + aqt);
    popupUniversal.SetSize(widthLupaMagazina, heightLupaMagazina);
    popupUniversal.Show();
}

function keyPressMagazina(event, idRreshti) {
    var idRow = $('#rowed5').getLastSel2();
    if (typeof idRreshti == "undefined")
        idRreshti = idRow;
    var vlera = event.target.value;
    if (vlera == "" && idRreshti == idRow)
        $('#' + arrayIdKolonaGrides[7] + idRreshti).autocomplete("close");
    else
        callWebserviceListeMagazina(vlera, idRreshti);
}

function keyPressMagazina2(event, idRreshti) {
    var idRow = $('#rowed5').getLastSel2();
    if (typeof idRreshti == "undefined")
        idRreshti = idRow;
    var vlera = event.target.value;
    if (vlera == "" && idRreshti == idRow)
        $('#' + arrayIdKolonaGrides[12] + idRreshti).autocomplete("close");
    else
        callWebserviceListeMagazina2(vlera, idRreshti);
}

function vendosMagazinen(result, idFushaMag, index) {
    if (result.magazina == null || Utils.IsNullOrEmpty(result.magazina.Kodi))
        return;
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    gjendjeMag = result.gjendjeMag;
    if (index != undefined && idRow != index)
        idRow = index;
    var llojartikulli = kthellojArt(idRow);
    var iPerketGrupit = (llojartikulli == -1 || (llojartikulli == 0 && (result.magazina.IdLlojMagazine == 1 || result.magazina.IdLlojMagazine == 3)) ||
        (llojartikulli == 1 && (result.magazina.IdLlojMagazine == 2 || result.magazina.IdLlojMagazine == 3)));
    if (iPerketGrupit) {
        grida.setTekstQelize(idFushaMag, idRow, result.magazina.Kodi);
        if (idFushaMag == 'txtMagazina')
            grida.setTekstQelize('txtPershkrimmag', idRow, result.magazina.Pershkrimi);
        var artikulli = memoryArt.Get(grida.getTekstQelize('txtIdKodi', idRow));
        if (artikulli != null) {
            artikulli[idFushaMag] = result.magazina;
            memoryArt.Set(artikulli);
            if (gjendjeartminmax == 1) {
                $.ajax({
                    url: Utils.getServerApiUrl("Rregjistrime", "merrGjendjeArtikulliMag"),
                    data: JSON.stringify({ idartikulli: artikulli.IdArtikulli, mag: grida.getTekstQelize('txtMagazina', idRow), idNdermarrje: pageState.idNdermarrje })
                }).done(SucceededCallbackGjendjeArtikulli);
            }
        }
    }
    else {
        myMesazh.ShtoMesazhGabimi("Magazina " + result.magazina.Kodi + " nuk i perket llojit per artikullin " + grida.getTekstQelize('txtKodi', idRow));
        grida.setTekstQelize(idFushaMag, idRow, null);
        if (idFushaMag == 'txtMagazina')
            grida.setTekstQelize('txtPershkrimmag', idRow, null);
        grida.selektoRreshtin(idRow, true);
        grida.setFocus(idFushaMag, idRow);
    }
}

function SucceededCallbackMag(result, idFushaMag, index) {
    vendosMagazinen(result, idFushaMag, index);
}

function selectFuncMag(event, ui, emerfushe, idMag, kodiMag, idFushaMag) {
    
    var txtMagKodi = idFushaMag ? idFushaMag : idMag;
    if (txtMagKodi == "txtMagazina" && kaPyetjeHapurMagazina) return;
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var idArt = grida.getTekstQelize("txtIdKodi", idRresht);
    var mag = kodiMag ? kodiMag : ui.item.label;
    var art = memoryArt.Get(idArt);
    var previousMagTrupi = grida.merrTeDhenaPerQelizen(txtMagKodi, idRresht, "IshMagazina");

    if (txtMagKodi != "txtMagazina" || mag == previousMagTrupi || Utils.getUrlVar('lloj') == "hyrje" || !kaSeriale || art == undefined || (art != undefined && art.IdFormatSeriali == 0 && art.Klasa != 4)) {
        selectFuncMagVendosMagazinen(event, ui, emerfushe, idMag, kodiMag, idFushaMag, idArt, dteDtDok.GetDate());
        return;
    }

    kaPyetjeHapurMagazina = true;
    myMesazh.ShtoMesazh({
        type: "confirm",
        UseCancelButton: true,
        text: "Serialet per artikullin do fshihen. Jeni te sigurt qe doni te vazhdoni?",
        modal: true,
        layout: "center",
        idGjuha: pageState.idGjuha,
        okClick: function (noty) {
            kaPyetjeHapurMagazina = false;
            FshiSeriale(idArt, previousMagTrupi, Utils.IsNullOrEmpty(grida.getTekstQelize("txtIdArtikullSet", idRresht)));
            selectFuncMagVendosMagazinen(event, ui, emerfushe, idMag, kodiMag, idFushaMag, idArt, dteDtDok.GetDate());
        },
        cancelClick: function (noty) {
            kaPyetjeHapurMagazina = false;
            grida.setTekstQelize("txtMagazina", idRresht, previousMagTrupi);
        }
    });
}

function selectFuncMagVendosMagazinen(event, ui, emerfushe, idMag, kodiMag, idFushaMag, idja, datedok) {
    var idRresht = $('#rowed5').getLastSel2();
    if (idMag && kodiMag && $(emerfushe).val() !== undefined && idFushaMag != undefined) {
        $(emerfushe).val(kodiMag);
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "KtheVleraMagazinaMeID"),
            idFushaMag: idFushaMag,
            index: idRresht,
            data: JSON.stringify({ id: idMag, index: idRresht, idPerdoruesi: pageState.idPerdoruesi, idNdermarrje: pageState.idNdermarrje, idja: idja, datedok: datedok})
        }).done(function (result) { SucceededCallbackMag(result, this.idFushaMag, this.index); });
        return false;
    }
    if (ui.item != null) {
        $(emerfushe).val(ui.item.label);
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "KtheVleraMagazinaMeID"),
            idFushaMag: idMag,
            index: idRresht,
            data: JSON.stringify({ id: ui.item.value, index: idRresht, idPerdoruesi: pageState.idPerdoruesi, idNdermarrje: pageState.idNdermarrje, idja: idja, datedok: datedok})
        }).done(function (result) { SucceededCallbackMag(result, this.idFushaMag, this.index); });
        return false;
    }
}

function changeFuncMag(event, ui, emerKodi, index) {//po
    if (emerKodi == "txtMagazina" && kaPyetjeHapurMagazina) return;
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var idkodi = grida.getTekstQelize("txtIdKodi", index);
    var magazina = grida.getTekstQelize(emerKodi, index);
    var art = memoryArt.Get(idkodi);
    var magazinaTrupit = art == null ? magazina : art[emerKodi];
    if (!(index == idRresht || magazinaTrupit.Kodi != magazina))
        return;

    var idNdermarrje = hfState.Get('idNdermarrje');
    if (!(ui == null || ui.item == null || magazinaTrupit.Kodi != magazina))
        return;

    var previousMagTrupi = grida.merrTeDhenaPerQelizen("txtMagazina", idRresht, "IshMagazina");

    if (emerKodi != "txtMagazina" || magazina == previousMagTrupi || Utils.getUrlVar('lloj') == "hyrje" || !kaSeriale || art == undefined || (art != undefined && art.IdFormatSeriali == 0 && art.Klasa != 4)) {
        changeFuncMagVendosMagazine(magazina, idNdermarrje, emerKodi, index, idkodi, dteDtDok.GetDate());
        return;
    }

    kaPyetjeHapurMagazina = true;
    myMesazh.ShtoMesazh({
        type: "confirm",
        UseCancelButton: true,
        text: hfState.Get("msgSerialet1ArtDoTeFshihen1"),
        modal: true,
        layout: "center",
        idGjuha: pageState.idGjuha,
        okClick: function (noty) {
            kaPyetjeHapurMagazina = false;
            FshiSeriale(idkodi, previousMagTrupi,Utils.IsNullOrEmpty(grida.getTekstQelize("txtIdArtikullSet", index)));
            changeFuncMagVendosMagazine(magazina, idNdermarrje, emerKodi, index, idkodi, dteDtDok.GetDate());
        },
        cancelClick: function (noty) {
            kaPyetjeHapurMagazina = false;
            grida.setTekstQelize(emerKodi, idRresht, previousMagTrupi);
        }
    });
}

function changeFuncMagVendosMagazine(magazina, idNdermarrje, emerKodi, index, idja, datedok) {
    if (!Utils.IsNullOrEmpty(magazina)) {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "KtheVleraMagazinaMeKod"),
            idFushaMag: emerKodi,
            index: index,
            data: JSON.stringify({
                kodi: magazina, idNdermarrje: idNdermarrje, emerKodi: emerKodi, idja: idja, datedok: datedok
            })
        }).done(function (result) { SucceededCallbackMag(result, this.idFushaMag, this.index); });

        return;
    }
    vendosMagazinen({ idRreshti: index, Kodi: null }, emerKodi);
}

function callWebserviceListeMagazina(vlera, idRreshti) {//po
    var llojartikulli = kthellojArt(idRreshti);
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "KtheListMagazinat"),
        data: JSON.stringify({ prefixText: vlera, idNdermarrje: pageState.idNdermarrje, idPerdoruesi: pageState.idPerdoruesi, meAutorizim: true, llojArt: llojartikulli })
    }).done(function (result) {
        myJQGrid.SucceededCallbackKodi(result, '#txtMagazina' + idRreshti);
    });
}

function callWebserviceListeMagazina2(vlera, idRreshti) {//po
    var llojartikulli = kthellojArt(idRreshti);
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "KtheListMagazinat"),
        data: JSON.stringify({ prefixText: vlera, idNdermarrje: pageState.idNdermarrje, idPerdoruesi: pageState.idPerdoruesi, meAutorizim: true, llojArt: llojartikulli })
    }).done(function (result) {
        myJQGrid.SucceededCallbackKodi(result, '#txtMagazina2' + idRreshti);
    });
}

function kthellojArt(idRreshti) {
    var grida = $('#rowed5');
    var lloji = grida.getTekstQelize('txtKategoria', idRreshti);
    var idArt = grida.getTekstQelize('txtIdKodi', idRreshti);
    var llojartikulli = -1;
    if (lloji === "Artikull" && HfArt.Contains(idArt)) {
        var artikulli = HfArt.Get(idArt);
        llojartikulli = artikulli.LlojiArt == false ? 0 : 1;
    }
    return llojartikulli;
}

function SucceededCallbackAutomjetILidhur(result) {
    if (result != null && result.KodKlientFurnitor != null) {
        btneAutomjeti.SetText('');
        btneAutomjeti.SetSelectedIndex(-1);
        btneAutomjeti.SetValue(null);
        txtTarga.SetText('');

    }
}

function SucceededCallbackIDAutomjet(result) {
    if (result != null) {
        var idAuto = result;
        $.ajax({ pritPergjigje: true, url: Utils.getServerApiUrl("Rregjistrime", "ktheKlientAutomjeti"), data: JSON.stringify({ idAutomjeti: idAuto }) }).done(SucceededCallbackAutomjetILidhur);
    }
}

/*
    Function: TextChangedKlientFurnitori

    Therret funksionin <callWebserviceKF> per te marre vlerat e klientit/furnitorit te zgjedhur
    */
function TextChangedKlientFurnitori() {
    if (btneKlientFurnitori.GetText() != "" && regjistrimKF != 3)
        callWebserviceKF(btneKlientFurnitori.GetValue());
    if (btneAutomjeti.GetValue() != null)
        $.ajax({ pritPergjigje: true, url: Utils.getServerApiUrl("Rregjistrime", "ktheIDAutomjeti"), data: JSON.stringify({ Automjeti: btneAutomjeti.GetText(), idNdermarrje: pageState.idNdermarrje }) }).done(SucceededCallbackIDAutomjet);
}

//Therret funksionin <callWebserviceKF> per te marre vlerat e klientit/furnitorit te zgjedhur
//Nese nuk eshte zgjedhur nonje klient/furnitor ekzistues fshin textin dhe venod fokusin te kontroli
function KlientFurnitoriChanged() {
    txtTarga.SetText('');
    if (isNaN(btneKlientFurnitori.GetValue())) {
        btneKlientFurnitori.SetText('');
        btneKlientFurnitori.Focus();
        return;
    }
    if (regjistrimKF != 3) {
        var kfSelected = btneKlientFurnitori.GetSelectedItem();
        if (kfSelected !== null)
            callWebserviceKF(kfSelected.GetColumnText('KodKlientFurnitor'));
    }
    if (btneAutomjeti.GetValue() != null)
        $.ajax({ pritPergjigje: true, url: Utils.getServerApiUrl("Rregjistrime", "ktheIDAutomjeti"), data: JSON.stringify({ Automjeti: btneAutomjeti.GetText(), idNdermarrje: pageState.idNdermarrje }) }).done(SucceededCallbackIDAutomjet);
}

/*
Function: callWebserviceKF

Therret funksionin <ktheKlientFurnitor> per te marre vlerat e klientit/furnitorit te zgjedhur.
Shiko funksionin <SucceededCallbackKF>.
*/
function callWebserviceKF(name) {
    try {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheKlientFurnitor"),
            data: JSON.stringify({ emri: name, data: dteDtDok.GetDate() })
        }).done(SucceededCallbackKF);
    } catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
    }
}

function SucceededCallbackFormatNumri(formatNumri) {
    var grida = $('#rowed5');
    ndryshoKonfigFormatNumri(grida, formatNumri);
    vendosKonfigFormatNumri();
}

function changeFunc6(event, ui, emerKodi, index) {//po
}

function selectFunc6(event, ui, emerfushe) { //po per cmimet
}
/*
Function: SucceededCallbackKF

Vendos nivelin e cmimit qe i eshte caktuar klientit/furnitorit.
*/
function SucceededCallbackKF(result) {
    var vlerat = '';
    vlerat = result.split(';');
    NivelCmimi = vlerat[10];
}

function MagazinaChanged() {
    var grida = $(pageState.gridaSelector);
    var idRresht = grida.getLastSel2();
    //vendoset magazina e zgjedhur te koka ne rreshtin qe po editohet
    var item = btneMagazina.GetSelectedItem();
    if (item === null) {
        previousMag = undefined;
        return;
    }
    var kodi = item.GetColumnText('Kodi');
    var previousCode = previousMag == undefined ? "" : previousMag.GetColumnText('Kodi');
    if (kodi == previousCode || Utils.getUrlVar('lloj') == "hyrje" || !kaSeriale) {
        TextChangedMagazina();
        previousMag = item;
        return;
    }

    myMesazh.ShtoMesazh({
        type: "confirm",
        UseCancelButton: true,
        text: hfState.Get("msgSerialetDoTeFshihen"),
        modal: true,
        layout: "center",
        idGjuha: pageState.idGjuha,
        okClick: function (noty) {
            TextChangedMagazina();
            previousMag = item;
        },
        cancelClick: function (noty) {
            if (previousMag != undefined)
                Utils.SelectComboItem(btneMagazina, previousMag.value, previousMag.texts[0], previousMag.texts[1]);
            else
                btneMagazina.SetSelectedItem(undefined);
        }
    });
}

/*
Function: TextChangedMagazina

Vendos ne gride magazinen qe zgjidhet te koka
*/
function TextChangedMagazina(s, e) {
    //vendoset magazina e zgjedhur te koka ne rreshtin qe po editohet
    var grida = $("#rowed5");
    var idRresht = grida.getLastSel2();
    if (btneMagazina.GetSelectedItem() === null)
        return;
    
    var rreshtaTeGrides = grida.jqGrid('getDataIDs');
    var pershkrimmag = btneMagazina.GetSelectedItem().GetColumnText('Pershkrimi');
    magazina1 = btneMagazina.GetSelectedItem().GetColumnText('Kodi');

    txtPershkrimMagazine.SetText(pershkrimmag);
    for (i = 0; i < rreshtaTeGrides.length; i++) {
        var idArt = grida.getTekstQelize('txtIdKodi', rreshtaTeGrides[i]);
        var art = memoryArt.Get(idArt);
        //nese eshte artikull qe ka nevoje per seriale ose eshte artikull set, qe perbehet nga art me seriale
        if (Utils.getUrlVar("lloj") != "hyrje" && art != undefined && (art.IdFormatSeriali != 0 || art.Klasa == 4) && kaSeriale && grida.getTekstQelize("txtMagazina", rreshtaTeGrides[i]) != magazina1)
            FshiSeriale(idArt, grida.getTekstQelize("txtMagazina", rreshtaTeGrides[i]), !Utils.IsNullOrEmpty(grida.getTekstQelize("txtIdArtikullSet", rreshtaTeGrides[i])));

        grida.setTekstQelize('txtPershkrimmag', rreshtaTeGrides[i], pershkrimmag);
        grida.setTekstQelize('txtMagazina', rreshtaTeGrides[i], magazina1);
        grida.vendosTeDhenaPerQelizen('txtMagazina', rreshtaTeGrides[i], "IshMagazina", magazina1);
    }

    if (btneMagazina2.GetText() === "0")
        btneMagazina2.SetText('');

    if (hfState.Get("kushtTransferim") === "Po") {
        magazina2 = btneMagazina2.GetText().split(' ')[0];
        if (magazina1 == magazina2 && magazina1 != "") {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukMundTeBeniTransferimNeTeNjejtenMagazine"));
            btneMagazina.SetText('');
            previousMag = btneMagazina.GetSelectedItem();
            return;
        }
    }
    var magazinagrides = $('#txtMagazina' + idRresht)[0];
    if (magazinagrides != undefined) {
        callWebServiceInfoRow(idRresht);
    }

    if (magazina1 != "" && $('#hfShtimModifikim').val() != 'modifikim' && $('#hfShtimModifikim').val() != 'klonim') {
        try {
            $.ajax({
                url: Utils.getServerApiUrl("Rregjistrime", "ktheDegeMagazineSipasKodit"),
                data: JSON.stringify({ kodi: magazina1, idNdermarrje: pageState.idNdermarrje, idPerdoruesi: pageState.idPerdoruesi})
            }).done(SucceededCallbackDega);
        }
        catch (e) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
        }
    }
}

function SucceededCallbackDega(rezult) {
    if (rezult != 0) {
        cmbDegeAdministrative.SetValue(rezult);
        try {
            $.ajax({ url: Utils.getServerApiUrl("Rregjistrime", "kthePershkrimDegeSipasID"), data: JSON.stringify({ id: rezult }) }).done(SucceededCallbackDegePershkrim);
        }
        catch (e) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTeDhenave"));
        }
    }
}

function SucceededCallbackDegePershkrim(result) {
    if (result != 0)
        txtPershkrimDege.SetText(result);
}

function vendosVleraDefaultTeKoka(colKontrollet, colAtrTrupi, objekteDefault) {
    var shtim = $('#hfShtimModifikim').val() != 'modifikim' && ($('#hfShtimModifikim').val() != 'klonim' || hfState.Get("vleraDefaultKlonimi"));
    for (var i = 0; i < colKontrollet.length; i++) {
        //id e konfigurimit te lupes vendoset ne hidden field
        //vlera e saj do t'i kalohet si query string hapjes se popUp-it
        var objektDefault = objekteDefault[colKontrollet[i].KodKontrolli];
        switch (colKontrollet[i].KodKontrolli) {
            case "btneKlientFurnitori":
                if (shtim && !btneKlientFurnitori.GetSelectedItem() && btneKlientFurnitori.GetText() != "" && objektDefault)
                    Utils.SelectComboItem(btneKlientFurnitori, objektDefault.IdKlientFurnitor, objektDefault.KodKlientFurnitor, objektDefault.EmertimiKF);
                $("#hfLupaKlientFurnitor")[0].value = colAtrTrupi[i].IdKonfigAmbjenteLupa.toString(); //merret id e konfigurimit te lupes per lupen e klient furnitorit ne forme
                break;
            case "btneMagazina":
                if (shtim && !btneMagazina.GetSelectedItem() && btneMagazina.GetText() != "" && objektDefault)
                    Utils.SelectComboItem(btneMagazina, objektDefault.IdNjesiAdministrative, objektDefault.Kodi, objektDefault.Pershkrimi);
                TextChangedMagazina();
                $("#hfLupaMagazina")[0].value = colAtrTrupi[i].IdKonfigAmbjenteLupa.toString(); //merret id e konfigurimit te lupes per lupen e magazines ne forme
                break;
            case "btneMagazina2":
                if (shtim && !btneMagazina2.GetSelectedItem() && btneMagazina2.GetText() != "" && objektDefault)
                    Utils.SelectComboItem(btneMagazina2, objektDefault.IdNjesiAdministrative, objektDefault.Kodi, objektDefault.Pershkrimi);
                //merret id e konfigurimit te lupes per lupen e njesise vartese vetem ne rastet kur konfigurimi eshte fhnv ose fdnv
                if (cmbKonfigurimi.GetText() === 'FHNV' || cmbKonfigurimi.GetText() === 'FDNV')
                    $("#hfLupaNjesiVartese")[0].value = colAtrTrupi[i].IdKonfigAmbjenteLupa.toString();
                break;
            case "cmbLlogariKunderParti":
                if (shtim && !cmbLlogariKunderParti.GetSelectedItem() && cmbLlogariKunderParti.GetText() != "" && colAtrTrupi[i].VlereDefault != "" && colAtrTrupi[i].VlereDefault != "0") {
                    cmbLlogariKunderParti.SetSelectedItem(cmbLlogariKunderParti.FindItemByValue(colAtrTrupi[i].VlereDefault));
                    nrLlogariChange(cmbLlogariKunderParti);
                }
                $("#hfLlogaria").value = colAtrTrupi[i].IdKonfigAmbjenteLupa.toString();
                break;
            case "cmbDegeAdministrative":
                if (shtim && !cmbDegeAdministrative.GetSelectedItem() && cmbDegeAdministrative.GetText() != "" && colAtrTrupi[i].VlereDefault != "" && colAtrTrupi[i].VlereDefault != "0") {
                    cmbDegeAdministrative.SetSelectedItem(cmbDegeAdministrative.FindItemByValue(colAtrTrupi[i].VlereDefault));
                }
                TextChangedDega();
                break;
            case "cmbOperatori":
                if (shtim && !cmbOperatori.GetSelectedItem() && cmbOperatori.GetText() != "" && colAtrTrupi[i].VlereDefault != "" && colAtrTrupi[i].VlereDefault != "0") {
                    cmbOperatori.SetSelectedItem(cmbOperatori.FindItemByValue(colAtrTrupi[i].VlereDefault));
                }
                break;
            default:
                break;
        }
    }
}

function TextChangedDega() {
    var dega = cmbDegeAdministrative.GetSelectedItem();
    if (dega != null) {
        txtPershkrimDege.SetText(dega.GetColumnText('Pershkrimi'));
    }
}


/*
Function: TextChangedMagazina2

Vendos ne gride magazinen destinacion qe zgjidhet te koka
*/
function TextChangedMagazina2() {
    //var reshtiieditueshem = false;
    var grida = $("#rowed5");
    var idRresht = grida.getLastSel2();
    if (btneMagazina.GetSelectedItem() != null)
        magazina1 = btneMagazina.GetSelectedItem().GetColumnText('Kodi');
    //btneMagazina.GetText().split(' ')[0];
    if (btneMagazina2.GetSelectedItem() != null) {
        magazina2 = btneMagazina2.GetSelectedItem().GetColumnText('Kodi');
    }
    //btneMagazina2.GetText().split(' ')[0];
    
    var rreshtaTeGrides = grida.jqGrid('getDataIDs');
    for (i = 0; i < rreshtaTeGrides.length; i++) {
        grida.setTekstQelize('txtMagazina2', rreshtaTeGrides[i], magazina2);
    }
    var magazinagrides = $('#txtMagazina2' + idRresht)[0];
    if (magazinagrides != undefined) {
        callWebServiceInfoRow(idRresht);
    }
    //if (cmbKonfigurimi.GetText() == "FDT" || cmbKonfigurimi.GetText() == "FDTK") {
    if (hfState.Get("kushtTransferim") === "Po") {
        if (magazina1 == magazina2 && magazina1 != "") {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukMundTeBeniTransferimNeTeNjejtenMagazine"));
            btneMagazina2.SetText('');
            return;
        }
    }
    //var magazinagrides = $('#txtMagazina2' + lastsel2)[0];
    //if (magazinagrides != undefined) {
    //    for (var i = 0; i < magazinagrides.length; i++) {
    //        if (magazinagrides[i].text == magazina2)
    //            magazinagrides.selectedIndex = i;
    //    }
    //    reshtiieditueshem = true;
    //}

    //for (i = 0; i < rreshtaTeGrides.length; i++) {
    //    if (!reshtiieditueshem)
    //        grida.jqGrid('setCell', rreshtaTeGrides[i], 'txtMagazina2', magazina2, 'clientArray', '');
    //    else if (rreshtaTeGrides[i] != lastsel2) {
    //        grida.jqGrid('setCell', rreshtaTeGrides[i], 'txtMagazina2', magazina2, 'clientArray', '');
    //    }
    //}
}

/*
Function: TextChangedLloji

Ben ndryshime ne gride ne varesi te llojit te veprimit te zgjedhur (hyrje, Dalje apo Transferim)
*/


function TextChangedLloji(pastroSeriale) {
    var grida = jQuery("#rowed5");
    var lloji = cmbLloji.GetText();
    if (lloji == 'FH')  //Hyrje
    {
        if (cmbKonfigurimi.GetText() != 'FHNV') {
            btneMagazina2.SetEnabled(false);
        }
    }
    else if (lloji == 2)    //Dalje
    {
        if (hfState.Get("kushtTransferim") === "Jo" && cmbKonfigurimi.GetText() != 'FDNV') {
            btneMagazina2.SetEnabled(false);
        }
        else {
            btneMagazina2.SetEnabled(true);
            grida.showCol("txtMagazina2");
        }
    }

    var mod;
    if ($("#hfShtimModifikim")[0].value == 'modifikim')
        mod = true;
    else mod = false;
    if (cmbLloji.GetText() != "")
        callWebserviceNiveliNew(cmbLloji.GetValue(), 'magazina', mod, pastroSeriale);
    else
        callWebserviceNiveliNew(0, 'magazina', mod, pastroSeriale);
}

function callWebserviceNiveliNew(lloji, tipi, mod, pastroSeriale) {
    try {
        var idNdermarrje = hfState.Get('idNdermarrje');
        var idPerdoruesi = hfState.Get('idPerdoruesi');
        $.ajax({ url: Utils.getServerApiUrl("Rregjistrime", "ktheTemplateNiveli"), data: JSON.stringify({ idNiveli: lloji, veprimi: tipi, mod: mod, idPerdoruesi: idPerdoruesi, idNdermarrje: idNdermarrje, idGjuha: hfState.Get('idGjuha') }) }).done(function (result) {
            SucceededCallbackNiveliNew(result, pastroSeriale);
        });
    } catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
    }
}
function SucceededCallbackNiveliNew(colModelet, pastroSeriale) {
    if ($("#hfShtimModifikim").val() == "shtim") {
        cmbKonfigurimi.ClearItems();
        for (i = 0; i < colModelet.length; i++) {
            cmbKonfigurimi.AddItem([colModelet[i].KodKonfigAmbjente, colModelet[i].PershkrimKonfigAmbjente], colModelet[i].IdKonfigAmbjente); //AddItem(teksti, vlera);
        }
        cmbKonfigurimi.SelectIndex(0);
    }
    ndryshoKonfigurimin(pastroSeriale);
}

/*
Function: merrTeDhena

Merr te dhenat qe ka grida dhe i vendos neper hidden field-e per ti perdorur ne server side
*/
function merrTeDhena(e) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    grida.jqGrid('saveRow', idRresht, false, 'clientArray');
    hfIdGride.Clear();
    trupiBosh = true; var kamag = true;
    panjesi = false;
    var idTe = grida.jqGrid('getDataIDs');
    for (var i = 0; i < idTe.length; i++) {
        hfIdGride.Add(i.toString(), idTe[i]);
        editorKategoria = grida.getTekstQelize('txtKategoria', idTe[i]);
        editorKodi = grida.getTekstQelize('txtKodi', idTe[i]);
        editorEmertimi = grida.getTekstQelize('txtEmertimi', idTe[i]);
        editorDetajimet = grida.getTekstQelize('txtDetajimi', idTe[i]);
        editorMagazina = grida.getTekstQelize('txtMagazina', idTe[i]);
        editorNjesia = grida.getTekstQelize('txtNjesia', idTe[i]);
        editorSasia = grida.getTekstQelize('txtSasia', idTe[i]);
        editorCmimi = grida.getTekstQelize('txtCmimi', idTe[i]);
        editorVlefta = grida.getTekstQelize('txtVlefta', idTe[i]);
        //if (cmbKonfigurimi.GetText() == "FDT" || cmbKonfigurimi.GetText() == "FDTK") {
        if (hfState.Get("kushtTransferim") === "Po") {
            editorMagazina2 = grida.getTekstQelize('txtMagazina2', idTe[i]);
        }
        else
            editorMagazina2 = "";
        if ($('#hfRuajDraft').val() != "Draft") {
            if (cmimzero == 2) {
                if (editorKategoria == "Artikull" && editorKodi != '' && parseFloat(editorCmimi) == 0.00 && cmbKonfigurimi.GetText() != "FH (SS)") {
                    myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukDuhetTeKeteArtikujMeCmimZero"));
                    e.processOnServer = false; click = false;
                    return;
                }
            }
            else if (cmimzero == 1) {
                if (editorKategoria == "Artikull" && editorKodi != '' && parseFloat(editorCmimi) == 0.00 && cmbKonfigurimi.GetText() != "FH (SS)") {
                    myMesazh.ShtoMesazhInformues(hfState.Get("msgKujdesKaCmimZeroNeGride"));
                    Utils.shfaqLoadingGif();
                }
            }
        }

        if (editorKodi != "") { trupiBosh = false; }
        var artikull = memoryArt.Get(grida.getTekstQelize('txtIdKodi', idTe[i]));
        if (editorKategoria == "Artikull" && editorKodi != '' && (artikull != null || artikull != undefined)) {
            var kaDetajimArtikulli = artikull.DetajimArtikulli;

            if (hfState.Get('plotesoDetajim1') == true && kaDetajimArtikulli == true) {
                if (grida.getTekstQelize('txtDetajimi', idTe[i]) == '') {
                    myMesazh.ShtoMesazhGabimi(hfState.Get("msgPlotesoniDetajiminEArtikullit") + grida.getTekstQelize('txtNrRendor', idTe[i]) + '!');
                    e.processOnServer = false;
                    click = false;
                    return;
                }
            }
            if (hfState.Get('plotesoDetajim2') == true && kaDetajimArtikulli == true) {
                if (grida.getTekstQelize('txtDetajimi2t', idTe[i]) == '') {
                    myMesazh.ShtoMesazhGabimi(hfState.Get("msgPlotesoniDetajimin2EArtikullit") + grida.getTekstQelize('txtNrRendor', idTe[i]) + '!');
                    e.processOnServer = false;
                    click = false;
                    return;
                }
            }

        }


        if (btneMagazina.GetEnabled() && editorMagazina == "" && editorKodi != '') {
            kamag = false;
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgShenoniMagazinen"));
            e.processOnServer = false;
        }

        if (editorKodi != "" && editorNjesia == "")
            panjesi = true;

        if (btneMagazina2.GetEnabled() && editorMagazina2 == "" && editorKodi != '' && (hfState.Get("kushtTransferim") === "Po")) {
            kamag = false;
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgShenoniMagazinenDestinacion"));
            click = false;
            e.processOnServer = false; return;
        }
        //if (cmbKonfigurimi.GetText() == "FDT" || cmbKonfigurimi.GetText() == "FDTK") {
        if (hfState.Get("kushtTransferim") === "Po") {
            if (editorMagazina !== "" && editorMagazina === editorMagazina2 && editorKodi !== "") {
                e.processOnServer = false;
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukMundTeBeniTransferimNeTeNjejtenMagazine"));
                click = false; return;
            }
        }

        if (editorSasia < 0 && hfState.Get("kushtTransferim") === "Po") {
            e.processOnServer = false;
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgTransferimMeSasiNegative"));
            click = false; return;
        }
    }
   

    if (kamag == false) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgShenoniMagazinen"));
        click = false;
    }

    var rreshtaTeGrides = grida.getTeDhenaRreshti();
    $('#gridDataObject').val(JSON.stringify(rreshtaTeGrides));
    unformatoFushaDevi();
}
function ruajKolonatEGrides(grida) {
    var idGride = colGrida[0].IdKoka;
    myJQGrid.ruajKolonatEGrides(grida, idGride, pageState.idGjuha, pageState.idNdermarrje, pageState.idViti, pageState.idPerdoruesi);
}
/*
Function: pastro

Pastrin array-t e perdorura dhe vendos counterat ne 0.
*/
function pastro() {
    arrKategoria = new Array();
    arrKodi = new Array();
    arrEmertimi = new Array();
    arrDetajimet = new Array();
    arrMagazina = new Array();
    arrNjesia = new Array();
    arrSasia = new Array();
    arrCmimi = new Array();
    arrVlefta = new Array();
    arrMagazina2 = new Array();
    resetCountera();
    var hidField1 = document.getElementById("hfKategoria");
    var hidField2 = document.getElementById("hfKodi");
    var hidField3 = document.getElementById("hfEmertimi");
    var hidField4 = document.getElementById("hfDetajimet");
    var hidField5 = document.getElementById("hfMagazina");
    var hidField6 = document.getElementById("hfNjesia");
    var hidField7 = document.getElementById("hfSasia");
    var hidField8 = document.getElementById("hfCmimi");
    var hidField9 = document.getElementById("hfVlefta");
    var hidField10 = document.getElementById("hfMagazina2");
    hidField1.value = "";
    hidField2.value = "";
    hidField3.value = "";
    hidField4.value = "";
    hidField5.value = "";
    hidField6.value = "";
    hidField7.value = "";
    hidField8.value = "";
    hidField9.value = "";
    hidField10.value = "";
    HfArt.Clear();
    hfArkiva.Clear();
    $('#hfArkivaDokId').val('');
    pageState.LejoMagazinaNdryshe = false;
}
/*
Function: ButtonClickKodi

Hap lupen e artikujve apo makrove sipas zgjedhjes qe eshte bere te kategoria.
*/
function ButtonClickKodi() {
    var grida = $('#rowed5');
    identikuesPerPopupArtikulli = "RegjistrimMagazine";
    var idRresht = grida.getLastSel2();
    var hfKod = document.getElementById("hfGridaKodi");
    var queryStr = hfKod.value;
    var kategoria = grida.getTekstQelize('txtKategoria', idRresht);
    switch (kategoria) {
        case "":
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniLlojinEVeprimit"));
            break;
        case "Artikull":
            popupUniversal.SetHeaderText(hfState.Get("headerPopUpZgjidhArtikullin"));
            var grup = '';
            if (btneMagazina.GetText() !== '') {
                var idMagazina = btneMagazina.GetValue();
                popupUniversal.SetContentUrl('LupaArtikull.aspx?idKonfigAmbjente=' + queryStr + '&grup=' + grup + '&idMag=' + idMagazina);
            }
            else
                popupUniversal.SetContentUrl('LupaArtikull.aspx?idKonfigAmbjente=' + queryStr + '&grup=' + grup);
            popupUniversal.SetSize(widthLupaArtikull, heightLupaArtikull);
            popupUniversal.Show();
            break;
        case "Makro":
            popupUniversal.SetHeaderText('Zgjidh makron');
            popupUniversal.SetContentUrl("LupaMakro.aspx?idKonfigAmbjente=" + queryStr);
            popupUniversal.SetSize(widthLupaMakro, heightLupaMakro);
            popupUniversal.Show();
            break;
        default:
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgLlojiVeprimitIPanjohur"));
            break;
    }
}
function ButtonClickKodiSet() {
    var grida = $('#rowed5');
    identikuesPerPopupArtikulli = "RegjistrimMagazineSet";
    var idRresht = grida.getLastSel2();
    var hfKod = document.getElementById("hfGridaKodi");
    var queryStr = hfKod.value;

    popupUniversal.SetHeaderText(hfState.Get("headerPopUpZgjidhArtikullin"));
    var grup = '';
    if (btneMagazina.GetText() !== '') {
        var idMagazina = btneMagazina.GetValue();
        popupUniversal.SetContentUrl('LupaArtikull.aspx?idKonfigAmbjente=' + queryStr + '&grup=' + grup + '&klasa=44&idMag=' + idMagazina);
    }
    else
        popupUniversal.SetContentUrl('LupaArtikull.aspx?idKonfigAmbjente=' + queryStr + '&grup=' + grup + '&klasa=44');
    popupUniversal.SetSize(widthLupaArtikull, heightLupaArtikull);
    popupUniversal.Show();


}
/*
Function: resetCountera

Vendos vleren 0 tek te gjithe counter-at.
*/
function resetCountera() {
    counter1 = 0;
    counter2 = 0;
    counter3 = 0;
    counter4 = 0;
    counter5 = 0;
    counter6 = 0;
    counter7 = 0;
    counter8 = 0;
    counter9 = 0;
    counter10 = 0;
}
/*
   Function: ButtonClickDetajimet

   Hap lupen e detajimeve te artikullit.
   */
function ButtonClickDetajim(lloji) {//po
    //marr vleren e IdKonfigurim te luper se detajimeve
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    pageState.identifikuesRreshti = idRresht;
    var vleraLlojit = grida.getTekstQelize('txtKategoria', idRresht);
    var hfDetajim = document.getElementById("hfGridaDetajimi");
    var queryStr = hfDetajim.value;
    var kategoria = jQuery('#txtKategoria' + idRresht)[0];
    var magazine = grida.getTekstQelize('txtMagazina', idRresht);

    if (vleraLlojit == "Artikull") //Artikull
    {
        var vleraKodit = grida.getTekstQelize('txtIdKodi', idRresht);
        var queryString = 'idArtikulli=' + vleraKodit + '&mag=' + magazine + '&vjenNga=' + Utils.getUrlVar('lloj') + '&lloji=' + lloji + '&idKonfigAmbjente=' + hfDetajim.value + '&detajimi1=' + grida.getTekstQelize('txtDetajimi', idRresht) + '&dateDok=' + dteDtDok.GetText();
        popupUniversal.SetHeaderText(hfState.Get("msgZgjidhDetajimArtikulli"));
        popupUniversal.SetContentUrl('LupaDetajimArtikulliRegjistrim.aspx?' + queryString);
        popupUniversal.SetSize(widthLupaDetajim, heightLupaDetajim);
        popupUniversal.Show();
    }
}


function ButtonClickDetajimet2() {
    ButtonClickDetajim(2);
}

function ButtonClickDetajimet() {
    ButtonClickDetajim(1);
}

function keyPressDetajimi(event, lloji) {//po
    var idRresht = $('#rowed5').getLastSel2();
    var vlera = event.target.value;  //$('#txtKodi' + lastsel2).val();
    if (vlera == "" && lloji == 1)
        $('#' + arrayIdKolonaGrides[5] + idRresht).autocomplete("close");
    else if (vlera == "" && lloji == 2)
        $('#' + arrayIdKolonaGrides[6] + idRresht).autocomplete("close");
    else
        callWebserviceDetajimi(vlera, lloji);
}



function keyPressDet(event) {
    keyPressDetajimi(event, 1);
}

function keyPressDet2(event) {
    keyPressDetajimi(event, 2);
}


/*
Function: vendosVleftat
Merr dhe validon vlerat e sasise dhe cmimit te caktuara ne gride dhe therret funksionin <updateTotalet>
*/
function vendosVleftat(s, idRreshti, arti) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    if (typeof idRreshti == 'undefined')
        idRreshti = idRow;
    var sasia = grida.getTekstQelize('txtSasia', idRreshti);
    var cmimi = grida.getTekstQelize('txtCmimi', idRreshti);
    var vlefta = grida.getTekstQelize('txtVlefta', idRreshti);
    var editorKodi = grida.getTekstQelize('txtKodi', idRreshti);
    var idKodi = grida.getTekstQelize('txtIdKodi', idRreshti);
    var detajim = -1;


    if (grida.getTekstQelize('txtDetajimi', idRreshti) != "")
        detajim = grida.getTekstQelize('txtDetajimi', idRreshti);
    if (sasia == '.') {
        grida.setTekstQelize('txtSasia', idRreshti, '0.');
        return;
    }
    if (cmimi == '.') {
        grida.setTekstQelize('txtCmimi', idRreshti, '0.');
        return;
    }
    if (cmimi == 0 && cmimzero == 2) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukLejohetCmimZeroNeGride"));
        if (pageState.kushte.F != 1)
            $("#txtCmimi" + idRreshti).focus();
    }
    if (cmimi == 0 && cmimzero == 1) {
        if (cmbLloji.GetText() != "FD" && cmbLloji.GetText() != "UD")
            myMesazh.ShtoMesazhInformues('Kujdes ka cmim zero ne gride');
    }
    if (isNaN(sasia)) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiaDuhetNumer"));
        sasia = grida.getVlereDefault('txtSasia');
        $('#txtSasia' + idRreshti).focus();
    }
    if (sasia == "0") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiaNukMundTeJeteZero"));
        sasia = grida.getVlereDefault('txtSasia');
        $('#txtSasia' + idRreshti).focus();
    }
    else
        if (sasia < totaletSasiveDetajimeve[idRreshti]) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiaNukMundTeJeteMeEVogelSeTotDetajimeve"));
            grida.setTekstQelize('txtSasia', idRreshti, totaletSasiveDetajimeve[idRreshti]);
            $('#txtSasia' + idRreshti).focus();
        }
        else
            if (gjendjeartminmax == 1 && editorKodi != '' && s == "txtSasia") {
                $.ajax({
                    url: Utils.getServerApiUrl("Rregjistrime", "merrGjendjeArtikulliMag"),
                    data: JSON.stringify({ idartikulli: idKodi, mag: grida.getTekstQelize('txtMagazina', idRreshti), idNdermarrje: pageState.idNdermarrje })
                }).done(function (result) {
                    SucceededCallbackGjendjeArtikulli(result, arti); 
                });
            }
    if (cmimi === '' || isNaN((cmimi))) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgCmimiDuhetTeJeteNumer"));
        cmimi = grida.getVlereDefault('txtCmimi');
        $('#txtCmimi' + idRreshti).focus();
    }
    if (isNaN(vlefta)) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgVleftaDuhetTeJeteNumer"));
        vlefta = grida.getVlereDefault('txtVlefta');
        grida.setTekstQelize('txtVlefta', idRreshti, vlefta);
        $('#txtVlefta' + idRreshti).focus();
    }

    if (s == 'txtCmimi' || s == 'txtSasia') {
        if (!isNaN(parseFloat(cmimi)) && !isNaN(parseFloat(sasia))) {
            grida.setTekstQelize('txtVlefta', idRreshti, parseFloat(sasia) * parseFloat(cmimi));
        }
    }
    else if (s == 'txtVlefta') {
        if (Utils.getUrlVar("lloj") == 'hyrje') {
            if (!isNaN(parseFloat(vlefta)) && !isNaN(parseFloat(sasia))) {
                grida.setTekstQelize('txtCmimi', idRreshti, parseFloat(parseFloat(vlefta) / sasia));
            }
        }
    }
    updateTotalet();
}

function kontrolloVlereBosh() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var sasia = grida.getTekstQelize('txtSasia', idRresht);
    var cmimi = grida.getTekstQelize('txtCmimi', idRresht);
    var vlefta = grida.getTekstQelize('txtVlefta', idRresht);
    var updateVleften = false;
    if (cmimi === '' || isNaN(parseFloat(cmimi))) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgCmimiDuhetTeJeteNumer"));
        cmimi = grida.getVlereDefault('txtCmimi');
        grida.setTekstQelize('txtCmimi', idRresht, cmimi);
        updateVleften = true;
    }
    //if (cmimi == 0 && cmimzero == 1) {
    //    myMesazh.ShtoMesazhInformues("Kujdes ka cmim zero ne gride!");
    //}
    if (sasia === '' || isNaN(parseFloat(sasia)) || parseFloat(sasia) === 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiaDuhetNumer"));
        sasia = grida.getVlereDefault('txtSasia');
        grida.setTekstQelize('txtSasia', idRresht, sasia);
        updateVleften = true;
    }
    if (updateVleften)
        grida.setTekstQelize('txtVlefta', idRresht, parseFloat(sasia) * parseFloat(cmimi));
}


/*
Function: vendosCmimin

Llogarit cmimin kur shenohen sasia dhe vlefta dhe therret funksionin <updateTotalet>.
*/
function vendosCmimin() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var sasia = grida.getTekstQelize('txtSasia', idRresht);
    var cmimi = grida.getTekstQelize('txtCmimi', idRresht);
    var vlefta = grida.getTekstQelize('txtVlefta', idRresht);

    if (isNaN(sasia)) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiaDuhetNumer"));
        sasia = grida.getVlereDefault('txtSasia');
        grida.setTekstQelize('txtSasia', idRresht, sasia);
        $('#txtSasia' + idRresht).focus();
    }
    else if (sasia == "0") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiaNukMundTeJeteZero"));
        sasia = grida.getVlereDefault('txtSasia');
        grida.setTekstQelize('txtSasia', idRresht, sasia);
        $('#txtSasia' + idRresht).focus();
    }
    else if (sasia < totaletSasiveDetajimeve[idRresht]) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiaNukMundTeJeteMeEVogelSeTotDetajimeve"));
        grida.setTekstQelize('txtSasia', idRresht, totaletSasiveDetajimeve[idRresht]);
        $('#txtSasia' + idRresht).focus();
    }
    if (isNaN(cmimi.value)) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgCmimiDuhetTeJeteNumer"));
        cmimi = grida.getVlereDefault('txtCmimi');
        grida.setTekstQelize('txtCmimi', idRresht, cmimi);
        $('#txtCmimi' + idRresht).focus();
    }
    if (isNaN(vlefta.value)) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgVleftaDuhetTeJeteNumer"));
        vlefta = grida.getVlereDefault('txtVlefta');
        grida.setTekstQelize('txtVlefta', idRresht, vlefta);
        $('#txtVlefta' + idRresht).focus();
    }
    grida.setTekstQelize('txtCmimi', idRresht, parseFloat(parseFloat(vlefta) / sasia));
    updateTotalet();
}


function updateTotalet() {
    var totali = 0;
    var grida = $("#rowed5");

    var idTe = grida.jqGrid('getDataIDs');
    for (var i = 0; i < idTe.length; i++) {
        if (grida.getTekstQelize('txtKodi', idTe[i]) != "") {
            var vlefta = grida.getTekstQelize('txtVlefta', idTe[i]);
            if (vlefta == '.' || vlefta == 'NaN' || vlefta == '')
                vlefta = 0;

            totali = parseFloat(totali) + parseFloat(vlefta);
        }
    }
    txtVlefta.SetText(totali);
}


function vendosNrAutomatik(colAtrTrupi, colKontrollet) {
    myNrAuto.vendosNrAutomatik(colAtrTrupi, colKontrollet, dteDtDok.GetDate());
}
/*
Function: pastroFushatKokes

Pastron fushat pasi eshte bere ruajtja apo modifikimi per te bere gati ambjentin per shtim.
*/
function pastroFushatKokes() {
    hfSeriale.Clear(); hfSasiSeriale.Clear();
    $('#hfId').val(0);
    btneKlientFurnitori.SetValue(null);
    btneMagazina.SetValue(null);
    previousMag = btneMagazina.GetSelectedItem();
    cmbLlogariKunderParti.SetValue(null);
    btneMagazina2.SetValue(null);
    txtNrDok.SetText('');
    cmbDegeAdministrative.SetValue(null);
    txtPershkrimMagazine.SetText('');
    txtPershkrimDege.SetText('');
    txtNrProjekti.SetText('');
    cbMeKonfirmim.SetChecked(false);
    txtShenime.SetText('');
    txtVlefta.SetText(0);
    cmbGrup1.SetValue(null);
    cmbGrup2.SetValue(null);
    cmbGrup3.SetValue(null);
    txtMagazinieri.SetText('');
    txtPershkrimi.SetText('');
    txtadresa.SetText('');
    ucEmerSkedari.ClearText('');
    btneAutomjeti.SetValue(null);
    cmbFormatiPrintimit.SetText('');
    txtTarga.SetText('');
    cbDergoMeEmail.SetChecked(false);
    cbDergoEmailDokArkives.SetChecked(false);
    var hf = document.getElementById("status1");
    hf.value = "false";
    vendosDateDefault();
    pyeturDoni = 0;
    txtShoferi.SetText('');
    txtTarga2.SetText('');
    cmbKategoriSeriali.SetSelectedIndex(-1);
    txtNrSerial.SetText('');
    txtNIVFSH.SetText('');
    txtWTNIC.SetText('');
    cmbOperatori.SetValue(null);
    btnTransportuesi.SetValue(null);
}
function ButtonClickKategoriSeriali() {
    myButtonClickLupa.LupaUniversal_Click("Zgjidhni kategorine e serialit", 'LupaKategoriSeriali.aspx?vjenNga=Shto_Artikull', widthLupaKF, heightLupaKF);
}
var dtDokumentit;

function vendosDateDefault() {
    if ($("input[id$='hfShtimModifikim']").val() == "shtim" || $("input[id$='hfShtimModifikim']").val() === "shtimraport"|| $("input[id$='hfShtimModifikim']").val() == "rezervim" || $("input[id$='hfShtimModifikim']").val() == "konvertim") {
        var hfPeriudheObj = window.parent.lexoHfPeriudhe();
        dteDtDok.SetDate(Utils.ktheDateDefault(hfPeriudheObj));
        dteDtTransporti.SetDate(Utils.ktheDateDefault(hfPeriudheObj));
        var dataSot = Utils.zeroOren(new Date());
        dteDtRegjistrimi.SetDate(dataSot);
    }
}

/*
Function: EndRequestHandler

Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe array-t e perdorura.
Shiko funksionet <pastro>, <pastroFushatKokes> dhe <ndryshoKonfigurimin>.
*/
function EndRequestHandler(sender, args) {
    formatoFushaDevi();
    var hf = document.getElementById("status1");
    if (hf.value == "konvertuar") {
        popKonvertuar.Show();
        return;
    }

    if ($('#hfqkmesazhi').val() == 'shfaqmesazh') {
        myMesazh.ShtoMesazh({ type: "confirm", layout: "center", modal: true, text: hfState.Get("msgDeshironiShperndQendraKostoMag"), okClick: function () { Utils.hapPopUp(hfState.Get("msgShperndarjeNeQendratEKostos"), $('#hfUrl')); }, cancelClick: function () { Utils.JopopupClick($('#hfUrl')); } });
        $('#hfqkmesazhi').val('jo');
    }
    if ($('#hfqkmesazhi').val() == 'shfaqlupe') {
        Utils.hapPopUp(hfState.Get("msgShperndarjeNeQendratEKostos"), $('#hfUrl'));
        $('#hfqkmesazhi').val('jo');
    }
    if (hf.value == "true") {
        myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimMagazine.aspx?lloj=' + Utils.getUrlVar('lloj') + '&shtim_modifikim=shtim', true);
    }
    else
        click = false;
}

function pastroHfQk() {
    if ($('#hfUrl').val() != "")
        $('#hfUrl').val("");
}
var shtoTimer;
function shtoTimedClick(e) {
    if (!Utils.nrWsRrugesManager.kanePerfunduarWs())
        shtoTimer = setTimeout(function () { shtoTimedClick(e); }, 500);
    else {
        clearTimeout(shtoTimer);
        myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimMagazine.aspx?lloj=' + Utils.getUrlVar('lloj') + '&shtim_modifikim=shtim', true);
        e.processOnServer = false;
    }
}
/*
Function: isValidKoka

Kontrollon nese jane plotesuar fushat e detyrueshme te kokes se dokumentit dhe validon daten.
*/
function isValidKoka() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if (!Utils.nrWsRrugesManager.kanePerfunduarWs()) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgPoTransferohetTeDhenatShtypniPerseriRuaj"));
        return false;
    }
    editorCmimi = jQuery("#txtCmimi" + idRresht)[0];
    editori = jQuery("#txtKodi" + idRresht)[0];
    editorKodi = jQuery("#txtKodi" + idRresht)[0];
    editorLloji = jQuery("#txtKategoria" + idRresht)[0];
    if (cmbLloji.GetText() == "") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniLlojin"));
        return false;
    }
    if (txtNrDok.GetText() == "") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgShenoniNumrinEDokumentit"));
        return false;
    }
    if (cmbLlogariKunderParti.GetVisible() && cmbLlogariKunderParti.GetText() == "") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgShenoniLlogarine"));
        return false;
    }
    if (dteDtDok.GetDate() == null) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniNjeDateDokumenti"));
        return false;
    }
    if (dteDtRegjistrimi.GetDate() == null) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniNjeDateRegjstrimi"));
        return false;
    }
    if ((cmbKonfigurimi.GetText() == 'FHNV' || cmbKonfigurimi.GetText() == 'FDNV') && btneMagazina2.GetText() === "") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgVendosniNjesineVartese"));
        return false;
    }

    if (regjistrimKF != 1 && btneKlientFurnitori.validationGroup == "entries" && btneKlientFurnitori.GetValue() == null) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgJepniKF"));
        return false;
    }
    if ($('#hfRuajDraft').val() != "Draft" && editorCmimi != undefined && cmimzero == 2 && grida.getTekstQelize('txtCmimi', idRresht) == 0.00 && grida.getTekstQelize('txtKodi', idRresht) != '' && grida.getTekstQelize('txtKategoria', idRresht) == "Artikull") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukLejohetCmimZeroNeGride"));
        editorCmimi.focus();
        return false;
    }
    return ValidoArtikujSet();
}

function MagazinaNdryshetrup(s, e) {
    if (e.item.name != 'Ruaj' && e.item.name != 'RuajPrint' && e.item.name != 'Draft')
        return false;
    var kaNdryshuarMagDest = false;
    var kaNdryshuarMag = false;
    
    pageState.parametraMenuClick = new Array(s, e, true);
    var grida = $(pageState.gridaSelector);
    var data = grida.getTeDhenaRreshti();
    for (i = 0; i < data.length; i++)
    {
        var row = data[i];
        var row1 = data[i + 1];
     

        if (row.txtIdKodi != "" && (data.filter(function (item) { return item.txtMagazina != row.txtMagazina && item.txtMagazina != "" && item.txtKodi != ""; }).length > 0))
                        kaNdryshuarMag = true;
        if (row.txtIdKodi != "" && (data.filter(function (item) { return item.txtMagazina2 != row.txtMagazina2 && item.txtMagazina2 != "" && item.txtKodi != ""; }).length > 0))
                        kaNdryshuarMagDest = true;

       
    }

    identifikuesPyetje = "magazina";
    if (kaNdryshuarMagDest && kaNdryshuarMag && pageState.lajmerimmagazineDes==1) {
        myMesazh.ShtoPyetje(hfState.Get("msgLajmerimmagdheMagazinedestinacionENdryshmeNeGride"), true);
        e.processOnServer = false;
        return true;
    }
    if (kaNdryshuarMagDest && pageState.lajmerimmagazineDes == 1) {
        myMesazh.ShtoPyetje(hfState.Get("msgLajmerimMagazinedestinacionENdryshmeNeGride"), true);
        e.processOnServer = false;
        return true;
    }
    if (kaNdryshuarMag) {
        myMesazh.ShtoPyetje(hfState.Get("msgLajmerimMagazineENdryshmeNeGride"), true);
        e.processOnServer = false;
        return true;
    }
    
   
    return false;
}

function menu_click(s, e) {
    var kaMagazinaNdryshe = (lajmerimmagazine == 1 ? MagazinaNdryshetrup(s, e) : false);
    if (kaMagazinaNdryshe)
        return;

    if (!myJQGrid.checkIsPageReadyToSave(e.item.name)) {
        Utils.shfaqLoadingGif();
        Utils.shtoFunksionNeRadheMeParametra(menuClick, [s, e, true], pageState.idGjuha, this);
        e.processOnServer = false;
        return;
    }

    menuClick(s, e, false);
}

function menuClick(s, e, doPostback) {

    $('#hfRuajDraft').val(e.item.name);
    e.processOnServer = true;
    if (e.item.name === 'Ruaj' || e.item.name === 'RuajPrint' || e.item.name === 'Refuzo') {
        myMesazh.vendosServer();
        
        var valid = myFaqeCelje.validim(s, e);
       
        if (!valid) {
            Utils.hiqLoadingGif();
            e.processOnServer = false;
            click = false;
            return;
        }
        if (isValidKoka()) {
            Utils.shfaqLoadingGif();
            RuajClick(s, e);
            Utils.doPostback(s == 'tastiera' ? { name: "ASPxMenu1" } : s, e, s == 'tastiera' ? true : doPostback);
            return;
        }
        else {
            e.processOnServer = false;
            click = false;
        }
        //if (s == 'tastiera' && e.processOnServer)
        //    btn.DoClick();
        //return;
    }
    if (e.item.name === 'Klono') {
        KlonoClick(e);
    }
    if (e.item.name == 'Serialet') {
        if (ValidoArtikujSet()) {
            HapLupeSerialeshUnike();
        }

        e.processOnServer = false;
        click = false;
        return;
    }
    if (e.item.name == 'Konverto') {
        ButtonClickKonverto();
        click = false;
        e.processOnServer = false;
        return;
    }
    if (e.item.name === 'QendraKosto') {
        e.processOnServer = false;
        click = false;
        return;
    }
    else if (e.item.name == 'Draft') {
        myMesazh.vendosServer();
        if ($("input[id$='hfAutorizimi']").val() == 'False') {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukKeniAutorizimPerTeRuajturKeteDok"));
            Utils.hiqLoadingGif();
            e.processOnServer = false;
            click = false;
            return;
        }
        myFaqeCelje.validim(s, e);
        if (isValidKoka()) {
            Utils.shfaqLoadingGif();
            RuajClick(s, e);
            Utils.doPostback(s, e, doPostback);
            return;
        }
        else {
            e.processOnServer = false;
        }
        //if (s == 'tastiera' && e.processOnServer)
        //    btn.DoClick();
        return;
    }
    else if (e.item.name == 'Kerko') {
        myFaqeCelje.kontrolloTeDrejta('RegjistrimMagazine.aspx?lloj=' + Utils.getUrlVar('lloj'), null, true);
        e.processOnServer = false;
        return;
    }
    else if (e.item.name == 'Shto') {
        myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimMagazine.aspx?lloj=' + Utils.getUrlVar('lloj') + '&shtim_modifikim=shtim', true);
    }
    else if (e.item.name == 'Fshi') {
        identifikuesPyetje = 'FshiDokMag';
        if ($("input[id$='hfAutorizimi']").val() == 'False') {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukKeniAutorizimPerTeRuajturKeteDok"));
            Utils.hiqLoadingGif();
            click = false;
            e.processOnServer = false;
            return;
        }
        if ($("input[id$='hfShtimModifikim']").val() == 'modifikim' && hfState.Get('mesazhKonvertuar') != "") {
            myMesazh.ShtoPyetje(hfState.Get("mesazhKonvertuar"));
        }
        else
            myMesazh.ShtoPyetje(hfState.Get("labelAdministrimiMsgJeniSigurt"));
        e.processOnServer = false;
        return;
    }
    else if (e.item.name == 'Anullo') {
        myFaqeCelje.kontrolloTeDrejta('RegjistrimMagazine.aspx?lloj=' + Utils.getUrlVar('lloj'));
        e.processOnServer = false;
        click = false;
        return;
    }
    if (e.item.name == 'Arkiva') {
        ButtonClickArkiva();
        e.processOnServer = false;
        click = false;
        return;
    }
    Utils.doPostback(s, e, doPostback);
}

function HapLupeSerialeshUnike(idSeti) {
    var idMag = btneMagazina.GetValue();
    myButtonClickLupa.ButtonClickLupaSerialeUnike(popupSerialet, 'Serialet', {
        "guidString": hfState.Get("guidString"),
        "hyrje_dalje": Utils.getUrlVar('lloj'),
        "lidhur": $("input[id$='hfLidhur']").val().toLowerCase() === 'true',
        "MerrArtikujSet": Utils.getUrlVar('lloj') == "dalje",
        "MenyreKontrollSeriali": pageState.kushte.kushtSerialSinkron,
        "MNSA": !(hfState.Get("MNSA")),
        "IdMag": idMag != undefined ? idMag : 0,
        "Shitje": false,
        "Kthim": false,
        "KGJAPMR": false,
        "Id": pageState.lloji == 'modifikim' ? Utils.getUrlVar('id') : 0,
        "IdSeti": idSeti ? idSeti : 0
    });
}

function MerrArtikujDheSasitePerkatese() {
    var artikujMeSasi = new Array();
    var rreshta = $(pageState.gridaSelector).getTeDhenaRreshti().filter(function (s) { return s.txtIdKodi != '' && s.txtIdArtikullSet == ""; });
    for (var i = 0; i < rreshta.length; i++)
        artikujMeSasi.push({ IdArtikulli: rreshta[i].txtIdKodi, Sasi: rreshta[i].txtSasia, Mag: rreshta[i].txtMagazina, IdSeti: 0 });
    return artikujMeSasi;
}

function ValidoArtikujSet() {
    if ($("input[id$='hfLidhur']").val().toLowerCase() === 'true' || hfState.Get("GjeneruarNgaMema"))
        return true;

    var perberesit = grupoArtikujtEPerbere();
    var perberes;
    var grida = $(pageState.gridaSelector);
    var data = grida.getTeDhenaRreshti();

    for (i = 0; i < data.length; i++) {
        var row = data[i];
        if (row.txtIdKodi == "" || row.txtIdArtikullSet == "")
            continue;
        for (j = 0; j < perberesit.length; j++) {
            perberes = perberesit[j];
            if (perberes.KodSeti == row.txtIdArtikullSet && row.txtIdKodi == perberes.IdArtikulli) {
                perberes.SasiaNeGride += row.txtSasia;
                break;
            }
        }
    }
    for (k = 0; k < perberesit.length; k++) {
        perberes = perberesit[k];
        if (perberes.SasiaNeGride == 0) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("artikujSetProblemeMeRecepturat"));
            return false;
        }
        if (perberes.SasiaNeGride != perberes.SasiaSet * perberes.Koeficenti) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("artikujSetProblemeMeRecepturatSasite"));
            return false;
        }
    }

    return true;
}

function grupoArtikujtEPerbere() {
    var perberesit = new Array();

    for (i = 0; i < colPerberesList.length; i++) {
        var perberes = colPerberesList[i];
        var ugjet = false;
        for (j = 0; j < perberesit.length; j++) {
            var newPerberes = perberesit[j];
            if (perberes.IdSeti == newPerberes.IdSeti && perberes.IdArtikulli == newPerberes.IdArtikulli && perberes.KodSeti == newPerberes.KodSeti && perberes.Koeficenti == newPerberes.Koeficenti) {
                newPerberes.SasiaSet += perberes.SasiaSet;
                ugjet = true;
                break;
            }
        }
        if (!ugjet) {
            perberesit.push({ IdSeti: perberes.IdSeti, IdArtikulli: perberes.IdArtikulli, Koeficenti: perberes.Koeficenti, KodSeti: perberes.KodSeti, SasiaSet: perberes.SasiaSet, SasiaNeGride: 0 });
        }
    }
    return perberesit;
}


function ButtonClickKonverto() {//po

    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "KontrolloKonvertuar"),
        data: JSON.stringify({ ids: [Utils.getUrlVar('id')], kodkonfig: "LDMD", idNdermarrje: pageState.idNdermarrje, idPerdoruesi: pageState.idPerdoruesi, idGjuha: pageState.idGjuha, pageId: window['CurrentPageId'] })
    }).done(SuccededCallbackKonvertime);
}

var konfigurimet, idte;
function SuccededCallbackKonvertime(result) {
    Utils.hiqLoadingGif();
    var nivelet = result.nivelet;
    var colKonfig = result.colKonfig;
    if (result.mesazh !== "Nuk jane konvertuar") {
        myMesazh.ShtoMesazhGabimi(result.mesazh);
        return;
    }
    if (nivelet.length == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgDokNukMundTeKonvertohet"));
        return;
    }
    cmbKonverto.BeginUpdate();
    cmbKonverto.ClearItems();
    for (i = 0; i < nivelet.length; i++)
        cmbKonverto.AddItem(nivelet[i].Kodi, nivelet[i].IdNivel); //AddItem(teksti, vlera);
    cmbKonverto.EndUpdate();
    cmbKonverto.SelectIndex(0);
    cmbKonf.BeginUpdate();
    cmbKonf.ClearItems();
    for (i = 0; i < colKonfig.length; i++)
        if (colKonfig[i].IdNivel == cmbKonverto.GetValue())
            cmbKonf.AddItem(colKonfig[i].KodKonfigAmbjente, colKonfig[i].IdKonfigAmbjente); //AddItem(teksti, vlera);
    cmbKonf.EndUpdate();
    cmbKonf.SelectIndex(0);
    konfigurimet = colKonfig;
    idte = result.ids;
    popKonvertim.Show();
}

function ndryshoNiveli(s, e) {
    cmbKonf.ClearItems();
    for (i = 0; i < konfigurimet.length; i++)
        if (konfigurimet[i].IdNivel == s.GetValue())
            cmbKonf.AddItem(konfigurimet[i].KodKonfigAmbjente, konfigurimet[i].IdKonfigAmbjente); //AddItem(teksti, vlera);
    cmbKonf.SelectIndex(0);
}

function konverto() {
    Utils.konverto("", idte, "", SuccededCallbackKontrolloEkzistojneDokQePoKonvertohen);
}

function SuccededCallbackKontrolloEkzistojneDokQePoKonvertohen() {
   
    if (cmbKonverto.GetText() == 'FD' || cmbKonverto.GetText() == 'UD')
        myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimMagazine.aspx?lloj=dalje' + '&id=' + idte[0] + '&shtim_modifikim=konvertim&niveli=' + cmbKonverto.GetValue() + '&konfigurim=' + cmbKonf.GetValue() + '&fsh=jo' + '&pageCacheId=' + window['CurrentPageId']);

    else if (cmbKonverto.GetText() == 'FH' || cmbKonverto.GetText() == 'UH')
        myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimMagazine.aspx?lloj=hyrje' + '&id=' + idte[0] + '&shtim_modifikim=konvertim&niveli=' + cmbKonverto.GetValue() + '&konfigurim=' + cmbKonf.GetValue() + '&fsh=jo' + '&pageCacheId=' + window['CurrentPageId']);

    else
        myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimDokumentash.aspx?shitje_blerje=' + Utils.getUrlVar('shitje_blerje') + '&id=' + idte[0] + '&shtim_modifikim=konvertim&niveli=' + cmbKonverto.GetValue() + '&konfigurim=' + cmbKonf.GetValue() + '&pageCacheId=' + window['CurrentPageId']);
}

function KlonoClick(e) {
    var katDokAndKomponentObj = [{ idKatDok: 6, komponente: '' }];
    popUpKlonimiFunctions.showPopUp(cmbLloji.GetValue(), cmbKonfigurimi.GetValue(), Utils.getUrlVar('id'), katDokAndKomponentObj);
    click = false;
    e.processOnServer = false;
}

function Kthehu(mesazh) {
    myFaqeCelje.kontrolloTeDrejta('RegjistrimMagazine.aspx?lloj=' + Utils.getUrlVar('lloj') + "&ruaj=po");
}

function RuajClick(s, e) {
    var grida = $('#rowed5');
    if (click) {
        e.processOnServer = false;
        Utils.hiqLoadingGif();
        return;
    }
    click = true;
    if (isValidKoka()) {

        merrTeDhena(e);
        grida.setLastSel2(0);
        if (trupiBosh == true) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgTrupiDokumentitNukDuhetLeneBosh"));
            e.processOnServer = false;
            click = false;
        }
        else if (panjesi == true) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgKaArtikujPaNjesi"));
            e.processOnServer = false;
            click = false;
        }
    }
    else {
        e.processOnServer = false;
        click = false;
        Utils.hiqLoadingGif();
    }
}

/*
Function: PastroClick

Pastron koken e dokumentit dhe array-t e perdorura deh inicializon griden.
Shiko funksionet <pastro>, <pastroFushatKokes> dhe <inicializoGride>.
*/
function PastroClick() {
    var hf = $("#hfShtimModifikim");
    var modifikim = hf.val();
    hf.val("shtim");
    if (modifikim == 'modifikim')
        TextChangedLloji(true);
    else ndryshoKonfigurimin(true);
    $("input[id$='hfLidhur']").val(false);
    $("input[id$='hfAutorizimi']").val(true);
    if (ruajlocalstorage) {
        localStorage.setItem("degeadmmagazina", cmbDegeAdministrative.GetText());
        localStorage.setItem("magazinamag", btneMagazina.GetText());
        localStorage.setItem("magazinamag2", btneMagazina2.GetText());
        localStorage.setItem("datedokmag", JSON.stringify(dteDtDok.GetDate()));
    }
    lejomod = true;
    pastro();
    pastroFushatKokes();
    var hf = $("#hfShtimModifikim");
    ASPxMenu1.GetItemByName('Shto').SetVisible(true);
    ASPxMenu1.GetItemByName('PrintPreview').SetVisible(false);
    ASPxMenu1.GetItemByName('FletaKontabel').SetVisible(false);
    ASPxMenu1.GetItemByName('Fshi').SetVisible(false);
    ASPxMenu1.GetItemByName('QendraKosto').SetVisible(false);
    ASPxMenu1.GetItemByName('Klono').SetVisible(false);
    ASPxMenu1.GetItemByName('Draft').SetVisible(true);
    ASPxMenu1.GetItemByName('Ruaj').SetVisible(true);
    ASPxMenu1.GetItemByName('RuajPrint').SetVisible(true);
    ASPxMenu1.GetItemByName('RefuzoDraft').SetVisible(false);
    
    hfState.Set("HyrjeGjeneruarNgaMema", false);
    hfState.Set("GjeneruarNgaMema", false);
    colPerberesList = new Array();
    myMenu.menuSipasTeDrejtaRegjistrim(hf, hfTeDrejta);
    $('#ASPxSplitter1_hl').empty();
    $('#ASPxSplitter1_tblKonfigurimi>tbody>tr:eq(0)>td:eq(0)').empty();
    click = false;
}

function ndryshoImazhin(nr, index) {
    var id = "butonFshi" + index;
    if (document.getElementById(id) != null) {
        if (nr == 1)
            document.getElementById(id).src = "images/blue-square-icon.png";
        else if (nr == 0)
            document.getElementById(id).src = "images/square-icon.png";
    }
}

function ButtonClickNavBar(s) {
    var modinfo = -1;
    for (kus = 0; kus < colKushte.length; kus++) {
        if (colKushte[kus].Kodi == 'ZIA')
            modinfo = colKushte[kus].Vlera;
    }

    if (s.GetText().split('alt="')[1].split('"')[0] == "Artikulli")
        myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhiniKonfiguriminEInfosSeArtikullit"), 'LupaKonfigurimInfo.aspx?id=' + modinfo + '&ruaj=po', 600, 500);
}
/*
Function: ShfaqPeriudhen

Hap lupen e priudhave.
*/
function ShfaqPeriudhen() {
    popupUniversal.SetHeaderText(hfState.Get("msgZgjidhPeriudheKontabel"));
    popupUniversal.SetContentUrl('LupaPeriudhaKontabel.aspx');
    popupUniversal.SetSize(widthLupaPeriudha, heightLupaPeriudha);
    popupUniversal.Show();
}

function lostFocusPeriudha(vlera) {
    //            if (vlera != '') {
    //                callBackPanel.PerformCallback('skeme,' + vlera);
    //            }
}

function valueChangedPeriudha() {
    var vleraLabel = lblPeriudhaAktuale.GetText();
    var periudha = vleraLabel.split("-");
    var dataDok = new Date();
    dataDok = formatDate(dataDok, "dd/MM/yyyy");

    periudha1 = periudha[0].split("/");
    periudha2 = periudha[1].split("/");
    dtDokumentit = dataDok.split("/");
    if (periudha1[2] != dtDokumentit[2])
        //return false;
        dteDtDok.SetText(periudha[0]);
    else {
        if ((dtDokumentit[1] < periudha1[1] || dtDokumentit[1] > periudha2[1]))
            dteDtDok.SetText(periudha[0]);
        else
            dteDtDok.SetText(dataDok);
    }
}
function Succedcallback(result) {
    lbxZgjedhur.ClearItems();
    for (var i = 0; i < result.length; i++) {
        lbxZgjedhur.AddItem([result[i].PershkrimKolone, ''], result[i].EmerKolone);
    }
    lbxZgjedhur.SetHeight(result.length * 22 + 28);
}

function Expanded() {
    lbxZgjedhur.SetHeight(lbxZgjedhur.GetItemCount() * 22 + 28);
}

function KontrolloTeDrejta(s) {
    var alti = $(s.GetValue()).attr('alt');
    if (alti == "Artikulli" && $('#hfTeDrejtaInfoArt').val() == 'False')
        s.SetEnabled(false);
}

function hapPopUpRi(eshteAqt) {
    var qs = '?vjenNga=Shto_RegjistrimMagazina&veprimi=shtim';
    var headerText;
    if (eshteAqt) {
        qs += '&llojiart=aqt';
        headerText = hfState.Get("JQgridShtoArtikullAqt");
    }
    else {
        qs += '&llojiart=afatshkurter';
        headerText = hfState.Get("msgShtoArtikull");
    }
    myButtonClickLupa.LupaUniversal_Click(headerText, 'LupaArtikullShpejte.aspx' + qs, 1100, 600);
}

function hapPopUpModifikoArt(eshteAqt) {
    var kodArtikulli = '';
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    kodArtikulli = grida.getTekstQelize('txtKodi', idRresht);
    if (kodArtikulli != '' && kodArtikulli != null && kodArtikulli != undefined) {
        var qs = '?vjenNga=Shto_RegjistrimMagazina&veprimi=modifikim&kodArt=' + kodArtikulli;
        if (eshteAqt)
            qs += '&llojiart=aqt';
        else
            qs += '&llojiart=afatshkurter';
        myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgShtoArtikull"), 'LupaArtikullShpejte.aspx' + qs, 1100, 600);
    }
}

var mbyll = true;
function RuajHapurMbyllur(hapur) {
    mbyll = false;
    if (!hapur) { splitter.GetPane(1).CollapseBackward(); $('#hfHapurMbyllur').val('False'); }
    else $('#hfHapurMbyllur').val('True');
    try {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ruajHapurMbyllur"),
            data: JSON.stringify({ hapur: hapur, idperdorues: pageState.idPerdoruesi, idperdoruesveprimi: pageState.idPerdoruesi })
        }).done(SuccededCallbackHapurMbyllur);
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimRuajtje"));
    }
}

function RuajHapurMbyllurplus(hapur) {
    mbyll = false;
    if (!hapur) {
        splitter.GetPane(1).CollapseBackward();
        $('#hfHapurMbyllur').val('False');
    }
    else
        $('#hfHapurMbyllur').val('True');
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ruajHapurMbyllurplus"),
        data: JSON.stringify({ hapur: hapur, idperdorues: pageState.idPerdoruesi, idperdoruesveprimi: pageState.idPerdoruesi })
    }).done(SuccededCallbackHapurMbyllur);
}


function RuajHapurMbyllurminus(hapur) {
    mbyll = false;
    if (!hapur) { splitter.GetPane(1).CollapseBackward(); $('#hfHapurMbyllur').val('False'); }
    else
        $('#hfHapurMbyllur').val('True');
    $.ajax({
        pritPergjigje: true, url: Utils.getServerApiUrl("Rregjistrime", "ruajHapurMbyllurminus"),
        data: JSON.stringify({ hapur: hapur, idperdorues: pageState.idPerdoruesi, idperdoruesveprimi: pageState.idPerdoruesi })
    }).done(SuccededCallbackHapurMbyllur);
}


function SuccededCallbackHapurMbyllur(result) {
    myMesazh.ShtoMesazhSuksesi(result);
}
function HeaderClick(s, e) {
    if (!mbyll) { e.cancel = true; mbyll = true; } //per rastet kur shtyp butonat + dhe -
}

function myvalueComboMagazina2(elem, operation, value) {
    //if (cmbKonfigurimi.GetText() == "FDT" || cmbKonfigurimi.GetText() == "FDTK") {
    if (hfState.Get("kushtTransferim") === "Po") {
        return myJQGrid.myValueTextBox(elem, operation, value);
    }
    else
        return "";
}

function callWebserviceMagazina() {
    if (cmbKonfigurimi.GetText() != 'FHNV' && cmbKonfigurimi.GetText() != 'FDNV')
        shtoMagazinat(colMagazina2, btneMagazina2, $("input[id$='hfShtimModifikim']").val());
    else {
        try {
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "ktheTemplatetNjesiVartese"),
                data: JSON.stringify({})
            }).done(SucceededCallbackNjesiVartese);
        } catch (e) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
        }
    }
}

var colNjesiVartese;
/*
Function: SucceededCallbackNiveli

Ndryshon vleren e zgjedhur te konfigurimit sipas nivelit te zgjedhur.
Therret funksionin <ndryshoKonfigurimin>.
*/
function SucceededCallbackNjesiVartese(result) {
    colNjesiVartese = result;
    //if ($("input[id$='hfShtimModifikim']").val() == "shtim") {
    //    btneMagazina2.ClearItems();
    //    for (i = 0; i < result.length; i++) {
    //        btneMagazina2.AddItem([result[i].Kodi, result[i].Pershkrimi], result[i].IdNjesiVartese); //AddItem(teksti, vlera);
    //    }
    //}
}

/*
Function: Auto_Click

Hap lupen e automjeteve.
*/
function Auto_Click() {//po
    var hfAuto = $("#hfLupaAutomjet");
    var queryStr = hfAuto.val();
    var klienti = '';
    if (btneKlientFurnitori.GetText() != '') {
        klienti = btneKlientFurnitori.GetValue();
        popupUniversal.SetContentUrl('LupaAutomjeti.aspx?idKonfigAmbjente=' + queryStr);
    }
    else
        popupUniversal.SetContentUrl('LupaAutomjeti.aspx?idKonfigAmbjente=' + queryStr);
    popupUniversal.SetHeaderText('Zgjidh Automjetin');
    popupUniversal.SetSize(widthLupaAutomjet, heightLupaAutomjet);
    popupUniversal.Show();
}

function textChangedAuto(s, e) {
    var idAuto = btneAutomjeti.GetValue();
    if (idAuto == null || idAuto == 0 || idAuto == -1)
        return;
    try {
        $.ajax({ url: Utils.getServerApiUrl("Rregjistrime", "ktheTargeAutomjeti"), data: JSON.stringify({ idAutomjeti: idAuto }) }).done(SucceededCallbackTargeAutomjeti);
        if (btneKlientFurnitori.GetText() == '') {
            try {
                $.ajax({ url: Utils.getServerApiUrl("Rregjistrime", "ktheKlientAutomjeti"), data: JSON.stringify({ idAutomjeti: idAuto }) }).done(SucceededCallbackKlientAutomjeti);
            }
            catch (e) {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiNjeGabimGjateMarrjesSeMonedhes"));
            }
        }
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTargesSeAutomjetit"));
    }
}

//kur ndryshon automjeti ndryshohet dhe targa e tij
function SucceededCallbackTargeAutomjeti(result) {
    if (result != null)
        txtTarga.SetText(result);
}

// Kur ndryshohet automjeti vihet klienti i atij automjeti
function SucceededCallbackKlientAutomjeti(result) {
    if (result != null)
        Utils.SelectComboItem(btneKlientFurnitori, result.IdKlientFurnitor, result.EmertimiKF, result.KodKlientFurnitor);
}

function shtoMagazinat(colMagazina2, btneMagazina2, shtimModifikim) {
    if (shtimModifikim == "shtim" || shtimModifikim == "rezervim" || shtimModifikim == "konvertim") {
        var magDefault, valueMagDefault;
        if (btneMagazina2.GetSelectedItem() != null)
            magDefault = btneMagazina2.GetSelectedItem().value;
        else
            valueMagDefault = btneMagazina2.GetValue();
        //btneMagazina2.ClearItems();
        if (colMagazina2 == undefined)
            return;
        if (magDefault)
            btneMagazina2.SetValue(magDefault);
        if (valueMagDefault)
            btneMagazina2.SetSelectedItem(btneMagazina2.FindItemByValue(valueMagDefault));
        //btneMagazina2.EndUpdate();
    }
}

var panjesi = false;

function ButtonOkQKClick(s, e) {
    //popMesazhQK.Hide();
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgShperndarjeNeQendratEKostos"), $('#hfUrl').val(), 900, 600);
}

function RenditjeCheck(reload) {
    var grida = $('#rowed5');
    var hf = $("input[id$='hfShtimModifikim']");
    if (cbRenditje.GetChecked() && (hf.val() == "modifikim")) {
        lejomod = false;
    }
    else {
        if (vlerakushti == "Po" || vlerakushti == "")
            lejomod = true;
        else lejomod = false;
        cbRenditje.SetEnabled(false);
        if (reload) {
            var hfLidhur = $("input[id$='hfLidhur']");
            grida.setLastSel2(-1);
            grida.jqGrid('GridUnload', "rowed5");

            var isLidhur = (hfLidhur.val().toLowerCase() === 'true');

            inicializoGride(isLidhur);
            mbushGrideNgaHiddenFieldet(isLidhur);
        }
    }
}

function nrLlogariChange(s, e) {
    if (!s)
        return;
    var v = s.GetText();
    if (!v)
        return;
    s.SetText(v.split(';')[0]);
}
function Ngarko() {
    //ucEmerSkedari.Upload();
    hfState.Set("kategoriSeriali", cmbKategoriSeriali.GetText());
}
function LostFocusSerial() {
    if (cmbKategoriSeriali.GetText() != '')
        ucEmerSkedari.SetEnabled(true);
    else {
        ucEmerSkedari.SetEnabled(false);
        ucEmerSkedari.ClearText('');
    }
}
function TextChangeFile() {
    if (ucEmerSkedari.GetText() != "")
        btnNgarko.SetEnabled(true);
    else btnNgarko.SetEnabled(false);
}

function vendosMagazinenNgaLupa(fusha, params){
	var index = $('#rowed5').getLastSel2();//window.parent.lastsel2;
	var idKontrolli = "#" + fusha + index;
	selectFuncMag(null, null, idKontrolli, params.idKodi, params.kodi, fusha);
	$(idKontrolli).focus();
}

function ngarkoKategoriSerialesh() {
    $.ajax({
        pritPergjigje: true,
        method: "GET",
        url: Utils.getServerApiUrl("SerialeUnike", "merrKategoriSeriali")
    }).done(function (res) {
        $.each(res, function (key, value) {
            $('#selKategoriSeriali')
                .append($("<option></option>")
                           .text(value.Kategori));
        });
    });

}

function hapFineUpload() {
    $('#fine-uploader-validation').fineUploader({
        template: 'qq-template-validation',
        request: {
            endpoint: Utils.getServerApiUrl("Rregjistrime", "NgarkoFile"),
            params: {
                scopeID: Utils.getUrlVar("scopeID"),
                lloji: "SkedareSerial"
            }
        },
        thumbnails: {
            placeholders: {
                waitingPath: '/fine-uploader/placeholders/waiting-generic.png',
                notAvailablePath: '/fine-uploader/placeholders/not_available-generic.png'
            }
        },
        paramsInBody: true,
        deleteFile: {
            enabled: true,
            forceConfirm: true,
            endpoint: Utils.getServerApiUrl("Rregjistrime", "FshiFile"),
            params: {
                scopeID: Utils.getUrlVar("scopeID"),
                lloji: "SkedareSerial"
            }
        },
        validation: {
            allowedExtensions: ['csv', 'xls', 'xlsx', 'txt']
        },
        onComplete: function (id) {
            qq(this.getItemByFileId(id)).remove();
        },
        callbacks: {
            onError: function (id, name, errorReason, xhrOrXdr) {
                myMesazh.ShtoMesazhGabimi(qq.format("Error ne ngarkimin e skedarit me numer {} - {}.  Gabimi: {}", id, name, errorReason));
            }
        },
        messages: Utils.getGlobalization(pageState.idGjuha)
    });
}

function hapNgarkimSerialesh() {
    $('#myModal').modal('toggle');

    $('#fine-uploader-validation').unbind().empty();
    $("#selKategoriSeriali").val($("#selKategoriSeriali option:first").val());
    $('#selKategoriSeriali').on('change', function () {
        hapFineUpload();
        var kategoria = $("#selKategoriSeriali option:selected").text();
        if (kategoria == '')
            $('#fine-uploader-validation').hide();
        else
            $('#fine-uploader-validation').show();
    });

    $('#buttonNgarko').unbind().click(function () {
        hfState.Set("kategoriSeriali", $("#selKategoriSeriali option:selected").text());
        serialetCallbackPanel.PerformCallback(JSON.stringify(MerrArtikujDheSasitePerkatese()));
    });
}


function BeginCallback(s, e) {
    Utils.shfaqLoadingGif();
}

function SkedariUploaded(s, e) {
    var mesazhgabimi = s.cpSerialMesazhGabimi;
    if (mesazhgabimi)
        myMesazh.ShtoMesazhGabimi(mesazhgabimi);

    if (s.cpArtikujSeriale && s.cpArtikujSeriale != "" && !(hfState.Get("MNSA")))
        ShtoArtikujPerSerialet(JSON.parse(s.cpArtikujSeriale));
    Utils.hiqLoadingGif();
    delete s.cpArtikujSeriale;
    delete s.cpSerialMesazhGabimi;
}

function SetKaSeriale(arrSerialet, nrSerialesh) {
    if (arrSerialet.length > 0) {
        kaSeriale = true;
    } else if (nrSerialesh != undefined) {
        kaSeriale = nrSerialesh == 0;
    }
}

function ShtoArtikujPerSerialet(arrArtikujt, idArtikujPertuHequr, nrSerialesh) {
    var grida = $(pageState.gridaSelector);
    var idRow = parseInt(grida.gjeRreshtBosh("txtIdKodi"));
    grida.selektoRreshtin(idRow, true);
    var magazine = grida.getTekstQelize('txtMagazina', idRow);
    var magazinatKoka = merrMagazinatKoka();
    SetKaSeriale(arrArtikujt, nrSerialesh);

    if (idArtikujPertuHequr != undefined) {
        for (var i = 0; i < idArtikujPertuHequr.length; i++) {
            var id = grida.gjejIdRreshtiSipasFunksionit(function (rreshti) {
                return rreshti.txtIdKodi == idArtikujPertuHequr[i].IdArtikulli.toString() && rreshti.txtMagazina == idArtikujPertuHequr[i].KodMag;
            });
            if (id)
                fshiClicked(id);

        }
    }
    var artikujt = {};
    var iArt = 0;
    var idRreshti = -1;
    var buttonFshiEnable = arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides - 1] == 'True' || ($("input[id$='hfLidhur']").val().toLowerCase() === 'true') || !lejomod;
    for (var i = 0; i < arrArtikujt.length; i++) {



        var idRreshtiEkzistues = grida.gjejIdRreshtiSipasFunksionit(function (rreshti) {
            return rreshti.txtIdKodi == arrArtikujt[i].IdArtikulli.toString() && (arrArtikujt[i].KodMag == undefined || rreshti.txtMagazina == arrArtikujt[i].KodMag) && Utils.IsNullOrEmpty(rreshti.txtIdArtikullSet);
        });
        if (idRreshtiEkzistues) {
            if (grida.getTekstQelize("txtSasia", idRreshtiEkzistues) != arrArtikujt[i].Sasia) {
                grida.setTekstQelize("txtSasia", idRreshtiEkzistues, arrArtikujt[i].Sasia);
                var arti = memoryArt.Get(arrArtikujt[i].IdArtikulli);
                vendosVleftat("txtSasia", idRreshtiEkzistues, arti);
            }
            continue;
        }
        if (arrArtikujt[i].KodMag) {
            magazinatKoka.magArtKoka = arrArtikujt[i].KodMag;
        }

        var newIdRreshti = grida.gjeRreshtBoshPasKetijRreshti("txtIdKodi", undefined, undefined, idRreshti);
        var be = myJQGrid.myValueButtonFshi(buttonFshiEnable, idRreshti, pageState.gridaSelector);
        if (newIdRreshti == -1) {
            idRreshti = grida.shtoRresht(buttonFshiEnable, pageState.gridaSelector);
        } else {
            idRreshti = newIdRreshti;
        }

        $("#txtCmimi" + idRreshti).data('pending', 1);
        grida.selektoRreshtin(idRreshti, true);
        $("#txtCmimi" + idRreshti).removeData('pending');

        grida.setTekstQelize('txtKategoria', idRreshti, 'Artikull');
        grida.setTekstQelize('txtKodi', idRreshti, ' ');
        grida.setTekstQelize('txtMagazina', idRreshti, arrArtikujt[i].KodMag);
        grida.vendosTeDhenaPerQelizen('txtMagazina', idRreshti, "IshMagazina", arrArtikujt[i].KodMag);
        grida.setTekstQelize("txtSasia", idRreshti, arrArtikujt[i].Sasia);
        grida.setTekstQelize("txtCmimi", idRreshti, "1");
        vendosVleftat("txtSasia", idRreshti, arti);
        artikujt[iArt] = { idja: arrArtikujt[i].IdArtikulli, rreshti: idRreshti, data: dteDtDok.GetDate(), iddetajim: 0, magazina: arrArtikujt[i].KodMag ? arrArtikujt[i].KodMag : magazine, sasiaNeGride: arrArtikujt[i].Sasia, magazinatKoka: magazinatKoka, merrMagMeAutorizim: false, meDetajim: false, magazinaDest: "", idNdermarrje: pageState.idNdermarrje, idPerdoruesi: pageState.idPerdoruesi, njesiDef: njesiDef, sasiaNeRresht: arrArtikujt[i].Sasia };

        iArt++;
    }
    grida.rregulloNrRendor("txtNrRendor");
    callWebserviceMerrArtikujMeID(artikujt);
}

function FshiDokumentMagazine() {
    var guidString = hfState.Get("guidString");
    var komponente = "RegjistrimMagazine.aspx?lloj=" + Utils.getUrlVar("lloj");
    var arrayId = new Array(1);
    arrayId[0] = Utils.getUrlVar("id");
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "FshiDokumentMagazine"),
        data: JSON.stringify({
            ids: arrayId,
            komponente: komponente,
            guidString: guidString,
            periudhaIdViti: 0,
            periudhaDok: ''
        })
    }).done(SuccededCallbackDelete);
}

function SuccededCallbackDelete(result) {
    Utils.hiqLoadingGif();
    if (result.mesazhSukses != "") {
        myMesazh.ShtoMesazhSuksesi(result.mesazhSukses);
        if (result.mesazhRivleresim != "") {
            identifikuesPyetje = "Rivleresimi";
            myMesazh.ShtoPyetje(result.mesazhRivleresim);
        }
        else
            window.location = "RegjistrimMagazine.aspx?lloj=" + Utils.getUrlVar("lloj");
    }
    if (result.mesazhGabim != "")
        myMesazh.ShtoMesazhGabimi(result.mesazhGabim);
    for (var i = 0; i < result.faf.length; i++)
        myMesazh.ShtoMesazhGabimi(result.faf[i]);
    if (result.fafErr)
        window.open("RaportiShpejte.aspx?Sesioni=false&emriReal=gabimeImporti&printo=false&db=jo&scopeID=" + Utils.getUrlVar("scopeID"), '_blank');
}

function Rivleresim() {
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "BejRivleresim"),
        data: JSON.stringify({ guidString: hfState.Get("guidString") })
    }).done(function (result) {
        Utils.hiqLoadingGif();
        myMesazh.ShtoMesazhSesioni(result);
        window.location = "RegjistrimMagazine.aspx?lloj=" + Utils.getUrlVar("lloj");
    });
}


function koloneDisabled() {
    return (hfState.Get("IdStatusDok") == 0 && hfState.Get("LMDET") && $("input[id$='hfShtimModifikim']").val() == 'modifikim' && $("input[id$='hfLidhur']").val().toLowerCase() === 'true');
}

function popUniversalClose(s, e) {
    popupUniversal.SetContentUrl('');
}
/*
Function: ButtonClickTransportues

Hap lupen e transportuesit.
*/
function ButtonClickTransportues() {//po
    identikuesPerPopupTransportuesi = "RegjistrimMagazine";
    var hfTr = document.getElementById("hfLupaTransportues");
    var queryStr = hfTr.value;
    popupUniversal.SetHeaderText(hfState.Get("msgZgjidhTransportuesin"));
    popupUniversal.SetContentUrl('Shto_Transportues.aspx?lupe=true&idKonfigAmbjente=' + queryStr);
    popupUniversal.SetSize(widthLupaTransportues, heightLupaTransportues);
    popupUniversal.Show();
}
function IndexChangedTransportues(s, e) {
    var idtransporti = btnTransportuesi.GetValue();
    if (isNaN(btnTransportuesi.GetValue()) || btnTransportuesi.GetValue() == null || btnTransportuesi.GetSelectedIndex() == -1) {
        btnTransportuesi.SetText('');
        return;
    }

    try {
        $.ajax({ url: Utils.getServerApiUrl("Rregjistrime", "ktheTargeTransportues"), data: JSON.stringify({ idTransportues: idtransporti }) }).done(SucceededCallbackTargeTransportues);

    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTargesSeAutomjetit"));
    }
}
function SucceededCallbackTargeTransportues(result) {
    if (result != null)
        txtTarga2.SetText(result);
}
