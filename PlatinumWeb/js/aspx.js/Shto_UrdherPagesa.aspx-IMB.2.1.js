;
var lidhur = false;
var arrayIdKolonaGrides = new Array();
var arrayPershkrimiKolonaGrides = new Array();
var arrayVisibleKolonaGrides = new Array();
var arrayWidthKolonaGrides = new Array();
var arrayReadOnlyKolonaGrides = new Array();
var arrayRenditjeKolonaGrides = new Array();
var arrayIndexTrupi = new Array();
var widthLupa = 750;
var heightLupa = 600;
var widthLupaKerko = 850;
var heightLupaKerko = 600;

var formatNumriZgjedhur;
var formatKursi;
var vjenNgaKokeApoTrup = false;

var varKonfig = {
    identifikuesPerLocalStorageKey: 'UrdherPagesa',
    kontrolloPerKategoriShpenzimi: false
};

jQuery(document).ready(function () {
    /*
Function:
ekzekutohet sa here i behet resize faqes, dhe ben resize te grides
*/
    Utils.konfiguroAccorditionNeDocReady(varKonfig.identifikuesPerLocalStorageKey);
    $(window).on('resize', function () {

        try {
            if (Utils.isGridResized())//if (!Utils.resizeSplitter(170))
                return;
        }
        catch (ee) {
        }
        var grida = $("#rowed5");
        if ($('#divgride2').width() !== null) {
            grida.setGridWidth($('#divgride2').width() - 5, true);
        }
    }).trigger('resize');

    $(window).on('load', function () {
      formGridColsArray();
      inicializoGride();
      mbushGrideNgaHiddenFieldet();
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



function Init() {

    if (typeof (isPostBack) === "undefined") {
        document.getElementById("kokeKonfigurimi").innerHTML = hfState.Get("MenuKokeDokumenti");
        document.getElementById("trupKonfigurimi").innerHTML = hfState.Get("MenuTrupDokumenti");
        document.getElementById("fundKonfigurimi").innerHTML = hfState.Get("MenuFundDokumenti");
        var hf = $("#hfKonffillestar");
        cmbKonfigurimi.SetText(hf.val());
        ndryshoKonfigurimin();
        identifikuesPerPopupDokumentat = "UrdherPagesa";
        changeName();
        vendosDateDefault();
        myMesazh.shtoHandler();
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(EndRequestHandler);
    }
}

/*
Function: inicializoGride

Inicializon griden e trupit. Konfiguron kolonat e grides dhe percakton veprimin qe kryhet onCellSelect.
*/
function inicializoGride() {
    var classes = '';
    if (lidhur === true && $("input[id$='hfShtimModifikim']").val() === 'modifikim')
        classes = 'uigray';
    var arrayPershkrime = [arrayPershkrimiKolonaGrides[0], arrayPershkrimiKolonaGrides[1], arrayPershkrimiKolonaGrides[2], arrayPershkrimiKolonaGrides[3], arrayPershkrimiKolonaGrides[4], arrayPershkrimiKolonaGrides[5], arrayPershkrimiKolonaGrides[6], arrayPershkrimiKolonaGrides[7], arrayPershkrimiKolonaGrides[8]];
    var arrayModel = [
               { name: arrayIdKolonaGrides[0], index: arrayIdKolonaGrides[0], width: arrayWidthKolonaGrides[0], hidden: arrayVisibleKolonaGrides[0], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myElemGrupi, custom_value: myJQGrid.myValueTextBox } },
                { name: arrayIdKolonaGrides[1], index: arrayIdKolonaGrides[1], width: arrayWidthKolonaGrides[1], hidden: arrayVisibleKolonaGrides[1], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myElemKapitulli, custom_value: myJQGrid.myValueTextBox } },
                { name: arrayIdKolonaGrides[2], index: arrayIdKolonaGrides[2], width: arrayWidthKolonaGrides[2], hidden: arrayVisibleKolonaGrides[2], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myElemTitulli, custom_value: myJQGrid.myValueTextBox } },
                { name: arrayIdKolonaGrides[3], index: arrayIdKolonaGrides[3], width: arrayWidthKolonaGrides[3], hidden: arrayVisibleKolonaGrides[3], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemArtikulli, custom_value: myJQGrid.myValueTextBox } },
                { name: arrayIdKolonaGrides[4], index: arrayIdKolonaGrides[4], width: arrayWidthKolonaGrides[4], hidden: arrayVisibleKolonaGrides[4], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemAnalize, custom_value: myJQGrid.myValueTextBox } },
                { name: arrayIdKolonaGrides[5], index: arrayIdKolonaGrides[5], width: arrayWidthKolonaGrides[5], hidden: arrayVisibleKolonaGrides[5], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemKodProjekti, custom_value: myJQGrid.myValueTextBox } },
                { name: arrayIdKolonaGrides[6], index: arrayIdKolonaGrides[6], width: arrayWidthKolonaGrides[6], hidden: arrayVisibleKolonaGrides[6], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemShuma, custom_value: myJQGrid.myValueTextBox } },
                { name: arrayIdKolonaGrides[7], index: arrayIdKolonaGrides[7], width: arrayWidthKolonaGrides[7], hidden: arrayVisibleKolonaGrides[7], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemObjekti, custom_value: myJQGrid.myValueTextBox } },
                { name: arrayIdKolonaGrides[8], index: arrayIdKolonaGrides[8], width: arrayWidthKolonaGrides[8], hidden: arrayVisibleKolonaGrides[8], classes: classes, sorttype: "int", editable: true, edittype: 'custom', editoptions: { custom_element: myElemButtonFshi, custom_value: myValueButtonFshi }, hidedlg: true }
    ];

    if ($("input[id$='hfLidhur']")[0].value === 'True')
        lidhur = true;
    else
        lidhur = false;

    var selektoriGrides = "#rowed5";

    var gridParams = {
        emergride: selektoriGrides,
        arrayPershkrime: arrayPershkrime,
        arrayModel: arrayModel,
        lidhur: lidhur,
        emerEditorKodi: "txtGrupi",
        widthi: $('#divgride2').width() - 5,
        subgrid: false,
        lostFocusKoloneFundit: lostFocusKoloneFundit,

        autocompleteList: [{ emerEditor: "txtGrupi", selectFunc: selectFunc, changeFunc: changeFunc, shtoDataKod: false },
        { emerEditor: 'txtTitulli', selectFunc: selectFunc2, changeFunc: changeFunc2, shtoDataKod: false },
        { emerEditor: "txtKapitulli", selectFunc: selectFunc3, changeFunc: changeFunc3, shtoDataKod: false },
        { emerEditor: "txtArtikulli", selectFunc: selectFunc4, changeFunc: changeFunc4, shtoDataKod: false },
          { emerEditor: "txtAnalize", selectFunc: selectFunc5, changeFunc: changeFunc5, shtoDataKod: false }],

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
    grida.setVlereDefault('txtShuma', 0);
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
    grida.setShifraPasPresjes('txtShuma', formatNumri.ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtVlefta, formatNumri.ShifraPasPresjesVlefta);
}

function dateGotFocus(s, e) {
    window.setTimeout(function () { s.SetCaretPosition(0); }, 0);
}

/*
Formaton vlerat e fushave sipas formatit perkates ne te gjithe rreshtat e grides
*/
function vendosKonfigFormatNumri() {
    var grida = $('#rowed5');
    var idTe = grida.jqGrid('getDataIDs');
    for (j = 0; j < idTe.length; j++) {
        var idRreshti = idTe[j];
        grida.formatoQelize('txtShuma', idRreshti);
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
    var idPerdoruesi = hfState.Get('idPerdoruesi');
    var idViti = hfState.Get('idViti');
    myJQGrid.ruajKolonatEGrides(grida, idGride, idGjuha, idNdermarrje, idViti, idPerdoruesi);
}

function formGridColsArray() {//po
    var IdKonfigAmbjenteLupat = [{ hfVar: $('#hfGridaGrupi'), kodiText: "txtGrupi" }, { hfVar: $('#hfGridaTitulli'), kodiText: "txtTitulli" }
            , { hfVar: $('#hfGridaKapitulli'), kodiText: "txtKapitulli" }, { hfVar: $('#hfGridaAnaliza'), kodiText: "txtAnalize" }
            , { hfVar: $('#hfGridaArtikulli'), kodiText: "txtArtikulli" }];
    myJQGrid.formArrayKolGrides($("input[id$='HfGridCol']"), arrayIdKolonaGrides, arrayPershkrimiKolonaGrides, arrayVisibleKolonaGrides, arrayReadOnlyKolonaGrides, lidhur, arrayWidthKolonaGrides, IdKonfigAmbjenteLupat, arrayRenditjeKolonaGrides);
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
function myElemGrupi(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var grupi = btneGrupi.GetText();
    if (value == "" && grupi)
        value = grupi;
    return myJQGrid.myElemKodi(value, options, arrayReadOnlyKolonaGrides[0], idRresht, arrayIdKolonaGrides[0], ButtonClickGrupi, keyPressGrupi, changeFunc);
}

function myElemTitulli(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var titulli = btneTitulli.GetText();
    if (value == "" && titulli)
        value = titulli;
    return myJQGrid.myElemKodi(value, options, arrayReadOnlyKolonaGrides[2], idRresht, arrayIdKolonaGrides[2], ButtonClickTitulli, keyPressTitulli, changeFunc2);
}

function myElemKapitulli(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var kapitulli = btneKapitulli.GetText();
    if (value == "" && kapitulli)
        value = kapitulli;
    return myJQGrid.myElemKodi(value, options, arrayReadOnlyKolonaGrides[1], idRresht, arrayIdKolonaGrides[1], ButtonClickKapitulli, keyPressKapitulli, changeFunc3);
}

function myElemAnalize(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemKodi(value, options, arrayReadOnlyKolonaGrides[4], idRresht, arrayIdKolonaGrides[4], ButtonClickAnalize, keyPressAnalize, changeFunc5);
}

function myElemArtikulli(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var artikulli = btneArtikulli.GetText();
    if (value == "" && artikulli)
        value = artikulli;
    return myJQGrid.myElemKodi(value, options, arrayReadOnlyKolonaGrides[3], idRresht, arrayIdKolonaGrides[3], ButtonClickArtikulli, keyPressArtikulli, changeFunc4);
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

/*
Function: myElemEmertimi

Nderton nje textbox ku vendoset emertimi i artikullit apo makros
*/
function myElemKodProjekti(value) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemEmertimi(value, arrayReadOnlyKolonaGrides[5], idRresht, 'txtKodProjekti')
}

function myElemObjekti(value) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemEmertimi(value, arrayReadOnlyKolonaGrides[7], idRresht, 'txtObjekti')
}

function myElemShuma(value, options) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, arrayReadOnlyKolonaGrides[6], idRresht, 'txtShuma', vendosTotalet);
    //return myJQGrid.myElemTextBoxVlefte(value, options, arrayReadOnlyKolonaGrides[6], lastsel2, 'txtShuma', vendosTotalet, '0.00');
}


/*
Function: fshiClicked

Fshin nje rresht te grides

Parameters:

index - Id e rreshtit qe do fshihet
*/
function fshiClicked(index) {
    myJQGrid.fshiClicked(index, "#rowed5", inicializoGride);
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

function mbushGrideNgaHiddenFieldet() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var veprimi = $('#hfShtimModifikim').val();
    if (veprimi === "shtim") {
        return;
    }
    if (veprimi === "modifikim" || veprimi === "klonim") {
        var colTrupi = JSON.parse($('#HfColTrupMag').val());
        var colKapituj = JSON.parse($('#HfColArt').val());
        var colGrupet = JSON.parse($('#HfColNjesiArt').val());
        var colTitujt = JSON.parse($('#HfColDetArt').val());
        var colArtikuj = JSON.parse($('#HfColNjesAdminis').val());
        var colAnaliza = JSON.parse($('#HfColNjesAdminisDest').val());
        grida.setLastSel2(1);
        idRresht = 1;
        grida.jqGrid('clearGridData');
        var grupi, kapitulli, titulli, artikulli, analiza, kod, shuma, objekti;
    }
    if (veprimi == "klonim")
        txtNrDok.SetEnabled(true);
    for (var i = 0; i < colTrupi.length; i++) {
        if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides - 1] === 'True' || lidhur)
            var be = myJQGrid.myValueButtonFshi(true, idRresht, "#rowed5");
        else
            be = myJQGrid.myValueButtonFshi(false, idRresht, "#rowed5");
        grupi = colGrupet[i].Kodi;
        kapitulli = colKapituj[i].Kodi;
        titulli = colTitujt[i].Kodi;
        artikulli = colArtikuj[i].NrLlogari;
        analiza = colAnaliza[i].NrLlogari;
        shuma = colTrupi[i].Shuma; //.toFixed(2);
        kod = colTrupi[i].KodProjekti;
        objekti = colTrupi[i].Objekti;

        var datarow = { txtGrupi: grupi, txtKapitulli: kapitulli, txtTitulli: titulli, txtArtikulli: artikulli, txtAnalize: analiza, txtKodProjekti: kod, txtShuma: shuma, txtObjekti: objekti, txtFshi: be };
        grida.addRowData(parseInt(idRresht), datarow);
        idRresht = idRresht + 1;
        grida.setLastSel2(idRresht);
    }
}

/*
Function: ndryshoKonfigurimin

Therret funksionin <callWebserviceKonfigurimi> per te vendosur nje konfigurim te ri.
*/
function ndryshoKonfigurimin() {//po
    //if (cmbKonfigurimi.GetText().split(';').length > 1)
    //    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    //cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);
    if (cmbKonfigurimi.GetSelectedItem().texts != null && cmbKonfigurimi.GetSelectedItem().texts.length > 1)
        $('#kokeKonfigurimi').text(hfState.Get("MenuKokeDokumenti") + ': ' +cmbKonfigurimi.GetSelectedItem().texts[1] );
       // lblKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().texts[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().texts[0]);
    callWebserviceKonfigurimi(313, cmbKonfigurimi.GetText());
}

var identifikuesPerPopupDokumentat;

function changeName() {//po
    myFaqeCelje.shtoHandlerSession();
    try {
        if (Utils.getUrlVar('id') == undefined)
            window.parent.callWebServiceKtheInfoLart('Shto_UrdherPagesa.aspx', 0);
        else
            window.parent.callWebServiceKtheInfoLart('Shto_UrdherPagesa.aspx', Utils.getUrlVar('id'));

    } catch (e) { }
    myCookies.createCookie('adresa', window.location.href, 0, 1);
}

function vendosNrAutomatik(colAtrTrupi, colKontrollet) {//po
    myNrAuto.vendosNrAutomatik(colAtrTrupi, colKontrollet, dteDtDok.GetDate());
}

/*
Function: ButtonClickKerko

Hap lupen e dokumentave.
*/
function ButtonClickKerko(listUrl) {//po

    var queryString = {
        veprimi: 'UrdherPagesa',
        listUrl: listUrl
    };
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhDokumentin"), 'LupaDokumenta.aspx?' + Utils.KonvertoObjectQueryString(queryString), 950, 560);
}

/*
Function: callWebserviceKonfigurimi

Therret funksionin <ktheKonfigurim> per te vendosur konfigurimin per dokumentin.
Shiko funksionin <SucceededCallbackKonfigurimi>.
*/
function callWebserviceKonfigurimi(idKomp, kodKonf) {//po
    var idGjuha = hfState.Get('idGjuha');
    var idNdermarrje = hfState.Get('idNdermarrje');
    var idPerdoruesi = hfState.Get('idPerdoruesi');
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfigAmbjentiMeFormatNumrash"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, kodKontrolli: '', idObjekti: -1, shtim: false, merrFormatKursi: true, merrGjitheKonf: false, idGjuha: idGjuha, idPerdoruesi: idPerdoruesi, idNdermarrje: idNdermarrje })
    }).done(SucceededCallbackKonfig);
}

function DateChanged(s, e) {//po
    var hfKontrollet = JSON.parse($('#hfKontrolletNrAutom').val());
    var atributet = JSON.parse($('#hfAtributeNrAutom').val());
    var veprimi = $('#hfShtimModifikim').val();
    if (veprimi != 'modifikim')
        vendosNrAutomatik(atributet, hfKontrollet);
}

var colKushte; var colAlterKusht;
var colGrida;
function SucceededCallbackKonfig(result) {
    var hf = $("input[id$='hfShtimModifikim']");
    var hfLidhur = $("input[id$='hfLidhur']");
    var arrTabela = ['tblFillim', "tblFund"];
    var arrPrind = ["dvFillim", "dvFundi"];
    var colKontrollet = result.colKontroll;  //[0];
    var colAtrTrupi = result.colAtrTrupi;  //[1];
    formatNumriZgjedhur = result.formatNumri; //[7];
    formatKursi = result.formatKursi;  //[8];
    myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, undefined, hf, '', arrTabela, undefined, undefined, hfLidhur, arrPrind);
    $("#divgride1").show();//$("#divgride1")[0].style.visibility = 'visible';
    $("#dvFillim").show();//$("#dvFillim")[0].style.visibility = 'visible';
    //$("#dvFillim")[0].style.display = '';
    $("#dvFundi").show();//$("#dvFundi")[0].style.visibility = 'visible';
    //$("#dvFundi")[0].style.display = '';
    var colKontrollet = result.colKontroll; //[0];
    var colAtrTrupi = result.colAtrTrupi;  //[1];
    colGrida = result.colGrida;  //[2];
    colKushte = result.colKushte;   //[3];
    colAlterKusht = result.colAlterKusht;  //[4];
    $('#HfGridCol').val(JSON.stringify(colGrida));
    $('#hfKontrolletNrAutom').val(JSON.stringify(colKontrollet));
    $('#hfAtributeNrAutom').val(JSON.stringify(colAtrTrupi));
    if (hf.val() === "shtim" || hf.val() === "klonim") {
        hfNrAuto.Clear();
        hfNrAutoShitje.Clear();
        vendosNrAutomatik(colAtrTrupi, colKontrollet);
    }
    formGridColsArray();
    var grida = $('#rowed5');
    grida.setLastSel2(-1);
    grida.GridUnload("rowed5");
    grida = $('#rowed5');
    ruajFormatetNeGride(grida, formatNumriZgjedhur);
    inicializoGride();
    mbushGrideNgaHiddenFieldet();
    Utils.konfiguroAccorditionPasKonfigDokumenti(varKonfig.identifikuesPerLocalStorageKey);
    varKonfig.kontrolloPerKategoriShpenzimi = kushtiAKSH();
}


/*
Function: merrTeDhena

Merr te dhenat qe ka grida dhe i vendos neper hidden field-e per ti perdorur ne server side
*/
function merrTeDhena(e) {
    var grida = jQuery("#rowed5");
    trupiBosh = true;
    var rreshtaTeGrides = grida.getRowData();
    var tmp2 = new Array();
    var k = 0;
    for (i = 0; i < rreshtaTeGrides.length; i++) {
        editorKodi = rreshtaTeGrides[i].txtGrupi;
        if (editorKodi !== "") {
            trupiBosh = false;
            tmp2[k] = rreshtaTeGrides[i];
            k++;
        }
    }
    for (var i = 0; i < tmp2.length; i++) {
        tmp2[i].txtFshi = "";
    }
    $('#gridDataObject').val(JSON.stringify(tmp2));
    unformatoFushaDevi();
}

/*
Function: pastro

Pastrin array-t e perdorura dhe vendos counterat ne 0.
*/
function pastro() {


}
/*
Function: ButtonClickKodi

Hap lupen e artikujve apo makrove sipas zgjedhjes qe eshte bere te kategoria.
*/
function ButtonClickGrupi() {
    vjenNgaKokeApoTrup = false;
    var hfKod = $("#hfGridaGrupi");
    var queryStr = hfKod.val();
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgKodifikimArtikullZgjidhGrup"), 'LupaKonfigUrdherPagesa.aspx?vjenNga=Grupi&idkonfigambjente=' + queryStr, widthLupa, heightLupa);

}
function ButtonClickTitulli() {
    vjenNgaKokeApoTrup = false;
    var hfKod = $("#hfGridaTitulli");
    var queryStr = hfKod.val();
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhniTitullin"), 'LupaKonfigUrdherPagesa.aspx?vjenNga=Titulli&idkonfigambjente=' + queryStr, widthLupa, heightLupa);

}
function ButtonClickKapitulli() {
    vjenNgaKokeApoTrup = false;
    var hfKod = $("#hfGridaKapitulli");
    var queryStr = hfKod.val();
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhniKapitullin"), 'LupaKonfigUrdherPagesa.aspx?vjenNga=Kapitulli&idkonfigambjente=' + queryStr, widthLupa, heightLupa);

}
function ButtonClickArtikulli() {
    vjenNgaKokeApoTrup = false;
    var hfKod = $("#hfGridaArtikulli");
    var queryStr = hfKod.val();
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhniLlogarine"), 'LupaLlogaria.aspx?vjennga=LlogArt&idkonfigambjente=' + queryStr, widthLupa, heightLupa);

}
function ButtonClickAnalize() {
    var hfKod = $("#hfGridaAnaliza");
    var queryStr = hfKod.val();
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhniLlogarine"), 'LupaLlogaria.aspx?vjennga=Analiza&idkonfigambjente=' + queryStr, widthLupa, heightLupa);

}


function keyPressGrupi() {
    callWebserviceGrupi();

}
function keyPressTitulli() {
    callWebserviceTitulli();

}
function keyPressKapitulli() {
    callWebserviceKapitulli();

}
function keyPressArtikulli() {
    callWebserviceArtikulli();

}
function keyPressAnalize() {
    callWebserviceAnaliza();

}
function callWebserviceGrupi() {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    var vlera = $('#txtGrupi' + idRow).val();
    $.ajax({
        url: Utils.getServerApiUrl("ListPagesa", "ktheACListeKonfigurimeshUrdherPagese"),
        data: JSON.stringify({ infixText: vlera, lloj: 1, idNdermarrje: hfState.Get('idNdermarrje') })
    }).done(SucceededCallbackGrupi);
}
function callWebserviceTitulli() {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    var vlera = $('#txtTitulli' + idRow).val();
    $.ajax({
        url: Utils.getServerApiUrl("ListPagesa", "ktheACListeKonfigurimeshUrdherPagese"),
        data: JSON.stringify({ infixText: vlera, lloj: 2, idNdermarrje: hfState.Get('idNdermarrje') })
    }).done(SucceededCallbackTitulli);
}
function callWebserviceKapitulli() {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    var vlera = $('#txtKapitulli' + idRow).val();
    $.ajax({
        url: Utils.getServerApiUrl("ListPagesa", "ktheACListeKonfigurimeshUrdherPagese"),
        data: JSON.stringify({ infixText: vlera, lloj: 3, idNdermarrje: hfState.Get('idNdermarrje') })
    }).done(SucceededCallbackKapitulli);
}
function callWebserviceArtikulli() {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    var vlera = $('#txtArtikulli' + idRow).val();
    $.ajax({
        url: Utils.getServerApiUrl("ListPagesa", "ktheACListeLlogarishBeginWith"),
        data: JSON.stringify({ infixText: vlera, idPerdoruesi: hfState.Get('idPerdoruesi'), idNderrmarje: hfState.Get('idNdermarrje') })
    }).done(SucceededCallbackArtikulli);

}
function callWebserviceAnaliza() {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    var vlera = $('#txtAnalize' + idRow).val();
    $.ajax({
        url: Utils.getServerApiUrl("ListPagesa", "ktheACListeLlogarishBeginWith"),
        data: JSON.stringify({ infixText: vlera, idPerdoruesi: hfState.Get('idPerdoruesi'), idNderrmarje: hfState.Get('idNdermarrje') })
    }).done(SucceededCallbackAnaliza);
}
function SucceededCallbackGrupi(result) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    myJQGrid.SucceededCallbackKodi(result, '#txtGrupi' + idRow);
}
function SucceededCallbackTitulli(result) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    myJQGrid.SucceededCallbackKodi(result, '#txtTitulli' + idRow);
}
function SucceededCallbackKapitulli(result) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    myJQGrid.SucceededCallbackKodi(result, '#txtKapitulli' + idRow);
}
function SucceededCallbackArtikulli(result) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    myJQGrid.SucceededCallbackKodi(result, '#txtArtikulli' + idRow);
}
function SucceededCallbackAnaliza(result) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    myJQGrid.SucceededCallbackKodi(result, '#txtAnalize' + idRow);
}
function selectFunc(event, ui, emerfushe, idArt, kodArt) {
    if (idArt != undefined && kodArt != undefined && idArt != "" && kodArt != "") {
        $(emerfushe).val(kodArt);
        KtheVleraArt(idArt);
        return false;
    }
    if (ui.item != null) {
        $(emerfushe).val(ui.item.label);
    }
    KtheVleraArt(ui.item.value);
    return false;
}
function selectFunc2(event, ui, emerfushe, idArt, kodArt) {
    if (idArt != undefined && kodArt != undefined && idArt != "" && kodArt != "") {
        $(emerfushe).val(kodArt);
        KtheVleraArtTitulli(idArt);
        return false;
    }
    if (ui.item != null) {
        $(emerfushe).val(ui.item.label);
    }
    KtheVleraArtTitulli(ui.item.value);
    return false;
}
function selectFunc3(event, ui, emerfushe, idArt, kodArt) {
    if (idArt != undefined && kodArt != undefined && idArt != "" && kodArt != "") {
        $(emerfushe).val(kodArt);
        KtheVleraArtKapitulli(idArt);
        return false;
    }
    if (ui.item != null) {
        $(emerfushe).val(ui.item.label);
    }
    KtheVleraArtKapitulli(ui.item.value);
    return false;
}
function selectFunc4(event, ui, emerfushe, idArt, kodArt) {
    if (idArt != undefined && kodArt != undefined && idArt != "" && kodArt != "") {
        $(emerfushe).val(kodArt);
        KtheVleraArtArtikulli(idArt);
        return false;
    }
    if (ui.item != null) {
        $(emerfushe).val(ui.item.label);
    }
    KtheVleraArtArtikulli(ui.item.value);
    return false;
}
function selectFunc5(event, ui, emerfushe, idArt, kodArt) {
    if (idArt != undefined && kodArt != undefined && idArt != "" && kodArt != "") {
        $(emerfushe).val(kodArt);
        KtheVleraArtAnaliza(idArt);
        return false;
    }
    if (ui.item != null) {
        $(emerfushe).val(ui.item.label);
    }
    KtheVleraArtAnaliza(ui.item.value);
    return false;
}
function changeFunc(event, ui, emerKodi, index) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    var emerfushe = '#' + emerKodi + idRow;
    if (ui == null || ui.item == null) {
        if ($(emerfushe).val() != "") {
            KtheVleraArtMeKod($(emerfushe).val());
            return;
        }
    }
}

function changeFunc2(event, ui, emerKodi, index) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    var emerfushe = emerKodi + idRow;
    if (ui == null || ui.item == null) {
        if ($(emerfushe).val() != "") {
            KtheVleraArtMeKodTitulli($(emerfushe).val());
            return;
        }
    }
}

function changeFunc3(event, ui, emerKodi, index) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    var emerfushe = '#' + emerKodi + idRow;
    if (ui == null || ui.item == null) {
        if ($(emerfushe).val() != "") {
            KtheVleraArtMeKodKapitulli($(emerfushe).val());
            return;
        }
    }
}
function changeFunc4(event, ui, emerKodi, index) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    var emerfushe = '#' + emerKodi + idRow;
    if (ui == null || ui.item == null) {
        if ($(emerfushe).val() != "") {
            KtheVleraArtMeKodArtikulli($(emerfushe).val());
            return;
        }
    }
}
function changeFunc5(event, ui, emerKodi, index) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    var emerfushe = emerKodi + idRow;
    if (ui == null || ui.item == null) {
        if ($(emerfushe).val() != "") {
            KtheVleraArtMeKodAnaliza($(emerfushe).val());
            return;
        }
    }
}
function KtheVleraArt(idja) {
    $.ajax({
        url: Utils.getServerApiUrl("ListPagesa", "ktheVleraKonfigMeID"),
        data: JSON.stringify({ idja: idja })
    }).done(SucceededCallbackArt);
}
function KtheVleraArtMeKod(kodi) {
    if (kodi != undefined && typeof (kodi) != undefined && kodi != "") {
        $.ajax({
            url: Utils.getServerApiUrl("ListPagesa", "ktheVleraKonfigMeKod"),
            data: JSON.stringify({ kodi: kodi, lloji: 1, idNderrmarje: hfState.Get('idNdermarrje') })
        }).done(SucceededCallbackArt);
    }
}
function SucceededCallbackArt(grupi) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    var kodi = "#txtGrupi" + idRow;

    if (grupi !== null && grupi.Id !== -1) {
        $(kodi).val(grupi.Kodi);

    } else {
        $(kodi).val('');
    }
    vendosTotalet();
}
function KtheVleraArtTitulli(idja) {
    $.ajax({
        url: Utils.getServerApiUrl("ListPagesa", "ktheVleraKonfigMeID"),
        data: JSON.stringify({ idja: idja })
    }).done(SucceededCallbackArtTitulli);
}
function KtheVleraArtMeKodTitulli(kodi) {
    if (kodi != undefined && typeof (kodi) != undefined && kodi != "")
    {
        $.ajax({
            url: Utils.getServerApiUrl("ListPagesa", "ktheVleraKonfigMeKod"),
            data: JSON.stringify({ kodi: kodi, lloji: 2, idNderrmarje: hfState.Get('idNdermarrje') })
        }).done(SucceededCallbackArtTitulli);
    }
}
function SucceededCallbackArtTitulli(titulli) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    var kodi = "#txtTitulli" + idRow;
    if (titulli !== null && titulli.Id !== -1) {
        $(kodi).val(titulli.Kodi);

    } else {
        $(kodi).val('');
    }
    vendosTotalet()
}
function KtheVleraArtKapitulli(idja) {
    $.ajax({
        url: Utils.getServerApiUrl("ListPagesa", "ktheVleraKonfigMeID"),
        data: JSON.stringify({ idja: idja })
    }).done(SucceededCallbackArtKapitulli);
}
function KtheVleraArtMeKodKapitulli(kodi) {
    if (kodi != undefined && typeof (kodi) != undefined && kodi != "")
    {
        $.ajax({
            url: Utils.getServerApiUrl("ListPagesa", "ktheVleraKonfigMeKod"),
            data: JSON.stringify({ kodi: kodi, lloji: 3, idNderrmarje: hfState.Get('idNdermarrje') })
        }).done(SucceededCallbackArtKapitulli);
    }
}
function SucceededCallbackArtKapitulli(kapitulli) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    var kodi = "#txtKapitulli" + idRow;
    if (kapitulli !== null && kapitulli.Id !== -1) {
        $(kodi).val(kapitulli.Kodi);

    } else {
        $(kodi).val('');
    }
    vendosTotalet()
}
function KtheVleraArtArtikulli(idja) {
    $.ajax({
        url: Utils.getServerApiUrl("ListPagesa", "KtheVleraLlogMeID"),
        data: JSON.stringify({ idja: idja })
    }).done(SucceededCallbackArtArtikulli);
}

function KtheVleraArtMeKodArtikulli(kodi) {
    if (kodi != undefined && typeof (kodi) != "undefined" && kodi != "") {
        $.ajax({
            url: Utils.getServerApiUrl("ListPagesa", "ktheVleraLlogMeKod"),
            data: JSON.stringify({ kodi: kodi, idNderrmarje: hfState.Get('idNdermarrje') })
        }).done(SucceededCallbackArtArtikulli);
    }
}

function SucceededCallbackArtArtikulli(artikulli) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    var kodi = "#txtArtikulli" + idRow;
    if (artikulli !== null && artikulli.IdLlogari !== -1) {
        $(kodi).val(artikulli.NrLlogari);

    } else {
        $(kodi).val('');
    }
    vendosTotalet()
}

function KtheVleraArtAnaliza(idja) {
    $.ajax({
        url: Utils.getServerApiUrl("ListPagesa", "KtheVleraLlogMeID"),
        data: JSON.stringify({ idja: idja })
    }).done(SucceededCallbackArtAnaliza);
}

function KtheVleraArtMeKodAnaliza(kodi) {
    if (kodi != undefined && kodi != "undefined" && kodi != "")
    {
        $.ajax({
            url: Utils.getServerApiUrl("ListPagesa", "ktheVleraLlogMeKod"),
            data: JSON.stringify({ kodi: kodi, idNderrmarje: hfState.Get('idNdermarrje') })
        }).done(SucceededCallbackArtAnaliza);
    }
}
function SucceededCallbackArtAnaliza(analiza) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    var kodi = "#txtAnalize" + idRow;
    if (varKonfig.kontrolloPerKategoriShpenzimi && analiza.IdKategoriShpenzimi == 0) {
        mesazhGabimiLlogariPaKategoriShpenzimi(analiza.NrLlogari);
        $(kodi).val('');
    } else if (analiza !== null && analiza.IdLlogari !== -1) {
        $(kodi).val(analiza.NrLlogari);
        $("#txtObjekti" + idRow).val(analiza.EmerLlogari1);
    }
    else {
        $(kodi).val('');
    }
    vendosTotalet()
}

function kushtiAKSH() {
    var kushtiAKSH = Utils.findFieldValueByAttribute(colKushte, "Kodi", "IdKusht", "AKSH");
    if (kushtiAKSH == null)
        return false;
    var alternativa = Utils.findFieldValueByAttribute(colAlterKusht, "IdKushti", "Alternativa", kushtiAKSH);
    if (alternativa == null)
        return false;
    return alternativa == "Po";
}

function mesazhGabimiLlogariPaKategoriShpenzimi(nrLlogarie) {
    myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimiLlogariKategoriShpenzimi").replace("XXX", nrLlogarie));
}

/*
Function: vendosTotalet

Llogarit dhe vendos totalin.

Nryshoi kevi
*/
function vendosTotalet() {
    var grida = jQuery("#rowed5");
    var totali = 0;
    var rreshtaTeGrides = grida.getRowData();
    var ids = grida.getDataIDs();
    for (i = 0; i < rreshtaTeGrides.length; i++) {

        if ($("#txtShuma" + ids[i]).val() !== undefined) {
            if ($("#txtGrupi" + ids[i]).val() !== "") //totali += parseFloat($("#txtShuma" + ids[i]).val());
                totali = totali + parseFloat($("#txtShuma" + ids[i]).val());
        }
        else if (rreshtaTeGrides[i].txtGrupi !== "") //totali += parseFloat(rreshtaTeGrides[i].txtShuma);
            totali = totali + parseFloat(grida.getTekstQelize('txtShuma', ids[i]));
        if (isNaN(totali))
            totali = 0;
        txtVlefta.SetText(totali); // roundDecimal(totali, 2));
    }
}


/*
Function: pastroFushatKokes

Pastron fushat pasi eshte bere ruajtja apo modifikimi per te bere gati ambjentin per shtim.
*/
function pastroFushatKokes() {
    txtNrDok.SetText('');
    txtNrKuponi.SetText('');
    txtNrPunonjesve.SetText('');
    txtEmriPerfitues.SetText('');
    txtNipti.SetText('');
    txtEmriBankes.SetText('');
    txtNrLlogBankare.SetText('');
    txtAdresa.SetText('');
    radLlojDok.SetValue(1);
    txtNrDokNgjitur.SetText('');
    txtVlefta.SetText('0');
    txtUrdheruesi.SetText('');
    txtNenpunesiThesarit.SetText('');
    txtKontabilisti.SetText('');
    btneArtikulli.SetValue(null);
    btneKapitulli.SetValue(null);
    btneTitulli.SetValue(null);
    btneGrupi.SetValue(null);
    var hf = $("#status1");
    hf.val("false");
    vendosDateDefault();
}

function vendosDateDefault() {
    if ($("input[id$='hfShtimModifikim']").val() === "shtim") {
        try {
            var hfPeriudheObj = window.parent.lexoHfPeriudhe();
            dteDtDok.SetDate(Utils.ktheDateDefault(hfPeriudheObj));
            var dataSot = Utils.zeroOren(new Date());
            dteDtDokNgjitur.SetDate(dataSot);
            dteDtAprovimi.SetDate(dataSot);
        }
        catch (e) { }
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
    else if (dteDtDokNgjitur.GetDate() === null) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniNjeDateDokumentiNgjitur"));
        return false;
    }
    else if (dteDtAprovimi.GetDate() === null) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniNjeDateAprovimi"));
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
    if (hf.value === "true") {
        myFaqeCelje.kontrolloTeDrejta('Shto_UrdherPagesa.aspx?shtim_modifikim=shtim', true);
    }
    else
        click = false;
}

/*
Function: menuClick

Percakton veprimin qe kryhet nese klikohet nje nga butonat e menuse.
*/
function menu_click(s, e) {
    if (e.item.name === 'Ruaj' || e.item.name === 'RuajPrint') {
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
    else if (e.item.name === 'Draft') {
        myFaqeCelje.validim(s, e);
        if (isValidKoka()) {
            Utils.shfaqLoadingGif();;
            RuajClick(s, e);
        }
        else {
            e.processOnServer = false;
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgPlotesoniTeGjithaFushat"));
        }
    }
    else if (e.item.name === 'Kerko') {
        myFaqeCelje.kontrolloTeDrejta('UrdherPagesa.aspx', null, true);
        e.processOnServer = false;
    }
    else if (e.item.name === 'Shto') {
        myFaqeCelje.kontrolloTeDrejta('Shto_UrdherPagesa.aspx?shtim_modifikim=shtim', true);
        e.processOnServer = false;
    }
    else if (e.item.name === 'Fshi') {
        popFshi.Show(); e.processOnServer = false;
    }
    else if (e.item.name === 'Anullo') {
        myFaqeCelje.kontrolloTeDrejta('UrdherPagesa.aspx');
        e.processOnServer = false;
        click = false;
    }
    else if (e.item.name === 'Klono') {
        KlonoClick(e);
    }

}

function KlonoClick(e) {
    var hf1 = $("#hfShtimModifikim");
    hf1.val("klonim");
    myMenu.menuSipasTeDrejtaRegjistrim(hf1, hfTeDrejta);
    e.processOnServer = false;
    ndryshoKonfigurimin();
    click = false;
}

function Kthehu(mesazh) {
    myFaqeCelje.kontrolloTeDrejta('UrdherPagesa.aspx?ruaj=po');

}
var click = false;
/*
Function: RuajClick

Validon dokumentin dhe therret funksionin <merrTeDhena> per te marre te dhenat e grides kur klikohet butoni Ruaj.
*/

function RuajClick(s, e) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    if (click) {
        e.processOnServer = false; Utils.hiqLoadingGif();;
        return;
    }
    click = true;
    if (isValidKoka()) {
        grida.saveRow(idRow, false, 'clientArray');
        merrTeDhena(e);
        grida.setLastSel2(0);
        if (trupiBosh === true) {
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
    var hf = $("#hfShtimModifikim");
    ASPxMenu1.GetItemByName('Fshi').SetVisible(false);
    hf.val("shtim"); pastro();
    myMenu.menuSipasTeDrejtaRegjistrim(hf, hfTeDrejta);
    pastroFushatKokes();
    if (hf.val() === "shtim") {
        ASPxMenu1.GetItemByName('Klono').SetVisible(false);
    }

    ndryshoKonfigurimin(); //duhet kur klijkojme butonin shto ne rastin kur kemi hap nje dok. per modifikim
    //            jQuery("#rowed5").GridUnload("rowed5");
    //            inicializoGride();
    mbushGrideNgaHiddenFieldet();
    $('#ASPxSplitter1_hl').empty(); click = false;
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

/*
Function: LostFocusobjeti (per grupin, artikullin, ose kapitullin ne varesi te parametrit qe i caktohet)

Vendosur ne gride vlera perkatese qe zgjidhet te koka
*/
function LostFocusObjekti(objekti, kontrolli) {
    var grida = $("#rowed5");
    var idRresht = grida.getLastSel2();
    //vendoset vlera e zgjedhur te koka ne rreshtin qe po editohet
    var reshtiieditueshem = false;
    var vlera = kontrolli.GetText();
    var objektigrides = $("select[id$='txtGrupi" + idRresht + "']");
    if (objektigrides.val() != undefined) {
        for (var i = 0; i < grupigrides[0].length; i++) {
            if (objektigrides[0][i].text == vlera)
                objektigrides[0].selectedIndex = i;
        }
        reshtiieditueshem = true;
    }
    //vendoset vlera e zgjedhur te koka ne rreshtat e tjere te grides
    var rreshtaTeGrides = grida.jqGrid('getDataIDs');
    for (i = 0; i < rreshtaTeGrides.length; i++) {
        if (!reshtiieditueshem)
            grida.setTekstQelize(objekti, rreshtaTeGrides[i], vlera);
        else if (rreshtaTeGrides[i] != idRresht) {
            grida.setTekstQelize(objekti, rreshtaTeGrides[i], vlera);
        }
    }
}

function ButtonClickKapitulliNgaKoka() {
    vjenNgaKokeApoTrup = true;
    var hfKod = $("#hfGridaKapitulli");
    var queryStr = hfKod.val();
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhniKapitullin"), 'LupaKonfigUrdherPagesa.aspx?vjenNga=Kapitulli&idkonfigambjente=' + queryStr, widthLupa, heightLupa);
}
function ButtonClickArtikulliNgaKoka() {
    vjenNgaKokeApoTrup = true;
    var hfKod = $("#hfGridaArtikulli");
    var queryStr = hfKod.val();
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhniLlogarine"), 'LupaLlogaria.aspx?vjennga=LlogArt&idkonfigambjente=' + queryStr, widthLupa, heightLupa);
}

function ButtonClickGrupiNgaKoka() {
    vjenNgaKokeApoTrup = true;
    var hfKod = $("#hfGridaGrupi");
    var queryStr = hfKod.val();
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgKodifikimArtikullZgjidhGrup"), 'LupaKonfigUrdherPagesa.aspx?vjenNga=Grupi&idkonfigambjente=' + queryStr, widthLupa, heightLupa);
}

function ButtonClickTitulliNgaKoka() {
    vjenNgaKokeApoTrup = true;
    var hfKod = $("#hfGridaTitulli");
    var queryStr = hfKod.val();
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhniTitullin"), 'LupaKonfigUrdherPagesa.aspx?vjenNga=Titulli&idkonfigambjente=' + queryStr, widthLupa, heightLupa);
}
