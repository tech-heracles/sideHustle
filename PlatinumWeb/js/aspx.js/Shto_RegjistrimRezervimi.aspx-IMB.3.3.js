;
var kodkodbar = 1;
var lidhur = false;
var arrayMeMagazina = new Array();
var colMagazina;
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
var widthLupaArtikull = 700;
var heightLupaArtikull = 600;
var widthLupaKF = 800;
var heightLupaKF = 600;
var widthLupaMakro = 600;
var heightLupaMakro = 600;
var widthLupaMagazina = 600;
var heightLupaMagazina = 600;
var widthLupaPeriudha = 600;
var heightLupaPeriudha = 600;
var widthLupaKerko = 600;
var heightLupaKerko = 600;
var widthLupaDetajim = 600;
var heightLupaDetajim = 600;
var editorData;

//var STR_sasiaNumer = 'Sasia duhet të jetë numer!';

var varKonfig = {
    identifikuesPerLocalStorageKey: 'RegjistrimRezervimi'
};

jQuery(document).ready(function () {
    //formoArrayKolonaGrides();
    ////var isLidhur = ($("input[id$='hfLidhur']").val().toLowerCase() === 'true' || cmbStatusi.GetSelectedIndex() == 1 || cmbStatusi.GetSelectedIndex() == 2);
    ////formGridColsArray(isLidhur);
    ////inicializoGride(isLidhur);
    ////mbushGrideNgaHiddenFieldet(isLidhur);
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
            //                $("#rowed5").setGridWidth($('#divgride2').width() - 5, true);
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
        ////    callWebserviceKonfigurimi("510"); //510 = id komponente (Shto_RegjistrimMagazine.aspx)
        //var hf = document.getElementById("hfKonffillestar");

        //cmbKonfigurimi.SetText(hf.value);
        //ndryshoKonfigurimin();
        document.getElementById("kokeKonfigurimi").innerHTML = hfState.Get("MenuKokeDokumenti");
        document.getElementById("trupKonfigurimi").innerHTML = hfState.Get("MenuTrupDokumenti");
        document.getElementById("fundKonfigurimi").innerHTML = hfState.Get("MenuFundDokumenti");
        identifikuesPerPopupDokumentat = "Shto_RegjistrimRezervimi.aspx";
        identikuesPerPopupKlientFurnitori = "RegjistrimRezervimi";
        identikuesPerPopupArtikulli = "RegjistrimRezervimi";
        identifikuesPerPopupMakro = "RegjistrimRezervimi";
        identifikuesPerPopupMagazina = "RegjistrimRezervimi"
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
    mbushArrayMagazinat();
    var classes = '';
    if (isLidhur === true && $("input[id$='hfShtimModifikim']").val() == 'modifikim')
        classes = 'uigray';

    var arrayPershkrime = [arrayPershkrimiKolonaGrides[0], arrayPershkrimiKolonaGrides[1], arrayPershkrimiKolonaGrides[2], arrayPershkrimiKolonaGrides[3],
                           arrayPershkrimiKolonaGrides[4], arrayPershkrimiKolonaGrides[5], arrayPershkrimiKolonaGrides[6], arrayPershkrimiKolonaGrides[7], arrayPershkrimiKolonaGrides[8], arrayPershkrimiKolonaGrides[9], arrayPershkrimiKolonaGrides[10], arrayPershkrimiKolonaGrides[11], arrayPershkrimiKolonaGrides[12], arrayPershkrimiKolonaGrides[13]];
    var arrayModel = [
        { name: arrayIdKolonaGrides[0], index: arrayIdKolonaGrides[0], width: arrayWidthKolonaGrides[0], hidden: arrayVisibleKolonaGrides[0], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myElemCombo, custom_value: myJQGrid.myValueCombo } },
        { name: arrayIdKolonaGrides[1], index: arrayIdKolonaGrides[1], width: arrayWidthKolonaGrides[1], hidden: arrayVisibleKolonaGrides[1], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myElemKodi, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[2], index: arrayIdKolonaGrides[2], width: arrayWidthKolonaGrides[2], hidden: arrayVisibleKolonaGrides[2], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myElemEmertimi, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[3], index: arrayIdKolonaGrides[3], width: arrayWidthKolonaGrides[3], hidden: arrayVisibleKolonaGrides[3], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemComboMagazina, custom_value: myJQGrid.myValueCombo } },
        { name: arrayIdKolonaGrides[4], index: arrayIdKolonaGrides[4], width: arrayWidthKolonaGrides[4], hidden: arrayVisibleKolonaGrides[4], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemComboNjesiaSup, custom_value: myJQGrid.myValueCombo } },
        { name: arrayIdKolonaGrides[5], index: arrayIdKolonaGrides[5], width: arrayWidthKolonaGrides[5], hidden: arrayVisibleKolonaGrides[5], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemSasiGjendje, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[6], index: arrayIdKolonaGrides[6], width: arrayWidthKolonaGrides[6], hidden: arrayVisibleKolonaGrides[6], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemSasiRez, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[7], index: arrayIdKolonaGrides[7], width: arrayWidthKolonaGrides[7], hidden: arrayVisibleKolonaGrides[7], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemSasiPritje, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[8], index: arrayIdKolonaGrides[8], width: arrayWidthKolonaGrides[8], hidden: arrayVisibleKolonaGrides[8], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemSasiDisponueshme, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[9], index: arrayIdKolonaGrides[9], width: arrayWidthKolonaGrides[9], hidden: arrayVisibleKolonaGrides[9], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemSasia, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[10], index: arrayIdKolonaGrides[10], width: arrayWidthKolonaGrides[10], hidden: arrayVisibleKolonaGrides[10], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemSasiMbetur, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[11], index: arrayIdKolonaGrides[11], width: arrayWidthKolonaGrides[11], hidden: arrayVisibleKolonaGrides[11], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myelemIdKodi, custom_value: myJQGrid.myValueTextBox }, hidedlg: true },
        { name: arrayIdKolonaGrides[12], index: arrayIdKolonaGrides[12], width: arrayWidthKolonaGrides[12], hidden: arrayVisibleKolonaGrides[12], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myelemIdTrupi, custom_value: myJQGrid.myValueTextBox }, hidedlg: true },
        { name: arrayIdKolonaGrides[13], index: arrayIdKolonaGrides[13], width: arrayWidthKolonaGrides[13], hidden: arrayVisibleKolonaGrides[13], classes: classes, sorttype: "int", editable: true, edittype: 'custom', editoptions: { custom_element: myElemButtonFshi, custom_value: myValueButtonFshi }, hidedlg: true }
    ];

    var selektoriGrides = "#rowed5";

    var gridParams = {
        emergride: selektoriGrides,
        arrayPershkrime: arrayPershkrime,
        arrayModel: arrayModel,
        lidhur: isLidhur,
        emerEditorKodi: "txtKodi",
        emerEditorLloji: "txtKategoria",
        cmimzero: cmimzero,
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
            ruajKolonatEGrides: ruajKolonatEGrides,
            hapPopUpRi: hapPopUpRi,
            hapPopUpModifikoArt: hapPopUpModifikoArt
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
    return;
}

/*
Vendos vlerat default te fushave ne gride ne baze te emrit te kolones
*/
function vendosVleraDefaultNeGride(grida) {
    grida.setVlereDefault('txtSasiGjendje', 0);
    grida.setVlereDefault('txtSasiaRez', 0);
    grida.setVlereDefault('txtSasiaPritje', 0);
    grida.setVlereDefault('txtSasiaDisp', 0);
    grida.setVlereDefault('txtSasia', 1);
    grida.setVlereDefault('txtSasiaMbetur', 0);
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
    grida.setShifraPasPresjes('txtSasiGjendje', formatNumri.ShifraPasPresjesSasia);
    grida.setShifraPasPresjes('txtSasiaRez', formatNumri.ShifraPasPresjesSasia);
    grida.setShifraPasPresjes('txtSasiaPritje', formatNumri.ShifraPasPresjesSasia);
    grida.setShifraPasPresjes('txtSasiaDisp', formatNumri.ShifraPasPresjesSasia);
    grida.setShifraPasPresjes('txtSasia', formatNumri.ShifraPasPresjesSasia);
    grida.setShifraPasPresjes('txtSasiaMbetur', formatNumri.ShifraPasPresjesSasia);
}

/*
Formaton vlerat e fushave sipas formatit perkates.
*/
function vendosKonfigFormatNumri() {
    var grida = $('#rowed5');
    var idTe = grida.jqGrid('getDataIDs');
    for (j = 0; j < idTe.length; j++) {
        var idRreshti = idTe[j];
        grida.formatoQelize('txtSasiGjendje', idRreshti);
        grida.formatoQelize('txtSasiaRez', idRreshti);
        grida.formatoQelize('txtSasiaPritje', idRreshti);
        grida.formatoQelize('txtSasiaDisp', idRreshti);
        grida.formatoQelize('txtSasia', idRreshti);
        grida.formatoQelize('txtSasiaMbetur', idRreshti);
    }
}
function selectFunc(event, ui, emerfushe, idArt, kodArt) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    var detajim = -1;
    var lloj = Utils.getUrlVar('lloj');
    var magazine = $('#txtMagazina' + idRow).val();
    var myDate = dteDtDok.GetDate();
    myDate.setDate(myDate.getDate() + 365 * 2);

    if (idArt != undefined && kodArt != undefined && idArt != "" && kodArt != "") {
        $(emerfushe).val(kodArt);
        if (!kontrolloRow(idRow)) {
            $.ajax({
                url: Utils.getServerApiUrl("Rregjistrime", "ktheVleraArtRez"),
                data: JSON.stringify({ idja: idArt, rreshti: idRow, data: myDate, iddetajim: detajim, idmag: magazine })
            }).done(SucceededCallbackArtNew);
        }
        return false;
    }
    if (ui.item != null && ui.item != null) {
        $(emerfushe).val(ui.item.label);
        var kategoria = grida.getTekstQelize('txtKategoria', idRow);
        if (kategoria == "Artikull") {
            $.ajax({
                url: Utils.getServerApiUrl("Rregjistrime", "ktheVleraArtRez"),
                data: JSON.stringify({ idja: ui.item.value, rreshti: idRow, data: myDate, iddetajim: detajim, idmag: magazine })
            }).done(SucceededCallbackArtNew);
        }
        return false;
    }
}

function changeFunc(event, ui, emerKodi, index) {
    var detajim = -1;
    //var index = -1;
    //var idKod = 'txtKodi';
    var lloj = Utils.getUrlVar('lloj');
    var magazine = 0;
    var myDate = dteDtDok.GetDate();
    myDate.setDate(myDate.getDate() + 365 * 2);

    //if (emerKodi === undefined || emerKodi === null)
    //    index = myJQGrid.getIndexFromEvent(event, idKod);
    //else
    //    index = emerKodi.split(idKod)[1];
    var idNdermarrje = hfState.Get("idNdermarrje");
    var idPerdorues = hfState.Get("idPerdoruesi");
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    if (index == idRow) {
        magazine = $('#txtMagazina' + index).val();
        var kategoria = grida.getTekstQelize('txtKategoria', idRow);
        var emerfushe = "#" + emerKodi + index;
        if (ui == null || ui.item == null) {
            var kodi = $(emerfushe).val();
            if (typeof (kodi) != "undefined" && kodi != undefined && kodi != "") {
                if (kategoria == "Artikull") { //Artikull                       
                    $.ajax({
                        pritPergjigje: true,
                        url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraArtRezMeKodOseKodBar"),
                        data: JSON.stringify({ kodi: kodi, rreshti: index, data: myDate, iddetajim: detajim, idmag: magazine, idNdermarrje: idNdermarrje, idPerdoruesi: idPerdorues })
                    }).done(SucceededCallbackArtNew);
                    return;
                }
            }
            if (kategoria == "Artikull") {
                vendosArt([index, null]);
                return;
            }
        }
        if (kategoria == "Artikull") { //Artikull
            $.ajax({
                url: Utils.getServerApiUrl("Rregjistrime", "ktheVleraArtRez"),
                data: JSON.stringify({ idja: ui.item.value, rreshti: index, data: myDate, iddetajim: detajim, idmag: magazine })
            }).done(SucceededCallbackArtNew);
            return;
        }
        return;
    }
    var rreshti = grida.jqGrid('getRowData', index);

    if (ui == null || ui.item == null) {
        var kodi = rreshti.txtKodi;
        if (typeof (kodi) != "undefined" && kodi != undefined && kodi != "") {
            if (rreshti.txtKategoria == "Artikull") { //Artikull           
                magazine = $('#txtMagazina' + idRow).val();
                $.ajax({
                    pritPergjigje: true,
                    url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraArtRezMeKodOseKodBar"),
                    data: JSON.stringify({ kodi: kodi, rreshti: index, data: myDate, iddetajim: detajim, idmag: magazine,  idNdermarrje: idNdermarrje, idPerdoruesi: idPerdorues })
                }).done(SucceededCallbackArtNew);
                return;
            }
            return;
        }
        if (rreshti.txtKategoria == "Artikull") {
            vendosArt([index, null]);
        }
    }
}

function resetRreshtKorent(idRreshti) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    if (idRreshti == idRow && grida.getTekstQelize('txtKodi', idRow) != undefined) {
        grida.setTekstQelize('txtKodi', idRreshti, '');
        grida.setTekstQelize('txtIdKodi', idRreshti, '');
        grida.setTekstQelize('txtIdTrupi', idRreshti, '0');
        grida.setTekstQelize('txtEmertimi', idRreshti), '';
        grida.setTekstQelize('txtSasiGjendje', idRreshti);
        grida.setTekstQelize('txtSasiaRez', idRreshti);
        grida.setTekstQelize('txtSasiaPritje', idRreshti);
        grida.setTekstQelize('txtSasiaDisp', idRreshti);
        grida.setTekstQelize('txtSasia', idRreshti);

        $('#cmbNjesia' + idRreshti).replaceWith(myJQGrid.myElemCombo("", 'cmbNjesia', idRreshti, updateCmimiRowKorrent, undefined, true).children()[0]);
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
    if (result != null) {
        var idRreshti = result[0];
        var artikulli = result[1];
        var gjendjetot = result[2];
        var sasiRez = result[3];
        var sasiUB = result[4];

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
        if (artikulli.Klasa === 2 || artikulli.Klasa === 3) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukKryhenVeprimeMeArtikujTePastokueshem"));
            resetRreshtKorent(idRreshti);
            return;
        }
        //if ((artikulli.Klasa === 5 || artikulli.Klasa === 6) && Utils.getUrlVar("lloj") == 'hyrje') {
        //    myMesazh.ShtoMesazhGabimi(hfState.Get("NukKryhenVeprimeMeArtikujProdhimOseProdhimNeProces"));
        //    resetRreshtKorent(idRreshti);
        //    return;
        //}
        if (artikulli.Klasa === 4) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukMundTeKryeniVeprimeMeArtikujTePerbere"));
            resetRreshtKorent(idRreshti);
            return;
        }
        if (!artikulli.IRezervueshem) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukMundTaZgjidhniArtikullinPerRezervim"));
            resetRreshtKorent(idRreshti);
            return;
        }
        if (idRreshti == idRow && grida.getTekstQelize('txtKodi', idRow) != undefined) {

            if ($(idKodi).val() == artikulli.IdArtikulli) {
                grida.setTekstQelize('txtKodi', idRreshti, artikulli.KodArtikulli);
                return; //eshte i njejti artikull 
            }
            hfArt.Set(artikulli.IdArtikulli, artikulli);
            kontrolloRow(idRreshti);
            if (urritsasia) return;
            var mag = btneMagazina.GetText();
            if (((artikulli.IdMagazina != 0) && (mag == ''))) //magazina 
                $('#txtMagazina' + idRreshti).val(artikulli.IdMagazina);

            grida.setTekstQelize('txtKodi', idRreshti, artikulli.KodArtikulli);
            myJQGrid.closeAutocomplete('txtKodi', idRreshti);
            grida.setTekstQelize('txtIdKodi', idRreshti, artikulli.IdArtikulli);

            if (pershk == 1) {
                grida.setTekstQelize('txtEmertimi', idRreshti, artikulli.PershkrimArtikulli)
                $(emerArt).attr('title', artikulli.PershkrimArtikulli);
            }
            else
                if (artikulli.PershkrimiAngArtikulli !== "")
                    grida.setTekstQelize('txtEmertimi', idRreshti, artikulli.PershkrimiAngArtikulli)
                else
                    grida.setTekstQelize('txtEmertimi', idRreshti, artikulli.PershkrimArtikulli)



            var comboNjesia = $('#' + arrayIdKolonaGrides[4] + idRreshti);
            var arrayOptions = grida.formoArrayOptinosNjesia(artikulli);
            if (njesiDef == 1) {
                comboNjesia.replaceWith(myelemComboNjesiaSup(artikulli.KodNjesia1, null, idRreshti).children()[0]);
            }
            else {
                comboNjesia.replaceWith(myelemComboNjesiaSup(artikulli.KodNjesia2, null, idRreshti).children()[0]);
                gjendjetot = gjendjetot * artikulli.KoeficientArtikulli;
                sasiRez = sasiRez * artikulli.KoeficientArtikulli;
                sasiUB = sasiUB * artikulli.KoeficientArtikulli;
            }
            var sasiDisp = gjendjetot - sasiRez;
            grida.setTekstQelize('txtSasiGjendje', idRreshti, gjendjetot);
            grida.setTekstQelize('txtSasiaRez', idRreshti, sasiRez);
            grida.setTekstQelize('txtSasiaDisp', idRreshti, sasiDisp);
            grida.setTekstQelize('txtSasiaPritje', idRreshti, sasiUB);
            changedSasia();
            return;
        }


        if (grida.getTekstQelize('txtIdKodi', idRreshti) == artikulli.IdArtikulli)
            return; //eshte i njejti artikull 
        hfArt.Set(artikulli.IdArtikulli, artikulli);
        grida.setTekstQelize('txtKodi', idRreshti, artikulli.KodArtikulli);


        kontrolloRow(idRreshti);
        if (urritsasia) return;
        grida.setTekstQelize('txtIdKodi', idRreshti, artikulli.IdArtikulli);

        if (artikulli.IdMagazina != 0) //magazina
            grida.setTekstQelize('txtMagazina', idRreshti, artikulli.Magazina);
        if (pershk == 1)
            grida.setTekstQelize('txtEmertimi', idRreshti, artikulli.PershkrimArtikulli);
        else
            if (artikulli.PershkrimiAngArtikulli !== "")
                grida.setTekstQelize('txtEmertimi', idRreshti, artikulli.PershkrimiAngArtikulli);
            else grida.setTekstQelize('txtEmertimi', idRreshti, artikulli.PershkrimArtikulli);
        if (njesiDef == 1)
            grida.setTekstQelize('txtNjesia', idRreshti, artikulli.KodNjesia1);
        else {
            grida.setTekstQelize('txtNjesia', idRreshti, artikulli.KodNjesia2);
            gjendjetot = gjendjetot * artikulli.KoeficientArtikulli;
            sasiRez = sasiRez * artikulli.KoeficientArtikulli;
            sasiUB = sasiUB * artikulli.KoeficientArtikulli;
        }
        var sasiDisp = gjendjetot - sasiRez;
        grida.setTekstQelize('txtSasiGjendje', idRreshti, gjendjetot);
        grida.setTekstQelize('txtSasiaRez', idRreshti, sasiRez);
        grida.setTekstQelize('txtSasiaDisp', idRreshti, sasiDisp);
        grida.setTekstQelize('txtSasiaPritje', idRreshti, sasiUB);


        return;
    }
}

function formGridColsArray(isLidhur) {
    var hfGridKod = $('#hfGridaKodi');
    var hfGridDetajim = $('#hfGridaDetajimi');
    var IdKonfigAmbjenteLupat = [{ hfVar: hfGridKod, kodiText: "txtKodi" }, { hfVar: hfGridDetajim, kodiText: "txtDetajimi" }];
    myJQGrid.formArrayKolGrides($("input[id$='HfGridCol']"), arrayIdKolonaGrides, arrayPershkrimiKolonaGrides, arrayVisibleKolonaGrides, arrayReadOnlyKolonaGrides, isLidhur, arrayWidthKolonaGrides, IdKonfigAmbjenteLupat, arrayRenditjeKolonaGrides);
}

function myelemIdKodi(value, options) {//po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemIdKodi(value, options, arrayReadOnlyKolonaGrides[11], idRresht, arrayIdKolonaGrides[11]);
}

function myelemIdTrupi(value, options) {//po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemIdKodi(value, options, arrayReadOnlyKolonaGrides[12], idRresht, arrayIdKolonaGrides[12]);
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

    if (arrayReadOnlyKolonaGrides[0] == 'True') {
        disabled = true;
    }
    return myJQGrid.myElemCombo(value, arrayIdKolonaGrides[0], idRresht, change, arrayOptions, disabled);
}

function change() {//po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    resetRreshtKorent(idRresht);
    enable();
}
/*
Function: myElemKodi

Nderton nje textbox dhe nje buton per te zgjedhur artikullin ose makron sipas llojit te zgjedhur tek combo Kategoria
*/
function myElemKodi(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[1];
    return myJQGrid.myElemKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[1], ButtonClickKodi, keyPressKodi, changeFunc, fokusi, lostFocusKoloneFundit);
}

function keyPressKodi(event) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    try {
        var vlera = event.target.value;  //$('#txtKodi' + lastsel2).val();
        if (vlera == "")
            $('#txtKodi' + idRresht).autocomplete("close");
        else
            callWebserviceKodi(vlera);
    } catch (e) { }
}

function dateGotFocus(s, e) {
    window.setTimeout(function () { s.SetCaretPosition(0); }, 0);
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
    //  callWebServiceInfoArt();
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
    return myJQGrid.myElemEmertimi(value, arrayReadOnlyKolonaGrides[2], idRresht, arrayIdKolonaGrides[2]); // 'txtEmertimi'
}

function myElemSasiGjendje(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, arrayReadOnlyKolonaGrides[5], idRresht, arrayIdKolonaGrides[5], changeds, hfFormatNumri, changeds); //txtSasiGjendje
}

function myElemSasiRez(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, arrayReadOnlyKolonaGrides[6], idRresht, arrayIdKolonaGrides[6], changeds, hfFormatNumri, changeds); //txtSasiaRez
}

function myElemSasiPritje(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, arrayReadOnlyKolonaGrides[7], idRresht, arrayIdKolonaGrides[7], changeds, hfFormatNumri, changeds); //txtSasiaPritje
}

function myElemSasiDisponueshme(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, arrayReadOnlyKolonaGrides[8], idRresht, arrayIdKolonaGrides[8], changeds, hfFormatNumri, changeds); //txtSasiaDisp
}

function myElemSasia(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, arrayReadOnlyKolonaGrides[9], idRresht, arrayIdKolonaGrides[9], changedSasia, hfFormatNumri, focusoutSasia); //txtSasia  
}

function myElemSasiMbetur(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, arrayReadOnlyKolonaGrides[10], idRresht, arrayIdKolonaGrides[10], changeds, hfFormatNumri, changeds); //txtSasiaMbetur
}

var pyeturDoni = 0;
var urritsasia = false;
function kontrolloRow(rowid) {//po
    var grida = $("#rowed5");
    var idRresht = grida.getLastSel2();
    var lloji = grida.getTekstQelize('txtKategoria', rowid);
    var detajim = '';
    var name = grida.getTekstQelize('txtKodi', rowid);
    urritsasia = false;
    if ((konfirmimArt == 1 && pyeturDoni == 0) || shtosasiaporreshtiRi == false) {
        var gridIds = grida.jqGrid('getDataIDs');
        for (i = 0; i < gridIds.length; i++) {
            if (rowid == gridIds[i])
                continue;
            var editorkodi = grida.getTekstQelize('txtKodi', gridIds[i]);
            var editordetajimi = '';
            var editorlloji = grida.getTekstQelize('txtKategoria', gridIds[i]);
            if (editorkodi == name && editorkodi != "" && editorlloji == lloji) {
                if (shtosasiaporreshtiRi == false) {
                    var sasia = grida.getTekstQelize('txtSasia', gridIds[i]);
                    grida.setTekstQelize('txtSasia', gridIds[i], parseFloat(sasia) + 1);

                    var editorSasiDisp = grida.getTekstQelize('txtSasiaDisp', gridIds[i]);
                    grida.setTekstQelize('txtSasiaMbetur', gridIds[i], parseFloat(editorSasiDisp - (sasia + 1)));
                    resetRreshtKorent(rowid);
                    urritsasia = true;
                    if (fokusi == 1) {
                        grida.jqGrid('saveRow', rowid, null, 'clientArray');

                        grida.jqGrid('setSelection', gridIds[i], true);
                        idRresht = gridIds[i];

                        grida.jqGrid('editRow', idRresht, false);
                        $('#txtSasia' + idRresht).focus();
                    }

                    //  alert(fokusi);
                    break;
                }
                if (konfirmimArt == 1) {
                    myMesazh.vendosClient();
                    identifikuesPyetje = "artikulli";
                    rreshtidyfish = rowid;
                    myMesazh.ShtoPyetje(hfState.Get("msgEkzistonArtikullNeGride"), true);
                    pyeturDoni = 1;
                    break;
                }
            }
        }
    }
}

/*
Function: fshiClicked

Fshin nje rresht te grides

Parameters:

index - Id e rreshtit qe do fshihet    
*/
function fshiClicked(index) {
    myJQGrid.fshiClicked(index, "#rowed5", inicializoGride);
}
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
    if (lloji === "Artikull" && hfArt.Contains(idArt)) {
        var artikulli = hfArt.Get(idArt);
        arrayOptions = grida.formoArrayOptinosNjesia(artikulli);
        if (value === '')
            value = arrayOptions[0].text;
        if (idRreshti == idRow)
            return myJQGrid.myElemCombo(value, 'txtNjesia', idRreshti, updateCmimiRowKorrent, arrayOptions, arrayReadOnlyKolonaGrides[7]);
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
    if (lloji === "Artikull" && kodi !== "") {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheNjesiArtikulliComboMeKodRow"), data: JSON.stringify({ kodi: kodi, index: idRreshti, textNjesiZgjedhur: value, idNdermarrje: hfState.Get("idNdermarrje") })
        }).done(SucceededCallbackComboNjesiArtikulli);
    }
    return myJQGrid.myElemCombo(value, 'txtNjesia', idRreshti, updateCmimiRowKorrent, arrayOptions, true);

}
/*
Function: myelemComboMagazina

Nderton nje combobox per te zgjedhur magazinen.
*/
function myelemComboMagazina(value) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    magazina1 = btneMagazina.GetText();
    if (value == "" && magazina1 != "" && magazina1 != undefined)
        value = magazina1;
    var arrayOptions;
    if (colMagazina !== "") {
        arrayOptions = new Array(colMagazina.length + 1);
        var objBosh = new Object();
        objBosh.value = 0;
        objBosh.text = "Pa Magazine";
        arrayOptions[0] = objBosh;
        for (var i = 1; i < arrayOptions.length; i++) {
            var objNjesi = new Object();
            objNjesi.value = colMagazina[i - 1].IdNjesiAdministrative;
            objNjesi.text = colMagazina[i - 1].Kodi;
            objNjesi.desc = colMagazina[i - 1].Pershkrimi;
            arrayOptions[i] = objNjesi;
        }
    }
    var disabled = false;
    if (arrayReadOnlyKolonaGrides[3] == 'True')
        disabled = true;

    return myJQGrid.myElemCombo(value, arrayIdKolonaGrides[3], idRresht, changeMag, arrayOptions, disabled);
}


function changeMag() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    merrSasite(idRresht);
}
function SucceededCallbackComboNjesiArtikulli(comboListNjesiArt) {
    var grida = $("#rowed5");
    var idRow = grida.getLastSel2();
    var idreshti = comboListNjesiArt[0];
    var textzgjedhur = comboListNjesiArt[1];
    comboListNjesiArt = comboListNjesiArt[2];
    if (idreshti == idRow && grida.getTekstQelize('txtKodi', idRow) != undefined) {
        if (arrayReadOnlyKolonaGrides[4] == 'True')
            $('#' + arrayIdKolonaGrides[4] + idRow).replaceWith(myJQGrid.myElemCombo(textzgjedhur, arrayIdKolonaGrides[4], idRow, updateCmimiRowKorrent, comboListNjesiArt, true).children()[0]);
        else
            $('#' + arrayIdKolonaGrides[4] + idRow).replaceWith(myJQGrid.myElemCombo(textzgjedhur, arrayIdKolonaGrides[4], idRow, updateCmimiRowKorrent, comboListNjesiArt, false).children()[0]);
    }
    else {
        grida.setTekstQelize('txtNjesia', idRreshti, textzgjedhur);
    }
}

function JoClick(s, e) {
    var grida = $('#rowed5');
    switch (identifikuesPyetje) {
        case "artikulli":
            resetRreshtKorent(rreshtidyfish);
            break;


        default:
            alert('Pyetje e panjohur');
            break;
    }

}

var queryString;
function PoClick(s, e) {

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
    {
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
        //myJQGrid.keyPressKodi("#rowed5", window.lastsel2, arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1]);
        return;
    }
    if ($('#hfShtimModifikim').val() == "modifikim") {
        var colTrupMag = JSON.parse($('#HfColTrupMag').val());
        var colArt = JSON.parse($('#HfColArt').val());
        var colNjesAdminis = JSON.parse($('#HfColNjesAdminis').val());
        var colNjesiArt = JSON.parse($('#HfColNjesiArt').val());
        //        var colTvshArt = JSON.parse($('#HfColTvshArt').val());
        var konfAmb = JSON.parse($('#HfKonfAmb').val());
        grida.setLastSel2(1);
        idRresht = 1;
        grida.jqGrid('clearGridData');
        var kategoria, kodi, emertimi, magazina, njesia, sasia, idkodi, idtrupi;


        for (var i = 0; i < colArt.length; i++) {

            if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides - 1] == 'True' || isLidhur)
                be = myJQGrid.myValueButtonFshi(true, idRresht, "#rowed5");
            else
                be = myJQGrid.myValueButtonFshi(false, idRresht, "#rowed5");

            kategoria = "Artikull";

            kodi = colArt[i].KodArtikulli;
            idkodi = colArt[i].IdArtikulli;
            if (pershk == 1)
                emertimi = colArt[i].PershkrimArtikulli;
            else
                emertimi = colArt[i].PershkrimAngArtikulli;
            var tmpArtikulli = colArt[i];
            //        tmpArtikulli.listeTvsh = colTvshArt[i]; //todo kelvin
            hfArt.Set(colArt[i].IdArtikulli, tmpArtikulli);
            if (colNjesAdminis[i].Kodi == null) magazina = "Pa Magazine";
            else magazina = colNjesAdminis[i].Kodi;

            njesia = colNjesiArt[i].KodNjesia;
            sasia = colTrupMag[i].Sasia;
            idtrupi = colTrupMag[i].IdTrupiRezervime;

            var datarow = {
                txtKategoria: kategoria, txtKodi: kodi, txtEmertimi: emertimi, txtMagazina: magazina,
                txtNjesia: njesia, txtIdKodi: idkodi, txtIdTrupi: idtrupi, txtFshi: be
            };
            var su = grida.jqGrid('addRowData', parseInt(idRresht), datarow);
            grida.setTekstQelize('txtSasia', idRresht, sasia);

            idRresht = idRresht + 1;
            grida.setLastSel2(idRresht);
        }
    }
}


var arrKategoria = new Array();
var arrKodi = new Array();
var arrEmertimi = new Array();
var arrMagazina = new Array();
var arrNjesia = new Array();
var arrSasia = new Array();
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
var identifikuesPerPopupMakro;
var identifikuesPerPopupMagazina;
function changeName() {
    myFaqeCelje.shtoHandlerSession();
    try {
        if (Utils.getUrlVar('id') == undefined)
            window.parent.callWebServiceKtheInfoLart('Shto_RegjistrimRezervimi.aspx?lloj=' + Utils.getUrlVar('lloj'), 0);
        else
            window.parent.callWebServiceKtheInfoLart('Shto_RegjistrimRezervimi.aspx?lloj=' + Utils.getUrlVar('lloj'), Utils.getUrlVar('id'));

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
        veprimi: 'RegjistrimRezervimi',
        niveli:niveli,
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
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
            data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
        }).done(SucceededCallbackKonfig);
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
    }
}

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}

var kushtet; var colKushte; var colAlterKusht;
var fokusi = 0; var info = false;
var colGrida;
var shtosasiaporreshtiRi = true;
function SucceededCallbackKonfig(result) {
    var hf = $("input[id$='hfShtimModifikim']");
    var hfLidhur = $("input[id$='hfLidhur']");
    var arrTabela = ['tblFillim', "tblFund"];
    var arrPrind = ["dvFillim", "dvFundi"];
    var colKontrollet = result.colKontrollet;
    var colAtrTrupi = result.colAtrTrupi;
    var grida = $('#rowed5');

    var vlerat = myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, undefined, hf, '', arrTabela, undefined, undefined, hfLidhur, arrPrind);
    $("#divgride1").show();//$("#divgride1")[0].style.visibility = 'visible';
    $("#dvFillim").show();//$("#dvFillim")[0].style.visibility = 'visible';
    //$("#dvFillim")[0].style.display = '';
    $("#dvFundi").show();//$("#dvFundi")[0].style.visibility = 'visible';
    //$("#dvFundi")[0].style.display = '';
    cmbKonfigurimi.ShowDropDown();
    cmbKonfigurimi.HideDropDown();
    vendosDateDefault();
    callWebserviceKF(0);
    colGrida = result.colGrida;
    colKushte = result.colKushte;
    colAlterKusht = result.colAlterKusht;
    var kodniveli = result.kodniveli;
    formatNumriZgjedhur = result.formatNumriZgjedhur;
    $('#HfGridCol').val(JSON.stringify(colGrida));

    var hfKl = $("#hfLupaKlientFurnitor")[0];
    var hfMag = $("#hfLupaMagazina")[0];
    $('#hfKontrolletNrAutom').val(JSON.stringify(colKontrollet));
    $('#hfAtributeNrAutom').val(JSON.stringify(colAtrTrupi));
    if ($("input[id$='hfShtimModifikim']").val() == "shtim") {
        hfNrAuto.Clear();
        hfNrAutoShitje.Clear();
        vendosNrAutomatik(colAtrTrupi, colKontrollet);
    }

    for (var i = 0; i < colKontrollet.length - 1; i++) {
        //id e konfigurimit te lupes vendoset ne hidden field
        //vlera e saj do t'i kalohet si query string hapjes se popUp-it
        if (colAtrTrupi[i].KodKontrolli == "btneKlientFurnitori")
            hfKl.value = colKontrollet[i].IdKonfigAmbjenteLupa.toString(); //merret id e konfigurimit te lupes per lupen e klient furnitorit ne forme
        else
            if (colAtrTrupi[i].KodKontrolli == "btneMagazina")
                hfMag.value = colKontrollet[i].IdKonfigAmbjenteLupa.toString(); //merret id e konfigurimit te lupes per lupen e magazines ne forme            
    }

    pershk = 1;
    konfirmimArt = 2;
    njesiDef = 1;
    cmimzero = 1;
    regjistrimKF = 1;
    kodkodbar = 1; shtosasiaporreshtiRi = true;
    var modinfo = 0;
    for (j = 0; j < colKushte.length; j++) {
        switch (colKushte[j].Kodi) {
            case 'LPNJAG':
                if (colAlterKusht[j].Alternativa == 'Po')
                    konfirmimArt = 1;
                else
                    konfirmimArt = 2;
                break;
            case 'P':
                if (colAlterKusht[j].Alternativa == 'Pershkrimi 1')
                    pershk = 1;
                else
                    pershk = 2;
                break;
            case 'ZADHG':
                if (colAlterKusht[j].Alternativa == 'Shto ne rresht te ri')
                    shtosasiaporreshtiRi = true;
                else shtosasiaporreshtiRi = false;
                break;
            case 'NJ':
                if (colAlterKusht[j].Alternativa == 'Njesia 1')
                    njesiDef = 1;
                else
                    njesiDef = 2;
                break;
            case 'IA':
                if (colAlterKusht[j].Alternativa == 'Kod')
                    kodkodbar = 1;
                else
                    kodkodbar = 2;
                break;
            case 'F':
                if (colAlterKusht[j].Alternativa == 'Sasia')
                    fokusi = 1;
                else
                    if (colAlterKusht[j].Alternativa == 'Rreshti tjeter')
                        fokusi = 2;
                    else
                        fokusi = 0;
                break;
            default:
                break;
        }
    }

    grida.jqGrid('GridUnload', "rowed5");;
    var isLidhur = ($("input[id$='hfLidhur']").val().toLowerCase() === 'true' || cmbStatusi.GetSelectedIndex() == 1 || cmbStatusi.GetSelectedIndex() == 2);
    formGridColsArray(isLidhur);
    grida.setLastSel2(-1);
    grida = $('#rowed5');
    ruajFormatetNeGride(grida, formatNumriZgjedhur);
    inicializoGride(isLidhur);
    mbushGrideNgaHiddenFieldet(isLidhur);
    Utils.konfiguroAccorditionPasKonfigDokumenti(varKonfig.identifikuesPerLocalStorageKey);
    if (cmbStatusi.GetSelectedIndex() == 1 || cmbStatusi.GetSelectedIndex() == 2)
        cmbStatusi.SetEnabled(false);
}

function DateChanged(s, e) {
    var hfKontrollet = JSON.parse($('#hfKontrolletNrAutom').val());
    var atributet = JSON.parse($('#hfAtributeNrAutom').val());
    vendosNrAutomatik(atributet, hfKontrollet);
}

var pershk = 1;
var konfirmimArt = 2;
var njesiDef = 1;
var regjistrimKF = 1;
//var gjendjeartminmax = 1; //1- per detajimin e pare, informon nese artikulli kalon min max e vendosur
var cmimzero = 1;
/*
Function: ndryshoKonfigurimin
    
Therret funksionin <callWebserviceKonfigurimi> per te vendosur nje konfigurim te ri.
*/
function ndryshoKonfigurimin() {

    var lloji = Utils.getUrlVar('lloj');
    var pershkKonfigAmb = cmbKonfigurimi.GetSelectedItem().GetColumnText("PershkrimKonfigAmbjente");
    if (pershkKonfigAmb != undefined)
       // lblKonfigurimi.SetText(pershkKonfigAmb)
        $('#kokeKonfigurimi').text(hfState.Get("MenuKokeDokumenti") + ': ' + pershkKonfigAmb);
    if (lloji == 'hyrje') callWebserviceKonfigurimi(539, cmbKonfigurimi.GetText());
    else callWebserviceKonfigurimi(540, cmbKonfigurimi.GetText());
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
    popupUniversal.SetHeaderText(hfState.Get("headerZgjidhKlientFurnitorin"));
    if (klientfurnitor == "Klient")
        popupUniversal.SetContentUrl('LupaKlientFurnitor.aspx?veprimi=1&idKonfigAmbjente=' + queryStr);
    else if (klientfurnitor == "Furnitor")
        popupUniversal.SetContentUrl('LupaKlientFurnitor.aspx?veprimi=2&idKonfigAmbjente=' + queryStr);
    else popupUniversal.SetContentUrl('LupaKlientFurnitor.aspx?idKonfigAmbjente=' + queryStr);
    popupUniversal.SetSize(widthLupaKF, heightLupaKF);
    popupUniversal.Show();
}

/*
Function: TextChangedKlientFurnitori
    
Therret funksionin <callWebserviceKF> per te marre vlerat e klientit/furnitorit te zgjedhur
*/
function TextChangedKlientFurnitori() {
    if (btneKlientFurnitori.GetText() != "" && regjistrimKF != 3)
        callWebserviceKF(btneKlientFurnitori.GetValue());
}
//Therret funksionin <callWebserviceKF> per te marre vlerat e klientit/furnitorit te zgjedhur
//Nese nuk eshte zgjedhur nonje klient/furnitor ekzistues fshin textin dhe venod fokusin te kontroli
function KlientFurnitoriChanged() {
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
function SucceededCallbackFormatNumri(result) {
    var formatNumri = result;
    var grida = $('#rowed5');
    ndryshoKonfigFormatNumri(grida, formatNumri);
    vendosKonfigFormatNumri();
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
/*
Function: TextChangedMagazina
    
Vendos ne gride magazinen qe zgjidhet te koka
*/
function TextChangedMagazina() {
    //vendoset magazina e zgjedhur te koka ne rreshtin qe po editohet
    var reshtiieditueshem = false;
    magazina1 = btneMagazina.GetText();
    var grida = $("#rowed5");
    var idRresht = grida.getLastSel2();
    var magazinagrides = $('#txtMagazina' + idRresht)[0];
    if (magazinagrides != undefined) {
        for (var i = 0; i < magazinagrides.length; i++) {
            if (magazinagrides[i].text == magazina1)
                magazinagrides.selectedIndex = i;
        }
        reshtiieditueshem = true;
    }

    //vendoset magazina e zgjedhur te koka ne rreshtat e tjere te grides       
    var rreshtaTeGrides = grida.jqGrid('getDataIDs');
    for (i = 0; i < rreshtaTeGrides.length; i++) {
        if (!reshtiieditueshem)
            grida.setTekstQelize('txtMagazina', rreshtaTeGrides[i], magazina1);
        else if (rreshtaTeGrides[i] != idRresht) {
            grida.setTekstQelize('txtMagazina', rreshtaTeGrides[i], magazina1);
        } merrSasite(rreshtaTeGrides[i]);
    }

    if (magazina1 != "") {
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
        callWebserviceNiveliNew(cmbLloji.GetValue(), 'rezervime', mod);
    else
        callWebserviceNiveliNew('', 'rezervime', mod);
}

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

var panjesi = false;
/*
Function: merrTeDhena
    
Merr te dhenat qe ka grida dhe i vendos neper hidden field-e per ti perdorur ne server side
*/
function merrTeDhena(e) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    grida.jqGrid('saveRow', idRresht, false, 'clientArray');

    trupiBosh = true; var kamag = true;
    panjesi = false;

    var idTe = grida.jqGrid('getDataIDs');
    for (var i = 0; i < idTe.length; i++) {
        editorKategoria = grida.getTekstQelize('txtKategoria', idTe[i]);
        editorKodi = grida.getTekstQelize('txtKodi', idTe[i]);
        editorEmertimi = grida.getTekstQelize('txtEmertimi', idTe[i]);
        editorMagazina = grida.getTekstQelize('txtMagazina', idTe[i]);
        editorNjesia = grida.getTekstQelize('txtNjesia', idTe[i]);
        editorSasia = grida.getTekstQelize('txtSasia', idTe[i]);

        if (editorKodi != "") { trupiBosh = false; }

        if (btneMagazina.GetEnabled() && editorMagazina == "" && editorKodi != '') {
            kamag = false;
            e.processOnServer = false;
        }
        if (editorKodi != "" && editorSasia == 0) {
            e.processOnServer = false; click = false;
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgKaRreshtaMeSasiZero"));
        }
        if (editorKodi != "" && editorNjesia == "")
            panjesi = true;

    }
    var rreshtaTeGrides = grida.getTeDhenaRreshti();
    $('#gridDataObject').val(JSON.stringify(rreshtaTeGrides));
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
    arrKategoria = new Array();
    arrKodi = new Array();
    arrEmertimi = new Array();
    arrDetajimet = new Array();
    arrMagazina = new Array();
    arrNjesia = new Array();
    arrSasia = new Array();
    arrCmimi = new Array();
    arrVlefta = new Array();
    // arrMagazina2 = new Array();
    resetCountera();
    var hidField1 = document.getElementById("hfKategoria");
    var hidField2 = document.getElementById("hfKodi");
    var hidField3 = document.getElementById("hfEmertimi");
    var hidField4 = document.getElementById("hfMagazina");
    var hidField5 = document.getElementById("hfNjesia");
    var hidField6 = document.getElementById("hfSasia");
    var hidField7 = document.getElementById("hfPrioriteti");
    hidField1.value = "";
    hidField2.value = "";
    hidField3.value = "";
    hidField4.value = "";
    hidField5.value = "";
    hidField6.value = "";
    hidField7.value = "";
    hfArt.Clear();
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

}
/*
Function: ButtonClickKodi
    
Hap lupen e artikujve apo makrove sipas zgjedhjes qe eshte bere te kategoria.
*/
function ButtonClickKodi() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var hfKod = document.getElementById("hfGridaKodi");
    var queryStr = hfKod.value;

    var kategoria = grida.getTekstQelize('txtKategoria', idRresht);
    switch (kategoria) {
        case "":
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniLlojinEVeprimit"));
            break;
        case "Artikull":
            popupUniversal.SetHeaderText(hfState.Get("roundPanelZgjidhArtikullin"));
            var grup = '';
            popupUniversal.SetContentUrl('LupaArtikull.aspx?idKonfigAmbjente=' + queryStr);
            popupUniversal.SetSize(widthLupaArtikull, heightLupaArtikull);
            popupUniversal.Show();

            break;
        case "Makro":
            popupUniversal.SetHeaderText(hfState.Get("msgZgjidhMakro"));
            popupUniversal.SetContentUrl("LupaMakro.aspx?idKonfigAmbjente=" + queryStr);
            popupUniversal.SetSize(widthLupaMakro, heightLupaMakro);
            popupUniversal.Show();
        default: myMesazh.ShtoMesazhGabimi(hfState.Get("msgLlojiVeprimitIPanjohur"));
            break;
    }

}

/*
Function: vendosVleftat
    
Merr dhe validon vlerat e sasise dhe cmimit te caktuara ne gride dhe therret funksionin <vendosTotalet>
*/
function vendosVleftat(s) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var sasia = grida.getTekstQelize('txtSasia', idRresht);

    var editorKodi = grida.getTekstQelize('txtKodi', idRresht);
    var detajim = -1;

    if (isNaN(sasia)) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiaDuhetNumer"));
        sasia = grida.getVlereDefault('txtSasia');
        grida.setTekstQelize('txtSasia', idRresht, sasia);
        $('#txtSasia' + idRresht).focus();
    }
    if (sasia == "0") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiaNukMundTeJeteZero"));
        sasia = grida.getVlereDefault('txtSasia');
        grida.setTekstQelize('txtSasia', idRresht, sasia);
        $('#txtSasia' + idRresht).focus();
    }


}

function kontrolloVlereBosh() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var sasia = grida.getTekstQelize('txtSasia', idRresht);
    if (isNaN(parseFloat(sasia))) {

        myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiaDuhetNumer"));
        sasia = grida.getVlereDefault('txtSasia');
        grida.setTekstQelize('txtSasia', idRresht, sasia);
        $('#txtSasia' + idRresht).focus();
    }
}

function changeds() { }

function changedSasia() {//po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    editorLloji = grida.getTekstQelize(arrayIdKolonaGrides[0], idRresht);
    editorKodi = grida.getTekstQelize('txtKodi', idRresht);
    editorSasia = grida.getTekstQelize('txtSasia', idRresht);
    editorSasiaMbetur = grida.getTekstQelize('txtSasiaMbetur', idRresht);
    editorSasiDisp = grida.getTekstQelize('txtSasiaDisp', idRresht);
    var sasia;

    if (editorSasia == '.') {
        grida.setTekstQelize('txtSasia', idRresht, '0.');
        return;
    }
    if (editorSasia == "" || isNaN(editorSasia)) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiaNumer"));
        sasia = grida.getVlereDefault('txtSasia');
        //grida.setTekstQelize('txtSasia', lastsel2, sasia);
        $('#txtSasia' + idRresht).focus();
    }
    else
        if (editorSasia == "0") {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiaNukMundTeJeteZero"));
            sasia = grida.getVlereDefault('txtSasia');
            //grida.setTekstQelize('txtSasia', lastsel2, sasia);
            $('#txtSasia' + idRresht).focus();
        }

    if (sasia == null)
        sasia = parseFloat(editorSasia);
    grida.setTekstQelize('txtSasiaMbetur', idRresht, parseFloat(editorSasiDisp - sasia));
}

function focusoutSasia() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var sasia = grida.getTekstQelize('txtSasia', idRresht);
    if (sasia === '' || isNaN(sasia) || sasia === 0 || sasia === '0')
        grida.setTekstQelize('txtSasia', idRresht);
}

var textNjesiZgjedhur;

function updateCmimiRowKorrent() {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    var njesi = $('#' + arrayIdKolonaGrides[4] + idRow).val();
    var idArt = grida.getTekstQelize(arrayIdKolonaGrides[11], idRow);
    var artikulli = hfArt.Get(idArt);

    var koef = $("#" + arrayIdKolonaGrides[4] + idRow + " option:selected").data("koeficienti");
    if (artikulli.Njesi1Artikulli == njesi) {
        grida.setTekstQelize(arrayIdKolonaGrides[5], idRow, grida.getTekstQelize(arrayIdKolonaGrides[5], idRow) * artikulli.KoeficientArtikulli);
        grida.setTekstQelize(arrayIdKolonaGrides[6], idRow, grida.getTekstQelize(arrayIdKolonaGrides[6], idRow) * artikulli.KoeficientArtikulli);
        grida.setTekstQelize(arrayIdKolonaGrides[7], idRow, grida.getTekstQelize(arrayIdKolonaGrides[7], idRow) * artikulli.KoeficientArtikulli);
        grida.setTekstQelize(arrayIdKolonaGrides[8], idRow, grida.getTekstQelize(arrayIdKolonaGrides[8], idRow) * artikulli.KoeficientArtikulli);
        grida.setTekstQelize(arrayIdKolonaGrides[9], idRow, grida.getTekstQelize(arrayIdKolonaGrides[9], idRow) * artikulli.KoeficientArtikulli);
        grida.setTekstQelize(arrayIdKolonaGrides[10], idRow, grida.getTekstQelize(arrayIdKolonaGrides[10], idRow) * artikulli.KoeficientArtikulli);
    }
    else {
        grida.setTekstQelize(arrayIdKolonaGrides[5], idRow, grida.getTekstQelize(arrayIdKolonaGrides[5], idRow) / artikulli.KoeficientArtikulli);
        grida.setTekstQelize(arrayIdKolonaGrides[6], idRow, grida.getTekstQelize(arrayIdKolonaGrides[6], idRow) / artikulli.KoeficientArtikulli);
        grida.setTekstQelize(arrayIdKolonaGrides[7], idRow, grida.getTekstQelize(arrayIdKolonaGrides[7], idRow) / artikulli.KoeficientArtikulli);
        grida.setTekstQelize(arrayIdKolonaGrides[8], idRow, grida.getTekstQelize(arrayIdKolonaGrides[8], idRow) / artikulli.KoeficientArtikulli);
        grida.setTekstQelize(arrayIdKolonaGrides[9], idRow, grida.getTekstQelize(arrayIdKolonaGrides[9], idRow) / artikulli.KoeficientArtikulli);
        grida.setTekstQelize(arrayIdKolonaGrides[10], idRow, grida.getTekstQelize(arrayIdKolonaGrides[10], idRow) / artikulli.KoeficientArtikulli);


    }
}


function merrSasite(index) {
    var grida = $('#rowed5');
    var detajim = -1;
    var idKod = 'txtKodi';
    var magazine = 0;
    var myDate = dteDtDok.GetDate();
    myDate.setDate(myDate.getDate() + 365 * 2);


    // magazine = grida.getTekstQelize('txtMagazina', index);
    magazine = $('#txtMagazina' + index).val();
    var kategoria = grida.getTekstQelize('txtKategoria', index);
    var emerfushe = grida.getTekstQelize('txtKodi', index);
    var idNdermarrje = hfState.Get("idNdermarrje");
    var idPerdorues = hfState.Get("idPerdoruesi");
    if (typeof (emerfushe) != "undefined" && emerfushe != undefined && emerfushe != "") {
        if (magazine != undefined && magazine != "undefined" && magazine != "") {
            if (kategoria == "Artikull") { //Artikull  
                $.ajax({
                    pritPergjigje: true,
                    url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraArtRezMeKodOseKodBar"),
                    data: JSON.stringify({ kodi: emerfushe, rreshti: index, data: myDate, iddetajim: detajim, idmag: magazine, idNdermarrje: idNdermarrje, idPerdoruesi: idPerdorues })
                }).done(SucceededCallbackSasi);
                return;
            }
        }
    }
}


function SucceededCallbackSasi(result) {
    vendosSasite(result);
}

function vendosSasite(result) {
    var grida = $('#rowed5');
    if (result != null) {
        var idRreshti = result[0];
        var artikulli = result[1];
        var gjendjetot = result[2]; //tocheck gerta
        var sasiRez = result[3];
        var sasiUB = result[4];

        var kodi = grida.getTekstQelize('txtKodi', idRreshti);
        var idKodi = grida.getTekstQelize('txtIdKodi', idRreshti);


        kontrolloRow(idRreshti);
        if (njesiDef != 1) {
            gjendjetot = gjendjetot * artikulli.KoeficientArtikulli;
            sasiRez = sasiRez * artikulli.KoeficientArtikulli;
            sasiUB = sasiUB * artikulli.KoeficientArtikulli;
        }
        var sasiDisp = gjendjetot - sasiRez;
        grida.setTekstQelize('txtSasiGjendje', idRreshti, gjendjetot);
        grida.setTekstQelize('txtSasiaRez', idRreshti, sasiRez);
        grida.setTekstQelize('txtSasiaDisp', idRreshti, sasiDisp);
        grida.setTekstQelize('txtSasiaPritje', idRreshti, sasiUB);
        return;
    }
}


function enable() {// ben enable disable fushat sipas kategoriese

}

function vendosNrAutomatik(colAtrTrupi, colKontrollet) {
    myNrAuto.vendosNrAutomatik(colAtrTrupi, colKontrollet, dteDtDok.GetDate());
}

/*
Function: pastroFushatKokes
    
Pastron fushat pasi eshte bere ruajtja apo modifikimi per te bere gati ambjentin per shtim.
*/
function pastroFushatKokes() {

    btneKlientFurnitori.SetValue(null);
    btneMagazina.SetValue(null);
    if (btneMagazina.GetItemCount() == 1)
        btneMagazina.SetSelectedIndex(0);
    txtNrDok.SetText('');
    cmbDegeAdministrative.SetValue(null);
    txtShenime.SetText('');
    txtPrioriteti.SetText('');
    cmbStatusi.SetSelectedIndex(0);

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
    if(!Utils.nrWsRrugesManager.kanePerfunduarWs()) {
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
    }
    else if (dteDtDok.GetDate() == null) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniNjeDateDokumenti"));
        return false;
    }
    else if (dteDtRegjistrimi.GetDate() == null) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniNjeDateRegjstrimi"));
        return false;
    }
    else if (regjistrimKF != 1 && btneKlientFurnitori.GetValue() == null) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgJepniKF"));
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
    var hf = document.getElementById("status1");

    if (hf.value == "true") {

        myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimRezervimi.aspx?lloj=' + Utils.getUrlVar('lloj') + '&shtim_modifikim=shtim', true);

    } else click = false;
}
var shtoTimer;
function shtoTimedClick(e) {
    if (!Utils.nrWsRrugesManager.kanePerfunduarWs())
        shtoTimer = setTimeout(function () { shtoTimedClick(e) }, 500);
    else {
        clearTimeout(shtoTimer);
        myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimRezervimi.aspx?lloj=' + Utils.getUrlVar('lloj') + '&shtim_modifikim=shtim', true);
        e.processOnServer = false;
    }
}
/*
Function: menuClick

Percakton veprimin qe kryhet nese klikohet nje nga butonat e menuse.
*/
function menu_click(s, e) {
    if (e.item.name == 'Ruaj' || e.item.name == 'RuajPrint') {
        if ($("input[id$='hfAutorizimi']").val() == 'False') {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukKeniAutorizimPerTeRuajturKeteDok"));
            Utils.hiqLoadingGif();; click = false;
            e.processOnServer = false;
            return;
        }
        var valid = myFaqeCelje.validim(s, e);
        if (!valid) {
            Utils.hiqLoadingGif();;
            e.processOnServer = false;
            return;
        }
        if (isValidKoka()) {
            Utils.shfaqLoadingGif();;
            RuajClick(s, e);
        }
        else {
            e.processOnServer = false;
            // myMesazh.ShtoMesazhGabimi('Plotesoni te gjitha fushat');
        }
    }
    else if (e.item.name == 'Draft') {
        if ($("input[id$='hfAutorizimi']").val() == 'False') {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukKeniAutorizimPerTeRuajturKeteDok"));
            Utils.hiqLoadingGif();; click = false;
            e.processOnServer = false;
            return;
        }
        myFaqeCelje.validim(s, e);
        if (isValidKoka()) {
            Utils.shfaqLoadingGif();;
            RuajClick(s, e);
        }
        else {
            e.processOnServer = false;
            //myMesazh.ShtoMesazhGabimi('Plotesoni te gjitha fushat');
        }
    }
    else if (e.item.name == 'Kerko') {
        myFaqeCelje.kontrolloTeDrejta('RegjistrimRezervimi.aspx?lloj=' + Utils.getUrlVar('lloj'), null, true);
        e.processOnServer = false;
    }
    else if (e.item.name == 'Shto') {
        shtoTimer = setTimeout(function () { shtoTimedClick(e) }, 500);
    }
    else if (e.item.name == 'Fshi') {
        if ($("input[id$='hfAutorizimi']").val() == 'False') {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukKeniAutorizimPerTeFshireDok"));
            Utils.hiqLoadingGif();; click = false;
            e.processOnServer = false;
            return;
        }
        popFshi.Show(); e.processOnServer = false;
    }
    else if (e.item.name == 'Anullo') {
        myFaqeCelje.kontrolloTeDrejta('RegjistrimRezervimi.aspx?lloj=' + Utils.getUrlVar('lloj'));
        e.processOnServer = false;
        click = false;
    }
    if (e.item.name == 'Konverto') {
        if ($("input[id$='hfAutorizimi']").val() == 'False') {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukKeniAutorizimKonvertim"));
            Utils.hiqLoadingGif();; click = false;
            e.processOnServer = false;
            return;
        }
        ButtonClickKonverto(); click = false; e.processOnServer = false;
        return;
    }
}

function Kthehu(mesazh) {
    myFaqeCelje.kontrolloTeDrejta('RegjistrimRezervimi.aspx?lloj=' + Utils.getUrlVar('lloj') + "&ruaj=po");

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
        e.processOnServer = false;
        Utils.hiqLoadingGif();;
        return;
    }
    click = true;
    if (isValidKoka()) {
       grida.saveRow(idRresht, false, 'clientArray');
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
    $("input[id$='hfLidhur']").val(false); $("input[id$='hfAutorizimi']").val(true);
    pastro();
    pastroFushatKokes();
    var hf = $("#hfShtimModifikim");
    ASPxMenu1.GetItemByName('Shto').SetVisible(true);
    ASPxMenu1.GetItemByName('Fshi').SetVisible(false);
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

function ButtonClickNavBar(s) {

}


function hapPopUpRi(eshteAqt) {
    var qs = '?vjenNga=Shto_RegjistrimRezervimi&veprimi=shtim';
    var headerText;
    if (eshteAqt) {
        qs += '&llojiart=aqt';
        headerText = hfState.Get("JQgridShtoArtikullAqt");
    }
    else {
        qs += '&llojiart=afatshkurter';
        headerText = hfState.Get("JQgridShtoArtikull");
    }
    myButtonClickLupa.LupaUniversal_Click(headerText, 'LupaArtikullShpejte.aspx' + qs, 1100, 600);
}

function hapPopUpModifikoArt(eshteAqt) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var kodArtikulli = '';
    $('#txtKodi' + idRresht).val();
    editorKodi = $('#txtKodi' + idRresht);
    if (editorKodi.val() != '' && editorKodi.val() != null && editorKodi.val() != undefined)
        kodArtikulli = editorKodi.val();
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("JQgridShtoArtikull"), 'LupaArtikullShpejte.aspx?vjenNga=RegjistrimRezervimi&llojiart=afatshkurter&veprimi=modifikim&kodArt=' + kodArtikulli, 1100, 600);
}

function konverto() {
    Utils.konverto("", "", idte, SuccededCallbackKontrolloEkzistojneDokQePoKonvertohen);
}

function SuccededCallbackKontrolloEkzistojneDokQePoKonvertohen() {

    if (cmbKonverto.GetText() == 'FD')
        myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimMagazine.aspx?lloj=dalje' + '&id=' + idte[0] + '&numer=' + 0 + '&indexrow=' + 0 + '&shtim_modifikim=rezervim&niveli=' + cmbKonverto.GetValue() + '&konfigurim=' + cmbKonf.GetValue() + '&pageCacheId=' + window['CurrentPageId'])
    else if (cmbKonverto.GetText() == 'FH')
        myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimMagazine.aspx?lloj=hyrje' + '&id=' + idte[0] + '&numer=' + 0 + '&indexrow=' + 0 + '&shtim_modifikim=rezervim&niveli=' + cmbKonverto.GetValue() + '&konfigurim=' + cmbKonf.GetValue() + '&pageCacheId=' + window['CurrentPageId'])
    else
        myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimDokumentash.aspx?shitje_blerje=shitje&id=' + idte[0] + '&numer=' + 0 + '&indexrow=' + 0 + '&shtim_modifikim=rezervim&niveli=' + cmbKonverto.GetValue() + '&konfigurim=' + cmbKonf.GetValue() + '&pageCacheId=' + window['CurrentPageId']);
}

/*
Function: ButtonClickKonverto
    
Kontrollon nese dokumenti eshte i konvertuar
*/
function ButtonClickKonverto() {//po
    var ids = [Utils.getUrlVar('id')];
    if (!(typeof (ids) == "undefined" || ids == undefined || ids == "" || ids == "0")) {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "KontrolloKonvertuarRezervime"),
            data: JSON.stringify({ ids: [Utils.getUrlVar('id')], pageId: window['CurrentPageId'] })
        }).done(SuccededCallbackKonvertime);
    }
}

var konfigurimet, idte;
function SuccededCallbackKonvertime(result) {
    if (result[1] == "Nuk jane konvertuar") {
        if (result[0].length == 0) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgDokNukMundTeKonvertohet")); return;
        }

        cmbKonverto.ClearItems();
        for (i = 0; i < result[0].length; i++)
            cmbKonverto.AddItem(result[0][i].Kodi, result[0][i].IdNivel); //AddItem(teksti, vlera);
        cmbKonverto.SelectIndex(0);
        cmbKonf.ClearItems();
        for (i = 0; i < result[3].length; i++)
            if (result[3][i].IdNivel == result[0][0].IdNivel)
                cmbKonf.AddItem(result[3][i].KodKonfigAmbjente, result[3][i].IdKonfigAmbjente); //AddItem(teksti, vlera);
        cmbKonf.SelectIndex(0);
        konfigurimet = result[3];
        idte = result[2];
        popKonvertim.Show();
    }
    else myMesazh.ShtoMesazhGabimi(result[1]);
}

function ndryshoNiveli(s, e) {
    cmbKonf.ClearItems();
    for (i = 0; i < konfigurimet.length; i++)
        if (konfigurimet[i].IdNivel == s.GetValue())
            cmbKonf.AddItem(konfigurimet[i].KodKonfigAmbjente, konfigurimet[i].IdKonfigAmbjente); //AddItem(teksti, vlera);
    cmbKonf.SelectIndex(0);
}
function Pane_Collapsed(s, e) {
    if ($('#divgride2').width() != null) {
       
        myJQGrid.fixGridWidth($('#rowed5'), $('#divgride2'));
    }
}
function Pane_Expanded(s, e) {
    if ($('#divgride2').width() != null) {
        
        myJQGrid.fixGridWidth($('#rowed5'), $('#divgride2'));
    }
}
function Pane_Resized(s, e) {
    if ($('#divgride2').width() != null) {
       
        myJQGrid.fixGridWidth($('#rowed5'), $('#divgride2'));
    }
}