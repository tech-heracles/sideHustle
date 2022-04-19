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
var memoryLlog;
var identikuesPerPopupKursi = "ShtoFleteKontabel";
var llojkursi = 1; //perdoret per te ruajtur llojin e kursit te zgjedhur tek konfigurimi. Ne qofte se nuk ka asnje lloj te zgjedhur, atehere merret lloji i pare.
var widthLupaKursi = 950;
var heightLupaKursi = 560;

var regjQK = new dxQendraKosto();

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
    identifikuesPerLocalStorageKey: 'FleteKontabel'
};


jQuery(document).ready(function () {
    Utils.konfiguroAccorditionNeDocReady(varKonfig.identifikuesPerLocalStorageKey);
    memoryLlog = new memory("IdLlogari");
    $(window).on('resize', function () {
        try {
            if (Utils.isGridResized())
                return;
        }
        catch (ee) {
        }
        var grida = $("#rowed5");
        if ($('#divgride2').width() != null || $('#divgride2').width() != undefined) {
            grida.setGridWidth($('#divgride2').width() - 5, true);
        }
    }).trigger('resize');

    merrFormatNumrash();
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

/*
Function: inicializoGride

Inicializon griden e trupit. Konfiguron kolonat e grides dhe percakton veprimin qe kryhet onCellSelect.
*/
function inicializoGride() {
    if ($("input[id$='hfLidhur']").val() == 'True')
        lidhur = true;
    else lidhur = false;
    mbushArrayMonedha();
    var classes = '';
    if (lidhur == true && $("input[id$='hfShtimModifikim']").val() == 'modifikim')
        classes = 'uigray';
    var arrayPershkrime = [arrayPershkrimiKolonaGrides[0], arrayPershkrimiKolonaGrides[1], arrayPershkrimiKolonaGrides[2], arrayPershkrimiKolonaGrides[3],
    arrayPershkrimiKolonaGrides[4], arrayPershkrimiKolonaGrides[5], arrayPershkrimiKolonaGrides[6], arrayPershkrimiKolonaGrides[7],
    arrayPershkrimiKolonaGrides[8], arrayPershkrimiKolonaGrides[9], arrayPershkrimiKolonaGrides[10]];
    var arrayModel = [
        { name: arrayIdKolonaGrides[0], index: arrayIdKolonaGrides[0], width: arrayWidthKolonaGrides[0], hidden: arrayVisibleKolonaGrides[0], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemNrRendor, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[1], index: arrayIdKolonaGrides[1], width: arrayWidthKolonaGrides[1], hidden: arrayVisibleKolonaGrides[1], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myelemNrlogarie, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[2], index: arrayIdKolonaGrides[2], width: arrayWidthKolonaGrides[2], hidden: arrayVisibleKolonaGrides[2], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myElemEmertimi, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[3], index: arrayIdKolonaGrides[3], width: arrayWidthKolonaGrides[3], hidden: arrayVisibleKolonaGrides[3], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myElemPershkrimi, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[4], index: arrayIdKolonaGrides[4], width: arrayWidthKolonaGrides[4], hidden: arrayVisibleKolonaGrides[4], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemCombo, custom_value: myJQGrid.myValueCombo } },
        { name: arrayIdKolonaGrides[5], index: arrayIdKolonaGrides[5], width: arrayWidthKolonaGrides[5], hidden: arrayVisibleKolonaGrides[5], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemKursi, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[6], index: arrayIdKolonaGrides[6], width: arrayWidthKolonaGrides[6], hidden: arrayVisibleKolonaGrides[6], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemVldebi, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[7], index: arrayIdKolonaGrides[7], width: arrayWidthKolonaGrides[7], hidden: arrayVisibleKolonaGrides[7], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemVlkredi, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[8], index: arrayIdKolonaGrides[8], width: arrayWidthKolonaGrides[8], hidden: arrayVisibleKolonaGrides[8], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemVlmondebi, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[9], index: arrayIdKolonaGrides[9], width: arrayWidthKolonaGrides[9], hidden: arrayVisibleKolonaGrides[9], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemVlmonkredi, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[10], index: arrayIdKolonaGrides[10], width: arrayWidthKolonaGrides[10], hidden: arrayVisibleKolonaGrides[10], classes: classes, sorttype: "int", editable: true, edittype: 'custom', editoptions: { custom_element: myElemButtonFshi, custom_value: myValueButtonFshi }, hidedlg: true }
    ];
    try {

        var selektoriGrides = "#rowed5";

        var gridParams = {
            emergride: selektoriGrides,
            arrayPershkrime: arrayPershkrime,
            arrayModel: arrayModel,
            lidhur: lidhur,
            emerEditorKodi: arrayIdKolonaGrides[1],
            widthi: $('#divgride2').width() - 5,
            resetRreshtKorent: resetRreshtKorent,
            lostFocusKoloneFundit: lostFocusKoloneFundit,

            autocompleteList: [{ emerEditor: arrayIdKolonaGrides[1], selectFunc: selectFunc, changeFunc: changeFunc, shtoDataKod: false }],

            konfigToolbar: {
                konfigGrid: $('#hfTeDrejtaKonfGride').val(),
                ruajKolonatEGrides: ruajKolonatEGrides
            }

        };
        return myJQGrid.initGride(gridParams);

        //myJQGrid.inicializoGride("#rowed5", arrayPershkrime, arrayModel, lidhur,
        //    lastsel2, '#' + arrayIdKolonaGrides[1], null, null, null, null, null, $('#divgride2').width() - 5,
        //    undefined, undefined, undefined, undefined, undefined, undefined, undefined, undefined, $('#hfTeDrejtaKonfGride').val());
    }
    catch (e) {
    }

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
    grida.setVlereDefault('txtKursi', 1);
    grida.setVlereDefault('txtVlmonkredi', 0);
    grida.setVlereDefault('txtVlmondebi', 0);
    grida.setVlereDefault('txtVlkredi', 0);
    grida.setVlereDefault('txtVldebi', 0);
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
    var formatMOn = $.parseJSON(hfState.Get("formatKurset"));
    grida.setShifraPasPresjes('txtKursi', formatMOn[0].IdFormatNrKursi);
    grida.setShifraPasPresjes('txtVlmonkredi', formatNumri.KonfigTrupi[0].ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtVlmondebi', formatNumri.KonfigTrupi[0].ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtVlkredi', formatNumri.KonfigTrupi[0].ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtVldebi', formatNumri.KonfigTrupi[0].ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtDebiMonedhaBaze, formatNumri.KonfigTrupi[0].ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtKrediMonedhaBaze, formatNumri.KonfigTrupi[0].ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtDebiDiferenca, formatNumri.KonfigTrupi[0].ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtKrediDiferenca, formatNumri.KonfigTrupi[0].ShifraPasPresjesVlefta);
}

function gjejFormatSipasMonedhes(idmonedha) {
    var formatNumri = $.parseJSON(hfState.Get("formatMonedhe"));
    for (i = 0; i < formatNumri.KonfigTrupi.length; i++)
        if (formatNumri.KonfigTrupi[i].IdMonedha == idmonedha) {
            return formatNumri.KonfigTrupi[i]
        }
    return formatNumri.KonfigTrupi[0];
}

function gjejFormatKursiSipasMonedhes(idmonedha) {
    var formatNumri = $.parseJSON(hfState.Get("formatKurset"));
    for (i = 0; i < formatNumri.length; i++)
        if (formatNumri[i].IdMonedha == idmonedha) {
            return formatNumri[i].IdFormatNrKursi;
        }
    return 2;
}

function dateGotFocus(s, e) {
    window.setTimeout(function () { s.SetCaretPosition(0); }, 0);
}

/*
Formaton vlerat e fushave sipas formatit perkates.
*/
function vendosKonfigFormatNumri() {
    var grida = $('#rowed5');
    var idTe = grida.jqGrid('getDataIDs');
    for (j = 0; j < idTe.length; j++) {
        var idRreshti = idTe[j];
        grida.formatoQelize('txtKursi', idRreshti);
        grida.formatoQelize('txtVlmonkredi', idRreshti);
        grida.formatoQelize('txtVlmondebi', idRreshti);
        grida.formatoQelize('txtVlkredi', idRreshti);
        grida.formatoQelize('txtVldebi', idRreshti);
    }
    formatoFushaDevi();
}

function formatoFushaDevi() {
    Utils.formatoTextBox(txtDebiMonedhaBaze);
    Utils.formatoTextBox(txtKrediMonedhaBaze);
    Utils.formatoTextBox(txtDebiDiferenca);
    Utils.formatoTextBox(txtKrediDiferenca);
}

var arrformatevlefta = new Array();
var arrformatevleftaqk = new Array();
var arrformatevleftamon = new Array();
function unformatoFushaDevi() {
    Utils.unFormatoTextBox(txtDebiMonedhaBaze);
    Utils.unFormatoTextBox(txtKrediMonedhaBaze);
    Utils.unFormatoTextBox(txtDebiDiferenca);
    Utils.unFormatoTextBox(txtKrediDiferenca);
}


function mbushArrayMonedha() {
    try {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheMonedheNdermarrjeClientSide"),
            data: JSON.stringify({})
        }).done(SucceededCallbackMonedha);
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
    }
}

function ndryshoImazhin(nr, index) {
    myJQGrid.ndryshoImazhin(nr, index);
}

var lidhur = false;
function kursiFmatter(cellvalue, options, rowObject) {
    return myJQGrid.kursiFmatter(cellvalue, options, rowObject);
}

function formoArrayKolGrides() {
    var arr = $("input[id$='HfGridCol']");
    var hfGridLlog = $("input[id$='hfGridaLlogaria']");
    var IdKonfigAmbjenteLupat = [{ hfVar: hfGridLlog, kodiText: "txtNrllogarie" }];
    myJQGrid.formArrayKolGrides(arr, arrayIdKolonaGrides, arrayPershkrimiKolonaGrides, arrayVisibleKolonaGrides, arrayReadOnlyKolonaGrides, lidhur, arrayWidthKolonaGrides, IdKonfigAmbjenteLupat, arrayRenditjeKolonaGrides)
}
/*
Function: renditKolonatGrides

Therret metoden remapColumns te jqGrid per te renditur kolonat e grides sipas vlerave te array-t qe i kalohet kesaj metode si parameter
*/
//function renditKolonatGrides() {
//    myJQGrid.renditKolonatGrides("#rowed5", arrayRenditjeKolonaGrides);
//}


function SucceededCallbackMonedha(result) {
    arrayMeMonedha = result.split("|");
}


/*
Function: myelemNrlogarie

Nderton nje textbox dhe nje buton per te zgjedhur llogarine
*/
function myelemNrlogarie(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[1];
    return myJQGrid.myElemKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[1], ButtonClickKodi, keyPressKodi, changeFunc);
}

/*
Function: keyPressKodi

Shton nje rresht te ri ne gride nese jemi ne rreshtin e fundit dhe therret funksionin <callWebserviceKodi>.
*/
function keyPressKodi() {
    ASPxMenu1.GetItemByName('QendraKosto').SetVisible(false);
    $("#div1").hide();
    $("#divQk").hide();
    callWebserviceListeLlogarish();
}

function myValueButtonFshi(elem, operation, value) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1] == 'True')
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
    if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1] == 'True')
        return myJQGrid.myElemButtonFshi(true, idRresht, "#rowed5", lostFocusKoloneFundit);
    else
        return myJQGrid.myElemButtonFshi(false, idRresht, "#rowed5", lostFocusKoloneFundit);
}


/*
Function: myElemEmertimi

Nderton nje textbox ku vendoset emertimi i klient furnitorit
*/
function myElemEmertimi(value) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemEmertimi(value, arrayReadOnlyKolonaGrides[2], idRresht, 'txtEmertimi');
}

/*
Function: myElemPershkrimi

Nderton nje textbox ku vendoset pershkrimi
*/
function myElemPershkrimi(value) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if (value == "")
        value = txtPershkrimi.GetText();
    return myJQGrid.myElemEmertimi(value, arrayReadOnlyKolonaGrides[3], idRresht, 'txtPershkrimi');
}

/*
Function: myelemMonedha

Nderton nje textbox ku vendoset monedha
*/
//function myelemMonedha(value) {
//    return myJQGrid.myElemEmertimi(value, arrayReadOnlyKolonaGrides[3], lastsel2, 'txtMonedha');

//}
var kursifundit = 0;

/*
Function: myElemKursi

Nderton nje textbox per te vendosur kursin.
*/
function myelemKursi(value, options) {
    disabled = arrayReadOnlyKolonaGrides[5]
    hf = $("input[id$='hfMonedhaNder']"); var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();

    if (hf.val() == grida.getTekstQelize('txtMonedha', idRresht))
        disabled = 'True';
    if (value == '') {
        value = "1"; disabled = 'True';
    }

    kursifundit = value;

    var el = myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, disabled, idRresht, 'txtKursi', textChangedKursi, hfFormatNumri, focusutKursi, true, ButtonClickKursi);
    $(document).on("blur", '#txtKursi' + idRresht, function () {
        var grida = $('#rowed5');
        var diferenca = Math.abs(kursifundit - parseFloat(grida.getTekstQelize('txtKursi', idRresht)));
        if (diferenca / kursifundit > 0.2)
            myMesazh.ShtoMesazhInformues(hfState.Get("msgKursiRiNdryshonShumeMeKursinMePare"));
    });
    return el;
}

function focusutKursi() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var kursi = grida.getTekstQelize('txtKursi', idRresht);
    if (kursi === "" || isNaN(kursi))
        grida.setTekstQelize('txtKursi', idRresht);
    if (parseFloat(kursi) === 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKursiNukMundTeJeteZero"));
        grida.setTekstQelize('txtKursi', idRresht);
    }
}

function textChangedKursi() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var kursi = grida.getTekstQelize('txtKursi', idRresht);
    if (parseFloat(kursi) <= 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKursiNukMundTeJeteNegativOseZero"));
        kursi = grida.getVlereDefault('txtKursi');
    }
    var monedha = $('#txtMonedha' + idRresht + ' option:selected').text();
    for (var i = 0; i < arrFormati.length; i++) {
        var arrKonfig = new Array();
        arrKonfig = arrFormati[i].split(':');
        if (monedha == arrKonfig[0]) {
            formati = arrKonfig[1] - 1;
        }
    }
    var idKontrolliKursi = grida.getTekstQelize('txtKursi', idRresht);
    if (idKontrolliKursi == "" || isNaN(idKontrolliKursi))
        idKontrolliKursi = 1;
    var idKontrolliDebi = grida.getTekstQelize('txtVldebi', idRresht);
    var idKontrolliKredi = grida.getTekstQelize('txtVlkredi', idRresht);
    grida.setTekstQelize('txtVlmondebi', idRresht, parseFloat(idKontrolliDebi) * parseFloat(idKontrolliKursi));
    grida.setTekstQelize('txtVlmonkredi', idRresht, (parseFloat(idKontrolliKredi) * parseFloat(idKontrolliKursi)));
    updateTotalet();
}

/*
Function: myelemVldebi

Nderton nje textbox per te vendosur vleften.
*/
function myelemVldebi(value, options) {
    var disabl = 'False';
    if (azhornim)
        disabl = 'True';
    else
        disabl = arrayReadOnlyKolonaGrides[6];
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: disabl, indexRow: idRresht, id: "txtVldebi", onKeyDown: textChangedDebiNew, onFocusout: lostFocusVlera });
    //return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, disabl, idRresht, 'txtVldebi', textChangedDebiNew, hfFormatNumri, lostFocusVlera);
}

/*
Function: myelemVlkredi

Nderton nje textbox per te vendosur vleften.
*/
function myelemVlkredi(value, options) {
    var disabled = 'False';
    if (azhornim)
        disabled = 'True'; 
    else
        disabled = arrayReadOnlyKolonaGrides[7];
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: disabled, indexRow: idRresht, id: "txtVlkredi", onKeyDown: textChangedKrediNew, onFocusout: lostFocusVlera });
    //return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, disabled, idRresht, 'txtVlkredi', textChangedKrediNew, hfFormatNumri, lostFocusVlera);
}

/*
Function: myelemVlmondebi

Nderton nje textbox per te vendosur vleften.
*/
function myelemVlmondebi(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var disabl = arrayReadOnlyKolonaGrides[8];
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: disabl, indexRow: idRresht, id: "txtVlmondebi", onKeyDown: textChangedDebiMonNew, onFocusout: lostFocusVlera });
    //return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, disabl, idRresht, 'txtVlmondebi', textChangedDebiMonNew, hfFormatNumri, lostFocusVlera);
}

/*
Function: myelemVlmonkredi

Nderton nje textbox per te vendosur vleften.
*/
function myelemVlmonkredi(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var disabl = arrayReadOnlyKolonaGrides[9];
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: disabl, indexRow: idRresht, id: "txtVlmonkredi", onKeyDown: textChangedKrediMonNew, onFocusout: lostFocusVlera });
    //return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, disabl, idRresht, 'txtVlmonkredi', textChangedKrediMonNew, hfFormatNumri, lostFocusVlera);
}

/*
Function: fshiClicked

Fshin nje rresht te grides

Parameters:

index - Id e rreshtit qe do fshihet    
*/
function fshiClicked(index) {
    ASPxMenu1.GetItemByName('QendraKosto').SetVisible(false);
    $("#div1").hide();
    $("#divQk").hide();
    var grida = $("#rowed5");
    var idRresht = grida.getLastSel2();
    var ids = grida.getDataIDs();
    for (var i = 0; i < ids.length; i++) {
        if (ids[i] <= index)
            continue;
        if (grida.getTekstQelize('txtNrRendor', ids[i]) == undefined || typeof (grida.getTekstQelize('txtNrRendor', ids[i])) == 'undefined' || grida.getTekstQelize('txtNrRendor', ids[i]) == '')
            continue;
        grida.setTekstQelize('txtNrRendor', ids[i], parseInt(grida.getTekstQelize('txtNrRendor', ids[i])) - 1);
    }
    myJQGrid.fshiClicked(index, "#rowed5", inicializoGride);
    if ($("#" + arrayIdKolonaGrides[1] + idRresht).val() == undefined)
        updateTotalet(0, 0, 0, 0);
    else
        updateTotalet(grida.getTekstQelize(arrayIdKolonaGrides[6], idRresht), grida.getTekstQelize(arrayIdKolonaGrides[7], idRresht), grida.getTekstQelize(arrayIdKolonaGrides[8], idRresht), grida.getTekstQelize(arrayIdKolonaGrides[9], idRresht));
}

function myelemCombo(value, options) {
    var arrayMonedha = new Array();
    for (var i = 0; i < arrayMeMonedha.length; i++) {
        var tmpArray = new Array();
        tmpArray = arrayMeMonedha[i].split(";");
        var objMonedhe = new Object();
        objMonedhe.value = tmpArray[0];
        objMonedhe.text = tmpArray[1];
        arrayMonedha[i] = objMonedhe;
    }
    var disabled;
    var grida = $("#rowed5");
    var idRresht = grida.getLastSel2();
    if (arrayReadOnlyKolonaGrides[4] == 'True' || $('#' + arrayIdKolonaGrides[1] + idRresht).val() != "")
        disabled = true;
    else
        disabled = false;
    return myJQGrid.myElemCombo(value, "txtMonedha", idRresht, monedhaTextChanged, arrayMonedha, disabled);
}

function monedhaTextChanged(el) {
    var grida = $("#rowed5");
    var idRresht = grida.getLastSel2();
    if (grida.getTekstQelize('txtMonedha', idRresht) == $("input[id$='hfMonedhaNder']").val()) {

        grida.setTekstQelize('txtKursi', idRresht);
        $('#txtKursi' + idRresht).attr('disabled', true);
        $('#btntxtKursi' + idRresht).attr('disabled', true);
        textChangedKursi();
        return;
    }

    callWebservice1($('#txtMonedha' + idRresht).val(), formatDate(Data_DateEdit.GetValue(), 'MM/dd/yyyy'), idRresht, "");
}

//nuk perdoret
function shtoResht() {
    var datarow = {
        txtNrllogarie: "", txtEmertimi: "", txtPershkrimi: "", txtMonedha: "",
        txtKursi: "", txtVldebi: "", txtVlkredi: "", txtVlmondebi: "", txtVlmonkredi: "", txtFshi: be
    };
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    myJQGrid.keyPressKodi(grida, idRresht, arrayReadOnlyKolonaGrides[10], datarow);
    var idkontrolli = "#txtNrllogarie" + idRresht;
    var name = jQuery(idkontrolli)[0].value;
    try {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheEmerLlogarie"),
            data: JSON.stringify({ prefixText: name })
        }).done(SucceededCallback);
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
    }
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
    if (($('#' + 'txtNrRendor' + idRresht).attr("disabled") == 'disabled')) {
        $('#txtNrllogarie' + idRresht).focus();
        $('#txtNrllogarie' + idRresht).blur();
        $('#txtNrllogarie' + idRresht).focus();
    }
}

function myvalue(elem, operation, value) {
    // return $(elem).children()[0].val();
    return elem[0].firstChild.value;
}

/*
Function: myvalueCombo

Percakton vleren qe merr kolona qe ka kete funksion si custom_value (thirret kur kolona eshte combobox-i DebiKredi)
*/
function myvalueCombo(elem, operation, value) {
    return myJQGrid.myvaluecombo(elem, operation, value);
}

/*
Function: myvalueFshi

Percakton vleren qe merr kolona qe ka butonin Fshi.
*/
function myvalueFshi(elem) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemButon(arrayReadOnlyKolonaGrides[10], idRresht, '');
}


function textChangedDebiNew() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    ASPxMenu1.GetItemByName('QendraKosto').SetVisible(false);
    $("#div1").hide();
    $("#divQk").hide();
    //$("#div1")[0].style.visibility = 'hidden'; $("#div1")[0].style.display = 'none';

    var monedha = $('#txtMonedha' + idRresht + ' option:selected').text();
    for (var i = 0; i < arrFormati.length; i++) {
        var arrKonfig = new Array();
        arrKonfig = arrFormati[i].split(':');
        if (monedha == arrKonfig[0]) {
            formati = arrKonfig[1] - 1;
        }
    }

    var idKontrolliKursi = grida.getTekstQelize('txtKursi', idRresht);
    var idKontrolliDebi = grida.getTekstQelize('txtVldebi', idRresht);
    var idKontrolliKredi = grida.getTekstQelize('txtVlkredi', idRresht);
    var vlDebi;
    if (idKontrolliDebi == "-") {
        vlDebi = 0;
        grida.setTekstQelize('txtVldebi', idRresht, "-");
        return;
    }
    if (idKontrolliDebi == ".") {
        grida.setTekstQelize('txtVldebi', idRresht, '0.');
        return;
    }
    if (isNaN(idKontrolliDebi) || idKontrolliDebi == "") {
        vlDebi = 0;
        grida.setTekstQelize('txtVldebi', idRresht, parseFloat(vlDebi));
    }
    else vlDebi = idKontrolliDebi;

    grida.setTekstQelize('txtVlmondebi', idRresht, parseFloat(vlDebi) * parseFloat(idKontrolliKursi));
    grida.setTekstQelize('txtVlmonkredi', idRresht, (parseFloat(idKontrolliKredi) * parseFloat(idKontrolliKursi)));

    updateTotalet();
}

function lostFocusVlera() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var idKontrolliDebi = grida.getTekstQelize('txtVldebi', idRresht);
    if (isNaN(idKontrolliDebi) || idKontrolliDebi == "") {
        vlDebi = 0;
        grida.setTekstQelize('txtVldebi', idRresht, parseFloat(vlDebi));
    }
    var idKontrolliMonDebi = grida.getTekstQelize('txtVlmondebi', idRresht);
    if (isNaN(idKontrolliMonDebi) || idKontrolliMonDebi == "") {
        vlDebi = 0;
        grida.setTekstQelize('txtVlmondebi', idRresht, parseFloat(vlDebi));
    }
    var idKontrolliMonKredi = grida.getTekstQelize('txtVlmonkredi', idRresht);
    if (isNaN(idKontrolliMonKredi) || idKontrolliMonKredi == "") {
        vlKredi = 0;
        grida.setTekstQelize('txtVlmonkredi', idRresht, parseFloat(vlKredi));
    }
    var idKontrolliKredi = grida.getTekstQelize('txtVlkredi', idRresht);
    if (isNaN(idKontrolliKredi) || idKontrolliKredi == "") {
        vlKredi = 0;
        grida.setTekstQelize('txtVlkredi', idRresht, parseFloat(vlKredi));
    }
}

function textChangedDebiMonNew() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    ASPxMenu1.GetItemByName('QendraKosto').SetVisible(false);
    $("#div1").hide();
    $("#divQk").hide();
    var idKontrolliKursi = grida.getTekstQelize('txtKursi', idRresht);
    var idKontrolliDebi = grida.getTekstQelize('txtVlmondebi', idRresht);
    var idKontrolliKredi = grida.getTekstQelize('txtVlmonkredi', idRresht);
    var vlDebi;
    if (idKontrolliDebi == "-") {
        vlDebi = 0;
        grida.setTekstQelize('txtVlmondebi', idRresht, "-");
        return;
    }
    if (idKontrolliDebi == ".") {
        grida.setTekstQelize('txtVlmondebi', idRresht, '0.');
        return;
    }

    if (isNaN(idKontrolliDebi) || idKontrolliDebi == "") {
        vlDebi = 0;
        grida.setTekstQelize('txtVlmondebi', idRresht, parseFloat(vlDebi));
    }
    else vlDebi = idKontrolliDebi;

    var idMonedheGride = grida.getVlereQelize('txtMonedha', idRresht);
    var nrLlogari = grida.getTekstQelize('txtNrllogarie', idRresht);
    var llogaria = memoryLlog.GetAll().find(function (x) { return x.NrLlogari == nrLlogari })
    if (!azhornim || (llogaria && llogaria.IdMonedha == idMonedheGride)) {
        grida.setTekstQelize('txtVldebi', idRresht, parseFloat(vlDebi) / parseFloat(idKontrolliKursi));
        grida.setTekstQelize('txtVlkredi', idRresht, (parseFloat(idKontrolliKredi) / parseFloat(idKontrolliKursi)));
    }
    updateTotalet();
}

function textChangedKrediMonNew() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    ASPxMenu1.GetItemByName('QendraKosto').SetVisible(false);
    $("#div1").hide();
    $("#divQk").hide();
    var monedha = $('#txtMonedha' + idRresht + ' option:selected').text();
    for (var i = 0; i < arrFormati.length; i++) {
        var arrKonfig = new Array();
        arrKonfig = arrFormati[i].split(':');
        if (monedha == arrKonfig[0]) {
            formati = arrKonfig[1] - 1;
        }
    }
    var idKontrolliKursi = grida.getTekstQelize('txtKursi', idRresht);
    var idKontrolliDebi = grida.getTekstQelize('txtVlmondebi', idRresht);
    var idKontrolliKredi = grida.getTekstQelize('txtVlmonkredi', idRresht);
    var vlKredi;
    if (idKontrolliKredi == "-") {
        grida.setTekstQelize('txtVlmonkredi', idRresht, "-");
        return;
    }
    if (idKontrolliKredi == ".") {
        grida.setTekstQelize('txtVlmonkredi', idRresht, '0.');
        return;
    }
    if (isNaN(idKontrolliKredi) || idKontrolliKredi == "") {
        vlKredi = 0;
        grida.setTekstQelize('txtVlmonkredi', idRresht, parseFloat(vlKredi));
    }
    else vlKredi = idKontrolliKredi;

    var idMonedheGride = grida.getVlereQelize('txtMonedha', idRresht);
    var nrLlogari = grida.getTekstQelize('txtNrllogarie', idRresht);
    var llogaria = memoryLlog.GetAll().find(function (x) { return x.NrLlogari == nrLlogari })
    if (!azhornim || (llogaria && llogaria.IdMonedha == idMonedheGride)) {
        grida.setTekstQelize('txtVldebi', idRresht, parseFloat(idKontrolliDebi) / parseFloat(idKontrolliKursi));
        grida.setTekstQelize('txtVlkredi', idRresht, (parseFloat(vlKredi) / parseFloat(idKontrolliKursi)));
    }
    updateTotalet();
}

function setSelectionRange(input, selectionStart, selectionEnd) {
    if (input.setSelectionRange) {
        input.focus();
        input.setSelectionRange(selectionStart, selectionStart);
    }
    else if (input.createTextRange) {
        var range = input.createTextRange();
        range.collapse(true);
        range.moveEnd('character', selectionEnd);
        range.moveStart('character', selectionStart);
        range.select();
    }
}

function textChangedKrediNew() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    ASPxMenu1.GetItemByName('QendraKosto').SetVisible(false);
    $("#div1").hide();
    $("#divQk").hide();
    var monedha = $('#txtMonedha' + idRresht + ' option:selected').text();

    var idKontrolliKursi = grida.getTekstQelize('txtKursi', idRresht);
    var idKontrolliDebi = grida.getTekstQelize('txtVldebi', idRresht);
    var idKontrolliKredi = grida.getTekstQelize('txtVlkredi', idRresht);
    var vlKredi;
    if (idKontrolliKredi == "-") {
        grida.setTekstQelize('txtVlkredi', idRresht, "-");
        return;
    }
    if (idKontrolliKredi == ".") {
        grida.setTekstQelize('txtVlkredi', idRresht, '0.');
        return;
    }
    if (isNaN(idKontrolliKredi) || idKontrolliKredi == "") {
        vlKredi = 0;
        grida.setTekstQelize('txtVlkredi', idRresht, parseFloat(vlKredi));
    }
    else vlKredi = idKontrolliKredi;
    grida.setTekstQelize('txtVlmondebi', idRresht, parseFloat(idKontrolliDebi) * parseFloat(idKontrolliKursi));
    grida.setTekstQelize('txtVlmonkredi', idRresht, (parseFloat(vlKredi) * parseFloat(idKontrolliKursi)));


    updateTotalet();
}

function gjejIdmonedhe(kodi) {
    var tempArray = new Array();
    for (var i = 0; i < arrayMeMonedha.length; i++) {
        tempArray = arrayMeMonedha[i].split(";");
        if (tempArray[1] == kodi) {
            return tempArray[0];
        }
    }
    return -1;
}

function gjejKodmonedhe(id) {
    var tempArray = new Array();
    for (var i = 0; i < arrayMeMonedha.length; i++) {
        tempArray = arrayMeMonedha[i].split(";");
        if (tempArray[0] == id) {
            return tempArray[1];
        }
    }
    return id;
}

function ValidoDateDokumenti() {
    var vleraLabel = lblPeriudhaAktuale.GetText();

    var periudha = vleraLabel.split("-");
    var dataDok = Data_DateEdit.GetText();

    periudha1 = periudha[0].split("/");
    periudha2 = periudha[1].split("/");
    if (dataDok == "") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgVendosniDateEDokumentit"));
        return false;
    }

    else {
        dtDokumentit = dataDok.split("/");

        if (periudha1[2] != dtDokumentit[2])
            return false;
        else {
            if ((dtDokumentit[1] < periudha1[1] || dtDokumentit[1] > periudha2[1]))
                return false;
        }
        return true;
    }
}

function merrFormatNumrash() {
    var hfFormati = $("input[id$='hfFormatNr']")[0];
    var formati;
    arrFormati = hfFormati.value.split("||");
}

function mbushGrideNgaHiddenFieldet() {
    var grida = $("#rowed5");
    var idRresht = grida.getLastSel2();
    if ($("input[id$='HfColTrup']").val() == "")
        return;
    if ($('#hfShtimModifikim').val() == "shtim") {
        return;
    }
    var colTrup = JSON.parse($("input[id$='HfColTrup']").val());
    var colLlog = JSON.parse($("input[id$='HfColLlog']").val());
    var colMon = JSON.parse($("input[id$='HfColMon']").val());
    grida.setLastSel2(1);
    idRresht = 1;
    grida.jqGrid('clearGridData');
    var nrLlogarie, emertimLllogarie, pershkrim, monedha, kursi, vlDebi, vlKredi, vlMondebi, vlMonkredi;

    for (var i = 0; i < colLlog.length; i++) {
        if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides - 1] == 'True' || lidhur)
            be = myJQGrid.myValueButtonFshi(true, idRresht, "#rowed5");
        else
            be = myJQGrid.myValueButtonFshi(false, idRresht, "#rowed5");

        nrLlogarie = colLlog[i].NrLlogari;

        memoryLlog.Set(colLlog[i]);
        emertimLllogarie = colLlog[i].EmerLlogari1;
        pershkrim = colTrup[i].PershkrimTrupiFleteKontabel;
        monedha = colMon[i].KodiMonedha;
        kursi = colTrup[i].Kursi;
        vlDebi = colTrup[i].VleftaDebiTrupiFleteKontabel;
        vlKredi = colTrup[i].VleftaKrediTrupiFleteKontabel;
        vlMondebi = colTrup[i].VleftaDebiMonBazeTrupiFleteKontabel;
        vlMonkredi = colTrup[i].VleftaKrediMonBazeTrupiFleteKontabel;
        var formatNumri = gjejFormatSipasMonedhes(colMon[i].IdMonedha);
        var formatKursi = gjejFormatKursiSipasMonedhes(colMon[i].IdMonedha);
        grida.setShifraPasPresjes('txtVlkredi', formatNumri.ShifraPasPresjesVlefta, idRresht);
        grida.setShifraPasPresjes('txtVldebi', formatNumri.ShifraPasPresjesVlefta, idRresht);
        grida.setShifraPasPresjes('txtVlmonkredi', formatNumri.ShifraPasPresjesVlefta, idRresht);
        grida.setShifraPasPresjes('txtVlmondebi', formatNumri.ShifraPasPresjesVlefta, idRresht);
        grida.setShifraPasPresjes('txtKursi', formatKursi, idRresht);
        var datarow = { txtNrRendor: i + 1, txtNrllogarie: nrLlogarie, txtEmertimi: emertimLllogarie, txtPershkrimi: pershkrim, txtMonedha: monedha, txtFshi: be };
        var su = grida.jqGrid('addRowData', parseInt(idRresht), datarow);
        grida.setTekstQelize('txtKursi', idRresht, kursi);
        grida.setTekstQelize('txtVldebi', idRresht, vlDebi);
        grida.setTekstQelize('txtVlkredi', idRresht, vlKredi);
        grida.setTekstQelize('txtVlmondebi', idRresht, vlMondebi);
        grida.setTekstQelize('txtVlmonkredi', idRresht, vlMonkredi);
        idRresht = idRresht + 1;
        grida.setLastSel2(idRresht);
    }
}

var identifikuesPerPopupDokumentat = "Shto_FleteKontabel.aspx";

var KPF;
var constanteParashtese = 'ASPxRoundPanel1_ASPxCallbackPanel1_gvFleteKontabelTrupi_cell';
var constantePrapashtese = '_4_';
var arrLlogari = new Array();
var arrLlogariEmer = new Array();
var arrPershkrimi = new Array();
var arrMonedha = new Array();
var arrKursi = new Array();
var arrDebi = new Array();
var arrKredi = new Array();
var arrSkemaKontabel = new Array();
var arrDebiMon = new Array();
var arrKrediMon = new Array();
var counter1 = 0;
var counter2 = 0;
var counter3 = 0;
var counter5 = 0;
var counter6 = 0;
var counter7 = 0;
var counter8 = 0;
var counter9 = 0;
var indeksi = -1;
var editorLlogaria;
var editorLlogEmer;
var editorPershkrimi;
var editorMonedha;
var editorKursi;
var editorDebi;
var editorKredi;
var editorDebiMon;
var editorKrediMon;
var indexCounter;
var indeksPerEmerLlogarie;

var identikuesPerPopupLlogari = "";

function checkText(s, e) {
    $("input[id$='hfRuajFiltra']").val('po');
    myMenu.checkText(s, e);

}

var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    if (s.FindItemByText(s.GetInputElement().value) != null) {
        btnRuaj.SetEnabled(false); btnFshi.SetEnabled(true);
        callBackPanel.PerformCallback("filtra");
    }
    else if (s.GetText() == '') {
        btnRuaj.SetEnabled(false);
        btnFshi.SetEnabled(false);
    }
}
function LlogaritAzhornim() {
    callBackPanel.PerformCallback("azhornim");
}
function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function ruajFiltra(s, e) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    grida.saveRow(idRresht, false, 'clientArray');
    merrTeDhena(s, e);
}

/*
Function: ButtonClickKerko
    
Hap lupen e dokumentave.
*/
function ButtonClickKerko(listUrl) {
    var queryString = {
        veprimi: 'FleteKontabel',
        listUrl: listUrl
    };
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhDokumentin"), 'LupaDokumenta.aspx?' + Utils.KonvertoObjectQueryString(queryString), 950, 560);
}

function Init() {
    // PatchJQuery.myPatchJQuery();
    if (typeof (isPostBack) == "undefined") {
        document.getElementById("kokeKonfigurimi").innerHTML = hfState.Get("MenuKokeDokumenti");
        document.getElementById("trupKonfigurimi").innerHTML = hfState.Get("MenuTrupDokumenti");
        document.getElementById("fundKonfigurimi").innerHTML = hfState.Get("MenuFundDokumenti");
        var hf = $("input[id$='hfKonffillestar']");
        if ($("input[id$='hfShtimModifikim']").val() == 'klonim')
            $("input[id$='hfId']").val('0');
        $('#kokeKonfigurimi').text(hfState.Get("MenuKokeDokumenti") + ': ' + hf.val().split(';')[1]);
        // lblKonfigurimi.SetText(hf.val().split(';')[1]);
        callWebserviceKonfigurimi("116", konfigurimi_ComboBox.GetText());
        changeName();
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(EndRequestHandler);
        myMesazh.shtoHandler();
    }
}

function Autorizime_Click() {
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("MsgBlerjeShitjeAutorizime"), 'LupaAutorizim.aspx', 600, 600);
}

function changeName() {
    myFaqeCelje.shtoHandlerSession();
    try {
        if (Utils.getUrlVar('id') == undefined)
            window.parent.callWebServiceKtheInfoLart('Shto_FleteKontabel.aspx', 0);
        else
            window.parent.callWebServiceKtheInfoLart('Shto_FleteKontabel.aspx', Utils.getUrlVar('id'));
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
var formatNumriZgjedhur;

function SucceededCallbackKonfig(result) {
    var hf = $("input[id$='hfShtimModifikim']");
    vendosDateDefault(hf.val());
    var hfLidhur = $("input[id$='hfLidhur']");
    var arrTabela = ['tblFleteKontabel', "tblTotale", "tblQendra"];
    var arrPrind = ["dvFillim", "dvFundi", "div1"];
    colKontrollet = result.colKontroll;    //[0];
    colAtrTrupi = result.colAtrTrupi;    //[1];
    var vlerat = myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, undefined, hf, '', arrTabela, undefined, undefined, hfLidhur, arrPrind);
    colKontrollet = result.colKontroll; //[0];
    colAtrTrupi = result.colAtrTrupi;   //[1];
    colGrida = result.colGrida;    //[2];
    colKushte = result.colKushte;   //[3];
    colAlterKusht = result.colAlterKusht;   //[4];
    var kodniveli = result.kodniveli;   //[5];
    var konfLlojRreshti = result.konfLlojRreshti;  //[6];
    formatNumriZgjedhur = result.formatNumri;   //[7];
    $("input[id$='HfGridCol']").val(JSON.stringify(colGrida));
    azhornim = false;
    $('#hfAzhornim').val(false);
    for (j = 0; j < colKushte.length; j++) {
        if (colKushte[j].Kodi == 'LLFK') {
            if (colAlterKusht[j].Alternativa == "Azhornim") {
                azhornim = true;
                $('#hfAzhornim').val(true);
            }
            continue;
        }
        if (colKushte[j].Kodi == 'LLK') {
            llojkursi = colAlterKusht[j].Alternativa.substring(colAlterKusht[j].Alternativa.length - 1);
            continue;
        }
    }

    // mbushim hidden fields me vleren e konfigurimit te lupes per secilin kontroll qe ka lupe
    var hfDok = $("input[id$='hfLupaDokumenti']");
    var hfNrLlog = $("input[id$='hfLupaLlogaria']");
    var hfSkema = $("input[id$='hfLupaSkemaFK']");
    $("#divgride1").show();
    $("#dvFillim").show();
    $("#dvFundi").show();
    if (ASPxMenu1.GetItemByName('QendraKosto').GetVisible()) {
        $("#div1").show();
        $("#divQk").show();
        regjQK.initGridQK({ ngaThirret: 'fk', formatNr: formatNumriZgjedhur, konfigurimGride: null, PershkrimiKokes: "" }, "modifikim", { idKoka: 0, dteDtDok: Data_DateEdit.date.toDateString(), dteDtRegj: DateRegjistrimi_DateEdit.date.toDateString(), idKonfig: $('#hfIdKonfigAmbjenteQK').val() }, { idDokGjenerues: $('#hfId').val(), idKonfigGjenerues: konfigurimi_ComboBox.GetValue(), llogariFK: null });
    }
    if (hf.val() == "shtim" || hf.val() === "klonim") {
        hfNrAuto.Clear();
        hfNrAutoShitje.Clear();
        vendosNrAutomatik(colAtrTrupi, colKontrollet);
    }
    for (var i = 0; i < colKontrollet.length; i++) {
        //id e konfigurimit te lupes vendoset ne hidden field
        //vlera e saj do t'i kalohet si query string hapjes se popUp-it perkates
        if (colKontrollet[i].KodKontrolli == "lblKodiSkemaKontabel") {
            hfDok.val(colAtrTrupi[i].IdKonfigAmbjenteLupa.toString()); //merret id e konfigurimit te lupes per lupen ne fjale
            continue;
        }
        if (colKontrollet[i].KodKontrolli == "btneSkemaFK") {
            hfSkema.val(colAtrTrupi[i].IdKonfigAmbjenteLupa.toString());
            continue;
        }
    }

    formoArrayKolGrides();
    var grida = $('#rowed5');
    grida.setLastSel2(-1);
    grida.GridUnload("rowed5");
    ruajFormatetNeGride(grida, formatNumriZgjedhur);
    inicializoGride();
    mbushGrideNgaHiddenFieldet();
    Utils.konfiguroAccorditionPasKonfigDokumenti(varKonfig.identifikuesPerLocalStorageKey);
}

function ndryshoKonfigurimin() {
    var cmbModKontroll = konfigurimi_ComboBox.GetSelectedItem();
    var pershkKonfigAmb = cmbModKontroll.GetColumnText("PershkrimKonfigAmbjente");
    if (pershkKonfigAmb != undefined) {        
        $('#kokeKonfigurimi').text(hfState.Get("MenuKokeDokumenti") + ': ' + pershkKonfigAmb);
    }
    callWebserviceKonfigurimi("116", konfigurimi_ComboBox.GetText());
}

var trupibosh;
function merrTeDhena(s, e) {//merren te dhenat qe ka grida
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    grida.jqGrid('saveRow', idRresht, false, 'clientArray');
    trupibosh = true;
    var idTe = grida.jqGrid('getDataIDs');
    for (var i = 0; i < idTe.length; i++) {
        editorLlogEmer = grida.getTekstQelize('txtEmertimi', idTe[i]);
        editorPershkrimi = grida.getTekstQelize('txtPershkrimi', idTe[i]);
        editorMonedha = grida.getTekstQelize('txtMonedha', idTe[i]);
        editorKursi = grida.getTekstQelize('txtKursi', idTe[i]);
        editorKredi = grida.getTekstQelize('txtVlkredi', idTe[i]);
        editorDebi = grida.getTekstQelize('txtVldebi', idTe[i]);
        editorDebiMon = grida.getTekstQelize('txtVlmondebi', idTe[i]);
        editorKrediMon = grida.getTekstQelize('txtVlmonkredi', idTe[i]);
        editorLlogaria = grida.getTekstQelize('txtNrllogarie', idTe[i]);
        if (editorLlogaria != "") trupibosh = false;

        if (editorDebiMon == 0 && editorKrediMon == 0 && editorLlogaria != '') {
            e.processOnServer = false; click = false;
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgRreshtaTePavlefshemNeGride"));
        }

    }
    var rreshtaTeGrides = grida.getTeDhenaRreshti();
    $('#gridDataObject').val(JSON.stringify(rreshtaTeGrides));
    unformatoFushaDevi();
}

function ruajKolonatEGrides(grida) {
    var idGjuha = hfState.Get('idGjuha');
    var idNdermarrje = hfState.Get('idNdermarrje');
    var idPerdoruesi = hfState.Get('idPerdoruesi');
    var idViti = hfState.Get('idViti');
    var idGride = colGrida[0].IdKoka;
    myJQGrid.ruajKolonatEGrides(grida, idGride, idGjuha, idNdermarrje, idViti, idPerdoruesi)
}



//pastrimi i array te perdorura
function pastro() {
    arrLlogari = new Array();
    arrLlogariEmer = new Array();
    arrPershkrimi = new Array();
    arrMonedha = new Array();
    arrKursi = new Array();
    arrDebi = new Array();
    arrKredi = new Array();
    arrDebiMon = new Array();
    arrKrediMon = new Array();
    var hidField1 = $("input[id$='hfNrLlogaria']")[0];  //document.getElementById("hfNrLlogaria"); //shton vleren tek hidden field e cila perdoret me vone nga kodi
    var hidfield2 = $("input[id$='hfEmerLlogaria']")[0];  // document.getElementById("hfEmerLlogaria");
    var hidField3 = $("input[id$='hfPershkrimi']")[0];  // document.getElementById("hfPershkrimi");
    var hidField4 = $("input[id$='hfDebi']")[0];  // document.getElementById("hfDebi");
    var hidField5 = $("input[id$='hfKredi']")[0];  //document.getElementById("hfKredi");

    var hidField7 = $("input[id$='hfMonedha']")[0];  //document.getElementById("hfMonedha");
    var hidfield8 = $("input[id$='hfKursi']")[0];  // document.getElementById("hfKursi");
    var hidfield9 = $("input[id$='hfSkemaKontabel']")[0];  // document.getElementById("hfSkemaKontabel");
    var hidfield10 = $("input[id$='hfDebiMon']")[0];  //document.getElementById("hfDebiMon");
    var hidfield11 = $("input[id$='hfKrediMon']")[0];  // document.getElementById("hfKrediMon");
    hidField1.value = '';  //document.getElementById("hfNrLlogaria"); //shton vleren tek hidden field e cila perdoret me vone nga kodi
    hidfield2.value = '';  // document.getElementById("hfEmerLlogaria");
    hidField3.value = '';  // document.getElementById("hfPershkrimi");
    hidField4.value = '';  // document.getElementById("hfDebi");
    hidField5.value = '';  //document.getElementById("hfKredi");

    hidField7.value = '';  //document.getElementById("hfMonedha");
    hidfield8.value = '';  // document.getElementById("hfKursi");
    hidfield9.value = '';  // document.getElementById("hfSkemaKontabel");
    hidfield10.value = '';  //document.getElementById("hfDebiMon");
    hidfield11.value = '';  // document.getElementById("hfKrediMon");
    $("input[id$='HfColTrup']").val('');
    $("input[id$='HfColLlog']").val('');
    $("input[id$='HfColMon']").val('');
    $('#hfIdKonfigAmbjenteQK').val('0')
    pastroTextBoxet();
}

function pastroTextBoxet() {
    txtNrDokumenti.SetText('');
    txtPershkrimi.SetText('');
    lblKodiSkemaKontabel.SetText('');
    txtNrGrupKontabilizimi.SetValue(null);
    btneSkemaFK.SetValue(null);
    btnPeriudha.SetText('');
    $("input[id$='hfStatusRuajtje']")[0].value = "false";
    txtDebiDiferenca.SetText('0.00');
    txtKrediDiferenca.SetText('0.00');
    txtDebiMonedhaBaze.SetText('0.00');
    txtKrediMonedhaBaze.SetText('0.00');
    txtNrReference.SetText($("input[id$='hfNrRef']")[0].value);
    cmbQendraKosto.SetValue(null);
    regjQK.clearGrid();
}

function vendosDateDefault(shtimMod) {
    switch (shtimMod) {
        case "klonim":
            var dataSot = Utils.zeroOren(new Date());
            DateRegjistrimi_DateEdit.SetDate(dataSot);
            break;
        case "shtim":
            try {
                if (shtimMod == "shtim") {
                    var hfPeriudheObj = window.parent.lexoHfPeriudhe();
                    Data_DateEdit.SetDate(Utils.ktheDateDefault(hfPeriudheObj));
                }
            } catch (ee) {
            }
            break;
        default:
            break;
    }
}

function updateTotalet() {
    var shumadebi = 0;
    var shumakredi = 0;
    var shumadebimonedha = 0;
    var shumakredimonedha = 0;
    var grida = $('#rowed5');
    var idTe = grida.jqGrid('getDataIDs');

    for (var i = 0; i < idTe.length; i++) {
        if (grida.getTekstQelize('txtNrllogarie', idTe[i]) != "") {
            if (grida.getTekstQelize('txtVldebi', idTe[i]) !== "") {
                shumadebi = mbledhje(parseFloat(shumadebi), grida.getTekstQelize('txtVldebi', idTe[i]));
                shumakredi = mbledhje(parseFloat(shumakredi), grida.getTekstQelize('txtVlkredi', idTe[i]));
                shumadebimonedha = mbledhje(parseFloat(shumadebimonedha), grida.getTekstQelize('txtVlmondebi', idTe[i]));
                shumakredimonedha = mbledhje(parseFloat(shumakredimonedha), grida.getTekstQelize('txtVlmonkredi', idTe[i]));
            }

        }

    }
    txtDebiMonedhaBaze.SetText(shumadebimonedha);
    txtKrediMonedhaBaze.SetText(shumakredimonedha);
    var diferenca = mbledhje(shumadebimonedha, -shumakredimonedha);
    if (diferenca > 0) {
        txtDebiDiferenca.SetText(diferenca);
        txtKrediDiferenca.SetText(0);
    }
    else {
        txtKrediDiferenca.SetText(-diferenca);
        txtDebiDiferenca.SetText(0);
    }
}
function Totalet() {
    var shumadebi = 0;
    var shumakredi = 0;
    var shumadebimonedha = 0;
    var shumakredimonedha = 0;
    var grida = $("#rowed5");

    var idTe = grida.jqGrid('getDataIDs');
    for (var i = 0; i < idTe.length; i++) {
        if (grida.getTekstQelize('txtNrllogarie', idTe[i]) != "") {
            //  if (grida.getTekstQelize('txtVldebi', idTe[i]) != "") {
            shumadebi = mbledhje(parseFloat(shumadebi), grida.getTekstQelize('txtVldebi', idTe[i]));
            shumakredi = mbledhje(parseFloat(shumakredi), grida.getTekstQelize('txtVlkredi', idTe[i]));
            shumadebimonedha = mbledhje(parseFloat(shumadebimonedha), grida.getTekstQelize('txtVlmondebi', idTe[i]));
            shumakredimonedha = mbledhje(parseFloat(shumakredimonedha), grida.getTekstQelize('txtVlmonkredi', idTe[i]));

            // }
        }

    }

    txtDebiMonedhaBaze.SetText(shumadebimonedha);
    txtKrediMonedhaBaze.SetText(shumakredimonedha);
    var diferenca = mbledhje(shumadebimonedha, -shumakredimonedha);
    if (diferenca > 0) {
        txtDebiDiferenca.SetText(diferenca);
        txtKrediDiferenca.SetText(0);
    }
    else {
        txtKrediDiferenca.SetText(-diferenca);
        txtDebiDiferenca.SetText(0);
    }

}

// shfaq popupin e grupkontabilizimit
function Shfaq() {
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgGrupKontabilizimi"), 'LupaGrupKontabilizim.aspx', 600, 600);
}

//kur zgjidhet nje element nga autosuggesti qe te vendoset edhe pershkrimi i llogarise
function OnContactSelected(source, eventArgs) {
    var gjatesiaParashteses = constanteParashtese.length;
    var tempStr = source.get_element().id.substring(gjatesiaParashteses, source.get_element().id.length);
    var poziocionVize = tempStr.indexOf("_");
    var indexRreshti = tempStr.substring(0, poziocionVize);
    editorLlogEmer = Utils.ktheKontroll('txtEmerLlogari' + indexRreshti);
    editorLlogEmer.SetText(eventArgs.get_value());
}

//mbush gjithe pershkrimet e grides me textin e txtPershkrimit
function pershkrimi() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var per = txtPershkrimi.GetText();
    grida.saveRow(idRresht, false, 'clientArray');
    //vendoset magazina e zgjedhur te koka ne rreshtat e tjere te grides
    var rreshtaTeGrides = grida.getDataIDs();
    for (i = 0; i < rreshtaTeGrides.length; i++) {
        //  if (i+1 != lastsel2) { 
        if (rreshtaTeGrides[i].txtPershkrimi == '')
            grida.setCell(rreshtaTeGrides[i], 'txtPershkrimi', per, 'clientArray', '');
        //   }
    }
    grida.editRow(idRresht);
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ? evt.keyCode : evt.charCode;
}

//duhet per te mbushur trupin e fletes kontabel me llogarite e caktuara tek 
//skema kontabel e zgjedhur
function lostFocusSkemaKontabel(vlera) {
    if (vlera != '') {

        callBackPanel.PerformCallback('skeme,' + vlera);
    }
}

function lostFocusPeriudha(vlera) {
}

//shfaq popupin e llogarive kur shtypet enter brenda fushes se llogarive
function KeyPress(editor, key) {
    if (event.keyCode == 13) {
        editorLlogaria = document.getElementById(editor);;
        editorLlogEmer = Utils.ktheKontroll('txtEmerLlogari' + key);
        myButtonClickLupa.LupaUniversal_Click(hfState.Get("popupAdministrimiUniversal"), 'LupaLlogaria.aspx?&array=' + arrLlogari, 800, 600);
        event.returnValue = false;
        event.cancel = true;
    }
}

//kontrollon nese jemi ne rreshtin e fundit
////        function LostFocus(editor, editorEmer, key) {
////            indeksi = indexCounter;
////            if (key == gvFleteKontabelTrupi.cpNoRows - 1) {//shtohet rresht i ri nqs jemi ne rreshtin e fundit
////                indexCounter = indexCounter + 1;
////                merrTeDhena();
////                gvFleteKontabelTrupi.PerformCallback();
////            }
////        }

function LostFocusDebi(editor) {
    if (editor.GetText() == '') {
        editor.SetText('0.00');
    }
    else if (isNaN(editor.GetText())) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgVleraEDebiseDuhetTeJeteNumerike"));
        editor.SetFocus(true);
        editor.SelectAll();
    }
    else if (editor.GetText() == '') {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgJepniVlerenEDebise"));
        editor.SetFocus(true);
        editor.SelectAll();
    }
}

function LostFocusKursi(editor, key) {
    if (editor.GetText() == '') {
        editor.SetText('1.00');
    }
    else if (isNaN(editor.GetText())) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgVleraEKursitDuhetTeJeteNumerike"));
        editor.SetFocus(true);
        editor.SelectAll();
    }
}


function LostFocusKredi(editor) {
    if (editor.GetText() == '') {
        editor.SetText('0.00');
    }
    else if (isNaN(editor.GetText())) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgVleraEKrediseDuhetTeJeteNumerike"));
        editor.SetFocus(true);
        editor.SelectAll();
    }
    else if (editor.GetText() == '') {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgJepniVlerenEKredise"));
        editor.SetFocus(true);
        editor.SelectAll();
    }
}

function LostFocusNrLlogarie(editor, key) {
    indeksPerEmerLlogarie = key;
    var value = document.getElementById(editor).value
    if (value != '')
        callWebserviceEmerLlogarie(value);
    indeksi = indexCounter;
}


//Ndryshimi i periudhes kontabel nga popup-i ndryshon daten default te dok,
function valueChangedPeriudha() {

    var vleraLabel = lblPeriudhaAktuale.GetText();
    var periudha = vleraLabel.split("-");
    var dataDok = new Date();
    dataDok = formatDate(dataDok, "dd/MM/yyyy");

    periudha1 = periudha[0].split("/");
    periudha2 = periudha[1].split("/");

    dtDokumentit = dataDok.split("/");

    if (periudha1[2] != dtDokumentit[2])

        Data_DateEdit.SetText(periudha[0]);
    else {
        if ((dtDokumentit[1] < periudha1[1] || dtDokumentit[1] > periudha2[1]))
            Data_DateEdit.SetText(periudha[0]);
        else
            Data_DateEdit.SetText(dataDok);
    }
}

function SucceededCallbackPeriudha(result) {

    Data_DateEdit.SetText(formatDate(result, "dd/MM/yyyy"));
}


function callWebserviceEmerLlogarie(name) {
    try {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheEmerLlogarie"),
            data: JSON.stringify({ prefixText: name })
        }).done(SucceededCallback);
    } catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
    }
}

// This is the callback function that
// processes the Web Service return value.
function SucceededCallback(result) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var idKontrolliLlogarie = "#txtNrllogarie" + idRresht;
    if (result != '') {
        var idKontrolli = "#txtEmertimi" + idRresht;
        jQuery(idKontrolli)[0].value = result;
    }
    else {
        jQuery(idKontrolliLlogarie)[0].value = '';
        if (jQuery(idKontrolliLlogarie)[0].isDisabled == false)
            jQuery(idKontrolliLlogarie)[0].focus();
    }
}


/*
Function: ButtonClickKodi
    
Hap lupen e llogarive sipas zgjedhjes qe eshte bere te kategoria.
*/

function ButtonClickKodi() {
    //marr vleren e IdKonfigurim te lupes se detajimeve
    var hfLlog = $("input[id$='hfGridaLlogaria']")[0];
    var queryStr = hfLlog.value;
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("popupAdministrimiUniversal"), 'LupaLlogaria.aspx?&array=' + arrLlogari + '&idKonfigAmbjente=' + queryStr, 750, 600);
}
//kontrollon nese jemi ne rreshtin e fundit
function TextChangedNrLlogari(editor, editorEmer, key) {
}

////        function GotFocusNrLlogari(editor, key) {
////            if (key == gvFleteKontabelTrupi.cpNoRows - 1) {//shton rresht kur jemi ne rreshtin e fundit
////                indexCounter = indexCounter + 1;
////                merrTeDhena();
////                gvFleteKontabelTrupi.PerformCallback('shto,' + 'NrLlogari');
////            }
////        }

////        //kontrollon nese jemi ne rreshtin e fundit
////        function TextChangedPershkrimi(editor, key) {
////            indeksi = indexCounter;
////            if (key == gvFleteKontabelTrupi.cpNoRows - 1) {//shton rresht kur jemi ne rreshtin e fundit
////                indexCounter = indexCounter + 1;
////                merrTeDhena();
////                gvFleteKontabelTrupi.PerformCallback('shto,' + 'PershkrimTrupiFleteKontabel');
////            }
////        }

function GotFocusDebi(editor, key) {
    var nrLlogarieClientID = getClientID(key);
    editorLlogaria = $("input[id$=" + nrLlogarieClientID + "]")[0];
    if (editorLlogaria.value == '') {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgJuLutemPlotesoniLlogarine"));
    }
    else {
        editorMonedha = Utils.ktheKontroll('txtMonedha' + key);
        if (editorMonedha.GetText() == '') {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgJuLutemPlotesoniMonedhen"));
            editorMonedha.SetFocus(true);
        }
        else {
            editor.SetSelection(0, editor.GetText().length, true);
        }
    }
}

function GotFocusKredi(editor, key) {
    var nrLlogarieClientID = getClientID(key);
    editorLlogaria = $("input[id$=" + nrLlogarieClientID + "]")[0];
    if (editorLlogaria.value == '') {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgJuLutemPlotesoniLlogarine"));
        editorLlogaria.SetFocus(true);
    }
    else {
        editorMonedha = Utils.ktheKontroll('txtMonedha' + key);
        if (editorMonedha.GetText() == '') {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgJuLutemPlotesoniMonedhen"));
            editorMonedha.SetFocus(true);
        }
    }
}



function getClientID(key) {
    return 'txtNrllogarie' + key;
}


//kontrollon nese jemi ne rreshtin e fundit
var MONTH_NAMES = new Array('January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December', 'Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec');
var DAY_NAMES = new Array('Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat');

function LZ(x) { return (x < 0 || x > 9 ? "" : "0") + x }

function formatDate(date, format) {
    format = format + "";
    var result = "";
    var i_format = 0;
    var c = "";
    var token = "";
    var y = date.getYear() + "";
    var M = date.getMonth() + 1;
    var d = date.getDate();
    var E = date.getDay();
    var H = date.getHours();
    var m = date.getMinutes();
    var s = date.getSeconds();
    var yyyy, yy, MMM, MM, dd, hh, h, mm, ss, ampm, HH, H, KK, K, kk, k;
    // Convert real date parts into formatted versions
    var value = new Object();
    if (y.length < 4) { y = "" + (y - 0 + 1900); }
    value["y"] = "" + y;
    value["yyyy"] = y;
    value["yy"] = y.substring(2, 4);
    value["M"] = M;
    value["MM"] = LZ(M);
    value["MMM"] = MONTH_NAMES[M - 1];
    value["NNN"] = MONTH_NAMES[M + 11];
    value["d"] = d;
    value["dd"] = LZ(d);
    value["E"] = DAY_NAMES[E + 7];
    value["EE"] = DAY_NAMES[E];
    value["H"] = H;
    value["HH"] = LZ(H);
    if (H == 0) { value["h"] = 12; }
    else if (H > 12) { value["h"] = H - 12; }
    else { value["h"] = H; }
    value["hh"] = LZ(value["h"]);
    if (H > 11) { value["K"] = H - 12; } else { value["K"] = H; }
    value["k"] = H + 1;
    value["KK"] = LZ(value["K"]);
    value["kk"] = LZ(value["k"]);
    if (H > 11) { value["a"] = "PM"; }
    else { value["a"] = "AM"; }
    value["m"] = m;
    value["mm"] = LZ(m);
    value["s"] = s;
    value["ss"] = LZ(s);
    while (i_format < format.length) {
        c = format.charAt(i_format);
        token = "";
        while ((format.charAt(i_format) == c) && (i_format < format.length)) {
            //token += format.charAt(i_format++);
            token = token + format.charAt(i_format++);
        }
        if (value[token] != null) { result = result + value[token]; }
        else { result = result + token; }
    }
    return result;
}

var indeksiMon;

function kurset() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    counter3 = 0;
    grida.saveRow(idRresht, false, 'clientArray');
    grida.setLastSel2(0);
    var rreshtaTeGrides = grida.getRowData();
    for (var i = 0; i < rreshtaTeGrides.length; i++) {
        if (!arrMonedha.ekzistonElementi(rreshtaTeGrides[i].txtMonedha)) {
            arrMonedha[counter3] = rreshtaTeGrides[i].txtMonedha;
            counter3 = counter3 + 1;
        }
    }
    callWebservice2(arrMonedha, formatDate(Data_DateEdit.GetValue(), 'MM/dd/yyyy'));
    vendosNrAutomatik(colAtrTrupi, colKontrollet);
}

function vendosNrAutomatik(colAtrTrupi, colKontrollet) {//po

    myNrAuto.vendosNrAutomatik(colAtrTrupi, colKontrollet, Data_DateEdit.GetDate());
}

function callWebservice1(id, date, idrreshti, kodmonedha) {//nqs ke id lere bosh""
    try {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "merrKursiSipasMonedhesAndDatesDheRreshti"),
            data: JSON.stringify({ idMonedha: id, date: date, idrreshti: idrreshti, kodmonedha: kodmonedha, lloji: llojkursi })
        }).done(SucceededCallback1);
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
    }
}

function callWebserviceListeLlogarish() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var vlera = $('#txtNrllogarie' + idRresht).val();
    //            var tekstSelektuar = document.selection.createRange().text;
    //            var vleraShkrojtur = vlera.substr(0, vlera.length - tekstSelektuar.length) + String.fromCharCode(event.keyCode); 
    try {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheACListeLlogarish"), data: JSON.stringify({ infixText: vlera, pershk: pershk, idNdermarrje: hfState.Get("idNdermarrje"), idPerdoruesi: hfState.Get("idPerdoruesi") })
        }).done(SucceededCallbackListeLlogarish);
    } catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
    }
}

function callWebservice2(array, date) {
    try {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "merrKursetDheMonedhatSipasDates"),
            data: JSON.stringify({ prefixText: array, date: date })
        }).done(SucceededCallback2);
    } catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
    }
}

function SucceededCallback2(result) {
    var grida = jQuery("#rowed5");
    var rreshtaTeGrides = grida.getRowData();
    var tempArray = result.split("||");
    var kursi;
    var str;
    var arrayMeindekse = grida.getDataIDs();

    for (var i = 0; i < arrayMeindekse.length; i++) {
        if (rreshtaTeGrides[i].txtMonedha === "") continue;
        kursi = gjejKursPerMonedhe(rreshtaTeGrides[i].txtMonedha, tempArray);
        //  txtVlmonkredi
        grida.setCell(i + 1, "txtKursi", kursi);
        grida.setCell(i + 1, "txtVlmondebi", rreshtaTeGrides[i].txtVldebi * kursi);
        grida.setCell(i + 1, "txtVlmonkredi", rreshtaTeGrides[i].txtVlkredi * kursi);
    }
    Totalet();
}

function gjejKursPerMonedhe(kodi, arr) {
    var tempStr;
    for (var i = 0; i < arr.length; i++) {
        if (arr[i] != '') {
            tempStr = arr[i].split('|');
            if (tempStr[0] == kodi) {
                return tempStr[1];
            }
        }
    }
    return 1.00;
}

function selectFunc(event, ui, emerfushe, idLlog, kodLlog) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    ASPxMenu1.GetItemByName('QendraKosto').SetVisible(false);
    $("#div1").hide();
    $("#divQk").hide();
    //emerfushe = '#' + emerfushe;
    //$("#div1")[0].style.visibility = 'hidden'; $("#div1")[0].style.display = 'none';
    if (idLlog != undefined && kodLlog != undefined && idLlog != "" && kodLlog != "" && $(emerfushe).val() !== undefined) {
        $(emerfushe).val(kodLlog);
        //        if (!kontrolloRow(lastsel2))
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "KtheVleraLlogMeIDRow"),
            data: JSON.stringify({ idja: idLlog, rreshti: idRow })
        }).done(SucceededCallbackLlog);
        return false;
    }
    if (ui !== null && ui.item != null) {
        $(emerfushe).val(ui.item.label);
        //kontrollo();
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "KtheVleraLlogMeIDRow"),
            data: JSON.stringify({ idja: ui.item.value, rreshti: idRow })
        }).done(SucceededCallbackLlog);
    }

    return false;
}

function changeFunc(event, ui, emerKodi, index) {
    //var index = -1;
    //var idKod = arrayIdKolonaGrides[1];
    //if (emerKodi === undefined || emerKodi === null)
    //    index = myJQGrid.getIndexFromEvent(event, idKod);
    //else
    //    index = emerKodi.split(idKod)[1];
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    if (index == idRow) {
        var emerfushe = '#' + emerKodi + index;
        if (ui == null || ui.item == null) {
            var kodi = $(emerfushe).val();
            //kontrollo();
            if (typeof (kodi) != "undefined" && kodi != undefined && kodi != "") {
                $.ajax({
                    url: Utils.getServerApiUrl("Rregjistrime", "ktheVleraLlogMeKodRow"),
                    data: JSON.stringify({ kodi: kodi, rreshti: index, idNdermarrje: hfState.Get("idNdermarrje") })
                }).done(SucceededCallbackLlog);
                return;
            }
            vendosLlog([index, null]);
            return;
        }
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "KtheVleraLlogMeIDRow"),
            data: JSON.stringify({ idja: ui.item.value, rreshti: idRow })
        }).done(SucceededCallbackLlog);
        return;
    }
    var rreshti = grida.jqGrid('getRowData', index);
    if (rreshti.txtNrllogarie != '') {
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "ktheVleraLlogMeKodRow"),
            data: JSON.stringify({ kodi: rreshti.txtNrllogarie, rreshti: index, idNdermarrje: hfState.Get("idNdermarrje") })
        }).done(SucceededCallbackLlog);
        return;
    }
    vendosLlog([index, null]);
}

function SucceededCallback1(result) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    hf = $("input[id$='hfMonedhaNder']")[0];
    var editorKursi = grida.getTekstQelize('txtKursi', idRow);
    if (result[0] == idRow) {
        if (hf.value == grida.getTekstQelize("txtMonedha", idRow)) {
            editorKursi = 1.00;

            $("#txtKursi" + idRow)[0].disabled = 'disabled';
            $("#btntxtKursi" + idRow)[0].disabled = 'disabled';
            grida.setTekstQelize('txtKursi', idRow);
        }
        else {

            editorKursi = result[1];
            grida.setTekstQelize('txtKursi', idRow, result[1]);
            $("#txtKursi" + idRow)[0].disabled = false;
            $("#btntxtKursi" + idRow)[0].disabled = false;
        }
    }
    else {
        if (hf.value == grida.getTekstQelize('txtKursi', result[0])) {
            editorKursi = 1.00;
            grida.setTekstQelize('txtKursi', result[0]);
        }
        else {
            editorKursi = result[1];
            grida.setTekstQelize('txtKursi', result[0], result[1]);
        }
    }
    kursifundit = editorKursi;
    textChangedKursi();
}

function resetRreshtKorent(idRreshti) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    if (idRreshti == idRow && grida.getTekstQelize('txtNrllogarie', idRow) != undefined) {
        grida.setTekstQelize(arrayIdKolonaGrides[1], idRreshti, '');
        grida.setTekstQelize(arrayIdKolonaGrides[2], idRreshti, '');
        grida.setTekstQelize(arrayIdKolonaGrides[3], idRreshti, '');
        grida.setTekstQelize(arrayIdKolonaGrides[6], idRreshti);
        grida.setTekstQelize(arrayIdKolonaGrides[7], idRreshti);

        return;
    }
    grida.jqGrid('delRowData', idRreshti);
    return;
}
function SucceededCallbackLlog(result) {//po
    vendosLlog(result);
}
function vendosLlog(result) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    if (result != null) {
        var idRreshti = result[0];
        var llogaria = result[1];
        if (llogaria == null || llogaria == undefined || llogaria.NrLlogari < 1) {
            resetRreshtKorent(idRreshti);
            return;
        }
        memoryLlog.Set(llogaria);
        var formatNumri = gjejFormatSipasMonedhes(llogaria.IdMonedha);
        var formatKursi = gjejFormatKursiSipasMonedhes(llogaria.IdMonedha);
        grida.setShifraPasPresjes('txtVlkredi', formatNumri.ShifraPasPresjesVlefta, idRreshti);

        grida.setShifraPasPresjes('txtVldebi', formatNumri.ShifraPasPresjesVlefta, idRreshti);
        grida.setShifraPasPresjes('txtKursi', formatKursi, idRreshti);


        if (idRreshti == idRow) {
            $('#' + arrayIdKolonaGrides[1] + idRow).val(llogaria.NrLlogari);
            if (pershk == 1)
                $('#' + arrayIdKolonaGrides[2] + idRow).val(llogaria.EmerLlogari1);
            else
                $('#' + arrayIdKolonaGrides[2] + idRow).val(llogaria.EmerLlogari2);
            //$('select option', elem).attr('selected', false); //dese
            hfmonnder = $("input[id$='hfMonedhaNder']").val();
            hfidmonnder = $("input[id$='hfIdMonedhaNder']").val();
            var monedhavjeter = $("#txtMonedha" + idRow).val();
            if (azhornim)
                $("#txtMonedha" + idRow).val(hfidmonnder);//$("#txtMonedha" + lastsel2 + " option[value='" + hfmonnder + "']").attr('selected', 'selected');
            else
                $("#txtMonedha" + idRow).val(llogaria.IdMonedha);//$("#txtMonedha" + lastsel2 + " option[value='" + llogaria.IdMonedha + "']").attr('selected', 'selected');
            $("#txtMonedha" + idRow).attr('disabled', 'disabled');
            if (monedhavjeter != $("#txtMonedha" + idRow).val()) $("#txtMonedha" + idRow).change();
            return;
        }
        var rreshti = grida.jqGrid('getRowData', idRreshti);
        grida.setTekstQelize('txtNrllogarie', idRreshti, llogaria.NrLlogari);
        if (pershk == 1) grida.setTekstQelize('txtEmertimi', idRreshti, llogaria.EmerLlogari1);
        else grida.setTekstQelize('txtEmertimi', idRreshti, llogaria.EmerLlogari2);
        hfmonnder = $("input[id$='hfMonedhaNder']").val();
        hfidmonnder = $("input[id$='hfIdMonedhaNder']").val();
        var monedhavjeter = grida.getTekstQelize('txtMonedha', idRreshti);
        if (azhornim) grida.setTekstQelize('txtMonedha', idRreshti, hfmonnder);

        else grida.setTekstQelize('txtMonedha', idRreshti, llogaria.KodiMonedha);
        if (monedhavjeter != grida.getTekstQelize('txtMonedha', idRreshti))
            callWebservice1(0, formatDate(Data_DateEdit.GetValue(), 'MM/dd/yyyy'), idRreshti, grida.getTekstQelize('txtMonedha', idRreshti));

        grida.formatoQelize('txtKursi', idRreshti);


        //   $('#rowed5').jqGrid('setRowData', idRreshti, rreshti);
    }
}

function setSelectionRange(input, selectionStart, selectionEnd) {
    if (input.setSelectionRange) {
        input.focus();
        input.setSelectionRange(selectionStart, selectionStart);
    }
    else if (input.createTextRange) {
        var range = input.createTextRange();
        range.collapse(true);
        range.moveEnd('character', selectionEnd);
        range.moveStart('character', selectionStart);
        range.select();
    }
}

function SucceededCallbackListeLlogarish(result) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    myJQGrid.SucceededCallbackKodi(result, '#txtNrllogarie' + idRresht)
}


function NrDokumentiClick() {
    var hfDok = $("input[id$='hfLupaDokumenti']")[0];
    var queryStr = hfDok.value;
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhLlojinEDokumentit"), 'LupaLlojDokumenti.aspx?idKonfigAmbjente=' + queryStr, 600, 600);

}

function LlogariteClick() {
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("popupAdministrimiUniversal"), 'LupaLlogaria.aspx?veprimi=LlogariAzhornimi', 750, 600);
    identikuesPerPopupLlogari = "LlogariAzhornimi";

}

function ShfaqSkemeKontabel() {
    var hfSkemaFK = $("input[id$='hfLupaSkemaFK']")[0];
    var queryStr = hfSkemaFK.value;
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhSkemenKontabel"), 'LupaSkemaKontabel.aspx?default=0&idKonfigAmbjente=' + queryStr, 600, 600);

}

function ShfaqSkemeFleteKontabel() {
    var hfSkemaFK = $("input[id$='hfLupaSkemaFK']")[0];
    var queryStr = hfSkemaFK.value;
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhSkemenFleteKontabel"), 'LupaSkemaFk.aspx?idKonfigAmbjente=' + queryStr, 600, 600);

}

function ShfaqPeriudhen() {
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhPeriudhen"), 'LupaPeriudhaKontabel.aspx', 600, 600);
}

function ruajClick(s, e) {
    //pastro();
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    grida.saveRow(idRresht, false, 'clientArray');
    merrTeDhena(s, e);

    if (trupibosh == true) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgTrupiDokumentitNukDuhetLeneBosh"));
        e.processOnServer = false;
        click = false;
    }
    grida.setLastSel2(-1);
}
function EndRequestHandler(sender, args) {
    formatoFushaDevi();
    // This code is executed after a successful async. postback

    var statusRuajtje = $("input[id$='hfStatusRuajtje']")[0];
    var statusRuajtjeQendra = $("input[id$='hfStatusQendra']")[0];


    // Utils.hiqLoadingGif();;
    switch (statusRuajtjeQendra.value) {
        case "true":
            regjQK.initGridQK({ ngaThirret: 'fk', formatNr: formatNumriZgjedhur, konfigurimGride: null, PershkrimiKokes: "" }, "modifikim", { idKoka: 0, dteDtDok: Data_DateEdit.date.toDateString(), dteDtRegj: DateRegjistrimi_DateEdit.date.toDateString(), idKonfig: $('#hfIdKonfigAmbjenteQK').val() }, { idDokGjenerues: $('#hfId').val(), idKonfigGjenerues: konfigurimi_ComboBox.GetValue(), llogariFK: null });
            statusRuajtjeQendra.value = "false";
            break;
        case "false":
            ASPxMenu1.GetItemByName('QendraKosto').SetVisible(true);
            ASPxMenu1.GetItemByName('Kontabilizo').SetVisible(false);
            ASPxMenu1.GetItemByName('Draft').SetVisible(false);
            break;
    }

    if (statusRuajtje.value == "true") {
        if ($("input[id$='hfqkmesazhi']").val() == 'shfaqmesazh') {
            myMesazh.ShtoMesazh({ type: "confirm", layout: "center", modal: true, text: hfState.Get("msgDeshironiShperndarjeQendraKosto"), cancelClick: JopopupClick, okClick: hapPopUp });
            click = false;
            $("input[id$='hfqkmesazhi']").val('jo');
            return;
        }
        if ($("input[id$='hfqkmesazhi']").val() == 'shfaqlupe') {
            $("#div1").show();
            $("#divQk").show();
            $("input[id$='hfqkmesazhi']").val('jo');
            regjQK.initGridQK({ ngaThirret: 'fk', formatNr: formatNumriZgjedhur, konfigurimGride: null, PershkrimiKokes: "" }, "modifikim", { idKoka: 0, dteDtDok: Data_DateEdit.date.toDateString(), dteDtRegj: DateRegjistrimi_DateEdit.date.toDateString(), idKonfig: $('#hfIdKonfigAmbjenteQK').val() }, { idDokGjenerues: $('#hfId').val(), idKonfigGjenerues: konfigurimi_ComboBox.GetValue(), llogariFK: null });
            ASPxMenu1.GetItemByName('QendraKosto').SetVisible(true);
            click = false;
            ASPxMenu1.GetItemByName('Kontabilizo').SetVisible(false);
            ASPxMenu1.GetItemByName('Draft').SetVisible(false);
            return;
        }
        ASPxMenu1.GetItemByName('Shto').SetVisible(true);
        myFaqeCelje.kontrolloTeDrejta('Shto_FleteKontabel.aspx', true);
    }
    else click = false;
}

function JopopupClick(s, e) {
    var statusRuajtje = $("input[id$='hfStatusRuajtje']")[0];
    if (statusRuajtje.value == "true") {
        ASPxMenu1.GetItemByName('Shto').SetVisible(true);
        myFaqeCelje.kontrolloTeDrejta('Shto_FleteKontabel.aspx', true);
    }
    else
        click = false;
    $("#div1").hide();
    $("#divQk").hide();
}

function closePopup(s, e) {
    popupUniversal.SetContentUrl('');

}
function hapPopUp(s, e) {
    $("#div1").show();
    $("#divQk").show();
    regjQK.initGridQK({ ngaThirret: 'fk', formatNr: formatNumriZgjedhur, konfigurimGride: null, PershkrimiKokes: "" }, "modifikim", { idKoka: 0, dteDtDok: Data_DateEdit.date.toDateString(), dteDtRegj: DateRegjistrimi_DateEdit.date.toDateString(), idKonfig: $('#hfIdKonfigAmbjenteQK').val() }, { idDokGjenerues: $('#hfId').val(), idKonfigGjenerues: konfigurimi_ComboBox.GetValue(), llogariFK: null });
    ASPxMenu1.GetItemByName('QendraKosto').SetVisible(true);
    ASPxMenu1.GetItemByName('Kontabilizo').SetVisible(false);
    ASPxMenu1.GetItemByName('Draft').SetVisible(false);
}
function endCallBack() {
    var grida = jQuery("#rowed5");
    grida.GridUnload("rowed5");

    inicializoGride();
    mbushGrideNgaHiddenFieldet(); //kurset();
    Totalet(); Utils.hiqLoadingGif();;
}
function validoClientSide() {
    if (txtNrDokumenti.GetText() == '') {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgVendosniNumrinEDokumentit"));
        return false;
    }
    else if (Data_DateEdit.GetDate() == null) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniNjeDateDokumenti"));
        return false;
    }
    else if (DateRegjistrimi_DateEdit.GetDate() == null) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniNjeDateRegjstrimi"));
        return false;
    }

    //kontroll nqs date e dok eshte ne intervalin e periidhes kontabel aktuale qe ruhet ne sesion
    //            var result = ValidoDateDokumenti();
    //            if (!result) {
    //                alert("Data e dokumentit nuk i perket periudhes aktuale!");
    //                return false;
    //            }
    else return true;
}

function PastroClick() {

    ASPxMenu1.GetItemByName('Fshi').SetVisible(false);
    ASPxMenu1.GetItemByName('PrintPreview').SetVisible(false);
    ASPxMenu1.GetItemByName('Klono').SetVisible(false);
    ASPxMenu1.GetItemByName('QendraKosto').SetVisible(false);
    ASPxMenu1.GetItemByName('Kontabilizo').SetVisible(true);
    ASPxMenu1.GetItemByName('Draft').SetVisible(true);
    var hf = $("input[id$='hfShtimModifikim']");
    hf.val("shtim");
    myMenu.menuSipasTeDrejtaRegjistrim(hf, hfTeDrejta);
    pastro();
    callWebserviceNiveliNew('', 'fletekontabel', false);
    $('#ASPxSplitter1_hl').empty();
    click = false;
    $("#div1").hide();
    $("#divQk").hide();
}
//function callWebserviceNiveli(name) {
//}
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
    konfigurimi_ComboBox.ClearItems();
    for (i = 0; i < colModelet.length; i++) {
        konfigurimi_ComboBox.AddItem([colModelet[i].KodKonfigAmbjente, colModelet[i].PershkrimKonfigAmbjente], colModelet[i].IdKonfigAmbjente); //AddItem(teksti, vlera);
    }
    konfigurimi_ComboBox.SelectIndex(0);
    ndryshoKonfigurimin();
}

/*
Function: SucceededCallbackNiveli
    
Ndryshon vleren e zgjedhur te konfigurimit sipas nivelit te zgjedhur.
Therret funksionin <ndryshoKonfigurimin>.
*/
function SucceededCallbackNiveli(result) {
    var vlerat = '';
    vlerat = result.split('|');
    konfigurimi_ComboBox.ClearItems();
    for (i = 0; i < vlerat.length - 1; i++) {
        var arr = vlerat[i].split(',')[1].split(';');
        konfigurimi_ComboBox.AddItem(arr.split(';')[0], vlerat[i].split(',')[0]); //AddItem(teksti, vlera);
    }
    konfigurimi_ComboBox.SelectIndex(0);
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
                Utils.shfaqLoadingGif();
                ruajClick(s, e);
            }
            else {
                e.processOnServer = false;
                click = false;
                Utils.hiqLoadingGif();
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgPlotesoniTeGjithaFushat"));
            }
        }
        else if (e.item.name == 'Draft') {
            myFaqeCelje.validim(s, e);
            if (validoClientSide()) {
                Utils.shfaqLoadingGif();;
                ruajClick(s, e);
            }
            else {
                click = false;
                e.processOnServer = false;
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgPlotesoniTeGjithaFushat"));
            }
        }
        else if (e.item.name == 'Kontabilizo') {
            myFaqeCelje.validim(s, e);
            if (validoClientSide()) {
                Utils.shfaqLoadingGif();;
                ruajClick(s, e);
            }
            else {
                click = false;
                e.processOnServer = false;
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgPlotesoniTeGjithaFushat"));
            }
        }
        else if (e.item.name == "Klono") {
            var HfShtimModifikim = $("input[id$='hfShtimModifikim']");
            HfShtimModifikim.val('klonim');
            $("input[id$='hfLidhur']").val(false);
            lidhur = false;
            click = false;
            myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, HfShtimModifikim, '');
            formoArrayKolGrides();
            $('#ASPxSplitter1_hl').empty();
            var grida = $('#rowed5');
            grida.setLastSel2(-1);
            $("#div1").hide();
            $("#divQk").hide();
            $("input[id$='hfId']").val('0');
            grida.GridUnload("rowed5");
            grida = $('#rowed5');
            ruajFormatetNeGride(grida);
            inicializoGride();
            mbushGrideNgaHiddenFieldet();
            txtNrReference.SetText($("input[id$='hfNrRef']")[0].value);
            ASPxMenu1.GetItemByName('Shto').SetVisible(true);
            vendosDateDefault(HfShtimModifikim.val());
            myMenu.menuSipasTeDrejtaRegjistrim(HfShtimModifikim, hfTeDrejta);
        }
        else if (e.item.name == 'Fshi') {
            popFshi.Show(); e.processOnServer = false; click = false;
        }
        else if (e.item.name == 'Kerko') {
            myFaqeCelje.kontrolloTeDrejta("FleteKontabel.aspx", null, true);
            e.processOnServer = false; click = false;
        }
        else if (e.item.name == 'Shto') {
            $("input[id$='hfLidhur']").val(false);

            myFaqeCelje.kontrolloTeDrejta('Shto_FleteKontabel.aspx', true);
            e.processOnServer = false;
            click = false;
        }
        else if (e.item.name == 'Anullo') {
            myFaqeCelje.kontrolloTeDrejta("FleteKontabel.aspx");
            e.processOnServer = false;
            click = false;
        }
        else if (e.item.name === 'QendraKosto') {
            myFaqeCelje.validim(s, e);

            if (validoClientSide()) {
                Utils.shfaqLoadingGif();
                e.processOnServer = false;
                regjQK.ruajRegjQK(true, txtNrDokumenti.GetText(), txtNrReference.GetText(), txtPershkrimi.GetText(), 1, "LupaRegjistrimQendraKosto.aspx", false, Data_DateEdit.date.toDateString(), DateRegjistrimi_DateEdit.date.toDateString());
                click = false;
            }
            else {
                e.processOnServer = false;

            }
        }
    }
}
function Kthehu(mesazh) {
    myFaqeCelje.kontrolloTeDrejta("FleteKontabel.aspx?ruaj=po");

}

function ButtonClickQendraKosto(mag) {//po
    if (cmbLloji.GetValue() == 1)
        myButtonClickLupa.LupaUniversal_Click(hfState.Get("headerPopUpZgjidhQendrenKostos"), 'Shto_QendraKosto.aspx?lupe=true&llojLupe=plote&vjenNga=RegjistrimQendraKosto', 900, 600);
    else myButtonClickLupa.LupaUniversal_Click(hfState.Get("headerPopUpZgjidhSkemenKostos"), 'LupaSkemaKosto.aspx?vjenNga=RegjistrimQendraKosto', 600, 600);

}

function ButtonClickObjektiva() {
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhniObjektivenEKostos"), 'LupaObjektivaKosto.aspx?vjenNga=Shto_Punonjes', 600, 600);
}

function TextChangedQendraKosto() {//po
    if (cmbQendraKosto.GetText() != "")
        regjQK.vendosQKapoSkemeNeGride(cmbLloji.GetValue(), cmbQendraKosto.GetText(), $('#hfShtimModifikim').val());
}
function TextChangedObjektiva() {//po
    magazina1 = cmbObjektiva.GetText();
    if (magazina1 != "")
        regjQK.vendosObjektiveNeGride(cmbObjektiva.GetText());
}

/*
Function: RuajClick

Validon dokumentin dhe therret funksionin <merrTeDhena> per te marre te dhenat e grides kur klikohet butoni Ruaj.
*/

function RuajClick(s, e) {
    if (validoClientSide()) {
        merrTeDhenatArtPerberes(-1);
    }
    else {
        e.processOnServer = false;
        click = false;
        Utils.hiqLoadingGif();;
    }
}

var colNorma = new Array();

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

var editorNjesia;


function ButtonClickKursi(s, e) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var idMonedha = $("#txtMonedha" + idRresht).val();
    if (idMonedha != 0 && idMonedha != null && idMonedha != -1 && idMonedha != '') {
        popupUniversal.SetHeaderText('Zgjidh Kursin');
        popupUniversal.SetContentUrl('LupaKursiShpejte.aspx?idMonedha=' + idMonedha + '&llojKursi=' + llojkursi);
        popupUniversal.SetSize(widthLupaKursi, heightLupaKursi);
        popupUniversal.Show();
    }
}