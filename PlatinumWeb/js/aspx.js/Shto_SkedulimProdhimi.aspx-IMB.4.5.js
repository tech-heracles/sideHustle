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
    identifikuesPerLocalStorageKey: 'SkedulimProdhimi'
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

        identifikuesPerPopupDokumentat = "Shto_SkedulimProdhimi.aspx";
        identikuesPerPopupKlientFurnitori = "SkedulimProdhimi";
        identikuesPerPopupArtikulli = "SkedulimProdhimi";
        identifikuesPerPopupMakro = "SkedulimProdhimi";
        identifikuesPerPopupMagazina = "SkedulimProdhimi"
        changeName();
        ndryshoKonfigurimin();
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

    var classes = '';
    if (isLidhur === true && $("input[id$='hfShtimModifikim']").val() == 'modifikim')
        classes = 'uigray';
    //            var arr = ['Fshi', 'Kategoria', 'Kodi', 'Emertimi',  'Magazina', 'Njesia', 'Sasia'];
    var arrayPershkrime = [arrayPershkrimiKolonaGrides[0], arrayPershkrimiKolonaGrides[1], arrayPershkrimiKolonaGrides[2], arrayPershkrimiKolonaGrides[3],
     arrayPershkrimiKolonaGrides[4], arrayPershkrimiKolonaGrides[5], arrayPershkrimiKolonaGrides[6], arrayPershkrimiKolonaGrides[7], arrayPershkrimiKolonaGrides[8], arrayPershkrimiKolonaGrides[9], arrayPershkrimiKolonaGrides[10], arrayPershkrimiKolonaGrides[11], arrayPershkrimiKolonaGrides[12], arrayPershkrimiKolonaGrides[13], arrayPershkrimiKolonaGrides[14], arrayPershkrimiKolonaGrides[15], arrayPershkrimiKolonaGrides[16], arrayPershkrimiKolonaGrides[17]];
    var arrayModel = [
          { name: arrayIdKolonaGrides[0], index: arrayIdKolonaGrides[0], width: arrayWidthKolonaGrides[0], hidden: arrayVisibleKolonaGrides[0], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemNrRendor, custom_value: myJQGrid.myValueTextBox } },

            { name: arrayIdKolonaGrides[1], index: arrayIdKolonaGrides[1], width: arrayWidthKolonaGrides[1], hidden: arrayVisibleKolonaGrides[1], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myElemKodi, custom_value: myJQGrid.myValueTextBox } },
            { name: arrayIdKolonaGrides[2], index: arrayIdKolonaGrides[2], width: arrayWidthKolonaGrides[2], hidden: arrayVisibleKolonaGrides[2], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myElemEmertimi, custom_value: myJQGrid.myValueTextBox } },
            { name: arrayIdKolonaGrides[3], index: arrayIdKolonaGrides[3], width: arrayWidthKolonaGrides[3], hidden: arrayVisibleKolonaGrides[3], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemProjekti, custom_value: myJQGrid.myValueTextBox } },
            { name: arrayIdKolonaGrides[4], index: arrayIdKolonaGrides[4], width: arrayWidthKolonaGrides[4], hidden: arrayVisibleKolonaGrides[4], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemIdProjekti, custom_value: myJQGrid.myValueTextBox } },
            { name: arrayIdKolonaGrides[5], index: arrayIdKolonaGrides[5], width: arrayWidthKolonaGrides[5], hidden: arrayVisibleKolonaGrides[5], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemAktiviteti, custom_value: myJQGrid.myValueTextBox } },
            { name: arrayIdKolonaGrides[6], index: arrayIdKolonaGrides[6], width: arrayWidthKolonaGrides[6], hidden: arrayVisibleKolonaGrides[6], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemProdukti, custom_value: myJQGrid.myValueTextBox } },
            { name: arrayIdKolonaGrides[7], index: arrayIdKolonaGrides[7], width: arrayWidthKolonaGrides[7], hidden: arrayVisibleKolonaGrides[7], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemEmertimiProdukti, custom_value: myJQGrid.myValueTextBox } },
            { name: arrayIdKolonaGrides[8], index: arrayIdKolonaGrides[8], width: arrayWidthKolonaGrides[8], hidden: arrayVisibleKolonaGrides[8], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemData, custom_value: myJQGrid.myValueTextBox } },
            { name: arrayIdKolonaGrides[9], index: arrayIdKolonaGrides[9], width: arrayWidthKolonaGrides[9], hidden: arrayVisibleKolonaGrides[9], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemNga, custom_value: myJQGrid.myValueTextBox } },
            { name: arrayIdKolonaGrides[10], index: arrayIdKolonaGrides[10], width: arrayWidthKolonaGrides[10], hidden: arrayVisibleKolonaGrides[10], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemNe, custom_value: myJQGrid.myValueTextBox } },
            { name: arrayIdKolonaGrides[11], index: arrayIdKolonaGrides[11], width: arrayWidthKolonaGrides[11], hidden: arrayVisibleKolonaGrides[11], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemKoha, custom_value: myJQGrid.myValueTextBox } },
            { name: arrayIdKolonaGrides[12], index: arrayIdKolonaGrides[12], width: arrayWidthKolonaGrides[12], hidden: arrayVisibleKolonaGrides[12], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemComboNjesia, custom_value: myJQGrid.myValueCombo } },
            { name: arrayIdKolonaGrides[13], index: arrayIdKolonaGrides[13], width: arrayWidthKolonaGrides[13], hidden: arrayVisibleKolonaGrides[13], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemShenime, custom_value: myJQGrid.myValueTextBox } },
            { name: arrayIdKolonaGrides[14], index: arrayIdKolonaGrides[14], width: arrayWidthKolonaGrides[14], hidden: arrayVisibleKolonaGrides[14], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemKosto, custom_value: myJQGrid.myValueTextBox } },
            { name: arrayIdKolonaGrides[15], index: arrayIdKolonaGrides[15], width: arrayWidthKolonaGrides[15], hidden: arrayVisibleKolonaGrides[15], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemKostoTotale, custom_value: myJQGrid.myValueTextBox } },
            { name: arrayIdKolonaGrides[16], index: arrayIdKolonaGrides[16], width: arrayWidthKolonaGrides[16], hidden: arrayVisibleKolonaGrides[16], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myelemIdKodi, custom_value: myJQGrid.myValueTextBox }, hidedlg: true },
            { name: arrayIdKolonaGrides[17], index: arrayIdKolonaGrides[17], width: arrayWidthKolonaGrides[17], hidden: arrayVisibleKolonaGrides[17], classes: classes, sorttype: "int", editable: true, edittype: 'custom', editoptions: { custom_element: myElemButtonFshi, custom_value: myValueButtonFshi }, hidedlg: true }
    ];


    var selektoriGrides = "#rowed5";

    var gridParams = {
        emergride: selektoriGrides,
        arrayPershkrime: arrayPershkrime,
        arrayModel: arrayModel,
        lidhur: isLidhur,
        emerEditorKodi: "txtBurimi",
        widthi: $('#divgride2').width() - 5,
        fokus: fokusi,
        //autoComplete4: "txtKategoriShpenzimi",
        lostFocusKoloneFundit: lostFocusKoloneFundit,
        resetRreshtKorent: resetRreshtKorent,

        autocompleteList: [{ emerEditor: "txtBurimi", selectFunc: selectFunc, changeFunc: changeFunc, shtoDataKod: false },
                           { emerEditor: 'txtAktiviteti', selectFunc: selectFunc2, changeFunc: changeFunc2, shtoDataKod: false },
                           { emerEditor: 'txtProdukti', selectFunc: selectFunc3, changeFunc: changeFunc3, shtoDataKod: false }],

        konfigToolbar: {
            konfigGrid: $('#hfTeDrejtaKonfGride').val(),
            ruajKolonatEGrides: ruajKolonatEGrides
        }

    };
   return myJQGrid.initGride(gridParams);
}


//    myJQGrid.inicializoGride("#rowed5", arrayPershkrime, arrayModel, isLidhur,
//        lastsel2, "#txtBurimi", "", undefined, 0, '', undefined, $('#divgride2').width() - 5, undefined,
//        fokusi, null, null, '#txtAktiviteti', "#txtProdukti", undefined, undefined, $('#hfTeDrejtaKonfGride').val());
//}
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
function myElemNrRendor(value, options) {//po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[0];
    return myJQGrid.myElemNrRendor(value, options, idRresht, 'txtNrRendor', grida);
}
/*
Vendos vlerat default te fushave ne gride ne baze te emrit te kolones
*/
function vendosVleraDefaultNeGride(grida) {
    grida.setVlereDefault('txtKoha', 0);
    grida.setVlereDefault('txtKosto', 0);
    grida.setVlereDefault('txtKostoTotale', 0);
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
    grida.setShifraPasPresjes('txtKoha', formatNumri.ShifraPasPresjesSasia);
    grida.setShifraPasPresjes('txtKosto', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtKostoTotale', formatNumri.ShifraPasPresjesVlefta);
}

/*
Formaton vlerat e fushave sipas formatit perkates.
*/
function vendosKonfigFormatNumri() {
    var grida = $('#rowed5');
    var idTe = grida.jqGrid('getDataIDs');
    for (j = 0; j < idTe.length; j++) {
        var idRreshti = idTe[j];
        grida.formatoQelize('txtKoha', idRreshti);
        grida.formatoQelize('txtKosto', idRreshti);
        grida.formatoQelize('txtKostoTotale', idRreshti);
    }
}
function selectFunc(event, ui, emerfushe, idArt, kodArt) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    if (idArt != undefined && kodArt != undefined && idArt != "" && kodArt != "") {
        $(emerfushe).val(kodArt);
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "KtheVleraBurimeMeIDRow"),
            data: JSON.stringify({ idja: idArt, rreshti: idRow })
        }).done(SucceededCallbackArtNew);

        return false;
    }
    if (ui.item != null && ui.item != null) {
        $(emerfushe).val(ui.item.label);
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "KtheVleraBurimeMeIDRow"),
            data: JSON.stringify({ idja: ui.item.value, rreshti: idRow })
        }).done(SucceededCallbackArtNew);
        return false;
    }
}

function changeFunc(event, ui, emerKodi, index) {
    //var index = -1;
    //var idKod = 'txtBurimi';
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
            if (!Utils.IsNullOrWhiteSpace(kodi)) {
                $.ajax({
                    pritPergjigje: true,
                    url: Utils.getServerApiUrl("Rregjistrime", "ktheVleraBurimeMeKodRow"),
                    data: JSON.stringify({ kodi: kodi, rreshti: idRow })
                }).done(SucceededCallbackArtNew);
                return;
            }

            vendosBurim([index, null]);
            return;

        }
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheVleraBurimeMeKodRow"),
            data: JSON.stringify({ kodi: ui.item.label, rreshti: idRow })
        }).done(SucceededCallbackArtNew);
        return;
    }
    var rreshti = grida.jqGrid('getRowData', index);

    if (ui == null || ui.item == null) {
        if (rreshti.txtBurimi != "") {

            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "ktheVleraBurimeMeKodRow"),
                data: JSON.stringify({ kodi: rreshti.txtBurimi, rreshti: index })
            }).done(SucceededCallbackArtNew);
            return;
        }

        vendosBurim([index, null]);

    }
}

function selectFunc2(event, ui, emerfushe, idArt, kodArt) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    if (idArt != undefined && kodArt != undefined && idArt != "" && kodArt != "") {
        $(emerfushe).val(kodArt);

        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "KtheVleraAktiviteteMeIDRow"),
            data: JSON.stringify({ idja: idArt, rreshti: idRow })
        }).done(SucceededCallbackAktivitet);

        return false;
    }
    if (ui.item != null && ui.item != null) {
        $(emerfushe).val(ui.item.label);
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "KtheVleraAktiviteteMeIDRow"),
            data: JSON.stringify({ idja: ui.item.value, rreshti: idRow })
        }).done(SucceededCallbackAktivitet);
        return false;
    }
}

function changeFunc2(event, ui, emerKodi, index) {
    //var index = -1;
    //var idKod = 'txtAktiviteti';
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
            if (typeof(kodi) != "undefined" && kodi != undefined && kodi != "") {
                $.ajax({
                    pritPergjigje: true,
                    url: Utils.getServerApiUrl("Rregjistrime", "ktheVleraAktiviteteMeKodRow"),
                    data: JSON.stringify({ kodi:kodi, rreshti: idRow })
                }).done(SucceededCallbackAktivitet);
            }

            vendosAktivitet([index, null]);
            return;
        }
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheVleraAktiviteteMeKodRow"),
            data: JSON.stringify({ kodi: ui.item.label, rreshti: idRow })
        }).done(SucceededCallbackAktivitet);
        return;
    }
    var rreshti = grida.jqGrid('getRowData', index);

    if (ui == null || ui.item == null) {
        if (rreshti.txtAktiviteti != "") {

            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "ktheVleraAktiviteteMeKodRow"),
                data: JSON.stringify({ kodi: rreshti.txtAktiviteti, rreshti: index })
            }).done(SucceededCallbackAktivitet);
            return;
        }

        vendosAktivitet([index, null]);

    }
}

function selectFunc3(event, ui, emerfushe, idArt, kodArt) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    if (idArt != undefined && kodArt != undefined && idArt != "" && kodArt != "") {
        $(emerfushe).val(kodArt);

        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraArtMeIDPerRec"),
            data: JSON.stringify({ idja: idArt, rreshti: idRow, data: new Date(), idmag: 0})
        }).done(SucceededCallbackArt);


        return false;
    }
    if (ui.item != null && ui.item != null) {
        $(emerfushe).val(ui.item.label);
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraArtMeIDPerRec"),
            data: JSON.stringify({ idja: ui.item.value, rreshti: idRow, data: new Date(), idmag: 0 })
        }).done(SucceededCallbackArt);
        return false;
    }
}

function changeFunc3(event, ui, emerKodi, index) {
    //var index = -1;
    //var idKod = 'txtProdukti';
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
            if (kodi != undefined && typeof(kodi) != "undefined" && kodi != "") {
               $.ajax({
                    pritPergjigje: true,
                    url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraArtMeKodRec"),
                    data: JSON.stringify({ kodi: kodi, rreshti: idRow, data: new Date(), kodmag: '' })
                }).done(SucceededCallbackArt);
            }

            vendosArt([index, null]);
            return;

        }
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraArtMeKodRec"),
            data: JSON.stringify({ kodi: ui.item.label, rreshti: idRow, data: new Date(), kodmag: '' })
        }).done(SucceededCallbackArt);
        return;
    }
    var rreshti = grida.jqGrid('getRowData', index);

    if (ui == null || ui.item == null) {
        if (rreshti.txtProdukti != "") {

           $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "ktheRowVleraArtMeKodRec"),
                data: JSON.stringify({ kodi: rreshti.txtProdukti, rreshti: index, data: new Date(), kodmag: '' })
            }).done(SucceededCallbackArt);
            return;
        }

        vendosArt([index, null]);

    }
}
function resetRreshtKorent(idRreshti) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    if (idRreshti == idRow && grida.getTekstQelize('txtBurimi', idRow) != undefined) {
        grida.setTekstQelize('txtBurimi', idRreshti, '');
        grida.setTekstQelize('txtIdKodi', idRreshti, '');
        grida.setTekstQelize('txtProjekti', idRreshti, '');
        grida.setTekstQelize('txtIdProjekti', idRreshti, 0);
        grida.setTekstQelize('txtEmertimiB', idRreshti, '');
        grida.setTekstQelize('txtAktiviteti', idRreshti, '');
        grida.setTekstQelize('txtProdukti', idRreshti, '');
        grida.setTekstQelize('txtShenime', idRreshti, '');
        grida.setTekstQelize('txtEmertimiA', idRreshti, '');
        grida.setTekstQelize('txtData', idRreshti, dteDtDok.GetText());
        grida.setTekstQelize('txtNga', idRreshti, '00:00:00');
        grida.setTekstQelize('txtNe', idRreshti, '00:00:00');
        grida.setTekstQelize('txtKoha', idRreshti);
        grida.setTekstQelize('txtNjesia', idRreshti, 'Ore');
        grida.setTekstQelize('txtKosto', idRreshti);
        grida.setTekstQelize('txtKostoTotale', idRreshti);


        return;
    }
    grida.jqGrid('delRowData', idRreshti);
    return;
}

function SucceededCallbackArtNew(result) {
    vendosBurim(result);
}
function SucceededCallbackArt(result) {
    vendosArt(result);
}
function SucceededCallbackAktivitet(result) {
    vendosAktivitet(result);
}

function vendosBurim(result) {
    var grida = $("#rowed5");
    var idRow = grida.getLastSel2();
    if (result == null)
        return;
    var idRreshti = result[0];
    var burimi = result[1];
    var kosto = result[2];

    var kodi = "#txtBurimi" + idRreshti;
    var idKodi = "#txtIdKodi" + idRreshti;
    var emerArt = '#txtEmertimiB' + idRreshti;

    if (burimi == null || burimi == undefined || burimi.IdBurimi == -1) {
        resetRreshtKorent(idRreshti);
        return;
    }
    if (burimi.Aktiv == false) {
        myMesazh.ShtoMesazhGabimi("Burimi nuk eshte aktiv");
        resetRreshtKorent(idRreshti);
        return;
    }

    if (grida.getTekstQelize('txtIdKodi', idRreshti) == burimi.IdBurimi)
        return; //eshte i njejti artikull 
    hfArt.Set(burimi.IdBurimi, burimi);
    grida.setTekstQelize('txtBurimi', idRreshti, burimi.Kodi);
    if (idRreshti == idRow && grida.getTekstQelize('txtBurimi', idRow) != undefined) {
        myJQGrid.closeAutocomplete('txtBurimi', idRreshti);
        $(emerArt).attr('title', burimi.Emertimi);
    }
    grida.setTekstQelize('txtIdKodi', idRreshti, burimi.IdBurimi);
    grida.setTekstQelize('txtEmertimiB', idRreshti, burimi.Emertimi);
    grida.setTekstQelize('txtKosto', idRreshti, burimi.KostoPlan);
    if (idRreshti == idRow && grida.getTekstQelize('txtBurimi', idRow) != undefined)
        Llogaritkohen();
}

function vendosAktivitet(result) {
    var grida = $("#rowed5");
    var idRow = grida.getLastSel2();
    if (result != null) {
        var idRreshti = result[0];
        var aktiviteti = result[1];


        var kodi = "#txtAktiviteti" + idRreshti;

        if (aktiviteti == null || aktiviteti == undefined || aktiviteti.IdBurimi == -1) {
            grida.setTekstQelize('txtAktiviteti', idRreshti, "");
            return;
        }
        var njesi = 'Ore';
        if (aktiviteti.NjesiKohe == 1)
            njesi = 'Sec';
        else if (aktiviteti.NjesiKohe == 2)
            njesi = 'Min';
        else if (aktiviteti.NjesiKohe == 3)
            njesi = 'Ore';
        else if (aktiviteti.NjesiKohe == 4)
            njesi = 'Dite';

        if (idRreshti == idRow && grida.getTekstQelize('txtAktiviteti', idRow) != undefined) {
            grida.setTekstQelize('txtAktiviteti', idRreshti, aktiviteti.Kodi);
            myJQGrid.closeAutocomplete('txtAktiviteti', idRreshti);

            var comboMag = $('#txtNjesia' + idRreshti);
            comboMag.replaceWith(myelemComboNjesia(njesi, null, idRreshti).children()[0]);
            //  grida.setTekstQelize('txtNjesia', idRreshti, aktiviteti.NjesiKohe);
            // grida.setTekstQelize('txtEmertimiB', idRreshti, burimi.Emertimi)
            // $(emerArt).attr('title', burimi.Emertimi);
            //  grida.setTekstQelize('txtKosto', idRreshti, burimi.KostoPlan);

            // changedSasia();
            return;
        }




        grida.setTekstQelize('txtAktiviteti', idRreshti, aktiviteti.Kodi);
        grida.setTekstQelize('txtNjesia', njesi);
        //  kontrolloRow(idRreshti);
        //   grida.setTekstQelize('txtIdKodi', idRreshti, burimi.IdBurimi);
        // grida.setTekstQelize('txtEmertimiB', idRreshti, burimi.Emertimi);
        //  grida.setTekstQelize('txtKosto', idRreshti, burimi.KostoPlan);


        return;
    }
}
function vendosArt(result) {
    var grida = $("#rowed5");
    var idRow = grida.getLastSel2();
    if (result == null)
        return;

    var idRreshti = result[0];
    var aktiviteti = result[1];
    var kodi = "#txtProdukti" + idRreshti;
    if (aktiviteti == null || aktiviteti == undefined || aktiviteti.IdBurimi == -1) {
        grida.setTekstQelize('txtProdukti', idRreshti, "");
        return;
    }
    grida.setTekstQelize('txtProdukti', idRreshti, aktiviteti.KodArtikulli);
    if (idRreshti == idRow && grida.getTekstQelize('txtProdukti', idRow) != undefined)
        myJQGrid.closeAutocomplete('txtProdukti', idRreshti);
    grida.setTekstQelize('txtEmertimiA', idRreshti, aktiviteti.PershkrimArtikulli);
}

function formGridColsArray(isLidhur) {
    var hfGridKod = $('#hfGridaKodi');

    var IdKonfigAmbjenteLupat = [{ hfVar: hfGridKod, kodiText: "txtBurimi" }];
    myJQGrid.formArrayKolGrides($("input[id$='HfGridCol']"), arrayIdKolonaGrides, arrayPershkrimiKolonaGrides, arrayVisibleKolonaGrides, arrayReadOnlyKolonaGrides, isLidhur, arrayWidthKolonaGrides, IdKonfigAmbjenteLupat, arrayRenditjeKolonaGrides);
}

function myelemIdKodi(value, options) {//po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if (value == "")
        value = btnBurimi.GetValue();
    return myJQGrid.myElemIdKodi(value, options, arrayReadOnlyKolonaGrides[16], idRresht, arrayIdKolonaGrides[16]);
}
function myElemShenime(value) {//po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[13];
    return myJQGrid.myElemEmertimi(value, disabled, idRresht, arrayIdKolonaGrides[13]);
}
function myelemIdProjekti(value, options) {//po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if (value == "")
        value = $('#hfPrioriteti').val();
    return myJQGrid.myElemIdKodi(value, options, arrayReadOnlyKolonaGrides[4], idRresht, arrayIdKolonaGrides[4]);
}
function myelemKosto(value, options) {//po
    disabled = arrayReadOnlyKolonaGrides[14];
    if (value == "")
        value = $('#hfKosto').val();
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, disabled, idRresht, arrayIdKolonaGrides[14], keyup, hfFormatNumri, keyup);
}
function dateGotFocus(s, e) {
    window.setTimeout(function () { s.SetCaretPosition(0); }, 0);
}
function keyup() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    editorKodi = grida.getTekstQelize('txtBurimi', idRresht);
    editorSasia = grida.getTekstQelize('txtKoha', idRresht);
    editorSasiaMbetur = grida.getTekstQelize('txtKosto', idRresht);
    editorSasiDisp = grida.getTekstQelize('txtKostoTotale', idRresht);
    var sasia;

    if (editorSasiaMbetur == '.') {
        grida.setTekstQelize('txtKosto', idRresht, '0.');
        return;
    }
    if (editorSasiaMbetur == "" || isNaN(editorSasiaMbetur)) {
        myMesazh.ShtoMesazhGabimi('Kosto duhet te jete numer');
        sasia = grida.getVlereDefault('txtKosto');
        grida.setTekstQelize('txtKosto', idRresht, sasia);
        $('#txtKosto' + idRresht).focus();
    }
    else
        if (editorSasiaMbetur == "0") {
            myMesazh.ShtoMesazhGabimi("Kosto nuk mund te jete zero");
            sasia = grida.getVlereDefault('txtKosto');
            grida.setTekstQelize('txtKosto', idRresht, sasia);
            $('#txtKosto' + idRresht).focus();
        }

    if (sasia == null)
        sasia = parseFloat(editorSasiaMbetur);
    Llogaritkohen();
}
function myelemKostoTotale(value, options) {//po
    disabled = arrayReadOnlyKolonaGrides[15];
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, disabled, idRresht, arrayIdKolonaGrides[15], keyup, hfFormatNumri, keyup);
}

function myelemData(value) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if (value == "")
        value = dteDtDok.GetText();
    var hf = document.getElementById("hfShtimModifikim");
    disabled = arrayReadOnlyKolonaGrides[8];
    //  if (hf.value == "modifikim")
    //    disabled = 'True';
    return myJQGrid.myelemData(value, disabled, idRresht, 'txtData', undefined, onchanged);

}
function myElemNga(value) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if (value == "")
        value = '00:00:00';
    var hf = document.getElementById("hfShtimModifikim");
    disabled = arrayReadOnlyKolonaGrides[9];
    //  if (hf.value == "modifikim")
    //    disabled = 'True';
    return myJQGrid.myelemTimePicker(value, disabled, idRresht, 'txtNga', undefined, onchangednga, onkeyup1);

}
function myElemNe(value) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if (value == "")
        value = '00:00:00';
    var hf = document.getElementById("hfShtimModifikim");
    disabled = arrayReadOnlyKolonaGrides[10];
    //  if (hf.value == "modifikim")
    //    disabled = 'True';
    return myJQGrid.myelemTimePicker(value, disabled, idRresht, 'txtNe', undefined, onchangedne, onkeyup2);

}
function onkeyup1() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    onkeyup($('#txtNga' + idRresht));
}
function onkeyup2() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    onkeyup($('#txtNe' + idRresht));
}
function onkeyup(elementi) {
    if (elementi.val().length == 2)
        elementi.val(elementi.val() + ':');
    if (elementi.val().length == 5)
        elementi.val(elementi.val() + ':');

    if (elementi.getCursorPosition() == 2)
        elementi.setCursorPosition(3, 5);
    if (elementi.getCursorPosition() == 5)
        elementi.setCursorPosition(6, 8);

}
function onchangednga() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    $('#txtNe' + idRresht).val($('#txtNga' + idRresht).val());
}
function Llogaritkohen() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var njesia = $('#txtNjesia' + idRresht).val();

    var koha = 0;

    var ore = $('#txtNe' + idRresht).val().substring(0, 2) - $('#txtNga' + idRresht).val().substring(0, 2);
    var min = ($('#txtNe' + idRresht).val().substring(3, 5) - $('#txtNga' + idRresht).val().substring(3, 5)) / 60;
    var sec = ($('#txtNe' + idRresht).val().substring(6, 8) - $('#txtNga' + idRresht).val().substring(6, 8)) / 3600;
    koha = (ore + min + sec)
    if (njesia == 1)
        njesiakohe = 3600;
    else if (njesia == 2)
        njesiakohe = 60;
    else if (njesia == 3)
        njesiakohe = 1;
    else njesiakohe = 1 / 24;
    if (koha != 0)
        $('#txtKoha' + idRresht).val(koha * njesiakohe);
    $('#txtKostoTotale' + idRresht).val($('#txtKosto' + idRresht).val() * $('#txtKoha' + idRresht).val() / njesiakohe)
}
function onchangedne() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if ($('#txtNe' + idRresht).val() < $('#txtNga' + idRresht).val()) {
        myMesazh.ShtoMesazhGabimi('Koha Ne eshte me e vogel se koha nga');
        $('#txtNe' + idRresht).val($('#txtNga' + idRresht).val());
    }

    Llogaritkohen();
}
function onchanged() {

}


/*
Function: myElemKodi

Nderton nje textbox dhe nje buton per te zgjedhur artikullin ose makron sipas llojit te zgjedhur tek combo Kategoria
*/
function myElemKodi(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if (value == "")
        value = btnBurimi.GetText();
    disabled = arrayReadOnlyKolonaGrides[1];
    return myJQGrid.myElemKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[1], ButtonClickKodi, keyPressKodi, changeFunc, fokusi, lostFocusKoloneFundit);
}
function myElemAktiviteti(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[5];
    return myJQGrid.myElemKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[5], ButtonClickAktiviteti, keyPressAktiviteti, changeFunc2);
}
function myElemProdukti(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if (value == "")
        value = $('#hfKodProdukti').val();
    disabled = arrayReadOnlyKolonaGrides[6];
    return myJQGrid.myElemKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[6], ButtonClickArtikulli, keyPressArtikulli, changeFunc3);
}
function ButtonClickAktiviteti() {//po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    myButtonClickLupa.LupaUniversal_Click("Zgjidhni aktivitetin", 'LupaAktivitete.aspx?vjenNga=SkedulimProdhimi&idburimi=' + grida.getTekstQelize('txtIdKodi', idRresht), 600, 500);

}
function ButtonClickArtikulli() {//po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    identikuesPerPopupArtikulli = 'SkedulimProdhimi'
    myButtonClickLupa.LupaUniversal_Click("Zgjidhni artikullin", 'LupaArtikull.aspx?klasa=55&idplanifikimi=' + grida.getTekstQelize('txtIdProjekti', idRresht) + '&idKonfigAmbjente= ', 600, 500);

}
function keyPressKodi(event) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    try {
        var vlera = event.target.value;  //$('#txtBurimi' + lastsel2).val();
        if (vlera == "")
            $('#txtBurimi' + idRresht).autocomplete("close");
        else
            callWebserviceKodi(vlera);
    } catch (e) { }

}
function keyPressAktiviteti(event) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    try {
        var vlera = event.target.value;  //$('#txtBurimi' + lastsel2).val();
        if (vlera == "")
            $('#txtBurimi' + idRresht).autocomplete("close");
        else
            callWebserviceAktivitete(vlera);
    } catch (e) { }

}
function keyPressArtikulli(event) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    try {
        var vlera = event.target.value;  //$('#txtBurimi' + lastsel2).val();
        if (vlera == "")
            $('#txtBurimi' + idRresht).autocomplete("close");
        else
            callWebserviceArtikulli(vlera);
    } catch (e) { }

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
    if (value == "")
        value = $('#hfPershkrimBurimi').val();
    return myJQGrid.myElemEmertimi(value, arrayReadOnlyKolonaGrides[2], idRresht, arrayIdKolonaGrides[2]); // 'txtEmertimi'
}
function myElemEmertimiProdukti(value) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if (value == "")
        value = $('#hfPershkrimProdukti').val();
    return myJQGrid.myElemEmertimi(value, arrayReadOnlyKolonaGrides[7], idRresht, arrayIdKolonaGrides[7]); // 'txtEmertimi'
}


function myElemKoha(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, arrayReadOnlyKolonaGrides[11], idRresht, arrayIdKolonaGrides[11], changedSasia, hfFormatNumri, focusoutSasia); //txtSasia  
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
}


/*
Function: myelemComboMagazina

Nderton nje combobox per te zgjedhur magazinen.
*/
function myelemProjekti(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if (value == "")
        value = btnProjekti.GetText();
    var disabled = false;
    if (arrayReadOnlyKolonaGrides[3] == 'True')
        disabled = true;

    var el = myJQGrid.myElemKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[3], ButtonClickProjektig);
    //e bejme disable fushen e projektit sepse perdoruesi nuk mund te shkruaj nr projekti vetem ta zgjedhi ate
    el.children(":first").attr("disabled", "disabled");
    return el;
}

function ButtonClickProjektig() {
    var grida = $('#rowed5');

    var queryStr = '';
    popupUniversal.SetHeaderText("Zgjidhni planifikimin");
    popupUniversal.SetContentUrl('LupaPlanifikime.aspx?vjenNga=SkedulimProdhimiGrida&idKonfigAmbjente=' + queryStr);
    popupUniversal.SetSize(widthLupaMagazina, heightLupaMagazina);
    popupUniversal.Show();
}

function changeNjesia() {

    Llogaritkohen();
}

function myelemComboNjesia(value, options, idRreshti) {//po
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();;
    if (idRreshti === undefined)
        idRreshti = idRow;

    if (value == "")
        value = 'Ore';

    var arrayOptions = new Array(0);
    arrayOptions = new Array();

    var objNjesi = new Object();
    objNjesi.value = 1;
    objNjesi.text = 'Sec';
    arrayOptions[0] = objNjesi;
    var objNjesi = new Object();
    objNjesi.value = 2;
    objNjesi.text = 'Min';
    arrayOptions[1] = objNjesi;
    var objNjesi = new Object();
    objNjesi.value = 3;
    objNjesi.text = 'Ore';
    arrayOptions[2] = objNjesi;
    var objNjesi = new Object();
    objNjesi.value = 4;
    objNjesi.text = 'Dite';
    arrayOptions[3] = objNjesi;

    return myJQGrid.myElemCombo(value, 'txtNjesia', idRow, changeNjesia, arrayOptions, disabled);
}
/*
Function: callWebserviceKodi
    
Sugjeron listen e artikujve kur shkruajme te kodi.
Shiko funksionin <SucceededCallbackKodi>.
*/
function callWebserviceKodi(vlera) {
    var grida = $('#rowed5');

    try {
     $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheBurime"),
            data: JSON.stringify({ prefixText: vlera })
        }).done(SucceededCallbackKodi);
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
    }
    return;

}
function callWebserviceAktivitete(vlera) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    try {
        var idburimi = grida.getTekstQelize('txtIdKodi', idRresht);
        if (idburimi != "" && idburimi != "0")
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "ktheAktiviteteSipasBurimit"),
                data: JSON.stringify({ prefixText: vlera, idburimi: idburimi })
            }).done(SucceededCallbackAktiv);
        else
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Konfigurime", "ktheAktivitete"),
                data: JSON.stringify({ prefixText: vlera, idNdermarrje: hfState.Get('idNdermarrje') })
            }).done(SucceededCallbackAktiv);
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
    }
    return;

}
function merrArtikullPlanifikimi(idplanifikimi, index) {
    var idIdja = idplanifikimi;
    if (idIdja != undefined && typeof (idIdja) != "undefined" && idIdja != "") {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheArtikullPlanifikimi"),
            data: JSON.stringify({ idja: idIdja, rreshti: index })
        }).done(SucceededCallbackProduktiplan);
    }
}
function SucceededCallbackProduktiplan(result) {
    if (result[1] != null)
        vendosArt(result);
}
function callWebserviceArtikulli(vlera) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    try {
        var idplanifikimi = grida.getTekstQelize('txtIdProjekti', idRresht);
        if (idplanifikimi != undefined && typeof (idplanifikimi) != "undefined" && idplanifikimi != "") {
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "ktheArtikullPerProdhimPlanifikim"),
                data: JSON.stringify({ prefixText: vlera, idplanifikimi: idplanifikimi })
            }).done(SucceededCallbackART);
        }
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
    }
    return;

}
/*
Function: SucceededCallbackKodi
    
Sugjeron listen e artikujve kur shkruajme te kodi.
*/
function SucceededCallbackKodi(result) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    myJQGrid.SucceededCallbackKodi(result, '#txtBurimi' + idRresht);

}
function SucceededCallbackAktiv(result) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    myJQGrid.SucceededCallbackKodi(result, '#txtAktiviteti' + idRresht);

}
function SucceededCallbackART(result) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    myJQGrid.SucceededCallbackKodi(result, '#txtProdukti' + idRresht);

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
    jQuery("#txtProjekti" + idRresht).focus();
    jQuery("#txtProjekti" + idRresht).blur();
    jQuery("#txtBurimi" + idRresht).focus();
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
    if ($('#hfShtimModifikim').val() == "modifikim"||$('#hfShtimModifikim').val() == "klonim") {
        var colTrupMag = JSON.parse($('#HfColTrupMag').val());
        var colArt = JSON.parse($('#HfColArt').val());
        var colBurime = JSON.parse($('#HfColBurimi').val());
        var colAktivitete = JSON.parse($('#HfColAktiviteti').val());
        var colPlanifikime = JSON.parse($('#HfColPlanifikime').val());
        //        var colTvshArt = JSON.parse($('#HfColTvshArt').val());
        var konfAmb = JSON.parse($('#HfKonfAmb').val());
        grida.setLastSel2(1);
        idRresht = 1;
        grida.jqGrid('clearGridData');
        var kodi, emertimi, projekti, idprojekti, aktiviteti, produkti, emertimia, data, nga, ne, koha, njesia, shenime, kosto, kostototale, idkodi;

    }
    for (var i = 0; i < colTrupMag.length; i++) {

        if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides - 1] == 'True' || isLidhur)
            be = myJQGrid.myValueButtonFshi(true, idRresht, "#rowed5");
        else
            be = myJQGrid.myValueButtonFshi(false, idRresht, "#rowed5");
        kodi = colBurime[i].Kodi;
        emertimi = colBurime[i].Emertimi;
        if ($('#hfShtimModifikim').val() != "klonim") {
            projekti = colPlanifikime[i].NrDok;
            idprojekti = colPlanifikime[i].IdKokaPlanifikim;
        }
        else {
            projekti = "";
            idprojekti = 0;
        }
        aktiviteti = colAktivitete[i].Kodi;
        

        produkti = colArt[i].KodArtikulli;
        idkodi = colBurime[i].IdKodi;
        emertimia = colArt[i].PershkrimArtikulli;
        
          var tmpArtikulli = colBurime[i];
        //        tmpArtikulli.listeTvsh = colTvshArt[i]; //todo kelvin
          hfArt.Set(colBurime[i].IdBurimi, tmpArtikulli);
          data = new Date(parseInt(colTrupMag[i].Data.replace('/Date(', '').replace(')/', ''))).format('dd/MM/yyyy');;
          nga = new Date(parseInt(colTrupMag[i].Nga.replace('/Date(', '').replace(')/', ''))).format('HH:mm:ss'); //colTrupMag[i].Nga;
          ne = new Date(parseInt(colTrupMag[i].Ne.replace('/Date(', '').replace(')/', ''))).format('HH:mm:ss');// colTrupMag[i].Ne;
          koha = colTrupMag[i].Koha;
          shenime = colTrupMag[i].Shenime;
          kosto = colTrupMag[i].Kosto;
          kostototale = colTrupMag[i].KostoTotale;
        switch (colTrupMag[i].Njesia)
        {
        	case 1:
        	    njesia = 'Sec';
        	    break;
            case 2:
                njesia = 'Min';
                break;
            case 3:
                njesia = 'Ore';
                break;
            case 4:
                njesia = 'Dite';
                break;
        }
       

        var datarow = {
            txtNrRendor: i + 1, txtBurimi: kodi, txtEmertimiB: emertimi, txtProjekti: projekti, txtIdProjekti: idprojekti,
            txtAktiviteti: aktiviteti, txtProdukti: produkti, txtEmertimiA: emertimia, txtData: data, txtNga: nga, txtNe: ne, txtKoha: koha, txtNjesia: njesia, txtShenime: shenime,txtKosto:kosto,txtKostoTotale:kostototale, txtIdKodi: idkodi, txtFshi: be
        };
        var su = grida.jqGrid('addRowData', parseInt(idRresht), datarow);
             grida.setTekstQelize('txtNrRendor', idRresht, grida.getInd(idRresht, false));
             idRresht = idRresht + 1;
             grida.setLastSel2(idRresht);
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
            window.parent.callWebServiceKtheInfoLart('Shto_SkedulimProdhimi.aspx', 0);
        else
            window.parent.callWebServiceKtheInfoLart('Shto_SkedulimProdhimi.aspx', Utils.getUrlVar('id'));

    } catch (e) { }
    myCookies.createCookie('adresa', window.location.href, 1);
}

/*
Function: ButtonClickKerko
    
Hap lupen e dokumentave.
*/
function ButtonClickKerko(listUrl) {
    var queryString = {
        veprimi: 'SkedulimProdhimi',
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
    var idGjuha = hfState.Get('idGjuha');
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
}

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}

var kushtet; var colKushte; var colAlterKusht;
var fokusi = 0; var info = false;
var colGrida;

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
    if ($("input[id$='hfShtimModifikim']").val() == "shtim"||$("input[id$='hfShtimModifikim']").val() == "klonim") {
        hfNrAuto.Clear();
        hfNrAutoShitje.Clear();
        vendosNrAutomatik(colAtrTrupi, colKontrollet);
    }

    for (var i = 0; i < colKontrollet.length - 1; i++) {
        //id e konfigurimit te lupes vendoset ne hidden field
        //vlera e saj do t'i kalohet si query string hapjes se popUp-it
        if (colAtrTrupi[i].KodKontrolli == "btnProjekti")
            hfKl.value = colKontrollet[i].IdKonfigAmbjenteLupa.toString(); //merret id e konfigurimit te lupes per lupen e klient furnitorit ne forme
        else
            if (colAtrTrupi[i].KodKontrolli == "btnBurimi")
                hfMag.value = colKontrollet[i].IdKonfigAmbjenteLupa.toString(); //merret id e konfigurimit te lupes per lupen e magazines ne forme            
    }




    grida.jqGrid('GridUnload', "rowed5");;
    var isLidhur = ($("input[id$='hfLidhur']").val().toLowerCase() === 'true');
    formGridColsArray(isLidhur);
    grida.setLastSel2(-1);
    grida = $('#rowed5');
    ruajFormatetNeGride(grida, formatNumriZgjedhur);
    inicializoGride(isLidhur);
    mbushGrideNgaHiddenFieldet(isLidhur);
    Utils.konfiguroAccorditionPasKonfigDokumenti(varKonfig.identifikuesPerLocalStorageKey);
}

function DateChanged(s, e) {
    var date = dteDtDok.GetText();
    var grida = $("#rowed5");
    var indexe = grida.getDataIDs();
    for (i = 0; i < indexe.length; i++) {
        grida.setTekstQelize('txtData', indexe[i], date);

    }
    var hfKontrollet = JSON.parse($('#hfKontrolletNrAutom').val());
    var atributet = JSON.parse($('#hfAtributeNrAutom').val());
    vendosNrAutomatik(atributet, hfKontrollet);
}
function pershkrimi() {
    var per = txtShenime.GetText();
    var grida = $("#rowed5");
    var indexe = grida.getDataIDs();
    for (i = 0; i < indexe.length; i++) {
        grida.setTekstQelize('txtShenime', indexe[i], per);

    }
}
function ChangeProjekti() {
    var per = btnProjekti.GetText();
    if (Utils.IsNullOrWhiteSpace(per))
        return;
    var idprojekti = $('#hfPrioriteti').val();    
    var grida = $("#rowed5");
    var idRresht = grida.getLastSel2();
    var indexe = grida.getDataIDs();
    for (i = 0; i < indexe.length; i++) {
        grida.setTekstQelize('txtProjekti', indexe[i], per);
        grida.setTekstQelize('txtIdProjekti', indexe[i], idprojekti);
    }
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheArtikullPlanifikimi"),
        data: JSON.stringify({ idja: idprojekti, rreshti: idRresht })
    }).done(SucceededCallbackProduktiplanGroup);
}

/*
Function: ndryshoKonfigurimin
    
Therret funksionin <callWebserviceKonfigurimi> per te vendosur nje konfigurim te ri.
*/
function ndryshoKonfigurimin() {
    var pershkKonfigAmb = cmbKonfigurimi.GetSelectedItem().GetColumnText("PershkrimKonfigAmbjente");
    if (pershkKonfigAmb != undefined)
        $('#kokeKonfigurimi').text(hfState.Get("MenuKokeDokumenti") + ': ' + pershkKonfigAmb);
    callWebserviceKonfigurimi(809, cmbKonfigurimi.GetSelectedItem().GetColumnText("KodKonfigAmbjente"));
}

/*
Function: SucceededCallbackNiveli
    
Ndryshon vleren e zgjedhur te konfigurimit sipas nivelit te zgjedhur.textc
Therret funksionin <ndryshoKonfigurimin>.
*/
function ButtonClickBurimi() {

    var hfKl = document.getElementById("hfLupaMagazina");
    var queryStr = hfKl.value;
    popupUniversal.SetHeaderText("Zgjidhni burimin");
    popupUniversal.SetContentUrl('LupaBurime.aspx?vjenNga=SkedulimProdhimi&idKonfigAmbjente=' + queryStr);
    popupUniversal.SetSize(widthLupaMagazina, heightLupaMagazina);
    popupUniversal.Show();
}
/*
Function: ButtonClickFurnitori
    
Hap lupen e klienteve/furnitoreve.
*/
function ButtonClickProjekti() {
    var hfKl = document.getElementById("hfLupaKlientFurnitor");
    var queryStr = hfKl.value;
    popupUniversal.SetHeaderText("Zgjidhni Planifikimin");
    popupUniversal.SetContentUrl('LupaPlanifikime.aspx?vjenNga=SkedulimProdhimi&idKonfigAmbjente=' + queryStr);
    popupUniversal.SetSize(widthLupaKF, heightLupaKF);
    popupUniversal.Show();
}


/*
Function: TextChangedBurimi
    
Vendos ne gride burimin qe zgjidhet te koka
*/
function TextChangedBurimi() {
    var per = btnBurimi.GetText();
    if (Utils.IsNullOrWhiteSpace(per))
        return;

    var idburimi = btnBurimi.GetValue();
    var grida = $("#rowed5");
    var idRow = grida.getLastSel2();
    var indexe = grida.getDataIDs();
    for (i = 0; i < indexe.length; i++) {
        grida.setTekstQelize('txtBurimi', indexe[i], per);
    }
   $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "KtheVleraBurimeMeIDRow"),
        data: JSON.stringify({ idja: idburimi, rreshti: idRow })
    }).done(SucceededCallbackBurimiall);
}

function SucceededCallbackBurimiall(result) {
    var grida = $("#rowed5");
    var indexe = grida.getDataIDs();
    for (i = 0; i < indexe.length; i++) {
        result[0] = indexe[i];
        vendosBurim(result);
    }
    $('#hfPershkrimBurimi').val(result[1].Emertimi);
    $('#hfKosto').val(result[1].KostoPlan);
}

function SucceededCallbackProduktiplanGroup(result) {
    if (result[1] == null || (result[1] != null && result[1].IdArtikulli == 0))
        return;

    var grida = $("#rowed5");
    var indexe = grida.getDataIDs();
    for (i = 0; i < indexe.length; i++) {
        result[0] = indexe[i];
        vendosArt(result);
    }
    $('#hfKodProdukti').val(result[1].KodArtikulli);
    $('#hfPershkrimProdukti').val(result[1].PershkrimArtikulli);
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

    trupiBosh = true; var
    panjesi = false;

    var idTe = grida.jqGrid('getDataIDs');
    for (var i = 0; i < idTe.length; i++) {

        editorKodi = grida.getTekstQelize('txtBurimi', idTe[i]);   
        editorProjekti = grida.getTekstQelize('txtProjekti', idTe[i]); 
        editorProdukti = grida.getTekstQelize('txtProdukti', idTe[i]);
        editorAktivieteti = grida.getTekstQelize('txtAktiviteti', idTe[i]);
        editorEmertimi = grida.getTekstQelize('txtEmertimiB', idTe[i]);
        editorNjesia = grida.getTekstQelize('txtNjesia', idTe[i]);
        editorSasia = grida.getTekstQelize('txtKoha', idTe[i]);

        if (editorKodi != "" && editorProjekti!="" &&  editorProdukti!="" ) { trupiBosh = false; }


        if (editorKodi != "" && editorProjekti != "" && editorProdukti != "" && editorAktivieteti != "" && editorSasia == 0) {
            e.processOnServer = false; click = false;
            myMesazh.ShtoMesazhGabimi('Koha nuk mund te jete zero');
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
    var hfKod = document.getElementById("hfGridaKodi");
    var queryStr = hfKod.value;

    popupUniversal.SetHeaderText("Zgjidhni burimin");
    popupUniversal.SetContentUrl('LupaBurime.aspx?vjenNga=SkedulimProdhimiGrida&idKonfigAmbjente=' + queryStr);
    popupUniversal.SetSize(widthLupaMagazina, heightLupaMagazina);
    popupUniversal.Show();

}


function changedSasia() {//po
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    editorKodi = grida.getTekstQelize('txtBurimi', idRresht);
    editorSasia = grida.getTekstQelize('txtKoha', idRresht);
    editorSasiaMbetur = grida.getTekstQelize('txtKosto', idRresht);
    editorSasiDisp = grida.getTekstQelize('txtKostoTotale', idRresht);
    var sasia;

    if (editorSasia == '.') {
        grida.setTekstQelize('txtKoha', idRresht, '0.');
        return;
    }
    if (editorSasia == "" || isNaN(editorSasia)) {
        myMesazh.ShtoMesazhGabimi('Koha duhet te jete numer');
        sasia = grida.getVlereDefault('txtKoha');
        //grida.setTekstQelize('txtSasia', lastsel2, sasia);
        $('#txtKoha' + idRresht).focus();
    }
    else
        if (editorSasia == "0") {
            myMesazh.ShtoMesazhGabimi("Koha nuk mund te jete zero");
            sasia = grida.getVlereDefault('txtKoha');
            //grida.setTekstQelize('txtSasia', lastsel2, sasia);
            $('#txtKoha' + idRresht).focus();
        }

    if (sasia == null)
        sasia = parseFloat(editorSasia);
    Llogaritkohen();
}

function focusoutSasia() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var sasia = grida.getTekstQelize('txtKoha', idRresht);
    if (sasia === '' || isNaN(sasia) || sasia === 0 || sasia === '0')
        grida.setTekstQelize('txtKoha', idRresht);
}

function vendosNrAutomatik(colAtrTrupi, colKontrollet) {
    myNrAuto.vendosNrAutomatik(colAtrTrupi, colKontrollet, dteDtDok.GetDate());
}

/*
Function: pastroFushatKokes
    
Pastron fushat pasi eshte bere ruajtja apo modifikimi per te bere gati ambjentin per shtim.
*/
function pastroFushatKokes() {

    btnProjekti.SetValue(null);
    btnBurimi.SetValue(null);
    $('#hfPrioriteti').val('');
    $('#hfPershkrimBurimi').val('');
    $('#hfKosto').val('');
    $('#hfPershkrimProdukti').val('');
    $('#hfKodProdukti').val('');
 
    txtNrDok.SetText('');
    txtShenime.SetText('');

    var hf = document.getElementById("status1");
    hf.value = "false";
    vendosDateDefault();
    pyeturDoni = 0;
}


var dtDokumentit;

function vendosDateDefault() {
    if ($("input[id$='hfShtimModifikim']").val() == "shtim"||$("input[id$='hfShtimModifikim']").val() == "klonim") {
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
   if( !Utils.nrWsRrugesManager.kanePerfunduarWs()) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgPoTransferohetTeDhenatShtypniPerseriRuaj"));
        return false;
    }
    if (txtNrDok.GetText() == "") {
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
    var hf = document.getElementById("status1");

    if (hf.value == "true") {

        myFaqeCelje.kontrolloTeDrejta('Shto_SkedulimProdhimi.aspx?shtim_modifikim=shtim', true);

    } else click = false;
}
var shtoTimer;
function shtoTimedClick(e) {
    if (!Utils.nrWsRrugesManager.kanePerfunduarWs())
        shtoTimer = setTimeout(function () { shtoTimedClick(e) }, 500);
    else {
        clearTimeout(shtoTimer);
        myFaqeCelje.kontrolloTeDrejta('Shto_SkedulimProdhimi.aspx?shtim_modifikim=shtim', true);
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
    else if (e.item.name === 'Klono') {
        if ($("input[id$='hfAutorizimi']").val() == 'False') {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukKeniAutorizimKlonim"));
            Utils.hiqLoadingGif();;
            e.processOnServer = false;
            click = false;
            return;
        }
        KlonoClick(e);
    }
    else if (e.item.name == 'Kerko') {
        myFaqeCelje.kontrolloTeDrejta('SkedulimProdhimi.aspx', null, true);
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
        myFaqeCelje.kontrolloTeDrejta('SkedulimProdhimi.aspx');
        e.processOnServer = false;
        click = false;
    }

}
function KlonoClick(e) {
    var hf1 = $("#hfShtimModifikim");
    $("input[id$='hfLidhur']").val(false);
    hf1.val("klonim");
 
    try {
        
        ASPxMenu1.GetItemByName('Klono').SetVisible(false);
        ASPxMenu1.GetItemByName('Draft').SetVisible(true);
      
    }
    catch (ex) {
    }
    ASPxMenu1.GetItemByName('Fshi').SetVisible(false);
    btnProjekti.SetText('');
    $('#hfPrioriteti').val('');
    $('#hfPershkrimProdukti').val('');
    $('#hfKodProdukti').val('');
    var per ='';
    var idprojekti =0;
    var grida = $("#rowed5");
    var indexe = grida.getDataIDs();
    for (i = 0; i < indexe.length; i++) {
        grida.setTekstQelize('txtProjekti', indexe[i], per);
        grida.setTekstQelize('txtIdProjekti', indexe[i], idprojekti);
    }
    myMenu.menuSipasTeDrejtaRegjistrim(hf1, hfTeDrejta);
   
    $("#ASPxSplitter1_hl").empty();
    $('#ASPxSplitter1_tblKonfigurimi>tbody>tr:eq(0)>td:eq(0)').empty();
    e.processOnServer = false;
    ndryshoKonfigurimin();
    click = false;
}
function Kthehu(mesazh) {
    myFaqeCelje.kontrolloTeDrejta('SkedulimProdhimi.aspx?ruaj=po');

}
var click = false;
/*
Function: RuajClick

Validon dokumentin dhe therret funksionin <merrTeDhena> per te marre te dhenat e grides kur klikohet butoni Ruaj.
*/

function RuajClick(s, e) {
    var grida = $("#rowed5");
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
    ASPxMenu1.GetItemByName('Fshi').SetVisible(false); ASPxMenu1.GetItemByName('Klono').SetVisible(false);
    ASPxMenu1.GetItemByName('Draft').SetVisible(true);
    hf.val("shtim"); myMenu.menuSipasTeDrejtaRegjistrim(hf, hfTeDrejta);
    ndryshoKonfigurimin();

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
