;
var pageState = {
    webhook: {},   
    llojDokumenti: undefined,
    formatNumri: undefined,
    formatKursi: undefined,
    formatVleftaDB: 7,
    llojiVeprimi: undefined,
    shtimModifikim: undefined,
    IsLidhur: false,
    aprovim: false,
    llojSubjektiDefault: "",
    subjektiDefault: 0,
    gridaSelector: "#rowed5",
    kushte: {},
    klientiDefault: undefined,
    isKursiTextChange: false,
    queryString: "",
    vendosLlojFature: false,
    ruajVleratFundit: false,
    mySessionStorage: null,
    poNdryshojKursinNeModifikim: false,
    kontrolloPerRreshtaLLMKF: false,
    rreshtatPerNdryshimKursFature: []
};
var kategoria = "";
var pershk = 1;
var arrayIdKolonaGrides = new Array();
var arrayPershkrimiKolonaGrides = new Array();
var arrayVisibleKolonaGrides = new Array();
var arrayWidthKolonaGrides = new Array();
var arrayReadOnlyKolonaGrides = new Array();
var arrayRenditjeKolonaGrides = new Array();
var enablekurs = true;
var identikuesPerPopupKursi = "ShtoVeprimBanka";
var llojkursi = 1; //perdoret per te ruajtur llojin e kursit te zgjedhur tek konfigurimi. Ne qofte se nuk ka asnje lloj te zgjedhur, atehere merret lloji i pare.
var widthLupaKursi = 950;
var heightLupaKursi = 560;
var widthLupaAutomjet = 800;
var heightLupaAutomjet = 600;
var identifikuesPerPopupAutomjet = "ShtoVeprimBanka";
var identifikuesPerPopupBanka = "ShtoVeprimBanka";

var arrMon = new Array();
var arrVlera = new Array();
var infoKf = false;
var idInfoKf = 0;
var updateGride = true;
var dokumentat, dokRradhes = 0; //per dokumentat qe do shperndahen ne qendrakosto

var varKonfig = {
    identifikuesPerLocalStorageKey: 'VeprimArkaBanka'
};

jQuery(document).ready(function () {
    pageState.mySessionStorage = new mySessionStorage();
    var lloji = Utils.getUrlVar("lloji");
    NgjyrosFaqetERegjistrimeve(lloji);
    Utils.konfiguroAccorditionNeDocReady(varKonfig.identifikuesPerLocalStorageKey);
    $(window).on('resize', function () {
        try {
            if (Utils.isGridResized())//if (!Utils.resizeSplitter(145))
                return;
        }
        catch (ee) {
            console.log(ee);
        }
        var grida = $('#rowed5');
        if ($('#divgride2').width() != null) {
            myJQGrid.fixGridWidth(grida, $('#divgride3'));
        }
    }).trigger('resize');
    $(document).keydown(function (e) {//po
        switch (e.which) {
            case 9:
                if (myMesazh.pyetjeEHapur)
                    e.preventDefault();
                break;
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
            case 120: //F9
                e.preventDefault();
                if ($(e.target).prop('tagName') === 'INPUT' && $(e.target).prop('type') === 'text')
                    $(e.target).trigger('blur');
                if ($('#hfShtimModifikim').val() == 'modifikim' && $('#hfSkema').val() > 0 && lblStatusAprovimi.GetText() == 'Per Aprovim') {
                    myMesazh.ShtoMesazhGabimi(hfState.Get("msgDokNeProcesAprovimi"));
                    return;
                }
                if ($('#hfShtimModifikim').val() == 'modifikim' && $('#hfSkema').val() > 0 && lblStatusAprovimi.GetText() == 'Aprovuar' && ASPxMenu1.GetItemByName("Ruaj").clientVisible == false) {
                    myMesazh.ShtoMesazhGabimi("Ju nuk keni te drejta per kete veprim!");
                    return;
                }
                if (($('#hfShtimModifikim').val() == 'shtim' && $('#hfSkema').val() > 0) || ($('#hfShtimModifikim').val() == 'modifikim' && $('#hfSkema').val() > 0 && lblStatusAprovimi.GetText() != 'Aprovuar')) {
                    var eventData = {
                        item: {
                            name: 'Aprovo',
                            index: ASPxMenu1.GetItemByName("Aprovo").index //6
                        },
                        processOnServer: true
                    };

                }
                else {
                    var eventData = {
                        item: {
                            name: 'Ruaj',
                            index: ASPxMenu1.GetItemByName("Ruaj").index //0
                        },
                        processOnServer: true
                    };
                }
                var sender = 'tastiera';
                menu_click(sender, eventData);
                break;
            default:
                break;
        }
    });
    $(window).on('load', function () {
        Init();
        Utils.resizeSplitter();
        if (Utils.getUrlVar('teDrejta') === "po") {
            window.parent.btn.DoClick();
            Utils.SetOrRefreshSplitterPaneContentUrl("Footer", "FooterPanelInfo.aspx");
        }
    });


});

$(window).unload(function () {
    pageState.mySessionStorage.removeAll();
});

function Init() {
    if (typeof (isPostBack) == "undefined") {
        document.getElementById("kokeKonfigurimi").innerHTML = hfState.Get("MenuKokeDokumenti");
        document.getElementById("trupKonfigurimi").innerHTML = hfState.Get("MenuTrupDokumenti");
        document.getElementById("fundKonfigurimi").innerHTML = hfState.Get("MenuFundDokumenti");

        pageState.queryString = Utils.getUrlVars();
        if (pageState.queryString.vjenNga == "Shto_RegjistrimDokumentash" || pageState.queryString.vjenNga == "RegjistrimDokumentash")
            pageState.vendosLlojFature = true;
        pageState.shtimModifikim = $('#hfShtimModifikim').val();
        pageState.IsLidhur = $("input[id$='hfLidhur']").val() == 'True';
        pageState.llojDokumenti = Utils.getUrlVar('lloji');

        var idArkaBanka = banka_ComboBox.GetValue();
        pageState.mySessionStorage.setObject("banka_ComboBox_fillestare", idArkaBanka ? { text: banka_ComboBox.GetSelectedItem().text, value: banka_ComboBox.GetSelectedItem().value } : null);

        if (pageState.shtimModifikim == 'modifikim' || pageState.shtimModifikim == 'anullim' || pageState.shtimModifikim == 'klonim') {
            ndryshokurs = false;
            if (pageState.shtimModifikim == 'modifikim')
                pageState.kontrolloPerRreshtaLLMKF = true;
        }
        else {
            ndryshokurs = true;
            kursi_TextBox.SetText(1);
        }
        kursss = kursi_TextBox.GetText();
        kursifundit = kursi_TextBox.GetText();
        keyGlobal = -1;
        furnitori_Label.SetText("Klienti/Furnitori");
        changeName();
        if (pageState.shtimModifikim == 'shtim') {
            ndryshodege = true;
            vendosPeriudhenBanka();
        }
        identikuesPerPopupLlogari = "VeprimeBanka";
        identikuesPerPopupKlientFurnitori = "VeprimeBanka";
        identifikuesPerPopupDokumentat = "ShtoVeprimBanka.aspx";
        ndryshoKonfigurimin(true);

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(EndRequestHandler);
        myMesazh.shtoHandler();
        merrKonfigurimeWebhook(hfState.Get("idNdermarrje"))

    }
}

function vendosPeriudhenBanka() {
    var HfPeriudhaObj = ktheObjektPeriudhe();
    data_DateEdit.SetDate(Utils.ktheDateDefault(HfPeriudhaObj));
    var dataSot = Utils.zeroOren(new Date());
    data_regj_DateEdit.SetDate(dataSot);
}

function ktheObjektPeriudhe() {  //kthen nje Objekt qe permban periudhen kontabel te zgjedhur
    var HfPeriudhaObj = new Object();
    HfPeriudhaObj.idPeriudha = hfPeriudhaKontabelBanka.Get("idPeriudha");
    HfPeriudhaObj.emerPeriudha = hfPeriudhaKontabelBanka.Get("emerPeriudha");
    HfPeriudhaObj.FillimiPeriudha = hfPeriudhaKontabelBanka.Get("fillimiPeriudha");
    HfPeriudhaObj.MbarimiPeriudha = hfPeriudhaKontabelBanka.Get("mbarimiPeriudha");
    return HfPeriudhaObj;
}

/*
Function: inicializoGrideTrupi

Inicializon griden e trupit. Konfiguron kolonat e grides dhe percakton veprimin qe kryhet onCellSelect.
*/
function inicializoGrideTrupi() {
    if (arrayIdKolonaGrides.length == 0)
        return;
    var classes = '';

    if (pageState.IsLidhur)
        classes = 'uigray';
    var arrayPershkrime = [arrayPershkrimiKolonaGrides[0], arrayPershkrimiKolonaGrides[1], arrayPershkrimiKolonaGrides[2], arrayPershkrimiKolonaGrides[3],
    arrayPershkrimiKolonaGrides[4], arrayPershkrimiKolonaGrides[5], arrayPershkrimiKolonaGrides[6], arrayPershkrimiKolonaGrides[7],
        arrayPershkrimiKolonaGrides[8], arrayPershkrimiKolonaGrides[9], arrayPershkrimiKolonaGrides[10], arrayPershkrimiKolonaGrides[11], arrayPershkrimiKolonaGrides[12], arrayPershkrimiKolonaGrides[13], arrayPershkrimiKolonaGrides[14], arrayPershkrimiKolonaGrides[15], arrayPershkrimiKolonaGrides[16], arrayPershkrimiKolonaGrides[17], arrayPershkrimiKolonaGrides[18], arrayPershkrimiKolonaGrides[19], arrayPershkrimiKolonaGrides[20], arrayPershkrimiKolonaGrides[21], arrayPershkrimiKolonaGrides[22]
    ];
    var arrayModel = [
        { name: arrayIdKolonaGrides[0], index: arrayIdKolonaGrides[0], width: arrayWidthKolonaGrides[0], hidden: arrayVisibleKolonaGrides[0], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemNrRendor, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[1], index: arrayIdKolonaGrides[1], width: arrayWidthKolonaGrides[1], hidden: arrayVisibleKolonaGrides[1], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myElemLloji, custom_value: myJQGrid.myValueCombo } },
        { name: arrayIdKolonaGrides[2], index: arrayIdKolonaGrides[2], width: arrayWidthKolonaGrides[2], hidden: arrayVisibleKolonaGrides[2], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myElemSubjekti, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[3], index: arrayIdKolonaGrides[3], width: arrayWidthKolonaGrides[3], hidden: arrayVisibleKolonaGrides[3], classes: classes, editable: true, edittype: 'custom', editoptions: { custom_element: myElemEmertimi, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[4], index: arrayIdKolonaGrides[4], width: arrayWidthKolonaGrides[4], hidden: arrayVisibleKolonaGrides[4], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemFatura, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[5], index: arrayIdKolonaGrides[5], width: arrayWidthKolonaGrides[5], hidden: arrayVisibleKolonaGrides[5], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemDebiKredi, custom_value: myJQGrid.myValueCombo } },
        { name: arrayIdKolonaGrides[6], index: arrayIdKolonaGrides[6], width: arrayWidthKolonaGrides[6], hidden: arrayVisibleKolonaGrides[6], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemVlera, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[7], index: arrayIdKolonaGrides[7], width: arrayWidthKolonaGrides[7], hidden: arrayVisibleKolonaGrides[7], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemZbritja, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[8], index: arrayIdKolonaGrides[8], width: arrayWidthKolonaGrides[8], hidden: arrayVisibleKolonaGrides[8], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemPershkrimi, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[9], index: arrayIdKolonaGrides[9], width: arrayWidthKolonaGrides[9], hidden: arrayVisibleKolonaGrides[9], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemKreditet, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[10], index: arrayIdKolonaGrides[10], width: arrayWidthKolonaGrides[10], hidden: arrayVisibleKolonaGrides[10], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemVleraArketuar, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[11], index: arrayIdKolonaGrides[11], width: arrayWidthKolonaGrides[11], hidden: arrayVisibleKolonaGrides[11], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemVleraMonBaze, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[12], index: arrayIdKolonaGrides[12], width: arrayWidthKolonaGrides[12], hidden: arrayVisibleKolonaGrides[12], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myelemIdKodi, custom_value: myJQGrid.myValueTextBox }, hidedlg: true },
        { name: arrayIdKolonaGrides[13], index: arrayIdKolonaGrides[13], width: arrayWidthKolonaGrides[13], hidden: arrayVisibleKolonaGrides[13], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemOpsione, custom_value: myJQGrid.myValueCombo } },
        { name: arrayIdKolonaGrides[14], index: arrayIdKolonaGrides[14], width: arrayWidthKolonaGrides[14], hidden: arrayVisibleKolonaGrides[14], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemNrTel, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[15], index: arrayIdKolonaGrides[15], width: arrayWidthKolonaGrides[15], hidden: arrayVisibleKolonaGrides[15], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemKursi, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[16], index: arrayIdKolonaGrides[16], width: arrayWidthKolonaGrides[16], hidden: arrayVisibleKolonaGrides[16], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemMuaji, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[17], index: arrayIdKolonaGrides[17], width: arrayWidthKolonaGrides[17], hidden: arrayVisibleKolonaGrides[17], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemKodi, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[18], index: arrayIdKolonaGrides[18], width: arrayWidthKolonaGrides[18], hidden: arrayVisibleKolonaGrides[18], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemVleraFill, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[19], index: arrayIdKolonaGrides[19], width: arrayWidthKolonaGrides[19], hidden: arrayVisibleKolonaGrides[19], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemVleraMbetur, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[20], index: arrayIdKolonaGrides[20], width: arrayWidthKolonaGrides[20], hidden: arrayVisibleKolonaGrides[20], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemStatusFature, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[21], index: arrayIdKolonaGrides[21], width: 0, hidden: true, classes: classes, sortable: false, editable: false },
        { name: arrayIdKolonaGrides[22], index: arrayIdKolonaGrides[22], width: arrayWidthKolonaGrides[22], hidden: arrayVisibleKolonaGrides[22], classes: classes, sorttype: "int", editable: true, edittype: 'custom', editoptions: { custom_element: myElemButtonFshi, custom_value: myValueButtonFshi }, hidedlg: true }
    ];

    var gridParams =
        {
            emergride: "#rowed5",
            arrayPershkrime: arrayPershkrime,
            arrayModel: arrayModel,
            lidhur: pageState.IsLidhur,
            emerEditorKodi: 'txtSubjekti',
            emerEditorLloji: arrayIdKolonaGrides[1],
            widthi: $('#divgride2').width() - 5,
            afterSaveFunc: afterSaveFunc,
            lostFocusKoloneFundit: lostFocusKoloneFundit,
            resetRreshtKorent: resetRreshtKorent,
            autocompleteList: [{ emerEditor: 'txtSubjekti', selectFunc: selectFunc, changeFunc: changeFunc, shtoDataKod: false }],
            konfigToolbar: {
                konfigGrid: $('#hfTeDrejtaKonfGride').val(),
                konfigkf: $('#hfTeDrejtaKFRi').val(),
                identifikuesNrRreshta: "ShtoVeprimBanka",
                ruajKolonatEGrides: ruajKolonatEGrides,
                hapPopUpRi: hapPopUpRi,
                exportExcel: true,        //Ben enable exportin e F
                EmerExporti: "Arka/Banka" //Emri i filet .xls qe gjenerohet
            }
        };
    return myJQGrid.initGride(gridParams);
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
    grida.setVlereDefault('txtVlera', 0);
    grida.setVlereDefault('txtZbritja', 0);
    grida.setVlereDefault('txtKreditet', 0);
    grida.setVlereDefault('txtVleraArketuar', 0);
    grida.setVlereDefault('txtVleraMonBaze', 0);
    grida.setVlereDefault('txtKursi', 1);
    grida.setVlereDefault('txtVleraFill', 0);
    grida.setVlereDefault('txtVleraMbetur', 0);
    return;
}

function dateGotFocus(s, e) {
    window.setTimeout(function () { s.SetCaretPosition(0); }, 0);
}

function ndryshoKonfigFormatNumri(grida, formatNumri, formatkursi) {
    ///<summary> metode per ndryshimin e formateve te nr ne gride ne baze te emrit te kolones. gjithashtu vendos formatin e nr dhe per fushat e devexpresit ne baze te emrit te kontrollit</summary>
    ///<param name="formatNumri"> objekt i tipit format nr me te dhenat e formatimit</param>
    ///<param name="formatkursi">sherben per te vendosur formatin e kursit ne varesi te monedhes </param>
    if (typeof formatNumri == 'undefined')
        formatNumri = $.parseJSON(hfState.Get("formatMonedhe"));
    else
        hfState.Set("formatMonedhe", JSON.stringify(formatNumri));
    grida.setShifraPasPresjes('txtVlera', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtZbritja', formatNumri.ShifraPasPresjesZbritja);
    grida.setShifraPasPresjes('txtKreditet', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtVleraArketuar', pageState.formatVleftaDB); //formatNumri.ShifraPasPresjesVlefta
    grida.setShifraPasPresjes('txtVleraMonBaze', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtKursi', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtVleraFill', formatNumri.ShifraPasPresjesVlefta);
    grida.setShifraPasPresjes('txtVleraMbetur', formatNumri.ShifraPasPresjesVlefta);
    Utils.setFormatNumri(kursi_TextBox, formatkursi);
    Utils.setFormatNumri(vlera_TextBox, formatNumri.ShifraPasPresjesVlefta);
    Utils.setFormatNumri(vleraMonedhaBaze_TextBox, formatNumri.ShifraPasPresjesVlefta);
    Utils.setFormatNumri(komision_TextBox, formatNumri.ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtDebi, pageState.formatVleftaDB); //formatNumri.ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtKredi, pageState.formatVleftaDB); //formatNumri.ShifraPasPresjesVlefta);
    Utils.setFormatNumri(txtDiferenca, pageState.formatVleftaDB); //formatNumri.ShifraPasPresjesVlefta);
}

/*
Formaton vlerat e fushave sipas formatit perkates.
*/
function vendosKonfigFormatNumri() {
    var grida = $('#rowed5');
    var idTe = grida.jqGrid('getDataIDs');
    for (j = 0; j < idTe.length; j++) {
        var idRreshti = idTe[j];
        grida.formatoQelize('txtVlera', idRreshti);
        grida.formatoQelize('txtZbritja', idRreshti);
        grida.formatoQelize('txtKreditet', idRreshti);
        grida.formatoQelize('txtVleraArketuar', idRreshti);
        grida.formatoQelize('txtVleraMonBaze', idRreshti);
        grida.formatoQelize('txtKursi', idRreshti);
        grida.formatoQelize('txtVleraFill', idRreshti);
        grida.formatoQelize('txtVleraMbetur', idRreshti);
    }
    formatoFushaDevi();
}

function formatoFushaDevi() {
    Utils.formatoShumeTextBox(kursi_TextBox, vleraMonedhaBaze_TextBox, vlera_TextBox, komision_TextBox, txtDebi, txtKredi, txtDiferenca);
}

function unformatoFushaDevi() {
    Utils.unFormatoShumeTextBox(kursi_TextBox, vleraMonedhaBaze_TextBox, vlera_TextBox, komision_TextBox, txtDebi, txtKredi, txtDiferenca);
}

function selectFunc(event, ui, emerfushe) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    vendosTotalet();
    //var ekziston = false;
    if (ui.item != null) {
        $(emerfushe).val(ui.item.label);
    }

    var lloji = grida.getTekstQelize(arrayIdKolonaGrides[1], idRresht);
    switch (lloji) {
        case "Llogari": //Artikull
            $.ajax({
                url: Utils.getServerApiUrl("Rregjistrime", "KtheVleraLlogMeIDRow"),
                data: JSON.stringify({ idja: ui.item.value, rreshti: idRresht })
            }).done(SucceededCallbackLlog);
            break;
        case "Klient":
        case "Furnitor": //Llogari
            $.ajax({
                url: Utils.getServerApiUrl("Rregjistrime", "ktheKlientFurnitorVeprimeKFMeIDRow"),
                data: JSON.stringify({ IDkf: ui.item.value, rreshti: idRresht, date: data_DateEdit.GetDate(), llojKursi: llojkursi })
            }).done(SucceededCallbackVleraKodi);
            break;
        case "Punonjes":
            $.ajax({
                url: Utils.getServerApiUrl("ListPagesa", "kthePunonjesMeIdRow"),
                data: JSON.stringify({ idPunonjes: ui.item.value, rreshti: idRresht })
            }).done(SucceededCallbackPunonjes);
            break;
    }
    return false;
}

function changeFunc(event, ui, emerKodi, index) {
    var grida = $('#rowed5');
    //var idRow = grida.getLastSel2();

    var lloji = grida.getTekstQelize(arrayIdKolonaGrides[1], index);
    var emerfushe = grida.getTekstQelize(arrayIdKolonaGrides[2], index);
    if (ui == null || ui.item == null) {
        if (emerfushe !== "") {
            switch (lloji) {
                case "Llogari": //Llogari
                    $.ajax({
                        url: Utils.getServerApiUrl("Rregjistrime", "ktheVleraLlogMeKodRow"),
                        data: JSON.stringify({ kodi: emerfushe, rreshti: index, idNdermarrje: hfState.Get("idNdermarrje") })
                    }).done(SucceededCallbackLlog);
                    break;
                case "Klient":
                case "Furnitor"://KF
                    $.ajax({
                        url: Utils.getServerApiUrl("Rregjistrime", "ktheKlientFurnitorVeprimeKFRow"),
                        data: JSON.stringify({ kodi: emerfushe, rreshti: index, data: data_DateEdit.GetDate() })
                    }).done(SucceededCallbackVleraKodi);
                    break;
                case "Punonjes":
                    $.ajax({
                        url: Utils.getServerApiUrl("ListPagesa", "kthePunonjesMeKodRow"),
                        data: JSON.stringify({ kodPunonjes: emerfushe, idNdermarrje: hfState.Get('idNdermarrje'), rreshti: index })
                    }).done(SucceededCallbackPunonjes);
                    break;
            }
            return;
        }
        switch (lloji) {
            case "Llogari": //Llogari
                vendosLlogari([index, null]);
                break;
            case "Klient":
            case "Furnitor": //KF
                vendosKf({ idRreshti: index, oKF: null, monedha: 0, kursi: 0 });
                break;
            case "Punonjes":
                vendosPunonjes({ idRreshti: index, punonjesi: null, llogaria: null });
                break;
        }
        return;
    }
    switch (lloji) {
        case "Llogari": //Llogari
            $.ajax({
                url: Utils.getServerApiUrl("Rregjistrime", "KtheVleraLlogMeIDRow"),
                data: JSON.stringify({ idja: ui.item.value, rreshti: index })
            }).done(SucceededCallbackLlog);
            break;
        case "Klient":
        case "Furnitor": //KF
            $.ajax({
                url: Utils.getServerApiUrl("Rregjistrime", "ktheKlientFurnitorVeprimeKFMeIDRow"),
                data: JSON.stringify({ IDkf: ui.item.value, rreshti: index, date: data_DateEdit.GetDate(), llojKursi: llojkursi })
            }).done(SucceededCallbackVleraKodi);
            break;
        case "Punonjes":
            $.ajax({
                url: Utils.getServerApiUrl("ListPagesa", "kthePunonjesMeIdRow"),
                data: JSON.stringify({ idPunonjes: ui.item.value, rreshti: index })
            }).done(SucceededCallbackPunonjes);
            break;
    }
}

function resetRreshtKorent(idRreshti) {
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    if (idRreshti == idRow && grida.getTekstQelize('txtSubjekti', idRow) != undefined) {
        grida.setTekstQelize('txtVlera', idRreshti);
        grida.setTekstQelize('txtSubjekti', idRreshti, '');
        grida.setTekstQelize('txtIdKodi', idRreshti, '');
        grida.setTekstQelize('txtPershkrimi', idRreshti, pershkrimi_Memo.GetText());
        grida.setTekstQelize('txtKreditet', idRreshti);
        grida.setTekstQelize('txtEmertimi', idRreshti, '');
        grida.setTekstQelize('txtFatura', idRreshti, '');
        grida.setTekstQelize('txtNrTel', idRreshti, '');
        grida.setTekstQelize('txtZbritja', idRreshti);
        grida.setTekstQelize('txtVleraMonBaze', idRreshti);
        grida.setTekstQelize('txtVleraArketuar', idRreshti);
        grida.setTekstQelize('txtKursi', idRreshti);
        grida.setTekstQelize('txtVleraFill', idRreshti);
        grida.setTekstQelize('txtVleraMbetur', idRreshti);
        grida.setTekstQelize('StatusFature', idRreshti, '');
        grida.setTekstQelize('MeKursFature', idRreshti, false);
        vendosTotalet();
        return;
    }
    grida.jqGrid('delRowData', idRreshti);
    return;
}

function SucceededCallbackPunonjes(result) {   
    if (result == null)
        return;
    vendosPunonjes(result);
}

function msgPunonjesPaLlogari(punonjesit) {

    if (!Array.isArray(punonjesit))
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgPunonjesiPrefixNjejes") + punonjesit.punonjesi.NrPersonal + hfState.Get("msgPunonjesPaLlogariPageseNjejes"));
    else {
        if (punonjesit.length == 0)
            return;
        else if (punonjesit.length == 1)
             myMesazh.ShtoMesazhGabimi(hfState.Get("msgPunonjesiPrefixNjejes") + punonjesit[0].punonjesi.NrPersonal + hfState.Get("msgPunonjesPaLlogariPageseNjejes"));
        else {
            var pun = "";
            punonjesit.forEach(function (item) { pun += item.punonjesi.NrPersonal + ", "; });
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgPunonjesiPrefixShumes") + pun.substring(0, pun.length - 2) + hfState.Get("msgPunonjesPaLlogariPageseShumes"));
        }
    }
}

function kontrolloPunonjes(result) {
    var punonjes = result.punonjesi;
    var llogariPunonjesi = result.llogaria;
    if (punonjes.IdLlogari == 0) {
        return false;
    }
    if (monedha_Label.GetText() !== 'Monedha' && $('#hfMonedhaNder').val() !== monedha_Label.GetText() && llogariPunonjesi.KodiMonedha !== ""
        && llogariPunonjesi.KodiMonedha !== $('#hfMonedhaNder').val() && llogariPunonjesi.KodiMonedha !== monedha_Label.GetText()) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukMundTeKryheniVeprimeMePunonjes"));
        furnitori_ComboBox.SetSelectedIndex(-1);
        nvFatura.CollapseAll();
        return false;
    }
    return true;
}

function vendosPunonjes(result, rresht) {
    var emerGride = "#rowed5";
    var grida = $(emerGride);
    var idRreshti;
    if (rresht)
        idRreshti = rresht;
    else idRreshti = result.idRreshti;
    var punonjes = result.punonjesi;
    var llogariPunonjesi = result.llogaria;
    if (punonjes === null || punonjes === undefined || punonjes.IdPunonjes == 0) {
        resetRreshtKorent(idRreshti);
        return;
    }
    if (grida.getTekstQelize('txtIdKodi', idRreshti) == punonjes.IdPunonjes && grida.getTekstQelize('txtLloji', idRreshti) === "Punonjes") {
        grida.setTekstQelize('txtSubjekti', idRreshti, punonjes.NrPersonal);
        return false;
    }
    if (!kontrolloPunonjes(result)) {
        resetRreshtKorent(idRreshti);
        if (!rresht) {
            msgPunonjesPaLlogari(result);
        }
        return;
    }
    var monkurs = hfState.Get('monedhatKurse');
    arrMon[idRreshti] = llogariPunonjesi.KodiMonedha;
    grida.setTekstQelize('txtLloji', idRreshti, "Punonjes");
    grida.setTekstQelize('txtVlera', idRreshti, 0);
    grida.setTekstQelize('txtVleraArketuar', idRreshti, 0);
    grida.setTekstQelize('txtVleraMonBaze', idRreshti, 0);
    grida.setTekstQelize('MeKursFature', idRreshti, false);
    var shuma = 0;
    var idTe = grida.jqGrid('getDataIDs');
    for (var i = 0; i < idTe.length; i++) {
        if (grida.getTekstQelize('txtSubjekti', idTe[i]) !== "") {
            var vlefta = grida.getVlereReale('txtVlera' + idTe[i]); //grida.getTekstQelize('txtVlera', idTe[i]);
            if (vlefta === '.' || vlefta === 'NaN' || vlefta === '')
                vlefta = 0;
            if (pageState.llojDokumenti === "terheqje" || pageState.llojDokumenti === "pagese") {
                if (grida.getTekstQelize('txtDebiKredi', idTe[i]) === 'Debi')
                    shuma -= parseFloat(vlefta);
                else
                    shuma = shuma + parseFloat(vlefta);
            }
            else if (pageState.llojDokumenti === "derdhje" || pageState.llojDokumenti === "arketim" || pageState.llojDokumenti == "arketimLlogariKlienti" || pageState.llojDokumenti == "arketimAbonent") {
                if (grida.getTekstQelize('txtDebiKredi', idTe[i]) === 'Kredi')
                    shuma -= parseFloat(vlefta);
                else
                    shuma = shuma + parseFloat(vlefta);
            }
        }
    }

    shuma = shuma + parseFloat(Utils.HiqPresjet(vlera_TextBox.GetText()) - parseFloat(komision_TextBox.GetText()));

    if (grida.getTekstQelize('txtIdKodi', idRreshti) == punonjes.IdPunonjes && grida.getTekstQelize('txtLloji', idRreshti) == "Punonjes") {
        grida.setTekstQelize('txtSubjekti', idRreshti, punonjes.NrPersonal);
        grida.setTekstQelize('txtVlera', idRreshti, shuma);
        grida.setTekstQelize('txtVleraArketuar', idRreshti, shuma);
        grida.setTekstQelize('txtVleraMonBaze', idRreshti, (shuma * kursi_TextBox.GetText()));
        return;
    }
    grida.setTekstQelize('txtIdKodi', idRreshti, punonjes.IdPunonjes);
    grida.setTekstQelize('txtVlera', idRreshti, shuma);
    grida.setTekstQelize('txtVleraArketuar', idRreshti, shuma);
    grida.setTekstQelize('txtVleraMonBaze', idRreshti, (shuma * kursi_TextBox.GetText()));
    grida.setTekstQelize('txtSubjekti', idRreshti, punonjes.NrPersonal);
    grida.setTekstQelize('txtEmertimi', idRreshti, punonjes.Emer + ' ' + punonjes.Mbiemer);
    grida.setTekstQelize('txtDebiKredi', idRreshti, ktheDebiKrediSipasDok());
    
    if (llogariPunonjesi.IdMonedha == monedhabanka)///kursi i monedhes se fatures ne daten e bankes
        grida.setTekstQelize('txtKursi', idRreshti, kursi_TextBox.GetText());
    else {
        for (var m = 0; m < monkurs.length; m++)
            if (parseInt(monkurs[m].split(';')[0]) === llogariPunonjesi.IdMonedha) {
                grida.setTekstQelize('txtKursi', idRreshti, monkurs[m].split(';')[2]);
                break;
            }
    }
    vendosTotalet();
    return;
}

function SucceededCallbackLlog(llogaria) {
    vendosLlogari(llogaria);
}

function vendosSubjektNgaLupa(vleratSelektuara, lloji) {
    var grida = $("#rowed5");
    var idRresht = grida.getLastSel2();
    $("#txtSubjekti" + idRresht).focus();
    var idTe = "";
    switch (lloji) {

        case "KlientFurnitor":
            vleratSelektuara.forEach(function(value){
                idTe += value[2] + ",";
            });

            $.ajax({
                url: Utils.getServerApiUrl("Rregjistrime", "ktheDisaKlientFurnitorVeprimeKFMeIDRow"),
                data: JSON.stringify({ IDteKf: idTe, rreshti: idRresht, date: data_DateEdit.GetDate(), llojKursi: llojkursi })
            }).done(SucceededCallbackVendosKfPerDisaRreshta);
            break;
        case "Llogari":
            vleratSelektuara.forEach(function (value){
                idTe += value[3] + ",";
            });

            $.ajax({
                url: Utils.getServerApiUrl("Rregjistrime", "KtheVleraLlogarishMeIDRow"),
                data: JSON.stringify({ idLlogarish: idTe, rreshti: idRresht })
            }).done(SucceededCallbackVendosLlogariPerDisaRreshta);
            break;
        case "Punonjes":
            vleratSelektuara.forEach(function (value){
                idTe += value[0] + ",";
            });
            
            $.ajax({
                url: Utils.getServerApiUrl("ListPagesa", "KthePunonjesitMeIdRow"),
                data: JSON.stringify({ idPunonjesish: idTe, rreshti: idRresht })
            }).done(SucceededCallbackVendosPunonjesPerDisaRreshta);
            break;
    }
}

function SucceededCallbackVendosLlogariPerDisaRreshta(pergjigje) {
    var grida = $('#rowed5');
    grida.VendosDisaRreshtaMeSubjekte(pergjigje.result, pergjigje.rreshti, kontrolloKfOseLLogari, vendosLlogari);
}


function vendosLlogari(result, rresht) {
    var grida = $("#rowed5");
    var idRreshti;
    var llogaria; 
    if (result != null) {
        if (rresht) {
            idRreshti = rresht;
            llogaria = result;
        }
        else {
            idRreshti = result[0];
            llogaria = result[1];
        }
       
        var monkurs = hfState.Get('monedhatKurse');
        if (llogaria === null || llogaria === undefined || llogaria.NrLlogari < 1) {
            resetRreshtKorent(idRreshti);
            return;
        }
        if (!kontrolloKfOseLLogari(llogaria)) {
            resetRreshtKorent(idRreshti);
            return;
        }

        arrMon[idRreshti] = llogaria.IdMonedha;
        if (grida.getTekstQelize('txtIdKodi', idRreshti) == llogaria.IdLlogari && grida.getTekstQelize('txtLloji', idRreshti) === "Llogari") {
            grida.setTekstQelize('txtSubjekti', idRreshti, llogaria.NrLlogari);
            return;
        }
        grida.setTekstQelize('txtLloji', idRreshti, "Llogari");
        grida.setTekstQelize('txtVlera', idRreshti, 0);
        grida.setTekstQelize('txtVleraArketuar', idRreshti, 0);
        grida.setTekstQelize('txtVleraMonBaze', idRreshti, 0);
        grida.setTekstQelize('MeKursFature', idRreshti, false);

        var shuma = 0;
        var idTe = grida.jqGrid('getDataIDs');
        for (var i = 0; i < idTe.length; i++) {
            if (grida.getTekstQelize('txtSubjekti', idTe[i]) !== "") {
                var vlefta = grida.getVlereReale('txtVlera' + idTe[i]); //grida.getTekstQelize('txtVlera', idTe[i]);
                if (vlefta === '.' || vlefta === 'NaN' || vlefta === '')
                    vlefta = 0;
                if (pageState.llojDokumenti === "terheqje" || pageState.llojDokumenti === "pagese") {
                    if (grida.getTekstQelize('txtDebiKredi', idTe[i]) === 'Debi')
                        shuma -= parseFloat(vlefta);
                    else
                        shuma = shuma + parseFloat(vlefta);
                }
                else if (pageState.llojDokumenti === "derdhje" || pageState.llojDokumenti === "arketim" || pageState.llojDokumenti == "arketimLlogariKlienti" || pageState.llojDokumenti == "arketimAbonent") {
                    if (grida.getTekstQelize('txtDebiKredi', idTe[i]) === 'Kredi')
                        shuma -= parseFloat(vlefta);
                    else
                        shuma = shuma + parseFloat(vlefta);
                }
            }
        }

        shuma = shuma + parseFloat(Utils.HiqPresjet(vlera_TextBox.GetText()) - parseFloat(komision_TextBox.GetText()));

        if (grida.getTekstQelize('txtIdKodi', idRreshti) == llogaria.IdLlogari && grida.getTekstQelize('txtLloji', idRreshti) == "Llogari") {
            grida.setTekstQelize('txtSubjekti', idRreshti, llogaria.NrLlogari);
            grida.setTekstQelize('txtVlera', idRreshti, shuma);
            grida.setTekstQelize('txtVleraArketuar', idRreshti, shuma);
            grida.setTekstQelize('txtVleraMonBaze', idRreshti, (shuma * kursi_TextBox.GetText()));
            return;
        }
        grida.setTekstQelize('txtIdKodi', idRreshti, llogaria.IdLlogari);
        grida.setTekstQelize('txtVlera', idRreshti, shuma);
        grida.setTekstQelize('txtVleraArketuar', idRreshti, shuma);
        grida.setTekstQelize('txtVleraMonBaze', idRreshti, (shuma * kursi_TextBox.GetText()));
        grida.setTekstQelize('txtSubjekti', idRreshti, llogaria.NrLlogari);
        if (pershk === 1) grida.setTekstQelize('txtEmertimi', idRreshti, llogaria.EmerLlogari1);        
        else grida.setTekstQelize('txtEmertimi', idRreshti, llogaria.EmerLlogari2);
        grida.setTekstQelize('txtDebiKredi', idRreshti, ktheDebiKrediSipasDok());
        if (llogaria.IdMonedha == monedhabanka)///kursi i monedhes se fatures ne daten e bankes
            grida.setTekstQelize('txtKursi', idRreshti, kursi_TextBox.GetText());
        else {
            for (var m = 0; m < monkurs.length; m++)
                if (parseInt(monkurs[m].split(';')[0]) === llogaria.IdMonedha) {
                    grida.setTekstQelize('txtKursi', idRreshti, monkurs[m].split(';')[2]);
                    break;
                }
        }
        vendosTotalet();
    }
}

function SucceededCallbackVleraKodiPerDisaRreshta(result, ids) {
    for (i = 0; i < ids.length; i++) {
        result.idRreshti = ids[i];
        SucceededCallbackVleraKodi(result);
    }
}

function SucceededCallbackVleraKodi(result) {//po
    vendosKf(result);
}

function SucceededCallbackVendosKfPerDisaRreshta(pergjigje) {
    var grida = $('#rowed5');
    grida.VendosDisaRreshtaMeSubjekte(pergjigje.result, pergjigje.idRreshti, kontrolloKfOseLLogari, vendosKf);
}

function kontrolloKfOseLLogari(objekti) {
    if (monedha_Label.GetText() !== 'Monedha' && $('#hfMonedhaNder').val() !== monedha_Label.GetText() && objekti.KodiMonedha !== ""
        && objekti.KodiMonedha !== $('#hfMonedhaNder').val() && objekti.KodiMonedha !== monedha_Label.GetText()) {

        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukMundTeKryheniVeprimeMeKF"));
        furnitori_ComboBox.SetSelectedIndex(-1);
        nvFatura.CollapseAll();
        return false;
    }
    return true;
}

function vendosKf(kfMonKurs, rresht) {
    var grida = $("#rowed5");
    var idRreshti, kf, mon;
    if (rresht) {
        idRreshti = rresht;
        kf = mon = kfMonKurs;
    }
    else {
        idRreshti = kfMonKurs.idRreshti;
        kf = kfMonKurs.oKF;
        mon = kfMonKurs.monedha;
    }
    var idRow = grida.getLastSel2();
    var monkurs = hfState.Get('monedhatKurse');
    if (kf == null || kf == undefined || kf.IdKlientFurnitor < 1) {
        resetRreshtKorent(idRreshti);
        return;
    }

    llojiKF = kf.LlojiKF;
    var lloji = llojiKF ? "Klient" : "Furnitor";
    grida.setTekstQelize('txtLloji', idRreshti, lloji);

    arrMon[idRreshti] = mon.IdMonedha;
    if (!kontrolloKfOseLLogari(mon)) {
        resetRreshtKorent(idRreshti);
        return;
    }

    if (kf.IdKlientFurnitor > 0) {
        if (kf.AktivKF == false) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgKlientFurnitoriNukEshteAktiv"));
            resetRreshtKorent(idRreshti);
            furnitori_ComboBox.SetSelectedIndex(-1);
            nvFatura.CollapseAll();
            //  grid_faturat.PerformCallback(0);
            return;
        }
    }

    if (grida.getTekstQelize('txtIdKodi', idRreshti) == kf.IdKlientFurnitor && grida.getTekstQelize('txtLloji', idRreshti) != "Llogari" && grida.getTekstQelize('txtLloji', idRreshti) != "Punonjes") {
        grida.setTekstQelize('txtSubjekti', idRreshti, kf.KodKlientFurnitor);
        return;
    }
    grida.setTekstQelize('txtVlera', idRreshti, 0);
    grida.setTekstQelize('txtVleraArketuar', idRreshti, 0);
    grida.setTekstQelize('txtVleraMonBaze', idRreshti, 0);
    grida.setTekstQelize('MeKursFature', idRreshti, false);
    grida.setTekstQelize('txtDebiKredi', idRreshti, ktheDebiKrediSipasDok());

    var shuma = MerrShumen(grida);

    if (idRreshti == idRow && grida.getTekstQelize('txtSubjekti', idRow) != undefined) {
        if (grida.getTekstQelize('txtIdKodi', idRow) == kf.IdKlientFurnitor && grida.getTekstQelize('txtLloji', idRreshti) != "Llogari" && grida.getTekstQelize('txtLloji', idRreshti) != "Punonjes") {
            grida.setTekstQelize('txtSubjekti', idRow, kf.KodKlientFurnitor);
            return;
        }
        grida.setTekstQelize('txtIdKodi', idRreshti, kf.IdKlientFurnitor);
        grida.setTekstQelize('txtVlera', idRreshti, shuma);
        grida.setTekstQelize('txtSubjekti', idRreshti, kf.KodKlientFurnitor);
        grida.setTekstQelize('txtFatura', idRreshti, '');
        grida.setTekstQelize('txtEmertimi', idRreshti, kf.EmertimiKF);
        if (mon.IdMonedha == monedhabanka)///kursi i monedhes se fatures ne daten e bankes
            grida.setTekstQelize('txtKursi', idRreshti, kursi_TextBox.GetText());
        else {
            for (var m = 0; m < monkurs.length; m++)
                if (parseInt(monkurs[m].split(';')[0]) == mon.IdMonedha) {
                    grida.setTekstQelize('txtKursi', idRreshti, monkurs[m].split(';')[2]);
                    break;
                }
        }

        $('#txtDebiKredi' + idRow).removeAttr("disabled");
    }
    else {
        if (grida.getTekstQelize('txtIdKodi', idRreshti) == kf.IdKlientFurnitor && grida.getTekstQelize('txtLloji', idRreshti) != "Llogari" && grida.getTekstQelize('txtLloji', idRreshti) != "Punonjes") {
            grida.setTekstQelize(arrayIdKolonaGrides[2], idRreshti, kfMonKurs.oKF.KodKlientFurnitor);
            return;
        }
        grida.setTekstQelize('txtIdKodi', idRreshti, kf.IdKlientFurnitor);
        grida.setTekstQelize('txtVlera', idRreshti, shuma);
        grida.setTekstQelize('txtSubjekti', idRreshti, kf.KodKlientFurnitor);
        grida.setTekstQelize('txtFatura', idRreshti, '');
        grida.setTekstQelize('txtEmertimi', idRreshti, kf.EmertimiKF);
        grida.setTekstQelize('txtVleraArketuar', idRreshti, shuma);
        grida.setTekstQelize('txtVleraMonBaze', idRreshti, (shuma * kursi_TextBox.GetText()));
        if (mon.IdMonedha == monedhabanka)///kursi i monedhes se fatures ne daten e bankes
            grida.setTekstQelize('txtKursi', idRreshti, kursi_TextBox.GetText());
        else {
            for (var m = 0; m < monkurs.length; m++)
                if (parseInt(monkurs[m].split(';')[0]) == mon.IdMonedha) {
                    grida.setTekstQelize('txtKursi', idRreshti, monkurs[m].split(';')[2]);
                    break;
                }
        }
    }

    vendosTotalet();
    callWebServiceInfoKF();
}

function formGridColsArray() {//po
    var hfGridKod = $('#hfGridaKodi');
    var IdKonfigAmbjenteLupat = [{ hfVar: hfGridKod, kodiText: "txtSubjekti" }];

    myJQGrid.formArrayKolGrides($("input[id$='HfGridCol']"), arrayIdKolonaGrides, arrayPershkrimiKolonaGrides, arrayVisibleKolonaGrides, arrayReadOnlyKolonaGrides, pageState.IsLidhur, arrayWidthKolonaGrides, IdKonfigAmbjenteLupat, arrayRenditjeKolonaGrides);
}

function myelemIdKodi(value, options) {//po
    var idRresht = $('#rowed5').getLastSel2();
    disabled = arrayReadOnlyKolonaGrides[12];
    return myJQGrid.myElemIdKodi(value, options, disabled, idRresht, arrayIdKolonaGrides[12]);
}

/*
Function: myelemCombo

Nderton combo-n Lloji per griden. Combo ka vlerat: Klient, Furnitor, Llogari, Punonjes
*/
function myElemLloji(value) {
    var idRow = $('#rowed5').getLastSel2();
    var disabled = arrayReadOnlyKolonaGrides[1] == 'True';
    var objTmp;
    var arrayOptions = new Array(arrayMeLloje.ColKonfLlojRreshtiVlere.length);
    for (var i = 0; i < arrayMeLloje.ColKonfLlojRreshtiVlere.length; i++) {
        objTmp = new Object();
        objTmp.value = arrayMeLloje.ColKonfLlojRreshtiVlere[i].IdLlojRreshti.toString();
        objTmp.text = arrayMeLloje.ColKonfLlojRreshtiVlere[i].KodLlojRreshti.toString();
        arrayOptions[i] = objTmp;
    }
    if (value == '')
        value = pageState.llojSubjektiDefault;
    return myJQGrid.myElemCombo(value, arrayIdKolonaGrides[1], idRow, IndexChangedLloji, arrayOptions, disabled);
}

/*
Function: myElemSubjekti

Nderton nje textbox dhe nje buton per te zgjedhur llogarite ose klientin/furnitorin sipas llojit te zgjedhur tek combo Lloji
*/
function myElemSubjekti(value, options) {
    var idRow = $('#rowed5').getLastSel2();
    if (value == '') {
        if (pageState.subjektiDefault != 0) {
            switch (pageState.llojSubjektiDefault) {
                case "Klient":
                case "Furnitor":
                    $.ajax({
                        url: Utils.getServerApiUrl("Rregjistrime", "ktheKlientFurnitorVeprimeKFMeIDRow"),
                        data: JSON.stringify({ IDkf: pageState.subjektiDefault, rreshti: idRow, date: data_DateEdit.GetDate(), llojKursi: llojkursi })
                    }).done(SucceededCallbackVleraKodi);
                    break;
                case "Llogari":
                    $.ajax({
                        url: Utils.getServerApiUrl("Rregjistrime", "KtheVleraLlogMeIDRow"),
                        data: JSON.stringify({ idja: pageState.subjektiDefault, rreshti: idRow })
                    }).done(SucceededCallbackLlog);
                    break;
                case "Punonjes":
                    $.ajax({
                        url: Utils.getServerApiUrl("ListPagesa", "kthePunonjesMeIdRow"),
                        data: JSON.stringify({ idPunonjes: pageState.subjektiDefault, rreshti: idRow })
                    }).done(SucceededCallbackPunonjes);
                    break;
            }
        }
        else if (furnitori_ComboBox.GetValue() != undefined && pageState.llojSubjektiDefault != "Llogari" && pageState.llojSubjektiDefault != "Punonjes" && pageState.shtimModifikim != "modifikim" ) {
            $.ajax({
                url: Utils.getServerApiUrl("Rregjistrime", "ktheKlientFurnitorVeprimeKFMeIDRow"),
                data: JSON.stringify({ IDkf: furnitori_ComboBox.GetValue(), rreshti: idRow, date: data_DateEdit.GetDate(), llojKursi: llojkursi })
            }).done(SucceededCallbackVleraKodi);
        }
    }
    return myJQGrid.myElemKodi(value, options, arrayReadOnlyKolonaGrides[2] == 'True', idRow, arrayIdKolonaGrides[2], ButtonClickSubjekti, KeyPressSubjekti, changeFunc);
}

function myValueButtonFshi(elem, operation, value) {
    var idRow = $('#rowed5').getLastSel2();
    if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1] == 'True' || pageState.IsLidhur)
        return myJQGrid.myValueButtonFshi(true, idRow, "#rowed5");
    else
        return myJQGrid.myValueButtonFshi(false, idRow, "#rowed5");
}

function myElemButtonFshi() {
    var idRow = $('#rowed5').getLastSel2();
    if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1] == 'True' || pageState.IsLidhur)
        return myJQGrid.myElemButtonFshi(true, idRow, "#rowed5", lostFocusKoloneFundit);
    else
        return myJQGrid.myElemButtonFshi(false, idRow, "#rowed5", lostFocusKoloneFundit);
}
function myElemEmertimi(value) {
    var idRow = $('#rowed5').getLastSel2();
    return myJQGrid.myElemEmertimi(value, arrayReadOnlyKolonaGrides[3], idRow, arrayIdKolonaGrides[3]);
}

function myElemFatura(value, options) {
    var idRow = $('#rowed5').getLastSel2();
    return myJQGrid.myElemEmertimi(value, arrayReadOnlyKolonaGrides[4], idRow, arrayIdKolonaGrides[4]);
}
function myElemMuaji(value) {//po
    var idRow = $('#rowed5').getLastSel2();
    return myJQGrid.myElemEmertimi(value, arrayReadOnlyKolonaGrides[16], idRow, arrayIdKolonaGrides[16]);
}
function myElemKodi(value) {//po
    var idRow = $('#rowed5').getLastSel2();
    return myJQGrid.myElemEmertimi(value, arrayReadOnlyKolonaGrides[17], idRow, arrayIdKolonaGrides[17]);
}
function myElemVleraFill(value) {//po
    var idRow = $('#rowed5').getLastSel2();
    return myJQGrid.myElemEmertimi(value, arrayReadOnlyKolonaGrides[18], idRow, arrayIdKolonaGrides[18]);
}
function myElemVleraMbetur(value) {//po
    var idRow = $('#rowed5').getLastSel2();
    return myJQGrid.myElemEmertimi(value, arrayReadOnlyKolonaGrides[19], idRow, arrayIdKolonaGrides[19]);
}

function myElemStatusFature(value) {//po
    var idRow = $('#rowed5').getLastSel2();
    return myJQGrid.myElemEmertimi(value, arrayReadOnlyKolonaGrides[20], idRow, arrayIdKolonaGrides[20]);
}

/*
Function: myElemDebiKredi

Nderton nje combobox per te zgjedhur Debi apo Kredi (ne rastin kur subjekti eshte llogari)
*/
function myElemDebiKredi(value) {
    var objNjesi = new Object();
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    objNjesi.value = "2";
    objNjesi.text = "Kredi";
    var arrayOptions = new Array(2);
    arrayOptions[0] = objNjesi;
    objNjesi = new Object();
    objNjesi.value = "1";
    objNjesi.text = "Debi";
    arrayOptions[1] = objNjesi;
    var disabled;
    if (arrayReadOnlyKolonaGrides[5] == 'True' || grida.getTekstQelize('txtFatura', idRow) != "")
        disabled = true;
    else
        disabled = false;
    if (value == "") {
        value = ktheDebiKrediSipasDok();
    }
    return myJQGrid.myElemCombo(value, arrayIdKolonaGrides[5], idRow, vendosTotalet, arrayOptions, disabled);


}

function myElemOpsione(value) {//po
    var idRow = $('#rowed5').getLastSel2();
    var colMagazina = JSON.parse($('#hfOpsione').val());
    var arrayOptions = new Array(0);
    if (colMagazina !== "") {
        arrayOptions = new Array(colMagazina.length);
        for (var i = 0; i < colMagazina.length; i++) {
            var objNjesi = new Object();
            objNjesi.value = colMagazina[i].Id;
            objNjesi.text = colMagazina[i].Pershkrimi;
            arrayOptions[i] = objNjesi;
        }
    }
    var disabled;
    if (arrayReadOnlyKolonaGrides[13] == 'True')
        disabled = true;
    else
        disabled = false;
    if (value === "")
        value = 0;

    return myJQGrid.myElemCombo(value, 'txtOpsione', idRow, change, arrayOptions, disabled);
}

function change() {
}

function myElemZbritja(value, options) {
    var disable = 'False';
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    if (arrayReadOnlyKolonaGrides[7] == 'True')
        disable = 'True';
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, disable, idRow, 'txtZbritja', vendosTotalet, hfFormatNumri, vendosTotalet);
}

function myElemPershkrimi(value, options) {
    var idRow = $('#rowed5').getLastSel2();
    if (value == "")
        value = pershkrimi_Memo.GetText();
    return myJQGrid.myElemEmertimi(value, arrayReadOnlyKolonaGrides[8], idRow, arrayIdKolonaGrides[8]);
}

function myElemNrTel(value, options) {
    var idRow = $('#rowed5').getLastSel2();
    return myJQGrid.myElemEmertimi(value, arrayReadOnlyKolonaGrides[14], idRow, arrayIdKolonaGrides[14]);
}
function myElemKursi(value, options) {
    var idRow = $('#rowed5').getLastSel2();
    return myJQGrid.myElemEmertimi(value, arrayReadOnlyKolonaGrides[15], idRow, arrayIdKolonaGrides[15]);
}

/*
Function: myElemVlera

Nderton nje textbox per te vendosur vleren
*/
function myElemVlera(value, options) {
    var disable = 'False';
    if (arrayReadOnlyKolonaGrides[6] == 'True')
        disable = 'True'; var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, disable, idRow, 'txtVlera', vendosTotalet, hfFormatNumri, vendosTotalet);
}

/*
Function: myElemKreditet

Nderton nje textbox per te vendosur vleren e krediteve
*/
function myElemKreditet(value, options) {
    var disable = 'False';
    if (arrayReadOnlyKolonaGrides[9] == 'True')
        disable = 'True'; var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, disable, idRow, 'txtKreditet', vendosTotalet, hfFormatNumri, vendosTotalet);
}

/*
Function: myElemKreditet

Nderton nje textbox per te vendosur vleren e arketuar
*/
function myElemVleraArketuar(value, options) {
    var disable = 'False';
    if (arrayReadOnlyKolonaGrides[10] == 'True')
        disable = 'True'; var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, disable, idRow, 'txtVleraArketuar', vendosTotalet, hfFormatNumri, vendosTotalet);
}

/*
Function: myElemVleraMonBaze

Nderton nje textbox per te vendosur vleren ne monedhe baze
*/
function myElemVleraMonBaze(value, options) {
    var disable = 'True'; var grida = $('#rowed5');
    var idRow = grida.getLastSel2();

    return myJQGrid.myElemTextBoxVlefteSipasFormatNumri(grida, value, options, disable, idRow, 'txtVleraMonBaze', undefined, hfFormatNumri);
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
    myJQGrid.fshiClicked(index, "#rowed5", inicializoGrideTrupi);
    vendosTotalet();
}

/*
Function: lostFocusKoloneFundit

Percakton veprimin qe kryhet kur heqim fokusin nga kolona e fundit e gride (ruhet rreshti korent dhe shtohet nje rresht i ri bosh i editueshem).
*/
function lostFocusKoloneFundit() {
    var grida = $("#rowed5");
    var idRow = grida.getLastSel2();
    vendosTotalet();
    idRow = grida.lostFocusKoloneFundit();
    Totalet();
}

/*
Function: KeyPressSubjekti

Shton nje rresht bosh ne gride (nese nuk ka) dhe therret funksionin <TextChangedSubjekti>
*/
function KeyPressSubjekti() {
    TextChangedSubjekti();

}

/*
Function: TextChangedSubjekti

Sugjeron listen e llogarive ose te klienteve/furnitoreve kur shkruajme te subjekti.
Shiko funksionin <SucceededCallbackSubjekti>.
*/
function TextChangedSubjekti() {
    var grida = $("#rowed5");
    var idRow = grida.getLastSel2();
    var vlera = grida.getTekstQelize('txtSubjekti', idRow);
    var lloji = grida.getTekstQelize('txtLloji', idRow);
    switch (lloji) {
        case "Llogari":
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "ktheACListeLlogarish"),
                data: JSON.stringify({ infixText: vlera, pershk: pershk, idNdermarrje: hfState.Get('idNdermarrje'), idPerdoruesi: hfState.Get('idPerdoruesi') })
            }).done(SucceededCallbackSubjekti);
            break;
        case "Furnitor":
        case "Klient":
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "ktheArrayKlienteFurnitoresh"),
                data: JSON.stringify({ infixText: vlera, tipKlientFurnitor: lloji == "Furnitor" ? 2 : 1 })
            }).done(SucceededCallbackSubjekti);
            break;
        case "Punonjes":
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("ListPagesa", "ktheACListePunonjesish"),
                data: JSON.stringify({ infixText: vlera, idNdermarrje: hfState.Get('idNdermarrje') })
            }).done(SucceededCallbackSubjekti);
            break;
    }
}

/*
Function: SucceededCallbackSubjekti

Sugjeron listen e llogarive ose te klienteve/furnitoreve kur shkruajme te subjekti.
*/
function SucceededCallbackSubjekti(result) {
    var idRow = $("#rowed5").getLastSel2();
    myJQGrid.SucceededCallbackKodi(result, '#txtSubjekti' + idRow);
}

/*
Function: mbushGrideNgaHiddenFieldi

Merr te dhena nga hidden field-et dhe me to ploteson griden. Hidden field-et plotesohen ne server side kur behet modifikim dokumenti.
*/
function mbushGrideNgaHiddenFieldi(klientFurnitor, llogari, punonjes) {
    var grida = $("#rowed5");
    var idRow = grida.getLastSel2();
    if (arrayPershkrimiKolonaGrides.length == 0)
        return;
    if (pageState.shtimModifikim == "shtim" && $('#hfKthehu').val() != "kthehu" && pageState.subjektiDefault != 0) {
        switch (pageState.llojSubjektiDefault) {
            case "Klient":
            case "Furnitor":
                SucceededCallbackVleraKodi(klientFurnitor);
                break;
            case "Llogari":
                SucceededCallbackLlog(llogari);
                break;
            case "Punonjes":
                SucceededCallbackPunonjes(punonjes);
                break;
        }
        return;
    }
    if ((pageState.shtimModifikim == "modifikim" || pageState.shtimModifikim == "klonim" || pageState.shtimModifikim == "anullim") || $('#hfKthehu').val() == "kthehu") {
        $('#hfKthehu').val('');
        resetCountera();
        var colTrup = JSON.parse($('#HfColTrupBanka').val());
        var colKF = JSON.parse($('#HfColKF').val());
        var colPun = JSON.parse($('#hfColPunonjes').val());
        var colLlog = JSON.parse($('#HfColLlog').val());
        var colKokaShitje = JSON.parse($('#HfColFatShitje').val());
        var colKokaVeprimekf = JSON.parse($('#HfColFatVeprime').val());
        arrNiv = JSON.parse($('#hfNivele').val());
        arrId = JSON.parse($('#hfId').val());
        arrMon = new Array(); arrVlera = new Array();
        var lloji;
        var subjekti;
        var emertimi;
        var fatura;
        var debikredi;
        var zbritja;
        var pershkrimi;
        var vlera;
        var kreditet;
        var vleraarketuar;
        var vleramonbaze;
        var nrtel;
        var opsione;
        var idkodi;
        var kmk;
        var muaji;
        var kodi;
        var vlerafill;
        var vlerambetur;
        var statusFature;
        var meKursFature;
        grida.setLastSel2(1);
        idRow = 1;
        if (colTrup !== "") {
            grida.jqGrid('clearGridData');
            for (var i = 0; i < colTrup.length; i++) {
                if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1] == 'True' || pageState.IsLidhur)
                    be = myJQGrid.myValueButtonFshi('True', idRow, "#rowed5");
                else
                    be = myJQGrid.myValueButtonFshi('False', idRow, "#rowed5");
                lloji = (colTrup[i].Lloji == undefined) ? "" : colTrup[i].Lloji;
                switch (lloji) {
                    case "Llogari":
                        subjekti = (colLlog[i].NrLlogari == undefined) ? "" : colLlog[i].NrLlogari;
                        emertimi = (colLlog[i].EmerLlogari1 == undefined) ? "" : colLlog[i].EmerLlogari1;
                        idkodi = colLlog[i].IdLlogari;
                        arrMon[parseInt(i) + 1] = colLlog[i].IdMonedha;
                        break;
                    case "Punonjes":
                        subjekti = (colPun[i].NrPersonal == undefined) ? "" : colPun[i].NrPersonal;
                        emertimi = (colPun[i].Emer == undefined) ? "" : colPun[i].Emer + " " +colPun[i].Mbiemer;
                        idkodi = colPun[i].IdPunonjes;
                        arrMon[parseInt(i) + 1] = colPun[i].IdMonedha;
                        break;
                    case "Klient":
                    case "Furnitor":
                        subjekti = (colKF[i].KodKlientFurnitor == undefined) ? "" : colKF[i].KodKlientFurnitor;
                        emertimi = (colKF[i].EmertimiKF == undefined) ? "" : colKF[i].EmertimiKF;
                        idkodi = colKF[i].IdKlientFurnitor;
                        arrMon[parseInt(i) + 1] = colKF[i].idMonedha;
                        break;
                }
               
                fatura = '';
                if (colTrup[i].IdFatura !== 0) {
                    if (colKokaShitje[i].IdShitjeKoka !== 0) {
                        fatura = (colKokaShitje[i].NrDok === undefined) ? "" : colKokaShitje[i].NrDok + ',' + new Date(parseInt(colKokaShitje[i].DtDok.replace('/Date(', '').replace(')/', ''))).format('dd/MM/yyyy');
                    }
                    else if (colKokaVeprimekf[i].IdVeprimeKFKoka !== 0) {
                        fatura = (colKokaVeprimekf[i].NrDok === undefined) ? "" : colKokaVeprimekf[i].NrDok + ',' + new Date(parseInt(colKokaVeprimekf[i].DtDok.replace('/Date(', '').replace(')/', ''))).format('dd/MM/yyyy');
                    }
                }

                opsione = (colTrup[i].OpsionePagese == undefined) ? "" : colTrup[i].OpsionePagese;
                nrtel = (colTrup[i].NrTel == undefined) ? "" : colTrup[i].NrTel;
                debikredi = (colTrup[i].DebiKredi == undefined) ? "" : colTrup[i].DebiKredi;
                if (pageState.shtimModifikim == "anullim")
                    if (debikredi == 'Debi') debikredi = 'Kredi';
                    else debikredi = 'Debi';
                zbritja = (colTrup[i].Zbritja == undefined || colTrup[i].Zbritja == "") ? "" : parseFloat(colTrup[i].Zbritja);
                pershkrimi = (colTrup[i].PershkrimiTrupi == undefined) ? "" : colTrup[i].PershkrimiTrupi;
                vlera = (colTrup[i].VleraPaguar == undefined || colTrup[i].VleraPaguar === "") ? "" : parseFloat(colTrup[i].VleraPaguar);
                arrVlera[parseInt(i) + 1] = colTrup[i].VleraPaguarMonedhaBaze / colTrup[i].KMK;
                kmk = (colTrup[i].KMK == undefined || colTrup[i].KMK === "") ? "" : parseFloat(colTrup[i].KMK);
                meKursFature = (colTrup[i].MeKursFature == undefined || colTrup[i].MeKursFature === "") ? false : colTrup[i].MeKursFature;
                kreditet = (colTrup[i].Kreditet == undefined || colTrup[i].Kreditet === "") ? "" : parseFloat(colTrup[i].Kreditet);
                vleraarketuar = (colTrup[i].VleraPaArketueshme == undefined || colTrup[i].VleraPaArketueshme === "") ? "" : parseFloat(colTrup[i].VleraPaArketueshme);
                vleramonbaze = (colTrup[i].VleraPaguarMonedhaBaze == undefined || colTrup[i].VleraPaguarMonedhaBaze === "") ? "" : parseFloat(colTrup[i].VleraPaguarMonedhaBaze);
                muaji = (colTrup[i].Muaji == undefined) ? "" : colTrup[i].Muaji;
                kodi = (colTrup[i].Kodi == undefined) ? "" : colTrup[i].Kodi;
                vlera = (colTrup[i].VleraPaArketueshme == undefined || colTrup[i].VleraPaArketueshme == "") ? "" : parseFloat(colTrup[i].VleraPaArketueshme);

                vlerafill = (colTrup[i].VleraFillestare == undefined || colTrup[i].VleraFillestare == "") ? "" : parseFloat(colTrup[i].VleraFillestare);
                vlerambetur = (colTrup[i].VleraMbetur == undefined || colTrup[i].VleraMbetur == "") ? "" : parseFloat(colTrup[i].VleraMbetur);
                statusFature = (colTrup[i].StatusFature == undefined) ? "" : colTrup[i].StatusFature;
                var datarow = {
                    txtNrRendor: i + 1, txtLloji: lloji, txtSubjekti: subjekti, txtEmertimi: emertimi, txtFatura: fatura, txtDebiKredi: debikredi, txtVlera: vlera,
                    txtZbritja: zbritja, txtPershkrimi: pershkrimi, txtKreditet: kreditet, txtVleraArketuar: vleraarketuar, txtVleraMonBaze: vleramonbaze, txtIdKodi: idkodi, txtOpsione: opsione, txtNrTel: nrtel, txtKursi: kmk, txtMuaji: muaji, txtKodi: kodi, txtVleraFill: vlerafill, txtVleraMbetur: vlerambetur, StatusFature: statusFature, txtFshi: be
                };
                var su = grida.jqGrid('addRowData', parseInt(idRow), datarow);
                grida.vendosTeDhenaPerQelizen("txtFatura", idRow, "IdFatura", colTrup[i].IdFatura);
                grida.setTekstQelize('txtNrRendor', idRow, grida.getInd(idRow, false));
                grida.setTekstQelize('txtVlera', idRow, vlera);
                grida.setTekstQelize('txtZbritja', idRow, zbritja);
                grida.setTekstQelize('txtKreditet', idRow, kreditet);
                grida.setTekstQelize('txtVleraArketuar', idRow, vleraarketuar);
                grida.setTekstQelize('txtVleraMonBaze', idRow, vleramonbaze);
                grida.setTekstQelize('txtKursi', idRow, kmk);

                grida.setTekstQelize('txtMuaji', idRow, muaji);
                grida.setTekstQelize('txtKodi', idRow, kodi);
                grida.setTekstQelize('txtVleraFill', idRow, vlerafill);
                grida.setTekstQelize('txtVleraMbetur', idRow, vlerambetur);
                grida.setTekstQelize('StatusFature', idRow, statusFature);
                grida.setTekstQelize('MeKursFature', idRow, meKursFature);

                idRow = idRow + 1; grida.setLastSel2(idRow);
            }
            Totalet();
        }
    }
}

var arrLloji = new Array();
var arrSubjekti = new Array();
var arrEmertimi = new Array();
var arrDebiKredi = new Array();
var arrPershkrimi = new Array();
var arrZbritja = new Array();
var arrKreditet = new Array();
var arrVlera = new Array();
var arrVleraArketuar = new Array();
var arrVleraMonBaze = new Array();
var arrFatura = new Array();
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

var identikuesPerPopupKlientFurnitori;
var identikuesPerPopupLlogari;
var identifikuesPerPopupDokumentat;

var veprimiCombo;
var combo = false; //behet true nese zgjidhet nje klientfurnitor nga comboja
var comboKreditet = false;
//anullon veprimin e enterit

function changeName() {
    myFaqeCelje.shtoHandlerSession();
    try {
        if (Utils.getUrlVar('id') == "undefined")
            window.parent.callWebServiceKtheInfoLart('ShtoVeprimBanka.aspx?lloji=' + pageState.llojDokumenti, 0);
        else
            window.parent.callWebServiceKtheInfoLart('ShtoVeprimBanka.aspx?lloji=' + pageState.llojDokumenti, Utils.getUrlVar('id'));
        window.parent.createCookie('adresa', window.location.href, 1);
    } catch (e) { }
}

/*
Function: ButtonClickKerko

Hap lupen me listen e dokumentave
*/
function ButtonClickKerko(listUrl) {
    var niv = veprimi_ComboBox.GetValue();
    var arkabanka = banka_ComboBox.GetText().split(' (')[0];
    var degeAdmin = '';
    if (cmbDegeAdministrative.GetSelectedItem() != null)
        degeAdmin = cmbDegeAdministrative.GetSelectedItem().GetColumnText('Kodi');
    var queryString = {
        veprimi: 'VeprimeBanka',
        niveli: niv,
        idKonfigAmbjente: 6706,
        lloji: pageState.llojDokumenti,
        njesia: arkabanka,
        degeAdmin: degeAdmin,
        listUrl: listUrl
    };
    popupUniversal.SetContentUrl('LupaDokumenta.aspx?' + Utils.KonvertoObjectQueryString(queryString));

    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhDokumentin"), 'LupaDokumenta.aspx?' + Utils.KonvertoObjectQueryString(queryString), 950, 600);
}

/*
Function: callWebserviceKonfigurimi

Therret funksionin <ktheKonfigurim> per te vendosur konfigurimin per veprimet e bankes.
Shiko funksionin <SucceededCallbackKonfigurimi>.
*/
function callWebserviceKonfig(idKomp, kodKonf, init) {
    var idGjuha = hfState.Get('idGjuha');
    var idPerdoruesi = hfState.Get('idPerdoruesi');
    var idNdermarrje = hfState.Get('idNdermarrje');
    var idObjekti = -1;
    if (pageState.shtimModifikim !== "shtim")
        idObjekti = banka_ComboBox.GetValue() === null ? -1 : banka_ComboBox.GetValue();
    var shtim = $("#hfShtimModifikim").val() === "modifikim" ? false : true;
    var grida = $("#rowed5");
    var idRow = grida.getLastSel2();
    var arkaBankaFillestareLs = pageState.mySessionStorage.getObject("banka_ComboBox_fillestare" + konfigurimi_ComboBox.GetValue());
    var arkaBankaLs = pageState.mySessionStorage.getObject("banka_ComboBox" + konfigurimi_ComboBox.GetValue());

    var idVleraCombos = banka_ComboBox.GetValue() ? banka_ComboBox.GetValue() : 0;
    var idArkaBankaLocalStorage = arkaBankaLs ? arkaBankaLs.value : idVleraCombos;
    //nqs jemi ne init duhet te marr arken fillestare, nqs nuk jemi ne init te marre vleren e arkes qe ka ruajtur ne ls nqs kushti ka qene po perndryshe -1
    var idArkaBankaFillestareLocalStorage = init ? (arkaBankaFillestareLs ? arkaBankaFillestareLs.value : idVleraCombos) : (arkaBankaLs ? arkaBankaLs.value : - 1);

    var llojiKF = pageState.llojDokumenti === "derdhje" || pageState.llojDokumenti === "arketim" || pageState.llojDokumenti === "arketimLlogariKlienti" || pageState.llojDokumenti === "arketimAbonent";
    var item = furnitori_ComboBox.GetSelectedItem();

    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "merrTeDhenaKonfigurimiVeprimeBanka"),
        data: JSON.stringify({
            idKomp: idKomp, kodKonf: kodKonf, kodKontrolli: 'banka_ComboBox', idObjekti: idObjekti,
            shtim: shtim, merrFormatKursi: true, merrGjitheKonf: false, idGjuha: idGjuha, idNdermarrje: idNdermarrje, idPerdoruesi: idPerdoruesi,
            status: lblStatusAprovimi.GetText(), idkokashitje: Utils.getNumberOrDefaultFromUrl("id"), kodkonf: konfigurimi_ComboBox.GetText(),
            idlloji: 3, rreshti: idRow, data: data_DateEdit.GetDate(), idKonfigurimi: konfigurimi_ComboBox.GetValue(),
            idArkaBankaLocalStorage: idArkaBankaLocalStorage, idArkaBankaFillestareLocalStorage: idArkaBankaFillestareLocalStorage,
            EmertimiKF: item ? item.text : "", LlojiKF: llojiKF, dataKurs: new Date().toJSON().slice(0, 10).replace(/-/g, '/')
        })
    }).done(SucceededCallbackInit);

}

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}

function SucceededCallbackInit(result) {
    SucceededCallbackKonfig(result);
    Utils.SucceededCallbackGrupimDokumentash(result.grupimDok);
    hfState.Set('monedhatKurse', result.monedhatkurs);
}
/*
Function: SucceededCallbackKonfigurimi

U vendos atributeve te kontrolleve vlerat e konfigurimit perkates.
*/
var LlojiDefault = "0";
var kushtet;
var colGrida;

function SucceededCallbackKonfig(fullResult) {
    var kontrolloNrAutomatik = true;
    var result = fullResult.konfig;
    var hf = $("input[id$='hfShtimModifikim']");
    var hfLidhur = $("input[id$='hfLidhur']");
    pageState.shtimModifikim = hf.val();
    pageState.IsLidhur = hfLidhur.val() === 'True';
    if (pageState.shtimModifikim === "shtim" && $('#hfKthehu').val() === '')
        pastroFushatKokes(true);

    var arrTabela = ['tblFillim', 'tblFund'];
    var arrPrind = ["dvFillim", "dvFund"];
    var colKontrollet = result.colKontroll;  //[0];
    var colAtrTrupi = result.colAtrTrupi;  //[1];
    var grida = $('#rowed5');
    var statusAprovimi = lblStatusAprovimi.GetText();
    if (hf.val() !== 'klonim' && (($('#hfTeDrejtaModSkema').val() === "False" && statusAprovimi === 'Per Aprovim') || (statusAprovimi === 'Aprovuar' || statusAprovimi === 'Refuzuar'))) {
        pageState.aprovim = true;
        pageState.IsLidhur = true;
    }

    var vlerat = myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, undefined, hf, '', arrTabela, undefined, undefined, hfLidhur, arrPrind, pageState.aprovim);
    $("#divgride1").show();
    $("#dvFillim").show();
    $("#dvFund").show();
    if (furnitori_ComboBox.GetValue() !== null)
        VendosTeDhenaFurnitoriNeTrup(true);
    pageState.formatNumri = result.formatNumri;
    pageState.formatKursi = result.formatKursi;
    colGrida = result.colGrida.filter(function (x) { return x.GridKokaEmri == "grid_trupi"; }); //[2];
    var kushte = result.colKushte; //[3];
    var colAlterKusht = result.colAlterKusht;  //[4];
    var konfLlojRreshti = result.konfLlojRreshti;  //[6];
    $("input[id$='HfGridCol']").val(JSON.stringify(colGrida));
    var hfBank = $("#hfLupaBanka");
    var hfKlFur = $("#hfLupaKlientFurnitor");
    var hfLlog = $("#hfLupaLlogarite");
    var hfAuto = $("#hfLupaAutomjet");
    $('#hfKontrolletNrAutom').val(JSON.stringify(colKontrollet)); $('#hfAtributeNrAutom').val(JSON.stringify(colAtrTrupi));
    if (pageState.shtimModifikim === "shtim" || pageState.shtimModifikim === "anullim" || pageState.shtimModifikim === "klonim") {
        hfNrAuto.Clear();
        hfNrAutoBanka.Clear();
        vendosNrAutomatik(colAtrTrupi, colKontrollet);
        kontrolloNrAutomatik = false;
    }
    for (var i = 0; i <= colKontrollet.length - 1; i++) {
        if (colKontrollet[i].KodKontrolli === "banka_ComboBox") {
            hfBank.val(colAtrTrupi[i].IdKonfigAmbjenteLupa.toString()); //merret id e konfigurimit te lupes per lupen e klient furnitorit ne forme

            continue;
        }
        if (colKontrollet[i].KodKontrolli === "furnitori_ComboBox") {
            hfKlFur.val(colAtrTrupi[i].IdKonfigAmbjenteLupa.toString()); //merret id e konfigurimit te lupes per lupen e magazines ne forme
            continue;
        }
        if (colKontrollet[i].KodKontrolli === "kredite_ButtonEdit") {
            hfLlog.val(colAtrTrupi[i].IdKonfigAmbjenteLupa.toString());
            continue;
        }
        if (colKontrollet[i].KodKontrolli === "btneAutomjet") {
            hfAuto.val(colAtrTrupi[i].IdKonfigAmbjenteLupa.toString());
            continue;
        }
        if (colKontrollet[i].KodKontrolli === "kursi_TextBox") {

            if (colAtrTrupi[i].VlereDefault !== "")
                llojkursi = colAtrTrupi[i].VlereDefault;
            else
                llojkursi = 1;

            if (colAtrTrupi[i].Enabled === false) {
                enablekurs = colAtrTrupi[i].Enabled;
                kursi_TextBox.SetEnabled(false);
            }
            else {

                switch (colAtrTrupi[i].Identifikues) {
                    case 1:
                        kursi_TextBox.SetEnabled(false);
                        break;
                    case 2:
                    case 3:
                        if (pageState.IsLidhur)
                            kursi_TextBox.SetEnabled(false);
                        else if ((pageState.shtimModifikim === "shtim" || pageState.shtimModifikim === "modifikim" || pageState.shtimModifikim === "klonim") && parseFloat(kursi_TextBox.GetText()) !== 1)
                            kursi_TextBox.SetEnabled(true);
                        else
                            kursi_TextBox.SetEnabled(false);
                        break;

                    default:
                        throw new Error("vlera identifikues e kursit eshte e pavlefshme!");
                }
            }
            continue;
        }
        if (colKontrollet[i].KodKontrolli == "cmbKonfigurimKase") {
            var item = null;
            if (fullResult.KasePerdoruesi && fullResult.KasePerdoruesi != 0)
                item = cmbKonfigurimKase.FindItemByValue(fullResult.KasePerdoruesi);
            else if (colAtrTrupi[i].VlereDefault !== "")
                item = cmbKonfigurimKase.FindItemByValue(colAtrTrupi[i].VlereDefault);

            if (item != null) {
                cmbKonfigurimKase.SetSelectedItem(item);
                localStorage.setItem("kase_key" + pageState.idNdermarrje + "_" + pageState.idPerdoruesi, item.text);
            }
        }
    }
    LlojiDefault = '0';
    var hidField1 = $("#hfKontabilizimi");
    hidField1.val(0);
    pageState.ruajVleratFundit = false;
    $('#hfSkema').val('0');
    mesazhriprintimi = false;
    shfaqlupepaprintuar = false;
    for (j = 0; j < kushte.length; j++) {
        if (kushte[j].Kodi === 'MMK') {
            mesazhriprintimi = (colAlterKusht[j].Alternativa === "Po");
            pageState.kushte['MMK'] = colAlterKusht[j].Alternativa === "Po";
        }
        else if (kushte[j].Kodi === 'PPK') {
            shfaqlupepaprintuar = (colAlterKusht[j].Alternativa === "Po");
            pageState.kushte['PPK'] = shfaqlupepaprintuar;
        }
        else if (kushte[j].Kodi === 'LLD') {
            LlojiDefault = kushte[j].Vlera;
            arrayMeLloje = konfLlojRreshti;
        }
        else if (kushte[j].Kodi === "LSD") {
            pageState.llojSubjektiDefault = colAlterKusht[j].Alternativa;
            pageState.kushte['LSD'] = colAlterKusht[j].Alternativa;
        }
        else if (kushte[j].Kodi === 'SD') {
            pageState.subjektiDefault = kushte[j].Vlera;
            pageState.kushte['SD'] = kushte[j].Vlera;
            if (pageState.llojSubjektiDefault == "Klient" || pageState.llojSubjektiDefault == "Furnitor") {
                $.ajax({
                    data: JSON.stringify({ "idKlientFurnitor": pageState.subjektiDefault }),
                    url: Utils.getServerApiUrl("Rregjistrime", "ktheKlientFurnitorSipasId")
                }).done(function (result) {
                    pageState.klientiDefault = result.klientFurnitor;
                });
            }
        }
        else if (kushte[j].Kodi === 'GJK') {
            if (colAlterKusht[j].Alternativa === 'Jo')
                hidField1.val(0);
            else
                if (colAlterKusht[j].Alternativa === "Direkt")
                    hidField1.val(1);
                else
                    hidField1.val(2);
            pageState.kushte['GJK'] = colAlterKusht[j].Alternativa;
        }
        else if (kushte[j].Kodi === 'RVF') {
            pageState.kushte['RVF'] = (colAlterKusht[j].Alternativa === 'Po');
            pageState.ruajVleratFundit = pageState.kushte['RVF'];
        }
        else if (kushte[j].Kodi === 'SHDPL') {
            if (colAlterKusht[j].Alternativa !== hfTeDrejtaGjitheDokPerTuLikujduar.Get("kushtDokPerLikujdim")) {
                hfTeDrejtaGjitheDokPerTuLikujduar.Set("kushtDokPerLikujdim", colAlterKusht[j].Alternativa);
                grid_faturat.PerformCallback('kushtDokPerLikujdim');
            }
            pageState.kushte["SHPDL"] = colAlterKusht[j].Alternativa;
        }
        else if (kushte[j].Kodi === 'ZSP') {
            pageState.kushte['ZSP'] = kushte[j].Vlera;
            $('#hfSkema').val(kushte[j].Vlera);
            lblStatusApr.SetVisible(false);
            lblStatusAprovimi.SetVisible(false);
            SuccedcallbackSkemaMenu(fullResult.skema);

            if (kushte[j].Vlera !== 0) {
                if (pageState.shtimModifikim === 'modifikim') {
                    lblStatusApr.SetVisible(true);
                    lblStatusAprovimi.SetVisible(true);
                }
            }
        }
        else if (kushte[j].Kodi === 'RRVPS') {
            pageState.kushte["RRVPS"] = colAlterKusht[j].Alternativa;
        }
        else if (kushte[j].Kodi === 'PVKST') {
            pageState.kushte['PVKST'] = colAlterKusht[j].Alternativa;
        }
        else if (kushte[j].Kodi === 'KLPC') {
            pageState.kushte["KLPC"] = colAlterKusht[j].Alternativa;
        }
        else if (kushte[j].Kodi === 'ZIKF') {

            if (kushte[j].Vlera !== "0") {
                infoKf = true;
                idInfoKf = kushte[j].Vlera;
                //$('#hfHapurMbyllur').val('True');
            }
            else
                infoKf = false;
        }
        else if (kushte[j].Kodi === 'ALF') {
            if (colAlterKusht[j].Alternativa === "Jo") {
                if ($("#dvgrid_faturat").is(":visible"))
                    $("#dvgrid_faturat").hide();
            }
            else {
                if (!$("#dvgrid_faturat").is(":visible"))
                    $("#dvgrid_faturat").show();
            }
            pageState.kushte["ALF"] = colAlterKusht[j].Alternativa === "Po";
        }
        else if (kushte[j].Kodi === 'MKBRM') {
            pageState.kushte["MKBRM"] = colAlterKusht[j].Alternativa === "Po";
        }
        else if (kushte[j].Kodi === 'LLMKF') {
            pageState.kushte["LLMKF"] = colAlterKusht[j].Alternativa === "Po";
        }
    }

    grida.jqGrid('GridUnload', "rowed5");
    formGridColsArray();
    grida.setLastSel2(-1);
    grida = $('#rowed5');
    ruajFormatetNeGride(grida, pageState.formatNumri);
    inicializoGrideTrupi();
    pershk = 1;
    if (pageState.shtimModifikim === 'shtim') {
        ndryshodege = true;
    }

    hapMbyllInfo($('#hfHapurMbyllur').val() === 'True');


    var arkaBankaFillestareLs = pageState.mySessionStorage.getObject("banka_ComboBox_fillestare");
    if (arkaBankaFillestareLs) {
        Utils.ShtoNeseNukGjendetDheSelektoCombo(banka_ComboBox, arkaBankaFillestareLs.value, arkaBankaFillestareLs.text);
        ndryshokurs = pageState.shtimModifikim === 'shtim';
        ndryshodege = true;
    }

    mbushGrideNgaHiddenFieldi(fullResult.klientFurnitor, fullResult.llogari, fullResult.punonjes);
    Utils.konfiguroAccorditionPasKonfigDokumenti(varKonfig.identifikuesPerLocalStorageKey);
    for (var count = 0; count < colKontrollet.length; count++)
        if (colKontrollet[count].KodKontrolli === "cmbDegeAdministrative" && colAtrTrupi[count].VlereDefault !== "")
            ndryshodege = false;

    if (pageState.ruajVleratFundit) {
        var degeAdmArkaBankaLs = pageState.mySessionStorage.getObject("cmbDegeAdministrative" + konfigurimi_ComboBox.GetValue());
        var arkaBankaLs = pageState.mySessionStorage.getObject("banka_ComboBox" + konfigurimi_ComboBox.GetValue());
        var dateArkaBankaLs = pageState.mySessionStorage.getObject("data_DateEdit" + konfigurimi_ComboBox.GetValue());

        if (degeAdmArkaBankaLs) {//njejta situate si per banken nqs kemi ne ls atehere kemi perparesi mbi vleren default
            Utils.ShtoNeseNukGjendetDheSelektoCombo(cmbDegeAdministrative, degeAdmArkaBankaLs.value, degeAdmArkaBankaLs.text);
            ndryshodege = false;
        } else if (arkaBankaLs !== null && degeAdmArkaBankaLs === null) {
            cmbDegeAdministrative.SetValue(null);
            ndryshodege = false;
        }
        if (arkaBankaLs) {//nqs kushti per te marre arken e ls eshte po eshte me me prioritet se sa arka qe ka default vete konfigurimi
            Utils.ShtoNeseNukGjendetDheSelektoCombo(banka_ComboBox, arkaBankaLs.value, arkaBankaLs.text);
            ndryshokurs = pageState.shtimModifikim === 'shtim';
            TextChangedBanka(fullResult.arkaBanka, fullResult.furnitori);
        }
        if (dateArkaBankaLs) {
            var HfPeriudhaObj = ktheObjektPeriudhe();
            if (data_DateEdit.GetDate().format('dd/MM/yyyy') === Utils.ktheDateDefault(HfPeriudhaObj).format('dd/MM/yyyy')) { //krahasojme Dt Dokumenti me daten default te periudhes kontabel
                data_DateEdit.SetDate(new Date(dateArkaBankaLs));
                DateChanged(kontrolloNrAutomatik, false);
            }
        }
        else {
            TextChangedBanka(fullResult.arkaBanka, fullResult.furnitori);
        }

    } else {
        TextChangedBanka(fullResult.arkaBanka, fullResult.furnitori);
    }
    if (shfaqlupepaprintuar && cmbDegeAdministrative.GetValue() > 0 && pageState.shtimModifikim !== 'modifikim')
        kontrollofaturaTePaprintuara();

    else if ((pageState.llojDokumenti == "arketimLlogariKlienti" || pageState.llojDokumenti == "arketimAbonent") && pageState.shtimModifikim != 'modifikim') {
        ButtonClickValidim();
    }
    if (Utils.getUrlVar('idfatura') !== typeof (undefined)) //do te lihet keshtu per momentin sepse nuk mund te ruajme faturen ne trup ne ndryshimin e llojit te dokumentit
        konfigurimi_ComboBox.SetEnabled(false);
    $("#DergoArke").css("display", "flex");
    $("#DergoArke").css("flex-wrap", "wrap");
}

function DateChanged(kontrolloNrAutomatik, dateChangedByUser) {
    ndryshokurs = true;
    ndryshodege = false;
    updateGride = !dateChangedByUser;
    merrKursetSipasDates();
    if (pageState.shtimModifikim !== 'modifikim') {
        var hfKontrollet = JSON.parse($('#hfKontrolletNrAutom').val());
        var atributet = JSON.parse($('#hfAtributeNrAutom').val());
        if (typeof kontrolloNrAutomatik === "undefined" || kontrolloNrAutomatik)
            vendosNrAutomatik(atributet, hfKontrollet);
    }
}

function merrKursetSipasDates() {
    try {
        var idNdermarrje = hfState.Get("idNdermarrje");
        var idPerdorues = hfState.Get("idPerdoruesi");

        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheKurseMonedhashSipasDates"),
            data: JSON.stringify({ dtDokumenti: data_DateEdit.GetText(), idNdermarrje: idNdermarrje, idPerdorues: idPerdorues, llojKursi: llojkursi })
        }).done(SucceededCallbackKurseSipasMonedhes);
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi("Ndodhi nje gabim gjate marrjes se kurseve!");
    }
}

function SucceededCallbackKurseSipasMonedhes(result) {
    hfState.Set('monedhatKurse', result);
    TextChangedBanka();
}

var ndryshokurs = false;
/*
Function: ndryshoKonfigurimin

Therret funksionin <callWebserviceKonfigurimi> per te vendosur nje konfigurim te ri.
*/
function ndryshoKonfigurimin(init) {
    var itemSelected = konfigurimi_ComboBox.GetSelectedItem();
    if (itemSelected.texts !== null && itemSelected.texts.length > 1) {
        var pershkKonfigAmb = itemSelected.texts[1];
        var kodi = itemSelected.texts[0];
        lblKonfigurimi.SetText(pershkKonfigAmb);
        $('#kokeKonfigurimi').text(hfState.Get("MenuKokeDokumenti") + ': ' + pershkKonfigAmb);
        if (!init) pastro();
    }
    callWebserviceKonfig(301, kodi, init);
}

/*
Function: ButtonClickBanka

Hap lupen me listen e bankave.
*/
function ButtonClickBanka() {
    var hfBank = $("#hfLupaBanka");
    var queryStr = hfBank.val();
    var header;
    var arkabanka;
    var arka = false;
    if (pageState.llojDokumenti == "derdhje" || pageState.llojDokumenti == "terheqje") {
        arkabanka = 4; header = 'Zgjidh banken';
        arka = true;
    }
    else {
        arkabanka = 3; header = 'Zgjidh arken';
        arka = false;
    }
    myButtonClickLupa.LupaUniversal_Click(header, 'LupaBanka.aspx?idKonfigAmbjente=' + queryStr + "&arkabanka=" + arkabanka + '&arka=' + arka, 600, 600);
}

/*
Function: ButtonClickLlogariKredite

Hap lupen me listen e llogarive (per llogarite kredite)
*/
function ButtonClickLlogariKredite() {
    var hfLlog = $("#hfLupaLlogarite");
    var queryStr = hfLlog.val();
    comboKreditet = true;
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("popupAdministrimiUniversal"), 'LupaLlogaria.aspx?idKonfigAmbjente=' + queryStr, 800, 600);
}

/*
Function: ButtonClickFurnitori

Hap lupen me listen e klienteve/furnitoreve
*/
function ButtonClickFurnitori() {
    var hfKl = $("#hfLupaKlientFurnitor");
    var queryStr = hfKl.val();

    combo = true;
    var url = "";
    if (pageState.llojDokumenti == "terheqje" || pageState.llojDokumenti == "pagese")
        url = 'LupaKlientFurnitor.aspx?KlientApoFurnitor=Furnitor&idKonfigAmbjente=' + queryStr;
    else if (pageState.llojDokumenti == "derdhje" || pageState.llojDokumenti == "arketim" || pageState.llojDokumenti == "arketimLlogariKlienti" || pageState.llojDokumenti == "arketimAbonent")
        url = 'LupaKlientFurnitor.aspx?KlientApoFurnitor=Klient&idKonfigAmbjente=' + queryStr;
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhKF"), url, 600, 600);
}


function ButtonClickPunonjesi() {
    var hfPunonjes = $("#hfLupaPunonjes");
    var queryStr = hfPunonjes.val();
    combo = true;
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhPunonjes"), 'LupaPunonjes.aspx?vjenNga=VeprimeBanka&idKonfigAmbjente=' + queryStr, 800, 600);
    
}
/*
Function: ButtonClickSubjekti

Hap lupen e llogarive ose te klienteve/furnitoreve sipas zgjedhjes se bere te combo Lloji ne gride.
*/
function ButtonClickSubjekti(editor, key) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var lloji = grida.getTekstQelize('txtLloji', idRresht);

    var hfKod = $("#hfGridaKodi");
    var queryStr = hfKod.val();

    combo == false;
    switch (lloji) {
        case "Llogari":
            myButtonClickLupa.LupaUniversal_Click(hfState.Get("popupAdministrimiUniversal"), 'LupaLlogaria.aspx?vjenNga=VeprimeBanka&idKonfigAmbjente=' + queryStr, 800, 600);
            break;
        case "Furnitor":
            myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhFurnitorin"), 'LupaKlientFurnitor.aspx?veprimi=2&idKonfigAmbjente=' + queryStr, 800, 600);
            break;
        case "Klient":
            myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhKlientin"), 'LupaKlientFurnitor.aspx?veprimi=1&idKonfigAmbjente=' + queryStr, 800, 600);
            break;
        case "Punonjes":
            myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhPunonjesin"), 'LupaPunonjes.aspx?vjenNga=VeprimeBanka&idKonfigAmbjente=' + queryStr, 800, 600);
            break;
    }
}


function ButtonClickArkiva() {//po
    popupUniversal.SetHeaderText(hfState.Get("regjisDokZgjidhDokPerTeBashkengjitur"));
    popupUniversal.SetSize(738, 548);
    popupUniversal.SetContentUrl('LupaArkiva.aspx?vjenNga=RegjistrimDokumentash&veprimi=' + pageState.llojDokumenti + '&idDok=' + $('#hfArkivaDokId').val() + "&tmpfolder=" + hfArkiva.Get("rootFolder"));
    popupUniversal.Show();
}
/*
Function: SucceededCallbackLloje

Vendos ne nje array te gjithe llojet (artikull, makro, llogari, text, credit note, nentotali).
*/
function SucceededCallbackLloje(result) {
    arrayMeLloje = result.split("|");
}
/*
Function: mbushArrayLloje

Therret funksionin <ktheLlojeClientSide>.
Shiko funksionin <SucceededCallbackLloje>.
*/
function mbushArrayLloje() {
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheLlojeClientSide"),
        data: JSON.stringify({ text: LlojiDefault })
    }).done(SucceededCallbackLloje);
}


function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ? evt.keyCode : evt.charCode;
}

/*
Function: TextChangedVeprimi

Pastron dokumentin kur ndryshohet lloji i veprimit te bankes (terheqje apo derdhje)
*/
function TextChangedVeprimi() {
    veprimiCombo = veprimi_ComboBox.GetText();

    var mod = false;
    if (pageState.shtimModifikim == 'shtim') {
        mod = false;
        if (Utils.getUrlVar("idfatura") == undefined) {
            pastro();
            pastroFushatKokes(true);
        }
    }
    else mod = true;
    if (pageState.llojDokumenti == 'terheqje' || pageState.llojDokumenti == 'derdhje')
        callWebserviceNiveliNew(veprimi_ComboBox.GetValue(), 'banka', mod);
    else callWebserviceNiveliNew(veprimi_ComboBox.GetValue(), 'arka', mod);
}

/*
Function: TextChangedBanka

Thirret kur ndryshojme banken e selektuar. Kur ndryshon banka ndryshon dhe monedha dhe gjendja sipas bankes se re te zgjedhur.
Shiko funksionin <SucceededCallbackBanka>.
*/
function TextChangedBanka(result, resultFurnitori) {
    if (banka_ComboBox.GetValue() == null) {
        banka_ComboBox.SetText('');
    }
    if (data_DateEdit.GetDate() != null && data_DateEdit.GetText() != "" && data_DateEdit.GetDate() != "Invalid Date" && banka_ComboBox.GetText() != "" && banka_ComboBox.GetValue() != null) {
        if (result) {
            SucceededCallbackMonedhaBanka(result, resultFurnitori);
            return;
        }
        var idNdermarrje = hfState.Get("idNdermarrje");
        var idPerdorues = hfState.Get("idPerdoruesi");

        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "merrKursinMonedhenGjendjenSipasBankesAndDates"),
            data: JSON.stringify({ idBanka: banka_ComboBox.GetValue(), dtDokumenti: data_DateEdit.GetText(), idKonfigurimi: konfigurimi_ComboBox.GetValue(), llojKursi: llojkursi, idNdermarrje: idNdermarrje, idPerdorues: idPerdorues })
        }).done(SucceededCallbackMonedhaBanka);
    }


}

/*
Function: TextChangedFurnitori

Mbush griden e faturave me faturat sipas klientit/furnitorit te zgjedhur.
*/
function TextChangedFurnitori() {
    //nvFatura.ExpandAll();
    VendosTeDhenaFurnitoriNeTrup(false);
    FiltroGrideSipasKlientFurnitor();

}


function TextChangedPunonjesi() {
    //nvFatura.ExpandAll();
    VendosTeDhenaPunonjesiNeTrup(false);
    FiltroGrideSipasPunonjes();

}

function FiltroGrideSipasKlientFurnitor() {
    if (Utils.getUrlVars["lloji"] == "arketimLlogariKlienti" || Utils.getUrlVars["lloji"] == "arketimAbonent")
        return;
    else if (pageState.shtimModifikim == 'shtim')
        grid_faturat.PerformCallback("pastroKF");
}



function FiltroGrideSipasPunonjes() {
    if (Utils.getUrlVars["lloji"] == "arketimLlogariKlienti" || Utils.getUrlVars["lloji"] == "arketimAbonent")
        return;
    else if (pageState.shtimModifikim == 'shtim')
        grid_faturat.PerformCallback("pastroP");
}


function VendosTeDhenaFurnitoriNeTrup(shtoNeCombo) {
    var idkf = furnitori_ComboBox.GetValue();
    if (!idkf)
        return;
    if (pageState.subjektiDefault != 0)
        return;
    var grida = $(pageState.gridaSelector);
    var ids = grida.getDataIDs();
    var idsPerNdryshim = new Array();
    for (i = 0; i < ids.length; i++) {
        var data = grida.getTeDhenaRreshti(ids[i])[0];
        if (data.txtLloji == "Klient" && data.txtSubjekti == "")
            idsPerNdryshim.push(ids[i]);
    }
    if (ids.length > 0 || shtoNeCombo)
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "ktheKlientFurnitorVeprimeKFMeIDRow"),
            data: JSON.stringify({ IDkf: idkf, rreshti: 0, date: data_DateEdit.GetDate(), llojKursi: llojkursi })
        }).done(function (result) {
            var kf = result.oKF;
            if (shtoNeCombo && !(kf == null || kf == undefined || kf.IdKlientFurnitor < 1)) {
                var item = furnitori_ComboBox.GetSelectedItem();
                if (!item) {
                    furnitori_ComboBox.AddItem([kf.KodKlientFurnitor, kf.EmertimiKF], kf.IdKlientFurnitor);
                    furnitori_ComboBox.SetValue(kf.IdKlientFurnitor);
                }
            }
            SucceededCallbackVleraKodiPerDisaRreshta(result, idsPerNdryshim);
        });
}

/*
Function: LostFocusFurnitori

Therret funksionin <ktheEmerKlientFurnitor> per te marre emrin e klientit/furnitorit
*/
function LostFocusFurnitori(result) {
    if (furnitori_ComboBox.GetText() == "")
        FiltroGrideSipasKlientFurnitor();
    if (result) {
        SucceededCallbackEmertimiNew(result);
        return;
    }
    var llojiKF = pageState.llojDokumenti == "derdhje" || pageState.llojDokumenti == "arketim" || pageState.llojDokumenti == "arketimLlogariKlienti" || pageState.llojDokumenti == "arketimAbonent";

    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheEmerKlientFurnitorNew"),
        data: JSON.stringify({ EmertimiKF: furnitori_ComboBox.GetText(), LlojiKF: llojiKF })
    }).done(SucceededCallbackEmertimiNew);


}



function LostFocusPunonjesi(result) {
    if (punonjesi_ComboBox.GetText() == "")
        FiltroGrideSipasPunonjes();
    if (result) {
        SucceededCallbackEmertimiNew(result);
        return;
    }
    var llojiP = pageState.llojDokumenti == "derdhje" || pageState.llojDokumenti == "arketim" || pageState.llojDokumenti == "arketimLlogariKlienti" || pageState.llojDokumenti == "arketimAbonent";

    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheEmerPunonjesi"),
        data: JSON.stringify({ Emer: punonjesi_ComboBox.GetText(), llojiP: llojiP })
    }).done(SucceededCallbackEmertimiNew);


}




/*
Function: callWebserviceNiveli

Therret funksionin <ktheTemplatetNivelit> per te marre temlaten e nivelit.
Shiko funksionin <SucceededCallbackNiveli>.
*/

function callWebserviceNiveliNew(lloji, tipi, mod) {
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheTemplateteNivelit"),
        data: JSON.stringify({ lloji: lloji, veprimi: tipi, mod: mod })
    }).done(SucceededCallbackNiveliNew);
}

function SucceededCallbackNiveliNew(colModelet) {
    if (pageState.shtimModifikim == 'shtim') {
        konfigurimi_ComboBox.ClearItems();
        if (colModelet.length === 0) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukKeniAutorizimPerAsnjeDokument"));
            return;
        }
        for (i = 0; i < colModelet.length; i++) {
            konfigurimi_ComboBox.AddItem([colModelet[i].KodKonfigAmbjente, colModelet[i].PershkrimKonfigAmbjente], colModelet[i].IdKonfigAmbjente); //AddItem(teksti, vlera);
        }

        //konfigurimi_ComboBox.SelectIndex(0);
    }
    ndryshoKonfigurimin(false);
}
function gjendja_labelFormat() {
    var gjendjaFillestare = gjendja_Label.GetText();
    gjendja_Label.SetText(numberWithCommas(parseFloat(gjendjaFillestare)));
}
/*
Function: SucceededCallbackMonedhaBanka

Vendos monedhen dhe gjendjen e bankes kur ndryshon banka e selektuar
*/
function SucceededCallbackMonedhaBanka(result, resultFurnitori) {
    if (result.mesazh !== "") {
        myMesazh.ShtoMesazhGabimi(result.mesazh);
        banka_ComboBox.SetText('');
        return;
    }
    var banka = result.banka;
    Utils.SelectComboItem(banka_ComboBox, banka.IdBanka, banka.KodiBanka, banka.EmerBanka);
    var grida = $('#rowed5');
    pageState.formatNumri = result.formatMonedhe;
    pageState.formatKursi = result.formatKursi;
    ndryshoKonfigFormatNumri(grida, pageState.formatNumri, pageState.formatKursi);
    vendosKonfigFormatNumri(grida);
    if (pageState.shtimModifikim === 'shtim' || ((pageState.shtimModifikim === 'modifikim' || pageState.shtimModifikim === 'klonim') && pageState.poNdryshojKursinNeModifikim)) {
        if (result.kursifundit !== "") {
            if (result.kursifundit === "0") {
                if (pageState.shtimModifikim === 'shtim')
                    myMesazh.ShtoMesazhGabimi("Vendosni kursin e monedhes");
                kursi_TextBox.SetText(vlDefaultKursi);
            } else
                kursi_TextBox.SetText(result.kursifundit);
        } else
            kursi_TextBox.SetText(vlDefaultKursi);
    }
    pageState.poNdryshojKursinNeModifikim = true;
    if (Utils.getUrlVar('idfatura') !== typeof (undefined) && hfState.Get("gjenerimAuto")) {
        hfState.Set("gjenerimAuto", false);
        kursiParafunidt = kursi_TextBox.GetText();
    }
    else
        kursiParafunidt = kursss;
    kursss = kursi_TextBox.GetText();
    kursifundit = kursi_TextBox.GetText();
    var vleratxt = (vleraMonedhaBaze_TextBox.GetText() * kursiParafunidt) / kursifundit;

    //var vlera_koka = (vleraMonedhaBaze_TextBox.GetText() / kursifundit);

    //var mefatureNeGride = kaRreshtaMeFatureNeGride(grida, grida.getDataIDs());
    //if (mefatureNeGride.gjendur)
    //    vlera_TextBox.SetText(vlera_koka);

    vleraMonedhaBaze_TextBox.SetText(vleratxt);
    plotesoMeFjale();
    if (ndryshodege && pageState.shtimModifikim === "shtim")
        cmbDegeAdministrative.SetValue(result.degeAdministrative === 0 ? null : result.degeAdministrative);
    monedha_Label.SetText(result.kodimonedha);
    gjendja_Label.SetText(numberWithCommas(parseFloat(result.gjendja).toFixed(2)));

    monedhabanka = result.idMonedha;
    if (countMonParafundit === 0) {
        monedhaparafundit = monedhabanka;
        countMonParafundit++;
    }
    hf = $("input[id$='hfMonedhaNder']")[0];
    if (hf.value === monedha_Label.GetText()) {
        kursi_TextBox.SetEnabled(false);
    }
    else {
        if (pageState.IsLidhur)
            kursi_TextBox.SetEnabled(false);
        else kursi_TextBox.SetEnabled(enablekurs);
    }
    if (ndryshokurs) {
        ndryshimKursi();
    }
    grida = $("#rowed5");
    var ids = grida.getDataIDs();
    for (var i = 0; i < ids.length; i++) {
        updateTotalet(ids[i]);
    }
    llogaritVlereNeMonedheBaze();
    LostFocusFurnitori(resultFurnitori);

    if (kursi_TextBox.GetText() == 1 && hfState.Get("IdMonedha") != result.idMonedha)
        myMesazh.ShtoMesazhInformues(hfState.Get("msgKujdesKursiKembimitNje"));
}

function ruajKolonatEGrides(grida) {
    var idGride = colGrida[0].IdKoka;
    var idGjuha = hfState.Get('idGjuha');
    var idPerdoruesi = hfState.Get('idPerdoruesi');
    var idNdermarrje = hfState.Get('idNdermarrje');
    var idViti = hfState.Get('idViti');
    myJQGrid.ruajKolonatEGrides(grida, idGride, idGjuha, idNdermarrje, idViti, idPerdoruesi);
}

function kursiFmatter(cellvalue, options, rowObject) {
    return myJQGrid.kursiFmatter(cellvalue, options, rowObject);
}

function TextChangedPershkrimi(s, e) {//po
    //vendoset magazina e zgjedhur te koka ne rreshtin qe po editohet
    pershkrimi = pershkrimi_Memo.GetText();
    var grida = $("#rowed5");
    var idTe = grida.jqGrid('getDataIDs');
    for (var i = 0; i < idTe.length; i++) {
        grida.setTekstQelize('txtPershkrimi', idTe[i], pershkrimi);
    }
}

var kursifundit = 1;
var monedhabanka = 0;
var kursiParafunidt = 1;
var monedhaparafundit = 0;
var ndryshodege = false;
var countMonParafundit = 0;
//variabel qe perdoret per te percaktuar nese eshte hera e pare qe po i jepet vlere monedhes se parafundit. monedhaparafundit ne fillim duhet te kete vleren e monedhabanka, pastaj ndryshon.

function kaRreshtaMeFatureNeGride(grida, idTe) {
    var eGjetaVangjel = false;
    var idTeMeFature = new Array();
    var idTePaFature = new Array();
    for (var i = 0; i < idTe.length; i++) {
        var index = idTe[i];
        if (arrNiv[index] != undefined && arrNiv[index] != 0) {
            eGjetaVangjel = true;
            idTeMeFature.push(index);
        }
        else
            idTePaFature.push(index);
    }
    return { gjendur: eGjetaVangjel, idTeMeFature: idTeMeFature, idTePaFature: idTePaFature };
}

function ndryshimKursi() {
    var grida = $("#rowed5");
    var idTe = grida.jqGrid('getDataIDs');
    var kursi = kursi_TextBox.GetText();
    if (kursi === '' || isNaN(kursi))
        kursi = 1;
    var kontrollPerRreshtaMeFature = kaRreshtaMeFatureNeGride(grida, idTe);
    if (kontrollPerRreshtaMeFature.gjendur) {
        if (kursiParafunidt != kursss) {
            myMesazh.ShtoPyetje('Deshironi te rillogariten vlerat e trupit, pas modifikimit te kursit?', false);
        }
        else if (updateGride) {
            VendosVleratNeGrideSipasKursit(kontrollPerRreshtaMeFature.idTePaFature, grida, kursi, false);
            VendosVleratNeGrideSipasKursit(kontrollPerRreshtaMeFature.idTeMeFature, grida, kursi, true);
            riselektoFaturatNeGride(kontrollPerRreshtaMeFature.idTeMeFature, grida, kursi);
        }
    }
    else {
        VendosVleratNeGrideSipasKursit(idTe, grida, kursi, false);
    }
    monedhaparafundit = monedhabanka;
    Totalet();
}


function VendosVleratNeGrideSipasKursit(idTe, grida, kursi, meFatura) {
    for (var i = 0; i < idTe.length; i++) {
        var idERradhes = idTe[i];
        if (grida.getTekstQelize('txtSubjekti', idERradhes) == "")
            continue;
        var vlera = grida.getVlereReale('txtVlera' + idERradhes); //grida.getTekstQelize('txtVlera', idERradhes);
        var vleraMbetur = grida.getTekstQelize('txtVleraMbetur', idERradhes);
        var formatnrtxtVleraMbetur = grida.getShifraPasPresjes('txtVleraMbetur', idERradhes);
        var formatnrtxtVleraFill = grida.getShifraPasPresjes('txtVleraFill', idERradhes);
        var vleraFillestare = grida.getTekstQelize('txtVleraFill', idERradhes);

        grida.setTekstQelize('txtVleraMbetur', idERradhes, isNaN(parseFloat(vleraMbetur)) ? 0 : parseFloat(vleraMbetur).toFixed(formatnrtxtVleraMbetur));
        grida.setTekstQelize('txtVleraFill', idERradhes, isNaN(parseFloat(vleraFillestare)) ? 0 : parseFloat(vleraFillestare).toFixed(formatnrtxtVleraFill));
        grida.setTekstQelize('txtVleraArketuar', idERradhes, isNaN(parseFloat(vlera)) ? 0 : parseFloat(vlera).toFixed(pageState.formatVleftaDB));
        grida.setTekstQelize('txtVleraMonBaze', idERradhes, (isNaN(parseFloat(vlera)) || isNaN(parseFloat(kursi))) ? 0 : (parseFloat(vlera) * parseFloat(kursi)).toFixed(pageState.formatVleftaDB));

        if (!meFatura)
            grida.setTekstQelize('txtVlera', idERradhes, isNaN(parseFloat(vlera)) ? 0 : parseFloat(vlera).toFixed(pageState.formatVleftaDB));

        vendosKursinNeGride(idERradhes, grida);
    }
}

function vendosKursinNeGride(idERradhes, grida) {
    var monkurs = hfState.Get('monedhatKurse');
    if (parseInt(arrMon[idERradhes]) == monedhabanka)///kursi i monedhes se fatures ne daten e bankes
        grida.setTekstQelize('txtKursi', idERradhes, kursi_TextBox.GetText());
    else {
        for (var m = 0; m < monkurs.length; m++)
            if (parseInt(monkurs[m].split(';')[0]) == parseInt(arrMon[idERradhes])) {
                grida.setTekstQelize('txtKursi', idERradhes, monkurs[m].split(';')[2]);
                break;
            }
    }
}

function PoClick(s, e) {
    var grida = $("#rowed5");
    var idTe = grida.jqGrid('getDataIDs');
    var kursi = kursi_TextBox.GetText();
    if (kursi === '' || isNaN(kursi))
        kursi = 1;
    var kontrollPerRreshtaMeFature = kaRreshtaMeFatureNeGride(grida, idTe);
    VendosVleratNeGrideSipasKursit(kontrollPerRreshtaMeFature.idTePaFature, grida, kursi, false);
    riselektoFaturatNeGride(kontrollPerRreshtaMeFature.idTeMeFature, grida, kursi);
    return;
}

function JoClick(s, e) {
    var grida = $("#rowed5");
    var idTe = grida.jqGrid('getDataIDs');
    var kursi = kursi_TextBox.GetText();
    if (kursi === '' || isNaN(kursi))
        kursi = 1;
    var kontrollPerRreshtaMeFature = kaRreshtaMeFatureNeGride(grida, idTe);
    VendosVleratNeGrideSipasKursit(kontrollPerRreshtaMeFature.idTePaFature, grida, kursi, false);
    VendosVleratNeGrideSipasKursit(kontrollPerRreshtaMeFature.idTeMeFature, grida, kursi, true);
    return;
}


function merrTeDhenaGridFatura(Faturat, idTe, grida) {
    Utils.kontrolloGridSipasKeyValue(Faturat, "IdDokumenti", 'IdDokumenti;IdKlientFurnitori;VleftaPaLikujduar;NrDokumenti;DtDokumenti;IdKushtPagese;Kursi;IdNiveli;IdMonedha;DtAzhornimi;KursAzhornimi;Vlefta;Pershkrimi;StatusFature;KodiKlientit;EmertimiKf',
        function (result) { SuccededCallBackkontrolloGridSipasKeyValue(result, idTe, grida); });
}

function riselektoFaturatNeGride(idTe, grida, kursi) {
    Utils.shfaqLoadingGif();
    var Faturat = new Array();
    if ($('#hfShtimModifikim').val() !== "modifikim" && $('#hfShtimModifikim').val() !== "klonim") {
        if (pageState.vendosLlojFature) {
            for (var i = 0; i < idTe.length; i++) {
                var idFatura = grida.merrTeDhenaPerQelizen("txtFatura", idTe[i], "IdFatura");
                if (idFatura.toString().substring(0, 1) === "S" || idFatura.toString().substring(0, 1) === "V")
                    Faturat.push(idFatura);
                else
                    Faturat.push("SH" + idFatura);
            }
            merrTeDhenaGridFatura(Faturat, idTe, grida);
        } else {
            for (var i = 0; i < idTe.length; i++) {
                var idFatura = grida.merrTeDhenaPerQelizen("txtFatura", idTe[i], "IdFatura");
                Faturat.push(idFatura);
            }
            merrTeDhenaGridFatura(Faturat, idTe, grida);
        }
    }
    else {
        var faturat = new Array();
        var idKatDok = (pageState.llojDokumenti == "terheqje" || pageState.llojDokumenti == "pagese") ? 2 : 1;
        var idPerdoruesi = hfState.Get("idPerdoruesi");
        var idNdermarrje = hfState.Get('idNdermarrje');
        for (var i = 0; i < idTe.length; i++) {
            var idFatura = grida.merrTeDhenaPerQelizen("txtFatura", idTe[i], "IdFatura");
            idFatura = Utils.HiqParashtesenNgaIdFatura(idFatura);
            var idNiveli = arrNiv[idTe[i]];
            faturat.push({ idFatura: idFatura, idNiveli: idNiveli });
        }
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheTeDhenaFaturashSipasId"),
            data: JSON.stringify({ faturat: faturat, idKatDok: idKatDok, idPerdoruesi: idPerdoruesi, idNdermarrje: idNdermarrje, idDokumenti: Utils.getNumberOrDefaultFromUrl("id"), idNiveli: veprimi_ComboBox.GetSelectedItem().value, idkonfigurim: konfigurimi_ComboBox.GetSelectedItem().value })
        }).done(function (result) { SucceededCallbackKtheTeDhenaFaturashSipasId(result, idTe, grida); });
    }
}
function SuccededCallBackkontrolloGridSipasKeyValue(result, idTe, grida) {
    if (result.length === 0) {
        Utils.hiqLoadingGif();
        return;
    }
    for (var i = 0; i < result.length; i++) {
        OnGridFaturatSelectionComplete(result[i], i, result.length, idTe[i]);
    }
}

function SucceededCallbackKtheTeDhenaFaturashSipasId(result, idTe, grida) {
    var faturatArr = JSON.parse(result);
    for (var i = 0; i < idTe.length; i++) {
        var fatura = new Array();
        var IdFatura = grida.merrTeDhenaPerQelizen("txtFatura", idTe[i], "IdFatura");
        IdFatura = parseInt(Utils.HiqParashtesenNgaIdFatura(IdFatura));
        var objekti = faturatArr.filter(function (f) {
            return f.IdDokumenti === IdFatura;
        })[0];
        if (objekti === null)
            continue;
        fatura.push(objekti.IdDokumenti, objekti.IdKlientFurnitori, objekti.VleftaPaLikujduar, objekti.NrDokumenti, objekti.DtDokumenti, objekti.IdKushtPagese, objekti.Kursi, objekti.IdNiveli, objekti.IdMonedha, objekti.DtAzhornimi, objekti.KursAzhornimi, objekti.Vlefta, objekti.Pershkrimi, objekti.StatusFature, grida.getTekstQelize('txtSubjekti', idTe[i]), grida.getTekstQelize('txtEmertimi', idTe[i]));
        OnGridFaturatSelectionComplete(fatura, i, faturatArr.length, idTe[i]);
    }
}

function OnGridFaturat() {
    var grida = $("#rowed5");
    var idTe = grida.jqGrid('getDataIDs');
    var kursi = kursi_TextBox.GetText();
    if (kursi === '' || isNaN(kursi))
        kursi = 1;
    var indexsel = grida.getLastSel2();
    var first = true;
    var vleraMbetur, vleraFillestare;
    for (var i = 0; i < idTe.length; i++) {
        var idERradhes = idTe[i];
        if (arrNiv[idERradhes] != undefined && arrNiv[idERradhes] != 0) {
            if (kursiParafunidt != kursss && first) {
                myMesazh.ShtoPyetje('Deshironi te ringarkoni vlerat e palikujduara te faturave pas modifikimit te kursit?', false);
                //myMesazh.ShtoMesazh({ type: "confirm", modal: true, cancelClick: JoClick, okClick: PoClick });
                first = false;
            }
            vlera = grida.getVlereReale('txtVlera' + idERradhes); //grida.getTekstQelize('txtVlera', idERradhes);
            vleraMbetur = grida.getTekstQelize('txtVleraMbetur', idERradhes);
            var formatnrtxtVleraMbetur = grida.getShifraPasPresjes('txtVleraMbetur', idERradhes);
            var formatnrtxtVleraFill = grida.getShifraPasPresjes('txtVleraFill', idERradhes);
            vleraFillestare = grida.getTekstQelize('txtVleraFill', idERradhes);

            if (idERradhes != indexsel) {
                grida.setTekstQelize('txtVleraMbetur', idERradhes, vleraMbetur.toFixed(formatnrtxtVleraMbetur));
                grida.setTekstQelize('txtVleraFill', idERradhes, vleraFillestare.toFixed(formatnrtxtVleraFill));
                grida.setTekstQelize('txtVleraArketuar', idERradhes, vlera.toFixed(pageState.formatVleftaDB));
                grida.setTekstQelize('txtVleraMonBaze', idERradhes, (vlera * parseFloat(kursi)).toFixed(pageState.formatVleftaDB));
            }
            else {
                grida.setTekstQelize('txtVleraArketuar', idERradhes, vlera);
                grida.setTekstQelize('txtVleraMonBaze', idERradhes, (vlera * parseFloat(kursi)));
            }
            vendosKursinNeGride(idERradhes, grida);
        }
        else {
            if (grida.getTekstQelize('txtSubjekti', idERradhes) == "")
                continue;
            vlera = grida.getVlereReale('txtVlera' + idERradhes); //grida.getTekstQelize('txtVlera', idERradhes);
            grida.setTekstQelize('txtVlera', idERradhes, vlera);
            grida.setTekstQelize('txtVleraArketuar', idERradhes, vlera);
            grida.setTekstQelize('txtVleraMonBaze', idERradhes, (vlera * parseFloat(kursi)));


            var formatnrvleraMbetur = grida.getShifraPasPresjes('txtVleraMbetur', idERradhes);
            vleraMbetur = grida.getTekstQelize('txtVleraMbetur', idERradhes);
            grida.setTekstQelize('txtVleraMbetur', idERradhes, vleraMbetur.toFixed(formatnrvleraMbetur));

            var formatnrvleraFill = grida.getShifraPasPresjes('txtVleraFill', idERradhes);
            vleraFillestare = grida.getTekstQelize('txtVleraFill', idERradhes);
            grida.setTekstQelize('txtVleraFill', idERradhes, vleraFillestare.toFixed(formatnrvleraFill));

            vendosKursinNeGride(idERradhes, grida);

        }
    }
    monedhaparafundit = monedhabanka;
    Totalet();
}



/*
Function: ValueChangedVlera

Therritet kur ndryshojme vleren e kokes se dokumentit. Ben nje kontroll ne rastin e terheqjes nese vlera e vendosur eshte > se gjendja e bankes.
*/
function TextChangedVlera() {
    var grida = $("#rowed5");
    var lastSel = grida.getLastSel2();
    txtTotaliPaguar.SetText(Utils.HiqPresjet(vlera_TextBox.GetText()).toFixed(pageState.formatNumri.ShifraPasPresjesVlefta));

    if (grida.getTekstQelize('txtVlera', lastSel) === 0 && grida.getTekstQelize('txtSubjekti', lastSel) !== '') {
        var shuma = MerrShumen(grida);
        grida.setTekstQelize('txtVleraArketuar', lastSel, shuma);
        grida.setTekstQelize('txtVleraMonBaze', lastSel, (shuma * kursi_TextBox.GetText()));
        grida.setTekstQelize('txtVlera', lastSel, shuma);
    }
}
function MerrShumen(grida) {
    var shuma = 0;
    var idTe = grida.jqGrid('getDataIDs');
    for (var i = 0; i < idTe.length; i++) {
        if (grida.getTekstQelize('txtSubjekti', idTe[i]) != "") {
            var vlefta = grida.getTekstQelize('txtVleraArketuar', idTe[i]);
            if (vlefta == '.' || vlefta == 'NaN' || vlefta == '')
                vlefta = 0;
            if (pageState.llojDokumenti == "terheqje" || pageState.llojDokumenti == "pagese") {
                if (grida.getTekstQelize('txtDebiKredi', idTe[i]) == 'Debi')
                    shuma -= parseFloat(vlefta);
                else
                    shuma = shuma + parseFloat(vlefta);
            }
            else if (pageState.llojDokumenti == "derdhje" || pageState.llojDokumenti == "arketim" || pageState.llojDokumenti == "arketimLlogariKlienti" || pageState.llojDokumenti == "arketimAbonent") {
                if (grida.getTekstQelize('txtDebiKredi', idTe[i]) == 'Kredi')
                    shuma -= parseFloat(vlefta);
                else
                    shuma = shuma + parseFloat(vlefta);
            }
        }
    }

    shuma = shuma + parseFloat(Utils.HiqPresjet(vlera_TextBox.GetText()) - parseFloat(komision_TextBox.GetText()));
    return shuma;
}
function SuccedcallbackSkemaMenu(result) {

    var ruajItem = ASPxMenu1.GetItemByName('Ruaj');
    if (ruajItem !== null && ruajItem !== undefined)
        ruajItem.SetVisible(result[0]);

    ASPxMenu1.GetItemByName('RuajPrint').SetVisible(result[0]);
    if (pageState.shtimModifikim === 'kthim')
        ASPxMenu1.GetItemByName('Draft').SetVisible(false);
    else
        ASPxMenu1.GetItemByName('Draft').SetVisible(result[1]);
    ASPxMenu1.GetItemByName('Aprovo').SetVisible(result[2]);
    ASPxMenu1.GetItemByName('Refuzo').SetVisible(result[3]);
    ASPxMenu1.GetItemByName('Delego').SetVisible(result[4]);
    ASPxMenu1.GetItemByName('Komento').SetVisible(result[5]);
    ASPxMenu1.GetItemByName('Modifiko').SetVisible(result[6]);

    ASPxMenu1.GetItemByName('Shto').SetVisible(result[8]);
    ASPxMenu1.GetItemByName('Fshi').SetVisible(result[10]);
}
var vlerazerogrid = false;
var arrNiv = new Array();
var arrId = new Array();

/*
Function: merrTeDhena

Merr te dhenat qe ka grida dhe i vendos neper hidden field-e per ti perdorur ne server side
*/
function merrTeDhena() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    grida.jqGrid('saveRow', idRresht, false, 'clientArray');

    resetCountera();
    var niv = new Array();
    var idss = new Array();
    vlerazerogrid = false;
    var idTe = grida.jqGrid('getDataIDs');
    for (var i = 0; i < idTe.length; i++) {
        editorLloji = grida.getTekstQelize('txtLloji', idTe[i]);
        editorSubjekti = grida.getTekstQelize('txtSubjekti', idTe[i]);
        editorEmertimi = grida.getTekstQelize('txtEmertimi', idTe[i]);
        editorFatura = grida.getTekstQelize('txtFatura', idTe[i]);
        editorDebiKredi = grida.getTekstQelize('txtDebiKredi', idTe[i]);
        editorVlera = grida.getTekstQelize('txtVlera', idTe[i]);
        editorZbritja = grida.getTekstQelize('txtZbritja', idTe[i]);
        editorPershkrimi = grida.getTekstQelize('txtPershkrimi', idTe[i]);
        editorKreditet = grida.getTekstQelize('txtKreditet', idTe[i]);
        editorVleraArketuar = grida.getTekstQelize('txtVleraArketuar', idTe[i]);
        editorVleraMonBaze = grida.getTekstQelize('txtVleraMonBaze', idTe[i]);

        var editorfaturattext = editorFatura;
        var fatura = editorfaturattext.split(',');
        arrFatura[counter4] = i.toString() + ":" + fatura[0] + "," + fatura[1] + "," + fatura[2]; //ne array shtohet nr i fatures dhe data e fatures
        //counter4 += 1;
        counter4 = counter4 + 1;
        if (arrNiv[idTe[i]] != undefined) {
            niv[i] = arrNiv[idTe[i]];
            idss[i] = arrId[idTe[i]];
        }
        else {
            niv[i] = 0;
            idss[i] = 0;
        }
        if (parseFloat(editorVleraArketuar) == 0 && (editorSubjekti != "" && editorSubjekti != pageState.klientiDefault.KodKlientFurnitor))
            vlerazerogrid = true;
    }
    var rreshtaTeGrides = grida.getTeDhenaRreshti();
    $('#gridDataObject').val(JSON.stringify(rreshtaTeGrides));
    $('#hfNivele').val(JSON.stringify(niv));
    $('#hfId').val(JSON.stringify(idss));
    unformatoFushaDevi();
}

/*
Function: pastro

Pastrin array-t e perdorura dhe vendos counterat ne 0.
*/
function pastro() {
    arrLloji = new Array();
    arrSubjekti = new Array();
    arrEmertimi = new Array();
    arrDebiKredi = new Array();
    arrPershkrimi = new Array();
    arrZbritja = new Array();
    arrKreditet = new Array();
    arrVlera = new Array();
    arrVleraArketuar = new Array();
    arrVleraMonBaze = new Array();
    arrFatura = new Array();
    $("input[id$='hfLloji']").val('');
    $("input[id$='hfSubjekti']").val('');
    $("input[id$='hfEmertimi']").val('');
    $("input[id$='hfFatura']").val('');
    $("input[id$='hfDebiKredi']").val('');
    $("input[id$='hfVlera']").val('');
    $("input[id$='hfZbritja']").val('');
    $("input[id$='hfPershkrimi']").val('');
    $("input[id$='hfKreditet']").val('');
    $("input[id$='hfVleraArketuar']").val('');
    $("input[id$='hfVleraMonBaze']").val('');
    $("input[id$='hfNivele']").val('');
    $("input[id$='hfMonedha']").val('');
    $("input[id$='hfLidhur']").val('');
    $('#hfKthehu').val('');
    hfArkiva.Clear();
    $('#hfArkivaDokId').val("");
    resetCountera();
    arrNiv = new Array();
    arrId = new Array();
    arrMon = new Array(); arrVlera = new Array();
    ASPxMenu1.GetItemByName('PrintPreview').SetVisible(false);
    ASPxMenu1.GetItemByName('DergoEmail').SetVisible(false);
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

var llojiKF = "";
/*
Function: IndexChangedLloji

Caktivizon combon DebiKredi kur zgjedhim llojin Klient/Furnitor ne gride dhe vendos klientin/furnitorin ekzistues(te zgjedhur te koka e dokumentit) ne gride (nese ka).
*/
function IndexChangedLloji() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    resetRreshtKorent(idRresht);
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

/*
Function: SucceededCallbackSubjekti

Sugjeron listen e llogarive ose te klienteve/furnitoreve kur shkruajme te subjekti.
Shiko funksionin <SucceededCallback>.
*/
function vendosEmertimSubjekti() {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    var subjekti = grida.getTekstQelize('txtSubjekti', idRresht);
    var lloji = grida.getTekstQelize('txtLloji', idRresht);
    if (lloji == 'Llogari') //Llogari
    {
        $('#txtDebiKredi' + idRresht).attr('disabled', false);

        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheEmerLlogarie"),
            data: JSON.stringify({ prefixText: subjekti })
        }).done(SucceededCallback);
    }
    else if (lloji == 'Klient' || lloji == 'Furnitor')//Klient ose furnitor
    {
        {

            if (lloji === "Furnitor")
                $.ajax({
                    pritPergjigje: true,
                    url: Utils.getServerApiUrl("Rregjistrime", "ktheEmerKlientFurnitorNew"),
                    data: JSON.stringify({ EmertimiKF: subjekti, LlojiKF: false })
                }).done(SucceededCallbackNew);
            else
                $.ajax({
                    pritPergjigje: true,
                    url: Utils.getServerApiUrl("Rregjistrime", "ktheEmerKlientFurnitorNew"),
                    data: JSON.stringify({ EmertimiKF: subjekti, LlojiKF: true })
                }).done(SucceededCallbackNew);
        }
    }
}

function vendosNrAutomatik(colAtrTrupi, colKontrollet) {//po
    myNrAuto.vendosNrAutomatik(colAtrTrupi, colKontrollet, data_DateEdit.GetDate());
}

/*
Function: callWebserviceKF

Therret <ktheEmerKlientFurnitor>.
Shiko funksionin <SucceededCallback>.
 var grida = $(pageState.gridaSelector); rezi
    var idRow = grida.getLastSel2(); rezi
*/
function callWebserviceKF(name) {

    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheEmerKlientFurnitorNew"),
        data: JSON.stringify({ EmertimiKF: name })
    }).done(SucceededCallbackNew);

}

/*
Function: callWebserviceValidateDateDokumenti

Therret <ktheIsValidDateDokumenti>
Shiko funksionin <SucceededCallbackDateDokumenti>.
*/
function callWebserviceValidateDateDokumenti(data) {

    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheIsValidDateDokumenti"),
        data: JSON.stringify({ dt: data })
    }).done(SucceededCallbackDateDokumenti);
}

/*
Function: SucceededCallbackDateDokumenti

Tani per tani nuk ben gje.
*/
function SucceededCallbackDateDokumenti(result) {
    return result;
}

/*
Function: SucceededCallback

Vendos emrin e llogarise dhe shton nje rresht bosh (nese jemi te rreshti i fundit).
*/

function SucceededCallback(result) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if (result != "") {
        grida.setTekstQelize("txtEmertimi", idRresht, result.split(';')[0]);
        llojiKF = result.split(';')[2];
    }

    emriKlientit = result.split(';')[0];
    if (monedha_Label.GetText() != 'Monedha' && $('#hfMonedhaNder').val() != monedha_Label.GetText() && result.split(';')[1] != "" && result.split(';')[1] != undefined && result.split(';')[1] != $('#hfMonedhaNder').val() && result.split(';')[1] != monedha_Label.GetText()) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukMundTeKryheniVeprimeMeKF"));
        resetRreshtKorent(idRresht);

        vendosTotalet();
        furnitori_ComboBox.SetSelectedIndex(-1);
        nvFatura.CollapseAll();
    }
}

function SucceededCallbackNew(result) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if (result != "") {
        grida.setTekstQelize("txtEmertimi", idRresht, result.EmertimiKF);
        llojiKF = result.LlojiKF;
    }

    emriKlientit = result.EmertimiKF;
    if (monedha_Label.GetText() != 'Monedha' && $('#hfMonedhaNder').val() != monedha_Label.GetText() && result.KodiMonedha != "" && result.KodiMonedha != undefined && result.KodiMonedha != $('#hfMonedhaNder').val() && result.KodiMonedha != monedha_Label.GetText()) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukMundTeKryheniVeprimeMeKF"));
        resetRreshtKorent(idRresht);
        vendosTotalet();
        furnitori_ComboBox.SetSelectedIndex(-1);
        nvFatura.CollapseAll();
    }
}

function SucceededCallbackEmertimiNew(result) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    emriKlientit = result.EmertimiKF;
    llojiKF = result.LlojiKF;
    if (monedha_Label.GetText() != 'Monedha' && $('#hfMonedhaNder').val() != monedha_Label.GetText() && result.KodiMonedha != "" && result.KodiMonedha != undefined && result.KodiMonedha != $('#hfMonedhaNder').val() && result.KodiMonedha != monedha_Label.GetText()) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukMundTeKryheniVeprimeMeKF"));
        resetRreshtKorent(idRresht);
        furnitori_ComboBox.SetSelectedIndex(-1); nvFatura.CollapseAll();
    }
}

function updateTotalet(rowid) {
    var grida = $("#rowed5");
    var vlera = grida.getTekstQelize('txtVlera', rowid);
    var vleraark = grida.getTekstQelize('txtVleraArketuar', rowid);
    var zbritja = grida.getTekstQelize('txtZbritja', rowid);
    var krediti = grida.getTekstQelize('txtKreditet', rowid);
    if (vlera != "" && isNaN(vlera)) {
        vlera = grida.getVlereDefault('txtVlera');
        grida.setTekstQelize('txtVlera', rowid, vlera);
    }
    if (vleraark != "" && isNaN(vleraark)) {
        vleraark = grida.getVlereDefault('txtVleraArketuar');
        grida.setTekstQelize('txtVleraArketuar', rowid, vleraark);
    }
    if (zbritja != "" && isNaN(zbritja)) {
        zbritja = grida.getVlereDefault('txtZbritja');
        grida.setTekstQelize('txtZbritja', rowid, zbritja);
    }
    if (krediti != "" && isNaN(krediti)) {
        krediti = grida.getVlereDefault('txtKreditet');
        grida.setTekstQelize('txtKreditet', rowid, krediti);
    }
    if (vlera !== '' && kursi_TextBox.GetText() != '') {
        var formatnrvleftaarketuar = grida.getShifraPasPresjes('txtVleraArketuar', rowid);
        grida.setTekstQelize('txtVleraArketuar', rowid, (parseFloat(vlera) - (parseFloat(zbritja) + parseFloat(krediti))));
        var formatnrvleftamonbaze = grida.getShifraPasPresjes('txtVleraMonBaze', rowid);
        grida.setTekstQelize('txtVleraMonBaze', rowid, (parseFloat(grida.getTekstQelize('txtVleraArketuar', rowid)) * parseFloat(kursi_TextBox.GetText())));
    }
    Totalet();
}

/*
Function: vendosTotalet

Llogarit dhe vendos vleren e arketuar dhe vleren ne monedhe baze.
*/
function vendosTotalet() {
    var grida = $("#rowed5");
    var idRresht = grida.getLastSel2();
    updateTotalet(idRresht);
}

function Totalet() {
    var formatNumri = pageState.formatNumri;
    var totalidebi = 0;
    var totalikredi = 0;
    var totaliPaFature = 0;
    var grida = $("#rowed5");
    var lloji = pageState.llojDokumenti;
    var idTe = grida.jqGrid('getDataIDs');

    for (var i = 0; i < idTe.length; i++) {

        if (grida.getTekstQelize('txtSubjekti', idTe[i]) == "") {
            continue;
        }
        var iEditorVlArketuar = parseFloat(grida.getVlereReale('txtVlera' + idTe[i])); 

        if (grida.getTekstQelize('txtMuaji', idTe[i]) == 'Pa fature')
            totaliPaFature += iEditorVlArketuar;
        if (grida.getTekstQelize('txtDebiKredi', idTe[i]) == "Kredi")
            totalikredi = totalikredi + iEditorVlArketuar;
        else
            if (grida.getTekstQelize('txtDebiKredi', idTe[i]) == "Debi")
                totalidebi = totalidebi + iEditorVlArketuar;
        continue;
    }

    if (lloji == "arketimLlogariKlienti" || lloji == "arketimAbonent" || lloji == "arketim") {
        if (pageState.kushte['PVKST'] == 'PO') PlotesoFushatEKokesNgaTrupi(totalikredi);
    }
    var vlera = isNaN(Utils.HiqPresjet(vlera_TextBox.GetText())) ? 0 : Utils.HiqPresjet(vlera_TextBox.GetText());
    var komision = isNaN(parseFloat(komision_TextBox.GetText())) ? 0 : parseFloat(komision_TextBox.GetText());
    if (lloji == 'derdhje') {
        totalidebi = totalidebi + parseFloat(vlera);
        totalikredi = totalikredi + parseFloat(komision);
    }
    else if (lloji == 'terheqje') {
        totalikredi = totalikredi + parseFloat(vlera);
        totalidebi = totalidebi + parseFloat(komision);
    }
    else if (lloji == 'arketim' || lloji == "arketimLlogariKlienti" || lloji == "arketimAbonent") {
        totalidebi = totalidebi + parseFloat(vlera);
    }
    else if (lloji == 'pagese') {
        totalikredi = totalikredi + parseFloat(vlera);
    }
    var totaliFaturave = totalikredi - totaliPaFature;
    txtDebi.SetText(totalidebi.toFixed(pageState.formatVleftaDB));
    txtKredi.SetText(totalikredi.toFixed(pageState.formatVleftaDB));

    txtTotaliZgjedhur.SetText(totaliFaturave.toFixed(formatNumri.ShifraPasPresjesVlefta));
    txtDiferenca.SetText(Math.abs(totalidebi - totalikredi).toFixed(pageState.formatVleftaDB));
    if (pageState.kushte['PVKST'] == 'PO' && pageState.kushte['RRVPS'] == 'PO') {
        var totaliPerTuRrumbullakosur = MerrVlerenRealeTeFaturave() == 0 ? totalikredi : totaliFaturave;
        var vlerSiperme = Math.ceil(totaliPerTuRrumbullakosur);
        if (vlerSiperme != parseFloat(txtTotaliPaguar.GetText()) && Utils.getUrlVar("shtim_modifikim") == "shtim") {
            txtTotaliPaguar.SetText(vlerSiperme);
            shtoRreshtPaFature();
        }
        else txtTotaliPaguar.SetText(totalikredi.toFixed(formatNumri.ShifraPasPresjesVlefta));
    }
    else txtTotaliPaguar.SetText(totalikredi.toFixed(formatNumri.ShifraPasPresjesVlefta));
}

function shtoRreshtPaFature() {
    var grida = jQuery("#rowed5");
    var rreshtaTeGrides = grida.getDataIDs();
    var vlerapafature = parseFloat(txtTotaliPaguar.GetText() - MerrVlerenRealeTeFaturave());
    if (vlerapafature > 0) {
        var selectedFurnitor = furnitori_ComboBox.GetSelectedItem();
        if (pageState.klientiDefault && pageState.klientiDefault.IdKlientFurnitor > 0) {
            VendosVleraNeRreshtPaFature(rreshtaTeGrides, grida, vlerapafature, pageState.klientiDefault.KodKlientFurnitor, pageState.klientiDefault.EmertimiKF);
        }
        else if (selectedFurnitor) {
            var subjekti = selectedFurnitor.GetColumnText('KodKlientFurnitor');
            var emriKlientit = selectedFurnitor.GetColumnText('EmertimiKF');
            VendosVleraNeRreshtPaFature(rreshtaTeGrides, grida, vlerapafature, subjekti, emriKlientit);
        } else {
            VendosVleraNeRreshtPaFature(rreshtaTeGrides, grida, vlerapafature, subjekti, emriKlientit);
        }
    }
}
function VendosVleraNeRreshtPaFature(rreshtaTeGrides, grida, vlerapafature, subjekti, emriKlientit) {
    for (var i = 0; i < rreshtaTeGrides.length; i++) {
        if (grida.getTekstQelize("txtMuaji", rreshtaTeGrides[i]) == 'Pa fature' || grida.getTekstQelize("txtMuaji", rreshtaTeGrides[i]) == '') {
            vlerapafature = vlerapafature.toFixed(2);
            grida.setTekstQelize('txtDebiKredi', rreshtaTeGrides[i], 'Kredi');
            grida.setTekstQelize('txtMuaji', rreshtaTeGrides[i], 'Pa fature');
            grida.setTekstQelize('txtVlera', rreshtaTeGrides[i], vlerapafature.toString());
            grida.setTekstQelize('txtVleraArketuar', rreshtaTeGrides[i], vlerapafature.toString());
            grida.setTekstQelize('txtVleraMonBaze', rreshtaTeGrides[i], (vlerapafature * parseFloat(kursi_TextBox.GetValue())).toString());
            grida.setTekstQelize('txtSubjekti', rreshtaTeGrides[i], (grida.getTekstQelize('txtSubjekti', rreshtaTeGrides[i]) != subjekti) ? (grida.getTekstQelize('txtSubjekti', rreshtaTeGrides[i]) != '' ? grida.getTekstQelize('txtSubjekti', rreshtaTeGrides[i]) : subjekti) : subjekti);
            grida.setTekstQelize('txtEmertimi', rreshtaTeGrides[i], (grida.getTekstQelize('txtEmertimi', rreshtaTeGrides[i]) != emriKlientit) ? (grida.getTekstQelize('txtEmertimi', rreshtaTeGrides[i]) != '' ? grida.getTekstQelize('txtEmertimi', rreshtaTeGrides[i]) : emriKlientit) : emriKlientit);
            grida.setTekstQelize('txtLloji', rreshtaTeGrides[i], (grida.getTekstQelize('txtLloji', rreshtaTeGrides[i]) != 'Klient') ? (grida.getTekstQelize('txtLloji', rreshtaTeGrides[i]) != '' ? grida.getTekstQelize('txtLloji', rreshtaTeGrides[i]) : 'Klient'): 'Klient');
            grida.setTekstQelize('StatusFature', rreshtaTeGrides[i], grida.getTekstQelize('StatusFature', rreshtaTeGrides[i]) != 'Active' ? grida.getTekstQelize('StatusFature', rreshtaTeGrides[i]) : 'Active');
            grida.setTekstQelize('MeKursFature', rreshtaTeGrides[i], false);
            break;
        }
    }         
    monedhaparafundit = monedhabanka;
    Totalet();
}
function MerrVlerenRealeTeFaturave() {
    var vleraReale = 0;
    var grida = jQuery("#rowed5");
    var rreshtaTeGrides = grida.getDataIDs();
    for (var i = 0; i < rreshtaTeGrides.length; i++) {
        if (grida.getTekstQelize("txtMuaji", rreshtaTeGrides[i]) != 'Pa fature' && grida.getTekstQelize("txtMuaji", rreshtaTeGrides[i]) != "") {
            vleraReale += parseFloat(grida.getTekstQelize("txtVlera", rreshtaTeGrides[i]));

        }
    }
    return vleraReale;
}



/*
Function: ekzistonNjeFurnitor

Kontrollon nese eshte zgjedhur nje klient/furnitor te koka e dokumentit apo jo.
*/
function ekzistonNjeFurnitor() {
    var kaFurnitor = false;
    if (furnitori_ComboBox.GetText() != "" && furnitori_ComboBox.GetText() != null && furnitori_ComboBox.GetText() != undefined)
        kaFurnitor = true;
    return kaFurnitor;
}

/*
Function: pastroFushatKokes

Pastron te gjitha fushat te koka e dokumentit.
*/
function pastroFushatKokes(pastrobanke) {
    if (pastrobanke) {
        kursifundit = 1;
        monedhabanka = 0;
        kursiParafunidt = 1;
        monedhaparafundit = 0;
        banka_ComboBox.SetValue(null);
        monedha_Label.SetText('');
        gjendja_Label.SetText('');
        vlera_TextBox.SetText('0.00');
        kursi_TextBox.SetText('1');
        $('#hfqkmesazhiVDK').val('jo');
    }
    cmbGrup1.SetValue(null);
    cmbGrup2.SetValue(null);
    cmbGrup3.SetValue(null);
    kredite_ButtonEdit.SetValue(null);
    nrDokumenti_TextBox.SetText('');
    nrSerial_TextBox.SetText('');
    txtShoqeria.SetText('');
    txtCustomerNr.SetText('');
    txtNrLlogari.SetText('');
    cmbDegeAdministrative.SetValue(null);
    referenca_TextBox.SetText($("input[id$='hfNrRef']").val());
    pershkrimi_Memo.SetText('');
    menyrePagese_ComboBox.SetValue(null);
    if (Utils.getUrlVar('idfatura') == typeof (undefined) || Utils.getUrlVar('idfatura') == null)
        vlera_TextBox.SetText('0.00');
    shuma_TextBox.SetText('');
    vleraMonedhaBaze_TextBox.SetText('0.00');
    komision_TextBox.SetText('0.00');
    txtTotaliZgjedhur.SetText('0.00');
    txtTotaliPaguar.SetText('0.00');
    txtDetyrimi.SetText('0.00');
    txtDetyrimiTerminated.SetText('0.00');
    furnitori_ComboBox.SetSelectedIndex(-1);

    btneAutomjet.SetValue(null);
    txtTarga.SetText('');
    txtFinancieri.SetText('');
    txtDhenesiMarresi.SetText('');
    txtArketari.SetText('');
    txtMbiPagesa.SetText('0.00');
    lblStatusAprovimi.SetText('');
    cbDergoMeEmail.SetChecked(false);
    vendosDateDefault();
    var hf = $("#status1")[0];
    if (hf.value != 'kthehu')
        hf.value = "false";
    nvFatura.CollapseAll();
    grid_faturat.PerformCallback('pastro');
    cmbFormatiPrintimit.SetText('');
    cbPrinto.SetChecked(false);
    hfArkiva.Clear();
    $('#hfArkivaDokId').val("");
}

function nvFaturaClick(s, e) {
    grid_faturat.PerformCallback('mbush');
}
function vendosDateDefault() {
    if (pageState.shtimModifikim == "shtim" || pageState.shtimModifikim == "klonim" || pageState.shtimModifikim == "anullim") {
        var hfPeriudheObj = $.parseJSON(hfState.Get("periudha"));
        if (hfPeriudheObj.emerPeriudha == undefined || hfPeriudheObj.emerPeriudha == "") {
            vendosPeriudhenBanka();
            return;
        }
        vendosPeriudhenBanka();
        DateChanged(true, false);
    }
}

var fillimprint;
/*
Function: EndRequestHandler

Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe inicializon griden.
*/
function EndRequestHandler(sender, args) {
    formatoFushaDevi();
    var hf = $("#status1")[0];
    if ($('#hfqkmesazhi').val() == 'shfaqmesazh' || $('#hfqkmesazhiVDK').val() == 'shfaqmesazh') {

        if ($('#hfqkmesazhi').val() == 'shfaqmesazh') {
            $('#hfqkmesazhi').val('jo');
            myMesazh.ShtoMesazh({ type: "confirm", layout: "center", modal: true, text: hfState.Get("msgDeshironiTeBeniShperndarjenNeQendraKosto"), cancelClick: JopopupClick, okClick: hapPopUp });
        }
        else if ($('#hfqkmesazhiVDK').val() == 'shfaqmesazh') {
            myMesazh.ShtoMesazh({ type: "confirm", layout: "center", modal: true, text: hfState.Get("msgDeshironiTeBeniShperndarjenNeQendraKosto"), cancelClick: JopopupClick, okClick: hapPopUp });
        }
    }
    if ($('#hfqkmesazhi').val() == 'shfaqlupe') {
        myButtonClickLupa.LupaUniversal_Click('Shperndarje ne qendrat e kostos', $('#hfUrl').val(), 900, 600);
        $('#hfUrl').val('');
    }
    if (hf.value == "true" || hf.value == "kthehu") {
        if (hf.value == "kthehu" && $('#hfUrl').val() == "" && $('#hfUrlVDK').val() == '') {
            fillimprint = new Date();
            if (printo)
                CheckWindowState();
            else if ($('#hfqkmesazhi').val() == 'shfaqlupe')
                    myFaqeCelje.kontrolloTeDrejta($('#hfUrl1').val(), true);
                 else myFaqeCelje.kontrolloTeDrejta($('#hfUrl1').val());
        }
        else myFaqeCelje.kontrolloTeDrejta('ShtoVeprimBanka.aspx?lloji=' + Utils.getUrlVar('lloji') + '&shtim_modifikim=shtim', true);
    }
    else if (hf.value == "anullim") {
        konfigurimi_ComboBox.SetSelectedIndex = 0;
        return;
    }
    else
        click = false;
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "MerrMenuPerPerdoruesSipasSkemes"),
        data: JSON.stringify({
            idskema: $('#hfSkema').val(),
            idperdoruesi: hfState.Get("idPerdoruesi"),
            isNotModifikim: (pageState.shtimModifikim == 'modifikim' ? false : true),
            status: lblStatusAprovimi.GetText(),
            idkokashitje: Utils.getNumberOrDefaultFromUrl("id"),
            kodkonf: konfigurimi_ComboBox.GetText(), idlloji: 3
        })
    }).done(SuccedcallbackSkemaMenu);
    for (var i = 0; i < pageState.webhook.length; i++) {
        if (pageState.webhook[i].Kategoria == 2)
            kategoria = "arketim";
        else if (pageState.webhook[i].Kategoria == 3)
            kategoria = "pagese";
        else kategoria = "";
        if ((pageState.webhook[i].Eventi == 1 || pageState.webhook[i].Eventi == 0 || pageState.webhook[i].Event==3) && kategoria == pageState.llojDokumenti && pageState.webhook[i].Aktive == true) {
            if ($('#hfObjektRuajtur').val() != "") {

                $.ajax({
                    type: "POST",
                    url: pageState.webhook[i].Urlpritese,

                    data: $('#hfObjektRuajtur').val(),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response != null) {
                            alert("Eventi : " + response.Event + ", Kategri : " + response.Kategori + ", Mesazh :" + response.Mesazh);
                        } else {
                            Console.log("Something went wrong");
                        }
                    },
                    failure: function (response) {
                        Console.log(response.responseText);
                    },
                    error: function (response) {
                        Console.log(response.responseText);
                    }
                });
            }
        }

    }
}

function CheckWindowState() {
    printo = false;
    setTimeout(function () {
        $('body').on('mousemove click keypress', function () {
            myFaqeCelje.kontrolloTeDrejta($('#hfUrl1').val());
            $('body').off('mousemove click keypress');
        });
    }, 1200);
}

function JopopupClick(s, e) {
    if ($('#hfUrl').val() != '')
        $('#hfUrl').val('');
    else if ($('#hfUrlVDK').val() != '')
        $('#hfUrlVDK').val($('#hfUrlVDK').val().replace($('#hfUrlVDK').val().split(';')[0] + ";", ""));

    if ($('#hfqkmesazhiVDK').val() == 'shfaqmesazh' && $('#hfUrlVDK').val() != '') {
        myMesazh.ShtoMesazh({ type: "confirm", layout: "center", modal: true, text: hfState.Get("msgDoTeBeshShperndarjenNeQKostoTeDifTeKursit"), cancelClick: JopopupClick, okClick: hapPopUp });
    }
    else if ($('#hfqkmesazhiVDK').val() == 'shfaqlupe') {
        if ($('#hfUrlVDK').val() != '') {
            myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgShperndarjeNeQendratEKostos"), $('#hfUrlVDK').val().split(';')[0], 900, 600);
            $('#hfUrlVDK').val($('#hfUrlVDK').val().replace($('#hfUrlVDK').val().split(';')[0] + ";", "")); if ($('#hfUrlVDK').val() == '') $('#hfqkmesazhiVDK').val('jo');
        }
    }
    var hf = document.getElementById("status1");
    var status = hf.value;
    if (status == "kthehu" && ($('#hfUrl').val() == "" && $('#hfUrlVDK').val() == "")) {

        myFaqeCelje.kontrolloTeDrejta($('#hfUrl1').val());
    }
}

function closePopup(s, e) {
    popupUniversal.SetContentUrl('');
    if ($('#hfqkmesazhiVDK').val() == 'shfaqmesazh' && $('#hfUrlmag').val() != '') {
        myMesazh.ShtoMesazh({ type: "confirm", layout: "center", modal: true, text: hfState.Get("msgDoTeBeshShperndarjenNeQKostoTeDifTeKursit"), cancelClick: JopopupClick, okClick: hapPopUp });
    }
    else if ($('#hfqkmesazhiVDK').val() == 'shfaqlupe') {
        if ($('#hfUrlVDK').val() != '') {
            myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgShperndarjeNeQendratEKostos"), $('#hfUrlVDK').val().split(';')[0], 900, 600);
            $('#hfUrlVDK').val($('#hfUrlVDK').val().replace($('#hfUrlVDK').val().split(';')[0] + ";", "")); if ($('#hfUrlVDK').val() == '') $('#hfqkmesazhiVDK').val('jo');
        }
    }
    var hf = document.getElementById("status1");
    var status = hf.value;
    if (status == "kthehu" && ($('#hfUrl').val() == "" && $('#hfUrlVDK').val() == ""))
        myFaqeCelje.kontrolloTeDrejta($('#hfUrl1').val());
    HapLupeValidimiPasLupesPaPrintuar();
}


function hapPopUp(s, e) {
    if ($('#hfUrl').val() != '') {
        myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgShperndarjeNeQendratEKostos"), $('#hfUrl').val(), 900, 600);
        $('#hfUrl').val('');
    }
    else if ($('#hfUrlVDK').val() != '') {
        dokumentat = $('#hfUrlVDK').val().split(';');
        dokumentat.splice(dokumentat.length - 1);
        dokRradhes++;
        myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgShperndarjeNeQendratEKostos"), dokumentat[0], 900, 600);
        $('#hfUrlVDK').val($('#hfUrlVDK').val().replace($('#hfUrlVDK').val().split(';')[0] + ";", ""));
        if ($('#hfUrlVDK').val() == '')
           $('#hfqkmesazhiVDK').val('jo');
    }
}
/*
Function: isValidKoka

Kontrollon nese jane plotesuar fushat e detyrueshme te kokes se dokumentit
*/
function isValidKoka() {
    if (!Utils.nrWsRrugesManager.kanePerfunduarWs()) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgPoTransferohetTeDhenatShtypniPerseriRuaj"));
        return false;
    }
    if (nrDokumenti_TextBox.GetText() === "") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgShenoniNumrinEDokumentit"));
        return false;
    }
    if (banka_ComboBox.GetText() === "") {
        if (pageState.llojDokumenti === "derdhje" || pageState.llojDokumenti == "terheqje")
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniBanken"));
        else myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniArken"));
        return false;
    }
    if (data_DateEdit.GetDate() === null) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniNjeDateDokumenti"));
        return false;
    }
    if (data_regj_DateEdit.GetDate() === null) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhniNjeDateRegjstrimi"));
        return false;
    }
    if (Utils.HiqPresjet(vlera_TextBox.GetText()) === 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgShenoniVleren"));
        return false;
    }
    if (parseFloat(kursi_TextBox.GetText()) === 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgShenoniKursin"));
        return false;
    }
    if (!llogaritTotaleDebiKredi()) {

        return false;
    }
    if (parseFloat(txtTotaliZgjedhur.GetText()) > parseFloat(txtTotaliPaguar.GetText())) {
        myMesazh.ShtoMesazhGabimi('Totali i paguar duhet te jete me i madh se totali i perzgjedhur!');
        return false;
    }
    if (parseFloat($('#hfLimit').val()) < parseFloat(txtTotaliPaguar.GetText())) {
        myMesazh.ShtoMesazhGabimi('Vlera e paguar tejkalon limitin prej ' + $('#hfLimit').val() + ' lek te lejuar me ligj per pagesat me para ne dore. Ju lutem beni korigjimet ne menyre qe te ruani dokumentin!');
        return false;
    }

    if (pageState.kushte['KLPC'] === 'PO') {
        if (parseFloat(txtTotaliPaguar.GetText()) > 150000) {
            myMesazh.ShtoMesazhGabimi("Nuk lejohet te behen pagasa cash qe kalojne vleren 150.000");
            return false;
        }
    }
    if (pageState.llojDokumenti === "arketimAbonent" || pageState.llojDokumenti === "arketimLlogariKlienti") {
        return kontrolloFaturaPerStatusin();
    }
    return true;
}

/*
Function: llogaritTotaleDebiKredi

Llogarit totalin ne debi dhe totalin ne kredi te dokumentit te bankes.Nese keto dy totale nuk jane te barabarte veprimi eshte i pakuadruar
*/
function llogaritTotaleDebiKredi() {
    var vleraBankes = 0;
    var totalKredi = 0;
    var totalDebi = 0;
    if (komision_TextBox.GetText() === '')
        komision_TextBox.SetText(0);
    if (pageState.llojDokumenti === "terheqje") {
        vleraBankes = Utils.HiqPresjet(vlera_TextBox.GetText());
        if (komision_TextBox.GetText() !== "")
            totalDebi = totalDebi + parseFloat(komision_TextBox.GetText());
    }
    else {
        vleraBankes = Utils.HiqPresjet(vlera_TextBox.GetText());
        if (komision_TextBox.GetText() !== "")
            totalKredi = totalKredi + parseFloat(komision_TextBox.GetText());
    }

    var grida = $("#rowed5");
    var idTe = grida.jqGrid('getDataIDs');
    var kaRreshtaNeTrup = false;

    for (var i = 0; i < idTe.length; i++) {

        if (pageState.llojDokumenti === "terheqje" || pageState.llojDokumenti === "pagese") {
            if (grida.getTekstQelize('txtSubjekti', idTe[i]) === "") {
                continue;
            }
            kaRreshtaNeTrup = true;
            if (grida.getTekstQelize('txtDebiKredi', idTe[i]) === "Kredi")
                totalKredi = totalKredi + parseFloat(grida.getTekstQelize('txtVleraArketuar', idTe[i]));
            else
                if (grida.getTekstQelize('txtDebiKredi', idTe[i]) === "Debi")
                    totalDebi = totalDebi + parseFloat(grida.getTekstQelize('txtVleraArketuar', idTe[i]));
            continue;
        }
        else if (pageState.llojDokumenti === "derdhje" || pageState.llojDokumenti === "arketim" || pageState.llojDokumenti === "arketimLlogariKlienti" || pageState.llojDokumenti === "arketimAbonent") {
            if (grida.getTekstQelize('txtSubjekti', idTe[i]) === "") {
                continue;
            }
            kaRreshtaNeTrup = true;
            if (grida.getTekstQelize('txtDebiKredi', idTe[i]) === "Kredi")
                totalKredi = totalKredi + parseFloat(grida.getTekstQelize('txtVleraArketuar', idTe[i]));
            else
                if (grida.getTekstQelize('txtDebiKredi', idTe[i]) === "Debi")
                    totalDebi = totalDebi + parseFloat(grida.getTekstQelize('txtVleraArketuar', idTe[i]));
            continue;
        }
    }


    if (!kaRreshtaNeTrup) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgTrupiDokNukDuhetBosh"));
        return;
    }
    var diferenca = totalDebi - totalKredi;
    //diferenca = Math.round(diferenca * 1000) / 1000;
    var kuadruar;
    if (pageState.llojDokumenti == "terheqje" && diferenca >= 0) {
        kuadruar = diferenca.toFixed(pageState.formatVleftaDB) == Math.abs(vleraBankes.toFixed(pageState.formatVleftaDB));

    }
    else if (pageState.llojDokumenti == "derdhje" && diferenca <= 0) {
        kuadruar = Math.abs(diferenca.toFixed(pageState.formatVleftaDB)) == Math.abs(vleraBankes.toFixed(pageState.formatVleftaDB));

    }
    else if (pageState.llojDokumenti == "pagese" && diferenca >= 0) {
        kuadruar = diferenca.toFixed(pageState.formatVleftaDB) == Math.abs(vleraBankes.toFixed(pageState.formatVleftaDB));

    }
    else if (pageState.llojDokumenti == "arketim" && diferenca <= 0 || pageState.llojDokumenti == "arketimLlogariKlienti" || pageState.llojDokumenti == "arketimAbonent") {
        kuadruar = Math.abs(diferenca.toFixed(pageState.formatVleftaDB)) == Math.abs(vleraBankes.toFixed(pageState.formatVleftaDB));

    }
    else kuadruar = false;

    if (!kuadruar)
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgVeprimiNukEshteIKuadruar"));

    return kuadruar;
}


function kontrolloFaturaPerStatusin() {
    var grida = $("#rowed5");
    var nrFaturashAktive = parseInt(grid_faturat["cpNrFaturashActive"]);
    var nrFaturashTerminated = parseInt(grid_faturat["cpNrFaturashTerminated"]);


    //Kontrollet vlejne vetem ne rastet kur klienti ka fatura me statusin terminated
    if (nrFaturashTerminated == 0)
        return true;

    var totaliPaguar = parseFloat(txtTotaliPaguar.GetText());
    var totaliZgjedhur = parseFloat(txtTotaliZgjedhur.GetText());


    if (totaliZgjedhur == 0) {
        if (nrFaturashAktive > 0)
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhFaturatTerminated"));
        else
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhCaFatura"));
        return false;
    }
    //te mos merret parasysh mbipagesa ne rastin kur ajo ka vetem rrumbullakosje
    if (nrFaturashAktive == 0 && (totaliPaguar - totaliZgjedhur) >= 1) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukLejohetPageseMeEMadheSeTotaliIFaturave"));
        return false;
    }

    var NrFaturashJoTerminatedNeGride = 0;
    var NrFaturashTerminatedNeGride = 0;
    var idTe = grida.getDataIDs();
    for (var i = 0; i < idTe.length; i++) {
        if (eshteRreshtBosh(idTe, i))
            continue;
        var vlera = grida.getTekstQelize("txtVlera", idTe[i]);
        var vleraMbetur = grida.getTekstQelize("txtVleraMbetur", idTe[i]);
        var status = grida.getTekstQelize("StatusFature", idTe[i]);
        var muaji = grida.getTekstQelize("txtMuaji", idTe[i]);

        if (status == "Terminated") {
            if (vleraMbetur > 0)
                NrFaturashTerminatedNeGride++;
            if (parseFloat(vlera) > parseFloat(vleraMbetur)) {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukLejohetPageseMeEMadheSeDetyrim"));
                return false;
            }
        } else {
            if (muaji != "Pa fature" || (muaji == "Pa fature" && vlera > 1))
                NrFaturashJoTerminatedNeGride++;
        }

    }
    if (NrFaturashJoTerminatedNeGride > 0 && NrFaturashTerminatedNeGride < nrFaturashTerminated) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhFaturaTerminated"));
        return false;
    }

    return true;
}

function eshteRreshtBosh(idte, index) {
    var grida = $("#rowed5");
    return grida.getTekstQelize('txtMuaji', idte[index]) == "" && grida.getTekstQelize('txtVlera', idte[index]) == 0;
}

var printo = false;


function menu_click(s, e) {
    if (myMesazh.pyetjeEHapur) {
        e.preventDefault;
        e.processOnServer = false;
    }
    else {
        if (!myJQGrid.checkIsPageReadyToSave(e.item.name)) {
            Utils.shfaqLoadingGif();
            Utils.shtoFunksionNeRadheMeParametra(menuclick, [s, e, true], hfState.Get('idGjuha'), this);
            e.processOnServer = false;
            return;
        }
        menuclick(s, e, false);
    }
}
/*
Function: menuClick

Percakton veprimin qe kryhet nese klikohet nje nga butonat e menuse.
*/
function menuclick(s, e, doPostback) {
    var validim;
    e.processOnServer = true;
    if (Utils.BenPjeseNeNjeNgaKeto(e.item.name, ['Ruaj', "Aprovo", "Refuzo", "Modifiko", "Delego", "RuajPrint", 'Draft'])) {
        if (e.item.name === 'RuajPrint')
            printo = true;
        if ($("input[id$='hfAutorizimi']").val() === 'False') {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukKeniAutorizimPerTeRuajturKeteDok"));
            Utils.hiqLoadingGif();
            e.processOnServer = false;
            click = false;
            return;
        }
        if (e.item.name === 'Ruaj' || e.item.name === 'Draft') {
            if (parseInt($('#hfIdMonedhaNder').val()) == monedhabanka && pageState.kontrolloPerRreshtaLLMKF) {
                kontrolloRreshtatPerKursFature();
                if (pageState.rreshtatPerNdryshimKursFature.length > 0) {
                    e.processOnServer = false;
                    click = false;
                    myMesazh.ShtoMesazh({ type: "confirm", modal: true, text: "Deshironi te llogarisni vlerat me kurs fature?", cancelClick: JoClick_KursFature, okClick: PoClick_KursFature });
                    return;
                }
            }
        }
        validim = myFaqeCelje.validim(s, e);

        if (!validim) {
            e.processOnServer = false;
            click = false;
            return;
        }
        if (isValidKoka()) {
            Utils.shfaqLoadingGif();
            RuajClick(s, e);
            Utils.doPostback(s == 'tastiera' ? { name: "ASPxMenu1" } : s, e, s == 'tastiera' ? true : doPostback);
            if (e.item.name === "Modifiko" && cbDergoMeEmail.GetChecked()) {
                OnGridSelectionCompleteDergoMeEmail(Utils.getUrlVar('id'));
            }
            return;
        }
        else {
            e.processOnServer = false;
            return;
        }
    }
    else if (e.item.name === 'Klono') {
        $("input[id$='hfAutorizimi']").val("True");
        KlonoClick(e);
    }
    else if (e.item.name === "Komento") {
        if (Utils.getUrlVar('vjenNga') === 'aprovim' || Utils.getUrlVar('vjenNga') === 'kerkese')
            myButtonClickLupa.LupaUniversal_Click('Komentet', 'LupaKomente.aspx?idetapa=' + Utils.getUrlVar('idetapa') + '&nrprocesi=' + Utils.getUrlVar('nrprocesi') + '&veprimi=Gjitha&idkategoria=3', 600, 500);
        else {
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "gjejEtapeDokumenti"),
                data: JSON.stringify({ idperdoruesi: hfState.Get("idPerdoruesi"), idDok: Utils.getUrlVar('id'), lloji: "arka" })
            }).done(SucededCallbackLupaKomente);

        }
        e.processOnServer = false;
        click = false;
        return;
    }
    else if (e.item.name === 'QendraKosto') {
        e.processOnServer = false;
        click = false;
        return;
    }
    else if (e.item.name === 'Kerko') {
        myFaqeCelje.kontrolloTeDrejta('VeprimeBanka.aspx?lloji=' + pageState.llojDokumenti, null, true);
        e.processOnServer = false;
        return;
    }
    else if (e.item.name === 'Shto') {
        myFaqeCelje.kontrolloTeDrejta('ShtoVeprimBanka.aspx?lloji=' + pageState.llojDokumenti + '&shtim_modifikim=shtim', true);
        e.processOnServer = false;
        return;
    }
    else if (e.item.name === 'Fshi') {
        if ($("input[id$='hfAutorizimi']").val() === 'False') {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukKeniAutorizimPerTeFshireDok"));
            Utils.hiqLoadingGif();
            click = false;
            e.processOnServer = false;
            return;
        }
        popFshi.Show();
        e.processOnServer = false;
        return;
    }
    else if (e.item.name === 'Anullo') {
        if (Utils.getUrlVar('vjenNga') === 'aprovim')
            myFaqeCelje.kontrolloTeDrejta('ListeAprovimi.aspx?status=aprovim');
        else if (Utils.getUrlVar('vjenNga') === 'kerkese')
            myFaqeCelje.kontrolloTeDrejta('ListeAprovimi.aspx?status=kerkese');
        else if (pageState.llojDokumenti === 'arketimLlogariKlienti' || pageState.llojDokumenti === 'arketimAbonent')
            myFaqeCelje.kontrolloTeDrejta('VeprimeBanka.aspx?lloji=arketim');
        else
            myFaqeCelje.kontrolloTeDrejta('VeprimeBanka.aspx?lloji=' + pageState.llojDokumenti);

        e.processOnServer = false;
        click = false;
        return;
    }
    else if (e.item.name === 'Arkiva') {
        ButtonClickArkiva();
        e.processOnServer = false;
        click = false;
        return;
    }
    else if (e.item.name === "Validim") {
        ButtonClickValidim();
        e.processOnServer = false;
        return;
    }
    else if (e.item.name === "PrintoBalancen") {
        window.open("RaportiShpejte.aspx?Sesioni=false&emriReal=Fature_detyrimi&printo=0&printoDetyrim=po&scopeID=" + Utils.getUrlVar("scopeID"));
        e.processOnServer = false;
        return;
    }
    else if (e.item.name === "DergoEmail") {
        e.processOnServer = false;
        OnGridSelectionCompleteDergoMeEmail(Utils.getUrlVar('id'));
        return;
    }
    Utils.doPostback(s, e, doPostback);
}

function OnGridSelectionCompleteDergoMeEmail(values) {
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "VeprimeArkaBanka_DergoFatureMeEmail"),
        data: JSON.stringify({ idsKokaDok: JSON.parse("[" + values + "]") })
    }).done(SuccededCallbacDergimiMeEMail);
}
function SuccededCallbacDergimiMeEMail(result) {
    if (result.informimi.PershkrimMesazhi != "")
        myMesazh.ShtoMesazhGabimi(result.informimi.PershkrimMesazhi);
    if (result.mesazhi.PershkrimMesazhi != "") {
        if (result.mesazhi.Status == false)
            myMesazh.ShtoMesazhGabimi(result.mesazhi.PershkrimMesazhi);
        else
            myMesazh.ShtoMesazhSuksesi(result.mesazhi.PershkrimMesazhi);
    }
}

function kontrolloRreshtatPerKursFature() {    
    var grida = jQuery("#rowed5");
    var indekset = [];
    var index = grida.getDataIDs();
    for (var i = 1; i <= index.length; i++) {
        if (arrId[i] != 0) {
            var dokument = JSON.parse($('#HfColFatShitje').val()).filter(function (x) { return x.IdShitjeKoka == arrId[i]; })[0];
            if (dokument && monedhabanka != dokument.IdMonedha && pageState.kushte["LLMKF"].toString() !== grida.getTekstQelize('MeKursFature', i)) {
                indekset.push(i);
            }
        }        
    }
    pageState.rreshtatPerNdryshimKursFature = indekset;
}

function JoClick_KursFature() {
//vendoset -> kursi i fundit i monedhes
    pageState.kontrolloPerRreshtaLLMKF = false;
    if (!($("#hfShtimModifikim").val() == 'modifikim' && pageState.kushte["LLMKF"] == false))
        return;
    var grida = $('#rowed5');
    var kursi = 1;
    var monkursNderm = hfState.Get('monedhatKurse');
    for (var i = 0; i < pageState.rreshtatPerNdryshimKursFature.length; i++) {
        var indeksi = pageState.rreshtatPerNdryshimKursFature[i];
        var HfColFatShitje = JSON.parse($('#HfColFatShitje').val());
        var fatura = HfColFatShitje.filter(function (x) { return x.IdShitjeKoka == arrId[indeksi]; })[0];
        if (!fatura)
            continue;
        for (var nr = 0; nr < monkursNderm.length; nr++)
            if (parseInt(monkursNderm[nr].split(';')[0]) == fatura.IdMonedha) {
                kursi = monkursNderm[nr].split(';')[2];
                break;
            }
        grida.setTekstQelize('txtKursi', indeksi, kursi);
        grida.setTekstQelize('MeKursFature', indeksi, false);
    }
}
function PoClick_KursFature() {
//vendoset -> kursi i fatures ose i azhornimit nese fatura eshte e azhornuar
    pageState.kontrolloPerRreshtaLLMKF = false;
    var grida = $('#rowed5');
    var idfaturat = [];
    for (var i = 0; i < pageState.rreshtatPerNdryshimKursFature.length; i++) {
        if (grida.getTeDhenaRreshti(indeksi) == undefined)
            continue;
        var indeksi = pageState.rreshtatPerNdryshimKursFature[i];
        var idFatura = grida.merrTeDhenaPerQelizen("txtFatura", indeksi, "IdFatura");
        idfaturat.push(Utils.HiqParashtesenNgaIdFatura(idFatura));
        
    }
    MerrVendosKursFatureOseAzhornimiPerFaturat(idfaturat);
}

function MerrVendosKursFatureOseAzhornimiPerFaturat(idfaturaPerNdryshimKursi) {
    if (!($("#hfShtimModifikim").val() == 'modifikim' && pageState.kushte["LLMKF"] == true))
        return;
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "merrKursFatureOseAzhornimiPerIdDok"),
        data: JSON.stringify({ ids: idfaturaPerNdryshimKursi.join(',') })
    }).done(SuccededMerrKursFatureOseAzhornimiPerIdDok);
}

function SuccededMerrKursFatureOseAzhornimiPerIdDok(result) {
    var grida = $('#rowed5');
    for (var i = 0; i < pageState.rreshtatPerNdryshimKursFature.length; i++) {
        if (grida.getTeDhenaRreshti(indeksi) == undefined)
            continue;
        var indeksi = pageState.rreshtatPerNdryshimKursFature[i];
        var idFatura = grida.merrTeDhenaPerQelizen("txtFatura", indeksi, "IdFatura");
        idFatura = Utils.HiqParashtesenNgaIdFatura(idFatura);
        var gridaRr = grida.getTeDhenaRreshti(indeksi)[0];
        var dokument = result.dt.filter(function (x) { return x.IdFatura == idFatura; })[0];
        var kursi = dokument != undefined ? dokument.Kursi : gridaRr.txtKursi;
        grida.setTekstQelize('txtKursi', indeksi, kursi);
        grida.setTekstQelize('MeKursFature', indeksi, true);
    }
}

function KlonoClick(e) {
    var hf1 = $("#hfShtimModifikim");
    $("input[id$='hfLidhur']").val(false);
    pageState.lloji = "klonim";
    hf1.val(pageState.lloji);


    try {
        ASPxMenu1.GetItemByName('FletaKontabel').SetVisible(false);
        ASPxMenu1.GetItemByName('QendraKosto').SetVisible(false);
        ASPxMenu1.GetItemByName('Konverto').SetVisible(false);
        ASPxMenu1.GetItemByName('Klono').SetVisible(false);

    }
    catch (ex) {
    }
    ASPxMenu1.GetItemByName('Fshi').SetVisible(false);
    ASPxMenu1.GetItemByName('PrintPreview').SetVisible(false);
    ASPxMenu1.GetItemByName('DergoEmail').SetVisible(false);

    myMenu.menuSipasTeDrejtaRegjistrim(hf1, hfTeDrejta);

    $("#ASPxSplitter1_hl").empty();
    $('#ASPxSplitter1_tblKonfigurimi>tbody>tr:eq(0)>td:eq(0)').empty();

    e.processOnServer = false;
    ndryshoKonfigurimin(true);
    click = false;
}
var lupaValidimHapur = false;
function ButtonClickValidim() {
    if (lupaValidimHapur)
        return;

    lupaValidimHapur = true;
    popupUniversal.SetHeaderText('Lupa e validimit');
    popupUniversal.SetSize(400, 400);
    popupUniversal.SetContentUrl('LupaValidimKlientiArketime.aspx?lloji=' + pageState.llojDokumenti + "&MerrKlient=" + pageState.kushte["MKBRM"]);
    popupUniversal.Show();
}

function SucededCallbackLupaKomente(result) {
    myButtonClickLupa.LupaUniversal_Click('Komentet', 'LupaKomente.aspx?idetapa=' + result.IdEtapa + '&nrprocesi=' + result.NrProcesi + '&veprimi=Gjitha&idkategoria=3', 600, 500);
}

function Kthehu(mesazh) {
    myFaqeCelje.kontrolloTeDrejta('VeprimeBanka.aspx?lloji=' + pageState.llojDokumenti + "&ruaj=po");
}
var click = false;
/*
Function: RuajClick

Therret funksionin <merrTeDhena> per te marre te dhenat e grides kur klikohet butoni Ruaj.
*/
function RuajClick(s, e) {
    var grida = $('#rowed5');
    var idRresht = grida.getLastSel2();
    if (click) {
        e.processOnServer = false;
        Utils.hiqLoadingGif();
    }
    else {
        click = true;
        if (isValidKoka()) {
            grida.jqGrid('saveRow', idRresht, false, 'clientArray', {}, afterSaveFunc);
            merrTeDhena();
            grida.setLastSel2(-1);
            if (vlerazerogrid == true) {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgRreshtaTePavlefshemNeGride"));
                e.processOnServer = false;
                click = false;
            }
            if (cbKasa.GetChecked() && mesazhriprintimi && pageState.shtimModifikim == 'modifikim' && hfState.Get('printuarKase')) {
                popKupon.Show();
                e.processOnServer = false;
                click = false;
                Utils.hiqLoadingGif();
            }
        }
        else {
            e.processOnServer = false;
            click = false;
            Utils.hiqLoadingGif();
        }
    }
}

function afterSaveFunc(rowid, serverResp) {
    updateTotalet(rowid);
}

/*
Function: PastroClick

Pastron fushat e kokes se dokumentit dhe inicializon serish griden.
*/
function PastroClick() {
    var hf = $("#hfShtimModifikim");
    if (pageState.ruajVleratFundit) {
        var idDegAdmArkaBanka = cmbDegeAdministrative.GetValue();
        var idArkaBanka = banka_ComboBox.GetValue();
        //shtojme dhe idkonfigurimin qe ne rast se jemi duke ndryshuar konfigurimin e dokumentave te kemi local storage ne nivel konfigurimi
        pageState.mySessionStorage.setObject("cmbDegeAdministrative" + konfigurimi_ComboBox.GetValue(), idDegAdmArkaBanka ? { text: cmbDegeAdministrative.GetText(), value: cmbDegeAdministrative.GetValue() } : null);
        pageState.mySessionStorage.setObject("banka_ComboBox" + konfigurimi_ComboBox.GetValue(), idArkaBanka ? { text: banka_ComboBox.GetText(), value: banka_ComboBox.GetValue() } : null);
        pageState.mySessionStorage.setObject("data_DateEdit" + konfigurimi_ComboBox.GetValue(), data_DateEdit.GetDate());
    }
    pageState.mySessionStorage.setObject("banka_ComboBox_fillestare", null);

    hf.val("shtim");
    pageState.shtimModifikim = "shtim";
    $("input[id$='hfAutorizimi']").val(true);
    pastro();
    pastroFushatKokes(false);
    ASPxMenu1.GetItemByName('Shto').SetVisible(true);
    ASPxMenu1.GetItemByName('FletaKontabel').SetVisible(false);
    ASPxMenu1.GetItemByName('Fshi').SetVisible(false);
    ASPxMenu1.GetItemByName('QendraKosto').SetVisible(false);
    ASPxMenu1.GetItemByName('Draft').SetVisible(true);
    ASPxMenu1.GetItemByName('Klono').SetVisible(false);
    myMenu.menuSipasTeDrejtaRegjistrim(hf, hfTeDrejta);
    ndryshoKonfigurimin(false);
    llojiKF = '';
    $('#ASPxSplitter1_hl').empty();
    click = false;
}

/*
Function: llogaritVlereNeMonedheBaze

Llogarit vleren ne monedhe baze te dokumentit.
*/
function llogaritVlereNeMonedheBaze() {
    if (parseFloat(vlera_TextBox.GetText()) === 0.0 || parseFloat(kursi_TextBox.GetText()) === 0.0)
        vleraMonedhaBaze_TextBox.SetText('0');
    else {
        var vleraMonBaze = Utils.HiqPresjet(vlera_TextBox.GetText()) * parseFloat(kursi_TextBox.GetText());

        if (isNaN(vleraMonBaze))
            vleraMonedhaBaze_TextBox.SetText('0');
        else
            vleraMonedhaBaze_TextBox.SetText(vleraMonBaze);
    }
}



/*
Function: plotesoMeFjale

Shenon vleren e dokumentit ne fjale.
*/
function plotesoMeFjale() {
    var num = vlera_TextBox.GetValue();
    var words;
    if (num < 0) {
        num = num.slice(1);
        words = 'minus ' + toWords(num);
    }
    else
        words = toWords(num);

    shuma_TextBox.SetText(words);
}

/*
Function: lostFocusVleraTotale

Kur heqim fokusin nga textbox-i i vleres. Therret funksionet <plotesoMeFjale> dhe <llogaritVlereNeMonedheBaze>
*/
function lostFocusVleraTotale() {
    if (!isNaN(Utils.HiqPresjet(vlera_TextBox.GetText()))) {
        Totalet();

    }
}
function PlotesoFushatEKokesNgaTrupi(vlera) {
    vlera_TextBox.SetText(vlera);
    llogaritVlereNeMonedheBaze();
    plotesoMeFjale();
}
/*
Function: lostFocusVleraTotale

Kur heqim fokusin nga textbox-i i kursit. Therret funksionin <llogaritVlereNeMonedheBaze>
*/
var kursss = 1;

function keyUpKursi() {
    kursiParafunidt = kursss;
    kursss = parseFloat(kursi_TextBox.GetText());
    if (kursss === '' || isNaN(kursss))
        kursss = 1;

    ndryshimKursi();
    if (!isNaN(vlera_TextBox.GetText()) && !isNaN(kursi_TextBox.GetText()))
        PlotesoFushatEKokesNgaTrupi(vlera_TextBox.GetText());
}

function lostFocusKursi() {
    if (kursi_TextBox.GetText() === '' || isNaN(kursi_TextBox.GetText()))
        kursi_TextBox.SetText(1);
    if (parseFloat(kursi_TextBox.GetText()) === 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKursiNukMundTeJeteZero"));
        kursi_TextBox.SetText(1);
    }
    var diferenca = Math.abs(kursifundit - parseFloat(kursi_TextBox.GetText()));
    if (diferenca / kursifundit > 0.2)
        myMesazh.ShtoMesazhInformues(hfState.Get("msgKursiRiNdryshonShumeMeKursinMePare"));
}


var rreshtiBosh;
var emriKlientit;
var kushtetPageses = new Array();

var rreshtiKushtPagese;
var diferencaDatave;
var faturanivel = new Array();
/*
Function: OnGridFaturatSelectionComplete
Pasi ndryshon selection-i i grides se faturave ne griden e trupit shtohen faturat qe jane zgjedhur.
*/
function OnGridFaturatSelectionComplete(values, indeksFature, numerFaturash, idRreshti) {
    if (values[0] === null) {
        Utils.hiqLoadingGif();
        return;
    }
    var grida = $('#rowed5');
    var idRow = grida.getLastSel2();
    var idFature = values[0];
    var subjekti;
    var selectedItem = furnitori_ComboBox.GetSelectedItem();

    if (values[1] !== undefined && values[1] !== '' && values[1] != 0) {
        emriKlientit = values[15];
        subjekti = values[14];
    }
    else if (pageState.llojSubjektiDefault !== "Llogari" && pageState.llojSubjektiDefault !== "Punonjes" && pageState.klientiDefault && pageState.klientiDefault.IdKlientFurnitor > 0) {
        subjekti = pageState.klientiDefault.KodKlientFurnitor;
        emriKlientit = pageState.klientiDefault.EmertimiKF;
    }
    else if (selectedItem != undefined && selectedItem.GetColumnText("KodKlientFurnitor") != "") {
        subjekti = selectedItem.GetColumnText('KodKlientFurnitor');
        emriKlientit = selectedItem.GetColumnText('EmertimiKF');
    }

    var monkurs = hfState.Get('monedhatKurse');
    var fatura = (values[3] + "," + formatDate(values[4], 'dd/MM/yyyy'));
    var vlera = values[2];
    var kursi = values[6];
    var idKf = values[1];
    var idmonedha = values[8];
    var dtdok = values[4];
    var dtazhornimi = values[9];
    var kursAzhornim = (dtdok < dtazhornimi) ? values[10] : -1;
    var vlerafillestare = values[11];
    var muajFature = values[12];
    var kodFature = values[3];
    var statusFature = values[13];
    var meKursFature = (pageState.kushte["LLMKF"] && monedhabanka == parseInt($("#hfIdMonedhaNder").val()) && monedhabanka != idmonedha)? true: false;
    var emertimi = emriKlientit;

    if (idRreshti)
        UpdateVleratOnGrideSelectionComplete(idRreshti, grida, monkurs, monedhabanka, vlera, vlerafillestare, kursi, idmonedha, kursAzhornim);
    else {
        var rreshtiBosh = -1;
        var rreshtifundit = false;
        var idTe = grida.jqGrid('getDataIDs');
        for (var i = 0; i < idTe.length; i++) {
            if (((grida.getTekstQelize('txtVlera', idTe[i]) == 0 || isNaN(grida.getTekstQelize('txtVlera', idTe[i]))) || grida.getTekstQelize('txtSubjekti', idTe[i]) == '')
                && grida.getTekstQelize('txtLloji', idTe[i]) != 'Llogari' && grida.getTekstQelize('txtLloji', idTe[i]) != 'Punonjes') {
                rreshtiBosh = idTe[i];
                if (i === idTe.length - 1)
                    rreshtifundit = true;
                break;
            }
        }
        if (rreshtiBosh == -1) {
            var max = 0; //ishte redeklaruar
            for (i = 0; i < idTe.length; i++) {
                max = Math.max(max, idTe[i]);
            }
            rreshtifundit = true;
            var id = parseInt(max) + 1;
            max = id;
            if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1] == 'True')
                be = "<input id='butonFshi" + id + "' type='image' disabled='disabled' value='Fshi' onBlur = 'lostFocusKoloneFundit()' onmouseover='ndryshoImazhin(1," + id + ")' onmouseout='ndryshoImazhin(0," + id + ")'  src='images/square-icon.png' onclick='fshiClicked(" + id + ")'/>";
            else
                be = "<input id='butonFshi" + id + "' type='image' value='Fshi' onBlur = 'lostFocusKoloneFundit()'  onmouseover='ndryshoImazhin(1," + id + ")' onmouseout='ndryshoImazhin(0," + id + ")'  src='images/square-icon.png' onclick='fshiClicked(" + id + ")'/>";
            var datarow = { txtFshi: be };
            jQuery("#rowed5").addRowData(id, datarow);
            rreshtiBosh = id;
        }
        if (pageState.llojDokumenti == "terheqje" || pageState.llojDokumenti == "pagese") {
            if (rreshtiBosh == idRow && $('#txtLloji' + idRow).val() != undefined) {
                $('#txtLloji' + idRow).val(8);
                $('#txtDebiKredi' + idRow).val(1);
                $('#txtDebiKredi' + idRow).attr('disabled', 'disabled');
            }
            else {
                grida.setTekstQelize('txtLloji', rreshtiBosh, "Furnitor");
                grida.setTekstQelize('txtDebiKredi', rreshtiBosh, "Debi");
            }
        }
        else {
            if (rreshtiBosh == idRow && $('#txtLloji' + idRow).val() != undefined) {
                $('#txtLloji' + idRow).val(7);
                $('#txtDebiKredi' + idRow).val(2); $('#txtDebiKredi' + idRow).attr('disabled', 'disabled');
            }
            else {
                grida.setTekstQelize('txtLloji', rreshtiBosh, "Klient");
                grida.setTekstQelize('txtDebiKredi', rreshtiBosh, "Kredi");
            }
        }
        grida.setTekstQelize('txtNrRendor', rreshtiBosh, grida.getInd(rreshtiBosh, false));
        grida.setTekstQelize('txtSubjekti', rreshtiBosh, subjekti);
        grida.setTekstQelize('txtIdKodi', rreshtiBosh, idKf);
        grida.setTekstQelize('txtEmertimi', rreshtiBosh, emertimi);
        grida.setTekstQelize('txtFatura', rreshtiBosh, fatura);
        grida.vendosTeDhenaPerQelizen("txtFatura", rreshtiBosh, "IdFatura", idFature);
        grida.setTekstQelize('txtKodi', rreshtiBosh, kodFature);
        grida.setTekstQelize('txtMuaji', rreshtiBosh, muajFature);
        grida.setTekstQelize('StatusFature', rreshtiBosh, statusFature);
        grida.setTekstQelize('MeKursFature', rreshtiBosh, meKursFature);

        if (rreshtiBosh == idRow && $('#txtLloji' + idRow).val() != undefined)
            $('#txtOpsione' + idRow).val(1);
        else
            grida.setTekstQelize('txtOpsione', rreshtiBosh, 'Pagese fature');

        UpdateVleratOnGrideSelectionComplete(rreshtiBosh, grida, monkurs, monedhabanka, vlera, vlerafillestare, kursi, idmonedha, kursAzhornim);

        arrNiv[rreshtiBosh] = values[7];
        arrId[rreshtiBosh] = values[0];
        arrMon[rreshtiBosh] = idmonedha;
        arrVlera[rreshtiBosh] = values[2];

        kushtetPageses[rreshtiBosh] = rreshtiBosh + ";" + values[5] + ";" + data_DateEdit.GetText() + ";" + formatDate(values[4], 'dd/MM/yyyy') + ";" + values[2];

        if (rreshtifundit) {
            var id = parseInt(rreshtiBosh) + 1;
            if (arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides.length - 1] === 'True')
                be = "<input id='butonFshi" + id + "' type='image' disabled='disabled' value='Fshi' onBlur = 'lostFocusKoloneFundit()' onmouseover='ndryshoImazhin(1," + id + ")' onmouseout='ndryshoImazhin(0," + id + ")'  src='images/square-icon.png' onclick='fshiClicked(" + id + ")'/>";
            else
                be = "<input id='butonFshi" + id + "' type='image' value='Fshi' onBlur = 'lostFocusKoloneFundit()'  onmouseover='ndryshoImazhin(1," + id + ")' onmouseout='ndryshoImazhin(0," + id + ")'  src='images/square-icon.png' onclick='fshiClicked(" + id + ")'/>";

            var datarow = { txtLloji: "", txtSubjekti: "", txtEmertimi: "", txtFatura: "", txtDebiKredi: "", txtVlera: "", txtZbritja: "", txtPershkrimi: "", StatusFature: '', txtKreditet: "", txtVleraArketuar: "", txtVleraMonBaze: "", txtMuaji: "", txtKodi: "", txtVleraFill: "", txtVleraMbetur: "", MeKursFature: false, txtFshi: be };
            grida.jqGrid('addRowData', id, datarow);
            grida.setTekstQelize('txtVlera', id, 0);
            grida.setTekstQelize('txtZbritja', id, 0);
            grida.setTekstQelize('txtKreditet', id, 0);
            grida.setTekstQelize('txtVleraArketuar', id, 0);
            grida.setTekstQelize('txtVleraMonBaze', id, 0);
            grida.selektoRreshtin(id);
        }
    }
    Totalet();
    if ((idRreshti && indeksFature == numerFaturash - 1) || (idRreshti == undefined && indeksFature == numerFaturash)) {
        Utils.hiqLoadingGif();
        grida.rregulloNrRendor();
    }
}

function UpdateVleratOnGrideSelectionComplete(idRreshti, grida, monkursNderm, monedhabanka, vlera, vlerafillestare, kursTrup, idMonTrup, kursAzhornimi) {

    var meKursFature = false;
    var kursi = 1;
    if (pageState.kushte['LLMKF'] && parseInt($("#hfIdMonedhaNder").val()) == monedhabanka && monedhabanka != idMonTrup) { 
        meKursFature = true;
        kursi = (kursAzhornimi == -1) ? kursTrup : kursAzhornimi;    
    }
    else {
        if (idMonTrup == monedhabanka)
            kursi = kursi_TextBox.GetText();
        else {
            for (var nr = 0; nr < monkursNderm.length; nr++)
                if (parseInt(monkursNderm[nr].split(';')[0]) == idMonTrup) {
                    kursi = monkursNderm[nr].split(';')[2];
                    break;
                }
        }
    }
    grida.setTekstQelize('txtKursi', idRreshti, kursi);
    grida.setTekstQelize('MeKursFature', idRreshti, meKursFature);

    if (idMonTrup == monedhabanka)
        vlera = parseFloat(vlera);
    else
        for (var m = 0; m < monkursNderm.length; m++) {
            if (monkursNderm[m].split(';')[0] == idMonTrup + "") {
                vlera = parseFloat(vlera * monkursNderm[m].split(';')[2] / parseFloat(kursi_TextBox.GetText()));
                break;
            }
            else  {
                vlera = parseFloat(vlera * monkursNderm[m].split(';')[2] * parseFloat(kursi));
                break;
            }
        }

    grida.setTekstQelize('txtVleraMonBaze', idRreshti, (vlera * parseFloat(kursi_TextBox.GetText())).toFixed(pageState.formatVleftaDB));
    grida.setTekstQelize('txtZbritja', idRreshti, 0);
    grida.setTekstQelize('txtKreditet', idRreshti, 0);

    if (kursTrup != 0 && parseFloat(kursi_TextBox.GetText()) != 0.0) {
        var formatnrvlefta = grida.getShifraPasPresjes('txtVlera', idRreshti);
        grida.setTekstQelize('txtVlera', idRreshti, vlera.toFixed(pageState.formatVleftaDB));
        grida.setTekstQelize('txtVleraArketuar', idRreshti, vlera.toFixed(pageState.formatVleftaDB));
        grida.setTekstQelize('txtVleraFill', idRreshti, (vlerafillestare / parseFloat(kursi_TextBox.GetText())).toFixed(formatnrvlefta) * kursi_TextBox.GetText());
        grida.setTekstQelize('txtVleraMbetur', idRreshti, vlera.toFixed(formatnrvlefta));
    }
}

/*
Function: eshteShtuarFatura

Kontrollon nese fatura (qe kalohet si parameter) eshte shtuar me pare te grida e trupit apo jo.
*/
function eshteShtuarFatura(id, niv) {
    id = Utils.HiqParashtesenNgaIdFatura(id);
    var grida = jQuery("#rowed5");
    var eshteShtuar = false;
    var index = grida.getDataIDs();
    for (i = 0; i < index.length; i++) {
        if (arrId[index[i]] !== undefined) {
            arrId[index[i]] = Utils.HiqParashtesenNgaIdFatura(arrId[index[i]]);
            if (parseInt(arrId[[index[i]]]) === parseInt(id) && arrNiv[index[i]] === niv) {
                eshteShtuar = true;
                break;
            }
        }
    }
    return eshteShtuar;
}

/*
Function: ValidateDateDokumenti

Therret funksionin <callWebserviceValidateDateDokumenti>.
*/
function ValidateDateDokumenti() {
    callWebserviceValidateDateDokumenti(data_DateEdit.GetText());
}
/*
Function: callWebserviceKushtPagese

Therret funksionin <ktheKushtePagese>.
Shiko funksionin <SucceededCallbackKushtPagese>.
*/
function callWebserviceKushtPagese(name) {

    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheKushtePagese"),
        data: JSON.stringify({ prefixText: name })
    }).done(SucceededCallbackKushtPagese);
}

/*
Function: SucceededCallbackKushtPagese

Vendos per cdo rresht te grides pershkrimin e kushtit te pageses dhe llogarit zbritjen sipas ketij kushti pagese.
*/
function SucceededCallbackKushtPagese(result) {
    var vlerat = result.split(';');            //nestila meqe per momentin nuk perdoret dhe pengon ne llogaritjet e tjera
}

/*
Function: ShfaqPeriudhen

Hap lupen e periudhave.
*/
function ShfaqPeriudhen() {
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgZgjidhPeriudheKontabel"), 'LupaPeriudhaKontabel.aspx', 600, 600);
}

function lostFocusPeriudha(vlera) {

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
        data_DateEdit.SetText(periudha[0]);
    else {
        if ((dtDokumentit[1] < periudha1[1] || dtDokumentit[1] > periudha2[1]))
            data_DateEdit.SetText(periudha[0]);
        else
            data_DateEdit.SetText(dataDok);
    }
}

function ndryshoImazhin(nr, index) {
    var id = "butonFshi" + index;
    if (document.getElementById(id) != null) {
        if (nr == 1)
            document.getElementById(id).src = "images/blue-square-icon.png";
        else
            if (nr == 0)
                document.getElementById(id).src = "images/square-icon.png";
    }
}

function hapPopUpRi(kf) {
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgShtoKF"), 'LupaKlientShpejte.aspx?vjenNga=ShtoVeprimBanka&kf=' + kf, 850, 600);
}

function nrLlogariChange(s, e) {
    var v = s.GetText().split(';');
    s.SetText(v[0]);
}

function ButtonClickKursi(s, e) {
    if (monedhabanka != 0 && monedhabanka != null && monedhabanka != -1) {
        popupUniversal.SetHeaderText('Zgjidh Kursin');
        popupUniversal.SetContentUrl('LupaKursiShpejte.aspx?idMonedha=' + monedhabanka + '&llojKursi=' + llojkursi);
        popupUniversal.SetSize(widthLupaKursi, heightLupaKursi);
        popupUniversal.Show();
    }
}

/*
Function: Auto_Click

Hap lupen e automjeteve.
*/
function Auto_Click() {//po
    var hfAuto = $("#hfLupaAutomjet");
    var queryStr = hfAuto.val();
    popupUniversal.SetContentUrl('LupaAutomjeti.aspx?idKonfigAmbjente=' + queryStr);
    popupUniversal.SetHeaderText('Zgjidh Automjetin');
    popupUniversal.SetSize(widthLupaAutomjet, heightLupaAutomjet);
    popupUniversal.Show();
}

function textChangedAuto(s, e) {
    var idAuto = btneAutomjet.GetValue();
    if (idAuto == null || idAuto == 0 || idAuto == -1)
        return;
    try {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ktheTargeAutomjeti"), data: JSON.stringify({ idAutomjeti: idAuto })
        }).done(SucceededCallbackTargeAutomjeti);
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTargesSeAutomjetit"));
    }
}

//kur ndryshon automjeti ndryshohet dhe targa e tij
function SucceededCallbackTargeAutomjeti(result) {
    if (result != null)
        txtTarga.SetText(result);
    else {
        btneAutomjet.SetSelectedIndex(-1);
        btneAutomjet.SetText('');
        txtTarga.SetText('');
    }
}

function lostFocusData(s, e) { //komentuar sepse pengonte levizjen e tabit neper fusha sipas radhes

}

function gotFocusVlera(s, e) {
    Utils.gotFocusTxtNumer(s, e);
    s.SelectAll();
}

function lostFocusVlera(s, e) {
    Utils.lostFocusTxtNumer(s, e);
    lostFocusVleraTotale();
    llogaritVlereNeMonedheBaze();
    plotesoMeFjale();
    vlera_TextBox.SetValue(Utils.FormatNumberBy3(Utils.HiqPresjet(vlera_TextBox.GetText())));
}

function gotFocusVleraMonBaze(s, e) {
    Utils.gotFocusTxtNumer(s, e);
}

function lostFocusVleraMonBaze(s, e) {
    Utils.lostFocusTxtNumer(s, e);
}
function numberWithCommas(x) {
    var parts = x.toString().split(".");
    parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    return parts.join(".");
}
function textChangedTotaliPaguar(s, e) {
    vlera_TextBox.SetText(txtTotaliPaguar.GetText());
    if (pageState.kushte['PVKST'] == 'PO') shtoRreshtPaFature();
    llogaritVlereNeMonedheBaze();
    plotesoMeFjale();
}

function kontrollofaturaTePaprintuara(idPerdoruesi, idNdermarrje) {//po
    var idPerdoruesi = hfState.Get('idPerdoruesi');
    var idNdermarrje = hfState.Get('idNdermarrje');
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "KontrolloFaturaBankaPaprintuara"),
        data: JSON.stringify({ idndermarje: idNdermarrje, idperdorues: idPerdoruesi, iddege: cmbDegeAdministrative.GetValue() })
    }).done(SuccededCallbackPaPrintuar);
}
function SuccededCallbackPaPrintuar(result) {

    if (result)
        ButtonClickPaPrintuar();
    else {
        var lloji = Utils.getUrlVar("lloji");
        if (lloji == "arketimLlogariKlienti" || lloji == "arketimAbonent")
            ButtonClickValidim();
    }
}
var hapLupeValidimi = false;
function ButtonClickPaPrintuar() {//po
    popupUniversal.SetHeaderText('Arketimet e meposhtme nuk jane printuar ne kase. Per ti printuar zgjidhni nje nga nje arketimet dhe riprintoji!');
    identifikuesPerPopupDokumentat = "VeprimeBankaPaprintuar";
    popupUniversal.SetContentUrl('LupaDokumenta.aspx?veprimi=VeprimeBankaPaprintuar&niveli=' + veprimi_ComboBox.GetValue() + '&lloji=' + pageState.llojDokumenti + '&idshop=' + cmbDegeAdministrative.GetValue());
    popupUniversal.SetSize(600, 600);
    //  popupUniversal.AdjustSize();
    popupUniversal.Show();
    var lloji = Utils.getUrlVar("lloji");
    if (lloji == "arketimLlogariKlienti" || lloji == "arketimAbonent") {
        hapLupeValidimi = true;
    }
}
function MbushGridenEfaturave() {
    grid_faturat.PerformCallback('mbush');
}
function EndCallbackGrid_Faturat(s, e) {
    var detyrimiActive = s["cpDetyrimiMbeturActive"];
    var detyrimiTerminated = s["cpDetyrimiMbeturTerminated"];
    if (detyrimiActive != undefined && detyrimiActive != '')
        txtDetyrimi.SetText(detyrimiActive);
    if (detyrimiTerminated != undefined && detyrimiTerminated != '')
        txtDetyrimiTerminated.SetText(detyrimiTerminated);
    var mesazh = s["cpMesazhKlientTerminated"];
    var terminated = s["cpKlientTerminated"];
    if (!Utils.IsNullOrEmpty(mesazh)) {
        myMesazh.ShtoMesazhInformues(mesazh);
        s["cpMesazhKlientTerminated"] = "";
    }
    if (!Utils.IsNullOrEmpty(terminated)) {
        setMenuItemEnabled("RuajPrint", false);
        setMenuItemEnabled("Ruaj", false);
        setMenuItemEnabled("Draft", false);
    } else {
        setMenuItemEnabled("RuajPrint", getMenuItemEnabled("RuajPrint"));
        setMenuItemEnabled("Ruaj", getMenuItemEnabled("Ruaj"));
        setMenuItemEnabled("Draft", getMenuItemEnabled("Draft"));
    }
}
function getMenuItemEnabled(name) {
    var item = ASPxMenu1.GetItemByName(name);
    if (item)
        return item.GetEnabled();

    return false;
}
function setMenuItemEnabled(name, enabled) {
    var item = ASPxMenu1.GetItemByName(name);
    if (item)
        item.SetEnabled(enabled);
}

function SelectionChanged(s, e) {
    grid_faturat.GetSelectedFieldValues('IdDokumenti;IdKlientFurnitori;VleftaPaLikujduar;NrDokumenti;DtDokumenti;IdKushtPagese;Kursi;IdNiveli;IdMonedha;DtAzhornimi;KursAzhornimi;Vlefta;Pershkrimi;StatusFature;KodiKlientit;EmertimiKf', OnGetSelectedFieldValues);
}

function OnGetSelectedFieldValues(selectedValues) {
    var len = selectedValues.length;
    if (len === 0)
        return;
    Utils.shfaqLoadingGif();
    var shtoFaturat = [];

    for (var i = 0; i < len; i++) {
        if (!eshteShtuarFatura(selectedValues[i][0], selectedValues[i][7])) {
            shtoFaturat.push({ id: i, element: selectedValues[i] });
        }
    }
    for (var i = 0; i < shtoFaturat.length; i++) {
        OnGridFaturatSelectionComplete(shtoFaturat[i].element, shtoFaturat[i].id, (i == shtoFaturat.length - 1 ? shtoFaturat[i].id : len));
    }
    if (shtoFaturat.length === 0)
        Utils.hiqLoadingGif();
}

function NgjyrosFaqetERegjistrimeve(lloji) {
    if (hfState.Get("vodafoneShops")) {
        document.getElementsByTagName("body")[0].setAttribute("id", "faqetengjyrosura");
        var faqetengjyrosura = document.querySelector("#faqetengjyrosura");
        switch (lloji) {
            case "pagese":
                faqetengjyrosura.classList.add("KlasePerKthimetEGarancive");
                break;
            case "arketimLlogariKlienti":
                faqetengjyrosura.classList.add("KlasePerLlogariKlienti");
                break;
            case "arketimAbonent":
                faqetengjyrosura.classList.add("KlasePerArketimAbonenti");
                break;
            case "arketim":
                if (Utils.getUrlVar('shtim_modifikim') === "shtim")
                    faqetengjyrosura.classList.add("KlasePerArketim");
                break;
            default:
                break;
        }
    }
}

function callWebServiceInfoKF() {
    if (!eshteInfoHapur()) return;
    var grida = $(pageState.gridaSelector);
    var idRreshti = grida.getLastSel2();
    var idKodi = grida.getTekstQelize('txtIdKodi', idRreshti);
    if (!idKodi)
        return;
    if (infoKf) {
        try {
            pastroInfoKf();
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "ktheInfoKF"), data: JSON.stringify({ idja: idKodi, data: data_DateEdit.GetDate(), idInfo: idInfoKf })
            }).done(SucceededCallbackInfoKF);
        }

        catch (e) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTeDhenave"));
            console.log(e);
        }
    }
}


function SucceededCallbackInfoKF(result) {
    if (result.colInfoTrupi.length != 0) {
        navbar.GetGroupByName('KF').SetExpanded(true);
        vendosInfo(lbxKF, result);
    }
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


function pastroInfoKf() {
    navbar.GetGroupByName('KF').SetExpanded(false);
    lbxKF.ClearItems();
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


function spliterPaneResized(s, e) {
    spliterPaneCollapsed(s, e);
}

function spliterPaneCollapsed(s, e) {
    var grida = $("#rowed5");

    if ($('#divgride2').width() != null) {
        myJQGrid.fixGridWidth(grida, $('#divgride2'));
    }
}


var mbyll = true;
function RuajHapurMbyllur(hapur) {
    mbyll = false;
    if (!hapur) { splitter.GetPane(1).CollapseBackward(); $('#hfHapurMbyllur').val('False'); }
    else $('#hfHapurMbyllur').val('True');
    var idPerdoruesi = hfState.Get('idPerdoruesi');
    try {
        $.ajax({
            pritPergjigje: true,
            url: Utils.getServerApiUrl("Rregjistrime", "ruajHapurMbyllur"),
            data: JSON.stringify({ hapur: hapur, idperdorues: idPerdoruesi, idperdoruesveprimi: idPerdoruesi })
        }).done(SuccededCallbackHapurMbyllur);
    }
    catch (e) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimRuajtje"));
    }
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
    if (!hapur) { splitter.GetPane(1).CollapseBackward(); $('#hfHapurMbyllur').val('False'); }
    else
        $('#hfHapurMbyllur').val('True');
    $.ajax({ pritPergjigje: true, url: Utils.getServerApiUrl("Rregjistrime", "ruajHapurMbyllurminus"), data: JSON.stringify({ hapur: hapur, idperdorues: idPerdoruesi, idperdoruesveprimi: idPerdoruesi }) }).done(SuccededCallbackHapurMbyllur);
}


function SuccededCallbackHapurMbyllur(result) {
    myMesazh.ShtoMesazhSuksesi(result);
}

function HeaderClick(s, e) {
    if (!mbyll) { e.cancel = true; mbyll = true; } //per rastet kur shtyp butonat + dhe -
}


function KontrolloTeDrejta(s) {
    var alti = $(s.GetValue()).attr('alt');
    if (alti == "KF" && $('#hfTeDrejtaInfoKF').val() == 'False')
        s.SetEnabled(false);
}


function Expanded() {
    lbxKF.SetHeight(lbxKF.GetItemCount() * 22 + 28);
}

function ButtonClickNavBar(s) {
    if (s.GetText().split('alt="')[1].split('"')[0] == "KF") {
        myButtonClickLupa.LupaUniversal_Click('Zgjidhni konfigurimin e infos se klient/furnitorit', 'LupaKonfigurimInfo.aspx?id=' + idInfoKf + '&ruaj=po', 600, 500);
        return;
    }

}
function SuccededCallbackInfoKfStructure(result) {
    vendosInfo(lbxKF, result, true);
}

function TextChangedGrupe(s, e) {
    if (s.GetValue() == null) {
        s.SetText('');
    }
}

function VendosTeDhenaPerKlienteNgaBRM(TeDhenaKlienti) {
    txtCustomerNr.SetText(TeDhenaKlienti.MSISDN);
    txtNrLlogari.SetText(TeDhenaKlienti.ACCOUNTNO);
    txtShoqeria.SetText(TeDhenaKlienti.EMRIKLIENTIT);
    txtMbiPagesa.SetText(TeDhenaKlienti.MBIPAGESA);
    myMesazh.ShtoMesazhSuksesi("Validimi perfundoi me sukses!");
    //mbyll popupin edhe mbush griden e faturave nese eshte nje nga keto dy lloje dokumenti
    var lloji = Utils.getUrlVar('lloji');
    if (lloji == "arketimLlogariKlienti" || lloji == "arketimAbonent");
    {
        popupUniversal.Hide();
        MbushGridenEfaturave();
        nvFatura.ExpandAll();

    }
    if (pageState.kushte["MKBRM"]) {
        var klientItem = furnitori_ComboBox.FindItemByText(TeDhenaKlienti.ACCOUNTNO);
        if (klientItem == undefined) {
            if (!TeDhenaKlienti.KLIENTI)
                myMesazh.ShtoMesazhGabimi("Klienti me kod " + TeDhenaKlienti.ACCOUNTNO + " nuk ekziston!");
            else {
                furnitori_ComboBox.AddItem([TeDhenaKlienti.KLIENTI.KodKlientFurnitor, TeDhenaKlienti.KLIENTI.EmertimiKF], TeDhenaKlienti.KLIENTI.IdKlientFurnitor);
                furnitori_ComboBox.SetValue(TeDhenaKlienti.KLIENTI.IdKlientFurnitor);
            }
        } else {
            furnitori_ComboBox.SetSelectedItem(klientItem);
        }
        VendosTeDhenaFurnitoriNeTrup(false);
    }
}

function ndryshoFormatimKursi(s, e) {
    if (pageState.isKursiTextChange) {
        pageState.isKursiTextChange = false;
        return;
    }
    Utils.lostFocusTxtNumer(s, e);
}

function ValuedChangedKursi(s, e) {
    pageState.isKursiTextChange = true;
}

function HapLupeValidimiPasLupesPaPrintuar() {
    lupaValidimHapur = false;
    if (hapLupeValidimi) {
        hapLupeValidimi = false;
        ButtonClickValidim();
    }
}

function cmbKonfigurimKaseInit(s, e) {
    var value = localStorage.getItem("kase_key" + pageState.idNdermarrje + "_" + pageState.idPerdoruesi) !== null ? localStorage.getItem("kase_key" + pageState.idNdermarrje + "_" + pageState.idPerdoruesi) : localStorage.getItem("kase_key" + pageState.idNdermarrje);
    if (value != null) {
        var item = cmbKonfigurimKase.FindItemByText(value);
        cmbKonfigurimKase.SetSelectedItem(item);
    }
}

function cmbKonfigurimKaseSelectedChanged(s, e) {
    var item = cmbKonfigurimKase.GetText();
    localStorage.setItem("kase_key" + pageState.idNdermarrje + "_" + pageState.idPerdoruesi, item);
}

function SucceededCallbackVendosPunonjesPerDisaRreshta(pergjigje) {
    var grida = $('#rowed5');
    grida.VendosDisaRreshtaMeSubjekte(pergjigje.result, pergjigje.rreshti, kontrolloPunonjes, vendosPunonjes, msgPunonjesPaLlogari);
}

function ktheDebiKrediSipasDok() {
    if (pageState.llojDokumenti == "terheqje" || pageState.llojDokumenti == "pagese")
        return "Debi";
    if (pageState.llojDokumenti == "derdhje" || pageState.llojDokumenti == "arketim" || pageState.llojDokumenti == "arketimLlogariKlienti" || pageState.llojDokumenti == "arketimAbonent")
        return "Kredi";
}
function merrKonfigurimeWebhook(idnderrmarje) {
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheKonfigurimeWebhook"), data: JSON.stringify({ idndermarje: idnderrmarje })
    }).done(SucceededCallbackWebhook);
}

function SucceededCallbackWebhook(result) {


    pageState.webhook = result;


}