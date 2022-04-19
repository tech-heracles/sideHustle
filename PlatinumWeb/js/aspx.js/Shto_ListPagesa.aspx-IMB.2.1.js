;
var pageState = {
    idKokaLp: 0,
    idEtapeAprovimi: 0,
    colKompListPagese: {},
    shtimModifikim: "shtim",
    RimerrVlera: false,
    kaNdryshime: false,
    TrupiDok: [],
    teDhenaPerRuajtje: {
        colKomponente: [],
        rreshtaGride: []
    },
    clearState: function () {
        this.idKokaLp = 0;
        this.idEtapeAprovimi = 0;
        this.colKompListPagese = {};
        this.shtimModifikim = 'shtim';
        this.idGjuha = 0;
        this.TrupiDok = [];
        this.teDhenaPerRuajtje = {
            colKomponente: [],
            rreshtaGride: []
        };
        this.RimerrVlera = false;
    }
};
var lidhur = false;
var lastsel2 = 1;
var arrayIdKolonaGrides = new Array();
var arrayPershkrimiKolonaGrides = new Array();
var arrayVisibleKolonaGrides = new Array();
var arrayWidthKolonaGrides = new Array();
var arrayReadOnlyKolonaGrides = new Array();
var arrayRenditjeKolonaGrides = new Array();
var arrayIndexTrupi = new Array();
var widthLupaPunonjes = 1100;
var heightLupaPunonjes = 600;
var widthLupaDep = 600;
var heightLupaDep = 600;
var widthLupaKerko = 600;
var heightLupaKerko = 600;

var formatNumriZgjedhur;
var formatKursi;

var identikuesPerPopupKursi = "ShtoListPagesa";
var llojkursi = 1; //perdoret per te ruajtur llojin e kursit te zgjedhur tek konfigurimi. Ne qofte se nuk ka asnje lloj te zgjedhur, atehere merret lloji i pare.
var widthLupaKursi = 950;
var heightLupaKursi = 560;
var MonedhaNdermarrje;
var autoSaver;
var ruajAutomatikisht = false;
jQuery(document).ready(function () {
    $(window).on('resize', function () {
        try {
            if (Utils.isGridResized())
                return;
        }
        catch (ee) {
            console.log(ee);
        }
        var grida = $('#rowed5');
        if ($('#divgride2').width() !== null) {
            myJQGrid.fixGridWidth(grida, $('#divgride2'));
        }
    }).trigger('resize');
    $(window).on('load', function () {
        Init();
        Utils.resizeSplitter();
    });
   
    $(document).keydown(function (e) {
        switch (e.which) {
            case 13:
                e.preventDefault();
                break;
            case 116: //F5
                if(window.parent)
                    window.parent.rifresko = true;
                break;
            case 82:
                if (e.ctrlKey && window.parent) //ctrl+r
                    window.parent.rifresko = true;
            default:
                break;
        }
    });
});

function Init() {
    if (typeof (isPostBack) === "undefined") {
        MonedhaNdermarrje = hfState.Get("idMonedhaNdermarrje");
        var hf = $("#hfKonffillestar");
        pageState.idKokaLp = Utils.getNumberOrDefaultFromUrl("id");
        pageState.idEtapeAprovimi = Utils.getNumberOrDefaultFromUrl("idetapa");
        pageState.shtimModifikim = $('#hfShtimModifikim').val();
        pageState.idGjuha = hfState.Get("idGjuha");
        pageState.idNdermarrje = hfState.Get("idNdermarrje");
        pageState.idPerdoruesi = hfState.Get("idPerdoruesi");
        pageState.idViti = hfState.Get("idViti");
        pageState.RimerrVlera = hfState.Get("rimerrVlera");
        pageState.idNdermarrjeVit = hfState.Get("idNdermarrjeVit");
        pageState.idViti = hfState.Get("idViti");
        pageState.idPerdoruesi = hfState.Get("idPerdoruesi");
        ndryshoKonfigurimin();
        identifikuesPerPopupDokumentat = "ListPagesa";
        changeName();
        myMesazh.shtoHandler();
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(EndRequestHandler);
        autoSaver = new AutoSaverManager(RuajAutomatikisht, ["Draft"], this);
    }
}

function spliterPaneCollapsed(s, e) {
    var grida = $("#rowed5");
    if ($('#divgride2').width() != null) {
        myJQGrid.fixGridWidth(grida, $('#divgride2'));
    }
}

/*
Function: inicializoGride

Inicializon griden e trupit. Konfiguron kolonat e grides dhe percakton veprimin qe kryhet onCellSelect.
*/
function inicializoGride() {
    var classes = '';
    if (lidhur === true && pageState.shtimModifikim === 'modifikim')
        classes = 'uigray';
    var arrayPershkrime = [arrayPershkrimiKolonaGrides[0], arrayPershkrimiKolonaGrides[1], arrayPershkrimiKolonaGrides[2], arrayPershkrimiKolonaGrides[3], arrayPershkrimiKolonaGrides[4], arrayPershkrimiKolonaGrides[5], arrayPershkrimiKolonaGrides[6], arrayPershkrimiKolonaGrides[7], arrayPershkrimiKolonaGrides[8], arrayPershkrimiKolonaGrides[9], arrayPershkrimiKolonaGrides[10], arrayPershkrimiKolonaGrides[11], arrayPershkrimiKolonaGrides[12], arrayPershkrimiKolonaGrides[13], arrayPershkrimiKolonaGrides[14], arrayPershkrimiKolonaGrides[15], arrayPershkrimiKolonaGrides[16], arrayPershkrimiKolonaGrides[17], arrayPershkrimiKolonaGrides[18], arrayPershkrimiKolonaGrides[19], arrayPershkrimiKolonaGrides[20], arrayPershkrimiKolonaGrides[21], arrayPershkrimiKolonaGrides[22], arrayPershkrimiKolonaGrides[23], arrayPershkrimiKolonaGrides[24], arrayPershkrimiKolonaGrides[25]];
    var arrayModel = [
        { name: arrayIdKolonaGrides[0], index: arrayIdKolonaGrides[0], width: arrayWidthKolonaGrides[0], hidden: arrayVisibleKolonaGrides[0], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemNrRendor, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[1], index: arrayIdKolonaGrides[1], width: arrayWidthKolonaGrides[1], hidden: arrayVisibleKolonaGrides[1], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemNrPersonal, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[2], index: arrayIdKolonaGrides[2], width: arrayWidthKolonaGrides[2], hidden: arrayVisibleKolonaGrides[2], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemPunonjes, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[3], index: arrayIdKolonaGrides[3], width: arrayWidthKolonaGrides[3], hidden: arrayVisibleKolonaGrides[3], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemPP, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[4], index: arrayIdKolonaGrides[4], width: arrayWidthKolonaGrides[4], hidden: arrayVisibleKolonaGrides[4], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemPR, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[5], index: arrayIdKolonaGrides[5], width: arrayWidthKolonaGrides[5], hidden: arrayVisibleKolonaGrides[5], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemOD, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[6], index: arrayIdKolonaGrides[6], width: arrayWidthKolonaGrides[6], hidden: arrayVisibleKolonaGrides[6], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemDM, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[7], index: arrayIdKolonaGrides[7], width: arrayWidthKolonaGrides[7], hidden: arrayVisibleKolonaGrides[7], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemPJ, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[8], index: arrayIdKolonaGrides[8], width: arrayWidthKolonaGrides[8], hidden: arrayVisibleKolonaGrides[8], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemTP, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[9], index: arrayIdKolonaGrides[9], width: arrayWidthKolonaGrides[9], hidden: arrayVisibleKolonaGrides[9], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemSP, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[10], index: arrayIdKolonaGrides[10], width: arrayWidthKolonaGrides[10], hidden: arrayVisibleKolonaGrides[10], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemSN, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[11], index: arrayIdKolonaGrides[11], width: arrayWidthKolonaGrides[11], hidden: arrayVisibleKolonaGrides[11], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemSPS, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[12], index: arrayIdKolonaGrides[12], width: arrayWidthKolonaGrides[12], hidden: arrayVisibleKolonaGrides[12], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemSPD, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[13], index: arrayIdKolonaGrides[13], width: arrayWidthKolonaGrides[13], hidden: arrayVisibleKolonaGrides[13], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemSNS, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[14], index: arrayIdKolonaGrides[14], width: arrayWidthKolonaGrides[14], hidden: arrayVisibleKolonaGrides[14], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemSND, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[15], index: arrayIdKolonaGrides[15], width: arrayWidthKolonaGrides[15], hidden: arrayVisibleKolonaGrides[15], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemST, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[16], index: arrayIdKolonaGrides[16], width: arrayWidthKolonaGrides[16], hidden: arrayVisibleKolonaGrides[16], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemNT, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[17], index: arrayIdKolonaGrides[17], width: arrayWidthKolonaGrides[17], hidden: arrayVisibleKolonaGrides[17], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemPT, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[18], index: arrayIdKolonaGrides[18], width: arrayWidthKolonaGrides[18], hidden: arrayVisibleKolonaGrides[18], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemPPS, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[19], index: arrayIdKolonaGrides[19], width: arrayWidthKolonaGrides[19], hidden: arrayVisibleKolonaGrides[19], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemPRN, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[20], index: arrayIdKolonaGrides[20], width: arrayWidthKolonaGrides[20], hidden: arrayVisibleKolonaGrides[20], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemPaguar, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[21], index: arrayIdKolonaGrides[21], width: arrayWidthKolonaGrides[21], hidden: arrayVisibleKolonaGrides[21], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemShenime, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[22], index: arrayIdKolonaGrides[22], width: arrayWidthKolonaGrides[22], hidden: arrayVisibleKolonaGrides[22], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemDep, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[23], index: arrayIdKolonaGrides[23], width: arrayWidthKolonaGrides[23], hidden: arrayVisibleKolonaGrides[23], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemCost, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[24], index: arrayIdKolonaGrides[24], width: arrayWidthKolonaGrides[24], hidden: arrayVisibleKolonaGrides[24], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemPagaShtesa, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[25], index: arrayIdKolonaGrides[25], width: arrayWidthKolonaGrides[25], hidden: arrayVisibleKolonaGrides[25], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemButtonFshi, custom_value: myValueButtonFshi }, hidedlg: true }
    ];

    if ($("input[id$='hfLidhur']")[0].value === 'True')
        lidhur = true;
    else
        lidhur = false;
    if (aprovim)
        lidhur = true;
    var selektoriGrides = "#rowed5";

    var gridParams = {
        emergride: selektoriGrides,
        arrayPershkrime: arrayPershkrime,
        lidhur: lidhur,
        arrayModel: arrayModel,
        widthi: $('#divgride2').width() - 5,
        subgrid: false,
        doubleclickfunction: merrIdPunonjes,
        lostFocusKoloneFundit: lostFocusKoloneFundit,
        konfigToolbar: {
            konfigGrid: $('#hfTeDrejtaKonfGride').val(),
            ruajKolonatEGrides: ruajKolonatEGrides,
            exportExcel: true,        //Ben enable exportin e F
            EmerExporti: "ListePagesa", //Emri i filet .xls qe gjenerohet
            FormateText: ["txtNrPersonal"]
        }
    };
    return myJQGrid.initGride(gridParams);
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

/*
Vendos vlerat default te fushave ne gride ne baze te emrit te kolones
*/
function vendosVleraDefaultNeGride(grida) {
    grida.setVlereDefault('txtPP', 0);
    grida.setVlereDefault('txtPR', 0);
    grida.setVlereDefault('txtOD', 0);
    grida.setVlereDefault('txtDM', 0);
    grida.setVlereDefault('txtPJ', 0);
    grida.setVlereDefault('txtTP', 0);
    grida.setVlereDefault('txtSP', 0);
    grida.setVlereDefault('txtSN', 0);
    grida.setVlereDefault('txtSPS', 0);
    grida.setVlereDefault('txtSPD', 0);
    grida.setVlereDefault('txtSNS', 0);
    grida.setVlereDefault('txtSND', 0);
    grida.setVlereDefault('txtST', 0);
    grida.setVlereDefault('txtNT', 0);
    grida.setVlereDefault('txtPT', 0);
    grida.setVlereDefault('txtPPS', 0);
    grida.setVlereDefault('txtPRN', 0);
    grida.setVlereDefault('txtPaguar', 0);
    grida.setVlereDefault('txtCosto', 0);
    grida.setVlereDefault('txtPagaShtesa', 0);
    return;
}


function ndryshoKonfigFormatNumri(grida, formatNumri, formatkursi) {
    ///<summary> metode per ndryshimin e formateve te nr ne gride ne baze te emrit te kolones. gjithashtu vendos formatin e nr dhe per fushat e devexpresit ne baze te emrit te kontrollit</summary>
    ///<param name="formatNumri"> objekt i tipit format nr me te dhenat e formatimit</param>
    ///<param name="formatkursi">sherben per te vendosur formatin e kursit ne varesi te monedhes </param>
    if (typeof formatNumri == 'undefined') {
        formatNumri = $.parseJSON(hfState.Get("formatMonedhe"));
        formatNumriZgjedhur = formatNumri;
    }
    else
        hfState.Set("formatMonedhe", JSON.stringify(formatNumri));

    grida.setShifraPasPresjes('txtPP', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtPR', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtOD', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtDM', formatNumri.ShifraPasPresjesSasia);
    grida.setShifraPasPresjes('txtPJ', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtTP', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtSP', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtSN', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtSPS', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtSPD', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtSNS', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtSND', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtST', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtNT', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtPT', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtPPS', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtPRN', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtPaguar', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtCosto', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtPagaShtesa', formatNumri.ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtVlefta, formatNumri.ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtVlefta2, formatNumri.ShifraPasPresjesVlefta);
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
        grida.formatoQelize('txtPP', idRreshti);
        grida.formatoQelize('txtPR', idRreshti);
        grida.formatoQelize('txtOD', idRreshti);
        grida.formatoQelize('txtDM', idRreshti);
        grida.formatoQelize('txtPJ', idRreshti);
        grida.formatoQelize('txtTP', idRreshti);
        grida.formatoQelize('txtSP', idRreshti);
        grida.formatoQelize('txtSN', idRreshti);
        grida.formatoQelize('txtSPS', idRreshti);
        grida.formatoQelize('txtSPD', idRreshti);
        grida.formatoQelize('txtSNS', idRreshti);
        grida.formatoQelize('txtSND', idRreshti);
        grida.formatoQelize('txtST', idRreshti);
        grida.formatoQelize('txtNT', idRreshti);
        grida.formatoQelize('txtPT', idRreshti);
        grida.formatoQelize('txtPPS', idRreshti);
        grida.formatoQelize('txtPRN', idRreshti);
        grida.formatoQelize('txtPaguar', idRreshti);
        grida.formatoQelize('txtCosto', idRreshti);
        grida.formatoQelize('txtPagaShtesa', idRreshti);
    }
    formatoFushaDevi();
}

function formatoFushaDevi() {
    Utils.formatoTextBox(txtVlefta);
    Utils.formatoTextBox(txtVlefta2);
    Utils.formatoTextBox(txtKursi);
}

function unformatoFushaDevi() {
    Utils.unFormatoTextBox(txtVlefta);
    Utils.unFormatoTextBox(txtVlefta2);
    Utils.unFormatoTextBox(txtKursi);
}

function ruajKolonatEGrides(grida) {
    var idGride = colGrida[0].IdKoka;
    myJQGrid.ruajKolonatEGrides(grida, idGride, pageState.idGjuha, pageState.idNdermarrje, pageState.idViti, pageState.idPerdoruesi)
}

function formGridColsArray() {//po                       
    var hfGridKod = $('#hfGridaKodi');
    var IdKonfigAmbjenteLupat = [{ hfVar: hfGridKod, kodiText: "txtNrPersonal" }];
    myJQGrid.formArrayKolGrides($("#HfGridCol"), arrayIdKolonaGrides, arrayPershkrimiKolonaGrides, arrayVisibleKolonaGrides, arrayReadOnlyKolonaGrides, lidhur, arrayWidthKolonaGrides, IdKonfigAmbjenteLupat, arrayRenditjeKolonaGrides);
}
function dateGotFocus(s, e) {
    window.setTimeout(function () { s.SetCaretPosition(0); }, 0);
}

/*
Function: renditKolonatGrides

Therret metoden remapColumns te jqGrid per te renditur kolonat e grides sipas vlerave te array-t qe i kalohet kesaj metode si parameter
*/
function renditKolonatGrides() {
    var grida = jQuery("#rowed5");
    grida.remapColumns(arrayRenditjeKolonaGrides);
}

/*
Function: myElemKodi

Nderton nje textbox dhe nje buton per te zgjedhur artikullin ose makron sipas llojit te zgjedhur tek combo Kategoria
*/
function myElemNrPersonal(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemNrPersonal(value, options, arrayReadOnlyKolonaGrides[1], idRresht, arrayIdKolonaGrides[1], ButtonClickKodi, false);
}

function myValueButtonFshi(elem, operation, value) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1] === 'True')
        return myJQGrid.myValueButtonFshi(true, idRresht, "#rowed5");
    else
        return myJQGrid.myValueButtonFshi(false, idRresht, "#rowed5");
}

/*
Function: myElemButon

Nderton nje buton per te fshire nje rresht te grides
*/
function myElemButtonFshi() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1] === 'True')
        return myJQGrid.myElemButtonFshi(true, idRresht, "#rowed5", lostFocusKoloneFundit);
    else
        return myJQGrid.myElemButtonFshi(false, idRresht, "#rowed5", lostFocusKoloneFundit);
}
function pershkrimi() {
    var grida = jQuery("#rowed5");
    var per = txtShenime.GetText();

    var rreshtaTeGrides = grida.getDataIDs();
    for (var i = 0; i < rreshtaTeGrides.length; i++) {

        grida.setTekstQelize('txtShenime', rreshtaTeGrides[i], per);

    }

}
/*
Function: myElemEmertimi

Nderton nje textbox ku vendoset emertimi i artikullit apo makros
*/
function myElemPunonjes(value) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemEmertimi(value, arrayReadOnlyKolonaGrides[2], idRresht, 'txtPunonjes');
}
function myElemShenime(value) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if (value == "")
        value = txtShenime.GetText();
    return myJQGrid.myElemEmertimi(value, arrayReadOnlyKolonaGrides[21], idRresht, 'txtShenime');
}
function myElemDep(value) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemEmertimi(value, arrayReadOnlyKolonaGrides[22], idRresht, 'txtDep');
}
function myElemPP(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, arrayReadOnlyKolonaGrides[3], idRresht, 'txtPP', changedFusha, undefined, changedFusha);
}

function myElemPR(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, arrayReadOnlyKolonaGrides[4], idRresht, 'txtPR', changedFusha, undefined, changedFusha);
}

function myElemOD(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, arrayReadOnlyKolonaGrides[5], idRresht, 'txtOD', changedFusha, undefined, changedFusha);
}

function myElemDM(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, arrayReadOnlyKolonaGrides[6], idRresht, 'txtDM', changedFusha, undefined, changedFusha);
}

function myElemPJ(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, arrayReadOnlyKolonaGrides[7], idRresht, 'txtPJ', changedFusha, undefined, changedFusha);
}

function myElemTP(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, arrayReadOnlyKolonaGrides[8], idRresht, 'txtTP', changedFusha, undefined, changedFusha);
}

function myElemSP(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, arrayReadOnlyKolonaGrides[9], idRresht, 'txtSP', changedFusha, undefined, changedFusha);
}

function myElemSN(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, arrayReadOnlyKolonaGrides[10], idRresht, 'txtSN', changedFusha, undefined, changedFusha);
}

function myElemSPS(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, arrayReadOnlyKolonaGrides[11], idRresht, 'txtSPS', changedFusha, undefined, changedFusha);
}

function myElemSPD(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, arrayReadOnlyKolonaGrides[12], idRresht, 'txtSPD', changedFusha, undefined, changedFusha);
}

function myElemSNS(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, arrayReadOnlyKolonaGrides[13], idRresht, 'txtSNS', changedFusha, undefined, changedFusha);
}

function myElemSND(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, arrayReadOnlyKolonaGrides[14], idRresht, 'txtSND', changedFusha, undefined, changedFusha);
}

function myElemST(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, arrayReadOnlyKolonaGrides[15], idRresht, 'txtST', changedFusha, undefined, changedFusha);
}

function myElemNT(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, arrayReadOnlyKolonaGrides[16], idRresht, 'txtNT', changedFusha, undefined, changedFusha);
}

function myElemPT(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, arrayReadOnlyKolonaGrides[17], idRresht, 'txtPT', changedFusha, undefined, changedFusha);
}

function myElemPPS(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, arrayReadOnlyKolonaGrides[18], idRresht, 'txtPPS', changedFusha, undefined, changedFusha);
}

function myElemPRN(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, arrayReadOnlyKolonaGrides[19], idRresht, 'txtPRN', changedFusha, undefined, changedFusha);
}

function myElemPaguar(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, arrayReadOnlyKolonaGrides[20], idRresht, 'txtPaguar', changedFusha, undefined, changedFusha);
}
function myElemCost(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, arrayReadOnlyKolonaGrides[23], idRresht, 'txtCosto', changedFusha, undefined, changedFusha);
}
function myElemPagaShtesa(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, arrayReadOnlyKolonaGrides[24], idRresht, 'txtPagaShtesa', changedFusha, undefined, changedFusha);
}

/*
Function: myElemNrRendor

Nderton nje textbox per te vendosur nr rendor te rreshtit.
*/
function myElemNrRendor(value, options) {//po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[0];
    return myJQGrid.myElemNrRendor(value, options, idRresht, 'txtNrRendor', grida);
}

function changedFusha() {

}

/*
Function: fshiClicked

Fshin nje rresht te grides

Parameters:

index - Id e rreshtit qe do fshihet
*/
var rreshtiqepofshihet;
var identifikuesPyetje = "";
function fshiClicked(index, e) {
    var grida = $("#rowed5");
    rreshtiqepofshihet = index;
    if (grida.getTekstQelize("txtPunonjes", index) != "") {
        identifikuesPyetje = "FshiPunonjes";
        myMesazh.ShtoPyetje(hfState.Get("msgFshiniPunonjesin") + " " + grida.getTekstQelize("txtPunonjes", index) + "?")
        e.processOnServer = false;
    }
    else fshiClickedPo();
    e.preventDefault();
    return false;
}
function fshiClickedPo() {

    var index = rreshtiqepofshihet;
    var grida = $("#rowed5");
    var ids = grida.getDataIDs();
    var txtNrRendor = undefined;

    for (var i = 0; i < ids.length; i++) {

        if (ids[i] <= index)
            continue;
        txtNrRendor = grida.getTekstQelize('txtNrRendor', ids[i]);
        if (txtNrRendor == undefined || txtNrRendor == '')
            continue;
        grida.setTekstQelize('txtNrRendor', ids[i], parseInt(txtNrRendor) - 1);
    }
    var nrPersonal = grida.getTekstQelize("txtNrPersonal", index);
    $.ajax({
        url: Utils.getServerApiUrl("ListPagesa", "FshiKomponenteVleraTePunonjesit"),
        data: JSON.stringify({ nrPersonal: nrPersonal, idNdermarrje : pageState.idNdermarrje})
    }).done(function (result) {
        console.log(result);
    });

    myJQGrid.fshiClicked(index, '#rowed5', inicializoGride);
    delete pageState.colKompListPagese[nrPersonal];
    vendosTotalet();
}

/*
Function: lostFocusKoloneFundit

Percakton veprimin qe kryhet kur heqim fokusin nga kolona e fundit e gride (ruhet rreshti korent dhe shtohet nje rresht i ri bosh i editueshem).
Ketu kolona e fundit eshte "Vlefta" (sepse eshte rasti kur nuk po behet transferim).
*/
function lostFocusKoloneFundit() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    idRresht = grida.lostFocusKoloneFundit();
}

function MbushTrupDokumenti() {
    var grida = $('#rowed5');
    grida.setLastSel2(1);
    grida.jqGrid('clearGridData');

    var rreshtat = new Array();
    var idRreshti = 1;
    var isReadOnlyKolonaFshi = arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides - 1] === 'True' || lidhur ? 'True' : 'False';
   var  colTrupi = pageState.TrupiDok;
    for (var i = 0; i < colTrupi.length; i++) {

        var colFillestare = colTrupi[i].ColKomponente;
        var infoPunonjesi = {
            txtNrPersonal: colTrupi[i].NrPersonal,
            txtPunonjes: colTrupi[i].Emri,
            txtPaguar: colTrupi[i].Paguar,
            txtShenime: colTrupi[i].Shenime,
            txtDep: colTrupi[i].Departamenti,
            txtCosto: colTrupi[i].Cost,
            name: idRreshti,
            id: idRreshti,
            txtNrRendor: idRreshti,
            txtFshi: myJQGrid.myValueButtonFshi(isReadOnlyKolonaFshi, idRreshti, "#rowed5")
        };
        var rreshtiLlogaritur = ListPagesaUtils.KrijoRreshtMeVleraSipasKompListpagese(colFillestare);
        var rreshtiPerGride = $.extend(true, infoPunonjesi, rreshtiLlogaritur);

        rreshtat.push(rreshtiPerGride);
        pageState.colKompListPagese[rreshtiPerGride.txtNrPersonal] = colFillestare;
        idRreshti++;
    }
    grida[0].addJSONData(rreshtat);
    grida.setLastSel2(-1);
    vendosTotalet();
}

function mbushGrideNgaHiddenFieldet() {
    switch (pageState.shtimModifikim) {
        case "shtim":
            break;
        case "klonim":
            callWsMerrTrupDokumenti(false);
            break;
        case "modifikim":
            callWsMerrTrupDokumenti(false);
            break;
        default:
            console.error("Nuk duhet te mbushet trupi!");
    }
}



function callWsMerrTrupDokumenti(ndryshimMuajiCombo) {
 
    if (pageState.shtimModifikim == "klonim")
       var  hiqPunonjesTelarguar = true;
    else hiqPunonjesTelarguar = false;

    if (pageState.idKokaLp != 0)
        $.ajax({
        pritPergjigje: true,
        showLoading: true,
        data: JSON.stringify({
            idkoka: pageState.idKokaLp,
            idGjuha: pageState.idGjuha,
            muaji: cmbMuaji.GetValue(),
            rimerrVlera: pageState.RimerrVlera,
            llogaritDiteLejeNgaImporti: llogaritDiteLejeNgaImporti,
            hiqPunonjesTelarguar: hiqPunonjesTelarguar,
            cmbData: new Date(dteDtDok.GetDate())
        }),
        url: Utils.getServerApiUrl("ListPagesa", "MerrTrupDokumentiListpagese")
        }).done(function (result) {
            doneCallbackMerrTrupDokumenti(result, ndryshimMuajiCombo);
    });
}
function doneCallbackMerrTrupDokumenti(result, ndryshimMuajiCombo ) {

    pageState.TrupiDok = result.TrupiDok;
    if (pageState.RimerrVlera) {
        var colFillestare;
        for (var i = 0; i < pageState.TrupiDok.length; i++) {
            colFillestare = pageState.TrupiDok[i].ColKomponente;
            pageState.colKompListPagese[pageState.TrupiDok[i].NrPersonal] = $.extend(true, [], colFillestare);
        }
        doneCallbackLlogaritPagenNew(result.TeRillogaritura);
        return;
    }
    if (ndryshimMuajiCombo == false && result.TrupiDok.length > 0) { 
            MbushTrupDokumenti();
            return;
        }

    else {
            if  (result.TrupiDok.length > 0)
                RimerrVlera(ditemuajindryshueshme, false, true);
        
        else
                    {
                    var grida = $("#rowed5");
                    grida.jqGrid('clearGridData');
                    $(grida).shtoRreshtinEpare(arrayReadOnlyKolonaGrides, lostFocusKoloneFundit, grida);
                  }
    }

   
  
}


function doneCallbackKaTeDhena(result) {
    if (result) {
        RimerrVlera(false, false, false);
    }
}
/*
Function: ndryshoKonfigurimin
	
Therret funksionin <callWebserviceKonfigurimi> per te vendosur nje konfigurim te ri.
*/
function ndryshoKonfigurimin() {
    var pershkKonfigAmb = cmbKonfigurimi.GetSelectedItem().GetColumnText("PershkrimKonfigAmbjente");
    if (pershkKonfigAmb != undefined) {
        lblKonfigurimi.SetText(pershkKonfigAmb);
    }
    callWebserviceKonfigurimi();
}

function SelectedIndexChangedMonedha() {
    if (cmbMonedha.GetText() !== "")
        callWebserviceKursi(cmbMonedha.GetValue());
}

function callWebserviceKursi(name) {
    if (name !== null)
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "merrKursiSipasMonedhesDatesDheLlojit"),
            data: JSON.stringify({ idMonedha: name, date: dteDtDok.GetDate(), lloji: llojkursi })
        }).done(SucceededCallbackKursi);
}

var kursifundit = 1;
function SucceededCallbackKursi(result) {

    if (result === "0") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimKursiMonedha"));
        txtKursi.SetText("1");
        kursifundit = 1;
    }
    else {
        txtKursi.SetText(result);
        kursifundit = result;
    }
    vendosTotalet();
    enableKursi();
}

function kontrolloKurs(s, e) {//po
    if (parseFloat(txtKursi.GetText()) === 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKursiNukMundTeJeteZero"));
        txtKursi.SetText(1);
    }
    var diferenca = Math.abs(kursifundit - parseFloat(txtKursi.GetText()));
    if (diferenca / kursifundit > 0.2)
        myMesazh.ShtoMesazhInformues(hfState.Get("msgKursiRiNdryshonShumeMeKursinMePare"));
}

function enableKursi() {
    if (cmbMonedha.GetValue() !== MonedhaNdermarrje.toString()) {
        var kursi = txtKursi.GetText();
        txtKursi.SetEnabled(true);

        if (isNaN(kursi)) {
            txtKursi.SetFocus();
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgKursiDuhetNumer"));
            txtKursi.SetText("1");
        }
    }
    else {
        txtKursi.SetText("1");
        txtKursi.SetEnabled(false);
    }
}
var identifikuesPerPopupDokumentat;
function changeName() {
    myFaqeCelje.shtoHandlerSession();
    try {
        if (window.parent) {
            if (Utils.getNumberOrDefaultFromUrl('id') == 0)
                window.parent.callWebServiceKtheInfoLart('Shto_ListPagesa.aspx', 0);
            else
                window.parent.callWebServiceKtheInfoLart('Shto_ListPagesa.aspx', Utils.getNumberOrDefaultFromUrl('id'));
            window.parent.createCookie('adresa', window.location.href, 1);
        }
    } catch (e) { console.log(e) }
    myCookies.createCookie('adresa', window.location.href, 1);
}

function vendosNrAutomatik(colAtrTrupi, colKontrollet) {
    myNrAuto.vendosNrAutomatik(colAtrTrupi, colKontrollet, dteDtDok.GetDate());
}

/*
Function: ButtonClickKerko
	
Hap lupen e dokumentave.
*/
function ButtonClickKerko(listUrl) {
    var queryString = {
        veprimi: 'ListPagesa',
        listUrl: listUrl
    };
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhDokumentin"), 'LupaDokumenta.aspx?' + Utils.KonvertoObjectQueryString(queryString), widthLupaKerko, heightLupaKerko);
}

/*
Function: callWebserviceKonfigurimi
	
Therret funksionin <ktheKonfigurim> per te vendosur konfigurimin per dokumentin.
Shiko funksionin <SucceededCallbackKonfigurimi>.
*/
function callWebserviceKonfigurimi() {
 
    var kodKonf = cmbKonfigurimi.GetSelectedItem().GetColumnText("KodKonfigAmbjente")
    if (pageState.shtimModifikim === "shtim")
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Konfigurime", "ktheKonfigAmbjentiMeFormatNumrash"),
            data: JSON.stringify({
                idKomp: 710, kodKonf: kodKonf, kodKontrolli: "cmbMonedha", idObjekti: -1, shtim: true, merrFormatKursi: true, merrGjitheKonf: false,
                idGjuha: pageState.idGjuha, idPerdoruesi: pageState.idPerdoruesi, idNdermarrje: pageState.idNdermarrje
            })
        }).done(SucceededCallbackKonfig);
    else {
        var idMon = cmbMonedha.GetValue() == null ? -1 : cmbMonedha.GetValue();
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Konfigurime", "ktheKonfigAmbjentiMeFormatNumrash"),
            data: JSON.stringify({
                idKomp: 710, kodKonf: kodKonf, kodKontrolli: "cmbMonedha", idObjekti: idMon, shtim: false, merrFormatKursi: true, merrGjitheKonf: false,
                idGjuha: pageState.idGjuha, idPerdoruesi: pageState.idPerdoruesi, idNdermarrje: pageState.idNdermarrje
            })
        }).done(SucceededCallbackKonfig);
    }
}

function DateChanged(s, e) {
    var hfKontrollet = JSON.parse($('#hfKontrolletNrAutom').val());
    var atributet = JSON.parse($('#hfAtributeNrAutom').val());
    vendosNrAutomatik(atributet, hfKontrollet);
    callWebserviceKursi(cmbMonedha.GetValue());
}

var colKushte;
var colAlterKusht;
var colGrida;
var aprovim = false;
var ditemuajindryshueshme = false;
var shfaqlupemuaji = false;
var mesazhnjepernje = true;
var llogaritDiteLejeNgaImporti = true;
var mosPastroPasRuajtjes = false;

function SucceededCallbackKonfig(result) {

    var hf = $("input[id$='hfShtimModifikim']");
    vendosDateDefault(hf.val());
    var hfLidhur = $("input[id$='hfLidhur']");
    var arrTabela = ['tblFillim', "tblFund"];
    var arrPrind = ["dvFillim", "dvFundi"];
    var colKontrollet = result.colKontroll; //[0];
    var colAtrTrupi = result.colAtrTrupi; //[1];
    formatNumriZgjedhur = result.formatNumri; //[7];
    formatKursi = result.formatKursi; //[8];

    aprovim = false;
    if (hf.val() != 'klonim' && (($('#hfTeDrejtaModSkema').val() == "False" && hfState.Get("StatusAprovimi") == 'Per Aprovim') || (hfState.Get("StatusAprovimi") == 'Aprovuar' || hfState.Get("StatusAprovimi") == 'Refuzuar'))) {
        aprovim = true;
    }
    myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, undefined, hf, '', arrTabela, undefined, undefined, hfLidhur, arrPrind, aprovim);
    $("#divgride1").show();
    $("#dvFillim").show();
    $("#dvFundi").show();
    var colKontrollet = result.colKontroll; //[0];
    var colAtrTrupi = result.colAtrTrupi; //[1];
    colGrida = result.colGrida; //[2];
    colKushte = result.colKushte; //[3];
    colAlterKusht = result.colAlterKusht; //[4];
    $('#HfGridCol').val(JSON.stringify(colGrida));
    var hfKl = $("#hfLupaDepartament");
    var hfMag = $("#hfLupaNenDepartament");
    $('#hfKontrolletNrAutom').val(JSON.stringify(colKontrollet));
    $('#hfAtributeNrAutom').val(JSON.stringify(colAtrTrupi));
    if (pageState.shtimModifikim === "shtim" || pageState.shtimModifikim === "klonim") {
        hfNrAuto.Clear();
        hfNrAutoShitje.Clear();
        vendosNrAutomatik(colAtrTrupi, colKontrollet);
        callWebserviceKursi(cmbMonedha.GetValue());
    }
    if (cmbDepartamenti.GetSelectedIndex() !== -1)
        cmbNenDepartamenti.SetEnabled(true);
    enableKursi();
    for (var i = 0; i < colKontrollet.length - 1; i++) {
        if (colKontrollet[i].KodKontrolli == "txtKursi") {
            if (colAtrTrupi[i].VlereDefault !== "")
                llojkursi = colAtrTrupi[i].VlereDefault;
            else
                llojkursi = 1;
            continue;
        }

        //id e konfigurimit te lupes vendoset ne hidden field
        //vlera e saj do t'i kalohet si query string hapjes se popUp-it
        if (colKontrollet[i].KodKontrolli === "cmbDepartamenti") {

            hfKl.val(colAtrTrupi[i].IdKonfigAmbjenteLupa.toString()); //merret id e konfigurimit te lupes per lupen e klient furnitorit ne forme
        } else if (colKontrollet[i].KodKontrolli === "cmbNenDepartamenti")
            hfMag.val(colAtrTrupi[i].IdKonfigAmbjenteLupa.toString()); //merret id e konfigurimit te lupes per lupen e magazines ne forme
    }

    var hidField1 = $("#hfKontabilizimi");
    hidField1.val(0);
    $('#hfSkema').val('0');
    ditemuajindryshueshme = false;
    llogaritDiteLejeNgaImporti = true;
    mesazhnjepernje = true;
    for (j = 0; j < colKushte.length; j++) {
        if (colKushte[j].Kodi === 'GJK') {
            if (colAlterKusht[j].Alternativa === 'Jo') {
                hidField1.val(0);
            } else if (colAlterKusht[j].Alternativa === "Direkt")
                hidField1.val(1);
            else
                hidField1.val(2);
        }
        if (colKushte[j].Kodi === 'DPMN') {
            if (colAlterKusht[j].Alternativa === 'Jo') {
                ditemuajindryshueshme = false;
            } else
                ditemuajindryshueshme = true;
        }
        if (colKushte[j].Kodi === 'LLDLNI') {
            if (colAlterKusht[j].Alternativa === 'Jo') {
                llogaritDiteLejeNgaImporti = false;
            } else
                llogaritDiteLejeNgaImporti = true;
        }
        if (colKushte[j].Kodi === 'MEPNJPNJ') {
            if (colAlterKusht[j].Alternativa === 'Jo') {
                mesazhnjepernje = false;
            } else
                mesazhnjepernje = true;
        }
        if (colKushte[j].Kodi === 'AMK') {
            if (colAlterKusht[j].Alternativa === 'Jo') {
                shfaqlupemuaji = false;
            } else
                shfaqlupemuaji = true;
        }
        if (colKushte[j].Kodi == 'MPPRLP') {
            if (colAlterKusht[j].Alternativa === 'Jo') {
                mosPastroPasRuajtjes = false;
            } else
                mosPastroPasRuajtjes = true;
        }
        if (colKushte[j].Kodi == "RAD")
            ruajAutomatikisht = colAlterKusht[j].Alternativa === 'PO';
        if (colKushte[j].Kodi == 'ZSP') {
            $('#hfSkema').val(colKushte[j].Vlera);
            lblStatusApr.SetVisible(false);
            lblStatusAprovimi.SetVisible(false);

            var isNotModifikim = (pageState.shtimModifikim == 'modifikim' ? false : true);

            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "MerrMenuPerPerdoruesSipasSkemes"),
                data: JSON.stringify({ idskema: colKushte[j].Vlera, idperdoruesi: $('#hfPerdoruesi').val(), isNotModifikim: isNotModifikim, status: hfState.Get("StatusAprovimi"), idkokashitje: pageState.idKokaLp, kodkonf: cmbKonfigurimi.GetText(), idlloji: 38 })
            }).done(SuccedcallbackSkemaMenu);
            if (colKushte[j].Vlera != 0) {
                if (pageState.shtimModifikim == 'modifikim') {
                    lblStatusApr.SetVisible(true);
                    lblStatusAprovimi.SetVisible(true);
                }
            }
        }
    }

    var grida = $('#rowed5');
    grida.setLastSel2(-1);
    grida.GridUnload("rowed5");
    grida = $('#rowed5');
    formGridColsArray();
    ruajFormatetNeGride(grida, formatNumriZgjedhur, formatKursi);
    inicializoGride();
    setTimeout(function () {
        mbushGrideNgaHiddenFieldet();
    });
}

function SuccedcallbackSkemaMenu(result) {
    ASPxMenu1.GetItemByName('Ruaj').SetVisible(result[0]);
    if (pageState.shtimModifikim == 'kthim')
        ASPxMenu1.GetItemByName('Draft').SetVisible(false);
    else
        ASPxMenu1.GetItemByName('Draft').SetVisible(result[1]);
    ASPxMenu1.GetItemByName('Aprovo').SetVisible(result[2]);
    ASPxMenu1.GetItemByName('Refuzo').SetVisible(result[3]);
    ASPxMenu1.GetItemByName('Delego').SetVisible(result[4]);
    ASPxMenu1.GetItemByName('Komento').SetVisible(result[5]);
    ASPxMenu1.GetItemByName('Modifiko').SetVisible(result[6]);
    ASPxMenu1.GetItemByName('Shto').SetVisible(result[8]);
    ASPxMenu1.GetItemByName('Fshi').SetVisible(result[10]);
}
/*
Function: merrTeDhena
 
Merr te dhenat qe ka grida dhe i vendos neper hidden field-e per ti perdorur ne server side
*/
function merrTeDhena(e, callbackGjeneral) {
    trupiBosh = true;
    var grida = $('#rowed5');
    grida.getTeDhenaRreshtiAsync(function (rreshtaTeGrides) {
        var tmp2 = new Array();
        var col = new Array();
        var k = 0;
        for (i = 0; i < rreshtaTeGrides.length; i++) {
            var nrPersonal = rreshtaTeGrides[i].txtNrPersonal;
            if (nrPersonal !== "") {
                trupiBosh = false;
                tmp2[k] = rreshtaTeGrides[i];
                col[k] = pageState.colKompListPagese[nrPersonal];
                k++;
            }
        }
        unformatoFushaDevi()
        pageState.teDhenaPerRuajtje = {
            colKomponente: col,
            rreshtaGride: tmp2
        };
        callbackGjeneral();
    });

}



function ButtonClickKodi() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    localStorage.removeItem("punonjesitEZgjedhur");
    var contentUrl = "";
    var msgLupe = "";
    if ($('#txtNrPersonal' + idRresht).val() !== "")
        merrIdPunonjes(idRresht);
    else {
        var queryStr = Utils.KonvertoObjectQueryString({
            vjenNga: "ListPagesa",
            dep: (cmbDepartamenti.GetValue() !== null && cmbDepartamenti.GetSelectedIndex()!==-1 ? cmbDepartamenti.GetValue() : 0 ),
            nendep: (cmbNenDepartamenti.GetValue() !== null && cmbNenDepartamenti.GetSelectedIndex() !==-1? cmbNenDepartamenti.GetValue() : 0),
            idkonfigambjente: $("#hfGridaKodi").val(),
            data: dteDtDok.GetText(),
            muaj: cmbMuaji.GetValue(),
            nrPersonal: "",
            llogaritDiteLejeNgaImporti: llogaritDiteLejeNgaImporti,
            mesazhnjepernje: mesazhnjepernje,
            ditemuajindryshueshme: ditemuajindryshueshme,
            idKokaLp: pageState.idKokaLp
        });
        msgLupe = hfState.Get("msgZgjidhniPunonjesin");
        contentUrl = 'LupaPunonjes.aspx?' + queryStr;
        myButtonClickLupa.LupaUniversal_Click(msgLupe, contentUrl, widthLupaPunonjes, heightLupaPunonjes);
    }
    //ruaj per rihapjen e popup
    localStorage.setItem("lupaPunonjesitURL", contentUrl);
    localStorage.setItem("lupaPunonjesitHeaderText", msgLupe);
}

function merrIdPunonjes(id) {
    var grida = $("#rowed5");
    var nrPersonal = grida.getTekstQelize('txtNrPersonal', id);
    $.ajax({
        pritPergjigje: true,
        showLoading: true,
        url: Utils.getServerApiUrl("ListPagesa", "MerrIdPunonjesi"),
        data: JSON.stringify({ nrPersonal: nrPersonal, idNdermarrje : pageState.idNdermarrje})
    }).done(function (result) {
        LupKomponente({ id: result, nrPersonal: nrPersonal });
    });
}


/*
Function: vendosTotalet
 
Llogarit dhe vendos totalin.
 
Nryshoi kevi
 
TORECHECK GETSON
*/
function vendosTotalet() {
    var grida = jQuery("#rowed5");
    var totali = 0;
    var rreshtaTeGrides = grida.getRowData();
    var ids = grida.getDataIDs();
    for (i = 0; i < rreshtaTeGrides.length; i++) {
        if ($("#txtPaguar" + ids[i]).val() !== undefined) {
            if ($("#txtNrPersonal" + ids[i]).val() !== "")
                totali = totali + parseFloat($("#txtPaguar" + ids[i]).val());
        }
        else if (rreshtaTeGrides[i].txtNrPersonal !== "")
            totali = totali + parseFloat(rreshtaTeGrides[i].txtPaguar.replace(/,/g, ""));
    }
    txtVlefta.SetText(totali);
    var tot2 = 0;
    // var tot3 = 0;
    for (var key in pageState.colKompListPagese) {//gjithe punonjesit
        {
            var colKomp = pageState.colKompListPagese[key];
            if (colKomp != undefined) {
                for (p = 0; p < colKomp.length; p++) {//secili punonjes
                    if (colKomp[p].Tipi == 1) {
                        tot2 += parseFloat(colKomp[p].Vlera);
                        // tot3 += parseFloat(colKomp[p].Vlera);
                    }
                    else if (colKomp[p].KodKomponente == "SN" || colKomp[p].KodKomponente == "COMPPENS")
                        tot2 += parseFloat(colKomp[p].Vlera);
                    else if (colKomp[p].KodKomponente == "SICKLEAVES" || colKomp[p].KodKomponente == "UNPAIDLEAVES") {
                        tot2 -= parseFloat(colKomp[p].Vlera);
                    }
                }
            }
        }
    }
    // console.log(tot3);
    txtVlefta2.SetText(tot2);
}

///hap lupen e departamentit nendepartamentit
function ButtonClickedDepartamenti(vjenNga) {


    if (vjenNga !== 'NenDepartamenti') {
        var hf = $("#hfLupaDepartament");
        var idprindi = 0;
        var title = hfState.Get("msgZgjidhniDepartamentin");
    }
    else {
        hf = $("#hfLupaNenDepartament");
        idprindi = cmbDepartamenti.GetValue();
        if (idprindi == null) {
            cmbNenDepartamenti.SetEnabled(false);
            cmbNenDepartamenti.SetText('');
            return;
        }
        title = hfState.Get("msgZgjidhniNenDepartamentin");
    }
    var queryStr = hf.val();

    myButtonClickLupa.LupaUniversal_Click(title, 'LupaStrukturaAdministrative.aspx?vjenNga=' + vjenNga + '&IdPrindi=' + idprindi + '&idKonfigAmbjente=' + queryStr, widthLupaDep, heightLupaDep);
}

///kur selektohet nje departament
function departamentiChanged() {
    if (cmbDepartamenti.GetSelectedIndex() !== -1) {
        cmbNenDepartamenti.SetEnabled(true);
        cmbNenDepartamenti.SetText('');
    }
    else {
        cmbNenDepartamenti.SetEnabled(false);
        cmbNenDepartamenti.SetText('');
    }
}

/*
Function: pastroFushatKokes
	
Pastron fushat pasi eshte bere ruajtja apo modifikimi per te bere gati ambjentin per shtim.
*/
function pastroFushatKokes() {
    txtNrDok.SetText('');
    txtShenime.SetText('');
    txtVlefta.SetText('0'); txtVlefta2.SetText('0');
    cmbDepartamenti.SetSelectedIndex(-1);
    cmbDepartamenti.SetText('');
    departamentiChanged();
    lblStatusAprovimi.SetText('');
    hfState.Set("StatusAprovimi","");
    cmbMonedha.SetSelectedIndex(0);
    callWebserviceKursi(cmbMonedha.GetValue());
    var hf = $("#status1");
    hf.val("false");
    hfArkiva.Clear();
    $('#hfArkivaDokId').val("");
}

function vendosDateDefault(shtoModifiko) {
    switch (shtoModifiko) {
        case "shtim":
            try {
                var hfPeriudheObj = {};
                hfPeriudheObj.idPeriudha = hfPeriudhaKontabel.Get("idPeriudha");
                hfPeriudheObj.emerPeriudha = hfPeriudhaKontabel.Get("emerPeriudha");
                hfPeriudheObj.fillimiPeriudha = hfPeriudhaKontabel.Get("fillimiPeriudha");
                hfPeriudheObj.mbarimiPeriudha = hfPeriudhaKontabel.Get("mbarimiPeriudha");


                dteDtDok.SetDate(new Date(hfPeriudheObj.mbarimiPeriudha));
                dteDtRegjistrimi.SetDate(Utils.ktheDateServeriFromCookies());
                cmbMuaji.SetSelectedIndex(new Date(hfPeriudheObj.mbarimiPeriudha).getMonth());
            }
            catch (e) { console.log(e); }
            break;
        case "klonim":
            dteDtRegjistrimi.SetDate(Utils.ktheDateServeriFromCookies());
            break;
        default:
            break;
    }
}

/*
Function: isValidKoka
	
Kontrollon nese jane plotesuar fushat e detyrueshme te kokes se dokumentit dhe validon daten.
*/
function isValidKoka() {
    if (cmbKonfigurimi.GetText() === "") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniLlojin"));
        return false;
    }
    else if (txtNrDok.GetText() === "") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgShenoniNumrinEDokumentit"));
        return false;
    }
    else if (dteDtDok.GetDate() === null) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniNjeDateDokumenti"));
        return false;
    }
    else if (dteDtRegjistrimi.GetDate() === null) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniNjeDateRegjstrimi"));
        return false;
    }
    else return true;
}

/*
Function: EndRequestHandler
	
Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe array-t e perdorura.
Shiko funksionet <pastro>, <pastroFushatKokes> dhe <ndryshoKonfigurimin>.
*/
function EndRequestHandler(sender, args) {

    console.error("erdhi ketu ku sduhet!")
    //  EndRequest();
}

function EndRequest(autosave) {
    formatoFushaDevi();
    var hf = document.getElementById("status1");
    var idNdermarrje = hfState.Get('idNdermarrje');
    if ($('#hfqkmesazhi').val() == 'shfaqmesazh' && $('#hfStatusRuajtje').val() != '0') {
        //popMesazhQK.Show();
        $('#hfqkmesazhi').val('jo');
        myMesazh.ShtoMesazh({ type: "confirm", layout: "center", modal: true, text: hfState.Get("msgDeshironiTeBeniShperndarjenNeQendratEKostos"), cancelClick: JopopupClick, okClick: hapPopUp });
    }
    if ($('#hfqkmesazhi').val() == 'shfaqlupe') {
        myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgShperndarjeNeQendratEKostos"), $('#hfUrl').val(), 900, 600);
        $('#hfUrl').val('');
    }
    if (hf.value === "true") {
        var newId = $("#hfNewId").val();
        var eshteNeCikelAprovimi = Utils.getNumberOrDefaultFromUrl("idetapa") != 0 || $("#hfEshteNeCikelAprovimi").val() == 'True';

        if (mosPastroPasRuajtjes && !eshteNeCikelAprovimi && newId > 0) {
            pageState.idKokaLp = newId;

            if (pageState.shtimModifikim == "shtim" || pageState.shtimModifikim == "klonim") {
                //nese dokumenti eshte krijuar per here te pare atehere tani nuk eshte me shtim por modifikim meqe po rri i hapur!
                $('#hfShtimModifikim').val('modifikim');
                $('#hfLidhur').val("False");
                pageState.shtimModifikim = 'modifikim'
            }
            var newUrl = Utils.AddOrReplaceQueryString(location.href, "id", newId);
            newUrl = Utils.AddOrReplaceQueryString(newUrl, "shtim_modifikim", pageState.shtimModifikim);
            var aspxForm = document.getElementsByTagName("form")[0];
            aspxForm.setAttribute("action", newUrl);
            history.pushState({ newUrl: newUrl }, '', newUrl);
            click = false;
        }
        else {
            PastroClick();
        }
    }
    else
        click = false;

    if (autosave) {
        btnImporti.SetEnabled(true);
        ASPxMenu1.SetEnabled(true);
        autoSaver.SetSaving(false);
        if (autoSaver.IsPending()) {
            autoSaver.Save();
        }
    }

    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "MerrMenuPerPerdoruesSipasSkemes"),
        data: JSON.stringify({
            idskema: $('#hfSkema').val(),
            idperdoruesi: $('#hfPerdoruesi').val(),
            isNotModifikim: (pageState.shtimModifikim == 'modifikim' ? false : true),
            status: hfState.Get("StatusAprovimi"),
            idkokashitje: pageState.idKokaLp,
            kodkonf: cmbKonfigurimi.GetText(),
            idlloji: 38
        })
    }).done(SuccedcallbackSkemaMenu);
}

function JopopupClick(s, e) {
    if ($('#hfUrl').val() != '')
        $('#hfUrl').val('');
}

function closePopup(s, e) {

    popupUniversal.SetContentUrl('');
    if (e.closeReason != undefined && e.closeReason == "API" && pageState.kaNdryshime) {
        KryejRuajtjeAutomatike();
    }
    pageState.kaNdryshime = false;
}

function hapPopUp(s, e) {
    if ($('#hfUrl').val() != '') {
        myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgShperndarjeNeQendratEKostos"), $('#hfUrl').val(), 900, 600);
        $('#hfUrl').val('');
    }
}

/*
Function: menuClick
 
Percakton veprimin qe kryhet nese klikohet nje nga butonat e menuse.
*/
function menu_click(s, e) {
    if (e.item.name == "Aprovo") {
        popAprovo.Show();
        e.processOnServer = false;
        return;
    }
    if (e.item.name === 'Ruaj' || e.item.name == "Refuzo" || e.item.name == "Modifiko" || e.item.name == "Delego") {
        e.processOnServer = false;
        myFaqeCelje.validim(s, e);
        if (isValidKoka()) {
            RuajClick(s, e, e.item.name, false);
        }
    }
    else if (e.item.name === 'Draft') {
        e.processOnServer = false;
        myFaqeCelje.validim(s, e);
        if (isValidKoka()) {
            RuajClick(s, e, e.item.name, false);
        }
        else {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgPlotesoniTeGjithaFushat"));
        }
    }
    else if (e.item.name == "Komento") {
        if (Utils.getUrlVar('vjenNga') == 'aprovim' || Utils.getUrlVar('vjenNga') == 'kerkese')
            myButtonClickLupa.LupaUniversal_Click('Komentet', 'LupaKomente.aspx?idetapa=' + pageState.idEtapeAprovimi + '&nrprocesi=' + Utils.getUrlVar('nrprocesi') + '&veprimi=Gjitha&idkategoria=38', 600, 500);
        else {
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "gjejEtapeDokumentiList"),
                data: JSON.stringify({ idperdoruesi: $('#hfPerdoruesi').val(), idkoka: pageState.idKokaLp })
            }).done(SucededCallbackLupaKomente);
        }
        click = false;
    }
    else if (e.item.name === 'Klono') {
        if(!click)
            KlonoClick(e);
    }
    else if (e.item.name === 'Kerko') {
        myFaqeCelje.kontrolloTeDrejta('ListPagesa.aspx', null, true);
        e.processOnServer = false;
    }
    else if (e.item.name === 'Shto') {
        myFaqeCelje.kontrolloTeDrejta('Shto_ListPagesa.aspx?shtim_modifikim=shtim', true);
    }
    else if (e.item.name === 'Fshi') {
        identifikuesPyetje = "FshiListePagese"
        myMesazh.ShtoPyetje(hfState.Get("labelAdministrimiMsgJeniSigurt"));
        e.processOnServer = false;
    }
    else if (e.item.name === 'Anullo') {
        if (!click) {
            if (Utils.getUrlVar('vjenNga') == 'aprovim')
                myFaqeCelje.kontrolloTeDrejta('ListeAprovimi.aspx?status=aprovim');
            else if (Utils.getUrlVar('vjenNga') == 'kerkese')
                myFaqeCelje.kontrolloTeDrejta('ListeAprovimi.aspx?status=kerkese');
            else
                myFaqeCelje.kontrolloTeDrejta('ListPagesa.aspx');
        }
        click = false;
    }
    else if (e.item.name == 'Arkiva') {
        ButtonClickArkiva();
        e.processOnServer = false;
        click = false;
        return;
    }
    e.processOnServer = false;
}

function ButtonClickArkiva() {//po
    popupUniversal.SetHeaderText(hfState.Get("regjisDokZgjidhDokPerTeBashkengjitur"));
    popupUniversal.SetSize(738, 548);

    popupUniversal.SetContentUrl('LupaArkiva.aspx?vjenNga=ListPagesa&veprimi=listpagesa' + '&idDok=' + $('#hfArkivaDokId').val() + "&tmpfolder=" + hfArkiva.Get("rootFolder"));

    popupUniversal.Show();
}

function SucededCallbackLupaKomente(result) {
    myButtonClickLupa.LupaUniversal_Click('Komentet', 'LupaKomente.aspx?idetapa=' + result[0] + '&nrprocesi=' + result[1] + '&veprimi=Gjitha&idkategoria=38', 600, 500);
}
function Kthehu(mesazh) {
    myFaqeCelje.kontrolloTeDrejta('ListPagesa.aspx?ruaj=po');
}

function KlonoClick(e) {
    ASPxMenu1.GetItemByName('FletaKontabel').SetVisible(false);
    ASPxMenu1.GetItemByName('QendraKosto').SetVisible(false);
    ASPxMenu1.GetItemByName('Draft').SetVisible(true);
    var hf1 = $("#hfShtimModifikim");
    e.processOnServer = false;
    hf1.val("klonim");
    hfState.Set("StatusAprovimi", "");
    pageState.shtimModifikim = "klonim";
    myMenu.menuSipasTeDrejtaRegjistrim(hf1, hfTeDrejta);
    ndryshoKonfigurimin();
    //inicializoGride();
}

var click = false;

function KryejRuajtjeAutomatike() {
    if (!(ruajAutomatikisht && lblStatusAprovimi.GetText() == "" && pageState.shtimModifikim == "modifikim" && hfState.Get("IdStatusdok") == 0))
        return;

    autoSaver.Save();
}


function RuajAutomatikisht(veprimi) {
    btnImporti.SetEnabled(false);
    ASPxMenu1.SetEnabled(false);
    RuajClick(undefined, undefined, veprimi, true);
}

/*
Function: RuajClick
 
Validon dokumentin dhe therret funksionin <merrTeDhena> per te marre te dhenat e grides kur klikohet butoni Ruaj.
*/

function RuajClick(s, e, veprimi, autoSave) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if (click) {
        if(e)
            e.processOnServer = false;
        return;
    }
    click = true;
    if (isValidKoka()) {
        grida.saveRow(idRresht, false, 'clientArray');
        merrTeDhena(e, function () {
            grida.setLastSel2(0);
            if (trupiBosh === true) {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgTrupiDokumentitNukDuhetLeneBosh"));
                if(e)
                    e.processOnServer = false;
                click = false;
                return;
            }
            if (!Utils.nrWsRrugesManager.kanePerfunduarWs()) {
                Utils.shtoFunksionNeRadheMeParametra(RuajRegjistrimListepagese, [veprimi, autoSave], pageState.idGjuha)
            }
            else RuajRegjistrimListepagese(veprimi, autoSave);

        });
    }
    else {
        click = false;

    }
}

function RuajRegjistrimListepagese(veprimi, autoSave) {
    var idEtapa = 0;
    if (Utils.getUrlVar("vjenNga") != "undefined") {
        idEtapa = Utils.getNumberOrDefaultFromUrl("idetapa");
    }
    var IdNgaQueryString = Utils.getNumberOrDefaultFromUrl("id");
    var nrAutoNrDok = hfNrAuto.Get("txtNrDok");
    if (autoSave) {
        myMesazh.ShtoMesazh({ type: "warning", text: (pageState.idGjuha == 0 ? 'Duke ruajtur...' : 'Saving...'), timeout: 2000, layout: "topRight" });
    }
    var idDepartamenti = cmbDepartamenti.GetValue() !== null && cmbDepartamenti.GetSelectedIndex()!==-1 ? cmbDepartamenti.GetValue() : 0 ;
    var idNenDepartamenti = cmbNenDepartamenti.GetValue() !== null && cmbNenDepartamenti.GetSelectedIndex()!==-1 ? cmbNenDepartamenti.GetValue() : 0;

    hfArkiva.Set("kopjoArkiven", $("#hfShtimModifikim").val() != "shtim" ? true : false);

    callWebServiceRuajRegjistrimListePagese({
        veprimi: veprimi,
        shtimModifikim: $("#hfShtimModifikim").val(),
        DtDok: dteDtDok.GetText(),
        DtRegjistrimi: dteDtRegjistrimi.GetText(),
        kontabilizim: $("#hfKontabilizimi").val(),
        IdNgaQueryString: IdNgaQueryString,
        IdEtapa: idEtapa,
        ServerUrl: hfState.Get("ServerUrl"),
        IdKonfigurimi: cmbKonfigurimi.GetValue(),
        KodKonfigurimi: cmbKonfigurimi.GetText(),
        Vlefta: txtVlefta.GetText(),
        Vlefta2: txtVlefta2.GetText(),
        NrDok: txtNrDok.GetText(),
        Muaji: cmbMuaji.GetValue(),
        IdMonedha: cmbMonedha.GetValue(),
        KodMonedha: cmbMonedha.GetText(),
        Kursi: txtKursi.GetText(),
        Shenime: txtShenime.GetText(),
        gridDataObject: pageState.teDhenaPerRuajtje.rreshtaGride,
        hfKomp: pageState.teDhenaPerRuajtje.colKomponente,
        IdSkema: $("#hfSkema").val(),
        Lidhur: $("#hfLidhur").val(),
        lblStatusAprovimi: hfState.Get("StatusAprovimi"),
        hfNewId: $("#hfNewId").val(),
        nrAutoNrDok: nrAutoNrDok != undefined ? nrAutoNrDok : "",
        hfStatus1: $("#status1").val(),
        pergjigjaValue: pergjigja.GetText(),
        idGjuha: pageState.idGjuha,
        idPerdoruesi:  pageState.idPerdoruesi,
        idNdermarrjeVit: pageState.idNdermarrjeVit,
        idViti: pageState.idViti,
        idNdermarrje: pageState.idNdermarrje, 
        idDepartamenti: idDepartamenti,
        idNenDepartamenti: idNenDepartamenti,
        Departamenti: cmbDepartamenti.GetText(),
        Nendepartamenti: cmbNenDepartamenti.GetText(),
        hfArkiva: JSON.stringify({ rootFolder: hfArkiva.properties.dxprootFolder, kopjoArkiven: hfArkiva.properties.dxpkopjoArkiven })
    }, autoSave)
}

function callWebServiceRuajRegjistrimListePagese(params, autoSave) {
    $.ajax({
        pritPergjigje: !autoSave,
        showLoading: !autoSave,
        url: Utils.getServerApiUrl("ListPagesa", "RuajRegjistrimListPagese"),
        data: JSON.stringify(params)
    }).done(function (result) {
        doneCallbackRuajRegjistrimListePagese(result, autoSave);
    }).error(function (result) {
        errorListPagese(result, autoSave);
            
    });
}
function errorListPagese(result, autosave) {
    if(!autosave)
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgBlerjeShitjeRuajtjeMeGabime"));
}
function doneCallbackRuajRegjistrimListePagese(result, autoSave) {
    $("#hfStatusRuajtje").val(result.StatusDokumenti);
    $("#status1").val(result.hfStatus1Value);
    pergjigja.SetText(result.pergjigjaValue);
    pergjigja.SetVisible(false);
    myMesazh.ShtoMesazhSesioni(result.Mesazh);
    if (result.Mesazh.Tipi == 1) {
        $("#hfNewId").val(result.hfNewIdValue);
        hfState.Set("IdStatusdok", result.StatusDokumenti);
        $("#hfEshteNeCikelAprovimi").val(result.hfEshteNeCikelAprovimiValue);
        $("#hfShtimModifikim").val(result.hfShtimModifikimValue);
        $("#hfqkmesazhi").val(result.hfqkmesazhiValue);
        pageState.shtimModifikim = result.hfShtimModifikimValue;
        if (result.hfqkmesazhiValue != "jo")
            $("#hfUrl").val(result.hfUrlValue);
    }
    if (result.visibleMenus != undefined) {
        VendosVisibleMenus(result.visibleMenus);
        $("#hfTeDrejtaModSkema").val(result.hfTeDrejtaModSkemaValue);
    }
    $("#pnlLidhur tr").remove()
    EndRequest(autoSave);
}



function VendosVisibleMenus(visibleMenus) {

    ASPxMenu1.GetItemByName("Ruaj").SetVisible(visibleMenus[0]);
    ASPxMenu1.GetItemByName("Draft").SetVisible($("#hfShtimModifikim").val() != "kthim" && visibleMenus[1]);
    ASPxMenu1.GetItemByName("Aprovo").SetVisible(visibleMenus[2]);
    ASPxMenu1.GetItemByName("Refuzo").SetVisible(visibleMenus[3]);
    ASPxMenu1.GetItemByName("Delego").SetVisible(visibleMenus[4]);
    ASPxMenu1.GetItemByName("Komento").SetVisible(visibleMenus[5]);
    ASPxMenu1.GetItemByName("Modifiko").SetVisible(visibleMenus[6]);
    ASPxMenu1.GetItemByName("Shto").SetVisible(visibleMenus[8]);
    ASPxMenu1.GetItemByName("Fshi").SetVisible(visibleMenus[10]);

}
/*
Function: PastroClick
 
Pastron koken e dokumentit dhe array-t e perdorura deh inicializon griden.
Shiko funksionet <pastro>, <pastroFushatKokes> dhe <inicializoGride>.
*/

function hiqKomponenteMuajiNgaSessioni() {
    $.ajax({
        type: "DELETE",
        url: Utils.getServerApiUrl("Listpagesa", "PastroSessionNgaKomponenteMuaji")
    });
}

function PastroClick() {
    ASPxMenu1.GetItemByName('FletaKontabel').SetVisible(false);
    ASPxMenu1.GetItemByName('Fshi').SetVisible(false);
    ASPxMenu1.GetItemByName('QendraKosto').SetVisible(false);
    var hf = $("#hfShtimModifikim");
    hf.val("shtim");
    pageState.clearState();
    $('#HfTrupiDok').val('');
    $("#HfVleraTeRillogaritura").val('');
    ASPxMenu1.GetItemByName('Draft').SetVisible(true);
    pastroFushatKokes();
    hiqKomponenteMuajiNgaSessioni();
    myMenu.menuSipasTeDrejtaRegjistrim(hf, hfTeDrejta);
    ndryshoKonfigurimin(); //duhet kur klijkojme butonin shto ne rastin kur kemi hap nje dok. per modifikim
   // jQuery("#rowed5").GridUnload("rowed5");
  //  inicializoGride();

    $('#ASPxSplitter1_hl').empty(); click = false;
}
function SucceededCallbackSess(result) {

}
function ndryshoImazhin(nr, index) {
    var id = "butonFshi" + index;
    if (document.getElementById(id) !== null) {
        if (nr === 1)
            document.getElementById(id).src = "images/blue-square-icon.png";
        else if (nr === 0)
            document.getElementById(id).src = "images/square-icon.png";
    }
}

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}

function NdryshimMuaji() {
    var muaji = cmbMuaji.GetValue();
    var hfPeriudheObj = new Object();
    hfPeriudheObj.idPeriudha = window.hfPeriudhaKontabel.Get("idPeriudha");
    hfPeriudheObj.emerPeriudha = window.hfPeriudhaKontabel.Get("emerPeriudha");
    hfPeriudheObj.fillimiPeriudha = window.hfPeriudhaKontabel.Get("fillimiPeriudha");
    hfPeriudheObj.mbarimiPeriudha = window.hfPeriudhaKontabel.Get("mbarimiPeriudha");
    var sot = hfPeriudheObj.mbarimiPeriudha.format("dd/MM/yyyy");
    var sotSplit = sot.split("/");
    var date = new Date((new Date(sotSplit[2], muaji, 1)) - 1);
    date.setSeconds(0);
    date.setMilliseconds(0);
    date.setHours(0);
    date.setMinutes(0);
    dteDtDok.SetDate(date);
    //dteDtRegjistrimi.SetDate(date);
    var hfKontrollet = JSON.parse($('#hfKontrolletNrAutom').val());
    var atributet = JSON.parse($('#hfAtributeNrAutom').val());
    vendosNrAutomatik(atributet, hfKontrollet);
    if (pageState.shtimModifikim == "klonim")
        callWsMerrTrupDokumenti(true);
    return;
    if (pageState.colKompListPagese == undefined || Object.keys(pageState.colKompListPagese).length == 0)
        return;

     RimerrVlera(ditemuajindryshueshme, true, true);
}
function btnImportiClick() {
    RimerrVlera(false, false, false);
}

function LupKomponente(punonjesi) {
    var newrecord = true;
    var nrPersonalGrida = "";

    var grida = $("#rowed5");
    var idRresht = grida.getLastSel2();

    nrPersonalGrida = grida.getTekstQelize('txtNrPersonal', idRresht);
    if (pageState.colKompListPagese[nrPersonalGrida] !== undefined) {
        if (punonjesi.nrPersonal != '' && punonjesi.nrPersonal == nrPersonalGrida) {
            newrecord = false;
        }
    }
    var queryStr = Utils.KonvertoObjectQueryString({
        idpunonjesi: punonjesi.id,
        monedha: cmbMonedha.GetValue(),
        konfig: cmbKonfigurimi.GetValue(),
        newrecord: newrecord,
        formatnr: formatNumriZgjedhur.ShifraPasPresjesVlefta,
        lidhur: lidhur,
        data: dteDtDok.GetText(),
        muaj: cmbMuaji.GetValue(),
        nrPersonal: punonjesi.nrPersonal,
        llogaritDiteLejeNgaImporti: llogaritDiteLejeNgaImporti,
        mesazhnjepernje: mesazhnjepernje,
        ditemuajindryshueshme: ditemuajindryshueshme,
        idKokaLp: pageState.idKokaLp

    });
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgTeDhenatEPages"), 'LupaKomponente.aspx?' + queryStr, 800, 600);

}
function ModifikoRreshtNeGrid(rreshtiRi, idRow, grida) {

    grida.setTekstQelize('txtNrPersonal', idRow, rreshtiRi.txtNrPersonal);
    grida.setTekstQelize('txtPunonjes', idRow, rreshtiRi.txtPunonjes);
    grida.setTekstQelize('txtDep', idRow, rreshtiRi.txtDep);
    grida.setTekstQelize('txtPP', idRow, rreshtiRi.txtPP);
    grida.setTekstQelize('txtPR', idRow, rreshtiRi.txtPR);
    grida.setTekstQelize('txtOD', idRow, rreshtiRi.txtOD);
    grida.setTekstQelize('txtDM', idRow, rreshtiRi.txtDM);
    grida.setTekstQelize('txtPJ', idRow, rreshtiRi.txtPJ);
    grida.setTekstQelize('txtTP', idRow, rreshtiRi.txtTP);
    grida.setTekstQelize('txtSP', idRow, rreshtiRi.txtSP);
    grida.setTekstQelize('txtSN', idRow, rreshtiRi.txtSN);
    grida.setTekstQelize('txtSPS', idRow, rreshtiRi.txtSPS);
    grida.setTekstQelize('txtSPD', idRow, rreshtiRi.txtSPD);
    grida.setTekstQelize('txtSNS', idRow, rreshtiRi.txtSNS);
    grida.setTekstQelize('txtSND', idRow, rreshtiRi.txtSND);
    grida.setTekstQelize('txtST', idRow, rreshtiRi.txtST);
    grida.setTekstQelize('txtNT', idRow, rreshtiRi.txtNT);
    grida.setTekstQelize('txtPT', idRow, rreshtiRi.txtPT);
    grida.setTekstQelize('txtPPS', idRow, rreshtiRi.txtPPS);
    grida.setTekstQelize('txtPRN', idRow, rreshtiRi.txtPRN);
    grida.setTekstQelize('txtPaguar', idRow, rreshtiRi.txtPaguar);
    grida.setTekstQelize('txtCosto', idRow, rreshtiRi.txtCosto);
    grida.setTekstQelize('txtPagaShtesa', idRow, rreshtiRi.txtPagaShtesa);

}
function shtoPunonjesNeGrid(vleratPerPunonjesit) {
    if (undefined == vleratPerPunonjesit || vleratPerPunonjesit.length == 0) return;
    var grida = $("#rowed5");
    var rreshtaEkzistues = grida.getTeDhenaRreshti();
    if (rreshtaEkzistues == undefined || Utils.IsNullOrWhiteSpace(rreshtaEkzistues[0].txtNrPersonal)) {
        rreshtaEkzistues = new Array();
    }
    //merr ata qe nuk jane bosh
    var idRreshtiIPareBosh = findFirstEmptyRow(rreshtaEkzistues);
    var ids = grida.getDataIDs();
    rreshtaEkzistues = rreshtaEkzistues.filter(function (x) { return x.txtNrPersonal != '' });
    var kaRreshtaEkzistues = rreshtaEkzistues.length > 0;
    var nrRendor = rreshtaEkzistues.length + 1;

    //Kur ka vetem 1 punonjes dhe disa punonjes ne gride shtojme vetem ate, nuk vendosim gjithe punonjesit njekohesisht
    if (vleratPerPunonjesit.length == 1 && rreshtaEkzistues.length > 1) {
        var rreshtiRi = vleratPerPunonjesit[0];
        var indexOfRreshtiVjeter = merrIdRreshtiEkzistuesNeGride(rreshtaEkzistues, rreshtiRi.NrPersonal);
        if (indexOfRreshtiVjeter == -1) {
            if (idRreshtiIPareBosh == -1) {
                VendosVleraFillestareNeRresht(rreshtiRi.RreshtiPerGride, nrRendor);
                grida.addRowData(nrRendor, rreshtiRi.RreshtiPerGride);
            } else {
                ModifikoRreshtNeGrid(rreshtiRi.RreshtiPerGride, ids[idRreshtiIPareBosh], grida);
            }
        } else {
            rreshtiVjeter = rreshtaEkzistues[indexOfRreshtiVjeter];
            rreshtiRi.RreshtiPerGride.txtShenime = rreshtiVjeter.txtShenime;
            ModifikoRreshtNeGrid(rreshtiRi.RreshtiPerGride, ids[indexOfRreshtiVjeter], grida);

        }
        pageState.colKompListPagese[rreshtiRi.NrPersonal] = rreshtiRi.ColKomponente;
        vendosTotalet();
        return;
    }
    if (rreshtaEkzistues.length > 0)
        VendosVleraFillestareNeRresht(rreshtaEkzistues[0], 1);
    for (var r = 0, length = vleratPerPunonjesit.length; r < length; r++) {
        var rreshtiRi = vleratPerPunonjesit[r];
        var rreshtiVjeter = undefined;
        var indexOfRreshtiVjeter = merrIdRreshtiEkzistuesNeGride(rreshtaEkzistues, rreshtiRi.NrPersonal);
        rreshtiVjeter = rreshtaEkzistues[indexOfRreshtiVjeter];

        if (rreshtiVjeter == undefined) {
            //shtohet per here te pare ne grid 
            rreshtiRi.RreshtiPerGride = VendosVleraFillestareNeRresht(rreshtiRi.RreshtiPerGride, nrRendor);
            rreshtaEkzistues.push(rreshtiRi.RreshtiPerGride);
            pageState.colKompListPagese[rreshtiRi.NrPersonal] = rreshtiRi.ColKomponente;
            nrRendor++;
            continue;
        }
        //rasti kur rizgjidhen punonjesit dhe mbishkruhen vlerat
        rreshtiRi.RreshtiPerGride = VendosVleraFillestareNeRresht(rreshtiRi.RreshtiPerGride, indexOfRreshtiVjeter + 1);
        rreshtiRi.RreshtiPerGride.txtShenime = rreshtiVjeter.txtShenime;
        rreshtaEkzistues[indexOfRreshtiVjeter] = rreshtiRi.RreshtiPerGride;
        pageState.colKompListPagese[rreshtiRi.NrPersonal] = rreshtiRi.ColKomponente;
    }
    grida.jqGrid("clearGridData");

    grida[0].addJSONData(rreshtaEkzistues);
    grida.addRowData(nrRendor, {});
    grida.setLastSel2(nrRendor + 1);
    vendosTotalet();

}

function merrIdRreshtiEkzistuesNeGride(rreshtaEkzistues, NrPersonalIRi) {
    for (var i = 0, lengthOld = rreshtaEkzistues.length; i < lengthOld; i++)
        if (rreshtaEkzistues[i].txtNrPersonal == NrPersonalIRi)
            return i;

    return -1;
}

function findFirstEmptyRow(rreshtaEkzistues) {
    for (var i = 0, rowLength = rreshtaEkzistues.length; i < rowLength; i++)
        if (Utils.IsNullOrWhiteSpace(rreshtaEkzistues[i].txtNrPersonal))
            return i;

    return -1;
}

function VendosVleraFillestareNeRresht(rreshtiRi, nrRendor) {
    var isReadOnlyKolonaFshi = arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides - 1] === 'True' || lidhur ? 'True' : "False";
    rreshtiRi.id = nrRendor;
    rreshtiRi.name = nrRendor;
    rreshtiRi.txtNrRendor = nrRendor;
    rreshtiRi.txtFshi = myJQGrid.myValueButtonFshi(isReadOnlyKolonaFshi, nrRendor, "#rowed5")
    return rreshtiRi;
}
function ButtonClickKursi(s, e) {
    if (cmbMonedha.GetText() !== '') {
        popupUniversal.SetHeaderText(hfState.Get("msgZgjidhKurs"));
        popupUniversal.SetContentUrl('LupaKursiShpejte.aspx?idMonedha=' + cmbMonedha.GetValue() + '&llojKursi=' + llojkursi);
        popupUniversal.SetSize(widthLupaKursi, heightLupaKursi);
        popupUniversal.Show();
    }
}
var arrvlera = new Array();
var arrvleraParam = new Array();
var arrKodi = new Array();
var arrMosNdrysho = new Array();

function RimerrVlera(DPZero, listPageseRe, pastroVleratPerMuajt) {
    if (pageState.TrupiDok.length == 0) return;

    if (pageState.shtimModifikim == "klonim") {
       var colTrupi = pageState.TrupiDok;
        var grida = jQuery("#rowed5");
        var jqGrida = grida;
        var rreshtat = new Array();
        var colsFillestare = new Array();
        var nrPersonals = new Array();
        var ids = grida.getDataIDs();
        colTrupi = pageState.TrupiDok;
        for (k = 0; k < colTrupi.length; k++) {
            var nrPersonal = colTrupi[k].NrPersonal;
            colFillestare = pageState.TrupiDok[k].ColKomponente;
            pageState.colKompListPagese[pageState.TrupiDok[k].NrPersonal] = $.extend(true, [], colFillestare);

            if (pageState.colKompListPagese[nrPersonal] !== undefined) {
                nrPersonals.push(nrPersonal);
                colsFillestare.push(pageState.colKompListPagese[pageState.TrupiDok[k].NrPersonal]);
            }
        }
    }

    else {
    var grida = jQuery("#rowed5");
    var jqGrida = grida;
    var rreshtaTeGrides = jqGrida.getRowData();
    var colsFillestare = new Array();
    var nrPersonals = new Array();
    var ids = grida.getDataIDs();
    for (k = 0; k < rreshtaTeGrides.length; k++) {
        var nrPersonal = jqGrida.getTekstQelize('txtNrPersonal', ids[k]);
        if (pageState.colKompListPagese[nrPersonal] !== undefined) {
            nrPersonals.push(nrPersonal);
            colsFillestare.push(pageState.colKompListPagese[nrPersonal]);
        }
     }
    }

    var params = {
        DPZero: DPZero,
        colFillestare: colsFillestare,
        data: new Date(dteDtDok.GetDate()),
        personalNrs: nrPersonals,
        muaji: cmbMuaji.GetValue(),
        ditemuajindryshueshme: ditemuajindryshueshme,
        muajitjeter: cmbMuaji.GetValue(),
        vititjeter: new Date(dteDtDok.GetDate()).getFullYear(),
        llogaritDiteLejeNgaImporti: llogaritDiteLejeNgaImporti,
        idNdermarrje: pageState.idNdermarrje,
        idGjuha: pageState.idGjuha,
        listPageseRe: listPageseRe,
        idKokaLp: pageState.idKokaLp,
        pastroVleratPerMuajt: pastroVleratPerMuajt
    };
    $.ajax({
        pritPergjigje: true,
        showLoading: true,
        url: Utils.getServerApiUrl("ListPagesa", "RimerrVlera"),
        data: JSON.stringify(params)
    }).done(doneCallbackLlogaritPagenNew).then(function (then) {
        if (pastroVleratPerMuajt)
            hiqKomponenteMuajiNgaSessioni();
    });


}
function doneCallbackLlogaritPagenNew(results) {
    var isReadOnlyKolonaFshi = arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides - 1] === 'True' || lidhur ? 'True' : 'False';
    var start = new Date().getTime();
    var mesazhet = results.mesazhetPerPunonjes;
    var grida = $("#rowed5");
    var teGjitheRreshtat = results.data;
    var rreshtaGride = new Array();
    var nrRendor = 0;
    for (var z = 0; z < teGjitheRreshtat.length; z++) {
        nrRendor = z + 1;
        var vleratPerNjePunonjes = ListPagesaUtils.LlogaritPagenModel(teGjitheRreshtat[z]);
        var infoPunonjesi = {
            txtNrRendor: nrRendor,
            name: nrRendor,
            id: nrRendor,
            txtNrPersonal: vleratPerNjePunonjes.NrPersonal,
            txtPunonjes: vleratPerNjePunonjes.Emri,
            txtShenime: '',
            txtDep: vleratPerNjePunonjes.Departamenti,
            txtFshi: myJQGrid.myValueButtonFshi(isReadOnlyKolonaFshi, nrRendor, "#rowed5")
        };


        if (vleratPerNjePunonjes.NrKomponenteve === 0) {
            myMesazh.ShtoMesazhInformues("Punonjesi " + infoPunonjesi.txtPunonjes + " nuk ka komponente per kete date!");
            continue;
        }

        var colFillestare = pageState.colKompListPagese[infoPunonjesi.txtNrPersonal];
        if (colFillestare == undefined) {
            myMesazh.ShtoMesazhInformues("Punonjesi :" + nrpersonal + " nuk u shtua ne gride!");
            continue
        };

        var vleraELlogaritura = ListPagesaUtils.llogaritPagen(colFillestare, vleratPerNjePunonjes.ColKomponente, vleratPerNjePunonjes.Kodet, vleratPerNjePunonjes.VleraParam, vleratPerNjePunonjes.Vlerat, vleratPerNjePunonjes.Tatimet, vleratPerNjePunonjes.KursiNdermarrjes, vleratPerNjePunonjes.Kursi, vleratPerNjePunonjes.OrePuneNeDite);

        var rreshtiPerGride = $.extend(infoPunonjesi, vleraELlogaritura.rreshtTrupi);
        rreshtaGride.push(rreshtiPerGride);

        pageState.colKompListPagese[infoPunonjesi.txtNrPersonal] = vleraELlogaritura.colKomponentePerPunonjes;

    }



    if (mesazhet != undefined || Object.keys(mesazhet).length > 0)
        ListPagesaUtils.shfaqMesazhGabimiNgaLLogaritjaEPages(mesazhet);
    grida.jqGrid("clearGridData");

    if (rreshtaGride.length == 0) inicializoGride();
    else grida[0].addJSONData(rreshtaGride);

    grida.setLastSel2(-1);
    vendosTotalet();
    var time = new Date().getTime() - start;
    console.info('Grid load execution time: ' + (time / 1000).toFixed(3) + " s");
}

function Click_ButtonOk1(s, e) {
    popAprovo.Hide();
    e.processOnServer = false;
    myFaqeCelje.validim(s, e);
    if (isValidKoka()) {
        RuajClick(s, e, 'Aprovo', false);
    }
}

function FshiDokument() {
    var arrayId = new Array(1);
    arrayId[0] = Utils.getUrlVar("id");
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("ListPagesa", "FshiDokument"),
        data: JSON.stringify({ ids: arrayId, guidString: "" })
    }).done(SuccededCallbackDelete);    
}

function SuccededCallbackDelete(result) {
    if (result.mesazhSukses != "") {
        myMesazh.ShtoMesazhSuksesi(result.mesazhSukses);
        window.location = "ListPagesa.aspx";
    }
    if (result.mesazhGabim != "")
        myMesazh.ShtoMesazhGabimi(result.mesazhGabim);
}

function PoClick() {
    switch (identifikuesPyetje) {
        case "FshiPunonjes":
            fshiClickedPo();
            break;
        case "FshiListePagese":
            FshiDokument();
            break;
        default:
            break;
    }
}

function JoClick() {
    return;
}