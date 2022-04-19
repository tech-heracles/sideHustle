var pageState = { cache: { kodbare: {}, seriale: {} }, kushte: {}, lloji: ''};
var isLidhur = false;
var arrayMeMagazina = new Array();
var colMagazina;
var arrayIdKolonaGrides = new Array();
var arrayPershkrimiKolonaGrides = new Array();
var arrayVisibleKolonaGrides = new Array();
var arrayWidthKolonaGrides = new Array();
var arrayReadOnlyKolonaGrides = new Array();
var arrayRenditjeKolonaGrides = new Array();
var widthLupaMagazina = 600;
var heightLupaMagazina = 600;
var widthLupaPeriudha = 600;
var heightLupaPeriudha = 600;
var widthLupaKerko = 600;
var heightLupaKerko = 600;

var editorData;

var STR_sasiaNumer = 'Sasia duhet të jetë numer!';

var click = false;

var varKonfig = {
    identifikuesPerLocalStorageKey: 'Inventarizimi'
};


//$("#shtoSasine").on("keydown", function () { alert("bkjhkjhklj"); })
jQuery(document).ready(function () {
    krijoLupeShtoSasi();
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

function Init() {
    if (typeof (isPostBack) == "undefined") {
        document.getElementById("kokeKonfigurimi").innerHTML = hfState.Get("MenuKokeDokumenti");
        document.getElementById("trupKonfigurimi").innerHTML = hfState.Get("MenuTrupDokumenti");
        document.getElementById("fundKonfigurimi").innerHTML = hfState.Get("MenuFundDokumenti");
        editorData = dteDtDok;
        pageState.idNdermarrje = hfState.Get("idNdermarrje");        
        pageState.idPerdoruesi = hfState.Get("idPerdoruesi");
        pageState.idViti = hfState.Get("idViti");
        pageState.idGjuha = hfState.Get("idGjuha");
        identifikuesPerPopupDokumentat = "Shto_RegjistrimInventarizimi.aspx";
        identikuesPerPopupArtikulli = "RegjistrimInventarizimi";
        identifikuesPerPopupMagazina = "RegjistrimInventarizimi";
        changeName();      
        myMesazh.shtoHandler();
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(EndRequestHandler);
        pageState.lloji = $('#hfShtimModifikim').val();
    }
}

function krijoLupeShtoSasi() {
    $(function () {
        
        $("#dialog-shtoSasi").dialog({
            modal: true,
            buttons: {
                Ok: function () {
                     
                   if(ndryshoSasine())
                        $(this).dialog("close");
                    
                }
            },
            autoOpen: false
        });
        $("#ui-id-1").parent().addClass("klasePerDialogtitlebar");
    });
    $("#shtoSasine").on("keydown", function (e) {
        switch (e.which) {
            case 13:
                ndryshoSasine();
                $("#dialog-shtoSasi").dialog("close");
                break;
            default:
                break;
        }
    });
    
   // $("#dialog-shtoSasi").addClass("klasePerDialogtitlebar");
}
/*
Function: inicializoGride

Inicializon griden e trupit. Konfiguron kolonat e grides dhe percakton veprimin qe kryhet onCellSelect.
*/
function inicializoGride(isLidhur) {
    if (arrayPershkrimiKolonaGrides.length == 0)
        return;
    mbushArrayMagazinat();
    var classes = '';
    if (isLidhur == true && $("input[id$='hfShtimModifikim']").val() == 'modifikim')
        classes = 'uigray';
    var arrayPershkrime = [arrayPershkrimiKolonaGrides[0], arrayPershkrimiKolonaGrides[1], arrayPershkrimiKolonaGrides[2], arrayPershkrimiKolonaGrides[3], arrayPershkrimiKolonaGrides[4], arrayPershkrimiKolonaGrides[5], arrayPershkrimiKolonaGrides[6], arrayPershkrimiKolonaGrides[7]];
    var arrayModel = [
        { name: arrayIdKolonaGrides[0], index: arrayIdKolonaGrides[0], width: arrayWidthKolonaGrides[0], hidden: arrayVisibleKolonaGrides[0], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemNrRendor, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[1], index: arrayIdKolonaGrides[1], width: arrayWidthKolonaGrides[1], hidden: arrayVisibleKolonaGrides[1], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myElemSerial, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[2], index: arrayIdKolonaGrides[2], width: arrayWidthKolonaGrides[2], hidden: arrayVisibleKolonaGrides[2], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myElemKodbari, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[3], index: arrayIdKolonaGrides[3], width: arrayWidthKolonaGrides[3], hidden: arrayVisibleKolonaGrides[3], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myElemKodi, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[4], index: arrayIdKolonaGrides[4], width: arrayWidthKolonaGrides[4], hidden: arrayVisibleKolonaGrides[4], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myElemPershkrimi, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[5], index: arrayIdKolonaGrides[5], width: arrayWidthKolonaGrides[5], hidden: arrayVisibleKolonaGrides[5], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemSasia, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[6], index: arrayIdKolonaGrides[6], width: arrayWidthKolonaGrides[6], hidden: arrayVisibleKolonaGrides[6], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myElemShenime, custom_value: myJQGrid.myValueTextBox } },
      { name: arrayIdKolonaGrides[7], index: arrayIdKolonaGrides[7], width: arrayWidthKolonaGrides[7], hidden: arrayVisibleKolonaGrides[7], classes: classes, sorttype: "int", editable: true, edittype: 'custom', editoptions: { custom_element: myElemButtonFshi, custom_value: myValueButtonFshi }, hidedlg: true },
    ];




    var selektoriGrides = "#rowed5";

    var gridParams = {
        emergride: selektoriGrides,
        arrayPershkrime: arrayPershkrime,
        arrayModel: arrayModel,
        lidhur: isLidhur,
        emerEditorKodi: eshteAfatShkurter ? "txtKodbari" : "txtSerial",
        widthi: $('#divgride2').width() - 5,
        fokus: pageState.kushte.F,
        lostFocusKoloneFundit: lostFocusKoloneFundit,
        resetRreshtKorent: resetRreshtKorent,
        mosshtorresht: !lejomod ? true : false,
        konfigToolbar: {
            konfigGrid: $('#hfTeDrejtaKonfGride').val(),
            shtoArtikull: $('#hfTeDrejtaArtRi').val(),
            modArtikull: $('#hfTeDrejtaArtMod').val(),
            ruajKolonatEGrides: ruajKolonatEGrides,
            exportExcel: true,        //Ben enable exportin e F
            EmerExporti: "Inventarizimi" //Emri i filet .xls qe gjenerohet
        }
        
    };
    return myJQGrid.initGride(gridParams);
    //myJQGrid.inicializoGride("#rowed5", arrayPershkrime, arrayModel, isLidhur,
    //    lastsel2, "#" + (eshteAfatShkurter ? "txtKodbari" : "txtSerial"), null, null, null, '', '',
    //    $('#divgride2').width() - 5, undefined, fokusi, null, null, '', '', undefined, undefined, $('#hfTeDrejtaKonfGride').val(),
    //    undefined, undefined, undefined, !lejomod ? true : false);
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
    grida.setShifraPasPresjes('txtSasia', formatNumri.ShifraPasPresjesSasia);

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

    }
    formatoFushaDevi();
}

function formatoFushaDevi() {

}

function unformatoFushaDevi() {

}

/*
Function: myElemNrRendor

Nderton nje textbox per te vendosur nr rendor te rreshtit.
*/
function myElemNrRendor(value, options) {//po
    disabled = arrayReadOnlyKolonaGrides[0];
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemNrRendor(value, options, idRresht, 'txtNrRendor', grida);
}

function resetRreshtKorent(idRreshti) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();

    if (idRreshti == idRow && grida.getTekstQelize('txtKodbari', idRow) != '') {
        grida.setTekstQelize('txtSerial', idRreshti, '');
        grida.setTekstQelize('txtKodbari', idRreshti, '');
        grida.setTekstQelize('txtSasia', idRreshti);
        return;
    }
    grida.jqGrid('delRowData', idRreshti);
    //grida.rregulloNrRendorMeTeMadh(idRreshti);
    grida.rregulloNrRendor("txtKodbari");
    return;
}

function dateGotFocus(s, e) {
    window.setTimeout(function () { s.SetCaretPosition(0); }, 0);
}

function formGridColsArray(isLidhur) {
    var IdKonfigAmbjenteLupat = [{}];
    myJQGrid.formArrayKolGrides($('#HfGridCol'), arrayIdKolonaGrides, arrayPershkrimiKolonaGrides, arrayVisibleKolonaGrides, arrayReadOnlyKolonaGrides, isLidhur, arrayWidthKolonaGrides, IdKonfigAmbjenteLupat, arrayRenditjeKolonaGrides);
}

/*
Function: myElemSerial

Nderton nje textbox dhe nje buton per te zgjedhur artikullin ose makron sipas llojit te zgjedhur tek combo Kategoria
*/
function myElemSerial(value, options) {
    var idRresht = $('#rowed5').getLastSel2();
    disabled = !lejomod ? 'True' : arrayReadOnlyKolonaGrides[1];
    return myJQGrid.myElemEmertimi(value, !lejomod ? 'True' : arrayReadOnlyKolonaGrides[1], idRresht, arrayIdKolonaGrides[1], null, kodiKeydown);
}

/*
Function: myElemShenime

Nderton nje textbox dhe nje buton per te shkruar shenime ose makron sipas llojit te zgjedhur tek combo Kategoria
*/
function myElemShenime(value, options) {
    var idRresht = $('#rowed5').getLastSel2();
    return myJQGrid.myElemEmertimi(value, arrayReadOnlyKolonaGrides[6], idRresht, arrayIdKolonaGrides[6]);
}

/*
Function: myElemPershkrimi

Nderton nje textbox dhe nje buton per te zgjedhur artikullin ose makron sipas llojit te zgjedhur tek combo Kategoria
*/
function myElemPershkrimi(value, options) {
    var idRresht = $('#rowed5').getLastSel2();
    return myJQGrid.myElemEmertimi(value, arrayReadOnlyKolonaGrides[4], idRresht, arrayIdKolonaGrides[4]);
}

/*
Function: myElemKosi

Nderton nje textbox dhe nje buton per te zgjedhur artikullin ose makron sipas llojit te zgjedhur tek combo Kategoria
*/
function myElemKodi(value, options) {
    var idRresht = $('#rowed5').getLastSel2();
    return myJQGrid.myElemEmertimi(value, arrayReadOnlyKolonaGrides[3], idRresht, arrayIdKolonaGrides[3]);
}


function myValueButtonFshi(elem, operation, value) {
    var idRresht = $('#rowed5').getLastSel2();
    if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1] == 'True' || !lejomod)
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
    if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1] == 'True' || !lejomod)
        return myJQGrid.myElemButtonFshi(true, idRresht, "#rowed5", lostFocusKoloneFundit);
    else
        return myJQGrid.myElemButtonFshi(false, idRresht, "#rowed5", lostFocusKoloneFundit);
}

/*
Function: myElemEmertimi

Nderton nje textbox ku vendoset emertimi i artikullit apo makros
*/

function myElemKodbari(value) {//po        
    var idRresht = $('#rowed5').getLastSel2();
    return myJQGrid.myElemEmertimi(value, !lejomod ? 'True' : arrayReadOnlyKolonaGrides[2], idRresht, arrayIdKolonaGrides[2], null, kodiKeydown);
}

/*
Function: myElemSasia

Nderton nje textbox per te vendosur sasine.
*/

function myElemSasia(value, options) {//po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return grida.myElemTextBoxFormatNumri({ value: value, options: options, disabled: arrayReadOnlyKolonaGrides[5] == 'True' || !lejomod ? "True" : "False", indexRow: idRresht, id: "txtSasia", onTrueKeyDown: vendosVleftat, onFocusout: kontrolloVlereBosh, onFocus: SelektoNeFokusSasi, buttonClick: hapPopupShtoSasi, buttonValue: "+", button: true, returnOnEnter: false });
}

function SelektoNeFokusSasi(idElementi) {
    $('#' +idElementi).select();
}

function hapPopupShtoSasi(evt) {
    $("#shtoSasine").val('');
    $("#dialog-shtoSasi").dialog("open");
}


function ndryshoSasine() {
    if (isNaN($("#shtoSasine").val()) || ($("#shtoSasine").val())=="") {
        myMesazh.ShtoMesazhGabimi("Sasia duhet te jete numer!");
        $("#shtoSasine").val("");
        return false;
    }
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var shuma = parseFloat(grida.getTekstQelize('txtSasia', idRresht)) + parseFloat($("#shtoSasine").val());
    grida.setTekstQelize('txtSasia', idRresht, shuma);
    return true;
}

var pyeturDoni = 0;
function kodiKeydown(id, idRreshti, event) {
    if (event.which != 13)
        return;
    //event.stopPropagation();
    //event.preventDefault();
    changeKodbari(id, idRreshti);
}

//kopjon te dhenat e rreshtit x te y nqs kane kod te njejte dhe mbledh sasite
function kopjoRreshtin(grida, idRreshtiDest, idRreshtiOrig) {
    grida.setTekstQelize('txtSasia', idRreshtiDest, parseFloat(grida.getTekstQelize('txtSasia', idRreshtiOrig)) + parseFloat(grida.getTekstQelize("txtSasia", idRreshtiDest)));
    grida.setTekstQelize('txtPershkrimi', idRreshtiDest, grida.getTekstQelize('txtPershkrimi', idRreshtiOrig));
    grida.setTekstQelize('txtShenime', idRreshtiDest, grida.getTekstQelize('txtShenime', idRreshtiOrig));
    var kodi = grida.getTekstQelize('txtKodi', idRreshtiOrig);
    grida.setTekstQelize('txtKodi', idRreshtiDest, kodi);
    return kodi;
}


function kontrolloGride(id, idRreshti, grida, kodbari, kodMag, eshteAfatShkurter,dateDok) {
    var gridIds = grida.jqGrid('getDataIDs');
    for (i = 0; i < gridIds.length; i++) {
        var tmpId = gridIds[i];
        if (idRreshti == tmpId)
            continue;
        var tmpKodbar = grida.getTekstQelize(id, tmpId);
        if (tmpKodbar != kodbari || tmpKodbar == "")
            continue;
        switch (id) {
            case "txtKodbari":
                var kodi = kopjoRreshtin(grida, idRreshti, tmpId); //kopjo tmpid ne idrresht dhe mblidh sasi
                grida.ngjyrosEkziston(idRreshti, "txtKodbari", kodi == "" ? false : true); //ngjyros
                resetRreshtKorent(tmpId); //fshi rreshtin tmpid
                var artikulli = pageState.cache.kodbare[kodbari]; //shiko nese e kam ne cache
                if (artikulli && typeof (artikulli.infoArt) != "undefined") {
                    SucceededCallbackInfoArt(artikulli.infoArt, idRreshti); //ploteso info
                }
                else {
                    ktheArtInfo(kodbari, idRreshti, pageState.idNdermarrje, kodMag, eshteAfatShkurter,dateDok ); //ik merr info ne server se nuk e kam
                }
                myJQGrid.setFokus(pageState.kushte.F, lostFocusKoloneFundit, idRreshti, "#txtSasia"); //vendos fokus sipas kushtit
                return true;
                break;
            case "txtSerial":
                if (tmpKodbar == kodbari && tmpKodbar != "") {
                    myMesazh.ShtoMesazhGabimi('Nuk mund te vendosni nje serial dy here ne gride');
                    resetRreshtKorent(idRreshti);
                    return true;
                }
                break;
            default:
                myMesazh.ShtoMesazhGabimi('Lloj kodbari i panjohur: ' + id);
                return true;
        }
    }
    return false;
}
function changeKodbari(id, idRreshti) {
    //pageState.kushte.F = 0; //0,1,2
    //pageState.kushte.ZADHG = true; //true, false
    var grida = $("#rowed5");
    var idRow = grida.getLastSel2();
    var kodbari = grida.getTekstQelize(id, idRreshti);
    if (kodbari == "")
        return;
    var kodMag = btneMagazina.GetText();
    if (!kodMag) {
        myMesazh.ShtoMesazhGabimi("Duhet te zgjidhni nje magazine");
        return;
    }
    if (typeof idRreshti == "undefined")
        idRreshti = idRow;    
    if (!eshteAfatShkurter || !pageState.kushte.ZADHG) {  //per tu pare boo 
        if (kontrolloGride(id, idRreshti, grida, kodbari, kodMag, eshteAfatShkurter, dteDtDok.GetDate()))
            return;
    }
    var artikulli = pageState.cache.kodbare[kodbari];
    if (artikulli) {
        grida.ngjyrosEkziston(idRreshti, "txtKodbari", true);
        grida.setTekstQelize('txtPershkrimi', idRreshti, artikulli.PershkrimArtikulli);
        grida.setTekstQelize('txtKodi', idRreshti, artikulli.KodArtikulli);
        if (artikulli.infoArt)
            SucceededCallbackInfoArt(artikulli.infoArt, idRreshti);
        else
            console.warn("SucceededCallbackInfoArt(" + artikulli.infoArt + ", " + idRreshti + ") -- problem tek artikulli.infoArt");
        myJQGrid.setFokus(pageState.kushte.F, lostFocusKoloneFundit, idRreshti, "#txtSasia");
        return;
    }
    ktheArtInfo(kodbari, idRreshti, pageState.idNdermarrje, kodMag, eshteAfatShkurter, dteDtDok.GetDate());
}



/*
Function: fshiClicked

Fshin nje rresht te grides

Parameters:

index - Id e rreshtit qe do fshihet    
*/
function fshiClicked(index) {
    var grida = $('#rowed5');
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


function JoClick(s, e) {
    var grida = $('#rowed5');
    switch (identifikuesPyetje) {
        default:
            alert('Pyetje e panjohur');
            break;
    }
}

function PoClick(s, e) {
    switch (identifikuesPyetje) {
        case "artikulli":
            //asgje
            break;

        default:
            alert(hfState.Get("msgPyetjePanjohur"));
            break;
    }
}

/*
Function: mbushArrayMagazinat

*/
function mbushArrayMagazinat() {//po
    var colMag = $('#hfTmpColMag').val();
    if (colMag != '') {
        colMagazina = $.parseJSON(colMag);
        $('#hfTmpColMag').val(''); //boshatisim HF-ne qe mos te harxhojme kot memorie
    }
}


/*
Function: lostFocusKoloneFundit

Percakton veprimin qe kryhet kur heqim fokusin nga kolona e fundit e gride (ruhet rreshti korent dhe shtohet nje rresht i ri bosh i editueshem).
Ketu kolona e fundit eshte "Vlefta" (sepse eshte rasti kur nuk po behet transferim).
*/
function lostFocusKoloneFundit() {
    $("#rowed5").lostFocusKoloneFundit();
    //jQuery("#txtSasia" + lastsel2).focus();
    //jQuery("#txtSasia" + lastsel2).blur();
    //jQuery("#txtKodbari" + lastsel2).focus();
}


function mbushGrideNgaHiddenFieldet(isLidhur) {
    var grida = $("#rowed5");
    var idRresht = grida.getLastSel2();
    if (arrayPershkrimiKolonaGrides.length == 0)
        return;
    if (pageState.lloji == "shtim") {
        myJQGrid.focusGrid(focusGridParams());
        return;
    }

    if (pageState.lloji == "modifikim") {
        if (Utils.getUrlVar('krahasuar') == 'Po')
            myMesazh.ShtoMesazhInformues("Ky dokument eshte perdorur per inventarizim!")
        var colTrupMag = JSON.parse($('#HfColTrupMag').val());
        var konfAmb = JSON.parse($('#HfKonfAmb').val());
        grida.setLastSel2(1);
        idRresht = 1;
        grida.jqGrid('clearGridData');
        var kodi, serial, sasia, shenime;
        for (var i = 0; i < colTrupMag.length; i++) {
            if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1] == 'True' || isLidhur || !lejomod)
                be = myJQGrid.myValueButtonFshi(true, idRresht, "#rowed5");
            else
                be = myJQGrid.myValueButtonFshi(false, idRresht, "#rowed5");
            kodi = colTrupMag[i].Barkod;
            serial = colTrupMag[i].Serial;
            sasia = colTrupMag[i].Sasi;
            shenime = colTrupMag[i].Shenime;
            kodArtikulli = colTrupMag[i].Kodi;
            pershkrimi = colTrupMag[i].Pershkrimi;
            var datarow = {
                txtNrRendor: i + 1, txtKodbari: kodi, txtSerial: serial, txtKodi: kodArtikulli, txtPershkrimi: pershkrimi, txtSasia: sasia, txtShenime: shenime,
                txtFshi: be
            };
            var su = grida.jqGrid('addRowData', parseInt(idRresht), datarow);
            grida.setTekstQelize('txtSasia', idRresht, sasia); 

            idRresht = idRresht + 1;
            grida.setLastSel2(idRresht);

        }
    }
}

var identifikuesPerPopupDokumentat;
var identikuesPerPopupArtikulli;
var identifikuesPerPopupMagazina;
function changeName() {
    myFaqeCelje.shtoHandlerSession();
    try {
        if (Utils.getUrlVar('id') == undefined)
            window.parent.callWebServiceKtheInfoLart('Shto_RegjistrimInventarizimi.aspx?lloj=' + Utils.getUrlVar('lloj'), 0);
        else
            window.parent.callWebServiceKtheInfoLart('Shto_RegjistrimInventarizimi.aspx?lloj=' + Utils.getUrlVar('lloj'), Utils.getUrlVar('id'));
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

    var queryString = {
        veprimi: 'RegjistrimInventarizimi',
        listUrl: listUrl,
        niveli:niveli
    };


    popupUniversal.SetHeaderText(hfState.Get("msgZgjidhDokumentin"));
    popupUniversal.SetContentUrl('LupaDokumenta.aspx?' + Utils.KonvertoObjectQueryString(queryString));
    popupUniversal.SetSize(widthLupaKerko, heightLupaKerko);
    popupUniversal.Show();
}
function focusGridParams(idRreshti) {
    var params = { emergride: '#rowed5', isLidhur: isLidhur, idKoloneGrideFokus: eshteAfatShkurter ? arrayIdKolonaGrides[2] : arrayIdKolonaGrides[1] };
    if (idRreshti)
        params.idRreshti = idRreshti;
    return params;
}

/*
Function: callWebserviceKonfigurimi

*/
//function callWebserviceKonfigurimi(idKomp, kodKonf) {
    
//        var idGjuha = hfState.Get('idGjuha');
//        var idNdermarrje = hfState.Get('idNdermarrje');
//        var idPerdorues = hfState.Get('idPerdoruesi');
//        $.ajax({
//            url: Utils.getServerApiUrl("Konfigurime", "ktheKonfigAmbjentiMeFormatNumrash"),
//            data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, kodKontrolli: '', idObjekti: -1, shtim: false, merrFormatKursi: false, merrGjitheKonf: false, idGjuha: idGjuha })
//        }).done(SucceededCallbackKonfig);
   
//}

function callWebserviceKonfigurimi(idKomp, kodKonf) {

    var idGjuha = hfState.Get('idGjuha');
    var idNdermarrje = hfState.Get('idNdermarrje');
    var idPerdorues = hfState.Get('idPerdoruesi');
    var url = Utils.getServerApiUrl("Rregjistrime", "ktheKonfigDB");
    $.ajax({
        url: url,
        data: JSON.stringify({
            idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: pageState.idNdermarrje, kodKontrollKlienti: "", idKlienti: -1, shtim: false,
            merrFormatKursi: false, idGjuha: idGjuha, idPerdoruesi: idPerdorues, llojVeprimi: pageState.lloji })
    }).done(SucceededCallbackKonfig);
}

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}

var colKushte; var colAlterKusht;
var fokusi = 0; lejomod = true;
var colGrida;
var formatNumriZgjedhur;

function SucceededCallbackKonfig(result) {    
    if (pageState.lloji == "shtim")
        pastroFushatKokes();
    var hf = $("input[id$='hfShtimModifikim']");
    var hfLidhur = $("input[id$='hfLidhur']");
    var arrTabela = ['tblFillim', "tblFund"];
    var arrPrind = ["dvFillim", "dvFundi"];
    var colKontrollet = result.colKontroll;   //[0];
    var colAtrTrupi = result.colAtrTrupi;   //[1];
    var grida = $('#rowed5');
    formatNumriZgjedhur = result.formatNumri;   //[7];
    mbushArrayMagazinat();

    var vlerat = myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, undefined, hf, '', arrTabela, undefined, undefined, hfLidhur, arrPrind);
    $("#divgride1").show();
    $("#dvFillim").show();
    cmbKonfigurimi.ShowDropDown();
    cmbKonfigurimi.HideDropDown();
    $("#dvFundi").show();
    vendosDateDefault();
    colGrida = result.colGrida;   //[2];
    colKushte = result.colKushte;   //[3];
    colAlterKusht = result.colAlterKusht;    //[4];
    var kodniveli = result.kodniveli;   //[5];
    $('#HfGridCol').val(JSON.stringify(colGrida));
    var hfMag = $("#hfLupaMagazina")[0];

    $('#hfKontrolletNrAutom').val(JSON.stringify(colKontrollet));
    $('#hfAtributeNrAutom').val(JSON.stringify(colAtrTrupi));
    if (pageState.lloji == "shtim") {
        hfNrAuto.Clear();
        hfNrAutoShitje.Clear();
        vendosNrAutomatik(colAtrTrupi, colKontrollet);
    }

    for (var i = 0; i < colKontrollet.length - 1; i++) {
        //id e konfigurimit te lupes vendoset ne hidden field
        //vlera e saj do t'i kalohet si query string hapjes se popUp-it

        if (colKontrollet[i].KodKontrolli == "btneMagazina") {

            hfMag.value = colAtrTrupi[i].IdKonfigAmbjenteLupa.toString(); //merret id e konfigurimit te lupes per lupen e magazines ne forme      
        }
    }


    for (j = 0; j < colKushte.length; j++) {
        var kusht = colKushte[j];
        switch (kusht.Kodi) {
            case "ZIA":
                    pageState.kushte[kusht.Kodi] = parseInt(kusht.Vlera);
                    break;
            case "ZADHG":
                pageState.kushte[kusht.Kodi] = kusht.Alternativa == "Shto ne rresht te ri" ? true : false;
                break;
            case "F":
                pageState.kushte[kusht.Kodi] = kusht.Alternativa == "Sasia" ? 1 : kusht.Alternativa == "Rreshti tjeter" ? 2 : 0;
                //fokus = pageState.kushte[kusht.Kodi]; 
                break;
            default:
                break;
        }
    }

    hapMbyllInfo($('#hfHapurMbyllur').val() == 'True');
    grida.jqGrid('GridUnload', "rowed5");
    isLidhur = (hfLidhur.val().toLowerCase() === 'true');
    formGridColsArray(isLidhur);
    grida.setLastSel2(-1);
    grida = $('#rowed5');
    ruajFormatetNeGride(grida, formatNumriZgjedhur);
    inicializoGride(isLidhur);
    mbushGrideNgaHiddenFieldet(isLidhur);

    Utils.konfiguroAccorditionPasKonfigDokumenti(varKonfig.identifikuesPerLocalStorageKey);
}

function DateChanged(s, e) {
    var hfKontrollet = JSON.parse($('#hfKontrolletNrAutom').val());
    var atributet = JSON.parse($('#hfAtributeNrAutom').val());
    vendosNrAutomatik(atributet, hfKontrollet);
}


var cmimzero = 1;
/*
Function: ndryshoKonfigurimin
    
Therret funksionin <callWebserviceKonfigurimi> per te vendosur nje konfigurim te ri.
*/
function ndryshoKonfigurimin() {
    var pershkKonfigAmb = cmbKonfigurimi.GetSelectedItem().GetColumnText("PershkrimKonfigAmbjente");
    if (pershkKonfigAmb != undefined)
      //  lblKonfigurimi.SetText(pershkKonfigAmb);
        $('#kokeKonfigurimi').text(hfState.Get("MenuKokeDokumenti") +': ' + pershkKonfigAmb);
    callWebserviceKonfigurimi(549, cmbKonfigurimi.GetText());
}


/*
Function: SucceededCallbackNiveli
    
Ndryshon vleren e zgjedhur te konfigurimit sipas nivelit te zgjedhur.
Therret funksionin <ndryshoKonfigurimin>.
*/
function ButtonClickMagazina() {
    var hfKl = document.getElementById("hfLupaMagazina");
    var queryStr = hfKl.value;
    popupUniversal.SetHeaderText(hfState.Get("msgZgjidhMagazinen"));
    popupUniversal.SetContentUrl('LupaMagazina.aspx?idKonfigAmbjente=' + queryStr);
    popupUniversal.SetSize(widthLupaMagazina, heightLupaMagazina);
    popupUniversal.Show();
}


function SucceededCallbackFormatNumri(result) {
    var formatNumri = result;
    var grida = $('#rowed5');
    ndryshoKonfigFormatNumri(grida, formatNumri);
    vendosKonfigFormatNumri();
}

/*
Function: TextChangedLloji
    
Ben ndryshime ne gride ne varesi te llojit te veprimit te zgjedhur (hyrje, Dalje apo Transferim)
*/
function TextChangedLloji() {
    var lloji = cmbLloji.GetText();
    var mod;
    if (pageState.lloji == 'modifikim')
        mod = true;
    else mod = false;
    if (cmbLloji.GetText() != "")
        callWebserviceNiveliNew(cmbLloji.GetValue(), 'inventarizim', mod);
    else
        callWebserviceNiveliNew('', 'inventarizim', mod);
}

function callWebserviceNiveliNew(lloji, tipi, mod) {

        $.ajax({ url: Utils.getServerApiUrl("Rregjistrime", "ktheTemplateNiveli"), data: JSON.stringify({ idNiveli: lloji, veprimi: tipi, mod: mod, idPerdoruesi: pageState.idPerdoruesi, idNdermarrje: pageState.idNdermarrje, idGjuha: pageState.idGjuha }) }).done(SucceededCallbackNiveliNew);
}

function SucceededCallbackNiveliNew(colModelet) {
    if (pageState.lloji == "shtim") {
        cmbKonfigurimi.ClearItems();
        for (i = 0; i < colModelet.length; i++) {
            cmbKonfigurimi.AddItem([colModelet[i].KodKonfigAmbjente, colModelet[i].PershkrimKonfigAmbjente], colModelet[i].IdKonfigAmbjente); //AddItem(teksti, vlera);
        }
        cmbKonfigurimi.SelectIndex(0);
    }
    ndryshoKonfigurimin();
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

        editorKodi = grida.getTekstQelize('txtKodbari', idTe[i]);
        editorEmertimi = grida.getTekstQelize('txtSerial', idTe[i]);
        editorSasia = grida.getTekstQelize('txtSasia', idTe[i]);
        if (editorKodi != "" || editorEmertimi != "") { trupiBosh = false; }

    }
    var rreshtaTeGrides = grida.getTeDhenaRreshti();
    $('#gridDataObject').val(JSON.stringify(rreshtaTeGrides));
    unformatoFushaDevi();
}

function ruajKolonatEGrides(grida) {
    var idGride = colGrida[0].IdKoka;
    myJQGrid.ruajKolonatEGrides(grida, idGride, pageState.idGjuha, pageState.idNdermarrje, pageState.idViti, pageState.idPerdoruesi)
}


/*
Function: vendosVleftat
    
Merr dhe validon vlerat e sasise dhe cmimit te caktuara ne gride dhe therret funksionin <updateTotalet>
*/
function vendosVleftat(id, indexRow, e) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var sasia = grida.getTekstQelize(id, idRresht);
    if (sasia == '.') {
        grida.setTekstQelize(id, idRresht, '0.');
        return;
    }   
    if (e.which == 13)
        lostFocusKoloneFundit();
}


function kontrolloVlereBosh() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var sasia = grida.getTekstQelize('txtSasia', idRresht);

    if (sasia === '' || isNaN(parseFloat(sasia))) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiaDuhetNumer"));
        sasia = grida.getVlereDefault('txtSasia');
        grida.setTekstQelize('txtSasia', idRresht, sasia);

    }
    if (sasia < 0) {
        myMesazh.ShtoMesazhGabimi("Sasia duhet te jete me e madhe se 0!");
        sasia = grida.getVlereDefault('txtSasia');
        grida.setTekstQelize('txtSasia', idRresht, sasia);

    }

}

function vendosNrAutomatik(colAtrTrupi, colKontrollet) {
    myNrAuto.vendosNrAutomatik(colAtrTrupi, colKontrollet, dteDtDok.GetDate());
}

/*
Function: pastroFushatKokes
    
Pastron fushat pasi eshte bere ruajtja apo modifikimi per te bere gati ambjentin per shtim.
*/
function pastroFushatKokes() {
    btneMagazina.SetValue(null);;
    if (btneMagazina.GetItemCount() == 1)
        btneMagazina.SetSelectedIndex(0);
    txtNrDok.SetText('');
    txtPershkrimi.SetText('');
    txtSkano.SetText('');
    var hf = document.getElementById("status1");
    hf.value = "false";
    vendosDateDefault();
    pyeturDoni = 0;
}

var dtDokumentit;


function vendosDateDefault() {
    if (pageState.lloji == "shtim") {
        var hfPeriudheObj = window.parent.lexoHfPeriudhe();
        dteDtDok.SetDate(Utils.ktheDateDefault(hfPeriudheObj));
        var dataSot = Utils.zeroOren(new Date());
        dteDtRegjistrimi.SetDate(dataSot);
    }
}

/*
Function: isValidKoka
    
Kontrollon nese jane plotesuar fushat e detyrueshme te kokes se dokumentit dhe validon daten.
*/
function isValidKoka() {
    var grida = $('#rowed5');
    //!Utils.nrWsRrugesManager.kanePerfunduarWs() {
    //    myMesazh.ShtoMesazhGabimi(hfState.Get("msgPoTransferohetTeDhenatShtypniPerseriRuaj"));
    //    return false;
    //}
    if (cmbLloji.GetText() == "") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniLlojin"));
        return false;
    }
    else if (txtNrDok.GetText() == "") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgShenoniNumrinEDokumentit"));
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

/*
Function: EndRequestHandler
    
Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe array-t e perdorura.
Shiko funksionet <pastro>, <pastroFushatKokes> dhe <ndryshoKonfigurimin>.
*/
function EndRequestHandler(sender, args) {
    formatoFushaDevi();
    var hf = document.getElementById("status1");
    if (hf.value == "true") {
        myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimInventarizimi.aspx?lloj=' + Utils.getUrlVar('lloj') + '&shtim_modifikim=shtim', true);

    } else click = false;
}

var shtoTimer;
function shtoTimedClick(e) {
    if (!Utils.nrWsRrugesManager.kanePerfunduarWs())
        shtoTimer = setTimeout(function () { shtoTimedClick(e) }, 500);
    else {
        clearTimeout(shtoTimer);
        myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimInventarizimi.aspx?lloj=' + Utils.getUrlVar('lloj') + '&shtim_modifikim=shtim', true);
        e.processOnServer = false;
    }
}
/*
Function: menuClick

Percakton veprimin qe kryhet nese klikohet nje nga butonat e menuse.
*/
function menu_click(s, e) {
    $('#hfRuajDraft').val(e.item.name);
    if (e.item.name === 'Ruaj' || e.item.name === 'RuajPrint') {
        myMesazh.vendosServer();
        if ($("input[id$='hfAutorizimi']").val() === 'False') {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukKeniAutorizimPerTeRuajturKeteDok"));
            Utils.hiqLoadingGif();;
            click = false;
            e.processOnServer = false;
            return;
        }
        var valid = myFaqeCelje.validim(s, e);
        if (!valid) {
            Utils.hiqLoadingGif();;
            e.processOnServer = false;
            click = false;
            return;
        }
        if (isValidKoka()) {
            Utils.shfaqLoadingGif();;
            RuajClick(s, e);
        }
        else {
            e.processOnServer = false;
            click = false;
        }
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
            Utils.hiqLoadingGif();;
            e.processOnServer = false;
            click = false;
            return;
        }
        var isValid = myFaqeCelje.validim(s, e);
       // myFaqeCelje.validim(s, e);
        if (isValid) {
            Utils.shfaqLoadingGif();;
            RuajClick(s, e);
        }
        else {
            e.processOnServer = false;
        }
    }
    else if (e.item.name == 'Kerko') {
        myFaqeCelje.kontrolloTeDrejta('RegjistrimInventarizimi.aspx?lloj=' + Utils.getUrlVar('lloj'), null, true);
        e.processOnServer = false;
    }
    else if (e.item.name == 'Shto') {
        shtoTimer = setTimeout(function () { shtoTimedClick(e) }, 500);
    }
    else if (e.item.name == 'Fshi') {
        if ($("input[id$='hfAutorizimi']").val() == 'False') {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukKeniAutorizimPerTeRuajturKeteDok"));
            Utils.hiqLoadingGif();; click = false;
            e.processOnServer = false;
            return;
        }
        if (Utils.getUrlVar('krahasuar') == 'Po')
        {
            lblMsgbox.SetText("Ky dokument eshte perdorur ne inventarizim! Jeni i sigurt?")
        }
       
        popFshi.Show();
        e.processOnServer = false;
    }
    else if (e.item.name == 'Anullo') {
        myFaqeCelje.kontrolloTeDrejta('RegjistrimInventarizimi.aspx?lloj=' + Utils.getUrlVar('lloj'));
        e.processOnServer = false;
        click = false;
    }
}

function Kthehu(mesazh) {
    myFaqeCelje.kontrolloTeDrejta('RegjistrimInventarizimi.aspx?lloj=' + Utils.getUrlVar('lloj') + '&ruaj=po');

}

/*
Function: RuajClick

Validon dokumentin dhe therret funksionin <merrTeDhena> per te marre te dhenat e grides kur klikohet butoni Ruaj.
*/

function RuajClick(s, e) {
    var grida = $('#rowed5');
    if (click) {
        e.processOnServer = false; Utils.hiqLoadingGif();;
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


    }
    else {
        e.processOnServer = false; click = false; Utils.hiqLoadingGif();;
    }
}

/*
Function: PastroClick

Pastron koken e dokumentit dhe array-t e perdorura deh inicializon griden.
Shiko funksionet <pastro>, <pastroFushatKokes> dhe <inicializoGride>.
*/
function PastroClick() {
    $("input[id$='hfLidhur']").val(false);
    $("input[id$='hfAutorizimi']").val(true);

    pastroFushatKokes();
    var hf = $("#hfShtimModifikim");
    ASPxMenu1.GetItemByName('Shto').SetVisible(true);
    ASPxMenu1.GetItemByName('Ruaj').SetVisible(true);
    ASPxMenu1.GetItemByName('Fshi').SetVisible(false);
    ASPxMenu1.GetItemByName('Draft').SetVisible(true);
    hf.val("shtim");
    pageState.lloji = "shtim";
    myMenu.menuSipasTeDrejtaRegjistrim(hf, hfTeDrejta);
    TextChangedLloji();
    $("#ASPxSplitter1_hl").empty();
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


var eshteAfatShkurter = Utils.getUrlVar('lloj') == 'ash';
function KaloBarkod(s, e) {
 
}

function ktheArtInfo(kodbar, idRreshti, idNdermarrje, kodMag, eshteAfatShkurter, data) {
    if (!eshteAfatShkurter){
        myMesazh.ShtoMesazhInformues("Info-ja per artikujt afatgjate nuk eshte bere akoma e mundur!");
        return;
    }
    $.ajax({
        pritPergjigje: false,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheArtInfo"),//ktheTeDhenaKodBar
        data: JSON.stringify({
            kodbar: kodbar, idNdermarrje: idNdermarrje,
            idInfoArt: eshteInfoHapur() ? pageState.kushte.ZIA : 0, //vetem nqs info eshte hapur e marrim
            mag: kodMag, eshteAfatShkurter: eshteAfatShkurter, data: data,
            idViti:  pageState.idViti, idPerdoruesi: pageState.idPerdoruesi
        })
    }).done(function (result) {
        doneKtheArtInfo(result, idRreshti, kodbar);
    }).fail(function (x) { console.log('ska lidhje me serverin per te kontrolluar kodbarin') });
}
function ngjyrosEkzistonDheSelekt(grida, idRreshti, id, ekziston) {
    console.log("ngjyrosEkzistonDheSelekt("+grida+", "+idRreshti+", "+id+", "+ekziston+")");
    grida.jqGrid('saveRow', idRreshti, null, 'clientArray', {}, null);
    grida.ngjyrosEkziston(idRreshti, "txtKodbari", ekziston);
    grida.jqGrid("editRow", idRreshti, false);
    $("#" + id + idRreshti).focus();
}
function callWebServiceInfoRow(idRreshti) {
    var grida = $("#rowed5");
    var id = eshteAfatShkurter ? "txtKodbari" : "txtSerial";
    var kodbari = grida.getTekstQelize(id, idRreshti);
    if (kodbari == "")
        return;
    var kodi = grida.getTekstQelize('txtKodi', idRreshti);
    if (kodi == "") {
        ngjyrosEkzistonDheSelekt(grida, idRreshti, id, false);
        pastroInfoArt();
        return;
    }
    ngjyrosEkzistonDheSelekt(grida, idRreshti, id, true);
    if (!eshteInfoHapur()) return;
    var artikulli = pageState.cache.kodbare[kodbari];    
    if (artikulli && typeof (artikulli.infoArt) != "undefined") {
        SucceededCallbackInfoArt(artikulli.infoArt, idRreshti);
        return;
    }
    else {
        var kodMag = btneMagazina.GetText();
        if (!kodMag) {
            myMesazh.ShtoMesazhGabimi("Duhet te zgjidhni nje magazine");
            return;
        }
        ktheArtInfo(kodbari, idRreshti, pageState.idNdermarrje, kodMag, eshteAfatShkurter, dteDtDok.GetDate(), true);
    }
}

function doneKtheArtInfo(result, idRreshti, kodbar) {
    var grida = $('#rowed5');   
    //grida.jqGrid('saveRow', idRreshti, null, 'clientArray', {}, null);
    if (!result) {
        grida.ngjyrosEkziston(idRreshti, "txtKodbari", false);
        //if (editRow)
        //    grida.jqGrid("editRow", idRreshti, false);
        //else
        //    lostFocusKoloneFundit();
        pastroInfoArt();        
        myJQGrid.setFokus(pageState.kushte.F, lostFocusKoloneFundit, idRreshti, "#txtSasia");
        return;
    }
   
    grida.ngjyrosEkziston(idRreshti, "txtKodbari", true);
    //if (editRow)
    //    grida.jqGrid("editRow", idRreshti, false);
    var artikulli = result.artikulli;
    grida.setTekstQelize('txtPershkrimi', idRreshti, artikulli.PershkrimArtikulli);
    grida.setTekstQelize('txtKodi', idRreshti, artikulli.KodArtikulli);
    //grida.setTekstQelize('txtSasia', idRreshti, 1);
    if (result.infoArt) {
        result.infoArt.colInfoTrupi.unshift({ PershkrimKolone: "Kodbari", EmerKolone: "Kodbari" });
        result.infoArt.vlerat.unshift(kodbar);
        artikulli.infoArt = result.infoArt;
        SucceededCallbackInfoArt(artikulli.infoArt, idRreshti);        
    }
    pageState.cache.kodbare[kodbar] = artikulli; //e fusim nqs kemi info
    myJQGrid.setFokus(pageState.kushte.F, lostFocusKoloneFundit, idRreshti, "#txtSasia");
}




//---------------------------------- FUNKSIONE INFO -----------------------------------------

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

var mbyll = true;
function RuajHapurMbyllurminus(hapur) {
    mbyll = false;
    if (!hapur) { splitter.GetPane(1).CollapseBackward(); $('#hfHapurMbyllur').val('False'); }
    else
        $('#hfHapurMbyllur').val('True')
    $.ajax({ pritPergjigje: true, url: Utils.getServerApiUrl("Rregjistrime", "ruajHapurMbyllurminus"), data: JSON.stringify({ hapur: hapur, idperdorues: pageState.idPerdoruesi, idperdoruesveprimi: pageState.idPerdoruesi }) }).done(SuccededCallbackHapurMbyllur);
}

function SuccededCallbackHapurMbyllur(result) {
    myMesazh.ShtoMesazhSuksesi(result);
}


function HeaderClick(s, e) {
    if (!mbyll) { e.cancel = true; mbyll = true; } //per rastet kur shtyp butonat + dhe -
}

function Expanded() {
    lbxZgjedhur.SetHeight(lbxZgjedhur.GetItemCount() * 23 + 28);
    //lbxLlogari.SetHeight(lbxLlogari.GetItemCount() * 22 + 28);
    //lbxKF.SetHeight(lbxKF.GetItemCount() * 22 + 28);
}

function KontrolloTeDrejta(s) {
    var alti = $(s.GetValue()).attr('alt');
    //    if (s.GetText().split('alt="')[1].split('"')[0] == "Artikulli" && $('#hfTeDrejtaInfoArt').val() == 'False')
    if (alti == "Artikulli" && $('#hfTeDrejtaInfoArt').val() == 'False')
        s.SetEnabled(false);
    if (alti == "KF" && $('#hfTeDrejtaInfoKF').val() == 'False')
        s.SetEnabled(false);
}

function ButtonClickNavBar(s) {
    if (s.GetText().split('alt="')[1].split('"')[0] == "Artikulli") {
        myButtonClickLupa.LupaUniversal_Click('Zgjidhni konfigurimin e infos se artikullit', 'LupaKonfigurimInfo.aspx?id=' + idInfoArt + '&ruaj=po', 600, 500);
        return;
    }
    if (s.GetText().split('alt="')[1].split('"')[0] == "KF") {
        myButtonClickLupa.LupaUniversal_Click('Zgjidhni konfigurimin e infos se klient/furnitorit', 'LupaKonfigurimInfo.aspx?id=' + idInfoKf + '&ruaj=po', 600, 500);
        return;
    }
    if (s.GetText().split('alt="')[1].split('"')[0] == "Llogari") {
        myButtonClickLupa.LupaUniversal_Click('Zgjidhni konfigurimin e infos se llogarise', 'LupaKonfigurimInfo.aspx?id=' + idInfoLlog + '&ruaj=po', 600, 500);
        return;
    }
}



function pastroInfoArt() {
    navbar.GetGroupByName('Artikulli').SetExpanded(false);
    lbxZgjedhur.ClearItems();
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
    var artExpanded = navbar.GetGroupByName('Artikulli').GetExpanded();
    navbar.GetGroupByName('Artikulli').SetExpanded(!artExpanded);
    navbar.GetGroupByName('Artikulli').SetExpanded(artExpanded);  
}

function SucceededCallbackInfoArt(result, idRreshti) {
    var idRow = $('#rowed5').getLastSel2();
    if (result && result.colInfoTrupi && result.colInfoTrupi.length != 0) {//idRreshti == idRow &&
        navbar.GetGroupByName('Artikulli').SetExpanded(true);
        vendosInfo(lbxZgjedhur, result);
    }
    else {
        if (!(result && result.colInfoTrupi))
            console.error("trupi infos vjen bosh");
    }
}

function eshteInfoHapur() {
    return !splitter.GetPane(1).IsCollapsed();
}

function spliterPaneExpanding(s, e) {
    var idRow = $('#rowed5').getLastSel2();
    hapMbyllInfo(true);
    callWebServiceInfoRow(idRow);
}

function spliterPaneCollapsing(s, e) {
    hapMbyllInfo(false);
}

function hapMbyllInfo(infoExpanded) {
    if (infoExpanded && (pageState.kushte.ZIA || infoKf || infoLl)) {
        if (!eshteInfoHapur())
            splitter.GetPane(1).Expand();
        if (pageState.kushte.ZIA)
            navbar.GetGroupByName('Artikulli').SetVisible(true);
        else
            navbar.GetGroupByName('Artikulli').SetVisible(false);
        navbar.GetGroupByName('Artikulli').SetExpanded(false);
    }
    else if (!infoExpanded && eshteInfoHapur()) {
        if (pageState.kushte.ZIA)
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
function spliterPaneResized(s, e) {
    if ($('#divgride2').width() != null) {
        myJQGrid.fixGridWidth($('#rowed5'), $('#divgride2'));
    }
}