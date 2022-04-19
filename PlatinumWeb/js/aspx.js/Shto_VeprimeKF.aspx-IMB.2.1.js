;
var isKursNdrysheMonShfaqur = false;
var lidhur = false;
var sourceAutocomplete = new Array();
var totaletSasiveDetajimeve = new Array();
var arrayIdKolonaGrides = new Array();
var arrayPershkrimiKolonaGrides = new Array();
var arrayVisibleKolonaGrides = new Array();
var arrayWidthKolonaGrides = new Array();
var arrayReadOnlyKolonaGrides = new Array();
var arrayRenditjeKolonaGrides = new Array();
var arrayIndexTrupi = new Array();
var kfTeshtuar = 0;
var widthLupaLlogari = 800;
var heightLupaLlogari = 600;
var widthLupaKF = 930;
var heightLupaKF = 600;
var widthLupaKerko = 1000;
var heightLupaKerko = 600;

var identikuesPerPopupKursi = "ShtoVeprimeKf";
var llojkursi = 1; //perdoret per te ruajtur llojin e kursit te zgjedhur tek konfigurimi. Ne qofte se nuk ka asnje lloj te zgjedhur, atehere merret lloji i pare.
var widthLupaKursi = 950;
var heightLupaKursi = 560;
var infoKf = false;
var idInfoKf = 0;

var varKonfig = {
    identifikuesPerLocalStorageKey: 'VerpimeKF'
};

$(document).ready(function () {
    Utils.konfiguroAccorditionNeDocReady(varKonfig.identifikuesPerLocalStorageKey);
    $(window).on('resize', function () {
        try {
            if (Utils.isGridResized())//if (!Utils.resizeSplitter(145))
                return;
        }
        catch (ee) {
        }
        var grida = $('#rowed5');
        if ($('#divgride2').width() != null) {
            myJQGrid.fixGridWidth(grida, $('#divgride2'));
        }
    }).trigger('resize');
    $(document).keydown(function (e) {//po
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

/*
Function: inicializoGride

Inicializon griden e trupit. Konfiguron kolonat e grides dhe percakton veprimin qe kryhet onCellSelect.
ndryshuar rreshti i pare
*/
function inicializoGride() {
    var classes = '';
    if (lidhur == true)
        classes = 'uigray';

    var arrayPershkrime = [arrayPershkrimiKolonaGrides[0], arrayPershkrimiKolonaGrides[1], arrayPershkrimiKolonaGrides[2], arrayPershkrimiKolonaGrides[3],
                           arrayPershkrimiKolonaGrides[4], arrayPershkrimiKolonaGrides[5], arrayPershkrimiKolonaGrides[6], arrayPershkrimiKolonaGrides[7],
                            arrayPershkrimiKolonaGrides[8], arrayPershkrimiKolonaGrides[9], arrayPershkrimiKolonaGrides[10], arrayPershkrimiKolonaGrides[11],
                            arrayPershkrimiKolonaGrides[12], arrayPershkrimiKolonaGrides[13], arrayPershkrimiKolonaGrides[14], arrayPershkrimiKolonaGrides[15], arrayPershkrimiKolonaGrides[16], arrayPershkrimiKolonaGrides[17]];
    var arrayModel = [
        { name: arrayIdKolonaGrides[0], index: arrayIdKolonaGrides[0], width: arrayWidthKolonaGrides[0], hidden: arrayVisibleKolonaGrides[0], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemNrRendor, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[1], index: arrayIdKolonaGrides[1], width: arrayWidthKolonaGrides[1], hidden: arrayVisibleKolonaGrides[1], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myElemKodi, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[2], index: arrayIdKolonaGrides[2], width: arrayWidthKolonaGrides[2], hidden: arrayVisibleKolonaGrides[2], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myElemEmertimi, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[3], index: arrayIdKolonaGrides[3], width: arrayWidthKolonaGrides[3], hidden: arrayVisibleKolonaGrides[3], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemPershkrimi, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[4], index: arrayIdKolonaGrides[4], width: arrayWidthKolonaGrides[4], hidden: arrayVisibleKolonaGrides[4], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemData, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[5], index: arrayIdKolonaGrides[5], width: arrayWidthKolonaGrides[5], hidden: arrayVisibleKolonaGrides[5], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemComboDebiKredi, custom_value: myJQGrid.myValueCombo } },
        { name: arrayIdKolonaGrides[6], index: arrayIdKolonaGrides[6], width: arrayWidthKolonaGrides[6], hidden: arrayVisibleKolonaGrides[6], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemFatura, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[7], index: arrayIdKolonaGrides[7], width: arrayWidthKolonaGrides[7], hidden: arrayVisibleKolonaGrides[7], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemDataFatura, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[8], index: arrayIdKolonaGrides[8], width: arrayWidthKolonaGrides[8], hidden: arrayVisibleKolonaGrides[8], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemVleftaFatura, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[9], index: arrayIdKolonaGrides[9], width: arrayWidthKolonaGrides[9], hidden: arrayVisibleKolonaGrides[9], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemVleftaFaturaMonBaze, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[10], index: arrayIdKolonaGrides[10], width: arrayWidthKolonaGrides[10], hidden: arrayVisibleKolonaGrides[10], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemMonedha, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[11], index: arrayIdKolonaGrides[11], width: arrayWidthKolonaGrides[11], hidden: arrayVisibleKolonaGrides[11], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemVlefta, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[12], index: arrayIdKolonaGrides[12], width: arrayWidthKolonaGrides[12], hidden: arrayVisibleKolonaGrides[12], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemKursi, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[13], index: arrayIdKolonaGrides[13], width: arrayWidthKolonaGrides[13], hidden: arrayVisibleKolonaGrides[13], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemVleftaMonBaze, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[14], index: arrayIdKolonaGrides[14], width: arrayWidthKolonaGrides[14], hidden: arrayVisibleKolonaGrides[14], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemLlog, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[15], index: arrayIdKolonaGrides[15], width: arrayWidthKolonaGrides[15], hidden: arrayVisibleKolonaGrides[15], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemIdKodi, custom_value: myJQGrid.myValueTextBox }, hidedlg: true },
        { name: arrayIdKolonaGrides[16], index: arrayIdKolonaGrides[16], width: arrayWidthKolonaGrides[16], hidden: arrayVisibleKolonaGrides[16], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemEmertimiLlog, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[17], index: arrayIdKolonaGrides[17], width: arrayWidthKolonaGrides[17], hidden: arrayVisibleKolonaGrides[17], classes: classes, sorttype: "int", editable: true, edittype: 'custom', editoptions: { custom_element: myElemButtonFshi, custom_value: myValueButtonFshi }, hidedlg: true }
    ];
    if ($("input[id$='hfLidhur']")[0].value == 'True')
        lidhur = true;
    else lidhur = false;


    var selektoriGrides = "#rowed5";

    var gridParams = {
        emergride: selektoriGrides,
        arrayPershkrime: arrayPershkrime,
        arrayModel: arrayModel,
        lidhur: lidhur,
        emerEditorKodi: "txtKodi",
        widthi: $('#divgride2').width() - 5,
        subgrid: false,
        lostFocusKoloneFundit: lostFocusKoloneFundit,
        resetRreshtKorent: resetRreshtKorent,

        autocompleteList: [{ emerEditor: "txtKodi", selectFunc: selectFunc, changeFunc: changeFunc, shtoDataKod: false },
    { emerEditor: 'txtLlogKunderparti', selectFunc: selectFunc2, changeFunc: changeFunc2, shtoDataKod: false }],

        konfigToolbar: {
            konfigGrid: $('#hfTeDrejtaKonfGride').val(),
            konfigkf: $("#hfMeFatura").val() == 'true' ? false : $('#hfTeDrejtaKFRi').val(),
            ruajKolonatEGrides: ruajKolonatEGrides
        }
    };
    myJQGrid.initGride(gridParams);
}


//    myJQGrid.inicializoGride("#rowed5", arrayPershkrime, arrayModel, lidhur, lastsel2,
//        '#txtKodi', null, null, null, null, null, $('#divgride2').width() - 5, false, null, null, null,
//        '#txtLlogKunderparti', undefined, undefined, undefined, $('#hfTeDrejtaKonfGride').val(), undefined,
//        $("#hfMeFatura").val() == 'true' ? false : $('#hfTeDrejtaKFRi').val());
//}

/*
Vendos formatet e numrave ne gride ne baze te emrit te kolones
*/
function ruajFormatetNeGride(grida, formatNumri) {
    if (typeof formatNumri == 'undefined')
        formatNumri = $.parseJSON(hfState.Get("formatMonedhe"));

    var formatMOn = $.parseJSON(hfState.Get("formatKurset"));
    ndryshoKonfigFormatNumri(grida, formatNumri);
    vendosVleraDefaultNeGride(grida);
    vendosKonfigFormatNumri();
    return;
}


function dateGotFocus(s, e) {
    window.setTimeout(function () { s.SetCaretPosition(0); }, 0);
}
/*
Vendos vlerat default te fushave ne gride ne baze te emrit te kolones
*/
function vendosVleraDefaultNeGride(grida) {
    grida.setVlereDefault('txtVlefteFature', 1);
    grida.setVlereDefault('txtVlefteMonFature', 1);
    grida.setVlereDefault('txtVlefta', 0);
    grida.setVlereDefault('txtVleftaMon', 1);
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
    var formatMOn = $.parseJSON(hfState.Get("formatKurset"));
    grida.setShifraPasPresjes('txtVlefteFature', formatNumri.KonfigTrupi[0].ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtVlefteMonFature', formatNumri.KonfigTrupi[0].ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtVlefta', formatNumri.KonfigTrupi[0].ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtVleftaMon', formatNumri.KonfigTrupi[0].ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtKursi', formatMOn[0].IdFormatNrKursi);
    Utils.setFormatNumri(txtVlefta, formatNumri.ShifraPasPresjesVlefta);
}

function SucceededCallbackFormatNumri(formatNumri) {
    var grida = $('#rowed5');
    ndryshoKonfigFormatNumri(grida, formatNumri);
    vendosKonfigFormatNumri();
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
/*
Formaton vlerat e fushave sipas formatit perkates.
*/
function vendosKonfigFormatNumri() {
    var grida = $('#rowed5');
    var idTe = grida.jqGrid('getDataIDs');
    for (j = 0; j < idTe.length; j++) {
        var idRreshti = idTe[j];
        grida.formatoQelize('txtVlefteFature', idRreshti);
        grida.formatoQelize('txtVlefteMonFature', idRreshti);
        grida.formatoQelize('txtVlefta', idRreshti);
        grida.formatoQelize('txtVleftaMon', idRreshti);
        grida.formatoQelize('txtKursi', idRreshti);
    }
    formatoFushaDevi();
}

function formatoFushaDevi() {
    Utils.formatoTextBox(txtVlefta);
}

function unformatoFushaDevi() {
    Utils.unFormatoTextBox(txtVlefta);
}

function ruajKolonatEGrides(grida) {
    var idGride = colGrida[0].IdKoka;
    var idGjuha = hfState.Get('idGjuha');
    var idNdermarrje = hfState.Get('idNdermarrje');
    var idViti = hfState.Get('idViti');
    var idPerdoruesi = hfState.Get('idPerdoruesi');
    myJQGrid.ruajKolonatEGrides(grida, idGride, idGjuha, idNdermarrje,idViti, idPerdoruesi);
}

function formGridColsArray() {
    var hfGridKod = $('#hfGridaKodi');
    var IdKonfigAmbjenteLupat = [{ hfVar: hfGridKod, kodiText: "txtKodi" }];
    myJQGrid.formArrayKolGrides($("input[id$='HfGridCol']"), arrayIdKolonaGrides, arrayPershkrimiKolonaGrides, arrayVisibleKolonaGrides, arrayReadOnlyKolonaGrides, lidhur, arrayWidthKolonaGrides, IdKonfigAmbjenteLupat, arrayRenditjeKolonaGrides);
}


function kursiFmatter(cellvalue, options, rowObject) {
    return myJQGrid.kursiFmatter(cellvalue, options, rowObject);
}

/*
Function: renditKolonatGrides

Therret metoden remapColumns te jqGrid per te renditur kolonat e grides sipas vlerave te array-t qe i kalohet kesaj metode si parameter
*/
//function renditKolonatGrides() {
//    myJQGrid.renditKolonatGrides("#rowed5", arrayRenditjeKolonaGrides);
//}

function myelemIdKodi(value, options) {//po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[15];
    return myJQGrid.myElemIdKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[15]);
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
Function: myElemKodi

Nderton nje textbox dhe nje buton per te zgjedhur klient furnitorin

*/
function myElemKodi(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var hf = $("#hfShtimModifikim");
    var hffat = $("#hfMeFatura");
    if (hffat.val() == "true")
        disabled = true;
  else  disabled = arrayReadOnlyKolonaGrides[1];
    return myJQGrid.myElemKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[1], ButtonClickKodi, keyPressKodi, changeFunc); //'kontrollo',
}

/*
Function: myElemLlog

Nderton nje textbox dhe nje buton per te zgjedhur llogarine

*/
function myElemLlog(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var hf = $("#hfShtimModifikim");
    disabled = arrayReadOnlyKolonaGrides[14];
    var hidField3 = $("#hfMeKF");
    if (hidField3.val() == "false")
        llog = (cmbLlogariKunderParti.GetSelectedItem() != null) ? cmbLlogariKunderParti.GetSelectedItem().GetColumnText('NrLlogari') : cmbLlogariKunderParti.GetText();
    else llog = (cmbKFKunderParti.GetSelectedItem() != null) ? cmbKFKunderParti.GetSelectedItem().GetColumnText('KodKlientFurnitor') : '';
    if (value == "" && llog != "" && llog != undefined)
        value = llog;
    return myJQGrid.myElemKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[14], ButtonClickLlog, keyPressLlog, changeFunc2); //'kontrollo',
}

/*
0Function: keyPressKodi

Shton nje rresht te ri ne gride nese jemi ne rreshtin e fundit dhe therret funksionin <callWebserviceKodi>.
*/
function keyPressKodi() {

    callWebserviceKodi();
}

/*
Function: keyPressKodi

Shton nje rresht te ri ne gride nese jemi ne rreshtin e fundit dhe therret funksionin <callWebserviceKodi>.
*/
function keyPressLlog() {
    callWebserviceLlog();

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
    callWebServiceInfoKF();
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
function myElemEmertimiLlog(value) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var hidField3 = $("#hfMeKF");
    if (hidField3.val() == "false")
        llog = (cmbLlogariKunderParti.GetSelectedItem() != null) ? cmbLlogariKunderParti.GetSelectedItem().GetColumnText('EmerLlogari1') : llogariezgjedhur.EmerLlogari1;
    else llog = (cmbKFKunderParti.GetSelectedItem() != null) ? cmbKFKunderParti.GetSelectedItem().GetColumnText('EmertimiKF') : '';
    if (value == "" && llog != "" && llog != undefined)
        value = llog;
    return myJQGrid.myElemEmertimi(value, arrayReadOnlyKolonaGrides[16], idRresht, 'txtEmertimiLlog');
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
    return myJQGrid.myElemEmertimi(value, arrayReadOnlyKolonaGrides[3], idRresht, 'txtPershkrim');

}
/*
Function: myElemFatura

Nderton nje textbox ku vendoset nr i fatures
*/
function myElemFatura(value) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemEmertimi(value, arrayReadOnlyKolonaGrides[6], idRresht, 'txtNrFature');

}
/*
Function: myElemDataFatura

Nderton nje textbox ku vendoset dt i fatures
*/
function myElemDataFatura(value) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemEmertimi(value, arrayReadOnlyKolonaGrides[7], idRresht, 'txtDtFature');

}
/*
Function: myelemData

Nderton nje dateedit ku vendoset data
*/
function myelemData(value) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if (value == "")
        value = dteDtDok.GetText();
    var hf = document.getElementById("hfShtimModifikim");
    disabled = arrayReadOnlyKolonaGrides[4];
    if (hf.value == "modifikim")
        disabled = 'True';
    return myJQGrid.myelemData(value, disabled, idRresht, 'dteData', undefined, onchanged);

}
function onchanged() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    try {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "merrKursiSipasKodMonedhesAndDates"),
            data: JSON.stringify({ prefixText: $('#txtMonedha' + idRresht).val(), date: $('#dteData' + idRresht).val(), lloji: llojkursi })
        }).done(SucceededCallbackKursi);
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
    }
}

function SucceededCallbackKursi(result) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if (result === "0") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimKursiMonedha"));
        txtKursi.SetText("1");
        kursifundit = 1;
    }
    else {
        txtKursi.SetText(new Date(result));
        kursifundit = result;
    }
    $('#txtKursi' + idRresht).keyup();
}




/*
Function: myelemComboDebiKredi

Nderton nje combobox ku vendoset debi kredi
*/
function myelemComboDebiKredi(value, options, idRreshti, replace) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    if (replace === undefined)
        replace = false;
    if (idRreshti === undefined)
        idRreshti = idRow;
    var objNjesi = new Object();
    objNjesi.value = "1";
    objNjesi.text = "Debi";
    var arrayOptions = new Array(2);
    arrayOptions[0] = objNjesi;
    objNjesi = new Object();
    objNjesi.value = "2";
    objNjesi.text = "Kredi";
    arrayOptions[1] = objNjesi;
    var disabled;
    if (arrayReadOnlyKolonaGrides[5] == 'True')
        disabled = true;
    else
        disabled = false;
    if ($("#hfMeFatura").val() == 'true')
        disabled = true;
    var myCombo = myJQGrid.getMyCombo(grida, "cmbDebiKredi", idRreshti, arrayOptions, value, replace, disabled);
    if (myCombo)
        return myCombo;
    return myJQGrid.myElemCombo(value, arrayIdKolonaGrides[5], idRreshti, null, arrayOptions, disabled);
}
/*
Function: myElemVlefta

Nderton nje textbox per te vendosur vleften.
*/
function myElemVlefta(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();    
    //return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, arrayReadOnlyKolonaGrides[11], idRresht, 'txtVlefta', vendosVleftat, hfFormatNumri);
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: arrayReadOnlyKolonaGrides[11] == 'True' ? "True" : "False", indexRow: idRresht, id: "txtVlefta", onKeyDown: vendosVleftat });
}
/*
Function: myElemVleraFatura

Nderton nje textbox per te vendosur vlerafatura.
*/
function myElemVleftaFatura(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, arrayReadOnlyKolonaGrides[8], idRresht, 'txtVlefteFature', '', hfFormatNumri);
    // return myJQGrid.myElemVleftaFat(grida,value, options, arrayReadOnlyKolonaGrides[7], lastsel2, 'txtVlefteFature', '');

}
/*
Function: myElemMonedha

Nderton nje textbox ku vendoset monedha
*/
function myElemMonedha(value) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemEmertimi(value, arrayReadOnlyKolonaGrides[10], idRresht, 'txtMonedha');

}
var kursifundit = 1;

function getKurs(monedha, tmpMonKursi) {
    for (var i = 0; i < tmpMonKursi.length; i++) {
        if (tmpMonKursi[i][0] == monedha)
            return tmpMonKursi[i][1];
    }
    return 0;
}

function isKursNdryshe(value) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if (!isKursNdrysheMonShfaqur) {
        var rreshtaTeGrides = grida.getRowData();
        var tmpMonKursi = new Array();
        var j = 0;
        var idTe = grida.jqGrid('getDataIDs');
        for (var i = 0; i < idTe.length; i++) {
            var kodi, monedha, kursi;
            if (grida.getTekstQelize('txtKodi', idTe[i]) != undefined) {
                kodi = grida.getTekstQelize('txtKodi', idTe[i]);
                monedha = grida.getTekstQelize('txtMonedha', idTe[i]);
                kursi = grida.getTekstQelize('txtKursi', idTe[i]);
            }


            if (kodi.toString() === "")
                continue;
            tmpMonKursi[j] = new Array();
            var kursGride = getKurs(monedha, tmpMonKursi);
            if (kursGride !== 0 && kursGride !== kursi) {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgShtoVeprimKFKujdesKaKurseTeNdryshemPerTeNjejtenMonedhe"));
                isKursNdrysheMonShfaqur = true;
                var hffat = $("#hfMeFatura");
                if (hffat.val() == 'true') {
                    grida.setTekstQelize('txtKursi', idRresht, kursGride);
                    isKursNdrysheMonShfaqur = false;
                }
                return;
            }
            tmpMonKursi[j][0] = monedha;
            tmpMonKursi[j][1] = kursi;
            j++;
        }
        kursmon = grida.getTekstQelize('txtKursi', idRresht);
    }
}

/*
Function: myElemKursi

Nderton nje textbox per te vendosur kursin.
*/
function myElemKursi(value, options) {
    var grida = $('#rowed5');
  var idRresht = grida.getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[12];
    hf = $("input[id$='hfMonedhaNder']")[0];
    if (hf.value == $('#txtMonedha' + idRresht).val())
        disabled = 'True';
    if (value != "")
        kursifundit = value;
    if (value === "")
        value = "1";
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, disabled, idRresht, 'txtKursi', changedKursi, hfFormatNumri, kontrolloKurs, true, ButtonClickKursi);
}

function kontrolloKurs(s, e) {//po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var kursi = grida.getTekstQelize('txtKursi', idRresht);
    if (kursi === "" || isNaN(kursi))
        grida.setTekstQelize('txtKursi', idRresht);
    if (parseFloat(kursi) === 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKursiNukMundTeJeteZero"));
        grida.setTekstQelize('txtKursi', idRresht);
    }
    var diferenca = Math.abs(kursifundit - parseFloat(grida.getTekstQelize('txtKursi', idRresht)));
    if (diferenca / kursifundit > 0.2)
        myMesazh.ShtoMesazhInformues(hfState.Get("msgKursiRiNdryshonShumeMeKursinMePare"));
}

function changedKursi() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var kursi = grida.getTekstQelize('txtKursi', idRresht);
    if (kursi == ".")
        grida.setTekstQelize('txtKursi', idRresht, '0.');
    vendosVleftat("", idRresht);
}

/*
Function:myElemVleftaMonBaze

Nderton nje textbox per te vendosur vleften.
*/
function myElemVleftaMonBaze(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
   // return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, arrayReadOnlyKolonaGrides[13], idRresht, 'txtVleftaMon', vendosVleftatMon, hfFormatNumri);
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: arrayReadOnlyKolonaGrides[13] == 'True' ? "True" : "False", indexRow: idRresht, id: "txtVleftaMon", onKeyDown: vendosVleftatMon });
}

/*
    Function:myElemVleftaFaturaMonBaze

    Nderton nje textbox per te vendosur vleften.
*/
function myElemVleftaFaturaMonBaze(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, arrayReadOnlyKolonaGrides[9], idRresht, 'txtVlefteMonFature', '', hfFormatNumri);
    // return myJQGrid.myElemVleftaFat(value, options, arrayReadOnlyKolonaGrides[8], lastsel2, 'txtVlefteMonFature', '');
}
function selectFunc(event, ui, emerfushe, idArt, kodArt) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    var hffat = $("#hfMeFatura");
    var hidField3 = $("#hfMeKF");
    var ekziston = false;
    if (ui !== null && ui.item != null) {
        $(emerfushe).val(ui.item.label);
        if (hffat.val() != 'true' && hidField3.val() == "false") {
            var rreshtaTeGrides = grida.getRowData();
            for (i = 0; i < rreshtaTeGrides.length; i++) {
                if (rreshtaTeGrides[i].txtKodi.toString().search('value') != -1)
                    continue;
                var editorkodi = rreshtaTeGrides[i].txtKodi;
                if (editorkodi == ui.item.label && editorkodi != "") {
                    myMesazh.ShtoMesazhGabimi(hfState.Get("msgLupaKFEkzistonKyKFNeGride"));
                    ekziston = true;
                    $(emerfushe).val("");
                    break;
                }
            }
        }
        if (!ekziston)
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "ktheKlientFurnitorVeprimeKFMeIDRow"),
            data: JSON.stringify({ IDkf: ui.item.value, rreshti: idRow, date: dteDtDok.GetDate(), llojKursi: llojkursi })
        }).done(SucceededCallbackVleraKodi);
    }
    return false;
}

function selectFunc2(event, ui, emerfushe, idArt, kodArt) {
    if (idArt != undefined && kodArt != undefined && idArt != "" && kodArt != "") {
        $(emerfushe).val(kodArt);
        KtheVleraLLog(idArt);
        return false;
    }
    if (ui.item != null) {
        $(emerfushe).val(ui.item.label);
    }
    KtheVleraLLog(ui.item.value);
    return false;
}
function changeFunc2(event, ui, emerKodi, index) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    var emerfushe = '#' + emerKodi + idRow;
    if (ui == null || ui.item == null) {
        if ($(emerfushe).val() != "") {
            KtheVleraLLogMeKod($(emerfushe).val());
            return;
        }
    }
    KtheVleraLLog(ui.item.value);
}
function KtheVleraLLog(idja) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    try {
        var hidField3 = $("#hfMeKF");
        if (hidField3.val() == "false") {
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("ListPagesa", "KtheVleraLlogMeID"),
                data: JSON.stringify({ idja: idja })
            }).done(SucceededCallbackLlogari);
        }
        else
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "ktheKlientFurnitorVeprimeKFMeIDRow"),
            data: JSON.stringify({ IDkf: idja, rreshti: idRow, date: dteDtDok.GetDate(), llojKursi: llojkursi })
        }).done(SucceededCallbackLlogari);
    } catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
    }
}

function KtheVleraLLogMeKod(kodi) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    try {
        var hidField3 = $("#hfMeKF");
        if (hidField3.val() == "false") {
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("ListPagesa", "ktheVleraLlogMeKod"),
                data: JSON.stringify({ kodi: kodi, idNderrmarje: hfState.Get('idNdermarrje') })
            }).done(SucceededCallbackLlogari);
        }
        else {
            $.ajax({
                url: Utils.getServerApiUrl("Rregjistrime", "ktheKlientFurnitorVeprimeKFRow"),
                data: JSON.stringify({ kodi: kodi, rreshti: idRresht, data: dteDtDok.GetDate() })
            }).done(SucceededCallbackLlogari);
        }
    } catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
    }
}
function SucceededCallbackLlogari(artikulli) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var kodi = "#txtLlogKunderparti" + idRresht;
    var hidField3 = $("#hfMeKF");
    if (hidField3.val() == "false") {
        if (artikulli !== null && artikulli.IdLlogari !== -1) {
            $(kodi).val(artikulli.NrLlogari);
            $('#txtEmertimiLlog' + idRresht).val(artikulli.EmerLlogari1);

        } else {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgShtoVeprimKFNukEkzistonKjoLlogari"));
            $(kodi).val('');
            $('#txtEmertimiLlog' + idRresht).val('');
        }

    }
    else {
        if (artikulli !== null && artikulli.oKF.IdKlientFurnitor !== -1) {
            if (artikulli.oKF.KodKlientFurnitor == $('#txtKodi' + idRresht).val()) {
                myMesazh.ShtoMesazhGabimi('Nuk lejohet i njejti klient/furnitor ne te dyja fushat');
                $(kodi).val('');
                $('#txtEmertimiLlog' + idRresht).val('');
                return;
            }
            $(kodi).val(artikulli.oKF.KodKlientFurnitor);
            $('#txtEmertimiLlog' + idRresht).val(artikulli.oKF.EmertimiKF);

        } else {
            myMesazh.ShtoMesazhGabimi('Ky klient/furnitor nuk ekziston');
            $(kodi).val('');
            $('#txtEmertimiLlog' + idRresht).val('');
        }
    }
}
var llogariezgjedhur = new Object();
function SucceededCallbackLlogariKunder(artikulli) {
    if (artikulli !== null && artikulli.IdLlogari !== -1) {
        TextChangedLlogaria(artikulli);
        llogariezgjedhur = artikulli;

    } else {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgShtoVeprimKFNukEkzistonKjoLlogari"));
        cmbLlogariKunderParti.SetText('');
        llogariezgjedhur.EmerLlogari1 = '';
    }

}
function SucceededCallbackKFKunder(artikulli) {
    if (artikulli !== null && artikulli.kf!==null && artikulli.kf.IdKlientFurnitor !== -1) {
        TextChangedKFur();

    } else {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgShtoVeprimKFNukEkzistonKjoLlogari"));
        cmbKFKunderParti.SetText('');
    }

}
function resetRreshtKorent(idRreshti) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    if (idRreshti == idRow && $('#txtKodi' + idRow).val() != undefined) {
        grida.setTekstQelize('txtKodi', idRreshti, '');
        grida.setTekstQelize('txtIdKodi', idRreshti, '');
        grida.setTekstQelize('txtEmertimi', idRreshti, '');// grida.setTekstQelize('txtEmertimiLlog', idRreshti, '');
        grida.setTekstQelize('txtMonedha', idRreshti, '');
        grida.setTekstQelize('txtKursi', idRreshti);
        kursifundit = 1;
        vendosVleftat("", idRreshti);
        return;
    }
    grida.jqGrid('delRowData', idRreshti);
    return;
}
/*
Function: kontrollo

Kontrollon nese nje artikull eshte zgjedhur me pare (ndodhet ne gride) apo jo.
*/
function changeFunc(event, ui, emerKodi, index) {
    var hffat = $("#hfMeFatura");
    var hidField3 = $("#hfMeKF");
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    //var index = -1;
    //var idKod = 'txtKodi';
    //if (emerKodi === undefined || emerKodi === null)
    //    index = myJQGrid.getIndexFromEvent(event, idKod);
    //else
    //    index = emerKodi.split(idKod)[1];
    if (index == idRow && $('#txtKodi' + idRow).val() != undefined) {
        if (ui == null || ui.item == null) {
            var emerfushe = '#' + emerKodi + idRow;
            var ekziston = false;
            if ($(emerfushe).val() != "")
                if (hffat.val() != 'true' && hidField3.val() == "false") {
                    var rreshtaTeGrides = grida.getRowData();
                    for (i = 0; i < rreshtaTeGrides.length; i++) {
                        if (rreshtaTeGrides[i].txtKodi.toString().search('value') != -1)
                            continue;
                        var editorkodi = rreshtaTeGrides[i].txtKodi;
                        if (editorkodi == $(emerfushe).val() && editorkodi != "") {
                            myMesazh.ShtoMesazhGabimi(hfState.Get("msgLupaKFEkzistonKyKFNeGride"));
                            ekziston = true;
                            resetRreshtKorent(index);
                            return;
                        }
                    }
                }
            if (ekziston)
                return;
            var kodi = $(emerfushe).val();
            if (kodi != undefined && typeof(kodi) != "undefined" && kodi != "") {
                $.ajax({
                    url: Utils.getServerApiUrl("Rregjistrime", "ktheKlientFurnitorVeprimeKFRow"),
                    data: JSON.stringify({ kodi: kodi, rreshti: index, data: dteDtDok.GetDate() })
                }).done(SucceededCallbackVleraKodi);
                return;
            }
            vendosKf({ idRreshti: index, oKF: null, monedha: 0, kursi: 0 })
            return;
        }
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "ktheKlientFurnitorVeprimeKFMeIDRow"),
            data: JSON.stringify({ IDkf: ui.item.value, rreshti: idRow, date: dteDtDok.GetDate(), llojKursi: llojkursi })
        }).done(SucceededCallbackVleraKodi);
        return;
    }
    var rreshti = grida.jqGrid('getRowData', index);
    if (ui == null || ui.item == null) {
        var ekziston = false;
        if (hffat.val() != 'true' && hidField3.val() == "false") {
            if (rreshti.txtKodi != "") {
                var counter = 0;
                var rreshtaTeGrides = grida.getRowData();
                for (i = 0; i < rreshtaTeGrides.length; i++) {
                    if (rreshtaTeGrides[i].txtKodi.toString().search('value') != -1)
                        if (rreshti.txtKodi == $('#txtKodi' + idRow).val())
                            counter++;
                    if (rreshtaTeGrides[i].txtKodi == rreshti.txtKodi && rreshtaTeGrides[i].txtKodi != "") {
                        counter++;
                    }
                }
                if (counter > 1) {
                    myMesazh.ShtoMesazhGabimi(hfState.Get("msgLupaKFEkzistonKyKFNeGride"));
                    ekziston = true;
                    resetRreshtKorent(index);
                    return;
                }
            }
        }
        if (ekziston)
            return;
        var kodi = rreshti.txtKodi;
        if (kodi != undefined && typeof(kodi) != "undefined" && kodi != "") {
            $.ajax({
                url: Utils.getServerApiUrl("Rregjistrime", "ktheKlientFurnitorVeprimeKFRow"),
                data: JSON.stringify({ kodi: kodi, rreshti: index, data: dteDtDok.GetDate() })
            }).done(SucceededCallbackVleraKodi);
            return;
        }
        vendosKf({ idRreshti: index, oKF: null, monedha: 0, kursi: 0 })
    }
}

/*
Function: fshiClicked

Fshin nje rresht te grides

Parameters:

index - Id e rreshtit qe do fshihet    
*/
function fshiClicked(index) {
    var grida = $("#rowed5");
    var ids = grida.getDataIDs();
    for (var i = 0; i < ids.length; i++) {
        if (ids[i] <= index)
            continue;
        if (grida.getTekstQelize('txtNrRendor', ids[i]) == undefined || typeof (grida.getTekstQelize('txtNrRendor', ids[i])) == 'undefined' || grida.getTekstQelize('txtNrRendor', ids[i]) == '')
            continue;
        grida.setTekstQelize('txtNrRendor', ids[i], parseInt(grida.getTekstQelize('txtNrRendor', ids[i])) - 1);
    }
    myJQGrid.fshiClicked(index, "#rowed5", inicializoGride);
    vendosTotal();
}

/*
Function: callWebserviceKodi
    
Sugjeron listen e klient furnitoreve kur shkruajme te kodi.
Shiko funksionin <SucceededCallbackKodi>.
*/
function callWebserviceKodi() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var vlera = $('#txtKodi' + idRresht).val();
    if (vlera != undefined && vlera != "") {
        try {
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "ktheArrayKlienteFurnitoresh"),
                data: JSON.stringify({ infixText: vlera, tipKlientFurnitor: 0 })
            }).done(SucceededCallbackKodi);
        } catch (e) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
        }
    }
}
/*
Function: callWebserviceLlog
    
Sugjeron listen e klient furnitoreve kur shkruajme te kodi.
Shiko funksionin <SucceededCallbackLlog>.
*/
function callWebserviceLlog() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var vlera = $('#txtLlogKunderparti' + idRresht).val();
    try {
        var hidField3 = $("#hfMeKF");
        if (hidField3.val() == "false") {
            $.ajax({   
                url: Utils.getServerApiUrl("Rregjistrime", "ktheACListeLlogarish"), data: JSON.stringify({ infixText: vlera, pershk: 1, idNdermarrje: hfState.Get('idNdermarrje'), idPerdoruesi: hfState.Get('idPerdoruesi') })
            }).done(SucceededCallbackLlog);
        }
        else {
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "ktheArrayKlienteFurnitoresh"),
                data: JSON.stringify({ infixText: vlera, tipKlientFurnitor: 0 })
            }).done(function (result) { 
                SucceededCallbackLlog(result);
            });
        }
    } catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
    }
}

/*
Function: SucceededCallbackKodi
    
Sugjeron listen e klient furnitorit kur shkruajme te kodi.
*/
function SucceededCallbackKodi(result) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    myJQGrid.SucceededCallbackKodi(result, '#txtKodi' + idRresht);
}  /*
        Function: SucceededCallbackLlog
    
        Sugjeron listen e klient furnitorit kur shkruajme te kodi.
        */
function SucceededCallbackLlog(result) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    myJQGrid.SucceededCallbackKodi(result, '#txtLlogKunderparti' + idRresht); //, changeFuncLlog);
}
var clsKlienti;
var mon;
var kursmon;
/*
Function: SucceededCallbackVleraKodi
    
mbush vlerat ne gride sipas klient furnitorit
*/
function SucceededCallbackVleraKodi(result) {//po
    vendosKf(result);
}

function vendosKf(kfMonKurs) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    var idRreshti = kfMonKurs.idRreshti;
    var clsKlientFurnitor = kfMonKurs.oKF;
    if (clsKlientFurnitor == null || clsKlientFurnitor == undefined || clsKlientFurnitor.IdKlientFurnitor < 1) {
        resetRreshtKorent(idRreshti);
        return;
    }

    var idKodi = "#txtIdKodi" + idRreshti;
    if ($(idKodi).val() == clsKlientFurnitor.IdKlientFurnitor)
        return; //eshte i njejti kf    
    var clsMonedha = kfMonKurs.monedha;
    var kursi;
    if ($('#hfShtimModifikim').val() != "modifikim") {
        kursi = gjejKursMonedheMeIdMon(clsMonedha.IdMonedha, kurse);
    }
    else
        kursi = kfMonKurs.kursi; //grida.getTekstQelize('txtKursi', lastsel2);

    var hffat = $("#hfMeFatura");
    var hidField3 = $("#hfMeKF");

    if (clsKlientFurnitor.AktivKF == false) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKlientFurnitoriNukEshteAktiv"));
        resetRreshtKorent(idRreshti);
        return;
    }

    var formatNumri = gjejFormatSipasMonedhes(clsMonedha.IdMonedha);
    var formatKursi = gjejFormatKursiSipasMonedhes(clsMonedha.IdMonedha);
    grida.setShifraPasPresjes('txtVlefta', formatNumri.ShifraPasPresjesVlefta, idRreshti);
    grida.setShifraPasPresjes('txtVlefteFature', formatNumri.ShifraPasPresjesVlefta, idRreshti);
    grida.setShifraPasPresjes('txtKursi', formatKursi, idRreshti);

    hf = $("input[id$='hfMonedhaNder']")[0];
    if (idRreshti == idRow && $('#txtKodi' + idRow).val() != undefined) {
        var ekziston = false;
        if (hffat.val() != 'true' && hidField3.val() == "false") {
            var rreshtaTeGrides = grida.getRowData();
            for (i = 0; i < rreshtaTeGrides.length; i++) {
                if (rreshtaTeGrides[i].txtKodi.toString().search('value') != -1)
                    continue;
                var editorkodi = rreshtaTeGrides[i].txtKodi;
                if (editorkodi != "" && editorkodi == clsKlientFurnitor.KodKlientFurnitor) {
                    myMesazh.ShtoMesazhGabimi(hfState.Get("msgLupaKFEkzistonKyKFNeGride"));
                    ekziston = true;
                    resetRreshtKorent(idRreshti);
                    return;
                }
            }
        }
        if (hidField3.val() == "true" && clsKlientFurnitor.KodKlientFurnitor == $('#txtLlogKunderparti' + idRreshti).val()) {
            myMesazh.ShtoMesazhGabimi('Nuk lejohet i njejti klient/furnitor ne te dyja fushat');
            resetRreshtKorent(idRreshti);
            return;
        }
        $(idKodi).val(clsKlientFurnitor.IdKlientFurnitor);
        //$("#cmbDebiKredi" + lastsel2 + " option:contains('" + debiKredi + "')").attr('selected', 'selected');
        var debiKredi;
        //if ($('#hfShtimModifikim').val() != "modifikim") {
        if (hffat.val() == 'true') {
            if (clsKlientFurnitor.LlojiKF == false)
                debiKredi = "Debi";
            else debiKredi = "Kredi";
        }
        else
            if (clsKlientFurnitor.LlojiKF == false)
                debiKredi = "Kredi";
            else
                debiKredi = "Debi";

        //}
        //$("#cmbDebiKredi" + idRreshti + " option:contains('" + debiKredi + "')").attr('selected', 'selected');
        grida.setTekstQelize('cmbDebiKredi', idRreshti, debiKredi, null, null, myelemComboDebiKredi);
        $('#txtKodi' + idRreshti).val(clsKlientFurnitor.KodKlientFurnitor);
        $('#txtEmertimi' + idRreshti).val(clsKlientFurnitor.EmertimiKF);
        $('#txtMonedha' + idRreshti).val(clsMonedha.KodiMonedha);
        if (kursi > 0) {
            grida.setTekstQelize('txtKursi', idRreshti, parseFloat(kursi));
            isKursNdryshe(parseFloat(kursi));
            kursifundit = kursi;
            kursmon = kursi;
        }
        if (hf.value == clsMonedha.KodiMonedha) {
            $('#txtKursi' + idRreshti)[0].disabled = true;
            $('#btntxtKursi' + idRreshti)[0].disabled = true;
        }
        else {
            $('#txtKursi' + idRreshti)[0].disabled = false;
            $('#btntxtKursi' + idRreshti)[0].disabled = false;
        }
        callWebServiceInfoKF();
        vendosVleftat("", idRreshti);

        if (parseFloat(kursi) == 1 && hfState.Get("idMonBazeNdermarrje") != kfMonKurs.monedha.IdMonedha)
            myMesazh.ShtoMesazhInformues(hfState.Get("msgKujdesKursiKembimitNje"));

        return;
    }
    var rreshti = grida.jqGrid('getRowData', idRreshti);
    if (rreshti.txtIdKodi == clsKlientFurnitor.IdKlientFurnitor)
        return; //eshte i njejti artikull 
    var ekziston = false;
    if (hffat.val() != 'true' && hidField3.val() == "false") {
        var rreshtaTeGrides = grida.jqGrid('getRowData');
        var ids = grida.jqGrid('getDataIDs');
        for (i = 0; i < rreshtaTeGrides.length; i++) {
            if (idRreshti == ids[i])
                continue;
            var editorkodi = rreshtaTeGrides[i].txtKodi;
            if (editorkodi != "" && editorkodi == clsKlientFurnitor.KodKlientFurnitor) {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgLupaKFEkzistonKyKFNeGride"));
                ekziston = true;
                resetRreshtKorent(idRreshti);
                return;
            }
        }
    }
    if (hidField3.val() == "true" && clsKlientFurnitor.KodKlientFurnitor == rreshti.txtLlogKunderparti) {
        myMesazh.ShtoMesazhGabimi('Nuk lejohet i njejti klient/furnitor ne te dyja fushat');
        resetRreshtKorent(idRreshti);
        return;
    }
    rreshti.cmbDebiKredi = debiKredi;
    rreshti.txtIdKodi = clsKlientFurnitor.IdKlientFurnitor
    rreshti.txtKodi = clsKlientFurnitor.KodKlientFurnitor;
    rreshti.txtEmertimi = clsKlientFurnitor.EmertimiKF;
    rreshti.txtMonedha = clsMonedha.KodiMonedha;
    if (kursi > 0) {
        rreshti.txtKursi = parseFloat(kursi);
        grida.setTekstQelize('txtKursi', idRreshti, parseFloat(kursi));
    }
    grida.formatoQelize('txtVlefta', idRreshti);
    grida.formatoQelize('txtKursi', idRreshti);
    grida.formatoQelize('txtVlefteFature', idRreshti);
    grida.jqGrid('setRowData', idRreshti, rreshti);
    callWebServiceInfoKF();
    vendosVleftat("", idRreshti);

    if (parseFloat(kursi) == 1 && hfState.Get("idMonBazeNdermarrje") != kfMonKurs.monedha.IdMonedha)
        myMesazh.ShtoMesazhInformues(hfState.Get("msgKujdesKursiKembimitNje"));
}

function plotesoEmrat(idRreshti) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    if (!idRreshti)
        idRreshti = idRow;
    var rreshtaTeGrides = grida.getRowData();
    for (i = parseInt(idRreshti) ; i < parseInt(idRreshti) + parseInt(kfTeshtuar) - 1; i++) {
        try {
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "ktheKlientFurnitorVeprimeKF"),
                data: JSON.stringify({ inFixText: rreshtaTeGrides[i].txtKodi, data: dteDtDok.GetDate() })
            }).done(SucceededCallbackVleraKodi2);
        } catch (e) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
        }
    }
    try {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheKlientFurnitorVeprimeKF"),
            data: JSON.stringify({ inFixText: $('#txtKodi' + idRow).val(), data: dteDtDok.GetDate() })
        }).done(SucceededCallbackVleraKodi2);
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
    }
}

function SucceededCallbackVleraKodi2(result, idRreshti) {
    var grida = $('#rowed5');
    if (result == null)
        return;
    
    clsKlientFurnitor = result[0];
    var debiKredi;
    var hffat = $("#hfMeFatura");
    if (clsKlientFurnitor.LlojiKF == false)
        debiKredi = "Kredi";
    else
        debiKredi = "Debi";
    if (hffat.val() == 'true') {
        if (clsKlientFurnitor.LlojiKF == false)
            debiKredi = "Debi";
        else debiKredi = "Kredi";
    }
    clsMonedha = result[1];
    kursi = result[2];
    hf = $("input[id$='hfMonedhaNder']")[0];
    var rreshtaTeGrides = grida.getRowData();
    var index = idRreshti;
    for (i = parseInt(index) ; i < parseInt(index) + parseInt(kfTeshtuar) ; i++) {
        //if (i != lastsel2 - 1) {
            var kodi = rreshtaTeGrides[i].txtKodi;
            if (kodi == clsKlientFurnitor.KodKlientFurnitor) {
                grida.setCell(i + 1, 'txtEmertimi', clsKlientFurnitor.EmertimiKF, 'clientArray', '');
                grida.setCell(i + 1, 'cmbDebiKredi', debiKredi, 'clientArray', '');
                grida.setCell(i + 1, 'txtMonedha', clsMonedha.KodiMonedha, 'clientArray', '');
                grida.setCell(i + 1, 'txtKursi', kursi, 'clientArray', '');
            //}
        }
    }
    if ($('#txtKodi' + idRreshti).val() == clsKlientFurnitor.KodKlientFurnitor) {
        //$("#cmbDebiKredi" + idRreshti + " option:contains('" + debiKredi + "')").attr('selected', 'selected');
        grida.setTekstQelize('cmbDebiKredi', idRreshti, debiKredi, null, null, myelemComboDebiKredi);

        $('#txtKodi' + idRreshti).val(clsKlientFurnitor.KodKlientFurnitor);
        $('#txtEmertimi' + idRreshti).val(clsKlientFurnitor.EmertimiKF);
        $('#txtMonedha' + idRreshti).val(clsMonedha.KodiMonedha);
        if (kursi > 0) {
            grida.setTekstQelize('txtKursi', idRreshti, parseFloat(kursi));
            isKursNdryshe(parseFloat(kursi));
            kursifundit = kursi;
            kursmon = kursi;
        }
        if (hf.value == clsMonedha.KodiMonedha) {
            $('#txtKursi' + idRreshti)[0].disabled = true;
            $('#btntxtKursi' + idRreshti)[0].disabled = true;
        }
        else {
            $('#txtKursi' + idRreshti)[0].disabled = false;
            $('#btntxtKursi' + idRreshti)[0].disabled = false;
        }
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
}



/*
Function: mbushGrideNgaHiddenFieldi

Merr te dhena nga hidden field-et dhe me to ploteson griden. Hidden field-et plotesohen ne server side kur behet modifikim dokumenti.
*/
function mbushGrideNgaHiddenFieldi() {
    var grida = $("#rowed5");
    var idRow = grida.getLastSel2();
    if ($('#hfShtimModifikim').val() == "shtim") {
        return;
    }
    if ($('#hfShtimModifikim').val() == "modifikim") {
        resetCountera();
        var colTrup = JSON.parse($('#HfColTrupBanka').val());
        var colKF = JSON.parse($('#HfColKF').val());
        var colKokaShitje = JSON.parse($('#HfColFatShitje').val());
        var colMonedha = JSON.parse($('#hfMonedha').val());
        var colLlog = JSON.parse($('#HfColLlog').val());
        var colKFKundra = JSON.parse($('#HfColKfKundra').val());
        arrNiv = JSON.parse($('#hfNivele').val());
        arrId = JSON.parse($('#hfId').val());
        var kodi, emertimi, pershkrimi, data, debikredi, vlefta, monedha, kursi, vleftamon, nrfatura, dtfatura, vlerafatura, vleramonfatura, llogkundr, emertimillog;
        grida.setLastSel2(1);
        idRow = 1;
        var disabled = false;

        if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1] == 'True' || lidhur)
            disabled = true;
        var be;
        if (colTrup !== "") {
          grida.jqGrid('clearGridData');
            var hffat = $("#hfMeFatura");
            var hidField3 = $("#hfMeKF");

            clsKlienti = colKF[0];
            mon = colMonedha[0].KodiMonedha;
            kursmon = colTrup[0].Kursi;
            for (var i = 0; i < colTrup.length; i++) {
                if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1] == 'True' || lidhur)
                    be = myJQGrid.myValueButtonFshi(true, idRow, "#rowed5");
                else
                    be = myJQGrid.myValueButtonFshi(false, idRow, "#rowed5");
                idKf = (colKF[i].IdKlientFurnitor == undefined) ? "" : colKF[i].IdKlientFurnitor;
                kodi = (colKF[i].KodKlientFurnitor == undefined) ? "" : colKF[i].KodKlientFurnitor;
                emertimi = (colKF[i].EmertimiKF == undefined) ? "" : colKF[i].EmertimiKF;
                pershkrimi = (colTrup[i].Pershkrimi == undefined) ? "" : colTrup[i].Pershkrimi;
                data = (colTrup[i].Data == undefined) ? "" : new Date(parseInt(colTrup[i].Data.replace('/Date(', '').replace(')/', ''))).format('dd/MM/yyyy');
                debikredi = (colTrup[i].DebiKredi == undefined) ? "" : colTrup[i].DebiKredi;
                vlefta = (colTrup[i].Vlefta == undefined || colTrup[i].Vlefta == "") ? "" : parseFloat(colTrup[i].Vlefta);
                monedha = (colMonedha[i].KodiMonedha == undefined) ? "" : colMonedha[i].KodiMonedha;
                kursi = (colTrup[i].Kursi == undefined || colTrup[i].Kursi == "") ? "" : parseFloat(colTrup[i].Kursi);
                vleftamon = (colTrup[i].VleftaMonBaze == undefined || colTrup[i].VleftaMonBaze == "") ? "" : parseFloat(colTrup[i].VleftaMonBaze);
                if (colTrup[i].IdFatura != 0) {
                    nrfatura = (colKokaShitje[i].NrDok == undefined) ? "" : colKokaShitje[i].NrDok;
                    dtfatura = (colKokaShitje[i].DtDok == undefined) ? "" : new Date(parseInt(colKokaShitje[i].DtDok.replace('/Date(', '').replace(')/', ''))).format('dd/MM/yyyy');
                    vlerafatura = (colKokaShitje[i].TotaliMeZbritjeMeTVSH == undefined || colKokaShitje[i].TotaliMeZbritjeMeTVSH == "") ? 1 : parseFloat(colKokaShitje[i].TotaliMeZbritjeMeTVSH);
                    vleramonfatura = (colKokaShitje[i].TotaliMeZbritjeMeTVSH == undefined || colKokaShitje[i].TotaliMeZbritjeMeTVSH == "") ? 1 : parseFloat(colKokaShitje[i].TotaliMeZbritjeMeTVSH * colKokaShitje[i].Kursi);
                }
                else {
                    nrfatura = "";
                    dtfatura = "";
                    vlerafatura = 1;
                    vleramonfatura = 1;
                }
                if (hidField3.val() == "true") {
                    llogkundr = (colKFKundra[i].KodKlientFurnitor == undefined) ? "" : colKFKundra[i].KodKlientFurnitor;
                    emertimillog = (colKFKundra[i].EmertimiKF == undefined) ? "" : colKFKundra[i].EmertimiKF;
                }
                else {
                    llogkundr = (colLlog[i].NrLlogari == undefined) ? "" : colLlog[i].NrLlogari;
                    emertimillog = (colLlog[i].EmerLlogari1 == undefined) ? "" : colLlog[i].EmerLlogari1;
                }
                if (debikredi == 2)
                    debikredi = 'Kredi';
                else if (debikredi == 1) debikredi = 'Debi';
                var formatNumri = gjejFormatSipasMonedhes(colMonedha[i].IdMonedha);
                var formatKursi = gjejFormatKursiSipasMonedhes(colMonedha[i].IdMonedha);
                grida.setShifraPasPresjes('txtVlefta', formatNumri.ShifraPasPresjesVlefta, idRow);
                grida.setShifraPasPresjes('txtVlefteFature', formatNumri.ShifraPasPresjesVlefta, idRow);
                grida.setShifraPasPresjes('txtKursi', formatKursi, idRow);

                var datarow = {
                    txtNrRendor: i + 1, txtIdKodi: idKf,
                    txtKodi: kodi, txtEmertimi: emertimi, txtPershkrim: pershkrimi, dteData: data,
                    cmbDebiKredi: debikredi, txtMonedha: monedha, txtNrFature: nrfatura, txtDtFature: dtfatura, txtLlogKunderparti: llogkundr, txtEmertimiLlog: emertimillog, txtFshi: be
                };
                var su = grida.addRowData(parseInt(idRow), datarow);

                grida.setTekstQelize('txtKursi', idRow, kursi);
                grida.setTekstQelize('txtVlefta', idRow, vlefta);
                grida.setTekstQelize('txtVleftaMon', idRow, vleftamon);
                grida.setTekstQelize('txtVlefteFature', idRow, vlerafatura);
                grida.setTekstQelize('txtVlefteMonFature', idRow, vleramonfatura);
                // faturanivel[lastsel2] = nivele[i].split(':')[1] + ";" + nivele[i].split(':')[2];
                idRow = idRow + 1;
                grida.setLastSel2(idRow);
            }
        }
        grida.setLastSel2(-1);
    }
    vendosTotal();
}

var arrKodi = new Array();
var arrEmertimi = new Array();
var arrPershkrimi = new Array();
var arrData = new Array();
var arrDebiKredi = new Array();
var arrVlefta = new Array();
var arrMonedha = new Array();
var arrKursi = new Array();
var arrVleftaMon = new Array();
var arrNrFatura = new Array();
var arrDtFatura = new Array();
var arrVleraFatura = new Array();
var arrVleraMonFatura = new Array();
var arrLlogKund = new Array();
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
var counter11 = 0;
var counter12 = 0;
var counter13 = 0;
var counter14 = 0;
var counter15 = 0;
var indeksi = -1;
var indexCounter;
var identifikuesPerPopupDokumentat;
var identikuesPerPopupKlientFurnitori;
var identikuesPerPopupLlogari;
function changeName() {
    myFaqeCelje.shtoHandlerSession();
    try {
        if (Utils.getUrlVar('id') == undefined)
            window.parent.callWebServiceKtheInfoLart('Shto_VeprimeKF.aspx', 0);
        else
            window.parent.callWebServiceKtheInfoLart('Shto_VeprimeKF.aspx', Utils.getUrlVar('id'));

    } catch (e) { }
    myCookies.createCookie('adresa', window.location.href, 1);
}
function validim(s, e) {
    myFaqeCelje.validim(s, e);
}
var dtDokumentit;
/*
Function: ValidoDateDokumenti

Kontrollon nese data e dokumentit i perket periudhes aktuale
*/
function ValidoDateDokumenti(data) {
    try {
        var vleraLabel = window.parent.document.getElementById("hfPeriudha").value;
        var periudha = vleraLabel.split("--");
        var dataDok = data;
        periudha1 = periudha[0].split("/");
        periudha2 = periudha[1].split("/");
        dtDokumentit = dataDok.split("/");
        if (periudha1[2] != dtDokumentit[2])
            return false;
        else {
            if ((dtDokumentit[1] < periudha1[1] || dtDokumentit[1] > periudha2[1]))
                return false;
        }
        return true;
    }
    catch (e) {
        return false;
    }
}

/*
Function: ButtonClickKerko
    
Hap lupen e dokumentave.
*/
function ButtonClickKerko(listUrl) {//po

    var queryString = {
        veprimi: 'VeprimeKF',
        listUrl: listUrl
    };
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhDokumentin"), 'LupaDokumenta.aspx?' + Utils.KonvertoObjectQueryString(queryString), 950, 560);
}

/*
Function: ButtonClickLlogaria
    
Hap lupen e dokumentave.
*/
function ButtonClickLlogaria() {
    var hfLlog = document.getElementById("hfLlogaria");
    var queryStr = hfLlog.value; identikuesPerPopupLlogari = "VeprimeKF";
    myButtonClickLupa.ButtonClickLlogaria(hfState.Get("popupAdministrimiUniversal"), queryStr, widthLupaLlogari, heightLupaLlogari);

}
//function ButtonClickKF() {
//    var hfLlog = document.getElementById("hfKF");
//    var queryStr = hfLlog.value; identikuesPerPopupLlogari = "VeprimeKF";
//    myButtonClickLupa.ButtonClickLlogaria(hfState.Get("popupAdministrimiUniversal"), queryStr, widthLupaLlogari, heightLupaLlogari);

//}

function Init() {
    if (typeof (isPostBack) == "undefined") {
        document.getElementById("kokeKonfigurimi").innerHTML = hfState.Get("MenuKokeDokumenti");
        document.getElementById("trupKonfigurimi").innerHTML = hfState.Get("MenuTrupDokumenti");
        document.getElementById("fundKonfigurimi").innerHTML = hfState.Get("MenuFundDokumenti");
        var hf = document.getElementById("hfKonffillestar");
        llogariezgjedhur.EmerLlogari1 = '';
        //   cmbKonfigurimi.SetText(hf.value);
        //  ndryshoKonfigurimin();

        identikuesPerPopupLlogari = "VeprimeKF";
        identifikuesPerPopupDokumentat = "Shto_VeprimeKF.aspx";
        changeName();
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(EndRequestHandler);
        myMesazh.shtoHandler();
        vendosDateDefault();
    }
    identikuesPerPopupKlientFurnitori = "VeprimeKF";
}

/*
Function: ndryshoKonfigurimin
    
Therret funksionin <callWebserviceKonfigurimi> per te vendosur nje konfigurim te ri.
*/
function ndryshoKonfigurimin() {
    var pershkKonfigAmb = cmbKonfigurimi.GetSelectedItem().GetColumnText("PershkrimKonfigAmbjente");
    if (pershkKonfigAmb != undefined)
       // lblKonfigurimi.SetText(pershkKonfigAmb);
        $('#kokeKonfigurimi').text(hfState.Get("MenuKokeDokumenti") + ': ' + pershkKonfigAmb);
    callWebserviceKonfigurimi(653, cmbKonfigurimi.GetText());
}
/*
Function: TextChangedLloji
    
Ben ndryshime ne gride ne varesi te llojit te veprimit te zgjedhur (hyrje, Dalje apo Transferim)
*/
function TextChangedLloji() {

    var mod;
    if ($("#hfShtimModifikim")[0].value == 'modifikim')
        mod = true;
    else mod = false;
    if (cmbLloji.GetText() != "")
        callWebserviceNiveliNew(cmbLloji.GetValue(), 'vkf', mod);
    else callWebserviceNiveliNew('', 'vkf', mod);
    grid_faturat.UnselectAllRowsOnPage();

}
/*
Function: callWebserviceNiveli
    
Therret funksionin <ktheTemplatetNivelit> per te marre temlaten e nivelit.
Shiko funksionin <SucceededCallbackNiveli>.
*/
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
    if ($("#hfShtimModifikim").val() == "shtim") {
        cmbKonfigurimi.ClearItems();
        for (i = 0; i < colModelet.length; i++) {
            cmbKonfigurimi.AddItem([colModelet[i].KodKonfigAmbjente, colModelet[i].PershkrimKonfigAmbjente], colModelet[i].IdKonfigAmbjente); //AddItem(teksti, vlera);
        }
        cmbKonfigurimi.SelectIndex(0);
    }
    ndryshoKonfigurimin();
}

/*
Function: callWebserviceKonfigurimi
    
Therret funksionin <ktheKonfigurim> per te vendosur konfigurimin per dokumentin.
Shiko funksionin <SucceededCallbackKonfigurimi>.
*/
function callWebserviceKonfigurimi(idKomp, kodKonf) {

        var idGjuha = hfState.Get('idGjuha');
        var idNdermarrje = hfState.Get('idNdermarrje');
        var idPerdoruesi = hfState.Get('idPerdoruesi');

        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Konfigurime", "ktheKonfigAmbjentiMeFormatNumrash"),
            data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, kodKontrolli: "", idObjekti: -1, shtim: true, merrFormatKursi: true, merrGjitheKonf: true, idGjuha: idGjuha, idPerdoruesi: idPerdoruesi, idNdermarrje: idNdermarrje })
        }).done(SucceededCallbackKonfig);

}

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}
var kushtet;
var colGrida;
var formatNumriZgjedhur;
var colKushte;
function SucceededCallbackKonfig(result) {
    var hf = $("input[id$='hfShtimModifikim']");
    var hfLidhur = $("input[id$='hfLidhur']");
    var arrTabela = ['tblFillim', "tblFund"];
    var arrPrind = ["dvFillim", "dvFundi"];
    var colKontrollet = result.colKontroll;  //[0];
    var colAtrTrupi = result.colAtrTrupi;  //[1];
    if (hf.val() == "shtim")
        pastroFushatKokes();

    var vlerat = myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, undefined, hf, '', arrTabela, undefined, undefined, hfLidhur, arrPrind);
    $("#divgride1").show();
    $("#dvFillim").show();
    $("#dvFundi").show();
    cmbKonfigurimi.ShowDropDown();
    cmbKonfigurimi.HideDropDown();
    var colKontrollet = result.colKontroll;  //[0];
    var colAtrTrupi = result.colAtrTrupi;   //[1];
    colGrida = result.colGrida;  //[2];
    colKushte = result.colKushte;  //[3];
    var colAlterKusht = result.colAlterKusht;  //[4];
    var kodniveli = result.kodniveli;  //[5];
    var konfLlojRreshti = result.konfLlojRreshti;  //[6];
    formatNumriZgjedhur = result.formatNumri;  //[7];
    var hidField2 = $("#hfMeFatura");
    var hidField2OldValue = hidField2.val();
    if (!($("#dvgrid_faturat").is(":visible"))) {
        hidField2OldValue = false;
    }
    hidField2.val(false);
    $('#HfGridCol').val(JSON.stringify(colGrida));
    var hfLlog = $("#hfLlogaria")[0];

    $('#hfKontrolletNrAutom').val(JSON.stringify(colKontrollet));
    $('#hfAtributeNrAutom').val(JSON.stringify(colAtrTrupi));
    if ($("input[id$='hfShtimModifikim']").val() == "shtim") {
        hfNrAuto.Clear();
        hfNrAutoShitje.Clear();
        vendosNrAutomatik(colAtrTrupi, colKontrollet);
    }    
    for (var i = 0; i < colKontrollet.length; i++) {

        //id e konfigurimit te lupes vendoset ne hidden field
        //vlera e saj do t'i kalohet si query string hapjes se popUp-it
        if (colKontrollet[i].KodKontrolli == "grid_faturat") {            
            if (colAtrTrupi[i].Visible == true)
                $("#dvgrid_faturat").show();
            else $("#dvgrid_faturat").hide();
        }
        if (colAtrTrupi[i].KodKontrolli == "cmbLlogariKunderParti")
            hfLlog.value = colKontrollet[i].IdKonfigAmbjenteLupa.toString(); //merret id e konfigurimit te lupes per lupen e magazines ne forme

        else if (colKontrollet[i].KodKontrolli == "cmbDegeAdministrative" && hf.val() != 'modifikim' && colAtrTrupi[i].VlereDefault != "" && colAtrTrupi[i].VlereDefault != "0") {
            kaVlereDefaultDegaAdmin = true;
            cmbDegeAdministrative.SetSelectedItem(cmbDegeAdministrative.FindItemByValue(colAtrTrupi[i].VlereDefault));
            TextChangedDega();
        }
    }
    var hidField1 = $("#hfKontabilizimi")[0];
    hidField1.value = 0;
    var hidField3 = $("#hfMeKF");
    hidField3.val(false);
    for (j = 0; j < colKushte.length; j++) {
        if (colKushte[j].Kodi == 'GJK') {
            if (colAlterKusht[j].Alternativa == 'Jo') {
                hidField1.value = 0;
            }
            else if (colAlterKusht[j].Alternativa == "Direkt")
                hidField1.value = 1;
            else hidField1.value = 2;
            continue;
        }
        if (colKushte[j].Kodi == 'MF') {
            if (colAlterKusht[j].Alternativa == 'Po') {
                hidField2.val(true);
            }
            else {
                hidField2.val(false);
                if ($("#dvgrid_faturat").is(":visible"))
                    $("#dvgrid_faturat").hide();
            }
            continue;
        }
        if (colKushte[j].Kodi == 'NDKF') {
            if (colAlterKusht[j].Alternativa == 'Po') {
                hidField3.val(true);
            }
            else {
                hidField3.val(false); TextChanged();
            }
            continue;
        }
        if (colKushte[j].Kodi == 'LLK') {
            llojkursi = colAlterKusht[j].Alternativa.substring(colAlterKusht[j].Alternativa.length - 1);
            continue;
        }
        if (colKushte[j].Kodi == 'ZIKF') {
            if (colKushte[j].Vlera != "0") {
                infoKf = true;
                idInfoKf = colKushte[j].Vlera;
                //hapMbyllInfo($('#hfHapurMbyllur').val() == 'True');
                //callWebServiceInfoKF();
            }
            else
                infoKf = false;
        }
    }

    if (hidField2OldValue != hidField2.val() && $("#dvgrid_faturat").is(":visible"))
        grid_faturat.PerformCallback(cmbKonfigurimi.GetText());
    hapMbyllInfo($('#hfHapurMbyllur').val() == 'True');
    MerrKurseSipasDates(true);
    formGridColsArray();
    var grida = $('#rowed5');
    grida.setLastSel2(-1);
    grida.GridUnload("rowed5");
    grida = $('#rowed5');
    ruajFormatetNeGride(grida, formatNumriZgjedhur);
    inicializoGride();
    Utils.konfiguroAccorditionPasKonfigDokumenti(varKonfig.identifikuesPerLocalStorageKey);
}

var pershk = 1;
var konfirmimArt = 2;
var njesiDef = 1;
var regjistrimKF = 1;
var gjendjeartminmax = 2;


function TextChangedDega() {
    if (cmbDegeAdministrative.GetSelectedItem() != null) {
        var dega = cmbDegeAdministrative.GetSelectedItem();
        if (dega != null)
            txtPershkrimDege.SetText(dega.GetColumnText('Pershkrimi'));
    }
}


/*
Function: ValueChangedPershkrimi
    
Vendos ne gride magazinen qe zgjidhet te koka
*/
function ValueChangedPershkrimi() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    pershkrimi = txtPershkrimi.GetText();

    var datagrid = $("#txtPershkrim" + idRresht)[0];
    if (datagrid != undefined) {
        datagrid.value = pershkrimi;
    }
    var indexe = grida.getDataIDs();

    var rreshtaTeGrides = grida.getRowData();
    for (i = 0; i < rreshtaTeGrides.length - 1; i++) {
        if (indexe[i] != idRresht) {
            rreshtaTeGrides[i].txtPershkrim = pershkrimi;
           grida.setCell(indexe[i], 'txtPershkrim', pershkrimi, 'clientArray', '');
        }
    }

}
/*
Function: DateChange
    
Vendos ne gride magazinen qe zgjidhet te koka
*/
function DateChange() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var hffat = $("#hfMeFatura");
    if (hffat.val() != 'true') {
        date = dteDtDok.GetText();
        var datagrid = $("#dteData" + idRresht);
        if (datagrid.val() != undefined) {
            datagrid.val(date);
        }
        var indexe = grida.getDataIDs();
        var rreshtaTeGrides = grida.getRowData();
        for (i = 0; i < rreshtaTeGrides.length; i++) {
            if (indexe[i] != idRresht) {
                rreshtaTeGrides[i].dteData = date;
                grida.setCell(indexe[i], 'dteData', date, 'clientArray', '');
            }
        }
        MerrKurseSipasDates(false);
    }
    var hfKontrollet = JSON.parse($('#hfKontrolletNrAutom').val());
    var atributet = JSON.parse($('#hfAtributeNrAutom').val());
    if ($('#hfShtimModifikim').val() != "modifikim")
        vendosNrAutomatik(atributet, hfKontrollet);
    callWebServiceInfoKF();
}

var mbushGriden = false;
function MerrKurseSipasDates(mbush) {
    mbushGriden = mbush;
    try {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "merrKursetMonedhaveDate"),
            data: JSON.stringify({ datedok: dteDtDok.GetDate(), llojKursi: llojkursi })
        }).done(SucceededCallbackKurse);
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
    }
}

var kurse;
function SucceededCallbackKurse(result) {
    kurse = result;
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
   grida.saveRow(idRresht, false, 'clientArray');
    grida.setLastSel2(0);
    var dataRow = grida.getRowData();
    var indexe = grida.getDataIDs();
    for (var i = 0; i < dataRow.length; i++) {
        var kursi = gjejKursMonedheMeKodMon(dataRow[i].txtMonedha, kurse);
        grida.setCell(indexe[i], 'txtKursi', kursi, 'clientArray', '', true);
    }
    if (mbushGriden)
        mbushGrideNgaHiddenFieldi();
}

function updateKurseMonedhe(idMonedha, kursi, kurse) {
    if (idMonedha == 0 || idMonedha == undefined)
        return false;
    for (var i = 0; i < kurse.length; i++) {
        if (kurse[i] != null) {
            if (kurse[i].idMonedha == idMonedha) {
                kurse[i].kursi = kursi;
                return true;
            }
        }
    }
    return false;
}

function updateKurseMonedheMeKod(kodMonedha, kursi, kurse) {
    if (kodMonedha == "" || kodMonedha == undefined)
        return false;
    for (var i = 0; i < kurse.length; i++) {
        if (kurse[i] != null) {
            if (kurse[i].kodMonedha == kodMonedha) {
                kurse[i].kursi = kursi;
                return true;
            }
        }
    }
    return false;
}

function gjejKursMonedheMeKodMon(kodMonedha, kurse) {
    if (kodMonedha == "" || kodMonedha == undefined)
        return 1;
    for (var i = 0; i < kurse.length; i++) {
        if (kurse[i] != null) {
            if (kurse[i].kodMonedha == kodMonedha) {
                return kurse[i].kursi;
            }
        }
    }
    return 1;
}

function gjejKursMonedheMeIdMon(idMonedha, kurse) {
    if (idMonedha == 0 || idMonedha == undefined)
        return 1;
    for (var i = 0; i < kurse.length; i++) {
        if (kurse[i] != null) {
            if (kurse[i].idMonedha == idMonedha) {
                return kurse[i].kursi;
            }
        }
    }
    return 1;
}
function ktheKodMonedheMeIdMon(idMonedha, kurse) {
    if (idMonedha == 0 || idMonedha == undefined)
        return 1;
    for (var i = 0; i < kurse.length; i++) {
        if (kurse[i] != null) {
            if (kurse[i].idMonedha == idMonedha) {
                return kurse[i].kodMonedha;
            }
        }
    }
    return "";
}


/*
Function: ButtonClickFurnitori
    
Hap lupen e klienteve/furnitoreve.
*/
function ButtonClickFurnitori() {
    var klientfurnitor = "";
    var hfKl = document.getElementById("hfLupaKlientFurnitor");
    var queryStr = hfKl.value;
    identikuesPerPopupKlientFurnitori = "VeprimeKFKrye";
    myButtonClickLupa.ButtonClickFurnitori(hfState.Get("headerZgjidhKlientFurnitorin"), queryStr, klientfurnitor, widthLupaKF, heightLupaKF);

}

var rreshtapafatura = false;
/*
Function: merrTeDhena
    
Merr te dhenat qe ka grida dhe i vendos neper hidden field-e per ti perdorur ne server side
*/
function merrTeDhena() {

    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    grida.jqGrid('saveRow', idRresht, false, 'clientArray');
    grida.setLastSel2(0);
    var kamag = true;
    trupiBosh = true;
    var total = 0;
    var idTe = grida.jqGrid('getDataIDs');
    var niv = new Array();
    var idss = new Array();
    dataJoNeRregull = false;
    rreshtapafatura = false;
    hffat = $("#hfMeFatura");
    var nivele = new Array();
    for (var i = 0; i < idTe.length; i++) {
        editorKodi = grida.getTekstQelize('txtKodi', idTe[i]);
        editorEmertimi = grida.getTekstQelize('txtEmertimi', idTe[i]);
        editorPershkrimi = grida.getTekstQelize('txtPershkrim', idTe[i]);
        editorData = grida.getTekstQelize('dteData', idTe[i]);
        editorDebiKredi = grida.getTekstQelize('cmbDebiKredi', idTe[i]);
        editorVlefta = grida.getTekstQelize('txtVlefta', idTe[i]);
        editorMonedha = grida.getTekstQelize('txtMonedha', idTe[i]);
        editorKursi = grida.getTekstQelize('txtKursi', idTe[i]);
        editorVleftaMon = grida.getTekstQelize('txtVleftaMon', idTe[i]);
        editorNrFatura = grida.getTekstQelize('txtNrFature', idTe[i]);
        editorDataFatura = grida.getTekstQelize('txtDtFature', idTe[i]);
        editorVleraFatura = grida.getTekstQelize('txtVlefteFature', idTe[i]);
        editorVleraMonFatura = grida.getTekstQelize('txtVlefteMonFature', idTe[i]);
        editorLlogKunder = grida.getTekstQelize('txtLlogKunderparti', idTe[i]);


        var eshteRreshtBosh = (editorKodi == "") && (editorEmertimi == "") && (editorPershkrimi == "") && (editorData == "") && (editorDebiKredi == "") && (editorVlefta == "") && (editorMonedha == "") && (editorVleftaMon == "") && (editorLlogKunder == "");

        if (!eshteRreshtBosh) {
            arrKodi[counter2] = i.toString() + ":" + editorKodi;
            if (editorKodi != "") trupiBosh = false;
            //if (editorMonedha == "")
            //    dataJoNeRregull = true;
            arrEmertimi[counter2] = i.toString() + ":" + editorEmertimi;
        }
        if (arrNiv[idTe[i]] != undefined) {
            niv[i] = arrNiv[idTe[i]];
            idss[i] = arrId[idTe[i]];
        }
        else {
            niv[i] = 0;
            idss[i] = 0;
        }
    }
    var tmp2 = grida.getRowData();
    for (var i = 0; i < tmp2.length; i++) {
        tmp2[i].txtFshi = "";
        if (tmp2[i].txtKodi == "") {
            tmp2.splice(i, 1);
            i--;
        }
    }

    var rreshtaTeGrides = grida.getTeDhenaRreshti();
    $('#gridDataObject').val(JSON.stringify(rreshtaTeGrides));
    unformatoFushaDevi();
    $('#hfNivele').val(JSON.stringify(niv));
    $('#hfId').val(JSON.stringify(idss));

}

/*
Function: pastro
    
Pastrin array-t e perdorura dhe vendos counterat ne 0.
*/
function pastro() {
    arrKodi = new Array();
    arrEmertimi = new Array();
    arrPershkrimi = new Array();
    arrData = new Array();
    arrDebiKredi = new Array();
    arrVlefta = new Array();
    arrMonedha = new Array();
    arrKursi = new Array();
    arrVleftaMon = new Array();
    arrNrFatura = new Array();
    arrDtFatura = new Array();
    arrVleraFatura = new Array();
    arrVleraMonFatura = new Array();
    arrLlogKund = new Array();

    resetCountera();
    var hidField2 = $("input[id$='hfKodi']")[0];
    var hidField3 = $("input[id$='hfEmertimi']")[0];
    var hidField4 = $("input[id$='hfPershkrimi']")[0];
    var hidField5 = $("input[id$='hfData']")[0];
    var hidField6 = $("input[id$='hfDebiKredi']")[0];
    var hidField7 = $("input[id$='hfVlefta']")[0];
    var hidField8 = $("input[id$='hfMonedha']")[0];
    var hidField9 = $("input[id$='hfKursi']")[0];
    var hidField10 = $("input[id$='hfVleftaMon']")[0];
    var hidField11 = $("input[id$='hfNrFatura']")[0];
    var hidField12 = $("input[id$='hfDataFatura']")[0];
    var hidField13 = $("input[id$='hfVleraFatura']")[0];
    var hidField14 = $("input[id$='hfVleramonFatura']")[0];
    var hidField15 = $("input[id$='hfLlogKunderparti']")[0];
    var hidField16 = $("input[id$='hfFatura']")[0];
    hidField2.value = "";
    hidField3.value = "";
    hidField4.value = "";
    hidField5.value = "";
    hidField6.value = "";
    hidField7.value = "";
    hidField8.value = "";
    hidField9.value = "";
    hidField10.value = "";
    hidField11.value = "";
    hidField12.value = "";
    hidField13.value = "";
    hidField14.value = "";
    hidField15.value = "";
    hidField16.value = "";
    var grida = $('#rowed5');
    grida.setLastSel2(0);
    arrNiv = new Array();
    arrId = new Array();
    ASPxMenu1.GetItemByName('PrintPreview').SetVisible(false);
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
    counter11 = 0;
    counter12 = 0;
    counter13 = 0;
    counter14 = 0;
    counter15 = 0;
}

/*
Function: ButtonClickKodi
    
Hap lupen e artikujve apo makrove sipas zgjedhjes qe eshte bere te kategoria.
*/
function ButtonClickKodi() {
    var hfKod = $("#hfGridaKodi");
    var queryStr = hfKod.val();
    identikuesPerPopupKlientFurnitori = "VeprimeKF";
    myButtonClickLupa.ButtonClickFurnitori(hfState.Get("headerZgjidhKlientFurnitorin"), queryStr, "", widthLupaKF, heightLupaKF);
}

/*
Function: ButtonClickLlogaria
    
Hap lupen e dokumentave.
*/
function ButtonClickLlog() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var hfLlog = document.getElementById("hfLlogaria");
    var queryStr = hfLlog.value;
    identikuesPerPopupLlogari = "VeprimeKFgrida";
    identikuesPerPopupKlientFurnitori = "VeprimeKFgrida";
    editorGlobal = $('#txtLlogKunderparti' + idRresht);
    var hfKod = $("#hfGridaKodi");
    var queryStr2 = hfKod.val();
    var hidField3 = $("#hfMeKF");

    if (hidField3.val() == "false")
        myButtonClickLupa.ButtonClickLlogaria(hfState.Get("popupAdministrimiUniversal"), queryStr, widthLupaLlogari, heightLupaLlogari);
    else myButtonClickLupa.ButtonClickFurnitori(hfState.Get("headerZgjidhKlientFurnitorin"), queryStr2, "", widthLupaKF, heightLupaKF);
}

/*
Function: vendosVleftat
    
Merr dhe validon vlerat e sasise dhe cmimit te caktuara ne gride dhe therret funksionin <vendosTotalet>
*/
function vendosVleftat(kontroll, idRreshti) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    if (idRreshti === undefined)
        idRreshti = idRow;
    var vlefta = grida.getTekstQelize('txtVlefta', idRreshti);
    if (idRreshti == idRow && vlefta !== undefined && grida.getTekstQelize('txtKodi', idRreshti) != undefined) {
        var vleftafat = grida.getTekstQelize('txtVlefteFature', idRow);
        var kursi = grida.getTekstQelize('txtKursi', idRow);
        if (kursi == "" || isNaN(kursi))
            kursi = 1;
        var tmpVlefta = vlefta;
        var tmpKursi = kursi;
        if (tmpVlefta == "")
            tmpVlefta = grida.getVlereDefault('txtVlefta');

        if (isNaN(tmpVlefta)) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgVleftaDuhetTeJeteNumer"));
            grida.setTekstQelize('txtVlefta', idRow);
            tmpVlefta = grida.getVlereDefault('txtVlefta');
        }
        if (vleftafat != "1" && parseFloat(tmpVlefta) > parseFloat(vleftafat)) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgShtoVeprimNdryshimiNukDuhetTeKalojeVleftenEFatures"));
            grida.setTekstQelize('txtVlefta', idRow);
            tmpVlefta = grida.getVlereDefault('txtVlefta');

        }
        if (tmpKursi == "")
            tmpKursi = grida.getVlereDefault('txtKursi');
        if (isNaN(tmpKursi)) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgKursiDuhetNumer"));
            kursi = grida.getVlereDefault('txtKursi');

        }

        var tmpVleftaMon = parseFloat(tmpVlefta * tmpKursi);
        grida.setTekstQelize('txtVleftaMon', idRow, tmpVleftaMon);
        vendosTotal(tmpVleftaMon);

        return;
    }
    var rreshti = grida.jqGrid('getRowData', idRreshti);
    var vlf = grida.getTekstQelize('txtVlefta', idRreshti);
    var vlfat = grida.getTekstQelize('txtVlefteFature', idRreshti);
    if (isNaN(vlf)) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgVleftaDuhetTeJeteNumer"));
        grida.setVlereDefault('txtVlefta');
    }

    else if (vlfat != "1" && parseFloat(vlf) > parseFloat(vlfat)) {
        // else if (rreshti.txtVlefteFature != "0.00" && parseFloat(rreshti.txtVlefta) > parseFloat(rreshti.txtVlefteFature)) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgShtoVeprimNdryshimiNukDuhetTeKalojeVleftenEFatures"));
        grida.setVlereDefault('txtVlefta');
    }
    var kurs = grida.getTekstQelize('txtKursi', idRreshti);
    if (isNaN(kurs)) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKursiDuhetNumer"));
        grida.setVlereDefault('txtKursi');
    }

    grida.setTekstQelize('txtVleftaMon', idRreshti, grida.getTekstQelize('txtVlefta', idRreshti) * grida.getTekstQelize('txtKursi', idRreshti))
    vendosTotal(grida.getTekstQelize('txtVleftaMon', idRreshti));
    return;
}

function vendosTotal(txtVleftaMon) {
    var total = 0;
    //vendoset magazina e zgjedhur te koka ne rreshtat e tjere te grides 
    var grida = $('#rowed5');
    var idTe = grida.jqGrid('getDataIDs');

    for (var i = 0; i < idTe.length; i++) {
        if (grida.getTekstQelize('txtKodi', idTe[i]).search('value') === -1) {
            if (grida.getTekstQelize('txtKodi', idTe[i]) != "") {
                total = total + parseFloat(grida.getTekstQelize('txtVleftaMon', idTe[i]));
            }
        }
        else {
            if (txtVleftaMon == null || txtVleftaMon == undefined) {
                var vlefta = grida.getTekstQelize('txtVleftaMon', idTe[i]);
                total = total + parseFloat(vlefta);
            }
            else
                total = total + parseFloat(txtVleftaMon);
        }
    }
    txtVlefta.SetText(total);
}

/*
Function: vendosVleftatMon

Merr dhe validon vlerat e sasise dhe cmimit te caktuara ne gride dhe therret funksionin <vendosTotalet>
*/
function vendosVleftatMon() {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    var vlefta = grida.getTekstQelize('txtVleftaMon', idRow);

    if (isNaN(vlefta)) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgVleftaDuhetTeJeteNumer"));
        vlefta = grida.getVlereDefault('txtVlefta');

    }
    var kursi = grida.getTekstQelize('txtKursi', idRow);
    if (isNaN(kursi)) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKursiDuhetNumer"));
        kursi = grida.getVlereDefault('txtKursi');
    }
    grida.setTekstQelize('txtVlefta', idRow, parseFloat(vlefta / kursi));

    vendosTotal();

}


/*
Function: pastroFushatKokes
    
Pastron fushat pasi eshte bere ruajtja apo modifikimi per te bere gati ambjentin per shtim.
*/
function pastroFushatKokes() {
    var faturanivel = new Array();
    txtNrDok.SetText('');
    cmbKFKunderParti.SetValue(null);;
    cmbDegeAdministrative.SetValue(null);
    cmbLlogariKunderParti.SetValue(null);
    txtPershkrimi.SetText('');
    var hf = document.getElementById("status1");
    hf.value = "false";
    //ndryshoKonfigurimin();
    nvFatura.CollapseAll();
    grid_faturat.UnselectAllRowsOnPage();
    grid_faturat.PerformCallback('pastro');
    clsKlienti = "";
    mon = "";
    kursmon = "";
    vendosDateDefault();
    pastroInfoKf();
    MerrKurseSipasDates(false);
}

function vendosDateDefault() {
    if ($("input[id$='hfShtimModifikim']").val() == "shtim") {
        try {
            var hfPeriudheObj = window.parent.lexoHfPeriudhe();
            dteDtDok.SetDate(Utils.ktheDateDefault(hfPeriudheObj));
            DateChange();
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
  if(  !Utils.nrWsRrugesManager.kanePerfunduarWs() ){
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgPoTransferohetTeDhenatShtypniPerseriRuaj"));
        return false;
    }
    if (txtNrDok.GetText() == "") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgShenoniNumrinEDokumentit"));
        return false;
    }
        //else if (cmbLlogariKunderParti.GetText() == "") {
        //    myMesazh.ShtoMesazhGabimi( hfState.Get("msgShtoVeprimShenoniLlogarineKundraparti"));
        //    return false;
        //}
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

/*
Function: EndRequestHandler
    
Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe array-t e perdorura.
Shiko funksionet <pastro>, <pastroFushatKokes> dhe <ndryshoKonfigurimin>.
*/
function EndRequestHandler(sender, args) {
    formatoFushaDevi();
    var hf = document.getElementById("status1");
    if ($('#hfqkmesazhi').val() == 'shfaqmesazh' || $('#hfqkmesazhiVDK').val() == 'shfaqmesazh') {
        //popMesazhQK.Show();
        if ($('#hfqkmesazhi').val() == 'shfaqmesazh') {
            $('#hfqkmesazhi').val('jo');
            //lblmesazhqendra.SetText(hfState.Get("msgShtoVeprimDoniTeBeniShperdrjenNeQKSeDokTeVeprimeveKF"));
            myMesazh.ShtoMesazh({ type: "confirm", layout: "center", modal: true, text: hfState.Get("msgShtoVeprimDoniTeBeniShperdrjenNeQKSeDokTeVeprimeveKF"), cancelClick: JopopupClick, okClick: hapPopUp });
        }
        else if ($('#hfqkmesazhiVDK').val() == 'shfaqmesazh') {
            //lblmesazhqendra.SetText(hfState.Get("msgDoTeBeshShperndarjenNeQKostoTeDifTeKursit"));
            myMesazh.ShtoMesazh({ type: "confirm", layout: "center", modal: true, text: hfState.Get("msgDoTeBeshShperndarjenNeQKostoTeDifTeKursit"), cancelClick: JopopupClick, okClick: hapPopUp });
        }
    }
    if ($('#hfqkmesazhi').val() == 'shfaqlupe') {
        myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgShperndarjeNeQendratEKostos"), $('#hfUrl').val(), 900, 600);
        $('#hfUrl').val('');
    }
    if (hf.value == "true") {
        myFaqeCelje.kontrolloTeDrejta('Shto_VeprimeKF.aspx?shtim_modifikim=shtim', true);
    }
    else
        click = false;
}

function JopopupClick(s, e) {
    if ($('#hfUrl').val() != '')
        $('#hfUrl').val('');

    else if ($('#hfUrlVDK').val() != '')
        $('#hfUrlVDK').val($('#hfUrlVDK').val().replace($('#hfUrlVDK').val().split(';')[0] + ";", ""));
    if ($('#hfqkmesazhiVDK').val() == 'shfaqmesazh' && $('#hfUrlVDK').val() != '') {
        //popMesazhQK.Show();
        //lblmesazhqendra.SetText(hfState.Get("msgDoTeBeshShperndarjenNeQKostoTeDifTeKursit"));
        myMesazh.ShtoMesazh({ type: "confirm", layout: "center", modal: true, text: hfState.Get("msgDoTeBeshShperndarjenNeQKostoTeDifTeKursit"), cancelClick: JopopupClick, okClick: hapPopUp });
    }
    else if ($('#hfqkmesazhiVDK').val() == 'shfaqlupe') {
        if ($('#hfUrlVDK').val() != '') {
            myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgShperndarjeNeQendratEKostos"), $('#hfUrlVDK').val().split(';')[0], 900, 600);
            $('#hfUrlVDK').val($('#hfUrlVDK').val().replace($('#hfUrlVDK').val().split(';')[0] + ";", ""));
            if ($('#hfUrlVDK').val() == '')
                $('#hfqkmesazhiVDK').val('jo');
        }
    }
}

function closePopup(s, e) {
    popupUniversal.SetContentUrl('');
    if ($('#hfqkmesazhiVDK').val() == 'shfaqmesazh' && $('#hfUrlVDK').val() != '') {
        //popMesazhQK.Show();
        //lblmesazhqendra.SetText(hfState.Get("msgDoTeBeshShperndarjenNeQKostoTeDifTeKursit"));
        myMesazh.ShtoMesazh({ type: "confirm", layout: "center", modal: true, text: hfState.Get("msgDoTeBeshShperndarjenNeQKostoTeDifTeKursit"), cancelClick: JopopupClick, okClick: hapPopUp });
    }
    else if ($('#hfqkmesazhiVDK').val() == 'shfaqlupe') {
        if ($('#hfUrlVDK').val() != '') {
            myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgShperndarjeNeQendratEKostos"), $('#hfUrlVDK').val().split(';')[0], 900, 600);
            $('#hfUrlVDK').val($('#hfUrlVDK').val().replace($('#hfUrlVDK').val().split(';')[0] + ";", ""));
            if ($('#hfUrlVDK').val() == '') $('#hfqkmesazhiVDK').val('jo');
        }
    }
}

function hapPopUp(s, e) {
    if ($('#hfUrl').val() != '') {
        myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgShperndarjeNeQendratEKostos"), $('#hfUrl').val(), 900, 600);
        $('#hfUrl').val('');
    }
    else if ($('#hfUrlVDK').val() != '') {
        myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgShperndarjeNeQendratEKostos"), $('#hfUrlVDK').val().split(';')[0], 900, 600);
        $('#hfUrlVDK').val($('#hfUrlVDK').val().replace($('#hfUrlVDK').val().split(';')[0] + ";", "")); if ($('#hfUrlVDK').val() == '') $('#hfqkmesazhiVDK').val('jo');
    }
}

/*
Function: menuClick

Percakton veprimin qe kryhet nese klikohet nje nga butonat e menuse.
*/
function menu_click(s, e) {
    myJQGrid.menuClick(s, e, "VeprimeKF.aspx", 'Shto_VeprimeKF.aspx?shtim_modifikim=shtim');
}

function Kthehu(mesazh) {
    myFaqeCelje.kontrolloTeDrejta("VeprimeKF.aspx?ruaj=po");
}

var click = false;
/*
Function: RuajClick

Validon dokumentin dhe therret funksionin <merrTeDhena> per te marre te dhenat e grides kur klikohet butoni Ruaj.
*/

function RuajClick(s, e) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if (click) {
        e.processOnServer = false; Utils.hiqLoadingGif();;
    }
    else {

        click = true;
        if (isValidKoka()) {
            grida.saveRow(idRresht, false, 'clientArray');
            merrTeDhena();
            grida.setLastSel2(0);
            if (dataJoNeRregull == true) {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgRreshtaTePavlefshemNeGride"));
                e.processOnServer = false;
                click = false;
            }
            if (rreshtapafatura == true) {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgShtoVeprimKaRreshtaPaFaturaNeGride"));
                e.processOnServer = false;
                click = false;
            }
            if (trupiBosh == true) {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgTrupiDokumentitNukDuhetLeneBosh"));
                e.processOnServer = false;
                click = false;
            }

        }
        else { e.processOnServer = false; click = false; Utils.hiqLoadingGif();; }
    }
}

/*
Function: PastroClick

Pastron koken e dokumentit dhe array-t e perdorura deh inicializon griden.
Shiko funksionet <pastro>, <pastroFushatKokes> dhe <inicializoGride>.
*/
function PastroClick() {
    $("input[id$='hfAutorizimi']").val(true);
    pastro();
    ASPxMenu1.GetItemByName('FletaKontabel').SetVisible(false);
    $('#ASPxSplitter1_hl').empty();
    ASPxMenu1.GetItemByName('Fshi').SetVisible(false);
    ASPxMenu1.GetItemByName('QendraKosto').SetVisible(false);
    ASPxMenu1.GetItemByName('Draft').SetVisible(true);
    faturanivel = new Array();
    var hf = $("#hfShtimModifikim");
    hf.val("shtim");
    $("input[id$='hfLidhur']")[0].value = 'False';
    ndryshoKonfigurimin();
    myMenu.menuSipasTeDrejtaRegjistrim(hf, hfTeDrejta);

    click = false;
    //    jQuery("#rowed5").GridUnload("rowed5");
    //    inicializoGride();
}

function ndryshoImazhin(nr, index) {
    myJQGrid.ndryshoImazhin(nr, index);
}


function valueChangedPeriudha() {
    var vleraLabel = lblPeriudhaAktuale.GetText();
    var periudha = vleraLabel.split("--");
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
/// vendos llogarine e zgjedhur tek koka tek grida
function TextChangedLlogaria(llogaria) {
    var llog = cmbLlogariKunderParti.GetText();
    var grida = $('#rowed5');
    //vendoset llogaria e zgjedhur te koka ne rreshtat e grides       
    var ids = grida.getDataIDs();
    for (i = 0; i < ids.length; i++) {
        grida.setTekstQelize('txtLlogKunderparti', ids[i], llogaria.NrLlogari);
        grida.setTekstQelize('txtEmertimiLlog', ids[i], llogaria.EmerLlogari1);
    }

}
function TextChangedKFur() {
    var llog = (cmbKFKunderParti.GetSelectedItem() != null) ? cmbKFKunderParti.GetSelectedItem().GetColumnText('KodKlientFurnitor') : '';;
    var lloge = (cmbKFKunderParti.GetSelectedItem() != null) ? cmbKFKunderParti.GetSelectedItem().GetColumnText('EmertimiKF') : '';
    var grida = $('#rowed5');
    //vendoset llogaria e zgjedhur te koka ne rreshtat e grides       
    var ids = grida.getDataIDs();
    for (i = 0; i < ids.length; i++) {
        grida.setTekstQelize('txtLlogKunderparti', ids[i], llog);
        grida.setTekstQelize('txtEmertimiLlog', ids[i], lloge);
    }

}
function TextChanged(s, e) {
    var s = cmbLlogariKunderParti.GetText().split(';');
    cmbLlogariKunderParti.SetText(s[0]);
    if (s[0] != '') {
        try {
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("ListPagesa", "ktheVleraLlogMeKod"),
                data: JSON.stringify({ kodi: s[0], idNderrmarje: hfState.Get('idNdermarrje') })
            }).done(SucceededCallbackLlogariKunder);
        } catch (e) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
        }
    }
}
function TextChangedKF(s, e) {
    var s = cmbKFKunderParti.GetText();

    if (s != '') {
        try {
           $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Celje", "ktheKFSipasKodit"), data: JSON.stringify({ kodi: s[0], idNdermarrje: hfState.Get('idNdermarrje') })
            }).done(SucceededCallbackKFKunder);
        } catch (e) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
        }
    }
}
/*
Function: SelectionChangedGridFaturat

Kur ndryshojme selection-in e grides se faturave.
*/
function SelectionChangedGridFaturat(s, e) {
    if (e.visibleIndex != -1)
        grid_faturat.GetRowValues(e.visibleIndex, 'IdDokumenti;IdKlientFurnitori;Vlefta;NrDokumenti;DtDokumenti;IdKushtPagese;Kursi;IdNiveli;IdMonedha;KodKlientFurnitor;EmertimiKf;LlojiKf', function (result) { OnGridFaturatSelectionComplete(result, e.visibleIndex) });
}

var arrNiv = new Array();
var arrId = new Array();
function OnGridFaturatSelectionComplete(values,rowind) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    var index2 = parseInt(idRow);
 
    if (grid_faturat._isRowSelected(rowind)) {
        var fatura = values[3];
        var data = formatDate(values[4], 'dd/MM/yyyy');
        var vlera = values[2];
        var kursi = values[6];
        var idKF = values[1];
        var kodKF = values[9];
        var pershkKF = values[10];
        var llojiKF = values[11];
        var idMonedha = values[8];
        var ids = grida.getDataIDs();
        var max = 0;
        for (i = 0; i < ids.length; i++) {
            max = Math.max(max, ids[i]);
        }
        // if (!eshteShtuarFatura(values[0], values[7])) {
        var formatNumri = gjejFormatSipasMonedhes(idMonedha);
        var formatKursi = gjejFormatKursiSipasMonedhes(idMonedha);

        var rreshtiBosh = -1;
        for (var i = 0; i < ids.length; i++) {
            if (grida.getTekstQelize('txtNrFature', ids[i]) === "") {
                rreshtiBosh = i;
                break;
            }
        }
        if (rreshtiBosh == -1) {
            max = 0; //ishte redeklaruar
            for (i = 0; i < ids.length; i++) {
                max = Math.max(max, ids[i]);
            }
            var rreshtiFundit = grida.getGridParam('reccount');
            var id = parseInt(max) + 1;
            max = id;
            if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1] == 'True')
                be = "<input id='butonFshi" + id + "' type='image' disabled='disabled' value='Fshi' onBlur = 'lostFocusKoloneFundit()' onmouseover='ndryshoImazhin(1," + id + ")' onmouseout='ndryshoImazhin(0," + id + ")'  src='images/square-icon.png' onclick='fshiClicked(" + id + ")'/>";
            else
                be = "<input id='butonFshi" + id + "' type='image' value='Fshi' onBlur = 'lostFocusKoloneFundit()'  onmouseover='ndryshoImazhin(1," + id + ")' onmouseout='ndryshoImazhin(0," + id + ")'  src='images/square-icon.png' onclick='fshiClicked(" + id + ")'/>";
            var datarow = { txtFshi: be };
            var su = grida.addRowData(id, datarow);
            rreshtiBosh = i;
        }
        ids = grida.getDataIDs();
        var idx = ids[rreshtiBosh];
        grida.setShifraPasPresjes('txtVlefta', formatNumri.ShifraPasPresjesVlefta, idx);
        grida.setShifraPasPresjes('txtVlefteFature', formatNumri.ShifraPasPresjesVlefta, idx);
        grida.setShifraPasPresjes('txtKursi', formatKursi, idx);
        grida.setTekstQelize('txtIdKodi', idx, idKF);
        grida.setTekstQelize('txtKodi', idx, kodKF);
        grida.setTekstQelize('txtEmertimi', idx, pershkKF);
        grida.setTekstQelize('txtKursi', idx, gjejKursMonedheMeIdMon(idMonedha, kurse));
        var kodiMon = ktheKodMonedheMeIdMon(idMonedha, kurse);
        var hf = $("input[id$='hfMonedhaNder']");
        if (hf.val() == kodiMon) {
            $('#txtKursi' + idx).attr('disabled', 'disabled');
            $('#btntxtKursi' + idx).attr('disabled', 'disabled');
        }
        grida.setTekstQelize('txtNrRendor', idx, grida.getInd(idx, false));
        grida.setTekstQelize('txtMonedha', idx, kodiMon);
        grida.setTekstQelize('txtDtFature', idx, data);
        grida.setTekstQelize('txtVlefteFature', idx, parseFloat(vlera));
        grida.setTekstQelize('txtVlefteMonFature', idx, parseFloat(vlera * kursi));
        grida.setTekstQelize('txtNrFature', idx, fatura);
       // var selektori = '#' + 'ASPxSplitter1_dteDtDok_I';
       // grida.setTekstQelize('dteData', idx, $(selektori).val());
        grida.setTekstQelize('dteData', idx, dteDtDok.GetText());
        if (llogariezgjedhur.NrLlogari != undefined) {
            grida.setTekstQelize('txtLlogKunderparti', idx, llogariezgjedhur.NrLlogari);
            grida.setTekstQelize('txtEmertimiLlog', idx, llogariezgjedhur.EmerLlogari1);
        }
        var debiKredi1;
        if (llojiKF == true)
            debiKredi1 = 'Kredi';
        else
            debiKredi1 = 'Debi';
        //$("#cmbDebiKredi" + idx + " option:contains('" + debiKredi1 + "')").attr('selected', 'selected');
        grida.setTekstQelize('cmbDebiKredi', idx, debiKredi1, null, null, myelemComboDebiKredi);
        grida.setTekstQelize('txtVlefta', idx);
        arrNiv[idx] = values[7];
        arrId[idx] = values[0];

        var rreshtiFundit = grida.getGridParam('reccount');
        if (rreshtiBosh + 1 == rreshtiFundit) {
            var id = parseInt(max) + 1;
            if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1] == 'True')
                be = "<input id='butonFshi" + id + "' type='image' disabled='disabled' value='Fshi' onBlur = 'lostFocusKoloneFundit()' onmouseover='ndryshoImazhin(1," + id + ")' onmouseout='ndryshoImazhin(0," + id + ")'  src='images/square-icon.png' onclick='fshiClicked(" + id + ")'/>";
            else
                be = "<input id='butonFshi" + id + "' type='image' value='Fshi' onBlur = 'lostFocusKoloneFundit()'  onmouseover='ndryshoImazhin(1," + id + ")' onmouseout='ndryshoImazhin(0," + id + ")'  src='images/square-icon.png' onclick='fshiClicked(" + id + ")'/>";
            var datarow = { txtFshi: be };
            var su = grida.addRowData(id, datarow);
        }
        vendosVleftat("", idx);
        //  }
    }
}

var faturanivel = new Array();

/*
Function: eshteShtuarFatura

Kontrollon nese fatura (qe kalohet si parameter) eshte shtuar me pare te grida e trupit apo jo.
*/
function eshteShtuarFatura(id, niv) {
    var grida = jQuery("#rowed5");

    var eshteShtuar = false;
    var rreshtaTeGrides = grida.getDataIDs();
    for (i = 0; i < rreshtaTeGrides.length; i++) {
        if (arrId[rreshtaTeGrides[i]] !== undefined)
            if (arrId[rreshtaTeGrides[i]] == id && arrNiv[rreshtaTeGrides[i]] == niv) {
                eshteShtuar = true;
                break;
            }
    }

    return eshteShtuar;
}

function hapPopUpRi(kf) {
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgShtoKF"), 'LupaKlientShpejte.aspx?vjenNga=Shto_VeprimeKF&kf=' + kf, 850, 600);
}

function ButtonClickKursi(s, e) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var idMonedha = $('#txtMonedha' + idRresht).val();
    if (idMonedha != 0 && idMonedha != null && idMonedha != -1 && idMonedha != '') {
        popupUniversal.SetHeaderText(hfState.Get("msgZgjidhKurs"));
        popupUniversal.SetContentUrl('LupaKursiShpejte.aspx?kodMonedha=' + idMonedha + '&llojKursi=' + llojkursi);
        popupUniversal.SetSize(widthLupaKursi, heightLupaKursi);
        popupUniversal.Show();
    }
}

function vendosNrAutomatik(colAtrTrupi, colKontrollet) {
    myNrAuto.vendosNrAutomatik(colAtrTrupi, colKontrollet, dteDtDok.GetDate());
}
function Close_Up(s, e) {
    if(popupUniversal.GetContentUrl().search('LupaKlientFurnitor.aspx')!=-1)
        plotesoEmrat();
    popupUniversal.SetContentUrl('');closePopup(s,e);
}

function nvFaturaClick(s, e) {
    grid_faturat.PerformCallback('mbush');
}

//#REGION INFO
//TODO PATI: Per t'u kaluar tek utils se jane njesoj tek te gjithe ambientet me info.
var mbyll = true;
function RuajHapurMbyllurplus(hapur) {
    var idPerdoruesi = hfState.Get('idPerdoruesi');
    mbyll = false;
    if (!hapur) {
        splitter.GetPane(1).CollapseBackward();
        $('#hfHapurMbyllur').val('False');
    }
    else
        $('#hfHapurMbyllur').val('True')
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ruajHapurMbyllurplus"), data: JSON.stringify({ hapur: hapur, idperdorues: idPerdoruesi, idperdoruesveprimi: idPerdoruesi })
    }).done(SuccededCallbackHapurMbyllur);
}

function RuajHapurMbyllurminus(hapur) {
    var idPerdoruesi = hfState.Get('idPerdoruesi');
    mbyll = false;
    if (!hapur) { splitter.GetPane(1).CollapseBackward(); $('#hfHapurMbyllur').val('False'); }
    else
        $('#hfHapurMbyllur').val('True')
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ruajHapurMbyllurminus"), data: JSON.stringify({ hapur: hapur, idperdorues: idPerdoruesi, idperdoruesveprimi: idPerdoruesi })
    }).done(SuccededCallbackHapurMbyllur);
}

function SuccededCallbackHapurMbyllur(result) {
    myMesazh.ShtoMesazhSuksesi(result);
}

function RuajHapurMbyllur(hapur) {
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
        url: Utils.getServerApiUrl("Rregjistrime", "ruajHapurMbyllur"), data: JSON.stringify({ hapur: hapur, idperdorues: idPerdoruesi, idperdoruesveprimi: idPerdoruesi })
    }).done(SuccededCallbackHapurMbyllur);
}

function KontrolloTeDrejta(s) {
    var alti = $(s.GetValue()).attr('alt');
    if (alti == "KF" && $('#hfTeDrejtaInfoKF').val() == 'False')
        s.SetEnabled(false);
}

function HeaderClick(s, e) {
    if (!mbyll) {
        e.cancel = true;
        mbyll = true;
    } //per rastet kur shtyp butonat + dhe -
}

function Expanded() {
    lbxKF.SetHeight(lbxKF.GetItemCount() * 22 + 28);
}

function Succedcallback(result) {
    lbxKF.ClearItems();
    for (var i = 0; i < result.length; i++) {
        lbxKF.AddItem([result[i].PershkrimKolone, ''], result[i].EmerKolone);
    }
    lbxKF.SetHeight(result.length * 22 + 28);
}

function ButtonClickNavBar(s) {
    var modinfo = -1;
    for (kus = 0; kus < colKushte.length; kus++) {
        if (colKushte[kus].Kodi == 'ZIKF')
            modinfo = colKushte[kus].Vlera;
    }
    if (s.GetText().split('alt="')[1].split('"')[0] == "KF") {
        myButtonClickLupa.LupaUniversal_Click('Zgjidhni konfigurimin e infos se klient/furnitorit', 'LupaKonfigurimInfo.aspx?id=' + idInfoKf + '&ruaj=po', 600, 500);
        return;
    }
}

//TODO PATI: Deri ketu jane njesoj

function callWebServiceInfoKF() {
    if (!eshteInfoHapur() || !infoKf)
        return;
    var grida = $("#rowed5");
    var idRresht = grida.getLastSel2();
    var idKodi = grida.getTekstQelize('txtIdKodi', idRresht);
    if (!idKodi)
        return;
    pastroInfoKf();
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheInfoKF"), data: JSON.stringify({ idja: idKodi, data: dteDtDok.GetDate(), idInfo: idInfoKf })
    }).done(SucceededCallbackInfoKF);
}

function pastroInfoKf() {
    navbar.GetGroupByName('KF').SetExpanded(false);
    lbxKF.ClearItems();
}

function SucceededCallbackInfoKF(result) {
    if (result.colInfoTrupi.length != 0) {
        navbar.GetGroupByName('KF').SetExpanded(true);
        vendosInfo(lbxKF, result);
    }
}

function vendosInfo(listBoxInfo, result) {
    listBoxInfo.BeginUpdate();
    listBoxInfo.ClearItems();
    if (result.vlerat === null) {
        for (var i = 0; i < result.colInfoTrupi.length; i++) {
            listBoxInfo.AddItem([result.colInfoTrupi[i].PershkrimKolone, ''], result.colInfoTrupi[i].EmerKolone);
        }
    }
    else {
        if (result.colInfoTrupi.length != result.vlerat.length) {
            myMesazh.ShtoMesazhGabimi('Ka nje gabim tek rezultati i infos!');
            listBoxInfo.EndUpdate();
            return;
        }
        for (var i = 0; i < result.colInfoTrupi.length; i++) {
            listBoxInfo.AddItem([result.colInfoTrupi[i].PershkrimKolone, result.vlerat[i].toString()], result.colInfoTrupi[i].EmerKolone);
        }
    }
    listBoxInfo.EndUpdate();
    refreshInfoLists();
}

function refreshInfoLists() {
    var kfExpanded = navbar.GetGroupByName('KF').GetExpanded();
    navbar.GetGroupByName('KF').SetExpanded(!kfExpanded);
    navbar.GetGroupByName('KF').SetExpanded(kfExpanded);
}

function eshteInfoHapur() {
    return !splitter.GetPane(1).IsCollapsed();
}

function spliterPaneExpanding(s, e) {
    hapMbyllInfo(true);   
    callWebServiceInfoKF();
}

function spliterPaneCollapsing(s, e) {
    hapMbyllInfo(false);
}

function hapMbyllInfo(infoExpanded) {
    if (infoExpanded)
        splitter.GetPane(1).SetSize(300);//madhesia default

    if (infoExpanded && infoKf) {
        if (!eshteInfoHapur())
            splitter.GetPane(1).Expand();
        if (infoKf)
            navbar.GetGroupByName('KF').SetVisible(true);
        else
            navbar.GetGroupByName('KF').SetVisible(false);
        navbar.GetGroupByName('KF').SetExpanded(false);
    }
    else if (!infoExpanded && eshteInfoHapur()) {
        if (infoKf)
            navbar.GetGroupByName('KF').SetVisible(true);
        else
            navbar.GetGroupByName('KF').SetVisible(false);

        navbar.GetGroupByName('KF').SetExpanded(false);
    }
    else {
        if (eshteInfoHapur())
            splitter.GetPane(1).CollapseBackward();
        navbar.GetGroupByName('KF').SetVisible(false);
    }
}

function spliterPaneResized(s, e) {
    if ($('#divgride2').width() != null) {
        myJQGrid.fixGridWidth($('#rowed5'), $('#divgride2'));
    }
}

//#ENDREGION INFO