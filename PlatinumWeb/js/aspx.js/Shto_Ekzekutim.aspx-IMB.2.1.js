;
var lidhur = false;
var magazina1;
var widthLupaArtikull = 1200;
var heightLupaArtikull = 600;
var widthLupaMagazina = 600;
var heightLupaMagazina = 600;
var widthLupaKerko = 600;
var heightLupaKerko = 600;
var editorData;


var pageState = {
    kushte: {},
    memoryArt: null,
    hapurpermodifikim: false,
    gridaSelector: "#rowed5",
    gridaSelector2: "#rowed6",
    colGridaProdukti: [],
    colGridaReceptura: []
};

var varKonfig = {
    identifikuesPerLocalStorageKey: 'EkzekutimProdhimi'
};


$(document).ready(function () {

    pageState.memoryArt = new memory("IdArtikulli");

    Utils.konfiguroAccorditionNeDocReady(varKonfig.identifikuesPerLocalStorageKey);

    $(window).on('resize', function () {
        try {
            if (Utils.isGridResized())
                return;
        }
        catch (ee) {
        }
        var grida = $('#rowed5');
        if ($('#divgride3').width() != null) {
            myJQGrid.fixGridWidth(grida, $('#divgride3'));
        }
        if ($('#divgride3').width() != null) {
            myJQGrid.fixGridWidth($('#rowed6'), $('#divgride3'));
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

function Init() {
    if (typeof (isPostBack) == "undefined") {
        document.getElementById("kokeKonfigurimi").innerHTML = hfState.Get("MenuKokeDokumenti");
        document.getElementById("trupKonfigurimi").innerHTML = hfState.Get("MenuTrupDokumenti");
        document.getElementById("fundKonfigurimi").innerHTML = hfState.Get("MenuFundDokumenti");
        editorData = dteDtDok;
        //    callWebserviceKonfigurimi("806"); //806 = id komponente (Shto_Ekzekutim.aspx)
        var hf = document.getElementById("hfKonffillestar");
        if ($('#hfShtimModifikim').val() == 'modifikim')
            arrPlanifikime = JSON.parse($('#hfPlanifikime').val());
        pageState.hapurpermodifikim = true;
        cmbKonfigurimi.SetText(hf.value);
        ndryshoKonfigurimin();
        identifikuesPerPopupDokumentat = "EkzekutimProdhimi";
        identikuesPerPopupKlientFurnitori = "EkzekutimProdhimi";
        identikuesPerPopupArtikulli = "EkzekutimProdhimi";
        identifikuesPerPopupMagazina = "EkzekutimProdhimi";
        changeName();
        myMesazh.shtoHandler();
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(EndRequestHandler);
    }
}

var arrayIdKolonaGrides = new Array();
var arrayPershkrimiKolonaGrides = new Array();
var arrayVisibleKolonaGrides = new Array();
var arrayWidthKolonaGrides = new Array();
var arrayReadOnlyKolonaGrides = new Array();
var arrayRenditjeKolonaGrides = new Array();

function inicializoGride() {
    if (arrayPershkrimiKolonaGrides.length == 0)
        return;
    mbushArrayMagazinat();
    var classes = '';
    if (lidhur === true)
        classes = 'uigray';
    var arrayPershkrime = [arrayPershkrimiKolonaGrides[0], arrayPershkrimiKolonaGrides[1], arrayPershkrimiKolonaGrides[2], arrayPershkrimiKolonaGrides[3], arrayPershkrimiKolonaGrides[4],
    arrayPershkrimiKolonaGrides[5], arrayPershkrimiKolonaGrides[6], arrayPershkrimiKolonaGrides[7], arrayPershkrimiKolonaGrides[8], arrayPershkrimiKolonaGrides[9],
    arrayPershkrimiKolonaGrides[10], arrayPershkrimiKolonaGrides[11], arrayPershkrimiKolonaGrides[12], arrayPershkrimiKolonaGrides[13], arrayPershkrimiKolonaGrides[14],
    arrayPershkrimiKolonaGrides[15], arrayPershkrimiKolonaGrides[16], arrayPershkrimiKolonaGrides[17], arrayPershkrimiKolonaGrides[18], arrayPershkrimiKolonaGrides[19]
    ];
    var arrayModel = [
        { name: arrayIdKolonaGrides[0], index: arrayIdKolonaGrides[0], width: arrayWidthKolonaGrides[0], hidden: arrayVisibleKolonaGrides[0], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemNrRendor, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[1], index: arrayIdKolonaGrides[1], width: arrayWidthKolonaGrides[1], hidden: arrayVisibleKolonaGrides[1], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemIdKoka, custom_value: myJQGrid.myValueTextBox }, hidedlg: true },
        { name: arrayIdKolonaGrides[2], index: arrayIdKolonaGrides[2], width: arrayWidthKolonaGrides[2], hidden: arrayVisibleKolonaGrides[2], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemIdArtikulli, custom_value: myJQGrid.myValueTextBox }, hidedlg: true },
        { name: arrayIdKolonaGrides[3], index: arrayIdKolonaGrides[3], width: arrayWidthKolonaGrides[3], hidden: arrayVisibleKolonaGrides[3], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemKodi, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[4], index: arrayIdKolonaGrides[4], width: arrayWidthKolonaGrides[4], hidden: arrayVisibleKolonaGrides[4], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemPershkrimi, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[5], index: arrayIdKolonaGrides[5], width: arrayWidthKolonaGrides[5], hidden: arrayVisibleKolonaGrides[5], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemComboNjesia, custom_value: myJQGrid.myValueCombo } },
        { name: arrayIdKolonaGrides[6], index: arrayIdKolonaGrides[6], width: arrayWidthKolonaGrides[6], hidden: arrayVisibleKolonaGrides[6], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemGjeresi, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[7], index: arrayIdKolonaGrides[7], width: arrayWidthKolonaGrides[7], hidden: arrayVisibleKolonaGrides[7], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemGjatesi, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[8], index: arrayIdKolonaGrides[8], width: arrayWidthKolonaGrides[8], hidden: arrayVisibleKolonaGrides[8], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemSasiPor, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[9], index: arrayIdKolonaGrides[9], width: arrayWidthKolonaGrides[9], hidden: arrayVisibleKolonaGrides[9], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemSasiAkt, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[10], index: arrayIdKolonaGrides[10], width: arrayWidthKolonaGrides[10], hidden: arrayVisibleKolonaGrides[10], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemKosto, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[11], index: arrayIdKolonaGrides[11], width: arrayWidthKolonaGrides[11], hidden: arrayVisibleKolonaGrides[11], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemKostoTotale, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[12], index: arrayIdKolonaGrides[12], width: arrayWidthKolonaGrides[12], hidden: arrayVisibleKolonaGrides[12], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemComboMagazina, custom_value: myJQGrid.myValueCombo } },
        { name: arrayIdKolonaGrides[13], index: arrayIdKolonaGrides[13], width: arrayWidthKolonaGrides[13], hidden: arrayVisibleKolonaGrides[13], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemSasiPer, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[14], index: arrayIdKolonaGrides[14], width: arrayWidthKolonaGrides[14], hidden: arrayVisibleKolonaGrides[14], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemIdPlanifikim, custom_value: myJQGrid.myValueTextBox }, hidedlg: true },
        { name: arrayIdKolonaGrides[15], index: arrayIdKolonaGrides[15], width: arrayWidthKolonaGrides[15], hidden: arrayVisibleKolonaGrides[15], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemIdUrdher, custom_value: myJQGrid.myValueTextBox }, hidedlg: true },
        { name: arrayIdKolonaGrides[16], index: arrayIdKolonaGrides[16], width: arrayWidthKolonaGrides[16], hidden: arrayVisibleKolonaGrides[16], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemShenime, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[17], index: arrayIdKolonaGrides[17], width: arrayWidthKolonaGrides[17], hidden: arrayVisibleKolonaGrides[17], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemDetajim1, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[18], index: arrayIdKolonaGrides[18], width: arrayWidthKolonaGrides[18], hidden: arrayVisibleKolonaGrides[18], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemDetajim2, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[19], index: arrayIdKolonaGrides[19], width: arrayWidthKolonaGrides[19], hidden: arrayVisibleKolonaGrides[19], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemButtonFshi, custom_value: myValueButtonFshi }, hidedlg: true }
    ];
    lidhur = $("#hfLidhur").val() === 'True';
    var gridParams = {
        emergride: "#rowed5",
        arrayPershkrime: arrayPershkrime,
        arrayModel: arrayModel,
        lidhur: lidhur,
        emerEditorKodi: "txtKodiArtikull",
        widthi: $('#divgride2').width(),
        resetRreshtKorent: resetRreshtKorent,
        lostFocusKoloneFundit: lostFocusKoloneFundit,
        arrayRenditjeKolonaGridesName: arrayRenditjeKolonaGrides,
        arrayReadOnlyKolonaGrides: arrayReadOnlyKolonaGrides,
        autocompleteList: [
            { emerEditor: "txtKodiArtikull", selectFunc: selectFunc, changeFunc: changeFunc, shtoDataKod: false },
            { emerEditor: "txtDetajimi", selectFunc: selectFunca2, changeFunc: changeFunca2, shtoDataKod: false },
            { emerEditor: "txtDetajimi2t", selectFunc: selectFunca3, changeFunc: changeFunca3, shtoDataKod: false }
        ],
        konfigToolbar: {
            konfigGrid: $('#hfTeDrejtaKonfGride').val(),
            kaTeDrejtaArkive: $('#hfTeDrejtaArtImazhe').val(),
            ruajKolonatEGrides: ruajKolonatEGrides,
            hapPopUpImazheArkive: onHapPopUpImazheArkive,
            hapPopUpArkive: onHapLupeArkive
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
    grida.setVlereDefault('txtGjeresiPlanifikuar', 0);
    grida.setVlereDefault('txtGjatesiPlanifikuar', 0);
    grida.setVlereDefault('txtSasiaPlanifikuar', 0);
    grida.setVlereDefault('txtSasiaAktuale', 1);
    grida.setVlereDefault('txtKosto', 1);
    grida.setVlereDefault('txtKostoTotale', 1);
    grida.setVlereDefault('txtSasiPermase', 0);
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
    grida.setShifraPasPresjes('txtGjeresiPlanifikuar', formatNumri.ShifraPasPresjesSasia);
    grida.setShifraPasPresjes('txtGjatesiPlanifikuar', formatNumri.ShifraPasPresjesSasia);
    grida.setShifraPasPresjes('txtSasiaPlanifikuar', formatNumri.ShifraPasPresjesSasia);
    grida.setShifraPasPresjes('txtSasiaAktuale', formatNumri.ShifraPasPresjesSasia);
    grida.setShifraPasPresjes('txtKosto', formatNumri.ShifraPasPresjesCmimi);
    grida.setShifraPasPresjes('txtKostoTotale', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtSasiPermase', formatNumri.ShifraPasPresjesSasia);
    Utils.setFormatNumri(txtTotali, formatNumri.ShifraPasPresjesVlefta);
}

function SucceededCallbackFormatNumri(result) {
    var formatNumri = result;
    var grida = $('#rowed5');
    var grida1 = $('#rowed6');
    ndryshoKonfigFormatNumri(grida, formatNumri);
    vendosKonfigFormatNumri();
    ndryshoKonfigFormatNumriR(grida1, formatNumri);
    vendosKonfigFormatNumriR();
}

/*
Formaton vlerat e fushave sipas formatit perkates.
*/
function vendosKonfigFormatNumri() {
    var grida = $('#rowed5');
    var idTe = grida.jqGrid('getDataIDs');
    for (j = 0; j < idTe.length; j++) {
        var idRreshti = idTe[j];
        grida.formatoQelize('txtGjeresiPlanifikuar', idRreshti);
        grida.formatoQelize('txtGjatesiPlanifikuar', idRreshti);
        grida.formatoQelize('txtSasiaPlanifikuar', idRreshti);
        grida.formatoQelize('txtSasiaAktuale', idRreshti);
        grida.formatoQelize('txtKosto', idRreshti);
        grida.formatoQelize('txtKostoTotale', idRreshti);
        grida.formatoQelize('txtSasiPermase', idRreshti);
    }
    formatoFushaDevi();
}

function formatoFushaDevi() {
    Utils.formatoTextBox(txtTotali);
}

function unformatoFushaDevi() {
    Utils.unFormatoTextBox(txtTotali);
}

function dateGotFocus(s, e) {
    window.setTimeout(function () { s.SetCaretPosition(0); }, 0);
}
/*
Function: myElemNrRendor

Nderton nje textbox per te vendosur nr rendor te rreshtit.
*/
function myElemNrRendor(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[0];
    return myJQGrid.myElemNrRendor(value, options, idRresht, 'txtNrRendor', grida);
}

function myelemIdKoka(value, options) {
    var idRresht = $('#rowed5').getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[1];
    return myJQGrid.myElemIdKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[1]);
}

function myelemIdArtikulli(value, options) {
    var idRresht = $('#rowed5').getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[2];
    return myJQGrid.myElemIdKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[2]);
}

function myelemIdPlanifikim(value, options) {
    var idRresht = $('#rowed5').getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[14];
    return myJQGrid.myElemIdKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[14]);
}

function myelemIdUrdher(value, options) {
    var idRresht = $('#rowed5').getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[15];
    return myJQGrid.myElemIdKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[15]);
}

function myelemKodi(value, options) {
    var idRresht = $('#rowed5').getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[3];
    return myJQGrid.myElemKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[3], ButtonClickKodi, keyPressKodi, changeFunc, undefined, lostFocusKoloneFundit);
}

function myElemPershkrimi(value) {
    var idRresht = $('#rowed5').getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[4];
    return myJQGrid.myElemEmertimi(value, disabled, idRresht, arrayIdKolonaGrides[4]);
}

function myelemShenime(value) {
    var idRresht = $('#rowed5').getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[16];
    return myJQGrid.myElemEmertimi(value, disabled, idRresht, arrayIdKolonaGrides[16]);
}

function myelemDetajim1(value, options) {
    var idRresht = $('#rowed5').getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[17];
    return myJQGrid.myElemKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[17], ButtonClickDetajimet, keyPressDet, changeFunca2, undefined, lostFocusKoloneFundit);
}

function myelemDetajim2(value, options) {
    var idRresht = $('#rowed5').getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[18];
    return myJQGrid.myElemKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[18], ButtonClickDetajimet2, keyPressDet2, changeFunca3, undefined, lostFocusKoloneFundit);
}

function myelemComboNjesia(value) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var vleraKodit = grida.getTekstQelize('txtKodiArtikull', idRresht);
    var objNjesi = new Object();
    objNjesi.value = "";
    objNjesi.text = "";
    var arrayOptions = new Array(1);
    arrayOptions[0] = objNjesi;
    if (arrayReadOnlyKolonaGrides[5] == 'True' || vleraKodit == "") {
        return myJQGrid.myElemCombo(value, 'txtIdNjesia', idRresht, ndryshosasi, arrayOptions, true);
    }
    textNjesiZgjedhur = value;
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheNjesiArtikulliComboMeKodRow"),
        data: JSON.stringify({ kodi: vleraKodit, index: idRresht, textNjesiZgjedhur: textNjesiZgjedhur, idNdermarrje: hfState.Get('idNdermarrje') })
    }).done(SucceededCallbackComboNjesiArtikulli);
    return myJQGrid.myElemCombo(value, 'txtIdNjesia', idRresht, ndryshosasi, arrayOptions, true);
}

function myelemSasiAkt(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var sasiaQelizes = grida.getTekstQelize("txtSasiaAktuale", idRresht);
    sasiavjeter = sasiaQelizes != '' ? parseFloat(sasiaQelizes) : 1;//mbahet vlera e vjeter se duhet ne llogaritjet per recepturat - //tocheck Nestila sigurt qe punon kjo?
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: arrayReadOnlyKolonaGrides[9] == 'True' ? "True" : "False", indexRow: idRresht, id: "txtSasiaAktuale", onKeyDown: keyup, onKeyUp: changedSasiAkt, onFocusout: focusOutSasiaAkt });
}

function myelemGjeresi(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: arrayReadOnlyKolonaGrides[6] == 'True' ? "True" : "False", indexRow: idRresht, id: "txtGjeresiPlanifikuar", onKeyDown: keyup, onFocusout: keyup });
}

function myelemGjatesi(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: arrayReadOnlyKolonaGrides[7] == 'True' ? "True" : "False", indexRow: idRresht, id: "txtGjatesiPlanifikuar", onKeyDown: keyup, onFocusout: keyup });
}

function myelemSasiPor(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: arrayReadOnlyKolonaGrides[8] == 'True' ? "True" : "False", indexRow: idRresht, id: "txtSasiaPlanifikuar", onKeyDown: keyup, onFocusout: keyup });
}

function myelemKosto(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: arrayReadOnlyKolonaGrides[10], indexRow: idRresht, id: arrayIdKolonaGrides[10], onKeyDown: keyup, onFocusout: keyup });
}

function myelemKostoTotale(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: arrayReadOnlyKolonaGrides[11], indexRow: idRresht, id: arrayIdKolonaGrides[11], onKeyDown: keyup, onFocusout: keyup });
}

function myelemComboMagazina(value) {//po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    magazina1 = btneMagazina.GetText();
    if (value == "" && magazina1 != "" && magazina1 != undefined)
        value = magazina1;
    var arrayOptions = new Array(0);
    if (colMagazina !== "") {
        arrayOptions = new Array(colMagazina.length);
        for (var i = 0; i < colMagazina.length; i++) {
            var objNjesi = new Object();
            objNjesi.value = colMagazina[i].IdNjesiAdministrative;
            objNjesi.text = colMagazina[i].Kodi;
            arrayOptions[i] = objNjesi;
        }
    }
    var disabled;
    if (arrayReadOnlyKolonaGrides[12] == 'True')
        disabled = true;
    else
        disabled = false;
    return myJQGrid.myElemCombo(value, 'txtIdMag', idRresht, changemagProd, arrayOptions, disabled);
}

function myelemSasiPer(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: arrayReadOnlyKolonaGrides[13] == 'True' ? "True" : "False", indexRow: idRresht, id: "txtSasiPermase", onKeyDown: keyup, onFocusout: keyup });
}

function mbushArrayMagazinat() {//po
    var colMag = $('#hfTmpColMag').val();
    if (colMag != '') {
        colMagazina = $.parseJSON(colMag);
        $('#hfTmpColMag').val(''); //boshatisim HF-ne qe mos te harxhojme kot memorie
    }
}

function myValueButtonFshi(elem, operation, value) {//po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1] == 'True')
        return myJQGrid.myValueButtonFshi(true, idRresht, "#rowed5");
    else
        return myJQGrid.myValueButtonFshi(false, idRresht, "#rowed5");
}

function myElemButtonFshi() {//po

    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    callWebServiceInfoRow();
    if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1] == 'True')
        return myJQGrid.myElemButtonFshi(true, idRresht, "#rowed5", lostFocusKoloneFundit);
    else
        return myJQGrid.myElemButtonFshi(false, idRresht, "#rowed5", lostFocusKoloneFundit);
}

function ndryshosasi() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ndryshoNjesi"),
        data: JSON.stringify({ key: idRresht, njesia: $('#txtIdNjesia' + idRresht).val(), idart: grida.getTekstQelize('txtIdArtikulli', idRresht) })
    }).done(SucceededCallbackNjesi);
}

function SucceededCallbackNjesi(result) {
    var grida = $('#rowed5');
    grida.setTekstQelize('txtKosto', result[0], grida.getTekstQelize('txtKosto', result[0]) / result[1]);
    grida.setTekstQelize('txtSasiaPlanifikuar', result[0], grida.getTekstQelize('txtSasiaPlanifikuar', result[0]) * result[1]);
    grida.setTekstQelize('txtSasiaAktuale', result[0], grida.getTekstQelize('txtSasiaAktuale', result[0]) * result[1]);
    sasiavjeter = parseFloat(grida.getTekstQelize('txtSasiaAktuale', result[0]));
}

function fshiRecepturatEProduktitNgaGrida(indexPrindi) {
    var grida = $("#rowed6");
    var idRow = grida.getLastSel2();
    grida.jqGrid('saveRow', idRow, null, 'clientArray', {}, null);
    var rreshtat = grida.jqGrid('getGridParam', 'data');
    var indekset = new Array();
    var idTe = grida.jqGrid('getDataIDs');

    for (var ind = 0; ind < rreshtat.length; ind++) {
        if (rreshtat[ind].txtIndexPrindi == indexPrindi) {
            indekset.push(ind);
        }
        rreshtat[ind].id = idTe[ind];
    }

    for (var j = indekset.length - 1; j >= 0; j--) {
        rreshtat.splice(indekset[j], 1);
        //idTe.splice(indekset[j], 1);
    }

    //for (var i = 0; i < rreshtat.length; i++) {
    //    rreshtat[i].id = idTe[i];
    //}

    grida.jqGrid('clearGridData');
    grida.jqGrid('setGridParam', {
        datatype: 'local',
        data: rreshtat,
        rowNum: 10000000
    })
        .trigger("reloadGrid");
    //$("#rowed6")[0].addJSONData(rreshtat);
    //sistemoNrRendorDheBtnFshi();
    llogaritkostoprindi(indexPrindi);
}

function sistemoNrRendorDheBtnFshi() {
    var rreshtat = $("#rowed6").jqGrid('getRowData');
    var idTe = $("#rowed6").jqGrid('getDataIDs');
    for (var i = 0; i < idTe.length; i++) {
        $("#rowed6").setTekstQelize('txtFshiR', idTe[i], '<input id="butonFshi1" type="image" value="Fshi" onmouseover="ndryshoImazhin(1,' + idTe[i] + ')" onmouseout="ndryshoImazhin(0,' + idTe[i] + ')" src="images/square-icon.png" onclick="fshiClicked2(' + idTe[i] + ')">');
        $("#rowed6").setTekstQelize('txtNrRendor', idTe[i], $("#rowed6").getInd(idTe[i], false));
    }
    //window.lastsel3 = idTe[idTe.length - 1] + 1;
}

function fshiClicked(index) {//po
    $('#butonFshi' + index).prop('disabled', true);
    var grida = $("#rowed6");
    var gridaP = $("#rowed5");
    var idRresht = grida.getLastSel2();
    //var idTe = grida.jqGrid('getDataIDs');
    //for (var j = 0; j < idTe.length; j++) {
    //    if (grida.getTekstQelize('txtIndexPrindi', idTe[j]) == index)
    //        fshiClicked2(idTe[j]);
    //}
    fshiRecepturatEProduktitNgaGrida(index);
    var idTe = grida.jqGrid('getDataIDs');
    for (var j = 0; j < idTe.length; j++) {
        grida.setTekstQelize('txtNrRendor', idTe[j], grida.getInd(idTe[j], false));
    }
    $('#butonFshi' + index).prop('disabled', false);
    //var ids = gridaP.getDataIDs();
    //for (var i = 0; i < ids.length; i++) {
    //    if (ids[i] <= index)
    //        continue;
    //    if (gridaP.getTekstQelize('txtNrRendor', ids[i]) == undefined || typeof (gridaP.getTekstQelize('txtNrRendor', ids[i])) == 'undefined' || gridaP.getTekstQelize('txtNrRendor', ids[i]) == '')
    //        continue;
    //    gridaP.setTekstQelize('txtNrRendor', ids[i], parseInt(gridaP.getTekstQelize('txtNrRendor', ids[i])) - 1);
    //}
    myJQGrid.fshiClicked(index, '#rowed5', inicializoGride);
    gridaP.rregulloNrRendorMeTeMadh(index);
}

function selectFunc(event, ui, emerfushe, idArt, kodArt) { //po
    var mag1 = 0;
    if (btneMagazina.GetText() != '')
        mag1 = btneMagazina.GetValue();
    var mag2 = 0;
    if (btneMagazin2a.GetText() != '')
        mag2 = btneMagazin2a.GetValue();
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if (idArt != undefined && idArt != "" && !isNaN(parseFloat(idArt)) && parseFloat(idArt) > 0 && kodArt != undefined && kodArt != "" && $(emerfushe).val() !== undefined) {
        $(emerfushe).val(kodArt);
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraArtMeIDMeRec"),
            data: JSON.stringify({ idja: idArt, rreshti: idRresht, data: dteDtDok.GetDate(), magazina: $('#txtIdMag' + idRresht).val(), magazina2: mag2, sasiplanrec: sasiplanrec })
        }).done(SucceededCallbackArt);
        return false;
    }
    if (ui !== null && ui.item != null) {
        $(emerfushe).val(ui.item.label);
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraArtMeIDMeRec"),
            data: JSON.stringify({ idja: ui.item.value, rreshti: idRresht, data: dteDtDok.GetDate(), magazina: $('#txtIdMag' + idRresht).val(), magazina2: mag2, sasiplanrec: sasiplanrec })
        }).done(SucceededCallbackArt);
        return false;
    }
}
function changeFunc(event, ui, emerKodi, index) {//po
    //var index = -1;
    //var idKod = 'txtKodiArtikull';
    var mag1 = 0;
    if (btneMagazina.GetText() != '')
        mag1 = btneMagazina.GetValue();
    var mag2 = 0;
    if (btneMagazin2a.GetText() != '')
        mag2 = btneMagazin2a.GetValue();
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    if (index == idRow) {


        var vleraKodit = grida.getTekstQelize(emerKodi, index);
        if (ui == null || ui.item == null) {
            if (vleraKodit != "") {

                $.ajax({
                    pritPergjigje: true,
                    url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraArtMeRec"),
                    data: JSON.stringify({ kodi: vleraKodit, rreshti: idRow, data: dteDtDok.GetDate(), magazina: $('#txtIdMag' + idRow).val(), magazina2: mag2, sasiplanrec: sasiplanrec })
                }).done(SucceededCallbackArt);

                return;
            }
            vendosArt({ rreshti: idRow, artKryesor: null });
            return;
        }

        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraArtMeRec"),
            data: JSON.stringify({ kodi: ui.item.label, rreshti: idRow, data: dteDtDok.GetDate(), magazina: $('#txtIdMag' + idRow).val(), magazina2: mag2, sasiplanrec: sasiplanrec })
        }).done(SucceededCallbackArt);

        return;
    }
    var rreshti = grida.jqGrid('getRowData', index);

    if (ui == null || ui.item == null) {
        if (rreshti.txtKodiArtikull) {
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraArtMeRec"),
                data: JSON.stringify({ kodi: rreshti.txtKodiArtikull, rreshti: index, data: dteDtDok.GetDate(), magazina: $('#txtIdMag' + idRow).val(), magazina2: mag2, sasiplanrec: sasiplanrec })
            }).done(SucceededCallbackArt);
            return;
        }
        vendosArt({ rreshti: index, artKryesor: null });
        return;
    }
}

function resetRreshtKorent(idRreshti) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    var grida2 = $("#rowed6");
    var idTe;
    if (idRreshti == idRow && grida.getTekstQelize('txtKodiArtikull', idRow) != undefined) {
        grida.setTekstQelize('txtKodiArtikull', idRreshti, '');
        grida.setTekstQelize('txtIdArtikulli', idRreshti, '');
        grida.setTekstQelize('txtIdKoka', idRreshti, '');
        grida.setTekstQelize('txtPershkrimArtikull', idRreshti, '');
        grida.setTekstQelize('txtKosto', idRreshti);
        grida.setTekstQelize('txtIdNjesia', idRreshti, '');
        grida.setTekstQelize('txtGjeresiPlanifikuar', idRreshti);
        grida.setTekstQelize('txtGjatesiPlanifikuar', idRreshti);
        grida.setTekstQelize('txtSasiaPlanifikuar', idRreshti);
        grida.setTekstQelize('txtKostoTotale', idRreshti);
        grida.setTekstQelize('txtIdPlanifikim', idRreshti, '');
        grida.setTekstQelize('txtIdUrdherPorosi', idRreshti, '');
        grida.setTekstQelize('txtSasiaAktuale', idRreshti);
        grida.setTekstQelize('txtSasiPermase', idRreshti);
        grida.setTekstQelize('txtShenime', idRreshti, '');
        grida.setTekstQelize('txtDetajimi', idRreshti, '');
        grida.setTekstQelize('txtDetajimi2t', idRreshti, '');

        idTe = grida2.jqGrid('getDataIDs');
        for (var j = 0; j < idTe.length; j++) {
            if (grida2.getTekstQelize('txtIndexPrindi', idTe[j]) == idRreshti)
                fshiClicked2(idTe[j]);
        }
        return;
    }
    grida.jqGrid('delRowData', idRreshti);
    grida.rregulloNrRendorMeTeMadh(idRreshti);
    idTe = grida2.jqGrid('getDataIDs');
    for (var j = 0; j < idTe.length; j++) {
        if (grida2.getTekstQelize('txtIndexPrindi', idTe[j]) == idRreshti)
            fshiClicked2(idTe[j]);
    }
    return;
}

function vendosArt(result) {
    var grida = $("#rowed5");
    var idRow = grida.getLastSel2();
    if (result == null)
        return;

    var idRreshti = result.rreshti;
    var artikulli = result.artkryesor;

    var kodi = "#txtKodiArtikull" + idRreshti;
    var idKodi = "txtIdArtikulli";
    var emerArt = 'txtPershkrimArtikull';
    if (artikulli == null || artikulli == undefined || artikulli.IdArtikulli == -1) {
        resetRreshtKorent(idRreshti);
        return;
    }
    if (artikulli.Klasa != 5 && artikulli.Klasa != 6) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKyArtikullNukIPerketKlasesProdhimOsePProces"));
        resetRreshtKorent(idRreshti);
        return;
    }
    if (artikulli.Aktiv == false) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgArtikulliEshteInaktiv"));
        resetRreshtKorent(idRreshti);
        return;
    }

    pageState.memoryArt.Set(artikulli);
    var comboNjesia = $('#txtIdNjesia' + idRreshti);
    var arrayOptions = new Array();
    var tmpObjNjesi = new Object();
    tmpObjNjesi.value = artikulli.Njesi1Artikulli;
    tmpObjNjesi.text = artikulli.KodNjesia1;
    arrayOptions[0] = tmpObjNjesi;
    if (artikulli.Njesi1Artikulli !== artikulli.Njesi2Artikulli) {
        tmpObjNjesi = new Object();
        tmpObjNjesi.value = artikulli.Njesi2Artikulli;
        tmpObjNjesi.text = artikulli.KodNjesia2;
        arrayOptions[1] = tmpObjNjesi;
    }
    if (grida.getTekstQelize(idKodi, idRow) == artikulli.IdArtikulli || grida.getTekstQelize(idKodi, idRreshti) == artikulli.IdArtikulli) {
        grida.setTekstQelize('txtKodiArtikull', idRreshti, artikulli.KodArtikulli);
        return; //eshte i njejti artikull 
    }
    var grida1 = $("#rowed6");
    var idRowR = grida1.getLastSel2();
    grida1.jqGrid('saveRow', idRowR, false, 'clientArray');
    grida1.setLastSel2(-1);
    fshiRecepturatEProduktitNgaGrida(idRow);
    grida.setTekstQelize('txtKodiArtikull', idRreshti, artikulli.KodArtikulli);
    if (idRreshti == idRow && $(kodi).val() != undefined)
        myJQGrid.closeAutocomplete('txtKodiArtikull', idRreshti);
    grida.setTekstQelize('txtNrRendor', idRreshti, grida.getInd(idRreshti, false));
    grida.setTekstQelize('txtIdArtikulli', idRreshti, artikulli.IdArtikulli);
    grida.setTekstQelize(emerArt, idRreshti, artikulli.PershkrimArtikulli);

    grida.setTekstQelize('txtKosto', idRreshti, result.prod.Kosto);
    grida.setTekstQelize('txtKostoTotale', idRreshti, result.prod.KostoTotale);
    if (idRreshti == idRow && $(kodi).val() != undefined) {
        var mag = btneMagazina.GetText();
        if (artikulli.IdMagazina != 0 && mag == '') //magazina 
            $('#txtIdMag' + idRreshti).val(artikulli.IdMagazina);
        comboNjesia.replaceWith(myJQGrid.myElemCombo(artikulli.KodNjesia1, 'txtIdNjesia', idRow, ndryshosasi, arrayOptions, false).children()[0]);
    }
    else {
        if (artikulli.IdMagazina != 0) //magazina
            grida.setTekstQelize('txtIdMag', idRreshti, artikulli.Magazina);
        grida.setTekstQelize('txtIdNjesia', idRreshti, artikulli.KodNjesia1);
    }

    GjendjaMinMaxArtikull();
    var idProd = result.teDhenaRecepturash.length > 0 ? result.teDhenaRecepturash[0].rec.IdProdukti : 0;
    pageState.hapurpermodifikim = false;
    vendosRec(result.teDhenaRecepturash, idRow, idProd, true);

    totali();
    if (idRreshti == idRow && $(kodi).val() != undefined)
        callWebServiceInfoRow();
}

function vendosRecepe(result) {
    var grida = $("#rowed5");
    if (result == null)
        return;
    var idRreshti = result.rreshti;
    var artikulli = result.artkryesor;
    grida.setTekstQelize('txtKosto', idRreshti, result.prod.Kosto);
    grida.setTekstQelize('txtKostoTotale', idRreshti, result.prod.KostoTotale);
    var idProd = result.teDhenaRecepturash.length > 0 ? result.teDhenaRecepturash[0].rec.IdProdukti : 0;
    vendosRec(result.teDhenaRecepturash, idRreshti, idProd, true);
    totali();
}

function SucceededCallbackArt(result) {//po
    vendosArt(result);
}

function SucceededCallbackArtRec(result) {//po
    vendosRecepe(result);
}

function formGridColsArray() {//po
    var IdKonfigAmbjenteLupat = [];
    myJQGrid.formArrayKolGridesNew(pageState.colGridaProdukti, arrayIdKolonaGrides, arrayPershkrimiKolonaGrides, arrayVisibleKolonaGrides, arrayReadOnlyKolonaGrides, lidhur, arrayWidthKolonaGrides, IdKonfigAmbjenteLupat, arrayRenditjeKolonaGrides);
    myJQGrid.formArrayKolGridesNew(pageState.colGridaReceptura, arrayIdKolonaSubGrides, arrayPershkrimiKolonaSubGrides, arrayVisibleKolonaSubGrides, arrayReadOnlyKolonaSubGrides, lidhur, arrayWidthKolonaSubGrides, IdKonfigAmbjenteLupat, arrayRenditjeKolonaSubGrides);
}


function mbushGrideNgaHiddenFieldet() {
    if (arrayPershkrimiKolonaGrides.length == 0)
        return;

    if ($('#hfShtimModifikim').val() == "shtim") {
        //myJQGrid.keyPressKodi("#rowed5", window.lastsel2, arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1]);
        return;
    }
    if ($('#hfShtimModifikim').val() == "modifikim") {
        var colArt = JSON.parse($('#HfColArt').val());
        mbushGrideMePlanifikimin(colArt);
        //SucceededCallbackMbushGride(colArt);
    }
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

function lostFocusKoloneFundit() {     //po 
    var grida = $("#rowed5");
    var idRow = grida.getLastSel2();
    idRow = grida.lostFocusKoloneFundit();
    if ($('#' + arrayIdKolonaGrides[1] + idRow).attr("disabled") == 'disabled') {
        $('#txtKodiArtikull' + idRow).focus();
        $('#txtKodiArtikull' + idRow).blur();
        $('#txtKodiArtikull' + idRow).focus();
    }
}

function keyup() {

}

var sasiavjeter = 1;

function changedSasiAkt() {
    var grida = $("#rowed5");
    var idRow = grida.getLastSel2();
    GjendjaMinMaxArtikull();
    ndryshoSasiteERecepturave(idRow, true, pageState.hapurpermodifikim);

}

function focusOutSasiaAkt() {
    var grida = $("#rowed5");
    var idRow = grida.getLastSel2();
    GjendjaMinMaxArtikull();
    ndryshoSasiteERecepturave(idRow, true, true);
}

function GjendjaMinMaxArtikull() {
    var tabIndex = PageControl.GetActiveTabIndex();
    var grida = ktheGrideSipasTabit(tabIndex);
    var idRow = grida.getLastSel2();
    var idNdermarrje = hfState.Get('idNdermarrje');
    var editorIdArtikulli = ktheEmerEditoreshSipasTabit(tabIndex).emerIdArt;
    var editorIdMag = ktheEmerEditoreshSipasTabit(tabIndex).emerIdMag;
    var editorSasia = ktheEmerEditoreshSipasTabit(tabIndex).sasia;
    var editorKodi = ktheEmerEditoreshSipasTabit(tabIndex).kodi;
    var idkodi = grida.getTekstQelize(editorIdArtikulli, idRow);
    var kodi = grida.getTekstQelize(editorKodi, idRow);
    var ids = grida.getDataIDs();
    var totali = 0;
    var artikulli = pageState.memoryArt.Get(idkodi);
    for (var i = 0; i < ids.length; i++) {
        var sasia = parseFloat(grida.getTekstQelize(editorSasia, ids[i]));
        if (grida.getTekstQelize(editorIdArtikulli, ids[i]) == idkodi)
            totali = totali + sasia;
    }

    if (pageState.kushte.KGJMM && !Utils.IsNullOrEmpty(kodi)) {
        if (!Utils.IsNullOrEmpty(idkodi) && idkodi != "0") {
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "ktheGjendjeArtikulli"),
                data: JSON.stringify({
                    idja: idkodi, mag: grida.getTekstQelize(editorIdMag, idRow), data: dteDtDok.GetDate(), koddetajim: '', koddetajim2: '', iddok: Utils.IsUndefined(Utils.getUrlVar('id')) ? 0 : Utils.getUrlVar('id'),
                    idndermarje: idNdermarrje, idKonfigAmbjente: cmbKonfigurimi.GetValue(),
                    idreshti: idRow, totalartikulli: totali, totaldetajim1: 0, totaldetajim2: 0, shitje_blerje: false, ekzekutimProdhim: true

                })
            }).done(SucceededCallbackKontrollGjendje);

        }
    }

}


function SucceededCallbackKontrollGjendje(result) {
    if (result.PershkrimMesazhi != "Ne rregull")
        myMesazh.ShtoMesazhInformues(result.PershkrimMesazhi);
}


function ndryshoSasiteERecepturave(idRow, merrKostoMag, hapurpermodifikim) {
    var grida = $("#rowed5");
    var idNdermarrje = hfState.Get('idNdermarrje');

    //nese rreshti ku po ndryshon sasine ska artikull te ngarkuar (produkt), ske pse ndryshon sasite e recepturave
    if (Utils.IsNullOrEmpty(grida.getTekstQelize('txtIdArtikulli', idRow))) return;

    editorSasia = parseFloat(grida.getTekstQelize("txtSasiaAktuale", idRow));
    var gjeresi;
    if (editorSasia == "" || isNaN(editorSasia)) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiaAktualeDuhetTeJeteNr"));
        sasia = grida.getVlereDefault('txtSasiaAktuale');
        grida.setTekstQelize('txtSasiaAktuale', idRow, sasia);
        $('#txtSasiaAktuale' + idRow).focus();
        editorSasia = "1";
    }
    else
        if (editorSasia == "0") {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiaAktualeNukMundTeJeteZero"));
            sasia = grida.getVlereDefault('txtSasiaAktuale');
            grida.setTekstQelize('txtSasiaAktuale', idRow, sasia);
            $('#txtSasiaAktuale' + idRow).focus();
        }
    grida.setTekstQelize('txtKostoTotale', idRow, (editorSasia * grida.getTekstQelize('txtKosto', idRow)));

    var grida2 = $("#rowed6");
    var idTe = grida2.jqGrid('getDataIDs');
    for (var j = 0; j < idTe.length; j++) {
        if (grida2.getTekstQelize('txtIndexPrindi', idTe[j]) == idRow && grida2.getTekstQelize('txtSkedulim', idTe[j]) == '') {
            if (grida2.getTekstQelize('txtSasiaAktualeR', idTe[j]) == grida2.getTekstQelize('txtSasia', idTe[j]))
                grida2.setTekstQelize('txtSasiaAktualeR', idTe[j], grida2.getTekstQelize('txtSasia', idTe[j]) * editorSasia / sasiavjeter);
            else
                grida2.setTekstQelize('txtSasiaAktualeR', idTe[j], grida2.getTekstQelize('txtSasiaAktualeR', idTe[j]) * editorSasia / sasiavjeter);


            if (Math.round(grida2.getTekstQelize('txtScrap', idTe[j]), 2) == Math.round(grida2.getTekstQelize('txtFiroPerqindje', idTe[j]) * grida2.getTekstQelize('txtSasia', idTe[j]) / (100 * (1 + grida2.getTekstQelize('txtFiroPerqindje', idTe[j]) / 100)), 2))
                grida2.setTekstQelize('txtScrap', idTe[j], grida2.getTekstQelize('txtScrap', idTe[j]) * editorSasia / sasiavjeter);
        }
        if (isNaN(editorSasia)) {
            editorSasia = 1;

        }
        if (sasiplanrec === 'sasi akt')
            grida2.setTekstQelize('txtSasia', idTe[j], (grida2.getTekstQelize('txtSasia', idTe[j]) * editorSasia / sasiavjeter));
        grida2.setTekstQelize('txtKostoTotaleR', idTe[j], grida2.getTekstQelize('txtSasiaAktualeR', idTe[j]) * grida2.getTekstQelize('txtKostoR', idTe[j]));
        var totalirec = 0;
        var idkodi = grida2.getTekstQelize("txtIdArtikulliR", idTe[j]);
        var ids = grida2.getDataIDs();
        var artikulli = pageState.memoryArt.Get(idkodi);
        for (var i = 0; i < ids.length; i++) {
            var editorSasiaRec = parseFloat(grida2.getTekstQelize("txtSasiaAktualeR", ids[i]));
            if (grida2.getTekstQelize("txtIdArtikulliR", ids[i]) == idkodi)
                totalirec = totalirec + editorSasiaRec;
        }
        if (pageState.kushte.KGJMM && !Utils.IsNullOrEmpty(idkodi) && idkodi != "0" && !hapurpermodifikim && grida2.getTekstQelize('txtIndexPrindi', idTe[j]) == idRow) {

            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "ktheGjendjeArtikulli"),
                data: JSON.stringify({
                    idja: idkodi, mag: grida2.getTekstQelize("txtIdMagR", idTe[j]), data: dteDtDok.GetDate(), koddetajim: '', koddetajim2: '', iddok: Utils.IsUndefined(Utils.getUrlVar('id')) ? 0 : Utils.getUrlVar('id'),
                    idndermarje: idNdermarrje, idKonfigAmbjente: cmbKonfigurimi.GetValue(),
                    idreshti: idTe[j], totalartikulli: totalirec, totaldetajim1: 0, totaldetajim2: 0, shitje_blerje: false, ekzekutimProdhim: true
                })
            }).done(SucceededCallbackKontrollGjendje);
        }

    }
    if (merrKostoMag)
        merrkostomag(idTe);
    llogaritkostoprindi(idRow);
    sasiavjeter = editorSasia; //vendoset vlera e re per ndryshimet e mevonshme
    totali();
}


function SucceededCallbackComboNjesiArtikulli(result) {//po
    var idreshti = result[0];
    var textzgjedhur = result[1];
    comboListNjesiArt = result[2];
    var grida = $("#rowed5");
    var idRow = grida.getLastSel2();
    if (idreshti == idRow && $('#' + 'txtIdNjesia' + idRow).val() != undefined)
        $('#' + 'txtIdNjesia' + idreshti).replaceWith(myJQGrid.myElemCombo(textzgjedhur, 'txtIdNjesia', idreshti, ndryshosasi, comboListNjesiArt, false).children()[0]);
    else {
        grida.setTekstQelize('cmbNjesia', idreshti, textzgjedhur);
    }
}

function keyPressKodi(event) {     //po
    var grida = $("#rowed5");
    var idRow = grida.getLastSel2();
    try {
        var vlera = event.target.value;
        if (vlera == "")
            $('#' + arrayIdKolonaGrides[3] + idRow).autocomplete("close");
        else
            callWebserviceKodi(vlera);
    }
    catch (e) { }
}

function callWebserviceKodi(vlera) {//po
    var grida = $('#rowed5');
    //var idPerdoruesi = hfState.Get('idPerdoruesi');
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheArtikullPerProdhim"),
        data: JSON.stringify({ prefixText: vlera, klasa: 0 })
    }).done(SucceededCallbackKodi);
}

function SucceededCallbackKodi(result) {
    var grida = $("#rowed5");
    var idRow = grida.getLastSel2();
    myJQGrid.SucceededCallbackKodi(result, '#txtKodiArtikull' + idRow);
}

var arrayIdKolonaSubGrides = new Array();
var arrayPershkrimiKolonaSubGrides = new Array();
var arrayVisibleKolonaSubGrides = new Array();
var arrayWidthKolonaSubGrides = new Array();
var arrayReadOnlyKolonaSubGrides = new Array();
var arrayRenditjeKolonaSubGrides = new Array();
var arrayIndexTrupi = new Array();
var lastsel3;

function inicializoGrideR() {
    if (arrayPershkrimiKolonaSubGrides.length == 0)
        return;
    var classes = '';
    if (lidhur === true)
        classes = 'uigray';
    var arrayPershkrime = [
        'id', arrayPershkrimiKolonaSubGrides[0], arrayPershkrimiKolonaSubGrides[1], arrayPershkrimiKolonaSubGrides[2], arrayPershkrimiKolonaSubGrides[3],
        arrayPershkrimiKolonaSubGrides[4], arrayPershkrimiKolonaSubGrides[5], arrayPershkrimiKolonaSubGrides[6], arrayPershkrimiKolonaSubGrides[7],
        arrayPershkrimiKolonaSubGrides[8], arrayPershkrimiKolonaSubGrides[9], arrayPershkrimiKolonaSubGrides[10], arrayPershkrimiKolonaSubGrides[11],
        arrayPershkrimiKolonaSubGrides[12], arrayPershkrimiKolonaSubGrides[13], arrayPershkrimiKolonaSubGrides[14], arrayPershkrimiKolonaSubGrides[15],
        arrayPershkrimiKolonaSubGrides[16], arrayPershkrimiKolonaSubGrides[17], arrayPershkrimiKolonaSubGrides[18], arrayPershkrimiKolonaSubGrides[19],
        arrayPershkrimiKolonaSubGrides[20], arrayPershkrimiKolonaSubGrides[21]
    ];
    var arrayModel = [
        { name: 'id', index: 'id', width: 0, hidden: true, classes: classes, sortable: false, editable: false, edittype: 'custom' },
        { name: arrayIdKolonaGrides[0], index: arrayIdKolonaGrides[0], width: arrayWidthKolonaGrides[0], hidden: arrayVisibleKolonaGrides[0], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemNrRendorR, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaSubGrides[1], index: arrayIdKolonaSubGrides[1], width: arrayWidthKolonaSubGrides[1], hidden: arrayVisibleKolonaSubGrides[1], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemIdProdukti, custom_value: myJQGrid.myValueTextBox }, hidedlg: true },
        { name: arrayIdKolonaSubGrides[2], index: arrayIdKolonaSubGrides[2], width: arrayWidthKolonaSubGrides[2], hidden: arrayVisibleKolonaSubGrides[2], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemKodProdukti, custom_value: myJQGrid.myValueCombo } },
        { name: arrayIdKolonaSubGrides[3], index: arrayIdKolonaSubGrides[3], width: arrayWidthKolonaSubGrides[3], hidden: arrayVisibleKolonaSubGrides[3], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemEmerProdukti, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaSubGrides[4], index: arrayIdKolonaSubGrides[4], width: arrayWidthKolonaSubGrides[4], hidden: arrayVisibleKolonaSubGrides[4], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemCombo, custom_value: myJQGrid.myValueCombo } },
        { name: arrayIdKolonaSubGrides[5], index: arrayIdKolonaSubGrides[5], width: arrayWidthKolonaSubGrides[5], hidden: arrayVisibleKolonaSubGrides[5], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemIdArtikulliR, custom_value: myJQGrid.myValueTextBox }, hidedlg: true },
        { name: arrayIdKolonaSubGrides[6], index: arrayIdKolonaSubGrides[6], width: arrayWidthKolonaSubGrides[6], hidden: arrayVisibleKolonaSubGrides[6], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemKodiR, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaSubGrides[7], index: arrayIdKolonaSubGrides[7], width: arrayWidthKolonaSubGrides[7], hidden: arrayVisibleKolonaSubGrides[7], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemPershkrimiR, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaSubGrides[8], index: arrayIdKolonaSubGrides[8], width: arrayWidthKolonaSubGrides[8], hidden: arrayVisibleKolonaSubGrides[8], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemComboNjesiaR, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaSubGrides[9], index: arrayIdKolonaSubGrides[9], width: arrayWidthKolonaSubGrides[9], hidden: arrayVisibleKolonaSubGrides[9], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemIdBurimeR, custom_value: myJQGrid.myValueTextBox }, hidedlg: true },
        { name: arrayIdKolonaSubGrides[10], index: arrayIdKolonaSubGrides[10], width: arrayWidthKolonaSubGrides[10], hidden: arrayVisibleKolonaSubGrides[10], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemSasiAktR, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaSubGrides[11], index: arrayIdKolonaSubGrides[11], width: arrayWidthKolonaSubGrides[11], hidden: arrayVisibleKolonaSubGrides[11], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemSasiPorR, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaSubGrides[12], index: arrayIdKolonaSubGrides[12], width: arrayWidthKolonaSubGrides[12], hidden: arrayVisibleKolonaSubGrides[12], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemFiro, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaSubGrides[13], index: arrayIdKolonaSubGrides[13], width: arrayWidthKolonaSubGrides[13], hidden: arrayVisibleKolonaSubGrides[13], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemScrap, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaSubGrides[14], index: arrayIdKolonaSubGrides[14], width: arrayWidthKolonaSubGrides[14], hidden: arrayVisibleKolonaSubGrides[14], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemKostoR, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaSubGrides[15], index: arrayIdKolonaSubGrides[15], width: arrayWidthKolonaSubGrides[15], hidden: arrayVisibleKolonaSubGrides[15], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemKostoTotaleR, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaSubGrides[16], index: arrayIdKolonaSubGrides[16], width: arrayWidthKolonaSubGrides[16], hidden: arrayVisibleKolonaSubGrides[16], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemComboMagazinaR, custom_value: myJQGrid.myValueCombo } },
        { name: arrayIdKolonaSubGrides[17], index: arrayIdKolonaSubGrides[17], width: arrayWidthKolonaSubGrides[17], hidden: arrayVisibleKolonaSubGrides[17], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemIndexPrindi, custom_value: myJQGrid.myValueTextBox }, hidedlg: true },
        { name: arrayIdKolonaSubGrides[18], index: arrayIdKolonaSubGrides[18], width: arrayWidthKolonaSubGrides[18], hidden: arrayVisibleKolonaSubGrides[18], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemSkedulim, custom_value: myJQGrid.myValueTextBox }, hidedlg: true },
        { name: arrayIdKolonaSubGrides[19], index: arrayIdKolonaSubGrides[19], width: arrayWidthKolonaSubGrides[19], hidden: arrayVisibleKolonaSubGrides[19], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemDetajimi, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaSubGrides[20], index: arrayIdKolonaSubGrides[20], width: arrayWidthKolonaSubGrides[20], hidden: arrayVisibleKolonaSubGrides[20], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemDetajimi2, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaSubGrides[21], index: arrayIdKolonaSubGrides[21], width: arrayWidthKolonaSubGrides[21], hidden: arrayVisibleKolonaSubGrides[21], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemButtonFshiR, custom_value: myValueButtonFshiR }, hidedlg: true }
    ];
    lidhur = $("#hfLidhur").val() === 'True';
    var selektoriGrides = "#rowed6";

    var gridParams = {
        emergride: selektoriGrides,
        arrayPershkrime: arrayPershkrime,
        arrayModel: arrayModel,
        lidhur: lidhur,
        emerEditorKodi: "txtKodiArtikullR",
        emerEditorLloji: "txtLloji",
        widthi: $('#divgride4')[0].offsetWidth,
        resetRreshtKorent: resetRreshtKorentR,
        lostFocusKoloneFundit: lostFocusKoloneFunditR,
        arrayRenditjeKolonaGridesName: arrayRenditjeKolonaSubGrides,
        arrayReadOnlyKolonaGrides: arrayReadOnlyKolonaSubGrides,
        autocompleteList: [{ emerEditor: "txtKodiArtikullR", selectFunc: selectFunca, changeFunc: changeFunca, shtoDataKod: false },
        { emerEditor: "txtDetajimi", selectFunc: selectFunca2, changeFunc: changeFunca2, shtoDataKod: false },
        { emerEditor: "txtDetajimi2t", selectFunc: selectFunca3, changeFunc: changeFunca3, shtoDataKod: false }],
        konfigToolbar: {
            kaTeDrejtaArkive: $('#hfTeDrejtaArtImazhe').val(),
            hapPopUpImazheArkive: onHapPopUpImazheArkive,
            hapPopUpArkive: onHapLupeArkive
        }

    };
    return myJQGrid.initGride(gridParams);
}

/*
Vendos formatet e numrave ne gride ne baze te emrit te kolones
*/
function ruajFormatetNeGrideR(grida, formatNumri) {
    if (typeof formatNumri == 'undefined')
        formatNumri = $.parseJSON(hfState.Get("formatMonedhe"));
    ndryshoKonfigFormatNumriR(grida, formatNumri);
    vendosVleraDefaultNeGrideR(grida);
    return;
}
/*
Vendos vlerat default te fushave ne gride ne baze te emrit te kolones
*/
function vendosVleraDefaultNeGrideR(grida) {
    grida.setVlereDefault('txtSasia', 0);
    grida.setVlereDefault('txtScrap', 0);
    grida.setVlereDefault('txtKostoR', 1);
    grida.setVlereDefault('txtKostoTotaleR', 1);
    grida.setVlereDefault('txtSasiaAktualeR', 1);
    grida.setVlereDefault('txtFiroPerqindje', 0);
    return;
}

function ndryshoKonfigFormatNumriR(grida, formatNumri, formatkursi) {
    ///<summary> metode per ndryshimin e formateve te nr ne gride ne baze te emrit te kolones. gjithashtu vendos formatin e nr dhe per fushat e devexpresit ne baze te emrit te kontrollit</summary>
    ///<param name="formatNumri"> objekt i tipit format nr me te dhenat e formatimit</param>
    ///<param name="formatkursi">sherben per te vendosur formatin e kursit ne varesi te monedhes </param>
    if (typeof formatNumri == 'undefined')
        formatNumri = $.parseJSON(hfState.Get("formatMonedhe"));
    else
        hfState.Set("formatMonedhe", JSON.stringify(formatNumri));
    grida.setShifraPasPresjes('txtSasia', formatNumri.ShifraPasPresjesSasia);
    grida.setShifraPasPresjes('txtScrap', formatNumri.ShifraPasPresjesSasia);
    grida.setShifraPasPresjes('txtKostoR', formatNumri.ShifraPasPresjesCmimi);
    grida.setShifraPasPresjes('txtKostoTotaleR', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtSasiaAktualeR', formatNumri.ShifraPasPresjesSasia);
    grida.setShifraPasPresjes('txtFiroPerqindje', formatNumri.ShifraPasPresjesSasia);
}

/*
Formaton vlerat e fushave sipas formatit perkates.
*/
function vendosKonfigFormatNumriR() {
    var grida = $('#rowed6');
    var idTe = grida.jqGrid('getDataIDs');
    for (j = 0; j < idTe.length; j++) {
        var idRreshti = idTe[j];
        grida.formatoQelize('txtSasia', idRreshti);
        grida.formatoQelize('txtScrap', idRreshti);
        grida.formatoQelize('txtKostoR', idRreshti);
        grida.formatoQelize('txtKostoTotaleR', idRreshti);
        grida.formatoQelize('txtSasiaAktualeR', idRreshti);
        grida.formatoQelize('txtFiroPerqindje', idRreshti);
    }
}

function myElemDetajimi(value, options) {
    var grida = $("#rowed6");
    var idRow = grida.getLastSel2();
    var vleraLlojit = grida.getTekstQelize('txtLloji', idRow);
    disabled = arrayReadOnlyKolonaSubGrides[19] == 'True' || vleraLlojit == "Burim";
    return myJQGrid.myElemKodi(value, options, disabled, idRow, arrayIdKolonaSubGrides[19], ButtonClickDetajimet, keyPressDet, changeFunca2, undefined, lostFocusKoloneFunditR);
}

function myElemDetajimi2(value, options) {

    var grida = $("#rowed6");
    var idRow = grida.getLastSel2();
    var vleraLlojit = grida.getTekstQelize('txtLloji', idRow);
    disabled = arrayReadOnlyKolonaSubGrides[20] == 'True' || vleraLlojit == "Burim";
    return myJQGrid.myElemKodi(value, options, disabled, idRow, arrayIdKolonaSubGrides[20], ButtonClickDetajimet2, keyPressDet2, changeFunca3, undefined, lostFocusKoloneFunditR);
}

function ButtonClickDetajimet() {
    var tabIndex = PageControl.GetActiveTabIndex();
    var grida = ktheGrideSipasTabit(tabIndex);
    ButtonClickDetajim(grida, 1);
}

function ButtonClickDetajimet2() {
    var tabIndex = PageControl.GetActiveTabIndex();
    var grida = ktheGrideSipasTabit(tabIndex);
    ButtonClickDetajim(grida, 2);
}


/*
Function: ButtonClickDetajimet
 
Hap lupen e detajimeve te artikullit.
*/

function ButtonClickDetajim(grida, lloji) {
    var idRresht = grida.getLastSel2();
    var vleraLlojit = grida.selector == "#rowed5" ? "Artikull" : grida.getTekstQelize('txtLloji', idRresht);
    identifikuesPerPopupDetajime = 'Ekzekutim';
    var magazine = grida.getTekstQelize(grida.selector == "#rowed5" ? 'txtIdMag' : 'txtIdMagR', idRresht);
    //marr vleren e IdKonfigurim te lupes se detajimeve
    var hfDetajim = document.getElementById("hfGridaDetajimi");
    var vjennga = (PageControl.GetActiveTabIndex() == 1 ? "EkzekutimProdhimi" : "");
    if (vleraLlojit == "Artikull") {
        var vleraKodit = grida.getTekstQelize(grida.selector == "#rowed5" ? 'txtIdArtikulli' : 'txtIdArtikulliR', idRresht);
        var queryString = 'idArtikulli=' + vleraKodit + '&mag=' + magazine + '&vjenNga=' + vjennga + '&lloji=' + lloji + '&idKonfigAmbjente=' + hfDetajim.value + '&detajimi1=' + grida.getTekstQelize('txtDetajimi', idRresht) + '&dateDok=' + dteDtDok.GetText();
        popupUniversal.SetHeaderText(hfState.Get("msgZgjidhDetajimArtikulli"));
        popupUniversal.SetContentUrl('LupaDetajimArtikulliRegjistrim.aspx?' + queryString);
        popupUniversal.SetSize(600, 600);
        popupUniversal.Show();
    }
}
function keyPressDet(event) {
    keyPressDetajimi(event, 1);
}

function keyPressDet2(event) {
    keyPressDetajimi(event, 2);
}

function keyPressDetajimi(event, lloji) {
    var tabIndex = PageControl.GetActiveTabIndex();
    var grida = ktheGrideSipasTabit(tabIndex);
    var idRow = grida.getLastSel2();
    try {
        var vlera = event.target.value;
        if (vlera == "" && lloji == 1)
            $('#' + grida.selector == "#rowed5" ? arrayIdKolonaGrides[17] : arrayIdKolonaGrides[19] + idRow).autocomplete("close");
        else if (vlera == "" && lloji == 2)
            $('#' + grida.selector == "#rowed5" ? arrayIdKolonaGrides[18] : arrayIdKolonaGrides[20] + idRow).autocomplete("close");
        else
            callWebserviceDetajimi(grida, vlera, lloji);
    }
    catch (e) { }
}
function callWebserviceDetajimi(grida, vlera, lloji) {
    var idRow = grida.getLastSel2();
    var kodArtikulli = grida.getTekstQelize(grida.selector == "#rowed5" ? 'txtKodiArtikull' : 'txtKodiArtikullR', idRow);
    var idPerdoruesi = hfState.Get('idPerdoruesi');
    var idNdermarrje = hfState.Get('idNdermarrje');
    if (kodArtikulli == "")
        return;

    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheListeDetajimeshArtikulliNew"),
        data: JSON.stringify({
            infixText: vlera, art: kodArtikulli, lloji: lloji, idNdermarrje: idNdermarrje, idPerdoruesi: idPerdoruesi,
            merrPerberesit: false, date: new Date(), sipasGjendjes: false, detajimi1: '', mag: ''
        })
    }).done(function (result) {
        result.lloji == 1 ? SucceededCallbackDetajimiAutoComplete(result) : SucceededCallbackDetajimiAutoComplete2(result);
    });
}

function SucceededCallbackDetajimiAutoComplete(result) {
    var tabIndex = PageControl.GetActiveTabIndex();
    var grida = ktheGrideSipasTabit(tabIndex);
    var idRow = grida.getLastSel2();
    myJQGrid.SucceededCallbackKodiDetajimi(result.autocomplete, '#txtDetajimi' + idRow, result.kategoriDet, result[2]);
}

function SucceededCallbackDetajimiAutoComplete2(result) {
    var tabIndex = PageControl.GetActiveTabIndex();
    var grida = ktheGrideSipasTabit(tabIndex);
    var idRow = grida.getLastSel2();
    myJQGrid.SucceededCallbackKodiDetajimi(result.autocomplete, '#txtDetajimi2t' + idRow, result.kategoriDet, result[2]);
}
function JoClick(s, e) {
    var grida = $('#rowed5');
    switch (identifikuesPyetje) {
        case 'detajimi1':
        case 'detajimi1Lidhje':
            grida.SetTekst('txtDetajimi', pageState.identifikuesRreshti, '');
            break;
        case 'detajimi2':
        case 'detajimi2Lidhje':
            grida.SetTekst('txtDetajimi2t', pageState.identifikuesRreshti, '');
            break;
        default:
            alert('Pyetje e panjohur');
            break;
    }
}

var queryString;
function PoClick(s, e) {
    var grida = $('#rowed5');
    switch (identifikuesPyetje) {
        case 'detajimi1':
            mesazhList.SetSelectedIndex(-1);
            btnPo.SetVisible(false);
            btnJo.SetVisible(false);
            hlClose.SetVisible(false);
            queryString = 'vjenNga=Shto_Ekzekutim&kodArtikulli=' + grida.getTekstQelize("txtKodiArtikull", pageState.identifikuesRreshti) + '&lloji=1' +'&veprimi=' + pageState.memoryArt.Get(grida.getTekstQelize('txtIdArtikulli', pageState.identifikuesRreshti)).IdKategoriDetajimi + '&kodDet=' + grida.getTekstQelize('txtDetajimi', pageState.identifikuesRreshti);
            myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgShtoDetajim"), 'LupaDetajimShpejte.aspx?' + queryString, 900, 600);
            break;
        case 'detajimi2':
            mesazhList.SetSelectedIndex(-1);
            btnPo.SetVisible(false);
            btnJo.SetVisible(false);
            hlClose.SetVisible(false);
            queryString = 'vjenNga=Shto_Ekzekutim&kodArtikulli=' + grida.getTekstQelize("txtKodiArtikull", pageState.identifikuesRreshti) + '&lloji=2' +'&veprimi=' + pageState.memoryArt.Get(grida.getTekstQelize('txtIdArtikulli', pageState.identifikuesRreshti)).IdKategoriDetajimi2 + '&kodDet=' + grida.getTekstQelize('txtDetajimi2t', pageState.identifikuesRreshti);
            myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgShtoDetajim"), 'LupaDetajimShpejte.aspx?' + queryString, 900, 600);
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
function changeFunca2(event, ui, emerKodi, index) {//po
    changeFuncDetajimi(index, 1);
}

function changeFunca3(event, ui, emerKodi, index) {//po
    changeFuncDetajimi(index, 2);
}

function changeFuncDetajimi(index, lloji) {
    var tabIndex = PageControl.GetActiveTabIndex();
    var grida = ktheGrideSipasTabit(tabIndex);
    var data = grida.getTeDhenaRreshti(index);
    var rreshti = (data.length > 0) ? data[0] : data;
    var detajimi = lloji == 1 ? rreshti.txtDetajimi : rreshti.txtDetajimi2t;
    if (detajimi != "" && rreshti.txtKodiArtikull != "") {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraDetajimMeKod"), data: JSON.stringify({ kodi: detajimi, index: index, lloji: lloji, kodartikulli: grida.getTekstQelize(grida.selector == "#rowed5" ? 'txtKodiArtikull' : 'txtKodiArtikullR', index), idkokamag: 0, idNdermarrje: hfState.Get('idNdermarrje'), idPerdorues: hfState.Get('idPerdoruesi'), magazine: grida.getTekstQelize(grida.selector == "#rowed5" ? 'txtIdMag' : 'txtIdMagR', index), date: dteDtDok.GetText(), kontrolloImeiFifo: false, listeImei: new Array(), promocione: false, DokumentTransferimiOwn: false, merrPerberesit: false })
        }).done(SucceededCallbackDetajim);
    }
    else {
        var art = pageState.memoryArt.Get(grida.getTekstQelize('txtIdArtikulli', index));
        if (!art)
            return;
        var idKategoriDetajimi = lloji == 1 ? art.IdKategoriDetajimi : art.IdKategoriDetajimi2;
        if (art && idKategoriDetajimi)
            vendosDetajim([index, null, lloji, idKategoriDetajimi, null]);
    }
}

function SucceededCallbackDetajim(result) {
    vendosDetajim(result);
}

function vendosDetajim(result) {
    var tabIndex = PageControl.GetActiveTabIndex();
    var grida = ktheGrideSipasTabit(tabIndex);
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
    var kaDetajimArtikulli = pageState.memoryArt.Get(grida.getTekstQelize(tabIndex == 0 ? 'txtIdArtikulli' : 'txtIdArtikulliR', idRreshti)).DetajimArtikulli;
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
                case 2: //serial
                    ShtoPyetjeCeljeDetajim(grida, detKontroll, idRreshti, kategoria, lloji);
                    return;
                case 4: //seri
                    if (grida.getTekstQelize(detKontroll, idRreshti) != '')
                        CelDheLidhDetajimMeArtikull(grida.getTekstQelize('txtKodiArtikull', idRreshti), grida.getTekstQelize(tabIndex == 0 ? 'txtIdArtikulli' : 'txtIdArtikulliR', idRreshti), lloji, kategoria, 1, grida.getTekstQelize(detKontroll, idRreshti));
                    return;
                case 3: //date skadence
                    var dtSkadence = grida.getTekstQelize(detKontroll, idRreshti);
                    if (dtSkadence && dtSkadence != '') {
                        if ((Date.parseLocale(dtSkadence, "dd/MM/yyyy")) == null || dtSkadence.length != 10) {
                            myMesazh.ShtoMesazhGabimi(hfState.Get("msgKodiDateSkadenceDuhetFormat"));
                            if (idRreshti != idRow)
                                grida.setTekstQelize(detKontroll, idRreshti, '');
                            else {
                                grida.setTekstQelize(detKontroll, idRreshti, '');
                                $(det).data('Kodi', '');
                            }
                        }
                        else
                            CelDheLidhDetajimMeArtikull(grida.getTekstQelize('txtKodiArtikull', idRreshti), grida.getTekstQelize(tabIndex == 0 ? 'txtIdArtikulli' : 'txtIdArtikulliR', idRreshti), lloji, kategoria, 3, dtSkadence);
                    }
                    return;
                default://detajimi nuk ekziston por nuk mund te celet sepse ska kategori detajimi ne kartele artikulli
                    if (grida.getTekstQelize(detKontroll, idRreshti) != '') {
                        myMesazh.ShtoMesazhGabimi(hfState.Get("msgArtSkaKategoriPerDetajim"));
                        grida.setTekstQelize(detKontroll, idRreshti, '');
                        $(det).data('Kodi', '');
                    }
                    return;
            }
        }

    if (lloji == 1) {
        grida.setTekstQelize('txtDetajimi', idRreshti, detajim.KodDetajimArtikulli);
        if (idRreshti == idRow)
            $(det).data('Kodi', detajim.KodDetajimArtikulli);
        if (kategoria == 3 || kategoria == 4) {
            var art = pageState.memoryArt.Get(grida.getTekstQelize(tabIndex == 0 ? 'txtIdArtikulli' : 'txtIdArtikulliR', idRreshti));
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

function selectFunca2(event, ui, emerfushe, iddetajim, koddetajim) {
    selectFuncaDetajim(ui, emerfushe, iddetajim, koddetajim, 1);
}

function selectFunca3(event, ui, emerfushe, iddetajim, koddetajim) {
    selectFuncaDetajim(ui, emerfushe, iddetajim, koddetajim, 2);
}

function selectFuncaDetajim(ui, emerfushe, iddetajim, koddetajim, lloji) {
    var tabIndex = PageControl.GetActiveTabIndex();
    var grida = ktheGrideSipasTabit(tabIndex);
    var idRresht = grida.getLastSel2();
    var idPerdoruesi = hfState.Get('idPerdoruesi');
    var idNdermarrje = hfState.Get('idNdermarrje');
    var magazine = grida.getTekstQelize(grida.selector == "#rowed5" ? 'txtIdMag' : 'txtIdMagR', idRresht);
    var kodArtikull = grida.getTekstQelize(grida.selector == "#rowed5" ? 'txtKodiArtikull' : 'txtKodiArtikullR', idRresht);
    var kodiDetajim = iddetajim && koddetajim ? koddetajim : ui.item != null ? ui.item.value : '';
    var idJa = iddetajim && koddetajim ? iddetajim : ui.item != null ? ui.item.value : 0;
    if (kodiDetajim == '')
        return;
    $(emerfushe).val(kodiDetajim);
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraDetajimMeID"),
        data: JSON.stringify({
            idja: idJa, index: idRresht, lloji: lloji, kodartikulli: kodArtikull, idkokamagazina: 0, idNdermarrje: idNdermarrje, idPerdorues: idPerdoruesi, magazine: magazine,
            date: dteDtDok.GetText(), kontrolloImeiFifo: false, listeImei: new Array(), promocione: false, DokumentTransferimiOwn: false, merrPerberesit: false
        })
    }).done(SucceededCallbackDetajim);
    return false;
}

/*
Function: myElemNrRendor
 
Nderton nje textbox per te vendosur nr rendor te rreshtit.
*/
function myElemNrRendorR(value, options) {//po
    disabled = arrayReadOnlyKolonaGrides[0];
    var grida1 = $("#rowed6");
    var idRow = grida1.getLastSel2();
    return myJQGrid.myElemNrRendor(value, options, idRow, 'txtNrRendor', $("#rowed6"));
}

function myelemIdProdukti(value, options) {//po
    disabled = arrayReadOnlyKolonaSubGrides[1];
    var grida1 = $("#rowed6");
    var idRow = grida1.getLastSel2();
    return myJQGrid.myElemIdKodi(value, options, disabled, idRow, arrayIdKolonaSubGrides[1]);
}

function myelemIndexPrindi(value, options) {//po
    disabled = arrayReadOnlyKolonaSubGrides[17];
    var grida1 = $("#rowed6");
    var idRow = grida1.getLastSel2();
    return myJQGrid.myElemIdKodi(value, options, disabled, idRow, arrayIdKolonaSubGrides[17]);
}
function myelemSkedulim(value, options) {//po
    disabled = arrayReadOnlyKolonaSubGrides[18];
    var grida1 = $("#rowed6");
    var idRow = grida1.getLastSel2();
    return myJQGrid.myElemIdKodi(value, options, disabled, idRow, arrayIdKolonaSubGrides[18]);
}

var kaprodukt = false;
function myElemKodProdukti(value) {//po
    disabled = arrayReadOnlyKolonaSubGrides[2];
    var grida = $("#rowed5");
    var gridarec = $("#rowed6");
    var arrayOptions = new Array();
    var gridIds2 = grida.jqGrid('getDataIDs');
    var indexprind = 0;
    var idRow = gridarec.getLastSel2();
    indexprind = gridarec.getTekstQelize('txtIndexPrindi', idRow);
    arrayOptions = new Array();
    var select = 0;
    
    for (var i = 0; i < gridIds2.length; i++) {
        var objNjesi = new Object();
        if (grida.getTekstQelize('txtKodiArtikull', gridIds2[i]) != '') {
            objNjesi.value = grida.getTekstQelize('txtIdArtikulli', gridIds2[i]);
            objNjesi.text = grida.getTekstQelize('txtKodiArtikull', gridIds2[i]);
            objNjesi.norma = grida.getTekstQelize('txtPershkrimArtikull', gridIds2[i]);
            objNjesi.caktuar = gridIds2[i];
            arrayOptions.push(objNjesi);
            if (indexprind == gridIds2[i])
                select = i;
            kaprodukt = true;
        }
    }
    if (arrayOptions.length == 0) {
        objNjesi = new Object();
        objNjesi.value = 0;
        objNjesi.text = '';
        objNjesi.norma = '';
        objNjesi.caktuar = '';
        arrayOptions.push(objNjesi);
        kaprodukt = false;
    }
    if (!kaprodukt)
        disabled = true;
    var combo = myJQGrid.myElemCombo(value, arrayIdKolonaSubGrides[2], idRow, changeart, arrayOptions, disabled);
    combo.children(0).prop('selectedIndex', select);
    return combo;
}

function changeart() {
    var gridarec = $("#rowed6");
    var idRow = gridarec.getLastSel2();
    var indexprindivjeter = gridarec.getTekstQelize('txtIndexPrindi', idRow);
    gridarec.setTekstQelize('txtPershkrimProdukti', idRow, $("#" + 'txtKodProdukti' + idRow + " option:selected").data("norma"));
    gridarec.setTekstQelize('txtIdProdukti', idRow, $("#" + 'txtKodProdukti' + idRow).val());
    gridarec.setTekstQelize('txtIndexPrindi', idRow, $("#" + 'txtKodProdukti' + idRow + " option:selected").data("caktuar"));
    llogaritkostoprindi(indexprindivjeter);
    llogaritkostoprindi(gridarec.getTekstQelize('txtIndexPrindi', idRow));
    callWebServiceInfoRow();

}

function myElemEmerProdukti(value) {//po
    disabled = arrayReadOnlyKolonaSubGrides[3];
    if (!kaprodukt)
        disabled = 'True';
    var grida1 = $("#rowed6");
    var idRow = grida1.getLastSel2();
    return myJQGrid.myElemEmertimi(value, disabled, idRow, arrayIdKolonaSubGrides[3]);
}

function myelemCombo(value) {//po
    var disabled = false;
    var objTmp;
    var arrayOptions = new Array(2);
    objTmp = new Object();
    objTmp.value = "1";
    objTmp.text = "Artikull";
    arrayOptions[0] = objTmp;
    objTmp = new Object();
    objTmp.value = "2";
    objTmp.text = "Burim";
    arrayOptions[1] = objTmp;
    if (arrayReadOnlyKolonaSubGrides[4] == 'True') {
        disabled = true;
    }
    if (!kaprodukt)
        disabled = true;
    var grida1 = $("#rowed6");
    var idRow = grida1.getLastSel2();
    return myJQGrid.myElemCombo(value, arrayIdKolonaSubGrides[4], idRow, change, arrayOptions, disabled);
}

function myelemIdArtikulliR(value, options) {//po
    disabled = arrayReadOnlyKolonaSubGrides[5];
    var grida1 = $("#rowed6");
    var idRow = grida1.getLastSel2();
    return myJQGrid.myElemIdKodi(value, options, disabled, idRow, arrayIdKolonaSubGrides[5]);
}

function myelemKodiR(value, options) {//po
    disabled = arrayReadOnlyKolonaSubGrides[6];
    if (!kaprodukt)
        disabled = 'True';
    var grida1 = $("#rowed6");
    var idRow = grida1.getLastSel2();
    return myJQGrid.myElemKodi(value, options, disabled, idRow, arrayIdKolonaSubGrides[6], ButtonClickKodiR, keyPressKodiR, changeFunca, undefined, lostFocusKoloneFunditR);
}

function myElemPershkrimiR(value) {//po
    disabled = arrayReadOnlyKolonaSubGrides[7];
    if (!kaprodukt)
        disabled = 'True';
    var grida1 = $("#rowed6");
    var idRow = grida1.getLastSel2();
    return myJQGrid.myElemEmertimi(value, disabled, idRow, arrayIdKolonaSubGrides[7]);
}

function myelemComboNjesiaR(value) {//po
    disabled = arrayReadOnlyKolonaSubGrides[8];
    if (!kaprodukt)
        disabled = 'True';
    var grida1 = $("#rowed6");
    var idRow = grida1.getLastSel2();
    return myJQGrid.myElemEmertimi(value, disabled, idRow, arrayIdKolonaSubGrides[8]);
}

function myelemIdBurimeR(value, options) {//po
    disabled = arrayReadOnlyKolonaSubGrides[9];
    var grida1 = $("#rowed6");
    var idRow = grida1.getLastSel2();
    return myJQGrid.myElemIdKodi(value, options, disabled, idRow, arrayIdKolonaSubGrides[9]);
}

function myelemSasiAktR(value, options) {
    var grida1 = $("#rowed6");
    var idRow = grida1.getLastSel2();
    return $("#rowed6").myElemTextBoxFormatNumri({ value: value, options: options, disabled: !kaprodukt ? "True" : arrayReadOnlyKolonaSubGrides[10] == 'True' ? "True" : "False", indexRow: idRow, id: "txtSasiaAktualeR", onKeyDown: keyup, onFocusout: changedSasiAktR });
}

function myelemSasiPorR(value, options) {
    var grida1 = $("#rowed6");
    var idRow = grida1.getLastSel2();
    return $("#rowed6").myElemTextBoxFormatNumri({ value: value, options: options, disabled: !kaprodukt ? "True" : arrayReadOnlyKolonaSubGrides[11] == 'True' ? "True" : "False", indexRow: idRow, id: "txtSasia", onKeyDown: keyup, onFocusout: keyup });
}

function myelemFiro(value, options) {
    var grida1 = $("#rowed6");
    var idRow = grida1.getLastSel2();
    return $("#rowed6").myElemTextBoxFormatNumri({ value: value, options: options, disabled: !kaprodukt ? "True" : arrayReadOnlyKolonaSubGrides[12] == 'True' ? "True" : "False", indexRow: idRow, id: "txtFiroPerqindje", onKeyDown: keyup, onFocusout: keyup });
}

function myelemScrap(value, options) {
    var grida1 = $("#rowed6");
    var idRow = grida1.getLastSel2();
    return $("#rowed6").myElemTextBoxFormatNumri({ value: value, options: options, disabled: !kaprodukt ? "True" : arrayReadOnlyKolonaSubGrides[13] == 'True' ? "True" : "False", indexRow: idRow, id: "txtScrap", onKeyDown: keyup, onFocusout: changeScrapRec });
}

function myelemKostoR(value, options) {
    var disabled = arrayReadOnlyKolonaSubGrides[14];
    var grida1 = $("#rowed6");
    var idRow = grida1.getLastSel2();
    var grida = $('#rowed6');
    var vleraLlojit = grida.getTekstQelize('txtLloji', idRow);
    var sasia = grida.getTekstQelize('txtSasiaAktualeR', idRow);
    if ((vleraLlojit == "Artikull" && parseFloat(sasia) < 0))
        disabled = 'False';
    else if (arrayReadOnlyKolonaGrides[8] == 'True' || (vleraLlojit == "Artikull" && parseFloat(sasia) > 0))
        disabled = 'True';
    if (!kaprodukt)
        disabled = 'True';
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: disabled, indexRow: idRow, id: arrayIdKolonaSubGrides[14], onKeyDown: keyup, onFocusout: changedkosto });
}

function myelemKostoTotaleR(value, opsions) {
    var grida1 = $("#rowed6");
    var idRow = grida1.getLastSel2();
    return $('#rowed6').myElemTextBoxFormatNumri({ value: value, options: opsions, disabled: !kaprodukt ? "True" : arrayReadOnlyKolonaSubGrides[15], indexRow: idRow, id: arrayIdKolonaSubGrides[15], onKeyDown: keyup, onFocusout: keyup });
}

function myelemComboMagazinaR(value) {//po
    magazina1 = btneMagazin2a.GetText();
    if (value == "" && magazina1 != "" && magazina1 != undefined)
        value = magazina1;
    var arrayOptions = new Array(0);
    if (colMagazina !== "") {
        arrayOptions = new Array(colMagazina.length);
        for (var i = 0; i < colMagazina.length; i++) {
            var objNjesi = new Object();
            objNjesi.value = colMagazina[i].IdNjesiAdministrative;
            objNjesi.text = colMagazina[i].Kodi;
            arrayOptions[i] = objNjesi;
        }
    }
    var disabled;
    if (arrayReadOnlyKolonaSubGrides[16] == 'True')
        disabled = true;
    else
        disabled = false;
    if (!kaprodukt)
        disabled = true;
    var grida1 = $("#rowed6");
    var idRow = grida1.getLastSel2();
    return myJQGrid.myElemCombo(value, 'txtIdMagR', idRow, changemagRec, arrayOptions, disabled);
}

function changemagProd() {
    changemag($("#rowed5"));
}
function changemagRec() {
    changemag($("#rowed6"));
}
function changemag(grida) {
    var idRow = grida.getLastSel2();
    GjendjaMinMaxArtikull();
    merrkostomag([idRow]);
}

function merrkostomag(idTe) {
    var recepturat = new Array();
    var grida = $('#rowed6');
    var idDokRegj = Utils.IsUndefined(Utils.getUrlVar('id')) ? 0 : Utils.getUrlVar('id');
    for (var i = 0; i < idTe.length; i++) {
        var index = idTe[i];
        if (grida.getTekstQelize('txtLloji', index) == 'Artikull') {
            var idart = grida.getTekstQelize('txtIdArtikulliR', index);
            if (!(typeof (idart) == "undefined" || idart == undefined || idart == "" || idart == "0"))
                recepturat.push({ idRreshti: index, mag: grida.getTekstQelize('txtIdMagR', index), idart: idart, sasia: grida.getTekstQelize('txtSasiaAktualeR', index), idDok: idDokRegj });
        }
    }
    if (recepturat.length == 0) return;
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "merrKostoMagazineKod"),
        data: JSON.stringify({ recepturat: recepturat, data: dteDtDok.GetDate() })
    }).done(SucceededCallbackMagazine);
}

function SucceededCallbackMagazine(result) {
    var grida = $('#rowed6');
    for (var i = 0; i < result.length; i++) {
        grida.setTekstQelize('txtKostoR', result[i].index, result[i].kosto);
        grida.setTekstQelize('txtKostoTotaleR', result[i].index, (grida.getTekstQelize('txtKostoR', result[i].index) * grida.getTekstQelize('txtSasiaAktualeR', result[i].index)));
        llogaritkostoprindi(grida.getTekstQelize('txtIndexPrindi', result[i].index));
    }

}


function myValueButtonFshiR(elem, operation, value) {//po
    var grida1 = $("#rowed6");
    var idRow = grida1.getLastSel2();
    if (arrayReadOnlyKolonaSubGrides[arrayReadOnlyKolonaSubGrides.length - 1] == 'True')
        return myJQGrid.myValueButtonFshi(true, idRow, "#rowed6");
    else
        return myJQGrid.myValueButtonFshi(false, idRow, "#rowed6");

}

function myElemButtonFshiR() {//po
    changeart();
    var grida1 = $("#rowed6");
    var idRow = grida1.getLastSel2();
    if (arrayReadOnlyKolonaSubGrides[arrayReadOnlyKolonaSubGrides.length - 1] == 'True')
        return myJQGrid.myElemButtonFshi(true, idRow, "#rowed6", lostFocusKoloneFunditR);
    else
        return myJQGrid.myElemButtonFshi(false, idRow, "#rowed6", lostFocusKoloneFunditR);
}

function changedSasiAktR() {
    var grida = $('#rowed6');
    var idRow = grida.getLastSel2();
    editorSasia = grida.getTekstQelize('txtSasiaAktualeR', idRow);

    var sasia;
    if (editorSasia == "" || isNaN(editorSasia)) {

        myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiaAktualeDuhetTeJeteNr"));
        sasia = grida.getVlereDefault('txtSasiaAktualeR');
        grida.setTekstQelize('txtSasiaAktualeR', idRow, sasia);
        $('#txtSasiaAktualeR' + idRow).focus();
    }
    else
        if (editorSasia == "0.00") {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiaAktualeNukMundTeJeteZero"));
            sasia = grida.getVlereDefault('txtSasiaAktualeR');
            grida.setTekstQelize('txtSasiaAktualeR', idRow, sasia);
            $('#txtSasiaAktualeR' + idRow).focus();
        }
    if (parseFloat(editorSasia) < parseFloat(grida.getTekstQelize('txtScrap', idRow)) && parseFloat(grida.getTekstQelize('txtScrap', idRow)) != 0) {
        grida.setTekstQelize('txtSasiaAktualeR', idRow, grida.getTekstQelize('txtScrap', idRow));
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgFiroLigjoreDuhetTeJeteMeEVogelSeSasiaAktuale"));
    }
    if ($('#txtLloji' + idRow).val() == 1 && editorSasia >= 0)
        $('#txtKostoR' + idRow).prop('disabled', true);
    else $('#txtKostoR' + idRow).prop('disabled', false);
    grida.setTekstQelize('txtKostoTotaleR', idRow, (editorSasia * grida.getTekstQelize('txtKostoR', idRow)));
    GjendjaMinMaxArtikull();
    llogaritkostoprindi(grida.getTekstQelize('txtIndexPrindi', idRow));
    merrkostomag([idRow]);
}

function ButtonClickKodiR() {//po
    var grida = $("#rowed6");
    var idRow = grida.getLastSel2();
    var vleraLlojit = grida.getTekstQelize('txtLloji', idRow);

    switch (vleraLlojit) {
        case "":
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniLlojinEVeprimit"));
            break;
        case "Artikull":
            identikuesPerPopupArtikulli = 'Receptura';
            myButtonClickLupa.LupaUniversal_Click(hfState.Get("roundPanelZgjidhArtikullin"), 'LupaArtikull.aspx?klasa=5&idKonfigAmbjente=' + $("#hfGridaKodi")[0].value, 600, 500);

            break;
        case "Burim":
            identikuesPerPopupLlogari = "Receptura";
            myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhBurimin"), 'LupaBurime.aspx?vjenNga=Shto_Ekzekutim', 600, 560);
            break;

        default: myMesazh.ShtoMesazhGabimi(hfState.Get("msgLlojiVeprimitIPanjohur"));
            break;
    }



}
function keyPressKodiR(event) {     //po           
    var grida1 = $("#rowed6");
    var idRow = grida1.getLastSel2();
    try {
        var vlera = event.target.value;
        if (vlera == "")
            $('#' + arrayIdKolonaSubGrides[6] + idRow).autocomplete("close");
        else
            callWebserviceKodiR(vlera);
    } catch (e) { }
}

function callWebserviceKodiR(vlera) {//po
    var grida = $('#rowed6');
    var idRow = grida.getLastSel2();
    var vleraLlojit = grida.getTekstQelize('txtLloji', idRow);
    if (vleraLlojit == "Artikull") { //Artikull 
        //var idPerdoruesi = hfState.Get('idPerdoruesi');
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheArtikullPerProdhim"),
            data: JSON.stringify({ prefixText: vlera, klasa: 4 })
        }).done(SucceededCallbackKodiR);
        return;
    }
    if (vleraLlojit == "Burim") {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheBurime"),
            data: JSON.stringify({ prefixText: vlera })
        }).done(SucceededCallbackKodiR);
        return;
    }
}

function SucceededCallbackKodiR(result) {//po
    var grida1 = $("#rowed6");
    var idRow = grida1.getLastSel2();
    myJQGrid.SucceededCallbackKodi(result, '#txtKodiArtikullR' + idRow);
}

function selectFunca(event, ui, emerfushe, idArt, kodArt) { //po

    var grida = $("#rowed6");
    var idRow = grida.getLastSel2();
    vleraLlojit = grida.getTekstQelize('txtLloji', idRow);
    if (idArt != undefined && kodArt != undefined && idArt != "" && kodArt != "" && $(emerfushe).val() !== undefined) {
        $(emerfushe).val(kodArt);
        if (vleraLlojit == "Artikull") {

            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraArtMeIDPerRec"),
                data: JSON.stringify({ idja: idArt, rreshti: idRow, data: dteDtDok.GetDate(), idmag: $('#txtIdMagR' + idRow).val() })
            }).done(SucceededCallbackArtR);
        }
        else
            if (vleraLlojit == "Burim") { //Llogari

                $.ajax({
                    pritPergjigje: true,
                    url: Utils.getServerApiUrl("Rregjistrime", "KtheVleraBurimeMeIDRow"),
                    data: JSON.stringify({ idja: idArt, rreshti: idRow })
                }).done(SucceededCallbackBurim);
            }



        return false;
    }

    var vleraLlojit = grida.getTekstQelize('txtLloji', idRow);
    if (ui !== null && ui.item != null) {
        $(emerfushe).val(ui.item.label);
        if (vleraLlojit == "Artikull") {
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraArtMeIDPerRec"),
                data: JSON.stringify({ idja: ui.item.value, rreshti: idRow, data: dteDtDok.GetDate(), idmag: $('#txtIdMagR' + idRow).val() })
            }).done(SucceededCallbackArtR);


        }
        else
            if (vleraLlojit == "Burim") { //Llogari
                $.ajax({
                    pritPergjigje: true,
                    url: Utils.getServerApiUrl("Rregjistrime", "KtheVleraBurimeMeIDRow"),
                    data: JSON.stringify({ idja: ui.item.value, rreshti: idRow })
                }).done(SucceededCallbackBurim);
            }
        return false;
    }

}

function changeFunca(event, ui, emerKodi, index) {//po
    var grida1 = $("#rowed6");
    var idRow = grida1.getLastSel2();
    var vleraKodit = grida1.getTekstQelize(emerKodi, index);
    var vleraLlojit = $("#rowed6").getTekstQelize('txtLloji', index);
    if (ui == null || ui.item == null) {
        if (vleraKodit != "") {
            switch (vleraLlojit) {
                case "Artikull":
                    $.ajax({
                        pritPergjigje: true,
                        url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraArtMeKodRec"),
                        data: JSON.stringify({ kodi: vleraKodit, rreshti: index, data: dteDtDok.GetDate(), kodmag: $("#rowed6").getTekstQelize('txtIdMagR', index) })
                    }).done(SucceededCallbackArtR);
                    break;
                case "Burim":
                    $.ajax({
                        pritPergjigje: true,
                        url: Utils.getServerApiUrl("Rregjistrime", "ktheVleraBurimeMeKodRow"),
                        data: JSON.stringify({ kodi: vleraKodit, rreshti: index })
                    }).done(SucceededCallbackBurim);
                    break;
            }
            return;
        }
        switch (vleraLlojit) {
            case "Artikull":
                vendosArtR([index, null]);
                break;
            case "Burim":
                vendosBurim([index, null]);
                break;
        }
        return;
    }
    if (index == idRow) {
        switch (vleraLlojit) {
            case "Artikull":
                $.ajax({
                    pritPergjigje: true,
                    url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraArtMeKodRec"),
                    data: JSON.stringify({ kodi: ui.item.label, rreshti: index, data: dteDtDok.GetDate(), kodmag: $("#rowed6").getTekstQelize('txtIdMagR', index) })
                }).done(SucceededCallbackArtR);
                break;
            case "Burim":
                $.ajax({
                    pritPergjigje: true,
                    url: Utils.getServerApiUrl("Rregjistrime", "ktheVleraBurimeMeKodRow"),
                    data: JSON.stringify({ kodi: ui.item.label, rreshti: idRow })
                }).done(SucceededCallbackBurim);
                break;
        }
    }
}

function resetRreshtKorentR(idRreshti) {
    var grida = $('#rowed6');
    var idRow = grida.getLastSel2();
    if (idRreshti == idRow && grida.getTekstQelize('txtKodiArtikullR', idRow) != undefined) {
        grida.setTekstQelize('txtKodiArtikullR', idRow, '');
        grida.setTekstQelize('txtIdArtikulliR', idRow, '0');
        grida.setTekstQelize('txtIdBurimi', idRow, '0'); grida.setTekstQelize('txtSkedulim', idRow, '');
        grida.setTekstQelize('txtPershkrimArtikullR', idRow, '');
        grida.setTekstQelize('txtNjesiArtikull', idRow, '');
        grida.setTekstQelize('txtKostoR', idRow);
        grida.setTekstQelize('txtSasiaAktualeR', idRow);
        grida.setTekstQelize('txtSasia', idRow);
        grida.setTekstQelize('txtFiroPerqindje', idRow);
        grida.setTekstQelize('txtScrap', idRow);
        grida.setTekstQelize('txtKostoTotaleR', idRow);
        if ($('#txtLloji' + idRow).val() == 1)
            $('#txtKostoR' + idRow).prop('disabled', true);
        else $('#txtKostoR' + idRow).prop('disabled', false);
        return;
    }
    grida.jqGrid('delRowData', idRreshti);
    return;
}

function vendosArtR(result) {
    var grida = $("#rowed6");
    var idRow = grida.getLastSel2();
    if (result != null) {
        var idRreshti = result[0];
        var artikulli = result[1];
        // var colmagrec = result[4];
        var detajimi = result[3];
        var detajimi2 = result[4];
        var kodi = "#txtKodiArtikullR" + idRreshti;
        var idKodi = "#txtIdArtikulliR" + idRreshti;
        var emerArt = '#' + 'txtPershkrimArtikullR' + idRreshti;
        if (artikulli == null || artikulli == undefined || artikulli.IdArtikulli == -1) {

            resetRreshtKorentR(idRreshti);
            return;
        }

        if (artikulli.Aktiv == false) {
            myMesazh.ShtoMesazhGabimi("Ky artikull eshte inaktiv!");
            resetRreshtKorent(idRreshti);
            return;
        }
        if (!prodsirecepture && grida.getTekstQelize('txtKodProdukti', idRreshti) == artikulli.KodArtikulli) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgVendosetVetArtikulli"));
            resetRreshtKorentR(idRreshti);
            return;
        }
        grida.setTekstQelize('txtNjesiArtikull', idRreshti, artikulli.KodNjesia1);

        pageState.memoryArt.Set(artikulli);
        if (idRreshti == idRow && grida.getTekstQelize('txtKodiArtikullR', idRreshti) != undefined) {
            if (grida.getTekstQelize('txtIdArtikulliR', idRreshti) == artikulli.IdArtikulli && $("#rowed6").getTekstQelize('txtLloji', idRreshti) == "Artikull") {
                $("#rowed6").setTekstQelize('txtKodiArtikullR', idRreshti, artikulli.KodArtikulli);
                return; //eshte i njejti artikull 
            }

            grida.setTekstQelize('txtKodiArtikullR', idRreshti, artikulli.KodArtikulli);
            myJQGrid.closeAutocomplete('txtKodiArtikullR', idRreshti);
            grida.setTekstQelize('txtIdArtikulliR', idRreshti, artikulli.IdArtikulli);
            grida.setTekstQelize('txtPershkrimArtikullR', idRreshti, artikulli.PershkrimArtikulli);
            grida.setTekstQelize('txtDetajimi', idRreshti, detajimi == null ? "" : detajimi.KodDetajimArtikulli);
            grida.setTekstQelize('txtDetajimi2t', idRreshti, detajimi2 == null ? "" : detajimi2.KodDetajimArtikulli);
            grida.setTekstQelize('txtKostoR', idRreshti, result[2]);
            grida.setTekstQelize('txtKostoTotaleR', idRreshti, (result[2] * grida.getTekstQelize('txtSasiaAktualeR', idRreshti)));

            var mag = btneMagazin2a.GetText();
            if (((artikulli.Magazina != "") && (mag == '')))
                $('#txtIdMagR' + idRreshti).val(artikulli.IdMagazina);

            llogaritkostoprindi(grida.getTekstQelize('txtIndexPrindi', idRreshti));
            callWebServiceInfoRow();
            GjendjaMinMaxArtikull();
            return;
        }



        var rreshti = $('#rowed6').jqGrid('getRowData', idRreshti);
        if (grida.getTekstQelize('txtIdArtikulliR', idRreshti) == artikulli.IdArtikulli) {
            $("#rowed6").setTekstQelize('txtKodiArtikullR', idRreshti, artikulli.KodArtikulli);
            return; //eshte i njejti artikull 
        }
        grida.setTekstQelize('txtKodiArtikullR', idRreshti, artikulli.KodArtikulli);
        grida.setTekstQelize('txtIdArtikulliR', idRreshti, artikulli.IdArtikulli);
        grida.setTekstQelize('txtPershkrimArtikullR', idRreshti, artikulli.PershkrimArtikulli);
        grida.setTekstQelize('txtDetajimi', idRreshti, detajimi == null ? "" : detajimi.KodDetajimArtikulli);
        grida.setTekstQelize('txtDetajimi2t', idRreshti, detajimi2 == null ? "" : detajimi2.KodDetajimArtikulli);
        grida.setTekstQelize('txtKostoR', idRreshti, result[2]);
        grida.setTekstQelize('txtKostoTotaleR', idRreshti, (result[2] * grida.getTekstQelize('txtSasiaAktualeR', idRreshti)));
        if ((artikulli.Magazina != "") && (mag == '')) grida.setTekstQelize('txtIdMagR', idRreshti, artikulli.Magazina);

        llogaritkostoprindi(grida.getTekstQelize('txtIndexPrindi', idRreshti));
        callWebServiceInfoRow();
    }
}
function SucceededCallbackArtR(result) {//po
    vendosArtR(result);
}
function SucceededCallbackBurim(result) {//po
    vendosBurim(result);
}
function vendosBurim(result) {
    if (result != null) {
        var idRreshti = result[0];
        var llogaria = result[1];
        if (llogaria == null || llogaria == undefined || llogaria.NrLlogari == -1) {
            resetRreshtKorentR(idRreshti);
            return;
        }
        var grida = $("#rowed6");
        var idRow = grida.getLastSel2();
        if (idRreshti == idRow && $('#txtKodiArtikullR' + idRow).val() != undefined) {
            var kodi = "#txtKodiArtikullR" + idRow;
            var idKodi = "#txtIdBurimi" + idRow;
            var emerLlog = '#' + 'txtPershkrimArtikullR' + idRow;
            if (grida.getTekstQelize('txtIdBurimi', idRreshti) == llogaria.IdBurimi && $("#rowed6").getTekstQelize('txtLloji', idRreshti) == "Burim") {
                $("#rowed6").setTekstQelize('txtKodiArtikullR', idRreshti, llogaria.Kodi);
                return; //eshte i njejta llogari 
            }
            grida.setTekstQelize('txtKodiArtikullR', idRreshti, llogaria.Kodi);
            grida.setTekstQelize('txtIdBurimi', idRreshti, llogaria.IdBurimi);
            grida.setTekstQelize('txtPershkrimArtikullR', idRreshti, llogaria.Emertimi);
            grida.setTekstQelize('txtKostoR', idRreshti, result[2]);
            grida.setTekstQelize('txtKostoTotaleR', idRreshti, (result[2] * grida.getTekstQelize('txtSasiaAktualeR', idRreshti)));
            grida.setTekstQelize('txtNjesiArtikull', idRreshti, 'ore');
            grida.setTekstQelize('txtSkedulim', idRreshti, '');
            llogaritkostoprindi(grida.getTekstQelize('txtIndexPrindi', idRreshti));

            return;
        }

        if (grida.getTekstQelize('txtIdBurimi', idRreshti) == llogaria.IdBurimi && $("#rowed6").getTekstQelize('txtLloji', idRreshti) == "Burim") {
            $("#rowed6").setTekstQelize('txtKodiArtikullR', idRreshti, llogaria.Kodi);
            return; //eshte i njejta llogari 
        }
        grida.setTekstQelize('txtKodiArtikullR', idRreshti, llogaria.Kodi);
        grida.setTekstQelize('txtIdBurimi', idRreshti, llogaria.IdBurimi);
        grida.setTekstQelize('txtPershkrimArtikullR', idRreshti, llogaria.Emertimi);
        grida.setTekstQelize('txtKostoR', idRreshti, result[2]);
        grida.setTekstQelize('txtSkedulim', idRreshti, '');
        grida.setTekstQelize('txtKostoTotaleR', idRreshti, (result[2] * grida.getTekstQelize('txtSasiaAktualeR', idRreshti)));
        grida.setTekstQelize('txtNjesiArtikull', idRreshti, 'ore');
        llogaritkostoprindi(grida.getTekstQelize('txtIndexPrindi', idRreshti));
    }
}
function lostFocusKoloneFunditR() {     //po 
    var grida1 = $("#rowed6");
    var idRow = grida1.getLastSel2();
    idRow = grida1.lostFocusKoloneFundit();

    //myJQGrid.lostFocusKoloneFunditR("#rowed6", idRow);
    if (($('#' + arrayIdKolonaSubGrides[1] + idRow).attr("disabled") == 'disabled')) {
        $('#txtKodProdukti' + idRow).focus();
        $('#txtKodProdukti' + idRow).blur();
        $('#txtKodProdukti' + idRow).focus();
    }
}

function change() {//po
    var grida1 = $("#rowed6");
    var idRow = grida1.getLastSel2();
    resetRreshtKorentR(idRow);
}

function fshiClicked2(indexi) {//po
    indexprindi = $('#rowed6').getTekstQelize('txtIndexPrindi', indexi);
    myJQGrid.fshiClicked(indexi, '#rowed6', inicializoGrideR);
    llogaritkostoprindi(indexprindi);
    $('#rowed6').rregulloNrRendorMeTeMadh(indexi);
}



function changedkosto() {
    var grida = $('#rowed6');
    var idRow = grida.getLastSel2();
    editorSasia = grida.getTekstQelize('txtKostoR', idRow);

    var sasia;
    if (editorSasia == "" || isNaN(editorSasia)) {

        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKostojaDuhetTeJeteNumer"));
        sasia = grida.getVlereDefault('txtKostoR');
        grida.setTekstQelize('txtKostoR', idRow, sasia);
        $('#txtKostoR' + idRow).focus();
    }
    else
        if (parseFloat(editorSasia) < 0) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgKostojaNukMundTeJeteNegative"));
            sasia = grida.getVlereDefault('txtKostoR');
            grida.setTekstQelize('txtKostoR', idRow, sasia);
            $('#txtKostoR' + idRow).focus();
        }
    grida.setTekstQelize('txtKostoTotaleR', idRow, (editorSasia * grida.getTekstQelize('txtSasiaAktualeR', idRow)));
    llogaritkostoprindi(grida.getTekstQelize('txtIndexPrindi', idRow));
}
function llogaritkostoprindi(indexprindi) {

    var grida = $("#rowed6");
    var idTe = grida.jqGrid('getDataIDs');
    var kosto = 0;
    for (i = 0; i < idTe.length; i++) {
        if (grida.getTekstQelize('txtIndexPrindi', idTe[i]) == indexprindi)
            kosto += parseFloat(grida.getTekstQelize('txtKostoTotaleR', idTe[i]));
    }
    var grida2 = $("#rowed5");
    editorSasia = grida2.getTekstQelize('txtSasiaAktuale', indexprindi);
    grida2.setTekstQelize('txtKostoTotale', indexprindi, kosto);
    if (isNaN(editorSasia)) {
        grida2.setTekstQelize('txtKosto', indexprindi, kosto / 1);
    }
    else
        grida2.setTekstQelize('txtKosto', indexprindi, kosto / grida2.getTekstQelize('txtSasiaAktuale', indexprindi));
    totali();
}



function changeScrapRec() {
    var grida = $('#rowed6');
    var idRow = grida.getLastSel2();
    editor = grida.getTekstQelize('txtScrap', idRow);
    if (isNaN(parseFloat(editor))) {
        sasia = grida.getVlereDefault('txtScrap');
        grida.setTekstQelize('txtScrap', idRow, sasia);
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgFiroNumer"));
    }
    if (parseFloat(editor) < 0) {
        sasia = grida.getVlereDefault('txtScrap');
        grida.setTekstQelize('txtScrap', idRow, sasia);
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgFiroLigjoreDuhetTeJeteNumerNegativ"));
    }
    if (parseFloat(editor) != 0 && parseFloat(editor) > parseFloat(grida.getTekstQelize('txtSasiaAktualeR', idRow))) {
        sasia = grida.getVlereDefault('txtScrap');
        grida.setTekstQelize('txtScrap', idRow, sasia);
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgFiroLigjoreDuhetTeJeteMeEVogelSeSasiaAktuale"));
    }

}

var identifikuesPerPopupDokumentat;
var identikuesPerPopupArtikulli;
var identifikuesPerPopupMagazina;
var identifikuesPerPopupDetajime;


function changeName() {//po
    myFaqeCelje.shtoHandlerSession();
    try {
        if (Utils.getUrlVar('id') == undefined)
            window.parent.callWebServiceKtheInfoLart('Shto_Ekzekutim.aspx', 0);
        else
            window.parent.callWebServiceKtheInfoLart('Shto_Ekzekutim.aspx', Utils.getUrlVar('id'));

    } catch (e) { }
    myCookies.createCookie('adresa', window.location.href, 1);
}
//per filtrat
function checkText(s, e) {
    myMenu.checkText(s, e);
} var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, grid_faturat, "", "");
}
function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}
function textChanged(s, e) {
    myMenu.textChanged(s, e);
}
function vendosNrAutomatik(colAtrTrupi, colKontrollet) {//po
    myNrAuto.vendosNrAutomatik(colAtrTrupi, colKontrollet, dteDtDok.GetDate());
}
function ruajKolonatEGrides(grida) {
    var idGride = colGrida[0].IdKoka;
    var idGjuha = hfState.Get('idGjuha');
    var idPerdoruesi = hfState.Get('idPerdoruesi');
    var idNdermarrje = hfState.Get('idNdermarrje');
    var idViti = hfState.Get('idViti');
    myJQGrid.ruajKolonatEGrides(grida, idGride, idGjuha, idNdermarrje, idViti, idPerdoruesi);
}
/*
Function: ButtonClickKerko
    
Hap lupen e dokumentave.
*/

function ButtonClickKerko(listUrl) {

    var queryString = {
        veprimi: 'EkzekutimProdhimi',
        listUrl: listUrl
    };
    popupUniversal.SetContentUrl('LupaDokumenta.aspx?' + Utils.KonvertoObjectQueryString(queryString));
    popupUniversal.SetHeaderText(hfState.Get("msgZgjidhDokumentin"));
    popupUniversal.SetSize(widthLupaKerko, heightLupaKerko);
    popupUniversal.Show();
}

/*
Function: callWebserviceKonfigurimi
    
Therret funksionin <ktheKonfigurim> per te vendosur konfigurimin per dokumentin.
Shiko funksionin <SucceededCallbackKonfigurimi>.
*/
function callWebserviceKonfigurimi(idKomp, kodKonf) {//po
    var idGjuha = hfState.Get('idGjuha');
    var idPerdoruesi = hfState.Get('idPerdoruesi');
    var idNdermarrje = hfState.Get('idNdermarrje');
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: idNdermarrje, idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);

    grid_faturat.PerformCallback(idKomp + ";" + kodKonf);

    if ($('#hfShtimModifikim').val() != 'modifikim') {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheGrupimDokumentashNderm"),
            data: JSON.stringify({ kodkonfig: kodKonf, idNdermarrje: idNdermarrje, idPerdoruesi: idPerdoruesi })
        }).done(Utils.SucceededCallbackGrupimDokumentash);
    }
}

function DateChanged(s, e) {//po
    var grida = $("#rowed5");
    var hfKontrollet = JSON.parse($('#hfKontrolletNrAutom').val());
    var atributet = JSON.parse($('#hfAtributeNrAutom').val());
    if ($('#hfShtimModifikim').val() != 'modifikim')
        vendosNrAutomatik(atributet, hfKontrollet);
    if (ndryshorecepturasipasdates) {
        if (grida.jqGrid('getGridParam', 'reccount') > 0)
            myMesazh.ShtoMesazhInformues(hfState.Get("msgKujdesRecepturatNdryshuanSipasDateSeZgjedhur"));
        var mag1 = 0;
        if (btneMagazina.GetText() != '')
            mag1 = btneMagazina.GetValue();
        var mag2 = 0;
        if (btneMagazin2a.GetText() != '')
            mag2 = btneMagazin2a.GetValue();
        var grida1 = $("#rowed6");
        grida1.setLastSel2(-1);

        jQuery("#rowed6").jqGrid('GridUnload', "rowed6");
        ruajFormatetNeGrideR(grida1, formatNumriZgjedhur);
        inicializoGrideR();
        var idRow = grida.getLastSel2();
        var idTe = grida.jqGrid('getDataIDs');
        for (var i = 0; i < idTe.length; i++) {
            if (grida.getTekstQelize('txtIdArtikulli', idTe[i]) != "") {
                var idPlanifikimi = grida.getTekstQelize('txtIdPlanifikim', idTe[i]);

                $.ajax({
                    pritPergjigje: true,
                    showLoading: true,
                    url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraArtMeIDMeRecRes"),
                    data: JSON.stringify({
                        idja: grida.getTekstQelize('txtIdArtikulli', idTe[i]), rreshti: idTe[i], data: dteDtDok.GetDate(), magazi: grida.getTekstQelize('txtIdMag', idTe[i]),
                        magazina2: mag2, sasia: grida.getTekstQelize('txtSasiaAktuale', idTe[i]), sasiplanrec: sasiplanrec, idtrupiplanifikimi: idPlanifikimi === "" ? 0 : idPlanifikimi, sasiburimi: sasiburimi
                    })
                }).done(SucceededCallbackArtRec);
            }
        }
        callWebServiceInfoRow();
    }
}

var sasiplanrec = 'sasi plan';
var colKushte;
var colAlterKusht;
var colGrida;
var colGrida1;
var formatNumriZgjedhur;
var sasiburimi = 'kartela';
var ndryshorecepturasipasdates;
var prodsirecepture;

function NdajLlojeGrida(coll, gridName) {
    return coll.filter(function (element) { return element.GridKokaEmri == gridName; });
}
function SucceededCallbackKonfig(result) {
    var hf = $("input[id$='hfShtimModifikim']");
    var hfLidhur = $("input[id$='hfLidhur']");
    var arrTabela = ['tblFillim', "tblFund"];
    var arrPrind = ["dvFillim", "dvFundi"];
    var hidField1 = $("#hfKontabilizimi");
    hidField1.val(0);
    var hidField2 = $("#hfFD");
    hidField2.val(0);
    var hidField3 = $("#hfFH");
    hidField3.val(0);
    var colKontrollet = result.colKontrollet;
    var colAtrTrupi = result.colAtrTrupi;
    formatNumriZgjedhur = result.formatNumriZgjedhur;
    var vlerat = myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, undefined, hf, '', arrTabela, undefined, undefined, hfLidhur, arrPrind);
    $("#divgride1").show();
    $("#divgride3").show();
    $("#divgride4").show();
    $("#dvFillim").show();
    $("#dvFundi").show();
    vendosDateDefault();
    PageControl.AdjustControl();
    $("#dvgvFaturat").show();
    var hfrivleresim = $('#hfKontrollRivleresim');
    colKushte = result.colKushte;
    colAlterKusht = result.colAlterKusht;
    colGrida = result.colGrida;
    var colFiltraGrida = result.colFiltraGrida;
    pageState.colGridaProdukti = NdajLlojeGrida(colGrida, "gvProdukti");
    pageState.colGridaReceptura = NdajLlojeGrida(colGrida, "gvReceptura");
    ndryshorecepturasipasdates = true;
    var hfMag = $("#hfLupaMagazina")[0];
    var hfLupaNjesiProdhimi = $("#hfLupaNjesiProdhimi")[0];
    $('#hfKontrolletNrAutom').val(JSON.stringify(colKontrollet));
    $('#hfAtributeNrAutom').val(JSON.stringify(colAtrTrupi));
    if ($("input[id$='hfShtimModifikim']").val() === "shtim") {
        hfNrAuto.Clear();
        hfNrAutoShitje.Clear();
        vendosNrAutomatik(colAtrTrupi, colKontrollet);
    }
    sasiplanrec = 'sasi plan';
    myMenu.UpdateFilter(cmbfiltra, colFiltraGrida, result.filtriDefault);

    for (var i = 0; i < colKontrollet.length - 1; i++) {

        if (colAtrTrupi[i].KodKontrolli === "btneKlientFurnitori")
            hfKl.value = colKontrollet[i].IdKonfigAmbjenteLupa.toString(); //merret id e konfigurimit te lupes per lupen e klient furnitorit ne forme
        else
            if (colAtrTrupi[i].KodKontrolli === "btneMagazina")
                hfMag.value = colKontrollet[i].IdKonfigAmbjenteLupa.toString(); //merret id e konfigurimit te lupes per lupen e magazines ne forme      
            else if (colAtrTrupi[i].KodKontrolli === "cmbNjesiProdhimi")
                hfLupaNjesiProdhimi.value = colKontrollet[i].IdKonfigAmbjenteLupa.toString();
    }
    for (j = 0; j < colKushte.length; j++) {
        var kusht = colKushte[j];
        var alternativa = colAlterKusht[j];
        switch (kusht.Kodi) {
            case "ZKDM":
                hidField3.val(kusht.Vlera);
                break;
            case "ZKDM2":
                hidField2.val(kusht.Vlera);
                break;
            case "KR":
                if (alternativa.Alternativa == 'Po')
                    hfrivleresim.val(true);
                else
                    hfrivleresim.val(false);
                break;
            case "GJK":
                if (alternativa.Alternativa == 'Jo')
                    hidField1.val(0);
                else
                    if (alternativa.Alternativa == "Direkt")
                        hidField1.val(1);
                    else
                        hidField1.val(2);
                break;
            case "SPR":
                if (alternativa.Alternativa == 'Sipas sasise plan produkti')
                    sasiplanrec = 'sasi plan';
                else sasiplanrec = 'sasi akt';
                break;
            case "PSB":
                if (alternativa.Alternativa == 'Kartela e artikullit')
                    sasiburimi = 'kartela';
                else sasiburimi = 'skedulimi';
                break;
            case "KGJMM":
                pageState.kushte[kusht.Kodi] = alternativa.Alternativa == "Po" ? true : false;
                break;
            case "NRSD":
                if (alternativa.Alternativa == 'Po')
                    ndryshorecepturasipasdates = true;
                else ndryshorecepturasipasdates = false;
                break;
            case "LPAPR":
                if (alternativa.Alternativa == 'Po')
                    prodsirecepture = true;
                else prodsirecepture = false;
                break;
            case "ZIA":
                if (kusht.Vlera != "0") {
                    if ($('#hfHapurMbyllur').val() == 'True')
                        splitter.GetPane(1).Expand();
                    else splitter.GetPane(1).CollapseBackward();
                    idInfoArt = kusht.Vlera;

                    $.ajax({
                        pritPergjigje: true,
                        url: Utils.getServerApiUrl("Rregjistrime", "ruajNeSessionTrupInfoArt"),
                        data: JSON.stringify({ idkoka: kusht.Vlera })
                    }).done(Succedcallback);
                    infoArt = true;
                }
                else {
                    splitter.GetPane(1).CollapseBackward();

                    $.ajax({
                        pritPergjigje: true,
                        url: Utils.getServerApiUrl("Rregjistrime", "ruajNeSessionTrupInfoArt"),
                        data: JSON.stringify({ idkoka: -1 })
                    }).done(Succedcallback);
                    infoArt = false;
                }
                break;
        }
    }
    var grida = $('#rowed5');
    var grida1 = $('#rowed6');
    grida.setLastSel2(-1);
    grida1.setLastSel2(-1);

    grida.jqGrid('GridUnload', "rowed5");
    grida1.jqGrid('GridUnload', "rowed6");
    formGridColsArray();

    grida = $('#rowed5');
    inicializoGride();
    ruajFormatetNeGride(grida, formatNumriZgjedhur);

    grida1 = $('#rowed6');
    inicializoGrideR();
    ruajFormatetNeGrideR(grida1, formatNumriZgjedhur);
    Utils.konfiguroAccorditionPasKonfigDokumenti(varKonfig.identifikuesPerLocalStorageKey);

    if (btneMagazina.GetText() != "")
        TextChangedMagazina('mag1');
    if (btneMagazin2a.GetText() != "")
        TextChangedMagazina('mag2');
    mbushGrideNgaHiddenFieldet();
}



/*
Function: ndryshoKonfigurimin
    
Therret funksionin <callWebserviceKonfigurimi> per te vendosur nje konfigurim te ri.
*/
function ndryshoKonfigurimin() {//po

    var pershkKonfigAmb = cmbKonfigurimi.GetSelectedItem().GetColumnText("PershkrimKonfigAmbjente");
    if (pershkKonfigAmb != undefined)
        $('#kokeKonfigurimi').text(hfState.Get("MenuKokeDokumenti") + ': ' + pershkKonfigAmb);
    callWebserviceKonfigurimi(806, cmbKonfigurimi.GetText());
}

var editorMag;
var identifikuesMagazina;

/*
Function: SucceededCallbackNiveli
    
Ndryshon vleren e zgjedhur te konfigurimit sipas nivelit te zgjedhur.
Therret funksionin <ndryshoKonfigurimin>.
*/
function ButtonClickMagazina(mag) {//po

    var hfKl = document.getElementById("hfLupaMagazina");
    var queryStr = hfKl.value;
    if (mag == 'mag1')
        editorMag = btneMagazina;
    else editorMag = btneMagazin2a;
    identifikuesMagazina = mag;
    popupUniversal.SetHeaderText(hfState.Get("msgZgjidhMagazinen"));
    popupUniversal.SetContentUrl('LupaMagazina.aspx?idKonfigAmbjente=' + queryStr);

    popupUniversal.SetSize(widthLupaMagazina, heightLupaMagazina);
    popupUniversal.Show();
}

/*
Function: TextChangedMagazina
    
Vendos ne gride magazinen qe zgjidhet te koka
*/
function TextChangedMagazina(mag) {//po
    //vendoset magazina e zgjedhur te koka ne rreshtin qe po editohet  
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var reshtiieditueshem = false;
    var rreshtaTeGrides, magazinagrides;
    if (mag == 'mag1') {
        magazina1 = btneMagazina.GetText();
        if (magazina1 != "") {
            magazinagrides = $("select[id$='txtIdMag" + idRresht + "']");
            if (magazinagrides.val() != undefined) {
                for (var i = 0; i < magazinagrides[0].length; i++) {
                    if (magazinagrides[0][i].text == magazina1)
                        magazinagrides[0].selectedIndex = i;
                }
                reshtiieditueshem = true;
                //callWebServiceInfoRow();
            }
            //vendoset magazina e zgjedhur te koka ne rreshtat e tjere te grides
            rreshtaTeGrides = grida.jqGrid('getDataIDs');
            for (i = 0; i < rreshtaTeGrides.length; i++) {
                if (!reshtiieditueshem)
                    grida.jqGrid('setCell', rreshtaTeGrides[i], 'txtIdMag', magazina1, 'clientArray', '');
                else if (rreshtaTeGrides[i] != idRresht) {
                    grida.jqGrid('setCell', rreshtaTeGrides[i], 'txtIdMag', magazina1, 'clientArray', '');
                }
            }
        }
    }
    else {
        var grida1 = $("#rowed6");
        var idRow = grida1.getLastSel2();
        magazina1 = btneMagazin2a.GetText();
        if (magazina1 != "") {
            magazinagrides = $("select[id$='txtIdMagR" + idRow + "']");
            if (magazinagrides.val() != undefined) {
                for (var i = 0; i < magazinagrides[0].length; i++) {
                    if (magazinagrides[0][i].text == magazina1)
                        magazinagrides[0].selectedIndex = i;
                }
                reshtiieditueshem = true;
                //callWebServiceInfoRow();
            }
            //vendoset magazina e zgjedhur te koka ne rreshtat e tjere te grides
            rreshtaTeGrides = $("#rowed6").jqGrid('getDataIDs');
            for (i = 0; i < rreshtaTeGrides.length; i++) {
                if (!reshtiieditueshem)
                    jQuery("#rowed6").jqGrid('setCell', rreshtaTeGrides[i], 'txtIdMagR', magazina1, 'clientArray', '');
                else if (rreshtaTeGrides[i] != idRow) {
                    jQuery("#rowed6").jqGrid('setCell', rreshtaTeGrides[i], 'txtIdMagR', magazina1, 'clientArray', '');
                }
            }
            merrkostomag(rreshtaTeGrides);
        }
    }
    callWebServiceInfoRow();
}

/*
Function: pastro
    
Pastrin array-t e perdorura dhe vendos counterat ne 0.
*/
function pastro() {
    InitArtPerb();
    grid_faturat.PerformCallback();
    arrPlanifikime = new Array();
}

/*
Function: pastroFushatKokes
    
Pastron fushat pasi eshte bere ruajtja apo modifikimi per te bere gati ambjentin per shtim.
*/
function pastroFushatKokes() {
    btneMagazin2a.SetValue(null); if (btneMagazin2a.GetItemCount() == 1)
        btneMagazin2a.SetSelectedIndex(0);
    btneMagazina.SetValue(null); if (btneMagazina.GetItemCount() == 1)
        btneMagazina.SetSelectedIndex(0);
    txtNrDok.SetText('');
    txtShenime.SetText('');
    cmbGrup1.SetValue(null);
    cmbGrup2.SetValue(null);
    cmbGrup3.SetValue(null);
    hfArkiva.Clear();
    $('#hfArkivaDokId').val("");
    cmbNjesiProdhimi.SetValue(null);
    var hf = document.getElementById("status1");
    hf.value = "false";
    vendosDateDefault();
}

function vendosDateDefault() {
    if ($("input[id$='hfShtimModifikim']").val() === "shtim") {
        try {
            var hfPeriudheObj = window.parent.lexoHfPeriudhe();
            dteDtDok.SetDate(Utils.ktheDateDefault(hfPeriudheObj));
            var dataSot = Utils.zeroOren(new Date());
            dteDtRegjistrimi.SetDate(dataSot);
        }
        catch (e) { }
    }
}

/*
Function: isValidKoka
    
Kontrollon nese jane plotesuar fushat e detyrueshme te kokes se dokumentit dhe validon daten.
*/
function isValidKoka() {
    if (!Utils.nrWsRrugesManager.kanePerfunduarWs()) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgPoTransferohetTeDhenatShtypniPerseriRuaj"));
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
    formatoFushaDevi();
    var hf = document.getElementById("status1");
    if (hf.value == "konvertuar") {
        popKonvertuar.Show();
        return;
    }
    if ($('#hfqkmesazhi').val() == 'shfaqmesazh') {
        //popMesazhQK.Show();
        myMesazh.ShtoMesazh({
            type: "confirm", layout: "center", modal: true, text: hfState.Get("msgDeshironiShperndarjeQendraKosto"),
            okClick: function () { Utils.hapPopUp(hfState.Get("msgShperndarjeNeQendratEKostos"), $('#hfUrl')); }, cancelClick: function () { Utils.JopopupClick($('#hfUrl')); }
        });
        $('#hfqkmesazhi').val('jo');

    }
    if ($('#hfqkmesazhi').val() == 'shfaqlupe') {
        Utils.hapPopUp(hfState.Get("msgShperndarjeNeQendratEKostos"), $('#hfUrl'));
    }
    if (hf.value === "true") {
        myFaqeCelje.kontrolloTeDrejta('Shto_Ekzekutim.aspx?shtim_modifikim=shtim', true);
    }
    else click = false;
}

function JopopupClick(s, e) {
    if ($('#hfUrl').val() != '')
        $('#hfUrl').val('');
}

function closePopup(s, e) {
    popupUniversal.SetContentUrl('');
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
    if (e.item.name === 'Ruaj' || e.item.name === 'Draft' || e.item.name === 'RuajPrint') {
        $('#hfRuajDraft')[0].value = e.item.name;
        if ($("input[id$='hfAutorizimi']").val() == 'False') {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukKeniAutorizimPerTeRuajturKeteDok"));
            Utils.hiqLoadingGif();
            click = false;
            e.processOnServer = false;
            return;
        }
        if (!myFaqeCelje.validim(s, e)) return;

        if (isValidKoka()) {
            Utils.shfaqLoadingGif();
            RuajClick(s, e);
        }
        else {
            e.processOnServer = false;
            //myMesazh.ShtoMesazhGabimi('Plotesoni te gjitha fushat');
        }
    }
    else if (e.item.name === 'Kerko') {
        myFaqeCelje.kontrolloTeDrejta('EkzekutimProdhimi.aspx', null, true);
        e.processOnServer = false;
    }
    else if (e.item.name === 'Shto') {
        myFaqeCelje.kontrolloTeDrejta('Shto_Ekzekutim.aspx?shtim_modifikim=shtim', true);
        e.processOnServer = false;
    }
    else if (e.item.name === 'Fshi') {
        if ($("input[id$='hfAutorizimi']").val() == 'False') {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukKeniAutorizimPerTeFshireDok"));
            Utils.hiqLoadingGif();
            click = false;
            e.processOnServer = false;
            return;
        }
        popFshi.Show();
        e.processOnServer = false;
    }
    else if (e.item.name === 'Anullo') {
        myFaqeCelje.kontrolloTeDrejta('EkzekutimProdhimi.aspx');
        e.processOnServer = false;
        click = false;
    }
    else if (e.item.name == 'Arkiva') {
        ButtonClickArkiva($('#hfArkivaDokId').val());
        e.processOnServer = false;
        click = false;
        return;
    }
}

function ButtonClickArkiva(idDok, lupeOptions) {//po    
    lupeOptions = lupeOptions ? lupeOptions : {};
    var defaults = {
        emerPopUpi: popupUniversal, titull: hfState.Get("regjisDokZgjidhDokPerTeBashkengjitur"), baseUrl: "LupaArkiva.aspx", width: 738, height: 548,
        params: { vjenNga: "EkzekutimProdhimi", veprimi: pageState.veprimi, idDok: idDok, tmpfolder: hfArkiva.Get("rootFolder") }
    };
    lupeOptions = $.extend({}, defaults, lupeOptions);
    Utils.hapLupe(lupeOptions);
}


function Kthehu(mesazh) {
    myFaqeCelje.kontrolloTeDrejta('EkzekutimProdhimi.aspx?ruaj=po');
}

var click = false;
/*
Function: RuajClick
 
Validon dokumentin dhe therret funksionin <merrTeDhena> per te marre te dhenat e grides kur klikohet butoni Ruaj.
*/
function RuajClick(s, e) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var grida1 = $("#rowed6");
    var idRow = grida1.getLastSel2();
    if (click) {
        e.processOnServer = false;
        Utils.hiqLoadingGif();
        return;
    }
    click = true;
    if (isValidKoka()) {
        $('#hfPlanifikime').val(JSON.stringify(arrPlanifikime));
        // merrTeDhenatArtPerberes(-1);
        grida.jqGrid('saveRow', idRresht, false, 'clientArray');
        grida.setLastSel2(0);
        jQuery("#rowed6").jqGrid('saveRow', idRow, false, 'clientArray');
        grida1.setLastSel2(0);
        merrTeDhenaProd();
        merrTeDhenaRec();
    }
    else {
        e.processOnServer = false;
        click = false;
        Utils.hiqLoadingGif();
    }
}
function merrTeDhenaProd(s, e) {//po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    grida.jqGrid('saveRow', idRresht, false, 'clientArray');
    var gridIds = grida.jqGrid('getDataIDs');
    var rreshtaTeGrides = grida.getTeDhenaRreshti();
    for (i = 0; i < rreshtaTeGrides.length; i++) {
        rreshtaTeGrides[i].txtFshi = gridIds[i];
    }

    $('#gridDataObject').val(JSON.stringify(rreshtaTeGrides));
    unformatoFushaDevi();
}
function merrTeDhenaRec(s, e) {//po
    var grida = $('#rowed6');
    var idRow = grida.getLastSel2();
    grida.jqGrid('saveRow', idRow, false, 'clientArray');

    var rreshtaTeGrides = grida.getTeDhenaRreshti();
    $('#gridDataObject2').val(JSON.stringify(rreshtaTeGrides));

}
/*
Function: PastroClick
 
Pastron koken e dokumentit dhe array-t e perdorura dhe inicializon griden.
Shiko funksionet <pastro>, <pastroFushatKokes> dhe <inicializoGride>.
*/
function PastroClick() {
    $("input[id$='hfAutorizimi']").val(true);
    pastro();
    pastroFushatKokes();
    var hf1 = $("#hfShtimModifikim");
    ASPxMenu1.GetItemByName('Shto').SetVisible(true);
    ASPxMenu1.GetItemByName('Draft').SetVisible(true);
    ASPxMenu1.GetItemByName('Fshi').SetVisible(false);
    ASPxMenu1.GetItemByName('FletaKontabel').SetVisible(false);
    ASPxMenu1.GetItemByName('QendraKosto').SetVisible(false);
    ASPxMenu1.AdjustControl();
    ndryshoKonfigurimin();
    hf1.val("shtim");
    myMenu.menuSipasTeDrejtaRegjistrim(hf1, hfTeDrejta);
    $('#ASPxSplitter1_hl').empty(); click = false;
}

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}


var coliArtPerb = new Array();
var keyGlobal;
var editorKodi;
var editorEmertimi;
var editorVlera;
var numerReshtashQeShtohen;
function InitArtPerb() {
    keyGlobal = -1;
    numerReshtashQeShtohen = 1;
}


var indeksiArtPerb;
var tekstiShkruar;
var editorMagazina;

var editorNjesia;
function ButtonClickKodi() {
    var grida = $('#rowed5');

    identikuesPerPopupArtikulli = 'EkzekutimProdhimi';
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("roundPanelZgjidhArtikullin"), 'LupaArtikull.aspx?klasa=0&idKonfigAmbjente=' + $("#hfGridaKodi")[0].value, 600, 500);
}

function totali() {

    var totali = 0;
    var grida = $("#rowed5");
    var idTe = grida.jqGrid('getDataIDs');
    for (var i = 0; i < idTe.length; i++) {
        if (grida.getTekstQelize('txtKodiArtikull', idTe[i]) != "") {
            var vlefta = grida.getTekstQelize('txtKostoTotale', idTe[i]);
            if (vlefta == '.' || vlefta == 'NaN' || vlefta == '')
                vlefta = 0;
            totali = parseFloat(totali) + parseFloat(vlefta);
        }
    }
    txtTotali.SetText(totali);
}

/*
 
Function: SelectionChangedGridFaturat
 
Kur ndryshojme selection-in e grides se faturave.
*/
function SelectionChangedGridFaturat(s, e) {
    grid_faturat.GetSelectedFieldValues('IdDokumenti', OnGetSelectedFieldValues);
}

function OnGetSelectedFieldValues(selectedValues) {
    if (selectedValues.length == 0)
        return;
    Utils.shfaqLoadingGif();
    for (var i = 0; i < selectedValues.length; i++) {
        OnGridFaturatSelectionComplete(selectedValues[i], i, selectedValues.length);
    }
}

/*
Function: eshteShtuarFatura
 
Kontrollon nese fatura (qe kalohet si parameter) eshte shtuar me pare te grida e trupit apo jo.
*/
function eshteShtuarFatura(id) {
    var grida = $("#rowed5");
    var eshteShtuar = false;
    var idTe = grida.jqGrid('getDataIDs');
    for (i = 0; i < idTe.length; i++) {
        if (grida.getTekstQelize('txtIdKoka', idTe[i]) == id) {
            eshteShtuar = true;
            break;
        }
    }
    return eshteShtuar;
}

function kontrolloPlanifikim(id) {
    var grida = $("#rowed5");
    var idTe = grida.jqGrid('getDataIDs');
    for (var i = 0; i < idTe.length; i++) {
        var idplan = grida.getTekstQelize('txtIdPlanifikim', idTe[i]);
        if (idplan == id && idplan != 0) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgKyDokumentPlanifikimiEshteZgjedhurNeGride"));
            return true;
        }
    }
    return false;
}

var arrPlanifikime = new Array();
function OnGridFaturatSelectionComplete(values, indeksFature, numerFaturash) { //po
    if (eshteShtuarFatura(values)) {
        if (indeksFature == numerFaturash - 1)
            Utils.hiqLoadingGif();
        return;
    }
    if ($.inArray(values, arrPlanifikime) !== -1)
        return;
    arrPlanifikime.push(values);
    var mag1 = 0;
    if (btneMagazina.GetText() != '')
        mag1 = btneMagazina.GetValue();
    var mag2 = 0;
    if (btneMagazin2a.GetText() != '')
        mag2 = btneMagazin2a.GetValue();
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "MerrPlanifikimeTeGjeneruara"),
        data: JSON.stringify({ id: values, magazinaprod: mag1, magazinarec: mag2, date: dteDtDok.GetDate(), sasiplanrec: sasiplanrec, sasiburimi: sasiburimi })
    }).done(function (result) { SucceededCallbackMbushGride(result, indeksFature, numerFaturash); });
}

var rezultatPlanifikimi;
//autor Pati, keto jane te perkoheshme sepse do behet ngarkimi ne grup me addJSONdata i planifikimeve
var indeksiFatures;
var numerFaturashSel;
function SucceededCallbackMbushGride(result, indeksFature, numerFaturash) {
    rezultatPlanifikimi = result;
    indeksiFatures = indeksFature;
    numerFaturashSel = numerFaturash;
    var idNjesiProdh = result.idnjesiprodhim;
    if (cmbNjesiProdhimi.GetVisible() && idNjesiProdh != 0) {
        if (cmbNjesiProdhimi.GetText() == '') {
            cmbNjesiProdhimi.SetValue(idNjesiProdh);
            mbushGrideMePlanifikimin(result, indeksFature, numerFaturash);
        }
        else {
            if (idNjesiProdh != cmbNjesiProdhimi.GetValue()) {
                popNdryshoNjesiProdhimi.Show();
            }
            else
                mbushGrideMePlanifikimin(result, indeksFature, numerFaturash);
        }
    }
    else
        mbushGrideMePlanifikimin(result, indeksFature, numerFaturash);
}

function ndryshoNjesiProdhimiJoClick(s, e) {
    popNdryshoNjesiProdhimi.Hide();
    return;
}

function ndryshoNjesiProdhimiPoClick(s, e) {
    cmbNjesiProdhimi.SetSelectedIndex(-1);
    cmbNjesiProdhimi.SetText('');
    popNdryshoNjesiProdhimi.Hide();
    mbushGrideMePlanifikimin(rezultatPlanifikimi, indeksiFatures, numerFaturashSel);
}

function mbushGrideMePlanifikimin(result, indeksFature, numerFaturash) {
    var grida = $("#rowed5");
    var colTrup = result.produktet;
    var colDetajimet = result.detajimet;
    var colnjesi = result.njesite;
    var colmag = result.magazinat;
    var gridIds = grida.jqGrid('getDataIDs');
    var idNdermarrje = hfState.Get('idNdermarrje');
    var index;
    if (gridIds.length > 0) {
        for (var i = 0; i < gridIds.length; i++) {
            if (grida.getTekstQelize('txtKodiArtikull', gridIds[i]) == '') {
                grida.jqGrid('delRowData', gridIds[i]);
            }

        }
        index = parseFloat(gridIds[gridIds.length - 1]) + 1;
    }
    else index = 1;
    var recepturat;
    var nrReshtiRi = grida.getRowData().length + 1;
    var idkoka, kodart, pershkart, sasiakt, kosto, gjeresi, gjatesi, sasipor, sasipermase, kostototale, mag, njesi, idart, idplanifikim, idtrupi, shenime, detajim1, detajim2;
    var disabled = arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides - 1] == 'True' || lidhur;
    var emergride = "#rowed5";
    for (var i = 0; i < colTrup.length; i++) {
        if (kontrolloPlanifikim(colTrup[i].IdPlanifikim))
            continue;
        recepturat = result.teDhenaRecepturash[i];
        idkoka = result.id;
        kodart = colTrup[i].KodiArtikull;
        pershkart = colTrup[i].PershkrimArtikull;
        shenime = colTrup[i].Shenime;
        kosto = colTrup[i].Kosto;
        kostototale = colTrup[i].KostoTotale;
        if (colmag[i].Kodi != null)
            mag = colmag[i].Kodi;
        else mag = colMagazina[0].Kodi;
        njesi = colnjesi[i].KodNjesia;
        idart = colTrup[i].IdArtikulli;
        idplanifikim = colTrup[i].IdPlanifikim;
        idtrupi = colTrup[i].IdUrdherPorosi;
        sasiakt = colTrup[i].SasiaAktuale == null ? "" : colTrup[i].SasiaAktuale;
        gjeresi = colTrup[i].GjeresiPlanifikuar == null ? "" : colTrup[i].GjeresiPlanifikuar;
        gjatesi = colTrup[i].GjatesiPlanifikuar == null ? "" : colTrup[i].GjatesiPlanifikuar;
        sasipor = colTrup[i].SasiaPlanifikuar == null ? "" : colTrup[i].SasiaPlanifikuar;
        sasipermase = colTrup[i].SasiPermase == null ? "" : colTrup[i].SasiPermase;
        detajim1 = colDetajimet[i].det1ArtProdhim.KodDetajimArtikulli == null ? "" : colDetajimet[i].det1ArtProdhim.KodDetajimArtikulli;
        detajim2 = colDetajimet[i].det2ArtProdhim.KodDetajimArtikulli == null ? "" : colDetajimet[i].det2ArtProdhim.KodDetajimArtikulli;

        if (result.art != undefined) {
            pageState.memoryArt.Set(result.art);
            var idkodi = result.art.IdArtikulli;
            var kodi = result.art.KodArtikulli;
            var artikulli = pageState.memoryArt.Get(idkodi);

            if (pageState.kushte.KGJMM && !Utils.IsNullOrEmpty(kodi)) {
                if (!Utils.IsNullOrEmpty(idkodi) && idkodi != "0") {
                    $.ajax({
                        pritPergjigje: true,
                        url: Utils.getServerApiUrl("Rregjistrime", "ktheGjendjeArtikulli"),
                        data: JSON.stringify({
                            idja: idkodi, mag: mag, data: dteDtDok.GetDate(), koddetajim: '', koddetajim2: '', iddok: Utils.IsUndefined(Utils.getUrlVar('id')) ? 0 : Utils.getUrlVar('id'),
                            idndermarje: idNdermarrje, idKonfigAmbjente: cmbKonfigurimi.GetValue(),
                            idreshti: index, totalartikulli: sasiakt, totaldetajim1: 0, totaldetajim2: 0, shitje_blerje: false, ekzekutimProdhim: true

                        })
                    }).done(SucceededCallbackKontrollGjendje);
                }
                pageState.hapurpermodifikim = false;
            }

        }



        var datarow = {
            txtNrRendor: nrReshtiRi, txtIdKoka: idkoka, txtIdArtikulli: idart, txtKodiArtikull: kodart, txtPershkrimArtikull: pershkart, txtIdNjesia: njesi, txtGjeresiPlanifikuar: gjeresi,
            txtGjatesiPlanifikuar: gjatesi, txtSasiaPlanifikuar: sasipor, txtSasiaAktuale: sasiakt, txtKosto: kosto, txtKostoTotale: kostototale, txtIdMag: mag, txtSasiPermase: sasipermase,
            txtIdPlanifikim: idplanifikim, txtIdUrdherPorosi: idtrupi, txtShenime: shenime, txtDetajimi: detajim1, txtDetajimi2t: detajim2
        };
        index = grida.shtoRresht(disabled, emergride, datarow);
        grida.setTekstQelize('txtGjeresiPlanifikuar', index, gjeresi);
        grida.setTekstQelize('txtGjatesiPlanifikuar', index, gjatesi);
        grida.setTekstQelize('txtSasiaPlanifikuar', index, sasipor);
        grida.setTekstQelize('txtSasiaAktuale', index, sasiakt);
        grida.setTekstQelize('txtKosto', index, kosto);
        grida.setTekstQelize('txtKostoTotale', index, kostototale);
        grida.setTekstQelize('txtSasiPermase', index, sasipermase);
        sasiavjeter = sasiakt;
        vendosRec(recepturat, index, colTrup[i].Id, false);
        nrReshtiRi = nrReshtiRi + 1;
    }
    pageState.hapurpermodifikim = false;
    totali();
    if (indeksFature == numerFaturash - 1)
        Utils.hiqLoadingGif();

}

function reloadgrid(grid) {
    grid.trigger("reloadGrid");
}

function vendosRec(recepturat, indexprindi, idprod, merrKostoMag) {

    var grida = $("#rowed6");
    var idRow = grida.getLastSel2();
    grida.jqGrid('saveRow', idRow, null, 'clientArray', {}, null);
    var rreshtat = grida.jqGrid('getGridParam', 'data');
    var gridIds2 = grida.jqGrid('getDataIDs');
    var idNdermarrje = hfState.Get('idNdermarrje');
    var index2, txtNrRendor;
    if (rreshtat.length > 0) {
        var indeksetToDel = new Array();
        for (var i = 0; i < rreshtat.length; i++) {
            if (typeof rreshtat[i].txtKodiArtikullR === "undefined" || rreshtat[i].txtKodiArtikullR == '') {
                indeksetToDel.push(i);
            }
            rreshtat[i].id = gridIds2[i];
        }
        for (var j = indeksetToDel.length - 1; j >= 0; j--) {
            rreshtat.splice(indeksetToDel[j], 1);
        }
        if (rreshtat.length == 0) {
            index2 = txtNrRendor = 1;
        }
        else {
            index2 = parseFloat(rreshtat[rreshtat.length - 1].id) + 1;
            txtNrRendor = parseFloat(rreshtat[rreshtat.length - 1].txtNrRendor) + 1;
        }
    }
    else {
        index2 = txtNrRendor = 1;
    }


    var idprodukti, kodprodukti, pershkprod, lloji, idartr, kodartr, pershkartr, njesir, idbur, sasiaktr, sasi, firo, scrap, kostor, kostototaler, magr, skedulim, datarow, detajim = "", detajim2 = "";

    for (var i = 0; i < recepturat.length; i++) {
        var teDhenaRec = recepturat[i];
        var receptura = teDhenaRec.rec;
        if (receptura.IdProdukti != idprod)
            continue;
        idprodukti = receptura.IdProdukti;
        kodprodukti = receptura.KodProdukti;
        pershkprod = receptura.PershkrimProdukti;
        lloji = (receptura.Lloji == 1) ? 'Artikull' : 'Burim';
        idartr = receptura.IdArtikulli;
        kodartr = receptura.KodiArtikull;
        pershkartr = receptura.PershkrimArtikull;


        njesir = (teDhenaRec.njesi.KodNjesia != "" && teDhenaRec.njesi.KodNjesia != null) ? teDhenaRec.njesi.KodNjesia : 'ore';
        idbur = receptura.IdBurimi;
        kostototaler = receptura.KostoTotale;
        if (btneMagazin2a.GetText() != '')
            magr = btneMagazin2a.GetText();
        else if (teDhenaRec.mag.Kodi != null)
            magr = teDhenaRec.mag.Kodi;
        else
            magr = colMagazina[0].Kodi;
        if (teDhenaRec.det1 != undefined && teDhenaRec.det1 != null && teDhenaRec.det1.KodDetajimArtikulli != null)
            detajim = teDhenaRec.det1.KodDetajimArtikulli;
        else detajim = "";

        if (teDhenaRec.det2 != undefined && teDhenaRec.det2 != null && teDhenaRec.det2.KodDetajimArtikulli != null)
            detajim2 = teDhenaRec.det2.KodDetajimArtikulli;
        else detajim2 = "";
        sasiaktr = receptura.SasiaAktuale == null ? "" : receptura.SasiaAktuale;
        sasi = receptura.Sasia == null ? "" : receptura.Sasia;
        firo = receptura.FiroPerqindje == null ? "" : receptura.FiroPerqindje;
        scrap = receptura.Scrap == null ? "" : receptura.Scrap;
        kostor = receptura.Kosto == null ? "" : receptura.Kosto;
        skedulim = receptura.Skedulim == null ? "" : receptura.Skedulim;
        if (arrayReadOnlyKolonaSubGrides[arrayReadOnlyKolonaSubGrides - 1] == 'True' || lidhur)
            ce = myJQGrid.myValueButtonFshi(true, index2, "#rowed6");
        else
            ce = myJQGrid.myValueButtonFshi(false, index2, "#rowed6");
        pageState.memoryArt.Set(teDhenaRec.artikulli);
        var idkodi = teDhenaRec.artikulli.IdArtikulli;
        var kodi = teDhenaRec.artikulli.KodArtikulli;
        var artikulli = pageState.memoryArt.Get(idkodi);
        datarow = {
            id: index2, name: index2, txtNrRendor: txtNrRendor, txtIdProdukti: idprodukti, txtKodProdukti: kodprodukti, txtPershkrimProdukti: pershkprod, txtLloji: lloji, txtIdArtikulliR: idartr,
            txtKodiArtikullR: kodartr, txtPershkrimArtikullR: pershkartr, txtNjesiArtikull: njesir, txtIdBurimi: idbur, txtSasiaAktualeR: sasiaktr, txtSasia: sasi, txtFiroPerqindje: firo,
            txtScrap: scrap, txtKostoR: kostor, txtKostoTotaleR: kostototaler, txtIdMagR: magr, txtIndexPrindi: indexprindi, txtSkedulim: skedulim, txtDetajimi: detajim, txtDetajimi2t: detajim2,
            txtFshiR: ce
        };

        rreshtat.push(datarow);
        index2++;
        txtNrRendor++;
    }
    grida.jqGrid('setGridParam', {
        datatype: 'local',
        data: rreshtat,
        rowNum: 10000000
    }).trigger("reloadGrid");

    grida.setLastSel2(0);

    idRow = 1;
    if (grida.jqGrid('getGridParam', 'reccount') == 0) {
        if (arrayReadOnlyKolonaSubGrides[arrayReadOnlyKolonaSubGrides.length - 1] == 'True')
            be = "<input id='butonFshi" + idRow + "' type='image' value='Fshi' disabled='disabled' onmouseover='ndryshoImazhin(1," + idRow + ")' onmouseout='ndryshoImazhin(0," + idRow + ")'  src='images/square-icon.png'  onclick='fshiClicked2(" + idRow + ")'/>";
        else
            be = "<input id='butonFshi" + idRow + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + idRow + ")' onmouseout='ndryshoImazhin(0," + idRow + ")'  src='images/square-icon.png' onclick='fshiClicked2(" + idRow + ")'/>";
        datarow = { txtFshiR: be };
        grida.jqGrid('addRowData', parseInt(idRow), datarow);
    }

    ndryshoSasiteERecepturave(indexprindi, merrKostoMag, pageState.hapurpermodifikim);
    callWebServiceInfoRow();
}

function spliterPaneExpanding(s, e) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    hapMbyllInfo(true);
    callWebServiceInfoRow();
}

function spliterPaneCollapsing(s, e) {
    hapMbyllInfo(false);
}

function spliterPaneCollapsed(s, e) {
    var grida = $('#rowed5');
    if ($('#divgride3').width() != null) {
        myJQGrid.fixGridWidth(grida, $('#divgride3'));
        myJQGrid.fixGridWidth($('#rowed6'), $('#divgride3'));
    }
}
function hapMbyllInfo(infoExpanded) {
    if (infoExpanded && infoArt) {
        if (!eshteInfoHapur())
            splitter.GetPane(1).Expand();
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

function eshteInfoHapur() {
    return !splitter.GetPane(1).IsCollapsed();
}



function callWebServiceInfoRow() {
    if (!eshteInfoHapur())
        return;
    var tabIndex = PageControl.GetActiveTabIndex();
    var grida = ktheGrideSipasTabit(tabIndex);
    var editorIdArtikulli = ktheEmerEditoreshSipasTabit(tabIndex).emerIdArt;
    var editorIdMag = ktheEmerEditoreshSipasTabit(tabIndex).emerIdMag;
    var idRow = grida.getLastSel2();
    var idKodi = grida.getTekstQelize(editorIdArtikulli, idRow);
    if (Utils.IsNullOrEmpty(idKodi) || idKodi == "0")
        return;
    if (infoArt) {
        var mag = grida.getTekstQelize(editorIdMag, idRow);
        var idViti = hfState.Get('idViti');
        pastroInfoArt();
        var detajim1 = grida.getTekstQelize('txtDetajimi', idRow);
        var detajim2 = grida.getTekstQelize('txtDetajimi2t', idRow);

        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "mbushInfoArtikulliMeDetajime"),
            data: JSON.stringify({
                idkodi: idKodi, data: dteDtDok.GetDate(), detajim: detajim1, index: idRow, idInfo: idInfoArt, detajim2: detajim2, idViti: idViti,
                idPerdoruesi: hfState.Get('idPerdoruesi'), idklient: 0, mag: mag, njesiart: '', idKarta: 0
            })
        }).done(function (result) { SucceededCallbackInfoArt(result, grida); });
    }
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
            myMesazh.ShtoMesazhGabimi('Ka nje gabim tek rezultati i infos!');
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

function SucceededCallbackInfoArt(result, gridaOrig) {
    var grida = ktheGrideSipasTabit(PageControl.GetActiveTabIndex());
    if (grida.selector !== gridaOrig.selector)
        return;
    var idRresht = grida.getLastSel2();
    if (result.idRreshti == idRresht && result.colInfoTrupi.length != 0) {
        navbar.GetGroupByName('Artikulli').SetExpanded(true);
        vendosInfo(lbxZgjedhur, result);
    }
}

function activeTabsChanged(s, e) {
    var grida = ktheGrideSipasTabit(e.tab.index);
    var idRresht = grida.getLastSel2();
    callWebServiceInfoRow();
}

function ktheGrideSipasTabit(indexTabi) {
    return indexTabi == 0 ? $('#rowed5') : $('#rowed6');
}

function ktheEmerEditoreshSipasTabit(indexTabi) {
    return indexTabi == 0 ? { emerIdArt: 'txtIdArtikulli', emerIdMag: 'txtIdMag', kodi: 'txtKodiArtikull', sasia: 'txtSasiaAktuale' } : { emerIdArt: 'txtIdArtikulliR', emerIdMag: 'txtIdMagR', kodi: 'txtKodiArtikullR', sasia: 'txtSasiaAktualeR' };
}


function getInfoArtStructure(idInfoArt) {
    try {
        var idNdermarrje = hfState.Get('idNdermarrje');
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "getInfoArtStructure"),
            data: JSON.stringify({ idkoka: idInfoArt, idNdermarrje: idNdermarrje })
        }).done(SuccededCallbackInfoArtStructure);
    } catch (e) {
        myMesazh.ShtoMesazhGabimi('Ndodhi nje gabim gjate marrjes se te dhenave!');
    }
}

function SuccededCallbackInfoArtStructure(result) {
    vendosInfo(lbxZgjedhur, result, true);
}

var infoArt = false;
var idInfoArt = 0;
var mbyll = true;

function RuajHapurMbyllur(hapur) {
    mbyll = false;
    if (!hapur) {
        splitter.GetPane(1).CollapseBackward();
        $('#hfHapurMbyllur').val('False');
    }
    else $('#hfHapurMbyllur').val('True');
    var idPerdoruesi = hfState.Get('idPerdoruesi');
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ruajHapurMbyllur"),
        data: JSON.stringify({ hapur: hapur, idperdorues: idPerdoruesi, idperdoruesveprimi: idPerdoruesi })
    }).done(SuccededCallbackHapurMbyllur);
}

function RuajHapurMbyllurplus(hapur) {
    var idPerdoruesi = hfState.Get('idPerdoruesi');
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
        data: JSON.stringify({ hapur: hapur, idperdorues: idPerdoruesi, idperdoruesveprimi: idPerdoruesi })
    }).done(SuccededCallbackHapurMbyllur);
}

function RuajHapurMbyllurminus(hapur) {
    var idPerdoruesi = hfState.Get('idPerdoruesi');
    mbyll = false;
    if (!hapur) {
        splitter.GetPane(1).CollapseBackward();
        $('#hfHapurMbyllur').val('False');
    }
    else
        $('#hfHapurMbyllur').val('True');
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ruajHapurMbyllurminus"),
        data: JSON.stringify({ hapur: hapur, idperdorues: idPerdoruesi, idperdoruesveprimi: idPerdoruesi })
    }).done(SuccededCallbackHapurMbyllur);
}


function SuccededCallbackHapurMbyllur(result) {
    myMesazh.ShtoMesazhSuksesi(result);
}

function KontrolloTeDrejta(s) {
    var alti = $(s.GetValue()).attr('alt');
    if (alti == "Artikulli" && $('#hfTeDrejtaInfoArt').val() == 'False')
        s.SetEnabled(false);
}

function HeaderClick(s, e) {
    if (!mbyll) { e.cancel = true; mbyll = true; } //per rastet kur shtyp butonat + dhe -
}

function Expanded() {
    lbxZgjedhur.SetHeight(lbxZgjedhur.GetItemCount() * 22 + 28);
}

function Succedcallback(result) {
    lbxZgjedhur.ClearItems();
    for (var i = 0; i < result.length; i++) {

        lbxZgjedhur.AddItem([result[i].PershkrimKolone, ''], result[i].EmerKolone);
    }
    lbxZgjedhur.SetHeight(result.length * 22 + 28);
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

function ButtonClickNjesiProdhimi(s, e) {
    identifikuesPerPopupNjesiProdhimi = "Ekzekutim";
    var hfNjesiProdhimi = document.getElementById("hfLupaNjesiProdhimi");
    myButtonClickLupa.LupaUniversal_Click('Zgjidh Njesi Prodhimi', 'LupaNjesiProdhimi.aspx?idKonfigAmbjente=' + hfNjesiProdhimi.value, 700, 560);
}

function TextChangedNjesiProdhimi(s, e) {
    if (isNaN(cmbNjesiProdhimi.GetValue())) {
        cmbNjesiProdhimi.SetText('');
        cmbNjesiProdhimi.Focus();
        return;
    }
}
function onHapLupeArkive() {
    var grida = $(pageState.gridaSelector);
    var grida2 = $(pageState.gridaSelector2);
    var idArtikull = grida.getTekstQelize('txtIdArtikulli', grida.getLastSel2());
    var idArtikull2 = grida2.getTekstQelize('txtIdArtikulli', grida2.getLastSel2());

    if (idArtikull) {
        ButtonClickArkiva(idArtikull, { params: { veprimi: "artikull", idDok: idArtikull } });
    }
    else if (idArtikull2) {
        ButtonClickArkiva(idArtikull2, { params: { veprimi: "artikull", idDok: idArtikull2 } });
    }
    else {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniArtikullin"));
    }
}

function onHapPopUpImazheArkive() {
    var grida = $(pageState.gridaSelector);
    var grida2 = $(pageState.gridaSelector2);
    var idArtikull = grida.getTekstQelize('txtIdArtikulli', grida.getLastSel2());
    var idArtikull2 = grida2.getTekstQelize('txtIdArtikulli', grida2.getLastSel2());

    if (idArtikull) {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheImazheArkive"),
            data: JSON.stringify({ idEntitet: idArtikull, idKategoria: 13 }) //13 - kategoria per artikullin
        }).done(hapLupeImazheArkive).fail(function (obj) { console.error(obj.responseText); });
    }
    else if (idArtikull2) {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheImazheArkive"),
            data: JSON.stringify({ idEntitet: idArtikull2, idKategoria: 13 }) //13 - kategoria per artikullin
        }).done(hapLupeImazheArkive).fail(function (obj) { console.error(obj.responseText); });
    }
    else {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniArtikullin"));
    }
}

function hapLupeImazheArkive(result) {
    //{ statusPergjigje = mesazh.Status, mesazh = mesazh.PershkrimMesazhi, listaUrl = myArkiva }
    if (!result.statusPergjigje) {
        myMesazh.ShtoMesazhGabimi(result.mesazh);
        return;
    }
    Arkiva.HapLupeImazheArkive(result.listaUrl);
}