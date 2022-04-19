;
var kodkodbar = 1;
var lidhur = false;
var arrayMeMagazina = new Array();
var colMagazina;
var magazina1;
var arrayIdKolonaGrides = new Array();
var arrayPershkrimiKolonaGrides = new Array();
var arrayVisibleKolonaGrides = new Array();
var arrayWidthKolonaGrides = new Array();
var arrayReadOnlyKolonaGrides = new Array();
var arrayRenditjeKolonaGrides = new Array();
var widthLupaArtikull = 850;
var heightLupaArtikull = 600;

var widthLupaMagazina = 600;
var heightLupaMagazina = 600;
var widthLupaPeriudha = 600;
var heightLupaPeriudha = 600;
var widthLupaKerko = 600;
var heightLupaKerko = 600;
var editorData;


var varKonfig = {
    identifikuesPerLocalStorageKey: 'RivleresimAmortizimi'
};


jQuery(document).ready(function () {
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
        editorData = dteDtDok;
        document.getElementById("kokeKonfigurimi").innerHTML = hfState.Get("MenuKokeDokumenti");
        document.getElementById("trupKonfigurimi").innerHTML = hfState.Get("MenuTrupDokumenti");
        document.getElementById("fundKonfigurimi").innerHTML = hfState.Get("MenuFundDokumenti");
        identifikuesPerPopupDokumentat = "RivleresimeAmortizimi";
        identikuesPerPopupArtikulli = "RivleresimeAmortizimi";
        identifikuesPerPopupMagazina = "RivleresimeAmortizimi";
        changeName();
        myMesazh.shtoHandler();
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(EndRequestHandler);
    }
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
    var arrayPershkrime = [arrayPershkrimiKolonaGrides[0], arrayPershkrimiKolonaGrides[1], arrayPershkrimiKolonaGrides[2], arrayPershkrimiKolonaGrides[3], arrayPershkrimiKolonaGrides[4], arrayPershkrimiKolonaGrides[5], arrayPershkrimiKolonaGrides[6], arrayPershkrimiKolonaGrides[7], arrayPershkrimiKolonaGrides[8], arrayPershkrimiKolonaGrides[9], arrayPershkrimiKolonaGrides[10]];
    var arrayModel = [
        { name: arrayIdKolonaGrides[0], index: arrayIdKolonaGrides[0], width: arrayWidthKolonaGrides[0], hidden: arrayVisibleKolonaGrides[0], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemNrRendor, custom_value: myJQGrid.myValueTextBox } },
		{ name: arrayIdKolonaGrides[1], index: arrayIdKolonaGrides[1], width: arrayWidthKolonaGrides[1], hidden: arrayVisibleKolonaGrides[1], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myElemKodi, custom_value: myJQGrid.myValueTextBox } },
		{ name: arrayIdKolonaGrides[2], index: arrayIdKolonaGrides[2], width: arrayWidthKolonaGrides[2], hidden: arrayVisibleKolonaGrides[2], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myElemEmertimi, custom_value: myJQGrid.myValueTextBox } },
		{ name: arrayIdKolonaGrides[3], index: arrayIdKolonaGrides[3], width: arrayWidthKolonaGrides[3], hidden: arrayVisibleKolonaGrides[3], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemComboMagazina, custom_value: myJQGrid.myValueCombo } },
		{ name: arrayIdKolonaGrides[4], index: arrayIdKolonaGrides[4], width: arrayWidthKolonaGrides[4], hidden: arrayVisibleKolonaGrides[4], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemGjendja, custom_value: myJQGrid.myValueTextBox } },
		{ name: arrayIdKolonaGrides[5], index: arrayIdKolonaGrides[5], width: arrayWidthKolonaGrides[5], hidden: arrayVisibleKolonaGrides[5], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemGjithsej, custom_value: myJQGrid.myValueTextBox } },
		{ name: arrayIdKolonaGrides[6], index: arrayIdKolonaGrides[6], width: arrayWidthKolonaGrides[6], hidden: arrayVisibleKolonaGrides[6], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemVjetor, custom_value: myJQGrid.myValueTextBox } },
		{ name: arrayIdKolonaGrides[7], index: arrayIdKolonaGrides[7], width: arrayWidthKolonaGrides[7], hidden: arrayVisibleKolonaGrides[7], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myelemIdKodi, custom_value: myJQGrid.myValueTextBox }, hidedlg: true },
		{ name: arrayIdKolonaGrides[8], index: arrayIdKolonaGrides[8], width: arrayWidthKolonaGrides[8], hidden: arrayVisibleKolonaGrides[8], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemSeriali, custom_value: myJQGrid.myValueTextBox } },
		{ name: arrayIdKolonaGrides[9], index: arrayIdKolonaGrides[9], width: arrayWidthKolonaGrides[9], hidden: arrayVisibleKolonaGrides[9], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemAmFill, custom_value: myJQGrid.myValueTextBox } },
		{ name: arrayIdKolonaGrides[10], index: arrayIdKolonaGrides[10], width: arrayWidthKolonaGrides[10], hidden: arrayVisibleKolonaGrides[10], classes: classes, sorttype: "int", editable: true, edittype: 'custom', editoptions: { custom_element: myElemButtonFshi, custom_value: myValueButtonFshi }, hidedlg: true },
    ];


    var selektoriGrides = "#rowed5";

    var gridParams = {
        emergride: selektoriGrides,
        arrayPershkrime: arrayPershkrime,
        arrayModel: arrayModel,
        lidhur: isLidhur,
        emerEditorKodi: "txtKodi",
        emerMag1: 'txtMagazina',
        widthi: $('#divgride2').width() - 5,
        fokus: fokusi,
        mosshtorresht: false,
        emerEditorMagazina: "txtMagazina",
        lostFocusKoloneFundit: lostFocusKoloneFundit,
        resetRreshtKorent: resetRreshtKorent,

        autocompleteList: [{ emerEditor: "txtKodi", selectFunc: selectFunc, changeFunc: changeFunc, shtoDataKod: false }],

        konfigToolbar: {
            konfigGrid: $('#hfTeDrejtaKonfGride').val(),
            shtoArtikull: $('#hfTeDrejtaArtRi').val(),
            modArtikull: $('#hfTeDrejtaArtMod').val(),
            ruajKolonatEGrides: ruajKolonatEGrides,
            exportExcel: true,        //Ben enable exportin e F
            EmerExporti: "Rivleresim Amortizimi" //Emri i filet .xls qe gjenerohet
        }
    };
   return myJQGrid.initGride(gridParams);
}

    //myJQGrid.inicializoGride("#rowed5", arrayPershkrime, arrayModel, isLidhur,
    //    lastsel2, "#txtKodi", undefined, undefined, undefined, 'txtMagazina', undefined, $('#divgride2').width() - 5,
    //    undefined, fokusi, null, null, undefined, undefined, undefined, undefined, $('#hfTeDrejtaKonfGride').val(), $('#hfTeDrejtaArtRi').val(),
    //    undefined, undefined, false, "#txtMagazina", undefined);


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
    grida.setVlereDefault('txtGjendja', 0);
    grida.setVlereDefault('txtGjithsej', 0);
    grida.setVlereDefault('txtVjetor', 0);
    grida.setVlereDefault('txtAmFillestar', 0);
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
    grida.setShifraPasPresjes('txtGjendja', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtGjithsej', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtVjetor', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtAmFillestar', formatNumri.ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtTotali, formatNumri.ShifraPasPresjesVlefta);
}

/*
Formaton vlerat e fushave sipas formatit perkates.
*/
function vendosKonfigFormatNumri() {
    var grida = $('#rowed5');
    var idTe = grida.jqGrid('getDataIDs');
    for (j = 0; j < idTe.length; j++) {
        var idRreshti = idTe[j];
        grida.formatoQelize('txtGjendja', idRreshti);
        grida.formatoQelize('txtGjithsej', idRreshti);
        grida.formatoQelize('txtVjetor', idRreshti);
        grida.formatoQelize('txtAmFillestar', idRreshti);
    } formatoFushaDevi();
}
function formatoFushaDevi() {
    Utils.formatoTextBox(txtTotali);

}
function unformatoFushaDevi() {
    Utils.unFormatoTextBox(txtTotali);

}
function selectFunc(event, ui, emerfushe, idArt, kodArt) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    var detajim = -1;
    var plotesuarMagKoka = (btneMagazina.GetValue() != null);
    var lloj = Utils.getUrlVar('lloj');
    var magazine = $('#txtMagazina' + idRow).val();
    if (idArt != undefined && kodArt != undefined && idArt != "" && kodArt != "") {
        $(emerfushe).val(kodArt);
      
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraAQTMeID"),
            data: JSON.stringify({ idja: idArt, plotesuarMagKoka: plotesuarMagKoka, rreshti: idRow, data: dteDtDok.GetDate(), idmagazina: magazine, idndermarja: hfState.Get('idNdermarrje'), rezerva: (hfState.Get("AR") == "Po" && Utils.getUrlVar('lloj') != "amortizim") })
        }).done(SucceededCallbackArtNew)
        return false;
    }
    if (ui.item != null) {
        $(emerfushe).val(ui.item.label);
    }
  
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraAQTMeID"),
        data: JSON.stringify({ idja: ui.item.value, plotesuarMagKoka: plotesuarMagKoka, rreshti: idRow, data: dteDtDok.GetDate(), idmagazina: magazine, idndermarja: hfState.Get('idNdermarrje'), rezerva: (hfState.Get("AR") == "Po" && Utils.getUrlVar('lloj') != "amortizim") })
    }).done(SucceededCallbackArtNew)
    return false;
}

function changeFunc(event, ui, emerKodi, index) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    var emerfushe = '#' + emerKodi + idRow;
    var detajim = -1;
    var plotesuarMagKoka = (btneMagazina.GetValue() != null);
    //var index = -1;
    //var idKod = 'txtKodi';
    var lloj = Utils.getUrlVar('lloj');
    //if (emerKodi === undefined || emerKodi === null)
    //    index = myJQGrid.getIndexFromEvent(event, idKod);
    //else
    //    index = emerKodi.split(idKod)[1];

    if (index == idRow) {
        var magazine = grida.getTekstQelize('txtMagazina', index);
        if (ui == null || ui.item == null) {
            var kodi = $(emerfushe).val();
            if (typeof (kodi) != "undefined" && kodi != undefined && kodi != "") {
             
                $.ajax({
                    url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraAqtMeKodOseKodBar"),
                    data: JSON.stringify({ kodi: kodi, rreshti: idRow, data: dteDtDok.GetDate(), magazina: magazine, idndermarje: hfState.Get('idNdermarrje'), idperdoruesi: hfState.Get('idPerdoruesi'), rezerva: (hfState.Get("AR") == "Po" && Utils.getUrlVar('lloj') != "amortizim") })
                }).done(SucceededCallbackArtNew);
                return;

            }

            vendosArt([index, null]);
            return;

        }
        var idmagazine = $('#txtMagazina' + idRow).val();
       
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraAQTMeID"),
            data: JSON.stringify({ idja: ui.item.value, plotesuarMagKoka: plotesuarMagKoka, rreshti: idRow, data: dteDtDok.GetDate(), idmagazina: idmagazine, idndermarja: hfState.Get('idNdermarrje'), rezerva: (hfState.Get("AR") == "Po" && Utils.getUrlVar('lloj') != "amortizim") })
        }).done(SucceededCallbackArtNew)
        return;

        return;
    }
    var rreshti = grida.jqGrid('getRowData', index);

    if (ui == null || ui.item == null) {
        if (rreshti.txtKodi != "") {
            
            $.ajax({
                url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraAqtMeKodOseKodBar"),
                data: JSON.stringify({ kodi: rreshti.txtKodi, rreshti: index, data: dteDtDok.GetDate(), magazina: rreshti.txtMagazina, idndermarje: hfState.Get('idNdermarrje'), idperdoruesi: hfState.Get('idPerdoruesi'), rezerva: (hfState.Get("AR") == "Po" && Utils.getUrlVar('lloj') != "amortizim") })
            }).done(SucceededCallbackArtNew)
            return;
        }
        vendosArt([index, null]);
    }
}

function resetRreshtKorent(idRreshti) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    var idkodi = grida.getTekstQelize('txtIdKodi', idRreshti);//heq serialet
    if (hfSeriale.Contains(idkodi + '_' + idRreshti))
        hfSeriale.Remove(idkodi + '_' + idRreshti);
    if (idRreshti == idRow && grida.getTekstQelize('txtKodi', idRow) != undefined) {
        grida.setTekstQelize('txtGjendja', idRreshti);
        grida.setTekstQelize('txtKodi', idRreshti, '');
        grida.setTekstQelize('txtIdKodi', idRreshti, '');
        grida.setTekstQelize('txtEmertimi', idRreshti, '');
        grida.setTekstQelize('txtGjithsej', idRreshti);
        grida.setTekstQelize('txtVjetor', idRreshti);
        grida.setTekstQelize('txtAmFillestar', idRreshti);
        grida.setTekstQelize('txtSerial', idRreshti, '');
        updateTotalet();
        return;
    }
    grida.jqGrid('delRowData', idRreshti);
    return;
}

function SucceededCallbackArtNew(result) {
    vendosArt(result);
}

function vendosArt(result) {
    var grida = $("#rowed5");
    var idRow = grida.getLastSel2();
    if (result !== null) {
        var idRreshti = result[0];
        var artikulli = result[1];
        var gjendjetot = result[2];
        var kodi = "#txtKodi" + idRreshti;
        var idKodi = "#txtIdKodi" + idRreshti;
        var emerArt = '#' + 'txtEmertimi' + idRreshti;
        if (artikulli == null || artikulli == undefined || artikulli.IdArtikulli == -1) {
            resetRreshtKorent(idRreshti);
            return;
        }
        if (artikulli.Aktiv == false) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgArtikulliEshteInaktiv"));
            resetRreshtKorent(idRreshti);
            return;
        }
        if (!artikulli.LlojiArt) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukMundTeKryesniVeprimeAnalitikeMeArtikujAfatShkurter"));
            resetRreshtKorent(idRreshti);
            return;
        }
        if (!artikulli.MeRezerveRivleresimi && hfState.Get("AR") == "Po") {//meRezerve//todones
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukMundTeKryesniVeprimeAnalitikeMeArtikujJoRezerve"));
            resetRreshtKorent(idRreshti);
            return;
        }
        if (hfState.Get("AR") == "Po" && gjendjetot == 0)
        {
            myMesazh.ShtoMesazhGabimi("Nuk mund te kryeni verpime me artikull qe nuk ka gjendje ne kete magazine!");
            resetRreshtKorent(idRreshti);
            return;
        }
        // u komnetua sepse u kerkua qe te beheshin veprime edhe me artikuj pa serial, pika ALPHAWEB-2271

        //if (analitike == 1 && !artikulli.MeSerial) {

        //    myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukMundTeKryesniVeprimeAnalitikeMeArtMeSerialeTeNdashem"));
        //    resetRreshtKorent(idRreshti);
        //    return;
        //}
        if (analitike == 2) {
            if (kontrolloRow(idRreshti))
                return;
        }
        //heq serialet e artikullit te vjeter ne rast se po modifikohet i njejti rresht
        var idkodi = grida.getTekstQelize('txtIdKodi', idRreshti);
        if (hfSeriale.Contains(idkodi + '_' + idRreshti))
            hfSeriale.Remove(idkodi + '_' + idRreshti);

        if (idRreshti == idRow && grida.getTekstQelize('txtKodi', idRow) != undefined) {


            if ($(idKodi).val() == artikulli.IdArtikulli) {
                grida.setTekstQelize('txtKodi', idRreshti, artikulli.KodArtikulli);
                return; //eshte i njejti artikull 
            }

            HfArt.Set(artikulli.IdArtikulli, artikulli);
            grida.setTekstQelize('txtKodi', idRreshti, artikulli.KodArtikulli);
            myJQGrid.closeAutocomplete('txtKodi', idRreshti);
            grida.setTekstQelize('txtIdKodi', idRreshti, artikulli.IdArtikulli);
            if (hfState.Get("AR") == "Jo" || Utils.getUrlVar('lloj') != "amortizim")
                grida.setTekstQelize('txtGjendja', idRreshti, gjendjetot);

            var comboMag = $('#txtMagazina' + idRreshti);

            var mag = '';
            var selectedItem = btneMagazina.GetSelectedItem();
            if (selectedItem != null)
                mag = selectedItem.GetColumnText('Kodi');
            
            if (((artikulli.IdMagazina != 0) && (mag == ''))) //magazina 
                mag = artikulli.Magazina;
            // comboMag.replaceWith(myelemComboMagazina(mag, null, idRreshti).children()[0]);
            comboMag.zevendeso($(myelemComboMagazina(mag, null, idRreshti).children()[0]), idRreshti);

            if (pershk == 1) {
                grida.setTekstQelize('txtEmertimi', idRreshti, artikulli.PershkrimArtikulli)
                $(emerArt).attr('title', artikulli.PershkrimArtikulli);
            }
            else
                if (artikulli.PershkrimiAngArtikulli !== "")
                    grida.setTekstQelize('txtEmertimi', idRreshti, artikulli.PershkrimiAngArtikulli)
                else
                    grida.setTekstQelize('txtEmertimi', idRreshti, artikulli.PershkrimArtikulli)



            return;
        }

        if (grida.getTekstQelize('txtIdKodi', idRreshti) == artikulli.IdArtikulli) {
            grida.setTekstQelize('txtKodi', idRreshti, artikulli.KodArtikulli);
            return; //eshte i njejti artikull 
        }
        grida.setTekstQelize('txtKodi', idRreshti, artikulli.KodArtikulli);


        HfArt.Set(artikulli.IdArtikulli, artikulli);

        grida.setTekstQelize('txtKodi', idRreshti, artikulli.KodArtikulli);
        grida.setTekstQelize('txtIdKodi', idRreshti, artikulli.IdArtikulli);
        if (hfState.Get("AR") == "Jo" || Utils.getUrlVar('lloj') != "amortizim")
        grida.setTekstQelize('txtGjendja', idRreshti, gjendjetot);
        var mag = btneMagazina.GetSelectedItem().GetColumnText("Kodi");
        if (artikulli.IdMagazina != 0 && (mag == '')) //magazina
            mag = artikulli.Magazina;

        var magazinadefiperketgrupit = false;
        var magazinaepare = "";
        if (colMagazina !== "") {
            for (var i = 0; i < colMagazina.length; i++) {

                if (magazinaepare == "")
                    magazinaepare = colMagazina[i].Kodi;
                if (colMagazina[i].Kodi == mag) {
                    magazinadefiperketgrupit = true;

                }
            }
        }
        if (magazinadefiperketgrupit && mag != "")
            grida.setTekstQelize('txtMagazina', idRreshti, mag);
        else grida.setTekstQelize('txtMagazina', idRreshti, magazinaepare);

        if (pershk == 1)
            grida.setTekstQelize('txtEmertimi', idRreshti, artikulli.PershkrimArtikulli);
        else
            if (artikulli.PershkrimiAngArtikulli !== "")
                grida.setTekstQelize('txtEmertimi', idRreshti, artikulli.PershkrimiAngArtikulli);
            else
                grida.setTekstQelize('txtEmertimi', idRreshti, artikulli.PershkrimArtikulli);
        return;
    }
}

function kontrolloRow(rowid) {//po
    var grida = $("#rowed5");
    var name = grida.getTekstQelize('txtKodi', rowid);
    var mag = grida.getTekstQelize('txtMagazina', rowid);

    var gridIds = grida.jqGrid('getDataIDs');
    for (i = 0; i < gridIds.length; i++) {
        if (rowid == gridIds[i])
            continue;
        var editorkodi = grida.getTekstQelize('txtKodi', gridIds[i]);
        var editormag = grida.getTekstQelize('txtMagazina', gridIds[i]);
        if (editorkodi == name && editorkodi !== '' && mag == editormag && editormag !== '' && cmbKonfigurimi.GetText() != "FAFanalitike") {
            resetRreshtKorent(rowid);
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgKyArtikullNdodhetNjehereNeGride"));
            return true;
            break;
        }
    }
    return false;
}

function spliterPaneExpanding(s, e) {


}
function spliterPaneCollapsing(s, e) {

}
function dateGotFocus(s, e) {
    window.setTimeout(function () { s.SetCaretPosition(0); }, 0);
}

function spliterPaneCollapsed(s, e) {
    var grida = $('#rowed5');
    if ($('#divgride2').width() != null) {
        myJQGrid.fixGridWidth(grida, $('#divgride2'));
    }
}

function formGridColsArray(isLidhur) {
    var hfGridKod = $('#hfGridaKodi');
    var hfGridDetajim = $('#hfGridaDetajimi');
    var IdKonfigAmbjenteLupat = [{ hfVar: hfGridKod, kodiText: "txtKodi" }];
    myJQGrid.formArrayKolGrides($("input[id$='HfGridCol']"), arrayIdKolonaGrides, arrayPershkrimiKolonaGrides, arrayVisibleKolonaGrides, arrayReadOnlyKolonaGrides, isLidhur, arrayWidthKolonaGrides, IdKonfigAmbjenteLupat, arrayRenditjeKolonaGrides);
}

/*
Function: myElemNrRendor

Nderton nje textbox per te vendosur nr rendor te rreshtit.
*/
function myElemNrRendor(value, options) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[0];
    return myJQGrid.myElemNrRendor(value, options, idRow, 'txtNrRendor', grida);
}

/*
Function: myElemCmimi

Nderton nje textbox per te vendosur cmimin.
*/
function myElemGjendja(value, options) {
    var disable = 'False';
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    if (arrayReadOnlyKolonaGrides[4] == 'True')
        disable = 'True';
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, disable, idRow, 'txtGjendja', vendosVleftat, hfFormatNumri, kontrolloVlereBosh);
}

/*
Function: myElemKodi

Nderton nje textbox dhe nje buton per te zgjedhur artikullin ose makron sipas llojit te zgjedhur tek combo Kategoria
*/
function myElemKodi(value, options) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[1];
    return myJQGrid.myElemKodi(value, options, disabled, idRow, arrayIdKolonaGrides[1], ButtonClickKodi, keyPressKodi, changeFunc, fokusi, lostFocusKoloneFundit);
}
function myelemSeriali(value, options) {//po    
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    return myJQGrid.myElemKodi(value, options, arrayReadOnlyKolonaGrides[8], idRow, arrayIdKolonaGrides[8], ButtonClickSeriali, keyPressSeriali, changeFuncSeriali);
}
function myelemIdKodi(value, options) {//po
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    return myJQGrid.myElemIdKodi(value, options, arrayReadOnlyKolonaGrides[7], idRow, arrayIdKolonaGrides[7]);
}
function keyPressSeriali(e) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    if (e.which == 13) {
        var serial = grida.getTekstQelize('txtSerial', idRow);
        var idkodi = grida.getTekstQelize('txtIdKodi', idRow);
        if (grida.getTekstQelize('txtKodi', idRow) == "") {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniNjeArtikullAfatGjate"));
            grida.setTekstQelize('txtSerial', idRow, "");
            return;
        }
        if (HfArt.Contains(idkodi)) {
            var artikulli = HfArt.Get(idkodi);
            if (!artikulli.LlojiArt) {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniNjeArtikullAfatGjate"));
                grida.setTekstQelize('txtSerial', idRow, "");
                return;
            }
        }
        if (serial != "" && serial != "Me serial" && serial != "Pa serial") {
            var gridIds = grida.jqGrid('getDataIDs');
            var arrayMeSeriale = new Array();
            var a = 0;
            for (i = 0; i < gridIds.length; i++) {
                if (idRow == gridIds[i])
                    continue;
                if (hfSeriale.Contains(idkodi + '_' + gridIds[i])) {
                    arrayMeSeriale[a] = hfSeriale.Get(idkodi + '_' + gridIds[i]);
                    a++;
                }
            }
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "CelSerialNqsNukEkziston"), data: JSON.stringify({ kodserial: serial, idndermarje: hfState.Get('idNdermarrje'), idperdoruesi: hfState.Get('idPerdoruesi'), idartikulli: idkodi, lastsel: idRow, serialeekzistuese: "", serialeteperdoruraNeKeteFature: arrayMeSeriale, celnqsnukekziston: false, magazina: grida.getTekstQelize('txtMagazina', idRow), sasishuma: 0, iddokmodmagazine: 0 })
            }).done(SucceededCallbackSerial)

        }
        else {
            if (hfSeriale.Contains(idkodi + '_' + idRow)) {
                hfSeriale.Set(idkodi + '_' + idRow, "[]");

            }
        }
        grida.setTekstQelize('txtSerial', idRow, "");
    }


}
function SucceededCallbackSerial(result) {
    var grida = $('#rowed5');
    if (result[2] != "") {
        myMesazh.ShtoMesazhGabimi(result[2]);
        return;
    }

    if (hfSeriale.Contains(result[0] + '_' + result[4])) {
        hfSeriale.Set(result[0] + '_' + result[4], result[1]);

    }
    else hfSeriale.Add(result[0] + '_' + result[4], result[1]);

    var seriale = JSON.parse(result[1]);
    grida.setTekstQelize('txtSerial', result[4], seriale[0].AqtSerialKod);
    merrGjendjePerSerial(result[4])
}
function merrGjendjePerSerial(idrreshti) {
    var grida = $('#rowed5');
    try {
        var serial = grida.getTekstQelize('txtSerial', idrreshti);
    
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheRowGjendjeSerialPerMagazine"),
            data: JSON.stringify({ serial: serial, rreshti: idrreshti, data: dteDtDok.GetDate(), idndermarja: hfState.Get('idNdermarrje'), rezerva: (hfState.Get("AR") == "Po" && Utils.getUrlVar('lloj') != "amortizim") })
        }).done(SucceededCallbackGjendjaSipasMag);
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTeDhenave"));
    }
}

function changeFuncSeriali(event, ui, emerKodi, index) {
    //var idKod = 'txtSerial';
    var grida = $('#rowed5');
    //if (emerKodi === undefined || emerKodi === null)
    //    index = myJQGrid.getIndexFromEvent(event, idKod);
    ////////else
    //    index = emerKodi.split(idKod)[1];
    var idkodi = grida.getTekstQelize('txtIdKodi', index);
    if (!hfSeriale.Contains(idkodi + '_' + index) || hfSeriale.Get(idkodi + '_' + index) == "[]")
        grida.setTekstQelize('txtSerial', index, "");
}

function ButtonClickSeriali() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var idkodi = grida.getTekstQelize('txtIdKodi', idRresht);
    if (grida.getTekstQelize('txtKodi', idRresht) == "") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniNjeArtikullAfatGjate"));
        return;
    }
    if (HfArt.Contains(idkodi)) {
        var artikulli = HfArt.Get(idkodi);
        if (!artikulli.LlojiArt) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniNjeArtikullAfatGjate"));
            return;
        }

        myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhniSerialet"), 'LupaSeriale.aspx?vjennga=' + Utils.getUrlVar("lloj") + '&magazina=' + $('#rowed5').getTekstQelize('txtMagazina', idRresht) + '&id=' + $('#rowed5').getTekstQelize('txtIdKodi', idRresht) + '&lastsel=' + idRresht + '&mecope=' + !artikulli.MeSerial + '&sasia=' + 1 + '&data=' + dteDtDok.GetText() + '&iddokmag=0', 900, 600);
    }
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

Nderton nje textbox ku vendoset emertimi i artikullit apo makros
*/
function myElemEmertimi(value) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemEmertimi(value, arrayReadOnlyKolonaGrides[2], idRresht, arrayIdKolonaGrides[2]);
}




/*
Function: myElemSasia

Nderton nje textbox per te vendosur sasine.
*/
function myElemGjithsej(value, options) {//po
    var disable = 'False';
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if (arrayReadOnlyKolonaGrides[5] == 'True')
        disable = 'True';
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, disable, idRresht, 'txtGjithsej', vendosVleftat, hfFormatNumri, txtGjithsejOnFocusOut);
   
}

/*
Function: myElemVlefta

Nderton nje textbox per te vendosur vleften.
*/
function myElemVjetor(value, options) {
    var disable = 'False';
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var readonly = 'False'
    if (arrayReadOnlyKolonaGrides[6] == 'True')
        disable = 'True';
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, disable, idRresht, 'txtVjetor', vendosVleftat, hfFormatNumri, kontrolloVlereBosh);
}

function myElemAmFill(value, options) {
    var disable = 'False';
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var readonly = 'False'
    if (arrayReadOnlyKolonaGrides[7] == 'True')
        disable = 'True';
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, disable, idRresht, 'txtAmFillestar', vendosVleftat, hfFormatNumri, kontrolloVlereBosh);
}

/*
Function: fshiClicked

Fshin nje rresht te grides

Parameters:

index - Id e rreshtit qe do fshihet    
*/
function fshiClicked(index) {
    var grida = $('#rowed5');
    var idkodi = grida.getTekstQelize('txtIdKodi', index);
    if (hfSeriale.Contains(idkodi + '_' + index))
        hfSeriale.Remove(idkodi + '_' + index);
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

/*
Function: myelemComboMagazina

Nderton nje combobox per te zgjedhur magazinen.
*/
function myelemComboMagazina(value, options, idRreshti) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    if (idRreshti === undefined)
        idRreshti = idRow;
    if(btneMagazina.GetSelectedItem()!=null)
    magazina1 = btneMagazina.GetSelectedItem().GetColumnText("Kodi");
    if (value == "" && magazina1 != "" && magazina1 != undefined)
        value = magazina1;
    var idArt = grida.getTekstQelize('txtIdKodi', idRreshti);
    var llojartikulli = false;
    var arrayOptions;
    if (HfArt.Contains(idArt)) {
        var artikulli = HfArt.Get(idArt);
        arrayOptions = grida.formoArrayOptinosNjesia(artikulli);
        llojartikulli = artikulli.LlojiArt;
    }
    var arrayOptions = new Array(0);
    var j = 0;
    if (colMagazina !== "" || colMagazina !== undefined) {
        arrayOptions = new Array();
        for (var i = 0; i < colMagazina.length; i++) {
            var objNjesi = new Object();
            objNjesi.value = colMagazina[i].IdNjesiAdministrative;
            objNjesi.text = colMagazina[i].Kodi;
            objNjesi.desc = colMagazina[i].Pershkrimi;
            arrayOptions[j] = objNjesi;
            j++;
        }
    }
    var disabled = false;
    if (arrayReadOnlyKolonaGrides[3] == 'True')
        disabled = true;
    return myJQGrid.myElemCombo(value, arrayIdKolonaGrides[3], idRow, changeMag, arrayOptions, disabled);
}

function changeMag() {
    ///<summary> metode per ndryshimin e vleres se magazines. kontrollon nese magazina eksiston tek comboja e magazines dhe therret webservicet e infos nqs ka info <summary>
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    if ($('#txtMagazina' + idRow + ' option').length == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKjoMagazineNukEkziston"));
        $('#txtMagazina' + idRow).val('');
    }
    var idkodi = grida.getTekstQelize('txtIdKodi', idRow);//heq serialet
    if (kontrolloRow(idRow))
        resetRreshtKorent(idRow);
    if (hfSeriale.Contains(idkodi + '_' + idRow)) {
        hfSeriale.Remove(idkodi + '_' + idRow);
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgSerialetEArtikullitUhoqenSepseNukIPerkasinKesajMagazines"));
        grida.setTekstQelize('txtSerial', idRow, '');
    }
    try {
        var magazina = grida.getTekstQelize('txtMagazina', idRow);
        var serial = grida.getTekstQelize('txtSerial', idRow);
        var idja = idkodi;
        if (!(typeof (idja) == "undefined" || idja == undefined || idja == "" || idja == "0")) {
         
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "ktheRowGjendjeArtikulliPerMagazine"),
                data: JSON.stringify({ idja: idkodi, rreshti: idRow, data: dteDtDok.GetDate(), magazina: magazina, idndermarja: hfState.Get('idNdermarrje'), serial: serial, rezerva: (hfState.Get("AR") == "Po" && Utils.getUrlVar('lloj') != "amortizim") })
            }).done(SucceededCallbackGjendjaSipasMag);
        }
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTeDhenave"));
    }
}

function SucceededCallbackGjendjaSipasMag(result) {
    var grida = $('#rowed5');
    if (hfState.Get("AR") == "Jo" || Utils.getUrlVar('lloj') != "amortizim") {
        grida.setTekstQelize('txtGjendja', result[0], result[1]);
        kontrolloVlereBosh();
    }
    else if (result[1] == 0) {
        myMesazh.ShtoMesazhGabimi("Nuk mund te kryeni verpime me artikull qe nuk ka gjendje ne kete magazine!");

    }
}

/*
Function: callWebserviceKodi
    
Sugjeron listen e artikujve kur shkruajme te kodi.
Shiko funksionin <SucceededCallbackKodi>.
*/
function callWebserviceKodi(vlera) {
    var grida = $('#rowed5');
    try {
        var idNdermarrje = hfState.Get("idNdermarrje");
        var idPerdorues = hfState.Get("idPerdoruesi");
        $.ajax({    
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheACListeArtikujshKodPershkKodbarEShpejt"), data: JSON.stringify({ infixText: vlera, pershk: pershk, grup: '', idNdermarrje: idNdermarrje, idPerdoruesi: idPerdorues, artikujTeShitshem: false, merrVetemAfatgjate: true, merrSipasDetajimit: false, klasa: "" })
        }).done(SucceededCallbackKodi);
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
    }
    return;
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
        $('#hfTmpColMag').val(''); //boshatisim HF-ne qe mos te harxhojme kot memorie
    }
}

/*
Function: SucceededCallbackKodi
    
Sugjeron listen e artikujve kur shkruajme te kodi.
*/
function SucceededCallbackKodi(result) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    myJQGrid.SucceededCallbackKodi(result, '#txtKodi' + idRresht);

}

/*
Function: lostFocusKoloneFundit

Percakton veprimin qe kryhet kur heqim fokusin nga kolona e fundit e gride (ruhet rreshti korent dhe shtohet nje rresht i ri bosh i editueshem).
Ketu kolona e fundit eshte "Vlefta" (sepse eshte rasti kur nuk po behet transferim).
*/
function lostFocusKoloneFundit() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
   grida.lostFocusKoloneFundit();
    jQuery("#txtMagazina" + idRresht).focus();
    jQuery("#txtMagazina" + idRresht).blur();
    jQuery("#txtKodi" + idRresht).focus();
}

function mbushGrideNgaHiddenFieldet(isLidhur) {
    var grida = $("#rowed5");
    var idRresht = grida.getLastSel2();
    if (arrayPershkrimiKolonaGrides.length == 0)
        return;
    if ($('#hfShtimModifikim').val() == "shtim") {
        return;
    }
    if ($('#hfShtimModifikim').val() == "modifikim" || $('#hfShtimModifikim').val() === "klonim") {
        var colTrupMag = JSON.parse($('#HfColTrupMag').val());
        var colArt = JSON.parse($('#HfColArt').val());
        var colAmort = JSON.parse($('#HfColAmort').val());
        var colNjesAdminis = JSON.parse($('#HfColNjesAdminis').val());
        var colSeriale = JSON.parse($('#HfColDetArt').val());
        var konfAmb = JSON.parse($('#HfKonfAmb').val());
        grida.setLastSel2(1);
        idRresht = 1;
        grida.jqGrid('clearGridData');
        var kodi, emertimi, magazina, gjendja, gjithsej, vjetor, idkodi, serial, amfill;

        for (var i = 0; i < colArt.length; i++) {
            if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides - 1] == 'True' || isLidhur)
                be = myJQGrid.myValueButtonFshi(true, idRresht, "#rowed5");
            else
                be = myJQGrid.myValueButtonFshi(false, idRresht, "#rowed5");

            HfArt.Set(colArt[i].IdArtikulli, colArt[i]);
            kodi = colArt[i].KodArtikulli;
            idkodi = colArt[i].IdArtikulli;
            if (pershk == 1)
                emertimi = colArt[i].PershkrimArtikulli;
            else
                emertimi = colArt[i].PershkrimiAngArtikulli;
            magazina = colNjesAdminis[i].Kodi;
            serial = colSeriale[i].AqtSerialKod;
            amfill = colAmort[i].AmortizimiFillestar;
            gjendja = colTrupMag[i].VleftaGjendje - colTrupMag[i].VleftaShteseRivleresim;
            if (Utils.getUrlVar('lloj') == "amortizim") {
                gjithsej = colTrupMag[i].AmortizimiGjithsej;
                vjetor = colTrupMag[i].AmortizimiVjetor;
            }
            else {
                if (rivleresimXStandart == 1)
                    gjithsej = colTrupMag[i].VleftaShteseRivleresim;
                else
                    gjithsej = colTrupMag[i].VleftaPlusMinus;
                vjetor = 0;
            }
            var datarow = {
                txtNrRendor: i + 1,
                txtKodi: kodi, txtEmertimi: emertimi, txtMagazina: magazina,
                txtIdKodi: idkodi, txtSerial: serial, txtFshi: be
            };
            var su = grida.jqGrid('addRowData', parseInt(idRresht), datarow);
            grida.setTekstQelize('txtNrRendor', idRresht, grida.getInd(idRresht, false));
        
            grida.setTekstQelize('txtGjendja', idRresht, gjendja);
            grida.setTekstQelize('txtGjithsej', idRresht, gjithsej);
            grida.setTekstQelize('txtVjetor', idRresht, vjetor);
            grida.setTekstQelize('txtAmFillestar', idRresht, amfill);
            idRresht = idRresht + 1;
            grida.setLastSel2(idRresht);
        }
        updateTotalet();
    }
}

var arrKodi = new Array();
var arrEmertimi = new Array();
var arrMagazina = new Array();
var arrSasia = new Array();
var arrCmimi = new Array();
var arrVlefta = new Array();
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
var identikuesPerPopupArtikulli;
var identifikuesPerPopupMagazina;

function changeName() {
    myFaqeCelje.shtoHandlerSession();
    try {
        if (Utils.getUrlVar('id') == undefined)
            window.parent.callWebServiceKtheInfoLart('Shto_RivleresimeAmortizimi.aspx?lloj=' + Utils.getUrlVar('lloj'), 0);
        else
            window.parent.callWebServiceKtheInfoLart('Shto_RegjistrimeAmortizimi.aspx?lloj=' + Utils.getUrlVar('lloj'), Utils.getUrlVar('id'));
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
        veprimi: 'RivleresimeAmortizimi',
        niveli: niveli,
        listUrl: listUrl
    };
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhDokumentin"), 'LupaDokumenta.aspx?' + Utils.KonvertoObjectQueryString(queryString), 950, 560);


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
            data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, kodKontrolli: '', idObjekti: -1, shtim: false, merrFormatKursi: false, merrGjitheKonf: false, idGjuha: idGjuha, idPerdoruesi: idPerdoruesi, idNdermarrje: idNdermarrje })
        }).done(SucceededCallbackKonfig);
        // Ia kalojme false parametrin shtim, sepse ne rastin e magazines duhet marre formati i numrit per monedhen baze pavaresisht klientit dhe nuk duhet te kontrollojme nese ka ndonje vlere default te konfigurimi per klientin.
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
    }
}

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}


var kushtet; var colKushte; var colAlterKusht;
var fokusi = 0;
var colGrida;
var formatNumriZgjedhur;
var analitike = 1;
var rivleresimXStandart = 0;
function SucceededCallbackKonfig(result) {
    var hf = $("input[id$='hfShtimModifikim']");
    var hfLidhur = $("input[id$='hfLidhur']");
    var arrTabela = ['tblFillim', "tblFund"];
    var arrPrind = ["dvFillim", "dvFundi"];
    var colKontrollet = result.colKontroll;  //[0];
    var colAtrTrupi = result.colAtrTrupi;    //[1];
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
    var colKontrollet = result.colKontroll;  //[0];
    var colAtrTrupi = result.colAtrTrupi;  //[1];
    colGrida = result.colGrida;  //[2];
    colKushte = result.colKushte;  //[3];
    colAlterKusht = result.colAlterKusht;   //[4];
    var kodniveli = result.kodniveli;  //[5];
    $('#HfGridCol').val(JSON.stringify(colGrida));
    var hfMag = $("#hfLupaMagazina")[0];
    $('#hfKontrolletNrAutom').val(JSON.stringify(colKontrollet));
    $('#hfAtributeNrAutom').val(JSON.stringify(colAtrTrupi));
    if ($("input[id$='hfShtimModifikim']").val() == "shtim" || $("input[id$='hfShtimModifikim']").val() == "klonim") {
        hfNrAuto.Clear();
        hfNrAutoShitje.Clear();
        vendosNrAutomatik(colAtrTrupi, colKontrollet);
    }
    var hfLlog = $("#hfLlogaria");
    for (var i = 0; i < colKontrollet.length - 1; i++) {
        //id e konfigurimit te lupes vendoset ne hidden field
        //vlera e saj do t'i kalohet si query string hapjes se popUp-it

        if (colKontrollet[i].KodKontrolli == "btneMagazina") {
            if (btneMagazina.GetText() != "" && btneMagazina.GetSelectedItem() != null && $('#hfShtimModifikim').val() != 'modifikim') {
                try {
                    $.ajax({
                        pritPergjigje: true,
                        url: Utils.getServerApiUrl("Rregjistrime", "ktheDegeMagazineSipasKodit"),
                        data: JSON.stringify({ kodi: btneMagazina.GetSelectedItem().GetColumnText("Kodi"), idNdermarrje: hfState.Get('idNdermarrje'), idPerdoruesi: hfState.Get('idPerdoruesi') })
                    }).done(SucceededCallbackDega);
                }
                catch (e) {
                    myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
                }
            }
            hfMag.value = colAtrTrupi[i].IdKonfigAmbjenteLupa.toString(); //merret id e konfigurimit te lupes per lupen e magazines ne forme      
        }

        else if (colKontrollet[i].KodKontrolli == "cmbLlogariKunderParti")
            hfLlog.val(colAtrTrupi[i].IdKonfigAmbjenteLupa.toString());
    }
    rivleresimXStandart = 0;
    pershk = 1;
    kodkodbar = 1;
    analitike = 1;
    var hidField1 = $("#hfKontabilizimi")[0];
    hidField1.value = 0;
    var modinfo = 0;
    for (j = 0; j < colKushte.length; j++) {
        if (colKushte[j].Kodi == 'GJDM') {
            if (colAlterKusht[j].Alternativa == "Po") {
                gjdm = true;
                hfState.Set('GJDM', gjdm);
            }
            else {
                gjdm = false;
                hfState.Set('GJDM', gjdm);
            }
        }
        if (colKushte[j].Kodi == 'AR') {
            hfState.Set('AR', colAlterKusht[j].Alternativa);
            
        }
        if (colKushte[j].Kodi == 'P') {
            if (colAlterKusht[j].Alternativa == 'Pershkrimi 1')
                pershk = 1;
            else
                pershk = 2;
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
        if (colKushte[j].Kodi == 'IA') {
            if (colAlterKusht[j].Alternativa == 'Kod')
                kodkodbar = 1;
            else
                kodkodbar = 2;
        }
        if (colKushte[j].Kodi == 'FAAP') {
            if (colAlterKusht[j].Alternativa == 'Analitike')
                analitike = 1;
            else
                analitike = 2;
        }

        if (colKushte[j].Kodi == 'RASS') {
            if (colAlterKusht[j].Alternativa == 'Po')
                rivleresimXStandart = 1;
            else
                rivleresimXStandart = 0;
        }

        if (colKushte[j].Kodi == 'F') {
            if (colAlterKusht[j].Alternativa == 'Sasia')
                fokusi = 1;
            else if (colAlterKusht[j].Alternativa == 'Rreshti tjeter') fokusi = 2;
            else fokusi = 0;
        }
    }
    hfState.Set('analitike', analitike);

    grida.jqGrid('GridUnload', "rowed5");;
    var isLidhur = (hfLidhur.val().toLowerCase() === 'true');
    formGridColsArray(isLidhur);
    grida.setLastSel2(-1);
    grida = $('#rowed5');
    if ($("input[id$='hfShtimModifikim']").val() == "klonim") {
        cmbKonfigurimi.SetEnabled(false);
        cmbLloji.SetEnabled(false);
    }
       
    
    ruajFormatetNeGride(grida, formatNumriZgjedhur);
    inicializoGride(isLidhur);
    mbushGrideNgaHiddenFieldet(isLidhur);
    Utils.konfiguroAccorditionPasKonfigDokumenti(varKonfig.identifikuesPerLocalStorageKey);
}

function DateChanged(s, e) {
    var hfKontrollet = JSON.parse($('#hfKontrolletNrAutom').val());
    var atributet = JSON.parse($('#hfAtributeNrAutom').val());
    vendosNrAutomatik(atributet, hfKontrollet);
    kontrolloVlereBosh();
    var grida = $('#rowed5');
    var rreshtaTeGrides = grida.jqGrid('getDataIDs');
    for (i = 0; i < rreshtaTeGrides.length; i++) {

        var idkodi = grida.getTekstQelize('txtIdKodi', rreshtaTeGrides[i]);//heq serialet
        var magazina = grida.getTekstQelize('txtMagazina', rreshtaTeGrides[i]);
        var serial = grida.getTekstQelize('txtSerial', rreshtaTeGrides[i]);
        try {
            var idja = idkodi;
            if (!(typeof (idja) == "undefined" || idja == undefined || idja == "" || idja == "0")) {


                $.ajax({
                    pritPergjigje: true,
                    url: Utils.getServerApiUrl("Rregjistrime", "ktheRowGjendjeArtikulliPerMagazine"),
                    data: JSON.stringify({ idja: idkodi, rreshti: rreshtaTeGrides[i], data: dteDtDok.GetDate(), magazina: magazina, idndermarja: hfState.Get('idNdermarrje'), serial: serial, rezerva: (hfState.Get("AR") == "Po" && Utils.getUrlVar('lloj') != "amortizim") })
                }).done(SucceededCallbackGjendjaSipasMag);
            }
        }
        catch (e) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTeDhenave"));
        }
    }
}

var pershk = 1;

/*
Function: ndryshoKonfigurimin
    
Therret funksionin <callWebserviceKonfigurimi> per te vendosur nje konfigurim te ri.
*/
function ndryshoKonfigurimin() {
    var pershkKonfigAmb = cmbKonfigurimi.GetSelectedItem().GetColumnText("PershkrimKonfigAmbjente");
    if (pershkKonfigAmb != undefined) {
        lblKonfigurimi.SetText(pershkKonfigAmb);
        $('#kokeKonfigurimi').text(hfState.Get("MenuKokeDokumenti") +': ' + pershkKonfigAmb);
    }

    callWebserviceKonfigurimi(1007, cmbKonfigurimi.GetText());
}

/*
Function: ButtonClickLlogaria
    
Hap lupen e dokumentave.
*/
function ButtonClickLlogaria() {
    var hfLlog = $("#hfLlogaria");
    var queryStr = hfLlog.val(); identikuesPerPopupLlogari = "Magazina";
    myButtonClickLupa.ButtonClickLlogaria(hfState.Get("popupAdministrimiUniversal"), queryStr, 600, 600);

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
    popupUniversal.SetContentUrl('LupaMagazina.aspx?aqt=true&idKonfigAmbjente=' + queryStr);

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
Function: TextChangedMagazina
    
Vendos ne gride magazinen qe zgjidhet te koka
*/
function TextChangedMagazina() {
    //vendoset magazina e zgjedhur te koka ne rreshtin qe po editohet
    var grida = $("#rowed5");
    var idRresht = grida.getLastSel2();
    magazina1 = btneMagazina.GetSelectedItem().GetColumnText("Kodi");

    var magazinagrides = $('#txtMagazina' + idRresht)[0];
    if (magazinagrides != undefined) {
        for (var i = 0; i < magazinagrides.length; i++) {
            if (magazinagrides[i].text == magazina1)
                magazinagrides.selectedIndex = i;
        } reshtiieditueshem = true;

    }

    var rreshtaTeGrides = grida.jqGrid('getDataIDs');
    for (i = 0; i < rreshtaTeGrides.length; i++) {
        if (!reshtiieditueshem)
            grida.jqGrid('setCell', rreshtaTeGrides[i], 'txtMagazina', magazina1, 'clientArray', '');
        else if (rreshtaTeGrides[i] != idRresht) {
            grida.jqGrid('setCell', rreshtaTeGrides[i], 'txtMagazina', magazina1, 'clientArray', '');
        }
        var idkodi = grida.getTekstQelize('txtIdKodi', rreshtaTeGrides[i]);//heq serialet

        if (hfSeriale.Contains(idkodi + '_' + rreshtaTeGrides[i])) {
            hfSeriale.Remove(idkodi + '_' + rreshtaTeGrides[i]);
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgSerialetEArtikullitUhoqenSepseNukIPerkasinKesajMagazines"));
            grida.setTekstQelize('txtSerial', rreshtaTeGrides[i], '')

        }
        try {
            var serial = grida.getTekstQelize('txtSerial', rreshtaTeGrides[i]);
        
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "ktheRowGjendjeArtikulliPerMagazine"),
                data: JSON.stringify({ idja: idkodi, rreshti: rreshtaTeGrides[i], data: dteDtDok.GetDate(), magazina: btneMagazina.GetSelectedItem().GetColumnText("Kodi"), idndermarja: hfState.Get('idNdermarrje'), serial: serial, rezerva: (hfState.Get("AR") == "Po" && Utils.getUrlVar('lloj') != "amortizim") })
            }).done(SucceededCallbackGjendjaSipasMag);
        }
        catch (e) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTeDhenave"));
        }
    }

    if (magazina1 != "" && $('#hfShtimModifikim').val() != 'modifikim') {
        try {
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "ktheDegeMagazineSipasKodit"),
                data: JSON.stringify({ kodi: magazina1, idNdermarrje: hfState.Get('idNdermarrje'), idPerdoruesi: hfState.Get('idPerdoruesi') })
            }).done(SucceededCallbackDega);
        } catch (e) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
        }
    }
}
function SucceededCallbackDega(rezult) {
    cmbDegeAdministrative.SetValue(rezult);
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
        callWebserviceNiveliNew(cmbLloji.GetValue(), 'rivleresimAm', mod);
    else
        callWebserviceNiveliNew('', 'rivleresimAm', mod);
}

function callWebserviceNiveliNew(lloji, tipi, mod) {
  
        var idNdermarrje = hfState.Get('idNdermarrje');
        var idPerdoruesi = hfState.Get('idPerdoruesi');
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheTemplateNiveli"), data: JSON.stringify({ idNiveli: lloji, veprimi: tipi, mod: mod, idPerdoruesi: idPerdoruesi, idNdermarrje: idNdermarrje, idGjuha: hfState.Get('idGjuha') })
        }).done(SucceededCallbackNiveliNew);
  
}
function SucceededCallbackNiveliNew(colModelet) {
    if ($("#hfShtimModifikim").val() != "modifikim" && $("#hfShtimModifikim").val() != "klonim") {
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
        editorKodi = grida.getTekstQelize('txtKodi', idTe[i]);
        editorEmertimi = grida.getTekstQelize('txtEmertimi', idTe[i]);
        editorMagazina = grida.getTekstQelize('txtMagazina', idTe[i]);
        editorGjendja = grida.getTekstQelize('txtGjendja', idTe[i]);
        editorGjithsej = grida.getTekstQelize('txtGjithsej', idTe[i]);
        editorVjetor = grida.getTekstQelize('txtVjetor', idTe[i]);
        editorAmFill = grida.getTekstQelize('txtAmFillestar', idTe[i]);

        if (editorKodi != "") { trupiBosh = false; }

        if (btneMagazina.GetEnabled() && editorMagazina == "" && editorKodi != '') {
            kamag = false;
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgShenoniMagazinen"));
            e.processOnServer = false;
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
    var idGjuha = hfState.Get('idGjuha');
    var idNdermarrje = hfState.Get('idNdermarrje');
    var idPerdoruesi = hfState.Get('idPerdoruesi');
    var idViti = hfState.Get('idViti');
    myJQGrid.ruajKolonatEGrides(grida, idGride, idGjuha, idNdermarrje, idViti, idPerdoruesi);
}

/*
Function: pastro
    
Pastrin array-t e perdorura dhe vendos counterat ne 0.
*/
function pastro() {
    arrKodi = new Array();
    arrEmertimi = new Array();
    arrMagazina = new Array();
    arrNjesia = new Array();
    arrSasia = new Array();
    arrCmimi = new Array();
    arrVlefta = new Array();
    resetCountera();
    var hidField2 = document.getElementById("hfKodi");
    var hidField3 = document.getElementById("hfEmertimi");
    var hidField5 = document.getElementById("hfMagazina");
    var hidField6 = document.getElementById("hfNjesia");
    var hidField7 = document.getElementById("hfSasia");
    var hidField8 = document.getElementById("hfCmimi");
    var hidField9 = document.getElementById("hfVlefta");

    hidField2.value = "";
    hidField3.value = "";
    hidField5.value = "";
    hidField6.value = "";
    hidField7.value = "";
    hidField8.value = "";
    hidField9.value = "";
    HfArt.Clear();
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
Function: ButtonClickKodi
    
Hap lupen e artikujve apo makrove sipas zgjedhjes qe eshte bere te kategoria.
*/
function ButtonClickKodi() {
    var grida = $('#rowed5');
    var hfKod = document.getElementById("hfGridaKodi");
    var queryStr = hfKod.value;
    popupUniversal.SetHeaderText(hfState.Get("headerPopUpZgjidhArtikullin"));
    var grup = '';
    popupUniversal.SetContentUrl('LupaArtikull.aspx?idKonfigAmbjente=' + queryStr + '&aqt=aqt&grup=' + grup);
    popupUniversal.SetSize(widthLupaArtikull, heightLupaArtikull);
    popupUniversal.Show();
}


/*
Function: vendosVleftat
    
Merr dhe validon vlerat e sasise dhe cmimit te caktuara ne gride dhe therret funksionin <updateTotalet>
*/
function vendosVleftat(s) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var gjendja = grida.getTekstQelize('txtGjendja', idRresht);
    var gjithsej = grida.getTekstQelize('txtGjithsej', idRresht);
    var vjetor = grida.getTekstQelize('txtVjetor', idRresht);
    var amfill = grida.getTekstQelize('txtAmFillestar', idRresht);
    var editorKodi = grida.getTekstQelize('txtKodi', idRresht);

    if (gjendja == '.') {
        grida.setTekstQelize('txtGjendja', idRresht, '0.');
        return;
    }
    if (gjithsej == '.') {
        grida.setTekstQelize('txtGjithsej', idRresht, '0.');
        updateTotalet();
        return;
    }
    if (vjetor == '.') {
        grida.setTekstQelize('txtVjetor', idRresht, '0.');
        return;
    } if (amfill == '.') {
        grida.setTekstQelize('txtAmFillestar', idRresht, '0.');
        return;
    }

   
}

function kontrolloVlereBosh() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var gjendja = grida.getTekstQelize('txtGjendja', idRresht);
    var gjithsej = grida.getTekstQelize('txtGjithsej', idRresht);
    var vjetor = grida.getTekstQelize('txtVjetor', idRresht);
    var amfill = grida.getTekstQelize('txtAmFillestar', idRresht);
    if (gjithsej === '' || isNaN(parseFloat(gjithsej))) {
        if (Utils.getUrlVar('lloj') == 'amortizim')
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgAmortizimiGjithsejDuhetNumer"));
        else
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdryshimiIVleresDuhetNumer"));
        gjithsej = grida.getVlereDefault('txtGjithsej');
        grida.setTekstQelize('txtGjithsej', idRresht, gjithsej);
    }
    if (vjetor === '' || isNaN(parseFloat(vjetor))) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgAmortizimiVjetorDuhetNumer"));
        vjetor = grida.getVlereDefault('txtVjetor');
        grida.setTekstQelize('txtVjetor', idRresht, vjetor);
    } 
    if (gjendja === '' || isNaN(parseFloat(gjendja))) {
        myMesazh.ShtoMesazhGabimi("Gjendja duhet te jete numer!");
        gjendja = grida.getVlereDefault('txtGjendja');
        grida.setTekstQelize('txtGjendja', idRresht, gjendja);
    }
    if (amfill === '' || isNaN(parseFloat(amfill))) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgAmortizimiFillestarDuhetNumer"));
        amfill = grida.getVlereDefault('txtAmFillestar');
        grida.setTekstQelize('txtAmFillestar', idRresht, amfill);
    }
    if (vjetor > gjendja) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgRivleresimAmortVjetorNukDuhetMeIMadhSeGjendja"));
        vjetor = gjendja;
        grida.setTekstQelize('txtVjetor', idRresht, vjetor);
    }
    if (gjithsej > gjendja && (Utils.getUrlVar('lloj') == 'amortizim')) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgRivleresimAmortGjithsejNukDuhetMeIMadhSeGjendja"));
        gjithsej = gjendja;
        grida.setTekstQelize('txtGjithsej', idRresht, gjithsej);
    }
    if (vjetor > gjithsej) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgRivleresimAmortVjetorNukDuhetMeIMadhSeAmortGjithsej"));
        vjetor = gjithsej;
        grida.setTekstQelize('txtVjetor', idRresht, vjetor);
    }
    if (dteDtDok.GetDate().getDate() == 1 && dteDtDok.GetDate().getMonth() == 0 && (Utils.getUrlVar('lloj') == 'amortizim'))
        if (vjetor != gjithsej) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgRivleresimAmortVjetorDuhetIBarabarteMeAmortGjithsej"));
            vjetor = gjithsej;
            grida.setTekstQelize('txtVjetor', idRresht, vjetor);
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
    hfSeriale.Clear();
    btneMagazina.SetSelectedIndex(-1);;
    if (btneMagazina.GetItemCount() == 1)
        btneMagazina.SetSelectedIndex(0);
    cmbLlogariKunderParti.SetValue(null);
    txtNrDok.SetText('');
    cmbDegeAdministrative.SetSelectedIndex(-1);
    txtShenime.SetText('');
    txtTotali.SetText('0.00');
    cmbStandarti.SetSelectedIndex(0);
    var hf = document.getElementById("status1");
    hf.value = "false";
    vendosDateDefault();

}

var dtDokumentit;


function vendosDateDefault() {
    if ($("input[id$='hfShtimModifikim']").val() == "shtim" || $("input[id$='hfShtimModifikim']").val() == "klonim") {
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
    var grida = $('#rowed5');
   if( !Utils.nrWsRrugesManager.kanePerfunduarWs()) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgPoTransferohetTeDhenatShtypniPerseriRuaj"));
        return false;
    }
    if (cmbLloji.GetText() == "") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniLlojin"));
        return false;
    }
    else if (txtNrDok.GetText() == "") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgShenoniNumrinEDokumentit"));
        return false;
    } else if (cmbLlogariKunderParti.GetVisible() && cmbLlogariKunderParti.GetText() == "") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgShenoniLlogarine"));
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
    if ($('#hfqkmesazhi').val() == 'shfaqmesazh') {
        //popMesazhQK.Show();
        myMesazh.ShtoMesazh({ type: "confirm", layout: "center", modal: true, text: hfState.Get("msgDeshironiShperndarjeQendraKosto"), okClick: function () { Utils.hapPopUp(hfState.Get("msgShperndarjeNeQendratEKostos"), $('#hfUrl')); }, cancelClick: function () { Utils.JopopupClick($('#hfUrl')); } });
    }
    if ($('#hfqkmesazhi').val() == 'shfaqlupe') {
        Utils.hapPopUp(hfState.Get("msgShperndarjeNeQendratEKostos"), $('#hfUrl'));
    }
    if (hf.value == "true") {

        myFaqeCelje.kontrolloTeDrejta('Shto_RivleresimeAmortizimi.aspx?lloj=' + Utils.getUrlVar('lloj') + '&shtim_modifikim=shtim', true);

    } else click = false;
}

var shtoTimer;
function shtoTimedClick(e) {
    if (!Utils.nrWsRrugesManager.kanePerfunduarWs())
        shtoTimer = setTimeout(function () { shtoTimedClick(e) }, 500);
    else {
        clearTimeout(shtoTimer);
        myFaqeCelje.kontrolloTeDrejta('Shto_RivleresimeAmortizimi.aspx?lloj=' + Utils.getUrlVar('lloj') + '&shtim_modifikim=shtim', true);
        e.processOnServer = false;
    }
}
/*
Function: menuClick

Percakton veprimin qe kryhet nese klikohet nje nga butonat e menuse.
*/
function menu_click(s, e) {
    $('#hfRuajDraft').val(e.item.name);
    if (e.item.name == 'Ruaj' || e.item.name == 'Draft') {
        myMesazh.vendosServer();
        if ($("input[id$='hfAutorizimi']").val() == 'False') {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukKeniAutorizimPerTeRuajturKeteDok"));
            Utils.hiqLoadingGif();; click = false;
            e.processOnServer = false;
            return;
        }
        var valid = myFaqeCelje.validim(s, e);
        if (!valid) {
            Utils.hiqLoadingGif();;
            e.processOnServer = false; click = false;
            return;
        }
        if (isValidKoka()) {
            Utils.shfaqLoadingGif();;
            RuajClick(s, e);
        }
        else {
            e.processOnServer = false; click = false;
            // myMesazh.ShtoMesazhGabimi('Plotesoni te gjitha fushat');
        }
    }
    if (e.item.name == 'QendraKosto') {

        e.processOnServer = false; click = false;
        return;
    }

    else if (e.item.name == 'Kerko') {
        myFaqeCelje.kontrolloTeDrejta('RivleresimeAmortizimi.aspx?lloj=' + Utils.getUrlVar('lloj'), null, true);
        e.processOnServer = false;
    }
    else if (e.item.name == 'Shto') {
        shtoTimer = setTimeout(function () { shtoTimedClick(e) }, 500);
    }
    if (e.item.name === 'Klono') {
        if ($("input[id$='hfAutorizimi']").val() == 'False') {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukKeniAutorizimKlonim"));
            Utils.hiqLoadingGif();;
            e.processOnServer = false;
            click = false;
            return;
        }
        KlonoClick(e);
    }
    else if (e.item.name == 'Fshi') {
        if ($("input[id$='hfAutorizimi']").val() == 'False') {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukKeniAutorizimPerTeFshireDok"));
            Utils.hiqLoadingGif();; click = false;
            e.processOnServer = false;
            return;
        } popFshi.Show(); e.processOnServer = false;
    }
    else if (e.item.name == 'Anullo') {
        myFaqeCelje.kontrolloTeDrejta('RivleresimeAmortizimi.aspx?lloj=' + Utils.getUrlVar('lloj'));
        e.processOnServer = false;
        click = false;
    }
}

function KlonoClick(e) {
    var hf1 = $("#hfShtimModifikim");
    $("input[id$='hfLidhur']").val(false);
    hf1.val("klonim");

    try {
        ASPxMenu1.GetItemByName('FletaKontabel').SetVisible(false);
        ASPxMenu1.GetItemByName('QendraKosto').SetVisible(false);
        ASPxMenu1.GetItemByName('Klono').SetVisible(false);
        ASPxMenu1.GetItemByName('Draft').SetVisible(true);

    }
    catch (ex) {
    }
    ASPxMenu1.GetItemByName('Fshi').SetVisible(false);

    $("#ASPxSplitter1_hl").empty();
    $('#ASPxSplitter1_tblKonfigurimi>tbody>tr:eq(0)>td:eq(0)').empty();
    e.processOnServer = false;
    ndryshoKonfigurimin();
    click = false;
}
function Kthehu(mesazh) {
    myFaqeCelje.kontrolloTeDrejta('RivleresimeAmortizimi.aspx?lloj=' + Utils.getUrlVar('lloj') + "&ruaj=po");

}

var click = false;
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
    $("input[id$='hfLidhur']").val(false); $("input[id$='hfAutorizimi']").val(true);
    pastro();
    pastroFushatKokes();
    var hf = $("#hfShtimModifikim");
    ASPxMenu1.GetItemByName('Shto').SetVisible(true);
    ASPxMenu1.GetItemByName('FletaKontabel').SetVisible(false);
    ASPxMenu1.GetItemByName('Fshi').SetVisible(false);
    ASPxMenu1.GetItemByName('QendraKosto').SetVisible(false);
    ASPxMenu1.GetItemByName('Draft').SetVisible(true);
    hf.val("shtim"); myMenu.menuSipasTeDrejtaRegjistrim(hf, hfTeDrejta);
    TextChangedLloji();

    $("#ASPxSplitter1_hl").empty(); click = false;
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

function ButtonOkQK_Click(s, e) {
    popMesazhQK.Hide();
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgShperndarjeNeQendratEKostos"), $('#hfUrl').val(), 900, 600);
}

function txtGjithsejOnFocusOut() {
    kontrolloVlereBosh();
    updateTotalet();
}

function updateTotalet() {
    if (Utils.getUrlVar('lloj') != 'amortizim')
        return;
    var grida = $('#rowed5');
    var totali = calcTotale(grida);
    txtTotali.SetText(totali.totalGjithsej);
}

function calcTotale(grida) {
    var idTe = grida.jqGrid('getDataIDs');
    var  totalGjithsej = 0.00;
    for (var i = 0; i < idTe.length; i++) {
        if (grida.getTekstQelize('txtKodi', idTe[i]) != "") {
            var vleraRreshtit = grida.getTekstQelize('txtGjithsej', idTe[i]);
            if (vleraRreshtit === "")
                vleraRreshtit = grida.getVlereDefault('txtGjithsej');
            
            totalGjithsej += parseFloat(vleraRreshtit);
        }
    }
    return { totalGjithsej: totalGjithsej };
}