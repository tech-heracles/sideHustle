;
var kodkodbar = 1;
var lidhur = false;
var arrayMeMagazina = new Array();
var colMagazina;
var magazina1;

var njesiaZgjedhur;

var arrayIdKolonaGrides = new Array();
var arrayPershkrimiKolonaGrides = new Array();
var arrayVisibleKolonaGrides = new Array();
var arrayWidthKolonaGrides = new Array();
var arrayReadOnlyKolonaGrides = new Array();
var arrayRenditjeKolonaGrides = new Array();
var arrMeNjesi = new Array();
var widthLupaArtikull = 850;
var heightLupaArtikull = 600;

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
    identifikuesPerLocalStorageKey: 'NdryshimCmimSasi'
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
        identifikuesPerPopupDokumentat = "Shto_RegjistrimNdryshimCmimSasi.aspx";
        identikuesPerPopupArtikulli = "RegjistrimNdryshimCmimSasi";

        identifikuesPerPopupMagazina = "RegjistrimNdryshimCmimSasi";
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
    var arrayPershkrime = [arrayPershkrimiKolonaGrides[0], arrayPershkrimiKolonaGrides[1], arrayPershkrimiKolonaGrides[2], arrayPershkrimiKolonaGrides[3], arrayPershkrimiKolonaGrides[4], arrayPershkrimiKolonaGrides[5], arrayPershkrimiKolonaGrides[6], arrayPershkrimiKolonaGrides[7], arrayPershkrimiKolonaGrides[8], arrayPershkrimiKolonaGrides[9], arrayPershkrimiKolonaGrides[10], arrayPershkrimiKolonaGrides[11], arrayPershkrimiKolonaGrides[12], arrayPershkrimiKolonaGrides[13]];
    var arrayModel = [
        { name: arrayIdKolonaGrides[0], index: arrayIdKolonaGrides[0], width: arrayWidthKolonaGrides[0], hidden: arrayVisibleKolonaGrides[0], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemNrRendor, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[1], index: arrayIdKolonaGrides[1], width: arrayWidthKolonaGrides[1], hidden: arrayVisibleKolonaGrides[1], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myElemKodi, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[2], index: arrayIdKolonaGrides[2], width: arrayWidthKolonaGrides[2], hidden: arrayVisibleKolonaGrides[2], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myElemKodbari, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[3], index: arrayIdKolonaGrides[3], width: arrayWidthKolonaGrides[3], hidden: arrayVisibleKolonaGrides[3], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myElemEmertimi, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[4], index: arrayIdKolonaGrides[4], width: arrayWidthKolonaGrides[4], hidden: arrayVisibleKolonaGrides[4], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemComboMagazina, custom_value: myJQGrid.myValueCombo } },
        { name: arrayIdKolonaGrides[5], index: arrayIdKolonaGrides[5], width: arrayWidthKolonaGrides[5], hidden: arrayVisibleKolonaGrides[5], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemComboNjesiaSup, custom_value: myJQGrid.myValueCombo } },
        { name: arrayIdKolonaGrides[6], index: arrayIdKolonaGrides[6], width: arrayWidthKolonaGrides[6], hidden: arrayVisibleKolonaGrides[6], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemSasia, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[7], index: arrayIdKolonaGrides[7], width: arrayWidthKolonaGrides[7], hidden: arrayVisibleKolonaGrides[7], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemCmimi, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[8], index: arrayIdKolonaGrides[8], width: arrayWidthKolonaGrides[8], hidden: arrayVisibleKolonaGrides[8], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemVlefta, custom_value: myJQGrid.myValueTextBox } },
       { name: arrayIdKolonaGrides[9], index: arrayIdKolonaGrides[9], width: arrayWidthKolonaGrides[9], hidden: arrayVisibleKolonaGrides[9], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemSasiaRe, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[10], index: arrayIdKolonaGrides[10], width: arrayWidthKolonaGrides[10], hidden: arrayVisibleKolonaGrides[10], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemCmimiRi, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[11], index: arrayIdKolonaGrides[11], width: arrayWidthKolonaGrides[11], hidden: arrayVisibleKolonaGrides[11], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemVleftaRe, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[12], index: arrayIdKolonaGrides[12], width: arrayWidthKolonaGrides[12], hidden: arrayVisibleKolonaGrides[12], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myelemIdKodi, custom_value: myJQGrid.myValueTextBox }, hidedlg: true },
        { name: arrayIdKolonaGrides[13], index: arrayIdKolonaGrides[13], width: arrayWidthKolonaGrides[13], hidden: arrayVisibleKolonaGrides[13], classes: classes, sorttype: "int", editable: true, edittype: 'custom', editoptions: { custom_element: myElemButtonFshi, custom_value: myValueButtonFshi }, hidedlg: true },
    ];



    var selektoriGrides = "#rowed5";

    var gridParams = {
        emergride: selektoriGrides,
        arrayPershkrime: arrayPershkrime,
        arrayModel: arrayModel,
        lidhur: isLidhur,
        emerEditorKodi: "txtKodi",
        emerEditorCmimi: "txtCmimi",
        cmimzero: cmimzero,
        emerMag1: 'txtMagazina',
        widthi: $('#divgride2').width() - 5,
        fokus: fokusi,
        mosshtorresht: !lejomod ? true : false,
        lostFocusKoloneFundit: lostFocusKoloneFundit,
        resetRreshtKorent: resetRreshtKorent,
        autocompleteList: 
         [{ emerEditor: "txtKodi", selectFunc: selectFunc, changeFunc: changeFunc, shtoDataKod: false },
         { emerEditor: "txtCmimi", selectFunc: selectFunc6, changeFunc: changeFunc6, shtoDataKod: false }],

        konfigToolbar: {
            konfigGrid: $('#hfTeDrejtaKonfGride').val(),
            shtoArtikull: $('#hfTeDrejtaArtRi').val(),
            ruajKolonatEGrides: ruajKolonatEGrides,
            hapPopUpRi: hapPopUpRi,
            hapPopUpModifikoArt: hapPopUpModifikoArt
        }
    };
   return  myJQGrid.initGride(gridParams);
    //myJQGrid.inicializoGride("#rowed5", arrayPershkrime, arrayModel, isLidhur, lastsel2,
    //    "#txtKodi", "", "#txtCmimi", cmimzero, 'txtMagazina', '', $('#divgride2').width() - 5, undefined,
    //    fokusi, null, null, '', '', undefined, undefined, $('#hfTeDrejtaKonfGride').val(), $('#hfTeDrejtaArtRi').val(),
    //    undefined, undefined, !lejomod ? true : false);
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
    grida.setVlereDefault('txtCmimiRi', 0);
    grida.setVlereDefault('txtVleftaRe', 0);
    grida.setVlereDefault('txtSasiaRe', 1);
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
    grida.setShifraPasPresjes('txtCmimiRi', formatNumri.ShifraPasPresjesCmimi);
    grida.setShifraPasPresjes('txtVleftaRe', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtSasiaRe', formatNumri.ShifraPasPresjesSasia);
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
        grida.formatoQelize('txtSasiaRe', idRreshti);
        grida.formatoQelize('txtCmimiRi', idRreshti);
        grida.formatoQelize('txtVleftaRe', idRreshti);
    }
    formatoFushaDevi();
}

function formatoFushaDevi() {
    Utils.formatoTextBox(txtVlefta);
}

function unformatoFushaDevi() {
    Utils.unFormatoTextBox(txtVlefta);
}

function changeFunc6(event, ui, emerKodi, index) {//po

}
function selectFunc6(event, ui, emerfushe) { //po per cmimet

}
/*
Function: myElemNrRendor

Nderton nje textbox per te vendosur nr rendor te rreshtit.
*/
function myElemNrRendor(value, options) {//po
    var grida = $("#rowed5");
    var idRresht = grida.getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[0];
    return myJQGrid.myElemNrRendor(value, options, idRresht, 'txtNrRendor', grida);
}

function selectFunc(event, ui, emerfushe, idArt, kodArt) {
    var detajim = -1;
    var lloj = Utils.getUrlVar('lloj');
    var idNdermarrje = hfState.Get("idNdermarrje");
    var idPerdoruesi = hfState.Get('idPerdoruesi');
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var magazine = grida.getTekstQelize('txtMagazina', idRresht);
    if (idArt != undefined && kodArt != undefined && idArt != "" && kodArt != "") {
        $(emerfushe).val(kodArt);
        if (!kontrolloRow(idRresht)) {
            $.ajax({
                url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraArtMeID"),
                data: JSON.stringify({
                    idja: idArt, rreshti: idRresht, data: dteDtDok.GetDate(), iddetajim: detajim, magazina: magazine, meDetajim: false, magazinaDest: "", idNdermarrje: idNdermarrje,
                    idPerdoruesi: idPerdoruesi, njesiDef: 1, sasiaNeRresht: 1, merrCmim: false
                })
            }).done(SucceededCallbackArtNew);
        }
        return false;
    }
    if (ui.item != null) {
        $(emerfushe).val(ui.item.label);
    }

    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraArtMeID"),
        data: JSON.stringify({
            idja: ui.item.value, rreshti: idRresht, data: dteDtDok.GetDate(), iddetajim: detajim, magazina: magazine, meDetajim: false, magazinaDest: "", idNdermarrje: idNdermarrje,
            idPerdoruesi: idPerdoruesi, njesiDef: 1, sasiaNeRresht: 1, merrCmim: false
        })
    }).done(SucceededCallbackArtNew);
    return false;
}

function changeFunc(event, ui, emerKodi, index) {
    var idNdermarrje = hfState.Get("idNdermarrje");
    var idPerdoruesi = hfState.Get('idPerdoruesi');
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var emerfushe = '#' + emerKodi + idRresht;
    var detajim = -1;
    //var index = -1;
    //var idKod = 'txtKodi';
    //if (emerKodi === undefined || emerKodi === null)
    //    index = myJQGrid.getIndexFromEvent(event, idKod);
    //else
    //    index = emerKodi.split(idKod)[1];
    if (index == idRresht) {
        var magazine = grida.getTekstQelize('txtMagazina', idRresht);
        if (ui == null || ui.item == null) {
            var kodi = $(emerfushe).val();
            if (kodi != undefined && typeof (kodi) !== "undefined" && kodi != "") {
                $.ajax({
                    pritPergjigje: true,
                    url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraArtMeKodOseKodBar"),
                    data: JSON.stringify({ kodi: kodi, index: idRresht, date: dteDtDok.GetDate(), iddetajim: detajim, magazina: magazine, idMagazina: 0, meDetajim: false, magazinaDest: "", idNdermarrje: idNdermarrje, idPerdorues: idPerdoruesi, merrSipasDetajimit: false, njesiDef: 1, sasiaNeRresht: 1, merrCmim: false })
                }).done(SucceededCallbackArtNew);
                return;
            }
            vendosArt([index, null]);
            return;
        }
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraArtMeID"),
            data: JSON.stringify({ idja: ui.item.value, rreshti: idRresht, data: dteDtDok.GetDate(), iddetajim: detajim, magazina: magazine, meDetajim: false, magazinaDest: "", idNdermarrje: idNdermarrje, idPerdoruesi: idPerdoruesi, njesiDef: 1, sasiaNeRresht: 1, merrCmim: false})
        }).done(SucceededCallbackArtNew);
        return;
    }
    var rreshti = grida.jqGrid('getRowData', index);
    if (ui == null || ui.item == null) {
        if (rreshti.txtKodi != "") {
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraArtMeKodOseKodBar"),
                data: JSON.stringify({ kodi: rreshti.txtKodi, index: index, date: dteDtDok.GetDate(), iddetajim: detajim, magazina: rreshti.txtMagazina, idMagazina: 0, meDetajim: false, magazinaDest: "", idNdermarrje: idNdermarrje, idPerdorues: idPerdoruesi, merrSipasDetajimit: false, njesiDef: 1, sasiaNeRresht: 1, merrCmim: false  })
            }).done(SucceededCallbackArtNew);
            return;
        }
        vendosArt([index, null]);
    }
}

function resetRreshtKorent(idRreshti) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var idkodi = grida.getTekstQelize('txtIdKodi', idRreshti);//heq serialet

    if (idRreshti == idRresht && grida.getTekstQelize('txtKodi', idRresht) != undefined) {
        grida.setTekstQelize('txtCmimi', idRreshti); grida.setTekstQelize('txtCmimiRi', idRreshti);
        grida.setTekstQelize('txtKodi', idRreshti, '');
        grida.setTekstQelize('txtIdKodi', idRreshti, '');

        grida.setTekstQelize('txtEmertimi', idRreshti, '');
        grida.setTekstQelize('txtKodbari', idRreshti, '');
        grida.setTekstQelize('txtSasia', idRreshti);
        grida.setTekstQelize('txtVlefta', idRreshti); grida.setTekstQelize('txtSasiaRe', idRreshti);
        grida.setTekstQelize('txtVleftaRe', idRreshti);
        var objTmp = new Object();
        objTmp.value = "";
        objTmp.text = "";
        var arrayOptions = new Array(1);
        arrayOptions[0] = objTmp;
        $('#cmbNjesia' + idRreshti).replaceWith(myJQGrid.myElemCombo("", 'cmbNjesia', idRreshti, minmax, arrayOptions, true).children()[0]);

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
    var idRresht = grida.getLastSel2();
    if (result !== null) {
        var idRreshti = result.idRreshti;
        var artikulli = result.artikulli;
        var gjendjetot = result.gjendjetot;
        var gjendja = result.gjendja;
        var vleftat = result.vleftat;
        var kodbarsel = result.kodbarsel;
        var kodi = "#txtKodi" + idRreshti;
        var idKodi = "#txtIdKodi" + idRreshti;
        var kodbari = '#txtKodbari' + idRreshti;
        var emerArt = '#txtEmertimi' + idRreshti;
        var detajimi = '';
        if (artikulli == null || artikulli == undefined || artikulli.IdArtikulli == -1) {
            resetRreshtKorent(idRreshti);
            return;
        }
        if (myJQGrid.checkIfIsTheSame(grida, artikulli, null, idRreshti, 'Artikull'))
            return;//eshte i njejti artikull
        if (artikulli.LlojiArt == 1) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgArtikulliAfatgjate"));
            resetRreshtKorent(idRreshti);
            return;
        }
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
        if ((artikulli.Klasa === 5 || artikulli.Klasa === 6) && Utils.getUrlVar("lloj") == 'hyrje' && (lloji != 'VKM')) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("NukKryhenVeprimeMeArtikujProdhimOseProdhimNeProces"));
            resetRreshtKorent(idRreshti);
            return;
        }
        if (artikulli.Klasa === 4) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukMundTeKryeniVeprimeMeArtikujTePerbere"));
            resetRreshtKorent(idRreshti);
            return;
        }

        if (idRreshti == idRresht && grida.getTekstQelize('txtKodi', idRresht) != undefined) {
            grida.setTekstQelize('txtKodi', idRreshti, artikulli.KodArtikulli);
            if (kontrolloRow(idRreshti)) return;
            HfArt.Set(artikulli.IdArtikulli, artikulli);

            myJQGrid.closeAutocomplete('txtKodi', idRreshti);
            grida.setTekstQelize('txtIdKodi', idRreshti, artikulli.IdArtikulli);
            var kodbar = "";
            if (artikulli.OColKodbare.length > 0) kodbar = artikulli.OColKodbare[0].Pershkrimi;
            grida.setTekstQelize('txtKodbari', idRreshti, (kodbarsel != undefined && kodbarsel != null && kodbarsel !== "" ? kodbarsel : kodbar))
            var comboMag = $('#txtMagazina' + idRreshti);
            var merrgjendje = false;
            //  myelemComboMagazina(artikulli.IdMagazina, null, idRreshti);
            var mag = btneMagazina.GetText();
            if (((artikulli.IdMagazina != 0) && (mag == ''))) //magazina 
            {
                mag = artikulli.Magazina; //ishte IdMagazina
                merrgjendje = true;

            }
            comboMag.replaceWith(myelemComboMagazina(mag, null, idRreshti).children()[0]);
            if (merrgjendje) {
                changeMagMerrGjendje(idRreshti);
            }
            if (pershk == 1) {
                grida.setTekstQelize('txtEmertimi', idRreshti, artikulli.PershkrimArtikulli)
                $(emerArt).attr('title', artikulli.PershkrimArtikulli);
            }
            else
                if (artikulli.PershkrimiAngArtikulli !== "")
                    grida.setTekstQelize('txtEmertimi', idRreshti, artikulli.PershkrimiAngArtikulli)
                else
                    grida.setTekstQelize('txtEmertimi', idRreshti, artikulli.PershkrimArtikulli)
            grida.setTekstQelize('txtSasia', idRreshti, gjendja);
            grida.setTekstQelize('txtVlefta', idRreshti, vleftat);
            grida.setTekstQelize('txtCmimi', idRreshti, gjendja != 0 ? vleftat / gjendja : 0);
            grida.setTekstQelize('txtSasiaRe', idRreshti, gjendja);
            grida.setTekstQelize('txtVleftaRe', idRreshti, vleftat);
            grida.setTekstQelize('txtCmimiRi', idRreshti, gjendja != 0 ? vleftat / gjendja : 0);
            var comboNjesia = $('#' + arrayIdKolonaGrides[5] + idRreshti);
            if (njesiDef == 1)
                comboNjesia.replaceWith(myelemComboNjesiaSup(artikulli.KodNjesia1, null, idRreshti).children()[0]);
            else
                comboNjesia.replaceWith(myelemComboNjesiaSup(artikulli.KodNjesia2, null, idRreshti).children()[0]);

            callWebServiceInfoRow();

            updateTotalet();
            return;
        }

        if (grida.getTekstQelize('txtIdKodi', idRreshti) == artikulli.IdArtikulli) {
            grida.setTekstQelize('txtKodi', idRreshti, artikulli.KodArtikulli);
            return; //eshte i njejti artikull 
        }
        grida.setTekstQelize('txtKodi', idRreshti, artikulli.KodArtikulli);

        if (kontrolloRow(idRreshti)) return;
        HfArt.Set(artikulli.IdArtikulli, artikulli);

        grida.setTekstQelize('txtKodi', idRreshti, artikulli.KodArtikulli);
        var kodbar = "";
        if (artikulli.OColKodbare.length > 0) kodbar = artikulli.OColKodbare[0].Pershkrimi;
        grida.setTekstQelize('txtKodbari', idRreshti, kodbarsel == "" ? kodbar : kodbarsel);
        grida.setTekstQelize('txtIdKodi', idRreshti, artikulli.IdArtikulli);
        var mag = btneMagazina.GetText();
        var merrgjendje = false;
        if (artikulli.IdMagazina != 0 && (mag == '')) //magazina
        {
            mag = artikulli.Magazina;
            merrgjendje = true;

        }
        var magazinadefiperketgrupit = false;
        var magazinadefiperketgrupit2 = false;
        var magazinaepare = "";
        if (colMagazina !== "") {
            for (var i = 0; i < colMagazina.length; i++) {
                if ((!artikulli.LlojiArt && (colMagazina[i].IdLlojMagazine == 1 || colMagazina[i].IdLlojMagazine == 3)) || (artikulli.LlojiArt && (colMagazina[i].IdLlojMagazine == 2 || colMagazina[i].IdLlojMagazine == 3))) {
                    if (magazinaepare == "")
                        magazinaepare = colMagazina[i].Kodi;
                    if (colMagazina[i].Kodi == mag) {
                        magazinadefiperketgrupit = true;
                    }

                }
            }
        }
        if (magazinadefiperketgrupit && mag != "") {
            grida.setTekstQelize('txtMagazina', idRreshti, mag);
            if (merrgjendje) changeMagMerrGjendje(idRreshti);
        }
        else grida.setTekstQelize('txtMagazina', idRreshti, magazinaepare);


        grida.setTekstQelize('txtSasia', idRreshti, gjendja);
        grida.setTekstQelize('txtVlefta', idRreshti, vleftat);
        grida.setTekstQelize('txtCmimi', idRreshti, gjendja != 0 ? vleftat / gjendja : 0);
        grida.setTekstQelize('txtSasiaRe', idRreshti, gjendja);
        grida.setTekstQelize('txtVleftaRe', idRreshti, vleftat);
        grida.setTekstQelize('txtCmimiRi', idRreshti, gjendja != 0 ? vleftat / gjendja : 0);
        if (pershk == 1)
            grida.setTekstQelize('txtEmertimi', idRreshti, artikulli.PershkrimArtikulli);
        else
            if (artikulli.PershkrimiAngArtikulli !== "")
                grida.setTekstQelize('txtEmertimi', idRreshti, artikulli.PershkrimiAngArtikulli);
            else grida.setTekstQelize('txtEmertimi', idRreshti, artikulli.PershkrimArtikulli);
        if (njesiDef == 1)
            grida.setTekstQelize('txtNjesia', idRreshti, artikulli.KodNjesia1);
        else
            grida.setTekstQelize('txtNjesia', idRreshti, artikulli.KodNjesia2);

        updateTotalet();
        return;
    }
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
        var mag = grida.getTekstQelize('txtMagazina', idRow);
        var idPerdoruesi = hfState.Get('idPerdoruesi');
        var idViti = hfState.Get('idViti');
        pastroInfoArt();
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "mbushInfoArtikulliMeDetajime"),
            data: JSON.stringify({ idkodi: idKodi, data: dteDtDok.GetDate(), detajim: "", index: idRow, idInfo: idInfoArt, detajim2: "", idViti: idViti, idPerdoruesi: idPerdoruesi, idklient: 0, mag: mag, njesiart: "", idKarta: 0 })
        }).done(SucceededCallbackInfoArt);
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
    var idRow = $('#rowed5').getLastSel2();
    if (result.idRreshti == idRow && result.colInfoTrupi.length != 0) {
        navbar.GetGroupByName('Artikulli').SetExpanded(true);
        vendosInfo(lbxZgjedhur, result);
    }
}

function eshteInfoHapur() {
    return !splitter.GetPane(1).IsCollapsed();
}

function spliterPaneExpanding(s, e) {
    hapMbyllInfo(true);
    var idRow = $('#rowed5').getLastSel2();
    callWebServiceInfoRow(idRow);

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
function dateGotFocus(s, e) {
    window.setTimeout(function () { s.SetCaretPosition(0); }, 0);
}
function hapMbyllInfo(infoExpanded) {
    if (infoExpanded && (infoArt)) {
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

function formGridColsArray(isLidhur) {
    var hfGridKod = $('#hfGridaKodi');
    var hfGridDetajim = $('#hfGridaDetajimi');
    var IdKonfigAmbjenteLupat = [{ hfVar: hfGridKod, kodiText: "txtKodi" }];
    myJQGrid.formArrayKolGrides($("input[id$='HfGridCol']"), arrayIdKolonaGrides, arrayPershkrimiKolonaGrides, arrayVisibleKolonaGrides, arrayReadOnlyKolonaGrides, isLidhur, arrayWidthKolonaGrides, IdKonfigAmbjenteLupat, arrayRenditjeKolonaGrides);
}

/*
Function: myElemCmimi

Nderton nje textbox per te vendosur cmimin.
*/
function myElemCmimi(value, options) {
    var disable = 'False';
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if (arrayReadOnlyKolonaGrides[7] == 'True' || !lejomod)
        disable = 'True';
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, disable, idRresht, 'txtCmimi', vendosVleftat, hfFormatNumri, kontrolloVlereBosh);
}
/*
Function: myElemCmimi

Nderton nje textbox per te vendosur cmimin.
*/
function myElemCmimiRi(value, options) {
    var disable = 'False';
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if (arrayReadOnlyKolonaGrides[10] == 'True' || !lejomod)
        disable = 'True';
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, disable, idRresht, 'txtCmimiRi', vendosVleftat, hfFormatNumri, kontrolloVlereBosh);
}



/*
Function: myElemKodi

Nderton nje textbox dhe nje buton per te zgjedhur artikullin ose makron sipas llojit te zgjedhur tek combo Kategoria
*/
function myElemKodi(value, options) {
    disabled = !lejomod ? 'True' : arrayReadOnlyKolonaGrides[1];
    var idRresht = $('#rowed5').getLastSel2();
    return myJQGrid.myElemKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[1], ButtonClickKodi, keyPressKodi, changeFunc, fokusi, lostFocusKoloneFundit);
}

function myelemIdKodi(value, options) {//po
    var idRresht = $('#rowed5').getLastSel2();
    return myJQGrid.myElemIdKodi(value, options, !lejomod ? 'True' : arrayReadOnlyKolonaGrides[12], idRresht, arrayIdKolonaGrides[12]);
}


function keyPressKodi(event) {
    var idRresht = $('#rowed5').getLastSel2();
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
    callWebServiceInfoRow();
    if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1] == 'True' || !lejomod)
        return myJQGrid.myElemButtonFshi(true, idRresht, "#rowed5", lostFocusKoloneFundit);
    else
        return myJQGrid.myElemButtonFshi(false, idRresht, "#rowed5", lostFocusKoloneFundit);
}

/*
Function: myElemEmertimi

Nderton nje textbox ku vendoset emertimi i artikullit apo makros
*/
function myElemEmertimi(value) {
    var idRresht = $('#rowed5').getLastSel2();
    return myJQGrid.myElemEmertimi(value, !lejomod ? 'True' : arrayReadOnlyKolonaGrides[3], idRresht, arrayIdKolonaGrides[3]); // 'txtEmertimi'
}

function myElemKodbari(value) {//po      
    var idRresht = $('#rowed5').getLastSel2();
    return myJQGrid.myElemEmertimi(value, !lejomod ? 'True' : arrayReadOnlyKolonaGrides[2], idRresht, arrayIdKolonaGrides[2]);
}



/*
Function: myElemSasia

Nderton nje textbox per te vendosur sasine.
*/
function myElemSasia(value, options) {//po
    var disable = 'False';
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if (arrayReadOnlyKolonaGrides[6] == 'True' || !lejomod)
        disable = 'True';
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, disable, idRresht, 'txtSasia', vendosVleftat, hfFormatNumri, kontrolloVlereBosh);
}
function myElemSasiaRe(value, options) {//po
    var disable = 'False';
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if (arrayReadOnlyKolonaGrides[9] == 'True' || !lejomod)
        disable = 'True';
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, disable, idRresht, 'txtSasiaRe', vendosVleftat, hfFormatNumri, kontrolloVlereBosh);
}
/*
Function: myElemVlefta

Nderton nje textbox per te vendosur vleften.
*/
function myElemVlefta(value, options) {
    var disable = 'False';
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var readonly = 'False'
    if (arrayReadOnlyKolonaGrides[8] == 'True' || !lejomod)
        disable = 'True';
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, disable, idRresht, 'txtVlefta', vendosVleftat, hfFormatNumri, kontrolloVlereBosh);
}
function myElemVleftaRe(value, options) {
    var disable = 'False';
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var readonly = 'False'
    if (arrayReadOnlyKolonaGrides[11] == 'True' || !lejomod)
        disable = 'True';
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, disable, idRresht, 'txtVleftaRe', vendosVleftat, hfFormatNumri, kontrolloVlereBosh);
}
var pyeturDoni = 0;
function kontrolloRow(rowid) {//po
    var grida = $("#rowed5");

    var detajim = '';
    var name = grida.getTekstQelize('txtKodi', rowid);

    var gridIds = grida.jqGrid('getDataIDs');
    for (i = 0; i < gridIds.length; i++) {
        if (rowid == gridIds[i])
            continue;
        var editorkodi = grida.getTekstQelize('txtKodi', gridIds[i]);


        if (editorkodi == name && editorkodi != "") {
            myMesazh.ShtoMesazhGabimi('Nuk mund te vendosni nje artikull dy here ne gride');
            resetRreshtKorent(rowid);
            return true;


        }
    }

    return false;
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
    updateTotalet();
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

    var idArt = grida.getTekstQelize('txtIdKodi', idRreshti);
    var disabled = true;
    var arrayOptions;
    if (HfArt.Contains(idArt)) {
        var artikulli = HfArt.Get(idArt);
        arrayOptions = grida.formoArrayOptinosNjesia(artikulli);
        if (value === '')
            value = arrayOptions[0].text;
        if (arrayReadOnlyKolonaGrides[5] == "True")
            disabled = true;
        else disabled = false;
        if (idRreshti == idRow)
            return myJQGrid.myElemCombo(value, 'txtNjesia', idRreshti, minmax, arrayOptions, disabled);
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
    if (kodi !== "")
    {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheNjesiArtikulliComboMeKodRow"),
            data: JSON.stringify({ kodi: kodi, index: idRreshti, textNjesiZgjedhur: value, idNdermarrje: hfState.Get("idNdermarrje") })
        }).done(SucceededCallbackComboNjesiArtikulli);
    }

    return myJQGrid.myElemCombo(value, 'txtNjesia', idRreshti, minmax, arrayOptions, true);

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
    magazina1 = btneMagazina.GetText();
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
            if ((!llojartikulli && (colMagazina[i].IdLlojMagazine == 1 || colMagazina[i].IdLlojMagazine == 3)) || (llojartikulli && (colMagazina[i].IdLlojMagazine == 2 || colMagazina[i].IdLlojMagazine == 3))) {
                var objNjesi = new Object();
                objNjesi.value = colMagazina[i].IdNjesiAdministrative;
                objNjesi.text = colMagazina[i].Kodi;
                arrayOptions[j] = objNjesi;
                j++;
            }
        }
    }
    var disabled = false;
    if (arrayReadOnlyKolonaGrides[4] == 'True' || !lejomod)
        disabled = true;

    return myJQGrid.myElemCombo(value, arrayIdKolonaGrides[4], idRow, changeMag, arrayOptions, disabled);
}

function changeMagMerrGjendje(idreshti) {
    var grida = $('#rowed5');
    var idkodi = grida.getTekstQelize('txtIdKodi', idreshti);
    var idNdermarrje = hfState.Get("idNdermarrje");
    var idja = idkodi;
    if (!(typeof (idja) == "undefined" || idja == undefined || idja == "" || idja == "0")) {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheGjendjeVlefteSipasMagazines"),
            data: JSON.stringify({ idja: idkodi, rreshti: idreshti, data: dteDtDok.GetDate(), iddetajim: -1, magazina: grida.getTekstQelize('txtMagazina', idreshti), idndermarje: idNdermarrje })
        }).done(SucceededCallbackChangeMag);
    }
}
function changeMag() {
    ///<summary> metode per ndryshimin e vleres se magazines. kontrollon nese magazina eksiston tek comboja e magazines dhe therret webservicet e infos nqs ka info <summary>
    var idRresht = $('#rowed5').getLastSel2();
    if ($('#txtMagazina' + idRresht + ' option').length == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKjoMagazineNukEkziston"));
        $('#txtMagazina' + idRresht).val('');
    }
    changeMagMerrGjendje(idRresht);
    callWebServiceInfoRow(idRresht);
}
function SucceededCallbackChangeMag(result) {
    var grida = $('#rowed5');
    grida.setTekstQelize('txtSasia', result[0], result[2]);
    grida.setTekstQelize('txtVlefta', result[0], result[3]);
    grida.setTekstQelize('txtCmimi', result[0], result[2] != 0 ? result[3] / result[2] : 0);
    grida.setTekstQelize('txtSasiaRe', result[0], result[2]);
    grida.setTekstQelize('txtVleftaRe', result[0], result[3]);
    grida.setTekstQelize('txtCmimiRi', result[0], result[2] != 0 ? result[3] / result[2] : 0);
    updateTotalet();
}
function SucceededCallbackComboNjesiArtikulli(comboListNjesiArt) {
    var grida = $("#rowed5");
    var idRow = grida.getLastSel2();
    var idreshti = comboListNjesiArt[0];
    var textzgjedhur = comboListNjesiArt[1];
    comboListNjesiArt = comboListNjesiArt[2];
    if (idreshti == idRow && grida.getTekstQelize('txtKodi', idRow) != undefined) {

        if (arrayReadOnlyKolonaGrides[5] == 'True' || !lejomod)
            $('#' + arrayIdKolonaGrides[5] + idRow).replaceWith(myJQGrid.myElemCombo(textzgjedhur, arrayIdKolonaGrides[5], idRow, minmax, comboListNjesiArt, true).children()[0]);
        else
            $('#' + arrayIdKolonaGrides[5] + idRow).replaceWith(myJQGrid.myElemCombo(textzgjedhur, arrayIdKolonaGrides[5], idRow, minmax, comboListNjesiArt, false).children()[0]);
    }
    else {
        grida.setTekstQelize('txtNjesia', idRreshti, textzgjedhur);
    }
}

function minmax() {


}

function JoClick(s, e) {
    var grida = $('#rowed5');
    switch (identifikuesPyetje) {


        default:
            alert('Pyetje e panjohur');
            break;
    }
}

var queryString;
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
            url: Utils.getServerApiUrl("Rregjistrime", "ktheACListeArtikujshKodPershkKodbarEShpejt"), data: JSON.stringify({ infixText: vlera, pershk: pershk, grup: '', idNdermarrje: idNdermarrje, idPerdoruesi: idPerdorues, artikujTeShitshem: false, merrVetemAfatgjate: false, merrSipasDetajimit: false, klasa: "" })
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
    var idRresht = $("#rowed5").getLastSel2();
    myJQGrid.SucceededCallbackKodi(result, '#txtKodi' + idRresht);
}


/*
Function: lostFocusKoloneFundit

Percakton veprimin qe kryhet kur heqim fokusin nga kolona e fundit e gride (ruhet rreshti korent dhe shtohet nje rresht i ri bosh i editueshem).
Ketu kolona e fundit eshte "Vlefta" (sepse eshte rasti kur nuk po behet transferim).
*/
function lostFocusKoloneFundit() {
    var grida = $("#rowed5");
    //var idRresht = grida.getLastSel2();
      grida.lostFocusKoloneFundit();
    //jQuery("#txtMagazina" + idRresht).focus();
    //jQuery("#txtMagazina" + idRresht).blur();
    //jQuery("#txtKodi" + idRresht).focus();

}


function mbushGrideNgaHiddenFieldet(isLidhur) {
    var grida = $("#rowed5");
    var idRresht = grida.getLastSel2();
    if (arrayPershkrimiKolonaGrides.length == 0)
        return;
    if ($('#hfShtimModifikim').val() == "shtim") {
        return;
    }
    if ($('#hfShtimModifikim').val() == "modifikim") {
        var colTrupMag = JSON.parse($('#HfColTrupMag').val());
        var colArt = JSON.parse($('#HfColArt').val());
        var colNjesAdminis = JSON.parse($('#HfColNjesAdminis').val());
        var colNjesiArt = JSON.parse($('#HfColNjesiArt').val());

        var konfAmb = JSON.parse($('#HfKonfAmb').val());
        grida.setLastSel2(1);
        idRresht = 1;
        grida.jqGrid('clearGridData');
        var kodi, emertimi, magazina, njesia, sasia, cmimi, vlefta, sasiare, cmimiri, vleftare, idkodi;

        for (var i = 0; i < colArt.length; i++) {
            if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides - 1] == 'True' || isLidhur || !lejomod)
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

            njesia = colNjesiArt[i].KodNjesia;

            sasia = colTrupMag[i].SasiaGjendje;
            cmimi = colTrupMag[i].CmimiGjendje;
            if (sasia == 0)
                vlefta = colTrupMag[i].VleftaGjendje;
            else vlefta = parseFloat(sasia * cmimi);
            sasiare = colTrupMag[i].SasiaRe;
            cmimiri = colTrupMag[i].CmimiRi;
            if (sasiare == 0)
                vleftare = colTrupMag[i].VleftaRe;
            else vleftare = parseFloat(sasiare * cmimiri);

            var datarow = {
                txtNrRendor: i + 1, txtKodi: kodi, txtEmertimi: emertimi, txtMagazina: magazina,
                txtNjesia: njesia, txtIdKodi: idkodi, txtFshi: be
            };
            var su = grida.jqGrid('addRowData', parseInt(idRresht), datarow);
            grida.setTekstQelize('txtSasia', idRresht, sasia);
            grida.setTekstQelize('txtCmimi', idRresht, cmimi);
            grida.setTekstQelize('txtVlefta', idRresht, vlefta);
            grida.setTekstQelize('txtSasiaRe', idRresht, sasiare);
            grida.setTekstQelize('txtCmimiRi', idRresht, cmimiri);
            grida.setTekstQelize('txtVleftaRe', idRresht, vleftare);
            idRresht = idRresht + 1;
            grida.setLastSel2(idRresht);
        }
        updateTotalet();
    }
}

var arrKodi = new Array();
var arrEmertimi = new Array();
var arrMagazina = new Array();
var arrNjesia = new Array();
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
var NivelCmimi; // variabel qe mban nivelin e cmimit qe i eshte caktuar klientit qe kemi zgjedhur

function changeName() {
    myFaqeCelje.shtoHandlerSession();
    try {
        if (Utils.getUrlVar('id') == undefined)
            window.parent.callWebServiceKtheInfoLart('Shto_RegjistrimNdryshimCmimSasi.aspx', 0);
        else
            window.parent.callWebServiceKtheInfoLart('Shto_RegjistrimNdryshimCmimSasi.aspx', Utils.getUrlVar('id'));
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
        veprimi: 'RegjistrimNdryshimCmimSasi',
        listUrl: listUrl,
        niveli:niveli
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
function callWebserviceKonfigurimi(idKomp, kodKonf) {
    try {
        var idGjuha = hfState.Get('idGjuha');
        var idPerdoruesi = hfState.Get('idPerdoruesi');
        var idNdermarrje = hfState.Get('idNdermarrje');
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Konfigurime", "ktheKonfigAmbjentiMeFormatNumrash"),
            data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, kodKontrolli: '', idObjekti: -1, shtim: false, merrFormatKursi: false, merrGjitheKonf: false, idGjuha: idGjuha, idPerdoruesi: idPerdoruesi, idNdermarrje: idNdermarrje })
        }).done(SucceededCallbackKonfig);
        // Ia kalojme false parametrin shtim, sepse ne rastin e magazines duhet marre formati i numrit per monedhen baze pavaresisht klientit dhe nuk duhet te kontrollojme nese ka ndonje vlere default te konfigurimi per klientin.
        if ($('#hfShtimModifikim').val() != 'modifikim') {
            $.ajax({ pritPergjigje: true, url: Utils.getServerApiUrl("Rregjistrime", "ktheGrupimDokumentashNderm"), data: JSON.stringify({ kodkonfig: kodKonf, idNdermarrje: idNdermarrje, idPerdoruesi: idPerdoruesi }) }).done(Utils.SucceededCallbackGrupimDokumentash);
        }
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
    }
}

function getInfoArtStructure(idInfoArt) {
    try {
        var idNdermarrje = hfState.Get('idNdermarrje');        
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "getInfoArtStructure"),
            data: JSON.stringify({ idkoka: idInfoArt, idNdermarrje: idNdermarrje })
        }).done(SuccededCallbackInfoArtStructure);
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
var fokusi = 0; var info = false, lejomod = true;
var colGrida;
var ndryshimsasi;
var formatNumriZgjedhur;

function SucceededCallbackKonfig(result) {
    if ($("input[id$='hfShtimModifikim']").val() == "shtim")
        pastroFushatKokes();
    var hf = $("input[id$='hfShtimModifikim']");
    var hfAuto = $("#hfLupaAutomjet");
    var hfLidhur = $("input[id$='hfLidhur']");
    var arrTabela = ['tblFillim', "tblFund"];
    var arrPrind = ["dvFillim", "dvFundi"];
    var colKontrollet = result.colKontroll;  //[0];
    var colAtrTrupi = result.colAtrTrupi;  //[1];
    var grida = $('#rowed5');
    formatNumriZgjedhur = result.formatNumri;    //[7];
    mbushArrayMagazinat();

    var vlerat = myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, undefined, hf, '', arrTabela, undefined, undefined, hfLidhur, arrPrind);
    $("#divgride1").show();
    $("#dvFillim").show();
    cmbKonfigurimi.ShowDropDown();
    cmbKonfigurimi.HideDropDown();
    $("#dvFundi").show();
    vendosDateDefault();

    var colKontrollet = result.colKontroll; //[0];
    var colAtrTrupi = result.colAtrTrupi;  //[1];
    colGrida = result.colGrida;  //[2];
    colKushte = result.colKushte;  //[3];
    colAlterKusht = result.colAlterKusht;  //[4];
    var kodniveli = result.kodniveli; //[5];
    $('#HfGridCol').val(JSON.stringify(colGrida));
    var hfMag = $("#hfLupaMagazina")[0];

    var hfrivleresim = $('#hfKontrollRivleresim');
    $('#hfKontrolletNrAutom').val(JSON.stringify(colKontrollet));
    $('#hfAtributeNrAutom').val(JSON.stringify(colAtrTrupi));
    if ($("input[id$='hfShtimModifikim']").val() == "shtim") {
        hfNrAuto.Clear();
        hfNrAutoShitje.Clear();
        vendosNrAutomatik(colAtrTrupi, colKontrollet);
    }
    var hfLlog = $("#hfLlogaria");
    for (var i = 0; i < colKontrollet.length - 1; i++) {
        //id e konfigurimit te lupes vendoset ne hidden field
        //vlera e saj do t'i kalohet si query string hapjes se popUp-it

        if (colKontrollet[i].KodKontrolli == "btneMagazina") {
            if (btneMagazina.GetText() != "" && $('#hfShtimModifikim').val() != 'modifikim') {
                try {
                    $.ajax({
                        pritPergjigje: true,
                        url: Utils.getServerApiUrl("Rregjistrime", "ktheDegeMagazineSipasKodit"),
                        data: JSON.stringify({ kodi: btneMagazina.GetText(), idNdermarrje: hfState.Get('idNdermarrje'), idPerdoruesi: hfState.Get('idPerdoruesi') })
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

    pershk = 1;
    konfirmimArt = 2;
    njesiDef = 1;
    cmimzero = 1;
    ndryshimsasi = true;
    kodkodbar = 1;
    var hidField1 = $("#hfKontabilizimi")[0];
    hidField1.value = 0;
    var modinfo = 0;
    for (j = 0; j < colKushte.length; j++) {


        if (colKushte[j].Kodi == 'P') {
            if (colAlterKusht[j].Alternativa == 'Pershkrimi 1')
                pershk = 1;
            else
                pershk = 2;
        }

        if (colKushte[j].Kodi == 'KR') {
            if (colAlterKusht[j].Alternativa == 'Po')
                hfrivleresim.val(true);
            else
                hfrivleresim.val(false);
        }
        if (colKushte[j].Kodi == 'ZIA') {
            if (colKushte[j].Vlera != "0") {
                if ($('#hfHapurMbyllur').val() == 'True')
                    splitter.GetPane(1).Expand();
                else splitter.GetPane(1).CollapseBackward();
                idInfoArt = colKushte[j].Vlera;
                try {
                    $.ajax({
                        pritPergjigje: true,
                        url: Utils.getServerApiUrl("Rregjistrime", "ruajNeSessionTrupInfoArt"),
                        data: JSON.stringify({ idkoka: colKushte[j].Vlera })
                    }).done(Succedcallback);
                }
                catch (e) {
                    myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
                }
                infoArt = true;
            }
            else {
                splitter.GetPane(1).CollapseBackward();
                try {
                    $.ajax({
                        pritPergjigje: true,
                        url: Utils.getServerApiUrl("Rregjistrime", "ruajNeSessionTrupInfoArt"),
                        data: JSON.stringify({ idkoka: -1 })
                    }).done(Succedcallback);
                }
                catch (e) {
                    myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
                }
                infoArt = false;
            }
        }

        if (colKushte[j].Kodi == 'NJ') {
            if (colAlterKusht[j].Alternativa == 'Njesia 1')
                njesiDef = 1;
            else
                njesiDef = 2;
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
        if (colKushte[j].Kodi == 'F') {
            if (colAlterKusht[j].Alternativa == 'Sasia')
                fokusi = 1;
            else if (colAlterKusht[j].Alternativa == 'Rreshti tjeter') fokusi = 2;
            else fokusi = 0;
        }
        if (colKushte[j].Kodi == 'LLN') {
            if (colAlterKusht[j].Alternativa == 'Ndryshim sasie')
                ndryshimsasi = true;
            else if (colAlterKusht[j].Alternativa == 'Ndryshim cmimi') ndryshimsasi = false;

        }
    }


    grida.jqGrid('GridUnload', "rowed5");;
    var isLidhur = (hfLidhur.val().toLowerCase() === 'true');
    formGridColsArray(isLidhur);
    grida.setLastSel2(-1);
    grida = $('#rowed5');

    ruajFormatetNeGride(grida, formatNumriZgjedhur);

    inicializoGride(isLidhur);
    mbushGrideNgaHiddenFieldet(isLidhur);
    Utils.konfiguroAccorditionPasKonfigDokumenti(varKonfig.identifikuesPerLocalStorageKey);
}

function DateChanged(s, e) {
    var grida = $("#rowed5");
    var idRresht = grida.getLastSel2();
    var hfKontrollet = JSON.parse($('#hfKontrolletNrAutom').val());
    var atributet = JSON.parse($('#hfAtributeNrAutom').val());
    vendosNrAutomatik(atributet, hfKontrollet);
    callWebServiceInfoRow(idRresht);
    var rreshtaTeGrides = grida.jqGrid('getDataIDs');
    for (i = 0; i < rreshtaTeGrides.length; i++) {

        changeMagMerrGjendje(rreshtaTeGrides[i]);
    }

}

var pershk = 1;
var konfirmimArt = 2;
var njesiDef = 1;

var cmimzero = 1;
var magdefault = 0;
/*
Function: ndryshoKonfigurimin
    
Therret funksionin <callWebserviceKonfigurimi> per te vendosur nje konfigurim te ri.
*/
function ndryshoKonfigurimin() {
    var pershkKonfigAmb = cmbKonfigurimi.GetSelectedItem().GetColumnText("PershkrimKonfigAmbjente");
    if (pershkKonfigAmb != undefined)
        //lblKonfigurimi.SetText(pershkKonfigAmb);
        $('#kokeKonfigurimi').text(hfState.Get("MenuKokeDokumenti") +': ' + pershkKonfigAmb);
    callWebserviceKonfigurimi(545, cmbKonfigurimi.GetText());
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
Function: TextChangedMagazina
    
Vendos ne gride magazinen qe zgjidhet te koka
*/
function TextChangedMagazina() {
    //vendoset magazina e zgjedhur te koka ne rreshtin qe po editohet
    var grida = $("#rowed5");
    var idRresht = grida.getLastSel2();
    magazina1 = btneMagazina.GetText();

    var magazinagrides = $('#txtMagazina' + idRresht)[0];
    if (magazinagrides != undefined) {
        for (var i = 0; i < magazinagrides.length; i++) {
            if (magazinagrides[i].text == magazina1)
                magazinagrides.selectedIndex = i;
        } reshtiieditueshem = true;
        callWebServiceInfoRow(idRresht);
        changeMagMerrGjendje(idRresht);
    }

    var rreshtaTeGrides = grida.jqGrid('getDataIDs');
    for (i = 0; i < rreshtaTeGrides.length; i++) {
        if (!reshtiieditueshem)
            grida.jqGrid('setCell', rreshtaTeGrides[i], 'txtMagazina', magazina1, 'clientArray', '');
        else if (rreshtaTeGrides[i] != idRresht) {
            grida.jqGrid('setCell', rreshtaTeGrides[i], 'txtMagazina', magazina1, 'clientArray', '');
        }
        changeMagMerrGjendje(rreshtaTeGrides[i]);
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
    var lloji = cmbLloji.GetText();

    var mod;
    if ($("#hfShtimModifikim")[0].value == 'modifikim')
        mod = true;
    else mod = false;
    if (cmbLloji.GetText() != "")
        callWebserviceNiveliNew(cmbLloji.GetValue(), 'ndryshimcmimisasi', mod);
    else
        callWebserviceNiveliNew('', 'ndryshimcmimisasi', mod);
}

function callWebserviceNiveliNew(lloji, tipi, mod) {
    try {
        var idNdermarrje = hfState.Get('idNdermarrje');
        var idPerdoruesi = hfState.Get('idPerdoruesi');
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheTemplateNiveli"), data: JSON.stringify({ idNiveli: lloji, veprimi: tipi, mod: mod, idPerdoruesi: idPerdoruesi, idNdermarrje: idNdermarrje, idGjuha: hfState.Get('idGjuha') })
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
        editorNjesia = grida.getTekstQelize('txtNjesia', idTe[i]);
        editorSasia = grida.getTekstQelize('txtSasia', idTe[i]);
        editorCmimi = grida.getTekstQelize('txtCmimi', idTe[i]);
        editorVlefta = grida.getTekstQelize('txtVlefta', idTe[i]);

        if ($('#hfRuajDraft').val() != "Draft") {
            if (cmimzero == 2) {
                if (editorKodi != '' && parseFloat(editorCmimi) == 0.00) {
                    myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukDuhetTeKeteArtikujMeCmimZero"));
                    e.processOnServer = false; click = false;
                    return;
                }
            }
            else if (editorKodi != '' && parseFloat(editorCmimi) == 0.00) {
                myMesazh.ShtoMesazhInformues(hfState.Get("msgKujdesKaCmimZeroNeGride"));
                Utils.shfaqLoadingGif();;
            }
        }
        if (editorKodi != "") { trupiBosh = false; }

        if (btneMagazina.GetEnabled() && editorMagazina == "" && editorKodi != '') {
            kamag = false;
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgShenoniMagazinen"));
            e.processOnServer = false;
        }

        if (editorKodi != "" && editorNjesia == "")
            panjesi = true;



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
    var idPerdoruesi = hfState.Get('idPerdoruesi');
    var idNdermarrje = hfState.Get('idNdermarrje');
    var idViti = hfState.Get('idViti');
    myJQGrid.ruajKolonatEGrides(grida, idGride, idGjuha, idNdermarrje, idViti, idPerdoruesi);
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
    if (btneMagazina.GetText() !== '') {
        var idMagazina = btneMagazina.GetValue();
        popupUniversal.SetContentUrl('LupaArtikull.aspx?idKonfigAmbjente=' + queryStr + '&grup=' + grup + '&idMag=' + idMagazina);
    }
    else
        popupUniversal.SetContentUrl('LupaArtikull.aspx?idKonfigAmbjente=' + queryStr + '&grup=' + grup);
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
    var sasia = grida.getTekstQelize('txtSasiaRe', idRresht);
    var cmimi = grida.getTekstQelize('txtCmimiRi', idRresht);
    var vlefta = grida.getTekstQelize('txtVleftaRe', idRresht);
    var editorKodi = grida.getTekstQelize('txtKodi', idRresht);
    var detajim = -1;

    if (sasia == '.') {
        grida.setTekstQelize('txtSasiaRe', idRresht, '0.');
        return;
    }
    if (cmimi == '.') {
        grida.setTekstQelize('txtCmimiRi', idRresht, '0.');
        return;
    }

    if (isNaN(sasia)) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiaDuhetNumer"));
        sasia = grida.getVlereDefault('txtSasiaRe');
        //grida.setTekstQelize('txtSasia', lastsel2, sasia);
        $('#txtSasiaRe' + idRresht).focus();
    }
    //if (sasia == "0") {
    //    myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiaNukMundTeJeteZero"));
    //    sasia = grida.getVlereDefault('txtSasiaRe');
    //    //grida.setTekstQelize('txtSasia', lastsel2, sasia);
    //    $('#txtSasiaRe' + lastsel2).focus();
    //}

    if (cmimi === '' || isNaN((cmimi))) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgCmimiDuhetTeJeteNumer"));
        cmimi = grida.getVlereDefault('txtCmimiRi');
        //grida.setTekstQelize('txtCmimi', lastsel2, cmimi);
        $('#txtCmimiRi' + idRresht).focus();
    }
    if (isNaN(vlefta)) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgVleftaDuhetTeJeteNumer"));
        vlefta = grida.getVlereDefault('txtVleftaRe');
        grida.setTekstQelize('txtVleftaRe', idRresht, vlefta);
        $('#txtVleftaRe' + idRresht).focus();
    }

    if (s == 'txtCmimiRi'  || s == 'txtSasiaRe' ) {
        if (!isNaN(parseFloat(cmimi)) && !isNaN(parseFloat(sasia))) {
            grida.setTekstQelize('txtVleftaRe', idRresht, parseFloat(sasia) * parseFloat(cmimi));
        }
    }
    else if (s == 'txtVleftaRe' ) {
        if (!isNaN(parseFloat(vlefta)) && !isNaN(parseFloat(sasia))) {
            grida.setTekstQelize('txtCmimiRi', idRresht, parseFloat(parseFloat(vlefta) / sasia));
        }

    }
    updateTotalet();
}


function kontrolloVlereBosh() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var sasia = grida.getTekstQelize('txtSasiaRe', idRresht);
    var cmimi = grida.getTekstQelize('txtCmimiRi', idRresht);
    var vlefta = grida.getTekstQelize('txtVleftaRe', idRresht);
    var updateVleften = false;
    if (cmimi === '' || isNaN(parseFloat(cmimi))) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgCmimiDuhetTeJeteNumer"));
        cmimi = grida.getVlereDefault('txtCmimiRi');
        grida.setTekstQelize('txtCmimiRi', idRresht, cmimi);
        updateVleften = true;
    }
    if (cmimi <= 0) {
        myMesazh.ShtoMesazhGabimi("Cmimi duhet te jete me i madh se 0!");
        cmimi = grida.getVlereDefault('txtCmimiRi');
        grida.setTekstQelize('txtCmimiRi', idRresht, cmimi);
        updateVleften = true;
    }

    if (sasia === '' || isNaN(parseFloat(sasia))) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiaDuhetNumer"));
        sasia = grida.getVlereDefault('txtSasiaRe');
        grida.setTekstQelize('txtSasiaRe', idRresht, sasia);
        updateVleften = true;
    }
    if (sasia < 0) {
        myMesazh.ShtoMesazhGabimi("Sasia duhet te jete me e madhe se 0!");
        sasia = grida.getVlereDefault('txtSasiaRe');
        grida.setTekstQelize('txtSasiaRe', idRresht, sasia);
        updateVleften = true;
    }
    if (!ndryshimsasi && grida.getTekstQelize('txtSasia', idRresht) == 0 && cmimi > 0) {
        myMesazh.ShtoMesazhGabimi("Nuk mund te ndryshohet cmimi per nje artikull qe nuk ka gjendje!");
        cmimi = grida.getVlereDefault('txtCmimiRi');
        grida.setTekstQelize('txtCmimiRi', idRresht, cmimi);
        updateVleften = true;
    }
    if (updateVleften)
        grida.setTekstQelize('txtVleftaRe', idRresht, parseFloat(sasia) * parseFloat(cmimi));
}

/*
Function: vendosCmimin

Llogarit cmimin kur shenohen sasia dhe vlefta dhe therret funksionin <updateTotalet>.
*/
function vendosCmimin() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var sasia = grida.getTekstQelize('txtSasiaRe', idRresht);
    var cmimi = grida.getTekstQelize('txtCmimiRi', idRresht);
    var vlefta = grida.getTekstQelize('txtVleftaRe', idRresht);

    if (isNaN(sasia)) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiaDuhetNumer"));
        sasia = grida.getVlereDefault('txtSasiaRe');
        grida.setTekstQelize('txtSasiaRe', idRresht, sasia);
        $('#txtSasiaRe' + idRresht).focus();
    }
    else if (sasia == "0") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiaNukMundTeJeteZero"));
        sasia = grida.getVlereDefault('txtSasiaRe');
        grida.setTekstQelize('txtSasiaRe', idRresht, sasia);
        $('#txtSasiaRe' + idRresht).focus();
    }

    if (isNaN(cmimi.value)) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgCmimiDuhetTeJeteNumer"));
        cmimi = grida.getVlereDefault('txtCmimiRi');
        grida.setTekstQelize('txtCmimiRi', idRresht, cmimi);
        $('#txtCmimiRi' + idRresht).focus();
    }
    if (isNaN(vlefta.value)) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgVleftaDuhetTeJeteNumer"));
        vlefta = grida.getVlereDefault('txtVleftaRe');
        grida.setTekstQelize('txtVleftaRe', idRresht, vlefta);
        $('#txtVleftaRe' + idRresht).focus();
    }
    grida.setTekstQelize('txtCmimiRi', idRresht, parseFloat(parseFloat(vlefta) / sasia));
    updateTotalet();
}



function updateTotalet() {
    var totali = 0;
    var grida = $("#rowed5");
    var idTe = grida.jqGrid('getDataIDs');
    for (var i = 0; i < idTe.length; i++) {
        if (grida.getTekstQelize('txtKodi', idTe[i]) != "") {
            var vlefta = grida.getTekstQelize('txtVleftaRe', idTe[i]);
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
    btneMagazina.SetValue(null);;
    if (btneMagazina.GetItemCount() == 1)
        btneMagazina.SetSelectedIndex(0);
    cmbLlogariKunderParti.SetValue(null);;
    txtNrDok.SetText('');
    cmbDegeAdministrative.SetValue(null);;

    txtVlefta.SetText(0);
    cmbGrup1.SetValue(null);
    cmbGrup2.SetValue(null);
    cmbGrup3.SetValue(null);
    txtPershkrimi.SetText('');


    var hf = document.getElementById("status1");
    hf.value = "false";
    vendosDateDefault();
    pyeturDoni = 0;
}

var dtDokumentit;


function vendosDateDefault() {
    if ($("input[id$='hfShtimModifikim']").val() == "shtim") {
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
    var idRresht = grida.getLastSel2();
   if( !Utils.nrWsRrugesManager.kanePerfunduarWs() ){
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgPoTransferohetTeDhenatShtypniPerseriRuaj"));
        return false;
    }
    editorCmimi = jQuery("#txtCmimi" + idRresht)[0];
    editorKodi = jQuery("#txtKodi" + idRresht)[0];
    editorLloji = jQuery("#txtKategoria" + idRresht)[0];
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



    else if ($('#hfRuajDraft').val() != "Draft" && editorCmimi != undefined && cmimzero == 2 && grida.getTekstQelize('txtCmimi', idRresht) == 0.00 && grida.getTekstQelize('txtKodi', idRresht) != '') {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukLejohetCmimZeroNeGride"));
        editorCmimi.focus();
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

        myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimNdryshimCmimSasi.aspx?shtim_modifikim=shtim', true);

    } else click = false;
}

var shtoTimer;
function shtoTimedClick(e) {
    if (!Utils.nrWsRrugesManager.kanePerfunduarWs())
        shtoTimer = setTimeout(function () { shtoTimedClick(e) }, 500);
    else {
        clearTimeout(shtoTimer);
        myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimNdryshimCmimSasi.aspx?shtim_modifikim=shtim', true);
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
        myFaqeCelje.validim(s, e);
        if (isValidKoka()) {
            Utils.shfaqLoadingGif();;
            RuajClick(s, e);
        }
        else {
            e.processOnServer = false;
        }
    }
    else if (e.item.name == 'Kerko') {
        myFaqeCelje.kontrolloTeDrejta('RegjistrimNdryshimCmimSasi.aspx', null, true);
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
        popFshi.Show();
        e.processOnServer = false;
    }
    else if (e.item.name == 'Anullo') {
        myFaqeCelje.kontrolloTeDrejta('RegjistrimNdryshimCmimSasi.aspx');
        e.processOnServer = false;
        click = false;
    }
}

function Kthehu(mesazh) {
    myFaqeCelje.kontrolloTeDrejta('RegjistrimNdryshimCmimSasi.aspx?ruaj=po');

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
        else if (panjesi == true) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgKaArtikujPaNjesi"));
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
    pastro();
    pastroFushatKokes();
    var hf = $("#hfShtimModifikim");
    ASPxMenu1.GetItemByName('Shto').SetVisible(true);
    ASPxMenu1.GetItemByName('Ruaj').SetVisible(true);
    ASPxMenu1.GetItemByName('FletaKontabel').SetVisible(false);
    ASPxMenu1.GetItemByName('Fshi').SetVisible(false);
    ASPxMenu1.GetItemByName('QendraKosto').SetVisible(false);
    ASPxMenu1.GetItemByName('Draft').SetVisible(true);
    hf.val("shtim");
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
    var qs = '?vjenNga=Shto_RegjistrimNdryshimCmimSasi&veprimi=shtim';
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
        var qs = '?vjenNga=Shto_RegjistrimNdryshimCmimSasi&veprimi=modifikim&kodArt=' + kodArtikulli;
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
    else $('#hfHapurMbyllur').val('True')

    var idPerdoruesi = hfState.Get('idPerdoruesi');
    try {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ruajHapurMbyllur"), data: JSON.stringify({ hapur: hapur, idperdorues: idPerdoruesi, idperdoruesveprimi: idPerdoruesi })
        }).done(SuccededCallbackHapurMbyllur);
    } catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
    }
}


function RuajHapurMbyllurplus(hapur) {
    var idPerdoruesi = hfState.Get('idPerdoruesi');

    mbyll = false;
    if (!hapur) { splitter.GetPane(1).CollapseBackward(); $('#hfHapurMbyllur').val('False'); }
    else $('#hfHapurMbyllur').val('True')
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ruajHapurMbyllurplus"), data: JSON.stringify({ hapur: hapur, idperdorues: idPerdoruesi, idperdoruesveprimi: idPerdoruesi })
    }).done(SuccededCallbackHapurMbyllur);
}

function RuajHapurMbyllurminus(hapur) {
    var idPerdoruesi = hfState.Get('idPerdoruesi');

    mbyll = false;
    if (!hapur) { splitter.GetPane(1).CollapseBackward(); $('#hfHapurMbyllur').val('False'); }
    else {
        $('#hfHapurMbyllur').val('True')
        $.ajax({
            pritPergjigje: true, url: Utils.getServerApiUrl("Rregjistrime", "ruajHapurMbyllurminus"), data: JSON.stringify({ hapur: hapur, idperdorues: idPerdoruesi, idperdoruesveprimi: idPerdoruesi })
        }).done(SuccededCallbackHapurMbyllur);
    }
}


function SuccededCallbackHapurMbyllur(result) {
    myMesazh.ShtoMesazhSuksesi(result);
}
function HeaderClick(s, e) {
    if (!mbyll) { e.cancel = true; mbyll = true; } //per rastet kur shtyp butonat + dhe -

}



var panjesi = false;

function ButtonOkQKClick(s, e) {
    //popMesazhQK.Hide();
    myButtonClickLupa.LupaUniversal_Click('Shperndarje ne qendrat e kostos', $('#hfUrl').val(), 900, 600);
}
