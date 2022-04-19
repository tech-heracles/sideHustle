;
var formati = 5;
var lidhur = false;
var arrFormati = new Array();
var sourceAutocomplete = new Array();
var arrayMeMonedha = new Array();
var arrayIdKolonaGrides = new Array();
var arrayPershkrimiKolonaGrides = new Array();
var arrayVisibleKolonaGrides = new Array();
var arrayWidthKolonaGrides = new Array();
var arrayReadOnlyKolonaGrides = new Array();
var arrayRenditjeKolonaGrides = new Array();
var arrayIndexTrupi = new Array();
var identikuesPerPopupLlogari;
var identikuesPerPopupKlientFurnitori;
var widthLupaLlogari = 600;
var heightLupaLlogari = 600;
var widthLupaKF = 920;
var heightLupaKF = 600;
var widthLupaKerko = 850;
var heightLupaKerko = 600;

var diferenca = -1;
var kod = 0;
var identikuesPerPopupKursi = "ShtoAzhornimKf";
var llojkursi = 1; //perdoret per te ruajtur llojin e kursit te zgjedhur tek konfigurimi. Ne qofte se nuk ka asnje lloj te zgjedhur, atehere merret lloji i pare.
var widthLupaKursi = 950;
var heightLupaKursi = 560;


$(document).ready(function () {
    /*
Function: 
ekzekutohet sa here i behet resize faqes, dhe ben resize te grides
*/
    $(window).on('resize', function () {
        try {
            if (Utils.isGridResized())//if (!Utils.resizeSplitter(165))
                return;
        }
        catch (ee) {
        }
        var grida = jQuery("#rowed5");
        if ($('#divgride2').width() != null) {
            grida.setGridWidth($('#divgride2').width(), true);
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

function inicializoGride() {
    var classes = '';
    if (lidhur == true)
        classes = 'uigray';

    var arrayPershkrime = [arrayPershkrimiKolonaGrides[0], arrayPershkrimiKolonaGrides[1], arrayPershkrimiKolonaGrides[2], arrayPershkrimiKolonaGrides[3],
                           arrayPershkrimiKolonaGrides[4], arrayPershkrimiKolonaGrides[5], arrayPershkrimiKolonaGrides[6], arrayPershkrimiKolonaGrides[7], arrayPershkrimiKolonaGrides[8],
                            arrayPershkrimiKolonaGrides[9], arrayPershkrimiKolonaGrides[10], arrayPershkrimiKolonaGrides[11], arrayPershkrimiKolonaGrides[12], arrayPershkrimiKolonaGrides[13]];
    var arrayModel = [
        { name: arrayIdKolonaGrides[0], index: arrayIdKolonaGrides[0], width: arrayWidthKolonaGrides[0], hidden: arrayVisibleKolonaGrides[0], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemNrRendor, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[1], index: arrayIdKolonaGrides[1], width: arrayWidthKolonaGrides[1], hidden: arrayVisibleKolonaGrides[1], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myElemLloji, custom_value: myJQGrid.myValueCombo } },
        { name: arrayIdKolonaGrides[2], index: arrayIdKolonaGrides[2], width: arrayWidthKolonaGrides[2], hidden: arrayVisibleKolonaGrides[2], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myElemKodi, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[3], index: arrayIdKolonaGrides[3], width: arrayWidthKolonaGrides[3], hidden: arrayVisibleKolonaGrides[3], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myElemEmertimi, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[4], index: arrayIdKolonaGrides[4], width: arrayWidthKolonaGrides[4], hidden: arrayVisibleKolonaGrides[4], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myElemPershkrimi, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[5], index: arrayIdKolonaGrides[5], width: arrayWidthKolonaGrides[5], hidden: arrayVisibleKolonaGrides[5], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myElemMonedha, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[6], index: arrayIdKolonaGrides[6], width: arrayWidthKolonaGrides[6], hidden: arrayVisibleKolonaGrides[6], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myElemGjendja, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[7], index: arrayIdKolonaGrides[7], width: arrayWidthKolonaGrides[7], hidden: arrayVisibleKolonaGrides[7], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myElemGjendjaMon, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[8], index: arrayIdKolonaGrides[8], width: arrayWidthKolonaGrides[8], hidden: arrayVisibleKolonaGrides[8], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemKursi, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[9], index: arrayIdKolonaGrides[9], width: arrayWidthKolonaGrides[9], hidden: arrayVisibleKolonaGrides[9], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myElemGjendjaAktuale, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[10], index: arrayIdKolonaGrides[10], width: arrayWidthKolonaGrides[10], hidden: arrayVisibleKolonaGrides[10], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemComboDebiKredi, custom_value: myJQGrid.myValueCombo } },
        { name: arrayIdKolonaGrides[11], index: arrayIdKolonaGrides[11], width: arrayWidthKolonaGrides[11], hidden: arrayVisibleKolonaGrides[11], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myElemVlefta, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[12], index: arrayIdKolonaGrides[12], width: arrayWidthKolonaGrides[12], hidden: arrayVisibleKolonaGrides[12], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemLlog, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[13], index: arrayIdKolonaGrides[13], width: arrayWidthKolonaGrides[13], hidden: arrayVisibleKolonaGrides[13], classes: classes, sorttype: "int", editable: true, edittype: 'custom', editoptions: { custom_element: myElemButtonFshi, custom_value: myValueButtonFshi }, hidedlg: true }
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
        widthi: $('#divgride2')[0].offsetWidth,
        resetRreshtKorent: resetRreshtKorent,
        lostFocusKoloneFundit: lostFocusKoloneFundit,

        autocompleteList: [{ emerEditor: "txtKodi", selectFunc: selectFunc, changeFunc: changeFunc, shtoDataKod: false },
                          { emerEditor: 'txtLlogKunderparti', selectFunc: selectFunc2, changeFunc: changeFunc2, shtoDataKod: false }],
        konfigToolbar: {
            konfigGrid: $('#hfTeDrejtaKonfGride').val(),
            ruajKolonatEGrides: ruajKolonatEGrides
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

    var formatMOn = $.parseJSON(hfState.Get("formatKurset"));
    ndryshoKonfigFormatNumri(grida, formatNumri, formatKursi);
    vendosVleraDefaultNeGride(grida);
    vendosKonfigFormatNumri();
    return;
}
/*
Vendos vlerat default te fushave ne gride ne baze te emrit te kolones
*/
function vendosVleraDefaultNeGride(grida) {
    grida.setVlereDefault('txtVlefta', 0);
    grida.setVlereDefault('txtGjendja', 0);
    grida.setVlereDefault('txtGjendjaAkt', 0);
    grida.setVlereDefault('txtGjendjaMon', 0);
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
    if (typeof formatkursi == 'undefined')
        formatkursi = $.parseJSON(hfState.Get("formatKurset"));
    else
        hfState.Set("formatKurset", JSON.stringify(formatkursi));
    // var formatMOn = $.parseJSON(hfState.Get("formatKurset"));
    grida.setShifraPasPresjes('txtVlefta', formatNumri.KonfigTrupi[0].ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtGjendja', formatNumri.KonfigTrupi[0].ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtGjendjaAkt', formatNumri.KonfigTrupi[0].ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtGjendjaMon', formatNumri.KonfigTrupi[0].ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtKursi', formatkursi[0].IdFormatNrKursi);

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
        grida.formatoQelize('txtVlefta', idRreshti);
        grida.formatoQelize('txtGjendja', idRreshti);
        grida.formatoQelize('txtGjendjaAkt', idRreshti);
        grida.formatoQelize('txtGjendjaMon', idRreshti);
        grida.formatoQelize('txtKursi', idRreshti);
    } formatoFushaDevi();
}

function formatoFushaDevi() {

}

function unformatoFushaDevi() {

}

function ruajKolonatEGrides(grida) {
    var idGride = colGrida[0].IdKoka;
    var idGjuha = hfState.Get('idGjuha');
    var idNdermarrje = hfState.Get('idNdermarrje');
    var idPerdoruesi = hfState.Get('idPerdoruesi');
    var idViti = hfState.Get('idViti');
    myJQGrid.ruajKolonatEGrides(grida, idGride, idGjuha, idNdermarrje, idViti, idPerdoruesi)
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
Function: myElemLlog

Nderton nje textbox dhe nje buton per te zgjedhur llogarine

*/
function myElemLlog(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var hf = $("#hfShtimModifikim");
    disabled = arrayReadOnlyKolonaGrides[12];
    if ((Utils.getUrlVar('vep') == 'mbyllje' && $('#cmbDebiKredi' + idRresht).val() == 1)
        || (Utils.getUrlVar('vep') == 'azhornim' && $('#cmbDebiKredi' + idRresht).val() == 2 && (parseFloat(grida.getTekstQelize('txtVlefta', idRresht)) > 0))
        || (Utils.getUrlVar('vep') == 'azhornim' && $('#cmbDebiKredi' + idRresht).val() == 1 && (parseFloat(grida.getTekstQelize('txtVlefta', idRresht)) < 0))) {
       
        llog = llogariDebi_ButtonEdit.GetText(); 
    }
   
    else llog = llogariKredi_ButtonEdit.GetText();
    if (value == "" && llog != "" && llog != undefined)
        value = llog;
    //            if (hf.val() == "modifikim")
    //                disabled = 'True';
    return myJQGrid.myElemKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[12], ButtonClickLlog, keyPressLlog, changeFunc2); //'kontrollo',
}

function vendosDebi() {
    var grida = $('#rowed5');
    var ids = grida.getDataIDs();
    for (var i = 0; i < ids.length; i++) {


        if (grida.getTekstQelize('txtKodi', ids[i]) != "") {
            if ((Utils.getUrlVar('vep') == 'mbyllje' && grida.getTekstQelize('cmbDebiKredi', ids[i]) == "Debi")
                || (Utils.getUrlVar('vep') == 'azhornim' && grida.getTekstQelize('cmbDebiKredi', ids[i]) == "Kredi" && (parseFloat(grida.getTekstQelize('txtVlefta', ids[i])) > 0))
                || (Utils.getUrlVar('vep') == 'azhornim' && grida.getTekstQelize('cmbDebiKredi', ids[i]) == "Debi" && (parseFloat(grida.getTekstQelize('txtVlefta', ids[i])) < 0))
              ) {


                grida.setTekstQelize('txtLlogKunderparti', ids[i], llogariDebi_ButtonEdit.GetText());
            }

        }
    }

}

function VendosKredi() {

    var grida = $('#rowed5');
    var ids = grida.getDataIDs();
    for (var i = 0; i < ids.length; i++) {
        if (grida.getTekstQelize('txtKodi', ids[i]) != "") {
            if ((Utils.getUrlVar('vep') == 'mbyllje' && grida.getTekstQelize('cmbDebiKredi', ids[i]) == "Kredi")
                 || (Utils.getUrlVar('vep') == 'azhornim' && grida.getTekstQelize('cmbDebiKredi', ids[i]) == "Debi" && (parseFloat(grida.getTekstQelize('txtVlefta', ids[i])) > 0))
                 || (Utils.getUrlVar('vep') == 'azhornim' && grida.getTekstQelize('cmbDebiKredi', ids[i]) == "Kredi" && (parseFloat(grida.getTekstQelize('txtVlefta', ids[i])) < 0))
               ) {


                grida.setTekstQelize('txtLlogKunderparti', ids[i], llogariKredi_ButtonEdit.GetText());
            }

        }
    }
}
/*
Function: keyPressKodi

Shton nje rresht te ri ne gride nese jemi ne rreshtin e fundit dhe therret funksionin <callWebserviceKodi>.
*/
function keyPressLlog() {
    callWebserviceLlog();

}
/*
Function: callWebserviceLlog
    
Sugjeron listen e klient furnitoreve kur shkruajme te kodi.
Shiko funksionin <eeededCallbackLlog>.
*/
function callWebserviceLlog() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var vlera = grida.getTekstQelize('txtLlogKunderparti', idRresht);
    try {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheACListeLlogarish"), data: JSON.stringify({ infixText: vlera, pershk: 1, idNdermarrje: hfState.Get("idNdermarrje"), idPerdoruesi: hfState.Get("idPerdoruesi") })
        }).done(SucceededCallbackLlog);
    } catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
    }
}  /*Function: SucceededCallbackLlog
    
        Sugjeron listen e klient furnitorit kur shkruajme te kodi.
        */
function SucceededCallbackLlog(result) {
    var idRresht = $('#rowed5').getLastSel2();
    myJQGrid.SucceededCallbackKodi(result, '#txtLlogKunderparti' + idRresht); //, changeFuncLlog);
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
    var emerfushe = grida.getTekstQelize('txtLlogKunderparti', index);
    if (ui == null || ui.item == null) {
        if ($(emerfushe).val() != "") {
            KtheVleraLLogMeKod($(emerfushe).val());
            return;
        }
    }
}

function KtheVleraLLog(idja) {
    try {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("ListPagesa", "KtheVleraLlogMeID"),
            data: JSON.stringify({ idja: idja })
        }).done(SucceededCallbackLlogari);
    } catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
    }
}

function KtheVleraLLogMeKod(kodi) {
    try {
        if (kodi != undefined && kodi != undefined && kodi != "" && kodi != "")
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("ListPagesa", "ktheVleraLlogMeKod"),
            data: JSON.stringify({ kodi: kodi, idNderrmarje: hfState.Get("idNdermarrje") })
        }).done(SucceededCallbackLlogari);
    } catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
    }
}
function SucceededCallbackLlogari(artikulli) {
    var idRresht = $('#rowed5').getLastSel2();
    var kodi = grida.getTekstQelize('txtLlogKunderparti', idRresht);
    if (artikulli !== null && artikulli.IdLlogari !== -1) {
        $(kodi).val(artikulli.NrLlogari);

    } else {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgLlogNukEkziston"));
        $(kodi).val('');
    }

}
/*
Function: ButtonClickLlogaria
    
Hap lupen e dokumentave.
*/
function ButtonClickLlog() {
    var idRresht = $('#rowed5').getLastSel2();
    var hfLlog = document.getElementById("hfLlogaria");
    var queryStr = hfLlog.value;
    identikuesPerPopupLlogari = "VeprimeKFgrida";
    editorGlobal = grida.getTekstQelize('txtLlogKunderparti', idRresht);
    myButtonClickLupa.ButtonClickLlogaria('Zgjidh Llogarine', queryStr, widthLupaLlogari, heightLupaLlogari);

}
function formGridColsArray() {//po                       
    var hfGridKod = $('#hfGridaKodi');
    var IdKonfigAmbjenteLupat = [{ hfVar: hfGridKod, kodiText: "txtKodi" }];
    myJQGrid.formArrayKolGrides($("#HfGridCol"), arrayIdKolonaGrides, arrayPershkrimiKolonaGrides, arrayVisibleKolonaGrides, arrayReadOnlyKolonaGrides, lidhur, arrayWidthKolonaGrides, IdKonfigAmbjenteLupat, arrayRenditjeKolonaGrides);

}

function kursiFmatter(cellvalue, options, rowObject) {
    return myJQGrid.kursiFmatter(cellvalue, options, rowObject);
}
/*
Function: renditKolonatGrides

Therret metoden remapColumns te jqGrid per te renditur kolonat e grides sipas vlerave te array-t qe i kalohet kesaj metode si parameter
*/
function renditKolonatGrides() {
    myJQGrid.renditKolonatGrides("#rowed5", arrayRenditjeKolonaGrides);
}

/*
Function: myElemKodi

Nderton nje textbox dhe nje buton per te zgjedhur klient furnitorin

*/
function myElemKodi(value, options) {
    var idRresht = $('#rowed5').getLastSel2();
    var hf = $("#hfShtimModifikim");
    disabled = 'True';
    klientifundit = value;
    return myJQGrid.myElemKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[2], ButtonClickKodi, keyPressKodi); //'kontrollo',
}
/*
Function: keyPressKodi

Shton nje rresht te ri ne gride nese jemi ne rreshtin e fundit dhe therret funksionin <callWebserviceKodi>.
*/
function keyPressKodi() {
    callWebserviceKodi();
}

function myValueButtonFshi(elem, operation, value) {
    var idRresht = $('#rowed5').getLastSel2();
    if (arrayReadOnlyKolonaGrides[13] == 'True')
        return myJQGrid.myValueButtonFshi(true, idRresht, "#rowed5");
    else
        return myJQGrid.myValueButtonFshi(false, idRresht, "#rowed5");
}
/*
Function: myElemButon

Nderton nje buton per te fshire nje rresht te grides
*/
function myElemButtonFshi() {
    var idRresht = $('#rowed5').getLastSel2();
    callWebServiceInfoKF();
    if (arrayReadOnlyKolonaGrides[13] == 'True')
        return myJQGrid.myElemButtonFshi(true, idRresht, "#rowed5", lostFocusKoloneFundit);
    else
        return myJQGrid.myElemButtonFshi(false, idRresht, "#rowed5", lostFocusKoloneFundit);
}
/*
Function: myElemEmertimi

Nderton nje textbox ku vendoset emertimi i klient furnitorit
*/
function myElemEmertimi(value) {
    // lastsel2 = $("input[id$='hfId']")[0].value;
    var idRresht = $('#rowed5').getLastSel2();
    return myJQGrid.myElemEmertimi(value, arrayReadOnlyKolonaGrides[3], idRresht, 'txtEmertimi');

}

/*
Function: myElemPershkrimi

Nderton nje textbox ku vendoset pershkrimi
*/
function myElemPershkrimi(value) {
    var idRresht = $('#rowed5').getLastSel2();
    // lastsel2 = $("input[id$='hfId']")[0].value;
    if (value == "")
        value = txtPershkrimi.GetText();
    return myJQGrid.myElemEmertimi(value, arrayReadOnlyKolonaGrides[4], idRresht, 'txtPershkrim');

}
/*
Function: myelemComboDebiKredi

Nderton nje combobox ku vendoset debi kredi
*/
function myelemComboDebiKredi(value) {
    var disabled = false;
    var objTmp;
    var arrayOptions = new Array();
    arrayOptions.push({ value: 1, text: "Debi" });
    arrayOptions.push({ value: 2, text: "Kredi" });
    return myJQGrid.myElemCombo(value, arrayIdKolonaGrides[10], $("#rowed5").getLastSel2(), null, arrayOptions, arrayReadOnlyKolonaGrides[10] == 'True' || $("#hfMeFatura").val() == 'true');
}

/*
Function: myElemVlefta

Nderton nje textbox per te vendosur vleften.
*/
function myElemVlefta(value, options) {
    var grida = $("#rowed5");
    var idRresht = grida.getLastSel2();
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: arrayReadOnlyKolonaGrides[11], indexRow: idRresht, id: "txtVlefta" });
}
/*
Function: myElemGjendja

Nderton nje textbox per te vendosur vleften.
*/
function myElemGjendja(value, options) {
    var grida = $("#rowed5");
    var idRresht = grida.getLastSel2();
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: arrayReadOnlyKolonaGrides[6], indexRow: idRresht, id: "txtGjendja" });
}
/*
Function: myElemGjendjaMon

Nderton nje textbox per te vendosur vleften.
*/
function myElemGjendjaMon(value, options) {
    var grida = $("#rowed5");
    var idRresht = grida.getLastSel2();
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: arrayReadOnlyKolonaGrides[7], indexRow: idRresht, id: "txtGjendjaMon" });
}

/*
Function: myElemGjendjaAktuale

Nderton nje textbox per te vendosur vleften.
*/
function myElemGjendjaAktuale(value, options) {
    var grida = $("#rowed5");
    var idRresht = grida.getLastSel2();
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: arrayReadOnlyKolonaGrides[9], indexRow: idRresht, id: "txtGjendjaAkt" });
}

/*
Function: myElemMonedha

Nderton nje textbox ku vendoset monedha
*/
function myElemMonedha(value) {
    var grida = $("#rowed5");
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemEmertimi(value, arrayReadOnlyKolonaGrides[5], idRresht, 'txtMonedha');
}

/*
Function: myElemKursi

Nderton nje textbox per te vendosur kursin.
*/
function myElemKursi(value, options) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    return grida.myElemTextBoxFormatNumri({ value: value === "" ? "1" : value, options: options, disabled: $("input[id$='hfMonedhaNder']").val() == grida.getTekstQelize('txtMonedha', idRow) ? "True" : arrayReadOnlyKolonaGrides[8], indexRow: idRow, id: "txtKursi" });
}

/*
Function: myElemLloji

Nderton combo-n Lloji per griden. Combo ka vlerat: Llogari,  Furnitor, Klient
*/
function myElemLloji(value) {
    var disabled = false;
    var objTmp;
    var arrayOptions = new Array();
    arrayOptions.push({ value: "1", text: "Furnitor" });
    arrayOptions.push({ value: "2", text: "Klient" });    
    return myJQGrid.myElemCombo(value, 'txtLloji', $("#rowed5").getLastSel2(), null, arrayOptions, arrayReadOnlyKolonaGrides[1] == 'True');
}

function selectFunc(event, ui, emerfushe) {
    var grida = $("#rowed5");
    var idRresht = grida.getLastSel2();
    var ekziston = false;
    if (ui.item != null) {
        $(emerfushe).val(ui.item.label);
        var rreshtaTeGrides = grida.getRowData();
        for (i = 0; i < rreshtaTeGrides.length; i++) {
            if (rreshtaTeGrides[i].txtKodi.toString().search('value') != -1)
                continue;
            var editorkodi = rreshtaTeGrides[i].txtKodi;
            if (editorkodi == ui.item.label && editorkodi != "") {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgEkzistonkfGride"));
                ekziston = true;
                $(emerfushe).val("");
                if (klientifundit != "" && klientifundit != editorkodi) {

                    gvKF.PerformCallback('majtas:' + klientifundit);
                }
                klientifundit = "";
                break;
            }
        }
        if (!ekziston)
            $.ajax({
                url: Utils.getServerApiUrl("Rregjistrime", "ktheKlientFurnitorVeprimeKFMeIDRow"),
                data: JSON.stringify({ IDkf: ui.item.value, rreshti: idRresht, date: Data_DateEdit.GetDate(), llojKursi: llojkursi })
            }).done(SucceededCallbackVleraKodi);
    }
    return false;
}

/*
Function: kontrollo

Kontrollon nese nje artikull eshte zgjedhur me pare (ndodhet ne gride) apo jo.
*/
function changeFunc(event, ui, emerKodi, index) {
    grida = $("#rowed5");
    var idRresht = grida.getLastSel2();    
    if (index == idRresht) {
        if (ui == null || ui.item == null) {
            var emerfushe = grida.getTekstQelize('txtKodi', idRresht);
            var rreshtaTeGrides = grida.getRowData();
            for (i = 0; i < rreshtaTeGrides.length; i++) {
                if (rreshtaTeGrides[i].txtKodi.toString().search('value') != -1)
                    continue;
                var editorkodi = rreshtaTeGrides[i].txtKodi;
                if (editorkodi == emerfushe && editorkodi != "") {
                    myMesazh.ShtoMesazhGabimi(hfState.Get("msgEkzistonkfGride"));
                    grida.setTekstQelize('txtKodi', idRreshti, '');
                    grida.setTekstQelize('txtEmertimi', idRreshti, '');
                    grida.setTekstQelize('txtMonedha', idRreshti, '');
                    grida.setTekstQelize('txtKursi', idRreshti);
                    if (klientifundit != "" && klientifundit != editorkodi)
                        gvKF.PerformCallback('majtas:' + klientifundit);
                    klientifundit = "";
                    kursifundit = 1;
                    return;
                }
            }
            if (emerfushe != "") {
                $.ajax({
                    url: Utils.getServerApiUrl("Rregjistrime", "ktheKlientFurnitorVeprimeKFRow"),
                    data: JSON.stringify({ kodi: emerfushe, rreshti: index, data: Data_DateEdit.GetDate() })
                }).done(SucceededCallbackVleraKodi);
                return;
            }
            vendosKf({ idRreshti: index, oKF: null, monedha: 0, kursi: 0 });
            return;
        }
    }
    var rreshti = grida.jqGrid('getRowData', index);

    if (ui == null || ui.item == null) {
        if (rreshti.txtKodi != "") {
            $.ajax({
                url: Utils.getServerApiUrl("Rregjistrime", "ktheKlientFurnitorVeprimeKFRow"),
                data: JSON.stringify({ kodi: rreshti.txtKodi, rreshti: index, data: Data_DateEdit.GetDate() })
            }).done(SucceededCallbackVleraKodi);
            return;
        }
        vendosKf({ idRreshti: index, oKF: null, monedha: 0, kursi: 0 });
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
    if (grida.getTekstQelize('txtKodi', index) != undefined) {
        if (grida.getTekstQelize('txtKodi', index) != "")
            gvKF.PerformCallback('majtas:' + grida.getTekstQelize('txtKodi', index));
    }
    else {
        var rreshtaTeGrides = grida.getRowData(index);
        var editorkodi = rreshtaTeGrides.txtKodi;
        if (editorkodi != "")
            gvKF.PerformCallback('majtas:' + editorkodi);
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
}

/*
Function: callWebserviceKodi
    
Sugjeron listen e klient furnitoreve kur shkruajme te kodi.
Shiko funksionin <SucceededCallbackKodi>.
*/
function callWebserviceKodi() {
    var grida = $("#rowed5");
    var idRresht = grida.getLastSel2();
    var vlera = grida.getTekstQelize('txtKodi', idRresht);
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheArrayKlienteFurnitoresh"),
        data: JSON.stringify({ infixText: vlera, tipKlientFurnitor: 0 })
    }).done(SucceededCallbackKodi);
}

/*
Function: SucceededCallbackKodi
    
Sugjeron listen e klient furnitorit kur shkruajme te kodi.
*/
function SucceededCallbackKodi(result) {
    var grida = $("#rowed5");
    var idRresht = grida.getLastSel2();
    myJQGrid.SucceededCallbackKodi(result, '#txtKodi' + idRresht);
}

var klientifundit = "";
function resetRreshtKorent(idRreshti) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    if (idRreshti == idRow) {
        grida.setTekstQelize('txtKodi', idRreshti, '');
        grida.setTekstQelize('txtEmertimi', idRreshti, '');
        grida.setTekstQelize('txtMonedha', idRreshti, '');
        grida.setTekstQelize('txtKursi', idRreshti);
        LlogaritAzhornim();
        return;
    }
    grida.jqGrid('delRowData', idRreshti);
    return;
}

/*
Function: SucceededCallbackVleraKodi
    
mbush vlerat ne gride sipas klient furnitorit
*/
function SucceededCallbackVleraKodi(result) {//po
    vendosKf(result);
}

function vendosKf(kfMonKurs) {
    var idRreshti = kfMonKurs.idRreshti;
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    //  lastsel2 = $("input[id$='hfId']")[0].value;

    var clsKlientFurnitor = kfMonKurs.oKF;
    if (clsKlientFurnitor == null || clsKlientFurnitor == undefined || clsKlientFurnitor.IdKlientFurnitor < 1) {
        resetRreshtKorent(idRreshti);
        return;
    }
    grida.editRow(idRow);
    var kursi;
    var clsMonedha = kfMonKurs.monedha;
    kursi = kfMonKurs.kursi;
    var lloji;
    if (clsKlientFurnitor.IdKlientFurnitor <= 0)
        return;
    var ekziston = false;
    if ((Utils.getUrlVar('vep') == 'azhornim') && clsMonedha.KodiMonedha == $('#hfMonedhaNder').val()) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukZgjidhenkfMonBaze"));
        resetRreshtKorent(idRow);
        if (klientifundit != "" && klientifundit != clsKlientFurnitor.KodKlientFurnitor) {
            gvKF.PerformCallback('majtas:' + klientifundit);
        }
        klientifundit = "";
        return;
    }

    if (!clsKlientFurnitor.AktivKF) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKlientFurnitoriNukEshteAktiv"));
        resetRreshtKorent(idRow);
        //grida.setTekstQelize('txtKodi', idRreshti, '');
        //grida.setTekstQelize('txtEmertimi', idRreshti, '');
        //grida.setTekstQelize('txtMonedha', idRreshti, '');
        //grida.setTekstQelize('txtKursi', idRreshti);
        if (klientifundit != "" && klientifundit != clsKlientFurnitor.KodKlientFurnitor) {

            gvKF.PerformCallback('majtas:' + klientifundit);
        }
        klientifundit = "";
        return;
    }

    if (clsKlientFurnitor.DtAzhornimi >= Data_DateEdit.GetDate() || clsKlientFurnitor.DtLidhje >= Data_DateEdit.GetDate()) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgAzhornimPasDtDok"));
        resetRreshtKorent(idRow);
        //grida.setTekstQelize('txtKodi', idRreshti, '');
        //grida.setTekstQelize('txtEmertimi', idRreshti, '');
        //grida.setTekstQelize('txtMonedha', idRreshti, '');
        //grida.setTekstQelize('txtKursi', idRreshti);
        if (klientifundit != "" && klientifundit != clsKlientFurnitor.KodKlientFurnitor) {
            gvKF.PerformCallback('majtas:' + klientifundit);
        }
        klientifundit = "";
        return;
    }
    var rreshtaTeGrides = grida.jqGrid('getDataIDs');
    for (i = 0; i < rreshtaTeGrides.length; i++) {
        if (rreshtaTeGrides[i] == idRreshti)
            continue;
        var editorkodi = grida.getTekstQelize('txtKodi', rreshtaTeGrides[i]);
        if (editorkodi != "" && editorkodi == clsKlientFurnitor.KodKlientFurnitor) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgEkzistonkfGride"));
            ekziston = true;
            resetRreshtKorent(idRow);
            //grida.setTekstQelize('txtKodi', idRreshti, '');
            //grida.setTekstQelize('txtEmertimi', idRreshti, '');
            //grida.setTekstQelize('txtMonedha', idRreshti, '');
            //grida.setTekstQelize('txtKursi', idRreshti);
            kursifundit = 1;
            if (klientifundit != "" && klientifundit != clsKlientFurnitor.KodKlientFurnitor) {

                gvKF.PerformCallback('majtas:' + klientifundit);
            }
            klientifundit = "";
            return;
        }
    }

    var debiKredi;
    if (clsKlientFurnitor.LlojiKF == false) {
        debiKredi = "Kredi";
        lloji = 1;
    }
    else {
        debiKredi = "Debi";
        lloji = 2;
    }

    hf = $("input[id$='hfMonedhaNder']")[0];
    //$("#cmbDebiKredi" + lastsel2 + " option[text=" + debiKredi + "]").attr('checked', 'checked');
    $("#cmbDebiKredi" + idRreshti + " option:contains('" + debiKredi + "')").attr('selected', 'selected');
    grida.setTekstQelize('txtKodi', idRreshti, clsKlientFurnitor.KodKlientFurnitor);
    grida.setTekstQelize('txtEmertimi', idRreshti, clsKlientFurnitor.EmertimiKF);
    grida.setTekstQelize('txtMonedha', idRreshti, clsMonedha.PershkrimiMonedha);
    grida.setTekstQelize('txtLloji', idRreshti, lloji);

    if (kursi > 0) {
        grida.setTekstQelize('txtKursi', idRreshti, parseFloat(kursi));
        kursifundit = kursi;
    }
    var formatNumri = gjejFormatSipasMonedhes(clsMonedha.IdMonedha);
    var formatKursi = gjejFormatKursiSipasMonedhes(clsMonedha.IdMonedha);
    grida.setShifraPasPresjes('txtVlefta', formatNumri.ShifraPasPresjesVlefta, idRreshti);
    grida.setShifraPasPresjes('txtGjendjaMon', formatNumri.ShifraPasPresjesVlefta, idRreshti);
    grida.setShifraPasPresjes('txtGjendjaAkt', formatNumri.ShifraPasPresjesVlefta, idRreshti);
    grida.setShifraPasPresjes('txtGjendja', formatNumri.ShifraPasPresjesVlefta, idRreshti);
    grida.setShifraPasPresjes('txtKursi', formatKursi, idRreshti);
    grida.formatoQelize('txtKursi', idRreshti);
    gvKF.PerformCallback('hiq:' + clsKlientFurnitor.KodKlientFurnitor);
    if (klientifundit != "" && klientifundit != clsKlientFurnitor.KodKlientFurnitor) {
        gvKF.PerformCallback('majtas:' + klientifundit);
    }
    klientifundit = clsKlientFurnitor.KodKlientFurnitor;
    callWebServiceInfoKF();
    LlogaritAzhornim();
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
Function: ButtonClickKerko
    
Hap lupen e dokumentave.
*/
function ButtonClickKerko(listUrl) {

    var queryString = {
        veprimi: (Utils.getUrlVar('vep') == 'azhornim') ? 'AzhornimKF' : "MbylljeKF",
        listUrl: listUrl
    };
    popupUniversal.SetContentUrl('LupaDokumenta.aspx?' + Utils.KonvertoObjectQueryString(queryString));
    popupUniversal.SetHeaderText(hfState.Get("msgZgjidhDokumentin"));
    popupUniversal.SetSize(widthLupaKerko, heightLupaKerko);
    popupUniversal.Show();
}

function Init() {
    if (typeof (isPostBack) == "undefined") {
        var hf = document.getElementById("hfKonffillestar");
        countwebservice = 0;
        countsucceded = 0;
        cmbKonfigurimi.SetText(hf.value);
        ndryshoKonfigurimin();
        identifikuesPerPopupDokumentat = "Shto_AzhornimKlientFurnitor.aspx?vep=" + Utils.getUrlVar('vep');
        changeName();
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(EndRequestHandler);
        myMesazh.shtoHandler();
    }
}

/*
Function: ndryshoKonfigurimin
    
Therret funksionin <callWebserviceKonfigurimi> per te vendosur nje konfigurim te ri.
*/
function ndryshoKonfigurimin() {
    //var pershkKonfigAmb = cmbKonfigurimi.GetSelectedItem().GetColumnText("PershkrimKonfigAmbjente");
    var pershkKonfigAmb = cmbKonfigurimi.GetText();
    if (pershkKonfigAmb != undefined)
        lblKonfigurimi.SetText(pershkKonfigAmb);
    if (Utils.getUrlVar('vep') == 'azhornim')
        callWebserviceKonfigurimi(650, cmbKonfigurimi.GetText());
    else callWebserviceKonfigurimi(679, cmbKonfigurimi.GetText());
}

/*
Function: callWebserviceKonfigurimi
    
Therret funksionin <ktheKonfigurim> per te vendosur konfigurimin per dokumentin.
Shiko funksionin <SucceededCallbackKonfigurimi>.
*/
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

function ButtonClickLlogariKredite() {
    var hfLlog = document.getElementById("hfLlogaria");
    var queryStr = hfLlog.value;
    identikuesPerPopupLlogari = "AzhornimKlientFurnitoriLlogariKredi";
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("popupAdministrimiUniversal"), 'LupaLlogaria.aspx?idKonfigAmbjente=' + queryStr, 750, 600);

}

function ButtonClickLlogariDebi() {
    identikuesPerPopupLlogari = "AzhornimKlientFurnitoriLlogariDebi";
    var hfLlog = document.getElementById("hfLlogaria");
    var queryStr = hfLlog.value;

    myButtonClickLupa.LupaUniversal_Click(hfState.Get("popupAdministrimiUniversal"), 'LupaLlogaria.aspx?idKonfigAmbjente=' + queryStr, 750, 600);

}
function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}


function ButtonClickKlientFurnitor() {
    identikuesPerPopupKlientFurnitori = "AzhornimKlientFurnitori";
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhKF"), 'LupaKlientFurnitor.aspx', 800, 600);
}
var editorGlobal;
function ButtonClickKlientFurnitorNgaGrida() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    editorGlobal = $('#txtKodiKlientit' + idRresht)[0];
    identikuesPerPopupKlientFurnitori = "AzhornimKlientFurnitoriGrida";
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhKF"), 'LupaKlientFurnitor.aspx', 800, 600);
}

var kushtet;
var colGrida;
var formatNumriZgjedhur;
var colKushte;
function SucceededCallbackKonfig(result) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var hf = $("input[id$='hfShtimModifikim']");
    var hfLidhur = $("input[id$='hfLidhur']");
    var arrTabela = ['tblFillim', "tblFund"];
    var arrPrind = ["dvFillim", "dvFundi"]
    var colKontrollet = result.colKontroll;           //[0];
    var colAtrTrupi = result.colAtrTrupi;            //[1];
    var vlerat = myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, undefined, hf, '', arrTabela, undefined, undefined, hfLidhur, arrPrind);
    $("#divgride1").show();
    $("#dvFillim").show();
    $("#dvFundi").show();
    var colKontrollet = result.colKontroll;     //[0];
    var colAtrTrupi = result.colAtrTrupi;    //[1];
    colGrida = result.colGrida;     //[2];
    colKushte = result.colKushte;    //[3];
    var colAlterKusht = result.colAlterKusht;    //[4];
    var kodniveli = result.kodniveli;    //[5];
    var konfLlojRreshti = result.konfLlojRreshti;   //[6];
    formatNumriZgjedhur = result.formatNumri;    //[7];
    formatKursi = result.formatKursi;    //[8];
    $('#HfGridCol').val(JSON.stringify(colGrida));

    var hfLlog = $("#hfLlogaria")[0];
    for (var i = 0; i < colKontrollet.length; i++) {

        //id e konfigurimit te lupes vendoset ne hidden field
        //vlera e saj do t'i kalohet si query string hapjes se popUp-it

        if (colAtrTrupi[i].KodKontrolli == "llogariDebi_ButtonEdit")
            hfLlog.value = colKontrollet[i].IdKonfigAmbjenteLupa.toString(); //merret id e konfigurimit te lupes per lupen e magazines ne forme
    }
    var hidField1 = $("#hfKontabilizimi")[0];
    hidField1.value = 0;
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
        if (colKushte[j].Kodi == 'LLK') {
            llojkursi = colAlterKusht[j].Alternativa.substring(colAlterKusht[j].Alternativa.length - 1);
            continue;
        }
        if (colKushte[j].Kodi == 'ZIKF') {
            if (colKushte[j].Vlera != "0") {
                infoKf = true;
                idInfoKf = colKushte[j].Vlera;
                hapMbyllInfo($('#hfHapurMbyllur').val() == 'True');
                callWebServiceInfoKF();
            }
            else
                infoKf = false;
        }
    }

    formGridColsArray();
    grida.setLastSel2(-1);
    grida.GridUnload("rowed5");
    ruajFormatetNeGride(grida, formatNumriZgjedhur, formatKursi);
    inicializoGride();
    mbushGrideNgaHiddenFieldet();
    //  mbushGrideNgaHiddenFieldi();
}
/*
Function: ValueChangedPershkrimi
    
Vendos ne gride magazinen qe zgjidhet te koka
*/
function ValueChangedPershkrimi() {
    var grida = $("#rowed5");
    var idRresht = grida.getLastSel2();
    pershkrimi = txtPershkrimi.GetText();
    var datagrid = $("#txtPershkrim" + idRresht)[0];
    if (datagrid != undefined) {
        datagrid.value = pershkrimi;
    }

    var rreshtaTeGrides = grida.getRowData();
    for (i = 0; i < rreshtaTeGrides.length - 1; i++) {
        if (rreshtaTeGrides[i].txtKodi.search('value') === -1) {
            rreshtaTeGrides[i].txtPershkrim = pershkrimi;
            grida.setTekstQelize('txtPershkrim', rreshtaTeGrides[i], pershkrimi);
        }
    }

}
function changeName() {
    myFaqeCelje.shtoHandlerSession();
    myCookies.createCookie('adresa', window.location.href, 1);
    if (Utils.IsUndefined(Utils.getUrlVar('id')))
        window.parent.callWebServiceKtheInfoLart('Shto_AzhornimKlientFurnitor.aspx?vep=' + Utils.getUrlVar('vep'), 0);
    else
        window.parent.callWebServiceKtheInfoLart('Shto_AzhornimKlientFurnitor.aspx?vep=' + Utils.getUrlVar('vep'), Utils.getUrlVar('id'));
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

function valueChangedPeriudha() {
    var vleraLabel = lblPeriudhaAktuale.GetText();
    var periudha = vleraLabel.split("-");
    //var dataDok = data_DateEdit.GetText();

    var dataDok = new Date();
    dataDok = formatDate(dataDok, "dd/MM/yyyy");
    periudha1 = periudha[0].split("/");
    periudha2 = periudha[1].split("/");
    dtDokumentit = dataDok.split("/");

    if (periudha1[2] != dtDokumentit[2])
        //return false;
        data_DateEdit.SetText(periudha[0]);
    else {
        if ((dtDokumentit[1] < periudha1[1] || dtDokumentit[1] > periudha2[1]))
            Data_DateEdit.SetText(periudha[0]);
        else
            Data_DateEdit.SetText(dataDok);
    }
    // return true;
}
function EndRequestHandler(sender, args) {
    var hf = document.getElementById("hfStatusRuajtje");
    if ($('#hfqkmesazhi').val() == 'shfaqmesazh') {
        //popMesazhQK.Show();
        if ($('#hfqkmesazhi').val() == 'shfaqmesazh') {
            $('#hfqkmesazhi').val('jo');
            myMesazh.ShtoMesazh({ type: "confirm", layout: "center", modal: true, text: hfState.Get("msgDeshironiShperndarjeQendraKosto"), okClick: function () { Utils.hapPopUp(hfState.Get("msgShperndarjeNeQendratEKostos"), $('#hfUrl')); }, cancelClick: function () { Utils.JopopupClick($('#hfUrl')); } });
        }
    }
    if ($('#hfqkmesazhi').val() == 'shfaqlupe') {
        Utils.hapPopUp(hfState.Get("msgShperndarjeNeQendratEKostos"), $('#hfUrl'));

    }
    if (hf.value == "true") {
        myFaqeCelje.kontrolloTeDrejta('Shto_AzhornimKlientFurnitor.aspx?vep=' + Utils.getUrlVar('vep') + '&shtim_modifikim=shtim', true);


    } else click = false;

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
Function: pastroFushatKokes
    
Pastron fushat pasi eshte bere ruajtja apo modifikimi per te bere gati ambjentin per shtim.
*/
function pastroFushatKokes() {
    txtNrDokumenti.SetText("");
    llogariDebi_ButtonEdit.SetValue(null);;
    llogariKredi_ButtonEdit.SetValue(null);;
    vendosDateDefault();
    txtPershkrimi.SetText("");
    gvKF.PerformCallback('pastro');
    var hf = document.getElementById("hfStatusRuajtje")
    hf.value = "false";

}

function vendosDateDefault() {
    if ($("input[id$='hfShtimModifikim']").val() == "shtim") {
        try {
            var hfPeriudheObj = window.parent.lexoHfPeriudhe();
            Data_DateEdit.SetDate(Utils.ktheDateDefault(hfPeriudheObj));
            var dataSot = Utils.zeroOren(new Date());
            dteDtRegjistrimi.SetDate(dataSot);
        }
        catch (e) {
        }


    }
}

/*
Function: isValidKoka
    
Kontrollon nese jane plotesuar fushat e detyrueshme te kokes se dokumentit dhe validon daten.
*/
function isValidKoka() {

    if (txtNrDokumenti.GetText() == "") {

        myMesazh.ShtoMesazhGabimi(hfState.Get("msgShenoniNumrinEDokumentit"));
        return false;
    }
    else if (llogariDebi_ButtonEdit.GetText() == "") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgShenoniLlogDebi"));
        return false;
    }
    else if (llogariKredi_ButtonEdit.GetText() == "") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgShenoniLlogKredi"));
        return false;
    }
    else if (Data_DateEdit.GetDate() == null) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniNjeDateDokumenti"));
        return false;
    }
    else if (dteDtRegjistrimi.GetDate() == null) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniNjeDateRegjstrimi"));
        return false;
    }
    else if (countwebservice != countsucceded) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgPrisniPlotesohenTeDhenat"));
        return false;
    }
    else return true;
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
        //  lastsel2 = $("input[id$='hfId']")[0].value;
        if (isValidKoka()) {
            grida.saveRow(idRresht, false, 'clientArray');
            merrTeDhena();
            grida.setLastSel2(0);


            if (trupiBosh == true) {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgTrupiDokumentitNukDuhetLeneBosh"));
                e.processOnServer = false;
                click = false; Utils.hiqLoadingGif();;
            }
            if (llogaribosh == true && Utils.getUrlVar('vep') == 'mbyllje') {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgRreshtaGridePaLLogKunderparti"));
                e.processOnServer = false;
                click = false; Utils.hiqLoadingGif();;
            }
            if (diferenca == 0 && Utils.getUrlVar('vep') == 'azhornim') {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgPerkfMekod") + kod + hfState.Get("msgJoFitimHumbjeAzhornim"));
                e.processOnServer = false;
                click = false; Utils.hiqLoadingGif();;
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
    var hf = $("#hfShtimModifikim");
    hf.val("shtim");
    $("input[id$='hfAutorizimi']").val(true);
    myMenu.menuSipasTeDrejtaRegjistrim(hf, hfTeDrejta);
    pastro();
    ASPxMenu1.GetItemByName('FletaKontabel').SetVisible(false);
    ASPxMenu1.GetItemByName('Fshi').SetVisible(false);
    ASPxMenu1.GetItemByName('QendraKosto').SetVisible(false);
    ASPxMenu1.GetItemByName('Draft').SetVisible(true);
    pastroFushatKokes();
    ndryshoKonfigurimin();
    $('#ASPxSplitter1_hl').empty(); click = false;
}

/*
Function: menuClick

Percakton veprimin qe kryhet nese klikohet nje nga butonat e menuse.
*/
function menu_click(s, e) {
    myJQGrid.menuClick(s, e, "AzhornimKlientFurnitor.aspx?vep=" + Utils.getUrlVar('vep'), 'Shto_AzhornimKlientFurnitor.aspx?vep=' + Utils.getUrlVar('vep') + '&shtim_modifikim=shtim');
}

function Kthehu(mesazh) {
    myFaqeCelje.kontrolloTeDrejta("AzhornimKlientFurnitor.aspx?vep=" + Utils.getUrlVar('vep') + "&ruaj=po");
}

function ndryshoImazhin(nr, index) {
    myJQGrid.ndryshoImazhin(nr, index);
}

function merrTeDhena() {//merren te dhenat qe ka grida
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    grida.jqGrid('saveRow', idRresht, false, 'clientArray');
    grida.setLastSel2(0);
    llogaribosh = false;
    trupiBosh = true;
    var total = 0;
    var idTe = grida.jqGrid('getDataIDs');
    for (var i = 0; i < idTe.length; i++) {
        editorKodi = grida.getTekstQelize('txtKodi', idTe[i]);
        if (grida.getTekstQelize('txtKodi', idTe[i]) != "") trupiBosh = false;
        editorEmertimi = grida.getTekstQelize('txtEmertimi', idTe[i]);
        editorPershkrimi = grida.getTekstQelize('txtPershkrim', idTe[i]);
        editorDebiKredi = grida.getTekstQelize('cmbDebiKredi', idTe[i]);
        editorVlefta = grida.getTekstQelize('txtVlefta', idTe[i]);
        if (grida.getTekstQelize('txtVlefta', idTe[i]) != "") {
            diferenca = grida.getTekstQelize('txtVlefta', idTe[i]);
            kodi = grida.getTekstQelize('txtKodi', idTe[i]);
        }
        editorLloji = grida.getTekstQelize('txtLloji', idTe[i]);
        editorMonedha = grida.getTekstQelize('txtMonedha', idTe[i]);
        editorKursi = grida.getTekstQelize('txtKursi', idTe[i]);
        editorGjendja = grida.getTekstQelize('txtGjendja', idTe[i]);
        editorGjendjaMon = grida.getTekstQelize('txtGjendjaMon', idTe[i]);
        editorGjendjaAkt = grida.getTekstQelize('txtGjendjaAkt', idTe[i]);
        editorLlogKunder = grida.getTekstQelize('txtLlogKunderparti', idTe[i]);
        if (grida.getTekstQelize('txtKodi', idTe[i]) != "" && grida.getTekstQelize('txtLlogKunderparti', idTe[i]) == "")
            llogaribosh = true;

    }

    var tmp2 = grida.getRowData();
    for (var i = 0; i < tmp2.length; i++) {
        tmp2[i].txtFshi = "";
    }
    var rreshtaTeGrides = grida.getTeDhenaRreshti();
    $('#gridDataObject').val(JSON.stringify(rreshtaTeGrides));

}
/*
Function: pastro
    
Pastrin array-t e perdorura dhe vendos counterat ne 0.
*/
function pastro() {

    arrKodi = new Array();
    arrEmertimi = new Array();
    arrPershkrimi = new Array();
    arrLloji = new Array();
    arrDebiKredi = new Array();
    arrVlefta = new Array();
    arrMonedha = new Array();
    arrKursi = new Array();
    arrGjendja = new Array();
    arrGjendjaAkt = new Array();
    arrGjendjaMon = new Array();

    resetCountera();
    var hidField1 = $("input[id$='hfDebiKredi']")[0];  //document.getElementById("hfNrLlogaria"); //shton vleren tek hidden field e cila perdoret me vone nga kodi
    var hidField2 = $("input[id$='hfEmer']")[0];  // document.getElementById("hfEmerLlogaria");
    var hidField3 = $("input[id$='hfKod']")[0];  // document.getElementById("hfPershkrimi");
    var hidField4 = $("input[id$='hfLloji']")[0];  // document.getElementById("hfDebi");
    var hidField5 = $("input[id$='hfPershkrimi']")[0];  //document.getElementById("hfKredi");      
    var hidField6 = $("input[id$='hfDiferenca']")[0];  //document.getElementById("hfMonedha");
    var hidField7 = $("input[id$='hfMonedha']")[0];
    var hidField8 = $("input[id$='hfGjendja']")[0];
    var hidField9 = $("input[id$='hfGjendjaMon']")[0];
    var hidField10 = $("input[id$='hfKursi']")[0];
    var hidField11 = $("input[id$='hfGjendjaAktuale']")[0];
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
    var grida = $('#rowed5');
    grida.setLastSel2(0);

    //   $("input[id$='hfId']")[0].value = 0;
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

}
/*
Function: ButtonClickKodi
    
Hap lupen e artikujve apo makrove sipas zgjedhjes qe eshte bere te kategoria.
*/
function ButtonClickKodi() {
    var hfKod = $("#hfGridaKodi");
    var queryStr = hfKod.val();
    identikuesPerPopupKlientFurnitori = "AzhornimKlientFurnitoriGrida";
    myButtonClickLupa.ButtonClickFurnitori(hfState.Get("headerZgjidhKlientFurnitorin"), queryStr, "", widthLupaKF, heightLupaKF);
}

function mbushGrideNgaHiddenFieldet() {
    var grida = $("#rowed5");
    var idRresht = grida.getLastSel2();
    if ($('#hfShtimModifikim').val() == "shtim") {
        return;
    }
    if ($('#hfShtimModifikim').val() == "modifikim") {
        var col = JSON.parse($('#HfColTrup').val());
        var colLlog = JSON.parse($('#HfColLlog').val()); var colLlogKunder = JSON.parse($('#HfColLlogKunder').val());
        var colKF = JSON.parse($('#HfColKF').val());
        grida.setLastSel2(1);
        idRresht = 1;
        grida.jqGrid('clearGridData');
        var debiKredi, emerKlienti, kodKlienti, lloji, pershkrimi, monedha, idmonedha, gjendja, gjendjamon, kursi, gjendjaaktuale, vlefta, llog;
    }
    for (var i = 0; i < col.length; i++) {

        if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides - 1] == 'True' || lidhur)
            be = myJQGrid.myValueButtonFshi(true, idRresht, "#rowed5");
        else
            be = myJQGrid.myValueButtonFshi(false, idRresht, "#rowed5");

        emerKlienti = col[i].EmerKlientFurnitor;
        kodKlienti = col[i].KodiKlientFurnitor;
        if (colKF[i].LlojiKF)
            lloji = "Klient";
        else lloji = "Furnitor";
        monedha = colLlog[i].KodiMonedha;
        idmonedha = colLlog[i].IdMonedha;
        if (Utils.getUrlVar('vep') == 'mbyllje') {
            gjendja = col[i].GjendjaLlog;
            gjendjamon = col[i].GjendjaMonBaze;
            if (col[i].DebiKredi == 1)
                debiKredi = "Debi";
            else if (col[i].DebiKredi == 2)
                debiKredi = "Kredi";
        }
        else {
            gjendja = "0";
            gjendjamon = "0";
            if (col[i].DebiKredi == true)
                debiKredi = "Debi";
            else if (col[i].DebiKredi == false)
                debiKredi = "Kredi";
        }
        gjendjaaktuale = "0";
        kursi = col[i].Kursi;
        pershkrimi = col[i].Pershkrimi;
        vlefta = col[i].Vlefta;
        llog = colLlogKunder[i].NrLlogari;
        var mydata2 = {
            txtNrRendor: i + 1, txtLloji: lloji, txtKodi: kodKlienti, txtEmertimi: emerKlienti, txtPershkrim: pershkrimi, txtMonedha: monedha, txtGjendja: gjendja,
            txtGjendjaMon: gjendjamon, txtKursi: kursi, txtGjendjaAkt: gjendjaaktuale, cmbDebiKredi: debiKredi, txtVlefta: vlefta, txtLlogKunderparti: llog, txtFshi: be
        }

        var formatNumri = gjejFormatSipasMonedhes(idmonedha);
        var formatKursi = gjejFormatKursiSipasMonedhes(idmonedha);
        grida.setShifraPasPresjes('txtVlefta', formatNumri.ShifraPasPresjesVlefta, idRresht);
        grida.setShifraPasPresjes('txtGjendja', formatNumri.ShifraPasPresjesVlefta, idRresht);
        grida.setShifraPasPresjes('txtGjendjaMon', formatNumri.ShifraPasPresjesVlefta, idRresht);
        grida.setShifraPasPresjes('txtGjendjaAkt', formatNumri.ShifraPasPresjesVlefta, idRresht);
        grida.setShifraPasPresjes('txtGjendja', formatNumri.ShifraPasPresjesVlefta, idRresht);
        grida.setShifraPasPresjes('txtKursi', formatKursi, idRresht);
        grida.formatoQelize('txtKursi', idRresht);

        var su = grida.addRowData(parseInt(idRresht), mydata2);
        grida.setTekstQelize('txtNrRendor', idRresht, grida.getInd(idRresht, false));
        idRresht = idRresht + 1;
        grida.setLastSel2(idRresht);
    }
    grida.setLastSel2(0);
}

var arrKodi = new Array();
var arrEmertimi = new Array();
var arrPershkrimi = new Array();
var arrLloji = new Array();
var arrDebiKredi = new Array();
var arrVlefta = new Array();
var arrMonedha = new Array();
var arrKursi = new Array();
var arrGjendja = new Array();
var arrGjendjaMon = new Array();
var arrGjendjaAkt = new Array();

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

//function kundert() {

//    if (gvKF.cpNoRows > 10 * (gvKF.cpNoPage + 1)) {
//        for (l = 10 * gvKF.cpNoPage; l < 10 * (gvKF.cpNoPage + 1) ; l++) {

//            gvKF.SelectRowOnPage(l, !gvKF.IsRowSelectedOnPage(l));

//        }
//    }
//    else {
//        for (l = 10 * gvKF.cpNoPage; l < gvKF.cpNoRows; l++) {

//            gvKF.SelectRowOnPage(l, !gvKF.IsRowSelectedOnPage(l));
//        }
//    }
//}

function KlikoTeGjitha() {
    gvKF.SelectAllRowsOnPage();
}

function HiqTeGjitha() {
    gvKF.UnselectRows();
}

function SelectionChangedGrid() {
    btnDjathtas1.SetEnabled(false);
    gvKF.GetSelectedFieldValues('KodKlientFurnitor;NrLlogKlientFurnitor;EmertimiKF;LlojiKF;IdKlientFurnitor;Monedha;DtAzhornimi;DtLidhje;IdLlogari', OnGridSelectionCompleteImproved);

}

function GetAllGrid() {
    btnDjathtasGjitha.SetEnabled(false);
    gvKF.PerformCallback('djathtasgjithe');
}

function SucceededCallbackIdMonedha(result) {
    grida = $("#rowed5");
    var formatNumri = gjejFormatSipasMonedhes(result[1]);
    grida.setShifraPasPresjes('txtVlefta', formatNumri.ShifraPasPresjesVlefta, result[0]);
    grida.setShifraPasPresjes('txtGjendjaMon', formatNumri.ShifraPasPresjesVlefta, result[0]);
    grida.setShifraPasPresjes('txtGjendjaAkt', formatNumri.ShifraPasPresjesVlefta, result[0]);
    grida.setShifraPasPresjes('txtGjendja', formatNumri.ShifraPasPresjesVlefta, result[0]);
    var formatKursi = gjejFormatKursiSipasMonedhes(result[1]);
    grida.setShifraPasPresjes('txtKursi', formatKursi, result[0]);
    grida.formatoQelize('txtKursi', result[0]);
}

var index = 0;

function OnGridSelectionCompleteImproved(values, custom) {
    var grida = $('#rowed5');    
    var arrayMsgAzhornimPasDtDok = new Array();
    var rreshtat = grida.getTeDhenaRreshti().filter(function (s) { return s.txtKodi != '' });
    if (rreshtat.length > 0) {
        for (l = 0; l < rreshtat.length; l++) {
            rreshtat[l].txtFshi = myJQGrid.myValueButtonFshi(arrayReadOnlyKolonaGrides[12] == 'True', l + 1, "#rowed5");;
        }
    }
   
    grida.GridUnload("rowed5");
    grida = inicializoGride();
    var idRresht = rreshtat.length;
    for (var k = 0; k < values.length; k++) {
        var klienti = custom ? values[k].KodKlientFurnitor : values[k][0];
        var dtazhornimi = custom ? values[k].DtAzhornimi : values[k][6];
        var dtlidhje = custom ? values[k].DtLidhje : values[k][7];

        if (dtazhornimi >= Data_DateEdit.GetDate() || dtlidhje >= Data_DateEdit.GetDate()) {
            arrayMsgAzhornimPasDtDok.push(klienti);
            continue;
        }

        var rreshti = krijoRreshtBosh(idRresht + k + 1);
        var emer = custom ? values[k].EmertimiKF : values[k][2];
        var lloji = custom ? values[k].LlojiKF : values[k][3];
        var monedha = custom ? values[k].Monedha : values[k][5];
        var idllogari = custom ? values[k].IdLlogari : values[k][8];
        var debiKredi = lloji ? "Debi" : "Kredi";
        var llojKf = lloji ? "Klient" : "Furnitor";
        var kursi = gjejKursPerMonedhe(monedha, $("#hfKurset").val().split("||"));

        if (idllogari != null)
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "KtheIdMonedhaSipasIdLlogarise"),
                data: JSON.stringify({ idja: idllogari, idrreshti: idRresht + k + 1 })
            }).done(SucceededCallbackIdMonedha);
        rreshti.txtKodi = klienti;
        rreshti.txtEmertimi = emer;
        rreshti.txtLloji = llojKf;
        rreshti.cmbDebiKredi = debiKredi;
        rreshti.txtMonedha = monedha;
        rreshti.txtKursi = kursi;
        rreshti.txtFshi = myJQGrid.myValueButtonFshi(arrayReadOnlyKolonaGrides[12] == 'True', idRresht + k + 1, "#rowed5");;
        rreshtat.push(rreshti);
    }
    var idRow = 0;
    for (var r = 0; r < rreshtat.length; r++) {
        idRow = idRow + 1;
        grida.setLastSel2(idRow);
    }
    grida[0].addJSONData(rreshtat);
    grida.setMaxLastSel(rreshtat.length);
    grida.shtoNeRreshtinEPareBosh("txtKodi", arrayReadOnlyKolonaGrides[12] == 'True', "#rowed5");

    if (arrayMsgAzhornimPasDtDok.length > 0) {
        var mesazhGabimi = hfState.Get("msgAzhornimPasDtDok") + "Klientet jane: ";
        for (var j = 0; j < arrayMsgAzhornimPasDtDok.length; j++) {
            mesazhGabimi += arrayMsgAzhornimPasDtDok[j];
            mesazhGabimi += ((j == arrayMsgAzhornimPasDtDok.length - 1) ? "." : ",");
        }
        myMesazh.ShtoMesazhGabimi(mesazhGabimi);
    }
    callWebServiceInfoKF();
    LlogaritAzhornim();
    gvKF.PerformCallback('djathtas');
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
    return 1;
}

function krijoRreshtBosh(id) {   
    return {
        //name: id, id: id,
        txtNrRendor: id,
        cmbDebiKredi: "1", txtEmertimi: "", txtFshi: "", txtGjendja: "", txtGjendjaAkt: "", txtGjendjaMon: "", txtKodi: "", txtKursi: "", txtLlogKunderparti: "", txtLloji: "1", txtMonedha: "", txtPershkrim: "", txtVlefta: ""
    }
}

function Majtas1(s, e) {
    var grida = $("#rowed5");
    var idRresht = grida.getLastSel2();
    if (grida.getTekstQelize('txtKodi', idRresht) != undefined && grida.getTekstQelize('txtKodi', idRresht) != "") {
        gvKF.PerformCallback('majtas:' + grida.getTekstQelize('txtKodi', idRresht));
        myJQGrid.fshiClicked(idRresht, "#rowed5", inicializoGride);
    }
}

function MajtasGjithe() {
    var grida = $("#rowed5");
    grida.setLastSel2(-1);
    grida.GridUnload("rowed5");
    inicializoGride();
    gvKF.PerformCallback('kalotegjithe');
}

var countwebservice = 0;
var countsucceded = 0;
function LlogaritAzhornim() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var rreshtaTeGrides = grida.getDataIDs();
    var id = 0;
    var arrkodi = new Array();
    var arrindexe = new Array();
    if ($('#hfShtimModifikim').val() == 'modifikim') {
        id = (Utils.getUrlVar('id'));
        if (id == undefined)
            id = 0;
    }

    for (i = 0; i < rreshtaTeGrides.length; i++) {
        if (grida.getTekstQelize('txtKodi', rreshtaTeGrides[i]) != "") {
            arrkodi.push(grida.getTekstQelize('txtKodi', rreshtaTeGrides[i]));
            arrindexe.push(i);
        }
    }

    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "merrGjendje"),
        data: JSON.stringify({ kodikf: arrkodi, datedok: Data_DateEdit.GetDate(), i: arrindexe, iddok: id, kodkonfigurim: cmbKonfigurimi.GetText() })
    }).done(SucceededCallbackAzhornim);
}

function SucceededCallbackAzhornim(rezult) {
    var grida = $("#rowed5");
    var idRresht = grida.getLastSel2();
    var arrfshi = new Array();
    var arrayMsgJoVlefteZero = new Array();
    for (var k = 0; k < rezult.length; k++) {
        var i = rezult[k][0];
        var idx = grida.getDataIDs()[i];
        var rreshtaTeGrides = grida.getRowData(idx);
        while (rreshtaTeGrides.txtEmertimi == "") {
            //Vlen ne rastet kur zgjidhen disa furnitore.
            //Nese njeri prej tyre nuk ploteson kushtet per tu ngarkuar, rreshti i tij ne gride fshihet. Behet kontrolli qe plotesimi te vazhdoje ne rreshtin ku ka nisur, jo me poshte.
            idx--;
            rreshtaTeGrides = grida.getRowData(idx);
        }

        grida.setTekstQelize('txtGjendja', idx, rezult[k][1]);
        grida.setTekstQelize('txtGjendjaMon', idx, rezult[k][2]);
        if (Utils.getUrlVar('vep') == 'mbyllje') {
            if ((rezult[k][2] == 0 && rezult[k][1] != 0) || (rezult[k][2] != 0 && rezult[k][1] == 0))
                grida.setTekstQelize('txtKursi', idx, 1);
            else if (rezult[k][1] != 0)
                grida.setTekstQelize('txtKursi', idx, (Math.abs(rezult[k][1] == 0 ? 1 : rezult[k][2] / rezult[k][1])));

        }
        grida.setTekstQelize('txtGjendjaAkt', idx, (rezult[k][1] * grida.getTekstQelize('txtKursi', idx)));
        grida.setTekstQelize('txtVlefta', idx, (rezult[k][1] * grida.getTekstQelize('txtKursi', idx) - rezult[k][2]));

        if (rezult[k][3] == 1) {
            if (grida.getTekstQelize('txtLlogKunderparti', idx) === '') {
                if (idx == idRresht) {
                    $("#cmbDebiKredi" + idx).val(1);
                }
                else
                    grida.setTekstQelize('cmbDebiKredi', idx, 'Debi');
                if (Utils.getUrlVar('vep') == 'mbyllje') {
                    grida.setTekstQelize('txtLlogKunderparti', idx, llogariDebi_ButtonEdit.GetText());
                }
                else
                    if (parseFloat(grida.getTekstQelize('txtVlefta', idx)) > 0)
                        grida.setTekstQelize('txtLlogKunderparti', idx, llogariKredi_ButtonEdit.GetText());
                    else grida.setTekstQelize('txtLlogKunderparti', idx, llogariDebi_ButtonEdit.GetText());
            }
        }
        else {
            if (grida.getTekstQelize('txtLlogKunderparti', idx) === '') {
                if (idx == idRresht) {
                    $("#cmbDebiKredi" + idx).val(2);
                }
                else
                    grida.setTekstQelize('cmbDebiKredi', idx, 'Kredi');
                if (Utils.getUrlVar('vep') == 'mbyllje') {
                    grida.setTekstQelize('txtLlogKunderparti', idx, llogariKredi_ButtonEdit.GetText());
                }
                else
                    if (parseFloat(grida.getTekstQelize('txtVlefta', idx)) > 0)
                        grida.setTekstQelize('txtLlogKunderparti', idx, llogariDebi_ButtonEdit.GetText());
                    else grida.setTekstQelize('txtLlogKunderparti', idx, llogariKredi_ButtonEdit.GetText());

            }
        }

        //Kontrollon nese vlefta e furnitorit te zgjedhur eshte zero
        if (Utils.getUrlVar('vep') != 'mbyllje' && grida.getTekstQelize('txtVlefta', idx) == 0) {

            arrfshi.push([idx]);
            arrayMsgJoVlefteZero.push(grida.getTekstQelize('txtKodi',idx));
        }
    }
    if (arrayMsgJoVlefteZero.length > 0) {
        var mesazhGabimi = hfState.Get("msgJoVlefteZero") + "Klientet jane: ";
        for (var j = 0; j < arrayMsgJoVlefteZero.length; j++) {
            mesazhGabimi += arrayMsgJoVlefteZero[j]
            mesazhGabimi += ((j == arrayMsgJoVlefteZero.length - 1) ? "." : ",");
        }
        myMesazh.ShtoMesazhGabimi(mesazhGabimi);
    }
    for (var m = 0; m < arrfshi.length; m++) {
        grida.delRowData(arrfshi[m]);
        grida.rregulloNrRendorMeTeMadh(arrfshi[m] - 1);
    }
}

function dateChanged() {
    MerrKurseSipasDates();
    callWebServiceInfoKF();
}


function MerrKurseSipasDates() {
    gvKF.PerformCallback('data');
    if (Utils.getUrlVar('vep') == 'azhornim') {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "merrKursetMonedhaveSipasDates"),
            data: JSON.stringify({ datedok: Data_DateEdit.GetDate(), pershkrimi: true, llojkursi: llojkursi })
        }).done(SucceededCallbackKurset);
    }
    else
        ndryshoKonfigurimin();
}

function SucceededCallbackKurset(result) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    grida.saveRow(idRresht, false, 'clientArray');
    grida.setLastSel2(-1);
    $('#hfKurset').val(result);
    var dataRow = grida.getRowData();
   var ids= grida.getDataIDs();
    for (var i = 0; i < dataRow.length; i++) {
        var kursi = gjejKursPerMonedhe(dataRow[i].txtMonedha, $("#hfKurset").val().split("||"));
        grida.setTekstQelize('txtKursi', ids[i], kursi);
    }
    LlogaritAzhornim();
}

function SelectionChange(s, e) {
    gvKF.GetRowValues(gvKF.GetFocusedRowIndex(), 'IdKlientFurnitor;Monedha', kontrolloVeprimFunditKlientFurnitor);
}

function kontrolloVeprimFunditKlientFurnitor(values) {
    try {
        if (values == null) {
            myMesazh.ShtoMesazhGabimi('Selektoni nje rresht!');
            return;
        }
        if (Utils.getUrlVar('vep') == 'mbyllje' && values != null) {
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "eshteAzhornimVeprimiFunditKlientFurnitor"),
                data: JSON.stringify({ idKf: values[0], dateSelektuar: Data_DateEdit.GetDate(), kodMonedhaKlFurn: values[1] })
            }).done(SucceededCallbackMesazhAzhornimi);
        }
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
    }
}

function SucceededCallbackMesazhAzhornimi(result) {
    if (result == null) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
        return;
    }
    if (result == false) { //nqs nuk eshte bere veprimi i azhornimit i fundit per klientin/furnitorin ne monedhe te huaj do shfaqet pyejta per azhornimin
        myMesazh.vendosClient();
        myMesazh.ShtoPyetje(hfState.Get("msgDoniTeBeniAzhornimkf"), false);
    }
}

function PoClick(s, e) {//po
    mesazhList.SetSelectedIndex(-1);
    btnPo.SetVisible(false);
    btnJo.SetVisible(false);
    myFaqeCelje.kontrolloTeDrejta('Shto_AzhornimKlientFurnitor.aspx?vep=azhornim&shtim_modifikim=shtim');
}

function JoClick(s, e) {//po
    mesazhList.SetSelectedIndex(-1);
    btnPo.SetVisible(false);
    btnJo.SetVisible(false);
}


function ShtoArrayMeVleraNeJqGrid(schema, keyFieldName, jqGrida, arrayMeVlera, enableFshi) {

    var arrayMeVleraTeReja = new Array();

    var idRreshtiBosh = -1;
    var ids = jqGrida.getDataIDs();
    var max = 0;
    var nrRreshtaBoshNeGride = 0;

    //gjej rreshtin bosh dhe numero sa rreshta bosh kemi ne grid aktualisht
    //gjej dhe id max ne grid
    for (var id in ids) {
        max = Math.max(max, id);
        var tekstTmp = jqGrida.getTekstQelize(keyFieldName, id);
        if (tekstTmp == undefined || tekstTmp == "") {
            //gjej te parin rresht bosh
            if (idRreshtiBosh == -1)
                idRreshtiBosh = id;
            nrRreshtaBoshNeGride++;
        }
    }

    max = parseInt(max);
    //kalojme ne nje array te ri te gjithe rreshtat qe jane per tu shtuar ne grid,te cilent nuk gjenden aty!
    var rreshtiRadhes = undefined;
    var ekziston = false;
    for (var i = 0; i < arrayMeVlera.length; i++) {
        rreshtiRadhes = arrayMeVlera[i];
        ekziston = false;
        for (j = 0; j < ids.length; j++) {
            var editorkodi = jqGrida.getTekstQelize(keyFieldName, ids[j]);
            if (editorkodi == rreshtiRadhes[schema[keyFieldName]]) {
                ekziston = true;
                break;
            }
        }
        if (!ekziston)
            arrayMeVleraTeReja.push(rreshtiRadhes);
    }

    //shtojme aq rreshta bosh sa na duhen +1
    for (var b = 0, length = (arrayMeVleraTeReja.length - nrRreshtaBoshNeGride + 1) ; b < length; i++) {
        var id = max + 1;
        if (!enableFshi)
            be = "<input id='butonFshi" + id + "' type='image' disabled='disabled' value='Fshi' onBlur = 'lostFocusKoloneFundit()' onmouseover='ndryshoImazhin(1," + id + ")' onmouseout='ndryshoImazhin(0," + id + ")'  src='images/square-icon.png' onclick='fshiClicked(" + id + ")'/>";
        else
            be = "<input id='butonFshi" + id + "' type='image' value='Fshi' onBlur = 'lostFocusKoloneFundit()'  onmouseover='ndryshoImazhin(1," + id + ")' onmouseout='ndryshoImazhin(0," + id + ")'  src='images/square-icon.png' onclick='fshiClicked(" + id + ")'/>";

        var datarow = { txtFshi: be };
        jqGrida.addRowData(id, datarow);
        max++;
    }

    //marrim gjithe fushat qe ka schema
    var keys = Object.keys(schema);
    var rreshtat = new Array(arrayMeVleraTeReja.length);
    for (var r = 0; r < rreshtat.length; i++) {
        rreshtiRadhes = arrayMeVleraTeReja[r];
        //mbush rreshtin
        for (var k = 0; k < keys.length; i++) {
            jqGrida.setTekstQelize(k, idRreshtiBosh, rreshtiRadhes[k]);
        }
        idRreshtiBosh++;
    }
}
function gvKfEndCallback(s, e) {
    btnDjathtasGjitha.SetEnabled(true);
    btnDjathtas1.SetEnabled(true);
    if (gvKF["cpKlientTeZgjedhur"] != undefined) {
        var teZgjedhur = JSON.parse(gvKF["cpKlientTeZgjedhur"]);
        OnGridSelectionCompleteImproved(teZgjedhur, true);
        delete gvKF["cpKlientTeZgjedhur"];
    }
}

//#REGION INFO
//TODO PATI: Per t'u kaluar tek utils se jane njesoj tek te gjithe ambientet me info.
var mbyll = true;
var infoKf = false;
var idInfoKf = 0;
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
    var kodi = grida.getTekstQelize('txtKodi', idRresht);
    if (!kodi)
        return;
    pastroInfoKf();
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheInfoKFSipasKodit"), data: JSON.stringify({ kodi: kodi, idNdermarje: hfState.Get("idNdermarrje"), data: Data_DateEdit.GetDate(), idInfo: idInfoKf })
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